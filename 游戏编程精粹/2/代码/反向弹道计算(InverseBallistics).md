# 反向弹道计算（Inverse Ballistics）- C#实现

## 核心概念

- **定义**：计算发射角度，使弹道能够命中指定目标位置
- **核心思想**：基于物理运动学公式，考虑重力、初速度、目标位置，通过数学计算求解发射角度
- **应用场景**：弹道类游戏、塔防游戏、射击游戏的AI瞄准系统、投掷物轨迹计算
- **Unity应用**：Unity没有直接提供，需要自己实现或使用第三方插件

---

## 核心原理

### 物理公式

**抛体运动方程**：
```
x(t) = v₀ * cos(θ) * t
y(t) = v₀ * sin(θ) * t - 0.5 * g * t²
```

其中：
- `v₀`：初速度
- `θ`：发射角度
- `t`：时间
- `g`：重力加速度

### 求解方法

给定目标位置 `(x, y)` 和初速度 `v₀`，求解发射角度 `θ`：

1. **从x方向方程**：`t = x / (v₀ * cos(θ))`
2. **代入y方向方程**：`y = x * tan(θ) - (g * x²) / (2 * v₀² * cos²(θ))`
3. **使用三角恒等式**：`1 + tan²(θ) = 1 / cos²(θ)`
4. **得到二次方程**：`tan²(θ) - (2 * v₀² / (g * x)) * tan(θ) + (2 * v₀² * y / (g * x²) + 1) = 0`

### 解的情况

- **两个解**：高抛（高角度）和低抛（低角度）
- **一个解**：目标刚好在最大射程
- **无解**：目标超出射程

---

## 实现代码

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 反向弹道计算结果
/// </summary>
public struct BallisticSolution
{
    public bool isValid;           // 是否有解
    public float angle1;           // 第一个角度（低抛）
    public float angle2;           // 第二个角度（高抛）
    public float flightTime1;      // 第一个角度的飞行时间
    public float flightTime2;      // 第二个角度的飞行时间
    public Vector3 velocity1;       // 第一个角度的初速度
    public Vector3 velocity2;       // 第二个角度的初速度
    
    public BallisticSolution(bool valid)
    {
        isValid = valid;
        angle1 = 0f;
        angle2 = 0f;
        flightTime1 = 0f;
        flightTime2 = 0f;
        velocity1 = Vector3.zero;
        velocity2 = Vector3.zero;
    }
}

/// <summary>
/// 反向弹道计算工具类
/// </summary>
public static class InverseBallistics
{
    /// <summary>
    /// 计算2D反向弹道（在XZ平面上，Y为高度）
    /// </summary>
    /// <param name="startPos">起始位置</param>
    /// <param name="targetPos">目标位置</param>
    /// <param name="initialSpeed">初速度</param>
    /// <param name="gravity">重力加速度（通常为Physics.gravity.y的绝对值）</param>
    /// <returns>弹道解</returns>
    public static BallisticSolution Calculate2D(Vector3 startPos, Vector3 targetPos, float initialSpeed, float gravity = 9.81f)
    {
        BallisticSolution solution = new BallisticSolution(false);
        
        // 计算相对位置
        Vector3 displacement = targetPos - startPos;
        float x = displacement.x;
        float y = displacement.y;
        float z = displacement.z;
        
        // 计算水平距离（在XZ平面上）
        float horizontalDistance = Mathf.Sqrt(x * x + z * z);
        
        // 如果水平距离太小，无法计算
        if (horizontalDistance < 0.01f)
        {
            return solution;
        }
        
        // 计算角度（使用2D公式）
        float angle1, angle2;
        bool hasSolution = CalculateAngle2D(horizontalDistance, y, initialSpeed, gravity, out angle1, out angle2);
        
        if (!hasSolution)
        {
            return solution;
        }
        
        solution.isValid = true;
        solution.angle1 = angle1;
        solution.angle2 = angle2;
        
        // 计算飞行时间
        solution.flightTime1 = CalculateFlightTime(horizontalDistance, angle1, initialSpeed);
        solution.flightTime2 = CalculateFlightTime(horizontalDistance, angle2, initialSpeed);
        
        // 计算初速度向量
        Vector3 horizontalDirection = new Vector3(x, 0, z).normalized;
        solution.velocity1 = CalculateVelocity(horizontalDirection, angle1, initialSpeed);
        solution.velocity2 = CalculateVelocity(horizontalDirection, angle2, initialSpeed);
        
        return solution;
    }
    
