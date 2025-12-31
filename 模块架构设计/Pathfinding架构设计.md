# Pathfinding寻路系统架构设计

## 设计目标

设计一套完整的高性能寻路系统，支持多种寻路算法（A*、BFS、Dijkstra、JPS、Flow Field、NavMesh），实现算法无关性、统一数据结构、委托模式架构、智能算法推荐，提供灵活、高性能的寻路解决方案。

---

## 核心设计理念

### 1. 委托模式为核心

**本质**：寻路系统的核心是委托模式的应用，彻底消除算法特定的硬编码逻辑
- 算法无关性 = 通过委托模式处理算法差异，避免硬编码
- 统一接口 = 所有算法通过统一的委托接口访问
- 动态选择 = 运行时动态选择最适合的算法
- 易于扩展 = 新增算法只需实现委托接口

### 2. 统一数据结构 + 高性能优化

**本质**：使用PathfindingNode统一所有算法的节点表示，通过值类型和位运算优化性能
- 统一节点 = PathfindingNode适配所有算法需求
- 值类型设计 = struct避免GC压力，零拆装箱
- 位运算优化 = 使用位标志进行高效状态管理
- 内存紧凑 = 结构体字段排列优化，提高缓存命中率

### 3. 门面模式 + 工厂模式

**本质**：通过门面模式提供统一API，通过工厂模式管理算法实例
- 门面模式 = PathfindingService提供统一的API接口
- 工厂模式 = PathfindingFactory管理算法实例创建和注册
- 智能推荐 = 基于网格特征的算法自动推荐
- 事件驱动 = 支持寻路生命周期事件

---

## 整体架构设计

### 四层架构 + 委托模式

```mermaid
graph TB
    subgraph UserLayer["用户接口层<br/>PathfindingService"]
        FacadeService["门面服务<br/>PathfindingService<br/>统一API接口"]
        EventSystem["事件系统<br/>PathfindingStarted/Completed/Failed"]
        CacheManager["缓存管理器<br/>结果缓存"]
    end
    
    subgraph BusinessLayer["业务逻辑层<br/>PathfindingFactory"]
        AlgorithmFactory["算法工厂<br/>PathfindingFactory<br/>算法实例管理"]
        AlgorithmRegistry["算法注册表<br/>algorithms[AlgorithmType]"]
        RecommendationEngine["推荐引擎<br/>RecommendAlgorithm"]
    end
    
    subgraph DelegateLayer["委托处理层<br/>PathfindingNodeUtils"]
        DelegateManager["委托管理器<br/>PathAlgorithmDelegate"]
        DelegateDict["委托字典<br/>delegateDict[AlgorithmType]"]
        AlgorithmLogic["算法逻辑处理<br/>通过委托处理算法差异"]
    end
    
    subgraph DataLayer["数据结构层<br/>PathfindingNode"]
        NodeStructure["节点结构<br/>PathfindingNode<br/>统一节点表示"]
        StateFlags["状态标志<br/>NodeStateFlags<br/>位标志管理"]
        NodeArray["节点数组<br/>nodes[]<br/>高性能存储"]
    end
    
    UserLayer -->|调用| BusinessLayer
    BusinessLayer -->|使用| DelegateLayer
    DelegateLayer -->|操作| DataLayer
    
    style UserLayer fill:#e1f5ff
    style BusinessLayer fill:#fff4e1
    style DelegateLayer fill:#c8e6c9
    style DataLayer fill:#f3e5f5
```

### 委托模式数据流

```mermaid
graph LR
    Start[寻路请求<br/>FindPath] -->|1. 获取算法| Factory[算法工厂<br/>GetAlgorithm]
    Factory -->|2. 获取委托| Delegate[委托管理器<br/>GetDelegate]
    Delegate -->|3. 执行委托| Execute[执行算法逻辑<br/>通过委托处理]
    Execute -->|4. 计算代价| CalculateCost[计算移动代价<br/>CalculateMoveCost]
    Execute -->|5. 计算启发式| CalculateHeuristic[计算启发式<br/>CalculateHeuristic]
    Execute -->|6. 计算总代价| CalculateTotal[计算总代价<br/>CalculateTotalCost]
    CalculateCost -->|7. 构建路径| BuildPath[构建路径<br/>ReconstructPath]
    CalculateHeuristic --> BuildPath
    CalculateTotal --> BuildPath
    BuildPath -->|8. 返回结果| End[寻路结果<br/>PathfindingResult]
    
    style Delegate fill:#fff4e1
    style Execute fill:#c8e6c9
    style BuildPath fill:#c8e6c9
```

