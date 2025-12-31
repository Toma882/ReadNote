# Loader资源加载系统架构设计

## 设计目标

设计一套完整的资源加载系统，支持多种加载策略（Editor、AssetBundle、Resources），实现统一接口、策略切换、异步加载、对象池优化，提供灵活、高性能的资源管理解决方案。

---

## 核心设计理念

### 1. 策略模式为核心

**本质**：资源加载系统的核心是策略模式的应用
- 加载策略 = 不同的资源加载方式（Editor/AssetBundle/Resources）
- 策略切换 = 运行时动态切换加载策略
- 统一接口 = 所有策略通过统一接口访问
- 策略隔离 = 每个策略独立实现，互不干扰

### 2. 对象池优化 + 任务封装

**本质**：通过对象池管理LoadTask，通过任务封装简化异步加载
- 对象池管理 = LoadTask使用对象池，减少GC压力
- 任务封装 = LoadTask封装加载参数和回调
- 链式配置 = 流畅的API设计，易于使用
- 性能优化 = 对象池复用，减少对象创建开销

### 3. 职责分离 + 开闭原则

**本质**：加载逻辑和业务逻辑分离，对扩展开放对修改封闭
- 职责分离 = 专注底层概念，避免业务耦合
- 开闭原则 = 新增加载策略只需实现IAssetLoadStrategy
- 统一接口 = 所有策略通过统一接口访问
- 向后兼容 = 与旧版本完全兼容

---

## 整体架构设计

### 三层架构 + 策略模式

```mermaid
graph TB
    subgraph ClientLayer["客户端层<br/>业务代码"]
        LoadRequest["加载请求<br/>Load/LoadAsync"]
        TaskRequest["任务请求<br/>CreateTask"]
    end
    
    subgraph ServiceLayer["服务层<br/>AssetLoader"]
        StrategyManager["策略管理器<br/>currentStrategy"]
        TaskPool["任务对象池<br/>LoadTaskPool"]
        TaskFactory["任务工厂<br/>CreateTask"]
    end
    
    subgraph StrategyLayer["策略层<br/>IAssetLoadStrategy"]
        EditorStrategy["Editor策略<br/>EditorLoadStrategy"]
        ABStrategy["AssetBundle策略<br/>AssetBundleLoadStrategy"]
        ResourcesStrategy["Resources策略<br/>ResourcesLoadStrategy"]
        CustomStrategy["自定义策略<br/>..."]
    end
    
    ClientLayer -->|调用| ServiceLayer
    ServiceLayer -->|使用| StrategyLayer
    ServiceLayer -->|管理| TaskPool
    
    style ClientLayer fill:#e1f5ff
    style ServiceLayer fill:#fff4e1
    style StrategyLayer fill:#c8e6c9
```

### 资源加载数据流

```mermaid
graph LR
    Start[加载请求<br/>LoadAsync] -->|1. 创建任务| CreateTask[创建LoadTask<br/>从对象池获取]
    CreateTask -->|2. 配置任务| ConfigTask[配置回调<br/>OnLoaded/OnProgress]
    ConfigTask -->|3. 执行策略| Strategy[当前策略<br/>currentStrategy.LoadAsync]
    Strategy -->|4. 加载资源| LoadAsset[加载资源<br/>Editor/AB/Resources]
    LoadAsset -->|5. 回调通知| Callback[回调通知<br/>OnLoaded/OnProgress]
    Callback -->|6. 归还任务| ReturnTask[归还到对象池<br/>TaskPool.Push]
    ReturnTask -->|7. 完成| End[加载完成]
    
    style CreateTask fill:#e1f5ff
    style Strategy fill:#fff4e1
    style LoadAsset fill:#c8e6c9
    style ReturnTask fill:#c8e6c9
```

**数据流特性**：
- ✅ **策略隔离**：每个策略独立实现，互不干扰
- ✅ **统一接口**：所有策略通过统一接口访问
- ✅ **对象池优化**：LoadTask使用对象池，减少GC压力
- ✅ **异步加载**：支持进度回调和完成回调

---

## 服务层架构设计

### 核心职责

策略管理 + 任务管理 + 统一接口

### 架构图

