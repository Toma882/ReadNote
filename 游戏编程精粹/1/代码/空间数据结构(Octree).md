# 空间数据结构（Octree/Quadtree）- C#实现

## 核心概念

- **定义**：空间分割数据结构，用于快速空间查询和碰撞检测
- **核心思想**：递归地将空间分割成更小的区域，只查询相关区域，大幅减少计算量
- **应用场景**：大规模场景优化、碰撞检测、空间查询、视锥体剔除
- **Unity应用**：Unity Physics内部已使用类似优化，但大规模场景可能需要自定义实现

---

## 4.10 八叉树（Octree）

### 核心原理

- **算法思想**：3D空间的递归分割，每个节点分割成8个子节点（2×2×2）
- **四叉树（Quadtree）**：2D版本，每个节点分割成4个子节点（2×2）
- **特点**：快速空间查询，O(log n)复杂度
- **应用**：碰撞检测、视锥体剔除、空间查询、大规模场景优化

### 实现代码

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 八叉树节点
/// </summary>
public class OctreeNode
{
    public Bounds bounds;              // 节点边界
    public List<GameObject> objects;   // 节点内的对象
    public OctreeNode[] children;     // 8个子节点
    public int maxObjects;            // 节点最大对象数
    public int maxDepth;              // 最大深度
    public int depth;                 // 当前深度
    
    public OctreeNode(Bounds bounds, int maxObjects, int maxDepth, int depth)
    {
        this.bounds = bounds;
        this.maxObjects = maxObjects;
        this.maxDepth = maxDepth;
        this.depth = depth;
        this.objects = new List<GameObject>();
        this.children = null;
    }
    
    /// <summary>
    /// 插入对象
    /// </summary>
    public void Insert(GameObject obj)
    {
        // 如果对象不在节点范围内，不插入
        if (!bounds.Contains(obj.transform.position))
            return;
        
        // 如果节点未分割且有空间，直接添加
        if (children == null && objects.Count < maxObjects)
        {
            objects.Add(obj);
            return;
        }
        
        // 如果达到最大深度，直接添加
        if (depth >= maxDepth)
        {
            objects.Add(obj);
            return;
        }
        
        // 如果节点未分割，需要分割
        if (children == null)
        {
            Subdivide();
        }
        
        // 尝试插入到子节点
        bool inserted = false;
        for (int i = 0; i < 8; i++)
        {
            if (children[i].bounds.Contains(obj.transform.position))
            {
                children[i].Insert(obj);
                inserted = true;
                break;
            }
        }
        
        // 如果无法插入到子节点，保留在当前节点
        if (!inserted)
        {
            objects.Add(obj);
        }
    }
    
