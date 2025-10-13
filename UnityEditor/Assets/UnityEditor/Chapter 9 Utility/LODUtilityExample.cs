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
    }
}
