using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Animations;

/// <summary>
/// UnityEngine.Playables 命名空间案例演示
/// 展示可播放系统、动画混合、时间轴控制等核心功能
/// </summary>
public class PlayablesExample : MonoBehaviour
{
    [Header("可播放系统配置")]
    [SerializeField] private bool enablePlayables = true; //启用可播放系统
    [SerializeField] private bool enableAnimationBlending = true; //启用动画混合
    [SerializeField] private bool enableTimelineControl = true; //启用时间轴控制
    [SerializeField] private bool enableCustomPlayables = true; //启用自定义可播放
    [SerializeField] private bool enablePerformanceMonitoring = true; //启用性能监控
    
    [Header("动画混合")]
    [SerializeField] private AnimationClip[] animationClips; //动画片段
    [SerializeField] private float blendWeight = 0.5f; //混合权重
    [SerializeField] private float blendSpeed = 1f; //混合速度
    [SerializeField] private bool enableCrossFade = true; //启用交叉淡入淡出
    [SerializeField] private float crossFadeDuration = 0.3f; //交叉淡入淡出持续时间
    
    [Header("时间轴控制")]
    [SerializeField] private PlayableDirector playableDirector; //可播放导演
    [SerializeField] private TimelineAsset timelineAsset; //时间轴资源
    [SerializeField] private double currentTime = 0.0; //当前时间
    [SerializeField] private double duration = 10.0; //持续时间
    [SerializeField] private bool isPlaying = false; //是否播放
    [SerializeField] private bool isLooping = false; //是否循环
    
    [Header("自定义可播放")]
    [SerializeField] private bool enableCustomAnimation = true; //启用自定义动画
    [SerializeField] private bool enableCustomAudio = true; //启用自定义音频
    [SerializeField] private bool enableCustomVideo = true; //启用自定义视频
    [SerializeField] private bool enableCustomScript = true; //启用自定义脚本
    
    [Header("性能监控")]
    [SerializeField] private bool enablePlayableMonitoring = true; //启用可播放监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logPlayableData = false; //记录可播放数据
    [SerializeField] private int activePlayables = 0; //活跃可播放数
    [SerializeField] private float playableUpdateTime = 0f; //可播放更新时间
    
    [Header("系统状态")]
    [SerializeField] private string currentPlayableState = "未初始化"; //当前可播放状态
    [SerializeField] private string currentAnimationState = "空闲"; //当前动画状态
    [SerializeField] private float animationProgress = 0f; //动画进度
    [SerializeField] private float audioProgress = 0f; //音频进度
    [SerializeField] private float videoProgress = 0f; //视频进度
    
    private PlayableGraph playableGraph;
    private AnimationMixerPlayable animationMixer;
    private AudioMixerPlayable audioMixer;
    private VideoMixerPlayable videoMixer;
    private ScriptPlayable<CustomPlayableBehaviour> scriptPlayable;
    private Animator animator;
    private AudioSource audioSource;
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;

    private void Start()
    {
        InitializePlayablesSystem();
    }

    /// <summary>
    /// 初始化可播放系统
    /// </summary>
    private void InitializePlayablesSystem()
    {
        // 获取组件
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
        }
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // 创建可播放图
        CreatePlayableGraph();
        
        // 初始化动画混合
        if (enableAnimationBlending)
        {
            InitializeAnimationBlending();
        }
        
        // 初始化时间轴控制
        if (enableTimelineControl)
        {
            InitializeTimelineControl();
        }
        
        // 初始化自定义可播放
        if (enableCustomPlayables)
        {
            InitializeCustomPlayables();
        }
        
