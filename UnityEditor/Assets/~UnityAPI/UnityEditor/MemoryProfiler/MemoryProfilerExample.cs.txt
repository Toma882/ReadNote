using UnityEngine;
using UnityEditor;
using UnityEditor.MemoryProfiler;

/// <summary>
/// UnityEditor.MemoryProfiler 命名空间案例演示
/// 展示内存分析器系统的核心功能，包括内存快照、内存分析、内存泄漏检测等
/// </summary>
public class MemoryProfilerExample : MonoBehaviour
{
    [Header("内存分析器配置")]
    [SerializeField] private bool enableMemoryProfiler = true; //启用内存分析器
    [SerializeField] private bool enableMemorySnapshot = true; //启用内存快照
    [SerializeField] private bool enableMemoryAnalysis = true; //启用内存分析
    [SerializeField] private bool enableMemoryLeakDetection = true; //启用内存泄漏检测
    [SerializeField] private bool enableMemoryTracking = true; //启用内存跟踪
    [SerializeField] private bool enableMemoryReporting = true; //启用内存报告
    
    [Header("内存快照配置")]
    [SerializeField] private bool autoSnapshot = false; //自动快照
    [SerializeField] private float snapshotInterval = 30f; //快照间隔
    [SerializeField] private int maxSnapshots = 10; //最大快照数
    [SerializeField] private bool snapshotOnSceneChange = true; //场景切换时快照
    [SerializeField] private bool snapshotOnPlayModeChange = true; //播放模式切换时快照
    [SerializeField] private string snapshotName = "MemorySnapshot"; //快照名称
    [SerializeField] private int activeSnapshotIndex = 0; //活动快照索引
    [SerializeField] private bool enableSnapshotComparison = true; //启用快照比较
    
    [Header("内存分析配置")]
    [SerializeField] private bool analyzeNativeMemory = true; //分析原生内存
    [SerializeField] private bool analyzeManagedMemory = true; //分析托管内存
    [SerializeField] private bool analyzeGraphicsMemory = true; //分析图形内存
    [SerializeField] private bool analyzeAudioMemory = true; //分析音频内存
    [SerializeField] private bool analyzePhysicsMemory = true; //分析物理内存
    [SerializeField] private bool analyzeNetworkMemory = true; //分析网络内存
    [SerializeField] private float analysisThreshold = 1f; //分析阈值 (MB)
    [SerializeField] private int maxAnalysisDepth = 10; //最大分析深度
    [SerializeField] private bool enableDetailedAnalysis = true; //启用详细分析
    
    [Header("内存泄漏检测配置")]
    [SerializeField] private bool detectObjectLeaks = true; //检测对象泄漏
    [SerializeField] private bool detectTextureLeaks = true; //检测纹理泄漏
    [SerializeField] private bool detectMeshLeaks = true; //检测网格泄漏
    [SerializeField] private bool detectAudioLeaks = true; //检测音频泄漏
    [SerializeField] private bool detectMaterialLeaks = true; //检测材质泄漏
    [SerializeField] private float leakDetectionThreshold = 0.1f; //泄漏检测阈值 (MB)
    [SerializeField] private int leakDetectionInterval = 60; //泄漏检测间隔 (秒)
    [SerializeField] private bool enableLeakAlerts = true; //启用泄漏警报
    [SerializeField] private int maxLeakHistory = 50; //最大泄漏历史
    
    [Header("内存跟踪配置")]
    [SerializeField] private bool trackAllocations = true; //跟踪分配
    [SerializeField] private bool trackDeallocations = true; //跟踪释放
    [SerializeField] private bool trackGarbageCollection = true; //跟踪垃圾回收
    [SerializeField] private bool trackMemoryPressure = true; //跟踪内存压力
    [SerializeField] private float trackingUpdateInterval = 1f; //跟踪更新间隔
    [SerializeField] private bool enableRealTimeTracking = true; //启用实时跟踪
    [SerializeField] private int maxTrackingHistory = 1000; //最大跟踪历史
    [SerializeField] private bool enableTrackingVisualization = true; //启用跟踪可视化
    
    [Header("内存报告配置")]
    [SerializeField] private bool generateMemoryReport = true; //生成内存报告
    [SerializeField] private bool exportMemoryData = true; //导出内存数据
    [SerializeField] private bool enableMemoryCharts = true; //启用内存图表
    [SerializeField] private bool enableMemoryAlerts = true; //启用内存警报
    [SerializeField] private float memoryAlertThreshold = 100f; //内存警报阈值 (MB)
    [SerializeField] private string reportFormat = "JSON"; //报告格式
    [SerializeField] private string reportPath = "MemoryReports/"; //报告路径
    [SerializeField] private bool autoGenerateReport = false; //自动生成报告
    
