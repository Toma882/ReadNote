using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Toma.OdinSamples.CompleteProject
{
    /// <summary>
    /// 属性列表系统
    /// 提供类似字典的属性管理功能，同时保持编辑器友好的显示
    /// 
    /// 功能特点：
    /// - 类字典访问方式
    /// - 自动属性类型验证
    /// - 编辑器中的表格显示
    /// - 支持快速查找和修改
    /// </summary>
    [Serializable]
    public class StatList
    {
        /// <summary>
        /// 内部属性列表
        /// 使用 TableList 特性在编辑器中以表格形式显示
        /// </summary>
        [TableList(ShowIndexLabels = true, AlwaysExpanded = true)]
        [ListDrawerSettings(
            DraggableItems = true,
            ShowItemCount = true,
            CustomAddFunction = "AddStat")]
        [InfoBox("属性列表：管理角色或物品的各种数值属性")]
        [SerializeField]
        private List<StatValue> stats = new List<StatValue>();

        /// <summary>
        /// 获取所有属性值
        /// </summary>
        public IReadOnlyList<StatValue> AllStats => stats;

        /// <summary>
        /// 获取属性数量
        /// </summary>
        public int Count => stats.Count;

        /// <summary>
        /// 索引器：通过属性类型访问属性值
        /// </summary>
        /// <param name="type">属性类型</param>
        /// <returns>属性值，如果不存在则返回0</returns>
        public float this[StatType type]
        {
            get => GetValue(type);
            set => SetValue(type, value);
        }

        /// <summary>
        /// 获取指定类型的属性值
        /// </summary>
        public float GetValue(StatType type)
        {
            var stat = stats.FirstOrDefault(s => s.Type == type);
            return stat.Value;
        }

        /// <summary>
        /// 设置指定类型的属性值
        /// 如果属性不存在，则自动添加
        /// </summary>
        public void SetValue(StatType type, float value)
        {
            var index = stats.FindIndex(s => s.Type == type);
            if (index >= 0)
            {
                var stat = stats[index];
                stat.Value = value;
                stats[index] = stat;
            }
            else
            {
                stats.Add(new StatValue(type, value));
            }
        }

        /// <summary>
        /// 添加新属性
        /// </summary>
        public void Add(StatType type, float value)
        {
            if (!Contains(type))
            {
                stats.Add(new StatValue(type, value));
            }
            else
            {
                Debug.LogWarning($"属性 {type} 已存在，使用 SetValue 来修改值");
            }
        }

        /// <summary>
        /// 移除指定属性
        /// </summary>
        public bool Remove(StatType type)
        {
            return stats.RemoveAll(s => s.Type == type) > 0;
        }

        /// <summary>
        /// 检查是否包含指定属性
        /// </summary>
        public bool Contains(StatType type)
        {
            return stats.Any(s => s.Type == type);
        }

        /// <summary>
        /// 清空所有属性
        /// </summary>
        public void Clear()
        {
            stats.Clear();
        }

        /// <summary>
        /// 自定义添加函数（供 Odin Inspector 使用）
        /// 确保不会添加重复的属性类型
        /// </summary>
        private StatValue AddStat()
        {
            // 查找第一个未使用的属性类型
            var allTypes = Enum.GetValues(typeof(StatType)).Cast<StatType>();
            var usedTypes = stats.Select(s => s.Type).ToHashSet();
            var availableType = allTypes.FirstOrDefault(t => !usedTypes.Contains(t));

            return new StatValue(availableType, 0f);
        }

        /// <summary>
        /// 从另一个 StatList 复制所有属性
        /// </summary>
        public void CopyFrom(StatList other)
        {
            stats.Clear();
            foreach (var stat in other.stats)
            {
                stats.Add(stat);
            }
        }

        /// <summary>
        /// 获取所有属性的格式化字符串
        /// </summary>
        public override string ToString()
        {
            if (stats.Count == 0)
                return "空属性列表";

            return string.Join("\n", stats.Select(s => s.ToString()));
        }

        #region 编辑器按钮（仅在编辑器中可用）

#if UNITY_EDITOR
        /// <summary>
        /// 快速添加常用战斗属性
        /// </summary>
        [Button("快速添加：战斗属性")]
        [PropertySpace(SpaceBefore = 10)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void AddCombatStats()
        {
            Add(StatType.Health, 100f);
            Add(StatType.Attack, 10f);
            Add(StatType.Defense, 5f);
            Add(StatType.Speed, 10f);
            Debug.Log("已添加基础战斗属性");
        }

        /// <summary>
        /// 快速添加常用角色属性
        /// </summary>
        [Button("快速添加：角色属性")]
        [GUIColor(0.3f, 0.6f, 0.9f)]
        private void AddCharacterStats()
        {
            Add(StatType.Strength, 10f);
            Add(StatType.Intelligence, 10f);
            Add(StatType.Dexterity, 10f);
            Add(StatType.Constitution, 10f);
            Debug.Log("已添加基础角色属性");
        }

        /// <summary>
        /// 清空所有属性（带确认）
        /// </summary>
        [Button("清空所有属性")]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ClearAllStats()
        {
            if (UnityEditor.EditorUtility.DisplayDialog(
                "确认清空",
                "确定要清空所有属性吗？此操作不可撤销。",
                "确定",
                "取消"))
            {
                Clear();
                Debug.Log("已清空所有属性");
            }
        }
#endif

        #endregion
    }
}

