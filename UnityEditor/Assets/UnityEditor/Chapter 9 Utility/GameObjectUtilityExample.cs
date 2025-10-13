using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace UnityEditor.Examples
{
    /// <summary>
    /// GameObjectUtility 工具类示例
    /// 提供游戏对象相关的实用工具功能
    /// </summary>
    public static class GameObjectUtilityExample
    {
        #region 导航网格区域示例

        /// <summary>
        /// 从名称获取导航网格区域
        /// </summary>
        public static void GetNavMeshAreaFromNameExample()
        {
            string areaName = "Walkable";
            int areaIndex = GameObjectUtility.GetNavMeshAreaFromName(areaName);
            
            Debug.Log($"导航网格区域 '{areaName}' 的索引: {areaIndex}");
        }

        /// <summary>
        /// 获取所有导航网格区域名称
        /// </summary>
        public static void GetNavMeshAreaNamesExample()
        {
            string[] areaNames = GameObjectUtility.GetNavMeshAreaNames();
            
            Debug.Log($"导航网格区域数量: {areaNames.Length}");
            
            for (int i = 0; i < areaNames.Length; i++)
            {
                Debug.Log($"区域 {i}: {areaNames[i]}");
            }
        }

        /// <summary>
        /// 设置游戏对象导航区域
        /// </summary>
        public static void SetNavigationAreaExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            int walkableArea = GameObjectUtility.GetNavMeshAreaFromName("Walkable");
            GameObjectUtility.SetNavMeshArea(selected, walkableArea);
            
            Debug.Log($"游戏对象 {selected.name} 的导航区域已设置为: Walkable");
        }

        #endregion

        #region 游戏对象状态示例

        /// <summary>
        /// 获取游戏对象状态标志
        /// </summary>
        public static void GetStaticEditorFlagsExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            StaticEditorFlags flags = GameObjectUtility.GetStaticEditorFlags(selected);
            
            Debug.Log($"游戏对象 {selected.name} 的静态标志:");
            Debug.Log($"- 标志值: {flags}");
            Debug.Log($"- 是否静态: {flags != 0}");
            
            // 检查具体标志
            if ((flags & StaticEditorFlags.LightmapStatic) != 0)
                Debug.Log("- 光照贴图静态: 是");
            if ((flags & StaticEditorFlags.OccluderStatic) != 0)
                Debug.Log("- 遮挡剔除静态: 是");
            if ((flags & StaticEditorFlags.BatchingStatic) != 0)
                Debug.Log("- 批处理静态: 是");
            if ((flags & StaticEditorFlags.NavigationStatic) != 0)
                Debug.Log("- 导航静态: 是");
        }

        /// <summary>
        /// 设置游戏对象状态标志
        /// </summary>
        public static void SetStaticEditorFlagsExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            // 设置为完全静态
            StaticEditorFlags flags = StaticEditorFlags.LightmapStatic | 
                                     StaticEditorFlags.OccluderStatic | 
                                     StaticEditorFlags.BatchingStatic;
            
            GameObjectUtility.SetStaticEditorFlags(selected, flags);
            
            Debug.Log($"游戏对象 {selected.name} 的静态标志已设置");
        }

        #endregion

        #region 游戏对象层级示例

        /// <summary>
        /// 获取游戏对象层级
        /// </summary>
        public static void GetLayerExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            int layer = selected.layer;
            string layerName = LayerMask.LayerToName(layer);
            
            Debug.Log($"游戏对象 {selected.name} 的层级:");
            Debug.Log($"- 层级索引: {layer}");
            Debug.Log($"- 层级名称: {layerName}");
        }

        /// <summary>
        /// 设置游戏对象层级
        /// </summary>
        public static void SetLayerExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            int defaultLayer = LayerMask.NameToLayer("Default");
            selected.layer = defaultLayer;
            
            Debug.Log($"游戏对象 {selected.name} 的层级已设置为: Default");
        }

        /// <summary>
        /// 递归设置层级
        /// </summary>
        public static void SetLayerRecursivelyExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            int layer = LayerMask.NameToLayer("UI");
            SetLayerRecursively(selected, layer);
            
            Debug.Log($"游戏对象 {selected.name} 及其子对象的层级已设置为: UI");
        }

        #endregion

        #region 游戏对象标签示例

        /// <summary>
        /// 获取游戏对象标签
        /// </summary>
        public static void GetTagExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            string tag = selected.tag;
            Debug.Log($"游戏对象 {selected.name} 的标签: {tag}");
        }

        /// <summary>
        /// 设置游戏对象标签
        /// </summary>
        public static void SetTagExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            selected.tag = "Player";
            Debug.Log($"游戏对象 {selected.name} 的标签已设置为: Player");
        }

        /// <summary>
        /// 比较标签
        /// </summary>
        public static void CompareTagExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            bool isPlayer = selected.CompareTag("Player");
            Debug.Log($"游戏对象 {selected.name} 是否为Player: {isPlayer}");
        }

        #endregion

        #region 游戏对象激活状态示例

        /// <summary>
        /// 获取激活状态
        /// </summary>
        public static void GetActiveStateExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            bool activeSelf = selected.activeSelf;
            bool activeInHierarchy = selected.activeInHierarchy;
            
            Debug.Log($"游戏对象 {selected.name} 的激活状态:");
            Debug.Log($"- 自身激活: {activeSelf}");
            Debug.Log($"- 层级激活: {activeInHierarchy}");
        }

        /// <summary>
        /// 设置激活状态
        /// </summary>
        public static void SetActiveExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            bool currentState = selected.activeSelf;
            selected.SetActive(!currentState);
            
            Debug.Log($"游戏对象 {selected.name} 的激活状态已切换为: {!currentState}");
        }

        #endregion

        #region 游戏对象图标示例

        /// <summary>
        /// 设置游戏对象图标
        /// </summary>
        public static void SetIconExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Texture2D icon = EditorGUIUtility.FindTexture("d_GameObject Icon");
            GameObjectUtility.SetIcon(selected, icon);
            
            Debug.Log($"游戏对象 {selected.name} 的图标已设置");
        }

        /// <summary>
        /// 获取游戏对象图标
        /// </summary>
        public static void GetIconExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Texture2D icon = GameObjectUtility.GetIcon(selected);
            
            if (icon != null)
            {
                Debug.Log($"游戏对象 {selected.name} 的图标: {icon.name}");
            }
            else
            {
                Debug.Log($"游戏对象 {selected.name} 没有设置图标");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建游戏对象测试
        /// </summary>
        public static void CreateGameObjectTestExample()
        {
            // 创建游戏对象
            GameObject testObj = new GameObject("TestGameObject");
            
            // 设置层级
            testObj.layer = LayerMask.NameToLayer("Default");
            
            // 设置标签
            testObj.tag = "Untagged";
            
            // 设置静态标志
            GameObjectUtility.SetStaticEditorFlags(testObj, StaticEditorFlags.BatchingStatic);
            
            // 设置导航区域
            int walkableArea = GameObjectUtility.GetNavMeshAreaFromName("Walkable");
            GameObjectUtility.SetNavMeshArea(testObj, walkableArea);
            
            Debug.Log($"测试游戏对象已创建: {testObj.name}");
            Debug.Log($"- 层级: {LayerMask.LayerToName(testObj.layer)}");
            Debug.Log($"- 标签: {testObj.tag}");
            Debug.Log($"- 静态标志: {GameObjectUtility.GetStaticEditorFlags(testObj)}");
        }

        /// <summary>
        /// 批量处理游戏对象
        /// </summary>
        public static void BatchProcessGameObjectsExample()
        {
            GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
            int processedCount = 0;

            foreach (GameObject obj in allObjects)
            {
                // 检查并设置静态标志
                StaticEditorFlags flags = GameObjectUtility.GetStaticEditorFlags(obj);
                
                if (flags == 0 && obj.GetComponent<MeshRenderer>() != null)
                {
                    // 为有MeshRenderer的对象设置批处理静态
                    GameObjectUtility.SetStaticEditorFlags(obj, StaticEditorFlags.BatchingStatic);
                    processedCount++;
                }
            }

            Debug.Log($"批量处理完成，共处理 {processedCount} 个游戏对象");
        }

        /// <summary>
        /// 游戏对象诊断
        /// </summary>
        public static void GameObjectDiagnosticsExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Debug.Log($"=== 游戏对象诊断: {selected.name} ===");
            
            // 基本信息
            Debug.Log($"✓ 层级: {LayerMask.LayerToName(selected.layer)}");
            Debug.Log($"✓ 标签: {selected.tag}");
            Debug.Log($"✓ 激活状态: {selected.activeSelf}");
            
            // 静态标志
            StaticEditorFlags flags = GameObjectUtility.GetStaticEditorFlags(selected);
            Debug.Log($"✓ 静态标志: {flags}");
            
            // 导航区域
            string[] areaNames = GameObjectUtility.GetNavMeshAreaNames();
            Debug.Log($"✓ 可用导航区域数量: {areaNames.Length}");
            
            // 组件信息
            Component[] components = selected.GetComponents<Component>();
            Debug.Log($"✓ 组件数量: {components.Length}");
            
            // 子对象信息
            Debug.Log($"✓ 子对象数量: {selected.transform.childCount}");
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 递归设置层级
        /// </summary>
        private static void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }

        /// <summary>
        /// 检查游戏对象是否静态
        /// </summary>
        private static bool IsGameObjectStatic(GameObject obj)
        {
            StaticEditorFlags flags = GameObjectUtility.GetStaticEditorFlags(obj);
            return flags != 0;
        }

        #endregion

        #region 高级游戏对象操作示例

        /// <summary>
        /// 游戏对象层级管理
        /// </summary>
        public static void GameObjectHierarchyManagementExample()
        {
            Debug.Log("=== 游戏对象层级管理 ===");
            
            GameObject[] selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 0)
            {
                Debug.LogWarning("请先选择游戏对象");
                return;
            }
            
            foreach (GameObject obj in selectedObjects)
            {
                // 获取层级信息
                int depth = GetGameObjectDepth(obj);
                string hierarchyPath = GetGameObjectHierarchyPath(obj);
                
                Debug.Log($"对象: {obj.name}");
                Debug.Log($"  层级深度: {depth}");
                Debug.Log($"  层级路径: {hierarchyPath}");
                
                // 检查是否为根对象
                bool isRoot = obj.transform.parent == null;
                Debug.Log($"  是否为根对象: {isRoot}");
                
                // 获取子对象数量
                int childCount = obj.transform.childCount;
                Debug.Log($"  子对象数量: {childCount}");
            }
        }

        /// <summary>
        /// 游戏对象组件管理
        /// </summary>
        public static void GameObjectComponentManagementExample()
        {
            Debug.Log("=== 游戏对象组件管理 ===");
            
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            // 获取所有组件
            Component[] components = selected.GetComponents<Component>();
            Debug.Log($"对象 '{selected.name}' 的组件数量: {components.Length}");
            
            foreach (Component component in components)
            {
                Debug.Log($"  组件: {component.GetType().Name}");
                
                // 检查组件是否启用
                if (component is Behaviour behaviour)
                {
                    Debug.Log($"    启用状态: {behaviour.enabled}");
                }
                
                // 检查组件是否必需
                bool isRequired = IsComponentRequired(component);
                Debug.Log($"    是否必需: {isRequired}");
            }
            
            // 添加组件
            if (selected.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = selected.AddComponent<Rigidbody>();
                Debug.Log($"已添加Rigidbody组件: {rb}");
            }
            
            // 移除组件
            Component[] componentsToRemove = selected.GetComponents<Component>();
            foreach (Component component in componentsToRemove)
            {
                if (component.GetType() == typeof(Rigidbody))
                {
                    DestroyImmediate(component);
                    Debug.Log($"已移除Rigidbody组件");
                    break;
                }
            }
        }

        /// <summary>
        /// 游戏对象标签管理
        /// </summary>
        public static void GameObjectTagManagementExample()
        {
            Debug.Log("=== 游戏对象标签管理 ===");
            
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            // 获取当前标签
            string currentTag = selected.tag;
            Debug.Log($"当前标签: {currentTag}");
            
            // 获取所有可用标签
            string[] allTags = UnityEditorInternal.InternalEditorUtility.tags;
            Debug.Log($"可用标签数量: {allTags.Length}");
            
            foreach (string tag in allTags)
            {
                Debug.Log($"  标签: {tag}");
            }
            
            // 设置新标签
            if (allTags.Length > 1)
            {
                string newTag = allTags[1]; // 使用第二个标签
                selected.tag = newTag;
                Debug.Log($"标签已设置为: {newTag}");
            }
            
            // 检查标签是否存在
            bool tagExists = !string.IsNullOrEmpty(selected.tag) && 
                            System.Array.IndexOf(allTags, selected.tag) >= 0;
            Debug.Log($"标签是否存在: {tagExists}");
        }

        /// <summary>
        /// 游戏对象层管理
        /// </summary>
        public static void GameObjectLayerManagementExample()
        {
            Debug.Log("=== 游戏对象层管理 ===");
            
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            // 获取当前层
            int currentLayer = selected.layer;
            string layerName = LayerMask.LayerToName(currentLayer);
            Debug.Log($"当前层: {currentLayer} ({layerName})");
            
            // 获取所有层
            string[] allLayers = UnityEditorInternal.InternalEditorUtility.layers;
            Debug.Log($"可用层数量: {allLayers.Length}");
            
            for (int i = 0; i < allLayers.Length; i++)
            {
                int layerIndex = LayerMask.NameToLayer(allLayers[i]);
                Debug.Log($"  层 {layerIndex}: {allLayers[i]}");
            }
            
            // 设置新层
            if (allLayers.Length > 1)
            {
                string newLayerName = allLayers[1]; // 使用第二个层
                int newLayerIndex = LayerMask.NameToLayer(newLayerName);
                selected.layer = newLayerIndex;
                Debug.Log($"层已设置为: {newLayerIndex} ({newLayerName})");
            }
        }

        /// <summary>
        /// 游戏对象引用管理
        /// </summary>
        public static void GameObjectReferenceManagementExample()
        {
            Debug.Log("=== 游戏对象引用管理 ===");
            
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            // 查找引用此对象的组件
            Component[] allComponents = FindObjectsOfType<Component>();
            int referenceCount = 0;
            
            foreach (Component component in allComponents)
            {
                if (HasReferenceToGameObject(component, selected))
                {
                    referenceCount++;
                    Debug.Log($"引用对象: {component.gameObject.name}.{component.GetType().Name}");
                }
            }
            
            Debug.Log($"总引用数量: {referenceCount}");
            
            // 查找此对象引用的其他对象
            Component[] objectComponents = selected.GetComponents<Component>();
            int referencedCount = 0;
            
            foreach (Component component in objectComponents)
            {
                GameObject[] referencedObjects = GetReferencedGameObjects(component);
                referencedCount += referencedObjects.Length;
                
                foreach (GameObject referencedObj in referencedObjects)
                {
                    Debug.Log($"引用其他对象: {referencedObj.name}");
                }
            }
            
            Debug.Log($"引用的其他对象数量: {referencedCount}");
        }

        #endregion

        #region 游戏对象工具示例

        /// <summary>
        /// 游戏对象工具函数
        /// </summary>
        public static void GameObjectToolsExample()
        {
            Debug.Log("=== 游戏对象工具函数 ===");
            
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            // 获取对象信息
            GameObjectInfo info = GetGameObjectInfo(selected);
            Debug.Log($"对象信息: {info}");
            
            // 检查对象状态
            bool isActive = selected.activeInHierarchy;
            bool isStatic = selected.isStatic;
            Debug.Log($"是否激活: {isActive}");
            Debug.Log($"是否静态: {isStatic}");
            
            // 获取对象边界
            Bounds bounds = GetGameObjectBounds(selected);
            Debug.Log($"对象边界: {bounds}");
            
            // 检查对象可见性
            bool isVisible = IsGameObjectVisible(selected);
            Debug.Log($"是否可见: {isVisible}");
        }

        /// <summary>
        /// 游戏对象验证
        /// </summary>
        public static void GameObjectValidationExample()
        {
            Debug.Log("=== 游戏对象验证 ===");
            
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            // 验证对象完整性
            ValidationResult result = ValidateGameObject(selected);
            Debug.Log($"验证结果: {result}");
            
            if (!result.isValid)
            {
                Debug.LogWarning($"验证失败: {result.errorMessage}");
                
                // 尝试修复
                bool fixed = TryFixGameObject(selected);
                Debug.Log($"修复尝试: {(fixed ? "成功" : "失败")}");
            }
            
            // 检查命名规范
            bool nameValid = IsGameObjectNameValid(selected.name);
            Debug.Log($"命名是否规范: {nameValid}");
            
            if (!nameValid)
            {
                string suggestedName = SuggestGameObjectName(selected);
                Debug.Log($"建议名称: {suggestedName}");
            }
        }

        #endregion

        #region 游戏对象管理示例

        /// <summary>
        /// 游戏对象管理
        /// </summary>
        public static void GameObjectManagementExample()
        {
            Debug.Log("=== 游戏对象管理 ===");
            
            // 获取场景中的所有对象
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            Debug.Log($"场景中总对象数量: {allObjects.Length}");
            
            // 按类型分类
            Dictionary<string, List<GameObject>> typeGroups = new Dictionary<string, List<GameObject>>();
            
            foreach (GameObject obj in allObjects)
            {
                string type = obj.GetType().Name;
                if (!typeGroups.ContainsKey(type))
                {
                    typeGroups[type] = new List<GameObject>();
                }
                typeGroups[type].Add(obj);
            }
            
            foreach (var group in typeGroups)
            {
                Debug.Log($"类型 {group.Key}: {group.Value.Count} 个对象");
            }
            
            // 按层分类
            Dictionary<int, List<GameObject>> layerGroups = new Dictionary<int, List<GameObject>>();
            
            foreach (GameObject obj in allObjects)
            {
                int layer = obj.layer;
                if (!layerGroups.ContainsKey(layer))
                {
                    layerGroups[layer] = new List<GameObject>();
                }
                layerGroups[layer].Add(obj);
            }
            
            foreach (var group in layerGroups)
            {
                string layerName = LayerMask.LayerToName(group.Key);
                Debug.Log($"层 {group.Key} ({layerName}): {group.Value.Count} 个对象");
            }
        }

        /// <summary>
        /// 游戏对象统计
        /// </summary>
        public static void GameObjectStatisticsExample()
        {
            Debug.Log("=== 游戏对象统计 ===");
            
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            
            int totalObjects = allObjects.Length;
            int activeObjects = 0;
            int staticObjects = 0;
            int totalComponents = 0;
            int totalChildren = 0;
            
            foreach (GameObject obj in allObjects)
            {
                if (obj.activeInHierarchy) activeObjects++;
                if (obj.isStatic) staticObjects++;
                
                totalComponents += obj.GetComponents<Component>().Length;
                totalChildren += obj.transform.childCount;
            }
            
            Debug.Log($"=== 游戏对象统计 ===");
            Debug.Log($"总对象数: {totalObjects}");
            Debug.Log($"激活对象数: {activeObjects}");
            Debug.Log($"静态对象数: {staticObjects}");
            Debug.Log($"总组件数: {totalComponents}");
            Debug.Log($"总子对象数: {totalChildren}");
            Debug.Log($"平均组件数: {totalComponents / (float)totalObjects:F2}");
            Debug.Log($"平均子对象数: {totalChildren / (float)totalObjects:F2}");
        }

        #endregion
