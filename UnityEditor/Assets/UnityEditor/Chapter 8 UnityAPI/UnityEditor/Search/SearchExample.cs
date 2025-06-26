using UnityEngine;
using UnityEditor;
using UnityEditor.Search;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.Search 命名空间案例演示
/// 展示搜索系统的使用，包括资产搜索、场景搜索和自定义搜索
/// </summary>
public class SearchExample : MonoBehaviour
{
    [Header("搜索系统配置")]
    [SerializeField] private bool enableSearchSystem = true;
    [SerializeField] private bool enableSearchLogging = true;
    [SerializeField] private bool enableSearchValidation = true;
    [SerializeField] private bool enableSearchPerformance = true;
    [SerializeField] private bool enableSearchHistory = true;
    
    [Header("搜索配置")]
    [SerializeField] private string searchQuery = "";
    [SerializeField] private SearchProvider currentProvider = SearchProvider.Asset;
    [SerializeField] private SearchFlags searchFlags = SearchFlags.Default;
    [SerializeField] private int maxResults = 100;
    [SerializeField] private bool enableFuzzySearch = true;
    [SerializeField] private bool enableRegexSearch = false;
    [SerializeField] private bool enableCaseSensitive = false;
    
    [Header("搜索状态")]
    [SerializeField] private SearchStatus searchStatus = SearchStatus.Idle;
    [SerializeField] private bool isSearching = false;
    [SerializeField] private float searchProgress = 0f;
    [SerializeField] private string searchMessage = "";
    [SerializeField] private int totalResults = 0;
    [SerializeField] private int filteredResults = 0;
    [SerializeField] private float searchTime = 0f;
    
    [Header("搜索结果")]
    [SerializeField] private SearchItem[] searchResults = new SearchItem[0];
    [SerializeField] private SearchItem[] filteredSearchResults = new SearchItem[0];
    [SerializeField] private string[] resultPaths = new string[0];
    [SerializeField] private string[] resultTypes = new string[0];
    [SerializeField] private string[] resultLabels = new string[0];
    
    [Header("搜索历史")]
    [SerializeField] private SearchHistoryEntry[] searchHistory = new SearchHistoryEntry[10];
    [SerializeField] private int searchHistoryIndex = 0;
    [SerializeField] private bool enableSearchHistory = true;
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceMonitoring = true;
    [SerializeField] private float[] searchTimeHistory = new float[100];
    [SerializeField] private int searchTimeIndex = 0;
    [SerializeField] private float averageSearchTime = 0f;
    [SerializeField] private float maxSearchTime = 0f;
    [SerializeField] private float totalSearchTime = 0f;
    [SerializeField] private int totalSearchCount = 0;
    
    [Header("搜索统计")]
    [SerializeField] private Dictionary<string, int> searchTypeCount = new Dictionary<string, int>();
    [SerializeField] private Dictionary<string, int> searchProviderCount = new Dictionary<string, int>();
    [SerializeField] private int totalAssetSearches = 0;
    [SerializeField] private int totalSceneSearches = 0;
    [SerializeField] private int totalCustomSearches = 0;
    [SerializeField] private int totalGlobalSearches = 0;
    
    [Header("搜索过滤器")]
    [SerializeField] private string[] searchFilters = new string[0];
    [SerializeField] private string[] excludeFilters = new string[0];
    [SerializeField] private string[] typeFilters = new string[0];
    [SerializeField] private string[] pathFilters = new string[0];
    [SerializeField] private bool enableAdvancedFiltering = false;
    
    private bool isInitialized = false;
    private float searchStartTime = 0f;
    private SearchContext searchContext;
    private List<SearchItem> pendingResults = new List<SearchItem>();

    private void Start()
    {
        InitializeSearchSystem();
    }

    private void InitializeSearchSystem()
    {
        if (!enableSearchSystem) return;
        
        InitializeSearchState();
        InitializePerformanceMonitoring();
        InitializeSearchStatistics();
        RegisterSearchCallbacks();
        
        isInitialized = true;
        searchStatus = SearchStatus.Idle;
        Debug.Log("搜索系统初始化完成");
    }

