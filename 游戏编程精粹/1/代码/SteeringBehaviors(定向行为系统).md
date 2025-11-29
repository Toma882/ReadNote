## Steering Behaviors（定向行为系统）- C#实现

### 核心概念

- **定义**：Reynolds提出的基础行为系统，用于实现各种移动行为
- **核心思想**：每个行为返回一个Steering（转向）矢量，多个行为可以组合，最终矢量决定Agent的移动方向
- **优势**：模块化设计，可以灵活组合多个行为

### 基础数据结构

```csharp
using UnityEngine;

/// <summary>
/// Steering（转向）数据结构：包含线性和角速度
/// </summary>
public struct Steering
{
    public Vector3 linear;   // 线性转向力（移动方向）
    public float angular;    // 角转向力（旋转方向）
    
    public Steering(Vector3 linear, float angular)
    {
        this.linear = linear;
        this.angular = angular;
    }
    
    public static Steering operator +(Steering a, Steering b)
    {
        return new Steering(a.linear + b.linear, a.angular + b.angular);
    }
}

/// <summary>
/// Agent基础类：包含位置、速度、朝向等属性
/// </summary>
public class Agent
{
    public Vector3 position;
    public Vector3 velocity;
    public float orientation;  // 朝向角度（弧度）
    public float maxSpeed;
    public float maxAccel;
    public float maxRotation;
    public float maxAngularAccel;
}
```

### 基础行为实现

#### 1. Seek（寻找目标）

**功能**：朝着目标位置移动

```csharp
public class Seek : SteeringBehavior
{
    public Transform target;
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 计算到目标的方向
        Vector3 direction = target.position - agent.position;
        direction.Normalize();
        
        // 计算期望速度
        Vector3 desiredVelocity = direction * agent.maxSpeed;
        
        // 计算转向力
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear = Vector3.ClampMagnitude(steering.linear, agent.maxAccel);
        
        return steering;
    }
}
```

#### 2. Flee（逃离目标）

**功能**：远离目标位置

```csharp
public class Flee : SteeringBehavior
{
    public Transform target;
    public float panicDistance = 10f;  // 恐慌距离，超过此距离不再逃离
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 计算到目标的距离
        Vector3 direction = agent.position - target.position;
        float distance = direction.magnitude;
        
        // 如果距离太远，不逃离
        if (distance > panicDistance)
        {
            return steering;  // 返回零转向
        }
        
        direction.Normalize();
        
        // 计算期望速度（远离目标）
        Vector3 desiredVelocity = direction * agent.maxSpeed;
        
        // 计算转向力
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear = Vector3.ClampMagnitude(steering.linear, agent.maxAccel);
        
        return steering;
    }
}
```

#### 3. Arrive（到达目标）

**功能**：到达目标位置，带减速效果

```csharp
public class Arrive : SteeringBehavior
{
    public Transform target;
    public float targetRadius = 1f;      // 到达半径
    public float slowRadius = 5f;        // 减速半径
    public float timeToTarget = 0.25f;  // 到达目标的时间
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 计算到目标的方向和距离
        Vector3 direction = target.position - agent.position;
        float distance = direction.magnitude;
        
        // 如果已经在目标范围内，停止
        if (distance < targetRadius)
        {
            return steering;  // 返回零转向
        }
        
        // 计算目标速度
        float targetSpeed;
        if (distance > slowRadius)
        {
            // 在减速半径外，使用最大速度
            targetSpeed = agent.maxSpeed;
        }
        else
        {
            // 在减速半径内，按比例减速
            targetSpeed = agent.maxSpeed * (distance / slowRadius);
        }
        
        // 计算期望速度
        Vector3 desiredVelocity = direction.normalized * targetSpeed;
        
        // 计算转向力
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear /= timeToTarget;  // 调整加速度
        
        // 限制加速度
        if (steering.linear.magnitude > agent.maxAccel)
        {
            steering.linear = steering.linear.normalized * agent.maxAccel;
        }
        
        return steering;
    }
}
```

