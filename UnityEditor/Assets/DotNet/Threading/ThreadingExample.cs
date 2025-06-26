// ThreadingExample.cs
// .NET多线程API使用详解示例
// 包含Task、Thread、并发集合、同步原语、取消令牌、并行编程等
// 每个方法、关键步骤、枚举值均有详细中文注释
// 适合.NET初学者学习和查阅
// 
// 主要功能模块：
// 1. Task<T> - 现代任务编程模型，支持异步操作和返回值
// 2. async/await - 现代异步编程模式，简化异步代码
// 3. Thread - 传统线程编程，直接控制线程生命周期
// 4. 并发集合 - 线程安全的集合类型
// 5. 同步原语 - 线程同步和互斥机制
// 6. 取消令牌 - 任务取消和超时控制
// 7. 并行编程 - 数据并行和任务并行
// 8. 高级线程操作 - 线程池、异步流等

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace DotNet.Threading
{
    /// <summary>
    /// .NET多线程API使用详解示例
    /// 演示Task、Thread、并发集合、同步原语、取消令牌、并行编程等
    /// 
    /// 重要说明：
    /// - Task是.NET中推荐的任务编程模型，优于直接使用Thread
    /// - async/await模式简化了异步代码的编写和维护
    /// - 并发集合提供线程安全的操作，避免手动同步
    /// - 同步原语用于控制多线程访问共享资源
    /// - 取消令牌支持优雅的任务取消和超时处理
    /// - 跨平台注意：线程行为在不同平台可能略有差异
    /// </summary>
    public class ThreadingExample : MonoBehaviour
    {
        [Header("多线程示例配置")]
        [Tooltip("是否自动运行所有示例代码")]
        [SerializeField] private bool runExamples = true;
        [Tooltip("并发任务数量 - 用于演示批量任务管理")]
        [SerializeField] private int taskCount = 5;
        [Tooltip("任务执行延迟时间（毫秒）")]
        [SerializeField] private int taskDelayMs = 100;
        [Tooltip("线程同步超时时间（毫秒）")]
        [SerializeField] private int syncTimeoutMs = 5000;

        private void Start()
        {
            if (runExamples)
            {
                RunAllExamples();
            }
        }

        /// <summary>
        /// 运行所有多线程相关示例
        /// 按顺序执行：Task -> 异步等待 -> Thread -> 并发集合 -> 同步原语 -> 取消令牌 -> 并行编程 -> 高级线程操作
        /// 
        /// 执行流程：
        /// 1. 基础任务编程模型
        /// 2. 现代异步编程模式
        /// 3. 传统线程编程
        /// 4. 线程安全集合操作
        /// 5. 线程同步和互斥
        /// 6. 任务取消和超时
        /// 7. 并行数据处理
        /// 8. 高级线程功能
        /// </summary>
        private async void RunAllExamples()
        {
            Debug.Log("=== .NET多线程API示例开始 ===");
            Debug.Log($"并发任务数量: {taskCount}");
            Debug.Log($"任务延迟时间: {taskDelayMs}毫秒");
            Debug.Log($"同步超时时间: {syncTimeoutMs}毫秒");
            
            TaskExample();              // Task<T> 任务
            await AsyncAwaitExample();  // async/await 异步编程
            ThreadExample();            // Thread 线程
            ConcurrentCollectionsExample(); // 并发集合
            SynchronizationExample();   // 同步原语
            CancellationTokenExample(); // 取消令牌
            ParallelExample();          // 并行编程
            AdvancedThreadingExample(); // 高级线程操作
            
            Debug.Log("=== .NET多线程API示例结束 ===");
        }

        // ================= Task<T> 任务 =================
        /// <summary>
        /// Task<T> 任务示例
        /// Task是.NET中推荐的任务编程模型，支持异步操作和返回值
        /// 
        /// 主要特性：
        /// - 基于线程池，自动管理线程资源
        /// - 支持返回值和无返回值任务
        /// - 内置异常处理和状态管理
        /// - 支持任务延续和组合
        /// - 提供丰富的等待和同步选项
        /// 
        /// 注意事项：
        /// - Task.Run适合CPU密集型任务
        /// - 避免在Task中执行长时间阻塞操作
        /// - 正确处理Task异常（AggregateException）
        /// - 使用ContinueWith时注意异常传播
        /// - 考虑使用CancellationToken支持取消
        /// </summary>
        private void TaskExample()
        {
            Debug.Log("--- Task<T> 任务示例 ---");
            
            try
            {
                // ========== 创建和启动Task ==========
                
                // Task.Run: 在线程池中执行任务
                // 参数说明：Action - 要执行的无返回值委托
                // 返回值：Task - 表示异步操作的任务对象
                // 注意事项：任务立即开始执行，无需手动启动
                Task task1 = Task.Run(() =>
                {
                    Debug.Log($"Task1执行中，线程ID: {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(taskDelayMs);
                    Debug.Log("Task1完成");
                });
                
                // Task.Run: 带返回值的任务
                // 参数说明：Func<T> - 要执行的有返回值委托
                // 返回值：Task<T> - 包含返回值的任务对象
                // 注意事项：泛型参数T指定返回值类型
                Task<int> task2 = Task.Run(() =>
                {
                    Debug.Log($"Task2执行中，线程ID: {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(taskDelayMs / 2);
                    return 42;
                });
                
                // ========== 等待Task完成 ==========
                
                // Wait: 同步等待任务完成
                // 参数说明：无参数或可选的超时时间
                // 返回值：bool - 是否在超时前完成
                // 注意事项：会阻塞当前线程直到任务完成
                bool waitResult = task1.Wait();
                Debug.Log($"Task1等待完成: {waitResult}");
                
                // Result: 获取任务返回值（会阻塞直到完成）
                // 参数说明：无
                // 返回值：T - 任务的返回值
                // 注意事项：如果任务抛出异常，会重新抛出AggregateException
                int result = task2.Result;
                Debug.Log($"Task2返回值: {result}");
                
                // ========== 批量任务管理 ==========
                
                // 创建多个Task
                // 参数说明：taskCount - 要创建的任务数量
                // 返回值：List<Task> - 任务列表
                var tasks = new List<Task>();
                for (int i = 0; i < taskCount; i++)
                {
                    int taskId = i; // 捕获循环变量
                    tasks.Add(Task.Run(() =>
                    {
                        Debug.Log($"Task{taskId}开始执行，线程ID: {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(taskDelayMs);
                        Debug.Log($"Task{taskId}完成");
                    }));
                }
                
                // WaitAll: 等待所有任务完成
                // 参数说明：Task[] - 要等待的任务数组
                // 返回值：void，会阻塞直到所有任务完成
                // 注意事项：如果任何任务抛出异常，会抛出AggregateException
                Task.WaitAll(tasks.ToArray());
                Debug.Log("所有Task已完成");
                
                // WaitAny: 等待任意一个任务完成
                // 参数说明：Task[] - 要等待的任务数组
                // 返回值：int - 第一个完成的任务索引
                // 注意事项：只等待第一个完成的任务，其他任务继续执行
                var longTasks = new List<Task>();
                for (int i = 0; i < 3; i++)
                {
                    int taskId = i;
                    longTasks.Add(Task.Run(() =>
                    {
                        Thread.Sleep(1000 + taskId * 500);
                        Debug.Log($"长任务{taskId}完成");
                    }));
                }
                
                int completedIndex = Task.WaitAny(longTasks.ToArray());
                Debug.Log($"第一个完成的任务索引: {completedIndex}");
                
                // ========== Task状态检查 ==========
                
                // 创建状态检查任务
                var statusTask = Task.Run(() =>
                {
                    Thread.Sleep(taskDelayMs * 2);
                    return "完成";
                });
                
                // 检查任务状态
                // 参数说明：无
                // 返回值：TaskStatus - 任务状态枚举
                // 状态枚举值：
                // - Created: 已创建但未调度
                // - WaitingForActivation: 等待激活
                // - WaitingToRun: 等待运行
                // - Running: 正在运行
                // - WaitingForChildrenToComplete: 等待子任务完成
                // - RanToCompletion: 成功完成
                // - Canceled: 已取消
                // - Faulted: 出现异常
                Debug.Log($"任务状态: {statusTask.Status}");
                statusTask.Wait();
                Debug.Log($"任务完成状态: {statusTask.Status}");
                Debug.Log($"任务结果: {statusTask.Result}");
                
                // ========== 异常处理 ==========
                
                // 创建会抛出异常的任务
                var exceptionTask = Task.Run(() =>
                {
                    throw new InvalidOperationException("任务异常测试");
                });
                
                try
                {
                    // 等待异常任务完成
                    exceptionTask.Wait();
                }
                catch (AggregateException ex)
                {
                    // 处理任务异常
                    // 参数说明：AggregateException - 包含所有内部异常的聚合异常
                    // 返回值：Exception[] - 内部异常数组
                    Debug.LogError($"任务异常: {ex.InnerException?.Message}");
                    Debug.LogError($"异常类型: {ex.InnerException?.GetType().Name}");
                }
                
                // ========== 任务延续 ==========
                
                // 任务延续：一个任务完成后执行另一个任务
                // 参数说明：Func<Task<T>, TResult> - 延续任务委托
                // 返回值：Task<TResult> - 延续任务对象
                // 注意事项：延续任务会接收原任务的结果和状态
                var continuationTask = Task.Run(() =>
                {
                    Debug.Log("原始任务执行");
                    return 100;
                }).ContinueWith(t =>
                {
                    Debug.Log($"延续任务执行，原任务结果: {t.Result}");
                    Debug.Log($"原任务状态: {t.Status}");
                    return t.Result * 2;
                });
                
                int continuationResult = continuationTask.Result;
                Debug.Log($"延续任务结果: {continuationResult}");
                
                // ========== 任务工厂 ==========
                
                // 使用任务工厂创建任务
                // 参数说明：无
                // 返回值：TaskFactory - 任务工厂实例
                var factory = Task.Factory;
                
                // 使用工厂创建任务
                // 参数说明：Func<T> - 要执行的任务委托
                // 返回值：Task<T> - 创建的任务对象
                var factoryTask = factory.StartNew(() =>
                {
                    Debug.Log("工厂创建的任务执行");
                    return "工厂任务结果";
                });
                
                string factoryResult = factoryTask.Result;
                Debug.Log($"工厂任务结果: {factoryResult}");
                
                // ========== 任务完成源 ==========
                
                // TaskCompletionSource: 手动控制任务完成
                // 参数说明：T - 任务返回值类型
                // 返回值：TaskCompletionSource<T> - 任务完成源
                var tcs = new TaskCompletionSource<string>();
                
                // 在另一个任务中完成TaskCompletionSource
                Task.Run(() =>
                {
                    Thread.Sleep(taskDelayMs);
                    tcs.SetResult("手动完成的任务");
                });
                
                string tcsResult = tcs.Task.Result;
                Debug.Log($"TaskCompletionSource结果: {tcsResult}");
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Task操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        // ================= async/await 异步编程 =================
        /// <summary>
        /// async/await 异步编程示例
        /// 现代.NET异步编程模型，简化异步代码编写
        /// 
        /// 主要特性：
        /// - 简化异步代码的编写和维护
        /// - 自动处理异常传播
        /// - 支持取消令牌和进度报告
        /// - 提供异步流支持
        /// - 与Task完美集成
        /// 
        /// 注意事项：
        /// - async方法返回Task或Task<T>
        /// - await会释放线程，不会阻塞
        /// - 避免async void，除非是事件处理程序
        /// - 正确处理异步异常
        /// - 考虑使用ConfigureAwait(false)优化性能
        /// </summary>
        private async Task AsyncAwaitExample()
        {
            Debug.Log("--- async/await 异步编程示例 ---");
            
            try
            {
                // ========== 基本异步操作 ==========
                
                // 异步方法调用
                // 参数说明：delayMs - 延迟时间, workName - 工作名称
                // 返回值：Task<int> - 异步任务
                Debug.Log("开始异步工作...");
                int result1 = await SimulateAsyncWork(taskDelayMs, "工作1");
                Debug.Log($"异步工作1完成，结果: {result1}");
                
                // 并发异步操作
                // 参数说明：多个Task<int> - 并发执行的异步任务
                // 返回值：int[] - 所有任务的结果数组
                Debug.Log("开始并发异步工作...");
                var concurrentTasks = new List<Task<int>>
                {
                    SimulateAsyncWork(taskDelayMs, "并发工作1"),
                    SimulateAsyncWork(taskDelayMs * 2, "并发工作2"),
                    SimulateAsyncWork(taskDelayMs / 2, "并发工作3")
                };
                
                int[] results = await Task.WhenAll(concurrentTasks);
                Debug.Log($"并发工作完成，结果: [{string.Join(", ", results)}]");
                
                // ========== 异步异常处理 ==========
                
                try
                {
                    // 参数说明：workName - 工作名称
                    // 返回值：Task - 异步任务
                    await SimulateAsyncException("异常测试");
                }
                catch (InvalidOperationException ex)
                {
                    Debug.LogError($"捕获异步异常: {ex.Message}");
                }
                
                // ========== 异步取消操作 ==========
                
                // 创建取消令牌源
                // 参数说明：delay - 自动取消延迟时间
                // 返回值：CancellationTokenSource - 取消令牌源
                using (var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(taskDelayMs * 3)))
                {
                    try
                    {
                        // 参数说明：delayMs - 延迟时间, workName - 工作名称, cancellationToken - 取消令牌
                        // 返回值：Task<int> - 异步任务
                        int cancelResult = await SimulateAsyncWorkWithCancellation(taskDelayMs * 5, "可取消工作", cts.Token);
                        Debug.Log($"可取消工作完成: {cancelResult}");
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("异步工作被取消");
                    }
                }
                
                // ========== 异步进度报告 ==========
                
                // 创建进度报告器
                // 参数说明：Action<T> - 进度报告回调
                // 返回值：IProgress<T> - 进度报告接口
                var progress = new Progress<int>(percent =>
                {
                    Debug.Log($"异步工作进度: {percent}%");
                });
                
                // 参数说明：delayMs - 延迟时间, workName - 工作名称, progress - 进度报告器
                // 返回值：Task<int> - 异步任务
                int progressResult = await SimulateAsyncWorkWithProgress(taskDelayMs * 2, "进度报告工作", progress);
                Debug.Log($"进度报告工作完成: {progressResult}");
                
                // ========== 异步流处理 ==========
                
                Debug.Log("开始异步流处理...");
                int count = 0;
                // 参数说明：无
                // 返回值：IAsyncEnumerable<int> - 异步枚举器
                await foreach (int item in GenerateAsyncStream())
                {
                    Debug.Log($"异步流项目{++count}: {item}");
                    if (count >= 5) break; // 限制处理数量
                }
                Debug.Log("异步流处理完成");
                
                // ========== 异步超时处理 ==========
                
                try
                {
                    // 使用Task.Delay实现超时
                    // 参数说明：delay - 延迟时间
                    // 返回值：Task - 延迟任务
                    var timeoutTask = Task.Delay(TimeSpan.FromMilliseconds(taskDelayMs));
                    var workTask = SimulateAsyncWork(taskDelayMs * 3, "超时测试工作");
                    
                    // 等待第一个完成的任务
                    var completedTask = await Task.WhenAny(workTask, timeoutTask);
                    
                    if (completedTask == timeoutTask)
                    {
                        Debug.Log("异步工作超时");
                    }
                    else
                    {
                        int timeoutResult = await workTask;
                        Debug.Log($"超时测试工作完成: {timeoutResult}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"超时处理异常: {ex.Message}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"异步操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// 模拟异步工作
        /// </summary>
        private async Task<int> SimulateAsyncWork(int delayMs, string workName)
        {
            Debug.Log($"{workName}开始，线程ID: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(delayMs);
            Debug.Log($"{workName}完成");
            return delayMs;
        }

        /// <summary>
        /// 模拟异步异常
        /// </summary>
        private async Task SimulateAsyncException(string workName)
        {
            Debug.Log($"{workName}开始");
            await Task.Delay(100);
            throw new InvalidOperationException($"{workName}发生异常");
        }

        /// <summary>
        /// 模拟带取消令牌的异步工作
        /// </summary>
        private async Task SimulateAsyncWorkWithCancellation(int delayMs, string workName, CancellationToken cancellationToken)
        {
            Debug.Log($"{workName}开始");
            await Task.Delay(delayMs, cancellationToken);
            Debug.Log($"{workName}完成");
        }

        /// <summary>
        /// 模拟带进度报告的异步工作
        /// </summary>
        private async Task SimulateAsyncWorkWithProgress(int delayMs, string workName, IProgress<int> progress)
        {
            Debug.Log($"{workName}开始");
            for (int i = 0; i <= 100; i += 10)
            {
                progress?.Report(i);
                await Task.Delay(delayMs / 10);
            }
            Debug.Log($"{workName}完成");
        }

        /// <summary>
        /// 生成异步流
        /// </summary>
        private async IAsyncEnumerable<int> GenerateAsyncStream()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(200);
                yield return i;
            }
        }

        // ================= Thread 线程 =================
        /// <summary>
        /// Thread 线程示例
        /// 直接操作线程，提供更底层的控制
        /// </summary>
        private void ThreadExample()
        {
            Debug.Log("--- Thread 线程示例 ---");
            
            try
            {
                // ========== 创建和启动线程 ==========
                
                // 创建和启动线程
                Thread thread1 = new Thread(() =>
                {
                    Debug.Log($"线程1执行中，线程ID: {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(1000);
                    Debug.Log("线程1完成");
                });
                
                thread1.Start();
                
                // 带参数的线程
                Thread thread2 = new Thread((object param) =>
                {
                    string message = param as string;
                    Debug.Log($"线程2收到参数: {message}");
                    Thread.Sleep(500);
                });
                
                thread2.Start("Hello Thread!");
                
                // ========== 线程状态管理 ==========
                
                // 检查线程状态
                Debug.Log($"线程1状态: {thread1.ThreadState}");
                
                // 等待线程完成
                thread1.Join();
                thread2.Join();
                
                Debug.Log($"线程1完成状态: {thread1.ThreadState}");
                
                // ========== 线程优先级 ==========
                
                var highPriorityThread = new Thread(() =>
                {
                    Debug.Log("高优先级线程执行");
                    Thread.Sleep(100);
                });
                
                highPriorityThread.Priority = ThreadPriority.Highest;
                highPriorityThread.Start();
                highPriorityThread.Join();
                
                // ========== 前台和后台线程 ==========
                
                var backgroundThread = new Thread(() =>
                {
                    Debug.Log("后台线程执行");
                    Thread.Sleep(200);
                });
                
                backgroundThread.IsBackground = true; // 设置为后台线程
                backgroundThread.Start();
                
                // 主线程不等待后台线程，程序可能提前退出
                Debug.Log("主线程继续执行");
                
                // ========== 线程本地存储 ==========
                
                var threadLocal = new ThreadLocal<int>(() => 0);
                
                var tlsThread1 = new Thread(() =>
                {
                    threadLocal.Value = 100;
                    Debug.Log($"线程1的本地值: {threadLocal.Value}");
                });
                
                var tlsThread2 = new Thread(() =>
                {
                    threadLocal.Value = 200;
                    Debug.Log($"线程2的本地值: {threadLocal.Value}");
                });
                
                tlsThread1.Start();
                tlsThread2.Start();
                tlsThread1.Join();
                tlsThread2.Join();
                
                // ========== 线程池 ==========
                
                // 使用线程池执行工作项
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Debug.Log($"线程池工作项执行: {state}");
                }, "线程池任务");
                
                // 获取线程池信息
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
                Debug.Log($"线程池最大工作线程: {maxWorkerThreads}, 最大完成端口线程: {maxCompletionPortThreads}");
                
                // ========== 线程同步 ==========
                
                var sharedCounter = 0;
                var lockObject = new object();
                
                var syncThread1 = new Thread(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        lock (lockObject)
                        {
                            sharedCounter++;
                        }
                    }
                });
                
                var syncThread2 = new Thread(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        lock (lockObject)
                        {
                            sharedCounter++;
                        }
                    }
                });
                
                syncThread1.Start();
                syncThread2.Start();
                syncThread1.Join();
                syncThread2.Join();
                
                Debug.Log($"共享计数器最终值: {sharedCounter}");
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Thread操作出错: {ex.Message}");
            }
        }

        // ================= 并发集合 =================
        /// <summary>
        /// 并发集合示例
        /// 线程安全的集合类型，适用于多线程环境
        /// </summary>
        private void ConcurrentCollectionsExample()
        {
            Debug.Log("--- 并发集合示例 ---");
            
            try
            {
                // ========== ConcurrentQueue<T> 并发队列 ==========
                
                var concurrentQueue = new ConcurrentQueue<int>();
                
                // 并发入队
                Parallel.For(0, 10, i =>
                {
                    concurrentQueue.Enqueue(i);
                    Debug.Log($"入队: {i}");
                });
                
                // 并发出队
                var dequeueResults = new List<int>();
                while (concurrentQueue.TryDequeue(out int item))
                {
                    dequeueResults.Add(item);
                    Debug.Log($"出队: {item}");
                }
                
                Debug.Log($"出队结果: {string.Join(", ", dequeueResults)}");
                
                // ========== ConcurrentStack<T> 并发栈 ==========
                
                var concurrentStack = new ConcurrentStack<int>();
                
                // 并发压栈
                Parallel.For(0, 10, i =>
                {
                    concurrentStack.Push(i);
                    Debug.Log($"压栈: {i}");
                });
                
                // 并发出栈
                var popResults = new List<int>();
                while (concurrentStack.TryPop(out int item))
                {
                    popResults.Add(item);
                    Debug.Log($"出栈: {item}");
                }
                
                Debug.Log($"出栈结果: {string.Join(", ", popResults)}");
                
                // ========== ConcurrentDictionary<TKey, TValue> 并发字典 ==========
                
                var concurrentDict = new ConcurrentDictionary<string, int>();
                
                // 并发添加
                Parallel.For(0, 10, i =>
                {
                    string key = $"Key{i}";
                    concurrentDict.TryAdd(key, i);
                    Debug.Log($"添加: {key} = {i}");
                });
                
                // 并发更新
                Parallel.For(0, 10, i =>
                {
                    string key = $"Key{i}";
                    concurrentDict.AddOrUpdate(key, i * 2, (k, oldValue) => oldValue * 2);
                    Debug.Log($"更新: {key} = {concurrentDict[key]}");
                });
                
                // 遍历并发字典
                foreach (var kvp in concurrentDict)
                {
                    Debug.Log($"字典项: {kvp.Key} = {kvp.Value}");
                }
                
                // ========== ConcurrentBag<T> 并发包 ==========
                
                var concurrentBag = new ConcurrentBag<int>();
                
                // 并发添加
                Parallel.For(0, 10, i =>
                {
                    concurrentBag.Add(i);
                    Debug.Log($"添加到包: {i}");
                });
                
                // 并发取出
                var bagResults = new List<int>();
                while (concurrentBag.TryTake(out int item))
                {
                    bagResults.Add(item);
                    Debug.Log($"从包取出: {item}");
                }
                
                Debug.Log($"包结果: {string.Join(", ", bagResults)}");
                
                // ========== BlockingCollection<T> 阻塞集合 ==========
                
                var blockingCollection = new BlockingCollection<int>(10); // 容量为10
                
                // 生产者任务
                var producer = Task.Run(() =>
                {
                    for (int i = 0; i < 20; i++)
                    {
                        blockingCollection.Add(i);
                        Debug.Log($"生产者添加: {i}");
                        Thread.Sleep(50);
                    }
                    blockingCollection.CompleteAdding();
                });
                
                // 消费者任务
                var consumer = Task.Run(() =>
                {
                    foreach (int item in blockingCollection.GetConsumingEnumerable())
                    {
                        Debug.Log($"消费者取出: {item}");
                        Thread.Sleep(100);
                    }
                });
                
                Task.WaitAll(producer, consumer);
                Debug.Log("阻塞集合操作完成");
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"并发集合操作出错: {ex.Message}");
            }
        }

        // ================= 同步原语 =================
        /// <summary>
        /// 同步原语示例
        /// 用于线程同步和协调的机制
        /// </summary>
        private void SynchronizationExample()
        {
            Debug.Log("--- 同步原语示例 ---");
            
            try
            {
                // ========== lock 关键字 ==========
                
                var lockCounter = 0;
                var lockObject = new object();
                
                var lockTasks = new List<Task>();
                for (int i = 0; i < 5; i++)
                {
                    lockTasks.Add(Task.Run(() =>
                    {
                        for (int j = 0; j < 1000; j++)
                        {
                            lock (lockObject)
                            {
                                lockCounter++;
                            }
                        }
                    }));
                }
                
                Task.WaitAll(lockTasks.ToArray());
                Debug.Log($"lock计数器结果: {lockCounter}");
                
                // ========== Monitor 监视器 ==========
                
                var monitorCounter = 0;
                var monitorObject = new object();
                
                var monitorTasks = new List<Task>();
                for (int i = 0; i < 5; i++)
                {
                    monitorTasks.Add(Task.Run(() =>
                    {
                        for (int j = 0; j < 1000; j++)
                        {
                            Monitor.Enter(monitorObject);
                            try
                            {
                                monitorCounter++;
                            }
                            finally
                            {
                                Monitor.Exit(monitorObject);
                            }
                        }
                    }));
                }
                
                Task.WaitAll(monitorTasks.ToArray());
                Debug.Log($"Monitor计数器结果: {monitorCounter}");
                
                // ========== Mutex 互斥体 ==========
                
                var mutex = new Mutex();
                var mutexCounter = 0;
                
                var mutexTasks = new List<Task>();
                for (int i = 0; i < 5; i++)
                {
                    mutexTasks.Add(Task.Run(() =>
                    {
                        for (int j = 0; j < 1000; j++)
                        {
                            mutex.WaitOne();
                            try
                            {
                                mutexCounter++;
                            }
                            finally
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }));
                }
                
                Task.WaitAll(mutexTasks.ToArray());
                mutex.Dispose();
                Debug.Log($"Mutex计数器结果: {mutexCounter}");
                
                // ========== SemaphoreSlim 信号量 ==========
                
                var semaphore = new SemaphoreSlim(3, 3); // 最多3个并发
                var semaphoreCounter = 0;
                var semaphoreLock = new object();
                
                var semaphoreTasks = new List<Task>();
                for (int i = 0; i < 10; i++)
                {
                    int taskId = i;
                    semaphoreTasks.Add(Task.Run(async () =>
                    {
                        await semaphore.WaitAsync();
                        try
                        {
                            Debug.Log($"任务{taskId}获得信号量");
                            lock (semaphoreLock)
                            {
                                semaphoreCounter++;
                            }
                            await Task.Delay(100);
                        }
                        finally
                        {
                            semaphore.Release();
                            Debug.Log($"任务{taskId}释放信号量");
                        }
                    }));
                }
                
                Task.WaitAll(semaphoreTasks.ToArray());
                semaphore.Dispose();
                Debug.Log($"信号量计数器结果: {semaphoreCounter}");
                
                // ========== AutoResetEvent 自动重置事件 ==========
                
                var autoResetEvent = new AutoResetEvent(false);
                var autoResetCounter = 0;
                
                var autoResetTasks = new List<Task>();
                for (int i = 0; i < 5; i++)
                {
                    int taskId = i;
                    autoResetTasks.Add(Task.Run(() =>
                    {
                        autoResetEvent.WaitOne();
                        lock (semaphoreLock)
                        {
                            autoResetCounter++;
                        }
                        Debug.Log($"任务{taskId}处理完成，计数器: {autoResetCounter}");
                    }));
                }
                
                // 逐个触发事件
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(100);
                    autoResetEvent.Set();
                }
                
                Task.WaitAll(autoResetTasks.ToArray());
                autoResetEvent.Dispose();
                Debug.Log($"自动重置事件计数器结果: {autoResetCounter}");
                
                // ========== ManualResetEventSlim 手动重置事件 ==========
                
                var manualResetEvent = new ManualResetEventSlim(false);
                var manualResetCounter = 0;
                
                var manualResetTasks = new List<Task>();
                for (int i = 0; i < 5; i++)
                {
                    int taskId = i;
                    manualResetTasks.Add(Task.Run(() =>
                    {
                        manualResetEvent.Wait();
                        lock (semaphoreLock)
                        {
                            manualResetCounter++;
                        }
                        Debug.Log($"任务{taskId}处理完成，计数器: {manualResetCounter}");
                    }));
                }
                
                // 触发所有等待的任务
                Thread.Sleep(100);
                manualResetEvent.Set();
                
                Task.WaitAll(manualResetTasks.ToArray());
                manualResetEvent.Dispose();
                Debug.Log($"手动重置事件计数器结果: {manualResetCounter}");
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"同步原语操作出错: {ex.Message}");
            }
        }

        // ================= 取消令牌 =================
        /// <summary>
        /// 取消令牌示例
        /// 用于协调任务取消的机制
        /// </summary>
        private void CancellationTokenExample()
        {
            Debug.Log("--- 取消令牌示例 ---");
            
            try
            {
                // ========== 基本取消操作 ==========
                
                var cts = new CancellationTokenSource();
                var token = cts.Token;
                
                var cancelTask = Task.Run(async () =>
                {
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            token.ThrowIfCancellationRequested();
                            Debug.Log($"任务执行中: {i}");
                            await Task.Delay(200);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("任务被取消");
                    }
                }, token);
                
                // 延迟取消
                await Task.Delay(1000);
                cts.Cancel();
                
                try
                {
                    await cancelTask;
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("任务已取消");
                }
                
                // ========== 超时取消 ==========
                
                var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
                var timeoutTask = Task.Run(async () =>
                {
                    try
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            timeoutCts.Token.ThrowIfCancellationRequested();
                            Debug.Log($"超时任务执行中: {i}");
                            await Task.Delay(100);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("任务因超时被取消");
                    }
                }, timeoutCts.Token);
                
                try
                {
                    await timeoutTask;
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("超时任务已取消");
                }
                
                // ========== 链接取消令牌 ==========
                
                var cts1 = new CancellationTokenSource();
                var cts2 = new CancellationTokenSource();
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);
                
                var linkedTask = Task.Run(async () =>
                {
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            linkedCts.Token.ThrowIfCancellationRequested();
                            Debug.Log($"链接任务执行中: {i}");
                            await Task.Delay(200);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("链接任务被取消");
                    }
                }, linkedCts.Token);
                
                // 取消其中一个令牌
                await Task.Delay(500);
                cts1.Cancel();
                
                try
                {
                    await linkedTask;
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("链接任务已取消");
                }
                
                linkedCts.Dispose();
                
                // ========== 注册取消回调 ==========
                
                var callbackCts = new CancellationTokenSource();
                callbackCts.Token.Register(() =>
                {
                    Debug.Log("取消回调被调用");
                });
                
                var callbackTask = Task.Run(async () =>
                {
                    try
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            callbackCts.Token.ThrowIfCancellationRequested();
                            Debug.Log($"回调任务执行中: {i}");
                            await Task.Delay(300);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("回调任务被取消");
                    }
                }, callbackCts.Token);
                
                await Task.Delay(800);
                callbackCts.Cancel();
                
                try
                {
                    await callbackTask;
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("回调任务已取消");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"取消令牌操作出错: {ex.Message}");
            }
        }

        // ================= 并行编程 =================
        /// <summary>
        /// 并行编程示例
        /// 使用Parallel类进行数据并行和任务并行
        /// </summary>
        private void ParallelExample()
        {
            Debug.Log("--- 并行编程示例 ---");
            
            try
            {
                // ========== Parallel.For 数据并行 ==========
                
                var parallelSum = 0;
                var parallelLock = new object();
                
                Parallel.For(0, 1000, i =>
                {
                    lock (parallelLock)
                    {
                        parallelSum += i;
                    }
                });
                
                Debug.Log($"并行求和结果: {parallelSum}");
                
                // ========== Parallel.ForEach 集合并行 ==========
                
                var numbers = Enumerable.Range(0, 1000).ToList();
                var parallelMax = 0;
                
                Parallel.ForEach(numbers, number =>
                {
                    lock (parallelLock)
                    {
                        if (number > parallelMax)
                            parallelMax = number;
                    }
                });
                
                Debug.Log($"并行最大值: {parallelMax}");
                
                // ========== Parallel.Invoke 任务并行 ==========
                
                var invokeResults = new int[3];
                
                Parallel.Invoke(
                    () =>
                    {
                        Debug.Log("并行任务1执行");
                        invokeResults[0] = 100;
                    },
                    () =>
                    {
                        Debug.Log("并行任务2执行");
                        invokeResults[1] = 200;
                    },
                    () =>
                    {
                        Debug.Log("并行任务3执行");
                        invokeResults[2] = 300;
                    }
                );
                
                Debug.Log($"并行任务结果: {string.Join(", ", invokeResults)}");
                
                // ========== 并行选项 ==========
                
                var parallelOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 2, // 最大并行度
                    CancellationToken = CancellationToken.None
                };
                
                var limitedSum = 0;
                Parallel.For(0, 100, parallelOptions, i =>
                {
                    lock (parallelLock)
                    {
                        limitedSum += i;
                    }
                });
                
                Debug.Log($"限制并行度求和结果: {limitedSum}");
                
                // ========== 并行LINQ (PLINQ) ==========
                
                var plinqNumbers = Enumerable.Range(0, 10000);
                
                var plinqSum = plinqNumbers.AsParallel().Sum();
                Debug.Log($"PLINQ求和结果: {plinqSum}");
                
                var plinqFiltered = plinqNumbers.AsParallel()
                    .Where(x => x % 2 == 0)
                    .Select(x => x * x)
                    .Take(10)
                    .ToList();
                
                Debug.Log($"PLINQ过滤结果: {string.Join(", ", plinqFiltered)}");
                
                // ========== 并行异常处理 ==========
                
                try
                {
                    Parallel.For(0, 10, i =>
                    {
                        if (i == 5)
                            throw new InvalidOperationException($"任务{i}异常");
                        
                        Debug.Log($"任务{i}执行");
                    });
                }
                catch (AggregateException ex)
                {
                    Debug.LogError($"并行任务异常: {ex.InnerException?.Message}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"并行编程操作出错: {ex.Message}");
            }
        }

        // ================= 高级线程操作 =================
        /// <summary>
        /// 高级线程操作示例
        /// 包括线程池、定时器、异步流等高级功能
        /// </summary>
        private void AdvancedThreadingExample()
        {
            Debug.Log("--- 高级线程操作示例 ---");
            
            try
            {
                // ========== 定时器 ==========
                
                var timer = new Timer(state =>
                {
                    Debug.Log($"定时器触发: {DateTime.Now:HH:mm:ss}");
                }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
                
                // 运行5秒后停止
                Thread.Sleep(5000);
                timer.Dispose();
                Debug.Log("定时器已停止");
                
                // ========== 异步流 ==========
                
                _ = Task.Run(async () =>
                {
                    await foreach (int item in GenerateAsyncStream())
                    {
                        Debug.Log($"异步流项: {item}");
                        if (item >= 5) break;
                    }
                });
                
                // ========== 线程池配置 ==========
                
                ThreadPool.GetMinThreads(out int minWorkerThreads, out int minCompletionPortThreads);
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
                
                Debug.Log($"线程池配置 - 最小工作线程: {minWorkerThreads}, 最大工作线程: {maxWorkerThreads}");
                Debug.Log($"线程池配置 - 最小完成端口线程: {minCompletionPortThreads}, 最大完成端口线程: {maxCompletionPortThreads}");
                
                // ========== 线程本地存储 ==========
                
                var threadLocalStorage = new ThreadLocal<string>(() => $"线程{Thread.CurrentThread.ManagedThreadId}的本地存储");
                
                var tlsTasks = new List<Task>();
                for (int i = 0; i < 3; i++)
                {
                    tlsTasks.Add(Task.Run(() =>
                    {
                        Debug.Log($"线程本地存储: {threadLocalStorage.Value}");
                        threadLocalStorage.Value = $"修改后的值 - 线程{Thread.CurrentThread.ManagedThreadId}";
                        Debug.Log($"修改后: {threadLocalStorage.Value}");
                    }));
                }
                
                Task.WaitAll(tlsTasks.ToArray());
                threadLocalStorage.Dispose();
                
                // ========== 内存屏障 ==========
                
                var barrier = new Barrier(3, (b) =>
                {
                    Debug.Log($"所有线程到达屏障点 {b.CurrentPhaseNumber}");
                });
                
                var barrierTasks = new List<Task>();
                for (int i = 0; i < 3; i++)
                {
                    int taskId = i;
                    barrierTasks.Add(Task.Run(() =>
                    {
                        Debug.Log($"任务{taskId}开始");
                        Thread.Sleep(100 + taskId * 100);
                        Debug.Log($"任务{taskId}到达屏障");
                        barrier.SignalAndWait();
                        Debug.Log($"任务{taskId}通过屏障");
                    }));
                }
                
                Task.WaitAll(barrierTasks.ToArray());
                barrier.Dispose();
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"高级线程操作出错: {ex.Message}");
            }
        }
    }
} 