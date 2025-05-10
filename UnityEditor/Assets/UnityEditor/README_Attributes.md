# Unity特性(Attributes)详解

## 概述

Unity的特性(Attributes)系统是扩展编辑器和控制组件行为的强大工具。通过特性，开发者可以自定义Inspector界面、控制序列化行为、定义菜单项和调整编译设置等。本文档详细介绍Unity特性系统的架构、常用特性及其应用场景。

## 特性基础

特性(Attributes)是一种向代码添加元数据的机制，通过方括号`[]`标记。Unity使用这些元数据来修改编辑器行为或运行时行为，无需开发者编写额外的功能代码。

```csharp
[SerializeField] // 这是一个特性，使私有字段在Inspector中可见
private int myPrivateVariable;
```

### AttributeUsage

`AttributeUsage` 是 C# 中用于控制特性如何使用的元特性。它可以规定特性可以应用的目标类型、是否允许多次使用以及是否从父类继承。

```csharp
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class MyCustomAttribute : PropertyAttribute
{
    // 特性实现
}
```

### AttributeTargets

`AttributeTargets` 是一个枚举类型，定义了特性可以应用的目标类型。常见的目标类型包括：

- `AttributeTargets.Class` - 应用于类
- `AttributeTargets.Field` - 应用于字段
- `AttributeTargets.Method` - 应用于方法
- `AttributeTargets.Property` - 应用于属性
- `AttributeTargets.All` - 应用于所有可能的元素

可以使用按位或运算符 `|` 组合多个目标，例如：
```csharp
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
```

## UML类图

```
+---------------------------+
|        Attribute          | <-- C#基类
+---------------------------+
| + AttributeTargets        |
| + AllowMultiple           |
+---------------------------+
             ^
             |
+---------------------------+       +----------------------------+
|    PropertyAttribute      | <---- |       CustomEditor         |
+---------------------------+       +----------------------------+
| + order                   |       | + inspectedType            |
+--------------^------------+       | + editorForChildClasses    |
               |                    +----------------------------+
               |                    | + OnInspectorGUI()         |
     +---------+----------+         +----------------------------+
     |                    |
+----------------+  +------------------+
|    RangeAttribute |  | HeaderAttribute |
+----------------+  +------------------+
| + min          |  | + header         |
| + max          |  +------------------+
+----------------+
     
+---------------------------+       +---------------------------+
|      ContextMenuItem      |       |      InitializeOnLoad     |
+---------------------------+       +---------------------------+
| + name                    |       |                           |
| + function                |       |                           |
+---------------------------+       +---------------------------+

+---------------------------+       +---------------------------+
|        MenuItem           |       |    ExecuteInEditMode      |
+---------------------------+       +---------------------------+
| + itemName                |       |                           |
| + validate                |       |                           |
+---------------------------+       +---------------------------+
```

## 思维导图

