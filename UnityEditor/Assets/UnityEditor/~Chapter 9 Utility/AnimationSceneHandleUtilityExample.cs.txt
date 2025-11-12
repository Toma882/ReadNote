using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

namespace UnityEditor.Examples
{
    /// <summary>
    /// AnimationSceneHandleUtility 工具类示例
    /// 提供动画场景句柄相关的实用工具功能
    /// </summary>
    public static class AnimationSceneHandleUtilityExample
    {
        #region 场景句柄示例

        /// <summary>
        /// 获取场景句柄
        /// </summary>
        public static void GetSceneHandleExample()
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

            // 获取场景句柄
            var sceneHandle = AnimationSceneHandleUtility.GetSceneHandle(animator);
            Debug.Log($"获取到场景句柄: {sceneHandle}");
        }

        /// <summary>
        /// 获取场景句柄（带动画器）
        /// </summary>
        public static void GetSceneHandleWithAnimatorExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Animator animator = selected.GetComponent<Animator>();
            if (animator == null) return;

            // 获取场景句柄
            var sceneHandle = AnimationSceneHandleUtility.GetSceneHandle(animator);
            
            if (sceneHandle.IsValid())
            {
                Debug.Log($"场景句柄有效: {sceneHandle}");
                Debug.Log($"动画器名称: {animator.name}");
                Debug.Log($"动画器运行时模式: {animator.runtimeAnimatorController}");
            }
            else
            {
                Debug.LogWarning("场景句柄无效");
            }
        }

        #endregion

        #region 动画器状态示例

        /// <summary>
        /// 检查动画器状态
        /// </summary>
        public static void CheckAnimatorStateExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Animator animator = selected.GetComponent<Animator>();
            if (animator == null) return;

            var sceneHandle = AnimationSceneHandleUtility.GetSceneHandle(animator);
            
            if (sceneHandle.IsValid())
            {
                Debug.Log($"动画器状态检查:");
                Debug.Log($"- 是否启用: {animator.enabled}");
                Debug.Log($"- 是否应用根运动: {animator.applyRootMotion}");
                Debug.Log($"- 更新模式: {animator.updateMode}");
                Debug.Log($"- 动画模式: {animator.animatorUpdateMode}");
            }
        }

        /// <summary>
        /// 获取动画器参数
        /// </summary>
        public static void GetAnimatorParametersExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null) return;

            Animator animator = selected.GetComponent<Animator>();
            if (animator == null) return;

            var sceneHandle = AnimationSceneHandleUtility.GetSceneHandle(animator);
            
            if (sceneHandle.IsValid())
            {
                AnimatorController controller = animator.runtimeAnimatorController as AnimatorController;
                if (controller != null)
                {
                    Debug.Log($"动画器参数:");
                    foreach (var parameter in controller.parameters)
                    {
                        Debug.Log($"- {parameter.name}: {parameter.type}");
                    }
                }
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建动画场景句柄测试
        /// </summary>
        public static void CreateAnimationSceneHandleTestExample()
        {
            // 创建测试对象
            GameObject testObj = new GameObject("AnimationTestObject");
            Animator animator = testObj.AddComponent<Animator>();
            
            // 创建动画控制器
            AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath("Assets/TestController.controller");
            animator.runtimeAnimatorController = controller;

            // 获取场景句柄
            var sceneHandle = AnimationSceneHandleUtility.GetSceneHandle(animator);
            
            if (sceneHandle.IsValid())
            {
                Debug.Log("动画场景句柄测试成功");
                Debug.Log($"测试对象: {testObj.name}");
                Debug.Log($"动画器: {animator.name}");
                Debug.Log($"控制器: {controller.name}");
            }

            // 清理
            Object.DestroyImmediate(testObj);
            AssetDatabase.DeleteAsset("Assets/TestController.controller");
        }

        /// <summary>
        /// 批量处理动画器
        /// </summary>
        public static void BatchProcessAnimatorsExample()
        {
            Animator[] animators = Object.FindObjectsOfType<Animator>();
            int processedCount = 0;

            foreach (Animator animator in animators)
            {
                var sceneHandle = AnimationSceneHandleUtility.GetSceneHandle(animator);
                
                if (sceneHandle.IsValid())
                {
                    // 处理动画器
                    Debug.Log($"处理动画器: {animator.name}");
                    processedCount++;
                }
            }

            Debug.Log($"批量处理完成，共处理 {processedCount} 个动画器");
        }

        #endregion
    }
}
