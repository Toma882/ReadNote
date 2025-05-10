# Unity编辑器扩展开发指南

## 核心特征

### 扩展类型概述

| 扩展类型 | 主要功能 | 核心类/接口 |
|---------|---------|------------|
| **自定义Inspector** | 为组件提供自定义界面 | `Editor`、`CustomEditor` |
| **编辑器窗口** | 创建独立的编辑器工具窗口 | `EditorWindow` |
| **自定义菜单** | 在Unity菜单栏添加功能入口 | `MenuItem` |
| **属性绘制器** | 为特定数据类型创建自定义UI | `PropertyDrawer`、`CustomPropertyDrawer` |
| **场景视图工具** | 在Scene视图中绘制辅助线、控制手柄 | `Gizmos`、`Handles` |
| **编辑器偏好设置** | 保存和读取编辑器配置数据 | `EditorPrefs` |
| **界面样式** | 定制编辑器界面外观 | `GUISkin`、`EditorStyles` |
| **资产处理器** | 自动处理资产导入和修改 | `AssetPostprocessor`、`AssetModificationProcessor` |
| **构建工具** | 自定义Unity构建流程 | `BuildPipeline`、`IPreprocessBuild`、`IPostprocessBuild` |
| **场景管理器** | 场景操作和管理 | `SceneManager`、`EditorSceneManager` |

### 核心接口与特性

| 特性/接口 | 描述 | 应用场景 | 重要参数/属性 |
|----------|------|----------|--------------|
| `[CustomEditor(typeof(T))]` | 指定自定义编辑器类适用的组件类型 | 创建自定义Inspector | `canEditMultipleObjects`: 是否支持多对象编辑<br>`isFallback`: 是否作为后备编辑器 |
| `[MenuItem("路径/项目名")]` | 在菜单栏添加菜单项 | 创建自定义菜单项 | `priority`: 菜单项优先级<br>`validate`: 验证菜单项是否可用的回调 |
| `[CustomPropertyDrawer(typeof(T))]` | 为特定类型创建属性绘制器 | 自定义序列化数据显示 | `useForChildren`: 是否应用于子类 |
| `[InitializeOnLoad]` | 编辑器启动时自动初始化类 | 创建编辑器启动时的初始化逻辑 | 需配合静态构造函数使用 |
| `[InitializeOnLoadMethod]` | 编辑器启动时自动调用标记的方法 | 创建编辑器启动时的初始化方法 | 必须是静态方法 |
| `OnInspectorGUI()` | 绘制Inspector界面的方法 | 自定义Inspector内容 | 所在类必须继承`Editor` |
| `OnGUI()` | 绘制编辑器窗口内容的方法 | 自定义EditorWindow内容 | 每帧调用，需优化性能 |
| `OnSceneGUI()` | 在场景视图中绘制的方法 | 创建场景交互工具 | 所在类必须继承`Editor` |
| `[CanEditMultipleObjects]` | 标记编辑器支持多对象编辑 | 允许同时编辑多个相同类型的对象 | 通常与`CustomEditor`一起使用 |
| `[ExecuteInEditMode]` | 组件在编辑模式下也会执行 | 创建在编辑器中实时更新的组件 | 可能影响编辑器性能 |
| `[CreateAssetMenu]` | 定义可创建的自定义资产类型 | 创建自定义ScriptableObject资产 | `fileName`、`menuName`、`order` |
| `IHasCustomMenu` | 为编辑器窗口添加自定义上下文菜单 | 扩展编辑器窗口功能 | 实现`AddItemsToMenu`方法 |
| `IPreprocessBuild` | 构建前处理接口 | 在构建之前执行自定义操作 | 实现`OnPreprocessBuild`方法 |
| `IPostprocessBuild` | 构建后处理接口 | 在构建之后执行自定义操作 | 实现`OnPostprocessBuild`方法 |
| `OnEnable()` | 编辑器类初始化时调用 | 初始化编辑器状态和资源 | 所有编辑器类的生命周期方法 |
| `OnDisable()` | 编辑器类禁用时调用 | 清理编辑器资源 | 所有编辑器类的生命周期方法 |
| `[PreferenceItem]` | 在Unity首选项窗口添加项 | 创建自定义设置面板 | 标记静态方法 |
| `[HideInInspector]` | 在Inspector中隐藏字段 | 隐藏不需要在UI中显示的变量 | 字段仍会被序列化 |
| `[SerializeField]` | 序列化私有或受保护的字段 | 让私有字段在Inspector中可见 | 常与自定义编辑器配合使用 |
| `AssetDatabase` | 资产数据库访问类 | 管理项目资产 | 提供资产创建、导入、刷新等方法 |
| `[DrawGizmo]` | 定义何时和对哪些对象绘制Gizmo | 为特定类型对象添加场景视图辅助图形 | `GizmoType`参数控制显示条件 |

## UML类图

```
+-------------------------+        +-------------------------+
|      EditorWindow       |        |         Editor         |
+-------------------------+        +-------------------------+
| + titleContent          |        | + target               |
| + position              |        | + targets              |
| + wantsMouseMove        |        | + serializedObject     |
+-------------------------+        +-------------------------+
| + OnGUI()               |        | + OnInspectorGUI()     |
| + Repaint()             |        | + OnSceneGUI()         |
| + ShowUtility()         |        | + OnEnable()           |
| + ShowPopup()           |        | + OnDisable()          |
| + ShowModal()           |        | + CreateEditor()       |
+-------------------------+        +-------------------------+
          ^                                   ^
          |                                   |
+-------------------------+        +-------------------------+
| 自定义EditorWindow子类  |        |   自定义Editor子类     |
+-------------------------+        +-------------------------+
| + 自定义状态字段        |        | + 自定义状态字段       |
+-------------------------+        +-------------------------+
| + OnGUI()               |        | + OnInspectorGUI()     |
| + OnEnable()            |        | + OnSceneGUI()         |
| + OnDisable()           |        | + OnEnable()           |
+-------------------------+        +-------------------------+

+-------------------------+        +-------------------------+
|     PropertyDrawer      |        |         GUILayout      |
+-------------------------+        +-------------------------+
| + fieldInfo             |        | + Width()              |
| + attribute             |        | + Height()             |
+-------------------------+        | + ExpandWidth()        |
| + OnGUI()               |        | + ExpandHeight()       |
| + GetPropertyHeight()   |        +-------------------------+
+-------------------------+        | + BeginHorizontal()    |
          ^                        | + EndHorizontal()      |
          |                        | + BeginVertical()      |
+-------------------------+        | + EndVertical()        |
| 自定义PropertyDrawer子类|        | + BeginScrollView()    |
+-------------------------+        | + EndScrollView()      |
| + OnGUI()               |        | + Space()              |
| + GetPropertyHeight()   |        | + FlexibleSpace()      |
+-------------------------+        +-------------------------+
```

