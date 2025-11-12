using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Animation 命名空间案例演示
/// 展示动画系统的核心功能
/// </summary>
public class AnimationExample : MonoBehaviour
{
    [Header("动画组件")]
    [SerializeField] private Animator animator;
    [SerializeField] private Animation legacyAnimation;
    [SerializeField] private AnimationClip[] animationClips;
    
    [Header("动画设置")]
    [SerializeField] private bool enableAnimation = true;
    [SerializeField] private float playbackSpeed = 1.0f;
    [SerializeField] private bool loopAnimation = true;
    [SerializeField] private bool crossFade = true;
    [SerializeField] private float crossFadeTime = 0.25f;
    
    [Header("动画状态")]
    [SerializeField] private bool isPlaying = false;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private string currentAnimation = "";
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float totalTime = 0f;
    [SerializeField] private int currentClipIndex = 0;
    
    [Header("动画参数")]
    [SerializeField] private float speedParameter = 0f;
    [SerializeField] private float jumpParameter = 0f;
    [SerializeField] private bool isGroundedParameter = true;
    [SerializeField] private int stateParameter = 0;
    
    [Header("动画层")]
    [SerializeField] private int baseLayerIndex = 0;
    [SerializeField] private int upperBodyLayerIndex = 1;
    [SerializeField] private float upperBodyWeight = 1.0f;
    [SerializeField] private AvatarMask upperBodyMask;
    
    // 动画事件
    private System.Action<string> onAnimationStart;
    private System.Action<string> onAnimationEnd;
    private System.Action<string> onAnimationEvent;
    
    private void Start()
    {
        InitializeAnimationSystem();
    }
    
