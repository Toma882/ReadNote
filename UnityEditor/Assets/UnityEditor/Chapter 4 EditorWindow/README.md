# Unity编辑器扩展 - Chapter 4: EditorWindow

## 概述

本章专注于Unity编辑器窗口(EditorWindow)的开发，展示了如何创建自定义编辑器窗口来扩展Unity编辑器功能。通过实现不同类型的编辑器窗口，可以为Unity开发工作流提供定制化工具，提高开发效率。

## 核心特征

### EditorWindow的基本特征

| 特征 | 描述 |
|------|------|
| **自定义布局** | EditorWindow允许完全自定义UI布局，通过OnGUI方法实现界面绘制 |
| **可停靠** | 可以被停靠到Unity编辑器的任何位置，与内置窗口集成 |
| **持久化** | 窗口状态可以在编辑器会话之间保持，使用EditorPrefs存储设置 |
| **交互式** | 支持完整的鼠标和键盘交互，可以处理各种输入事件 |
| **可重用** | 可以创建可重用的窗口组件，便于维护和扩展 |
| **可编程** | 提供完整的API用于控制窗口外观和行为 |

### EditorWindow的生命周期

| 方法 | 调用时机 | 用途 |
|------|---------|------|
| **Open** | 窗口首次打开时 | 初始化窗口状态和布局 |
| **OnEnable** | 窗口首次打开或重新启用时 | 初始化资源和状态 |
| **OnDisable** | 窗口关闭或禁用时 | 清理资源 |
| **OnGUI** | 每一帧或需要重绘时 | 绘制窗口界面 |
| **OnFocus** | 窗口获得焦点时 | 处理窗口获得焦点事件 |
| **OnLostFocus** | 窗口失去焦点时 | 处理窗口失去焦点事件 |
| **Update** | 每一帧 | 执行持续性更新逻辑 |
| **OnDestroy** | 窗口被销毁时 | 释放资源和保存状态 |
| **OnInspectorUpdate** | 检查器更新时 | 更新窗口内容 |
| **OnProjectChange** | 项目更改时 | 响应项目更改事件 |
| **OnHierarchyChange** | 层级变化时 | 响应层级变化事件 |
| **OnSelectionChange** | 选择变化时 | 响应选择变化事件 |
| **OnRepaint** | 窗口重绘时 | 处理窗口重绘事件 |
| **OnLayout** | 窗口布局变化时 | 处理窗口布局更新 |
| **OnValidate** | 资源验证时 | 验证窗口中的数据 |
|

### EditorWindow的重要属性

| 属性 | 类型 | 用途 |
|------|------|------|
| **position** | Rect | 设置或获取窗口位置和大小 |
| **titleContent** | GUIContent | 设置窗口标题和图标 |
| **minSize** | Vector2 | 设置窗口最小尺寸 |
| **maxSize** | Vector2 | 设置窗口最大尺寸 |
| **wantsMouseMove** | bool | 是否接收鼠标移动事件 |
| **autoRepaintOnSceneChange** | bool | 场景变化时是否自动重绘 |

### GenericMenu

GenericMenu是Unity中用于创建上下文菜单的类。它允许开发者在编辑器中快速构建和显示菜单，提供用户交互的便利。以下是GenericMenu的一些核心特性和用法：

#### 核心特性

- **动态菜单项**：可以根据需要动态添加、修改或删除菜单项。
- **支持子菜单**：可以创建嵌套的子菜单，组织复杂的菜单结构。
- **回调函数**：每个菜单项可以绑定一个回调函数，当用户选择该项时执行特定操作。
- **分隔符**：可以在菜单中添加分隔符，以提高可读性和组织性。


### GUIContent

GUIContent是Unity中用于描述用户界面元素的内容的类。它包含了文本、图像和tooltip信息，用于在编辑器和游戏中显示用户界面元素。以下是GUIContent的一些核心特性和用法：

#### 核心特性

- **文本**：可以设置文本内容，用于显示在用户界面元素上。
- **图像**：可以设置图像，用于显示在用户界面元素上。
- **tooltip**：可以设置tooltip信息，用于在用户界面元素上显示提示信息。

#### 用法