```
Unity特性系统
├── 编辑器特性
│   ├── 界面相关
│   │   ├── HeaderAttribute - 添加标题
│   │   ├── SpaceAttribute - 添加空间
│   │   ├── TooltipAttribute - 添加提示
│   │   ├── TextAreaAttribute - 多行文本
│   │   ├── ColorUsageAttribute - 颜色选择器
│   │   ├── RangeAttribute - 范围滑动条
│   │   ├── MultilineAttribute - 多行输入
│   │   └── DelayedAttribute - 延迟输入
│   ├── 菜单相关
│   │   ├── MenuItem - 创建菜单项
│   │   ├── ContextMenu - 右键菜单
│   │   ├── ContextMenuItem - 右键菜单项
│   │   └── CreateAssetMenu - 创建资源菜单
│   ├── 编辑器初始化
│   │   ├── InitializeOnLoad - 编辑器加载时执行
│   │   ├── InitializeOnLoadMethod - 编辑器加载时执行方法
│   │   └── DidReloadScripts - 脚本重新加载时执行
│   ├── 设置相关
│   │   ├── SettingsProvider - 创建设置提供者
│   │   ├── UserSetting - 用户设置
│   │   └── SettingsProviderGroup - 设置提供者组
│   ├── Gizmo相关
│   │   ├── GizmoDrawer - Gizmo绘制器
│   │   ├── DrawGizmo - 绘制Gizmo
│   │   └── GizmoIcon - Gizmo图标
│   └── 自定义编辑器
│       ├── CustomEditor - 自定义检视器
│       ├── CanEditMultipleObjects - 可编辑多对象
│       └── CustomPropertyDrawer - 自定义属性绘制器
├── 运行时特性
│   ├── 序列化相关
│   │   ├── SerializeField - 序列化私有字段
│   │   ├── NonSerialized - 不序列化
│   │   ├── SerializeReference - 引用序列化
│   │   └── Serializable - 可序列化类
│   ├── 组件相关
│   │   ├── RequireComponent - 需要组件
│   │   ├── DisallowMultipleComponent - 禁止多组件
│   │   ├── AddComponentMenu - 添加组件菜单
│   │   └── HideInInspector - 在检视器中隐藏
│   ├── 执行相关
│   │   ├── ExecuteInEditMode - 编辑模式执行
│   │   ├── ExecuteAlways - 总是执行
│   │   └── RuntimeInitializeOnLoadMethod - 运行时初始化
│   └── 场景相关
│       ├── SelectionBase - 选择基础
│       └── PreferBinarySerialization - 偏好二进制序列化
└── API相关特性
    ├── 可见性控制
    │   ├── HideInInspector - 隐藏
    │   ├── FormerlySerializedAs - 曾序列化为
    │   └── ShowInInspector - 在检视器中显示
    ├── 弃用与兼容性
    │   ├── Obsolete - 过时
    │   └── HelpURL - 帮助URL
    └── 其他
        ├── AssemblyIsEditorAssembly - 编辑器程序集
        └── GUITarget - GUI目标
```

## 常用特性一览表

### 编辑器界面特性

| 特性名称 | 应用目标 | 描述 | 示例 |
|---------|---------|------|------|
| **HeaderAttribute** | 字段 | 在Inspector中添加标题 | `[Header("Player Settings")]` |
| **TooltipAttribute** | 字段 | 添加鼠标悬停提示 | `[Tooltip("Player's movement speed")]` |
| **RangeAttribute** | 数值字段 | 限制数值范围并显示滑动条 | `[Range(0, 100)]` |
| **SpaceAttribute** | 字段 | 在Inspector中添加空间 | `[Space(10)]` |
| **TextAreaAttribute** | 字符串字段 | 显示多行文本区域 | `[TextArea(3, 10)]` |
| **ColorUsageAttribute** | Color字段 | 自定义颜色选择器 | `[ColorUsage(true, true)]` |
| **MultilineAttribute** | 字符串字段 | 显示多行输入框 | `[Multiline(3)]` |
| **DelayedAttribute** | 值类型字段 | 延迟更新值 | `[Delayed]` |
| **InspectorNameAttribute** | 枚举值/字段 | 自定义Inspector显示名称 | `[InspectorName("Friendly Name")]` |
| **GradientUsageAttribute** | Gradient字段 | 自定义渐变编辑器 | `[GradientUsage(true)]` |

### 序列化特性

| 特性名称 | 应用目标 | 描述 | 示例 |
|---------|---------|------|------|
| **SerializeField** | 私有字段 | 使私有字段可在Inspector中显示 | `[SerializeField]` |
| **NonSerialized** | 公有字段 | 阻止字段序列化 | `[System.NonSerialized]` |
| **SerializeReference** | 引用类型 | 序列化对象引用而非对象本身 | `[SerializeReference]` |
| **Serializable** | 类 | 标记类为可序列化 | `[System.Serializable]` |
| **FormerlySerializedAs** | 字段 | 重命名字段时保持序列化兼容性 | `[FormerlySerializedAs("oldName")]` |
| **PreferBinarySerialization** | ScriptableObject | 使用二进制序列化而非YAML | `[PreferBinarySerialization]` |

### 菜单特性

