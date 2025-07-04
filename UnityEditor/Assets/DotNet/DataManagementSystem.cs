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
using UnityEngine;

namespace DotNet.Examples
{
    /// <summary>
    /// 数据管理系统 - .NET综合案例
    /// 涵盖Collections、IO、Networking、Threading、Serialization、Reflection等所有知识点
    /// </summary>
    public class DataManagementSystem : MonoBehaviour
    {
        [Header("系统配置")]
        [SerializeField] private bool autoStart = true;
        [SerializeField] private int maxConcurrentTasks = 3;
        [SerializeField] private string apiUrl = "https://jsonplaceholder.typicode.com/posts";
        
        [Header("系统状态")]
        [SerializeField] private int totalDataCount = 0;
        [SerializeField] private int processedCount = 0;
        [SerializeField] private bool isRunning = false;
        
        // Collections: 各种集合类型
        private List<DataItem> dataList = new List<DataItem>();
        private Dictionary<int, DataItem> dataCache = new Dictionary<int, DataItem>();
        private ConcurrentQueue<DataItem> processingQueue = new ConcurrentQueue<DataItem>();
        private HashSet<string> processedIds = new HashSet<string>();
        
        // IO: 文件路径
        private string dataPath;
        private string jsonPath;
        
        // Networking: HTTP客户端
        private HttpClient httpClient;
        
        // Threading: 并发控制
        private SemaphoreSlim semaphore;
        private CancellationTokenSource cancellationToken;
        private Task[] processingTasks;
        
        // Serialization: JSON选项
        private JsonSerializerOptions jsonOptions;
        
        // Reflection: 类型信息
        private Type dataItemType;
        private PropertyInfo[] properties;
        
        private void Start()
        {
            InitializeSystem();
            if (autoStart) StartSystem();
        }
        
        /// <summary>
        /// 初始化系统 - 涵盖所有.NET知识点
        /// </summary>
        private void InitializeSystem()
        {
            Debug.Log("=== 初始化数据管理系统 ===");
            
            // Collections: 初始化集合
            dataList = new List<DataItem>();
            dataCache = new Dictionary<int, DataItem>();
            processingQueue = new ConcurrentQueue<DataItem>();
            processedIds = new HashSet<string>();
            
            // IO: 设置文件路径
            dataPath = Path.Combine(Application.persistentDataPath, "DataSystem");
            jsonPath = Path.Combine(dataPath, "data.json");
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
                Debug.Log($"创建数据目录: {dataPath}");
            }
            
            // Networking: 初始化HTTP客户端
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            
            // Threading: 初始化并发控制
            semaphore = new SemaphoreSlim(maxConcurrentTasks, maxConcurrentTasks);
            cancellationToken = new CancellationTokenSource();
            processingTasks = new Task[maxConcurrentTasks];
            
            // Serialization: 配置JSON序列化
            jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            // Reflection: 获取类型信息
            dataItemType = typeof(DataItem);
            properties = dataItemType.GetProperties();
            
            Debug.Log($"反射获取到 {properties.Length} 个属性");
            foreach (var prop in properties)
            {
                Debug.Log($"  - {prop.Name}: {prop.PropertyType.Name}");
            }
            
            Debug.Log("系统初始化完成");
        }
        
        /// <summary>
        /// 启动系统
        /// </summary>
        public async void StartSystem()
        {
            if (isRunning) return;
            
            isRunning = true;
            Debug.Log("=== 启动数据管理系统 ===");
            
            // 启动并发处理任务
            StartProcessingTasks();
            
            // 开始数据采集循环
            await DataCollectionLoop();
        }
        
        /// <summary>
        /// 启动并发处理任务 - Threading知识点
        /// </summary>
        private void StartProcessingTasks()
        {
            Debug.Log($"启动 {maxConcurrentTasks} 个并发处理任务");
            
            for (int i = 0; i < maxConcurrentTasks; i++)
            {
                int taskId = i;
                processingTasks[i] = Task.Run(async () =>
                {
                    await ProcessDataTask(taskId);
                }, cancellationToken.Token);
            }
        }
        