    /// <summary>
    /// 分割节点成8个子节点
    /// </summary>
    private void Subdivide()
    {
        children = new OctreeNode[8];
        Vector3 center = bounds.center;
        Vector3 size = bounds.size * 0.5f;
        Vector3 halfSize = size * 0.5f;
        
        // 创建8个子节点（2×2×2）
        children[0] = new OctreeNode(new Bounds(center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z), size), maxObjects, maxDepth, depth + 1);
        children[1] = new OctreeNode(new Bounds(center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z), size), maxObjects, maxDepth, depth + 1);
        children[2] = new OctreeNode(new Bounds(center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z), size), maxObjects, maxDepth, depth + 1);
        children[3] = new OctreeNode(new Bounds(center + new Vector3(halfSize.x, halfSize.y, -halfSize.z), size), maxObjects, maxDepth, depth + 1);
        children[4] = new OctreeNode(new Bounds(center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z), size), maxObjects, maxDepth, depth + 1);
        children[5] = new OctreeNode(new Bounds(center + new Vector3(halfSize.x, -halfSize.y, halfSize.z), size), maxObjects, maxDepth, depth + 1);
        children[6] = new OctreeNode(new Bounds(center + new Vector3(-halfSize.x, halfSize.y, halfSize.z), size), maxObjects, maxDepth, depth + 1);
        children[7] = new OctreeNode(new Bounds(center + new Vector3(halfSize.x, halfSize.y, halfSize.z), size), maxObjects, maxDepth, depth + 1);
        
        // 将当前节点的对象重新分配到子节点
        List<GameObject> objectsToRedistribute = new List<GameObject>(objects);
        objects.Clear();
        
        foreach (GameObject obj in objectsToRedistribute)
        {
            bool redistributed = false;
            for (int i = 0; i < 8; i++)
            {
                if (children[i].bounds.Contains(obj.transform.position))
                {
                    children[i].Insert(obj);
                    redistributed = true;
                    break;
                }
            }
            
            if (!redistributed)
            {
                objects.Add(obj);
            }
        }
    }
    
    /// <summary>
    /// 查询范围内的对象
    /// </summary>
    public void Query(Bounds queryBounds, List<GameObject> results)
    {
        // 如果查询范围与节点不相交，返回
        if (!bounds.Intersects(queryBounds))
            return;
        
        // 添加当前节点的对象
        foreach (GameObject obj in objects)
        {
            if (queryBounds.Contains(obj.transform.position))
            {
                results.Add(obj);
            }
        }
        
        // 查询子节点
        if (children != null)
        {
            for (int i = 0; i < 8; i++)
            {
                children[i].Query(queryBounds, results);
            }
        }
    }
    
    /// <summary>
    /// 移除对象
    /// </summary>
    public bool Remove(GameObject obj)
    {
        if (objects.Remove(obj))
        {
            return true;
        }
        
        if (children != null)
        {
            for (int i = 0; i < 8; i++)
            {
                if (children[i].Remove(obj))
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}

/// <summary>
/// 八叉树管理器
/// </summary>
public class Octree
{
    private OctreeNode root;
    
    public Octree(Bounds bounds, int maxObjects = 10, int maxDepth = 5)
    {
        root = new OctreeNode(bounds, maxObjects, maxDepth, 0);
    }
    
    public void Insert(GameObject obj)
    {
        root.Insert(obj);
    }
    
    public List<GameObject> Query(Bounds bounds)
    {
        List<GameObject> results = new List<GameObject>();
        root.Query(bounds, results);
        return results;
    }
    
    public bool Remove(GameObject obj)
    {
        return root.Remove(obj);
    }
    
    public void Clear()
    {
        // 重新创建根节点
        Bounds bounds = root.bounds;
        int maxObjects = root.maxObjects;
        int maxDepth = root.maxDepth;
        root = new OctreeNode(bounds, maxObjects, maxDepth, 0);
    }
}
```

---

## 4.11 松散八叉树（Loose Octree）

### 核心原理

- **算法思想**：使用松散边界（扩大节点边界范围），减少对象在边界处的频繁节点切换
- **松散四叉树（Loose Quadtree）**：2D版本，使用松散边界优化2D空间查询
- **特点**：减少边界问题，对象移动时减少节点切换，提高性能
- **应用**：动态对象管理、实时空间查询优化

### 实现代码

```csharp
/// <summary>
/// 松散八叉树节点
/// </summary>
public class LooseOctreeNode
{
    public Bounds tightBounds;        // 紧密边界（实际节点大小）
    public Bounds looseBounds;        // 松散边界（扩大后的边界）
    public List<GameObject> objects;
    public LooseOctreeNode[] children;
    public int maxObjects;
    public int maxDepth;
    public int depth;
    public float looseness;           // 松散系数（通常为2.0）
    
    public LooseOctreeNode(Bounds tightBounds, float looseness, int maxObjects, int maxDepth, int depth)
    {
        this.tightBounds = tightBounds;
        this.looseness = looseness;
        this.looseBounds = new Bounds(tightBounds.center, tightBounds.size * looseness);
        this.maxObjects = maxObjects;
        this.maxDepth = maxDepth;
        this.depth = depth;
        this.objects = new List<GameObject>();
        this.children = null;
    }
    
    /// <summary>
    /// 插入对象（使用松散边界判断）
    /// </summary>
    public void Insert(GameObject obj)
    {
        // 使用松散边界判断
        if (!looseBounds.Contains(obj.transform.position))
            return;
        
        // 如果节点未分割且有空间，直接添加
        if (children == null && objects.Count < maxObjects)
        {
            objects.Add(obj);
            return;
        }
        
        // 如果达到最大深度，直接添加
        if (depth >= maxDepth)
        {
            objects.Add(obj);
            return;
        }
        
        // 如果节点未分割，需要分割
        if (children == null)
        {
            Subdivide();
        }
        
        // 尝试插入到子节点（使用松散边界）
        bool inserted = false;
        for (int i = 0; i < 8; i++)
        {
            if (children[i].looseBounds.Contains(obj.transform.position))
            {
                children[i].Insert(obj);
                inserted = true;
                break;
            }
        }
        
        // 如果无法插入到子节点，保留在当前节点
        if (!inserted)
        {
            objects.Add(obj);
        }
    }
    
    /// <summary>
    /// 分割节点（使用紧密边界）
    /// </summary>
    private void Subdivide()
    {
        children = new LooseOctreeNode[8];
        Vector3 center = tightBounds.center;
        Vector3 size = tightBounds.size * 0.5f;
        Vector3 halfSize = size * 0.5f;
        
        // 创建8个子节点（使用紧密边界）
        children[0] = new LooseOctreeNode(new Bounds(center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z), size), looseness, maxObjects, maxDepth, depth + 1);
        children[1] = new LooseOctreeNode(new Bounds(center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z), size), looseness, maxObjects, maxDepth, depth + 1);
        children[2] = new LooseOctreeNode(new Bounds(center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z), size), looseness, maxObjects, maxDepth, depth + 1);
        children[3] = new LooseOctreeNode(new Bounds(center + new Vector3(halfSize.x, halfSize.y, -halfSize.z), size), looseness, maxObjects, maxDepth, depth + 1);
        children[4] = new LooseOctreeNode(new Bounds(center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z), size), looseness, maxObjects, maxDepth, depth + 1);
        children[5] = new LooseOctreeNode(new Bounds(center + new Vector3(halfSize.x, -halfSize.y, halfSize.z), size), looseness, maxObjects, maxDepth, depth + 1);
        children[6] = new LooseOctreeNode(new Bounds(center + new Vector3(-halfSize.x, halfSize.y, halfSize.z), size), looseness, maxObjects, maxDepth, depth + 1);
        children[7] = new LooseOctreeNode(new Bounds(center + new Vector3(halfSize.x, halfSize.y, halfSize.z), size), looseness, maxObjects, maxDepth, depth + 1);
        
        // 重新分配对象
        List<GameObject> objectsToRedistribute = new List<GameObject>(objects);
        objects.Clear();
        
        foreach (GameObject obj in objectsToRedistribute)
        {
            bool redistributed = false;
            for (int i = 0; i < 8; i++)
            {
                if (children[i].looseBounds.Contains(obj.transform.position))
                {
                    children[i].Insert(obj);
                    redistributed = true;
                    break;
                }
            }
            
            if (!redistributed)
            {
                objects.Add(obj);
            }
        }
    }
    
    /// <summary>
    /// 查询范围内的对象（使用紧密边界）
    /// </summary>
    public void Query(Bounds queryBounds, List<GameObject> results)
    {
        // 使用紧密边界判断相交
        if (!tightBounds.Intersects(queryBounds))
            return;
        
        // 添加当前节点的对象
        foreach (GameObject obj in objects)
        {
            if (queryBounds.Contains(obj.transform.position))
            {
                results.Add(obj);
            }
        }
        
        // 查询子节点
        if (children != null)
        {
            for (int i = 0; i < 8; i++)
            {
                children[i].Query(queryBounds, results);
            }
        }
    }
    
    public bool Remove(GameObject obj)
    {
        if (objects.Remove(obj))
        {
            return true;
        }
        
        if (children != null)
        {
            for (int i = 0; i < 8; i++)
            {
                if (children[i].Remove(obj))
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}

/// <summary>
/// 松散八叉树管理器
/// </summary>
public class LooseOctree
{
    private LooseOctreeNode root;
    private float looseness;
    
    public LooseOctree(Bounds bounds, float looseness = 2.0f, int maxObjects = 10, int maxDepth = 5)
    {
        this.looseness = looseness;
        root = new LooseOctreeNode(bounds, looseness, maxObjects, maxDepth, 0);
    }
    
    public void Insert(GameObject obj)
    {
        root.Insert(obj);
    }
    
    public List<GameObject> Query(Bounds bounds)
    {
        List<GameObject> results = new List<GameObject>();
        root.Query(bounds, results);
        return results;
    }
    
    public bool Remove(GameObject obj)
    {
        return root.Remove(obj);
    }
    
    public void Clear()
    {
        Bounds bounds = root.tightBounds;
        int maxObjects = root.maxObjects;
        int maxDepth = root.maxDepth;
        root = new LooseOctreeNode(bounds, looseness, maxObjects, maxDepth, 0);
    }
}
```

---

## 四叉树（Quadtree）实现

```csharp
/// <summary>
/// 四叉树节点（2D版本）
/// </summary>
public class QuadtreeNode
{
    public Rect bounds;
    public List<GameObject> objects;
    public QuadtreeNode[] children;  // 4个子节点
    public int maxObjects;
    public int maxDepth;
    public int depth;
    
    public QuadtreeNode(Rect bounds, int maxObjects, int maxDepth, int depth)
    {
        this.bounds = bounds;
        this.maxObjects = maxObjects;
        this.maxDepth = maxDepth;
        this.depth = depth;
        this.objects = new List<GameObject>();
        this.children = null;
    }
    
    public void Insert(GameObject obj)
    {
        Vector2 pos = new Vector2(obj.transform.position.x, obj.transform.position.z);
        if (!bounds.Contains(pos))
            return;
        
        if (children == null && objects.Count < maxObjects)
        {
            objects.Add(obj);
            return;
        }
        
        if (depth >= maxDepth)
        {
            objects.Add(obj);
            return;
        }
        
        if (children == null)
        {
            Subdivide();
        }
        
        bool inserted = false;
        for (int i = 0; i < 4; i++)
        {
            if (children[i].bounds.Contains(pos))
            {
                children[i].Insert(obj);
                inserted = true;
                break;
            }
        }
        
        if (!inserted)
        {
            objects.Add(obj);
        }
    }
    
    private void Subdivide()
    {
        children = new QuadtreeNode[4];
        float halfWidth = bounds.width * 0.5f;
        float halfHeight = bounds.height * 0.5f;
        float x = bounds.x;
        float y = bounds.y;
        
        children[0] = new QuadtreeNode(new Rect(x, y, halfWidth, halfHeight), maxObjects, maxDepth, depth + 1);
        children[1] = new QuadtreeNode(new Rect(x + halfWidth, y, halfWidth, halfHeight), maxObjects, maxDepth, depth + 1);
        children[2] = new QuadtreeNode(new Rect(x, y + halfHeight, halfWidth, halfHeight), maxObjects, maxDepth, depth + 1);
        children[3] = new QuadtreeNode(new Rect(x + halfWidth, y + halfHeight, halfWidth, halfHeight), maxObjects, maxDepth, depth + 1);
        
        List<GameObject> objectsToRedistribute = new List<GameObject>(objects);
        objects.Clear();
        
        foreach (GameObject obj in objectsToRedistribute)
        {
            Vector2 pos = new Vector2(obj.transform.position.x, obj.transform.position.z);
            bool redistributed = false;
            for (int i = 0; i < 4; i++)
            {
                if (children[i].bounds.Contains(pos))
                {
                    children[i].Insert(obj);
                    redistributed = true;
                    break;
                }
            }
            
            if (!redistributed)
            {
                objects.Add(obj);
            }
        }
    }
    
    public void Query(Rect queryBounds, List<GameObject> results)
    {
        if (!bounds.Intersects(queryBounds))
            return;
        
        foreach (GameObject obj in objects)
        {
            Vector2 pos = new Vector2(obj.transform.position.x, obj.transform.position.z);
            if (queryBounds.Contains(pos))
            {
                results.Add(obj);
            }
        }
        
        if (children != null)
        {
            for (int i = 0; i < 4; i++)
            {
                children[i].Query(queryBounds, results);
            }
        }
    }
}

/// <summary>
/// 四叉树管理器
/// </summary>
public class Quadtree
{
    private QuadtreeNode root;
    
    public Quadtree(Rect bounds, int maxObjects = 10, int maxDepth = 5)
    {
        root = new QuadtreeNode(bounds, maxObjects, maxDepth, 0);
    }
    
    public void Insert(GameObject obj)
    {
        root.Insert(obj);
    }
    
    public List<GameObject> Query(Rect bounds)
    {
        List<GameObject> results = new List<GameObject>();
        root.Query(bounds, results);
        return results;
    }
}
```

---

## Unity应用建议

1. **使用场景**：
   - 大规模场景优化（数千个对象）
   - 实时碰撞检测优化
   - 空间查询优化
   - 视锥体剔除优化

2. **性能优化**：
   - 合理设置`maxObjects`和`maxDepth`
   - 动态对象需要定期重建或使用松散八叉树
   - 使用对象池减少GC

3. **与Unity集成**：
   - 配合`Physics.OverlapSphere()`使用
   - 可以替代Unity Physics的空间查询
   - 用于自定义碰撞检测系统

---

## 参考文献

- 《游戏编程精粹1》- 4.10 八叉树构造
- 《游戏编程精粹1》- 4.11 松散的八叉树