| 特性名称 | 应用目标 | 描述 | 示例 |
|---------|---------|------|------|
| **MenuItem** | 静态方法 | 创建菜单项 | `[MenuItem("Tools/Reset Transform")]` |
| **ContextMenu** | 方法 | 为组件添加右键菜单项 | `[ContextMenu("Reset")]` |
| **ContextMenuItem** | 字段 | 为字段添加右键菜单项 | `[ContextMenuItem("Reset", "ResetValue")]` |
| **CreateAssetMenu** | 类 | 在"Create"菜单中添加选项 | `[CreateAssetMenu(fileName = "New Data", menuName = "Game/Data")]` |

### 组件相关特性

| 特性名称 | 应用目标 | 描述 | 示例 |
|---------|---------|------|------|
| **RequireComponent** | 类 | 添加依赖组件 | `[RequireComponent(typeof(Rigidbody))]` |
| **DisallowMultipleComponent** | 类 | 禁止多个相同组件 | `[DisallowMultipleComponent]` |
| **ExecuteInEditMode** | 类 | 在编辑模式下执行 | `[ExecuteInEditMode]` |
| **ExecuteAlways** | 类 | 在编辑和运行模式下执行 | `[ExecuteAlways]` |
| **AddComponentMenu** | 类 | 添加到组件菜单 | `[AddComponentMenu("Custom/MyComponent")]` |
| **HideInInspector** | 字段 | 在Inspector中隐藏字段 | `[HideInInspector]` |
| **SelectionBase** | 类 | 设为场景选择基础 | `[SelectionBase]` |
| **DefaultExecutionOrder** | 类 | 设置脚本执行顺序 | `[DefaultExecutionOrder(-100)]` |
| **Icon** | 类 | 为组件设置图标 | `[Icon("Assets/Icons/MyIcon.png")]` |

### 编辑器定制特性

| 特性名称 | 应用目标 | 描述 | 示例 |
|---------|---------|------|------|
| **CustomEditor** | 编辑器类 | 自定义Inspector | `[CustomEditor(typeof(MyComponent))]` |
| **CustomPropertyDrawer** | 属性绘制类 | 自定义属性绘制 | `[CustomPropertyDrawer(typeof(MyClass))]` |
| **CanEditMultipleObjects** | 编辑器类 | 支持多对象编辑 | `[CanEditMultipleObjects]` |
| **InitializeOnLoad** | 类 | 编辑器加载时初始化 | `[InitializeOnLoad]` |
| **InitializeOnLoadMethod** | 静态方法 | 编辑器加载时执行方法 | `[InitializeOnLoadMethod]` |
| **OnOpenAsset** | 静态方法 | 资源打开时执行 | `[OnOpenAsset]` |

### Gizmo相关特性

| 特性名称 | 应用目标 | 描述 | 示例 |
|---------|---------|------|------|
| **DrawGizmo** | 静态方法 | 绘制场景视图中的Gizmo | `[DrawGizmo(GizmoType.Selected)]` |
| **GizmoDrawer** | 类 | 定义Gizmo绘制器 | 自定义实现 |
| **GizmoIcon** | 类 | 为对象添加Gizmo图标 | 自定义实现 |

### 设置相关特性

| 特性名称 | 应用目标 | 描述 | 示例 |
|---------|---------|------|------|
| **SettingsProvider** | 类 | 创建设置提供者 | `[SettingsProvider]` |
| **UserSetting** | 字段 | 标记为用户设置 | `[UserSetting]` |
| **SettingsProviderGroup** | 类 | 创建设置提供者组 | `[SettingsProviderGroup]` |

## 使用案例

### 案例1: 自定义组件检视器

