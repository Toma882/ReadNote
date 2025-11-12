# Unity Utility Classes 工具类参考

## 概述

本文档提供了Unity中所有Utility工具类的完整参考，包括各种实用工具类、辅助方法和静态工具函数。

## 星级评价说明

- ⭐⭐⭐⭐⭐ **极高重要性** - 核心工具类，几乎每个Unity项目都会使用
- ⭐⭐⭐⭐ **高重要性** - 常用工具类，大部分项目会用到
- ⭐⭐⭐ **中等重要性** - 特定场景下很有用的工具类
- ⭐⭐ **低重要性** - 专业用途或特殊场景的工具类
- ⭐ **极低重要性** - 很少使用的工具类

## 主要Utility类

### 核心Utility类

#### PrefabUtility ⭐⭐⭐⭐⭐
- [x] **UnityEditor.PrefabUtility** [预制体工具] (https://docs.unity3d.com/ScriptReference/PrefabUtility.html)
    -[x] **PrefabUtility.SaveAsPrefabAsset** [保存为预制体资源] (https://docs.unity3d.com/ScriptReference/PrefabUtility.SaveAsPrefabAsset.html)
    -[x] **PrefabUtility.InstantiatePrefab** [实例化预制体] (https://docs.unity3d.com/ScriptReference/PrefabUtility.InstantiatePrefab.html)
    -[x] **PrefabUtility.ApplyPrefabInstance** [应用预制体实例] (https://docs.unity3d.com/ScriptReference/PrefabUtility.ApplyPrefabInstance.html)
    -[x] **PrefabUtility.RevertPrefabInstance** [还原预制体实例] (https://docs.unity3d.com/ScriptReference/PrefabUtility.RevertPrefabInstance.html)
    -[x] **PrefabUtility.IsPartOfPrefabInstance** [是否为预制体实例] (https://docs.unity3d.com/ScriptReference/PrefabUtility.IsPartOfPrefabInstance.html)

#### EditorUtility ⭐⭐⭐⭐⭐
- [x] **UnityEditor.EditorUtility** [编辑器工具] (https://docs.unity3d.com/ScriptReference/EditorUtility.html)
    -[x] **EditorUtility.DisplayDialog** [显示对话框] (https://docs.unity3d.com/ScriptReference/EditorUtility.DisplayDialog.html)
    -[x] **EditorUtility.SaveFilePanel** [保存文件面板] (https://docs.unity3d.com/ScriptReference/EditorUtility.SaveFilePanel.html)
    -[x] **EditorUtility.OpenFilePanel** [打开文件面板] (https://docs.unity3d.com/ScriptReference/EditorUtility.OpenFilePanel.html)
    -[x] **EditorUtility.SetDirty** [标记为脏] (https://docs.unity3d.com/ScriptReference/EditorUtility.SetDirty.html)
    -[x] **EditorUtility.DisplayProgressBar** [显示进度条] (https://docs.unity3d.com/ScriptReference/EditorUtility.DisplayProgressBar.html)

#### EditorGUIUtility ⭐⭐⭐⭐
- [x] **UnityEditor.EditorGUIUtility** [编辑器GUI工具] (https://docs.unity3d.com/ScriptReference/EditorGUIUtility.html)
    -[x] **EditorGUIUtility.ObjectContent** [对象内容] (https://docs.unity3d.com/ScriptReference/EditorGUIUtility.ObjectContent.html)
    -[x] **EditorGUIUtility.SetIconSize** [设置图标大小] (https://docs.unity3d.com/ScriptReference/EditorGUIUtility.SetIconSize.html)
    -[x] **EditorGUIUtility.LoadRequired** [加载必需资源] (https://docs.unity3d.com/ScriptReference/EditorGUIUtility.LoadRequired.html)
    -[x] **EditorGUIUtility.systemCopyBuffer** [系统复制缓冲区] (https://docs.unity3d.com/ScriptReference/EditorGUIUtility-systemCopyBuffer.html)

#### GUIUtility ⭐⭐⭐⭐
- [x] **UnityEngine.GUIUtility** [GUI工具] (https://docs.unity3d.com/ScriptReference/GUIUtility.html)
    -[x] **GUIUtility.ScreenToGUIPoint** [屏幕坐标转GUI坐标] (https://docs.unity3d.com/ScriptReference/GUIUtility.ScreenToGUIPoint.html)
    -[x] **GUIUtility.GUIToScreenPoint** [GUI坐标转屏幕坐标] (https://docs.unity3d.com/ScriptReference/GUIUtility.GUIToScreenPoint.html)
    -[x] **GUIUtility.GetControlID** [获取控件ID] (https://docs.unity3d.com/ScriptReference/GUIUtility.GetControlID.html)
    -[x] **GUIUtility.keyboardControl** [键盘控制] (https://docs.unity3d.com/ScriptReference/GUIUtility-keyboardControl.html)

#### JsonUtility ⭐⭐⭐⭐⭐
- [x] **UnityEngine.JsonUtility** [JSON工具] (https://docs.unity3d.com/ScriptReference/JsonUtility.html)
    -[x] **JsonUtility.ToJson** [转换为JSON] (https://docs.unity3d.com/ScriptReference/JsonUtility.ToJson.html)
    -[x] **JsonUtility.FromJson** [从JSON转换] (https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html)
    -[x] **JsonUtility.FromJsonOverwrite** [从JSON覆盖] (https://docs.unity3d.com/ScriptReference/JsonUtility.FromJsonOverwrite.html)

#### EditorJsonUtility ⭐⭐⭐
- [x] **UnityEditor.EditorJsonUtility** [编辑器JSON工具] (https://docs.unity3d.com/ScriptReference/EditorJsonUtility.html)
    -[x] **EditorJsonUtility.ToJson** [转换为JSON] (https://docs.unity3d.com/ScriptReference/EditorJsonUtility.ToJson.html)
    -[x] **EditorJsonUtility.FromJsonOverwrite** [从JSON覆盖] (https://docs.unity3d.com/ScriptReference/EditorJsonUtility.FromJsonOverwrite.html)

### 几何和数学工具

#### GeometryUtility ⭐⭐⭐⭐
- [x] **UnityEngine.GeometryUtility** [几何工具] (https://docs.unity3d.com/ScriptReference/GeometryUtility.html)
    -[x] **GeometryUtility.CalculateFrustumPlanes** [计算视锥平面] (https://docs.unity3d.com/ScriptReference/GeometryUtility.CalculateFrustumPlanes.html)
    -[x] **GeometryUtility.TestPlanesAABB** [测试平面包围盒] (https://docs.unity3d.com/ScriptReference/GeometryUtility.TestPlanesAABB.html)

#### RectTransformUtility ⭐⭐⭐⭐⭐
- [x] **UnityEngine.RectTransformUtility** [矩形变换工具] (https://docs.unity3d.com/ScriptReference/RectTransformUtility.html)
    -[x] **RectTransformUtility.ScreenPointToLocalPointInRectangle** [屏幕点转本地点] (https://docs.unity3d.com/ScriptReference/RectTransformUtility.ScreenPointToLocalPointInRectangle.html)
    -[x] **RectTransformUtility.LocalPointToScreenPoint** [本地点转屏幕点] (https://docs.unity3d.com/ScriptReference/RectTransformUtility.LocalPointToScreenPoint.html)
    -[x] **RectTransformUtility.FlipLayoutOnAxis** [翻转布局轴] (https://docs.unity3d.com/ScriptReference/RectTransformUtility.FlipLayoutOnAxis.html)

#### TransformUtils ⭐⭐
- [x] **UnityEngine.TransformUtils** [变换工具] (https://docs.unity3d.com/ScriptReference/TransformUtils.html)
    -[x] **TransformUtils.GetInspectorShowMode** [获取检查器显示模式] (https://docs.unity3d.com/ScriptReference/TransformUtils.GetInspectorShowMode.html)
    -[x] **TransformUtils.SetInspectorShowMode** [设置检查器显示模式] (https://docs.unity3d.com/ScriptReference/TransformUtils.SetInspectorShowMode.html)

### 动画工具

#### AnimationUtility ⭐⭐⭐⭐
- [x] **UnityEditor.AnimationUtility** [动画工具] (https://docs.unity3d.com/ScriptReference/AnimationUtility.html)
    -[x] **AnimationUtility.GetAnimationClips** [获取动画剪辑] (https://docs.unity3d.com/ScriptReference/AnimationUtility.GetAnimationClips.html)
    -[x] **AnimationUtility.SetAnimationClips** [设置动画剪辑] (https://docs.unity3d.com/ScriptReference/AnimationUtility.SetAnimationClips.html)
    -[x] **AnimationUtility.GetAnimationEvents** [获取动画事件] (https://docs.unity3d.com/ScriptReference/AnimationUtility.GetAnimationEvents.html)
    -[x] **AnimationUtility.SetAnimationEvents** [设置动画事件] (https://docs.unity3d.com/ScriptReference/AnimationUtility.SetAnimationEvents.html)

#### AnimationSceneHandleUtility ⭐⭐
- [x] **UnityEditor.AnimationSceneHandleUtility** [动画场景句柄工具] (https://docs.unity3d.com/ScriptReference/AnimationSceneHandleUtility.html)
    -[x] **AnimationSceneHandleUtility.GetSceneHandle** [获取场景句柄] (https://docs.unity3d.com/ScriptReference/AnimationSceneHandleUtility.GetSceneHandle.html)

#### AnimationStreamHandleUtility ⭐⭐
- [x] **UnityEditor.AnimationStreamHandleUtility** [动画流句柄工具] (https://docs.unity3d.com/ScriptReference/AnimationStreamHandleUtility.html)
    -[x] **AnimationStreamHandleUtility.CreateHandle** [创建句柄] (https://docs.unity3d.com/ScriptReference/AnimationStreamHandleUtility.CreateHandle.html)

### 地形工具

#### TerrainUtility ⭐⭐⭐
- [x] **UnityEditor.TerrainUtility** [地形工具] (https://docs.unity3d.com/ScriptReference/TerrainUtility.html)
    -[x] **TerrainUtility.AutoConnect** [自动连接] (https://docs.unity3d.com/ScriptReference/TerrainUtility.AutoConnect.html)
    -[x] **TerrainUtility.ValidTerrainsCheck** [有效地形检查] (https://docs.unity3d.com/ScriptReference/TerrainUtility.ValidTerrainsCheck.html)

#### TerrainPaintUtility ⭐⭐⭐
- [x] **UnityEditor.TerrainPaintUtility** [地形绘制工具] (https://docs.unity3d.com/ScriptReference/TerrainPaintUtility.html)
    -[x] **TerrainPaintUtility.BeginPaintTexture** [开始绘制纹理] (https://docs.unity3d.com/ScriptReference/TerrainPaintUtility.BeginPaintTexture.html)
    -[x] **TerrainPaintUtility.EndPaintTexture** [结束绘制纹理] (https://docs.unity3d.com/ScriptReference/TerrainPaintUtility.EndPaintTexture.html)

#### TerrainInspectorUtility ⭐⭐
- [x] **UnityEditor.TerrainInspectorUtility** [地形检查器工具] (https://docs.unity3d.com/ScriptReference/TerrainInspectorUtility.html)
    -[x] **TerrainInspectorUtility.ShowDefaultTerrainInspector** [显示默认地形检查器] (https://docs.unity3d.com/ScriptReference/TerrainInspectorUtility.ShowDefaultTerrainInspector.html)

### GUI和布局工具

#### GUILayoutUtility ⭐⭐⭐⭐
- [x] **UnityEngine.GUILayoutUtility** [GUI布局工具] (https://docs.unity3d.com/ScriptReference/GUILayoutUtility.html)
    -[x] **GUILayoutUtility.GetRect** [获取矩形] (https://docs.unity3d.com/ScriptReference/GUILayoutUtility.GetRect.html)
    -[x] **GUILayoutUtility.GetLastRect** [获取最后矩形] (https://docs.unity3d.com/ScriptReference/GUILayoutUtility.GetLastRect.html)

#### EditorToolbarUtility ⭐⭐
- [x] **UnityEditor.EditorToolbarUtility** [编辑器工具栏工具] (https://docs.unity3d.com/ScriptReference/EditorToolbarUtility.html)
    -[x] **EditorToolbarUtility.GetToolbarRect** [获取工具栏矩形] (https://docs.unity3d.com/ScriptReference/EditorToolbarUtility.GetToolbarRect.html)

### 渲染和着色器工具

#### ShaderUtil ⭐⭐⭐⭐
- [x] **UnityEditor.ShaderUtil** [着色器工具] (https://docs.unity3d.com/ScriptReference/ShaderUtil.html)
    -[x] **ShaderUtil.GetPropertyCount** [获取属性数量] (https://docs.unity3d.com/ScriptReference/ShaderUtil.GetPropertyCount.html)
    -[x] **ShaderUtil.GetPropertyName** [获取属性名称] (https://docs.unity3d.com/ScriptReference/ShaderUtil.GetPropertyName.html)
    -[x] **ShaderUtil.GetPropertyType** [获取属性类型] (https://docs.unity3d.com/ScriptReference/ShaderUtil.GetPropertyType.html)

#### RenderPipelineEditorUtility ⭐⭐⭐
- [x] **UnityEditor.RenderPipelineEditorUtility** [渲染管线编辑器工具] (https://docs.unity3d.com/ScriptReference/RenderPipelineEditorUtility.html)
    -[x] **RenderPipelineEditorUtility.GetPipelineAsset** [获取管线资源] (https://docs.unity3d.com/ScriptReference/RenderPipelineEditorUtility.GetPipelineAsset.html)

#### LightmapperUtils ⭐⭐⭐
- [x] **UnityEditor.LightmapperUtils** [光照贴图工具] (https://docs.unity3d.com/ScriptReference/LightmapperUtils.html)
    -[x] **LightmapperUtils.Extract** [提取] (https://docs.unity3d.com/ScriptReference/LightmapperUtils.Extract.html)
    -[x] **LightmapperUtils.Store** [存储] (https://docs.unity3d.com/ScriptReference/LightmapperUtils.Store.html)

### 性能分析工具

#### ProfilerEditorUtility ⭐⭐⭐⭐
- [x] **UnityEditor.ProfilerEditorUtility** [性能分析器编辑器工具] (https://docs.unity3d.com/ScriptReference/ProfilerEditorUtility.html)
    -[x] **ProfilerEditorUtility.GetConnectionState** [获取连接状态] (https://docs.unity3d.com/ScriptReference/ProfilerEditorUtility.GetConnectionState.html)

#### ProfilerUnsafeUtility ⭐⭐⭐
- [x] **UnityEditor.ProfilerUnsafeUtility** [性能分析器不安全工具] (https://docs.unity3d.com/ScriptReference/ProfilerUnsafeUtility.html)
    -[x] **ProfilerUnsafeUtility.BeginSample** [开始采样] (https://docs.unity3d.com/ScriptReference/ProfilerUnsafeUtility.BeginSample.html)
    -[x] **ProfilerUnsafeUtility.EndSample** [结束采样] (https://docs.unity3d.com/ScriptReference/ProfilerUnsafeUtility.EndSample.html)

### 内存和序列化工具

#### UnsafeUtility ⭐⭐⭐
- [x] **Unity.Collections.LowLevel.Unsafe.UnsafeUtility** [不安全工具] (https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.UnsafeUtility.html)
    -[x] **UnsafeUtility.Malloc** [分配内存] (https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc.html)
    -[x] **UnsafeUtility.Free** [释放内存] (https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free.html)
    -[x] **UnsafeUtility.MemCpy** [内存复制] (https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy.html)

#### SerializationUtility ⭐⭐⭐
- [x] **UnityEditor.SerializationUtility** [序列化工具] (https://docs.unity3d.com/ScriptReference/SerializationUtility.html)
    -[x] **SerializationUtility.SerializeValue** [序列化值] (https://docs.unity3d.com/ScriptReference/SerializationUtility.SerializeValue.html)
    -[x] **SerializationUtility.DeserializeValue** [反序列化值] (https://docs.unity3d.com/ScriptReference/SerializationUtility.DeserializeValue.html)

### 原生数组工具

#### NativeArrayUnsafeUtility ⭐⭐
- [x] **Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility** [原生数组不安全工具] (https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.html)
    -[x] **NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray** [转换现有数据为原生数组] (https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray.html)

#### NativeSliceUnsafeUtility ⭐⭐
- [x] **Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility** [原生切片不安全工具] (https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.html)
    -[x] **NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice** [转换现有数据为原生切片] (https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice.html)

### 其他工具

#### GameObjectUtility ⭐⭐⭐
- [x] **UnityEditor.GameObjectUtility** [游戏对象工具] (https://docs.unity3d.com/ScriptReference/GameObjectUtility.html)
    -[x] **GameObjectUtility.GetNavMeshAreaFromName** [从名称获取导航网格区域] (https://docs.unity3d.com/ScriptReference/GameObjectUtility.GetNavMeshAreaFromName.html)
    -[x] **GameObjectUtility.GetNavMeshAreaNames** [获取导航网格区域名称] (https://docs.unity3d.com/ScriptReference/GameObjectUtility.GetNavMeshAreaNames.html)

#### LODUtility ⭐⭐⭐
- [x] **UnityEditor.LODUtility** [LOD工具] (https://docs.unity3d.com/ScriptReference/LODUtility.html)
    -[x] **LODUtility.CalculateLODGroupBoundingBox** [计算LOD组包围盒] (https://docs.unity3d.com/ScriptReference/LODUtility.CalculateLODGroupBoundingBox.html)

#### SpriteAtlasUtility ⭐⭐⭐
- [x] **UnityEditor.U2D.SpriteAtlasUtility** [精灵图集工具] (https://docs.unity3d.com/ScriptReference/UnityEditor.U2D.SpriteAtlasUtility.html)
    -[x] **SpriteAtlasUtility.PackAtlases** [打包图集] (https://docs.unity3d.com/ScriptReference/UnityEditor.U2D.SpriteAtlasUtility.PackAtlases.html)

#### StageUtility ⭐⭐⭐
- [x] **UnityEditor.Experimental.SceneManagement.StageUtility** [场景工具] (https://docs.unity3d.com/ScriptReference/UnityEditor.Experimental.SceneManagement.StageUtility.html)
    -[x] **StageUtility.GetMainStage** [获取主场景] (https://docs.unity3d.com/ScriptReference/UnityEditor.Experimental.SceneManagement.StageUtility.GetMainStage.html)

#### PrefabStageUtility ⭐⭐⭐
- [x] **UnityEditor.Experimental.SceneManagement.PrefabStageUtility** [预制体场景工具] (https://docs.unity3d.com/ScriptReference/UnityEditor.Experimental.SceneManagement.PrefabStageUtility.html)
    -[x] **PrefabStageUtility.GetCurrentPrefabStage** [获取当前预制体场景] (https://docs.unity3d.com/ScriptReference/UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage.html)

#### LicensingUtility ⭐⭐
- [x] **UnityEditor.LicensingUtility** [许可工具] (https://docs.unity3d.com/ScriptReference/LicensingUtility.html)
    -[x] **LicensingUtility.HasAValidLicense** [是否有有效许可] (https://docs.unity3d.com/ScriptReference/LicensingUtility.HasAValidLicense.html)

#### BuildUtilities ⭐⭐⭐
- [x] **UnityEditor.BuildUtilities** [构建工具] (https://docs.unity3d.com/ScriptReference/BuildUtilities.html)
    -[x] **BuildUtilities.GetBuildTargetName** [获取构建目标名称] (https://docs.unity3d.com/ScriptReference/BuildUtilities.GetBuildTargetName.html)

## 示例文件生成状态

### 已生成的示例文件 ✅
- **PrefabUtilityExample.cs** - 预制体工具示例
- **JsonUtilityExample.cs** - JSON工具示例  
- **GeometryUtilityExample.cs** - 几何工具示例
- **AnimationUtilityExample.cs** - 动画工具示例
- **ShaderUtilityExample.cs** - 着色器工具示例
- **EditorUtilityExample.cs** - 编辑器工具示例
- **EditorGUIUtilityExample.cs** - 编辑器GUI工具示例
- **GUIUtilityExample.cs** - GUI工具示例
- **EditorJsonUtilityExample.cs** - 编辑器JSON工具示例
- **RectTransformUtilityExample.cs** - 矩形变换工具示例
- **TransformUtilsExample.cs** - 变换工具示例
- **AnimationSceneHandleUtilityExample.cs** - 动画场景句柄工具示例
- **AnimationStreamHandleUtilityExample.cs** - 动画流句柄工具示例
- **TerrainUtilityExample.cs** - 地形工具示例
- **TerrainPaintUtilityExample.cs** - 地形绘制工具示例
- **TerrainInspectorUtilityExample.cs** - 地形检查器工具示例
- **GUILayoutUtilityExample.cs** - GUI布局工具示例
- **EditorToolbarUtilityExample.cs** - 编辑器工具栏工具示例
- **GameObjectUtilityExample.cs** - 游戏对象工具示例
- **LODUtilityExample.cs** - LOD工具示例
- **SerializationUtilityExample.cs** - 序列化工具示例
- **UnsafeUtilityExample.cs** - 不安全工具示例
- **NativeArrayUnsafeUtilityExample.cs** - 原生数组不安全工具示例
- **NativeSliceUnsafeUtilityExample.cs** - 原生切片不安全工具示例
- **SpriteAtlasUtilityExample.cs** - 精灵图集工具示例
- **StageUtilityExample.cs** - 场景工具示例
- **PrefabStageUtilityExample.cs** - 预制体场景工具示例
- **LicensingUtilityExample.cs** - 许可工具示例
- **BuildUtilitiesExample.cs** - 构建工具示例
- **RenderPipelineEditorUtilityExample.cs** - 渲染管线编辑器工具示例
- **LightmapperUtilsExample.cs** - 光照贴图工具示例
- **ProfilerEditorUtilityExample.cs** - 性能分析器编辑器工具示例
- **ProfilerUnsafeUtilityExample.cs** - 性能分析器不安全工具示例
- **UtilityExample.cs** - 综合工具示例

### 待生成的示例文件 ⏳
*所有示例文件已完成生成！*

## 使用示例

### 基础工具使用
```csharp
// PrefabUtility示例
GameObject prefab = PrefabUtility.SaveAsPrefabAsset(selectedObject, "Assets/MyPrefab.prefab");
GameObject instance = PrefabUtility.InstantiatePrefab(prefab);

// EditorUtility示例
bool result = EditorUtility.DisplayDialog("确认", "是否继续？", "是", "否");
string path = EditorUtility.SaveFilePanel("保存文件", "", "MyFile", "txt");

// JsonUtility示例
string json = JsonUtility.ToJson(myObject);
MyClass obj = JsonUtility.FromJson<MyClass>(json);
```

### 几何工具使用
```csharp
// GeometryUtility示例
Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
bool isVisible = GeometryUtility.TestPlanesAABB(planes, renderer.bounds);

// RectTransformUtility示例
Vector2 localPoint;
RectTransformUtility.ScreenPointToLocalPointInRectangle(
    rectTransform, Input.mousePosition, Camera.main, out localPoint);
```

### 动画工具使用
```csharp
// AnimationUtility示例
AnimationClip[] clips = AnimationUtility.GetAnimationClips(gameObject);
AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
```

## 最佳实践

1. **工具类选择** - 根据具体需求选择合适的工具类
2. **性能考虑** - 避免在Update中频繁调用工具方法
3. **错误处理** - 始终检查工具方法的返回值
4. **资源管理** - 及时释放工具创建的资源

## 相关资源

- [Unity Scripting API](https://docs.unity3d.com/ScriptReference/)
- [Unity Editor API](https://docs.unity3d.com/ScriptReference/UnityEditor.html)
- [Unity Collections API](https://docs.unity3d.com/ScriptReference/Unity.Collections.html)
