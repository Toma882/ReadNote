using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

namespace UnityEditor.Chapter9Utility.AnimationUtility
{
    /// <summary>
    /// AnimationUtility 动画工具详细示例
    /// 展示动画剪辑、事件、曲线等操作
    /// </summary>
    public class AnimationUtilityExample : EditorWindow
    {
        private Vector2 scrollPosition;
        private GameObject targetObject;
        private AnimationClip selectedClip;
        private List<AnimationEvent> animationEvents = new List<AnimationEvent>();
        private List<EditorCurveBinding> curveBindings = new List<EditorCurveBinding>();

        [MenuItem("Tools/Utility Examples/AnimationUtility Detailed Example")]
        public static void ShowWindow()
        {
            AnimationUtilityExample window = GetWindow<AnimationUtilityExample>("AnimationUtility 示例");
            window.Show();
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            GUILayout.Label("AnimationUtility 动画工具示例", EditorStyles.boldLabel);
            
            EditorGUILayout.Space();
            
            // 目标对象选择
            EditorGUILayout.LabelField("目标对象:", EditorStyles.boldLabel);
            targetObject = (GameObject)EditorGUILayout.ObjectField("目标对象", targetObject, typeof(GameObject), true);
            
            if (targetObject == null)
            {
                targetObject = Selection.activeGameObject;
            }
            
            EditorGUILayout.Space();
            
            // 动画剪辑操作
            EditorGUILayout.LabelField("动画剪辑操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("获取所有动画剪辑"))
            {
                GetAllAnimationClips();
            }
            
            if (GUILayout.Button("创建新动画剪辑"))
            {
                CreateNewAnimationClip();
            }
            
            if (GUILayout.Button("复制动画剪辑"))
            {
                CopyAnimationClip();
            }
            
            EditorGUILayout.Space();
            
            // 动画事件操作
            EditorGUILayout.LabelField("动画事件操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("获取动画事件"))
            {
                GetAnimationEvents();
            }
            
            if (GUILayout.Button("添加动画事件"))
            {
                AddAnimationEvent();
            }
            
            if (GUILayout.Button("删除动画事件"))
            {
                DeleteAnimationEvent();
            }
            
            if (GUILayout.Button("批量添加事件"))
            {
                BatchAddEvents();
            }
            
            EditorGUILayout.Space();
            
            // 动画曲线操作
            EditorGUILayout.LabelField("动画曲线操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("获取动画曲线"))
            {
                GetAnimationCurves();
            }
            
            if (GUILayout.Button("创建位置动画"))
            {
                CreatePositionAnimation();
            }
            
            if (GUILayout.Button("创建旋转动画"))
            {
                CreateRotationAnimation();
            }
            
            if (GUILayout.Button("创建缩放动画"))
            {
                CreateScaleAnimation();
            }
            
            EditorGUILayout.Space();
            
            // 高级操作
            EditorGUILayout.LabelField("高级操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("动画剪辑分析"))
            {
                AnalyzeAnimationClip();
            }
            
            if (GUILayout.Button("动画优化"))
            {
                OptimizeAnimation();
            }
            
            if (GUILayout.Button("动画合并"))
            {
                MergeAnimations();
            }
            
            if (GUILayout.Button("动画分割"))
            {
                SplitAnimation();
            }
            
            EditorGUILayout.Space();
            
            // 显示当前信息
            if (selectedClip != null)
            {
                EditorGUILayout.LabelField("当前动画剪辑信息:", EditorStyles.boldLabel);
                EditorGUILayout.LabelField($"名称: {selectedClip.name}");
                EditorGUILayout.LabelField($"长度: {selectedClip.length:F2} 秒");
                EditorGUILayout.LabelField($"帧率: {selectedClip.frameRate:F0} FPS");
                EditorGUILayout.LabelField($"事件数量: {animationEvents.Count}");
                EditorGUILayout.LabelField($"曲线数量: {curveBindings.Count}");
            }
            
            EditorGUILayout.EndScrollView();
        }

        #region 动画剪辑操作

        /// <summary>
        /// 获取所有动画剪辑
        /// </summary>
        private void GetAllAnimationClips()
        {
            if (targetObject == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            AnimationClip[] clips = UnityEditor.AnimationUtility.GetAnimationClips(targetObject);
            
            Debug.Log($"对象 {targetObject.name} 的动画剪辑:");
            for (int i = 0; i < clips.Length; i++)
            {
                Debug.Log($"  {i + 1}. {clips[i].name} (长度: {clips[i].length:F2}s)");
            }
            
            if (clips.Length > 0)
            {
                selectedClip = clips[0];
                GetAnimationEvents();
                GetAnimationCurves();
            }
        }

        /// <summary>
        /// 创建新动画剪辑
        /// </summary>
        private void CreateNewAnimationClip()
        {
            if (targetObject == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            string fileName = EditorUtility.SaveFilePanelInProject(
                "保存动画剪辑", 
                "NewAnimation", 
                "anim", 
                "选择保存位置");
            
            if (!string.IsNullOrEmpty(fileName))
            {
                AnimationClip newClip = new AnimationClip();
                newClip.name = System.IO.Path.GetFileNameWithoutExtension(fileName);
                
                AssetDatabase.CreateAsset(newClip, fileName);
                AssetDatabase.SaveAssets();
                
                Debug.Log($"创建动画剪辑: {newClip.name}");
                selectedClip = newClip;
            }
        }

        /// <summary>
        /// 复制动画剪辑
        /// </summary>
        private void CopyAnimationClip()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            string fileName = EditorUtility.SaveFilePanelInProject(
                "复制动画剪辑", 
                selectedClip.name + "_Copy", 
                "anim", 
                "选择保存位置");
            
            if (!string.IsNullOrEmpty(fileName))
            {
                AnimationClip copiedClip = Object.Instantiate(selectedClip);
                copiedClip.name = System.IO.Path.GetFileNameWithoutExtension(fileName);
                
                AssetDatabase.CreateAsset(copiedClip, fileName);
                AssetDatabase.SaveAssets();
                
                Debug.Log($"复制动画剪辑: {copiedClip.name}");
            }
        }

        #endregion

        #region 动画事件操作

        /// <summary>
        /// 获取动画事件
        /// </summary>
        private void GetAnimationEvents()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            AnimationEvent[] events = UnityEditor.AnimationUtility.GetAnimationEvents(selectedClip);
            animationEvents.Clear();
            animationEvents.AddRange(events);
            
            Debug.Log($"动画剪辑 {selectedClip.name} 的事件:");
            for (int i = 0; i < events.Length; i++)
            {
                AnimationEvent animEvent = events[i];
                Debug.Log($"  {i + 1}. {animEvent.functionName} 在时间 {animEvent.time:F2}s");
            }
        }

        /// <summary>
        /// 添加动画事件
        /// </summary>
        private void AddAnimationEvent()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            AnimationEvent newEvent = new AnimationEvent();
            newEvent.time = selectedClip.length * 0.5f; // 在中间时间添加
            newEvent.functionName = "OnAnimationEvent";
            newEvent.stringParameter = "TestEvent";
            newEvent.intParameter = 123;
            newEvent.floatParameter = 45.6f;
            
            animationEvents.Add(newEvent);
            UnityEditor.AnimationUtility.SetAnimationEvents(selectedClip, animationEvents.ToArray());
            
            Debug.Log($"添加动画事件: {newEvent.functionName} 在时间 {newEvent.time:F2}s");
        }

        /// <summary>
        /// 删除动画事件
        /// </summary>
        private void DeleteAnimationEvent()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            if (animationEvents.Count == 0)
            {
                Debug.LogWarning("没有动画事件可删除");
                return;
            }
            
            animationEvents.RemoveAt(animationEvents.Count - 1);
            UnityEditor.AnimationUtility.SetAnimationEvents(selectedClip, animationEvents.ToArray());
            
            Debug.Log($"删除最后一个动画事件，剩余: {animationEvents.Count}");
        }

        /// <summary>
        /// 批量添加事件
        /// </summary>
        private void BatchAddEvents()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            string[] eventNames = { "Event1", "Event2", "Event3", "Event4", "Event5" };
            
            for (int i = 0; i < eventNames.Length; i++)
            {
                AnimationEvent newEvent = new AnimationEvent();
                newEvent.time = selectedClip.length * (i + 1) / (eventNames.Length + 1);
                newEvent.functionName = eventNames[i];
                newEvent.stringParameter = $"Parameter{i}";
                
                animationEvents.Add(newEvent);
            }
            
            UnityEditor.AnimationUtility.SetAnimationEvents(selectedClip, animationEvents.ToArray());
            
            Debug.Log($"批量添加 {eventNames.Length} 个动画事件");
        }

        #endregion

        #region 动画曲线操作

        /// <summary>
        /// 获取动画曲线
        /// </summary>
        private void GetAnimationCurves()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            EditorCurveBinding[] bindings = UnityEditor.AnimationUtility.GetCurveBindings(selectedClip);
            curveBindings.Clear();
            curveBindings.AddRange(bindings);
            
            Debug.Log($"动画剪辑 {selectedClip.name} 的曲线:");
            for (int i = 0; i < bindings.Length; i++)
            {
                EditorCurveBinding binding = bindings[i];
                Debug.Log($"  {i + 1}. {binding.propertyName} 路径: {binding.path}");
            }
        }

