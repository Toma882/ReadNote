# Unity编辑器扩展 - Chapter 6: Gizmos

## 概述

本章专注于Unity编辑器中的Gizmos和Handles系统，这是Unity场景视图中用于可视化和交互操作的重要工具。Gizmos主要用于可视化显示游戏对象的信息，而Handles则提供了与场景视图中对象交互的功能。通过本章的学习，你将掌握如何使用这些工具来增强场景编辑体验，创建自定义的编辑器工具，并提供直观的可视化效果。

## 核心知识点

### Gizmos系统概览

Unity中的Gizmos主要分为以下几种类型：

- **内置Gizmos** - Unity自带的基本可视化形状和图标（如球体、立方体、线条等）
- **自定义Gizmos** - 开发者自定义的可视化效果，用于展示特定的游戏对象信息
- **Gizmo图标** - 可用于标记场景中的特定位置或对象
- **场景Gizmos** - 场景视图中的轴向、网格和其他辅助元素

### Handles系统概览

Unity中的Handles主要分为以下几种类型：

- **位置控制** - 用于移动、旋转和缩放对象的控制柄
- **自定义控制柄** - 开发者自定义的交互控制，用于特定功能
- **GUI控制** - 在场景视图中绘制的GUI元素，用于与对象交互
- **拾取和选择** - 用于在场景视图中选择对象的功能

## 相关特性

### Gizmos类

- **基本用法**: `Gizmos.DrawLine(Vector3 from, Vector3 to)`等方法
- **用途**: 在场景视图中绘制基本形状和图标，用于可视化信息
- **示例**:  
  ```csharp
  void OnDrawGizmos() {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(transform.position, 1f);
  }
  ```

### Handles类

- **基本用法**: `Handles.PositionHandle(Vector3 position, Quaternion rotation)`等方法
- **用途**: 在场景视图中提供交互控制
- **示例**:  
  ```csharp
  void OnSceneGUI() {
      EditorGUI.BeginChangeCheck();
      Vector3 newPosition = Handles.PositionHandle(target.position, Quaternion.identity);
      if (EditorGUI.EndChangeCheck()) {
          Undo.RecordObject(target, "Move Object");
          target.position = newPosition;
      }
  }
  ```

### DrawGizmo特性

- **基本语法**: `[DrawGizmo(GizmoType flags, Type targetType)]`
- **用途**: 为特定类型的对象绘制Gizmo，不需要对象自身实现OnDrawGizmos方法
- **示例**:  
  ```csharp
  [DrawGizmo(GizmoType.Active | GizmoType.Selected, typeof(MyComponent))]
  static void DrawGizmo(MyComponent target, GizmoType gizmoType) {
      Gizmos.DrawSphere(target.transform.position, 1f);
  }
  ```

### OnDrawGizmos和OnDrawGizmosSelected方法

- **基本用法**: 在MonoBehaviour中实现这些方法
- **用途**: OnDrawGizmos总是绘制，OnDrawGizmosSelected仅在对象被选中时绘制
- **示例**:  
  ```csharp
  void OnDrawGizmos() {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, detectionRadius);
  }
  void OnDrawGizmosSelected() {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(transform.position, attackRadius);
  }
  ```

## 代码结构

本章包含的主要代码文件：

1. **GizmosExample.cs**: 演示了基本Gizmos的使用方法
2. **GizmosExampleEditor.cs**: 演示了如何在自定义编辑器中使用Gizmos
3. **BeizerPath.cs**: 实现了贝塞尔曲线路径系统
4. **BeizerPathEditor.cs**: 贝塞尔曲线路径的编辑器界面
5. **AvatarCameraController.cs**: 实现了角色相机控制器
6. **AvatarCameraControllerEditor.cs**: 角色相机控制器的可视化编辑界面

## 思维导图

