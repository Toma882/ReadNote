#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Sirenix.OdinInspector.Editor;
    using UnityEngine;

    [TypeInfoBox(
        "在Odin 2.0中，现在可以通过使用AttributeProcessors间接地装饰类型。\n" +
        "这意味着您甚至可以为无法访问源代码的类型添加属性。")]
    public class PutAttributesOnAnyType : MonoBehaviour
    {
        public Matrix4x4 Matrix;
    }

    public class Matrix4x4AttributeProcessor : OdinAttributeProcessor<Matrix4x4>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member is FieldInfo)
            {
                //attributes.Add(new IndentAttribute(-1));
                attributes.Add(new LabelWidthAttribute(30));
                attributes.Add(new HorizontalGroupAttribute(member.Name.Substring(0, 2)));
            }

            if (member.Name == "determinant")
            {
                //attributes.Add(new IndentAttribute(-1));
                attributes.Add(new ShowInInspectorAttribute());
            }
        }
    }
}
#endif