## 思维导图

```
Unity编辑器扩展
├── 核心API
│   ├── GUI系统
│   │   ├── GUILayout
│   │   │   ├── 基础控件
│   │   │   │   ├── Button() - 创建按钮
│   │   │   │   ├── Label() - 显示文本标签
│   │   │   │   ├── Toggle() - 创建复选框
│   │   │   │   ├── TextField() - 文本输入框
│   │   │   │   ├── TextArea() - 多行文本区域
│   │   │   │   ├── PasswordField() - 密码输入框
│   │   │   │   └── Box() - 绘制盒子区域
│   │   │   ├── 布局方法
│   │   │   │   ├── BeginHorizontal()/EndHorizontal() - 水平布局
│   │   │   │   ├── BeginVertical()/EndVertical() - 垂直布局 
│   │   │   │   ├── BeginScrollView()/EndScrollView() - 滚动视图
│   │   │   │   └── BeginArea()/EndArea() - 固定区域
│   │   │   └── 布局选项
│   │   │       ├── Width() - 设置宽度
│   │   │       ├── Height() - 设置高度
│   │   │       ├── ExpandWidth() - 是否自动扩展宽度
│   │   │       ├── ExpandHeight() - 是否自动扩展高度
│   │   │       ├── MaxWidth() - 最大宽度
│   │   │       └── MinWidth() - 最小宽度
│   │   │
│   │   ├── EditorGUILayout
│   │   │   ├── 编辑器专用控件
│   │   │   │   ├── ObjectField() - 对象引用字段
│   │   │   │   ├── EnumPopup() - 枚举下拉菜单
│   │   │   │   ├── Foldout() - 折叠面板
│   │   │   │   ├── ColorField() - 颜色选择器
│   │   │   │   ├── Popup() - 弹出选择菜单
│   │   │   │   ├── TagField() - 标签选择器
│   │   │   │   ├── LayerField() - 层级选择器
│   │   │   │   ├── CurveField() - 曲线编辑器
│   │   │   │   └── GradientField() - 渐变编辑器
│   │   │   └── 属性字段
│   │   │       ├── PropertyField() - 序列化属性字段
│   │   │       ├── LabelField() - 只读标签字段
│   │   │       ├── IntField() - 整数输入字段
│   │   │       ├── FloatField() - 浮点数输入字段
│   │   │       ├── Vector2Field() - 二维向量输入
│   │   │       ├── Vector3Field() - 三维向量输入
│   │   │       ├── Vector4Field() - 四维向量输入
│   │   │       └── BoundsField() - 边界框输入
│   │   │
│   │   ├── EditorGUI
│   │   │   ├── 无自动布局版本的控件
│   │   │   │   ├── IntField() - 指定位置的整数字段
│   │   │   │   ├── FloatField() - 指定位置的浮点数字段
│   │   │   │   ├── TextField() - 指定位置的文本字段
│   │   │   │   └── Toggle() - 指定位置的开关
│   │   │   └── 低级绘制方法
│   │   │       ├── LabelField() - 绘制标签
│   │   │       ├── PrefixLabel() - 绘制前缀标签
│   │   │       ├── BeginProperty()/EndProperty() - 属性作用域
│   │   │       ├── BeginDisabledGroup()/EndDisabledGroup() - 禁用组
│   │   │       └── BeginChangeCheck()/EndChangeCheck() - 变更检查
│   │   │
│   │   └── 样式系统
│   │       ├── GUIStyle
│   │       │   ├── 状态样式
│   │       │   │   ├── normal - 正常状态
│   │       │   │   ├── hover - 悬停状态
│   │       │   │   ├── active - 激活状态
│   │       │   │   ├── focused - 焦点状态
│   │       │   │   └── onNormal/onHover/onActive/onFocused - 开启状态
│   │       │   └── 属性
│   │       │       ├── fontSize - 字体大小
│   │       │       ├── fontStyle - 字体样式
│   │       │       ├── alignment - 对齐方式
│   │       │       ├── wordWrap - 文字换行
│   │       │       ├── stretchWidth/stretchHeight - 拉伸
│   │       │       ├── margin - 外边距
│   │       │       └── padding - 内边距
│   │       └── EditorStyles
│   │           ├── 预定义样式
│   │           │   ├── boldLabel - 粗体标签
│   │           │   ├── miniButton - 迷你按钮
│   │           │   ├── foldout - 折叠面板样式
│   │           │   ├── helpBox - 帮助框
│   │           │   ├── toolbar - 工具栏
│   │           │   └── inspectorFullWidthMargins - Inspector全宽边距
│   │           └── 使用方法
│   │               ├── 直接引用(如 EditorStyles.boldLabel)
│   │               └── 自定义克隆(new GUIStyle(EditorStyles.xxx))
│   │
│   ├── 编辑器基类
│   │   ├── Editor
│   │   │   ├── 生命周期
│   │   │   │   ├── OnEnable() - 编辑器启用时调用
│   │   │   │   ├── OnDisable() - 编辑器禁用时调用
│   │   │   │   └── OnDestroy() - 编辑器销毁时调用
│   │   │   ├── 关键方法
│   │   │   │   ├── OnInspectorGUI() - 绘制Inspector界面
│   │   │   │   ├── OnSceneGUI() - 绘制Scene视图界面
│   │   │   │   ├── OnPreviewGUI() - 绘制预览窗口
│   │   │   │   ├── HasPreviewGUI() - 是否有预览窗口
│   │   │   │   ├── GetInfoString() - 获取信息字符串
│   │   │   │   └── DrawDefaultInspector() - 绘制默认Inspector
│   │   │   └── 关键属性
│   │   │       ├── target - 当前目标对象
│   │   │       ├── targets - 多选时的目标对象数组
│   │   │       ├── serializedObject - 序列化对象
│   │   │       └── targetObject - 目标对象的Object引用
│   │   │
│   │   ├── EditorWindow
│   │   │   ├── 窗口方法
│   │   │   │   ├── GetWindow<T>() - 获取窗口实例
│   │   │   │   ├── Show() - 显示窗口
│   │   │   │   ├── ShowUtility() - 显示实用工具窗口
│   │   │   │   ├── ShowPopup() - 显示弹出窗口
│   │   │   │   ├── ShowModal() - 显示模态窗口
│   │   │   │   ├── Close() - 关闭窗口
│   │   │   │   └── Focus() - 窗口获取焦点
│   │   │   ├── 绘制方法
│   │   │   │   ├── OnGUI() - 绘制窗口界面
│   │   │   │   ├── Repaint() - 重绘窗口
│   │   │   │   ├── OnEnable() - 窗口启用时调用
│   │   │   │   ├── OnDisable() - 窗口禁用时调用
│   │   │   │   ├── OnFocus() - 窗口获取焦点时
│   │   │   │   └── OnLostFocus() - 窗口失去焦点时
│   │   │   └── 窗口属性
│   │   │       ├── position - 窗口位置和大小
│   │   │       ├── titleContent - 窗口标题
│   │   │       ├── wantsMouseMove - 是否需要鼠标移动事件
│   │   │       ├── minSize - 最小窗口尺寸
│   │   │       └── maxSize - 最大窗口尺寸
│   │   │
│   │   └── PropertyDrawer
│   │       ├── 绘制方法
│   │       │   ├── OnGUI() - 绘制属性UI
│   │       │   └── GetPropertyHeight() - 获取属性高度
│   │       └── 关联到特定字段或类型
│   │           ├── fieldInfo - 关联的字段信息
│   │           └── attribute - 关联的特性实例
│   │
│   └── 辅助工具
│       ├── Handles
│       │   ├── 3D控制工具
│       │   │   ├── PositionHandle() - 位置控制柄
│       │   │   ├── RotationHandle() - 旋转控制柄
│       │   │   ├── ScaleHandle() - 缩放控制柄
│       │   │   ├── FreeMoveHandle() - 自由移动控制柄
│       │   │   └── Button() - 场景中的按钮
│       │   └── 绘制方法
│       │       ├── DrawLine() - 绘制线条
│       │       ├── DrawWireCube() - 绘制线框立方体
│       │       ├── DrawSolidRectangleWithOutline() - 绘制实心矩形带轮廓
│       │       ├── color - 绘制颜色
│       │       ├── matrix - 绘制矩阵
│       │       └── SphereHandleCap() - 球形控制柄
│       │
│       ├── Gizmos
│       │   ├── 场景辅助图形绘制
│   │   │   ├── DrawSphere() - 绘制球体
│   │   │   ├── DrawCube() - 绘制立方体
│   │   │   ├── DrawLine() - 绘制线段
│   │   │   ├── DrawWireSphere() - 绘制线框球体
│   │   │   ├── DrawWireCube() - 绘制线框立方体
│   │   │   └── DrawIcon() - 绘制图标
│   │   └── 属性控制
│   │       ├── color - 绘制颜色
│   │       └── matrix - 绘制矩阵
│   │
│   └── EditorPrefs
│       ├── 数据持久化
│       │   ├── GetInt()/SetInt() - 整数值存取
│       │   ├── GetFloat()/SetFloat() - 浮点数值存取
│       │   ├── GetBool()/SetBool() - 布尔值存取
│       │   ├── GetString()/SetString() - 字符串存取
│       │   └── HasKey() - 检查键是否存在
│       └── 高级用法
│           ├── DeleteKey() - 删除指定键
│           ├── DeleteAll() - 删除所有键
│           └── 自定义对象序列化/反序列化
│
├── 扩展特性
│   ├── CustomEditor
│   │   ├── 指定目标组件类型
│   │   │   ├── [CustomEditor(typeof(Transform))]
│   │   │   └── [CustomEditor(typeof(MyComponent))]
│   │   └── 多对象编辑
│   │       ├── [CustomEditor(typeof(X), true)] - 应用于子类
│   │       └── [CanEditMultipleObjects] - 支持多选编辑
│   │
│   ├── MenuItem
│   │   ├── 在菜单中添加项目
│   │   │   ├── [MenuItem("Tools/MyTool")]
│   │   │   ├── [MenuItem("Assets/Create/MyAsset")]
│   │   │   └── [MenuItem("CONTEXT/Transform/MyOperation")]
│   │   └── 快捷键和验证
│   │       ├── [MenuItem("...", false, priority)]
│   │       └── [MenuItem("...", true)] - 验证函数
│   │
│   ├── CustomPropertyDrawer
│   │   ├── 指定自定义属性绘制器的目标类型
│   │   │   ├── [CustomPropertyDrawer(typeof(MyType))]
│   │   │   └── [CustomPropertyDrawer(typeof(MyAttribute))]
│   │   └── 应用范围
│   │       └── [CustomPropertyDrawer(typeof(X), true)] - 应用于子类
│   │
│   ├── InitializeOnLoad
│   │   ├── 编辑器启动时初始化
│   │   │   ├── [InitializeOnLoad]
│   │   │   └── 静态构造函数
│   │   └── 方法级初始化
│   │       └── [InitializeOnLoadMethod]
│   │
│   └── ContextMenu
│       ├── 在组件右键菜单中添加项
│       │   ├── [ContextMenu("ItemName")]
│       │   └── 标记在方法上
│       └── 带条件的菜单项
│           └── [ContextMenuItem("显示名称", "方法名")]
│
├── 对象交互
│   ├── SerializedObject
│   │   ├── 包装目标对象的序列化数据
│   │   │   ├── new SerializedObject(targetObject)
│   │   │   └── new SerializedObject(targetObjects) - 多对象
│   │   └── 提供修改属性的接口
│   │       ├── FindProperty() - 查找序列化属性
│   │       ├── Update() - 从目标对象更新数据
│   │       ├── ApplyModifiedProperties() - 应用修改
│   │       └── ApplyModifiedPropertiesWithoutUndo() - 不记录撤销
│   │
│   ├── SerializedProperty
│   │   ├── 代表单个序列化属性
│   │   │   ├── 类型特定值
│   │   │   │   ├── intValue - 整数值
│   │   │   │   ├── floatValue - 浮点数值
│   │   │   │   ├── boolValue - 布尔值
│   │   │   │   ├── stringValue - 字符串值
│   │   │   │   ├── objectReferenceValue - 对象引用
│   │   │   │   ├── colorValue - 颜色值
│   │   │   │   └── vector3Value - 三维向量值
│   │   │   └── 属性信息
│   │   │       ├── name - 属性名称
│   │   │       ├── displayName - 显示名称
│   │   │       ├── propertyType - 属性类型
│   │   │       ├── isArray - 是否是数组
│   │   │       └── hasMultipleDifferentValues - 多值不同
│   │   └── 提供访问和修改属性的方法
│   │       ├── Next() - 移至下一属性
│   │       ├── FindPropertyRelative() - 查找相对属性
│   │       ├── GetArrayElementAtIndex() - 获取数组元素
│   │       ├── arraySize - 数组大小
│   │       └── isExpanded - 是否展开
│   │
│   └── Undo系统
│       ├── Undo.RecordObject
│       │   ├── Undo.RecordObject(obj, "Action Name")
│       │   └── Undo.RecordObjects(objs, "Action Name")
│       ├── Undo操作
│       │   ├── Undo.RegisterCreatedObjectUndo() - 注册创建对象撤销
│       │   ├── Undo.DestroyObjectImmediate() - 可撤销地销毁对象
│       │   ├── Undo.RegisterCompleteObjectUndo() - 注册完整对象撤销
│       │   └── Undo.RegisterFullObjectHierarchyUndo() - 注册层级撤销
│       └── 支持编辑操作的撤销和重做
│           ├── Undo.PerformUndo() - 执行撤销
│           ├── Undo.PerformRedo() - 执行重做
│           ├── Undo.IncrementCurrentGroup() - 增加当前组
│           └── Undo.SetCurrentGroupName() - 设置当前组名称
│
├── 实现方式
│   ├── 自定义Inspector
│   │   ├── 继承Editor类
│   │   │   └── public class MyComponentEditor : Editor { }
│   │   ├── 应用CustomEditor特性
│   │   │   └── [CustomEditor(typeof(MyComponent))]
│   │   └── 实现OnInspectorGUI方法
│   │       ├── serializedObject.Update()
│   │       ├── EditorGUILayout控件调用
│   │       └── serializedObject.ApplyModifiedProperties()
│   │
│   ├── 自定义编辑器窗口
│   │   ├── 继承EditorWindow类
│   │   │   └── public class MyEditorWindow : EditorWindow { }
│   │   ├── 通过MenuItem打开
│   │   │   └── [MenuItem("Window/My Window")]
│   │   │       public static void ShowWindow() { ... }
│   │   └── 实现OnGUI方法
│   │       ├── 使用EditorGUILayout绘制控件
│   │       ├── 处理输入事件(Event.current)
│   │       └── 必要时调用Repaint()
│   │
│   ├── 自定义属性绘制器
│   │   ├── 继承PropertyDrawer类
│   │   │   └── public class MyPropertyDrawer : PropertyDrawer { }
│   │   ├── 应用CustomPropertyDrawer特性
│   │   │   └── [CustomPropertyDrawer(typeof(MyType))]
│   │   │       或[CustomPropertyDrawer(typeof(MyAttribute))]
│   │   └── 实现OnGUI方法
│   │       ├── EditorGUI.BeginProperty()/EndProperty()
│   │       ├── 使用EditorGUI绘制控件
│   │       └── 控制位置和布局
│   │
│   └── 自定义Gizmo和Handle
│       ├── 重写OnSceneGUI方法
│       │   └── protected override void OnSceneGUI() { ... }
│       └── 调用Handles和Gizmos API
│           ├── Handles绘制代码
│           │   ├── Handles.matrix = ...
│           │   ├── Handles.color = ...
│           │   └── Handles.PositionHandle()/RotationHandle()/...
│           ├── 事件处理
│           │   ├── Event.current.type
│           │   ├── HandleUtility.GUIPointToWorldRay()
│           │   └── EditorGUI.BeginChangeCheck()/EndChangeCheck()
│           └── Undo记录
│               └── Undo.RecordObject()
│
└── 最佳实践
    ├── 保持一致的UI风格
    │   ├── 使用EditorStyles预定义样式
    │   ├── 遵循Unity排版规则
    │   └── 适当使用空间和分组
    │
    ├── 有效管理Undo操作
    │   ├── 每次修改前记录状态
    │   ├── 使用有意义的操作名称
    │   └── 适当合并操作(IncrementCurrentGroup)
    │
    ├── 优化编辑器性能
    │   ├── 缓存静态计算结果
    │   ├── 避免在OnGUI中进行频繁计算
    │   ├── 使用EditorApplication.delayCall推迟操作
    │   └── 使用异步加载大型资源
    │
    └── 封装和模块化编辑器代码
        ├── 抽象通用UI绘制方法
        ├── 分离数据和视图逻辑
        ├── 使用EditorPrefs保存配置
        └── 实现可重用的编辑器组件
```

