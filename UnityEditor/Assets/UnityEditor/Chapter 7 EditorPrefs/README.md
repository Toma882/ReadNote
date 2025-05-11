# Unity编辑器扩展 - Chapter 7: EditorPrefs

## 概述

本章专注于Unity编辑器中的EditorPrefs系统和资源管理相关功能。EditorPrefs是Unity提供的编辑器持久化系统，用于在编辑器会话之间保存和读取设置。同时，本章还涵盖了资源处理的多个方面，包括资源导入处理、资源修改监控、资源打包和配置等。这些功能对于创建稳健的编辑器工具和优化开发工作流至关重要。

## 核心知识点

### EditorPrefs系统概览

EditorPrefs系统提供了在编辑器会话之间持久化数据的功能，主要用于：

- **编辑器设置保存** - 保存用户的偏好设置和工作环境
- **工具状态记忆** - 记住工具窗口的状态和配置
- **开发团队配置共享** - 在团队成员之间共享编辑器设置

### 资源管理系统概览

Unity的资源管理系统包含多个组件，本章涉及：

- **AssetPostprocessor** - 资源导入过程钩子，用于自动化处理资源导入
- **AssetModificationProcessor** - 资源修改监控，用于跟踪资源变化
- **AssetBundle** - 资源打包系统，用于分发游戏内容
- **资源组件化** - 将资源组织成可重用的组件
- **AssetDatabase** - 项目资源管理核心，提供资源CRUD操作和导入控制

## 相关特性

### EditorPrefs类

- **基本用法**: `EditorPrefs.SetString(string key, string value)`, `EditorPrefs.GetString(string key, string defaultValue)`
- **用途**: 在编辑器会话之间保存和读取设置
- **支持的数据类型**: 
  - `int` (SetInt/GetInt)
  - `float` (SetFloat/GetFloat)
  - `string` (SetString/GetString)
  - `bool` (SetBool/GetBool)
- **示例**:  
  ```csharp
  // 保存设置
  EditorPrefs.SetString("LastProjectPath", projectPath);
  EditorPrefs.SetBool("AutoSave", true);
  
  // 读取设置
  string lastPath = EditorPrefs.GetString("LastProjectPath", "");
  bool autoSave = EditorPrefs.GetBool("AutoSave", false);
  ```

### AssetDatabase类

- **基本用法**: 静态类，直接通过类名调用方法
- **用途**: 管理项目资源，提供资源的创建、加载、保存、导入、刷新等功能
- **主要功能分类**:
  - 资源导入控制
  - 资源创建和保存
  - 资源查询和加载
  - 资源依赖和引用关系
  - 资源序列化和反序列化
- **示例**:  
  ```csharp
  // 创建资源
  Material newMaterial = new Material(Shader.Find("Standard"));
  AssetDatabase.CreateAsset(newMaterial, "Assets/Materials/NewMaterial.mat");
  
  // 加载资源
  Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/MyTexture.png");
  
  // 刷新资源数据库
  AssetDatabase.Refresh();
  ```

#### AssetDatabase 重要接口

