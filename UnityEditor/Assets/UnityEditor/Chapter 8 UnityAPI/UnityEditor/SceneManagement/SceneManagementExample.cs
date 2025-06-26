using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.SceneManagement 命名空间案例演示
/// 展示场景管理系统的使用，包括场景加载、保存、切换等
/// </summary>
public class SceneManagementExample : MonoBehaviour
{
    [Header("场景管理系统配置")]
    [SerializeField] private bool enableSceneManagement = true;
    [SerializeField] private bool enableSceneLogging = true;
    [SerializeField] private bool enableSceneValidation = true;
    [SerializeField] private bool enableScenePerformance = true;
    [SerializeField] private bool enableSceneHistory = true;
    
    [Header("场景配置")]
    [SerializeField] private string currentScenePath = "";
    [SerializeField] private string targetScenePath = "";
    [SerializeField] private OpenSceneMode openSceneMode = OpenSceneMode.Single;
    [SerializeField] private bool enableSceneAdditive = false;
    [SerializeField] private bool enableSceneAsync = false;
    [SerializeField] private bool enableSceneAutoSave = true;
    [SerializeField] private float autoSaveInterval = 300f; // 5分钟
    
    [Header("场景状态")]
    [SerializeField] private SceneManagementStatus sceneStatus = SceneManagementStatus.Idle;
    [SerializeField] private bool isSceneLoading = false;
    [SerializeField] private bool isSceneSaving = false;
    [SerializeField] private float sceneProgress = 0f;
    [SerializeField] private string sceneMessage = "";
    [SerializeField] private int totalScenes = 0;
    [SerializeField] private int loadedScenes = 0;
    [SerializeField] private int modifiedScenes = 0;
    
    [Header("场景信息")]
    [SerializeField] private SceneInfo[] sceneInfos = new SceneInfo[0];
    [SerializeField] private SceneInfo currentSceneInfo;
    [SerializeField] private SceneInfo[] loadedSceneInfos = new SceneInfo[0];
    [SerializeField] private string[] scenePaths = new string[0];
    [SerializeField] private string[] sceneNames = new string[0];
    [SerializeField] private bool[] sceneEnabled = new bool[0];
    
    [Header("场景历史")]
    [SerializeField] private SceneHistoryEntry[] sceneHistory = new SceneHistoryEntry[10];
    [SerializeField] private int sceneHistoryIndex = 0;
    [SerializeField] private bool enableSceneHistory = true;
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceMonitoring = true;
    [SerializeField] private float[] loadTimeHistory = new float[100];
    [SerializeField] private int loadTimeIndex = 0;
    [SerializeField] private float averageLoadTime = 0f;
    [SerializeField] private float maxLoadTime = 0f;
    [SerializeField] private float totalLoadTime = 0f;
    [SerializeField] private int totalLoadCount = 0;
    
    [Header("场景统计")]
    [SerializeField] private Dictionary<string, int> sceneLoadCount = new Dictionary<string, int>();
    [SerializeField] private Dictionary<string, float> sceneLoadTime = new Dictionary<string, float>();
    [SerializeField] private int totalGameObjects = 0;
    [SerializeField] private int totalComponents = 0;
    [SerializeField] private long totalSceneSize = 0;
    
    [Header("场景操作")]
    [SerializeField] private bool enableSceneBackup = true;
    [SerializeField] private bool enableSceneRestore = true;
    [SerializeField] private bool enableSceneDuplicate = true;
    [SerializeField] private bool enableSceneMerge = false;
    [SerializeField] private string backupPath = "SceneBackups/";
    
    private bool isInitialized = false;
    private float sceneStartTime = 0f;
    private AsyncOperation sceneAsyncOperation;
    private float lastAutoSaveTime = 0f;

    private void Start()
    {
        InitializeSceneManagement();
    }

    private void InitializeSceneManagement()
    {
        if (!enableSceneManagement) return;
        
        InitializeSceneState();
        InitializePerformanceMonitoring();
        InitializeSceneStatistics();
        RegisterSceneCallbacks();
        
        isInitialized = true;
        sceneStatus = SceneManagementStatus.Idle;
        Debug.Log("场景管理系统初始化完成");
    }

