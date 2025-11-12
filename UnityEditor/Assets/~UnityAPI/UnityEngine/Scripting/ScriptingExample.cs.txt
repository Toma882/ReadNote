using UnityEngine;
using UnityEngine.Scripting;
using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// UnityEngine.Scripting 命名空间案例演示
/// 展示脚本系统的核心功能
/// </summary>
public class ScriptingExample : MonoBehaviour
{
    [Header("脚本系统设置")]
    [SerializeField] private bool enableScriptingSystem = true;
    [SerializeField] private bool enableReflection = true;
    [SerializeField] private bool enableAttributeProcessing = true;
    [SerializeField] private bool enableAssemblyLoading = true;
    
    [Header("脚本状态")]
    [SerializeField] private bool isScriptingSystemActive = false;
    [SerializeField] private int totalAssemblies = 0;
    [SerializeField] private int loadedAssemblies = 0;
    [SerializeField] private int totalTypes = 0;
    [SerializeField] private int totalMethods = 0;
    [SerializeField] private List<string> assemblyList = new List<string>();
    
    [Header("反射信息")]
    [SerializeField] private string targetTypeName = "UnityEngine.GameObject";
    [SerializeField] private string targetMethodName = "Find";
    [SerializeField] private bool showPrivateMembers = false;
    [SerializeField] private bool showStaticMembers = true;
    
    [Header("属性处理")]
    [SerializeField] private bool processCustomAttributes = true;
    [SerializeField] private bool processBuiltinAttributes = true;
    [SerializeField] private List<string> attributeList = new List<string>();
    
    // 脚本系统事件
    private System.Action<string> onAssemblyLoaded;
    private System.Action<string> onTypeDiscovered;
    private System.Action<string> onMethodInvoked;
    private System.Action<string> onAttributeProcessed;
    
    // 缓存
    private Dictionary<string, Type> typeCache = new Dictionary<string, Type>();
    private Dictionary<string, MethodInfo> methodCache = new Dictionary<string, MethodInfo>();
    private List<Assembly> loadedAssemblyList = new List<Assembly>();
    
    private void Start()
    {
        InitializeScriptingSystem();
    }
    
    /// <summary>
    /// 初始化脚本系统
    /// </summary>
    private void InitializeScriptingSystem()
    {
        // 获取程序集信息
        GetAssemblyInformation();
        
        // 设置脚本系统事件
        SetupScriptingSystemEvents();
        
        // 初始化类型缓存
        InitializeTypeCache();
        
        // 初始化方法缓存
        InitializeMethodCache();
        
        isScriptingSystemActive = true;
        Debug.Log("脚本系统初始化完成");
    }
    
    /// <summary>
    /// 获取程序集信息
    /// </summary>
    private void GetAssemblyInformation()
    {
        // 获取所有已加载的程序集
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        loadedAssemblies = assemblies.Length;
        totalAssemblies = assemblies.Length;
        
        // 统计类型和方法数量
        totalTypes = 0;
        totalMethods = 0;
        assemblyList.Clear();
        loadedAssemblyList.Clear();
        
        foreach (Assembly assembly in assemblies)
        {
            try
            {
                assemblyList.Add(assembly.FullName);
                loadedAssemblyList.Add(assembly);
                
                Type[] types = assembly.GetTypes();
                totalTypes += types.Length;
                
                foreach (Type type in types)
                {
                    MethodInfo[] methods = type.GetMethods();
                    totalMethods += methods.Length;
                }
                
                onAssemblyLoaded?.Invoke(assembly.FullName);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"无法处理程序集 {assembly.FullName}: {ex.Message}");
            }
        }
        
