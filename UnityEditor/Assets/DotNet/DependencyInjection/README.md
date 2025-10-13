# .NET DependencyInjection (依赖注入) API 参考

## 概述

本文档提供了.NET中依赖注入相关的完整API参考，包括IoC容器、服务注册、生命周期管理等依赖注入功能。

## 主要类和接口

### 核心接口

#### 服务容器
- [x] **Microsoft.Extensions.DependencyInjection.IServiceCollection** [服务集合] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection)
- [x] **Microsoft.Extensions.DependencyInjection.IServiceProvider** [服务提供程序] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.iserviceprovider)
- [x] **Microsoft.Extensions.DependencyInjection.IServiceScope** [服务作用域] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.iservicescope)
- [x] **Microsoft.Extensions.DependencyInjection.IServiceScopeFactory** [服务作用域工厂] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.iservicescopefactory)

#### 服务描述符
- [x] **Microsoft.Extensions.DependencyInjection.ServiceDescriptor** [服务描述符] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicedescriptor)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceLifetime** [服务生命周期] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime)

### 服务注册

#### 扩展方法
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddTransient** [瞬态服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addtransient)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddScoped** [作用域服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addscoped)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton** [单例服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addsingleton)

#### 工厂模式
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddTransient<TService>(Func<IServiceProvider, TService>)** [工厂瞬态服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addtransient)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddScoped<TService>(Func<IServiceProvider, TService>)** [工厂作用域服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addscoped)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<TService>(Func<IServiceProvider, TService>)** [工厂单例服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addsingleton)

### 服务解析

#### 服务定位器
- [x] **Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetService<T>** [获取服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.getservice)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>** [获取必需服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.getrequiredservice)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetServices<T>** [获取服务集合] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.getservices)

### 高级功能

#### 装饰器模式
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.Decorate<TService>** [装饰器服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.decorate)

#### 条件注册
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.TryAddTransient** [尝试添加瞬态服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.tryaddtransient)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.TryAddScoped** [尝试添加作用域服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.tryaddscoped)
- [x] **Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.TryAddSingleton** [尝试添加单例服务] (https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.tryaddsingleton)

## 服务生命周期

### 瞬态 (Transient)
- [x] **每次请求创建新实例**
    - 适用于轻量级、无状态服务
    - 每次注入都创建新实例
    - 不共享状态

### 作用域 (Scoped)
- [x] **每个作用域一个实例**
    - 适用于Web请求作用域
    - 同一作用域内共享实例
    - 作用域结束时释放

### 单例 (Singleton)
- [x] **应用程序生命周期内一个实例**
    - 适用于重量级、有状态服务
    - 整个应用程序共享一个实例
    - 应用程序结束时释放

## 常用模式

### 接口注入
- [x] **接口-实现分离**
    - 定义服务接口
    - 实现具体服务
    - 注册接口-实现映射

### 构造函数注入
- [x] **依赖通过构造函数注入**
    - 声明依赖参数
    - 容器自动解析依赖
    - 编译时类型安全

### 属性注入
- [x] **依赖通过属性注入**
    - 标记注入属性
    - 容器自动设置属性
    - 运行时注入

### 方法注入
- [x] **依赖通过方法参数注入**
    - 方法参数声明依赖
    - 容器自动解析参数
    - 灵活的参数注入

## 最佳实践

### 服务设计
1. 面向接口编程
2. 单一职责原则
3. 依赖倒置原则
4. 接口隔离原则

### 生命周期管理
1. 合理选择生命周期
2. 避免循环依赖
3. 及时释放资源
4. 作用域管理

### 性能优化
1. 避免过度注入
2. 使用工厂模式
3. 延迟初始化
4. 缓存服务实例

### 测试支持
1. 模拟依赖服务
2. 测试容器配置
3. 集成测试支持
4. 单元测试隔离

## 相关资源

- [.NET 依赖注入文档](https://docs.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection)
- [服务生命周期](https://docs.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection#service-lifetimes)
- [依赖注入最佳实践](https://docs.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection-guidelines)
- [高级场景](https://docs.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection#advanced-scenarios)
