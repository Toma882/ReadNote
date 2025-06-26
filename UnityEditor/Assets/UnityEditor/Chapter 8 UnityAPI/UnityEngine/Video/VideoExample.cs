using UnityEngine;
using UnityEngine.Video;
using System.Collections;

namespace UnityEditor.Chapter8.Video
{
    /// <summary>
    /// UnityEngine.Video 视频系统案例
    /// 演示VideoPlayer、视频播放控制、视频纹理等功能
    /// </summary>
    public class VideoExample : MonoBehaviour
    {
        [Header("视频播放器设置")]
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private RenderTexture renderTexture;
        [SerializeField] private Material videoMaterial;
        
        [Header("视频源")]
        [SerializeField] private VideoClip videoClip;
        [SerializeField] private string videoURL = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
        
        [Header("播放控制")]
        [SerializeField] private bool autoPlay = true;
        [SerializeField] private bool loop = true;
        [SerializeField] private float playbackSpeed = 1.0f;
        
        [Header("音频设置")]
        [SerializeField] private bool enableAudio = true;
        [SerializeField] private float audioVolume = 1.0f;
        
        [Header("UI控制")]
        [SerializeField] private bool showControls = true;
        
        private bool isPlaying = false;
        private bool isPaused = false;
        private double currentTime = 0;
        private double totalTime = 0;
        
        private void Start()
        {
            InitializeVideoPlayer();
        }
        
        /// <summary>
        /// 初始化视频播放器
        /// </summary>
        private void InitializeVideoPlayer()
        {
            if (videoPlayer == null)
            {
                videoPlayer = gameObject.AddComponent<VideoPlayer>();
            }
            
            // 设置视频播放器属性
            videoPlayer.playOnAwake = autoPlay;
            videoPlayer.isLooping = loop;
            videoPlayer.playbackSpeed = playbackSpeed;
            
            // 设置音频
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
            videoPlayer.audioVolume = audioVolume;
            
            // 设置渲染目标
            if (renderTexture != null)
            {
                videoPlayer.targetTexture = renderTexture;
            }
            
            // 设置视频源
            if (videoClip != null)
            {
                videoPlayer.clip = videoClip;
            }
            else if (!string.IsNullOrEmpty(videoURL))
            {
                videoPlayer.url = videoURL;
            }
            
            // 注册事件
            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.loopPointReached += OnVideoLoopPointReached;
            videoPlayer.errorReceived += OnVideoError;
            
            // 准备视频
            videoPlayer.Prepare();
        }
        
        /// <summary>
        /// 视频准备完成回调
        /// </summary>
        private void OnVideoPrepared(VideoPlayer source)
        {
            Debug.Log("视频准备完成");
            totalTime = source.length;
            
            if (autoPlay)
            {
                PlayVideo();
            }
        }
        
        /// <summary>
        /// 视频循环点到达回调
        /// </summary>
        private void OnVideoLoopPointReached(VideoPlayer source)
        {
            Debug.Log("视频播放完成，准备循环");
        }
        
        /// <summary>
        /// 视频错误回调
        /// </summary>
        private void OnVideoError(VideoPlayer source, string message)
        {
            Debug.LogError($"视频播放错误: {message}");
        }
        
        /// <summary>
        /// 播放视频
        /// </summary>
        public void PlayVideo()
        {
            if (videoPlayer.isPrepared)
            {
                videoPlayer.Play();
                isPlaying = true;
                isPaused = false;
                Debug.Log("开始播放视频");
            }
        }
        
        /// <summary>
        /// 暂停视频
        /// </summary>
        public void PauseVideo()
        {
            if (isPlaying)
            {
                videoPlayer.Pause();
                isPaused = true;
                Debug.Log("视频已暂停");
            }
        }
        
        /// <summary>
        /// 恢复播放
        /// </summary>
        public void ResumeVideo()
        {
            if (isPaused)
            {
                videoPlayer.Play();
                isPaused = false;
                Debug.Log("视频已恢复播放");
            }
        }
        
        /// <summary>
        /// 停止视频
        /// </summary>
        public void StopVideo()
        {
            videoPlayer.Stop();
            isPlaying = false;
            isPaused = false;
            Debug.Log("视频已停止");
        }
        
        /// <summary>
        /// 跳转到指定时间
        /// </summary>
        public void SeekToTime(double timeInSeconds)
        {
            if (videoPlayer.isPrepared)
            {
                videoPlayer.time = Mathf.Clamp((float)timeInSeconds, 0, (float)totalTime);
                Debug.Log($"跳转到时间: {timeInSeconds}秒");
            }
        }
        
        /// <summary>
        /// 设置播放速度
        /// </summary>
        public void SetPlaybackSpeed(float speed)
        {
            playbackSpeed = Mathf.Clamp(speed, 0.1f, 3.0f);
            videoPlayer.playbackSpeed = playbackSpeed;
            Debug.Log($"播放速度设置为: {playbackSpeed}x");
        }
        
        /// <summary>
        /// 设置音频音量
        /// </summary>
        public void SetAudioVolume(float volume)
        {
            audioVolume = Mathf.Clamp01(volume);
            videoPlayer.audioVolume = audioVolume;
            Debug.Log($"音频音量设置为: {audioVolume}");
        }
        
