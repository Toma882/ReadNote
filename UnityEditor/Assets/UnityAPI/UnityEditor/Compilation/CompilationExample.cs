using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.Compilation 命名空间案例演示
/// 展示编译系统的使用，包括编译状态、编译错误和编译回调
/// </summary>
public class CompilationExample : MonoBehaviour
{
    [Header("编译系统配置")]
    [SerializeField] private bool enableCompilationSystem = true;
    [SerializeField] private bool enableCompilationLogging = true;
    [SerializeField] private bool enableCompilationValidation = true;
    [SerializeField] private bool enableCompilationPerformance = true;
    [SerializeField] private bool enableCompilationHistory = true;
    
    [Header("编译状态")]
    [SerializeField] private CompilationStatus compilationStatus = CompilationStatus.Idle;
    [SerializeField] private bool isCompiling = false;
    [SerializeField] private float compilationProgress = 0f;
    [SerializeField] private string compilationMessage = "";
    [SerializeField] private int totalAssemblies = 0;
    [SerializeField] private int compiledAssemblies = 0;
    [SerializeField] private int failedAssemblies = 0;
    
    [Header("编译错误")]
    [SerializeField] private CompilerMessage[] compilerMessages = new CompilerMessage[0];
    [SerializeField] private int totalErrors = 0;
    [SerializeField] private int totalWarnings = 0;
    [SerializeField] private string[] errorMessages = new string[0];
    [SerializeField] private string[] warningMessages = new string[0];
    
    [Header("编译历史")]
    [SerializeField] private CompilationHistoryEntry[] compilationHistory = new CompilationHistoryEntry[10];
    [SerializeField] private int compilationHistoryIndex = 0;
    [SerializeField] private bool enableCompilationHistory = true;
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceMonitoring = true;
    [SerializeField] private float[] compilationTimeHistory = new float[100];
    [SerializeField] private int compilationTimeIndex = 0;
    [SerializeField] private float averageCompilationTime = 0f;
    [SerializeField] private float maxCompilationTime = 0f;
    [SerializeField] private float totalCompilationTime = 0f;
    [SerializeField] private int totalCompilationCount = 0;
    
    [Header("编译统计")]
    [SerializeField] private Dictionary<string, int> assemblyCompilationCount = new Dictionary<string, int>();
    [SerializeField] private Dictionary<string, float> assemblyCompilationTime = new Dictionary<string, float>();
    [SerializeField] private int totalScripts = 0;
    [SerializeField] private int totalScriptLines = 0;
    [SerializeField] private long totalScriptSize = 0;
    
    [Header("编译配置")]
    [SerializeField] private bool enableAutoCompilation = true;
    [SerializeField] private bool enableBackgroundCompilation = true;
    [SerializeField] private bool enableScriptCompilation = true;
    [SerializeField] private bool enableAssemblyCompilation = true;
    [SerializeField] private int maxCompilationTimeSeconds = 300;
    
    private bool isInitialized = false;
    private float compilationStartTime = 0f;
    private System.Action<CompilationResult> compilationCallback;

    private void Start()
    {
        InitializeCompilationSystem();
    }

    private void InitializeCompilationSystem()
    {
        if (!enableCompilationSystem) return;
        
        InitializeCompilationState();
        InitializePerformanceMonitoring();
        InitializeCompilationStatistics();
        RegisterCompilationCallbacks();
        
        isInitialized = true;
        compilationStatus = CompilationStatus.Idle;
        Debug.Log("编译系统初始化完成");
    }

    private void InitializeCompilationState()
    {
        compilationStatus = CompilationStatus.Idle;
        isCompiling = false;
        compilationProgress = 0f;
        compilationMessage = "就绪";
        totalAssemblies = 0;
        compiledAssemblies = 0;
        failedAssemblies = 0;
        
        Debug.Log("编译状态已初始化");
    }

    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            compilationTimeHistory = new float[100];
            compilationTimeIndex = 0;
            averageCompilationTime = 0f;
            maxCompilationTime = 0f;
            totalCompilationTime = 0f;
            totalCompilationCount = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    private void InitializeCompilationStatistics()
    {
        assemblyCompilationCount.Clear();
        assemblyCompilationTime.Clear();
        totalScripts = 0;
        totalScriptLines = 0;
        totalScriptSize = 0;
        
        Debug.Log("编译统计初始化完成");
    }

