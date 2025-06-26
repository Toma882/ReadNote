using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;

/// <summary>
/// UnityEditor.Toolbars 命名空间案例演示
/// 展示工具栏系统的核心功能，包括自定义工具栏、工具按钮、下拉菜单等
/// </summary>
public class ToolbarsExample : MonoBehaviour
{
    [Header("工具栏配置")]
    [SerializeField] private bool enableToolbars = true; //启用工具栏
    [SerializeField] private bool enableCustomToolbar = true; //启用自定义工具栏
    [SerializeField] private bool enableToolbarElements = true; //启用工具栏元素
    [SerializeField] private bool enableToolbarGroups = true; //启用工具栏组
    [SerializeField] private bool enableToolbarDropdowns = true; //启用工具栏下拉菜单
    [SerializeField] private bool enableToolbarToggles = true; //启用工具栏开关
    
    [Header("工具栏布局")]
    [SerializeField] private ToolbarZone toolbarZone = ToolbarZone.ToolbarZoneRightAlign; //工具栏区域
    [SerializeField] private int toolbarPriority = 100; //工具栏优先级
    [SerializeField] private string toolbarId = "CustomToolbar"; //工具栏ID
    [SerializeField] private string toolbarName = "自定义工具栏"; //工具栏名称
    [SerializeField] private bool toolbarVisible = true; //工具栏可见性
    [SerializeField] private bool toolbarEnabled = true; //工具栏启用状态
    
    [Header("工具栏元素配置")]
    [SerializeField] private string[] toolbarElementIds; //工具栏元素ID数组
    [SerializeField] private string[] toolbarElementNames; //工具栏元素名称数组
    [SerializeField] private Texture2D[] toolbarElementIcons; //工具栏元素图标数组
    [SerializeField] private bool[] toolbarElementStates; //工具栏元素状态数组
    [SerializeField] private int activeElementIndex = 0; //活动元素索引
    [SerializeField] private int maxElements = 10; //最大元素数量
    
    [Header("工具栏组配置")]
    [SerializeField] private string[] toolbarGroupIds; //工具栏组ID数组
    [SerializeField] private string[] toolbarGroupNames; //工具栏组名称数组
    [SerializeField] private bool[] toolbarGroupStates; //工具栏组状态数组
    [SerializeField] private int activeGroupIndex = 0; //活动组索引
    [SerializeField] private int maxGroups = 5; //最大组数量
    
    [Header("工具栏状态")]
    [SerializeField] private string toolbarsState = "未初始化"; //工具栏状态
    [SerializeField] private string currentToolbarMode = "空闲"; //当前工具栏模式
    [SerializeField] private bool isToolbarDirty = false; //工具栏是否脏
    [SerializeField] private bool isLayoutDirty = false; //布局是否脏
    [SerializeField] private Vector2 toolbarSize = Vector2.zero; //工具栏大小
    [SerializeField] private Vector2 toolbarPosition = Vector2.zero; //工具栏位置
    
    [Header("性能监控")]
    [SerializeField] private bool enableToolbarMonitoring = true; //启用工具栏监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logToolbarData = false; //记录工具栏数据
    [SerializeField] private float toolbarUpdateTime = 0f; //工具栏更新时间
    [SerializeField] private int totalElements = 0; //总元素数
    [SerializeField] private int totalGroups = 0; //总组数
    [SerializeField] private float memoryUsage = 0f; //内存使用量
    
    [Header("工具栏数据")]
    [SerializeField] private ToolbarElementData[] elementData; //元素数据
    [SerializeField] private ToolbarGroupData[] groupData; //组数据
    [SerializeField] private ToolbarEventData[] eventData; //事件数据
    [SerializeField] private string[] toolbarLogs; //工具栏日志
    
    private Toolbar customToolbar;
    private System.Collections.Generic.List<IToolbarElement> toolbarElements;
    private System.Collections.Generic.List<IToolbarGroup> toolbarGroups;
    private System.Collections.Generic.List<ToolbarElementData> elementDataList;
    private System.Collections.Generic.List<ToolbarGroupData> groupDataList;
    private System.Collections.Generic.List<ToolbarEventData> eventDataList;
    private System.Collections.Generic.List<string> toolbarLogList;
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;
    private int eventCounter = 0;