        /// <summary>
        /// 切换循环播放
        /// </summary>
        public void ToggleLoop()
        {
            loop = !loop;
            videoPlayer.isLooping = loop;
            Debug.Log($"循环播放: {(loop ? "开启" : "关闭")}");
        }
        
        /// <summary>
        /// 获取当前播放时间
        /// </summary>
        public double GetCurrentTime()
        {
            return videoPlayer.time;
        }
        
        /// <summary>
        /// 获取视频总时长
        /// </summary>
        public double GetTotalTime()
        {
            return videoPlayer.length;
        }
        
        /// <summary>
        /// 获取播放进度百分比
        /// </summary>
        public float GetProgress()
        {
            if (totalTime > 0)
            {
                return (float)(currentTime / totalTime);
            }
            return 0f;
        }
        
        /// <summary>
        /// 设置视频纹理到材质
        /// </summary>
        public void SetVideoTextureToMaterial()
        {
            if (videoMaterial != null && renderTexture != null)
            {
                videoMaterial.mainTexture = renderTexture;
                Debug.Log("视频纹理已设置到材质");
            }
        }
        
        /// <summary>
        /// 创建视频纹理
        /// </summary>
        public void CreateVideoTexture(int width = 1920, int height = 1080)
        {
            renderTexture = new RenderTexture(width, height, 0);
            renderTexture.Create();
            videoPlayer.targetTexture = renderTexture;
            Debug.Log($"创建视频纹理: {width}x{height}");
        }
        
        /// <summary>
        /// 截图功能
        /// </summary>
        public void TakeScreenshot()
        {
            if (renderTexture != null)
            {
                StartCoroutine(CaptureScreenshot());
            }
        }
        
        private IEnumerator CaptureScreenshot()
        {
            yield return new WaitForEndOfFrame();
            
            RenderTexture.active = renderTexture;
            Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            screenshot.Apply();
            RenderTexture.active = null;
            
            byte[] bytes = screenshot.EncodeToPNG();
            string filename = $"VideoScreenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + filename, bytes);
            
            Debug.Log($"截图已保存: {filename}");
            DestroyImmediate(screenshot);
        }
        
        private void Update()
        {
            if (videoPlayer.isPlaying)
            {
                currentTime = videoPlayer.time;
            }
        }
        
        private void OnGUI()
        {
            if (!showControls) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 400, 500));
            GUILayout.Label("UnityEngine.Video 视频系统案例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            // 播放控制
            GUILayout.Label("播放控制", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("播放")) PlayVideo();
            if (GUILayout.Button("暂停")) PauseVideo();
            if (GUILayout.Button("停止")) StopVideo();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(5);
            
            // 进度显示
            float progress = GetProgress();
            GUILayout.Label($"进度: {progress:P1} ({currentTime:F1}s / {totalTime:F1}s)");
            float newProgress = GUILayout.HorizontalSlider(progress, 0f, 1f);
            if (Mathf.Abs(newProgress - progress) > 0.01f)
            {
                SeekToTime(newProgress * totalTime);
            }
            
            GUILayout.Space(10);
            
            // 播放设置
            GUILayout.Label("播放设置", EditorStyles.boldLabel);
            
            float newSpeed = GUILayout.HorizontalSlider(playbackSpeed, 0.1f, 3.0f);
            if (Mathf.Abs(newSpeed - playbackSpeed) > 0.01f)
            {
                SetPlaybackSpeed(newSpeed);
            }
            GUILayout.Label($"播放速度: {playbackSpeed:F1}x");
            
            float newVolume = GUILayout.HorizontalSlider(audioVolume, 0f, 1f);
            if (Mathf.Abs(newVolume - audioVolume) > 0.01f)
            {
                SetAudioVolume(newVolume);
            }
            GUILayout.Label($"音频音量: {audioVolume:F2}");
            
            if (GUILayout.Button($"循环播放: {(loop ? "开启" : "关闭")}"))
            {
                ToggleLoop();
            }
            
            GUILayout.Space(10);
            
            // 功能按钮
            GUILayout.Label("功能", EditorStyles.boldLabel);
            if (GUILayout.Button("创建视频纹理"))
            {
                CreateVideoTexture();
            }
            
            if (GUILayout.Button("设置视频纹理到材质"))
            {
                SetVideoTextureToMaterial();
            }
            
            if (GUILayout.Button("截图"))
            {
                TakeScreenshot();
            }
            
            GUILayout.Space(10);
            
            // 状态信息
            GUILayout.Label("状态信息", EditorStyles.boldLabel);
            GUILayout.Label($"准备状态: {(videoPlayer.isPrepared ? "已准备" : "未准备")}");
            GUILayout.Label($"播放状态: {(isPlaying ? (isPaused ? "暂停" : "播放中") : "停止")}");
            GUILayout.Label($"视频尺寸: {videoPlayer.texture?.width ?? 0} x {videoPlayer.texture?.height ?? 0}");
            
            GUILayout.EndArea();
        }
        
        private void OnDestroy()
        {
            if (videoPlayer != null)
            {
                videoPlayer.prepareCompleted -= OnVideoPrepared;
                videoPlayer.loopPointReached -= OnVideoLoopPointReached;
                videoPlayer.errorReceived -= OnVideoError;
            }
            
            if (renderTexture != null)
            {
                renderTexture.Release();
            }
        }
    }
} 