# 剔除技术（Culling）- C#实现

## 核心概念

- **定义**：剔除不可见或不需要渲染的对象，减少渲染负担
- **核心思想**：在渲染前判断对象是否可见，只渲染可见对象
- **应用场景**：大规模场景优化、性能优化、减少DrawCall
- **Unity应用**：Unity自动进行视锥体剔除和遮挡剔除，但了解原理有助于优化

---

## 4.4 视锥体剔除（Frustum Culling）

### 核心原理

- **算法思想**：快速判断物体是否在摄像机视野（视锥体）内
- **视锥体**：摄像机视野形成的截头锥体（Frustum）
- **特点**：剔除视野外的对象，大幅减少渲染负担
- **应用**：所有3D游戏的基础优化技术

### 实现代码

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 视锥体剔除系统
/// </summary>
public class FrustumCulling
{
    /// <summary>
    /// 判断包围盒是否在视锥体内
    /// </summary>
    public static bool IsBoundsInFrustum(Bounds bounds, Plane[] frustumPlanes)
    {
        // 检查包围盒的8个顶点
        Vector3[] corners = GetBoundsCorners(bounds);
        
        // 对每个平面进行检查
        for (int i = 0; i < 6; i++)
        {
            bool allOutside = true;
            
            // 检查所有顶点是否都在平面外侧
            for (int j = 0; j < 8; j++)
            {
                if (frustumPlanes[i].GetSide(corners[j]))
                {
                    allOutside = false;
                    break;
                }
            }
            
            // 如果所有顶点都在平面外侧，对象在视锥体外
            if (allOutside)
            {
                return false;
            }
        }
        
        return true;
    }
    
    /// <summary>
    /// 获取包围盒的8个顶点
    /// </summary>
    private static Vector3[] GetBoundsCorners(Bounds bounds)
    {
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;
        
        return new Vector3[]
        {
            center + new Vector3(-extents.x, -extents.y, -extents.z),
            center + new Vector3(extents.x, -extents.y, -extents.z),
            center + new Vector3(-extents.x, extents.y, -extents.z),
            center + new Vector3(extents.x, extents.y, -extents.z),
            center + new Vector3(-extents.x, -extents.y, extents.z),
            center + new Vector3(extents.x, -extents.y, extents.z),
            center + new Vector3(-extents.x, extents.y, extents.z),
            center + new Vector3(extents.x, extents.y, extents.z)
        };
    }
    
    /// <summary>
    /// 快速测试（使用包围盒中心点）
    /// </summary>
    public static bool IsBoundsInFrustumFast(Bounds bounds, Plane[] frustumPlanes)
    {
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;
        
        for (int i = 0; i < 6; i++)
        {
            float distance = frustumPlanes[i].GetDistanceToPoint(center);
            float radius = GetBoundsRadius(bounds, frustumPlanes[i].normal);
            
            // 如果中心到平面的距离小于负半径，对象在平面外侧
            if (distance < -radius)
            {
                return false;
            }
        }
        
        return true;
    }
    
    /// <summary>
    /// 计算包围盒在指定方向上的半径
    /// </summary>
    private static float GetBoundsRadius(Bounds bounds, Vector3 direction)
    {
        Vector3 extents = bounds.extents;
        return Mathf.Abs(extents.x * direction.x) + 
               Mathf.Abs(extents.y * direction.y) + 
               Mathf.Abs(extents.z * direction.z);
    }
}

/// <summary>
/// 视锥体剔除管理器
/// </summary>
public class FrustumCullingManager : MonoBehaviour
{
    public Camera mainCamera;
    public List<GameObject> renderableObjects = new List<GameObject>();
    
    private Plane[] frustumPlanes = new Plane[6];
    private List<GameObject> visibleObjects = new List<GameObject>();
    
