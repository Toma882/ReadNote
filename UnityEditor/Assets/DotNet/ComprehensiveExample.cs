// ComprehensiveExample.cs
// .NET综合案例 - 数据管理系统
// 涵盖Collections、IO、Networking、Threading、Serialization、Reflection等所有知识点
// 实现一个完整的数据采集、处理、存储、分析系统

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;
using UnityEngine;
using System.Net;
using System.Text;

namespace DotNet.Comprehensive
{
    /// <summary>
    /// .NET综合案例 - 数据管理系统
    /// 
    /// 系统功能：
    /// 1. 数据采集 - 从网络API获取数据
    /// 2. 数据处理 - 使用集合和LINQ处理数据
    /// 3. 数据存储 - 文件IO和序列化
    /// 4. 数据分析 - 反射和动态处理
    /// 5. 并发处理 - 多线程和异步操作
    /// 6. 实时监控 - 文件系统监控
    /// 
    /// 涵盖的知识点：
    /// - Collections: List、Dictionary、ConcurrentQueue、LINQ
    /// - IO: 文件操作、流处理、异步IO
    /// - Networking: HttpClient、异步网络请求
    /// - Threading: Task、async/await、并发集合
    /// - Serialization: JSON、XML序列化
    /// - Reflection: 动态类型处理、属性访问
    /// </summary>
    public class ComprehensiveExample : MonoBehaviour
    {
        [Header("系统配置")]
        [SerializeField] private bool autoStart = true;
        [SerializeField] private int maxConcurrentTasks = 4;
        [SerializeField] private string dataApiUrl = "https://jsonplaceholder.typicode.com/posts";
        [SerializeField] private float updateInterval = 5f;
        
        [Header("系统状态")]
        [SerializeField] private int totalDataCount = 0;
        [SerializeField] private int processedDataCount = 0;
        [SerializeField] private bool isRunning = false;
        [SerializeField] private string currentStatus = "未启动";
        
        // ================= Collections 相关 =================
        private List<DataItem> dataItems = new List<DataItem>();
        private ConcurrentQueue<DataItem> processingQueue = new ConcurrentQueue<DataItem>();
        private Dictionary<int, DataItem> dataCache = new Dictionary<int, DataItem>();
        private HashSet<string> processedIds = new HashSet<string>();
        
        // ================= IO 相关 =================
        private string dataDirectory;
        private string jsonFilePath;
        private string xmlFilePath;
        private FileSystemWatcher fileWatcher;
        
        // ================= Networking 相关 =================
        private HttpClient httpClient;
        private CancellationTokenSource cancellationTokenSource;
        
        // ================= Threading 相关 =================
        private SemaphoreSlim semaphore;
        private Task[] processingTasks;
        
        // ================= Serialization 相关 =================
        private JsonSerializerOptions jsonOptions;
        private XmlSerializer xmlSerializer;
        
        // ================= Reflection 相关 =================
        private Type dataItemType;
        private PropertyInfo[] dataItemProperties;
        
        private void Start()
        {
            InitializeSystem();
            
            if (autoStart)
            {
                StartDataProcessing();
            }
        }
        
        /// <summary>
        /// 初始化系统
        /// 设置所有必要的组件和配置
        /// </summary>
        private void InitializeSystem()
        {
            Debug.Log("=== 初始化数据管理系统 ===");
            
            // ================= Collections 初始化 =================
            dataItems = new List<DataItem>();
            processingQueue = new ConcurrentQueue<DataItem>();
            dataCache = new Dictionary<int, DataItem>();
            processedIds = new HashSet<string>();
            
            // ================= IO 初始化 =================
            dataDirectory = Path.Combine(Application.persistentDataPath, "DataSystem");
            jsonFilePath = Path.Combine(dataDirectory, "data.json");
            xmlFilePath = Path.Combine(dataDirectory, "data.xml");
            
            // 创建数据目录
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
                Debug.Log($"创建数据目录: {dataDirectory}");
            }
            
            // 初始化文件监控
            InitializeFileWatcher();
            
            // ================= Networking 初始化 =================
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "DataSystem/1.0");
            
            cancellationTokenSource = new CancellationTokenSource();
            
            // ================= Threading 初始化 =================
            semaphore = new SemaphoreSlim(maxConcurrentTasks, maxConcurrentTasks);
            processingTasks = new Task[maxConcurrentTasks];
            
