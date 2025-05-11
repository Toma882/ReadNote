/* =======================================================
 *  Unity版本：2021.3.0f1c1
 *  作 者：张寿昆
 *  邮 箱：136512892@qq.com
 *  创建时间：2025-05-11 22:44:38
 *  当前版本：1.0.0
 *  主要功能：
 *  详细描述：
 *  修改记录：
 * =======================================================*/

#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Serialization;

    //
    // 这个类由RPGEditorWindow使用，用于使用TableList属性渲染所有角色的概览。
    // 所有角色都是Unity对象，所以它们在检查器中被渲染为单个Unity对象字段，
    // 这不是我们想要在我们的表格中显示的。我们想要显示Unity对象的成员。
    //
    // 所以为了渲染Unity对象的成员，我们将创建一个类来包装Unity对象，
    // 并通过属性显示相关成员，这可以与TableList属性一起工作。
    //

    public class CharacterTable
    {
        [FormerlySerializedAs("allCharecters")]
        [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
        private readonly List<CharacterWrapper> allCharacters;

        public Character this[int index]
        {
            get { return this.allCharacters[index].Character; }
        }

        public CharacterTable(IEnumerable<Character> characters)
        {
            this.allCharacters = characters.Select(x => new CharacterWrapper(x)).ToList();
        }

        private class CharacterWrapper
        {
            private Character character; // Character是一个ScriptableObject，如果在检查器中绘制，
                                         // 会渲染一个unity对象字段，这不是我们想要的。

            public Character Character
            {
                get { return this.character; }
            }

            public CharacterWrapper(Character character)
            {
                this.character = character;
            }

            [TableColumnWidth(50, false)]
            [ShowInInspector, PreviewField(45, ObjectFieldAlignment.Center)]
            public Texture Icon { get { return this.character.Icon; } set { this.character.Icon = value; EditorUtility.SetDirty(this.character); } }

            [TableColumnWidth(120)]
            [ShowInInspector]
            public string Name { get { return this.character.Name; } set { this.character.Name = value; EditorUtility.SetDirty(this.character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Shooting { get { return this.character.Skills.Shooting; } set { this.character.Skills.Shooting = value; EditorUtility.SetDirty(this.character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Melee { get { return this.character.Skills.Melee; } set { this.character.Skills.Melee = value; EditorUtility.SetDirty(this.character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Social { get { return this.character.Skills.Social; } set { this.character.Skills.Social = value; EditorUtility.SetDirty(this.character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Animals { get { return this.character.Skills.Animals; } set { this.character.Skills.Animals = value; EditorUtility.SetDirty(this.character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Medicine { get { return this.character.Skills.Medicine; } set { this.character.Skills.Medicine = value; EditorUtility.SetDirty(this.character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Crafting { get { return this.character.Skills.Crafting; } set { this.character.Skills.Crafting = value; EditorUtility.SetDirty(this.character); } }
        }
    }
}
#endif 