#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Sirenix.OdinInspector.Editor;
    using UnityEngine;
    using UnityEditor;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;

    [TypeInfoBox("此示例演示了如何创建自定义AttributeProcessorLocator来完全改变整个PropertyTree解析属性的方式，从而改变树中所有对象的显示方式。")]
    public class CustomAttributeProcessorLocatorExample : MonoBehaviour
    {
        [Button(ButtonSizes.Large)]
        private void OpenEditorWindow()
        {
            var window = Editor.CreateInstance<SomeCustomEditorWindow>();
            window.Show();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 300);
        }
    }

    public class SomeCustomEditorWindow : UnityEditor.EditorWindow
    {
        private PropertyTree defaultPropertyTree;
        private PropertyTree customPropertyTree;

        private void OnEnable()
        {
            this.wantsMouseMove = true;
        }

        private void OnGUI()
        {
            this.DrawWithDefaultLocator();
            this.DrawWithCustomLocator();

            this.RepaintIfRequested();
        }

        private void DrawWithDefaultLocator()
        {
            if (this.defaultPropertyTree == null)
            {
                this.defaultPropertyTree = PropertyTree.Create(new SomeClass());
            }

            SirenixEditorGUI.BeginBox("默认定位器");
            this.defaultPropertyTree.Draw(false);
            SirenixEditorGUI.EndBox();
        }

        private void DrawWithCustomLocator()
        {
            if (this.customPropertyTree == null)
            {
                this.customPropertyTree = PropertyTree.Create(new SomeClass());
                this.customPropertyTree.AttributeProcessorLocator = new CustomMinionAttributeProcessorLocator();
            }

            SirenixEditorGUI.BeginBox("自定义定位器");
            this.customPropertyTree.Draw(false);
            SirenixEditorGUI.EndBox();
        }
    }

    public class SomeClass
    {
        [HorizontalGroup("Split", LabelWidth = 80)]
        [BoxGroup("Split/$Name", showLabel: false)]
        [BoxGroup("Split/$Name/NameId", showLabel: false)]
        public string Name, Id;

        [HideLabel, PropertyOrder(5)]
        [PreviewField(Height = 105), HorizontalGroup("Split", width: 105)]
        public Texture2D Icon;

        [BoxGroup("Split/$Name/Properties", showLabel: false)]
        public int Health, Damage, Speed;
    }

    [OdinDontRegister] // 此属性防止Odin在默认属性解析器定位器中使用此AttributeProcessor。
    public class CustomMinionAttributeProcessor : OdinAttributeProcessor<SomeClass>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            attributes.Clear(); // 清除所有其他属性。

            switch (member.Name)
            {
                case "Icon":
                    attributes.Add(new HorizontalGroupAttribute("Split", width: 70));
                    attributes.Add(new PreviewFieldAttribute(70, OdinInspector.ObjectFieldAlignment.Left));
                    attributes.Add(new PropertyOrderAttribute(-5));
                    attributes.Add(new HideLabelAttribute());
                    break;

                case "Name":
                case "Id":
                    attributes.Add(new BoxGroupAttribute("Split/$Name"));
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

    public class CustomMinionAttributeProcessorLocator : OdinAttributeProcessorLocator
    {
        private static readonly CustomMinionAttributeProcessor Processor = new CustomMinionAttributeProcessor();

        public override List<OdinAttributeProcessor> GetChildProcessors(InspectorProperty parentProperty, MemberInfo member)
        {
            return new List<OdinAttributeProcessor>() { Processor };
        }

        public override List<OdinAttributeProcessor> GetSelfProcessors(InspectorProperty property)
        {
            return new List<OdinAttributeProcessor>() { Processor };
        }
    }
}
#endif
