using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Events 命名空间案例演示
/// 展示事件系统的核心功能
/// </summary>
public class EventsExample : MonoBehaviour
{
    [Header("事件系统设置")]
    [SerializeField] private bool enableEventSystem = true;
    [SerializeField] private bool enableDebugLog = true;
    [SerializeField] private int maxEventListeners = 100;
    
    [Header("UnityEvent示例")]
    [SerializeField] private UnityEvent onStartEvent;
    [SerializeField] private UnityEvent onUpdateEvent;
    [SerializeField] private UnityEvent onDestroyEvent;
    [SerializeField] private UnityEvent<string> onMessageEvent;
    [SerializeField] private UnityEvent<int> onNumberEvent;
    [SerializeField] private UnityEvent<float> onFloatEvent;
    [SerializeField] private UnityEvent<Vector3> onVector3Event;
    [SerializeField] private UnityEvent<GameObject> onGameObjectEvent;
    
    [Header("自定义事件")]
    [SerializeField] private CustomEvent onCustomEvent;
    [SerializeField] private PlayerEvent onPlayerEvent;
    [SerializeField] private ScoreEvent onScoreEvent;
    [SerializeField] private DamageEvent onDamageEvent;
    
    [Header("事件状态")]
    [SerializeField] private bool isEventSystemActive = false;
    [SerializeField] private int totalEventCount = 0;
    [SerializeField] private int activeEventCount = 0;
    [SerializeField] private List<string> eventHistory = new List<string>();
    
    [Header("事件参数")]
    [SerializeField] private string messageText = "Hello World";
    [SerializeField] private int numberValue = 42;
    [SerializeField] private float floatValue = 3.14f;
    [SerializeField] private Vector3 vectorValue = Vector3.one;
    
    // 事件系统
    private EventSystem eventSystem;
    private List<UnityEventBase> registeredEvents = new List<UnityEventBase>();
    
    // 事件回调
    private System.Action<string> onEventTriggered;
    private System.Action<UnityEventBase> onEventRegistered;
    private System.Action<UnityEventBase> onEventUnregistered;
    
    // 自定义事件类
    [System.Serializable]
    public class CustomEvent : UnityEvent<string, int>
    {
    }
    
    [System.Serializable]
    public class PlayerEvent : UnityEvent<GameObject, string>
    {
    }
    
    [System.Serializable]
    public class ScoreEvent : UnityEvent<int, int> // currentScore, previousScore
    {
    }
    
    [System.Serializable]
    public class DamageEvent : UnityEvent<GameObject, float, Vector3> // target, damage, hitPoint
    {
    }
    
    private void Start()
    {
        InitializeEventSystem();
    }
    
    /// <summary>
    /// 初始化事件系统
    /// </summary>
    private void InitializeEventSystem()
    {
        // 获取或创建事件系统
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystem = eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<StandaloneInputModule>();
        }
        
        // 初始化UnityEvent
        InitializeUnityEvents();
        
        // 注册事件监听器
        RegisterEventListeners();
        
        // 触发开始事件
        TriggerStartEvent();
        
