using UnityEngine;
using UnityEditor;
using Unity.Profiling;
using Unity.Profiling.Editor;
using System.Collections.Generic;

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
                Debug.Log("- 性能分析器已连接");
                Debug.Log("- 可以开始性能分析");
            }
            else
            {
                Debug.LogWarning("- 性能分析器未连接");
            }
        }

        /// <summary>
        /// 获取性能分析器连接状态
        /// </summary>
        public static void GetProfilerConnectionStateExample()
        {
            Debug.Log($"性能分析器连接状态:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            Debug.Log($"- 连接状态: {(profilerAvailable ? "已连接" : "未连接")}");
            
            if (profilerAvailable)
            {
                Debug.Log("- 可以开始性能分析");
                Debug.Log("- 可以获取性能数据");
            }
            else
            {
                Debug.LogWarning("- 无法进行性能分析");
                Debug.LogWarning("- 请检查性能分析器连接");
            }
        }

        #endregion

        #region 性能数据获取示例

        /// <summary>
        /// 获取性能数据
        /// </summary>
        public static void GetProfilerDataExample()
        {
            Debug.Log($"获取性能数据:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法获取数据");
                return;
            }
            
            // 获取性能数据
            ProfilerData profilerData = ProfilerEditorUtility.GetProfilerData();
            Debug.Log($"- 性能数据: {profilerData}");
            
            if (profilerData != null)
            {
                Debug.Log("- 性能数据获取成功");
                Debug.Log("- 可以分析性能指标");
            }
            else
            {
                Debug.LogWarning("- 性能数据获取失败");
            }
        }

        /// <summary>
        /// 获取性能分析器内存数据
        /// </summary>
        public static void GetProfilerMemoryExample()
        {
            Debug.Log($"获取性能分析器内存数据:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法获取内存数据");
                return;
            }
            
            // 获取内存数据
            ProfilerMemory profilerMemory = ProfilerEditorUtility.GetProfilerMemory();
            Debug.Log($"- 内存数据: {profilerMemory}");
            
            if (profilerMemory != null)
            {
                Debug.Log("- 内存数据获取成功");
                Debug.Log("- 可以分析内存使用情况");
            }
            else
            {
                Debug.LogWarning("- 内存数据获取失败");
            }
        }

        /// <summary>
        /// 获取性能分析器操作数据
        /// </summary>
        public static void GetProfilerOperationsExample()
        {
            Debug.Log($"获取性能分析器操作数据:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法获取操作数据");
                return;
            }
            
            // 获取操作数据
            ProfilerOperations profilerOperations = ProfilerEditorUtility.GetProfilerOperations();
            Debug.Log($"- 操作数据: {profilerOperations}");
            
            if (profilerOperations != null)
            {
                Debug.Log("- 操作数据获取成功");
                Debug.Log("- 可以分析操作性能");
            }
            else
            {
                Debug.LogWarning("- 操作数据获取失败");
            }
        }

        #endregion

        #region 性能分析器工具示例

        /// <summary>
        /// 获取性能分析器工具
        /// </summary>
        public static void GetProfilerToolsExample()
        {
            Debug.Log($"获取性能分析器工具:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法获取工具");
                return;
            }
            
            // 获取工具
            ProfilerTools profilerTools = ProfilerEditorUtility.GetProfilerTools();
            Debug.Log($"- 工具: {profilerTools}");
            
            if (profilerTools != null)
            {
                Debug.Log("- 工具获取成功");
                Debug.Log("- 可以使用性能分析工具");
            }
            else
            {
                Debug.LogWarning("- 工具获取失败");
            }
        }

        /// <summary>
        /// 获取性能分析器状态
        /// </summary>
        public static void GetProfilerStatusExample()
        {
            Debug.Log($"获取性能分析器状态:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法获取状态");
                return;
            }
            
            // 获取状态
            ProfilerStatus profilerStatus = ProfilerEditorUtility.GetProfilerStatus();
            Debug.Log($"- 状态: {profilerStatus}");
            
            if (profilerStatus != null)
            {
                Debug.Log("- 状态获取成功");
                Debug.Log("- 可以查看性能分析器状态");
            }
            else
            {
                Debug.LogWarning("- 状态获取失败");
            }
        }

        /// <summary>
        /// 获取性能分析器信息
        /// </summary>
        public static void GetProfilerInfoDetailedExample()
        {
            Debug.Log($"获取性能分析器详细信息:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法获取信息");
                return;
            }
            
            // 获取信息
            ProfilerInfo profilerInfo = ProfilerEditorUtility.GetProfilerInfo();
            Debug.Log($"- 信息: {profilerInfo}");
            
            if (profilerInfo != null)
            {
                Debug.Log("- 信息获取成功");
                Debug.Log("- 可以查看性能分析器详细信息");
            }
            else
            {
                Debug.LogWarning("- 信息获取失败");
            }
        }

        #endregion

        #region 高级性能分析示例

        /// <summary>
        /// 性能分析器高级功能
        /// </summary>
        public static void ProfilerAdvancedFeaturesExample()
        {
            Debug.Log($"性能分析器高级功能:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法使用高级功能");
                return;
            }
            
            // 启用高级功能
            ProfilerEditorUtility.EnableAdvancedFeatures(true);
            Debug.Log("- 高级功能已启用");
            
            // 设置采样率
            ProfilerEditorUtility.SetSamplingRate(1000);
            Debug.Log("- 采样率已设置为1000Hz");
            
            // 启用内存分析
            ProfilerEditorUtility.EnableMemoryProfiling(true);
            Debug.Log("- 内存分析已启用");
            
            // 启用GPU分析
            ProfilerEditorUtility.EnableGPUProfiling(true);
            Debug.Log("- GPU分析已启用");
        }

        /// <summary>
        /// 性能分析器配置
        /// </summary>
        public static void ProfilerConfigurationExample()
        {
            Debug.Log($"性能分析器配置:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法配置");
                return;
            }
            
            // 配置性能分析器
            ProfilerConfiguration config = new ProfilerConfiguration
            {
                enableCPUProfiling = true,
                enableMemoryProfiling = true,
                enableGPUProfiling = true,
                samplingRate = 1000,
                maxFrames = 300
            };
            
            ProfilerEditorUtility.SetConfiguration(config);
            Debug.Log("- 配置已设置");
            Debug.Log($"  - CPU分析: {config.enableCPUProfiling}");
            Debug.Log($"  - 内存分析: {config.enableMemoryProfiling}");
            Debug.Log($"  - GPU分析: {config.enableGPUProfiling}");
            Debug.Log($"  - 采样率: {config.samplingRate}Hz");
            Debug.Log($"  - 最大帧数: {config.maxFrames}");
        }

        /// <summary>
        /// 性能分析器监控
        /// </summary>
        public static void ProfilerMonitoringExample()
        {
            Debug.Log($"性能分析器监控:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法监控");
                return;
            }
            
            // 开始监控
            ProfilerEditorUtility.StartMonitoring();
            Debug.Log("- 监控已开始");
            
            // 设置监控回调
            ProfilerEditorUtility.SetMonitoringCallback(OnProfilerDataReceived);
            Debug.Log("- 监控回调已设置");
            
            // 设置监控间隔
            ProfilerEditorUtility.SetMonitoringInterval(0.1f);
            Debug.Log("- 监控间隔已设置为0.1秒");
        }

        /// <summary>
        /// 性能数据接收回调
        /// </summary>
        private static void OnProfilerDataReceived(ProfilerData data)
        {
            Debug.Log($"接收到性能数据: {data}");
        }

        #endregion

        #region 性能分析器工具示例

        /// <summary>
        /// 性能分析器工具函数
        /// </summary>
        public static void ProfilerToolsExample()
        {
            Debug.Log($"性能分析器工具函数:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法使用工具");
                return;
            }
            
            // 获取工具列表
            List<ProfilerTool> tools = ProfilerEditorUtility.GetAvailableTools();
            Debug.Log($"- 可用工具数量: {tools.Count}");
            
            foreach (ProfilerTool tool in tools)
            {
                Debug.Log($"  - 工具: {tool.name}, 类型: {tool.type}");
            }
            
            // 激活工具
            if (tools.Count > 0)
            {
                ProfilerEditorUtility.ActivateTool(tools[0]);
                Debug.Log($"- 工具 {tools[0].name} 已激活");
            }
        }

        /// <summary>
        /// 性能分析器验证
        /// </summary>
        public static void ProfilerValidationExample()
        {
            Debug.Log($"性能分析器验证:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法验证");
                return;
            }
            
            // 验证性能分析器
            bool isValid = ProfilerEditorUtility.ValidateProfiler();
            Debug.Log($"- 验证结果: {(isValid ? "有效" : "无效")}");
            
            if (isValid)
            {
                Debug.Log("- 性能分析器配置正确");
                Debug.Log("- 可以正常使用");
            }
            else
            {
                Debug.LogWarning("- 性能分析器配置有问题");
                Debug.LogWarning("- 请检查配置");
            }
        }

        #endregion

        #region 性能分析器管理示例

        /// <summary>
        /// 性能分析器管理
        /// </summary>
        public static void ProfilerManagementExample()
        {
            Debug.Log($"性能分析器管理:");
            
            // 获取所有性能分析器
            List<ProfilerEditorUtility> profilers = ProfilerEditorUtility.GetAllProfilers();
            Debug.Log($"- 性能分析器数量: {profilers.Count}");
            
            foreach (ProfilerEditorUtility profiler in profilers)
            {
                Debug.Log($"  - 性能分析器: {profiler.name}");
            }
            
            // 管理性能分析器
            if (profilers.Count > 0)
            {
                ProfilerEditorUtility profiler = profilers[0];
                
                // 启动性能分析器
                ProfilerEditorUtility.StartProfiler(profiler);
                Debug.Log($"- 性能分析器 {profiler.name} 已启动");
                
                // 停止性能分析器
                ProfilerEditorUtility.StopProfiler(profiler);
                Debug.Log($"- 性能分析器 {profiler.name} 已停止");
            }
        }

        /// <summary>
        /// 性能分析器统计
        /// </summary>
        public static void ProfilerStatisticsExample()
        {
            Debug.Log($"性能分析器统计:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法统计");
                return;
            }
            
            // 获取统计信息
            ProfilerStatistics stats = ProfilerEditorUtility.GetStatistics();
            Debug.Log($"- 统计信息: {stats}");
            
            if (stats != null)
            {
                Debug.Log($"  - 总帧数: {stats.totalFrames}");
                Debug.Log($"  - 平均FPS: {stats.averageFPS}");
                Debug.Log($"  - 内存使用: {stats.memoryUsage}MB");
                Debug.Log($"  - CPU使用率: {stats.cpuUsage}%");
                Debug.Log($"  - GPU使用率: {stats.gpuUsage}%");
            }
            else
            {
                Debug.LogWarning("- 统计信息获取失败");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建自定义性能分析器
        /// </summary>
        public static void CreateCustomProfilerExample()
        {
            Debug.Log($"创建自定义性能分析器:");
            
            // 创建自定义性能分析器
            CustomProfiler customProfiler = new CustomProfiler("MyCustomProfiler");
            
            // 配置性能分析器
            customProfiler.Configure(new ProfilerConfiguration
            {
                enableCPUProfiling = true,
                enableMemoryProfiling = true,
                samplingRate = 500,
                maxFrames = 200
            });
            
            // 启动性能分析器
            customProfiler.Start();
            Debug.Log("- 自定义性能分析器已创建并启动");
            
            // 停止性能分析器
            customProfiler.Stop();
            Debug.Log("- 自定义性能分析器已停止");
        }

        /// <summary>
        /// 性能分析器最佳实践
        /// </summary>
        public static void ProfilerBestPracticesExample()
        {
            Debug.Log($"性能分析器最佳实践:");
            
            bool profilerAvailable = ProfilerEditorUtility.IsProfilerAvailable();
            if (!profilerAvailable)
            {
                Debug.LogWarning("性能分析器未连接，无法演示最佳实践");
                return;
            }
            
            // 1. 设置合适的采样率
            ProfilerEditorUtility.SetSamplingRate(1000);
            Debug.Log("✓ 采样率设置为1000Hz");
            
            // 2. 启用必要的分析模块
            ProfilerEditorUtility.EnableCPUProfiling(true);
            ProfilerEditorUtility.EnableMemoryProfiling(true);
            Debug.Log("✓ CPU和内存分析已启用");
            
            // 3. 设置合理的帧数限制
            ProfilerEditorUtility.SetMaxFrames(300);
            Debug.Log("✓ 最大帧数设置为300");
            
            // 4. 使用性能标记
            ProfilerEditorUtility.BeginSample("MyPerformanceTest");
            Debug.Log("✓ 性能标记已开始");
            
            // 模拟一些工作
            System.Threading.Thread.Sleep(100);
            
            ProfilerEditorUtility.EndSample();
            Debug.Log("✓ 性能标记已结束");
            
            Debug.Log("✓ 性能分析器最佳实践演示完成");
        }

        #endregion

        #region 数据结构

        /// <summary>
        /// 性能分析器配置
        /// </summary>
        [System.Serializable]
        public class ProfilerConfiguration
        {
            public bool enableCPUProfiling;
            public bool enableMemoryProfiling;
            public bool enableGPUProfiling;
            public int samplingRate;
            public int maxFrames;
        }

        /// <summary>
        /// 性能分析器统计信息
        /// </summary>
        [System.Serializable]
        public class ProfilerStatistics
        {
            public int totalFrames;
            public float averageFPS;
            public float memoryUsage;
            public float cpuUsage;
            public float gpuUsage;
        }

        /// <summary>
        /// 性能分析器工具
        /// </summary>
        [System.Serializable]
        public class ProfilerTool
        {
            public string name;
            public string type;
        }

        /// <summary>
        /// 自定义性能分析器
        /// </summary>
        public class CustomProfiler
        {
            private string name;
            private ProfilerConfiguration configuration;
            private bool isRunning;

            public CustomProfiler(string name)
            {
                this.name = name;
            }

            public void Configure(ProfilerConfiguration config)
            {
                this.configuration = config;
            }

            public void Start()
            {
                isRunning = true;
                Debug.Log($"自定义性能分析器 {name} 已启动");
            }

            public void Stop()
            {
                isRunning = false;
                Debug.Log($"自定义性能分析器 {name} 已停止");
            }
        }

        #endregion

        #region 高级性能分析示例

        /// <summary>
        /// 性能分析器配置
        /// </summary>
        public static void ProfilerConfigurationExample()
        {
            Debug.Log("=== 性能分析器配置 ===");
            
            // 设置性能分析器配置
            ProfilerConfiguration config = new ProfilerConfiguration();
            config.enableProfiler = true;
            config.enableMemoryProfiler = true;
            config.enableGPUProfiler = true;
            config.enableNetworkProfiler = true;
            
            Debug.Log($"性能分析器配置: {config}");
            
            // 应用配置
            ProfilerEditorUtility.SetProfilerConfiguration(config);
            Debug.Log("性能分析器配置已应用");
        }

        /// <summary>
        /// 性能分析器数据导出
        /// </summary>
        public static void ProfilerDataExportExample()
        {
            Debug.Log("=== 性能分析器数据导出 ===");
            
            // 导出性能数据
            string exportPath = "Assets/ProfilerData.raw";
            bool exportSuccess = ProfilerEditorUtility.ExportProfilerData(exportPath);
            
            if (exportSuccess)
            {
                Debug.Log($"性能数据已导出到: {exportPath}");
            }
            else
            {
                Debug.LogWarning("性能数据导出失败");
            }
            
            // 导出内存快照
            string memorySnapshotPath = "Assets/MemorySnapshot.mem";
            bool snapshotSuccess = ProfilerEditorUtility.ExportMemorySnapshot(memorySnapshotPath);
            
            if (snapshotSuccess)
            {
                Debug.Log($"内存快照已导出到: {memorySnapshotPath}");
            }
            else
            {
                Debug.LogWarning("内存快照导出失败");
            }
        }

        /// <summary>
        /// 性能分析器数据导入
        /// </summary>
        public static void ProfilerDataImportExample()
        {
            Debug.Log("=== 性能分析器数据导入 ===");
            
            // 导入性能数据
            string importPath = "Assets/ProfilerData.raw";
            if (System.IO.File.Exists(importPath))
            {
                bool importSuccess = ProfilerEditorUtility.ImportProfilerData(importPath);
                
                if (importSuccess)
                {
                    Debug.Log($"性能数据已从 {importPath} 导入");
                }
                else
                {
                    Debug.LogWarning("性能数据导入失败");
                }
            }
            else
            {
                Debug.LogWarning($"导入文件不存在: {importPath}");
            }
        }

        /// <summary>
        /// 性能分析器窗口管理
        /// </summary>
        public static void ProfilerWindowManagementExample()
        {
            Debug.Log("=== 性能分析器窗口管理 ===");
            
            // 打开性能分析器窗口
            ProfilerEditorUtility.OpenProfilerWindow();
            Debug.Log("性能分析器窗口已打开");
            
            // 关闭性能分析器窗口
            ProfilerEditorUtility.CloseProfilerWindow();
            Debug.Log("性能分析器窗口已关闭");
            
            // 检查窗口状态
            bool isWindowOpen = ProfilerEditorUtility.IsProfilerWindowOpen();
            Debug.Log($"性能分析器窗口是否打开: {isWindowOpen}");
            
            // 最大化窗口
            ProfilerEditorUtility.MaximizeProfilerWindow();
            Debug.Log("性能分析器窗口已最大化");
            
            // 最小化窗口
            ProfilerEditorUtility.MinimizeProfilerWindow();
            Debug.Log("性能分析器窗口已最小化");
        }

        /// <summary>
        /// 性能分析器录制控制
        /// </summary>
        public static void ProfilerRecordingControlExample()
        {
            Debug.Log("=== 性能分析器录制控制 ===");
            
            // 开始录制
            ProfilerEditorUtility.StartProfilerRecording();
            Debug.Log("性能分析器录制已开始");
            
            // 停止录制
            ProfilerEditorUtility.StopProfilerRecording();
            Debug.Log("性能分析器录制已停止");
            
            // 暂停录制
            ProfilerEditorUtility.PauseProfilerRecording();
            Debug.Log("性能分析器录制已暂停");
            
            // 恢复录制
            ProfilerEditorUtility.ResumeProfilerRecording();
            Debug.Log("性能分析器录制已恢复");
            
            // 检查录制状态
            bool isRecording = ProfilerEditorUtility.IsProfilerRecording();
            Debug.Log($"是否正在录制: {isRecording}");
            
            bool isPaused = ProfilerEditorUtility.IsProfilerPaused();
            Debug.Log($"是否已暂停: {isPaused}");
        }

        #endregion

        #region 性能分析器工具示例

        /// <summary>
        /// 性能分析器工具管理
        /// </summary>
        public static void ProfilerToolsManagementExample()
        {
            Debug.Log("=== 性能分析器工具管理 ===");
            
            // 获取可用工具
            ProfilerTool[] availableTools = ProfilerEditorUtility.GetAvailableProfilerTools();
            Debug.Log($"可用工具数量: {availableTools.Length}");
            
            foreach (ProfilerTool tool in availableTools)
            {
                Debug.Log($"工具: {tool.name} ({tool.type})");
            }
            
            // 激活工具
            if (availableTools.Length > 0)
            {
                ProfilerEditorUtility.ActivateProfilerTool(availableTools[0]);
                Debug.Log($"工具 {availableTools[0].name} 已激活");
            }
            
            // 停用工具
            ProfilerEditorUtility.DeactivateProfilerTool();
            Debug.Log("当前工具已停用");
            
            // 获取当前活动工具
            ProfilerTool activeTool = ProfilerEditorUtility.GetActiveProfilerTool();
            if (activeTool != null)
            {
                Debug.Log($"当前活动工具: {activeTool.name}");
            }
            else
            {
                Debug.Log("没有活动工具");
            }
        }

        /// <summary>
        /// 性能分析器数据查询
        /// </summary>
        public static void ProfilerDataQueryExample()
        {
            Debug.Log("=== 性能分析器数据查询 ===");
            
            // 查询CPU数据
            ProfilerData cpuData = ProfilerEditorUtility.GetCPUProfilerData();
            Debug.Log($"CPU数据: {cpuData}");
            
            // 查询内存数据
            ProfilerData memoryData = ProfilerEditorUtility.GetMemoryProfilerData();
            Debug.Log($"内存数据: {memoryData}");
            
            // 查询GPU数据
            ProfilerData gpuData = ProfilerEditorUtility.GetGPUProfilerData();
            Debug.Log($"GPU数据: {gpuData}");
            
            // 查询网络数据
            ProfilerData networkData = ProfilerEditorUtility.GetNetworkProfilerData();
            Debug.Log($"网络数据: {networkData}");
            
            // 查询渲染数据
            ProfilerData renderData = ProfilerEditorUtility.GetRenderProfilerData();
            Debug.Log($"渲染数据: {renderData}");
        }

        /// <summary>
        /// 性能分析器统计
        /// </summary>
        public static void ProfilerStatisticsExample()
        {
            Debug.Log("=== 性能分析器统计 ===");
            
            // 获取统计信息
            ProfilerStatistics stats = ProfilerEditorUtility.GetProfilerStatistics();
            Debug.Log($"性能统计: {stats}");
            
            // 获取帧率统计
            float averageFPS = ProfilerEditorUtility.GetAverageFPS();
            Debug.Log($"平均帧率: {averageFPS} FPS");
            
            float minFPS = ProfilerEditorUtility.GetMinFPS();
            Debug.Log($"最低帧率: {minFPS} FPS");
            
            float maxFPS = ProfilerEditorUtility.GetMaxFPS();
            Debug.Log($"最高帧率: {maxFPS} FPS");
            
            // 获取内存统计
            long totalMemory = ProfilerEditorUtility.GetTotalMemory();
            Debug.Log($"总内存: {totalMemory / 1024 / 1024} MB");
            
            long usedMemory = ProfilerEditorUtility.GetUsedMemory();
            Debug.Log($"已用内存: {usedMemory / 1024 / 1024} MB");
            
            long freeMemory = ProfilerEditorUtility.GetFreeMemory();
            Debug.Log($"空闲内存: {freeMemory / 1024 / 1024} MB");
        }

        #endregion

        #region 性能分析器调试示例

        /// <summary>
        /// 性能分析器调试
        /// </summary>
        public static void ProfilerDebuggingExample()
        {
            Debug.Log("=== 性能分析器调试 ===");
            
            // 启用调试模式
            ProfilerEditorUtility.EnableProfilerDebugging(true);
            Debug.Log("性能分析器调试模式已启用");
            
            // 设置调试级别
            ProfilerEditorUtility.SetProfilerDebugLevel(ProfilerDebugLevel.Verbose);
            Debug.Log("调试级别已设置为详细");
            
            // 获取调试信息
            string debugInfo = ProfilerEditorUtility.GetProfilerDebugInfo();
            Debug.Log($"调试信息: {debugInfo}");
            
            // 清除调试信息
            ProfilerEditorUtility.ClearProfilerDebugInfo();
            Debug.Log("调试信息已清除");
            
            // 禁用调试模式
            ProfilerEditorUtility.EnableProfilerDebugging(false);
            Debug.Log("性能分析器调试模式已禁用");
        }

        /// <summary>
        /// 性能分析器错误处理
        /// </summary>
        public static void ProfilerErrorHandlingExample()
        {
            Debug.Log("=== 性能分析器错误处理 ===");
            
            // 检查错误
            bool hasErrors = ProfilerEditorUtility.HasProfilerErrors();
            Debug.Log($"是否有错误: {hasErrors}");
            
            if (hasErrors)
            {
                // 获取错误信息
                string[] errors = ProfilerEditorUtility.GetProfilerErrors();
                Debug.Log($"错误数量: {errors.Length}");
                
                foreach (string error in errors)
                {
                    Debug.LogError($"错误: {error}");
                }
                
                // 清除错误
                ProfilerEditorUtility.ClearProfilerErrors();
                Debug.Log("错误已清除");
            }
            
            // 检查警告
            bool hasWarnings = ProfilerEditorUtility.HasProfilerWarnings();
            Debug.Log($"是否有警告: {hasWarnings}");
            
            if (hasWarnings)
            {
                // 获取警告信息
                string[] warnings = ProfilerEditorUtility.GetProfilerWarnings();
                Debug.Log($"警告数量: {warnings.Length}");
                
                foreach (string warning in warnings)
                {
                    Debug.LogWarning($"警告: {warning}");
                }
                
                // 清除警告
                ProfilerEditorUtility.ClearProfilerWarnings();
                Debug.Log("警告已清除");
            }
        }

        #endregion

        #region 综合示例