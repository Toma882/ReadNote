# FSM状态机架构设计

## 设计目标

设计一套完整的状态机系统，支持多种状态机类型（基础状态机、下推状态机、并发状态机、层次状态机），实现状态切换、事件监听、状态栈管理、子状态机管理，提供灵活的状态管理解决方案。

---

## 核心设计理念

### 1. 状态模式为核心

**本质**：状态机系统的核心是状态模式的应用
- 状态切换 = 当前状态退出 → 新状态进入
- 状态更新 = 当前状态的Update方法持续调用
- 状态管理 = 状态注册、查找、切换的统一管理
- 状态隔离 = 每个状态独立管理自己的逻辑

### 2. 多态状态机架构

**本质**：通过继承实现多种状态机类型，满足不同场景需求
- 基础状态机 = 简单的状态切换，适用于单一系统
- 下推状态机 = 状态栈管理，适用于菜单系统、暂停恢复
- 并发状态机 = 多子状态机并行，适用于多系统协同
- 层次状态机 = 状态嵌套，适用于复杂状态管理

### 3. 对象池管理 + 事件驱动

**本质**：状态机使用对象池管理，通过事件系统实现解耦
- 对象池管理 = 状态机实例复用，减少GC压力
- 事件驱动 = 状态变化通过事件通知，实现解耦
- 生命周期管理 = 状态进入/退出/暂停/恢复的标准化流程

---

## 整体架构设计

### 四类状态机架构 + 状态模式

```mermaid
graph TB
    subgraph BaseFSM["基础状态机<br/>FSMKit"]
        StateRegistry["状态注册表<br/>stateDict"]
        CurrentState["当前状态<br/>currentState"]
        EventSystem["事件系统<br/>OnStateChanged"]
    end
    
    subgraph PDAFSM["下推状态机<br/>FSM_PDAKit"]
        StateStack["状态栈<br/>stateStack"]
        PushState["推入状态<br/>PushState"]
        PopState["弹出状态<br/>PopState"]
    end
    
    subgraph ParallelFSM["并发状态机<br/>ParallelFSMKit"]
        SubFSMList["子状态机列表<br/>subFSMList"]
        ParallelUpdate["并行更新<br/>Update所有子FSM"]
    end
    
    subgraph HierarchicalFSM["层次状态机<br/>HierarchicalFSMKit"]
        ParentState["父状态<br/>parentState"]
        SubFSMMap["子FSM映射<br/>subFSMMap[stateId]"]
        AutoActivate["自动激活<br/>父状态进入时激活子FSM"]
    end
    
    BaseFSM -->|继承| PDAFSM
    BaseFSM -->|继承| ParallelFSM
    BaseFSM -->|继承| HierarchicalFSM
    
    style BaseFSM fill:#e1f5ff
    style PDAFSM fill:#fff4e1
    style ParallelFSM fill:#c8e6c9
    style HierarchicalFSM fill:#f3e5f5
```

### 状态切换数据流

```mermaid
graph TD
    Start[状态切换请求<br/>ChangeState] -->|1. 退出当前状态| Exit[当前状态退出<br/>Exit]
    Exit -->|2. 触发事件| Event[事件通知<br/>OnStateChanged]
    Event -->|3. 查找新状态| Find[查找新状态<br/>stateDict]
    Find -->|4. 进入新状态| Enter[新状态进入<br/>Enter]
    Enter -->|5. 更新当前状态| Update[更新当前状态<br/>currentState]
    Update -->|6. 持续更新| Loop[状态更新循环<br/>Update]
    
    style Exit fill:#ffebee
    style Enter fill:#c8e6c9
    style Loop fill:#e1f5ff
```

**数据流特性**：
- ✅ **状态隔离**：每个状态独立管理自己的逻辑
- ✅ **标准化流程**：退出 → 切换 → 进入 → 更新
- ✅ **事件驱动**：状态变化通过事件通知
- ✅ **生命周期管理**：状态进入/退出/暂停/恢复的完整生命周期

---

## 基础状态机架构设计

### 核心职责

状态注册管理 + 状态切换 + 状态更新 + 事件通知

