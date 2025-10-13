using UnityEngine;
using UnityEditor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// TerrainPaintUtility 工具类示例
    /// 提供地形绘制相关的实用工具功能
    /// </summary>
    public static class TerrainPaintUtilityExample
    {
        #region 纹理绘制示例

        /// <summary>
        /// 开始绘制纹理
        /// </summary>
        public static void BeginPaintTextureExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            if (terrainData.terrainLayers.Length == 0)
            {
                Debug.LogWarning("地形没有设置纹理层");
                return;
            }

            // 开始绘制纹理
            var paintContext = TerrainPaintUtility.BeginPaintTexture(terrainData, new Rect(0, 0, 1, 1));
            
            if (paintContext != null)
            {
                Debug.Log("开始绘制纹理成功");
                Debug.Log($"绘制上下文: {paintContext}");
                
                // 结束绘制
                TerrainPaintUtility.EndPaintTexture(paintContext, "Paint Texture");
            }
            else
            {
                Debug.LogWarning("开始绘制纹理失败");
            }
        }

        /// <summary>
        /// 结束绘制纹理
        /// </summary>
        public static void EndPaintTextureExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            if (terrainData.terrainLayers.Length == 0)
            {
                Debug.LogWarning("地形没有设置纹理层");
                return;
            }

            // 开始绘制
            var paintContext = TerrainPaintUtility.BeginPaintTexture(terrainData, new Rect(0, 0, 1, 1));
            
            if (paintContext != null)
            {
                // 结束绘制
                TerrainPaintUtility.EndPaintTexture(paintContext, "End Paint Texture");
                Debug.Log("结束绘制纹理成功");
            }
        }

        #endregion

        #region 高度绘制示例

        /// <summary>
        /// 开始绘制高度
        /// </summary>
        public static void BeginPaintHeightmapExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 开始绘制高度图
            var paintContext = TerrainPaintUtility.BeginPaintHeightmap(terrainData, new Rect(0, 0, 1, 1));
            
            if (paintContext != null)
            {
                Debug.Log("开始绘制高度图成功");
                Debug.Log($"绘制上下文: {paintContext}");
                
                // 结束绘制
                TerrainPaintUtility.EndPaintHeightmap(paintContext, "Paint Heightmap");
            }
            else
            {
                Debug.LogWarning("开始绘制高度图失败");
            }
        }

        /// <summary>
        /// 结束绘制高度图
        /// </summary>
        public static void EndPaintHeightmapExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 开始绘制
            var paintContext = TerrainPaintUtility.BeginPaintHeightmap(terrainData, new Rect(0, 0, 1, 1));
            
            if (paintContext != null)
            {
                // 结束绘制
                TerrainPaintUtility.EndPaintHeightmap(paintContext, "End Paint Heightmap");
                Debug.Log("结束绘制高度图成功");
            }
        }

        #endregion

        #region 细节绘制示例

        /// <summary>
        /// 开始绘制细节
        /// </summary>
        public static void BeginPaintDetailsExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 开始绘制细节
            var paintContext = TerrainPaintUtility.BeginPaintDetails(terrainData, new Rect(0, 0, 1, 1));
            
            if (paintContext != null)
            {
                Debug.Log("开始绘制细节成功");
                Debug.Log($"绘制上下文: {paintContext}");
                
                // 结束绘制
                TerrainPaintUtility.EndPaintDetails(paintContext, "Paint Details");
            }
            else
            {
                Debug.LogWarning("开始绘制细节失败");
            }
        }

        /// <summary>
        /// 结束绘制细节
        /// </summary>
        public static void EndPaintDetailsExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 开始绘制
            var paintContext = TerrainPaintUtility.BeginPaintDetails(terrainData, new Rect(0, 0, 1, 1));
            
            if (paintContext != null)
            {
                // 结束绘制
                TerrainPaintUtility.EndPaintDetails(paintContext, "End Paint Details");
                Debug.Log("结束绘制细节成功");
            }
        }

        #endregion

        #region 绘制上下文示例

        /// <summary>
        /// 获取绘制上下文
        /// </summary>
        public static void GetPaintContextExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 获取纹理绘制上下文
            var textureContext = TerrainPaintUtility.BeginPaintTexture(terrainData, new Rect(0, 0, 1, 1));
            
            if (textureContext != null)
            {
                Debug.Log($"纹理绘制上下文:");
                Debug.Log($"- 目标纹理: {textureContext.destinationRenderTexture}");
                Debug.Log($"- 源纹理: {textureContext.sourceRenderTexture}");
                Debug.Log($"- 像素矩形: {textureContext.pixelRect}");
                
                TerrainPaintUtility.EndPaintTexture(textureContext, "Get Paint Context");
            }
        }

        /// <summary>
        /// 绘制上下文信息
        /// </summary>
        public static void PaintContextInfoExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 获取高度图绘制上下文
            var heightmapContext = TerrainPaintUtility.BeginPaintHeightmap(terrainData, new Rect(0, 0, 1, 1));
            
            if (heightmapContext != null)
            {
                Debug.Log($"高度图绘制上下文:");
                Debug.Log($"- 目标纹理: {heightmapContext.destinationRenderTexture}");
                Debug.Log($"- 源纹理: {heightmapContext.sourceRenderTexture}");
                Debug.Log($"- 像素矩形: {heightmapContext.pixelRect}");
                Debug.Log($"- 归一化坐标: {heightmapContext.normalizedCoordinate}");
                
                TerrainPaintUtility.EndPaintHeightmap(heightmapContext, "Paint Context Info");
            }
        }

        #endregion

        #region 绘制区域示例

        /// <summary>
        /// 绘制指定区域
        /// </summary>
        public static void PaintSpecificAreaExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 绘制中心区域
            Rect centerArea = new Rect(0.25f, 0.25f, 0.5f, 0.5f);
            
            var paintContext = TerrainPaintUtility.BeginPaintTexture(terrainData, centerArea);
            
            if (paintContext != null)
            {
                Debug.Log($"绘制中心区域: {centerArea}");
                Debug.Log($"像素矩形: {paintContext.pixelRect}");
                
                TerrainPaintUtility.EndPaintTexture(paintContext, "Paint Specific Area");
            }
        }

        /// <summary>
        /// 绘制多个区域
        /// </summary>
        public static void PaintMultipleAreasExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            TerrainData terrainData = selectedTerrain.terrainData;
            
            // 定义多个绘制区域
            Rect[] areas = {
                new Rect(0, 0, 0.3f, 0.3f),      // 左上角
                new Rect(0.7f, 0, 0.3f, 0.3f),   // 右上角
                new Rect(0, 0.7f, 0.3f, 0.3f),   // 左下角
                new Rect(0.7f, 0.7f, 0.3f, 0.3f) // 右下角
            };
            
            foreach (Rect area in areas)
            {
                var paintContext = TerrainPaintUtility.BeginPaintTexture(terrainData, area);
                
                if (paintContext != null)
                {
                    Debug.Log($"绘制区域: {area}");
                    TerrainPaintUtility.EndPaintTexture(paintContext, $"Paint Area {area}");
                }
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建地形绘制测试
        /// </summary>
        public static void CreateTerrainPaintTestExample()
        {
            // 创建地形数据
            TerrainData terrainData = new TerrainData();
            terrainData.size = new Vector3(100, 30, 100);
            terrainData.heightmapResolution = 513;
            
            // 创建地形
            GameObject terrainObj = Terrain.CreateTerrainGameObject(terrainData);
            terrainObj.name = "PaintTestTerrain";
            
            Terrain terrain = terrainObj.GetComponent<Terrain>();
            
            // 添加纹理层
            TerrainLayer[] terrainLayers = new TerrainLayer[2];
            
            terrainLayers[0] = new TerrainLayer();
            terrainLayers[0].diffuseTexture = CreateTestTexture("Grass", Color.green);
            
            terrainLayers[1] = new TerrainLayer();
            terrainLayers[1].diffuseTexture = CreateTestTexture("Rock", Color.gray);
            
            terrainData.terrainLayers = terrainLayers;
            
            // 测试绘制
            var paintContext = TerrainPaintUtility.BeginPaintTexture(terrainData, new Rect(0, 0, 1, 1));
            
            if (paintContext != null)
            {
                Debug.Log("地形绘制测试成功");
                TerrainPaintUtility.EndPaintTexture(paintContext, "Terrain Paint Test");
            }
            
            Debug.Log($"测试地形已创建: {terrain.name}");
        }

        /// <summary>
        /// 批量地形绘制
        /// </summary>
        public static void BatchTerrainPaintExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            if (terrains.Length == 0)
            {
                Debug.LogWarning("场景中没有地形");
                return;
            }
            
            foreach (Terrain terrain in terrains)
            {
                TerrainData terrainData = terrain.terrainData;
                
                if (terrainData.terrainLayers.Length > 0)
                {
                    var paintContext = TerrainPaintUtility.BeginPaintTexture(terrainData, new Rect(0, 0, 1, 1));
                    
                    if (paintContext != null)
                    {
                        Debug.Log($"绘制地形: {terrain.name}");
                        TerrainPaintUtility.EndPaintTexture(paintContext, $"Batch Paint {terrain.name}");
                    }
                }
            }
            
            Debug.Log($"批量绘制完成，共处理 {terrains.Length} 个地形");
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
