using UnityEngine;
using System.Collections.Generic;

namespace UnityEditor.Examples
{
    /// <summary>
    /// GUIUtility 工具类示例
    /// 提供GUI相关的实用工具功能
    /// </summary>
    public static class GUIUtilityExample
    {
        #region 坐标转换示例

        /// <summary>
        /// 屏幕坐标转GUI坐标
        /// </summary>
        public static void ScreenToGUIPointExample()
        {
            Vector2 screenPoint = Input.mousePosition;
            Vector2 guiPoint = GUIUtility.ScreenToGUIPoint(screenPoint);
            
            Debug.Log($"屏幕坐标: {screenPoint} -> GUI坐标: {guiPoint}");
        }

        /// <summary>
        /// GUI坐标转屏幕坐标
        /// </summary>
        public static void GUIToScreenPointExample()
        {
            Vector2 guiPoint = new Vector2(100, 100);
            Vector2 screenPoint = GUIUtility.GUIToScreenPoint(guiPoint);
            
            Debug.Log($"GUI坐标: {guiPoint} -> 屏幕坐标: {screenPoint}");
        }

        /// <summary>
        /// 批量坐标转换
        /// </summary>
        public static void BatchCoordinateConversionExample()
        {
            Vector2[] screenPoints = {
                new Vector2(0, 0),
                new Vector2(Screen.width / 2, Screen.height / 2),
                new Vector2(Screen.width, Screen.height)
            };

            foreach (Vector2 screenPoint in screenPoints)
            {
                Vector2 guiPoint = GUIUtility.ScreenToGUIPoint(screenPoint);
                Vector2 backToScreen = GUIUtility.GUIToScreenPoint(guiPoint);
                
                Debug.Log($"屏幕: {screenPoint} -> GUI: {guiPoint} -> 屏幕: {backToScreen}");
            }
        }

        #endregion

        #region 控件ID示例

        /// <summary>
        /// 获取控件ID
        /// </summary>
        public static void GetControlIDExample()
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            Debug.Log($"控件ID: {controlID}");
        }

        /// <summary>
        /// 获取控件ID（带焦点类型）
        /// </summary>
        public static void GetControlIDWithFocusExample()
        {
            int keyboardControlID = GUIUtility.GetControlID(FocusType.Keyboard);
            int mouseControlID = GUIUtility.GetControlID(FocusType.Mouse);
            
            Debug.Log($"键盘焦点控件ID: {keyboardControlID}");
            Debug.Log($"鼠标焦点控件ID: {mouseControlID}");
        }

        /// <summary>
        /// 获取控件ID（带名称）
        /// </summary>
        public static void GetControlIDWithNameExample()
        {
            int controlID = GUIUtility.GetControlID("MyControl", FocusType.Passive);
            Debug.Log($"命名控件ID: {controlID}");
        }

        #endregion

        #region 键盘控制示例

        /// <summary>
        /// 键盘控制操作
        /// </summary>
        public static void KeyboardControlExample()
        {
            int currentControl = GUIUtility.keyboardControl;
            Debug.Log($"当前键盘控制: {currentControl}");

            // 设置键盘控制
            GUIUtility.keyboardControl = 0;
            Debug.Log($"键盘控制已设置为: {GUIUtility.keyboardControl}");
        }

        /// <summary>
        /// 检查是否有键盘焦点
        /// </summary>
        public static void HasKeyboardFocusExample()
        {
            bool hasFocus = GUIUtility.HasKeyboardFocus(0);
            Debug.Log($"控件0是否有键盘焦点: {hasFocus}");
        }

        #endregion

        #region GUI状态示例

        /// <summary>
        /// GUI启用状态
        /// </summary>
        public static void GUIEnabledExample()
        {
            bool originalEnabled = GUI.enabled;
            Debug.Log($"原始GUI启用状态: {originalEnabled}");

            // 禁用GUI
            GUI.enabled = false;
            Debug.Log($"GUI已禁用: {GUI.enabled}");

            // 恢复原始状态
            GUI.enabled = originalEnabled;
            Debug.Log($"GUI状态已恢复: {GUI.enabled}");
        }

