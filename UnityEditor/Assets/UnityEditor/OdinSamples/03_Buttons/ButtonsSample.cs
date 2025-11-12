using Sirenix.OdinInspector;
using UnityEngine;

namespace OdinSamples.Buttons
{
    /// <summary>
    /// Odin Inspector 按钮和方法调用演示
    /// 展示如何在 Inspector 中添加可执行按钮
    /// </summary>
    public class ButtonsSample : MonoBehaviour
    {
        /*
        学习要点：
        ----------------------
        Button 特性可以将方法转换为 Inspector 中的可点击按钮
            参数
            string name: 按钮名称
            ButtonSizes size: 按钮大小
        ----------------------
        ButtonGroup 特性可以将多个按钮排列在一行
            参数
            string name: 按钮组名称
        ButtonSizes 特性可以控制按钮大小
            参数
            ButtonSizes size: 按钮大小

        EnableIf 特性可以控制按钮是否启用
            参数
            string condition: 条件
        DisableIf 特性可以控制按钮是否禁用
            参数
            string condition: 条件
        ReadOnly 特性可以控制按钮是否只读
            参数
            bool readOnly: 是否只读
        ProgressBar 特性可以控制按钮是否显示进度条
            参数
            int minValue: 最小值
            int maxValue: 最大值
        FoldoutGroup 特性可以控制按钮是否显示折叠组
            参数
            string name: 折叠组名称
        HorizontalGroup 特性可以控制按钮是否显示水平组
           参数
            string name: 水平组名称
        VerticalGroup 特性可以控制按钮是否显示垂直组
           参数
            string name: 垂直组名称
        BoxGroup 特性可以控制按钮是否显示盒子组
           参数
            string name: 盒子组名称
        TabGroup 特性可以控制按钮是否显示标签组
           参数
            string name: 标签组名称
        ShowInInspector 特性可以控制按钮是否显示在Inspector中
           参数
            bool showInInspector: 是否显示在Inspector中
        GUIColor 特性可以控制按钮颜色
            参数
            float r: 红色
            float g: 绿色
            float b: 蓝色
        ----------------------
        */ 


        [Title("按钮演示")]
        [InfoBox("Button 特性可以将方法转换为 Inspector 中的可点击按钮")]
        
        #region 基础按钮
        
        [Title("基础按钮")]
        
        [Button("点击我")]
        private void SimpleButton()
        {
            Debug.Log("按钮被点击了！");
        }
        
        [Button]
        private void DefaultButtonName()
        {
            Debug.Log("不指定名称时，使用方法名作为按钮文字");
        }
        
        #endregion

        #region 按钮大小
        
        [Title("按钮大小")]
        
        [Button(ButtonSizes.Small)]
        private void SmallButton()
        {
            Debug.Log("小按钮");
        }
        
        [Button(ButtonSizes.Medium)]
        private void MediumButton()
        {
            Debug.Log("中等按钮");
        }
        
        [Button(ButtonSizes.Large)]
        private void LargeButton()
        {
            Debug.Log("大按钮");
        }
        
        [Button(ButtonSizes.Gigantic)]
        private void GiganticButton()
        {
            Debug.Log("超大按钮");
        }
        
        #endregion

        #region 按钮样式
        
        [Title("按钮样式")]
        
        [Button]
        [InfoBox("Button 特性可以设置不同的大小，但样式由 Odin 自动处理")]
        private void StyledButton()
        {
            Debug.Log("样式按钮");
        }
        
        #endregion

        #region GUIColor 按钮颜色
        
        [Title("按钮颜色")]
        
        [Button("红色按钮")]
        [GUIColor(1, 0, 0)]
        private void RedButton()
        {
            Debug.Log("红色按钮被点击");
        }
        
        [Button("绿色按钮")]
        [GUIColor(0, 1, 0)]
        private void GreenButton()
        {
            Debug.Log("绿色按钮被点击");
        }
        
        [Button("蓝色按钮")]
        [GUIColor(0, 0, 1)]
        private void BlueButton()
        {
            Debug.Log("蓝色按钮被点击");
        }
        
        [Button("黄色按钮", ButtonSizes.Large)]
        [GUIColor(1, 1, 0)]
        private void YellowButton()
        {
            Debug.Log("黄色大按钮被点击");
        }
        
        #endregion

        #region ButtonGroup 按钮组
        
        [Title("按钮组")]
        [InfoBox("ButtonGroup 可以将多个按钮排列在一行")]
        
        [ButtonGroup("Group1")]
        [Button("按钮1")]
        private void ButtonInGroup1()
        {
            Debug.Log("组1 - 按钮1");
        }
        
        [ButtonGroup("Group1")]
        [Button("按钮2")]
        private void ButtonInGroup2()
        {
            Debug.Log("组1 - 按钮2");
        }
        
        [ButtonGroup("Group1")]
        [Button("按钮3")]
        private void ButtonInGroup3()
        {
            Debug.Log("组1 - 按钮3");
        }
        
        [Title("不同大小的按钮组")]
        [InfoBox("使用 Button 特性的 ButtonSizes 参数来控制按钮大小")]
        
