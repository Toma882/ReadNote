using Sirenix.OdinInspector;
using UnityEngine;

namespace Toma.OdinSamples.CompleteProject
{
    /// <summary>
    /// 物品数据类
    /// 展示了如何设计物品系统的数据结构
    /// 
    /// 学习要点：
    /// 1. 使用复杂的分组布局
    /// 2. 使用 HideInInspector 隐藏不需要编辑的字段
    /// 3. 使用 OnValueChanged 响应数值变化
    /// 4. 使用 AssetSelector 选择资源
    /// 5. 使用条件特性实现动态UI
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Odin Samples/Complete Project/Item")]
    public class Item : ScriptableObject
    {
        #region 基本信息

        [Title("物品基本信息")]
        [HorizontalGroup("Header", 70)]
        [VerticalGroup("Header/Icon")]
        [HideLabel, PreviewField(70)]
        [Tooltip("物品图标")]
        public Texture2D Icon;

        [VerticalGroup("Header/Info")]
        [LabelText("物品名称")]
        [Required("物品名称不能为空")]
        public string ItemName = "新物品";

        [VerticalGroup("Header/Info")]
        [LabelText("物品类型")]
        [EnumToggleButtons]
        [OnValueChanged("OnItemTypeChanged")]
        public ItemType Type = ItemType.Consumable;

        [VerticalGroup("Header/Info")]
        [LabelText("稀有度")]
        [EnumPaging]
        [OnValueChanged("UpdateRarityColor")]
        public ItemRarity Rarity = ItemRarity.Common;

        [HideInInspector]
        public Color RarityColor = Color.white;

        #endregion

        #region 物品描述

        [Space(10)]
        [BoxGroup("描述")]
        [LabelText("物品描述")]
        [TextArea(3, 6)]
        [InfoBox("物品的基本描述")]
        public string Description = "在这里输入物品描述...";

        [BoxGroup("描述")]
        [LabelText("使用效果说明")]
        [TextArea(2, 4)]
        [ShowIf("@Type == ItemType.Consumable")]
        public string UseDescription = "描述使用此物品的效果...";

        [BoxGroup("描述")]
        [LabelText("装备效果说明")]
        [TextArea(2, 4)]
        [ShowIf("@Type == ItemType.Weapon || Type == ItemType.Armor || Type == ItemType.Accessory")]
        public string EquipDescription = "描述装备此物品的效果...";

        #endregion

        #region 物品属性

        [Space(10)]
        [TabGroup("属性", "基础")]
        [LabelText("堆叠上限")]
        [MinValue(1)]
        [InfoBox("同一格子可以叠加的最大数量")]
        public int MaxStackSize = 1;

        [TabGroup("属性", "基础")]
        [LabelText("物品重量")]
        [MinValue(0)]
        [SuffixLabel("kg", overlay: true)]
        public float Weight = 0.1f;

        [TabGroup("属性", "基础")]
        [LabelText("价格")]
        [MinValue(0)]
        [SuffixLabel("金币", overlay: true)]
        [OnValueChanged("CalculateSellPrice")]
        public int Price = 100;

        [TabGroup("属性", "基础")]
        [LabelText("出售价格")]
        [ReadOnly]
        [ShowInInspector]
        [SuffixLabel("金币", overlay: true)]
        public int SellPrice => Mathf.RoundToInt(Price * 0.5f);

        [TabGroup("属性", "基础")]
        [LabelText("等级需求")]
        [Range(1, 100)]
        public int RequiredLevel = 1;

        [TabGroup("属性", "基础")]
        [LabelText("可交易")]
        [ToggleLeft]
        public bool IsTradeable = true;

        [TabGroup("属性", "基础")]
        [LabelText("可掉落")]
        [ToggleLeft]
        public bool IsDroppable = true;

        #endregion

        #region 装备属性（条件显示）

        [TabGroup("属性", "装备")]
        [ShowIf("@Type == ItemType.Weapon || Type == ItemType.Armor || Type == ItemType.Accessory")]
        [LabelText("装备属性加成")]
        [InfoBox("装备此物品获得的属性加成")]
        public StatList EquipmentStats = new StatList();

        [TabGroup("属性", "装备")]
        [ShowIf("@Type == ItemType.Weapon")]
        [LabelText("武器类型")]
        [EnumToggleButtons]
        public WeaponType WeaponType = WeaponType.Sword;

        [TabGroup("属性", "装备")]
        [ShowIf("@Type == ItemType.Weapon")]
        [LabelText("攻击距离")]
        [MinValue(0)]
        [SuffixLabel("米", overlay: true)]
        public float AttackRange = 1.5f;

        [TabGroup("属性", "装备")]
        [ShowIf("@Type == ItemType.Weapon")]
        [LabelText("攻击速度")]
        [Range(0.1f, 3f)]
        public float AttackSpeed = 1f;

        [TabGroup("属性", "装备")]
        [ShowIf("@Type == ItemType.Armor")]
        [LabelText("护甲部位")]
        [EnumToggleButtons]
        public ArmorSlot ArmorSlot = ArmorSlot.Chest;

        #endregion

        #region 消耗品属性（条件显示）

