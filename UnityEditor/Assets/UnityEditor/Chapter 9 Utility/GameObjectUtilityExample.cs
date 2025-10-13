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
    }
}
