using UnityEngine;

/// <summary>
/// UnityEngine.CrashReportHandler 命名空间案例演示
/// 展示崩溃报告处理、错误日志、异常捕获等核心功能
/// </summary>
public class CrashReportHandlerExample : MonoBehaviour
{
    [Header("崩溃报告设置")]
    [SerializeField] private bool enableCrashReporting = true; //启用崩溃报告
    [SerializeField] private bool enableAutomaticReporting = true; //自动报告
    [SerializeField] private bool enableDetailedLogging = false; //详细日志
    [SerializeField] private string crashReportUrl = "https://api.crashlytics.com"; //崩溃报告URL

    [Header("错误处理")]
    [SerializeField] private bool enableExceptionHandling = true; //启用异常处理
    [SerializeField] private bool enableLogCapture = true; //启用日志捕获
    [SerializeField] private bool enableStackTraces = true; //启用堆栈跟踪
    [SerializeField] private int maxLogEntries = 1000; //最大日志条目数

    [Header("报告信息")]
    [SerializeField] private string appVersion = "1.0.0"; //应用版本
    [SerializeField] private string buildNumber = "1"; //构建号
    [SerializeField] private string deviceId = ""; //设备ID
    [SerializeField] private string userId = ""; //用户ID

    [Header("崩溃统计")]
    [SerializeField] private int crashCount = 0; //崩溃次数
    [SerializeField] private string lastCrashTime = ""; //最后崩溃时间
    [SerializeField] private string lastCrashType = ""; //最后崩溃类型
    [SerializeField] private bool hasUnsentReports = false; //是否有未发送报告

    private System.Collections.Generic.List<string> logBuffer = new System.Collections.Generic.List<string>();
    private System.Collections.Generic.List<CrashReport> crashReports = new System.Collections.Generic.List<CrashReport>();

    [System.Serializable]
    public class CrashReport
    {
        public string timestamp;
        public string type;
        public string message;
        public string stackTrace;
        public string deviceInfo;
        public string userInfo;
    }

    private void Start()
    {
        InitializeCrashReporting();
    }

    /// <summary>
    /// 初始化崩溃报告系统
    /// </summary>
    private void InitializeCrashReporting()
    {
        if (enableCrashReporting)
        {
            // 设置设备ID
            deviceId = SystemInfo.deviceUniqueIdentifier;
            
            // 设置异常处理
            if (enableExceptionHandling)
            {
                Application.logMessageReceived += OnLogMessageReceived;
            }
            
            // 检查未发送的报告
            CheckUnsentReports();
            
            Debug.Log("崩溃报告系统已初始化");
        }
    }

    /// <summary>
    /// 日志消息接收回调
    /// </summary>
    /// <param name="logString">日志字符串</param>
    /// <param name="stackTrace">堆栈跟踪</param>
    /// <param name="type">日志类型</param>
    private void OnLogMessageReceived(string logString, string stackTrace, LogType type)
    {
        if (enableLogCapture)
        {
            // 添加到日志缓冲区
            string logEntry = $"[{System.DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}] {logString}";
            logBuffer.Add(logEntry);
            
            // 限制日志条目数量
            if (logBuffer.Count > maxLogEntries)
            {
                logBuffer.RemoveAt(0);
            }
        }
        
        // 检查是否是错误或异常
        if (type == LogType.Error || type == LogType.Exception)
        {
            HandleCrash(logString, stackTrace, type.ToString());
        }
    }

    /// <summary>
    /// 处理崩溃
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="stackTrace">堆栈跟踪</param>
    /// <param name="type">错误类型</param>
    private void HandleCrash(string message, string stackTrace, string type)
    {
        if (!enableCrashReporting) return;
        
        // 创建崩溃报告
        CrashReport report = new CrashReport
        {
            timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            type = type,
            message = message,
            stackTrace = enableStackTraces ? stackTrace : "",
            deviceInfo = GetDeviceInfo(),
            userInfo = GetUserInfo()
        };
        
        // 添加到崩溃报告列表
        crashReports.Add(report);
        crashCount++;
        
        // 更新统计信息
        lastCrashTime = report.timestamp;
        lastCrashType = type;
        hasUnsentReports = true;
        
        Debug.Log($"崩溃已记录: {type} - {message}");
        
        // 自动发送报告
        if (enableAutomaticReporting)
        {
            SendCrashReport(report);
        }
    }

