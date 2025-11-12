using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// UnityEditor.Animations 命名空间案例演示
/// 展示动画编辑器的使用，包括动画控制器、状态机、混合树等
/// </summary>
public class AnimationsExample : MonoBehaviour
{
    [Header("动画系统配置")]
    [SerializeField] private bool enableAnimations = true; // 是否启用动画系统
    [SerializeField] private bool enableAnimationController = true; // 是否启用动画控制器
    [SerializeField] private bool enableStateMachine = true; // 是否启用状态机
    [SerializeField] private bool enableBlendTree = true; // 是否启用混合树
    [SerializeField] private bool enableAnimationEvents = true; // 是否启用动画事件
    [SerializeField] private bool enableAnimationCurves = true; // 是否启用动画曲线
    [SerializeField] private bool enableAnimationLayers = true; // 是否启用动画层
    [SerializeField] private bool enableAnimationMasks = true; // 是否启用动画遮罩
    [SerializeField] private bool enableAnimationOptimization = true; // 是否启用动画优化
    [SerializeField] private bool enableAnimationProfiling = true; // 是否启用动画分析
    
    [Header("动画控制器配置")]
    [SerializeField] private RuntimeAnimatorController animatorController; // 动画控制器
    [SerializeField] private AnimatorController controller; // 编辑器动画控制器
    [SerializeField] private string controllerPath = "Assets/Animations/"; // 控制器路径
    [SerializeField] private bool enableControllerAutoSave = true; // 是否启用控制器自动保存
    [SerializeField] private bool enableControllerValidation = true; // 是否启用控制器验证
    [SerializeField] private bool enableControllerOptimization = true; // 是否启用控制器优化
    [SerializeField] private bool enableControllerBackup = true; // 是否启用控制器备份
    [SerializeField] private string backupPath = "AnimationBackups/"; // 备份路径
    
    [Header("动画状态")]
    [SerializeField] private AnimationStatus animationStatus = AnimationStatus.Idle; // 动画状态
    [SerializeField] private bool isPlaying = false; // 是否正在播放
    [SerializeField] private bool isPaused = false; // 是否暂停
    [SerializeField] private bool isStopped = true; // 是否停止
    [SerializeField] private float playbackTime = 0f; // 播放时间
    [SerializeField] private float playbackSpeed = 1f; // 播放速度
    [SerializeField] private float normalizedTime = 0f; // 标准化时间
    [SerializeField] private int currentFrame = 0; // 当前帧
    [SerializeField] private int totalFrames = 0; // 总帧数
    [SerializeField] private float duration = 0f; // 持续时间
    [SerializeField] private bool isLooping = false; // 是否循环
    
    [Header("动画状态机")]
    [SerializeField] private StateMachineInfo stateMachineInfo = new StateMachineInfo(); // 状态机信息
    [SerializeField] private string currentState = ""; // 当前状态
    [SerializeField] private string previousState = ""; // 前一状态
    [SerializeField] private string[] availableStates = new string[0]; // 可用状态
    [SerializeField] private string[] stateNames = new string[0]; // 状态名称
    [SerializeField] private bool[] stateEnabled = new bool[0]; // 状态是否启用
    [SerializeField] private float[] stateWeights = new float[0]; // 状态权重
    [SerializeField] private float[] stateSpeeds = new float[0]; // 状态速度
    [SerializeField] private bool[] stateLooping = new bool[0]; // 状态是否循环
    [SerializeField] private int totalStates = 0; // 总状态数
    [SerializeField] private int activeStates = 0; // 活跃状态数
    
    [Header("动画参数")]
    [SerializeField] private AnimationParameterInfo[] parameters = new AnimationParameterInfo[0]; // 动画参数
    [SerializeField] private string[] parameterNames = new string[0]; // 参数名称
    [SerializeField] private AnimatorControllerParameterType[] parameterTypes = new AnimatorControllerParameterType[0]; // 参数类型
    [SerializeField] private object[] parameterValues = new object[0]; // 参数值
    [SerializeField] private bool[] parameterEnabled = new bool[0]; // 参数是否启用
    [SerializeField] private int totalParameters = 0; // 总参数数
    [SerializeField] private int activeParameters = 0; // 活跃参数数
    
    [Header("动画层")]
    [SerializeField] private AnimationLayerInfo[] layers = new AnimationLayerInfo[0]; // 动画层
    [SerializeField] private string[] layerNames = new string[0]; // 层名称
    [SerializeField] private float[] layerWeights = new float[0]; // 层权重
    [SerializeField] private bool[] layerEnabled = new bool[0]; // 层是否启用
    [SerializeField] private AvatarMask[] layerMasks = new AvatarMask[0]; // 层遮罩
    [SerializeField] private int totalLayers = 0; // 总层数
    [SerializeField] private int activeLayers = 0; // 活跃层数
    
