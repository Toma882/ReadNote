using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

namespace OdinSamples.Groups
{

    /*
        学习要点：
        ----------------------
        FoldoutGroup 特性可以创建可折叠的分组
            参数
            string name: 分组名称
        ----------------------
        BoxGroup 特性可以创建带边框的分组
        ----------------------
        TabGroup 特性可以创建选项卡式布局
            参数
            string name: 选项卡组名称
        ----------------------
        HorizontalGroup 特性可以创建水平组
            参数
            string name: 水平组名称
        ----------------------
        VerticalGroup 特性可以创建垂直组
            参数
            string name: 垂直组名称
        ----------------------
        TitleGroup 特性可以创建带标题的分组
            参数
            string name: 标题组名称
        ----------------------
        ToggleGroup 特性可以创建可开关的分组
            参数
            string name: 开关组名称
        ----------------------
        ResponsiveButtonGroup 特性可以创建响应式按钮组
            参数
            string name: 响应式按钮组名称
        ----------------------
        ResponsiveButton 特性可以创建响应式按钮
            参数
            string name: 响应式按钮名称
        ----------------------
        ResponsiveButtonGroup 特性可以创建响应式按钮组
            参数
            string name: 响应式按钮组名称
        ----------------------
        ResponsiveButton 特性可以创建响应式按钮
            参数
            string name: 响应式按钮名称
        ----------------------
        ResponsiveButtonGroup 特性可以创建响应式按钮组
            参数
            string name: 响应式按钮组名称
        ----------------------  
    */

    /// <summary>
    /// Odin Inspector 分组和布局演示
    /// 展示各种分组和布局特性
    /// </summary>
    public class GroupsSample : MonoBehaviour
    {
        #region FoldoutGroup 折叠组
        
        [Title("折叠组")]
        [InfoBox("FoldoutGroup 创建可折叠的分组")]
        
        [FoldoutGroup("玩家属性")]
        public string playerName = "勇者";
        
        [FoldoutGroup("玩家属性")]
        public int playerLevel = 1;
        
        [FoldoutGroup("玩家属性")]
        public float playerHealth = 100f;
        
        [FoldoutGroup("玩家属性")]
        public int playerMana = 50;
        
        [FoldoutGroup("敌人属性")]
        public string enemyName = "哥布林";
        
        [FoldoutGroup("敌人属性")]
        public int enemyLevel = 5;
        
        [FoldoutGroup("敌人属性")]
        public float enemyHealth = 80f;
        
        [FoldoutGroup("敌人属性", Expanded = true)]
        [InfoBox("Expanded = true 默认展开组")]
        public int enemyDamage = 15;
        
        #endregion

        #region BoxGroup 盒子组
        
        [Title("盒子组")]
        [InfoBox("BoxGroup 创建带边框的分组")]
        
        [BoxGroup("基础信息")]
        public string itemName = "铁剑";
        
        [BoxGroup("基础信息")]
        public string itemDescription = "一把普通的铁剑";
        
        [BoxGroup("基础信息")]
        public int itemLevel = 5;
        
        [BoxGroup("物品属性")]
        public int attack = 20;
        
        [BoxGroup("物品属性")]
        public int defense = 5;
        
        [BoxGroup("物品属性")]
        public float weight = 3.5f;
        
        [BoxGroup("物品属性", ShowLabel = true)]
        [InfoBox("ShowLabel 显示组标签")]
        public int durability = 100;
        
        #endregion

        #region TabGroup 选项卡组
        
        [Title("选项卡组")]
        [InfoBox("TabGroup 创建选项卡式布局")]
        
        [TabGroup("角色信息", "基本")]
        public string characterName = "战士";
        
        [TabGroup("角色信息", "基本")]
        public int characterLevel = 10;
        
        [TabGroup("角色信息", "基本")]
        public float characterExp = 500f;
        
        [TabGroup("角色信息", "属性")]
        public int strength = 20;
        
        [TabGroup("角色信息", "属性")]
        public int agility = 15;
        
        [TabGroup("角色信息", "属性")]
        public int intelligence = 10;
        
        [TabGroup("角色信息", "装备")]
        public GameObject weapon;
        
