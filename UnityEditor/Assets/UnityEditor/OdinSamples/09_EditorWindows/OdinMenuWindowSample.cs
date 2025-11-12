#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace OdinSamples.EditorWindows
{
    /// <summary>
    /// OdinMenuEditorWindow 示例
    /// 展示如何创建带侧边栏菜单的编辑器窗口
    /// </summary>
    public class OdinMenuWindowSample : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Odin Samples/Windows/02 - 菜单窗口")]
        private static void OpenWindow()
        {
            var window = GetWindow<OdinMenuWindowSample>();
            window.minSize = new Vector2(900, 600);
            window.titleContent = new GUIContent("Odin 菜单窗口示例");
            window.Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                // 配置菜单树
                { "主页", new HomePage() },
                { "角色管理", null },
                { "角色管理/角色列表", new CharacterListPage() },
                { "角色管理/创建角色", new CreateCharacterPage() },
                { "物品管理", null },
                { "物品管理/武器", new WeaponPage() },
                { "物品管理/防具", new ArmorPage() },
                { "物品管理/消耗品", new ConsumablePage() },
                { "设置", new SettingsPage() },
            };
            
            // 设置菜单样式
            tree.Config.DrawSearchToolbar = true;
            tree.DefaultMenuStyle.Height = 35;
            
            // 添加所有角色到菜单
            AddCharactersToMenu(tree);
            
            return tree;
        }
        
        private void AddCharactersToMenu(OdinMenuTree tree)
        {
            var characters = CharacterDatabase.Instance.characters;
            
            for (int i = 0; i < characters.Count; i++)
            {
                tree.Add($"角色管理/所有角色/{characters[i].name}", characters[i]);
            }
        }
        
        protected override void OnBeginDrawEditors()
        {
            // 在开始绘制编辑器之前，绘制工具栏
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;
            
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }
                
                if (SirenixEditorGUI.ToolbarButton(EditorIcons.Refresh))
                {
                    ForceMenuTreeRebuild();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
    
    #region 页面类
    
    /// <summary>
    /// 主页
    /// </summary>
    public class HomePage
    {
        [Title("欢迎使用游戏数据编辑器", TitleAlignment = TitleAlignments.Centered)]
        [HideLabel]
        [DisplayAsString(false)]
        [PropertySpace(20)]
        public string welcome = "这是一个使用 OdinMenuEditorWindow 创建的完整编辑器窗口示例";
        
        [Title("功能介绍")]
        [InfoBox(
            "• 角色管理 - 创建和管理游戏角色\n" +
            "• 物品管理 - 管理武器、防具和消耗品\n" +
            "• 设置 - 编辑器设置\n\n" +
            "点击左侧菜单开始使用")]
        public string info;
        
        [Title("统计信息")]
        [ShowInInspector, ReadOnly, LabelText("角色数量")]
        public int CharacterCount => CharacterDatabase.Instance.characters.Count;
        
        [ShowInInspector, ReadOnly, LabelText("武器数量")]
        public int WeaponCount => ItemDatabase.Instance.weapons.Count;
        
        [ShowInInspector, ReadOnly, LabelText("防具数量")]
        public int ArmorCount => ItemDatabase.Instance.armors.Count;
        
        [Button("重置所有数据", ButtonSizes.Large)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ResetAllData()
        {
            if (EditorUtility.DisplayDialog("确认重置", "确定要重置所有数据吗？", "确定", "取消"))
            {
                CharacterDatabase.Instance.characters.Clear();
                ItemDatabase.Instance.weapons.Clear();
                ItemDatabase.Instance.armors.Clear();
                Debug.Log("所有数据已重置");
            }
        }
    }
    
    /// <summary>
    /// 角色列表页
    /// </summary>
    public class CharacterListPage
    {
        [Title("角色列表")]
        [TableList(ShowIndexLabels = true, AlwaysExpanded = true)]
        [ShowInInspector]
        public List<CharacterData> Characters => CharacterDatabase.Instance.characters;
        
        [Button("添加新角色", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void AddCharacter()
        {
            CharacterDatabase.Instance.characters.Add(new CharacterData
            {
                name = "新角色",
                level = 1,
                health = 100,
                attack = 10
            });
        }
        
        [Button("清空列表", ButtonSizes.Large)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ClearCharacters()
        {
            if (EditorUtility.DisplayDialog("确认", "确定要清空角色列表吗？", "确定", "取消"))
            {
                CharacterDatabase.Instance.characters.Clear();
            }
        }
    }
    
    /// <summary>
    /// 创建角色页
    /// </summary>
    public class CreateCharacterPage
    {
        [Title("创建新角色")]
        
        [BoxGroup("基础信息")]
        [LabelText("角色名称")]
        [Required]
        public string characterName = "";
        
        [BoxGroup("基础信息")]
        [LabelText("等级")]
        [Range(1, 100)]
        public int level = 1;
        
        [BoxGroup("属性")]
        [LabelText("生命值")]
        [Range(1, 1000)]
        public float health = 100;
        
        [BoxGroup("属性")]
        [LabelText("攻击力")]
        [Range(1, 100)]
        public int attack = 10;
        
        [BoxGroup("属性")]
        [LabelText("防御力")]
        [Range(1, 100)]
        public int defense = 5;
        
        [Button("创建角色", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void CreateCharacter()
        {
            if (string.IsNullOrEmpty(characterName))
            {
                EditorUtility.DisplayDialog("错误", "请输入角色名称", "确定");
                return;
            }
            
            CharacterDatabase.Instance.characters.Add(new CharacterData
            {
                name = characterName,
                level = level,
                health = health,
                attack = attack,
                defense = defense
            });
            
            Debug.Log($"已创建角色：{characterName}");
            
            // 重置表单
            characterName = "";
            level = 1;
            health = 100;
            attack = 10;
            defense = 5;
        }
    }
    
    /// <summary>
    /// 武器页
    /// </summary>
    public class WeaponPage
    {
        [Title("武器管理")]
        [TableList(AlwaysExpanded = true)]
        [ShowInInspector]
        public List<WeaponData> Weapons => ItemDatabase.Instance.weapons;
        
        [Button("添加武器")]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void AddWeapon()
        {
            ItemDatabase.Instance.weapons.Add(new WeaponData
            {
                name = "新武器",
                damage = 10,
                durability = 100
            });
        }
    }
    
    /// <summary>
    /// 防具页
    /// </summary>
    public class ArmorPage
    {
        [Title("防具管理")]
        [TableList(AlwaysExpanded = true)]
        [ShowInInspector]
        public List<ArmorData> Armors => ItemDatabase.Instance.armors;
        
        [Button("添加防具")]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void AddArmor()
        {
            ItemDatabase.Instance.armors.Add(new ArmorData
            {
                name = "新防具",
                defense = 5,
                durability = 100
            });
        }
    }
    
    /// <summary>
    /// 消耗品页
    /// </summary>
    public class ConsumablePage
    {
        [Title("消耗品管理")]
        [InfoBox("消耗品功能开发中...")]
        
        [Button("添加消耗品")]
        [DisableInEditorMode]
        private void AddConsumable()
        {
            Debug.Log("添加消耗品");
        }
    }
    
    /// <summary>
    /// 设置页
    /// </summary>
    public class SettingsPage
    {
        [Title("编辑器设置")]
        
        [BoxGroup("显示设置")]
        [LabelText("显示调试信息")]
        public bool showDebugInfo = false;
        
        [BoxGroup("显示设置")]
        [LabelText("自动保存")]
        public bool autoSave = true;
        
        [BoxGroup("显示设置")]
        [LabelText("自动保存间隔（秒）")]
        [Range(10, 300)]
        [ShowIf("autoSave")]
        public int autoSaveInterval = 60;
        
        [BoxGroup("默认值")]
        [LabelText("默认角色等级")]
        [Range(1, 100)]
        public int defaultCharacterLevel = 1;
        
        [BoxGroup("默认值")]
        [LabelText("默认生命值")]
        [Range(1, 1000)]
        public float defaultHealth = 100;
        
        [Button("重置设置")]
        [GUIColor(0.8f, 0.5f, 0.3f)]
        private void ResetSettings()
        {
            showDebugInfo = false;
            autoSave = true;
            autoSaveInterval = 60;
            defaultCharacterLevel = 1;
            defaultHealth = 100;
            Debug.Log("设置已重置");
        }
    }
    
    #endregion
    
    #region 数据类
    
    /// <summary>
    /// 角色数据
    /// </summary>
    [System.Serializable]
    public class CharacterData
    {
        [TableColumnWidth(120)]
        [LabelText("名称")]
        public string name;
        
        [TableColumnWidth(60)]
        [LabelText("等级")]
        public int level;
        
        [TableColumnWidth(80)]
        [LabelText("生命值")]
        [ProgressBar(0, 1000)]
        public float health;
        
        [TableColumnWidth(80)]
        [LabelText("攻击力")]
        public int attack;
        
        [TableColumnWidth(80)]
        [LabelText("防御力")]
        public int defense;
    }
    
    /// <summary>
    /// 武器数据
    /// </summary>
    [System.Serializable]
    public class WeaponData
    {
        [TableColumnWidth(120)]
        public string name;
        
        [TableColumnWidth(80)]
        public int damage;
        
        [TableColumnWidth(80)]
        [ProgressBar(0, 100)]
        public int durability;
    }
    
    /// <summary>
    /// 防具数据
    /// </summary>
    [System.Serializable]
    public class ArmorData
    {
        [TableColumnWidth(120)]
        public string name;
        
        [TableColumnWidth(80)]
        public int defense;
        
        [TableColumnWidth(80)]
        [ProgressBar(0, 100)]
        public int durability;
    }
    
    /// <summary>
    /// 角色数据库（单例）
    /// </summary>
    public class CharacterDatabase
    {
        private static CharacterDatabase _instance;
        public static CharacterDatabase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CharacterDatabase();
                return _instance;
            }
        }
        
        public List<CharacterData> characters = new List<CharacterData>
        {
            new CharacterData { name = "战士", level = 10, health = 200, attack = 50, defense = 30 },
            new CharacterData { name = "法师", level = 8, health = 120, attack = 80, defense = 15 },
            new CharacterData { name = "刺客", level = 12, health = 150, attack = 90, defense = 20 }
        };
    }
    
    /// <summary>
    /// 物品数据库（单例）
    /// </summary>
    public class ItemDatabase
    {
        private static ItemDatabase _instance;
        public static ItemDatabase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ItemDatabase();
                return _instance;
            }
        }
        
        public List<WeaponData> weapons = new List<WeaponData>
        {
            new WeaponData { name = "铁剑", damage = 20, durability = 100 },
            new WeaponData { name = "钢剑", damage = 35, durability = 150 }
        };
        
        public List<ArmorData> armors = new List<ArmorData>
        {
            new ArmorData { name = "皮甲", defense = 10, durability = 80 },
            new ArmorData { name = "铁甲", defense = 25, durability = 120 }
        };
    }
    
    #endregion
}
#endif