| 接口/方法 | 功能描述 | 参数说明 | 返回值 | 使用场景 |
|----------|---------|---------|-------|----------|
| `CreateAsset` | 创建新资源 | `(Object asset, string path)` | `void` | 创建ScriptableObject或Material等资源 |
| `SaveAssets` | 保存所有修改的资源 | 无 | `void` | 批量操作资源后保存更改 |
| `Refresh` | 刷新资源数据库 | `(ImportAssetOptions options = ImportAssetOptions.Default)` | `void` | 外部修改资源后刷新视图 |
| `ImportAsset` | 导入特定资源 | `(string path, ImportAssetOptions options = ImportAssetOptions.Default)` | `void` | 手动触发资源导入过程 |
| `LoadAssetAtPath` | 加载特定路径的资源 | `<T>(string assetPath)` | `T` | 读取项目中的资源 |
| `LoadAllAssetsAtPath` | 加载路径下所有资源 | `(string assetPath)` | `Object[]` | 加载复合资源（如FBX）中的全部子资源 |
| `GetAssetPath` | 获取资源路径 | `(Object assetObject)` | `string` | 根据资源对象获取其在项目中的路径 |
| `AssetPathToGUID` | 路径转GUID | `(string path)` | `string` | 获取资源的唯一标识符 |
| `GUIDToAssetPath` | GUID转路径 | `(string guid)` | `string` | 通过唯一标识符查找资源路径 |
| `FindAssets` | 查找符合条件的资源 | `(string filter, string[] searchInFolders = null)` | `string[]` | 搜索特定类型或名称的资源 |
| `GetDependencies` | 获取资源依赖 | `(string assetPath, bool recursive = true)` | `string[]` | 分析资源的依赖关系 |
| `CopyAsset` | 复制资源 | `(string path, string newPath)` | `bool` | 复制已有资源到新位置 |
| `MoveAsset` | 移动资源 | `(string oldPath, string newPath)` | `string` | 移动资源到新位置，返回错误信息或空字符串 |
| `DeleteAsset` | 删除资源 | `(string path)` | `bool` | 从项目中删除资源 |
| `RenameAsset` | 重命名资源 | `(string pathName, string newName)` | `string` | 重命名资源，返回错误信息或空字符串 |
| `StartAssetEditing` | 开始批量编辑 | 无 | `void` | 批量操作资源前调用，提高性能 |
| `StopAssetEditing` | 结束批量编辑 | 无 | `void` | 批量操作资源后调用，恢复正常状态 |
| `IsValidFolder` | 检查文件夹是否有效 | `(string path)` | `bool` | 检验文件夹路径是否存在且有效 |
| `CreateFolder` | 创建文件夹 | `(string parentFolder, string newFolderName)` | `string` | 在项目中创建新文件夹 |
| `ExportPackage` | 导出资源包 | `(string assetPathName, string fileName, ExportPackageOptions options = ExportPackageOptions.Default)` | `void` | 将资源导出为Unity包 |

### AssetPostprocessor类

- **基本用法**: 继承AssetPostprocessor并重写其处理方法
- **用途**: 自动化处理资源导入过程
- **关键方法**:
  - `OnPreprocessTexture` - 纹理导入前处理
  - `OnPostprocessTexture` - 纹理导入后处理
  - `OnPreprocessModel` - 模型导入前处理
  - `OnPostprocessModel` - 模型导入后处理
  - `OnPostprocessAllAssets` - 所有资源导入后处理
- **示例**:  
  ```csharp
  public class TextureProcessor : AssetPostprocessor
  {
      void OnPreprocessTexture()
      {
          TextureImporter importer = assetImporter as TextureImporter;
          if (assetPath.Contains("UI/"))
          {
              importer.textureType = TextureImporterType.Sprite;
          }
      }
  }
  ```

### AssetModificationProcessor类

- **基本用法**: 继承AssetModificationProcessor并重写其处理方法
- **用途**: 监控资源文件的修改、创建、删除等操作
- **关键方法**:
  - `OnWillSaveAssets` - 资源保存前
  - `OnWillCreateAsset` - 资源创建前
  - `OnWillDeleteAsset` - 资源删除前
  - `OnWillMoveAsset` - 资源移动前
- **示例**:  
  ```csharp
  public class AssetTracker : AssetModificationProcessor
  {
      static string[] OnWillSaveAssets(string[] paths)
      {
          Debug.Log("Saving assets: " + string.Join(", ", paths));
          return paths;
      }
  }
  ```

### AssetBundle相关类

- **基本用法**: 使用BuildPipeline.BuildAssetBundles创建AssetBundle
- **用途**: 将游戏资源打包为可单独下载和加载的包
- **关键组件**:
  - `AssetBundleBuild` - 定义AssetBundle的构建信息
  - `BuildPipeline` - 提供构建AssetBundle的方法
  - `AssetDatabase` - 管理项目中的资源