- 在EditorWindow中，GUIContent通常用于设置窗口的标题和图标。
- 在游戏中，GUIContent用于设置用户界面元素的文本和图像。
- 通过设置tooltip信息，可以为用户界面元素提供额外的信息和帮助。

``` c#
    GenericMenu gm = new GenericMenu();
        //添加菜单项
        gm.AddItem(new GUIContent("Memu1"), true, () => Debug.Log("Select Menu1"));
        //添加分隔符 参数传空字符串表示在一级菜单中添加分隔符
        gm.AddSeparator(string.Empty);
        //添加不可交互菜单项
        gm.AddDisabledItem(new GUIContent("Memu2"));
        //通过'/'可添加子菜单项
        gm.AddItem(new GUIContent("Menu3/SubMenu1"), false,
            () => Debug.Log("Select SubMenu1"));
        //在子菜单中添加分隔符
        gm.AddSeparator("Menu3/");
        gm.AddItem(new GUIContent("Menu3/SubMenu2"), false,
            () => Debug.Log("Select SubMenu2"));
        //显示菜单
        gm.ShowAsContext();
```
### InitializeOnLoadMethod

`InitializeOnLoadMethod` 是一个用于在Unity编辑器加载时自动执行特定方法的特性。它允许开发者在编辑器启动或脚本重新编译时执行初始化代码，而无需手动调用。这对于设置全局状态、注册事件或执行一次性配置非常有用。

#### 使用说明

1. **定义方法**：创建一个静态方法，并在方法上方添加 `[InitializeOnLoadMethod]` 特性。
2. **自动执行**：当Unity编辑器加载时，该方法会自动被调用。


### GUI.enabled 
是一个用于控制GUI元素是否可点击的属性。它可以用于控制按钮、文本框、复选框等元素的可用性。

#### 核心特性

- **启用/禁用控件**：通过设置`GUI.enabled`为`true`或`false`，可以控制控件的交互状态。
- **嵌套作用域**：`GUI.enabled`的设置会影响其后的所有控件，直到显式恢复为`true`。
- **视觉反馈**：被禁用的控件通常会显示为灰色，提示用户不可用。

#### 使用说明

```c#
GUI.enabled = false; // 禁用控件
GUILayout.Button("不可点击的按钮");

GUI.enabled = true; // 启用控件
GUILayout.Button("可点击的按钮");
```

#### 注意事项

- 在复杂的UI布局中，确保在需要的范围内正确恢复`GUI.enabled`的值，以避免意外禁用其他控件。
- 如果需要临时禁用一组控件，可以使用嵌套的`GUI.enabled`设置。

#### 常见应用场景

1. **条件性禁用**：
```c#
GUI.enabled = PlayerPrefs.GetInt("IsUnlocked") > 0;
GUILayout.Button("高级功能");
GUI.enabled = true;
```

2. **批量控制**：
```c#
GUI.enabled = !isProcessing;
GUILayout.Button("开始");
GUILayout.Button("暂停");
GUILayout.Button("停止");
GUI.enabled = true;
```

3. **嵌套使用**：
```c#
bool oldEnabled = GUI.enabled;
GUI.enabled = someCondition;
// 绘制一组控件
GUI.enabled = oldEnabled; // 恢复原始状态
```

## UML类图

