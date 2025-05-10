# AssetBundleConfigure 模块

## 概述
AssetBundleConfigure 模块提供了一个可视化的Unity AssetBundle配置工具，允许开发者通过图形界面轻松管理项目中的AssetBundle。该工具提供了直观的双栏界面，左侧显示所有AssetBundle名称列表，右侧显示选中AssetBundle的详细信息和包含的资源。通过拖放操作、右键菜单和搜索功能，开发者可以方便地创建、删除和管理AssetBundle配置，简化了AssetBundle的设置和维护工作，提高了资源打包的效率。

## 核心功能
- **可视化AssetBundle管理**：提供直观的图形界面管理AssetBundle
- **拖放资源分配**：通过拖放操作分配资源到AssetBundle
- **AssetBundle详情查看**：查看AssetBundle包含的资源和依赖关系
- **资源检索功能**：快速搜索和定位AssetBundle和资源
- **AssetBundle名称管理**：创建、删除和移除未使用的AssetBundle名称

## 重要接口和类

### `AssetBundleConfigure` 类
提供AssetBundle配置的主编辑器窗口。

| 方法 | 说明 |
|------|------|
| `Open()` | 打开AssetBundle配置窗口的静态方法 |
| `OnGUI()` | 绘制编辑器窗口的GUI |
| `OnLeftGUI()` | 绘制左侧面板，显示AssetBundle列表 |
| `OnRightGUI()` | 绘制右侧面板，显示选中AssetBundle的详细信息 |
| `Init()` | 初始化或刷新AssetBundle信息 |
| `DragObjects2RectCheck()` | 检查是否有对象被拖放到指定区域 |

### `AssetBundleUtility` 类
提供AssetBundle操作的实用工具方法。

| 方法 | 说明 |
|------|------|
| `GetUniqueAssetBundleNameRecursive(string assetBundleName)` | 递归获取唯一的AssetBundle名称，避免命名冲突 |
| `CreateAssetBundle4Object(Object obj)` | 为指定对象创建AssetBundle |
| `DeleteAssetBundleName(string assetBundleName)` | 删除指定的AssetBundle名称 |

## UML类图

```
+----------------------+       +----------------------+
| AssetBundleConfigure |------>| AssetBundleUtility  |
+----------------------+       +----------------------+
| -splitterPos: float  |       | +GetUniqueAssetBundleNameRecursive() |
| -assetBundleNames    |       | +CreateAssetBundle4Object()         |
| -map: Dictionary     |       | +DeleteAssetBundleName()            |
| -selectedAssetName   |       +----------------------+
| -selectedAssetPath   |
| -searchAssetBundle   |
| -searchAssetPath     |
| +OnGUI()             |
| +OnLeftGUI()         |
| +OnRightGUI()        |
| +Init()              |
| +DragObjects2RectCheck() |
+----------------------+
```

## 界面布局图

```
+-------------------------------------+
|             Toolbar                 |
+---------------+---------------------+
|               |                     |
|               |                     |
|  AssetBundle  |   AssetBundle       |
|    List       |    Details          |
|               |                     |
|               |   +---------------+ |
|               |   |  Asset List   | |
|               |   +---------------+ |
|               |                     |
|               |   +---------------+ |
|               |   |  Asset Info   | |
|               |   +---------------+ |
+---------------+---------------------+
```

## 思维导图

```
AssetBundleConfigure
├── 界面结构
│   ├── 分割面板布局
│   │   ├── 左侧面板 (AssetBundle列表)
│   │   └── 右侧面板 (详细信息)
│   ├── 搜索功能
│   │   ├── AssetBundle搜索
│   │   └── 资源路径搜索
│   └── 交互元素
│       ├── 拖放区域
│       ├── 右键菜单
│       └── 选择高亮
├── AssetBundle管理
│   ├── 创建操作
│   │   ├── 拖放资源创建
│   │   └── 名称唯一性检查
│   ├── 列表显示
│   │   ├── 名称过滤
│   │   └── 选择状态
│   └── 删除操作
│       ├── 单个AssetBundle删除
│       └── 清理未使用名称
├── 资源管理
│   ├── 资源关联
│   │   ├── 查看资源所属AssetBundle
│   │   └── 资源依赖关系
│   ├── 资源操作
│   │   ├── 选择资源
│   │   └── 查看资源信息
│   └── 资源搜索
│       ├── 路径过滤
│       └── 类型过滤
└── 实用工具
    ├── 名称管理
    │   ├── 生成唯一名称
    │   └── 重命名处理
    ├── 资源引用
    │   ├── 获取AssetBundle中的资源
    │   └── 资源路径查询
    └── 编辑器集成
        ├── 菜单项注册
        └── 编辑器窗口管理
```

## 应用场景
1. **资源打包管理**：管理游戏资源的AssetBundle分配和打包策略
2. **版本更新控制**：规划资源分包，便于实现增量更新
3. **依赖关系分析**：查看和分析资源之间的依赖关系
4. **资源优化**：通过合理规划AssetBundle减少冗余和优化加载性能
5. **团队协作**：提供统一的资源管理界面，便于团队协作

## 最佳实践
1. **命名规范**：为AssetBundle制定一致的命名规范，便于管理和识别
2. **合理分组**：根据资源类型、使用场景或加载需求合理分组
3. **依赖管理**：注意资源间的依赖关系，避免循环依赖和不必要的包间依赖
4. **包大小控制**：保持AssetBundle的合理大小，避免过大造成加载延迟
5. **版本控制**：将AssetBundle配置纳入版本控制系统，跟踪变更历史
6. **搜索功能使用**：充分利用搜索功能快速定位资源和AssetBundle

## 界面使用说明
1. **左侧面板操作**：
   - 拖放资源到列表区域创建新的AssetBundle
   - 点击AssetBundle名称选中并查看详情
   - 右键点击AssetBundle名称显示操作菜单
   - 使用搜索框过滤AssetBundle名称

2. **右侧面板操作**：
   - 查看选中AssetBundle的包含资源列表
   - 点击资源查看其详细信息
   - 使用搜索框过滤资源路径
   - 查看选中资源的预览和属性

3. **工具栏操作**：
   - "Refresh"按钮：刷新AssetBundle信息
   - "RemoveUnusedNames"按钮：清理未使用的AssetBundle名称

## 代码示例
```csharp
// 如何通过代码使用AssetBundleUtility创建AssetBundle
using UnityEngine;
using UnityEditor;

public class AssetBundleCreator
{
    [MenuItem("Tools/Create AssetBundle For Selected")]
    public static void CreateAssetBundleForSelected()
    {
        // 获取当前选中的所有对象
        Object[] selectedObjects = Selection.objects;
        if (selectedObjects.Length == 0)
        {
            Debug.Log("未选中任何对象");
            return;
        }
        
        // 为每个对象创建AssetBundle
        int successCount = 0;
        foreach (Object obj in selectedObjects)
        {
            if (AssetBundleUtility.CreateAssetBundle4Object(obj))
            {
                successCount++;
            }
        }
        
        // 输出结果
        Debug.Log($"成功为 {successCount}/{selectedObjects.Length} 个对象创建AssetBundle");
        
        // 刷新Asset数据库
        AssetDatabase.Refresh();
    }
}
```

## 相关资源
- [Unity文档: AssetBundle](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)
- [Unity文档: AssetImporter](https://docs.unity3d.com/ScriptReference/AssetImporter.html)
- [Unity文档: AssetDatabase](https://docs.unity3d.com/ScriptReference/AssetDatabase.html)
- [Unity文档: EditorWindow](https://docs.unity3d.com/ScriptReference/EditorWindow.html)
