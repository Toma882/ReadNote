using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace UnityEditor.Chapter8.Tilemaps
{
    /// <summary>
    /// UnityEngine.Tilemaps 瓦片地图系统案例
    /// 演示Tilemap、Tile、TileBase、瓦片地图操作等功能
    /// </summary>
    public class TilemapsExample : MonoBehaviour
    {
        [Header("瓦片地图设置")]
        [SerializeField] private Grid grid;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TilemapRenderer tilemapRenderer;
        
        [Header("瓦片资源")]
        [SerializeField] private List<TileBase> tiles = new List<TileBase>();
        [SerializeField] private TileBase currentTile;
        [SerializeField] private int currentTileIndex = 0;
        
        [Header("地图操作")]
        [SerializeField] private Vector3Int mapSize = new Vector3Int(20, 20, 1);
        [SerializeField] private Vector3Int currentPosition = Vector3Int.zero;
        [SerializeField] private bool showGrid = true;
        [SerializeField] private bool showTilemap = true;
        
        [Header("绘制设置")]
        [SerializeField] private bool isDrawing = false;
        [SerializeField] private bool isErasing = false;
        [SerializeField] private Vector3Int brushSize = Vector3Int.one;
        
        [Header("地图生成")]
        [SerializeField] private bool autoGenerate = false;
        [SerializeField] private float noiseScale = 0.1f;
        [SerializeField] private float noiseThreshold = 0.5f;
        
        [Header("UI控制")]
        [SerializeField] private bool showControls = true;
        
        private Vector3Int lastMousePosition;
        private bool isMouseDown = false;
        
        private void Start()
        {
            InitializeTilemapComponents();
        }
        
        /// <summary>
        /// 初始化瓦片地图组件
        /// </summary>
        private void InitializeTilemapComponents()
        {
            // 获取或创建Grid
            if (grid == null)
            {
                grid = GetComponent<Grid>();
                if (grid == null)
                {
                    grid = gameObject.AddComponent<Grid>();
                }
            }
            
            // 获取或创建Tilemap
            if (tilemap == null)
            {
                tilemap = GetComponentInChildren<Tilemap>();
                if (tilemap == null)
                {
                    GameObject tilemapObject = new GameObject("Tilemap");
                    tilemapObject.transform.SetParent(transform);
                    tilemap = tilemapObject.AddComponent<Tilemap>();
                    tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
                }
            }
            
            // 获取TilemapRenderer
            if (tilemapRenderer == null)
            {
                tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
            }
            
            // 设置初始属性
            SetupGrid();
            SetupTilemap();
            
            // 设置当前瓦片
            if (tiles.Count > 0)
            {
                currentTile = tiles[0];
            }
        }
        
        /// <summary>
        /// 设置Grid属性
        /// </summary>
        private void SetupGrid()
        {
            if (grid != null)
            {
                grid.cellSize = Vector3.one;
                grid.cellGap = Vector3.zero;
                grid.cellSwizzle = GridLayout.CellSwizzle.XYZ;
            }
        }
        
        /// <summary>
        /// 设置Tilemap属性
        /// </summary>
        private void SetupTilemap()
        {
            if (tilemap != null)
            {
                tilemap.tileAnchor = Vector3.zero;
                tilemap.orientation = Tilemap.Orientation.XY;
            }
            
            if (tilemapRenderer != null)
            {
                tilemapRenderer.sortingOrder = 0;
                tilemapRenderer.mode = TilemapRenderer.Mode.Chunk;
            }
        }
        
        /// <summary>
        /// 在指定位置放置瓦片
        /// </summary>
        public void PlaceTile(Vector3Int position, TileBase tile)
        {
            if (tilemap != null && tile != null)
            {
                tilemap.SetTile(position, tile);
                Debug.Log($"在位置 {position} 放置瓦片: {tile.name}");
            }
        }
        
        /// <summary>
        /// 在指定位置移除瓦片
        /// </summary>
        public void RemoveTile(Vector3Int position)
        {
            if (tilemap != null)
            {
                tilemap.SetTile(position, null);
                Debug.Log($"移除位置 {position} 的瓦片");
            }
        }
        
        /// <summary>
        /// 获取指定位置的瓦片
        /// </summary>
        public TileBase GetTile(Vector3Int position)
        {
            if (tilemap != null)
            {
                return tilemap.GetTile(position);
            }
            return null;
        }
        
        /// <summary>
        /// 清除整个地图
        /// </summary>
        public void ClearMap()
        {
            if (tilemap != null)
            {
                tilemap.ClearAllTiles();
                Debug.Log("地图已清空");
            }
        }
        
        /// <summary>
        /// 填充矩形区域
        /// </summary>
        public void FillRectangle(Vector3Int start, Vector3Int end, TileBase tile)
        {
            if (tilemap != null && tile != null)
            {
                Vector3Int min = Vector3Int.Min(start, end);
                Vector3Int max = Vector3Int.Max(start, end);
                
                for (int x = min.x; x <= max.x; x++)
                {
                    for (int y = min.y; y <= max.y; y++)
                    {
                        for (int z = min.z; z <= max.z; z++)
                        {
                            tilemap.SetTile(new Vector3Int(x, y, z), tile);
                        }
                    }
                }
                
                Debug.Log($"填充矩形区域: {min} 到 {max}");
            }
        }
        
        /// <summary>
        /// 绘制圆形区域
        /// </summary>
        public void FillCircle(Vector3Int center, int radius, TileBase tile)
        {
            if (tilemap != null && tile != null)
            {
                for (int x = center.x - radius; x <= center.x + radius; x++)
                {
                    for (int y = center.y - radius; y <= center.y + radius; y++)
                    {
                        Vector3Int pos = new Vector3Int(x, y, center.z);
                        float distance = Vector3Int.Distance(center, pos);
                        
                        if (distance <= radius)
                        {
                            tilemap.SetTile(pos, tile);
                        }
                    }
                }
                
                Debug.Log($"绘制圆形区域: 中心 {center}, 半径 {radius}");
            }
        }
        
        /// <summary>
        /// 生成随机地图
        /// </summary>
        public void GenerateRandomMap()
        {
            if (tilemap != null && tiles.Count > 0)
            {
                ClearMap();
                
                for (int x = 0; x < mapSize.x; x++)
                {
                    for (int y = 0; y < mapSize.y; y++)
                    {
                        Vector3Int pos = new Vector3Int(x, y, 0);
                        int randomIndex = Random.Range(0, tiles.Count);
                        tilemap.SetTile(pos, tiles[randomIndex]);
                    }
                }
                
                Debug.Log($"生成随机地图: {mapSize.x} x {mapSize.y}");
            }
        }
        
        /// <summary>
        /// 使用噪声生成地图
        /// </summary>
        public void GenerateNoiseMap()
        {
            if (tilemap != null && tiles.Count >= 2)
            {
                ClearMap();
                
                for (int x = 0; x < mapSize.x; x++)
                {
                    for (int y = 0; y < mapSize.y; y++)
                    {
                        Vector3Int pos = new Vector3Int(x, y, 0);
                        float noise = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);
                        
                        TileBase tile = noise > noiseThreshold ? tiles[0] : tiles[1];
                        tilemap.SetTile(pos, tile);
                    }
                }
                
                Debug.Log($"使用噪声生成地图: 阈值 {noiseThreshold}");
            }
        }
        
        /// <summary>
        /// 切换瓦片
        /// </summary>
        public void NextTile()
        {
            if (tiles.Count > 0)
            {
                currentTileIndex = (currentTileIndex + 1) % tiles.Count;
                currentTile = tiles[currentTileIndex];
                Debug.Log($"切换到瓦片: {currentTileIndex + 1}/{tiles.Count}");
            }
        }
        
        /// <summary>
        /// 上一个瓦片
        /// </summary>
        public void PreviousTile()
        {
            if (tiles.Count > 0)
            {
                currentTileIndex = (currentTileIndex - 1 + tiles.Count) % tiles.Count;
                currentTile = tiles[currentTileIndex];
                Debug.Log($"切换到瓦片: {currentTileIndex + 1}/{tiles.Count}");
            }
        }
        
        /// <summary>
        /// 设置当前瓦片
        /// </summary>
        public void SetCurrentTile(int index)
        {
            if (index >= 0 && index < tiles.Count)
            {
                currentTileIndex = index;
                currentTile = tiles[index];
                Debug.Log($"设置当前瓦片: {index + 1}/{tiles.Count}");
            }
        }
        
        /// <summary>
        /// 切换绘制模式
        /// </summary>
        public void ToggleDrawing()
        {
            isDrawing = !isDrawing;
            isErasing = false;
            Debug.Log($"绘制模式: {(isDrawing ? "开启" : "关闭")}");
        }
        
        /// <summary>
        /// 切换擦除模式
        /// </summary>
        public void ToggleErasing()
        {
            isErasing = !isErasing;
            isDrawing = false;
            Debug.Log($"擦除模式: {(isErasing ? "开启" : "关闭")}");
        }
        
        /// <summary>
        /// 设置画笔大小
        /// </summary>
        public void SetBrushSize(Vector3Int size)
        {
            brushSize = size;
            Debug.Log($"画笔大小设置为: {brushSize}");
        }
        
        /// <summary>
        /// 获取地图信息
        /// </summary>
        public void GetMapInfo()
        {
            if (tilemap != null)
            {
                BoundsInt bounds = tilemap.cellBounds;
                Debug.Log("=== 地图信息 ===");
                Debug.Log($"地图边界: {bounds}");
                Debug.Log($"地图大小: {bounds.size}");
                Debug.Log($"瓦片数量: {tilemap.GetUsedTilesCount()}");
                Debug.Log($"当前瓦片: {(currentTile != null ? currentTile.name : "无")}");
            }
        }
        
        /// <summary>
        /// 保存地图数据
        /// </summary>
        public void SaveMapData()
        {
            if (tilemap != null)
            {
                // 这里可以实现地图数据的序列化保存
                Debug.Log("地图数据保存功能待实现");
            }
        }
        
        /// <summary>
        /// 加载地图数据
        /// </summary>
        public void LoadMapData()
        {
            if (tilemap != null)
            {
                // 这里可以实现地图数据的反序列化加载
                Debug.Log("地图数据加载功能待实现");
            }
        }
        
        /// <summary>
        /// 世界坐标转换为瓦片坐标
        /// </summary>
        public Vector3Int WorldToCell(Vector3 worldPosition)
        {
            if (grid != null)
            {
                return grid.WorldToCell(worldPosition);
            }
            return Vector3Int.zero;
        }
        
        /// <summary>
        /// 瓦片坐标转换为世界坐标
        /// </summary>
        public Vector3 CellToWorld(Vector3Int cellPosition)
        {
            if (grid != null)
            {
                return grid.CellToWorld(cellPosition);
            }
            return Vector3.zero;
        }
        
        private void Update()
        {
            HandleMouseInput();
            
            if (autoGenerate && Input.GetKeyDown(KeyCode.G))
            {
                GenerateRandomMap();
            }
        }
        
        /// <summary>
        /// 处理鼠标输入
        /// </summary>
        private void HandleMouseInput()
        {
            if (Camera.main == null) return;
            
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mouseCellPos = WorldToCell(mouseWorldPos);
            
            // 鼠标按下
            if (Input.GetMouseButtonDown(0))
            {
                isMouseDown = true;
                lastMousePosition = mouseCellPos;
                
                if (isDrawing && currentTile != null)
                {
                    PlaceTile(mouseCellPos, currentTile);
                }
                else if (isErasing)
                {
                    RemoveTile(mouseCellPos);
                }
            }
            
            // 鼠标拖拽
            if (isMouseDown && mouseCellPos != lastMousePosition)
            {
                if (isDrawing && currentTile != null)
                {
                    PlaceTile(mouseCellPos, currentTile);
                }
                else if (isErasing)
                {
                    RemoveTile(mouseCellPos);
                }
                
                lastMousePosition = mouseCellPos;
            }
            
            // 鼠标释放
            if (Input.GetMouseButtonUp(0))
            {
                isMouseDown = false;
            }
            
            currentPosition = mouseCellPos;
        }
        
        private void OnGUI()
        {
            if (!showControls) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 400, 700));
            GUILayout.Label("UnityEngine.Tilemaps 瓦片地图系统案例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            // 瓦片选择
            GUILayout.Label("瓦片选择", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("上一个瓦片")) PreviousTile();
            if (GUILayout.Button("下一个瓦片")) NextTile();
            GUILayout.EndHorizontal();
            
            GUILayout.Label($"当前瓦片: {currentTileIndex + 1}/{tiles.Count}");
            if (currentTile != null)
            {
                GUILayout.Label($"瓦片名称: {currentTile.name}");
            }
            
            GUILayout.Space(10);
            
            // 绘制控制
            GUILayout.Label("绘制控制", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button($"绘制模式: {(isDrawing ? "开启" : "关闭")}"))
            {
                ToggleDrawing();
            }
            if (GUILayout.Button($"擦除模式: {(isErasing ? "开启" : "关闭")}"))
            {
                ToggleErasing();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // 地图操作
            GUILayout.Label("地图操作", EditorStyles.boldLabel);
            if (GUILayout.Button("清空地图"))
            {
                ClearMap();
            }
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("随机生成")) GenerateRandomMap();
            if (GUILayout.Button("噪声生成")) GenerateNoiseMap();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // 区域填充
            GUILayout.Label("区域填充", EditorStyles.boldLabel);
            if (GUILayout.Button("填充矩形区域"))
            {
                Vector3Int start = new Vector3Int(0, 0, 0);
                Vector3Int end = new Vector3Int(5, 5, 0);
                FillRectangle(start, end, currentTile);
            }
            
            if (GUILayout.Button("绘制圆形区域"))
            {
                Vector3Int center = new Vector3Int(10, 10, 0);
                FillCircle(center, 5, currentTile);
            }
            
            GUILayout.Space(10);
            
            // 地图设置
            GUILayout.Label("地图设置", EditorStyles.boldLabel);
            
            Vector3Int newMapSize = mapSize;
            newMapSize.x = (int)GUILayout.HorizontalSlider(newMapSize.x, 5, 50);
            newMapSize.y = (int)GUILayout.HorizontalSlider(newMapSize.y, 5, 50);
            GUILayout.Label($"地图大小: {newMapSize.x} x {newMapSize.y}");
            
            if (newMapSize != mapSize)
            {
                mapSize = newMapSize;
            }
            
            float newNoiseScale = GUILayout.HorizontalSlider(noiseScale, 0.01f, 1f);
            if (Mathf.Abs(newNoiseScale - noiseScale) > 0.01f)
            {
                noiseScale = newNoiseScale;
            }
            GUILayout.Label($"噪声缩放: {noiseScale:F2}");
            
            float newNoiseThreshold = GUILayout.HorizontalSlider(noiseThreshold, 0f, 1f);
            if (Mathf.Abs(newNoiseThreshold - noiseThreshold) > 0.01f)
            {
                noiseThreshold = newNoiseThreshold;
            }
            GUILayout.Label($"噪声阈值: {noiseThreshold:F2}");
            
            GUILayout.Space(10);
            
            // 画笔设置
            GUILayout.Label("画笔设置", EditorStyles.boldLabel);
            Vector3Int newBrushSize = brushSize;
            newBrushSize.x = (int)GUILayout.HorizontalSlider(newBrushSize.x, 1, 10);
            newBrushSize.y = (int)GUILayout.HorizontalSlider(newBrushSize.y, 1, 10);
            GUILayout.Label($"画笔大小: {newBrushSize.x} x {newBrushSize.y}");
            
            if (newBrushSize != brushSize)
            {
                SetBrushSize(newBrushSize);
            }
            
            GUILayout.Space(10);
            
            // 信息显示
            GUILayout.Label("信息", EditorStyles.boldLabel);
            if (GUILayout.Button("获取地图信息"))
            {
                GetMapInfo();
            }
            
            GUILayout.Label($"鼠标位置: {currentPosition}");
            GUILayout.Label($"绘制模式: {(isDrawing ? "绘制" : (isErasing ? "擦除" : "无"))}");
            
            if (tilemap != null)
            {
                GUILayout.Label($"瓦片数量: {tilemap.GetUsedTilesCount()}");
            }
            
            GUILayout.Space(10);
            
            // 快捷键提示
            GUILayout.Label("快捷键", EditorStyles.boldLabel);
            GUILayout.Label("G - 随机生成地图");
            GUILayout.Label("鼠标左键 - 绘制/擦除");
            GUILayout.Label("拖拽 - 连续绘制");
            
            GUILayout.EndArea();
        }
        
        private void OnDrawGizmos()
        {
            if (showGrid && grid != null)
            {
                Gizmos.color = Color.white;
                BoundsInt bounds = new BoundsInt(Vector3Int.zero, mapSize);
                
                for (int x = bounds.min.x; x <= bounds.max.x; x++)
                {
                    for (int y = bounds.min.y; y <= bounds.max.y; y++)
                    {
                        Vector3 worldPos = CellToWorld(new Vector3Int(x, y, 0));
                        Gizmos.DrawWireCube(worldPos + Vector3.one * 0.5f, Vector3.one);
                    }
                }
            }
        }
    }
} 