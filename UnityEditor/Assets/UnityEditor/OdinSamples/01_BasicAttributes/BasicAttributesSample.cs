using Sirenix.OdinInspector;
using UnityEngine;

namespace OdinSamples.BasicAttributes
{
    /*
        学习要点：
        ----------------------
        Title 特性用于在 Inspector 中显示分隔标题，让界面更清晰 
            参数
            string title: 标题文字
            string subtitle: 副标题文字
            TitleAlignments alignment: 标题对齐方式
        LabelText 可以修改字段在 Inspector 中显示的名称 用于修改字段在 Inspector 中显示的名称
            参数
            string label: 标签文字
        ReadOnly 让字段在 Inspector 中只读，不可编辑 
            参数
            bool readOnly: 是否只读
        DisplayAsString 将任何类型以字符串形式显示，完全只读 
            参数
            bool displayAsString: 是否以字符串形式显示
        InfoBox 信息提示框 用于显示信息提示文字
            参数
            string info: 信息提示文字
            InfoMessageType type: 信息提示类型
            string condition: 条件
        DetailedInfoBox 详细信息框 用于显示较长的说明文字
            参数
            string info: 详细信息提示文字
            string condition: 条件
        PropertyTooltip 属性提示 用于显示属性提示文字
            参数
            string tooltip: 属性提示文字
        MultiLineProperty 多行文本 用于显示多行文本
            参数
            int lines: 行数
        PropertySpace 间距控制 用于控制属性之间的间距
            参数
            float space: 间距
        Indent 缩进 用于控制属性之间的缩进
            参数
            int indent: 缩进
        PropertyOrder 属性顺序 用于控制属性之间的顺序
            参数
            int order: 顺序
        GUIColor GUI颜色 用于控制属性之间的颜色
            参数
            float r: 红色
            float g: 绿色
            float b: 蓝色
        ----------------------
    */


    /// <summary>
    /// Odin Inspector 基础属性演示
    /// 展示最常用的基础标注特性
    /// </summary>
    public class BasicAttributesSample : MonoBehaviour
    {
        #region Title 标题显示
        
        [Title("基础属性演示")]
        [InfoBox("Title 特性用于在 Inspector 中显示分隔标题，让界面更清晰")]
        public string titleExample = "这是一个带标题的区域";
        
        [Title("第二个标题", "可以添加副标题", TitleAlignments.Centered)]
        public int anotherSection = 42;
        
        #endregion

        #region LabelText 自定义标签
        
        [Title("自定义标签")]
        [LabelText("自定义的字段名称")]
        [InfoBox("LabelText 可以修改字段在 Inspector 中显示的名称")]
        public string customLabelField = "默认值";
        
        [LabelText("生命值 (HP)")]
        public float health = 100f;
        
        [LabelText("攻击力")]
        public int attackPower = 50;
        
        #endregion

        #region HideLabel 隐藏标签
        
        [Title("隐藏标签")]
        [InfoBox("HideLabel 可以隐藏字段标签，常用于大文本框或特殊布局")]
        
        [HideLabel]
        [TextArea(5, 10)]
        public string description = "这是一个隐藏了标签的大文本框\n可以输入多行文字";
        
        #endregion

        #region ReadOnly 只读显示
        
        [Title("只读字段")]
        [InfoBox("ReadOnly 让字段在 Inspector 中只读，不可编辑")]
        
        [ReadOnly]
        public string readOnlyText = "这个字段只能读取，不能修改";
        
        [ReadOnly]
        public float currentTime = 0f;
        
        void Update()
        {
            currentTime = Time.time; // 只读字段可以在代码中修改
        }
        
        #endregion

        #region DisplayAsString 字符串显示
        
        [Title("字符串显示")]
        [InfoBox("DisplayAsString 将任何类型以字符串形式显示，完全只读")]
        
        [DisplayAsString]  // 将Vector3以字符串形式显示
        public Vector3 position = new Vector3(1, 2, 3);
        
        [DisplayAsString]  // 将Color以字符串形式显示
        public Color color = Color.red;
        
        #endregion

        #region InfoBox 信息提示框
        
        [Title("信息提示框")]
        
        [InfoBox("这是一个普通的信息提示框")]
        public string normalInfo;
        
        [InfoBox("警告：这是一个警告信息", InfoMessageType.Warning)]
        public string warningInfo;
        
        [InfoBox("错误：这是一个错误信息", InfoMessageType.Error)]
        public string errorInfo;
        