        /// <summary>
        /// 数据处理任务 - Threading + Collections知识点
        /// </summary>
        private async Task ProcessDataTask(int taskId)
        {
            Debug.Log($"处理任务 {taskId} 启动");
            
            while (!cancellationToken.Token.IsCancellationRequested)
            {
                try
                {
                    await semaphore.WaitAsync(cancellationToken.Token);
                    
                    if (processingQueue.TryDequeue(out DataItem item))
                    {
                        // 使用Reflection处理数据
                        await ProcessWithReflection(item);
                        
                        Interlocked.Increment(ref processedCount);
                        Debug.Log($"任务{taskId}处理完成: {item.Id}");
                    }
                    else
                    {
                        await Task.Delay(100, cancellationToken.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                finally
                {
                    semaphore.Release();
                }
            }
            
            Debug.Log($"处理任务 {taskId} 结束");
        }
        
        /// <summary>
        /// 使用反射处理数据 - Reflection知识点
        /// </summary>
        private async Task ProcessWithReflection(DataItem item)
        {
            foreach (var property in properties)
            {
                try
                {
                    var value = property.GetValue(item);
                    
                    if (property.PropertyType == typeof(string) && value != null)
                    {
                        string strValue = value.ToString();
                        if (strValue.Length > 50)
                        {
                            property.SetValue(item, strValue.Substring(0, 50) + "...");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"反射处理属性 {property.Name} 时出错: {ex.Message}");
                }
            }
            
            await Task.Delay(50); // 模拟处理时间
        }
        
        /// <summary>
        /// 数据采集循环 - Networking + Threading知识点
        /// </summary>
        private async Task DataCollectionLoop()
        {
            Debug.Log("开始数据采集循环");
            
            while (!cancellationToken.Token.IsCancellationRequested)
            {
                try
                {
                    // 从网络获取数据
                    var newData = await FetchDataFromApi();
                    
                    if (newData != null && newData.Any())
                    {
                        // 处理新数据
                        ProcessNewData(newData);
                        
                        // 保存数据
                        await SaveDataToFile();
                    }
                    
                    await Task.Delay(5000, cancellationToken.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"数据采集出错: {ex.Message}");
                    await Task.Delay(5000, cancellationToken.Token);
                }
            }
        }
        
        /// <summary>
        /// 从API获取数据 - Networking + Serialization知识点
        /// </summary>
        private async Task<List<DataItem>> FetchDataFromApi()
        {
            try
            {
                Debug.Log($"从API获取数据: {apiUrl}");
                
                var response = await httpClient.GetStringAsync(apiUrl);
                var apiData = JsonSerializer.Deserialize<List<ApiDataItem>>(response, jsonOptions);
                
                var dataItems = apiData.Select(item => new DataItem
                {
                    Id = item.Id,
                    Title = item.Title,
                    Body = item.Body,
                    UserId = item.UserId,
                    Timestamp = DateTime.Now
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
        /// 处理新数据 - Collections知识点
        /// </summary>
        private void ProcessNewData(List<DataItem> newData)
        {
            Debug.Log($"处理 {newData.Count} 条新数据");
            
            foreach (var item in newData)
            {
                string itemKey = $"{item.UserId}_{item.Id}";
                if (!processedIds.Contains(itemKey))
                {
                    dataCache[item.Id] = item;
                    dataList.Add(item);
                    processingQueue.Enqueue(item);
                    processedIds.Add(itemKey);
                    Interlocked.Increment(ref totalDataCount);
                }
            }
            
            Debug.Log($"数据缓存: {dataCache.Count}, 队列: {processingQueue.Count}");
        }
        
        /// <summary>
        /// 保存数据到文件 - IO + Serialization知识点
        /// </summary>
        private async Task SaveDataToFile()
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(dataList, jsonOptions);
                await File.WriteAllTextAsync(jsonPath, jsonData);
                
                Debug.Log($"数据已保存: {jsonPath}");
                Debug.Log($"文件大小: {new FileInfo(jsonPath).Length} 字节");
            }
            catch (Exception ex)
            {
                Debug.LogError($"保存数据失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 获取统计信息 - Collections + LINQ知识点
        /// </summary>
        public void GetStatistics()
        {
            Debug.Log("=== 系统统计信息 ===");
            Debug.Log($"总数据: {totalDataCount}");
            Debug.Log($"已处理: {processedCount}");
            Debug.Log($"队列大小: {processingQueue.Count}");
            Debug.Log($"缓存大小: {dataCache.Count}");
            
            // 使用LINQ统计
            var userGroups = dataList.GroupBy(x => x.UserId)
                                   .Select(g => new { UserId = g.Key, Count = g.Count() })
                                   .OrderByDescending(x => x.Count)
                                   .Take(5);
            
            Debug.Log("用户数据分布(前5):");
            foreach (var group in userGroups)
            {
                Debug.Log($"  用户{group.UserId}: {group.Count}条");
            }
        }
        
        /// <summary>
        /// 停止系统
        /// </summary>
        public async void StopSystem()
        {
            if (!isRunning) return;
            
            Debug.Log("=== 停止数据管理系统 ===");
            
            cancellationToken.Cancel();
            
            if (processingTasks != null)
            {
                await Task.WhenAll(processingTasks);
            }
            
            await SaveDataToFile();
            
            httpClient?.Dispose();
            semaphore?.Dispose();
            cancellationToken?.Dispose();
            
            isRunning = false;
            Debug.Log("系统已停止");
        }
        
        private void OnDestroy()
        {
            StopSystem();
        }
        
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 200));
            
            GUILayout.Label("数据管理系统", EditorStyles.boldLabel);
            GUILayout.Space(10);
            
            GUILayout.Label($"状态: {(isRunning ? "运行中" : "已停止")}");
            GUILayout.Label($"总数据: {totalDataCount}");
            GUILayout.Label($"已处理: {processedCount}");
            GUILayout.Label($"队列: {processingQueue.Count}");
            
            GUILayout.Space(10);
            
            if (!isRunning)
            {
                if (GUILayout.Button("启动系统"))
                {
                    StartSystem();
                }
            }
            else
            {
                if (GUILayout.Button("停止系统"))
                {
                    StopSystem();
                }
            }
            
            if (GUILayout.Button("获取统计"))
            {
                GetStatistics();
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
        
        public override string ToString()
        {
            return $"DataItem[Id={Id}, Title={Title}]";
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
} 