        [TabGroup("属性", "消耗")]
        [ShowIf("@Type == ItemType.Consumable")]
        [LabelText("使用效果")]
        [InfoBox("使用此物品产生的效果")]
        public ConsumableEffect[] Effects = new ConsumableEffect[0];

        [TabGroup("属性", "消耗")]
        [ShowIf("@Type == ItemType.Consumable")]
        [LabelText("冷却时间")]
        [MinValue(0)]
        [SuffixLabel("秒", overlay: true)]
        public float UseCooldown = 1f;

        [TabGroup("属性", "消耗")]
        [ShowIf("@Type == ItemType.Consumable")]
        [LabelText("可在战斗中使用")]
        [ToggleLeft]
        public bool UsableInCombat = true;

        #endregion

        #region 高级设置

        [Space(10)]
        [FoldoutGroup("高级设置")]
        [LabelText("物品模型")]
        [AssetsOnly]
        [InfoBox("3D 模型预制体")]
        public GameObject Prefab;

        [FoldoutGroup("高级设置")]
        [LabelText("拾取音效")]
        [AssetsOnly]
        public AudioClip PickupSound;

        [FoldoutGroup("高级设置")]
        [LabelText("使用音效")]
        [AssetsOnly]
        public AudioClip UseSound;

        [FoldoutGroup("高级设置")]
        [LabelText("特殊标签")]
        [InfoBox("物品的自定义标签（例如：Quest, Unique, Stackable）")]
        public string[] Tags = new string[0];

        #endregion

        #region 编辑器功能

#if UNITY_EDITOR

        [Title("编辑器功能")]
        [PropertySpace(SpaceBefore = 10)]

        [ShowInInspector, ReadOnly, DisplayAsString]
        [LabelText("稀有度颜色预览")]
        [GUIColor("@RarityColor")]
        private string RarityColorPreview => $"■ {Rarity}";

        [Button("根据稀有度自动定价", ButtonSizes.Medium)]
        [HorizontalGroup("ItemActions")]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void AutoPrice()
        {
            Price = Rarity switch
            {
                ItemRarity.Common => 10,
                ItemRarity.Uncommon => 50,
                ItemRarity.Rare => 200,
                ItemRarity.Epic => 500,
                ItemRarity.Legendary => 2000,
                _ => 10
            };
            Debug.Log($"{ItemName} 价格已设置为 {Price} 金币");
        }

        [Button("复制为新物品", ButtonSizes.Medium)]
        [HorizontalGroup("ItemActions")]
        [GUIColor(0.4f, 0.8f, 0.8f)]
        private void DuplicateItem()
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            string newPath = path.Replace(".asset", "_Copy.asset");
            UnityEditor.AssetDatabase.CopyAsset(path, newPath);
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log($"已创建物品副本：{newPath}");
        }

#endif

        #endregion

        #region 辅助方法

        private void OnItemTypeChanged()
        {
            // 类型变化时重置相关属性
            if (Type == ItemType.Consumable)
            {
                MaxStackSize = 99;
            }
            else if (Type == ItemType.Weapon || Type == ItemType.Armor || Type == ItemType.Accessory)
            {
                MaxStackSize = 1;
            }
        }

        private void UpdateRarityColor()
        {
            RarityColor = Rarity switch
            {
                ItemRarity.Common => Color.white,
                ItemRarity.Uncommon => Color.green,
                ItemRarity.Rare => Color.blue,
                ItemRarity.Epic => new Color(0.6f, 0.2f, 0.8f), // 紫色
                ItemRarity.Legendary => new Color(1f, 0.5f, 0f), // 橙色
                _ => Color.white
            };
        }

        private void CalculateSellPrice()
        {
            // 价格变化时自动计算出售价格（已通过属性实现）
        }

        private void OnValidate()
        {
            UpdateRarityColor();
        }

        #endregion
    }

    /// <summary>
    /// 武器类型枚举
    /// </summary>
    public enum WeaponType
    {
        Sword,      // 剑
        Axe,        // 斧
        Mace,       // 锤
        Dagger,     // 匕首
        Bow,        // 弓
        Staff,      // 法杖
        Wand        // 魔杖
    }

    /// <summary>
    /// 护甲部位枚举
    /// </summary>
    public enum ArmorSlot
    {
        Head,       // 头部
        Chest,      // 胸部
        Hands,      // 手部
        Legs,       // 腿部
        Feet        // 脚部
    }

    /// <summary>
    /// 消耗品效果结构体
    /// </summary>
    [System.Serializable]
    public struct ConsumableEffect
    {
        [LabelText("效果类型")]
        [EnumToggleButtons]
        public ConsumableEffectType Type;

        [LabelText("效果值")]
        [MinValue(0)]
        public float Value;

        [LabelText("持续时间")]
        [MinValue(0)]
        [SuffixLabel("秒", overlay: true)]
        [ShowIf("@Type == ConsumableEffectType.BuffHealth || Type == ConsumableEffectType.BuffMana")]
        public float Duration;
    }

    /// <summary>
    /// 消耗品效果类型枚举
    /// </summary>
    public enum ConsumableEffectType
    {
        RestoreHealth,  // 恢复生命
        RestoreMana,    // 恢复魔法
        BuffHealth,     // 增益生命上限
        BuffMana,       // 增益魔法上限
        BuffAttack,     // 增益攻击
        BuffDefense     // 增益防御
    }
}

