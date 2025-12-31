# Lifecycle生命周期架构设计

## 设计目标

设计一套完整的C#-Lua生命周期桥接系统，支持Unity生命周期事件自动调用Lua函数，实现C#与Lua之间的无缝交互，提供配置化的映射机制和自动代码生成。

---

## 核心设计理念

### 1. 桥接模式为核心

**本质**：生命周期系统的核心是C#与Lua之间的桥接
- C#生命周期 = Unity MonoBehaviour生命周期事件
- Lua函数调用 = 自动调用对应的Lua全局函数
- 映射机制 = 通过mappingName配置映射关系
- 自动生成 = 编辑器工具自动生成Lua函数代码

### 2. 模板方法模式 + 配置化

**本质**：通过模板方法模式定义标准流程，通过配置化实现灵活映射
- 模板方法 = MonoLifecycle定义标准的生命周期流程
- 配置化 = mappingName配置不同的映射类型
- 自动生成 = 编辑器工具根据配置自动生成Lua函数
- 统一接口 = 所有生命周期组件继承自MonoLifecycle

### 3. 职责分离 + 开闭原则

**本质**：每个类只负责特定的生命周期管理，对扩展开放对修改封闭
- 单一职责 = 每个类只负责特定的生命周期管理
- 开闭原则 = 新增生命周期类型只需继承MonoLifecycle
- 配置化 = 通过mappingName实现配置化的接口映射
- 统一接口 = 所有生命周期组件都继承自同一个基类

---

## 整体架构设计

### 三层架构 + 桥接模式

```mermaid
graph TB
    subgraph UnityLayer["MonoBehaviour生命周期<br/>"]
        AwakeEvent["Awake事件"]
        StartEvent["Start事件"]
        OnEnableEvent["OnEnable事件"]
        OnDestroyEvent["OnDestroy事件"]
        OtherEvents["其他生命周期事件"]
    end
    
    subgraph BridgeLayer["桥接层<br/>MonoLifecycle"]
        LifecycleBase["生命周期基类<br/>MonoLifecycle"]
        MappingManager["映射管理器<br/>mappingName"]
        LuaCaller["Lua调用器<br/>调用Lua函数"]
    end
    
    subgraph LuaLayer["Lua层<br/>生成的Lua函数"]
        UnitStart["Unit_Start函数"]
        ItemDestroy["Item_OnDestroy函数"]
        CustomLifecycle["自定义生命周期函数<br/>..."]
    end
    
    UnityLayer -->|触发| BridgeLayer
    BridgeLayer -->|调用| LuaLayer
    
    style UnityLayer fill:#e1f5ff
    style BridgeLayer fill:#fff4e1
    style LuaLayer fill:#c8e6c9
```

### 生命周期桥接数据流

```mermaid
graph TD
    Start[Unity生命周期事件<br/>Start/OnDestroy等] -->|1. 触发| Bridge[桥接层<br/>MonoLifecycle]
    Bridge -->|2. 获取映射名称| Mapping[mappingName<br/>Unit/Item/Custom]
    Mapping -->|3. 构建函数名| FunctionName[函数名<br/>MappingName_MethodName]
    FunctionName -->|4. 调用Lua函数| Lua[Lua层<br/>Unit_Start/Item_OnDestroy]
    Lua -->|5. 执行Lua逻辑| End[完成]
    
    style Bridge fill:#fff4e1
    style Mapping fill:#e1f5ff
    style Lua fill:#c8e6c9
```

**数据流特性**：
- ✅ **自动桥接**：Unity生命周期事件自动调用Lua函数
- ✅ **配置化映射**：通过mappingName配置不同的映射类型
- ✅ **统一接口**：所有生命周期组件使用统一的桥接机制
- ✅ **自动生成**：编辑器工具自动生成Lua函数代码

---

## 桥接层架构设计

### 核心职责

生命周期事件捕获 + 映射管理 + Lua函数调用

### 架构图

