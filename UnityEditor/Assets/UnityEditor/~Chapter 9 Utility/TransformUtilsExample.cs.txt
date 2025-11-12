using UnityEngine;
using UnityEditor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// TransformUtils 工具类示例
    /// 提供Transform相关的实用工具功能
    /// </summary>
    public static class TransformUtilsExample
    {
        #region 检查器显示模式示例

        /// <summary>
        /// 获取检查器显示模式
        /// </summary>
        public static void GetInspectorShowModeExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            InspectorShowMode showMode = TransformUtils.GetInspectorShowMode(selected);
            Debug.Log($"Transform {selected.name} 的检查器显示模式: {showMode}");
        }

        /// <summary>
        /// 设置检查器显示模式
        /// </summary>
        public static void SetInspectorShowModeExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            // 设置为世界空间模式
            TransformUtils.SetInspectorShowMode(selected, InspectorShowMode.World);
            Debug.Log($"Transform {selected.name} 的检查器显示模式已设置为世界空间");

            // 设置为本地空间模式
            TransformUtils.SetInspectorShowMode(selected, InspectorShowMode.Local);
            Debug.Log($"Transform {selected.name} 的检查器显示模式已设置为本地空间");
        }

        /// <summary>
        /// 切换检查器显示模式
        /// </summary>
        public static void ToggleInspectorShowModeExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            InspectorShowMode currentMode = TransformUtils.GetInspectorShowMode(selected);
            InspectorShowMode newMode = currentMode == InspectorShowMode.World ? 
                InspectorShowMode.Local : InspectorShowMode.World;

            TransformUtils.SetInspectorShowMode(selected, newMode);
            Debug.Log($"检查器显示模式已从 {currentMode} 切换到 {newMode}");
        }

        #endregion

        #region Transform操作示例

        /// <summary>
        /// 重置Transform
        /// </summary>
        public static void ResetTransformExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            selected.position = Vector3.zero;
            selected.rotation = Quaternion.identity;
            selected.localScale = Vector3.one;

            Debug.Log($"Transform {selected.name} 已重置");
        }

        /// <summary>
        /// 设置Transform位置
        /// </summary>
        public static void SetTransformPositionExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Vector3 newPosition = new Vector3(5, 10, 15);
            selected.position = newPosition;
            Debug.Log($"Transform {selected.name} 位置已设置为: {newPosition}");
        }

        /// <summary>
        /// 设置Transform旋转
        /// </summary>
        public static void SetTransformRotationExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Quaternion newRotation = Quaternion.Euler(45, 90, 135);
            selected.rotation = newRotation;
            Debug.Log($"Transform {selected.name} 旋转已设置为: {newRotation.eulerAngles}");
        }

        /// <summary>
        /// 设置Transform缩放
        /// </summary>
        public static void SetTransformScaleExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Vector3 newScale = new Vector3(2, 2, 2);
            selected.localScale = newScale;
            Debug.Log($"Transform {selected.name} 缩放已设置为: {newScale}");
        }

        #endregion

        #region 父子关系示例

        /// <summary>
        /// 设置父对象
        /// </summary>
        public static void SetParentExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            // 创建父对象
            GameObject parentObj = new GameObject("ParentObject");
            Transform parent = parentObj.transform;

            // 设置父对象
            selected.SetParent(parent);
            Debug.Log($"Transform {selected.name} 的父对象已设置为: {parent.name}");

            // 清理
            Object.DestroyImmediate(parentObj);
        }

        /// <summary>
        /// 获取子对象
        /// </summary>
        public static void GetChildrenExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            int childCount = selected.childCount;
            Debug.Log($"Transform {selected.name} 有 {childCount} 个子对象");

            for (int i = 0; i < childCount; i++)
            {
                Transform child = selected.GetChild(i);
                Debug.Log($"子对象 {i}: {child.name}");
            }
        }

        /// <summary>
        /// 查找子对象
        /// </summary>
        public static void FindChildExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            // 创建测试子对象
            GameObject childObj = new GameObject("TestChild");
            childObj.transform.SetParent(selected);

            // 查找子对象
            Transform foundChild = selected.Find("TestChild");
            if (foundChild != null)
            {
                Debug.Log($"找到子对象: {foundChild.name}");
            }

            // 清理
            Object.DestroyImmediate(childObj);
        }

        #endregion

        #region 变换矩阵示例

        /// <summary>
        /// 本地到世界矩阵
        /// </summary>
        public static void LocalToWorldMatrixExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Matrix4x4 localToWorld = selected.localToWorldMatrix;
            Debug.Log($"本地到世界矩阵: {localToWorld}");

            // 变换点
            Vector3 localPoint = new Vector3(1, 2, 3);
            Vector3 worldPoint = localToWorld.MultiplyPoint3x4(localPoint);
            Debug.Log($"本地点 {localPoint} 变换为世界点 {worldPoint}");
        }

        /// <summary>
        /// 世界到本地矩阵
        /// </summary>
        public static void WorldToLocalMatrixExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Matrix4x4 worldToLocal = selected.worldToLocalMatrix;
            Debug.Log($"世界到本地矩阵: {worldToLocal}");

            // 变换点
            Vector3 worldPoint = new Vector3(5, 10, 15);
            Vector3 localPoint = worldToLocal.MultiplyPoint3x4(worldPoint);
            Debug.Log($"世界点 {worldPoint} 变换为本地点 {localPoint}");
        }

        #endregion

        #region 方向向量示例

        /// <summary>
        /// 获取方向向量
        /// </summary>
        public static void GetDirectionVectorsExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Vector3 forward = selected.forward;
            Vector3 right = selected.right;
            Vector3 up = selected.up;

            Debug.Log($"Transform {selected.name} 的方向向量:");
            Debug.Log($"前方向: {forward}");
            Debug.Log($"右方向: {right}");
            Debug.Log($"上方向: {up}");
        }

        /// <summary>
        /// 设置方向向量
        /// </summary>
        public static void SetDirectionVectorsExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            // 看向目标
            Vector3 targetPosition = new Vector3(10, 0, 10);
            selected.LookAt(targetPosition);
            Debug.Log($"Transform {selected.name} 已看向目标: {targetPosition}");
        }

        #endregion

        #region 变换操作示例

        /// <summary>
        /// 平移变换
        /// </summary>
        public static void TranslateExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Vector3 originalPosition = selected.position;
            Vector3 translation = new Vector3(2, 0, 2);

            selected.Translate(translation);
            Debug.Log($"Transform {selected.name} 已平移: {originalPosition} -> {selected.position}");
        }

        /// <summary>
        /// 旋转变换
        /// </summary>
        public static void RotateExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Vector3 originalRotation = selected.eulerAngles;
            Vector3 rotation = new Vector3(0, 45, 0);

            selected.Rotate(rotation);
            Debug.Log($"Transform {selected.name} 已旋转: {originalRotation} -> {selected.eulerAngles}");
        }

        /// <summary>
        /// 缩放变换
        /// </summary>
        public static void ScaleExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Vector3 originalScale = selected.localScale;
            Vector3 scaleFactor = new Vector3(1.5f, 1.5f, 1.5f);

            selected.localScale = Vector3.Scale(originalScale, scaleFactor);
            Debug.Log($"Transform {selected.name} 已缩放: {originalScale} -> {selected.localScale}");
        }

        #endregion

        #region 批量操作示例

        /// <summary>
        /// 批量重置Transform
        /// </summary>
        public static void BatchResetTransformExample()
        {
            Transform[] allTransforms = Object.FindObjectsOfType<Transform>();
            int resetCount = 0;

            foreach (Transform transform in allTransforms)
            {
                if (transform.parent == null) // 只重置根对象
                {
                    transform.position = Vector3.zero;
                    transform.rotation = Quaternion.identity;
                    transform.localScale = Vector3.one;
                    resetCount++;
                }
            }

            Debug.Log($"已重置 {resetCount} 个根Transform");
        }

        /// <summary>
        /// 批量设置检查器显示模式
        /// </summary>
        public static void BatchSetInspectorShowModeExample()
        {
            Transform[] allTransforms = Object.FindObjectsOfType<Transform>();
            int setCount = 0;

            foreach (Transform transform in allTransforms)
            {
                TransformUtils.SetInspectorShowMode(transform, InspectorShowMode.World);
                setCount++;
            }

            Debug.Log($"已为 {setCount} 个Transform设置检查器显示模式");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建Transform层次结构
        /// </summary>
        public static void CreateTransformHierarchyExample()
        {
            // 创建根对象
            GameObject rootObj = new GameObject("RootTransform");
            Transform root = rootObj.transform;
            root.position = Vector3.zero;

            // 创建子对象
            for (int i = 0; i < 3; i++)
            {
                GameObject childObj = new GameObject($"ChildTransform_{i}");
                Transform child = childObj.transform;
                child.SetParent(root);
                child.localPosition = new Vector3(i * 2, 0, 0);
                child.localRotation = Quaternion.Euler(0, i * 30, 0);
                child.localScale = Vector3.one * (1 + i * 0.2f);
            }

            Debug.Log("Transform层次结构已创建");
            Debug.Log($"根对象: {root.name}, 子对象数量: {root.childCount}");

            // 设置检查器显示模式
            TransformUtils.SetInspectorShowMode(root, InspectorShowMode.World);
            Debug.Log("根对象检查器显示模式已设置为世界空间");

            // 清理
            Object.DestroyImmediate(rootObj);
        }

        /// <summary>
        /// Transform动画示例
        /// </summary>
        public static void TransformAnimationExample()
        {
            Transform selected = GetSelectedTransform();
            if (selected == null) return;

            Vector3 startPosition = selected.position;
            Vector3 endPosition = startPosition + Vector3.up * 5;

            // 简单的动画循环
            float time = 0;
            while (time < 2f)
            {
                time += Time.deltaTime;
                float t = Mathf.PingPong(time, 1f);
                selected.position = Vector3.Lerp(startPosition, endPosition, t);
            }

            Debug.Log("Transform动画示例完成");
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取选中的Transform
        /// </summary>
        private static Transform GetSelectedTransform()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                return selected.transform;
            }

            // 创建测试对象
            GameObject testObj = new GameObject("TestTransform");
            Transform testTransform = testObj.transform;
            testTransform.position = new Vector3(0, 0, 0);
            testTransform.rotation = Quaternion.identity;
            testTransform.localScale = Vector3.one;

            return testTransform;
        }

        #endregion
    }
}
