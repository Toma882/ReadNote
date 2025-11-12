using UnityEngine;
using UnityEditor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// GUILayoutUtility 工具类示例
    /// 提供GUI布局相关的实用工具功能
    /// </summary>
    public static class GUILayoutUtilityExample
    {
        #region 矩形获取示例

        /// <summary>
        /// 获取矩形
        /// </summary>
        public static void GetRectExample()
        {
            // 获取固定大小的矩形
            Rect rect = GUILayoutUtility.GetRect(200, 100);
            Debug.Log($"获取矩形: {rect}");
            
            // 绘制矩形
            GUI.Box(rect, "测试矩形");
        }

        /// <summary>
        /// 获取矩形（带选项）
        /// </summary>
        public static void GetRectWithOptionsExample()
        {
            GUILayoutOption[] options = {
                GUILayout.Width(150),
                GUILayout.Height(80)
            };
            
            Rect rect = GUILayoutUtility.GetRect(150, 80, options);
            Debug.Log($"获取矩形（带选项）: {rect}");
            
            GUI.Box(rect, "带选项的矩形");
        }

        /// <summary>
        /// 获取最后矩形
        /// </summary>
        public static void GetLastRectExample()
        {
            // 先绘制一个控件
            GUILayout.Button("测试按钮");
            
            // 获取最后绘制的矩形
            Rect lastRect = GUILayoutUtility.GetLastRect();
            Debug.Log($"最后矩形: {lastRect}");
            
            // 在最后矩形上绘制边框
            GUI.Box(lastRect, "");
        }

        #endregion

        #region 布局选项示例

        /// <summary>
        /// 宽度选项
        /// </summary>
        public static void WidthOptionsExample()
        {
            // 固定宽度
            GUILayout.Button("固定宽度", GUILayout.Width(200));
            
            // 最小宽度
            GUILayout.Button("最小宽度", GUILayout.MinWidth(100));
            
            // 最大宽度
            GUILayout.Button("最大宽度", GUILayout.MaxWidth(300));
            
            // 扩展宽度
            GUILayout.Button("扩展宽度", GUILayout.ExpandWidth(true));
        }

        /// <summary>
        /// 高度选项
        /// </summary>
        public static void HeightOptionsExample()
        {
            // 固定高度
            GUILayout.Button("固定高度", GUILayout.Height(50));
            
            // 最小高度
            GUILayout.Button("最小高度", GUILayout.MinHeight(30));
            
            // 最大高度
            GUILayout.Button("最大高度", GUILayout.MaxHeight(100));
            
            // 扩展高度
            GUILayout.Button("扩展高度", GUILayout.ExpandHeight(true));
        }

        /// <summary>
        /// 组合选项
        /// </summary>
        public static void CombinedOptionsExample()
        {
            GUILayoutOption[] options = {
                GUILayout.Width(150),
                GUILayout.Height(40),
                GUILayout.ExpandWidth(false),
                GUILayout.ExpandHeight(false)
            };
            
            GUILayout.Button("组合选项", options);
        }

        #endregion

        #region 布局组示例

        /// <summary>
        /// 水平布局组
        /// </summary>
        public static void HorizontalLayoutGroupExample()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Button("按钮1");
            GUILayout.Button("按钮2");
            GUILayout.Button("按钮3");
            
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 垂直布局组
        /// </summary>
        public static void VerticalLayoutGroupExample()
        {
            GUILayout.BeginVertical();
            
            GUILayout.Button("按钮1");
            GUILayout.Button("按钮2");
            GUILayout.Button("按钮3");
            
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 嵌套布局组
        /// </summary>
        public static void NestedLayoutGroupExample()
        {
            GUILayout.BeginVertical();
            
            GUILayout.Label("垂直组");
            
            GUILayout.BeginHorizontal();
            GUILayout.Button("水平1");
            GUILayout.Button("水平2");
            GUILayout.EndHorizontal();
            
            GUILayout.Button("垂直按钮");
            
            GUILayout.EndVertical();
        }

        #endregion

        #region 空间和分隔符示例

        /// <summary>
        /// 灵活空间
        /// </summary>
        public static void FlexibleSpaceExample()
        {
            GUILayout.BeginVertical();
            
            GUILayout.Button("顶部按钮");
            
            GUILayout.FlexibleSpace();
            
            GUILayout.Button("底部按钮");
            
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 固定空间
        /// </summary>
        public static void FixedSpaceExample()
        {
            GUILayout.BeginVertical();
            
            GUILayout.Button("按钮1");
            GUILayout.Space(20);
            GUILayout.Button("按钮2");
            GUILayout.Space(10);
            GUILayout.Button("按钮3");
            
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 分隔符
        /// </summary>
        public static void SeparatorExample()
        {
            GUILayout.BeginVertical();
            
            GUILayout.Button("按钮1");
            GUILayout.Separator();
            GUILayout.Button("按钮2");
            GUILayout.Separator();
            GUILayout.Button("按钮3");
            
            GUILayout.EndVertical();
        }

        #endregion

        #region 区域示例

        /// <summary>
        /// 滚动区域
        /// </summary>
        public static void ScrollAreaExample()
        {
            Vector2 scrollPosition = Vector2.zero;
            
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            
            for (int i = 0; i < 20; i++)
            {
                GUILayout.Button($"滚动按钮 {i}");
            }
            
            GUILayout.EndScrollView();
        }

        /// <summary>
        /// 固定滚动区域
        /// </summary>
        public static void FixedScrollAreaExample()
        {
            Vector2 scrollPosition = Vector2.zero;
            
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
            
            for (int i = 0; i < 20; i++)
            {
                GUILayout.Button($"固定滚动按钮 {i}");
            }
            
            GUILayout.EndScrollView();
        }

        #endregion

        #region 自定义控件示例

        /// <summary>
        /// 自定义按钮
        /// </summary>
        public static void CustomButtonExample()
        {
            Rect buttonRect = GUILayoutUtility.GetRect(100, 30);
            
            if (GUI.Button(buttonRect, "自定义按钮"))
            {
                Debug.Log("自定义按钮被点击");
            }
        }

        /// <summary>
        /// 自定义标签
        /// </summary>
        public static void CustomLabelExample()
        {
            Rect labelRect = GUILayoutUtility.GetRect(200, 20);
            
            GUI.Label(labelRect, "自定义标签");
        }

        /// <summary>
        /// 自定义文本框
        /// </summary>
        public static void CustomTextFieldExample()
        {
            Rect textFieldRect = GUILayoutUtility.GetRect(200, 20);
            
            string text = GUI.TextField(textFieldRect, "自定义文本框");
            Debug.Log($"文本框内容: {text}");
        }

        #endregion

        #region 布局计算示例

        /// <summary>
        /// 布局计算
        /// </summary>
        public static void LayoutCalculationExample()
        {
            // 计算按钮大小
            Rect buttonRect = GUILayoutUtility.GetRect(150, 40);
            Debug.Log($"按钮矩形: {buttonRect}");
            
            // 计算标签大小
            Rect labelRect = GUILayoutUtility.GetRect(200, 20);
            Debug.Log($"标签矩形: {labelRect}");
            
            // 计算总布局大小
            Rect totalRect = new Rect(
                Mathf.Min(buttonRect.x, labelRect.x),
                Mathf.Min(buttonRect.y, labelRect.y),
                Mathf.Max(buttonRect.xMax, labelRect.xMax) - Mathf.Min(buttonRect.x, labelRect.x),
                Mathf.Max(buttonRect.yMax, labelRect.yMax) - Mathf.Min(buttonRect.y, labelRect.y)
            );
            
            Debug.Log($"总布局矩形: {totalRect}");
        }

        /// <summary>
        /// 动态布局
        /// </summary>
        public static void DynamicLayoutExample()
        {
            float width = 100 + Mathf.Sin(Time.time) * 50;
            float height = 30 + Mathf.Cos(Time.time) * 20;
            
            Rect dynamicRect = GUILayoutUtility.GetRect(width, height);
            
            GUI.Box(dynamicRect, "动态布局");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建复杂布局
        /// </summary>
        public static void CreateComplexLayoutExample()
        {
            GUILayout.BeginVertical("box");
            
            // 标题
            GUILayout.Label("复杂布局示例", EditorStyles.boldLabel);
            GUILayout.Separator();
            
            // 水平组
            GUILayout.BeginHorizontal();
            GUILayout.Label("名称:", GUILayout.Width(50));
            string name = GUILayout.TextField("默认名称");
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // 按钮组
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("确定"))
            {
                Debug.Log($"确定按钮被点击，名称: {name}");
            }
            if (GUILayout.Button("取消"))
            {
                Debug.Log("取消按钮被点击");
            }
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 响应式布局
        /// </summary>
        public static void ResponsiveLayoutExample()
        {
            float screenWidth = Screen.width;
            float buttonWidth = screenWidth > 800 ? 200 : 100;
            
            GUILayout.BeginHorizontal();
            
            GUILayout.Button("响应式按钮1", GUILayout.Width(buttonWidth));
            GUILayout.Button("响应式按钮2", GUILayout.Width(buttonWidth));
            
            GUILayout.EndHorizontal();
        }

        #endregion
    }
}
