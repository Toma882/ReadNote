# Odin Inspector API 演示系统

欢迎来到 Odin Inspector API 演示系统！这个项目旨在通过一系列实际可运行的示例，帮助你快速理解和掌握 Odin Inspector 的强大功能。

每个示例都按功能分类，并提供了详细的中文注释，解释了每个特性（Attribute）的具体功能、参数以及常见使用场景。

## 目录结构

```
OdinSamples/
├── README.md               # 项目总览 (当前文件)
│
├── 01_BasicAttributes/     # 基础属性演示 (LabelText, Tooltip, ReadOnly, Title等)
│   └── BasicAttributesSample.cs
│
├── 02_Collections/         # 集合类型演示 (List, Array, Dictionary, TableList等)
│   └── CollectionsSample.cs
│
├── 03_Buttons/             # 按钮功能演示 (Button, ButtonGroup, GUIColor等)
│   └── ButtonsSample.cs
│
├── 04_Validation/          # 数据验证演示 (Required, ValidateInput, InfoBox等)
│   └── ValidationSample.cs
│
├── 05_Groups/              # 属性分组演示 (BoxGroup, TabGroup, FoldoutGroup, HorizontalGroup等)
│   └── GroupsSample.cs
│
├── 06_Conditional/         # 条件显示演示 (ShowIf, HideIf, EnableIf, DisableIf等)
│   └── ConditionalSample.cs
│
├── 07_ValueDropdowns/      # 值下拉选择器演示 (ValueDropdown, AssetSelector等)
│   └── ValueDropdownsSample.cs
│
├── 08_Advanced/            # 高级特性组合演示 (PropertyOrder, CustomDrawer, OnInspectorGUI等)
│   └── AdvancedSample.cs
│
├── 09_EditorWindows/       # Odin编辑器窗口演示 (OdinEditorWindow, OdinMenuEditorWindow)
│   ├── SimpleOdinWindowSample.cs
│   ├── OdinMenuWindowSample.cs
│   └── AdvancedWindowSample.cs
│
├── 10_CustomEditors/       # 自定义Odin编辑器演示 (OdinEditor)
│   ├── CustomOdinEditorSample.cs
│   └── Editor/
│       └── CustomOdinEditorSampleEditor.cs
│
├── 12_Serialization/      # Odin序列化系统演示 (SerializedMonoBehaviour, OdinSerialize)
│   └── SerializationSample.cs
│
├── 11_CompleteProject/     # ⭐ 完整项目示例 - 游戏数据管理器
│   ├── README.md           # 详细项目说明
│   ├── Data/               # 示例数据资源
│   │   ├── Characters/     # 角色数据
│   │   ├── Skills/         # 技能数据
│   │   └── Items/          # 物品数据
│   └── Scripts/
│       ├── Core/           # 核心数据类
│       │   ├── Character.cs
│       │   ├── Skill.cs
│       │   └── Item.cs
│       ├── Editor/         # 编辑器扩展
│       │   └── GameDataEditor.cs
│       └── Utility/        # 辅助类
│           ├── DataTypes.cs
│           └── StatList.cs
│
└── Editor/                 # 编辑器工具
    └── OdinSamplesCreator.cs # 快速创建示例的工具
```

## 如何使用

### 基础示例（01-10）

1. 在 Unity 中，通过菜单 `Tools > Odin Samples > Create Samples` 创建示例对象
2. 在 Hierarchy 中选择示例对象
3. 在 Inspector 中查看 Odin 属性的效果
4. 阅读代码注释了解每个属性的用法

### ⭐ 完整项目示例（11）

1. 通过菜单 `Tools > Odin Samples > Complete Project > 打开游戏数据编辑器`
2. 点击工具栏按钮创建角色、技能、物品等数据
3. 在左侧菜单中浏览和编辑数据
4. 参考 `11_CompleteProject/README.md` 获取详细说明