```mermaid
graph TB
    subgraph MonoLifecycle["MonoLifecycle基类"]
        LifecycleMethods["生命周期方法<br/>Awake/Start/OnDestroy等"]
        MappingName["映射名称<br/>mappingName"]
        LuaCaller["Lua调用器<br/>CallLuaFunction"]
        ErrorHandler["错误处理器<br/>错误日志/异常处理"]
    end
    
    subgraph ConcreteLifecycle["具体生命周期类"]
        UnitLifecycle["UnitLifecycle<br/>单位生命周期"]
        ItemLifecycle["ItemLifecycle<br/>物品生命周期"]
        CustomLifecycle["自定义生命周期类<br/>..."]
    end
    
    subgraph EditorTool["编辑器工具<br/>CS2LuaLifecycleEditor"]
        ConfigManager["配置管理器<br/>mappingConfigs"]
        CodeGenerator["代码生成器<br/>生成Lua函数"]
        FileWriter["文件写入器<br/>写入Lua文件"]
    end
    
    MonoLifecycle -->|继承| ConcreteLifecycle
    EditorTool -->|生成| LuaLayer[Lua层<br/>生成的Lua函数]
    
    style MonoLifecycle fill:#e1f5ff
    style ConcreteLifecycle fill:#fff4e1
    style EditorTool fill:#c8e6c9
```

### 工作流程

```mermaid
flowchart TD
    Start[Unity生命周期事件<br/>Start/OnDestroy] --> Override[重写生命周期方法<br/>protected override]
    Override --> GetMapping[获取映射名称<br/>mappingName]
    GetMapping --> BuildFunctionName[构建函数名<br/>MappingName_MethodName]
    BuildFunctionName --> CallLua[调用Lua函数<br/>CallLuaFunction]
    CallLua --> CheckLua{Lua函数<br/>是否存在?}
    CheckLua -->|是| ExecuteLua[执行Lua逻辑]
    CheckLua -->|否| LogError[记录错误日志]
    ExecuteLua --> Complete[完成]
    LogError --> Complete
    
    Generate[编辑器生成Lua函数] --> ReadConfig[读取配置<br/>mappingConfigs]
    ReadConfig --> GenerateCode[生成代码<br/>Unit_Start]
    GenerateCode --> WriteFile[写入文件<br/>MonoLifecycleGlobal.lua]
    
    style CheckLua fill:#fff4e1,stroke:#333,stroke-width:2px
    style CallLua fill:#c8e6c9
    style GenerateCode fill:#c8e6c9
```

---

## 编辑器工具架构设计

### 核心职责

配置管理 + 代码生成 + 文件写入

### 架构图

```mermaid
graph TB
    subgraph EditorTool["CS2LuaLifecycleEditor编辑器工具"]
        ConfigManager["配置管理器<br/>mappingConfigs"]
        MethodConfig["方法配置<br/>lifecycleMethods"]
        CodeGenerator["代码生成器<br/>GenerateLuaFunctions"]
        FileWriter["文件写入器<br/>WriteToFile"]
    end
    
    subgraph ConfigData["配置数据"]
        MappingConfig["映射配置<br/>{name: 'Unit', methods: [...]}"]
        MethodConfig["方法配置<br/>{name: 'Start', params: [...]}"]
    end
    
    subgraph GeneratedCode["生成的代码"]
        LuaFunctions["Lua函数<br/>function Unit_Start(...) end"]
        GlobalTable["全局表<br/>MonoLifecycleGlobal"]
    end
    
    EditorTool -->|读取| ConfigData
    EditorTool -->|生成| GeneratedCode
    
    style EditorTool fill:#e1f5ff
    style ConfigData fill:#fff4e1
    style GeneratedCode fill:#c8e6c9
```

### 代码生成流程

