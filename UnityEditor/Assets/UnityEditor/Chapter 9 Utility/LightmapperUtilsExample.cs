using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

namespace UnityEditor.Examples
{
    /// <summary>
    /// LightmapperUtils 工具类示例
    /// 提供光照贴图相关的实用工具功能
    /// </summary>
    public static class LightmapperUtilsExample
    {
        #region 光照贴图提取示例

        /// <summary>
        /// 提取光照贴图
        /// </summary>
        public static void ExtractExample()
        {
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            if (lightmapData.Length == 0)
            {
                Debug.LogWarning("场景中没有光照贴图数据");
                return;
            }

            Debug.Log($"提取光照贴图:");
            Debug.Log($"- 光照贴图数量: {lightmapData.Length}");
            
            for (int i = 0; i < lightmapData.Length; i++)
            {
                LightmapData data = lightmapData[i];
                
                Debug.Log($"光照贴图 {i}:");
                Debug.Log($"  - 光照贴图: {data.lightmapColor?.name ?? "无"}");
                Debug.Log($"  - 方向贴图: {data.lightmapDir?.name ?? "无"}");
                Debug.Log($"  - 阴影遮罩: {data.shadowMask?.name ?? "无"}");
                
                // 提取光照贴图
                if (data.lightmapColor != null)
                {
                    Texture2D extractedTexture = LightmapperUtils.Extract(data.lightmapColor);
                    Debug.Log($"  - 提取的光照贴图: {extractedTexture.name}");
                }
            }
        }

        /// <summary>
        /// 提取指定光照贴图
        /// </summary>
        public static void ExtractSpecificLightmapExample()
        {
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            if (lightmapData.Length == 0)
            {
                Debug.LogWarning("场景中没有光照贴图数据");
                return;
            }

            // 提取第一个光照贴图
            LightmapData firstLightmap = lightmapData[0];
            
            if (firstLightmap.lightmapColor != null)
            {
                Texture2D extractedTexture = LightmapperUtils.Extract(firstLightmap.lightmapColor);
                
                Debug.Log($"提取指定光照贴图:");
                Debug.Log($"- 原始贴图: {firstLightmap.lightmapColor.name}");
                Debug.Log($"- 提取贴图: {extractedTexture.name}");
                Debug.Log($"- 贴图大小: {extractedTexture.width}x{extractedTexture.height}");
                Debug.Log($"- 贴图格式: {extractedTexture.format}");
            }
            else
            {
                Debug.LogWarning("指定的光照贴图为空");
            }
        }

        #endregion

        #region 光照贴图存储示例

        /// <summary>
        /// 存储光照贴图
        /// </summary>
        public static void StoreExample()
        {
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            if (lightmapData.Length == 0)
            {
                Debug.LogWarning("场景中没有光照贴图数据");
                return;
            }

            Debug.Log($"存储光照贴图:");
            
            for (int i = 0; i < lightmapData.Length; i++)
            {
                LightmapData data = lightmapData[i];
                
                if (data.lightmapColor != null)
                {
                    // 存储光照贴图
                    LightmapperUtils.Store(data.lightmapColor);
                    Debug.Log($"- 已存储光照贴图 {i}: {data.lightmapColor.name}");
                }
            }
        }

        /// <summary>
        /// 存储指定光照贴图
        /// </summary>
        public static void StoreSpecificLightmapExample()
        {
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            if (lightmapData.Length == 0)
            {
                Debug.LogWarning("场景中没有光照贴图数据");
                return;
            }

            // 存储第一个光照贴图
            LightmapData firstLightmap = lightmapData[0];
            
            if (firstLightmap.lightmapColor != null)
            {
                LightmapperUtils.Store(firstLightmap.lightmapColor);
                
                Debug.Log($"存储指定光照贴图:");
                Debug.Log($"- 已存储: {firstLightmap.lightmapColor.name}");
                Debug.Log($"- 贴图大小: {firstLightmap.lightmapColor.width}x{firstLightmap.lightmapColor.height}");
            }
            else
            {
                Debug.LogWarning("指定的光照贴图为空");
            }
        }

        #endregion

        #region 光照贴图信息示例

        /// <summary>
        /// 获取光照贴图信息
        /// </summary>
        public static void GetLightmapInfoExample()
        {
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            Debug.Log($"光照贴图信息:");
            Debug.Log($"- 光照贴图数量: {lightmapData.Length}");
            
            if (lightmapData.Length > 0)
            {
                for (int i = 0; i < lightmapData.Length; i++)
                {
                    LightmapData data = lightmapData[i];
                    
                    Debug.Log($"光照贴图 {i}:");
                    Debug.Log($"  - 光照贴图: {data.lightmapColor?.name ?? "无"}");
                    Debug.Log($"  - 方向贴图: {data.lightmapDir?.name ?? "无"}");
                    Debug.Log($"  - 阴影遮罩: {data.shadowMask?.name ?? "无"}");
                    
                    if (data.lightmapColor != null)
                    {
                        Debug.Log($"  - 光照贴图大小: {data.lightmapColor.width}x{data.lightmapColor.height}");
                        Debug.Log($"  - 光照贴图格式: {data.lightmapColor.format}");
                    }
                }
            }
            else
            {
                Debug.LogWarning("场景中没有光照贴图");
            }
        }

