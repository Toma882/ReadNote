#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using UnityEngine;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;

#endif

    // 演示绘制器优先级的示例
    [TypeInfoBox(
        "在此示例中，我们有三个不同优先级的绘制器，都绘制相同的值。\n\n" +
        "目的是演示绘制器链，以及每个绘制器优先级的一般用途。")]
    public class PriorityExamples : MonoBehaviour
    {
        [ShowDrawerChain] // 显示参与绘制属性的所有绘制器
        public MyClass MyClass;
    }

    [Serializable]
    public class MyClass
    {
        public string Name;
        public float Value;
    }

#if UNITY_EDITOR

    // 此绘制器被配置为具有超级优先级。在这三个绘制器中，此类将首先被调用。
    // 在我们的示例中，超级绘制器会在值为空时实例化它。
    [DrawerPriority(1, 0, 0)]
    public class CUSTOM_SuperPriorityDrawer : OdinValueDrawer<MyClass>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 如果值尚未创建，则创建它
            if (this.ValueEntry.SmartValue == null)
            {
                this.ValueEntry.SmartValue = new MyClass();
            }

            this.CallNextDrawer(label);
        }
    }

    // 此绘制器被配置为具有包装器优先级，因此会被第二个调用。
    // 在此示例中，包装器绘制器在属性周围绘制一个框。
    [DrawerPriority(0, 1, 0)]
    public class CUSTOM_WrapperPriorityDrawer : OdinValueDrawer<MyClass>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 在属性周围绘制一个框
            SirenixEditorGUI.BeginBox(label);
            this.CallNextDrawer(null);
            SirenixEditorGUI.EndBox();
        }
    }

    // 此绘制器被配置为具有值优先级，因此最后被调用。
    // 在此示例中，值绘制器绘制PriorityClass对象的字段。
    [DrawerPriority(0, 0, 1)]
    public class CUSTOM_ValuePriorityDrawer : OdinValueDrawer<MyClass>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 绘制值字段
            this.ValueEntry.Property.Children["Name"].Draw();
            this.ValueEntry.Property.Children["Value"].Draw();
        }
    }

#endif
}
#endif