**数据流特性**：
- ✅ **算法无关性**：通过委托模式处理算法差异，避免硬编码
- ✅ **统一接口**：所有算法通过统一的委托接口访问
- ✅ **高性能**：值类型设计、位运算优化、零拆装箱
- ✅ **智能推荐**：基于网格特征自动推荐最适合的算法

---

## 用户接口层架构设计

### 核心职责

统一API接口 + 事件系统 + 缓存管理

### 架构图

```mermaid
graph TB
    subgraph PathfindingService["PathfindingService门面服务"]
        FindPathSync["同步寻路<br/>FindPath"]
        FindPathAsync["异步寻路<br/>FindPathAsync"]
        FindPaths["批量寻路<br/>FindPaths"]
        ConvenienceMethods["便捷方法<br/>FindPathForAI/ForPlayer/Optimal"]
        EventSystem["事件系统<br/>PathfindingStarted/Completed/Failed"]
        CacheManager["缓存管理器<br/>EnableCaching/CacheHitCount"]
    end
    
    subgraph RequestResponse["请求响应"]
        PathfindingRequest["寻路请求<br/>PathfindingRequest"]
        PathfindingResult["寻路结果<br/>PathfindingResult"]
        PathfindingMetrics["性能指标<br/>PathfindingMetrics"]
    end
    
    PathfindingService -->|处理| RequestResponse
    
    style PathfindingService fill:#e1f5ff
    style RequestResponse fill:#fff4e1
```

---

## 业务逻辑层架构设计

### 核心职责

算法管理 + 算法推荐 + 算法注册

### 架构图

```mermaid
graph TB
    subgraph PathfindingFactory["PathfindingFactory算法工厂"]
        AlgorithmRegistry["算法注册表<br/>algorithms[AlgorithmType]"]
        GetAlgorithm["获取算法<br/>GetAlgorithm"]
        RecommendAlgorithm["推荐算法<br/>RecommendAlgorithm"]
        IsSupported["检查支持<br/>IsAlgorithmSupported"]
    end
    
    subgraph Algorithms["算法实现"]
        AStarAlgorithm["A*算法<br/>AStarAlgorithm"]
        BFSAlgorithm["BFS算法<br/>BFSAlgorithm"]
        DijkstraAlgorithm["Dijkstra算法<br/>DijkstraAlgorithm"]
        JPSAlgorithm["JPS算法<br/>JPSAlgorithm"]
        FlowFieldAlgorithm["FlowField算法<br/>FlowFieldAlgorithm"]
        NavMeshAlgorithm["NavMesh算法<br/>NavMeshAlgorithm"]
    end
    
    PathfindingFactory -->|管理| AlgorithmRegistry
    AlgorithmRegistry -->|包含| Algorithms
    
    style PathfindingFactory fill:#e1f5ff
    style Algorithms fill:#fff4e1
```

### 算法推荐流程

```mermaid
flowchart TD
    Start[算法推荐请求<br/>RecommendAlgorithm] --> Analyze[分析网格特征<br/>gridSize/needsOptimalPath/hasWeights]
    Analyze --> CheckSize{网格大小<br/>totalNodes}
    CheckSize -->|< 100| SelectBFS[选择BFS<br/>简单可靠]
    CheckSize -->|>= 100| CheckWeights{是否有权重<br/>hasWeights}
    CheckWeights -->|是| CheckOptimal{需要最优路径<br/>needsOptimalPath}
    CheckWeights -->|否| SelectAStar[选择A*<br/>通用寻路]
    CheckOptimal -->|是| SelectDijkstra[选择Dijkstra<br/>最优保证]
    CheckOptimal -->|否| SelectAStar
    SelectBFS --> Validate{算法是否<br/>支持?}
    SelectAStar --> Validate
    SelectDijkstra --> Validate
    Validate -->|是| Return[返回推荐算法]
    Validate -->|否| Default[返回默认算法<br/>A*]
    
    style CheckSize fill:#fff4e1,stroke:#333,stroke-width:2px
    style CheckWeights fill:#fff4e1,stroke:#333,stroke-width:2px
    style CheckOptimal fill:#fff4e1,stroke:#333,stroke-width:2px
    style Validate fill:#fff4e1,stroke:#333,stroke-width:2px
```

