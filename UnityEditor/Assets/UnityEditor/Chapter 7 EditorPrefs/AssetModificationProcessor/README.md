# AssetModificationProcessor 模块

## 概述
AssetModificationProcessor 模块提供了一套用于监控和干预Unity资源修改流程的工具。通过继承Unity内置的AssetModificationProcessor类，可以在资源创建、移动、删除和保存等操作前后插入自定义逻辑，实现对资源变更的监控、验证和自动化处理。该模块展示了如何为新创建的脚本自动添加统一的头部注释模板，以及如何在移动包含脚本的文件夹时进行确认提示，帮助团队维护项目资源的一致性和规范性。

## 核心功能
- **资源创建干预**：在创建新资源时执行自定义逻辑
- **资源移动监控**：在移动资源前进行确认和验证
- **代码规范自动化**：为新创建的脚本文件自动添加统一的头部注释
- **资源操作验证**：对资源操作进行验证和确认，防止误操作

## 重要接口和类

### `AssetModificationProcessor` 类
Unity内置的资源修改监听基类，提供各种资源操作的回调方法。

| 静态方法 | 说明 |
|---------|------|
| `OnWillCreateAsset(string path)` | 在资产创建前调用，可用于修改新创建的资产内容 |
| `OnWillMoveAsset(string sourcePath, string destinationPath)` | 在资产移动前调用，可用于验证移动操作或提供自定义移动逻辑 |
| `OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)` | 在资产删除前调用，可用于验证删除操作 |
| `OnWillSaveAssets(string[] paths)` | 在资产保存前调用，可用于修改将要保存的资产内容 |

### `ScriptsHeader` 类
实现了自动为新创建的脚本文件添加统一头部注释的功能。

| 静态方法 | 说明 |
|---------|------|
| `OnWillCreateAsset(string path)` | 重写基类方法，在脚本文件创建时自动添加头部注释模板 |

### `OnWillMoveAssetPrompt` 类
实现了在移动包含脚本的文件夹时进行确认提示的功能。

| 静态方法 | 说明 |
|---------|------|
| `OnWillMoveAsset(string sourcePath, string destinationPath)` | 重写基类方法，在移动包含脚本的文件夹时弹出确认对话框 |

## UML类图

```
+-----------------------------+
| UnityEditor.AssetModification|
|         Processor           |
+-----------------------------+
| +OnWillCreateAsset()        |
| +OnWillMoveAsset()          |
| +OnWillDeleteAsset()        |
| +OnWillSaveAssets()         |
+-----------------------------+
           ^
           |
+-----------------------------+    +-----------------------------+
|       ScriptsHeader         |    |   OnWillMoveAssetPrompt    |
+-----------------------------+    +-----------------------------+
| -author: string             |    | +OnWillMoveAsset()         |
| -email: string              |    |                            |
| -firstVersion: string       |    |                            |
| +OnWillCreateAsset()        |    |                            |
+-----------------------------+    +-----------------------------+
```

## 资源修改流程图

```
Unity编辑器 --> 触发资源操作 --> AssetModificationProcessor拦截 --> 执行自定义逻辑 --> 返回操作结果 --> Unity继续或取消操作
```

## 思维导图

```
AssetModificationProcessor
├── 资源操作拦截点
│   ├── 创建资源 (OnWillCreateAsset)
│   │   └── 脚本文件头部注释添加
│   ├── 移动资源 (OnWillMoveAsset)
│   │   └── 包含脚本的文件夹移动确认
│   ├── 删除资源 (OnWillDeleteAsset)
│   │   └── 重要资源删除验证
│   └── 保存资源 (OnWillSaveAssets)
│       └── 保存前资源内容修改
├── 应用场景
│   ├── 代码规范强制执行
│   │   ├── 统一头部注释
│   │   └── 命名规范验证
│   ├── 资源管理安全
│   │   ├── 重要资源保护
│   │   └── 操作确认机制
│   ├── 工作流程自动化
│   │   ├── 自动元数据生成
│   │   └── 依赖关系维护
│   └── 团队协作支持
│       ├── 资源变更记录
│       └── 冲突预防机制
└── 实现技术
    ├── 静态方法回调
    ├── 文件IO操作
    └── 编辑器UI交互
```

