#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using System.Linq;
    using UnityEngine;

    // 
    // 这是所有物品的基类。它使用各种布局组属性包含了大量的布局设置。
    // 我们还在常量变量中定义了一些相关的组，派生类可以利用这些组。
    // 
    // 还要注意，每个继承自此类的物品都需要通过SupporteItemTypes属性指定支持哪些物品类型。
    // 这在Type字段上的ValueDropdown属性中被引用，这样用户就只能指定支持的物品类型。
    // 

    public abstract class Item : ScriptableObject
    {
        protected const string LEFT_VERTICAL_GROUP             = "Split/Left";
        protected const string STATS_BOX_GROUP                 = "Split/Left/Stats";
        protected const string GENERAL_SETTINGS_VERTICAL_GROUP = "Split/Left/General Settings/Split/Right";

        [HideLabel, PreviewField(55)]
        [VerticalGroup(LEFT_VERTICAL_GROUP)]
        [HorizontalGroup(LEFT_VERTICAL_GROUP + "/General Settings/Split", 55, LabelWidth = 67)]
        public Texture Icon;

        [BoxGroup(LEFT_VERTICAL_GROUP + "/General Settings")]
        [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
        [LabelText("名称")]
        public string Name;

        [BoxGroup("Split/Right/Description")]
        [HideLabel, TextArea(4, 14)]
        [LabelText("描述")]
        public string Description;

        [HorizontalGroup("Split", 0.5f, MarginLeft = 5, LabelWidth = 130)]
        [BoxGroup("Split/Right/Notes")]
        [HideLabel, TextArea(4, 9)]
        [LabelText("备注")]
        public string Notes;

        [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
        [ValueDropdown("SupportedItemTypes")]
        [ValidateInput("IsSupportedType")]
        [LabelText("类型")]
        public ItemTypes Type;

        [VerticalGroup("Split/Right")]
        [LabelText("需求")]
        public StatList Requirements;

        [AssetsOnly]
        [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
        [LabelText("预制件")]
        public GameObject Prefab;

        [BoxGroup(STATS_BOX_GROUP)]
        [LabelText("堆叠大小")]
        public int ItemStackSize = 1;

        [BoxGroup(STATS_BOX_GROUP)]
        [LabelText("稀有度")]
        public float ItemRarity;

        public abstract ItemTypes[] SupportedItemTypes { get; }

        private bool IsSupportedType(ItemTypes type)
        {
            return this.SupportedItemTypes.Contains(type);
        }
    }
}
#endif
