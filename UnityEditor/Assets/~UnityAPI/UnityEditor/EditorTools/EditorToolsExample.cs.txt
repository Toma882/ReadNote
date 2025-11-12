using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using System.Collections.Generic;

namespace UnityEditor.EditorTools.Examples
{
    /// <summary>
    /// UnityEditor.EditorTools 命名空间使用示例
    /// 演示编辑器工具系统的创建、注册和管理功能
    /// </summary>
    public class EditorToolsExample : MonoBehaviour
    {
        [Header("编辑器工具配置")]
        [SerializeField] private bool enableEditorTools = true;
        [SerializeField] private string toolName = "CustomTool";
        [SerializeField] private string toolIconPath = "";
        [SerializeField] private bool isToolActive = false;
        
        [Header("工具状态")]
        [SerializeField] private int toolCount = 0;
        [SerializeField] private string currentTool = "";
        [SerializeField] private bool isToolValid = false;
        
        [Header("目标对象")]
        [SerializeField] private GameObject targetObject;
        [SerializeField] private Transform targetTransform;
        
        private List<EditorTool> availableTools = new List<EditorTool>();
        private Dictionary<string, EditorTool> toolRegistry = new Dictionary<string, EditorTool>();
        
        /// <summary>
        /// 初始化编辑器工具系统
        /// </summary>
        private void Start()
        {
            InitializeEditorToolsSystem();
        }
        
        /// <summary>
        /// 初始化编辑器工具系统
        /// </summary>
        private void InitializeEditorToolsSystem()
        {
            if (!enableEditorTools)
            {
                Debug.Log("编辑器工具系统已禁用");
                return;
            }
            
            try
            {
                // 加载可用工具
                LoadAvailableTools();
                
                // 注册默认工具
                RegisterDefaultTools();
                
                Debug.Log("编辑器工具系统初始化完成");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"编辑器工具系统初始化失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 加载可用工具
        /// </summary>
        private void LoadAvailableTools()
        {
            availableTools.Clear();
            toolRegistry.Clear();
            
            // 获取所有已注册的编辑器工具
            var tools = EditorToolManager.GetAvailableTools();
            foreach (var tool in tools)
            {
                availableTools.Add(tool);
                toolRegistry[tool.GetType().Name] = tool;
            }
            
            toolCount = availableTools.Count;
            Debug.Log($"加载了 {toolCount} 个编辑器工具");
        }
        
        /// <summary>
        /// 注册默认工具
        /// </summary>
        private void RegisterDefaultTools()
        {
            // 注册自定义工具
            RegisterCustomTool<CustomTransformTool>("CustomTransformTool");
            RegisterCustomTool<CustomScaleTool>("CustomScaleTool");
            RegisterCustomTool<CustomRotationTool>("CustomRotationTool");
        }
        
        /// <summary>
        /// 注册自定义工具
        /// </summary>
        public void RegisterCustomTool<T>(string toolName) where T : EditorTool, new()
        {
            try
            {
                var tool = new T();
                availableTools.Add(tool);
                toolRegistry[toolName] = tool;
                toolCount = availableTools.Count;
                
                Debug.Log($"自定义工具已注册: {toolName}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"注册自定义工具失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 激活工具
        /// </summary>
        public void ActivateTool(string toolName)
        {
            if (string.IsNullOrEmpty(toolName))
            {
                Debug.LogError("工具名称为空");
                return;
            }
            
            if (toolRegistry.ContainsKey(toolName))
            {
                var tool = toolRegistry[toolName];
                EditorToolManager.SetActiveTool(tool);
                currentTool = toolName;
                isToolActive = true;
                
                Debug.Log($"工具已激活: {toolName}");
            }
            else
            {
                Debug.LogWarning($"工具不存在: {toolName}");
            }
        }
        
        /// <summary>
        /// 停用工具
        /// </summary>
        public void DeactivateTool()
        {
            EditorToolManager.SetActiveTool(null);
            currentTool = "";
            isToolActive = false;
            
            Debug.Log("工具已停用");
        }
        
        /// <summary>
        /// 获取工具信息
        /// </summary>
        public string GetToolInfo(string toolName)
        {
            if (string.IsNullOrEmpty(toolName))
                return "工具名称为空";
            
            if (toolRegistry.ContainsKey(toolName))
            {
                var tool = toolRegistry[toolName];
                return $"名称: {toolName}, 类型: {tool.GetType().Name}, 激活: {tool.IsActive}";
            }
            
            return $"工具不存在: {toolName}";
        }
        
        /// <summary>
        /// 获取所有工具名称
        /// </summary>
        public string[] GetAllToolNames()
        {
            return availableTools.Select(t => t.GetType().Name).ToArray();
        }
        
        /// <summary>
        /// 验证工具有效性
        /// </summary>
        public bool ValidateTool(string toolName)
        {
            return toolRegistry.ContainsKey(toolName);
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 500, 600));
            GUILayout.Label("UnityEditor.EditorTools 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {(enableEditorTools ? "启用" : "禁用")}");
            GUILayout.Label($"工具数量: {toolCount}");
            GUILayout.Label($"当前工具: {currentTool}");
            GUILayout.Label($"工具激活: {(isToolActive ? "是" : "否")}");
            
            GUILayout.Space(10);
            GUILayout.Label("工具管理", EditorStyles.boldLabel);
            
            string[] toolNames = GetAllToolNames();
            if (toolNames.Length > 0)
            {
                int selectedIndex = EditorGUILayout.Popup("选择工具", 0, toolNames);
                if (selectedIndex >= 0 && selectedIndex < availableTools.Count)
                {
                    var selectedTool = availableTools[selectedIndex];
                    
                    GUILayout.Label(GetToolInfo(selectedTool.GetType().Name));
                    
                    if (GUILayout.Button("激活工具"))
                    {
                        ActivateTool(selectedTool.GetType().Name);
                    }
                }
            }
            
            if (GUILayout.Button("停用工具"))
            {
                DeactivateTool();
            }
            
            GUILayout.Space(10);
            GUILayout.Label("目标对象", EditorStyles.boldLabel);
            
            targetObject = (GameObject)EditorGUILayout.ObjectField("目标对象", targetObject, typeof(GameObject), true);
            targetTransform = (Transform)EditorGUILayout.ObjectField("目标变换", targetTransform, typeof(Transform), true);
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            enableEditorTools = EditorGUILayout.Toggle("启用编辑器工具", enableEditorTools);
            toolName = EditorGUILayout.TextField("工具名称", toolName);
            toolIconPath = EditorGUILayout.TextField("工具图标路径", toolIconPath);
            
            GUILayout.EndArea();
        }
    }
    
    /// <summary>
    /// 自定义变换工具
    /// </summary>
    [EditorTool("Custom Transform Tool")]
    public class CustomTransformTool : EditorTool
    {
        private Vector3 originalPosition;
        private bool isDragging = false;
        
        public override void OnToolGUI(EditorWindow window)
        {
            if (!(window is SceneView sceneView))
                return;
            
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 0)
                return;
            
            foreach (var obj in selectedObjects)
            {
                if (obj == null) continue;
                
                var transform = obj.transform;
                var position = transform.position;
                
                // 绘制位置手柄
                EditorGUI.BeginChangeCheck();
                var newPosition = Handles.PositionHandle(position, transform.rotation);
                
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(transform, "Move Object");
                    transform.position = newPosition;
                }
                
                // 绘制选择框
                Handles.color = Color.yellow;
                Handles.DrawWireCube(position, Vector3.one);
            }
        }
        
        public override bool IsAvailable()
        {
            return Selection.gameObjects.Length > 0;
        }
    }
    