    private void Start()
    {
        InitializeToolbars();
    }

    /// <summary>
    /// 初始化工具栏
    /// </summary>
    private void InitializeToolbars()
    {
        // 初始化工具栏元素列表
        InitializeToolbarElements();
        
        // 初始化工具栏组列表
        InitializeToolbarGroups();
        
        // 初始化数据列表
        InitializeDataLists();
        
        // 创建自定义工具栏
        CreateCustomToolbar();
        
        // 注册工具栏事件
        RegisterToolbarEvents();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置工具栏
        ConfigureToolbars();
        
        isInitialized = true;
        toolbarsState = "已初始化";
        Debug.Log("工具栏系统初始化完成");
    }

    /// <summary>
    /// 初始化工具栏元素列表
    /// </summary>
    private void InitializeToolbarElements()
    {
        toolbarElements = new System.Collections.Generic.List<IToolbarElement>();
        
        // 创建默认工具栏元素
        for (int i = 0; i < maxElements; i++)
        {
            var element = CreateToolbarElement($"Element_{i}", $"元素_{i}", null, i % 2 == 0);
            toolbarElements.Add(element);
        }
        
        Debug.Log($"工具栏元素初始化完成: {toolbarElements.Count} 个元素");
    }

    /// <summary>
    /// 初始化工具栏组列表
    /// </summary>
    private void InitializeToolbarGroups()
    {
        toolbarGroups = new System.Collections.Generic.List<IToolbarGroup>();
        
        // 创建默认工具栏组
        for (int i = 0; i < maxGroups; i++)
        {
            var group = CreateToolbarGroup($"Group_{i}", $"组_{i}", i % 2 == 0);
            toolbarGroups.Add(group);
        }
        
        Debug.Log($"工具栏组初始化完成: {toolbarGroups.Count} 个组");
    }

    /// <summary>
    /// 初始化数据列表
    /// </summary>
    private void InitializeDataLists()
    {
        elementDataList = new System.Collections.Generic.List<ToolbarElementData>();
        groupDataList = new System.Collections.Generic.List<ToolbarGroupData>();
        eventDataList = new System.Collections.Generic.List<ToolbarEventData>();
        toolbarLogList = new System.Collections.Generic.List<string>();
        
        Debug.Log("数据列表初始化完成");
    }

    /// <summary>
    /// 创建自定义工具栏
    /// </summary>
    private void CreateCustomToolbar()
    {
        if (!enableCustomToolbar) return;
        
        // 创建工具栏
        customToolbar = new Toolbar(toolbarId, toolbarName);
        customToolbar.zone = toolbarZone;
        customToolbar.priority = toolbarPriority;
        customToolbar.visible = toolbarVisible;
        customToolbar.enabled = toolbarEnabled;
        
        // 添加工具栏元素
        foreach (var element in toolbarElements)
        {
            customToolbar.AddElement(element);
        }
        
        // 添加工具栏组
        foreach (var group in toolbarGroups)
        {
            customToolbar.AddGroup(group);
        }
        
        Debug.Log($"自定义工具栏创建完成: {toolbarName}");
    }

    /// <summary>
    /// 注册工具栏事件
    /// </summary>
    private void RegisterToolbarEvents()
    {
        if (customToolbar != null)
        {
            customToolbar.onElementAdded += OnToolbarElementAdded;
            customToolbar.onElementRemoved += OnToolbarElementRemoved;
            customToolbar.onGroupAdded += OnToolbarGroupAdded;
            customToolbar.onGroupRemoved += OnToolbarGroupRemoved;
            customToolbar.onVisibilityChanged += OnToolbarVisibilityChanged;
            customToolbar.onEnabledChanged += OnToolbarEnabledChanged;
        }
        
        Debug.Log("工具栏事件注册完成");
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        lastMonitoringTime = Time.time;
        Debug.Log("性能监控初始化完成");
    }