```
+-----------------------------+        +-------------------------------+
|        MonoBehaviour        |        |          EditorWindow         |
+-----------------------------+        +-------------------------------+
| + enabled                   |        | + position                    |
| + gameObject                |        | + titleContent                |
| + transform                 |        | + minSize/maxSize             |
+-----------------------------+        | + wantsMouseMove              |
| + Start()                   |        +-------------------------------+
| + OnEnable()                |        | + OnGUI()                     |
| + OnDisable()               |        | + OnEnable()/OnDisable()      |
| + OnGUI()                   |        | + Repaint()                   |
| + Update()                  |        | + GetWindow<T>()              |
+-------------^---------------+        | + Show()/Close()              |
              |                        | + ShowUtility()/ShowPopup()   |
              |                        +---------------^---------------+
              |                                        |
+-----------------------------+                        |
|     ConsoleGUIWindow        |        +-------------------------------+
+-----------------------------+        |       DevelopmentMemo         |
| - workingType               |        +-------------------------------+
| - logs: List<ConsoleItem>   |        | - memos: MemoGroup[]          |
| - expandRect/retractRect    |        | - currentGroup                |
| - isExpand                  |        | - searchText                  |
+-----------------------------+        +-------------------------------+
| + Start()                   |        | + OnGUI()                     |
| + OnGUI()                   |        | + CreateAsset<T>()            |
| + OnLogMessageReceived()    |        | + DrawMemoList()              |
| - OnExpandGUI()             |        | + DrawMemoDetail()            |
| - OnRetractGUI()            |        | + SaveMemos()                 |
+-----------------------------+        +-------------------------------+

+-----------------------------+
|     HierarchyGUIWindow      |
+-----------------------------+
| - expandRect/retractRect    |
| - isExpand                  |
| - list: List<HGWItem>       |
| + currentSelected           |
+-----------------------------+
| + OnEnable()/OnDisable()    |
| + OnGUI()                   |
| - CollectRoots()            |
| - CollectChildrens()        |
| - OnExpandGUI()             |
| - OnRetractGUI()            |
+-------------^---------------+
              |
+-----------------------------+        +-------------------------------+
|     InspectorGUIWindow      |        |   HierarchyGUIWindowItem     |
+-----------------------------+        +-------------------------------+
| - hierarchyGUIWindow        |        | - transform                   |
| - expandRect/retractRect    |        | - window                      |
| - selected                  |        | - childrens: List<HGWItem>    |
| - components: Component[]   |        | - expand                      |
| - currentComponent          |        | - level                       |
| - inspectorDic              |        +-------------------------------+
+-----------------------------+        | + HierarchyGUIWindowItem()    |
| + OnEnable()/OnDisable()    |        | + Draw()                      |
| + OnGUI()                   |        | - GetParent()                 |
| - OnExpandGUI()             |        +-------------------------------+
| - OnRetractGUI()            |
| - OnListGUI()               |
| - OnComponentInspector()    |
+-----------------------------+

+-------------------------------+
|    IComponentGUIInspector     |
+-------------------------------+
| + Draw(Component)             |
+---------------^---------------+
                |
+-------------------------------+        +-------------------------------+
|    ComponentGUIInspector      |        | ComponentGUIInspectorAttribute|
+-------------------------------+        +-------------------------------+
| # valueStr/newValueStr        |        | + ComponentType               |
| # floatValue/boolValue        |        +-------------------------------+
+-------------------------------+        | + ComponentGUIInspectorAttribute(Type) |
| + Draw(Component)             |        +-------------------------------+
| # OnDraw(Component)           |
| # DrawText()                  |
| # DrawToggle()                |
| # DrawInt()/DrawFloat()       |
| # DrawColor()                 |
| # DrawVector2()/DrawVector3() |
| # DrawHorizontalSlider()      |
+---------------^---------------+
                |
       +-----------------+-----------------+
       |                 |                 |
+------v------+  +-------v-----+  +-------v-----+
| TransformGUI|  |  CameraGUI  |  |  Other GUI  |
| Inspector   |  |  Inspector  |  |  Inspectors |
+-------------+  +-------------+  +-------------+
```

## 思维导图