    [Header("内存分析器状态")]
    [SerializeField] private string memoryProfilerState = "未初始化"; //内存分析器状态
    [SerializeField] private string currentAnalysisMode = "空闲"; //当前分析模式
    [SerializeField] private bool isSnapshotDirty = false; //快照是否脏
    [SerializeField] private bool isAnalysisDirty = false; //分析是否脏
    [SerializeField] private bool isTrackingDirty = false; //跟踪是否脏
    [SerializeField] private Vector2 profilerSize = Vector2.zero; //分析器大小
    [SerializeField] private Vector2 profilerPosition = Vector2.zero; //分析器位置
    
    [Header("内存数据")]
    [SerializeField] private long totalMemory = 0; //总内存
    [SerializeField] private long nativeMemory = 0; //原生内存
    [SerializeField] private long managedMemory = 0; //托管内存
    [SerializeField] private long graphicsMemory = 0; //图形内存
    [SerializeField] private long audioMemory = 0; //音频内存
    [SerializeField] private long physicsMemory = 0; //物理内存
    [SerializeField] private long networkMemory = 0; //网络内存
    [SerializeField] private float memoryUsagePercentage = 0f; //内存使用百分比
    [SerializeField] private int garbageCollectionCount = 0; //垃圾回收次数
    [SerializeField] private float lastGCTime = 0f; //上次垃圾回收时间
    
    [Header("性能监控")]
    [SerializeField] private bool enableMemoryMonitoring = true; //启用内存监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logMemoryData = false; //记录内存数据
    [SerializeField] private float profilerUpdateTime = 0f; //分析器更新时间
    [SerializeField] private int totalSnapshots = 0; //总快照数
    [SerializeField] private int totalLeaks = 0; //总泄漏数
    [SerializeField] private int totalAlerts = 0; //总警报数
    [SerializeField] private float memoryEfficiency = 0f; //内存效率
    
    [Header("内存分析器数据")]
    [SerializeField] private MemorySnapshotData[] snapshotData; //快照数据
    [SerializeField] private MemoryAnalysisData[] analysisData; //分析数据
    [SerializeField] private MemoryLeakData[] leakData; //泄漏数据
    [SerializeField] private MemoryTrackingData[] trackingData; //跟踪数据
    [SerializeField] private MemoryReportData[] reportData; //报告数据
    [SerializeField] private string[] profilerLogs; //分析器日志
    
    private MemoryProfiler memoryProfiler;
    private System.Collections.Generic.List<MemorySnapshotData> snapshotDataList;
    private System.Collections.Generic.List<MemoryAnalysisData> analysisDataList;
    private System.Collections.Generic.List<MemoryLeakData> leakDataList;
    private System.Collections.Generic.List<MemoryTrackingData> trackingDataList;
    private System.Collections.Generic.List<MemoryReportData> reportDataList;
    private System.Collections.Generic.List<string> profilerLogList;
    private float lastMonitoringTime = 0f;
    private float lastSnapshotTime = 0f;
    private float lastLeakDetectionTime = 0f;
    private float lastTrackingTime = 0f;
    private bool isInitialized = false;
    private int snapshotCounter = 0;
    private int leakCounter = 0;
    private int alertCounter = 0;

    private void Start()
    {
        InitializeMemoryProfiler();
    }

    /// <summary>
    /// 初始化内存分析器
    /// </summary>
    private void InitializeMemoryProfiler()
    {
        // 初始化数据列表
        InitializeDataLists();
        
        // 初始化内存分析器
        InitializeProfiler();
        
        // 初始化内存快照系统
        InitializeSnapshotSystem();
        
        // 初始化内存分析系统
        InitializeAnalysisSystem();
        
        // 初始化内存泄漏检测系统
        InitializeLeakDetectionSystem();
        
        // 初始化内存跟踪系统
        InitializeTrackingSystem();
        
        // 初始化内存报告系统
        InitializeReportingSystem();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置内存分析器
        ConfigureMemoryProfiler();
        
        isInitialized = true;
        memoryProfilerState = "已初始化";
        Debug.Log("内存分析器系统初始化完成");
    }

