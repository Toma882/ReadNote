# LOD技术（Level of Detail）- C#实现

## 核心概念

- **定义**：根据距离和屏幕空间选择不同细节层次的模型，优化渲染性能
- **核心思想**：远距离使用低精度模型，近距离使用高精度模型
- **应用场景**：大规模场景优化、减少DrawCall、提高帧率
- **Unity应用**：Unity有LOD Group组件，但了解选择算法有助于优化

---

## 4.9 几何体细节层次选择问题（LOD）

### 核心原理

- **算法思想**：根据对象到摄像机的距离或屏幕空间大小，选择不同细节层次的模型
- **LOD级别**：通常有3-4个级别（LOD0高精度，LOD1中精度，LOD2低精度，LOD3最低精度）
- **选择标准**：
  - **距离**：根据对象到摄像机的距离
  - **屏幕空间**：根据对象在屏幕上的大小（像素数）
  - **重要性**：根据对象的重要性（玩家关注度）

### 实现代码

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// LOD选择算法
/// </summary>
public static class LODSelection
{
    /// <summary>
    /// 基于距离的LOD选择
    /// </summary>
    public static int SelectLODByDistance(Vector3 objectPos, Vector3 cameraPos, float[] distanceThresholds)
    {
        float distance = Vector3.Distance(objectPos, cameraPos);
        
        for (int i = 0; i < distanceThresholds.Length; i++)
        {
            if (distance <= distanceThresholds[i])
            {
                return i;
            }
        }
        
        return distanceThresholds.Length;  // 最远距离，使用最低LOD
    }
    
    /// <summary>
    /// 基于屏幕空间的LOD选择（更准确）
    /// </summary>
    public static int SelectLODByScreenSpace(Bounds bounds, Camera camera, float[] screenSizeThresholds)
    {
        // 计算对象在屏幕上的大小（像素数）
        float screenSize = GetScreenSize(bounds, camera);
        
        for (int i = 0; i < screenSizeThresholds.Length; i++)
        {
            if (screenSize >= screenSizeThresholds[i])
            {
                return i;
            }
        }
        
        return screenSizeThresholds.Length;  // 最小屏幕空间，使用最低LOD
    }
    
    /// <summary>
    /// 计算对象在屏幕上的大小（像素数）
    /// </summary>
    private static float GetScreenSize(Bounds bounds, Camera camera)
    {
        // 计算包围盒的8个顶点在屏幕上的投影
        Vector3[] corners = GetBoundsCorners(bounds);
        Vector2 minScreen = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 maxScreen = new Vector2(float.MinValue, float.MinValue);
        
        foreach (Vector3 corner in corners)
        {
            Vector3 screenPos = camera.WorldToScreenPoint(corner);
            if (screenPos.z > 0)  // 在摄像机前方
            {
                minScreen.x = Mathf.Min(minScreen.x, screenPos.x);
                minScreen.y = Mathf.Min(minScreen.y, screenPos.y);
                maxScreen.x = Mathf.Max(maxScreen.x, screenPos.x);
                maxScreen.y = Mathf.Max(maxScreen.y, screenPos.y);
            }
        }
        
        // 计算屏幕空间大小（像素数）
        float width = maxScreen.x - minScreen.x;
        float height = maxScreen.y - minScreen.y;
        return Mathf.Max(width, height);
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
    /// 综合LOD选择（距离 + 屏幕空间）
    /// </summary>
    public static int SelectLOD(Bounds bounds, Vector3 objectPos, Camera camera, 
        float[] distanceThresholds, float[] screenSizeThresholds)
    {
        // 先基于距离选择
        int distanceLOD = SelectLODByDistance(objectPos, camera.transform.position, distanceThresholds);
        
        // 再基于屏幕空间选择
        int screenLOD = SelectLODByScreenSpace(bounds, camera, screenSizeThresholds);
        
        // 选择较低的LOD（更优化）
        return Mathf.Max(distanceLOD, screenLOD);
    }
}

/// <summary>
/// LOD管理器
/// </summary>
public class LODManager : MonoBehaviour
{
    public Camera mainCamera;
    public List<LODObject> lodObjects = new List<LODObject>();
    
    [Header("LOD阈值")]
    public float[] distanceThresholds = new float[] { 10f, 30f, 60f };
    public float[] screenSizeThresholds = new float[] { 200f, 100f, 50f };
    
    void Update()
    {
        foreach (LODObject lodObj in lodObjects)
        {
            if (lodObj != null && lodObj.gameObject.activeSelf)
            {
                int lodLevel = LODSelection.SelectLOD(
                    lodObj.bounds,
                    lodObj.transform.position,
                    mainCamera,
                    distanceThresholds,
                    screenSizeThresholds
                );
                
                lodObj.SetLOD(lodLevel);
            }
        }
    }
}

/// <summary>
/// LOD对象
/// </summary>
public class LODObject : MonoBehaviour
{
    public Renderer[] lodRenderers;  // 不同LOD级别的Renderer
    public Bounds bounds;
    
    void Start()
    {
        // 计算包围盒
        if (lodRenderers.Length > 0 && lodRenderers[0] != null)
        {
            bounds = lodRenderers[0].bounds;
        }
    }
    