        /// <summary>
        /// 检查光照贴图状态
        /// </summary>
        public static void CheckLightmapStatusExample()
        {
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            Debug.Log($"光照贴图状态检查:");
            Debug.Log($"- 光照贴图数量: {lightmapData.Length}");
            
            if (lightmapData.Length > 0)
            {
                int validLightmaps = 0;
                int validDirectionalMaps = 0;
                int validShadowMasks = 0;
                
                foreach (LightmapData data in lightmapData)
                {
                    if (data.lightmapColor != null) validLightmaps++;
                    if (data.lightmapDir != null) validDirectionalMaps++;
                    if (data.shadowMask != null) validShadowMasks++;
                }
                
                Debug.Log($"- 有效光照贴图: {validLightmaps}");
                Debug.Log($"- 有效方向贴图: {validDirectionalMaps}");
                Debug.Log($"- 有效阴影遮罩: {validShadowMasks}");
            }
            else
            {
                Debug.LogWarning("- 场景中没有光照贴图数据");
                Debug.LogWarning("- 需要烘焙光照贴图");
            }
        }

        #endregion

        #region 光照贴图设置示例

        /// <summary>
        /// 检查光照贴图设置
        /// </summary>
        public static void CheckLightmapSettingsExample()
        {
            Debug.Log($"光照贴图设置检查:");
            
            // 检查光照贴图设置
            LightmapSettings lightmapSettings = LightmapSettings.lightmaps;
            Debug.Log($"- 光照贴图数量: {lightmapSettings.Length}");
            
            // 检查光照贴图模式
            LightmapSettings.lightmapsMode mode = LightmapSettings.lightmapsMode;
            Debug.Log($"- 光照贴图模式: {mode}");
            
            // 检查光照贴图参数
            LightmapParameters parameters = LightmapSettings.lightmapParameters;
            if (parameters != null)
            {
                Debug.Log($"- 光照贴图参数: {parameters.name}");
            }
            else
            {
                Debug.Log("- 使用默认光照贴图参数");
            }
        }

        /// <summary>
        /// 设置光照贴图模式
        /// </summary>
        public static void SetLightmapModeExample()
        {
            Debug.Log($"设置光照贴图模式:");
            
            // 设置为方向模式
            LightmapSettings.lightmapsMode = LightmapSettings.lightmapsMode.Directional;
            Debug.Log($"- 已设置为方向模式: {LightmapSettings.lightmapsMode}");
            
            // 设置为非方向模式
            LightmapSettings.lightmapsMode = LightmapSettings.lightmapsMode.NonDirectional;
            Debug.Log($"- 已设置为非方向模式: {LightmapSettings.lightmapsMode}");
        }

        #endregion

        #region 光照贴图烘焙示例

        /// <summary>
        /// 检查光照贴图烘焙状态
        /// </summary>
        public static void CheckLightmapBakingStatusExample()
        {
            Debug.Log($"光照贴图烘焙状态检查:");
            
            // 检查是否有光照贴图
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            Debug.Log($"- 光照贴图数量: {lightmapData.Length}");
            
            if (lightmapData.Length > 0)
            {
                Debug.Log("✓ 光照贴图已烘焙");
                Debug.Log("✓ 场景光照已计算");
            }
            else
            {
                Debug.LogWarning("✗ 光照贴图未烘焙");
                Debug.LogWarning("✗ 需要烘焙光照贴图");
            }
            
            // 检查光照贴图质量
            LightmapSettings.lightmapsMode mode = LightmapSettings.lightmapsMode;
            Debug.Log($"- 光照贴图模式: {mode}");
            
            if (mode == LightmapSettings.lightmapsMode.Directional)
            {
                Debug.Log("- 使用方向光照贴图");
                Debug.Log("- 支持法线贴图");
            }
            else
            {
                Debug.Log("- 使用非方向光照贴图");
                Debug.Log("- 不支持法线贴图");
            }
        }

