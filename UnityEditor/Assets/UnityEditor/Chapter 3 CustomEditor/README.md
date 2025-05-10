# Unity编辑器扩展 - Chapter 3: CustomEditor

## 概述

本章专注于Unity编辑器中的自定义编辑器(Custom Editor)系统，这是Unity编辑器扩展中最常用和最强大的功能之一。通过自定义编辑器，开发者可以完全控制组件在Inspector中的显示和交互方式，提供更直观、更高效的开发体验。本章通过多个示例展示了如何创建和使用各种类型的自定义编辑器。

## 核心知识点

### 自定义编辑器概览

Unity编辑器中的自定义编辑器主要分为以下几种类型：

- **CustomEditor** - 为特定组件类型创建自定义Inspector界面
- **PropertyDrawer** - 为特定属性类型创建自定义绘制方式
- **EditorWindow** - 创建完全自定义的编辑器窗口
- **DecoratorDrawer** - 为特性标签创建自定义绘制方式

## 相关特性

### CustomEditor特性

- **基本语法**: `[CustomEditor(typeof(YourComponentType))]`
- **用途**: 为指定的组件类型创建自定义Inspector界面
- **关键功能**:
  - 替换默认Inspector界面
  - 自定义属性的显示和编辑方式
  - 添加自定义按钮和功能
  - 控制组件预览
- **示例**: TransformEditor类使用`[CustomEditor(typeof(Transform))]`来自定义Transform组件的Inspector界面

### PropertyDrawer特性

- **基本语法**: `[CustomPropertyDrawer(typeof(YourAttributeType))]` 或 `[CustomPropertyDrawer(typeof(YourFieldType))]`
- **用途**: 为特定属性类型或使用特定特性标记的属性创建自定义绘制方式
- **关键功能**:
  - 自定义单个属性的显示方式
  - 跨组件复用绘制逻辑
  - 创建更复杂的属性编辑界面
- **示例**: TimePropertyDrawer类使用`[CustomPropertyDrawer(typeof(TimeAttribute))]`来自定义时间属性的显示方式

### SerializedProperty和SerializedObject

- **SerializedObject**: 表示序列化对象，通常用于访问和修改目标对象的属性
- **SerializedProperty**: 表示序列化属性，用于读取和修改特定属性的值
- **关键功能**:
  - 提供对私有字段的访问
  - 处理撤销/重做操作
  - 支持多对象编辑
  - 自动处理脏标记和保存

## 代码结构

本章包含的主要示例：

1. **TransfromEditor** - 演示如何自定义Transform组件的Inspector界面
2. **RectTransformEditor** - 演示如何自定义RectTransform组件的Inspector界面
3. **CustomComponent** - 演示如何为自定义组件创建编辑器界面
4. **PropertyDrawer** - 演示如何创建自定义属性绘制器
5. **TransformTweenAnimation** - 一个复杂示例，展示如何创建动画编辑器

## 思维导图

