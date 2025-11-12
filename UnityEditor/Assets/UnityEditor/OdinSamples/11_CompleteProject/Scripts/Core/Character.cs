using Sirenix.OdinInspector;
using UnityEngine;

namespace Toma.OdinSamples.CompleteProject
{
    /// <summary>
    /// 角色数据类
    /// 展示了复杂数据结构的组织和布局
    /// 
    /// 学习要点：
    /// 1. 使用 HorizontalGroup/VerticalGroup 组织复杂布局
    /// 2. 使用 TabGroup 分类不同类型的数据
    /// 3. 使用 PreviewField 显示预览图
    /// 4. 使用 EnumToggleButtons 美化枚举选择
    /// 5. 使用 SerializedScriptableObject 序列化复杂类型
    /// </summary>
    [CreateAssetMenu(fileName = "New Character", menuName = "Odin Samples/Complete Project/Character")]
    public class Character : SerializedScriptableObject
    {
        #region 基本信息（使用水平分组布局）

        [Title("角色基本信息")]
        [HorizontalGroup("Split", 70, LabelWidth = 70)]
        [VerticalGroup("Split/Icon")]
        [HideLabel, PreviewField(70, ObjectFieldAlignment.Left)]
        [Tooltip("角色头像图标")]
        public Texture2D Icon;

        [VerticalGroup("Split/Basic")]
        [LabelText("角色名称")]
        [Required("角色名称不能为空")]
        public string CharacterName = "新角色";

        [VerticalGroup("Split/Basic")]
        [LabelText("角色称号")]
        [InfoBox("可选的角色称号，如'传奇剑士'、'黑暗法师'等")]
        public string Title;

        [VerticalGroup("Split/Basic")]
        [LabelText("等级"), Range(1, 100)]
        [OnValueChanged("OnLevelChanged")]
        public int Level = 1;

        [VerticalGroup("Split/Basic")]
        [LabelText("经验值"), ProgressBar(0, "MaxExperience", ColorGetter = "GetExpColor")]
        [ShowInInspector, ReadOnly]
        public int Experience = 0;

        [HideInInspector]
        public int MaxExperience => Level * 100; // 每级需要 Level * 100 经验

        private Color GetExpColor(int value)
        {
            float ratio = (float)value / MaxExperience;
            return Color.Lerp(Color.red, Color.green, ratio);
        }

        #endregion

        #region 职业和阵营（使用枚举按钮美化）

        [HorizontalGroup("Class", LabelWidth = 70)]
        [VerticalGroup("Class/Left")]
        [LabelText("职业")]
        [EnumToggleButtons]
        [InfoBox("选择角色的职业类型")]
        public CharacterClass Class = CharacterClass.Warrior;

        [VerticalGroup("Class/Right")]
        [LabelText("阵营")]
        [EnumToggleButtons]
        [InfoBox("选择角色的道德倾向")]
        public CharacterAlignment Alignment = CharacterAlignment.True_Neutral;

        #endregion

        #region 详细信息（使用 TabGroup 分类显示）

        [Space(10)]

        [TabGroup("详情", "属性")]
        [LabelText("角色属性")]
        [InlineProperty, HideLabel]
        public StatList Stats = new StatList();

        [TabGroup("详情", "技能")]
        [LabelText("已学技能")]
        [ListDrawerSettings(
            DraggableItems = true,
            ShowItemCount = true,
            CustomAddFunction = "AddSkillSlot")]
        [InfoBox("将技能资源拖放到这里，或点击 + 按钮添加")]
        public Skill[] KnownSkills = new Skill[0];

        [TabGroup("详情", "物品栏")]
        [LabelText("背包物品")]
        [TableList(ShowIndexLabels = true, AlwaysExpanded = false)]
        [InfoBox("角色的物品栏，最多可携带 20 个物品")]
        public ItemSlot[] Inventory = new ItemSlot[20];

        [TabGroup("详情", "装备")]
        [LabelText("当前装备")]
        [InlineProperty, HideLabel]
        public CharacterEquipment Equipment = new CharacterEquipment();

