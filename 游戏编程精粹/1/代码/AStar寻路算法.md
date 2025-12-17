# A*寻路算法 - C#实现

## 核心概念

- **定义**：A*是一种启发式搜索算法，用于在图形和网格中找到最短路径
- **核心思想**：使用启发式函数估算到目标的距离，优先探索最有希望的节点
- **公式**：`f(n) = g(n) + h(n)`
  - `g(n)`：从起点到当前节点的实际代价
  - `h(n)`：从当前节点到目标的启发式估算代价
  - `f(n)`：节点的总代价

## 基础数据结构

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A*节点：表示搜索空间中的一个节点
/// </summary>
public class AStarNode
{
    public Vector2Int position;      // 节点位置（网格坐标）
    public AStarNode parent;         // 父节点（用于回溯路径）
    public float gCost;              // 从起点到当前节点的实际代价
    public float hCost;              // 从当前节点到目标的启发式估算代价
    public float fCost => gCost + hCost;  // 总代价
    
    public AStarNode(Vector2Int pos)
    {
        position = pos;
        parent = null;
        gCost = 0f;
        hCost = 0f;
    }
}
```

## 核心算法实现

```csharp
/// <summary>
/// A*寻路算法核心实现
/// </summary>
public class AStarPathfinding
{
    private GridMap gridMap;  // 网格地图
    
    /// <summary>
    /// A*寻路主函数
    /// </summary>
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        // 1. 初始化
        List<AStarNode> openList = new List<AStarNode>();  // 待探索节点
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();  // 已探索节点
        
        AStarNode startNode = new AStarNode(start);
        startNode.gCost = 0f;
        startNode.hCost = Heuristic(start, end);
        openList.Add(startNode);
        
        // 2. 主循环
        while (openList.Count > 0)
        {
            // 2.1 从开放列表中选择f值最小的节点
            AStarNode currentNode = GetLowestFCostNode(openList);
            openList.Remove(currentNode);
            closedSet.Add(currentNode.position);
            
            // 2.2 如果到达目标，回溯路径
            if (currentNode.position.Equals(end))
            {
                return RetracePath(startNode, currentNode);
            }
            
            // 2.3 探索邻居节点
            foreach (Vector2Int neighbor in GetNeighbors(currentNode.position))
            {
                // 跳过已探索或不可行走的节点
                if (closedSet.Contains(neighbor) || !gridMap.IsWalkable(neighbor))
                    continue;
                
                // 计算到邻居的新代价
                float newGCost = currentNode.gCost + GetDistance(currentNode.position, neighbor);
                
                // 检查是否在开放列表中
                AStarNode neighborNode = openList.Find(n => n.position.Equals(neighbor));
                
                if (neighborNode == null)
                {
                    // 新节点，添加到开放列表
                    neighborNode = new AStarNode(neighbor);
                    neighborNode.parent = currentNode;
                    neighborNode.gCost = newGCost;
                    neighborNode.hCost = Heuristic(neighbor, end);
                    openList.Add(neighborNode);
                }
                else if (newGCost < neighborNode.gCost)
                {
                    // 找到更优路径，更新节点
                    neighborNode.parent = currentNode;
                    neighborNode.gCost = newGCost;
                }
            }
        }
        
        // 3. 未找到路径
        return null;
    }
    
    /// <summary>
    /// 启发式函数：估算从A到B的距离（欧氏距离）
    /// </summary>
    private float Heuristic(Vector2Int a, Vector2Int b)
    {
        float dx = Mathf.Abs(a.x - b.x);
        float dy = Mathf.Abs(a.y - b.y);
        return Mathf.Sqrt(dx * dx + dy * dy);
    }
    
    /// <summary>
    /// 获取f值最小的节点
    /// </summary>
    private AStarNode GetLowestFCostNode(List<AStarNode> nodes)
    {
        AStarNode lowest = nodes[0];
        for (int i = 1; i < nodes.Count; i++)
        {
            if (nodes[i].fCost < lowest.fCost)
                lowest = nodes[i];
        }
        return lowest;
    }
    
    /// <summary>
    /// 回溯路径：从终点回溯到起点
    /// </summary>
    private List<Vector2Int> RetracePath(AStarNode startNode, AStarNode endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        AStarNode currentNode = endNode;
        
        while (currentNode != null)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        
        path.Reverse();  // 反转路径，从起点到终点
        return path;
    }
    
    /// <summary>
    /// 获取邻居节点（4方向或8方向）
    /// </summary>
    private List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        
        // 4方向移动
        neighbors.Add(new Vector2Int(pos.x + 1, pos.y));
        neighbors.Add(new Vector2Int(pos.x - 1, pos.y));
        neighbors.Add(new Vector2Int(pos.x, pos.y + 1));
        neighbors.Add(new Vector2Int(pos.x, pos.y - 1));
        
        // 可选：8方向移动（包含对角线）
        // neighbors.Add(new Vector2Int(pos.x + 1, pos.y + 1));
        // neighbors.Add(new Vector2Int(pos.x + 1, pos.y - 1));
        // neighbors.Add(new Vector2Int(pos.x - 1, pos.y + 1));
        // neighbors.Add(new Vector2Int(pos.x - 1, pos.y - 1));
        
        return neighbors;
    }
    
    /// <summary>
    /// 计算两个节点之间的距离
    /// </summary>
    private float GetDistance(Vector2Int a, Vector2Int b)
    {
        float dx = Mathf.Abs(a.x - b.x);
        float dy = Mathf.Abs(a.y - b.y);
        
        // 4方向移动：使用曼哈顿距离
        return dx + dy;
        
        // 8方向移动：使用对角线距离
        // if (dx > dy)
        //     return 14 * dy + 10 * (dx - dy);
        // return 14 * dx + 10 * (dy - dx);
    }
}
```

## 性能优化实现

### 使用优先队列优化Open表

```csharp
using System.Collections.Generic;