```
Unity CustomEditor系统
├── 自定义编辑器类型
│   ├── CustomEditor
│   │   ├── 基本用法
│   │   │   └── [CustomEditor(typeof(ComponentType))]
│   │   ├── 关键方法
│   │   │   ├── 生命周期函数:
│   │   │   │   ├── Awake() -- 编辑器创建时调用，通常用于初始化数据
│   │   │   │   ├── OnEnable() -- 编辑器启用时调用，通常用于初始化
│   │   │   │   ├── OnDisable() -- 编辑器禁用时调用，通常用于清理
│   │   │   │   ├── OnDestroy() -- 编辑器销毁时调用，通常用于释放资源
│   │   │   │   ├── OnValidate() -- 属性值更改时调用，通常用于验证和更新
│   │   │   ├── OnInspectorGUI() -- InspectorGui编辑入口，负责绘制Inspector界面的内容
│   │   │   ├── OnPreviewGUI() -- 控制组件的预览显示，允许自定义预览的渲染
│   │   │   ├── HasPreviewGUI() -- 检查是否启用预览功能，返回布尔值
│   │   │   ├── GetPreviewTitle() -- 获取预览的标题，通常用于显示在预览窗口的顶部
│   │   │   └── OnPreviewSettings() -- 处理预览的设置，允许用户自定义预览的参数
│   │   └── 应用场景
│   │       ├── 替换内置组件编辑器 -- 通过自定义编辑器替代Unity的默认Inspector
│   │       └── 自定义组件编辑器 -- 为自定义组件提供特定的Inspector界面
│   │
│   ├── PropertyDrawer - 自定义属性绘制器，用于定制Inspector中属性的显示方式，通过EditorGUI提供的控件和方法来实现自定义的显示效果
│   │   ├── 基本用法
│   │   │   ├── [CustomPropertyDrawer(typeof(AttributeType))] -- 为特定属性类型创建自定义制器
│   │   │   └── [CustomPropertyDrawer(typeof(FieldType))] -- 为特定字段类型创建自定义绘制器
│   │   ├── 关键方法
│   │   │   ├── OnGUI() - 用于绘制属性的GUI界面，通常在此方法中调用绘制控件的代码
│   │   │   └── GetPropertyHeight() - 返回属性在Inspector中所需的高度，通常用于动态调整属性的显示空间
│   │   ├── 特性列表
│   │   │   ├── [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)] // 指定该特性可以应用于类、结构、属性和字段，并支持继承
│   │   │   ├── [Range(4f, 10f)] - 限制数值在4到10之间的范围，确保用户输入的值在此范围内
│   │   │   ├── [Min(4f)] - 限制数值最小为4，防止输入低于此值
│   │   │   ├── [Multiline(5)] - 允许多行文本输入，显示5行，适用于长文本
│   │   │   ├── [TextArea] - 允许多行文本输入，显示可调整的文本区域，适合用户输入较长的文本
│   │   │   ├── [ColorUsage(true, true)] - 允许选择颜色并支持HDR，适用于需要颜色选择的属性
│   │   │   ├── [GradientUsage(false, ColorSpace.Gamma)] - 自定义渐变属性，禁用HDR，适合普通颜色渐变
│   │   │   ├── [Space(10f)] - 在属性之间添加10单位的空间，改善Inspector的可读性
│   │   │   ├── [Header("***相关变量")] - 添加标题以分隔属性，帮助用户理解属性的分组
│   │   │   ├── [Time] - 自定义时间属性，适用于需要时间输入的场景
│   │   └── 应用场景
│   │       ├── 自定义特性绘制 -- 为特定属性类型提供自定义的显示方式
│   │       └── 自定义字段类型绘制 -- 为特定字段类型提供自定义的显示方式
│   │
│   └── DecoratorDrawer - 用于装饰属性的绘制器，提供了在Inspector中添加标题、分隔线等功能，提高了Inspector的布局和可读性。
│       ├── 基本用法
│       │   └── [CustomPropertyDrawer(typeof(DecoratorAttributeType))] -- 为装饰属性创建自定义绘制器
│       └── 应用场景
│           └── 装饰属性（添加标题、分隔线等） -- 用于改善Inspector的布局和可读性
│
├── 核心API和类
│   ├── SerializedObject
│   │   ├── 创建和更新
│   │   │   ├── 构造函数 -- 用于创建SerializedObject实例
│   │   │   └── Update() -- 更新SerializedObject的状态，确保与目标对象同步
│   │   ├── 查找属性
│   │   │   └── FindProperty() -- 查找SerializedProperty，允许访问目标对象的属性
│   │   └── 应用修改
│   │       ├── ApplyModifiedProperties() -- 应用对SerializedObject的修改，保存更改
│   │       └── ApplyModifiedPropertiesWithoutUndo() -- 应用修改但不记录撤销操作
│   │
│   ├── SerializedProperty
│   │   ├── 属性类型
│   │   │   └── propertyType -- 获取属性的类型，帮助判断如何处理属性
│   │   ├── 属性值访问
│   │   │   ├── 基本类型 (intValue, floatValue, etc.) -- 访问基本数据类型的值
│   │   │   ├── objectReferenceValue -- 访问对象引用类型的值
│   │   │   └── 数组和复合类型 -- 处理数组和复合类型的属性
│   │   └── 迭代子属性
│   │       └── GetEnumerator() -- 迭代SerializedProperty的子属性，便于处理复杂数据结构
│   │
│   └── EditorGUI / EditorGUILayout
│       ├── 基本控件
│       │   ├── PropertyField() -- 绘制SerializedProperty的控件
│       │   ├── 输入字段 (IntField, TextField, etc.) -- 提供多种输入控件
│       │   └── ObjectField() -- 用于选择对象的控件
│       ├── 特殊控件
│       │   ├── Foldout() -- 可折叠的控件，适合分组显示
│       │   ├── Popup() -- 弹出选择框，适合选择项较多的情况
│       │   └── HelpBox() -- 显示帮助信息的控件，提供用户指导
│       └── 布局控制
│           ├── 区域绘制 (Rect) -- 控制绘制区域的大小和位置
│           └── 自动布局 (GUILayout) -- 自动管理控件的布局，简化UI设计
│
├── 编辑器功能扩展
│   ├── 撤销系统
│   │   ├── Undo.RecordObject() -- 记录对象的修改，以便撤销
│   │   ├── Undo.RegisterCreatedObjectUndo() -- 注册新创建对象的撤销操作
│   │   └── Undo.DestroyObjectImmediate() -- 立即销毁对象并注册撤销操作
│   │
│   ├── 预览系统
│   │   ├── HasPreviewGUI() -- 检查是否支持预览GUI
│   │   ├── GetPreviewTitle() -- 获取预览窗口的标题
│   │   ├── OnPreviewSettings() -- 处理预览的设置
│   │   └── OnPreviewGUI() -- 绘制预览内容
│   │
│   └── 变更检测
│       ├── EditorGUI.BeginChangeCheck() -- 开始检测用户输入的变化
│       ├── EditorGUI.EndChangeCheck() -- 结束变化检测并返回结果
│       └── GUI.changed -- 检查是否有GUI状态变化
│
└── 实际应用示例
    ├── 基础组件编辑器
    │   ├── TransformEditor -- 自定义Transform组件的编辑器
    │   └── RectTransformEditor -- 自定义RectTransform组件的编辑器
    ├── 自定义组件编辑器
    │   └── CustomComponentEditor -- 自定义组件的编辑器实现
    ├── 属性绘制器
    │   ├── TimePropertyDrawer -- 自定义时间属性的绘制器
    │   ├── SpritePropertyDrawer -- 自定义精灵属性的绘制器
    │   └── ColorPropertyDrawer -- 自定义颜色属性的绘制器
    └── 复杂编辑器
        └── TransformTweenAnimationEditor -- 复杂动画编辑器的实现
```

