using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

/// <summary>
/// UnityEditor.TerrainTools 命名空间案例演示
/// 展示地形工具编辑器的核心功能，包括地形创建、编辑、纹理应用等
/// </summary>
public class TerrainToolsExample : MonoBehaviour
{
    [Header("地形工具配置")]
    [SerializeField] private bool enableTerrainTools = true; //启用地形工具
    [SerializeField] private bool enableTerrainCreation = true; //启用地形创建
    [SerializeField] private bool enableTerrainEditing = true; //启用地形编辑
    [SerializeField] private bool enableTerrainTexturing = true; //启用地形纹理
    [SerializeField] private bool enableTerrainTrees = true; //启用地形树木
    [SerializeField] private bool enableTerrainDetails = true; //启用地形细节
    
    [Header("地形创建配置")]
    [SerializeField] private int terrainWidth = 1024; //地形宽度
    [SerializeField] private int terrainLength = 1024; //地形长度
    [SerializeField] private int terrainHeight = 600; //地形高度
    [SerializeField] private int heightmapResolution = 513; //高度图分辨率
    [SerializeField] private int detailResolution = 1024; //细节分辨率
    [SerializeField] private int controlTextureResolution = 512; //控制纹理分辨率
    [SerializeField] private int baseTextureResolution = 1024; //基础纹理分辨率
    
    [Header("地形编辑配置")]
    [SerializeField] private float brushSize = 10f; //笔刷大小
    [SerializeField] private float brushStrength = 0.5f; //笔刷强度
    [SerializeField] private float brushOpacity = 1f; //笔刷透明度
    [SerializeField] private TerrainToolType currentTool = TerrainToolType.Raise; //当前工具类型
    [SerializeField] private bool enableSmooth = false; //启用平滑
    [SerializeField] private bool enableFlatten = false; //启用平整
    [SerializeField] private float targetHeight = 0f; //目标高度
    
    [Header("地形纹理配置")]
    [SerializeField] private Texture2D[] terrainTextures; //地形纹理数组
    [SerializeField] private float[] textureTiling; //纹理平铺
    [SerializeField] private float[] textureOffset; //纹理偏移
    [SerializeField] private int activeTextureIndex = 0; //活动纹理索引
    [SerializeField] private bool enableTextureBlending = true; //启用纹理混合
    [SerializeField] private float blendStrength = 0.5f; //混合强度
    
    [Header("地形树木配置")]
    [SerializeField] private GameObject[] treePrefabs; //树木预制体
    [SerializeField] private float[] treeDensity; //树木密度
    [SerializeField] private float[] treeHeight; //树木高度
    [SerializeField] private float[] treeWidth; //树木宽度
    [SerializeField] private int activeTreeIndex = 0; //活动树木索引
    [SerializeField] private bool enableTreePlacement = true; //启用树木放置
    
    [Header("地形细节配置")]
    [SerializeField] private GameObject[] detailPrefabs; //细节预制体
    [SerializeField] private float[] detailDensity; //细节密度
    [SerializeField] private float[] detailHeight; //细节高度
    [SerializeField] private float[] detailWidth; //细节宽度
    [SerializeField] private int activeDetailIndex = 0; //活动细节索引
    [SerializeField] private bool enableDetailPlacement = true; //启用细节放置
    
    [Header("地形状态")]
    [SerializeField] private string terrainToolsState = "未初始化"; //地形工具状态
    [SerializeField] private string currentEditingMode = "空闲"; //当前编辑模式
    [SerializeField] private bool isTerrainDirty = false; //地形是否脏
    [SerializeField] private bool isHeightmapDirty = false; //高度图是否脏
    [SerializeField] private bool isTextureDirty = false; //纹理是否脏
    [SerializeField] private Vector3 terrainSize = Vector3.zero; //地形大小
    [SerializeField] private Vector3 terrainPosition = Vector3.zero; //地形位置
    
    [Header("性能监控")]
    [SerializeField] private bool enableTerrainMonitoring = true; //启用地形监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logTerrainData = false; //记录地形数据
    [SerializeField] private float terrainEditTime = 0f; //地形编辑时间
    [SerializeField] private int totalVertices = 0; //总顶点数
    [SerializeField] private int totalTriangles = 0; //总三角形数
    [SerializeField] private float memoryUsage = 0f; //内存使用量
    
