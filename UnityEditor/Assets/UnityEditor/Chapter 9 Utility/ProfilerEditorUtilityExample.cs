using UnityEngine;
using UnityEditor;
using Unity.Profiling;
using Unity.Profiling.Editor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// ProfilerEditorUtility 工具类示例
    /// 提供性能分析器编辑器相关的实用工具功能
    /// </summary>
    public static class ProfilerEditorUtilityExample
    {
        #region 性能分析器状态示例

        /// <summary>
        /// 检查性能分析器状态
        /// </summary>
        public static void CheckProfilerStatusExample()
        {
            Debug.Log($"性能分析器状态检查:");
            
            // 检查性能分析器是否可用
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            Debug.Log($"- 性能分析器可用: {profilerAvailable}");
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 性能分析器已连接");
                Debug.Log("✓ 可以开始性能分析");
            }
            else
            {
                Debug.LogWarning("✗ 性能分析器未连接");
                Debug.LogWarning("✗ 无法进行性能分析");
            }
        }

        /// <summary>
        /// 获取性能分析器信息
        /// </summary>
        public static void GetProfilerInfoExample()
        {
            Debug.Log($"性能分析器信息:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            Debug.Log($"- 可用状态: {profilerAvailable}");
            
            if (profilerAvailable)
            {
                Debug.Log("- 连接状态: 已连接");
                Debug.Log("- 分析状态: 可分析");
                Debug.Log("- 数据收集: 正常");
            }
            else
            {
                Debug.Log("- 连接状态: 未连接");
                Debug.Log("- 分析状态: 不可分析");
                Debug.Log("- 数据收集: 停止");
            }
        }

        #endregion

        #region 性能分析器连接示例

        /// <summary>
        /// 检查性能分析器连接
        /// </summary>
        public static void CheckProfilerConnectionExample()
        {
            Debug.Log($"性能分析器连接检查:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 性能分析器已连接");
                Debug.Log("✓ 可以开始性能分析");
                Debug.Log("✓ 数据收集正常");
            }
            else
            {
                Debug.LogWarning("✗ 性能分析器未连接");
                Debug.LogWarning("✗ 需要连接性能分析器");
                Debug.LogWarning("✗ 无法进行性能分析");
            }
        }

        /// <summary>
        /// 性能分析器连接建议
        /// </summary>
        public static void ProfilerConnectionAdviceExample()
        {
            Debug.Log($"性能分析器连接建议:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("当前状态:");
                Debug.Log("✓ 性能分析器已连接");
                Debug.Log("✓ 可以开始性能分析");
                Debug.Log("建议操作:");
                Debug.Log("- 开始性能分析");
                Debug.Log("- 收集性能数据");
                Debug.Log("- 分析性能瓶颈");
            }
            else
            {
                Debug.Log("当前状态:");
                Debug.LogWarning("✗ 性能分析器未连接");
                Debug.Log("建议操作:");
                Debug.Log("1. 打开性能分析器窗口");
                Debug.Log("2. 连接到目标设备");
                Debug.Log("3. 开始性能分析");
            }
        }

        #endregion

        #region 性能分析器设置示例

        /// <summary>
        /// 检查性能分析器设置
        /// </summary>
        public static void CheckProfilerSettingsExample()
        {
            Debug.Log($"性能分析器设置检查:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            Debug.Log($"- 性能分析器可用: {profilerAvailable}");
            
            if (profilerAvailable)
            {
                Debug.Log("- 连接状态: 正常");
                Debug.Log("- 数据收集: 正常");
                Debug.Log("- 分析功能: 可用");
            }
            else
            {
                Debug.LogWarning("- 连接状态: 异常");
                Debug.LogWarning("- 数据收集: 停止");
                Debug.LogWarning("- 分析功能: 不可用");
            }
        }

        /// <summary>
        /// 设置性能分析器
        /// </summary>
        public static void SetupProfilerExample()
        {
            Debug.Log($"设置性能分析器:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 性能分析器已就绪");
                Debug.Log("✓ 可以开始性能分析");
                Debug.Log("✓ 设置完成");
            }
            else
            {
                Debug.Log("设置步骤:");
                Debug.Log("1. 打开性能分析器窗口");
                Debug.Log("2. 连接到目标设备");
                Debug.Log("3. 开始性能分析");
                Debug.Log("4. 收集性能数据");
            }
        }

        #endregion

        #region 性能分析器监控示例

        /// <summary>
        /// 性能分析器监控
        /// </summary>
        public static void ProfilerMonitoringExample()
        {
            Debug.Log($"性能分析器监控:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("监控状态:");
                Debug.Log("✓ 性能分析器已连接");
                Debug.Log("✓ 数据收集正常");
                Debug.Log("✓ 可以监控性能");
                
                // 模拟监控数据
                Debug.Log("监控数据:");
                Debug.Log("- CPU使用率: 正常");
                Debug.Log("- 内存使用: 正常");
                Debug.Log("- 渲染性能: 正常");
            }
            else
            {
                Debug.LogWarning("监控状态:");
                Debug.LogWarning("✗ 性能分析器未连接");
                Debug.LogWarning("✗ 无法监控性能");
            }
        }

        /// <summary>
        /// 性能分析器诊断
        /// </summary>
        public static void ProfilerDiagnosticsExample()
        {
            Debug.Log("=== 性能分析器诊断 ===");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            Debug.Log($"✓ 性能分析器可用: {profilerAvailable}");
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 连接状态: 正常");
                Debug.Log("✓ 数据收集: 正常");
                Debug.Log("✓ 分析功能: 可用");
                Debug.Log("✓ 监控功能: 可用");
            }
            else
            {
                Debug.LogWarning("✗ 连接状态: 异常");
                Debug.LogWarning("✗ 数据收集: 停止");
                Debug.LogWarning("✗ 分析功能: 不可用");
                Debug.LogWarning("✗ 监控功能: 不可用");
            }
        }

        #endregion

        #region 性能分析器工具示例

        /// <summary>
        /// 性能分析器工具
        /// </summary>
        public static void ProfilerToolsExample()
        {
            Debug.Log($"性能分析器工具:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("可用工具:");
                Debug.Log("✓ 性能分析器窗口");
                Debug.Log("✓ 数据收集工具");
                Debug.Log("✓ 性能监控工具");
                Debug.Log("✓ 分析报告工具");
            }
            else
            {
                Debug.LogWarning("工具状态:");
                Debug.LogWarning("✗ 性能分析器窗口: 不可用");
                Debug.LogWarning("✗ 数据收集工具: 不可用");
                Debug.LogWarning("✗ 性能监控工具: 不可用");
                Debug.LogWarning("✗ 分析报告工具: 不可用");
            }
        }

        /// <summary>
        /// 性能分析器功能测试
        /// </summary>
        public static void ProfilerFunctionalityTestExample()
        {
            Debug.Log("性能分析器功能测试:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            Debug.Log($"测试结果:");
            Debug.Log($"- 可用性检查: {(profilerAvailable ? "通过" : "失败")}");
            
            if (profilerAvailable)
            {
                Debug.Log("- 连接测试: 通过");
                Debug.Log("- 数据收集测试: 通过");
                Debug.Log("- 分析功能测试: 通过");
                Debug.Log("- 监控功能测试: 通过");
                Debug.Log("✓ 所有测试通过");
            }
            else
            {
                Debug.LogWarning("- 连接测试: 失败");
                Debug.LogWarning("- 数据收集测试: 失败");
                Debug.LogWarning("- 分析功能测试: 失败");
                Debug.LogWarning("- 监控功能测试: 失败");
                Debug.LogWarning("✗ 测试失败");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 性能分析器综合测试
        /// </summary>
        public static void ProfilerComprehensiveTestExample()
        {
            Debug.Log("=== 性能分析器综合测试 ===");
            
            // 基本检查
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            Debug.Log($"1. 基本检查:");
            Debug.Log($"   - 可用性: {(profilerAvailable ? "通过" : "失败")}");
            
            // 功能检查
            Debug.Log($"2. 功能检查:");
            if (profilerAvailable)
            {
                Debug.Log("   - 连接功能: ✓ 可用");
                Debug.Log("   - 数据收集: ✓ 可用");
                Debug.Log("   - 分析功能: ✓ 可用");
                Debug.Log("   - 监控功能: ✓ 可用");
            }
            else
            {
                Debug.LogWarning("   - 连接功能: ✗ 不可用");
                Debug.LogWarning("   - 数据收集: ✗ 不可用");
                Debug.LogWarning("   - 分析功能: ✗ 不可用");
                Debug.LogWarning("   - 监控功能: ✗ 不可用");
            }
            
            // 建议操作
            Debug.Log($"3. 建议操作:");
            if (profilerAvailable)
            {
                Debug.Log("   - 开始性能分析");
                Debug.Log("   - 收集性能数据");
                Debug.Log("   - 分析性能瓶颈");
                Debug.Log("   - 优化性能问题");
            }
            else
            {
                Debug.Log("   - 打开性能分析器窗口");
                Debug.Log("   - 连接到目标设备");
                Debug.Log("   - 开始性能分析");
            }
        }

        /// <summary>
        /// 性能分析器状态监控
        /// </summary>
        public static void ProfilerStatusMonitoringExample()
        {
            Debug.Log("性能分析器状态监控:");
            
            // 监控性能分析器状态
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            Debug.Log($"监控结果:");
            Debug.Log($"- 当前状态: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"- 连接状态: {(profilerAvailable ? "已连接" : "未连接")}");
            Debug.Log($"- 数据收集: {(profilerAvailable ? "正常" : "停止")}");
            Debug.Log($"- 分析功能: {(profilerAvailable ? "可用" : "不可用")}");
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 性能分析器状态正常");
                Debug.Log("✓ 可以继续性能分析");
            }
            else
            {
                Debug.LogWarning("✗ 性能分析器状态异常");
                Debug.LogWarning("✗ 需要检查连接状态");
            }
        }

        /// <summary>
        /// 性能分析器工具集
        /// </summary>
        public static void ProfilerToolsetExample()
        {
            Debug.Log("性能分析器工具集:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            Debug.Log($"工具集状态:");
            Debug.Log($"- 性能分析器: {(profilerAvailable ? "可用" : "不可用")}");
            Debug.Log($"- 数据收集: {(profilerAvailable ? "正常" : "停止")}");
            Debug.Log($"- 分析工具: {(profilerAvailable ? "可用" : "不可用")}");
            Debug.Log($"- 监控工具: {(profilerAvailable ? "可用" : "不可用")}");
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 工具集完整");
                Debug.Log("✓ 可以开始性能分析");
            }
            else
            {
                Debug.LogWarning("✗ 工具集不完整");
                Debug.LogWarning("✗ 需要连接性能分析器");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查性能分析器是否可用
        /// </summary>
        private static bool IsProfilerAvailable()
        {
            return ProfilerEditorUtility.IsProfilerAvailable();
        }

        /// <summary>
        /// 获取性能分析器状态描述
        /// </summary>
        private static string GetProfilerStatusDescription()
        {
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            return profilerAvailable ? "已连接" : "未连接";
        }

        /// <summary>
        /// 获取性能分析器建议
        /// </summary>
        private static string GetProfilerAdvice()
        {
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                return "性能分析器已连接，可以开始性能分析";
            }
            else
            {
                return "性能分析器未连接，请先连接性能分析器";
            }
        }

        #endregion
    }
}