    [Header("混合树")]
    [SerializeField] private BlendTreeInfo[] blendTrees = new BlendTreeInfo[0]; // 混合树
    [SerializeField] private string[] blendTreeNames = new string[0]; // 混合树名称
    [SerializeField] private BlendTreeType[] blendTreeTypes = new BlendTreeType[0]; // 混合树类型
    [SerializeField] private float[] blendTreeWeights = new float[0]; // 混合树权重
    [SerializeField] private bool[] blendTreeEnabled = new bool[0]; // 混合树是否启用
    [SerializeField] private int totalBlendTrees = 0; // 总混合树数
    [SerializeField] private int activeBlendTrees = 0; // 活跃混合树数
    
    [Header("动画事件")]
    [SerializeField] private AnimationEventInfo[] events = new AnimationEventInfo[0]; // 动画事件
    [SerializeField] private string[] eventNames = new string[0]; // 事件名称
    [SerializeField] private float[] eventTimes = new float[0]; // 事件时间
    [SerializeField] private string[] eventFunctions = new string[0]; // 事件函数
    [SerializeField] private object[] eventParameters = new object[0]; // 事件参数
    [SerializeField] private bool[] eventEnabled = new bool[0]; // 事件是否启用
    [SerializeField] private int totalEvents = 0; // 总事件数
    [SerializeField] private int activeEvents = 0; // 活跃事件数
    
    [Header("动画曲线")]
    [SerializeField] private AnimationCurveInfo[] curves = new AnimationCurveInfo[0]; // 动画曲线
    [SerializeField] private string[] curveNames = new string[0]; // 曲线名称
    [SerializeField] private AnimationCurve[] curveData = new AnimationCurve[0]; // 曲线数据
    [SerializeField] private string[] curveProperties = new string[0]; // 曲线属性
    [SerializeField] private bool[] curveEnabled = new bool[0]; // 曲线是否启用
    [SerializeField] private int totalCurves = 0; // 总曲线数
    [SerializeField] private int activeCurves = 0; // 活跃曲线数
    
    [Header("动画性能")]
    [SerializeField] private AnimationPerformanceInfo performanceInfo = new AnimationPerformanceInfo(); // 动画性能信息
    [SerializeField] private float animationFPS = 60f; // 动画帧率
    [SerializeField] private float animationMemory = 0f; // 动画内存使用
    [SerializeField] private float animationCPU = 0f; // 动画CPU使用
    [SerializeField] private int animationCalls = 0; // 动画调用次数
    [SerializeField] private int animationUpdates = 0; // 动画更新次数
    [SerializeField] private float averageUpdateTime = 0f; // 平均更新时间
    [SerializeField] private float maxUpdateTime = 0f; // 最大更新时间
    [SerializeField] private float minUpdateTime = float.MaxValue; // 最小更新时间
    
    [Header("动画统计")]
    [SerializeField] private AnimationStatistics statistics = new AnimationStatistics(); // 动画统计
    [SerializeField] private Dictionary<string, int> stateTransitionCounts = new Dictionary<string, int>(); // 状态转换计数
    [SerializeField] private Dictionary<string, float> statePlayTime = new Dictionary<string, float>(); // 状态播放时间
    [SerializeField] private Dictionary<string, int> parameterChangeCounts = new Dictionary<string, int>(); // 参数变更计数
    [SerializeField] private List<AnimationEvent> animationEvents = new List<AnimationEvent>(); // 动画事件列表
    [SerializeField] private List<AnimationWarning> animationWarnings = new List<AnimationWarning>(); // 动画警告列表
    [SerializeField] private List<AnimationError> animationErrors = new List<AnimationError>(); // 动画错误列表
    
    [Header("动画优化")]
    [SerializeField] private bool enableAnimationCulling = true; // 是否启用动画剔除
    [SerializeField] private bool enableAnimationLOD = true; // 是否启用动画LOD
    [SerializeField] private bool enableAnimationCompression = true; // 是否启用动画压缩
    [SerializeField] private bool enableAnimationStreaming = true; // 是否启用动画流式传输
    [SerializeField] private float cullingDistance = 100f; // 剔除距离
    [SerializeField] private int maxAnimationInstances = 100; // 最大动画实例数
    [SerializeField] private float compressionQuality = 0.8f; // 压缩质量
    [SerializeField] private bool enableOptimization = true; // 是否启用优化
    
