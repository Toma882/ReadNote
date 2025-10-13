using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Diagnostics
{
    /// <summary>
    /// .NET Diagnostics (诊断) API 综合示例
    /// 展示System.Diagnostics和Microsoft.Extensions.Logging命名空间中所有主要功能
    /// </summary>
    public class DiagnosticsExample
    {
        #region 基础日志示例

        /// <summary>
        /// Debug和Trace日志示例
        /// </summary>
        public static void BasicLoggingExample()
        {
            Console.WriteLine("=== Debug和Trace日志示例 ===");
            
            // Debug日志
            Debug.WriteLine("这是Debug日志消息");
            Debug.WriteLineIf(true, "这是条件Debug日志");
            Debug.Assert(true, "这是Debug断言");
            
            // Trace日志
            Trace.WriteLine("这是Trace日志消息");
            Trace.WriteLineIf(true, "这是条件Trace日志");
            Trace.Assert(true, "这是Trace断言");
            
            // 使用TraceSource
            TraceSource traceSource = new TraceSource("MyTraceSource", SourceLevels.All);
            traceSource.TraceInformation("这是TraceSource信息");
            traceSource.TraceEvent(TraceEventType.Warning, 1, "这是警告事件");
            traceSource.TraceEvent(TraceEventType.Error, 2, "这是错误事件");
            traceSource.Flush();
            traceSource.Close();
            
            Console.WriteLine("基础日志示例完成");
        }

        /// <summary>
        /// 结构化日志示例
        /// </summary>
        public static void StructuredLoggingExample()
        {
            Console.WriteLine("\n=== 结构化日志示例 ===");
            
            // 创建日志工厂
            using (ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            }))
            {
                ILogger logger = loggerFactory.CreateLogger<DiagnosticsExample>();
                
                // 不同级别的日志
                logger.LogTrace("这是Trace级别日志");
                logger.LogDebug("这是Debug级别日志");
                logger.LogInformation("这是Information级别日志");
                logger.LogWarning("这是Warning级别日志");
                logger.LogError("这是Error级别日志");
                logger.LogCritical("这是Critical级别日志");
                
                // 结构化日志
                string userId = "user123";
                string action = "login";
                DateTime timestamp = DateTime.Now;
                
                logger.LogInformation("用户 {UserId} 执行了 {Action} 操作，时间: {Timestamp}", 
                    userId, action, timestamp);
                
                // 使用日志作用域
                using (logger.BeginScope("操作ID: {OperationId}", Guid.NewGuid()))
                {
                    logger.LogInformation("在作用域内的日志消息");
                    logger.LogWarning("作用域内的警告消息");
                }
                
                // 异常日志
                try
                {
                    throw new InvalidOperationException("这是一个测试异常");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "捕获到异常: {Message}", ex.Message);
                }
            }
        }

        #endregion

        #region 性能监控示例

        /// <summary>
        /// Stopwatch性能计时示例
        /// </summary>
        public static void StopwatchExample()
        {
            Console.WriteLine("\n=== Stopwatch性能计时示例 ===");
            
            // 基本计时
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            // 模拟一些工作
            Thread.Sleep(100);
            
            stopwatch.Stop();
            Console.WriteLine($"执行时间: {stopwatch.ElapsedMilliseconds} 毫秒");
            Console.WriteLine($"执行时间: {stopwatch.ElapsedTicks} 时钟周期");
            Console.WriteLine($"高精度时间: {stopwatch.Elapsed}");
            
            // 重置和重新计时
            stopwatch.Restart();
            Thread.Sleep(50);
            stopwatch.Stop();
            Console.WriteLine($"重新计时: {stopwatch.ElapsedMilliseconds} 毫秒");
            
            // 检查是否正在运行
            Console.WriteLine($"是否正在运行: {stopwatch.IsRunning}");
        }

        /// <summary>
        /// 性能计数器示例
        /// </summary>
        public static void PerformanceCounterExample()
        {
            Console.WriteLine("\n=== 性能计数器示例 ===");
            
            try
            {
                // 获取CPU使用率
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                float cpuUsage = cpuCounter.NextValue();
                Console.WriteLine($"CPU使用率: {cpuUsage:F2}%");
                
                // 获取内存使用情况
                PerformanceCounter memoryCounter = new PerformanceCounter("Memory", "Available MBytes");
                float availableMemory = memoryCounter.NextValue();
                Console.WriteLine($"可用内存: {availableMemory:F2} MB");
                
                // 获取进程信息
                Process currentProcess = Process.GetCurrentProcess();
                PerformanceCounter processCpuCounter = new PerformanceCounter("Process", "% Processor Time", currentProcess.ProcessName);
                float processCpuUsage = processCpuCounter.NextValue();
                Console.WriteLine($"当前进程CPU使用率: {processCpuUsage:F2}%");
                
                // 清理资源
                cpuCounter.Dispose();
                memoryCounter.Dispose();
                processCpuCounter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"性能计数器异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 进程监控示例
        /// </summary>
        public static void ProcessMonitoringExample()
        {
            Console.WriteLine("\n=== 进程监控示例 ===");
            
            try
            {
                // 获取当前进程
                Process currentProcess = Process.GetCurrentProcess();
                Console.WriteLine($"当前进程信息:");
                Console.WriteLine($"  进程ID: {currentProcess.Id}");
                Console.WriteLine($"  进程名: {currentProcess.ProcessName}");
                Console.WriteLine($"  主窗口标题: {currentProcess.MainWindowTitle}");
                Console.WriteLine($"  启动时间: {currentProcess.StartTime}");
                Console.WriteLine($"  运行时间: {DateTime.Now - currentProcess.StartTime}");
                Console.WriteLine($"  工作集内存: {currentProcess.WorkingSet64 / 1024 / 1024} MB");
                Console.WriteLine($"  虚拟内存: {currentProcess.VirtualMemorySize64 / 1024 / 1024} MB");
                Console.WriteLine($"  句柄数: {currentProcess.HandleCount}");
                Console.WriteLine($"  线程数: {currentProcess.Threads.Count}");
                
                // 获取所有进程
                Process[] allProcesses = Process.GetProcesses();
                Console.WriteLine($"\n系统进程总数: {allProcesses.Length}");
                
                // 显示前10个进程
                Console.WriteLine("\n前10个进程:");
                var topProcesses = allProcesses
                    .OrderByDescending(p => p.WorkingSet64)
                    .Take(10);
                
                foreach (var process in topProcesses)
                {
                    try
                    {
                        Console.WriteLine($"  {process.ProcessName} - {process.WorkingSet64 / 1024 / 1024} MB");
                    }
                    catch
                    {
                        // 忽略无法访问的进程
                    }
                }
                
                // 清理资源
                foreach (var process in allProcesses)
                {
                    process.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"进程监控异常: {ex.Message}");
            }
        }

        #endregion

        #region 活动跟踪示例

        /// <summary>
        /// Activity活动跟踪示例
        /// </summary>
        public static void ActivityTracingExample()
        {
            Console.WriteLine("\n=== Activity活动跟踪示例 ===");
            
            // 创建活动源
            ActivitySource activitySource = new ActivitySource("MyApplication", "1.0.0");
            
            // 创建根活动
            using (Activity rootActivity = activitySource.StartActivity("RootOperation"))
            {
                rootActivity?.SetTag("operation.type", "root");
                rootActivity?.SetTag("user.id", "user123");
                
                Console.WriteLine($"根活动ID: {rootActivity?.Id}");
                Console.WriteLine($"根活动名称: {rootActivity?.DisplayName}");
                
                // 创建子活动
                using (Activity childActivity = activitySource.StartActivity("ChildOperation"))
                {
                    childActivity?.SetTag("operation.type", "child");
                    childActivity?.SetTag("operation.duration", "100ms");
                    
                    Console.WriteLine($"子活动ID: {childActivity?.Id}");
                    Console.WriteLine($"子活动父ID: {childActivity?.ParentId}");
                    
                    // 模拟一些工作
                    Thread.Sleep(50);
                    
                    // 添加事件
                    childActivity?.AddEvent(new ActivityEvent("ProcessingCompleted"));
                }
                
                // 创建另一个子活动
                using (Activity anotherChildActivity = activitySource.StartActivity("AnotherChildOperation"))
                {
                    anotherChildActivity?.SetTag("operation.type", "another-child");
                    
                    Console.WriteLine($"另一个子活动ID: {anotherChildActivity?.Id}");
                    
                    // 模拟工作
                    Thread.Sleep(30);
                }
            }
            
            Console.WriteLine("活动跟踪示例完成");
        }

        #endregion

        #region 内存监控示例

        /// <summary>
        /// 垃圾回收监控示例
        /// </summary>
        public static void GarbageCollectionExample()
        {
            Console.WriteLine("\n=== 垃圾回收监控示例 ===");
            
            // 获取GC信息
            Console.WriteLine($"GC总内存: {GC.GetTotalMemory(false) / 1024 / 1024} MB");
            Console.WriteLine($"GC代数: {GC.MaxGeneration + 1}");
            Console.WriteLine($"第0代集合次数: {GC.CollectionCount(0)}");
            Console.WriteLine($"第1代集合次数: {GC.CollectionCount(1)}");
            Console.WriteLine($"第2代集合次数: {GC.CollectionCount(2)}");
            
            // 强制垃圾回收
            Console.WriteLine("\n执行垃圾回收前:");
            Console.WriteLine($"GC总内存: {GC.GetTotalMemory(false) / 1024 / 1024} MB");
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            Console.WriteLine("执行垃圾回收后:");
            Console.WriteLine($"GC总内存: {GC.GetTotalMemory(true) / 1024 / 1024} MB");
            
            // 内存压力测试
            Console.WriteLine("\n内存压力测试:");
            List<byte[]> memoryList = new List<byte[]>();
            
            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    memoryList.Add(new byte[1024 * 1024]); // 1MB
                    
                    if (i % 100 == 0)
                    {
                        Console.WriteLine($"已分配 {i + 1} MB 内存");
                        Console.WriteLine($"GC总内存: {GC.GetTotalMemory(false) / 1024 / 1024} MB");
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("内存不足异常");
            }
            finally
            {
                memoryList.Clear();
                GC.Collect();
                Console.WriteLine($"清理后GC总内存: {GC.GetTotalMemory(true) / 1024 / 1024} MB");
            }
        }

        /// <summary>
        /// 内存使用分析示例
        /// </summary>
        public static void MemoryUsageAnalysisExample()
        {
            Console.WriteLine("\n=== 内存使用分析示例 ===");
            
            Process currentProcess = Process.GetCurrentProcess();
            
            Console.WriteLine($"进程内存信息:");
            Console.WriteLine($"  工作集: {currentProcess.WorkingSet64 / 1024 / 1024} MB");
            Console.WriteLine($"  虚拟内存: {currentProcess.VirtualMemorySize64 / 1024 / 1024} MB");
            Console.WriteLine($"  分页内存: {currentProcess.PagedMemorySize64 / 1024 / 1024} MB");
            Console.WriteLine($"  非分页内存: {currentProcess.NonpagedSystemMemorySize64 / 1024 / 1024} MB");
            Console.WriteLine($"  分页系统内存: {currentProcess.PagedSystemMemorySize64 / 1024 / 1024} MB");
            
            // 获取系统内存信息
            Console.WriteLine($"\n系统内存信息:");
            Console.WriteLine($"  总物理内存: {GC.GetTotalMemory(false) / 1024 / 1024} MB");
            
            // 监控内存分配
            long initialMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"\n初始内存: {initialMemory / 1024 / 1024} MB");
            
            // 分配一些内存
            List<string> strings = new List<string>();
            for (int i = 0; i < 10000; i++)
            {
                strings.Add($"String {i} - " + new string('A', 100));
            }
            
            long afterAllocation = GC.GetTotalMemory(false);
            Console.WriteLine($"分配后内存: {afterAllocation / 1024 / 1024} MB");
            Console.WriteLine($"内存增长: {(afterAllocation - initialMemory) / 1024 / 1024} MB");
            
            // 清理内存
            strings.Clear();
            strings = null;
            GC.Collect();
            
            long afterCleanup = GC.GetTotalMemory(true);
            Console.WriteLine($"清理后内存: {afterCleanup / 1024 / 1024} MB");
            Console.WriteLine($"内存释放: {(afterAllocation - afterCleanup) / 1024 / 1024} MB");
        }

        #endregion

        #region 调试支持示例

        /// <summary>
        /// 调试器支持示例
        /// </summary>
        public static void DebuggerSupportExample()
        {
            Console.WriteLine("\n=== 调试器支持示例 ===");
            
            // 检查调试器是否附加
            bool isDebuggerAttached = Debugger.IsAttached;
            Console.WriteLine($"调试器是否附加: {isDebuggerAttached}");
            
            // 调试器断点
            Debugger.Break();
            Console.WriteLine("调试器断点后继续执行");
            
            // 调试器日志
            Debugger.Log(0, "Category", "这是调试器日志消息");
            
            // 条件调试
            bool debugMode = true;
            Debugger.Log(debugMode ? 0 : 1, "Debug", debugMode ? "调试模式开启" : "调试模式关闭");
            
            Console.WriteLine("调试器支持示例完成");
        }

        /// <summary>
        /// 条件编译示例
        /// </summary>
        public static void ConditionalCompilationExample()
        {
            Console.WriteLine("\n=== 条件编译示例 ===");
            
            // DEBUG条件编译
            #if DEBUG
            Console.WriteLine("这是DEBUG模式下的代码");
            #endif
            
            // RELEASE条件编译
            #if RELEASE
            Console.WriteLine("这是RELEASE模式下的代码");
            #endif
            
            // 自定义条件编译
            #if TRACE
            Console.WriteLine("这是TRACE模式下的代码");
            #endif
            
            // 条件方法
            ConditionalMethod();
            
            Console.WriteLine("条件编译示例完成");
        }

        [Conditional("DEBUG")]
        private static void ConditionalMethod()
        {
            Console.WriteLine("这是条件编译方法，只在DEBUG模式下执行");
        }

        #endregion

        #region 事件日志示例

        /// <summary>
        /// 事件日志示例
        /// </summary>
        public static void EventLogExample()
        {
            Console.WriteLine("\n=== 事件日志示例 ===");
            
            try
            {
                // 检查事件日志是否存在
                string logName = "Application";
                if (EventLog.SourceExists("MyApplication"))
                {
                    EventLog.DeleteEventSource("MyApplication");
                }
                
                // 创建事件源
                EventLog.CreateEventSource("MyApplication", logName);
                
                // 写入事件日志
                EventLog eventLog = new EventLog(logName);
                eventLog.Source = "MyApplication";
                
                eventLog.WriteEntry("这是信息级别的事件日志", EventLogEntryType.Information);
                eventLog.WriteEntry("这是警告级别的事件日志", EventLogEntryType.Warning);
                eventLog.WriteEntry("这是错误级别的事件日志", EventLogEntryType.Error);
                
                Console.WriteLine("事件日志写入完成");
                
                // 读取事件日志
                Console.WriteLine("\n最近的事件日志:");
                EventLogEntryCollection entries = eventLog.Entries;
                int count = 0;
                for (int i = entries.Count - 1; i >= 0 && count < 5; i--)
                {
                    EventLogEntry entry = entries[i];
                    if (entry.Source == "MyApplication")
                    {
                        Console.WriteLine($"  {entry.TimeGenerated}: {entry.EntryType} - {entry.Message}");
                        count++;
                    }
                }
                
                eventLog.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"事件日志异常: {ex.Message}");
            }
        }

        #endregion

        #region 性能分析示例

        /// <summary>
        /// 性能分析示例
        /// </summary>
        public static void PerformanceAnalysisExample()
        {
            Console.WriteLine("\n=== 性能分析示例 ===");
            
            // 方法执行时间分析
            AnalyzeMethodPerformance();
            
            // 内存分配分析
            AnalyzeMemoryAllocation();
            
            // 并发性能分析
            AnalyzeConcurrencyPerformance();
        }

        private static void AnalyzeMethodPerformance()
        {
            Console.WriteLine("\n方法执行时间分析:");
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            // 模拟不同复杂度的操作
            for (int i = 0; i < 1000000; i++)
            {
                Math.Sqrt(i);
            }
            
            stopwatch.Stop();
            Console.WriteLine($"数学运算耗时: {stopwatch.ElapsedMilliseconds} ms");
            
            // 字符串操作性能
            stopwatch.Restart();
            string result = "";
            for (int i = 0; i < 10000; i++)
            {
                result += i.ToString();
            }
            stopwatch.Stop();
            Console.WriteLine($"字符串拼接耗时: {stopwatch.ElapsedMilliseconds} ms");
            
            // StringBuilder性能
            stopwatch.Restart();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append(i.ToString());
            }
            string sbResult = sb.ToString();
            stopwatch.Stop();
            Console.WriteLine($"StringBuilder耗时: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void AnalyzeMemoryAllocation()
        {
            Console.WriteLine("\n内存分配分析:");
            
            long initialMemory = GC.GetTotalMemory(false);
            
            // 分配大量小对象
            List<object> objects = new List<object>();
            for (int i = 0; i < 100000; i++)
            {
                objects.Add(new { Id = i, Name = $"Object{i}" });
            }
            
            long afterAllocation = GC.GetTotalMemory(false);
            Console.WriteLine($"分配100,000个小对象后内存增长: {(afterAllocation - initialMemory) / 1024} KB");
            
            // 分配少量大对象
            List<byte[]> largeObjects = new List<byte[]>();
            for (int i = 0; i < 100; i++)
            {
                largeObjects.Add(new byte[1024 * 1024]); // 1MB each
            }
            
            long afterLargeAllocation = GC.GetTotalMemory(false);
            Console.WriteLine($"分配100个大对象后内存增长: {(afterLargeAllocation - afterAllocation) / 1024 / 1024} MB");
            
            // 清理
            objects.Clear();
            largeObjects.Clear();
            GC.Collect();
            
            long afterCleanup = GC.GetTotalMemory(true);
            Console.WriteLine($"清理后内存: {afterCleanup / 1024 / 1024} MB");
        }

        private static void AnalyzeConcurrencyPerformance()
        {
            Console.WriteLine("\n并发性能分析:");
            
            // 单线程性能
            Stopwatch stopwatch = Stopwatch.StartNew();
            long singleThreadSum = 0;
            for (int i = 0; i < 1000000; i++)
            {
                singleThreadSum += i;
            }
            stopwatch.Stop();
            Console.WriteLine($"单线程计算耗时: {stopwatch.ElapsedMilliseconds} ms, 结果: {singleThreadSum}");
            
            // 多线程性能
            stopwatch.Restart();
            long multiThreadSum = 0;
            object lockObject = new object();
            
            Parallel.For(0, 1000000, i =>
            {
                lock (lockObject)
                {
                    multiThreadSum += i;
                }
            });
            
            stopwatch.Stop();
            Console.WriteLine($"多线程计算耗时: {stopwatch.ElapsedMilliseconds} ms, 结果: {multiThreadSum}");
        }

        #endregion

        #region 主方法

        /// <summary>
        /// 运行所有诊断示例
        /// </summary>
        public static void RunAllExamples()
        {
            Console.WriteLine("C# Diagnostics (诊断) API 综合示例");
            Console.WriteLine("====================================");
            
            try
            {
                BasicLoggingExample();
                StructuredLoggingExample();
                StopwatchExample();
                PerformanceCounterExample();
                ProcessMonitoringExample();
                ActivityTracingExample();
                GarbageCollectionExample();
                MemoryUsageAnalysisExample();
                DebuggerSupportExample();
                ConditionalCompilationExample();
                EventLogExample();
                PerformanceAnalysisExample();
                
                Console.WriteLine("\n所有诊断示例运行完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"运行示例时发生错误: {ex.Message}");
            }
        }

        #endregion
    }
}
