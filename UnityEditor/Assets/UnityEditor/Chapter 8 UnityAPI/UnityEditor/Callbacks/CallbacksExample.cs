using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.Callbacks 命名空间案例演示
/// 展示编辑器回调系统的使用，包括构建回调、场景回调等
/// </summary>
public class CallbacksExample : MonoBehaviour, IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    [Header("回调系统配置")]
    [SerializeField] private bool enableCallbackSystem = true;
    [SerializeField] private bool enableCallbackLogging = true;
    [SerializeField] private bool enableCallbackValidation = true;
    [SerializeField] private bool enableCallbackPerformance = true;
    [SerializeField] private bool enableCallbackHistory = true;
    
    [Header("回调类型")]
    [SerializeField] private CallbackType currentCallbackType = CallbackType.None;
    [SerializeField] private bool enableBuildCallbacks = true;
    [SerializeField] private bool enableSceneCallbacks = true;
    [SerializeField] private bool enableAssetCallbacks = true;
    [SerializeField] private bool enableEditorCallbacks = true;
    
    [Header("回调状态")]
    [SerializeField] private string callbackSystemState = "未初始化";
    [SerializeField] private bool isCallbackExecuting = false;
    [SerializeField] private float callbackExecutionTime = 0f;
    [SerializeField] private int callbackExecutionCount = 0;
    [SerializeField] private string lastCallbackResult = "";
    
    [Header("回调历史")]
    [SerializeField] private CallbackHistoryEntry[] callbackHistory = new CallbackHistoryEntry[20];
    [SerializeField] private int callbackHistoryIndex = 0;
    [SerializeField] private bool enableCallbackHistory = true;
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceMonitoring = true;
    [SerializeField] private float[] executionTimeHistory = new float[100];
    [SerializeField] private int executionTimeIndex = 0;
    [SerializeField] private float averageExecutionTime = 0f;
    [SerializeField] private float maxExecutionTime = 0f;
    [SerializeField] private float totalExecutionTime = 0f;
    [SerializeField] private int totalExecutionCount = 0;
    
    [Header("回调统计")]
    [SerializeField] private Dictionary<CallbackType, int> callbackTypeCount = new Dictionary<CallbackType, int>();
    [SerializeField] private Dictionary<string, int> callbackMethodCount = new Dictionary<string, int>();
    [SerializeField] private int totalBuildCallbacks = 0;
    [SerializeField] private int totalSceneCallbacks = 0;
    [SerializeField] private int totalAssetCallbacks = 0;
    [SerializeField] private int totalEditorCallbacks = 0;
    
    [Header("回调配置")]
    [SerializeField] private int callbackOrder = 0;
    [SerializeField] private bool enableCallbackOrdering = true;
    [SerializeField] private bool enableCallbackChaining = true;
    [SerializeField] private bool enableCallbackErrorHandling = true;
    
    private bool isInitialized = false;
    private float callbackStartTime = 0f;
    private List<System.Action> pendingCallbacks = new List<System.Action>();
    private List<System.Action> completedCallbacks = new List<System.Action>();

    public int callbackOrder => this.callbackOrder;

    private void Start()
    {
        InitializeCallbackSystem();
    }

    private void InitializeCallbackSystem()
    {
        if (!enableCallbackSystem) return;
        
        InitializeCallbackState();
        InitializePerformanceMonitoring();
        InitializeCallbackStatistics();
        RegisterCallbacks();
        
        isInitialized = true;
        callbackSystemState = "已初始化";
        Debug.Log("回调系统初始化完成");
    }

    private void InitializeCallbackState()
    {
        currentCallbackType = CallbackType.None;
        isCallbackExecuting = false;
        callbackExecutionTime = 0f;
        callbackExecutionCount = 0;
        lastCallbackResult = "就绪";
        
        Debug.Log("回调状态已初始化");
    }

    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            executionTimeHistory = new float[100];
            executionTimeIndex = 0;
            averageExecutionTime = 0f;
            maxExecutionTime = 0f;
            totalExecutionTime = 0f;
            totalExecutionCount = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    private void InitializeCallbackStatistics()
    {
        callbackTypeCount.Clear();
        callbackMethodCount.Clear();
        totalBuildCallbacks = 0;
        totalSceneCallbacks = 0;
        totalAssetCallbacks = 0;
        totalEditorCallbacks = 0;
        
        Debug.Log("回调统计初始化完成");
    }

    private void RegisterCallbacks()
    {
        if (enableBuildCallbacks)
        {
            RegisterBuildCallbacks();
        }
        
        if (enableSceneCallbacks)
        {
            RegisterSceneCallbacks();
        }
        
        if (enableAssetCallbacks)
        {
            RegisterAssetCallbacks();
        }
        
        if (enableEditorCallbacks)
        {
            RegisterEditorCallbacks();
        }
        
        Debug.Log("回调方法注册完成");
    }

    private void RegisterBuildCallbacks()
    {
        totalBuildCallbacks = 2;
        Debug.Log("构建回调已注册");
    }

    private void RegisterSceneCallbacks()
    {
        UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += OnSceneOpened;
        UnityEditor.SceneManagement.EditorSceneManager.sceneClosing += OnSceneClosing;
        UnityEditor.SceneManagement.EditorSceneManager.sceneClosed += OnSceneClosed;
        
        totalSceneCallbacks = 3;
        Debug.Log("场景回调已注册");
    }

    private void RegisterAssetCallbacks()
    {
        AssetDatabase.importPackageStarted += OnImportPackageStarted;
        AssetDatabase.importPackageCompleted += OnImportPackageCompleted;
        AssetDatabase.importPackageFailed += OnImportPackageFailed;
        
        totalAssetCallbacks = 3;
        Debug.Log("资产回调已注册");
    }

    private void RegisterEditorCallbacks()
    {
        EditorApplication.update += OnEditorUpdate;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
        EditorApplication.projectChanged += OnProjectChanged;
        
        totalEditorCallbacks = 4;
        Debug.Log("编辑器回调已注册");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        UpdateCallbackStatus();
        ProcessPendingCallbacks();
        
        if (enablePerformanceMonitoring)
        {
            UpdatePerformanceMonitoring();
        }
    }

    private void UpdateCallbackStatus()
    {
        if (isCallbackExecuting)
        {
            callbackSystemState = "执行中";
        }
        else
        {
            callbackSystemState = "空闲";
        }
    }

    private void ProcessPendingCallbacks()
    {
        if (pendingCallbacks.Count > 0)
        {
            var callback = pendingCallbacks[0];
            pendingCallbacks.RemoveAt(0);
            
            ExecuteCallback(callback);
        }
    }

    private void ExecuteCallback(System.Action callback)
    {
        if (callback == null) return;
        
        callbackStartTime = Time.realtimeSinceStartup;
        isCallbackExecuting = true;
        callbackExecutionCount++;
        
        try
        {
            callback.Invoke();
            lastCallbackResult = "执行成功";
            
            if (enableCallbackLogging)
            {
                Debug.Log($"回调执行成功: {callback.Method.Name}");
            }
        }
        catch (System.Exception e)
        {
            lastCallbackResult = $"执行失败: {e.Message}";
            
            if (enableCallbackErrorHandling)
            {
                Debug.LogError($"回调执行失败: {e.Message}");
            }
        }
        
        callbackExecutionTime = Time.realtimeSinceStartup - callbackStartTime;
        isCallbackExecuting = false;
        
        UpdateCallbackPerformance();
        
        if (enableCallbackHistory)
        {
            AddCallbackHistoryEntry(callback.Method.Name, lastCallbackResult, callbackExecutionTime);
        }
        
        completedCallbacks.Add(callback);
    }

    private void UpdateCallbackPerformance()
    {
        if (enablePerformanceMonitoring)
        {
            executionTimeHistory[executionTimeIndex] = callbackExecutionTime;
            executionTimeIndex = (executionTimeIndex + 1) % 100;
            
            totalExecutionTime += callbackExecutionTime;
            totalExecutionCount++;
            
            if (callbackExecutionTime > maxExecutionTime)
            {
                maxExecutionTime = callbackExecutionTime;
            }
        }
    }

    private void UpdatePerformanceMonitoring()
    {
        if (totalExecutionCount > 0)
        {
            averageExecutionTime = totalExecutionTime / totalExecutionCount;
        }
    }

    private void AddCallbackHistoryEntry(string methodName, string result, float executionTime)
    {
        var entry = new CallbackHistoryEntry
        {
            timestamp = System.DateTime.Now.ToString(),
            methodName = methodName,
            result = result,
            executionTime = executionTime,
            callbackType = currentCallbackType.ToString()
        };
        
        callbackHistory[callbackHistoryIndex] = entry;
        callbackHistoryIndex = (callbackHistoryIndex + 1) % callbackHistory.Length;
        
        UpdateCallbackStatistics(methodName);
    }

    private void UpdateCallbackStatistics(string methodName)
    {
        if (!callbackMethodCount.ContainsKey(methodName))
        {
            callbackMethodCount[methodName] = 0;
        }
        callbackMethodCount[methodName]++;
        
        if (!callbackTypeCount.ContainsKey(currentCallbackType))
        {
            callbackTypeCount[currentCallbackType] = 0;
        }
        callbackTypeCount[currentCallbackType]++;
    }

    private void OnSceneOpened(UnityEngine.SceneManagement.Scene scene, UnityEditor.SceneManagement.OpenSceneMode mode)
    {
        currentCallbackType = CallbackType.Scene;
        Debug.Log($"场景已打开: {scene.name}, 模式: {mode}");
    }

    private void OnSceneClosing(UnityEngine.SceneManagement.Scene scene, bool removingScene)
    {
        currentCallbackType = CallbackType.Scene;
        Debug.Log($"场景正在关闭: {scene.name}, 移除: {removingScene}");
    }

    private void OnSceneClosed(UnityEngine.SceneManagement.Scene scene)
    {
        currentCallbackType = CallbackType.Scene;
        Debug.Log($"场景已关闭: {scene.name}");
    }

    private void OnImportPackageStarted(string packageName)
    {
        currentCallbackType = CallbackType.Asset;
        Debug.Log($"包导入开始: {packageName}");
    }

    private void OnImportPackageCompleted(string packageName)
    {
        currentCallbackType = CallbackType.Asset;
        Debug.Log($"包导入完成: {packageName}");
    }

    private void OnImportPackageFailed(string packageName, string errorMessage)
    {
        currentCallbackType = CallbackType.Asset;
        Debug.LogError($"包导入失败: {packageName}, 错误: {errorMessage}");
    }

    private void OnEditorUpdate()
    {
        currentCallbackType = CallbackType.Editor;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        currentCallbackType = CallbackType.Editor;
        Debug.Log($"播放模式状态改变: {state}");
    }

    private void OnHierarchyChanged()
    {
        currentCallbackType = CallbackType.Editor;
        Debug.Log("层级视图已改变");
    }

    private void OnProjectChanged()
    {
        currentCallbackType = CallbackType.Editor;
        Debug.Log("项目已改变");
    }

    public void OnPreprocessBuild(BuildReport report)
    {
        currentCallbackType = CallbackType.Build;
        Debug.Log("构建预处理开始");
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        currentCallbackType = CallbackType.Build;
        Debug.Log("构建后处理开始");
    }

    public void AddPendingCallback(System.Action callback)
    {
        if (callback != null)
        {
            pendingCallbacks.Add(callback);
            Debug.Log($"已添加待执行回调: {callback.Method.Name}");
        }
    }

    public void ClearPendingCallbacks()
    {
        pendingCallbacks.Clear();
        Debug.Log("所有待执行回调已清除");
    }

    public void ClearCompletedCallbacks()
    {
        completedCallbacks.Clear();
        Debug.Log("所有已完成回调已清除");
    }

    public void GenerateCallbackReport()
    {
        Debug.Log("=== 回调系统报告 ===");
        Debug.Log($"回调系统状态: {callbackSystemState}");
        Debug.Log($"当前回调类型: {currentCallbackType}");
        Debug.Log($"回调执行次数: {callbackExecutionCount}");
        Debug.Log($"最后回调结果: {lastCallbackResult}");
        Debug.Log($"总执行次数: {totalExecutionCount}");
        Debug.Log($"平均执行时间: {averageExecutionTime:F4}秒");
        Debug.Log($"最大执行时间: {maxExecutionTime:F4}秒");
        Debug.Log($"总执行时间: {totalExecutionTime:F4}秒");
        Debug.Log($"待执行回调数: {pendingCallbacks.Count}");
        Debug.Log($"已完成回调数: {completedCallbacks.Count}");
        
        Debug.Log("=== 回调类型统计 ===");
        foreach (var kvp in callbackTypeCount)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value} 次");
        }
        
        Debug.Log("=== 回调方法统计 ===");
        foreach (var kvp in callbackMethodCount)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value} 次");
        }
    }

    public void ClearCallbackHistory()
    {
        callbackHistory = new CallbackHistoryEntry[20];
        callbackHistoryIndex = 0;
        Debug.Log("回调历史已清除");
    }

    public void ResetCallbackStatistics()
    {
        callbackTypeCount.Clear();
        callbackMethodCount.Clear();
        totalExecutionCount = 0;
        totalExecutionTime = 0f;
        averageExecutionTime = 0f;
        maxExecutionTime = 0f;
        
        Debug.Log("回调统计已重置");
    }

    private void OnDestroy()
    {
        if (enableSceneCallbacks)
        {
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened -= OnSceneOpened;
            UnityEditor.SceneManagement.EditorSceneManager.sceneClosing -= OnSceneClosing;
            UnityEditor.SceneManagement.EditorSceneManager.sceneClosed -= OnSceneClosed;
        }
        
        if (enableAssetCallbacks)
        {
            AssetDatabase.importPackageStarted -= OnImportPackageStarted;
            AssetDatabase.importPackageCompleted -= OnImportPackageCompleted;
            AssetDatabase.importPackageFailed -= OnImportPackageFailed;
        }
        
        if (enableEditorCallbacks)
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
            EditorApplication.projectChanged -= OnProjectChanged;
        }
        
        Debug.Log("回调注册已清理");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Callbacks 回调系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("回调系统配置:");
        enableCallbackSystem = GUILayout.Toggle(enableCallbackSystem, "启用回调系统");
        enableCallbackLogging = GUILayout.Toggle(enableCallbackLogging, "启用回调日志");
        enableCallbackValidation = GUILayout.Toggle(enableCallbackValidation, "启用回调验证");
        enableCallbackPerformance = GUILayout.Toggle(enableCallbackPerformance, "启用回调性能监控");
        enableCallbackHistory = GUILayout.Toggle(enableCallbackHistory, "启用回调历史记录");
        
        GUILayout.Space(10);
        GUILayout.Label("回调类型:");
        enableBuildCallbacks = GUILayout.Toggle(enableBuildCallbacks, "启用构建回调");
        enableSceneCallbacks = GUILayout.Toggle(enableSceneCallbacks, "启用场景回调");
        enableAssetCallbacks = GUILayout.Toggle(enableAssetCallbacks, "启用资产回调");
        enableEditorCallbacks = GUILayout.Toggle(enableEditorCallbacks, "启用编辑器回调");
        
        GUILayout.Space(10);
        GUILayout.Label("回调状态:");
        GUILayout.Label($"回调系统状态: {callbackSystemState}");
        GUILayout.Label($"当前回调类型: {currentCallbackType}");
        GUILayout.Label($"回调执行次数: {callbackExecutionCount}");
        GUILayout.Label($"最后回调结果: {lastCallbackResult}");
        GUILayout.Label($"总执行次数: {totalExecutionCount}");
        GUILayout.Label($"平均执行时间: {averageExecutionTime:F4}秒");
        GUILayout.Label($"最大执行时间: {maxExecutionTime:F4}秒");
        GUILayout.Label($"待执行回调数: {pendingCallbacks.Count}");
        GUILayout.Label($"已完成回调数: {completedCallbacks.Count}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("生成回调报告"))
        {
            GenerateCallbackReport();
        }
        
        if (GUILayout.Button("清除回调历史"))
        {
            ClearCallbackHistory();
        }
        
        if (GUILayout.Button("重置回调统计"))
        {
            ResetCallbackStatistics();
        }
        
        if (GUILayout.Button("清除待执行回调"))
        {
            ClearPendingCallbacks();
        }
        
        if (GUILayout.Button("清除已完成回调"))
        {
            ClearCompletedCallbacks();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("回调历史:");
        for (int i = 0; i < callbackHistory.Length; i++)
        {
            if (callbackHistory[i] != null && !string.IsNullOrEmpty(callbackHistory[i].timestamp))
            {
                GUILayout.Label($"{callbackHistory[i].timestamp} - {callbackHistory[i].methodName} - {callbackHistory[i].result}");
            }
        }
        
        GUILayout.EndArea();
    }
}

public enum CallbackType
{
    None,
    Build,
    Scene,
    Asset,
    Editor
}

[System.Serializable]
public class CallbackHistoryEntry
{
    public string timestamp;
    public string methodName;
    public string result;
    public float executionTime;
    public string callbackType;
} 