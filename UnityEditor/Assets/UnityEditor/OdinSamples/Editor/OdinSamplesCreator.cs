#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace OdinSamples.Editor
{
    /// <summary>
    /// Odin 示例创建器
    /// 提供快捷菜单创建所有示例对象
    /// </summary>
    public static class OdinSamplesCreator
    {
        private const string MENU_ROOT = "GameObject/Odin Samples/";
        
        [MenuItem(MENU_ROOT + "创建所有示例", false, 0)]
        private static void CreateAllSamples()
        {
            CreateBasicAttributesSample();
            CreateCollectionsSample();
            CreateButtonsSample();
            CreateValidationSample();
            CreateGroupsSample();
            CreateConditionalSample();
            CreateValueDropdownsSample();
            CreateAdvancedSample();
            CreateCustomEditorSample();
            
            Debug.Log("✅ 已创建所有 Odin Inspector 示例对象（不包括编辑器窗口）");
        }
        
        [MenuItem(MENU_ROOT + "01 - 基础属性示例", false, 1)]
        private static void CreateBasicAttributesSample()
        {
            CreateSample<BasicAttributes.BasicAttributesSample>("01_基础属性示例");
        }
        
        [MenuItem(MENU_ROOT + "02 - 集合示例", false, 2)]
        private static void CreateCollectionsSample()
        {
            CreateSample<Collections.CollectionsSample>("02_集合示例");
        }
        
        [MenuItem(MENU_ROOT + "03 - 按钮示例", false, 3)]
        private static void CreateButtonsSample()
        {
            CreateSample<Buttons.ButtonsSample>("03_按钮示例");
        }
        
        [MenuItem(MENU_ROOT + "04 - 验证示例", false, 4)]
        private static void CreateValidationSample()
        {
            CreateSample<Validation.ValidationSample>("04_验证示例");
        }
        
        [MenuItem(MENU_ROOT + "05 - 分组示例", false, 5)]
        private static void CreateGroupsSample()
        {
            CreateSample<Groups.GroupsSample>("05_分组示例");
        }
        
        [MenuItem(MENU_ROOT + "06 - 条件显示示例", false, 6)]
        private static void CreateConditionalSample()
        {
            CreateSample<Conditional.ConditionalSample>("06_条件显示示例");
        }
        
        [MenuItem(MENU_ROOT + "07 - 下拉选择器示例", false, 7)]
        private static void CreateValueDropdownsSample()
        {
            CreateSample<ValueDropdowns.ValueDropdownsSample>("07_下拉选择器示例");
        }
        
        [MenuItem(MENU_ROOT + "08 - 高级特性示例", false, 8)]
        private static void CreateAdvancedSample()
        {
            CreateSample<Advanced.AdvancedSample>("08_高级特性示例");
        }
        
        [MenuItem(MENU_ROOT + "10 - 自定义编辑器示例", false, 10)]
        private static void CreateCustomEditorSample()
        {
            CreateSample<CustomEditors.CustomOdinEditorSample>("10_自定义编辑器示例");
        }
        
        [MenuItem(MENU_ROOT + "删除所有示例", false, 100)]
        private static void DeleteAllSamples()
        {
            if (EditorUtility.DisplayDialog("删除确认", 
                "确定要删除场景中所有 Odin 示例对象吗？", 
                "删除", "取消"))
            {
                int count = 0;
                var samples = Object.FindObjectsOfType<MonoBehaviour>();
                foreach (var sample in samples)
                {
                    if (sample.GetType().Namespace != null && 
                        sample.GetType().Namespace.StartsWith("OdinSamples"))
                    {
                        Object.DestroyImmediate(sample.gameObject);
                        count++;
                    }
                }
                
                Debug.Log($"已删除 {count} 个示例对象");
            }
        }
        
        private static void CreateSample<T>(string name) where T : MonoBehaviour
        {
            // 检查是否已存在
            var existing = Object.FindObjectOfType<T>();
            if (existing != null)
            {
                Selection.activeGameObject = existing.gameObject;
                EditorGUIUtility.PingObject(existing.gameObject);
                Debug.Log($"⚠️ {name} 已存在，已选中该对象");
                return;
            }
            
            // 创建新对象
            GameObject go = new GameObject(name);
            go.AddComponent<T>();
            
            // 设置位置
            if (Selection.activeGameObject != null)
            {
                go.transform.SetParent(Selection.activeGameObject.transform);
            }
            
            // 选中新对象
            Selection.activeGameObject = go;
            EditorGUIUtility.PingObject(go);
            
            // 标记场景为已修改
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
                go.scene);
            
            Debug.Log($"✅ 已创建 {name}");
        }
    }
    
    /// <summary>
    /// Odin 示例文档查看器
    /// </summary>
    public static class OdinSamplesDocViewer
    {
        [MenuItem("Tools/Odin Samples/Windows/01 - 简单窗口", false, 1)]
        private static void OpenSimpleWindow()
        {
            EditorWindow.GetWindow<EditorWindows.SimpleOdinWindowSample>().Show();
        }
        
        [MenuItem("Tools/Odin Samples/Windows/02 - 菜单窗口", false, 2)]
        private static void OpenMenuWindow()
        {
            EditorWindow.GetWindow<EditorWindows.OdinMenuWindowSample>().Show();
        }
        
        [MenuItem("Tools/Odin Samples/Windows/03 - 高级窗口", false, 3)]
        private static void OpenAdvancedWindow()
        {
            EditorWindow.GetWindow<EditorWindows.AdvancedWindowSample>().Show();
        }
        
        [MenuItem("Tools/Odin Samples/打开使用文档", false, 100)]
        private static void OpenUsageDoc()
        {
            string path = "Assets/Scripts/OdinSamples/README_USAGE.md";
            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(path, 1);
        }
        
        [MenuItem("Tools/Odin Samples/打开总览文档", false, 1)]
        private static void OpenOverviewDoc()
        {
            string path = "Assets/Scripts/OdinSamples/README.md";
            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(path, 1);
        }
        
        [MenuItem("Tools/Odin Samples/打开 Odin 官方文档", false, 20)]
        private static void OpenOdinDocs()
        {
            Application.OpenURL("https://odininspector.com/attributes");
        }
        
        [MenuItem("Tools/Odin Samples/关于 Odin Samples", false, 100)]
        private static void ShowAbout()
        {
            EditorUtility.DisplayDialog(
                "关于 Odin Samples",
                "Odin Inspector 完整功能演示\n\n" +
                "包含以下模块：\n" +
                "• 基础属性 - 基本标注特性\n" +
                "• 集合 - 列表和数组显示\n" +
                "• 按钮 - 方法按钮\n" +
                "• 验证 - 数据验证\n" +
                "• 分组 - 布局分组\n" +
                "• 条件显示 - 动态显示\n" +
                "• 下拉选择器 - 自定义下拉框\n" +
                "• 高级特性 - 高级功能\n\n" +
                "所有示例均配有详细的中文注释\n" +
                "可以直接在场景中运行查看效果",
                "确定"
            );
        }
    }
}
#endif