- **示例**:  
  ```csharp
  public static void BuildAllAssetBundles(string outputPath)
  {
      if (!Directory.Exists(outputPath))
          Directory.CreateDirectory(outputPath);
      
      BuildPipeline.BuildAssetBundles(outputPath, 
          BuildAssetBundleOptions.None, 
          BuildTarget.StandaloneWindows64);
  }
  ```

## 代码结构

本章包含的主要代码文件：

1. **EditorPrefsUtility**: 提供EditorPrefs的扩展功能和使用示例
2. **AudioDatabase**: 使用EditorPrefs和ScriptableObject管理音频资源
3. **AssetPostprocessor示例**: 演示自动化处理资源导入
4. **AssetModificationProcessor示例**: 演示资源修改监控
5. **AssetBundleBuilder**: 提供资源打包工具
6. **AssetBundleConfigure**: 提供资源包配置工具
7. **BatchBuild**: 批量构建工具，使用EditorPrefs存储构建设置
8. **ResourceComponent**: 资源组件化示例

## 思维导图

```
Unity EditorPrefs和资源管理系统
├── EditorPrefs系统
│   ├── 基本操作
│   │   ├── 存储数据
│   │   │   ├── SetInt/SetFloat - 存储数值类型
│   │   │   ├── SetString - 存储字符串
│   │   │   └── SetBool - 存储布尔值
│   │   ├── 读取数据
│   │   │   ├── GetInt/GetFloat - 读取数值类型
│   │   │   ├── GetString - 读取字符串
│   │   │   └── GetBool - 读取布尔值
│   │   └── 管理数据
│   │       ├── HasKey - 检查键是否存在
│   │       ├── DeleteKey - 删除特定键值对
│   │       └── DeleteAll - 删除所有EditorPrefs数据
│   │
│   ├── 应用场景
│   │   ├── 编辑器窗口状态
│   │   │   ├── 窗口位置和大小
│   │   │   ├── 选择的选项卡
│   │   │   └── 折叠状态
│   │   ├── 用户偏好设置
│   │   │   ├── 主题和颜色
│   │   │   ├── 默认路径
│   │   │   └── 自动保存设置
│   │   ├── 项目配置
│   │   │   ├── 构建设置
│   │   │   ├── 资源导入设置
│   │   │   └── 团队共享配置
│   │   └── 工具状态
│   │       ├── 最后使用的工具
│   │       ├── 工具参数
│   │       └── 批处理任务状态
│   │
│   └── 最佳实践
│       ├── 键命名规范
│       │   ├── 使用前缀避免冲突
│       │   ├── 分层次的键名设计
│       │   └── 一致的命名风格
│       ├── 数据管理
│       │   ├── 默认值的使用
│       │   ├── 数据验证和清理
│       │   └── 版本控制兼容性
│       └── 安全考虑
│           ├── 敏感数据处理
│           ├── 数据备份策略
│           └── 多用户环境处理
│
├── 资源处理系统
│   ├── AssetDatabase系统
│   │   ├── 资源CRUD操作
│   │   │   ├── CreateAsset - 创建新资源
│   │   │   ├── SaveAssets - 保存修改的资源
│   │   │   ├── LoadAssetAtPath - 加载特定路径资源
│   │   │   ├── CopyAsset - 复制资源
│   │   │   ├── MoveAsset - 移动资源
│   │   │   ├── RenameAsset - 重命名资源
│   │   │   └── DeleteAsset - 删除资源
│   │   ├── 资源导入与刷新
│   │   │   ├── ImportAsset - 导入单个资源
│   │   │   ├── Refresh - 刷新资源数据库
│   │   │   ├── StartAssetEditing - 开始批量编辑
│   │   │   └── StopAssetEditing - 结束批量编辑
│   │   ├── 资源查询与搜索
│   │   │   ├── FindAssets - 查找符合条件的资源
│   │   │   ├── GetAssetPath - 获取资源路径
│   │   │   ├── AssetPathToGUID - 路径转GUID
│   │   │   └── GUIDToAssetPath - GUID转路径
│   │   ├── 依赖关系管理
│   │   │   ├── GetDependencies - 获取资源依赖
│   │   │   ├── GetDependenciesAsync - 异步获取依赖
│   │   │   └── CanOpenForEdit - 检查资源是否可编辑
│   │   └── 资源包导入导出
│   │       ├── ExportPackage - 导出资源包
│   │       ├── ImportPackage - 导入资源包
│   │       └── ExtractAsset - 从资源包提取资源
│   │
│   ├── AssetPostprocessor
│   │   ├── 导入前处理
│   │   │   ├── OnPreprocessTexture - 纹理导入前处理
│   │   │   ├── OnPreprocessModel - 模型导入前处理
│   │   │   ├── OnPreprocessAudio - 音频导入前处理
│   │   │   └── 其他资源类型前处理
│   │   ├── 导入后处理
│   │   │   ├── OnPostprocessTexture - 纹理导入后处理
│   │   │   ├── OnPostprocessModel - 模型导入后处理
│   │   │   ├── OnPostprocessAudio - 音频导入后处理
│   │   │   └── OnPostprocessAllAssets - 批量资源后处理
│   │   └── 应用场景
│   │       ├── 自动设置导入参数
│   │       ├── 导入资源优化
│   │       ├── 资源命名规范检查
│   │       └── 自动生成元数据
│   │
│   ├── AssetModificationProcessor
│   │   ├── 资源修改监控
│   │   │   ├── OnWillSaveAssets - 资源保存前处理
│   │   │   ├── OnWillCreateAsset - 资源创建前处理
│   │   │   ├── OnWillDeleteAsset - 资源删除前处理
│   │   │   └── OnWillMoveAsset - 资源移动前处理
│   │   └── 应用场景
│   │       ├── 资源变更日志
│   │       ├── 资源命名和组织规范检查
│   │       ├── 依赖关系维护
│   │       └── 版本控制系统集成
│   │
│   ├── AssetBundle系统
│   │   ├── 资源包构建
│   │   │   ├── BuildPipeline.BuildAssetBundles - 构建资源包
│   │   │   ├── AssetBundleBuild结构 - 定义资源包内容
│   │   │   └── 构建选项与策略
│   │   ├── 资源包配置
│   │   │   ├── 资源标记与分组
│   │   │   ├── 依赖关系管理
│   │   │   └── 版本控制
│   │   └── 批量构建工具
│   │       ├── 多平台构建支持
│   │       ├── 增量构建策略
│   │       └── 构建报告生成
│   │
│   └── 资源组件化
│       ├── 组件设计
│       │   ├── 资源引用管理
│       │   ├── 预制体组合策略
│       │   └── 动态加载支持
│       ├── ScriptableObject应用
│       │   ├── 数据资源定义
│       │   ├── 编辑器工具集成
│       │   └── 运行时数据管理
│       └── 资源优化策略
│           ├── 内存占用优化
│           ├── 加载性能优化
│           └── 资源复用机制
```