```mermaid
flowchart TD
    Start[用户点击生成按钮] --> ReadConfig[读取配置<br/>mappingConfigs + lifecycleMethods]
    ReadConfig --> Loop[遍历映射配置]
    Loop --> ForEachMapping[对每个映射配置]
    ForEachMapping --> ForEachMethod[对每个生命周期方法]
    ForEachMethod --> GenerateFunction[生成函数代码<br/>MappingName_MethodName]
    GenerateFunction --> AddToCode[添加到代码字符串]
    AddToCode --> CheckMore{还有方法?}
    CheckMore -->|是| ForEachMethod
    CheckMore -->|否| CheckMoreMapping{还有映射?}
    CheckMoreMapping -->|是| ForEachMapping
    CheckMoreMapping -->|否| WriteFile[写入文件<br/>MonoLifecycleGlobal.lua]
    WriteFile --> Complete[生成完成]
    
    style CheckMore fill:#fff4e1,stroke:#333,stroke-width:2px
    style CheckMoreMapping fill:#fff4e1,stroke:#333,stroke-width:2px
    style GenerateFunction fill:#c8e6c9
    style WriteFile fill:#c8e6c9
```

---

## 架构模式分析

### 桥接模式（Bridge Pattern）

**核心思想**：将C#生命周期和Lua函数调用分离，通过桥接层连接

```mermaid
graph TB
    Abstraction[抽象层<br/>Unity生命周期]
    Implementor[实现层<br/>Lua函数]
    Bridge[桥接层<br/>MonoLifecycle]
    
    Abstraction -->|桥接| Bridge
    Bridge -->|调用| Implementor
    
    style Abstraction fill:#e1f5ff
    style Bridge fill:#f3e5f5
    style Implementor fill:#c8e6c9
```

**优势**：
- ✅ **解耦设计**：C#生命周期和Lua实现分离
- ✅ **灵活扩展**：可以轻松添加新的生命周期类型
- ✅ **统一接口**：所有生命周期组件使用统一的桥接机制

### 模板方法模式（Template Method Pattern）

**核心思想**：MonoLifecycle定义标准的生命周期流程

```mermaid
graph TB
    TemplateMethod[模板方法<br/>MonoLifecycle<br/>定义标准流程]
    
    Step1[步骤1：获取映射名称<br/>抽象步骤]
    Step2[步骤2：构建函数名<br/>具体步骤]
    Step3[步骤3：调用Lua函数<br/>具体步骤]
    
    ConcreteStep1[具体实现1<br/>UnitLifecycle.mappingName]
    ConcreteStep2[具体实现2<br/>ItemLifecycle.mappingName]
    
    TemplateMethod --> Step1
    TemplateMethod --> Step2
    TemplateMethod --> Step3
    
    Step1 --> ConcreteStep1
    Step1 --> ConcreteStep2
    
    style TemplateMethod fill:#f3e5f5
    style Step1 fill:#e1f5ff
    style Step2 fill:#fff4e1
    style Step3 fill:#fff4e1
```

---

## 数据流设计

### 生命周期事件数据流

```mermaid
sequenceDiagram
    participant Unity as Unity引擎
    participant MonoLifecycle as MonoLifecycle
    participant Mapping as MappingManager
    participant Lua as Lua环境
    
    Unity->>MonoLifecycle: Start() 生命周期事件
    MonoLifecycle->>Mapping: 获取mappingName
    Mapping-->>MonoLifecycle: 返回 "Unit"
    MonoLifecycle->>MonoLifecycle: 构建函数名 "Unit_Start"
    MonoLifecycle->>Lua: 调用 Unit_Start(gameObject, ...)
    Lua->>Lua: 执行Lua逻辑
    Lua-->>MonoLifecycle: 返回结果
    MonoLifecycle-->>Unity: 生命周期处理完成
```

### 代码生成数据流

```mermaid
sequenceDiagram
    participant User as 用户
    participant Editor as CS2LuaLifecycleEditor
    participant Config as 配置数据
    participant Generator as 代码生成器
    participant File as Lua文件
    
    User->>Editor: 点击生成按钮
    Editor->>Config: 读取mappingConfigs
    Config-->>Editor: 返回配置数据
    Editor->>Generator: 生成Lua函数代码
    Generator->>Generator: 遍历配置生成代码
    Generator-->>Editor: 返回生成的代码
    Editor->>File: 写入MonoLifecycleGlobal.lua
    File-->>Editor: 写入完成
    Editor-->>User: 生成完成
```