        /// <summary>
        /// 创建位置动画
        /// </summary>
        private void CreatePositionAnimation()
        {
            if (targetObject == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            // 创建位置动画曲线
            AnimationCurve posXCurve = AnimationCurve.EaseInOut(0f, targetObject.transform.position.x, 1f, targetObject.transform.position.x + 5f);
            AnimationCurve posYCurve = AnimationCurve.EaseInOut(0f, targetObject.transform.position.y, 1f, targetObject.transform.position.y + 2f);
            AnimationCurve posZCurve = AnimationCurve.EaseInOut(0f, targetObject.transform.position.z, 1f, targetObject.transform.position.z + 3f);
            
            // 设置动画曲线
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalPosition.x"), posXCurve);
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalPosition.y"), posYCurve);
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalPosition.z"), posZCurve);
            
            Debug.Log("创建位置动画完成");
        }

        /// <summary>
        /// 创建旋转动画
        /// </summary>
        private void CreateRotationAnimation()
        {
            if (targetObject == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            // 创建旋转动画曲线
            AnimationCurve rotXCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 360f);
            AnimationCurve rotYCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 180f);
            AnimationCurve rotZCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 90f);
            
            // 设置动画曲线
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalRotation.x"), rotXCurve);
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalRotation.y"), rotYCurve);
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalRotation.z"), rotZCurve);
            
