# BatchBuild 模块

## 概述
BatchBuild 模块是一个强大的批量构建系统，旨在简化Unity项目的多平台打包和发布流程。通过可视化的编辑器界面，开发者可以创建和管理多个构建配置文件，为不同的目标平台设置不同的构建参数，并一键触发批量构建过程。该模块基于ScriptableObject实现持久化数据存储，提供了直观的配置界面和完整的构建流程管理，大大提高了多平台发布的效率。

## 核心功能
- **批量构建配置管理**：创建并管理多个构建配置任务
- **多平台支持**：支持Unity所有可构建的目标平台
- **可视化编辑界面**：提供直观的构建配置界面
- **自动化构建流程**：一键触发多个构建任务
- **构建报告生成**：提供详细的构建结果报告

## 重要接口和类

### `BuildProfile` 类
构建配置文件类，继承自ScriptableObject，用于存储构建任务集合。

| 属性/方法 | 说明 |
|---------|------|
| `BuildTasks` | 构建任务列表，存储所有的构建任务 |

### `BuildTask` 类
单个构建任务的数据类，定义了构建任务的各项参数。

| 属性/方法 | 说明 |
|---------|------|
| `ProductName` | 产品名称，也用作输出文件名 |
| `BuildTarget` | 目标平台，定义构建输出的目标平台 |
| `BuildPath` | 构建输出路径，定义构建文件的保存位置 |
| `SceneAssets` | 要包含在构建中的场景列表 |

### `BuildProfileEditor` 类
自定义编辑器类，为BuildProfile提供可视化编辑界面。

| 方法 | 说明 |
|------|------|
| `OnInspectorGUI()` | 绘制Inspector界面，显示构建配置编辑器 |
| `OnTopGUI()` | 绘制顶部工具栏，包含创建、展开、收缩、清空和打包按钮 |
| `OnBodyGUI()` | 绘制主体内容，显示所有构建任务的详细配置 |

## UML类图

```
+-------------------+       +------------------+
|   BuildProfile    |1------*|    BuildTask    |
+-------------------+       +------------------+
| +BuildTasks: List |       | +ProductName     |
+-------------------+       | +BuildTarget     |
        ^                   | +BuildPath       |
        |                   | +SceneAssets     |
        |                   +------------------+
+---------------------+             ^
| ScriptableObject   |             |
+---------------------+             |
| +hideFlags         |     +-------------------------+
| +name              |     |     [Serializable]      |
+---------------------+     +-------------------------+

+---------------------+       +------------------+
| BuildProfileEditor  |------>|   BuildProfile   |
+---------------------+       +------------------+
| -foldoutMap         |
| -scroll             |
| -profile            |
| +OnInspectorGUI()   |
| +OnTopGUI()         |
| +OnBodyGUI()        |
+---------------------+
```

## 批量构建流程图

```
创建构建配置文件 --> 添加构建任务 --> 配置构建参数(平台、场景、路径) --> 点击"打包"按钮 --> 执行构建流程 --> 生成构建报告
```

## 思维导图

```
BatchBuild模块
├── 数据结构
│   ├── BuildProfile (ScriptableObject)
│   │   └── 管理多个BuildTask
│   └── BuildTask (可序列化类)
│       ├── 产品名称
│       ├── 目标平台
│       ├── 构建路径
│       └── 场景列表
├── 编辑器扩展
│   ├── BuildProfileEditor
│   │   ├── 顶部工具栏
│   │   │   ├── 新建任务
│   │   │   ├── 展开/收缩
│   │   │   ├── 清空列表
│   │   │   └── 打包按钮
│   │   └── 任务配置区域
│   │       ├── 任务折叠/展开
│   │       ├── 场景资源选择
│   │       ├── 平台选择
│   │       └── 路径配置
│   └── 编辑器集成
│       ├── 撤销/重做支持
│       ├── 进度条显示
│       └── 日志输出
└── 构建流程
    ├── 任务准备
    │   ├── 场景列表转换
    │   └── 输出路径组装
    ├── 构建执行
    │   ├── BuildPipeline.BuildPlayer调用
    │   └── 构建选项设置
    └── 结果报告
        ├── 构建成功/失败统计
        └── 日志输出
```

## 应用场景
1. **多平台同步发布**：同时为多个平台构建游戏版本
2. **自动化构建流程**：集成到CI/CD系统中实现自动化构建
3. **版本发布管理**：管理不同版本的构建配置
4. **测试环境部署**：快速为测试团队提供不同平台的构建版本
5. **构建参数管理**：统一管理和复用构建参数设置

## 最佳实践
1. **构建配置文件组织**：为不同类型的构建（开发版、测试版、发布版）创建单独的配置文件
2. **命名规范**：为构建任务和输出文件采用清晰的命名规则，包含版本号和平台信息
3. **场景管理**：确保所有需要构建的场景都已保存并正确添加到构建任务中
4. **构建路径策略**：使用相对路径或环境变量增强构建脚本的可移植性
5. **构建报告保存**：保存构建报告以便追踪和分析构建历史
6. **持续集成**：将批量构建功能集成到版本控制和CI/CD工作流程中

## 代码示例
```csharp
// 1. 如何从脚本触发批量构建
public class BatchBuildExample
{
    [MenuItem("Tools/Run Batch Build")]
    public static void RunBatchBuild()
    {
        // 加载构建配置文件
        BuildProfile profile = AssetDatabase.LoadAssetAtPath<BuildProfile>(
            "Assets/BuildProfiles/ReleaseProfile.asset");
        
        if (profile == null)
        {
            Debug.LogError("构建配置文件不存在!");
            return;
        }
        
        // 执行构建
        foreach (BuildTask task in profile.BuildTasks)
        {
            // 准备场景列表
            List<string> scenePaths = new List<string>();
            foreach (SceneAsset sceneAsset in task.SceneAssets)
            {
                string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                if (!string.IsNullOrEmpty(scenePath))
                {
                    scenePaths.Add(scenePath);
                }
            }
            
            // 执行构建
            string outputPath = Path.Combine(task.BuildPath, task.ProductName);
            BuildReport report = BuildPipeline.BuildPlayer(
                scenePaths.ToArray(),
                outputPath,
                task.BuildTarget,
                BuildOptions.None
            );
            
            // 输出构建结果
            Debug.Log($"构建 {task.ProductName} 结果: {report.summary.result}");
        }
    }
}
```

## 相关资源
- [Unity文档: BuildPipeline](https://docs.unity3d.com/ScriptReference/BuildPipeline.html)
- [Unity文档: BuildTarget](https://docs.unity3d.com/ScriptReference/BuildTarget.html)
- [Unity文档: BuildOptions](https://docs.unity3d.com/ScriptReference/BuildOptions.html)
- [Unity文档: ScriptableObject](https://docs.unity3d.com/ScriptReference/ScriptableObject.html)
- [Unity文档: EditorGUI](https://docs.unity3d.com/ScriptReference/EditorGUI.html)
