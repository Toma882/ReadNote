using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Audio 命名空间案例演示
/// 展示音频系统的核心功能
/// </summary>
public class AudioExample : MonoBehaviour
{
    [Header("音频组件")]
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private AudioSource[] additionalAudioSources;
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private AudioClip[] audioClips;
    
    [Header("音频设置")]
    [SerializeField] private bool enableAudio = true;
    [SerializeField] private float masterVolume = 1.0f;
    [SerializeField] private float musicVolume = 0.8f;
    [SerializeField] private float sfxVolume = 1.0f;
    [SerializeField] private bool enable3DAudio = true;
    [SerializeField] private float spatialBlend = 1.0f;
    
    [Header("音频播放")]
    [SerializeField] private bool isPlaying = false;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float totalTime = 0f;
    [SerializeField] private int currentClipIndex = 0;
    [SerializeField] private AudioClip currentClip;
    
    [Header("音频效果")]
    [SerializeField] private bool enableReverb = false;
    [SerializeField] private bool enableEcho = false;
    [SerializeField] private bool enableChorus = false;
    [SerializeField] private bool enableDistortion = false;
    [SerializeField] private bool enableLowPass = false;
    [SerializeField] private bool enableHighPass = false;
    
    [Header("音频分析")]
    [SerializeField] private float[] spectrumData = new float[256];
    [SerializeField] private float[] samples = new float[256];
    [SerializeField] private float averageVolume = 0f;
    [SerializeField] private float peakVolume = 0f;
    
    // 音频事件
    private System.Action<AudioClip> onAudioStart;
    private System.Action<AudioClip> onAudioEnd;
    private System.Action<AudioClip> onAudioPause;
    private System.Action<float> onVolumeChanged;
    
    private void Start()
    {
        InitializeAudioSystem();
    }
    
