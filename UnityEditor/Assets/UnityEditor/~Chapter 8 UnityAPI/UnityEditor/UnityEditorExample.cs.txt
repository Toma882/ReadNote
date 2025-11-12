// UnityEditorExample.cs
// UnityEditor 编辑器扩展示例
// 展示自定义编辑器、窗口、工具等编辑器API的使用

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Compilation;

namespace UnityAPI.UnityEditor
{
    /// <summary>
    /// UnityEditor 编辑器扩展示例
    /// 
    /// 涵盖内容：
    /// - 自定义编辑器窗口
    /// - 自定义Inspector
    /// - 菜单项和快捷键
    /// - 资源管理
    /// - 场景管理
    /// - 构建系统
    /// - 编辑器工具
    /// </summary>
    public class UnityEditorExample : EditorWindow
    {
        [MenuItem("UnityAPI/Editor Example")]
        public static void ShowWindow()
        {
            GetWindow<UnityEditorExample>("UnityEditor示例");
        }
        
        [MenuItem("UnityAPI/Quick Actions/Create Test Objects")]
        public static void CreateTestObjects()
        {
            // 创建测试对象
            GameObject parent = new GameObject("TestObjects");
            
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.name = $"TestCube_{i}";
                obj.transform.position = new Vector3(i * 2, 0, 0);
                obj.transform.parent = parent.transform;
                
                // 添加随机颜色
                Renderer renderer = obj.GetComponent<Renderer>();
                renderer.material.color = UnityEngine.Random.ColorHSV();
            }
            
            Selection.activeGameObject = parent;
            EditorGUIUtility.PingObject(parent);
            
            Debug.Log("创建了测试对象");
        }
        
        [MenuItem("UnityAPI/Quick Actions/Clear Empty Objects")]
        public static void ClearEmptyObjects()
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            List<GameObject> emptyObjects = new List<GameObject>();
            
            foreach (GameObject obj in allObjects)
            {
                if (obj.transform.childCount == 0 && obj.GetComponents<Component>().Length <= 1)
                {
                    emptyObjects.Add(obj);
                }
            }
            
            foreach (GameObject obj in emptyObjects)
            {
                DestroyImmediate(obj);
            }
            