        [TabGroup("角色信息", "装备")]
        public GameObject armor;
        
        [TabGroup("角色信息", "装备")]
        public GameObject accessory;
        
        #endregion

        #region HorizontalGroup 水平组
        
        [Title("水平组")]
        [InfoBox("HorizontalGroup 将字段水平排列")]
        
        [HorizontalGroup("坐标")]
        public float posX = 0f;
        
        [HorizontalGroup("坐标")]
        public float posY = 0f;
        
        [HorizontalGroup("坐标")]
        public float posZ = 0f;
        
        [HorizontalGroup("尺寸", Width = 0.33f)]
        [LabelText("宽")]
        public float sizeWidth = 1f;
        
        [HorizontalGroup("尺寸", Width = 0.33f)]
        [LabelText("高")]
        public float sizeHeight = 1f;
        
        [HorizontalGroup("尺寸", Width = 0.34f)]
        [LabelText("深")]
        public float sizeDepth = 1f;
        
        #endregion

        #region VerticalGroup 垂直组
        
        [Title("垂直组")]
        [InfoBox("VerticalGroup 将字段垂直排列")]
        
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/左侧")]
        [BoxGroup("Split/左侧/玩家数据")]
        public string leftPlayerName = "玩家1";
        
        [BoxGroup("Split/左侧/玩家数据")]
        public int leftPlayerScore = 100;
        
        [BoxGroup("Split/左侧/玩家数据")]
        public int leftPlayerLevel = 5;
        
        [VerticalGroup("Split/右侧")]
        [BoxGroup("Split/右侧/敌人数据")]
        public string rightEnemyName = "敌人1";
        
        [BoxGroup("Split/右侧/敌人数据")]
        public int rightEnemyScore = 50;
        
        [BoxGroup("Split/右侧/敌人数据")]
        public int rightEnemyLevel = 3;
        
        #endregion

        #region TitleGroup 标题组
        
        [Title("标题组")]
        [InfoBox("TitleGroup 创建带标题的分组")]
        
        [TitleGroup("武器系统")]
        public string weaponName = "长剑";
        
        [TitleGroup("武器系统")]
        public int weaponDamage = 50;
        
        [TitleGroup("武器系统")]
        public float weaponSpeed = 1.2f;
        
        [TitleGroup("防具系统", "防御装备")]
        [InfoBox("可以添加副标题")]
        public string armorName = "铁甲";
        
        [TitleGroup("防具系统")]
        public int armorDefense = 30;
        
        [TitleGroup("防具系统")]
        public float armorWeight = 20f;
        
        #endregion

        #region ToggleGroup 切换组
        
        [Title("切换组")]
        [InfoBox("ToggleGroup 创建可开关的分组")]
        
        [ToggleGroup("EnableAI", "启用AI")]
        public bool EnableAI = true;
        
        [ToggleGroup("EnableAI")]
        public float aiUpdateInterval = 0.5f;
        
        [ToggleGroup("EnableAI")]
        public int aiDifficulty = 5;
        
        [ToggleGroup("EnableAI")]
        public bool aiUsePathfinding = true;
        
        [ToggleGroup("EnablePhysics", "启用物理")]
        public bool EnablePhysics = false;
        
        [ToggleGroup("EnablePhysics")]
        public float gravity = 9.8f;
        
        [ToggleGroup("EnablePhysics")]
        public float friction = 0.5f;
        
        [ToggleGroup("EnablePhysics")]
        public bool useCollision = true;
        
        #endregion

        #region 嵌套分组
        
        [Title("嵌套分组")]
        [InfoBox("分组可以嵌套使用")]
        
        [FoldoutGroup("游戏设置")]
        [BoxGroup("游戏设置/图形设置")]
        [HorizontalGroup("游戏设置/图形设置/分辨率")]
        public int screenWidth = 1920;
        
        [HorizontalGroup("游戏设置/图形设置/分辨率")]
        public int screenHeight = 1080;
        
        [BoxGroup("游戏设置/图形设置")]
        public bool fullscreen = true;
        
        [BoxGroup("游戏设置/图形设置")]
        [Range(0, 3)]
        public int qualityLevel = 2;
        
