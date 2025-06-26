# Odin Inspector 使用指南

Odin Inspector是Unity编辑器的一个强大扩展插件，它提供了丰富的特性来增强Unity编辑器的功能，使开发者能够创建更加强大和用户友好的自定义编辑器。

## 目录

- [核心功能](#核心功能)
- [重要类和接口](#重要类和接口)
- [特性（Attributes）详解](#特性attributes详解)
- [核心架构](#核心架构)
- [使用示例](#使用示例)
- [实例案例库](#实例案例库)
  - [自定义绘制器案例](#自定义绘制器案例)
  - [编辑器窗口案例](#编辑器窗口案例)
  - [属性处理器案例](#属性处理器案例)
  - [特性组合案例](#特性组合案例)
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

| 类名 | 命名空间 | 描述 | 重要方法 | 案例参考 |
|------|----------|------|----------|----------|
| `OdinEditor` | `Sirenix.OdinInspector.Editor` | Odin编辑器的基类，用于创建自定义编辑器 | `OdinEditor.DrawEditor(Object)` | - |
| `OdinEditorWindow` | `Sirenix.OdinInspector.Editor` | 创建自定义编辑器窗口的基类 | `OdinEditorWindow.DrawEditor(Object)`, `OdinEditorWindow.GetTarget()` | [BasicOdinEditorExampleWindow](#1-基础odin编辑器窗口---basicodineditorexamplewindow) |
| `OdinMenuEditorWindow` | `Sirenix.OdinInspector.Editor` | 带菜单的编辑器窗口基类 | `OdinMenuEditorWindow.BuildMenuTree()`, `OdinMenuEditorWindow.DrawMenu()` | [OdinMenuEditorWindowExample](#2-odin菜单编辑器窗口---odinmenueditorwindowexample) |
| `OdinSelector<T>` | `Sirenix.OdinInspector.Editor` | 创建自定义选择器的基类 | `OdinSelector<T>.SelectionConfirmed`, `OdinSelector<T>.DrawSelectionTree()` | - |
| `SerializedMonoBehaviour` | `Sirenix.OdinInspector` | 支持Odin序列化的MonoBehaviour | - | [SomeData.cs](Assets/Plugins/Sirenix/Demos/Editor%20Windows/Scripts/Editor/SomeData.cs) |
| `SerializedScriptableObject` | `Sirenix.OdinInspector` | 支持Odin序列化的ScriptableObject | - | - |
| `GlobalConfig<T>` | `Sirenix.OdinInspector` | 全局配置单例的基类 | `GlobalConfig<T>.Instance` | - |
| `PropertyTree` | `Sirenix.OdinInspector.Editor` | 表示Inspector属性树的类 | `PropertyTree.Draw()`, `PropertyTree.GetPropertyAtPath()`, `PropertyTree.GetPropertyAtIndex()` | [QuicklyInspectObjects](#4-快速检查对象---quicklyinspectobjects) |
| `InspectorProperty` | `Sirenix.OdinInspector.Editor` | 表示Inspector中的单个属性 | `InspectorProperty.Draw()`, `InspectorProperty.Children`, `InspectorProperty.ValueEntry` | [BasicAttributeProcessorExample](#1-基础属性处理器---basicattributeprocessorexample) |
| `OdinMenuTree` | `Sirenix.OdinInspector.Editor` | 菜单树，用于构建编辑器窗口菜单 | `OdinMenuTree.AddItem()`, `OdinMenuTree.AddAllAssetsAtPath()`, `OdinMenuTree.Selection` | [OdinMenuEditorWindowExample](#2-odin菜单编辑器窗口---odinmenueditorwindowexample) |
| `OdinMenuItem` | `Sirenix.OdinInspector.Editor` | 表示菜单树中的单个菜单项 | `OdinMenuItem.ChildMenuItems`, `OdinMenuItem.Selected` | [OdinMenuStyleExample](#3-菜单样式自定义---odinmenustyleexample) |
| `TypeInfoBox` | `Sirenix.OdinInspector.Editor` | 用于在编辑器中显示类型信息的工具类 | `TypeInfoBox.DrawInfoBox()` | [HealthBarExample](#1-健康条绘制器---healthbarexample) |
| `SirenixEditorGUI` | `Sirenix.Utilities.Editor` | 提供扩展的编辑器GUI功能 | `SirenixEditorGUI.BeginBox()`, `SirenixEditorGUI.DrawSolidRect()` | [HealthBarExample](#1-健康条绘制器---healthbarexample) |

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

| 类名 | 命名空间 | 描述 | 重要方法 | 案例参考 |
|------|----------|------|----------|----------|
| `OdinAttributeProcessor<T>` | `Sirenix.OdinInspector.Editor` | 属性处理器基类，用于处理特定类型的属性 | `OdinAttributeProcessor<T>.ProcessChildMemberAttributes()`, `OdinAttributeProcessor<T>.ProcessMemberAttributes()` | [BasicAttributeProcessorExample](#1-基础属性处理器---basicattributeprocessorexample) |
| `AttributeProcessorLocator` | `Sirenix.OdinInspector.Editor` | 属性处理器定位器，用于查找适用的处理器 | `AttributeProcessorLocator.GetProcessors()` | [CustomAttributeProcessorLocatorExample](Assets/Plugins/Sirenix/Demos/Custom%20Attribute%20Processors/Scripts/CustomAttributeProcessorLocatorExample.cs) |
| `DefaultOdinAttributeProcessor` | `Sirenix.OdinInspector.Editor` | 默认属性处理器 | - | - |
| `OdinAttributeProcessorLocator` | `Sirenix.OdinInspector.Editor` | Odin属性处理器定位器 | `OdinAttributeProcessorLocator.GetChildProcessors()` | - |

### 自定义绘制器类

| 类名 | 命名空间 | 描述 | 重要方法 | 案例参考 |
|------|----------|------|----------|----------|
| `OdinAttributeDrawer<T>` | `Sirenix.OdinInspector.Editor` | 属性绘制器基类，用于绘制带特定特性的属性 | `OdinAttributeDrawer<T>.DrawPropertyLayout()`, `OdinAttributeDrawer<T>.CallNextDrawer()` | - |
| `OdinAttributeDrawer<T, U>` | `Sirenix.OdinInspector.Editor` | 泛型属性绘制器基类，用于绘制带特定特性的特定类型属性 | `OdinAttributeDrawer<T, U>.DrawPropertyLayout()`, `OdinAttributeDrawer<T, U>.ValueEntry` | [HealthBarExample](#1-健康条绘制器---healthbarexample) |
| `OdinValueDrawer<T>` | `Sirenix.OdinInspector.Editor` | 值绘制器基类，用于绘制特定类型的值 | `OdinValueDrawer<T>.DrawPropertyLayout()`, `OdinValueDrawer<T>.ValueEntry` | [CustomDrawerExample](#2-自定义结构体绘制器---customdrawerexample) |
| `OdinGroupDrawer<T>` | `Sirenix.OdinInspector.Editor` | 组绘制器基类，用于绘制属性组 | `OdinGroupDrawer<T>.DrawPropertyLayout()`, `OdinGroupDrawer<T>.GroupAttribute` | [CustomGroupExample](Assets/Plugins/Sirenix/Demos/Custom%20Drawers/Scripts/CustomGroupExample.cs) |
| `InspectorPropertyDrawer` | `Sirenix.OdinInspector.Editor` | Inspector属性绘制器基类 | `InspectorPropertyDrawer.CanDrawProperty()`, `InspectorPropertyDrawer.DrawProperty()` | [GenericDrawerExample](Assets/Plugins/Sirenix/Demos/Custom%20Drawers/Scripts/GenericDrawerExample.cs) |
| `DrawerPriority` | `Sirenix.OdinInspector.Editor` | 绘制器优先级特性，用于控制绘制器的执行顺序 | - | [PriorityExamples](Assets/Plugins/Sirenix/Demos/Custom%20Drawers/Scripts/PriorityExamples.cs) |
| `DrawerLocator` | `Sirenix.OdinInspector.Editor` | 绘制器定位器，用于查找适用的绘制器 | `DrawerLocator.GetDrawers()` | - |

### 值提供器和解析器

| 类名 | 命名空间 | 描述 | 重要方法 | 案例参考 |
|------|----------|------|----------|----------|
| `ValueResolver<T>` | `Sirenix.OdinInspector.Editor` | 值解析器，用于解析表达式或成员引用 | `ValueResolver<T>.GetValue()`, `ValueResolver<T>.TryGetValue()` | [ValueAndActionResolversExample](#1-值和动作解析器---valueandactionresolversexample) |
| `ActionResolver` | `Sirenix.OdinInspector.Editor` | 动作解析器，用于解析方法调用 | `ActionResolver.DoAction()`, `ActionResolver.TryDoAction()` | [ValueAndActionResolversExample](#1-值和动作解析器---valueandactionresolversexample) |
| `ValueProvider<T>` | `Sirenix.OdinInspector.Editor` | 值提供器，用于提供值 | `ValueProvider<T>.GetValue()` | - |
| `PropertyValueEntry<T>` | `Sirenix.OdinInspector.Editor` | 属性值条目，用于访问和修改属性值 | `PropertyValueEntry<T>.SmartValue`, `PropertyValueEntry<T>.Values` | [HealthBarExample](#1-健康条绘制器---healthbarexample) |

## 特性（Attributes）详解

Odin Inspector的核心功能是通过特性（Attributes）来实现的。以下是一些最常用和最重要的特性：

### 布局特性

| 特性 | 描述 | 示例 | 案例参考 |
|------|------|------|----------|
| `[BoxGroup]` | 在盒子中组织属性 | `[BoxGroup("Settings")]` | [BasicAttributeProcessorExample](#1-基础属性处理器---basicattributeprocessorexample) |
| `[TabGroup]` | 创建选项卡组 | `[TabGroup("Tabs", "General")]` | [TabGroupByDeclaringType](#2-按声明类型分组---tabgroupbydeclaringtype) |
| `[HorizontalGroup]` | 水平排列属性 | `[HorizontalGroup("Split")]` | - |
| `[VerticalGroup]` | 垂直排列属性 | `[VerticalGroup("Left")]` | - |
| `[FoldoutGroup]` | 创建可折叠组 | `[FoldoutGroup("Advanced")]` | - |
| `[ResponsiveButtonGroup]` | 响应式按钮组 | `[ResponsiveButtonGroup]` | - |
| `[PropertyOrder]` | 设置属性顺序 | `[PropertyOrder(-1)]` | - |
| `[Title]` | 添加标题 | `[Title("Player Settings")]` | - |
| `[TitleGroup]` | 带标题的组 | `[TitleGroup("Stats")]` | - |

### 显示特性

| 特性 | 描述 | 示例 | 案例参考 |
|------|------|------|----------|
| `[LabelText]` | 自定义标签文本 | `[LabelText("玩家名称")]` | [BasicAttributeProcessorExample](#1-基础属性处理器---basicattributeprocessorexample) |
| `[GUIColor]` | 设置GUI颜色 | `[GUIColor(1, 0, 0)]` | - |
| `[HideLabel]` | 隐藏属性标签 | `[HideLabel]` | [BasicAttributeProcessorExample](#1-基础属性处理器---basicattributeprocessorexample) |
| `[HideInInspector]` | 在Inspector中隐藏属性 | `[HideInInspector]` | - |
| `[ShowInInspector]` | 在Inspector中显示属性 | `[ShowInInspector]` | - |
| `[ReadOnly]` | 使属性只读 | `[ReadOnly]` | - |
| `[ShowIf]` | 条件性显示属性 | `[ShowIf("IsEnabled")]` | - |
| `[HideIf]` | 条件性隐藏属性 | `[HideIf("IsDisabled")]` | - |
| `[EnableIf]` | 条件性启用属性 | `[EnableIf("CanEdit")]` | - |
| `[DisableIf]` | 条件性禁用属性 | `[DisableIf("IsLocked")]` | - |
| `[InfoBox]` | 显示信息框 | `[InfoBox("重要提示")]` | [BasicAttributeProcessorExample](#1-基础属性处理器---basicattributeprocessorexample), [BasicOdinEditorExampleWindow](#1-基础odin编辑器窗口---basicodineditorexamplewindow) |
| `[DetailedInfoBox]` | 显示详细信息框 | `[DetailedInfoBox("标题", "详情")]` | - |
| `[PreviewField]` | 显示预览字段 | `[PreviewField]` | - |
| `[ProgressBar]` | 显示进度条 | `[ProgressBar(0, 100)]` | - |
| `[TableList]` | 表格形式显示列表 | `[TableList]` | - |

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

## 实例案例库

以下是基于Odin Inspector官方Demo和源代码的实际案例，每个案例都对应README中的知识点，可以直接查看和学习。

### 自定义绘制器案例

#### 1. 健康条绘制器 - HealthBarExample
**对应知识点**: `OdinAttributeDrawer<T, U>` 自定义属性绘制器
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Drawers/Scripts/HealthBarExample.cs`
**功能**: 为float类型字段创建可视化的健康条显示

```csharp
// 使用示例
[HealthBar(100)]
public float Health;

// 实现要点
public class HealthBarAttributeDrawer : OdinAttributeDrawer<HealthBarAttribute, float>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        this.CallNextDrawer(label);  // 绘制原始字段
        // 绘制自定义健康条可视化
        float width = Mathf.Clamp01(this.ValueEntry.SmartValue / this.Attribute.MaxHealth);
        SirenixEditorGUI.DrawSolidRect(rect.SetWidth(rect.width * width), Color.red, false);
    }
}
```

#### 2. 自定义结构体绘制器 - CustomDrawerExample
**对应知识点**: `OdinValueDrawer<T>` 值类型绘制器
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Drawers/Scripts/CustomDrawerExample.cs`
**功能**: 为自定义结构体创建专门的Inspector显示

```csharp
// 自定义结构体
[Serializable]
public struct MyStruct
{
    public float X;
    public float Y;
}

// 绘制器实现
public class CustomStructDrawer : OdinValueDrawer<MyStruct>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        // 自定义绘制逻辑，将X和Y显示为滑动条
        value.X = EditorGUI.Slider(rect.AlignLeft(rect.width * 0.5f), "X", value.X, 0, 1);
        value.Y = EditorGUI.Slider(rect.AlignRight(rect.width * 0.5f), "Y", value.Y, 0, 1);
    }
}
```

#### 3. 扫雷游戏绘制器 - MinesweeperExample
**对应知识点**: 复杂自定义绘制器，游戏逻辑集成
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Drawers/Scripts/MinesweeperExample.cs`
**功能**: 在Inspector中实现完整的扫雷游戏

#### 4. 验证系统示例 - ValidationExample
**对应知识点**: `IAttributeValidator` 属性验证器
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Drawers/Scripts/ValidationExample.cs`
**功能**: 实现字段值的自定义验证逻辑

### 编辑器窗口案例

#### 1. 基础Odin编辑器窗口 - BasicOdinEditorExampleWindow
**对应知识点**: `OdinEditorWindow` 基础编辑器窗口
**文件位置**: `Assets/Plugins/Sirenix/Demos/Editor Windows/Scripts/Editor/BasicOdinEditorExampleWindow.cs`
**功能**: 展示如何创建最简单的Odin编辑器窗口

```csharp
public class BasicOdinEditorExampleWindow : OdinEditorWindow
{
    [MenuItem("Tools/Odin/Demos/基础Odin编辑器窗口")]
    private static void OpenWindow()
    {
        var window = GetWindow<BasicOdinEditorExampleWindow>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
    }

    [EnumToggleButtons]
    [InfoBox("从OdinEditorWindow继承，像创建Inspector一样创建编辑器窗口")]
    public ViewTool SomeField;
}
```

#### 2. Odin菜单编辑器窗口 - OdinMenuEditorWindowExample
**对应知识点**: `OdinMenuEditorWindow`, `OdinMenuTree` 菜单系统
**文件位置**: `Assets/Plugins/Sirenix/Demos/Editor Windows/Scripts/Editor/OdinMenuEditorWindowExample.cs`
**功能**: 创建带有树形菜单的复杂编辑器窗口

```csharp
public class OdinMenuEditorWindowExample : OdinMenuEditorWindow
{
    protected override OdinMenuTree BuildMenuTree()
    {
        OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true)
        {
            { "首页", this, EditorIcons.House },
            { "Odin设置", null, SdfIconType.GearFill },
            { "Odin设置/调色板", ColorPaletteManager.Instance, SdfIconType.PaletteFill }
        };
        
        tree.AddAllAssetsAtPath("更多设置", "Plugins/Sirenix", typeof(ScriptableObject), true);
        return tree;
    }
}
```

#### 3. 菜单样式自定义 - OdinMenuStyleExample
**对应知识点**: `OdinMenuStyle` 菜单外观定制
**文件位置**: `Assets/Plugins/Sirenix/Demos/Editor Windows/Scripts/Editor/OdinMenuStyleExample.cs`
**功能**: 展示如何自定义菜单树的外观和行为

#### 4. 快速检查对象 - QuicklyInspectObjects
**对应知识点**: `PropertyTree` 属性树系统
**文件位置**: `Assets/Plugins/Sirenix/Demos/Editor Windows/Scripts/Editor/QuicklyInspectObjects.cs`
**功能**: 创建快速检查和编辑对象的工具窗口

### 属性处理器案例

#### 1. 基础属性处理器 - BasicAttributeProcessorExample
**对应知识点**: `OdinAttributeProcessor<T>` 属性处理器基类
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Attribute Processors/Scripts/BasicAttributeProcessorExample.cs`
**功能**: 动态为类型添加特性，修改Inspector显示

```csharp
public class MyResolvedClassAttributeProcessor : OdinAttributeProcessor<MyCustomClass>
{
    public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
    {
        attributes.Add(new InfoBoxAttribute("动态添加的属性。"));
        attributes.Add(new InlinePropertyAttribute());
    }

    public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
    {
        if (member.Name == "Mode")
        {
            attributes.Add(new EnumToggleButtonsAttribute());
        }
        else if (member.Name == "Size")
        {
            attributes.Add(new RangeAttribute(0, 100));
        }
    }
}
```

#### 2. 按声明类型分组 - TabGroupByDeclaringType
**对应知识点**: 属性处理器的高级应用
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Attribute Processors/Scripts/TabGroupByDeclaringType.cs`
**功能**: 根据成员的声明类型自动创建选项卡分组

#### 3. 列表项属性处理器 - AttributeProcessorForListItemsExample
**对应知识点**: 集合类型的属性处理
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Attribute Processors/Scripts/AttributeProcessorForListItemsExample.cs`
**功能**: 为列表中的每个项目动态添加特性

#### 4. 字典键值自定义 - CustomizeDictionaryKeyValueExample
**对应知识点**: 复杂类型的属性处理
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Attribute Processors/Scripts/CustomizeDictionaryKeyValueExample.cs`
**功能**: 自定义字典类型在Inspector中的显示方式

### 特性组合案例

#### 1. 值和动作解析器 - ValueAndActionResolversExample
**对应知识点**: `ValueResolver<T>`, `ActionResolver` 解析器系统
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Drawers/Scripts/ValueAndActionResolversExample.cs`
**功能**: 展示如何使用解析器系统实现动态值计算和方法调用

#### 2. 反射示例 - ReflectionExample
**对应知识点**: Odin的反射能力和动态特性
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Drawers/Scripts/ReflectionExample.cs`
**功能**: 展示Odin如何处理复杂的反射场景

#### 3. 泛型菜单示例 - GenericMenuExample
**对应知识点**: 泛型类型的处理
**文件位置**: `Assets/Plugins/Sirenix/Demos/Custom Drawers/Scripts/GenericMenuExample.cs`
**功能**: 展示如何为泛型类型创建自定义菜单

### 案例学习路径建议

1. **初学者路径**:
   - 从 `BasicOdinEditorExampleWindow` 开始了解基础窗口创建
   - 学习 `HealthBarExample` 理解简单的自定义绘制器
   - 查看 `BasicAttributeProcessorExample` 了解属性处理器概念

2. **进阶路径**:
   - 研究 `OdinMenuEditorWindowExample` 掌握复杂窗口结构
   - 分析 `CustomDrawerExample` 学习值类型绘制器
   - 深入 `ValueAndActionResolversExample` 理解解析器系统

3. **高级路径**:
   - 挑战 `MinesweeperExample` 学习复杂交互逻辑
   - 研究 `OdinMenuStyleExample` 掌握界面定制技巧
   - 分析各种属性处理器实现高级功能

### 如何使用这些案例

1. **查看源码**: 直接打开对应文件查看完整实现
2. **运行Demo**: 导入对应的unitypackage文件体验效果
3. **修改实验**: 基于现有代码进行修改和扩展
4. **组合应用**: 将多个案例的技术点组合到自己的项目中

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