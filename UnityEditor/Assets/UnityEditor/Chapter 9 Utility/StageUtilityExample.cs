using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.SceneManagement;

namespace UnityEditor.Examples
{
    /// <summary>
    /// StageUtility 工具类示例
    /// 提供场景舞台相关的实用工具功能
    /// </summary>
    public static class StageUtilityExample
    {
        #region 主舞台示例

        /// <summary>
        /// 获取主舞台
        /// </summary>
        public static void GetMainStageExample()
        {
            Stage mainStage = StageUtility.GetMainStage();
            
            if (mainStage != null)
            {
                Debug.Log($"主舞台信息:");
                Debug.Log($"- 舞台名称: {mainStage.name}");
                Debug.Log($"- 舞台类型: {mainStage.GetType().Name}");
                Debug.Log($"- 舞台有效: {mainStage.IsValid()}");
            }
            else
            {
                Debug.LogWarning("未找到主舞台");
            }
        }

        /// <summary>
        /// 检查主舞台状态
        /// </summary>
        public static void CheckMainStageStatusExample()
        {
            Stage mainStage = StageUtility.GetMainStage();
            
            if (mainStage != null)
            {
                Debug.Log($"主舞台状态检查:");
                Debug.Log($"- 舞台存在: {mainStage != null}");
                Debug.Log($"- 舞台有效: {mainStage.IsValid()}");
                Debug.Log($"- 舞台名称: {mainStage.name}");
                
                // 检查舞台场景
                Scene stageScene = mainStage.scene;
                Debug.Log($"- 舞台场景: {stageScene.name}");
                Debug.Log($"- 场景路径: {stageScene.path}");
                Debug.Log($"- 场景已加载: {stageScene.isLoaded}");
            }
        }

        #endregion

        #region 舞台操作示例

        /// <summary>
        /// 获取当前舞台
        /// </summary>
        public static void GetCurrentStageExample()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            
            if (currentStage != null)
            {
                Debug.Log($"当前舞台:");
                Debug.Log($"- 舞台名称: {currentStage.name}");
                Debug.Log($"- 舞台类型: {currentStage.GetType().Name}");
                Debug.Log($"- 是否为预制体舞台: {currentStage is PrefabStage}");
            }
            else
            {
                Debug.LogWarning("未找到当前舞台");
            }
        }

