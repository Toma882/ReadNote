using UnityEngine;
using UnityEditor;
using UnityEditor.Profiling;
using Unity.Profiling;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// UnityEditor.Profiling 命名空间案例演示
/// 展示性能分析系统的使用，包括性能监控、分析器、内存分析等
/// </summary>
public class ProfilingExample : MonoBehaviour
{
    [Header("性能分析系统配置")]
    [SerializeField] private bool enableProfiling = true; // 是否启用性能分析系统
    [SerializeField] private bool enableMemoryProfiling = true; // 是否启用内存分析
    [SerializeField] private bool enableCPUProfiling = true; // 是否启用CPU分析
    [SerializeField] private bool enableGPUProfiling = true; // 是否启用GPU分析
    [SerializeField] private bool enableNetworkProfiling = true; // 是否启用网络分析
    [SerializeField] private bool enableAudioProfiling = true; // 是否启用音频分析
    [SerializeField] private bool enablePhysicsProfiling = true; // 是否启用物理分析
    [SerializeField] private bool enableRenderingProfiling = true; // 是否启用渲染分析
    [SerializeField] private bool enableScriptingProfiling = true; // 是否启用脚本分析
    [SerializeField] private bool enableUIProfiling = true; // 是否启用UI分析
    
    [Header("性能监控配置")]
    [SerializeField] private bool enableRealTimeMonitoring = true; // 是否启用实时监控
    [SerializeField] private bool enableFrameRateMonitoring = true; // 是否启用帧率监控
    [SerializeField] private bool enableMemoryMonitoring = true; // 是否启用内存监控
    [SerializeField] private bool enablePerformanceMonitoring = true; // 是否启用性能监控
    [SerializeField] private bool enableProfilerSampling = true; // 是否启用性能分析器采样
    [SerializeField] private float monitoringInterval = 0.1f; // 100ms
    [SerializeField] private int maxDataPoints = 1000; // 最大数据点数
    [SerializeField] private bool enableDataExport = true; // 是否启用数据导出
    [SerializeField] private string exportPath = "ProfilingData/"; // 数据导出路径
    
    [Header("性能状态")]
    [SerializeField] private ProfilingStatus profilingStatus = ProfilingStatus.Idle; // 性能分析状态
    [SerializeField] private bool isProfilingActive = false; // 是否正在性能分析
    [SerializeField] private bool isRecording = false; // 是否正在录制
    [SerializeField] private float profilingDuration = 0f; // 性能分析持续时间
    [SerializeField] private int frameCount = 0; // 帧数
    [SerializeField] private float averageFrameTime = 0f; // 平均帧时间
    [SerializeField] private float minFrameTime = float.MaxValue; // 最小帧时间
    [SerializeField] private float maxFrameTime = 0f; // 最大帧时间
    [SerializeField] private float targetFrameRate = 60f; // 目标帧率
    [SerializeField] private float currentFrameRate = 0f; // 当前帧率
    
    [Header("内存分析")]
    [SerializeField] private MemoryInfo memoryInfo = new MemoryInfo();//内存信息
    [SerializeField] private long totalMemory = 0; // 总内存
    [SerializeField] private long usedMemory = 0; // 已使用内存
    [SerializeField] private long freeMemory = 0; // 空闲内存
    [SerializeField] private long reservedMemory = 0; // 保留内存
    [SerializeField] private long systemMemory = 0; // 系统内存
    [SerializeField] private long graphicsMemory = 0; // 图形内存
    [SerializeField] private long audioMemory = 0; // 音频内存
    [SerializeField] private long physicsMemory = 0; // 物理内存
    [SerializeField] private long renderingMemory = 0; // 渲染内存
    [SerializeField] private long scriptingMemory = 0; // 脚本内存
    [SerializeField] private long uiMemory = 0; // UI内存
    
    [Header("CPU分析")]
    [SerializeField] private CPUInfo cpuInfo = new CPUInfo();
    [SerializeField] private float cpuUsage = 0f;
    [SerializeField] private float mainThreadTime = 0f;
    [SerializeField] private float renderThreadTime = 0f;
    [SerializeField] private float physicsThreadTime = 0f;
    [SerializeField] private float audioThreadTime = 0f;
    [SerializeField] private float networkThreadTime = 0f;
    [SerializeField] private float uiThreadTime = 0f;
    [SerializeField] private float scriptingTime = 0f;
    [SerializeField] private float renderingTime = 0f;
    [SerializeField] private float physicsTime = 0f;
    [SerializeField] private float audioTime = 0f;
    
