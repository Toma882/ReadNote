using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.Build 命名空间案例演示
/// 展示构建系统的使用，包括构建流程、构建报告和构建回调
/// </summary>
public class BuildExample : MonoBehaviour, IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    [Header("构建系统配置")]
    [SerializeField] private bool enableBuildSystem = true; //启用构建系统
    [SerializeField] private bool enableBuildLogging = true; //启用构建日志
    [SerializeField] private bool enableBuildValidation = true; //启用构建验证
    [SerializeField] private bool enableBuildOptimization = true; //启用构建优化
    [SerializeField] private bool enableBuildCompression = true; //启用构建压缩
    
    [Header("构建目标")]
    [SerializeField] private BuildTarget buildTarget = BuildTarget.StandaloneWindows; //构建目标
    [SerializeField] private BuildTargetGroup buildTargetGroup = BuildTargetGroup.Standalone; //构建目标组
    [SerializeField] private string buildPath = "Builds/"; //构建路径
    [SerializeField] private string buildName = "MyGame"; //构建名称
    [SerializeField] private BuildOptions buildOptions = BuildOptions.None; //构建选项
    
    [Header("构建配置")]
    [SerializeField] private bool developmentBuild = false; //开发构建
    [SerializeField] private bool debugBuild = false; //调试构建
    [SerializeField] private bool allowDebugging = false; //允许调试
    [SerializeField] private bool compressWithLz4 = true; //使用LZ4压缩
    [SerializeField] private bool compressWithLz4HC = false; //使用LZ4HC压缩
    [SerializeField] private bool buildAppBundle = false; //构建应用包
    
    [Header("构建状态")]
    [SerializeField] private string buildState = "未构建"; //构建状态
    [SerializeField] private bool isBuilding = false; //是否正在构建
    [SerializeField] private float buildProgress = 0f; //构建进度
    [SerializeField] private string buildMessage = ""; //构建消息
    [SerializeField] private BuildResult lastBuildResult = BuildResult.Unknown; //最后构建结果
    [SerializeField] private string lastBuildPath = ""; //最后构建路径
    [SerializeField] private long lastBuildSize = 0; //最后构建大小
    
    [Header("构建历史")]
    [SerializeField] private BuildHistoryEntry[] buildHistory = new BuildHistoryEntry[10]; //构建历史
    [SerializeField] private int buildHistoryIndex = 0; //构建历史索引
    [SerializeField] private bool enableBuildHistory = true; //启用构建历史
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceMonitoring = true; //启用性能监控
    [SerializeField] private float[] buildTimeHistory = new float[100]; //构建时间历史
    [SerializeField] private int buildTimeIndex = 0; //构建时间索引
    [SerializeField] private float averageBuildTime = 0f; //平均构建时间
    [SerializeField] private float maxBuildTime = 0f; //最大构建时间
    [SerializeField] private float totalBuildTime = 0f; //总构建时间
    [SerializeField] private int totalBuildCount = 0; //总构建次数
    
    [Header("构建报告")]
    [SerializeField] private BuildReport lastBuildReport; //最后构建报告
    [SerializeField] private string buildSummary = ""; //构建摘要
    [SerializeField] private int totalErrors = 0; //总错误数
    [SerializeField] private int totalWarnings = 0; //总警告数
    [SerializeField] private int totalSteps = 0; //总步骤数
    [SerializeField] private float totalDuration = 0f; //总持续时间
    
    private bool isInitialized = false;
    private float buildStartTime = 0f;
    private System.Action<float, string> buildProgressCallback;

    // IPreprocessBuildWithReport 接口实现
    public int callbackOrder => 0;

    private void Start()
    {
        InitializeBuildSystem();
    }

    /// <summary>
    /// 初始化构建系统
    /// </summary>
    private void InitializeBuildSystem()
    {
        if (!enableBuildSystem) return;
        
        // 初始化构建路径
        InitializeBuildPath();
        
        // 初始化构建选项
        InitializeBuildOptions();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 设置构建进度回调
        buildProgressCallback = OnBuildProgress;
        
        isInitialized = true;
        buildState = "已初始化";
        Debug.Log("构建系统初始化完成");
    }

    /// <summary>
    /// 初始化构建路径
    /// </summary>
    private void InitializeBuildPath()
    {
        if (string.IsNullOrEmpty(buildPath))
        {
            buildPath = "Builds/";
        }
        
        // 确保构建路径存在
        if (!System.IO.Directory.Exists(buildPath))
        {
            System.IO.Directory.CreateDirectory(buildPath);
        }
        
        Debug.Log($"构建路径已设置: {buildPath}");
    }

    /// <summary>
    /// 初始化构建选项
    /// </summary>
    private void InitializeBuildOptions()
    {
        buildOptions = BuildOptions.None;
        
        if (developmentBuild)
        {
            buildOptions |= BuildOptions.Development;
        }
        
        if (debugBuild)
        {
            buildOptions |= BuildOptions.Development;
        }
        
        if (allowDebugging)
        {
            buildOptions |= BuildOptions.AllowDebugging;
        }
        
        if (compressWithLz4)
        {
            buildOptions |= BuildOptions.CompressWithLz4;
        }
        
        if (compressWithLz4HC)
        {
            buildOptions |= BuildOptions.CompressWithLz4HC;
        }
        
        if (buildAppBundle)
        {
            buildOptions |= BuildOptions.BuildAppBundle;
        }
        
        Debug.Log($"构建选项已设置: {buildOptions}");
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            buildTimeHistory = new float[100];
            buildTimeIndex = 0;
            averageBuildTime = 0f;
            maxBuildTime = 0f;
            totalBuildTime = 0f;
            totalBuildCount = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新构建状态
        UpdateBuildStatus();
        
        // 性能监控
        if (enablePerformanceMonitoring)
        {
            UpdatePerformanceMonitoring();
        }
    }

    /// <summary>
    /// 更新构建状态
    /// </summary>
    private void UpdateBuildStatus()
    {
        if (isBuilding)
        {
            buildState = "构建中";
        }
        else
        {
            buildState = "空闲";
        }
    }

    /// <summary>
    /// 更新性能监控
    /// </summary>
    private void UpdatePerformanceMonitoring()
    {
        // 计算平均构建时间
        if (totalBuildCount > 0)
        {
            averageBuildTime = totalBuildTime / totalBuildCount;
        }
    }

    /// <summary>
    /// 构建进度回调
    /// </summary>
    /// <param name="progress">进度 (0-1)</param>
    /// <param name="message">消息</param>
    private void OnBuildProgress(float progress, string message)
    {
        buildProgress = progress;
        buildMessage = message;
        
        if (enableBuildLogging)
        {
            Debug.Log($"构建进度: {progress * 100:F1}% - {message}");
        }
    }

    /// <summary>
    /// 开始构建
    /// </summary>
    public void StartBuild()
    {
        if (isBuilding)
        {
            Debug.LogWarning("构建正在进行中，请等待完成");
            return;
        }
        
        if (!enableBuildValidation || ValidateBuild())
        {
            isBuilding = true;
            buildStartTime = Time.realtimeSinceStartup;
            buildProgress = 0f;
            buildMessage = "开始构建...";
            
            // 设置构建选项
            InitializeBuildOptions();
            
            // 开始构建
            BuildPlayer();
        }
    }

    /// <summary>
    /// 验证构建
    /// </summary>
    /// <returns>是否通过验证</returns>
    private bool ValidateBuild()
    {
        bool isValid = true;
        
        // 检查构建路径
        if (string.IsNullOrEmpty(buildPath))
        {
            Debug.LogError("构建路径不能为空");
            isValid = false;
        }
        
        // 检查构建名称
        if (string.IsNullOrEmpty(buildName))
        {
            Debug.LogError("构建名称不能为空");
            isValid = false;
        }
        
        // 检查场景
        if (EditorBuildSettings.scenes.Length == 0)
        {
            Debug.LogWarning("没有场景添加到构建设置中");
        }
        
        return isValid;
    }

    /// <summary>
    /// 构建玩家
    /// </summary>
    private void BuildPlayer()
    {
        try
        {
            // 获取构建路径
            string fullBuildPath = GetFullBuildPath();
            
            // 获取场景路径
            string[] scenePaths = GetScenePaths();
            
            // 开始构建
            BuildReport report = BuildPipeline.BuildPlayer(scenePaths, fullBuildPath, buildTarget, buildOptions);
            
            // 处理构建结果
            ProcessBuildResult(report);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"构建失败: {e.Message}");
            ProcessBuildError(e);
        }
    }

    /// <summary>
    /// 获取完整构建路径
    /// </summary>
    /// <returns>完整构建路径</returns>
    private string GetFullBuildPath()
    {
        string extension = GetBuildExtension();
        return System.IO.Path.Combine(buildPath, $"{buildName}{extension}");
    }

    /// <summary>
    /// 获取构建扩展名
    /// </summary>
    /// <returns>构建扩展名</returns>
    private string GetBuildExtension()
    {
        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return ".exe";
            case BuildTarget.StandaloneOSX:
                return ".app";
            case BuildTarget.StandaloneLinux64:
                return ".x86_64";
            case BuildTarget.Android:
                return buildAppBundle ? ".aab" : ".apk";
            case BuildTarget.iOS:
                return ""; // iOS 构建到文件夹
            default:
                return "";
        }
    }

    /// <summary>
    /// 获取场景路径
    /// </summary>
    /// <returns>场景路径数组</returns>
    private string[] GetScenePaths()
    {
        List<string> scenePaths = new List<string>();
        
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenePaths.Add(scene.path);
            }
        }
        
        if (scenePaths.Count == 0)
        {
            // 如果没有场景，添加当前场景
            scenePaths.Add(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path);
        }
        
        return scenePaths.ToArray();
    }

    /// <summary>
    /// 处理构建结果
    /// </summary>
    /// <param name="report">构建报告</param>
    private void ProcessBuildResult(BuildReport report)
    {
        lastBuildReport = report;
        lastBuildResult = report.summary.result;
        
        float buildTime = Time.realtimeSinceStartup - buildStartTime;
        
        // 更新构建信息
        if (report.summary.result == BuildResult.Succeeded)
        {
            lastBuildPath = report.summary.outputPath;
            lastBuildSize = GetBuildSize(lastBuildPath);
            buildMessage = "构建成功";
            
            // 记录到历史
            if (enableBuildHistory)
            {
                AddBuildHistoryEntry(report, buildTime);
            }
            
            Debug.Log($"构建成功: {lastBuildPath} (大小: {FormatFileSize(lastBuildSize)})");
        }
        else
        {
            buildMessage = "构建失败";
            Debug.LogError($"构建失败: {report.summary.result}");
        }
        
        // 更新性能数据
        UpdateBuildPerformance(buildTime);
        
        // 生成构建摘要
        GenerateBuildSummary(report);
        
        isBuilding = false;
        buildProgress = 1f;
    }

    /// <summary>
    /// 处理构建错误
    /// </summary>
    /// <param name="exception">异常</param>
    private void ProcessBuildError(System.Exception exception)
    {
        lastBuildResult = BuildResult.Failed;
        buildMessage = $"构建错误: {exception.Message}";
        isBuilding = false;
        buildProgress = 0f;
        
        Debug.LogError($"构建过程中发生错误: {exception}");
    }

    /// <summary>
    /// 获取构建大小
    /// </summary>
    /// <param name="buildPath">构建路径</param>
    /// <returns>构建大小（字节）</returns>
    private long GetBuildSize(string buildPath)
    {
        if (System.IO.File.Exists(buildPath))
        {
            return new System.IO.FileInfo(buildPath).Length;
        }
        else if (System.IO.Directory.Exists(buildPath))
        {
            return GetDirectorySize(buildPath);
        }
        
        return 0;
    }

    /// <summary>
    /// 获取目录大小
    /// </summary>
    /// <param name="directoryPath">目录路径</param>
    /// <returns>目录大小（字节）</returns>
    private long GetDirectorySize(string directoryPath)
    {
        long size = 0;
        
        try
        {
            string[] files = System.IO.Directory.GetFiles(directoryPath, "*", System.IO.SearchOption.AllDirectories);
            foreach (string file in files)
            {
                size += new System.IO.FileInfo(file).Length;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"计算目录大小时出错: {e.Message}");
        }
        
        return size;
    }

    /// <summary>
    /// 格式化文件大小
    /// </summary>
    /// <param name="bytes">字节数</param>
    /// <returns>格式化的文件大小字符串</returns>
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

    /// <summary>
    /// 添加构建历史条目
    /// </summary>
    /// <param name="report">构建报告</param>
    /// <param name="buildTime">构建时间</param>
    private void AddBuildHistoryEntry(BuildReport report, float buildTime)
    {
        var entry = new BuildHistoryEntry
        {
            timestamp = System.DateTime.Now.ToString(),
            buildTarget = buildTarget.ToString(),
            buildResult = report.summary.result.ToString(),
            buildPath = report.summary.outputPath,
            buildSize = GetBuildSize(report.summary.outputPath),
            buildTime = buildTime,
            totalErrors = report.summary.totalErrors,
            totalWarnings = report.summary.totalWarnings
        };
        
        buildHistory[buildHistoryIndex] = entry;
        buildHistoryIndex = (buildHistoryIndex + 1) % buildHistory.Length;
    }

    /// <summary>
    /// 更新构建性能数据
    /// </summary>
    /// <param name="buildTime">构建时间</param>
    private void UpdateBuildPerformance(float buildTime)
    {
        if (enablePerformanceMonitoring)
        {
            buildTimeHistory[buildTimeIndex] = buildTime;
            buildTimeIndex = (buildTimeIndex + 1) % 100;
            
            totalBuildTime += buildTime;
            totalBuildCount++;
            
            if (buildTime > maxBuildTime)
            {
                maxBuildTime = buildTime;
            }
        }
    }

    /// <summary>
    /// 生成构建摘要
    /// </summary>
    /// <param name="report">构建报告</param>
    private void GenerateBuildSummary(BuildReport report)
    {
        totalErrors = report.summary.totalErrors;
        totalWarnings = report.summary.totalWarnings;
        totalSteps = report.steps.Length;
        totalDuration = report.summary.totalTime.TotalSeconds;
        
        buildSummary = $"构建结果: {report.summary.result}\n" +
                      $"总时间: {totalDuration:F2}秒\n" +
                      $"总步骤: {totalSteps}\n" +
                      $"总错误: {totalErrors}\n" +
                      $"总警告: {totalWarnings}\n" +
                      $"输出路径: {report.summary.outputPath}";
    }

    /// <summary>
    /// 清理构建
    /// </summary>
    public void CleanBuild()
    {
        if (isBuilding)
        {
            Debug.LogWarning("构建正在进行中，无法清理");
            return;
        }
        
        try
        {
            if (System.IO.Directory.Exists(buildPath))
            {
                System.IO.Directory.Delete(buildPath, true);
                System.IO.Directory.CreateDirectory(buildPath);
            }
            
            Debug.Log("构建目录已清理");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"清理构建目录时出错: {e.Message}");
        }
    }

    /// <summary>
    /// 生成构建报告
    /// </summary>
    public void GenerateBuildReport()
    {
        Debug.Log("=== 构建系统报告 ===");
        Debug.Log($"构建系统状态: {buildState}");
        Debug.Log($"构建目标: {buildTarget}");
        Debug.Log($"构建路径: {buildPath}");
        Debug.Log($"构建名称: {buildName}");
        Debug.Log($"构建选项: {buildOptions}");
        Debug.Log($"最后构建结果: {lastBuildResult}");
        Debug.Log($"最后构建路径: {lastBuildPath}");
        Debug.Log($"最后构建大小: {FormatFileSize(lastBuildSize)}");
        Debug.Log($"总构建次数: {totalBuildCount}");
        Debug.Log($"平均构建时间: {averageBuildTime:F2}秒");
        Debug.Log($"最大构建时间: {maxBuildTime:F2}秒");
        Debug.Log($"总构建时间: {totalBuildTime:F2}秒");
        
        if (lastBuildReport != null)
        {
            Debug.Log("=== 最后构建报告 ===");
            Debug.Log(buildSummary);
        }
    }

    /// <summary>
    /// 清除构建历史
    /// </summary>
    public void ClearBuildHistory()
    {
        buildHistory = new BuildHistoryEntry[10];
        buildHistoryIndex = 0;
        Debug.Log("构建历史已清除");
    }

    // IPreprocessBuildWithReport 接口实现
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("构建预处理开始");
        
        // 在这里可以添加构建前的准备工作
        // 例如：清理临时文件、验证资源等
    }

    // IPostprocessBuildWithReport 接口实现
    public void OnPostprocessBuild(BuildReport report)
    {
        Debug.Log("构建后处理开始");
        
        // 在这里可以添加构建后的处理工作
        // 例如：复制额外文件、设置权限等
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Build 构建系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("构建系统配置:");
        enableBuildSystem = GUILayout.Toggle(enableBuildSystem, "启用构建系统");
        enableBuildLogging = GUILayout.Toggle(enableBuildLogging, "启用构建日志");
        enableBuildValidation = GUILayout.Toggle(enableBuildValidation, "启用构建验证");
        enableBuildOptimization = GUILayout.Toggle(enableBuildOptimization, "启用构建优化");
        enableBuildCompression = GUILayout.Toggle(enableBuildCompression, "启用构建压缩");
        
        GUILayout.Space(10);
        GUILayout.Label("构建目标:");
        buildTarget = (BuildTarget)System.Enum.Parse(typeof(BuildTarget), GUILayout.TextField("构建目标", buildTarget.ToString()));
        buildPath = GUILayout.TextField("构建路径", buildPath);
        buildName = GUILayout.TextField("构建名称", buildName);
        
        GUILayout.Space(10);
        GUILayout.Label("构建配置:");
        developmentBuild = GUILayout.Toggle(developmentBuild, "开发构建");
        debugBuild = GUILayout.Toggle(debugBuild, "调试构建");
        allowDebugging = GUILayout.Toggle(allowDebugging, "允许调试");
        compressWithLz4 = GUILayout.Toggle(compressWithLz4, "使用LZ4压缩");
        compressWithLz4HC = GUILayout.Toggle(compressWithLz4HC, "使用LZ4HC压缩");
        buildAppBundle = GUILayout.Toggle(buildAppBundle, "构建应用包");
        
        GUILayout.Space(10);
        GUILayout.Label("构建状态:");
        GUILayout.Label($"构建状态: {buildState}");
        GUILayout.Label($"构建进度: {buildProgress * 100:F1}%");
        GUILayout.Label($"构建消息: {buildMessage}");
        GUILayout.Label($"最后结果: {lastBuildResult}");
        GUILayout.Label($"最后路径: {lastBuildPath}");
        GUILayout.Label($"最后大小: {FormatFileSize(lastBuildSize)}");
        GUILayout.Label($"总构建次数: {totalBuildCount}");
        GUILayout.Label($"平均构建时间: {averageBuildTime:F2}秒");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("开始构建"))
        {
            StartBuild();
        }
        
        if (GUILayout.Button("清理构建"))
        {
            CleanBuild();
        }
        
        if (GUILayout.Button("生成构建报告"))
        {
            GenerateBuildReport();
        }
        
        if (GUILayout.Button("清除构建历史"))
        {
            ClearBuildHistory();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("构建历史:");
        for (int i = 0; i < buildHistory.Length; i++)
        {
            if (buildHistory[i] != null && !string.IsNullOrEmpty(buildHistory[i].timestamp))
            {
                GUILayout.Label($"{buildHistory[i].timestamp} - {buildHistory[i].buildTarget} - {buildHistory[i].buildResult}");
            }
        }
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 构建历史条目
/// </summary>
[System.Serializable]
public class BuildHistoryEntry
{
    public string timestamp;
    public string buildTarget;
    public string buildResult;
    public string buildPath;
    public long buildSize;
    public float buildTime;
    public int totalErrors;
    public int totalWarnings;
} 