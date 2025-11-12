using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.UI 命名空间案例演示
/// 展示UI系统的核心功能
/// </summary>
public class UIExample : MonoBehaviour
{
    [Header("UI组件")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private CanvasScaler canvasScaler;
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    
    [Header("UI元素")]
    [SerializeField] private Button[] buttons;
    [SerializeField] private Image[] images;
    [SerializeField] private Text[] texts;
    [SerializeField] private InputField[] inputFields;
    [SerializeField] private Slider[] sliders;
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private Dropdown[] dropdowns;
    [SerializeField] private ScrollRect[] scrollRects;
    
    [Header("UI设置")]
    [SerializeField] private bool enableUI = true;
    [SerializeField] private float uiScale = 1.0f;
    [SerializeField] private Vector2 canvasSize = new Vector2(1920, 1080);
    
    [Header("UI状态")]
    [SerializeField] private bool isInteractable = true;
    [SerializeField] private Color uiColor = Color.white;
    [SerializeField] private string currentText = "";
    
    // UI事件
    private List<UnityEngine.Events.UnityAction> buttonActions = new List<UnityEngine.Events.UnityAction>();
    private List<UnityEngine.Events.UnityAction<bool>> toggleActions = new List<UnityEngine.Events.UnityAction<bool>>();
    
    private void Start()
    {
        InitializeUISystem();
    }
    
    /// <summary>
    /// 初始化UI系统
    /// </summary>
    private void InitializeUISystem()
    {
        // 获取或创建Canvas
        if (mainCanvas == null)
        {
            mainCanvas = GetComponent<Canvas>();
            if (mainCanvas == null)
            {
                mainCanvas = gameObject.AddComponent<Canvas>();
            }
        }
        
        // 获取或创建CanvasScaler
        if (canvasScaler == null)
        {
            canvasScaler = GetComponent<CanvasScaler>();
            if (canvasScaler == null)
            {
                canvasScaler = gameObject.AddComponent<CanvasScaler>();
            }
        }
        
        // 获取或创建GraphicRaycaster
        if (graphicRaycaster == null)
        {
            graphicRaycaster = GetComponent<GraphicRaycaster>();
            if (graphicRaycaster == null)
            {
                graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
            }
        }
        
        // 配置Canvas
        ConfigureCanvas();
        
        // 查找UI元素
        FindUIElements();
        
        // 设置UI事件
        SetupUIEvents();
        
        Debug.Log("UI系统初始化完成");
    }
    
    /// <summary>
    /// 配置Canvas
    /// </summary>
    private void ConfigureCanvas()
    {
        if (mainCanvas == null) return;
        
        // 设置Canvas属性
        mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        mainCanvas.sortingOrder = 0;
        mainCanvas.targetDisplay = 0;
        
        // 配置CanvasScaler
        if (canvasScaler != null)
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = canvasSize;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
        }
        
        Debug.Log("Canvas配置完成");
    }
    
    /// <summary>
    /// 查找UI元素
    /// </summary>
    private void FindUIElements()
    {
        // 查找所有UI组件
        buttons = GetComponentsInChildren<Button>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
        inputFields = GetComponentsInChildren<InputField>();
        sliders = GetComponentsInChildren<Slider>();
        toggles = GetComponentsInChildren<Toggle>();
        dropdowns = GetComponentsInChildren<Dropdown>();
        scrollRects = GetComponentsInChildren<ScrollRect>();
        
        Debug.Log($"找到UI元素 - 按钮: {buttons.Length}, 图片: {images.Length}, 文本: {texts.Length}");
    }
    
    /// <summary>
    /// 设置UI事件
    /// </summary>
    private void SetupUIEvents()
    {
        // 设置按钮事件
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 闭包变量
            UnityEngine.Events.UnityAction action = () => OnButtonClick(index);
            buttons[i].onClick.AddListener(action);
            buttonActions.Add(action);
        }
        