**完整项目示例展示了：**
- 数据驱动设计架构
- OdinMenuEditorWindow 的实际应用
- 复杂数据结构的组织
- 自定义编辑器工作流程
- 模块化系统设计

## 知识点体系与深化路径

### 📚 知识点映射表

| 本项目章节 | 核心知识点 | 官方深化案例 | 深化内容 |
|-----------|-----------|-------------|---------|
| **01_BasicAttributes** | 基础属性装饰 | → 所有 Demo 通用 | 查看官方如何组合使用 |
| **02_Collections** | 集合和列表显示 | → `RPG Editor/Character.cs` | 二维数组序列化、复杂集合 |
| **03_Buttons** | 按钮和方法调用 | → `RPG Editor/RPGEditorWindow.cs` | 工具栏按钮、创建资源按钮 |
| **04_Validation** | 数据验证 | → `Custom Drawers/ValidationExample.cs` | 自定义验证器、复杂验证逻辑 |
| **05_Groups** | 布局分组 | → `RPG Editor/Item.cs` | 复杂嵌套分组、响应式布局 |
| **06_Conditional** | 条件显示 | → `RPG Editor/Item.cs` | 条件表达式、动态 UI |
| **07_ValueDropdowns** | 下拉选择器 | → `RPG Editor/Item.cs` | 动态数据源、验证联动 |
| **08_Advanced** | 高级特性组合 | → `Custom Drawers/` 所有案例 | 自定义绘制、底层 API |
| **09_EditorWindows** | 编辑器窗口 | → `Editor Windows/` 所有案例 | 高级窗口特性、性能优化 |
| **10_CustomEditors** | 自定义编辑器 | → `RPG Editor/ItemDrawer.cs` | 完全自定义绘制器 |
| **12_Serialization** | Odin序列化系统 | → `Serialization/` 所有案例 | Dictionary序列化、接口序列化、多态序列化 |
| **⭐ 11_CompleteProject** | 完整项目架构 | → `Sample - RPG Editor/` 整个项目 | 生产级架构、拖放、撤销 |

---

## 🎓 系统学习路径（4 阶段渐进式）

### 📖 阶段 1：基础入门（预计 1-2 小时）

**学习目标**：掌握 Odin Inspector 所有常用属性

#### 学习步骤：
```
01_BasicAttributes → 02_Collections → 03_Buttons → 04_Validation
```

#### 知识点与特性说明：

**01_BasicAttributes（基础属性装饰）**
- `[Title("标题")]` - 显示分组标题
- `[LabelText("标签名")]` - 自定义字段标签
- `[Tooltip("提示文本")]` - 鼠标悬停提示
- `[ReadOnly]` - 字段只读，不可编辑
- `[HideLabel]` - 隐藏字段标签
- `[DisplayAsString]` - 以字符串形式显示
- `[InfoBox("信息", InfoMessageType.Info)]` - 显示信息框
  - 参数：InfoMessageType（None/Info/Warning/Error）
- `[PropertySpace(SpaceBefore = 10)]` - 添加间距
- `[ProgressBar(0, 100)]` - 进度条显示
  - 参数：最小值、最大值、颜色获取器
- `[SuffixLabel("单位")]` - 字段后缀标签
- `[PrefixLabel("前缀")]` - 字段前缀标签

**02_Collections（集合类型显示）**
- `[ListDrawerSettings]` - 列表绘制设置
  - ShowItemCount：显示项目数量
  - DraggableItems：可拖动排序
  - ShowIndexLabels：显示索引标签
  - CustomAddFunction：自定义添加函数
- `[TableList]` - 表格形式显示列表
  - ShowIndexLabels：显示行号
  - AlwaysExpanded：始终展开
  - DrawScrollView：显示滚动条
- `[InlineEditor]` - 内联编辑器
  - InlineEditorModes：显示模式（Full/GUIAndHeader/...）
- `[AssetsOnly]` - 仅允许资源引用
- `[SceneObjectsOnly]` - 仅允许场景对象
- `[Searchable]` - 为集合添加搜索栏