            // ================= Serialization 初始化 =================
            jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            xmlSerializer = new XmlSerializer(typeof(List<DataItem>));
            
            // ================= Reflection 初始化 =================
            dataItemType = typeof(DataItem);
            dataItemProperties = dataItemType.GetProperties();
            
            Debug.Log($"反射获取到 {dataItemProperties.Length} 个属性");
            foreach (var prop in dataItemProperties)
            {
                Debug.Log($"  - {prop.Name}: {prop.PropertyType.Name}");
            }
            
            Debug.Log("系统初始化完成");
        }
        
        /// <summary>
        /// 启动数据处理系统
        /// </summary>
        public async void StartDataProcessing()
        {
            if (isRunning)
            {
                Debug.LogWarning("系统已在运行中");
                return;
            }
            
            isRunning = true;
            currentStatus = "启动中...";
            
            Debug.Log("=== 启动数据管理系统 ===");
            
            try
            {
                // 启动并发处理任务
                StartConcurrentProcessing();
                
                // 开始数据采集循环
                await StartDataCollectionLoop();
            }
            catch (Exception ex)
            {
                Debug.LogError($"系统启动失败: {ex.Message}");
                currentStatus = "启动失败";
                isRunning = false;
            }
        }
        
        /// <summary>
        /// 启动并发处理任务
        /// 使用Threading知识点
        /// </summary>
        private void StartConcurrentProcessing()
        {
            Debug.Log($"启动 {maxConcurrentTasks} 个并发处理任务");
            
            for (int i = 0; i < maxConcurrentTasks; i++)
            {
                int taskId = i;
                processingTasks[i] = Task.Run(async () =>
                {
                    await ProcessDataItems(taskId);
                }, cancellationTokenSource.Token);
            }
        }
        
        /// <summary>
        /// 数据处理任务
        /// 使用Collections和Threading知识点
        /// </summary>
        private async Task ProcessDataItems(int taskId)
        {
            Debug.Log($"处理任务 {taskId} 启动");
            
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    // 等待信号量
                    await semaphore.WaitAsync(cancellationTokenSource.Token);
                    
                    // 从队列中获取数据
                    if (processingQueue.TryDequeue(out DataItem item))
                    {
                        currentStatus = $"任务{taskId}处理数据: {item.Id}";
                        
                        // 使用Reflection动态处理数据
                        await ProcessDataItemWithReflection(item);
                        
                        // 更新统计
                        Interlocked.Increment(ref processedDataCount);
                        
                        Debug.Log($"任务{taskId}处理完成: {item.Id}");
                    }
                    else
                    {
                        // 队列为空，等待一段时间
                        await Task.Delay(100, cancellationTokenSource.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    Debug.Log($"处理任务 {taskId} 被取消");
                    break;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"处理任务 {taskId} 出错: {ex.Message}");
                }
                finally
                {
                    semaphore.Release();
                }
            }
            
            Debug.Log($"处理任务 {taskId} 结束");
        }
        
        /// <summary>
        /// 使用反射处理数据项
        /// 使用Reflection知识点
        /// </summary>
        private async Task ProcessDataItemWithReflection(DataItem item)
        {
            // 使用反射获取所有属性
            foreach (var property in dataItemProperties)
            {
                try
                {
                    var value = property.GetValue(item);
                    
                    // 根据属性类型进行不同处理
                    if (property.PropertyType == typeof(string))
                    {
                        // 字符串处理
                        if (value != null)
                        {
                            string strValue = value.ToString();
                            if (strValue.Length > 100)
                            {
                                // 截断长字符串
                                property.SetValue(item, strValue.Substring(0, 100) + "...");
                            }
                        }
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        // 数值处理
                        if (value != null)
                        {
                            int intValue = (int)value;
                            if (intValue < 0)
                            {
                                property.SetValue(item, 0);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"反射处理属性 {property.Name} 时出错: {ex.Message}");
                }
            }
            
            // 模拟处理时间
            await Task.Delay(50);
        }
        
        /// <summary>
        /// 数据采集循环
        /// 使用Networking和Threading知识点
        /// </summary>
        private async Task StartDataCollectionLoop()
        {
            Debug.Log("开始数据采集循环");
            
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    currentStatus = "采集数据中...";
                    
                    // 从网络API获取数据
                    var newData = await FetchDataFromApi();
                    
                    if (newData != null && newData.Any())
                    {
                        // 使用Collections处理新数据
                        ProcessNewData(newData);
                        
                        // 保存数据到文件
                        await SaveDataToFiles();
                        
                        currentStatus = $"已处理 {processedDataCount}/{totalDataCount} 条数据";
                    }
                    
                    // 等待下次采集
                    await Task.Delay((int)(updateInterval * 1000), cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("数据采集循环被取消");
                    break;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"数据采集出错: {ex.Message}");
                    currentStatus = "采集出错";
                    await Task.Delay(5000, cancellationTokenSource.Token);
                }
            }
        }
        
        /// <summary>
        /// 从API获取数据
        /// 使用Networking知识点
        /// </summary>
        private async Task<List<DataItem>> FetchDataFromApi()
        {
            try
            {
                Debug.Log($"从API获取数据: {dataApiUrl}");
                
                var response = await httpClient.GetStringAsync(dataApiUrl);
                
                // 使用Serialization反序列化JSON
                var apiData = JsonSerializer.Deserialize<List<ApiDataItem>>(response, jsonOptions);
                
                // 转换为内部数据格式
                var dataItems = apiData.Select(item => new DataItem
                {
                    Id = item.Id,
                    Title = item.Title,
                    Body = item.Body,
                    UserId = item.UserId,
                    Timestamp = DateTime.Now,
                    Status = DataStatus.New
                }).ToList();
                
                Debug.Log($"获取到 {dataItems.Count} 条数据");
                return dataItems;
            }
            catch (Exception ex)
            {
                Debug.LogError($"API请求失败: {ex.Message}");
                return new List<DataItem>();
            }
        }
        
        /// <summary>
        /// 处理新数据
        /// 使用Collections知识点
        /// </summary>
        private void ProcessNewData(List<DataItem> newData)
        {
            Debug.Log($"处理 {newData.Count} 条新数据");
            
            foreach (var item in newData)
            {
                // 检查是否已处理过
                string itemKey = $"{item.UserId}_{item.Id}";
                if (!processedIds.Contains(itemKey))
                {
                    // 添加到缓存
                    dataCache[item.Id] = item;
                    
                    // 添加到主列表
                    dataItems.Add(item);
                    
                    // 添加到处理队列
                    processingQueue.Enqueue(item);
                    
                    // 标记为已处理
                    processedIds.Add(itemKey);
                    
                    // 更新总数
                    Interlocked.Increment(ref totalDataCount);
                }
            }
            
            Debug.Log($"数据缓存大小: {dataCache.Count}");
            Debug.Log($"处理队列大小: {processingQueue.Count}");
        }
        
        /// <summary>
        /// 保存数据到文件
        /// 使用IO和Serialization知识点
        /// </summary>
        private async Task SaveDataToFiles()
        {
            try
            {
                // 保存为JSON格式
                await SaveAsJson();
                
                // 保存为XML格式
                await SaveAsXml();
                
                Debug.Log("数据保存完成");
            }
            catch (Exception ex)
            {
                Debug.LogError($"保存数据失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 保存为JSON格式
        /// 使用IO和Serialization知识点
        /// </summary>
        private async Task SaveAsJson()
        {
            var jsonData = JsonSerializer.Serialize(dataItems, jsonOptions);
            await File.WriteAllTextAsync(jsonFilePath, jsonData, Encoding.UTF8);
            
            Debug.Log($"JSON数据已保存: {jsonFilePath}");
            Debug.Log($"文件大小: {new FileInfo(jsonFilePath).Length} 字节");
        }
        
        /// <summary>
        /// 保存为XML格式
        /// 使用IO和Serialization知识点
        /// </summary>
        private async Task SaveAsXml()
        {
            using (var stream = new FileStream(xmlFilePath, FileMode.Create))
            {
                xmlSerializer.Serialize(stream, dataItems);
            }
            
            Debug.Log($"XML数据已保存: {xmlFilePath}");
            Debug.Log($"文件大小: {new FileInfo(xmlFilePath).Length} 字节");
        }
        
        /// <summary>
        /// 初始化文件监控
        /// 使用IO知识点
        /// </summary>
        private void InitializeFileWatcher()
        {
            try
            {
                fileWatcher = new FileSystemWatcher(dataDirectory)
                {
                    Filter = "*.json",
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                    EnableRaisingEvents = true
                };
                
                fileWatcher.Changed += OnFileChanged;
                fileWatcher.Created += OnFileCreated;
                fileWatcher.Deleted += OnFileDeleted;
                
                Debug.Log($"文件监控已启动: {dataDirectory}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"文件监控启动失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 文件变化事件处理
        /// 使用IO知识点
        /// </summary>
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            Debug.Log($"文件已修改: {e.Name}");
        }
        
        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            Debug.Log($"文件已创建: {e.Name}");
        }
        
        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            Debug.Log($"文件已删除: {e.Name}");
        }
        
        /// <summary>
        /// 停止系统
        /// </summary>
        public async void StopDataProcessing()
        {
            if (!isRunning)
            {
                Debug.LogWarning("系统未在运行");
                return;
            }
            
            Debug.Log("=== 停止数据管理系统 ===");
            
            currentStatus = "停止中...";
            
            // 取消所有任务
            cancellationTokenSource.Cancel();
            
            // 等待所有处理任务完成
            if (processingTasks != null)
            {
                await Task.WhenAll(processingTasks);
            }
            
            // 保存最终数据
            await SaveDataToFiles();
            
            // 清理资源
            httpClient?.Dispose();
            fileWatcher?.Dispose();
            semaphore?.Dispose();
            cancellationTokenSource?.Dispose();
            
            isRunning = false;
            currentStatus = "已停止";
            
            Debug.Log("系统已停止");
        }
        
        /// <summary>
        /// 获取系统统计信息
        /// 使用Collections和LINQ知识点
        /// </summary>
        public void GetSystemStatistics()
        {
            Debug.Log("=== 系统统计信息 ===");
            Debug.Log($"总数据量: {totalDataCount}");
            Debug.Log($"已处理数据: {processedDataCount}");
            Debug.Log($"处理队列大小: {processingQueue.Count}");
            Debug.Log($"数据缓存大小: {dataCache.Count}");
            Debug.Log($"已处理ID数量: {processedIds.Count}");
            
            // 使用LINQ统计
            var statusGroups = dataItems.GroupBy(x => x.Status)
                                       .Select(g => new { Status = g.Key, Count = g.Count() })
                                       .OrderBy(x => x.Status);
            
            Debug.Log("数据状态分布:");
            foreach (var group in statusGroups)
            {
                Debug.Log($"  {group.Status}: {group.Count}");
            }
            
            // 使用LINQ查找最新数据
            var latestData = dataItems.OrderByDescending(x => x.Timestamp).Take(5);
            Debug.Log("最新5条数据:");
            foreach (var item in latestData)
            {
                Debug.Log($"  {item.Id}: {item.Title}");
            }
        }
        
        private void OnDestroy()
        {
            StopDataProcessing();
        }
        
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 400, 300));
            
            GUILayout.Label("数据管理系统", EditorStyles.boldLabel);
            GUILayout.Space(10);
            
            GUILayout.Label($"状态: {currentStatus}");
            GUILayout.Label($"总数据: {totalDataCount}");
            GUILayout.Label($"已处理: {processedDataCount}");
            GUILayout.Label($"队列大小: {processingQueue.Count}");
            GUILayout.Label($"缓存大小: {dataCache.Count}");
            
            GUILayout.Space(10);
            
            if (!isRunning)
            {
                if (GUILayout.Button("启动系统"))
                {
                    StartDataProcessing();
                }
            }
            else
            {
                if (GUILayout.Button("停止系统"))
                {
                    StopDataProcessing();
                }
            }
            
            if (GUILayout.Button("获取统计信息"))
            {
                GetSystemStatistics();
            }
            
            GUILayout.EndArea();
        }
    }
    
    /// <summary>
    /// 数据项类
    /// </summary>
    [Serializable]
    public class DataItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public DataStatus Status { get; set; }
        
        public override string ToString()
        {
            return $"DataItem[Id={Id}, Title={Title}, Status={Status}]";
        }
    }
    
    /// <summary>
    /// API数据项类
    /// </summary>
    public class ApiDataItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
    
    /// <summary>
    /// 数据状态枚举
    /// </summary>
    public enum DataStatus
    {
        New,
        Processing,
        Completed,
        Error
    }
} 