---

## 架构验证

### 流程合理性验证

从架构可验证：
- ✅ **数据流完整**：Unity事件 → 桥接层 → Lua函数（完整流程）
- ✅ **职责清晰**：Unity层、桥接层、Lua层职责明确，无重叠
- ✅ **解耦设计**：通过桥接模式实现C#和Lua解耦
- ✅ **配置化**：通过mappingName实现配置化的映射

### 扩展性验证

从架构可验证：
- ✅ **模板方法模式**：新增生命周期类型只需继承MonoLifecycle
- ✅ **配置化映射**：通过mappingName配置不同的映射类型
- ✅ **自动生成**：编辑器工具自动生成Lua函数代码
- ✅ **统一接口**：所有生命周期组件使用统一的桥接机制

### 易用性验证

从架构可验证：
- ✅ **简单配置**：只需设置mappingName即可完成配置
- ✅ **自动生成**：编辑器工具自动生成Lua函数代码
- ✅ **统一接口**：所有生命周期组件使用统一的接口
- ✅ **错误处理**：完善的错误日志和异常处理

---

## 开发指导原则

### 一、开发约束（什么能做，什么不能做）

#### ✅ 应该做的

1. **生命周期类必须继承MonoLifecycle**
   ```
   ✅ 正确：
   public class UnitLifecycle : MonoLifecycle
   
   ❌ 错误：
   不继承MonoLifecycle的生命周期类
   ```

2. **必须设置mappingName**
   ```
   ✅ 正确：
   SetMappingName("Unit")
   
   ❌ 错误：
   不设置mappingName
   ```

3. **必须调用基类方法**
   ```
   ✅ 正确：
   protected override void Start()
   {
       base.Start();
   }
   
   ❌ 错误：
   不调用base.Start()
   ```

#### ❌ 不应该做的

1. **禁止直接调用Lua函数**
   - 必须通过MonoLifecycle的桥接机制
   - 不能直接调用Lua函数

2. **禁止修改生成的Lua函数**
   - 生成的Lua函数应该由编辑器工具管理
   - 不能手动修改生成的代码

3. **禁止在生命周期方法中执行耗时操作**
   - 生命周期方法应该快速执行
   - 耗时操作应该异步处理

### 二、开发流程（标准化开发步骤）

#### 开发新生命周期类型的流程

```
1. 创建生命周期类
   ↓
   public class MyLifecycle : MonoLifecycle
   
2. 设置映射名称
   ↓
   SetMappingName("MyCustom")
   
3. 重写生命周期方法
   ↓
   protected override void Start() { base.Start(); }
   
4. 使用编辑器工具生成Lua函数
   ↓
   Tools/CS2LuaLifecycle/生成Lua生命周期函数
   
5. 在Lua端实现函数
   ↓
   function MyCustom_Start(gameObject, ...) end
```

---

## 总结

### 架构设计价值

该架构设计文档的价值在于：
- ✅ **思路解构**：完整解构生命周期桥接系统的搭建思路
- ✅ **流程验证**：从架构层面验证流程合理性
- ✅ **模式分析**：分析桥接模式、模板方法模式的应用
- ✅ **开发指导**：为后续详细设计和实现提供清晰指导

### 设计原则

- ✅ **桥接模式为核心**：C#生命周期和Lua函数调用通过桥接层连接
- ✅ **模板方法模式 + 配置化**：通过模板方法定义标准流程，通过配置化实现灵活映射
- ✅ **职责分离 + 开闭原则**：每个类只负责特定的生命周期管理，对扩展开放对修改封闭
- ✅ **自动生成**：编辑器工具自动生成Lua函数代码

### 架构特点

- ✅ **自动桥接**：Unity生命周期事件自动调用Lua函数
- ✅ **配置化映射**：通过mappingName配置不同的映射类型
- ✅ **统一接口**：所有生命周期组件使用统一的桥接机制
- ✅ **自动生成**：编辑器工具自动生成Lua函数代码

细节实现是后续开发阶段的工作，当前架构设计已足够指导整个生命周期桥接系统的开发。
