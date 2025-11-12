using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Events 命名空间案例演示
/// 展示Unity事件系统的核心功能
/// </summary>
public class EventsSystemExample : MonoBehaviour
{
    [Header("UnityEvent 示例")]
    [SerializeField] private UnityEvent onStartEvent; // 启动事件
    [SerializeField] private UnityEvent onUpdateEvent; // 更新事件
    [SerializeField] private UnityEvent onDestroyEvent; // 销毁事件
    
    [Header("带参数的UnityEvent")]
    [SerializeField] private UnityEvent<int> onValueChangedEvent; // 值改变事件
    [SerializeField] private UnityEvent<string> onMessageEvent; // 消息事件
    [SerializeField] private UnityEvent<Vector3> onPositionChangedEvent; // 位置改变事件
    [SerializeField] private UnityEvent<GameObject> onObjectSelectedEvent; // 对象选择事件
    
    [Header("自定义事件")]
    [SerializeField] private UnityEvent<CustomEventData> onCustomEvent; // 自定义事件
    
    [Header("事件设置")]
    [SerializeField] private bool enableLogging = true; // 日志开关
    [SerializeField] private int testValue = 0; // 测试值
    [SerializeField] private string testMessage = "测试消息"; // 测试消息
    [SerializeField] private Vector3 testPosition = Vector3.zero; // 测试位置
    
    // 事件监听器列表
    private List<UnityAction> eventListeners = new List<UnityAction>(); // 基础事件监听器列表
    private List<UnityAction<int>> intEventListeners = new List<UnityAction<int>>(); // 带参数的事件监听器列表
    private List<UnityAction<string>> stringEventListeners = new List<UnityAction<string>>(); // 带参数的事件监听器列表
    
    // 事件计数
    private int eventCount = 0;
    
    private void Start()
    {
        InitializeEvents();
        LogMessage("EventsSystemExample 启动完成");
    }
    
    /// <summary>
    /// 初始化事件系统
    /// </summary>
    private void InitializeEvents()
    {
        // 初始化UnityEvent
        if (onStartEvent == null)
            onStartEvent = new UnityEvent();
        if (onUpdateEvent == null)
            onUpdateEvent = new UnityEvent();
        if (onDestroyEvent == null)
            onDestroyEvent = new UnityEvent();
        
        // 初始化带参数的UnityEvent
        if (onValueChangedEvent == null)
            onValueChangedEvent = new UnityEvent<int>();
        if (onMessageEvent == null)
            onMessageEvent = new UnityEvent<string>();
        if (onPositionChangedEvent == null)
            onPositionChangedEvent = new UnityEvent<Vector3>();
        if (onObjectSelectedEvent == null)
            onObjectSelectedEvent = new UnityEvent<GameObject>();
        if (onCustomEvent == null)
            onCustomEvent = new UnityEvent<CustomEventData>();
        
        // 添加事件监听器
        AddEventListeners();
        
        // 触发启动事件
        onStartEvent?.Invoke();
    }
    
    /// <summary>
    /// 添加事件监听器
    /// </summary>
    private void AddEventListeners()
    {
        // 添加基础事件监听器
        onStartEvent.AddListener(OnStartHandler);
        onUpdateEvent.AddListener(OnUpdateHandler);
        onDestroyEvent.AddListener(OnDestroyHandler);
        
        // 添加带参数的事件监听器
        onValueChangedEvent.AddListener(OnValueChangedHandler);
        onMessageEvent.AddListener(OnMessageHandler);
        onPositionChangedEvent.AddListener(OnPositionChangedHandler);
        onObjectSelectedEvent.AddListener(OnObjectSelectedHandler);
        onCustomEvent.AddListener(OnCustomEventHandler);
        
        // 添加多个监听器示例
        onValueChangedEvent.AddListener(OnValueChangedHandler2);
        onMessageEvent.AddListener(OnMessageHandler2);
        
        // 保存监听器引用以便后续移除
        eventListeners.Add(OnStartHandler);
        eventListeners.Add(OnUpdateHandler);
        eventListeners.Add(OnDestroyHandler);
        
        intEventListeners.Add(OnValueChangedHandler);
        intEventListeners.Add(OnValueChangedHandler2);
        
        stringEventListeners.Add(OnMessageHandler);
        stringEventListeners.Add(OnMessageHandler2);
    }
    