### 架构图

```mermaid
graph TB
    subgraph FSMKit["FSMKit基础状态机"]
        StateRegistry["状态注册表<br/>stateDict[stateId] = StateClass"]
        CurrentState["当前状态<br/>currentState"]
        StateInstance["状态实例<br/>stateInstance"]
        EventSystem["事件系统<br/>eventListeners"]
    end
    
    subgraph StateBase["状态基类<br/>FSMState"]
        EnterMethod["Enter方法<br/>进入状态"]
        UpdateMethod["Update方法<br/>更新状态"]
        ExitMethod["Exit方法<br/>退出状态"]
        PauseMethod["Pause方法<br/>暂停状态"]
        ResumeMethod["Resume方法<br/>恢复状态"]
    end
    
    FSMKit -->|管理| StateRegistry
    FSMKit -->|切换| CurrentState
    CurrentState -->|调用| StateInstance
    StateInstance -->|继承| StateBase
    
    style FSMKit fill:#e1f5ff
    style StateBase fill:#fff4e1
```

### 工作流程

```mermaid
flowchart TD
    Start[初始化状态机] --> Register[注册状态<br/>AddState]
    Register --> ChangeState[切换状态<br/>ChangeState]
    
    ChangeState --> CheckCurrent{当前状态<br/>是否存在?}
    CheckCurrent -->|是| ExitCurrent[退出当前状态<br/>Exit]
    CheckCurrent -->|否| FindNew[查找新状态<br/>stateDict]
    
    ExitCurrent --> TriggerEvent1[触发事件<br/>OnStateChanged]
    TriggerEvent1 --> FindNew
    
    FindNew --> CheckNew{新状态<br/>是否存在?}
    CheckNew -->|是| CreateInstance[创建状态实例<br/>StateClass.New]
    CheckNew -->|否| Error[错误：状态不存在]
    
    CreateInstance --> EnterNew[进入新状态<br/>Enter]
    EnterNew --> UpdateCurrent[更新当前状态<br/>currentState = newState]
    UpdateCurrent --> TriggerEvent2[触发事件<br/>OnStateChanged]
    TriggerEvent2 --> UpdateLoop[更新循环<br/>UpdateCurrentState]
    
    UpdateLoop -->|持续调用| UpdateMethod[状态Update方法]
    
    style CheckCurrent fill:#fff4e1,stroke:#333,stroke-width:2px
    style CheckNew fill:#fff4e1,stroke:#333,stroke-width:2px
    style EnterNew fill:#c8e6c9
    style UpdateLoop fill:#e1f5ff
```

---

## 下推状态机架构设计

### 核心职责

状态栈管理 + 状态推入/弹出 + 状态暂停/恢复

### 架构图

```mermaid
graph TB
    subgraph PDAFSM["FSM_PDAKit下推状态机"]
        StateStack["状态栈<br/>stateStack"]
        CurrentState["当前状态<br/>currentState"]
        PushState["推入状态<br/>PushState"]
        PopState["弹出状态<br/>PopState"]
    end
    
    subgraph StackOperation["栈操作"]
        Push[推入操作<br/>暂停当前状态<br/>推入新状态]
        Pop[弹出操作<br/>退出当前状态<br/>恢复旧状态]
    end
    
    PDAFSM -->|使用| StateStack
    StateStack -->|执行| StackOperation
    
    style PDAFSM fill:#fff4e1
    style StackOperation fill:#c8e6c9
```

### 工作流程

```mermaid
flowchart TD
    Start[推入状态请求<br/>PushState] --> PauseCurrent[暂停当前状态<br/>Pause]
    PauseCurrent --> PushToStack[推入栈<br/>stateStack.push]
    PushToStack --> ChangeToNew[切换到新状态<br/>ChangeState]
    
    PopRequest[弹出状态请求<br/>PopState] --> ExitCurrent[退出当前状态<br/>Exit]
    ExitCurrent --> PopFromStack[从栈弹出<br/>stateStack.pop]
    PopFromStack --> ResumeOld[恢复旧状态<br/>Resume]
    ResumeOld --> SetCurrent[设置当前状态<br/>currentState = oldState]
    
    style PauseCurrent fill:#fff4e1,stroke:#333,stroke-width:2px
    style PopFromStack fill:#fff4e1,stroke:#333,stroke-width:2px
    style ChangeToNew fill:#c8e6c9
    style ResumeOld fill:#c8e6c9
```

