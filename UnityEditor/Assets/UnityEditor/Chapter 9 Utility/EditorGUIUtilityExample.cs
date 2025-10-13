using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace UnityEditor.Examples
{
    /// <summary>
    /// EditorGUIUtility 工具类示例
    /// 提供编辑器GUI相关的实用工具功能
    /// </summary>
    public static class EditorGUIUtilityExample
    {
        #region 对象内容示例

        /// <summary>
        /// 获取对象内容
        /// </summary>
        public static void ObjectContentExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                GUIContent content = EditorGUIUtility.ObjectContent(selected, typeof(GameObject));
                Debug.Log($"对象内容 - 文本: {content.text}, 图片: {content.image}");
            }
        }

        /// <summary>
        /// 获取对象内容（指定类型）
        /// </summary>
        public static void ObjectContentWithTypeExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                GUIContent content = EditorGUIUtility.ObjectContent(selected, typeof(Transform));
                Debug.Log($"Transform内容 - 文本: {content.text}");
            }
        }

        #endregion

        #region 图标操作示例

        /// <summary>
        /// 设置图标大小
        /// </summary>
        public static void SetIconSizeExample()
        {
            // 设置图标大小为32x32
            EditorGUIUtility.SetIconSize(new Vector2(32, 32));
            Debug.Log("图标大小已设置为32x32");
        }

        /// <summary>
        /// 获取图标大小
        /// </summary>
        public static void GetIconSizeExample()
        {
            Vector2 iconSize = EditorGUIUtility.GetIconSize();
            Debug.Log($"当前图标大小: {iconSize}");
        }

        /// <summary>
        /// 查找纹理
        /// </summary>
        public static void FindTextureExample()
        {
            Texture2D texture = EditorGUIUtility.FindTexture("d_GameObject Icon");
            if (texture != null)
            {
                Debug.Log($"找到纹理: {texture.name}");
            }
            else
            {
                Debug.Log("未找到指定纹理");
            }
        }

        #endregion

        #region 资源加载示例

        /// <summary>
        /// 加载必需资源
        /// </summary>
        public static void LoadRequiredExample()
        {
            Texture2D texture = EditorGUIUtility.LoadRequired("d_GameObject Icon") as Texture2D;
            if (texture != null)
            {
                Debug.Log($"必需资源已加载: {texture.name}");
            }
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        public static void LoadExample()
        {
            Texture2D texture = EditorGUIUtility.Load("d_GameObject Icon") as Texture2D;
            if (texture != null)
            {
                Debug.Log($"资源已加载: {texture.name}");
            }
        }

        #endregion

        #region 系统缓冲区示例

        /// <summary>
        /// 系统复制缓冲区操作
        /// </summary>
        public static void SystemCopyBufferExample()
        {
            // 设置复制缓冲区
            EditorGUIUtility.systemCopyBuffer = "Hello from Unity Editor!";
            Debug.Log("已设置系统复制缓冲区");

            // 获取复制缓冲区
            string buffer = EditorGUIUtility.systemCopyBuffer;
            Debug.Log($"系统复制缓冲区内容: {buffer}");
        }

        /// <summary>
        /// 复制到系统缓冲区
        /// </summary>
        public static void CopyToSystemBufferExample()
        {
            string textToCopy = "Unity Editor GUI Utility Example";
            EditorGUIUtility.systemCopyBuffer = textToCopy;
            Debug.Log($"文本已复制到系统缓冲区: {textToCopy}");
        }

        #endregion

        #region 控件ID示例

        /// <summary>
        /// 获取控件ID
        /// </summary>
        public static void GetControlIDExample()
        {
            int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
            Debug.Log($"控件ID: {controlID}");
        }

        /// <summary>
        /// 获取控件ID（带焦点类型）
        /// </summary>
        public static void GetControlIDWithFocusExample()
        {
            int controlID = EditorGUIUtility.GetControlID(FocusType.Keyboard);
            Debug.Log($"键盘焦点控件ID: {controlID}");
        }

        #endregion

        #region 标签和字段示例

        /// <summary>
        /// 标签字段宽度
        /// </summary>
        public static void LabelWidthExample()
        {
            float originalWidth = EditorGUIUtility.labelWidth;
            Debug.Log($"原始标签宽度: {originalWidth}");

            // 设置新的标签宽度
            EditorGUIUtility.labelWidth = 200f;
            Debug.Log($"新标签宽度: {EditorGUIUtility.labelWidth}");

            // 恢复原始宽度
            EditorGUIUtility.labelWidth = originalWidth;
        }

        /// <summary>
        /// 字段宽度
        /// </summary>
        public static void FieldWidthExample()
        {
            float originalWidth = EditorGUIUtility.fieldWidth;
            Debug.Log($"原始字段宽度: {originalWidth}");

            // 设置新的字段宽度
            EditorGUIUtility.fieldWidth = 150f;
            Debug.Log($"新字段宽度: {EditorGUIUtility.fieldWidth}");

            // 恢复原始宽度
            EditorGUIUtility.fieldWidth = originalWidth;
        }

        #endregion

        #region 颜色和样式示例

        /// <summary>
        /// 获取默认颜色
        /// </summary>
        public static void GetDefaultColorExample()
        {
            Color defaultColor = EditorGUIUtility.GetDefaultColor();
            Debug.Log($"默认颜色: {defaultColor}");
        }

        /// <summary>
        /// 设置默认颜色
        /// </summary>
        public static void SetDefaultColorExample()
        {
            Color originalColor = EditorGUIUtility.GetDefaultColor();
            
            // 设置新的默认颜色
            EditorGUIUtility.SetDefaultColor(Color.blue);
            Debug.Log("默认颜色已设置为蓝色");

            // 恢复原始颜色
            EditorGUIUtility.SetDefaultColor(originalColor);
        }

        #endregion

        #region 工具提示示例

        /// <summary>
        /// 显示工具提示
        /// </summary>
        public static void ShowTooltipExample()
        {
            EditorGUIUtility.ShowTooltip("这是一个工具提示示例");
        }

        /// <summary>
        /// 获取工具提示
        /// </summary>
        public static void GetTooltipExample()
        {
            string tooltip = EditorGUIUtility.GetTooltip();
            Debug.Log($"当前工具提示: {tooltip}");
        }

        #endregion

        #region 编辑器窗口示例

        /// <summary>
        /// 获取编辑器窗口
        /// </summary>
        public static void GetEditorWindowExample()
        {
            EditorWindow window = EditorGUIUtility.GetEditorWindow(typeof(SceneView));
            if (window != null)
            {
                Debug.Log($"找到编辑器窗口: {window.GetType().Name}");
            }
        }

        /// <summary>
        /// 聚焦编辑器窗口
        /// </summary>
        public static void FocusEditorWindowExample()
        {
            EditorWindow window = EditorGUIUtility.GetEditorWindow(typeof(SceneView));
            if (window != null)
            {
                window.Focus();
                Debug.Log("场景视图窗口已聚焦");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建自定义GUI内容
        /// </summary>
        public static void CreateCustomGUIContentExample()
        {
            // 创建自定义内容
            GUIContent content = new GUIContent("自定义标签", EditorGUIUtility.FindTexture("d_GameObject Icon"));
            
            Debug.Log($"自定义内容 - 文本: {content.text}");
            Debug.Log($"自定义内容 - 图片: {content.image}");
            Debug.Log($"自定义内容 - 工具提示: {content.tooltip}");
        }

        /// <summary>
        /// 批量处理图标
        /// </summary>
        public static void BatchProcessIconsExample()
        {
            string[] iconNames = {
                "d_GameObject Icon",
                "d_Camera Icon",
                "d_Light Icon",
                "d_MeshRenderer Icon"
            };

            List<Texture2D> textures = new List<Texture2D>();

            foreach (string iconName in iconNames)
            {
                Texture2D texture = EditorGUIUtility.FindTexture(iconName);
                if (texture != null)
                {
                    textures.Add(texture);
                    Debug.Log($"找到图标: {iconName}");
                }
            }

            Debug.Log($"总共找到 {textures.Count} 个图标");
        }

        /// <summary>
        /// GUI样式工具
        /// </summary>
        public static void GUIStyleUtilityExample()
        {
            // 获取默认样式
            GUIStyle labelStyle = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).label;
            Debug.Log($"默认标签样式: {labelStyle.name}");

            // 设置标签宽度
            float originalLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 150f;

            Debug.Log($"标签宽度已设置为: {EditorGUIUtility.labelWidth}");

            // 恢复原始宽度
            EditorGUIUtility.labelWidth = originalLabelWidth;
        }

        #endregion
    }
}