    private void InitializeSearchState()
    {
        searchStatus = SearchStatus.Idle;
        isSearching = false;
        searchProgress = 0f;
        searchMessage = "就绪";
        totalResults = 0;
        filteredResults = 0;
        searchTime = 0f;
        
        Debug.Log("搜索状态已初始化");
    }

    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            searchTimeHistory = new float[100];
            searchTimeIndex = 0;
            averageSearchTime = 0f;
            maxSearchTime = 0f;
            totalSearchTime = 0f;
            totalSearchCount = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    private void InitializeSearchStatistics()
    {
        searchTypeCount.Clear();
        searchProviderCount.Clear();
        totalAssetSearches = 0;
        totalSceneSearches = 0;
        totalCustomSearches = 0;
        totalGlobalSearches = 0;
        
        Debug.Log("搜索统计初始化完成");
    }

    private void RegisterSearchCallbacks()
    {
        // 注册搜索回调
        SearchService.searchItemSelected += OnSearchItemSelected;
        SearchService.searchItemExecuted += OnSearchItemExecuted;
        
        Debug.Log("搜索回调已注册");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        UpdateSearchStatus();
        UpdateSearchProgress();
        
        if (enablePerformanceMonitoring)
        {
            UpdatePerformanceMonitoring();
        }
    }

    private void UpdateSearchStatus()
    {
        if (isSearching)
        {
            searchStatus = SearchStatus.Searching;
        }
        else
        {
            searchStatus = SearchStatus.Idle;
        }
    }

    private void UpdateSearchProgress()
    {
        if (isSearching)
        {
            // 模拟搜索进度
            searchProgress = Mathf.Clamp01(searchProgress + Time.deltaTime * 0.2f);
        }
        else
        {
            searchProgress = 0f;
        }
    }

    private void UpdatePerformanceMonitoring()
    {
        if (totalSearchCount > 0)
        {
            averageSearchTime = totalSearchTime / totalSearchCount;
        }
    }

    private void OnSearchItemSelected(SearchItem item)
    {
        if (enableSearchLogging)
        {
            Debug.Log($"搜索项被选中: {item.label} ({item.id})");
        }
    }

    private void OnSearchItemExecuted(SearchItem item)
    {
        if (enableSearchLogging)
        {
            Debug.Log($"搜索项被执行: {item.label} ({item.id})");
        }
    }

    public void StartSearch()
    {
        if (isSearching)
        {
            Debug.LogWarning("搜索正在进行中，请等待完成");
            return;
        }
        
        if (string.IsNullOrEmpty(searchQuery))
        {
            Debug.LogWarning("搜索查询不能为空");
            return;
        }
        
        if (!enableSearchValidation || ValidateSearch())
        {
            isSearching = true;
            searchStartTime = Time.realtimeSinceStartup;
            searchProgress = 0f;
            searchMessage = "搜索开始...";
            
            // 执行搜索
            ExecuteSearch();
        }
    }

    private bool ValidateSearch()
    {
        bool isValid = true;
        
        if (string.IsNullOrEmpty(searchQuery))
        {
            Debug.LogError("搜索查询不能为空");
            isValid = false;
        }
        
        if (maxResults <= 0)
        {
            Debug.LogError("最大结果数必须大于0");
            isValid = false;
        }
        
        return isValid;
    }

