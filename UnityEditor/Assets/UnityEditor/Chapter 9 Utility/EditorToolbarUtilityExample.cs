using UnityEngine;
using UnityEditor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// EditorToolbarUtility 工具类示例
    /// 提供编辑器工具栏相关的实用工具功能
    /// </summary>
    public static class EditorToolbarUtilityExample
    {
        #region 工具栏矩形示例

        /// <summary>
        /// 获取工具栏矩形
        /// </summary>
        public static void GetToolbarRectExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            Debug.Log($"工具栏矩形: {toolbarRect}");
        }

        /// <summary>
        /// 获取工具栏矩形（带验证）
        /// </summary>
        public static void GetToolbarRectWithValidationExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            if (toolbarRect.width > 0 && toolbarRect.height > 0)
            {
                Debug.Log($"工具栏矩形有效: {toolbarRect}");
                Debug.Log($"工具栏位置: {toolbarRect.position}");
                Debug.Log($"工具栏大小: {toolbarRect.size}");
            }
            else
            {
                Debug.LogWarning("工具栏矩形无效");
            }
        }

        #endregion

        #region 工具栏操作示例

        /// <summary>
        /// 检查工具栏状态
        /// </summary>
        public static void CheckToolbarStateExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            Debug.Log($"工具栏状态:");
            Debug.Log($"- 矩形: {toolbarRect}");
            Debug.Log($"- 宽度: {toolbarRect.width}");
            Debug.Log($"- 高度: {toolbarRect.height}");
            Debug.Log($"- X坐标: {toolbarRect.x}");
            Debug.Log($"- Y坐标: {toolbarRect.y}");
        }

        /// <summary>
        /// 工具栏位置信息
        /// </summary>
        public static void ToolbarPositionInfoExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            Debug.Log($"工具栏位置信息:");
            Debug.Log($"- 左上角: {toolbarRect.min}");
            Debug.Log($"- 右下角: {toolbarRect.max}");
            Debug.Log($"- 中心点: {toolbarRect.center}");
            Debug.Log($"- 面积: {toolbarRect.width * toolbarRect.height}");
        }

        #endregion

        #region 工具栏绘制示例

        /// <summary>
        /// 在工具栏上绘制
        /// </summary>
        public static void DrawOnToolbarExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            // 在工具栏上绘制一个简单的矩形
            GUI.Box(toolbarRect, "工具栏测试");
        }

        /// <summary>
        /// 工具栏按钮示例
        /// </summary>
        public static void ToolbarButtonExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            // 在工具栏上绘制按钮
            if (GUI.Button(toolbarRect, "工具栏按钮"))
            {
                Debug.Log("工具栏按钮被点击");
            }
        }

        #endregion

        #region 工具栏布局示例

        /// <summary>
        /// 工具栏布局计算
        /// </summary>
        public static void ToolbarLayoutCalculationExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            // 计算工具栏布局
            float buttonWidth = 80;
            float buttonHeight = toolbarRect.height - 4;
            float spacing = 2;
            
            int buttonCount = Mathf.FloorToInt(toolbarRect.width / (buttonWidth + spacing));
            
            Debug.Log($"工具栏布局计算:");
            Debug.Log($"- 工具栏宽度: {toolbarRect.width}");
            Debug.Log($"- 按钮宽度: {buttonWidth}");
            Debug.Log($"- 按钮高度: {buttonHeight}");
            Debug.Log($"- 间距: {spacing}");
            Debug.Log($"- 可容纳按钮数量: {buttonCount}");
        }

        /// <summary>
        /// 工具栏按钮布局
        /// </summary>
        public static void ToolbarButtonLayoutExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            // 计算按钮布局
            float buttonWidth = 80;
            float buttonHeight = toolbarRect.height - 4;
            float spacing = 2;
            
            for (int i = 0; i < 5; i++)
            {
                Rect buttonRect = new Rect(
                    toolbarRect.x + i * (buttonWidth + spacing) + spacing,
                    toolbarRect.y + 2,
                    buttonWidth,
                    buttonHeight
                );
                
                if (GUI.Button(buttonRect, $"按钮{i}"))
                {
                    Debug.Log($"按钮 {i} 被点击");
                }
            }
        }

        #endregion

        #region 工具栏事件示例

        /// <summary>
        /// 工具栏事件处理
        /// </summary>
        public static void ToolbarEventHandlerExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            Event currentEvent = Event.current;
            
            if (currentEvent.type == EventType.MouseDown && 
                toolbarRect.Contains(currentEvent.mousePosition))
            {
                Debug.Log($"工具栏被点击: {currentEvent.mousePosition}");
            }
        }

        /// <summary>
        /// 工具栏键盘事件
        /// </summary>
        public static void ToolbarKeyboardEventExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            Event currentEvent = Event.current;
            
            if (currentEvent.type == EventType.KeyDown)
            {
                switch (currentEvent.keyCode)
                {
                    case KeyCode.F1:
                        Debug.Log("F1键被按下 - 工具栏帮助");
                        break;
                    case KeyCode.F2:
                        Debug.Log("F2键被按下 - 工具栏设置");
                        break;
                    case KeyCode.F3:
                        Debug.Log("F3键被按下 - 工具栏工具");
                        break;
                }
            }
        }

        #endregion

        #region 工具栏样式示例

        /// <summary>
        /// 工具栏样式设置
        /// </summary>
        public static void ToolbarStyleExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            // 使用不同的样式绘制工具栏
            GUIStyle toolbarStyle = new GUIStyle(GUI.skin.box);
            toolbarStyle.normal.background = EditorGUIUtility.FindTexture("d_Toolbar");
            
            GUI.Box(toolbarRect, "", toolbarStyle);
        }

        /// <summary>
        /// 工具栏颜色设置
        /// </summary>
        public static void ToolbarColorExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            // 保存原始颜色
            Color originalColor = GUI.color;
            
            // 设置工具栏颜色
            GUI.color = Color.cyan;
            GUI.Box(toolbarRect, "彩色工具栏");
            
            // 恢复原始颜色
            GUI.color = originalColor;
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建自定义工具栏
        /// </summary>
        public static void CreateCustomToolbarExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            // 创建自定义工具栏
            GUILayout.BeginArea(toolbarRect);
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("新建", EditorStyles.toolbarButton))
            {
                Debug.Log("新建按钮被点击");
            }
            
            if (GUILayout.Button("打开", EditorStyles.toolbarButton))
            {
                Debug.Log("打开按钮被点击");
            }
            
            if (GUILayout.Button("保存", EditorStyles.toolbarButton))
            {
                Debug.Log("保存按钮被点击");
            }
            
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("设置", EditorStyles.toolbarButton))
            {
                Debug.Log("设置按钮被点击");
            }
            
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 工具栏工具提示
        /// </summary>
        public static void ToolbarTooltipExample()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            
            // 计算按钮位置
            float buttonWidth = 80;
            float buttonHeight = toolbarRect.height - 4;
            
            Rect buttonRect = new Rect(toolbarRect.x + 2, toolbarRect.y + 2, buttonWidth, buttonHeight);
            
            // 绘制带工具提示的按钮
            if (GUI.Button(buttonRect, new GUIContent("工具", "这是一个工具按钮")))
            {
                Debug.Log("工具按钮被点击");
            }
            
            // 显示工具提示
            if (buttonRect.Contains(Event.current.mousePosition))
            {
                GUI.tooltip = "这是一个工具按钮";
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查工具栏是否可见
        /// </summary>
        private static bool IsToolbarVisible()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            return toolbarRect.width > 0 && toolbarRect.height > 0;
        }

        /// <summary>
        /// 获取工具栏中心点
        /// </summary>
        private static Vector2 GetToolbarCenter()
        {
            Rect toolbarRect = EditorToolbarUtility.GetToolbarRect();
            return toolbarRect.center;
        }

        #endregion
    }
}
