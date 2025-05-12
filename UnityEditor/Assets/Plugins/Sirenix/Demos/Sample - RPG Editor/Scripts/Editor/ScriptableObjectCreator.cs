#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    // 
    // 通过我们自定义的RPG编辑器窗口，这个ScriptableObjectCreator是Unity提供的[CreateAssetMenu]属性的替代品。
    // 
    // 例如，如果我们调用ScriptableObjectCreator.ShowDialog<Item>(..., ...)，它将自动在你的项目中找到所有
    // 继承自Item的ScriptableObjects，并提示用户一个弹出窗口，让他选择他想要创建的物品类型。
    // 我们还为ShowDialog提供了一个默认路径，以帮助用户在特定目录中创建它。
    // 

    public static class ScriptableObjectCreator
    {
        public static void ShowDialog<T>(string defaultDestinationPath, Action<T> onScritpableObjectCreated = null)
            where T : ScriptableObject
        {
            var selector = new ScriptableObjectSelector<T>(defaultDestinationPath, onScritpableObjectCreated);

            if (selector.SelectionTree.EnumerateTree().Count() == 1)
            {
                // 如果选择器中只有一个可选的scriptable object，那么
                // 我们将自动选择它并确认选择。
                selector.SelectionTree.EnumerateTree().First().Select();
                selector.SelectionTree.Selection.ConfirmSelection();
            }
            else
            {
                // 否则，我们将在弹出窗口中打开选择器，让用户选择。
                selector.ShowInPopup(200);
            }
        }

        // 这里是实际的ScriptableObjectSelector，它继承自OdinSelector。
        // 你可以在文档中了解更多关于它们的信息：http://sirenix.net/odininspector/documentation/sirenix/odininspector/editor/odinselector(t)
        // 这个选择器构建了一个继承自T的所有类型的菜单树，当选择确认时，它会提示用户
        // 一个保存对话框来保存新创建的scriptable object。

        private class ScriptableObjectSelector<T> : OdinSelector<Type> where T : ScriptableObject
        {
            private Action<T> onScritpableObjectCreated;
            private string defaultDestinationPath;

            public ScriptableObjectSelector(string defaultDestinationPath, Action<T> onScritpableObjectCreated = null)
            {
                this.onScritpableObjectCreated = onScritpableObjectCreated;
                this.defaultDestinationPath = defaultDestinationPath;
                this.SelectionConfirmed += this.ShowSaveFileDialog;
            }

            protected override void BuildSelectionTree(OdinMenuTree tree)
            {
                var scriptableObjectTypes = AssemblyUtilities.GetTypes(AssemblyCategory.ProjectSpecific)
                    .Where(x => x.IsClass && !x.IsAbstract && x.InheritsFrom(typeof(T)));

                tree.Selection.SupportsMultiSelect = false;
                tree.Config.DrawSearchToolbar = true;
                tree.AddRange(scriptableObjectTypes, x => x.GetNiceName()).AddThumbnailIcons();
            }

            private void ShowSaveFileDialog(IEnumerable<Type> selection)
            {
                var obj = ScriptableObject.CreateInstance(selection.FirstOrDefault()) as T;

                string dest = this.defaultDestinationPath.TrimEnd('/');

                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                    AssetDatabase.Refresh();
                }

                dest = EditorUtility.SaveFilePanel("保存对象为", dest, "新建 " + typeof(T).GetNiceName(), "asset");

                if (!string.IsNullOrEmpty(dest) && PathUtilities.TryMakeRelative(Path.GetDirectoryName(Application.dataPath), dest, out dest))
                {
                    AssetDatabase.CreateAsset(obj, dest);
                    AssetDatabase.Refresh();

                    if (this.onScritpableObjectCreated != null)
                    {
                        this.onScritpableObjectCreated(obj);
                    }
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(obj);
                }
            }
        }
    }
}
#endif
