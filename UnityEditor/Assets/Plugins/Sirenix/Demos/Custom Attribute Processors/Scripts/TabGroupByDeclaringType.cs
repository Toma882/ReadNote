#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;

    [TypeInfoBox(
        "此示例演示了如何使用AttributeProcessors根据属性声明的位置将其排列到不同的组中。")]
    public class TabGroupByDeclaringType : Bar // Bar继承自Foo
    {
        public string A, B, C;
    }

    public class TabifyFooResolver<T> : OdinAttributeProcessor<T> where T : Foo
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            var inheritanceDistance = member.DeclaringType.GetInheritanceDistance(typeof(object));
            var tabName = member.DeclaringType.Name;
            attributes.Add(new TabGroupAttribute(tabName));
            attributes.Add(new PropertyOrderAttribute(-inheritanceDistance));
        }
    }
}
#endif
