using Sirenix.OdinInspector;
using UnityEngine;

namespace Toma.OdinSamples.CompleteProject
{
    /// <summary>
    /// 技能数据类
    /// 展示了如何设计技能系统的数据结构
    /// 
    /// 学习要点：
    /// 1. 使用 BoxGroup 创建视觉上分隔的区块
    /// 2. 使用 ShowIf 实现条件显示
    /// 3. 使用 ColorPalette 选择颜色
    /// 4. 使用 MinMaxSlider 设置范围值
    /// 5. 使用 ProgressBar 可视化数值
    /// </summary>
    [CreateAssetMenu(fileName = "New Skill", menuName = "Odin Samples/Complete Project/Skill")]
    public class Skill : ScriptableObject
    {
        #region 基本信息

        [Title("技能基本信息")]
        [HorizontalGroup("Header", 70)]
        [VerticalGroup("Header/Icon")]
        [HideLabel, PreviewField(70)]
        [Tooltip("技能图标")]
        public Texture2D Icon;

        [VerticalGroup("Header/Info")]
        [LabelText("技能名称")]
        [Required("技能名称不能为空")]
        public string SkillName = "新技能";

        [VerticalGroup("Header/Info")]
        [LabelText("技能类型")]
        [EnumToggleButtons]
        public SkillType Type = SkillType.Active;

        [VerticalGroup("Header/Info")]
        [LabelText("元素属性")]
        [EnumPaging]
        public ElementType Element = ElementType.None;

        #endregion

        #region 技能描述

        [Space(10)]
        [BoxGroup("描述")]
        [LabelText("技能描述")]
        [TextArea(3, 6)]
        [InfoBox("简要描述技能的效果和用途")]
        public string Description = "在这里输入技能描述...";

        [BoxGroup("描述")]
        [LabelText("技能效果说明")]
        [TextArea(2, 4)]
        public string EffectDescription = "详细的技能效果说明...";

        #endregion

        #region 技能属性

        [Space(10)]
        [FoldoutGroup("技能属性", expanded: true)]
        [LabelText("等级需求"), Range(1, 100)]
        [InfoBox("学习此技能所需的角色等级")]
        public int RequiredLevel = 1;

        [FoldoutGroup("技能属性")]
        [LabelText("技能等级"), Range(1, 10)]
        [OnValueChanged("OnSkillLevelChanged")]
        public int SkillLevel = 1;

        [FoldoutGroup("技能属性")]
        [LabelText("最大等级"), Range(1, 10)]
        [MinValue("@SkillLevel")]
        public int MaxSkillLevel = 10;

        [FoldoutGroup("技能属性")]
        [LabelText("目标类型")]
        [EnumToggleButtons]
        public SkillTargetType TargetType = SkillTargetType.SingleEnemy;

        #endregion

        #region 消耗和冷却

        [Space(10)]
        [TabGroup("数值", "消耗")]
        [LabelText("魔法消耗")]
        [MinValue(0)]
        [SuffixLabel("MP", overlay: true)]
        [InfoBox("使用技能消耗的魔法值")]
        public int ManaCost = 10;

        [TabGroup("数值", "消耗")]
        [LabelText("生命消耗")]
        [MinValue(0)]
        [SuffixLabel("HP", overlay: true)]
        [ShowIf("@Type == SkillType.Active")]
        [InfoBox("某些强力技能需要消耗生命值")]
        public int HealthCost = 0;

        [TabGroup("数值", "消耗")]
        [LabelText("耐力消耗")]
        [MinValue(0)]
        [SuffixLabel("Stamina", overlay: true)]
        public int StaminaCost = 5;

        [TabGroup("数值", "冷却")]
        [LabelText("冷却时间")]
        [MinValue(0)]
        [SuffixLabel("秒", overlay: true)]
        [ShowIf("@Type != SkillType.Passive")]
        [InfoBox("技能使用后的冷却时间")]
        public float Cooldown = 5f;

        [TabGroup("数值", "冷却")]
        [LabelText("施法时间")]
        [MinValue(0)]
        [SuffixLabel("秒", overlay: true)]
        [ShowIf("@Type == SkillType.Active")]
        public float CastTime = 1f;

        [TabGroup("数值", "冷却")]
        [LabelText("持续时间")]
        [MinValue(0)]
        [SuffixLabel("秒", overlay: true)]
        [ShowIf("@Type == SkillType.Passive")]
        [InfoBox("被动技能或持续效果的时长，0 表示永久")]
        public float Duration = 0f;

        #endregion

        #region 伤害/治疗数值

