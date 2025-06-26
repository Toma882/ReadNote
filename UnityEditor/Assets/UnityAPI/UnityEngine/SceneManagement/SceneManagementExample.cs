using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.SceneManagement 命名空间案例演示
/// 展示场景管理的核心功能
/// </summary>
public class SceneManagementExample : MonoBehaviour
{
    [Header("场景管理设置")]
    [SerializeField] private bool enableSceneManagement = true;
    [SerializeField] private bool autoLoadScenes = false;
    [SerializeField] private string[] sceneNames;
    [SerializeField] private int currentSceneIndex = 0;
    [SerializeField] private LoadSceneMode loadMode = LoadSceneMode.Single;
    
    [Header("场景状态")]
    [SerializeField] private bool isLoading = false;
    [SerializeField] private float loadProgress = 0f;
    [SerializeField] private string currentSceneName = "";
    [SerializeField] private int totalScenes = 0;
    [SerializeField] private List<string> loadedScenes = new List<string>();
    
    [Header("场景操作")]
    [SerializeField] private bool enableSceneAdditive = false;
    [SerializeField] private bool enableSceneUnload = false;
    [SerializeField] private bool enableSceneReload = false;
    [SerializeField] private float sceneTransitionTime = 1f;
    
    [Header("场景信息")]
    [SerializeField] private int activeSceneIndex = 0;
    [SerializeField] private string activeSceneName = "";
    [SerializeField] private bool isSceneLoaded = false;
    [SerializeField] private bool isSceneValid = false;
    
    // 场景管理事件
    private System.Action<string> onSceneLoaded;
    private System.Action<string> onSceneUnloaded;
    private System.Action<string> onSceneActivated;
    private System.Action<float> onLoadProgress;
    
    private void Start()
    {
        InitializeSceneManagement();
    }
    
    /// <summary>
    /// 初始化场景管理
    /// </summary>
    private void InitializeSceneManagement()
    {
        // 获取场景信息
        GetSceneInformation();
        
        // 设置场景管理事件
        SetupSceneManagementEvents();
        
        // 初始化场景列表
        InitializeSceneList();
        
        Debug.Log("场景管理系统初始化完成");
    }
    
    /// <summary>
    /// 获取场景信息
    /// </summary>
    private void GetSceneInformation()
    {
        totalScenes = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        activeSceneName = SceneManager.GetActiveScene().name;
        isSceneLoaded = SceneManager.GetActiveScene().isLoaded;
        isSceneValid = SceneManager.GetActiveScene().IsValid();
        
        Debug.Log($"当前场景: {currentSceneName} (索引: {currentSceneIndex})");
        Debug.Log($"总场景数: {totalScenes}");
    }
    
    /// <summary>
    /// 设置场景管理事件
    /// </summary>
    private void SetupSceneManagementEvents()
    {
        // 注册场景管理事件
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        
        Debug.Log("场景管理事件设置完成");
    }
    
    /// <summary>
    /// 初始化场景列表
    /// </summary>
    private void InitializeSceneList()
    {
        if (sceneNames == null || sceneNames.Length == 0)
        {
            // 获取所有场景名称
            sceneNames = new string[totalScenes];
            for (int i = 0; i < totalScenes; i++)
            {
                sceneNames[i] = GetSceneNameByBuildIndex(i);
            }
        }
        
        // 更新已加载场景列表
        UpdateLoadedScenesList();
        
        Debug.Log($"初始化了 {sceneNames.Length} 个场景");
    }
    
