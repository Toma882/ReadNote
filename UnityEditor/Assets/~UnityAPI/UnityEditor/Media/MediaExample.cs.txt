using UnityEngine;
using UnityEditor;
using UnityEditor.Media;
using System.Collections.Generic;

namespace UnityEditor.Media.Examples
{
    /// <summary>
    /// UnityEditor.Media 命名空间使用示例
    /// 演示媒体录制、编码和播放功能
    /// </summary>
    public class MediaExample : MonoBehaviour
    {
        [Header("媒体录制配置")]
        [SerializeField] private string outputPath = "Assets/Recordings/";
        [SerializeField] private string fileName = "recording.mp4";
        [SerializeField] private int width = 1920;
        [SerializeField] private int height = 1080;
        [SerializeField] private int frameRate = 30;
        [SerializeField] private int bitRate = 5000000; // 5Mbps
        
        [Header("录制状态")]
        [SerializeField] private bool isRecording = false;
        [SerializeField] private float recordingTime = 0f;
        [SerializeField] private string currentStatus = "就绪";
        
        [Header("媒体播放配置")]
        [SerializeField] private string mediaFilePath = "";
        [SerializeField] private bool isPlaying = false;
        [SerializeField] private float playbackTime = 0f;
        [SerializeField] private float totalDuration = 0f;
        
        private MediaEncoder encoder;
        private MediaPlayer player;
        private List<Texture2D> frameBuffer = new List<Texture2D>();
        private float lastFrameTime;
        
        /// <summary>
        /// 初始化媒体系统
        /// </summary>
        private void Start()
        {
            InitializeMediaSystem();
        }
        
        /// <summary>
        /// 初始化媒体录制和播放系统
        /// </summary>
        private void InitializeMediaSystem()
        {
            // 确保输出目录存在
            if (!System.IO.Directory.Exists(outputPath))
            {
                System.IO.Directory.CreateDirectory(outputPath);
            }
            
            // 初始化媒体播放器
            player = new MediaPlayer();
            
            currentStatus = "媒体系统已初始化";
            Debug.Log("媒体系统初始化完成");
        }
        
        /// <summary>
        /// 开始录制
        /// </summary>
        public void StartRecording()
        {
            if (isRecording)
            {
                Debug.LogWarning("已经在录制中");
                return;
            }
            
            string fullPath = System.IO.Path.Combine(outputPath, fileName);
            
            // 配置编码器参数
            var encoderParams = new MediaEncoder.EncoderParams
            {
                width = width,
                height = height,
                frameRate = frameRate,
                bitRate = bitRate,
                outputPath = fullPath
            };
            
            try
            {
                encoder = new MediaEncoder(encoderParams);
                isRecording = true;
                recordingTime = 0f;
                lastFrameTime = Time.time;
                currentStatus = "录制中...";
                
                Debug.Log($"开始录制到: {fullPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"录制失败: {e.Message}");
                currentStatus = "录制失败";
            }
        }
        
        /// <summary>
        /// 停止录制
        /// </summary>
        public void StopRecording()
        {
            if (!isRecording || encoder == null)
            {
                Debug.LogWarning("没有在录制");
                return;
            }
            
            try
            {
                encoder.Dispose();
                encoder = null;
                isRecording = false;
                currentStatus = "录制完成";
                
                Debug.Log("录制已停止");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"停止录制失败: {e.Message}");
                currentStatus = "停止录制失败";
            }
        }
        