        /// <summary>
        /// GUI颜色状态
        /// </summary>
        public static void GUIColorExample()
        {
            Color originalColor = GUI.color;
            Debug.Log($"原始GUI颜色: {originalColor}");

            // 设置新颜色
            GUI.color = Color.red;
            Debug.Log($"GUI颜色已设置为: {GUI.color}");

            // 恢复原始颜色
            GUI.color = originalColor;
            Debug.Log($"GUI颜色已恢复: {GUI.color}");
        }

        #endregion

        #region GUI矩阵示例

        /// <summary>
        /// GUI矩阵操作
        /// </summary>
        public static void GUIMatrixExample()
        {
            Matrix4x4 originalMatrix = GUI.matrix;
            Debug.Log($"原始GUI矩阵: {originalMatrix}");

            // 创建变换矩阵
            Matrix4x4 scaleMatrix = Matrix4x4.Scale(new Vector3(2, 2, 1));
            Matrix4x4 translateMatrix = Matrix4x4.Translate(new Vector3(100, 100, 0));
            Matrix4x4 combinedMatrix = translateMatrix * scaleMatrix;

            GUI.matrix = combinedMatrix;
            Debug.Log($"GUI矩阵已设置: {GUI.matrix}");

            // 恢复原始矩阵
            GUI.matrix = originalMatrix;
            Debug.Log($"GUI矩阵已恢复: {GUI.matrix}");
        }

        /// <summary>
        /// 矩阵变换示例
        /// </summary>
        public static void MatrixTransformExample()
        {
            Vector2 originalPoint = new Vector2(50, 50);
            
            // 应用缩放变换
            Matrix4x4 scaleMatrix = Matrix4x4.Scale(new Vector3(2, 2, 1));
            Vector2 scaledPoint = scaleMatrix.MultiplyPoint3x4(originalPoint);
            
            Debug.Log($"原始点: {originalPoint} -> 缩放后: {scaledPoint}");
        }

        #endregion

        #region GUI工具提示示例

        /// <summary>
        /// 工具提示操作
        /// </summary>
        public static void TooltipExample()
        {
            string tooltip = GUI.tooltip;
            Debug.Log($"当前工具提示: {tooltip}");
        }

        /// <summary>
        /// 设置工具提示
        /// </summary>
        public static void SetTooltipExample()
        {
            GUI.tooltip = "这是一个工具提示";
            Debug.Log($"工具提示已设置: {GUI.tooltip}");
        }

        #endregion

        #region GUI深度示例

        /// <summary>
        /// GUI深度操作
        /// </summary>
        public static void GUIDepthExample()
        {
            int originalDepth = GUI.depth;
            Debug.Log($"原始GUI深度: {originalDepth}");

            // 设置新深度
            GUI.depth = 1;
            Debug.Log($"GUI深度已设置为: {GUI.depth}");

            // 恢复原始深度
            GUI.depth = originalDepth;
            Debug.Log($"GUI深度已恢复: {GUI.depth}");
        }

        #endregion

        #region GUI皮肤示例

        /// <summary>
        /// GUI皮肤操作
        /// </summary>
        public static void GUISkinExample()
        {
            GUISkin originalSkin = GUI.skin;
            Debug.Log($"原始GUI皮肤: {originalSkin.name}");

            // 获取默认皮肤
            GUISkin defaultSkin = Resources.GetBuiltinResource<GUISkin>("DefaultSkin");
            if (defaultSkin != null)
            {
                GUI.skin = defaultSkin;
                Debug.Log($"GUI皮肤已设置为: {GUI.skin.name}");
            }

            // 恢复原始皮肤
            GUI.skin = originalSkin;
            Debug.Log($"GUI皮肤已恢复: {GUI.skin.name}");
        }

        #endregion

        #region GUI状态管理示例

        /// <summary>
        /// 获取GUI状态对象
        /// </summary>
        public static void GetStateObjectExample()
        {
            // 获取或创建状态对象
            object stateObject = GUIUtility.GetStateObject(typeof(Dictionary<string, object>), 123);
            Debug.Log($"状态对象: {stateObject.GetType().Name}");
        }

