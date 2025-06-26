using UnityEngine;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine.Events;
using System.Collections.Generic;

namespace UnityEditor.Events.Examples
{
    /// <summary>
    /// UnityEditor.Events 命名空间使用示例
    /// 演示编辑器事件系统的动态事件绑定和管理功能
    /// </summary>
    public class EventsExample : MonoBehaviour
    {
        [Header("事件配置")]
        [SerializeField] private bool enableDynamicEvents = true;
        [SerializeField] private int maxEventListeners = 10;
        [SerializeField] private string eventName = "CustomEvent";
        
        [Header("事件状态")]
        [SerializeField] private int currentListenerCount = 0;
        [SerializeField] private bool isEventActive = false;
        [SerializeField] private string lastEventData = "";
        
        [Header("目标对象")]
        [SerializeField] private GameObject targetObject;
        [SerializeField] private MonoBehaviour targetComponent;
        
        private UnityEvent customEvent;
        private List<UnityAction> registeredListeners = new List<UnityAction>();
        private Dictionary<string, UnityEvent> eventRegistry = new Dictionary<string, UnityEvent>();
        
        /// <summary>
        /// 初始化事件系统
        /// </summary>
        private void Start()
        {
            InitializeEventSystem();
        }
        
        /// <summary>
        /// 初始化事件系统
        /// </summary>
        private void InitializeEventSystem()
        {
            // 创建主事件
            customEvent = new UnityEvent();
            
            // 注册到事件注册表
            eventRegistry[eventName] = customEvent;
            
            // 添加默认监听器
            AddDefaultListeners();
            
            isEventActive = true;
            Debug.Log("事件系统初始化完成");
        }
        
        /// <summary>
        /// 添加默认监听器
        /// </summary>
        private void AddDefaultListeners()
        {
            // 添加日志监听器
            UnityAction logAction = () => Debug.Log($"事件 '{eventName}' 被触发");
            AddListener(eventName, logAction);
            
            // 添加状态更新监听器
            UnityAction statusAction = () => UpdateEventStatus();
            AddListener(eventName, statusAction);
        }
        
        /// <summary>
        /// 添加事件监听器
        /// </summary>
        public void AddListener(string eventName, UnityAction listener)
        {
            if (!eventRegistry.ContainsKey(eventName))
            {
                eventRegistry[eventName] = new UnityEvent();
            }
            
            if (registeredListeners.Count >= maxEventListeners)
            {
                Debug.LogWarning($"已达到最大监听器数量限制: {maxEventListeners}");
                return;
            }
            
            eventRegistry[eventName].AddListener(listener);
            registeredListeners.Add(listener);
            currentListenerCount = registeredListeners.Count;
            
            Debug.Log($"添加监听器到事件 '{eventName}': {listener.Method.Name}");
        }
        
        /// <summary>
        /// 移除事件监听器
        /// </summary>
        public void RemoveListener(string eventName, UnityAction listener)
        {
            if (eventRegistry.ContainsKey(eventName))
            {
                eventRegistry[eventName].RemoveListener(listener);
                registeredListeners.Remove(listener);
                currentListenerCount = registeredListeners.Count;
                
                Debug.Log($"从事件 '{eventName}' 移除监听器: {listener.Method.Name}");
            }
        }
        
        /// <summary>
        /// 移除所有监听器
        /// </summary>
        public void RemoveAllListeners(string eventName)
        {
            if (eventRegistry.ContainsKey(eventName))
            {
                eventRegistry[eventName].RemoveAllListeners();
                registeredListeners.Clear();
                currentListenerCount = 0;
                
                Debug.Log($"移除事件 '{eventName}' 的所有监听器");
            }
        }
        
        /// <summary>
        /// 触发事件
        /// </summary>
        public void TriggerEvent(string eventName)
        {
            if (eventRegistry.ContainsKey(eventName))
            {
                lastEventData = $"事件 '{eventName}' 在 {System.DateTime.Now:HH:mm:ss} 被触发";
                eventRegistry[eventName].Invoke();
                
                Debug.Log($"触发事件: {eventName}");
            }
            else
            {
                Debug.LogWarning($"事件 '{eventName}' 不存在");
            }
        }
        
        /// <summary>
        /// 创建新事件
        /// </summary>
        public void CreateEvent(string newEventName)
        {
            if (eventRegistry.ContainsKey(newEventName))
            {
                Debug.LogWarning($"事件 '{newEventName}' 已存在");
                return;
            }
            
            eventRegistry[newEventName] = new UnityEvent();
            Debug.Log($"创建新事件: {newEventName}");
        }
        
        /// <summary>
        /// 删除事件
        /// </summary>
        public void DeleteEvent(string eventName)
        {
            if (eventRegistry.ContainsKey(eventName))
            {
                RemoveAllListeners(eventName);
                eventRegistry.Remove(eventName);
                Debug.Log($"删除事件: {eventName}");
            }
        }
        
        /// <summary>
        /// 获取事件信息
        /// </summary>
        public string GetEventInfo(string eventName)
        {
            if (eventRegistry.ContainsKey(eventName))
            {
                var evt = eventRegistry[eventName];
                return $"事件: {eventName}, 监听器数量: {evt.GetPersistentEventCount()}";
            }
            return $"事件 '{eventName}' 不存在";
        }
        
