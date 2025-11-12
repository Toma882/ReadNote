using UnityEngine;
using UnityEditor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// TerrainInspectorUtility 工具类示例
    /// 提供地形检查器相关的实用工具功能
    /// </summary>
    public static class TerrainInspectorUtilityExample
    {
        #region 默认地形检查器示例

        /// <summary>
        /// 显示默认地形检查器
        /// </summary>
        public static void ShowDefaultTerrainInspectorExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            // 显示默认地形检查器
            TerrainInspectorUtility.ShowDefaultTerrainInspector(selectedTerrain);
            Debug.Log($"已显示地形 {selectedTerrain.name} 的默认检查器");
        }

        /// <summary>
        /// 显示默认地形检查器（带验证）
        /// </summary>
        public static void ShowDefaultTerrainInspectorWithValidationExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            // 检查地形是否有效
            if (selectedTerrain.terrainData != null)
            {
                TerrainInspectorUtility.ShowDefaultTerrainInspector(selectedTerrain);
                Debug.Log($"地形检查器已显示: {selectedTerrain.name}");
            }
            else
            {
                Debug.LogWarning($"地形 {selectedTerrain.name} 没有地形数据");
            }
        }

        #endregion

        #region 地形检查器状态示例

        /// <summary>
        /// 检查地形检查器状态
        /// </summary>
        public static void CheckTerrainInspectorStateExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            Debug.Log($"场景中地形数量: {terrains.Length}");
            
            foreach (Terrain terrain in terrains)
            {
                Debug.Log($"地形: {terrain.name}");
                Debug.Log($"- 地形数据: {(terrain.terrainData != null ? "存在" : "不存在")}");
                Debug.Log($"- 地形组件: {(terrain.GetComponent<Terrain>() != null ? "存在" : "不存在")}");
                Debug.Log($"- 地形碰撞器: {(terrain.GetComponent<TerrainCollider>() != null ? "存在" : "不存在")}");
            }
        }

        /// <summary>
        /// 验证地形检查器
        /// </summary>
        public static void ValidateTerrainInspectorExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            // 验证地形数据
            TerrainData terrainData = selectedTerrain.terrainData;
            if (terrainData == null)
            {
                Debug.LogWarning("地形没有地形数据");
                return;
            }

            // 验证地形设置
            Debug.Log($"地形验证结果:");
            Debug.Log($"- 高度图分辨率: {terrainData.heightmapResolution}");
            Debug.Log($"- 细节分辨率: {terrainData.detailResolution}");
            Debug.Log($"- 控制纹理分辨率: {terrainData.alphamapResolution}");
            Debug.Log($"- 基础纹理分辨率: {terrainData.baseMapResolution}");
            Debug.Log($"- 地形大小: {terrainData.size}");
            Debug.Log($"- 纹理层数量: {terrainData.terrainLayers.Length}");
        }

        #endregion

        #region 地形检查器操作示例

        /// <summary>
        /// 刷新地形检查器
        /// </summary>
        public static void RefreshTerrainInspectorExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            // 刷新检查器
            EditorUtility.SetDirty(selectedTerrain);
            EditorUtility.SetDirty(selectedTerrain.terrainData);
            
            Debug.Log($"地形检查器已刷新: {selectedTerrain.name}");
        }

        /// <summary>
        /// 重置地形检查器
        /// </summary>
        public static void ResetTerrainInspectorExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            // 重置地形设置
            selectedTerrain.drawHeightmap = true;
            selectedTerrain.drawInstanced = false;
            selectedTerrain.allowAutoConnect = true;
            
            Debug.Log($"地形检查器已重置: {selectedTerrain.name}");
        }

        #endregion

        #region 地形检查器设置示例

        /// <summary>
        /// 设置地形检查器属性
        /// </summary>
        public static void SetTerrainInspectorPropertiesExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            // 设置地形属性
            selectedTerrain.drawHeightmap = true;
            selectedTerrain.drawInstanced = true;
            selectedTerrain.allowAutoConnect = true;
            selectedTerrain.groupingID = 0;
            
            Debug.Log($"地形检查器属性已设置:");
            Debug.Log($"- 绘制高度图: {selectedTerrain.drawHeightmap}");
            Debug.Log($"- 实例化绘制: {selectedTerrain.drawInstanced}");
            Debug.Log($"- 允许自动连接: {selectedTerrain.allowAutoConnect}");
            Debug.Log($"- 分组ID: {selectedTerrain.groupingID}");
        }

        /// <summary>
        /// 获取地形检查器属性
        /// </summary>
        public static void GetTerrainInspectorPropertiesExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            Debug.Log($"地形检查器属性:");
            Debug.Log($"- 绘制高度图: {selectedTerrain.drawHeightmap}");
            Debug.Log($"- 实例化绘制: {selectedTerrain.drawInstanced}");
            Debug.Log($"- 允许自动连接: {selectedTerrain.allowAutoConnect}");
            Debug.Log($"- 分组ID: {selectedTerrain.groupingID}");
            Debug.Log($"- 材质模板: {selectedTerrain.materialTemplate}");
        }

        #endregion

        #region 地形检查器工具示例

        /// <summary>
        /// 地形检查器工具
        /// </summary>
        public static void TerrainInspectorToolsExample()
        {
            Terrain selectedTerrain = GetSelectedTerrain();
            if (selectedTerrain == null) return;

            // 获取地形数据
            TerrainData terrainData = selectedTerrain.terrainData;
            
            Debug.Log($"地形检查器工具:");
            Debug.Log($"- 高度图工具: 可用");
            Debug.Log($"- 纹理绘制工具: {(terrainData.terrainLayers.Length > 0 ? "可用" : "不可用")}");
            Debug.Log($"- 细节绘制工具: 可用");
            Debug.Log($"- 树绘制工具: 可用");
            Debug.Log($"- 草绘制工具: 可用");
        }

        /// <summary>
        /// 地形检查器快捷键
        /// </summary>
        public static void TerrainInspectorShortcutsExample()
        {
            Debug.Log("地形检查器快捷键:");
            Debug.Log("- F: 聚焦到地形");
            Debug.Log("- Ctrl+D: 复制地形");
            Debug.Log("- Delete: 删除地形");
            Debug.Log("- Ctrl+S: 保存地形数据");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建地形检查器测试
        /// </summary>
        public static void CreateTerrainInspectorTestExample()
        {
            // 创建地形数据
            TerrainData terrainData = new TerrainData();
            terrainData.size = new Vector3(100, 30, 100);
            terrainData.heightmapResolution = 513;
            
            // 创建地形
            GameObject terrainObj = Terrain.CreateTerrainGameObject(terrainData);
            terrainObj.name = "InspectorTestTerrain";
            
            Terrain terrain = terrainObj.GetComponent<Terrain>();
            
            // 设置地形属性
            terrain.drawHeightmap = true;
            terrain.drawInstanced = false;
            terrain.allowAutoConnect = true;
            
            // 显示默认检查器
            TerrainInspectorUtility.ShowDefaultTerrainInspector(terrain);
            
            Debug.Log($"地形检查器测试已创建: {terrain.name}");
        }

        /// <summary>
        /// 批量地形检查器处理
        /// </summary>
        public static void BatchTerrainInspectorProcessingExample()
        {
            Terrain[] terrains = Object.FindObjectsOfType<Terrain>();
            
            if (terrains.Length == 0)
            {
                Debug.LogWarning("场景中没有地形");
                return;
            }
            
            foreach (Terrain terrain in terrains)
            {
                // 显示默认检查器
                TerrainInspectorUtility.ShowDefaultTerrainInspector(terrain);
                
                // 设置属性
                terrain.drawHeightmap = true;
                terrain.allowAutoConnect = true;
                
                Debug.Log($"处理地形检查器: {terrain.name}");
            }
            
            Debug.Log($"批量处理完成，共处理 {terrains.Length} 个地形检查器");
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

        #endregion
    }
}
