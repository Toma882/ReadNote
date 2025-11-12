using UnityEngine;
using UnityEditor;
using UnityEditor.Overlays;

/// <summary>
/// UnityEditor.Overlays 命名空间案例演示
/// 展示覆盖层系统的核心功能，包括自定义覆盖层、覆盖层布局、覆盖层交互等
/// </summary>
public class OverlaysExample : MonoBehaviour
{
    [Header("覆盖层配置")]
    [SerializeField] private bool enableOverlays = true; //启用覆盖层
    [SerializeField] private bool enableCustomOverlay = true; //启用自定义覆盖层
    [SerializeField] private bool enableOverlayLayout = true; //启用覆盖层布局
    [SerializeField] private bool enableOverlayInteraction = true; //启用覆盖层交互
    [SerializeField] private bool enableOverlayAnimation = true; //启用覆盖层动画
    [SerializeField] private bool enableOverlayStyling = true; //启用覆盖层样式
    
    [Header("覆盖层布局")]
    [SerializeField] private OverlayContainerType overlayContainer = OverlayContainerType.Toolbar; //覆盖层容器
    [SerializeField] private OverlayAlignment overlayAlignment = OverlayAlignment.Top; //覆盖层对齐
    [SerializeField] private int overlayPriority = 100; //覆盖层优先级
    [SerializeField] private string overlayId = "CustomOverlay"; //覆盖层ID
    [SerializeField] private string overlayName = "自定义覆盖层"; //覆盖层名称
    [SerializeField] private bool overlayVisible = true; //覆盖层可见性
    [SerializeField] private bool overlayEnabled = true; //覆盖层启用状态
    [SerializeField] private Vector2 overlaySize = new Vector2(200, 100); //覆盖层大小
    [SerializeField] private Vector2 overlayPosition = Vector2.zero; //覆盖层位置
    
    [Header("覆盖层元素配置")]
    [SerializeField] private string[] overlayElementIds; //覆盖层元素ID数组
    [SerializeField] private string[] overlayElementNames; //覆盖层元素名称数组
    [SerializeField] private Texture2D[] overlayElementIcons; //覆盖层元素图标数组
    [SerializeField] private bool[] overlayElementStates; //覆盖层元素状态数组
    [SerializeField] private OverlayElementType[] overlayElementTypes; //覆盖层元素类型数组
    [SerializeField] private int activeElementIndex = 0; //活动元素索引
    [SerializeField] private int maxElements = 10; //最大元素数量
    [SerializeField] private bool enableElementGrouping = true; //启用元素分组
    [SerializeField] private bool enableElementDragging = true; //启用元素拖拽
    
