# 可视点寻径（Points-of-Visibility Pathfinding）- C#实现

## 核心概念

- **定义**：可视点寻径优化技术，用于优化路径查找
- **核心思想**：使用可视点（可见的角点）简化路径，减少路径点数量
- **应用场景**：复杂环境中的路径优化，减少路径点数量

## 基础实现

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 可视点：路径上的关键可见点
/// </summary>
public class VisibilityPoint
{
    public Vector3 position;        // 位置
    public List<VisibilityPoint> visiblePoints;  // 可见的其他点
    
    public VisibilityPoint(Vector3 pos)
    {
        position = pos;
        visiblePoints = new List<VisibilityPoint>();
    }
    
    /// <summary>
    /// 检查是否能看到另一个点
    /// </summary>
    public bool CanSee(VisibilityPoint other, LayerMask obstacleLayer)
    {
        Vector3 direction = other.position - position;
        float distance = direction.magnitude;
        
        // 射线检测，检查是否有障碍物
        RaycastHit hit;
        if (Physics.Raycast(position, direction.normalized, out hit, distance, obstacleLayer))
        {
            return false;  // 有障碍物，不可见
        }
        
        return true;  // 可见
    }
}
```

## 可视点寻径算法

```csharp
/// <summary>
/// 可视点寻径：使用可视点优化路径
/// </summary>
public class PointsOfVisibilityPathfinding
{
    private List<VisibilityPoint> visibilityPoints;
    private Dictionary<VisibilityPoint, Dictionary<VisibilityPoint, float>> shortestPaths;
    
    /// <summary>
    /// 构建可视点网络
    /// </summary>
    public void BuildVisibilityGraph(List<Vector3> cornerPoints, LayerMask obstacleLayer)
    {
        visibilityPoints = new List<VisibilityPoint>();
        
        // 1. 创建可视点
        foreach (var corner in cornerPoints)
        {
            visibilityPoints.Add(new VisibilityPoint(corner));
        }
        
        // 2. 建立可见性连接
        for (int i = 0; i < visibilityPoints.Count; i++)
        {
            for (int j = i + 1; j < visibilityPoints.Count; j++)
            {
                if (visibilityPoints[i].CanSee(visibilityPoints[j], obstacleLayer))
                {
                    visibilityPoints[i].visiblePoints.Add(visibilityPoints[j]);
                    visibilityPoints[j].visiblePoints.Add(visibilityPoints[i]);
                }
            }
        }
        
        // 3. 预计算最短路径
        PrecomputeShortestPaths();
    }
    
    /// <summary>
    /// 预计算所有点对之间的最短路径
    /// </summary>
    private void PrecomputeShortestPaths()
    {
        shortestPaths = new Dictionary<VisibilityPoint, Dictionary<VisibilityPoint, float>>();
        
        // 使用Floyd-Warshall算法或Dijkstra算法
        foreach (var start in visibilityPoints)
        {
            shortestPaths[start] = new Dictionary<VisibilityPoint, float>();
            Dijkstra(start, shortestPaths[start]);
        }
    }
    
    /// <summary>
    /// Dijkstra算法：计算从起点到所有点的最短路径
    /// </summary>
    private void Dijkstra(VisibilityPoint start, Dictionary<VisibilityPoint, float> distances)
    {
        Dictionary<VisibilityPoint, float> dist = new Dictionary<VisibilityPoint, float>();
        HashSet<VisibilityPoint> visited = new HashSet<VisibilityPoint>();
        
        foreach (var point in visibilityPoints)
        {
            dist[point] = float.MaxValue;
        }
        dist[start] = 0f;
        
        while (visited.Count < visibilityPoints.Count)
        {
            // 找到未访问的距离最小的点
            VisibilityPoint current = null;
            float minDist = float.MaxValue;
            
            foreach (var point in visibilityPoints)
            {
                if (!visited.Contains(point) && dist[point] < minDist)
                {
                    minDist = dist[point];
                    current = point;
                }
            }
            
            if (current == null) break;
            
            visited.Add(current);
            distances[current] = dist[current];
            
            // 更新邻居的距离
            foreach (var neighbor in current.visiblePoints)
            {
                if (!visited.Contains(neighbor))
                {
                    float newDist = dist[current] + Vector3.Distance(current.position, neighbor.position);
                    if (newDist < dist[neighbor])
                    {
                        dist[neighbor] = newDist;
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 查找路径：使用可视点网络
    /// </summary>
    public List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        // 1. 找到最近的可视点
        VisibilityPoint startPoint = FindNearestVisibilityPoint(start);
        VisibilityPoint endPoint = FindNearestVisibilityPoint(end);
        
        if (startPoint == null || endPoint == null)
            return null;
        
        // 2. 使用预计算的最短路径
        if (shortestPaths.ContainsKey(startPoint) && 
            shortestPaths[startPoint].ContainsKey(endPoint))
        {
            // 3. 回溯路径
            return ReconstructPath(startPoint, endPoint);
        }
        
        return null;
    }
    
    private VisibilityPoint FindNearestVisibilityPoint(Vector3 position)
    {
        VisibilityPoint nearest = null;
        float minDistance = float.MaxValue;
        
        foreach (var point in visibilityPoints)
        {
            float distance = Vector3.Distance(position, point.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = point;
            }
        }
        
        return nearest;
    }
}
```

## 轮廓区优化

```csharp
/// <summary>
/// 轮廓区：用于空间分区系统
/// </summary>
public class ContourRegion
{
    public List<Vector3> boundaryPoints;  // 边界点
    public List<VisibilityPoint> interiorPoints;  // 内部可视点
    
    public ContourRegion()
    {
        boundaryPoints = new List<Vector3>();
        interiorPoints = new List<VisibilityPoint>();
    }
    
    /// <summary>
    /// 检查点是否在轮廓区内
    /// </summary>
    public bool Contains(Vector3 point)
    {
        // 使用点在多边形内的算法
        return IsPointInPolygon(point, boundaryPoints);
    }
}
```

## Unity应用

```csharp
/// <summary>
/// Unity中使用可视点寻径
/// </summary>
public class PointsOfVisibilityUnity : MonoBehaviour
{
    private PointsOfVisibilityPathfinding pathfinding;
    
    void Start()
    {
        pathfinding = new PointsOfVisibilityPathfinding();
        
        // 从场景中提取角点（障碍物的角）
        List<Vector3> cornerPoints = ExtractCornerPoints();
        
        // 构建可视点网络
        pathfinding.BuildVisibilityGraph(cornerPoints, LayerMask.GetMask("Obstacle"));
    }
    
    /// <summary>
    /// 查找路径
    /// </summary>
    public List<Vector3> FindPathToTarget(Vector3 start, Vector3 end)
    {
        return pathfinding.FindPath(start, end);
    }
}
```

## 与A*的关系

- A*是基础寻径算法
- 可视点寻径是A*的优化技术
- 可以结合使用：先用A*找到基础路径，再用可视点优化

