using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine.Rendering;

namespace UnityEditor.Examples
{
    /// <summary>
    /// RenderPipelineEditorUtility 工具类示例
    /// 提供渲染管线编辑器相关的实用工具功能
    /// </summary>
    public static class RenderPipelineEditorUtilityExample
    {
        #region 管线资源示例

        /// <summary>
        /// 获取管线资源
        /// </summary>
        public static void GetPipelineAssetExample()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            if (pipelineAsset != null)
            {
                Debug.Log($"当前渲染管线资源:");
                Debug.Log($"- 资源名称: {pipelineAsset.name}");
                Debug.Log($"- 资源类型: {pipelineAsset.GetType().Name}");
                Debug.Log($"- 资源路径: {AssetDatabase.GetAssetPath(pipelineAsset)}");
            }
            else
            {
                Debug.Log("没有找到渲染管线资源");
            }
        }

        /// <summary>
        /// 检查管线资源状态
        /// </summary>
        public static void CheckPipelineAssetStatusExample()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            Debug.Log($"渲染管线资源状态检查:");
            
            if (pipelineAsset != null)
            {
                Debug.Log($"✓ 管线资源存在: {pipelineAsset.name}");
                Debug.Log($"✓ 管线类型: {pipelineAsset.GetType().Name}");
                Debug.Log($"✓ 资源路径: {AssetDatabase.GetAssetPath(pipelineAsset)}");
                
                // 检查管线设置
                if (pipelineAsset is UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset urpAsset)
                {
                    Debug.Log($"✓ URP设置: {urpAsset.name}");
                }
                else if (pipelineAsset is UnityEngine.Rendering.HighDefinition.HDRenderPipelineAsset hdrpAsset)
                {
                    Debug.Log($"✓ HDRP设置: {hdrpAsset.name}");
                }
            }
            else
            {
                Debug.LogWarning("✗ 没有找到渲染管线资源");
                Debug.LogWarning("✗ 使用内置渲染管线");
            }
        }

        #endregion

        #region 渲染管线类型示例

        /// <summary>
        /// 检查渲染管线类型
        /// </summary>
        public static void CheckRenderPipelineTypeExample()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            Debug.Log($"渲染管线类型检查:");
            
            if (pipelineAsset != null)
            {
                string pipelineType = pipelineAsset.GetType().Name;
                Debug.Log($"- 管线类型: {pipelineType}");
                
                if (pipelineType.Contains("Universal"))
                {
                    Debug.Log("- 当前使用URP (Universal Render Pipeline)");
                    Debug.Log("- 适用于移动端和中等质量项目");
                }
                else if (pipelineType.Contains("HighDefinition"))
                {
                    Debug.Log("- 当前使用HDRP (High Definition Render Pipeline)");
                    Debug.Log("- 适用于高端PC和主机平台");
                }
                else if (pipelineType.Contains("Builtin"))
                {
                    Debug.Log("- 当前使用内置渲染管线");
                    Debug.Log("- 传统渲染管线");
                }
                else
                {
                    Debug.Log("- 使用自定义渲染管线");
                }
            }
            else
            {
                Debug.Log("- 使用内置渲染管线");
            }
        }

        /// <summary>
        /// 获取渲染管线信息
        /// </summary>
        public static void GetRenderPipelineInfoExample()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            Debug.Log($"渲染管线信息:");
            
            if (pipelineAsset != null)
            {
                Debug.Log($"- 管线资源: {pipelineAsset.name}");
                Debug.Log($"- 管线类型: {pipelineAsset.GetType().Name}");
                Debug.Log($"- 管线版本: {pipelineAsset.version}");
                
                // 检查管线设置
                if (pipelineAsset is UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset urpAsset)
                {
                    Debug.Log($"- URP设置: {urpAsset.name}");
                }
                else if (pipelineAsset is UnityEngine.Rendering.HighDefinition.HDRenderPipelineAsset hdrpAsset)
                {
                    Debug.Log($"- HDRP设置: {hdrpAsset.name}");
                }
            }
            else
            {
                Debug.Log("- 使用内置渲染管线");
                Debug.Log("- 无自定义管线资源");
            }
        }

        #endregion

        #region 渲染管线设置示例

        /// <summary>
        /// 检查渲染管线设置
        /// </summary>
        public static void CheckRenderPipelineSettingsExample()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            Debug.Log($"渲染管线设置检查:");
            
            if (pipelineAsset != null)
            {
                Debug.Log($"- 管线资源: {pipelineAsset.name}");
                Debug.Log($"- 管线类型: {pipelineAsset.GetType().Name}");
                
                // 检查质量设置
                QualitySettings[] qualitySettings = QualitySettings.names;
                Debug.Log($"- 质量设置数量: {qualitySettings.Length}");
                
                // 检查当前质量级别
                int currentQuality = QualitySettings.GetQualityLevel();
                Debug.Log($"- 当前质量级别: {currentQuality}");
                Debug.Log($"- 当前质量名称: {QualitySettings.names[currentQuality]}");
                
                // 检查渲染设置
                Debug.Log($"- 抗锯齿: {QualitySettings.antiAliasing}");
                Debug.Log($"- 软阴影: {QualitySettings.shadows}");
                Debug.Log($"- 阴影分辨率: {QualitySettings.shadowResolution}");
            }
            else
            {
                Debug.Log("- 使用内置渲染管线设置");
            }
        }

        /// <summary>
        /// 设置渲染管线质量
        /// </summary>
        public static void SetRenderPipelineQualityExample()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            if (pipelineAsset != null)
            {
                // 设置质量级别
                int maxQuality = QualitySettings.names.Length - 1;
                QualitySettings.SetQualityLevel(maxQuality);
                
                Debug.Log($"渲染管线质量已设置为最高级别: {QualitySettings.names[maxQuality]}");
            }
            else
            {
                Debug.Log("使用内置渲染管线，设置质量级别");
                QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1);
            }
        }

        #endregion

        #region 渲染管线资源管理示例

        /// <summary>
        /// 查找渲染管线资源
        /// </summary>
        public static void FindRenderPipelineAssetsExample()
        {
            string[] guids = AssetDatabase.FindAssets("t:RenderPipelineAsset");
            
            Debug.Log($"找到 {guids.Length} 个渲染管线资源:");
            
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                RenderPipelineAsset asset = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>(path);
                
                if (asset != null)
                {
                    Debug.Log($"- {asset.name} ({path})");
                    Debug.Log($"  - 类型: {asset.GetType().Name}");
                    Debug.Log($"  - 版本: {asset.version}");
                }
            }
        }

        /// <summary>
        /// 创建渲染管线资源
        /// </summary>
        public static void CreateRenderPipelineAssetExample()
        {
            // 创建URP资源
            var urpAsset = ScriptableObject.CreateInstance<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>();
            
            // 保存资源
            string path = "Assets/URP_Test_Asset.asset";
            AssetDatabase.CreateAsset(urpAsset, path);
            AssetDatabase.SaveAssets();
            
            Debug.Log($"URP资源已创建: {path}");
        }

        #endregion

        #region 渲染管线切换示例

        /// <summary>
        /// 切换渲染管线
        /// </summary>
        public static void SwitchRenderPipelineExample()
        {
            RenderPipelineAsset currentAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            if (currentAsset != null)
            {
                Debug.Log($"当前渲染管线: {currentAsset.name}");
                
                // 查找其他管线资源
                string[] guids = AssetDatabase.FindAssets("t:RenderPipelineAsset");
                
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    RenderPipelineAsset asset = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>(path);
                    
                    if (asset != null && asset != currentAsset)
                    {
                        Debug.Log($"可切换到: {asset.name}");
                        // 这里可以添加切换逻辑
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("当前使用内置渲染管线");
            }
        }

        /// <summary>
        /// 重置渲染管线
        /// </summary>
        public static void ResetRenderPipelineExample()
        {
            // 重置为内置渲染管线
            GraphicsSettings.renderPipelineAsset = null;
            
            Debug.Log("渲染管线已重置为内置管线");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 渲染管线诊断
        /// </summary>
        public static void RenderPipelineDiagnosticsExample()
        {
            Debug.Log("=== 渲染管线诊断 ===");
            
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            if (pipelineAsset != null)
            {
                Debug.Log($"✓ 渲染管线资源: {pipelineAsset.name}");
                Debug.Log($"✓ 管线类型: {pipelineAsset.GetType().Name}");
                Debug.Log($"✓ 资源路径: {AssetDatabase.GetAssetPath(pipelineAsset)}");
            }
            else
            {
                Debug.LogWarning("✗ 没有找到渲染管线资源");
                Debug.LogWarning("✗ 使用内置渲染管线");
            }
            
            // 检查质量设置
            Debug.Log($"✓ 质量级别: {QualitySettings.GetQualityLevel()}");
            Debug.Log($"✓ 质量名称: {QualitySettings.names[QualitySettings.GetQualityLevel()]}");
            
            // 检查渲染设置
            Debug.Log($"✓ 抗锯齿: {QualitySettings.antiAliasing}");
            Debug.Log($"✓ 阴影质量: {QualitySettings.shadows}");
            Debug.Log($"✓ 阴影分辨率: {QualitySettings.shadowResolution}");
        }

        /// <summary>
        /// 批量渲染管线处理
        /// </summary>
        public static void BatchRenderPipelineProcessingExample()
        {
            // 查找所有渲染管线资源
            string[] guids = AssetDatabase.FindAssets("t:RenderPipelineAsset");
            
            Debug.Log($"批量处理 {guids.Length} 个渲染管线资源:");
            
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                RenderPipelineAsset asset = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>(path);
                
                if (asset != null)
                {
                    Debug.Log($"处理资源: {asset.name}");
                    Debug.Log($"- 路径: {path}");
                    Debug.Log($"- 类型: {asset.GetType().Name}");
                    Debug.Log($"- 版本: {asset.version}");
                }
            }
        }

        /// <summary>
        /// 渲染管线测试
        /// </summary>
        public static void RenderPipelineTestExample()
        {
            Debug.Log("渲染管线测试:");
            
            // 测试当前管线
            RenderPipelineAsset currentAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            if (currentAsset != null)
            {
                Debug.Log($"当前管线: {currentAsset.name}");
                Debug.Log($"- 类型: {currentAsset.GetType().Name}");
                Debug.Log($"- 版本: {currentAsset.version}");
                Debug.Log($"- 路径: {AssetDatabase.GetAssetPath(currentAsset)}");
            }
            else
            {
                Debug.Log("当前管线: 内置渲染管线");
            }
            
            // 测试质量设置
            Debug.Log($"质量设置:");
            Debug.Log($"- 当前级别: {QualitySettings.GetQualityLevel()}");
            Debug.Log($"- 级别名称: {QualitySettings.names[QualitySettings.GetQualityLevel()]}");
            Debug.Log($"- 总级别数: {QualitySettings.names.Length}");
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查渲染管线是否可用
        /// </summary>
        private static bool IsRenderPipelineAvailable()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            return pipelineAsset != null;
        }

        /// <summary>
        /// 获取渲染管线类型名称
        /// </summary>
        private static string GetRenderPipelineTypeName()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            return pipelineAsset?.GetType().Name ?? "Builtin";
        }

        /// <summary>
        /// 获取渲染管线描述
        /// </summary>
        private static string GetRenderPipelineDescription()
        {
            RenderPipelineAsset pipelineAsset = RenderPipelineEditorUtility.GetPipelineAsset();
            
            if (pipelineAsset != null)
            {
                string typeName = pipelineAsset.GetType().Name;
                
                if (typeName.Contains("Universal"))
                {
                    return "URP (Universal Render Pipeline)";
                }
                else if (typeName.Contains("HighDefinition"))
                {
                    return "HDRP (High Definition Render Pipeline)";
                }
                else
                {
                    return $"Custom ({typeName})";
                }
            }
            
            return "Built-in Render Pipeline";
        }

        #endregion
    }
}
