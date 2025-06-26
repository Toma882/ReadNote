using UnityEngine;
using UnityEditor;
using UnityEditor.Playables;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;

namespace UnityEditor.Playables.Examples
{
    /// <summary>
    /// UnityEditor.Playables 命名空间使用示例
    /// 演示可播放编辑器系统的创建、编辑和管理功能
    /// </summary>
    public class PlayablesExample : MonoBehaviour
    {
        [Header("可播放配置")]
        [SerializeField] private bool enablePlayables = true;
        [SerializeField] private string timelineName = "CustomTimeline";
        [SerializeField] private float timelineDuration = 10f;
        [SerializeField] private bool autoPlay = false;
        
        [Header("可播放状态")]
        [SerializeField] private bool isPlaying = false;
        [SerializeField] private float currentTime = 0f;
        [SerializeField] private float totalDuration = 0f;
        [SerializeField] private string currentTrack = "";
        
        [Header("目标对象")]
        [SerializeField] private GameObject targetObject;
        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private TimelineAsset timelineAsset;
        
        [Header("轨道数据")]
        [SerializeField] private List<TrackAsset> availableTracks = new List<TrackAsset>();
        [SerializeField] private TrackAsset currentTrackAsset;
        
        private PlayableGraph playableGraph;
        private Dictionary<string, Playable> playableRegistry = new Dictionary<string, Playable>();
        
        /// <summary>
        /// 初始化可播放系统
        /// </summary>
        private void Start()
        {
            InitializePlayablesSystem();
        }
        