## 重要类与接口一览表

### 编辑器基础类

| 类名 | 用途 | 关键方法/属性 |
|------|------|--------------|
| `Editor` | 自定义Inspector基类 | `OnInspectorGUI()`, `OnSceneGUI()`, `target`, `targets`, `serializedObject` |
| `EditorWindow` | 自定义编辑器窗口基类 | `OnGUI()`, `GetWindow()`, `Show()`, `position`, `titleContent` |
| `PropertyDrawer` | 自定义属性绘制器基类 | `OnGUI()`, `GetPropertyHeight()`, `attribute`, `fieldInfo` |
| `SerializedObject` | 序列化对象封装 | `FindProperty()`, `Update()`, `ApplyModifiedProperties()` |
| `SerializedProperty` | 序列化属性封装 | `floatValue`, `intValue`, `stringValue`, `objectReferenceValue`, `Next()` |

### GUI系统类

| 类名 | 用途 | 主要方法/属性 |
|------|------|---------------|
| `GUILayout` | 自动布局GUI控件 | `Button()`, `Label()`, `Toggle()`, `TextField()`, `BeginHorizontal()`, `EndHorizontal()` |
| `EditorGUILayout` | 编辑器专用GUI控件 | `ObjectField()`, `PropertyField()`, `EnumPopup()`, `Foldout()`, `ColorField()` |
| `GUIStyle` | GUI样式定义 | `normal`, `hover`, `active`, `fontSize`, `alignment`, `wordWrap` |
| `EditorStyles` | 预定义编辑器样式 | `boldLabel`, `miniButton`, `helpBox`, `foldout` |
| `GUISkin` | GUI皮肤 | `button`, `label`, `textField`, `box`, `customStyles` |

