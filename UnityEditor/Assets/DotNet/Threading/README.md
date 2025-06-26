# .NET Threading API 参考文档

本文档基于 `ThreadingExample.cs` 文件，详细介绍了 .NET Threading 相关的所有常用 API。

## 目录
- [Task<T> 任务](#taskt-任务)
- [async/await 异步编程](#asyncawait-异步编程)
- [Thread 线程](#thread-线程)
- [并发集合](#并发集合)
- [同步原语](#同步原语)
- [取消令牌](#取消令牌)
- [并行编程](#并行编程)
- [高级线程操作](#高级线程操作)

---

## Task<T> 任务

### 创建和启动Task

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.Run(action)` | 在线程池中执行任务 |
| `Task.Run<T>(func)` | 在线程池中执行带返回值的任务 |
| `Task.Factory.StartNew(action)` | 使用任务工厂创建任务 |
| `new Task(action)` | 创建任务但不启动 |

### 等待Task完成

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.Wait()` | 同步等待任务完成 |
| `Task<T>.Result` | 获取任务返回值（会阻塞直到完成） |
| `Task.WaitAll(tasks)` | 等待所有任务完成 |
| `Task.WaitAny(tasks)` | 等待任意一个任务完成 |
| `Task.WhenAll(tasks)` | 异步等待所有任务完成 |
| `Task.WhenAny(tasks)` | 异步等待任意一个任务完成 |

### Task状态检查

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.Status` | 获取任务状态 |
| `Task.IsCompleted` | 检查任务是否完成 |
| `Task.IsFaulted` | 检查任务是否出错 |
| `Task.IsCanceled` | 检查任务是否被取消 |

### 异常处理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.Exception` | 获取任务异常 |
| `AggregateException.InnerException` | 获取内部异常 |

### 任务延续

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.ContinueWith(action)` | 创建任务延续 |
| `Task.ContinueWith<T>(func)` | 创建带返回值的任务延续 |

### 任务工厂

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.Factory.StartNew(action)` | 使用工厂创建任务 |
| `Task.Factory.StartNew<T>(func)` | 使用工厂创建带返回值的任务 |

---

## async/await 异步编程

### 基本异步操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `async Task Method()` | 声明异步方法 |
| `async Task<T> Method()` | 声明带返回值的异步方法 |
| `await task` | 等待异步任务完成 |
| `await Task.Delay(milliseconds)` | 异步延迟 |

### 并发异步任务

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.WhenAll(tasks)` | 等待所有异步任务完成 |
| `Task.WhenAny(tasks)` | 等待任意一个异步任务完成 |

### 异步异常处理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `try/catch` | 异步异常处理 |
| `Task.FromException(exception)` | 创建异常任务 |

### 异步超时处理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `CancellationTokenSource(TimeSpan)` | 创建超时取消令牌 |
| `Task.Delay(delay, cancellationToken)` | 带取消令牌的延迟 |

### 异步进度报告

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IProgress<T>` | 进度报告接口 |
| `Progress<T>(action)` | 创建进度报告对象 |
| `IProgress<T>.Report(value)` | 报告进度 |

---

## Thread 线程

### 创建和启动线程

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Thread(action)` | 创建线程 |
| `new Thread(ParameterizedThreadStart)` | 创建带参数的线程 |
| `Thread.Start()` | 启动线程 |
| `Thread.Start(object)` | 启动带参数的线程 |

### 线程状态管理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Thread.Join()` | 等待线程完成 |
| `Thread.Join(TimeSpan)` | 等待线程完成（超时） |
| `Thread.ThreadState` | 获取线程状态 |
| `Thread.IsAlive` | 检查线程是否存活 |

### 线程优先级

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Thread.Priority = ThreadPriority.Highest` | 设置最高优先级 |
| `Thread.Priority = ThreadPriority.AboveNormal` | 设置高于正常优先级 |
| `Thread.Priority = ThreadPriority.Normal` | 设置正常优先级 |
| `Thread.Priority = ThreadPriority.BelowNormal` | 设置低于正常优先级 |
| `Thread.Priority = ThreadPriority.Lowest` | 设置最低优先级 |

### 前台和后台线程

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Thread.IsBackground = true` | 设置为后台线程 |
| `Thread.IsBackground = false` | 设置为前台线程 |

### 线程本地存储

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ThreadLocal<T>(factory)` | 创建线程本地存储 |
| `ThreadLocal<T>.Value` | 获取或设置线程本地值 |
| `ThreadLocal<T>.Dispose()` | 释放线程本地存储 |

### 线程池

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ThreadPool.QueueUserWorkItem(action)` | 在线程池中执行工作项 |
| `ThreadPool.QueueUserWorkItem(action, state)` | 在线程池中执行带状态的工作项 |
| `ThreadPool.GetMaxThreads(out, out)` | 获取最大线程数 |
| `ThreadPool.GetMinThreads(out, out)` | 获取最小线程数 |

### 线程同步

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `lock (object)` | 锁定语句 |
| `Monitor.Enter(object)` | 进入监视器 |
| `Monitor.Exit(object)` | 退出监视器 |
| `Monitor.Pulse(object)` | 通知等待线程 |
| `Monitor.Wait(object)` | 等待通知 |

---

## 并发集合

### ConcurrentQueue<T> 并发队列

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentQueue<T>.Enqueue(item)` | 入队 |
| `ConcurrentQueue<T>.TryDequeue(out item)` | 尝试出队 |
| `ConcurrentQueue<T>.TryPeek(out item)` | 尝试查看队首元素 |
| `ConcurrentQueue<T>.Count` | 获取队列长度 |

### ConcurrentStack<T> 并发栈

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentStack<T>.Push(item)` | 压栈 |
| `ConcurrentStack<T>.TryPop(out item)` | 尝试出栈 |
| `ConcurrentStack<T>.TryPeek(out item)` | 尝试查看栈顶元素 |
| `ConcurrentStack<T>.Count` | 获取栈长度 |

### ConcurrentDictionary<TKey, TValue> 并发字典

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentDictionary<TKey, TValue>.TryAdd(key, value)` | 尝试添加键值对 |
| `ConcurrentDictionary<TKey, TValue>.TryRemove(key, out value)` | 尝试移除键值对 |
| `ConcurrentDictionary<TKey, TValue>.TryGetValue(key, out value)` | 尝试获取值 |
| `ConcurrentDictionary<TKey, TValue>.AddOrUpdate(key, addValue, updateFactory)` | 添加或更新 |
| `ConcurrentDictionary<TKey, TValue>.GetOrAdd(key, valueFactory)` | 获取或添加 |

### ConcurrentBag<T> 并发包

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentBag<T>.Add(item)` | 添加元素 |
| `ConcurrentBag<T>.TryTake(out item)` | 尝试取出元素 |
| `ConcurrentBag<T>.TryPeek(out item)` | 尝试查看元素 |
| `ConcurrentBag<T>.Count` | 获取包长度 |

### BlockingCollection<T> 阻塞集合

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `BlockingCollection<T>.Add(item)` | 添加元素 |
| `BlockingCollection<T>.Take()` | 取出元素（阻塞） |
| `BlockingCollection<T>.TryTake(out item, TimeSpan)` | 尝试取出元素（超时） |
| `BlockingCollection<T>.CompleteAdding()` | 完成添加 |
| `BlockingCollection<T>.GetConsumingEnumerable()` | 获取消费枚举器 |

---

## 同步原语

### lock 关键字

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `lock (object) { }` | 锁定语句块 |

### Monitor 监视器

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Monitor.Enter(object)` | 进入监视器 |
| `Monitor.Exit(object)` | 退出监视器 |
| `Monitor.TryEnter(object)` | 尝试进入监视器 |
| `Monitor.TryEnter(object, TimeSpan)` | 尝试进入监视器（超时） |
| `Monitor.Pulse(object)` | 通知等待线程 |
| `Monitor.PulseAll(object)` | 通知所有等待线程 |
| `Monitor.Wait(object)` | 等待通知 |

### Mutex 互斥体

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Mutex()` | 创建互斥体 |
| `Mutex.WaitOne()` | 等待获取互斥体 |
| `Mutex.WaitOne(TimeSpan)` | 等待获取互斥体（超时） |
| `Mutex.ReleaseMutex()` | 释放互斥体 |
| `Mutex.Dispose()` | 释放资源 |

### SemaphoreSlim 信号量

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new SemaphoreSlim(initialCount, maxCount)` | 创建信号量 |
| `SemaphoreSlim.Wait()` | 等待信号量 |
| `SemaphoreSlim.WaitAsync()` | 异步等待信号量 |
| `SemaphoreSlim.WaitAsync(TimeSpan)` | 异步等待信号量（超时） |
| `SemaphoreSlim.Release()` | 释放信号量 |
| `SemaphoreSlim.Release(count)` | 释放指定数量的信号量 |
| `SemaphoreSlim.Dispose()` | 释放资源 |

### AutoResetEvent 自动重置事件

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new AutoResetEvent(initialState)` | 创建自动重置事件 |
| `AutoResetEvent.WaitOne()` | 等待事件 |
| `AutoResetEvent.WaitOne(TimeSpan)` | 等待事件（超时） |
| `AutoResetEvent.Set()` | 设置事件 |
| `AutoResetEvent.Reset()` | 重置事件 |
| `AutoResetEvent.Dispose()` | 释放资源 |

### ManualResetEventSlim 手动重置事件

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new ManualResetEventSlim(initialState)` | 创建手动重置事件 |
| `ManualResetEventSlim.Wait()` | 等待事件 |
| `ManualResetEventSlim.Wait(TimeSpan)` | 等待事件（超时） |
| `ManualResetEventSlim.WaitAsync()` | 异步等待事件 |
| `ManualResetEventSlim.Set()` | 设置事件 |
| `ManualResetEventSlim.Reset()` | 重置事件 |
| `ManualResetEventSlim.Dispose()` | 释放资源 |

---

## 取消令牌

### 基本取消操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new CancellationTokenSource()` | 创建取消令牌源 |
| `CancellationTokenSource.Token` | 获取取消令牌 |
| `CancellationTokenSource.Cancel()` | 取消操作 |
| `CancellationToken.ThrowIfCancellationRequested()` | 检查取消并抛出异常 |
| `CancellationToken.IsCancellationRequested` | 检查是否已取消 |

### 超时取消

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new CancellationTokenSource(TimeSpan)` | 创建超时取消令牌源 |
| `new CancellationTokenSource(milliseconds)` | 创建超时取消令牌源 |

### 链接取消令牌

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `CancellationTokenSource.CreateLinkedTokenSource(tokens)` | 创建链接取消令牌源 |

### 注册取消回调

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `CancellationToken.Register(action)` | 注册取消回调 |
| `CancellationToken.Register(action, useSynchronizationContext)` | 注册取消回调（指定同步上下文） |

---

## 并行编程

### Parallel.For 数据并行

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Parallel.For(fromInclusive, toExclusive, action)` | 并行循环 |
| `Parallel.For(fromInclusive, toExclusive, parallelOptions, action)` | 并行循环（带选项） |
| `Parallel.For(fromInclusive, toExclusive, localInit, body, localFinally)` | 并行循环（带本地状态） |

### Parallel.ForEach 集合并行

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Parallel.ForEach(source, action)` | 并行遍历集合 |
| `Parallel.ForEach(source, parallelOptions, action)` | 并行遍历集合（带选项） |
| `Parallel.ForEach(source, localInit, body, localFinally)` | 并行遍历集合（带本地状态） |

### Parallel.Invoke 任务并行

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Parallel.Invoke(actions)` | 并行执行多个操作 |
| `Parallel.Invoke(parallelOptions, actions)` | 并行执行多个操作（带选项） |

### 并行选项

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new ParallelOptions()` | 创建并行选项 |
| `ParallelOptions.MaxDegreeOfParallelism` | 设置最大并行度 |
| `ParallelOptions.CancellationToken` | 设置取消令牌 |
| `ParallelOptions.TaskScheduler` | 设置任务调度器 |

### 并行LINQ (PLINQ)

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.AsParallel()` | 启用并行查询 |
| `IEnumerable<T>.AsParallel().WithDegreeOfParallelism(n)` | 设置并行度 |
| `IEnumerable<T>.AsParallel().WithCancellation(token)` | 设置取消令牌 |
| `IEnumerable<T>.AsParallel().WithExecutionMode(mode)` | 设置执行模式 |

---

## 高级线程操作

### 定时器

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Timer(callback, state, dueTime, period)` | 创建定时器 |
| `Timer.Change(dueTime, period)` | 修改定时器 |
| `Timer.Dispose()` | 释放定时器 |

### 异步流

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `async IAsyncEnumerable<T> Method()` | 声明异步流方法 |
| `await foreach (var item in stream)` | 异步遍历流 |
| `yield return item` | 产生流项 |

### 线程池配置

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ThreadPool.GetMinThreads(out, out)` | 获取最小线程数 |
| `ThreadPool.GetMaxThreads(out, out)` | 获取最大线程数 |
| `ThreadPool.SetMinThreads(workerThreads, completionPortThreads)` | 设置最小线程数 |
| `ThreadPool.SetMaxThreads(workerThreads, completionPortThreads)` | 设置最大线程数 |

### 线程本地存储

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ThreadLocal<T>(factory)` | 创建线程本地存储 |
| `ThreadLocal<T>.Value` | 获取或设置线程本地值 |
| `ThreadLocal<T>.Values` | 获取所有线程的值 |
| `ThreadLocal<T>.Dispose()` | 释放线程本地存储 |

### 内存屏障

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Barrier(participantCount)` | 创建屏障 |
| `new Barrier(participantCount, action)` | 创建带后置操作的屏障 |
| `Barrier.SignalAndWait()` | 到达屏障并等待 |
| `Barrier.SignalAndWait(TimeSpan)` | 到达屏障并等待（超时） |
| `Barrier.AddParticipant()` | 添加参与者 |
| `Barrier.RemoveParticipant()` | 移除参与者 |
| `Barrier.Dispose()` | 释放屏障 |

---

## 使用注意事项

1. **避免死锁**：注意锁的获取顺序，避免循环等待
2. **资源管理**：及时释放线程、定时器等资源
3. **异常处理**：正确处理异步和并行操作中的异常
4. **性能考虑**：合理使用线程池，避免创建过多线程
5. **取消机制**：为长时间运行的任务提供取消机制
6. **线程安全**：在多线程环境中使用线程安全的集合
7. **同步原语选择**：根据具体需求选择合适的同步原语
8. **异步编程**：优先使用async/await而不是直接操作线程
9. **并行度控制**：合理设置并行度，避免过度并行
10. **内存屏障**：在需要时使用内存屏障确保内存可见性

---

## 示例代码

完整的示例代码请参考 `ThreadingExample.cs` 文件，其中包含了所有API的详细使用示例和中文注释。 