    /// <summary>
    /// 初始化数据列表
    /// </summary>
    private void InitializeDataLists()
    {
        snapshotDataList = new System.Collections.Generic.List<MemorySnapshotData>();
        analysisDataList = new System.Collections.Generic.List<MemoryAnalysisData>();
        leakDataList = new System.Collections.Generic.List<MemoryLeakData>();
        trackingDataList = new System.Collections.Generic.List<MemoryTrackingData>();
        reportDataList = new System.Collections.Generic.List<MemoryReportData>();
        profilerLogList = new System.Collections.Generic.List<string>();
        
        Debug.Log("数据列表初始化完成");
    }

    /// <summary>
    /// 初始化内存分析器
    /// </summary>
    private void InitializeProfiler()
    {
        memoryProfiler = new MemoryProfiler();
        memoryProfiler.enableProfiler = enableMemoryProfiler;
        memoryProfiler.enableSnapshot = enableMemorySnapshot;
        memoryProfiler.enableAnalysis = enableMemoryAnalysis;
        memoryProfiler.enableLeakDetection = enableMemoryLeakDetection;
        memoryProfiler.enableTracking = enableMemoryTracking;
        memoryProfiler.enableReporting = enableMemoryReporting;
        
        Debug.Log("内存分析器初始化完成");
    }

    /// <summary>
    /// 初始化内存快照系统
    /// </summary>
    private void InitializeSnapshotSystem()
    {
        if (!enableMemorySnapshot) return;
        
        // 创建初始快照
        CreateMemorySnapshot("InitialSnapshot");
        
        Debug.Log("内存快照系统初始化完成");
    }

    /// <summary>
    /// 初始化内存分析系统
    /// </summary>
    private void InitializeAnalysisSystem()
    {
        if (!enableMemoryAnalysis) return;
        
        // 执行初始内存分析
        PerformMemoryAnalysis();
        
        Debug.Log("内存分析系统初始化完成");
    }

    /// <summary>
    /// 初始化内存泄漏检测系统
    /// </summary>
    private void InitializeLeakDetectionSystem()
    {
        if (!enableMemoryLeakDetection) return;
        
        // 执行初始泄漏检测
        PerformLeakDetection();
        
        Debug.Log("内存泄漏检测系统初始化完成");
    }

    /// <summary>
    /// 初始化内存跟踪系统
    /// </summary>
    private void InitializeTrackingSystem()
    {
        if (!enableMemoryTracking) return;
        
        // 开始内存跟踪
        StartMemoryTracking();
        
        Debug.Log("内存跟踪系统初始化完成");
    }

    /// <summary>
    /// 初始化内存报告系统
    /// </summary>
    private void InitializeReportingSystem()
    {
        if (!enableMemoryReporting) return;
        
        // 创建报告目录
        CreateReportDirectory();
        
        Debug.Log("内存报告系统初始化完成");
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        lastMonitoringTime = Time.time;
        lastSnapshotTime = Time.time;
        lastLeakDetectionTime = Time.time;
        lastTrackingTime = Time.time;
        Debug.Log("性能监控初始化完成");
    }

    /// <summary>
    /// 配置内存分析器
    /// </summary>
    private void ConfigureMemoryProfiler()
    {
        // 配置快照系统
        ConfigureSnapshotSystem();
        
        // 配置分析系统
        ConfigureAnalysisSystem();
        
        // 配置泄漏检测系统
        ConfigureLeakDetectionSystem();
        
        // 配置跟踪系统
        ConfigureTrackingSystem();
        
        // 配置报告系统
        ConfigureReportingSystem();
        
        Debug.Log("内存分析器配置完成");
    }

    /// <summary>
    /// 配置快照系统
    /// </summary>
    private void ConfigureSnapshotSystem()
    {
        if (memoryProfiler != null)
        {
            memoryProfiler.autoSnapshot = autoSnapshot;
            memoryProfiler.snapshotInterval = snapshotInterval;
            memoryProfiler.maxSnapshots = maxSnapshots;
            memoryProfiler.snapshotOnSceneChange = snapshotOnSceneChange;
            memoryProfiler.snapshotOnPlayModeChange = snapshotOnPlayModeChange;
        }
    }

    /// <summary>
    /// 配置分析系统
    /// </summary>
    private void ConfigureAnalysisSystem()
    {
        if (memoryProfiler != null)
        {
            memoryProfiler.analyzeNativeMemory = analyzeNativeMemory;
            memoryProfiler.analyzeManagedMemory = analyzeManagedMemory;
            memoryProfiler.analyzeGraphicsMemory = analyzeGraphicsMemory;
            memoryProfiler.analyzeAudioMemory = analyzeAudioMemory;
            memoryProfiler.analyzePhysicsMemory = analyzePhysicsMemory;
            memoryProfiler.analyzeNetworkMemory = analyzeNetworkMemory;
            memoryProfiler.analysisThreshold = analysisThreshold;
            memoryProfiler.maxAnalysisDepth = maxAnalysisDepth;
            memoryProfiler.enableDetailedAnalysis = enableDetailedAnalysis;
        }
    }