    [Header("地形数据")]
    [SerializeField] private float[] heightmapData; //高度图数据
    [SerializeField] private Color[] splatmapData; //混合图数据
    [SerializeField] private int[] detailmapData; //细节图数据
    [SerializeField] private TreeInstance[] treeInstances; //树木实例
    [SerializeField] private DetailInstance[] detailInstances; //细节实例
    
    private Terrain terrain;
    private TerrainData terrainData;
    private TerrainLayer[] terrainLayers;
    private TerrainTool[] terrainTools;
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;
    private Vector3 lastMousePosition = Vector3.zero;

    private void Start()
    {
        InitializeTerrainTools();
    }

    /// <summary>
    /// 初始化地形工具
    /// </summary>
    private void InitializeTerrainTools()
    {
        // 创建地形
        CreateTerrain();
        
        // 初始化地形数据
        InitializeTerrainData();
        
        // 初始化地形工具
        InitializeTerrainTools();
        
        // 初始化地形纹理
        InitializeTerrainTextures();
        
        // 初始化地形树木
        InitializeTerrainTrees();
        
        // 初始化地形细节
        InitializeTerrainDetails();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        isInitialized = true;
        terrainToolsState = "已初始化";
        Debug.Log("地形工具初始化完成");
    }

    /// <summary>
    /// 创建地形
    /// </summary>
    private void CreateTerrain()
    {
        // 检查是否已存在地形
        terrain = FindObjectOfType<Terrain>();
        if (terrain == null)
        {
            // 创建新的地形游戏对象
            GameObject terrainObject = new GameObject("Terrain");
            terrain = terrainObject.AddComponent<Terrain>();
            terrainObject.AddComponent<TerrainCollider>();
        }
        
        // 创建地形数据
        terrainData = new TerrainData();
        terrainData.heightmapResolution = heightmapResolution;
        terrainData.size = new Vector3(terrainWidth, terrainHeight, terrainLength);
        terrainData.SetDetailResolution(detailResolution, 16);
        
        // 设置地形数据
        terrain.terrainData = terrainData;
        terrain.transform.position = terrainPosition;
        
        terrainSize = terrainData.size;
        Debug.Log($"地形创建完成: {terrainSize}");
    }

    /// <summary>
    /// 初始化地形数据
    /// </summary>
    private void InitializeTerrainData()
    {
        // 初始化高度图
        float[,] heights = new float[heightmapResolution, heightmapResolution];
        for (int x = 0; x < heightmapResolution; x++)
        {
            for (int y = 0; y < heightmapResolution; y++)
            {
                heights[x, y] = 0.5f; // 默认高度
            }
        }
        terrainData.SetHeights(0, 0, heights);
        
        // 初始化混合图
        terrainData.alphamapResolution = controlTextureResolution;
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, 1];
        for (int x = 0; x < terrainData.alphamapWidth; x++)
        {
            for (int y = 0; y < terrainData.alphamapHeight; y++)
            {
                splatmapData[x, y, 0] = 1f; // 默认纹理权重
            }
        }
        terrainData.SetAlphamaps(0, 0, splatmapData);
        
