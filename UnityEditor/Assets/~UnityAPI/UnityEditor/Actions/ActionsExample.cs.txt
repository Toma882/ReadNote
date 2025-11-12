using UnityEngine;
using UnityEditor;
using UnityEditor.Actions;

/// <summary>
/// UnityEditor.Actions 命名空间案例演示
/// 展示编辑器动作系统的使用，包括动作注册、执行和回调
/// </summary>
public class ActionsExample : MonoBehaviour
{
    [Header("动作系统配置")]
    [SerializeField] private bool enableActionSystem = true; //启用动作系统
    [SerializeField] private bool enableActionLogging = true; //启用动作日志
    [SerializeField] private bool enableActionValidation = true; //启用动作验证
    [SerializeField] private bool enableActionUndo = true; //启用动作撤销
    [SerializeField] private bool enableActionRedo = true; //启用动作重做
    
    [Header("动作类型")]
    [SerializeField] private ActionType currentActionType = ActionType.Create; //当前动作类型
    [SerializeField] private string actionName = "CustomAction"; //动作名称
    [SerializeField] private string actionDescription = "自定义动作描述"; //动作描述
    [SerializeField] private bool isActionEnabled = true; //动作是否启用
    [SerializeField] private bool isActionVisible = true; //动作是否可见
    
    [Header("动作状态")]
    [SerializeField] private string actionState = "未注册"; //动作状态
    [SerializeField] private bool isActionExecuting = false; //动作是否正在执行
    [SerializeField] private float actionExecutionTime = 0f; //动作执行时间
    [SerializeField] private int actionExecutionCount = 0; //动作执行次数
    [SerializeField] private string lastActionResult = ""; //最后动作结果
    
    [Header("动作历史")]
    [SerializeField] private string[] actionHistory = new string[10]; //动作历史
    [SerializeField] private int actionHistoryIndex = 0; //动作历史索引
    [SerializeField] private bool enableActionHistory = true; //启用动作历史
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceMonitoring = true; //启用性能监控
    [SerializeField] private float[] executionTimeHistory = new float[100]; //执行时间历史
    [SerializeField] private int executionTimeIndex = 0; //执行时间索引
    [SerializeField] private float averageExecutionTime = 0f; //平均执行时间
    [SerializeField] private float maxExecutionTime = 0f; //最大执行时间
    
    private ActionManager actionManager;
    private CustomAction customAction;
    private bool isInitialized = false;
    private float lastExecutionTime = 0f;

    private void Start()
    {
        InitializeActionSystem();
    }

    /// <summary>
    /// 初始化动作系统
    /// </summary>
    private void InitializeActionSystem()
    {
        if (!enableActionSystem) return;
        
        // 创建动作管理器
        actionManager = new ActionManager();
        
        // 创建自定义动作
        CreateCustomAction();
        
        // 注册动作
        RegisterActions();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        isInitialized = true;
        actionState = "已初始化";
        Debug.Log("动作系统初始化完成");
    }

    /// <summary>
    /// 创建自定义动作
    /// </summary>
    private void CreateCustomAction()
    {
        customAction = new CustomAction(actionName, actionDescription);
        customAction.SetEnabled(isActionEnabled);
        customAction.SetVisible(isActionVisible);
        
        Debug.Log($"自定义动作已创建: {actionName}");
    }

    /// <summary>
    /// 注册动作
    /// </summary>
    private void RegisterActions()
    {
        if (customAction != null)
        {
            // 注册动作到管理器
            actionManager.RegisterAction(customAction);
            
            // 设置动作回调
            customAction.OnExecute += OnActionExecute;
            customAction.OnValidate += OnActionValidate;
            customAction.OnUndo += OnActionUndo;
            customAction.OnRedo += OnActionRedo;
            
            Debug.Log("动作已注册到管理器");
        }
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            executionTimeHistory = new float[100];
            executionTimeIndex = 0;
            averageExecutionTime = 0f;
            maxExecutionTime = 0f;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新动作状态
        UpdateActionStatus();
        
        // 性能监控
        if (enablePerformanceMonitoring)
        {
            UpdatePerformanceMonitoring();
        }
    }

    /// <summary>
    /// 更新动作状态
    /// </summary>
    private void UpdateActionStatus()
    {
        if (customAction != null)
        {
            isActionEnabled = customAction.IsEnabled();
            isActionVisible = customAction.IsVisible();
        }
    }

