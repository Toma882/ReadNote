using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNet.DependencyInjection
{
    /// <summary>
    /// .NET DependencyInjection (依赖注入) API 综合示例
    /// 展示Microsoft.Extensions.DependencyInjection命名空间中所有主要功能
    /// </summary>
    public class DependencyInjectionExample
    {
        #region 基础服务定义

        /// <summary>
        /// 服务接口定义
        /// </summary>
        public interface IEmailService
        {
            Task SendEmailAsync(string to, string subject, string body);
        }

        public interface IUserService
        {
            Task<User> GetUserAsync(int id);
            Task<User> CreateUserAsync(string name, string email);
        }

        public interface ILoggerService
        {
            void LogInfo(string message);
            void LogError(string message, Exception exception = null);
        }

        /// <summary>
        /// 用户实体
        /// </summary>
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
        }

        /// <summary>
        /// 邮件服务实现
        /// </summary>
        public class EmailService : IEmailService
        {
            private readonly ILoggerService _logger;

            public EmailService(ILoggerService logger)
            {
                _logger = logger;
            }

            public async Task SendEmailAsync(string to, string subject, string body)
            {
                _logger.LogInfo($"发送邮件到: {to}, 主题: {subject}");
                
                // 模拟邮件发送
                await Task.Delay(100);
                
                _logger.LogInfo($"邮件发送成功: {to}");
            }
        }

        /// <summary>
        /// 用户服务实现
        /// </summary>
        public class UserService : IUserService
        {
            private readonly IEmailService _emailService;
            private readonly ILoggerService _logger;
            private readonly List<User> _users = new List<User>();
            private int _nextId = 1;

            public UserService(IEmailService emailService, ILoggerService logger)
            {
                _emailService = emailService;
                _logger = logger;
            }

            public async Task<User> GetUserAsync(int id)
            {
                _logger.LogInfo($"获取用户: {id}");
                
                await Task.Delay(50);
                
                var user = _users.Find(u => u.Id == id);
                if (user == null)
                {
                    _logger.LogError($"用户不存在: {id}");
                }
                
                return user;
            }

            public async Task<User> CreateUserAsync(string name, string email)
            {
                _logger.LogInfo($"创建用户: {name}, {email}");
                
                var user = new User
                {
                    Id = _nextId++,
                    Name = name,
                    Email = email,
                    CreatedAt = DateTime.Now
                };
                
                _users.Add(user);
                
                // 发送欢迎邮件
                await _emailService.SendEmailAsync(email, "欢迎", $"欢迎 {name}!");
                
                _logger.LogInfo($"用户创建成功: {user.Id}");
                return user;
            }
        }

        /// <summary>
        /// 日志服务实现
        /// </summary>
        public class LoggerService : ILoggerService
        {
            public void LogInfo(string message)
            {
                Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
            }

            public void LogError(string message, Exception exception = null)
            {
                Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
                if (exception != null)
                {
                    Console.WriteLine($"异常: {exception.Message}");
                }
            }
        }

        #endregion

        #region 基础依赖注入示例

        /// <summary>
        /// 基础服务注册示例
        /// </summary>
        public static void BasicServiceRegistrationExample()
        {
            Console.WriteLine("=== 基础服务注册示例 ===");
            
            // 创建服务集合
            var services = new ServiceCollection();
            
            // 注册服务
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            
            // 构建服务提供程序
            var serviceProvider = services.BuildServiceProvider();
            
            // 解析服务
            var userService = serviceProvider.GetRequiredService<IUserService>();
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            var loggerService = serviceProvider.GetRequiredService<ILoggerService>();
            
            Console.WriteLine($"用户服务: {userService.GetType().Name}");
            Console.WriteLine($"邮件服务: {emailService.GetType().Name}");
            Console.WriteLine($"日志服务: {loggerService.GetType().Name}");
            
            // 清理资源
            serviceProvider.Dispose();
        }

        /// <summary>
        /// 服务生命周期示例
        /// </summary>
        public static void ServiceLifetimeExample()
        {
            Console.WriteLine("\n=== 服务生命周期示例 ===");
            
            var services = new ServiceCollection();
            
            // 注册不同生命周期的服务
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            
            var serviceProvider = services.BuildServiceProvider();
            
            // 测试单例服务
            Console.WriteLine("\n单例服务测试:");
            var logger1 = serviceProvider.GetRequiredService<ILoggerService>();
            var logger2 = serviceProvider.GetRequiredService<ILoggerService>();
            Console.WriteLine($"单例服务实例相同: {ReferenceEquals(logger1, logger2)}");
            
            // 测试瞬态服务
            Console.WriteLine("\n瞬态服务测试:");
            var email1 = serviceProvider.GetRequiredService<IEmailService>();
            var email2 = serviceProvider.GetRequiredService<IEmailService>();
            Console.WriteLine($"瞬态服务实例相同: {ReferenceEquals(email1, email2)}");
            
            // 测试作用域服务
            Console.WriteLine("\n作用域服务测试:");
            using (var scope1 = serviceProvider.CreateScope())
            {
                var user1 = scope1.ServiceProvider.GetRequiredService<IUserService>();
                var user2 = scope1.ServiceProvider.GetRequiredService<IUserService>();
                Console.WriteLine($"同一作用域内服务实例相同: {ReferenceEquals(user1, user2)}");
            }
            
            using (var scope2 = serviceProvider.CreateScope())
            {
                var user3 = scope2.ServiceProvider.GetRequiredService<IUserService>();
                Console.WriteLine($"不同作用域服务实例相同: {ReferenceEquals(email1, email2)}");
            }
            
            serviceProvider.Dispose();
        }

        #endregion

        #region 工厂模式示例

        /// <summary>
        /// 工厂模式服务注册示例
        /// </summary>
        public static void FactoryPatternExample()
        {
            Console.WriteLine("\n=== 工厂模式服务注册示例 ===");
            
            var services = new ServiceCollection();
            
            // 使用工厂方法注册服务
            services.AddSingleton<ILoggerService>(provider =>
            {
                Console.WriteLine("创建日志服务实例");
                return new LoggerService();
            });
            
            services.AddTransient<IEmailService>(provider =>
            {
                var logger = provider.GetRequiredService<ILoggerService>();
                Console.WriteLine("创建邮件服务实例");
                return new EmailService(logger);
            });
            
            services.AddScoped<IUserService>(provider =>
            {
                var emailService = provider.GetRequiredService<IEmailService>();
                var logger = provider.GetRequiredService<ILoggerService>();
                Console.WriteLine("创建用户服务实例");
                return new UserService(emailService, logger);
            });
            
            var serviceProvider = services.BuildServiceProvider();
            
            // 解析服务
            var userService = serviceProvider.GetRequiredService<IUserService>();
            Console.WriteLine($"用户服务创建成功: {userService.GetType().Name}");
            
            serviceProvider.Dispose();
        }

        /// <summary>
        /// 条件服务注册示例
        /// </summary>
        public static void ConditionalRegistrationExample()
        {
            Console.WriteLine("\n=== 条件服务注册示例 ===");
            
            var services = new ServiceCollection();
            
            // 尝试添加服务（如果不存在则添加）
            services.TryAddSingleton<ILoggerService, LoggerService>();
            services.TryAddSingleton<ILoggerService, LoggerService>(); // 第二次添加会被忽略
            
            // 尝试添加瞬态服务
            services.TryAddTransient<IEmailService, EmailService>();
            services.TryAddTransient<IEmailService, EmailService>(); // 第二次添加会被忽略
            
            // 尝试添加作用域服务
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IUserService, UserService>(); // 第二次添加会被忽略
            
            var serviceProvider = services.BuildServiceProvider();
            
            // 验证服务是否正确注册
            var loggerService = serviceProvider.GetRequiredService<ILoggerService>();
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            var userService = serviceProvider.GetRequiredService<IUserService>();
            
            Console.WriteLine($"日志服务: {loggerService.GetType().Name}");
            Console.WriteLine($"邮件服务: {emailService.GetType().Name}");
            Console.WriteLine($"用户服务: {userService.GetType().Name}");
            
            serviceProvider.Dispose();
        }

        #endregion

        #region 装饰器模式示例

        /// <summary>
        /// 邮件服务装饰器
        /// </summary>
        public class EmailServiceDecorator : IEmailService
        {
            private readonly IEmailService _emailService;
            private readonly ILoggerService _logger;

            public EmailServiceDecorator(IEmailService emailService, ILoggerService logger)
            {
                _emailService = emailService;
                _logger = logger;
            }

            public async Task SendEmailAsync(string to, string subject, string body)
            {
                _logger.LogInfo($"装饰器: 准备发送邮件到 {to}");
                
                try
                {
                    await _emailService.SendEmailAsync(to, subject, body);
                    _logger.LogInfo($"装饰器: 邮件发送成功到 {to}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"装饰器: 邮件发送失败到 {to}", ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// 装饰器模式示例
        /// </summary>
        public static void DecoratorPatternExample()
        {
            Console.WriteLine("\n=== 装饰器模式示例 ===");
            
            var services = new ServiceCollection();
            
            // 注册基础服务
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IEmailService, EmailService>();
            
            // 注册装饰器
            services.AddTransient<IEmailService>(provider =>
            {
                var emailService = provider.GetRequiredService<IEmailService>();
                var logger = provider.GetRequiredService<ILoggerService>();
                return new EmailServiceDecorator(emailService, logger);
            });
            
            var serviceProvider = services.BuildServiceProvider();
            
            // 使用装饰器服务
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            emailService.SendEmailAsync("user@example.com", "测试邮件", "这是测试内容").Wait();
            
            serviceProvider.Dispose();
        }

        #endregion

        #region 服务解析示例

        /// <summary>
        /// 服务解析示例
        /// </summary>
        public static void ServiceResolutionExample()
        {
            Console.WriteLine("\n=== 服务解析示例 ===");
            
            var services = new ServiceCollection();
            
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            
            var serviceProvider = services.BuildServiceProvider();
            
            // GetService - 可能返回null
            var loggerService = serviceProvider.GetService<ILoggerService>();
            Console.WriteLine($"GetService结果: {(loggerService != null ? "成功" : "失败")}");
            
            // GetRequiredService - 如果服务不存在则抛出异常
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            Console.WriteLine($"GetRequiredService结果: {emailService.GetType().Name}");
            
            // GetServices - 获取所有注册的服务
            var allServices = serviceProvider.GetServices<ILoggerService>();
            Console.WriteLine($"GetServices结果: {allServices.Count()} 个服务");
            
            // 使用作用域
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedUserService = scope.ServiceProvider.GetRequiredService<IUserService>();
                Console.WriteLine($"作用域服务: {scopedUserService.GetType().Name}");
            }
            
            serviceProvider.Dispose();
        }

        #endregion

        #region 高级功能示例

        /// <summary>
        /// 服务集合扩展示例
        /// </summary>
        public static void ServiceCollectionExtensionsExample()
        {
            Console.WriteLine("\n=== 服务集合扩展示例 ===");
            
            var services = new ServiceCollection();
            
            // 添加多个服务
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            
            // 添加服务集合
            var additionalServices = new ServiceCollection();
            additionalServices.AddSingleton<ILoggerService, LoggerService>();
            
            // 合并服务集合
            foreach (var service in additionalServices)
            {
                services.Add(service);
            }
            
            var serviceProvider = services.BuildServiceProvider();
            
            // 验证服务
            var loggerService = serviceProvider.GetRequiredService<ILoggerService>();
            Console.WriteLine($"日志服务: {loggerService.GetType().Name}");
            
            serviceProvider.Dispose();
        }

        /// <summary>
        /// 服务提供程序工厂示例
        /// </summary>
        public static void ServiceProviderFactoryExample()
        {
            Console.WriteLine("\n=== 服务提供程序工厂示例 ===");
            
            var services = new ServiceCollection();
            
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            
            // 使用默认服务提供程序工厂
            var serviceProvider = services.BuildServiceProvider();
            
            // 创建作用域工厂
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            
            // 使用作用域工厂
            using (var scope = scopeFactory.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                Console.WriteLine($"使用作用域工厂创建的服务: {userService.GetType().Name}");
            }
            
            serviceProvider.Dispose();
        }

        #endregion

        #region 实际应用示例

        /// <summary>
        /// 实际应用示例
        /// </summary>
        public static async Task RealWorldApplicationExample()
        {
            Console.WriteLine("\n=== 实际应用示例 ===");
            
            var services = new ServiceCollection();
            
            // 注册服务
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            
            var serviceProvider = services.BuildServiceProvider();
            
            try
            {
                // 模拟用户注册流程
                using (var scope = serviceProvider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                    
                    // 创建用户
                    var user = await userService.CreateUserAsync("张三", "zhangsan@example.com");
                    Console.WriteLine($"用户创建成功: {user.Name} (ID: {user.Id})");
                    
                    // 获取用户
                    var retrievedUser = await userService.GetUserAsync(user.Id);
                    if (retrievedUser != null)
                    {
                        Console.WriteLine($"用户获取成功: {retrievedUser.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"应用执行异常: {ex.Message}");
            }
            finally
            {
                serviceProvider.Dispose();
            }
        }

        #endregion

        #region 最佳实践示例

        /// <summary>
        /// 依赖注入最佳实践示例
        /// </summary>
        public static void BestPracticesExample()
        {
            Console.WriteLine("\n=== 依赖注入最佳实践示例 ===");
            
            var services = new ServiceCollection();
            
            // 1. 面向接口编程
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            
            // 2. 合理选择生命周期
            // - Singleton: 无状态服务，如配置、日志
            // - Scoped: 有状态服务，如数据库上下文
            // - Transient: 轻量级服务，如工具类
            
            // 3. 避免循环依赖
            // 通过接口和抽象类来解耦
            
            // 4. 使用工厂模式处理复杂创建逻辑
            services.AddTransient<IEmailService>(provider =>
            {
                var logger = provider.GetRequiredService<ILoggerService>();
                // 可以在这里添加复杂的创建逻辑
                return new EmailService(logger);
            });
            
            var serviceProvider = services.BuildServiceProvider();
            
            // 5. 及时释放资源
            try
            {
                var userService = serviceProvider.GetRequiredService<IUserService>();
                Console.WriteLine($"最佳实践示例: {userService.GetType().Name}");
            }
            finally
            {
                serviceProvider.Dispose();
            }
        }

        #endregion

        #region 主方法

        /// <summary>
        /// 运行所有依赖注入示例
        /// </summary>
        public static async Task RunAllExamples()
        {
            Console.WriteLine("C# DependencyInjection (依赖注入) API 综合示例");
            Console.WriteLine("===============================================");
            
            try
            {
                BasicServiceRegistrationExample();
                ServiceLifetimeExample();
                FactoryPatternExample();
                ConditionalRegistrationExample();
                DecoratorPatternExample();
                ServiceResolutionExample();
                ServiceCollectionExtensionsExample();
                ServiceProviderFactoryExample();
                await RealWorldApplicationExample();
                BestPracticesExample();
                
                Console.WriteLine("\n所有依赖注入示例运行完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"运行示例时发生错误: {ex.Message}");
            }
        }

        #endregion
    }
}