---

## 并发状态机架构设计

### 核心职责

多子状态机管理 + 并行更新 + 生命周期同步

### 架构图

```mermaid
graph TB
    subgraph ParallelFSM["ParallelFSMKit并发状态机"]
        SubFSMList["子状态机列表<br/>subFSMList"]
        AddSubFSM["添加子FSM<br/>AddSubFSM"]
        UpdateAll["更新所有子FSM<br/>Update"]
    end
    
    subgraph SubFSMs["子状态机组"]
        SubFSM1["子FSM1<br/>CharacterFSM"]
        SubFSM2["子FSM2<br/>AnimationFSM"]
        SubFSM3["子FSM3<br/>AIFSM"]
    end
    
    ParallelFSM -->|管理| SubFSMList
    SubFSMList -->|包含| SubFSMs
    ParallelFSM -->|更新| UpdateAll
    UpdateAll -->|并行调用| SubFSMs
    
    style ParallelFSM fill:#c8e6c9
    style SubFSMs fill:#e1f5ff
```

### 工作流程

```mermaid
flowchart TD
    Start[创建并发状态机] --> AddSubFSM1[添加子FSM1<br/>AddSubFSM]
    AddSubFSM1 --> AddSubFSM2[添加子FSM2<br/>AddSubFSM]
    AddSubFSM2 --> AddSubFSM3[添加子FSM3<br/>AddSubFSM]
    
    UpdateRequest[更新请求<br/>Update] --> Loop[遍历子FSM列表]
    Loop --> UpdateSubFSM1[更新子FSM1<br/>Update]
    Loop --> UpdateSubFSM2[更新子FSM2<br/>Update]
    Loop --> UpdateSubFSM3[更新子FSM3<br/>Update]
    
    UpdateSubFSM1 --> Complete[更新完成]
    UpdateSubFSM2 --> Complete
    UpdateSubFSM3 --> Complete
    
    style Loop fill:#fff4e1,stroke:#333,stroke-width:2px
    style UpdateSubFSM1 fill:#c8e6c9
    style UpdateSubFSM2 fill:#c8e6c9
    style UpdateSubFSM3 fill:#c8e6c9
```

---

## 层次状态机架构设计

### 核心职责

状态嵌套管理 + 子FSM自动激活 + 父子状态同步

### 架构图

```mermaid
graph TB
    subgraph HierarchicalFSM["HierarchicalFSMKit层次状态机"]
        ParentState["父状态<br/>parentState"]
        SubFSMMap["子FSM映射<br/>subFSMMap[stateId]"]
        AutoActivate["自动激活机制<br/>父状态进入时激活子FSM"]
    end
    
    subgraph StateSubFSM["状态-子FSM关联"]
        MoveState["移动状态<br/>MoveState"]
        MoveSubFSM["移动子FSM<br/>AnimationFSM"]
        AttackState["攻击状态<br/>AttackState"]
        AttackSubFSM["攻击子FSM<br/>CombatFSM"]
    end
    
    HierarchicalFSM -->|管理| SubFSMMap
    SubFSMMap -->|关联| StateSubFSM
    HierarchicalFSM -->|自动激活| AutoActivate
    AutoActivate -->|触发| StateSubFSM
    
    style HierarchicalFSM fill:#f3e5f5
    style StateSubFSM fill:#e1f5ff
```

### 工作流程