    /// <summary>
    /// 计算发射角度（2D）
    /// </summary>
    private static bool CalculateAngle2D(float horizontalDistance, float height, float initialSpeed, float gravity, 
        out float angle1, out float angle2)
    {
        angle1 = 0f;
        angle2 = 0f;
        
        // 计算二次方程的系数
        float a = gravity * horizontalDistance * horizontalDistance / (2 * initialSpeed * initialSpeed);
        float b = -horizontalDistance;
        float c = a - height;
        
        // 判别式
        float discriminant = b * b - 4 * a * c;
        
        // 如果判别式小于0，无解
        if (discriminant < 0)
        {
            return false;
        }
        
        // 计算两个解
        float sqrtDiscriminant = Mathf.Sqrt(discriminant);
        float tan1 = (-b - sqrtDiscriminant) / (2 * a);
        float tan2 = (-b + sqrtDiscriminant) / (2 * a);
        
        angle1 = Mathf.Atan(tan1) * Mathf.Rad2Deg;
        angle2 = Mathf.Atan(tan2) * Mathf.Rad2Deg;
        
        // 确保角度在合理范围内（0-90度）
        if (angle1 < 0 || angle1 > 90)
        {
            angle1 = -1;
        }
        if (angle2 < 0 || angle2 > 90)
        {
            angle2 = -1;
        }
        
        return angle1 >= 0 || angle2 >= 0;
    }
    
    /// <summary>
    /// 计算飞行时间
    /// </summary>
    private static float CalculateFlightTime(float horizontalDistance, float angle, float initialSpeed)
    {
        if (angle < 0)
        {
            return 0f;
        }
        
        float angleRad = angle * Mathf.Deg2Rad;
        float horizontalSpeed = initialSpeed * Mathf.Cos(angleRad);
        
        if (horizontalSpeed < 0.001f)
        {
            return 0f;
        }
        
        return horizontalDistance / horizontalSpeed;
    }
    
    /// <summary>
    /// 计算初速度向量
    /// </summary>
    private static Vector3 CalculateVelocity(Vector3 horizontalDirection, float angle, float initialSpeed)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        float horizontalSpeed = initialSpeed * Mathf.Cos(angleRad);
        float verticalSpeed = initialSpeed * Mathf.Sin(angleRad);
        
        return horizontalDirection * horizontalSpeed + Vector3.up * verticalSpeed;
    }
    
    /// <summary>
    /// 计算3D反向弹道（考虑所有方向）
    /// </summary>
    public static BallisticSolution Calculate3D(Vector3 startPos, Vector3 targetPos, float initialSpeed, float gravity = 9.81f)
    {
        // 3D情况可以简化为2D（在包含起始点和目标点的垂直平面上）
        return Calculate2D(startPos, targetPos, initialSpeed, gravity);
    }
    
    /// <summary>
    /// 验证弹道解（通过正向模拟）
    /// </summary>
    public static bool ValidateSolution(Vector3 startPos, Vector3 targetPos, Vector3 initialVelocity, float gravity, float tolerance = 0.5f)
    {
        // 使用正向弹道模拟验证
        Vector3 pos = startPos;
        Vector3 velocity = initialVelocity;
        float timeStep = 0.02f;
        float maxTime = 10f;
        float elapsedTime = 0f;
        
        while (elapsedTime < maxTime)
        {
            // 更新位置
            pos += velocity * timeStep;
            
            // 更新速度（考虑重力）
            velocity += Vector3.down * gravity * timeStep;
            
            // 检查是否到达目标
            if (Vector3.Distance(pos, targetPos) < tolerance)
            {
                return true;
            }
            
            // 如果已经超过目标（在目标下方），失败
            if (pos.y < targetPos.y && Vector3.Distance(new Vector3(pos.x, 0, pos.z), new Vector3(targetPos.x, 0, targetPos.z)) < tolerance)
            {
                return false;
            }
            
            elapsedTime += timeStep;
        }
        
        return false;
    }
}

/// <summary>
/// 反向弹道计算器组件
/// </summary>
public class InverseBallisticsCalculator : MonoBehaviour
{
    [Header("弹道参数")]
    public float initialSpeed = 20f;
    public float gravity = 9.81f;
    public bool useLowAngle = true;  // 使用低角度还是高角度
    
    [Header("调试")]
    public bool showTrajectory = true;
    public int trajectoryPoints = 50;
    public Color trajectoryColor = Color.red;
    
    private LineRenderer trajectoryLine;
    
    void Start()
    {
        // 创建轨迹线
        if (showTrajectory)
        {
            GameObject lineObj = new GameObject("TrajectoryLine");
            lineObj.transform.SetParent(transform);
            trajectoryLine = lineObj.AddComponent<LineRenderer>();
            trajectoryLine.material = new Material(Shader.Find("Sprites/Default"));
            trajectoryLine.color = trajectoryColor;
            trajectoryLine.width = 0.1f;
            trajectoryLine.positionCount = trajectoryPoints;
        }
    }
    