    /// <summary>
    /// 更新性能监控
    /// </summary>
    private void UpdatePerformanceMonitoring()
    {
        // 计算平均执行时间
        float totalTime = 0f;
        int validCount = 0;
        
        for (int i = 0; i < executionTimeHistory.Length; i++)
        {
            if (executionTimeHistory[i] > 0f)
            {
                totalTime += executionTimeHistory[i];
                validCount++;
            }
        }
        
        if (validCount > 0)
        {
            averageExecutionTime = totalTime / validCount;
        }
    }

    /// <summary>
    /// 动作执行回调
    /// </summary>
    private void OnActionExecute()
    {
        float startTime = Time.realtimeSinceStartup;
        
        isActionExecuting = true;
        actionExecutionCount++;
        
        // 执行动作逻辑
        ExecuteActionLogic();
        
        // 记录执行时间
        actionExecutionTime = Time.realtimeSinceStartup - startTime;
        lastExecutionTime = actionExecutionTime;
        
        // 更新性能历史
        executionTimeHistory[executionTimeIndex] = actionExecutionTime;
        executionTimeIndex = (executionTimeIndex + 1) % 100;
        
        // 更新最大执行时间
        if (actionExecutionTime > maxExecutionTime)
        {
            maxExecutionTime = actionExecutionTime;
        }
        
        // 记录到历史
        if (enableActionHistory)
        {
            string historyEntry = $"[{System.DateTime.Now:HH:mm:ss}] 执行动作: {actionName} (耗时: {actionExecutionTime * 1000:F2}ms)";
            actionHistory[actionHistoryIndex] = historyEntry;
            actionHistoryIndex = (actionHistoryIndex + 1) % actionHistory.Length;
        }
        
        isActionExecuting = false;
        lastActionResult = "执行成功";
        
        if (enableActionLogging)
        {
            Debug.Log($"动作执行完成: {actionName}, 耗时: {actionExecutionTime * 1000:F2}ms");
        }
    }

    /// <summary>
    /// 执行动作逻辑
    /// </summary>
    private void ExecuteActionLogic()
    {
        switch (currentActionType)
        {
            case ActionType.Create:
                CreateGameObject();
                break;
            case ActionType.Delete:
                DeleteSelectedObjects();
                break;
            case ActionType.Modify:
                ModifySelectedObjects();
                break;
            case ActionType.Move:
                MoveSelectedObjects();
                break;
            case ActionType.Rotate:
                RotateSelectedObjects();
                break;
            case ActionType.Scale:
                ScaleSelectedObjects();
                break;
        }
    }

    /// <summary>
    /// 创建游戏对象
    /// </summary>
    private void CreateGameObject()
    {
        GameObject newObject = new GameObject($"CreatedObject_{actionExecutionCount}");
        newObject.transform.position = Random.insideUnitSphere * 5f;
        
        // 添加一些组件
        newObject.AddComponent<MeshRenderer>();
        newObject.AddComponent<MeshFilter>();
        newObject.AddComponent<BoxCollider>();
        
        Debug.Log($"创建游戏对象: {newObject.name}");
    }

    /// <summary>
    /// 删除选中的对象
    /// </summary>
    private void DeleteSelectedObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        foreach (var obj in selectedObjects)
        {
            if (obj != null)
            {
                DestroyImmediate(obj);
            }
        }
        