        [TabGroup("详情", "描述")]
        [LabelText("角色描述")]
        [TextArea(5, 10)]
        [InfoBox("角色的背景故事和详细描述")]
        public string Description = "在这里输入角色的背景故事...";

        #endregion

        #region 编辑器功能按钮

#if UNITY_EDITOR

        [Title("编辑器功能")]
        [PropertySpace(SpaceBefore = 10)]

        [Button("升级", ButtonSizes.Medium)]
        [HorizontalGroup("Actions")]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void LevelUp()
        {
            Level++;
            Experience = 0;
            Debug.Log($"{CharacterName} 升级到 {Level} 级！");
        }

        [Button("添加经验", ButtonSizes.Medium)]
        [HorizontalGroup("Actions")]
        [GUIColor(0.3f, 0.6f, 0.9f)]
        private void AddExperience()
        {
            Experience += 50;
            if (Experience >= MaxExperience)
            {
                LevelUp();
            }
            else
            {
                Debug.Log($"{CharacterName} 获得 50 经验，当前经验: {Experience}/{MaxExperience}");
            }
        }

        [Button("重置角色", ButtonSizes.Medium)]
        [HorizontalGroup("Actions")]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ResetCharacter()
        {
            if (UnityEditor.EditorUtility.DisplayDialog(
                "确认重置",
                $"确定要重置角色 {CharacterName} 吗？这将清空所有数据！",
                "确定",
                "取消"))
            {
                Level = 1;
                Experience = 0;
                Stats.Clear();
                KnownSkills = new Skill[0];
                Inventory = new ItemSlot[20];
                Debug.Log($"已重置角色 {CharacterName}");
            }
        }

        [Button("查看详细信息", ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 0.8f)]
        private void ShowDetails()
        {
            string details = $"=== 角色详细信息 ===\n" +
                           $"名称: {CharacterName}\n" +
                           $"称号: {Title}\n" +
                           $"等级: {Level}\n" +
                           $"经验: {Experience}/{MaxExperience}\n" +
                           $"职业: {Class}\n" +
                           $"阵营: {Alignment}\n" +
                           $"已学技能数: {KnownSkills.Length}\n" +
                           $"属性数量: {Stats.Count}\n";
            Debug.Log(details);
        }

#endif

        #endregion

        #region 辅助方法

        /// <summary>
        /// 等级变化时的回调
        /// </summary>
        private void OnLevelChanged()
        {
            // 确保经验值不超过最大值
            if (Experience > MaxExperience)
            {
                Experience = MaxExperience;
            }
        }

        /// <summary>
        /// 添加技能槽的自定义函数
        /// </summary>
        private Skill AddSkillSlot()
        {
            return null; // 返回 null，用户可以手动拖放技能
        }

        #endregion
    }

    /// <summary>
    /// 物品槽结构体
    /// 用于存储物品栏中的物品和数量
    /// </summary>
    [System.Serializable]
    public struct ItemSlot
    {
        [LabelText("物品")]
        [HorizontalGroup("Slot", Width = 200)]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        public Item Item;

        [LabelText("数量")]
        [HorizontalGroup("Slot")]
        [MinValue(0)]
        public int Quantity;

        public bool IsEmpty => Item == null || Quantity <= 0;

        public ItemSlot(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }

    /// <summary>
    /// 角色装备结构体
    /// 用于管理角色当前装备的物品
    /// </summary>
    [System.Serializable]
    public struct CharacterEquipment
    {
        [Title("武器")]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("主手武器")]
        public Item MainHandWeapon;

        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("副手武器")]
        public Item OffHandWeapon;

        [Title("防具")]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("头盔")]
        public Item Helmet;

        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("胸甲")]
        public Item ChestArmor;

        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("手套")]
        public Item Gloves;

        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("裤子")]
        public Item Pants;

        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("靴子")]
        public Item Boots;

        [Title("饰品")]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("项链")]
        public Item Necklace;

        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("戒指1")]
        public Item Ring1;

        [PreviewField(50, ObjectFieldAlignment.Left)]
        [LabelText("戒指2")]
        public Item Ring2;
    }
}