### 工具与辅助类

| 类名 | 用途 | 主要方法/属性 |
|------|------|---------------|
| `Handles` | 场景视图交互控件 | `PositionHandle()`, `RotationHandle()`, `ScaleHandle()`, `DrawLine()` |
| `Gizmos` | 场景视图辅助图形 | `DrawSphere()`, `DrawCube()`, `DrawLine()`, `DrawWireCube()` |
| `EditorPrefs` | 编辑器配置持久化 | `GetInt()`, `SetInt()`, `GetString()`, `SetString()`, `GetBool()` |
| `EditorUtility` | 编辑器实用工具 | `DisplayDialog()`, `DisplayProgressBar()`, `SetDirty()` |
| `Selection` | 当前选择的对象 | `activeObject`, `activeGameObject`, `objects` |

## 主要扩展模块详解

### GUILayout系统

GUILayout是Unity编辑器扩展中用于创建自动布局UI的核心系统。它通过自动计算控件位置和大小，简化了UI开发。主要包含：

- **基础控件**：Button、Label、Toggle、TextField等
  - **特点**：自动处理布局，不需要手动指定坐标
  - **优势**：代码简洁，适应不同分辨率和DPI
  - **局限**：性能稍差，复杂界面应考虑使用非自动布局版本
  
- **布局容器**：BeginHorizontal/EndHorizontal、BeginVertical/EndVertical
  - **嵌套规则**：可以任意嵌套，但应避免过深嵌套
  - **使用模式**：必须成对调用，推荐使用using语句确保配对
  