    private bool isInitialized = false;
    private float lastUpdateTime = 0f;
    private float updateInterval = 0.016f; // 60 FPS
    private StringBuilder reportBuilder = new StringBuilder();
    private Animator animator;
    private List<AnimationClip> animationClips = new List<AnimationClip>();
    private List<AnimatorState> animatorStates = new List<AnimatorState>();
    private List<AnimatorTransition> animatorTransitions = new List<AnimatorTransition>();

    private void Start()
    {
        InitializeAnimations();
    }

    private void InitializeAnimations()
    {
        if (!enableAnimations) return;
        
        InitializeAnimationState();
        InitializeAnimationController();
        InitializeStateMachine();
        InitializeAnimationParameters();
        InitializeAnimationLayers();
        InitializeAnimationEvents();
        InitializeAnimationCurves();
        
        isInitialized = true;
        animationStatus = AnimationStatus.Idle;
        Debug.Log("动画系统初始化完成");
    }

    private void InitializeAnimationState()
    {
        animationStatus = AnimationStatus.Idle;
        isPlaying = false;
        isPaused = false;
        isStopped = true;
        playbackTime = 0f;
        playbackSpeed = 1f;
        normalizedTime = 0f;
        currentFrame = 0;
        totalFrames = 0;
        duration = 0f;
        isLooping = false;
        
        Debug.Log("动画状态已初始化");
    }

    private void InitializeAnimationController()
    {
        // 获取或创建Animator组件
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
        }
        
        // 创建动画控制器
        if (controller == null)
        {
            controller = AnimatorController.CreateAnimatorControllerAtPath($"{controllerPath}ExampleController.controller");
            animator.runtimeAnimatorController = controller;
        }
        