```csharp
// 组件定义
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Player movement speed in units per second")]
    [Range(1, 20)]
    public float moveSpeed = 5f;
    
    [Header("Jump Settings")]
    [Tooltip("Jump force applied to the player")]
    public float jumpForce = 10f;
    
    [Space(10)]
    [Header("Advanced Settings")]
    [SerializeField]
    private bool useGravity = true;
    
    [TextArea(3, 5)]
    public string playerDescription = "Default player character";
    
    [ContextMenu("Reset Values")]
    void ResetValues()
    {
        moveSpeed = 5f;
        jumpForce = 10f;
        useGravity = true;
    }
}

// 自定义检视器
[CustomEditor(typeof(PlayerController))]
[CanEditMultipleObjects]
public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerController player = (PlayerController)target;
        
        EditorGUILayout.LabelField("Player Stats Overview", EditorStyles.boldLabel);
        
        EditorGUI.BeginChangeCheck();
        // 绘制默认检视器
        DrawDefaultInspector();
        
        // 添加自定义按钮
        if(GUILayout.Button("Test Jump"))
        {
            // 执行测试跳跃逻辑
        }
        
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(player, "Changed Player Settings");
            // 处理更改
        }
    }
}
```

### 案例2: 菜单扩展与编辑器工具

```csharp
public static class EditorTools
{
    [MenuItem("Tools/Reset Scene Objects")]
    private static void ResetSceneObjects()
    {
        GameObject[] objects = Selection.gameObjects;
        foreach(GameObject obj in objects)
        {
            Undo.RecordObject(obj.transform, "Reset Transform");
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
        }
    }
    
    [MenuItem("Tools/Reset Scene Objects", true)]
    private static bool ValidateResetSceneObjects()
    {
        return Selection.gameObjects.Length > 0;
    }
    
    [InitializeOnLoadMethod]
    private static void OnProjectLoaded()
    {
        Debug.Log("Project loaded in editor");
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }
    
    private static void OnHierarchyChanged()
    {
        // 响应层级视图变化
    }
}
```

### 案例3: 自定义属性绘制器

```csharp
// 自定义序列化字段
[System.Serializable]
public class FloatRange
{
    public float min = 0;
    public float max = 1;
    
    public FloatRange(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
    
    public float GetRandomValue()
    {
        return Random.Range(min, max);
    }
}

// 自定义属性绘制器
[CustomPropertyDrawer(typeof(FloatRange))]
public class FloatRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
        var minProp = property.FindPropertyRelative("min");
        var maxProp = property.FindPropertyRelative("max");
        
        var minValue = minProp.floatValue;
        var maxValue = maxProp.floatValue;
        
        float rangeMin = 0;
        float rangeMax = 10;
        
        var rangeBounds = new Rect(position);
        rangeBounds.width = position.width;
        
        EditorGUI.MinMaxSlider(rangeBounds, ref minValue, ref maxValue, rangeMin, rangeMax);
        
        var minRect = new Rect(position);
        minRect.width = 50;
        position.xMin += 55;
        
        var maxRect = new Rect(position);
        maxRect.width = 50;
        position.xMin += 55;
        
        minValue = EditorGUI.FloatField(minRect, minValue);
        maxValue = EditorGUI.FloatField(maxRect, maxValue);
        
        minProp.floatValue = minValue;
        maxProp.floatValue = maxValue;
        
        EditorGUI.EndProperty();
    }
}
```

### 案例4: Gizmo绘制与自定义图标

```csharp
// 使用DrawGizmo特性的Gizmo绘制
public class GizmoExample : MonoBehaviour
{
    public float radius = 1.0f;
    public Color gizmoColor = Color.yellow;
}

// Gizmo绘制器 - 与MonoBehaviour分离
public class GizmoExampleDrawer
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmo(GizmoExample example, GizmoType gizmoType)
    {
        Gizmos.color = example.gizmoColor;
        Gizmos.DrawWireSphere(example.transform.position, example.radius);
        
        // 只为选中对象绘制额外指示器
        if (gizmoType == GizmoType.Selected)
        {
            Gizmos.color = new Color(example.gizmoColor.r, example.gizmoColor.g, example.gizmoColor.b, 0.3f);
            Gizmos.DrawSphere(example.transform.position, example.radius);
        }
    }
}
```

### 案例5: 设置提供者