- **布局选项**：Width、Height、ExpandWidth、ExpandHeight
  - **固定尺寸**：Width/Height设置固定尺寸
  - **弹性尺寸**：ExpandWidth/ExpandHeight设置是否自动扩展
  - **组合使用**：如`GUILayout.Width(100), GUILayout.ExpandHeight(true)`
  
- **高级布局**：ScrollView、Space、FlexibleSpace
  - **滚动视图**：处理内容超出可视区域的情况
  - **间距控制**：固定间距(Space)和自适应间距(FlexibleSpace)
  - **使用技巧**：FlexibleSpace用于两端对齐布局

### 自定义编辑器(CustomEditor)

自定义编辑器允许开发者为特定组件类型创建自定义Inspector界面，实现更直观、更专业的组件配置界面。

#### 设计思路

1. **分析目标组件的数据结构**
   - 识别关键属性和功能
   - 决定哪些属性需要特殊UI
   - 考虑属性之间的关联关系
   
2. **设计交互模式**
   - 单值编辑 vs. 多值编辑
   - 直接编辑 vs. 预设选择
   - 是否需要预览功能
   - 是否需要自定义场景工具

3. **组织UI布局**
   - 分组相关属性
   - 使用折叠面板整理复杂界面
   - 使用标签和颜色引导用户注意力

#### 工作流程

1. 创建继承自Editor的子类
2. 使用[CustomEditor(typeof(目标类))]特性标记
3. 重写OnInspectorGUI()方法实现自定义界面
   ```csharp
   public override void OnInspectorGUI()
   {
       serializedObject.Update();
       
       EditorGUI.BeginChangeCheck();
       // 绘制自定义UI
       if(EditorGUI.EndChangeCheck())
       {
           Undo.RecordObject(target, "Changed Properties");
           // 更新目标对象值
       }
       
       serializedObject.ApplyModifiedProperties();
   }
   ```
4. 可选重写OnSceneGUI()添加场景视图交互功能
   ```csharp
   public override void OnSceneGUI()
   {
       // 获取目标组件
       MyComponent myTarget = (MyComponent)target;
       
       // 绘制控制柄
       EditorGUI.BeginChangeCheck();
       Vector3 newPosition = Handles.PositionHandle(myTarget.position, Quaternion.identity);
       if (EditorGUI.EndChangeCheck())
       {
           Undo.RecordObject(target, "Move Point");
           myTarget.position = newPosition;
       }
   }
   ```

#### 案例：TransformTweenAnimation

**设计思路**：
- 提供直观的动画关键帧编辑界面
- 支持位置、旋转、缩放动画的可视化编辑
- 集成预览功能，直接在编辑器中测试动画效果
- 提供常用动画曲线预设

**关键实现**：
- 使用EditorGUILayout.CurveField编辑动画曲线
- 在Scene视图中绘制动画路径和关键帧
- 使用EditorApplication.update实现编辑器中的动画预览
- 创建自定义预览窗口展示动画效果

