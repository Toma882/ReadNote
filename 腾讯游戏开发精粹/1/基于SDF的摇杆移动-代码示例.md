# 基于SDF的摇杆移动 - 代码示例

> **来源**：《腾讯游戏开发精粹》- 第二部分 游戏数学 - 第1章  
> **用途**：理解SDF（有号距离场）在摇杆移动中的应用  
> **技术点**：SDF预计算、碰撞检测、碰撞响应、避免往返

---

## 📚 核心概念

### SDF（有号距离场）是什么？

**有号距离场（Signed Distance Field, SDF）**：
- **定义**：空间中每个点到最近障碍物边界的距离
- **符号**：
  - **正值**：点在障碍物外部（可通行区域）
  - **负值**：点在障碍物内部（不可通行区域）
  - **零值**：点在障碍物边界上
- **优势**：快速判断点是否在障碍物内，并知道距离边界的距离

---

## 💻 完整代码实现

### 1. SDF数据结构（基于栅格）

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// SDF数据结构 - 基于栅格
/// </summary>
public class SDFGrid
{
    public float[,] distanceField;  // 距离场数据
    public int width;               // 栅格宽度
    public int height;              // 栅格高度
    public float cellSize;          // 每个栅格单元的大小
    public Vector2 origin;          // 原点位置（世界坐标左下角）

    public SDFGrid(int width, int height, float cellSize, Vector2 origin)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
        this.distanceField = new float[width, height];
    }

    /// <summary>
    /// 世界坐标转栅格坐标
    /// </summary>
    public Vector2Int WorldToGrid(Vector2 worldPos)
    {
        Vector2 localPos = worldPos - origin;
        int x = Mathf.FloorToInt(localPos.x / cellSize);
        int y = Mathf.FloorToInt(localPos.y / cellSize);
        return new Vector2Int(
            Mathf.Clamp(x, 0, width - 1),
            Mathf.Clamp(y, 0, height - 1)
        );
    }

    /// <summary>
    /// 栅格坐标转世界坐标
    /// </summary>
    public Vector2 GridToWorld(Vector2Int gridPos)
    {
        return origin + new Vector2(
            gridPos.x * cellSize + cellSize * 0.5f,
            gridPos.y * cellSize + cellSize * 0.5f
        );
    }

    /// <summary>
    /// 获取世界坐标点的SDF值（双线性插值）
    /// </summary>
    public float GetSDF(Vector2 worldPos)
    {
        Vector2 localPos = worldPos - origin;
        float fx = localPos.x / cellSize;
        float fy = localPos.y / cellSize;

        int x0 = Mathf.FloorToInt(fx);
        int y0 = Mathf.FloorToInt(fy);
        int x1 = x0 + 1;
        int y1 = y0 + 1;

        // 边界检查
        x0 = Mathf.Clamp(x0, 0, width - 1);
        y0 = Mathf.Clamp(y0, 0, height - 1);
        x1 = Mathf.Clamp(x1, 0, width - 1);
        y1 = Mathf.Clamp(y1, 0, height - 1);

        // 双线性插值
        float dx = fx - x0;
        float dy = fy - y0;

        float sdf00 = distanceField[x0, y0];
        float sdf10 = distanceField[x1, y0];
        float sdf01 = distanceField[x0, y1];
        float sdf11 = distanceField[x1, y1];

        float sdf0 = Mathf.Lerp(sdf00, sdf10, dx);
        float sdf1 = Mathf.Lerp(sdf01, sdf11, dx);
        return Mathf.Lerp(sdf0, sdf1, dy);
    }

    /// <summary>
    /// 获取SDF梯度（法线方向，指向最近的可通行区域）
    /// </summary>
    public Vector2 GetGradient(Vector2 worldPos, float epsilon = 0.1f)
    {
        float sdfX0 = GetSDF(worldPos + new Vector2(-epsilon, 0));
        float sdfX1 = GetSDF(worldPos + new Vector2(epsilon, 0));
        float sdfY0 = GetSDF(worldPos + new Vector2(0, -epsilon));
        float sdfY1 = GetSDF(worldPos + new Vector2(0, epsilon));

        Vector2 gradient = new Vector2(
            (sdfX1 - sdfX0) / (2 * epsilon),
            (sdfY1 - sdfY0) / (2 * epsilon)
        );

        return gradient.normalized;
    }
}
```

### 2. SDF预计算（基于栅格数据）

```csharp
/// <summary>
/// SDF预计算器 - 使用距离变换算法
/// </summary>
public class SDFCalculator
{
    /// <summary>
    /// 从障碍物栅格生成SDF
    /// true = 障碍物，false = 可通行
    /// </summary>
    public static SDFGrid GenerateSDF(bool[,] obstacleGrid, float cellSize, Vector2 origin)
    {
        int width = obstacleGrid.GetLength(0);
        int height = obstacleGrid.GetLength(1);
        SDFGrid sdfGrid = new SDFGrid(width, height, cellSize, origin);

        // 第一步：初始化距离场
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (obstacleGrid[x, y])
                {
                    // 障碍物内部，设为负值（距离边界为0）
                    sdfGrid.distanceField[x, y] = -0.5f * cellSize;
                }
                else
                {
                    // 可通行区域，设为很大的正值
                    sdfGrid.distanceField[x, y] = float.MaxValue;
                }
            }
        }

        // 第二步：使用距离变换算法计算SDF
        // 这里使用简化的8方向距离变换
        CalculateDistanceTransform(sdfGrid, obstacleGrid, cellSize);

        return sdfGrid;
    }

    /// <summary>
    /// 距离变换算法（8方向）
    /// </summary>
    private static void CalculateDistanceTransform(SDFGrid sdfGrid, bool[,] obstacleGrid, float cellSize)
    {
        int width = sdfGrid.width;
        int height = sdfGrid.height;

        // 前向扫描
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!obstacleGrid[x, y])
                {
                    float minDist = sdfGrid.distanceField[x, y];

                    // 检查8个方向的邻居
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (dx == 0 && dy == 0) continue;

                            int nx = x + dx;
                            int ny = y + dy;

                            if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                            {
                                float dist = sdfGrid.distanceField[nx, ny];
                                float stepDist = new Vector2(dx, dy).magnitude * cellSize;
                                minDist = Mathf.Min(minDist, dist + stepDist);
                            }
                        }
                    }

                    sdfGrid.distanceField[x, y] = minDist;
                }
            }
        }

        // 后向扫描
        for (int x = width - 1; x >= 0; x--)
        {
            for (int y = height - 1; y >= 0; y--)
            {
                if (!obstacleGrid[x, y])
                {
                    float minDist = sdfGrid.distanceField[x, y];

                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (dx == 0 && dy == 0) continue;

                            int nx = x + dx;
                            int ny = y + dy;

                            if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                            {
                                float dist = sdfGrid.distanceField[nx, ny];
                                float stepDist = new Vector2(dx, dy).magnitude * cellSize;
                                minDist = Mathf.Min(minDist, dist + stepDist);
                            }
                        }
                    }

                    sdfGrid.distanceField[x, y] = minDist;
                }
            }
        }
    }
}
```

### 3. 基于SDF的摇杆移动控制器

```csharp
/// <summary>
/// 基于SDF的摇杆移动控制器
/// </summary>
public class SDFJoystickMovement : MonoBehaviour
{
    [Header("SDF设置")]
    public SDFGrid sdfGrid;
    public float characterRadius = 0.5f;  // 角色半径

