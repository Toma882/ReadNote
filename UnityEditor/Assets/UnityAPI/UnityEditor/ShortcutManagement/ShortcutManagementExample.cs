using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using System.Collections.Generic;
using System.Linq;

namespace UnityEditor.ShortcutManagement.Examples
{
    /// <summary>
    /// UnityEditor.ShortcutManagement 命名空间使用示例
    /// 演示快捷键管理系统的注册、绑定和管理功能
    /// </summary>
    public class ShortcutManagementExample : MonoBehaviour
    {
        [Header("快捷键配置")]
        [SerializeField] private bool enableShortcuts = true;
        [SerializeField] private string shortcutName = "CustomShortcut";
        [SerializeField] private string shortcutKey = "Ctrl+Shift+S";
        [SerializeField] private string shortcutContext = "Game";
        
        [Header("快捷键状态")]
        [SerializeField] private int shortcutCount = 0;
        [SerializeField] private string lastTriggeredShortcut = "";
        [SerializeField] private bool isShortcutValid = false;
        
        [Header("快捷键数据")]
        [SerializeField] private List<ShortcutEntry> availableShortcuts = new List<ShortcutEntry>();
        [SerializeField] private ShortcutEntry currentShortcut;
        
        private Dictionary<string, ShortcutEntry> shortcutRegistry = new Dictionary<string, ShortcutEntry>();
        
        /// <summary>
        /// 快捷键条目
        /// </summary>
        [System.Serializable]
        public class ShortcutEntry
        {
            public string id;
            public string displayName;
            public string keyCombination;
            public string context;
            public bool isActive;
            
            public ShortcutEntry(string id, string displayName, string keyCombination, string context)
            {
                this.id = id;
                this.displayName = displayName;
                this.keyCombination = keyCombination;
                this.context = context;
                this.isActive = true;
            }
        }
        
        /// <summary>
        /// 初始化快捷键系统
        /// </summary>
        private void Start()
        {
            InitializeShortcutSystem();
        }
        
