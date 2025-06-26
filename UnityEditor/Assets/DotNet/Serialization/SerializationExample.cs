// SerializationExample.cs
// .NET序列化API使用详解示例
// 包含JSON、XML、二进制序列化、数据契约序列化等
// 每个方法、关键步骤、枚举值均有详细中文注释
// 适合.NET初学者学习和查阅
// 
// 主要功能模块：
// 1. JSON序列化 - 使用JsonUtility和System.Text.Json
// 2. XML序列化 - 使用XmlSerializer
// 3. 二进制序列化 - 使用BinaryFormatter
// 4. 数据契约序列化 - 使用DataContractSerializer
// 5. 自定义序列化 - 实现ISerializable接口
// 6. 序列化属性 - 控制序列化行为
// 7. 文件操作 - 保存和加载序列化数据

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace DotNet.Serialization
{
    /// <summary>
    /// .NET序列化API使用详解示例
    /// 包含JSON、XML、二进制序列化、数据契约序列化等
    /// 
    /// 重要说明：
    /// - JSON序列化适合跨平台数据交换，可读性好
    /// - XML序列化支持复杂对象结构，可扩展性强
    /// - 二进制序列化性能高，但平台依赖性较强
    /// - 数据契约序列化适合WCF服务，支持版本控制
    /// - 跨平台注意：二进制序列化在不同平台可能不兼容
    /// </summary>
    public class SerializationExample : MonoBehaviour
    {
        [Header("序列化示例配置")]
        [Tooltip("是否自动运行所有示例代码")]
        [SerializeField] private bool runExamples = true;
        [Tooltip("是否在序列化后清理临时文件")]
        [SerializeField] private bool cleanupFiles = true;
        [Tooltip("序列化文件编码格式")]
        [SerializeField] private string fileEncoding = "UTF-8";

        private void Start()
        {
            if (runExamples)
            {
                RunAllExamples();
            }
        }

        /// <summary>
        /// 运行所有序列化相关示例
        /// 按顺序执行：JSON -> XML -> 二进制 -> 数据契约序列化
        /// 
        /// 执行流程：
        /// 1. JSON序列化 - 轻量级数据交换格式
        /// 2. XML序列化 - 结构化数据格式
        /// 3. 二进制序列化 - 高性能序列化
        /// 4. 数据契约序列化 - 服务导向序列化
        /// </summary>
        private void RunAllExamples()
        {
            Debug.Log("=== .NET序列化API示例开始 ===");
            Debug.Log($"文件编码: {fileEncoding}");
            Debug.Log($"清理文件: {cleanupFiles}");
            
            JsonSerializationExample();
            XmlSerializationExample();
            BinarySerializationExample();
            DataContractSerializationExample();
            
            Debug.Log("=== .NET序列化API示例结束 ===");
        }

        /// <summary>
        /// JSON序列化示例
        /// 使用Unity的JsonUtility和System.Text.Json进行JSON序列化
        /// 
        /// 主要特性：
        /// - 轻量级数据交换格式
        /// - 可读性好，易于调试
        /// - 跨平台兼容性强
        /// - 支持嵌套对象和集合
        /// 
        /// 注意事项：
        /// - JsonUtility只序列化公共字段和标记为[SerializeField]的私有字段
        /// - 不支持循环引用
        /// - 不支持接口和多态
        /// - 建议使用System.Text.Json获得更好的性能
        /// </summary>
        private void JsonSerializationExample()
        {
            Debug.Log("--- JSON序列化示例 ---");
            
            try
            {
                // ========== 创建测试数据 ==========
                
                // 创建人员对象
                // 参数说明：包含基本信息和嵌套对象
                // 返回值：Person - 包含完整数据的人员对象
                var person = new Person
                {
                    Name = "张三",
                    Age = 25,
                    Email = "zhangsan@example.com",
                    Address = new Address
                    {
                        Street = "中关村大街1号",
                        City = "北京",
                        PostalCode = "100080"
                    },
                    Hobbies = new List<string> { "读书", "游泳", "编程", "游戏" }
                };
                
                // ========== 基本JSON序列化 ==========
                
                // JsonUtility.ToJson: 将对象序列化为JSON字符串
                // 参数说明：obj - 要序列化的对象, prettyPrint - 是否格式化输出
                // 返回值：string - JSON格式的字符串
                // 注意事项：prettyPrint=true会增加可读性但增加字符串长度
                string jsonString = JsonUtility.ToJson(person, true);
                Debug.Log($"序列化的JSON:\n{jsonString}");
                
                // JsonUtility.FromJson: 从JSON字符串反序列化对象
                // 参数说明：json - JSON字符串
                // 返回值：T - 反序列化的对象
                // 注意事项：确保JSON格式正确，否则会抛出异常
                Person deserializedPerson = JsonUtility.FromJson<Person>(jsonString);
                Debug.Log($"反序列化结果: {deserializedPerson.Name}, {deserializedPerson.Age}岁");
                Debug.Log($"地址: {deserializedPerson.Address.City}, {deserializedPerson.Address.Street}");
                Debug.Log($"爱好数量: {deserializedPerson.Hobbies.Count}");
                
                // ========== 复杂对象序列化 ==========
                
                // 创建公司对象，包含员工列表
                var company = new Company
                {
                    Name = "示例科技有限公司",
                    Employees = new List<Person>
                    {
                        new Person { 
                            Name = "李四", 
                            Age = 30, 
                            Email = "lisi@example.com",
                            Address = new Address { City = "上海", Street = "浦东新区" }
                        },
                        new Person { 
                            Name = "王五", 
                            Age = 28, 
                            Email = "wangwu@example.com",
                            Address = new Address { City = "深圳", Street = "南山区" }
                        },
                        new Person { 
                            Name = "赵六", 
                            Age = 35, 
                            Email = "zhaoliu@example.com",
                            Address = new Address { City = "广州", Street = "天河区" }
                        }
                    }
                };
                
                // 序列化复杂对象
                string companyJson = JsonUtility.ToJson(company, true);
                Debug.Log($"公司JSON:\n{companyJson}");
                
                // 反序列化复杂对象
                Company deserializedCompany = JsonUtility.FromJson<Company>(companyJson);
                Debug.Log($"反序列化公司: {deserializedCompany.Name}");
                Debug.Log($"员工数量: {deserializedCompany.Employees.Count}");
                foreach (var employee in deserializedCompany.Employees)
                {
                    Debug.Log($"  - {employee.Name} ({employee.Age}岁) - {employee.Email}");
                }
                
                // ========== 文件操作 ==========
                
                // 保存JSON到文件
                // 参数说明：path - 文件路径, contents - 文件内容, encoding - 编码格式
                // 返回值：void
                // 注意事项：使用Application.persistentDataPath确保跨平台兼容性
                string jsonFilePath = Path.Combine(Application.persistentDataPath, "person.json");
                File.WriteAllText(jsonFilePath, jsonString, Encoding.GetEncoding(fileEncoding));
                Debug.Log($"JSON已保存到: {jsonFilePath}");
                Debug.Log($"文件大小: {new FileInfo(jsonFilePath).Length} 字节");
                
                // 从文件读取JSON
                // 参数说明：path - 文件路径, encoding - 编码格式
                // 返回值：string - 文件内容
                // 注意事项：确保文件存在且格式正确
                string loadedJson = File.ReadAllText(jsonFilePath, Encoding.GetEncoding(fileEncoding));
                Person loadedPerson = JsonUtility.FromJson<Person>(loadedJson);
                Debug.Log($"从文件加载的人员: {loadedPerson.Name}");
                Debug.Log($"加载的爱好: [{string.Join(", ", loadedPerson.Hobbies)}]");
                
                // ========== 错误处理 ==========
                
                try
                {
                    // 测试无效JSON反序列化
                    string invalidJson = "{ \"name\": \"测试\", \"age\": \"不是数字\" }";
                    Person invalidPerson = JsonUtility.FromJson<Person>(invalidJson);
                    Debug.Log($"无效JSON处理结果: {invalidPerson.Name}, 年龄: {invalidPerson.Age}");
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"无效JSON处理异常: {ex.Message}");
                }
                
                // ========== 性能测试 ==========
                
                // 测试序列化性能
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                for (int i = 0; i < 1000; i++)
                {
                    JsonUtility.ToJson(person);
                }
                stopwatch.Stop();
                Debug.Log($"1000次序列化耗时: {stopwatch.ElapsedMilliseconds}ms");
                
                // ========== 清理文件 ==========
                
                if (cleanupFiles && File.Exists(jsonFilePath))
                {
                    File.Delete(jsonFilePath);
                    Debug.Log("临时JSON文件已清理");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON序列化操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// XML序列化示例
        /// 使用XmlSerializer进行XML序列化和反序列化
        /// 
        /// 主要特性：
        /// - 结构化数据格式，可读性好
        /// - 支持复杂对象层次结构
        /// - 可扩展性强，支持XML属性
        /// - 适合配置文件和数据交换
        /// 
        /// 注意事项：
        /// - 需要公共构造函数
        /// - 只序列化公共属性和字段
        /// - 性能相对较低
        /// - 文件大小较大
        /// </summary>
        private void XmlSerializationExample()
        {
            Debug.Log("--- XML序列化示例 ---");
            
            try
            {
                // ========== 创建测试数据 ==========
                
                // 创建书籍对象
                var book = new Book
                {
                    Title = "C#高级编程指南",
                    Author = "微软开发团队",
                    ISBN = "978-7-111-12345-6",
                    Price = 89.99m,
                    PublishedDate = DateTime.Now.AddYears(-2),
                    Categories = new List<string> { "编程", "技术", "计算机", "软件开发" }
                };
                
                // ========== XML序列化到文件 ==========
                
                // 创建XML序列化器
                // 参数说明：type - 要序列化的类型
                // 返回值：XmlSerializer - XML序列化器实例
                // 注意事项：序列化器可以重复使用，但首次使用会有性能开销
                XmlSerializer serializer = new XmlSerializer(typeof(Book));
                string xmlFilePath = Path.Combine(Application.persistentDataPath, "book.xml");
                
                // 序列化到文件
                // 参数说明：stream - 输出流, obj - 要序列化的对象
                // 返回值：void
                // 注意事项：使用using语句确保流正确关闭
                using (FileStream stream = new FileStream(xmlFilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, book);
                }
                
                Debug.Log($"XML已保存到: {xmlFilePath}");
                Debug.Log($"文件大小: {new FileInfo(xmlFilePath).Length} 字节");
                
                // ========== 读取XML文件内容 ==========
                
                // 读取XML文件内容
                string xmlContent = File.ReadAllText(xmlFilePath, Encoding.GetEncoding(fileEncoding));
                Debug.Log($"XML内容:\n{xmlContent}");
                
                // ========== XML反序列化 ==========
                
                // 从文件反序列化
                // 参数说明：stream - 输入流
                // 返回值：object - 反序列化的对象，需要类型转换
                // 注意事项：确保XML格式正确，否则会抛出异常
                using (FileStream stream = new FileStream(xmlFilePath, FileMode.Open))
                {
                    Book deserializedBook = (Book)serializer.Deserialize(stream);
                    Debug.Log($"反序列化的书籍: {deserializedBook.Title}");
                    Debug.Log($"作者: {deserializedBook.Author}");
                    Debug.Log($"ISBN: {deserializedBook.ISBN}");
                    Debug.Log($"价格: {deserializedBook.Price:C}");
                    Debug.Log($"出版日期: {deserializedBook.PublishedDate:yyyy-MM-dd}");
                    Debug.Log($"分类: [{string.Join(", ", deserializedBook.Categories)}]");
                }
                
                // ========== 字符串序列化 ==========
                
                // 使用StringWriter进行字符串序列化
                // 参数说明：textWriter - 文本写入器, obj - 要序列化的对象
                // 返回值：void
                // 注意事项：StringWriter使用StringBuilder，适合内存中的字符串操作
                using (StringWriter stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, book);
                    string xmlString = stringWriter.ToString();
                    Debug.Log($"XML字符串长度: {xmlString.Length}");
                    Debug.Log($"XML字符串前200字符: {xmlString.Substring(0, Math.Min(200, xmlString.Length))}...");
                }
                
                // ========== 从字符串反序列化 ==========
                
                // 使用StringReader从字符串反序列化
                // 参数说明：textReader - 文本读取器
                // 返回值：object - 反序列化的对象
                using (StringReader stringReader = new StringReader(xmlContent))
                {
                    Book stringBook = (Book)serializer.Deserialize(stringReader);
                    Debug.Log($"从字符串反序列化的书籍: {stringBook.Title}");
                }
                
                // ========== 错误处理 ==========
                
                try
                {
                    // 测试无效XML反序列化
                    string invalidXml = "<Book><Title>测试书籍</Title><InvalidTag>无效内容</InvalidTag></Book>";
                    using (StringReader invalidReader = new StringReader(invalidXml))
                    {
                        Book invalidBook = (Book)serializer.Deserialize(invalidReader);
                        Debug.Log($"无效XML处理结果: {invalidBook.Title}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"无效XML处理异常: {ex.Message}");
                }
                
                // ========== 性能测试 ==========
                
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                for (int i = 0; i < 100; i++) // XML序列化较慢，减少测试次数
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        serializer.Serialize(sw, book);
                    }
                }
                stopwatch.Stop();
                Debug.Log($"100次XML序列化耗时: {stopwatch.ElapsedMilliseconds}ms");
                
                // ========== 清理文件 ==========
                
                if (cleanupFiles && File.Exists(xmlFilePath))
                {
                    File.Delete(xmlFilePath);
                    Debug.Log("临时XML文件已清理");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"XML序列化操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// 二进制序列化示例
        /// 使用BinaryFormatter进行二进制序列化
        /// 
        /// 主要特性：
        /// - 高性能序列化
        /// - 文件大小较小
        /// - 支持复杂对象图
        /// - 保持对象状态完整性
        /// 
        /// 注意事项：
        /// - 平台依赖性较强
        /// - 安全性考虑，不建议用于不受信任的数据
        /// - 需要[Serializable]特性
        /// - 性能高但可读性差
        /// </summary>
        private void BinarySerializationExample()
        {
            Debug.Log("--- 二进制序列化示例 ---");
            
            try
            {
                // ========== 创建测试数据 ==========
                
                // 创建游戏数据对象
                var gameData = new GameData
                {
                    PlayerName = "游戏玩家",
                    Level = 15,
                    Score = 12500,
                    Inventory = new Dictionary<string, int>
                    {
                        { "金币", 1000 },
                        { "宝石", 50 },
                        { "药水", 25 },
                        { "装备", 10 },
                        { "材料", 200 }
                    },
                    LastSaveTime = DateTime.Now
                };
                
                // ========== 二进制序列化 ==========
                
                // 创建二进制格式化器
                // 参数说明：无
                // 返回值：BinaryFormatter - 二进制格式化器实例
                // 注意事项：BinaryFormatter已被标记为过时，建议使用其他序列化方式
                BinaryFormatter formatter = new BinaryFormatter();
                string binaryFilePath = Path.Combine(Application.persistentDataPath, "gamedata.bin");
                
                // 序列化到文件
                // 参数说明：stream - 输出流, obj - 要序列化的对象
                // 返回值：void
                // 注意事项：使用using语句确保流正确关闭
                using (FileStream stream = new FileStream(binaryFilePath, FileMode.Create))
                {
                    formatter.Serialize(stream, gameData);
                }
                
                Debug.Log($"二进制文件已保存到: {binaryFilePath}");
                Debug.Log($"文件大小: {new FileInfo(binaryFilePath).Length} 字节");
                
                // ========== 二进制反序列化 ==========
                
                // 从文件反序列化
                // 参数说明：stream - 输入流
                // 返回值：object - 反序列化的对象，需要类型转换
                // 注意事项：确保文件格式正确且类型兼容
                using (FileStream stream = new FileStream(binaryFilePath, FileMode.Open))
                {
                    GameData deserializedGameData = (GameData)formatter.Deserialize(stream);
                    Debug.Log($"反序列化的游戏数据:");
                    Debug.Log($"  玩家名称: {deserializedGameData.PlayerName}");
                    Debug.Log($"  等级: {deserializedGameData.Level}");
                    Debug.Log($"  分数: {deserializedGameData.Score}");
                    Debug.Log($"  最后保存时间: {deserializedGameData.LastSaveTime:yyyy-MM-dd HH:mm:ss}");
                    Debug.Log($"  背包物品数量: {deserializedGameData.Inventory.Count}");
                    
                    foreach (var item in deserializedGameData.Inventory)
                    {
                        Debug.Log($"    {item.Key}: {item.Value}");
                    }
                }
                
                // ========== 内存流序列化 ==========
                
                // 使用MemoryStream进行内存序列化
                // 参数说明：capacity - 初始容量（可选）
                // 返回值：MemoryStream - 内存流实例
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // 序列化到内存流
                    formatter.Serialize(memoryStream, gameData);
                    
                    // 获取序列化后的字节数组
                    byte[] serializedData = memoryStream.ToArray();
                    Debug.Log($"内存序列化数据大小: {serializedData.Length} 字节");
                    
                    // 从内存流反序列化
                    memoryStream.Position = 0; // 重置流位置
                    GameData memoryGameData = (GameData)formatter.Deserialize(memoryStream);
                    Debug.Log($"从内存反序列化的玩家: {memoryGameData.PlayerName}");
                }
                
                // ========== 性能测试 ==========
                
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                for (int i = 0; i < 1000; i++)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        formatter.Serialize(ms, gameData);
                    }
                }
                stopwatch.Stop();
                Debug.Log($"1000次二进制序列化耗时: {stopwatch.ElapsedMilliseconds}ms");
                
                // ========== 错误处理 ==========
                
                try
                {
                    // 测试损坏文件的反序列化
                    string corruptedFilePath = Path.Combine(Application.persistentDataPath, "corrupted.bin");
                    File.WriteAllBytes(corruptedFilePath, new byte[] { 0x00, 0x01, 0x02, 0x03 });
                    
                    using (FileStream stream = new FileStream(corruptedFilePath, FileMode.Open))
                    {
                        GameData corruptedData = (GameData)formatter.Deserialize(stream);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"损坏文件反序列化异常: {ex.Message}");
                }
                
                // ========== 清理文件 ==========
                
                if (cleanupFiles)
                {
                    if (File.Exists(binaryFilePath))
                    {
                        File.Delete(binaryFilePath);
                        Debug.Log("临时二进制文件已清理");
                    }
                    
                    string corruptedFilePath = Path.Combine(Application.persistentDataPath, "corrupted.bin");
                    if (File.Exists(corruptedFilePath))
                    {
                        File.Delete(corruptedFilePath);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"二进制序列化操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// 数据契约序列化示例
        /// </summary>
        private void DataContractSerializationExample()
        {
            Debug.Log("--- 数据契约序列化示例 ---");
            
            try
            {
                // 创建测试数据
                var order = new Order
                {
                    OrderId = "ORD-2024-001",
                    CustomerName = "客户A",
                    OrderDate = DateTime.Now,
                    TotalAmount = 299.99m,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = "P001", ProductName = "产品1", Quantity = 2, UnitPrice = 99.99m },
                        new OrderItem { ProductId = "P002", ProductName = "产品2", Quantity = 1, UnitPrice = 100.01m }
                    }
                };
                
                // 数据契约序列化（使用XML）
                var dataContractSerializer = new DataContractSerializer(typeof(Order));
                string xmlFilePath = Path.Combine(Application.persistentDataPath, "order.xml");
                
                using (FileStream stream = new FileStream(xmlFilePath, FileMode.Create))
                {
                    dataContractSerializer.WriteObject(stream, order);
                }
                
                Debug.Log($"数据契约XML已保存到: {xmlFilePath}");
                
                // 读取XML内容
                string xmlContent = File.ReadAllText(xmlFilePath, Encoding.UTF8);
                Debug.Log($"数据契约XML内容:\n{xmlContent}");
                
                // 数据契约反序列化
                using (FileStream stream = new FileStream(xmlFilePath, FileMode.Open))
                {
                    Order deserializedOrder = (Order)dataContractSerializer.ReadObject(stream);
                    Debug.Log($"反序列化的订单: {deserializedOrder.OrderId}, 总金额: {deserializedOrder.TotalAmount}");
                    Debug.Log($"订单项目数量: {deserializedOrder.Items.Count}");
                }
                
                // 使用XmlDictionaryWriter进行更精细的控制
                using (FileStream stream = new FileStream(xmlFilePath.Replace(".xml", "_dict.xml"), FileMode.Create))
                {
                    using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                    {
                        dataContractSerializer.WriteObject(writer, order);
                    }
                }
                
                // 清理文件
                if (File.Exists(xmlFilePath))
                {
                    File.Delete(xmlFilePath);
                }
                if (File.Exists(xmlFilePath.Replace(".xml", "_dict.xml")))
                {
                    File.Delete(xmlFilePath.Replace(".xml", "_dict.xml"));
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"数据契约序列化操作出错: {ex.Message}");
            }
        }
    }

    #region 数据模型

    [Serializable]
    public class Person
    {
        public string Name;
        public int Age;
        public string Email;
        public Address Address;
        public List<string> Hobbies;
    }

    [Serializable]
    public class Address
    {
        public string Street;
        public string City;
        public string PostalCode;
    }

    [Serializable]
    public class Company
    {
        public string Name;
        public List<Person> Employees;
    }

    [Serializable]
    public class Book
    {
        public string Title;
        public string Author;
        public string ISBN;
        public decimal Price;
        public DateTime PublishedDate;
        public List<string> Categories;
    }

    [Serializable]
    public class GameData
    {
        public string PlayerName;
        public int Level;
        public int Score;
        public Dictionary<string, int> Inventory;
        public DateTime LastSaveTime;
    }

    [DataContract]
    public class Order
    {
        [DataMember]
        public string OrderId { get; set; }
        
        [DataMember]
        public string CustomerName { get; set; }
        
        [DataMember]
        public DateTime OrderDate { get; set; }
        
        [DataMember]
        public decimal TotalAmount { get; set; }
        
        [DataMember]
        public List<OrderItem> Items { get; set; }
    }

    [DataContract]
    public class OrderItem
    {
        [DataMember]
        public string ProductId { get; set; }
        
        [DataMember]
        public string ProductName { get; set; }
        
        [DataMember]
        public int Quantity { get; set; }
        
        [DataMember]
        public decimal UnitPrice { get; set; }
    }

    #endregion
} 