# AssetPostprocessor 模块

## 概述
AssetPostprocessor 模块提供了Unity资源导入过程中的自动化处理工具，特别是针对纹理资源的预处理和后处理。该模块允许开发者自定义资源导入参数，确保项目中的所有资源遵循统一的导入设置，从而提高项目的一致性和工作效率。

## 核心功能
- **自动化纹理资源导入处理**：在资源导入Unity编辑器时自动应用预设的导入参数
- **统一的纹理设置管理**：通过ScriptableObject集中管理纹理导入参数
- **自定义资源处理流程**：在资源导入的不同阶段进行干预和自定义处理

## 重要接口和类

### `AssetPostprocessor` 类
Unity内置的资源处理基类，提供资源导入各阶段的钩子方法。

| 方法 | 说明 |
|------|------|
| `OnPreprocessTexture()` | 在纹理导入之前触发，可以修改纹理导入器的设置 |
| `OnPostprocessTexture(Texture2D texture)` | 纹理导入完成后触发，可以修改已导入的纹理 |
| `OnPostprocessGameObjectWithUserProperties(GameObject go, string[] propNames, object[] values)` | 处理带有自定义属性的游戏对象导入 |
| `OnPostprocessMeshHierarchy(GameObject root)` | 处理导入的网格层级结构 |
| `OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)` | 所有资源处理完成后的回调方法 |

### `TexturePostprocessor` 类
继承自 `AssetPostprocessor`，专门处理纹理资源的导入。

| 方法 | 说明 |
|------|------|
| `OnPreprocessTexture()` | 在纹理导入前应用自定义的纹理导入设置 |

### `TexturePreprocessSettings` 类
ScriptableObject类型，用于定义和存储纹理导入的默认参数。

| 属性 | 说明 |
|------|------|
| `TextureType` | 纹理类型（默认、法线贴图、编辑器GUI等） |
| `TextureShape` | 纹理形状（2D、3D、Cube等） |
| `SRGBTexture` | 是否为sRGB纹理 |
| `MaxSize` | 最大纹理尺寸 |
| `Compression` | 压缩方式 |
| ... | 更多纹理导入相关设置 |

## UML类图

```
+--------------------+        +-------------------------+
|  AssetPostprocessor |<------| TexturePostprocessor   |
+--------------------+        +-------------------------+
| #assetImporter     |        | -OnPreprocessTexture() |
| #assetPath         |        +-------------------------+
|                    |                    ^
| +OnPreprocessXXX() |                    |
| +OnPostprocessXXX()|                    | 使用
+--------------------+                    |
                                          v
                            +---------------------------+
                            | TexturePreprocessSettings |
                            +---------------------------+
                            | -textureType              |
                            | -textureShape             |
                            | -sRGBTexture              |
                            | ...                       |
                            | +static Settings          |
                            | +static TextureType       |
                            | +static TextureShape      |
                            | ...                       |
                            +---------------------------+
```

## 工作流程图

```
导入纹理资源 --> OnPreprocessTexture() --> 应用TexturePreprocessSettings配置 --> Unity内部处理 --> OnPostprocessTexture() --> 完成导入
```

## 思维导图

```
AssetPostprocessor
├── 处理时机
│   ├── 预处理 (OnPreprocess系列方法)
│   │   └── 修改导入参数
│   └── 后处理 (OnPostprocess系列方法)
│       └── 修改已导入资源
├── 纹理处理 (TexturePostprocessor)
│   ├── 设置纹理类型
│   ├── 设置压缩方式
│   ├── 设置Mipmap生成
│   └── 设置其他参数
└── 设置管理 (TexturePreprocessSettings)
    ├── ScriptableObject存储
    ├── 统一项目配置
    ├── 编辑器界面配置
    └── 运行时读取
```

## 应用场景
1. **自动化资源导入工作流**: 确保所有团队成员导入的纹理资源遵循相同的参数设置
2. **针对特定平台优化**: 根据目标平台自动调整纹理的压缩格式和质量设置
3. **特殊资源处理**: 对特定路径或命名规则的资源应用特殊的导入设置
4. **内容流水线集成**: 在大型项目中集成到资源管理流水线

## 最佳实践
1. **模块化的处理器**: 为不同类型的资源创建专门的处理器类
2. **基于路径的规则**: 使用资源路径来决定应用哪些导入设置
3. **性能考虑**: 处理器中的代码应当高效，避免在导入阶段引入长时间操作
4. **记录和通知**: 考虑为重要的资源处理操作添加日志或编辑器通知
5. **集中配置**: 使用ScriptableObject存储配置，便于统一管理和版本控制

## 代码示例
```csharp
// 基于文件名应用不同的纹理设置
public class AdvancedTextureProcessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter importer = assetImporter as TextureImporter;
        
        if (assetPath.Contains("_normal"))
        {
            importer.textureType = TextureImporterType.NormalMap;
        }
        else if (assetPath.Contains("_mask"))
        {
            importer.textureType = TextureImporterType.Default;
            importer.alphaIsTransparency = true;
        }
    }
}
```

## 相关资源
- [Unity文档: AssetPostprocessor](https://docs.unity3d.com/ScriptReference/AssetPostprocessor.html)
- [Unity文档: TextureImporter](https://docs.unity3d.com/ScriptReference/TextureImporter.html)
- [Unity文档: ScriptableObject](https://docs.unity3d.com/ScriptReference/ScriptableObject.html)