---

## 委托处理层架构设计

### 核心职责

委托管理 + 算法逻辑处理 + 算法差异处理

### 架构图

```mermaid
graph TB
    subgraph PathfindingNodeUtils["PathfindingNodeUtils委托处理"]
        DelegateManager["委托管理器<br/>GetDelegate"]
        DelegateDict["委托字典<br/>delegateDict[AlgorithmType]"]
        AlgorithmDelegate["算法委托<br/>PathAlgorithmDelegate"]
    end
    
    subgraph DelegateMethods["委托方法"]
        Renew["Renew<br/>重置数据结构"]
        PopNodeIndex["PopNodeIndex<br/>从开放列表弹出节点"]
        CalculateMoveCost["CalculateMoveCost<br/>计算移动代价"]
        CalculateHeuristic["CalculateHeuristic<br/>计算启发式"]
        CalculateTotalCost["CalculateTotalCost<br/>计算总代价"]
        AddToOpenList["AddToOpenList<br/>添加到开放列表"]
        AddIterations["AddIterations<br/>增加迭代次数"]
    end
    
    PathfindingNodeUtils -->|管理| DelegateManager
    DelegateManager -->|使用| DelegateMethods
    
    style PathfindingNodeUtils fill:#e1f5ff
    style DelegateMethods fill:#fff4e1
```

### 委托模式工作流程

```mermaid
flowchart TD
    Start[寻路请求<br/>FindPath] --> GetAlgorithm[获取算法<br/>GetAlgorithm]
    GetAlgorithm --> GetDelegate[获取委托<br/>GetDelegate(algorithmType)]
    GetDelegate --> CheckDict{委托字典<br/>是否已有?}
    CheckDict -->|是| ReturnDelegate[返回已有委托]
    CheckDict -->|否| CreateDelegate[创建委托<br/>根据算法类型]
    CreateDelegate --> RegisterDelegate[注册委托<br/>delegateDict[type] = delegate]
    RegisterDelegate --> ReturnDelegate
    ReturnDelegate --> ExecuteDelegate[执行委托<br/>通过委托处理算法逻辑]
    ExecuteDelegate --> Complete[寻路完成]
    
    style CheckDict fill:#fff4e1,stroke:#333,stroke-width:2px
    style CreateDelegate fill:#c8e6c9
    style ExecuteDelegate fill:#c8e6c9
```

---

## 数据结构层架构设计

### 核心职责

节点结构定义 + 状态标志管理 + 路径重建

### 架构图

```mermaid
graph TB
    subgraph PathfindingNode["PathfindingNode节点结构"]
        Distance["距离/代价值<br/>Distance<br/>G值 for A*"]
        Heuristic["启发式值<br/>Heuristic<br/>H值 for A*"]
        ParentIndex["父节点索引<br/>ParentIndex<br/>-1 for no parent"]
        Flags["状态标志<br/>NodeStateFlags<br/>位标志管理"]
        TotalCost["总代价<br/>TotalCost<br/>Distance + Heuristic"]
    end
    
    subgraph NodeStateFlags["NodeStateFlags状态标志"]
        Visited["已访问<br/>Visited"]
        IsObstacle["是障碍物<br/>IsObstacle"]
        IsStart["是起点<br/>IsStart"]
        IsEnd["是终点<br/>IsEnd"]
        InOpenList["在开放列表<br/>InOpenList"]
        InClosedList["在关闭列表<br/>InClosedList"]
    end
    
    PathfindingNode -->|使用| NodeStateFlags
    
    style PathfindingNode fill:#e1f5ff
    style NodeStateFlags fill:#fff4e1
```

---

## 架构模式分析

### 委托模式（Delegate Pattern）

**核心思想**：通过委托处理算法差异，彻底消除硬编码

