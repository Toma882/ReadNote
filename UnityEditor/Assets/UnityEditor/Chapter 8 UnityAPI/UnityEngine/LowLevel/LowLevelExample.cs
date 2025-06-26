using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;

/// <summary>
/// UnityEngine.LowLevel 命名空间案例演示
/// 展示底层系统访问、内存管理、性能优化等核心功能
/// </summary>
public class LowLevelExample : MonoBehaviour
{
    [Header("底层系统配置")]
    [SerializeField] private bool enableLowLevelAccess = true; //启用底层访问
    [SerializeField] private bool enableJobSystem = true; //启用作业系统
    [SerializeField] private bool enableBurstCompilation = true; //启用Burst编译
    [SerializeField] private bool enableNativeCollections = true; //启用原生集合
    [SerializeField] private bool enableMemoryProfiling = true; //启用内存分析
    
    [Header("内存管理")]
    [SerializeField] private int nativeArraySize = 1000; //原生数组大小
    [SerializeField] private int jobBatchSize = 64; //作业批处理大小
    [SerializeField] private bool useUnsafeCode = false; //使用不安全代码
    [SerializeField] private bool enableMemoryPooling = true; //启用内存池
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceProfiling = true; //启用性能分析
    [SerializeField] private float profilingInterval = 1f; //分析间隔
    [SerializeField] private bool logPerformanceData = false; //记录性能数据
    [SerializeField] private int maxProfilingSamples = 100; //最大分析样本数
    
    [Header("系统状态")]
    [SerializeField] private long totalMemory = 0; //总内存
    [SerializeField] private long usedMemory = 0; //已用内存
    [SerializeField] private long nativeMemory = 0; //原生内存
    [SerializeField] private int activeJobs = 0; //活跃作业数
    [SerializeField] private float jobCompletionRate = 0f; //作业完成率
    [SerializeField] private float burstCompilationTime = 0f; //Burst编译时间
    
    [Header("性能数据")]
    [SerializeField] private float[] performanceHistory = new float[100]; //性能历史
    [SerializeField] private int performanceIndex = 0; //性能索引
    [SerializeField] private float averagePerformance = 0f; //平均性能
    [SerializeField] private float minPerformance = 0f; //最低性能
    [SerializeField] private float maxPerformance = 0f; //最高性能
    
    private NativeArray<float> nativeArray;
    private NativeList<int> nativeList;
    private JobHandle[] jobHandles;
    private float lastProfilingTime = 0f;
    private bool isInitialized = false;
    private System.Collections.Generic.List<JobHandle> activeJobHandles = new System.Collections.Generic.List<JobHandle>();

    private void Start()
    {
        InitializeLowLevelSystem();
    }

    /// <summary>
    /// 初始化底层系统
    /// </summary>
    private void InitializeLowLevelSystem()
    {
        // 初始化原生集合
        InitializeNativeCollections();
        
        // 初始化作业系统
        InitializeJobSystem();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置底层系统
        ConfigureLowLevelSystem();
        
        isInitialized = true;
        Debug.Log("底层系统初始化完成");
    }

    /// <summary>
    /// 初始化原生集合
    /// </summary>
    private void InitializeNativeCollections()
    {
        if (enableNativeCollections)
        {
            // 创建原生数组
            nativeArray = new NativeArray<float>(nativeArraySize, Allocator.Persistent);
            
            // 创建原生列表
            nativeList = new NativeList<int>(Allocator.Persistent);
            
            // 初始化数据
            for (int i = 0; i < nativeArraySize; i++)
            {
                nativeArray[i] = Random.Range(0f, 100f);
            }
            
            Debug.Log($"原生集合初始化完成: 数组大小={nativeArraySize}");
        }
    }