## UML类图

```
+----------------+        +----------------+        +----------------+
|     Editor     |        | PropertyDrawer |        | DecoratorDrawer|
+----------------+        +----------------+        +----------------+
| + target       |        | + attribute    |        | + attribute    |
| + targets      |        | + fieldInfo    |        +----------------+
| + serializedObj|        +----------------+        | + OnGUI()      |
+----------------+        | + OnGUI()      |        | + GetHeight()  |
| + OnEnable()   |        | + GetHeight()  |        +----------------+
| + OnDisable()  |        +----------------+
| + OnInspectorGUI() |            ^
| + HasPreviewGUI()  |            |
| + OnPreviewGUI()   |    +----------------+
+----------------+        | CustomAttribute |
        ^                 +----------------+
        |                 | (标记特性类)    |
+----------------+        +----------------+
| CustomEditor   |
+----------------+
| + OnInspectorGUI() |
| (自定义实现)     |
+----------------+
```

## 重要类和接口

### 基础类

| 类名 | 功能 | 主要方法 | 用途 |
|------|------|----------|------|
| Editor | 编辑器基类 | OnInspectorGUI(), OnEnable(), OnDisable() | 所有自定义编辑器的基类 |
| PropertyDrawer | 属性绘制器基类 | OnGUI(), GetPropertyHeight() | 自定义属性绘制器的基类 |
| SerializedObject | 序列化对象 | FindProperty(), ApplyModifiedProperties() | 访问和修改目标对象的属性 |
| SerializedProperty | 序列化属性 | 各种类型值访问器 (intValue, floatValue等) | 读取和修改特定属性值 |

### 特性类