/// <summary>
/// 使用优先队列（堆）优化Open表
/// </summary>
public class AStarPathfindingOptimized
{
    // 使用.NET 6+的PriorityQueue，或自己实现最小堆
    private PriorityQueue<AStarNode, float> openQueue;
    
    // 或者使用List配合堆操作
    private List<AStarNode> openList;
    
    /// <summary>
    /// 使用堆优化的节点插入
    /// </summary>
    private void AddToOpenList(AStarNode node)
    {
        openList.Add(node);
        // 上浮操作，维护堆性质
        HeapifyUp(openList.Count - 1);
    }
    
    /// <summary>
    /// 提取f值最小的节点
    /// </summary>
    private AStarNode ExtractMin()
    {
        if (openList.Count == 0) return null;
        
        AStarNode min = openList[0];
        openList[0] = openList[openList.Count - 1];
        openList.RemoveAt(openList.Count - 1);
        
        if (openList.Count > 0)
            HeapifyDown(0);
        
        return min;
    }
    
    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (openList[index].fCost >= openList[parent].fCost)
                break;
            
            Swap(index, parent);
            index = parent;
        }
    }
    
    private void HeapifyDown(int index)
    {
        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int smallest = index;
            
            if (left < openList.Count && openList[left].fCost < openList[smallest].fCost)
                smallest = left;
            if (right < openList.Count && openList[right].fCost < openList[smallest].fCost)
                smallest = right;
            
            if (smallest == index) break;
            
            Swap(index, smallest);
            index = smallest;
        }
    }
    
    private void Swap(int a, int b)
    {
        AStarNode temp = openList[a];
        openList[a] = openList[b];
        openList[b] = temp;
    }
}
```

### 节点银行（预分配优化）

```csharp
/// <summary>
/// 节点银行：预分配节点，避免频繁GC
/// </summary>
public class NodeBank
{
    private Queue<AStarNode> nodePool;
    private int poolSize;
    
    public NodeBank(int size = 1000)
    {
        poolSize = size;
        nodePool = new Queue<AStarNode>();
        
        // 预分配节点
        for (int i = 0; i < poolSize; i++)
        {
            nodePool.Enqueue(new AStarNode(Vector2Int.zero));
        }
    }
    
    /// <summary>
    /// 从节点银行获取节点
    /// </summary>
    public AStarNode GetNode(Vector2Int position)
    {
        AStarNode node;
        
        if (nodePool.Count > 0)
        {
            node = nodePool.Dequeue();
        }
        else
        {
            // 池用尽，创建新节点
            node = new AStarNode(Vector2Int.zero);
        }
        
        // 重置节点状态
        node.position = position;
        node.parent = null;
        node.gCost = 0f;
        node.hCost = 0f;
        
        return node;
    }
    
    /// <summary>
    /// 归还节点到银行
    /// </summary>
    public void ReturnNode(AStarNode node)
    {
        if (nodePool.Count < poolSize)
        {
            nodePool.Enqueue(node);
        }
    }
}
```

## Unity应用

```csharp
/// <summary>
/// Unity中使用A*寻路
/// </summary>
public class AStarPathfindingUnity : MonoBehaviour
{
    public GridMap gridMap;
    public Transform startPoint;
    public Transform endPoint;
    
    private AStarPathfinding pathfinding;
    
    void Start()
    {
        pathfinding = new AStarPathfinding();
        pathfinding.gridMap = gridMap;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2Int start = WorldToGrid(startPoint.position);
            Vector2Int end = WorldToGrid(endPoint.position);
            
            List<Vector2Int> path = pathfinding.FindPath(start, end);
            
            if (path != null)
            {
                Debug.Log($"找到路径，共{path.Count}个节点");
                // 可视化路径
                VisualizePath(path);
            }
            else
            {
                Debug.Log("未找到路径");
            }
        }
    }
    
    private Vector2Int WorldToGrid(Vector3 worldPos)
    {
        // 将世界坐标转换为网格坐标
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x),
            Mathf.FloorToInt(worldPos.z)
        );
    }
}
```

## 路径平滑

A*算法找到的路径通常是锯齿状的，需要进行平滑处理。参考：
- `PathFollow.cs` - 基础路径跟随实现
- `PathFollow_Optimized.cs` - Catmull-Rom插值优化版本

**平滑方法**：
1. **Catmull-Rom插值**：预生成平滑路径点
2. **分级路径点**：动态调整路径点，总是寻找下一个关键点

