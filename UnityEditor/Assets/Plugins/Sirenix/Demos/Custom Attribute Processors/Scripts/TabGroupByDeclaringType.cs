#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;

    [TypeInfoBox("此示例演示了如何使用AttributeProcessors根据属性声明的位置将其排列到不同的组中。")]
    public class TabGroupByDeclaringType : Bar // Bar继承自Foo
    {
        public string A, B, C;
    }

    public class TabifyFooResolver<T> : OdinAttributeProcessor<T> where T : Foo
    {
        //parentProperty是一个InspectorProperty对象，表示当前正在处理的属性的父级。
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            //DeclaringType?是一个PropertyInfo对象，表示成员的声明类型。
            //GetInheritanceDistance?是一个扩展方法，用于计算成员声明类型与object类型之间的继承距离。
            var inheritanceDistance = member.DeclaringType.GetInheritanceDistance(typeof(object));
            var tabName = member.DeclaringType.Name;//获取声明类型的名称
            attributes.Add(new TabGroupAttribute(tabName));
            attributes.Add(new PropertyOrderAttribute(-inheritanceDistance));
        }
    }
}
#endif
