using UnityEngine;
using UnityEngine.TerrainUtils;
using System.Collections.Generic;

namespace UnityEditor.Chapter8.TerrainUtils
{
    /// <summary>
    /// UnityEngine.TerrainUtils 地形工具系统案例
    /// 演示地形生成、地形修改、地形数据操作等功能
    /// </summary>
    public class TerrainUtilsExample : MonoBehaviour
    {
        [Header("地形设置")]
        [SerializeField] private Terrain terrain;
        [SerializeField] private TerrainData terrainData;
        [SerializeField] private bool autoCreateTerrain = true;
        
        [Header("地形尺寸")]
        [SerializeField] private int terrainWidth = 513;
        [SerializeField] private int terrainHeight = 513;
        [SerializeField] private int terrainLength = 513;
        [SerializeField] private float terrainScale = 1f;
        
        [Header("高度图设置")]
        [SerializeField] private float[,] heightMap;
        [SerializeField] private float maxHeight = 100f;
        [SerializeField] private float noiseScale = 0.01f;
        [SerializeField] private int noiseOctaves = 4;
        
        [Header("地形生成")]
        [SerializeField] private bool usePerlinNoise = true;
        [SerializeField] private bool useFractalNoise = false;
        [SerializeField] private bool useVoronoiNoise = false;
        [SerializeField] private float noiseAmplitude = 1f;
        
        [Header("地形修改")]
        [SerializeField] private float brushSize = 10f;
        [SerializeField] private float brushStrength = 1f;
        [SerializeField] private Vector3 brushPosition = Vector3.zero;
        [SerializeField] private bool isModifying = false;
        
        [Header("纹理设置")]
        [SerializeField] private List<TerrainLayer> terrainLayers = new List<TerrainLayer>();
        [SerializeField] private int currentLayerIndex = 0;
        
        [Header("UI控制")]
        [SerializeField] private bool showControls = true;
        
        private Vector3 lastMousePosition;
        private bool isMouseDown = false;
        
        private void Start()
        {
            InitializeTerrain();
        }
        
        /// <summary>
        /// 初始化地形
        /// </summary>
        private void InitializeTerrain()
        {
            if (autoCreateTerrain)
            {
                CreateTerrain();
            }
            else
            {
                // 获取现有地形
                terrain = GetComponent<Terrain>();
                if (terrain != null)
                {
                    terrainData = terrain.terrainData;
                }
            }
            
            if (terrainData != null)
            {
                heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            }
        }
        
        /// <summary>
        /// 创建地形
        /// </summary>
        public void CreateTerrain()
        {
            // 创建TerrainData
            terrainData = new TerrainData();
            terrainData.heightmapResolution = terrainWidth;
            terrainData.size = new Vector3(terrainLength, maxHeight, terrainHeight);
            
            // 创建Terrain
            if (terrain == null)
            {
                terrain = gameObject.AddComponent<Terrain>();
            }
            
            terrain.terrainData = terrainData;
            terrain.transform.position = Vector3.zero;
            terrain.transform.localScale = Vector3.one * terrainScale;
            
            // 初始化高度图
            heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
            
            Debug.Log($"创建地形: {terrainWidth} x {terrainHeight} x {terrainLength}");
        }
        
        /// <summary>
        /// 生成Perlin噪声地形
        /// </summary>
        public void GeneratePerlinNoise()
        {
            if (terrainData == null) return;
            
            int resolution = terrainData.heightmapResolution;
            heightMap = new float[resolution, resolution];
            
            for (int x = 0; x < resolution; x++)
            {
                for (int y = 0; y < resolution; y++)
                {
                    float xCoord = x * noiseScale;
                    float yCoord = y * noiseScale;
                    float height = Mathf.PerlinNoise(xCoord, yCoord) * noiseAmplitude;
                    heightMap[x, y] = height;
                }
            }
            
            terrainData.SetHeights(0, 0, heightMap);
            Debug.Log("生成Perlin噪声地形");
        }
        
        /// <summary>
        /// 生成分形噪声地形
        /// </summary>
        public void GenerateFractalNoise()
        {
            if (terrainData == null) return;
            
            int resolution = terrainData.heightmapResolution;
            heightMap = new float[resolution, resolution];
            
            for (int x = 0; x < resolution; x++)
            {
                for (int y = 0; y < resolution; y++)
                {
                    float height = 0f;
                    float amplitude = noiseAmplitude;
                    float frequency = noiseScale;
                    
                    for (int i = 0; i < noiseOctaves; i++)
                    {
                        float xCoord = x * frequency;
                        float yCoord = y * frequency;
                        height += Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
                        
                        amplitude *= 0.5f;
                        frequency *= 2f;
                    }
                    
                    heightMap[x, y] = height;
                }
            }
            
            terrainData.SetHeights(0, 0, heightMap);
            Debug.Log("生成分形噪声地形");
        }
        