        /// <summary>
        /// 查询GUI状态对象
        /// </summary>
        public static void QueryStateObjectExample()
        {
            // 查询状态对象
            object stateObject = GUIUtility.QueryStateObject(typeof(List<string>), 456);
            Debug.Log($"查询状态对象: {(stateObject != null ? "找到" : "未找到")}");
        }

        /// <summary>
        /// 获取永久控制ID
        /// </summary>
        public static void GetPermanentControlIDExample()
        {
            int controlID = GUIUtility.GetPermanentControlID();
            Debug.Log($"永久控制ID: {controlID}");
        }

        /// <summary>
        /// 设置键盘控制
        /// </summary>
        public static void SetKeyboardControlExample()
        {
            int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
            GUIUtility.keyboardControl = controlID;
            Debug.Log($"设置键盘控制ID: {controlID}");
        }

        #endregion

        #region GUI变换示例

        /// <summary>
        /// 围绕轴心旋转
        /// </summary>
        public static void RotateAroundPivotExample()
        {
            Vector2 pivot = new Vector2(100, 100);
            float angle = 45f;
            
            Matrix4x4 matrix = GUIUtility.RotateAroundPivot(angle, pivot);
            Debug.Log($"旋转矩阵: {matrix}");
        }

        /// <summary>
        /// 围绕轴心缩放
        /// </summary>
        public static void ScaleAroundPivotExample()
        {
            Vector2 pivot = new Vector2(100, 100);
            Vector2 scale = new Vector2(2f, 2f);
            
            Matrix4x4 matrix = GUIUtility.ScaleAroundPivot(scale, pivot);
            Debug.Log($"缩放矩阵: {matrix}");
        }

        /// <summary>
        /// 组合变换
        /// </summary>
        public static void CombinedTransformExample()
        {
            Vector2 pivot = new Vector2(100, 100);
            
            // 先旋转
            Matrix4x4 rotationMatrix = GUIUtility.RotateAroundPivot(30f, pivot);
            
            // 再缩放
            Matrix4x4 scaleMatrix = GUIUtility.ScaleAroundPivot(new Vector2(1.5f, 1.5f), pivot);
            
            // 组合变换
            Matrix4x4 combinedMatrix = scaleMatrix * rotationMatrix;
            Debug.Log($"组合变换矩阵: {combinedMatrix}");
        }

        #endregion

        #region GUI事件处理示例

        /// <summary>
        /// 处理GUI事件
        /// </summary>
        public static void HandleGUIEventExample()
        {
            Event currentEvent = Event.current;
            if (currentEvent != null)
            {
                Debug.Log($"当前GUI事件: {currentEvent.type}");
                
                if (currentEvent.type == EventType.MouseDown)
                {
                    Debug.Log($"鼠标按下位置: {currentEvent.mousePosition}");
                }
            }
        }

        /// <summary>
        /// 检查GUI事件类型
        /// </summary>
        public static void CheckGUIEventTypeExample()
        {
            Event currentEvent = Event.current;
            if (currentEvent != null)
            {
                bool isMouseEvent = currentEvent.type == EventType.MouseDown || 
                                  currentEvent.type == EventType.MouseUp || 
                                  currentEvent.type == EventType.MouseMove;
                
                bool isKeyboardEvent = currentEvent.type == EventType.KeyDown || 
                                     currentEvent.type == EventType.KeyUp;
                
                Debug.Log($"是否鼠标事件: {isMouseEvent}, 是否键盘事件: {isKeyboardEvent}");
            }
        }

        #endregion

        #region GUI布局示例

        /// <summary>
        /// GUI布局计算
        /// </summary>
        public static void GUILayoutCalculationExample()
        {
            // 计算布局矩形
            Rect layoutRect = GUILayoutUtility.GetRect(100, 50);
            Debug.Log($"布局矩形: {layoutRect}");
            
            // 获取最后使用的矩形
            Rect lastRect = GUILayoutUtility.GetLastRect();
            Debug.Log($"最后矩形: {lastRect}");
        }

