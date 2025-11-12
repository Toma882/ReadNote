using UnityEngine;
using UnityEditor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// TerrainUtility 工具类示例
    /// 提供地形相关的实用工具功能
    /// </summary>
    public static class TerrainUtilityExample
    {
        #region 地形连接示例

        /// <summary>
        /// 自动连接地形
        /// </summary>
        public static void AutoConnectExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            if (terrains.Length < 2)
            {
                Debug.LogWarning("需要至少2个地形才能进行自动连接");
                return;
            }

            // 自动连接地形
            TerrainUtility.AutoConnect(terrains);
            Debug.Log($"已自动连接 {terrains.Length} 个地形");
        }

        /// <summary>
        /// 自动连接指定地形
        /// </summary>
        public static void AutoConnectSpecificTerrainsExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            if (terrains.Length >= 2)
            {
                // 选择前两个地形进行连接
                Terrain[] selectedTerrains = { terrains[0], terrains[1] };
                TerrainUtility.AutoConnect(selectedTerrains);
                Debug.Log($"已连接地形: {terrains[0].name} 和 {terrains[1].name}");
            }
        }

        #endregion

        #region 地形验证示例

        /// <summary>
        /// 有效地形检查
        /// </summary>
        public static void ValidTerrainsCheckExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            if (terrains.Length == 0)
            {
                Debug.LogWarning("场景中没有找到地形");
                return;
            }

            // 检查有效地形
            bool isValid = TerrainUtility.ValidTerrainsCheck(terrains);
            Debug.Log($"地形验证结果: {(isValid ? "有效" : "无效")}");
            
            if (!isValid)
            {
                Debug.LogWarning("发现无效地形，请检查地形设置");
            }
        }

        /// <summary>
        /// 单个地形验证
        /// </summary>
        public static void SingleTerrainValidationExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            Terrain[] singleTerrain = { selectedTerrain };
            bool isValid = TerrainUtility.ValidTerrainsCheck(singleTerrain);
            
            Debug.Log($"地形 {selectedTerrain.name} 验证结果: {(isValid ? "有效" : "无效")}");
        }

        #endregion

        #region 地形信息示例

        /// <summary>
        /// 获取地形信息
        /// </summary>
        public static void GetTerrainInfoExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            Debug.Log($"场景中地形数量: {terrains.Length}");
            
            foreach (Terrain terrain in terrains)
            {
                TerrainData terrainData = terrain.terrainData;
                Debug.Log($"地形: {terrain.name}");
                Debug.Log($"- 分辨率: {terrainData.heightmapResolution}");
                Debug.Log($"- 大小: {terrainData.size}");
                Debug.Log($"- 高度范围: {terrainData.heightmapScale}");
            }
        }

        /// <summary>
        /// 地形设置检查
        /// </summary>
        public static void TerrainSettingsCheckExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            foreach (Terrain terrain in terrains)
            {
                TerrainData terrainData = terrain.terrainData;
                
                Debug.Log($"地形 {terrain.name} 设置:");
                Debug.Log($"- 高度图分辨率: {terrainData.heightmapResolution}");
                Debug.Log($"- 细节分辨率: {terrainData.detailResolution}");
                Debug.Log($"- 控制纹理分辨率: {terrainData.alphamapResolution}");
                Debug.Log($"- 基础纹理分辨率: {terrainData.baseMapResolution}");
            }
        }

        #endregion

        #region 地形操作示例

        /// <summary>
        /// 重置地形
        /// </summary>
        public static void ResetTerrainExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 重置高度图
            float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
            terrainData.SetHeights(0, 0, heights);
            
            Debug.Log($"地形 {selectedTerrain.name} 已重置");
        }

        /// <summary>
        /// 设置地形高度
        /// </summary>
        public static void SetTerrainHeightExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 设置中心高度
            int centerX = terrainData.heightmapResolution / 2;
            int centerY = terrainData.heightmapResolution / 2;
            
            float[,] heights = terrainData.GetHeights(centerX - 10, centerY - 10, 20, 20);
            
            // 创建小山丘
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(10, 10));
                    heights[x, y] = Mathf.Max(0, 0.1f - distance * 0.01f);
                }
            }
            
            terrainData.SetHeights(centerX - 10, centerY - 10, heights);
            Debug.Log($"地形 {selectedTerrain.name} 高度已设置");
        }

        #endregion

        #region 地形纹理示例

        /// <summary>
        /// 设置地形纹理
        /// </summary>
        public static void SetTerrainTexturesExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 创建纹理数组
            TerrainLayer[] terrainLayers = new TerrainLayer[2];
            
            // 草地纹理
            terrainLayers[0] = new TerrainLayer();
            terrainLayers[0].diffuseTexture = CreateTestTexture("Grass", Color.green);
            
            // 石头纹理
            terrainLayers[1] = new TerrainLayer();
            terrainLayers[1].diffuseTexture = CreateTestTexture("Rock", Color.gray);
            
            terrainData.terrainLayers = terrainLayers;
            
            Debug.Log($"地形 {selectedTerrain.name} 纹理已设置");
        }

        /// <summary>
        /// 绘制地形纹理
        /// </summary>
        public static void PaintTerrainTexturesExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            if (terrainData.terrainLayers.Length == 0)
            {
                Debug.LogWarning("地形没有设置纹理层");
                return;
            }

            // 绘制纹理
            int[,] alphamap = new int[terrainData.alphamapResolution, terrainData.alphamapResolution];
            
            // 在中心区域绘制第一个纹理
            int centerX = terrainData.alphamapResolution / 2;
            int centerY = terrainData.alphamapResolution / 2;
            int radius = terrainData.alphamapResolution / 4;
            
            for (int x = 0; x < terrainData.alphamapResolution; x++)
            {
                for (int y = 0; y < terrainData.alphamapResolution; y++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                    if (distance < radius)
                    {
                        alphamap[x, y] = 0; // 第一个纹理
                    }
                    else
                    {
                        alphamap[x, y] = 1; // 第二个纹理
                    }
                }
            }
            
            Debug.Log($"地形 {selectedTerrain.name} 纹理绘制完成");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建地形测试场景
        /// </summary>
        public static void CreateTerrainTestSceneExample()
        {
            // 创建地形数据
            TerrainData terrainData = new TerrainData();
            terrainData.size = new Vector3(100, 30, 100);
            terrainData.heightmapResolution = 513;
            
            // 创建地形
            GameObject terrainObj = Terrain.CreateTerrainGameObject(terrainData);
            terrainObj.name = "TestTerrain";
            
            Terrain terrain = terrainObj.GetComponent<Terrain>();
            
            // 验证地形
            Terrain[] terrains = { terrain };
            bool isValid = TerrainUtility.ValidTerrainsCheck(terrains);
            
            Debug.Log($"测试地形创建: {(isValid ? "成功" : "失败")}");
            Debug.Log($"地形名称: {terrain.name}");
            Debug.Log($"地形大小: {terrainData.size}");
        }

        /// <summary>
        /// 批量地形处理
        /// </summary>
        public static void BatchTerrainProcessingExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            if (terrains.Length == 0)
            {
                Debug.LogWarning("场景中没有地形");
                return;
            }

            // 验证所有地形
            bool allValid = TerrainUtility.ValidTerrainsCheck(terrains);
            Debug.Log($"所有地形验证: {(allValid ? "通过" : "失败")}");
            
            // 自动连接地形
            if (terrains.Length > 1)
            {
                TerrainUtility.AutoConnect(terrains);
                Debug.Log($"已连接 {terrains.Length} 个地形");
            }
            
            // 处理每个地形
            foreach (Terrain terrain in terrains)
            {
                Debug.Log($"处理地形: {terrain.name}");
                // 这里可以添加具体的地形处理逻辑
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取选中的地形
        /// </summary>
        private static Terrain GetSelectedTerrain()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                Terrain terrain = selected.GetComponent<Terrain>();
                if (terrain != null)
                {
                    return terrain;
                }
            }

            // 查找场景中的第一个地形
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            if (terrains.Length > 0)
            {
                return terrains[0];
            }

            return null;
        }

        /// <summary>
        /// 创建测试纹理
        /// </summary>
        private static Texture2D CreateTestTexture(string name, Color color)
        {
            Texture2D texture = new Texture2D(64, 64);
            Color[] pixels = new Color[64 * 64];
            
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }
            
            texture.SetPixels(pixels);
            texture.Apply();
            texture.name = name;
            
            return texture;
        }

        #endregion
    }
}
