# AssetBundleBuilder 模块

## 概述
AssetBundleBuilder 模块提供了一个功能完善的AssetBundle构建工具，通过可视化界面让开发者配置和执行AssetBundle打包过程。该工具支持多种构建选项，如目标平台选择、压缩方式设置、输出路径配置等，并提供了丰富的构建选项控制，使开发者能够根据项目需求灵活定制AssetBundle的构建过程。此外，该工具还支持将构建设置持久化保存，以便在不同的编辑器会话中复用相同的构建配置。

## 核心功能
- **可视化构建界面**：提供直观的编辑器窗口配置AssetBundle构建参数
- **多平台构建支持**：支持为不同目标平台构建AssetBundle
- **多种压缩类型**：支持Uncompressed、LZMA和LZ4压缩格式
- **丰富的构建选项**：提供多种构建选项以满足不同需求
- **构建设置持久化**：自动保存构建设置，跨会话保持配置一致性
- **构建结果处理**：支持将构建结果复制到StreamingAssets文件夹等功能

## 重要接口和类

### `AssetBundleBuilder` 类
主要的编辑器窗口类，提供AssetBundle构建的界面和逻辑。

| 方法 | 说明 |
|------|------|
| `Open()` | 打开AssetBundle构建窗口的静态方法 |
| `OnGUI()` | 绘制编辑器窗口的GUI |
| `BuildAssetBundle()` | 根据当前设置执行AssetBundle的构建 |
| `CopyDirectory(string sourceDir, string destDir)` | 将构建结果从源目录复制到目标目录 |

### `BuildTabData` 类
存储AssetBundle构建设置的数据类，支持序列化。

| 属性/字段 | 说明 |
|---------|------|
| `outputPath` | AssetBundle构建输出路径 |
| `buildTarget` | 目标构建平台 |
| `compressionType` | 压缩类型（无压缩、LZMA、LZ4） |
| `copy2StreamingAssets` | 是否将构建结果复制到StreamingAssets文件夹 |
| `disableWriteTypeTree` | 是否禁用类型树写入 |
| `forceRebuildAssetBundle` | 是否强制重新构建所有AssetBundle |
| `ignoreTypeTreeChanges` | 是否忽略类型树变化 |
| `appendHashToAssetBundleName` | 是否为AssetBundle名称附加哈希 |
| `strictMode` | 是否启用严格模式 |
| `dryRunBuild` | 是否执行构建预演(不实际生成文件) |

### `AssetInfo` 和 `AssetsInfo` 类
用于在构建过程中保存和管理资源信息的辅助类。

| 类/方法 | 说明 |
|---------|------|
| `AssetInfo` | 存储单个资源的路径和AssetBundle名称 |
| `AssetsInfo` | 包含多个AssetInfo的集合类 |

## UML类图

```
+--------------------+       +--------------------+
| AssetBundleBuilder |------>|    BuildTabData    |
+--------------------+       +--------------------+
| -data: BuildTabData|       | +outputPath: string|
| -scroll: Vector2   |       | +buildTarget       |
| +Open()            |       | +compressionType   |
| +OnGUI()           |       | +copy2StreamingAssets|
| +BuildAssetBundle()|       | +disableWriteTypeTree|
| +CopyDirectory()   |       | +forceRebuildAssetBundle|
+--------------------+       | +ignoreTypeTreeChanges|
        |                    | +appendHashToAssetBundleName|
        |                    | +strictMode        |
        |                    | +dryRunBuild       |
        |                    +--------------------+
        |                       ^
        |                       | 使用
        v                       |
+--------------------+       +--------------------+
|     AssetsInfo     |<------|     AssetInfo      |
+--------------------+       +--------------------+
| +list: List<>      |       | +path: string      |
| +AssetsInfo()      |       | +abName: string    |
+--------------------+       | +AssetInfo()       |
                            +--------------------+

+--------------------+
|    GUIContents     |
+--------------------+
| +copy2StreamingAssets|
| +disableWriteTypeTree|
| +forceRebuildAssetBundle|
| +ignoreTypeTreeChanges|
| +appendHash        |
| +strictMode        |
| +dryRunBuild       |
+--------------------+
```

## 构建流程图

```
配置构建参数 --> 设置目标平台 --> 选择输出路径 --> 配置压缩类型 --> 设置构建选项 --> 点击"Build"按钮 --> 执行BuildAssetBundle() --> 生成资源映射表 --> 复制到StreamingAssets(可选)
```

## 思维导图

```
AssetBundleBuilder
├── 界面组件
│   ├── 构建设置
│   │   ├── 目标平台选择
│   │   ├── 输出路径配置
│   │   └── 压缩方式选择
│   ├── 构建选项
│   │   ├── 复制到StreamingAssets
│   │   ├── 禁用类型树写入
│   │   ├── 强制重新构建
│   │   ├── 忽略类型树变化
│   │   ├── 添加哈希值
│   │   ├── 严格模式
│   │   └── 构建预演
│   └── 操作按钮
│       └── 构建按钮
├── 数据管理
│   ├── BuildTabData
│   │   ├── 序列化保存
│   │   └── 反序列化加载
│   └── 资源信息
│       ├── AssetInfo
│       └── AssetsInfo
├── 构建流程
│   ├── 参数验证
│   ├── 构建选项转换
│   ├── AssetBundle构建
│   ├── 资源映射表生成
│   └── 构建后处理
│       └── 复制到StreamingAssets
└── 工具辅助功能
    ├── 目录复制
    ├── 配置保存
    └── 界面提示
```

