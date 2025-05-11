#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using Sirenix.OdinInspector.Editor;
    using UnityEngine;

    [TypeInfoBox("此示例演示了AttributeProcessors如何按优先级排序。")]
    public class AttributeProcessorPriorityExample : MonoBehaviour
    {
        public PrioritizedProcessed Processed;
    }

    [Serializable]
    public class PrioritizedProcessed
    {
        public int A;
    }

    // 这个处理器具有最高优先级，因此最先执行。
    // 它为PrioritizedResolved类的子成员添加了Range属性。
    // 这个范围属性将被SecondAttributeProcessor移除。
    [ResolverPriority(100)]
    public class FirstAttributeProcessor : OdinAttributeProcessor<PrioritizedProcessed>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            attributes.Add(new BoxGroupAttribute("第一"));
            attributes.Add(new RangeAttribute(0, 10));
        }
    }

    // 这个处理器的默认优先级为0，因此第二个执行。
    // 它清除了属性列表，因此移除了PrioritizedResolved类成员的所有属性。
    public class SecondAttributeProcessor : OdinAttributeProcessor<PrioritizedProcessed>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            attributes.RemoveAttributeOfType<RangeAttribute>();

            var boxGroup = attributes.OfType<BoxGroupAttribute>().FirstOrDefault();
            boxGroup.GroupName = boxGroup.GroupName + " - 第二";
        }
    }

    // 这个处理器具有最低优先级，因此最后执行。
    // 它为PrioritizedResolved类的子成员添加了BoxGroup。
    // 由于这是在SecondAttributeProcessor之后执行的，BoxGroup属性不会被移除。
    [ResolverPriority(-100)]
    public class ThirdAttributeProcessor : OdinAttributeProcessor<PrioritizedProcessed>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            var boxGroup = attributes.OfType<BoxGroupAttribute>().FirstOrDefault();
            boxGroup.GroupName = boxGroup.GroupName + " - 第三";
        }
    }
}
#endif
