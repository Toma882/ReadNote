# .NET 反射 API 参考文档

本文档基于 `ReflectionExample.cs` 文件，详细介绍了 .NET 反射相关的所有常用 API。

## 目录
- [Type信息](#type信息)
- [Property信息](#property信息)
- [Method信息](#method信息)
- [Field信息](#field信息)
- [Constructor信息](#constructor信息)
- [Attribute处理](#attribute处理)
- [动态调用](#动态调用)
- [Assembly操作](#assembly操作)
- [泛型处理](#泛型处理)

---

## Type信息

### 类型获取操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `typeof()` | 编译时获取类型 |
| `GetType()` | 运行时获取对象类型 |
| `Type.GetType()` | 通过类型名称获取类型 |
| `Assembly.GetType()` | 从程序集获取类型 |

### 类型属性操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Type.Name` | 获取类型名称 |
| `Type.FullName` | 获取完整类型名称 |
| `Type.Namespace` | 获取命名空间 |
| `Type.Assembly` | 获取程序集 |
| `Type.Module` | 获取模块 |
| `Type.BaseType` | 获取基类 |
| `Type.IsClass` | 检查是否为类 |
| `Type.IsValueType` | 检查是否为值类型 |
| `Type.IsInterface` | 检查是否为接口 |
| `Type.IsEnum` | 检查是否为枚举 |
| `Type.IsArray` | 检查是否为数组 |
| `Type.IsGenericType` | 检查是否为泛型类型 |

### 类型关系操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Type.GetInterfaces()` | 获取实现的接口 |
| `Type.IsAssignableFrom()` | 检查类型兼容性 |
| `Type.IsInstanceOfType()` | 检查对象是否为实例 |
| `Type.GetNestedTypes()` | 获取嵌套类型 |

---

## Property信息

### Property获取操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Type.GetProperties()` | 获取所有公共属性 |
| `Type.GetProperty()` | 获取指定名称的属性 |
| `Type.GetProperties(BindingFlags)` | 获取指定绑定标志的属性 |

### Property属性操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `PropertyInfo.Name` | 获取属性名称 |
| `PropertyInfo.PropertyType` | 获取属性类型 |
| `PropertyInfo.CanRead` | 检查是否可读 |
| `PropertyInfo.CanWrite` | 检查是否可写 |
| `PropertyInfo.DeclaringType` | 获取声明类型 |
| `PropertyInfo.ReflectedType` | 获取反射类型 |

### Property访问操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `PropertyInfo.GetValue()` | 获取属性值 |
| `PropertyInfo.SetValue()` | 设置属性值 |
| `PropertyInfo.GetGetMethod()` | 获取get方法 |
| `PropertyInfo.GetSetMethod()` | 获取set方法 |

---

## Method信息

### Method获取操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Type.GetMethods()` | 获取所有公共方法 |
| `Type.GetMethod()` | 获取指定名称的方法 |
| `Type.GetMethods(BindingFlags)` | 获取指定绑定标志的方法 |

### Method属性操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `MethodInfo.Name` | 获取方法名称 |
| `MethodInfo.ReturnType` | 获取返回类型 |
| `MethodInfo.DeclaringType` | 获取声明类型 |
| `MethodInfo.IsStatic` | 检查是否为静态方法 |
| `MethodInfo.IsVirtual` | 检查是否为虚方法 |
| `MethodInfo.IsAbstract` | 检查是否为抽象方法 |
| `MethodInfo.IsConstructor` | 检查是否为构造函数 |

### Method参数操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `MethodInfo.GetParameters()` | 获取方法参数 |
| `ParameterInfo.Name` | 获取参数名称 |
| `ParameterInfo.ParameterType` | 获取参数类型 |
| `ParameterInfo.HasDefaultValue` | 检查是否有默认值 |
| `ParameterInfo.DefaultValue` | 获取默认值 |

### Method调用操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `MethodInfo.Invoke()` | 调用方法 |
| `MethodInfo.CreateDelegate()` | 创建委托 |
| `MethodInfo.GetGenericArguments()` | 获取泛型参数 |

---

## Field信息

### Field获取操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Type.GetFields()` | 获取所有公共字段 |
| `Type.GetField()` | 获取指定名称的字段 |
| `Type.GetFields(BindingFlags)` | 获取指定绑定标志的字段 |

### Field属性操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FieldInfo.Name` | 获取字段名称 |
| `FieldInfo.FieldType` | 获取字段类型 |
| `FieldInfo.DeclaringType` | 获取声明类型 |
| `FieldInfo.IsStatic` | 检查是否为静态字段 |
| `FieldInfo.IsLiteral` | 检查是否为常量 |
| `FieldInfo.IsInitOnly` | 检查是否为只读字段 |

### Field访问操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FieldInfo.GetValue()` | 获取字段值 |
| `FieldInfo.SetValue()` | 设置字段值 |
| `FieldInfo.GetRawConstantValue()` | 获取原始常量值 |

---

## Constructor信息

### Constructor获取操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Type.GetConstructors()` | 获取所有公共构造函数 |
| `Type.GetConstructor()` | 获取指定参数的构造函数 |
| `Type.GetConstructors(BindingFlags)` | 获取指定绑定标志的构造函数 |

### Constructor属性操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConstructorInfo.Name` | 获取构造函数名称 |
| `ConstructorInfo.DeclaringType` | 获取声明类型 |
| `ConstructorInfo.IsStatic` | 检查是否为静态构造函数 |
| `ConstructorInfo.IsPublic` | 检查是否为公共构造函数 |

### Constructor调用操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConstructorInfo.Invoke()` | 调用构造函数创建实例 |
| `ConstructorInfo.GetParameters()` | 获取构造函数参数 |

---

## Attribute处理

### Attribute获取操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `MemberInfo.GetCustomAttributes()` | 获取自定义特性 |
| `MemberInfo.GetCustomAttribute()` | 获取指定类型的特性 |
| `MemberInfo.IsDefined()` | 检查是否定义了特性 |

### Attribute类型操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Attribute.GetCustomAttribute()` | 获取自定义特性 |
| `Attribute.GetCustomAttributes()` | 获取所有自定义特性 |
| `Attribute.IsDefined()` | 检查是否定义了特性 |

### 自定义Attribute

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `AttributeUsage` | 定义特性使用方式 |
| `AttributeTargets` | 定义特性目标 |
| `AllowMultiple` | 允许多次使用 |
| `Inherited` | 是否可继承 |

---

## 动态调用

### 动态对象创建

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Activator.CreateInstance()` | 创建类型实例 |
| `Activator.CreateInstance(Type)` | 创建指定类型实例 |
| `Activator.CreateInstance(Type, Object[])` | 使用参数创建实例 |
| `Activator.CreateInstanceFrom()` | 从程序集文件创建实例 |

### 动态方法调用

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `MethodInfo.Invoke()` | 调用方法 |
| `PropertyInfo.GetValue()` | 获取属性值 |
| `PropertyInfo.SetValue()` | 设置属性值 |
| `FieldInfo.GetValue()` | 获取字段值 |
| `FieldInfo.SetValue()` | 设置字段值 |

### 动态类型操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Type.MakeGenericType()` | 创建构造泛型类型 |
| `Type.GetGenericTypeDefinition()` | 获取泛型类型定义 |
| `Type.GetGenericArguments()` | 获取泛型参数 |

---

## Assembly操作

### Assembly获取操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Assembly.GetExecutingAssembly()` | 获取当前执行程序集 |
| `Assembly.GetCallingAssembly()` | 获取调用程序集 |
| `Assembly.GetEntryAssembly()` | 获取入口程序集 |
| `Assembly.Load()` | 加载程序集 |
| `Assembly.LoadFrom()` | 从文件加载程序集 |
| `Assembly.ReflectionOnlyLoad()` | 仅反射加载程序集 |

### Assembly属性操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Assembly.FullName` | 获取程序集全名 |
| `Assembly.Location` | 获取程序集位置 |
| `Assembly.GetName()` | 获取程序集名称 |
| `Assembly.GetTypes()` | 获取所有类型 |
| `Assembly.GetExportedTypes()` | 获取导出的类型 |

### Assembly信息操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Assembly.GetManifestResourceNames()` | 获取清单资源名称 |
| `Assembly.GetManifestResourceStream()` | 获取清单资源流 |
| `Assembly.GetCustomAttributes()` | 获取程序集特性 |

---

## 泛型处理

### 泛型类型操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Type.IsGenericType` | 检查是否为泛型类型 |
| `Type.IsGenericTypeDefinition` | 检查是否为泛型类型定义 |
| `Type.GetGenericArguments()` | 获取泛型参数 |
| `Type.GetGenericTypeDefinition()` | 获取泛型类型定义 |
| `Type.MakeGenericType()` | 创建构造泛型类型 |

### 泛型方法操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `MethodInfo.IsGenericMethod` | 检查是否为泛型方法 |
| `MethodInfo.IsGenericMethodDefinition` | 检查是否为泛型方法定义 |
| `MethodInfo.GetGenericArguments()` | 获取泛型参数 |
| `MethodInfo.GetGenericMethodDefinition()` | 获取泛型方法定义 |
| `MethodInfo.MakeGenericMethod()` | 创建构造泛型方法 |

---

## 枚举值说明

### BindingFlags 枚举

| 值 | 说明 |
|----|------|
| `Default` | 默认绑定标志 |
| `IgnoreCase` | 忽略大小写 |
| `DeclaredOnly` | 仅在声明类型中查找 |
| `Instance` | 实例成员 |
| `Static` | 静态成员 |
| `Public` | 公共成员 |
| `NonPublic` | 非公共成员 |
| `FlattenHierarchy` | 展平层次结构 |
| `InvokeMethod` | 调用方法 |
| `CreateInstance` | 创建实例 |
| `GetField` | 获取字段 |
| `SetField` | 设置字段 |
| `GetProperty` | 获取属性 |
| `SetProperty` | 设置属性 |
| `PutDispProperty` | 设置调度属性 |
| `PutRefDispProperty` | 设置引用调度属性 |
| `ExactBinding` | 精确绑定 |
| `SuppressChangeType` | 抑制类型转换 |
| `OptionalParamBinding` | 可选参数绑定 |
| `IgnoreReturn` | 忽略返回值 |

### MemberTypes 枚举

| 值 | 说明 |
|----|------|
| `Constructor` | 构造函数 |
| `Event` | 事件 |
| `Field` | 字段 |
| `Method` | 方法 |
| `Property` | 属性 |
| `TypeInfo` | 类型信息 |
| `Custom` | 自定义 |
| `NestedType` | 嵌套类型 |
| `All` | 所有成员 |

### TypeAttributes 枚举

| 值 | 说明 |
|----|------|
| `NotPublic` | 非公共 |
| `Public` | 公共 |
| `NestedPublic` | 嵌套公共 |
| `NestedPrivate` | 嵌套私有 |
| `NestedFamily` | 嵌套族 |
| `NestedAssembly` | 嵌套程序集 |
| `NestedFamANDAssem` | 嵌套族和程序集 |
| `NestedFamORAssem` | 嵌套族或程序集 |
| `VisibilityMask` | 可见性掩码 |
| `SequentialLayout` | 顺序布局 |
| `ExplicitLayout` | 显式布局 |
| `LayoutMask` | 布局掩码 |
| `Interface` | 接口 |
| `Class` | 类 |
| `Abstract` | 抽象 |
| `Sealed` | 密封 |
| `SpecialName` | 特殊名称 |
| `Import` | 导入 |
| `Serializable` | 可序列化 |
| `WindowsRuntime` | Windows运行时 |
| `StringFormatMask` | 字符串格式掩码 |
| `HasSecurity` | 有安全性 |
| `ReservedMask` | 保留掩码 |

---

## 使用注意事项

1. **性能考虑**：反射操作比直接调用慢，应谨慎使用
2. **安全性**：反射可以访问私有成员，需要权限控制
3. **异常处理**：反射操作可能抛出异常，需要适当的异常处理
4. **缓存机制**：频繁使用的反射信息应该缓存
5. **泛型处理**：泛型类型需要特殊处理
6. **版本兼容**：反射可能受到程序集版本变化影响
7. **调试困难**：反射代码调试相对困难
8. **适用场景**：适合插件系统、序列化、依赖注入等场景

---

## 示例代码

完整的示例代码请参考 `ReflectionExample.cs` 文件，其中包含了所有API的详细使用示例和中文注释。 