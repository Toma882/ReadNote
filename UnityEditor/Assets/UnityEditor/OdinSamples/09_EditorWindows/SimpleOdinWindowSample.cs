#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace OdinSamples.EditorWindows
{
    /// <summary>
    /// OdinEditorWindow 简单示例
    /// 展示如何创建基础的 Odin 编辑器窗口
    /// </summary>
    public class SimpleOdinWindowSample : OdinEditorWindow
    {
        [MenuItem("Tools/Odin Samples/Windows/01 - 简单窗口")]
        private static void OpenWindow()
        {
            GetWindow<SimpleOdinWindowSample>().Show();
        }
        
        #region 基础字段
        
        [Title("基础字段")]
        [InfoBox("OdinEditorWindow 中可以使用所有 Odin 特性")]
        
        [LabelText("角色名称")]
        public string characterName = "勇者";
        
        [LabelText("等级")]
        [Range(1, 100)]
        public int level = 1;
        
        [LabelText("生命值")]
        [ProgressBar(0, 100, ColorGetter = "GetHealthColor")]
        public float health = 100f;
        
        private Color GetHealthColor(float value)
        {
            return Color.Lerp(Color.red, Color.green, value / 100f);
        }
        
        #endregion

        #region 按钮操作
        
        [Title("操作按钮")]
        
        [Button("保存数据", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void SaveData()
        {
            Debug.Log($"保存角色数据：{characterName} Lv.{level}");
        }
        
        [Button("加载数据", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.5f, 0.8f)]
        private void LoadData()
        {
            Debug.Log("加载角色数据");
        }
        
        [Button("重置", ButtonSizes.Large)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void Reset()
        {
            characterName = "勇者";
            level = 1;
            health = 100f;
            Debug.Log("数据已重置");
        }
        
        #endregion

        #region 列表数据
        
        [Title("列表数据")]
        
        [TableList(ShowIndexLabels = true, AlwaysExpanded = true)]
        public List<ItemData> items = new List<ItemData>
        {
            new ItemData { name = "生命药水", quantity = 5, price = 50 },
            new ItemData { name = "魔法药水", quantity = 3, price = 80 }
        };
        
        [System.Serializable]
        public class ItemData
        {
            [TableColumnWidth(120)]
            public string name;
            
            [TableColumnWidth(80)]
            public int quantity;
            
            [TableColumnWidth(80)]
            public int price;
        }
        
        #endregion
    }
}
#endif

