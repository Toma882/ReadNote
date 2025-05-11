#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using UnityEngine;

    //
    // 我们没有添加 [CreateAssetMenu] 特性，而是使用Odin选择器创建了一个ScriptableObject创建器。
    // 这样角色就可以在RPG编辑器窗口中轻松创建，并确保它们被放置在正确的文件夹中。
    //
    // 通过继承SerializedScriptableObject，我们可以利用Odin提供的额外序列化能力。
    // 在这个例子中，Odin序列化了Inventory（一个二维数组）。其他内容由Unity序列化。
    // 

    public class Character : SerializedScriptableObject
    {
        [HorizontalGroup("Split", 55, LabelWidth = 70)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
        public Texture Icon;

        [VerticalGroup("Split/Meta")]
        [LabelText("姓名")]
        public string Name;

        [VerticalGroup("Split/Meta")]
        [LabelText("姓氏")]
        public string Surname;

        [VerticalGroup("Split/Meta"), Range(0, 100)]
        [LabelText("年龄")]
        public int Age;

        [HorizontalGroup("Split", 290), EnumToggleButtons, HideLabel]
        public CharacterAlignment CharacterAlignment;

        [TabGroup("起始物品栏", "Starting Inventory")]
        public ItemSlot[,] Inventory = new ItemSlot[12, 6];

        [TabGroup("起始属性", "Starting Stats"), HideLabel]
        public CharacterStats Skills = new CharacterStats();

        [HideLabel]
        [TabGroup("起始装备", "Starting Equipment")]
        public CharacterEquipment StartingEquipment;
    }
}
#endif
