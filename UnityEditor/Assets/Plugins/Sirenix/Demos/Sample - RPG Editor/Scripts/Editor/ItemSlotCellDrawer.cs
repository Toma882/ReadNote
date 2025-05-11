#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using UnityEngine;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.OdinInspector.Editor.Drawers;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;
    using UnityEditor;

    // 
    // 在Character.cs中，我们有一个二维的ItemSlots数组，这是我们的物品栏。
    // 而不是使用TableMatrix属性在那里自定义它，我们在这种情况下
    // 创建一个自定义绘制器，它将适用于所有二维ItemSlot数组，
    // 这样我们就不必通过TableMatrix属性一遍又一遍地制作相同的自定义绘制器。
    // 

    internal sealed class ItemSlotCellDrawer<TArray> : TwoDimensionalArrayDrawer<TArray, ItemSlot>
        where TArray : System.Collections.IList
    {
        protected override TableMatrixAttribute GetDefaultTableMatrixAttributeSettings()
        {
            return new TableMatrixAttribute()
            {
                SquareCells = true,
                HideColumnIndices = true,
                HideRowIndices = true,
                ResizableColumns = false
            };
        }

        protected override ItemSlot DrawElement(Rect rect, ItemSlot value)
        {
            var id = DragAndDropUtilities.GetDragAndDropId(rect);
            DragAndDropUtilities.DrawDropZone(rect, value.Item ? value.Item.Icon : null, null, id); // 使用物品图标绘制放置区域。

            if (value.Item != null)
            {
                // 物品数量
                var countRect = rect.Padding(2).AlignBottom(16);
                value.ItemCount = EditorGUI.IntField(countRect, Mathf.Max(1, value.ItemCount));
                GUI.Label(countRect, "/ " + value.Item.ItemStackSize, SirenixGUIStyles.RightAlignedGreyMiniLabel);
            }

            value = DragAndDropUtilities.DropZone(rect, value);                                     // 用于ItemSlot结构的放置区域。
            value.Item = DragAndDropUtilities.DropZone<Item>(rect, value.Item);                     // 用于Item类型的放置区域。
            value = DragAndDropUtilities.DragZone(rect, value, true, true);                         // 启用ItemSlot的拖拽功能

            return value;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            base.DrawPropertyLayout(label);

            // 绘制一个可以销毁物品的放置区域。
            var rect = GUILayoutUtility.GetRect(0, 40).Padding(2);
            var id = DragAndDropUtilities.GetDragAndDropId(rect);
            DragAndDropUtilities.DrawDropZone(rect, null as UnityEngine.Object, null, id);
            DragAndDropUtilities.DropZone<ItemSlot>(rect, new ItemSlot(), false, id);
        }
    }

}
#endif