        Debug.Log("地形数据初始化完成");
    }

    /// <summary>
    /// 初始化地形工具
    /// </summary>
    private void InitializeTerrainTools()
    {
        terrainTools = new TerrainTool[]
        {
            new RaiseTerrainTool(),
            new LowerTerrainTool(),
            new SmoothTerrainTool(),
            new FlattenTerrainTool(),
            new PaintTerrainTool()
        };
        
        Debug.Log("地形工具初始化完成");
    }

    /// <summary>
    /// 初始化地形纹理
    /// </summary>
    private void InitializeTerrainTextures()
    {
        if (terrainTextures != null && terrainTextures.Length > 0)
        {
            terrainLayers = new TerrainLayer[terrainTextures.Length];
            for (int i = 0; i < terrainTextures.Length; i++)
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = terrainTextures[i];
                terrainLayers[i].tileSize = new Vector2(textureTiling != null && i < textureTiling.Length ? textureTiling[i] : 15f, 
                                                       textureTiling != null && i < textureTiling.Length ? textureTiling[i] : 15f);
                terrainLayers[i].tileOffset = new Vector2(textureOffset != null && i < textureOffset.Length ? textureOffset[i] : 0f,
                                                         textureOffset != null && i < textureOffset.Length ? textureOffset[i] : 0f);
            }
            terrainData.terrainLayers = terrainLayers;
        }
        
        Debug.Log("地形纹理初始化完成");
    }

    /// <summary>
    /// 初始化地形树木
    /// </summary>
    private void InitializeTerrainTrees()
    {
        if (treePrefabs != null && treePrefabs.Length > 0)
        {
            terrainData.treePrototypes = new TreePrototype[treePrefabs.Length];
            for (int i = 0; i < treePrefabs.Length; i++)
            {
                terrainData.treePrototypes[i] = new TreePrototype();
                terrainData.treePrototypes[i].prefab = treePrefabs[i];
            }
        }
        
        Debug.Log("地形树木初始化完成");
    }

    /// <summary>
    /// 初始化地形细节
    /// </summary>
    private void InitializeTerrainDetails()
    {
        if (detailPrefabs != null && detailPrefabs.Length > 0)
        {
            terrainData.detailPrototypes = new DetailPrototype[detailPrefabs.Length];
            for (int i = 0; i < detailPrefabs.Length; i++)
            {
                terrainData.detailPrototypes[i] = new DetailPrototype();
                terrainData.detailPrototypes[i].prototype = detailPrefabs[i];
                terrainData.detailPrototypes[i].minHeight = detailHeight != null && i < detailHeight.Length ? detailHeight[i] : 1f;
                terrainData.detailPrototypes[i].maxHeight = detailHeight != null && i < detailHeight.Length ? detailHeight[i] * 1.5f : 1.5f;
                terrainData.detailPrototypes[i].minWidth = detailWidth != null && i < detailWidth.Length ? detailWidth[i] : 1f;
                terrainData.detailPrototypes[i].maxWidth = detailWidth != null && i < detailWidth.Length ? detailWidth[i] * 1.5f : 1.5f;
            }
        }
        
        Debug.Log("地形细节初始化完成");
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        lastMonitoringTime = Time.time;
        Debug.Log("性能监控初始化完成");
    }

    private void Update()
    {
        if (!isInitialized || !enableTerrainTools) return;
        
        // 更新性能监控
        if (enableTerrainMonitoring && Time.time - lastMonitoringTime >= monitoringInterval)
        {
            MonitorTerrainPerformance();
            lastMonitoringTime = Time.time;
        }
        
        // 处理地形编辑
        HandleTerrainEditing();
        
        // 更新地形状态
        UpdateTerrainStatus();
    }

    /// <summary>
    /// 处理地形编辑
    /// </summary>
    private void HandleTerrainEditing()
    {
        if (!enableTerrainEditing) return;
        
        // 检查鼠标输入
        if (Input.GetMouseButton(0) && currentTool != TerrainToolType.None)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<Terrain>() == terrain)
            {
                Vector3 terrainPoint = hit.point;
                ApplyTerrainTool(terrainPoint);
                currentEditingMode = $"正在使用 {currentTool} 工具";
            }
        }
        else
        {
            currentEditingMode = "空闲";
        }
    }

    /// <summary>
    /// 应用地形工具
    /// </summary>
    private void ApplyTerrainTool(Vector3 point)
    {
        Vector3 terrainPosition = terrain.transform.position;
        Vector3 localPoint = point - terrainPosition;
        
        // 转换为地形坐标
        float normalizedX = localPoint.x / terrainData.size.x;
        float normalizedZ = localPoint.z / terrainData.size.z;
        
        // 应用工具效果
        switch (currentTool)
        {
            case TerrainToolType.Raise:
                RaiseTerrain(normalizedX, normalizedZ);
                break;
            case TerrainToolType.Lower:
                LowerTerrain(normalizedX, normalizedZ);
                break;
            case TerrainToolType.Smooth:
                SmoothTerrain(normalizedX, normalizedZ);
                break;
            case TerrainToolType.Flatten:
                FlattenTerrain(normalizedX, normalizedZ);
                break;
            case TerrainToolType.Paint:
                PaintTerrain(normalizedX, normalizedZ);
                break;
        }
        
        isTerrainDirty = true;
        isHeightmapDirty = true;
    }

    /// <summary>
    /// 升高地形
    /// </summary>
    private void RaiseTerrain(float x, float z)
    {
        int brushSizeInPixels = Mathf.RoundToInt(brushSize * terrainData.heightmapResolution / terrainData.size.x);
        int centerX = Mathf.RoundToInt(x * terrainData.heightmapResolution);
        int centerZ = Mathf.RoundToInt(z * terrainData.heightmapResolution);
        
        float[,] heights = terrainData.GetHeights(centerX - brushSizeInPixels, centerZ - brushSizeInPixels, 
                                                 brushSizeInPixels * 2, brushSizeInPixels * 2);
        
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                float distance = Vector2.Distance(new Vector2(i, j), new Vector2(brushSizeInPixels, brushSizeInPixels));
                float strength = Mathf.Clamp01(1f - distance / brushSizeInPixels) * brushStrength * brushOpacity;
                heights[i, j] += strength * Time.deltaTime;
            }
        }
        
        terrainData.SetHeights(centerX - brushSizeInPixels, centerZ - brushSizeInPixels, heights);
    }

    /// <summary>
    /// 降低地形
    /// </summary>
    private void LowerTerrain(float x, float z)
    {
        int brushSizeInPixels = Mathf.RoundToInt(brushSize * terrainData.heightmapResolution / terrainData.size.x);
        int centerX = Mathf.RoundToInt(x * terrainData.heightmapResolution);
        int centerZ = Mathf.RoundToInt(z * terrainData.heightmapResolution);
        
        float[,] heights = terrainData.GetHeights(centerX - brushSizeInPixels, centerZ - brushSizeInPixels, 
                                                 brushSizeInPixels * 2, brushSizeInPixels * 2);
        
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                float distance = Vector2.Distance(new Vector2(i, j), new Vector2(brushSizeInPixels, brushSizeInPixels));
                float strength = Mathf.Clamp01(1f - distance / brushSizeInPixels) * brushStrength * brushOpacity;
                heights[i, j] -= strength * Time.deltaTime;
            }
        }
        
        terrainData.SetHeights(centerX - brushSizeInPixels, centerZ - brushSizeInPixels, heights);
    }

    /// <summary>
    /// 平滑地形
    /// </summary>
    private void SmoothTerrain(float x, float z)
    {
        // 实现地形平滑逻辑
        Debug.Log("应用地形平滑");
    }

    /// <summary>
    /// 平整地形
    /// </summary>
    private void FlattenTerrain(float x, float z)
    {
        int brushSizeInPixels = Mathf.RoundToInt(brushSize * terrainData.heightmapResolution / terrainData.size.x);
        int centerX = Mathf.RoundToInt(x * terrainData.heightmapResolution);
        int centerZ = Mathf.RoundToInt(z * terrainData.heightmapResolution);
        
        float[,] heights = terrainData.GetHeights(centerX - brushSizeInPixels, centerZ - brushSizeInPixels, 
                                                 brushSizeInPixels * 2, brushSizeInPixels * 2);
        
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                float distance = Vector2.Distance(new Vector2(i, j), new Vector2(brushSizeInPixels, brushSizeInPixels));
                float strength = Mathf.Clamp01(1f - distance / brushSizeInPixels) * brushStrength * brushOpacity;
                heights[i, j] = Mathf.Lerp(heights[i, j], targetHeight / terrainData.size.y, strength * Time.deltaTime);
            }
        }
        
        terrainData.SetHeights(centerX - brushSizeInPixels, centerZ - brushSizeInPixels, heights);
    }

    /// <summary>
    /// 绘制地形纹理
    /// </summary>
    private void PaintTerrain(float x, float z)
    {
        if (!enableTerrainTexturing || terrainLayers == null || activeTextureIndex >= terrainLayers.Length) return;
        
        int brushSizeInPixels = Mathf.RoundToInt(brushSize * terrainData.alphamapResolution / terrainData.size.x);
        int centerX = Mathf.RoundToInt(x * terrainData.alphamapResolution);
        int centerZ = Mathf.RoundToInt(z * terrainData.alphamapResolution);
        
        float[,,] splatmapData = terrainData.GetAlphamaps(centerX - brushSizeInPixels, centerZ - brushSizeInPixels, 
                                                         brushSizeInPixels * 2, brushSizeInPixels * 2);
        
        for (int i = 0; i < splatmapData.GetLength(0); i++)
        {
            for (int j = 0; j < splatmapData.GetLength(1); j++)
            {
                float distance = Vector2.Distance(new Vector2(i, j), new Vector2(brushSizeInPixels, brushSizeInPixels));
                float strength = Mathf.Clamp01(1f - distance / brushSizeInPixels) * brushStrength * brushOpacity;
                
                // 混合纹理
                for (int k = 0; k < splatmapData.GetLength(2); k++)
                {
                    if (k == activeTextureIndex)
                    {
                        splatmapData[i, j, k] = Mathf.Lerp(splatmapData[i, j, k], 1f, strength * Time.deltaTime);
                    }
                    else
                    {
                        splatmapData[i, j, k] = Mathf.Lerp(splatmapData[i, j, k], 0f, strength * Time.deltaTime);
                    }
                }
            }
        }
        
        terrainData.SetAlphamaps(centerX - brushSizeInPixels, centerZ - brushSizeInPixels, splatmapData);
        isTextureDirty = true;
    }

    /// <summary>
    /// 监控地形性能
    /// </summary>
    private void MonitorTerrainPerformance()
    {
        if (terrainData != null)
        {
            totalVertices = terrainData.heightmapResolution * terrainData.heightmapResolution;
            totalTriangles = (terrainData.heightmapResolution - 1) * (terrainData.heightmapResolution - 1) * 2;
            memoryUsage = totalVertices * 4f / 1024f / 1024f; // 估算内存使用量 (MB)
        }
        
        if (logTerrainData)
        {
            Debug.Log($"地形性能数据 - 顶点数: {totalVertices}, 三角形数: {totalTriangles}, 内存使用: {memoryUsage:F2}MB");
        }
    }

    /// <summary>
    /// 更新地形状态
    /// </summary>
    private void UpdateTerrainStatus()
    {
        if (terrain != null)
        {
            terrainPosition = terrain.transform.position;
            terrainSize = terrainData != null ? terrainData.size : Vector3.zero;
        }
    }

    /// <summary>
    /// 设置地形工具类型
    /// </summary>
    public void SetTerrainTool(TerrainToolType toolType)
    {
        currentTool = toolType;
        Debug.Log($"切换到地形工具: {toolType}");
    }

    /// <summary>
    /// 设置笔刷大小
    /// </summary>
    public void SetBrushSize(float size)
    {
        brushSize = Mathf.Clamp(size, 1f, 100f);
        Debug.Log($"笔刷大小设置为: {brushSize}");
    }

    /// <summary>
    /// 设置笔刷强度
    /// </summary>
    public void SetBrushStrength(float strength)
    {
        brushStrength = Mathf.Clamp01(strength);
        Debug.Log($"笔刷强度设置为: {brushStrength}");
    }

    /// <summary>
    /// 设置活动纹理
    /// </summary>
    public void SetActiveTexture(int index)
    {
        if (terrainLayers != null && index >= 0 && index < terrainLayers.Length)
        {
            activeTextureIndex = index;
            Debug.Log($"活动纹理设置为: {index}");
        }
    }

    /// <summary>
    /// 添加树木
    /// </summary>
    public void AddTree(Vector3 position)
    {
        if (!enableTreePlacement || treePrefabs == null || activeTreeIndex >= treePrefabs.Length) return;
        
        Vector3 terrainPosition = terrain.transform.position;
        Vector3 localPosition = position - terrainPosition;
        
        float normalizedX = localPosition.x / terrainData.size.x;
        float normalizedZ = localPosition.z / terrainData.size.z;
        float normalizedHeight = localPosition.y / terrainData.size.y;
        
        TreeInstance treeInstance = new TreeInstance
        {
            position = new Vector3(normalizedX, normalizedHeight, normalizedZ),
            rotation = Random.Range(0f, 360f) * Mathf.Deg2Rad,
            prototypeIndex = activeTreeIndex,
            widthScale = treeWidth != null && activeTreeIndex < treeWidth.Length ? treeWidth[activeTreeIndex] : 1f,
            heightScale = treeHeight != null && activeTreeIndex < treeHeight.Length ? treeHeight[activeTreeIndex] : 1f,
            color = Color.white,
            lightmapColor = Color.white
        };
        
        terrainData.AddTreeInstance(treeInstance);
        Debug.Log($"添加树木到位置: {position}");
    }

    /// <summary>
    /// 添加细节
    /// </summary>
    public void AddDetail(Vector3 position)
    {
        if (!enableDetailPlacement || detailPrefabs == null || activeDetailIndex >= detailPrefabs.Length) return;
        
        Vector3 terrainPosition = terrain.transform.position;
        Vector3 localPosition = position - terrainPosition;
        
        int detailX = Mathf.RoundToInt(localPosition.x / terrainData.size.x * terrainData.detailWidth);
        int detailZ = Mathf.RoundToInt(localPosition.z / terrainData.size.z * terrainData.detailHeight);
        
        int[,] detailMap = terrainData.GetDetailLayer(detailX, detailZ, 1, 1, activeDetailIndex);
        detailMap[0, 0] = 1;
        terrainData.SetDetailLayer(detailX, detailZ, activeDetailIndex, detailMap);
        
        Debug.Log($"添加细节到位置: {position}");
    }

    /// <summary>
    /// 生成地形报告
    /// </summary>
    public void GenerateTerrainReport()
    {
        TerrainData reportData = new TerrainData
        {
            timestamp = System.DateTime.Now.ToString(),
            terrainToolsState = terrainToolsState,
            currentEditingMode = currentEditingMode,
            terrainSize = terrainSize,
            terrainPosition = terrainPosition,
            totalVertices = totalVertices,
            totalTriangles = totalTriangles,
            memoryUsage = memoryUsage,
            brushSize = brushSize,
            brushStrength = brushStrength,
            currentTool = currentTool.ToString(),
            activeTextureIndex = activeTextureIndex,
            activeTreeIndex = activeTreeIndex,
            activeDetailIndex = activeDetailIndex
        };
        
        string reportJson = JsonUtility.ToJson(reportData, true);
        Debug.Log($"地形报告生成完成:\n{reportJson}");
    }

    /// <summary>
    /// 导出地形数据
    /// </summary>
    public void ExportTerrainData()
    {
        if (terrainData == null) return;
        
        // 导出高度图
        float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        heightmapData = new float[heights.GetLength(0) * heights.GetLength(1)];
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                heightmapData[i * heights.GetLength(1) + j] = heights[i, j];
            }
        }
        
        // 导出混合图
        float[,,] splatmap = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
        splatmapData = new Color[splatmap.GetLength(0) * splatmap.GetLength(1)];
        for (int i = 0; i < splatmap.GetLength(0); i++)
        {
            for (int j = 0; j < splatmap.GetLength(1); j++)
            {
                splatmapData[i * splatmap.GetLength(1) + j] = new Color(
                    splatmap.GetLength(2) > 0 ? splatmap[i, j, 0] : 0f,
                    splatmap.GetLength(2) > 1 ? splatmap[i, j, 1] : 0f,
                    splatmap.GetLength(2) > 2 ? splatmap[i, j, 2] : 0f,
                    splatmap.GetLength(2) > 3 ? splatmap[i, j, 3] : 0f
                );
            }
        }
        
        // 导出树木实例
        treeInstances = terrainData.treeInstances;
        
        Debug.Log("地形数据导出完成");
    }

    private void OnGUI()
    {
        if (!isInitialized) return;
        
        GUILayout.BeginArea(new Rect(10, 10, 300, Screen.height - 20));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label("地形工具编辑器", EditorStyles.boldLabel);
        
        // 地形工具配置
        GUILayout.Space(10);
        GUILayout.Label("工具配置", EditorStyles.boldLabel);
        enableTerrainTools = GUILayout.Toggle(enableTerrainTools, "启用地形工具");
        enableTerrainCreation = GUILayout.Toggle(enableTerrainCreation, "启用地形创建");
        enableTerrainEditing = GUILayout.Toggle(enableTerrainEditing, "启用地形编辑");
        enableTerrainTexturing = GUILayout.Toggle(enableTerrainTexturing, "启用地形纹理");
        enableTerrainTrees = GUILayout.Toggle(enableTerrainTrees, "启用地形树木");
        enableTerrainDetails = GUILayout.Toggle(enableTerrainDetails, "启用地形细节");
        
        // 地形编辑配置
        GUILayout.Space(10);
        GUILayout.Label("编辑配置", EditorStyles.boldLabel);
        GUILayout.Label($"当前工具: {currentTool}");
        brushSize = GUILayout.HorizontalSlider(brushSize, 1f, 100f);
        GUILayout.Label($"笔刷大小: {brushSize:F1}");
        brushStrength = GUILayout.HorizontalSlider(brushStrength, 0f, 1f);
        GUILayout.Label($"笔刷强度: {brushStrength:F2}");
        brushOpacity = GUILayout.HorizontalSlider(brushOpacity, 0f, 1f);
        GUILayout.Label($"笔刷透明度: {brushOpacity:F2}");
        
        // 工具选择按钮
        GUILayout.Space(10);
        GUILayout.Label("工具选择", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("升高")) SetTerrainTool(TerrainToolType.Raise);
        if (GUILayout.Button("降低")) SetTerrainTool(TerrainToolType.Lower);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("平滑")) SetTerrainTool(TerrainToolType.Smooth);
        if (GUILayout.Button("平整")) SetTerrainTool(TerrainToolType.Flatten);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("绘制")) SetTerrainTool(TerrainToolType.Paint);
        
        // 地形状态
        GUILayout.Space(10);
        GUILayout.Label("地形状态", EditorStyles.boldLabel);
        GUILayout.Label($"状态: {terrainToolsState}");
        GUILayout.Label($"编辑模式: {currentEditingMode}");
        GUILayout.Label($"地形大小: {terrainSize}");
        GUILayout.Label($"地形位置: {terrainPosition}");
        GUILayout.Label($"顶点数: {totalVertices}");
        GUILayout.Label($"三角形数: {totalTriangles}");
        GUILayout.Label($"内存使用: {memoryUsage:F2}MB");
        
        // 操作按钮
        GUILayout.Space(10);
        GUILayout.Label("操作", EditorStyles.boldLabel);
        if (GUILayout.Button("生成报告")) GenerateTerrainReport();
        if (GUILayout.Button("导出数据")) ExportTerrainData();
        if (GUILayout.Button("重置地形")) InitializeTerrainData();
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        if (terrainData != null)
        {
            DestroyImmediate(terrainData);
        }
    }
}