    [Header("GPU分析")]
    [SerializeField] private GPUInfo gpuInfo = new GPUInfo();
    [SerializeField] private float gpuUsage = 0f;
    [SerializeField] private float gpuMemoryUsage = 0f;
    [SerializeField] private int drawCalls = 0;
    [SerializeField] private int triangles = 0;
    [SerializeField] private int vertices = 0;
    [SerializeField] private int batches = 0;
    [SerializeField] private float fillRate = 0f;
    [SerializeField] private float pixelFillRate = 0f;
    [SerializeField] private float vertexFillRate = 0f;
    
    [Header("性能数据")]
    [SerializeField] private PerformanceData[] performanceHistory = new PerformanceData[1000];
    [SerializeField] private int dataIndex = 0;
    [SerializeField] private bool enableDataCollection = true;
    [SerializeField] private float[] frameTimeHistory = new float[1000];
    [SerializeField] private float[] memoryHistory = new float[1000];
    [SerializeField] private float[] cpuHistory = new float[1000];
    [SerializeField] private float[] gpuHistory = new float[1000];
    
    [Header("性能统计")]
    [SerializeField] private PerformanceStatistics statistics = new PerformanceStatistics();
    [SerializeField] private Dictionary<string, float> functionTimings = new Dictionary<string, float>();
    [SerializeField] private Dictionary<string, int> functionCallCounts = new Dictionary<string, int>();
    [SerializeField] private List<PerformanceIssue> performanceIssues = new List<PerformanceIssue>();
    [SerializeField] private List<PerformanceRecommendation> recommendations = new List<PerformanceRecommendation>();
    
    [Header("分析器配置")]
    [SerializeField] private bool enableProfilerWindow = true;
    [SerializeField] private bool enableDeepProfiling = false;
    [SerializeField] private bool enableCallstackSampling = false;
    [SerializeField] private bool enableMemoryProfiler = true;
    [SerializeField] private bool enableFrameDebugger = true;
    [SerializeField] private bool enableAudioProfiler = true;
    [SerializeField] private bool enablePhysicsProfiler = true;
    [SerializeField] private bool enableNetworkProfiler = true;
    
    [Header("性能阈值")]
    [SerializeField] private float frameTimeThreshold = 16.67f; // 60 FPS
    [SerializeField] private float memoryThreshold = 1024f; // 1GB
    [SerializeField] private float cpuThreshold = 80f; // 80%
    [SerializeField] private float gpuThreshold = 80f; // 80%
    [SerializeField] private int drawCallThreshold = 1000;
    [SerializeField] private int triangleThreshold = 100000;
    
    private bool isInitialized = false;
    private float lastMonitoringTime = 0f;
    private float profilingStartTime = 0f;
    private ProfilerRecorder[] profilerRecorders;
    private StringBuilder reportBuilder = new StringBuilder();

    private void Start()
    {
        InitializeProfiling();
    }

    private void InitializeProfiling()
    {
        if (!enableProfiling) return;
        
        InitializeProfilerRecorders();
        InitializePerformanceData();
        InitializeStatistics();
        SetupProfilerCallbacks();
        
        isInitialized = true;
        profilingStatus = ProfilingStatus.Idle;
        Debug.Log("性能分析系统初始化完成");
    }

    private void InitializeProfilerRecorders()
    {
        if (enableProfilerSampling)
        {
            profilerRecorders = new ProfilerRecorder[]
            {
                ProfilerRecorder.StartNew(ProfilerCategory.Render, "Total"),
                ProfilerRecorder.StartNew(ProfilerCategory.Scripts, "Total"),
                ProfilerRecorder.StartNew(ProfilerCategory.Physics, "Total"),
                ProfilerRecorder.StartNew(ProfilerCategory.Audio, "Total"),
                ProfilerRecorder.StartNew(ProfilerCategory.UI, "Total"),
                ProfilerRecorder.StartNew(ProfilerCategory.GC, "Total"),
                ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total"),
                ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Total")
            };
            
            Debug.Log("性能分析器记录器初始化完成");
        }
    }

    private void InitializePerformanceData()
    {
        performanceHistory = new PerformanceData[maxDataPoints];
        frameTimeHistory = new float[maxDataPoints];
        memoryHistory = new float[maxDataPoints];
        cpuHistory = new float[maxDataPoints];
        gpuHistory = new float[maxDataPoints];
        
        dataIndex = 0;
        frameCount = 0;
        averageFrameTime = 0f;
        minFrameTime = float.MaxValue;
        maxFrameTime = 0f;
        currentFrameRate = 0f;
        
        Debug.Log("性能数据初始化完成");
    }