```mermaid
flowchart TD
    Start[切换父状态<br/>ChangeState] --> ExitOld[退出旧父状态<br/>Exit]
    ExitOld --> DeactivateOld[停用旧子FSM<br/>Reset]
    DeactivateOld --> EnterNew[进入新父状态<br/>Enter]
    EnterNew --> CheckSubFSM{是否有子FSM?<br/>subFSMMap}
    CheckSubFSM -->|是| ActivateSubFSM[激活子FSM<br/>Reset + ChangeState]
    CheckSubFSM -->|否| Continue[继续执行]
    ActivateSubFSM --> Continue
    
    UpdateRequest[更新请求<br/>Update] --> UpdateParent[更新父状态<br/>Update]
    UpdateParent --> CheckSubFSM2{是否有子FSM?}
    CheckSubFSM2 -->|是| UpdateSubFSM[更新子FSM<br/>Update]
    CheckSubFSM2 -->|否| Complete[更新完成]
    UpdateSubFSM --> Complete
    
    style CheckSubFSM fill:#fff4e1,stroke:#333,stroke-width:2px
    style CheckSubFSM2 fill:#fff4e1,stroke:#333,stroke-width:2px
    style ActivateSubFSM fill:#c8e6c9
    style UpdateSubFSM fill:#c8e6c9
```

---

## 架构模式分析

### 状态模式（State Pattern）

**核心思想**：状态封装行为，状态机管理状态切换

```mermaid
graph TB
    Context[上下文<br/>FSMKit]
    StateInterface[状态接口<br/>FSMState]
    
    ConcreteState1[具体状态1<br/>IdleState]
    ConcreteState2[具体状态2<br/>MoveState]
    ConcreteState3[具体状态3<br/>AttackState]
    
    Context -->|管理| StateInterface
    StateInterface -->|实现| ConcreteState1
    StateInterface -->|实现| ConcreteState2
    StateInterface -->|实现| ConcreteState3
    
    style Context fill:#f3e5f5
    style StateInterface fill:#e1f5ff
    style ConcreteState1 fill:#c8e6c9
    style ConcreteState2 fill:#c8e6c9
    style ConcreteState3 fill:#c8e6c9
```

**优势**：
- ✅ **状态隔离**：每个状态独立管理自己的逻辑
- ✅ **易于扩展**：新增状态只需添加新的状态类
- ✅ **消除条件判断**：状态切换逻辑清晰，避免大量if-else

### 对象池模式（Object Pool Pattern）

**核心思想**：状态机实例复用，减少GC压力

```mermaid
graph LR
    Request[请求状态机<br/>FSMKit.Pop] --> CheckPool{检查对象池<br/>pool}
    CheckPool -->|池中有| Reuse[复用实例<br/>Reset]
    CheckPool -->|池中无| Create[创建新实例<br/>New]
    Reuse --> Return[返回状态机]
    Create --> Return
    
    Release[释放状态机<br/>Push] --> Reset[重置状态机<br/>Reset]
    Reset --> ReturnToPool[归还到池<br/>pool.push]
    
    style CheckPool fill:#fff4e1,stroke:#333,stroke-width:2px
    style Reuse fill:#c8e6c9
    style ReturnToPool fill:#c8e6c9
```

---

## 数据流设计

### 状态切换数据流

```mermaid
sequenceDiagram
    participant Client as 客户端
    participant FSM as FSMKit
    participant CurrentState as 当前状态
    participant NewState as 新状态
    participant EventSystem as 事件系统
    
    Client->>FSM: ChangeState(newStateId)
    FSM->>CurrentState: Exit()
    CurrentState-->>FSM: 退出完成
    FSM->>EventSystem: OnStateChanged(oldState, newStateId)
    FSM->>FSM: 查找新状态 stateDict[newStateId]
    FSM->>NewState: New() 创建实例
    FSM->>NewState: Enter()
    NewState-->>FSM: 进入完成
    FSM->>FSM: currentState = newState
    FSM->>EventSystem: OnStateChanged(oldState, newState)
    FSM-->>Client: 切换完成
```

### 并发状态机更新数据流

```mermaid
sequenceDiagram
    participant Client as 客户端
    participant ParallelFSM as ParallelFSMKit
    participant SubFSM1 as 子FSM1
    participant SubFSM2 as 子FSM2
    participant SubFSM3 as 子FSM3
    
    Client->>ParallelFSM: Update(dt)
    ParallelFSM->>SubFSM1: Update(dt)
    ParallelFSM->>SubFSM2: Update(dt)
    ParallelFSM->>SubFSM3: Update(dt)
    SubFSM1-->>ParallelFSM: 更新完成
    SubFSM2-->>ParallelFSM: 更新完成
    SubFSM3-->>ParallelFSM: 更新完成
    ParallelFSM-->>Client: 更新完成
```