        /// <summary>
        /// 添加帧到录制
        /// </summary>
        public void AddFrame(Texture2D frame)
        {
            if (!isRecording || encoder == null)
                return;
                
            try
            {
                encoder.AddFrame(frame);
                frameBuffer.Add(frame);
                
                // 限制缓冲区大小
                if (frameBuffer.Count > 300) // 10秒@30fps
                {
                    var oldFrame = frameBuffer[0];
                    frameBuffer.RemoveAt(0);
                    DestroyImmediate(oldFrame);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"添加帧失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 播放媒体文件
        /// </summary>
        public void PlayMedia(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogWarning("文件路径为空");
                return;
            }
            
            try
            {
                if (player != null)
                {
                    player.Dispose();
                }
                
                player = new MediaPlayer();
                player.Open(filePath);
                player.Play();
                
                mediaFilePath = filePath;
                isPlaying = true;
                currentStatus = "播放中";
                
                Debug.Log($"开始播放: {filePath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"播放失败: {e.Message}");
                currentStatus = "播放失败";
            }
        }
        
        /// <summary>
        /// 停止播放
        /// </summary>
        public void StopPlayback()
        {
            if (player != null)
            {
                player.Stop();
                isPlaying = false;
                currentStatus = "播放已停止";
            }
        }
        
        /// <summary>
        /// 暂停/恢复播放
        /// </summary>
        public void TogglePlayback()
        {
            if (player == null) return;
            
            if (isPlaying)
            {
                player.Pause();
                isPlaying = false;
                currentStatus = "已暂停";
            }
            else
            {
                player.Play();
                isPlaying = true;
                currentStatus = "播放中";
            }
        }
        
        /// <summary>
        /// 设置播放位置
        /// </summary>
        public void SetPlaybackTime(float time)
        {
            if (player != null)
            {
                player.SetTime(time);
                playbackTime = time;
            }
        }
        
        /// <summary>
        /// 获取媒体信息
        /// </summary>
        public MediaInfo GetMediaInfo(string filePath)
        {
            try
            {
                return MediaInfo.Get(filePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"获取媒体信息失败: {e.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 更新录制时间
        /// </summary>
        private void Update()
        {
            if (isRecording)
            {
                recordingTime += Time.deltaTime;
                
                // 每秒添加一帧（示例）
                if (Time.time - lastFrameTime >= 1f / frameRate)
                {
                    // 创建示例帧（实际应用中应该从相机或其他源获取）
                    Texture2D frame = CreateSampleFrame();
                    AddFrame(frame);
                    lastFrameTime = Time.time;
                }
            }
            
            if (isPlaying && player != null)
            {
                playbackTime = player.GetTime();
                totalDuration = player.GetDuration();
            }
        }
        
        /// <summary>
        /// 创建示例帧
        /// </summary>
        private Texture2D CreateSampleFrame()
        {
            Texture2D frame = new Texture2D(width, height);
            Color[] pixels = new Color[width * height];
            
            // 创建简单的渐变效果
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float r = (float)x / width;
                    float g = (float)y / height;
                    float b = Mathf.Sin(Time.time + x * 0.01f) * 0.5f + 0.5f;
                    pixels[y * width + x] = new Color(r, g, b, 1f);
                }
            }
            
            frame.SetPixels(pixels);
            frame.Apply();
            return frame;
        }
        
        /// <summary>
        /// 清理资源
        /// </summary>
        private void OnDestroy()
        {
            if (encoder != null)
            {
                encoder.Dispose();
            }
            
            if (player != null)
            {
                player.Dispose();
            }
            
            // 清理帧缓冲区
            foreach (var frame in frameBuffer)
            {
                if (frame != null)
                {
                    DestroyImmediate(frame);
                }
            }
            frameBuffer.Clear();
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 400, 600));
            GUILayout.Label("UnityEditor.Media 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {currentStatus}");
            
            GUILayout.Space(10);
            GUILayout.Label("录制控制", EditorStyles.boldLabel);
            
            if (!isRecording)
            {
                if (GUILayout.Button("开始录制"))
                {
                    StartRecording();
                }
            }
            else
            {
                GUILayout.Label($"录制时间: {recordingTime:F2}秒");
                if (GUILayout.Button("停止录制"))
                {
                    StopRecording();
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("播放控制", EditorStyles.boldLabel);
            
            mediaFilePath = GUILayout.TextField("媒体文件路径", mediaFilePath);
            
            if (GUILayout.Button("选择文件"))
            {
                string path = EditorUtility.OpenFilePanel("选择媒体文件", "", "mp4,avi,mov");
                if (!string.IsNullOrEmpty(path))
                {
                    mediaFilePath = path;
                }
            }
            
            if (GUILayout.Button("播放"))
            {
                PlayMedia(mediaFilePath);
            }
            
            if (isPlaying)
            {
                if (GUILayout.Button("暂停/恢复"))
                {
                    TogglePlayback();
                }
                
                if (GUILayout.Button("停止"))
                {
                    StopPlayback();
                }
                
                GUILayout.Label($"播放时间: {playbackTime:F2} / {totalDuration:F2}");
                
                // 播放进度条
                float progress = totalDuration > 0 ? playbackTime / totalDuration : 0;
                float newProgress = GUILayout.HorizontalSlider(progress, 0, 1);
                if (Mathf.Abs(newProgress - progress) > 0.01f)
                {
                    SetPlaybackTime(newProgress * totalDuration);
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            width = EditorGUILayout.IntField("宽度", width);
            height = EditorGUILayout.IntField("高度", height);
            frameRate = EditorGUILayout.IntField("帧率", frameRate);
            bitRate = EditorGUILayout.IntField("比特率", bitRate);
            
            GUILayout.EndArea();
        }
    }
} 