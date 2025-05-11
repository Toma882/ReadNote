#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using Sirenix.OdinInspector.Editor;
    using Sirenix.OdinInspector.Editor.ActionResolvers;
    using Sirenix.OdinInspector.Editor.ValueResolvers;
    using Sirenix.Utilities.Editor;
    using System;
    using UnityEngine;

#if UNITY_EDITOR

#endif
    // 演示如何使用反射增强自定义绘制器的示例。
    [TypeInfoBox(
            "此示例演示了如何使用解析字符串来扩展绘制器的行为。\n\n" +
            "在这种情况下，用户可以使用解析字符串将值传递给绘制器，或指定要从绘制器调用的操作。请注意相比反射示例，这需要的绘制器代码是多么少。\n\n" +
            "解析字符串可以是硬编码的（如果解析类型是字符串），也可以是成员引用或表达式，并且是全局可扩展的，因此用户可以添加自己的字符串解析行为。")]
    public class ValueAndActionResolversExample : MonoBehaviour
    {
        [Title("操作解析器")]
        [OnClickAction("OnClick")]
        public int MethodAction;

        [OnClickAction("@UnityEngine.Debug.Log(DateTime.Now.ToString())")]
        public int ExpressionAction;

        [OnClickAction("Invalid Action String"), InfoBox("以下显示了如果向操作解析器传递无效的解析字符串时会得到的错误消息示例：")]
        public int InvalidActionString;

        [Title("值解析器")]
        [DisplayValueAsString("$MemberReferenceValue")]
        public int MemberReferenceValue = 1337;

        [DisplayValueAsString("@Mathf.Sin(Time.realtimeSinceStartup)")]
        public int ExpressionValue;

        [DisplayValueAsString("Invalid Value String"), InfoBox("以下显示了如果向值解析器传递无效的解析字符串时会得到的错误消息示例：")]
        public int InvalidValueString;

        private void OnClick()
        {
            Debug.Log("点击 - 这可以是静态或实例方法，代码仍然可以工作");
        }

#if UNITY_EDITOR
        [OnInspectorGUI]
        private void RepaintConstantly() { GUIHelper.RequestRepaint(); }
#endif
    }

    // 带有解析操作字符串的属性。
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class OnClickActionAttribute : Attribute
    {
        public string ActionString { get; private set; }

        public OnClickActionAttribute(string actionString)
        {
            this.ActionString = actionString;
        }
    }

    // 带有解析值字符串的属性。
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class DisplayValueAsStringAttribute : Attribute
    {
        public string ValueString { get; private set; }

        public DisplayValueAsStringAttribute(string valueString)
        {
            this.ValueString = valueString;
        }
    }


#if UNITY_EDITOR
    public class OnClickActionAttributeDrawer : OdinAttributeDrawer<OnClickActionAttribute>
    {
        private ActionResolver action;

        protected override void Initialize()
        {
            this.action = ActionResolver.Get(this.Property, this.Attribute.ActionString);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (this.action.HasError)
            {
                this.action.DrawError();
            }

            if (GUILayout.Button("执行操作 '" + this.Attribute.ActionString + "'"))
            {
                // 如果有错误，则什么也不做
                this.action.DoActionForAllSelectionIndices();
            }

            this.CallNextDrawer(label);
        }
    }

    public class DisplayValueAsStringAttributeDrawer : OdinAttributeDrawer<DisplayValueAsStringAttribute>
    {
        private ValueResolver<object> valueResolver;

        protected override void Initialize()
        {
            this.valueResolver = ValueResolver.Get<object>(this.Property, this.Attribute.ValueString);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (this.valueResolver.HasError)
            {
                this.valueResolver.DrawError();
            }
            else
            {
                var value = this.valueResolver.GetValue();
                string valueStr = value == null ? "空" : value.ToString();
                GUILayout.Label("'" + this.Attribute.ValueString + "'的值: " + valueStr);
            }

            this.CallNextDrawer(label);
        }
    }
#endif
}
#endif