        // 设置Toggle事件
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i; // 闭包变量
            UnityEngine.Events.UnityAction<bool> action = (isOn) => OnToggleChanged(index, isOn);
            toggles[i].onValueChanged.AddListener(action);
            toggleActions.Add(action);
        }
        
        Debug.Log("UI事件设置完成");
    }
    
    /// <summary>
    /// 按钮点击事件
    /// </summary>
    /// <param name="buttonIndex">按钮索引</param>
    private void OnButtonClick(int buttonIndex)
    {
        Debug.Log($"按钮 {buttonIndex} 被点击");
        
        // 执行不同的按钮操作
        switch (buttonIndex)
        {
            case 0:
                ChangeUIColor();
                break;
            case 1:
                ToggleUIInteractable();
                break;
            case 2:
                ScaleUI();
                break;
            default:
                Debug.Log($"按钮 {buttonIndex} 的默认操作");
                break;
        }
    }
    
    /// <summary>
    /// Toggle状态改变事件
    /// </summary>
    /// <param name="toggleIndex">Toggle索引</param>
    /// <param name="isOn">是否开启</param>
    private void OnToggleChanged(int toggleIndex, bool isOn)
    {
        Debug.Log($"Toggle {toggleIndex} 状态改变: {isOn}");
        
        // 根据Toggle状态执行操作
        switch (toggleIndex)
        {
            case 0:
                SetUIEnabled(isOn);
                break;
            case 1:
                SetUIVisible(isOn);
                break;
            default:
                Debug.Log($"Toggle {toggleIndex} 的默认操作");
                break;
        }
    }
    
    /// <summary>
    /// 设置UI启用状态
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetUIEnabled(bool enabled)
    {
        enableUI = enabled;
        
        // 设置所有UI元素的交互状态
        foreach (var button in buttons)
        {
            if (button != null) button.interactable = enabled;
        }
        
        foreach (var inputField in inputFields)
        {
            if (inputField != null) inputField.interactable = enabled;
        }
        
        foreach (var slider in sliders)
        {
            if (slider != null) slider.interactable = enabled;
        }
        
        foreach (var toggle in toggles)
        {
            if (toggle != null) toggle.interactable = enabled;
        }
        
        foreach (var dropdown in dropdowns)
        {
            if (dropdown != null) dropdown.interactable = enabled;
        }
        
        Debug.Log($"UI交互状态已设置为: {enabled}");
    }
    
    /// <summary>
    /// 设置UI可见性
    /// </summary>
    /// <param name="visible">是否可见</param>
    public void SetUIVisible(bool visible)
    {
        // 设置所有UI元素的可见性
        foreach (var image in images)
        {
            if (image != null) image.enabled = visible;
        }
        
        foreach (var text in texts)
        {
            if (text != null) text.enabled = visible;
        }
        
        Debug.Log($"UI可见性已设置为: {visible}");
    }
    
    /// <summary>
    /// 改变UI颜色
    /// </summary>
    public void ChangeUIColor()
    {
        uiColor = new Color(Random.value, Random.value, Random.value, 1f);
        
        // 改变所有UI元素的颜色
        foreach (var image in images)
        {
            if (image != null) image.color = uiColor;
        }
        
        foreach (var text in texts)
        {
            if (text != null) text.color = uiColor;
        }
        
        Debug.Log($"UI颜色已改变为: {uiColor}");
    }
    
    /// <summary>
    /// 切换UI交互状态
    /// </summary>
    public void ToggleUIInteractable()
    {
        isInteractable = !isInteractable;
        SetUIEnabled(isInteractable);
    }
    
    /// <summary>
    /// 缩放UI
    /// </summary>
    public void ScaleUI()
    {
        uiScale = Random.Range(0.5f, 2.0f);
        
        if (canvasScaler != null)
        {
            canvasScaler.scaleFactor = uiScale;
        }
        
        Debug.Log($"UI缩放已设置为: {uiScale}");
    }
    
    /// <summary>
    /// 设置文本内容
    /// </summary>
    /// <param name="text">文本内容</param>
    public void SetText(string text)
    {
        currentText = text;
        
        foreach (var textComponent in texts)
        {
            if (textComponent != null)
            {
                textComponent.text = text;
            }
        }
        
        Debug.Log($"文本内容已设置为: {text}");
    }
    
    /// <summary>
    /// 设置输入框内容
    /// </summary>
    /// <param name="text">输入内容</param>
    public void SetInputFieldText(string text)
    {
        foreach (var inputField in inputFields)
        {
            if (inputField != null)
            {
                inputField.text = text;
            }
        }
        
        Debug.Log($"输入框内容已设置为: {text}");
    }
    
    /// <summary>
    /// 设置滑块值
    /// </summary>
    /// <param name="value">滑块值</param>
    public void SetSliderValue(float value)
    {
        foreach (var slider in sliders)
        {
            if (slider != null)
            {
                slider.value = Mathf.Clamp01(value);
            }
        }
        
        Debug.Log($"滑块值已设置为: {value}");
    }
    
    /// <summary>
    /// 设置下拉菜单选项
    /// </summary>
    /// <param name="options">选项列表</param>
    public void SetDropdownOptions(List<string> options)
    {
        foreach (var dropdown in dropdowns)
        {
            if (dropdown != null)
            {
                dropdown.ClearOptions();
                dropdown.AddOptions(options);
            }
        }
        
        Debug.Log($"下拉菜单选项已设置，共 {options.Count} 个选项");
    }
    
    /// <summary>
    /// 滚动到指定位置
    /// </summary>
    /// <param name="position">滚动位置 (0-1)</param>
    public void ScrollToPosition(float position)
    {
        foreach (var scrollRect in scrollRects)
        {
            if (scrollRect != null)
            {
                scrollRect.verticalNormalizedPosition = Mathf.Clamp01(position);
            }
        }
        
        Debug.Log($"滚动位置已设置为: {position}");
    }
    
    /// <summary>
    /// 获取UI信息
    /// </summary>
    public void GetUIInfo()
    {
        Debug.Log("=== UI信息 ===");
        Debug.Log($"Canvas: {(mainCanvas != null ? mainCanvas.name : "无")}");
        Debug.Log($"CanvasScaler: {(canvasScaler != null ? "已配置" : "无")}");
        Debug.Log($"GraphicRaycaster: {(graphicRaycaster != null ? "已配置" : "无")}");
        Debug.Log($"按钮数量: {buttons.Length}");
        Debug.Log($"图片数量: {images.Length}");
        Debug.Log($"文本数量: {texts.Length}");
        Debug.Log($"输入框数量: {inputFields.Length}");
        Debug.Log($"滑块数量: {sliders.Length}");
        Debug.Log($"Toggle数量: {toggles.Length}");
        Debug.Log($"下拉菜单数量: {dropdowns.Length}");
        Debug.Log($"滚动视图数量: {scrollRects.Length}");
        Debug.Log($"UI启用状态: {enableUI}");
        Debug.Log($"UI缩放: {uiScale}");
        Debug.Log($"UI颜色: {uiColor}");
    }
    
    /// <summary>
    /// 重置UI
    /// </summary>
    public void ResetUI()
    {
        // 重置颜色
        uiColor = Color.white;
        ChangeUIColor();
        
        // 重置缩放
        uiScale = 1.0f;
        if (canvasScaler != null)
        {
            canvasScaler.scaleFactor = uiScale;
        }
        
        // 重置交互状态
        isInteractable = true;
        SetUIEnabled(true);
        
        // 重置文本
        SetText("重置完成");
        
        Debug.Log("UI已重置");
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 600));
        GUILayout.Label("UI系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // UI状态
        GUILayout.Label($"UI启用: {enableUI}");
        GUILayout.Label($"UI缩放: {uiScale:F2}");
        GUILayout.Label($"UI颜色: {uiColor}");
        GUILayout.Label($"当前文本: {currentText}");
        
        GUILayout.Space(10);
        
        // 控制按钮
        if (GUILayout.Button("改变UI颜色"))
        {
            ChangeUIColor();
        }
        
        if (GUILayout.Button("切换UI交互"))
        {
            ToggleUIInteractable();
        }
        
        if (GUILayout.Button("缩放UI"))
        {
            ScaleUI();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("设置文本"))
        {
            SetText("Hello UI System!");
        }
        
        if (GUILayout.Button("设置输入框"))
        {
            SetInputFieldText("输入测试");
        }
        
        if (GUILayout.Button("设置滑块值"))
        {
            SetSliderValue(Random.value);
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取UI信息"))
        {
            GetUIInfo();
        }
        
        if (GUILayout.Button("重置UI"))
        {
            ResetUI();
        }
        
        GUILayout.Space(10);
        
        // 设置选项
        enableUI = GUILayout.Toggle(enableUI, "启用UI");
        
        GUILayout.Label($"UI缩放: {uiScale:F2}");
        uiScale = GUILayout.HorizontalSlider(uiScale, 0.1f, 3.0f);
        
        GUILayout.EndArea();
    }
} 