using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.UIElements 命名空间案例演示
/// 展示UIElements编辑器扩展的核心功能
/// </summary>
public class UIElementsExample : EditorWindow
{
    [Header("UIElements设置")]
    [SerializeField] private string windowTitle = "UIElements演示";
    [SerializeField] private Vector2 windowSize = new Vector2(800, 600);
    [SerializeField] private bool enableDarkTheme = true;
    
    [Header("UIElements组件")]
    private VisualElement rootElement;
    private VisualElement mainContainer;
    private VisualElement leftPanel;
    private VisualElement rightPanel;
    private VisualElement toolbar;
    private VisualElement statusBar;
    
    [Header("UIElements状态")]
    private bool isInitialized = false;
    private int currentTab = 0;
    private string currentStatus = "就绪";
    
    // UI元素引用
    private TextField titleField;
    private Slider sizeSlider;
    private Toggle themeToggle;
    private Button[] tabButtons;
    private Label statusLabel;
    private ListView itemListView;
    private ObjectField objectField;
    private ColorField colorField;
    private Vector3Field vector3Field;
    private CurveField curveField;
    private GradientField gradientField;
    private EnumField enumField;
    private LayerMaskField layerMaskField;
    private TagField tagField;
    
    [MenuItem("Window/UIElements演示")]
    public static void ShowWindow()
    {
        UIElementsExample window = GetWindow<UIElementsExample>();
        window.titleContent = new GUIContent("UIElements演示");
        window.minSize = new Vector2(600, 400);
        window.Show();
    }
    
    private void CreateGUI()
    {
        InitializeUIElements();
    }
    
    /// <summary>
    /// 初始化UIElements
    /// </summary>
    private void InitializeUIElements()
    {
        if (isInitialized) return;
        
        // 获取根元素
        rootElement = rootVisualElement;
        
        // 设置样式
        SetupStyles();
        
        // 创建主布局
        CreateMainLayout();
        
        // 创建工具栏
        CreateToolbar();
        
        // 创建主容器
        CreateMainContainer();
        
        // 创建状态栏
        CreateStatusBar();
        
        // 初始化标签页
        InitializeTabs();
        
        isInitialized = true;
        UpdateStatus("UIElements初始化完成");
        
        Debug.Log("UIElements系统初始化完成");
    }
    
    /// <summary>
    /// 设置样式
    /// </summary>
    private void SetupStyles()
    {
        // 加载样式表
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor Default Resources/UIElementsExample.uss");
        if (styleSheet != null)
        {
            rootElement.styleSheets.Add(styleSheet);
        }
        
        // 应用主题
        if (enableDarkTheme)
        {
            rootElement.AddToClassList("dark-theme");
        }
        else
        {
            rootElement.AddToClassList("light-theme");
        }
    }
    
    /// <summary>
    /// 创建主布局
    /// </summary>
    private void CreateMainLayout()
    {
        // 创建主容器
        mainContainer = new VisualElement();
        mainContainer.name = "main-container";
        mainContainer.AddToClassList("main-container");
        rootElement.Add(mainContainer);
    }
    
    /// <summary>
    /// 创建工具栏
    /// </summary>
    private void CreateToolbar()
    {
        toolbar = new VisualElement();
        toolbar.name = "toolbar";
        toolbar.AddToClassList("toolbar");
        mainContainer.Add(toolbar);
        
        // 标题字段
        titleField = new TextField("窗口标题");
        titleField.value = windowTitle;
        titleField.RegisterValueChangedCallback(evt =>
        {
            windowTitle = evt.newValue;
            titleContent = new GUIContent(windowTitle);
            UpdateStatus($"标题已更改为: {windowTitle}");
        });
        toolbar.Add(titleField);
        
        // 大小滑块
        sizeSlider = new Slider("窗口大小", 400, 1200);
        sizeSlider.value = windowSize.x;
        sizeSlider.RegisterValueChangedCallback(evt =>
        {
            windowSize = new Vector2(evt.newValue, evt.newValue * 0.75f);
            minSize = windowSize;
            UpdateStatus($"窗口大小已设置为: {windowSize}");
        });
        toolbar.Add(sizeSlider);
        
        // 主题切换
        themeToggle = new Toggle("深色主题");
        themeToggle.value = enableDarkTheme;
        themeToggle.RegisterValueChangedCallback(evt =>
        {
            enableDarkTheme = evt.newValue;
            ApplyTheme();
            UpdateStatus($"主题已切换为: {(enableDarkTheme ? "深色" : "浅色")}");
        });
        toolbar.Add(themeToggle);
        
        // 刷新按钮
        var refreshButton = new Button(() =>
        {
            RefreshUI();
            UpdateStatus("UI已刷新");
        })
        {
            text = "刷新"
        };
        refreshButton.AddToClassList("toolbar-button");
        toolbar.Add(refreshButton);
    }
    