```mermaid
graph TB
    Context[上下文<br/>PathfindingNodeUtils]
    DelegateInterface[委托接口<br/>PathAlgorithmDelegate]
    
    AStarDelegate["A*委托<br/>AStarDelegate"]
    BFSDelegate["BFS委托<br/>BFSDelegate"]
    DijkstraDelegate["Dijkstra委托<br/>DijkstraDelegate"]
    
    Context -->|使用| DelegateInterface
    DelegateInterface -->|实现| AStarDelegate
    DelegateInterface -->|实现| BFSDelegate
    DelegateInterface -->|实现| DijkstraDelegate
    
    style Context fill:#f3e5f5
    style DelegateInterface fill:#e1f5ff
    style AStarDelegate fill:#c8e6c9
    style BFSDelegate fill:#c8e6c9
    style DijkstraDelegate fill:#c8e6c9
```

**优势**：
- ✅ **算法无关性**：彻底消除算法特定的硬编码逻辑
- ✅ **统一接口**：所有算法通过统一的委托接口访问
- ✅ **易于扩展**：新增算法只需实现委托接口
- ✅ **性能优化**：委托调用性能高，无额外开销

### 门面模式（Facade Pattern）

**核心思想**：PathfindingService提供统一的API接口，隐藏内部复杂性

```mermaid
graph TB
    Client[客户端<br/>业务代码]
    Facade[门面<br/>PathfindingService]
    
    Subsystem1[子系统1<br/>PathfindingFactory]
    Subsystem2[子系统2<br/>PathfindingNodeUtils]
    Subsystem3[子系统3<br/>算法实现]
    
    Client -->|简单调用| Facade
    Facade -->|协调调用| Subsystem1
    Facade -->|协调调用| Subsystem2
    Facade -->|协调调用| Subsystem3
    
    style Client fill:#fff9c4
    style Facade fill:#f3e5f5
    style Subsystem1 fill:#e1f5ff
    style Subsystem2 fill:#e1f5ff
    style Subsystem3 fill:#e1f5ff
```

### 工厂模式（Factory Pattern）

**核心思想**：PathfindingFactory管理算法实例创建和注册

```mermaid
graph TB
    Client[客户端<br/>业务代码]
    Factory[工厂<br/>PathfindingFactory]
    
    Product1[产品1<br/>AStarAlgorithm]
    Product2[产品2<br/>BFSAlgorithm]
    Product3[产品3<br/>DijkstraAlgorithm]
    
    Client -->|请求算法| Factory
    Factory -->|创建/返回| Product1
    Factory -->|创建/返回| Product2
    Factory -->|创建/返回| Product3
    
    style Client fill:#fff9c4
    style Factory fill:#f3e5f5
    style Product1 fill:#c8e6c9
    style Product2 fill:#c8e6c9
    style Product3 fill:#c8e6c9
```

---

## 数据流设计

### 寻路执行数据流

```mermaid
sequenceDiagram
    participant Client as 客户端
    participant Service as PathfindingService
    participant Factory as PathfindingFactory
    participant Delegate as PathAlgorithmDelegate
    participant NodeUtils as PathfindingNodeUtils
    participant Adapter as IPathfindingAdapter
    
    Client->>Service: FindPath(request)
    Service->>Factory: GetAlgorithm(algorithmType)
    Factory-->>Service: 返回算法实例
    Service->>Delegate: GetDelegate(algorithmType)
    Delegate-->>Service: 返回委托
    Service->>NodeUtils: 执行寻路逻辑
    NodeUtils->>Delegate: CalculateMoveCost
    NodeUtils->>Adapter: IsWalkable/GetCost
    Adapter-->>NodeUtils: 返回网格数据
    NodeUtils->>Delegate: CalculateHeuristic
    NodeUtils->>Delegate: CalculateTotalCost
    NodeUtils->>NodeUtils: ReconstructPath
    NodeUtils-->>Service: 返回路径结果
    Service-->>Client: 返回PathfindingResult
```

### 委托创建数据流

```mermaid
sequenceDiagram
    participant NodeUtils as PathfindingNodeUtils
    participant Delegate as PathAlgorithmDelegate
    participant Factory as PathfindingFactory
    
    NodeUtils->>Delegate: GetDelegate(algorithmType)
    Delegate->>Delegate: 检查delegateDict
    alt 委托已存在
        Delegate-->>NodeUtils: 返回已有委托
    else 委托不存在
        Delegate->>Factory: 获取算法实例
        Factory-->>Delegate: 返回算法
        Delegate->>Delegate: 创建委托实例
        Delegate->>Delegate: 注册到delegateDict
        Delegate-->>NodeUtils: 返回新委托
    end
```

---