        Debug.Log("动画控制器已初始化");
    }

    private void InitializeStateMachine()
    {
        stateMachineInfo = new StateMachineInfo();
        currentState = "";
        previousState = "";
        availableStates = new string[0];
        stateNames = new string[0];
        stateEnabled = new bool[0];
        stateWeights = new float[0];
        stateSpeeds = new float[0];
        stateLooping = new bool[0];
        totalStates = 0;
        activeStates = 0;
        
        Debug.Log("状态机已初始化");
    }

    private void InitializeAnimationParameters()
    {
        parameters = new AnimationParameterInfo[0];
        parameterNames = new string[0];
        parameterTypes = new AnimatorControllerParameterType[0];
        parameterValues = new object[0];
        parameterEnabled = new bool[0];
        totalParameters = 0;
        activeParameters = 0;
        
        Debug.Log("动画参数已初始化");
    }

    private void InitializeAnimationLayers()
    {
        layers = new AnimationLayerInfo[0];
        layerNames = new string[0];
        layerWeights = new float[0];
        layerEnabled = new bool[0];
        layerMasks = new AvatarMask[0];
        totalLayers = 0;
        activeLayers = 0;
        
        Debug.Log("动画层已初始化");
    }

    private void InitializeAnimationEvents()
    {
        events = new AnimationEventInfo[0];
        eventNames = new string[0];
        eventTimes = new float[0];
        eventFunctions = new string[0];
        eventParameters = new object[0];
        eventEnabled = new bool[0];
        totalEvents = 0;
        activeEvents = 0;
        
        Debug.Log("动画事件已初始化");
    }

    private void InitializeAnimationCurves()
    {
        curves = new AnimationCurveInfo[0];
        curveNames = new string[0];
        curveData = new AnimationCurve[0];
        curveProperties = new string[0];
        curveEnabled = new bool[0];
        totalCurves = 0;
        activeCurves = 0;
        
        Debug.Log("动画曲线已初始化");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        UpdateAnimationStatus();
        UpdateAnimationState();
        UpdateAnimationPerformance();
        
        if (Time.time - lastUpdateTime > updateInterval)
        {
            UpdateAnimationData();
            lastUpdateTime = Time.time;
        }
        
        if (enableAnimationProfiling)
        {
            UpdateAnimationProfiling();
        }
    }

    private void UpdateAnimationStatus()
    {
        if (animator != null)
        {
            if (animator.isActiveAndEnabled)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !isLooping)
                {
                    animationStatus = AnimationStatus.Completed;
                    isPlaying = false;
                    isStopped = true;
                }
                else if (isPlaying)
                {
                    animationStatus = AnimationStatus.Playing;
                }
                else if (isPaused)
                {
                    animationStatus = AnimationStatus.Paused;
                }
                else
                {
                    animationStatus = AnimationStatus.Idle;
                }
            }
            else
            {
                animationStatus = AnimationStatus.Disabled;
            }
        }
    }

    private void UpdateAnimationState()
    {
        if (animator != null && animator.isActiveAndEnabled)
        {
            var currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            currentState = currentStateInfo.IsName("") ? "Default" : currentStateInfo.fullPathHash.ToString();
            normalizedTime = currentStateInfo.normalizedTime;
            
            if (currentState != previousState)
            {
                // 状态转换
                if (!string.IsNullOrEmpty(previousState))
                {
                    if (!stateTransitionCounts.ContainsKey(previousState))
                    {
                        stateTransitionCounts[previousState] = 0;
                    }
                    stateTransitionCounts[previousState]++;
                }
                
                previousState = currentState;
            }
            
            // 更新状态播放时间
            if (!statePlayTime.ContainsKey(currentState))
            {
                statePlayTime[currentState] = 0f;
            }
            statePlayTime[currentState] += Time.deltaTime;
        }
    }

    private void UpdateAnimationPerformance()
    {
        if (enableAnimationProfiling)
        {
            var startTime = Time.realtimeSinceStartup;
            
            // 模拟动画更新
            animationUpdates++;
            animationCalls++;
            
            var updateTime = Time.realtimeSinceStartup - startTime;
            averageUpdateTime = (averageUpdateTime * (animationUpdates - 1) + updateTime) / animationUpdates;
            
            if (updateTime > maxUpdateTime)
            {
                maxUpdateTime = updateTime;
            }
            
            if (updateTime < minUpdateTime)
            {
                minUpdateTime = updateTime;
            }
            
            // 更新性能信息
            performanceInfo.fps = animationFPS;
            performanceInfo.memoryUsage = animationMemory;
            performanceInfo.cpuUsage = animationCPU;
            performanceInfo.updateCalls = animationCalls;
            performanceInfo.updateCount = animationUpdates;
            performanceInfo.averageUpdateTime = averageUpdateTime;
            performanceInfo.maxUpdateTime = maxUpdateTime;
            performanceInfo.minUpdateTime = minUpdateTime;
        }
    }

    private void UpdateAnimationData()
    {
        // 更新动画数据
        UpdateStateMachineData();
        UpdateParameterData();
        UpdateLayerData();
        UpdateBlendTreeData();
        UpdateEventData();
        UpdateCurveData();
    }

    private void UpdateStateMachineData()
    {
        if (controller != null)
        {
            var stateMachine = controller.layers[0].stateMachine;
            var states = stateMachine.states;
            
            stateNames = new string[states.Length];
            stateEnabled = new bool[states.Length];
            stateWeights = new float[states.Length];
            stateSpeeds = new float[states.Length];
            stateLooping = new bool[states.Length];
            
            for (int i = 0; i < states.Length; i++)
            {
                var state = states[i].state;
                stateNames[i] = state.name;
                stateEnabled[i] = state.enabled;
                stateWeights[i] = 1f;
                stateSpeeds[i] = state.speed;
                stateLooping[i] = state.motion != null && state.motion.isLooping;
            }
            
            totalStates = states.Length;
            activeStates = states.Length;
            
            // 更新状态机信息
            stateMachineInfo.totalStates = totalStates;
            stateMachineInfo.activeStates = activeStates;
            stateMachineInfo.currentState = currentState;
            stateMachineInfo.previousState = previousState;
        }
    }

    private void UpdateParameterData()
    {
        if (controller != null)
        {
            var controllerParameters = controller.parameters;
            
            parameterNames = new string[controllerParameters.Length];
            parameterTypes = new AnimatorControllerParameterType[controllerParameters.Length];
            parameterValues = new object[controllerParameters.Length];
            parameterEnabled = new bool[controllerParameters.Length];
            
            for (int i = 0; i < controllerParameters.Length; i++)
            {
                var param = controllerParameters[i];
                parameterNames[i] = param.name;
                parameterTypes[i] = param.type;
                parameterEnabled[i] = true;
                
                // 获取参数值
                switch (param.type)
                {
                    case AnimatorControllerParameterType.Float:
                        parameterValues[i] = animator.GetFloat(param.name);
                        break;
                    case AnimatorControllerParameterType.Int:
                        parameterValues[i] = animator.GetInteger(param.name);
                        break;
                    case AnimatorControllerParameterType.Bool:
                        parameterValues[i] = animator.GetBool(param.name);
                        break;
                    case AnimatorControllerParameterType.Trigger:
                        parameterValues[i] = false;
                        break;
                }
            }
            
            totalParameters = controllerParameters.Length;
            activeParameters = controllerParameters.Length;
        }
    }

    private void UpdateLayerData()
    {
        if (controller != null)
        {
            var controllerLayers = controller.layers;
            
            layerNames = new string[controllerLayers.Length];
            layerWeights = new float[controllerLayers.Length];
            layerEnabled = new bool[controllerLayers.Length];
            layerMasks = new AvatarMask[controllerLayers.Length];
            
            for (int i = 0; i < controllerLayers.Length; i++)
            {
                var layer = controllerLayers[i];
                layerNames[i] = layer.name;
                layerWeights[i] = layer.defaultWeight;
                layerEnabled[i] = layer.enabled;
                layerMasks[i] = layer.avatarMask;
            }
            
            totalLayers = controllerLayers.Length;
            activeLayers = controllerLayers.Length;
        }
    }

    private void UpdateBlendTreeData()
    {
        // 模拟混合树数据更新
        blendTrees = new BlendTreeInfo[0];
        blendTreeNames = new string[0];
        blendTreeTypes = new BlendTreeType[0];
        blendTreeWeights = new float[0];
        blendTreeEnabled = new bool[0];
        totalBlendTrees = 0;
        activeBlendTrees = 0;
    }

    private void UpdateEventData()
    {
        // 模拟事件数据更新
        events = new AnimationEventInfo[0];
        eventNames = new string[0];
        eventTimes = new float[0];
        eventFunctions = new string[0];
        eventParameters = new object[0];
        eventEnabled = new bool[0];
        totalEvents = 0;
        activeEvents = 0;
    }

    private void UpdateCurveData()
    {
        // 模拟曲线数据更新
        curves = new AnimationCurveInfo[0];
        curveNames = new string[0];
        curveData = new AnimationCurve[0];
        curveProperties = new string[0];
        curveEnabled = new bool[0];
        totalCurves = 0;
        activeCurves = 0;
    }

    private void UpdateAnimationProfiling()
    {
        // 更新动画分析数据
        animationFPS = 1f / averageUpdateTime;
        animationMemory = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong() / (1024f * 1024f);
        animationCPU = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong() / (1024f * 1024f);
    }

    public void PlayAnimation()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator组件不存在");
            return;
        }
        
        isPlaying = true;
        isPaused = false;
        isStopped = false;
        animationStatus = AnimationStatus.Playing;
        
        Debug.Log("动画开始播放");
    }

    public void PauseAnimation()
    {
        if (!isPlaying)
        {
            Debug.LogWarning("动画未在播放");
            return;
        }
        
        isPlaying = false;
        isPaused = true;
        isStopped = false;
        animationStatus = AnimationStatus.Paused;
        
        Debug.Log("动画已暂停");
    }

    public void StopAnimation()
    {
        isPlaying = false;
        isPaused = false;
        isStopped = true;
        animationStatus = AnimationStatus.Idle;
        playbackTime = 0f;
        normalizedTime = 0f;
        currentFrame = 0;
        
        Debug.Log("动画已停止");
    }

    public void SetPlaybackSpeed(float speed)
    {
        if (animator != null)
        {
            animator.speed = speed;
            playbackSpeed = speed;
            Debug.Log($"播放速度设置为: {speed}");
        }
    }

    public void SetParameter(string parameterName, object value)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator组件不存在");
            return;
        }
        
        try
        {
            if (value is float floatValue)
            {
                animator.SetFloat(parameterName, floatValue);
            }
            else if (value is int intValue)
            {
                animator.SetInteger(parameterName, intValue);
            }
            else if (value is bool boolValue)
            {
                animator.SetBool(parameterName, boolValue);
            }
            else if (value is string stringValue && stringValue == "trigger")
            {
                animator.SetTrigger(parameterName);
            }
            
            // 更新参数变更计数
            if (!parameterChangeCounts.ContainsKey(parameterName))
            {
                parameterChangeCounts[parameterName] = 0;
            }
            parameterChangeCounts[parameterName]++;
            
            Debug.Log($"参数 {parameterName} 设置为: {value}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"设置参数失败: {e.Message}");
        }
    }

    public void AddState(string stateName, AnimationClip clip)
    {
        if (controller == null)
        {
            Debug.LogWarning("动画控制器不存在");
            return;
        }
        
        try
        {
            var stateMachine = controller.layers[0].stateMachine;
            var state = stateMachine.AddState(stateName);
            state.motion = clip;
            
            Debug.Log($"状态 {stateName} 已添加");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"添加状态失败: {e.Message}");
        }
    }

    public void AddParameter(string parameterName, AnimatorControllerParameterType type)
    {
        if (controller == null)
        {
            Debug.LogWarning("动画控制器不存在");
            return;
        }
        
        try
        {
            var parameter = new AnimatorControllerParameter();
            parameter.name = parameterName;
            parameter.type = type;
            
            var parameters = new List<AnimatorControllerParameter>(controller.parameters);
            parameters.Add(parameter);
            controller.parameters = parameters.ToArray();
            
            Debug.Log($"参数 {parameterName} 已添加");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"添加参数失败: {e.Message}");
        }
    }

    public void AddLayer(string layerName, float weight = 1f)
    {
        if (controller == null)
        {
            Debug.LogWarning("动画控制器不存在");
            return;
        }
        
        try
        {
            var layer = new AnimatorControllerLayer();
            layer.name = layerName;
            layer.defaultWeight = weight;
            layer.enabled = true;
            
            var layers = new List<AnimatorControllerLayer>(controller.layers);
            layers.Add(layer);
            controller.layers = layers.ToArray();
            
            Debug.Log($"层 {layerName} 已添加");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"添加层失败: {e.Message}");
        }
    }

    public void CreateAnimationEvent(string eventName, float time, string functionName)
    {
        if (animationClips.Count == 0)
        {
            Debug.LogWarning("没有可用的动画片段");
            return;
        }
        
        try
        {
            var clip = animationClips[0];
            var animationEvent = new AnimationEvent();
            animationEvent.functionName = functionName;
            animationEvent.time = time;
            
            var events = new List<AnimationEvent>(clip.events);
            events.Add(animationEvent);
            clip.events = events.ToArray();
            
            Debug.Log($"动画事件 {eventName} 已创建");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"创建动画事件失败: {e.Message}");
        }
    }

    public void GenerateAnimationReport()
    {
        reportBuilder.Clear();
        reportBuilder.AppendLine("=== 动画系统报告 ===");
        reportBuilder.AppendLine($"生成时间: {System.DateTime.Now}");
        reportBuilder.AppendLine($"动画状态: {animationStatus}");
        reportBuilder.AppendLine($"是否正在播放: {isPlaying}");
        reportBuilder.AppendLine($"是否暂停: {isPaused}");
        reportBuilder.AppendLine($"是否停止: {isStopped}");
        reportBuilder.AppendLine($"播放时间: {playbackTime:F2}秒");
        reportBuilder.AppendLine($"播放速度: {playbackSpeed}");
        reportBuilder.AppendLine($"标准化时间: {normalizedTime:F2}");
        reportBuilder.AppendLine($"当前帧: {currentFrame}");
        reportBuilder.AppendLine($"总帧数: {totalFrames}");
        reportBuilder.AppendLine($"持续时间: {duration:F2}秒");
        reportBuilder.AppendLine($"是否循环: {isLooping}");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 状态机信息 ===");
        reportBuilder.AppendLine($"总状态数: {totalStates}");
        reportBuilder.AppendLine($"活跃状态数: {activeStates}");
        reportBuilder.AppendLine($"当前状态: {currentState}");
        reportBuilder.AppendLine($"前一状态: {previousState}");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 动画参数 ===");
        reportBuilder.AppendLine($"总参数数: {totalParameters}");
        reportBuilder.AppendLine($"活跃参数数: {activeParameters}");
        for (int i = 0; i < parameterNames.Length; i++)
        {
            reportBuilder.AppendLine($"- {parameterNames[i]} ({parameterTypes[i]}): {parameterValues[i]}");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 动画层 ===");
        reportBuilder.AppendLine($"总层数: {totalLayers}");
        reportBuilder.AppendLine($"活跃层数: {activeLayers}");
        for (int i = 0; i < layerNames.Length; i++)
        {
            reportBuilder.AppendLine($"- {layerNames[i]}: 权重={layerWeights[i]}, 启用={layerEnabled[i]}");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 动画性能 ===");
        reportBuilder.AppendLine($"动画帧率: {animationFPS:F1} FPS");
        reportBuilder.AppendLine($"动画内存使用: {animationMemory:F2} MB");
        reportBuilder.AppendLine($"动画CPU使用: {animationCPU:F2} MB");
        reportBuilder.AppendLine($"动画调用次数: {animationCalls}");
        reportBuilder.AppendLine($"动画更新次数: {animationUpdates}");
        reportBuilder.AppendLine($"平均更新时间: {averageUpdateTime * 1000:F2} ms");
        reportBuilder.AppendLine($"最大更新时间: {maxUpdateTime * 1000:F2} ms");
        reportBuilder.AppendLine($"最小更新时间: {minUpdateTime * 1000:F2} ms");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 状态转换统计 ===");
        foreach (var kvp in stateTransitionCounts)
        {
            reportBuilder.AppendLine($"- {kvp.Key}: {kvp.Value} 次");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 状态播放时间 ===");
        foreach (var kvp in statePlayTime)
        {
            reportBuilder.AppendLine($"- {kvp.Key}: {kvp.Value:F2} 秒");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 参数变更统计 ===");
        foreach (var kvp in parameterChangeCounts)
        {
            reportBuilder.AppendLine($"- {kvp.Key}: {kvp.Value} 次");
        }
        
        string report = reportBuilder.ToString();
        Debug.Log(report);
        
        if (enableControllerBackup)
        {
            ExportReport(report);
        }
    }

    private void ExportReport(string report)
    {
        try
        {
            string fileName = $"AnimationReport_{System.DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = System.IO.Path.Combine(backupPath, fileName);
            
            System.IO.Directory.CreateDirectory(backupPath);
            System.IO.File.WriteAllText(filePath, report);
            
            Debug.Log($"动画报告已导出: {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"导出动画报告失败: {e.Message}");
        }
    }

    public void OpenAnimationWindow()
    {
        if (enableAnimations)
        {
            EditorWindow.GetWindow<UnityEditor.AnimationWindow>();
            Debug.Log("动画窗口已打开");
        }
    }

    public void OpenAnimatorWindow()
    {
        if (enableAnimationController)
        {
            EditorWindow.GetWindow<UnityEditor.AnimatorWindow>();
            Debug.Log("动画器窗口已打开");
        }
    }

    public void ResetAnimationData()
    {
        InitializeAnimationState();
        InitializeStateMachine();
        InitializeAnimationParameters();
        InitializeAnimationLayers();
        InitializeAnimationEvents();
        InitializeAnimationCurves();
        
        stateTransitionCounts.Clear();
        statePlayTime.Clear();
        parameterChangeCounts.Clear();
        animationEvents.Clear();
        animationWarnings.Clear();
        animationErrors.Clear();
        
        Debug.Log("动画数据已重置");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Animations 动画系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("动画系统配置:");
        enableAnimations = GUILayout.Toggle(enableAnimations, "启用动画系统");
        enableAnimationController = GUILayout.Toggle(enableAnimationController, "启用动画控制器");
        enableStateMachine = GUILayout.Toggle(enableStateMachine, "启用状态机");
        enableBlendTree = GUILayout.Toggle(enableBlendTree, "启用混合树");
        enableAnimationProfiling = GUILayout.Toggle(enableAnimationProfiling, "启用动画分析");
        
        GUILayout.Space(10);
        GUILayout.Label("动画状态:");
        GUILayout.Label($"动画状态: {animationStatus}");
        GUILayout.Label($"是否正在播放: {isPlaying}");
        GUILayout.Label($"是否暂停: {isPaused}");
        GUILayout.Label($"是否停止: {isStopped}");
        GUILayout.Label($"播放时间: {playbackTime:F2}秒");
        GUILayout.Label($"播放速度: {playbackSpeed}");
        GUILayout.Label($"标准化时间: {normalizedTime:F2}");
        GUILayout.Label($"当前帧: {currentFrame}");
        GUILayout.Label($"总帧数: {totalFrames}");
        GUILayout.Label($"持续时间: {duration:F2}秒");
        
        GUILayout.Space(10);
        GUILayout.Label("状态机信息:");
        GUILayout.Label($"总状态数: {totalStates}");
        GUILayout.Label($"活跃状态数: {activeStates}");
        GUILayout.Label($"当前状态: {currentState}");
        GUILayout.Label($"前一状态: {previousState}");
        
        GUILayout.Space(10);
        GUILayout.Label("动画参数:");
        GUILayout.Label($"总参数数: {totalParameters}");
        GUILayout.Label($"活跃参数数: {activeParameters}");
        
        GUILayout.Space(10);
        GUILayout.Label("动画层:");
        GUILayout.Label($"总层数: {totalLayers}");
        GUILayout.Label($"活跃层数: {activeLayers}");
        
        GUILayout.Space(10);
        GUILayout.Label("动画性能:");
        GUILayout.Label($"动画帧率: {animationFPS:F1} FPS");
        GUILayout.Label($"动画内存使用: {animationMemory:F2} MB");
        GUILayout.Label($"动画CPU使用: {animationCPU:F2} MB");
        GUILayout.Label($"动画调用次数: {animationCalls}");
        GUILayout.Label($"动画更新次数: {animationUpdates}");
        GUILayout.Label($"平均更新时间: {averageUpdateTime * 1000:F2} ms");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("播放动画"))
        {
            PlayAnimation();
        }
        
        if (GUILayout.Button("暂停动画"))
        {
            PauseAnimation();
        }
        
        if (GUILayout.Button("停止动画"))
        {
            StopAnimation();
        }
        
        if (GUILayout.Button("设置播放速度"))
        {
            SetPlaybackSpeed(2f);
        }
        
        if (GUILayout.Button("设置参数"))
        {
            SetParameter("Speed", 1.5f);
        }
        
        if (GUILayout.Button("添加状态"))
        {
            AddState("NewState", null);
        }
        
        if (GUILayout.Button("添加参数"))
        {
            AddParameter("NewParameter", AnimatorControllerParameterType.Float);
        }
        
        if (GUILayout.Button("添加层"))
        {
            AddLayer("NewLayer", 0.5f);
        }
        
        if (GUILayout.Button("创建动画事件"))
        {
            CreateAnimationEvent("TestEvent", 0.5f, "OnAnimationEvent");
        }
        
        if (GUILayout.Button("生成动画报告"))
        {
            GenerateAnimationReport();
        }
        
        if (GUILayout.Button("打开动画窗口"))
        {
            OpenAnimationWindow();
        }
        
        if (GUILayout.Button("打开动画器窗口"))
        {
            OpenAnimatorWindow();
        }
        
        if (GUILayout.Button("重置动画数据"))
        {
            ResetAnimationData();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("状态转换统计:");
        foreach (var kvp in stateTransitionCounts)
        {
            GUILayout.Label($"{kvp.Key}: {kvp.Value} 次");
        }
        
        GUILayout.EndArea();
    }
}

public enum AnimationStatus
{
    Idle,
    Playing,
    Paused,
    Stopped,
    Completed,
    Disabled
}

public enum BlendTreeType
{
    BlendType,
    BlendType1D,
    BlendType2D,
    BlendTypeDirect
}

public enum AnimationEventType
{
    Function,
    Audio,
    Animation,
    Custom
}

public enum AnimationEventSeverity
{
    Info,
    Warning,
    Error
}

[System.Serializable]
public class StateMachineInfo
{
    public int totalStates;
    public int activeStates;
    public string currentState;
    public string previousState;
    public System.DateTime lastUpdateTime;
}

[System.Serializable]
public class AnimationParameterInfo
{
    public string name;
    public AnimatorControllerParameterType type;
    public object value;
    public bool enabled;
    public System.DateTime lastModified;
}

[System.Serializable]
public class AnimationLayerInfo
{
    public string name;
    public float weight;
    public bool enabled;
    public AvatarMask mask;
    public System.DateTime lastModified;
}

[System.Serializable]
public class BlendTreeInfo
{
    public string name;
    public BlendTreeType type;
    public float weight;
    public bool enabled;
    public System.DateTime lastModified;
}

[System.Serializable]
public class AnimationEventInfo
{
    public string name;
    public float time;
    public string function;
    public object parameter;
    public bool enabled;
    public System.DateTime lastModified;
}

[System.Serializable]
public class AnimationCurveInfo
{
    public string name;
    public AnimationCurve curve;
    public string property;
    public bool enabled;
    public System.DateTime lastModified;
}

[System.Serializable]
public class AnimationPerformanceInfo
{
    public float fps;
    public float memoryUsage;
    public float cpuUsage;
    public int updateCalls;
    public int updateCount;
    public float averageUpdateTime;
    public float maxUpdateTime;
    public float minUpdateTime;
}

[System.Serializable]
public class AnimationStatistics
{
    public int totalStates;
    public int totalParameters;
    public int totalLayers;
    public int totalEvents;
    public int totalCurves;
    public float totalPlayTime;
    public int totalTransitions;
    public System.DateTime firstPlayTime;
    public System.DateTime lastPlayTime;
}

[System.Serializable]
public class AnimationEvent
{
    public string name;
    public float time;
    public string function;
    public object parameter;
    public System.DateTime timestamp;
}

[System.Serializable]
public class AnimationWarning
{
    public string message;
    public System.DateTime timestamp;
    public AnimationEventSeverity severity;
}

[System.Serializable]
public class AnimationError
{
    public string message;
    public System.DateTime timestamp;
    public AnimationEventSeverity severity;
} 