| 特性名称 | 应用目标 | 功能描述 |
|----------|----------|----------|
| CustomEditor | 编辑器类 | 将编辑器类与目标组件类型关联 |
| CustomPropertyDrawer | 属性绘制器类 | 将属性绘制器与目标特性或字段类型关联 |
| SerializeField | 字段 | 标记私有字段使其可在Inspector中显示和编辑 |

### 实用API

| API | 功能 | 示例用法 |
|-----|------|----------|
| Undo | 撤销/重做系统 | Undo.RecordObject(target, "Change Value") |
| EditorGUI / EditorGUILayout | 编辑器GUI绘制 | EditorGUILayout.PropertyField() |
| EditorUtility | 编辑器实用方法 | EditorUtility.SetDirty() |
| GUIUtility | GUI实用方法 | GUIUtility.systemCopyBuffer |

## 示例类解析

### 1. TransformEditor

TransformEditor示例展示了如何扩展Unity内置Transform组件的Inspector界面，在保留原有功能的基础上添加新功能。主要特点：

- 使用反射获取原始TransformInspector
- 添加"Copy Full Path"按钮获取对象的完整层级路径
- 展示如何与内置编辑器协同工作

### 2. PropertyDrawer示例

PropertyDrawer示例展示了如何为特定属性创建自定义绘制方式：

- TimePropertyDrawer：将浮点数转换为时间格式(HH:MM:SS)显示
- SpritePropertyDrawer：自定义Sprite属性的显示方式
- ColorPropertyDrawer：为颜色提供更丰富的编辑选项

### 3. CustomComponentEditor

CustomComponentEditor示例展示了为自定义组件创建Inspector界面的完整流程：

- 访问并修改组件的公共和私有字段
- 使用SerializedProperty和反射两种方式处理属性
- 演示GUI变更检测和撤销系统的使用
- 实现自定义预览功能

### 4. TransformTweenAnimationEditor

TransformTweenAnimationEditor是一个复杂的示例，展示如何创建动画编辑器：

- 支持移动、旋转、缩放三种动画类型
- 提供可视化编辑界面
- 实现实时预览功能
- 展示自定义Inspector的高级应用

## 应用场景

1. **提升开发效率**：为常用组件创建更高效的编辑界面
2. **简化复杂数据编辑**：为复杂数据类型提供直观的编辑方式
3. **减少错误**：通过自定义验证和约束，减少错误输入
4. **功能扩展**：为内置组件添加新功能
5. **工作流优化**：创建符合特定项目需求的编辑器工具

## 最佳实践

1. **保持简洁直观**：设计清晰、直观的用户界面
2. **正确处理撤销/重做**：使用Undo API确保操作可以撤销
3. **多对象编辑支持**：确保编辑器支持同时编辑多个对象
4. **性能考虑**：避免在OnGUI中执行耗时操作
5. **正确应用修改**：使用ApplyModifiedProperties确保更改被保存
6. **符合Unity风格**：尽量遵循Unity原有的设计风格和交互模式

## 相关资源

### 官方文档

- [Unity Documentation - Custom Editors](https://docs.unity3d.com/Manual/editor-CustomEditors.html)
- [Unity Documentation - Property Drawers](https://docs.unity3d.com/Manual/editor-PropertyDrawers.html)
- [Unity Documentation - Editor Class](https://docs.unity3d.com/ScriptReference/Editor.html)
- [Unity Documentation - PropertyDrawer Class](https://docs.unity3d.com/ScriptReference/PropertyDrawer.html)
- [Unity Documentation - SerializedObject](https://docs.unity3d.com/ScriptReference/SerializedObject.html)
- [Unity Documentation - SerializedProperty](https://docs.unity3d.com/ScriptReference/SerializedProperty.html)
- [Unity Documentation - EditorGUI](https://docs.unity3d.com/ScriptReference/EditorGUI.html)
- [Unity Documentation - EditorGUILayout](https://docs.unity3d.com/ScriptReference/EditorGUILayout.html)
- [Unity Documentation - Undo](https://docs.unity3d.com/ScriptReference/Undo.html)

### 教程与参考

- [Unity Learn - Editor Scripting](https://learn.unity.com/tutorial/editor-scripting)
- [Unity Manual - Extending the Editor](https://docs.unity3d.com/Manual/ExtendingTheEditor.html)
- [Unity Blog - Editor Scripting](https://blog.unity.com/technology/editor-scripting) 