        /// <summary>
        /// 检查舞台类型
        /// </summary>
        public static void CheckStageTypeExample()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            
            if (currentStage != null)
            {
                Debug.Log($"舞台类型检查:");
                
                if (currentStage is PrefabStage prefabStage)
                {
                    Debug.Log($"- 类型: 预制体舞台");
                    Debug.Log($"- 预制体资源: {prefabStage.prefabContentsRoot}");
                    Debug.Log($"- 预制体路径: {prefabStage.assetPath}");
                }
                else if (currentStage is MainStage)
                {
                    Debug.Log($"- 类型: 主舞台");
                }
                else
                {
                    Debug.Log($"- 类型: 其他舞台 ({currentStage.GetType().Name})");
                }
            }
        }

        #endregion

        #region 预制体舞台示例

        /// <summary>
        /// 获取预制体舞台
        /// </summary>
        public static void GetPrefabStageExample()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            
            if (currentStage is PrefabStage prefabStage)
            {
                Debug.Log($"预制体舞台信息:");
                Debug.Log($"- 预制体根对象: {prefabStage.prefabContentsRoot.name}");
                Debug.Log($"- 预制体路径: {prefabStage.assetPath}");
                Debug.Log($"- 预制体场景: {prefabStage.scene.name}");
                Debug.Log($"- 预制体有效: {prefabStage.IsValid()}");
            }
            else
            {
                Debug.Log("当前不在预制体舞台中");
            }
        }

        /// <summary>
        /// 检查预制体舞台状态
        /// </summary>
        public static void CheckPrefabStageStatusExample()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            
            if (currentStage is PrefabStage prefabStage)
            {
                Debug.Log($"预制体舞台状态:");
                Debug.Log($"- 预制体根对象: {prefabStage.prefabContentsRoot.name}");
                Debug.Log($"- 预制体路径: {prefabStage.assetPath}");
                
                // 检查预制体修改状态
                bool hasModifications = PrefabUtility.HasPrefabInstanceAnyOverrides(prefabStage.prefabContentsRoot, false);
                Debug.Log($"- 是否有修改: {hasModifications}");
                
                // 检查预制体连接状态
                PrefabInstanceStatus status = PrefabUtility.GetPrefabInstanceStatus(prefabStage.prefabContentsRoot);
                Debug.Log($"- 预制体状态: {status}");
            }
        }

        #endregion

        #region 舞台切换示例

        /// <summary>
        /// 切换到主舞台
        /// </summary>
        public static void GoToMainStageExample()
        {
            Stage mainStage = StageUtility.GetMainStage();
            
            if (mainStage != null)
            {
                StageUtility.GoToMainStage();
                Debug.Log("已切换到主舞台");
            }
            else
            {
                Debug.LogWarning("无法切换到主舞台");
            }
        }

        /// <summary>
        /// 检查舞台切换状态
        /// </summary>
        public static void CheckStageSwitchStatusExample()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            Stage mainStage = StageUtility.GetMainStage();
            
            Debug.Log($"舞台切换状态:");
            Debug.Log($"- 当前舞台: {currentStage?.name ?? "无"}");
            Debug.Log($"- 主舞台: {mainStage?.name ?? "无"}");
            Debug.Log($"- 是否在主舞台: {currentStage == mainStage}");
        }

        #endregion

        #region 舞台场景示例

        /// <summary>
        /// 获取舞台场景
        /// </summary>
        public static void GetStageSceneExample()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            
            if (currentStage != null)
            {
                Scene stageScene = currentStage.scene;
                
                Debug.Log($"舞台场景信息:");
                Debug.Log($"- 场景名称: {stageScene.name}");
                Debug.Log($"- 场景路径: {stageScene.path}");
                Debug.Log($"- 场景已加载: {stageScene.isLoaded}");
                Debug.Log($"- 场景有效: {stageScene.IsValid()}");
                Debug.Log($"- 场景索引: {stageScene.buildIndex}");
            }
        }

        /// <summary>
        /// 检查舞台场景状态
        /// </summary>
        public static void CheckStageSceneStatusExample()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            
            if (currentStage != null)
            {
                Scene stageScene = currentStage.scene;
                
                Debug.Log($"舞台场景状态:");
                Debug.Log($"- 场景名称: {stageScene.name}");
                Debug.Log($"- 场景路径: {stageScene.path}");
                Debug.Log($"- 场景已加载: {stageScene.isLoaded}");
                Debug.Log($"- 场景根对象数量: {stageScene.rootCount}");
                
                // 获取场景中的所有根对象
                GameObject[] rootObjects = stageScene.GetRootGameObjects();
                Debug.Log($"- 根对象列表: {string.Join(", ", System.Array.ConvertAll(rootObjects, go => go.name))}");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 舞台诊断
        /// </summary>
        public static void StageDiagnosticsExample()
        {
            Debug.Log("=== 舞台诊断 ===");
            
            // 主舞台
            Stage mainStage = StageUtility.GetMainStage();
            Debug.Log($"✓ 主舞台: {mainStage?.name ?? "无"}");
            
            // 当前舞台
            Stage currentStage = StageUtility.GetCurrentStage();
            Debug.Log($"✓ 当前舞台: {currentStage?.name ?? "无"}");
            
            // 舞台类型
            if (currentStage != null)
            {
                Debug.Log($"✓ 舞台类型: {currentStage.GetType().Name}");
                
                if (currentStage is PrefabStage prefabStage)
                {
                    Debug.Log($"✓ 预制体路径: {prefabStage.assetPath}");
                    Debug.Log($"✓ 预制体根对象: {prefabStage.prefabContentsRoot?.name ?? "无"}");
                }
                
                // 舞台场景
                Scene stageScene = currentStage.scene;
                Debug.Log($"✓ 舞台场景: {stageScene.name}");
                Debug.Log($"✓ 场景路径: {stageScene.path}");
                Debug.Log($"✓ 场景已加载: {stageScene.isLoaded}");
            }
        }

        /// <summary>
        /// 批量舞台处理
        /// </summary>
        public static void BatchStageProcessingExample()
        {
            Debug.Log("批量舞台处理:");
            
            // 获取主舞台
            Stage mainStage = StageUtility.GetMainStage();
            if (mainStage != null)
            {
                Debug.Log($"- 处理主舞台: {mainStage.name}");
                ProcessStage(mainStage);
            }
            
            // 获取当前舞台
            Stage currentStage = StageUtility.GetCurrentStage();
            if (currentStage != null && currentStage != mainStage)
            {
                Debug.Log($"- 处理当前舞台: {currentStage.name}");
                ProcessStage(currentStage);
            }
        }

        /// <summary>
        /// 处理单个舞台
        /// </summary>
        private static void ProcessStage(Stage stage)
        {
            if (stage == null) return;
            
            Debug.Log($"  处理舞台: {stage.name}");
            Debug.Log($"  - 类型: {stage.GetType().Name}");
            Debug.Log($"  - 有效: {stage.IsValid()}");
            
            Scene stageScene = stage.scene;
            Debug.Log($"  - 场景: {stageScene.name}");
            Debug.Log($"  - 根对象数量: {stageScene.rootCount}");
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查是否在预制体舞台中
        /// </summary>
        private static bool IsInPrefabStage()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            return currentStage is PrefabStage;
        }

        /// <summary>
        /// 获取预制体舞台信息
        /// </summary>
        private static string GetPrefabStageInfo()
        {
            Stage currentStage = StageUtility.GetCurrentStage();
            
            if (currentStage is PrefabStage prefabStage)
            {
                return $"预制体舞台: {prefabStage.prefabContentsRoot.name} ({prefabStage.assetPath})";
            }
            
            return "不在预制体舞台中";
        }

        #endregion
    }
}