    private void InitializeStatistics()
    {
        statistics = new PerformanceStatistics();
        functionTimings.Clear();
        functionCallCounts.Clear();
        performanceIssues.Clear();
        recommendations.Clear();
        
        Debug.Log("性能统计初始化完成");
    }

    private void SetupProfilerCallbacks()
    {
        // 设置性能分析器回调
        if (enableProfilerWindow)
        {
            Profiler.enabled = true;
        }
        
        Debug.Log("性能分析器回调设置完成");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        UpdateProfilingStatus();
        UpdatePerformanceData();
        UpdateMemoryInfo();
        UpdateCPUInfo();
        UpdateGPUInfo();
        
        if (enableRealTimeMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            CollectPerformanceData();
            lastMonitoringTime = Time.time;
        }
        
        if (isRecording)
        {
            UpdateRecording();
        }
        
        CheckPerformanceThresholds();
    }

    private void UpdateProfilingStatus()
    {
        if (isRecording)
        {
            profilingStatus = ProfilingStatus.Recording;
            profilingDuration = Time.time - profilingStartTime;
        }
        else if (isProfilingActive)
        {
            profilingStatus = ProfilingStatus.Active;
        }
        else
        {
            profilingStatus = ProfilingStatus.Idle;
        }
    }

    private void UpdatePerformanceData()
    {
        frameCount++;
        
        float frameTime = Time.unscaledDeltaTime * 1000f; // 转换为毫秒
        averageFrameTime = (averageFrameTime * (frameCount - 1) + frameTime) / frameCount;
        
        if (frameTime < minFrameTime)
        {
            minFrameTime = frameTime;
        }
        
        if (frameTime > maxFrameTime)
        {
            maxFrameTime = frameTime;
        }
        
        currentFrameRate = 1f / Time.unscaledDeltaTime;
        
        // 更新帧时间历史
        if (enableDataCollection)
        {
            frameTimeHistory[dataIndex] = frameTime;
        }
    }

    private void UpdateMemoryInfo()
    {
        if (enableMemoryProfiling)
        {
            memoryInfo.totalMemory = SystemInfo.systemMemorySize * 1024L * 1024L;
            memoryInfo.usedMemory = SystemInfo.systemMemorySize * 1024L * 1024L - SystemInfo.systemMemorySize * 1024L * 1024L;
            memoryInfo.freeMemory = SystemInfo.systemMemorySize * 1024L * 1024L;
            memoryInfo.reservedMemory = Profiler.GetTotalReservedMemoryLong();
            memoryInfo.systemMemory = SystemInfo.systemMemorySize * 1024L * 1024L;
            memoryInfo.graphicsMemory = SystemInfo.graphicsMemorySize * 1024L * 1024L;
            
            totalMemory = memoryInfo.totalMemory;
            usedMemory = memoryInfo.usedMemory;
            freeMemory = memoryInfo.freeMemory;
            reservedMemory = memoryInfo.reservedMemory;
            systemMemory = memoryInfo.systemMemory;
            graphicsMemory = memoryInfo.graphicsMemory;
            
            // 估算其他内存使用
            audioMemory = Profiler.GetMonoUsedSizeLong() / 4;
            physicsMemory = Profiler.GetMonoUsedSizeLong() / 8;
            renderingMemory = graphicsMemory / 2;
            scriptingMemory = Profiler.GetMonoUsedSizeLong();
            uiMemory = Profiler.GetMonoUsedSizeLong() / 6;
        }
    }

    private void UpdateCPUInfo()
    {
        if (enableCPUProfiling)
        {
            // 获取CPU使用情况
            cpuUsage = SystemInfo.processorCount > 0 ? 
                (float)SystemInfo.processorCount / 100f : 0f;
            
            // 获取各线程时间（估算）
            mainThreadTime = Time.unscaledDeltaTime * 1000f;
            renderThreadTime = mainThreadTime * 0.3f;
            physicsThreadTime = mainThreadTime * 0.2f;
            audioThreadTime = mainThreadTime * 0.1f;
            networkThreadTime = mainThreadTime * 0.05f;
            uiThreadTime = mainThreadTime * 0.1f;
            
            // 更新CPU信息
            cpuInfo.usage = cpuUsage;
            cpuInfo.mainThreadTime = mainThreadTime;
            cpuInfo.renderThreadTime = renderThreadTime;
            cpuInfo.physicsThreadTime = physicsThreadTime;
            cpuInfo.audioThreadTime = audioThreadTime;
            cpuInfo.networkThreadTime = networkThreadTime;
            cpuInfo.uiThreadTime = uiThreadTime;
            
            // 从性能分析器获取更精确的数据
            if (profilerRecorders != null && profilerRecorders.Length > 0)
            {
                scriptingTime = profilerRecorders[1].GetValue(0) / 1000000f; // 转换为毫秒
                renderingTime = profilerRecorders[0].GetValue(0) / 1000000f;
                physicsTime = profilerRecorders[2].GetValue(0) / 1000000f;
                audioTime = profilerRecorders[3].GetValue(0) / 1000000f;
                uiTime = profilerRecorders[4].GetValue(0) / 1000000f;
            }
        }
    }