## 应用场景
1. **代码规范自动化**：自动为新创建的脚本添加统一的头部注释、版权信息和作者信息
2. **资源命名规范检查**：在资源创建或重命名时验证是否符合团队的命名规范
3. **重要资源保护**：防止意外删除或移动关键资源，如主场景、配置文件等
4. **资源变更日志**：自动记录项目中资源的创建、修改、移动和删除操作
5. **自动化导入处理**：与AssetPostprocessor配合，实现完整的资源生命周期管理

## 最佳实践
1. **轻量级处理**：在回调方法中避免执行耗时操作，以免影响编辑器性能
2. **精确目标资源**：只对特定类型或路径的资源应用处理逻辑，避免过度干预
3. **提供操作反馈**：在执行自动化操作时，为用户提供清晰的反馈和日志
4. **妥善处理异常**：添加适当的错误处理，避免因处理器异常导致编辑器操作中断
5. **保持向后兼容**：考虑项目演进过程中的向后兼容性，避免破坏现有资源结构

## 扩展示例
以下是一个扩展示例，用于在删除重要资源时进行确认和记录：

```csharp
public class ImportantAssetProtector : AssetModificationProcessor
{
    // 定义重要资源路径列表
    private static readonly string[] importantPaths = new string[]
    {
        "Assets/Scenes/Main.unity",
        "Assets/Resources/GameConfig.asset",
        "Assets/Scripts/Core"
    };
    
    public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
    {
        // 检查是否为重要资源
        foreach (string path in importantPaths)
        {
            if (assetPath == path || assetPath.StartsWith(path + "/"))
            {
                // 记录删除尝试
                Debug.LogWarning($"尝试删除重要资源: {assetPath}");
                
                // 弹出确认对话框
                bool confirmDelete = EditorUtility.DisplayDialog(
                    "警告",
                    $"您正在删除重要资源: {assetPath}\n此操作可能会影响项目的正常运行。确定要继续吗？",
                    "确认删除",
                    "取消"
                );
                
                if (!confirmDelete)
                {
                    return AssetDeleteResult.FailedDelete;
                }
                
                // 记录已确认的删除操作
                Debug.Log($"已确认删除重要资源: {assetPath}");
                
                // 可以在这里添加删除记录到外部日志或通知系统
                
                break;
            }
        }
        
        // 允许删除
        return AssetDeleteResult.DidNotDelete;
    }
}
```

## 代码示例
```csharp
// 自动为所有保存的C#脚本添加或更新最后修改时间
public class ScriptUpdateTracker : AssetModificationProcessor
{
    private static string[] OnWillSaveAssets(string[] paths)
    {
        foreach (string path in paths)
        {
            if (!path.EndsWith(".cs"))
                continue;
                
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);
            if (!File.Exists(fullPath))
                continue;
                
            string content = File.ReadAllText(fullPath);
            
            // 查找和更新最后修改时间注释
            string timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string pattern = @"// Last Modified: .*";
            string replacement = $"// Last Modified: {timeStamp}";
            
            if (System.Text.RegularExpressions.Regex.IsMatch(content, pattern))
            {
                // 更新现有的时间戳
                content = System.Text.RegularExpressions.Regex.Replace(content, pattern, replacement);
            }
            else
            {
                // 添加新的时间戳在第一个非空行后面
                string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                int insertIndex = 0;
                
                for (int i = 0; i < lines.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(lines[i]))
                    {
                        insertIndex = i + 1;
                        break;
                    }
                }
                
                List<string> newLines = new List<string>(lines);
                newLines.Insert(insertIndex, replacement);
                content = string.Join(Environment.NewLine, newLines);
            }
            
            File.WriteAllText(fullPath, content);
        }
        
        return paths;
    }
}
```

## 相关资源
- [Unity文档: AssetModificationProcessor](https://docs.unity3d.com/ScriptReference/AssetModificationProcessor.html)
- [Unity文档: AssetDatabase](https://docs.unity3d.com/ScriptReference/AssetDatabase.html)
- [Unity文档: EditorUtility](https://docs.unity3d.com/ScriptReference/EditorUtility.html)
- [Unity文档: File I/O](https://docs.microsoft.com/en-us/dotnet/api/system.io.file)
