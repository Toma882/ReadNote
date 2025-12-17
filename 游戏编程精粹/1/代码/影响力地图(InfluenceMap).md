# 影响力地图（Influence Map）- C#实现

## 核心概念

- **定义**：影响力地图用于AI决策，表示地图上各区域的重要性
- **核心思想**：每个单位/建筑在地图上产生影响力，影响力会传播和衰减
- **应用场景**：RTS游戏中的战略决策、单位部署、资源点选择

## 基础数据结构

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 影响力地图单元格（使用结构体提高性能）
/// </summary>
public struct InfluenceCell
{
    public Vector2Int position;      // 单元格位置
    public float influence;          // 影响力值（正数=友方，负数=敌方）
    public float desirability;       // 合意值（综合评估）
    
    public InfluenceCell(Vector2Int pos)
    {
        position = pos;
        influence = 0f;
        desirability = 0f;
    }
}

/// <summary>
/// 影响力地图：管理整个地图的影响力分布
/// </summary>
public class InfluenceMap
{
    private InfluenceCell[,] cells;  // 地图单元格
    private int width;
    private int height;
    private float cellSize;          // 单元格大小
    
    public InfluenceMap(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        cells = new InfluenceCell[width, height];
        
        // 初始化所有单元格
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new InfluenceCell(new Vector2Int(x, y));
            }
        }
    }
    
    /// <summary>
    /// 获取单元格引用（用于修改结构体）
    /// </summary>
    public ref InfluenceCell GetCellRef(Vector2Int pos)
    {
        return ref cells[pos.x, pos.y];
    }
    
    /// <summary>
    /// 获取单元格（用于读取，返回值类型副本）
    /// </summary>
    public InfluenceCell GetCell(Vector2Int pos)
    {
        if (IsValidPosition(pos))
            return cells[pos.x, pos.y];
        return default(InfluenceCell);
    }
    
    /// <summary>
    /// 检查位置是否有效
    /// </summary>
    public bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }
}
```

## 影响力传播算法

```csharp
/// <summary>
/// 影响力传播：从源点向外传播影响力
/// </summary>
public class InfluencePropagation
{
    private InfluenceMap influenceMap;  // 需要访问影响力地图
    
    public InfluencePropagation(InfluenceMap map)
    {
        influenceMap = map;
    }
    
    /// <summary>
    /// 添加影响力源（单位、建筑等）
    /// </summary>
    public void AddInfluenceSource(Vector2Int position, float strength, bool isFriendly)
    {
        // 影响力值：正数=友方，负数=敌方
        float influenceValue = isFriendly ? strength : -strength;
        
        // 从源点向外传播
        PropagateInfluence(position, influenceValue, GetDecayFunction());
    }
    
    /// <summary>
    /// 传播影响力（使用衰减函数）
    /// </summary>
    private void PropagateInfluence(Vector2Int source, float strength, System.Func<float, float> decayFunc)
    {
        // 使用广度优先搜索传播影响力
        Queue<(Vector2Int pos, float influence, int distance)> queue = new Queue<(Vector2Int, float, int)>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        
        queue.Enqueue((source, strength, 0));
        visited.Add(source);
        
        while (queue.Count > 0)
        {
            var (pos, influence, distance) = queue.Dequeue();
            
            // 如果影响力太小，停止传播
            if (Mathf.Abs(influence) < 0.01f)
                continue;
            
            // 更新单元格影响力（累加）
            // 注意：结构体是值类型，需要直接修改数组元素，不能获取副本
            if (influenceMap.IsValidPosition(pos))
            {
                ref InfluenceCell cell = ref influenceMap.GetCellRef(pos);
                cell.influence += influence;
            }
            
            // 传播到邻居
            foreach (Vector2Int neighbor in GetNeighbors(pos))
            {
                if (visited.Contains(neighbor))
                    continue;
                
                // 计算衰减后的影响力
                float newInfluence = decayFunc(influence);
                int newDistance = distance + 1;
                
                queue.Enqueue((neighbor, newInfluence, newDistance));
                visited.Add(neighbor);
            }
        }
    }
    