            Debug.Log("创建旋转动画完成");
        }

        /// <summary>
        /// 创建缩放动画
        /// </summary>
        private void CreateScaleAnimation()
        {
            if (targetObject == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            // 创建缩放动画曲线
            AnimationCurve scaleXCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 2f);
            AnimationCurve scaleYCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1.5f);
            AnimationCurve scaleZCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0.5f);
            
            // 设置动画曲线
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalScale.x"), scaleXCurve);
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalScale.y"), scaleYCurve);
            UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalScale.z"), scaleZCurve);
            
            Debug.Log("创建缩放动画完成");
        }

        #endregion

        #region 高级操作

        /// <summary>
        /// 动画剪辑分析
        /// </summary>
        private void AnalyzeAnimationClip()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            Debug.Log($"动画剪辑分析: {selectedClip.name}");
            Debug.Log($"  长度: {selectedClip.length:F2} 秒");
            Debug.Log($"  帧率: {selectedClip.frameRate:F0} FPS");
            Debug.Log($"  总帧数: {selectedClip.length * selectedClip.frameRate:F0}");
            
            // 分析事件
            AnimationEvent[] events = UnityEditor.AnimationUtility.GetAnimationEvents(selectedClip);
            Debug.Log($"  事件数量: {events.Length}");
            
            // 分析曲线
            EditorCurveBinding[] bindings = UnityEditor.AnimationUtility.GetCurveBindings(selectedClip);
            Debug.Log($"  曲线数量: {bindings.Length}");
            
            // 分析关键帧
            int totalKeyframes = 0;
            foreach (EditorCurveBinding binding in bindings)
            {
                AnimationCurve curve = UnityEditor.AnimationUtility.GetEditorCurve(selectedClip, binding);
                totalKeyframes += curve.length;
            }
            Debug.Log($"  总关键帧数: {totalKeyframes}");
            
            // 分析动画类型
            bool hasPosition = false, hasRotation = false, hasScale = false;
            foreach (EditorCurveBinding binding in bindings)
            {
                if (binding.propertyName.Contains("Position")) hasPosition = true;
                if (binding.propertyName.Contains("Rotation")) hasRotation = true;
                if (binding.propertyName.Contains("Scale")) hasScale = true;
            }
            
            Debug.Log($"  动画类型: {(hasPosition ? "位置 " : "")}{(hasRotation ? "旋转 " : "")}{(hasScale ? "缩放" : "")}");
        }

        /// <summary>
        /// 动画优化
        /// </summary>
        private void OptimizeAnimation()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            Debug.Log($"优化动画剪辑: {selectedClip.name}");
            
            EditorCurveBinding[] bindings = UnityEditor.AnimationUtility.GetCurveBindings(selectedClip);
            int removedKeyframes = 0;
            
            foreach (EditorCurveBinding binding in bindings)
            {
                AnimationCurve curve = UnityEditor.AnimationUtility.GetEditorCurve(selectedClip, binding);
                
                if (curve.length > 2)
                {
                    // 移除冗余关键帧
                    AnimationCurve optimizedCurve = new AnimationCurve();
                    
                    for (int i = 0; i < curve.length; i++)
                    {
                        Keyframe keyframe = curve[i];
                        
                        // 保留第一个和最后一个关键帧
                        if (i == 0 || i == curve.length - 1)
                        {
                            optimizedCurve.AddKey(keyframe);
                        }
                        else
                        {
                            // 检查是否与前一个关键帧值相同
                            Keyframe prevKey = curve[i - 1];
                            if (Mathf.Abs(keyframe.value - prevKey.value) > 0.001f)
                            {
                                optimizedCurve.AddKey(keyframe);
                            }
                            else
                            {
                                removedKeyframes++;
                            }
                        }
                    }
                    
                    UnityEditor.AnimationUtility.SetEditorCurve(selectedClip, binding, optimizedCurve);
                }
            }
            
            Debug.Log($"动画优化完成，移除 {removedKeyframes} 个冗余关键帧");
        }

        /// <summary>
        /// 动画合并
        /// </summary>
        private void MergeAnimations()
        {
            AnimationClip[] clips = UnityEditor.AnimationUtility.GetAnimationClips(targetObject);
            
            if (clips.Length < 2)
            {
                Debug.LogWarning("需要至少2个动画剪辑进行合并");
                return;
            }
            
            string fileName = EditorUtility.SaveFilePanelInProject(
                "合并动画剪辑", 
                "MergedAnimation", 
                "anim", 
                "选择保存位置");
            
            if (!string.IsNullOrEmpty(fileName))
            {
                AnimationClip mergedClip = new AnimationClip();
                mergedClip.name = System.IO.Path.GetFileNameWithoutExtension(fileName);
                
                float currentTime = 0f;
                
                foreach (AnimationClip clip in clips)
                {
                    EditorCurveBinding[] bindings = UnityEditor.AnimationUtility.GetCurveBindings(clip);
                    
                    foreach (EditorCurveBinding binding in bindings)
                    {
                        AnimationCurve originalCurve = UnityEditor.AnimationUtility.GetEditorCurve(clip, binding);
                        AnimationCurve newCurve = new AnimationCurve();
                        
                        for (int i = 0; i < originalCurve.length; i++)
                        {
                            Keyframe keyframe = originalCurve[i];
                            keyframe.time += currentTime;
                            newCurve.AddKey(keyframe);
                        }
                        
                        UnityEditor.AnimationUtility.SetEditorCurve(mergedClip, binding, newCurve);
                    }
                    
                    currentTime += clip.length;
                }
                
                AssetDatabase.CreateAsset(mergedClip, fileName);
                AssetDatabase.SaveAssets();
                
                Debug.Log($"合并 {clips.Length} 个动画剪辑完成: {mergedClip.name}");
            }
        }

        /// <summary>
        /// 动画分割
        /// </summary>
        private void SplitAnimation()
        {
            if (selectedClip == null)
            {
                Debug.LogWarning("请先选择一个动画剪辑");
                return;
            }
            
            float splitTime = selectedClip.length * 0.5f; // 在中间分割
            
            string fileName1 = EditorUtility.SaveFilePanelInProject(
                "保存第一部分", 
                selectedClip.name + "_Part1", 
                "anim", 
                "选择保存位置");
            
            string fileName2 = EditorUtility.SaveFilePanelInProject(
                "保存第二部分", 
                selectedClip.name + "_Part2", 
                "anim", 
                "选择保存位置");
            
            if (!string.IsNullOrEmpty(fileName1) && !string.IsNullOrEmpty(fileName2))
            {
                // 创建第一部分
                AnimationClip part1 = new AnimationClip();
                part1.name = System.IO.Path.GetFileNameWithoutExtension(fileName1);
                
                // 创建第二部分
                AnimationClip part2 = new AnimationClip();
                part2.name = System.IO.Path.GetFileNameWithoutExtension(fileName2);
                
                EditorCurveBinding[] bindings = UnityEditor.AnimationUtility.GetCurveBindings(selectedClip);
                
                foreach (EditorCurveBinding binding in bindings)
                {
                    AnimationCurve originalCurve = UnityEditor.AnimationUtility.GetEditorCurve(selectedClip, binding);
                    
                    // 分割曲线
                    AnimationCurve curve1 = new AnimationCurve();
                    AnimationCurve curve2 = new AnimationCurve();
                    
                    for (int i = 0; i < originalCurve.length; i++)
                    {
                        Keyframe keyframe = originalCurve[i];
                        
                        if (keyframe.time <= splitTime)
                        {
                            curve1.AddKey(keyframe);
                        }
                        else
                        {
                            Keyframe newKeyframe = keyframe;
                            newKeyframe.time -= splitTime;
                            curve2.AddKey(newKeyframe);
                        }
                    }
                    
                    UnityEditor.AnimationUtility.SetEditorCurve(part1, binding, curve1);
                    UnityEditor.AnimationUtility.SetEditorCurve(part2, binding, curve2);
                }
                
                AssetDatabase.CreateAsset(part1, fileName1);
                AssetDatabase.CreateAsset(part2, fileName2);
                AssetDatabase.SaveAssets();
                
                Debug.Log($"动画分割完成: {part1.name} 和 {part2.name}");
            }
        }

        #endregion

        #region 高级动画操作示例

        /// <summary>
        /// 动画剪辑属性操作
        /// </summary>
        public static void AnimationClipPropertiesExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            if (clips.Length > 0)
            {
                AnimationClip clip = clips[0];
                
                // 设置动画剪辑属性
                AnimationUtility.SetAnimationClipSettings(clip, new AnimationClipSettings
                {
                    loopTime = true,
                    loopBlend = true,
                    loopBlendOrientation = true,
                    loopBlendPositionY = true,
                    loopBlendPositionXZ = true,
                    keepOriginalOrientation = false,
                    keepOriginalPositionY = false,
                    keepOriginalPositionXZ = false,
                    heightFromFeet = false,
                    mirror = false
                });
                
                Debug.Log($"动画剪辑 {clip.name} 属性已设置");
            }
        }

        /// <summary>
        /// 动画曲线操作
        /// </summary>
        public static void AnimationCurveOperationsExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            if (clips.Length > 0)
            {
                AnimationClip clip = clips[0];
                
                // 获取所有曲线绑定
                EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
                
                foreach (EditorCurveBinding binding in bindings)
                {
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                    if (curve != null)
                    {
                        Debug.Log($"曲线: {binding.path}.{binding.propertyName}, 关键帧数: {curve.length}");
                        
                        // 修改曲线
                        Keyframe[] keys = curve.keys;
                        for (int i = 0; i < keys.Length; i++)
                        {
                            keys[i].value *= 1.1f; // 放大10%
                        }
                        curve.keys = keys;
                        
                        AnimationUtility.SetEditorCurve(clip, binding, curve);
                    }
                }
                
                Debug.Log($"动画曲线操作完成");
            }
        }

        /// <summary>
        /// 动画事件批量操作
        /// </summary>
        public static void AnimationEventBatchOperationsExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            if (clips.Length > 0)
            {
                AnimationClip clip = clips[0];
                
                // 批量添加事件
                AnimationEvent[] events = new AnimationEvent[]
                {
                    new AnimationEvent { time = 0.5f, functionName = "OnAnimationEvent1" },
                    new AnimationEvent { time = 1.0f, functionName = "OnAnimationEvent2" },
                    new AnimationEvent { time = 1.5f, functionName = "OnAnimationEvent3" }
                };
                
                AnimationUtility.SetAnimationEvents(clip, events);
                Debug.Log($"批量添加了 {events.Length} 个动画事件");
            }
        }

        /// <summary>
        /// 动画剪辑复制
        /// </summary>
        public static void AnimationClipCopyExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            if (clips.Length > 0)
            {
                AnimationClip sourceClip = clips[0];
                AnimationClip newClip = new AnimationClip();
                
                // 复制曲线
                EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(sourceClip);
                foreach (EditorCurveBinding binding in bindings)
                {
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(sourceClip, binding);
                    AnimationUtility.SetEditorCurve(newClip, binding, curve);
                }
                
                // 复制事件
                AnimationEvent[] events = AnimationUtility.GetAnimationEvents(sourceClip);
                AnimationUtility.SetAnimationEvents(newClip, events);
                
                Debug.Log($"动画剪辑复制完成: {sourceClip.name} -> {newClip.name}");
            }
        }

        /// <summary>
        /// 动画剪辑合并
        /// </summary>
        public static void AnimationClipMergeExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            if (clips.Length >= 2)
            {
                AnimationClip clip1 = clips[0];
                AnimationClip clip2 = clips[1];
                AnimationClip mergedClip = new AnimationClip();
                
                // 合并第一个剪辑
                EditorCurveBinding[] bindings1 = AnimationUtility.GetCurveBindings(clip1);
                foreach (EditorCurveBinding binding in bindings1)
                {
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(clip1, binding);
                    AnimationUtility.SetEditorCurve(mergedClip, binding, curve);
                }
                
                // 合并第二个剪辑（偏移时间）
                EditorCurveBinding[] bindings2 = AnimationUtility.GetCurveBindings(clip2);
                foreach (EditorCurveBinding binding in bindings2)
                {
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(clip2, binding);
                    
                    // 偏移关键帧时间
                    Keyframe[] keys = curve.keys;
                    for (int i = 0; i < keys.Length; i++)
                    {
                        keys[i].time += clip1.length; // 偏移到第一个剪辑之后
                    }
                    curve.keys = keys;
                    
                    AnimationUtility.SetEditorCurve(mergedClip, binding, curve);
                }
                
                Debug.Log($"动画剪辑合并完成: {clip1.name} + {clip2.name} -> {mergedClip.name}");
            }
        }

        #endregion

        #region 动画优化示例

        /// <summary>
        /// 动画优化
        /// </summary>
        public static void AnimationOptimizationExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            if (clips.Length > 0)
            {
                AnimationClip clip = clips[0];
                
                // 获取所有曲线
                EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
                
                foreach (EditorCurveBinding binding in bindings)
                {
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                    if (curve != null && curve.length > 2)
                    {
                        // 简化曲线（移除冗余关键帧）
                        Keyframe[] keys = curve.keys;
                        List<Keyframe> optimizedKeys = new List<Keyframe>();
                        
                        optimizedKeys.Add(keys[0]); // 保留第一个关键帧
                        
                        for (int i = 1; i < keys.Length - 1; i++)
                        {
                            // 检查是否冗余
                            float prevValue = keys[i - 1].value;
                            float currentValue = keys[i].value;
                            float nextValue = keys[i + 1].value;
                            
                            // 如果值变化超过阈值，保留关键帧
                            if (Mathf.Abs(currentValue - prevValue) > 0.01f || 
                                Mathf.Abs(currentValue - nextValue) > 0.01f)
                            {
                                optimizedKeys.Add(keys[i]);
                            }
                        }
                        
                        optimizedKeys.Add(keys[keys.Length - 1]); // 保留最后一个关键帧
                        
                        if (optimizedKeys.Count < keys.Length)
                        {
                            curve.keys = optimizedKeys.ToArray();
                            AnimationUtility.SetEditorCurve(clip, binding, curve);
                            Debug.Log($"曲线 {binding.propertyName} 优化: {keys.Length} -> {optimizedKeys.Count} 关键帧");
                        }
                    }
                }
                
                Debug.Log("动画优化完成");
            }
        }

        /// <summary>
        /// 动画压缩
        /// </summary>
        public static void AnimationCompressionExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            if (clips.Length > 0)
            {
                AnimationClip clip = clips[0];
                
                // 设置压缩设置
                AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(clip);
                settings.loopTime = true;
                settings.loopBlend = true;
                
                AnimationUtility.SetAnimationClipSettings(clip, settings);
                
                // 压缩曲线精度
                EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
                foreach (EditorCurveBinding binding in bindings)
                {
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                    if (curve != null)
                    {
                        Keyframe[] keys = curve.keys;
                        for (int i = 0; i < keys.Length; i++)
                        {
                            // 压缩精度到小数点后2位
                            keys[i].value = Mathf.Round(keys[i].value * 100f) / 100f;
                            keys[i].time = Mathf.Round(keys[i].time * 100f) / 100f;
                        }
                        curve.keys = keys;
                        AnimationUtility.SetEditorCurve(clip, binding, curve);
                    }
                }
                
                Debug.Log("动画压缩完成");
            }
        }

        #endregion

        #region 动画分析示例

        /// <summary>
        /// 动画分析
        /// </summary>
        public static void AnimationAnalysisExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            if (clips.Length > 0)
            {
                AnimationClip clip = clips[0];
                
                Debug.Log($"=== 动画分析: {clip.name} ===");
                Debug.Log($"长度: {clip.length}秒");
                Debug.Log($"帧率: {clip.frameRate}fps");
                Debug.Log($"是否循环: {clip.isLooping}");
                
                // 分析曲线
                EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
                Debug.Log($"曲线数量: {bindings.Length}");
                
                foreach (EditorCurveBinding binding in bindings)
                {
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                    if (curve != null)
                    {
                        Debug.Log($"曲线: {binding.propertyName}, 关键帧: {curve.length}");
                        
                        // 分析值范围
                        float minValue = float.MaxValue;
                        float maxValue = float.MinValue;
                        
                        foreach (Keyframe keyframe in curve.keys)
                        {
                            minValue = Mathf.Min(minValue, keyframe.value);
                            maxValue = Mathf.Max(maxValue, keyframe.value);
                        }
                        
                        Debug.Log($"  值范围: {minValue} ~ {maxValue}");
                    }
                }
                
                // 分析事件
                AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
                Debug.Log($"事件数量: {events.Length}");
                
                foreach (AnimationEvent animEvent in events)
                {
                    Debug.Log($"事件: {animEvent.functionName} at {animEvent.time}s");
                }
            }
        }

        /// <summary>
        /// 动画统计
        /// </summary>
        public static void AnimationStatisticsExample()
        {
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            
            int totalCurves = 0;
            int totalEvents = 0;
            float totalLength = 0f;
            
            foreach (AnimationClip clip in clips)
            {
                totalCurves += AnimationUtility.GetCurveBindings(clip).Length;
                totalEvents += AnimationUtility.GetAnimationEvents(clip).Length;
                totalLength += clip.length;
            }
            
            Debug.Log($"=== 动画统计 ===");
            Debug.Log($"总剪辑数: {clips.Length}");
            Debug.Log($"总曲线数: {totalCurves}");
            Debug.Log($"总事件数: {totalEvents}");
            Debug.Log($"总长度: {totalLength}秒");
            Debug.Log($"平均长度: {totalLength / clips.Length}秒");
        }

        #endregion