    /// <summary>
    /// 初始化动画系统
    /// </summary>
    private void InitializeAnimationSystem()
    {
        // 获取或创建Animator
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = gameObject.AddComponent<Animator>();
            }
        }
        
        // 获取或创建Legacy Animation
        if (legacyAnimation == null)
        {
            legacyAnimation = GetComponent<Animation>();
            if (legacyAnimation == null)
            {
                legacyAnimation = gameObject.AddComponent<Animation>();
            }
        }
        
        // 配置动画组件
        ConfigureAnimationComponents();
        
        // 设置动画事件
        SetupAnimationEvents();
        
        // 加载动画片段
        LoadAnimationClips();
        
        Debug.Log("动画系统初始化完成");
    }
    
    /// <summary>
    /// 配置动画组件
    /// </summary>
    private void ConfigureAnimationComponents()
    {
        if (animator != null)
        {
            // 设置Animator参数
            animator.speed = playbackSpeed;
            animator.applyRootMotion = false;
            animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            
            // 设置动画层
            if (animator.layerCount > 1)
            {
                animator.SetLayerWeight(upperBodyLayerIndex, upperBodyWeight);
                if (upperBodyMask != null)
                {
                    animator.SetLayerWeight(upperBodyLayerIndex, upperBodyWeight);
                }
            }
        }
        
        if (legacyAnimation != null)
        {
            // 设置Legacy Animation参数
            legacyAnimation.playAutomatically = false;
            legacyAnimation.wrapMode = loopAnimation ? WrapMode.Loop : WrapMode.Once;
        }
        
        Debug.Log("动画组件配置完成");
    }
    
    /// <summary>
    /// 设置动画事件
    /// </summary>
    private void SetupAnimationEvents()
    {
        if (animator != null)
        {
            // 监听动画状态变化
            StartCoroutine(MonitorAnimationState());
        }
        
        Debug.Log("动画事件设置完成");
    }
    
    /// <summary>
    /// 监控动画状态
    /// </summary>
    private System.Collections.IEnumerator MonitorAnimationState()
    {
        string previousState = "";
        
        while (true)
        {
            if (animator != null && animator.isActiveAndEnabled)
            {
                AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
                string currentStateName = currentState.IsName("") ? "Unknown" : currentState.fullPathHash.ToString();
                
                if (currentStateName != previousState)
                {
                    if (!string.IsNullOrEmpty(previousState))
                    {
                        onAnimationEnd?.Invoke(previousState);
                    }
                    
                    onAnimationStart?.Invoke(currentStateName);
                    previousState = currentStateName;
                }
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    /// <summary>
    /// 加载动画片段
    /// </summary>
    private void LoadAnimationClips()
    {
        if (animationClips != null && animationClips.Length > 0)
        {
            // 加载到Legacy Animation
            if (legacyAnimation != null)
            {
                foreach (var clip in animationClips)
                {
                    if (clip != null)
                    {
                        legacyAnimation.AddClip(clip, clip.name);
                    }
                }
            }
            
            Debug.Log($"加载了 {animationClips.Length} 个动画片段");
        }
    }
    
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    public void PlayAnimation(string animationName)
    {
        if (string.IsNullOrEmpty(animationName))
        {
            Debug.LogError("动画名称不能为空");
            return;
        }
        
        // 尝试使用Animator播放
        if (animator != null && animator.isActiveAndEnabled)
        {
            animator.Play(animationName);
            currentAnimation = animationName;
            isPlaying = true;
            isPaused = false;
            
            Debug.Log($"Animator播放动画: {animationName}");
            return;
        }
        
        // 尝试使用Legacy Animation播放
        if (legacyAnimation != null && legacyAnimation.GetClip(animationName) != null)
        {
            if (crossFade)
            {
                legacyAnimation.CrossFade(animationName, crossFadeTime);
            }
            else
            {
                legacyAnimation.Play(animationName);
            }
            
            currentAnimation = animationName;
            isPlaying = true;
            isPaused = false;
            
            Debug.Log($"Legacy Animation播放动画: {animationName}");
            return;
        }
        
        Debug.LogError($"找不到动画: {animationName}");
    }
    
    /// <summary>
    /// 播放动画片段
    /// </summary>
    /// <param name="clipIndex">片段索引</param>
    public void PlayAnimationClip(int clipIndex)
    {
        if (animationClips == null || clipIndex < 0 || clipIndex >= animationClips.Length)
        {
            Debug.LogError("无效的动画片段索引");
            return;
        }
        
        AnimationClip clip = animationClips[clipIndex];
        if (clip != null)
        {
            PlayAnimation(clip.name);
            currentClipIndex = clipIndex;
        }
    }
    
    /// <summary>
    /// 暂停动画
    /// </summary>
    public void PauseAnimation()
    {
        if (animator != null)
        {
            animator.speed = 0f;
        }
        
        if (legacyAnimation != null)
        {
            legacyAnimation.Sample();
        }
        
        isPaused = true;
        Debug.Log("动画已暂停");
    }
    
    /// <summary>
    /// 恢复动画
    /// </summary>
    public void ResumeAnimation()
    {
        if (animator != null)
        {
            animator.speed = playbackSpeed;
        }
        
        if (legacyAnimation != null)
        {
            legacyAnimation.Play();
        }
        
        isPaused = false;
        Debug.Log("动画已恢复");
    }
    
    /// <summary>
    /// 停止动画
    /// </summary>
    public void StopAnimation()
    {
        if (animator != null)
        {
            animator.Play("Idle");
        }
        
        if (legacyAnimation != null)
        {
            legacyAnimation.Stop();
        }
        
        isPlaying = false;
        isPaused = false;
        currentTime = 0f;
        
        Debug.Log("动画已停止");
    }
    
    /// <summary>
    /// 设置播放速度
    /// </summary>
    /// <param name="speed">播放速度</param>
    public void SetPlaybackSpeed(float speed)
    {
        playbackSpeed = Mathf.Clamp(speed, 0.1f, 5.0f);
        
        if (animator != null)
        {
            animator.speed = playbackSpeed;
        }
        
        if (legacyAnimation != null)
        {
            legacyAnimation.playbackSpeed = playbackSpeed;
        }
        
        Debug.Log($"播放速度已设置为: {playbackSpeed}");
    }
    
    /// <summary>
    /// 设置动画参数
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <param name="value">参数值</param>
    public void SetAnimationParameter(string parameterName, float value)
    {
        if (animator != null)
        {
            animator.SetFloat(parameterName, value);
        }
        
        Debug.Log($"设置动画参数 {parameterName}: {value}");
    }
    
    /// <summary>
    /// 设置动画参数
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <param name="value">参数值</param>
    public void SetAnimationParameter(string parameterName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(parameterName, value);
        }
        
        Debug.Log($"设置动画参数 {parameterName}: {value}");
    }
    
    /// <summary>
    /// 设置动画参数
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <param name="value">参数值</param>
    public void SetAnimationParameter(string parameterName, int value)
    {
        if (animator != null)
        {
            animator.SetInteger(parameterName, value);
        }
        
        Debug.Log($"设置动画参数 {parameterName}: {value}");
    }
    
    /// <summary>
    /// 触发动画触发器
    /// </summary>
    /// <param name="triggerName">触发器名称</param>
    public void SetAnimationTrigger(string triggerName)
    {
        if (animator != null)
        {
            animator.SetTrigger(triggerName);
        }
        
        Debug.Log($"触发动画触发器: {triggerName}");
    }
    
    /// <summary>
    /// 重置动画触发器
    /// </summary>
    /// <param name="triggerName">触发器名称</param>
    public void ResetAnimationTrigger(string triggerName)
    {
        if (animator != null)
        {
            animator.ResetTrigger(triggerName);
        }
        
        Debug.Log($"重置动画触发器: {triggerName}");
    }
    
    /// <summary>
    /// 设置动画层权重
    /// </summary>
    /// <param name="layerIndex">层索引</param>
    /// <param name="weight">权重</param>
    public void SetLayerWeight(int layerIndex, float weight)
    {
        if (animator != null && layerIndex >= 0 && layerIndex < animator.layerCount)
        {
            animator.SetLayerWeight(layerIndex, Mathf.Clamp01(weight));
        }
        
        Debug.Log($"设置动画层 {layerIndex} 权重: {weight}");
    }
    
    /// <summary>
    /// 播放下一个动画
    /// </summary>
    public void PlayNextAnimation()
    {
        if (animationClips == null || animationClips.Length == 0)
        {
            Debug.LogError("没有可播放的动画");
            return;
        }
        
        int nextIndex = (currentClipIndex + 1) % animationClips.Length;
        PlayAnimationClip(nextIndex);
    }
    
    /// <summary>
    /// 播放上一个动画
    /// </summary>
    public void PlayPreviousAnimation()
    {
        if (animationClips == null || animationClips.Length == 0)
        {
            Debug.LogError("没有可播放的动画");
            return;
        }
        
        int prevIndex = (currentClipIndex - 1 + animationClips.Length) % animationClips.Length;
        PlayAnimationClip(prevIndex);
    }
    
    /// <summary>
    /// 获取动画信息
    /// </summary>
    public void GetAnimationInfo()
    {
        Debug.Log("=== 动画系统信息 ===");
        Debug.Log($"动画启用: {enableAnimation}");
        Debug.Log($"当前动画: {currentAnimation}");
        Debug.Log($"播放状态: {(isPlaying ? (isPaused ? "暂停" : "播放中") : "停止")}");
        Debug.Log($"播放速度: {playbackSpeed}");
        Debug.Log($"循环播放: {loopAnimation}");
        Debug.Log($"当前时间: {currentTime:F2}s / {totalTime:F2}s");
        Debug.Log($"当前片段索引: {currentClipIndex}");
        
        if (animator != null)
        {
            Debug.Log($"Animator: 已配置");
            Debug.Log($"动画层数量: {animator.layerCount}");
            Debug.Log($"参数数量: {animator.parameters.Length}");
            
            foreach (var param in animator.parameters)
            {
                Debug.Log($"  参数: {param.name} ({param.type})");
            }
        }
        
        if (legacyAnimation != null)
        {
            Debug.Log($"Legacy Animation: 已配置");
            Debug.Log($"动画片段数量: {legacyAnimation.GetClipCount()}");
        }
        
        if (animationClips != null)
        {
            Debug.Log($"动画片段数组: {animationClips.Length} 个片段");
            for (int i = 0; i < animationClips.Length; i++)
            {
                if (animationClips[i] != null)
                {
                    Debug.Log($"  片段 {i}: {animationClips[i].name} ({animationClips[i].length:F2}s)");
                }
            }
        }
    }
    
    /// <summary>
    /// 重置动画设置
    /// </summary>
    public void ResetAnimationSettings()
    {
        // 重置设置
        playbackSpeed = 1.0f;
        loopAnimation = true;
        crossFade = true;
        crossFadeTime = 0.25f;
        
        // 重置参数
        speedParameter = 0f;
        jumpParameter = 0f;
        isGroundedParameter = true;
        stateParameter = 0;
        
        // 应用设置
        SetPlaybackSpeed(playbackSpeed);
        
        if (animator != null)
        {
            animator.SetFloat("Speed", speedParameter);
            animator.SetFloat("Jump", jumpParameter);
            animator.SetBool("IsGrounded", isGroundedParameter);
            animator.SetInteger("State", stateParameter);
        }
        
        Debug.Log("动画设置已重置");
    }
    
    /// <summary>
    /// 动画事件回调
    /// </summary>
    /// <param name="eventName">事件名称</param>
    public void OnAnimationEvent(string eventName)
    {
        Debug.Log($"动画事件: {eventName}");
        onAnimationEvent?.Invoke(eventName);
    }
    
    private void Update()
    {
        // 更新动画状态
        if (legacyAnimation != null && legacyAnimation.isPlaying)
        {
            currentTime = legacyAnimation.time;
            if (legacyAnimation.clip != null)
            {
                totalTime = legacyAnimation.clip.length;
            }
        }
        
        // 更新Animator参数
        if (animator != null)
        {
            animator.SetFloat("Speed", speedParameter);
            animator.SetFloat("Jump", jumpParameter);
            animator.SetBool("IsGrounded", isGroundedParameter);
            animator.SetInteger("State", stateParameter);
        }
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 700));
        GUILayout.Label("动画系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 动画状态
        GUILayout.Label($"当前动画: {currentAnimation}");
        GUILayout.Label($"播放状态: {(isPlaying ? (isPaused ? "暂停" : "播放中") : "停止")}");
        GUILayout.Label($"播放速度: {playbackSpeed:F2}");
        GUILayout.Label($"当前时间: {currentTime:F2}s / {totalTime:F2}s");
        
        GUILayout.Space(10);
        
        // 播放控制
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("播放"))
        {
            if (animationClips != null && animationClips.Length > 0)
            {
                PlayAnimationClip(currentClipIndex);
            }
        }
        if (GUILayout.Button("暂停"))
        {
            PauseAnimation();
        }
        if (GUILayout.Button("停止"))
        {
            StopAnimation();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("上一个"))
        {
            PlayPreviousAnimation();
        }
        if (GUILayout.Button("下一个"))
        {
            PlayNextAnimation();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        
        // 播放速度控制
        GUILayout.Label("播放速度:");
        playbackSpeed = GUILayout.HorizontalSlider(playbackSpeed, 0.1f, 3.0f);
        if (GUILayout.Button("设置播放速度"))
        {
            SetPlaybackSpeed(playbackSpeed);
        }
        
        GUILayout.Space(10);
        
        // 动画参数控制
        GUILayout.Label("动画参数:");
        
        GUILayout.Label($"速度: {speedParameter:F2}");
        speedParameter = GUILayout.HorizontalSlider(speedParameter, 0f, 10f);
        if (GUILayout.Button("设置速度"))
        {
            SetAnimationParameter("Speed", speedParameter);
        }
        
        GUILayout.Label($"跳跃: {jumpParameter:F2}");
        jumpParameter = GUILayout.HorizontalSlider(jumpParameter, 0f, 10f);
        if (GUILayout.Button("设置跳跃"))
        {
            SetAnimationParameter("Jump", jumpParameter);
        }
        
        isGroundedParameter = GUILayout.Toggle(isGroundedParameter, "着地状态");
        if (GUILayout.Button("设置着地状态"))
        {
            SetAnimationParameter("IsGrounded", isGroundedParameter);
        }
        
        GUILayout.Label($"状态: {stateParameter}");
        stateParameter = (int)GUILayout.HorizontalSlider(stateParameter, 0, 5);
        if (GUILayout.Button("设置状态"))
        {
            SetAnimationParameter("State", stateParameter);
        }
        
        GUILayout.Space(10);
        
        // 触发器
        if (GUILayout.Button("触发跳跃"))
        {
            SetAnimationTrigger("Jump");
        }
        
        if (GUILayout.Button("触发攻击"))
        {
            SetAnimationTrigger("Attack");
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取动画信息"))
        {
            GetAnimationInfo();
        }
        
        if (GUILayout.Button("重置设置"))
        {
            ResetAnimationSettings();
        }
        
        GUILayout.EndArea();
    }
} 