    private void UpdateGPUInfo()
    {
        if (enableGPUProfiling)
        {
            // 获取GPU信息
            gpuInfo.name = SystemInfo.graphicsDeviceName;
            gpuInfo.memorySize = SystemInfo.graphicsMemorySize * 1024L * 1024L;
            gpuInfo.api = SystemInfo.graphicsDeviceType.ToString();
            gpuInfo.version = SystemInfo.graphicsDeviceVersion;
            
            // 估算GPU使用情况
            gpuUsage = (renderingTime / mainThreadTime) * 100f;
            gpuMemoryUsage = (float)graphicsMemory / gpuInfo.memorySize * 100f;
            
            // 获取渲染统计
            drawCalls = UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset != null ? 
                UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.supportsCameraOpaqueTexture ? 100 : 50 : 0;
            triangles = drawCalls * 1000; // 估算
            vertices = triangles * 3;
            batches = drawCalls;
            
            // 计算填充率
            fillRate = drawCalls / 60f; // 每帧绘制调用
            pixelFillRate = (Screen.width * Screen.height) / 1000000f; // 百万像素
            vertexFillRate = vertices / 1000f; // 千顶点
            
            gpuInfo.usage = gpuUsage;
            gpuInfo.memoryUsage = gpuMemoryUsage;
            gpuInfo.drawCalls = drawCalls;
            gpuInfo.triangles = triangles;
            gpuInfo.vertices = vertices;
            gpuInfo.batches = batches;
            gpuInfo.fillRate = fillRate;
            gpuInfo.pixelFillRate = pixelFillRate;
            gpuInfo.vertexFillRate = vertexFillRate;
        }
    }

    private void CollectPerformanceData()
    {
        if (!enableDataCollection) return;
        
        var data = new PerformanceData
        {
            timestamp = Time.time,
            frameTime = Time.unscaledDeltaTime * 1000f,
            frameRate = currentFrameRate,
            memoryUsage = usedMemory,
            cpuUsage = cpuUsage,
            gpuUsage = gpuUsage,
            drawCalls = drawCalls,
            triangles = triangles,
            vertices = vertices
        };
        
        performanceHistory[dataIndex] = data;
        memoryHistory[dataIndex] = usedMemory / (1024f * 1024f); // 转换为MB
        cpuHistory[dataIndex] = cpuUsage;
        gpuHistory[dataIndex] = gpuUsage;
        
        dataIndex = (dataIndex + 1) % maxDataPoints;
        
        // 更新统计信息
        UpdateStatistics(data);
    }

    private void UpdateStatistics(PerformanceData data)
    {
        statistics.totalFrames++;
        statistics.totalFrameTime += data.frameTime;
        statistics.totalMemoryUsage += data.memoryUsage;
        statistics.totalCPUUsage += data.cpuUsage;
        statistics.totalGPUUsage += data.gpuUsage;
        statistics.totalDrawCalls += data.drawCalls;
        statistics.totalTriangles += data.triangles;
        statistics.totalVertices += data.vertices;
        
        if (data.frameTime > statistics.maxFrameTime)
        {
            statistics.maxFrameTime = data.frameTime;
        }
        
        if (data.frameTime < statistics.minFrameTime)
        {
            statistics.minFrameTime = data.frameTime;
        }
        
        if (data.memoryUsage > statistics.maxMemoryUsage)
        {
            statistics.maxMemoryUsage = data.memoryUsage;
        }
        
        if (data.cpuUsage > statistics.maxCPUUsage)
        {
            statistics.maxCPUUsage = data.cpuUsage;
        }
        
        if (data.gpuUsage > statistics.maxGPUUsage)
        {
            statistics.maxGPUUsage = data.gpuUsage;
        }
        
        // 计算平均值
        statistics.averageFrameTime = statistics.totalFrameTime / statistics.totalFrames;
        statistics.averageMemoryUsage = statistics.totalMemoryUsage / statistics.totalFrames;
        statistics.averageCPUUsage = statistics.totalCPUUsage / statistics.totalFrames;
        statistics.averageGPUUsage = statistics.totalGPUUsage / statistics.totalFrames;
        statistics.averageDrawCalls = statistics.totalDrawCalls / statistics.totalFrames;
        statistics.averageTriangles = statistics.totalTriangles / statistics.totalFrames;
        statistics.averageVertices = statistics.totalVertices / statistics.totalFrames;
    }

