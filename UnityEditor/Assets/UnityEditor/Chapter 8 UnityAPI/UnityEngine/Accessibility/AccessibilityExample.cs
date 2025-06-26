using UnityEngine;

/// <summary>
/// UnityEngine.Accessibility 命名空间案例演示
/// 展示无障碍功能、屏幕阅读器支持等核心功能
/// </summary>
public class AccessibilityExample : MonoBehaviour
{
    [Header("无障碍设置")]
    [SerializeField] private bool enableAccessibility = true; //启用无障碍功能
    [SerializeField] private string screenReaderText = "这是一个无障碍演示"; //屏幕阅读器文本
    [SerializeField] private bool announceChanges = true; //宣布变化
    [SerializeField] private float announcementDelay = 0.5f; //宣布延迟

    [Header("UI无障碍")]
    [SerializeField] private string buttonLabel = "测试按钮"; //按钮标签
    [SerializeField] private string buttonDescription = "点击此按钮进行测试"; //按钮描述
    [SerializeField] private bool isButtonEnabled = true; //按钮是否启用
    [SerializeField] private bool isButtonVisible = true; //按钮是否可见

    [Header("导航无障碍")]
    [SerializeField] private bool enableKeyboardNavigation = true; //启用键盘导航
    [SerializeField] private bool enableVoiceNavigation = true; //启用语音导航
    [SerializeField] private string navigationHint = "使用Tab键在元素间导航"; //导航提示

    [Header("视觉无障碍")]
    [SerializeField] private bool highContrastMode = false; //高对比度模式
    [SerializeField] private bool largeTextMode = false; //大文本模式
    [SerializeField] private float textScale = 1.0f; //文本缩放
    [SerializeField] private Color highContrastColor = Color.yellow; //高对比度颜色

    private float lastAnnouncementTime = 0f;
    private string lastAnnouncedText = "";

    private void Start()
    {
        InitializeAccessibility();
    }

    /// <summary>
    /// 初始化无障碍功能
    /// </summary>
    private void InitializeAccessibility()
    {
        if (enableAccessibility)
        {
            // 设置屏幕阅读器文本
            SetScreenReaderText(screenReaderText);
            
            // 启用无障碍功能
            EnableAccessibilityFeatures();
            
            Debug.Log("无障碍功能已初始化");
        }
    }

    /// <summary>
    /// 启用无障碍功能
    /// </summary>
    private void EnableAccessibilityFeatures()
    {
        // 这里可以调用Unity的无障碍API
        // 注意：Unity的无障碍API可能因版本而异
        
        Debug.Log("无障碍功能已启用");
    }

    /// <summary>
    /// 设置屏幕阅读器文本
    /// </summary>
    /// <param name="text">要朗读的文本</param>
    public void SetScreenReaderText(string text)
    {
        screenReaderText = text;
        
        if (enableAccessibility && announceChanges)
        {
            AnnounceToScreenReader(text);
        }
    }

    /// <summary>
    /// 向屏幕阅读器宣布文本
    /// </summary>
    /// <param name="text">要宣布的文本</param>
    public void AnnounceToScreenReader(string text)
    {
        if (Time.time - lastAnnouncementTime > announcementDelay)
        {
            // 这里应该调用实际的屏幕阅读器API
            // 由于Unity的无障碍API限制，这里只是模拟
            Debug.Log($"[屏幕阅读器] {text}");
            
            lastAnnouncedText = text;
            lastAnnouncementTime = Time.time;
        }
    }

    /// <summary>
    /// 设置按钮无障碍属性
    /// </summary>
    /// <param name="label">按钮标签</param>
    /// <param name="description">按钮描述</param>
    public void SetButtonAccessibility(string label, string description)
    {
        buttonLabel = label;
        buttonDescription = description;
        
        if (enableAccessibility)
        {
            AnnounceToScreenReader($"按钮: {label}, {description}");
        }
    }

    /// <summary>
    /// 启用/禁用按钮
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetButtonEnabled(bool enabled)
    {
        isButtonEnabled = enabled;
        
        if (enableAccessibility && announceChanges)
        {
            string status = enabled ? "启用" : "禁用";
            AnnounceToScreenReader($"按钮已{status}");
        }
    }

    /// <summary>
    /// 设置高对比度模式
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetHighContrastMode(bool enabled)
    {
        highContrastMode = enabled;
        
        if (enabled)
        {
            // 应用高对比度设置
            ApplyHighContrastSettings();
            AnnounceToScreenReader("高对比度模式已启用");
        }
        else
        {
            // 恢复正常设置
            ApplyNormalSettings();
            AnnounceToScreenReader("高对比度模式已禁用");
        }
    }

    /// <summary>
    /// 设置大文本模式
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetLargeTextMode(bool enabled)
    {
        largeTextMode = enabled;
        
        if (enabled)
        {
            textScale = 1.5f;
            AnnounceToScreenReader("大文本模式已启用");
        }
        else
        {
            textScale = 1.0f;
            AnnounceToScreenReader("大文本模式已禁用");
        }
    }

