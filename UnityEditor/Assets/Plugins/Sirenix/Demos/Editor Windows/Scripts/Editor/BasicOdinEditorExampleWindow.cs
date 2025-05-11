#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using UnityEditor;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.OdinInspector;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;

    public class BasicOdinEditorExampleWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Odin/Demos/Odin编辑器窗口演示/基础Odin编辑器窗口")]
        private static void OpenWindow()
        {
            var window = GetWindow<BasicOdinEditorExampleWindow>();

            // 一个很棒的小技巧，可以快速将窗口定位在编辑器的中间。
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
        }

        [EnumToggleButtons]
        [InfoBox("从OdinEditorWindow而不是EditorWindow继承，以便像创建检查器一样创建编辑器窗口 - 通过公开成员和使用属性。")]
        public ViewTool SomeField;
    }
}
#endif