/// <summary>
/// 地形工具类型枚举
/// </summary>
public enum TerrainToolType
{
    None,
    Raise,
    Lower,
    Smooth,
    Flatten,
    Paint
}

/// <summary>
/// 地形工具基类
/// </summary>
public abstract class TerrainTool
{
    public abstract void Apply(TerrainData terrainData, Vector3 worldPosition, float strength);
}

/// <summary>
/// 升高地形工具
/// </summary>
public class RaiseTerrainTool : TerrainTool
{
    public override void Apply(TerrainData terrainData, Vector3 worldPosition, float strength)
    {
        // 实现升高地形逻辑
    }
}

/// <summary>
/// 降低地形工具
/// </summary>
public class LowerTerrainTool : TerrainTool
{
    public override void Apply(TerrainData terrainData, Vector3 worldPosition, float strength)
    {
        // 实现降低地形逻辑
    }
}

/// <summary>
/// 平滑地形工具
/// </summary>
public class SmoothTerrainTool : TerrainTool
{
    public override void Apply(TerrainData terrainData, Vector3 worldPosition, float strength)
    {
        // 实现平滑地形逻辑
    }
}

/// <summary>
/// 平整地形工具
/// </summary>
public class FlattenTerrainTool : TerrainTool
{
    public override void Apply(TerrainData terrainData, Vector3 worldPosition, float strength)
    {
        // 实现平整地形逻辑
    }
}

/// <summary>
/// 绘制地形工具
/// </summary>
public class PaintTerrainTool : TerrainTool
{
    public override void Apply(TerrainData terrainData, Vector3 worldPosition, float strength)
    {
        // 实现绘制地形逻辑
    }
}

/// <summary>
/// 细节实例结构
/// </summary>
[System.Serializable]
public struct DetailInstance
{
    public Vector3 position;
    public float rotation;
    public float scale;
    public Color color;
}

/// <summary>
/// 地形数据报告
/// </summary>
[System.Serializable]
public class TerrainData
{
    public string timestamp;
    public string terrainToolsState;
    public string currentEditingMode;
    public Vector3 terrainSize;
    public Vector3 terrainPosition;
    public int totalVertices;
    public int totalTriangles;
    public float memoryUsage;
    public float brushSize;
    public float brushStrength;
    public string currentTool;
    public int activeTextureIndex;
    public int activeTreeIndex;
    public int activeDetailIndex;
} 