#### 4. Pursue（追逐移动目标）

**功能**：追逐移动的目标，预测目标未来位置

```csharp
public class Pursue : SteeringBehavior
{
    public Agent target;
    public float maxPrediction = 5f;  // 最大预测时间
    
    public override Steering GetSteering()
    {
        // 计算到目标的距离
        Vector3 direction = target.position - agent.position;
        float distance = direction.magnitude;
        
        // 计算当前速度
        float speed = agent.velocity.magnitude;
        
        // 计算预测时间
        float prediction;
        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }
        
        // 预测目标未来位置
        Vector3 targetPosition = target.position + target.velocity * prediction;
        
        // 使用Seek行为朝向预测位置
        Vector3 seekDirection = targetPosition - agent.position;
        seekDirection.Normalize();
        
        Vector3 desiredVelocity = seekDirection * agent.maxSpeed;
        
        Steering steering = new Steering();
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear = Vector3.ClampMagnitude(steering.linear, agent.maxAccel);
        
        return steering;
    }
}
```

#### 5. Evade（躲避移动目标）

**功能**：躲避移动的目标，预测目标未来位置

```csharp
public class Evade : SteeringBehavior
{
    public Agent target;
    public float maxPrediction = 5f;
    public float panicDistance = 10f;  // 恐慌距离
    
    public override Steering GetSteering()
    {
        // 计算到目标的距离
        Vector3 direction = agent.position - target.position;
        float distance = direction.magnitude;
        
        // 如果距离太远，不躲避
        if (distance > panicDistance)
        {
            return new Steering();
        }
        
        // 计算预测时间
        float speed = agent.velocity.magnitude;
        float prediction;
        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }
        
        // 预测目标未来位置
        Vector3 targetPosition = target.position + target.velocity * prediction;
        
        // 使用Flee行为远离预测位置
        Vector3 fleeDirection = agent.position - targetPosition;
        fleeDirection.Normalize();
        
        Vector3 desiredVelocity = fleeDirection * agent.maxSpeed;
        
        Steering steering = new Steering();
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear = Vector3.ClampMagnitude(steering.linear, agent.maxAccel);
        
        return steering;
    }
}
```

#### 6. Align（对齐朝向）

**功能**：对齐到目标朝向

```csharp
public class Align : SteeringBehavior
{
    public Agent target;
    public float targetRadius = 0.1f;      // 目标角度范围（弧度）
    public float slowRadius = 0.5f;       // 减速角度范围（弧度）
    public float timeToTarget = 0.1f;     // 到达目标的时间
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 计算角度差（归一化到-π到π）
        float rotation = target.orientation - agent.orientation;
        rotation = MapToRange(rotation);  // 映射到-π到π范围
        
        float rotationSize = Mathf.Abs(rotation);
        
        // 如果已经在目标范围内，停止
        if (rotationSize < targetRadius)
        {
            return steering;
        }
        
        // 计算目标角速度
        float targetRotation;
        if (rotationSize > slowRadius)
        {
            targetRotation = agent.maxRotation;
        }
        else
        {
            targetRotation = agent.maxRotation * (rotationSize / slowRadius);
        }
        
        // 根据旋转方向设置符号
        targetRotation *= rotation / rotationSize;
        
        // 计算角加速度
        steering.angular = targetRotation - agent.angular;
        steering.angular /= timeToTarget;
        
        // 限制角加速度
        if (Mathf.Abs(steering.angular) > agent.maxAngularAccel)
        {
            steering.angular = Mathf.Sign(steering.angular) * agent.maxAngularAccel;
        }
        
        return steering;
    }
    
    private float MapToRange(float rotation)
    {
        while (rotation > Mathf.PI)
            rotation -= 2f * Mathf.PI;
        while (rotation < -Mathf.PI)
            rotation += 2f * Mathf.PI;
        return rotation;
    }
}
```

#### 7. VelocityMatch（速度匹配）

**功能**：匹配目标的速度