```
Unity Gizmos和Handles系统
├── Gizmos系统
│   ├── 基本绘制函数
│   │   ├── DrawLine - 绘制线段
│   │   ├── DrawSphere/DrawWireSphere - 绘制实心/线框球体
│   │   ├── DrawCube/DrawWireCube - 绘制实心/线框立方体
│   │   ├── DrawIcon - 绘制图标
│   │   ├── DrawGUITexture - 绘制GUI纹理
│   │   ├── DrawFrustum - 绘制视锥体
│   │   ├── DrawRay - 绘制射线
│   │   └── DrawMesh - 绘制网格
│   │
│   ├── 属性设置
│   │   ├── color - 设置绘制颜色
│   │   ├── matrix - 设置变换矩阵
│   │   └── exposure - 设置曝光度
│   │
│   ├── 回调方法
│   │   ├── OnDrawGizmos - 总是绘制的Gizmos
│   │   └── OnDrawGizmosSelected - 仅在选中时绘制的Gizmos
│   │
│   └── DrawGizmo特性
│       ├── GizmoType.Active - 激活状态下的Gizmo
│       ├── GizmoType.Selected - 选中状态下的Gizmo
│       ├── GizmoType.Pickable - 可拾取的Gizmo
│       ├── GizmoType.NotInSelectionHierarchy - 未选中对象层级的Gizmo
│       └── GizmoType.InSelectionHierarchy - 选中对象层级的Gizmo
│
├── Handles系统
│   ├── 交互控制柄
│   │   ├── PositionHandle - 位置控制柄
│   │   ├── RotationHandle - 旋转控制柄
│   │   ├── ScaleHandle - 缩放控制柄
│   │   ├── FreeMoveHandle - 自由移动控制柄
│   │   ├── Slider - 单轴滑动控制
│   │   └── RadiusHandle - 半径控制柄
│   │
│   ├── 绘制函数
│   │   ├── DrawLine - 绘制线段
│   │   ├── DrawPolyLine - 绘制多段线
│   │   ├── DrawWireDisc - 绘制线框圆盘
│   │   ├── DrawWireArc - 绘制线框弧
│   │   ├── DrawSolidDisc - 绘制实心圆盘
│   │   ├── DrawSolidArc - 绘制实心弧
│   │   ├── DrawBezier - 绘制贝塞尔曲线
│   │   └── DrawDottedLine - 绘制虚线
│   │
│   ├── GUI相关
│   │   ├── BeginGUI/EndGUI - GUI绘制块
│   │   ├── Label - 绘制标签
│   │   ├── Button - 绘制按钮
│   │   └── ColorPicker - 颜色选择器
│   │
│   └── 事件和输入
│       ├── Event.current - 当前事件
│       ├── HandleUtility.GUIPointToWorldRay - GUI点转换为世界射线
│       ├── HandleUtility.PickObject - 拾取对象
│       └── HandleUtility.AddDefaultControl - 添加默认控制
│
├── 应用场景
│   ├── 路径编辑工具
│   │   ├── 贝塞尔曲线路径
│   │   ├── 路径点编辑
│   │   ├── 曲线可视化
│   │   └── 运行时导航指引
│   │
│   ├── 相机控制系统
│   │   ├── 视角预览
│   │   ├── 关注点控制
│   │   ├── 距离调整
│   │   └── 碰撞检测
│   │
│   ├── 游戏对象调试
│   │   ├── 碰撞区域可视化
│   │   ├── 视野范围显示
│   │   ├── AI路径展示
│   │   └── 物理关系展示
│   │
│   └── 编辑器工具
│       ├── 关卡设计辅助
│       ├── 对象对齐工具
│       ├── 批量编辑工具
│       └── 自定义变换控制
│
└── 最佳实践
    ├── 性能优化
    │   ├── 条件性绘制
    │   ├── 距离衰减显示
    │   ├── LOD (Level of Detail)
    │   └── 缓存计算结果
    │
    ├── 视觉设计
    │   ├── 一致的颜色方案
    │   ├── 清晰的形状区分
    │   ├── 适当的不透明度
    │   └── 动态视觉反馈
    │
    ├── 代码组织
    │   ├── 分离绘制逻辑
    │   ├── 复用公共函数
    │   ├── 使用SceneView.duringSceneGui
    │   └── 编辑模式与运行模式分离
    │
    └── 用户体验
        ├── 直观的交互设计
        ├── 恰当的拾取区域大小
        ├── 上下文相关的控制
        └── 撤销/重做支持
```

## UML类图

```
+--------------------+                 +--------------------+
|     MonoBehaviour  |                 |     Editor         |
+--------------------+                 +--------------------+
| + OnDrawGizmos()   |                 | + OnSceneGUI()     |
| + OnDrawGizmosSelected() |           | + OnInspectorGUI() |
+--------------------+                 +--------------------+
         ^                                     ^
         |                                     |
+--------------------+                 +--------------------+
|   GizmosExample    |                 | GizmosExampleEditor|
+--------------------+<----------------+--------------------+
| - showGizmos: bool |                 | - DrawGizmos()     |
| - radius: float    |                 | + OnSceneGUI()     |
| + OnDrawGizmos()   |                 | + OnInspectorGUI() |
+--------------------+                 +--------------------+

+--------------------+                 +--------------------+
|     BeizerPath     |                 |  BeizerPathEditor  |
+--------------------+<----------------+--------------------+
| - points: Vector3[]|                 | - DrawPath()       |
| - controlPoints: Vector3[] |         | - DrawControlPoints() |
| + GetPoint(): Vector3 |              | + OnSceneGUI()     |
| + OnDrawGizmos()   |                 | + OnInspectorGUI() |
+--------------------+                 +--------------------+

+--------------------+                 +---------------------+
|AvatarCameraController|               |AvatarCameraControllerEditor|
+--------------------+<----------------+---------------------+
| - target: Transform|                 | - DrawCameraView()  |
| - distance: float  |                 | - DrawOrbit()       |
| - minDistance: float |               | + OnSceneGUI()      |
| - maxDistance: float |               | + OnInspectorGUI()  |
| - orbitSpeed: float|                 | - DrawHandle()      |
| + UpdateCamera()   |                 +---------------------+
+--------------------+
```

