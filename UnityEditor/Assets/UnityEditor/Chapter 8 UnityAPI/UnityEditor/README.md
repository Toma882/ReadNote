# UnityEditor 命名空间示例

## 概述

UnityEditor 是 Unity 编辑器扩展的核心命名空间，提供了丰富的编辑器API来创建自定义工具、窗口和功能。

## 主要子命名空间

### 1. 编辑器核心
- **UnityEditor.Editor** - 自定义编辑器
- **UnityEditor.EditorWindow** - 自定义编辑器窗口
- **UnityEditor.PropertyDrawer** - 自定义属性绘制器
- **UnityEditor.CustomEditor** - 自定义编辑器基类

### 2. GUI系统
- **UnityEditor.EditorGUI** - 编辑器GUI绘制
- **UnityEditor.EditorGUILayout** - 编辑器GUI布局
- **UnityEditor.GUI** - GUI控件
- **UnityEditor.GUILayout** - GUI布局

### 3. 资源管理
- **UnityEditor.AssetDatabase** - 资源数据库
- **UnityEditor.AssetPostprocessor** - 资源导入处理器
- **UnityEditor.AssetModificationProcessor** - 资源修改处理器
- **UnityEditor.AssetImporter** - 资源导入器

### 4. 构建系统
- **UnityEditor.BuildPipeline** - 构建管线
- **UnityEditor.BuildTarget** - 构建目标
- **UnityEditor.BuildOptions** - 构建选项
- **UnityEditor.PlayerSettings** - 播放器设置

### 5. 场景管理
- **UnityEditor.SceneManagement** - 场景管理
- **UnityEditor.SceneView** - 场景视图
- **UnityEditor.Handles** - 场景手柄
- **UnityEditor.Gizmos** - 场景辅助线

### 6. 菜单系统
- **UnityEditor.MenuItem** - 菜单项
- **UnityEditor.ContextMenu** - 上下文菜单
- **UnityEditor.ShortcutManagement** - 快捷键管理

### 7. 工具系统
- **UnityEditor.EditorTools** - 编辑器工具
- **UnityEditor.ToolManager** - 工具管理器
- **UnityEditor.GridBrush** - 网格画笔

### 8. 包管理
- **UnityEditor.PackageManager** - 包管理器
- **UnityEditor.PackageManager.UI** - 包管理器UI
- **UnityEditor.PackageManager.Requests** - 包管理器请求

### 9. 版本控制
- **UnityEditor.VersionControl** - 版本控制
- **UnityEditor.VersionControl.Provider** - 版本控制提供者

### 10. 性能分析
- **UnityEditor.Profiling** - 性能分析
- **UnityEditor.Profiler** - 性能分析器
- **UnityEditor.MemoryProfiler** - 内存分析器

## 示例文件

每个子命名空间都有对应的示例文件，展示该命名空间中主要API的使用方法。

## 使用方法

1. 查看对应子命名空间的示例文件
2. 在编辑器中运行示例代码
3. 参考代码注释了解详细用法
4. 根据项目需求调整和扩展代码

## 最佳实践

1. **编辑器脚本放置** - 将编辑器脚本放在 Editor 文件夹中
2. **条件编译** - 使用 #if UNITY_EDITOR 条件编译
3. **性能考虑** - 避免在 OnGUI 中进行耗时操作
4. **用户体验** - 提供直观的界面和操作反馈
5. **错误处理** - 完善的异常处理和用户提示
6. **代码组织** - 模块化设计，便于维护和扩展