    private void RegisterCompilationCallbacks()
    {
        CompilationPipeline.compilationStarted += OnCompilationStarted;
        CompilationPipeline.compilationFinished += OnCompilationFinished;
        
        compilationCallback = OnCompilationResult;
        
        Debug.Log("编译回调已注册");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        UpdateCompilationStatus();
        UpdateCompilationProgress();
        
        if (enablePerformanceMonitoring)
        {
            UpdatePerformanceMonitoring();
        }
    }

    private void UpdateCompilationStatus()
    {
        if (isCompiling)
        {
            compilationStatus = CompilationStatus.Compiling;
        }
        else
        {
            compilationStatus = CompilationStatus.Idle;
        }
    }

    private void UpdateCompilationProgress()
    {
        if (isCompiling)
        {
            // 模拟编译进度
            compilationProgress = Mathf.Clamp01(compilationProgress + Time.deltaTime * 0.1f);
        }
        else
        {
            compilationProgress = 0f;
        }
    }

    private void UpdatePerformanceMonitoring()
    {
        if (totalCompilationCount > 0)
        {
            averageCompilationTime = totalCompilationTime / totalCompilationCount;
        }
    }

    private void OnCompilationStarted(object obj)
    {
        compilationStartTime = Time.realtimeSinceStartup;
        isCompiling = true;
        compilationProgress = 0f;
        compilationMessage = "编译开始...";
        
        // 获取编译信息
        UpdateCompilationInfo();
        
        if (enableCompilationLogging)
        {
            Debug.Log("编译开始");
        }
    }

    private void OnCompilationFinished(object obj)
    {
        float compilationTime = Time.realtimeSinceStartup - compilationStartTime;
        isCompiling = false;
        compilationProgress = 1f;
        compilationMessage = "编译完成";
        
        // 更新编译结果
        UpdateCompilationResult();
        
        // 更新性能数据
        UpdateCompilationPerformance(compilationTime);
        
        // 记录到历史
        if (enableCompilationHistory)
        {
            AddCompilationHistoryEntry(compilationTime);
        }
        
        if (enableCompilationLogging)
        {
            Debug.Log($"编译完成，耗时: {compilationTime:F2}秒");
        }
    }

    private void OnCompilationResult(CompilationResult result)
    {
        if (result.success)
        {
            compilationMessage = "编译成功";
            Debug.Log("编译成功");
        }
        else
        {
            compilationMessage = "编译失败";
            Debug.LogError("编译失败");
        }
    }

    private void UpdateCompilationInfo()
    {
        // 获取所有程序集
        var assemblies = CompilationPipeline.GetAssemblies();
        totalAssemblies = assemblies.Length;
        
        // 统计脚本信息
        CountScripts();
        
        Debug.Log($"编译信息更新: {totalAssemblies} 个程序集, {totalScripts} 个脚本");
    }

