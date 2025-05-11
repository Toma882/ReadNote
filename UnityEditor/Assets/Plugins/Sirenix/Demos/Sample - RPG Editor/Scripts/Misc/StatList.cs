#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

#if UNITY_EDITOR
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using System.Collections;
#endif

    // 
    // StatList是一个类字典的StatValues列表，它持有一个StatType和一个值。
    // 这可以被系统中的许多事物使用。在这个例子中，StatLists被角色和物品
    // 用来定义需求和修饰符。但可以想象，游戏中的许多事物都可以有StatLists。
    // 
    // 它是一个列表而不是字典的原因是，大多数情况下StatLists并不包含很多统计属性。
    // 例如，一个盾牌可能增加一些防御和几种其他随机加成，
    // 如果优化，对一打值进行迭代实际上比进行字典查找更快。
    // 
    // StatList然后用ValueDropdown属性进行自定义，我们覆盖元素的添加方式，
    // 并使用OdinSelectors向用户提供一个类型列表供选择。
    // 查看此脚本底部的CustomAddStatsButton。
    // 

    [Serializable]
    public class StatList
    {
        [SerializeField]
        [ValueDropdown("CustomAddStatsButton", IsUniqueList = true, DrawDropdownForListElements = false, DropdownTitle = "修改统计属性")]
        [ListDrawerSettings(DraggableItems = false, ShowFoldout = true)]
        private List<StatValue> stats = new List<StatValue>();

        public StatValue this[int index]
        {
            get { return this.stats[index]; }
            set { this.stats[index] = value; }
        }

        public int Count
        {
            get { return this.stats.Count; }
        }

        public float this[StatType type]
        {
            get
            {
                for (int i = 0; i < this.stats.Count; i++)
                {
                    if (this.stats[i].Type == type)
                    {
                        return this.stats[i].Value;
                    }
                }

                return 0;
            }
            set
            {
                for (int i = 0; i < this.stats.Count; i++)
                {
                    if (this.stats[i].Type == type)
                    {
                        var val = this.stats[i];
                        val.Value = value;
                        this.stats[i] = val;
                        return;
                    }
                }

                this.stats.Add(new StatValue(type, value));
            }
        }

#if UNITY_EDITOR
        // 查找所有可用的统计类型，并排除statList已经包含的类型，这样我们就不会出现同一类型的多个条目。
        private IEnumerable CustomAddStatsButton()
        {
            return Enum.GetValues(typeof(StatType)).Cast<StatType>()
                .Except(this.stats.Select(x => x.Type))
                .Select(x => new StatValue(x))
                .AppendWith(this.stats)
                .Select(x => new ValueDropdownItem(x.Type.ToString(), x));
        }
#endif
    }

#if UNITY_EDITOR

    // 
    // 由于StatList只是一个包含列表的类，所有StatLists在检查器中都会包含一个带有折叠箭头的额外标签，这是我们不想要的。
    // 
    // 所以通过这个绘制器，我们简单地采用持有StatsList的成员的标签，并使用该标签渲染实际的列表。
    //
    // 所以不是"private List<StatValue> stats"字段获得一个名为"Stats"的标签，
    // 它现在获得的是持有实际StatsList的成员的标签
    // 
    // 如果这让你困惑，试着注释掉下面的绘制器，然后在RPGEditor中查看一个物品，看看有什么不同。
    // 

    internal class StatListValueDrawer : OdinValueDrawer<StatList>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 这将是"private List<StatValue> stats"字段。
            this.Property.Children[0].Draw(label);
        }
    }

#endif
}
#endif
