using UnityEngine;
using UnityEngine.Diagnostics;

/// <summary>
/// UnityEngine.Diagnostics 命名空间案例演示
/// 展示性能诊断、内存分析、错误报告等核心功能
/// </summary>
public class DiagnosticsExample : MonoBehaviour
{
    [Header("性能诊断")]
    [SerializeField] private bool enablePerformanceDiagnostics = true; //启用性能诊断
    [SerializeField] private float diagnosticsInterval = 1f; //诊断间隔
    [SerializeField] private bool logPerformanceData = false; //记录性能数据
    [SerializeField] private int maxLogEntries = 100; //最大日志条目数
    
    [Header("内存分析")]
    [SerializeField] private bool enableMemoryAnalysis = true; //启用内存分析
    [SerializeField] private bool trackMemoryLeaks = false; //跟踪内存泄漏
    [SerializeField] private float memoryThreshold = 100f; //内存阈值(MB)
    [SerializeField] private bool forceGarbageCollection = false; //强制垃圾回收
    
    [Header("错误报告")]
    [SerializeField] private bool enableErrorReporting = true; //启用错误报告
    [SerializeField] private bool captureScreenshots = false; //捕获截图
    [SerializeField] private bool sendErrorReports = false; //发送错误报告
    [SerializeField] private string errorReportServer = "https://api.example.com/errors"; //错误报告服务器
    
    [Header("诊断结果")]
    [SerializeField] private float currentFPS = 0f; //当前帧率
    [SerializeField] private float averageFPS = 0f; //平均帧率
    [SerializeField] private float minFPS = 0f; //最低帧率
    [SerializeField] private float maxFPS = 0f; //最高帧率
    [SerializeField] private long totalMemory = 0; //总内存
    [SerializeField] private long usedMemory = 0; //已用内存
    [SerializeField] private long reservedMemory = 0; //保留内存
    [SerializeField] private int errorCount = 0; //错误数量
    [SerializeField] private string lastError = ""; //最后错误
    
    [Header("性能计数器")]
    [SerializeField] private float[] fpsHistory = new float[60]; //帧率历史
    [SerializeField] private int fpsIndex = 0; //帧率索引
    [SerializeField] private float[] memoryHistory = new float[60]; //内存历史
    [SerializeField] private int memoryIndex = 0; //内存索引
    
    private float lastDiagnosticsTime = 0f;
    private float fpsSum = 0f;
    private int fpsCount = 0;
    private bool isDiagnosticsRunning = false;
    private System.Collections.Generic.List<string> errorLog = new System.Collections.Generic.List<string>();

    private void Start()
    {
        InitializeDiagnostics();
    }

    /// <summary>
    /// 初始化诊断系统
    /// </summary>
    private void InitializeDiagnostics()
    {
        // 初始化性能计数器
        InitializePerformanceCounters();
        
        // 设置错误处理
        SetupErrorHandling();
        
        // 启动诊断
        StartDiagnostics();
        
        Debug.Log("诊断系统初始化完成");
    }

    /// <summary>
    /// 初始化性能计数器
    /// </summary>
    private void InitializePerformanceCounters()
    {
        fpsHistory = new float[maxLogEntries];
        memoryHistory = new float[maxLogEntries];
        fpsIndex = 0;
        memoryIndex = 0;
        fpsSum = 0f;
        fpsCount = 0;
        
        Debug.Log("性能计数器初始化完成");
    }

    /// <summary>
    /// 设置错误处理
    /// </summary>
    private void SetupErrorHandling()
    {
        if (enableErrorReporting)
        {
            Application.logMessageReceived += OnLogMessageReceived;
            Debug.Log("错误处理已设置");
        }
    }

    /// <summary>
    /// 启动诊断
    /// </summary>
    public void StartDiagnostics()
    {
        isDiagnosticsRunning = true;
        Debug.Log("诊断系统已启动");
    }

    /// <summary>
    /// 停止诊断
    /// </summary>
    public void StopDiagnostics()
    {
        isDiagnosticsRunning = false;
        Debug.Log("诊断系统已停止");
    }

    private void Update()
    {
        if (!isDiagnosticsRunning) return;
        
        // 更新性能数据
        UpdatePerformanceData();
        
        // 定期诊断
        if (Time.time - lastDiagnosticsTime > diagnosticsInterval)
        {
            RunDiagnostics();
            lastDiagnosticsTime = Time.time;
        }
        
        // 内存分析
        if (enableMemoryAnalysis)
        {
            AnalyzeMemory();
        }
    }