### 编辑器窗口(EditorWindow)

编辑器窗口允许创建独立于Inspector的工具窗口，实现复杂的编辑器功能和工作流。

#### 设计思路

1. **定义窗口用途**
   - 工具型：提供特定功能的工具
   - 数据编辑型：编辑特定类型的数据
   - 可视化型：展示项目信息或统计数据
   - 监控型：实时显示运行状态或调试信息

2. **设计用户体验**
   - 窗口布局和组织
   - 键盘和鼠标交互方式
   - 是否需要持久化存储设置
   - 与其他窗口的协作方式

3. **性能考量**
   - OnGUI调用频率和性能影响
   - 数据缓存和异步加载策略
   - UI绘制优化

#### 工作流程

1. 创建继承自EditorWindow的子类
   ```csharp
   public class MyEditorWindow : EditorWindow
   {
       [MenuItem("Window/My Window")]
       public static void ShowWindow()
       {
           GetWindow<MyEditorWindow>("My Window");
       }
   }
   ```

2. 实现静态的ShowWindow()方法并使用[MenuItem]特性注册菜单项
3. 重写OnGUI()方法实现窗口界面
   ```csharp
   private void OnGUI()
   {
       // 绘制窗口内容
       GUILayout.Label("My Window Content", EditorStyles.boldLabel);
       
       // 处理输入
       Event evt = Event.current;
       if (evt.type == EventType.MouseDown)
       {
           // 处理鼠标点击
       }
   }
   ```
4. 使用position和titleContent设置窗口属性
   ```csharp
   private void OnEnable()
   {
       titleContent = new GUIContent("My Window", EditorGUIUtility.FindTexture("_Help"));
       minSize = new Vector2(300, 200);
   }
   ```

#### 案例：DevelopmentMemo

**设计思路**：
- 为团队提供共享开发笔记和任务管理功能
- 支持按类别组织备忘录
- 集成简单的任务分配和跟踪功能
- 数据存储在项目内，便于版本控制

**关键实现**：
- 使用ScriptableObject存储备忘录数据
- 创建专用EditorWindow显示和编辑
- 集成简单的富文本编辑器
- 实现基于AssetDatabase的版本控制兼容
- 使用EditorApplication.update检查更新

### 属性绘制器(PropertyDrawer)

属性绘制器用于自定义特定类型或带特定特性的字段在Inspector中的显示方式，实现更专业和直观的数据编辑体验。

#### 设计思路

1. **识别目标数据类型的特性**
   - 数据结构和关系
   - 合理的编辑方式和约束
   - 可视化需求

2. **确定PropertyDrawer类型**
   - 类型绘制器：针对特定数据类型
   - 特性绘制器：针对带特定特性的字段

3. **规划UI布局**
   - 控件选择和排列
   - 错误处理和验证反馈
   - 自定义控件或复合控件

#### 工作流程

1. 创建继承自PropertyDrawer的子类
   ```csharp
   public class MyTypeDrawer : PropertyDrawer
   {
   }
   ```

2. 使用[CustomPropertyDrawer(typeof(目标类或特性))]特性标记
   ```csharp
   [CustomPropertyDrawer(typeof(MyType))]
   public class MyTypeDrawer : PropertyDrawer
   {
   }
   ```

3. 重写OnGUI()和GetPropertyHeight()方法
   ```csharp
   public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
   {
       EditorGUI.BeginProperty(position, label, property);
       
       // 绘制自定义UI
       
       EditorGUI.EndProperty();
   }
   
   public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
   {
       // 返回属性实际高度
       return EditorGUIUtility.singleLineHeight * 3;
   }
   ```

4. 使用EditorGUI.PropertyField()等方法绘制属性

#### 案例：SpritePropertyDrawer

**设计思路**：
- 为Sprite类型字段提供更直观的预览和编辑界面
- 在Inspector中直接显示精灵图片，而不只是名称
- 支持直接拖放精灵资源
- 显示精灵的基本信息（尺寸、类型等）

**关键实现**：
- 使用EditorGUI.ObjectField处理对象引用
- 绘制精灵预览图像
- 计算合适的UI尺寸
- 显示精灵相关属性

### 场景视图工具(Gizmos & Handles)

场景视图工具用于在Scene视图中绘制辅助图形和交互控件，实现直观的可视化编辑体验。

#### 设计思路

1. **确定可视化需求**
   - 静态展示型：只显示辅助信息
   - 交互编辑型：允许直接操作和编辑
   - 混合型：兼具显示和交互功能

2. **规划交互模式**
   - 拖拽操作
   - 选择和多选
   - 键盘修饰键结合
   - 自定义控制柄

3. **优化性能和视觉效果**
   - 适当使用透明度和颜色
   - 考虑绘制顺序和深度测试
   - 避免过度绘制

#### 实现方式

1. Gizmos用于绘制纯视觉辅助图形
   ```csharp
   private void OnDrawGizmos()
   {
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position, radius);
   }
   
   private void OnDrawGizmosSelected()
   {
       Gizmos.color = Color.green;
       Gizmos.DrawSphere(transform.position, radius * 0.8f);
   }
   ```

2. Handles用于创建可交互的3D控件
   ```csharp
   public override void OnSceneGUI()
   {
       MyComponent myTarget = (MyComponent)target;
       
       EditorGUI.BeginChangeCheck();
       Vector3 newPosition = Handles.PositionHandle(myTarget.point, Quaternion.identity);
       if (EditorGUI.EndChangeCheck())
       {
           Undo.RecordObject(target, "Move Point");
           myTarget.point = newPosition;
       }
   }
   ```

3. 通常在OnSceneGUI()方法中实现
4. 支持位置、旋转、缩放等交互操作

#### 案例：BezierCurvePath

**设计思路**：
- 创建可视化的贝塞尔曲线路径编辑工具
- 支持添加、删除和调整路径点
- 通过控制柄直观编辑曲线形状
- 提供路径预览和采样功能

