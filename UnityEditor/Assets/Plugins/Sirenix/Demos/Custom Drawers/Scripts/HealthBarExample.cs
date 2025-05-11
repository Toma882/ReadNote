#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using UnityEngine;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;

#endif

    // 演示如何为属性创建自定义绘制器的示例。
    [TypeInfoBox("这里展示了一个使用自定义属性绘制器绘制的生命值条的可视化效果。")]
    public class HealthBarExample : MonoBehaviour
    {
        [HealthBar(100)]
        public float Health;
    }

    // HealthBarAttributeDrawer使用的属性。
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class HealthBarAttribute : Attribute
    {
        public float MaxHealth { get; private set; }

        public HealthBarAttribute(float maxHealth)
        {
            this.MaxHealth = maxHealth;
        }
    }

#if UNITY_EDITOR

    // 将绘制器脚本文件放在Editor文件夹中，或将其包装在#if UNITY_EDITOR条件中。
    public class HealthBarAttributeDrawer : OdinAttributeDrawer<HealthBarAttribute, float>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 调用下一个绘制器，它将绘制浮点字段。
            this.CallNextDrawer(label);

            // 获取一个用于绘制生命值条的矩形。您也可以使用GUILayout，但使用矩形使绘制生命值条更简单。
            Rect rect = EditorGUILayout.GetControlRect();

            // 绘制生命值条。
            float width = Mathf.Clamp01(this.ValueEntry.SmartValue / this.Attribute.MaxHealth);
            SirenixEditorGUI.DrawSolidRect(rect, new Color(0f, 0f, 0f, 0.3f), false);
            SirenixEditorGUI.DrawSolidRect(rect.SetWidth(rect.width * width), Color.red, false);
            SirenixEditorGUI.DrawBorders(rect, 1);
        }
    }

#endif
}
#endif