    /// <summary>
    /// 配置泄漏检测系统
    /// </summary>
    private void ConfigureLeakDetectionSystem()
    {
        if (memoryProfiler != null)
        {
            memoryProfiler.detectObjectLeaks = detectObjectLeaks;
            memoryProfiler.detectTextureLeaks = detectTextureLeaks;
            memoryProfiler.detectMeshLeaks = detectMeshLeaks;
            memoryProfiler.detectAudioLeaks = detectAudioLeaks;
            memoryProfiler.detectMaterialLeaks = detectMaterialLeaks;
            memoryProfiler.leakDetectionThreshold = leakDetectionThreshold;
            memoryProfiler.leakDetectionInterval = leakDetectionInterval;
            memoryProfiler.enableLeakAlerts = enableLeakAlerts;
            memoryProfiler.maxLeakHistory = maxLeakHistory;
        }
    }

    /// <summary>
    /// 配置跟踪系统
    /// </summary>
    private void ConfigureTrackingSystem()
    {
        if (memoryProfiler != null)
        {
            memoryProfiler.trackAllocations = trackAllocations;
            memoryProfiler.trackDeallocations = trackDeallocations;
            memoryProfiler.trackGarbageCollection = trackGarbageCollection;
            memoryProfiler.trackMemoryPressure = trackMemoryPressure;
            memoryProfiler.trackingUpdateInterval = trackingUpdateInterval;
            memoryProfiler.enableRealTimeTracking = enableRealTimeTracking;
            memoryProfiler.maxTrackingHistory = maxTrackingHistory;
            memoryProfiler.enableTrackingVisualization = enableTrackingVisualization;
        }
    }

    /// <summary>
    /// 配置报告系统
    /// </summary>
    private void ConfigureReportingSystem()
    {
        if (memoryProfiler != null)
        {
            memoryProfiler.generateMemoryReport = generateMemoryReport;
            memoryProfiler.exportMemoryData = exportMemoryData;
            memoryProfiler.enableMemoryCharts = enableMemoryCharts;
            memoryProfiler.enableMemoryAlerts = enableMemoryAlerts;
            memoryProfiler.memoryAlertThreshold = memoryAlertThreshold;
            memoryProfiler.reportFormat = reportFormat;
            memoryProfiler.reportPath = reportPath;
            memoryProfiler.autoGenerateReport = autoGenerateReport;
        }
    }

    private void Update()
    {
        if (!isInitialized || !enableMemoryProfiler) return;
        
        // 更新性能监控
        if (enableMemoryMonitoring && Time.time - lastMonitoringTime >= monitoringInterval)
        {
            MonitorMemoryPerformance();
            lastMonitoringTime = Time.time;
        }
        
        // 更新内存数据
        UpdateMemoryData();
        
        // 处理自动快照
        HandleAutoSnapshot();
        
        // 处理自动泄漏检测
        HandleAutoLeakDetection();
        
        // 处理自动跟踪
        HandleAutoTracking();
        
        // 处理自动报告
        HandleAutoReporting();
        
        // 处理内存警报
        HandleMemoryAlerts();
    }

    /// <summary>
    /// 监控内存性能
    /// </summary>
    private void MonitorMemoryPerformance()
    {
        totalSnapshots = snapshotDataList != null ? snapshotDataList.Count : 0;
        totalLeaks = leakDataList != null ? leakDataList.Count : 0;
        totalAlerts = alertCounter;
        memoryEfficiency = CalculateMemoryEfficiency();
        
        if (logMemoryData)
        {
            Debug.Log($"内存性能数据 - 快照数: {totalSnapshots}, 泄漏数: {totalLeaks}, 警报数: {totalAlerts}, 内存效率: {memoryEfficiency:F2}%");
        }
    }

    /// <summary>
    /// 更新内存数据
    /// </summary>
    private void UpdateMemoryData()
    {
        // 获取当前内存使用情况
        totalMemory = System.GC.GetTotalMemory(false);
        nativeMemory = GetNativeMemoryUsage();
        managedMemory = GetManagedMemoryUsage();
        graphicsMemory = GetGraphicsMemoryUsage();
        audioMemory = GetAudioMemoryUsage();
        physicsMemory = GetPhysicsMemoryUsage();
        networkMemory = GetNetworkMemoryUsage();
        
        // 计算内存使用百分比
        memoryUsagePercentage = CalculateMemoryUsagePercentage();
        
        // 更新垃圾回收信息
        UpdateGarbageCollectionInfo();
    }