    /// <summary>
    /// 根据构建索引获取场景名称
    /// </summary>
    /// <param name="buildIndex">构建索引</param>
    /// <returns>场景名称</returns>
    private string GetSceneNameByBuildIndex(int buildIndex)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        return System.IO.Path.GetFileNameWithoutExtension(scenePath);
    }
    
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("场景名称不能为空");
            return;
        }
        
        if (isLoading)
        {
            Debug.LogWarning("正在加载场景，请稍后再试");
            return;
        }
        
        isLoading = true;
        loadProgress = 0f;
        
        Debug.Log($"开始加载场景: {sceneName}");
        
        // 异步加载场景
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    private System.Collections.IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, loadMode);
        
        if (asyncLoad == null)
        {
            Debug.LogError($"无法加载场景: {sceneName}");
            isLoading = false;
            yield break;
        }
        
        // 设置加载完成后的回调
        asyncLoad.completed += (op) =>
        {
            isLoading = false;
            loadProgress = 1f;
            Debug.Log($"场景加载完成: {sceneName}");
        };
        
        // 监控加载进度
        while (!asyncLoad.isDone)
        {
            loadProgress = asyncLoad.progress;
            onLoadProgress?.Invoke(loadProgress);
            yield return null;
        }
    }
    
    /// <summary>
    /// 加载场景（按索引）
    /// </summary>
    /// <param name="sceneIndex">场景索引</param>
    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= totalScenes)
        {
            Debug.LogError($"无效的场景索引: {sceneIndex}");
            return;
        }
        
        string sceneName = GetSceneNameByBuildIndex(sceneIndex);
        LoadScene(sceneName);
    }
    
    /// <summary>
    /// 加载下一个场景
    /// </summary>
    public void LoadNextScene()
    {
        int nextIndex = (currentSceneIndex + 1) % totalScenes;
        LoadScene(nextIndex);
    }
    
    /// <summary>
    /// 加载上一个场景
    /// </summary>
    public void LoadPreviousScene()
    {
        int prevIndex = (currentSceneIndex - 1 + totalScenes) % totalScenes;
        LoadScene(prevIndex);
    }
    
    /// <summary>
    /// 重新加载当前场景
    /// </summary>
    public void ReloadCurrentScene()
    {
        if (isLoading)
        {
            Debug.LogWarning("正在加载场景，请稍后再试");
            return;
        }
        
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log($"重新加载场景: {currentScene}");
        
        SceneManager.LoadScene(currentScene);
    }
    
    /// <summary>
    /// 添加场景（叠加模式）
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    public void AddScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("场景名称不能为空");
            return;
        }
        
        if (isLoading)
        {
            Debug.LogWarning("正在加载场景，请稍后再试");
            return;
        }
        
        isLoading = true;
        loadProgress = 0f;
        
        Debug.Log($"开始添加场景: {sceneName}");
        
        // 异步添加场景
        StartCoroutine(AddSceneAsync(sceneName));
    }
    
    /// <summary>
    /// 异步添加场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    private System.Collections.IEnumerator AddSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        if (asyncLoad == null)
        {
            Debug.LogError($"无法添加场景: {sceneName}");
            isLoading = false;
            yield break;
        }
        
        // 设置加载完成后的回调
        asyncLoad.completed += (op) =>
        {
            isLoading = false;
            loadProgress = 1f;
            UpdateLoadedScenesList();
            Debug.Log($"场景添加完成: {sceneName}");
        };
        
        // 监控加载进度
        while (!asyncLoad.isDone)
        {
            loadProgress = asyncLoad.progress;
            onLoadProgress?.Invoke(loadProgress);
            yield return null;
        }
    }
    
    /// <summary>
    /// 卸载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    public void UnloadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("场景名称不能为空");
            return;
        }
        
        if (isLoading)
        {
            Debug.LogWarning("正在加载场景，请稍后再试");
            return;
        }
        
        Debug.Log($"开始卸载场景: {sceneName}");
        
        // 异步卸载场景
        StartCoroutine(UnloadSceneAsync(sceneName));
    }
    
    /// <summary>
    /// 异步卸载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    private System.Collections.IEnumerator UnloadSceneAsync(string sceneName)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
        
        if (asyncUnload == null)
        {
            Debug.LogError($"无法卸载场景: {sceneName}");
            yield break;
        }
        
        // 监控卸载进度
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
        
        UpdateLoadedScenesList();
        Debug.Log($"场景卸载完成: {sceneName}");
    }
    
    /// <summary>
    /// 设置活动场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    public void SetActiveScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("场景名称不能为空");
            return;
        }
        
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (!scene.IsValid())
        {
            Debug.LogError($"场景不存在: {sceneName}");
            return;
        }
        
        if (!scene.isLoaded)
        {
            Debug.LogError($"场景未加载: {sceneName}");
            return;
        }
        
        SceneManager.SetActiveScene(scene);
        Debug.Log($"设置活动场景: {sceneName}");
    }
    
    /// <summary>
    /// 获取场景信息
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    /// <returns>场景信息</returns>
    public Scene GetSceneInfo(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName);
    }
    
    /// <summary>
    /// 检查场景是否已加载
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    /// <returns>是否已加载</returns>
    public bool IsSceneLoaded(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        return scene.IsValid() && scene.isLoaded;
    }
    
    /// <summary>
    /// 更新已加载场景列表
    /// </summary>
    private void UpdateLoadedScenesList()
    {
        loadedScenes.Clear();
        
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                loadedScenes.Add(scene.name);
            }
        }
    }
    
    /// <summary>
    /// 获取所有根游戏对象
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    /// <returns>根游戏对象数组</returns>
    public GameObject[] GetRootGameObjects(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid() && scene.isLoaded)
        {
            return scene.GetRootGameObjects();
        }
        
        return new GameObject[0];
    }
    
    /// <summary>
    /// 移动游戏对象到场景
    /// </summary>
    /// <param name="gameObject">游戏对象</param>
    /// <param name="sceneName">目标场景名称</param>
    public void MoveGameObjectToScene(GameObject gameObject, string sceneName)
    {
        if (gameObject == null)
        {
            Debug.LogError("游戏对象不能为空");
            return;
        }
        
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("场景名称不能为空");
            return;
        }
        
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (!scene.IsValid())
        {
            Debug.LogError($"场景不存在: {sceneName}");
            return;
        }
        
        SceneManager.MoveGameObjectToScene(gameObject, scene);
        Debug.Log($"移动游戏对象 {gameObject.name} 到场景 {sceneName}");
    }
    
    // 场景管理事件处理器
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"场景已加载: {scene.name} (模式: {mode})");
        onSceneLoaded?.Invoke(scene.name);
        
        // 更新状态
        currentSceneName = scene.name;
        currentSceneIndex = scene.buildIndex;
        UpdateLoadedScenesList();
    }
    
    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log($"场景已卸载: {scene.name}");
        onSceneUnloaded?.Invoke(scene.name);
        
        UpdateLoadedScenesList();
    }
    
    private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        Debug.Log($"活动场景已更改: {oldScene.name} -> {newScene.name}");
        onSceneActivated?.Invoke(newScene.name);
        
        // 更新状态
        activeSceneName = newScene.name;
        activeSceneIndex = newScene.buildIndex;
    }
    
    /// <summary>
    /// 获取场景管理信息
    /// </summary>
    public void GetSceneManagementInfo()
    {
        Debug.Log("=== 场景管理信息 ===");
        Debug.Log($"场景管理启用: {enableSceneManagement}");
        Debug.Log($"自动加载场景: {autoLoadScenes}");
        Debug.Log($"加载模式: {loadMode}");
        Debug.Log($"场景转换时间: {sceneTransitionTime}s");
        
        Debug.Log($"当前场景: {currentSceneName} (索引: {currentSceneIndex})");
        Debug.Log($"活动场景: {activeSceneName} (索引: {activeSceneIndex})");
        Debug.Log($"总场景数: {totalScenes}");
        Debug.Log($"已加载场景数: {loadedScenes.Count}");
        Debug.Log($"正在加载: {isLoading}");
        Debug.Log($"加载进度: {loadProgress:P1}");
        
        Debug.Log("场景列表:");
        for (int i = 0; i < sceneNames.Length; i++)
        {
            bool isLoaded = IsSceneLoaded(sceneNames[i]);
            bool isActive = sceneNames[i] == activeSceneName;
            string status = isActive ? "活动" : (isLoaded ? "已加载" : "未加载");
            Debug.Log($"  {i}: {sceneNames[i]} - {status}");
        }
        
        Debug.Log("已加载的场景:");
        foreach (string sceneName in loadedScenes)
        {
            Scene scene = GetSceneInfo(sceneName);
            Debug.Log($"  {sceneName} (根对象数: {scene.GetRootGameObjects().Length})");
        }
    }
    
    /// <summary>
    /// 重置场景管理设置
    /// </summary>
    public void ResetSceneManagementSettings()
    {
        // 重置设置
        enableSceneManagement = true;
        autoLoadScenes = false;
        loadMode = LoadSceneMode.Single;
        sceneTransitionTime = 1f;
        
        // 重置状态
        isLoading = false;
        loadProgress = 0f;
        
        Debug.Log("场景管理设置已重置");
    }
    
    private void Update()
    {
        // 更新场景状态
        if (SceneManager.GetActiveScene().IsValid())
        {
            isSceneLoaded = SceneManager.GetActiveScene().isLoaded;
            isSceneValid = SceneManager.GetActiveScene().IsValid();
        }
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("场景管理演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 场景状态
        GUILayout.Label($"当前场景: {currentSceneName}");
        GUILayout.Label($"活动场景: {activeSceneName}");
        GUILayout.Label($"总场景数: {totalScenes}");
        GUILayout.Label($"已加载场景数: {loadedScenes.Count}");
        GUILayout.Label($"正在加载: {isLoading}");
        GUILayout.Label($"加载进度: {loadProgress:P1}");
        
        GUILayout.Space(10);
        
        // 场景操作
        GUILayout.Label("场景操作:", EditorStyles.boldLabel);
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("上一个场景"))
        {
            LoadPreviousScene();
        }
        if (GUILayout.Button("下一个场景"))
        {
            LoadNextScene();
        }
        GUILayout.EndHorizontal();
        
        if (GUILayout.Button("重新加载当前场景"))
        {
            ReloadCurrentScene();
        }
        
        GUILayout.Space(10);
        
        // 场景选择
        GUILayout.Label("场景选择:", EditorStyles.boldLabel);
        
        for (int i = 0; i < sceneNames.Length; i++)
        {
            bool isLoaded = IsSceneLoaded(sceneNames[i]);
            bool isActive = sceneNames[i] == activeSceneName;
            string buttonText = $"{i}: {sceneNames[i]}";
            
            if (isActive)
            {
                buttonText += " [活动]";
            }
            else if (isLoaded)
            {
                buttonText += " [已加载]";
            }
            
            if (GUILayout.Button(buttonText))
            {
                if (isLoaded)
                {
                    SetActiveScene(sceneNames[i]);
                }
                else
                {
                    LoadScene(i);
                }
            }
        }
        
        GUILayout.Space(10);
        
        // 加载模式设置
        GUILayout.Label("加载模式:", EditorStyles.boldLabel);
        
        loadMode = (LoadSceneMode)EditorGUILayout.EnumPopup("加载模式", loadMode);
        
        if (GUILayout.Button("设置加载模式"))
        {
            Debug.Log($"加载模式已设置为: {loadMode}");
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取场景管理信息"))
        {
            GetSceneManagementInfo();
        }
        
        if (GUILayout.Button("重置设置"))
        {
            ResetSceneManagementSettings();
        }
        
        GUILayout.EndArea();
    }
    
    private void OnDestroy()
    {
        // 移除事件监听器
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }
} 