    /// <summary>
    /// 初始化作业系统
    /// </summary>
    private void InitializeJobSystem()
    {
        if (enableJobSystem)
        {
            jobHandles = new JobHandle[10]; // 最多10个并发作业
            Debug.Log("作业系统初始化完成");
        }
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceProfiling)
        {
            performanceHistory = new float[maxProfilingSamples];
            performanceIndex = 0;
            averagePerformance = 0f;
            minPerformance = 0f;
            maxPerformance = 0f;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    /// <summary>
    /// 配置底层系统
    /// </summary>
    private void ConfigureLowLevelSystem()
    {
        // 配置作业系统
        if (enableJobSystem)
        {
            // 设置作业批处理大小
            JobHandle.CompleteAll(activeJobHandles.ToArray());
            activeJobHandles.Clear();
        }
        
        // 配置内存管理
        if (enableMemoryPooling)
        {
            // 这里可以配置内存池
            Debug.Log("内存池配置完成");
        }
        
        Debug.Log("底层系统配置完成");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新系统状态
        UpdateSystemStatus();
        
        // 执行作业
        ExecuteJobs();
        
        // 性能监控
        if (enablePerformanceProfiling && Time.time - lastProfilingTime > profilingInterval)
        {
            MonitorPerformance();
            lastProfilingTime = Time.time;
        }
        
        // 内存管理
        if (enableMemoryProfiling)
        {
            MonitorMemory();
        }
    }

    /// <summary>
    /// 更新系统状态
    /// </summary>
    private void UpdateSystemStatus()
    {
        // 获取内存信息
        totalMemory = SystemInfo.systemMemorySize * 1024 * 1024;
        usedMemory = System.GC.GetTotalMemory(false);
        nativeMemory = GetNativeMemoryUsage();
        
        // 获取作业状态
        activeJobs = activeJobHandles.Count;
        jobCompletionRate = CalculateJobCompletionRate();
        
        // 获取Burst编译信息
        burstCompilationTime = GetBurstCompilationTime();
    }

    /// <summary>
    /// 执行作业
    /// </summary>
    private void ExecuteJobs()
    {
        if (!enableJobSystem) return;
        
        // 清理完成的作业
        CleanupCompletedJobs();
        
        // 执行新作业
        if (activeJobHandles.Count < jobHandles.Length)
        {
            ExecuteSampleJob();
        }
    }

    /// <summary>
    /// 执行示例作业
    /// </summary>
    private void ExecuteSampleJob()
    {
        if (enableBurstCompilation)
        {
            var job = new SampleBurstJob
            {
                input = nativeArray,
                output = new NativeArray<float>(nativeArraySize, Allocator.TempJob)
            };
            
            var jobHandle = job.Schedule(nativeArraySize, jobBatchSize);
            activeJobHandles.Add(jobHandle);
            
            if (logPerformanceData)
            {
                Debug.Log($"Burst作业已调度: 大小={nativeArraySize}, 批处理={jobBatchSize}");
            }
        }
        else
        {
            var job = new SampleJob
            {
                input = nativeArray,
                output = new NativeArray<float>(nativeArraySize, Allocator.TempJob)
            };
            
            var jobHandle = job.Schedule(nativeArraySize, jobBatchSize);
            activeJobHandles.Add(jobHandle);
            
            if (logPerformanceData)
            {
                Debug.Log($"普通作业已调度: 大小={nativeArraySize}, 批处理={jobBatchSize}");
            }
        }
    }

    /// <summary>
    /// 清理完成的作业
    /// </summary>
    private void CleanupCompletedJobs()
    {
        for (int i = activeJobHandles.Count - 1; i >= 0; i--)
        {
            if (activeJobHandles[i].IsCompleted)
            {
                activeJobHandles[i].Complete();
                activeJobHandles.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 监控性能
    /// </summary>
    private void MonitorPerformance()
    {
        float currentPerformance = 1f / Time.deltaTime;
        
        // 更新性能历史
        performanceHistory[performanceIndex] = currentPerformance;
        performanceIndex = (performanceIndex + 1) % maxProfilingSamples;
        
        // 计算性能统计
        CalculatePerformanceStats();
        
        if (logPerformanceData)
        {
            Debug.Log($"性能监控: FPS={currentPerformance:F1}, 平均={averagePerformance:F1}, 作业数={activeJobs}");
        }
    }

    /// <summary>
    /// 计算性能统计
    /// </summary>
    private void CalculatePerformanceStats()
    {
        float sum = 0f;
        minPerformance = float.MaxValue;
        maxPerformance = 0f;
        
        for (int i = 0; i < maxProfilingSamples; i++)
        {
            float value = performanceHistory[i];
            sum += value;
            
            if (value < minPerformance) minPerformance = value;
            if (value > maxPerformance) maxPerformance = value;
        }
        
        averagePerformance = sum / maxProfilingSamples;
    }

    /// <summary>
    /// 监控内存
    /// </summary>
    private void MonitorMemory()
    {
        // 获取内存使用情况
        long currentMemory = System.GC.GetTotalMemory(false);
        
        if (logPerformanceData)
        {
            Debug.Log($"内存监控: 总内存={totalMemory / (1024 * 1024):F1}MB, 已用={usedMemory / (1024 * 1024):F1}MB, 原生={nativeMemory / (1024 * 1024):F1}MB");
        }
    }

    /// <summary>
    /// 获取原生内存使用量
    /// </summary>
    /// <returns>原生内存使用量（字节）</returns>
    private long GetNativeMemoryUsage()
    {
        // 这是一个简化的原生内存计算
        // 在实际项目中，可能需要使用更复杂的计算方法
        return nativeArray.IsCreated ? nativeArray.Length * sizeof(float) : 0;
    }

    /// <summary>
    /// 计算作业完成率
    /// </summary>
    /// <returns>作业完成率（0-1）</returns>
    private float CalculateJobCompletionRate()
    {
        if (activeJobHandles.Count == 0) return 1f;
        
        int completedJobs = 0;
        foreach (var jobHandle in activeJobHandles)
        {
            if (jobHandle.IsCompleted)
            {
                completedJobs++;
            }
        }
        
        return (float)completedJobs / activeJobHandles.Count;
    }

    /// <summary>
    /// 获取Burst编译时间
    /// </summary>
    /// <returns>Burst编译时间（毫秒）</returns>
    private float GetBurstCompilationTime()
    {
        // 这是一个简化的Burst编译时间计算
        // 在实际项目中，可能需要使用更复杂的计算方法
        return Random.Range(10f, 100f); // 模拟数据
    }

    /// <summary>
    /// 执行批量计算
    /// </summary>
    public void ExecuteBatchCalculation()
    {
        if (!enableJobSystem) return;
        
        Debug.Log("开始批量计算...");
        
        // 创建批量计算作业
        var batchJob = new BatchCalculationJob
        {
            data = nativeArray,
            result = new NativeArray<float>(nativeArraySize, Allocator.TempJob)
        };
        
        var jobHandle = batchJob.Schedule(nativeArraySize, jobBatchSize);
        activeJobHandles.Add(jobHandle);
        
        Debug.Log($"批量计算作业已调度: 数据大小={nativeArraySize}");
    }

    /// <summary>
    /// 执行并行处理
    /// </summary>
    public void ExecuteParallelProcessing()
    {
        if (!enableJobSystem) return;
        
        Debug.Log("开始并行处理...");
        
        // 创建并行处理作业
        var parallelJob = new ParallelProcessingJob
        {
            input = nativeArray,
            output = new NativeArray<float>(nativeArraySize, Allocator.TempJob),
            multiplier = 2.0f
        };
        
        var jobHandle = parallelJob.Schedule(nativeArraySize, jobBatchSize);
        activeJobHandles.Add(jobHandle);
        
        Debug.Log($"并行处理作业已调度: 数据大小={nativeArraySize}");
    }

    /// <summary>
    /// 执行内存优化
    /// </summary>
    public void ExecuteMemoryOptimization()
    {
        Debug.Log("开始内存优化...");
        
        // 清理未使用的内存
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        System.GC.Collect();
        
        // 清理完成的作业
        CleanupCompletedJobs();
        
        // 重新分配原生集合
        if (nativeArray.IsCreated)
        {
            nativeArray.Dispose();
            nativeArray = new NativeArray<float>(nativeArraySize, Allocator.Persistent);
        }
        
        Debug.Log("内存优化完成");
    }

    /// <summary>
    /// 生成性能报告
    /// </summary>
    public void GeneratePerformanceReport()
    {
        Debug.Log("=== 底层系统性能报告 ===");
        Debug.Log($"总内存: {totalMemory / (1024 * 1024):F1} MB");
        Debug.Log($"已用内存: {usedMemory / (1024 * 1024):F1} MB");
        Debug.Log($"原生内存: {nativeMemory / (1024 * 1024):F1} MB");
        Debug.Log($"活跃作业数: {activeJobs}");
        Debug.Log($"作业完成率: {jobCompletionRate:P1}");
        Debug.Log($"Burst编译时间: {burstCompilationTime:F1} ms");
        Debug.Log($"平均性能: {averagePerformance:F1} FPS");
        Debug.Log($"最低性能: {minPerformance:F1} FPS");
        Debug.Log($"最高性能: {maxPerformance:F1} FPS");
    }

    /// <summary>
    /// 生成系统报告
    /// </summary>
    public void GenerateSystemReport()
    {
        Debug.Log("=== 底层系统报告 ===");
        Debug.Log($"底层访问: {(enableLowLevelAccess ? "启用" : "禁用")}");
        Debug.Log($"作业系统: {(enableJobSystem ? "启用" : "禁用")}");
        Debug.Log($"Burst编译: {(enableBurstCompilation ? "启用" : "禁用")}");
        Debug.Log($"原生集合: {(enableNativeCollections ? "启用" : "禁用")}");
        Debug.Log($"内存分析: {(enableMemoryProfiling ? "启用" : "禁用")}");
        Debug.Log($"性能分析: {(enablePerformanceProfiling ? "启用" : "禁用")}");
        Debug.Log($"原生数组大小: {nativeArraySize}");
        Debug.Log($"作业批处理大小: {jobBatchSize}");
        Debug.Log($"使用不安全代码: {(useUnsafeCode ? "是" : "否")}");
        Debug.Log($"启用内存池: {(enableMemoryPooling ? "是" : "否")}");
    }

    /// <summary>
    /// 重置系统
    /// </summary>
    public void ResetSystem()
    {
        Debug.Log("重置底层系统...");
        
        // 清理作业
        CleanupCompletedJobs();
        JobHandle.CompleteAll(activeJobHandles.ToArray());
        activeJobHandles.Clear();
        
        // 清理原生集合
        if (nativeArray.IsCreated)
        {
            nativeArray.Dispose();
        }
        if (nativeList.IsCreated)
        {
            nativeList.Dispose();
        }
        
        // 重新初始化
        InitializeLowLevelSystem();
        
        Debug.Log("底层系统重置完成");
    }

    /// <summary>
    /// 导出性能数据
    /// </summary>
    public void ExportPerformanceData()
    {
        var data = new PerformanceData
        {
            timestamp = System.DateTime.Now.ToString(),
            totalMemory = totalMemory,
            usedMemory = usedMemory,
            nativeMemory = nativeMemory,
            activeJobs = activeJobs,
            jobCompletionRate = jobCompletionRate,
            burstCompilationTime = burstCompilationTime,
            averagePerformance = averagePerformance,
            minPerformance = minPerformance,
            maxPerformance = maxPerformance,
            performanceHistory = performanceHistory
        };
        
        string json = JsonUtility.ToJson(data, true);
        string filename = $"lowlevel_performance_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        
        System.IO.File.WriteAllText(filename, json);
        Debug.Log($"性能数据已导出: {filename}");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("LowLevel 底层系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("系统配置:");
        enableLowLevelAccess = GUILayout.Toggle(enableLowLevelAccess, "启用底层访问");
        enableJobSystem = GUILayout.Toggle(enableJobSystem, "启用作业系统");
        enableBurstCompilation = GUILayout.Toggle(enableBurstCompilation, "启用Burst编译");
        enableNativeCollections = GUILayout.Toggle(enableNativeCollections, "启用原生集合");
        enableMemoryProfiling = GUILayout.Toggle(enableMemoryProfiling, "启用内存分析");
        enablePerformanceProfiling = GUILayout.Toggle(enablePerformanceProfiling, "启用性能分析");
        
        GUILayout.Space(10);
        GUILayout.Label("内存管理:");
        nativeArraySize = int.TryParse(GUILayout.TextField("原生数组大小", nativeArraySize.ToString()), out var arraySize) ? arraySize : nativeArraySize;
        jobBatchSize = int.TryParse(GUILayout.TextField("作业批处理大小", jobBatchSize.ToString()), out var batchSize) ? batchSize : jobBatchSize;
        useUnsafeCode = GUILayout.Toggle(useUnsafeCode, "使用不安全代码");
        enableMemoryPooling = GUILayout.Toggle(enableMemoryPooling, "启用内存池");
        
        GUILayout.Space(10);
        GUILayout.Label("系统状态:");
        GUILayout.Label($"总内存: {totalMemory / (1024 * 1024):F1} MB");
        GUILayout.Label($"已用内存: {usedMemory / (1024 * 1024):F1} MB");
        GUILayout.Label($"原生内存: {nativeMemory / (1024 * 1024):F1} MB");
        GUILayout.Label($"活跃作业数: {activeJobs}");
        GUILayout.Label($"作业完成率: {jobCompletionRate:P1}");
        GUILayout.Label($"Burst编译时间: {burstCompilationTime:F1} ms");
        
        GUILayout.Space(10);
        GUILayout.Label("性能数据:");
        GUILayout.Label($"平均性能: {averagePerformance:F1} FPS");
        GUILayout.Label($"最低性能: {minPerformance:F1} FPS");
        GUILayout.Label($"最高性能: {maxPerformance:F1} FPS");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("执行批量计算"))
        {
            ExecuteBatchCalculation();
        }
        
        if (GUILayout.Button("执行并行处理"))
        {
            ExecuteParallelProcessing();
        }
        
        if (GUILayout.Button("执行内存优化"))
        {
            ExecuteMemoryOptimization();
        }
        
        if (GUILayout.Button("生成性能报告"))
        {
            GeneratePerformanceReport();
        }
        
        if (GUILayout.Button("生成系统报告"))
        {
            GenerateSystemReport();
        }
        
        if (GUILayout.Button("重置系统"))
        {
            ResetSystem();
        }
        
        if (GUILayout.Button("导出性能数据"))
        {
            ExportPerformanceData();
        }
        
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        // 清理资源
        if (nativeArray.IsCreated)
        {
            nativeArray.Dispose();
        }
        if (nativeList.IsCreated)
        {
            nativeList.Dispose();
        }
        
        // 清理作业
        CleanupCompletedJobs();
        JobHandle.CompleteAll(activeJobHandles.ToArray());
    }
}

/// <summary>
/// 示例Burst作业
/// </summary>
[BurstCompile]
public struct SampleBurstJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<float> input;
    [WriteOnly] public NativeArray<float> output;
    
    public void Execute(int index)
    {
        output[index] = input[index] * 2.0f;
    }
}

/// <summary>
/// 示例普通作业
/// </summary>
public struct SampleJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<float> input;
    [WriteOnly] public NativeArray<float> output;
    
    public void Execute(int index)
    {
        output[index] = input[index] * 2.0f;
    }
}

/// <summary>
/// 批量计算作业
/// </summary>
[BurstCompile]
public struct BatchCalculationJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<float> data;
    [WriteOnly] public NativeArray<float> result;
    
    public void Execute(int index)
    {
        result[index] = Mathf.Sqrt(data[index]) + Mathf.Pow(data[index], 2);
    }
}

/// <summary>
/// 并行处理作业
/// </summary>
[BurstCompile]
public struct ParallelProcessingJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<float> input;
    [WriteOnly] public NativeArray<float> output;
    public float multiplier;
    
    public void Execute(int index)
    {
        output[index] = input[index] * multiplier + Mathf.Sin(input[index]);
    }
}

/// <summary>
/// 性能数据类
/// </summary>
[System.Serializable]
public class PerformanceData
{
    public string timestamp;
    public long totalMemory;
    public long usedMemory;
    public long nativeMemory;
    public int activeJobs;
    public float jobCompletionRate;
    public float burstCompilationTime;
    public float averagePerformance;
    public float minPerformance;
    public float maxPerformance;
    public float[] performanceHistory;
} 