        [TabGroup("数值", "效果")]
        [LabelText("伤害/治疗值")]
        [MinMaxSlider(0, 1000, showFields: true)]
        [InfoBox("技能造成的伤害或治疗量范围")]
        public Vector2 DamageRange = new Vector2(10, 20);

        [TabGroup("数值", "效果")]
        [LabelText("伤害类型")]
        [EnumToggleButtons]
        public DamageType DamageType = DamageType.Physical;

        [TabGroup("数值", "效果")]
        [LabelText("暴击率加成")]
        [Range(0f, 100f)]
        [SuffixLabel("%", overlay: true)]
        [ProgressBar(0, 100, ColorGetter = "GetCritRateColor")]
        public float CriticalRateBonus = 0f;

        [TabGroup("数值", "效果")]
        [LabelText("范围/半径")]
        [MinValue(0)]
        [SuffixLabel("米", overlay: true)]
        [ShowIf("@TargetType == SkillTargetType.Area")]
        public float EffectRadius = 5f;

        #endregion

        #region 技能效果（高级）

        [Space(10)]
        [FoldoutGroup("高级设置")]
        [LabelText("附加状态效果")]
        [InfoBox("技能命中后可能附加的状态效果")]
        public StatusEffect[] StatusEffects = new StatusEffect[0];

        [FoldoutGroup("高级设置")]
        [LabelText("需求属性")]
        [InfoBox("使用此技能需要的属性要求")]
        public StatList Requirements = new StatList();

        [FoldoutGroup("高级设置")]
        [LabelText("特效预制体")]
        [AssetsOnly]
        public GameObject EffectPrefab;

        [FoldoutGroup("高级设置")]
        [LabelText("技能音效")]
        [AssetsOnly]
        public AudioClip SoundEffect;

        [FoldoutGroup("高级设置")]
        [LabelText("技能颜色")]
        [ColorPalette("技能颜色主题")]
        public Color SkillColor = Color.white;

        #endregion

        #region 编辑器功能

#if UNITY_EDITOR

        [Title("编辑器功能")]
        [PropertySpace(SpaceBefore = 10)]

        [Button("升级技能", ButtonSizes.Medium)]
        [HorizontalGroup("SkillActions")]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        [DisableIf("@SkillLevel >= MaxSkillLevel")]
        private void LevelUpSkill()
        {
            if (SkillLevel < MaxSkillLevel)
            {
                SkillLevel++;
                // 升级后自动提升数值
                DamageRange *= 1.1f;
                ManaCost = Mathf.RoundToInt(ManaCost * 1.05f);
                Debug.Log($"{SkillName} 升级到 {SkillLevel} 级！");
            }
        }

        [Button("重置技能", ButtonSizes.Medium)]
        [HorizontalGroup("SkillActions")]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ResetSkill()
        {
            SkillLevel = 1;
            Debug.Log($"{SkillName} 已重置到 1 级");
        }

        [Button("复制为新技能", ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 0.8f)]
        private void DuplicateSkill()
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            string newPath = path.Replace(".asset", "_Copy.asset");
            UnityEditor.AssetDatabase.CopyAsset(path, newPath);
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log($"已创建技能副本：{newPath}");
        }

#endif

        #endregion

        #region 辅助方法

        private void OnSkillLevelChanged()
        {
            if (SkillLevel > MaxSkillLevel)
            {
                SkillLevel = MaxSkillLevel;
            }
        }

        private Color GetCritRateColor(float value)
        {
            return Color.Lerp(Color.gray, Color.yellow, value / 100f);
        }

        #endregion
    }

    /// <summary>
    /// 伤害类型枚举
    /// </summary>
    public enum DamageType
    {
        Physical,   // 物理伤害
        Magical,    // 魔法伤害
        True        // 真实伤害（无视防御）
    }

    /// <summary>
    /// 状态效果结构体
    /// 定义技能附加的状态效果
    /// </summary>
    [System.Serializable]
    public struct StatusEffect
    {
        [LabelText("效果类型")]
        [EnumToggleButtons]
        public StatusEffectType Type;

        [LabelText("持续时间")]
        [SuffixLabel("秒", overlay: true)]
        public float Duration;

        [LabelText("效果强度")]
        public float Intensity;

        [LabelText("触发概率")]
        [Range(0f, 100f)]
        [SuffixLabel("%", overlay: true)]
        public float Chance;
    }

    /// <summary>
    /// 状态效果类型枚举
    /// </summary>
    public enum StatusEffectType
    {
        Stun,       // 眩晕
        Poison,     // 中毒
        Burn,       // 燃烧
        Freeze,     // 冰冻
        Slow,       // 减速
        Buff,       // 增益
        Debuff,     // 减益
        Heal        // 治疗
    }
}