    /// <summary>
    /// 配置工具栏
    /// </summary>
    private void ConfigureToolbars()
    {
        // 配置工具栏元素
        ConfigureToolbarElements();
        
        // 配置工具栏组
        ConfigureToolbarGroups();
        
        Debug.Log("工具栏配置完成");
    }

    /// <summary>
    /// 配置工具栏元素
    /// </summary>
    private void ConfigureToolbarElements()
    {
        if (toolbarElementIds != null && toolbarElementIds.Length > 0)
        {
            for (int i = 0; i < Mathf.Min(toolbarElementIds.Length, toolbarElements.Count); i++)
            {
                var element = toolbarElements[i];
                if (element is CustomToolbarElement customElement)
                {
                    customElement.elementId = toolbarElementIds[i];
                    customElement.elementName = toolbarElementNames != null && i < toolbarElementNames.Length ? toolbarElementNames[i] : $"元素_{i}";
                    customElement.elementIcon = toolbarElementIcons != null && i < toolbarElementIcons.Length ? toolbarElementIcons[i] : null;
                    customElement.elementState = toolbarElementStates != null && i < toolbarElementStates.Length ? toolbarElementStates[i] : false;
                }
            }
        }
    }

    /// <summary>
    /// 配置工具栏组
    /// </summary>
    private void ConfigureToolbarGroups()
    {
        if (toolbarGroupIds != null && toolbarGroupIds.Length > 0)
        {
            for (int i = 0; i < Mathf.Min(toolbarGroupIds.Length, toolbarGroups.Count); i++)
            {
                var group = toolbarGroups[i];
                if (group is CustomToolbarGroup customGroup)
                {
                    customGroup.groupId = toolbarGroupIds[i];
                    customGroup.groupName = toolbarGroupNames != null && i < toolbarGroupNames.Length ? toolbarGroupNames[i] : $"组_{i}";
                    customGroup.groupState = toolbarGroupStates != null && i < toolbarGroupStates.Length ? toolbarGroupStates[i] : false;
                }
            }
        }
    }

    private void Update()
    {
        if (!isInitialized || !enableToolbars) return;
        
        // 更新性能监控
        if (enableToolbarMonitoring && Time.time - lastMonitoringTime >= monitoringInterval)
        {
            MonitorToolbarPerformance();
            lastMonitoringTime = Time.time;
        }
        
        // 更新工具栏状态
        UpdateToolbarStatus();
        
        // 处理工具栏输入
        HandleToolbarInput();
    }

    /// <summary>
    /// 监控工具栏性能
    /// </summary>
    private void MonitorToolbarPerformance()
    {
        totalElements = toolbarElements != null ? toolbarElements.Count : 0;
        totalGroups = toolbarGroups != null ? toolbarGroups.Count : 0;
        memoryUsage = (totalElements + totalGroups) * 0.1f; // 估算内存使用量 (MB)
        
        if (logToolbarData)
        {
            Debug.Log($"工具栏性能数据 - 元素数: {totalElements}, 组数: {totalGroups}, 内存使用: {memoryUsage:F2}MB");
        }
    }

    /// <summary>
    /// 更新工具栏状态
    /// </summary>
    private void UpdateToolbarStatus()
    {
        if (customToolbar != null)
        {
            toolbarVisible = customToolbar.visible;
            toolbarEnabled = customToolbar.enabled;
            toolbarSize = customToolbar.size;
            toolbarPosition = customToolbar.position;
        }
    }