    /// <summary>
    /// 创建主容器
    /// </summary>
    private void CreateMainContainer()
    {
        // 创建水平布局
        var horizontalLayout = new VisualElement();
        horizontalLayout.AddToClassList("horizontal-layout");
        mainContainer.Add(horizontalLayout);
        
        // 创建左侧面板
        CreateLeftPanel(horizontalLayout);
        
        // 创建右侧面板
        CreateRightPanel(horizontalLayout);
    }
    
    /// <summary>
    /// 创建左侧面板
    /// </summary>
    /// <param name="parent">父元素</param>
    private void CreateLeftPanel(VisualElement parent)
    {
        leftPanel = new VisualElement();
        leftPanel.name = "left-panel";
        leftPanel.AddToClassList("left-panel");
        parent.Add(leftPanel);
        
        // 标签页按钮
        CreateTabButtons();
        
        // 内容区域
        CreateContentArea();
    }
    
    /// <summary>
    /// 创建标签页按钮
    /// </summary>
    private void CreateTabButtons()
    {
        var tabContainer = new VisualElement();
        tabContainer.AddToClassList("tab-container");
        leftPanel.Add(tabContainer);
        
        string[] tabNames = { "基础控件", "高级控件", "布局", "样式", "事件" };
        tabButtons = new Button[tabNames.Length];
        
        for (int i = 0; i < tabNames.Length; i++)
        {
            int index = i; // 闭包变量
            tabButtons[i] = new Button(() =>
            {
                SwitchTab(index);
            })
            {
                text = tabNames[i]
            };
            tabButtons[i].AddToClassList("tab-button");
            tabContainer.Add(tabButtons[i]);
        }
    }
    
    /// <summary>
    /// 创建内容区域
    /// </summary>
    private void CreateContentArea()
    {
        var contentArea = new VisualElement();
        contentArea.name = "content-area";
        contentArea.AddToClassList("content-area");
        leftPanel.Add(contentArea);
        
        // 创建不同标签页的内容
        CreateBasicControlsContent(contentArea);
        CreateAdvancedControlsContent(contentArea);
        CreateLayoutContent(contentArea);
        CreateStyleContent(contentArea);
        CreateEventContent(contentArea);
    }
    