    private void CountScripts()
    {
        totalScripts = 0;
        totalScriptLines = 0;
        totalScriptSize = 0;
        
        // 获取所有脚本文件
        string[] scriptGuids = AssetDatabase.FindAssets("t:Script");
        
        foreach (string guid in scriptGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!string.IsNullOrEmpty(path))
            {
                totalScripts++;
                
                // 计算脚本大小
                var scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                if (scriptAsset != null)
                {
                    string scriptText = scriptAsset.text;
                    if (!string.IsNullOrEmpty(scriptText))
                    {
                        totalScriptLines += scriptText.Split('\n').Length;
                        totalScriptSize += System.Text.Encoding.UTF8.GetByteCount(scriptText);
                    }
                }
            }
        }
    }

    private void UpdateCompilationResult()
    {
        // 获取编译消息
        compilerMessages = CompilationPipeline.GetCompilerMessages();
        
        totalErrors = 0;
        totalWarnings = 0;
        List<string> errors = new List<string>();
        List<string> warnings = new List<string>();
        
        foreach (var message in compilerMessages)
        {
            if (message.type == CompilerMessageType.Error)
            {
                totalErrors++;
                errors.Add($"{message.file}:{message.line} - {message.message}");
            }
            else if (message.type == CompilerMessageType.Warning)
            {
                totalWarnings++;
                warnings.Add($"{message.file}:{message.line} - {message.message}");
            }
        }
        
        errorMessages = errors.ToArray();
        warningMessages = warnings.ToArray();
        
        // 更新程序集统计
        UpdateAssemblyStatistics();
    }

    private void UpdateAssemblyStatistics()
    {
        var assemblies = CompilationPipeline.GetAssemblies();
        compiledAssemblies = 0;
        failedAssemblies = 0;
        
        foreach (var assembly in assemblies)
        {
            string assemblyName = assembly.name;
            
            if (!assemblyCompilationCount.ContainsKey(assemblyName))
            {
                assemblyCompilationCount[assemblyName] = 0;
            }
            assemblyCompilationCount[assemblyName]++;
            
            if (assembly.status == AssemblyStatus.Compiled)
            {
                compiledAssemblies++;
            }
            else if (assembly.status == AssemblyStatus.CompilationError)
            {
                failedAssemblies++;
            }
        }
    }

    private void UpdateCompilationPerformance(float compilationTime)
    {
        if (enablePerformanceMonitoring)
        {
            compilationTimeHistory[compilationTimeIndex] = compilationTime;
            compilationTimeIndex = (compilationTimeIndex + 1) % 100;
            
            totalCompilationTime += compilationTime;
            totalCompilationCount++;
            
            if (compilationTime > maxCompilationTime)
            {
                maxCompilationTime = compilationTime;
            }
        }
    }

    private void AddCompilationHistoryEntry(float compilationTime)
    {
        var entry = new CompilationHistoryEntry
        {
            timestamp = System.DateTime.Now.ToString(),
            compilationTime = compilationTime,
            totalAssemblies = totalAssemblies,
            compiledAssemblies = compiledAssemblies,
            failedAssemblies = failedAssemblies,
            totalErrors = totalErrors,
            totalWarnings = totalWarnings,
            success = totalErrors == 0
        };
        
        compilationHistory[compilationHistoryIndex] = entry;
        compilationHistoryIndex = (compilationHistoryIndex + 1) % compilationHistory.Length;
    }

    public void ForceCompilation()
    {
        if (isCompiling)
        {
            Debug.LogWarning("编译正在进行中，请等待完成");
            return;
        }
        
        compilationStartTime = Time.realtimeSinceStartup;
        isCompiling = true;
        compilationProgress = 0f;
        compilationMessage = "强制编译开始...";
        
        // 强制重新编译
        AssetDatabase.Refresh();
        
        Debug.Log("强制编译已启动");
    }

    public void RequestScriptCompilation()
    {
        if (isCompiling)
        {
            Debug.LogWarning("编译正在进行中，请等待完成");
            return;
        }
        
        compilationStartTime = Time.realtimeSinceStartup;
        isCompiling = true;
        compilationProgress = 0f;
        compilationMessage = "脚本编译请求...";
        
        // 请求脚本编译
        CompilationPipeline.RequestScriptCompilation();
        
        Debug.Log("脚本编译请求已发送");
    }

    public void CancelCompilation()
    {
        if (!isCompiling)
        {
            Debug.LogWarning("没有正在进行的编译");
            return;
        }
        
        // 注意：Unity的编译系统通常不允许取消编译
        // 这里只是示例
        compilationMessage = "编译取消请求已发送";
        
        Debug.Log("编译取消请求已发送");
    }

    public void GenerateCompilationReport()
    {
        Debug.Log("=== 编译系统报告 ===");
        Debug.Log($"编译系统状态: {compilationStatus}");
        Debug.Log($"是否正在编译: {isCompiling}");
        Debug.Log($"编译进度: {compilationProgress * 100:F1}%");
        Debug.Log($"编译消息: {compilationMessage}");
        Debug.Log($"总程序集数: {totalAssemblies}");
        Debug.Log($"已编译程序集数: {compiledAssemblies}");
        Debug.Log($"失败程序集数: {failedAssemblies}");
        Debug.Log($"总错误数: {totalErrors}");
        Debug.Log($"总警告数: {totalWarnings}");
        Debug.Log($"总脚本数: {totalScripts}");
        Debug.Log($"总脚本行数: {totalScriptLines}");
        Debug.Log($"总脚本大小: {FormatFileSize(totalScriptSize)}");
        Debug.Log($"总编译次数: {totalCompilationCount}");
        Debug.Log($"平均编译时间: {averageCompilationTime:F2}秒");
        Debug.Log($"最大编译时间: {maxCompilationTime:F2}秒");
        Debug.Log($"总编译时间: {totalCompilationTime:F2}秒");
        
        if (totalErrors > 0)
        {
            Debug.Log("=== 编译错误 ===");
            foreach (string error in errorMessages)
            {
                Debug.LogError(error);
            }
        }
        
        if (totalWarnings > 0)
        {
            Debug.Log("=== 编译警告 ===");
            foreach (string warning in warningMessages)
            {
                Debug.LogWarning(warning);
            }
        }
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

    public void ClearCompilationHistory()
    {
        compilationHistory = new CompilationHistoryEntry[10];
        compilationHistoryIndex = 0;
        Debug.Log("编译历史已清除");
    }

    public void ResetCompilationStatistics()
    {
        assemblyCompilationCount.Clear();
        assemblyCompilationTime.Clear();
        totalCompilationCount = 0;
        totalCompilationTime = 0f;
        averageCompilationTime = 0f;
        maxCompilationTime = 0f;
        
        Debug.Log("编译统计已重置");
    }

    private void OnDestroy()
    {
        CompilationPipeline.compilationStarted -= OnCompilationStarted;
        CompilationPipeline.compilationFinished -= OnCompilationFinished;
        
        Debug.Log("编译回调已清理");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Compilation 编译系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("编译系统配置:");
        enableCompilationSystem = GUILayout.Toggle(enableCompilationSystem, "启用编译系统");
        enableCompilationLogging = GUILayout.Toggle(enableCompilationLogging, "启用编译日志");
        enableCompilationValidation = GUILayout.Toggle(enableCompilationValidation, "启用编译验证");
        enableCompilationPerformance = GUILayout.Toggle(enableCompilationPerformance, "启用编译性能监控");
        enableCompilationHistory = GUILayout.Toggle(enableCompilationHistory, "启用编译历史记录");
        
        GUILayout.Space(10);
        GUILayout.Label("编译配置:");
        enableAutoCompilation = GUILayout.Toggle(enableAutoCompilation, "启用自动编译");
        enableBackgroundCompilation = GUILayout.Toggle(enableBackgroundCompilation, "启用后台编译");
        enableScriptCompilation = GUILayout.Toggle(enableScriptCompilation, "启用脚本编译");
        enableAssemblyCompilation = GUILayout.Toggle(enableAssemblyCompilation, "启用程序集编译");
        
        GUILayout.Space(10);
        GUILayout.Label("编译状态:");
        GUILayout.Label($"编译状态: {compilationStatus}");
        GUILayout.Label($"是否正在编译: {isCompiling}");
        GUILayout.Label($"编译进度: {compilationProgress * 100:F1}%");
        GUILayout.Label($"编译消息: {compilationMessage}");
        GUILayout.Label($"总程序集数: {totalAssemblies}");
        GUILayout.Label($"已编译程序集数: {compiledAssemblies}");
        GUILayout.Label($"失败程序集数: {failedAssemblies}");
        GUILayout.Label($"总错误数: {totalErrors}");
        GUILayout.Label($"总警告数: {totalWarnings}");
        GUILayout.Label($"总脚本数: {totalScripts}");
        GUILayout.Label($"总编译次数: {totalCompilationCount}");
        GUILayout.Label($"平均编译时间: {averageCompilationTime:F2}秒");
        GUILayout.Label($"最大编译时间: {maxCompilationTime:F2}秒");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("强制编译"))
        {
            ForceCompilation();
        }
        
        if (GUILayout.Button("请求脚本编译"))
        {
            RequestScriptCompilation();
        }
        
        if (GUILayout.Button("取消编译"))
        {
            CancelCompilation();
        }
        
        if (GUILayout.Button("生成编译报告"))
        {
            GenerateCompilationReport();
        }
        
        if (GUILayout.Button("清除编译历史"))
        {
            ClearCompilationHistory();
        }
        
        if (GUILayout.Button("重置编译统计"))
        {
            ResetCompilationStatistics();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("编译历史:");
        for (int i = 0; i < compilationHistory.Length; i++)
        {
            if (compilationHistory[i] != null && !string.IsNullOrEmpty(compilationHistory[i].timestamp))
            {
                var entry = compilationHistory[i];
                string status = entry.success ? "成功" : "失败";
                GUILayout.Label($"{entry.timestamp} - {status} - {entry.compilationTime:F2}s - {entry.totalErrors}错误");
            }
        }
        
        GUILayout.EndArea();
    }
}

public enum CompilationStatus
{
    Idle,
    Compiling,
    Success,
    Failed
}

[System.Serializable]
public class CompilationHistoryEntry
{
    public string timestamp;
    public float compilationTime;
    public int totalAssemblies;
    public int compiledAssemblies;
    public int failedAssemblies;
    public int totalErrors;
    public int totalWarnings;
    public bool success;
} 