    /// <summary>
    /// 处理自动快照
    /// </summary>
    private void HandleAutoSnapshot()
    {
        if (!autoSnapshot || Time.time - lastSnapshotTime < snapshotInterval) return;
        
        CreateMemorySnapshot($"AutoSnapshot_{snapshotCounter++}");
        lastSnapshotTime = Time.time;
    }

    /// <summary>
    /// 处理自动泄漏检测
    /// </summary>
    private void HandleAutoLeakDetection()
    {
        if (Time.time - lastLeakDetectionTime < leakDetectionInterval) return;
        
        PerformLeakDetection();
        lastLeakDetectionTime = Time.time;
    }

    /// <summary>
    /// 处理自动跟踪
    /// </summary>
    private void HandleAutoTracking()
    {
        if (!enableRealTimeTracking || Time.time - lastTrackingTime < trackingUpdateInterval) return;
        
        UpdateMemoryTracking();
        lastTrackingTime = Time.time;
    }

    /// <summary>
    /// 处理自动报告
    /// </summary>
    private void HandleAutoReporting()
    {
        if (!autoGenerateReport) return;
        
        GenerateMemoryReport();
    }

    /// <summary>
    /// 处理内存警报
    /// </summary>
    private void HandleMemoryAlerts()
    {
        if (!enableMemoryAlerts) return;
        
        if (totalMemory > memoryAlertThreshold * 1024 * 1024) // 转换为字节
        {
            CreateMemoryAlert("内存使用过高", $"当前内存使用: {totalMemory / 1024 / 1024:F2}MB");
        }
    }

    /// <summary>
    /// 创建内存快照
    /// </summary>
    public void CreateMemorySnapshot(string snapshotName)
    {
        if (!enableMemorySnapshot) return;
        
        var snapshotData = new MemorySnapshotData
        {
            snapshotId = $"Snapshot_{snapshotCounter++}",
            snapshotName = snapshotName,
            timestamp = System.DateTime.Now.ToString(),
            totalMemory = totalMemory,
            nativeMemory = nativeMemory,
            managedMemory = managedMemory,
            graphicsMemory = graphicsMemory,
            audioMemory = audioMemory,
            physicsMemory = physicsMemory,
            networkMemory = networkMemory,
            memoryUsagePercentage = memoryUsagePercentage,
            garbageCollectionCount = garbageCollectionCount
        };
        
        snapshotDataList.Add(snapshotData);
        
        // 限制快照数量
        if (snapshotDataList.Count > maxSnapshots)
        {
            snapshotDataList.RemoveAt(0);
        }
        
        isSnapshotDirty = true;
        LogProfilerEvent("创建内存快照", snapshotName);
    }

    /// <summary>
    /// 执行内存分析
    /// </summary>
    public void PerformMemoryAnalysis()
    {
        if (!enableMemoryAnalysis) return;
        
        var analysisData = new MemoryAnalysisData
        {
            analysisId = $"Analysis_{System.DateTime.Now.Ticks}",
            timestamp = System.DateTime.Now.ToString(),
            totalMemory = totalMemory,
            nativeMemory = nativeMemory,
            managedMemory = managedMemory,
            graphicsMemory = graphicsMemory,
            audioMemory = audioMemory,
            physicsMemory = physicsMemory,
            networkMemory = networkMemory,
            memoryUsagePercentage = memoryUsagePercentage,
            analysisThreshold = analysisThreshold,
            maxAnalysisDepth = maxAnalysisDepth,
            enableDetailedAnalysis = enableDetailedAnalysis
        };
        
        analysisDataList.Add(analysisData);
        isAnalysisDirty = true;
        LogProfilerEvent("执行内存分析", $"分析ID: {analysisData.analysisId}");
    }

    /// <summary>
    /// 执行泄漏检测
    /// </summary>
    public void PerformLeakDetection()
    {
        if (!enableMemoryLeakDetection) return;
        
        var leakData = new MemoryLeakData
        {
            leakId = $"Leak_{leakCounter++}",
            timestamp = System.DateTime.Now.ToString(),
            leakType = "Potential",
            leakSize = Random.Range(0.1f, 10f), // 模拟泄漏大小
            leakThreshold = leakDetectionThreshold,
            detectObjectLeaks = detectObjectLeaks,
            detectTextureLeaks = detectTextureLeaks,
            detectMeshLeaks = detectMeshLeaks,
            detectAudioLeaks = detectAudioLeaks,
            detectMaterialLeaks = detectMaterialLeaks
        };
        
        leakDataList.Add(leakData);
        
        // 限制泄漏历史
        if (leakDataList.Count > maxLeakHistory)
        {
            leakDataList.RemoveAt(0);
        }
        
        LogProfilerEvent("执行泄漏检测", $"检测到潜在泄漏: {leakData.leakSize:F2}MB");
    }

