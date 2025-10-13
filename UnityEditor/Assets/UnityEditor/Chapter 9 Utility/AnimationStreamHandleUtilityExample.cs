using UnityEngine;
using UnityEditor;
using Unity.Collections;

namespace UnityEditor.Examples
{
    /// <summary>
    /// AnimationStreamHandleUtility 工具类示例
    /// 提供动画流句柄相关的实用工具功能
    /// </summary>
    public static class AnimationStreamHandleUtilityExample
    {
        #region 句柄创建示例

        /// <summary>
        /// 创建动画流句柄
        /// </summary>
        public static void CreateHandleExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("请选择一个GameObject");
                return;
            }

            Animator animator = selected.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("选中的对象没有Animator组件");
                return;
            }

            // 创建动画流句柄
            var streamHandle = AnimationStreamHandleUtility.CreateHandle(animator);
            Debug.Log($"创建动画流句柄: {streamHandle}");
        }

        /// <summary>
        /// 创建动画流句柄（带验证）
        /// </summary>
        public static void CreateHandleWithValidationExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Animator animator = selected.GetComponent<Animator>();
            if (animator == null) return;

            // 创建动画流句柄
            var streamHandle = AnimationStreamHandleUtility.CreateHandle(animator);
            
            if (streamHandle.IsValid())
            {
                Debug.Log($"动画流句柄有效: {streamHandle}");
                Debug.Log($"动画器名称: {animator.name}");
            }
            else
            {
                Debug.LogWarning("动画流句柄无效");
            }
        }

        #endregion

        #region 动画流操作示例

        /// <summary>
        /// 获取动画流数据
        /// </summary>
        public static void GetAnimationStreamDataExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Animator animator = selected.GetComponent<Animator>();
            if (animator == null) return;

            var streamHandle = AnimationStreamHandleUtility.CreateHandle(animator);
            
            if (streamHandle.IsValid())
            {
                Debug.Log($"动画流数据:");
                Debug.Log($"- 动画器启用状态: {animator.enabled}");
                Debug.Log($"- 动画器速度: {animator.speed}");
                Debug.Log($"- 动画器层数: {animator.layerCount}");
            }
        }

        /// <summary>
        /// 设置动画流参数
        /// </summary>
        public static void SetAnimationStreamParametersExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Animator animator = selected.GetComponent<Animator>();
            if (animator == null) return;

            var streamHandle = AnimationStreamHandleUtility.CreateHandle(animator);
            
            if (streamHandle.IsValid())
            {
                // 设置动画器参数
                animator.speed = 1.5f;
                Debug.Log($"动画器速度已设置为: {animator.speed}");
            }
        }

        #endregion

        #region 批量处理示例

        /// <summary>
        /// 批量创建动画流句柄
        /// </summary>
        public static void BatchCreateHandlesExample()
        {
            Animator[] animators = Object.FindObjectsOfType<Animator>();
            int createdCount = 0;

            foreach (Animator animator in animators)
            {
                var streamHandle = AnimationStreamHandleUtility.CreateHandle(animator);
                
                if (streamHandle.IsValid())
                {
                    Debug.Log($"为 {animator.name} 创建句柄成功");
                    createdCount++;
                }
            }

            Debug.Log($"批量创建完成，共创建 {createdCount} 个动画流句柄");
        }

        /// <summary>
        /// 批量处理动画流
        /// </summary>
        public static void BatchProcessAnimationStreamsExample()
        {
            Animator[] animators = Object.FindObjectsOfType<Animator>();
            int processedCount = 0;

            foreach (Animator animator in animators)
            {
                var streamHandle = AnimationStreamHandleUtility.CreateHandle(animator);
                
                if (streamHandle.IsValid())
                {
                    // 处理动画流
                    Debug.Log($"处理动画流: {animator.name}");
                    processedCount++;
                }
            }

            Debug.Log($"批量处理完成，共处理 {processedCount} 个动画流");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建动画流测试
        /// </summary>
        public static void CreateAnimationStreamTestExample()
        {
            // 创建测试对象
            GameObject testObj = new GameObject("AnimationStreamTestObject");
            Animator animator = testObj.AddComponent<Animator>();
            
            // 创建动画控制器
            AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath("Assets/StreamTestController.controller");
            animator.runtimeAnimatorController = controller;

            // 创建动画流句柄
            var streamHandle = AnimationStreamHandleUtility.CreateHandle(animator);
            
            if (streamHandle.IsValid())
            {
                Debug.Log("动画流测试成功");
                Debug.Log($"测试对象: {testObj.name}");
                Debug.Log($"动画器: {animator.name}");
                Debug.Log($"控制器: {controller.name}");
                Debug.Log($"句柄: {streamHandle}");
            }

            // 清理
            Object.DestroyImmediate(testObj);
            AssetDatabase.DeleteAsset("Assets/StreamTestController.controller");
        }

        /// <summary>
        /// 动画流性能测试
        /// </summary>
        public static void AnimationStreamPerformanceExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Animator animator = selected.GetComponent<Animator>();
            if (animator == null) return;

            // 性能测试
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int i = 0; i < 1000; i++)
            {
                var streamHandle = AnimationStreamHandleUtility.CreateHandle(animator);
            }
            
            stopwatch.Stop();
            Debug.Log($"1000次句柄创建耗时: {stopwatch.ElapsedMilliseconds}ms");
        }

        #endregion
    }
}