**03_Buttons（按钮与方法调用）**
- `[Button("按钮文本")]` - 在 Inspector 中显示按钮
  - ButtonSizes：按钮大小（Small/Medium/Large/Gigantic）
  - ButtonStyle：按钮样式
- `[ButtonGroup("组名")]` - 按钮分组
- `[GUIColor(r, g, b)]` - 设置按钮颜色
  - 参数：RGB 值（0-1）或颜色获取器
- `[HorizontalGroup("组名")]` - 水平排列按钮
- `[EnableIf("条件")]` / `[DisableIf("条件")]` - 条件启用/禁用

**04_Validation（数据验证）**
- `[Required]` - 必填验证（不能为 null）
  - InfoMessageType：错误提示类型
- `[ValidateInput("验证方法", "错误消息")]` - 自定义验证
  - 参数：验证方法名、错误消息、消息类型
- `[MinValue(0)]` / `[MaxValue(100)]` - 数值范围限制
- `[Range(0, 100)]` - 滑动条范围限制
- `[MinMaxSlider(0, 100)]` - 最小最大值滑动条
- `[FilePath]` / `[FolderPath]` - 路径选择器
  - ParentFolder：父文件夹
  - Extensions：文件扩展名
  - AbsolutePath：是否绝对路径

#### 完成标志：
- [x] 能够使用基础属性美化 Inspector
- [x] 理解集合类型的各种显示方式
- [x] 会添加按钮执行方法
- [x] 能添加基本的数据验证

#### 💡 深化方向：
> 学完后，浏览官方所有 Demo，观察这些基础属性如何在实际项目中组合使用

---

### 🔧 阶段 2：进阶技巧（预计 2-3 小时）

**学习目标**：掌握布局、条件显示、高级特性

#### 学习步骤：
```
05_Groups → 06_Conditional → 07_ValueDropdowns → 08_Advanced
```

#### 知识点：
- ✅ BoxGroup、TabGroup、HorizontalGroup 等布局
- ✅ ShowIf、HideIf、EnableIf 条件控制
- ✅ ValueDropdown 动态下拉列表
- ✅ ProgressBar、OnValueChanged 等高级特性

#### 完成标志：
- [x] 能设计复杂的 Inspector 布局
- [x] 会根据条件动态显示/隐藏属性
- [x] 能创建动态数据源的下拉列表
- [x] 理解属性的生命周期和回调

#### 💡 深化方向：
**重点研究：** `Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/Scripts/Items/Item.cs`
```
为什么看这个文件？
✓ 展示了复杂的嵌套分组（HorizontalGroup + VerticalGroup + BoxGroup）
✓ 使用 ShowIf 根据物品类型动态显示不同属性
✓ ValueDropdown 配合 ValidateInput 实现数据联动
✓ 几乎用到了所有进阶技巧
```

**对比学习：**
- 打开 `Item.cs`，对比我们的 `11_CompleteProject/Scripts/Core/Item.cs`
- 看看官方如何组织更复杂的布局
- 学习 `const string` 定义分组名称的技巧
- 理解 `SupportedItemTypes` 的验证模式

---

### 🏗️ 阶段 3：实战应用（预计 3-5 小时）

**学习目标**：构建完整的编辑器工具

#### 学习步骤：
```
09_EditorWindows → 10_CustomEditors → 12_Serialization → ⭐ 11_CompleteProject
```

#### 知识点：
- ✅ OdinEditorWindow 基础窗口
- ✅ OdinMenuEditorWindow 带菜单的窗口
- ✅ OdinEditor 自定义 Inspector
- ✅ Odin 序列化系统（Dictionary、接口、抽象类）
- ✅ 完整的数据管理工作流程

#### 完成标志：
- [ ] 能创建简单的编辑器窗口
- [ ] 会构建菜单树结构
- [ ] 能自定义对象的 Inspector
- [ ] 理解 Odin 序列化系统（Dictionary、接口、抽象类）
- [ ] 理解完整项目的架构设计