    /// <summary>
    /// 获取设备信息
    /// </summary>
    /// <returns>设备信息字符串</returns>
    private string GetDeviceInfo()
    {
        return $"Device: {SystemInfo.deviceModel}, OS: {SystemInfo.operatingSystem}, " +
               $"Memory: {SystemInfo.systemMemorySize}MB, GPU: {SystemInfo.graphicsDeviceName}";
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns>用户信息字符串</returns>
    private string GetUserInfo()
    {
        return $"UserID: {userId}, AppVersion: {appVersion}, Build: {buildNumber}";
    }

    /// <summary>
    /// 发送崩溃报告
    /// </summary>
    /// <param name="report">崩溃报告</param>
    public void SendCrashReport(CrashReport report)
    {
        if (string.IsNullOrEmpty(crashReportUrl)) return;
        
        // 这里应该实现实际的报告发送逻辑
        // 例如使用WWW或UnityWebRequest发送到服务器
        
        Debug.Log($"发送崩溃报告: {report.type} - {report.message}");
        
        // 模拟发送成功
        hasUnsentReports = false;
    }

    /// <summary>
    /// 手动触发崩溃测试
    /// </summary>
    public void TriggerTestCrash()
    {
        Debug.Log("触发测试崩溃");
        
        // 模拟不同类型的崩溃
        try
        {
            // 模拟空引用异常
            object nullObject = null;
            nullObject.ToString();
        }
        catch (System.Exception e)
        {
            HandleCrash(e.Message, e.StackTrace, "TestException");
        }
    }

    /// <summary>
    /// 检查未发送的报告
    /// </summary>
    private void CheckUnsentReports()
    {
        // 检查是否有未发送的崩溃报告
        hasUnsentReports = crashReports.Count > 0;
        
        if (hasUnsentReports)
        {
            Debug.Log($"发现 {crashReports.Count} 个未发送的崩溃报告");
        }
    }

    /// <summary>
    /// 发送所有未发送的报告
    /// </summary>
    public void SendAllUnsentReports()
    {
        if (!hasUnsentReports) return;
        
        Debug.Log($"发送 {crashReports.Count} 个崩溃报告");
        
        foreach (var report in crashReports)
        {
            SendCrashReport(report);
        }
        
        crashReports.Clear();
        hasUnsentReports = false;
    }

    /// <summary>
    /// 清除所有崩溃报告
    /// </summary>
    public void ClearAllCrashReports()
    {
        crashReports.Clear();
        logBuffer.Clear();
        crashCount = 0;
        hasUnsentReports = false;
        
        Debug.Log("所有崩溃报告已清除");
    }

    /// <summary>
    /// 获取崩溃报告信息
    /// </summary>
    public void GetCrashReportInfo()
    {
        Debug.Log("=== 崩溃报告信息 ===");
        Debug.Log($"崩溃报告启用: {enableCrashReporting}");
        Debug.Log($"自动报告: {enableAutomaticReporting}");
        Debug.Log($"详细日志: {enableDetailedLogging}");
        Debug.Log($"崩溃次数: {crashCount}");
        Debug.Log($"最后崩溃时间: {lastCrashTime}");
        Debug.Log($"最后崩溃类型: {lastCrashType}");
        Debug.Log($"未发送报告: {hasUnsentReports}");
        Debug.Log($"报告数量: {crashReports.Count}");
        Debug.Log($"日志条目数: {logBuffer.Count}");
    }

    /// <summary>
    /// 获取最近的日志
    /// </summary>
    /// <param name="count">日志数量</param>
    public void GetRecentLogs(int count = 10)
    {
        Debug.Log("=== 最近的日志 ===");
        
        int startIndex = Mathf.Max(0, logBuffer.Count - count);
        for (int i = startIndex; i < logBuffer.Count; i++)
        {
            Debug.Log(logBuffer[i]);
        }
    }

    /// <summary>
    /// 设置用户信息
    /// </summary>
    /// <param name="newUserId">用户ID</param>
    public void SetUserInfo(string newUserId)
    {
        userId = newUserId;
        Debug.Log($"用户信息已设置: {userId}");
    }

    /// <summary>
    /// 设置应用版本信息
    /// </summary>
    /// <param name="version">版本号</param>
    /// <param name="build">构建号</param>
    public void SetAppVersion(string version, string build)
    {
        appVersion = version;
        buildNumber = build;
        Debug.Log($"应用版本已设置: {appVersion} (Build {buildNumber})");
    }

    /// <summary>
    /// 测试崩溃报告功能
    /// </summary>
    public void TestCrashReporting()
    {
        Debug.Log("开始测试崩溃报告功能");
        
        // 测试不同类型的错误
        Debug.LogError("这是一个测试错误");
        Debug.LogWarning("这是一个测试警告");
        
        // 测试异常
        TriggerTestCrash();
        
        // 测试日志捕获
        GetRecentLogs(5);
        
        Debug.Log("崩溃报告功能测试完成");
    }

    /// <summary>
    /// 重置崩溃报告设置
    /// </summary>
    public void ResetCrashReportingSettings()
    {
        enableCrashReporting = true;
        enableAutomaticReporting = true;
        enableDetailedLogging = false;
        enableExceptionHandling = true;
        enableLogCapture = true;
        enableStackTraces = true;
        maxLogEntries = 1000;
        
        ClearAllCrashReports();
        Debug.Log("崩溃报告设置已重置");
    }

    private void OnDestroy()
    {
        // 清理事件监听
        if (enableExceptionHandling)
        {
            Application.logMessageReceived -= OnLogMessageReceived;
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 700));
        GUILayout.Label("CrashReportHandler 崩溃报告处理演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("崩溃统计:");
        GUILayout.Label($"崩溃次数: {crashCount}");
        GUILayout.Label($"最后崩溃: {lastCrashTime}");
        GUILayout.Label($"崩溃类型: {lastCrashType}");
        GUILayout.Label($"未发送报告: {(hasUnsentReports ? "是" : "否")}");
        
        GUILayout.Space(10);
        GUILayout.Label("报告设置:");
        
        enableCrashReporting = GUILayout.Toggle(enableCrashReporting, "启用崩溃报告");
        enableAutomaticReporting = GUILayout.Toggle(enableAutomaticReporting, "自动报告");
        enableDetailedLogging = GUILayout.Toggle(enableDetailedLogging, "详细日志");
        enableExceptionHandling = GUILayout.Toggle(enableExceptionHandling, "异常处理");
        enableLogCapture = GUILayout.Toggle(enableLogCapture, "日志捕获");
        enableStackTraces = GUILayout.Toggle(enableStackTraces, "堆栈跟踪");
        
        GUILayout.Space(5);
        maxLogEntries = int.TryParse(GUILayout.TextField("最大日志条目", maxLogEntries.ToString()), out var maxLog) ? maxLog : maxLogEntries;
        
        GUILayout.Space(10);
        GUILayout.Label("应用信息:");
        
        appVersion = GUILayout.TextField("应用版本", appVersion);
        buildNumber = GUILayout.TextField("构建号", buildNumber);
        userId = GUILayout.TextField("用户ID", userId);
        crashReportUrl = GUILayout.TextField("报告URL", crashReportUrl);
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("触发测试崩溃"))
        {
            TriggerTestCrash();
        }
        
        if (GUILayout.Button("发送所有未发送报告"))
        {
            SendAllUnsentReports();
        }
        
        if (GUILayout.Button("清除所有报告"))
        {
            ClearAllCrashReports();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取崩溃报告信息"))
        {
            GetCrashReportInfo();
        }
        
        if (GUILayout.Button("获取最近日志"))
        {
            GetRecentLogs(10);
        }
        
        if (GUILayout.Button("测试崩溃报告功能"))
        {
            TestCrashReporting();
        }
        
        if (GUILayout.Button("重置崩溃报告设置"))
        {
            ResetCrashReportingSettings();
        }
        
        GUILayout.EndArea();
    }
} 