```mermaid
graph TB
    subgraph AssetLoader["AssetLoader服务层"]
        StrategyManager["策略管理器<br/>currentStrategy"]
        TaskFactory["任务工厂<br/>CreateTask"]
        TaskPool["任务对象池<br/>LoadTaskPool"]
        LoadInterface["加载接口<br/>Load/LoadAsync"]
    end
    
    subgraph LoadTask["LoadTask任务封装"]
        AssetPath["资源路径<br/>assetPath"]
        AssetType["资源类型<br/>assetType"]
        OnLoaded["完成回调<br/>OnLoadedCallback"]
        OnProgress["进度回调<br/>OnProgressCallback"]
        OnInstantiate["实例化回调<br/>OnInstantiateCallback"]
    end
    
    AssetLoader -->|创建| TaskFactory
    TaskFactory -->|使用| TaskPool
    TaskPool -->|管理| LoadTask
    AssetLoader -->|执行| StrategyManager
    
    style AssetLoader fill:#e1f5ff
    style LoadTask fill:#fff4e1
```

### 工作流程

```mermaid
flowchart TD
    Start[加载请求<br/>LoadAsync] --> CreateTask[创建任务<br/>CreateTask]
    CreateTask --> CheckPool{对象池<br/>是否有任务?}
    CheckPool -->|是| GetFromPool[从池中获取<br/>TaskPool.Pop]
    CheckPool -->|否| CreateNew[创建新任务<br/>New LoadTask]
    
    GetFromPool --> ConfigTask[配置任务<br/>Reset + 设置参数]
    CreateNew --> ConfigTask
    
    ConfigTask --> ExecuteStrategy[执行策略<br/>currentStrategy.LoadAsync]
    ExecuteStrategy --> LoadAsset[加载资源<br/>Editor/AB/Resources]
    LoadAsset --> OnProgress[进度回调<br/>OnProgressCallback]
    OnProgress --> CheckComplete{加载<br/>是否完成?}
    CheckComplete -->|否| OnProgress
    CheckComplete -->|是| OnLoaded[完成回调<br/>OnLoadedCallback]
    OnLoaded --> ReturnToPool[归还到对象池<br/>TaskPool.Push]
    ReturnToPool --> Complete[完成]
    
    style CheckPool fill:#fff4e1,stroke:#333,stroke-width:2px
    style CheckComplete fill:#fff4e1,stroke:#333,stroke-width:2px
    style ExecuteStrategy fill:#c8e6c9
    style ReturnToPool fill:#c8e6c9
```

---

## 策略层架构设计

### 核心职责

资源加载实现 + 缓存管理 + 错误处理

### 架构图

```mermaid
graph TB
    subgraph IAssetLoadStrategy["IAssetLoadStrategy接口"]
        LoadMethod["Load方法<br/>同步加载"]
        LoadAsyncMethod["LoadAsync方法<br/>异步加载"]
        UnloadMethod["Unload方法<br/>卸载资源"]
    end
    
    subgraph EditorStrategy["EditorLoadStrategy"]
        AssetDatabase["AssetDatabase<br/>编辑器资源加载"]
    end
    
    subgraph ABStrategy["AssetBundleLoadStrategy"]
        ABManager["AssetBundle管理器<br/>AB加载/缓存"]
        CacheManager["缓存管理器<br/>资源缓存"]
    end
    
    subgraph ResourcesStrategy["ResourcesLoadStrategy"]
        ResourcesAPI["Resources API<br/>Resources.Load"]
    end
    
    IAssetLoadStrategy -->|实现| EditorStrategy
    IAssetLoadStrategy -->|实现| ABStrategy
    IAssetLoadStrategy -->|实现| ResourcesStrategy
    
    style IAssetLoadStrategy fill:#e1f5ff
    style EditorStrategy fill:#fff4e1
    style ABStrategy fill:#c8e6c9
    style ResourcesStrategy fill:#c8e6c9
```

### 策略切换流程

