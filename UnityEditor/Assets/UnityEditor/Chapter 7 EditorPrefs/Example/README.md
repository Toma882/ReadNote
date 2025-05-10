# Example 模块

## 概述
Example 模块提供了Unity编辑器扩展的多种示例实现，展示了不同类型的编辑器扩展功能，包括设置提供器、脚本化向导和场景GUI绘制等功能。这些示例代码展示了如何通过编辑器扩展来增强Unity开发工作流程、自定义编辑器界面和交互方式，为开发者提供了编辑器扩展开发的参考模板。

## 核心功能
- **设置提供器示例**：展示如何在Unity设置窗口中添加自定义设置页面
- **脚本化向导示例**：演示如何创建自定义向导窗口进行操作引导
- **场景GUI示例**：展示如何在场景视图中绘制自定义GUI元素
- **其他编辑器扩展示例**：包含各种编辑器扩展的基础实现

## 重要接口和类

### `ExampleSettingsProvider` 类
展示如何创建自定义设置提供器，在Unity设置窗口中添加自定义页面。

| 方法 | 说明 |
|------|------|
| `CreateProvider()` | 静态方法，创建设置提供器实例，使用SettingsProvider特性注册 |
| `OnGUI()` | 重写方法，定义设置页面的GUI绘制内容 |

### `ExampleScriptableWizard` 类
展示如何创建自定义向导窗口，引导用户完成特定操作流程。

| 方法 | 说明 |
|------|------|
| `Open()` | 静态方法，打开向导窗口，通过MenuItem特性在菜单中注册 |
| `OnWizardCreate()` | 在用户点击确认按钮时调用的回调方法 |
| `OnWizardOtherButton()` | 在用户点击取消(其他)按钮时调用的回调方法 |

### `SceneGUIExample` 类
展示如何在场景视图中绘制自定义GUI元素。

| 方法 | 说明 |
|------|------|
| `Open()` | 静态方法，打开窗口，通过MenuItem特性在菜单中注册 |
| `OnEnable()` | 注册场景GUI事件回调 |
| `OnDisable()` | 取消注册场景GUI事件回调 |
| `OnSceneGUI()` | 场景GUI绘制回调方法，用于在场景视图中绘制自定义内容 |

## UML类图

```
+----------------------+       +------------------------+
| SettingsProvider     |<------| ExampleSettingsProvider|
+----------------------+       +------------------------+
| +OnGUI()             |       | +CreateProvider()      |
| +OnDeactivate()      |       | +OnGUI()               |
| +HasSearchInterest() |       +------------------------+
+----------------------+

+--------------------+       +------------------------+
| ScriptableWizard   |<------| ExampleScriptableWizard|
+--------------------+       +------------------------+
| +OnWizardCreate()  |       | -s: string             |
| +OnWizardUpdate()  |       | +a: string             |
| +OnWizardOtherButton()|     | +Open()                |
+--------------------+       | +OnWizardCreate()      |
                            | +OnWizardOtherButton()  |
                            +------------------------+

+-------------------+       +----------------------+
| EditorWindow      |<------| SceneGUIExample     |
+-------------------+       +----------------------+
| +Show()           |       | +Open()              |
| +OnGUI()          |       | +OnEnable()          |
| +OnEnable()       |       | +OnDisable()         |
| +OnDisable()      |       | +OnSceneGUI()        |
+-------------------+       +----------------------+
```

## 编辑器扩展功能流程图

```
SettingsProvider:
用户打开设置窗口 --> 注册的SettingsProvider被初始化 --> OnGUI()绘制界面 --> 用户交互

ScriptableWizard:
调用Open()方法 --> DisplayWizard<>创建向导实例 --> 显示向导窗口 --> 用户点击按钮 --> 触发OnWizardCreate()或OnWizardOtherButton()

SceneGUI:
打开窗口实例 --> OnEnable()注册SceneView.duringSceneGui事件 --> 激活场景窗口 --> OnSceneGUI()绘制场景GUI --> 关闭窗口 --> OnDisable()取消注册事件
```

## 思维导图

```
Unity编辑器扩展示例
├── 设置提供器 (ExampleSettingsProvider)
│   ├── 设置范围
│   │   ├── 项目级设置
│   │   └── 用户级设置
│   ├── 设置页面位置
│   │   ├── 主设置窗口
│   │   └── 自定义项目分组
│   └── 功能特性
│       ├── 设置页面绘制
│       ├── 搜索关键字支持
│       └── 设置持久化
├── 脚本化向导 (ExampleScriptableWizard)
│   ├── 向导窗口创建
│   ├── 字段自动暴露
│   ├── 按钮事件
│   │   ├── 确认按钮 (OnWizardCreate)
│   │   └── 其他按钮 (OnWizardOtherButton)
│   └── 向导状态更新 (OnWizardUpdate)
└── 场景GUI (SceneGUIExample)
    ├── 事件注册与取消
    ├── 绘制方式
    │   ├── Handles类绘制
    │   ├── GUI类绘制
    │   └── Gizmos绘制
    └── 交互处理
        ├── 拾取与选择
        ├── 拖拽操作
        └── 自定义控件
```

## 应用场景
1. **工作流程自动化**：通过自定义编辑器扩展，自动化重复性工作
2. **可视化工具开发**：创建专用的可视化编辑工具，简化复杂操作
3. **设置界面定制**：为项目添加自定义设置页面，集中管理项目配置
4. **操作引导与向导**：为复杂操作提供向导式界面，引导用户完成步骤
5. **场景编辑增强**：在场景视图中添加自定义控件和辅助信息

## 最佳实践
1. **模块化设计**：保持编辑器扩展功能的模块化，便于维护和扩展
2. **一致的用户体验**：遵循Unity编辑器的设计语言和交互模式
3. **性能优化**：避免在频繁调用的方法中执行耗时操作
4. **错误处理与提示**：添加适当的错误处理和用户反馈
5. **撤销支持**：为编辑操作添加撤销支持，提高用户体验
6. **资源释放**：正确管理事件注册和资源释放，避免内存泄漏

## 代码示例
```csharp
// 创建自定义编辑器窗口示例
public class CustomEditorWindowExample : EditorWindow
{
    private string inputText = "";
    private bool toggleValue = false;
    
    [MenuItem("Examples/Custom Editor Window")]
    public static void ShowWindow()
    {
        GetWindow<CustomEditorWindowExample>("Custom Window").Show();
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Custom Editor Window Example", EditorStyles.boldLabel);
        
        EditorGUI.BeginChangeCheck();
        inputText = EditorGUILayout.TextField("Input:", inputText);
        toggleValue = EditorGUILayout.Toggle("Option:", toggleValue);
        
        if (EditorGUI.EndChangeCheck())
        {
            // 处理数据变化
            Debug.Log("Values changed");
        }
        
        if (GUILayout.Button("Apply"))
        {
            // 执行操作
            Debug.Log("Applied: " + inputText + ", " + toggleValue);
        }
    }
}
```

## 相关资源
- [Unity文档: 编辑器扩展](https://docs.unity3d.com/Manual/ExtendingTheEditor.html)
- [Unity文档: SettingsProvider](https://docs.unity3d.com/ScriptReference/SettingsProvider.html)
- [Unity文档: ScriptableWizard](https://docs.unity3d.com/ScriptReference/ScriptableWizard.html)
- [Unity文档: EditorWindow](https://docs.unity3d.com/ScriptReference/EditorWindow.html)
- [Unity文档: Handles](https://docs.unity3d.com/ScriptReference/Handles.html)
