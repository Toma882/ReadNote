using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using System.Collections.Generic;
using System.Linq;

namespace UnityEditor.Presets.Examples
{
    /// <summary>
    /// UnityEditor.Presets 命名空间使用示例
    /// 演示预设系统的创建、应用和管理功能
    /// </summary>
    public class PresetsExample : MonoBehaviour
    {
        [Header("预设配置")]
        [SerializeField] private bool enablePresets = true;
        [SerializeField] private string presetName = "DefaultPreset";
        [SerializeField] private string presetCategory = "Custom";
        [SerializeField] private bool autoApply = false;
        
        [Header("预设状态")]
        [SerializeField] private int presetCount = 0;
        [SerializeField] private string lastAppliedPreset = "";
        [SerializeField] private bool isPresetValid = false;
        
        [Header("目标对象")]
        [SerializeField] private Object targetObject;
        [SerializeField] private Component targetComponent;
        
        [Header("预设数据")]
        [SerializeField] private List<Preset> availablePresets = new List<Preset>();
        [SerializeField] private Preset currentPreset;
        
        private PresetManager presetManager;
        private Dictionary<string, Preset> presetRegistry = new Dictionary<string, Preset>();
        
        /// <summary>
        /// 初始化预设系统
        /// </summary>
        private void Start()
        {
            InitializePresetSystem();
        }
        
