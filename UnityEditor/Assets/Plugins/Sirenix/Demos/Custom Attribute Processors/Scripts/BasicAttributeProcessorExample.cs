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

    [Serializable]
    public class MyCustomClass
    {
        public ScaleMode Mode;
        public float Size;
    }

    // 这个AttributeProcessor将被发现并用于处理MyCustomClass类的属性。
    public class MyResolvedClassAttributeProcessor : OdinAttributeProcessor<MyCustomClass>
    {
        // 这个方法将被调用用于任何MyCustomClass类型的字段或属性。
        // 在本例中，它将为BasicAttributeProcessorExample.Processed字段运行。
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new InfoBoxAttribute("动态添加的属性。"));
            attributes.Add(new InlinePropertyAttribute());
        }

        // 这个方法将被调用用于MyCustomClass类型的任何成员。
        // 在本例中，它将为MyCustomClass.Mode和MyCustomClass.Size字段运行。
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new BoxGroupAttribute("盒子", showLabel: false));

            if (member.Name == "Mode")
            {
                attributes.Add(new EnumToggleButtonsAttribute());
            }
            else if (member.Name == "Size")
            {
                attributes.Add(new RangeAttribute(0, 5));
            }
        }
    }
}
#endif