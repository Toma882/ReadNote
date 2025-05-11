#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using Sirenix.OdinInspector.Editor;
    using UnityEngine;
    using UnityEditor;
    using System.Linq;
    using Sirenix.Utilities;
    using System.Collections.Generic;
    using Sirenix.Utilities.Editor;

    public class OdinMenuStyleExample : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Odin/Demos/Odin编辑器窗口演示/Odin菜单样式示例")]
        private static void OpenWindow()
        {
            var window = GetWindow<OdinMenuStyleExample>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);

            var customMenuStyle = new OdinMenuStyle
            {
                BorderPadding = 0f,
                AlignTriangleLeft = true,
                TriangleSize = 16f,
                TrianglePadding = 0f,
                Offset = 20f,
                Height = 23,
                IconPadding = 0f,
                BorderAlpha = 0.323f
            };

            tree.DefaultMenuStyle = customMenuStyle;

            tree.Config.DrawSearchToolbar = true;

            // 将自定义菜单样式添加到树中，以便你可以随意调整它。
            // 满意后，可以按下"复制C#代码片段"按钮复制其设置并粘贴到代码中。
            // 然后从树中删除"菜单样式"菜单项。
            tree.AddObjectAtPath("菜单样式", customMenuStyle);

            for (int i = 0; i < 5; i++)
            {
                var customObject = new SomeCustomClass() { Name = i.ToString() };
                var customMenuItem = new MyCustomMenuItem(tree, customObject);
                tree.AddMenuItemAtPath("自定义菜单项", customMenuItem);
            }

            tree.AddAllAssetsAtPath("Plugins中的ScriptableObjects(树形)", "Plugins", typeof(ScriptableObject), true, false);
            tree.AddAllAssetsAtPath("Plugins中的ScriptableObjects(平铺)", "Plugins", typeof(ScriptableObject), true, true);
            tree.AddAllAssetsAtPath("只有Configs有图标", "Plugins/Sirenix", true, false);

            tree.EnumerateTree()
                .AddThumbnailIcons()
                .SortMenuItemsByName();

            return tree;
        }

        //// 编辑器窗口本身也可以自定义。
        //protected override void OnEnable()
        //{
        //    base.OnEnable();

        //    this.MenuWidth = 200;
        //    this.ResizableMenuWidth = true;
        //    this.WindowPadding = new Vector4(10, 10, 10, 10);
        //    this.DrawUnityEditorPreview = true;
        //    this.DefaultEditorPreviewHeight = 20;
        //    this.UseScrollView = true;
        //}

        private class MyCustomMenuItem : OdinMenuItem
        {
            private readonly SomeCustomClass instance;

            public MyCustomMenuItem(OdinMenuTree tree, SomeCustomClass instance) : base(tree, instance.Name, instance)
            {
                this.instance = instance;
            }

            protected override void OnDrawMenuItem(Rect rect, Rect labelRect)
            {
                labelRect.x -= 16;
                this.instance.Enabled = GUI.Toggle(labelRect.AlignMiddle(18).AlignLeft(16), this.instance.Enabled, GUIContent.none);

                // 按空格键时切换选择。
                if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space)
                {
                    var selection = this.MenuTree.Selection
                        .Select(x => x.Value)
                        .OfType<SomeCustomClass>();

                    if (selection.Any())
                    {
                        var enabled = !selection.FirstOrDefault().Enabled;
                        selection.ForEach(x => x.Enabled = enabled);
                        Event.current.Use();
                    }
                }
            }

            public override string SmartName { get { return this.instance.Name; } }
        }

        private class SomeCustomClass
        {
            public bool Enabled = true;
            public string Name;
        }
    }
}
#endif