    private void UpdateRecording()
    {
        if (isRecording)
        {
            // 记录性能数据
            CollectPerformanceData();
            
            // 检查是否达到记录时间限制
            if (profilingDuration > 300f) // 5分钟
            {
                StopRecording();
            }
        }
    }

    private void CheckPerformanceThresholds()
    {
        performanceIssues.Clear();
        
        // 检查帧时间
        if (averageFrameTime > frameTimeThreshold)
        {
            performanceIssues.Add(new PerformanceIssue
            {
                type = PerformanceIssueType.FrameTime,
                severity = PerformanceIssueSeverity.Warning,
                message = $"帧时间过高: {averageFrameTime:F2}ms (阈值: {frameTimeThreshold}ms)",
                recommendation = "优化渲染管线或减少绘制调用"
            });
        }
        
        // 检查内存使用
        if (usedMemory > memoryThreshold * 1024 * 1024)
        {
            performanceIssues.Add(new PerformanceIssue
            {
                type = PerformanceIssueType.Memory,
                severity = PerformanceIssueSeverity.Warning,
                message = $"内存使用过高: {usedMemory / (1024 * 1024):F1}MB (阈值: {memoryThreshold}MB)",
                recommendation = "检查内存泄漏或优化资源管理"
            });
        }
        
        // 检查CPU使用
        if (cpuUsage > cpuThreshold)
        {
            performanceIssues.Add(new PerformanceIssue
            {
                type = PerformanceIssueType.CPU,
                severity = PerformanceIssueSeverity.Warning,
                message = $"CPU使用过高: {cpuUsage:F1}% (阈值: {cpuThreshold}%)",
                recommendation = "优化脚本性能或减少计算量"
            });
        }
        
        // 检查GPU使用
        if (gpuUsage > gpuThreshold)
        {
            performanceIssues.Add(new PerformanceIssue
            {
                type = PerformanceIssueType.GPU,
                severity = PerformanceIssueSeverity.Warning,
                message = $"GPU使用过高: {gpuUsage:F1}% (阈值: {gpuThreshold}%)",
                recommendation = "优化渲染或减少图形复杂度"
            });
        }
        
        // 检查绘制调用
        if (drawCalls > drawCallThreshold)
        {
            performanceIssues.Add(new PerformanceIssue
            {
                type = PerformanceIssueType.DrawCalls,
                severity = PerformanceIssueSeverity.Warning,
                message = $"绘制调用过多: {drawCalls} (阈值: {drawCallThreshold})",
                recommendation = "合并网格或使用批处理"
            });
        }
        
        // 检查三角形数量
        if (triangles > triangleThreshold)
        {
            performanceIssues.Add(new PerformanceIssue
            {
                type = PerformanceIssueType.Triangles,
                severity = PerformanceIssueSeverity.Warning,
                message = $"三角形数量过多: {triangles} (阈值: {triangleThreshold})",
                recommendation = "简化网格或使用LOD"
            });
        }
    }

    public void StartProfiling()
    {
        if (isProfilingActive)
        {
            Debug.LogWarning("性能分析已在进行中");
            return;
        }
        
        isProfilingActive = true;
        profilingStatus = ProfilingStatus.Active;
        
        Debug.Log("性能分析已开始");
    }

    public void StopProfiling()
    {
        if (!isProfilingActive)
        {
            Debug.LogWarning("性能分析未在进行中");
            return;
        }
        
        isProfilingActive = false;
        profilingStatus = ProfilingStatus.Idle;
        
        Debug.Log("性能分析已停止");
    }

    public void StartRecording()
    {
        if (isRecording)
        {
            Debug.LogWarning("性能记录已在进行中");
            return;
        }
        
        isRecording = true;
        profilingStartTime = Time.time;
        profilingDuration = 0f;
        profilingStatus = ProfilingStatus.Recording;
        
        Debug.Log("性能记录已开始");
    }

    public void StopRecording()
    {
        if (!isRecording)
        {
            Debug.LogWarning("性能记录未在进行中");
            return;
        }
        
        isRecording = false;
        profilingStatus = ProfilingStatus.Idle;
        
        Debug.Log($"性能记录已停止，持续时间: {profilingDuration:F2}秒");
    }

