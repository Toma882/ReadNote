using UnityEngine;
using Unity.SearchService;

/// <summary>
/// UnityEngine.SearchService 命名空间案例演示
/// 展示搜索服务、索引管理、查询优化等核心功能
/// </summary>
public class SearchServiceExample : MonoBehaviour
{
    [Header("搜索服务配置")]
    [SerializeField] private bool enableSearchService = true; //启用搜索服务
    [SerializeField] private bool enableIndexing = true; //启用索引
    [SerializeField] private bool enableQueryOptimization = true; //启用查询优化
    [SerializeField] private bool enableSearchProfiling = true; //启用搜索分析
    [SerializeField] private bool enableAsyncSearch = true; //启用异步搜索
    
    [Header("索引配置")]
    [SerializeField] private int maxIndexSize = 10000; //最大索引大小
    [SerializeField] private float indexUpdateInterval = 1f; //索引更新间隔
    [SerializeField] private bool enableAutoIndexing = true; //启用自动索引
    [SerializeField] private bool enableIncrementalIndexing = true; //启用增量索引
    [SerializeField] private string indexStoragePath = "SearchIndex"; //索引存储路径
    
    [Header("搜索参数")]
    [SerializeField] private string searchQuery = ""; //搜索查询
    [SerializeField] private int maxSearchResults = 100; //最大搜索结果数
    [SerializeField] private float searchTimeout = 5f; //搜索超时时间
    [SerializeField] private bool enableFuzzySearch = true; //启用模糊搜索
    [SerializeField] private bool enableCaseSensitive = false; //启用大小写敏感
    [SerializeField] private float fuzzyThreshold = 0.8f; //模糊阈值
    
    [Header("搜索类型")]
    [SerializeField] private bool enableTextSearch = true; //启用文本搜索
    [SerializeField] private bool enableTagSearch = true; //启用标签搜索
    [SerializeField] private bool enableTypeSearch = true; //启用类型搜索
    [SerializeField] private bool enablePropertySearch = true; //启用属性搜索
    [SerializeField] private bool enableHierarchySearch = true; //启用层级搜索
    
    [Header("性能监控")]
    [SerializeField] private bool enableSearchMonitoring = true; //启用搜索监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logSearchData = false; //记录搜索数据
    [SerializeField] private int totalSearches = 0; //总搜索次数
    [SerializeField] private float averageSearchTime = 0f; //平均搜索时间
    [SerializeField] private int indexedItems = 0; //已索引项目数
    
    [Header("搜索状态")]
    [SerializeField] private string searchServiceState = "未初始化"; //搜索服务状态
    [SerializeField] private string currentSearchStatus = "空闲"; //当前搜索状态
    [SerializeField] private int lastSearchResults = 0; //上次搜索结果数
    [SerializeField] private float lastSearchTime = 0f; //上次搜索时间
    [SerializeField] private string lastSearchQuery = ""; //上次搜索查询
    
    [Header("性能数据")]
    [SerializeField] private float[] searchTimeHistory = new float[100]; //搜索时间历史
    [SerializeField] private int searchTimeIndex = 0; //搜索时间索引
    [SerializeField] private float[] resultCountHistory = new float[100]; //结果数量历史
    [SerializeField] private int resultCountIndex = 0; //结果数量索引
    
    private SearchService searchService;
    private SearchIndex searchIndex;
    private System.Collections.Generic.List<SearchItem> searchItems = new System.Collections.Generic.List<SearchItem>();
    private System.Collections.Generic.List<SearchResult> lastResults = new System.Collections.Generic.List<SearchResult>();
    private float lastIndexUpdateTime = 0f;
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;

    private void Start()
    {
        InitializeSearchService();
    }

    /// <summary>
    /// 初始化搜索服务
    /// </summary>
    private void InitializeSearchService()
    {
        // 创建搜索服务
        searchService = new SearchService();
        
        // 初始化搜索索引
        InitializeSearchIndex();
        
        // 初始化搜索项目
        InitializeSearchItems();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置搜索服务
        ConfigureSearchService();
        
        isInitialized = true;
        searchServiceState = "已初始化";
        Debug.Log("搜索服务初始化完成");
    }

    /// <summary>
    /// 初始化搜索索引
    /// </summary>
    private void InitializeSearchIndex()
    {
        searchIndex = new SearchIndex();
        searchIndex.MaxSize = maxIndexSize;
        searchIndex.EnableIncrementalIndexing = enableIncrementalIndexing;
        
        Debug.Log($"搜索索引初始化完成: 最大大小={maxIndexSize}");
    }

