# ResourceComponent 模块

## 概述
ResourceComponent 模块是一个强大的资源管理系统，用于统一处理Unity项目中的资源加载、卸载和管理。该组件支持多种运行模式，包括编辑器模式、模拟模式和真实环境模式，可以无缝切换不同的资源来源，为游戏开发提供了灵活的资源管理解决方案。该组件的主要功能包括资源映射表管理、AssetBundle加载和依赖处理、场景异步加载以及资源释放管理等。

## 核心功能
- **多模式资源加载**：支持编辑器模式、模拟模式和真实环境模式
- **资源映射管理**：通过映射表建立资源路径与AssetBundle的关联
- **AssetBundle管理**：处理AssetBundle的加载、卸载和依赖关系
- **场景异步加载**：支持场景的异步加载和卸载
- **资源生命周期管理**：提供完整的资源加载和卸载API

## 重要接口和类

### `ResourceComponent` 类
核心资源管理组件，提供资源加载和卸载的主要功能。

| 属性/方法 | 说明 |
|---------|------|
| `MODE` | 枚举，定义了资源加载的三种模式：EDITOR、SIMULATIVE、REALITY |
| `LoadAssetAsync<T>()` | 异步加载指定路径的资源，支持加载进度回调 |
| `LoadSceneAsync()` | 异步加载指定路径的场景，支持加载进度回调 |
| `UnloadAsset()` | 卸载指定路径的资源 |
| `UnloadAllAsset()` | 卸载所有已加载的资源 |
| `UnloadScene()` | 卸载指定路径的场景 |

### `AssetInfo` 类
表示单个资源的信息，包含资源名称、路径和所属的AssetBundle包名称。

| 属性/方法 | 说明 |
|---------|------|
| `name` | 资源名称，不包含扩展名 |
| `path` | 资源在项目中的完整路径 |
| `abName` | 资源所属的AssetBundle包名称 |
| `ToString()` | 返回资产信息的字符串表示 |

### `AssetsInfo` 类
包含多个`AssetInfo`的集合，用于序列化和反序列化资源映射表。

| 属性/方法 | 说明 |
|---------|------|
| `list` | 存储所有`AssetInfo`的列表 |

## UML类图

```
+-------------------+       +------------------+       +-------------+
| ResourceComponent |       |   AssetsInfo     |       |  AssetInfo  |
+-------------------+       +------------------+       +-------------+
| -mode: MODE       |       | +list: List<>    |------>| +name: string|
| -assetBundleUrl   |       +------------------+       | +path: string|
| -map: Dictionary  |<------------------------------>| +abName:string|
| -assetBundles: Dic|                                  +-------------+
| -scenes: Dictionary|
| +LoadAssetAsync<T>|
| +LoadSceneAsync   |
| +UnloadAsset      |
| +UnloadAllAsset   |
| +UnloadScene      |
+-------------------+

+----------------+
|      MODE      |
+----------------+
| EDITOR         |
| SIMULATIVE     |
| REALITY        |
+----------------+
```

## 资源加载流程图

```
资源请求 --> 检查映射表 --> 检查加载模式
    |
    +--> [EDITOR模式] --> 使用AssetDatabase直接加载
    |
    +--> [SIMULATIVE/REALITY模式] --> 查找AssetBundle --> 加载依赖项 --> 加载AssetBundle --> 从AssetBundle加载资源
```

## 思维导图

```
ResourceComponent
├── 资源管理模式
│   ├── 编辑器模式 (EDITOR)
│   │   └── 直接从AssetDatabase加载
│   ├── 模拟模式 (SIMULATIVE)
│   │   └── 从StreamingAssets加载AssetBundle
│   └── 真实环境模式 (REALITY)
│       └── 从自定义URL加载AssetBundle
├── 资源映射管理
│   ├── 资源映射表 (map.dat)
│   │   ├── 资源路径到AssetBundle的映射
│   │   └── 基于AssetsInfo和AssetInfo的序列化
│   └── 映射表加载
│       └── 从StreamingAssets或指定URL下载
├── AssetBundle管理
│   ├── AssetBundle清单
│   │   └── AssetBundleManifest
│   ├── 依赖加载
│   │   └── 递归加载所有依赖项
│   └── AssetBundle缓存
│       └── 避免重复加载
├── 资源操作接口
│   ├── 资源加载
│   │   ├── LoadAssetAsync<T>
│   │   └── 支持加载进度回调
│   ├── 场景加载
│   │   ├── LoadSceneAsync
│   │   └── 支持加载进度回调
│   └── 资源卸载
│       ├── UnloadAsset
│       ├── UnloadAllAsset
│       └── UnloadScene
└── 异步操作支持
    ├── 协程实现
    ├── 进度回调
    └── 完成回调
```

## 应用场景
1. **大型游戏资源管理**：为大型游戏提供统一的资源管理接口，简化资源操作
2. **热更新支持**：通过切换资源加载模式，支持游戏资源热更新
3. **资源加载优化**：自动管理AssetBundle依赖关系，避免重复加载
4. **开发与发布无缝切换**：在开发阶段使用编辑器模式，发布时自动切换到真实环境模式
5. **场景管理**：提供场景的异步加载和卸载功能，支持多场景管理

## 最佳实践
1. **资源命名与组织**：建立清晰的资源命名和路径组织规范，便于管理
2. **AssetBundle打包策略**：根据游戏需求合理规划AssetBundle的打包粒度
3. **依赖关系管理**：注意资源间的依赖关系，避免循环依赖
4. **异步加载使用**：对于大型资源或场景，始终使用异步加载避免卡顿
5. **资源释放及时性**：及时释放不需要的资源，避免内存泄漏
6. **预加载关键资源**：对关键资源进行预加载，提高游戏响应速度

## 代码示例
```csharp
// 1. 组件初始化
ResourceComponent resourceManager;

void Start() 
{
    resourceManager = GetComponent<ResourceComponent>();
}

// 2. 异步加载纹理资源
public void LoadTexture(string path) 
{
    resourceManager.LoadAssetAsync<Texture2D>(path, 
        (success, texture) => {
            if (success && texture != null) {
                // 使用加载的纹理
                myRenderer.material.mainTexture = texture;
            }
        },
        (progress) => {
            // 更新加载进度
            loadingBar.value = progress;
        }
    );
}

// 3. 异步加载场景
public void LoadGameLevel(string scenePath) 
{
    resourceManager.LoadSceneAsync(scenePath,
        (success) => {
            if (success) {
                // 场景加载完成后的操作
                HideLoadingScreen();
            }
        },
        (progress) => {
            // 更新场景加载进度
            sceneLoadingBar.value = progress;
        }
    );
}

// 4. 释放资源
public void ReleaseResources() 
{
    // 卸载特定资源
    resourceManager.UnloadAsset("Assets/Textures/background.png");
    
    // 在场景转换后卸载所有资源
    resourceManager.UnloadAllAsset(true);
}
```

## 相关资源
- [Unity文档: AssetBundle](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)
- [Unity文档: 资源管理](https://docs.unity3d.com/Manual/SpecialFolders.html)
- [Unity文档: 场景管理](https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html)
- [Unity文档: AssetDatabase](https://docs.unity3d.com/ScriptReference/AssetDatabase.html)
