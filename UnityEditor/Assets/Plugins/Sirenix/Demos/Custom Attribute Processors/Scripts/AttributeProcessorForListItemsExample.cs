#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using Sirenix.OdinInspector.Editor;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    [TypeInfoBox(
        "这个示例展示了一个与自定义定位器示例类似的用例。\n" +
        "但这次我们展示的是一个只应用于列表项的AttributeProcessor。")]
    public class AttributeProcessorForListItemsExample : MonoBehaviour
    {
        [HideLabel]
        public ListedMinion NonListed;

        [ListDrawerSettings(ListElementLabelName = "Name")]
        public List<ListedMinion> ListedMinions;
    }

    [Serializable]
    public class ListedMinion
    {
        [BoxGroup("非列表")]
        [HorizontalGroup("非列表/Split", LabelWidth = 80)]
        [BoxGroup("非列表/Split/Name", showLabel: false)]
        [BoxGroup("非列表/Split/Name/NameId", showLabel: false)]
        public string Name, Id;

        [HideLabel, PropertyOrder(5)]
        [PreviewField(Height = 105), HorizontalGroup("非列表/Split", width: 105)]
        public Texture2D Icon;

        [BoxGroup("非列表/Split/Name/Properties", showLabel: false)]
        public int Health, Damage, Speed;
    }

    public class ListedMinionListAttributeProcessor : OdinAttributeProcessor<ListedMinion>
    {
        public override bool CanProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member)
        {
            return typeof(IList).IsAssignableFrom(parentProperty.ParentType);
        }

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            attributes.Clear();

            switch (member.Name)
            {
                case "Icon":
                    attributes.Add(new HorizontalGroupAttribute("Split", width: 70));
                    attributes.Add(new PreviewFieldAttribute(70, ObjectFieldAlignment.Left));
                    attributes.Add(new PropertyOrderAttribute(-5));
                    attributes.Add(new HideLabelAttribute());
                    break;

                case "Name":
                case "Id":
                    attributes.Add(new BoxGroupAttribute("Split/$Name", true));
                    attributes.Add(new VerticalGroupAttribute("Split/$Name/Vertical"));
                    attributes.Add(new HorizontalGroupAttribute("Split/$Name/Vertical/NameId"));
                    attributes.Add(new LabelWidthAttribute(40));
                    break;

                default:
                    attributes.Add(new FoldoutGroupAttribute("Split/$Name/Vertical/Properties", expanded: false));
                    attributes.Add(new LabelWidthAttribute(60));
                    break;
            }
        }
    }
}
#endif
