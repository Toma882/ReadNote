using Sirenix.OdinInspector;
using UnityEngine;

namespace OdinSamples.CustomEditors
{
    /// <summary>
    /// 自定义 Odin Editor 示例
    /// 这是要被自定义编辑器绘制的目标类
    /// </summary>
    public class CustomOdinEditorSample : MonoBehaviour
    {
        [Title("角色信息")]
        
        [LabelText("角色名称")]
        [Required]
        public string characterName = "勇者";
        
        [LabelText("等级")]
        [Range(1, 100)]
        public int level = 1;
        
        [Title("属性")]
        
        [LabelText("生命值")]
        [ProgressBar(0, 100)]
        public float health = 100f;
        
        [LabelText("魔法值")]
        [ProgressBar(0, 100)]
        public float mana = 50f;
        
        [Title("装备")]
        
        [PreviewField(80, ObjectFieldAlignment.Left)]
        [LabelText("武器")]
        public GameObject weapon;
        
        [PreviewField(80, ObjectFieldAlignment.Left)]
        [LabelText("护甲")]
        public GameObject armor;
        
        [Title("技能")]
        
        [TableList(AlwaysExpanded = true)]
        public System.Collections.Generic.List<Skill> skills = new System.Collections.Generic.List<Skill>
        {
            new Skill { name = "火球术", level = 1, manaCost = 10 },
            new Skill { name = "治疗", level = 1, manaCost = 15 }
        };
        
        [System.Serializable]
        public class Skill
        {
            [TableColumnWidth(100)]
            public string name;
            
            [TableColumnWidth(60)]
            public int level;
            
            [TableColumnWidth(80)]
            public int manaCost;
        }
    }
}