## 架构验证

### 流程合理性验证

从架构可验证：
- ✅ **数据流完整**：寻路请求 → 获取算法 → 获取委托 → 执行逻辑 → 返回结果（完整流程）
- ✅ **职责清晰**：用户接口层、业务逻辑层、委托处理层、数据结构层职责明确
- ✅ **解耦设计**：通过委托模式、门面模式、工厂模式实现解耦
- ✅ **算法无关性**：通过委托模式彻底消除硬编码

### 扩展性验证

从架构可验证：
- ✅ **委托模式**：新增算法只需实现委托接口
- ✅ **工厂模式**：新增算法只需注册到工厂
- ✅ **统一数据结构**：PathfindingNode适配所有算法
- ✅ **智能推荐**：基于网格特征自动推荐算法

### 性能验证

从架构可验证：
- ✅ **值类型设计**：PathfindingNode为struct，避免GC压力
- ✅ **位运算优化**：使用位标志进行高效状态管理
- ✅ **零拆装箱**：避免object[]参数，使用泛型和值类型
- ✅ **内存紧凑**：结构体字段排列优化，提高缓存命中率

---

## 开发指导原则

### 一、开发约束（什么能做，什么不能做）

#### ✅ 应该做的

1. **寻路必须通过PathfindingService**
   ```
   ✅ 正确：
   PathfindingService.Instance.FindPath(request)
   
   ❌ 错误：
   直接调用算法实例
   ```

2. **适配器必须实现IPathfindingAdapter**
   ```
   ✅ 正确：
   public class MyAdapter : IPathfindingAdapter
   
   ❌ 错误：
   不实现IPathfindingAdapter的适配器
   ```

3. **算法必须通过工厂注册**
   ```
   ✅ 正确：
   algorithms[AlgorithmType.CUSTOM] = new CustomAlgorithm()
   
   ❌ 错误：
   直接创建算法实例
   ```

#### ❌ 不应该做的

1. **禁止直接调用算法实例**
   - 必须通过PathfindingService统一接口
   - 不能直接调用算法的ExecuteAlgorithm方法

2. **禁止绕过委托模式**
   - 算法逻辑必须通过委托处理
   - 不能硬编码算法特定的逻辑

3. **禁止直接操作节点数组**
   - 必须通过PathfindingNodeUtils
   - 不能直接操作nodes数组

### 二、开发流程（标准化开发步骤）

#### 开发新算法的流程

```
1. 实现算法基类
   ↓
   public class CustomAlgorithm : PathfindingAlgorithmBase
   
2. 实现委托接口
   ↓
   创建PathAlgorithmDelegate实例
   实现所有委托方法
   
3. 注册算法
   ↓
   algorithms[AlgorithmType.CUSTOM] = new CustomAlgorithm()
   
4. 注册委托
   ↓
   delegateDict[AlgorithmType.CUSTOM] = customDelegate
   
5. 使用算法
   ↓
   PathfindingService.Instance.FindPath(request)
```

---

## 总结

### 架构设计价值

该架构设计文档的价值在于：
- ✅ **思路解构**：完整解构寻路系统的搭建思路
- ✅ **流程验证**：从架构层面验证流程合理性
- ✅ **模式分析**：分析委托模式、门面模式、工厂模式的应用
- ✅ **开发指导**：为后续详细设计和实现提供清晰指导

### 设计原则

- ✅ **委托模式为核心**：通过委托模式彻底消除算法特定的硬编码逻辑
- ✅ **统一数据结构 + 高性能优化**：使用PathfindingNode统一所有算法的节点表示
- ✅ **门面模式 + 工厂模式**：通过门面模式提供统一API，通过工厂模式管理算法实例
- ✅ **算法无关性**：所有算法通过统一的委托接口访问

### 架构特点

- ✅ **算法无关性**：通过委托模式彻底消除硬编码
- ✅ **高性能**：值类型设计、位运算优化、零拆装箱
- ✅ **智能推荐**：基于网格特征自动推荐最适合的算法
- ✅ **易于扩展**：新增算法只需实现委托接口和注册到工厂

**⚠️ 已知限制**：当前实现不支持并发寻路（多个请求共享静态数据结构），需要为每个请求创建独立的数据结构上下文。

细节实现是后续开发阶段的工作，当前架构设计已足够指导整个寻路系统的开发。
