# Unity特性(Attributes)详解

## 目录

- [概述](#概述)
- [特性基础](#特性基础)
  - [AttributeUsage](#attributeusage)
  - [AttributeTargets](#attributetargets)
- [UML类图](#uml类图)
- [思维导图](#思维导图)
- [常用特性一览表](#常用特性一览表)
  - [编辑器界面特性](#编辑器界面特性)
    - [HeaderAttribute - 添加标题分组](#headerattribute---添加标题分组)
    - [TooltipAttribute - 添加悬停提示](#tooltipattribute---添加悬停提示)
    - [RangeAttribute - 范围滑动条](#rangeattribute---范围滑动条)
    - [SpaceAttribute - 添加间距](#spaceattribute---添加间距)
    - [TextAreaAttribute - 多行文本区域](#textareaattribute---多行文本区域)
    - [ColorUsageAttribute - 颜色选择器定制](#colorusageattribute---颜色选择器定制)
    - [MultilineAttribute - 多行输入框](#multilineattribute---多行输入框)
    - [DelayedAttribute - 延迟更新](#delayedattribute---延迟更新)
    - [InspectorNameAttribute - 自定义显示名称](#inspectornameattribute---自定义显示名称)
    - [GradientUsageAttribute - 渐变编辑器定制](#gradientusageattribute---渐变编辑器定制)
  - [序列化特性](#序列化特性)
    - [SerializeField - 序列化私有字段](#serializefield---序列化私有字段)
    - [NonSerialized - 阻止序列化](#nonserialized---阻止序列化)
    - [SerializeReference - 引用序列化](#serializereference---引用序列化)
    - [Serializable - 可序列化类](#serializable---可序列化类)
    - [FormerlySerializedAs - 保持序列化兼容性](#formerlyserializedas---保持序列化兼容性)
    - [PreferBinarySerialization - 二进制序列化](#preferbinaryserialization---二进制序列化)
  - [菜单特性](#菜单特性)
    - [MenuItem - 创建编辑器菜单项](#menuitem---创建编辑器菜单项)
    - [ContextMenu - 组件右键菜单](#contextmenu---组件右键菜单)
    - [ContextMenuItem - 字段右键菜单](#contextmenuitem---字段右键菜单)
    - [CreateAssetMenu - 创建资源菜单](#createassetmenu---创建资源菜单)
  - [组件相关特性](#组件相关特性)
    - [RequireComponent - 依赖组件](#requirecomponent---依赖组件)
    - [DisallowMultipleComponent - 禁止多组件](#disallowmultiplecomponent---禁止多组件)
    - [ExecuteInEditMode - 编辑模式执行](#executeineditmode---编辑模式执行)
    - [ExecuteAlways - 总是执行](#executealways---总是执行)
    - [AddComponentMenu - 组件菜单](#addcomponentmenu---组件菜单)
    - [HideInInspector - 隐藏字段](#hideininspector---隐藏字段)
    - [SelectionBase - 选择基础](#selectionbase---选择基础)
    - [DefaultExecutionOrder - 执行顺序](#defaultexecutionorder---执行顺序)
    - [Icon - 组件图标](#icon---组件图标)
  - [编辑器定制特性](#编辑器定制特性)
    - [CustomEditor - 自定义检视器](#customeditor---自定义检视器)
    - [CustomPropertyDrawer - 自定义属性绘制器](#custompropertydrawer---自定义属性绘制器)
    - [CanEditMultipleObjects - 多对象编辑](#caneditmultipleobjects---多对象编辑)
    - [InitializeOnLoad - 编辑器加载时初始化](#initializeonload---编辑器加载时初始化)
    - [InitializeOnLoadMethod - 编辑器加载时执行方法](#initializeonloadmethod---编辑器加载时执行方法)
    - [OnOpenAsset - 资源打开时执行](#onopenasset---资源打开时执行)
  - [Gizmo相关特性](#gizmo相关特性)
    - [DrawGizmo - 绘制场景Gizmo](#drawgizmo---绘制场景gizmo)
    - [自定义Gizmo图标](#自定义gizmo图标)
    - [高级Gizmo绘制示例](#高级gizmo绘制示例)
  - [设置相关特性](#设置相关特性)
    - [SettingsProvider - 创建设置提供者](#settingsprovider---创建设置提供者)
    - [UserSetting - 用户设置](#usersetting---用户设置)
    - [SettingsProviderGroup - 设置提供者组](#settingsprovidergroup---设置提供者组)
- [使用案例](#使用案例)
  - [案例1: 自定义组件检视器](#案例1-自定义组件检视器)
  - [案例2: 菜单扩展与编辑器工具](#案例2-菜单扩展与编辑器工具)
  - [案例3: 自定义属性绘制器](#案例3-自定义属性绘制器)
  - [案例4: Gizmo绘制与自定义图标](#案例4-gizmo绘制与自定义图标)
  - [案例5: 设置提供者](#案例5-设置提供者)
- [自定义特性的创建与使用](#自定义特性的创建与使用)
  - [创建自定义特性](#创建自定义特性)
  - [使用自定义特性](#使用自定义特性)
- [运行时特性详解](#运行时特性详解)
  - [RuntimeInitializeOnLoadMethod - 运行时初始化](#runtimeinitializeonloadmethod---运行时初始化)
  - [HelpURL - 帮助文档链接](#helpurl---帮助文档链接)
  - [Obsolete - 标记过时API](#obsolete---标记过时api)
- [特性组合使用示例](#特性组合使用示例)
  - [完整的组件示例](#完整的组件示例)
- [特性使用最佳实践](#特性使用最佳实践)
- [总结](#总结)

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

#### HeaderAttribute - 添加标题分组
**作用**：在Inspector中为字段组添加标题，用于组织和分类相关字段。

```csharp
public class PlayerController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    
    [Header("战斗设置")]
    public int health = 100;
    public float attackDamage = 25f;
}
```

#### TooltipAttribute - 添加悬停提示
**作用**：为字段添加鼠标悬停时显示的提示信息，帮助理解字段用途。

```csharp
public class WeaponSystem : MonoBehaviour
{
    [Tooltip("每秒发射的子弹数量")]
    public float fireRate = 10f;
    
    [Tooltip("子弹飞行速度，单位：米/秒")]
    public float bulletSpeed = 50f;
}
```

#### RangeAttribute - 范围滑动条
**作用**：限制数值字段的取值范围，并在Inspector中显示为滑动条。

```csharp
public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("主音量")]
    public float masterVolume = 0.8f;
    
    [Range(1, 100)]
    [Tooltip("同时播放的最大音效数")]
    public int maxSounds = 32;
}
```

#### SpaceAttribute - 添加间距
**作用**：在Inspector中的字段之间添加空白间距，改善视觉布局。

```csharp
public class GameSettings : MonoBehaviour
{
    public string playerName = "Player";
    public int level = 1;
    
    [Space(20)]  // 添加20像素间距
    public bool enableDebug = false;
    public bool showFPS = true;
}
```

#### TextAreaAttribute - 多行文本区域
**作用**：将字符串字段显示为可调整大小的多行文本区域。

```csharp
public class DialogueSystem : MonoBehaviour
{
    [TextArea(3, 10)]  // 最小3行，最大10行
    [Tooltip("角色对话内容")]
    public string dialogueText = "在这里输入对话内容...";
    
    [TextArea(2, 5)]
    public string characterDescription;
}
```

#### ColorUsageAttribute - 颜色选择器定制
**作用**：自定义颜色选择器的行为，支持HDR颜色和透明度。

```csharp
public class LightingController : MonoBehaviour
{
    [ColorUsage(false)]  // 禁用透明度
    public Color ambientColor = Color.white;
    
    [ColorUsage(true, true)]  // 启用透明度和HDR
    public Color emissionColor = Color.red;
}
```

#### MultilineAttribute - 多行输入框
**作用**：将字符串字段显示为固定行数的多行输入框。

```csharp
public class QuestSystem : MonoBehaviour
{
    [Multiline(4)]  // 显示4行输入框
    public string questDescription = "任务描述...";
    
    [Multiline(2)]
    public string questHint = "提示信息...";
}
```

#### DelayedAttribute - 延迟更新
**作用**：延迟字段值的更新，直到用户按下Enter键或字段失去焦点。

```csharp
public class NetworkSettings : MonoBehaviour
{
    [Delayed]
    [Tooltip("服务器IP地址")]
    public string serverIP = "127.0.0.1";
    
    [Delayed]
    public int serverPort = 7777;
}
```

#### InspectorNameAttribute - 自定义显示名称
**作用**：为枚举值或字段在Inspector中显示自定义名称。

```csharp
public enum WeaponType
{
    [InspectorName("手枪")]
    Pistol,
    [InspectorName("步枪")]
    Rifle,
    [InspectorName("霰弹枪")]
    Shotgun
}

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType = WeaponType.Pistol;
}
```

#### GradientUsageAttribute - 渐变编辑器定制
**作用**：自定义渐变编辑器的行为，控制是否支持HDR颜色。

```csharp
public class ParticleController : MonoBehaviour
{
    [GradientUsage(false)]  // 标准渐变
    public Gradient colorOverLifetime;
    
    [GradientUsage(true)]   // HDR渐变
    public Gradient emissionGradient;
}

### 序列化特性

#### SerializeField - 序列化私有字段
**作用**：使私有字段在Inspector中可见和可编辑，同时保持字段的封装性。

```csharp
public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private int health = 100;  // 私有字段但在Inspector中可见
    
    [SerializeField]
    private string playerName = "Player";
    
    // 提供公共访问方法
    public int GetHealth() => health;
    public void SetHealth(int value) => health = Mathf.Clamp(value, 0, 100);
}
```

#### NonSerialized - 阻止序列化
**作用**：阻止公有字段被序列化，常用于运行时计算的临时数据。

```csharp
public class GameManager : MonoBehaviour
{
    public int score = 0;  // 会被序列化
    
    [System.NonSerialized]
    public float currentTime;  // 不会被序列化，每次运行重新计算
    
    [System.NonSerialized]
    public bool isGamePaused;  // 运行时状态，不需要保存
}
```

#### SerializeReference - 引用序列化
**作用**：序列化接口或抽象类的具体实现，支持多态序列化。

```csharp
public interface IWeapon
{
    void Attack();
}

[System.Serializable]
public class Sword : IWeapon
{
    public float damage = 50f;
    public void Attack() => Debug.Log("挥剑攻击！");
}

[System.Serializable]
public class Bow : IWeapon
{
    public int arrowCount = 30;
    public void Attack() => Debug.Log("射箭攻击！");
}

public class Player : MonoBehaviour
{
    [SerializeReference]  // 支持多态序列化
    public IWeapon currentWeapon;
    
    [SerializeReference]
    public List<IWeapon> inventory = new List<IWeapon>();
}
```

#### Serializable - 可序列化类
**作用**：标记类或结构体为可序列化，使其能够在Inspector中显示和保存。

```csharp
[System.Serializable]
public class PlayerStats
{
    public int level = 1;
    public float experience = 0f;
    public int skillPoints = 0;
    
    public void LevelUp()
    {
        level++;
        skillPoints += 3;
        experience = 0f;
    }
}

public class RPGCharacter : MonoBehaviour
{
    public PlayerStats stats = new PlayerStats();  // 在Inspector中显示为展开的字段组
}
```

#### FormerlySerializedAs - 保持序列化兼容性
**作用**：重命名字段时保持与旧版本的序列化兼容性，避免数据丢失。

```csharp
public class MovementController : MonoBehaviour
{
    [FormerlySerializedAs("speed")]  // 原来叫speed
    public float movementSpeed = 5f;  // 现在改名为movementSpeed
    
    [FormerlySerializedAs("jumpPower")]
    public float jumpForce = 10f;  // 从jumpPower改名为jumpForce
}
```

#### PreferBinarySerialization - 二进制序列化
**作用**：使ScriptableObject使用二进制序列化而非YAML，提高大数据的序列化性能。

```csharp
[CreateAssetMenu(fileName = "LargeDataSet", menuName = "Data/Large Data Set")]
[PreferBinarySerialization]  // 使用二进制序列化提高性能
public class LargeDataSet : ScriptableObject
{
    public float[] largeArray = new float[10000];  // 大量数据
    public Texture2D[] textures = new Texture2D[100];
    
    // 大量数据时二进制序列化更高效
}

### 菜单特性

#### MenuItem - 创建编辑器菜单项
**作用**：在Unity编辑器菜单栏中创建自定义菜单项，用于执行编辑器工具功能。

```csharp
public class EditorTools
{
    [MenuItem("Tools/重置选中对象位置")]
    private static void ResetSelectedTransforms()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj.transform, "Reset Transform");
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
        }
    }
    
    // 验证菜单项是否可用
    [MenuItem("Tools/重置选中对象位置", true)]
    private static bool ValidateResetSelectedTransforms()
    {
        return Selection.gameObjects.Length > 0;
    }
    
    [MenuItem("GameObject/创建空的UI容器", false, 10)]
    private static void CreateUIContainer()
    {
        GameObject container = new GameObject("UI Container");
        container.AddComponent<RectTransform>();
        Selection.activeGameObject = container;
    }
}
```

#### ContextMenu - 组件右键菜单
**作用**：为MonoBehaviour组件添加右键菜单项，方便在Inspector中快速执行操作。

```csharp
public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    
    [ContextMenu("恢复满血")]
    private void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} 血量已恢复至满血: {currentHealth}");
    }
    
    [ContextMenu("设置为半血")]
    private void SetHalfHealth()
    {
        currentHealth = maxHealth / 2;
        Debug.Log($"{gameObject.name} 血量设置为半血: {currentHealth}");
    }
    
    [ContextMenu("模拟死亡")]
    private void SimulateDeath()
    {
        currentHealth = 0;
        Debug.Log($"{gameObject.name} 已死亡");
    }
}
```

#### ContextMenuItem - 字段右键菜单
**作用**：为特定字段添加右键菜单项，提供字段相关的快捷操作。

```csharp
public class WeaponConfig : MonoBehaviour
{
    [ContextMenuItem("重置为默认值", "ResetDamage")]
    public float damage = 25f;
    
    [ContextMenuItem("随机化", "RandomizeFireRate")]
    public float fireRate = 1f;
    
    [ContextMenuItem("复制到剪贴板", "CopyWeaponName")]
    public string weaponName = "默认武器";
    
    private void ResetDamage()
    {
        damage = 25f;
    }
    
    private void RandomizeFireRate()
    {
        fireRate = Random.Range(0.5f, 3f);
    }
    
    private void CopyWeaponName()
    {
        GUIUtility.systemCopyBuffer = weaponName;
        Debug.Log($"武器名称已复制: {weaponName}");
    }
}
```

#### CreateAssetMenu - 创建资源菜单
**作用**：在Project窗口的"Create"菜单中添加创建ScriptableObject资源的选项。

```csharp
[CreateAssetMenu(
    fileName = "新的武器数据", 
    menuName = "游戏数据/武器配置", 
    order = 1
)]
public class WeaponData : ScriptableObject
{
    [Header("基础属性")]
    public string weaponName = "新武器";
    public float damage = 50f;
    public float fireRate = 1f;
    
    [Header("视觉效果")]
    public Sprite weaponIcon;
    public GameObject weaponPrefab;
    
    [Header("音效")]
    public AudioClip fireSound;
    public AudioClip reloadSound;
}

[CreateAssetMenu(
    fileName = "新的关卡数据", 
    menuName = "游戏数据/关卡配置", 
    order = 2
)]
public class LevelData : ScriptableObject
{
    public string levelName = "新关卡";
    public int levelIndex = 1;
    public float timeLimit = 300f;
    public GameObject[] enemyPrefabs;
    public Vector3[] spawnPoints;
}

### 组件相关特性

#### RequireComponent - 依赖组件
**作用**：自动添加依赖的组件，确保组件正常工作所需的其他组件存在。

```csharp
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    
    void Start()
    {
        // 这些组件会被自动添加，所以可以安全获取
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    
    void Update()
    {
        // 使用Rigidbody进行移动
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.AddForce(movement * 10f);
    }
}
```

#### DisallowMultipleComponent - 禁止多组件
**作用**：防止在同一个GameObject上添加多个相同类型的组件。

```csharp
[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    void Awake()
    {
        // 单例模式，确保只有一个GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
```

#### ExecuteInEditMode - 编辑模式执行
**作用**：使脚本在编辑模式下也能执行，常用于编辑器工具和预览功能。

```csharp
[ExecuteInEditMode]
public class GridGenerator : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f;
    public GameObject tilePrefab;
    
    void Update()
    {
        // 在编辑模式下实时更新网格预览
        if (!Application.isPlaying)
        {
            UpdateGridPreview();
        }
    }
    
    void UpdateGridPreview()
    {
        // 清除旧的子对象
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        
        // 生成新的网格
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x * cellSize, 0, y * cellSize);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                tile.name = $"Tile_{x}_{y}";
            }
        }
    }
}
```

#### ExecuteAlways - 总是执行
**作用**：在编辑模式和运行模式下都执行脚本，是ExecuteInEditMode的增强版本。

```csharp
[ExecuteAlways]
public class LightController : MonoBehaviour
{
    public Color lightColor = Color.white;
    public float intensity = 1f;
    
    private Light lightComponent;
    
    void OnEnable()
    {
        lightComponent = GetComponent<Light>();
    }
    
    void Update()
    {
        if (lightComponent != null)
        {
            lightComponent.color = lightColor;
            lightComponent.intensity = intensity;
        }
    }
}
```

#### AddComponentMenu - 组件菜单
**作用**：将组件添加到"Add Component"菜单的指定分类中，方便查找和使用。

```csharp
[AddComponentMenu("游戏系统/库存管理器")]
public class InventoryManager : MonoBehaviour
{
    public int maxSlots = 20;
    public List<Item> items = new List<Item>();
    
    public bool AddItem(Item item)
    {
        if (items.Count < maxSlots)
        {
            items.Add(item);
            return true;
        }
        return false;
    }
}

[AddComponentMenu("AI/敌人AI控制器")]
public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public Transform target;
    
    void Update()
    {
        // AI逻辑
    }
}
```

#### HideInInspector - 隐藏字段
**作用**：在Inspector中隐藏公有字段，常用于不希望在编辑器中修改的字段。

```csharp
public class ScoreManager : MonoBehaviour
{
    public int baseScore = 100;  // 在Inspector中显示
    
    [HideInInspector]
    public int currentScore;     // 在Inspector中隐藏
    
    [HideInInspector]
    public float multiplier = 1f; // 运行时计算，不需要在Inspector中显示
    
    void Start()
    {
        currentScore = baseScore;
    }
    
    public void AddScore(int points)
    {
        currentScore += Mathf.RoundToInt(points * multiplier);
    }
}
```

#### SelectionBase - 选择基础
**作用**：在场景视图中点击子对象时，选择标记了SelectionBase的父对象。

```csharp
[SelectionBase]
public class Vehicle : MonoBehaviour
{
    public Transform[] wheels;
    public Transform body;
    public Transform engine;
    
    // 当点击车轮、车身或引擎时，会选择整个Vehicle对象
    // 这样便于整体操作车辆而不是单独的部件
}
```

#### DefaultExecutionOrder - 执行顺序
**作用**：设置脚本的执行顺序，确保某些脚本在其他脚本之前或之后执行。

```csharp
[DefaultExecutionOrder(-100)]  // 优先执行
public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
        // 初始化游戏系统，需要在其他脚本之前执行
        Debug.Log("游戏系统初始化");
    }
}

[DefaultExecutionOrder(100)]   // 延后执行
public class GameFinalizer : MonoBehaviour
{
    void Start()
    {
        // 在所有其他脚本Start之后执行
        Debug.Log("游戏启动完成");
    }
}
```

#### Icon - 组件图标
**作用**：为组件在Hierarchy窗口中设置自定义图标，便于识别。

```csharp
[Icon("Assets/Icons/manager_icon.png")]
public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}
```

### 编辑器定制特性

#### CustomEditor - 自定义检视器
**作用**：为特定组件创建自定义的Inspector界面，提供更好的编辑体验。

```csharp
// 目标组件
public class WaveGenerator : MonoBehaviour
{
    public float amplitude = 1f;
    public float frequency = 1f;
    public float speed = 1f;
    public AnimationCurve waveCurve = AnimationCurve.Linear(0, 0, 1, 1);
}

// 自定义编辑器
[CustomEditor(typeof(WaveGenerator))]
public class WaveGeneratorEditor : Editor
{
    private WaveGenerator waveGen;
    
    void OnEnable()
    {
        waveGen = (WaveGenerator)target;
    }
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("波形生成器设置", EditorStyles.boldLabel);
        
        // 绘制默认属性
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        // 添加自定义按钮
        if (GUILayout.Button("生成预览波形"))
        {
            GeneratePreview();
        }
        
        if (GUILayout.Button("重置为默认值"))
        {
            ResetToDefaults();
        }
        
        // 显示实时信息
        EditorGUILayout.HelpBox($"当前波长: {2 * Mathf.PI / waveGen.frequency:F2}", MessageType.Info);
    }
    
    private void GeneratePreview()
    {
        Debug.Log("生成波形预览");
    }
    
    private void ResetToDefaults()
    {
        Undo.RecordObject(waveGen, "Reset Wave Generator");
        waveGen.amplitude = 1f;
        waveGen.frequency = 1f;
        waveGen.speed = 1f;
        waveGen.waveCurve = AnimationCurve.Linear(0, 0, 1, 1);
    }
}
```

#### CustomPropertyDrawer - 自定义属性绘制器
**作用**：为特定类型或特性创建自定义的属性绘制器，控制字段在Inspector中的显示方式。

```csharp
// 自定义数据类型
[System.Serializable]
public class MinMaxRange
{
    public float min = 0f;
    public float max = 1f;
    
    public float GetRandomValue()
    {
        return Random.Range(min, max);
    }
}

// 自定义属性绘制器
[CustomPropertyDrawer(typeof(MinMaxRange))]
public class MinMaxRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        // 绘制标签
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
        // 获取属性
        var minProp = property.FindPropertyRelative("min");
        var maxProp = property.FindPropertyRelative("max");
        
        // 计算布局
        var minRect = new Rect(position.x, position.y, 50, position.height);
        var sliderRect = new Rect(position.x + 55, position.y, position.width - 115, position.height);
        var maxRect = new Rect(position.x + position.width - 55, position.y, 50, position.height);
        
        // 绘制控件
        float minValue = EditorGUI.FloatField(minRect, minProp.floatValue);
        float maxValue = EditorGUI.FloatField(maxRect, maxProp.floatValue);
        
        EditorGUI.MinMaxSlider(sliderRect, ref minValue, ref maxValue, 0f, 10f);
        
        // 应用值
        minProp.floatValue = minValue;
        maxProp.floatValue = maxValue;
        
        EditorGUI.EndProperty();
    }
}

// 使用示例
public class SpawnController : MonoBehaviour
{
    public MinMaxRange spawnDelay = new MinMaxRange();
    public MinMaxRange spawnCount = new MinMaxRange();
}
```

#### CanEditMultipleObjects - 多对象编辑
**作用**：允许自定义编辑器同时编辑多个选中的对象。

```csharp
[CustomEditor(typeof(LightController))]
[CanEditMultipleObjects]  // 支持多选编辑
public class LightControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 绘制默认Inspector
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        // 批量操作按钮
        if (GUILayout.Button("批量开启所有灯光"))
        {
            foreach (LightController light in targets)
            {
                Undo.RecordObject(light, "Turn On Lights");
                light.GetComponent<Light>().enabled = true;
            }
        }
        
        if (GUILayout.Button("批量关闭所有灯光"))
        {
            foreach (LightController light in targets)
            {
                Undo.RecordObject(light, "Turn Off Lights");
                light.GetComponent<Light>().enabled = false;
            }
        }
        
        // 显示选中数量
        EditorGUILayout.HelpBox($"已选中 {targets.Length} 个灯光控制器", MessageType.Info);
    }
}
```

#### InitializeOnLoad - 编辑器加载时初始化
**作用**：在编辑器加载时自动初始化类，常用于编辑器工具的设置。

```csharp
[InitializeOnLoad]
public class EditorInitializer
{
    static EditorInitializer()
    {
        Debug.Log("编辑器初始化完成");
        
        // 订阅编辑器事件
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
        EditorApplication.projectChanged += OnProjectChanged;
        
        // 设置编辑器偏好
        EditorPrefs.SetBool("AutoSave", true);
    }
    
    private static void OnHierarchyChanged()
    {
        Debug.Log("层级视图发生变化");
    }
    
    private static void OnProjectChanged()
    {
        Debug.Log("项目发生变化");
    }
}
```

#### InitializeOnLoadMethod - 编辑器加载时执行方法
**作用**：标记静态方法在编辑器加载时执行，比InitializeOnLoad更精确。

```csharp
public class EditorUtilities
{
    [InitializeOnLoadMethod]
    private static void InitializeEditor()
    {
        Debug.Log("编辑器工具初始化");
        
        // 检查项目设置
        CheckProjectSettings();
        
        // 设置默认场景
        SetupDefaultScene();
    }
    
    [InitializeOnLoadMethod]
    private static void SetupCustomShortcuts()
    {
        Debug.Log("设置自定义快捷键");
        // 注册自定义快捷键
    }
    
    private static void CheckProjectSettings()
    {
        // 检查项目配置
        if (PlayerSettings.companyName == "DefaultCompany")
        {
            Debug.LogWarning("请设置公司名称");
        }
    }
    
    private static void SetupDefaultScene()
    {
        // 设置默认场景配置
    }
}
```

#### OnOpenAsset - 资源打开时执行
**作用**：在特定类型的资源被打开时执行自定义逻辑。

```csharp
public class AssetOpener
{
    [OnOpenAsset(1)]  // 优先级为1
    public static bool OnOpenAsset(int instanceID, int line)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID);
        
        // 处理特定类型的资源
        if (obj is TextAsset textAsset)
        {
            if (textAsset.name.EndsWith("_config"))
            {
                // 用自定义编辑器打开配置文件
                ConfigEditorWindow.OpenConfig(textAsset);
                return true;  // 返回true表示已处理，阻止默认行为
            }
        }
        
        // 处理ScriptableObject
        if (obj is WeaponData weaponData)
        {
            WeaponEditorWindow.OpenWeapon(weaponData);
            return true;
        }
        
        return false;  // 返回false使用默认处理方式
    }
    
    [OnOpenAsset(2)]  // 优先级为2，较低
    public static bool LogAssetOpen(int instanceID, int line)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID);
        Debug.Log($"打开资源: {obj.name} (类型: {obj.GetType().Name})");
        return false;  // 不阻止默认行为
    }
}

// 自定义编辑器窗口示例
public class ConfigEditorWindow : EditorWindow
{
    public static void OpenConfig(TextAsset config)
    {
        var window = GetWindow<ConfigEditorWindow>();
        window.titleContent = new GUIContent($"配置编辑器 - {config.name}");
        window.Show();
    }
}
```

### Gizmo相关特性

#### DrawGizmo - 绘制场景Gizmo
**作用**：在场景视图中为特定组件绘制自定义Gizmo，提供可视化调试信息。

```csharp
// 目标组件
public class DetectionZone : MonoBehaviour
{
    public float detectionRadius = 5f;
    public Color gizmoColor = Color.red;
    public bool showDetectionCone = true;
    public float coneAngle = 60f;
}

// Gizmo绘制器
public class DetectionZoneGizmos
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawDetectionZoneGizmo(DetectionZone zone, GizmoType gizmoType)
    {
        // 设置Gizmo颜色
        Gizmos.color = zone.gizmoColor;
        
        // 绘制检测范围圆圈
        Gizmos.DrawWireSphere(zone.transform.position, zone.detectionRadius);
        
        // 只为选中对象绘制额外细节
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            // 绘制半透明填充
            Color fillColor = zone.gizmoColor;
            fillColor.a = 0.1f;
            Gizmos.color = fillColor;
            Gizmos.DrawSphere(zone.transform.position, zone.detectionRadius);
            
            // 绘制检测锥形
            if (zone.showDetectionCone)
            {
                DrawDetectionCone(zone);
            }
        }
    }
    
    [DrawGizmo(GizmoType.NonSelected)]
    static void DrawSimpleGizmo(DetectionZone zone, GizmoType gizmoType)
    {
        // 为未选中对象绘制简单指示器
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(zone.transform.position, Vector3.one * 0.5f);
    }
    
    private static void DrawDetectionCone(DetectionZone zone)
    {
        Vector3 forward = zone.transform.forward;
        Vector3 position = zone.transform.position;
        
        // 计算锥形边界
        float halfAngle = zone.coneAngle * 0.5f * Mathf.Deg2Rad;
        Vector3 left = Quaternion.AngleAxis(-zone.coneAngle * 0.5f, zone.transform.up) * forward;
        Vector3 right = Quaternion.AngleAxis(zone.coneAngle * 0.5f, zone.transform.up) * forward;
        
        // 绘制锥形线条
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(position, left * zone.detectionRadius);
        Gizmos.DrawRay(position, right * zone.detectionRadius);
        Gizmos.DrawRay(position, forward * zone.detectionRadius);
    }
}
```

#### 自定义Gizmo图标
**作用**：为GameObject在场景视图中显示自定义图标，便于识别特殊对象。

```csharp
// 在组件的OnDrawGizmos方法中绘制图标
public class SpawnPoint : MonoBehaviour
{
    public Texture2D spawnIcon;  // 在Inspector中分配图标
    
    void OnDrawGizmos()
    {
        // 绘制图标
        if (spawnIcon != null)
        {
            Gizmos.DrawIcon(transform.position, spawnIcon.name, true);
        }
        
        // 绘制额外的Gizmo元素
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
    
    void OnDrawGizmosSelected()
    {
        // 选中时绘制更多细节
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 2f);
        
        // 绘制方向指示器
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 3f);
    }
}
```

#### 高级Gizmo绘制示例
**作用**：展示复杂的Gizmo绘制技术，包括条件绘制和交互式Gizmo。

```csharp
public class PathWaypoint : MonoBehaviour
{
    public PathWaypoint nextWaypoint;
    public float waypointRadius = 1f;
    public Color waypointColor = Color.yellow;
    
    [Header("调试选项")]
    public bool showConnections = true;
    public bool showDirection = true;
    public bool showIndex = true;
}

public class PathWaypointGizmos
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawWaypointGizmo(PathWaypoint waypoint, GizmoType gizmoType)
    {
        bool isSelected = (gizmoType & GizmoType.Selected) != 0;
        
        // 绘制路径点
        Gizmos.color = isSelected ? Color.white : waypoint.waypointColor;
        Gizmos.DrawWireSphere(waypoint.transform.position, waypoint.waypointRadius);
        
        if (isSelected)
        {
            // 选中时绘制填充球体
            Color fillColor = waypoint.waypointColor;
            fillColor.a = 0.3f;
            Gizmos.color = fillColor;
            Gizmos.DrawSphere(waypoint.transform.position, waypoint.waypointRadius);
        }
        
        // 绘制连接线
        if (waypoint.showConnections && waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(waypoint.transform.position, waypoint.nextWaypoint.transform.position);
            
            // 绘制方向箭头
            if (waypoint.showDirection)
            {
                DrawArrow(waypoint.transform.position, waypoint.nextWaypoint.transform.position);
            }
        }
        
        // 绘制索引标签（仅在选中时）
        if (isSelected && waypoint.showIndex)
        {
            UnityEditor.Handles.Label(
                waypoint.transform.position + Vector3.up * 2f,
                $"路径点 {waypoint.transform.GetSiblingIndex()}"
            );
        }
    }
    
    private static void DrawArrow(Vector3 from, Vector3 to)
    {
        Vector3 direction = (to - from).normalized;
        Vector3 right = Vector3.Cross(direction, Vector3.up).normalized;
        Vector3 arrowHead = to - direction * 0.5f;
        
        // 绘制箭头
        Gizmos.DrawLine(arrowHead, arrowHead + (-direction + right) * 0.3f);
        Gizmos.DrawLine(arrowHead, arrowHead + (-direction - right) * 0.3f);
    }
}

### 设置相关特性

#### SettingsProvider - 创建设置提供者
**作用**：在Unity的Project Settings或Preferences窗口中创建自定义设置面板。

```csharp
// 设置数据类
[System.Serializable]
public class MyProjectSettings
{
    public string apiKey = "";
    public bool enableDebugMode = false;
    public float networkTimeout = 30f;
    public LogLevel logLevel = LogLevel.Info;
}

public enum LogLevel
{
    Debug, Info, Warning, Error
}

// 设置提供者
public class MyProjectSettingsProvider
{
    private static MyProjectSettings settings;
    private static SerializedObject serializedSettings;
    
    [SettingsProvider]
    public static SettingsProvider CreateMyProjectSettingsProvider()
    {
        var provider = new SettingsProvider("Project/My Game Settings", SettingsScope.Project)
        {
            label = "我的游戏设置",
            guiHandler = (searchContext) =>
            {
                // 确保设置已加载
                if (settings == null)
                {
                    LoadSettings();
                }
                
                if (serializedSettings == null || serializedSettings.targetObject == null)
                {
                    serializedSettings = new SerializedObject(ScriptableObject.CreateInstance<MyProjectSettingsAsset>());
                }
                
                EditorGUILayout.LabelField("API设置", EditorStyles.boldLabel);
                settings.apiKey = EditorGUILayout.TextField("API密钥", settings.apiKey);
                
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("调试设置", EditorStyles.boldLabel);
                settings.enableDebugMode = EditorGUILayout.Toggle("启用调试模式", settings.enableDebugMode);
                settings.logLevel = (LogLevel)EditorGUILayout.EnumPopup("日志级别", settings.logLevel);
                
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("网络设置", EditorStyles.boldLabel);
                settings.networkTimeout = EditorGUILayout.FloatField("网络超时(秒)", settings.networkTimeout);
                
                // 保存按钮
                EditorGUILayout.Space();
                if (GUILayout.Button("保存设置"))
                {
                    SaveSettings();
                }
                
                if (GUILayout.Button("重置为默认值"))
                {
                    ResetToDefaults();
                }
            },
            
            // 搜索关键词
            keywords = new HashSet<string>(new[] { "API", "Debug", "Network", "Log" })
        };
        
        return provider;
    }
    
    private static void LoadSettings()
    {
        string json = EditorPrefs.GetString("MyProjectSettings", "{}");
        settings = JsonUtility.FromJson<MyProjectSettings>(json);
        if (settings == null)
        {
            settings = new MyProjectSettings();
        }
    }
    
    private static void SaveSettings()
    {
        string json = JsonUtility.ToJson(settings, true);
        EditorPrefs.SetString("MyProjectSettings", json);
        Debug.Log("设置已保存");
    }
    
    private static void ResetToDefaults()
    {
        settings = new MyProjectSettings();
        SaveSettings();
    }
}

// 用于序列化的ScriptableObject
public class MyProjectSettingsAsset : ScriptableObject
{
    public MyProjectSettings settings = new MyProjectSettings();
}
```

#### UserSetting - 用户设置
**作用**：标记字段为用户特定的设置，这些设置会保存在用户的偏好设置中。

```csharp
public class EditorPreferences
{
    // 用户设置会自动保存到EditorPrefs
    [UserSetting("Editor/Auto Save")]
    public static bool autoSave = true;
    
    [UserSetting("Editor/Show Grid")]
    public static bool showGrid = false;
    
    [UserSetting("Editor/Grid Size")]
    public static float gridSize = 1f;
    
    [UserSetting("Editor/Theme")]
    public static string editorTheme = "Dark";
    
    // 创建偏好设置界面
    [SettingsProvider]
    public static SettingsProvider CreateUserPreferencesProvider()
    {
        var provider = new SettingsProvider("Preferences/My Editor", SettingsScope.User)
        {
            label = "我的编辑器偏好",
            guiHandler = (searchContext) =>
            {
                EditorGUILayout.LabelField("编辑器设置", EditorStyles.boldLabel);
                
                autoSave = EditorGUILayout.Toggle("自动保存", autoSave);
                showGrid = EditorGUILayout.Toggle("显示网格", showGrid);
                
                if (showGrid)
                {
                    EditorGUI.indentLevel++;
                    gridSize = EditorGUILayout.FloatField("网格大小", gridSize);
                    EditorGUI.indentLevel--;
                }
                
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("外观设置", EditorStyles.boldLabel);
                
                string[] themes = { "Light", "Dark", "Custom" };
                int themeIndex = System.Array.IndexOf(themes, editorTheme);
                themeIndex = EditorGUILayout.Popup("编辑器主题", themeIndex, themes);
                editorTheme = themes[themeIndex];
                
                // 应用设置
                if (GUI.changed)
                {
                    ApplySettings();
                }
            },
            keywords = new HashSet<string>(new[] { "Auto", "Save", "Grid", "Theme" })
        };
        
        return provider;
    }
    
    private static void ApplySettings()
    {
        // 应用设置到编辑器
        EditorPrefs.SetBool("MyEditor_AutoSave", autoSave);
        EditorPrefs.SetBool("MyEditor_ShowGrid", showGrid);
        EditorPrefs.SetFloat("MyEditor_GridSize", gridSize);
        EditorPrefs.SetString("MyEditor_Theme", editorTheme);
    }
}
```

#### SettingsProviderGroup - 设置提供者组
**作用**：将多个相关的设置提供者组织在一起，创建设置分组。

```csharp
// 游戏设置组
public class GameSettingsGroup
{
    [SettingsProviderGroup]
    public static SettingsProvider[] CreateGameSettingsGroup()
    {
        return new SettingsProvider[]
        {
            CreatePlayerSettingsProvider(),
            CreateAudioSettingsProvider(),
            CreateGraphicsSettingsProvider()
        };
    }
    
    private static SettingsProvider CreatePlayerSettingsProvider()
    {
        return new SettingsProvider("Project/Game Settings/Player", SettingsScope.Project)
        {
            label = "玩家设置",
            guiHandler = (searchContext) =>
            {
                EditorGUILayout.LabelField("玩家配置", EditorStyles.boldLabel);
                
                PlayerSettings.defaultInterfaceOrientation = (UIOrientation)EditorGUILayout.EnumPopup(
                    "默认界面方向", PlayerSettings.defaultInterfaceOrientation);
                
                PlayerSettings.allowedAutorotateToPortrait = EditorGUILayout.Toggle(
                    "允许竖屏旋转", PlayerSettings.allowedAutorotateToPortrait);
                
                PlayerSettings.allowedAutorotateToLandscapeLeft = EditorGUILayout.Toggle(
                    "允许左横屏", PlayerSettings.allowedAutorotateToLandscapeLeft);
                
                PlayerSettings.allowedAutorotateToLandscapeRight = EditorGUILayout.Toggle(
                    "允许右横屏", PlayerSettings.allowedAutorotateToLandscapeRight);
            }
        };
    }
    
    private static SettingsProvider CreateAudioSettingsProvider()
    {
        return new SettingsProvider("Project/Game Settings/Audio", SettingsScope.Project)
        {
            label = "音频设置",
            guiHandler = (searchContext) =>
            {
                EditorGUILayout.LabelField("音频配置", EditorStyles.boldLabel);
                
                AudioSettings.GetConfiguration(out var config);
                
                EditorGUILayout.LabelField($"采样率: {config.sampleRate} Hz");
                EditorGUILayout.LabelField($"缓冲区大小: {config.bufferSize}");
                EditorGUILayout.LabelField($"扬声器模式: {config.speakerMode}");
                
                if (GUILayout.Button("重置音频设置"))
                {
                    AudioSettings.Reset();
                }
            }
        };
    }
    
    private static SettingsProvider CreateGraphicsSettingsProvider()
    {
        return new SettingsProvider("Project/Game Settings/Graphics", SettingsScope.Project)
        {
            label = "图形设置",
            guiHandler = (searchContext) =>
            {
                EditorGUILayout.LabelField("图形配置", EditorStyles.boldLabel);
                
                QualitySettings.pixelLightCount = EditorGUILayout.IntField(
                    "像素光源数量", QualitySettings.pixelLightCount);
                
                QualitySettings.shadows = (ShadowQuality)EditorGUILayout.EnumPopup(
                    "阴影质量", QualitySettings.shadows);
                
                QualitySettings.shadowResolution = (ShadowResolution)EditorGUILayout.EnumPopup(
                    "阴影分辨率", QualitySettings.shadowResolution);
                
                QualitySettings.antiAliasing = EditorGUILayout.IntPopup(
                    "抗锯齿", QualitySettings.antiAliasing, 
                    new string[] { "禁用", "2x", "4x", "8x" },
                    new int[] { 0, 2, 4, 8 });
            }
        };
    }
}

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

## 运行时特性详解

### RuntimeInitializeOnLoadMethod - 运行时初始化
**作用**：在游戏运行时自动执行初始化方法，支持不同的加载时机。

```csharp
public class GameInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeScene()
    {
        Debug.Log("在场景加载前初始化");
        // 初始化核心系统，如数据管理器
        DataManager.Initialize();
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAfterScene()
    {
        Debug.Log("在场景加载后初始化");
        // 初始化依赖场景对象的系统
        UIManager.Initialize();
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void InitializeAfterAssemblies()
    {
        Debug.Log("程序集加载后初始化");
        // 注册事件监听器
        EventManager.RegisterGlobalListeners();
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private static void InitializeBeforeSplash()
    {
        Debug.Log("启动画面前初始化");
        // 设置应用程序配置
        Application.targetFrameRate = 60;
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void RegisterSubsystems()
    {
        Debug.Log("注册子系统");
        // 注册自定义子系统
    }
}
```

### HelpURL - 帮助文档链接
**作用**：为组件添加帮助文档链接，在Inspector中显示帮助按钮。

```csharp
[HelpURL("https://docs.unity3d.com/Manual/class-AudioSource.html")]
public class CustomAudioController : MonoBehaviour
{
    [Header("音频设置")]
    public AudioClip backgroundMusic;
    public AudioClip[] soundEffects;
    
    [Range(0f, 1f)]
    public float masterVolume = 1f;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = masterVolume;
            audioSource.Play();
        }
    }
    
    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length && soundEffects[index] != null)
        {
            audioSource.PlayOneShot(soundEffects[index], masterVolume);
        }
    }
}
```

### Obsolete - 标记过时API
**作用**：标记方法、类或字段为过时，提供升级指导。

```csharp
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    // 新的移动方法
    public void Move(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    
    // 标记为过时的方法
    [System.Obsolete("使用 Move(Vector3 direction) 方法替代")]
    public void MovePlayer(float x, float z)
    {
        Move(new Vector3(x, 0, z));
    }
    
    // 带有错误级别的过时标记
    [System.Obsolete("此方法已被移除，请使用新的移动系统", true)]
    public void OldMoveMethod()
    {
        // 这个方法会产生编译错误
    }
    
    // 提供升级路径的过时标记
    [System.Obsolete("请使用 SetSpeed(float speed) 方法。这个属性将在下个版本中移除。")]
    public float Speed
    {
        get => moveSpeed;
        set => SetSpeed(value);
    }
    
    public void SetSpeed(float speed)
    {
        moveSpeed = Mathf.Clamp(speed, 0f, 20f);
    }
}
```

## 特性组合使用示例

### 完整的组件示例
**展示多个特性的组合使用，创建功能完整且用户友好的组件。**

```csharp
[AddComponentMenu("游戏系统/高级玩家控制器")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[DisallowMultipleComponent]
[HelpURL("https://mygame.com/docs/player-controller")]
public class AdvancedPlayerController : MonoBehaviour
{
    [Header("移动设置")]
    [Tooltip("玩家移动速度")]
    [Range(1f, 20f)]
    public float moveSpeed = 8f;
    
    [Tooltip("跳跃力度")]
    [Range(5f, 30f)]
    public float jumpForce = 15f;
    
    [Space(10)]
    [Header("高级设置")]
    [Tooltip("空中控制力度，0表示无法在空中控制")]
    [Range(0f, 1f)]
    public float airControl = 0.3f;
    
    [SerializeField]
    [Tooltip("地面检测层级")]
    private LayerMask groundLayer = 1;
    
    [Space(10)]
    [Header("音效设置")]
    [Tooltip("脚步声音效")]
    public AudioClip[] footstepSounds;
    
    [Tooltip("跳跃音效")]
    public AudioClip jumpSound;
    
    [Tooltip("着陆音效")]
    public AudioClip landSound;
    
    [Space(10)]
    [Header("调试信息")]
    [SerializeField]
    [Tooltip("显示调试信息")]
    private bool showDebugInfo = false;
    
    [HideInInspector]
    public bool isGrounded;
    
    [HideInInspector]
    public Vector3 velocity;
    
    // 私有字段
    private Rigidbody rb;
    private CapsuleCollider col;
    private AudioSource audioSource;
    
    [SerializeField]
    private float groundCheckDistance = 0.1f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void Update()
    {
        CheckGrounded();
        HandleMovement();
        HandleJump();
        
        if (showDebugInfo)
        {
            DisplayDebugInfo();
        }
    }
    
    private void CheckGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, 
            col.height * 0.5f + groundCheckDistance, groundLayer);
    }
    
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
        
        if (isGrounded)
        {
            rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
            
            // 播放脚步声
            if (movement.magnitude > 0.1f && footstepSounds.Length > 0)
            {
                PlayFootstepSound();
            }
        }
        else
        {
            // 空中控制
            Vector3 airMovement = movement * moveSpeed * airControl;
            rb.velocity = new Vector3(airMovement.x, rb.velocity.y, airMovement.z);
        }
        
        velocity = rb.velocity;
    }
    
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            
            if (jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }
    
    private void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip, 0.5f);
        }
    }
    
    private void DisplayDebugInfo()
    {
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * 
            (col.height * 0.5f + groundCheckDistance), isGrounded ? Color.green : Color.red);
    }
    
    [ContextMenu("重置为默认值")]
    private void ResetToDefaults()
    {
        moveSpeed = 8f;
        jumpForce = 15f;
        airControl = 0.3f;
        groundLayer = 1;
        showDebugInfo = false;
    }
    
    [ContextMenu("测试跳跃")]
    private void TestJump()
    {
        if (Application.isPlaying && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}
```

## 总结

Unity特性系统是扩展编辑器和定制组件行为的强大工具。通过合理使用特性，可以显著提升开发效率、改善编辑器体验并实现复杂的定制功能。特性的组合使用尤其重要，能够创建直观、便于使用的组件检视器和编辑工具。

### 关键要点：

1. **界面优化**：使用`Header`、`Tooltip`、`Space`等特性改善Inspector布局
2. **数据控制**：通过`Range`、`SerializeField`、`HideInInspector`控制数据显示和编辑
3. **功能扩展**：利用`ContextMenu`、`MenuItem`添加便捷操作
4. **编辑器定制**：使用`CustomEditor`、`CustomPropertyDrawer`创建专业的编辑界面
5. **系统集成**：通过`RequireComponent`、`ExecuteInEditMode`等确保组件正确工作
6. **调试支持**：使用`DrawGizmo`、Gizmo方法提供可视化调试信息

无论是使用Unity提供的内置特性，还是创建自定义特性，掌握特性系统都能使你的Unity开发流程更加高效和灵活。通过理解每个特性的用途和应用场景，你可以根据项目需求选择最合适的特性组合，创建出既直观又强大的工具和组件。 