    /// <summary>
    /// 开始内存跟踪
    /// </summary>
    public void StartMemoryTracking()
    {
        if (!enableMemoryTracking) return;
        
        LogProfilerEvent("开始内存跟踪", "内存跟踪已启动");
    }

    /// <summary>
    /// 更新内存跟踪
    /// </summary>
    public void UpdateMemoryTracking()
    {
        if (!enableMemoryTracking) return;
        
        var trackingData = new MemoryTrackingData
        {
            trackingId = $"Tracking_{System.DateTime.Now.Ticks}",
            timestamp = System.DateTime.Now.ToString(),
            totalMemory = totalMemory,
            memoryUsagePercentage = memoryUsagePercentage,
            garbageCollectionCount = garbageCollectionCount,
            trackAllocations = trackAllocations,
            trackDeallocations = trackDeallocations,
            trackGarbageCollection = trackGarbageCollection,
            trackMemoryPressure = trackMemoryPressure
        };
        
        trackingDataList.Add(trackingData);
        
        // 限制跟踪历史
        if (trackingDataList.Count > maxTrackingHistory)
        {
            trackingDataList.RemoveAt(0);
        }
    }

    /// <summary>
    /// 生成内存报告
    /// </summary>
    public void GenerateMemoryReport()
    {
        if (!generateMemoryReport) return;
        
        var reportData = new MemoryReportData
        {
            reportId = $"Report_{System.DateTime.Now.Ticks}",
            timestamp = System.DateTime.Now.ToString(),
            totalMemory = totalMemory,
            nativeMemory = nativeMemory,
            managedMemory = managedMemory,
            graphicsMemory = graphicsMemory,
            audioMemory = audioMemory,
            physicsMemory = physicsMemory,
            networkMemory = networkMemory,
            memoryUsagePercentage = memoryUsagePercentage,
            totalSnapshots = totalSnapshots,
            totalLeaks = totalLeaks,
            totalAlerts = totalAlerts,
            memoryEfficiency = memoryEfficiency,
            reportFormat = reportFormat,
            reportPath = reportPath
        };
        
        reportDataList.Add(reportData);
        LogProfilerEvent("生成内存报告", $"报告ID: {reportData.reportId}");
    }

    /// <summary>
    /// 创建内存警报
    /// </summary>
    public void CreateMemoryAlert(string alertType, string alertMessage)
    {
        alertCounter++;
        LogProfilerEvent($"内存警报: {alertType}", alertMessage);
    }

    /// <summary>
    /// 创建报告目录
    /// </summary>
    private void CreateReportDirectory()
    {
        if (!System.IO.Directory.Exists(reportPath))
        {
            System.IO.Directory.CreateDirectory(reportPath);
        }
    }

    /// <summary>
    /// 计算内存效率
    /// </summary>
    private float CalculateMemoryEfficiency()
    {
        if (totalMemory <= 0) return 0f;
        
        long usedMemory = nativeMemory + managedMemory + graphicsMemory + audioMemory + physicsMemory + networkMemory;
        return (float)usedMemory / totalMemory * 100f;
    }

    /// <summary>
    /// 计算内存使用百分比
    /// </summary>
    private float CalculateMemoryUsagePercentage()
    {
        long systemMemory = SystemInfo.systemMemorySize * 1024 * 1024; // 转换为字节
        if (systemMemory <= 0) return 0f;
        
        return (float)totalMemory / systemMemory * 100f;
    }

    /// <summary>
    /// 更新垃圾回收信息
    /// </summary>
    private void UpdateGarbageCollectionInfo()
    {
        if (trackGarbageCollection)
        {
            garbageCollectionCount = System.GC.CollectionCount(0);
            lastGCTime = Time.time;
        }
    }