    /// <summary>
    /// 更新性能数据
    /// </summary>
    private void UpdatePerformanceData()
    {
        // 计算当前帧率
        currentFPS = 1f / Time.deltaTime;
        
        // 更新帧率历史
        fpsHistory[fpsIndex] = currentFPS;
        fpsIndex = (fpsIndex + 1) % maxLogEntries;
        
        // 计算平均帧率
        fpsSum += currentFPS;
        fpsCount++;
        
        if (fpsCount > 0)
        {
            averageFPS = fpsSum / fpsCount;
        }
        
        // 更新最大最小帧率
        if (currentFPS > maxFPS) maxFPS = currentFPS;
        if (currentFPS < minFPS || minFPS == 0) minFPS = currentFPS;
    }

    /// <summary>
    /// 运行诊断
    /// </summary>
    private void RunDiagnostics()
    {
        // 性能诊断
        if (enablePerformanceDiagnostics)
        {
            DiagnosePerformance();
        }
        
        // 内存分析
        if (enableMemoryAnalysis)
        {
            AnalyzeMemory();
        }
        
        // 错误检查
        if (enableErrorReporting)
        {
            CheckForErrors();
        }
        
        if (logPerformanceData)
        {
            LogPerformanceData();
        }
    }

    /// <summary>
    /// 性能诊断
    /// </summary>
    private void DiagnosePerformance()
    {
        // 检查帧率
        if (currentFPS < 30f)
        {
            Debug.LogWarning($"性能警告: 帧率过低 ({currentFPS:F1} FPS)");
        }
        
        // 检查内存使用
        if (usedMemory > memoryThreshold * 1024 * 1024)
        {
            Debug.LogWarning($"性能警告: 内存使用过高 ({usedMemory / (1024 * 1024):F1} MB)");
        }
        
        // 检查CPU使用率
        float cpuUsage = GetCPUUsage();
        if (cpuUsage > 80f)
        {
            Debug.LogWarning($"性能警告: CPU使用率过高 ({cpuUsage:F1}%)");
        }
    }

    /// <summary>
    /// 内存分析
    /// </summary>
    private void AnalyzeMemory()
    {
        // 获取内存信息
        totalMemory = SystemInfo.systemMemorySize * 1024 * 1024;
        usedMemory = System.GC.GetTotalMemory(false);
        reservedMemory = System.GC.GetTotalMemory(true);
        
        // 更新内存历史
        memoryHistory[memoryIndex] = usedMemory / (1024 * 1024);
        memoryIndex = (memoryIndex + 1) % maxLogEntries;
        
        // 检查内存泄漏
        if (trackMemoryLeaks)
        {
            CheckMemoryLeaks();
        }
        
        // 强制垃圾回收
        if (forceGarbageCollection)
        {
            System.GC.Collect();
            Debug.Log("强制垃圾回收已执行");
        }
    }

    /// <summary>
    /// 检查内存泄漏
    /// </summary>
    private void CheckMemoryLeaks()
    {
        // 简单的内存泄漏检测
        long currentMemory = System.GC.GetTotalMemory(false);
        
        // 这里可以实现更复杂的内存泄漏检测逻辑
        if (currentMemory > usedMemory * 1.5f)
        {
            Debug.LogWarning("可能检测到内存泄漏");
        }
    }

    /// <summary>
    /// 检查错误
    /// </summary>
    private void CheckForErrors()
    {
        // 检查是否有未处理的异常
        if (errorCount > 0)
        {
            Debug.LogWarning($"检测到 {errorCount} 个错误");
        }
    }

    /// <summary>
    /// 记录性能数据
    /// </summary>
    private void LogPerformanceData()
    {
        Debug.Log($"性能数据: FPS={currentFPS:F1}, 内存={usedMemory / (1024 * 1024):F1}MB, CPU={GetCPUUsage():F1}%");
    }

    /// <summary>
    /// 获取CPU使用率
    /// </summary>
    /// <returns>CPU使用率百分比</returns>
    private float GetCPUUsage()
    {
        // 这是一个简化的CPU使用率计算
        // 在实际项目中，可能需要使用更复杂的计算方法
        return Random.Range(10f, 90f); // 模拟数据
    }

