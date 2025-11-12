using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;

namespace UnityEditor.Examples
{
    /// <summary>
    /// PrefabStageUtility 工具类示例
    /// 提供预制体场景舞台相关的实用工具功能
    /// </summary>
    public static class PrefabStageUtilityExample
    {
        #region 当前预制体舞台示例

        /// <summary>
        /// 获取当前预制体舞台
        /// </summary>
        public static void GetCurrentPrefabStageExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                Debug.Log($"当前预制体舞台信息:");
                Debug.Log($"- 预制体根对象: {prefabStage.prefabContentsRoot.name}");
                Debug.Log($"- 预制体路径: {prefabStage.assetPath}");
                Debug.Log($"- 预制体场景: {prefabStage.scene.name}");
                Debug.Log($"- 预制体有效: {prefabStage.IsValid()}");
            }
            else
            {
                Debug.Log("当前不在预制体舞台中");
            }
        }

        /// <summary>
        /// 检查预制体舞台状态
        /// </summary>
        public static void CheckPrefabStageStatusExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                Debug.Log($"预制体舞台状态:");
                Debug.Log($"- 预制体根对象: {prefabStage.prefabContentsRoot.name}");
                Debug.Log($"- 预制体路径: {prefabStage.assetPath}");
                Debug.Log($"- 预制体场景: {prefabStage.scene.name}");
                Debug.Log($"- 预制体有效: {prefabStage.IsValid()}");
                
                // 检查预制体修改状态
                bool hasModifications = PrefabUtility.HasPrefabInstanceAnyOverrides(prefabStage.prefabContentsRoot, false);
                Debug.Log($"- 是否有修改: {hasModifications}");
            }
            else
            {
                Debug.Log("当前不在预制体舞台中");
            }
        }

        #endregion

        #region 预制体舞台操作示例

        /// <summary>
        /// 打开预制体舞台
        /// </summary>
        public static void OpenPrefabStageExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("请选择一个GameObject");
                return;
            }

            // 检查是否为预制体实例
            if (PrefabUtility.IsPartOfPrefabInstance(selected))
            {
                string prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(selected);
                GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                
                if (prefabAsset != null)
                {
                    PrefabStage prefabStage = PrefabStageUtility.OpenPrefab(prefabPath);
                    
                    if (prefabStage != null)
                    {
                        Debug.Log($"预制体舞台已打开: {prefabStage.prefabContentsRoot.name}");
                        Debug.Log($"- 预制体路径: {prefabStage.assetPath}");
                    }
                }
            }
            else
            {
                Debug.LogWarning("选中的对象不是预制体实例");
            }
        }

        /// <summary>
        /// 关闭预制体舞台
        /// </summary>
        public static void ClosePrefabStageExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                string prefabName = prefabStage.prefabContentsRoot.name;
                PrefabStageUtility.ClosePrefabStage(prefabStage);
                Debug.Log($"预制体舞台已关闭: {prefabName}");
            }
            else
            {
                Debug.Log("当前没有打开的预制体舞台");
            }
        }

        #endregion

        #region 预制体内容示例

        /// <summary>
        /// 获取预制体内容根对象
        /// </summary>
        public static void GetPrefabContentsRootExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                GameObject rootObject = prefabStage.prefabContentsRoot;
                
                Debug.Log($"预制体内容根对象:");
                Debug.Log($"- 名称: {rootObject.name}");
                Debug.Log($"- 激活状态: {rootObject.activeSelf}");
                Debug.Log($"- 子对象数量: {rootObject.transform.childCount}");
                
                // 获取所有子对象
                Transform[] children = rootObject.GetComponentsInChildren<Transform>();
                Debug.Log($"- 总对象数量: {children.Length}");
            }
            else
            {
                Debug.Log("当前不在预制体舞台中");
            }
        }

        /// <summary>
        /// 检查预制体内容
        /// </summary>
        public static void CheckPrefabContentsExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                GameObject rootObject = prefabStage.prefabContentsRoot;
                
                Debug.Log($"预制体内容检查:");
                Debug.Log($"- 根对象: {rootObject.name}");
                
                // 检查组件
                Component[] components = rootObject.GetComponents<Component>();
                Debug.Log($"- 组件数量: {components.Length}");
                
                foreach (Component component in components)
                {
                    Debug.Log($"  - {component.GetType().Name}");
                }
                
                // 检查子对象
                Transform[] children = rootObject.GetComponentsInChildren<Transform>();
                Debug.Log($"- 子对象数量: {children.Length - 1}"); // 减去根对象本身
            }
        }

        #endregion

        #region 预制体修改示例

        /// <summary>
        /// 检查预制体修改
        /// </summary>
        public static void CheckPrefabModificationsExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                GameObject rootObject = prefabStage.prefabContentsRoot;
                
                Debug.Log($"预制体修改检查:");
                
                // 检查是否有修改
                bool hasModifications = PrefabUtility.HasPrefabInstanceAnyOverrides(rootObject, false);
                Debug.Log($"- 是否有修改: {hasModifications}");
                
                if (hasModifications)
                {
                    // 获取修改信息
                    PropertyModification[] modifications = PrefabUtility.GetPropertyModifications(rootObject);
                    Debug.Log($"- 修改数量: {modifications.Length}");
                    
                    foreach (PropertyModification modification in modifications)
                    {
                        Debug.Log($"  - 属性: {modification.propertyPath}");
                        Debug.Log($"  - 目标: {modification.target?.name}");
                    }
                }
            }
        }

        /// <summary>
        /// 应用预制体修改
        /// </summary>
        public static void ApplyPrefabModificationsExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                GameObject rootObject = prefabStage.prefabContentsRoot;
                
                // 检查是否有修改
                bool hasModifications = PrefabUtility.HasPrefabInstanceAnyOverrides(rootObject, false);
                
                if (hasModifications)
                {
                    // 应用修改
                    PrefabUtility.ApplyPrefabInstance(rootObject, InteractionMode.UserAction);
                    Debug.Log($"预制体修改已应用: {rootObject.name}");
                }
                else
                {
                    Debug.Log("预制体没有修改需要应用");
                }
            }
        }

        #endregion

        #region 预制体舞台场景示例

        /// <summary>
        /// 获取预制体舞台场景
        /// </summary>
        public static void GetPrefabStageSceneExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                Scene prefabScene = prefabStage.scene;
                
                Debug.Log($"预制体舞台场景:");
                Debug.Log($"- 场景名称: {prefabScene.name}");
                Debug.Log($"- 场景路径: {prefabScene.path}");
                Debug.Log($"- 场景已加载: {prefabScene.isLoaded}");
                Debug.Log($"- 场景有效: {prefabScene.IsValid()}");
                Debug.Log($"- 根对象数量: {prefabScene.rootCount}");
            }
            else
            {
                Debug.Log("当前不在预制体舞台中");
            }
        }

        /// <summary>
        /// 检查预制体舞台场景状态
        /// </summary>
        public static void CheckPrefabStageSceneStatusExample()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                Scene prefabScene = prefabStage.scene;
                
                Debug.Log($"预制体舞台场景状态:");
                Debug.Log($"- 场景名称: {prefabScene.name}");
                Debug.Log($"- 场景路径: {prefabScene.path}");
                Debug.Log($"- 场景已加载: {prefabScene.isLoaded}");
                
                // 获取场景中的所有根对象
                GameObject[] rootObjects = prefabScene.GetRootGameObjects();
                Debug.Log($"- 根对象数量: {rootObjects.Length}");
                
                foreach (GameObject rootObject in rootObjects)
                {
                    Debug.Log($"  - {rootObject.name}");
                }
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 预制体舞台诊断
        /// </summary>
        public static void PrefabStageDiagnosticsExample()
        {
            Debug.Log("=== 预制体舞台诊断 ===");
            
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                Debug.Log($"✓ 预制体舞台: {prefabStage.prefabContentsRoot.name}");
                Debug.Log($"✓ 预制体路径: {prefabStage.assetPath}");
                Debug.Log($"✓ 预制体有效: {prefabStage.IsValid()}");
                
                // 预制体内容
                GameObject rootObject = prefabStage.prefabContentsRoot;
                Debug.Log($"✓ 根对象: {rootObject.name}");
                Debug.Log($"✓ 子对象数量: {rootObject.transform.childCount}");
                
                // 预制体修改
                bool hasModifications = PrefabUtility.HasPrefabInstanceAnyOverrides(rootObject, false);
                Debug.Log($"✓ 是否有修改: {hasModifications}");
                
                // 预制体场景
                Scene prefabScene = prefabStage.scene;
                Debug.Log($"✓ 预制体场景: {prefabScene.name}");
                Debug.Log($"✓ 场景已加载: {prefabScene.isLoaded}");
            }
            else
            {
                Debug.Log("✗ 当前不在预制体舞台中");
            }
        }

        /// <summary>
        /// 批量预制体舞台处理
        /// </summary>
        public static void BatchPrefabStageProcessingExample()
        {
            Debug.Log("批量预制体舞台处理:");
            
            // 查找所有预制体
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            
            Debug.Log($"找到 {guids.Length} 个预制体:");
            
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                
                if (prefab != null)
                {
                    Debug.Log($"- 处理预制体: {prefab.name} ({path})");
                    
                    // 这里可以添加具体的处理逻辑
                    // 例如：打开预制体舞台、检查修改等
                }
            }
        }

        /// <summary>
        /// 创建预制体舞台测试
        /// </summary>
        public static void CreatePrefabStageTestExample()
        {
            // 创建测试预制体
            GameObject testPrefab = new GameObject("TestPrefab");
            testPrefab.AddComponent<MeshRenderer>();
            testPrefab.AddComponent<MeshFilter>();
            
            // 保存为预制体
            string prefabPath = "Assets/TestPrefab.prefab";
            GameObject prefabAsset = PrefabUtility.SaveAsPrefabAsset(testPrefab, prefabPath);
            
            if (prefabAsset != null)
            {
                // 打开预制体舞台
                PrefabStage prefabStage = PrefabStageUtility.OpenPrefab(prefabPath);
                
                if (prefabStage != null)
                {
                    Debug.Log($"预制体舞台测试已创建: {prefabStage.prefabContentsRoot.name}");
                    Debug.Log($"- 预制体路径: {prefabStage.assetPath}");
                }
                
                // 清理
                Object.DestroyImmediate(testPrefab);
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查是否在预制体舞台中
        /// </summary>
        private static bool IsInPrefabStage()
        {
            return PrefabStageUtility.GetCurrentPrefabStage() != null;
        }

        /// <summary>
        /// 获取预制体舞台信息
        /// </summary>
        private static string GetPrefabStageInfo()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage != null)
            {
                return $"预制体舞台: {prefabStage.prefabContentsRoot.name} ({prefabStage.assetPath})";
            }
            
            return "不在预制体舞台中";
        }

        #endregion
    }
}