        /// <summary>
        /// 初始化快捷键系统
        /// </summary>
        private void InitializeShortcutSystem()
        {
            if (!enableShortcuts)
            {
                Debug.Log("快捷键系统已禁用");
                return;
            }
            
            try
            {
                // 加载现有快捷键
                LoadAvailableShortcuts();
                
                // 注册默认快捷键
                RegisterDefaultShortcuts();
                
                Debug.Log("快捷键系统初始化完成");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"快捷键系统初始化失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 加载可用快捷键
        /// </summary>
        private void LoadAvailableShortcuts()
        {
            availableShortcuts.Clear();
            shortcutRegistry.Clear();
            
            // 获取所有已注册的快捷键
            var shortcuts = ShortcutManager.instance.GetAvailableShortcuts();
            foreach (var shortcut in shortcuts)
            {
                var entry = new ShortcutEntry(
                    shortcut.identifier,
                    shortcut.displayName,
                    shortcut.keyCombination,
                    shortcut.context
                );
                
                availableShortcuts.Add(entry);
                shortcutRegistry[shortcut.identifier] = entry;
            }
            
            shortcutCount = availableShortcuts.Count;
            Debug.Log($"加载了 {shortcutCount} 个快捷键");
        }
        
        /// <summary>
        /// 注册默认快捷键
        /// </summary>
        private void RegisterDefaultShortcuts()
        {
            // 注册自定义快捷键
            RegisterShortcut("Custom/TestShortcut", "测试快捷键", "Ctrl+Shift+T", "Game");
            RegisterShortcut("Custom/SaveScene", "保存场景", "Ctrl+Shift+S", "Game");
            RegisterShortcut("Custom/LoadScene", "加载场景", "Ctrl+Shift+L", "Game");
            RegisterShortcut("Custom/ResetTransform", "重置变换", "Ctrl+Shift+R", "Game");
        }
        
        /// <summary>
        /// 注册快捷键
        /// </summary>
        public void RegisterShortcut(string shortcutId, string displayName, string keyCombination, string context)
        {
            if (string.IsNullOrEmpty(shortcutId))
            {
                Debug.LogError("快捷键ID为空");
                return;
            }
            
            try
            {
                // 创建快捷键定义
                var shortcutDef = new ShortcutDefinition
                {
                    identifier = shortcutId,
                    displayName = displayName,
                    keyCombination = keyCombination,
                    context = context
                };
                
                // 注册快捷键
                ShortcutManager.instance.RegisterShortcut(shortcutDef);
                
                // 添加到本地注册表
                var entry = new ShortcutEntry(shortcutId, displayName, keyCombination, context);
                availableShortcuts.Add(entry);
                shortcutRegistry[shortcutId] = entry;
                shortcutCount = availableShortcuts.Count;
                
                Debug.Log($"快捷键已注册: {shortcutId} ({keyCombination})");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"注册快捷键失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 注销快捷键
        /// </summary>
        public void UnregisterShortcut(string shortcutId)
        {
            if (string.IsNullOrEmpty(shortcutId))
            {
                Debug.LogError("快捷键ID为空");
                return;
            }
            
            try
            {
                // 注销快捷键
                ShortcutManager.instance.UnregisterShortcut(shortcutId);
                
                // 从本地注册表移除
                if (shortcutRegistry.ContainsKey(shortcutId))
                {
                    var entry = shortcutRegistry[shortcutId];
                    availableShortcuts.Remove(entry);
                    shortcutRegistry.Remove(shortcutId);
                    shortcutCount = availableShortcuts.Count;
                    
                    Debug.Log($"快捷键已注销: {shortcutId}");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"注销快捷键失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 绑定快捷键到方法
        /// </summary>
        [Shortcut("Custom/TestShortcut", KeyCode.T, ShortcutModifiers.Control | ShortcutModifiers.Shift)]
        public static void TestShortcut()
        {
            Debug.Log("测试快捷键被触发");
        }
        
        /// <summary>
        /// 保存场景快捷键
        /// </summary>
        [Shortcut("Custom/SaveScene", KeyCode.S, ShortcutModifiers.Control | ShortcutModifiers.Shift)]
        public static void SaveSceneShortcut()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
            Debug.Log("场景保存快捷键被触发");
        }
        
        /// <summary>
        /// 加载场景快捷键
        /// </summary>
        [Shortcut("Custom/LoadScene", KeyCode.L, ShortcutModifiers.Control | ShortcutModifiers.Shift)]
        public static void LoadSceneShortcut()
        {
            EditorApplication.ExecuteMenuItem("File/Open Scene");
            Debug.Log("场景加载快捷键被触发");
        }
        
        /// <summary>
        /// 重置变换快捷键
        /// </summary>
        [Shortcut("Custom/ResetTransform", KeyCode.R, ShortcutModifiers.Control | ShortcutModifiers.Shift)]
        public static void ResetTransformShortcut()
        {
            var selectedObjects = Selection.gameObjects;
            foreach (var obj in selectedObjects)
            {
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one;
            }
            Debug.Log("重置变换快捷键被触发");
        }
        
        /// <summary>
        /// 修改快捷键绑定
        /// </summary>
        public void ModifyShortcutBinding(string shortcutId, string newKeyCombination)
        {
            if (string.IsNullOrEmpty(shortcutId))
            {
                Debug.LogError("快捷键ID为空");
                return;
            }
            
            try
            {
                // 获取现有快捷键
                var shortcut = ShortcutManager.instance.GetShortcut(shortcutId);
                if (shortcut != null)
                {
                    // 创建新的快捷键定义
                    var newShortcutDef = new ShortcutDefinition
                    {
                        identifier = shortcut.identifier,
                        displayName = shortcut.displayName,
                        keyCombination = newKeyCombination,
                        context = shortcut.context
                    };
                    
                    // 注销旧快捷键
                    ShortcutManager.instance.UnregisterShortcut(shortcutId);
                    
                    // 注册新快捷键
                    ShortcutManager.instance.RegisterShortcut(newShortcutDef);
                    
                    // 更新本地注册表
                    if (shortcutRegistry.ContainsKey(shortcutId))
                    {
                        shortcutRegistry[shortcutId].keyCombination = newKeyCombination;
                    }
                    
                    Debug.Log($"快捷键绑定已修改: {shortcutId} -> {newKeyCombination}");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"修改快捷键绑定失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 启用/禁用快捷键
        /// </summary>
        public void SetShortcutActive(string shortcutId, bool isActive)
        {
            if (string.IsNullOrEmpty(shortcutId))
            {
                Debug.LogError("快捷键ID为空");
                return;
            }
            
            try
            {
                if (isActive)
                {
                    ShortcutManager.instance.EnableShortcut(shortcutId);
                }
                else
                {
                    ShortcutManager.instance.DisableShortcut(shortcutId);
                }
                
                // 更新本地状态
                if (shortcutRegistry.ContainsKey(shortcutId))
                {
                    shortcutRegistry[shortcutId].isActive = isActive;
                }
                
                Debug.Log($"快捷键状态已更改: {shortcutId} -> {(isActive ? "启用" : "禁用")}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"更改快捷键状态失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 获取快捷键信息
        /// </summary>
        public string GetShortcutInfo(string shortcutId)
        {
            if (string.IsNullOrEmpty(shortcutId))
                return "快捷键ID为空";
            
            try
            {
                var shortcut = ShortcutManager.instance.GetShortcut(shortcutId);
                if (shortcut != null)
                {
                    return $"ID: {shortcut.identifier}, 名称: {shortcut.displayName}, 按键: {shortcut.keyCombination}, 上下文: {shortcut.context}";
                }
                
                return $"快捷键不存在: {shortcutId}";
            }
            catch (System.Exception e)
            {
                return $"获取快捷键信息失败: {e.Message}";
            }
        }
        
        /// <summary>
        /// 验证快捷键有效性
        /// </summary>
        public bool ValidateShortcut(string keyCombination)
        {
            if (string.IsNullOrEmpty(keyCombination))
                return false;
            
            try
            {
                // 尝试解析按键组合
                var keyCode = ShortcutManager.instance.ParseKeyCombination(keyCombination);
                return keyCode != KeyCode.None;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 导出快捷键配置
        /// </summary>
        public void ExportShortcuts()
        {
            string exportPath = EditorUtility.SaveFilePanel("导出快捷键", "", "shortcuts", "json");
            if (string.IsNullOrEmpty(exportPath))
                return;
            
            try
            {
                var exportData = new Dictionary<string, object>();
                foreach (var shortcut in availableShortcuts)
                {
                    exportData[shortcut.id] = new
                    {
                        displayName = shortcut.displayName,
                        keyCombination = shortcut.keyCombination,
                        context = shortcut.context,
                        isActive = shortcut.isActive
                    };
                }
                
                string json = JsonUtility.ToJson(new { shortcuts = exportData }, true);
                System.IO.File.WriteAllText(exportPath, json);
                
                Debug.Log($"快捷键配置已导出到: {exportPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导出快捷键失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 导入快捷键配置
        /// </summary>
        public void ImportShortcuts()
        {
            string importPath = EditorUtility.OpenFilePanel("导入快捷键", "", "json");
            if (string.IsNullOrEmpty(importPath))
                return;
            
            try
            {
                string json = System.IO.File.ReadAllText(importPath);
                var importData = JsonUtility.FromJson<Dictionary<string, object>>(json);
                
                foreach (var shortcut in importData)
                {
                    // 这里需要根据实际JSON结构解析数据
                    Debug.Log($"导入快捷键: {shortcut.Key}");
                }
                
                Debug.Log($"快捷键配置已从 {importPath} 导入");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导入快捷键失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 重置所有快捷键
        /// </summary>
        public void ResetAllShortcuts()
        {
            try
            {
                ShortcutManager.instance.ResetToDefault();
                LoadAvailableShortcuts();
                Debug.Log("所有快捷键已重置为默认值");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"重置快捷键失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 获取所有快捷键ID
        /// </summary>
        public string[] GetAllShortcutIds()
        {
            return availableShortcuts.Select(s => s.id).ToArray();
        }
        
        /// <summary>
        /// 按上下文获取快捷键
        /// </summary>
        public ShortcutEntry[] GetShortcutsByContext(string context)
        {
            return availableShortcuts.Where(s => s.context == context).ToArray();
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 500, 700));
            GUILayout.Label("UnityEditor.ShortcutManagement 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {(enableShortcuts ? "启用" : "禁用")}");
            GUILayout.Label($"快捷键数量: {shortcutCount}");
            GUILayout.Label($"最后触发: {lastTriggeredShortcut}");
            GUILayout.Label($"快捷键有效: {(isShortcutValid ? "是" : "否")}");
            
            GUILayout.Space(10);
            GUILayout.Label("快捷键注册", EditorStyles.boldLabel);
            
            shortcutName = GUILayout.TextField("快捷键名称", shortcutName);
            shortcutKey = GUILayout.TextField("按键组合", shortcutKey);
            shortcutContext = GUILayout.TextField("上下文", shortcutContext);
            
            if (GUILayout.Button("注册快捷键"))
            {
                RegisterShortcut(shortcutName, shortcutName, shortcutKey, shortcutContext);
            }
            
            if (GUILayout.Button("验证按键组合"))
            {
                isShortcutValid = ValidateShortcut(shortcutKey);
                Debug.Log($"按键组合验证结果: {(isShortcutValid ? "有效" : "无效")}");
            }
            
            GUILayout.Space(10);
            GUILayout.Label("快捷键管理", EditorStyles.boldLabel);
            
            string[] shortcutIds = GetAllShortcutIds();
            if (shortcutIds.Length > 0)
            {
                int selectedIndex = EditorGUILayout.Popup("选择快捷键", 0, shortcutIds);
                if (selectedIndex >= 0 && selectedIndex < availableShortcuts.Count)
                {
                    currentShortcut = availableShortcuts[selectedIndex];
                    
                    GUILayout.Label(GetShortcutInfo(currentShortcut.id));
                    
                    string newKey = GUILayout.TextField("新按键组合", currentShortcut.keyCombination);
                    if (newKey != currentShortcut.keyCombination)
                    {
                        ModifyShortcutBinding(currentShortcut.id, newKey);
                    }
                    
                    bool isActive = EditorGUILayout.Toggle("启用", currentShortcut.isActive);
                    if (isActive != currentShortcut.isActive)
                    {
                        SetShortcutActive(currentShortcut.id, isActive);
                    }
                    
                    if (GUILayout.Button("注销快捷键"))
                    {
                        UnregisterShortcut(currentShortcut.id);
                    }
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("快捷键操作", EditorStyles.boldLabel);
            
            if (GUILayout.Button("测试快捷键"))
            {
                TestShortcut();
                lastTriggeredShortcut = "Custom/TestShortcut";
            }
            
            if (GUILayout.Button("保存场景"))
            {
                SaveSceneShortcut();
                lastTriggeredShortcut = "Custom/SaveScene";
            }
            
            if (GUILayout.Button("重置变换"))
            {
                ResetTransformShortcut();
                lastTriggeredShortcut = "Custom/ResetTransform";
            }
            
            GUILayout.Space(10);
            GUILayout.Label("导入导出", EditorStyles.boldLabel);
            
            if (GUILayout.Button("导出快捷键"))
            {
                ExportShortcuts();
            }
            
            if (GUILayout.Button("导入快捷键"))
            {
                ImportShortcuts();
            }
            
            if (GUILayout.Button("重置所有快捷键"))
            {
                ResetAllShortcuts();
            }
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            enableShortcuts = EditorGUILayout.Toggle("启用快捷键", enableShortcuts);
            
            GUILayout.EndArea();
        }
    }
} 