```csharp
// 使用SettingsProvider特性创建设置面板
public class MyCustomSettings
{
    private const string SettingsPath = "Project/MyCustomSettings";
    
    private static SerializedObject settingsObject;
    private static SerializedProperty exampleProperty;
    
    // 创建设置提供者
    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider()
    {
        var provider = new SettingsProvider(SettingsPath, SettingsScope.Project)
        {
            label = "My Custom Settings",
            guiHandler = (searchContext) =>
            {
                // 确保设置对象已初始化
                if (settingsObject == null)
                {
                    var settings = MyCustomSettingsAsset.GetOrCreateSettings();
                    settingsObject = new SerializedObject(settings);
                    exampleProperty = settingsObject.FindProperty("exampleValue");
                }
                
                // 开始绘制设置界面
                EditorGUILayout.PropertyField(exampleProperty, new GUIContent("Example Value"));
                
                // 应用修改
                if (settingsObject.hasModifiedProperties)
                {
                    settingsObject.ApplyModifiedProperties();
                }
            },
            keywords = new HashSet<string>(new[] { "Custom", "Example", "Settings" })
        };
        
        return provider;
    }
}

// 设置资源类
[CreateAssetMenu(fileName = "MyCustomSettings", menuName = "Settings/My Custom Settings")]
public class MyCustomSettingsAsset : ScriptableObject
{
    public float exampleValue = 1.0f;
    
    private static MyCustomSettingsAsset instance;
    
    public static MyCustomSettingsAsset GetOrCreateSettings()
    {
        if (instance != null)
            return instance;
            
        const string settingsPath = "Assets/Settings/MyCustomSettings.asset";
        
        instance = AssetDatabase.LoadAssetAtPath<MyCustomSettingsAsset>(settingsPath);
        if (instance != null)
            return instance;
            
        // 创建新设置资源
        instance = CreateInstance<MyCustomSettingsAsset>();
        
        // 确保目录存在
        if (!AssetDatabase.IsValidFolder("Assets/Settings"))
            AssetDatabase.CreateFolder("Assets", "Settings");
            
        AssetDatabase.CreateAsset(instance, settingsPath);
        AssetDatabase.SaveAssets();
        
        return instance;
    }
}
```

## 自定义特性的创建与使用

### 创建自定义特性

要创建自定义特性，需要继承 `PropertyAttribute` 类，并可选择实现相应的 `PropertyDrawer`：

```csharp
// 定义特性
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class ReadOnlyAttribute : PropertyAttribute
{
    // 无需额外实现
}

// 实现属性绘制器
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 禁用GUI
        GUI.enabled = false;
        
        // 绘制标准字段
        EditorGUI.PropertyField(position, property, label, true);
        
        // 恢复GUI状态
        GUI.enabled = true;
    }
}
```

### 使用自定义特性

```csharp
public class ExampleBehaviour : MonoBehaviour
{
    [ReadOnly]
    public string readOnlyText = "This field cannot be edited";
    
    public string editableText = "This field can be edited";
}
```

## 特性使用最佳实践

1. **组合使用特性**：多个特性可以组合使用，如`[SerializeField]`和`[Range(0,100)]`

2. **顺序与分组**：使用`Header`和`Space`特性合理组织Inspector界面，提高可读性

3. **编写自定义特性**：继承`PropertyAttribute`和`PropertyDrawer`可创建自定义特性

4. **性能考虑**：`ExecuteInEditMode`和`InitializeOnLoad`特性可能影响编辑器性能，应谨慎使用

5. **特性继承**：特性不会被继承，需要为每个类单独添加特性

6. **特性与类型安全**：在创建自定义特性时，考虑添加类型检查，确保特性只应用于兼容的字段类型

7. **避免特性滥用**：过多使用特性可能导致代码可读性下降，应合理使用

8. **文档化特性行为**：为自定义特性提供清晰的文档，说明其行为和使用方法

## 总结

Unity特性系统是扩展编辑器和定制组件行为的强大工具。通过合理使用特性，可以显著提升开发效率、改善编辑器体验并实现复杂的定制功能。特性的组合使用尤其重要，能够创建直观、便于使用的组件检视器和编辑工具。

无论是使用Unity提供的内置特性，还是创建自定义特性，掌握特性系统都能使你的Unity开发流程更加高效和灵活。通过理解每个特性的用途和应用场景，你可以根据项目需求选择最合适的特性组合，创建出既直观又强大的工具和组件。 