    /// <summary>
    /// 设置LOD级别
    /// </summary>
    public void SetLOD(int lodLevel)
    {
        for (int i = 0; i < lodRenderers.Length; i++)
        {
            if (lodRenderers[i] != null)
            {
                lodRenderers[i].enabled = (i == lodLevel);
            }
        }
    }
}
```

---

## 4.12 独立于观察的渐进网格（Progressive Mesh）

### 核心原理

- **算法思想**：动态调整模型细节，根据距离或重要性逐步简化或细化网格
- **特点**：比传统LOD更灵活，可以平滑过渡
- **应用**：大规模场景优化、动态细节调整

### 实现代码

```csharp
/// <summary>
/// 渐进网格简化（基于边折叠）
/// </summary>
public class ProgressiveMesh
{
    /// <summary>
    /// 网格边
    /// </summary>
    public struct Edge
    {
        public int vertex1;
        public int vertex2;
        public float cost;  // 折叠代价
    }
    
    /// <summary>
    /// 简化网格（减少顶点数）
    /// </summary>
    public static Mesh SimplifyMesh(Mesh originalMesh, int targetVertexCount)
    {
        // 获取原始网格数据
        Vector3[] vertices = originalMesh.vertices;
        int[] triangles = originalMesh.triangles;
        
        // 计算边折叠代价
        List<Edge> edges = CalculateEdgeCosts(vertices, triangles);
        
        // 按代价排序
        edges.Sort((a, b) => a.cost.CompareTo(b.cost));
        
        // 逐步折叠边，直到达到目标顶点数
        // 注意：这是简化版本，实际实现更复杂
        
        // 创建简化后的网格
        Mesh simplifiedMesh = new Mesh();
        // ... 实现网格简化逻辑
        
        return simplifiedMesh;
    }
    
    /// <summary>
    /// 计算边折叠代价
    /// </summary>
    private static List<Edge> CalculateEdgeCosts(Vector3[] vertices, int[] triangles)
    {
        List<Edge> edges = new List<Edge>();
        
        // 遍历所有三角形，找到所有边
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int v1 = triangles[i];
            int v2 = triangles[i + 1];
            int v3 = triangles[i + 2];
            
            // 计算三条边的折叠代价
            AddEdge(edges, v1, v2, vertices);
            AddEdge(edges, v2, v3, vertices);
            AddEdge(edges, v3, v1, vertices);
        }
        
        return edges;
    }
    
    /// <summary>
    /// 添加边（如果不存在）
    /// </summary>
    private static void AddEdge(List<Edge> edges, int v1, int v2, Vector3[] vertices)
    {
        // 检查边是否已存在
        foreach (Edge edge in edges)
        {
            if ((edge.vertex1 == v1 && edge.vertex2 == v2) ||
                (edge.vertex1 == v2 && edge.vertex2 == v1))
            {
                return;
            }
        }
        
        // 计算折叠代价（简化：使用边长度）
        float cost = Vector3.Distance(vertices[v1], vertices[v2]);
        
        edges.Add(new Edge { vertex1 = v1, vertex2 = v2, cost = cost });
    }
}

/// <summary>
/// 渐进网格管理器
/// </summary>
public class ProgressiveMeshManager : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Camera mainCamera;
    
    [Header("LOD设置")]
    public int[] lodVertexCounts = new int[] { 1000, 500, 200, 50 };
    public float[] distanceThresholds = new float[] { 10f, 30f, 60f };
    
    private Mesh[] lodMeshes;
    private Mesh originalMesh;
    
    void Start()
    {
        originalMesh = meshFilter.mesh;
        lodMeshes = new Mesh[lodVertexCounts.Length];
        
        // 生成不同LOD级别的网格
        for (int i = 0; i < lodVertexCounts.Length; i++)
        {
            lodMeshes[i] = ProgressiveMesh.SimplifyMesh(originalMesh, lodVertexCounts[i]);
        }
    }
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, mainCamera.transform.position);
        
        // 根据距离选择LOD
        int lodLevel = 0;
        for (int i = 0; i < distanceThresholds.Length; i++)
        {
            if (distance > distanceThresholds[i])
            {
                lodLevel = i + 1;
            }
        }
        
        lodLevel = Mathf.Clamp(lodLevel, 0, lodMeshes.Length - 1);
        meshFilter.mesh = lodMeshes[lodLevel];
    }
}
```

---

## Unity应用建议

1. **使用Unity LOD Group**：
   - Unity有内置LOD Group组件
   - 在Inspector中设置不同LOD级别的模型
   - 自动根据距离切换

2. **性能优化**：
   - 合理设置LOD阈值
   - 使用屏幕空间大小而非仅距离
   - 考虑对象重要性

3. **渐进网格**：
   - Unity不直接支持，需要使用第三方工具
   - 可以使用`Mesh Simplifier`等插件
   - 或自己实现边折叠算法

---

## 参考文献

- 《游戏编程精粹1》- 4.9 几何体细节层次选择问题（LOD）
- 《游戏编程精粹1》- 4.12 独立于观察的渐进网格