    /// <summary>
    /// 应用高对比度设置
    /// </summary>
    private void ApplyHighContrastSettings()
    {
        // 这里应该应用实际的高对比度设置
        // 例如修改UI颜色、增加边框等
        Debug.Log("应用高对比度设置");
    }

    /// <summary>
    /// 应用正常设置
    /// </summary>
    private void ApplyNormalSettings()
    {
        // 恢复正常显示设置
        Debug.Log("应用正常显示设置");
    }

    /// <summary>
    /// 获取无障碍信息
    /// </summary>
    public void GetAccessibilityInfo()
    {
        Debug.Log("=== 无障碍信息 ===");
        Debug.Log($"无障碍功能启用: {enableAccessibility}");
        Debug.Log($"屏幕阅读器文本: {screenReaderText}");
        Debug.Log($"宣布变化: {announceChanges}");
        Debug.Log($"键盘导航: {enableKeyboardNavigation}");
        Debug.Log($"语音导航: {enableVoiceNavigation}");
        Debug.Log($"高对比度模式: {highContrastMode}");
        Debug.Log($"大文本模式: {largeTextMode}");
        Debug.Log($"文本缩放: {textScale}");
    }

    /// <summary>
    /// 测试无障碍功能
    /// </summary>
    public void TestAccessibility()
    {
        if (enableAccessibility)
        {
            AnnounceToScreenReader("开始无障碍功能测试");
            
            // 模拟各种无障碍操作
            SetButtonAccessibility("测试按钮", "用于测试无障碍功能");
            SetButtonEnabled(true);
            SetHighContrastMode(true);
            
            Debug.Log("无障碍功能测试完成");
        }
    }

    /// <summary>
    /// 重置无障碍设置
    /// </summary>
    public void ResetAccessibilitySettings()
    {
        enableAccessibility = true;
        screenReaderText = "这是一个无障碍演示";
        announceChanges = true;
        announcementDelay = 0.5f;
        
        buttonLabel = "测试按钮";
        buttonDescription = "点击此按钮进行测试";
        isButtonEnabled = true;
        isButtonVisible = true;
        
        enableKeyboardNavigation = true;
        enableVoiceNavigation = true;
        navigationHint = "使用Tab键在元素间导航";
        
        highContrastMode = false;
        largeTextMode = false;
        textScale = 1.0f;
        
        ApplyNormalSettings();
        AnnounceToScreenReader("无障碍设置已重置");
        
        Debug.Log("无障碍设置已重置");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 600));
        GUILayout.Label("Accessibility 无障碍功能演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("基本设置:");
        
        enableAccessibility = GUILayout.Toggle(enableAccessibility, "启用无障碍功能");
        announceChanges = GUILayout.Toggle(announceChanges, "宣布变化");
        announcementDelay = float.TryParse(GUILayout.TextField("宣布延迟", announcementDelay.ToString()), out var delay) ? delay : announcementDelay;
        
        GUILayout.Space(10);
        GUILayout.Label("屏幕阅读器:");
        
        screenReaderText = GUILayout.TextField("屏幕阅读器文本", screenReaderText);
        if (GUILayout.Button("宣布文本"))
        {
            AnnounceToScreenReader(screenReaderText);
        }
        
        GUILayout.Space(10);
        GUILayout.Label("UI无障碍:");
        
        buttonLabel = GUILayout.TextField("按钮标签", buttonLabel);
        buttonDescription = GUILayout.TextField("按钮描述", buttonDescription);
        
        GUILayout.BeginHorizontal();
        isButtonEnabled = GUILayout.Toggle(isButtonEnabled, "按钮启用");
        isButtonVisible = GUILayout.Toggle(isButtonVisible, "按钮可见");
        GUILayout.EndHorizontal();
        
        if (GUILayout.Button("设置按钮无障碍"))
        {
            SetButtonAccessibility(buttonLabel, buttonDescription);
        }
        
        GUILayout.Space(10);
        GUILayout.Label("导航设置:");
        
        enableKeyboardNavigation = GUILayout.Toggle(enableKeyboardNavigation, "键盘导航");
        enableVoiceNavigation = GUILayout.Toggle(enableVoiceNavigation, "语音导航");
        navigationHint = GUILayout.TextField("导航提示", navigationHint);
        
        GUILayout.Space(10);
        GUILayout.Label("视觉设置:");
        
        highContrastMode = GUILayout.Toggle(highContrastMode, "高对比度模式");
        largeTextMode = GUILayout.Toggle(largeTextMode, "大文本模式");
        textScale = float.TryParse(GUILayout.TextField("文本缩放", textScale.ToString()), out var scale) ? scale : textScale;
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("测试无障碍功能"))
        {
            TestAccessibility();
        }
        
        if (GUILayout.Button("获取无障碍信息"))
        {
            GetAccessibilityInfo();
        }
        
        if (GUILayout.Button("重置无障碍设置"))
        {
            ResetAccessibilitySettings();
        }
        
        GUILayout.EndArea();
    }
} 