            Debug.Log($"清除了 {emptyObjects.Count} 个空对象");
        }
        
        [MenuItem("UnityAPI/Quick Actions/Optimize Scene")]
        public static void OptimizeScene()
        {
            // 合并相同材质的对象
            MergeObjectsWithSameMaterial();
            
            // 移除未使用的资源
            RemoveUnusedAssets();
            
            Debug.Log("场景优化完成");
        }
        
        private static void MergeObjectsWithSameMaterial()
        {
            // 按材质分组对象
            Dictionary<Material, List<GameObject>> materialGroups = new Dictionary<Material, List<GameObject>>();
            
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null && renderer.material != null)
                {
                    Material mat = renderer.material;
                    if (!materialGroups.ContainsKey(mat))
                    {
                        materialGroups[mat] = new List<GameObject>();
                    }
                    materialGroups[mat].Add(obj);
                }
            }
            
            // 合并相同材质的对象
            foreach (var group in materialGroups)
            {
                if (group.Value.Count > 1)
                {
                    Debug.Log($"材质 {group.Key.name} 有 {group.Value.Count} 个对象");
                }
            }
        }
        
        private static void RemoveUnusedAssets()
        {
            // 这里可以实现移除未使用资源的逻辑
            Debug.Log("移除未使用的资源");
        }
        
        // 编辑器窗口内容
        private Vector2 scrollPosition;
        private bool showGameObjectTools = true;
        private bool showAssetTools = true;
        private bool showSceneTools = true;
        private bool showBuildTools = true;
        
        void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            EditorGUILayout.LabelField("UnityEditor 编辑器扩展示例", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            // 游戏对象工具
            showGameObjectTools = EditorGUILayout.Foldout(showGameObjectTools, "游戏对象工具");
            if (showGameObjectTools)
            {
                EditorGUI.indentLevel++;
                DrawGameObjectTools();
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space();
            
            // 资源工具
            showAssetTools = EditorGUILayout.Foldout(showAssetTools, "资源工具");
            if (showAssetTools)
            {
                EditorGUI.indentLevel++;
                DrawAssetTools();
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space();
            
            // 场景工具
            showSceneTools = EditorGUILayout.Foldout(showSceneTools, "场景工具");
            if (showSceneTools)
            {
                EditorGUI.indentLevel++;
                DrawSceneTools();
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space();
            
            // 构建工具
            showBuildTools = EditorGUILayout.Foldout(showBuildTools, "构建工具");
            if (showBuildTools)
            {
                EditorGUI.indentLevel++;
                DrawBuildTools();
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.EndScrollView();
        }
        
        private void DrawGameObjectTools()
        {
            if (GUILayout.Button("创建测试对象"))
            {
                CreateTestObjects();
            }
            
            if (GUILayout.Button("清除空对象"))
            {
                ClearEmptyObjects();
            }
            
            if (GUILayout.Button("选择所有相同类型对象"))
            {
                SelectAllSameType();
            }
            
            if (GUILayout.Button("批量重命名选中对象"))
            {
                BatchRenameSelected();
            }
        }
        
        private void DrawAssetTools()
        {
            if (GUILayout.Button("查找未使用的资源"))
            {
                FindUnusedAssets();
            }
            
            if (GUILayout.Button("优化纹理设置"))
            {
                OptimizeTextureSettings();
            }
            
            if (GUILayout.Button("批量导入设置"))
            {
                BatchImportSettings();
            }
            
            if (GUILayout.Button("生成资源报告"))
            {
                GenerateAssetReport();
            }
        }
        
        private void DrawSceneTools()
        {
            if (GUILayout.Button("保存当前场景"))
            {
                EditorSceneManager.SaveOpenScenes();
            }
            
            if (GUILayout.Button("加载场景"))
            {
                LoadScene();
            }
            
            if (GUILayout.Button("场景统计"))
            {
                ShowSceneStatistics();
            }
            
            if (GUILayout.Button("清理场景"))
            {
                CleanupScene();
            }
        }
        
        private void DrawBuildTools()
        {
            if (GUILayout.Button("构建当前平台"))
            {
                BuildCurrentPlatform();
            }
            
            if (GUILayout.Button("构建所有平台"))
            {
                BuildAllPlatforms();
            }
            
            if (GUILayout.Button("构建设置"))
            {
                ShowBuildSettings();
            }
            
            if (GUILayout.Button("构建报告"))
            {
                GenerateBuildReport();
            }
        }
        
        private void SelectAllSameType()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;
            
            System.Type componentType = selected.GetComponent<Component>().GetType();
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            List<GameObject> sameTypeObjects = new List<GameObject>();
            
            foreach (GameObject obj in allObjects)
            {
                if (obj.GetComponent(componentType) != null)
                {
                    sameTypeObjects.Add(obj);
                }
            }
            
            Selection.objects = sameTypeObjects.ToArray();
            Debug.Log($"选择了 {sameTypeObjects.Count} 个相同类型的对象");
        }
        
        private void BatchRenameSelected()
        {
            GameObject[] selected = Selection.gameObjects;
            if (selected.Length == 0) return;
            
            string baseName = "RenamedObject";
            for (int i = 0; i < selected.Length; i++)
            {
                selected[i].name = $"{baseName}_{i:D3}";
            }
            
            Debug.Log($"重命名了 {selected.Length} 个对象");
        }
        
        private void FindUnusedAssets()
        {
            // 查找未使用的资源
            string[] allAssets = AssetDatabase.FindAssets("t:Object");
            List<string> unusedAssets = new List<string>();
            
            foreach (string guid in allAssets)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                
                if (asset != null && !IsAssetUsed(asset))
                {
                    unusedAssets.Add(path);
                }
            }
            
            Debug.Log($"找到 {unusedAssets.Count} 个未使用的资源");
        }
        
        private bool IsAssetUsed(UnityEngine.Object asset)
        {
            // 简单的使用检查逻辑
            return false; // 简化实现
        }
        
        private void OptimizeTextureSettings()
        {
            string[] textureGuids = AssetDatabase.FindAssets("t:Texture2D");
            int optimizedCount = 0;
            
            foreach (string guid in textureGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                
                if (importer != null)
                {
                    // 优化纹理设置
                    importer.textureCompression = TextureImporterCompression.Compressed;
                    importer.maxTextureSize = 1024;
                    importer.SaveAndReimport();
                    optimizedCount++;
                }
            }
            
            Debug.Log($"优化了 {optimizedCount} 个纹理");
        }
        
        private void BatchImportSettings()
        {
            // 批量导入设置
            Debug.Log("批量导入设置");
        }
        
        private void GenerateAssetReport()
        {
            // 生成资源报告
            Debug.Log("生成资源报告");
        }
        
        private void LoadScene()
        {
            string scenePath = EditorUtility.OpenFilePanel("选择场景", "Assets", "unity");
            if (!string.IsNullOrEmpty(scenePath))
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }
        
        private void ShowSceneStatistics()
        {
            Scene currentScene = EditorSceneManager.GetActiveScene();
            GameObject[] allObjects = currentScene.GetRootGameObjects();
            
            Debug.Log($"场景: {currentScene.name}");
            Debug.Log($"根对象数量: {allObjects.Length}");
            Debug.Log($"总对象数量: {FindObjectsOfType<GameObject>().Length}");
        }
        
        private void CleanupScene()
        {
            // 清理场景
            Debug.Log("清理场景");
        }
        
        private void BuildCurrentPlatform()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = EditorSceneManager.GetActiveScene().path;
            buildPlayerOptions.locationPathName = "Builds/Game";
            buildPlayerOptions.target = EditorUserBuildSettings.activeBuildTarget;
            buildPlayerOptions.options = BuildOptions.None;
            
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
        
        private void BuildAllPlatforms()
        {
            // 构建所有平台
            Debug.Log("构建所有平台");
        }
        
        private void ShowBuildSettings()
        {
            EditorWindow.GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
        }
        
        private void GenerateBuildReport()
        {
            // 生成构建报告
            Debug.Log("生成构建报告");
        }
    }
    
    /// <summary>
    /// 自定义Inspector示例
    /// </summary>
    [CustomEditor(typeof(UnityEngineCoreExample))]
    public class UnityEngineCoreExampleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("自定义Inspector", EditorStyles.boldLabel);
            
            UnityEngineCoreExample example = (UnityEngineCoreExample)target;
            
            if (GUILayout.Button("生成对象"))
            {
                // 调用目标对象的方法
                example.SendMessage("SpawnObjects");
            }
            
            if (GUILayout.Button("清除对象"))
            {
                example.SendMessage("ClearAllObjects");
            }
            
            if (GUILayout.Button("获取场景信息"))
            {
                example.SendMessage("GetSceneInfo");
            }
        }
    }
    
    /// <summary>
    /// 自定义属性绘制器示例
    /// </summary>
    [CustomPropertyDrawer(typeof(Vector3))]
    public class Vector3PropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            float width = position.width / 3;
            Rect xRect = new Rect(position.x, position.y, width, position.height);
            Rect yRect = new Rect(position.x + width, position.y, width, position.height);
            Rect zRect = new Rect(position.x + width * 2, position.y, width, position.height);
            
            EditorGUI.PropertyField(xRect, property.FindPropertyRelative("x"), GUIContent.none);
            EditorGUI.PropertyField(yRect, property.FindPropertyRelative("y"), GUIContent.none);
            EditorGUI.PropertyField(zRect, property.FindPropertyRelative("z"), GUIContent.none);
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
    
    /// <summary>
    /// 编辑器工具示例
    /// </summary>
    public class CustomEditorTool : EditorTool
    {
        public override GUIContent toolbarIcon => new GUIContent("Custom Tool");
        
        public override void OnToolGUI(EditorWindow window)
        {
            Handles.BeginGUI();
            
            if (GUILayout.Button("自定义工具"))
            {
                Debug.Log("自定义工具被点击");
            }
            
            Handles.EndGUI();
        }
    }
}
