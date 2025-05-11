#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using UnityEngine;
    using System;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using Sirenix.Utilities;

#endif

    // 演示如何为自定义类型创建自定义绘制器
    [TypeInfoBox("此示例演示了如何为自定义结构体或类实现自定义绘制器。")]
    public class CustomDrawerExample : MonoBehaviour
    {
        public MyStruct MyStruct;
    }

    // 自定义数据结构，用于演示
    [Serializable]
    public struct MyStruct
    {
        public float X;
        public float Y;
    }

#if UNITY_EDITOR

    public class CustomStructDrawer : OdinValueDrawer<MyStruct>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            MyStruct value = this.ValueEntry.SmartValue;

            var rect = EditorGUILayout.GetControlRect();

            // 在Odin中，标签是可选的，可能为空，所以我们必须考虑到这一点
            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            var prev = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 20;

            value.X = EditorGUI.Slider(rect.AlignLeft(rect.width * 0.5f), "X", value.X, 0, 1);
            value.Y = EditorGUI.Slider(rect.AlignRight(rect.width * 0.5f), "Y", value.Y, 0, 1);

            EditorGUIUtility.labelWidth = prev;

            this.ValueEntry.SmartValue = value;
        }
    }

#endif
}
#endif