    /// <summary>
    /// 计算并应用弹道
    /// </summary>
    public bool CalculateAndApply(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        BallisticSolution solution = InverseBallistics.Calculate2D(startPos, targetPos, initialSpeed, gravity);
        
        if (!solution.isValid)
        {
            Debug.LogWarning("无法计算弹道：目标超出射程");
            return false;
        }
        
        // 选择角度（低角度或高角度）
        float angle = useLowAngle ? solution.angle1 : solution.angle2;
        Vector3 velocity = useLowAngle ? solution.velocity1 : solution.velocity2;
        float flightTime = useLowAngle ? solution.flightTime1 : solution.flightTime2;
        
        if (angle < 0)
        {
            Debug.LogWarning("所选角度无效，尝试使用另一个角度");
            angle = useLowAngle ? solution.angle2 : solution.angle1;
            velocity = useLowAngle ? solution.velocity2 : solution.velocity1;
            flightTime = useLowAngle ? solution.flightTime2 : solution.flightTime1;
        }
        
        if (angle < 0)
        {
            Debug.LogError("两个角度都无效");
            return false;
        }
        
        Debug.Log($"弹道计算成功：角度={angle:F2}°, 飞行时间={flightTime:F2}s");
        
        // 可视化轨迹
        if (showTrajectory && trajectoryLine != null)
        {
            DrawTrajectory(startPos, velocity, gravity);
        }
        
        return true;
    }
    
    /// <summary>
    /// 绘制轨迹
    /// </summary>
    private void DrawTrajectory(Vector3 startPos, Vector3 initialVelocity, float gravity)
    {
        Vector3 pos = startPos;
        Vector3 velocity = initialVelocity;
        float timeStep = 0.1f;
        float maxTime = 10f;
        float elapsedTime = 0f;
        
        List<Vector3> points = new List<Vector3>();
        points.Add(startPos);
        
        while (elapsedTime < maxTime && points.Count < trajectoryPoints)
        {
            pos += velocity * timeStep;
            velocity += Vector3.down * gravity * timeStep;
            points.Add(pos);
            elapsedTime += timeStep;
            
            // 如果到达地面，停止
            if (pos.y < startPos.y - 10f)
            {
                break;
            }
        }
        
        trajectoryLine.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            trajectoryLine.SetPosition(i, points[i]);
        }
    }
    
    /// <summary>
    /// 获取弹道解（不应用）
    /// </summary>
    public BallisticSolution GetSolution(Vector3 targetPos)
    {
        return InverseBallistics.Calculate2D(transform.position, targetPos, initialSpeed, gravity);
    }
}

/// <summary>
/// 弹道预测器（用于AI瞄准）
/// </summary>
public class BallisticPredictor : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public float gravity = 9.81f;
    public Transform target;
    
    void Update()
    {
        if (target == null)
            return;
        
        // 计算反向弹道
        BallisticSolution solution = InverseBallistics.Calculate2D(
            transform.position,
            target.position,
            projectileSpeed,
            gravity
        );
        
        if (solution.isValid)
        {
            // 使用低角度（更直接）
            float angle = solution.angle1 >= 0 ? solution.angle1 : solution.angle2;
            Vector3 velocity = solution.angle1 >= 0 ? solution.velocity1 : solution.velocity2;
            
            // 应用瞄准（例如：旋转炮台）
            if (angle >= 0)
            {
                Vector3 direction = velocity.normalized;
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
```

---

## 使用示例

### 示例1：基础使用

```csharp
// 计算从A点到B点的弹道
Vector3 startPos = new Vector3(0, 0, 0);
Vector3 targetPos = new Vector3(10, 5, 0);
float initialSpeed = 20f;
float gravity = 9.81f;

BallisticSolution solution = InverseBallistics.Calculate2D(startPos, targetPos, initialSpeed, gravity);

if (solution.isValid)
{
    // 使用低角度（更直接）
    float angle = solution.angle1;
    Vector3 velocity = solution.velocity1;
    
    // 发射物体
    Rigidbody projectile = GetComponent<Rigidbody>();
    projectile.velocity = velocity;
}
```

### 示例2：塔防游戏

```csharp
public class Tower : MonoBehaviour
{
    public float projectileSpeed = 15f;
    public Transform firePoint;
    public GameObject projectilePrefab;
    
    public void FireAtTarget(Vector3 targetPos)
    {
        BallisticSolution solution = InverseBallistics.Calculate2D(
            firePoint.position,
            targetPos,
            projectileSpeed,
            Physics.gravity.magnitude
        );
        
        if (solution.isValid)
        {
            // 使用低角度
            Vector3 velocity = solution.velocity1;
            
            // 创建并发射弹丸
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = velocity;
        }
    }
}
```

---

## Unity应用建议

1. **使用Unity Physics**：
   - 可以使用`Rigidbody`配合计算出的初速度
   - 设置`Physics.gravity`为实际重力值
   - 使用`Rigidbody.AddForce()`应用初始速度

2. **性能优化**：
   - 缓存计算结果
   - 使用对象池管理弹丸
   - 分帧计算（如果有很多目标）

3. **特殊情况处理**：
   - 目标移动：需要预测目标位置
   - 障碍物：需要检查弹道是否被阻挡
   - 风力影响：需要额外考虑风力因素

---

## 参考文献

- 《游戏编程精粹2》- 2.4 反向弹道计算