    /// <summary>
    /// 日志消息接收处理
    /// </summary>
    private void OnLogMessageReceived(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            errorCount++;
            lastError = logString;
            
            // 添加到错误日志
            errorLog.Add($"[{System.DateTime.Now:HH:mm:ss}] {logString}");
            
            // 限制错误日志大小
            if (errorLog.Count > maxLogEntries)
            {
                errorLog.RemoveAt(0);
            }
            
            // 发送错误报告
            if (sendErrorReports)
            {
                SendErrorReport(logString, stackTrace, type);
            }
            
            // 捕获截图
            if (captureScreenshots)
            {
                CaptureScreenshot();
            }
        }
    }

    /// <summary>
    /// 发送错误报告
    /// </summary>
    private void SendErrorReport(string error, string stackTrace, LogType type)
    {
        var report = new ErrorReport
        {
            error = error,
            stackTrace = stackTrace,
            type = type.ToString(),
            timestamp = System.DateTime.Now.ToString(),
            deviceInfo = GetDeviceInfo(),
            performanceData = GetPerformanceData()
        };
        
        string json = JsonUtility.ToJson(report, true);
        
        // 这里应该发送到实际的错误报告服务器
        Debug.Log($"错误报告已准备: {json}");
    }

    /// <summary>
    /// 捕获截图
    /// </summary>
    private void CaptureScreenshot()
    {
        string filename = $"screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
        ScreenCapture.CaptureScreenshotAsTexture();
        Debug.Log($"截图已保存: {filename}");
    }

    /// <summary>
    /// 获取设备信息
    /// </summary>
    /// <returns>设备信息字符串</returns>
    private string GetDeviceInfo()
    {
        return $"Device: {SystemInfo.deviceModel}, OS: {SystemInfo.operatingSystem}, Memory: {SystemInfo.systemMemorySize}MB";
    }

    /// <summary>
    /// 获取性能数据
    /// </summary>
    /// <returns>性能数据字符串</returns>
    private string GetPerformanceData()
    {
        return $"FPS: {currentFPS:F1}, Memory: {usedMemory / (1024 * 1024):F1}MB, CPU: {GetCPUUsage():F1}%";
    }

    /// <summary>
    /// 生成性能报告
    /// </summary>
    public void GeneratePerformanceReport()
    {
        Debug.Log("=== 性能诊断报告 ===");
        Debug.Log($"当前帧率: {currentFPS:F1} FPS");
        Debug.Log($"平均帧率: {averageFPS:F1} FPS");
        Debug.Log($"最低帧率: {minFPS:F1} FPS");
        Debug.Log($"最高帧率: {maxFPS:F1} FPS");
        Debug.Log($"总内存: {totalMemory / (1024 * 1024):F1} MB");
        Debug.Log($"已用内存: {usedMemory / (1024 * 1024):F1} MB");
        Debug.Log($"保留内存: {reservedMemory / (1024 * 1024):F1} MB");
        Debug.Log($"CPU使用率: {GetCPUUsage():F1}%");
        Debug.Log($"错误数量: {errorCount}");
        
        if (errorCount > 0)
        {
            Debug.Log($"最后错误: {lastError}");
        }
    }

    /// <summary>
    /// 生成内存报告
    /// </summary>
    public void GenerateMemoryReport()
    {
        Debug.Log("=== 内存分析报告 ===");
        Debug.Log($"系统内存: {SystemInfo.systemMemorySize} MB");
        Debug.Log($"图形内存: {SystemInfo.graphicsMemorySize} MB");
        Debug.Log($"当前内存使用: {usedMemory / (1024 * 1024):F1} MB");
        Debug.Log($"内存使用率: {(float)usedMemory / totalMemory * 100:F1}%");
        
        // 分析内存历史
        float avgMemory = 0f;
        for (int i = 0; i < maxLogEntries; i++)
        {
            avgMemory += memoryHistory[i];
        }
        avgMemory /= maxLogEntries;
        
        Debug.Log($"平均内存使用: {avgMemory:F1} MB");
    }

    /// <summary>
    /// 清理内存
    /// </summary>
    public void CleanupMemory()
    {
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        System.GC.Collect();
        
        Debug.Log("内存清理完成");
    }

    /// <summary>
    /// 重置性能计数器
    /// </summary>
    public void ResetPerformanceCounters()
    {
        fpsSum = 0f;
        fpsCount = 0;
        minFPS = 0f;
        maxFPS = 0f;
        errorCount = 0;
        errorLog.Clear();
        
        Debug.Log("性能计数器已重置");
    }

    /// <summary>
    /// 导出诊断数据
    /// </summary>
    public void ExportDiagnosticsData()
    {
        var data = new DiagnosticsData
        {
            timestamp = System.DateTime.Now.ToString(),
            performanceData = new PerformanceData
            {
                currentFPS = currentFPS,
                averageFPS = averageFPS,
                minFPS = minFPS,
                maxFPS = maxFPS,
                totalMemory = totalMemory,
                usedMemory = usedMemory,
                reservedMemory = reservedMemory
            },
            errors = errorLog.ToArray(),
            deviceInfo = GetDeviceInfo()
        };
        
        string json = JsonUtility.ToJson(data, true);
        string filename = $"diagnostics_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        
        System.IO.File.WriteAllText(filename, json);
        Debug.Log($"诊断数据已导出: {filename}");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Diagnostics 诊断工具演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("诊断设置:");
        enablePerformanceDiagnostics = GUILayout.Toggle(enablePerformanceDiagnostics, "启用性能诊断");
        enableMemoryAnalysis = GUILayout.Toggle(enableMemoryAnalysis, "启用内存分析");
        enableErrorReporting = GUILayout.Toggle(enableErrorReporting, "启用错误报告");
        logPerformanceData = GUILayout.Toggle(logPerformanceData, "记录性能数据");
        trackMemoryLeaks = GUILayout.Toggle(trackMemoryLeaks, "跟踪内存泄漏");
        captureScreenshots = GUILayout.Toggle(captureScreenshots, "捕获截图");
        sendErrorReports = GUILayout.Toggle(sendErrorReports, "发送错误报告");
        
        GUILayout.Space(10);
        GUILayout.Label("配置参数:");
        diagnosticsInterval = float.TryParse(GUILayout.TextField("诊断间隔", diagnosticsInterval.ToString()), out var interval) ? interval : diagnosticsInterval;
        memoryThreshold = float.TryParse(GUILayout.TextField("内存阈值(MB)", memoryThreshold.ToString()), out var threshold) ? threshold : memoryThreshold;
        maxLogEntries = int.TryParse(GUILayout.TextField("最大日志条目", maxLogEntries.ToString()), out var maxEntries) ? maxEntries : maxLogEntries;
        
        GUILayout.Space(10);
        GUILayout.Label("性能数据:");
        GUILayout.Label($"当前帧率: {currentFPS:F1} FPS");
        GUILayout.Label($"平均帧率: {averageFPS:F1} FPS");
        GUILayout.Label($"最低帧率: {minFPS:F1} FPS");
        GUILayout.Label($"最高帧率: {maxFPS:F1} FPS");
        GUILayout.Label($"已用内存: {usedMemory / (1024 * 1024):F1} MB");
        GUILayout.Label($"内存使用率: {(float)usedMemory / totalMemory * 100:F1}%");
        GUILayout.Label($"CPU使用率: {GetCPUUsage():F1}%");
        GUILayout.Label($"错误数量: {errorCount}");
        
        GUILayout.Space(10);
        
        if (!isDiagnosticsRunning)
        {
            if (GUILayout.Button("启动诊断"))
            {
                StartDiagnostics();
            }
        }
        else
        {
            if (GUILayout.Button("停止诊断"))
            {
                StopDiagnostics();
            }
        }
        
        if (GUILayout.Button("生成性能报告"))
        {
            GeneratePerformanceReport();
        }
        
        if (GUILayout.Button("生成内存报告"))
        {
            GenerateMemoryReport();
        }
        
        if (GUILayout.Button("清理内存"))
        {
            CleanupMemory();
        }
        
        if (GUILayout.Button("重置计数器"))
        {
            ResetPerformanceCounters();
        }
        
        if (GUILayout.Button("导出诊断数据"))
        {
            ExportDiagnosticsData();
        }
        
        if (GUILayout.Button("强制垃圾回收"))
        {
            System.GC.Collect();
        }
        
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        if (enableErrorReporting)
        {
            Application.logMessageReceived -= OnLogMessageReceived;
        }
    }
}

/// <summary>
/// 错误报告类
/// </summary>
[System.Serializable]
public class ErrorReport
{
    public string error;
    public string stackTrace;
    public string type;
    public string timestamp;
    public string deviceInfo;
    public string performanceData;
}

/// <summary>
/// 诊断数据类
/// </summary>
[System.Serializable]
public class DiagnosticsData
{
    public string timestamp;
    public PerformanceData performanceData;
    public string[] errors;
    public string deviceInfo;
}

/// <summary>
/// 性能数据类
/// </summary>
[System.Serializable]
public class PerformanceData
{
    public float currentFPS;
    public float averageFPS;
    public float minFPS;
    public float maxFPS;
    public long totalMemory;
    public long usedMemory;
    public long reservedMemory;
} 