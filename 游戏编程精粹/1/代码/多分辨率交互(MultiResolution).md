# 多分辨率交互（Multi-Resolution Interaction）- C#实现

## 核心概念

- **定义**：使用多分辨率栅格地图进行对象交互检测，解决不同大小对象的检测问题
- **核心思想**：为不同大小的对象使用不同分辨率的栅格地图，提高检测效率
- **应用场景**：大地图游戏、寻路系统、交互检测、AI决策
- **Unity应用**：Unity没有直接提供，需要自己实现

---

## 4.6 用于交互检测的多分辨率地图

### 核心原理

- **算法思想**：使用多个不同分辨率的栅格地图，小对象使用高分辨率地图，大对象使用低分辨率地图
- **特点**：根据对象大小选择合适的分辨率，避免不必要的检测
- **优点**：提高检测效率，减少计算量
- **应用**：大规模场景的交互检测、寻路系统优化

### 实现代码

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 多分辨率地图单元格
/// </summary>
public class MultiResolutionCell
{
    public Vector2Int position;
    public List<GameObject> objects;
    public float cellSize;
    
    public MultiResolutionCell(Vector2Int pos, float size)
    {
        position = pos;
        cellSize = size;
        objects = new List<GameObject>();
    }
}

/// <summary>
/// 多分辨率地图层级
/// </summary>
public class MultiResolutionLayer
{
    public float cellSize;                    // 单元格大小
    public int width;                         // 地图宽度（单元格数）
    public int height;                        // 地图高度（单元格数）
    public MultiResolutionCell[,] cells;      // 单元格数组
    
    public MultiResolutionLayer(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        cells = new MultiResolutionCell[width, height];
        
        // 初始化所有单元格
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new MultiResolutionCell(new Vector2Int(x, y), cellSize);
            }
        }
    }
    
    /// <summary>
    /// 世界坐标转换为单元格坐标
    /// </summary>
    public Vector2Int WorldToCell(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int y = Mathf.FloorToInt(worldPos.z / cellSize);
        return new Vector2Int(x, y);
    }
    
    /// <summary>
    /// 单元格坐标转换为世界坐标
    /// </summary>
    public Vector3 CellToWorld(Vector2Int cellPos)
    {
        float x = cellPos.x * cellSize + cellSize * 0.5f;
        float z = cellPos.y * cellSize + cellSize * 0.5f;
        return new Vector3(x, 0, z);
    }
    
    /// <summary>
    /// 添加对象到单元格
    /// </summary>
    public void AddObject(GameObject obj, Vector3 worldPos)
    {
        Vector2Int cellPos = WorldToCell(worldPos);
        if (IsValidCell(cellPos))
        {
            cells[cellPos.x, cellPos.y].objects.Add(obj);
        }
    }
    
    /// <summary>
    /// 移除对象
    /// </summary>
    public void RemoveObject(GameObject obj, Vector3 worldPos)
    {
        Vector2Int cellPos = WorldToCell(worldPos);
        if (IsValidCell(cellPos))
        {
            cells[cellPos.x, cellPos.y].objects.Remove(obj);
        }
    }
    
    /// <summary>
    /// 查询范围内的对象
    /// </summary>
    public List<GameObject> Query(Vector3 center, float radius)
    {
        List<GameObject> results = new List<GameObject>();
        
        // 计算查询范围对应的单元格范围
        Vector2Int minCell = WorldToCell(center - Vector3.one * radius);
        Vector2Int maxCell = WorldToCell(center + Vector3.one * radius);
        
        // 遍历范围内的单元格
        for (int x = minCell.x; x <= maxCell.x; x++)
        {
            for (int y = minCell.y; y <= maxCell.y; y++)
            {
                if (IsValidCell(new Vector2Int(x, y)))
                {
                    MultiResolutionCell cell = cells[x, y];
                    Vector3 cellWorldPos = CellToWorld(new Vector2Int(x, y));
                    
                    // 检查单元格是否在查询范围内
                    if (Vector3.Distance(center, cellWorldPos) <= radius + cellSize * 0.5f)
                    {
                        results.AddRange(cell.objects);
                    }
                }
            }
        }
        
        return results;
    }
    
    /// <summary>
    /// 检查单元格坐标是否有效
    /// </summary>
    private bool IsValidCell(Vector2Int cellPos)
    {
        return cellPos.x >= 0 && cellPos.x < width && cellPos.y >= 0 && cellPos.y < height;
    }
}

/// <summary>
/// 多分辨率地图系统
/// </summary>
public class MultiResolutionMap
{
    private List<MultiResolutionLayer> layers;
    private Bounds worldBounds;
    
    public MultiResolutionMap(Bounds worldBounds, int layerCount = 3, float baseCellSize = 1f)
    {
        this.worldBounds = worldBounds;
        layers = new List<MultiResolutionLayer>();
        
        // 创建多个分辨率的层级
        for (int i = 0; i < layerCount; i++)
        {
            float cellSize = baseCellSize * Mathf.Pow(2, i);  // 每层分辨率翻倍
            int width = Mathf.CeilToInt(worldBounds.size.x / cellSize);
            int height = Mathf.CeilToInt(worldBounds.size.z / cellSize);
            
            layers.Add(new MultiResolutionLayer(width, height, cellSize));
        }
    }
    
    /// <summary>
    /// 添加对象（根据对象大小选择合适的层级）
    /// </summary>
    public void AddObject(GameObject obj, Vector3 worldPos, float objectSize)
    {
        // 选择合适分辨率的层级（对象大小应该适合单元格大小）
        int layerIndex = SelectLayer(objectSize);
        layers[layerIndex].AddObject(obj, worldPos);
    }
    
