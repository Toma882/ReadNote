using System;

namespace Toma.OdinSamples.CompleteProject
{
    /// <summary>
    /// 角色职业枚举
    /// 定义游戏中可用的角色职业类型
    /// </summary>
    public enum CharacterClass
    {
        Warrior,    // 战士
        Mage,       // 法师
        Archer,     // 弓箭手
        Rogue,      // 刺客
        Priest,     // 牧师
        Paladin     // 圣骑士
    }

    /// <summary>
    /// 角色阵营枚举
    /// 定义角色的道德倾向
    /// </summary>
    public enum CharacterAlignment
    {
        Lawful_Good,    // 守序善良
        Neutral_Good,   // 中立善良
        Chaotic_Good,   // 混乱善良
        Lawful_Neutral, // 守序中立
        True_Neutral,   // 绝对中立
        Chaotic_Neutral,// 混乱中立
        Lawful_Evil,    // 守序邪恶
        Neutral_Evil,   // 中立邪恶
        Chaotic_Evil    // 混乱邪恶
    }

    /// <summary>
    /// 物品类型枚举
    /// 定义游戏中所有物品的分类
    /// </summary>
    public enum ItemType
    {
        Weapon,      // 武器
        Armor,       // 护甲
        Accessory,   // 饰品
        Consumable,  // 消耗品
        Material,    // 材料
        QuestItem    // 任务物品
    }

    /// <summary>
    /// 物品稀有度枚举
    /// 定义物品的稀有程度
    /// </summary>
    public enum ItemRarity
    {
        Common,      // 普通（白色）
        Uncommon,    // 非凡（绿色）
        Rare,        // 稀有（蓝色）
        Epic,        // 史诗（紫色）
        Legendary    // 传说（橙色）
    }

    /// <summary>
    /// 技能类型枚举
    /// 定义技能的分类
    /// </summary>
    public enum SkillType
    {
        Active,     // 主动技能
        Passive,    // 被动技能
        Ultimate    // 大招/终极技能
    }

    /// <summary>
    /// 技能目标类型枚举
    /// 定义技能可以作用的目标类型
    /// </summary>
    public enum SkillTargetType
    {
        Self,           // 自身
        SingleEnemy,    // 单个敌人
        AllEnemies,     // 所有敌人
        SingleAlly,     // 单个队友
        AllAllies,      // 所有队友
        Area            // 区域（AOE）
    }

    /// <summary>
    /// 元素类型枚举
    /// 定义游戏中的元素属性
    /// </summary>
    public enum ElementType
    {
        None,       // 无属性
        Fire,       // 火
        Water,      // 水
        Earth,      // 土
        Wind,       // 风
        Light,      // 光
        Dark,       // 暗
        Lightning   // 雷
    }

    /// <summary>
    /// 属性类型枚举
    /// 定义角色和物品的各种数值属性
    /// </summary>
    public enum StatType
    {
        // 基础属性
        Health,         // 生命值
        Mana,           // 魔法值
        Stamina,        // 耐力

        // 主要属性
        Strength,       // 力量
        Intelligence,   // 智力
        Dexterity,      // 敏捷
        Constitution,   // 体质
        Wisdom,         // 智慧
        Charisma,       // 魅力

        // 战斗属性
        Attack,         // 攻击力
        Defense,        // 防御力
        MagicAttack,    // 魔法攻击
        MagicDefense,   // 魔法防御
        CriticalRate,   // 暴击率
        CriticalDamage, // 暴击伤害
        Speed,          // 速度
        Accuracy,       // 命中率
        Evasion         // 闪避率
    }

    /// <summary>
    /// 属性值结构体
    /// 用于存储单个属性的类型和数值
    /// </summary>
    [Serializable]
    public struct StatValue
    {
        public StatType Type;   // 属性类型
        public float Value;     // 属性数值

        public StatValue(StatType type, float value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type}: {Value}";
        }
    }
}