```
Unity EditorWindow系统
├── 基础概念
│   ├── EditorWindow类
│   │   ├── 作为独立窗口的编辑器界面
│   │   ├── 可以停靠在Unity编辑器的任何位置
│   │   └── 通过OnGUI方法实现自定义界面
│   │
│   ├── 生命周期
│   │   ├── OnEnable - 窗口启用时调用
│   │   ├── OnDisable - 窗口禁用时调用
│   │   ├── OnGUI - 窗口界面绘制(每帧调用)
│   │   ├── OnFocus - 窗口获得焦点时调用
│   │   ├── OnLostFocus - 窗口失去焦点时调用
│   │   ├── OnDestroy - 窗口销毁时调用
│   │   ├── OnHierarchyChange - 层级变化时调用
│   │   ├── OnInspectorUpdate - 检视器更新时调用
│   │   ├── OnProjectChange - 项目变化时调用
│   │   └── OnSelectionChange - 选择变化时调用
│   │
│   ├── 窗口管理
│   │   ├── GetWindow<T>() - 获取窗口实例
│   │   ├── Show() - 显示窗口
│   │   ├── Close() - 关闭窗口
│   │   ├── ShowUtility() - 显示工具窗口
│   │   └── ShowPopup() - 显示弹出窗口
│   │
│   ├── EditorApplication类
│   │   ├── 编辑器状态管理
│   │   │   ├── isPlaying - 是否处于播放模式
│   │   │   ├── isPaused - 是否处于暂停状态
│   │   │   └── isCompiling - 是否正在编译
│   │   ├── 事件系统
│   │   │   ├── update - 每帧更新事件
│   │   │   ├── hierarchyChanged - 层级变化事件
│   │   │   ├── projectChanged - 项目变化事件
│   │   │   ├── playModeStateChanged - 播放模式变化事件
│   │   │   ├── hierarchyWindowItemOnGUI - 层级面板项绘制事件
│   │   │   └── projectWindowItemOnGUI - 项目面板项绘制事件
│   │   └── 操作方法
│   │       ├── ExecuteMenuItem - 执行菜单项
│   │       ├── Beep - 播放提示音
│   │       └── OpenProject - 打开项目
│   │
│   └── EditorUtility类
│       ├── 对话框工具
│       │   ├── DisplayDialog - 显示对话框
│       │   └── DisplayProgressBar - 显示进度条
│   │
│   ├── 窗口类型示例
│   │   ├── 游戏运行时辅助窗口
│   │   │   ├── ConsoleGUIWindow - 控制台窗口
│   │   │   │   ├── 日志显示和过滤
│   │   │   │   ├── 性能监控(FPS)
│   │   │   │   ├── 错误/警告/信息分类
│   │   │   │   └── 可展开/收起的界面
│   │   │   ├── HierarchyGUIWindow - 层级窗口
│   │   │   │   ├── 场景对象树状展示
│   │   │   │   ├── 对象选择功能
│   │   │   │   ├── 树形结构的递归绘制
│   │   │   │   └── 可展开/收起的界面
│   │   │   └── InspectorGUIWindow - 检视器窗口
│   │   │       ├── 组件属性编辑
│   │   │       ├── 基于反射的动态检视器
│   │   │       ├── 自定义属性编辑界面
│   │   │       └── 与HierarchyGUIWindow联动
│   │   │
│   │   ├── 编辑器工具窗口
│   │   │   ├── DevelopmentMemo - 开发备忘录
│   │   │   │   ├── 团队协作笔记工具
│   │   │   │   ├── 基于ScriptableObject的数据存储
│   │   │   │   ├── 分类和搜索功能
│   │   │   │   └── 富文本编辑
│   │   │   ├── ProtoEditor - 原型编辑器
│   │   │   │   ├── 快速游戏原型设计
│   │   │   │   ├── 预制体组装工具
│   │   │   │   └── 参数调整界面
│   │   │   └── AvatarController - 角色控制器编辑器
│   │   │       ├── 角色动画参数配置
│   │   │       ├── 动画状态机可视化
│   │   │       └── 角色控制器预设管理
│   │   │
│   │   └── 向导类窗口
│   │       └── AvatarCreateScriptableWizard - 角色创建向导
│   │           ├── 步骤化创建流程
│   │           ├── 参数验证和错误提示
│   │           └── 资源生成和配置
│   │
│   ├── 组件架构设计
│   │   ├── IComponentGUIInspector接口
│   │   │   └── Draw(Component) - 组件绘制方法
│   │   ├── ComponentGUIInspector基类
│   │   │   ├── 通用UI控件绘制方法
│   │   │   │   ├── DrawText() - 文本绘制
│   │   │   │   ├── DrawToggle() - 开关绘制
│   │   │   │   ├── DrawInt()/DrawFloat() - 数值绘制
│   │   │   │   ├── DrawColor() - 颜色绘制
│   │   │   │   ├── DrawVector2()/DrawVector3() - 向量绘制
│   │   │   │   └── DrawHorizontalSlider() - 滑动条绘制
│   │   │   └── 模板方法模式
│   │   │   │   └── OnDraw(Component) - 子类实现的绘制方法
│   │   │   ├── ComponentGUIInspectorAttribute特性
│   │   │   │   └── 标记检视器类对应的组件类型
│   │   │   └── 具体实现类
│   │   │       ├── TransformGUIInspector - Transform组件检视器
│   │   │       ├── CameraGUIInspector - Camera组件检视器
│   │   │       └── 其他组件检视器...
│   │   │
│   │   ├── 实现技术
│   │   │   ├── GUI/GUILayout系统
│   │   │   │   ├── 自动布局vs手动布局
│   │   │   │   ├── 控件类型和使用方式
│   │   │   │   └── 布局组织(水平/垂直/区域)
│   │   │   ├── 窗口状态管理
│   │   │   │   ├── 展开/收起状态切换
│   │   │   │   ├── 位置和大小控制
│   │   │   │   └── 拖拽功能实现
│   │   │   ├── 反射系统应用
│   │   │   │   ├── 动态发现和加载检视器类
│   │   │   │   ├── 特性(Attribute)的使用
│   │   │   │   └── 类型关联和实例创建
│   │   │   └── 场景对象交互
│   │   │       ├── 获取场景根对象
│   │   │       ├── 遍历对象层级结构
│   │   │       └── 组件访问和修改
│   │   │
│   │   └── 最佳实践
│   │       ├── 性能优化
│   │       │   ├── 避免OnGUI中的频繁计算
│   │       │   ├── 使用缓存减少GC压力
│   │       │   └── 合理使用布局和控件
│   │       ├── 用户体验
│   │       │   ├── 清晰的视觉层次和布局
│   │       │   ├── 响应式的交互设计
│   │       │   └── 状态保持和恢复
│   │       ├── 模块化设计
│   │       │   ├── 单一职责原则
│   │       │   ├── 接口设计和实现分离
│   │       │   └── 可扩展的架构
│   │       └── 调试和测试
│   │           ├── 错误处理和日志
│   │           ├── 边界情况测试
│   │           └── 性能分析
│   └── 重要的类和接口
│       ├── EditorWindow相关类和接口
│       │   ├── 类/接口名称 | 类型 | 描述 | 重要方法/属性
│       │   │   ├── `EditorWindow` | 基类 | Unity编辑器窗口基类 | `OnGUI()`, `OnEnable()`, `OnDisable()`, `ShowUtility()`, `position`
│       │   │   ├── `ConsoleGUIWindow` | 窗口类 | 运行时控制台窗口 | `OnLogMessageReceived()`, `workingType`, `logs`
│       │   │   ├── `HierarchyGUIWindow` | 窗口类 | 场景层级窗口 | `CollectRoots()`, `currentSelected`
│       │   │   ├── `HierarchyGUIWindowItem` | 数据类 | 层级窗口的树节点 | `Draw()`, `childrens`
│       │   │   └── `InspectorGUIWindow` | 窗口类 | 对象检视器窗口 | `OnComponentInspector()`, `inspectorDic`
│       │   │   └── `DevelopmentMemo` | 窗口类 | 开发备忘录窗口 | `SaveMemos()`, `DrawMemoList()`
│       │   └── 组件检视器相关类和接口
│       │       ├── `IComponentGUIInspector` | 接口 | 组件检视器接口 | `Draw(Component)`
│       │       ├── `ComponentGUIInspector` | 抽象类 | 组件检视器基类 | `OnDraw(Component)`, `DrawText()`, `DrawVector3()`
│       │       ├── `ComponentGUIInspectorAttribute` | 特性 | 组件类型标记特性 | `ComponentType`
│       │       └── `TransformGUIInspector` | 实现类 | Transform组件检视器 | `OnDraw(Component)`
│       │       └── `CameraGUIInspector` | 实现类 | Camera组件检视器 | `OnDraw(Component)`
│       │       └── `ConsoleItem` | 数据类 | 控制台日志项 | `type`, `message`, `stackTrace`
│       └── 其他辅助类和枚举
│           ├── `WorkingType` | 枚举 | 控制台窗口工作模式 | `ALWAYS_OPEN`, `ONLY_OPEN_IN_EDITOR`, `ALWAYS_CLOSE`
│           └── `EditorApplication` | 类 | 编辑器状态管理 | 
│               ├── 事件系统
│               │   ├── update - 每帧更新事件
│               │   └── hierarchyChanged - 层级变化事件
│               └── 操作方法
│                   ├── ExecuteMenuItem - 执行菜单项
│                   └── Beep - 播放提示音
│           └── `EditorUtility` | 类 | 对话框工具 | 
│               ├── 对话框工具
│               │   └── DisplayDialog - 显示对话框
│               └── 其他工具
│                   ├── CreateFolder - 创建文件夹
│                   └── RevealInFinder - 在文件浏览器中显示
├── 窗口类型示例
│   ├── 游戏运行时辅助窗口
│   │   ├── ConsoleGUIWindow - 控制台窗口
│   │   │   ├── 日志显示和过滤
│   │   │   ├── 性能监控(FPS)
│   │   │   ├── 错误/警告/信息分类
│   │   │   └── 可展开/收起的界面
│   │   ├── HierarchyGUIWindow - 层级窗口
│   │   │   ├── 场景对象树状展示
│   │   │   ├── 对象选择功能
│   │   │   ├── 树形结构的递归绘制
│   │   │   └── 可展开/收起的界面
│   │   └── InspectorGUIWindow - 检视器窗口
│   │       ├── 组件属性编辑
│   │       ├── 基于反射的动态检视器
│   │       ├── 自定义属性编辑界面
│   │       └── 与HierarchyGUIWindow联动
│   │
│   ├── 编辑器工具窗口
│   │   ├── DevelopmentMemo - 开发备忘录
│   │   │   ├── 团队协作笔记工具
│   │   │   ├── 基于ScriptableObject的数据存储
│   │   │   ├── 分类和搜索功能
│   │   │   └── 富文本编辑
│   │   ├── ProtoEditor - 原型编辑器
│   │   │   ├── 快速游戏原型设计
│   │   │   ├── 预制体组装工具
│   │   │   └── 参数调整界面
│   │   └── AvatarController - 角色控制器编辑器
│   │       ├── 角色动画参数配置
│   │       ├── 动画状态机可视化
│   │       └── 角色控制器预设管理
│   │
│   └── 向导类窗口
│       └── AvatarCreateScriptableWizard - 角色创建向导
│           ├── 步骤化创建流程
│           ├── 参数验证和错误提示
│           └── 资源生成和配置
│
├── 组件架构设计
│   ├── IComponentGUIInspector接口
│   │   └── Draw(Component) - 组件绘制方法
│   ├── ComponentGUIInspector基类
│   │   ├── 通用UI控件绘制方法
│   │   │   ├── DrawText() - 文本绘制
│   │   │   ├── DrawToggle() - 开关绘制
│   │   │   ├── DrawInt()/DrawFloat() - 数值绘制
│   │   │   ├── DrawColor() - 颜色绘制
│   │   │   ├── DrawVector2()/DrawVector3() - 向量绘制
│   │   │   └── DrawHorizontalSlider() - 滑动条绘制
│   │   └── 模板方法模式
│   │       └── OnDraw(Component) - 子类实现的绘制方法
│   ├── ComponentGUIInspectorAttribute特性
│   │   └── 标记检视器类对应的组件类型
│   └── 具体实现类
│       ├── TransformGUIInspector - Transform组件检视器
│       ├── CameraGUIInspector - Camera组件检视器
│       └── 其他组件检视器...
│

```

