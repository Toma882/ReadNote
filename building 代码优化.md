# Building 代码优化

## 1. 数据层级调整

### 1.1 父类 Player 调整

- **当前状态**：使用 `single`
- **调整方案**：改为使用 `G`

### 1.2 G.building 数据结构

```lua
G.building = {
    SData = {
        -- Pmall 等服务器数据
    },
    CData = {
        floor = {
            -- 商店设备等客户端数据
        }
    },
    -- ... 其他数据
    -- Object
    -- SData ?? component
}
```

### 1.3 G.me 相关

- **路径**：`G.me.Buildings`
- **原则**：`G.me` 不要存在 System
- **System 管理**：
  - `BuildSystem`
  - `GameState`
  - `me.building` 你拥有的数量，例如：1000

### 1.4 G.other 相关

```lua
G.other[1001]  -- Player
```

---

## 2. Data 层级

### 2.1 Program Language 中的 Data

- **Data decl**（数据声明）
- **Data Type**（数据类型）
- **Data inst**（数据实例）
- **Data copy**（数据拷贝）

### 2.2 NetData

网络数据传输类型：
- `string`
- `int`
- `float`
- `class`

### 2.3 Obj（对象）

- **ServerObject**
  - `ObjectPool`
- **Client**
- **dbObj**

### 2.4 OOP / ESC / Lua

**ESC（Entity-Component-System）架构：**
- **E (Entity)** = `GameObject`
- **C (Component)** = `Building`
- **S (System)** = `BuildingSystem:Set(Building)`
  - 类似 Mgr（管理器）

---

## 3. 请求响应处理

### 3.1 req(resp) v1

```lua
-- req(resp) v1
local building = resp.building

building.cfg = CfgData.building[building.id]

local prefab = ResNode:Inst()
prefab.AddTable(building)
```

### 3.2 req(resp) v2

```lua
-- req(resp) v2
-- resp.building
local building = Building:New()  -- ??

building.serverObject = resp.building
building.cfg = CfgData.building[building.id]

GameObject go = new GameObject

local prefab = ResNode:Inst()
prefab.AddTable(building)
```

---

## 4. 生命周期管理

### 4.1 Create（创建）

```lua
-- 通过 req(resp) 创建
```

### 4.2 Destroy（销毁）                                            

```lua
GameObject.Destroy(building.gameObject)
-- 原则：same Layer + same life cycle
-- building.gameObjectId
```

### 4.3 Get（获取）

```lua
buildingSystem:Set(building)
-- map[building.dbId] = building
```

### 4.4 Set（设置）

```lua
local building = buildingSystem:Get(dbId)
```

### 4.5 Update（更新）

#### UpdateBuildingAction

```csharp
class UpdateBuildingAction : SynAction
{
    long buildingId;
    int level;
    int worker;
}

Execute()
{
    local building = buildingSystem:Get(updateBuilding.Id)
    building.serverObject.worker = worker
}
```

#### UpdateBuildingWorkerAction

```csharp
class UpdateBuildingWorkerAction : SynAction
{
    long buildingId;
    int worker;
}

Execute()
{
    local building = buildingSystem:Get(updateBuilding.Id)
    building.serverObject.worker = worker
}
```

#### UpdateBuildingAction（完整对象更新）

```csharp
class UpdateBuildingAction : SynAction
{
    Building updateBuilding;
}

Execute()
{
    local building = buildingSystem:Get(updateBuilding.Id)
    building.serverObject = updateBuilding
}
```

---

## 5. BuildingSystem

**职责**：数据管理，所有数据从这里获取

```lua
buildingSystem = {
    -- 数据管理，所有数据这里拿
}
```

---

## 6. UI

### 6.1 优化方向

- **通用模板抽取**：可能需要抽一些通用模板
- **统一管理**：将 UI 统一以 GameObject 为单位承载功能

---

## 7. 设计评价与建议

### 7.1 优点 ✅

#### 1. **数据分离清晰**
- ✅ **SData / CData 分离**：明确区分服务器数据和客户端数据，符合关注点分离原则
- ✅ **G.me / G.other 分离**：区分自己的数据和他人数据，避免数据混乱

#### 2. **职责明确**
- ✅ **BuildingSystem 单一职责**：作为数据管理中心，所有数据从这里获取，符合单一职责原则
- ✅ **G.me 不存 System**：数据层和系统层分离，符合分层架构原则

#### 3. **生命周期管理完整**
- ✅ **Create/Destroy/Get/Set/Update**：完整的生命周期管理，便于追踪和调试
- ✅ **same Layer + same life cycle**：统一的生命周期管理原则

#### 4. **架构演进**
- ✅ **v1 → v2 演进**：从直接使用 resp 到封装 Building 对象，体现了架构优化思路

### 7.2 潜在问题 ⚠️

#### 1. **Get/Set 命名混淆**
```lua
-- 问题：Get 和 Set 的语义与常见理解相反
buildingSystem:Set(building)  -- 实际是存储
local building = buildingSystem:Get(dbId)  -- 实际是获取
```