    /// <summary>
    /// 自定义缩放工具
    /// </summary>
    [EditorTool("Custom Scale Tool")]
    public class CustomScaleTool : EditorTool
    {
        public override void OnToolGUI(EditorWindow window)
        {
            if (!(window is SceneView sceneView))
                return;
            
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 0)
                return;
            
            foreach (var obj in selectedObjects)
            {
                if (obj == null) continue;
                
                var transform = obj.transform;
                var position = transform.position;
                var scale = transform.localScale;
                
                // 绘制缩放手柄
                EditorGUI.BeginChangeCheck();
                var newScale = Handles.ScaleHandle(scale, position, transform.rotation, 1f);
                
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(transform, "Scale Object");
                    transform.localScale = newScale;
                }
                
                // 绘制选择框
                Handles.color = Color.green;
                Handles.DrawWireCube(position, scale);
            }
        }
        
        public override bool IsAvailable()
        {
            return Selection.gameObjects.Length > 0;
        }
    }
    
    /// <summary>
    /// 自定义旋转工具
    /// </summary>
    [EditorTool("Custom Rotation Tool")]
    public class CustomRotationTool : EditorTool
    {
        public override void OnToolGUI(EditorWindow window)
        {
            if (!(window is SceneView sceneView))
                return;
            
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 0)
                return;
            
            foreach (var obj in selectedObjects)
            {
                if (obj == null) continue;
                
                var transform = obj.transform;
                var position = transform.position;
                var rotation = transform.rotation;
                
                // 绘制旋转手柄
                EditorGUI.BeginChangeCheck();
                var newRotation = Handles.RotationHandle(rotation, position);
                
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(transform, "Rotate Object");
                    transform.rotation = newRotation;
                }
                
                // 绘制选择框
                Handles.color = Color.blue;
                Handles.DrawWireCube(position, transform.localScale);
            }
        }
        
        public override bool IsAvailable()
        {
            return Selection.gameObjects.Length > 0;
        }
    }
} 