    [Header("移动设置")]
    public float moveSpeed = 5f;
    public float collisionResponseStrength = 10f;  // 碰撞响应强度

    [Header("避免往返设置")]
    public float avoidRoundTripDistance = 0.1f;  // 避免往返的最小距离
    private Vector2 lastValidPosition;  // 上一个有效位置

    [Header("调试")]
    public bool showDebugGizmos = true;

    private Vector2 currentVelocity;
    private Vector2 targetPosition;

    void Start()
    {
        lastValidPosition = transform.position;
    }

    void Update()
    {
        // 获取摇杆输入（这里用键盘模拟，实际项目中用摇杆输入）
        Vector2 input = GetJoystickInput();

        if (input.magnitude > 0.1f)
        {
            // 计算目标位置
            targetPosition = (Vector2)transform.position + input * moveSpeed * Time.deltaTime;

            // 基于SDF的移动处理
            Vector2 newPosition = ProcessMovementWithSDF(targetPosition);

            // 避免往返检测
            newPosition = AvoidRoundTrip(newPosition);

            // 更新位置
            transform.position = newPosition;
            lastValidPosition = newPosition;
        }
    }

    /// <summary>
    /// 获取摇杆输入（示例：用WASD模拟）
    /// </summary>
    private Vector2 GetJoystickInput()
    {
        Vector2 input = Vector2.zero;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        return input.normalized;
    }

