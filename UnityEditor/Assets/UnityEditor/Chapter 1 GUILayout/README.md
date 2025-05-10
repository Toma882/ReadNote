# Unity编辑器扩展 - Chapter 1: GUILayout

## 概述

本章专注于Unity编辑器扩展中GUILayout系统的使用，GUILayout是Unity编辑器开发中用于创建自定义界面的核心技术。通过示例代码展示了各种UI控件的创建方法、布局技巧以及样式定制。

## 核心知识点

### GUILayout与EditorGUILayout

- **GUILayout**: 用于在GUI布局中创建控件的函数系列
- **EditorGUILayout**: 是GUILayout的子类，专为编辑器扩展设计，提供更多编辑器专用控件
- **两者关系**: 可以混用，EditorGUILayout是GUILayout的扩展，针对编辑器环境优化


### 相关特征
#### CustomEditor特性

- **CustomEditor**: 这是一个用于自定义Unity Inspector面板的特性。通过在类上使用`[CustomEditor(typeof(YourComponent))]`，可以指定该编辑器类用于哪个组件类型的Inspector面板。
- **用途**: 允许开发者为特定的组件类型创建自定义的Inspector界面，以便更好地展示和编辑组件的属性。
- **示例**: 在`ExampleEditor.cs`中，`ExampleEditor`类使用了`[CustomEditor(typeof(Example))]`特性，表示该编辑器类将用于`Example`组件的Inspector面板。

#### MenuItem特性

- **MenuItem**: 这是一个用于在Unity菜单中添加自定义菜单项的特性。通过在方法上使用`[MenuItem("Menu/ItemName")]`，可以将该方法注册为菜单项。
- **用途**: 允许开发者在Unity的菜单栏中添加自定义功能，方便用户通过菜单直接访问特定的功能或工具。
- **示例**: 在`ExampleEditorWindow.cs`中，使用`[MenuItem("Example/Open Example Editor Window")]`特性来创建一个菜单项，点击后会打开自定义的编辑器窗口。

### 代码结构

本章包含两个主要示例类:

1. **ExampleEditor.cs**: 继承自Editor，演示了Inspector面板的自定义界面
2. **ExampleEditorWindow.cs**: 继承自EditorWindow，演示了如何创建自定义编辑器窗口

## UI控件示例

### 基础控件

1. **标签 (Label)**
   - 普通标签、迷你标签、大标签、粗体标签等
   - 自定义样式（对齐方式、字体大小等）

2. **按钮 (Button)**
   - 普通按钮、迷你按钮、单选按钮、工具栏按钮
   - 组合按钮（左中右三种样式）

3. **开关 (Toggle)**
   - GUILayout.Toggle - 基础开关控件
   - EditorGUILayout.Toggle - 带标签的开关控件

4. **输入框 (InputField)**
   - 文本输入(TextField)
   - 数值输入(FloatField, IntField, LongField)
   - 密码输入(PasswordField)
   - 向量输入(Vector2Field, Vector3Field, Vector4Field)

5. **下拉菜单 (Dropdown)**
   - 枚举下拉(EnumPopup)
   - 标签选择(TagField)
   - 层级选择(LayerField)

6. **滑动条 (Slider)**
   - 整数滑动条(IntSlider)
   - 浮点数滑动条(Slider)

7. **折叠面板 (Foldout)**
   - 可折叠的内容组

### 布局系统

1. **基础布局**
   - 水平布局(BeginHorizontal/EndHorizontal)
   - 垂直布局(BeginVertical/EndVertical)
   - 嵌套布局

2. **布局选项**
   - 宽度设置(Width)
   - 高度设置(Height)
   - 扩展设置(ExpandWidth/ExpandHeight)

3. **间距控制**
   - 固定间距(Space)
   - 自适应间距(FlexibleSpace)

4. **滚动视图**
   - BeginScrollView/EndScrollView
   - 滚动位置控制

5. **分割窗口**
   - 可拖拽分割线
   - 事件处理

## 思维导图