    [Header("覆盖层样式配置")]
    [SerializeField] private Color overlayBackgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.8f); //覆盖层背景色
    [SerializeField] private Color overlayTextColor = Color.white; //覆盖层文本色
    [SerializeField] private Color overlayBorderColor = new Color(0.5f, 0.5f, 0.5f, 1f); //覆盖层边框色
    [SerializeField] private float overlayOpacity = 0.9f; //覆盖层透明度
    [SerializeField] private float overlayBorderWidth = 1f; //覆盖层边框宽度
    [SerializeField] private float overlayCornerRadius = 5f; //覆盖层圆角半径
    [SerializeField] private bool enableOverlayShadow = true; //启用覆盖层阴影
    [SerializeField] private Vector2 overlayShadowOffset = new Vector2(2f, 2f); //覆盖层阴影偏移
    [SerializeField] private float overlayShadowBlur = 3f; //覆盖层阴影模糊
    
    [Header("覆盖层动画配置")]
    [SerializeField] private bool enableOverlayFade = true; //启用覆盖层淡入淡出
    [SerializeField] private bool enableOverlaySlide = true; //启用覆盖层滑动
    [SerializeField] private bool enableOverlayScale = true; //启用覆盖层缩放
    [SerializeField] private float overlayAnimationDuration = 0.3f; //覆盖层动画持续时间
    [SerializeField] private OverlayAnimationType overlayAnimationType = OverlayAnimationType.Fade; //覆盖层动画类型
    [SerializeField] private OverlayAnimationEase overlayAnimationEase = OverlayAnimationEase.EaseOut; //覆盖层动画缓动
    [SerializeField] private bool enableOverlayHover = true; //启用覆盖层悬停
    [SerializeField] private float overlayHoverScale = 1.1f; //覆盖层悬停缩放
    [SerializeField] private Color overlayHoverColor = new Color(0.3f, 0.3f, 0.3f, 0.9f); //覆盖层悬停颜色
    
    [Header("覆盖层交互配置")]
    [SerializeField] private bool enableOverlayClick = true; //启用覆盖层点击
    [SerializeField] private bool enableOverlayDrag = true; //启用覆盖层拖拽
    [SerializeField] private bool enableOverlayResize = true; //启用覆盖层调整大小
    [SerializeField] private bool enableOverlayContextMenu = true; //启用覆盖层右键菜单
    [SerializeField] private bool enableOverlayTooltip = true; //启用覆盖层工具提示
    [SerializeField] private float overlayTooltipDelay = 0.5f; //覆盖层工具提示延迟
    [SerializeField] private string overlayTooltipText = "自定义覆盖层"; //覆盖层工具提示文本
    [SerializeField] private bool enableOverlayKeyboard = true; //启用覆盖层键盘支持
    
    [Header("覆盖层状态")]
    [SerializeField] private string overlaysState = "未初始化"; //覆盖层状态
    [SerializeField] private string currentOverlayMode = "空闲"; //当前覆盖层模式
    [SerializeField] private bool isOverlayDirty = false; //覆盖层是否脏
    [SerializeField] private bool isLayoutDirty = false; //布局是否脏
    [SerializeField] private bool isStyleDirty = false; //样式是否脏
    [SerializeField] private Vector2 overlayBounds = Vector2.zero; //覆盖层边界
    [SerializeField] private Vector2 overlayCenter = Vector2.zero; //覆盖层中心
    
    [Header("性能监控")]
    [SerializeField] private bool enableOverlayMonitoring = true; //启用覆盖层监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logOverlayData = false; //记录覆盖层数据
    [SerializeField] private float overlayUpdateTime = 0f; //覆盖层更新时间
    [SerializeField] private int totalElements = 0; //总元素数
    [SerializeField] private int totalGroups = 0; //总组数
    [SerializeField] private float memoryUsage = 0f; //内存使用量
    [SerializeField] private float renderTime = 0f; //渲染时间
    
    [Header("覆盖层数据")]
    [SerializeField] private OverlayElementData[] elementData; //元素数据
    [SerializeField] private OverlayGroupData[] groupData; //组数据
    [SerializeField] private OverlayEventData[] eventData; //事件数据
    [SerializeField] private OverlayStyleData[] styleData; //样式数据
    [SerializeField] private string[] overlayLogs; //覆盖层日志
    
    private Overlay customOverlay;
    private System.Collections.Generic.List<IOverlayElement> overlayElements;
    private System.Collections.Generic.List<IOverlayGroup> overlayGroups;
    private System.Collections.Generic.List<OverlayElementData> elementDataList;
    private System.Collections.Generic.List<OverlayGroupData> groupDataList;
    private System.Collections.Generic.List<OverlayEventData> eventDataList;
    private System.Collections.Generic.List<OverlayStyleData> styleDataList;
    private System.Collections.Generic.List<string> overlayLogList;
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;
    private int eventCounter = 0;
    private float animationTime = 0f;
    private bool isHovering = false;
    private Vector2 lastMousePosition = Vector2.zero;

    private void Start()
    {
        InitializeOverlays();
    }

    /// <summary>
    /// 初始化覆盖层
    /// </summary>
    private void InitializeOverlays()
    {
        // 初始化覆盖层元素列表
        InitializeOverlayElements();
        
        // 初始化覆盖层组列表
        InitializeOverlayGroups();
        
        // 初始化数据列表
        InitializeDataLists();
        
        // 创建自定义覆盖层
        CreateCustomOverlay();
        
        // 注册覆盖层事件
        RegisterOverlayEvents();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置覆盖层
        ConfigureOverlays();
        
        isInitialized = true;
        overlaysState = "已初始化";
        Debug.Log("覆盖层系统初始化完成");
    }

    /// <summary>
    /// 初始化覆盖层元素列表
    /// </summary>
    private void InitializeOverlayElements()
    {
        overlayElements = new System.Collections.Generic.List<IOverlayElement>();
        
        // 创建默认覆盖层元素
        for (int i = 0; i < maxElements; i++)
        {
            var element = CreateOverlayElement($"Element_{i}", $"元素_{i}", null, i % 2 == 0, (OverlayElementType)(i % 4));
            overlayElements.Add(element);
        }
        
        Debug.Log($"覆盖层元素初始化完成: {overlayElements.Count} 个元素");
    }

    /// <summary>
    /// 初始化覆盖层组列表
    /// </summary>
    private void InitializeOverlayGroups()
    {
        overlayGroups = new System.Collections.Generic.List<IOverlayGroup>();
        
        // 创建默认覆盖层组
        for (int i = 0; i < 3; i++)
        {
            var group = CreateOverlayGroup($"Group_{i}", $"组_{i}", i % 2 == 0);
            overlayGroups.Add(group);
        }
        
        Debug.Log($"覆盖层组初始化完成: {overlayGroups.Count} 个组");
    }

    /// <summary>
    /// 初始化数据列表
    /// </summary>
    private void InitializeDataLists()
    {
        elementDataList = new System.Collections.Generic.List<OverlayElementData>();
        groupDataList = new System.Collections.Generic.List<OverlayGroupData>();
        eventDataList = new System.Collections.Generic.List<OverlayEventData>();
        styleDataList = new System.Collections.Generic.List<OverlayStyleData>();
        overlayLogList = new System.Collections.Generic.List<string>();
        
        Debug.Log("数据列表初始化完成");
    }

    /// <summary>
    /// 创建自定义覆盖层
    /// </summary>
    private void CreateCustomOverlay()
    {
        if (!enableCustomOverlay) return;
        
        // 创建覆盖层
        customOverlay = new Overlay(overlayId, overlayName);
        customOverlay.container = overlayContainer;
        customOverlay.alignment = overlayAlignment;
        customOverlay.priority = overlayPriority;
        customOverlay.visible = overlayVisible;
        customOverlay.enabled = overlayEnabled;
        customOverlay.size = overlaySize;
        customOverlay.position = overlayPosition;
        
        // 添加覆盖层元素
        foreach (var element in overlayElements)
        {
            customOverlay.AddElement(element);
        }
        
        // 添加覆盖层组
        foreach (var group in overlayGroups)
        {
            customOverlay.AddGroup(group);
        }
        
        Debug.Log($"自定义覆盖层创建完成: {overlayName}");
    }

    /// <summary>
    /// 注册覆盖层事件
    /// </summary>
    private void RegisterOverlayEvents()
    {
        if (customOverlay != null)
        {
            customOverlay.onElementAdded += OnOverlayElementAdded;
            customOverlay.onElementRemoved += OnOverlayElementRemoved;
            customOverlay.onGroupAdded += OnOverlayGroupAdded;
            customOverlay.onGroupRemoved += OnOverlayGroupRemoved;
            customOverlay.onVisibilityChanged += OnOverlayVisibilityChanged;
            customOverlay.onEnabledChanged += OnOverlayEnabledChanged;
            customOverlay.onPositionChanged += OnOverlayPositionChanged;
            customOverlay.onSizeChanged += OnOverlaySizeChanged;
        }
        
        Debug.Log("覆盖层事件注册完成");
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
    /// 配置覆盖层
    /// </summary>
    private void ConfigureOverlays()
    {
        // 配置覆盖层样式
        ConfigureOverlayStyle();
        
        // 配置覆盖层动画
        ConfigureOverlayAnimation();
        
        // 配置覆盖层交互
        ConfigureOverlayInteraction();
        
        Debug.Log("覆盖层配置完成");
    }

    /// <summary>
    /// 配置覆盖层样式
    /// </summary>
    private void ConfigureOverlayStyle()
    {
        if (customOverlay != null)
        {
            customOverlay.backgroundColor = overlayBackgroundColor;
            customOverlay.textColor = overlayTextColor;
            customOverlay.borderColor = overlayBorderColor;
            customOverlay.opacity = overlayOpacity;
            customOverlay.borderWidth = overlayBorderWidth;
            customOverlay.cornerRadius = overlayCornerRadius;
            customOverlay.enableShadow = enableOverlayShadow;
            customOverlay.shadowOffset = overlayShadowOffset;
            customOverlay.shadowBlur = overlayShadowBlur;
        }
    }

    /// <summary>
    /// 配置覆盖层动画
    /// </summary>
    private void ConfigureOverlayAnimation()
    {
        if (customOverlay != null)
        {
            customOverlay.enableFade = enableOverlayFade;
            customOverlay.enableSlide = enableOverlaySlide;
            customOverlay.enableScale = enableOverlayScale;
            customOverlay.animationDuration = overlayAnimationDuration;
            customOverlay.animationType = overlayAnimationType;
            customOverlay.animationEase = overlayAnimationEase;
            customOverlay.enableHover = enableOverlayHover;
            customOverlay.hoverScale = overlayHoverScale;
            customOverlay.hoverColor = overlayHoverColor;
        }
    }

    /// <summary>
    /// 配置覆盖层交互
    /// </summary>
    private void ConfigureOverlayInteraction()
    {
        if (customOverlay != null)
        {
            customOverlay.enableClick = enableOverlayClick;
            customOverlay.enableDrag = enableOverlayDrag;
            customOverlay.enableResize = enableOverlayResize;
            customOverlay.enableContextMenu = enableOverlayContextMenu;
            customOverlay.enableTooltip = enableOverlayTooltip;
            customOverlay.tooltipDelay = overlayTooltipDelay;
            customOverlay.tooltipText = overlayTooltipText;
            customOverlay.enableKeyboard = enableOverlayKeyboard;
        }
    }

    private void Update()
    {
        if (!isInitialized || !enableOverlays) return;
        
        // 更新性能监控
        if (enableOverlayMonitoring && Time.time - lastMonitoringTime >= monitoringInterval)
        {
            MonitorOverlayPerformance();
            lastMonitoringTime = Time.time;
        }
        
        // 更新覆盖层状态
        UpdateOverlayStatus();
        
        // 更新覆盖层动画
        UpdateOverlayAnimation();
        
        // 处理覆盖层交互
        HandleOverlayInteraction();
        
        // 处理覆盖层输入
        HandleOverlayInput();
    }

    /// <summary>
    /// 监控覆盖层性能
    /// </summary>
    private void MonitorOverlayPerformance()
    {
        totalElements = overlayElements != null ? overlayElements.Count : 0;
        totalGroups = overlayGroups != null ? overlayGroups.Count : 0;
        memoryUsage = (totalElements + totalGroups) * 0.1f; // 估算内存使用量 (MB)
        renderTime = Time.deltaTime * 1000f; // 转换为毫秒
        
        if (logOverlayData)
        {
            Debug.Log($"覆盖层性能数据 - 元素数: {totalElements}, 组数: {totalGroups}, 内存使用: {memoryUsage:F2}MB, 渲染时间: {renderTime:F2}ms");
        }
    }

    /// <summary>
    /// 更新覆盖层状态
    /// </summary>
    private void UpdateOverlayStatus()
    {
        if (customOverlay != null)
        {
            overlayVisible = customOverlay.visible;
            overlayEnabled = customOverlay.enabled;
            overlaySize = customOverlay.size;
            overlayPosition = customOverlay.position;
            overlayBounds = customOverlay.bounds;
            overlayCenter = customOverlay.center;
        }
    }

    /// <summary>
    /// 更新覆盖层动画
    /// </summary>
    private void UpdateOverlayAnimation()
    {
        if (!enableOverlayAnimation) return;
        
        animationTime += Time.deltaTime;
        
        // 更新悬停动画
        if (enableOverlayHover && isHovering)
        {
            float hoverProgress = Mathf.Sin(animationTime * 2f) * 0.5f + 0.5f;
            if (customOverlay != null)
            {
                customOverlay.scale = Mathf.Lerp(1f, overlayHoverScale, hoverProgress);
            }
        }
    }

    /// <summary>
    /// 处理覆盖层交互
    /// </summary>
    private void HandleOverlayInteraction()
    {
        if (!enableOverlayInteraction) return;
        
        Vector2 mousePosition = Input.mousePosition;
        
        // 检查鼠标悬停
        if (customOverlay != null && customOverlay.bounds.Contains(mousePosition))
        {
            if (!isHovering)
            {
                isHovering = true;
                OnOverlayHoverEnter();
            }
        }
        else
        {
            if (isHovering)
            {
                isHovering = false;
                OnOverlayHoverExit();
            }
        }
        
        lastMousePosition = mousePosition;
    }

    /// <summary>
    /// 处理覆盖层输入
    /// </summary>
    private void HandleOverlayInput()
    {
        // 处理键盘快捷键
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleOverlayVisibility();
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleOverlayEnabled();
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            AddRandomElement();
        }
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            RemoveRandomElement();
        }
        
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ToggleOverlayAnimation();
        }
    }

    /// <summary>
    /// 创建覆盖层元素
    /// </summary>
    private IOverlayElement CreateOverlayElement(string id, string name, Texture2D icon, bool initialState, OverlayElementType elementType)
    {
        var element = new CustomOverlayElement
        {
            elementId = id,
            elementName = name,
            elementIcon = icon,
            elementState = initialState,
            elementType = elementType
        };
        
        element.onElementClicked += OnElementClicked;
        element.onElementStateChanged += OnElementStateChanged;
        
        return element;
    }

    /// <summary>
    /// 创建覆盖层组
    /// </summary>
    private IOverlayGroup CreateOverlayGroup(string id, string name, bool initialState)
    {
        var group = new CustomOverlayGroup
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
    /// 切换覆盖层可见性
    /// </summary>
    public void ToggleOverlayVisibility()
    {
        if (customOverlay != null)
        {
            customOverlay.visible = !customOverlay.visible;
            LogOverlayEvent("切换覆盖层可见性", customOverlay.visible ? "显示" : "隐藏");
        }
    }

    /// <summary>
    /// 切换覆盖层启用状态
    /// </summary>
    public void ToggleOverlayEnabled()
    {
        if (customOverlay != null)
        {
            customOverlay.enabled = !customOverlay.enabled;
            LogOverlayEvent("切换覆盖层启用状态", customOverlay.enabled ? "启用" : "禁用");
        }
    }

    /// <summary>
    /// 切换覆盖层动画
    /// </summary>
    public void ToggleOverlayAnimation()
    {
        enableOverlayAnimation = !enableOverlayAnimation;
        LogOverlayEvent("切换覆盖层动画", enableOverlayAnimation ? "启用" : "禁用");
    }

    /// <summary>
    /// 添加随机元素
    /// </summary>
    public void AddRandomElement()
    {
        if (overlayElements.Count >= maxElements) return;
        
        int randomIndex = Random.Range(0, 1000);
        var element = CreateOverlayElement($"RandomElement_{randomIndex}", $"随机元素_{randomIndex}", null, Random.value > 0.5f, (OverlayElementType)(randomIndex % 4));
        overlayElements.Add(element);
        
        if (customOverlay != null)
        {
            customOverlay.AddElement(element);
        }
        
        LogOverlayEvent("添加随机元素", $"元素_{randomIndex}");
    }

    /// <summary>
    /// 移除随机元素
    /// </summary>
    public void RemoveRandomElement()
    {
        if (overlayElements.Count <= 0) return;
        
        int randomIndex = Random.Range(0, overlayElements.Count);
        var element = overlayElements[randomIndex];
        overlayElements.RemoveAt(randomIndex);
        
        if (customOverlay != null)
        {
            customOverlay.RemoveElement(element);
        }
        
        LogOverlayEvent("移除随机元素", $"索引_{randomIndex}");
    }

    /// <summary>
    /// 设置活动元素
    /// </summary>
    public void SetActiveElement(int index)
    {
        if (overlayElements != null && index >= 0 && index < overlayElements.Count)
        {
            activeElementIndex = index;
            LogOverlayEvent("设置活动元素", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置覆盖层样式
    /// </summary>
    public void SetOverlayStyle(OverlayStyleData styleData)
    {
        if (customOverlay != null)
        {
            customOverlay.backgroundColor = styleData.backgroundColor;
            customOverlay.textColor = styleData.textColor;
            customOverlay.borderColor = styleData.borderColor;
            customOverlay.opacity = styleData.opacity;
            customOverlay.borderWidth = styleData.borderWidth;
            customOverlay.cornerRadius = styleData.cornerRadius;
            
            LogOverlayEvent("设置覆盖层样式", styleData.styleName);
        }
    }

    /// <summary>
    /// 生成覆盖层报告
    /// </summary>
    public void GenerateOverlayReport()
    {
        OverlayReportData reportData = new OverlayReportData
        {
            timestamp = System.DateTime.Now.ToString(),
            overlaysState = overlaysState,
            currentOverlayMode = currentOverlayMode,
            overlayId = overlayId,
            overlayName = overlayName,
            overlayVisible = overlayVisible,
            overlayEnabled = overlayEnabled,
            totalElements = totalElements,
            totalGroups = totalGroups,
            activeElementIndex = activeElementIndex,
            memoryUsage = memoryUsage,
            renderTime = renderTime,
            overlaySize = overlaySize,
            overlayPosition = overlayPosition,
            enableOverlayAnimation = enableOverlayAnimation,
            enableOverlayInteraction = enableOverlayInteraction
        };
        
        string reportJson = JsonUtility.ToJson(reportData, true);
        Debug.Log($"覆盖层报告生成完成:\n{reportJson}");
    }

    /// <summary>
    /// 导出覆盖层数据
    /// </summary>
    public void ExportOverlayData()
    {
        // 导出元素数据
        elementData = elementDataList.ToArray();
        
        // 导出组数据
        groupData = groupDataList.ToArray();
        
        // 导出事件数据
        eventData = eventDataList.ToArray();
        
        // 导出样式数据
        styleData = styleDataList.ToArray();
        
        // 导出日志数据
        overlayLogs = overlayLogList.ToArray();
        
        Debug.Log("覆盖层数据导出完成");
    }

    /// <summary>
    /// 记录覆盖层事件
    /// </summary>
    private void LogOverlayEvent(string eventType, string eventData)
    {
        var eventInfo = new OverlayEventData
        {
            eventId = eventCounter++,
            timestamp = System.DateTime.Now.ToString(),
            eventType = eventType,
            eventData = eventData
        };
        
        eventDataList.Add(eventInfo);
        overlayLogList.Add($"[{eventInfo.timestamp}] {eventInfo.eventType}: {eventInfo.eventData}");
        
        // 限制日志数量
        if (overlayLogList.Count > 100)
        {
            overlayLogList.RemoveAt(0);
        }
    }

    // 事件处理函数
    private void OnOverlayElementAdded(IOverlayElement element)
    {
        LogOverlayEvent("元素添加", element.GetType().Name);
    }

    private void OnOverlayElementRemoved(IOverlayElement element)
    {
        LogOverlayEvent("元素移除", element.GetType().Name);
    }

    private void OnOverlayGroupAdded(IOverlayGroup group)
    {
        LogOverlayEvent("组添加", group.GetType().Name);
    }

    private void OnOverlayGroupRemoved(IOverlayGroup group)
    {
        LogOverlayEvent("组移除", group.GetType().Name);
    }

    private void OnOverlayVisibilityChanged(bool visible)
    {
        LogOverlayEvent("可见性改变", visible ? "显示" : "隐藏");
    }

    private void OnOverlayEnabledChanged(bool enabled)
    {
        LogOverlayEvent("启用状态改变", enabled ? "启用" : "禁用");
    }

    private void OnOverlayPositionChanged(Vector2 position)
    {
        LogOverlayEvent("位置改变", position.ToString());
    }

    private void OnOverlaySizeChanged(Vector2 size)
    {
        LogOverlayEvent("大小改变", size.ToString());
    }

    private void OnOverlayHoverEnter()
    {
        LogOverlayEvent("悬停进入", "鼠标悬停");
    }

    private void OnOverlayHoverExit()
    {
        LogOverlayEvent("悬停退出", "鼠标离开");
    }

    private void OnElementClicked(string elementId)
    {
        LogOverlayEvent("元素点击", elementId);
    }

    private void OnElementStateChanged(string elementId, bool state)
    {
        LogOverlayEvent("元素状态改变", $"{elementId}: {state}");
    }

    private void OnGroupClicked(string groupId)
    {
        LogOverlayEvent("组点击", groupId);
    }

    private void OnGroupStateChanged(string groupId, bool state)
    {
        LogOverlayEvent("组状态改变", $"{groupId}: {state}");
    }

    private void OnGUI()
    {
        if (!isInitialized) return;
        
        GUILayout.BeginArea(new Rect(Screen.width - 310, 10, 300, Screen.height - 20));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label("覆盖层系统", EditorStyles.boldLabel);
        
        // 覆盖层配置
        GUILayout.Space(10);
        GUILayout.Label("覆盖层配置", EditorStyles.boldLabel);
        enableOverlays = GUILayout.Toggle(enableOverlays, "启用覆盖层");
        enableCustomOverlay = GUILayout.Toggle(enableCustomOverlay, "启用自定义覆盖层");
        enableOverlayLayout = GUILayout.Toggle(enableOverlayLayout, "启用覆盖层布局");
        enableOverlayInteraction = GUILayout.Toggle(enableOverlayInteraction, "启用覆盖层交互");
        enableOverlayAnimation = GUILayout.Toggle(enableOverlayAnimation, "启用覆盖层动画");
        enableOverlayStyling = GUILayout.Toggle(enableOverlayStyling, "启用覆盖层样式");
        
        // 覆盖层状态
        GUILayout.Space(10);
        GUILayout.Label("覆盖层状态", EditorStyles.boldLabel);
        GUILayout.Label($"状态: {overlaysState}");
        GUILayout.Label($"模式: {currentOverlayMode}");
        GUILayout.Label($"可见性: {overlayVisible}");
        GUILayout.Label($"启用状态: {overlayEnabled}");
        GUILayout.Label($"元素数: {totalElements}");
        GUILayout.Label($"组数: {totalGroups}");
        GUILayout.Label($"内存使用: {memoryUsage:F2}MB");
        GUILayout.Label($"渲染时间: {renderTime:F2}ms");
        
        // 覆盖层操作
        GUILayout.Space(10);
        GUILayout.Label("覆盖层操作", EditorStyles.boldLabel);
        if (GUILayout.Button("切换可见性")) ToggleOverlayVisibility();
        if (GUILayout.Button("切换启用状态")) ToggleOverlayEnabled();
        if (GUILayout.Button("切换动画")) ToggleOverlayAnimation();
        if (GUILayout.Button("添加随机元素")) AddRandomElement();
        if (GUILayout.Button("移除随机元素")) RemoveRandomElement();
        
        // 覆盖层数据
        GUILayout.Space(10);
        GUILayout.Label("覆盖层数据", EditorStyles.boldLabel);
        if (GUILayout.Button("生成报告")) GenerateOverlayReport();
        if (GUILayout.Button("导出数据")) ExportOverlayData();
        if (GUILayout.Button("清空日志")) overlayLogList.Clear();
        
        // 快捷键提示
        GUILayout.Space(10);
        GUILayout.Label("快捷键", EditorStyles.boldLabel);
        GUILayout.Label("F1: 切换可见性");
        GUILayout.Label("F2: 切换启用状态");
        GUILayout.Label("F3: 添加随机元素");
        GUILayout.Label("F4: 移除随机元素");
        GUILayout.Label("F5: 切换动画");
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        if (customOverlay != null)
        {
            customOverlay.onElementAdded -= OnOverlayElementAdded;
            customOverlay.onElementRemoved -= OnOverlayElementRemoved;
            customOverlay.onGroupAdded -= OnOverlayGroupAdded;
            customOverlay.onGroupRemoved -= OnOverlayGroupRemoved;
            customOverlay.onVisibilityChanged -= OnOverlayVisibilityChanged;
            customOverlay.onEnabledChanged -= OnOverlayEnabledChanged;
            customOverlay.onPositionChanged -= OnOverlayPositionChanged;
            customOverlay.onSizeChanged -= OnOverlaySizeChanged;
        }
    }
}

/// <summary>
/// 覆盖层容器类型枚举
/// </summary>
public enum OverlayContainerType
{
    Toolbar,
    SceneView,
    GameView,
    Inspector,
    Project,
    Hierarchy
}

/// <summary>
/// 覆盖层对齐方式枚举
/// </summary>
public enum OverlayAlignment
{
    Top,
    Bottom,
    Left,
    Right,
    Center
}

/// <summary>
/// 覆盖层元素类型枚举
/// </summary>
public enum OverlayElementType
{
    Button,
    Toggle,
    Slider,
    Dropdown
}

/// <summary>
/// 覆盖层动画类型枚举
/// </summary>
public enum OverlayAnimationType
{
    None,
    Fade,
    Slide,
    Scale,
    Rotate
}

/// <summary>
/// 覆盖层动画缓动枚举
/// </summary>
public enum OverlayAnimationEase
{
    Linear,
    EaseIn,
    EaseOut,
    EaseInOut
}

/// <summary>
/// 自定义覆盖层元素
/// </summary>
public class CustomOverlayElement : IOverlayElement
{
    public string elementId;
    public string elementName;
    public Texture2D elementIcon;
    public bool elementState;
    public OverlayElementType elementType;
    
    public System.Action<string> onElementClicked;
    public System.Action<string, bool> onElementStateChanged;
    
    public string id => elementId;
    public string name => elementName;
    public Texture2D icon => elementIcon;
    public bool state => elementState;
    public OverlayElementType type => elementType;
    
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
/// 自定义覆盖层组
/// </summary>
public class CustomOverlayGroup : IOverlayGroup
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
/// 覆盖层类
/// </summary>
public class Overlay
{
    public string id;
    public string name;
    public OverlayContainerType container;
    public OverlayAlignment alignment;
    public int priority;
    public bool visible;
    public bool enabled;
    public Vector2 size;
    public Vector2 position;
    public Vector2 bounds;
    public Vector2 center;
    public float scale;
    
    // 样式属性
    public Color backgroundColor;
    public Color textColor;
    public Color borderColor;
    public float opacity;
    public float borderWidth;
    public float cornerRadius;
    public bool enableShadow;
    public Vector2 shadowOffset;
    public float shadowBlur;
    
    // 动画属性
    public bool enableFade;
    public bool enableSlide;
    public bool enableScale;
    public float animationDuration;
    public OverlayAnimationType animationType;
    public OverlayAnimationEase animationEase;
    public bool enableHover;
    public float hoverScale;
    public Color hoverColor;
    
    // 交互属性
    public bool enableClick;
    public bool enableDrag;
    public bool enableResize;
    public bool enableContextMenu;
    public bool enableTooltip;
    public float tooltipDelay;
    public string tooltipText;
    public bool enableKeyboard;
    
    // 事件
    public System.Action<IOverlayElement> onElementAdded;
    public System.Action<IOverlayElement> onElementRemoved;
    public System.Action<IOverlayGroup> onGroupAdded;
    public System.Action<IOverlayGroup> onGroupRemoved;
    public System.Action<bool> onVisibilityChanged;
    public System.Action<bool> onEnabledChanged;
    public System.Action<Vector2> onPositionChanged;
    public System.Action<Vector2> onSizeChanged;
    
    public Overlay(string id, string name)
    {
        this.id = id;
        this.name = name;
    }
    
    public void AddElement(IOverlayElement element)
    {
        onElementAdded?.Invoke(element);
    }
    
    public void RemoveElement(IOverlayElement element)
    {
        onElementRemoved?.Invoke(element);
    }
    
    public void AddGroup(IOverlayGroup group)
    {
        onGroupAdded?.Invoke(group);
    }
    
    public void RemoveGroup(IOverlayGroup group)
    {
        onGroupRemoved?.Invoke(group);
    }
}

/// <summary>
/// 覆盖层元素接口
/// </summary>
public interface IOverlayElement
{
    string id { get; }
    string name { get; }
    Texture2D icon { get; }
    bool state { get; }
    OverlayElementType type { get; }
    void OnClick();
    void OnStateChanged(bool newState);
}

/// <summary>
/// 覆盖层组接口
/// </summary>
public interface IOverlayGroup
{
    string id { get; }
    string name { get; }
    bool state { get; }
    void OnClick();
    void OnStateChanged(bool newState);
}

/// <summary>
/// 覆盖层元素数据
/// </summary>
[System.Serializable]
public class OverlayElementData
{
    public string elementId;
    public string elementName;
    public bool elementState;
    public OverlayElementType elementType;
    public Vector2 elementPosition;
    public Vector2 elementSize;
}

/// <summary>
/// 覆盖层组数据
/// </summary>
[System.Serializable]
public class OverlayGroupData
{
    public string groupId;
    public string groupName;
    public bool groupState;
    public Vector2 groupPosition;
    public Vector2 groupSize;
}

/// <summary>
/// 覆盖层事件数据
/// </summary>
[System.Serializable]
public class OverlayEventData
{
    public int eventId;
    public string timestamp;
    public string eventType;
    public string eventData;
}

/// <summary>
/// 覆盖层样式数据
/// </summary>
[System.Serializable]
public class OverlayStyleData
{
    public string styleId;
    public string styleName;
    public Color backgroundColor;
    public Color textColor;
    public Color borderColor;
    public float opacity;
    public float borderWidth;
    public float cornerRadius;
}

/// <summary>
/// 覆盖层报告数据
/// </summary>
[System.Serializable]
public class OverlayReportData
{
    public string timestamp;
    public string overlaysState;
    public string currentOverlayMode;
    public string overlayId;
    public string overlayName;
    public bool overlayVisible;
    public bool overlayEnabled;
    public int totalElements;
    public int totalGroups;
    public int activeElementIndex;
    public float memoryUsage;
    public float renderTime;
    public Vector2 overlaySize;
    public Vector2 overlayPosition;
    public bool enableOverlayAnimation;
    public bool enableOverlayInteraction;
} 