---

## 架构验证

### 流程合理性验证

从架构可验证：
- ✅ **数据流完整**：状态切换 → 退出 → 进入 → 更新（完整流程）
- ✅ **职责清晰**：基础状态机、下推状态机、并发状态机、层次状态机职责明确
- ✅ **解耦设计**：通过状态模式和事件系统实现解耦
- ✅ **生命周期管理**：状态进入/退出/暂停/恢复的完整生命周期

### 扩展性验证

从架构可验证：
- ✅ **状态模式**：新增状态只需添加新的状态类
- ✅ **多态设计**：通过继承实现多种状态机类型
- ✅ **事件系统**：支持状态变化的事件监听
- ✅ **对象池管理**：状态机实例复用，减少GC压力

### 易用性验证

从架构可验证：
- ✅ **统一接口**：所有状态机类型使用统一的接口
- ✅ **标准化流程**：状态切换流程标准化
- ✅ **事件驱动**：状态变化通过事件通知
- ✅ **调试支持**：提供调试日志和调试信息

---

## 开发指导原则

### 一、开发约束（什么能做，什么不能做）

#### ✅ 应该做的

1. **状态必须继承FSMState**
   ```
   ✅ 正确：
   local MyState = BaseClass(FSMState)
   
   ❌ 错误：
   不继承FSMState的状态类
   ```

2. **状态切换必须通过ChangeState**
   ```
   ✅ 正确：
   fsm:ChangeState(newStateId)
   
   ❌ 错误：
   直接修改currentState
   ```

3. **状态机必须使用对象池**
   ```
   ✅ 正确：
   local fsm = FSMKit.Pop(type)
   -- 使用后
   fsm:Push()
   
   ❌ 错误：
   直接创建状态机实例
   ```

#### ❌ 不应该做的

1. **禁止直接修改状态机内部状态**
   - 必须通过ChangeState切换状态
   - 不能直接修改currentState

2. **禁止在状态Update中执行耗时操作**
   - Update方法应该快速执行
   - 耗时操作应该异步处理

3. **禁止状态之间直接依赖**
   - 状态之间应该通过状态机通信
   - 不能直接调用其他状态的方法

### 二、开发流程（标准化开发步骤）

#### 开发新状态的流程

```
1. 定义状态枚举
   ↓
   ECharacterState = { Idle = 1, Move = 2 }
   
2. 创建状态类
   ↓
   local IdleState = BaseClass(FSMState)
   function IdleState:Enter() end
   function IdleState:Update() end
   function IdleState:Exit() end
   
3. 注册状态
   ↓
   fsm:AddState(ECharacterState.Idle, IdleState)
   
4. 切换状态
   ↓
   fsm:ChangeState(ECharacterState.Idle)
```

---

## 总结

### 架构设计价值

该架构设计文档的价值在于：
- ✅ **思路解构**：完整解构状态机系统的搭建思路
- ✅ **流程验证**：从架构层面验证流程合理性
- ✅ **模式分析**：分析状态模式、对象池模式的应用
- ✅ **开发指导**：为后续详细设计和实现提供清晰指导

### 设计原则

- ✅ **状态模式为核心**：状态封装行为，状态机管理状态切换
- ✅ **多态状态机架构**：通过继承实现多种状态机类型
- ✅ **对象池管理 + 事件驱动**：状态机实例复用，状态变化通过事件通知
- ✅ **生命周期管理**：状态进入/退出/暂停/恢复的标准化流程

### 架构特点

- ✅ **状态隔离**：每个状态独立管理自己的逻辑
- ✅ **灵活扩展**：支持基础、下推、并发、层次四种状态机类型
- ✅ **事件驱动**：状态变化通过事件通知，实现解耦
- ✅ **性能优化**：对象池管理，减少GC压力

细节实现是后续开发阶段的工作，当前架构设计已足够指导整个状态机系统的开发。