        [BoxGroup("游戏设置/音频设置")]
        [Range(0, 1)]
        public float masterVolume = 1f;
        
        [BoxGroup("游戏设置/音频设置")]
        [Range(0, 1)]
        public float musicVolume = 0.8f;
        
        [BoxGroup("游戏设置/音频设置")]
        [Range(0, 1)]
        public float sfxVolume = 1f;
        
        #endregion

        #region 复杂布局示例
        
        [Title("复杂布局示例")]
        
        [TabGroup("技能系统", "技能列表")]
        [TableList(AlwaysExpanded = true, ShowIndexLabels = true)]
        public List<SkillData> skills = new List<SkillData>
        {
            new SkillData { name = "火球术", damage = 50, manaCost = 20, cooldown = 5f },
            new SkillData { name = "冰冻", damage = 30, manaCost = 15, cooldown = 3f }
        };
        
        [TabGroup("技能系统", "技能树")]
        [BoxGroup("技能系统/技能树/火系")]
        public bool fireballUnlocked = true;
        
        [BoxGroup("技能系统/技能树/火系")]
        public bool meteorUnlocked = false;
        
        [BoxGroup("技能系统/技能树/冰系")]
        public bool frostboltUnlocked = true;
        
        [BoxGroup("技能系统/技能树/冰系")]
        public bool blizzardUnlocked = false;
        
        [TabGroup("技能系统", "技能效果")]
        [FoldoutGroup("技能系统/技能效果/粒子效果")]
        public GameObject fireEffect;
        
        [FoldoutGroup("技能系统/技能效果/粒子效果")]
        public GameObject iceEffect;
        
        [FoldoutGroup("技能系统/技能效果/音效")]
        public AudioClip fireSound;
        
        [FoldoutGroup("技能系统/技能效果/音效")]
        public AudioClip iceSound;
        
        #endregion

        #region 响应式布局示例
        
        [Title("响应式布局")]
        
        [ResponsiveButtonGroup("ResponsiveButtons")]
        [Button("按钮1", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void Button1() { Debug.Log("按钮1"); }
        
        [ResponsiveButtonGroup("ResponsiveButtons")]
        [Button("按钮2", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.5f, 0.8f)]
        private void Button2() { Debug.Log("按钮2"); }
        
        [ResponsiveButtonGroup("ResponsiveButtons")]
        [Button("按钮3", ButtonSizes.Large)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void Button3() { Debug.Log("按钮3"); }
        
        #endregion

        #region 测试按钮
        
        [Title("测试按钮")]
        
        [FoldoutGroup("测试操作")]
        [Button("打印所有技能", ButtonSizes.Medium)]
        private void PrintAllSkills()
        {
            Debug.Log($"共有 {skills.Count} 个技能：");
            foreach (var skill in skills)
            {
                Debug.Log($"- {skill.name}: 伤害{skill.damage}, 消耗{skill.manaCost}魔法, 冷却{skill.cooldown}秒");
            }
        }
        
        [FoldoutGroup("测试操作")]
        [Button("添加随机技能", ButtonSizes.Medium)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void AddRandomSkill()
        {
            string[] names = { "雷击", "治疗", "护盾", "闪电链", "群体治疗" };
            skills.Add(new SkillData
            {
                name = names[Random.Range(0, names.Length)],
                damage = Random.Range(20, 100),
                manaCost = Random.Range(10, 50),
                cooldown = Random.Range(1f, 10f)
            });
            Debug.Log("已添加随机技能");
        }
        
        [FoldoutGroup("测试操作")]
        [Button("清空技能列表", ButtonSizes.Medium)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ClearSkills()
        {
            skills.Clear();
            Debug.Log("技能列表已清空");
        }
        
        #endregion

        #region 内部类
        
        [System.Serializable]
        public class SkillData
        {
            [TableColumnWidth(100)]
            public string name;
            
            [TableColumnWidth(60)]
            public int damage;
            
            [TableColumnWidth(80)]
            public int manaCost;
            
            [TableColumnWidth(80)]
            public float cooldown;
        }
        
        #endregion
    }
}

