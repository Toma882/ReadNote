using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Animations 命名空间案例演示
/// 展示动画控制器、动画状态机、动画事件等核心功能
/// </summary>
public class AnimationsExample : MonoBehaviour
{
    [Header("动画组件")]
    [SerializeField] private Animator animator; //动画控制器
    [SerializeField] private AnimationClip[] animationClips; //动画片段
    [SerializeField] private RuntimeAnimatorController animatorController; //运行时动画控制器
    
    [Header("Playable系统")]
    [SerializeField] private PlayableGraph playableGraph;
    [SerializeField] private AnimationMixerPlayable mixerPlayable;
    [SerializeField] private AnimationClipPlayable[] clipPlayables;
    [SerializeField] private bool usePlayableSystem = false;
    
    [Header("动画参数")]
    [SerializeField] private float speed = 1f; //播放速度
    [SerializeField] private bool isPlaying = false; //是否正在播放
    [SerializeField] private string currentState = ""; //当前状态
    [SerializeField] private float normalizedTime = 0f; //标准化时间
    
    [Header("动画控制")]
    [SerializeField] private bool loop = true; //循环播放
    [SerializeField] private bool crossFade = true; //交叉淡入淡出
    [SerializeField] private float crossFadeDuration = 0.25f; //交叉淡入淡出时长
    [SerializeField] private bool enableIK = false; //启用IK
    
    [Header("动画事件")]
    [SerializeField] private bool enableEvents = true; //启用事件
    [SerializeField] private string eventMessage = "动画事件触发"; //事件消息
    [SerializeField] private float eventTime = 0.5f; //事件时间
    
    [Header("动画层")]
    [SerializeField] private int currentLayer = 0; //当前层
    [SerializeField] private float layerWeight = 1f; //层权重
    [SerializeField] private bool layerEnabled = true; //层启用
    
    [Header("动画统计")]
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float playbackSpeed = 1.0f;
    [SerializeField] private int currentClipIndex = 0;
    
    // 动画状态
    private Dictionary<string, int> stateHashes = new Dictionary<string, int>();
    private List<AnimationClip> loadedClips = new List<AnimationClip>();
    
    private void Start()
    {
        InitializeAnimationSystem();
    }
    