        Debug.Log($"发现 {loadedAssemblies} 个程序集，{totalTypes} 个类型，{totalMethods} 个方法");
    }
    
    /// <summary>
    /// 设置脚本系统事件
    /// </summary>
    private void SetupScriptingSystemEvents()
    {
        // 注册程序集加载事件
        AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoaded;
        
        Debug.Log("脚本系统事件设置完成");
    }
    
    /// <summary>
    /// 初始化类型缓存
    /// </summary>
    private void InitializeTypeCache()
    {
        typeCache.Clear();
        
        foreach (Assembly assembly in loadedAssemblyList)
        {
            try
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    string typeKey = $"{assembly.GetName().Name}.{type.Name}";
                    if (!typeCache.ContainsKey(typeKey))
                    {
                        typeCache[typeKey] = type;
                        onTypeDiscovered?.Invoke(typeKey);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"无法缓存程序集 {assembly.FullName} 中的类型: {ex.Message}");
            }
        }
        
        Debug.Log($"缓存了 {typeCache.Count} 个类型");
    }
    
    /// <summary>
    /// 初始化方法缓存
    /// </summary>
    private void InitializeMethodCache()
    {
        methodCache.Clear();
        
        foreach (var kvp in typeCache)
        {
            Type type = kvp.Value;
            try
            {
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    string methodKey = $"{kvp.Key}.{method.Name}";
                    if (!methodCache.ContainsKey(methodKey))
                    {
                        methodCache[methodKey] = method;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"无法缓存类型 {type.Name} 中的方法: {ex.Message}");
            }
        }
        
        Debug.Log($"缓存了 {methodCache.Count} 个方法");
    }
    
    /// <summary>
    /// 获取类型信息
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <returns>类型信息</returns>
    public Type GetTypeInfo(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
        {
            Debug.LogError("类型名称不能为空");
            return null;
        }
        
        // 首先尝试从缓存中获取
        if (typeCache.ContainsKey(typeName))
        {
            return typeCache[typeName];
        }
        
        // 尝试从所有程序集中查找
        foreach (Assembly assembly in loadedAssemblyList)
        {
            try
            {
                Type type = assembly.GetType(typeName);
                if (type != null)
                {
                    typeCache[typeName] = type;
                    return type;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"在程序集 {assembly.FullName} 中查找类型 {typeName} 时出错: {ex.Message}");
            }
        }
        
        Debug.LogWarning($"未找到类型: {typeName}");
        return null;
    }
    
    /// <summary>
    /// 获取方法信息
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <param name="methodName">方法名称</param>
    /// <returns>方法信息</returns>
    public MethodInfo GetMethodInfo(string typeName, string methodName)
    {
        if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(methodName))
        {
            Debug.LogError("类型名称和方法名称不能为空");
            return null;
        }
        
        string methodKey = $"{typeName}.{methodName}";
        
        // 首先尝试从缓存中获取
        if (methodCache.ContainsKey(methodKey))
        {
            return methodCache[methodKey];
        }
        
        // 获取类型信息
        Type type = GetTypeInfo(typeName);
        if (type == null)
        {
            return null;
        }
        
        // 查找方法
        MethodInfo method = type.GetMethod(methodName);
        if (method != null)
        {
            methodCache[methodKey] = method;
            return method;
        }
        
        Debug.LogWarning($"在类型 {typeName} 中未找到方法: {methodName}");
        return null;
    }
    
    /// <summary>
    /// 通过反射调用方法
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <param name="methodName">方法名称</param>
    /// <param name="parameters">参数</param>
    /// <returns>方法返回值</returns>
    public object InvokeMethod(string typeName, string methodName, params object[] parameters)
    {
        if (!enableReflection)
        {
            Debug.LogWarning("反射功能已禁用");
            return null;
        }
        
        MethodInfo method = GetMethodInfo(typeName, methodName);
        if (method == null)
        {
            return null;
        }
        
        try
        {
            object instance = null;
            
            // 如果是静态方法，不需要实例
            if (!method.IsStatic)
            {
                // 尝试创建实例
                Type type = GetTypeInfo(typeName);
                if (type != null)
                {
                    instance = Activator.CreateInstance(type);
                }
            }
            
            object result = method.Invoke(instance, parameters);
            onMethodInvoked?.Invoke($"{typeName}.{methodName}");
            
            Debug.Log($"成功调用方法: {typeName}.{methodName}");
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"调用方法 {typeName}.{methodName} 时出错: {ex.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// 获取类型的所有成员
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <returns>成员信息列表</returns>
    public List<MemberInfo> GetTypeMembers(string typeName)
    {
        Type type = GetTypeInfo(typeName);
        if (type == null)
        {
            return new List<MemberInfo>();
        }
        
        List<MemberInfo> members = new List<MemberInfo>();
        
        try
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            
            if (showPrivateMembers)
            {
                flags |= BindingFlags.NonPublic;
            }
            
            if (showStaticMembers)
            {
                flags |= BindingFlags.Static;
            }
            
            // 获取方法
            MethodInfo[] methods = type.GetMethods(flags);
            members.AddRange(methods);
            
            // 获取属性
            PropertyInfo[] properties = type.GetProperties(flags);
            members.AddRange(properties);
            
            // 获取字段
            FieldInfo[] fields = type.GetFields(flags);
            members.AddRange(fields);
            
            // 获取事件
            EventInfo[] events = type.GetEvents(flags);
            members.AddRange(events);
            
            // 获取构造函数
            ConstructorInfo[] constructors = type.GetConstructors(flags);
            members.AddRange(constructors);
        }
        catch (Exception ex)
        {
            Debug.LogError($"获取类型 {typeName} 的成员时出错: {ex.Message}");
        }
        
        return members;
    }
    
    /// <summary>
    /// 获取类型的属性
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <returns>属性信息列表</returns>
    public List<Attribute> GetTypeAttributes(string typeName)
    {
        if (!enableAttributeProcessing)
        {
            Debug.LogWarning("属性处理功能已禁用");
            return new List<Attribute>();
        }
        
        Type type = GetTypeInfo(typeName);
        if (type == null)
        {
            return new List<Attribute>();
        }
        
        List<Attribute> attributes = new List<Attribute>();
        
        try
        {
            // 获取类型属性
            Attribute[] typeAttributes = Attribute.GetCustomAttributes(type);
            attributes.AddRange(typeAttributes);
            
            // 获取方法属性
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                Attribute[] methodAttributes = Attribute.GetCustomAttributes(method);
                attributes.AddRange(methodAttributes);
            }
            
            // 获取属性属性
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Attribute[] propertyAttributes = Attribute.GetCustomAttributes(property);
                attributes.AddRange(propertyAttributes);
            }
            
            // 获取字段属性
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Attribute[] fieldAttributes = Attribute.GetCustomAttributes(field);
                attributes.AddRange(fieldAttributes);
            }
            
            // 更新属性列表
            attributeList.Clear();
            foreach (Attribute attribute in attributes)
            {
                string attributeName = attribute.GetType().Name;
                if (!attributeList.Contains(attributeName))
                {
                    attributeList.Add(attributeName);
                    onAttributeProcessed?.Invoke(attributeName);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"获取类型 {typeName} 的属性时出错: {ex.Message}");
        }
        
        return attributes;
    }
    
    /// <summary>
    /// 创建类型实例
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <param name="parameters">构造函数参数</param>
    /// <returns>实例对象</returns>
    public object CreateInstance(string typeName, params object[] parameters)
    {
        Type type = GetTypeInfo(typeName);
        if (type == null)
        {
            return null;
        }
        
        try
        {
            object instance = Activator.CreateInstance(type, parameters);
            Debug.Log($"成功创建类型 {typeName} 的实例");
            return instance;
        }
        catch (Exception ex)
        {
            Debug.LogError($"创建类型 {typeName} 的实例时出错: {ex.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// 检查类型是否实现了指定接口
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <param name="interfaceName">接口名称</param>
    /// <returns>是否实现接口</returns>
    public bool ImplementsInterface(string typeName, string interfaceName)
    {
        Type type = GetTypeInfo(typeName);
        Type interfaceType = GetTypeInfo(interfaceName);
        
        if (type == null || interfaceType == null)
        {
            return false;
        }
        
        return interfaceType.IsAssignableFrom(type);
    }
    
    /// <summary>
    /// 检查类型是否继承自指定基类
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <param name="baseTypeName">基类名称</param>
    /// <returns>是否继承自基类</returns>
    public bool InheritsFrom(string typeName, string baseTypeName)
    {
        Type type = GetTypeInfo(typeName);
        Type baseType = GetTypeInfo(baseTypeName);
        
        if (type == null || baseType == null)
        {
            return false;
        }
        
        return type.IsSubclassOf(baseType);
    }
    
    /// <summary>
    /// 获取泛型类型参数
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <returns>泛型参数类型数组</returns>
    public Type[] GetGenericArguments(string typeName)
    {
        Type type = GetTypeInfo(typeName);
        if (type == null || !type.IsGenericType)
        {
            return new Type[0];
        }
        
        return type.GetGenericArguments();
    }
    
    /// <summary>
    /// 获取程序集信息
    /// </summary>
    /// <param name="assemblyName">程序集名称</param>
    /// <returns>程序集信息</returns>
    public Assembly GetAssemblyInfo(string assemblyName)
    {
        foreach (Assembly assembly in loadedAssemblyList)
        {
            if (assembly.GetName().Name == assemblyName)
            {
                return assembly;
            }
        }
        
        return null;
    }
    
    /// <summary>
    /// 获取脚本系统信息
    /// </summary>
    public void GetScriptingSystemInfo()
    {
        Debug.Log("=== 脚本系统信息 ===");
        Debug.Log($"脚本系统启用: {enableScriptingSystem}");
        Debug.Log($"反射功能启用: {enableReflection}");
        Debug.Log($"属性处理启用: {enableAttributeProcessing}");
        Debug.Log($"程序集加载启用: {enableAssemblyLoading}");
        
        Debug.Log($"脚本系统活跃: {isScriptingSystemActive}");
        Debug.Log($"总程序集数: {totalAssemblies}");
        Debug.Log($"已加载程序集数: {loadedAssemblies}");
        Debug.Log($"总类型数: {totalTypes}");
        Debug.Log($"总方法数: {totalMethods}");
        Debug.Log($"缓存类型数: {typeCache.Count}");
        Debug.Log($"缓存方法数: {methodCache.Count}");
        Debug.Log($"发现属性数: {attributeList.Count}");
        
        Debug.Log("已加载的程序集:");
        for (int i = 0; i < Mathf.Min(assemblyList.Count, 10); i++)
        {
            Debug.Log($"  {assemblyList[i]}");
        }
        
        if (assemblyList.Count > 10)
        {
            Debug.Log($"  ... 还有 {assemblyList.Count - 10} 个程序集");
        }
        
        Debug.Log("发现的属性:");
        foreach (string attribute in attributeList)
        {
            Debug.Log($"  {attribute}");
        }
    }
    
    /// <summary>
    /// 重置脚本系统设置
    /// </summary>
    public void ResetScriptingSystemSettings()
    {
        // 重置设置
        enableScriptingSystem = true;
        enableReflection = true;
        enableAttributeProcessing = true;
        enableAssemblyLoading = true;
        
        // 重置状态
        isScriptingSystemActive = false;
        showPrivateMembers = false;
        showStaticMembers = true;
        processCustomAttributes = true;
        processBuiltinAttributes = true;
        
        Debug.Log("脚本系统设置已重置");
    }
    
    // 事件处理器
    private void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
    {
        Debug.Log($"程序集已加载: {args.LoadedAssembly.FullName}");
        onAssemblyLoaded?.Invoke(args.LoadedAssembly.FullName);
        
        // 更新程序集列表
        assemblyList.Add(args.LoadedAssembly.FullName);
        loadedAssemblyList.Add(args.LoadedAssembly);
        loadedAssemblies++;
        totalAssemblies++;
    }
    
    private void Update()
    {
        // 更新脚本系统状态
        if (enableScriptingSystem)
        {
            isScriptingSystemActive = true;
        }
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("脚本系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 脚本系统状态
        GUILayout.Label($"脚本系统活跃: {isScriptingSystemActive}");
        GUILayout.Label($"总程序集数: {totalAssemblies}");
        GUILayout.Label($"已加载程序集数: {loadedAssemblies}");
        GUILayout.Label($"总类型数: {totalTypes}");
        GUILayout.Label($"总方法数: {totalMethods}");
        GUILayout.Label($"缓存类型数: {typeCache.Count}");
        GUILayout.Label($"缓存方法数: {methodCache.Count}");
        GUILayout.Label($"发现属性数: {attributeList.Count}");
        
        GUILayout.Space(10);
        
        // 反射操作
        GUILayout.Label("反射操作:", EditorStyles.boldLabel);
        
        targetTypeName = GUILayout.TextField("类型名称", targetTypeName);
        targetMethodName = GUILayout.TextField("方法名称", targetMethodName);
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("获取类型信息"))
        {
            Type type = GetTypeInfo(targetTypeName);
            if (type != null)
            {
                Debug.Log($"类型信息: {type.FullName}");
            }
        }
        if (GUILayout.Button("获取方法信息"))
        {
            MethodInfo method = GetMethodInfo(targetTypeName, targetMethodName);
            if (method != null)
            {
                Debug.Log($"方法信息: {method.Name}");
            }
        }
        GUILayout.EndHorizontal();
        
        if (GUILayout.Button("调用方法"))
        {
            InvokeMethod(targetTypeName, targetMethodName);
        }
        
        if (GUILayout.Button("获取类型成员"))
        {
            List<MemberInfo> members = GetTypeMembers(targetTypeName);
            Debug.Log($"类型 {targetTypeName} 有 {members.Count} 个成员");
        }
        
        if (GUILayout.Button("获取类型属性"))
        {
            List<Attribute> attributes = GetTypeAttributes(targetTypeName);
            Debug.Log($"类型 {targetTypeName} 有 {attributes.Count} 个属性");
        }
        
        GUILayout.Space(10);
        
        // 设置
        GUILayout.Label("设置:", EditorStyles.boldLabel);
        
        showPrivateMembers = GUILayout.Toggle(showPrivateMembers, "显示私有成员");
        showStaticMembers = GUILayout.Toggle(showStaticMembers, "显示静态成员");
        processCustomAttributes = GUILayout.Toggle(processCustomAttributes, "处理自定义属性");
        processBuiltinAttributes = GUILayout.Toggle(processBuiltinAttributes, "处理内置属性");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取脚本系统信息"))
        {
            GetScriptingSystemInfo();
        }
        
        if (GUILayout.Button("重置设置"))
        {
            ResetScriptingSystemSettings();
        }
        
        GUILayout.EndArea();
    }
    
    private void OnDestroy()
    {
        // 移除事件监听器
        AppDomain.CurrentDomain.AssemblyLoad -= OnAssemblyLoaded;
    }
} 