## 重要的类和接口

### EditorWindow相关类和接口

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|-----------|-----|------|--------------|
| `EditorWindow` | 基类 | Unity编辑器窗口基类 | `OnGUI()`, `OnEnable()`, `OnDisable()`, `ShowUtility()`, `position` |
| `ConsoleGUIWindow` | 窗口类 | 运行时控制台窗口 | `OnLogMessageReceived()`, `workingType`, `logs` |
| `HierarchyGUIWindow` | 窗口类 | 场景层级窗口 | `CollectRoots()`, `currentSelected` |
| `HierarchyGUIWindowItem` | 数据类 | 层级窗口的树节点 | `Draw()`, `childrens` |
| `InspectorGUIWindow` | 窗口类 | 对象检视器窗口 | `OnComponentInspector()`, `inspectorDic` |
| `DevelopmentMemo` | 窗口类 | 开发备忘录窗口 | `SaveMemos()`, `DrawMemoList()` |

### 组件检视器相关类和接口

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|-----------|-----|------|--------------|
| `IComponentGUIInspector` | 接口 | 组件检视器接口 | `Draw(Component)` |
| `ComponentGUIInspector` | 抽象类 | 组件检视器基类 | `OnDraw(Component)`, `DrawText()`, `DrawVector3()` |
| `ComponentGUIInspectorAttribute` | 特性 | 组件类型标记特性 | `ComponentType` |
| `TransformGUIInspector` | 实现类 | Transform组件检视器 | `OnDraw(Component)` |
| `CameraGUIInspector` | 实现类 | Camera组件检视器 | `OnDraw(Component)` |
| `ConsoleItem` | 数据类 | 控制台日志项 | `type`, `message`, `stackTrace` |