    /// <summary>
    /// 初始化搜索项目
    /// </summary>
    private void InitializeSearchItems()
    {
        // 创建示例搜索项目
        CreateSampleSearchItems();
        
        // 构建初始索引
        if (enableIndexing)
        {
            BuildSearchIndex();
        }
        
        Debug.Log($"搜索项目初始化完成: 项目数={searchItems.Count}");
    }

    /// <summary>
    /// 创建示例搜索项目
    /// </summary>
    private void CreateSampleSearchItems()
    {
        // 创建游戏对象搜索项目
        for (int i = 0; i < 100; i++)
        {
            var item = new SearchItem
            {
                id = $"GameObject_{i}",
                name = $"GameObject {i}",
                type = "GameObject",
                tags = new string[] { "object", "game", "unity" },
                properties = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "position", $"({Random.Range(-10f, 10f)}, {Random.Range(-10f, 10f)}, {Random.Range(-10f, 10f)})" },
                    { "scale", $"({Random.Range(0.1f, 2f)}, {Random.Range(0.1f, 2f)}, {Random.Range(0.1f, 2f)})" },
                    { "active", Random.value > 0.5f ? "true" : "false" }
                },
                content = $"This is GameObject {i} with some sample content for searching."
            };
            searchItems.Add(item);
        }
        
        // 创建脚本搜索项目
        for (int i = 0; i < 50; i++)
        {
            var item = new SearchItem
            {
                id = $"Script_{i}",
                name = $"Script {i}",
                type = "Script",
                tags = new string[] { "script", "code", "component" },
                properties = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "language", "C#" },
                    { "lines", Random.Range(10, 500).ToString() },
                    { "complexity", Random.Range(1, 10).ToString() }
                },
                content = $"Script {i} contains various functions and methods for game logic."
            };
            searchItems.Add(item);
        }
        
        // 创建资源搜索项目
        for (int i = 0; i < 75; i++)
        {
            var item = new SearchItem
            {
                id = $"Asset_{i}",
                name = $"Asset {i}",
                type = "Asset",
                tags = new string[] { "asset", "resource", "file" },
                properties = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "size", Random.Range(1, 1000).ToString() },
                    { "format", "asset" },
                    { "path", $"Assets/Resources/Asset_{i}.asset" }
                },
                content = $"Asset {i} is a game resource that can be loaded at runtime."
            };
            searchItems.Add(item);
        }
        
        indexedItems = searchItems.Count;
    }

    /// <summary>
    /// 构建搜索索引
    /// </summary>
    private void BuildSearchIndex()
    {
        float startTime = Time.realtimeSinceStartup;
        
        foreach (var item in searchItems)
        {
            searchIndex.AddItem(item);
        }
        
        searchIndex.Build();
        
        float buildTime = Time.realtimeSinceStartup - startTime;
        Debug.Log($"搜索索引构建完成: 耗时={buildTime * 1000:F2}ms, 项目数={indexedItems}");
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        if (enableSearchMonitoring)
        {
            searchTimeHistory = new float[100];
            resultCountHistory = new float[100];
            searchTimeIndex = 0;
            resultCountIndex = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    /// <summary>
    /// 配置搜索服务
    /// </summary>
    private void ConfigureSearchService()
    {
        searchService.EnableAsyncSearch = enableAsyncSearch;
        searchService.EnableQueryOptimization = enableQueryOptimization;
        searchService.EnableSearchProfiling = enableSearchProfiling;
        
        Debug.Log("搜索服务配置完成");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新索引
        if (enableAutoIndexing && Time.time - lastIndexUpdateTime > indexUpdateInterval)
        {
            UpdateSearchIndex();
            lastIndexUpdateTime = Time.time;
        }
        
        // 搜索监控
        if (enableSearchMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            MonitorSearchPerformance();
            lastMonitoringTime = Time.time;
        }
    }

    /// <summary>
    /// 更新搜索索引
    /// </summary>
    private void UpdateSearchIndex()
    {
        if (enableIncrementalIndexing)
        {
            // 增量更新索引
            searchIndex.Update();
        }
        else
        {
            // 完全重建索引
            searchIndex.Clear();
            BuildSearchIndex();
        }
        
        if (logSearchData)
        {
            Debug.Log("搜索索引已更新");
        }
    }

    /// <summary>
    /// 监控搜索性能
    /// </summary>
    private void MonitorSearchPerformance()
    {
        if (logSearchData)
        {
            Debug.Log($"搜索性能监控: 总搜索={totalSearches}, 平均时间={averageSearchTime * 1000:F2}ms, 索引项目={indexedItems}");
        }
    }

    /// <summary>
    /// 执行搜索
    /// </summary>
    /// <param name="query">搜索查询</param>
    public void ExecuteSearch(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            Debug.LogWarning("搜索查询不能为空");
            return;
        }
        
        searchQuery = query;
        currentSearchStatus = "搜索中...";
        
        float startTime = Time.realtimeSinceStartup;
        
        try
        {
            // 创建搜索请求
            var searchRequest = new SearchRequest
            {
                Query = query,
                MaxResults = maxSearchResults,
                Timeout = searchTimeout,
                EnableFuzzySearch = enableFuzzySearch,
                EnableCaseSensitive = enableCaseSensitive,
                FuzzyThreshold = fuzzyThreshold,
                SearchTypes = GetEnabledSearchTypes()
            };
            
            // 执行搜索
            if (enableAsyncSearch)
            {
                StartCoroutine(ExecuteAsyncSearch(searchRequest));
            }
            else
            {
                ExecuteSyncSearch(searchRequest);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"搜索执行失败: {e.Message}");
            currentSearchStatus = "搜索失败";
        }
    }

    /// <summary>
    /// 执行同步搜索
    /// </summary>
    private void ExecuteSyncSearch(SearchRequest request)
    {
        var results = searchService.Search(request);
        ProcessSearchResults(results);
    }

    /// <summary>
    /// 执行异步搜索
    /// </summary>
    private System.Collections.IEnumerator ExecuteAsyncSearch(SearchRequest request)
    {
        var searchOperation = searchService.SearchAsync(request);
        
        while (!searchOperation.IsDone)
        {
            yield return null;
        }
        
        if (searchOperation.HasError)
        {
            Debug.LogError($"异步搜索失败: {searchOperation.Error}");
            currentSearchStatus = "搜索失败";
        }
        else
        {
            ProcessSearchResults(searchOperation.Results);
        }
    }

    /// <summary>
    /// 处理搜索结果
    /// </summary>
    private void ProcessSearchResults(SearchResult[] results)
    {
        lastResults.Clear();
        lastResults.AddRange(results);
        
        lastSearchResults = results.Length;
        lastSearchTime = Time.realtimeSinceStartup;
        lastSearchQuery = searchQuery;
        totalSearches++;
        
        // 更新性能数据
        UpdatePerformanceData();
        
        currentSearchStatus = $"搜索完成 ({results.Length} 个结果)";
        
        Debug.Log($"搜索完成: 查询='{searchQuery}', 结果数={results.Length}");
    }

    /// <summary>
    /// 更新性能数据
    /// </summary>
    private void UpdatePerformanceData()
    {
        // 更新搜索时间历史
        searchTimeHistory[searchTimeIndex] = lastSearchTime;
        searchTimeIndex = (searchTimeIndex + 1) % 100;
        
        // 更新结果数量历史
        resultCountHistory[resultCountIndex] = lastSearchResults;
        resultCountIndex = (resultCountIndex + 1) % 100;
        
        // 计算平均搜索时间
        float totalTime = 0f;
        for (int i = 0; i < 100; i++)
        {
            totalTime += searchTimeHistory[i];
        }
        averageSearchTime = totalTime / 100;
    }

    /// <summary>
    /// 获取启用的搜索类型
    /// </summary>
    private SearchType GetEnabledSearchTypes()
    {
        SearchType types = SearchType.None;
        
        if (enableTextSearch) types |= SearchType.Text;
        if (enableTagSearch) types |= SearchType.Tag;
        if (enableTypeSearch) types |= SearchType.Type;
        if (enablePropertySearch) types |= SearchType.Property;
        if (enableHierarchySearch) types |= SearchType.Hierarchy;
        
        return types;
    }

    /// <summary>
    /// 添加搜索项目
    /// </summary>
    /// <param name="item">搜索项目</param>
    public void AddSearchItem(SearchItem item)
    {
        searchItems.Add(item);
        indexedItems++;
        
        if (enableIndexing)
        {
            searchIndex.AddItem(item);
        }
        
        Debug.Log($"搜索项目已添加: {item.name}");
    }

    /// <summary>
    /// 移除搜索项目
    /// </summary>
    /// <param name="itemId">项目ID</param>
    public void RemoveSearchItem(string itemId)
    {
        for (int i = searchItems.Count - 1; i >= 0; i--)
        {
            if (searchItems[i].id == itemId)
            {
                searchItems.RemoveAt(i);
                indexedItems--;
                
                if (enableIndexing)
                {
                    searchIndex.RemoveItem(itemId);
                }
                
                Debug.Log($"搜索项目已移除: {itemId}");
                break;
            }
        }
    }

    /// <summary>
    /// 重建搜索索引
    /// </summary>
    public void RebuildSearchIndex()
    {
        Debug.Log("开始重建搜索索引...");
        
        searchIndex.Clear();
        BuildSearchIndex();
        
        Debug.Log("搜索索引重建完成");
    }

    /// <summary>
    /// 优化搜索索引
    /// </summary>
    public void OptimizeSearchIndex()
    {
        Debug.Log("开始优化搜索索引...");
        
        searchIndex.Optimize();
        
        Debug.Log("搜索索引优化完成");
    }

    /// <summary>
    /// 生成搜索报告
    /// </summary>
    public void GenerateSearchReport()
    {
        Debug.Log("=== 搜索服务报告 ===");
        Debug.Log($"搜索服务状态: {searchServiceState}");
        Debug.Log($"当前搜索状态: {currentSearchStatus}");
        Debug.Log($"总搜索次数: {totalSearches}");
        Debug.Log($"平均搜索时间: {averageSearchTime * 1000:F2}ms");
        Debug.Log($"已索引项目数: {indexedItems}");
        Debug.Log($"上次搜索查询: {lastSearchQuery}");
        Debug.Log($"上次搜索结果数: {lastSearchResults}");
        Debug.Log($"上次搜索时间: {lastSearchTime * 1000:F2}ms");
        Debug.Log($"索引大小: {searchIndex.Size}");
        Debug.Log($"索引内存使用: {searchIndex.MemoryUsage / 1024:F1}KB");
    }

    /// <summary>
    /// 导出搜索数据
    /// </summary>
    public void ExportSearchData()
    {
        var data = new SearchServiceData
        {
            timestamp = System.DateTime.Now.ToString(),
            searchServiceState = searchServiceState,
            currentSearchStatus = currentSearchStatus,
            totalSearches = totalSearches,
            averageSearchTime = averageSearchTime,
            indexedItems = indexedItems,
            lastSearchQuery = lastSearchQuery,
            lastSearchResults = lastSearchResults,
            lastSearchTime = lastSearchTime,
            searchTimeHistory = searchTimeHistory,
            resultCountHistory = resultCountHistory
        };
        
        string json = JsonUtility.ToJson(data, true);
        string filename = $"searchservice_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        
        System.IO.File.WriteAllText(filename, json);
        Debug.Log($"搜索数据已导出: {filename}");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("SearchService 搜索服务演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("搜索服务配置:");
        enableSearchService = GUILayout.Toggle(enableSearchService, "启用搜索服务");
        enableIndexing = GUILayout.Toggle(enableIndexing, "启用索引");
        enableQueryOptimization = GUILayout.Toggle(enableQueryOptimization, "启用查询优化");
        enableSearchProfiling = GUILayout.Toggle(enableSearchProfiling, "启用搜索分析");
        enableAsyncSearch = GUILayout.Toggle(enableAsyncSearch, "启用异步搜索");
        
        GUILayout.Space(10);
        GUILayout.Label("索引配置:");
        maxIndexSize = int.TryParse(GUILayout.TextField("最大索引大小", maxIndexSize.ToString()), out var indexSize) ? indexSize : maxIndexSize;
        indexUpdateInterval = float.TryParse(GUILayout.TextField("索引更新间隔", indexUpdateInterval.ToString()), out var updateInterval) ? updateInterval : indexUpdateInterval;
        enableAutoIndexing = GUILayout.Toggle(enableAutoIndexing, "启用自动索引");
        enableIncrementalIndexing = GUILayout.Toggle(enableIncrementalIndexing, "启用增量索引");
        
        GUILayout.Space(10);
        GUILayout.Label("搜索参数:");
        searchQuery = GUILayout.TextField("搜索查询", searchQuery);
        maxSearchResults = int.TryParse(GUILayout.TextField("最大搜索结果数", maxSearchResults.ToString()), out var maxResults) ? maxResults : maxSearchResults;
        searchTimeout = float.TryParse(GUILayout.TextField("搜索超时时间", searchTimeout.ToString()), out var timeout) ? timeout : searchTimeout;
        enableFuzzySearch = GUILayout.Toggle(enableFuzzySearch, "启用模糊搜索");
        enableCaseSensitive = GUILayout.Toggle(enableCaseSensitive, "启用大小写敏感");
        fuzzyThreshold = float.TryParse(GUILayout.TextField("模糊阈值", fuzzyThreshold.ToString()), out var threshold) ? threshold : fuzzyThreshold;
        
        GUILayout.Space(10);
        GUILayout.Label("搜索类型:");
        enableTextSearch = GUILayout.Toggle(enableTextSearch, "启用文本搜索");
        enableTagSearch = GUILayout.Toggle(enableTagSearch, "启用标签搜索");
        enableTypeSearch = GUILayout.Toggle(enableTypeSearch, "启用类型搜索");
        enablePropertySearch = GUILayout.Toggle(enablePropertySearch, "启用属性搜索");
        enableHierarchySearch = GUILayout.Toggle(enableHierarchySearch, "启用层级搜索");
        
        GUILayout.Space(10);
        GUILayout.Label("搜索状态:");
        GUILayout.Label($"服务状态: {searchServiceState}");
        GUILayout.Label($"搜索状态: {currentSearchStatus}");
        GUILayout.Label($"总搜索次数: {totalSearches}");
        GUILayout.Label($"平均搜索时间: {averageSearchTime * 1000:F2}ms");
        GUILayout.Label($"已索引项目数: {indexedItems}");
        GUILayout.Label($"上次搜索结果: {lastSearchResults}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("执行搜索"))
        {
            ExecuteSearch(searchQuery);
        }
        
        if (GUILayout.Button("重建搜索索引"))
        {
            RebuildSearchIndex();
        }
        
        if (GUILayout.Button("优化搜索索引"))
        {
            OptimizeSearchIndex();
        }
        
        if (GUILayout.Button("生成搜索报告"))
        {
            GenerateSearchReport();
        }
        
        if (GUILayout.Button("导出搜索数据"))
        {
            ExportSearchData();
        }
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 搜索项目
/// </summary>
[System.Serializable]
public class SearchItem
{
    public string id;
    public string name;
    public string type;
    public string[] tags;
    public System.Collections.Generic.Dictionary<string, string> properties;
    public string content;
}

/// <summary>
/// 搜索结果
/// </summary>
[System.Serializable]
public class SearchResult
{
    public string id;
    public string name;
    public string type;
    public float relevance;
    public string[] matchedFields;
}

/// <summary>
/// 搜索请求
/// </summary>
[System.Serializable]
public class SearchRequest
{
    public string Query;
    public int MaxResults;
    public float Timeout;
    public bool EnableFuzzySearch;
    public bool EnableCaseSensitive;
    public float FuzzyThreshold;
    public SearchType SearchTypes;
}

/// <summary>
/// 搜索类型
/// </summary>
[System.Flags]
public enum SearchType
{
    None = 0,
    Text = 1 << 0,
    Tag = 1 << 1,
    Type = 1 << 2,
    Property = 1 << 3,
    Hierarchy = 1 << 4
}

/// <summary>
/// 搜索服务
/// </summary>
public class SearchService
{
    public bool EnableAsyncSearch { get; set; }
    public bool EnableQueryOptimization { get; set; }
    public bool EnableSearchProfiling { get; set; }
    
    public SearchResult[] Search(SearchRequest request)
    {
        // 模拟搜索实现
        return new SearchResult[0];
    }
    
    public SearchOperation SearchAsync(SearchRequest request)
    {
        // 模拟异步搜索实现
        return new SearchOperation();
    }
}

/// <summary>
/// 搜索操作
/// </summary>
public class SearchOperation
{
    public bool IsDone { get; set; }
    public bool HasError { get; set; }
    public string Error { get; set; }
    public SearchResult[] Results { get; set; }
}

/// <summary>
/// 搜索索引
/// </summary>
public class SearchIndex
{
    public int MaxSize { get; set; }
    public bool EnableIncrementalIndexing { get; set; }
    public int Size { get; set; }
    public long MemoryUsage { get; set; }
    
    public void AddItem(SearchItem item) { }
    public void RemoveItem(string itemId) { }
    public void Build() { }
    public void Update() { }
    public void Clear() { }
    public void Optimize() { }
}

/// <summary>
/// 搜索服务数据类
/// </summary>
[System.Serializable]
public class SearchServiceData
{
    public string timestamp;
    public string searchServiceState;
    public string currentSearchStatus;
    public int totalSearches;
    public float averageSearchTime;
    public int indexedItems;
    public string lastSearchQuery;
    public int lastSearchResults;
    public float lastSearchTime;
    public float[] searchTimeHistory;
    public float[] resultCountHistory;
}