    /// <summary>
    /// 基于SDF处理移动（碰撞检测与响应）
    /// </summary>
    private Vector2 ProcessMovementWithSDF(Vector2 targetPos)
    {
        if (sdfGrid == null)
        {
            return targetPos;
        }

        // 获取当前位置和目标位置的SDF值
        float currentSDF = sdfGrid.GetSDF(transform.position);
        float targetSDF = sdfGrid.GetSDF(targetPos);

        // 情况1：角色在障碍物内部（SDF < 0）
        if (currentSDF < 0)
        {
            return MoveOutOfObstacle();
        }

        // 情况2：目标位置在障碍物内
        if (targetSDF < characterRadius)
        {
            // 碰撞响应：沿着SDF梯度方向推出
            Vector2 gradient = sdfGrid.GetGradient(targetPos);
            float pushDistance = characterRadius - targetSDF;
            return targetPos + gradient * pushDistance;
        }

        // 情况3：正常移动，但需要确保路径安全
        return ValidatePath(transform.position, targetPos);
    }

    /// <summary>
    /// 将角色从障碍物区域移出
    /// </summary>
    private Vector2 MoveOutOfObstacle()
    {
        Vector2 currentPos = transform.position;
        Vector2 gradient = sdfGrid.GetGradient(currentPos);

        // 沿着梯度方向（指向可通行区域）移动
        float currentSDF = sdfGrid.GetSDF(currentPos);
        float pushDistance = Mathf.Abs(currentSDF) + characterRadius;

        return currentPos + gradient * pushDistance;
    }

    /// <summary>
    /// 验证路径安全性（检查路径上的点）
    /// </summary>
    private Vector2 ValidatePath(Vector2 start, Vector2 end)
    {
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);
        int steps = Mathf.CeilToInt(distance / (characterRadius * 0.5f));

        Vector2 safeEnd = end;

        // 沿着路径采样检查
        for (int i = 1; i <= steps; i++)
        {
            float t = (float)i / steps;
            Vector2 samplePos = Vector2.Lerp(start, end, t);
            float sdf = sdfGrid.GetSDF(samplePos);

            if (sdf < characterRadius)
            {
                // 遇到障碍物，停止在这里
                Vector2 gradient = sdfGrid.GetGradient(samplePos);
                safeEnd = samplePos - direction * (characterRadius - sdf);
                break;
            }
        }

        return safeEnd;
    }

    /// <summary>
    /// 避免往返（防止角色在障碍物边缘来回移动）
    /// </summary>
    private Vector2 AvoidRoundTrip(Vector2 newPos)
    {
        float distanceToLast = Vector2.Distance(newPos, lastValidPosition);

        // 如果移动距离太小，可能是往返，保持原位置
        if (distanceToLast < avoidRoundTripDistance)
        {
            return lastValidPosition;
        }

        return newPos;
    }

    /// <summary>
    /// 远距离移动（不能越过障碍物）
    /// </summary>
    public bool MoveToPosition(Vector2 targetPos)
    {
        Vector2 startPos = transform.position;
        Vector2 direction = (targetPos - startPos).normalized;
        float totalDistance = Vector2.Distance(startPos, targetPos);

        // 沿着路径逐步移动，遇到障碍物停止
        Vector2 currentPos = startPos;
        float movedDistance = 0f;
        float stepSize = characterRadius * 0.5f;

        while (movedDistance < totalDistance)
        {
            float remainingDistance = totalDistance - movedDistance;
            float step = Mathf.Min(stepSize, remainingDistance);
            Vector2 nextPos = currentPos + direction * step;

            float sdf = sdfGrid.GetSDF(nextPos);
            if (sdf < characterRadius)
            {
                // 遇到障碍物，停止移动
                break;
            }

            currentPos = nextPos;
            movedDistance += step;
        }

        transform.position = currentPos;
        return Vector2.Distance(currentPos, targetPos) < 0.1f;  // 是否到达目标
    }

    void OnDrawGizmos()
    {
        if (!showDebugGizmos || sdfGrid == null)
            return;

        // 绘制角色半径
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, characterRadius);

        // 绘制当前位置的SDF值
        float sdf = sdfGrid.GetSDF(transform.position);
        Vector2 gradient = sdfGrid.GetGradient(transform.position);

        // 绘制SDF梯度方向
        Gizmos.color = sdf < 0 ? Color.red : Color.blue;
        Gizmos.DrawRay(transform.position, gradient * Mathf.Abs(sdf));
    }
}
```

### 4. SDF管理器（初始化和管理）

```csharp
/// <summary>
/// SDF管理器 - 负责初始化和更新SDF
/// </summary>
public class SDFManager : MonoBehaviour
{
    [Header("SDF生成设置")]
    public int gridWidth = 100;
    public int gridHeight = 100;
    public float cellSize = 0.5f;
    public Vector2 gridOrigin = Vector2.zero;

    [Header("障碍物设置")]
    public LayerMask obstacleLayer;
    public float obstacleCheckRadius = 0.25f;

    private SDFGrid sdfGrid;
    private bool[,] obstacleGrid;

