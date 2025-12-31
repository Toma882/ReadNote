# OOP框架架构设计

## 设计目标

设计一套完整的Lua面向对象编程框架，支持接口机制、事件系统、装饰器模式、对象工厂，提供结构化、可维护的Lua代码编写方案，特别适合在Unity游戏中使用。

---

## 核心设计理念

### 1. 接口机制为核心

**本质**：OOP框架的核心是接口机制，解决Lua单继承的限制
- 接口定义 = 定义一组方法契约
- 多重继承 = 通过接口混入实现多重继承效果
- 接口混入 = 在类构造函数中混入接口
- 契约约束 = 通过接口明确定义类必须实现的方法

### 2. 设计模式组合

**本质**：通过组合多种设计模式提供完整的OOP支持
- 接口模式 = 解决Lua单继承限制
- 观察者模式 = 事件系统实现对象间松耦合通信
- 装饰器模式 = 动态扩展对象功能
- 工厂模式 = 简化对象创建和管理

### 3. 混入机制 + 链式调用

**本质**：通过混入机制实现接口继承，通过链式调用提供流畅API
- 混入机制 = ByInheritance方法实现接口混入
- 链式调用 = 流畅的API设计，易于使用
- 接口隔离 = 每个接口只定义相关的方法
- 组合复用 = 通过接口组合实现功能复用

---

## 整体架构设计

### 四模块架构 + 接口机制

```mermaid
graph TB
    subgraph InterfaceModule["接口模块<br/>Interface"]
        InterfaceBase["接口基类<br/>Interface"]
        MixinMechanism["混入机制<br/>ByInheritance"]
        MultipleInheritance["多重继承<br/>支持多个接口"]
    end
    
    subgraph EventModule["事件模块<br/>IEvent"]
        EventInterface["事件接口<br/>IEvent"]
        EventRegistry["事件注册表<br/>eventListeners"]
        EventTrigger["事件触发<br/>TriggerEvent"]
    end
    
    subgraph DecoratorModule["装饰器模块<br/>IDecorator"]
        DecoratorInterface["装饰器接口<br/>IDecorator"]
        DecoratorBehavior["装饰器行为<br/>IDecoratorBehavior"]
        DecoratorChain["装饰器链<br/>装饰器执行链"]
    end
    
    subgraph FactoryModule["工厂模块<br/>ObjFactory"]
        FactoryInterface["工厂接口<br/>ObjFactory"]
        TypeRegistry["类型注册表<br/>objClassDict"]
        ObjectPool["对象池<br/>对象复用"]
    end
    
    style InterfaceModule fill:#e1f5ff
    style EventModule fill:#fff4e1
    style DecoratorModule fill:#c8e6c9
    style FactoryModule fill:#f3e5f5
```

### 接口混入数据流

```mermaid
graph LR
    Start[创建类实例] -->|1. 构造函数| Ctor[ctor方法<br/>初始化]
    Ctor -->|2. 创建接口实例| CreateInterface[接口实例<br/>MyInterface.New()]
    CreateInterface -->|3. 混入接口| Mixin[混入接口<br/>ByInheritance(self)]
    Mixin -->|4. 绑定方法| Bind[绑定方法<br/>self.method = interface.method]
    Bind -->|5. 完成| End[接口混入完成]
    
    style CreateInterface fill:#e1f5ff
    style Mixin fill:#fff4e1
    style Bind fill:#c8e6c9
```

**数据流特性**：
- ✅ **多重继承**：通过接口混入实现多重继承效果
- ✅ **契约约束**：通过接口明确定义类必须实现的方法
- ✅ **灵活组合**：可以混入多个接口，实现功能组合
- ✅ **运行时绑定**：接口方法在运行时绑定到类实例

---

## 接口模块架构设计

### 核心职责

接口定义 + 接口混入 + 多重继承支持

### 架构图

```mermaid
graph TB
    subgraph Interface["Interface接口基类"]
        InterfaceBase["接口基类<br/>Interface"]
        ByInheritance["混入方法<br/>ByInheritance(target)"]
        MethodBinding["方法绑定<br/>绑定接口方法到目标"]
    end
    
    subgraph ConcreteInterface["具体接口"]
        MyInterface["MyInterface<br/>自定义接口"]
        IEvent["IEvent<br/>事件接口"]
        IDecorator["IDecorator<br/>装饰器接口"]
    end
    
    subgraph TargetClass["目标类"]
        MyClass["MyClass<br/>使用接口的类"]
        MethodImplementation["方法实现<br/>实现接口方法"]
    end
    
    Interface -->|继承| ConcreteInterface
    ConcreteInterface -->|混入| TargetClass
    TargetClass -->|实现| MethodImplementation
    
    style Interface fill:#e1f5ff
    style ConcreteInterface fill:#fff4e1
    style TargetClass fill:#c8e6c9
```

### 工作流程