**建议**：
- 使用更明确的命名：`Add/Register` 用于存储，`Get/Find` 用于获取
- 或者统一使用：`Set(id, building)` 和 `Get(id)`

#### 2. **Update 操作设计冗余**
```csharp
// 问题：UpdateBuildingAction 和 UpdateBuildingWorkerAction 职责重叠
class UpdateBuildingAction {
    long buildingId;
    int level;
    int worker;  // 这里也有 worker
}

class UpdateBuildingWorkerAction {
    long buildingId;
    int worker;  // 重复了
}
```

**建议**：
- 使用**命令模式**或**策略模式**，避免 Action 类膨胀
- 考虑使用**部分更新**机制，只更新变化的字段

#### 3. **ESC 架构理解偏差**
```lua
-- 问题：ESC 架构中，System 不应该直接 Set Component
-- S (System) = BuildingSystem:Set(Building)  -- 这个理解有偏差
```

**建议**：
- ESC 中 System 应该**查询 Entity 和 Component**，而不是直接 Set
- System 负责**逻辑处理**，Component 负责**数据存储**
- 正确的流程：`System:Update()` → 查询 Entity/Component → 处理逻辑 → 更新 Component

#### 4. **数据层级概念模糊**
```
Data decl / Data Type / Data inst / Data copy
NetData / Obj / ServerObject / Client / dbObj
```
这些概念之间的关系和职责不够清晰。

**建议**：
- 绘制**数据流图**，明确数据从网络 → 对象 → 组件的转换流程
- 定义清晰的**数据转换层**（Adapter/Converter）

### 7.3 改进建议 💡

#### 1. **明确数据架构**

```lua
-- 建议的数据架构
G = {
    -- 全局配置数据（只读）
    building = {
        configs = {},  -- 配置表数据
    },
    
    -- 玩家数据（读写）
    me = {
        buildings = {},  -- 玩家拥有的建筑实例
        -- 注意：这里不存 System
    },
    
    -- 其他玩家数据（只读）
    other = {
        [playerId] = {
            buildings = {},
        }
    }
}

-- System 独立管理
BuildingSystem = {
    buildingMap = {},  -- map[dbId] = building
    -- 所有建筑数据从这里获取
}
```

#### 2. **统一生命周期接口**

```lua
-- 建议的生命周期接口
BuildingLifecycle = {
    Create = function(serverData)
        -- 1. 创建 Building 对象
        -- 2. 绑定 GameObject
        -- 3. 注册到 BuildingSystem
    end,
    
    Destroy = function(building)
        -- 1. 从 BuildingSystem 注销
        -- 2. 销毁 GameObject
        -- 3. 清理资源
    end,
    
    Update = function(building, updateData)
        -- 统一更新接口
    end
}
```

#### 3. **优化 Update 机制**

```lua
-- 建议：使用部分更新机制
UpdateBuildingAction = {
    buildingId = 123,
    updates = {
        level = 5,      -- 只更新 level
        worker = 10,    -- 只更新 worker
    }
}

-- 执行时只更新指定字段
function Execute(action)
    local building = BuildingSystem:Get(action.buildingId)
    for key, value in pairs(action.updates) do
        building.serverObject[key] = value
    end
end
```

#### 4. **完善错误处理**

```lua
-- 建议：添加错误处理和验证
function BuildingSystem:Get(dbId)
    local building = self.buildingMap[dbId]
    if not building then
        LogWarning("Building not found: " .. dbId)
        return nil
    end
    return building
end

function BuildingSystem:Set(building)
    if not building or not building.dbId then
        LogError("Invalid building data")
        return false
    end
    self.buildingMap[building.dbId] = building
    return true
end
```

#### 5. **数据同步机制**

```lua
-- 建议：明确数据同步流程
-- 网络数据 → ServerObject → Building Component → GameObject
function SyncBuildingData(serverData)
    local building = BuildingSystem:Get(serverData.id)
    if not building then
        building = Building:New()
        BuildingSystem:Set(building)
    end
    
    -- 更新 ServerObject
    building.serverObject = serverData
    
    -- 同步到 Component
    building:SyncToComponent()
    
    -- 同步到 GameObject
    building:SyncToGameObject()
end
```

### 7.4 总结

**整体评价**：⭐⭐⭐⭐ (4/5)

这是一个**思路清晰、方向正确**的优化方案，主要优点：
- ✅ 数据分离明确
- ✅ 职责划分清晰
- ✅ 生命周期管理完整

**需要改进的地方**：
- ⚠️ 部分概念需要更明确的定义
- ⚠️ 命名和接口设计需要优化
- ⚠️ 错误处理和边界情况需要考虑

**建议实施步骤**：
1. 先明确数据架构和职责边界
2. 统一命名规范和接口设计
3. 完善错误处理和验证机制
4. 逐步重构，保持向后兼容