    /// <summary>
    /// 初始化音频系统
    /// </summary>
    private void InitializeAudioSystem()
    {
        // 获取或创建主音频源
        if (mainAudioSource == null)
        {
            mainAudioSource = GetComponent<AudioSource>();
            if (mainAudioSource == null)
            {
                mainAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // 获取或创建音频监听器
        if (audioListener == null)
        {
            audioListener = FindObjectOfType<AudioListener>();
            if (audioListener == null)
            {
                audioListener = Camera.main?.GetComponent<AudioListener>();
                if (audioListener == null)
                {
                    audioListener = Camera.main?.gameObject.AddComponent<AudioListener>();
                }
            }
        }
        
        // 配置音频组件
        ConfigureAudioComponents();
        
        // 设置音频事件
        SetupAudioEvents();
        
        // 创建额外的音频源
        CreateAdditionalAudioSources();
        
        Debug.Log("音频系统初始化完成");
    }
    
    /// <summary>
    /// 配置音频组件
    /// </summary>
    private void ConfigureAudioComponents()
    {
        if (mainAudioSource != null)
        {
            // 设置主音频源参数
            mainAudioSource.playOnAwake = false;
            mainAudioSource.loop = false;
            mainAudioSource.volume = musicVolume;
            mainAudioSource.spatialBlend = spatialBlend;
            mainAudioSource.dopplerLevel = 1f;
            mainAudioSource.rolloffMode = AudioRolloffMode.Linear;
            mainAudioSource.minDistance = 1f;
            mainAudioSource.maxDistance = 500f;
            
            // 设置3D音频
            if (enable3DAudio)
            {
                mainAudioSource.spatialBlend = 1f;
            }
            else
            {
                mainAudioSource.spatialBlend = 0f;
            }
        }
        
        Debug.Log("音频组件配置完成");
    }
    
    /// <summary>
    /// 设置音频事件
    /// </summary>
    private void SetupAudioEvents()
    {
        if (mainAudioSource != null)
        {
            // 监听音频播放状态
            StartCoroutine(MonitorAudioState());
        }
        
        Debug.Log("音频事件设置完成");
    }
    
    /// <summary>
    /// 监控音频状态
    /// </summary>
    private System.Collections.IEnumerator MonitorAudioState()
    {
        AudioClip previousClip = null;
        
        while (true)
        {
            if (mainAudioSource != null)
            {
                // 更新播放状态
                isPlaying = mainAudioSource.isPlaying;
                isPaused = mainAudioSource.time > 0 && !mainAudioSource.isPlaying;
                
                // 更新时间信息
                if (mainAudioSource.clip != null)
                {
                    currentTime = mainAudioSource.time;
                    totalTime = mainAudioSource.clip.length;
                    currentClip = mainAudioSource.clip;
                }
                
                // 检测音频开始
                if (mainAudioSource.clip != previousClip && mainAudioSource.isPlaying)
                {
                    onAudioStart?.Invoke(mainAudioSource.clip);
                    previousClip = mainAudioSource.clip;
                }
                
                // 检测音频结束
                if (previousClip != null && !mainAudioSource.isPlaying && mainAudioSource.time == 0)
                {
                    onAudioEnd?.Invoke(previousClip);
                    previousClip = null;
                }
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    /// <summary>
    /// 创建额外的音频源
    /// </summary>
    private void CreateAdditionalAudioSources()
    {
        if (additionalAudioSources == null || additionalAudioSources.Length == 0)
        {
            // 创建SFX音频源
            GameObject sfxObj = new GameObject("SFXAudioSource");
            sfxObj.transform.SetParent(transform);
            sfxObj.transform.localPosition = Vector3.zero;
            
            var sfxSource = sfxObj.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.loop = false;
            sfxSource.volume = sfxVolume;
            sfxSource.spatialBlend = 0f; // 2D音效
            
            // 创建环境音频源
            GameObject ambientObj = new GameObject("AmbientAudioSource");
            ambientObj.transform.SetParent(transform);
            ambientObj.transform.localPosition = Vector3.zero;
            
            var ambientSource = ambientObj.AddComponent<AudioSource>();
            ambientSource.playOnAwake = false;
            ambientSource.loop = true;
            ambientSource.volume = 0.5f;
            ambientSource.spatialBlend = 0f; // 2D环境音
            
            additionalAudioSources = new AudioSource[] { sfxSource, ambientSource };
        }
        
        Debug.Log($"创建了 {additionalAudioSources.Length} 个额外音频源");
    }
    
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="clipIndex">音频片段索引</param>
    public void PlayAudio(int clipIndex)
    {
        if (audioClips == null || clipIndex < 0 || clipIndex >= audioClips.Length)
        {
            Debug.LogError("无效的音频片段索引");
            return;
        }
        
        AudioClip clip = audioClips[clipIndex];
        if (clip != null && mainAudioSource != null)
        {
            mainAudioSource.clip = clip;
            mainAudioSource.Play();
            currentClipIndex = clipIndex;
            currentClip = clip;
            isPlaying = true;
            isPaused = false;
            
            Debug.Log($"播放音频: {clip.name}");
        }
    }
    
    /// <summary>
    /// 播放音频片段
    /// </summary>
    /// <param name="clip">音频片段</param>
    public void PlayAudio(AudioClip clip)
    {
        if (clip != null && mainAudioSource != null)
        {
            mainAudioSource.clip = clip;
            mainAudioSource.Play();
            currentClip = clip;
            isPlaying = true;
            isPaused = false;
            
            Debug.Log($"播放音频: {clip.name}");
        }
    }
    
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">音效片段</param>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && additionalAudioSources != null && additionalAudioSources.Length > 0)
        {
            additionalAudioSources[0].PlayOneShot(clip, sfxVolume);
            Debug.Log($"播放音效: {clip.name}");
        }
    }
    
    /// <summary>
    /// 播放环境音
    /// </summary>
    /// <param name="clip">环境音片段</param>
    public void PlayAmbient(AudioClip clip)
    {
        if (clip != null && additionalAudioSources != null && additionalAudioSources.Length > 1)
        {
            additionalAudioSources[1].clip = clip;
            additionalAudioSources[1].Play();
            Debug.Log($"播放环境音: {clip.name}");
        }
    }
    
    /// <summary>
    /// 暂停音频
    /// </summary>
    public void PauseAudio()
    {
        if (mainAudioSource != null)
        {
            mainAudioSource.Pause();
            isPaused = true;
        }
        
        // 暂停所有音频源
        foreach (var source in additionalAudioSources)
        {
            if (source != null)
            {
                source.Pause();
            }
        }
        
        Debug.Log("音频已暂停");
        onAudioPause?.Invoke(currentClip);
    }
    
    /// <summary>
    /// 恢复音频
    /// </summary>
    public void ResumeAudio()
    {
        if (mainAudioSource != null)
        {
            mainAudioSource.UnPause();
            isPaused = false;
        }
        
        // 恢复所有音频源
        foreach (var source in additionalAudioSources)
        {
            if (source != null)
            {
                source.UnPause();
            }
        }
        
        Debug.Log("音频已恢复");
    }
    
    /// <summary>
    /// 停止音频
    /// </summary>
    public void StopAudio()
    {
        if (mainAudioSource != null)
        {
            mainAudioSource.Stop();
            isPlaying = false;
            isPaused = false;
            currentTime = 0f;
        }
        
        // 停止所有音频源
        foreach (var source in additionalAudioSources)
        {
            if (source != null)
            {
                source.Stop();
            }
        }
        
        Debug.Log("音频已停止");
    }
    
    /// <summary>
    /// 设置主音量
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        AudioListener.volume = masterVolume;
        
        Debug.Log($"主音量已设置为: {masterVolume}");
        onVolumeChanged?.Invoke(masterVolume);
    }
    
    /// <summary>
    /// 设置音乐音量
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        
        if (mainAudioSource != null)
        {
            mainAudioSource.volume = musicVolume;
        }
        
        Debug.Log($"音乐音量已设置为: {musicVolume}");
    }
    
    /// <summary>
    /// 设置音效音量
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        
        if (additionalAudioSources != null && additionalAudioSources.Length > 0)
        {
            additionalAudioSources[0].volume = sfxVolume;
        }
        
        Debug.Log($"音效音量已设置为: {sfxVolume}");
    }
    
    /// <summary>
    /// 设置3D音频
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void Set3DAudio(bool enable)
    {
        enable3DAudio = enable;
        
        if (mainAudioSource != null)
        {
            mainAudioSource.spatialBlend = enable ? 1f : 0f;
        }
        
        Debug.Log($"3D音频已设置为: {enable}");
    }
    
    /// <summary>
    /// 设置音频位置
    /// </summary>
    /// <param name="position">位置</param>
    public void SetAudioPosition(Vector3 position)
    {
        if (mainAudioSource != null)
        {
            mainAudioSource.transform.position = position;
        }
        
        Debug.Log($"音频位置已设置为: {position}");
    }
    
    /// <summary>
    /// 获取音频频谱数据
    /// </summary>
    /// <param name="channel">声道</param>
    /// <returns>频谱数据</returns>
    public float[] GetSpectrumData(int channel = 0)
    {
        if (mainAudioSource != null)
        {
            mainAudioSource.GetSpectrumData(spectrumData, channel, FFTWindow.BlackmanHarris);
        }
        
        return spectrumData;
    }
    
    /// <summary>
    /// 获取音频采样数据
    /// </summary>
    /// <param name="channel">声道</param>
    /// <returns>采样数据</returns>
    public float[] GetOutputData(int channel = 0)
    {
        if (mainAudioSource != null)
        {
            mainAudioSource.GetOutputData(samples, channel);
        }
        
        return samples;
    }
    
    /// <summary>
    /// 计算音频分析数据
    /// </summary>
    private void CalculateAudioAnalysis()
    {
        if (mainAudioSource != null && mainAudioSource.isPlaying)
        {
            // 获取频谱数据
            GetSpectrumData();
            
            // 计算平均音量
            float sum = 0f;
            for (int i = 0; i < spectrumData.Length; i++)
            {
                sum += spectrumData[i];
            }
            averageVolume = sum / spectrumData.Length;
            
            // 计算峰值音量
            peakVolume = 0f;
            for (int i = 0; i < spectrumData.Length; i++)
            {
                if (spectrumData[i] > peakVolume)
                {
                    peakVolume = spectrumData[i];
                }
            }
        }
    }
    
    /// <summary>
    /// 播放下一个音频
    /// </summary>
    public void PlayNextAudio()
    {
        if (audioClips != null && audioClips.Length > 0)
        {
            int nextIndex = (currentClipIndex + 1) % audioClips.Length;
            PlayAudio(nextIndex);
        }
    }
    
    /// <summary>
    /// 播放上一个音频
    /// </summary>
    public void PlayPreviousAudio()
    {
        if (audioClips != null && audioClips.Length > 0)
        {
            int prevIndex = (currentClipIndex - 1 + audioClips.Length) % audioClips.Length;
            PlayAudio(prevIndex);
        }
    }
    
    /// <summary>
    /// 获取音频信息
    /// </summary>
    public void GetAudioInfo()
    {
        Debug.Log("=== 音频系统信息 ===");
        Debug.Log($"音频启用: {enableAudio}");
        Debug.Log($"播放状态: {(isPlaying ? (isPaused ? "暂停" : "播放中") : "停止")}");
        Debug.Log($"当前时间: {currentTime:F2}s / {totalTime:F2}s");
        Debug.Log($"当前片段: {(currentClip != null ? currentClip.name : "无")}");
        Debug.Log($"主音量: {masterVolume}");
        Debug.Log($"音乐音量: {musicVolume}");
        Debug.Log($"音效音量: {sfxVolume}");
        Debug.Log($"3D音频: {enable3DAudio}");
        Debug.Log($"空间混合: {spatialBlend}");
        
        if (mainAudioSource != null)
        {
            Debug.Log($"主音频源: 已配置");
            Debug.Log($"  播放中: {mainAudioSource.isPlaying}");
            Debug.Log($"  音量: {mainAudioSource.volume}");
            Debug.Log($"  音调: {mainAudioSource.pitch}");
            Debug.Log($"  循环: {mainAudioSource.loop}");
            Debug.Log($"  空间混合: {mainAudioSource.spatialBlend}");
        }
        
        if (audioClips != null)
        {
            Debug.Log($"音频片段数量: {audioClips.Length}");
            for (int i = 0; i < audioClips.Length; i++)
            {
                if (audioClips[i] != null)
                {
                    Debug.Log($"  片段 {i}: {audioClips[i].name} ({audioClips[i].length:F2}s)");
                }
            }
        }
        
        Debug.Log($"平均音量: {averageVolume:F4}");
        Debug.Log($"峰值音量: {peakVolume:F4}");
    }
    
    /// <summary>
    /// 重置音频设置
    /// </summary>
    public void ResetAudioSettings()
    {
        // 重置音量设置
        masterVolume = 1.0f;
        musicVolume = 0.8f;
        sfxVolume = 1.0f;
        
        // 重置音频设置
        enable3DAudio = true;
        spatialBlend = 1.0f;
        
        // 应用设置
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
        Set3DAudio(enable3DAudio);
        
        Debug.Log("音频设置已重置");
    }
    
    private void Update()
    {
        // 计算音频分析数据
        CalculateAudioAnalysis();
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 700));
        GUILayout.Label("音频系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 音频状态
        GUILayout.Label($"播放状态: {(isPlaying ? (isPaused ? "暂停" : "播放中") : "停止")}");
        GUILayout.Label($"当前时间: {currentTime:F2}s / {totalTime:F2}s");
        GUILayout.Label($"当前片段: {(currentClip != null ? currentClip.name : "无")}");
        GUILayout.Label($"平均音量: {averageVolume:F4}");
        GUILayout.Label($"峰值音量: {peakVolume:F4}");
        
        GUILayout.Space(10);
        
        // 播放控制
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("播放"))
        {
            if (audioClips != null && audioClips.Length > 0)
            {
                PlayAudio(currentClipIndex);
            }
        }
        if (GUILayout.Button("暂停"))
        {
            PauseAudio();
        }
        if (GUILayout.Button("停止"))
        {
            StopAudio();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("上一个"))
        {
            PlayPreviousAudio();
        }
        if (GUILayout.Button("下一个"))
        {
            PlayNextAudio();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        
        // 音量控制
        GUILayout.Label("主音量:");
        masterVolume = GUILayout.HorizontalSlider(masterVolume, 0f, 1f);
        if (GUILayout.Button("设置主音量"))
        {
            SetMasterVolume(masterVolume);
        }
        
        GUILayout.Label("音乐音量:");
        musicVolume = GUILayout.HorizontalSlider(musicVolume, 0f, 1f);
        if (GUILayout.Button("设置音乐音量"))
        {
            SetMusicVolume(musicVolume);
        }
        
        GUILayout.Label("音效音量:");
        sfxVolume = GUILayout.HorizontalSlider(sfxVolume, 0f, 1f);
        if (GUILayout.Button("设置音效音量"))
        {
            SetSFXVolume(sfxVolume);
        }
        
        GUILayout.Space(10);
        
        // 3D音频设置
        enable3DAudio = GUILayout.Toggle(enable3DAudio, "3D音频");
        if (GUILayout.Button("设置3D音频"))
        {
            Set3DAudio(enable3DAudio);
        }
        
        GUILayout.Label("空间混合:");
        spatialBlend = GUILayout.HorizontalSlider(spatialBlend, 0f, 1f);
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取音频信息"))
        {
            GetAudioInfo();
        }
        
        if (GUILayout.Button("重置设置"))
        {
            ResetAudioSettings();
        }
        
        GUILayout.EndArea();
    }
} 