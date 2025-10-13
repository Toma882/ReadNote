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