```mermaid
flowchart TD
    Start[创建类实例<br/>MyClass.New()] --> Ctor[调用构造函数<br/>ctor]
    Ctor --> CreateInterface[创建接口实例<br/>MyInterface.New()]
    CreateInterface --> CallMixin[调用混入方法<br/>ByInheritance(self)]
    CallMixin --> BindMethods[绑定接口方法<br/>遍历接口方法]
    BindMethods --> CheckMethod{目标类<br/>是否已实现方法?}
    CheckMethod -->|是| Skip[跳过绑定<br/>使用类实现]
    CheckMethod -->|否| Bind[绑定接口方法<br/>self.method = interface.method]
    Bind --> Complete[混入完成]
    Skip --> Complete
    
    style CheckMethod fill:#fff4e1,stroke:#333,stroke-width:2px
    style Bind fill:#c8e6c9
    style Complete fill:#c8e6c9
```

---

## 事件模块架构设计

### 核心职责

事件注册 + 事件触发 + 事件管理

### 架构图

```mermaid
graph TB
    subgraph IEvent["IEvent事件接口"]
        EventRegistry["事件注册表<br/>eventListeners[eventName]"]
        SetEventListener["注册事件<br/>SetEventListener"]
        TriggerEvent["触发事件<br/>TriggerEvent"]
        RemoveListener["移除监听<br/>RemoveListener"]
    end
    
    subgraph EventFlow["事件流"]
        Register[注册事件监听<br/>SetEventListener]
        Trigger[触发事件<br/>TriggerEvent]
        Notify[通知所有监听者<br/>调用回调函数]
    end
    
    IEvent -->|管理| EventRegistry
    IEvent -->|执行| EventFlow
    
    style IEvent fill:#e1f5ff
    style EventFlow fill:#fff4e1
```

---

## 装饰器模块架构设计

### 核心职责

装饰器定义 + 装饰器安装 + 装饰器执行链

### 架构图

```mermaid
graph TB
    subgraph IDecorator["IDecorator装饰器接口"]
        DecoratorBase["装饰器基类<br/>IDecorator"]
        DecorateMethod["装饰方法<br/>覆盖或扩展原方法"]
    end
    
    subgraph IDecoratorBehavior["IDecoratorBehavior装饰器行为"]
        DecoratorList["装饰器列表<br/>decoratorList"]
        AddDecorator["添加装饰器<br/>AddExtendDecorator"]
        ExecuteChain["执行链<br/>按顺序执行装饰器"]
    end
    
    subgraph DecoratorFlow["装饰器流"]
        OriginalMethod[原始方法<br/>Character:Attack]
        Decorator1[装饰器1<br/>FireDecorator:Attack]
        Decorator2[装饰器2<br/>IceDecorator:Attack]
        FinalResult[最终结果<br/>组合所有装饰器效果]
    end
    
    IDecorator -->|实现| DecoratorBase
    IDecoratorBehavior -->|管理| DecoratorList
    DecoratorList -->|执行| DecoratorFlow
    
    style IDecorator fill:#e1f5ff
    style IDecoratorBehavior fill:#fff4e1
    style DecoratorFlow fill:#c8e6c9
```

---

## 工厂模块架构设计

### 核心职责

类型注册 + 对象创建 + 对象池管理

### 架构图

```mermaid
graph TB
    subgraph ObjFactory["ObjFactory对象工厂"]
        TypeRegistry["类型注册表<br/>objClassDict[typeName]"]
        CreateObject["创建对象<br/>CreateObject"]
        ObjectPool["对象池<br/>对象复用"]
    end
    
    subgraph FactoryFlow["工厂流"]
        RegisterType[注册类型<br/>AddObjClass]
        CreateByType[按类型创建<br/>CreateObject]
        RecycleObject[回收对象<br/>Push]
    end
    
    ObjFactory -->|管理| TypeRegistry
    ObjFactory -->|执行| FactoryFlow
    
    style ObjFactory fill:#e1f5ff
    style FactoryFlow fill:#fff4e1
```

---

## 架构模式分析

### 接口模式（Interface Pattern）

**核心思想**：通过接口定义契约，通过混入实现多重继承

```mermaid
graph TB
    Interface[接口<br/>Interface]
    ConcreteInterface[具体接口<br/>MyInterface]
    TargetClass[目标类<br/>MyClass]
    
    Interface -->|继承| ConcreteInterface
    ConcreteInterface -->|混入| TargetClass
    TargetClass -->|实现| Method[方法实现]
    
    style Interface fill:#f3e5f5
    style ConcreteInterface fill:#e1f5ff
    style TargetClass fill:#c8e6c9
```

**优势**：
- ✅ **多重继承**：解决Lua单继承的限制
- ✅ **契约约束**：通过接口明确定义类必须实现的方法
- ✅ **灵活组合**：可以混入多个接口，实现功能组合

### 观察者模式（Observer Pattern）

**核心思想**：事件系统实现对象间松耦合通信

```mermaid
graph TB
    Subject[主题<br/>EventUser]
    Observer1[观察者1<br/>Listener1]
    Observer2[观察者2<br/>Listener2]
    
    Subject -->|注册| Observer1
    Subject -->|注册| Observer2
    Subject -->|触发事件| Observer1
    Subject -->|触发事件| Observer2
    
    style Subject fill:#f3e5f5
    style Observer1 fill:#c8e6c9
    style Observer2 fill:#c8e6c9
```