#### 💡 深化方向 A：编辑器窗口深化

**重点研究：** `Assets/Plugins/Sirenix/Demos/Editor Windows/Scripts/Editor/`

1. **OdinMenuEditorWindowExample.cs**（对比我们的 `GameDataEditor.cs`）
   ```
   学习要点：
   ✓ AddAllAssetsAtPath 的高级用法
   ✓ 添加图标到菜单项
   ✓ MenuStyle 自定义样式
   ✓ 多选支持
   ```

2. **OdinMenuStyleExample.cs**（我们没有的）
   ```
   学习要点：
   ✓ 完全自定义菜单样式
   ✓ 图标大小、间距、边距调整
   ✓ 不同的显示模式
   ```

3. **QuicklyInspectObjects.cs**（我们没有的）
   ```
   学习要点：
   ✓ 快速检查场景对象
   ✓ OverrideGetTargets 方法
   ✓ 动态更新菜单树
   ```

#### 💡 深化方向 B：序列化系统深化

**重点研究：** `Assets/Plugins/Sirenix/Demos/` 中所有使用 `SerializedMonoBehaviour` 或 `SerializedScriptableObject` 的示例

**学习要点：**
1. **Dictionary 序列化**
   - Unity 原生不支持 Dictionary，Odin 完全支持
   - 使用 `[OdinSerialize]` 标记 Dictionary 字段
   - 在 Inspector 中可以像普通字段一样编辑

2. **接口和抽象类序列化**
   - Unity 原生不支持接口和抽象类序列化
   - Odin 支持多态序列化
   - 可以序列化接口引用和抽象类引用

3. **序列化策略**
   - `SerializationPolicies.Unity`：兼容 Unity 原生规则
   - `SerializationPolicies.Everything`：序列化所有字段
   - `SerializationPolicies.Strict`：严格模式

4. **实际应用场景**
   - 游戏数据配置（如我们的 `ProtrudingPointDictData`）
   - 复杂数据结构存储
   - 需要 Dictionary 的场景

**对比学习：**
- 查看我们的 `12_Serialization/SerializationSample.cs`
- 对比 Unity 原生序列化的限制
- 理解何时使用 Odin 序列化

#### 💡 深化方向 C：完整项目深化

**重点研究：** `Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/`

**对比学习清单：**

| 功能模块 | 我们的实现 | 官方实现 | 学习重点 |
|---------|-----------|---------|---------|
| **主窗口** | `GameDataEditor.cs` | `RPGEditorWindow.cs` | 拖放功能、图标系统 |
| **角色系统** | `Character.cs` | `Character.cs` | 二维数组、装备系统 |
| **物品系统** | `Item.cs` | `Item.cs` | 抽象类设计、派生类 |
| **表格视图** | 我们没有 | `CharacterTable.cs` | TableList 高级用法 |
| **自定义绘制** | 我们没有 | `ItemDrawer.cs` | 完全自定义绘制器 |
| **资源创建** | 简单对话框 | `ScriptableObjectCreator.cs` | 选择器对话框 |

**深化学习步骤：**

1. **第一步：对比主窗口**
   ```
   打开两个文件：
   - 我们的：11_CompleteProject/Scripts/Editor/GameDataEditor.cs
   - 官方的：Sample - RPG Editor/Scripts/Editor/RPGEditorWindow.cs
   
   对比学习：
   ✓ AddDragHandles：我们没有的拖放功能
   ✓ 图标系统：tree.EnumerateTree().AddIcons<T>()
   ✓ 工具栏按钮：ScriptableObjectCreator 的使用
   ```

2. **第二步：学习自定义绘制器**
   ```
   文件：Sample - RPG Editor/Scripts/Editor/ItemDrawer.cs
   
   学习要点：
   ✓ 继承 OdinAttributeDrawer<T>
   ✓ 自定义 OnGUI 绘制
   ✓ 处理拖放事件
   ✓ 绘制预览图
   ```