    /// <summary>
    /// 处理工具栏输入
    /// </summary>
    private void HandleToolbarInput()
    {
        // 处理键盘快捷键
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleToolbarVisibility();
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleToolbarEnabled();
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            AddRandomElement();
        }
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            RemoveRandomElement();
        }
    }

    /// <summary>
    /// 创建工具栏元素
    /// </summary>
    private IToolbarElement CreateToolbarElement(string id, string name, Texture2D icon, bool initialState)
    {
        var element = new CustomToolbarElement
        {
            elementId = id,
            elementName = name,
            elementIcon = icon,
            elementState = initialState
        };
        
        element.onElementClicked += OnElementClicked;
        element.onElementStateChanged += OnElementStateChanged;
        
        return element;
    }

    /// <summary>
    /// 创建工具栏组
    /// </summary>
    private IToolbarGroup CreateToolbarGroup(string id, string name, bool initialState)
    {
        var group = new CustomToolbarGroup
        {
            groupId = id,
            groupName = name,
            groupState = initialState
        };
        
        group.onGroupClicked += OnGroupClicked;
        group.onGroupStateChanged += OnGroupStateChanged;
        
        return group;
    }

    /// <summary>
    /// 切换工具栏可见性
    /// </summary>
    public void ToggleToolbarVisibility()
    {
        if (customToolbar != null)
        {
            customToolbar.visible = !customToolbar.visible;
            LogToolbarEvent("切换工具栏可见性", customToolbar.visible ? "显示" : "隐藏");
        }
    }

    /// <summary>
    /// 切换工具栏启用状态
    /// </summary>
    public void ToggleToolbarEnabled()
    {
        if (customToolbar != null)
        {
            customToolbar.enabled = !customToolbar.enabled;
            LogToolbarEvent("切换工具栏启用状态", customToolbar.enabled ? "启用" : "禁用");
        }
    }

    /// <summary>
    /// 添加随机元素
    /// </summary>
    public void AddRandomElement()
    {
        if (toolbarElements.Count >= maxElements) return;
        
        int randomIndex = Random.Range(0, 1000);
        var element = CreateToolbarElement($"RandomElement_{randomIndex}", $"随机元素_{randomIndex}", null, Random.value > 0.5f);
        toolbarElements.Add(element);
        
        if (customToolbar != null)
        {
            customToolbar.AddElement(element);
        }
        
        LogToolbarEvent("添加随机元素", $"元素_{randomIndex}");
    }

    /// <summary>
    /// 移除随机元素
    /// </summary>
    public void RemoveRandomElement()
    {
        if (toolbarElements.Count <= 0) return;
        
        int randomIndex = Random.Range(0, toolbarElements.Count);
        var element = toolbarElements[randomIndex];
        toolbarElements.RemoveAt(randomIndex);
        
        if (customToolbar != null)
        {
            customToolbar.RemoveElement(element);
        }
        
        LogToolbarEvent("移除随机元素", $"索引_{randomIndex}");
    }

    /// <summary>
    /// 设置活动元素
    /// </summary>
    public void SetActiveElement(int index)
    {
        if (toolbarElements != null && index >= 0 && index < toolbarElements.Count)
        {
            activeElementIndex = index;
            LogToolbarEvent("设置活动元素", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动组
    /// </summary>
    public void SetActiveGroup(int index)
    {
        if (toolbarGroups != null && index >= 0 && index < toolbarGroups.Count)
        {
            activeGroupIndex = index;
            LogToolbarEvent("设置活动组", $"索引_{index}");
        }
    }

    /// <summary>
    /// 生成工具栏报告
    /// </summary>
    public void GenerateToolbarReport()
    {
        ToolbarReportData reportData = new ToolbarReportData
        {
            timestamp = System.DateTime.Now.ToString(),
            toolbarsState = toolbarsState,
            currentToolbarMode = currentToolbarMode,
            toolbarId = toolbarId,
            toolbarName = toolbarName,
            toolbarVisible = toolbarVisible,
            toolbarEnabled = toolbarEnabled,
            totalElements = totalElements,
            totalGroups = totalGroups,
            activeElementIndex = activeElementIndex,
            activeGroupIndex = activeGroupIndex,
            memoryUsage = memoryUsage,
            toolbarSize = toolbarSize,
            toolbarPosition = toolbarPosition
        };
        
        string reportJson = JsonUtility.ToJson(reportData, true);
        Debug.Log($"工具栏报告生成完成:\n{reportJson}");
    }

    /// <summary>
    /// 导出工具栏数据
    /// </summary>
    public void ExportToolbarData()
    {
        // 导出元素数据
        elementData = elementDataList.ToArray();
        
        // 导出组数据
        groupData = groupDataList.ToArray();
        
        // 导出事件数据
        eventData = eventDataList.ToArray();
        
        // 导出日志数据
        toolbarLogs = toolbarLogList.ToArray();
        
        Debug.Log("工具栏数据导出完成");
    }

    /// <summary>
    /// 记录工具栏事件
    /// </summary>
    private void LogToolbarEvent(string eventType, string eventData)
    {
        var eventInfo = new ToolbarEventData
        {
            eventId = eventCounter++,
            timestamp = System.DateTime.Now.ToString(),
            eventType = eventType,
            eventData = eventData
        };
        
        eventDataList.Add(eventInfo);
        toolbarLogList.Add($"[{eventInfo.timestamp}] {eventInfo.eventType}: {eventInfo.eventData}");
        
        // 限制日志数量
        if (toolbarLogList.Count > 100)
        {
            toolbarLogList.RemoveAt(0);
        }
    }

    // 事件处理函数
    private void OnToolbarElementAdded(IToolbarElement element)
    {
        LogToolbarEvent("元素添加", element.GetType().Name);
    }

    private void OnToolbarElementRemoved(IToolbarElement element)
    {
        LogToolbarEvent("元素移除", element.GetType().Name);
    }

    private void OnToolbarGroupAdded(IToolbarGroup group)
    {
        LogToolbarEvent("组添加", group.GetType().Name);
    }

    private void OnToolbarGroupRemoved(IToolbarGroup group)
    {
        LogToolbarEvent("组移除", group.GetType().Name);
    }

    private void OnToolbarVisibilityChanged(bool visible)
    {
        LogToolbarEvent("可见性改变", visible ? "显示" : "隐藏");
    }

    private void OnToolbarEnabledChanged(bool enabled)
    {
        LogToolbarEvent("启用状态改变", enabled ? "启用" : "禁用");
    }

    private void OnElementClicked(string elementId)
    {
        LogToolbarEvent("元素点击", elementId);
    }

    private void OnElementStateChanged(string elementId, bool state)
    {
        LogToolbarEvent("元素状态改变", $"{elementId}: {state}");
    }

    private void OnGroupClicked(string groupId)
    {
        LogToolbarEvent("组点击", groupId);
    }

    private void OnGroupStateChanged(string groupId, bool state)
    {
        LogToolbarEvent("组状态改变", $"{groupId}: {state}");
    }

    private void OnGUI()
    {
        if (!isInitialized) return;
        
        GUILayout.BeginArea(new Rect(Screen.width - 310, 10, 300, Screen.height - 20));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label("工具栏系统", EditorStyles.boldLabel);
        
        // 工具栏配置
        GUILayout.Space(10);
        GUILayout.Label("工具栏配置", EditorStyles.boldLabel);
        enableToolbars = GUILayout.Toggle(enableToolbars, "启用工具栏");
        enableCustomToolbar = GUILayout.Toggle(enableCustomToolbar, "启用自定义工具栏");
        enableToolbarElements = GUILayout.Toggle(enableToolbarElements, "启用工具栏元素");
        enableToolbarGroups = GUILayout.Toggle(enableToolbarGroups, "启用工具栏组");
        enableToolbarDropdowns = GUILayout.Toggle(enableToolbarDropdowns, "启用工具栏下拉菜单");
        enableToolbarToggles = GUILayout.Toggle(enableToolbarToggles, "启用工具栏开关");
        
        // 工具栏状态
        GUILayout.Space(10);
        GUILayout.Label("工具栏状态", EditorStyles.boldLabel);
        GUILayout.Label($"状态: {toolbarsState}");
        GUILayout.Label($"模式: {currentToolbarMode}");
        GUILayout.Label($"可见性: {toolbarVisible}");
        GUILayout.Label($"启用状态: {toolbarEnabled}");
        GUILayout.Label($"元素数: {totalElements}");
        GUILayout.Label($"组数: {totalGroups}");
        GUILayout.Label($"内存使用: {memoryUsage:F2}MB");
        
        // 工具栏操作
        GUILayout.Space(10);
        GUILayout.Label("工具栏操作", EditorStyles.boldLabel);
        if (GUILayout.Button("切换可见性")) ToggleToolbarVisibility();
        if (GUILayout.Button("切换启用状态")) ToggleToolbarEnabled();
        if (GUILayout.Button("添加随机元素")) AddRandomElement();
        if (GUILayout.Button("移除随机元素")) RemoveRandomElement();
        
        // 工具栏数据
        GUILayout.Space(10);
        GUILayout.Label("工具栏数据", EditorStyles.boldLabel);
        if (GUILayout.Button("生成报告")) GenerateToolbarReport();
        if (GUILayout.Button("导出数据")) ExportToolbarData();
        if (GUILayout.Button("清空日志")) toolbarLogList.Clear();
        
        // 快捷键提示
        GUILayout.Space(10);
        GUILayout.Label("快捷键", EditorStyles.boldLabel);
        GUILayout.Label("F1: 切换可见性");
        GUILayout.Label("F2: 切换启用状态");
        GUILayout.Label("F3: 添加随机元素");
        GUILayout.Label("F4: 移除随机元素");
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        if (customToolbar != null)
        {
            customToolbar.onElementAdded -= OnToolbarElementAdded;
            customToolbar.onElementRemoved -= OnToolbarElementRemoved;
            customToolbar.onGroupAdded -= OnToolbarGroupAdded;
            customToolbar.onGroupRemoved -= OnToolbarGroupRemoved;
            customToolbar.onVisibilityChanged -= OnToolbarVisibilityChanged;
            customToolbar.onEnabledChanged -= OnToolbarEnabledChanged;
        }
    }
}

/// <summary>
/// 自定义工具栏元素
/// </summary>
public class CustomToolbarElement : IToolbarElement
{
    public string elementId;
    public string elementName;
    public Texture2D elementIcon;
    public bool elementState;
    
    public System.Action<string> onElementClicked;
    public System.Action<string, bool> onElementStateChanged;
    
    public string id => elementId;
    public string name => elementName;
    public Texture2D icon => elementIcon;
    public bool state => elementState;
    
    public void OnClick()
    {
        onElementClicked?.Invoke(elementId);
    }
    
    public void OnStateChanged(bool newState)
    {
        elementState = newState;
        onElementStateChanged?.Invoke(elementId, newState);
    }
}

/// <summary>
/// 自定义工具栏组
/// </summary>
public class CustomToolbarGroup : IToolbarGroup
{
    public string groupId;
    public string groupName;
    public bool groupState;
    
    public System.Action<string> onGroupClicked;
    public System.Action<string, bool> onGroupStateChanged;
    
    public string id => groupId;
    public string name => groupName;
    public bool state => groupState;
    
    public void OnClick()
    {
        onGroupClicked?.Invoke(groupId);
    }
    
    public void OnStateChanged(bool newState)
    {
        groupState = newState;
        onGroupStateChanged?.Invoke(groupId, newState);
    }
}

/// <summary>
/// 工具栏元素数据
/// </summary>
[System.Serializable]
public class ToolbarElementData
{
    public string elementId;
    public string elementName;
    public bool elementState;
    public Vector2 elementPosition;
    public Vector2 elementSize;
}

/// <summary>
/// 工具栏组数据
/// </summary>
[System.Serializable]
public class ToolbarGroupData
{
    public string groupId;
    public string groupName;
    public bool groupState;
    public Vector2 groupPosition;
    public Vector2 groupSize;
}

/// <summary>
/// 工具栏事件数据
/// </summary>
[System.Serializable]
public class ToolbarEventData
{
    public int eventId;
    public string timestamp;
    public string eventType;
    public string eventData;
}

/// <summary>
/// 工具栏报告数据
/// </summary>
[System.Serializable]
public class ToolbarReportData
{
    public string timestamp;
    public string toolbarsState;
    public string currentToolbarMode;
    public string toolbarId;
    public string toolbarName;
    public bool toolbarVisible;
    public bool toolbarEnabled;
    public int totalElements;
    public int totalGroups;
    public int activeElementIndex;
    public int activeGroupIndex;
    public float memoryUsage;
    public Vector2 toolbarSize;
    public Vector2 toolbarPosition;
} 