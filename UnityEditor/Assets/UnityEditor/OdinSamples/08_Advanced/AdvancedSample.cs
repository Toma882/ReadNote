using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace OdinSamples.Advanced
{
    /// <summary>
    /// Odin Inspector 高级特性演示
    /// 展示各种高级功能和组合使用
    /// </summary>
    public class AdvancedSample : MonoBehaviour
    {
        #region OnInspectorGUI 自定义GUI
        
        [Title("自定义GUI绘制")]
        [InfoBox("OnInspectorGUI 允许自定义Inspector绘制")]
        
        [ShowInInspector, HideLabel]
        [OnInspectorGUI("DrawCustomGUI")]
        public string customGUIField = "";
        
        private void DrawCustomGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("自定义绘制区域", EditorStyles.boldLabel);
            GUILayout.Label("这是通过 OnInspectorGUI 绘制的自定义内容");
            GUILayout.Label($"当前时间：{System.DateTime.Now:HH:mm:ss}");
            
            if (GUILayout.Button("自定义按钮"))
            {
                Debug.Log("自定义按钮被点击");
            }
            EditorGUILayout.EndVertical();
        }
        
        #endregion

        #region InlineProperty 内联属性
        
        [Title("内联属性")]
        
        [InlineProperty]
        [InfoBox("InlineProperty 将对象的属性展开显示在一行")]
        public CharacterStats stats = new CharacterStats();
        
        [InlineProperty(LabelWidth = 80)]
        [InfoBox("可以自定义标签宽度")]
        public Vector3Data position = new Vector3Data();
        
        #endregion

        #region PropertySpace 和 PropertyOrder
        
        [Title("间距和顺序")]
        
        [PropertyOrder(3)]
        public string third = "第三个";
        
        [PropertyOrder(1)]
        [PropertySpace(0, 20)]
        public string first = "第一个";
        
        [PropertyOrder(2)]
        public string second = "第二个";
        
        #endregion

        #region Searchable 可搜索
        
        [Title("可搜索列表")]
        
        [Searchable]
        [InfoBox("Searchable 为列表添加搜索功能")]
        public List<string> itemDatabase = new List<string>
        {
            "铁剑", "钢剑", "魔法剑", "传说之剑",
            "皮甲", "铁甲", "魔法袍", "龙鳞甲",
            "生命药水", "魔法药水", "解毒剂", "力量药水"
        };
        
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        [TableList]
        public List<SearchableCharacter> characters = new List<SearchableCharacter>
        {
            new SearchableCharacter { name = "战士", level = 10, characterClass = "战士" },
            new SearchableCharacter { name = "法师", level = 8, characterClass = "法师" },
            new SearchableCharacter { name = "刺客", level = 12, characterClass = "刺客" }
        };
        
        #endregion

        #region ProgressBar 进度条
        
        [Title("进度条")]
        
        [ProgressBar(0, 100)]
        [InfoBox("ProgressBar 显示进度条")]
        public float health = 75f;
        
        [ProgressBar(0, 100, ColorGetter = "GetManaColor")]
        [InfoBox("可以自定义进度条颜色")]
        public float mana = 50f;
        
        [ProgressBar(0, 1000, Height = 30)]
        [InfoBox("可以自定义高度")]
        public float experience = 350f;
        
        [ProgressBar(0, 100, ColorMember = "healthBarColor")]
        [InfoBox("从字段获取颜色")]
        public float stamina = 80f;
        
        public Color healthBarColor = new Color(1f, 0.5f, 0f);
        
        private Color GetManaColor(float value)
        {
            return Color.Lerp(Color.red, Color.blue, value / 100f);
        }
        
        #endregion

        #region CustomValueDrawer 自定义值绘制
        
        [Title("自定义值绘制")]
        
        [CustomValueDrawer("DrawHealthBar")]
        [InfoBox("CustomValueDrawer 完全自定义值的绘制方式")]
        public int customHealth = 100;
        
        private int DrawHealthBar(int value, GUIContent label)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(100));
            value = EditorGUILayout.IntSlider(value, 0, 100);
            GUILayout.EndHorizontal();
            
            var rect = GUILayoutUtility.GetLastRect();
            rect.y += 25;
            rect.height = 20;
            
            EditorGUI.ProgressBar(rect, value / 100f, $"{value}/100");
            GUILayout.Space(25);
            
            return value;
        }
        
        #endregion

        #region OnValueChanged 值改变回调
        
        [Title("值改变回调")]
        
        [OnValueChanged("OnDifficultyChanged")]
        [InfoBox("OnValueChanged 在值改变时触发回调")]
        [Range(1, 10)]
        public int difficulty = 5;
        
        [ReadOnly]
        [ShowInInspector]
        public string difficultyDescription = "普通";
        
        private void OnDifficultyChanged()
        {
            if (difficulty <= 3)
                difficultyDescription = "简单";
            else if (difficulty <= 7)
                difficultyDescription = "普通";
            else
                difficultyDescription = "困难";
            
            Debug.Log($"难度已改变为：{difficultyDescription} ({difficulty})");
        }
        
        #endregion

        #region DelayedProperty 延迟属性
        
        [Title("延迟属性")]
        
        [DelayedProperty]
        [InfoBox("DelayedProperty 在失去焦点或按回车后才应用值")]
        [OnValueChanged("OnNameChanged")]
        public string delayedName = "";
        
        private void OnNameChanged()
        {
            Debug.Log($"名称已更改为：{delayedName}");
        }
        
        #endregion

        #region WrapProperty 包装属性
        
        [Title("包装属性")]
        
        [LabelText("包装的整数")]
        [PropertyRange(0, 100)]
        [Wrap(0, 100)]
        [InfoBox("Wrap 让值在范围内循环")]
        public int wrappedValue = 50;
        
        #endregion

        #region HideInPlayMode / HideInEditorMode
        
        [Title("运行模式显示控制")]
        
        [HideInPlayMode]
        [InfoBox("只在编辑模式显示")]
        public string editorOnlyField = "编辑器专用";
        
        [HideInEditorMode]
        [InfoBox("只在运行时显示")]
        public string runtimeOnlyField = "运行时专用";
        
        [ShowIf("@UnityEngine.Application.isPlaying")]
        [InfoBox("只在运行时显示（使用条件表达式）")]
        public float runtimeTimer = 0f;
        
        void Update()
        {
            if (Application.isPlaying)
            {
                runtimeTimer = Time.time;
            }
        }
        
        #endregion

        #region 复杂组合示例
        
        [Title("复杂组合示例")]
        
        [TabGroup("游戏数据", "角色")]
        [BoxGroup("游戏数据/角色/基础信息")]
        [HorizontalGroup("游戏数据/角色/基础信息/名称行")]
        [LabelText("角色名")]
        [ValidateInput("ValidateCharacterName", "名称不能为空")]
        public string characterName = "无名勇者";
        
        [HorizontalGroup("游戏数据/角色/基础信息/名称行")]
        [LabelText("等级")]
        [Range(1, 99)]
        public int characterLevel = 1;
        
        [BoxGroup("游戏数据/角色/基础信息")]
        [ProgressBar(0, 100, ColorGetter = "GetHealthColor")]
        [LabelText("生命值")]
        public float characterHealth = 100f;
        
        [BoxGroup("游戏数据/角色/基础信息")]
        [ProgressBar(0, 100, 0.3f, 0.5f, 0.8f)]
        [LabelText("魔法值")]
        public float characterMana = 100f;
        
        [BoxGroup("游戏数据/角色/属性")]
        [TableList(AlwaysExpanded = true)]
        public List<Attribute> attributes = new List<Attribute>
        {
            new Attribute { name = "力量", value = 10 },
            new Attribute { name = "敏捷", value = 10 },
            new Attribute { name = "智力", value = 10 }
        };
        
        [TabGroup("游戏数据", "装备")]
        [InlineEditor(InlineEditorModes.FullEditor)]
        public GameObject weaponPrefab;
        
        [TabGroup("游戏数据", "装备")]
        [PreviewField(100, ObjectFieldAlignment.Center)]
        public Sprite weaponIcon;
        
        [TabGroup("游戏数据", "技能")]
        [Searchable]
        [TableList]
        public List<Skill> skills = new List<Skill>();
        
        [TabGroup("游戏数据", "操作")]
        [Button("保存角色数据", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void SaveCharacterData()
        {
            Debug.Log($"保存角色：{characterName} Lv.{characterLevel}");
            Debug.Log($"生命：{characterHealth}/100, 魔法：{characterMana}/100");
        }
        
        [TabGroup("游戏数据", "操作")]
        [Button("重置角色数据", ButtonSizes.Large)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ResetCharacterData()
        {
            characterName = "无名勇者";
            characterLevel = 1;
            characterHealth = 100f;
            characterMana = 100f;
            Debug.Log("角色数据已重置");
        }
        
        private bool ValidateCharacterName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
        
        private Color GetHealthColor(float value)
        {
            if (value > 70) return Color.green;
            if (value > 30) return Color.yellow;
            return Color.red;
        }
        
        #endregion

        #region 内部类定义
        
        [System.Serializable]
        public class CharacterStats
        {
            [HorizontalGroup("行1")]
            public int strength = 10;
            
            [HorizontalGroup("行1")]
            public int agility = 10;
            
            [HorizontalGroup("行2")]
            public int intelligence = 10;
            
            [HorizontalGroup("行2")]
            public int vitality = 10;
        }
        
        [System.Serializable]
        public class Vector3Data
        {
            [HorizontalGroup]
            public float x = 0;
            
            [HorizontalGroup]
            public float y = 0;
            
            [HorizontalGroup]
            public float z = 0;
        }
        
        [System.Serializable]
        public class SearchableCharacter : ISearchFilterable
        {
            [TableColumnWidth(100)]
            public string name;
            
            [TableColumnWidth(60)]
            public int level;
            
            [TableColumnWidth(80)]
            public string characterClass;
            
            public bool IsMatch(string searchString)
            {
                return name.Contains(searchString) || characterClass.Contains(searchString);
            }
        }
        
        [System.Serializable]
        public class Attribute
        {
            [TableColumnWidth(100)]
            public string name;
            
            [TableColumnWidth(80)]
            [ProgressBar(0, 100)]
            public int value;
        }
        
        [System.Serializable]
        public class Skill
        {
            [TableColumnWidth(100)]
            public string name;
            
            [TableColumnWidth(200)]
            [MultiLineProperty(2)]
            public string description;
            
            [TableColumnWidth(60)]
            public int level;
        }
        
        #endregion
    }
}