        /// <summary>
        /// 动态绑定组件方法
        /// </summary>
        public void BindComponentMethod(string eventName, MonoBehaviour component, string methodName)
        {
            if (component == null)
            {
                Debug.LogError("目标组件为空");
                return;
            }
            
            var method = component.GetType().GetMethod(methodName);
            if (method == null)
            {
                Debug.LogError($"方法 '{methodName}' 在组件 '{component.GetType().Name}' 中不存在");
                return;
            }
            
            // 创建动态调用
            UnityAction dynamicAction = () => method.Invoke(component, null);
            AddListener(eventName, dynamicAction);
            
            Debug.Log($"动态绑定方法: {component.GetType().Name}.{methodName} 到事件 '{eventName}'");
        }
        
        /// <summary>
        /// 使用UnityEditor.Events进行持久化绑定
        /// </summary>
        public void PersistEventBinding(string eventName, MonoBehaviour target, string methodName)
        {
            if (target == null)
            {
                Debug.LogError("目标对象为空");
                return;
            }
            
            if (!eventRegistry.ContainsKey(eventName))
            {
                Debug.LogError($"事件 '{eventName}' 不存在");
                return;
            }
            
            try
            {
                // 使用UnityEditor.Events进行持久化绑定
                UnityEventTools.AddPersistentListener(eventRegistry[eventName], 
                    new UnityAction(() => target.SendMessage(methodName)));
                
                Debug.Log($"持久化绑定: {target.GetType().Name}.{methodName} 到事件 '{eventName}'");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"持久化绑定失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 移除持久化绑定
        /// </summary>
        public void RemovePersistentBinding(string eventName, int index)
        {
            if (eventRegistry.ContainsKey(eventName))
            {
                try
                {
                    UnityEventTools.RemovePersistentListener(eventRegistry[eventName], index);
                    Debug.Log($"移除持久化绑定: 事件 '{eventName}' 索引 {index}");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"移除持久化绑定失败: {e.Message}");
                }
            }
        }
        
        /// <summary>
        /// 更新事件状态
        /// </summary>
        private void UpdateEventStatus()
        {
            currentListenerCount = registeredListeners.Count;
            isEventActive = eventRegistry.Count > 0;
        }
        
        /// <summary>
        /// 获取所有事件名称
        /// </summary>
        public string[] GetAllEventNames()
        {
            string[] names = new string[eventRegistry.Count];
            eventRegistry.Keys.CopyTo(names, 0);
            return names;
        }
        
        /// <summary>
        /// 清理事件系统
        /// </summary>
        private void OnDestroy()
        {
            foreach (var evt in eventRegistry.Values)
            {
                evt.RemoveAllListeners();
            }
            eventRegistry.Clear();
            registeredListeners.Clear();
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 500, 700));
            GUILayout.Label("UnityEditor.Events 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {(isEventActive ? "活跃" : "非活跃")}");
            GUILayout.Label($"监听器数量: {currentListenerCount}");
            GUILayout.Label($"最后事件: {lastEventData}");
            
            GUILayout.Space(10);
            GUILayout.Label("事件控制", EditorStyles.boldLabel);
            
            eventName = GUILayout.TextField("事件名称", eventName);
            
            if (GUILayout.Button("触发事件"))
            {
                TriggerEvent(eventName);
            }
            
            if (GUILayout.Button("创建事件"))
            {
                CreateEvent(eventName);
            }
            
            if (GUILayout.Button("删除事件"))
            {
                DeleteEvent(eventName);
            }
            
            GUILayout.Space(10);
            GUILayout.Label("监听器管理", EditorStyles.boldLabel);
            
            if (GUILayout.Button("添加测试监听器"))
            {
                UnityAction testAction = () => Debug.Log("测试监听器被调用");
                AddListener(eventName, testAction);
            }
            
            if (GUILayout.Button("移除所有监听器"))
            {
                RemoveAllListeners(eventName);
            }
            
            GUILayout.Space(10);
            GUILayout.Label("动态绑定", EditorStyles.boldLabel);
            
            targetComponent = (MonoBehaviour)EditorGUILayout.ObjectField("目标组件", targetComponent, typeof(MonoBehaviour), true);
            
            if (targetComponent != null)
            {
                if (GUILayout.Button("绑定Start方法"))
                {
                    BindComponentMethod(eventName, targetComponent, "Start");
                }
                
                if (GUILayout.Button("绑定Update方法"))
                {
                    BindComponentMethod(eventName, targetComponent, "Update");
                }
                
                if (GUILayout.Button("持久化绑定Start方法"))
                {
                    PersistEventBinding(eventName, targetComponent, "Start");
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("事件信息", EditorStyles.boldLabel);
            
            string[] eventNames = GetAllEventNames();
            foreach (string name in eventNames)
            {
                GUILayout.Label(GetEventInfo(name));
            }
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            enableDynamicEvents = EditorGUILayout.Toggle("启用动态事件", enableDynamicEvents);
            maxEventListeners = EditorGUILayout.IntField("最大监听器数量", maxEventListeners);
            
            GUILayout.EndArea();
        }
    }
} 