    void Start()
    {
        GenerateSDF();
    }

    /// <summary>
    /// 生成SDF
    /// </summary>
    public void GenerateSDF()
    {
        // 第一步：扫描场景，生成障碍物栅格
        obstacleGrid = ScanObstacles();

        // 第二步：从障碍物栅格生成SDF
        sdfGrid = SDFCalculator.GenerateSDF(obstacleGrid, cellSize, gridOrigin);

        Debug.Log($"SDF生成完成: {gridWidth}x{gridHeight}, 单元大小: {cellSize}");
    }

    /// <summary>
    /// 扫描场景中的障碍物，生成障碍物栅格
    /// </summary>
    private bool[,] ScanObstacles()
    {
        bool[,] grid = new bool[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 worldPos = sdfGrid != null 
                    ? sdfGrid.GridToWorld(new Vector2Int(x, y))
                    : gridOrigin + new Vector2(x * cellSize, y * cellSize);

                // 检查该位置是否有障碍物
                Collider2D collider = Physics2D.OverlapCircle(worldPos, obstacleCheckRadius, obstacleLayer);
                grid[x, y] = collider != null;
            }
        }

        return grid;
    }

    /// <summary>
    /// 更新动态障碍物（运行时更新SDF）
    /// </summary>
    public void UpdateDynamicObstacles()
    {
        // 重新扫描障碍物
        obstacleGrid = ScanObstacles();

        // 重新生成SDF
        sdfGrid = SDFCalculator.GenerateSDF(obstacleGrid, cellSize, gridOrigin);
    }

    /// <summary>
    /// 获取SDF网格（供其他组件使用）
    /// </summary>
    public SDFGrid GetSDFGrid()
    {
        return sdfGrid;
    }

    void OnDrawGizmos()
    {
        if (sdfGrid == null)
            return;

        // 绘制SDF可视化（可选）
        // 这里可以绘制SDF的等值线等
    }
}
```

### 5. 使用示例

```csharp
/// <summary>
/// 使用示例
/// </summary>
public class SDFMovementExample : MonoBehaviour
{
    public SDFManager sdfManager;
    public SDFJoystickMovement character;

    void Start()
    {
        // 初始化SDF管理器
        if (sdfManager == null)
        {
            sdfManager = FindObjectOfType<SDFManager>();
        }

        // 设置角色的SDF网格
        if (character != null && sdfManager != null)
        {
            character.sdfGrid = sdfManager.GetSDFGrid();
        }
    }

    void Update()
    {
        // 示例：处理动态障碍物
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 按R键更新动态障碍物
            sdfManager.UpdateDynamicObstacles();
            character.sdfGrid = sdfManager.GetSDFGrid();
        }

        // 示例：远距离移动
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPos = new Vector2(mousePos.x, mousePos.y);
            character.MoveToPosition(targetPos);
        }
    }
}
```

---

## 🎯 核心要点总结

### 1. SDF的优势

- **快速碰撞检测**：O(1)时间判断点是否在障碍物内
- **平滑移动**：知道到边界的距离，可以平滑推出
- **预计算**：SDF可以预先计算，运行时查询很快

### 2. 碰撞检测与响应

- **检测**：通过SDF值判断（< 0 = 在障碍物内，< radius = 碰撞）
- **响应**：沿着SDF梯度方向推出（梯度指向最近的可通行区域）

### 3. 避免往返

- **问题**：角色可能在障碍物边缘来回移动
- **解决**：记录上一个有效位置，如果移动距离太小则保持原位置

### 4. 远距离移动

- **问题**：直接移动到目标可能穿过障碍物
- **解决**：沿着路径逐步检查，遇到障碍物停止

### 5. 动态障碍物

- **更新**：重新扫描障碍物，重新生成SDF
- **性能**：SDF生成有开销，需要权衡更新频率

---

## 📝 使用步骤

1. **创建SDF管理器**：在场景中添加`SDFManager`组件
2. **设置障碍物层**：配置`obstacleLayer`，标记障碍物
3. **生成SDF**：运行时调用`GenerateSDF()`
4. **添加角色控制器**：给角色添加`SDFJoystickMovement`组件
5. **连接SDF网格**：将SDF管理器的网格赋值给角色控制器

---

## 🔧 优化建议

1. **SDF预计算**：在加载时预计算，避免运行时计算
2. **分层SDF**：使用多级SDF（粗粒度+细粒度）提高性能
3. **局部更新**：只更新动态障碍物区域的SDF
4. **缓存查询**：缓存常用的SDF查询结果

---

**参考**：《腾讯游戏开发精粹》- 第二部分 游戏数学 - 第1章 基于SDF的摇杆移动