---

## 数据流设计

### 接口混入数据流

```mermaid
sequenceDiagram
    participant Client as 客户端
    participant MyClass as MyClass
    participant Interface as MyInterface
    participant Target as 目标对象
    
    Client->>MyClass: New() 创建实例
    MyClass->>MyClass: ctor() 构造函数
    MyClass->>Interface: New() 创建接口实例
    Interface-->>MyClass: 返回接口实例
    MyClass->>Interface: ByInheritance(self)
    Interface->>Target: 绑定接口方法
    Target-->>Interface: 绑定完成
    Interface-->>MyClass: 混入完成
    MyClass-->>Client: 返回实例
```

### 事件触发数据流

```mermaid
sequenceDiagram
    participant Client as 客户端
    participant EventUser as EventUser
    participant IEvent as IEvent
    participant Listener as 监听者
    
    Client->>EventUser: TriggerEvent("OnChange", value)
    EventUser->>IEvent: TriggerEvent(eventName, ...)
    IEvent->>IEvent: 查找eventListeners[eventName]
    IEvent->>Listener: 调用所有监听者回调
    Listener-->>IEvent: 回调完成
    IEvent-->>EventUser: 触发完成
    EventUser-->>Client: 返回结果
```

---

## 架构验证

### 流程合理性验证

从架构可验证：
- ✅ **数据流完整**：接口混入 → 方法绑定 → 功能使用（完整流程）
- ✅ **职责清晰**：接口模块、事件模块、装饰器模块、工厂模块职责明确
- ✅ **解耦设计**：通过接口和事件系统实现模块间解耦
- ✅ **灵活扩展**：可以轻松添加新的接口和装饰器

### 扩展性验证

从架构可验证：
- ✅ **接口扩展**：新增接口只需继承Interface
- ✅ **装饰器扩展**：新增装饰器只需实现IDecorator
- ✅ **工厂扩展**：新增类型只需注册到工厂
- ✅ **组合复用**：通过接口组合实现功能复用

### 易用性验证

从架构可验证：
- ✅ **简单易用**：流畅的API设计，易于使用
- ✅ **多重继承**：通过接口混入实现多重继承效果
- ✅ **事件驱动**：事件系统实现对象间松耦合通信
- ✅ **装饰器模式**：动态扩展对象功能

---

## 开发指导原则

### 一、开发约束（什么能做，什么不能做）

#### ✅ 应该做的

1. **接口必须继承Interface**
   ```
   ✅ 正确：
   local MyInterface = BaseClass(Interface)
   
   ❌ 错误：
   不继承Interface的接口类
   ```

2. **接口混入必须在ctor中完成**
   ```
   ✅ 正确：
   function MyClass:ctor()
       MyInterface.New():ByInheritance(self)
   end
   
   ❌ 错误：
   在ctor外混入接口
   ```

3. **事件监听必须及时清理**
   ```
   ✅ 正确：
   object:RemoveAllEventListeners()
   
   ❌ 错误：
   不清理事件监听导致内存泄漏
   ```

#### ❌ 不应该做的

1. **禁止在接口方法中直接访问self**
   - 接口方法应该通过参数传递目标对象
   - 不能直接访问self

2. **禁止装饰器之间直接依赖**
   - 装饰器应该独立实现
   - 不能直接调用其他装饰器

3. **禁止工厂直接创建未注册类型**
   - 必须先在工厂中注册类型
   - 不能直接创建未注册的类型

### 二、开发流程（标准化开发步骤）

#### 使用接口的标准流程

```
1. 定义接口
   ↓
   local MyInterface = BaseClass(Interface)
   function MyInterface:Method1() end
   
2. 在类中使用接口
   ↓
   function MyClass:ctor()
       MyInterface.New():ByInheritance(self)
   end
   
3. 实现接口方法
   ↓
   function MyClass:Method1()
       -- 实现逻辑
   end
```

---

## 总结

### 架构设计价值

该架构设计文档的价值在于：
- ✅ **思路解构**：完整解构OOP框架系统的搭建思路
- ✅ **流程验证**：从架构层面验证流程合理性
- ✅ **模式分析**：分析接口模式、观察者模式、装饰器模式、工厂模式的应用
- ✅ **开发指导**：为后续详细设计和实现提供清晰指导

### 设计原则

- ✅ **接口机制为核心**：通过接口机制解决Lua单继承的限制
- ✅ **设计模式组合**：通过组合多种设计模式提供完整的OOP支持
- ✅ **混入机制 + 链式调用**：通过混入机制实现接口继承，通过链式调用提供流畅API
- ✅ **结构化编程**：帮助开发者以更加结构化、可维护的方式编写Lua代码

### 架构特点

- ✅ **多重继承**：通过接口混入实现多重继承效果
- ✅ **事件驱动**：事件系统实现对象间松耦合通信
- ✅ **装饰器模式**：动态扩展对象功能
- ✅ **工厂模式**：简化对象创建和管理

细节实现是后续开发阶段的工作，当前架构设计已足够指导整个OOP框架系统的开发。