## 应用场景
1. **游戏资源打包**：为游戏打包AssetBundle，实现资源分离和动态加载
2. **多平台发布**：为不同目标平台构建优化的AssetBundle
3. **热更新支持**：准备可热更新的资源包，支持游戏内容的动态更新
4. **资源管理优化**：通过不同的构建选项优化资源加载性能和包体大小
5. **自动化构建流程**：集成到CI/CD系统中，实现资源的自动化构建

## 最佳实践
1. **平台特定设置**：针对不同目标平台选择最合适的压缩方式
   - 移动平台：推荐使用LZ4压缩，兼顾解压速度和包体大小
   - PC平台：如果优先考虑加载速度，可使用Uncompressed；如果优先考虑包体大小，可使用LZMA

2. **构建选项指南**：
   - `forceRebuildAssetBundle`：在资源有重大变化时使用，确保完全重新构建
   - `appendHashToAssetBundleName`：在实现热更新系统时使用，便于版本控制
   - `ignoreTypeTreeChanges`：在开发阶段可启用，加快迭代速度；正式发布前应禁用

3. **目录结构规划**：
   - 为不同平台构建结果使用不同的输出目录
   - 使用有意义的命名约定，便于识别和管理

4. **资源映射管理**：
   - 妥善保管和版本控制map.dat文件
   - 确保资源加载系统能正确解析资源映射表

5. **StreamingAssets使用**：
   - 开发测试阶段，启用copy2StreamingAssets选项便于在编辑器中测试
   - 发布前，根据实际部署策略决定是否需要此选项

## 构建设置说明

### 基本设置
- **Build Target**：选择目标构建平台
- **Output Path**：设置AssetBundle的输出路径
- **Compression**：选择压缩类型
  - `Uncompressed`：无压缩，文件较大但加载速度最快
  - `LZMA`：高压缩率，文件最小但解压缩较慢
  - `LZ4`：平衡的压缩率和解压缩速度，推荐用于大多数情况

### 选项设置
- **Copy to StreamingAssets**：构建完成后将AssetBundle复制到StreamingAssets文件夹
- **Disable Write Type Tree**：禁用类型树信息写入，减小包体但牺牲兼容性
- **Force Rebuild AssetBundle**：强制重新构建所有AssetBundle，忽略缓存
- **Ignore Type Tree Changes**：忽略类型树变化，加快增量构建
- **Append Hash To AssetBundle Name**：为AssetBundle名称附加内容哈希，便于版本控制
- **Strict Mode**：启用严格模式，对资源引用进行严格检查
- **Dry Run Build**：执行构建预演，不实际生成文件

## 代码示例
```csharp
// 如何通过代码触发AssetBundle构建
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleBuilderUtility
{
    [MenuItem("Tools/Build AssetBundles/Windows")]
    public static void BuildAssetBundlesForWindows()
    {
        // 设置输出路径
        string outputPath = Path.Combine(Application.dataPath, "../AssetBundles/Windows");
        
        // 确保输出目录存在
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        
        // 构建AssetBundle
        BuildPipeline.BuildAssetBundles(
            outputPath,
            BuildAssetBundleOptions.ChunkBasedCompression, // 使用LZ4压缩
            BuildTarget.StandaloneWindows64
        );
        
        Debug.Log("Windows AssetBundles built successfully: " + outputPath);
        
        // 刷新资源数据库
        AssetDatabase.Refresh();
    }
    
    [MenuItem("Tools/Build AssetBundles/Android")]
    public static void BuildAssetBundlesForAndroid()
    {
        // 设置输出路径
        string outputPath = Path.Combine(Application.dataPath, "../AssetBundles/Android");
        
        // 确保输出目录存在
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        
        // 构建AssetBundle
        BuildPipeline.BuildAssetBundles(
            outputPath,
            BuildAssetBundleOptions.ChunkBasedCompression, // 使用LZ4压缩
            BuildTarget.Android
        );
        
        Debug.Log("Android AssetBundles built successfully: " + outputPath);
        
        // 刷新资源数据库
        AssetDatabase.Refresh();
    }
}
```

## 相关资源
- [Unity文档: AssetBundle](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)
- [Unity文档: BuildPipeline.BuildAssetBundles](https://docs.unity3d.com/ScriptReference/BuildPipeline.BuildAssetBundles.html)
- [Unity文档: BuildAssetBundleOptions](https://docs.unity3d.com/ScriptReference/BuildAssetBundleOptions.html)
- [Unity文档: EditorWindow](https://docs.unity3d.com/ScriptReference/EditorWindow.html)
