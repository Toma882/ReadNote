#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using UnityEngine;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using System.Reflection;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;

#endif

    // 演示如何使用反射增强自定义绘制器的示例。
    [TypeInfoBox(
            "这个示例演示了如何使用反射来扩展绘制器的功能，超出原本可能的范围。\n\n" +
            "在这种情况下，用户可以指定自己的一个方法来接收来自绘制器链的回调。\n\n" +
            "请注意，这是一种手动方法；建议使用ValueResolver<T>和ActionResolver代替。")]
    public class ReflectionExample : MonoBehaviour
    {
        [OnClickMethod("OnClick")]
        public int InstanceMethod;

        [OnClickMethod("StaticOnClick")]
        public int StaticMethod;

        [OnClickMethod("InvalidOnClick")]
        public int InvalidMethod;

        private void OnClick()
        {
            Debug.Log("你好？");
        }

        private static void StaticOnClick()
        {
            Debug.Log("静态你好？");
        }
    }

    // 带有回调方法名称的属性。
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OnClickMethodAttribute : Attribute
    {
        public string MethodName { get; private set; }

        public OnClickMethodAttribute(string methodName)
        {
            this.MethodName = methodName;
        }
    }

#if UNITY_EDITOR

    // 将绘制器脚本文件放在Editor文件夹中。
    // 记得为自定义绘制器类添加OdinDrawer，否则Odin将找不到它们。
    public class OnClickMethodAttributeDrawer : OdinAttributeDrawer<OnClickMethodAttribute>
    {
        // 这个字段用于向用户显示错误消息，如果出现问题的话。
        private string ErrorMessage;

        // 用户在属性中指定的方法的引用。
        private MethodInfo Method;

        protected override void Initialize()
        {
            // 使用反射查找指定的方法，并将方法信息存储在上下文对象中。
            this.Method = this.Property.ParentType.GetMethod(this.Attribute.MethodName, Flags.StaticInstanceAnyVisibility, null, Type.EmptyTypes, null);

            if (this.Method == null)
            {
                this.ErrorMessage = "无法在类型'" + this.Property.ParentType + "'中找到名为'" + this.Attribute.MethodName + "'的无参数方法。";
            }
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 显示可能发生的任何错误。
            if (this.ErrorMessage != null)
            {
                SirenixEditorGUI.ErrorMessageBox(this.ErrorMessage);

                // 继续正常绘制属性的其余部分。
                this.CallNextDrawer(label);
            }
            else
            {
                // 获取鼠标按下事件。
                bool clicked = Event.current.rawType == EventType.MouseDown && Event.current.button == 0 && this.Property.LastDrawnValueRect.Contains(Event.current.mousePosition);

                if (clicked)
                {
                    // 调用存储在上下文对象中的方法。
                    if (this.Method.IsStatic)
                    {
                        this.Method.Invoke(null, null);
                    }
                    else
                    {
                        this.Method.Invoke(this.Property.ParentValues[0], null);
                    }
                }

                // 绘制属性。
                this.CallNextDrawer(label);

                if (clicked)
                {
                    // 如果事件尚未被使用，则在此处使用它。
                    Event.current.Use();
                }
            }
        }
    }

#endif
}
#endif
