# .NET 官方API示例项目

本项目包含了.NET框架中常用API的示例代码，涵盖了以下主要模块：

## 项目结构

### 1. Collections (集合)
- List<T>, Dictionary<TKey, TValue>, HashSet<T>等集合类型的使用示例
- LINQ查询操作
- 集合性能优化技巧

### 2. IO (输入输出)
- 文件操作 (File, Directory)
- 流操作 (Stream, FileStream, MemoryStream)
- 异步IO操作

### 3. Networking (网络)
- HttpClient使用示例
- WebSocket通信
- TCP/UDP网络编程

### 4. Threading (多线程)
- Task异步编程
- Thread线程管理
- 并发集合和同步原语

### 5. Serialization (序列化)
- JSON序列化/反序列化
- XML序列化
- 二进制序列化

### 6. Reflection (反射)
- 类型信息获取
- 动态方法调用
- 属性访问

### 7. Regex (正则表达式)
- 基础正则表达式模式
- 实用验证模式（邮箱、手机号、URL等）
- 高级功能（捕获组、替换、分割）
- 性能优化和错误处理

### 8. Security (安全)
- 加密算法（AES、RSA、SHA等）
- 数字签名和证书
- 身份验证和授权
- 密码安全和数据保护

### 9. Diagnostics (诊断)ss
- 日志记录和跟踪
- 性能监控和分析
- 调试和诊断工具
- 内存和资源监控

### 10. Configuration (配置)
- 配置文件管理（JSON、XML、INI）
- 环境变量和命令行参数
- 强类型配置绑定
- 配置验证和热重载

### 11. DependencyInjection (依赖注入)
- IoC容器和服务注册
- 服务生命周期管理
- 构造函数注入和接口注入
- 装饰器模式和条件注册

### 12. ComprehensiveExample (综合案例)
- **数据管理系统** - 涵盖所有.NET知识点的完整案例
- 实现数据采集、处理、存储、分析的完整流程
- 展示各模块间的协同工作


## 综合案例详解

### ComprehensiveExample - 数据管理系统

这是一个完整的.NET综合案例，涵盖了所有核心知识点：

#### 🎯 系统功能
1. **数据采集** - 从网络API获取数据
2. **数据处理** - 使用集合和LINQ处理数据
3. **数据存储** - 文件IO和序列化
4. **数据分析** - 反射和动态处理
5. **并发处理** - 多线程和异步操作
6. **实时监控** - 文件系统监控

#### 📚 涵盖的知识点

**Collections (集合)**
- `List<T>` - 存储数据项列表
- `Dictionary<TKey, TValue>` - 数据缓存
- `ConcurrentQueue<T>` - 线程安全的处理队列
- `HashSet<T>` - 去重处理
- LINQ - 数据查询和统计

**IO (输入输出)**
- `File` - 文件读写操作
- `Directory` - 目录管理
- `FileSystemWatcher` - 文件变化监控
- 异步文件操作

**Networking (网络)**
- `HttpClient` - HTTP请求
- 异步网络操作
- JSON数据获取

**Threading (多线程)**
- `Task` - 异步任务
- `async/await` - 异步编程模式
- `SemaphoreSlim` - 并发控制
- `CancellationTokenSource` - 取消令牌
- `Interlocked` - 原子操作

**Serialization (序列化)**
- `JsonSerializer` - JSON序列化
- `XmlSerializer` - XML序列化
- 文件格式转换

**Reflection (反射)**
- `Type` - 类型信息
- `PropertyInfo` - 属性反射
- 动态属性访问和修改

**Regex (正则表达式)**
- `Regex` - 正则表达式核心类
- `Match` - 单个匹配结果
- `MatchCollection` - 匹配结果集合
- `Group` - 捕获组
- 常用验证模式（邮箱、手机号、URL、IP地址等）
- 高级功能（命名捕获组、非捕获组、断言）
- 性能优化（编译模式、缓存）

**Security (安全)**
- `AES` - 高级加密标准
- `RSA` - 非对称加密算法
- `SHA256` - 安全哈希算法
- `X509Certificate2` - X.509证书
- 密码哈希和盐值
- 数字签名和验证
- 身份验证和授权

**Diagnostics (诊断)**
- `ILogger` - 结构化日志接口
- `Stopwatch` - 性能计时器
- `Process` - 进程监控
- `PerformanceCounter` - 性能计数器
- 活动跟踪和ETW
- 内存和资源监控
- 调试和诊断工具

**Configuration (配置)**
- `IConfiguration` - 配置接口
- `ConfigurationBuilder` - 配置构建器
- `IOptions<T>` - 强类型配置选项
- JSON/XML/INI配置文件支持
- 环境变量和命令行参数
- 配置验证和热重载

**DependencyInjection (依赖注入)**
- `IServiceCollection` - 服务集合
- `IServiceProvider` - 服务提供程序
- `ServiceLifetime` - 服务生命周期
- 构造函数注入和接口注入
- 装饰器模式和条件注册
- 作用域管理和资源释放

#### 🔄 工作流程

```
1. 系统初始化
   ├── Collections: 初始化各种集合
   ├── IO: 创建目录和文件监控
   ├── Networking: 配置HttpClient
   ├── Threading: 设置并发控制
   ├── Serialization: 配置序列化选项
   └── Reflection: 获取类型信息

2. 数据采集循环
   ├── Networking: 从API获取数据
   ├── Serialization: 反序列化JSON
   └── Collections: 处理新数据

3. 并发数据处理
   ├── Threading: 多任务并发处理
   ├── Collections: 队列管理
   └── Reflection: 动态数据处理

4. 数据存储
   ├── IO: 文件操作
   ├── Serialization: JSON/XML序列化
   └── Collections: 数据组织

5. 实时监控
   ├── IO: 文件变化监控
   └── Collections: 状态统计
```

#### 🎮 使用方法

1. **启动系统**
   ```csharp
   // 自动启动
   comprehensiveExample.autoStart = true;
   
   // 手动启动
   comprehensiveExample.StartDataProcessing();
   ```

2. **监控状态**
   ```csharp
   // 获取统计信息
   comprehensiveExample.GetSystemStatistics();
   ```

3. **停止系统**
   ```csharp
   comprehensiveExample.StopDataProcessing();
   ```

#### 📊 系统特点

- **完整性**: 涵盖所有.NET核心知识点
- **实用性**: 真实的数据处理场景
- **可扩展性**: 模块化设计，易于扩展
- **性能优化**: 使用并发和异步操作
- **错误处理**: 完善的异常处理机制
- **跨平台**: 兼容Unity各平台

#### 🎯 学习价值

这个综合案例展示了：
- 如何将多个.NET模块协同工作
- 实际项目中的架构设计思路
- 性能优化和并发处理的最佳实践
- 错误处理和资源管理的规范做法

通过这个案例，可以全面理解和掌握.NET框架的核心功能，为实际项目开发打下坚实基础。

## 使用说明

每个模块都包含详细的示例代码和注释，可以直接在Unity项目中使用。
所有示例都遵循.NET最佳实践和Unity开发规范。

**推荐学习顺序**：
1. 先学习各个独立模块的基础知识
2. 然后运行ComprehensiveExample综合案例
3. 分析案例中的模块协作方式
4. 尝试修改和扩展案例功能 