        [InfoBox("这个值不能为空！", InfoMessageType.Error, "IsNameEmpty")]
        public string characterName = "";
        
        private bool IsNameEmpty()
        {
            return string.IsNullOrEmpty(characterName);
        }
        
        #endregion

        #region DetailedInfoBox 详细信息框
        
        [Title("详细信息框")]
        [DetailedInfoBox("简短说明", 
            "这是详细信息框，可以折叠显示更多内容\n" +
            "适合放置较长的说明文字\n" +
            "比如功能说明、使用提示等")]
        public string detailedExample;
        
        #endregion

        #region PropertyTooltip 属性提示
        
        [Title("属性提示")]
        [PropertyTooltip("鼠标悬停时会显示这段提示文字")]  
        public float tooltipExample = 3.14f;
        
        [PropertyTooltip("伤害值\n范围：0-999\n默认：100")]
        public int damage = 100;
        
        #endregion

        #region MultiLineProperty 多行文本
        
        [Title("多行文本")]
        [MultiLineProperty(5)]
        [InfoBox("MultiLineProperty 提供多行文本输入框")]
        public string multiLineText = "第一行\n第二行\n第三行";
        
        #endregion

        #region PropertySpace 间距控制
        
        [Title("间距控制")]
        [InfoBox("PropertySpace 可以在属性之间添加间距")]
        
        public string field1 = "正常间距";
        
        [PropertySpace(20)]
        public string field2 = "上方有20像素间距";
        
        [PropertySpace(10, 30)]
        public string field3 = "上方10像素，下方30像素间距";
        
        #endregion

        #region Indent 缩进
        
        [Title("缩进控制")]
        [InfoBox("Indent 可以控制字段的缩进级别")]
        
        public string noIndent = "无缩进";
        
        [Indent(1)]
        public string indent1 = "缩进1级";
        
        [Indent(2)]
        public string indent2 = "缩进2级";
        
        [Indent(3)]
        public string indent3 = "缩进3级";
        
        [Indent(-1)]
        public string outdent = "反向缩进（减少缩进）";
        
        #endregion

        #region PropertyOrder 属性顺序
        
        [Title("属性顺序")]
        [InfoBox("PropertyOrder 控制属性的显示顺序，数字越小越靠前")]
        
        [PropertyOrder(3)]
        public string third = "我是第三个";
        
        [PropertyOrder(1)]
        public string first = "我是第一个";
        
        [PropertyOrder(2)]
        public string second = "我是第二个";
        
        #endregion

        #region GUIColor GUI颜色
        
        [Title("GUI 颜色")]
        [InfoBox("GUIColor 可以改变字段的背景颜色")]
        
        [GUIColor(1, 0, 0)] // 红色
        public string redField = "红色背景";
        
        [GUIColor(0, 1, 0)] // 绿色
        public string greenField = "绿色背景";
        
        [GUIColor(0, 0, 1)] // 蓝色
        public string blueField = "蓝色背景";
        
        [GUIColor(1, 1, 0)] // 黄色
        public string yellowField = "黄色背景";
        
        [GUIColor(0.5f, 0.5f, 0.5f)] // 灰色
        public string grayField = "灰色背景";
        
        #endregion

        #region 组合使用示例
        
        [Title("组合使用示例", "多个特性可以叠加使用")]
        
        [LabelText("角色等级")]
        [PropertyTooltip("当前角色的等级")]
        [PropertySpace(10)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        [Range(1, 100)]
        public int characterLevel = 1;
        
        [LabelText("角色经验值")]
        [ReadOnly]
        [ProgressBar(0, 1000, ColorGetter = "GetExpColor")]
        [InfoBox("经验值会自动增长到1000", InfoMessageType.Info, "IsExpNotFull")]
        public float experience = 0f;
        
        private Color GetExpColor(float value)
        {
            return Color.Lerp(Color.red, Color.green, value / 1000f);
        }
        
        private bool IsExpNotFull()
        {
            return experience < 1000f;
        }
        
        #endregion

        #region 测试按钮
        
        [Title("测试按钮")]
        
        [Button("增加经验值", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void AddExperience()
        {
            experience = Mathf.Min(experience + 100f, 1000f);
            Debug.Log($"当前经验值：{experience}");
        }
        
        [Button("重置所有值")]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ResetValues()
        {
            characterLevel = 1;
            experience = 0f;
            currentTime = 0f;
            characterName = "";
            Debug.Log("所有值已重置");
        }
        
        #endregion
    }
}