## 重要类和接口

### Gizmos相关类

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|----------|------|------|--------------|
| Gizmos | 静态类 | 提供在场景视图中绘制形状和图标的功能 | `DrawLine()`, `DrawSphere()`, `DrawIcon()`, `color` |
| GizmoType | 枚举 | 定义Gizmo的显示条件和类型 | `Active`, `Selected`, `Pickable` |
| DrawGizmo | 特性 | 用于标记静态方法为Gizmo绘制方法 | `targetType`, `gizmoType` |

### Handles相关类

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|----------|------|------|--------------|
| Handles | 静态类 | 提供在场景视图中绘制交互控制柄的功能 | `PositionHandle()`, `DrawLine()`, `Button()` |
| HandleUtility | 静态类 | 提供处理场景视图交互的辅助功能 | `PickObject()`, `GUIPointToWorldRay()` |
| EditorGUI | 静态类 | 提供在编辑器中绘制GUI元素的功能 | `BeginChangeCheck()`, `EndChangeCheck()` |
| EditorGUILayout | 静态类 | 提供在编辑器中使用布局绘制GUI的功能 | `FloatField()`, `Toggle()` |

### 实例项目类

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|----------|------|------|--------------|
| GizmosExample | MonoBehaviour | 基本Gizmos使用示例 | `OnDrawGizmos()`, `radius` |
| BeizerPath | MonoBehaviour | 贝塞尔曲线路径实现 | `GetPoint()`, `points`, `controlPoints` |
| AvatarCameraController | MonoBehaviour | 角色相机控制器 | `UpdateCamera()`, `distance`, `target` |
| GizmosExampleEditor | Editor | GizmosExample的自定义编辑器 | `OnSceneGUI()`, `DrawGizmos()` |
| BeizerPathEditor | Editor | BeizerPath的自定义编辑器 | `OnSceneGUI()`, `DrawControlPoints()` |
| AvatarCameraControllerEditor | Editor | 相机控制器的自定义编辑器 | `OnSceneGUI()`, `DrawCameraView()` |

## 应用场景

1. **游戏对象可视化**: 显示游戏对象的范围、方向或特殊属性
2. **编辑器工具创建**: 构建自定义的场景编辑工具，如路径编辑器
3. **调试辅助**: 在开发过程中可视化显示物理碰撞、AI路径或视线范围
4. **关卡设计**: 辅助开发者进行关卡布局和设计
5. **相机控制**: 创建和编辑相机行为，如镜头轨迹或视角边界

## 最佳实践

1. **性能优化**:
   - 在OnDrawGizmosSelected中绘制复杂Gizmo，而非OnDrawGizmos中
   - 为Gizmo添加距离检测，仅在靠近时显示详细信息
   - 避免在Gizmo绘制方法中分配新对象

2. **视觉清晰度**:
   - 为不同类型的信息使用一致的颜色方案
   - 使用适当的不透明度，避免遮挡场景内容
   - 选择易于识别的形状表示不同含义

3. **代码组织**:
   - 将复杂的Gizmo/Handle绘制代码分离到独立方法中
   - 使用DrawGizmo特性而非OnDrawGizmos以减少MonoBehaviour上的代码
   - 利用SceneView.duringSceneGui事件实现编辑模式下的交互

4. **用户体验**:
   - 总是添加Undo支持，让用户可以撤销交互操作
   - 提供适当大小的控制柄，便于鼠标选择
   - 添加视觉反馈，明确当前可交互的元素

## 相关资源

### 官方文档

- [Unity Gizmos 类参考](https://docs.unity3d.com/ScriptReference/Gizmos.html)
- [Unity Handles 类参考](https://docs.unity3d.com/ScriptReference/Handles.html)
- [Unity DrawGizmo 特性参考](https://docs.unity3d.com/ScriptReference/DrawGizmoAttribute.html)
- [Unity OnDrawGizmos 方法](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDrawGizmos.html)
- [Unity HandleUtility 类参考](https://docs.unity3d.com/ScriptReference/HandleUtility.html)
- [Unity Editor 扩展基础](https://docs.unity3d.com/Manual/ExtendingTheEditor.html)

### 教程与参考

- [Unity 编辑器工具开发入门](https://learn.unity.com/tutorial/editor-scripting)
- [创建自定义编辑器工具](https://docs.unity3d.com/Manual/editor-CustomEditors.html)
- [在场景视图中绘制](https://docs.unity3d.com/Manual/DrawingGizmos.html)
- [Handles与用户交互](https://docs.unity3d.com/Manual/HandleUtility.html)
- [Unity贝塞尔曲线实现](https://catlikecoding.com/unity/tutorials/curves-and-splines/) 