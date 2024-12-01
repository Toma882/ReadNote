
# 自定义菜单
## 顶部菜单拓展 
      [MeneItem(itemName, isValidataFunction, priority)]
      [MeneItem("itemName 快捷键", isValidataFunction, priority)]   

### 快捷键设置
    Ctrl --> %
    Shift --#
    Alt --> &
    Arrow Keys --> LEFT/RIGHT/UP/DOWN
    F keys --> F1..F12
    Home... --> HOME,...

### example
[MeshCombiner]("../CustomMenu/CustomMenu/Editor/MeshCombiner.cs")

### Seletion
* activeContext -->与SetActionObjectwithContext
* active .. --> 当前Hierarchy 或 Project 窗口中所选中的对象
    
| 类型          | 作用范围              |
|-------------|-------------------|
| GUIDs       | Project           |
| gameObjects | Hierarchy$Project |
| instnaceIDs | Hierarchy$Project |
| objects     | Hierarchy$Project |
| transforms  | Hierachy          |


## 自定义Hierarchy窗口右键菜单
    
    [MenuItem("GameObject/...", false, -1000)]
* 优先级设置中如果大于等于50则不显示在右键菜单中
### example
>CreateStair.cs
 ### 展开和收起游戏层级 
>HierarchyWindowUtility.cs
  ## 自定义Project窗口右键功能菜单
  
     [MenuItem("Assets/...", false, -1000)]

### example
> AssetReference.cs

##自定义组件下拉列表功能菜单

    [MenuItem("CONTEXT/ComponentName/...", false, -1000)]
### Example
> Image2RawImage.cs
> AutoBoxCollider.cs

## ContextMenuAttribute
  contextMenu 可以在组件下拉列表创建新的菜单选项

    [ContextMenu("FuncName", isValidataFunction, priority)]

> ContextMenuExample.cs
### ContextMenuItemAttribute

    [ContextMenuItem("FuncName", isValidataFunction, priority)]
#### example
 > ContextMenuItemExample.cs
 
