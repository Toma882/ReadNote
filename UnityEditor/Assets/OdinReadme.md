# Odin Inspector 使用指南

Odin Inspector是Unity编辑器的一个强大扩展插件，它提供了丰富的特性来增强Unity编辑器的功能，使开发者能够创建更加强大和用户友好的自定义编辑器。

## 目录

- [核心功能](#核心功能)
- [重要类和接口](#重要类和接口)
- [特性（Attributes）详解](#特性attributes详解)
- [核心架构](#核心架构)
- [使用示例](#使用示例)
- [思维导图](#思维导图)

## 核心功能

- **强大的Inspector自定义**：使用特性（Attributes）轻松自定义Inspector视图
- **自动序列化**：支持Unity原生不支持的类型序列化
- **编辑器窗口扩展**：创建复杂的自定义编辑器窗口
- **属性处理器**：自定义属性的处理方式
- **自定义绘制器**：创建自定义的属性绘制方式
- **验证系统**：为属性添加验证规则

## 重要类和接口

### 核心类

| 类名 | 命名空间 | 描述 | 重要方法 |
|------|----------|------|----------|
| `OdinEditor` | `Sirenix.OdinInspector.Editor` | Odin编辑器的基类，用于创建自定义编辑器 | `OdinEditor.DrawEditor(Object)` |
| `OdinEditorWindow` | `Sirenix.OdinInspector.Editor` | 创建自定义编辑器窗口的基类 | `OdinEditorWindow.DrawEditor(Object)`, `OdinEditorWindow.GetTarget()` |
| `OdinMenuEditorWindow` | `Sirenix.OdinInspector.Editor` | 带菜单的编辑器窗口基类 | `OdinMenuEditorWindow.BuildMenuTree()`, `OdinMenuEditorWindow.DrawMenu()` |
| `OdinSelector<T>` | `Sirenix.OdinInspector.Editor` | 创建自定义选择器的基类 | `OdinSelector<T>.SelectionConfirmed`, `OdinSelector<T>.DrawSelectionTree()` |
| `SerializedMonoBehaviour` | `Sirenix.OdinInspector` | 支持Odin序列化的MonoBehaviour | - |
| `SerializedScriptableObject` | `Sirenix.OdinInspector` | 支持Odin序列化的ScriptableObject | - |
| `GlobalConfig<T>` | `Sirenix.OdinInspector` | 全局配置单例的基类 | `GlobalConfig<T>.Instance` |
| `PropertyTree` | `Sirenix.OdinInspector.Editor` | 表示Inspector属性树的类 | `PropertyTree.Draw()`, `PropertyTree.GetPropertyAtPath()`, `PropertyTree.GetPropertyAtIndex()` |
| `InspectorProperty` | `Sirenix.OdinInspector.Editor` | 表示Inspector中的单个属性 | `InspectorProperty.Draw()`, `InspectorProperty.Children`, `InspectorProperty.ValueEntry` |
| `OdinMenuTree` | `Sirenix.OdinInspector.Editor` | 菜单树，用于构建编辑器窗口菜单 | `OdinMenuTree.AddItem()`, `OdinMenuTree.AddAllAssetsAtPath()`, `OdinMenuTree.Selection` |
| `OdinMenuItem` | `Sirenix.OdinInspector.Editor` | 表示菜单树中的单个菜单项 | `OdinMenuItem.ChildMenuItems`, `OdinMenuItem.Selected` |
| `TypeInfoBox` | `Sirenix.OdinInspector.Editor` | 用于在编辑器中显示类型信息的工具类 | `TypeInfoBox.DrawInfoBox()` |
| `SirenixEditorGUI` | `Sirenix.Utilities.Editor` | 提供扩展的编辑器GUI功能 | `SirenixEditorGUI.BeginBox()`, `SirenixEditorGUI.DrawSolidRect()` |

### 重要接口

| 接口名 | 命名空间 | 描述 | 重要方法 |
|--------|----------|------|----------|
| `IAttrbuteProcessor` | `Sirenix.OdinInspector.Editor` | 属性处理器接口 | `IAttrbuteProcessor.ProcessChildMemberAttributes()` |
| `IValueDropdownItem` | `Sirenix.OdinInspector` | 下拉值项接口 | `IValueDropdownItem.GetText()`, `IValueDropdownItem.GetValue()` |
| `ISearchFilterable` | `Sirenix.OdinInspector` | 搜索过滤接口 | `ISearchFilterable.IsMatch()` |
| `IPropertyValueEntry` | `Sirenix.OdinInspector.Editor` | 属性值条目接口 | `IPropertyValueEntry.SmartValue`, `IPropertyValueEntry.WeakSmartValue` |
| `IAttributeValidator` | `Sirenix.OdinInspector.Editor` | 属性验证器接口 | `IAttributeValidator.Validate()` |
| `IHideObjectMenuItems` | `Sirenix.OdinInspector.Editor` | 隐藏对象菜单项的接口 | - |
| `IDrawnWithUnityLayout` | `Sirenix.OdinInspector.Editor` | 使用Unity布局绘制的接口 | - |
| `IValueResolver` | `Sirenix.OdinInspector.Editor` | 值解析器接口 | `IValueResolver.TryGetValue()` |
| `IPathRedirector` | `Sirenix.OdinInspector.Editor` | 路径重定向接口 | `IPathRedirector.RedirectPath()` |

### 属性处理器类

| 类名 | 命名空间 | 描述 | 重要方法 |
|------|----------|------|----------|
| `OdinAttributeProcessor<T>` | `Sirenix.OdinInspector.Editor` | 属性处理器基类，用于处理特定类型的属性 | `OdinAttributeProcessor<T>.ProcessChildMemberAttributes()`, `OdinAttributeProcessor<T>.ProcessMemberAttributes()` |
| `AttributeProcessorLocator` | `Sirenix.OdinInspector.Editor` | 属性处理器定位器，用于查找适用的处理器 | `AttributeProcessorLocator.GetProcessors()` |
| `DefaultOdinAttributeProcessor` | `Sirenix.OdinInspector.Editor` | 默认属性处理器 | - |
| `OdinAttributeProcessorLocator` | `Sirenix.OdinInspector.Editor` | Odin属性处理器定位器 | `OdinAttributeProcessorLocator.GetChildProcessors()` |

### 自定义绘制器类

| 类名 | 命名空间 | 描述 | 重要方法 |
|------|----------|------|----------|
| `OdinAttributeDrawer<T>` | `Sirenix.OdinInspector.Editor` | 属性绘制器基类，用于绘制带特定特性的属性 | `OdinAttributeDrawer<T>.DrawPropertyLayout()`, `OdinAttributeDrawer<T>.CallNextDrawer()` |
| `OdinAttributeDrawer<T, U>` | `Sirenix.OdinInspector.Editor` | 泛型属性绘制器基类，用于绘制带特定特性的特定类型属性 | `OdinAttributeDrawer<T, U>.DrawPropertyLayout()`, `OdinAttributeDrawer<T, U>.ValueEntry` |
| `OdinValueDrawer<T>` | `Sirenix.OdinInspector.Editor` | 值绘制器基类，用于绘制特定类型的值 | `OdinValueDrawer<T>.DrawPropertyLayout()`, `OdinValueDrawer<T>.ValueEntry` |
| `OdinGroupDrawer<T>` | `Sirenix.OdinInspector.Editor` | 组绘制器基类，用于绘制属性组 | `OdinGroupDrawer<T>.DrawPropertyLayout()`, `OdinGroupDrawer<T>.GroupAttribute` |
| `InspectorPropertyDrawer` | `Sirenix.OdinInspector.Editor` | Inspector属性绘制器基类 | `InspectorPropertyDrawer.CanDrawProperty()`, `InspectorPropertyDrawer.DrawProperty()` |
| `DrawerPriority` | `Sirenix.OdinInspector.Editor` | 绘制器优先级特性，用于控制绘制器的执行顺序 | - |
| `DrawerLocator` | `Sirenix.OdinInspector.Editor` | 绘制器定位器，用于查找适用的绘制器 | `DrawerLocator.GetDrawers()` |

### 值提供器和解析器

| 类名 | 命名空间 | 描述 | 重要方法 |
|------|----------|------|----------|
| `ValueResolver<T>` | `Sirenix.OdinInspector.Editor` | 值解析器，用于解析表达式或成员引用 | `ValueResolver<T>.GetValue()`, `ValueResolver<T>.TryGetValue()` |
| `ActionResolver` | `Sirenix.OdinInspector.Editor` | 动作解析器，用于解析方法调用 | `ActionResolver.DoAction()`, `ActionResolver.TryDoAction()` |
| `ValueProvider<T>` | `Sirenix.OdinInspector.Editor` | 值提供器，用于提供值 | `ValueProvider<T>.GetValue()` |
| `PropertyValueEntry<T>` | `Sirenix.OdinInspector.Editor` | 属性值条目，用于访问和修改属性值 | `PropertyValueEntry<T>.SmartValue`, `PropertyValueEntry<T>.Values` |

## 特性（Attributes）详解

Odin Inspector的核心功能是通过特性（Attributes）来实现的。以下是一些最常用和最重要的特性：

### 布局特性

| 特性 | 描述 | 示例 |
|------|------|------|
| `[BoxGroup]` | 在盒子中组织属性 | `[BoxGroup("Settings")]` |
| `[TabGroup]` | 创建选项卡组 | `[TabGroup("Tabs", "General")]` |
| `[HorizontalGroup]` | 水平排列属性 | `[HorizontalGroup("Split")]` |
| `[VerticalGroup]` | 垂直排列属性 | `[VerticalGroup("Left")]` |
| `[FoldoutGroup]` | 创建可折叠组 | `[FoldoutGroup("Advanced")]` |
| `[ResponsiveButtonGroup]` | 响应式按钮组 | `[ResponsiveButtonGroup]` |
| `[PropertyOrder]` | 设置属性顺序 | `[PropertyOrder(-1)]` |
| `[Title]` | 添加标题 | `[Title("Player Settings")]` |
| `[TitleGroup]` | 带标题的组 | `[TitleGroup("Stats")]` |

### 显示特性

| 特性 | 描述 | 示例 |
|------|------|------|
| `[LabelText]` | 自定义标签文本 | `[LabelText("玩家名称")]` |
| `[GUIColor]` | 设置GUI颜色 | `[GUIColor(1, 0, 0)]` |
| `[HideLabel]` | 隐藏属性标签 | `[HideLabel]` |
| `[HideInInspector]` | 在Inspector中隐藏属性 | `[HideInInspector]` |
| `[ShowInInspector]` | 在Inspector中显示属性 | `[ShowInInspector]` |
| `[ReadOnly]` | 使属性只读 | `[ReadOnly]` |
| `[ShowIf]` | 条件性显示属性 | `[ShowIf("IsEnabled")]` |
| `[HideIf]` | 条件性隐藏属性 | `[HideIf("IsDisabled")]` |
| `[EnableIf]` | 条件性启用属性 | `[EnableIf("CanEdit")]` |
| `[DisableIf]` | 条件性禁用属性 | `[DisableIf("IsLocked")]` |
| `[InfoBox]` | 显示信息框 | `[InfoBox("重要提示")]` |
| `[DetailedInfoBox]` | 显示详细信息框 | `[DetailedInfoBox("标题", "详情")]` |
| `[PreviewField]` | 显示预览字段 | `[PreviewField]` |
| `[ProgressBar]` | 显示进度条 | `[ProgressBar(0, 100)]` |
| `[TableList]` | 表格形式显示列表 | `[TableList]` |

### 功能特性

| 特性 | 描述 | 示例 |
|------|------|------|
| `[Button]` | 创建按钮 | `[Button]` |
| `[OnValueChanged]` | 值变化时触发 | `[OnValueChanged("OnNameChanged")]` |
| `[OnInspectorGUI]` | 自定义Inspector GUI | `[OnInspectorGUI]` |
| `[ValidateInput]` | 验证输入 | `[ValidateInput("IsValidName")]` |
| `[Required]` | 标记必填字段 | `[Required]` |
| `[ValueDropdown]` | 创建下拉选择 | `[ValueDropdown("GetOptions")]` |
| `[ListDrawerSettings]` | 自定义列表绘制 | `[ListDrawerSettings(ShowIndexLabels = true)]` |
| `[InlineEditor]` | 内联编辑对象 | `[InlineEditor]` |
| `[InlineProperty]` | 内联显示属性 | `[InlineProperty]` |
| `[TypeFilter]` | 类型过滤 | `[TypeFilter("GetFilteredTypeList")]` |
| `[AssetSelector]` | 资源选择器 | `[AssetSelector]` |
| `[FilePath]` | 文件路径选择 | `[FilePath(Extensions = "cs")]` |
| `[FolderPath]` | 文件夹路径选择 | `[FolderPath]` |

### 自定义序列化特性

| 特性 | 描述 | 示例 |
|------|------|------|
| `[OdinSerialize]` | 标记使用Odin序列化 | `[OdinSerialize]` |
| `[NonSerialized]` | 标记不序列化 | `[NonSerialized]` |

## 核心架构

Odin Inspector的核心架构由以下几个部分组成：

1. **特性系统（Attribute System）**：提供各种特性来自定义Inspector
2. **绘制系统（Drawing System）**：负责在Inspector中绘制属性
3. **序列化系统（Serialization System）**：处理对象的序列化和反序列化
4. **属性树系统（Property Tree System）**：管理属性的层次结构
5. **处理器系统（Processor System）**：处理属性的特性和修改其行为
6. **验证系统（Validation System）**：验证属性值的有效性

### UML类图（核心部分）

```
+-------------------------+     +-------------------------+
|    OdinEditor           |     |    OdinEditorWindow     |
+-------------------------+     +-------------------------+
| - DrawEditor(Object)    |     | - GetTarget()           |
| - CreatePropertyTree()  |     | - DrawEditor()          |
+----------+--------------+     +------------+------------+
           |                                |
           v                                v
+----------+--------------+     +------------+------------+
|    PropertyTree         |     |    OdinMenuTree         |
+-------------------------+     +-------------------------+
| - Draw()                |     | - AddItem()             |
| - GetPropertyAtPath()   |     | - Selection             |
| - GetPropertyAtIndex()  |     | - EnumerateTree()       |
+----------+--------------+     +-------------------------+
           |                                ^
           v                                |
+----------+--------------+                 |
|  InspectorProperty      |                 |
+-------------------------+                 |
| - Draw()                |                 |
| - Children              |                 |
| - ValueEntry            |                 |
+----------+--------------+                 |
           |                                |
           v                                v
+----------+--------------+     +-----------+-------------+
| OdinAttributeProcessor  |     |    OdinMenuItem         |
+-------------------------+     +-------------------------+
| - ProcessAttributes()   |     | - Selected              |
| - ProcessMemberAttr()   |     | - ChildMenuItems        |
+----------+--------------+     +-------------------------+
           |
           v
+----------+--------------+     +-------------------------+
| OdinAttributeDrawer<T>  |     |   OdinValueDrawer<T>    |
+-------------------------+     +-------------------------+
| - DrawPropertyLayout()  |     | - DrawPropertyLayout()  |
| - CallNextDrawer()      |     | - ValueEntry            |
+-------------------------+     +-------------------------+
           |                                |
           v                                v
+----------+--------------+     +-------------------------+
| DrawerChain             |     |   PropertyValueEntry<T> |
+-------------------------+     +-------------------------+
| - DrawProperty()        |     | - SmartValue            |
| - GetDrawers()          |     | - Values                |
+-------------------------+     +-------------------------+
           |                                |
           v                                v
+----------+--------------+     +-------------------------+
| DrawerLocator           |     |   ValueResolver<T>      |
+-------------------------+     +-------------------------+
| - GetDrawers()          |     | - GetValue()            |
+-------------------------+     | - TryGetValue()         |
                                +-------------------------+
```

## 使用示例

### 基本使用

```csharp
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Title("基本信息")]
    [LabelText("玩家名称")]
    public string playerName;

    [ProgressBar(0, 100)]
    [LabelText("生命值")]
    public float health = 100;

    [BoxGroup("属性")]
    [LabelText("力量"), Range(1, 100)]
    public int strength = 10;

    [BoxGroup("属性")]
    [LabelText("敏捷"), Range(1, 100)]
    public int agility = 10;

    [BoxGroup("属性")]
    [LabelText("智力"), Range(1, 100)]
    public int intelligence = 10;

    [Button("重置属性")]
    private void ResetStats()
    {
        strength = 10;
        agility = 10;
        intelligence = 10;
    }
}
```

### 自定义绘制器

```csharp
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class HealthBarAttributeDrawer : OdinAttributeDrawer<HealthBarAttribute, float>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        // 调用下一个绘制器，它将绘制浮点字段
        this.CallNextDrawer(label);

        // 获取一个用于绘制生命值条的矩形
        Rect rect = EditorGUILayout.GetControlRect();

        // 绘制生命值条
        float width = Mathf.Clamp01(this.ValueEntry.SmartValue / this.Attribute.MaxHealth);
        EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.3f));
        EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width * width, rect.height), Color.red);
    }
}
#endif
```

### 自定义属性处理器

```csharp
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;

public class TabifyTypeProcessor<T> : OdinAttributeProcessor<T>
{
    public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
    {
        // 根据成员的声明类型将属性分组到不同的选项卡中
        var tabName = member.DeclaringType.Name;
        attributes.Add(new TabGroupAttribute("类型", tabName));
    }
}
#endif
```

## 思维导图

```
Odin Inspector
├── 核心功能
│   ├── Inspector自定义 - 使用特性轻松自定义Inspector视图
│   ├── 自动序列化 - 支持Unity原生不支持的类型序列化
│   ├── 编辑器窗口 - 创建复杂的自定义编辑器窗口
│   ├── 属性处理器 - 自定义属性的处理方式
│   ├── 自定义绘制器 - 创建自定义的属性绘制方式
│   └── 验证系统 - 为属性添加验证规则
│
├── 特性系统
│   ├── 布局特性 - 控制Inspector中属性的布局和组织
│   │   ├── BoxGroup - 在盒子中组织属性
│   │   ├── TabGroup - 创建选项卡组
│   │   ├── HorizontalGroup - 水平排列属性
│   │   ├── VerticalGroup - 垂直排列属性
│   │   ├── FoldoutGroup - 创建可折叠组
│   │   ├── PropertyOrder - 设置属性顺序
│   │   └── TitleGroup - 带标题的组
│   │
│   ├── 显示特性 - 控制属性在Inspector中的显示方式
│   │   ├── LabelText - 自定义标签文本
│   │   ├── GUIColor - 设置GUI颜色
│   │   ├── ShowIf/HideIf - 条件性显示或隐藏属性
│   │   ├── EnableIf/DisableIf - 条件性启用或禁用属性
│   │   ├── InfoBox - 显示信息框
│   │   ├── PreviewField - 显示预览字段
│   │   └── ProgressBar - 显示进度条
│   │
│   └── 功能特性 - 为Inspector添加功能
│       ├── Button - 创建按钮
│       ├── OnValueChanged - 值变化时触发
│       ├── ValidateInput - 验证输入
│       ├── Required - 标记必填字段
│       ├── ValueDropdown - 创建下拉选择
│       ├── InlineEditor - 内联编辑对象
│       ├── AssetSelector - 资源选择器
│       └── FilePath/FolderPath - 路径选择
│
├── 绘制系统
│   ├── OdinAttributeDrawer<T> - 属性绘制器基类
│   │   ├── DrawPropertyLayout() - 绘制属性布局
│   │   ├── CallNextDrawer() - 调用下一个绘制器
│   │   └── CanDrawProperty() - 检查是否可以绘制属性
│   │
│   ├── OdinValueDrawer<T> - 值绘制器基类
│   │   ├── DrawPropertyLayout() - 绘制属性布局
│   │   └── ValueEntry - 访问属性值
│   │
│   ├── OdinGroupDrawer<T> - 组绘制器基类
│   │   ├── DrawPropertyLayout() - 绘制属性布局
│   │   └── GroupAttribute - 访问组特性
│   │
│   ├── DrawerLocator - 绘制器定位器
│   │   └── GetDrawers() - 获取适用的绘制器
│   │
│   └── DrawerChain - 绘制器链
│       └── DrawProperty() - 绘制属性
│
├── 序列化系统
│   ├── SerializedMonoBehaviour - 支持Odin序列化的MonoBehaviour
│   ├── SerializedScriptableObject - 支持Odin序列化的ScriptableObject
│   ├── OdinSerialize特性 - 标记使用Odin序列化
│   └── 支持的类型 - 字典、队列、堆栈、复杂对象等
│
├── 属性树系统
│   ├── PropertyTree - 表示Inspector属性树的类
│   │   ├── Draw() - 绘制整个属性树
│   │   ├── GetPropertyAtPath() - 通过路径获取属性
│   │   └── GetPropertyAtIndex() - 通过索引获取属性
│   │
│   ├── InspectorProperty - 表示单个属性
│   │   ├── Draw() - 绘制属性
│   │   ├── Children - 子属性集合
│   │   └── ValueEntry - 属性值条目
│   │
│   └── PropertyValueEntry<T> - 属性值条目
│       ├── SmartValue - 获取或设置值
│       └── Values - 获取所有目标的值
│
├── 处理器系统
│   ├── OdinAttributeProcessor<T> - 属性处理器基类
│   │   ├── ProcessChildMemberAttributes() - 处理子成员特性
│   │   └── ProcessMemberAttributes() - 处理成员特性
│   │
│   ├── AttributeProcessorLocator - 属性处理器定位器
│   │   └── GetProcessors() - 获取适用的处理器
│   │
│   └── 处理器优先级 - 控制处理器执行顺序
│
├── 值解析系统
│   ├── ValueResolver<T> - 值解析器
│   │   ├── GetValue() - 获取值
│   │   └── TryGetValue() - 尝试获取值
│   │
│   ├── ActionResolver - 动作解析器
│   │   ├── DoAction() - 执行动作
│   │   └── TryDoAction() - 尝试执行动作
│   │
│   └── ValueProvider<T> - 值提供器
│       └── GetValue() - 获取值
│
└── 编辑器窗口系统
    ├── OdinEditorWindow - 创建自定义编辑器窗口的基类
    │   ├── DrawEditor() - 绘制编辑器
    │   └── GetTarget() - 获取目标对象
    │
    ├── OdinMenuEditorWindow - 带菜单的编辑器窗口基类
    │   ├── BuildMenuTree() - 构建菜单树
    │   └── DrawMenu() - 绘制菜单
    │
    ├── OdinMenuTree - 管理菜单项的树结构
    │   ├── AddItem() - 添加菜单项
    │   ├── AddAllAssetsAtPath() - 添加路径下的所有资源
    │   └── Selection - 当前选择的菜单项
    │
    └── OdinMenuItem - 表示单个菜单项
        ├── Selected - 是否被选中
        └── ChildMenuItems - 子菜单项集合
```
。 