#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using Sirenix.OdinInspector.Editor;
    using System.Linq;
    using UnityEngine;
    using Sirenix.Utilities.Editor;
    using Sirenix.Serialization;
    using UnityEditor;
    using Sirenix.Utilities;

    // 
    // 请务必查看OdinMenuStyleExample.cs。它展示了自定义OdinMenuTrees外观和行为的各种方法。
    // 

    public class OdinMenuEditorWindowExample : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Odin/Demos/Odin编辑器窗口演示/Odin菜单编辑器窗口示例")]
        private static void OpenWindow()
        {
            var window = GetWindow<OdinMenuEditorWindowExample>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        [SerializeField]
        private SomeData someData = new SomeData(); // 查看SomeData.cs了解编辑器窗口中的序列化工作原理。

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                { "首页",                           this,                           EditorIcons.House                       }, // 在这种情况下绘制this.someData字段。
                { "Odin设置",                       null,                           SdfIconType.GearFill                    },
                { "Odin设置/调色板",                ColorPaletteManager.Instance,   SdfIconType.PaletteFill                 },
                { "Odin设置/AOT生成",               AOTGenerationConfig.Instance,   EditorIcons.SmartPhone                  },
                { "播放器设置",                     Resources.FindObjectsOfTypeAll<PlayerSettings>().FirstOrDefault()       },
                { "一些类",                         this.someData                                                           }
            };

            tree.AddAllAssetsAtPath("Odin设置/更多Odin设置", "Plugins/Sirenix", typeof(ScriptableObject), true)
                .AddThumbnailIcons();

            tree.AddAssetAtPath("Odin入门指南", "Plugins/Sirenix/Getting Started With Odin.asset");

            tree.MenuItems.Insert(2, new OdinMenuItem(tree, "菜单样式", tree.DefaultMenuStyle));

            tree.Add("菜单/项目/按需/创建", new GUIContent());
            tree.Add("菜单/项目/按需", new GUIContent("并可以被覆盖"));

            tree.SortMenuItemsByName();

            // 如您所见，Odin提供了几种方法来快速将编辑器/对象添加到您的菜单树中。
            // API还使您可以完全控制选择等..
            // 请务必查看OdinMenuEditorWindow、OdinMenuTree和OdinMenuItem的API文档，以了解更多有关您可以做什么的信息！

            return tree;
        }
    }
}
#endif
