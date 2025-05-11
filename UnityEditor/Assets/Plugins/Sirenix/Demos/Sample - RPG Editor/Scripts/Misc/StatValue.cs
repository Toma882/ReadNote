#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using System;
    using UnityEngine;

    // 
    // 这些StatValues被StatLists使用，并且设置为一旦被添加到列表中，StatType就不能被更改。
    // 这是通过给Type一个HideInInspector属性，然后我们将Value标签重命名为类型的名称，
    // 并设置LabelWidth使其更加紧凑。
    // 

    [Serializable]
    public struct StatValue : IEquatable<StatValue>
    {
        [HideInInspector]
        public StatType Type;

        [Range(-100, 100)]
        [LabelWidth(70)]
        [LabelText("$Type")]
        public float Value;

        public StatValue(StatType type, float value)
        {
            this.Type = type;
            this.Value = value;
        }

        public StatValue(StatType type)
        {
            this.Type = type;
            this.Value = 0;
        }

        public bool Equals(StatValue other)
        {
            return this.Type == other.Type && this.Value == other.Value;
        }
    }
}
#endif