    // 获取各种内存使用量的模拟方法
    private long GetNativeMemoryUsage() => Random.Range(100 * 1024 * 1024, 500 * 1024 * 1024);
    private long GetManagedMemoryUsage() => Random.Range(50 * 1024 * 1024, 200 * 1024 * 1024);
    private long GetGraphicsMemoryUsage() => Random.Range(50 * 1024 * 1024, 300 * 1024 * 1024);
    private long GetAudioMemoryUsage() => Random.Range(10 * 1024 * 1024, 50 * 1024 * 1024);
    private long GetPhysicsMemoryUsage() => Random.Range(5 * 1024 * 1024, 30 * 1024 * 1024);
    private long GetNetworkMemoryUsage() => Random.Range(1 * 1024 * 1024, 20 * 1024 * 1024);

    /// <summary>
    /// 记录分析器事件
    /// </summary>
    private void LogProfilerEvent(string eventType, string eventData)
    {
        string logMessage = $"[{System.DateTime.Now:HH:mm:ss}] {eventType}: {eventData}";
        profilerLogList.Add(logMessage);
        
        // 限制日志数量
        if (profilerLogList.Count > 100)
        {
            profilerLogList.RemoveAt(0);
        }
        
        Debug.Log(logMessage);
    }

    /// <summary>
    /// 导出内存分析器数据
    /// </summary>
    public void ExportMemoryProfilerData()
    {
        // 导出快照数据
        snapshotData = snapshotDataList.ToArray();
        
        // 导出分析数据
        analysisData = analysisDataList.ToArray();
        
        // 导出泄漏数据
        leakData = leakDataList.ToArray();
        
        // 导出跟踪数据
        trackingData = trackingDataList.ToArray();
        
        // 导出报告数据
        reportData = reportDataList.ToArray();
        
        // 导出日志数据
        profilerLogs = profilerLogList.ToArray();
        
        Debug.Log("内存分析器数据导出完成");
    }

    private void OnGUI()
    {
        if (!isInitialized) return;
        
        GUILayout.BeginArea(new Rect(10, 10, 300, Screen.height - 20));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label("内存分析器系统", EditorStyles.boldLabel);
        
        // 内存分析器配置
        GUILayout.Space(10);
        GUILayout.Label("分析器配置", EditorStyles.boldLabel);
        enableMemoryProfiler = GUILayout.Toggle(enableMemoryProfiler, "启用内存分析器");
        enableMemorySnapshot = GUILayout.Toggle(enableMemorySnapshot, "启用内存快照");
        enableMemoryAnalysis = GUILayout.Toggle(enableMemoryAnalysis, "启用内存分析");
        enableMemoryLeakDetection = GUILayout.Toggle(enableMemoryLeakDetection, "启用泄漏检测");
        enableMemoryTracking = GUILayout.Toggle(enableMemoryTracking, "启用内存跟踪");
        enableMemoryReporting = GUILayout.Toggle(enableMemoryReporting, "启用内存报告");
        
        // 内存数据
        GUILayout.Space(10);
        GUILayout.Label("内存数据", EditorStyles.boldLabel);
        GUILayout.Label($"总内存: {totalMemory / 1024 / 1024:F2}MB");
        GUILayout.Label($"原生内存: {nativeMemory / 1024 / 1024:F2}MB");
        GUILayout.Label($"托管内存: {managedMemory / 1024 / 1024:F2}MB");
        GUILayout.Label($"图形内存: {graphicsMemory / 1024 / 1024:F2}MB");
        GUILayout.Label($"音频内存: {audioMemory / 1024 / 1024:F2}MB");
        GUILayout.Label($"物理内存: {physicsMemory / 1024 / 1024:F2}MB");
        GUILayout.Label($"网络内存: {networkMemory / 1024 / 1024:F2}MB");
        GUILayout.Label($"使用百分比: {memoryUsagePercentage:F1}%");
        GUILayout.Label($"内存效率: {memoryEfficiency:F1}%");
        
        // 分析器状态
        GUILayout.Space(10);
        GUILayout.Label("分析器状态", EditorStyles.boldLabel);
        GUILayout.Label($"状态: {memoryProfilerState}");
        GUILayout.Label($"模式: {currentAnalysisMode}");
        GUILayout.Label($"快照数: {totalSnapshots}");
        GUILayout.Label($"泄漏数: {totalLeaks}");
        GUILayout.Label($"警报数: {totalAlerts}");
        GUILayout.Label($"GC次数: {garbageCollectionCount}");
        
        // 分析器操作
        GUILayout.Space(10);
        GUILayout.Label("分析器操作", EditorStyles.boldLabel);
        if (GUILayout.Button("创建快照")) CreateMemorySnapshot($"ManualSnapshot_{snapshotCounter++}");
        if (GUILayout.Button("执行分析")) PerformMemoryAnalysis();
        if (GUILayout.Button("检测泄漏")) PerformLeakDetection();
        if (GUILayout.Button("生成报告")) GenerateMemoryReport();
        if (GUILayout.Button("导出数据")) ExportMemoryProfilerData();
        if (GUILayout.Button("清空日志")) profilerLogList.Clear();
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}

/// <summary>
/// 内存分析器类
/// </summary>
public class MemoryProfiler
{
    public bool enableProfiler;
    public bool enableSnapshot;
    public bool enableAnalysis;
    public bool enableLeakDetection;
    public bool enableTracking;
    public bool enableReporting;
    