        /// <summary>
        /// 光照贴图烘焙建议
        /// </summary>
        public static void LightmapBakingAdviceExample()
        {
            Debug.Log($"光照贴图烘焙建议:");
            
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            if (lightmapData.Length == 0)
            {
                Debug.Log("建议操作:");
                Debug.Log("1. 检查场景中的光源设置");
                Debug.Log("2. 确保对象有正确的光照贴图UV");
                Debug.Log("3. 设置合适的光照贴图参数");
                Debug.Log("4. 开始烘焙光照贴图");
            }
            else
            {
                Debug.Log("当前状态:");
                Debug.Log("✓ 光照贴图已烘焙");
                Debug.Log("✓ 可以优化光照贴图质量");
                Debug.Log("✓ 可以调整光照贴图参数");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 光照贴图诊断
        /// </summary>
        public static void LightmapDiagnosticsExample()
        {
            Debug.Log("=== 光照贴图诊断 ===");
            
            // 基本信息
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            Debug.Log($"✓ 光照贴图数量: {lightmapData.Length}");
            
            if (lightmapData.Length > 0)
            {
                Debug.Log("✓ 光照贴图状态: 已烘焙");
                
                // 详细信息
                for (int i = 0; i < lightmapData.Length; i++)
                {
                    LightmapData data = lightmapData[i];
                    
                    Debug.Log($"✓ 光照贴图 {i}:");
                    Debug.Log($"  - 光照贴图: {data.lightmapColor?.name ?? "无"}");
                    Debug.Log($"  - 方向贴图: {data.lightmapDir?.name ?? "无"}");
                    Debug.Log($"  - 阴影遮罩: {data.shadowMask?.name ?? "无"}");
                    
                    if (data.lightmapColor != null)
                    {
                        Debug.Log($"  - 大小: {data.lightmapColor.width}x{data.lightmapColor.height}");
                        Debug.Log($"  - 格式: {data.lightmapColor.format}");
                    }
                }
            }
            else
            {
                Debug.LogWarning("✗ 光照贴图状态: 未烘焙");
                Debug.LogWarning("✗ 需要烘焙光照贴图");
            }
            
            // 设置信息
            LightmapSettings.lightmapsMode mode = LightmapSettings.lightmapsMode;
            Debug.Log($"✓ 光照贴图模式: {mode}");
            
            LightmapParameters parameters = LightmapSettings.lightmapParameters;
            Debug.Log($"✓ 光照贴图参数: {parameters?.name ?? "默认"}");
        }

        /// <summary>
        /// 批量光照贴图处理
        /// </summary>
        public static void BatchLightmapProcessingExample()
        {
            Debug.Log("批量光照贴图处理:");
            
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            if (lightmapData.Length > 0)
            {
                Debug.Log($"处理 {lightmapData.Length} 个光照贴图:");
                
                for (int i = 0; i < lightmapData.Length; i++)
                {
                    LightmapData data = lightmapData[i];
                    
                    Debug.Log($"处理光照贴图 {i}:");
                    
                    if (data.lightmapColor != null)
                    {
                        Debug.Log($"  - 提取: {data.lightmapColor.name}");
                        Texture2D extracted = LightmapperUtils.Extract(data.lightmapColor);
                        
                        Debug.Log($"  - 存储: {extracted.name}");
                        LightmapperUtils.Store(extracted);
                    }
                }
            }
            else
            {
                Debug.LogWarning("没有光照贴图需要处理");
            }
        }

        /// <summary>
        /// 光照贴图测试
        /// </summary>
        public static void LightmapTestExample()
        {
            Debug.Log("光照贴图测试:");
            
            // 测试光照贴图数据
            LightmapData[] lightmapData = LightmapSettings.lightmaps;
            
            Debug.Log($"测试结果:");
            Debug.Log($"- 光照贴图数量: {lightmapData.Length}");
            
            if (lightmapData.Length > 0)
            {
                Debug.Log("- 测试状态: 通过");
                Debug.Log("- 光照贴图可用");
                
                // 测试提取和存储
                LightmapData firstLightmap = lightmapData[0];
                if (firstLightmap.lightmapColor != null)
                {
                    Texture2D extracted = LightmapperUtils.Extract(firstLightmap.lightmapColor);
                    LightmapperUtils.Store(extracted);
                    
                    Debug.Log("- 提取和存储测试: 通过");
                }
            }
            else
            {
                Debug.LogWarning("- 测试状态: 失败");
                Debug.LogWarning("- 没有光照贴图数据");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查是否有光照贴图
        /// </summary>
        private static bool HasLightmaps()
        {
            return LightmapSettings.lightmaps.Length > 0;
        }

        /// <summary>
        /// 获取光照贴图数量
        /// </summary>
        private static int GetLightmapCount()
        {
            return LightmapSettings.lightmaps.Length;
        }

        /// <summary>
        /// 获取光照贴图模式描述
        /// </summary>
        private static string GetLightmapModeDescription()
        {
            LightmapSettings.lightmapsMode mode = LightmapSettings.lightmapsMode;
            
            switch (mode)
            {
                case LightmapSettings.lightmapsMode.NonDirectional:
                    return "非方向光照贴图";
                case LightmapSettings.lightmapsMode.Directional:
                    return "方向光照贴图";
                default:
                    return "未知模式";
            }
        }

        #endregion
    }
}