    /// <summary>
    /// 衰减函数：线性衰减
    /// </summary>
    private System.Func<float, float> GetDecayFunction()
    {
        float decayRate = 0.8f;  // 每步衰减20%
        return (influence) => influence * decayRate;
    }
    
    /// <summary>
    /// 衰减函数：指数衰减
    /// </summary>
    private System.Func<float, float> GetExponentialDecay(float decayFactor)
    {
        return (influence) => influence * Mathf.Exp(-decayFactor);
    }
}
```

## 合意值计算

```csharp
/// <summary>
/// 合意值计算：综合评估单元格的吸引力
/// </summary>
public class DesirabilityCalculator
{
    private InfluenceMap influenceMap;  // 需要访问影响力地图
    
    public DesirabilityCalculator(InfluenceMap map)
    {
        influenceMap = map;
    }
    
    /// <summary>
    /// 计算合意值：综合考虑多个因素
    /// </summary>
    public float CalculateDesirability(InfluenceCell cell, Vector2Int target)
    {
        float desirability = 0f;
        
        // 1. 影响力因素（友方区域更合意）
        desirability += cell.influence * 0.5f;
        
        // 2. 距离因素（距离目标越近越合意）
        float distance = Vector2Int.Distance(cell.position, target);
        desirability += (1f / (1f + distance)) * 0.3f;
        
        // 3. 地形因素（可通行区域更合意）
        if (IsWalkable(cell.position))
            desirability += 0.2f;
        
        return desirability;
    }
    
    /// <summary>
    /// 找到最合意的单元格
    /// </summary>
    public Vector2Int FindBestCell(Vector2Int target, int searchRadius)
    {
        Vector2Int bestCell = target;
        float bestDesirability = float.MinValue;
        
        for (int x = -searchRadius; x <= searchRadius; x++)
        {
            for (int y = -searchRadius; y <= searchRadius; y++)
            {
                Vector2Int cellPos = target + new Vector2Int(x, y);
                
                // 结构体不能为null，需要检查位置是否有效
                if (influenceMap.IsValidPosition(cellPos))
                {
                    InfluenceCell cell = influenceMap.GetCell(cellPos);
                    float desirability = CalculateDesirability(cell, target);
                    if (desirability > bestDesirability)
                    {
                        bestDesirability = desirability;
                        bestCell = cellPos;
                    }
                }
            }
        }
        
        return bestCell;
    }
}
```

## Unity应用示例

```csharp
/// <summary>
/// Unity中使用影响力地图
/// </summary>
public class InfluenceMapUnity : MonoBehaviour
{
    public int mapWidth = 100;
    public int mapHeight = 100;
    public float cellSize = 1f;
    
    private InfluenceMap influenceMap;
    private InfluencePropagation propagation;
    
    void Start()
    {
        influenceMap = new InfluenceMap(mapWidth, mapHeight, cellSize);
        propagation = new InfluencePropagation(influenceMap);
    }
    
    /// <summary>
    /// 添加单位影响力
    /// </summary>
    public void AddUnitInfluence(Vector3 worldPos, float strength, bool isFriendly)
    {
        Vector2Int gridPos = WorldToGrid(worldPos);
        propagation.AddInfluenceSource(gridPos, strength, isFriendly);
    }
    
    /// <summary>
    /// 获取最佳部署位置
    /// </summary>
    public Vector3 GetBestDeploymentPosition(Vector3 targetPos)
    {
        Vector2Int targetGrid = WorldToGrid(targetPos);
        Vector2Int bestGrid = FindBestCell(targetGrid, 10);
        return GridToWorld(bestGrid);
    }
    
    private Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / cellSize),
            Mathf.FloorToInt(worldPos.z / cellSize)
        );
    }
    
    private Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x * cellSize,
            0f,
            gridPos.y * cellSize
        );
    }
}
```

## 性能优化

- **使用结构体**：`InfluenceCell` 使用结构体而非类，减少GC压力，提高内存访问效率
  - 结构体在数组中内联存储，更好的缓存局部性
  - 减少堆分配和垃圾回收
  - 适合小数据、频繁访问的场景
- **空间分割**：使用四叉树或网格优化影响力传播
- **增量更新**：只更新变化区域的影响力
- **LOD系统**：远距离使用低精度影响力地图