## UML类图

```
+--------------------+         +-----------------------+         +-----------------------+
|     EditorPrefs    |         |   AssetPostprocessor  |         |AssetModificationProcessor|
+--------------------+         +-----------------------+         +-----------------------+
| + SetInt()         |         | # assetImporter       |         | + OnWillSaveAssets() |
| + GetInt()         |         | # assetPath           |         | + OnWillCreateAsset()|
| + SetFloat()       |         +-----------------------+         | + OnWillDeleteAsset()|
| + GetFloat()       |         | + OnPreprocessTexture()|        | + OnWillMoveAsset()  |
| + SetString()      |         | + OnPostprocessTexture()|       +-----------------------+
| + GetString()      |         | + OnPreprocessModel() |
| + SetBool()        |         | + OnPostprocessModel()|
| + GetBool()        |         | + OnPostprocessAllAssets()|
| + HasKey()         |         +-----------------------+
| + DeleteKey()      |
| + DeleteAll()      |
+--------------------+

+--------------------+         +-----------------------+         +-----------------------+
|  AssetDatabase     |         |  TextureProcessor     |         |   AssetTracker        |
+--------------------+         +-----------------------+         +-----------------------+
| + CreateAsset()    |         | + OnPreprocessTexture()|        | + OnWillSaveAssets() |
| + LoadAssetAtPath()|-------->| + OnPostprocessTexture()|------>| + OnWillCreateAsset()|
| + ImportAsset()    |         +-----------------------+         | + OnWillDeleteAsset()|
| + Refresh()        |                                           +-----------------------+
| + FindAssets()     |
| + GetDependencies()|
+--------------------+
         |
         | 使用
         v
+--------------------+         +-----------------------+         +-----------------------+
|EditorPrefsUtility  |         | AssetBundleBuilder    |         | ResourceComponent    |
+--------------------+         +-----------------------+         +-----------------------+
| + SaveVector3()    |         | - bundleBuilds        |         | - prefabReference    |
| + LoadVector3()    |<--------| - outputPath          |-------->| - materialReference  |
| + SaveColor()      |         | - buildTarget         |         | - textureReference   |
| + LoadColor()      |         +-----------------------+         +-----------------------+
| + SaveSettings()   |         | + BuildAllBundles()   |         | + LoadResources()    |
| + LoadSettings()   |         | + BuildSelected()     |         | + ReleaseResources() |
+--------------------+         | + GetBundleDependencies()|      | + GetResourceInfo()  |
                              +-----------------------+         +-----------------------+

+--------------------+
|   BatchBuild       |
+--------------------+
| - buildSettings    |
| - buildOptions     |
| - lastBuildTime    |
+--------------------+
| + BuildAll()       |
| + BuildPlatform()  |<---------使用-------+
| + SaveSettings()   |                     |
| + LoadSettings()   |                     |
+--------------------+                     |
                                          |
         +--------------------------+     |
         |      AudioDatabase       |     |
         +--------------------------+     |
         | - audioClips             |     |
         | - categories             |     |
         | - settings               |     |
         +--------------------------+     |
         | + SaveDatabase()         |-----+
         | + LoadDatabase()         |
         | + ImportAudio()          |
         +--------------------------+
```