        Debug.Log($"删除 {selectedObjects.Length} 个选中对象");
    }

    /// <summary>
    /// 修改选中的对象
    /// </summary>
    private void ModifySelectedObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        foreach (var obj in selectedObjects)
        {
            if (obj != null)
            {
                // 随机修改位置
                obj.transform.position += Random.insideUnitSphere * 0.5f;
                
                // 随机修改旋转
                obj.transform.rotation *= Quaternion.Euler(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));
            }
        }
        
        Debug.Log($"修改 {selectedObjects.Length} 个选中对象");
    }

    /// <summary>
    /// 移动选中的对象
    /// </summary>
    private void MoveSelectedObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        Vector3 moveDirection = Random.onUnitSphere;
        
        foreach (var obj in selectedObjects)
        {
            if (obj != null)
            {
                obj.transform.position += moveDirection * 2f;
            }
        }
        
        Debug.Log($"移动 {selectedObjects.Length} 个选中对象");
    }

    /// <summary>
    /// 旋转选中的对象
    /// </summary>
    private void RotateSelectedObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        Vector3 rotationAxis = Random.onUnitSphere;
        float rotationAngle = Random.Range(30f, 90f);
        
        foreach (var obj in selectedObjects)
        {
            if (obj != null)
            {
                obj.transform.Rotate(rotationAxis, rotationAngle);
            }
        }
        
        Debug.Log($"旋转 {selectedObjects.Length} 个选中对象");
    }

    /// <summary>
    /// 缩放选中的对象
    /// </summary>
    private void ScaleSelectedObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        float scaleFactor = Random.Range(0.8f, 1.2f);
        
        foreach (var obj in selectedObjects)
        {
            if (obj != null)
            {
                obj.transform.localScale *= scaleFactor;
            }
        }
        
        Debug.Log($"缩放 {selectedObjects.Length} 个选中对象");
    }

    /// <summary>
    /// 动作验证回调
    /// </summary>
    private bool OnActionValidate()
    {
        if (!enableActionValidation) return true;
        
        // 验证动作是否可以执行
        bool canExecute = true;
        
        switch (currentActionType)
        {
            case ActionType.Delete:
            case ActionType.Modify:
            case ActionType.Move:
            case ActionType.Rotate:
            case ActionType.Scale:
                canExecute = Selection.gameObjects.Length > 0;
                break;
        }
        
        if (!canExecute)
        {
            lastActionResult = "验证失败：没有选中对象";
            Debug.LogWarning("动作验证失败");
        }
        
        return canExecute;
    }

    /// <summary>
    /// 动作撤销回调
    /// </summary>
    private void OnActionUndo()
    {
        if (!enableActionUndo) return;
        
        // 实现撤销逻辑
        Debug.Log("执行撤销操作");
        lastActionResult = "已撤销";
        
        if (enableActionHistory)
        {
            string historyEntry = $"[{System.DateTime.Now:HH:mm:ss}] 撤销动作: {actionName}";
            actionHistory[actionHistoryIndex] = historyEntry;
            actionHistoryIndex = (actionHistoryIndex + 1) % actionHistory.Length;
        }
    }

    /// <summary>
    /// 动作重做回调
    /// </summary>
    private void OnActionRedo()
    {
        if (!enableActionRedo) return;
        
        // 实现重做逻辑
        Debug.Log("执行重做操作");
        lastActionResult = "已重做";
        
        if (enableActionHistory)
        {
            string historyEntry = $"[{System.DateTime.Now:HH:mm:ss}] 重做动作: {actionName}";
            actionHistory[actionHistoryIndex] = historyEntry;
            actionHistoryIndex = (actionHistoryIndex + 1) % actionHistory.Length;
        }
    }

    /// <summary>
    /// 执行动作
    /// </summary>
    public void ExecuteAction()
    {
        if (customAction != null && customAction.IsEnabled())
        {
            customAction.Execute();
        }
    }

    /// <summary>
    /// 撤销动作
    /// </summary>
    public void UndoAction()
    {
        if (customAction != null && enableActionUndo)
        {
            customAction.Undo();
        }
    }

    /// <summary>
    /// 重做动作
    /// </summary>
    public void RedoAction()
    {
        if (customAction != null && enableActionRedo)
        {
            customAction.Redo();
        }
    }

    /// <summary>
    /// 设置动作类型
    /// </summary>
    /// <param name="actionType">动作类型</param>
    public void SetActionType(ActionType actionType)
    {
        currentActionType = actionType;
        Debug.Log($"动作类型已设置: {actionType}");
    }

    /// <summary>
    /// 设置动作名称
    /// </summary>
    /// <param name="name">动作名称</param>
    public void SetActionName(string name)
    {
        actionName = name;
        if (customAction != null)
        {
            customAction.SetName(name);
        }
        Debug.Log($"动作名称已设置: {name}");
    }

    /// <summary>
    /// 启用/禁用动作
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetActionEnabled(bool enabled)
    {
        isActionEnabled = enabled;
        if (customAction != null)
        {
            customAction.SetEnabled(enabled);
        }
        Debug.Log($"动作启用状态已设置: {enabled}");
    }

    /// <summary>
    /// 生成动作报告
    /// </summary>
    public void GenerateActionReport()
    {
        Debug.Log("=== 动作系统报告 ===");
        Debug.Log($"动作系统状态: {actionState}");
        Debug.Log($"当前动作类型: {currentActionType}");
        Debug.Log($"动作名称: {actionName}");
        Debug.Log($"动作描述: {actionDescription}");
        Debug.Log($"动作启用状态: {isActionEnabled}");
        Debug.Log($"动作可见状态: {isActionVisible}");
        Debug.Log($"动作执行次数: {actionExecutionCount}");
        Debug.Log($"最后执行时间: {lastExecutionTime * 1000:F2}ms");
        Debug.Log($"平均执行时间: {averageExecutionTime * 1000:F2}ms");
        Debug.Log($"最大执行时间: {maxExecutionTime * 1000:F2}ms");
        Debug.Log($"最后动作结果: {lastActionResult}");
        Debug.Log($"是否正在执行: {isActionExecuting}");
    }

    /// <summary>
    /// 清除动作历史
    /// </summary>
    public void ClearActionHistory()
    {
        actionHistory = new string[10];
        actionHistoryIndex = 0;
        Debug.Log("动作历史已清除");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Actions 动作系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("动作系统配置:");
        enableActionSystem = GUILayout.Toggle(enableActionSystem, "启用动作系统");
        enableActionLogging = GUILayout.Toggle(enableActionLogging, "启用动作日志");
        enableActionValidation = GUILayout.Toggle(enableActionValidation, "启用动作验证");
        enableActionUndo = GUILayout.Toggle(enableActionUndo, "启用动作撤销");
        enableActionRedo = GUILayout.Toggle(enableActionRedo, "启用动作重做");
        
        GUILayout.Space(10);
        GUILayout.Label("动作类型:");
        currentActionType = (ActionType)System.Enum.Parse(typeof(ActionType), GUILayout.TextField("动作类型", currentActionType.ToString()));
        actionName = GUILayout.TextField("动作名称", actionName);
        actionDescription = GUILayout.TextField("动作描述", actionDescription);
        isActionEnabled = GUILayout.Toggle(isActionEnabled, "动作启用");
        isActionVisible = GUILayout.Toggle(isActionVisible, "动作可见");
        
        GUILayout.Space(10);
        GUILayout.Label("动作状态:");
        GUILayout.Label($"动作状态: {actionState}");
        GUILayout.Label($"执行次数: {actionExecutionCount}");
        GUILayout.Label($"最后执行时间: {lastExecutionTime * 1000:F2}ms");
        GUILayout.Label($"平均执行时间: {averageExecutionTime * 1000:F2}ms");
        GUILayout.Label($"最大执行时间: {maxExecutionTime * 1000:F2}ms");
        GUILayout.Label($"最后结果: {lastActionResult}");
        GUILayout.Label($"正在执行: {isActionExecuting}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("执行动作"))
        {
            ExecuteAction();
        }
        
        if (GUILayout.Button("撤销动作"))
        {
            UndoAction();
        }
        
        if (GUILayout.Button("重做动作"))
        {
            RedoAction();
        }
        
        if (GUILayout.Button("生成动作报告"))
        {
            GenerateActionReport();
        }
        
        if (GUILayout.Button("清除动作历史"))
        {
            ClearActionHistory();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("动作历史:");
        for (int i = 0; i < actionHistory.Length; i++)
        {
            if (!string.IsNullOrEmpty(actionHistory[i]))
            {
                GUILayout.Label(actionHistory[i]);
            }
        }
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 动作类型枚举
/// </summary>
public enum ActionType
{
    Create,
    Delete,
    Modify,
    Move,
    Rotate,
    Scale
}

/// <summary>
/// 动作管理器
/// </summary>
public class ActionManager
{
    private System.Collections.Generic.List<CustomAction> actions = new System.Collections.Generic.List<CustomAction>();

    public void RegisterAction(CustomAction action)
    {
        if (!actions.Contains(action))
        {
            actions.Add(action);
        }
    }

    public void UnregisterAction(CustomAction action)
    {
        actions.Remove(action);
    }

    public CustomAction[] GetActions()
    {
        return actions.ToArray();
    }
}

/// <summary>
/// 自定义动作类
/// </summary>
public class CustomAction
{
    public System.Action OnExecute;
    public System.Func<bool> OnValidate;
    public System.Action OnUndo;
    public System.Action OnRedo;

    private string name;
    private string description;
    private bool isEnabled;
    private bool isVisible;

    public CustomAction(string actionName, string actionDescription)
    {
        name = actionName;
        description = actionDescription;
        isEnabled = true;
        isVisible = true;
    }

    public void Execute()
    {
        if (OnValidate != null && !OnValidate())
        {
            return;
        }

        OnExecute?.Invoke();
    }

    public void Undo()
    {
        OnUndo?.Invoke();
    }

    public void Redo()
    {
        OnRedo?.Invoke();
    }

    public bool IsEnabled() => isEnabled;
    public bool IsVisible() => isVisible;

    public void SetEnabled(bool enabled) => isEnabled = enabled;
    public void SetVisible(bool visible) => isVisible = visible;
    public void SetName(string actionName) => name = actionName;
} 