    /// <summary>
    /// 创建基础控件内容
    /// </summary>
    /// <param name="parent">父元素</param>
    private void CreateBasicControlsContent(VisualElement parent)
    {
        var container = new VisualElement();
        container.name = "basic-controls";
        container.AddToClassList("tab-content");
        parent.Add(container);
        
        // 标签
        var label = new Label("这是一个标签");
        container.Add(label);
        
        // 按钮
        var button = new Button(() =>
        {
            UpdateStatus("按钮被点击");
        })
        {
            text = "点击我"
        };
        container.Add(button);
        
        // 文本字段
        var textField = new TextField("文本字段");
        textField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"文本已更改: {evt.newValue}");
        });
        container.Add(textField);
        
        // 滑块
        var slider = new Slider("滑块", 0, 100);
        slider.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"滑块值: {evt.newValue}");
        });
        container.Add(slider);
        
        // 切换开关
        var toggle = new Toggle("切换开关");
        toggle.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"切换状态: {evt.newValue}");
        });
        container.Add(toggle);
    }
    
    /// <summary>
    /// 创建高级控件内容
    /// </summary>
    /// <param name="parent">父元素</param>
    private void CreateAdvancedControlsContent(VisualElement parent)
    {
        var container = new VisualElement();
        container.name = "advanced-controls";
        container.AddToClassList("tab-content");
        container.style.display = DisplayStyle.None;
        parent.Add(container);
        
        // 对象字段
        objectField = new ObjectField("对象字段");
        objectField.objectType = typeof(GameObject);
        objectField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"对象已选择: {(evt.newValue != null ? evt.newValue.name : "无")}");
        });
        container.Add(objectField);
        
        // 颜色字段
        colorField = new ColorField("颜色字段");
        colorField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"颜色已更改: {evt.newValue}");
        });
        container.Add(colorField);
        
        // 向量3字段
        vector3Field = new Vector3Field("向量3字段");
        vector3Field.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"向量3已更改: {evt.newValue}");
        });
        container.Add(vector3Field);
        
        // 曲线字段
        curveField = new CurveField("曲线字段");
        curveField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus("曲线已更改");
        });
        container.Add(curveField);
        
        // 渐变字段
        gradientField = new GradientField("渐变字段");
        gradientField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus("渐变已更改");
        });
        container.Add(gradientField);
        
        // 枚举字段
        enumField = new EnumField("枚举字段", HideFlags.None);
        enumField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"枚举已更改: {evt.newValue}");
        });
        container.Add(enumField);
        
        // 层遮罩字段
        layerMaskField = new LayerMaskField("层遮罩字段");
        layerMaskField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"层遮罩已更改: {evt.newValue}");
        });
        container.Add(layerMaskField);
        
        // 标签字段
        tagField = new TagField("标签字段");
        tagField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"标签已更改: {evt.newValue}");
        });
        container.Add(tagField);
    }
    
    /// <summary>
    /// 创建布局内容
    /// </summary>
    /// <param name="parent">父元素</param>
    private void CreateLayoutContent(VisualElement parent)
    {
        var container = new VisualElement();
        container.name = "layout-content";
        container.AddToClassList("tab-content");
        container.style.display = DisplayStyle.None;
        parent.Add(container);
        
        // 水平布局示例
        var horizontalBox = new Box();
        horizontalBox.AddToClassList("layout-example");
        container.Add(horizontalBox);
        
        var horizontalLabel = new Label("水平布局:");
        horizontalBox.Add(horizontalLabel);
        
        var horizontalLayout = new VisualElement();
        horizontalLayout.style.flexDirection = FlexDirection.Row;
        horizontalLayout.style.justifyContent = Justify.SpaceBetween;
        horizontalBox.Add(horizontalLayout);
        
        for (int i = 0; i < 3; i++)
        {
            var button = new Button() { text = $"按钮{i + 1}" };
            horizontalLayout.Add(button);
        }
        
        // 垂直布局示例
        var verticalBox = new Box();
        verticalBox.AddToClassList("layout-example");
        container.Add(verticalBox);
        
        var verticalLabel = new Label("垂直布局:");
        verticalBox.Add(verticalLabel);
        
        var verticalLayout = new VisualElement();
        verticalLayout.style.flexDirection = FlexDirection.Column;
        verticalLayout.style.alignItems = Align.Center;
        verticalBox.Add(verticalLayout);
        
        for (int i = 0; i < 3; i++)
        {
            var button = new Button() { text = $"按钮{i + 1}" };
            verticalLayout.Add(button);
        }
        
        // 网格布局示例
        var gridBox = new Box();
        gridBox.AddToClassList("layout-example");
        container.Add(gridBox);
        
        var gridLabel = new Label("网格布局:");
        gridBox.Add(gridLabel);
        
        var gridLayout = new VisualElement();
        gridLayout.style.flexDirection = FlexDirection.Row;
        gridLayout.style.flexWrap = Wrap.Wrap;
        gridBox.Add(gridLayout);
        
        for (int i = 0; i < 6; i++)
        {
            var button = new Button() { text = $"网格{i + 1}" };
            button.style.width = 80;
            button.style.height = 40;
            button.style.marginRight = 5;
            button.style.marginBottom = 5;
            gridLayout.Add(button);
        }
    }
    
    /// <summary>
    /// 创建样式内容
    /// </summary>
    /// <param name="parent">父元素</param>
    private void CreateStyleContent(VisualElement parent)
    {
        var container = new VisualElement();
        container.name = "style-content";
        container.AddToClassList("tab-content");
        container.style.display = DisplayStyle.None;
        parent.Add(container);
        
        // 样式示例
        var styleExamples = new[]
        {
            new { name = "边框", style = "border-example" },
            new { name = "背景", style = "background-example" },
            new { name = "阴影", style = "shadow-example" },
            new { name = "圆角", style = "rounded-example" }
        };
        
        foreach (var example in styleExamples)
        {
            var box = new Box();
            box.AddToClassList(example.style);
            box.AddToClassList("style-example");
            container.Add(box);
            
            var label = new Label(example.name);
            box.Add(label);
        }
        
        // 动态样式按钮
        var dynamicButton = new Button("动态样式");
        dynamicButton.RegisterCallback<ClickEvent>(evt =>
        {
            if (dynamicButton.ClassListContains("dynamic-style"))
            {
                dynamicButton.RemoveFromClassList("dynamic-style");
                UpdateStatus("动态样式已移除");
            }
            else
            {
                dynamicButton.AddToClassList("dynamic-style");
                UpdateStatus("动态样式已应用");
            }
        });
        container.Add(dynamicButton);
    }
    
    /// <summary>
    /// 创建事件内容
    /// </summary>
    /// <param name="parent">父元素</param>
    private void CreateEventContent(VisualElement parent)
    {
        var container = new VisualElement();
        container.name = "event-content";
        container.AddToClassList("tab-content");
        container.style.display = DisplayStyle.None;
        parent.Add(container);
        
        // 事件示例
        var eventBox = new Box();
        eventBox.AddToClassList("event-example");
        container.Add(eventBox);
        
        // 鼠标事件
        eventBox.RegisterCallback<MouseEnterEvent>(evt =>
        {
            UpdateStatus("鼠标进入事件");
        });
        
        eventBox.RegisterCallback<MouseLeaveEvent>(evt =>
        {
            UpdateStatus("鼠标离开事件");
        });
        
        eventBox.RegisterCallback<MouseDownEvent>(evt =>
        {
            UpdateStatus($"鼠标按下事件: {evt.mousePosition}");
        });
        
        eventBox.RegisterCallback<MouseUpEvent>(evt =>
        {
            UpdateStatus($"鼠标释放事件: {evt.mousePosition}");
        });
        
        var eventLabel = new Label("鼠标事件区域 (移动鼠标到此区域)");
        eventBox.Add(eventLabel);
        
        // 键盘事件
        var keyboardBox = new Box();
        keyboardBox.AddToClassList("event-example");
        container.Add(keyboardBox);
        
        keyboardBox.focusable = true;
        keyboardBox.RegisterCallback<KeyDownEvent>(evt =>
        {
            UpdateStatus($"键盘按下: {evt.keyCode}");
        });
        
        var keyboardLabel = new Label("键盘事件区域 (点击后按键盘)");
        keyboardBox.Add(keyboardLabel);
        
        // 拖拽事件
        var dragBox = new Box();
        dragBox.AddToClassList("event-example");
        container.Add(dragBox);
        
        dragBox.RegisterCallback<DragEnterEvent>(evt =>
        {
            UpdateStatus("拖拽进入");
        });
        
        dragBox.RegisterCallback<DragLeaveEvent>(evt =>
        {
            UpdateStatus("拖拽离开");
        });
        
        dragBox.RegisterCallback<DragUpdatedEvent>(evt =>
        {
            UpdateStatus("拖拽更新");
        });
        
        var dragLabel = new Label("拖拽事件区域");
        dragBox.Add(dragLabel);
    }
    
    /// <summary>
    /// 创建右侧面板
    /// </summary>
    /// <param name="parent">父元素</param>
    private void CreateRightPanel(VisualElement parent)
    {
        rightPanel = new VisualElement();
        rightPanel.name = "right-panel";
        rightPanel.AddToClassList("right-panel");
        parent.Add(rightPanel);
        
        // 列表视图
        CreateListView();
        
        // 属性面板
        CreatePropertyPanel();
    }
    
    /// <summary>
    /// 创建列表视图
    /// </summary>
    private void CreateListView()
    {
        var listContainer = new VisualElement();
        listContainer.AddToClassList("list-container");
        rightPanel.Add(listContainer);
        
        var listLabel = new Label("列表视图");
        listContainer.Add(listLabel);
        
        // 创建示例数据
        var items = new List<string>();
        for (int i = 1; i <= 20; i++)
        {
            items.Add($"项目 {i}");
        }
        
        // 创建列表视图
        itemListView = new ListView(items, 20, () => new Label(), (element, index) =>
        {
            ((Label)element).text = items[index];
        });
        
        itemListView.selectionType = SelectionType.Multiple;
        itemListView.onSelectionChange += (selectedItems) =>
        {
            UpdateStatus($"选择了 {selectedItems.Count} 个项目");
        };
        
        listContainer.Add(itemListView);
    }
    
    /// <summary>
    /// 创建属性面板
    /// </summary>
    private void CreatePropertyPanel()
    {
        var propertyContainer = new VisualElement();
        propertyContainer.AddToClassList("property-container");
        rightPanel.Add(propertyContainer);
        
        var propertyLabel = new Label("属性面板");
        propertyContainer.Add(propertyLabel);
        
        // 添加一些属性字段
        var intField = new IntegerField("整数字段");
        intField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"整数值: {evt.newValue}");
        });
        propertyContainer.Add(intField);
        
        var floatField = new FloatField("浮点数字段");
        floatField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"浮点数值: {evt.newValue}");
        });
        propertyContainer.Add(floatField);
        
        var stringField = new TextField("字符串字段");
        stringField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"字符串值: {evt.newValue}");
        });
        propertyContainer.Add(stringField);
        
        var boolField = new Toggle("布尔字段");
        boolField.RegisterValueChangedCallback(evt =>
        {
            UpdateStatus($"布尔值: {evt.newValue}");
        });
        propertyContainer.Add(boolField);
    }
    
    /// <summary>
    /// 创建状态栏
    /// </summary>
    private void CreateStatusBar()
    {
        statusBar = new VisualElement();
        statusBar.name = "status-bar";
        statusBar.AddToClassList("status-bar");
        mainContainer.Add(statusBar);
        
        statusLabel = new Label(currentStatus);
        statusBar.Add(statusLabel);
    }
    
    /// <summary>
    /// 初始化标签页
    /// </summary>
    private void InitializeTabs()
    {
        SwitchTab(0);
    }
    
    /// <summary>
    /// 切换标签页
    /// </summary>
    /// <param name="tabIndex">标签页索引</param>
    private void SwitchTab(int tabIndex)
    {
        currentTab = tabIndex;
        
        // 更新按钮状态
        for (int i = 0; i < tabButtons.Length; i++)
        {
            if (i == tabIndex)
            {
                tabButtons[i].AddToClassList("active");
            }
            else
            {
                tabButtons[i].RemoveFromClassList("active");
            }
        }
        
        // 更新内容显示
        var contentArea = leftPanel.Q("content-area");
        var tabContents = contentArea.Query<VisualElement>(className: "tab-content").ToList();
        
        for (int i = 0; i < tabContents.Count; i++)
        {
            if (i == tabIndex)
            {
                tabContents[i].style.display = DisplayStyle.Flex;
            }
            else
            {
                tabContents[i].style.display = DisplayStyle.None;
            }
        }
        
        UpdateStatus($"切换到标签页: {tabIndex + 1}");
    }
    
    /// <summary>
    /// 应用主题
    /// </summary>
    private void ApplyTheme()
    {
        if (enableDarkTheme)
        {
            rootElement.RemoveFromClassList("light-theme");
            rootElement.AddToClassList("dark-theme");
        }
        else
        {
            rootElement.RemoveFromClassList("dark-theme");
            rootElement.AddToClassList("light-theme");
        }
    }
    
    /// <summary>
    /// 更新状态
    /// </summary>
    /// <param name="status">状态信息</param>
    private void UpdateStatus(string status)
    {
        currentStatus = status;
        if (statusLabel != null)
        {
            statusLabel.text = status;
        }
        Debug.Log($"UIElements状态: {status}");
    }
    
    /// <summary>
    /// 刷新UI
    /// </summary>
    private void RefreshUI()
    {
        // 重新创建UI
        rootElement.Clear();
        isInitialized = false;
        InitializeUIElements();
    }
    
    /// <summary>
    /// 获取UI信息
    /// </summary>
    public void GetUIElementsInfo()
    {
        Debug.Log("=== UIElements信息 ===");
        Debug.Log($"窗口标题: {windowTitle}");
        Debug.Log($"窗口大小: {windowSize}");
        Debug.Log($"深色主题: {enableDarkTheme}");
        Debug.Log($"当前标签页: {currentTab}");
        Debug.Log($"当前状态: {currentStatus}");
        Debug.Log($"UI初始化: {isInitialized}");
        
        if (rootElement != null)
        {
            Debug.Log($"根元素子元素数量: {rootElement.childCount}");
        }
    }
    
    private void OnDestroy()
    {
        UpdateStatus("UIElements窗口已关闭");
    }
} 