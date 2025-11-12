using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.Examples
{
    /// <summary>
    /// RectTransformUtility 工具类示例
    /// 提供RectTransform相关的实用工具功能
    /// </summary>
    public static class RectTransformUtilityExample
    {
        #region 坐标转换示例

        /// <summary>
        /// 屏幕点转本地矩形点
        /// </summary>
        public static void ScreenPointToLocalPointInRectangleExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            Vector2 screenPoint = Input.mousePosition;
            Vector2 localPoint;
            bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, 
                screenPoint, 
                Camera.main, 
                out localPoint
            );

            if (success)
            {
                Debug.Log($"屏幕点 {screenPoint} 转换为本地点 {localPoint}");
            }
            else
            {
                Debug.Log("坐标转换失败");
            }
        }

        /// <summary>
        /// 本地点转屏幕点
        /// </summary>
        public static void LocalPointToScreenPointExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            Vector2 localPoint = Vector2.zero;
            Vector2 screenPoint = RectTransformUtility.LocalPointToScreenPoint(
                rectTransform, 
                localPoint, 
                Camera.main
            );

            Debug.Log($"本地点 {localPoint} 转换为屏幕点 {screenPoint}");
        }

        /// <summary>
        /// 批量坐标转换
        /// </summary>
        public static void BatchCoordinateConversionExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            Vector2[] localPoints = {
                new Vector2(-100, -100),
                new Vector2(0, 0),
                new Vector2(100, 100)
            };

            foreach (Vector2 localPoint in localPoints)
            {
                Vector2 screenPoint = RectTransformUtility.LocalPointToScreenPoint(
                    rectTransform, localPoint, Camera.main);
                
                Vector2 backToLocal;
                bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rectTransform, screenPoint, Camera.main, out backToLocal);

                Debug.Log($"本地: {localPoint} -> 屏幕: {screenPoint} -> 本地: {backToLocal} (成功: {success})");
            }
        }

        #endregion

        #region 布局翻转示例

        /// <summary>
        /// 翻转布局轴（水平）
        /// </summary>
        public static void FlipLayoutOnAxisHorizontalExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            RectTransformUtility.FlipLayoutOnAxis(rectTransform, 0, false, false);
            Debug.Log("布局已水平翻转");
        }

        /// <summary>
        /// 翻转布局轴（垂直）
        /// </summary>
        public static void FlipLayoutOnAxisVerticalExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            RectTransformUtility.FlipLayoutOnAxis(rectTransform, 1, false, false);
            Debug.Log("布局已垂直翻转");
        }

        /// <summary>
        /// 翻转布局轴（保持位置）
        /// </summary>
        public static void FlipLayoutOnAxisKeepPositionExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            RectTransformUtility.FlipLayoutOnAxis(rectTransform, 0, true, true);
            Debug.Log("布局已翻转（保持位置）");
        }

        #endregion

        #region 矩形操作示例

        /// <summary>
        /// 计算矩形边界
        /// </summary>
        public static void CalculateRectangleBoundsExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Debug.Log("矩形世界坐标角点:");
            for (int i = 0; i < corners.Length; i++)
            {
                Debug.Log($"角点 {i}: {corners[i]}");
            }

            // 计算边界
            Vector3 min = corners[0];
            Vector3 max = corners[0];
            
            for (int i = 1; i < corners.Length; i++)
            {
                min = Vector3.Min(min, corners[i]);
                max = Vector3.Max(max, corners[i]);
            }

            Vector3 center = (min + max) * 0.5f;
            Vector3 size = max - min;

            Debug.Log($"矩形中心: {center}");
            Debug.Log($"矩形大小: {size}");
        }

        /// <summary>
        /// 矩形相交检测
        /// </summary>
        public static void RectangleIntersectionExample()
        {
            RectTransform rect1 = GetRectTransform();
            if (rect1 == null) return;

            // 创建第二个矩形
            GameObject rect2Obj = new GameObject("Rect2");
            RectTransform rect2 = rect2Obj.AddComponent<RectTransform>();
            rect2.SetParent(rect1.parent);
            rect2.anchoredPosition = new Vector2(50, 50);
            rect2.sizeDelta = new Vector2(100, 100);

            // 检查相交
            Vector3[] corners1 = new Vector3[4];
            Vector3[] corners2 = new Vector3[4];
            
            rect1.GetWorldCorners(corners1);
            rect2.GetWorldCorners(corners2);

            bool intersects = RectTransformUtility.RectangleContainsScreenPoint(rect1, corners2[0], Camera.main);
            Debug.Log($"矩形相交检测: {intersects}");

            // 清理
            Object.DestroyImmediate(rect2Obj);
        }

        #endregion

        #region 锚点操作示例

        /// <summary>
        /// 锚点设置示例
        /// </summary>
        public static void AnchorSettingsExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            // 设置锚点为左上角
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.anchoredPosition = new Vector2(50, -50);

            Debug.Log("锚点已设置为左上角");
            Debug.Log($"锚点最小值: {rectTransform.anchorMin}");
            Debug.Log($"锚点最大值: {rectTransform.anchorMax}");
            Debug.Log($"锚点位置: {rectTransform.anchoredPosition}");
        }

        /// <summary>
        /// 拉伸模式示例
        /// </summary>
        public static void StretchModeExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            // 设置为拉伸模式
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = new Vector2(10, 10);
            rectTransform.offsetMax = new Vector2(-10, -10);

            Debug.Log("已设置为拉伸模式");
            Debug.Log($"偏移最小值: {rectTransform.offsetMin}");
            Debug.Log($"偏移最大值: {rectTransform.offsetMax}");
        }

        #endregion

        #region 变换操作示例

        /// <summary>
        /// 变换矩阵示例
        /// </summary>
        public static void TransformMatrixExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            Matrix4x4 localToWorldMatrix = rectTransform.localToWorldMatrix;
            Matrix4x4 worldToLocalMatrix = rectTransform.worldToLocalMatrix;

            Debug.Log($"本地到世界矩阵: {localToWorldMatrix}");
            Debug.Log($"世界到本地矩阵: {worldToLocalMatrix}");

            // 变换点
            Vector2 localPoint = new Vector2(50, 50);
            Vector3 worldPoint = localToWorldMatrix.MultiplyPoint3x4(localPoint);
            Vector2 backToLocal = worldToLocalMatrix.MultiplyPoint3x4(worldPoint);

            Debug.Log($"本地点: {localPoint} -> 世界点: {worldPoint} -> 本地点: {backToLocal}");
        }

        /// <summary>
        /// 旋转和缩放示例
        /// </summary>
        public static void RotationAndScaleExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            // 设置旋转
            rectTransform.rotation = Quaternion.Euler(0, 0, 45);
            Debug.Log($"旋转角度: {rectTransform.eulerAngles}");

            // 设置缩放
            rectTransform.localScale = new Vector3(1.5f, 1.5f, 1);
            Debug.Log($"缩放比例: {rectTransform.localScale}");

            // 重置变换
            rectTransform.rotation = Quaternion.identity;
            rectTransform.localScale = Vector3.one;
            Debug.Log("变换已重置");
        }

        #endregion

        #region 布局组示例

        /// <summary>
        /// 布局组操作示例
        /// </summary>
        public static void LayoutGroupExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            // 添加水平布局组
            HorizontalLayoutGroup layoutGroup = rectTransform.GetComponent<HorizontalLayoutGroup>();
            if (layoutGroup == null)
            {
                layoutGroup = rectTransform.gameObject.AddComponent<HorizontalLayoutGroup>();
            }

            layoutGroup.spacing = 10f;
            layoutGroup.padding = new RectOffset(10, 10, 10, 10);
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;

            Debug.Log("水平布局组已设置");
            Debug.Log($"间距: {layoutGroup.spacing}");
            Debug.Log($"内边距: {layoutGroup.padding}");
        }

        /// <summary>
        /// 内容大小适配器示例
        /// </summary>
        public static void ContentSizeFitterExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            ContentSizeFitter sizeFitter = rectTransform.GetComponent<ContentSizeFitter>();
            if (sizeFitter == null)
            {
                sizeFitter = rectTransform.gameObject.AddComponent<ContentSizeFitter>();
            }

            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            Debug.Log("内容大小适配器已设置");
            Debug.Log($"水平适配: {sizeFitter.horizontalFit}");
            Debug.Log($"垂直适配: {sizeFitter.verticalFit}");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建响应式UI元素
        /// </summary>
        public static void CreateResponsiveUIElementExample()
        {
            // 创建父容器
            GameObject parentObj = new GameObject("ResponsiveContainer");
            RectTransform parentRect = parentObj.AddComponent<RectTransform>();
            parentRect.SetParent(GetCanvas().transform, false);
            parentRect.anchorMin = Vector2.zero;
            parentRect.anchorMax = Vector2.one;
            parentRect.offsetMin = Vector2.zero;
            parentRect.offsetMax = Vector2.zero;

            // 创建子元素
            GameObject childObj = new GameObject("ResponsiveChild");
            RectTransform childRect = childObj.AddComponent<RectTransform>();
            childRect.SetParent(parentRect, false);
            
            // 设置响应式布局
            childRect.anchorMin = new Vector2(0.1f, 0.1f);
            childRect.anchorMax = new Vector2(0.9f, 0.9f);
            childRect.offsetMin = Vector2.zero;
            childRect.offsetMax = Vector2.zero;

            Debug.Log("响应式UI元素已创建");
            Debug.Log($"父容器锚点: {parentRect.anchorMin} - {parentRect.anchorMax}");
            Debug.Log($"子元素锚点: {childRect.anchorMin} - {childRect.anchorMax}");

            // 清理
            Object.DestroyImmediate(parentObj);
        }

        /// <summary>
        /// UI元素拖拽示例
        /// </summary>
        public static void UIDragExample()
        {
            RectTransform rectTransform = GetRectTransform();
            if (rectTransform == null) return;

            Vector2 mousePosition = Input.mousePosition;
            Vector2 localPoint;
            
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform,
                mousePosition,
                Camera.main,
                out localPoint))
            {
                rectTransform.anchoredPosition = localPoint;
                Debug.Log($"UI元素已移动到: {localPoint}");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取RectTransform组件
        /// </summary>
        private static RectTransform GetRectTransform()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                RectTransform rectTransform = selected.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    return rectTransform;
                }
            }

            // 创建测试对象
            GameObject testObj = new GameObject("TestRectTransform");
            RectTransform testRect = testObj.AddComponent<RectTransform>();
            testRect.SetParent(GetCanvas().transform, false);
            testRect.sizeDelta = new Vector2(200, 100);
            testRect.anchoredPosition = Vector2.zero;

            return testRect;
        }

        /// <summary>
        /// 获取Canvas组件
        /// </summary>
        private static Canvas GetCanvas()
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
            }
            return canvas;
        }

        #endregion
    }
}
