#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using System;
    using UnityEngine;

    // 
    // CharacterStats只是一个StatList，它暴露了角色的相关统计属性。
    // 还要注意StatList可能看起来像一个字典，在它的使用方式上，
    // 但它实际上只是一个由Unity序列化的普通列表。看看StatList了解更多。
    // 

    [Serializable]
    public class CharacterStats
    {
        [HideInInspector]
        public StatList Stats = new StatList();

        [ProgressBar(0, 100), ShowInInspector]
        public float Shooting
        {
            get { return this.Stats[StatType.Shooting]; }
            set { this.Stats[StatType.Shooting] = value; }
        }

        [ProgressBar(0, 100), ShowInInspector]
        public float Melee
        {
            get { return this.Stats[StatType.Melee]; }
            set { this.Stats[StatType.Melee] = value; }
        }

        [ProgressBar(0, 100), ShowInInspector]
        public float Social
        {
            get { return this.Stats[StatType.Social]; }
            set { this.Stats[StatType.Social] = value; }
        }

        [ProgressBar(0, 100), ShowInInspector]
        public float Animals
        {
            get { return this.Stats[StatType.Animals]; }
            set { this.Stats[StatType.Animals] = value; }
        }

        [ProgressBar(0, 100), ShowInInspector]
        public float Medicine
        {
            get { return this.Stats[StatType.Medicine]; }
            set { this.Stats[StatType.Medicine] = value; }
        }

        [ProgressBar(0, 100), ShowInInspector]
        public float Crafting
        {
            get { return this.Stats[StatType.Crafting]; }
            set { this.Stats[StatType.Crafting] = value; }
        }
    }
}
#endif
