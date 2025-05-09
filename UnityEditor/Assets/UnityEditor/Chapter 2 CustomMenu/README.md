# Unity编辑器扩展 - Chapter 2: CustomMenu

## 概述

本章专注于Unity编辑器中自定义菜单系统的使用。在Unity开发中，菜单是与编辑器交互的重要入口点，通过自定义菜单可以快速访问各种功能工具，提高开发效率。本章展示了如何通过特性标签创建和管理Unity编辑器中的各种菜单。

## 核心知识点

### 菜单系统概览

Unity编辑器中的菜单主要分为以下几种类型：

- **主菜单栏菜单** - 位于编辑器顶部的下拉菜单（如File, Edit, Assets, GameObject等）
- **上下文菜单** - 通过右键点击触发的菜单
- **组件上下文菜单** - 在Inspector面板中组件右上角的齿轮图标菜单
- **属性上下文菜单** - 在Inspector面板中特定属性的右键菜单

## 相关特性

### MenuItem特性

- **基本语法**: `[MenuItem("MenuPath/ItemName")]`自定义Unity顶部的功能菜单
- **用途**: 在Unity主菜单栏或右键上下文菜单中添加自定义功能入口
- **示例1**:  
  - `[MenuItem("GameObject/3D Object/Stair")]` 
  - 在Hierarchy窗口中右键点击GameObject时显示自定义菜单项-创建一个3D物体（阶梯）    
- **示例2**:  
  - `[MenuItem("CONTEXT/BoxCollider/Encapsulate")]` 为组件添加下拉列表功能菜单
  **示例3**:
  -   `[MenuItem("Assets/Function7]` 在Project窗口中右击显示Function7接口
  
- **限制**: 只能用于非静态方法，且该方法不能有参数

- **参数说明**:
  - **路径**: 指定菜单项在菜单层级中的位置（如"Example/Function1"）
  - **验证函数**: 用于确定菜单项是否可用
  - **优先级**: 控制菜单项在同级菜单中的排序位置
- **快捷键设置**:
  - **`%`**: Ctrl(Windows)/Command(macOS)
  - **`#`**: Shift
  - **`&`**: Alt
  - **示例**: `[MenuItem("Window/Function1 #E")]` - Shift+E 快捷键

### ContextMenu特性

- **基本语法**: `[ContextMenu("ItemName")]`
- **用途**: 为MonoBehaviour组件添加上下文菜单项
- **应用**: 在组件右上角的齿轮图标菜单中添加自定义功能
- **限制**: 只能用于非静态方法，且该方法不能有参数

### ContextMenuItem特性

- **基本语法**: `[ContextMenuItem("MenuItemName", "MethodName")]`
- **用途**: 为特定字段添加右键上下文菜单项
- **应用**: 在Inspector面板中右键点击字段时显示自定义菜单项
- **示例**: 重置字段值、随机生成值等快捷操作


## 代码结构

本章包含的主要代码文件：

1. **MenuItemExample.cs**: 演示了MenuItem特性的各种用法
2. **ContextMenuExample.cs**: 演示了ContextMenu特性的基本用法
3. **ContextMenuItemExample.cs**: 演示了ContextMenuItem特性的基本用法
4. **工具类实现**: 如AutoBoxCollider.cs、MeshCombiner.cs等展示了实用工具的实现

## 思维导图

```
Unity CustomMenu系统
├── 菜单特性
│   ├── MenuItem
│   │   ├── 基本用法
│   │   │   └── [MenuItem("MenuPath/ItemName")]
│   │   ├── 参数选项
│   │   │   ├── 验证函数
│   │   │   └── 优先级设置
│   │   ├── 快捷键设置
│   │   │   ├── % (Ctrl/Command)
│   │   │   ├── # (Shift)
│   │   │   └── & (Alt)
│   │   └── 特殊菜单路径
│   │       ├── Window
│   │       ├── GameObject
│   │       ├── Assets
│   │       └── CONTEXT
│   │
│   ├── ContextMenu
│   │   ├── 基本用法
│   │   │   └── [ContextMenu("ItemName")]
│   │   └── 应用场景
│   │       └── 组件操作功能
│   │
│   └── ContextMenuItem
│       ├── 基本用法
│       │   └── [ContextMenuItem("MenuName", "MethodName")]
│       └── 应用场景
│           └── 字段值操作
│
├── 菜单位置
│   ├── 主菜单栏
│   │   └── 顶部下拉菜单
│   ├── 右键上下文菜单
│   │   ├── Project面板
│   │   ├── Hierarchy面板
│   │   └── Scene视图
│   ├── 组件菜单
│   │   └── Inspector组件齿轮图标
│   └── 属性菜单
│       └── Inspector字段右键菜单
│
└── 实用工具示例
    ├── 网格处理
    │   ├── MeshCombiner (合并网格)
    │   └── MeshExtracter (提取网格)
    ├── 碰撞体处理
    │   └── AutoBoxCollider (自动包围碰撞体)
    ├── 场景工具
    │   └── CreateStair (创建楼梯)
    └── UI工具
        └── Image2RawImage (转换UI组件)
```

