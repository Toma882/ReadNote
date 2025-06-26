using UnityEngine;
#if UNITY_2020_2_OR_NEWER
using UnityEngine.TerrainTools;
#endif

/// <summary>
/// UnityEngine.TerrainTools 命名空间案例演示
/// 展示地形编辑、高度图、纹理等核心功能
/// </summary>
public class TerrainToolsExample : MonoBehaviour
{
    [Header("地形工具设置")]
    [SerializeField] private Terrain terrain; //地形对象
    [SerializeField] private bool enableTerrainTools = true; //是否启用地形工具
    [SerializeField] private float brushSize = 10f; //笔刷大小
    [SerializeField] private float brushStrength = 0.5f; //笔刷强度
    [SerializeField] private TerrainToolType toolType = TerrainToolType.Raise; //工具类型

    private TerrainData terrainData;
    private int terrainResolution;

    public enum TerrainToolType
    {
        Raise, //升高
        Lower, //降低
        Smooth, //平滑
        Paint //绘制
    }

    private void Start()
    {
        if (terrain == null)
        {
            terrain = FindObjectOfType<Terrain>();
        }

        if (terrain != null)
        {
            terrainData = terrain.terrainData;
            terrainResolution = terrainData.heightmapResolution;
            Debug.Log($"地形分辨率: {terrainResolution}x{terrainResolution}");
        }
    }

    private void Update()
    {
        if (enableTerrainTools && terrain != null)
        {
            HandleTerrainEditing();
        }
    }

    /// <summary>
    /// 处理地形编辑
    /// </summary>
    private void HandleTerrainEditing()
    {
        if (Input.GetMouseButton(0)) // 左键按下
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == terrain.gameObject)
            {
                Vector3 terrainPosition = hit.point;
                ApplyTerrainTool(terrainPosition);
            }
        }
    }

    /// <summary>
    /// 应用地形工具
    /// </summary>
    /// <param name="worldPosition">世界坐标</param>
    private void ApplyTerrainTool(Vector3 worldPosition)
    {
        Vector3 terrainPosition = terrain.transform.position;
        Vector3 localPosition = worldPosition - terrainPosition;

        // 转换为地形坐标
        float normalizedX = localPosition.x / terrainData.size.x;
        float normalizedZ = localPosition.z / terrainData.size.z;

        // 转换为高度图坐标
        int heightmapX = Mathf.RoundToInt(normalizedX * (terrainResolution - 1));
        int heightmapZ = Mathf.RoundToInt(normalizedZ * (terrainResolution - 1));

        // 确保坐标在有效范围内
        heightmapX = Mathf.Clamp(heightmapX, 0, terrainResolution - 1);
        heightmapZ = Mathf.Clamp(heightmapZ, 0, terrainResolution - 1);

        // 计算笔刷范围
        int brushRadius = Mathf.RoundToInt(brushSize);
        int startX = Mathf.Max(0, heightmapX - brushRadius);
        int endX = Mathf.Min(terrainResolution - 1, heightmapX + brushRadius);
        int startZ = Mathf.Max(0, heightmapZ - brushRadius);
        int endZ = Mathf.Min(terrainResolution - 1, heightmapZ + brushRadius);

        // 获取当前高度图数据
        float[,] heights = terrainData.GetHeights(startX, startZ, endX - startX + 1, endZ - startZ + 1);

        // 应用工具
        for (int x = 0; x <= endX - startX; x++)
        {
            for (int z = 0; z <= endZ - startZ; z++)
            {
                float distance = Vector2.Distance(new Vector2(x, z), new Vector2(heightmapX - startX, heightmapZ - startZ));
                float brushInfluence = 1f - Mathf.Clamp01(distance / brushRadius);
                brushInfluence *= brushStrength;

                switch (toolType)
                {
                    case TerrainToolType.Raise:
                        heights[x, z] += brushInfluence * Time.deltaTime;
                        break;
                    case TerrainToolType.Lower:
                        heights[x, z] -= brushInfluence * Time.deltaTime;
                        break;
                    case TerrainToolType.Smooth:
                        // 简单的平滑算法
                        if (x > 0 && x < heights.GetLength(0) - 1 && z > 0 && z < heights.GetLength(1) - 1)
                        {
                            float average = (heights[x - 1, z] + heights[x + 1, z] + heights[x, z - 1] + heights[x, z + 1]) / 4f;
                            heights[x, z] = Mathf.Lerp(heights[x, z], average, brushInfluence);
                        }
                        break;
                }

                // 限制高度范围
                heights[x, z] = Mathf.Clamp01(heights[x, z]);
            }
        }

        // 应用高度图数据
        terrainData.SetHeights(startX, startZ, heights);
    }

    /// <summary>
    /// 重置地形
    /// </summary>
    public void ResetTerrain()
    {
        if (terrainData != null)
        {
            float[,] heights = new float[terrainResolution, terrainResolution];
            terrainData.SetHeights(0, 0, heights);
            Debug.Log("地形已重置");
        }
    }

    /// <summary>
    /// 生成随机地形
    /// </summary>
    public void GenerateRandomTerrain()
    {
        if (terrainData != null)
        {
            float[,] heights = new float[terrainResolution, terrainResolution];
            
            for (int x = 0; x < terrainResolution; x++)
            {
                for (int z = 0; z < terrainResolution; z++)
                {
                    float noiseX = (float)x / terrainResolution * 10f;
                    float noiseZ = (float)z / terrainResolution * 10f;
                    heights[x, z] = Mathf.PerlinNoise(noiseX, noiseZ) * 0.5f;
                }
            }
            
            terrainData.SetHeights(0, 0, heights);
            Debug.Log("随机地形已生成");
        }
    }

    /// <summary>
    /// 获取地形信息
    /// </summary>
    public void GetTerrainInfo()
    {
        if (terrainData != null)
        {
            Debug.Log("=== 地形信息 ===");
            Debug.Log($"地形大小: {terrainData.size}");
            Debug.Log($"高度图分辨率: {terrainData.heightmapResolution}");
            Debug.Log($"地形分辨率: {terrainData.heightmapResolution}x{terrainData.heightmapResolution}");
            Debug.Log($"最大高度: {terrainData.size.y}");
            Debug.Log($"纹理层数: {terrainData.terrainLayers?.Length ?? 0}");
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 350));
        GUILayout.Label("Terrain Tools 地形工具演示", UnityEditor.EditorStyles.boldLabel);
        
        terrain = (Terrain)UnityEditor.EditorGUILayout.ObjectField("地形", terrain, typeof(Terrain), true);
        enableTerrainTools = GUILayout.Toggle(enableTerrainTools, "启用地形工具");
        brushSize = float.TryParse(GUILayout.TextField("笔刷大小", brushSize.ToString()), out var size) ? size : brushSize;
        brushStrength = float.TryParse(GUILayout.TextField("笔刷强度", brushStrength.ToString()), out var strength) ? strength : brushStrength;
        
        GUILayout.Label("工具类型:");
        toolType = (TerrainToolType)GUILayout.SelectionGrid((int)toolType, 
            new string[] { "升高", "降低", "平滑", "绘制" }, 2);
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取地形信息"))
        {
            GetTerrainInfo();
        }
        
        if (GUILayout.Button("重置地形"))
        {
            ResetTerrain();
        }
        
        if (GUILayout.Button("生成随机地形"))
        {
            GenerateRandomTerrain();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("操作说明:");
        GUILayout.Label("• 左键拖拽编辑地形");
        GUILayout.Label("• 调整笔刷大小和强度");
        GUILayout.Label("• 选择不同的工具类型");
        
        GUILayout.EndArea();
    }
} 