    public void GeneratePerformanceReport()
    {
        reportBuilder.Clear();
        reportBuilder.AppendLine("=== 性能分析报告 ===");
        reportBuilder.AppendLine($"生成时间: {System.DateTime.Now}");
        reportBuilder.AppendLine($"分析持续时间: {profilingDuration:F2}秒");
        reportBuilder.AppendLine($"总帧数: {frameCount}");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 性能统计 ===");
        reportBuilder.AppendLine($"平均帧时间: {averageFrameTime:F2}ms");
        reportBuilder.AppendLine($"最小帧时间: {minFrameTime:F2}ms");
        reportBuilder.AppendLine($"最大帧时间: {maxFrameTime:F2}ms");
        reportBuilder.AppendLine($"当前帧率: {currentFrameRate:F1} FPS");
        reportBuilder.AppendLine($"目标帧率: {targetFrameRate} FPS");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 内存使用 ===");
        reportBuilder.AppendLine($"总内存: {FormatFileSize(totalMemory)}");
        reportBuilder.AppendLine($"已用内存: {FormatFileSize(usedMemory)}");
        reportBuilder.AppendLine($"空闲内存: {FormatFileSize(freeMemory)}");
        reportBuilder.AppendLine($"保留内存: {FormatFileSize(reservedMemory)}");
        reportBuilder.AppendLine($"系统内存: {FormatFileSize(systemMemory)}");
        reportBuilder.AppendLine($"图形内存: {FormatFileSize(graphicsMemory)}");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== CPU使用 ===");
        reportBuilder.AppendLine($"CPU使用率: {cpuUsage:F1}%");
        reportBuilder.AppendLine($"主线程时间: {mainThreadTime:F2}ms");
        reportBuilder.AppendLine($"渲染线程时间: {renderThreadTime:F2}ms");
        reportBuilder.AppendLine($"物理线程时间: {physicsThreadTime:F2}ms");
        reportBuilder.AppendLine($"音频线程时间: {audioThreadTime:F2}ms");
        reportBuilder.AppendLine($"脚本时间: {scriptingTime:F2}ms");
        reportBuilder.AppendLine($"渲染时间: {renderingTime:F2}ms");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== GPU使用 ===");
        reportBuilder.AppendLine($"GPU名称: {gpuInfo.name}");
        reportBuilder.AppendLine($"GPU内存: {FormatFileSize(gpuInfo.memorySize)}");
        reportBuilder.AppendLine($"GPU使用率: {gpuUsage:F1}%");
        reportBuilder.AppendLine($"GPU内存使用率: {gpuMemoryUsage:F1}%");
        reportBuilder.AppendLine($"绘制调用: {drawCalls}");
        reportBuilder.AppendLine($"三角形数量: {triangles}");
        reportBuilder.AppendLine($"顶点数量: {vertices}");
        reportBuilder.AppendLine($"批处理数量: {batches}");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 性能问题 ===");
        if (performanceIssues.Count > 0)
        {
            foreach (var issue in performanceIssues)
            {
                reportBuilder.AppendLine($"[{issue.severity}] {issue.message}");
                reportBuilder.AppendLine($"建议: {issue.recommendation}");
                reportBuilder.AppendLine();
            }
        }
        else
        {
            reportBuilder.AppendLine("未发现性能问题");
            reportBuilder.AppendLine();
        }
        
        reportBuilder.AppendLine("=== 性能建议 ===");
        GenerateRecommendations();
        foreach (var recommendation in recommendations)
        {
            reportBuilder.AppendLine($"[{recommendation.priority}] {recommendation.description}");
        }
        
        string report = reportBuilder.ToString();
        Debug.Log(report);
        
        if (enableDataExport)
        {
            ExportReport(report);
        }
    }

    private void GenerateRecommendations()
    {
        recommendations.Clear();
        
        if (averageFrameTime > 16.67f)
        {
            recommendations.Add(new PerformanceRecommendation
            {
                priority = RecommendationPriority.High,
                description = "帧时间超过16.67ms，建议优化渲染性能"
            });
        }
        
        if (drawCalls > 1000)
        {
            recommendations.Add(new PerformanceRecommendation
            {
                priority = RecommendationPriority.Medium,
                description = "绘制调用过多，建议使用批处理或合并网格"
            });
        }
        
        if (usedMemory > 1024 * 1024 * 1024) // 1GB
        {
            recommendations.Add(new PerformanceRecommendation
            {
                priority = RecommendationPriority.High,
                description = "内存使用过高，建议检查内存泄漏"
            });
        }
        
        if (cpuUsage > 80f)
        {
            recommendations.Add(new PerformanceRecommendation
            {
                priority = RecommendationPriority.Medium,
                description = "CPU使用率过高，建议优化脚本性能"
            });
        }
        
        if (gpuUsage > 80f)
        {
            recommendations.Add(new PerformanceRecommendation
            {
                priority = RecommendationPriority.Medium,
                description = "GPU使用率过高，建议优化渲染设置"
            });
        }
    }

