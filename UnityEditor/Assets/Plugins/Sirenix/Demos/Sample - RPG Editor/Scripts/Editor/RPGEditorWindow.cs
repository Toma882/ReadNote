#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using UnityEditor;
    using UnityEngine;
    using System.Linq;

    // 
    // 这是主要的RPG编辑器，它展示了此示例项目中包含的所有内容。
    // 
    // 这个编辑器窗口允许用户编辑和创建角色与物品。为了实现这一点，我们继承了OdinMenuEditorWindow，
    // 这让我们可以快速为各种对象添加菜单项。每个这些对象都使用Odin属性进行自定义，使编辑器更加用户友好。
    // 
    // 为了让用户创建物品和角色，我们实际上并没有为任何ScriptableObject使用[CreateAssetMenu]属性，
    // 而是制作了一个自定义的ScriptableObjectCreator，我们在下方OnBeginDrawEditors方法中绘制的自定义工具栏中使用它。
    // 
    // 去探索各个类，看看是如何实现的。
    // 

    public class RPGEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Odin/Demos/RPG编辑器")]
        private static void Open()
        {
            var window = GetWindow<RPGEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            // 添加角色概览表格
            CharacterOverview.Instance.UpdateCharacterOverview();
            tree.Add("角色", new CharacterTable(CharacterOverview.Instance.AllCharacters));

            // 添加所有角色
            tree.AddAllAssetsAtPath("角色", "Assets/Plugins/Sirenix", typeof(Character), true, true);

            // 添加所有ScriptableObject物品
            tree.AddAllAssetsAtPath("", "Assets/Plugins/Sirenix/Demos/SAMPLE - RPG Editor/Items", typeof(Item), true)
                .ForEach(this.AddDragHandles);

            // 为物品添加拖动句柄，使它们可以轻松拖入角色的库存等...
            tree.EnumerateTree().Where(x => x.Value as Item).ForEach(AddDragHandles);

            // 为角色和物品添加图标
            tree.EnumerateTree().AddIcons<Character>(x => x.Icon);
            tree.EnumerateTree().AddIcons<Item>(x => x.Icon);

            return tree;
        }

        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            // 绘制一个工具栏，显示当前选择的菜单项名称
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("创建物品")))
                {
                    ScriptableObjectCreator.ShowDialog<Item>("Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/Items", obj =>
                    {
                        obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // 在编辑器中选择新创建的物品
                    });
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("创建角色")))
                {
                    ScriptableObjectCreator.ShowDialog<Character>("Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/Character", obj =>
                    {
                        obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // 在编辑器中选择新创建的角色
                    });
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}
#endif