```csharp
public class VelocityMatch : SteeringBehavior
{
    public Agent target;
    public float timeToTarget = 0.1f;
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 计算期望速度（目标速度）
        Vector3 desiredVelocity = target.velocity;
        
        // 计算转向力
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear /= timeToTarget;
        
        // 限制加速度
        if (steering.linear.magnitude > agent.maxAccel)
        {
            steering.linear = steering.linear.normalized * agent.maxAccel;
        }
        
        return steering;
    }
}
```

#### 8. Wander（漫游）

**功能**：随机漫游行为

```csharp
public class Wander : SteeringBehavior
{
    public float wanderOffset = 1.5f;    // 漫游中心点距离
    public float wanderRadius = 1.0f;   // 漫游半径
    public float wanderRate = 0.5f;     // 漫游角度变化率
    
    private float wanderOrientation = 0f;  // 当前漫游角度
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 更新漫游角度（随机变化）
        wanderOrientation += Random.Range(-1f, 1f) * wanderRate;
        
        // 计算目标朝向（当前朝向 + 漫游角度）
        float targetOrientation = wanderOrientation + agent.orientation;
        
        // 计算漫游中心点（在Agent前方）
        Vector3 wanderCenter = agent.position + 
            GetOrientationAsVector(agent.orientation) * wanderOffset;
        
        // 计算目标位置（漫游中心点 + 半径方向）
        Vector3 targetPosition = wanderCenter + 
            GetOrientationAsVector(targetOrientation) * wanderRadius;
        
        // 使用Seek行为朝向目标位置
        Vector3 direction = targetPosition - agent.position;
        direction.Normalize();
        
        Vector3 desiredVelocity = direction * agent.maxSpeed;
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear = Vector3.ClampMagnitude(steering.linear, agent.maxAccel);
        
        return steering;
    }
    
    private Vector3 GetOrientationAsVector(float orientation)
    {
        return new Vector3(Mathf.Sin(orientation), 0, Mathf.Cos(orientation));
    }
}
```

#### 9. AvoidWall（避障）

**功能**：避开墙壁

```csharp
public class AvoidWall : SteeringBehavior
{
    public float avoidDistance = 3f;    // 避障距离
    public float lookAhead = 5f;         // 前瞻距离
    public LayerMask wallLayer;          // 墙壁层
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 计算前瞻位置（Agent前方）
        Vector3 rayVector = agent.velocity.normalized * lookAhead;
        Vector3 ahead = agent.position + rayVector;
        
        // 检测前方是否有墙壁
        RaycastHit hit;
        if (Physics.Raycast(agent.position, rayVector.normalized, out hit, lookAhead, wallLayer))
        {
            // 计算避障方向（垂直于墙壁法线）
            Vector3 avoidanceDirection = Vector3.Reflect(rayVector.normalized, hit.normal);
            avoidanceDirection.Normalize();
            
            // 计算目标位置（避开墙壁）
            Vector3 targetPosition = hit.point + avoidanceDirection * avoidDistance;
            
            // 使用Seek行为朝向目标位置
            Vector3 direction = targetPosition - agent.position;
            direction.Normalize();
            
            Vector3 desiredVelocity = direction * agent.maxSpeed;
            steering.linear = desiredVelocity - agent.velocity;
            steering.linear = Vector3.ClampMagnitude(steering.linear, agent.maxAccel);
        }
        
        return steering;
    }
}
```

#### 10. AvoidAgent（避开其他Agent）

**功能**：避开其他Agent