    /// <summary>
    /// 移除事件监听器
    /// </summary>
    private void RemoveEventListeners()
    {
        // 移除基础事件监听器
        foreach (var listener in eventListeners)
        {
            onStartEvent?.RemoveListener(listener);
            onUpdateEvent?.RemoveListener(listener);
            onDestroyEvent?.RemoveListener(listener);
        }
        
        // 移除带参数的事件监听器
        foreach (var listener in intEventListeners)
        {
            onValueChangedEvent?.RemoveListener(listener);
        }
        
        foreach (var listener in stringEventListeners)
        {
            onMessageEvent?.RemoveListener(listener);
        }
        
        // 移除其他监听器
        onPositionChangedEvent?.RemoveListener(OnPositionChangedHandler);
        onObjectSelectedEvent?.RemoveListener(OnObjectSelectedHandler);
        onCustomEvent?.RemoveListener(OnCustomEventHandler);
    }
    
    /// <summary>
    /// 清除所有事件监听器
    /// </summary>
    public void ClearAllListeners()
    {
        onStartEvent?.RemoveAllListeners();
        onUpdateEvent?.RemoveAllListeners();
        onDestroyEvent?.RemoveAllListeners();
        onValueChangedEvent?.RemoveAllListeners();
        onMessageEvent?.RemoveAllListeners();
        onPositionChangedEvent?.RemoveAllListeners();
        onObjectSelectedEvent?.RemoveAllListeners();
        onCustomEvent?.RemoveAllListeners();
        
        LogMessage("所有事件监听器已清除");
    }
    
    /// <summary>
    /// 获取事件监听器数量
    /// </summary>
    public void GetListenerCount()
    {
        int startCount = onStartEvent?.GetPersistentEventCount() ?? 0;
        int updateCount = onUpdateEvent?.GetPersistentEventCount() ?? 0;
        int valueCount = onValueChangedEvent?.GetPersistentEventCount() ?? 0;
        int messageCount = onMessageEvent?.GetPersistentEventCount() ?? 0;
        
        LogMessage($"事件监听器数量 - Start: {startCount}, Update: {updateCount}, Value: {valueCount}, Message: {messageCount}");
    }
    
    /// <summary>
    /// 触发测试事件
    /// </summary>
    public void TriggerTestEvents()
    {
        eventCount++;
        
        // 触发基础事件
        onUpdateEvent?.Invoke();
        
        // 触发带参数的事件
        onValueChangedEvent?.Invoke(testValue);
        onMessageEvent?.Invoke(testMessage);
        onPositionChangedEvent?.Invoke(testPosition);
        onObjectSelectedEvent?.Invoke(gameObject);
        
        // 触发自定义事件
        CustomEventData customData = new CustomEventData
        {
            id = eventCount,
            message = $"自定义事件 #{eventCount}",
            timestamp = Time.time,
            position = transform.position
        };
        onCustomEvent?.Invoke(customData);
        
        LogMessage($"已触发所有测试事件 (第 {eventCount} 次)");
    }
    
    /// <summary>
    /// 设置测试值并触发事件
    /// </summary>
    /// <param name="newValue">新值</param>
    public void SetTestValue(int newValue)
    {
        testValue = newValue;
        onValueChangedEvent?.Invoke(testValue);
        LogMessage($"测试值已更新: {testValue}");
    }
    
    /// <summary>
    /// 设置测试消息并触发事件
    /// </summary>
    /// <param name="newMessage">新消息</param>
    public void SetTestMessage(string newMessage)
    {
        testMessage = newMessage;
        onMessageEvent?.Invoke(testMessage);
        LogMessage($"测试消息已更新: {testMessage}");
    }
    
    /// <summary>
    /// 设置测试位置并触发事件
    /// </summary>
    /// <param name="newPosition">新位置</param>
    public void SetTestPosition(Vector3 newPosition)
    {
        testPosition = newPosition;
        onPositionChangedEvent?.Invoke(testPosition);
        LogMessage($"测试位置已更新: {testPosition}");
    }
    
    /// <summary>
    /// 动态添加事件监听器
    /// </summary>
    public void AddDynamicListener()
    {
        UnityAction<int> dynamicListener = (value) => {
            LogMessage($"动态监听器收到值: {value}");
        };
        
        onValueChangedEvent?.AddListener(dynamicListener);
        intEventListeners.Add(dynamicListener);
        
        LogMessage("已添加动态事件监听器");
    }
    