        isInitialized = true;
        currentPlayableState = "已初始化";
        Debug.Log("可播放系统初始化完成");
    }

    /// <summary>
    /// 创建可播放图
    /// </summary>
    private void CreatePlayableGraph()
    {
        // 创建可播放图
        playableGraph = PlayableGraph.Create("PlayablesExample");
        
        // 设置输出
        var output = AnimationPlayableOutput.Create(playableGraph, "Animation", animator);
        
        // 创建动画混合器
        animationMixer = AnimationMixerPlayable.Create(playableGraph, 2);
        output.SetSourcePlayable(animationMixer);
        
        Debug.Log("可播放图创建完成");
    }

    /// <summary>
    /// 初始化动画混合
    /// </summary>
    private void InitializeAnimationBlending()
    {
        if (animationClips == null || animationClips.Length == 0)
        {
            Debug.LogWarning("没有可用的动画片段");
            return;
        }
        
        // 创建动画片段可播放
        for (int i = 0; i < Mathf.Min(animationClips.Length, 2); i++)
        {
            if (animationClips[i] != null)
            {
                var clipPlayable = AnimationClipPlayable.Create(playableGraph, animationClips[i]);
                animationMixer.ConnectInput(i, clipPlayable, 0);
                animationMixer.SetInputWeight(i, i == 0 ? 1.0f : 0.0f);
            }
        }
        
        currentAnimationState = "动画混合已初始化";
        Debug.Log("动画混合初始化完成");
    }

    /// <summary>
    /// 初始化时间轴控制
    /// </summary>
    private void InitializeTimelineControl()
    {
        // 获取或创建PlayableDirector
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
            if (playableDirector == null)
            {
                playableDirector = gameObject.AddComponent<PlayableDirector>();
            }
        }
        
        // 设置时间轴资源
        if (timelineAsset != null)
        {
            playableDirector.playableAsset = timelineAsset;
            duration = timelineAsset.duration;
        }
        
        Debug.Log("时间轴控制初始化完成");
    }

    /// <summary>
    /// 初始化自定义可播放
    /// </summary>
    private void InitializeCustomPlayables()
    {
        // 创建自定义脚本可播放
        if (enableCustomScript)
        {
            scriptPlayable = ScriptPlayable<CustomPlayableBehaviour>.Create(playableGraph);
            scriptPlayable.GetBehaviour().Initialize(this);
        }
        
        Debug.Log("自定义可播放初始化完成");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新可播放系统
        UpdatePlayablesSystem();
        
        // 更新动画混合
        if (enableAnimationBlending)
        {
            UpdateAnimationBlending();
        }
        
        // 更新时间轴控制
        if (enableTimelineControl)
        {
            UpdateTimelineControl();
        }
        
        // 性能监控
        if (enablePerformanceMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            MonitorPerformance();
            lastMonitoringTime = Time.time;
        }
    }

    /// <summary>
    /// 更新可播放系统
    /// </summary>
    private void UpdatePlayablesSystem()
    {
        float startTime = Time.realtimeSinceStartup;
        
        // 更新可播放图
        if (playableGraph.IsValid())
        {
            playableGraph.Evaluate(Time.deltaTime);
        }
        
        // 更新当前时间
        if (playableDirector != null && playableDirector.playableAsset != null)
        {
            currentTime = playableDirector.time;
        }
        
        playableUpdateTime = Time.realtimeSinceStartup - startTime;
        
        // 更新进度
        if (duration > 0)
        {
            animationProgress = (float)(currentTime / duration);
        }
    }

    /// <summary>
    /// 更新动画混合
    /// </summary>
    private void UpdateAnimationBlending()
    {
        if (!animationMixer.IsValid()) return;
        
        // 更新混合权重
        if (animationMixer.GetInputCount() >= 2)
        {
            float weight1 = 1.0f - blendWeight;
            float weight2 = blendWeight;
            
            animationMixer.SetInputWeight(0, weight1);
            animationMixer.SetInputWeight(1, weight2);
        }
        
        // 更新动画状态
        currentAnimationState = $"混合中 (权重: {blendWeight:F2})";
    }

    /// <summary>
    /// 更新时间轴控制
    /// </summary>
    private void UpdateTimelineControl()
    {
        if (playableDirector == null) return;
        
        // 更新播放状态
        isPlaying = playableDirector.state == PlayState.Playing;
        
        // 处理循环
        if (isLooping && currentTime >= duration)
        {
            playableDirector.time = 0.0;
        }
    }

    /// <summary>
    /// 监控性能
    /// </summary>
    private void MonitorPerformance()
    {
        // 计算活跃可播放数
        activePlayables = 0;
        if (animationMixer.IsValid()) activePlayables++;
        if (audioMixer.IsValid()) activePlayables++;
        if (videoMixer.IsValid()) activePlayables++;
        if (scriptPlayable.IsValid()) activePlayables++;
        
        if (logPlayableData)
        {
            Debug.Log($"可播放性能: 活跃数={activePlayables}, 更新时间={playableUpdateTime * 1000:F2}ms");
        }
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="clipIndex">动画片段索引</param>
    public void PlayAnimation(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= animationClips.Length)
        {
            Debug.LogWarning($"无效的动画片段索引: {clipIndex}");
            return;
        }
        
        if (enableCrossFade)
        {
            StartCrossFade(clipIndex);
        }
        else
        {
            // 直接切换动画
            if (animationMixer.IsValid() && animationMixer.GetInputCount() > clipIndex)
            {
                // 重置所有权重
                for (int i = 0; i < animationMixer.GetInputCount(); i++)
                {
                    animationMixer.SetInputWeight(i, i == clipIndex ? 1.0f : 0.0f);
                }
            }
        }
        
        currentAnimationState = $"播放动画: {animationClips[clipIndex].name}";
        Debug.Log($"开始播放动画: {animationClips[clipIndex].name}");
    }

    /// <summary>
    /// 开始交叉淡入淡出
    /// </summary>
    /// <param name="targetClipIndex">目标动画片段索引</param>
    public void StartCrossFade(int targetClipIndex)
    {
        StartCoroutine(CrossFadeCoroutine(targetClipIndex));
    }

    /// <summary>
    /// 交叉淡入淡出协程
    /// </summary>
    private System.Collections.IEnumerator CrossFadeCoroutine(int targetClipIndex)
    {
        float elapsedTime = 0f;
        float startWeight = blendWeight;
        float targetWeight = targetClipIndex == 0 ? 0f : 1f;
        
        while (elapsedTime < crossFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / crossFadeDuration;
            
            blendWeight = Mathf.Lerp(startWeight, targetWeight, t);
            
            yield return null;
        }
        
        blendWeight = targetWeight;
        currentAnimationState = $"交叉淡入淡出完成: {animationClips[targetClipIndex].name}";
    }

    /// <summary>
    /// 播放时间轴
    /// </summary>
    public void PlayTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Play();
            isPlaying = true;
            currentPlayableState = "时间轴播放中";
            Debug.Log("开始播放时间轴");
        }
    }

    /// <summary>
    /// 暂停时间轴
    /// </summary>
    public void PauseTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Pause();
            isPlaying = false;
            currentPlayableState = "时间轴已暂停";
            Debug.Log("时间轴已暂停");
        }
    }

    /// <summary>
    /// 停止时间轴
    /// </summary>
    public void StopTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Stop();
            isPlaying = false;
            currentTime = 0.0;
            currentPlayableState = "时间轴已停止";
            Debug.Log("时间轴已停止");
        }
    }

    /// <summary>
    /// 设置时间轴时间
    /// </summary>
    /// <param name="time">时间（秒）</param>
    public void SetTimelineTime(double time)
    {
        if (playableDirector != null)
        {
            playableDirector.time = Mathf.Clamp((float)time, 0f, (float)duration);
            currentTime = playableDirector.time;
        }
    }

    /// <summary>
    /// 设置循环播放
    /// </summary>
    /// <param name="looping">是否循环</param>
    public void SetLooping(bool looping)
    {
        isLooping = looping;
        if (playableDirector != null)
        {
            playableDirector.extrapolationMode = looping ? DirectorWrapMode.Loop : DirectorWrapMode.Hold;
        }
        Debug.Log($"循环播放: {(looping ? "启用" : "禁用")}");
    }

    /// <summary>
    /// 创建自定义动画可播放
    /// </summary>
    public void CreateCustomAnimationPlayable()
    {
        if (!enableCustomAnimation) return;
        
        // 这里可以实现自定义动画可播放
        Debug.Log("自定义动画可播放已创建");
    }

    /// <summary>
    /// 创建自定义音频可播放
    /// </summary>
    public void CreateCustomAudioPlayable()
    {
        if (!enableCustomAudio) return;
        
        // 这里可以实现自定义音频可播放
        Debug.Log("自定义音频可播放已创建");
    }

    /// <summary>
    /// 创建自定义视频可播放
    /// </summary>
    public void CreateCustomVideoPlayable()
    {
        if (!enableCustomVideo) return;
        
        // 这里可以实现自定义视频可播放
        Debug.Log("自定义视频可播放已创建");
    }

    /// <summary>
    /// 生成可播放报告
    /// </summary>
    public void GeneratePlayablesReport()
    {
        Debug.Log("=== 可播放系统报告 ===");
        Debug.Log($"可播放状态: {currentPlayableState}");
        Debug.Log($"动画状态: {currentAnimationState}");
        Debug.Log($"播放状态: {(isPlaying ? "播放中" : "已停止")}");
        Debug.Log($"当前时间: {currentTime:F2}s / {duration:F2}s");
        Debug.Log($"动画进度: {animationProgress:P1}");
        Debug.Log($"活跃可播放数: {activePlayables}");
        Debug.Log($"可播放更新时间: {playableUpdateTime * 1000:F2}ms");
        Debug.Log($"混合权重: {blendWeight:F2}");
        Debug.Log($"循环播放: {(isLooping ? "启用" : "禁用")}");
    }

    /// <summary>
    /// 重置可播放系统
    /// </summary>
    public void ResetPlayablesSystem()
    {
        Debug.Log("重置可播放系统...");
        
        // 停止播放
        StopTimeline();
        
        // 重置混合权重
        blendWeight = 0.5f;
        
        // 重置时间
        currentTime = 0.0;
        animationProgress = 0f;
        
        // 重置状态
        currentPlayableState = "已重置";
        currentAnimationState = "空闲";
        
        Debug.Log("可播放系统重置完成");
    }

    /// <summary>
    /// 导出可播放数据
    /// </summary>
    public void ExportPlayablesData()
    {
        var data = new PlayablesData
        {
            timestamp = System.DateTime.Now.ToString(),
            currentPlayableState = currentPlayableState,
            currentAnimationState = currentAnimationState,
            isPlaying = isPlaying,
            currentTime = currentTime,
            duration = duration,
            animationProgress = animationProgress,
            blendWeight = blendWeight,
            activePlayables = activePlayables,
            playableUpdateTime = playableUpdateTime,
            isLooping = isLooping
        };
        
        string json = JsonUtility.ToJson(data, true);
        string filename = $"playables_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        
        System.IO.File.WriteAllText(filename, json);
        Debug.Log($"可播放数据已导出: {filename}");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Playables 可播放系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("系统配置:");
        enablePlayables = GUILayout.Toggle(enablePlayables, "启用可播放系统");
        enableAnimationBlending = GUILayout.Toggle(enableAnimationBlending, "启用动画混合");
        enableTimelineControl = GUILayout.Toggle(enableTimelineControl, "启用时间轴控制");
        enableCustomPlayables = GUILayout.Toggle(enableCustomPlayables, "启用自定义可播放");
        enablePerformanceMonitoring = GUILayout.Toggle(enablePerformanceMonitoring, "启用性能监控");
        
        GUILayout.Space(10);
        GUILayout.Label("动画混合:");
        blendWeight = GUILayout.HorizontalSlider(blendWeight, 0f, 1f);
        GUILayout.Label($"混合权重: {blendWeight:F2}");
        blendSpeed = float.TryParse(GUILayout.TextField("混合速度", blendSpeed.ToString()), out var speed) ? speed : blendSpeed;
        enableCrossFade = GUILayout.Toggle(enableCrossFade, "启用交叉淡入淡出");
        crossFadeDuration = float.TryParse(GUILayout.TextField("交叉淡入淡出持续时间", crossFadeDuration.ToString()), out var fadeDuration) ? fadeDuration : crossFadeDuration;
        
        GUILayout.Space(10);
        GUILayout.Label("时间轴控制:");
        GUILayout.Label($"当前时间: {currentTime:F2}s / {duration:F2}s");
        GUILayout.Label($"播放状态: {(isPlaying ? "播放中" : "已停止")}");
        isLooping = GUILayout.Toggle(isLooping, "循环播放");
        
        GUILayout.Space(10);
        GUILayout.Label("系统状态:");
        GUILayout.Label($"可播放状态: {currentPlayableState}");
        GUILayout.Label($"动画状态: {currentAnimationState}");
        GUILayout.Label($"动画进度: {animationProgress:P1}");
        GUILayout.Label($"活跃可播放数: {activePlayables}");
        GUILayout.Label($"可播放更新时间: {playableUpdateTime * 1000:F2}ms");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("播放时间轴"))
        {
            PlayTimeline();
        }
        
        if (GUILayout.Button("暂停时间轴"))
        {
            PauseTimeline();
        }
        
        if (GUILayout.Button("停止时间轴"))
        {
            StopTimeline();
        }
        
        if (animationClips != null && animationClips.Length > 0)
        {
            for (int i = 0; i < animationClips.Length; i++)
            {
                if (GUILayout.Button($"播放动画 {i}: {animationClips[i].name}"))
                {
                    PlayAnimation(i);
                }
            }
        }
        
        if (GUILayout.Button("创建自定义动画可播放"))
        {
            CreateCustomAnimationPlayable();
        }
        
        if (GUILayout.Button("创建自定义音频可播放"))
        {
            CreateCustomAudioPlayable();
        }
        
        if (GUILayout.Button("创建自定义视频可播放"))
        {
            CreateCustomVideoPlayable();
        }
        
        if (GUILayout.Button("生成可播放报告"))
        {
            GeneratePlayablesReport();
        }
        
        if (GUILayout.Button("重置可播放系统"))
        {
            ResetPlayablesSystem();
        }
        
        if (GUILayout.Button("导出可播放数据"))
        {
            ExportPlayablesData();
        }
        
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        // 清理可播放图
        if (playableGraph.IsValid())
        {
            playableGraph.Destroy();
        }
    }
}

/// <summary>
/// 自定义可播放行为
/// </summary>
[System.Serializable]
public class CustomPlayableBehaviour : PlayableBehaviour
{
    public PlayablesExample playablesExample;
    
    public void Initialize(PlayablesExample example)
    {
        playablesExample = example;
    }
    
    public override void OnPlayableCreate(Playable playable)
    {
        Debug.Log("自定义可播放行为已创建");
    }
    
    public override void OnPlayableDestroy(Playable playable)
    {
        Debug.Log("自定义可播放行为已销毁");
    }
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // 处理帧数据
        if (playablesExample != null)
        {
            // 这里可以实现自定义的帧处理逻辑
        }
    }
}

/// <summary>
/// 可播放数据类
/// </summary>
[System.Serializable]
public class PlayablesData
{
    public string timestamp;
    public string currentPlayableState;
    public string currentAnimationState;
    public bool isPlaying;
    public double currentTime;
    public double duration;
    public float animationProgress;
    public float blendWeight;
    public int activePlayables;
    public float playableUpdateTime;
    public bool isLooping;
} 