        /// <summary>
        /// 生成Voronoi噪声地形
        /// </summary>
        public void GenerateVoronoiNoise()
        {
            if (terrainData == null) return;
            
            int resolution = terrainData.heightmapResolution;
            heightMap = new float[resolution, resolution];
            
            Vector2[] points = new Vector2[10];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
            }
            
            for (int x = 0; x < resolution; x++)
            {
                for (int y = 0; y < resolution; y++)
                {
                    float minDistance = float.MaxValue;
                    Vector2 currentPoint = new Vector2((float)x / resolution, (float)y / resolution);
                    
                    foreach (Vector2 point in points)
                    {
                        float distance = Vector2.Distance(currentPoint, point);
                        minDistance = Mathf.Min(minDistance, distance);
                    }
                    
                    heightMap[x, y] = minDistance * noiseAmplitude;
                }
            }
            
            terrainData.SetHeights(0, 0, heightMap);
            Debug.Log("生成Voronoi噪声地形");
        }
        
        /// <summary>
        /// 平滑地形
        /// </summary>
        public void SmoothTerrain()
        {
            if (terrainData == null) return;
            
            int resolution = terrainData.heightmapResolution;
            float[,] smoothedHeights = new float[resolution, resolution];
            
            for (int x = 1; x < resolution - 1; x++)
            {
                for (int y = 1; y < resolution - 1; y++)
                {
                    float sum = 0f;
                    int count = 0;
                    
                    // 3x3 平滑
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            sum += heightMap[x + dx, y + dy];
                            count++;
                        }
                    }
                    
                    smoothedHeights[x, y] = sum / count;
                }
            }
            
            heightMap = smoothedHeights;
            terrainData.SetHeights(0, 0, heightMap);
            Debug.Log("地形已平滑");
        }
        
        /// <summary>
        /// 应用高度修改
        /// </summary>
        public void ApplyHeightModification(Vector3 worldPosition, float strength, float size)
        {
            if (terrainData == null) return;
            
            Vector3 terrainPosition = terrain.transform.position;
            Vector3 localPosition = worldPosition - terrainPosition;
            
            int x = Mathf.RoundToInt((localPosition.x / terrainData.size.x) * terrainData.heightmapResolution);
            int z = Mathf.RoundToInt((localPosition.z / terrainData.size.z) * terrainData.heightmapResolution);
            
            int brushRadius = Mathf.RoundToInt(size);
            
            for (int dx = -brushRadius; dx <= brushRadius; dx++)
            {
                for (int dz = -brushRadius; dz <= brushRadius; dz++)
                {
                    int sampleX = x + dx;
                    int sampleZ = z + dz;
                    
                    if (sampleX >= 0 && sampleX < terrainData.heightmapResolution &&
                        sampleZ >= 0 && sampleZ < terrainData.heightmapResolution)
                    {
                        float distance = Mathf.Sqrt(dx * dx + dz * dz);
                        if (distance <= brushRadius)
                        {
                            float falloff = 1f - (distance / brushRadius);
                            float modification = strength * falloff;
                            
                            heightMap[sampleX, sampleZ] += modification;
                            heightMap[sampleX, sampleZ] = Mathf.Clamp01(heightMap[sampleX, sampleZ]);
                        }
                    }
                }
            }
            
            terrainData.SetHeights(0, 0, heightMap);
        }
        
        /// <summary>
        /// 添加地形层
        /// </summary>
        public void AddTerrainLayer(TerrainLayer layer)
        {
            if (terrainData != null && layer != null)
            {
                TerrainLayer[] layers = terrainData.terrainLayers;
                System.Array.Resize(ref layers, layers.Length + 1);
                layers[layers.Length - 1] = layer;
                terrainData.terrainLayers = layers;
                
                Debug.Log($"添加地形层: {layer.name}");
            }
        }
        
        /// <summary>
        /// 移除地形层
        /// </summary>
        public void RemoveTerrainLayer(int index)
        {
            if (terrainData != null && index >= 0 && index < terrainData.terrainLayers.Length)
            {
                TerrainLayer[] layers = terrainData.terrainLayers;
                for (int i = index; i < layers.Length - 1; i++)
                {
                    layers[i] = layers[i + 1];
                }
                System.Array.Resize(ref layers, layers.Length - 1);
                terrainData.terrainLayers = layers;
                
                Debug.Log($"移除地形层: {index}");
            }
        }
        
        /// <summary>
        /// 获取地形高度
        /// </summary>
        public float GetTerrainHeight(Vector3 worldPosition)
        {
            if (terrain != null)
            {
                return terrain.SampleHeight(worldPosition);
            }
            return 0f;
        }
        
        /// <summary>
        /// 设置地形高度
        /// </summary>
        public void SetTerrainHeight(Vector3 worldPosition, float height)
        {
            if (terrainData == null) return;
            
            Vector3 terrainPosition = terrain.transform.position;
            Vector3 localPosition = worldPosition - terrainPosition;
            
            int x = Mathf.RoundToInt((localPosition.x / terrainData.size.x) * terrainData.heightmapResolution);
            int z = Mathf.RoundToInt((localPosition.z / terrainData.size.z) * terrainData.heightmapResolution);
            
            if (x >= 0 && x < terrainData.heightmapResolution &&
                z >= 0 && z < terrainData.heightmapResolution)
            {
                heightMap[x, z] = Mathf.Clamp01(height / maxHeight);
                terrainData.SetHeights(0, 0, heightMap);
            }
        }
        
        /// <summary>
        /// 重置地形
        /// </summary>
        public void ResetTerrain()
        {
            if (terrainData != null)
            {
                int resolution = terrainData.heightmapResolution;
                heightMap = new float[resolution, resolution];
                terrainData.SetHeights(0, 0, heightMap);
                Debug.Log("地形已重置");
            }
        }
        
        /// <summary>
        /// 导出高度图
        /// </summary>
        public void ExportHeightmap()
        {
            if (terrainData == null) return;
            
            int resolution = terrainData.heightmapResolution;
            Texture2D heightmapTexture = new Texture2D(resolution, resolution, TextureFormat.RFloat, false);
            
            for (int x = 0; x < resolution; x++)
            {
                for (int y = 0; y < resolution; y++)
                {
                    float height = heightMap[x, y];
                    heightmapTexture.SetPixel(x, y, new Color(height, height, height, 1f));
                }
            }
            
            heightmapTexture.Apply();
            
            byte[] bytes = heightmapTexture.EncodeToPNG();
            string filename = $"Heightmap_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + filename, bytes);
            
            Debug.Log($"高度图已导出: {filename}");
            DestroyImmediate(heightmapTexture);
        }
        
        /// <summary>
        /// 获取地形信息
        /// </summary>
        public void GetTerrainInfo()
        {
            if (terrainData != null)
            {
                Debug.Log("=== 地形信息 ===");
                Debug.Log($"尺寸: {terrainData.size}");
                Debug.Log($"高度图分辨率: {terrainData.heightmapResolution}");
                Debug.Log($"地形层数量: {terrainData.terrainLayers.Length}");
                Debug.Log($"最大高度: {maxHeight}");
                Debug.Log($"地形缩放: {terrainScale}");
            }
        }
        
        private void Update()
        {
            HandleMouseInput();
        }
        
        /// <summary>
        /// 处理鼠标输入
        /// </summary>
        private void HandleMouseInput()
        {
            if (Camera.main == null || !isModifying) return;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<Terrain>() == terrain)
                {
                    brushPosition = hit.point;
                    
                    if (Input.GetMouseButton(0))
                    {
                        ApplyHeightModification(hit.point, brushStrength, brushSize);
                    }
                }
            }
        }
        
        private void OnGUI()
        {
            if (!showControls) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 400, 700));
            GUILayout.Label("UnityEngine.TerrainUtils 地形工具系统案例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            // 地形创建
            GUILayout.Label("地形创建", EditorStyles.boldLabel);
            if (GUILayout.Button("创建地形"))
            {
                CreateTerrain();
            }
            
            GUILayout.Space(10);
            
            // 地形生成
            GUILayout.Label("地形生成", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Perlin噪声")) GeneratePerlinNoise();
            if (GUILayout.Button("分形噪声")) GenerateFractalNoise();
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("Voronoi噪声"))
            {
                GenerateVoronoiNoise();
            }
            
            if (GUILayout.Button("平滑地形"))
            {
                SmoothTerrain();
            }
            
            if (GUILayout.Button("重置地形"))
            {
                ResetTerrain();
            }
            
            GUILayout.Space(10);
            
            // 地形修改
            GUILayout.Label("地形修改", EditorStyles.boldLabel);
            isModifying = GUILayout.Toggle(isModifying, "启用地形修改");
            
            float newBrushSize = GUILayout.HorizontalSlider(brushSize, 1f, 50f);
            if (Mathf.Abs(newBrushSize - brushSize) > 0.1f)
            {
                brushSize = newBrushSize;
            }
            GUILayout.Label($"画笔大小: {brushSize:F1}");
            
            float newBrushStrength = GUILayout.HorizontalSlider(brushStrength, 0.01f, 1f);
            if (Mathf.Abs(newBrushStrength - brushStrength) > 0.01f)
            {
                brushStrength = newBrushStrength;
            }
            GUILayout.Label($"画笔强度: {brushStrength:F2}");
            
            GUILayout.Space(10);
            
            // 地形设置
            GUILayout.Label("地形设置", EditorStyles.boldLabel);
            
            Vector3Int newTerrainSize = new Vector3Int(terrainWidth, terrainHeight, terrainLength);
            newTerrainSize.x = (int)GUILayout.HorizontalSlider(newTerrainSize.x, 129, 1025);
            newTerrainSize.y = (int)GUILayout.HorizontalSlider(newTerrainSize.y, 129, 1025);
            newTerrainSize.z = (int)GUILayout.HorizontalSlider(newTerrainSize.z, 129, 1025);
            GUILayout.Label($"地形尺寸: {newTerrainSize.x} x {newTerrainSize.y} x {newTerrainSize.z}");
            
            if (newTerrainSize != new Vector3Int(terrainWidth, terrainHeight, terrainLength))
            {
                terrainWidth = newTerrainSize.x;
                terrainHeight = newTerrainSize.y;
                terrainLength = newTerrainSize.z;
            }
            
            float newMaxHeight = GUILayout.HorizontalSlider(maxHeight, 10f, 1000f);
            if (Mathf.Abs(newMaxHeight - maxHeight) > 1f)
            {
                maxHeight = newMaxHeight;
            }
            GUILayout.Label($"最大高度: {maxHeight:F1}");
            
            float newNoiseScale = GUILayout.HorizontalSlider(noiseScale, 0.001f, 0.1f);
            if (Mathf.Abs(newNoiseScale - noiseScale) > 0.001f)
            {
                noiseScale = newNoiseScale;
            }
            GUILayout.Label($"噪声缩放: {noiseScale:F3}");
            
            float newNoiseAmplitude = GUILayout.HorizontalSlider(noiseAmplitude, 0.1f, 2f);
            if (Mathf.Abs(newNoiseAmplitude - noiseAmplitude) > 0.01f)
            {
                noiseAmplitude = newNoiseAmplitude;
            }
            GUILayout.Label($"噪声幅度: {noiseAmplitude:F2}");
            
            int newOctaves = (int)GUILayout.HorizontalSlider(noiseOctaves, 1, 8);
            if (newOctaves != noiseOctaves)
            {
                noiseOctaves = newOctaves;
            }
            GUILayout.Label($"噪声八度: {noiseOctaves}");
            
            GUILayout.Space(10);
            
            // 功能按钮
            GUILayout.Label("功能", EditorStyles.boldLabel);
            if (GUILayout.Button("导出高度图"))
            {
                ExportHeightmap();
            }
            
            if (GUILayout.Button("获取地形信息"))
            {
                GetTerrainInfo();
            }
            
            GUILayout.Space(10);
            
            // 信息显示
            GUILayout.Label("信息", EditorStyles.boldLabel);
            GUILayout.Label($"画笔位置: {brushPosition}");
            GUILayout.Label($"修改模式: {(isModifying ? "开启" : "关闭")}");
            
            if (terrainData != null)
            {
                GUILayout.Label($"地形层数量: {terrainData.terrainLayers.Length}");
            }
            
            GUILayout.Space(10);
            
            // 操作提示
            GUILayout.Label("操作提示", EditorStyles.boldLabel);
            GUILayout.Label("启用地形修改后，按住鼠标左键");
            GUILayout.Label("在地形上拖拽即可修改地形高度");
            
            GUILayout.EndArea();
        }
        
        private void OnDrawGizmos()
        {
            if (isModifying)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(brushPosition, brushSize);
            }
        }
    }
} 