    /// <summary>
    /// 检查事件是否为空
    /// </summary>
    public void CheckEventStatus()
    {
        bool startIsNull = onStartEvent == null;
        bool updateIsNull = onUpdateEvent == null;
        bool valueIsNull = onValueChangedEvent == null;
        bool messageIsNull = onMessageEvent == null;
        
        LogMessage($"事件状态检查 - Start: {(startIsNull ? "空" : "正常")}, Update: {(updateIsNull ? "空" : "正常")}, Value: {(valueIsNull ? "空" : "正常")}, Message: {(messageIsNull ? "空" : "正常")}");
    }
    
    // 事件处理器方法
    private void OnStartHandler()
    {
        LogMessage("OnStart 事件被触发");
    }
    
    private void OnUpdateHandler()
    {
        LogMessage("OnUpdate 事件被触发");
    }
    
    private void OnDestroyHandler()
    {
        LogMessage("OnDestroy 事件被触发");
    }
    
    private void OnValueChangedHandler(int value)
    {
        LogMessage($"值改变事件被触发: {value}");
    }
    
    private void OnValueChangedHandler2(int value)
    {
        LogMessage($"值改变事件处理器2: {value}");
    }
    
    private void OnMessageHandler(string message)
    {
        LogMessage($"消息事件被触发: {message}");
    }
    
    private void OnMessageHandler2(string message)
    {
        LogMessage($"消息事件处理器2: {message}");
    }
    
    private void OnPositionChangedHandler(Vector3 position)
    {
        LogMessage($"位置改变事件被触发: {position}");
    }
    
    private void OnObjectSelectedHandler(GameObject obj)
    {
        LogMessage($"对象选择事件被触发: {obj?.name ?? "null"}");
    }
    
    private void OnCustomEventHandler(CustomEventData data)
    {
        LogMessage($"自定义事件被触发 - ID: {data.id}, 消息: {data.message}, 时间: {data.timestamp:F2}, 位置: {data.position}");
    }
    
    private void Update()
    {
        // 每帧触发更新事件（可选）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onUpdateEvent?.Invoke();
        }
    }
    
    private void OnDestroy()
    {
        // 触发销毁事件
        onDestroyEvent?.Invoke();
        
        // 移除所有监听器
        RemoveEventListeners();
        
        LogMessage("EventsSystemExample 已销毁");
    }
    
    /// <summary>
    /// 日志输出
    /// </summary>
    /// <param name="message">消息内容</param>
    private void LogMessage(string message)
    {
        if (enableLogging)
        {
            Debug.Log($"[EventsSystem] {message}");
        }
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 500));
        GUILayout.Label("事件系统控制", EditorStyles.boldLabel);
        
        if (GUILayout.Button("触发测试事件"))
        {
            TriggerTestEvents();
        }
        
        GUILayout.Space(10);
        
        GUILayout.Label($"测试值: {testValue}");
        testValue = (int)GUILayout.HorizontalSlider(testValue, 0, 100);
        
        GUILayout.Label($"测试消息: {testMessage}");
        testMessage = GUILayout.TextField(testMessage);
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("设置测试值"))
        {
            SetTestValue(testValue);
        }
        
        if (GUILayout.Button("设置测试消息"))
        {
            SetTestMessage(testMessage);
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("添加动态监听器"))
        {
            AddDynamicListener();
        }
        
        if (GUILayout.Button("获取监听器数量"))
        {
            GetListenerCount();
        }
        
        if (GUILayout.Button("检查事件状态"))
        {
            CheckEventStatus();
        }
        
        if (GUILayout.Button("清除所有监听器"))
        {
            ClearAllListeners();
        }
        
        GUILayout.Space(10);
        
        GUILayout.Label($"事件计数: {eventCount}");
        GUILayout.Label($"日志开关: {(enableLogging ? "开启" : "关闭")}");
        enableLogging = GUILayout.Toggle(enableLogging, "启用日志");
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 自定义事件数据结构
/// </summary>
[System.Serializable]
public class CustomEventData
{
    public int id;
    public string message;
    public float timestamp;
    public Vector3 position;
} 