## 重要类和接口

### EditorPrefs相关类

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|----------|------|------|--------------|
| EditorPrefs | 静态类 | Unity编辑器持久化数据存储系统 | `SetInt()`, `GetInt()`, `SetString()`, `GetString()`, `DeleteKey()` |
| EditorPrefsUtility | 工具类 | EditorPrefs功能扩展 | `SaveVector3()`, `LoadVector3()`, `SaveSettings()`, `LoadSettings()` |

### 资源处理相关类

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|----------|------|------|--------------|
| AssetPostprocessor | 抽象类 | 资源导入处理基类 | `OnPreprocessTexture()`, `OnPostprocessModel()`, `OnPostprocessAllAssets()` |
| AssetModificationProcessor | 抽象类 | 资源修改监控基类 | `OnWillSaveAssets()`, `OnWillCreateAsset()`, `OnWillDeleteAsset()` |
| BuildPipeline | 静态类 | 提供资源包和应用构建功能 | `BuildAssetBundles()`, `BuildPlayer()` |
| AssetDatabase | 静态类 | 提供资源数据库操作功能 | `CreateAsset()`, `LoadAssetAtPath()`, `ImportAsset()`, `Refresh()`, `SaveAssets()`, `GetAssetPath()`, `FindAssets()`, `GetDependencies()`, `StartAssetEditing()`, `StopAssetEditing()` |

### AssetDatabase主要功能分类

| 功能类别 | 描述 | 主要方法 |
|---------|------|---------|
| 资源创建与管理 | 创建、复制、移动、重命名和删除资源 | `CreateAsset()`, `CopyAsset()`, `MoveAsset()`, `RenameAsset()`, `DeleteAsset()`, `CreateFolder()` |
| 资源加载与查询 | 查找和加载项目中的资源 | `LoadAssetAtPath()`, `LoadAllAssetsAtPath()`, `FindAssets()`, `GetAssetPath()` |
| 资源导入与刷新 | 控制资源的导入过程与刷新 | `ImportAsset()`, `Refresh()`, `StartAssetEditing()`, `StopAssetEditing()` |
| 依赖管理 | 分析和获取资源的依赖关系 | `GetDependencies()`, `GetDependenciesAsync()` |
| 标识符与路径转换 | 在资源路径和GUID之间转换 | `AssetPathToGUID()`, `GUIDToAssetPath()` |
| 资源包管理 | 导入和导出资源包 | `ExportPackage()`, `ImportPackage()` |