    private void ExecuteSearch()
    {
        try
        {
            // 创建搜索上下文
            searchContext = SearchService.CreateContext(searchQuery, searchFlags);
            
            // 设置搜索提供者
            SetSearchProvider();
            
            // 设置搜索过滤器
            SetSearchFilters();
            
            // 执行搜索
            SearchService.Request(searchContext, OnSearchCompleted, maxResults);
            
            Debug.Log($"开始搜索: {searchQuery}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"搜索失败: {e.Message}");
            OnSearchError(e);
        }
    }

    private void SetSearchProvider()
    {
        switch (currentProvider)
        {
            case SearchProvider.Asset:
                searchContext.providers = new[] { "asset" };
                totalAssetSearches++;
                break;
            case SearchProvider.Scene:
                searchContext.providers = new[] { "scene" };
                totalSceneSearches++;
                break;
            case SearchProvider.Global:
                searchContext.providers = new[] { "global" };
                totalGlobalSearches++;
                break;
            case SearchProvider.Custom:
                searchContext.providers = new[] { "custom" };
                totalCustomSearches++;
                break;
        }
        
        // 更新统计
        string providerName = currentProvider.ToString();
        if (!searchProviderCount.ContainsKey(providerName))
        {
            searchProviderCount[providerName] = 0;
        }
        searchProviderCount[providerName]++;
    }

    private void SetSearchFilters()
    {
        if (!enableAdvancedFiltering) return;
        
        // 设置类型过滤器
        if (typeFilters.Length > 0)
        {
            searchContext.filterId = string.Join(",", typeFilters);
        }
        
        // 设置路径过滤器
        if (pathFilters.Length > 0)
        {
            // 这里可以设置路径过滤器
        }
    }

    private void OnSearchCompleted(SearchContext context, IList<SearchItem> items)
    {
        searchTime = Time.realtimeSinceStartup - searchStartTime;
        isSearching = false;
        searchProgress = 1f;
        searchMessage = "搜索完成";
        
        // 处理搜索结果
        ProcessSearchResults(items);
        
        // 更新性能数据
        UpdateSearchPerformance();
        
        // 记录到历史
        if (enableSearchHistory)
        {
            AddSearchHistoryEntry();
        }
        
        if (enableSearchLogging)
        {
            Debug.Log($"搜索完成: {totalResults} 个结果, 耗时: {searchTime:F3}秒");
        }
    }

    private void OnSearchError(System.Exception exception)
    {
        searchTime = Time.realtimeSinceStartup - searchStartTime;
        isSearching = false;
        searchProgress = 0f;
        searchMessage = $"搜索失败: {exception.Message}";
        
        Debug.LogError($"搜索过程中发生错误: {exception}");
    }

    private void ProcessSearchResults(IList<SearchItem> items)
    {
        searchResults = new SearchItem[items.Count];
        items.CopyTo(searchResults, 0);
        totalResults = items.Count;
        
        // 过滤结果
        FilterSearchResults();
        
        // 提取结果信息
        ExtractResultInfo();
        
        // 更新统计
        UpdateSearchStatistics();
    }

    private void FilterSearchResults()
    {
        if (searchFilters.Length == 0 && excludeFilters.Length == 0)
        {
            filteredSearchResults = searchResults;
            filteredResults = totalResults;
            return;
        }
        
        List<SearchItem> filtered = new List<SearchItem>();
        
        foreach (var item in searchResults)
        {
            bool shouldInclude = true;
            
            // 应用包含过滤器
            if (searchFilters.Length > 0)
            {
                shouldInclude = false;
                foreach (string filter in searchFilters)
                {
                    if (item.label.ToLower().Contains(filter.ToLower()) ||
                        item.id.ToLower().Contains(filter.ToLower()))
                    {
                        shouldInclude = true;
                        break;
                    }
                }
            }
            
            // 应用排除过滤器
            if (shouldInclude && excludeFilters.Length > 0)
            {
                foreach (string filter in excludeFilters)
                {
                    if (item.label.ToLower().Contains(filter.ToLower()) ||
                        item.id.ToLower().Contains(filter.ToLower()))
                    {
                        shouldInclude = false;
                        break;
                    }
                }
            }
            
            if (shouldInclude)
            {
                filtered.Add(item);
            }
        }
        
        filteredSearchResults = filtered.ToArray();
        filteredResults = filtered.Count;
    }

    private void ExtractResultInfo()
    {
        resultPaths = new string[filteredSearchResults.Length];
        resultTypes = new string[filteredSearchResults.Length];
        resultLabels = new string[filteredSearchResults.Length];
        
        for (int i = 0; i < filteredSearchResults.Length; i++)
        {
            var item = filteredSearchResults[i];
            resultPaths[i] = item.id;
            resultTypes[i] = item.type?.ToString() ?? "Unknown";
            resultLabels[i] = item.label;
        }
    }

    private void UpdateSearchStatistics()
    {
        foreach (var item in filteredSearchResults)
        {
            string type = item.type?.ToString() ?? "Unknown";
            if (!searchTypeCount.ContainsKey(type))
            {
                searchTypeCount[type] = 0;
            }
            searchTypeCount[type]++;
        }
    }

    private void UpdateSearchPerformance()
    {
        if (enablePerformanceMonitoring)
        {
            searchTimeHistory[searchTimeIndex] = searchTime;
            searchTimeIndex = (searchTimeIndex + 1) % 100;
            
            totalSearchTime += searchTime;
            totalSearchCount++;
            
            if (searchTime > maxSearchTime)
            {
                maxSearchTime = searchTime;
            }
        }
    }

    private void AddSearchHistoryEntry()
    {
        var entry = new SearchHistoryEntry
        {
            timestamp = System.DateTime.Now.ToString(),
            query = searchQuery,
            provider = currentProvider.ToString(),
            totalResults = totalResults,
            filteredResults = filteredResults,
            searchTime = searchTime,
            success = !string.IsNullOrEmpty(searchMessage) && !searchMessage.Contains("失败")
        };
        
        searchHistory[searchHistoryIndex] = entry;
        searchHistoryIndex = (searchHistoryIndex + 1) % searchHistory.Length;
    }

    public void ClearSearchResults()
    {
        searchResults = new SearchItem[0];
        filteredSearchResults = new SearchItem[0];
        resultPaths = new string[0];
        resultTypes = new string[0];
        resultLabels = new string[0];
        totalResults = 0;
        filteredResults = 0;
        
        Debug.Log("搜索结果已清除");
    }

    public void SetSearchQuery(string query)
    {
        searchQuery = query;
        Debug.Log($"搜索查询已设置: {query}");
    }

    public void SetSearchProvider(SearchProvider provider)
    {
        currentProvider = provider;
        Debug.Log($"搜索提供者已设置: {provider}");
    }

    public void AddSearchFilter(string filter)
    {
        List<string> filters = new List<string>(searchFilters);
        if (!filters.Contains(filter))
        {
            filters.Add(filter);
            searchFilters = filters.ToArray();
            Debug.Log($"搜索过滤器已添加: {filter}");
        }
    }

    public void RemoveSearchFilter(string filter)
    {
        List<string> filters = new List<string>(searchFilters);
        if (filters.Remove(filter))
        {
            searchFilters = filters.ToArray();
            Debug.Log($"搜索过滤器已移除: {filter}");
        }
    }

    public void AddExcludeFilter(string filter)
    {
        List<string> filters = new List<string>(excludeFilters);
        if (!filters.Contains(filter))
        {
            filters.Add(filter);
            excludeFilters = filters.ToArray();
            Debug.Log($"排除过滤器已添加: {filter}");
        }
    }

    public void RemoveExcludeFilter(string filter)
    {
        List<string> filters = new List<string>(excludeFilters);
        if (filters.Remove(filter))
        {
            excludeFilters = filters.ToArray();
            Debug.Log($"排除过滤器已移除: {filter}");
        }
    }

    public void GenerateSearchReport()
    {
        Debug.Log("=== 搜索系统报告 ===");
        Debug.Log($"搜索系统状态: {searchStatus}");
        Debug.Log($"当前搜索提供者: {currentProvider}");
        Debug.Log($"搜索查询: {searchQuery}");
        Debug.Log($"搜索标志: {searchFlags}");
        Debug.Log($"最大结果数: {maxResults}");
        Debug.Log($"总结果数: {totalResults}");
        Debug.Log($"过滤后结果数: {filteredResults}");
        Debug.Log($"搜索时间: {searchTime:F3}秒");
        Debug.Log($"总搜索次数: {totalSearchCount}");
        Debug.Log($"平均搜索时间: {averageSearchTime:F3}秒");
        Debug.Log($"最大搜索时间: {maxSearchTime:F3}秒");
        Debug.Log($"总搜索时间: {totalSearchTime:F3}秒");
        
        Debug.Log("=== 搜索类型统计 ===");
        foreach (var kvp in searchTypeCount)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value} 个");
        }
        