### 其他辅助类和枚举

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|-----------|-----|------|--------------|
| `WorkingType` | 枚举 | 控制台窗口工作模式 | `ALWAYS_OPEN`, `ONLY_OPEN_IN_EDITOR`, `ALWAYS_CLOSE` |

## 实现细节与设计模式

1. **模板方法模式**
   - `ComponentGUIInspector`类使用模板方法模式，通过基类定义通用算法框架，子类提供具体实现
   - 基类提供通用的控件绘制方法，子类只需实现`OnDraw`方法

2. **反射与特性的应用**
   - 使用反射动态发现和加载实现了`IComponentGUIInspector`接口的类
   - 使用`ComponentGUIInspectorAttribute`特性标记组件检视器类对应的组件类型

3. **组合模式**
   - `HierarchyGUIWindow`和`HierarchyGUIWindowItem`使用组合模式构建树形结构
   - 每个`HierarchyGUIWindowItem`包含其子项列表，形成递归结构

4. **观察者模式**
   - `ConsoleGUIWindow`通过`Application.logMessageReceived`事件订阅Unity日志消息
   - 实现了自定义日志记录和显示功能

5. **单一职责原则**
   - 每个窗口类只负责特定功能：层级显示、对象检视、日志记录等
   - 组件检视器分离为独立的类，每个类只负责特定组件类型的绘制

