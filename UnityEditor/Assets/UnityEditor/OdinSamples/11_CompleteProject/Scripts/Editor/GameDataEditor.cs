#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Toma.OdinSamples.CompleteProject
{
    /// <summary>
    /// 游戏数据编辑器主窗口
    /// 这是整个完整项目示例的核心，展示了如何构建专业的数据编辑工具
    /// 
    /// 学习要点：
    /// 1. 继承 OdinMenuEditorWindow 创建带菜单的编辑器窗口
    /// 2. 使用 BuildMenuTree 构建菜单结构
    /// 3. 使用 AddAllAssetsAtPath 自动添加资源
    /// 4. 自定义工具栏和交互功能
    /// 5. 实现数据创建和管理工作流程
    /// 
    /// 参考：官方 RPG Editor Demo
    /// </summary>
    public class GameDataEditor : OdinMenuEditorWindow
    {
        #region 窗口打开

        [MenuItem("Tools/Odin Samples/Complete Project/打开游戏数据编辑器")]
        private static void OpenWindow()
        {
            var window = GetWindow<GameDataEditor>();
            window.minSize = new Vector2(900, 600);
            window.titleContent = new GUIContent("游戏数据编辑器");
            window.Show();
        }

        #endregion

        #region 构建菜单树

        /// <summary>
        /// 构建菜单树
        /// 这是 Odin Editor Window 的核心方法，定义了左侧菜单的结构
        /// </summary>
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                { "主页", this, EditorIcons.House },  // 添加主页，显示欢迎信息
                { "使用说明", new UsageGuide() },       // 添加使用说明页面
            };

            // 配置菜单树
            tree.Config.DrawSearchToolbar = true;  // 显示搜索栏
            tree.DefaultMenuStyle.Height = 30;      // 菜单项高度

            // === 添加角色数据 ===
            AddCharactersToTree(tree);

            // === 添加技能数据 ===
            AddSkillsToTree(tree);

            // === 添加物品数据 ===
            AddItemsToTree(tree);

            // === 添加统计信息 ===
            tree.Add("统计信息", new DataStatistics());

            return tree;
        }

        /// <summary>
        /// 添加角色数据到菜单树
        /// </summary>
        private void AddCharactersToTree(OdinMenuTree tree)
        {
            // 创建角色分类节点
            tree.Add("角色管理/所有角色", null);

            // 自动添加所有角色资源
            // AddAllAssetsAtPath 会扫描指定路径下的所有指定类型资源
            tree.AddAllAssetsAtPath(
                "角色管理/所有角色",
                "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Characters",
                typeof(Character),
                includeSubDirectories: true,
                flattenSubDirectories: false);

            // 按职业分类显示角色
            var characters = AssetDatabase.FindAssets("t:Character", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Characters" })
                .Select(guid => AssetDatabase.LoadAssetAtPath<Character>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(c => c != null);

            foreach (var character in characters)
            {
                tree.Add($"角色管理/按职业/{character.Class}/{character.CharacterName}", character);
            }
        }

        /// <summary>
        /// 添加技能数据到菜单树
        /// </summary>
        private void AddSkillsToTree(OdinMenuTree tree)
        {
            tree.Add("技能管理/所有技能", null);

            tree.AddAllAssetsAtPath(
                "技能管理/所有技能",
                "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Skills",
                typeof(Skill),
                includeSubDirectories: true,
                flattenSubDirectories: false);

            // 按技能类型分类
            var skills = AssetDatabase.FindAssets("t:Skill", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Skills" })
                .Select(guid => AssetDatabase.LoadAssetAtPath<Skill>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(s => s != null);

            foreach (var skill in skills)
            {
                tree.Add($"技能管理/按类型/{skill.Type}/{skill.SkillName}", skill);
                tree.Add($"技能管理/按元素/{skill.Element}/{skill.SkillName}", skill);
            }
        }

        /// <summary>
        /// 添加物品数据到菜单树
        /// </summary>
        private void AddItemsToTree(OdinMenuTree tree)
        {
            tree.Add("物品管理/所有物品", null);

            tree.AddAllAssetsAtPath(
                "物品管理/所有物品",
                "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Items",
                typeof(Item),
                includeSubDirectories: true,
                flattenSubDirectories: false);

            // 按物品类型和稀有度分类
            var items = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Items" })
                .Select(guid => AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(i => i != null);

            foreach (var item in items)
            {
                tree.Add($"物品管理/按类型/{item.Type}/{item.ItemName}", item);
                tree.Add($"物品管理/按稀有度/{item.Rarity}/{item.ItemName}", item);
            }
        }

        #endregion

        #region 自定义工具栏

        /// <summary>
        /// 在编辑器内容绘制之前调用
        /// 用于绘制自定义工具栏
        /// </summary>
        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();

            // 绘制工具栏
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                // 显示当前选中项的名称
                if (selected != null)
                {
                    GUILayout.Label(selected.Name, EditorStyles.boldLabel);
                }
                else
                {
                    GUILayout.Label("游戏数据编辑器", EditorStyles.boldLabel);
                }

                GUILayout.FlexibleSpace();

                // === 创建数据按钮 ===

                if (GUILayout.Button("创建角色", EditorStyles.toolbarButton, GUILayout.Width(80)))
                {
                    CreateCharacter();
                }

                if (GUILayout.Button("创建技能", EditorStyles.toolbarButton, GUILayout.Width(80)))
                {
                    CreateSkill();
                }

                if (GUILayout.Button("创建物品", EditorStyles.toolbarButton, GUILayout.Width(80)))
                {
                    CreateItem();
                }

                GUILayout.Space(10);

                // === 工具按钮 ===

                if (GUILayout.Button("刷新", EditorStyles.toolbarButton, GUILayout.Width(50)))
                {
                    ForceMenuTreeRebuild();
                    Debug.Log("菜单已刷新");
                }

                if (GUILayout.Button("帮助", EditorStyles.toolbarButton, GUILayout.Width(50)))
                {
                    ShowHelp();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 在编辑器内容绘制之后调用
        /// 用于绘制底部信息栏
        /// </summary>
        protected override void OnEndDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                if (selected != null)
                {
                    var path = AssetDatabase.GetAssetPath(selected.Value as Object);
                    if (!string.IsNullOrEmpty(path))
                    {
                        GUILayout.Label($"路径: {path}", EditorStyles.miniLabel);
                    }
                }

                GUILayout.FlexibleSpace();

                // 显示统计信息
                int characterCount = AssetDatabase.FindAssets("t:Character", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data" }).Length;
                int skillCount = AssetDatabase.FindAssets("t:Skill", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data" }).Length;
                int itemCount = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data" }).Length;

                GUILayout.Label($"角色: {characterCount}  技能: {skillCount}  物品: {itemCount}", EditorStyles.miniLabel);
            }
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region 数据创建方法

        /// <summary>
        /// 创建新角色
        /// </summary>
        private void CreateCharacter()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "创建新角色",
                "New Character",
                "asset",
                "请输入角色名称",
                "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Characters");

            if (!string.IsNullOrEmpty(path))
            {
                var character = ScriptableObject.CreateInstance<Character>();
                character.CharacterName = System.IO.Path.GetFileNameWithoutExtension(path);
                AssetDatabase.CreateAsset(character, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                // 选中新创建的角色
                TrySelectMenuItemWithObject(character);

                Debug.Log($"已创建角色: {path}");
            }
        }

        /// <summary>
        /// 创建新技能
        /// </summary>
        private void CreateSkill()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "创建新技能",
                "New Skill",
                "asset",
                "请输入技能名称",
                "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Skills");

            if (!string.IsNullOrEmpty(path))
            {
                var skill = ScriptableObject.CreateInstance<Skill>();
                skill.SkillName = System.IO.Path.GetFileNameWithoutExtension(path);
                AssetDatabase.CreateAsset(skill, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                TrySelectMenuItemWithObject(skill);

                Debug.Log($"已创建技能: {path}");
            }
        }

        /// <summary>
        /// 创建新物品
        /// </summary>
        private void CreateItem()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "创建新物品",
                "New Item",
                "asset",
                "请输入物品名称",
                "Assets/Scripts/OdinSamples/11_CompleteProject/Data/Items");

            if (!string.IsNullOrEmpty(path))
            {
                var item = ScriptableObject.CreateInstance<Item>();
                item.ItemName = System.IO.Path.GetFileNameWithoutExtension(path);
                AssetDatabase.CreateAsset(item, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                TrySelectMenuItemWithObject(item);

                Debug.Log($"已创建物品: {path}");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 显示帮助信息
        /// </summary>
        private void ShowHelp()
        {
            EditorUtility.DisplayDialog(
                "游戏数据编辑器 - 帮助",
                "欢迎使用游戏数据编辑器！\n\n" +
                "功能说明：\n" +
                "• 创建角色：创建新的角色数据\n" +
                "• 创建技能：创建新的技能数据\n" +
                "• 创建物品：创建新的物品数据\n" +
                "• 搜索栏：快速查找数据\n" +
                "• 分类浏览：按类型浏览数据\n\n" +
                "提示：\n" +
                "- 左侧菜单支持多选（Ctrl/Cmd + 点击）\n" +
                "- 可以拖放资源到相应的字段\n" +
                "- 使用搜索栏快速定位数据\n\n" +
                "详细说明请查看 README.md",
                "确定");
        }

        #endregion

        #region 主页数据

        [Title("欢迎使用游戏数据编辑器")]
        [InfoBox("这是一个完整的 Odin Inspector 应用示例，展示了如何构建专业的游戏数据管理工具。\n\n" +
                 "通过左侧菜单可以创建和编辑角色、技能、物品等游戏数据。\n\n" +
                 "点击工具栏的按钮可以创建新数据，或点击'帮助'查看详细说明。")]
        [PropertySpace(20)]
        [ShowInInspector, DisplayAsString, GUIColor(0.3f, 0.8f, 0.3f)]
        [LabelText("快速开始")]
        private string QuickStart = "→ 点击工具栏的'创建角色'/'创建技能'/'创建物品'按钮开始创建数据";

        [ShowInInspector, DisplayAsString]
        [LabelText("学习建议")]
        private string LearningTips = "1. 先阅读 README.md 了解项目结构\n" +
                                     "2. 创建一些示例数据体验功能\n" +
                                     "3. 查看代码注释了解实现细节\n" +
                                     "4. 尝试修改和扩展功能";

        #endregion
    }

    #region 辅助页面类

    /// <summary>
    /// 使用说明页面
    /// </summary>
    public class UsageGuide
    {
        [Title("使用说明")]
        [InfoBox("详细的使用说明和学习指南")]
        [TextArea(10, 20)]
        public string Guide = 
            "=== 游戏数据编辑器使用指南 ===\n\n" +
            "1. 创建数据\n" +
            "   - 点击工具栏的创建按钮\n" +
            "   - 选择保存位置和文件名\n" +
            "   - 在右侧编辑数据\n\n" +
            "2. 编辑数据\n" +
            "   - 在左侧菜单选择数据\n" +
            "   - 右侧显示详细编辑器\n" +
            "   - 使用 Odin 属性快速编辑\n\n" +
            "3. 搜索数据\n" +
            "   - 使用顶部搜索栏\n" +
            "   - 支持模糊搜索\n" +
            "   - 按类型分类浏览\n\n" +
            "4. 技巧\n" +
            "   - 拖放资源到字段\n" +
            "   - 使用按钮快速操作\n" +
            "   - 查看代码学习实现\n\n" +
            "更多信息请参阅 README.md";
    }

    /// <summary>
    /// 数据统计页面
    /// </summary>
    public class DataStatistics
    {
        [Title("数据统计")]
        [InfoBox("游戏数据的统计信息")]
        [ShowInInspector, ReadOnly, DisplayAsString]
        [LabelText("总角色数")]
        public int CharacterCount => AssetDatabase.FindAssets("t:Character", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data" }).Length;

        [ShowInInspector, ReadOnly, DisplayAsString]
        [LabelText("总技能数")]
        public int SkillCount => AssetDatabase.FindAssets("t:Skill", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data" }).Length;

        [ShowInInspector, ReadOnly, DisplayAsString]
        [LabelText("总物品数")]
        public int ItemCount => AssetDatabase.FindAssets("t:Item", new[] { "Assets/Scripts/OdinSamples/11_CompleteProject/Data" }).Length;

        [Button("刷新统计", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void Refresh()
        {
            Debug.Log("统计信息已刷新");
        }
    }

    #endregion
}
#endif

