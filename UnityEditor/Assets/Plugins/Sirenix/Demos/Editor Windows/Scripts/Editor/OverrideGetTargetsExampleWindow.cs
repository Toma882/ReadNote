#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using UnityEditor;
    using UnityEngine;
    using System.Collections.Generic;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using Sirenix.OdinInspector;
    using Sirenix.Utilities;

    public class OverrideGetTargetsExampleWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Odin/Demos/Odin编辑器窗口演示/绘制任意目标")]
        private static void OpenWindow()
        {
            GetWindow<OverrideGetTargetsExampleWindow>()
                .position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        [HideLabel]
        [Multiline(6)]
        [SuffixLabel("这个是被绘制的", true)]
        public string Test;

        // 在默认实现中，它只是简单地返回自身。
        // 但您也可以覆盖此行为，让您的窗口渲染任何您想要的对象
        // - 无论是Unity对象还是非Unity对象。
        protected override IEnumerable<object> GetTargets()
        {
            // 使用Odin绘制此实例
            yield return this;

            // 绘制非Unity对象。
            yield return GUI.skin.settings; // GUISettings是一个普通类。

            // 或Unity对象。
            yield return GUI.skin; // GUI.Skin是一个ScriptableObject
        }

        // 您还可以覆盖绘制每个编辑器的方法。
        // 如果您想添加标题、框或在GUI.Window等中绘制它们，这会很方便...
        protected override void DrawEditor(int index)
        {
            var currentDrawingEditor = this.CurrentDrawingTargets[index];

            SirenixEditorGUI.Title(
                title: currentDrawingEditor.ToString(),
                subtitle: currentDrawingEditor.GetType().GetNiceFullName(),
                textAlignment: TextAlignment.Left,
                horizontalLine: true
            );

            base.DrawEditor(index);

            if (index != this.CurrentDrawingTargets.Count - 1)
            {
                SirenixEditorGUI.DrawThickHorizontalSeparator(15, 15);
            }
        }
    }
}
#endif