        isEventSystemActive = true;
        Debug.Log("事件系统初始化完成");
    }
    
    /// <summary>
    /// 初始化UnityEvent
    /// </summary>
    private void InitializeUnityEvents()
    {
        // 初始化基本事件
        if (onStartEvent == null) onStartEvent = new UnityEvent();
        if (onUpdateEvent == null) onUpdateEvent = new UnityEvent();
        if (onDestroyEvent == null) onDestroyEvent = new UnityEvent();
        
        // 初始化带参数的事件
        if (onMessageEvent == null) onMessageEvent = new UnityEvent<string>();
        if (onNumberEvent == null) onNumberEvent = new UnityEvent<int>();
        if (onFloatEvent == null) onFloatEvent = new UnityEvent<float>();
        if (onVector3Event == null) onVector3Event = new UnityEvent<Vector3>();
        if (onGameObjectEvent == null) onGameObjectEvent = new UnityEvent<GameObject>();
        
        // 初始化自定义事件
        if (onCustomEvent == null) onCustomEvent = new CustomEvent();
        if (onPlayerEvent == null) onPlayerEvent = new PlayerEvent();
        if (onScoreEvent == null) onScoreEvent = new ScoreEvent();
        if (onDamageEvent == null) onDamageEvent = new DamageEvent();
        
        // 添加到注册列表
        registeredEvents.Add(onStartEvent);
        registeredEvents.Add(onUpdateEvent);
        registeredEvents.Add(onDestroyEvent);
        registeredEvents.Add(onMessageEvent);
        registeredEvents.Add(onNumberEvent);
        registeredEvents.Add(onFloatEvent);
        registeredEvents.Add(onVector3Event);
        registeredEvents.Add(onGameObjectEvent);
        registeredEvents.Add(onCustomEvent);
        registeredEvents.Add(onPlayerEvent);
        registeredEvents.Add(onScoreEvent);
        registeredEvents.Add(onDamageEvent);
        
        totalEventCount = registeredEvents.Count;
        activeEventCount = 0;
    }
    
    /// <summary>
    /// 注册事件监听器
    /// </summary>
    private void RegisterEventListeners()
    {
        // 注册基本事件监听器
        onStartEvent.AddListener(OnStartEventHandler);
        onUpdateEvent.AddListener(OnUpdateEventHandler);
        onDestroyEvent.AddListener(OnDestroyEventHandler);
        
        // 注册带参数的事件监听器
        onMessageEvent.AddListener(OnMessageEventHandler);
        onNumberEvent.AddListener(OnNumberEventHandler);
        onFloatEvent.AddListener(OnFloatEventHandler);
        onVector3Event.AddListener(OnVector3EventHandler);
        onGameObjectEvent.AddListener(OnGameObjectEventHandler);
        
        // 注册自定义事件监听器
        onCustomEvent.AddListener(OnCustomEventHandler);
        onPlayerEvent.AddListener(OnPlayerEventHandler);
        onScoreEvent.AddListener(OnScoreEventHandler);
        onDamageEvent.AddListener(OnDamageEventHandler);
        
        activeEventCount = registeredEvents.Count;
        
        Debug.Log($"注册了 {activeEventCount} 个事件监听器");
    }
    
    /// <summary>
    /// 触发开始事件
    /// </summary>
    private void TriggerStartEvent()
    {
        if (onStartEvent != null)
        {
            onStartEvent.Invoke();
            LogEvent("StartEvent");
        }
    }
    
    /// <summary>
    /// 触发消息事件
    /// </summary>
    /// <param name="message">消息内容</param>
    public void TriggerMessageEvent(string message)
    {
        if (onMessageEvent != null)
        {
            onMessageEvent.Invoke(message);
            LogEvent($"MessageEvent: {message}");
        }
    }
    
    /// <summary>
    /// 触发数字事件
    /// </summary>
    /// <param name="number">数字值</param>
    public void TriggerNumberEvent(int number)
    {
        if (onNumberEvent != null)
        {
            onNumberEvent.Invoke(number);
            LogEvent($"NumberEvent: {number}");
        }
    }
    
    /// <summary>
    /// 触发浮点数事件
    /// </summary>
    /// <param name="value">浮点数值</param>
    public void TriggerFloatEvent(float value)
    {
        if (onFloatEvent != null)
        {
            onFloatEvent.Invoke(value);
            LogEvent($"FloatEvent: {value}");
        }
    }
    
    /// <summary>
    /// 触发Vector3事件
    /// </summary>
    /// <param name="vector">向量值</param>
    public void TriggerVector3Event(Vector3 vector)
    {
        if (onVector3Event != null)
        {
            onVector3Event.Invoke(vector);
            LogEvent($"Vector3Event: {vector}");
        }
    }
    
    /// <summary>
    /// 触发GameObject事件
    /// </summary>
    /// <param name="gameObject">游戏对象</param>
    public void TriggerGameObjectEvent(GameObject gameObject)
    {
        if (onGameObjectEvent != null)
        {
            onGameObjectEvent.Invoke(gameObject);
            LogEvent($"GameObjectEvent: {gameObject.name}");
        }
    }
    
    /// <summary>
    /// 触发自定义事件
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="number">数字</param>
    public void TriggerCustomEvent(string message, int number)
    {
        if (onCustomEvent != null)
        {
            onCustomEvent.Invoke(message, number);
            LogEvent($"CustomEvent: {message}, {number}");
        }
    }
    
    /// <summary>
    /// 触发玩家事件
    /// </summary>
    /// <param name="player">玩家对象</param>
    /// <param name="action">动作</param>
    public void TriggerPlayerEvent(GameObject player, string action)
    {
        if (onPlayerEvent != null)
        {
            onPlayerEvent.Invoke(player, action);
            LogEvent($"PlayerEvent: {player.name}, {action}");
        }
    }
    
    /// <summary>
    /// 触发分数事件
    /// </summary>
    /// <param name="currentScore">当前分数</param>
    /// <param name="previousScore">之前分数</param>
    public void TriggerScoreEvent(int currentScore, int previousScore)
    {
        if (onScoreEvent != null)
        {
            onScoreEvent.Invoke(currentScore, previousScore);
            LogEvent($"ScoreEvent: {currentScore} (was {previousScore})");
        }
    }
    
    /// <summary>
    /// 触发伤害事件
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="damage">伤害值</param>
    /// <param name="hitPoint">击中点</param>
    public void TriggerDamageEvent(GameObject target, float damage, Vector3 hitPoint)
    {
        if (onDamageEvent != null)
        {
            onDamageEvent.Invoke(target, damage, hitPoint);
            LogEvent($"DamageEvent: {target.name}, {damage} damage at {hitPoint}");
        }
    }
    
    /// <summary>
    /// 添加事件监听器
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="listener">监听器</param>
    public void AddEventListener(string eventType, UnityAction listener)
    {
        switch (eventType.ToLower())
        {
            case "start":
                onStartEvent.AddListener(listener);
                break;
            case "update":
                onUpdateEvent.AddListener(listener);
                break;
            case "destroy":
                onDestroyEvent.AddListener(listener);
                break;
            default:
                Debug.LogWarning($"未知的事件类型: {eventType}");
                break;
        }
        
        Debug.Log($"添加事件监听器: {eventType}");
    }
    
    /// <summary>
    /// 移除事件监听器
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="listener">监听器</param>
    public void RemoveEventListener(string eventType, UnityAction listener)
    {
        switch (eventType.ToLower())
        {
            case "start":
                onStartEvent.RemoveListener(listener);
                break;
            case "update":
                onUpdateEvent.RemoveListener(listener);
                break;
            case "destroy":
                onDestroyEvent.RemoveListener(listener);
                break;
            default:
                Debug.LogWarning($"未知的事件类型: {eventType}");
                break;
        }
        
        Debug.Log($"移除事件监听器: {eventType}");
    }
    
    /// <summary>
    /// 清除所有事件监听器
    /// </summary>
    public void ClearAllEventListeners()
    {
        foreach (var unityEvent in registeredEvents)
        {
            if (unityEvent != null)
            {
                unityEvent.RemoveAllListeners();
            }
        }
        
        Debug.Log("所有事件监听器已清除");
    }
    
    /// <summary>
    /// 获取事件监听器数量
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <returns>监听器数量</returns>
    public int GetEventListenerCount(string eventType)
    {
        switch (eventType.ToLower())
        {
            case "start":
                return onStartEvent.GetPersistentEventCount();
            case "update":
                return onUpdateEvent.GetPersistentEventCount();
            case "destroy":
                return onDestroyEvent.GetPersistentEventCount();
            case "message":
                return onMessageEvent.GetPersistentEventCount();
            case "number":
                return onNumberEvent.GetPersistentEventCount();
            case "float":
                return onFloatEvent.GetPersistentEventCount();
            case "vector3":
                return onVector3Event.GetPersistentEventCount();
            case "gameobject":
                return onGameObjectEvent.GetPersistentEventCount();
            default:
                Debug.LogWarning($"未知的事件类型: {eventType}");
                return 0;
        }
    }
    
    /// <summary>
    /// 记录事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    private void LogEvent(string eventName)
    {
        if (enableDebugLog)
        {
            Debug.Log($"事件触发: {eventName}");
        }
        
        // 添加到事件历史
        eventHistory.Add($"{System.DateTime.Now:HH:mm:ss} - {eventName}");
        
        // 限制历史记录数量
        if (eventHistory.Count > 50)
        {
            eventHistory.RemoveAt(0);
        }
        
        onEventTriggered?.Invoke(eventName);
    }
    
    // 事件处理器
    private void OnStartEventHandler()
    {
        Debug.Log("开始事件处理器被调用");
    }
    
    private void OnUpdateEventHandler()
    {
        Debug.Log("更新事件处理器被调用");
    }
    
    private void OnDestroyEventHandler()
    {
        Debug.Log("销毁事件处理器被调用");
    }
    
    private void OnMessageEventHandler(string message)
    {
        Debug.Log($"消息事件处理器: {message}");
    }
    
    private void OnNumberEventHandler(int number)
    {
        Debug.Log($"数字事件处理器: {number}");
    }
    
    private void OnFloatEventHandler(float value)
    {
        Debug.Log($"浮点数事件处理器: {value}");
    }
    
    private void OnVector3EventHandler(Vector3 vector)
    {
        Debug.Log($"Vector3事件处理器: {vector}");
    }
    
    private void OnGameObjectEventHandler(GameObject gameObject)
    {
        Debug.Log($"GameObject事件处理器: {gameObject.name}");
    }
    
    private void OnCustomEventHandler(string message, int number)
    {
        Debug.Log($"自定义事件处理器: {message}, {number}");
    }
    
    private void OnPlayerEventHandler(GameObject player, string action)
    {
        Debug.Log($"玩家事件处理器: {player.name} - {action}");
    }
    
    private void OnScoreEventHandler(int currentScore, int previousScore)
    {
        Debug.Log($"分数事件处理器: {currentScore} (之前: {previousScore})");
    }
    
    private void OnDamageEventHandler(GameObject target, float damage, Vector3 hitPoint)
    {
        Debug.Log($"伤害事件处理器: {target.name} 受到 {damage} 点伤害，击中点: {hitPoint}");
    }
    
    /// <summary>
    /// 获取事件系统信息
    /// </summary>
    public void GetEventSystemInfo()
    {
        Debug.Log("=== 事件系统信息 ===");
        Debug.Log($"事件系统启用: {enableEventSystem}");
        Debug.Log($"事件系统活跃: {isEventSystemActive}");
        Debug.Log($"总事件数量: {totalEventCount}");
        Debug.Log($"活跃事件数量: {activeEventCount}");
        Debug.Log($"调试日志: {enableDebugLog}");
        Debug.Log($"最大监听器数量: {maxEventListeners}");
        
        Debug.Log("事件监听器数量:");
        Debug.Log($"  开始事件: {GetEventListenerCount("start")}");
        Debug.Log($"  更新事件: {GetEventListenerCount("update")}");
        Debug.Log($"  销毁事件: {GetEventListenerCount("destroy")}");
        Debug.Log($"  消息事件: {GetEventListenerCount("message")}");
        Debug.Log($"  数字事件: {GetEventListenerCount("number")}");
        Debug.Log($"  浮点数事件: {GetEventListenerCount("float")}");
        Debug.Log($"  Vector3事件: {GetEventListenerCount("vector3")}");
        Debug.Log($"  GameObject事件: {GetEventListenerCount("gameobject")}");
        
        Debug.Log($"事件历史记录数量: {eventHistory.Count}");
        if (eventHistory.Count > 0)
        {
            Debug.Log("最近的事件:");
            for (int i = Mathf.Max(0, eventHistory.Count - 5); i < eventHistory.Count; i++)
            {
                Debug.Log($"  {eventHistory[i]}");
            }
        }
    }
    
    /// <summary>
    /// 重置事件系统
    /// </summary>
    public void ResetEventSystem()
    {
        // 清除所有监听器
        ClearAllEventListeners();
        
        // 重新注册监听器
        RegisterEventListeners();
        
        // 清空事件历史
        eventHistory.Clear();
        
        Debug.Log("事件系统已重置");
    }
    
    private void Update()
    {
        // 触发更新事件
        if (onUpdateEvent != null)
        {
            onUpdateEvent.Invoke();
        }
    }
    
    private void OnDestroy()
    {
        // 触发销毁事件
        if (onDestroyEvent != null)
        {
            onDestroyEvent.Invoke();
        }
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("事件系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 事件状态
        GUILayout.Label($"事件系统活跃: {isEventSystemActive}");
        GUILayout.Label($"总事件数量: {totalEventCount}");
        GUILayout.Label($"活跃事件数量: {activeEventCount}");
        GUILayout.Label($"事件历史数量: {eventHistory.Count}");
        
        GUILayout.Space(10);
        
        // 基本事件触发
        GUILayout.Label("基本事件:", EditorStyles.boldLabel);
        if (GUILayout.Button("触发开始事件"))
        {
            TriggerStartEvent();
        }
        
        GUILayout.Space(10);
        
        // 带参数的事件触发
        GUILayout.Label("带参数的事件:", EditorStyles.boldLabel);
        
        if (GUILayout.Button("触发消息事件"))
        {
            TriggerMessageEvent(messageText);
        }
        
        if (GUILayout.Button("触发数字事件"))
        {
            TriggerNumberEvent(numberValue);
        }
        
        if (GUILayout.Button("触发浮点数事件"))
        {
            TriggerFloatEvent(floatValue);
        }
        
        if (GUILayout.Button("触发Vector3事件"))
        {
            TriggerVector3Event(vectorValue);
        }
        
        if (GUILayout.Button("触发GameObject事件"))
        {
            TriggerGameObjectEvent(gameObject);
        }
        
        GUILayout.Space(10);
        
        // 自定义事件触发
        GUILayout.Label("自定义事件:", EditorStyles.boldLabel);
        
        if (GUILayout.Button("触发自定义事件"))
        {
            TriggerCustomEvent("自定义消息", 123);
        }
        
        if (GUILayout.Button("触发玩家事件"))
        {
            TriggerPlayerEvent(gameObject, "跳跃");
        }
        
        if (GUILayout.Button("触发分数事件"))
        {
            TriggerScoreEvent(100, 50);
        }
        
        if (GUILayout.Button("触发伤害事件"))
        {
            TriggerDamageEvent(gameObject, 25f, transform.position);
        }
        
        GUILayout.Space(10);
        
        // 事件管理
        GUILayout.Label("事件管理:", EditorStyles.boldLabel);
        
        if (GUILayout.Button("清除所有监听器"))
        {
            ClearAllEventListeners();
        }
        
        if (GUILayout.Button("重置事件系统"))
        {
            ResetEventSystem();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取事件系统信息"))
        {
            GetEventSystemInfo();
        }
        
        GUILayout.EndArea();
    }
} 