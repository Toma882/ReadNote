// ReflectionExample.cs
// .NET反射API使用详解示例
// 包含类型信息获取、动态方法调用、属性访问、字段操作、特性处理等
// 每个方法、关键步骤、枚举值均有详细中文注释
// 适合.NET初学者学习和查阅
// 
// 主要功能模块：
// 1. Type信息 - 类型元数据获取和分析
// 2. Property信息 - 属性反射和动态访问
// 3. Method信息 - 方法反射和动态调用
// 4. Field信息 - 字段反射和动态访问
// 5. Attribute处理 - 特性获取和自定义特性
// 6. 动态调用 - 运行时方法调用和对象创建
// 7. Assembly操作 - 程序集信息和类型加载

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DotNet.Reflection
{
    /// <summary>
    /// .NET反射API使用详解示例
    /// 包含类型信息获取、动态方法调用、属性访问、字段操作、特性处理等
    /// 
    /// 重要说明：
    /// - 反射允许在运行时检查和操作类型信息
    /// - 性能考虑：反射操作比直接调用慢，应谨慎使用
    /// - 安全性：反射可以访问私有成员，需要权限控制
    /// - 跨平台注意：反射功能在不同平台基本一致
    /// - 适用场景：插件系统、序列化、依赖注入等
    /// </summary>
    public class ReflectionExample : MonoBehaviour
    {
        [Header("反射示例配置")]
        [Tooltip("是否自动运行所有示例代码")]
        [SerializeField] private bool runExamples = true;
        [Tooltip("是否显示详细的反射信息")]
        [SerializeField] private bool showDetailedInfo = true;
        [Tooltip("反射操作超时时间（毫秒）")]
        [SerializeField] private int reflectionTimeoutMs = 5000;

        private void Start()
        {
            if (runExamples)
            {
                RunAllExamples();
            }
        }

        /// <summary>
        /// 运行所有反射相关示例
        /// 按顺序执行：Type信息 -> Property信息 -> Method信息 -> Field信息 -> 特性处理 -> 动态调用 -> 程序集操作
        /// 
        /// 执行流程：
        /// 1. 基础类型信息获取和分析
        /// 2. 属性反射和动态访问
        /// 3. 方法反射和动态调用
        /// 4. 字段反射和动态访问
        /// 5. 特性获取和自定义特性
        /// 6. 运行时动态调用
        /// 7. 程序集信息和类型加载
        /// </summary>
        private void RunAllExamples()
        {
            Debug.Log("=== .NET反射API示例开始 ===");
            Debug.Log($"详细信息显示: {showDetailedInfo}");
            Debug.Log($"反射超时时间: {reflectionTimeoutMs}毫秒");
            
            TypeInfoExample();
            PropertyInfoExample();
            MethodInfoExample();
            FieldInfoExample();
            AttributeExample();
            DynamicInvocationExample();
            AssemblyExample();
            
            Debug.Log("=== .NET反射API示例结束 ===");
        }

        /// <summary>
        /// Type信息示例
        /// 演示如何获取和分析类型的元数据信息
        /// 
        /// 主要功能：
        /// - 获取类型基本信息（名称、命名空间、程序集）
        /// - 分析类型特性（类、值类型、接口、枚举等）
        /// - 获取基类和接口信息
        /// - 处理泛型类型
        /// - 类型比较和兼容性检查
        /// 
        /// 注意事项：
        /// - typeof()在编译时获取类型，GetType()在运行时获取
        /// - 泛型类型需要区分开放类型和封闭类型
        /// - 类型比较要考虑继承关系
        /// </summary>
        private void TypeInfoExample()
        {
            Debug.Log("--- Type信息示例 ---");
            
            try
            {
                // ========== 获取类型信息 ==========
                
                // typeof操作符：编译时获取类型
                // 参数说明：T - 类型参数
                // 返回值：Type - 类型对象
                // 注意事项：编译时确定，性能最好
                Type stringType = typeof(string);
                Type intType = typeof(int);
                Type listType = typeof(List<>);
                Type currentType = GetType();
                
                // GetType()方法：运行时获取类型
                // 参数说明：无
                // 返回值：Type - 当前对象的类型
                // 注意事项：运行时确定，需要对象实例
                Type runtimeStringType = "Hello".GetType();
                
                Debug.Log($"字符串类型名称: {stringType.Name}");
                Debug.Log($"字符串类型全名: {stringType.FullName}");
                Debug.Log($"字符串类型命名空间: {stringType.Namespace}");
                Debug.Log($"字符串类型程序集: {stringType.Assembly.GetName().Name}");
                Debug.Log($"字符串类型模块: {stringType.Module.Name}");
                
                // ========== 类型特性分析 ==========
                
                // 检查类型的基本特性
                // 参数说明：无
                // 返回值：bool - 是否具有指定特性
                Debug.Log($"字符串是类: {stringType.IsClass}");
                Debug.Log($"字符串是值类型: {stringType.IsValueType}");
                Debug.Log($"字符串是接口: {stringType.IsInterface}");
                Debug.Log($"字符串是枚举: {stringType.IsEnum}");
                Debug.Log($"字符串是数组: {stringType.IsArray}");
                Debug.Log($"字符串是泛型: {stringType.IsGenericType}");
                Debug.Log($"字符串是抽象类: {stringType.IsAbstract}");
                Debug.Log($"字符串是密封类: {stringType.IsSealed}");
                Debug.Log($"字符串是静态类: {stringType.IsStatic()}");
                
                // ========== 基类和接口信息 ==========
                
                // 获取基类信息
                // 参数说明：无
                // 返回值：Type - 基类类型，如果没有则为null
                Type baseType = currentType.BaseType;
                Debug.Log($"当前类型基类: {baseType?.Name}");
                Debug.Log($"当前类型基类全名: {baseType?.FullName}");
                
                // 获取实现的接口
                // 参数说明：无
                // 返回值：Type[] - 接口类型数组
                Type[] interfaces = currentType.GetInterfaces();
                Debug.Log($"当前类型实现的接口数量: {interfaces.Length}");
                foreach (Type interfaceType in interfaces)
                {
                    Debug.Log($"  接口: {interfaceType.Name} ({interfaceType.FullName})");
                }
                
                // ========== 泛型类型处理 ==========
                
                // 开放泛型类型（未指定类型参数）
                Debug.Log($"开放泛型列表类型: {listType.Name}");
                Debug.Log($"开放泛型类型定义: {listType.GetGenericTypeDefinition().Name}");
                
                // 封闭泛型类型（已指定类型参数）
                Type genericListType = typeof(List<string>);
                Debug.Log($"封闭泛型列表类型: {genericListType.Name}");
                Debug.Log($"封闭泛型类型定义: {genericListType.GetGenericTypeDefinition().Name}");
                
                // 获取泛型参数
                // 参数说明：无
                // 返回值：Type[] - 泛型参数类型数组
                Type[] genericArguments = genericListType.GetGenericArguments();
                Debug.Log($"泛型参数: {string.Join(", ", genericArguments.Select(t => t.Name))}");
                
                // 检查是否为泛型类型
                Debug.Log($"List<>是泛型类型: {listType.IsGenericType}");
                Debug.Log($"List<string>是泛型类型: {genericListType.IsGenericType}");
                Debug.Log($"List<>是泛型类型定义: {listType.IsGenericTypeDefinition}");
                Debug.Log($"List<string>是泛型类型定义: {genericListType.IsGenericTypeDefinition}");
                
                // ========== 类型比较 ==========
                
                // 直接类型比较
                Type type1 = typeof(string);
                Type type2 = "Hello".GetType();
                Debug.Log($"类型比较结果: {type1 == type2}");
                Debug.Log($"类型引用相等: {ReferenceEquals(type1, type2)}");
                
                // 类型兼容性检查
                // 参数说明：type - 要检查的类型
                // 返回值：bool - 是否兼容
                Debug.Log($"string是否可分配给object: {typeof(object).IsAssignableFrom(typeof(string))}");
                Debug.Log($"int是否可分配给object: {typeof(object).IsAssignableFrom(typeof(int))}");
                Debug.Log($"string是否可分配给int: {typeof(int).IsAssignableFrom(typeof(string))}");
                
                // ========== 数组类型处理 ==========
                
                // 获取数组类型信息
                Type arrayType = typeof(int[]);
                Debug.Log($"数组类型: {arrayType.Name}");
                Debug.Log($"数组元素类型: {arrayType.GetElementType().Name}");
                Debug.Log($"数组维度: {arrayType.GetArrayRank()}");
                
                // 多维数组
                Type multiArrayType = typeof(int[,]);
                Debug.Log($"多维数组类型: {multiArrayType.Name}");
                Debug.Log($"多维数组元素类型: {multiArrayType.GetElementType().Name}");
                Debug.Log($"多维数组维度: {multiArrayType.GetArrayRank()}");
                
                // ========== 嵌套类型处理 ==========
                
                // 获取嵌套类型
                // 参数说明：name - 嵌套类型名称, bindingAttr - 绑定标志
                // 返回值：Type - 嵌套类型，如果不存在则为null
                Type[] nestedTypes = currentType.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);
                Debug.Log($"嵌套类型数量: {nestedTypes.Length}");
                foreach (Type nestedType in nestedTypes)
                {
                    Debug.Log($"  嵌套类型: {nestedType.Name} ({(nestedType.IsPublic ? "公共" : "私有")})");
                }
                
                // ========== 类型成员统计 ==========
                
                if (showDetailedInfo)
                {
                    // 获取所有成员信息
                    MemberInfo[] members = currentType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    Debug.Log($"当前类型成员总数: {members.Length}");
                    
                    var memberTypes = members.GroupBy(m => m.MemberType).ToDictionary(g => g.Key, g => g.Count());
                    foreach (var memberType in memberTypes)
                    {
                        Debug.Log($"  {memberType.Key}: {memberType.Value}个");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Type信息操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// Property信息示例
        /// 演示如何通过反射获取和操作属性
        /// 
        /// 主要功能：
        /// - 获取类型的所有属性
        /// - 动态读取和设置属性值
        /// - 分析属性特性（可读、可写、索引器等）
        /// - 处理私有和静态属性
        /// - 获取属性上的特性
        /// 
        /// 注意事项：
        /// - 属性访问比字段访问慢
        /// - 需要检查CanRead和CanWrite
        /// - 索引器属性需要参数
        /// - 静态属性不需要实例
        /// </summary>
        private void PropertyInfoExample()
        {
            Debug.Log("--- Property信息示例 ---");
            
            try
            {
                // ========== 创建测试对象 ==========
                
                // 创建测试对象实例
                var testObject = new TestClass
                {
                    Name = "测试对象",
                    Age = 25,
                    Email = "test@example.com"
                };
                
                // 获取对象类型
                Type objectType = testObject.GetType();
                Debug.Log($"测试对象类型: {objectType.Name}");
                
                // ========== 获取公共属性 ==========
                
                // 获取所有公共实例属性
                // 参数说明：bindingAttr - 绑定标志
                // 返回值：PropertyInfo[] - 属性信息数组
                PropertyInfo[] properties = objectType.GetProperties();
                Debug.Log($"公共属性数量: {properties.Length}");
                
                foreach (PropertyInfo property in properties)
                {
                    Debug.Log($"属性: {property.Name}");
                    Debug.Log($"  类型: {property.PropertyType.Name}");
                    Debug.Log($"  可读: {property.CanRead}, 可写: {property.CanWrite}");
                    Debug.Log($"  声明类型: {property.DeclaringType?.Name}");
                    Debug.Log($"  反射类型: {property.ReflectedType?.Name}");
                    
                    // 获取属性值
                    if (property.CanRead)
                    {
                        try
                        {
                            // GetValue: 获取属性值
                            // 参数说明：obj - 对象实例, index - 索引参数（可选）
                            // 返回值：object - 属性值
                            object value = property.GetValue(testObject);
                            Debug.Log($"  当前值: {value ?? "null"}");
                        }
                        catch (Exception ex)
                        {
                            Debug.LogWarning($"  获取属性值失败: {ex.Message}");
                        }
                    }
                }
                
                // ========== 获取特定属性 ==========
                
                // 获取特定名称的属性
                // 参数说明：name - 属性名称, bindingAttr - 绑定标志
                // 返回值：PropertyInfo - 属性信息，如果不存在则为null
                PropertyInfo nameProperty = objectType.GetProperty("Name");
                if (nameProperty != null)
                {
                    Debug.Log($"找到Name属性: {nameProperty.PropertyType.Name}");
                    
                    // 设置属性值
                    if (nameProperty.CanWrite)
                    {
                        // SetValue: 设置属性值
                        // 参数说明：obj - 对象实例, value - 新值, index - 索引参数（可选）
                        // 返回值：void
                        nameProperty.SetValue(testObject, "新名称");
                        Debug.Log($"设置后的Name值: {nameProperty.GetValue(testObject)}");
                    }
                }
                
                // ========== 获取所有属性（包括私有） ==========
                
                // 使用BindingFlags获取所有属性
                // BindingFlags枚举值：
                // - Public: 公共成员
                // - NonPublic: 非公共成员
                // - Instance: 实例成员
                // - Static: 静态成员
                // - DeclaredOnly: 仅在声明类型中查找
                PropertyInfo[] allProperties = objectType.GetProperties(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                Debug.Log($"所有属性数量（包括私有和静态）: {allProperties.Length}");
                
                foreach (PropertyInfo property in allProperties)
                {
                    string accessModifier = property.GetMethod?.IsPublic == true ? "公共" : "私有";
                    string memberType = property.GetMethod?.IsStatic == true ? "静态" : "实例";
                    Debug.Log($"  {property.Name} ({accessModifier}{memberType})");
                }
                
                // ========== 属性特性处理 ==========
                
                // 获取属性上的特性
                foreach (PropertyInfo property in properties)
                {
                    // GetCustomAttributes: 获取自定义特性
                    // 参数说明：inherit - 是否继承, attributeType - 特性类型（可选）
                    // 返回值：object[] - 特性数组
                    var attributes = property.GetCustomAttributes(true);
                    if (attributes.Length > 0)
                    {
                        Debug.Log($"属性 {property.Name} 的特性:");
                        foreach (var attribute in attributes)
                        {
                            Debug.Log($"  {attribute.GetType().Name}");
                            
                            // 获取特性属性
                            Type attributeType = attribute.GetType();
                            PropertyInfo[] attributeProperties = attributeType.GetProperties();
                            foreach (PropertyInfo attrProp in attributeProperties)
                            {
                                if (attrProp.CanRead)
                                {
                                    object attrValue = attrProp.GetValue(attribute);
                                    Debug.Log($"    {attrProp.Name}: {attrValue}");
                                }
                            }
                        }
                    }
                }
                
                // ========== 索引器属性处理 ==========
                
                // 检查是否有索引器属性
                PropertyInfo[] indexers = objectType.GetProperties()
                    .Where(p => p.GetIndexParameters().Length > 0)
                    .ToArray();
                
                Debug.Log($"索引器属性数量: {indexers.Length}");
                foreach (PropertyInfo indexer in indexers)
                {
                    Debug.Log($"索引器: {indexer.Name}");
                    ParameterInfo[] parameters = indexer.GetIndexParameters();
                    Debug.Log($"  参数数量: {parameters.Length}");
                    foreach (ParameterInfo param in parameters)
                    {
                        Debug.Log($"    参数: {param.Name} ({param.ParameterType.Name})");
                    }
                }
                
                // ========== 静态属性处理 ==========
                
                // 获取静态属性
                PropertyInfo[] staticProperties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Static);
                Debug.Log($"静态属性数量: {staticProperties.Length}");
                
                foreach (PropertyInfo staticProp in staticProperties)
                {
                    Debug.Log($"静态属性: {staticProp.Name}");
                    if (staticProp.CanRead)
                    {
                        // 静态属性不需要实例，传入null
                        object staticValue = staticProp.GetValue(null);
                        Debug.Log($"  值: {staticValue}");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Property信息操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// Method信息示例
        /// 演示如何通过反射获取和分析方法信息
        /// 
        /// 主要功能：
        /// - 获取类型的所有方法
        /// - 分析方法的参数和返回值
        /// - 动态调用方法
        /// - 处理构造函数
        /// - 获取方法特性
        /// 
        /// 注意事项：
        /// - 方法调用比直接调用慢很多
        /// - 需要正确处理参数类型和数量
        /// - 构造函数是特殊的方法
        /// - 泛型方法需要特殊处理
        /// </summary>
        private void MethodInfoExample()
        {
            Debug.Log("--- Method信息示例 ---");
            
            try
            {
                // ========== 创建测试对象 ==========
                
                var testObject = new TestClass
                {
                    Name = "方法测试对象",
                    Age = 30
                };
                
                Type objectType = testObject.GetType();
                
                // ========== 获取公共方法 ==========
                
                // 获取所有公共方法
                // 参数说明：bindingAttr - 绑定标志
                // 返回值：MethodInfo[] - 方法信息数组
                MethodInfo[] methods = objectType.GetMethods();
                Debug.Log($"公共方法数量: {methods.Length}");
                
                foreach (MethodInfo method in methods)
                {
                    Debug.Log($"方法: {method.Name}");
                    Debug.Log($"  返回类型: {method.ReturnType.Name}");
                    Debug.Log($"  声明类型: {method.DeclaringType?.Name}");
                    Debug.Log($"  是静态: {method.IsStatic}");
                    Debug.Log($"  是抽象: {method.IsAbstract}");
                    Debug.Log($"  是虚方法: {method.IsVirtual}");
                    Debug.Log($"  是构造函数: {method.IsConstructor}");
                    
                    // 获取方法参数
                    ParameterInfo[] parameters = method.GetParameters();
                    Debug.Log($"  参数数量: {parameters.Length}");
                    foreach (ParameterInfo param in parameters)
                    {
                        string paramInfo = $"{param.ParameterType.Name} {param.Name}";
                        if (param.HasDefaultValue)
                        {
                            paramInfo += $" = {param.DefaultValue}";
                        }
                        if (param.IsOut)
                        {
                            paramInfo += " [out]";
                        }
                        if (param.IsIn)
                        {
                            paramInfo += " [in]";
                        }
                        Debug.Log($"    参数: {paramInfo}");
                    }
                }
                
                // ========== 获取特定方法 ==========
                
                // 获取特定名称的方法
                // 参数说明：name - 方法名称, bindingAttr - 绑定标志, binder - 绑定器, types - 参数类型数组, modifiers - 参数修饰符
                // 返回值：MethodInfo - 方法信息，如果不存在则为null
                MethodInfo calculateMethod = objectType.GetMethod("Calculate", new Type[] { typeof(int), typeof(int) });
                if (calculateMethod != null)
                {
                    Debug.Log($"找到Calculate方法: {calculateMethod.Name}");
                    
                    // 动态调用方法
                    // 参数说明：obj - 对象实例, parameters - 参数数组
                    // 返回值：object - 方法返回值
                    object result = calculateMethod.Invoke(testObject, new object[] { 10, 20 });
                    Debug.Log($"Calculate(10, 20) = {result}");
                }
                
                // ========== 构造函数处理 ==========
                
                // 获取构造函数
                // 参数说明：bindingAttr - 绑定标志, binder - 绑定器, types - 参数类型数组, modifiers - 参数修饰符
                // 返回值：ConstructorInfo[] - 构造函数信息数组
                ConstructorInfo[] constructors = objectType.GetConstructors();
                Debug.Log($"构造函数数量: {constructors.Length}");
                
                foreach (ConstructorInfo constructor in constructors)
                {
                    Debug.Log($"构造函数: {constructor.Name}");
                    Debug.Log($"  是公共: {constructor.IsPublic}");
                    Debug.Log($"  是静态: {constructor.IsStatic}");
                    
                    ParameterInfo[] ctorParams = constructor.GetParameters();
                    Debug.Log($"  参数数量: {ctorParams.Length}");
                    foreach (ParameterInfo param in ctorParams)
                    {
                        Debug.Log($"    参数: {param.ParameterType.Name} {param.Name}");
                    }
                }
                
                // 动态创建对象
                ConstructorInfo parameterizedCtor = objectType.GetConstructor(new Type[] { typeof(string), typeof(int) });
                if (parameterizedCtor != null)
                {
                    // 参数说明：parameters - 参数数组
                    // 返回值：object - 创建的对象实例
                    object newInstance = parameterizedCtor.Invoke(new object[] { "动态创建的对象", 42 });
                    Debug.Log($"动态创建的对象: {newInstance}");
                }
                
                // ========== 泛型方法处理 ==========
                
                // 检查泛型方法
                MethodInfo[] genericMethods = methods.Where(m => m.IsGenericMethod).ToArray();
                Debug.Log($"泛型方法数量: {genericMethods.Length}");
                
                foreach (MethodInfo genericMethod in genericMethods)
                {
                    Debug.Log($"泛型方法: {genericMethod.Name}");
                    Type[] genericParams = genericMethod.GetGenericArguments();
                    Debug.Log($"  泛型参数: {string.Join(", ", genericParams.Select(t => t.Name))}");
                }
                
                // ========== 方法重载处理 ==========
                
                // 获取重载方法
                MethodInfo[] overloadedMethods = methods.Where(m => m.Name == "Process").ToArray();
                Debug.Log($"Process方法重载数量: {overloadedMethods.Length}");
                
                foreach (MethodInfo overload in overloadedMethods)
                {
                    ParameterInfo[] overloadParams = overload.GetParameters();
                    string paramList = string.Join(", ", overloadParams.Select(p => p.ParameterType.Name));
                    Debug.Log($"  Process({paramList})");
                }
                
                // ========== 方法特性处理 ==========
                
                foreach (MethodInfo method in methods)
                {
                    var attributes = method.GetCustomAttributes(true);
                    if (attributes.Length > 0)
                    {
                        Debug.Log($"方法 {method.Name} 的特性:");
                        foreach (var attribute in attributes)
                        {
                            Debug.Log($"  {attribute.GetType().Name}");
                        }
                    }
                }
                
                // ========== 性能测试 ==========
                
                if (showDetailedInfo)
                {
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    for (int i = 0; i < 1000; i++)
                    {
                        calculateMethod?.Invoke(testObject, new object[] { i, i + 1 });
                    }
                    stopwatch.Stop();
                    Debug.Log($"1000次反射方法调用耗时: {stopwatch.ElapsedMilliseconds}ms");
                    
                    // 直接调用对比
                    stopwatch.Restart();
                    for (int i = 0; i < 1000; i++)
                    {
                        testObject.Calculate(i, i + 1);
                    }
                    stopwatch.Stop();
                    Debug.Log($"1000次直接方法调用耗时: {stopwatch.ElapsedMilliseconds}ms");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Method信息操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// Field信息示例
        /// </summary>
        private void FieldInfoExample()
        {
            Debug.Log("--- Field信息示例 ---");
            
            try
            {
                // 创建测试对象
                var testObject = new TestClass
                {
                    Name = "字段测试对象"
                };
                
                Type objectType = testObject.GetType();
                
                // 获取所有字段（包括私有）
                FieldInfo[] allFields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Debug.Log($"所有字段数量: {allFields.Length}");
                
                foreach (FieldInfo field in allFields)
                {
                    Debug.Log($"字段: {field.Name}, 类型: {field.FieldType.Name}");
                    Debug.Log($"  访问修饰符: {(field.IsPublic ? "public" : "private")}");
                    Debug.Log($"  是静态: {field.IsStatic}");
                    Debug.Log($"  是只读: {field.IsInitOnly}");
                    
                    // 获取字段值
                    object value = field.GetValue(testObject);
                    Debug.Log($"  当前值: {value}");
                }
                
                // 获取公共字段
                FieldInfo[] publicFields = objectType.GetFields();
                Debug.Log($"公共字段数量: {publicFields.Length}");
                
                // 设置字段值
                FieldInfo privateField = allFields.FirstOrDefault(f => f.Name == "privateField");
                if (privateField != null)
                {
                    Debug.Log($"设置私有字段 {privateField.Name} 的值");
                    privateField.SetValue(testObject, "通过反射设置的私有字段值");
                    
                    object newValue = privateField.GetValue(testObject);
                    Debug.Log($"设置后的值: {newValue}");
                }
                
                // 静态字段
                FieldInfo[] staticFields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                Debug.Log($"静态字段数量: {staticFields.Length}");
                
                foreach (FieldInfo field in staticFields)
                {
                    Debug.Log($"静态字段: {field.Name}");
                    object value = field.GetValue(null); // 静态字段不需要实例
                    Debug.Log($"  值: {value}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Field信息操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 特性示例
        /// </summary>
        private void AttributeExample()
        {
            Debug.Log("--- 特性示例 ---");
            
            try
            {
                // 获取类型上的特性
                Type currentType = GetType();
                var typeAttributes = currentType.GetCustomAttributes(true);
                Debug.Log($"当前类型特性数量: {typeAttributes.Length}");
                
                foreach (var attribute in typeAttributes)
                {
                    Debug.Log($"类型特性: {attribute.GetType().Name}");
                }
                
                // 获取方法上的特性
                MethodInfo[] methods = currentType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    var methodAttributes = method.GetCustomAttributes(true);
                    if (methodAttributes.Length > 0)
                    {
                        Debug.Log($"方法 {method.Name} 的特性:");
                        foreach (var attribute in methodAttributes)
                        {
                            Debug.Log($"  {attribute.GetType().Name}");
                        }
                    }
                }
                
                // 获取属性上的特性
                PropertyInfo[] properties = currentType.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    var propertyAttributes = property.GetCustomAttributes(true);
                    if (propertyAttributes.Length > 0)
                    {
                        Debug.Log($"属性 {property.Name} 的特性:");
                        foreach (var attribute in propertyAttributes)
                        {
                            Debug.Log($"  {attribute.GetType().Name}");
                        }
                    }
                }
                
                // 检查特定特性
                bool hasHeaderAttribute = currentType.GetCustomAttribute<HeaderAttribute>() != null;
                Debug.Log($"当前类型是否有Header特性: {hasHeaderAttribute}");
                
                // 获取特性参数
                var headerAttribute = currentType.GetCustomAttribute<HeaderAttribute>();
                if (headerAttribute != null)
                {
                    Debug.Log($"Header特性值: {headerAttribute.header}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"特性操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 动态调用示例
        /// </summary>
        private void DynamicInvocationExample()
        {
            Debug.Log("--- 动态调用示例 ---");
            
            try
            {
                // 动态创建对象
                Type testClassType = typeof(TestClass);
                object testObject = Activator.CreateInstance(testClassType);
                Debug.Log($"动态创建的对象类型: {testObject.GetType().Name}");
                
                // 动态设置属性
                PropertyInfo nameProperty = testClassType.GetProperty("Name");
                nameProperty?.SetValue(testObject, "动态设置的名字");
                
                PropertyInfo ageProperty = testClassType.GetProperty("Age");
                ageProperty?.SetValue(testObject, 35);
                
                // 动态获取属性值
                string name = (string)nameProperty?.GetValue(testObject);
                int age = (int)ageProperty?.GetValue(testObject);
                Debug.Log($"动态获取的值: {name}, {age}岁");
                
                // 动态调用方法
                MethodInfo calculateMethod = testClassType.GetMethod("Calculate");
                if (calculateMethod != null)
                {
                    object result = calculateMethod.Invoke(testObject, new object[] { 20, 10 });
                    Debug.Log($"动态调用Calculate(20, 10)结果: {result}");
                }
                
                // 动态调用构造函数
                ConstructorInfo[] constructors = testClassType.GetConstructors();
                Debug.Log($"构造函数数量: {constructors.Length}");
                
                foreach (ConstructorInfo constructor in constructors)
                {
                    ParameterInfo[] parameters = constructor.GetParameters();
                    string paramList = string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));
                    Debug.Log($"构造函数: {paramList}");
                }
                
                // 调用带参数的构造函数
                ConstructorInfo paramConstructor = testClassType.GetConstructor(new Type[] { typeof(string), typeof(int) });
                if (paramConstructor != null)
                {
                    object paramObject = paramConstructor.Invoke(new object[] { "构造函数参数", 42 });
                    Debug.Log($"通过构造函数创建的对象: {paramObject}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"动态调用操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// Assembly示例
        /// </summary>
        private void AssemblyExample()
        {
            Debug.Log("--- Assembly示例 ---");
            
            try
            {
                // 获取当前程序集
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Debug.Log($"当前程序集名称: {currentAssembly.GetName().Name}");
                Debug.Log($"当前程序集版本: {currentAssembly.GetName().Version}");
                Debug.Log($"当前程序集位置: {currentAssembly.Location}");
                
                // 获取程序集中的所有类型
                Type[] types = currentAssembly.GetTypes();
                Debug.Log($"程序集中的类型数量: {types.Length}");
                
                // 查找特定类型
                Type testClassType = currentAssembly.GetType("DotNet.Reflection.TestClass");
                if (testClassType != null)
                {
                    Debug.Log($"找到TestClass类型: {testClassType.FullName}");
                }
                
                // 获取程序集中的所有程序集
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Debug.Log($"当前域中的程序集数量: {assemblies.Length}");
                
                // 查找包含特定类型的程序集
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly.GetTypes().Any(t => t.Name == "String"))
                    {
                        Debug.Log($"包含String类型的程序集: {assembly.GetName().Name}");
                        break;
                    }
                }
                
                // 动态加载程序集
                try
                {
                    Assembly loadedAssembly = Assembly.Load("System.Core");
                    Debug.Log($"动态加载的程序集: {loadedAssembly.GetName().Name}");
                }
                catch (Exception ex)
                {
                    Debug.Log($"动态加载程序集失败: {ex.Message}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Assembly操作出错: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 测试类
    /// </summary>
    [Serializable]
    public class TestClass
    {
        [Header("测试属性")]
        public string Name { get; set; }
        
        [Range(0, 100)]
        public int Age { get; set; }
        
        public string Email { get; set; }
        
        private string privateField = "私有字段";
        
        public static string staticField = "静态字段";
        
        public TestClass()
        {
            Debug.Log("TestClass默认构造函数被调用");
        }
        
        public TestClass(string name, int age)
        {
            Name = name;
            Age = age;
            Debug.Log($"TestClass带参数构造函数被调用: {name}, {age}");
        }
        
        public int Calculate(int a, int b)
        {
            Debug.Log($"Calculate方法被调用: {a} + {b}");
            return a + b;
        }
        
        public void Process()
        {
            Debug.Log("Process方法被调用（无参数）");
        }
        
        public void Process(string message)
        {
            Debug.Log($"Process方法被调用（有参数）: {message}");
        }
        
        public override string ToString()
        {
            return $"TestClass: {Name}, {Age}岁";
        }
    }
} 