    void Update()
    {
        // 获取摄像机的视锥体平面
        GeometryUtility.CalculateFrustumPlanes(mainCamera, frustumPlanes);
        
        // 清除可见对象列表
        visibleObjects.Clear();
        
        // 检查每个对象
        foreach (GameObject obj in renderableObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Bounds bounds = renderer.bounds;
                
                // 判断是否在视锥体内
                if (FrustumCulling.IsBoundsInFrustum(bounds, frustumPlanes))
                {
                    visibleObjects.Add(obj);
                    renderer.enabled = true;
                }
                else
                {
                    renderer.enabled = false;
                }
            }
        }
    }
    
    /// <summary>
    /// 使用Unity内置方法（推荐）
    /// </summary>
    void UpdateWithUnityAPI()
    {
        GeometryUtility.CalculateFrustumPlanes(mainCamera, frustumPlanes);
        
        foreach (GameObject obj in renderableObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Unity内置的视锥体剔除测试
                if (GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds))
                {
                    renderer.enabled = true;
                }
                else
                {
                    renderer.enabled = false;
                }
            }
        }
    }
}
```

---

## 4.8 遮挡剔除（Occlusion Culling）

### 核心原理

- **算法思想**：判断物体是否被其他物体遮挡，剔除被遮挡的对象
- **特点**：进一步减少渲染负担，特别是室内场景
- **应用**：室内场景、复杂场景优化
- **Unity支持**：Unity有内置遮挡剔除系统

### 实现代码

```csharp
/// <summary>
/// 简单的遮挡剔除系统（基于射线检测）
/// </summary>
public class OcclusionCulling
{
    /// <summary>
    /// 判断对象是否被遮挡（使用射线检测）
    /// </summary>
    public static bool IsOccluded(GameObject obj, Camera camera, LayerMask occluderLayer)
    {
        Vector3 cameraPos = camera.transform.position;
        Vector3 objPos = obj.transform.position;
        Vector3 direction = (objPos - cameraPos).normalized;
        float distance = Vector3.Distance(cameraPos, objPos);
        
        // 从摄像机到对象发射射线
        RaycastHit hit;
        if (Physics.Raycast(cameraPos, direction, out hit, distance, occluderLayer))
        {
            // 如果射线击中其他物体，对象被遮挡
            return hit.collider.gameObject != obj;
        }
        
        return false;
    }
    
    /// <summary>
    /// 使用多个采样点判断遮挡（更准确）
    /// </summary>
    public static bool IsOccludedWithSampling(GameObject obj, Camera camera, LayerMask occluderLayer, int sampleCount = 5)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
            return false;
        
        Bounds bounds = renderer.bounds;
        Vector3 cameraPos = camera.transform.position;
        
        int occludedCount = 0;
        
        // 在包围盒上采样多个点
        for (int i = 0; i < sampleCount; i++)
        {
            Vector3 samplePoint = GetRandomPointInBounds(bounds);
            Vector3 direction = (samplePoint - cameraPos).normalized;
            float distance = Vector3.Distance(cameraPos, samplePoint);
            
            RaycastHit hit;
            if (Physics.Raycast(cameraPos, direction, out hit, distance, occluderLayer))
            {
                if (hit.collider.gameObject != obj)
                {
                    occludedCount++;
                }
            }
        }
        
        // 如果大部分采样点被遮挡，认为对象被遮挡
        return occludedCount > sampleCount * 0.5f;
    }
    
    /// <summary>
    /// 获取包围盒内的随机点
    /// </summary>
    private static Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}

/// <summary>
/// 遮挡剔除管理器
/// </summary>
public class OcclusionCullingManager : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask occluderLayer;
    public List<GameObject> renderableObjects = new List<GameObject>();
    
    void Update()
    {
        foreach (GameObject obj in renderableObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                // 先进行视锥体剔除
                Plane[] frustumPlanes = new Plane[6];
                GeometryUtility.CalculateFrustumPlanes(mainCamera, frustumPlanes);
                
                if (GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds))
                {
                    // 再进行遮挡剔除
                    if (!OcclusionCulling.IsOccluded(obj, mainCamera, occluderLayer))
                    {
                        renderer.enabled = true;
                    }
                    else
                    {
                        renderer.enabled = false;
                    }
                }
                else
                {
                    renderer.enabled = false;
                }
            }
        }
    }
}
```

---

## Unity应用建议

1. **视锥体剔除**：
   - Unity自动进行，无需手动实现
   - 使用`GeometryUtility.TestPlanesAABB()`进行自定义测试
   - 配合空间数据结构（八叉树）优化性能

2. **遮挡剔除**：
   - Unity有内置遮挡剔除系统（Occlusion Culling）
   - 在Window > Rendering > Occlusion Culling中设置
   - 需要标记静态对象为Occluder或Occludee

3. **性能优化**：
   - 使用空间数据结构减少测试对象数量
   - 分帧处理，避免单帧计算过多
   - 使用Job System并行计算

---

## 参考文献

- 《游戏编程精粹1》- 4.4 一种快速的圆柱棱台相交测试算法
- 《游戏编程精粹1》- 4.8 对象阻塞剔除

