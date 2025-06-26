using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// UnityEngine.PlayerLoop 命名空间案例演示
/// 展示玩家循环系统、自定义更新循环、性能优化等核心功能
/// </summary>
public class PlayerLoopExample : MonoBehaviour
{
    [Header("玩家循环配置")]
    [SerializeField] private bool enablePlayerLoop = true; //启用玩家循环
    [SerializeField] private bool enableCustomUpdateLoop = true; //启用自定义更新循环
    [SerializeField] private bool enablePerformanceOptimization = true; //启用性能优化
    [SerializeField] private bool enableLoopProfiling = true; //启用循环分析
    [SerializeField] private bool enableLoopDebugging = true; //启用循环调试
    
    [Header("循环参数")]
    [SerializeField] private float targetFrameRate = 60f; //目标帧率
    [SerializeField] private float fixedTimeStep = 0.02f; //固定时间步长
    [SerializeField] private int maxAllowedTimestep = 4; //最大允许时间步长
    [SerializeField] private bool useFixedTimeStep = true; //使用固定时间步长
    [SerializeField] private bool enableVSync = false; //启用垂直同步
    
    [Header("自定义循环")]
    [SerializeField] private bool enableCustomFixedUpdate = true; //启用自定义固定更新
    [SerializeField] private bool enableCustomLateUpdate = true; //启用自定义延迟更新
    [SerializeField] private bool enableCustomPreRender = true; //启用自定义预渲染
    [SerializeField] private bool enableCustomPostRender = true; //启用自定义后渲染
    [SerializeField] private bool enableCustomPreCull = true; //启用自定义预剔除
    
    [Header("性能监控")]
    [SerializeField] private bool enableLoopMonitoring = true; //启用循环监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logLoopData = false; //记录循环数据
    [SerializeField] private int loopIterations = 0; //循环迭代次数
    [SerializeField] private float loopUpdateTime = 0f; //循环更新时间
    
    [Header("循环状态")]
    [SerializeField] private string currentLoopState = "未初始化"; //当前循环状态
    [SerializeField] private float currentFrameRate = 0f; //当前帧率
    [SerializeField] private float averageFrameRate = 0f; //平均帧率
    [SerializeField] private float minFrameRate = 0f; //最低帧率
    [SerializeField] private float maxFrameRate = 0f; //最高帧率
    [SerializeField] private float deltaTime = 0f; //增量时间
    [SerializeField] private float fixedDeltaTime = 0f; //固定增量时间
    
    [Header("性能数据")]
    [SerializeField] private float[] frameRateHistory = new float[100]; //帧率历史
    [SerializeField] private int frameRateIndex = 0; //帧率索引
    [SerializeField] private float[] updateTimeHistory = new float[100]; //更新时间历史
    [SerializeField] private int updateTimeIndex = 0; //更新时间索引
    
    private float lastMonitoringTime = 0f;
    private float frameRateSum = 0f;
    private int frameRateCount = 0;
    private bool isInitialized = false;
    private System.Collections.Generic.List<CustomUpdateSystem> customUpdateSystems = new System.Collections.Generic.List<CustomUpdateSystem>();

    private void Start()
    {
        InitializePlayerLoop();
    }

    /// <summary>
    /// 初始化玩家循环
    /// </summary>
    private void InitializePlayerLoop()
    {
        // 配置玩家循环参数
        ConfigurePlayerLoop();
        
        // 初始化自定义更新系统
        InitializeCustomUpdateSystems();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 注册自定义循环
        RegisterCustomLoops();
        
        isInitialized = true;
        currentLoopState = "已初始化";
        Debug.Log("玩家循环系统初始化完成");
    }

    /// <summary>
    /// 配置玩家循环参数
    /// </summary>
    private void ConfigurePlayerLoop()
    {
        // 设置目标帧率
        Application.targetFrameRate = (int)targetFrameRate;
        
        // 设置垂直同步
        QualitySettings.vSyncCount = enableVSync ? 1 : 0;
        
        // 设置固定时间步长
        Time.fixedDeltaTime = fixedTimeStep;
        
        // 设置最大允许时间步长
        Time.maximumDeltaTime = maxAllowedTimestep * fixedTimeStep;
        
        Debug.Log($"玩家循环配置: 目标帧率={targetFrameRate}, 固定时间步长={fixedTimeStep}, 垂直同步={enableVSync}");
    }