### 应用示例类

| 类/接口名称 | 类型 | 描述 | 重要方法/属性 |
|----------|------|------|--------------|
| AudioDatabase | 编辑器工具 | 音频资源管理系统 | `SaveDatabase()`, `LoadDatabase()`, `ImportAudio()` |
| AssetBundleBuilder | 编辑器工具 | 资源包构建工具 | `BuildAllBundles()`, `BuildSelected()`, `GetBundleDependencies()` |
| BatchBuild | 编辑器工具 | 批量构建系统 | `BuildAll()`, `BuildPlatform()`, `SaveSettings()` |
| ResourceComponent | 组件类 | 资源引用和加载组件 | `LoadResources()`, `ReleaseResources()`, `GetResourceInfo()` |

## 应用场景

1. **编辑器工具状态保存**: 使用EditorPrefs记住窗口状态、用户偏好和工具设置
2. **资源自动化处理**: 使用AssetPostprocessor自动设置导入参数、优化资源和生成元数据
3. **工作流程规范化**: 使用AssetModificationProcessor实施命名和组织规范、跟踪资源变更
4. **资源包管理**: 创建用于资源包配置、构建和版本控制的工具
5. **批量构建系统**: 实现跨平台批量构建和自动化部署流程
6. **音频资源管理**: 创建音频资源数据库和运行时加载系统
7. **资源组件化**: 实现资源引用、预加载和释放的组件化管理

## 最佳实践

1. **EditorPrefs使用**:
   - 使用命名空间或前缀防止键名冲突
   - 总是提供合理的默认值
   - 注意在多用户环境中的数据隔离

2. **资源导入处理**:
   - 避免在导入处理器中执行耗时操作
   - 为不同类型的资源创建专用处理器
   - 使用条件逻辑根据命名或路径应用不同处理规则

3. **资源修改监控**:
   - 确保修改处理不阻塞用户操作
   - 添加撤销支持使修改可逆
   - 使用日志记录重要的资源变更

4. **资源包管理**:
   - 实施明确的包命名和分组策略
   - 优化包的粒度和依赖关系
   - 实现增量构建以减少构建时间

5. **批量构建**:
   - 使用版本号和构建元数据
   - 实现构建后验证步骤
   - 设计灵活的构建配置系统

6. **资源组件化**:
   - 遵循单一职责原则设计组件
   - 实现资源生命周期管理
   - 提供优雅的错误处理机制

## 相关资源

### 官方文档

- [Unity EditorPrefs 类参考](https://docs.unity3d.com/ScriptReference/EditorPrefs.html)
- [Unity AssetPostprocessor 类参考](https://docs.unity3d.com/ScriptReference/AssetPostprocessor.html)
- [Unity AssetModificationProcessor 类参考](https://docs.unity3d.com/ScriptReference/AssetModificationProcessor.html)
- [Unity AssetBundle 系统概述](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)
- [Unity BuildPipeline 类参考](https://docs.unity3d.com/ScriptReference/BuildPipeline.html)
- [Unity AssetDatabase 类参考](https://docs.unity3d.com/ScriptReference/AssetDatabase.html)
- [Unity 编辑器脚本化](https://docs.unity3d.com/Manual/ExtendingTheEditor.html)

### 教程与参考

- [Unity AssetBundle 最佳实践](https://unity.com/how-to/programming-unity-assetbundles)
- [优化资源导入工作流](https://blog.unity.com/technology/optimizing-unity-asset-import-workflow)
- [Unity 编辑器扩展基础](https://learn.unity.com/tutorial/editor-scripting)
- [Unity CI/CD 自动化构建](https://unity.com/how-to/set-cicd-pipeline-unity-projects)
- [资源管理系统设计](https://blog.unity.com/technology/asset-management-in-unity) 