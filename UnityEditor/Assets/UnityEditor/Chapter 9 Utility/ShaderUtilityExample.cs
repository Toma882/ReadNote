using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEditor.Chapter9Utility.ShaderUtility
{
    /// <summary>
    /// ShaderUtil 着色器工具详细示例
    /// 展示着色器属性、变体、编译等操作
    /// </summary>
    public class ShaderUtilityExample : EditorWindow
    {
        private Vector2 scrollPosition;
        private Shader selectedShader;
        private Material selectedMaterial;
        private List<ShaderPropertyInfo> shaderProperties = new List<ShaderPropertyInfo>();

        [MenuItem("Tools/Utility Examples/ShaderUtility Detailed Example")]
        public static void ShowWindow()
        {
            ShaderUtilityExample window = GetWindow<ShaderUtilityExample>("ShaderUtility 示例");
            window.Show();
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            GUILayout.Label("ShaderUtil 着色器工具示例", EditorStyles.boldLabel);
            
            EditorGUILayout.Space();
            
            // 着色器选择
            EditorGUILayout.LabelField("着色器选择:", EditorStyles.boldLabel);
            selectedShader = (Shader)EditorGUILayout.ObjectField("目标着色器", selectedShader, typeof(Shader), false);
            
            EditorGUILayout.Space();
            
            // 着色器分析
            EditorGUILayout.LabelField("着色器分析:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("分析着色器属性"))
            {
                AnalyzeShaderProperties();
            }
            
            if (GUILayout.Button("获取着色器变体"))
            {
                GetShaderVariants();
            }
            
            if (GUILayout.Button("检查着色器错误"))
            {
                CheckShaderErrors();
            }
            
            EditorGUILayout.Space();
            
            // 着色器编译
            EditorGUILayout.LabelField("着色器编译:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("编译着色器"))
            {
                CompileShader();
            }
            
            if (GUILayout.Button("获取编译信息"))
            {
                GetCompilationInfo();
            }
            
            if (GUILayout.Button("检查编译状态"))
            {
                CheckCompilationStatus();
            }
            
            EditorGUILayout.Space();
            
            // 材质操作
            EditorGUILayout.LabelField("材质操作:", EditorStyles.boldLabel);
            selectedMaterial = (Material)EditorGUILayout.ObjectField("目标材质", selectedMaterial, typeof(Material), false);
            
            if (GUILayout.Button("分析材质属性"))
            {
                AnalyzeMaterialProperties();
            }
            
            if (GUILayout.Button("优化材质"))
            {
                OptimizeMaterial();
            }
            
            if (GUILayout.Button("批量处理材质"))
            {
                BatchProcessMaterials();
            }
            
            EditorGUILayout.Space();
            
            // 高级操作
            EditorGUILayout.LabelField("高级操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("着色器性能分析"))
            {
                AnalyzeShaderPerformance();
            }
            
            if (GUILayout.Button("着色器优化建议"))
            {
                GetOptimizationSuggestions();
            }
            
            if (GUILayout.Button("着色器兼容性检查"))
            {
                CheckShaderCompatibility();
            }
            
            EditorGUILayout.Space();
            
            // 显示着色器信息
            if (selectedShader != null)
            {
                EditorGUILayout.LabelField("着色器信息:", EditorStyles.boldLabel);
                EditorGUILayout.LabelField($"名称: {selectedShader.name}");
                EditorGUILayout.LabelField($"渲染队列: {ShaderUtil.GetRenderQueue(selectedShader)}");
                EditorGUILayout.LabelField($"LOD: {ShaderUtil.GetShaderLOD(selectedShader)}");
                EditorGUILayout.LabelField($"属性数量: {shaderProperties.Count}");
                
                // 显示属性列表
                if (shaderProperties.Count > 0)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("着色器属性:", EditorStyles.boldLabel);
                    
                    foreach (ShaderPropertyInfo prop in shaderProperties)
                    {
                        EditorGUILayout.LabelField($"  {prop.name} ({prop.type})");
                    }
                }
            }
            
            EditorGUILayout.EndScrollView();
        }

        #region 着色器分析

        /// <summary>
        /// 分析着色器属性
        /// </summary>
        private void AnalyzeShaderProperties()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            shaderProperties.Clear();
            
            int propertyCount = ShaderUtil.GetPropertyCount(selectedShader);
            Debug.Log($"着色器 {selectedShader.name} 属性分析:");
            Debug.Log($"  属性数量: {propertyCount}");
            
            for (int i = 0; i < propertyCount; i++)
            {
                ShaderPropertyInfo propInfo = new ShaderPropertyInfo();
                propInfo.name = ShaderUtil.GetPropertyName(selectedShader, i);
                propInfo.type = ShaderUtil.GetPropertyType(selectedShader, i);
                propInfo.description = ShaderUtil.GetPropertyDescription(selectedShader, i);
                propInfo.flags = ShaderUtil.GetPropertyFlags(selectedShader, i);
                
                shaderProperties.Add(propInfo);
                
                Debug.Log($"  属性 {i + 1}: {propInfo.name}");
                Debug.Log($"    类型: {propInfo.type}");
                Debug.Log($"    描述: {propInfo.description}");
                Debug.Log($"    标志: {propInfo.flags}");
            }
        }

        /// <summary>
        /// 获取着色器变体
        /// </summary>
        private void GetShaderVariants()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            Debug.Log($"着色器 {selectedShader.name} 变体信息:");
            
            // 获取着色器变体数量
            int variantCount = ShaderUtil.GetVariantCount(selectedShader, true);
            Debug.Log($"  变体数量: {variantCount}");
            
            // 获取着色器关键字
            string[] keywords = ShaderUtil.GetShaderKeywords(selectedShader);
            Debug.Log($"  关键字数量: {keywords.Length}");
            
            foreach (string keyword in keywords)
            {
                Debug.Log($"    关键字: {keyword}");
            }
            
            // 获取着色器变体信息
            for (int i = 0; i < Mathf.Min(variantCount, 10); i++) // 只显示前10个变体
            {
                string variantInfo = ShaderUtil.GetVariantInfo(selectedShader, i);
                Debug.Log($"  变体 {i + 1}: {variantInfo}");
            }
        }

        /// <summary>
        /// 检查着色器错误
        /// </summary>
        private void CheckShaderErrors()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            Debug.Log($"检查着色器 {selectedShader.name} 错误:");
            
            // 检查着色器是否有错误
            bool hasErrors = ShaderUtil.HasErrors(selectedShader);
            Debug.Log($"  是否有错误: {hasErrors}");
            
            if (hasErrors)
            {
                // 获取错误信息
                string[] errors = ShaderUtil.GetShaderErrors(selectedShader);
                Debug.Log($"  错误数量: {errors.Length}");
                
                foreach (string error in errors)
                {
                    Debug.LogError($"    错误: {error}");
                }
            }
            else
            {
                Debug.Log("  着色器没有错误");
            }
        }

        #endregion

        #region 着色器编译

        /// <summary>
        /// 编译着色器
        /// </summary>
        private void CompileShader()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            Debug.Log($"编译着色器: {selectedShader.name}");
            
            // 强制重新编译着色器
            ShaderUtil.ClearShaderErrors(selectedShader);
            
            // 编译着色器
            ShaderUtil.CompileShader(selectedShader);
            
            Debug.Log("着色器编译完成");
        }

        /// <summary>
        /// 获取编译信息
        /// </summary>
        private void GetCompilationInfo()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            Debug.Log($"着色器 {selectedShader.name} 编译信息:");
            
            // 获取编译时间
            float compileTime = ShaderUtil.GetCompileTime(selectedShader);
            Debug.Log($"  编译时间: {compileTime:F2} 秒");
            
            // 获取着色器大小
            int shaderSize = ShaderUtil.GetShaderSize(selectedShader);
            Debug.Log($"  着色器大小: {shaderSize} 字节");
            
            // 获取着色器复杂度
            int complexity = ShaderUtil.GetShaderComplexity(selectedShader);
            Debug.Log($"  着色器复杂度: {complexity}");
        }

        /// <summary>
        /// 检查编译状态
        /// </summary>
        private void CheckCompilationStatus()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            Debug.Log($"检查着色器 {selectedShader.name} 编译状态:");
            
            // 检查是否正在编译
            bool isCompiling = ShaderUtil.IsCompiling(selectedShader);
            Debug.Log($"  是否正在编译: {isCompiling}");
            
            // 检查编译是否完成
            bool isCompiled = ShaderUtil.IsCompiled(selectedShader);
            Debug.Log($"  是否编译完成: {isCompiled}");
            
            // 检查编译是否成功
            bool isCompiledSuccessfully = !ShaderUtil.HasErrors(selectedShader);
            Debug.Log($"  编译是否成功: {isCompiledSuccessfully}");
        }

        #endregion

        #region 材质操作

        /// <summary>
        /// 分析材质属性
        /// </summary>
        private void AnalyzeMaterialProperties()
        {
            if (selectedMaterial == null)
            {
                Debug.LogWarning("请先选择一个材质");
                return;
            }
            
            Debug.Log($"分析材质 {selectedMaterial.name}:");
            
            Shader shader = selectedMaterial.shader;
            if (shader == null)
            {
                Debug.LogWarning("材质没有关联的着色器");
                return;
            }
            
            int propertyCount = ShaderUtil.GetPropertyCount(shader);
            Debug.Log($"  着色器属性数量: {propertyCount}");
            
            for (int i = 0; i < propertyCount; i++)
            {
                string propName = ShaderUtil.GetPropertyName(shader, i);
                ShaderUtil.ShaderPropertyType propType = ShaderUtil.GetPropertyType(shader, i);
                
                Debug.Log($"  属性 {i + 1}: {propName} ({propType})");
                
                // 检查材质是否使用了这个属性
                if (selectedMaterial.HasProperty(propName))
                {
                    switch (propType)
                    {
                        case ShaderUtil.ShaderPropertyType.Float:
                            Debug.Log($"    值: {selectedMaterial.GetFloat(propName)}");
                            break;
                        case ShaderUtil.ShaderPropertyType.Vector:
                            Debug.Log($"    值: {selectedMaterial.GetVector(propName)}");
                            break;
                        case ShaderUtil.ShaderPropertyType.Color:
                            Debug.Log($"    值: {selectedMaterial.GetColor(propName)}");
                            break;
                        case ShaderUtil.ShaderPropertyType.TexEnv:
                            Texture texture = selectedMaterial.GetTexture(propName);
                            Debug.Log($"    纹理: {(texture != null ? texture.name : "None")}");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 优化材质
        /// </summary>
        private void OptimizeMaterial()
        {
            if (selectedMaterial == null)
            {
                Debug.LogWarning("请先选择一个材质");
                return;
            }
            
            Debug.Log($"优化材质: {selectedMaterial.name}");
            
            Shader shader = selectedMaterial.shader;
            if (shader == null)
            {
                Debug.LogWarning("材质没有关联的着色器");
                return;
            }
            
            int optimizedProperties = 0;
            
            // 检查未使用的属性
            int propertyCount = ShaderUtil.GetPropertyCount(shader);
            for (int i = 0; i < propertyCount; i++)
            {
                string propName = ShaderUtil.GetPropertyName(shader, i);
                ShaderUtil.ShaderPropertyType propType = ShaderUtil.GetPropertyType(shader, i);
                
                if (selectedMaterial.HasProperty(propName))
                {
                    // 检查属性是否使用默认值
                    bool isDefault = false;
                    
                    switch (propType)
                    {
                        case ShaderUtil.ShaderPropertyType.Float:
                            isDefault = Mathf.Approximately(selectedMaterial.GetFloat(propName), 0f);
                            break;
                        case ShaderUtil.ShaderPropertyType.Vector:
                            isDefault = selectedMaterial.GetVector(propName) == Vector4.zero;
                            break;
                        case ShaderUtil.ShaderPropertyType.Color:
                            isDefault = selectedMaterial.GetColor(propName) == Color.white;
                            break;
                        case ShaderUtil.ShaderPropertyType.TexEnv:
                            isDefault = selectedMaterial.GetTexture(propName) == null;
                            break;
                    }
                    
                    if (isDefault)
                    {
                        optimizedProperties++;
                        Debug.Log($"  属性 {propName} 使用默认值，可以优化");
                    }
                }
            }
            
            Debug.Log($"材质优化完成，发现 {optimizedProperties} 个可优化的属性");
        }

        /// <summary>
        /// 批量处理材质
        /// </summary>
        private void BatchProcessMaterials()
        {
            Material[] materials = FindObjectsOfType<Material>();
            
            Debug.Log($"批量处理 {materials.Length} 个材质:");
            
            int processedCount = 0;
            int errorCount = 0;
            
            foreach (Material material in materials)
            {
                try
                {
                    Shader shader = material.shader;
                    if (shader != null)
                    {
                        // 检查材质是否有错误
                        if (!ShaderUtil.HasErrors(shader))
                        {
                            processedCount++;
                            Debug.Log($"  处理材质: {material.name}");
                        }
                        else
                        {
                            errorCount++;
                            Debug.LogWarning($"  材质 {material.name} 的着色器有错误");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    errorCount++;
                    Debug.LogError($"  处理材质 {material.name} 时出错: {ex.Message}");
                }
            }
            
            Debug.Log($"批量处理完成，成功: {processedCount}，错误: {errorCount}");
        }

        #endregion

        #region 高级操作

        /// <summary>
        /// 着色器性能分析
        /// </summary>
        private void AnalyzeShaderPerformance()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            Debug.Log($"着色器 {selectedShader.name} 性能分析:");
            
            // 获取着色器复杂度
            int complexity = ShaderUtil.GetShaderComplexity(selectedShader);
            Debug.Log($"  复杂度: {complexity}");
            
            // 获取着色器大小
            int shaderSize = ShaderUtil.GetShaderSize(selectedShader);
            Debug.Log($"  大小: {shaderSize} 字节");
            
            // 获取变体数量
            int variantCount = ShaderUtil.GetVariantCount(selectedShader, true);
            Debug.Log($"  变体数量: {variantCount}");
            
            // 性能评级
            string performanceRating = GetPerformanceRating(complexity, shaderSize, variantCount);
            Debug.Log($"  性能评级: {performanceRating}");
        }

        /// <summary>
        /// 获取优化建议
        /// </summary>
        private void GetOptimizationSuggestions()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            Debug.Log($"着色器 {selectedShader.name} 优化建议:");
            
            int complexity = ShaderUtil.GetShaderComplexity(selectedShader);
            int shaderSize = ShaderUtil.GetShaderSize(selectedShader);
            int variantCount = ShaderUtil.GetVariantCount(selectedShader, true);
            
            // 复杂度建议
            if (complexity > 100)
            {
                Debug.Log("  建议: 着色器复杂度过高，考虑简化计算");
            }
            
            // 大小建议
            if (shaderSize > 10000)
            {
                Debug.Log("  建议: 着色器过大，考虑拆分或优化");
            }
            
            // 变体建议
            if (variantCount > 1000)
            {
                Debug.Log("  建议: 变体数量过多，考虑减少关键字");
            }
            
            // 属性建议
            int propertyCount = ShaderUtil.GetPropertyCount(selectedShader);
            if (propertyCount > 20)
            {
                Debug.Log("  建议: 属性数量过多，考虑合并或移除未使用的属性");
            }
        }

        /// <summary>
        /// 检查着色器兼容性
        /// </summary>
        private void CheckShaderCompatibility()
        {
            if (selectedShader == null)
            {
                Debug.LogWarning("请先选择一个着色器");
                return;
            }
            
            Debug.Log($"检查着色器 {selectedShader.name} 兼容性:");
            
            // 检查着色器是否支持当前平台
            bool supportsCurrentPlatform = ShaderUtil.SupportsPlatform(selectedShader, EditorUserBuildSettings.activeBuildTarget);
            Debug.Log($"  支持当前平台: {supportsCurrentPlatform}");
            
            // 检查着色器是否支持移动平台
            bool supportsMobile = ShaderUtil.SupportsPlatform(selectedShader, BuildTarget.Android) && 
                                 ShaderUtil.SupportsPlatform(selectedShader, BuildTarget.iOS);
            Debug.Log($"  支持移动平台: {supportsMobile}");
            
            // 检查着色器是否支持WebGL
            bool supportsWebGL = ShaderUtil.SupportsPlatform(selectedShader, BuildTarget.WebGL);
            Debug.Log($"  支持WebGL: {supportsWebGL}");
            
            // 兼容性建议
            if (!supportsMobile)
            {
                Debug.Log("  建议: 着色器不支持移动平台，可能需要优化");
            }
            
            if (!supportsWebGL)
            {
                Debug.Log("  建议: 着色器不支持WebGL，可能需要简化");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取性能评级
        /// </summary>
        private string GetPerformanceRating(int complexity, int shaderSize, int variantCount)
        {
            int score = 0;
            
            if (complexity < 50) score += 3;
            else if (complexity < 100) score += 2;
            else if (complexity < 200) score += 1;
            
            if (shaderSize < 5000) score += 3;
            else if (shaderSize < 10000) score += 2;
            else if (shaderSize < 20000) score += 1;
            
            if (variantCount < 100) score += 3;
            else if (variantCount < 500) score += 2;
            else if (variantCount < 1000) score += 1;
            
            if (score >= 8) return "优秀";
            else if (score >= 6) return "良好";
            else if (score >= 4) return "一般";
            else return "需要优化";
        }

        #endregion

        #region 数据结构

        /// <summary>
        /// 着色器属性信息
        /// </summary>
        [System.Serializable]
        public class ShaderPropertyInfo
        {
            public string name;
            public ShaderUtil.ShaderPropertyType type;
            public string description;
            public ShaderUtil.ShaderPropertyFlags flags;
        }

        #endregion
    }
}