        /// <summary>
        /// 初始化可播放系统
        /// </summary>
        private void InitializePlayablesSystem()
        {
            if (!enablePlayables)
            {
                Debug.Log("可播放系统已禁用");
                return;
            }
            
            try
            {
                // 创建PlayableDirector
                if (playableDirector == null)
                {
                    playableDirector = gameObject.AddComponent<PlayableDirector>();
                }
                
                // 创建时间轴资源
                CreateTimelineAsset();
                
                // 初始化PlayableGraph
                InitializePlayableGraph();
                
                Debug.Log("可播放系统初始化完成");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"可播放系统初始化失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 创建时间轴资源
        /// </summary>
        private void CreateTimelineAsset()
        {
            timelineAsset = ScriptableObject.CreateInstance<TimelineAsset>();
            timelineAsset.name = timelineName;
            timelineAsset.durationMode = TimelineAsset.DurationMode.FixedLength;
            timelineAsset.fixedDuration = timelineDuration;
            
            // 保存时间轴资源
            string path = $"Assets/Timelines/{timelineName}.playable";
            string directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            
            AssetDatabase.CreateAsset(timelineAsset, path);
            AssetDatabase.SaveAssets();
            
            // 设置到PlayableDirector
            playableDirector.playableAsset = timelineAsset;
            
            Debug.Log($"时间轴资源已创建: {path}");
        }
        
        /// <summary>
        /// 初始化PlayableGraph
        /// </summary>
        private void InitializePlayableGraph()
        {
            playableGraph = PlayableGraph.Create("CustomPlayableGraph");
            playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            
            Debug.Log("PlayableGraph已初始化");
        }
        
        /// <summary>
        /// 创建动画轨道
        /// </summary>
        public AnimationTrack CreateAnimationTrack(string trackName)
        {
            if (timelineAsset == null)
            {
                Debug.LogError("时间轴资源为空");
                return null;
            }
            
            var animationTrack = timelineAsset.CreateTrack<AnimationTrack>(null, trackName);
            availableTracks.Add(animationTrack);
            currentTrack = trackName;
            
            Debug.Log($"动画轨道已创建: {trackName}");
            return animationTrack;
        }
        
        /// <summary>
        /// 创建音频轨道
        /// </summary>
        public AudioTrack CreateAudioTrack(string trackName)
        {
            if (timelineAsset == null)
            {
                Debug.LogError("时间轴资源为空");
                return null;
            }
            
            var audioTrack = timelineAsset.CreateTrack<AudioTrack>(null, trackName);
            availableTracks.Add(audioTrack);
            currentTrack = trackName;
            
            Debug.Log($"音频轨道已创建: {trackName}");
            return audioTrack;
        }
        
        /// <summary>
        /// 创建激活轨道
        /// </summary>
        public ActivationTrack CreateActivationTrack(string trackName)
        {
            if (timelineAsset == null)
            {
                Debug.LogError("时间轴资源为空");
                return null;
            }
            
            var activationTrack = timelineAsset.CreateTrack<ActivationTrack>(null, trackName);
            availableTracks.Add(activationTrack);
            currentTrack = trackName;
            
            Debug.Log($"激活轨道已创建: {trackName}");
            return activationTrack;
        }
        
        /// <summary>
        /// 创建控制轨道
        /// </summary>
        public ControlTrack CreateControlTrack(string trackName)
        {
            if (timelineAsset == null)
            {
                Debug.LogError("时间轴资源为空");
                return null;
            }
            
            var controlTrack = timelineAsset.CreateTrack<ControlTrack>(null, trackName);
            availableTracks.Add(controlTrack);
            currentTrack = trackName;
            
            Debug.Log($"控制轨道已创建: {trackName}");
            return controlTrack;
        }
        
        /// <summary>
        /// 添加动画片段
        /// </summary>
        public void AddAnimationClip(AnimationTrack track, AnimationClip clip, float startTime)
        {
            if (track == null || clip == null)
            {
                Debug.LogWarning("轨道或动画片段为空");
                return;
            }
            
            var timelineClip = track.CreateDefaultClip();
            timelineClip.animationClip = clip;
            timelineClip.start = startTime;
            timelineClip.duration = clip.length;
            
            Debug.Log($"动画片段已添加到轨道: {clip.name}");
        }
        
        /// <summary>
        /// 添加音频片段
        /// </summary>
        public void AddAudioClip(AudioTrack track, AudioClip clip, float startTime)
        {
            if (track == null || clip == null)
            {
                Debug.LogWarning("轨道或音频片段为空");
                return;
            }
            
            var timelineClip = track.CreateDefaultClip();
            timelineClip.audioClip = clip;
            timelineClip.start = startTime;
            timelineClip.duration = clip.length;
            
            Debug.Log($"音频片段已添加到轨道: {clip.name}");
        }
        
        /// <summary>
        /// 播放时间轴
        /// </summary>
        public void PlayTimeline()
        {
            if (playableDirector == null)
            {
                Debug.LogWarning("PlayableDirector为空");
                return;
            }
            
            playableDirector.Play();
            isPlaying = true;
            Debug.Log("时间轴开始播放");
        }
        
        /// <summary>
        /// 暂停时间轴
        /// </summary>
        public void PauseTimeline()
        {
            if (playableDirector == null)
            {
                Debug.LogWarning("PlayableDirector为空");
                return;
            }
            
            playableDirector.Pause();
            isPlaying = false;
            Debug.Log("时间轴已暂停");
        }
        
        /// <summary>
        /// 停止时间轴
        /// </summary>
        public void StopTimeline()
        {
            if (playableDirector == null)
            {
                Debug.LogWarning("PlayableDirector为空");
                return;
            }
            
            playableDirector.Stop();
            isPlaying = false;
            currentTime = 0f;
            Debug.Log("时间轴已停止");
        }
        
        /// <summary>
        /// 设置播放时间
        /// </summary>
        public void SetPlaybackTime(double time)
        {
            if (playableDirector == null)
            {
                Debug.LogWarning("PlayableDirector为空");
                return;
            }
            
            playableDirector.time = time;
            currentTime = (float)time;
            Debug.Log($"播放时间已设置为: {time}");
        }
        
        /// <summary>
        /// 设置播放速度
        /// </summary>
        public void SetPlaybackSpeed(float speed)
        {
            if (playableDirector == null)
            {
                Debug.LogWarning("PlayableDirector为空");
                return;
            }
            
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(speed);
            Debug.Log($"播放速度已设置为: {speed}");
        }
        
        /// <summary>
        /// 绑定对象到轨道
        /// </summary>
        public void BindObjectToTrack(TrackAsset track, Object binding)
        {
            if (track == null || binding == null)
            {
                Debug.LogWarning("轨道或绑定对象为空");
                return;
            }
            
            playableDirector.SetGenericBinding(track, binding);
            Debug.Log($"对象 {binding.name} 已绑定到轨道 {track.name}");
        }
        
        /// <summary>
        /// 获取轨道信息
        /// </summary>
        public string GetTrackInfo(TrackAsset track)
        {
            if (track == null)
                return "轨道为空";
            
            return $"名称: {track.name}, 类型: {track.GetType().Name}, 片段数量: {track.GetClips().Count}";
        }
        
        /// <summary>
        /// 删除轨道
        /// </summary>
        public void DeleteTrack(TrackAsset track)
        {
            if (track == null)
            {
                Debug.LogWarning("轨道为空");
                return;
            }
            
            timelineAsset.DeleteTrack(track);
            availableTracks.Remove(track);
            DestroyImmediate(track);
            
            Debug.Log($"轨道已删除: {track.name}");
        }
        
        /// <summary>
        /// 导出时间轴
        /// </summary>
        public void ExportTimeline()
        {
            if (timelineAsset == null)
            {
                Debug.LogWarning("时间轴资源为空");
                return;
            }
            
            string exportPath = EditorUtility.SaveFilePanel("导出时间轴", "", timelineAsset.name, "playable");
            if (string.IsNullOrEmpty(exportPath))
                return;
            
            try
            {
                string sourcePath = AssetDatabase.GetAssetPath(timelineAsset);
                System.IO.File.Copy(sourcePath, exportPath, true);
                Debug.Log($"时间轴已导出到: {exportPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导出时间轴失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 导入时间轴
        /// </summary>
        public void ImportTimeline()
        {
            string importPath = EditorUtility.OpenFilePanel("导入时间轴", "", "playable");
            if (string.IsNullOrEmpty(importPath))
                return;
            
            try
            {
                string fileName = System.IO.Path.GetFileName(importPath);
                string targetPath = $"Assets/Timelines/{fileName}";
                
                // 确保目录存在
                string directory = System.IO.Path.GetDirectoryName(targetPath);
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
                
                System.IO.File.Copy(importPath, targetPath, true);
                AssetDatabase.Refresh();
                
                // 加载时间轴
                timelineAsset = AssetDatabase.LoadAssetAtPath<TimelineAsset>(targetPath);
                if (playableDirector != null)
                {
                    playableDirector.playableAsset = timelineAsset;
                }
                
                Debug.Log($"时间轴已导入: {targetPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导入时间轴失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 获取所有轨道名称
        /// </summary>
        public string[] GetAllTrackNames()
        {
            return availableTracks.Select(t => t.name).ToArray();
        }
        
        /// <summary>
        /// 更新播放状态
        /// </summary>
        private void Update()
        {
            if (playableDirector != null && timelineAsset != null)
            {
                currentTime = (float)playableDirector.time;
                totalDuration = (float)timelineAsset.duration;
                isPlaying = playableDirector.state == PlayState.Playing;
            }
        }
        
        /// <summary>
        /// 清理资源
        /// </summary>
        private void OnDestroy()
        {
            if (playableGraph.IsValid())
            {
                playableGraph.Destroy();
            }
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 500, 700));
            GUILayout.Label("UnityEditor.Playables 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {(enablePlayables ? "启用" : "禁用")}");
            GUILayout.Label($"播放状态: {(isPlaying ? "播放中" : "已停止")}");
            GUILayout.Label($"当前时间: {currentTime:F2} / {totalDuration:F2}");
            GUILayout.Label($"当前轨道: {currentTrack}");
            
            GUILayout.Space(10);
            GUILayout.Label("播放控制", EditorStyles.boldLabel);
            
            if (GUILayout.Button("播放"))
            {
                PlayTimeline();
            }
            
            if (GUILayout.Button("暂停"))
            {
                PauseTimeline();
            }
            
            if (GUILayout.Button("停止"))
            {
                StopTimeline();
            }
            
            // 时间滑块
            float newTime = GUILayout.HorizontalSlider(currentTime, 0f, totalDuration);
            if (Mathf.Abs(newTime - currentTime) > 0.1f)
            {
                SetPlaybackTime(newTime);
            }
            
            GUILayout.Space(10);
            GUILayout.Label("轨道创建", EditorStyles.boldLabel);
            
            if (GUILayout.Button("创建动画轨道"))
            {
                CreateAnimationTrack("AnimationTrack");
            }
            
            if (GUILayout.Button("创建音频轨道"))
            {
                CreateAudioTrack("AudioTrack");
            }
            
            if (GUILayout.Button("创建激活轨道"))
            {
                CreateActivationTrack("ActivationTrack");
            }
            
            if (GUILayout.Button("创建控制轨道"))
            {
                CreateControlTrack("ControlTrack");
            }
            
            GUILayout.Space(10);
            GUILayout.Label("轨道管理", EditorStyles.boldLabel);
            
            string[] trackNames = GetAllTrackNames();
            if (trackNames.Length > 0)
            {
                int selectedIndex = EditorGUILayout.Popup("选择轨道", 0, trackNames);
                if (selectedIndex >= 0 && selectedIndex < availableTracks.Count)
                {
                    currentTrackAsset = availableTracks[selectedIndex];
                    
                    GUILayout.Label(GetTrackInfo(currentTrackAsset));
                    
                    if (GUILayout.Button("删除轨道"))
                    {
                        DeleteTrack(currentTrackAsset);
                    }
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("对象绑定", EditorStyles.boldLabel);
            
            targetObject = (GameObject)EditorGUILayout.ObjectField("目标对象", targetObject, typeof(GameObject), true);
            
            if (GUILayout.Button("绑定对象到当前轨道"))
            {
                if (currentTrackAsset != null && targetObject != null)
                {
                    BindObjectToTrack(currentTrackAsset, targetObject);
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("导入导出", EditorStyles.boldLabel);
            
            if (GUILayout.Button("导出时间轴"))
            {
                ExportTimeline();
            }
            
            if (GUILayout.Button("导入时间轴"))
            {
                ImportTimeline();
            }
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            enablePlayables = EditorGUILayout.Toggle("启用可播放", enablePlayables);
            timelineName = EditorGUILayout.TextField("时间轴名称", timelineName);
            timelineDuration = EditorGUILayout.FloatField("时间轴长度", timelineDuration);
            autoPlay = EditorGUILayout.Toggle("自动播放", autoPlay);
            
            GUILayout.EndArea();
        }
    }
} 