        Debug.Log("=== 搜索提供者统计 ===");
        foreach (var kvp in searchProviderCount)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value} 次");
        }
        
        Debug.Log("=== 搜索结果 ===");
        for (int i = 0; i < Mathf.Min(filteredSearchResults.Length, 10); i++)
        {
            var item = filteredSearchResults[i];
            Debug.Log($"{i + 1}. {item.label} ({item.type}) - {item.id}");
        }
    }

    public void ClearSearchHistory()
    {
        searchHistory = new SearchHistoryEntry[10];
        searchHistoryIndex = 0;
        Debug.Log("搜索历史已清除");
    }

    public void ResetSearchStatistics()
    {
        searchTypeCount.Clear();
        searchProviderCount.Clear();
        totalSearchCount = 0;
        totalSearchTime = 0f;
        averageSearchTime = 0f;
        maxSearchTime = 0f;
        
        Debug.Log("搜索统计已重置");
    }

    private void OnDestroy()
    {
        SearchService.searchItemSelected -= OnSearchItemSelected;
        SearchService.searchItemExecuted -= OnSearchItemExecuted;
        
        Debug.Log("搜索回调已清理");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Search 搜索系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("搜索系统配置:");
        enableSearchSystem = GUILayout.Toggle(enableSearchSystem, "启用搜索系统");
        enableSearchLogging = GUILayout.Toggle(enableSearchLogging, "启用搜索日志");
        enableSearchValidation = GUILayout.Toggle(enableSearchValidation, "启用搜索验证");
        enableSearchPerformance = GUILayout.Toggle(enableSearchPerformance, "启用搜索性能监控");
        enableSearchHistory = GUILayout.Toggle(enableSearchHistory, "启用搜索历史记录");
        
        GUILayout.Space(10);
        GUILayout.Label("搜索配置:");
        searchQuery = GUILayout.TextField("搜索查询", searchQuery);
        currentProvider = (SearchProvider)System.Enum.Parse(typeof(SearchProvider), GUILayout.TextField("搜索提供者", currentProvider.ToString()));
        maxResults = int.TryParse(GUILayout.TextField("最大结果数", maxResults.ToString()), out var max) ? max : maxResults;
        enableFuzzySearch = GUILayout.Toggle(enableFuzzySearch, "启用模糊搜索");
        enableRegexSearch = GUILayout.Toggle(enableRegexSearch, "启用正则表达式搜索");
        enableCaseSensitive = GUILayout.Toggle(enableCaseSensitive, "启用大小写敏感");
        
        GUILayout.Space(10);
        GUILayout.Label("搜索状态:");
        GUILayout.Label($"搜索状态: {searchStatus}");
        GUILayout.Label($"是否正在搜索: {isSearching}");
        GUILayout.Label($"搜索进度: {searchProgress * 100:F1}%");
        GUILayout.Label($"搜索消息: {searchMessage}");
        GUILayout.Label($"总结果数: {totalResults}");
        GUILayout.Label($"过滤后结果数: {filteredResults}");
        GUILayout.Label($"搜索时间: {searchTime:F3}秒");
        GUILayout.Label($"总搜索次数: {totalSearchCount}");
        GUILayout.Label($"平均搜索时间: {averageSearchTime:F3}秒");
        GUILayout.Label($"最大搜索时间: {maxSearchTime:F3}秒");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("开始搜索"))
        {
            StartSearch();
        }
        
        if (GUILayout.Button("清除搜索结果"))
        {
            ClearSearchResults();
        }
        
        if (GUILayout.Button("生成搜索报告"))
        {
            GenerateSearchReport();
        }
        
        if (GUILayout.Button("清除搜索历史"))
        {
            ClearSearchHistory();
        }
        
        if (GUILayout.Button("重置搜索统计"))
        {
            ResetSearchStatistics();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("搜索结果:");
        for (int i = 0; i < Mathf.Min(filteredSearchResults.Length, 5); i++)
        {
            var item = filteredSearchResults[i];
            GUILayout.Label($"{i + 1}. {item.label}");
        }
        
        GUILayout.Space(10);
        GUILayout.Label("搜索历史:");
        for (int i = 0; i < searchHistory.Length; i++)
        {
            if (searchHistory[i] != null && !string.IsNullOrEmpty(searchHistory[i].timestamp))
            {
                var entry = searchHistory[i];
                string status = entry.success ? "成功" : "失败";
                GUILayout.Label($"{entry.timestamp} - {entry.query} - {status} - {entry.searchTime:F3}s");
            }
        }
        
        GUILayout.EndArea();
    }
}

public enum SearchProvider
{
    Asset,
    Scene,
    Global,
    Custom
}

public enum SearchStatus
{
    Idle,
    Searching,
    Completed,
    Failed
}

[System.Serializable]
public class SearchHistoryEntry
{
    public string timestamp;
    public string query;
    public string provider;
    public int totalResults;
    public int filteredResults;
    public float searchTime;
    public bool success;
} 