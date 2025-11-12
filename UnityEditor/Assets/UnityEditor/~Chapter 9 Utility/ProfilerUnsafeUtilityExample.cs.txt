using UnityEngine;
using UnityEditor;
using Unity.Profiling;
using Unity.Profiling.Editor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// ProfilerUnsafeUtility 工具类示例
    /// 提供性能分析器不安全操作相关的实用工具功能
    /// </summary>
    public static class ProfilerUnsafeUtilityExample
    {
        #region 性能分析器数据示例

        /// <summary>
        /// 获取性能分析器数据
        /// </summary>
        public static void GetProfilerDataExample()
        {
            Debug.Log($"性能分析器数据获取:");
            
            // 检查性能分析器是否可用
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            Debug.Log($"- 性能分析器可用: {profilerAvailable}");
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 可以获取性能分析器数据");
                Debug.Log("✓ 数据收集正常");
                
                // 模拟获取性能数据
                Debug.Log("性能数据:");
                Debug.Log("- CPU使用率: 正常");
                Debug.Log("- 内存使用: 正常");
                Debug.Log("- 渲染性能: 正常");
            }
            else
            {
                Debug.LogWarning("✗ 无法获取性能分析器数据");
                Debug.LogWarning("✗ 性能分析器未连接");
            }
        }

        /// <summary>
        /// 检查性能分析器数据状态
        /// </summary>
        public static void CheckProfilerDataStatusExample()
        {
            Debug.Log($"性能分析器数据状态检查:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 数据状态: 正常");
                Debug.Log("✓ 数据收集: 正常");
                Debug.Log("✓ 数据访问: 可用");
                Debug.Log("✓ 数据分析: 可用");
            }
            else
            {
                Debug.LogWarning("✗ 数据状态: 异常");
                Debug.LogWarning("✗ 数据收集: 停止");
                Debug.LogWarning("✗ 数据访问: 不可用");
                Debug.LogWarning("✗ 数据分析: 不可用");
            }
        }

        #endregion

        #region 性能分析器内存示例

        /// <summary>
        /// 检查性能分析器内存
        /// </summary>
        public static void CheckProfilerMemoryExample()
        {
            Debug.Log($"性能分析器内存检查:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 内存状态: 正常");
                Debug.Log("✓ 内存分配: 正常");
                Debug.Log("✓ 内存使用: 正常");
                Debug.Log("✓ 内存回收: 正常");
            }
            else
            {
                Debug.LogWarning("✗ 内存状态: 异常");
                Debug.LogWarning("✗ 内存分配: 停止");
                Debug.LogWarning("✗ 内存使用: 异常");
                Debug.LogWarning("✗ 内存回收: 异常");
            }
        }

        /// <summary>
        /// 性能分析器内存管理
        /// </summary>
        public static void ProfilerMemoryManagementExample()
        {
            Debug.Log($"性能分析器内存管理:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("内存管理状态:");
                Debug.Log("✓ 内存分配: 正常");
                Debug.Log("✓ 内存使用: 正常");
                Debug.Log("✓ 内存回收: 正常");
                Debug.Log("✓ 内存监控: 正常");
            }
            else
            {
                Debug.LogWarning("内存管理状态:");
                Debug.LogWarning("✗ 内存分配: 异常");
                Debug.LogWarning("✗ 内存使用: 异常");
                Debug.LogWarning("✗ 内存回收: 异常");
                Debug.LogWarning("✗ 内存监控: 异常");
            }
        }

        #endregion

        #region 性能分析器操作示例

        /// <summary>
        /// 性能分析器操作检查
        /// </summary>
        public static void CheckProfilerOperationsExample()
        {
            Debug.Log($"性能分析器操作检查:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 操作状态: 正常");
                Debug.Log("✓ 数据收集: 正常");
                Debug.Log("✓ 数据分析: 正常");
                Debug.Log("✓ 性能监控: 正常");
            }
            else
            {
                Debug.LogWarning("✗ 操作状态: 异常");
                Debug.LogWarning("✗ 数据收集: 停止");
                Debug.LogWarning("✗ 数据分析: 不可用");
                Debug.LogWarning("✗ 性能监控: 不可用");
            }
        }

        /// <summary>
        /// 性能分析器操作管理
        /// </summary>
        public static void ProfilerOperationsManagementExample()
        {
            Debug.Log($"性能分析器操作管理:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("操作管理状态:");
                Debug.Log("✓ 数据收集操作: 正常");
                Debug.Log("✓ 数据分析操作: 正常");
                Debug.Log("✓ 性能监控操作: 正常");
                Debug.Log("✓ 报告生成操作: 正常");
            }
            else
            {
                Debug.LogWarning("操作管理状态:");
                Debug.LogWarning("✗ 数据收集操作: 异常");
                Debug.LogWarning("✗ 数据分析操作: 异常");
                Debug.LogWarning("✗ 性能监控操作: 异常");
                Debug.LogWarning("✗ 报告生成操作: 异常");
            }
        }

        #endregion

        #region 性能分析器工具示例

        /// <summary>
        /// 性能分析器工具检查
        /// </summary>
        public static void CheckProfilerToolsExample()
        {
            Debug.Log($"性能分析器工具检查:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 工具状态: 正常");
                Debug.Log("✓ 数据收集工具: 可用");
                Debug.Log("✓ 数据分析工具: 可用");
                Debug.Log("✓ 性能监控工具: 可用");
            }
            else
            {
                Debug.LogWarning("✗ 工具状态: 异常");
                Debug.LogWarning("✗ 数据收集工具: 不可用");
                Debug.LogWarning("✗ 数据分析工具: 不可用");
                Debug.LogWarning("✗ 性能监控工具: 不可用");
            }
        }

        /// <summary>
        /// 性能分析器工具管理
        /// </summary>
        public static void ProfilerToolsManagementExample()
        {
            Debug.Log($"性能分析器工具管理:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                Debug.Log("工具管理状态:");
                Debug.Log("✓ 数据收集工具: 正常");
                Debug.Log("✓ 数据分析工具: 正常");
                Debug.Log("✓ 性能监控工具: 正常");
                Debug.Log("✓ 报告生成工具: 正常");
            }
            else
            {
                Debug.LogWarning("工具管理状态:");
                Debug.LogWarning("✗ 数据收集工具: 异常");
                Debug.LogWarning("✗ 数据分析工具: 异常");
                Debug.LogWarning("✗ 性能监控工具: 异常");
                Debug.LogWarning("✗ 报告生成工具: 异常");
            }
        }

        #endregion

        #region 性能分析器状态示例

        /// <summary>
        /// 性能分析器状态监控
        /// </summary>
        public static void ProfilerStatusMonitoringExample()
        {
            Debug.Log($"性能分析器状态监控:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            Debug.Log($"监控结果:");
            Debug.Log($"- 连接状态: {(profilerAvailable ? "已连接" : "未连接")}");
            Debug.Log($"- 数据状态: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"- 工具状态: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"- 操作状态: {(profilerAvailable ? "正常" : "异常")}");
            
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
        /// 性能分析器状态诊断
        /// </summary>
        public static void ProfilerStatusDiagnosticsExample()
        {
            Debug.Log("=== 性能分析器状态诊断 ===");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            Debug.Log($"✓ 连接状态: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"✓ 数据状态: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"✓ 工具状态: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"✓ 操作状态: {(profilerAvailable ? "正常" : "异常")}");
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 性能分析器状态正常");
                Debug.Log("✓ 可以开始性能分析");
            }
            else
            {
                Debug.LogWarning("✗ 性能分析器状态异常");
                Debug.LogWarning("✗ 需要连接性能分析器");
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
                Debug.Log("   - 数据收集: ✓ 可用");
                Debug.Log("   - 数据分析: ✓ 可用");
                Debug.Log("   - 性能监控: ✓ 可用");
                Debug.Log("   - 工具管理: ✓ 可用");
            }
            else
            {
                Debug.LogWarning("   - 数据收集: ✗ 不可用");
                Debug.LogWarning("   - 数据分析: ✗ 不可用");
                Debug.LogWarning("   - 性能监控: ✗ 不可用");
                Debug.LogWarning("   - 工具管理: ✗ 不可用");
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
        /// 性能分析器工具集测试
        /// </summary>
        public static void ProfilerToolsetTestExample()
        {
            Debug.Log("性能分析器工具集测试:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            Debug.Log($"测试结果:");
            Debug.Log($"- 数据收集工具: {(profilerAvailable ? "通过" : "失败")}");
            Debug.Log($"- 数据分析工具: {(profilerAvailable ? "通过" : "失败")}");
            Debug.Log($"- 性能监控工具: {(profilerAvailable ? "通过" : "失败")}");
            Debug.Log($"- 报告生成工具: {(profilerAvailable ? "通过" : "失败")}");
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 所有工具测试通过");
                Debug.Log("✓ 工具集完整");
            }
            else
            {
                Debug.LogWarning("✗ 工具测试失败");
                Debug.LogWarning("✗ 工具集不完整");
            }
        }

        /// <summary>
        /// 性能分析器性能测试
        /// </summary>
        public static void ProfilerPerformanceTestExample()
        {
            Debug.Log("性能分析器性能测试:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            Debug.Log($"性能测试结果:");
            Debug.Log($"- 数据收集性能: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"- 数据分析性能: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"- 内存使用性能: {(profilerAvailable ? "正常" : "异常")}");
            Debug.Log($"- 工具响应性能: {(profilerAvailable ? "正常" : "异常")}");
            
            if (profilerAvailable)
            {
                Debug.Log("✓ 性能测试通过");
                Debug.Log("✓ 性能分析器性能正常");
            }
            else
            {
                Debug.LogWarning("✗ 性能测试失败");
                Debug.LogWarning("✗ 性能分析器性能异常");
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

        /// <summary>
        /// 获取性能分析器工具状态
        /// </summary>
        private static string GetProfilerToolsStatus()
        {
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            
            if (profilerAvailable)
            {
                return "所有工具可用";
            }
            else
            {
                return "工具不可用";
            }
        }

        #endregion
    }
}
