# EditorPrefsUtility 模块

## 概述
EditorPrefsUtility 模块提供了对Unity内置 EditorPrefs 功能的扩展，使其能够存储和读取复杂的对象数据类型，而不仅限于基本数据类型（int、float、string、bool）。该工具利用 Unity 的 JsonUtility 实现对象的序列化和反序列化，将复杂对象转换为 JSON 字符串后存储，并在需要时从 JSON 字符串重新构建对象。这使得开发者可以更方便地在编辑器会话之间持久化复杂的数据结构，大大增强了编辑器工具开发的灵活性。

## 核心功能
- **对象序列化存储**：将对象序列化为JSON并通过EditorPrefs存储
- **对象反序列化读取**：从EditorPrefs读取JSON并反序列化为对象
- **类型安全的API**：提供泛型接口，确保类型安全的数据存取

## 重要接口和类

### `EditorPrefsUtility` 类
提供扩展EditorPrefs功能的静态方法集合。

| 方法 | 说明 |
|------|------|
| `SetObject<T>(string key, T t)` | 将对象序列化为JSON并存储在EditorPrefs中 |
| `GetObject<T>(string key)` | 从EditorPrefs读取JSON并反序列化为指定类型的对象 |

## UML类图

```
+----------------------+     使用     +----------------+     使用     +----------------+
|  EditorPrefsUtility  |------------>|  EditorPrefs   |------------>|  JsonUtility   |
+----------------------+             +----------------+             +----------------+
| +SetObject<T>()      |             | +SetString()   |             | +ToJson()      |
| +GetObject<T>()      |             | +GetString()   |             | +FromJson<T>() |
+----------------------+             | +HasKey()      |             +----------------+
                                     +----------------+
```

## 数据流程图

```
+--------+     ToJson     +---------+    SetString    +------------+
| 对象实例 | -------------> | JSON字符串 | -------------> | EditorPrefs |
+--------+                +---------+                 +------------+
    ^                                                       |
    |                                                       |
    |                +---------+    GetString     |
    +--------------- | JSON字符串 | <-------------- +
      FromJson<T>    +---------+
```

## 思维导图

```
EditorPrefsUtility
├── 功能扩展
│   ├── 对象序列化
│   │   ├── JsonUtility.ToJson转换
│   │   └── EditorPrefs.SetString存储
│   └── 对象反序列化
│       ├── EditorPrefs.GetString读取
│       └── JsonUtility.FromJson<T>转换
├── 应用场景
│   ├── 编辑器工具设置存储
│   ├── 复杂数据结构持久化
│   ├── 用户偏好设置保存
│   └── 工作流程状态记忆
└── 相关API集成
    ├── EditorPrefs系统整合
    ├── JsonUtility序列化支持
    └── 泛型类型支持
```

## 应用场景
1. **复杂编辑器设置存储**：保存包含多个参数的编辑器工具设置
2. **用户界面状态记忆**：记住自定义编辑器窗口的各种状态和配置
3. **工作流程进度保存**：在多步骤工作流程中保存中间状态和进度
4. **自定义项目设置**：为项目创建自定义设置面板，并持久化设置
5. **团队配置共享**：在团队成员之间共享统一的编辑器配置

## 最佳实践
1. **键命名规范**：使用有意义且唯一的键名，避免冲突
2. **类型安全使用**：确保存取操作使用相同的类型参数
3. **可序列化对象**：确保待序列化的对象类型满足Unity JSON序列化要求
4. **数据版本控制**：考虑数据格式变化的兼容性问题
5. **默认值处理**：合理处理数据不存在时的默认值返回

## 扩展示例
以下是对EditorPrefsUtility的扩展示例，增加了更多类型和功能支持：

```csharp
public static class EditorPrefsUtilityExtended
{
    // 存储Vector3类型
    public static void SetVector3(string key, Vector3 vector)
    {
        EditorPrefs.SetFloat(key + "_x", vector.x);
        EditorPrefs.SetFloat(key + "_y", vector.y);
        EditorPrefs.SetFloat(key + "_z", vector.z);
    }
    
    // 读取Vector3类型
    public static Vector3 GetVector3(string key, Vector3 defaultValue = default)
    {
        if (!EditorPrefs.HasKey(key + "_x"))
            return defaultValue;
            
        return new Vector3(
            EditorPrefs.GetFloat(key + "_x"),
            EditorPrefs.GetFloat(key + "_y"),
            EditorPrefs.GetFloat(key + "_z")
        );
    }
    
    // 存储Color类型
    public static void SetColor(string key, Color color)
    {
        EditorPrefs.SetFloat(key + "_r", color.r);
        EditorPrefs.SetFloat(key + "_g", color.g);
        EditorPrefs.SetFloat(key + "_b", color.b);
        EditorPrefs.SetFloat(key + "_a", color.a);
    }
    
    // 读取Color类型
    public static Color GetColor(string key, Color defaultValue = default)
    {
        if (!EditorPrefs.HasKey(key + "_r"))
            return defaultValue;
            
        return new Color(
            EditorPrefs.GetFloat(key + "_r"),
            EditorPrefs.GetFloat(key + "_g"),
            EditorPrefs.GetFloat(key + "_b"),
            EditorPrefs.GetFloat(key + "_a")
        );
    }
    
    // 存储字符串数组
    public static void SetStringArray(string key, string[] array)
    {
        EditorPrefs.SetInt(key + "_count", array.Length);
        for (int i = 0; i < array.Length; i++)
        {
            EditorPrefs.SetString(key + "_" + i, array[i]);
        }
    }
    
    // 读取字符串数组
    public static string[] GetStringArray(string key)
    {
        if (!EditorPrefs.HasKey(key + "_count"))
            return new string[0];
            
        int count = EditorPrefs.GetInt(key + "_count");
        string[] array = new string[count];
        
        for (int i = 0; i < count; i++)
        {
            array[i] = EditorPrefs.GetString(key + "_" + i);
        }
        
        return array;
    }
}
```

## 代码示例
```csharp
// 自定义数据类
[System.Serializable]
public class EditorToolSettings
{
    public string projectName;
    public bool autoSave;
    public int buildNumber;
    public List<string> recentFiles;
}

// 使用示例
public class SettingsManager
{
    private const string SettingsKey = "MyTool_Settings";
    
    public static void SaveSettings(EditorToolSettings settings)
    {
        EditorPrefsUtility.SetObject(SettingsKey, settings);
    }
    
    public static EditorToolSettings LoadSettings()
    {
        EditorToolSettings settings = EditorPrefsUtility.GetObject<EditorToolSettings>(SettingsKey);
        
        // 如果设置不存在，创建默认设置
        if (settings == null)
        {
            settings = new EditorToolSettings
            {
                projectName = "New Project",
                autoSave = true,
                buildNumber = 1,
                recentFiles = new List<string>()
            };
        }
        
        return settings;
    }
}
```

## 相关资源
- [Unity文档: EditorPrefs](https://docs.unity3d.com/ScriptReference/EditorPrefs.html)
- [Unity文档: JsonUtility](https://docs.unity3d.com/ScriptReference/JsonUtility.html)
- [Unity文档: 序列化](https://docs.unity3d.com/Manual/script-Serialization.html)
