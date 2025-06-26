using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// UnityEngine.Audio 命名空间案例演示
/// 展示音频系统的核心功能
/// </summary>
public class AudioSystemExample : MonoBehaviour
{
    [Header("音频组件")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private AudioClip[] audioClips;
    
    [Header("音频混合器")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup[] mixerGroups;
    
    [Header("音频设置")]
    [SerializeField] private float volume = 1.0f;
    [SerializeField] private float pitch = 1.0f;
    [SerializeField] private bool loop = false;
    [SerializeField] private AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;
    
    [Header("3D音频设置")]
    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 500.0f;
    [SerializeField] private float dopplerLevel = 1.0f;
    
    [Header("音频效果")]
    [SerializeField] private AudioReverbFilter reverbFilter;
    [SerializeField] private AudioEchoFilter echoFilter;
    [SerializeField] private AudioLowPassFilter lowPassFilter;
    [SerializeField] private AudioHighPassFilter highPassFilter;
    [SerializeField] private AudioDistortionFilter distortionFilter;
    [SerializeField] private AudioChorusFilter chorusFilter;
    
    private int currentClipIndex = 0;
    private bool isPlaying = false;
    
    private void Start()
    {
        InitializeAudioSystem();
    }
    
    /// <summary>
    /// 初始化音频系统
    /// </summary>
    private void InitializeAudioSystem()
    {
        // 获取或创建AudioSource组件
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // 获取或创建AudioListener组件
        if (audioListener == null)
        {
            audioListener = FindObjectOfType<AudioListener>();
            if (audioListener == null)
            {
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    audioListener = mainCamera.gameObject.AddComponent<AudioListener>();
                }
            }
        }
        
        // 设置音频源属性
        ConfigureAudioSource();
        
        // 添加音频效果组件
        AddAudioEffects();
    }
    
    /// <summary>
    /// 配置AudioSource属性
    /// </summary>
    private void ConfigureAudioSource()
    {
        if (audioSource == null) return;
        
        // 基础设置
        audioSource.volume = volume; // 音量
        audioSource.pitch = pitch; // 音调
        audioSource.loop = loop; // 是否循环
        audioSource.playOnAwake = false; // 是否在Awake时播放
        
        // 3D音频设置
        audioSource.rolloffMode = rolloffMode; // 衰减模式
        audioSource.minDistance = minDistance; // 最小距离
        audioSource.maxDistance = maxDistance; // 最大距离
        audioSource.dopplerLevel = dopplerLevel; // 多普勒效应
        
        // 空间混合设置
        audioSource.spatialBlend = 1.0f; // 空间混合
        
        // 设置音频混合器组
        if (mixerGroups != null && mixerGroups.Length > 0)
        {
            audioSource.outputAudioMixerGroup = mixerGroups[0]; // 输出音频混合器组
        }
    }
    
    /// <summary>
    /// 添加音频效果组件
    /// </summary>
    private void AddAudioEffects()
    {
        // 混响效果
        if (reverbFilter == null)
        {
            reverbFilter = gameObject.AddComponent<AudioReverbFilter>(); // 添加混响效果组件
            reverbFilter.reverbPreset = AudioReverbPreset.Cave; // 混响预设
            reverbFilter.dryLevel = 0.0f; // 干声电平
            reverbFilter.roomLevel = 0.0f; // 房间电平
            reverbFilter.roomHFLevel = 0.0f; // 高频房间电平
            reverbFilter.roomRolloffFactor = 10.0f; // 房间衰减因子
            reverbFilter.decayTime = 1.49f; // 衰减时间
            reverbFilter.decayHFRatio = 0.83f; // 高频衰减比率
            reverbFilter.reflectionsLevel = -2602.0f; // 反射电平
            reverbFilter.reflectionsDelay = 0.007f; // 反射延迟
            reverbFilter.reverbLevel = 200.0f; // 混响电平
            reverbFilter.reverbDelay = 0.011f; // 混响延迟
            reverbFilter.hfReference = 5000.0f; // 高频参考
            reverbFilter.roomLF = 0.0f; // 低频房间电平
            reverbFilter.lfReference = 250.0f; // 低频参考
        }
        
        // 回声效果
        if (echoFilter == null)
        {
            echoFilter = gameObject.AddComponent<AudioEchoFilter>(); // 添加回声效果组件
            echoFilter.delay = 500.0f; // 延迟
            echoFilter.decayRatio = 0.5f; // 衰减比率
            echoFilter.wetMix = 1.0f; // 湿声电平
            echoFilter.dryMix = 1.0f; // 干声电平
        }
        
        // 低通滤波器
        if (lowPassFilter == null)
        {
            lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>(); // 添加低通滤波器组件
            lowPassFilter.cutoffFrequency = 5000.0f; // 截止频率
            lowPassFilter.lowpassResonanceQ = 1.0f; // 低通共振
        }
        
        // 高通滤波器
        if (highPassFilter == null)
        {
            highPassFilter = gameObject.AddComponent<AudioHighPassFilter>(); // 添加高通滤波器组件
            highPassFilter.cutoffFrequency = 1000.0f; // 截止频率
            highPassFilter.highpassResonanceQ = 1.0f; // 高通共振
        }
        
        // 失真效果
        if (distortionFilter == null)
        {
            distortionFilter = gameObject.AddComponent<AudioDistortionFilter>(); // 添加失真效果组件
            distortionFilter.distortionLevel = 0.5f; // 失真电平
        }
        
        // 合唱效果
        if (chorusFilter == null)
        {
            chorusFilter = gameObject.AddComponent<AudioChorusFilter>(); // 添加合唱效果组件
            chorusFilter.dryMix = 0.5f; // 干声电平
            chorusFilter.wetMix1 = 0.5f; // 湿声电平
            chorusFilter.wetMix2 = 0.5f; // 湿声电平
            chorusFilter.wetMix3 = 0.5f; // 湿声电平
            chorusFilter.delay = 40.0f; // 延迟
            chorusFilter.rate = 0.8f; // 速率
            chorusFilter.depth = 0.03f; // 深度
        }
    }
    
    /// <summary>
    /// 播放音频
    /// </summary>
    public void PlayAudio()
    {
        if (audioSource == null || audioClips == null || audioClips.Length == 0) return;
        
        if (currentClipIndex >= audioClips.Length)
        {
            currentClipIndex = 0;
        }
        
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();
        isPlaying = true;
        
        Debug.Log($"播放音频: {audioClips[currentClipIndex].name}");
    }
    
    /// <summary>
    /// 停止音频
    /// </summary>
    public void StopAudio()
    {
        if (audioSource == null) return;
        
        audioSource.Stop();
        isPlaying = false;
        Debug.Log("停止音频播放");
    }
    
    /// <summary>
    /// 暂停音频
    /// </summary>
    public void PauseAudio()
    {
        if (audioSource == null) return;
        
        audioSource.Pause();
        isPlaying = false;
        Debug.Log("暂停音频播放");
    }
    
    /// <summary>
    /// 恢复音频
    /// </summary>
    public void ResumeAudio()
    {
        if (audioSource == null) return;
        
        audioSource.UnPause();
        isPlaying = true;
        Debug.Log("恢复音频播放");
    }
    
    /// <summary>
    /// 切换到下一个音频片段
    /// </summary>
    public void NextClip()
    {
        if (audioClips == null || audioClips.Length == 0) return;
        
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        
        if (isPlaying)
        {
            PlayAudio(); // 重新播放新片段
        }
        
        Debug.Log($"切换到音频片段: {audioClips[currentClipIndex].name}");
    }
    
    /// <summary>
    /// 切换到上一个音频片段
    /// </summary>
    public void PreviousClip()
    {
        if (audioClips == null || audioClips.Length == 0) return;
        
        currentClipIndex = (currentClipIndex - 1 + audioClips.Length) % audioClips.Length;
        
        if (isPlaying)
        {
            PlayAudio(); // 重新播放新片段
        }
        
        Debug.Log($"切换到音频片段: {audioClips[currentClipIndex].name}");
    }
    
    /// <summary>
    /// 设置音量
    /// </summary>
    /// <param name="newVolume">新音量值 (0-1)</param>
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
        Debug.Log($"设置音量: {volume}");
    }
    