3. **第三步：学习表格视图**
   ```
   文件：Sample - RPG Editor/Scripts/Editor/CharacterTable.cs
   
   学习要点：
   ✓ TableList 的高级配置
   ✓ 列的自定义显示
   ✓ 排序和过滤
   ```

---

### 🚀 阶段 4：高级专精（按需深入）

**学习目标**：掌握自定义绘制、性能优化、生产级技巧

#### 💡 深化方向 C：自定义绘制器（高级）

**重点研究：** `Assets/Plugins/Sirenix/Demos/Custom Drawers/Scripts/`

**学习清单：**

1. **HealthBarExample.cs**（自定义进度条）
   ```
   学习要点：
   ✓ 继承 OdinAttributeDrawer
   ✓ 自定义 GUI 绘制
   ✓ 处理鼠标交互
   ✓ 实时预览
   ```

2. **MinesweeperExample.cs**（扫雷游戏 - 复杂绘制）
   ```
   学习要点：
   ✓ 完全自定义的 GUI
   ✓ 事件处理
   ✓ 状态管理
   ✓ 复杂交互逻辑
   ```

3. **GenericDrawerExample.cs**（泛型绘制器）
   ```
   学习要点：
   ✓ 泛型类型处理
   ✓ 类型推断
   ✓ 通用绘制器设计
   ```

4. **CustomGroupExample.cs**（自定义分组）
   ```
   学习要点：
   ✓ 自定义 GroupDrawer
   ✓ 布局控制
   ✓ 嵌套绘制
   ```

5. **ReflectionExample.cs**（反射和动态绘制）
   ```
   学习要点：
   ✓ 使用反射动态访问属性
   ✓ 动态创建 GUI
   ✓ 性能优化技巧
   ```

#### 推荐学习顺序：
```
1. 先学：HealthBarExample.cs（简单的自定义绘制）
2. 再学：CustomDrawerExample.cs（标准绘制器模式）
3. 进阶：GenericDrawerExample.cs（泛型处理）
4. 高级：MinesweeperExample.cs（复杂交互）
5. 深入：ReflectionExample.cs（反射和优化）
```

---

## 📊 完整学习路线图

```
入门阶段（OdinSamples 01-10）
    │
    ├─→ 掌握所有基础属性
    ├─→ 理解布局和条件控制
    └─→ 能够美化 Inspector
         │
         ↓
实战阶段（11_CompleteProject）
    │
    ├─→ 理解完整项目架构
    ├─→ 学会数据驱动设计
    └─→ 能够构建编辑器工具
         │
         ↓
深化阶段（官方 Demo 对比学习）
    │
    ├─→ 对比 RPG Editor 学习拖放、图标、高级菜单
    ├─→ 学习 ItemDrawer 等自定义绘制器
    └─→ 研究 CharacterTable 表格视图
         │
         ↓
专精阶段（Custom Drawers 深入）
    │
    ├─→ HealthBarExample（简单绘制器）
    ├─→ GenericDrawerExample（泛型处理）
    ├─→ MinesweeperExample（复杂交互）
    └─→ ReflectionExample（性能优化）
         │
         ↓
实践阶段（应用到自己的项目）
    │
    └─→ 根据需求组合使用所学技巧
```

## 🆚 完整项目 vs 官方 Demo 定位

| 特点 | 本项目 (OdinSamples) | 官方 Demo |
|------|---------------------|----------|
| **定位** | 教学系统 + 入门项目 | 生产级参考 + 高级技巧 |
| **复杂度** | 简化，教学级 | 高级，生产级 |
| **注释** | 中文，详细 | 英文，简略 |
| **学习曲线** | 平缓 ⭐⭐⭐ | 陡峭 ⭐⭐⭐⭐⭐ |
| **适用场景** | 快速入门、系统学习 | 深入研究、生产参考 |
| **代码量** | ~1500 行 | ~2000+ 行 |
| **功能完整度** | 核心功能 80% | 完整功能 100% |
| **最适合人群** | 初学者、快速上手 | 有基础、追求专业 |