    /// <summary>
    /// 移除对象
    /// </summary>
    public void RemoveObject(GameObject obj, Vector3 worldPos, float objectSize)
    {
        int layerIndex = SelectLayer(objectSize);
        layers[layerIndex].RemoveObject(obj, worldPos);
    }
    
    /// <summary>
    /// 查询对象（根据查询范围选择合适的层级）
    /// </summary>
    public List<GameObject> Query(Vector3 center, float radius)
    {
        List<GameObject> results = new List<GameObject>();
        
        // 选择合适分辨率的层级
        int layerIndex = SelectLayer(radius);
        results.AddRange(layers[layerIndex].Query(center, radius));
        
        return results;
    }
    
    /// <summary>
    /// 选择合适分辨率的层级
    /// </summary>
    private int SelectLayer(float size)
    {
        // 选择单元格大小与对象大小最接近的层级
        for (int i = 0; i < layers.Count - 1; i++)
        {
            if (size <= layers[i].cellSize * 2f)
            {
                return i;
            }
        }
        
        return layers.Count - 1;
    }
}
```

---

## 4.7 计算到区域内部的距离

### 核心原理

- **算法思想**：计算点到区域（多边形）内部的距离，用于判断点是否在区域内或距离区域多远
- **特点**：支持复杂多边形区域，计算精确
- **应用**：AI寻路、区域判断、范围检测

### 实现代码

```csharp
/// <summary>
/// 点到区域距离计算
/// </summary>
public static class RegionDistance
{
    /// <summary>
    /// 判断点是否在多边形内部
    /// </summary>
    public static bool IsPointInPolygon(Vector2 point, List<Vector2> polygon)
    {
        int intersections = 0;
        
        for (int i = 0; i < polygon.Count; i++)
        {
            Vector2 p1 = polygon[i];
            Vector2 p2 = polygon[(i + 1) % polygon.Count];
            
            // 射线法：从点向右发射射线，计算与多边形边的交点数
            if (((p1.y > point.y) != (p2.y > point.y)) &&
                (point.x < (p2.x - p1.x) * (point.y - p1.y) / (p2.y - p1.y) + p1.x))
            {
                intersections++;
            }
        }
        
        // 奇数个交点表示在内部
        return intersections % 2 == 1;
    }
    
    /// <summary>
    /// 计算点到多边形的最短距离
    /// </summary>
    public static float DistanceToPolygon(Vector2 point, List<Vector2> polygon)
    {
        // 如果点在多边形内部，距离为0
        if (IsPointInPolygon(point, polygon))
        {
            return 0f;
        }
        
        // 计算点到所有边的最短距离
        float minDistance = float.MaxValue;
        
        for (int i = 0; i < polygon.Count; i++)
        {
            Vector2 p1 = polygon[i];
            Vector2 p2 = polygon[(i + 1) % polygon.Count];
            
            float distance = DistanceToLineSegment(point, p1, p2);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        
        return minDistance;
    }
    
    /// <summary>
    /// 计算点到线段的距离
    /// </summary>
    private static float DistanceToLineSegment(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        Vector2 line = lineEnd - lineStart;
        float lineLength = line.magnitude;
        
        if (lineLength < 0.0001f)
        {
            return Vector2.Distance(point, lineStart);
        }
        
        Vector2 toPoint = point - lineStart;
        float t = Mathf.Clamp01(Vector2.Dot(toPoint, line) / (lineLength * lineLength));
        
        Vector2 closestPoint = lineStart + line * t;
        return Vector2.Distance(point, closestPoint);
    }
    
    /// <summary>
    /// 计算点到区域内部的距离（考虑区域边界）
    /// </summary>
    public static float DistanceToRegion(Vector2 point, List<Vector2> region)
    {
        // 如果点在区域内，返回负距离（表示在内部）
        if (IsPointInPolygon(point, region))
        {
            // 计算到边界的距离（负值表示在内部）
            float minDistanceToEdge = float.MaxValue;
            
            for (int i = 0; i < region.Count; i++)
            {
                Vector2 p1 = region[i];
                Vector2 p2 = region[(i + 1) % region.Count];
                
                float distance = DistanceToLineSegment(point, p1, p2);
                if (distance < minDistanceToEdge)
                {
                    minDistanceToEdge = distance;
                }
            }
            
            return -minDistanceToEdge;  // 负值表示在内部
        }
        else
        {
            // 在外部，返回正距离
            return DistanceToPolygon(point, region);
        }
    }
}

/// <summary>
/// 区域管理器
/// </summary>
public class RegionManager : MonoBehaviour
{
    public List<Vector2> regionPoints = new List<Vector2>();
    
    /// <summary>
    /// 判断点是否在区域内
    /// </summary>
    public bool IsPointInRegion(Vector3 worldPos)
    {
        Vector2 point = new Vector2(worldPos.x, worldPos.z);
        return RegionDistance.IsPointInPolygon(point, regionPoints);
    }
    
    /// <summary>
    /// 计算点到区域的距离
    /// </summary>
    public float GetDistanceToRegion(Vector3 worldPos)
    {
        Vector2 point = new Vector2(worldPos.x, worldPos.z);
        return RegionDistance.DistanceToRegion(point, regionPoints);
    }
}
```

---

## Unity应用建议

1. **多分辨率地图**：
   - 适合大规模场景的交互检测
   - 根据对象大小选择合适的分辨率
   - 配合空间数据结构使用

2. **区域距离计算**：
   - 用于AI寻路和区域判断
   - 可以用于生成区域影响力地图
   - 配合NavMesh使用

3. **性能优化**：
   - 使用对象池减少GC
   - 分帧处理，避免单帧计算过多
   - 缓存计算结果

---

## 参考文献

- 《游戏编程精粹1》- 4.6 用于交互检测的多分辨率地图
- 《游戏编程精粹1》- 4.7 计算到区域内部的距离