        /// <summary>
        /// 初始化预设系统
        /// </summary>
        private void InitializePresetSystem()
        {
            if (!enablePresets)
            {
                Debug.Log("预设系统已禁用");
                return;
            }
            
            try
            {
                // 获取预设管理器
                presetManager = PresetManager.DefaultPresetManager;
                
                // 加载可用预设
                LoadAvailablePresets();
                
                // 创建默认预设
                CreateDefaultPresets();
                
                Debug.Log("预设系统初始化完成");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"预设系统初始化失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 加载可用预设
        /// </summary>
        private void LoadAvailablePresets()
        {
            availablePresets.Clear();
            presetRegistry.Clear();
            
            // 获取所有预设资源
            string[] presetGuids = AssetDatabase.FindAssets("t:Preset");
            foreach (string guid in presetGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Preset preset = AssetDatabase.LoadAssetAtPath<Preset>(path);
                if (preset != null)
                {
                    availablePresets.Add(preset);
                    presetRegistry[preset.name] = preset;
                }
            }
            
            presetCount = availablePresets.Count;
            Debug.Log($"加载了 {presetCount} 个预设");
        }
        
        /// <summary>
        /// 创建默认预设
        /// </summary>
        private void CreateDefaultPresets()
        {
            // 创建Transform预设
            CreateTransformPreset();
            
            // 创建Material预设
            CreateMaterialPreset();
            
            // 创建AudioSource预设
            CreateAudioSourcePreset();
        }
        
        /// <summary>
        /// 创建Transform预设
        /// </summary>
        private void CreateTransformPreset()
        {
            var transformPreset = new Preset();
            var transform = new GameObject("TempTransform").transform;
            transformPreset.UpdateProperties(transform);
            
            transformPreset.name = "DefaultTransform";
            transformPreset.category = presetCategory;
            
            SavePreset(transformPreset, "DefaultTransform");
            DestroyImmediate(transform.gameObject);
        }
        
        /// <summary>
        /// 创建Material预设
        /// </summary>
        private void CreateMaterialPreset()
        {
            var materialPreset = new Preset();
            var material = new Material(Shader.Find("Standard"));
            materialPreset.UpdateProperties(material);
            
            materialPreset.name = "DefaultMaterial";
            materialPreset.category = presetCategory;
            
            SavePreset(materialPreset, "DefaultMaterial");
            DestroyImmediate(material);
        }
        
        /// <summary>
        /// 创建AudioSource预设
        /// </summary>
        private void CreateAudioSourcePreset()
        {
            var audioPreset = new Preset();
            var audioSource = new GameObject("TempAudio").AddComponent<AudioSource>();
            audioPreset.UpdateProperties(audioSource);
            
            audioPreset.name = "DefaultAudioSource";
            audioPreset.category = presetCategory;
            
            SavePreset(audioPreset, "DefaultAudioSource");
            DestroyImmediate(audioSource.gameObject);
        }
        
        /// <summary>
        /// 保存预设
        /// </summary>
        private void SavePreset(Preset preset, string fileName)
        {
            string path = $"Assets/Presets/{fileName}.preset";
            
            // 确保目录存在
            string directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            
            AssetDatabase.CreateAsset(preset, path);
            AssetDatabase.SaveAssets();
            
            Debug.Log($"预设已保存: {path}");
        }
        
        /// <summary>
        /// 应用预设到对象
        /// </summary>
        public void ApplyPreset(Preset preset, Object target)
        {
            if (preset == null || target == null)
            {
                Debug.LogWarning("预设或目标对象为空");
                return;
            }
            
            if (preset.CanBeAppliedTo(target))
            {
                preset.ApplyTo(target);
                lastAppliedPreset = preset.name;
                isPresetValid = true;
                
                Debug.Log($"预设 '{preset.name}' 已应用到 {target.name}");
            }
            else
            {
                Debug.LogWarning($"预设 '{preset.name}' 不能应用到 {target.name}");
                isPresetValid = false;
            }
        }
        
        /// <summary>
        /// 应用预设到组件
        /// </summary>
        public void ApplyPresetToComponent(Preset preset, Component component)
        {
            if (preset == null || component == null)
            {
                Debug.LogWarning("预设或组件为空");
                return;
            }
            
            if (preset.CanBeAppliedTo(component))
            {
                preset.ApplyTo(component);
                lastAppliedPreset = preset.name;
                isPresetValid = true;
                
                Debug.Log($"预设 '{preset.name}' 已应用到组件 {component.GetType().Name}");
            }
            else
            {
                Debug.LogWarning($"预设 '{preset.name}' 不能应用到组件 {component.GetType().Name}");
                isPresetValid = false;
            }
        }
        
        /// <summary>
        /// 从对象创建预设
        /// </summary>
        public Preset CreatePresetFromObject(Object sourceObject, string presetName)
        {
            if (sourceObject == null)
            {
                Debug.LogError("源对象为空");
                return null;
            }
            
            var preset = new Preset();
            preset.UpdateProperties(sourceObject);
            preset.name = presetName;
            preset.category = presetCategory;
            
            SavePreset(preset, presetName);
            availablePresets.Add(preset);
            presetRegistry[presetName] = preset;
            presetCount = availablePresets.Count;
            
            Debug.Log($"从 {sourceObject.name} 创建预设: {presetName}");
            return preset;
        }
        
        /// <summary>
        /// 从组件创建预设
        /// </summary>
        public Preset CreatePresetFromComponent(Component component, string presetName)
        {
            if (component == null)
            {
                Debug.LogError("组件为空");
                return null;
            }
            
            var preset = new Preset();
            preset.UpdateProperties(component);
            preset.name = presetName;
            preset.category = presetCategory;
            
            SavePreset(preset, presetName);
            availablePresets.Add(preset);
            presetRegistry[presetName] = preset;
            presetCount = availablePresets.Count;
            
            Debug.Log($"从组件 {component.GetType().Name} 创建预设: {presetName}");
            return preset;
        }
        
        /// <summary>
        /// 删除预设
        /// </summary>
        public void DeletePreset(string presetName)
        {
            if (presetRegistry.ContainsKey(presetName))
            {
                Preset preset = presetRegistry[presetName];
                string path = AssetDatabase.GetAssetPath(preset);
                
                AssetDatabase.DeleteAsset(path);
                availablePresets.Remove(preset);
                presetRegistry.Remove(presetName);
                presetCount = availablePresets.Count;
                
                Debug.Log($"预设已删除: {presetName}");
            }
        }
        
        /// <summary>
        /// 获取预设信息
        /// </summary>
        public string GetPresetInfo(Preset preset)
        {
            if (preset == null)
                return "预设为空";
            
            return $"名称: {preset.name}, 类别: {preset.category}, 目标类型: {preset.GetTargetTypeName()}";
        }
        
        /// <summary>
        /// 验证预设有效性
        /// </summary>
        public bool ValidatePreset(Preset preset, Object target)
        {
            if (preset == null || target == null)
                return false;
            
            return preset.CanBeAppliedTo(target);
        }
        
        /// <summary>
        /// 批量应用预设
        /// </summary>
        public void BatchApplyPreset(Preset preset, Object[] targets)
        {
            if (preset == null || targets == null)
            {
                Debug.LogWarning("预设或目标数组为空");
                return;
            }
            
            int successCount = 0;
            foreach (Object target in targets)
            {
                if (ValidatePreset(preset, target))
                {
                    ApplyPreset(preset, target);
                    successCount++;
                }
            }
            
            Debug.Log($"批量应用完成: {successCount}/{targets.Length} 个对象成功应用预设");
        }
        
        /// <summary>
        /// 导出预设
        /// </summary>
        public void ExportPreset(Preset preset)
        {
            if (preset == null)
            {
                Debug.LogWarning("预设为空");
                return;
            }
            
            string exportPath = EditorUtility.SaveFilePanel("导出预设", "", preset.name, "preset");
            if (string.IsNullOrEmpty(exportPath))
                return;
            
            try
            {
                string sourcePath = AssetDatabase.GetAssetPath(preset);
                System.IO.File.Copy(sourcePath, exportPath, true);
                Debug.Log($"预设已导出到: {exportPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导出预设失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 导入预设
        /// </summary>
        public void ImportPreset()
        {
            string importPath = EditorUtility.OpenFilePanel("导入预设", "", "preset");
            if (string.IsNullOrEmpty(importPath))
                return;
            
            try
            {
                string fileName = System.IO.Path.GetFileName(importPath);
                string targetPath = $"Assets/Presets/{fileName}";
                
                // 确保目录存在
                string directory = System.IO.Path.GetDirectoryName(targetPath);
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
                
                System.IO.File.Copy(importPath, targetPath, true);
                AssetDatabase.Refresh();
                
                // 重新加载预设
                LoadAvailablePresets();
                
                Debug.Log($"预设已导入: {targetPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导入预设失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 获取所有预设名称
        /// </summary>
        public string[] GetAllPresetNames()
        {
            return availablePresets.Select(p => p.name).ToArray();
        }
        
        /// <summary>
        /// 按类别获取预设
        /// </summary>
        public Preset[] GetPresetsByCategory(string category)
        {
            return availablePresets.Where(p => p.category == category).ToArray();
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 500, 700));
            GUILayout.Label("UnityEditor.Presets 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {(enablePresets ? "启用" : "禁用")}");
            GUILayout.Label($"预设数量: {presetCount}");
            GUILayout.Label($"最后应用: {lastAppliedPreset}");
            GUILayout.Label($"预设有效: {(isPresetValid ? "是" : "否")}");
            
            GUILayout.Space(10);
            GUILayout.Label("目标对象", EditorStyles.boldLabel);
            
            targetObject = EditorGUILayout.ObjectField("目标对象", targetObject, typeof(Object), true);
            targetComponent = (Component)EditorGUILayout.ObjectField("目标组件", targetComponent, typeof(Component), true);
            
            GUILayout.Space(10);
            GUILayout.Label("预设应用", EditorStyles.boldLabel);
            
            string[] presetNames = GetAllPresetNames();
            if (presetNames.Length > 0)
            {
                int selectedIndex = EditorGUILayout.Popup("选择预设", 0, presetNames);
                if (selectedIndex >= 0 && selectedIndex < availablePresets.Count)
                {
                    currentPreset = availablePresets[selectedIndex];
                    
                    if (GUILayout.Button("应用预设到对象"))
                    {
                        ApplyPreset(currentPreset, targetObject);
                    }
                    
                    if (GUILayout.Button("应用预设到组件"))
                    {
                        ApplyPresetToComponent(currentPreset, targetComponent);
                    }
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("预设创建", EditorStyles.boldLabel);
            
            presetName = GUILayout.TextField("预设名称", presetName);
            
            if (GUILayout.Button("从对象创建预设"))
            {
                CreatePresetFromObject(targetObject, presetName);
            }
            
            if (GUILayout.Button("从组件创建预设"))
            {
                CreatePresetFromComponent(targetComponent, presetName);
            }
            
            GUILayout.Space(10);
            GUILayout.Label("预设管理", EditorStyles.boldLabel);
            
            if (GUILayout.Button("刷新预设列表"))
            {
                LoadAvailablePresets();
            }
            
            if (GUILayout.Button("删除选中预设"))
            {
                if (currentPreset != null)
                {
                    DeletePreset(currentPreset.name);
                }
            }
            
            if (GUILayout.Button("导出选中预设"))
            {
                if (currentPreset != null)
                {
                    ExportPreset(currentPreset);
                }
            }
            
            if (GUILayout.Button("导入预设"))
            {
                ImportPreset();
            }
            
            GUILayout.Space(10);
            GUILayout.Label("预设信息", EditorStyles.boldLabel);
            
            if (currentPreset != null)
            {
                GUILayout.Label(GetPresetInfo(currentPreset));
                
                if (targetObject != null)
                {
                    bool isValid = ValidatePreset(currentPreset, targetObject);
                    GUILayout.Label($"对目标有效: {(isValid ? "是" : "否")}");
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            enablePresets = EditorGUILayout.Toggle("启用预设", enablePresets);
            presetCategory = EditorGUILayout.TextField("预设类别", presetCategory);
            autoApply = EditorGUILayout.Toggle("自动应用", autoApply);
            
            GUILayout.EndArea();
        }
    }
} 