### 💡 学习策略

**推荐路线（渐进式）：**
```
OdinSamples 01-10 (基础) 
    → 11_CompleteProject (实战)
        → 官方 Demo 对比学习 (深化)
            → Custom Drawers (专精)
```

**核心原则：**
- ✅ **先建立知识体系**（OdinSamples 系统化教程）
- ✅ **再理解完整架构**（11_CompleteProject 完整项目）
- ✅ **后学习高级技巧**（官方 Demo 生产级代码）
- ✅ **最后应用到项目**（结合实际需求）

## 📚 参考资源

### 官方资源
- [Odin Inspector 官方网站](https://odininspector.com/)
- [Odin Inspector 完整文档](https://odininspector.com/documentation)
- [Odin Inspector 属性速查](https://odininspector.com/attributes)

### 本地资源
- **基础教程**：`Assets/Scripts/OdinSamples/` (01-10 章节)
- **完整项目**：`Assets/Scripts/OdinSamples/11_CompleteProject/`
- **官方 Demo**：`Assets/Plugins/Sirenix/Demos/`
  - RPG Editor：完整的游戏编辑器示例
  - Editor Windows：编辑器窗口示例
  - Custom Drawers：自定义绘制器示例
  - Custom Attribute Processors：属性处理器示例

### 学习顺序建议
```
1️⃣ 本项目 README.md         ← 当前文件，了解整体结构
2️⃣ 11_CompleteProject/README.md  ← 完整项目说明
3️⃣ 官方 Demo 各文件夹的示例    ← 深化学习
```

## 🎯 快速开始检查清单

### ✅ 入门检查（完成 01-10 章节后）
- [ ] 能够使用 20+ 种常用属性美化 Inspector
- [ ] 理解集合类型的各种显示方式
- [ ] 会使用分组和条件控制组织布局
- [ ] 能添加按钮和数据验证

### ✅ 实战检查（完成 11_CompleteProject 后）
- [ ] 能够创建 OdinMenuEditorWindow
- [ ] 理解数据驱动的编辑器架构
- [ ] 会组织和管理 ScriptableObject 资源
- [ ] 能够构建完整的数据编辑工作流

### ✅ 深化检查（学习官方 Demo 后）
- [ ] 理解拖放功能的实现
- [ ] 会创建自定义绘制器
- [ ] 掌握高级菜单树技巧
- [ ] 理解性能优化策略

### ✅ 专精检查（学习 Custom Drawers 后）
- [ ] 能够完全自定义属性绘制
- [ ] 理解 Odin 绘制系统底层原理
- [ ] 掌握泛型绘制器设计
- [ ] 能处理复杂交互逻辑

---

## 💬 学习建议

1. **不要跳过基础**：即使你有经验，也建议快速浏览 01-10 章节
2. **动手实践**：每学一个特性就创建示例对象测试
3. **对比学习**：在理解我们的实现后，再对比官方 Demo
4. **循序渐进**：不要急于学习 Custom Drawers，先打好基础
5. **记录笔记**：遇到好的技巧记录下来，方便后续查阅

## 🐛 常见问题

**Q: 直接学官方 Demo 不行吗？**
A: 可以，但学习曲线陡峭。建议先用本项目建立知识体系，再深入官方 Demo。

**Q: 需要全部学完吗？**
A: 不需要。根据项目需求选择性学习。基础部分（01-08）建议都学，高级部分按需。

**Q: 学完需要多久？**
A: 基础入门 1-2 天，实战应用 3-5 天，深化专精按需。总计 1-2 周可以掌握核心内容。

**Q: 如何知道自己学会了？**
A: 能独立为自己的项目创建一个数据编辑器，并使用 10+ 种 Odin 特性美化 UI。

## 📮 贡献与反馈

如果你发现示例中的问题或有改进建议，欢迎提出！

**Happy Learning! 🚀**