    private void InitializeSceneState()
    {
        sceneStatus = SceneManagementStatus.Idle;
        isSceneLoading = false;
        isSceneSaving = false;
        sceneProgress = 0f;
        sceneMessage = "就绪";
        totalScenes = 0;
        loadedScenes = 0;
        modifiedScenes = 0;
        
        Debug.Log("场景状态已初始化");
    }

    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            loadTimeHistory = new float[100];
            loadTimeIndex = 0;
            averageLoadTime = 0f;
            maxLoadTime = 0f;
            totalLoadTime = 0f;
            totalLoadCount = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    private void InitializeSceneStatistics()
    {
        sceneLoadCount.Clear();
        sceneLoadTime.Clear();
        totalGameObjects = 0;
        totalComponents = 0;
        totalSceneSize = 0;
        
        Debug.Log("场景统计初始化完成");
    }

    private void RegisterSceneCallbacks()
    {
        EditorSceneManager.sceneOpened += OnSceneOpened;
        EditorSceneManager.sceneClosing += OnSceneClosing;
        EditorSceneManager.sceneClosed += OnSceneClosed;
        EditorSceneManager.sceneSaving += OnSceneSaving;
        EditorSceneManager.sceneSaved += OnSceneSaved;
        
        Debug.Log("场景回调已注册");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        UpdateSceneStatus();
        UpdateSceneProgress();
        UpdateSceneInfo();
        
        if (enableSceneAutoSave)
        {
            CheckAutoSave();
        }
        
        if (enablePerformanceMonitoring)
        {
            UpdatePerformanceMonitoring();
        }
    }

    private void UpdateSceneStatus()
    {
        if (isSceneLoading)
        {
            sceneStatus = SceneManagementStatus.Loading;
        }
        else if (isSceneSaving)
        {
            sceneStatus = SceneManagementStatus.Saving;
        }
        else
        {
            sceneStatus = SceneManagementStatus.Idle;
        }
    }

    private void UpdateSceneProgress()
    {
        if (sceneAsyncOperation != null && !sceneAsyncOperation.isDone)
        {
            sceneProgress = sceneAsyncOperation.progress;
        }
        else
        {
            sceneProgress = 0f;
        }
    }

    private void UpdateSceneInfo()
    {
        // 更新当前场景信息
        var currentScene = EditorSceneManager.GetActiveScene();
        currentScenePath = currentScene.path;
        
        // 更新场景信息
        UpdateSceneInfos();
        
        // 更新统计信息
        UpdateSceneStatistics();
    }

