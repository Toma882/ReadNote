using UnityEditor;

#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Sirenix.OdinInspector.Editor;
    using UnityEngine;

    public class BasicAttributeProcessorExample : MonoBehaviour
    {
        public MyCustomClass Processed = new MyCustomClass();
    }

    /// <summary>
    /// MyCustomClass是一个示例类，包含两个字段：Mode和Size。
    /// </summary>
    [Serializable]
    public class MyCustomClass
    {
        public ScaleMode Mode;
        public float Size;
    }
    
    [CustomEditor(typeof(MyCustomClass))]
    public class My : UnityEditor.Editor
    {
        // 这个方法用于在Inspector面板中绘制MyCustomClass类的内容。
        public override void OnInspectorGUI()
        {
            // 绘制MyCustomClass类的所有字段和属性。
            base.OnInspectorGUI();
            
            // 你可以在这里添加其他自定义的GUI元素或逻辑。
            EditorGUI.Slider(new Rect(0, 0, 100, 20),"Size", 0.5f, 0.0f, 1.0f);
        }
    }   
    
    
    // 这个OdinAttributeProcessor在用途上类似于PropertyDrawer，都用于自定义Inspector的显示和行为，
    // 但OdinAttributeProcessor通过动态添加或修改属性特性，适用范围更广、灵活性更高，而PropertyDrawer主要用于自定义单个字段的绘制。
    public class MyResolvedClassAttributeProcessor : OdinAttributeProcessor<MyCustomClass>
    {
        // 这个方法将被调用用于任何MyCustomClass类型的字段或属性。
        // 在本例中，它将为BasicAttributeProcessorExample.Processed字段运行。
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new InfoBoxAttribute("动态添加的属性。"));
            
            //InlinePropertyAttribute（内联属性特性）是 Odin Inspector 提供的一个特性，用于让自定义类或结构体的字段在 Inspector 面板中“内联”显示其所有成员，而不是以折叠的方式显示为一个对象引用。
            attributes.Add(new InlinePropertyAttribute());
        }

        // 这个方法将被调用用于MyCustomClass类型的任何成员。
        // 在本例中，它将为MyCustomClass.Mode和MyCustomClass.Size字段运行。
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            attributes.Add(new BoxGroupAttribute("", false));
            if (member.Name == "Mode")
            {
                attributes.Add(new HideLabelAttribute());
                attributes.Add(new EnumToggleButtonsAttribute());
            }
            else if (member.Name == "Size")
            {
               attributes.Add(new LabelTextAttribute("Size (cm)"));
                attributes.Add(new RangeAttribute(0, 100));
            }
        }
    }
}
#endif