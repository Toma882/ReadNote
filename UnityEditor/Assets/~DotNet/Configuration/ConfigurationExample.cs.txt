using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace DotNet.Configuration
{
    /// <summary>
    /// .NET Configuration (配置) API 综合示例
    /// 展示Microsoft.Extensions.Configuration命名空间中所有主要功能
    /// </summary>
    public class ConfigurationExample
    {
        #region 基础配置示例

        /// <summary>
        /// 基础配置构建示例
        /// </summary>
        public static void BasicConfigurationExample()
        {
            Console.WriteLine("=== 基础配置构建示例 ===");
            
            // 创建配置构建器
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(Environment.GetCommandLineArgs())
                .Build();
            
            // 读取配置值
            string appName = configuration["AppName"] ?? "DefaultApp";
            string version = configuration["Version"] ?? "1.0.0";
            bool debugMode = configuration.GetValue<bool>("DebugMode", false);
            
            Console.WriteLine($"应用程序名称: {appName}");
            Console.WriteLine($"版本: {version}");
            Console.WriteLine($"调试模式: {debugMode}");
            
            // 读取嵌套配置
            string databaseConnection = configuration["Database:ConnectionString"] ?? "DefaultConnection";
            int timeout = configuration.GetValue<int>("Database:Timeout", 30);
            
            Console.WriteLine($"数据库连接: {databaseConnection}");
            Console.WriteLine($"超时时间: {timeout} 秒");
        }

        /// <summary>
        /// JSON配置文件示例
        /// </summary>
        public static void JsonConfigurationExample()
        {
            Console.WriteLine("\n=== JSON配置文件示例 ===");
            
            // 创建示例JSON配置
            string jsonConfig = @"{
                ""AppSettings"": {
                    ""Name"": ""MyApplication"",
                    ""Version"": ""2.0.0"",
                    ""Environment"": ""Development"",
                    ""Features"": {
                        ""EnableLogging"": true,
                        ""EnableCaching"": false,
                        ""MaxRetryCount"": 3
                    }
                },
                ""Database"": {
                    ""ConnectionString"": ""Server=localhost;Database=MyDB;Trusted_Connection=true;"",
                    ""Timeout"": 30,
                    ""PoolSize"": 100
                },
                ""Logging"": {
                    ""LogLevel"": {
                        ""Default"": ""Information"",
                        ""Microsoft"": ""Warning"",
                        ""System"": ""Error""
                    }
                }
            }";
            
            // 将JSON写入临时文件
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, jsonConfig);
            
            try
            {
                // 从JSON文件加载配置
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile(tempFile, optional: false, reloadOnChange: true)
                    .Build();
                
                // 读取配置值
                Console.WriteLine($"应用程序名称: {configuration["AppSettings:Name"]}");
                Console.WriteLine($"版本: {configuration["AppSettings:Version"]}");
                Console.WriteLine($"环境: {configuration["AppSettings:Environment"]}");
                Console.WriteLine($"启用日志: {configuration["AppSettings:Features:EnableLogging"]}");
                Console.WriteLine($"启用缓存: {configuration["AppSettings:Features:EnableCaching"]}");
                Console.WriteLine($"最大重试次数: {configuration["AppSettings:Features:MaxRetryCount"]}");
                Console.WriteLine($"数据库连接: {configuration["Database:ConnectionString"]}");
                Console.WriteLine($"连接超时: {configuration["Database:Timeout"]} 秒");
                Console.WriteLine($"连接池大小: {configuration["Database:PoolSize"]}");
                Console.WriteLine($"默认日志级别: {configuration["Logging:LogLevel:Default"]}");
            }
            finally
            {
                // 清理临时文件
                File.Delete(tempFile);
            }
        }

        /// <summary>
        /// 环境变量配置示例
        /// </summary>
        public static void EnvironmentVariablesExample()
        {
            Console.WriteLine("\n=== 环境变量配置示例 ===");
            
            // 设置一些环境变量
            Environment.SetEnvironmentVariable("APP_NAME", "EnvironmentApp");
            Environment.SetEnvironmentVariable("APP_VERSION", "3.0.0");
            Environment.SetEnvironmentVariable("DATABASE__CONNECTION_STRING", "Server=env-server;Database=EnvDB;");
            Environment.SetEnvironmentVariable("FEATURES__ENABLE_LOGGING", "true");
            
            // 从环境变量加载配置
            IConfiguration configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            
            Console.WriteLine($"应用程序名称: {configuration["APP_NAME"]}");
            Console.WriteLine($"版本: {configuration["APP_VERSION"]}");
            Console.WriteLine($"数据库连接: {configuration["DATABASE__CONNECTION_STRING"]}");
            Console.WriteLine($"启用日志: {configuration["FEATURES__ENABLE_LOGGING"]}");
            
            // 使用前缀过滤
            IConfiguration prefixedConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables("APP_")
                .Build();
            
            Console.WriteLine("\n使用APP_前缀的环境变量:");
            Console.WriteLine($"名称: {prefixedConfig["NAME"]}");
            Console.WriteLine($"版本: {prefixedConfig["VERSION"]}");
        }

        /// <summary>
        /// 命令行参数配置示例
        /// </summary>
        public static void CommandLineConfigurationExample()
        {
            Console.WriteLine("\n=== 命令行参数配置示例 ===");
            
            // 模拟命令行参数
            string[] args = {
                "--AppName=CommandLineApp",
                "--Version=4.0.0",
                "--Database:ConnectionString=Server=cmd-server;Database=CmdDB;",
                "--Features:EnableLogging=true",
                "--Features:MaxRetryCount=5"
            };
            
            // 从命令行参数加载配置
            IConfiguration configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            
            Console.WriteLine($"应用程序名称: {configuration["AppName"]}");
            Console.WriteLine($"版本: {configuration["Version"]}");
            Console.WriteLine($"数据库连接: {configuration["Database:ConnectionString"]}");
            Console.WriteLine($"启用日志: {configuration["Features:EnableLogging"]}");
            Console.WriteLine($"最大重试次数: {configuration["Features:MaxRetryCount"]}");
        }

        #endregion

        #region 强类型配置示例

        /// <summary>
        /// 配置选项类定义
        /// </summary>
        public class AppSettings
        {
            public string Name { get; set; } = string.Empty;
            public string Version { get; set; } = string.Empty;
            public string Environment { get; set; } = string.Empty;
            public FeaturesSettings Features { get; set; } = new FeaturesSettings();
        }

        public class FeaturesSettings
        {
            public bool EnableLogging { get; set; }
            public bool EnableCaching { get; set; }
            public int MaxRetryCount { get; set; } = 3;
        }

        public class DatabaseSettings
        {
            public string ConnectionString { get; set; } = string.Empty;
            public int Timeout { get; set; } = 30;
            public int PoolSize { get; set; } = 100;
        }

        /// <summary>
        /// 强类型配置绑定示例
        /// </summary>
        public static void StrongTypedConfigurationExample()
        {
            Console.WriteLine("\n=== 强类型配置绑定示例 ===");
            
            // 创建配置
            var configData = new Dictionary<string, string>
            {
                ["AppSettings:Name"] = "StrongTypedApp",
                ["AppSettings:Version"] = "5.0.0",
                ["AppSettings:Environment"] = "Production",
                ["AppSettings:Features:EnableLogging"] = "true",
                ["AppSettings:Features:EnableCaching"] = "true",
                ["AppSettings:Features:MaxRetryCount"] = "5",
                ["Database:ConnectionString"] = "Server=prod-server;Database=ProdDB;",
                ["Database:Timeout"] = "60",
                ["Database:PoolSize"] = "200"
            };
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();
            
            // 绑定到强类型对象
            AppSettings appSettings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSettings);
            
            DatabaseSettings databaseSettings = new DatabaseSettings();
            configuration.GetSection("Database").Bind(databaseSettings);
            
            Console.WriteLine($"应用程序名称: {appSettings.Name}");
            Console.WriteLine($"版本: {appSettings.Version}");
            Console.WriteLine($"环境: {appSettings.Environment}");
            Console.WriteLine($"启用日志: {appSettings.Features.EnableLogging}");
            Console.WriteLine($"启用缓存: {appSettings.Features.EnableCaching}");
            Console.WriteLine($"最大重试次数: {appSettings.Features.MaxRetryCount}");
            Console.WriteLine($"数据库连接: {databaseSettings.ConnectionString}");
            Console.WriteLine($"连接超时: {databaseSettings.Timeout} 秒");
            Console.WriteLine($"连接池大小: {databaseSettings.PoolSize}");
        }

        /// <summary>
        /// 配置验证示例
        /// </summary>
        public static void ConfigurationValidationExample()
        {
            Console.WriteLine("\n=== 配置验证示例 ===");
            
            // 带验证的配置类
            var configData = new Dictionary<string, string>
            {
                ["AppSettings:Name"] = "", // 空名称，应该验证失败
                ["AppSettings:Version"] = "6.0.0",
                ["AppSettings:Environment"] = "Development",
                ["Database:ConnectionString"] = "Server=valid-server;Database=ValidDB;",
                ["Database:Timeout"] = "30",
                ["Database:PoolSize"] = "100"
            };
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();
            
            // 绑定并验证配置
            AppSettings appSettings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSettings);
            
            // 手动验证
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(appSettings);
            bool isValid = Validator.TryValidateObject(appSettings, validationContext, validationResults, true);
            
            if (!isValid)
            {
                Console.WriteLine("配置验证失败:");
                foreach (var result in validationResults)
                {
                    Console.WriteLine($"  - {result.ErrorMessage}");
                }
            }
            else
            {
                Console.WriteLine("配置验证通过");
            }
        }

        #endregion

        #region 配置选项模式示例

        /// <summary>
        /// 配置选项模式示例
        /// </summary>
        public static void OptionsPatternExample()
        {
            Console.WriteLine("\n=== 配置选项模式示例 ===");
            
            // 创建服务集合
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            
            // 创建配置
            var configData = new Dictionary<string, string>
            {
                ["AppSettings:Name"] = "OptionsPatternApp",
                ["AppSettings:Version"] = "7.0.0",
                ["AppSettings:Environment"] = "Staging",
                ["AppSettings:Features:EnableLogging"] = "true",
                ["AppSettings:Features:EnableCaching"] = "false",
                ["AppSettings:Features:MaxRetryCount"] = "3"
            };
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();
            
            // 注册配置
            services.AddSingleton<IConfiguration>(configuration);
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            
            // 构建服务提供程序
            var serviceProvider = services.BuildServiceProvider();
            
            // 使用IOptions
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
            Console.WriteLine($"应用程序名称: {appOptions.Value.Name}");
            Console.WriteLine($"版本: {appOptions.Value.Version}");
            Console.WriteLine($"环境: {appOptions.Value.Environment}");
            
            // 使用IOptionsSnapshot（支持配置热重载）
            var appSnapshot = serviceProvider.GetRequiredService<IOptionsSnapshot<AppSettings>>();
            Console.WriteLine($"快照版本: {appSnapshot.Value.Version}");
            
            // 使用IOptionsMonitor（支持配置变更通知）
            var appMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<AppSettings>>();
            Console.WriteLine($"监控版本: {appMonitor.CurrentValue.Version}");
            
            // 配置变更通知
            appMonitor.OnChange((settings, name) =>
            {
                Console.WriteLine($"配置已变更: {name}, 新版本: {settings.Version}");
            });
        }

        #endregion

        #region 配置热重载示例

        /// <summary>
        /// 配置热重载示例
        /// </summary>
        public static void ConfigurationReloadExample()
        {
            Console.WriteLine("\n=== 配置热重载示例 ===");
            
            // 创建临时配置文件
            string tempFile = Path.GetTempFileName();
            string initialConfig = @"{
                ""AppSettings"": {
                    ""Name"": ""ReloadApp"",
                    ""Version"": ""1.0.0"",
                    ""Environment"": ""Development""
                }
            }";
            
            File.WriteAllText(tempFile, initialConfig);
            
            try
            {
                // 创建支持热重载的配置
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddJsonFile(tempFile, optional: false, reloadOnChange: true)
                    .Build();
                
                Console.WriteLine($"初始版本: {configuration["AppSettings:Version"]}");
                
                // 模拟配置变更
                string updatedConfig = @"{
                    ""AppSettings"": {
                        ""Name"": ""ReloadApp"",
                        ""Version"": ""2.0.0"",
                        ""Environment"": ""Production""
                    }
                }";
                
                File.WriteAllText(tempFile, updatedConfig);
                
                // 等待文件变更
                System.Threading.Thread.Sleep(100);
                
                // 手动触发重载
                configuration.Reload();
                
                Console.WriteLine($"重载后版本: {configuration["AppSettings:Version"]}");
                Console.WriteLine($"重载后环境: {configuration["AppSettings:Environment"]}");
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        #endregion

        #region 配置加密示例

        /// <summary>
        /// 配置加密示例
        /// </summary>
        public static void ConfigurationEncryptionExample()
        {
            Console.WriteLine("\n=== 配置加密示例 ===");
            
            // 敏感配置数据
            string sensitiveData = "This is sensitive configuration data";
            string key = "MySecretKey12345"; // 实际应用中应该使用更安全的密钥管理
            
            // 简单的Base64编码（仅用于演示，实际应用中应使用真正的加密）
            byte[] dataBytes = Encoding.UTF8.GetBytes(sensitiveData);
            string encodedData = Convert.ToBase64String(dataBytes);
            
            Console.WriteLine($"原始数据: {sensitiveData}");
            Console.WriteLine($"编码后: {encodedData}");
            
            // 解码
            byte[] decodedBytes = Convert.FromBase64String(encodedData);
            string decodedData = Encoding.UTF8.GetString(decodedBytes);
            
            Console.WriteLine($"解码后: {decodedData}");
            Console.WriteLine($"数据完整性: {sensitiveData == decodedData}");
            
            // 配置中的敏感数据处理
            var configData = new Dictionary<string, string>
            {
                ["Database:ConnectionString"] = "Server=server;Database=db;Password=" + encodedData,
                ["ApiKeys:SecretKey"] = encodedData,
                ["Encryption:Key"] = key
            };
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();
            
            string encryptedConnectionString = configuration["Database:ConnectionString"];
            string encryptedApiKey = configuration["ApiKeys:SecretKey"];
            
            Console.WriteLine($"加密的连接字符串: {encryptedConnectionString}");
            Console.WriteLine($"加密的API密钥: {encryptedApiKey}");
        }

        #endregion

        #region 多环境配置示例

        /// <summary>
        /// 多环境配置示例
        /// </summary>
        public static void MultiEnvironmentConfigurationExample()
        {
            Console.WriteLine("\n=== 多环境配置示例 ===");
            
            // 模拟不同环境的配置
            var environments = new[] { "Development", "Staging", "Production" };
            
            foreach (string environment in environments)
            {
                Console.WriteLine($"\n{environment} 环境配置:");
                
                var configData = new Dictionary<string, string>
                {
                    ["Environment"] = environment,
                    ["AppSettings:Name"] = $"MultiEnvApp-{environment}",
                    ["AppSettings:Version"] = "8.0.0",
                    ["Database:ConnectionString"] = GetConnectionStringForEnvironment(environment),
                    ["Logging:LogLevel"] = GetLogLevelForEnvironment(environment),
                    ["Features:EnableDebugMode"] = (environment == "Development").ToString()
                };
                
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(configData)
                    .Build();
                
                Console.WriteLine($"  应用程序名称: {configuration["AppSettings:Name"]}");
                Console.WriteLine($"  数据库连接: {configuration["Database:ConnectionString"]}");
                Console.WriteLine($"  日志级别: {configuration["Logging:LogLevel"]}");
                Console.WriteLine($"  调试模式: {configuration["Features:EnableDebugMode"]}");
            }
        }

        private static string GetConnectionStringForEnvironment(string environment)
        {
            return environment switch
            {
                "Development" => "Server=dev-server;Database=DevDB;Trusted_Connection=true;",
                "Staging" => "Server=staging-server;Database=StagingDB;User=staging;Password=staging123;",
                "Production" => "Server=prod-server;Database=ProdDB;User=prod;Password=prod123;",
                _ => "Server=default-server;Database=DefaultDB;"
            };
        }

        private static string GetLogLevelForEnvironment(string environment)
        {
            return environment switch
            {
                "Development" => "Debug",
                "Staging" => "Information",
                "Production" => "Warning",
                _ => "Information"
            };
        }

        #endregion

        #region 配置最佳实践示例

        /// <summary>
        /// 配置最佳实践示例
        /// </summary>
        public static void ConfigurationBestPracticesExample()
        {
            Console.WriteLine("\n=== 配置最佳实践示例 ===");
            
            // 1. 使用默认值
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["AppSettings:Name"] = "BestPracticesApp"
                    // 其他配置项使用默认值
                })
                .Build();
            
            AppSettings appSettings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSettings);
            
            Console.WriteLine($"应用程序名称: {appSettings.Name}");
            Console.WriteLine($"版本: {appSettings.Version}"); // 使用默认值
            Console.WriteLine($"环境: {appSettings.Environment}"); // 使用默认值
            
            // 2. 配置验证
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(appSettings);
            bool isValid = Validator.TryValidateObject(appSettings, validationContext, validationResults, true);
            
            Console.WriteLine($"配置验证结果: {(isValid ? "通过" : "失败")}");
            
            // 3. 配置分层
            Console.WriteLine("\n配置分层示例:");
            var layeredConfig = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["AppSettings:Name"] = "BaseApp"
                })
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["AppSettings:Version"] = "9.0.0",
                    ["AppSettings:Environment"] = "Override"
                })
                .Build();
            
            Console.WriteLine($"基础名称: {layeredConfig["AppSettings:Name"]}");
            Console.WriteLine($"覆盖版本: {layeredConfig["AppSettings:Version"]}");
            Console.WriteLine($"覆盖环境: {layeredConfig["AppSettings:Environment"]}");
            
            // 4. 配置缓存
            Console.WriteLine("\n配置缓存示例:");
            var cachedConfig = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["Cache:Enabled"] = "true",
                    ["Cache:Duration"] = "300",
                    ["Cache:MaxSize"] = "1000"
                })
                .Build();
            
            bool cacheEnabled = cachedConfig.GetValue<bool>("Cache:Enabled");
            int cacheDuration = cachedConfig.GetValue<int>("Cache:Duration");
            int cacheMaxSize = cachedConfig.GetValue<int>("Cache:MaxSize");
            
            Console.WriteLine($"缓存启用: {cacheEnabled}");
            Console.WriteLine($"缓存持续时间: {cacheDuration} 秒");
            Console.WriteLine($"缓存最大大小: {cacheMaxSize} 项");
        }

        #endregion

        #region 主方法

        /// <summary>
        /// 运行所有配置示例
        /// </summary>
        public static void RunAllExamples()
        {
            Console.WriteLine("C# Configuration (配置) API 综合示例");
            Console.WriteLine("=====================================");
            
            try
            {
                BasicConfigurationExample();
                JsonConfigurationExample();
                EnvironmentVariablesExample();
                CommandLineConfigurationExample();
                StrongTypedConfigurationExample();
                ConfigurationValidationExample();
                OptionsPatternExample();
                ConfigurationReloadExample();
                ConfigurationEncryptionExample();
                MultiEnvironmentConfigurationExample();
                ConfigurationBestPracticesExample();
                
                Console.WriteLine("\n所有配置示例运行完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"运行示例时发生错误: {ex.Message}");
            }
        }

        #endregion
    }
}