    private void UpdateSceneInfos()
    {
        var scenes = EditorBuildSettings.scenes;
        sceneInfos = new SceneInfo[scenes.Length];
        scenePaths = new string[scenes.Length];
        sceneNames = new string[scenes.Length];
        sceneEnabled = new bool[scenes.Length];
        
        for (int i = 0; i < scenes.Length; i++)
        {
            var scene = scenes[i];
            sceneInfos[i] = new SceneInfo
            {
                path = scene.path,
                name = System.IO.Path.GetFileNameWithoutExtension(scene.path),
                enabled = scene.enabled,
                buildIndex = i
            };
            
            scenePaths[i] = scene.path;
            sceneNames[i] = sceneInfos[i].name;
            sceneEnabled[i] = scene.enabled;
        }
        
        totalScenes = scenes.Length;
        
        // 更新已加载场景信息
        var loadedScenes = new List<SceneInfo>();
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                loadedScenes.Add(new SceneInfo
                {
                    path = scene.path,
                    name = scene.name,
                    enabled = true,
                    buildIndex = scene.buildIndex
                });
            }
        }
        
        this.loadedSceneInfos = loadedScenes.ToArray();
        this.loadedScenes = loadedScenes.Count;
    }

    private void UpdateSceneStatistics()
    {
        totalGameObjects = 0;
        totalComponents = 0;
        
        var allGameObjects = FindObjectsOfType<GameObject>();
        totalGameObjects = allGameObjects.Length;
        
        foreach (var go in allGameObjects)
        {
            totalComponents += go.GetComponents<Component>().Length;
        }
        
        // 计算场景大小（估算）
        totalSceneSize = totalGameObjects * 1024; // 假设每个GameObject约1KB
    }

    private void CheckAutoSave()
    {
        if (Time.time - lastAutoSaveTime > autoSaveInterval)
        {
            if (EditorSceneManager.GetActiveScene().isDirty)
            {
                AutoSaveScene();
                lastAutoSaveTime = Time.time;
            }
        }
    }

    private void UpdatePerformanceMonitoring()
    {
        if (totalLoadCount > 0)
        {
            averageLoadTime = totalLoadTime / totalLoadCount;
        }
    }

    private void OnSceneOpened(UnityEngine.SceneManagement.Scene scene, OpenSceneMode mode)
    {
        sceneMessage = $"场景已打开: {scene.name}";
        
        if (enableSceneLogging)
        {
            Debug.Log($"场景已打开: {scene.name}, 模式: {mode}");
        }
    }

    private void OnSceneClosing(UnityEngine.SceneManagement.Scene scene, bool removingScene)
    {
        sceneMessage = $"场景正在关闭: {scene.name}";
        
        if (enableSceneLogging)
        {
            Debug.Log($"场景正在关闭: {scene.name}, 移除: {removingScene}");
        }
    }

    private void OnSceneClosed(UnityEngine.SceneManagement.Scene scene)
    {
        sceneMessage = $"场景已关闭: {scene.name}";
        
        if (enableSceneLogging)
        {
            Debug.Log($"场景已关闭: {scene.name}");
        }
    }

    private void OnSceneSaving(UnityEngine.SceneManagement.Scene scene, string path)
    {
        isSceneSaving = true;
        sceneMessage = $"场景正在保存: {scene.name}";
        
        if (enableSceneLogging)
        {
            Debug.Log($"场景正在保存: {scene.name} 到 {path}");
        }
    }

    private void OnSceneSaved(UnityEngine.SceneManagement.Scene scene)
    {
        isSceneSaving = false;
        sceneMessage = $"场景已保存: {scene.name}";
        
        if (enableSceneLogging)
        {
            Debug.Log($"场景已保存: {scene.name}");
        }
    }

    public void LoadScene(string scenePath)
    {
        if (isSceneLoading)
        {
            Debug.LogWarning("场景正在加载中，请等待完成");
            return;
        }
        
        if (string.IsNullOrEmpty(scenePath))
        {
            Debug.LogWarning("场景路径不能为空");
            return;
        }
        
        if (!enableSceneValidation || ValidateScenePath(scenePath))
        {
            isSceneLoading = true;
            sceneStartTime = Time.realtimeSinceStartup;
            sceneProgress = 0f;
            sceneMessage = $"正在加载场景: {scenePath}";
            
            if (enableSceneAsync)
            {
                LoadSceneAsync(scenePath);
            }
            else
            {
                LoadSceneSync(scenePath);
            }
        }
    }

    private bool ValidateScenePath(string scenePath)
    {
        bool isValid = true;
        
        if (string.IsNullOrEmpty(scenePath))
        {
            Debug.LogError("场景路径不能为空");
            isValid = false;
        }
        
        if (!System.IO.File.Exists(scenePath))
        {
            Debug.LogError($"场景文件不存在: {scenePath}");
            isValid = false;
        }
        
        return isValid;
    }

    private void LoadSceneAsync(string scenePath)
    {
        try
        {
            sceneAsyncOperation = EditorSceneManager.OpenSceneAsync(scenePath, openSceneMode);
            sceneAsyncOperation.completed += OnSceneLoadCompleted;
            
            Debug.Log($"开始异步加载场景: {scenePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"异步加载场景失败: {e.Message}");
            OnSceneLoadError(e);
        }
    }

    private void LoadSceneSync(string scenePath)
    {
        try
        {
            var scene = EditorSceneManager.OpenScene(scenePath, openSceneMode);
            
            float loadTime = Time.realtimeSinceStartup - sceneStartTime;
            OnSceneLoadCompleted(loadTime);
            
            Debug.Log($"同步加载场景完成: {scenePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"同步加载场景失败: {e.Message}");
            OnSceneLoadError(e);
        }
    }

    private void OnSceneLoadCompleted(AsyncOperation operation)
    {
        float loadTime = Time.realtimeSinceStartup - sceneStartTime;
        OnSceneLoadCompleted(loadTime);
    }

    private void OnSceneLoadCompleted(float loadTime)
    {
        isSceneLoading = false;
        sceneProgress = 1f;
        sceneMessage = "场景加载完成";
        
        UpdateSceneLoadPerformance(loadTime);
        
        if (enableSceneHistory)
        {
            AddSceneHistoryEntry("Load", currentScenePath, loadTime);
        }
        
        if (enableSceneLogging)
        {
            Debug.Log($"场景加载完成，耗时: {loadTime:F3}秒");
        }
    }

    private void OnSceneLoadError(System.Exception exception)
    {
        isSceneLoading = false;
        sceneProgress = 0f;
        sceneMessage = $"场景加载失败: {exception.Message}";
        
        Debug.LogError($"场景加载过程中发生错误: {exception}");
    }

    private void UpdateSceneLoadPerformance(float loadTime)
    {
        if (enablePerformanceMonitoring)
        {
            loadTimeHistory[loadTimeIndex] = loadTime;
            loadTimeIndex = (loadTimeIndex + 1) % 100;
            
            totalLoadTime += loadTime;
            totalLoadCount++;
            
            if (loadTime > maxLoadTime)
            {
                maxLoadTime = loadTime;
            }
            
            // 更新场景统计
            if (!sceneLoadCount.ContainsKey(currentScenePath))
            {
                sceneLoadCount[currentScenePath] = 0;
            }
            sceneLoadCount[currentScenePath]++;
            
            sceneLoadTime[currentScenePath] = loadTime;
        }
    }

    public void SaveScene()
    {
        if (isSceneSaving)
        {
            Debug.LogWarning("场景正在保存中，请等待完成");
            return;
        }
        
        var activeScene = EditorSceneManager.GetActiveScene();
        if (!activeScene.isDirty)
        {
            Debug.LogWarning("当前场景没有修改，无需保存");
            return;
        }
        
        isSceneSaving = true;
        sceneStartTime = Time.realtimeSinceStartup;
        sceneMessage = "正在保存场景...";
        
        try
        {
            bool success = EditorSceneManager.SaveScene(activeScene);
            
            float saveTime = Time.realtimeSinceStartup - sceneStartTime;
            isSceneSaving = false;
            
            if (success)
            {
                sceneMessage = "场景保存成功";
                if (enableSceneHistory)
                {
                    AddSceneHistoryEntry("Save", activeScene.path, saveTime);
                }
                Debug.Log($"场景保存成功，耗时: {saveTime:F3}秒");
            }
            else
            {
                sceneMessage = "场景保存失败";
                Debug.LogError("场景保存失败");
            }
        }
        catch (System.Exception e)
        {
            isSceneSaving = false;
            sceneMessage = $"场景保存失败: {e.Message}";
            Debug.LogError($"场景保存过程中发生错误: {e}");
        }
    }

    public void SaveSceneAs(string path)
    {
        if (isSceneSaving)
        {
            Debug.LogWarning("场景正在保存中，请等待完成");
            return;
        }
        
        isSceneSaving = true;
        sceneStartTime = Time.realtimeSinceStartup;
        sceneMessage = "正在另存为场景...";
        
        try
        {
            var activeScene = EditorSceneManager.GetActiveScene();
            bool success = EditorSceneManager.SaveScene(activeScene, path);
            
            float saveTime = Time.realtimeSinceStartup - sceneStartTime;
            isSceneSaving = false;
            
            if (success)
            {
                sceneMessage = "场景另存为成功";
                if (enableSceneHistory)
                {
                    AddSceneHistoryEntry("SaveAs", path, saveTime);
                }
                Debug.Log($"场景另存为成功: {path}, 耗时: {saveTime:F3}秒");
            }
            else
            {
                sceneMessage = "场景另存为失败";
                Debug.LogError("场景另存为失败");
            }
        }
        catch (System.Exception e)
        {
            isSceneSaving = false;
            sceneMessage = $"场景另存为失败: {e.Message}";
            Debug.LogError($"场景另存为过程中发生错误: {e}");
        }
    }

    public void CloseScene()
    {
        var activeScene = EditorSceneManager.GetActiveScene();
        if (activeScene.isDirty)
        {
            bool save = EditorUtility.DisplayDialog("保存场景", 
                $"场景 {activeScene.name} 已修改，是否保存？", "保存", "不保存");
            
            if (save)
            {
                SaveScene();
            }
        }
        
        EditorSceneManager.CloseScene(activeScene, true);
        Debug.Log($"场景已关闭: {activeScene.name}");
    }

    public void NewScene()
    {
        var activeScene = EditorSceneManager.GetActiveScene();
        if (activeScene.isDirty)
        {
            bool save = EditorUtility.DisplayDialog("保存场景", 
                $"场景 {activeScene.name} 已修改，是否保存？", "保存", "不保存");
            
            if (save)
            {
                SaveScene();
            }
        }
        
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        Debug.Log("新场景已创建");
    }

    private void AutoSaveScene()
    {
        if (enableSceneAutoSave)
        {
            var activeScene = EditorSceneManager.GetActiveScene();
            if (activeScene.isDirty)
            {
                string autoSavePath = System.IO.Path.Combine(backupPath, 
                    $"{activeScene.name}_AutoSave_{System.DateTime.Now:yyyyMMdd_HHmmss}.unity");
                
                try
                {
                    System.IO.Directory.CreateDirectory(backupPath);
                    EditorSceneManager.SaveScene(activeScene, autoSavePath);
                    Debug.Log($"场景自动保存: {autoSavePath}");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"场景自动保存失败: {e.Message}");
                }
            }
        }
    }

    private void AddSceneHistoryEntry(string operation, string scenePath, float time)
    {
        var entry = new SceneHistoryEntry
        {
            timestamp = System.DateTime.Now.ToString(),
            operation = operation,
            scenePath = scenePath,
            sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath),
            time = time,
            success = !string.IsNullOrEmpty(sceneMessage) && !sceneMessage.Contains("失败")
        };
        
        sceneHistory[sceneHistoryIndex] = entry;
        sceneHistoryIndex = (sceneHistoryIndex + 1) % sceneHistory.Length;
    }

    public void GenerateSceneReport()
    {
        Debug.Log("=== 场景管理系统报告 ===");
        Debug.Log($"场景管理系统状态: {sceneStatus}");
        Debug.Log($"当前场景路径: {currentScenePath}");
        Debug.Log($"总场景数: {totalScenes}");
        Debug.Log($"已加载场景数: {loadedScenes}");
        Debug.Log($"修改场景数: {modifiedScenes}");
        Debug.Log($"总游戏对象数: {totalGameObjects}");
        Debug.Log($"总组件数: {totalComponents}");
        Debug.Log($"总场景大小: {FormatFileSize(totalSceneSize)}");
        Debug.Log($"总加载次数: {totalLoadCount}");
        Debug.Log($"平均加载时间: {averageLoadTime:F3}秒");
        Debug.Log($"最大加载时间: {maxLoadTime:F3}秒");
        Debug.Log($"总加载时间: {totalLoadTime:F3}秒");
        
        Debug.Log("=== 场景加载统计 ===");
        foreach (var kvp in sceneLoadCount)
        {
            Debug.Log($"{System.IO.Path.GetFileNameWithoutExtension(kvp.Key)}: {kvp.Value} 次");
        }
        
        Debug.Log("=== 已加载场景 ===");
        foreach (var sceneInfo in loadedSceneInfos)
        {
            Debug.Log($"{sceneInfo.name} ({sceneInfo.path})");
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

    public void ClearSceneHistory()
    {
        sceneHistory = new SceneHistoryEntry[10];
        sceneHistoryIndex = 0;
        Debug.Log("场景历史已清除");
    }

    public void ResetSceneStatistics()
    {
        sceneLoadCount.Clear();
        sceneLoadTime.Clear();
        totalLoadCount = 0;
        totalLoadTime = 0f;
        averageLoadTime = 0f;
        maxLoadTime = 0f;
        
        Debug.Log("场景统计已重置");
    }

    private void OnDestroy()
    {
        EditorSceneManager.sceneOpened -= OnSceneOpened;
        EditorSceneManager.sceneClosing -= OnSceneClosing;
        EditorSceneManager.sceneClosed -= OnSceneClosed;
        EditorSceneManager.sceneSaving -= OnSceneSaving;
        EditorSceneManager.sceneSaved -= OnSceneSaved;
        
        Debug.Log("场景回调已清理");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("SceneManagement 场景管理系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("场景管理系统配置:");
        enableSceneManagement = GUILayout.Toggle(enableSceneManagement, "启用场景管理系统");
        enableSceneLogging = GUILayout.Toggle(enableSceneLogging, "启用场景日志");
        enableSceneValidation = GUILayout.Toggle(enableSceneValidation, "启用场景验证");
        enableScenePerformance = GUILayout.Toggle(enableScenePerformance, "启用场景性能监控");
        enableSceneHistory = GUILayout.Toggle(enableSceneHistory, "启用场景历史记录");
        
        GUILayout.Space(10);
        GUILayout.Label("场景配置:");
        targetScenePath = GUILayout.TextField("目标场景路径", targetScenePath);
        openSceneMode = (OpenSceneMode)System.Enum.Parse(typeof(OpenSceneMode), GUILayout.TextField("打开场景模式", openSceneMode.ToString()));
        enableSceneAdditive = GUILayout.Toggle(enableSceneAdditive, "启用场景叠加");
        enableSceneAsync = GUILayout.Toggle(enableSceneAsync, "启用异步加载");
        enableSceneAutoSave = GUILayout.Toggle(enableSceneAutoSave, "启用自动保存");
        autoSaveInterval = float.TryParse(GUILayout.TextField("自动保存间隔(秒)", autoSaveInterval.ToString()), out var interval) ? interval : autoSaveInterval;
        
        GUILayout.Space(10);
        GUILayout.Label("场景状态:");
        GUILayout.Label($"场景状态: {sceneStatus}");
        GUILayout.Label($"是否正在加载: {isSceneLoading}");
        GUILayout.Label($"是否正在保存: {isSceneSaving}");
        GUILayout.Label($"场景进度: {sceneProgress * 100:F1}%");
        GUILayout.Label($"场景消息: {sceneMessage}");
        GUILayout.Label($"当前场景路径: {currentScenePath}");
        GUILayout.Label($"总场景数: {totalScenes}");
        GUILayout.Label($"已加载场景数: {loadedScenes}");
        GUILayout.Label($"总游戏对象数: {totalGameObjects}");
        GUILayout.Label($"总组件数: {totalComponents}");
        GUILayout.Label($"总加载次数: {totalLoadCount}");
        GUILayout.Label($"平均加载时间: {averageLoadTime:F3}秒");
        GUILayout.Label($"最大加载时间: {maxLoadTime:F3}秒");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("加载场景"))
        {
            LoadScene(targetScenePath);
        }
        
        if (GUILayout.Button("保存场景"))
        {
            SaveScene();
        }
        
        if (GUILayout.Button("另存为场景"))
        {
            SaveSceneAs(targetScenePath);
        }
        
        if (GUILayout.Button("关闭场景"))
        {
            CloseScene();
        }
        
        if (GUILayout.Button("新建场景"))
        {
            NewScene();
        }
        
        if (GUILayout.Button("自动保存场景"))
        {
            AutoSaveScene();
        }
        
        if (GUILayout.Button("生成场景报告"))
        {
            GenerateSceneReport();
        }
        
        if (GUILayout.Button("清除场景历史"))
        {
            ClearSceneHistory();
        }
        
        if (GUILayout.Button("重置场景统计"))
        {
            ResetSceneStatistics();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("场景历史:");
        for (int i = 0; i < sceneHistory.Length; i++)
        {
            if (sceneHistory[i] != null && !string.IsNullOrEmpty(sceneHistory[i].timestamp))
            {
                var entry = sceneHistory[i];
                string status = entry.success ? "成功" : "失败";
                GUILayout.Label($"{entry.timestamp} - {entry.operation} - {entry.sceneName} - {status} - {entry.time:F3}s");
            }
        }
        
        GUILayout.EndArea();
    }
}

public enum SceneManagementStatus
{
    Idle,
    Loading,
    Saving,
    Completed,
    Failed
}

[System.Serializable]
public class SceneInfo
{
    public string path;
    public string name;
    public bool enabled;
    public int buildIndex;
}

[System.Serializable]
public class SceneHistoryEntry
{
    public string timestamp;
    public string operation;
    public string scenePath;
    public string sceneName;
    public float time;
    public bool success;
} 