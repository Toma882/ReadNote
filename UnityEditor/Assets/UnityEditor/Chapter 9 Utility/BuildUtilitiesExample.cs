using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace UnityEditor.Examples
{
    /// <summary>
    /// BuildUtilities 工具类示例
    /// 提供构建相关的实用工具功能
    /// </summary>
    public static class BuildUtilitiesExample
    {
        #region 构建目标名称示例

        /// <summary>
        /// 获取构建目标名称
        /// </summary>
        public static void GetBuildTargetNameExample()
        {
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            Debug.Log($"构建目标信息:");
            Debug.Log($"- 当前目标: {currentTarget}");
            Debug.Log($"- 目标名称: {targetName}");
        }

        /// <summary>
        /// 获取所有构建目标名称
        /// </summary>
        public static void GetAllBuildTargetNamesExample()
        {
            BuildTarget[] allTargets = {
                BuildTarget.StandaloneWindows,
                BuildTarget.StandaloneWindows64,
                BuildTarget.StandaloneOSX,
                BuildTarget.StandaloneLinux64,
                BuildTarget.Android,
                BuildTarget.iOS,
                BuildTarget.WebGL
            };
            
            Debug.Log("所有构建目标名称:");
            
            foreach (BuildTarget target in allTargets)
            {
                string targetName = BuildUtilities.GetBuildTargetName(target);
                Debug.Log($"- {target}: {targetName}");
            }
        }

        #endregion

        #region 构建目标检查示例

        /// <summary>
        /// 检查构建目标
        /// </summary>
        public static void CheckBuildTargetExample()
        {
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            Debug.Log($"构建目标检查:");
            Debug.Log($"- 当前目标: {currentTarget}");
            Debug.Log($"- 目标名称: {targetName}");
            Debug.Log($"- 目标有效: {IsValidBuildTarget(currentTarget)}");
            
            // 检查目标平台
            switch (currentTarget)
            {
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    Debug.Log("- 平台: Windows");
                    break;
                case BuildTarget.StandaloneOSX:
                    Debug.Log("- 平台: macOS");
                    break;
                case BuildTarget.StandaloneLinux64:
                    Debug.Log("- 平台: Linux");
                    break;
                case BuildTarget.Android:
                    Debug.Log("- 平台: Android");
                    break;
                case BuildTarget.iOS:
                    Debug.Log("- 平台: iOS");
                    break;
                case BuildTarget.WebGL:
                    Debug.Log("- 平台: WebGL");
                    break;
                default:
                    Debug.Log("- 平台: 其他");
                    break;
            }
        }

        /// <summary>
        /// 检查构建目标兼容性
        /// </summary>
        public static void CheckBuildTargetCompatibilityExample()
        {
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            Debug.Log($"构建目标兼容性检查:");
            Debug.Log($"- 目标: {currentTarget} ({targetName})");
            
            // 检查当前平台是否支持该目标
            bool isSupported = IsBuildTargetSupported(currentTarget);
            Debug.Log($"- 支持状态: {(isSupported ? "支持" : "不支持")}");
            
            if (isSupported)
            {
                Debug.Log("- 可以构建项目");
                Debug.Log("- 可以发布到目标平台");
            }
            else
            {
                Debug.LogWarning("- 无法构建项目");
                Debug.LogWarning("- 需要安装相应的构建模块");
            }
        }

        #endregion

        #region 构建设置示例

        /// <summary>
        /// 检查构建设置
        /// </summary>
        public static void CheckBuildSettingsExample()
        {
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            Debug.Log($"构建设置检查:");
            Debug.Log($"- 构建目标: {currentTarget} ({targetName})");
            Debug.Log($"- 开发构建: {EditorUserBuildSettings.development}");
            Debug.Log($"- 调试构建: {EditorUserBuildSettings.allowDebugging}");
            Debug.Log($"- 脚本调试: {EditorUserBuildSettings.connectProfiler}");
            Debug.Log($"- 自动连接分析器: {EditorUserBuildSettings.connectProfiler}");
            Debug.Log($"- 构建脚本: {EditorUserBuildSettings.buildScriptsOnly}");
        }

        /// <summary>
        /// 设置构建目标
        /// </summary>
        public static void SetBuildTargetExample()
        {
            BuildTarget newTarget = BuildTarget.StandaloneWindows64;
            string targetName = BuildUtilities.GetBuildTargetName(newTarget);
            
            Debug.Log($"设置构建目标:");
            Debug.Log($"- 新目标: {newTarget} ({targetName})");
            
            // 检查目标是否支持
            if (IsBuildTargetSupported(newTarget))
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, newTarget);
                Debug.Log("✓ 构建目标已设置");
            }
            else
            {
                Debug.LogWarning("✗ 构建目标不支持");
            }
        }

        #endregion

        #region 构建路径示例

        /// <summary>
        /// 获取构建路径
        /// </summary>
        public static void GetBuildPathExample()
        {
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            Debug.Log($"构建路径信息:");
            Debug.Log($"- 构建目标: {currentTarget} ({targetName})");
            
            // 获取默认构建路径
            string defaultPath = GetDefaultBuildPath(currentTarget);
            Debug.Log($"- 默认路径: {defaultPath}");
            
            // 获取当前构建路径
            string currentPath = EditorUserBuildSettings.GetBuildLocation(currentTarget);
            Debug.Log($"- 当前路径: {currentPath}");
        }

        /// <summary>
        /// 设置构建路径
        /// </summary>
        public static void SetBuildPathExample()
        {
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            string newPath = "Builds/MyGame";
            
            Debug.Log($"设置构建路径:");
            Debug.Log($"- 构建目标: {currentTarget} ({targetName})");
            Debug.Log($"- 新路径: {newPath}");
            
            EditorUserBuildSettings.SetBuildLocation(currentTarget, newPath);
            Debug.Log("✓ 构建路径已设置");
        }

        #endregion

        #region 构建选项示例

        /// <summary>
        /// 检查构建选项
        /// </summary>
        public static void CheckBuildOptionsExample()
        {
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            Debug.Log($"构建选项检查:");
            Debug.Log($"- 构建目标: {currentTarget} ({targetName})");
            Debug.Log($"- 开发构建: {EditorUserBuildSettings.development}");
            Debug.Log($"- 调试构建: {EditorUserBuildSettings.allowDebugging}");
            Debug.Log($"- 脚本调试: {EditorUserBuildSettings.connectProfiler}");
            Debug.Log($"- 构建脚本: {EditorUserBuildSettings.buildScriptsOnly}");
            Debug.Log($"- 压缩包: {EditorUserBuildSettings.compressFilesInPackage}");
        }

        /// <summary>
        /// 设置构建选项
        /// </summary>
        public static void SetBuildOptionsExample()
        {
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            Debug.Log($"设置构建选项:");
            Debug.Log($"- 构建目标: {currentTarget} ({targetName})");
            
            // 设置开发构建
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.allowDebugging = true;
            EditorUserBuildSettings.connectProfiler = true;
            
            Debug.Log("✓ 构建选项已设置为开发模式");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 构建目标诊断
        /// </summary>
        public static void BuildTargetDiagnosticsExample()
        {
            Debug.Log("=== 构建目标诊断 ===");
            
            BuildTarget currentTarget = EditorUserBuildSettings.activeBuildTarget;
            string targetName = BuildUtilities.GetBuildTargetName(currentTarget);
            
            Debug.Log($"✓ 当前构建目标: {currentTarget} ({targetName})");
            Debug.Log($"✓ 目标支持状态: {(IsBuildTargetSupported(currentTarget) ? "支持" : "不支持")}");
            Debug.Log($"✓ 开发构建: {EditorUserBuildSettings.development}");
            Debug.Log($"✓ 调试构建: {EditorUserBuildSettings.allowDebugging}");
            Debug.Log($"✓ 脚本调试: {EditorUserBuildSettings.connectProfiler}");
            
            string buildPath = EditorUserBuildSettings.GetBuildLocation(currentTarget);
            Debug.Log($"✓ 构建路径: {buildPath}");
        }

        /// <summary>
        /// 批量构建目标处理
        /// </summary>
        public static void BatchBuildTargetProcessingExample()
        {
            BuildTarget[] targets = {
                BuildTarget.StandaloneWindows,
                BuildTarget.StandaloneWindows64,
                BuildTarget.StandaloneOSX,
                BuildTarget.StandaloneLinux64,
                BuildTarget.Android,
                BuildTarget.iOS,
                BuildTarget.WebGL
            };
            
            Debug.Log("批量构建目标处理:");
            
            foreach (BuildTarget target in targets)
            {
                string targetName = BuildUtilities.GetBuildTargetName(target);
                bool isSupported = IsBuildTargetSupported(target);
                
                Debug.Log($"- {target} ({targetName}): {(isSupported ? "支持" : "不支持")}");
            }
        }

        /// <summary>
        /// 构建目标测试
        /// </summary>
        public static void BuildTargetTestExample()
        {
            Debug.Log("构建目标测试:");
            
            // 测试所有主要构建目标
            BuildTarget[] testTargets = {
                BuildTarget.StandaloneWindows64,
                BuildTarget.Android,
                BuildTarget.iOS,
                BuildTarget.WebGL
            };
            
            foreach (BuildTarget target in testTargets)
            {
                string targetName = BuildUtilities.GetBuildTargetName(target);
                bool isSupported = IsBuildTargetSupported(target);
                
                Debug.Log($"测试 {target} ({targetName}):");
                Debug.Log($"  - 支持状态: {(isSupported ? "支持" : "不支持")}");
                Debug.Log($"  - 目标名称: {targetName}");
                
                if (isSupported)
                {
                    Debug.Log($"  - 可以构建到 {targetName}");
                }
                else
                {
                    Debug.LogWarning($"  - 无法构建到 {targetName}");
                }
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查构建目标是否有效
        /// </summary>
        private static bool IsValidBuildTarget(BuildTarget target)
        {
            return target != BuildTarget.NoTarget;
        }

        /// <summary>
        /// 检查构建目标是否支持
        /// </summary>
        private static bool IsBuildTargetSupported(BuildTarget target)
        {
            // 这里简化了检查逻辑，实际使用时可能需要更复杂的检查
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.StandaloneOSX:
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.WebGL:
                    return true;
                case BuildTarget.Android:
                    return true; // 假设Android模块已安装
                case BuildTarget.iOS:
                    return true; // 假设iOS模块已安装
                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取默认构建路径
        /// </summary>
        private static string GetDefaultBuildPath(BuildTarget target)
        {
            string targetName = BuildUtilities.GetBuildTargetName(target);
            return $"Builds/{targetName}";
        }

        /// <summary>
        /// 获取构建目标描述
        /// </summary>
        private static string GetBuildTargetDescription(BuildTarget target)
        {
            string targetName = BuildUtilities.GetBuildTargetName(target);
            
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return $"Windows平台 ({targetName})";
                case BuildTarget.StandaloneOSX:
                    return $"macOS平台 ({targetName})";
                case BuildTarget.StandaloneLinux64:
                    return $"Linux平台 ({targetName})";
                case BuildTarget.Android:
                    return $"Android平台 ({targetName})";
                case BuildTarget.iOS:
                    return $"iOS平台 ({targetName})";
                case BuildTarget.WebGL:
                    return $"WebGL平台 ({targetName})";
                default:
                    return $"其他平台 ({targetName})";
            }
        }

        #endregion
    }
}