## UML类图

```
+------------------+            +------------------+            +------------------+
|   MenuItem       |            |   ContextMenu    |            | ContextMenuItem  |
+------------------+            +------------------+            +------------------+
| + menuItem       |            | + itemName       |            | + menuItemName   |
| + priority       |            +------------------+            | + functionName   |
| + validate       |            | + 应用于方法      |            +------------------+
+------------------+            +------------------+            | + 应用于字段      |
| + 静态方法        |                                           +------------------+
+------------------+
        |
        |
        v
+------------------+
|   实现类          |
+------------------+
| + 功能实现方法     |
| + 验证方法        |
+------------------+
```

## 重要类和接口

### 特性类

| 特性名称 | 应用目标 | 主要参数 | 功能描述 |
|----------|----------|----------|----------|
| MenuItem | 静态方法 | 菜单路径、优先级、验证函数 | 创建主菜单或上下文菜单项 |
| ContextMenu | 实例方法 | 菜单名称 | 为组件添加上下文菜单项 |
| ContextMenuItem | 字段 | 菜单名称、方法名称 | 为字段添加右键菜单项 |

### 实用工具类

| 类名 | 功能 | 菜单位置 | 主要方法 |
|------|------|----------|----------|
| AutoBoxCollider | 自动调整碰撞体 | CONTEXT/BoxCollider/Encapsulate | Execute() |
| MeshCombiner | 合并多个网格 | GameObject/Combine Meshes | CombineMeshes() |
| MeshExtracter | 提取网格到资源 | GameObject/Extract Mesh | ExtractMesh() |
| CreateStair | 创建楼梯结构 | GameObject/Create Stair | CreateStairObject() |
| HierarchyWindowUtility | 层级窗口工具 | GameObject/... | 多个工具方法 |
| Image2RawImage | UI组件转换 | CONTEXT/Image/Convert To RawImage | ConvertToRawImage() |
| AssetReference | 资源引用查找 | Assets/Find References In Scene | FindReferences() |

## 应用场景

1. **编辑器扩展工具**: 创建专用于项目的自定义工具，提高工作效率
2. **批处理操作**: 一键处理多个对象，如合并网格、提取材质等
3. **组件功能扩展**: 为现有组件添加新功能，如自动调整碰撞体
4. **资产处理流程**: 简化资产导入和处理流程，如一键导入和设置贴图
5. **数据验证与修复**: 检查和修复场景或资产中的问题

## 最佳实践

1. **合理组织菜单结构**: 相关功能放在同一菜单组下，避免菜单过于臃肿
2. **设置适当的优先级**: 常用功能应使用较高的优先级，使其更容易被找到
3. **添加验证函数**: 确保菜单项只在适当的上下文中可用，避免误操作
4. **提供快捷键**: 为常用功能设置快捷键，提高使用效率
5. **命名规范**: 菜单项名称应清晰表达其功能，便于用户理解
6. **错误处理**: 在实现中添加适当的错误处理和用户反馈

## 相关资源

### 官方文档

- [Unity Documentation - MenuItem](https://docs.unity3d.com/ScriptReference/MenuItem.html)
- [Unity Documentation - ContextMenu](https://docs.unity3d.com/ScriptReference/ContextMenu.html)
- [Unity Documentation - ContextMenuItem](https://docs.unity3d.com/ScriptReference/ContextMenuItemAttribute.html)
- [Unity Manual - Editor Extensions](https://docs.unity3d.com/Manual/ExtendingTheEditor.html)
- [Unity Manual - Creating Toolbar Menus](https://docs.unity3d.com/Manual/extending-the-editor-toolbar-menu-items.html)

### 教程与参考

- [Unity Editor Extensions - Custom Menus](https://unity3d.com/learn/tutorials/topics/interface-essentials/unity-editor-extensions-menu-items)
- [Custom Editor Scripting - Unity Learn](https://learn.unity.com/tutorial/editor-scripting)
- [Unity Editor Toolbox - GitHub](https://github.com/Unity-Technologies/UnityCsReference)
