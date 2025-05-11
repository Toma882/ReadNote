#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using UnityEngine;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;

#endif

    // 演示如何在自定义绘制器中使用上下文对象的示例。
    [InfoBox("从Odin 2.0开始，所有绘制器现在都按每个属性实例化。这意味着以前的上下文系统现在不再必要，因为你可以直接在绘制器中创建字段。")]
    public class InstancedDrawerExample : MonoBehaviour
    {
        [InstancedDrawerExample]
        public int Field;
    }

    // InstancedDrawerExampleAttributeDrawer使用的属性。
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InstancedDrawerExampleAttribute : Attribute
    { }

#if UNITY_EDITOR

    // 将绘制器脚本文件放在Editor文件夹中。
    public class InstancedDrawerExampleAttributeDrawer : OdinAttributeDrawer<InstancedDrawerExampleAttribute>
    {
        private int counter;
        private bool counterEnabled;

        // 当绘制器首次实例化时调用新的Initialize方法。
        protected override void Initialize()
        {
            this.counter = 0;
            this.counterEnabled = false;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 计算帧数。
            if (Event.current.type == EventType.Layout && this.counterEnabled)
            {
                this.counter++;
                GUIHelper.RequestRepaint();
            }

            // 绘制当前帧计数，以及开始/停止按钮。
            SirenixEditorGUI.BeginBox();
            {
                GUILayout.Label("帧计数: " + this.counter);

                if (GUILayout.Button(this.counterEnabled ? "停止" : "开始"))
                {
                    this.counterEnabled = !this.counterEnabled;
                }
            }
            SirenixEditorGUI.EndBox();

            // 继续绘制器链。
            this.CallNextDrawer(label);
        }
    }

#endif
}
#endif