    private void ExportReport(string report)
    {
        try
        {
            string fileName = $"PerformanceReport_{System.DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = System.IO.Path.Combine(exportPath, fileName);
            
            System.IO.Directory.CreateDirectory(exportPath);
            System.IO.File.WriteAllText(filePath, report);
            
            Debug.Log($"性能报告已导出: {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"导出性能报告失败: {e.Message}");
        }
    }

    public void OpenProfilerWindow()
    {
        if (enableProfilerWindow)
        {
            EditorWindow.GetWindow<UnityEditor.ProfilerWindow>();
            Debug.Log("性能分析器窗口已打开");
        }
    }

    public void OpenMemoryProfiler()
    {
        if (enableMemoryProfiler)
        {
            EditorWindow.GetWindow<UnityEditor.MemoryProfilerWindow>();
            Debug.Log("内存分析器窗口已打开");
        }
    }

    public void OpenFrameDebugger()
    {
        if (enableFrameDebugger)
        {
            EditorWindow.GetWindow<UnityEditor.FrameDebuggerWindow>();
            Debug.Log("帧调试器窗口已打开");
        }
    }

    public void ResetProfilingData()
    {
        InitializePerformanceData();
        InitializeStatistics();
        performanceIssues.Clear();
        recommendations.Clear();
        
        Debug.Log("性能分析数据已重置");
    }

    private string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes;
        int order = 0;
        
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        
        return $"{len:0.##} {sizes[order]}";
    }