```
Unity GUILayout系统
├── 基础概念
│   ├── GUILayout
│   │   └── 通用GUI布局控件
│   └── EditorGUILayout
│       └── 编辑器专用布局控件
│
├── 控件类型
│   ├── 标签(Label)
│   │   ├── 样式变体
│   │   │   ├── 普通标签(label)
│   │   │   ├── 迷你标签(miniLabel)
│   │   │   ├── 大标签(largeLabel)
│   │   │   ├── 粗体标签(boldLabel)
│   │   │   └── ...
│   │   └── 自定义样式
│   │       ├── 对齐方式(alignment)
│   │       └── 字体大小(fontSize)
│   │
│   ├── 按钮(Button)
│   │   ├── 普通按钮
│   │   ├── 迷你按钮(miniButton)
│   │   ├── 单选按钮(radioButton)
│   │   └── 组合按钮
│   │       ├── 左侧按钮(miniButtonLeft)
│   │       ├── 中间按钮(miniButtonMid)
│   │       └── 右侧按钮(miniButtonRight)
│   │
│   ├── 开关(Toggle)
│   │   ├── GUILayout.Toggle
│   │   └── EditorGUILayout.Toggle
│   │
│   ├── 输入框
│   │   ├── 文本输入(TextField)
│   │   ├── 数值输入
│   │   │   ├── 整数(IntField)
│   │   │   ├── 浮点数(FloatField)
│   │   │   └── 长整数(LongField)
│   │   ├── 密码输入(PasswordField)
│   │   └── 向量输入
│   │       ├── Vector2Field
│   │       ├── Vector3Field
│   │       └── Vector4Field
│   │
│   ├── 下拉菜单
│   │   ├── 枚举下拉(EnumPopup)
│   │   ├── 标签选择(TagField)
│   │   └── 层级选择(LayerField)
│   │
│   ├── 滑动条
│   │   ├── 整数滑动条(IntSlider)
│   │   └── 浮点数滑动条(Slider)
│   │
│   └── 折叠面板(Foldout)
│
├── 布局系统
│   ├── 基础布局
│   │   ├── 水平布局(BeginHorizontal/EndHorizontal)
│   │   ├── 垂直布局(BeginVertical/EndVertical)
│   │   └── 嵌套布局
│   │
│   ├── 布局选项
│   │   ├── 宽度(Width)
│   │   ├── 高度(Height)
│   │   └── 扩展(ExpandWidth/ExpandHeight)
│   │
│   ├── 间距控制
│   │   ├── 固定间距(Space)
│   │   └── 自适应间距(FlexibleSpace)
│   │
│   └── 高级布局
│       ├── 滚动视图(ScrollView)
│       └── 分割窗口
│           └── 可拖拽分割线
│
└── 实现方式
    ├── Editor扩展
    │   ├── CustomEditor特性
    │   └── OnInspectorGUI方法
    └── EditorWindow
        ├── MenuItem特性
        └── OnGUI方法
```

## 应用场景

1. **自定义Inspector面板**: 通过继承Editor类并重写OnInspectorGUI方法
2. **自定义编辑器窗口**: 通过继承EditorWindow类并实现OnGUI方法
3. **编辑器工具开发**: 为项目创建特定的编辑器工具
4. **属性绘制器**: 为特定属性类型创建自定义UI

## 代码关系

1. ExampleEditor.cs展示了如何通过CustomEditor特性修饰类并继承Editor，以自定义组件在Inspector中的显示方式
2. ExampleEditorWindow.cs展示了如何创建、打开和绘制自定义编辑器窗口

## 最佳实践

1. **合理组织布局**: 使用BeginHorizontal/EndHorizontal和BeginVertical/EndVertical嵌套创建复杂布局
2. **正确使用样式**: 利用EditorStyles提供的预定义样式，保持界面一致性
3. **控件选择**: 根据数据类型选择合适的控件(如枚举使用EnumPopup，布尔值使用Toggle)
4. **响应式设计**: 使用FlexibleSpace和ExpandWidth/ExpandHeight创建自适应界面
5. **事件处理**: 正确响应鼠标和键盘事件(如分割线拖拽)

## 相关资源

- [Unity官方文档 - GUILayout](https://docs.unity3d.com/ScriptReference/GUILayout.html)
- [Unity官方文档 - EditorGUILayout](https://docs.unity3d.com/ScriptReference/EditorGUILayout.html)
- [Unity官方文档 - Editor类](https://docs.unity3d.com/ScriptReference/Editor.html)
- [Unity官方文档 - EditorWindow类](https://docs.unity3d.com/ScriptReference/EditorWindow.html) 