**关键实现**：
- 使用Handles.PositionHandle()编辑路径点位置
- 自定义控制柄编辑切线方向和长度
- 使用Handles.DrawBezier()绘制曲线
- 实现曲线采样和长度计算功能
- 集成Undo/Redo支持
- 通过事件系统实现点击添加和删除节点

### 编辑器配置(EditorPrefs)

EditorPrefs用于在编辑器会话之间保存和加载设置，实现编辑器工具的状态持久化。

#### 设计思路

1. **确定需要持久化的数据**
   - 用户偏好设置
   - 窗口状态和布局
   - 最近使用的选项
   - 编辑器工具配置

2. **制定存储策略**
   - 键名命名规范（通常使用前缀避免冲突）
   - 数据类型选择
   - 默认值处理
   - 复杂对象的序列化方案

3. **考虑数据安全和兼容性**
   - 版本兼容处理
   - 数据验证和错误恢复
   - 数据导入导出

#### 实现方式

1. 支持整数、浮点数、字符串和布尔值类型
   ```csharp
   // 保存设置
   EditorPrefs.SetInt("MyTool.ItemCount", 10);
   EditorPrefs.SetFloat("MyTool.Scale", 1.5f);
   EditorPrefs.SetString("MyTool.LastPath", path);
   EditorPrefs.SetBool("MyTool.ShowHelp", showHelp);
   
   // 读取设置
   int count = EditorPrefs.GetInt("MyTool.ItemCount", 5); // 5是默认值
   float scale = EditorPrefs.GetFloat("MyTool.Scale", 1.0f);
   string path = EditorPrefs.GetString("MyTool.LastPath", "");
   bool showHelp = EditorPrefs.GetBool("MyTool.ShowHelp", true);
   ```

2. 数据持久化存储在编辑器配置中
3. 用于保存窗口位置、用户偏好等
4. 复杂对象通过JSON序列化存储
   ```csharp
   // 保存复杂对象
   public static void SetObject<T>(string key, T obj)
   {
       string json = JsonUtility.ToJson(obj);
       EditorPrefs.SetString(key, json);
   }
   
   // 读取复杂对象
   public static T GetObject<T>(string key, T defaultValue = default)
   {
       if (EditorPrefs.HasKey(key))
       {
           string json = EditorPrefs.GetString(key);
           return JsonUtility.FromJson<T>(json);
       }
       return defaultValue;
   }
   ```

#### 案例：EditorPrefsUtility

**设计思路**：
- 创建EditorPrefs的扩展工具类，简化配置管理
- 支持复杂对象的序列化和反序列化
- 提供分类管理和批量操作功能
- 集成配置导入导出功能

**关键实现**：
- 使用泛型方法处理不同类型数据
- 实现JSON序列化支持复杂对象
- 添加前缀管理避免键名冲突
- 集成配置编辑器界面
- 提供配置导入导出功能

## 实际应用案例与设计思路

### 1. TransformTweenAnimation - 自定义动画编辑器

**功能**：为Unity对象创建位移、旋转和缩放的补间动画。

**设计思路**：
- **数据结构**：使用ScriptableObject存储动画数据，包含关键帧和曲线
- **编辑器设计**：提供关键帧编辑、曲线编辑和动画预览功能
- **用户交互**：通过场景视图可视化编辑路径，Inspector中编辑曲线参数
- **集成模式**：与Unity动画系统兼容，支持Timeline集成

**技术实现**：
- 使用CustomEditor创建专用Inspector
- 通过OnSceneGUI实现场景视图编辑功能
- 使用EditorGUILayout.CurveField编辑动画曲线
- 通过EditorApplication.update实现编辑器中预览

### 2. EditorStylesPreviewer - 样式预览工具

**功能**：预览Unity内置编辑器样式，帮助开发者选择合适的样式。

**设计思路**：
- **分类显示**：按类型分组显示不同样式
- **实时预览**：实时显示样式效果和属性
- **代码生成**：自动生成样式使用代码
- **搜索功能**：支持按名称搜索样式

**技术实现**：
- 继承EditorWindow创建专用窗口
- 使用反射获取EditorStyles所有内置样式
- 创建可滚动的分组显示界面
- 集成代码生成和复制功能

### 3. BezierPath - 贝塞尔曲线路径编辑工具

**功能**：创建和编辑基于贝塞尔曲线的路径系统。

**设计思路**：
- **路径数据**：每个路径点包含位置和切线信息
- **可视化编辑**：在场景视图中通过控制柄直观编辑
- **采样功能**：支持按距离或时间采样路径点
- **扩展性**：支持自定义曲线插值和评估方法

**技术实现**：
- 创建自定义控件类表示路径点
- 使用Handles API实现控制柄
- 实现贝塞尔曲线数学计算
- 通过OnSceneGUI提供交互式编辑功能
- 集成Undo/Redo支持

### 4. AssetBundleBuilder - 资源包构建工具

**功能**：管理和构建Unity AssetBundle资源包。

**设计思路**：
- **配置管理**：集中管理不同平台的打包配置
- **依赖分析**：可视化显示资源依赖关系
- **批量操作**：支持批量设置和构建
- **版本控制**：集成版本管理和差异比较

**技术实现**：
- 创建专用EditorWindow管理界面
- 使用AssetDatabase API分析资源
- 集成BuildPipeline进行构建
- 使用EditorPrefs保存配置
- 实现可视化依赖图表

### 5. DevelopmentMemo - 团队开发备忘录工具

**功能**：团队内部共享开发注释和任务的工具。

**设计思路**：
- **数据存储**：将备忘录作为项目资产存储
- **分类系统**：支持按模块、优先级等分类
- **协作功能**：任务分配和状态跟踪
- **通知系统**：关键备忘更新提醒

**技术实现**：
- 使用ScriptableObject存储备忘数据
- 创建专用EditorWindow显示和编辑
- 集成简单的富文本编辑器
- 实现基于AssetDatabase的版本控制兼容
- 使用EditorApplication.update检查更新

