using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace UnityEditor.Chapter9Utility.PrefabUtility
{
    /// <summary>
    /// PrefabUtility 预制体工具详细示例
    /// 展示预制体的创建、修改、应用、还原等操作
    /// </summary>
    public class PrefabUtilityExample : EditorWindow
    {
        private GameObject selectedObject;
        private string prefabPath = "Assets/Prefabs/";
        private Vector2 scrollPosition;

        [MenuItem("Tools/Utility Examples/PrefabUtility Detailed Example")]
        public static void ShowWindow()
        {
            PrefabUtilityExample window = GetWindow<PrefabUtilityExample>("PrefabUtility 示例");
            window.Show();
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            GUILayout.Label("PrefabUtility 预制体工具示例", EditorStyles.boldLabel);
            
            EditorGUILayout.Space();
            
            // 选择对象
            selectedObject = (GameObject)EditorGUILayout.ObjectField("选择对象", selectedObject, typeof(GameObject), true);
            
            EditorGUILayout.Space();
            
            // 预制体路径
            EditorGUILayout.LabelField("预制体保存路径:", EditorStyles.boldLabel);
            prefabPath = EditorGUILayout.TextField("路径", prefabPath);
            
            EditorGUILayout.Space();
            
            // 基础操作
            EditorGUILayout.LabelField("基础操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("创建预制体"))
            {
                CreatePrefab();
            }
            
            if (GUILayout.Button("实例化预制体"))
            {
                InstantiatePrefab();
            }
            
            EditorGUILayout.Space();
            
            // 预制体状态检查
            EditorGUILayout.LabelField("预制体状态检查:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("检查预制体状态"))
            {
                CheckPrefabStatus();
            }
            
            if (GUILayout.Button("获取预制体信息"))
            {
                GetPrefabInfo();
            }
            
            EditorGUILayout.Space();
            
            // 预制体修改操作
            EditorGUILayout.LabelField("预制体修改操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("应用预制体修改"))
            {
                ApplyPrefabChanges();
            }
            
            if (GUILayout.Button("还原预制体修改"))
            {
                RevertPrefabChanges();
            }
            
            if (GUILayout.Button("断开预制体连接"))
            {
                DisconnectPrefab();
            }
            
            EditorGUILayout.Space();
            
            // 批量操作
            EditorGUILayout.LabelField("批量操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("批量创建预制体"))
            {
                BatchCreatePrefabs();
            }
            
            if (GUILayout.Button("批量应用修改"))
            {
                BatchApplyChanges();
            }
            
            if (GUILayout.Button("批量还原修改"))
            {
                BatchRevertChanges();
            }
            
            EditorGUILayout.Space();
            
            // 高级操作
            EditorGUILayout.LabelField("高级操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("替换预制体"))
            {
                ReplacePrefab();
            }
            
            if (GUILayout.Button("合并预制体"))
            {
                MergePrefabs();
            }
            
            if (GUILayout.Button("预制体变体操作"))
            {
                PrefabVariantOperations();
            }
            
            EditorGUILayout.EndScrollView();
        }

        #region 基础操作

        /// <summary>
        /// 创建预制体
        /// </summary>
        private void CreatePrefab()
        {
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个游戏对象", "确定");
                return;
            }
            
            // 确保目录存在
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }
            
            string fileName = selectedObject.name + ".prefab";
            string fullPath = prefabPath + fileName;
            
            // 保存为预制体
            GameObject prefab = UnityEditor.PrefabUtility.SaveAsPrefabAsset(selectedObject, fullPath);
            
            if (prefab != null)
            {
                Debug.Log($"成功创建预制体: {prefab.name} 路径: {fullPath}");
                EditorUtility.DisplayDialog("成功", $"预制体 {prefab.name} 创建成功！", "确定");
            }
            else
            {
                Debug.LogError("创建预制体失败");
                EditorUtility.DisplayDialog("错误", "创建预制体失败", "确定");
            }
        }

        /// <summary>
        /// 实例化预制体
        /// </summary>
        private void InstantiatePrefab()
        {
            string prefabPath = EditorUtility.OpenFilePanel("选择预制体", "Assets/Prefabs", "prefab");
            
            if (!string.IsNullOrEmpty(prefabPath))
            {
                // 转换为相对路径
                string relativePath = "Assets" + prefabPath.Substring(Application.dataPath.Length);
                
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(relativePath);
                if (prefab != null)
                {
                    GameObject instance = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    if (instance != null)
                    {
                        instance.name = prefab.name + "_Instance";
                        Debug.Log($"成功实例化预制体: {instance.name}");
                        
                        // 选中实例化的对象
                        Selection.activeGameObject = instance;
                    }
                }
            }
        }

        #endregion

        #region 状态检查

        /// <summary>
        /// 检查预制体状态
        /// </summary>
        private void CheckPrefabStatus()
        {
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个游戏对象", "确定");
                return;
            }
            
            bool isPrefabAsset = UnityEditor.PrefabUtility.IsPartOfPrefabAsset(selectedObject);
            bool isPrefabInstance = UnityEditor.PrefabUtility.IsPartOfPrefabInstance(selectedObject);
            bool isDisconnected = UnityEditor.PrefabUtility.IsDisconnectedFromPrefabAsset(selectedObject);
            
            string status = "";
            if (isPrefabAsset)
                status = "预制体资源";
            else if (isPrefabInstance)
                status = "预制体实例";
            else if (isDisconnected)
                status = "断开的预制体";
            else
                status = "普通游戏对象";
            
            Debug.Log($"对象 {selectedObject.name} 状态: {status}");
            EditorUtility.DisplayDialog("预制体状态", $"对象状态: {status}", "确定");
        }

        /// <summary>
        /// 获取预制体信息
        /// </summary>
        private void GetPrefabInfo()
        {
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个游戏对象", "确定");
                return;
            }
            
            if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(selectedObject))
            {
                // 获取预制体资源路径
                string prefabPath = UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(selectedObject);
                Debug.Log($"预制体路径: {prefabPath}");
                
                // 获取预制体实例状态
                PrefabInstanceStatus status = UnityEditor.PrefabUtility.GetPrefabInstanceStatus(selectedObject);
                Debug.Log($"预制体状态: {status}");
                
                // 获取预制体根对象
                GameObject prefabRoot = UnityEditor.PrefabUtility.GetOutermostPrefabInstanceRoot(selectedObject);
                Debug.Log($"预制体根对象: {prefabRoot.name}");
                
                // 获取预制体资源
                GameObject prefabAsset = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(selectedObject);
                if (prefabAsset != null)
                {
                    Debug.Log($"对应预制体资源: {prefabAsset.name}");
                }
                
                EditorUtility.DisplayDialog("预制体信息", 
                    $"路径: {prefabPath}\n状态: {status}\n根对象: {prefabRoot.name}", "确定");
            }
            else
            {
                EditorUtility.DisplayDialog("提示", "选中的对象不是预制体实例", "确定");
            }
        }

        #endregion

        #region 修改操作

        /// <summary>
        /// 应用预制体修改
        /// </summary>
        private void ApplyPrefabChanges()
        {
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个游戏对象", "确定");
                return;
            }
            
            if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(selectedObject))
            {
                PrefabInstanceStatus status = UnityEditor.PrefabUtility.GetPrefabInstanceStatus(selectedObject);
                
                if (status == PrefabInstanceStatus.Modified)
                {
                    UnityEditor.PrefabUtility.ApplyPrefabInstance(selectedObject, InteractionMode.UserAction);
                    Debug.Log($"已应用预制体修改: {selectedObject.name}");
                    EditorUtility.DisplayDialog("成功", "预制体修改已应用", "确定");
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "预制体没有修改需要应用", "确定");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("错误", "选中的对象不是预制体实例", "确定");
            }
        }

        /// <summary>
        /// 还原预制体修改
        /// </summary>
        private void RevertPrefabChanges()
        {
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个游戏对象", "确定");
                return;
            }
            
            if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(selectedObject))
            {
                PrefabInstanceStatus status = UnityEditor.PrefabUtility.GetPrefabInstanceStatus(selectedObject);
                
                if (status == PrefabInstanceStatus.Modified)
                {
                    UnityEditor.PrefabUtility.RevertPrefabInstance(selectedObject, InteractionMode.UserAction);
                    Debug.Log($"已还原预制体修改: {selectedObject.name}");
                    EditorUtility.DisplayDialog("成功", "预制体修改已还原", "确定");
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "预制体没有修改需要还原", "确定");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("错误", "选中的对象不是预制体实例", "确定");
            }
        }

        /// <summary>
        /// 断开预制体连接
        /// </summary>
        private void DisconnectPrefab()
        {
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个游戏对象", "确定");
                return;
            }
            
            if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(selectedObject))
            {
                bool confirm = EditorUtility.DisplayDialog("确认", 
                    "确定要断开预制体连接吗？此操作不可撤销。", "确定", "取消");
                
                if (confirm)
                {
                    UnityEditor.PrefabUtility.UnpackPrefabInstance(selectedObject, PrefabUnpackMode.Completely, InteractionMode.UserAction);
                    Debug.Log($"已断开预制体连接: {selectedObject.name}");
                    EditorUtility.DisplayDialog("成功", "预制体连接已断开", "确定");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("错误", "选中的对象不是预制体实例", "确定");
            }
        }

        #endregion

        #region 批量操作

        /// <summary>
        /// 批量创建预制体
        /// </summary>
        private void BatchCreatePrefabs()
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            
            if (selectedObjects.Length == 0)
            {
                EditorUtility.DisplayDialog("错误", "请先选择多个游戏对象", "确定");
                return;
            }
            
            bool confirm = EditorUtility.DisplayDialog("确认", 
                $"确定要为 {selectedObjects.Length} 个对象创建预制体吗？", "确定", "取消");
            
            if (!confirm) return;
            
            int successCount = 0;
            
            for (int i = 0; i < selectedObjects.Length; i++)
            {
                EditorUtility.DisplayProgressBar("创建预制体", 
                    $"正在处理 {selectedObjects[i].name}...", (float)i / selectedObjects.Length);
                
                string fileName = selectedObjects[i].name + ".prefab";
                string fullPath = prefabPath + fileName;
                
                GameObject prefab = UnityEditor.PrefabUtility.SaveAsPrefabAsset(selectedObjects[i], fullPath);
                if (prefab != null)
                {
                    successCount++;
                }
            }
            
            EditorUtility.ClearProgressBar();
            
            Debug.Log($"批量创建完成，成功: {successCount}/{selectedObjects.Length}");
            EditorUtility.DisplayDialog("完成", 
                $"批量创建完成\n成功: {successCount}\n失败: {selectedObjects.Length - successCount}", "确定");
        }

        /// <summary>
        /// 批量应用修改
        /// </summary>
        private void BatchApplyChanges()
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            int modifiedCount = 0;
            int appliedCount = 0;
            
            foreach (GameObject obj in selectedObjects)
            {
                if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(obj))
                {
                    PrefabInstanceStatus status = UnityEditor.PrefabUtility.GetPrefabInstanceStatus(obj);
                    if (status == PrefabInstanceStatus.Modified)
                    {
                        modifiedCount++;
                        UnityEditor.PrefabUtility.ApplyPrefabInstance(obj, InteractionMode.UserAction);
                        appliedCount++;
                    }
                }
            }
            
            Debug.Log($"批量应用完成，修改: {modifiedCount}，应用: {appliedCount}");
            EditorUtility.DisplayDialog("完成", 
                $"批量应用完成\n修改对象: {modifiedCount}\n成功应用: {appliedCount}", "确定");
        }

        /// <summary>
        /// 批量还原修改
        /// </summary>
        private void BatchRevertChanges()
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            int modifiedCount = 0;
            int revertedCount = 0;
            
            foreach (GameObject obj in selectedObjects)
            {
                if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(obj))
                {
                    PrefabInstanceStatus status = UnityEditor.PrefabUtility.GetPrefabInstanceStatus(obj);
                    if (status == PrefabInstanceStatus.Modified)
                    {
                        modifiedCount++;
                        UnityEditor.PrefabUtility.RevertPrefabInstance(obj, InteractionMode.UserAction);
                        revertedCount++;
                    }
                }
            }
            
            Debug.Log($"批量还原完成，修改: {modifiedCount}，还原: {revertedCount}");
            EditorUtility.DisplayDialog("完成", 
                $"批量还原完成\n修改对象: {modifiedCount}\n成功还原: {revertedCount}", "确定");
        }

        #endregion

        #region 高级操作

        /// <summary>
        /// 替换预制体
        /// </summary>
        private void ReplacePrefab()
        {
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个游戏对象", "确定");
                return;
            }
            
            string prefabPath = EditorUtility.OpenFilePanel("选择新预制体", "Assets/Prefabs", "prefab");
            
            if (!string.IsNullOrEmpty(prefabPath))
            {
                string relativePath = "Assets" + prefabPath.Substring(Application.dataPath.Length);
                GameObject newPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(relativePath);
                
                if (newPrefab != null)
                {
                    GameObject newInstance = UnityEditor.PrefabUtility.InstantiatePrefab(newPrefab) as GameObject;
                    if (newInstance != null)
                    {
                        // 复制变换信息
                        newInstance.transform.position = selectedObject.transform.position;
                        newInstance.transform.rotation = selectedObject.transform.rotation;
                        newInstance.transform.localScale = selectedObject.transform.localScale;
                        
                        // 复制父对象
                        newInstance.transform.SetParent(selectedObject.transform.parent);
                        
                        // 删除原对象
                        DestroyImmediate(selectedObject);
                        
                        // 选中新对象
                        Selection.activeGameObject = newInstance;
                        
                        Debug.Log($"已替换预制体: {newInstance.name}");
                        EditorUtility.DisplayDialog("成功", "预制体替换完成", "确定");
                    }
                }
            }
        }

        /// <summary>
        /// 合并预制体
        /// </summary>
        private void MergePrefabs()
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            
            if (selectedObjects.Length < 2)
            {
                EditorUtility.DisplayDialog("错误", "请选择至少2个游戏对象进行合并", "确定");
                return;
            }
            
            bool confirm = EditorUtility.DisplayDialog("确认", 
                $"确定要合并 {selectedObjects.Length} 个对象吗？", "确定", "取消");
            
            if (!confirm) return;
            
            // 创建父对象
            GameObject mergedObject = new GameObject("MergedPrefab");
            
            // 将所有对象作为子对象
            foreach (GameObject obj in selectedObjects)
            {
                obj.transform.SetParent(mergedObject.transform);
            }
            
            // 保存为预制体
            string fileName = "MergedPrefab.prefab";
            string fullPath = prefabPath + fileName;
            
            GameObject prefab = UnityEditor.PrefabUtility.SaveAsPrefabAsset(mergedObject, fullPath);
            
            if (prefab != null)
            {
                Debug.Log($"成功合并预制体: {prefab.name}");
                EditorUtility.DisplayDialog("成功", "预制体合并完成", "确定");
                
                // 选中合并后的对象
                Selection.activeGameObject = mergedObject;
            }
        }

        /// <summary>
        /// 预制体变体操作
        /// </summary>
        private void PrefabVariantOperations()
        {
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个游戏对象", "确定");
                return;
            }
            
            if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(selectedObject))
            {
                // 创建预制体变体
                string fileName = selectedObject.name + "_Variant.prefab";
                string fullPath = prefabPath + fileName;
                
                GameObject variant = UnityEditor.PrefabUtility.SaveAsPrefabAsset(selectedObject, fullPath);
                
                if (variant != null)
                {
                    Debug.Log($"成功创建预制体变体: {variant.name}");
                    EditorUtility.DisplayDialog("成功", "预制体变体创建完成", "确定");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("错误", "选中的对象不是预制体实例", "确定");
            }
        }

        #endregion
    }
}