    private void OnDestroy()
    {
        if (profilerRecorders != null)
        {
            foreach (var recorder in profilerRecorders)
            {
                recorder.Dispose();
            }
        }
        
        Debug.Log("性能分析器记录器已清理");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Profiling 性能分析系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("性能分析系统配置:");
        enableProfiling = GUILayout.Toggle(enableProfiling, "启用性能分析");
        enableMemoryProfiling = GUILayout.Toggle(enableMemoryProfiling, "启用内存分析");
        enableCPUProfiling = GUILayout.Toggle(enableCPUProfiling, "启用CPU分析");
        enableGPUProfiling = GUILayout.Toggle(enableGPUProfiling, "启用GPU分析");
        enableRealTimeMonitoring = GUILayout.Toggle(enableRealTimeMonitoring, "启用实时监控");
        enableDataCollection = GUILayout.Toggle(enableDataCollection, "启用数据收集");
        
        GUILayout.Space(10);
        GUILayout.Label("性能状态:");
        GUILayout.Label($"分析状态: {profilingStatus}");
        GUILayout.Label($"是否正在分析: {isProfilingActive}");
        GUILayout.Label($"是否正在记录: {isRecording}");
        GUILayout.Label($"记录持续时间: {profilingDuration:F2}秒");
        GUILayout.Label($"帧数: {frameCount}");
        GUILayout.Label($"平均帧时间: {averageFrameTime:F2}ms");
        GUILayout.Label($"当前帧率: {currentFrameRate:F1} FPS");
        GUILayout.Label($"目标帧率: {targetFrameRate} FPS");
        
        GUILayout.Space(10);
        GUILayout.Label("内存使用:");
        GUILayout.Label($"总内存: {FormatFileSize(totalMemory)}");
        GUILayout.Label($"已用内存: {FormatFileSize(usedMemory)}");
        GUILayout.Label($"空闲内存: {FormatFileSize(freeMemory)}");
        GUILayout.Label($"保留内存: {FormatFileSize(reservedMemory)}");
        GUILayout.Label($"系统内存: {FormatFileSize(systemMemory)}");
        GUILayout.Label($"图形内存: {FormatFileSize(graphicsMemory)}");
        
        GUILayout.Space(10);
        GUILayout.Label("CPU使用:");
        GUILayout.Label($"CPU使用率: {cpuUsage:F1}%");
        GUILayout.Label($"主线程时间: {mainThreadTime:F2}ms");
        GUILayout.Label($"渲染线程时间: {renderThreadTime:F2}ms");
        GUILayout.Label($"物理线程时间: {physicsThreadTime:F2}ms");
        GUILayout.Label($"脚本时间: {scriptingTime:F2}ms");
        GUILayout.Label($"渲染时间: {renderingTime:F2}ms");
        
        GUILayout.Space(10);
        GUILayout.Label("GPU使用:");
        GUILayout.Label($"GPU名称: {gpuInfo.name}");
        GUILayout.Label($"GPU使用率: {gpuUsage:F1}%");
        GUILayout.Label($"GPU内存使用率: {gpuMemoryUsage:F1}%");
        GUILayout.Label($"绘制调用: {drawCalls}");
        GUILayout.Label($"三角形数量: {triangles}");
        GUILayout.Label($"顶点数量: {vertices}");
        GUILayout.Label($"批处理数量: {batches}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("开始性能分析"))
        {
            StartProfiling();
        }
        
        if (GUILayout.Button("停止性能分析"))
        {
            StopProfiling();
        }
        
        if (GUILayout.Button("开始性能记录"))
        {
            StartRecording();
        }
        
        if (GUILayout.Button("停止性能记录"))
        {
            StopRecording();
        }
        
        if (GUILayout.Button("生成性能报告"))
        {
            GeneratePerformanceReport();
        }
        
        if (GUILayout.Button("打开性能分析器"))
        {
            OpenProfilerWindow();
        }
        
        if (GUILayout.Button("打开内存分析器"))
        {
            OpenMemoryProfiler();
        }
        
        if (GUILayout.Button("打开帧调试器"))
        {
            OpenFrameDebugger();
        }
        
        if (GUILayout.Button("重置性能数据"))
        {
            ResetProfilingData();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("性能问题:");
        foreach (var issue in performanceIssues)
        {
            string color = issue.severity == PerformanceIssueSeverity.High ? "red" : "yellow";
            GUILayout.Label($"<color={color}>[{issue.severity}] {issue.message}</color>");
        }
        
        GUILayout.EndArea();
    }
}

public enum ProfilingStatus
{
    Idle,
    Active,
    Recording,
    Completed,
    Failed
}

public enum PerformanceIssueType
{
    FrameTime,
    Memory,
    CPU,
    GPU,
    DrawCalls,
    Triangles
}

public enum PerformanceIssueSeverity
{
    Low,
    Medium,
    High,
    Critical
}

public enum RecommendationPriority
{
    Low,
    Medium,
    High,
    Critical
}

[System.Serializable]
public class MemoryInfo
{
    public long totalMemory;
    public long usedMemory;
    public long freeMemory;
    public long reservedMemory;
    public long systemMemory;
    public long graphicsMemory;
}

[System.Serializable]
public class CPUInfo
{
    public float usage;
    public float mainThreadTime;
    public float renderThreadTime;
    public float physicsThreadTime;
    public float audioThreadTime;
    public float networkThreadTime;
    public float uiThreadTime;
}

[System.Serializable]
public class GPUInfo
{
    public string name;
    public long memorySize;
    public string api;
    public string version;
    public float usage;
    public float memoryUsage;
    public int drawCalls;
    public int triangles;
    public int vertices;
    public int batches;
    public float fillRate;
    public float pixelFillRate;
    public float vertexFillRate;
}

[System.Serializable]
public class PerformanceData
{
    public float timestamp;
    public float frameTime;
    public float frameRate;
    public long memoryUsage;
    public float cpuUsage;
    public float gpuUsage;
    public int drawCalls;
    public int triangles;
    public int vertices;
}

[System.Serializable]
public class PerformanceStatistics
{
    public int totalFrames;
    public float totalFrameTime;
    public long totalMemoryUsage;
    public float totalCPUUsage;
    public float totalGPUUsage;
    public int totalDrawCalls;
    public int totalTriangles;
    public int totalVertices;
    public float averageFrameTime;
    public long averageMemoryUsage;
    public float averageCPUUsage;
    public float averageGPUUsage;
    public float averageDrawCalls;
    public float averageTriangles;
    public float averageVertices;
    public float maxFrameTime;
    public float minFrameTime = float.MaxValue;
    public long maxMemoryUsage;
    public float maxCPUUsage;
    public float maxGPUUsage;
}

[System.Serializable]
public class PerformanceIssue
{
    public PerformanceIssueType type;
    public PerformanceIssueSeverity severity;
    public string message;
    public string recommendation;
}

[System.Serializable]
public class PerformanceRecommendation
{
    public RecommendationPriority priority;
    public string description;
} 