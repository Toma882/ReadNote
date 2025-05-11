#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using UnityEngine;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using UnityEditor;

#endif

    // 演示如何通过绘制器创建新的泛型上下文菜单的示例组件
    [TypeInfoBox(
        "在此示例中，我们有一个特性绘制器，它向泛型上下文菜单添加了新选项。\n" +
        "在这种情况下，我们添加选择颜色的选项。")]
    public class GenericMenuExample : MonoBehaviour
    {
        [ColorPicker]
        public Color Color;
    }

    // 颜色选择器特性
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColorPickerAttribute : Attribute
    {
    }

#if UNITY_EDITOR

    public class ColorPickerAttributeDrawer : OdinAttributeDrawer<ColorPickerAttribute, Color>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 在这个例子中，我们不想手动绘制任何内容。
            // 所以我们调用下一个绘制器，让另一个绘制器为我们绘制实际的颜色字段。
            this.CallNextDrawer(label);
        }

        // IDefinesGenericMenuItems接口中定义的方法允许我们向上下文菜单添加自己的功能。
        // 每次打开上下文菜单时都会调用此函数，这允许你修改上下文菜单。
        public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            if (genericMenu.GetItemCount() > 0)
            {
                genericMenu.AddSeparator("");
            }

            genericMenu.AddItem(new GUIContent("颜色/红色"), false, () => this.SetColor(Color.red));
            genericMenu.AddItem(new GUIContent("颜色/绿色"), false, () => this.SetColor(Color.green));
            genericMenu.AddItem(new GUIContent("颜色/蓝色"), false, () => this.SetColor(Color.blue));
            genericMenu.AddItem(new GUIContent("颜色/黄色"), false, () => this.SetColor(Color.yellow));
            genericMenu.AddItem(new GUIContent("颜色/青色"), false, () => this.SetColor(Color.cyan));
            genericMenu.AddItem(new GUIContent("颜色/白色"), false, () => this.SetColor(Color.white));
            genericMenu.AddItem(new GUIContent("颜色/黑色"), false, () => this.SetColor(Color.black));
            genericMenu.AddDisabledItem(new GUIContent("颜色/洋红色"));
        }

        // 由上下文菜单调用的辅助函数
        private void SetColor(Color color)
        {
            this.ValueEntry.SmartValue = color;
            this.ValueEntry.ApplyChanges(); // ApplyChanges会在DrawPropertyLayout方法中自动调用，但在其他任何地方你都需要手动调用它。
        }
    }

#endif
}
#endif
