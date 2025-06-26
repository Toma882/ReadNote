using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DotNet.Reflection
{
    /// <summary>
    /// .NET反射API使用示例
    /// 包含类型信息获取、动态方法调用、属性访问等
    /// </summary>
    public class ReflectionExample : MonoBehaviour
    {
        [Header("反射示例")]
        [SerializeField] private bool runExamples = true;

        private void Start()
        {
            if (runExamples)
            {
                RunAllExamples();
            }
        }

        /// <summary>
        /// 运行所有反射示例
        /// </summary>
        private void RunAllExamples()
        {
            Debug.Log("=== .NET反射API示例开始 ===");
            
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
        /// </summary>
        private void TypeInfoExample()
        {
            Debug.Log("--- Type信息示例 ---");
            
            try
            {
                // 获取类型信息
                Type stringType = typeof(string);
                Type intType = typeof(int);
                Type listType = typeof(List<>);
                Type currentType = GetType();
                
                Debug.Log($"字符串类型名称: {stringType.Name}");
                Debug.Log($"字符串类型全名: {stringType.FullName}");
                Debug.Log($"字符串类型命名空间: {stringType.Namespace}");
                Debug.Log($"字符串类型程序集: {stringType.Assembly.GetName().Name}");
                
                // 类型特性
                Debug.Log($"字符串是类: {stringType.IsClass}");
                Debug.Log($"字符串是值类型: {stringType.IsValueType}");
                Debug.Log($"字符串是接口: {stringType.IsInterface}");
                Debug.Log($"字符串是枚举: {stringType.IsEnum}");
                Debug.Log($"字符串是数组: {stringType.IsArray}");
                Debug.Log($"字符串是泛型: {stringType.IsGenericType}");
                
                // 基类和接口
                Type baseType = currentType.BaseType;
                Debug.Log($"当前类型基类: {baseType?.Name}");
                
                Type[] interfaces = currentType.GetInterfaces();
                Debug.Log($"当前类型实现的接口数量: {interfaces.Length}");
                foreach (Type interfaceType in interfaces)
                {
                    Debug.Log($"  接口: {interfaceType.Name}");
                }
                
                // 泛型类型
                Type genericListType = typeof(List<string>);
                Debug.Log($"泛型列表类型: {genericListType.Name}");
                Debug.Log($"泛型类型定义: {genericListType.GetGenericTypeDefinition().Name}");
                
                Type[] genericArguments = genericListType.GetGenericArguments();
                Debug.Log($"泛型参数: {string.Join(", ", genericArguments.Select(t => t.Name))}");
                
                // 类型比较
                Type type1 = typeof(string);
                Type type2 = "Hello".GetType();
                Debug.Log($"类型比较结果: {type1 == type2}");
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Type信息操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// Property信息示例
        /// </summary>
        private void PropertyInfoExample()
        {
            Debug.Log("--- Property信息示例 ---");
            
            try
            {
                // 创建测试对象
                var testObject = new TestClass
                {
                    Name = "测试对象",
                    Age = 25,
                    Email = "test@example.com"
                };
                
                Type objectType = testObject.GetType();
                
                // 获取所有公共属性
                PropertyInfo[] properties = objectType.GetProperties();
                Debug.Log($"公共属性数量: {properties.Length}");
                
                foreach (PropertyInfo property in properties)
                {
                    Debug.Log($"属性: {property.Name}, 类型: {property.PropertyType.Name}");
                    Debug.Log($"  可读: {property.CanRead}, 可写: {property.CanWrite}");
                    
                    // 获取属性值
                    if (property.CanRead)
                    {
                        object value = property.GetValue(testObject);
                        Debug.Log($"  当前值: {value}");
                    }
                }
                
                // 获取特定属性
                PropertyInfo nameProperty = objectType.GetProperty("Name");
                if (nameProperty != null)
                {
                    Debug.Log($"Name属性类型: {nameProperty.PropertyType.Name}");
                    
                    // 设置属性值
                    if (nameProperty.CanWrite)
                    {
                        nameProperty.SetValue(testObject, "新名称");
                        Debug.Log($"设置后的Name值: {nameProperty.GetValue(testObject)}");
                    }
                }
                
                // 获取私有属性
                PropertyInfo[] allProperties = objectType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Debug.Log($"所有属性数量（包括私有）: {allProperties.Length}");
                
                // 属性特性
                foreach (PropertyInfo property in properties)
                {
                    var attributes = property.GetCustomAttributes(true);
                    if (attributes.Length > 0)
                    {
                        Debug.Log($"属性 {property.Name} 的特性:");
                        foreach (var attribute in attributes)
                        {
                            Debug.Log($"  {attribute.GetType().Name}");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Property信息操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// Method信息示例
        /// </summary>
        private void MethodInfoExample()
        {
            Debug.Log("--- Method信息示例 ---");
            
            try
            {
                // 创建测试对象
                var testObject = new TestClass
                {
                    Name = "方法测试对象",
                    Age = 30
                };
                
                Type objectType = testObject.GetType();
                
                // 获取所有公共方法
                MethodInfo[] methods = objectType.GetMethods();
                Debug.Log($"公共方法数量: {methods.Length}");
                
                foreach (MethodInfo method in methods)
                {
                    Debug.Log($"方法: {method.Name}");
                    Debug.Log($"  返回类型: {method.ReturnType.Name}");
                    Debug.Log($"  参数数量: {method.GetParameters().Length}");
                    
                    ParameterInfo[] parameters = method.GetParameters();
                    foreach (ParameterInfo parameter in parameters)
                    {
                        Debug.Log($"    参数: {parameter.Name}, 类型: {parameter.ParameterType.Name}");
                    }
                }
                
                // 获取特定方法
                MethodInfo calculateMethod = objectType.GetMethod("Calculate");
                if (calculateMethod != null)
                {
                    Debug.Log($"找到Calculate方法，参数数量: {calculateMethod.GetParameters().Length}");
                    
                    // 调用方法
                    object result = calculateMethod.Invoke(testObject, new object[] { 10, 5 });
                    Debug.Log($"Calculate(10, 5) 结果: {result}");
                }
                
                // 获取重载方法
                MethodInfo[] overloadedMethods = objectType.GetMethods()
                    .Where(m => m.Name == "Process")
                    .ToArray();
                
                Debug.Log($"Process方法重载数量: {overloadedMethods.Length}");
                foreach (MethodInfo method in overloadedMethods)
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    string paramList = string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));
                    Debug.Log($"  Process({paramList})");
                }
                
                // 调用重载方法
                if (overloadedMethods.Length > 0)
                {
                    // 调用无参数版本
                    MethodInfo noParamMethod = overloadedMethods.FirstOrDefault(m => m.GetParameters().Length == 0);
                    if (noParamMethod != null)
                    {
                        noParamMethod.Invoke(testObject, null);
                    }
                    
                    // 调用带参数版本
                    MethodInfo paramMethod = overloadedMethods.FirstOrDefault(m => m.GetParameters().Length > 0);
                    if (paramMethod != null)
                    {
                        paramMethod.Invoke(testObject, new object[] { "测试参数" });
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Method信息操作出错: {ex.Message}");
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