    /// <summary>
    /// 初始化动画系统
    /// </summary>
    private void InitializeAnimationSystem()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
        }
        
        // 设置动画控制器
        if (animatorController != null)
        {
            animator.runtimeAnimatorController = animatorController;
        }
        
        // 设置动画参数
        animator.speed = speed;
        
        // 启用IK
        if (enableIK)
        {
            animator.applyRootMotion = true;
        }
        
        // 设置动画事件
        if (enableEvents)
        {
            SetupAnimationEvents();
        }
        
        // 初始化Playable系统
        if (usePlayableSystem)
        {
            InitializePlayableSystem();
        }
        
        // 加载动画片段
        LoadAnimationClips();
        
        Debug.Log("动画系统初始化完成");
    }
    
    /// <summary>
    /// 设置动画事件
    /// </summary>
    private void SetupAnimationEvents()
    {
        if (animationClips != null && animationClips.Length > 0)
        {
            foreach (var clip in animationClips)
            {
                if (clip != null)
                {
                    // 创建动画事件
                    AnimationEvent animEvent = new AnimationEvent();
                    animEvent.functionName = "OnAnimationEvent";
                    animEvent.time = eventTime;
                    animEvent.stringParameter = eventMessage;
                    
                    // 添加事件到动画片段
                    clip.AddEvent(animEvent);
                }
            }
        }
    }
    
    /// <summary>
    /// 动画事件回调
    /// </summary>
    /// <param name="message">事件消息</param>
    public void OnAnimationEvent(string message)
    {
        Debug.Log($"动画事件: {message}");
    }
    
    /// <summary>
    /// 初始化Playable系统
    /// </summary>
    private void InitializePlayableSystem()
    {
        // 创建Playable图
        playableGraph = PlayableGraph.Create("Custom Animation Graph");
        
        // 创建混合器
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, animationClips.Length);
        
        // 创建片段播放器
        clipPlayables = new AnimationClipPlayable[animationClips.Length];
        for (int i = 0; i < animationClips.Length; i++)
        {
            if (animationClips[i] != null)
            {
                clipPlayables[i] = AnimationClipPlayable.Create(playableGraph, animationClips[i]);
                clipPlayables[i].SetApplyFootIK(false);
                clipPlayables[i].SetApplyPlayableIK(false);
                
                // 连接到混合器
                playableGraph.Connect(clipPlayables[i], 0, mixerPlayable, i);
            }
        }
        
        // 连接到输出
        var output = AnimationPlayableOutput.Create(playableGraph, "Animation", animator);
        output.SetSourcePlayable(mixerPlayable);
        
        // 播放图
        playableGraph.Play();
        
        Debug.Log("Playable系统初始化完成");
    }
    
    /// <summary>
    /// 加载动画片段
    /// </summary>
    private void LoadAnimationClips()
    {
        loadedClips.Clear();
        
        if (animationClips != null)
        {
            foreach (var clip in animationClips)
            {
                if (clip != null)
                {
                    loadedClips.Add(clip);
                    stateHashes[clip.name] = Animator.StringToHash(clip.name);
                }
            }
        }
        
        Debug.Log($"已加载 {loadedClips.Count} 个动画片段");
    }
    
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="clipName">动画片段名称</param>
    public void PlayAnimation(string clipName)
    {
        if (animator != null)
        {
            animator.Play(clipName);
            isPlaying = true;
            currentState = clipName;
            Debug.Log($"播放动画: {clipName}");
        }
    }
    
    /// <summary>
    /// 播放动画片段
    /// </summary>
    /// <param name="clipIndex">动画片段索引</param>
    public void PlayAnimationClip(int clipIndex)
    {
        if (animationClips != null && clipIndex >= 0 && clipIndex < animationClips.Length)
        {
            currentClipIndex = clipIndex;
            AnimationClip clip = animationClips[clipIndex];
            
            if (clip != null)
            {
                animator.Play(clip.name);
                isPlaying = true;
                currentState = clip.name;
                Debug.Log($"播放动画片段: {clip.name}");
            }
        }
    }
    
    /// <summary>
    /// 停止动画
    /// </summary>
    public void StopAnimation()
    {
        if (usePlayableSystem)
        {
            if (mixerPlayable.IsValid())
            {
                for (int i = 0; i < clipPlayables.Length; i++)
                {
                    mixerPlayable.SetInputWeight(i, 0f);
                }
            }
        }
        else
        {
            if (animator != null)
            {
                animator.enabled = false;
                animator.enabled = true;
            }
        }
        
        isPlaying = false;
        Debug.Log("动画已停止");
    }
    
    /// <summary>
    /// 暂停动画
    /// </summary>
    public void PauseAnimation()
    {
        if (usePlayableSystem)
        {
            if (playableGraph.IsValid())
            {
                playableGraph.GetRootPlayable(0).SetSpeed(0);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.speed = 0;
            }
        }
        
        Debug.Log("动画已暂停");
    }
    
    /// <summary>
    /// 恢复动画
    /// </summary>
    public void ResumeAnimation()
    {
        if (usePlayableSystem)
        {
            if (playableGraph.IsValid())
            {
                playableGraph.GetRootPlayable(0).SetSpeed(playbackSpeed);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.speed = playbackSpeed;
            }
        }
        
        Debug.Log("动画已恢复");
    }
    
    /// <summary>
    /// 设置播放速度
    /// </summary>
    /// <param name="newSpeed">新的播放速度</param>
    public void SetAnimationSpeed(float newSpeed)
    {
        speed = Mathf.Clamp(newSpeed, 0.1f, 10f);
        
        if (usePlayableSystem)
        {
            if (playableGraph.IsValid())
            {
                playableGraph.GetRootPlayable(0).SetSpeed(speed);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.speed = speed;
            }
        }
        
        Debug.Log($"播放速度已设置为: {speed}");
    }
    
    /// <summary>
    /// 设置动画参数
    /// </summary>
    /// <param name="paramName">参数名称</param>
    /// <param name="value">参数值</param>
    public void SetAnimationParameter(string paramName, float value)
    {
        if (animator != null)
        {
            animator.SetFloat(paramName, value);
        }
    }
    
    /// <summary>
    /// 设置布尔参数
    /// </summary>
    /// <param name="paramName">参数名称</param>
    /// <param name="value">参数值</param>
    public void SetAnimationParameter(string paramName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(paramName, value);
        }
    }
    
    /// <summary>
    /// 设置整数参数
    /// </summary>
    /// <param name="paramName">参数名称</param>
    /// <param name="value">参数值</param>
    public void SetAnimationParameter(string paramName, int value)
    {
        if (animator != null)
        {
            animator.SetInteger(paramName, value);
        }
    }
    
    /// <summary>
    /// 设置触发器参数
    /// </summary>
    /// <param name="paramName">参数名称</param>
    public void SetAnimationTrigger(string paramName)
    {
        if (animator != null)
        {
            animator.SetTrigger(paramName);
        }
    }
    
    /// <summary>
    /// 设置动画层权重
    /// </summary>
    /// <param name="layerIndex">层索引</param>
    /// <param name="weight">权重</param>
    public void SetLayerWeight(int layerIndex, float weight)
    {
        if (animator != null)
        {
            animator.SetLayerWeight(layerIndex, weight);
            layerWeight = weight;
        }
    }
    
    /// <summary>
    /// 启用/禁用动画层
    /// </summary>
    /// <param name="layerIndex">层索引</param>
    /// <param name="enabled">是否启用</param>
    public void SetLayerEnabled(int layerIndex, bool enabled)
    {
        if (animator != null)
        {
            animator.SetLayerWeight(layerIndex, enabled ? 1f : 0f);
            layerEnabled = enabled;
        }
    }
    
    /// <summary>
    /// 获取动画信息
    /// </summary>
    public void GetAnimationInfo()
    {
        if (animator != null)
        {
            Debug.Log("=== 动画信息 ===");
            Debug.Log($"动画控制器: {(animator.runtimeAnimatorController != null ? "已设置" : "未设置")}");
            Debug.Log($"当前状态: {currentState}");
            Debug.Log($"播放速度: {animator.speed}");
            Debug.Log($"是否播放: {isPlaying}");
            Debug.Log($"标准化时间: {normalizedTime}");
            Debug.Log($"当前层: {currentLayer}");
            Debug.Log($"层权重: {layerWeight}");
            Debug.Log($"层启用: {layerEnabled}");
            Debug.Log($"IK启用: {enableIK}");
            Debug.Log($"根运动: {animator.applyRootMotion}");
        }
    }
    
    /// <summary>
    /// 获取动画片段信息
    /// </summary>
    public void GetAnimationClipInfo()
    {
        if (animationClips != null)
        {
            Debug.Log("=== 动画片段信息 ===");
            for (int i = 0; i < animationClips.Length; i++)
            {
                if (animationClips[i] != null)
                {
                    Debug.Log($"片段 {i}: {animationClips[i].name}");
                    Debug.Log($"  长度: {animationClips[i].length}秒");
                    Debug.Log($"  帧率: {animationClips[i].frameRate}");
                    Debug.Log($"  循环: {animationClips[i].isLooping}");
                }
            }
        }
    }
    
    /// <summary>
    /// 测试动画功能
    /// </summary>
    public void TestAnimationFeatures()
    {
        Debug.Log("开始测试动画功能");
        
        // 测试播放动画
        if (animationClips != null && animationClips.Length > 0)
        {
            PlayAnimationClip(currentClipIndex);
        }
        
        // 测试设置参数
        SetAnimationParameter("Speed", 1.5f);
        SetAnimationParameter("IsMoving", true);
        
        // 测试层控制
        SetLayerWeight(0, 0.8f);
        
        Debug.Log("动画功能测试完成");
    }
    
    /// <summary>
    /// 重置动画设置
    /// </summary>
    public void ResetAnimationSettings()
    {
        speed = 1f;
        loop = true;
        crossFade = true;
        crossFadeDuration = 0.25f;
        enableIK = false;
        enableEvents = true;
        eventTime = 0.5f;
        currentLayer = 0;
        layerWeight = 1f;
        layerEnabled = true;
        
        if (animator != null)
        {
            animator.speed = speed;
            animator.applyRootMotion = enableIK;
            SetLayerWeight(currentLayer, layerWeight);
        }
        
        if (usePlayableSystem)
        {
            if (playableGraph.IsValid())
            {
                playableGraph.Destroy();
            }
        }
        
        Debug.Log("动画设置已重置");
    }
    
    private void Update()
    {
        // 更新动画状态
        if (isPlaying && currentClipIndex < loadedClips.Count)
        {
            AnimationClip clip = loadedClips[currentClipIndex];
            
            if (usePlayableSystem)
            {
                if (currentClipIndex < clipPlayables.Length && clipPlayables[currentClipIndex].IsValid())
                {
                    currentTime = (float)clipPlayables[currentClipIndex].GetTime();
                    normalizedTime = currentTime / clip.length;
                }
            }
            else
            {
                if (animator != null)
                {
                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    currentTime = stateInfo.normalizedTime * clip.length;
                    normalizedTime = stateInfo.normalizedTime;
                }
            }
        }
    }
    
    private void OnDestroy()
    {
        // 清理Playable系统
        if (playableGraph.IsValid())
        {
            playableGraph.Destroy();
        }
    }
    
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 700));
        GUILayout.Label("Animations 动画系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("动画控制:");
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("播放"))
        {
            if (animationClips != null && animationClips.Length > 0)
            {
                PlayAnimationClip(currentClipIndex);
            }
        }
        if (GUILayout.Button("停止"))
        {
            StopAnimation();
        }
        if (GUILayout.Button("暂停"))
        {
            PauseAnimation();
        }
        if (GUILayout.Button("恢复"))
        {
            ResumeAnimation();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(5);
        GUILayout.Label($"状态: {(isPlaying ? "播放中" : "已停止")}");
        GUILayout.Label($"当前状态: {currentState}");
        GUILayout.Label($"标准化时间: {normalizedTime:F2}");
        
        GUILayout.Space(10);
        GUILayout.Label("动画设置:");
        
        speed = float.TryParse(GUILayout.TextField("播放速度", speed.ToString()), out var spd) ? spd : speed;
        if (GUILayout.Button("设置速度"))
        {
            SetAnimationSpeed(speed);
        }
        
        loop = GUILayout.Toggle(loop, "循环播放");
        crossFade = GUILayout.Toggle(crossFade, "交叉淡入淡出");
        enableIK = GUILayout.Toggle(enableIK, "启用IK");
        enableEvents = GUILayout.Toggle(enableEvents, "启用事件");
        
        GUILayout.Space(10);
        GUILayout.Label("层控制:");
        
        currentLayer = int.TryParse(GUILayout.TextField("当前层", currentLayer.ToString()), out var layer) ? layer : currentLayer;
        layerWeight = float.TryParse(GUILayout.TextField("层权重", layerWeight.ToString()), out var weight) ? weight : layerWeight;
        
        if (GUILayout.Button("设置层权重"))
        {
            SetLayerWeight(currentLayer, layerWeight);
        }
        
        layerEnabled = GUILayout.Toggle(layerEnabled, "层启用");
        if (GUILayout.Button("设置层状态"))
        {
            SetLayerEnabled(currentLayer, layerEnabled);
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取动画信息"))
        {
            GetAnimationInfo();
        }
        
        if (GUILayout.Button("获取片段信息"))
        {
            GetAnimationClipInfo();
        }
        
        if (GUILayout.Button("测试动画功能"))
        {
            TestAnimationFeatures();
        }
        
        if (GUILayout.Button("重置动画设置"))
        {
            ResetAnimationSettings();
        }
        
        GUILayout.EndArea();
    }
} 