# 地形推理 - C#实现

## 核心概念

- **定义**：中继点（Waypoint）、战术分析等地形推理技术
- **核心思想**：分析地形的战术价值，为AI提供决策依据
- **应用场景**：3D游戏中的战术位置选择、伏击点识别、防御点评估

## 中继点系统

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 中继点（Waypoint）：路径上的关键点
/// </summary>
public class Waypoint
{
    public Vector3 position;        // 位置
    public float tacticalValue;     // 战术价值
    public WaypointType type;        // 类型（伏击点、防御点、观察点等）
    public List<Waypoint> connections;  // 连接的中继点
    
    public Waypoint(Vector3 pos)
    {
        position = pos;
        tacticalValue = 0f;
        type = WaypointType.Normal;
        connections = new List<Waypoint>();
    }
}

public enum WaypointType
{
    Normal,      // 普通点
    Ambush,      // 伏击点
    Defense,     // 防御点
    Observation, // 观察点
    Cover        // 掩体点
}
```

## 战术分析

```csharp
/// <summary>
/// 战术分析器：评估地形的战术价值
/// </summary>
public class TacticalAnalyzer
{
    /// <summary>
    /// 计算中继点的战术价值
    /// </summary>
    public float CalculateTacticalValue(Waypoint waypoint, List<Vector3> enemyPositions)
    {
        float value = 0f;
        
        // 1. 视野价值（能看到多少敌人）
        int visibleEnemies = CountVisibleEnemies(waypoint.position, enemyPositions);
        value += visibleEnemies * 10f;
        
        // 2. 掩体价值（有多少掩体保护）
        float coverValue = CalculateCoverValue(waypoint.position);
        value += coverValue * 5f;
        
        // 3. 高度优势（高度越高，价值越高）
        float heightAdvantage = CalculateHeightAdvantage(waypoint.position);
        value += heightAdvantage * 3f;
        
        // 4. 可达性（是否容易到达）
        float accessibility = CalculateAccessibility(waypoint.position);
        value += accessibility * 2f;
        
        return value;
    }
    
    /// <summary>
    /// 识别伏击点
    /// </summary>
    public bool IsAmbushPoint(Vector3 position, List<Vector3> enemyPositions)
    {
        // 伏击点特征：
        // 1. 有掩体保护
        // 2. 能看到敌人
        // 3. 敌人不容易发现
        // 4. 容易撤退
        
        bool hasCover = HasCover(position);
        bool canSeeEnemy = CanSeeAnyEnemy(position, enemyPositions);
        bool hiddenFromEnemy = IsHiddenFromEnemy(position, enemyPositions);
        bool easyRetreat = HasRetreatPath(position);
        
        return hasCover && canSeeEnemy && hiddenFromEnemy && easyRetreat;
    }
    
    /// <summary>
    /// 识别防御点
    /// </summary>
    public bool IsDefensePoint(Vector3 position)
    {
        // 防御点特征：
        // 1. 有良好的掩体
        // 2. 视野开阔
        // 3. 不易被包围
        // 4. 有撤退路径
        
        bool hasGoodCover = HasGoodCover(position);
        bool hasWideView = HasWideView(position);
        bool notSurrounded = !IsSurrounded(position);
        bool hasRetreatPath = HasRetreatPath(position);
        
        return hasGoodCover && hasWideView && notSurrounded && hasRetreatPath;
    }
}
```

## 从经验中学习

```csharp
/// <summary>
/// 经验学习系统：从战斗经验中学习地形的战术价值
/// </summary>
public class ExperienceLearning
{
    private Dictionary<Vector3, float> learnedValues;  // 学习到的战术价值
    
    public ExperienceLearning()
    {
        learnedValues = new Dictionary<Vector3, float>();
    }
    
    /// <summary>
    /// 记录战斗经验
    /// </summary>
    public void RecordExperience(Vector3 position, bool success, float outcome)
    {
        if (!learnedValues.ContainsKey(position))
        {
            learnedValues[position] = 0.5f;  // 初始值
        }
        
        // 根据战斗结果更新价值
        float learningRate = 0.1f;
        if (success)
        {
            learnedValues[position] += learningRate * outcome;
        }
        else
        {
            learnedValues[position] -= learningRate * (1f - outcome);
        }
        
        // 限制在[0, 1]范围内
        learnedValues[position] = Mathf.Clamp01(learnedValues[position]);
    }
    
    /// <summary>
    /// 获取学习到的战术价值
    /// </summary>
    public float GetLearnedValue(Vector3 position)
    {
        if (learnedValues.ContainsKey(position))
        {
            return learnedValues[position];
        }
        return 0.5f;  // 默认值
    }
}
```

## Unity应用

```csharp
/// <summary>
/// Unity中使用地形推理
/// </summary>
public class TerrainReasoningUnity : MonoBehaviour
{
    private TacticalAnalyzer analyzer;
    private ExperienceLearning learning;
    private List<Waypoint> waypoints;
    
    void Start()
    {
        analyzer = new TacticalAnalyzer();
        learning = new ExperienceLearning();
        waypoints = new List<Waypoint>();
        
        // 生成中继点网络
        GenerateWaypointNetwork();
    }
    
    /// <summary>
    /// 找到最佳战术位置
    /// </summary>
    public Vector3 FindBestTacticalPosition(List<Vector3> enemyPositions)
    {
        Waypoint bestWaypoint = null;
        float bestValue = float.MinValue;
        
        foreach (var waypoint in waypoints)
        {
            // 计算战术价值（结合分析和经验）
            float analyticalValue = analyzer.CalculateTacticalValue(waypoint, enemyPositions);
            float learnedValue = learning.GetLearnedValue(waypoint.position);
            float totalValue = analyticalValue * 0.7f + learnedValue * 100f * 0.3f;
            
            if (totalValue > bestValue)
            {
                bestValue = totalValue;
                bestWaypoint = waypoint;
            }
        }
        
        return bestWaypoint != null ? bestWaypoint.position : Vector3.zero;
    }
}
```

## 与Unity NavMesh的关系

- Unity NavMesh提供基础导航功能
- 地形推理在此基础上添加战术分析
- 可以结合使用：NavMesh用于路径查找，地形推理用于位置选择