```csharp
public class AvoidAgent : SteeringBehavior
{
    public List<Agent> targets;         // 需要避开的Agent列表
    public float avoidRadius = 2f;      // 避障半径
    public float lookAhead = 3f;        // 前瞻距离
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 计算前瞻位置
        Vector3 rayVector = agent.velocity.normalized * lookAhead;
        Vector3 ahead = agent.position + rayVector;
        
        // 找到最近的威胁Agent
        Agent closestThreat = null;
        float closestDistance = float.MaxValue;
        
        foreach (Agent target in targets)
        {
            if (target == agent) continue;
            
            // 计算到目标的距离
            float distance = Vector3.Distance(ahead, target.position);
            
            // 如果在前瞻范围内且更近
            if (distance < avoidRadius && distance < closestDistance)
            {
                closestThreat = target;
                closestDistance = distance;
            }
        }
        
        // 如果有威胁，避开它
        if (closestThreat != null)
        {
            // 计算避障方向（远离威胁）
            Vector3 avoidanceDirection = (agent.position - closestThreat.position).normalized;
            
            // 计算目标位置
            Vector3 targetPosition = closestThreat.position + avoidanceDirection * avoidRadius;
            
            // 使用Seek行为朝向目标位置
            Vector3 direction = targetPosition - agent.position;
            direction.Normalize();
            
            Vector3 desiredVelocity = direction * agent.maxSpeed;
            steering.linear = desiredVelocity - agent.velocity;
            steering.linear = Vector3.ClampMagnitude(steering.linear, agent.maxAccel);
        }
        
        return steering;
    }
}
```

### 行为组合

**核心思想**：多个行为可以组合，通过加权求和得到最终转向力

```csharp
public class BlendedSteering : SteeringBehavior
{
    [System.Serializable]
    public class BehaviorWeight
    {
        public SteeringBehavior behavior;
        public float weight;
    }
    
    public List<BehaviorWeight> behaviors = new List<BehaviorWeight>();
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        // 累积所有行为的转向力（加权）
        foreach (var bw in behaviors)
        {
            if (bw.behavior != null)
            {
                Steering behaviorSteering = bw.behavior.GetSteering();
                steering.linear += behaviorSteering.linear * bw.weight;
                steering.angular += behaviorSteering.angular * bw.weight;
            }
        }
        
        // 限制最终转向力
        steering.linear = Vector3.ClampMagnitude(steering.linear, agent.maxAccel);
        if (Mathf.Abs(steering.angular) > agent.maxAngularAccel)
        {
            steering.angular = Mathf.Sign(steering.angular) * agent.maxAngularAccel;
        }
        
        return steering;
    }
}
```

### 使用示例

```csharp
public class AgentController : MonoBehaviour
{
    private Agent agent;
    private BlendedSteering blendedSteering;
    
    void Start()
    {
        agent = new Agent();
        agent.position = transform.position;
        agent.maxSpeed = 5f;
        agent.maxAccel = 10f;
        
        // 创建组合行为
        blendedSteering = new BlendedSteering();
        blendedSteering.agent = agent;
        
        // 添加多个行为
        Seek seek = new Seek();
        seek.agent = agent;
        seek.target = GameObject.Find("Target").transform;
        
        AvoidWall avoidWall = new AvoidWall();
        avoidWall.agent = agent;
        avoidWall.wallLayer = LayerMask.GetMask("Wall");
        
        // 设置权重
        blendedSteering.behaviors.Add(new BlendedSteering.BehaviorWeight 
        { 
            behavior = seek, 
            weight = 1.0f 
        });
        blendedSteering.behaviors.Add(new BlendedSteering.BehaviorWeight 
        { 
            behavior = avoidWall, 
            weight = 2.0f  // 避障优先级更高
        });
    }
    
    void Update()
    {
        // 获取组合后的转向力
        Steering steering = blendedSteering.GetSteering();
        
        // 更新Agent状态
        agent.velocity += steering.linear * Time.deltaTime;
        agent.velocity = Vector3.ClampMagnitude(agent.velocity, agent.maxSpeed);
        agent.position += agent.velocity * Time.deltaTime;
        
        // 更新Transform
        transform.position = agent.position;
        if (agent.velocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity);
        }
    }
}
```

### 行为基类

```csharp
public abstract class SteeringBehavior
{
    public Agent agent;
    
    public abstract Steering GetSteering();
}
```

### 总结

- **模块化**：每个行为独立实现，易于维护
- **可组合**：通过加权求和组合多个行为
- **灵活**：可以根据场景动态调整行为权重
- **扩展性**：可以轻松添加新的行为类型