    /// <summary>
    /// 初始化自定义更新系统
    /// </summary>
    private void InitializeCustomUpdateSystems()
    {
        if (enableCustomUpdateLoop)
        {
            // 创建自定义更新系统
            customUpdateSystems.Add(new CustomUpdateSystem("Physics", 0.02f));
            customUpdateSystems.Add(new CustomUpdateSystem("AI", 0.1f));
            customUpdateSystems.Add(new CustomUpdateSystem("Audio", 0.05f));
            customUpdateSystems.Add(new CustomUpdateSystem("Network", 0.033f));
            
            Debug.Log($"自定义更新系统初始化完成: {customUpdateSystems.Count} 个系统");
        }
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        if (enableLoopMonitoring)
        {
            frameRateHistory = new float[100];
            updateTimeHistory = new float[100];
            frameRateIndex = 0;
            updateTimeIndex = 0;
            frameRateSum = 0f;
            frameRateCount = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    /// <summary>
    /// 注册自定义循环
    /// </summary>
    private void RegisterCustomLoops()
    {
        if (enableCustomUpdateLoop)
        {
            // 这里可以注册自定义的玩家循环阶段
            Debug.Log("自定义循环注册完成");
        }
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        float startTime = Time.realtimeSinceStartup;
        
        // 更新循环状态
        UpdateLoopState();
        
        // 执行自定义更新
        ExecuteCustomUpdates();
        
        // 更新性能数据
        UpdatePerformanceData(startTime);
        
        // 循环监控
        if (enableLoopMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            MonitorLoopPerformance();
            lastMonitoringTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (!isInitialized) return;
        
        // 固定更新逻辑
        if (enableCustomFixedUpdate)
        {
            ExecuteFixedUpdate();
        }
    }

    private void LateUpdate()
    {
        if (!isInitialized) return;
        
        // 延迟更新逻辑
        if (enableCustomLateUpdate)
        {
            ExecuteLateUpdate();
        }
    }

    /// <summary>
    /// 更新循环状态
    /// </summary>
    private void UpdateLoopState()
    {
        // 更新帧率
        currentFrameRate = 1f / Time.deltaTime;
        deltaTime = Time.deltaTime;
        fixedDeltaTime = Time.fixedDeltaTime;
        
        // 更新循环迭代次数
        loopIterations++;
        
        // 更新状态
        currentLoopState = $"运行中 (帧率: {currentFrameRate:F1} FPS)";
    }

    /// <summary>
    /// 执行自定义更新
    /// </summary>
    private void ExecuteCustomUpdates()
    {
        foreach (var system in customUpdateSystems)
        {
            if (system.ShouldUpdate())
            {
                system.Update();
            }
        }
    }

    /// <summary>
    /// 执行固定更新
    /// </summary>
    private void ExecuteFixedUpdate()
    {
        // 固定更新逻辑
        if (logLoopData)
        {
            Debug.Log($"固定更新: 时间={Time.fixedTime:F2}s, 步长={Time.fixedDeltaTime:F3}s");
        }
    }

    /// <summary>
    /// 执行延迟更新
    /// </summary>
    private void ExecuteLateUpdate()
    {
        // 延迟更新逻辑
        if (logLoopData)
        {
            Debug.Log($"延迟更新: 帧={Time.frameCount}");
        }
    }

    /// <summary>
    /// 更新性能数据
    /// </summary>
    private void UpdatePerformanceData(float startTime)
    {
        loopUpdateTime = Time.realtimeSinceStartup - startTime;
        
        // 更新帧率历史
        frameRateHistory[frameRateIndex] = currentFrameRate;
        frameRateIndex = (frameRateIndex + 1) % 100;
        
        // 更新更新时间历史
        updateTimeHistory[updateTimeIndex] = loopUpdateTime;
        updateTimeIndex = (updateTimeIndex + 1) % 100;
        
        // 计算帧率统计
        frameRateSum += currentFrameRate;
        frameRateCount++;
        
        if (frameRateCount > 0)
        {
            averageFrameRate = frameRateSum / frameRateCount;
        }
        
        // 更新最大最小帧率
        if (currentFrameRate > maxFrameRate) maxFrameRate = currentFrameRate;
        if (currentFrameRate < minFrameRate || minFrameRate == 0) minFrameRate = currentFrameRate;
    }

    /// <summary>
    /// 监控循环性能
    /// </summary>
    private void MonitorLoopPerformance()
    {
        if (logLoopData)
        {
            Debug.Log($"循环性能: 帧率={currentFrameRate:F1} FPS, 更新时间={loopUpdateTime * 1000:F2}ms, 迭代={loopIterations}");
        }
    }

    /// <summary>
    /// 设置目标帧率
    /// </summary>
    /// <param name="frameRate">目标帧率</param>
    public void SetTargetFrameRate(float frameRate)
    {
        targetFrameRate = Mathf.Max(1f, frameRate);
        Application.targetFrameRate = (int)targetFrameRate;
        Debug.Log($"目标帧率已设置为: {targetFrameRate} FPS");
    }

    /// <summary>
    /// 设置固定时间步长
    /// </summary>
    /// <param name="timeStep">时间步长</param>
    public void SetFixedTimeStep(float timeStep)
    {
        fixedTimeStep = Mathf.Max(0.001f, timeStep);
        Time.fixedDeltaTime = fixedTimeStep;
        Debug.Log($"固定时间步长已设置为: {fixedTimeStep}s");
    }

    /// <summary>
    /// 设置垂直同步
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetVSync(bool enabled)
    {
        enableVSync = enabled;
        QualitySettings.vSyncCount = enabled ? 1 : 0;
        Debug.Log($"垂直同步: {(enabled ? "启用" : "禁用")}");
    }

    /// <summary>
    /// 添加自定义更新系统
    /// </summary>
    /// <param name="name">系统名称</param>
    /// <param name="updateInterval">更新间隔</param>
    public void AddCustomUpdateSystem(string name, float updateInterval)
    {
        var system = new CustomUpdateSystem(name, updateInterval);
        customUpdateSystems.Add(system);
        Debug.Log($"自定义更新系统已添加: {name} (间隔: {updateInterval}s)");
    }

    /// <summary>
    /// 移除自定义更新系统
    /// </summary>
    /// <param name="name">系统名称</param>
    public void RemoveCustomUpdateSystem(string name)
    {
        for (int i = customUpdateSystems.Count - 1; i >= 0; i--)
        {
            if (customUpdateSystems[i].Name == name)
            {
                customUpdateSystems.RemoveAt(i);
                Debug.Log($"自定义更新系统已移除: {name}");
                break;
            }
        }
    }

    /// <summary>
    /// 优化玩家循环
    /// </summary>
    public void OptimizePlayerLoop()
    {
        Debug.Log("开始优化玩家循环...");
        
        // 调整目标帧率
        if (currentFrameRate < targetFrameRate * 0.8f)
        {
            float newTargetFrameRate = targetFrameRate * 0.8f;
            SetTargetFrameRate(newTargetFrameRate);
        }
        
        // 调整固定时间步长
        if (loopUpdateTime > 0.016f) // 如果更新时间超过16ms
        {
            float newTimeStep = fixedTimeStep * 1.2f;
            SetFixedTimeStep(newTimeStep);
        }
        
        // 优化自定义更新系统
        foreach (var system in customUpdateSystems)
        {
            if (system.UpdateTime > 0.01f) // 如果系统更新时间超过10ms
            {
                system.IncreaseUpdateInterval();
            }
        }
        
        Debug.Log("玩家循环优化完成");
    }

    /// <summary>
    /// 生成循环报告
    /// </summary>
    public void GenerateLoopReport()
    {
        Debug.Log("=== 玩家循环报告 ===");
        Debug.Log($"循环状态: {currentLoopState}");
        Debug.Log($"当前帧率: {currentFrameRate:F1} FPS");
        Debug.Log($"平均帧率: {averageFrameRate:F1} FPS");
        Debug.Log($"最低帧率: {minFrameRate:F1} FPS");
        Debug.Log($"最高帧率: {maxFrameRate:F1} FPS");
        Debug.Log($"目标帧率: {targetFrameRate} FPS");
        Debug.Log($"增量时间: {deltaTime * 1000:F2} ms");
        Debug.Log($"固定增量时间: {fixedDeltaTime * 1000:F2} ms");
        Debug.Log($"循环迭代次数: {loopIterations}");
        Debug.Log($"循环更新时间: {loopUpdateTime * 1000:F2} ms");
        Debug.Log($"自定义更新系统数: {customUpdateSystems.Count}");
        Debug.Log($"垂直同步: {(enableVSync ? "启用" : "禁用")}");
        Debug.Log($"使用固定时间步长: {(useFixedTimeStep ? "是" : "否")}");
    }

    /// <summary>
    /// 重置循环系统
    /// </summary>
    public void ResetLoopSystem()
    {
        Debug.Log("重置玩家循环系统...");
        
        // 重置性能数据
        frameRateSum = 0f;
        frameRateCount = 0;
        loopIterations = 0;
        minFrameRate = 0f;
        maxFrameRate = 0f;
        
        // 重置自定义更新系统
        customUpdateSystems.Clear();
        InitializeCustomUpdateSystems();
        
        // 重置状态
        currentLoopState = "已重置";
        
        Debug.Log("玩家循环系统重置完成");
    }

    /// <summary>
    /// 导出循环数据
    /// </summary>
    public void ExportLoopData()
    {
        var data = new PlayerLoopData
        {
            timestamp = System.DateTime.Now.ToString(),
            currentLoopState = currentLoopState,
            currentFrameRate = currentFrameRate,
            averageFrameRate = averageFrameRate,
            minFrameRate = minFrameRate,
            maxFrameRate = maxFrameRate,
            targetFrameRate = targetFrameRate,
            deltaTime = deltaTime,
            fixedDeltaTime = fixedDeltaTime,
            loopIterations = loopIterations,
            loopUpdateTime = loopUpdateTime,
            customUpdateSystemCount = customUpdateSystems.Count,
            enableVSync = enableVSync,
            useFixedTimeStep = useFixedTimeStep,
            frameRateHistory = frameRateHistory,
            updateTimeHistory = updateTimeHistory
        };
        
        string json = JsonUtility.ToJson(data, true);
        string filename = $"playerloop_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        
        System.IO.File.WriteAllText(filename, json);
        Debug.Log($"循环数据已导出: {filename}");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("PlayerLoop 玩家循环演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("循环配置:");
        enablePlayerLoop = GUILayout.Toggle(enablePlayerLoop, "启用玩家循环");
        enableCustomUpdateLoop = GUILayout.Toggle(enableCustomUpdateLoop, "启用自定义更新循环");
        enablePerformanceOptimization = GUILayout.Toggle(enablePerformanceOptimization, "启用性能优化");
        enableLoopProfiling = GUILayout.Toggle(enableLoopProfiling, "启用循环分析");
        enableLoopDebugging = GUILayout.Toggle(enableLoopDebugging, "启用循环调试");
        
        GUILayout.Space(10);
        GUILayout.Label("循环参数:");
        targetFrameRate = float.TryParse(GUILayout.TextField("目标帧率", targetFrameRate.ToString()), out var frameRate) ? frameRate : targetFrameRate;
        fixedTimeStep = float.TryParse(GUILayout.TextField("固定时间步长", fixedTimeStep.ToString()), out var timeStep) ? timeStep : fixedTimeStep;
        maxAllowedTimestep = int.TryParse(GUILayout.TextField("最大允许时间步长", maxAllowedTimestep.ToString()), out var maxTimeStep) ? maxTimeStep : maxAllowedTimestep;
        useFixedTimeStep = GUILayout.Toggle(useFixedTimeStep, "使用固定时间步长");
        enableVSync = GUILayout.Toggle(enableVSync, "启用垂直同步");
        
        GUILayout.Space(10);
        GUILayout.Label("自定义循环:");
        enableCustomFixedUpdate = GUILayout.Toggle(enableCustomFixedUpdate, "启用自定义固定更新");
        enableCustomLateUpdate = GUILayout.Toggle(enableCustomLateUpdate, "启用自定义延迟更新");
        enableCustomPreRender = GUILayout.Toggle(enableCustomPreRender, "启用自定义预渲染");
        enableCustomPostRender = GUILayout.Toggle(enableCustomPostRender, "启用自定义后渲染");
        enableCustomPreCull = GUILayout.Toggle(enableCustomPreCull, "启用自定义预剔除");
        
        GUILayout.Space(10);
        GUILayout.Label("循环状态:");
        GUILayout.Label($"状态: {currentLoopState}");
        GUILayout.Label($"当前帧率: {currentFrameRate:F1} FPS");
        GUILayout.Label($"平均帧率: {averageFrameRate:F1} FPS");
        GUILayout.Label($"最低帧率: {minFrameRate:F1} FPS");
        GUILayout.Label($"最高帧率: {maxFrameRate:F1} FPS");
        GUILayout.Label($"增量时间: {deltaTime * 1000:F2} ms");
        GUILayout.Label($"固定增量时间: {fixedDeltaTime * 1000:F2} ms");
        GUILayout.Label($"循环迭代次数: {loopIterations}");
        GUILayout.Label($"循环更新时间: {loopUpdateTime * 1000:F2} ms");
        GUILayout.Label($"自定义更新系统数: {customUpdateSystems.Count}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("设置目标帧率"))
        {
            SetTargetFrameRate(targetFrameRate);
        }
        
        if (GUILayout.Button("设置固定时间步长"))
        {
            SetFixedTimeStep(fixedTimeStep);
        }
        
        if (GUILayout.Button("设置垂直同步"))
        {
            SetVSync(enableVSync);
        }
        
        if (GUILayout.Button("添加自定义更新系统"))
        {
            AddCustomUpdateSystem("CustomSystem", 0.1f);
        }
        
        if (GUILayout.Button("优化玩家循环"))
        {
            OptimizePlayerLoop();
        }
        
        if (GUILayout.Button("生成循环报告"))
        {
            GenerateLoopReport();
        }
        
        if (GUILayout.Button("重置循环系统"))
        {
            ResetLoopSystem();
        }
        
        if (GUILayout.Button("导出循环数据"))
        {
            ExportLoopData();
        }
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 自定义更新系统
/// </summary>
[System.Serializable]
public class CustomUpdateSystem
{
    public string Name { get; private set; }
    public float UpdateInterval { get; private set; }
    public float LastUpdateTime { get; private set; }
    public float UpdateTime { get; private set; }
    public int UpdateCount { get; private set; }
    
    public CustomUpdateSystem(string name, float updateInterval)
    {
        Name = name;
        UpdateInterval = updateInterval;
        LastUpdateTime = 0f;
        UpdateTime = 0f;
        UpdateCount = 0;
    }
    
    public bool ShouldUpdate()
    {
        return Time.time - LastUpdateTime >= UpdateInterval;
    }
    
    public void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        
        // 执行更新逻辑
        ExecuteUpdate();
        
        UpdateTime = Time.realtimeSinceStartup - startTime;
        LastUpdateTime = Time.time;
        UpdateCount++;
    }
    
    private void ExecuteUpdate()
    {
        // 这里可以实现具体的更新逻辑
        // 例如：物理模拟、AI计算、音频处理等
    }
    
    public void IncreaseUpdateInterval()
    {
        UpdateInterval *= 1.2f;
    }
    
    public void DecreaseUpdateInterval()
    {
        UpdateInterval *= 0.8f;
    }
}

/// <summary>
/// 玩家循环数据类
/// </summary>
[System.Serializable]
public class PlayerLoopData
{
    public string timestamp;
    public string currentLoopState;
    public float currentFrameRate;
    public float averageFrameRate;
    public float minFrameRate;
    public float maxFrameRate;
    public float targetFrameRate;
    public float deltaTime;
    public float fixedDeltaTime;
    public int loopIterations;
    public float loopUpdateTime;
    public int customUpdateSystemCount;
    public bool enableVSync;
    public bool useFixedTimeStep;
    public float[] frameRateHistory;
    public float[] updateTimeHistory;
} 