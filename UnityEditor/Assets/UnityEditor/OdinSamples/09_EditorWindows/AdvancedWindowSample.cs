#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace OdinSamples.EditorWindows
{
    /*
        学习要点：
        ----------------------
        OdinMenuEditorWindow 特性可以将方法转换为 Inspector 中的可点击按钮
            参数
            string name: 按钮名称
            ButtonSizes size: 按钮大小
        ----------------------
    */
    /// <summary>
    /// 高级 Odin 窗口示例
    /// 展示复杂的窗口布局和交互功能
    /// </summary>
    public class AdvancedWindowSample : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Odin Samples/Windows/03 - 高级窗口")]
        private static void OpenWindow()
        {
            var window = GetWindow<AdvancedWindowSample>();
            window.minSize = new Vector2(1000, 700);
            window.titleContent = new GUIContent("高级窗口示例");
            window.Show();
        }
        
        private bool showSearchBar = true;
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                DefaultMenuStyle =
                {
                    Height = 32,
                },
                Config =
                {
                    DrawSearchToolbar = showSearchBar,
                }
            };
            
            // 主页
            tree.Add("主页", new AdvancedHomePage());
            
            // 数据编辑
            tree.Add("数据编辑/角色数据", new CharacterDataEditor());
            tree.Add("数据编辑/关卡数据", new LevelDataEditor());
            tree.Add("数据编辑/配置数据", new ConfigDataEditor());
            
            // 工具集
            tree.Add("工具集/批量处理", new BatchProcessTool());
            tree.Add("工具集/资源查找", new AssetFinderTool());
            tree.Add("工具集/场景工具", new SceneToolsPage());
            
            // 调试工具
            tree.Add("调试/性能分析", new PerformanceAnalyzer());
            tree.Add("调试/日志查看器", new LogViewer());
            tree.Add("调试/内存监控", new MemoryMonitor());
            
            // 设置
            tree.Add("设置", new AdvancedSettingsPage());
            
            return tree;
        }
        
        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            
            // 绘制顶部工具栏
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name, EditorStyles.boldLabel);
                }
                
                GUILayout.FlexibleSpace();
                
                if (GUILayout.Button("刷新", EditorStyles.toolbarButton))
                {
                    ForceMenuTreeRebuild();
                }
                
                if (GUILayout.Button("帮助", EditorStyles.toolbarButton))
                {
                    EditorUtility.DisplayDialog("帮助", "这是一个高级 Odin 窗口示例\n\n" +
                        "功能包括：\n" +
                        "• 多层级菜单导航\n" +
                        "• 数据编辑器\n" +
                        "• 工具集\n" +
                        "• 调试工具", "确定");
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        
        protected override void OnEndDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            
            if (selected == null) return;
            
            // 绘制底部信息栏
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                GUILayout.Label($"当前页面: {selected.Name}", EditorStyles.miniLabel);
                GUILayout.FlexibleSpace();
                GUILayout.Label($"Unity {Application.unityVersion}", EditorStyles.miniLabel);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
    
    #region 页面定义
    
    /// <summary>
    /// 高级主页
    /// </summary>
    public class AdvancedHomePage
    {
        [Title("高级窗口示例", TitleAlignment = TitleAlignments.Centered)]
        [HideLabel, DisplayAsString(false)]
        public string welcome = "展示 OdinMenuEditorWindow 的高级功能";
        
        [Title("快速访问")]
        [HorizontalGroup("快捷按钮")]
        [Button(ButtonSizes.Large), GUIColor(0.3f, 0.8f, 0.3f)]
        private void OpenCharacterEditor()
        {
            Debug.Log("打开角色编辑器");
        }
        
        [HorizontalGroup("快捷按钮")]
        [Button(ButtonSizes.Large), GUIColor(0.3f, 0.5f, 0.8f)]
        private void OpenLevelEditor()
        {
            Debug.Log("打开关卡编辑器");
        }
        
        [HorizontalGroup("快捷按钮")]
        [Button(ButtonSizes.Large), GUIColor(0.8f, 0.5f, 0.3f)]
        private void OpenBatchTool()
        {
            Debug.Log("打开批量工具");
        }
        
        [Title("系统状态")]
        [ProgressBar(0, 100, ColorGetter = "GetMemoryColor")]
        [ShowInInspector, LabelText("内存使用")]
        public float MemoryUsage => Random.Range(30f, 70f);
        
        [ShowInInspector, LabelText("场景对象数")]
        public int SceneObjectCount => Object.FindObjectsOfType<GameObject>().Length;
        
        private Color GetMemoryColor(float value)
        {
            return Color.Lerp(Color.green, Color.red, value / 100f);
        }
    }
    
    /// <summary>
    /// 角色数据编辑器
    /// </summary>
    public class CharacterDataEditor
    {
        [Title("角色数据编辑器")]
        
        [TableList(AlwaysExpanded = true, ShowIndexLabels = true)]
        public List<CharacterTemplate> characters = new List<CharacterTemplate>
        {
            new CharacterTemplate { name = "战士", health = 200, attack = 50, defense = 30 },
            new CharacterTemplate { name = "法师", health = 120, attack = 80, defense = 15 }
        };
        
        [Button("导出为 JSON")]
        private void ExportToJson()
        {
            string json = JsonUtility.ToJson(new CharacterList { characters = this.characters }, true);
            Debug.Log("导出 JSON:\n" + json);
        }
        
        [System.Serializable]
        public class CharacterTemplate
        {
            [TableColumnWidth(100)]
            public string name;
            
            [TableColumnWidth(80)]
            public float health;
            
            [TableColumnWidth(80)]
            public int attack;
            
            [TableColumnWidth(80)]
            public int defense;
        }
        
        [System.Serializable]
        public class CharacterList
        {
            public List<CharacterTemplate> characters;
        }
    }
    
    /// <summary>
    /// 关卡数据编辑器
    /// </summary>
    public class LevelDataEditor
    {
        [Title("关卡数据编辑器")]
        
        [FoldoutGroup("关卡列表")]
        [TableList]
        public List<LevelData> levels = new List<LevelData>();
        
        [FoldoutGroup("批量操作")]
        [Button("添加 10 个测试关卡")]
        private void AddTestLevels()
        {
            for (int i = 0; i < 10; i++)
            {
                levels.Add(new LevelData
                {
                    name = $"关卡 {i + 1}",
                    difficulty = Random.Range(1, 10),
                    enemyCount = Random.Range(5, 20)
                });
            }
        }
        
        [System.Serializable]
        public class LevelData
        {
            public string name;
            public int difficulty;
            public int enemyCount;
        }
    }
    
    /// <summary>
    /// 配置数据编辑器
    /// </summary>
    public class ConfigDataEditor
    {
        [Title("配置数据")]
        
        [BoxGroup("游戏设置")]
        [Range(0.1f, 2f)]
        public float timeScale = 1f;
        
        [BoxGroup("游戏设置")]
        public bool godMode = false;
        
        [BoxGroup("显示设置")]
        public bool showFPS = true;
        
        [BoxGroup("显示设置")]
        public bool showGrid = false;
    }
    
    /// <summary>
    /// 批量处理工具
    /// </summary>
    public class BatchProcessTool
    {
        [Title("批量处理工具")]
        
        [FoldoutGroup("选择对象")]
        [InfoBox("在场景中选择要处理的对象")]
        [ShowInInspector, ReadOnly]
        public List<GameObject> SelectedObjects
        {
            get { return Selection.gameObjects.ToList(); }
        }
        
        [FoldoutGroup("操作")]
        [Button("批量重命名")]
        private void BatchRename()
        {
            var objects = Selection.gameObjects;
            for (int i = 0; i < objects.Length; i++)
            {
                Undo.RecordObject(objects[i], "Batch Rename");
                objects[i].name = $"Object_{i:D3}";
            }
            Debug.Log($"已重命名 {objects.Length} 个对象");
        }
        
        [FoldoutGroup("操作")]
        [Button("批量设置Layer")]
        private void BatchSetLayer()
        {
            var objects = Selection.gameObjects;
            foreach (var obj in objects)
            {
                Undo.RecordObject(obj, "Set Layer");
                obj.layer = 0;
            }
            Debug.Log($"已设置 {objects.Length} 个对象的 Layer");
        }
    }
    
    /// <summary>
    /// 资源查找工具
    /// </summary>
    public class AssetFinderTool
    {
        [Title("资源查找工具")]
        
        [LabelText("搜索名称")]
        public string searchName = "";
        
        [LabelText("搜索类型")]
        [ValueDropdown("GetAssetTypes")]
        public string assetType = "GameObject";
        
        [Button("查找")]
        private void FindAssets()
        {
            string[] guids = AssetDatabase.FindAssets($"{searchName} t:{assetType}");
            Debug.Log($"找到 {guids.Length} 个匹配的资源");
            
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Debug.Log($"- {path}");
            }
        }
        
        private List<string> GetAssetTypes()
        {
            return new List<string> { "GameObject", "Material", "Texture", "Script", "Scene" };
        }
    }
    
    /// <summary>
    /// 场景工具
    /// </summary>
    public class SceneToolsPage
    {
        [Title("场景工具")]
        
        [Button("清理空对象")]
        private void CleanEmptyObjects()
        {
            var empties = Object.FindObjectsOfType<GameObject>()
                .Where(go => go.transform.childCount == 0 && go.GetComponents<Component>().Length == 1)
                .ToArray();
            
            foreach (var obj in empties)
            {
                Object.DestroyImmediate(obj);
            }
            
            Debug.Log($"已清理 {empties.Length} 个空对象");
        }
        
        [Button("统计场景对象")]
        private void CountSceneObjects()
        {
            var objects = Object.FindObjectsOfType<GameObject>();
            Debug.Log($"场景中共有 {objects.Length} 个对象");
        }
    }
    
    /// <summary>
    /// 性能分析器
    /// </summary>
    public class PerformanceAnalyzer
    {
        [Title("性能分析")]
        [InfoBox("性能分析工具")]
        
        [ShowInInspector, ReadOnly]
        [ProgressBar(0, 100)]
        public float CPUUsage => Random.Range(20f, 80f);
        
        [ShowInInspector, ReadOnly]
        [ProgressBar(0, 100)]
        public float GPUUsage => Random.Range(30f, 70f);
        
        [Button("开始分析")]
        private void StartAnalysis()
        {
            Debug.Log("开始性能分析...");
        }
    }
    
    /// <summary>
    /// 日志查看器
    /// </summary>
    public class LogViewer
    {
        [Title("日志查看器")]
        [TextArea(10, 20)]
        public string logs = "日志内容显示在这里...";
        
        [Button("清空日志")]
        private void ClearLogs()
        {
            logs = "";
        }
    }
    
    /// <summary>
    /// 内存监控
    /// </summary>
    public class MemoryMonitor
    {
        [Title("内存监控")]
        
        [ShowInInspector, ReadOnly]
        public string TotalMemory => $"{Random.Range(500, 1500)} MB";
        
        [ShowInInspector, ReadOnly]
        public string UsedMemory => $"{Random.Range(200, 800)} MB";
        
        [Button("强制GC")]
        private void ForceGC()
        {
            System.GC.Collect();
            Debug.Log("已执行垃圾回收");
        }
    }
    
    /// <summary>
    /// 高级设置页
    /// </summary>
    public class AdvancedSettingsPage
    {
        [Title("高级设置")]
        
        [TabGroup("设置", "常规")]
        public bool autoSave = true;
        
        [TabGroup("设置", "常规")]
        [Range(10, 300)]
        public int autoSaveInterval = 60;
        
        [TabGroup("设置", "编辑器")]
        public bool showGrid = true;
        
        [TabGroup("设置", "编辑器")]
        public bool showGizmos = true;
        
        [TabGroup("设置", "调试")]
        public bool enableDebugMode = false;
        
        [TabGroup("设置", "调试")]
        [ShowIf("enableDebugMode")]
        public bool verboseLogging = false;
    }
    
    #endregion
}
#endif