```mermaid
flowchart TD
    Start[切换策略请求<br/>SetStrategy] --> CheckCurrent{当前策略<br/>是否存在?}
    CheckCurrent -->|是| UnloadOld[卸载旧策略资源<br/>可选]
    CheckCurrent -->|否| SetNew[设置新策略<br/>currentStrategy = newStrategy]
    UnloadOld --> SetNew
    SetNew --> Complete[切换完成]
    
    LoadRequest[加载请求] --> GetStrategy[获取当前策略<br/>currentStrategy]
    GetStrategy --> ExecuteLoad[执行加载<br/>strategy.LoadAsync]
    ExecuteLoad --> Complete2[加载完成]
    
    style CheckCurrent fill:#fff4e1,stroke:#333,stroke-width:2px
    style GetStrategy fill:#c8e6c9
    style ExecuteLoad fill:#c8e6c9
```

---

## 架构模式分析

### 策略模式（Strategy Pattern）

**核心思想**：将不同的加载策略封装成独立的类，运行时动态切换

```mermaid
graph TB
    Context[上下文<br/>AssetLoader]
    StrategyInterface[策略接口<br/>IAssetLoadStrategy]
    
    Strategy1[策略1<br/>EditorLoadStrategy]
    Strategy2[策略2<br/>AssetBundleLoadStrategy]
    Strategy3[策略3<br/>ResourcesLoadStrategy]
    
    Context -->|使用| StrategyInterface
    StrategyInterface -->|实现| Strategy1
    StrategyInterface -->|实现| Strategy2
    StrategyInterface -->|实现| Strategy3
    
    style Context fill:#f3e5f5
    style StrategyInterface fill:#e1f5ff
    style Strategy1 fill:#c8e6c9
    style Strategy2 fill:#c8e6c9
    style Strategy3 fill:#c8e6c9
```

**优势**：
- ✅ **策略隔离**：每个策略独立实现，互不干扰
- ✅ **动态切换**：运行时动态切换加载策略
- ✅ **易于扩展**：新增策略只需实现IAssetLoadStrategy
- ✅ **统一接口**：所有策略通过统一接口访问

### 对象池模式（Object Pool Pattern）

**核心思想**：LoadTask使用对象池管理，减少GC压力

```mermaid
graph LR
    Request[请求LoadTask<br/>CreateTask] --> CheckPool{检查对象池<br/>TaskPool}
    CheckPool -->|池中有| Reuse[复用任务<br/>Reset]
    CheckPool -->|池中无| Create[创建新任务<br/>New]
    Reuse --> Return[返回任务]
    Create --> Return
    
    Release[释放任务<br/>使用完成] --> Reset[重置任务<br/>Reset]
    Reset --> ReturnToPool[归还到池<br/>TaskPool.Push]
    
    style CheckPool fill:#fff4e1,stroke:#333,stroke-width:2px
    style Reuse fill:#c8e6c9
    style ReturnToPool fill:#c8e6c9
```

---

## 数据流设计

### 资源加载数据流

```mermaid
sequenceDiagram
    participant Client as 客户端
    participant AssetLoader as AssetLoader
    participant TaskPool as LoadTaskPool
    participant Strategy as IAssetLoadStrategy
    participant Resource as 资源系统
    
    Client->>AssetLoader: LoadAsync(path, type, callback)
    AssetLoader->>TaskPool: Pop() 获取任务
    TaskPool-->>AssetLoader: 返回LoadTask
    AssetLoader->>AssetLoader: 配置任务参数
    AssetLoader->>Strategy: LoadAsync(task)
    Strategy->>Resource: 加载资源
    Resource-->>Strategy: 返回资源
    Strategy->>AssetLoader: 进度回调 OnProgress
    Strategy->>AssetLoader: 完成回调 OnLoaded
    AssetLoader->>Client: 回调通知
    AssetLoader->>TaskPool: Push(task) 归还任务
```

### 策略切换数据流

```mermaid
sequenceDiagram
    participant Client as 客户端
    participant AssetLoader as AssetLoader
    participant OldStrategy as 旧策略
    participant NewStrategy as 新策略
    
    Client->>AssetLoader: SetStrategy(newStrategy)
    AssetLoader->>OldStrategy: Unload() 可选卸载
    AssetLoader->>AssetLoader: currentStrategy = newStrategy
    AssetLoader-->>Client: 切换完成
    
    Client->>AssetLoader: LoadAsync(path, type)
    AssetLoader->>NewStrategy: LoadAsync(task)
    NewStrategy-->>AssetLoader: 加载完成
    AssetLoader-->>Client: 回调通知
```