        /// <summary>
        /// GUI区域管理
        /// </summary>
        public static void GUIAreaManagementExample()
        {
            Rect areaRect = new Rect(10, 10, 200, 100);
            
            // 开始GUI区域
            GUILayout.BeginArea(areaRect);
            Debug.Log("GUI区域已开始");
            
            // 结束GUI区域
            GUILayout.EndArea();
            Debug.Log("GUI区域已结束");
        }

        #endregion

        #region GUI样式示例

        /// <summary>
        /// 获取GUI样式
        /// </summary>
        public static void GetGUIStyleExample()
        {
            GUIStyle labelStyle = GUI.skin.label;
            Debug.Log($"标签样式: {labelStyle.name}");
            
            GUIStyle buttonStyle = GUI.skin.button;
            Debug.Log($"按钮样式: {buttonStyle.name}");
        }

        /// <summary>
        /// 设置GUI样式
        /// </summary>
        public static void SetGUIStyleExample()
        {
            GUIStyle customStyle = new GUIStyle();
            customStyle.normal.textColor = Color.blue;
            customStyle.fontSize = 16;
            customStyle.fontStyle = FontStyle.Bold;
            
            Debug.Log("自定义GUI样式已设置");
        }

        #endregion

        #region GUI调试示例

        /// <summary>
        /// GUI调试信息
        /// </summary>
        public static void GUIDebugInfoExample()
        {
            Debug.Log($"当前GUI深度: {GUI.depth}");
            Debug.Log($"GUI是否启用: {GUI.enabled}");
            Debug.Log($"GUI是否改变: {GUI.changed}");
        }

        /// <summary>
        /// GUI状态重置
        /// </summary>
        public static void GUIStateResetExample()
        {
            GUI.changed = false;
            Debug.Log("GUI状态已重置");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建自定义GUI控件
        /// </summary>
        public static void CreateCustomGUIControlExample()
        {
            int controlID = GUIUtility.GetControlID("CustomButton", FocusType.Passive);
            
            // 检查鼠标事件
            Event currentEvent = Event.current;
            if (currentEvent.type == EventType.MouseDown && 
                currentEvent.button == 0)
            {
                Debug.Log($"自定义控件 {controlID} 被点击");
            }
        }

        /// <summary>
        /// GUI状态管理
        /// </summary>
        public static void GUIStateManagementExample()
        {
            // 保存当前状态
            bool originalEnabled = GUI.enabled;
            Color originalColor = GUI.color;
            Matrix4x4 originalMatrix = GUI.matrix;
            int originalDepth = GUI.depth;

            Debug.Log("GUI状态已保存");

            // 修改状态
            GUI.enabled = false;
            GUI.color = Color.red;
            GUI.depth = 1;

            Debug.Log("GUI状态已修改");

            // 恢复状态
            GUI.enabled = originalEnabled;
            GUI.color = originalColor;
            GUI.matrix = originalMatrix;
            GUI.depth = originalDepth;

            Debug.Log("GUI状态已恢复");
        }

        /// <summary>
        /// 坐标系统转换
        /// </summary>
        public static void CoordinateSystemConversionExample()
        {
            // 世界坐标 -> 屏幕坐标 -> GUI坐标
            Vector3 worldPoint = Camera.main.transform.position;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);
            Vector2 guiPoint = GUIUtility.ScreenToGUIPoint(screenPoint);

            Debug.Log($"世界坐标: {worldPoint}");
            Debug.Log($"屏幕坐标: {screenPoint}");
            Debug.Log($"GUI坐标: {guiPoint}");

            // 反向转换
            Vector2 backToScreen = GUIUtility.GUIToScreenPoint(guiPoint);
            Vector3 backToWorld = Camera.main.ScreenToWorldPoint(backToScreen);

            Debug.Log($"反向屏幕坐标: {backToScreen}");
            Debug.Log($"反向世界坐标: {backToWorld}");
        }

        #endregion
    }
}