        [ButtonGroup("Group2")]
        [Button("大按钮1", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void LargeButtonInGroup1()
        {
            Debug.Log("大按钮组 - 按钮1");
        }
        
        [ButtonGroup("Group2")]
        [Button("大按钮2", ButtonSizes.Large)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void LargeButtonInGroup2()
        {
            Debug.Log("大按钮组 - 按钮2");
        }
        
        #endregion

        #region 带参数的按钮
        
        [Title("带参数的按钮")]
        [InfoBox("按钮方法可以带参数，参数会显示为输入框")]
        
        [Button("打印消息")]
        private void PrintMessage(string message)
        {
            Debug.Log($"消息：{message}");
        }
        
        [Button("计算和")]
        private void CalculateSum(int a, int b)
        {
            Debug.Log($"{a} + {b} = {a + b}");
        }
        
        [Button("设置位置")]
        private void SetPosition(Vector3 position)
        {
            transform.position = position;
            Debug.Log($"位置已设置为：{position}");
        }
        
        [Button("设置颜色")]
        private void SetColor(Color color)
        {
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
                Debug.Log($"颜色已设置为：{color}");
            }
        }
        
        #endregion

        #region 条件启用/禁用
        
        [Title("条件启用/禁用")]
        
        public bool enableButtons = true;
        
        [EnableIf("enableButtons")]
        [Button("条件启用的按钮")]
        [InfoBox("只有当 enableButtons 为 true 时才能点击")]
        private void ConditionallyEnabledButton()
        {
            Debug.Log("条件启用的按钮被点击");
        }
        
        [DisableIf("enableButtons")]
        [Button("条件禁用的按钮")]
        [InfoBox("只有当 enableButtons 为 false 时才能点击")]
        private void ConditionallyDisabledButton()
        {
            Debug.Log("条件禁用的按钮被点击");
        }
        
        #endregion

        #region 实用功能按钮
        
        [Title("实用功能按钮")]
        
        [FoldoutGroup("游戏对象操作")]
        [Button("激活游戏对象", ButtonSizes.Medium)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void ActivateGameObject()
        {
            gameObject.SetActive(true);
            Debug.Log("游戏对象已激活");
        }
        
        [FoldoutGroup("游戏对象操作")]
        [Button("停用游戏对象", ButtonSizes.Medium)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void DeactivateGameObject()
        {
            gameObject.SetActive(false);
            Debug.Log("游戏对象已停用");
        }
        
        [FoldoutGroup("游戏对象操作")]
        [Button("重置位置", ButtonSizes.Medium)]
        private void ResetPosition()
        {
            transform.position = Vector3.zero;
            Debug.Log("位置已重置为原点");
        }
        
        [FoldoutGroup("游戏对象操作")]
        [Button("重置旋转", ButtonSizes.Medium)]
        private void ResetRotation()
        {
            transform.rotation = Quaternion.identity;
            Debug.Log("旋转已重置");
        }
        
        [FoldoutGroup("游戏对象操作")]
        [Button("重置缩放", ButtonSizes.Medium)]
        private void ResetScale()
        {
            transform.localScale = Vector3.one;
            Debug.Log("缩放已重置");
        }
        
        #endregion

        #region 测试计数器
        
        [Title("测试计数器")]
        
        [ShowInInspector]
        [ReadOnly]
        [ProgressBar(0, 100)]
        private int clickCount = 0;
        
        [ButtonGroup("Counter")]
        [Button("增加计数")]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void IncrementCounter()
        {
            clickCount++;
            Debug.Log($"计数：{clickCount}");
        }
        
        [ButtonGroup("Counter")]
        [Button("减少计数")]
        [GUIColor(0.8f, 0.5f, 0.3f)]
        private void DecrementCounter()
        {
            clickCount = Mathf.Max(0, clickCount - 1);
            Debug.Log($"计数：{clickCount}");
        }
        
        [ButtonGroup("Counter")]
        [Button("重置计数")]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ResetCounter()
        {
            clickCount = 0;
            Debug.Log("计数已重置");
        }
        
        #endregion

        #region 组合示例
        
        [Title("组合示例")]
        [InfoBox("结合多个特性创建复杂的按钮布局")]
        
        [BoxGroup("操作面板")]
        [HorizontalGroup("操作面板/行1")]
        [Button("保存", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void Save()
        {
            Debug.Log("保存操作");
        }
        
        [HorizontalGroup("操作面板/行1")]
        [Button("加载", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.5f, 0.8f)]
        private void Load()
        {
            Debug.Log("加载操作");
        }
        
        [HorizontalGroup("操作面板/行1")]
        [Button("删除", ButtonSizes.Large)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void Delete()
        {
            Debug.Log("删除操作");
        }
        
        [HorizontalGroup("操作面板/行2")]
        [Button("导出")]
        private void Export()
        {
            Debug.Log("导出操作");
        }
        
        [HorizontalGroup("操作面板/行2")]
        [Button("导入")]
        private void Import()
        {
            Debug.Log("导入操作");
        }
        
        [HorizontalGroup("操作面板/行2")]
        [Button("刷新")]
        private void Refresh()
        {
            Debug.Log("刷新操作");
        }
        
        #endregion
    }
}