---

## 架构验证

### 流程合理性验证

从架构可验证：
- ✅ **数据流完整**：加载请求 → 创建任务 → 执行策略 → 加载资源 → 回调通知（完整流程）
- ✅ **职责清晰**：客户端层、服务层、策略层职责明确，无重叠
- ✅ **解耦设计**：通过策略模式实现加载逻辑和业务逻辑解耦
- ✅ **性能优化**：对象池管理，减少GC压力

### 扩展性验证

从架构可验证：
- ✅ **策略模式**：新增加载策略只需实现IAssetLoadStrategy
- ✅ **统一接口**：所有策略通过统一接口访问
- ✅ **对象池优化**：LoadTask使用对象池，减少GC压力
- ✅ **向后兼容**：与旧版本完全兼容

### 易用性验证

从架构可验证：
- ✅ **统一接口**：所有策略使用统一的加载接口
- ✅ **链式配置**：LoadTask支持链式配置，易于使用
- ✅ **异步加载**：支持进度回调和完成回调
- ✅ **策略切换**：运行时动态切换加载策略

---

## 开发指导原则

### 一、开发约束（什么能做，什么不能做）

#### ✅ 应该做的

1. **加载策略必须实现IAssetLoadStrategy**
   ```
   ✅ 正确：
   public class CustomStrategy : IAssetLoadStrategy
   
   ❌ 错误：
   不实现IAssetLoadStrategy的策略类
   ```

2. **加载必须通过AssetLoader**
   ```
   ✅ 正确：
   AssetLoader.LoadAsync(path, type, callback)
   
   ❌ 错误：
   直接调用策略的LoadAsync方法
   ```

3. **LoadTask必须使用对象池**
   ```
   ✅ 正确：
   var task = AssetLoader.CreateTask(path, type)
   -- 使用后自动归还到池
   
   ❌ 错误：
   直接创建LoadTask实例
   ```

#### ❌ 不应该做的

1. **禁止直接创建LoadTask**
   - 必须通过AssetLoader.CreateTask创建
   - 不能直接new LoadTask

2. **禁止直接调用策略方法**
   - 必须通过AssetLoader统一接口
   - 不能直接调用策略的LoadAsync方法

3. **禁止在回调中执行耗时操作**
   - 回调应该快速执行
   - 耗时操作应该异步处理

### 二、开发流程（标准化开发步骤）

#### 开发新加载策略的流程

```
1. 实现IAssetLoadStrategy接口
   ↓
   public class CustomStrategy : IAssetLoadStrategy
   
2. 实现Load和LoadAsync方法
   ↓
   public object Load(string path, Type type)
   public void LoadAsync(LoadTask task)
   
3. 注册策略
   ↓
   AssetLoader.SetStrategy(new CustomStrategy())
   
4. 使用策略
   ↓
   AssetLoader.LoadAsync(path, type, callback)
```

---

## 总结

### 架构设计价值

该架构设计文档的价值在于：
- ✅ **思路解构**：完整解构资源加载系统的搭建思路
- ✅ **流程验证**：从架构层面验证流程合理性
- ✅ **模式分析**：分析策略模式、对象池模式的应用
- ✅ **开发指导**：为后续详细设计和实现提供清晰指导

### 设计原则

- ✅ **策略模式为核心**：不同的加载策略封装成独立的类，运行时动态切换
- ✅ **对象池优化 + 任务封装**：LoadTask使用对象池，通过任务封装简化异步加载
- ✅ **职责分离 + 开闭原则**：加载逻辑和业务逻辑分离，对扩展开放对修改封闭
- ✅ **统一接口**：所有策略通过统一接口访问

### 架构特点

- ✅ **策略隔离**：每个策略独立实现，互不干扰
- ✅ **动态切换**：运行时动态切换加载策略
- ✅ **对象池优化**：LoadTask使用对象池，减少GC压力
- ✅ **向后兼容**：与旧版本完全兼容

细节实现是后续开发阶段的工作，当前架构设计已足够指导整个资源加载系统的开发。
