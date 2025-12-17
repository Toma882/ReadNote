# 3D碰撞检测（Collision Detection）- C#实现

## 核心概念

- **定义**：检测两个或多个物体是否相交或碰撞
- **核心思想**：使用数学方法快速判断几何体之间的相交关系
- **应用场景**：物理模拟、游戏逻辑、AI寻路
- **Unity应用**：Unity有完整的物理引擎（PhysX），但了解基础算法有助于优化和自定义碰撞检测

---

## 4.5 3D碰撞检测

### 核心原理

- **包围球碰撞检测**：使用球体包围物体，快速判断是否相交
- **AABB碰撞检测**：使用轴对齐包围盒，计算简单快速
- **OBB碰撞检测**：使用有向包围盒，更精确但计算复杂
- **三角形对三角形碰撞检测**：最精确的碰撞检测，但计算复杂

### 实现代码

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 碰撞检测工具类
/// </summary>
public static class CollisionDetection
{
    /// <summary>
    /// 包围球碰撞检测
    /// </summary>
    public static bool SphereSphere(Vector3 center1, float radius1, Vector3 center2, float radius2)
    {
        float distance = Vector3.Distance(center1, center2);
        float sumRadius = radius1 + radius2;
        return distance <= sumRadius;
    }
    
    /// <summary>
    /// AABB碰撞检测（轴对齐包围盒）
    /// </summary>
    public static bool AABBAABB(Bounds bounds1, Bounds bounds2)
    {
        return bounds1.Intersects(bounds2);
    }
    
    /// <summary>
    /// 包围球与AABB碰撞检测
    /// </summary>
    public static bool SphereAABB(Vector3 sphereCenter, float sphereRadius, Bounds aabb)
    {
        // 找到AABB上距离球心最近的点
        Vector3 closestPoint = aabb.ClosestPoint(sphereCenter);
        
        // 计算球心到最近点的距离
        float distance = Vector3.Distance(sphereCenter, closestPoint);
        
        return distance <= sphereRadius;
    }
    
    /// <summary>
    /// 射线与包围球碰撞检测
    /// </summary>
    public static bool RaySphere(Ray ray, Vector3 sphereCenter, float sphereRadius, out float distance)
    {
        Vector3 toSphere = sphereCenter - ray.origin;
        float projectionLength = Vector3.Dot(toSphere, ray.direction);
        
        // 如果投影在射线后方，不相交
        if (projectionLength < 0)
        {
            distance = 0;
            return false;
        }
        
        // 计算最近点
        Vector3 closestPoint = ray.origin + ray.direction * projectionLength;
        float distanceToCenter = Vector3.Distance(closestPoint, sphereCenter);
        
        if (distanceToCenter <= sphereRadius)
        {
            // 计算交点距离
            float halfChord = Mathf.Sqrt(sphereRadius * sphereRadius - distanceToCenter * distanceToCenter);
            distance = projectionLength - halfChord;
            return true;
        }
        
        distance = 0;
        return false;
    }
    
    /// <summary>
    /// 射线与AABB碰撞检测
    /// </summary>
    public static bool RayAABB(Ray ray, Bounds bounds, out float distance)
    {
        // 使用Unity内置方法
        return bounds.IntersectRay(ray, out distance);
    }
    
    /// <summary>
    /// 点与包围球碰撞检测
    /// </summary>
    public static bool PointSphere(Vector3 point, Vector3 sphereCenter, float sphereRadius)
    {
        float distance = Vector3.Distance(point, sphereCenter);
        return distance <= sphereRadius;
    }
    
    /// <summary>
    /// 点与AABB碰撞检测
    /// </summary>
    public static bool PointAABB(Vector3 point, Bounds bounds)
    {
        return bounds.Contains(point);
    }
}

/// <summary>
/// 碰撞检测管理器（使用空间数据结构优化）
/// </summary>
public class CollisionDetectionManager : MonoBehaviour
{
    public List<GameObject> collidableObjects = new List<GameObject>();
    
    private Octree octree;
    
    void Start()
    {
        // 创建八叉树
        Bounds worldBounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
        octree = new Octree(worldBounds, 10, 5);
        
        // 插入所有对象
        foreach (GameObject obj in collidableObjects)
        {
            octree.Insert(obj);
        }
    }
    
    void Update()
    {
        // 检查所有对象的碰撞
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            GameObject obj1 = collidableObjects[i];
            Collider collider1 = obj1.GetComponent<Collider>();
            
            if (collider1 != null)
            {
                // 使用八叉树查询附近的对象
                Bounds bounds1 = collider1.bounds;
                List<GameObject> nearbyObjects = octree.Query(bounds1);
                
                foreach (GameObject obj2 in nearbyObjects)
                {
                    if (obj1 == obj2) continue;
                    
                    Collider collider2 = obj2.GetComponent<Collider>();
                    if (collider2 != null)
                    {
                        // 进行碰撞检测
                        if (CheckCollision(collider1, collider2))
                        {
                            OnCollisionDetected(obj1, obj2);
                        }
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 检查两个碰撞体是否碰撞
    /// </summary>
    private bool CheckCollision(Collider collider1, Collider collider2)
    {
        // 使用Unity Physics进行精确检测
        return collider1.bounds.Intersects(collider2.bounds);
    }
    
    /// <summary>
    /// 碰撞检测回调
    /// </summary>
    private void OnCollisionDetected(GameObject obj1, GameObject obj2)
    {
        // 处理碰撞逻辑
        Debug.Log($"碰撞检测: {obj1.name} 与 {obj2.name}");
    }
}
```

---

## Unity应用建议

1. **使用Unity Physics**：
   - Unity有完整的物理引擎（PhysX）
   - 使用`Collider`组件定义碰撞形状
   - 使用`Physics.OverlapSphere()`、`Physics.Raycast()`等进行查询

2. **性能优化**：
   - 使用空间数据结构（八叉树、四叉树）减少检测次数
   - 使用包围盒进行粗略检测，再用精确检测
   - 分帧处理，避免单帧计算过多

3. **自定义碰撞检测**：
   - 游戏逻辑层面的碰撞（非物理碰撞）
   - 特殊形状的碰撞检测
   - 性能关键路径的优化

---

## 参考文献

- 《游戏编程精粹1》- 4.5 3D碰撞检测