## 案例应用

### 1. 运行时调试工具套件
该套件结合`ConsoleGUIWindow`、`HierarchyGUIWindow`和`InspectorGUIWindow`，提供了类似Unity编辑器的运行时调试功能。

主要功能：
- 实时日志显示和过滤
- 场景对象层级浏览和选择
- 对象组件属性查看和修改
- 性能监控(FPS)

### 2. 开发备忘录(DevelopmentMemo)
团队协作和开发笔记工具，用于记录和共享开发信息。

主要功能：
- 基于分组的笔记组织
- 搜索和筛选功能
- 富文本编辑支持
- 基于ScriptableObject的数据持久化

## 相关官方文档

1. [Unity EditorWindow 类参考](https://docs.unity3d.com/ScriptReference/EditorWindow.html)
2. [Unity 编辑器扩展入门](https://docs.unity3d.com/Manual/ExtendingTheEditor.html)
3. [Unity GUILayout 类参考](https://docs.unity3d.com/ScriptReference/GUILayout.html)
4. [Unity Editor 脚本示例](https://docs.unity3d.com/Manual/editor-EditorScripting.html)
5. [Unity ScriptableObject 类参考](https://docs.unity3d.com/ScriptReference/ScriptableObject.html)
6. [Unity Log 系统参考](https://docs.unity3d.com/ScriptReference/Application-logMessageReceived.html)
7. [Unity 事件处理参考](https://docs.unity3d.com/ScriptReference/Event.html)

## 最佳实践

1. **性能优化**
   - 避免在`OnGUI`中执行耗时操作或分配大量内存
   - 使用缓存减少GC压力
   - 仅在必要时调用`Repaint`方法

2. **用户体验**
   - 保持窗口界面简洁清晰
   - 提供足够的交互反馈
   - 考虑窗口状态的保存和恢复

3. **代码组织**
   - 将UI绘制和业务逻辑分离
   - 使用接口和抽象类提高代码复用性
   - 采用模块化设计便于扩展

4. **调试功能**
   - 添加适当的日志和错误处理
   - 考虑边界情况和异常情况
   - 提供清晰的错误提示