### 6. AvatarCameraController - 角色相机控制器编辑工具

**功能**：设计和调试第三人称相机控制系统。

**设计思路**：
- **预设管理**：保存和加载相机配置预设
- **实时预览**：在编辑时实时预览效果
- **场景交互**：通过Gizmos可视化相机参数
- **多模式支持**：跟随、环绕、定点等多种模式

**技术实现**：
- 使用CustomEditor创建专用界面
- 通过Handles实现视觉化编辑
- 在OnSceneGUI中预览相机视锥
- 使用EditorApplication.update实现编辑器中的相机模拟
- 集成预设系统管理配置

通过这些章节的学习和实践，你将能够掌握Unity编辑器扩展开发的核心技术，为你的项目创建高效的开发工具，提升开发效率和项目质量。

## 最佳实践建议

1. **保持UI一致性**：遵循Unity内置编辑器的UI风格和交互模式
2. **正确处理Undo**：使用Undo.RecordObject()记录可撤销操作
3. **优化性能**：避免在OnGUI中进行耗时操作，使用缓存减少重复计算
4. **模块化设计**：将复杂编辑器功能拆分为可重用组件
5. **保持代码整洁**：分离业务逻辑和UI代码，便于维护和扩展
6. **正确使用序列化系统**：理解SerializedObject和SerializedProperty的工作方式
7. **适当使用EditorPrefs**：保存用户偏好，但避免保存大量数据

## UnityEditor扩展的重要接口与方法

| 接口/方法 | 作用 | 所属类 | 重要参数/属性 |
|----------|------|--------|--------------|
| `OnInspectorGUI()` | 绘制Inspector窗口内容 | `Editor` | 无参数，返回void |
| `OnSceneGUI()` | 在场景视图中添加交互控件 | `Editor` | 无参数，返回void |
| `OnGUI()` | 绘制编辑器窗口内容 | `EditorWindow` | 无参数，返回void |
| `OnEnable()` | 当Inspector/窗口激活时调用 | `Editor`/`EditorWindow` | 无参数，初始化资源和状态 |
| `OnDisable()` | 当Inspector/窗口禁用时调用 | `Editor`/`EditorWindow` | 无参数，清理资源 |
| `DrawDefaultInspector()` | 绘制默认Inspector内容 | `Editor` | 返回bool，表示是否有属性被修改 |
| `serializedObject.Update()` | 更新序列化对象数据 | `SerializedObject` | 从目标对象更新数据到SerializedObject |
| `serializedObject.ApplyModifiedProperties()` | 应用修改的属性 | `SerializedObject` | 将修改应用回目标对象并记录撤销 |
| `Undo.RecordObject()` | 记录对象状态用于撤销 | `Undo` | 参数：目标对象, 操作名称 |
| `EditorGUIUtility.labelWidth` | 设置标签宽度 | `EditorGUIUtility` | 浮点数值，定义标签区域宽度 |
| `EditorGUILayout.PropertyField()` | 绘制属性字段 | `EditorGUILayout` | 参数：SerializedProperty, GUIContent, 额外选项 |
| `EditorGUI.BeginChangeCheck()/EndChangeCheck()` | 检测UI变更 | `EditorGUI` | 返回bool，表示变更状态 |
| `EditorGUI.BeginDisabledGroup()/EndDisabledGroup()` | 禁用UI区域 | `EditorGUI` | 参数：是否禁用(bool) |
| `Handles.DrawLine()` | 绘制线段 | `Handles` | 参数：起点, 终点 |
| `Handles.PositionHandle()` | 位置控制柄 | `Handles` | 参数：位置, 旋转; 返回新位置 |
| `AssetDatabase.CreateAsset()` | 创建资产文件 | `AssetDatabase` | 参数：对象, 路径 |
| `AssetDatabase.SaveAssets()` | 保存修改的资产 | `AssetDatabase` | 无参数，保存所有修改 |
| `Selection.objects` | 当前选中的对象 | `Selection` | 数组，可读可写 |
| `EditorUtility.SetDirty()` | 标记对象为已修改 | `EditorUtility` | 参数：目标对象 |
| `EditorUtility.DisplayDialog()` | 显示对话框 | `EditorUtility` | 参数：标题, 内容, 确认按钮, 取消按钮 |
| `ProjectWindowUtil.CreateAsset()` | 在项目窗口创建资产 | `ProjectWindowUtil` | 参数：对象, 路径 |
| `HandleUtility.GetHandleSize()` | 获取手柄大小 | `HandleUtility` | 参数：位置; 根据相机距离返回比例 |
| `SceneView.lastActiveSceneView` | 最后活动的场景视图 | `SceneView` | 只读，获取当前场景视图 |
| `PrefabUtility.InstantiatePrefab()` | 实例化预制体 | `PrefabUtility` | 参数：预制体对象; 返回实例 |
| `EditorSceneManager.SaveScene()` | 保存场景 | `EditorSceneManager` | 参数：场景对象, 路径 |
| `EditorApplication.delayCall` | 延迟调用 | `EditorApplication` | 委托，添加延迟执行的方法 |

## Unity编辑器扩展应用实例

1. **TransformTweenAnimation**：自定义动画编辑器，用于创建对象的位移、旋转和缩放动画
2. **EditorStylesPreviewer**：预览所有Unity内置编辑器样式的工具
3. **BeizerPath**：贝塞尔曲线路径编辑工具，带场景视图交互控件
4. **AssetBundleBuilder**：资源包构建与管理工具
5. **DevelopmentMemo**：开发备忘录工具，用于团队成员间共享开发信息
6. **AvatarCameraController**：角色相机控制器的可视化编辑工具
7. **ResourceComponent**：资源引用管理和优化工具
8. **AudioDatabase**：音频资源管理与预览系统
9. **BatchBuild**：多平台批量构建工具
10. **GUIPreviewer**：GUI控件预览和测试工具

通过这些章节的学习和实践，你将能够掌握Unity编辑器扩展开发的核心技术，为你的项目创建高效的开发工具，提升开发效率和项目质量。