    // 快照配置
    public bool autoSnapshot;
    public float snapshotInterval;
    public int maxSnapshots;
    public bool snapshotOnSceneChange;
    public bool snapshotOnPlayModeChange;
    
    // 分析配置
    public bool analyzeNativeMemory;
    public bool analyzeManagedMemory;
    public bool analyzeGraphicsMemory;
    public bool analyzeAudioMemory;
    public bool analyzePhysicsMemory;
    public bool analyzeNetworkMemory;
    public float analysisThreshold;
    public int maxAnalysisDepth;
    public bool enableDetailedAnalysis;
    
    // 泄漏检测配置
    public bool detectObjectLeaks;
    public bool detectTextureLeaks;
    public bool detectMeshLeaks;
    public bool detectAudioLeaks;
    public bool detectMaterialLeaks;
    public float leakDetectionThreshold;
    public int leakDetectionInterval;
    public bool enableLeakAlerts;
    public int maxLeakHistory;
    
    // 跟踪配置
    public bool trackAllocations;
    public bool trackDeallocations;
    public bool trackGarbageCollection;
    public bool trackMemoryPressure;
    public float trackingUpdateInterval;
    public bool enableRealTimeTracking;
    public int maxTrackingHistory;
    public bool enableTrackingVisualization;
    
    // 报告配置
    public bool generateMemoryReport;
    public bool exportMemoryData;
    public bool enableMemoryCharts;
    public bool enableMemoryAlerts;
    public float memoryAlertThreshold;
    public string reportFormat;
    public string reportPath;
    public bool autoGenerateReport;
}

/// <summary>
/// 内存快照数据
/// </summary>
[System.Serializable]
public class MemorySnapshotData
{
    public string snapshotId;
    public string snapshotName;
    public string timestamp;
    public long totalMemory;
    public long nativeMemory;
    public long managedMemory;
    public long graphicsMemory;
    public long audioMemory;
    public long physicsMemory;
    public long networkMemory;
    public float memoryUsagePercentage;
    public int garbageCollectionCount;
}

/// <summary>
/// 内存分析数据
/// </summary>
[System.Serializable]
public class MemoryAnalysisData
{
    public string analysisId;
    public string timestamp;
    public long totalMemory;
    public long nativeMemory;
    public long managedMemory;
    public long graphicsMemory;
    public long audioMemory;
    public long physicsMemory;
    public long networkMemory;
    public float memoryUsagePercentage;
    public float analysisThreshold;
    public int maxAnalysisDepth;
    public bool enableDetailedAnalysis;
}

/// <summary>
/// 内存泄漏数据
/// </summary>
[System.Serializable]
public class MemoryLeakData
{
    public string leakId;
    public string timestamp;
    public string leakType;
    public float leakSize;
    public float leakThreshold;
    public bool detectObjectLeaks;
    public bool detectTextureLeaks;
    public bool detectMeshLeaks;
    public bool detectAudioLeaks;
    public bool detectMaterialLeaks;
}

/// <summary>
/// 内存跟踪数据
/// </summary>
[System.Serializable]
public class MemoryTrackingData
{
    public string trackingId;
    public string timestamp;
    public long totalMemory;
    public float memoryUsagePercentage;
    public int garbageCollectionCount;
    public bool trackAllocations;
    public bool trackDeallocations;
    public bool trackGarbageCollection;
    public bool trackMemoryPressure;
}

/// <summary>
/// 内存报告数据
/// </summary>
[System.Serializable]
public class MemoryReportData
{
    public string reportId;
    public string timestamp;
    public long totalMemory;
    public long nativeMemory;
    public long managedMemory;
    public long graphicsMemory;
    public long audioMemory;
    public long physicsMemory;
    public long networkMemory;
    public float memoryUsagePercentage;
    public int totalSnapshots;
    public int totalLeaks;
    public int totalAlerts;
    public float memoryEfficiency;
    public string reportFormat;
    public string reportPath;
} 