    /// <summary>
    /// 设置音调
    /// </summary>
    /// <param name="newPitch">新音调值</param>
    public void SetPitch(float newPitch)
    {
        pitch = Mathf.Clamp(newPitch, -3.0f, 3.0f);
        if (audioSource != null)
        {
            audioSource.pitch = pitch;
        }
        Debug.Log($"设置音调: {pitch}");
    }
    
    /// <summary>
    /// 设置音频混合器参数
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <param name="value">参数值</param>
    public void SetMixerParameter(string parameterName, float value)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat(parameterName, value);
            Debug.Log($"设置混合器参数 {parameterName}: {value}");
        }
    }
    
    /// <summary>
    /// 获取音频混合器参数
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <returns>参数值</returns>
    public float GetMixerParameter(string parameterName)
    {
        if (audioMixer != null)
        {
            float value;
            if (audioMixer.GetFloat(parameterName, out value))
            {
                return value;
            }
        }
        return 0.0f;
    }
    
    /// <summary>
    /// 播放一次性音频（不循环）
    /// </summary>
    /// <param name="clip">音频片段</param>
    /// <param name="position">播放位置</param>
    public void PlayOneShot(AudioClip clip, Vector3 position)
    {
        if (audioSource == null || clip == null) return;
        
        AudioSource.PlayClipAtPoint(clip, position);
        Debug.Log($"在位置 {position} 播放一次性音频: {clip.name}");
    }
    
    /// <summary>
    /// 获取音频时间信息
    /// </summary>
    public void GetAudioTimeInfo()
    {
        if (audioSource == null || audioSource.clip == null) return;
        
        float currentTime = audioSource.time;
        float totalTime = audioSource.clip.length;
        float timeRemaining = totalTime - currentTime;
        
        Debug.Log($"音频时间信息 - 当前: {currentTime:F2}s, 总时长: {totalTime:F2}s, 剩余: {timeRemaining:F2}s");
    }
    
    /// <summary>
    /// 设置音频时间
    /// </summary>
    /// <param name="time">时间位置（秒）</param>
    public void SetAudioTime(float time)
    {
        if (audioSource == null || audioSource.clip == null) return;
        
        time = Mathf.Clamp(time, 0.0f, audioSource.clip.length);
        audioSource.time = time;
        Debug.Log($"设置音频时间: {time:F2}s");
    }
    
    private void Update()
    {
        // 更新音频状态
        if (audioSource != null && audioSource.clip != null)
        {
            isPlaying = audioSource.isPlaying;
        }
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 300, 400));
        GUILayout.Label("音频系统控制", EditorStyles.boldLabel);
        
        if (GUILayout.Button("播放"))
        {
            PlayAudio();
        }
        
        if (GUILayout.Button("停止"))
        {
            StopAudio();
        }
        
        if (GUILayout.Button("暂停"))
        {
            PauseAudio();
        }
        
        if (GUILayout.Button("恢复"))
        {
            ResumeAudio();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("下一个"))
        {
            NextClip();
        }
        
        if (GUILayout.Button("上一个"))
        {
            PreviousClip();
        }
        
        GUILayout.Space(10);
        
        GUILayout.Label($"音量: {volume:F2}");
        volume = GUILayout.HorizontalSlider(volume, 0.0f, 1.0f);
        
        GUILayout.Label($"音调: {pitch:F2}");
        pitch = GUILayout.HorizontalSlider(pitch, -3.0f, 3.0f);
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取时间信息"))
        {
            GetAudioTimeInfo();
        }
        
        GUILayout.EndArea();
    }
} 