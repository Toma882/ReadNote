using UnityEngine;
using UnityEditor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// LODUtility 工具类示例
    /// 提供LOD（细节层次）相关的实用工具功能
    /// </summary>
    public static class LODUtilityExample
    {
        #region LOD组包围盒示例

        /// <summary>
        /// 计算LOD组包围盒
        /// </summary>
        public static void CalculateLODGroupBoundingBoxExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            Bounds bounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
            
            Debug.Log($"LOD组 {lodGroup.name} 的包围盒:");
            Debug.Log($"- 中心: {bounds.center}");
            Debug.Log($"- 大小: {bounds.size}");
            Debug.Log($"- 最小点: {bounds.min}");
            Debug.Log($"- 最大点: {bounds.max}");
        }

        /// <summary>
        /// 计算LOD组包围盒（带验证）
        /// </summary>
        public static void CalculateLODGroupBoundingBoxWithValidationExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            Bounds bounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
            
            if (bounds.size.magnitude > 0)
            {
                Debug.Log($"LOD组包围盒有效:");
                Debug.Log($"- 中心: {bounds.center}");
                Debug.Log($"- 大小: {bounds.size}");
                Debug.Log($"- 体积: {bounds.size.x * bounds.size.y * bounds.size.z}");
            }
            else
            {
                Debug.LogWarning("LOD组包围盒无效或为空");
            }
        }

        #endregion

        #region LOD组信息示例

        /// <summary>
        /// 获取LOD组信息
        /// </summary>
        public static void GetLODGroupInfoExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            LOD[] lods = lodGroup.GetLODs();
            
            Debug.Log($"LOD组 {lodGroup.name} 信息:");
            Debug.Log($"- LOD级别数量: {lods.Length}");
            Debug.Log($"- 淡入淡出模式: {lodGroup.fadeMode}");
            Debug.Log($"- 动画交叉淡入淡出: {lodGroup.animateCrossFading}");
            Debug.Log($"- 本地参考点: {lodGroup.localReferencePoint}");
            Debug.Log($"- 大小: {lodGroup.size}");
            
            for (int i = 0; i < lods.Length; i++)
            {
                Debug.Log($"LOD {i}:");
                Debug.Log($"  - 屏幕相对高度: {lods[i].screenRelativeTransitionHeight}");
                Debug.Log($"  - 淡入淡出过渡宽度: {lods[i].fadeTransitionWidth}");
                Debug.Log($"  - 渲染器数量: {lods[i].renderers.Length}");
            }
        }

        /// <summary>
        /// 设置LOD组信息
        /// </summary>
        public static void SetLODGroupInfoExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            // 设置LOD组属性
            lodGroup.fadeMode = LODFadeMode.CrossFade;
            lodGroup.animateCrossFading = true;
            
            Debug.Log($"LOD组 {lodGroup.name} 属性已设置:");
            Debug.Log($"- 淡入淡出模式: {lodGroup.fadeMode}");
            Debug.Log($"- 动画交叉淡入淡出: {lodGroup.animateCrossFading}");
        }

        #endregion

        #region LOD级别操作示例

        /// <summary>
        /// 创建LOD级别
        /// </summary>
        public static void CreateLODLevelsExample()
        {
            GameObject obj = Selection.activeGameObject;
            if (obj == null) return;

            // 添加LOD组件
            LODGroup lodGroup = obj.GetComponent<LODGroup>();
            if (lodGroup == null)
            {
                lodGroup = obj.AddComponent<LODGroup>();
            }

            // 创建LOD级别
            LOD[] lods = new LOD[3];
            
            // LOD 0 - 高质量
            lods[0] = new LOD(0.6f, obj.GetComponentsInChildren<Renderer>());
            
            // LOD 1 - 中等质量
            lods[1] = new LOD(0.3f, obj.GetComponentsInChildren<Renderer>());
            
            // LOD 2 - 低质量
            lods[2] = new LOD(0.1f, obj.GetComponentsInChildren<Renderer>());
            
            lodGroup.SetLODs(lods);
            lodGroup.RecalculateBounds();
            
            Debug.Log($"LOD级别已创建，共 {lods.Length} 个级别");
        }

        /// <summary>
        /// 修改LOD级别
        /// </summary>
        public static void ModifyLODLevelsExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            LOD[] lods = lodGroup.GetLODs();
            
            // 修改LOD级别的屏幕相对高度
            for (int i = 0; i < lods.Length; i++)
            {
                lods[i].screenRelativeTransitionHeight = 1.0f - (i * 0.3f);
            }
            
            lodGroup.SetLODs(lods);
            
            Debug.Log($"LOD级别已修改，共 {lods.Length} 个级别");
        }

        #endregion

        #region LOD渲染器操作示例

        /// <summary>
        /// 获取LOD渲染器
        /// </summary>
        public static void GetLODRenderersExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            LOD[] lods = lodGroup.GetLODs();
            
            for (int i = 0; i < lods.Length; i++)
            {
                Debug.Log($"LOD {i} 渲染器:");
                
                foreach (Renderer renderer in lods[i].renderers)
                {
                    if (renderer != null)
                    {
                        Debug.Log($"  - {renderer.name} ({renderer.GetType().Name})");
                    }
                }
            }
        }

        /// <summary>
        /// 设置LOD渲染器
        /// </summary>
        public static void SetLODRenderersExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            LOD[] lods = lodGroup.GetLODs();
            
            if (lods.Length > 0)
            {
                // 获取所有渲染器
                Renderer[] renderers = lodGroup.GetComponentsInChildren<Renderer>();
                
                // 设置LOD 0的渲染器
                lods[0].renderers = renderers;
                
                lodGroup.SetLODs(lods);
                
                Debug.Log($"LOD 0 渲染器已设置，共 {renderers.Length} 个渲染器");
            }
        }

        #endregion

        #region LOD包围盒操作示例

        /// <summary>
        /// 重新计算LOD包围盒
        /// </summary>
        public static void RecalculateBoundsExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            lodGroup.RecalculateBounds();
            
            Bounds bounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
            
            Debug.Log($"LOD包围盒已重新计算:");
            Debug.Log($"- 中心: {bounds.center}");
            Debug.Log($"- 大小: {bounds.size}");
        }

        /// <summary>
        /// 比较LOD包围盒
        /// </summary>
        public static void CompareLODBoundsExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            // 计算前的包围盒
            Bounds beforeBounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
            
            // 重新计算
            lodGroup.RecalculateBounds();
            
            // 计算后的包围盒
            Bounds afterBounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
            
            Debug.Log("LOD包围盒比较:");
            Debug.Log($"- 计算前: {beforeBounds.size}");
            Debug.Log($"- 计算后: {afterBounds.size}");
            Debug.Log($"- 变化: {afterBounds.size - beforeBounds.size}");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建LOD组测试
        /// </summary>
        public static void CreateLODGroupTestExample()
        {
            // 创建根对象
            GameObject rootObj = new GameObject("LODTestObject");
            
            // 添加LOD组件
            LODGroup lodGroup = rootObj.AddComponent<LODGroup>();
            
            // 创建LOD级别的子对象
            GameObject[] lodObjects = new GameObject[3];
            
            for (int i = 0; i < 3; i++)
            {
                lodObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lodObjects[i].name = $"LOD{i}";
                lodObjects[i].transform.SetParent(rootObj.transform);
                lodObjects[i].transform.localPosition = Vector3.zero;
                lodObjects[i].transform.localScale = Vector3.one * (1.0f - i * 0.3f);
            }
            
            // 设置LOD级别
            LOD[] lods = new LOD[3];
            lods[0] = new LOD(0.6f, new Renderer[] { lodObjects[0].GetComponent<Renderer>() });
            lods[1] = new LOD(0.3f, new Renderer[] { lodObjects[1].GetComponent<Renderer>() });
            lods[2] = new LOD(0.1f, new Renderer[] { lodObjects[2].GetComponent<Renderer>() });
            
            lodGroup.SetLODs(lods);
            lodGroup.RecalculateBounds();
            
            // 计算包围盒
            Bounds bounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
            
            Debug.Log($"LOD组测试已创建: {rootObj.name}");
            Debug.Log($"- LOD级别数量: {lods.Length}");
            Debug.Log($"- 包围盒: {bounds}");
        }

        /// <summary>
        /// 批量处理LOD组
        /// </summary>
        public static void BatchProcessLODGroupsExample()
        {
            LODGroup[] lodGroups = Object.FindObjectsOfType<LODGroup>();
            
            Debug.Log($"批量处理 {lodGroups.Length} 个LOD组:");
            
            foreach (LODGroup lodGroup in lodGroups)
            {
                // 重新计算包围盒
                lodGroup.RecalculateBounds();
                
                // 计算包围盒
                Bounds bounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
                
                Debug.Log($"处理LOD组: {lodGroup.name}");
                Debug.Log($"  - 包围盒大小: {bounds.size}");
                Debug.Log($"  - LOD级别数量: {lodGroup.GetLODs().Length}");
            }
        }

        /// <summary>
        /// LOD组诊断
        /// </summary>
        public static void LODGroupDiagnosticsExample()
        {
            LODGroup lodGroup = GetSelectedLODGroup();
            if (lodGroup == null) return;

            Debug.Log($"=== LOD组诊断: {lodGroup.name} ===");
            
            // 基本信息
            LOD[] lods = lodGroup.GetLODs();
            Debug.Log($"✓ LOD级别数量: {lods.Length}");
            Debug.Log($"✓ 淡入淡出模式: {lodGroup.fadeMode}");
            Debug.Log($"✓ 动画交叉淡入淡出: {lodGroup.animateCrossFading}");
            
            // 包围盒信息
            Bounds bounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
            Debug.Log($"✓ 包围盒中心: {bounds.center}");
            Debug.Log($"✓ 包围盒大小: {bounds.size}");
            
            // LOD级别详情
            for (int i = 0; i < lods.Length; i++)
            {
                Debug.Log($"✓ LOD {i}:");
                Debug.Log($"  - 屏幕相对高度: {lods[i].screenRelativeTransitionHeight}");
                Debug.Log($"  - 渲染器数量: {lods[i].renderers.Length}");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取选中的LOD组
        /// </summary>
        private static LODGroup GetSelectedLODGroup()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                LODGroup lodGroup = selected.GetComponent<LODGroup>();
                if (lodGroup != null)
                {
                    return lodGroup;
                }
            }

            // 查找场景中的第一个LOD组
            LODGroup[] lodGroups = Object.FindObjectsOfType<LODGroup>();
            if (lodGroups.Length > 0)
            {
                return lodGroups[0];
            }

            Debug.LogWarning("未找到LOD组");
            return null;
        }

        #endregion

        #region 高级LOD操作示例

        /// <summary>
        /// LOD组管理
        /// </summary>
        public static void LODGroupManagementExample()
        {
            Debug.Log("=== LOD组管理 ===");
            
            LODGroup[] allLODGroups = FindObjectsOfType<LODGroup>();
            Debug.Log($"场景中LOD组数量: {allLODGroups.Length}");
            
            foreach (LODGroup lodGroup in allLODGroups)
            {
                Debug.Log($"LOD组: {lodGroup.name}");
                
                // 获取LOD信息
                LOD[] lods = lodGroup.GetLODs();
                Debug.Log($"  LOD级别数量: {lods.Length}");
                
                for (int i = 0; i < lods.Length; i++)
                {
                    Debug.Log($"    LOD {i}: 屏幕高度 {lods[i].screenRelativeTransitionHeight:F3}, 渲染器数量 {lods[i].renderers.Length}");
                }
                
                // 获取包围盒
                Bounds bounds = LODUtility.CalculateLODGroupBoundingBox(lodGroup);
                Debug.Log($"  包围盒大小: {bounds.size}");
            }
        }

        /// <summary>
        /// LOD性能分析
        /// </summary>
        public static void LODPerformanceAnalysisExample()
        {
            Debug.Log("=== LOD性能分析 ===");
            
            LODGroup[] allLODGroups = FindObjectsOfType<LODGroup>();
            
            int totalLODGroups = allLODGroups.Length;
            int totalLODLevels = 0;
            int totalRenderers = 0;
            float totalScreenSpace = 0f;
            
            foreach (LODGroup lodGroup in allLODGroups)
            {
                LOD[] lods = lodGroup.GetLODs();
                totalLODLevels += lods.Length;
                
                foreach (LOD lod in lods)
                {
                    totalRenderers += lod.renderers.Length;
                    totalScreenSpace += lod.screenRelativeTransitionHeight;
                }
            }
            
            Debug.Log($"=== LOD性能统计 ===");
            Debug.Log($"总LOD组数量: {totalLODGroups}");
            Debug.Log($"总LOD级别数量: {totalLODLevels}");
            Debug.Log($"总渲染器数量: {totalRenderers}");
            Debug.Log($"平均LOD级别: {totalLODLevels / (float)totalLODGroups:F2}");
            Debug.Log($"平均渲染器数量: {totalRenderers / (float)totalLODLevels:F2}");
            Debug.Log($"平均屏幕空间: {totalScreenSpace / totalLODLevels:F3}");
        }

        /// <summary>
        /// LOD优化建议
        /// </summary>
        public static void LODOptimizationSuggestionsExample()
        {
            Debug.Log("=== LOD优化建议 ===");
            
            LODGroup[] allLODGroups = FindObjectsOfType<LODGroup>();
            
            foreach (LODGroup lodGroup in allLODGroups)
            {
                LOD[] lods = lodGroup.GetLODs();
                
                Debug.Log($"LOD组: {lodGroup.name}");
                
                // 检查LOD级别数量
                if (lods.Length < 2)
                {
                    Debug.LogWarning("  建议: LOD级别数量过少，建议至少2个级别");
                }
                else if (lods.Length > 4)
                {
                    Debug.LogWarning("  建议: LOD级别数量过多，建议不超过4个级别");
                }
                
                // 检查屏幕空间分布
                for (int i = 0; i < lods.Length - 1; i++)
                {
                    float currentScreenSpace = lods[i].screenRelativeTransitionHeight;
                    float nextScreenSpace = lods[i + 1].screenRelativeTransitionHeight;
                    float ratio = currentScreenSpace / nextScreenSpace;
                    
                    if (ratio < 1.5f)
                    {
                        Debug.LogWarning($"  建议: LOD {i} 和 LOD {i + 1} 的屏幕空间差距过小 ({ratio:F2})");
                    }
                }
                
                // 检查渲染器数量
                for (int i = 0; i < lods.Length; i++)
                {
                    int rendererCount = lods[i].renderers.Length;
                    if (rendererCount == 0)
                    {
                        Debug.LogWarning($"  警告: LOD {i} 没有渲染器");
                    }
                    else if (rendererCount > 10)
                    {
                        Debug.LogWarning($"  建议: LOD {i} 渲染器数量过多 ({rendererCount})");
                    }
                }
            }
        }

        /// <summary>
        /// LOD验证
        /// </summary>
        public static void LODValidationExample()
        {
            Debug.Log("=== LOD验证 ===");
            
            LODGroup[] allLODGroups = FindObjectsOfType<LODGroup>();
            
            foreach (LODGroup lodGroup in allLODGroups)
            {
                Debug.Log($"验证LOD组: {lodGroup.name}");
                
                LOD[] lods = lodGroup.GetLODs();
                bool isValid = true;
                
                // 验证LOD级别数量
                if (lods.Length == 0)
                {
                    Debug.LogError("  错误: LOD组没有LOD级别");
                    isValid = false;
                }
                
                // 验证屏幕空间值
                for (int i = 0; i < lods.Length; i++)
                {
                    float screenSpace = lods[i].screenRelativeTransitionHeight;
                    if (screenSpace < 0f || screenSpace > 1f)
                    {
                        Debug.LogError($"  错误: LOD {i} 屏幕空间值无效 ({screenSpace})");
                        isValid = false;
                    }
                }
                
                // 验证屏幕空间顺序
                for (int i = 0; i < lods.Length - 1; i++)
                {
                    if (lods[i].screenRelativeTransitionHeight <= lods[i + 1].screenRelativeTransitionHeight)
                    {
                        Debug.LogError($"  错误: LOD {i} 屏幕空间值应该大于 LOD {i + 1}");
                        isValid = false;
                    }
                }
                
                // 验证渲染器
                for (int i = 0; i < lods.Length; i++)
                {
                    foreach (Renderer renderer in lods[i].renderers)
                    {
                        if (renderer == null)
                        {
                            Debug.LogError($"  错误: LOD {i} 包含空渲染器");
                            isValid = false;
                        }
                        else if (renderer.gameObject == null)
                        {
                            Debug.LogError($"  错误: LOD {i} 包含无效渲染器");
                            isValid = false;
                        }
                    }
                }
                
                Debug.Log($"  验证结果: {(isValid ? "通过" : "失败")}");
            }
        }

        #endregion

        #region LOD工具示例

        /// <summary>
        /// LOD工具函数
        /// </summary>
        public static void LODToolsExample()
        {
            Debug.Log("=== LOD工具函数 ===");
            
            LODGroup selectedLODGroup = GetSelectedLODGroup();
            if (selectedLODGroup == null)
            {
                Debug.LogWarning("请先选择一个LOD组");
                return;
            }
            
            // 获取LOD信息
            LODInfo info = GetLODInfo(selectedLODGroup);
            Debug.Log($"LOD信息: {info}");
            
            // 检查LOD状态
            bool isActive = selectedLODGroup.enabled;
            bool isStatic = selectedLODGroup.gameObject.isStatic;
            Debug.Log($"是否激活: {isActive}");
            Debug.Log($"是否静态: {isStatic}");
            
            // 获取LOD距离
            float[] distances = GetLODDistances(selectedLODGroup);
            Debug.Log($"LOD距离: {string.Join(", ", distances)}");
            
            // 检查LOD可见性
            bool isVisible = IsLODGroupVisible(selectedLODGroup);
            Debug.Log($"是否可见: {isVisible}");
        }

        /// <summary>
        /// LOD调试
        /// </summary>
        public static void LODDebuggingExample()
        {
            Debug.Log("=== LOD调试 ===");
            
            LODGroup[] allLODGroups = FindObjectsOfType<LODGroup>();
            
            foreach (LODGroup lodGroup in allLODGroups)
            {
                Debug.Log($"调试LOD组: {lodGroup.name}");
                
                // 获取当前LOD级别
                int currentLOD = GetCurrentLODLevel(lodGroup);
                Debug.Log($"  当前LOD级别: {currentLOD}");
                
                // 获取LOD切换距离
                float[] switchDistances = GetLODSwitchDistances(lodGroup);
                Debug.Log($"  LOD切换距离: {string.Join(", ", switchDistances)}");
                
                // 获取LOD性能统计
                LODPerformanceStats stats = GetLODPerformanceStats(lodGroup);
                Debug.Log($"  LOD性能统计: {stats}");
                
                // 检查LOD错误
                string[] errors = GetLODErrors(lodGroup);
                if (errors.Length > 0)
                {
                    Debug.LogWarning($"  LOD错误数量: {errors.Length}");
                    foreach (string error in errors)
                    {
                        Debug.LogWarning($"    {error}");
                    }
                }
            }
        }

        #endregion

        #region LOD管理示例

        /// <summary>
        /// LOD管理
        /// </summary>
        public static void LODManagementExample()
        {
            Debug.Log("=== LOD管理 ===");
            
            // 获取所有LOD组
            LODGroup[] allLODGroups = FindObjectsOfType<LODGroup>();
            Debug.Log($"场景中LOD组数量: {allLODGroups.Length}");
            
            // 按类型分类
            Dictionary<string, List<LODGroup>> typeGroups = new Dictionary<string, List<LODGroup>>();
            
            foreach (LODGroup lodGroup in allLODGroups)
            {
                string type = GetLODGroupType(lodGroup);
                if (!typeGroups.ContainsKey(type))
                {
                    typeGroups[type] = new List<LODGroup>();
                }
                typeGroups[type].Add(lodGroup);
            }
            
            foreach (var group in typeGroups)
            {
                Debug.Log($"类型 {group.Key}: {group.Value.Count} 个LOD组");
            }
            
            // 按LOD级别分类
            Dictionary<int, List<LODGroup>> levelGroups = new Dictionary<int, List<LODGroup>>();
            
            foreach (LODGroup lodGroup in allLODGroups)
            {
                int levelCount = lodGroup.GetLODs().Length;
                if (!levelGroups.ContainsKey(levelCount))
                {
                    levelGroups[levelCount] = new List<LODGroup>();
                }
                levelGroups[levelCount].Add(lodGroup);
            }
            
            foreach (var group in levelGroups)
            {
                Debug.Log($"LOD级别 {group.Key}: {group.Value.Count} 个LOD组");
            }
        }

        /// <summary>
        /// LOD统计
        /// </summary>
        public static void LODStatisticsExample()
        {
            Debug.Log("=== LOD统计 ===");
            
            LODGroup[] allLODGroups = FindObjectsOfType<LODGroup>();
            
            int totalLODGroups = allLODGroups.Length;
            int totalLODLevels = 0;
            int totalRenderers = 0;
            int activeLODGroups = 0;
            int staticLODGroups = 0;
            
            foreach (LODGroup lodGroup in allLODGroups)
            {
                if (lodGroup.enabled) activeLODGroups++;
                if (lodGroup.gameObject.isStatic) staticLODGroups++;
                
                LOD[] lods = lodGroup.GetLODs();
                totalLODLevels += lods.Length;
                
                foreach (LOD lod in lods)
                {
                    totalRenderers += lod.renderers.Length;
                }
            }
            
            Debug.Log($"=== LOD统计 ===");
            Debug.Log($"总LOD组数: {totalLODGroups}");
            Debug.Log($"激活LOD组数: {activeLODGroups}");
            Debug.Log($"静态LOD组数: {staticLODGroups}");
            Debug.Log($"总LOD级别数: {totalLODLevels}");
            Debug.Log($"总渲染器数: {totalRenderers}");
            Debug.Log($"平均LOD级别: {totalLODLevels / (float)totalLODGroups:F2}");
            Debug.Log($"平均渲染器数: {totalRenderers / (float)totalLODLevels:F2}");
        }

        #endregion
