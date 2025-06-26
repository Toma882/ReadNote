using UnityEngine;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEditor.Search;
using System.Collections.Generic;
using System.Linq;

namespace UnityEditor.SearchService.Examples
{
    /// <summary>
    /// UnityEditor.SearchService 命名空间使用示例
    /// 演示搜索服务编辑器系统的配置、查询和管理功能
    /// </summary>
    public class SearchServiceExample : MonoBehaviour
    {
        [Header("搜索服务配置")]
        [SerializeField] private bool enableSearchService = true;
        [SerializeField] private string searchQuery = "";
        [SerializeField] private string searchProvider = "asset";
        [SerializeField] private int maxResults = 100;
        
        [Header("搜索状态")]
        [SerializeField] private int resultCount = 0;
        [SerializeField] private string lastSearchQuery = "";
        [SerializeField] private bool isSearching = false;
        
        [Header("搜索结果")]
        [SerializeField] private List<SearchResult> searchResults = new List<SearchResult>();
        [SerializeField] private SearchResult selectedResult;
        
        [Header("搜索配置")]
        [SerializeField] private SearchContext searchContext;
        [SerializeField] private List<string> availableProviders = new List<string>();
        
        private Dictionary<string, ISearchProvider> providerRegistry = new Dictionary<string, ISearchProvider>();
        
        /// <summary>
        /// 搜索结果
        /// </summary>
        [System.Serializable]
        public class SearchResult
        {
            public string id;
            public string name;
            public string path;
            public string type;
            public float score;
            public Object asset;
            
            public SearchResult(string id, string name, string path, string type, float score)
            {
                this.id = id;
                this.name = name;
                this.path = path;
                this.type = type;
                this.score = score;
            }
        }
        
        /// <summary>
        /// 初始化搜索服务系统
        /// </summary>
        private void Start()
        {
            InitializeSearchServiceSystem();
        }
        
        /// <summary>
        /// 初始化搜索服务系统
        /// </summary>
        private void InitializeSearchServiceSystem()
        {
            if (!enableSearchService)
            {
                Debug.Log("搜索服务系统已禁用");
                return;
            }
            
            try
            {
                // 初始化搜索上下文
                InitializeSearchContext();
                
                // 加载可用搜索提供者
                LoadAvailableProviders();
                
                // 注册自定义搜索提供者
                RegisterCustomProviders();
                
                Debug.Log("搜索服务系统初始化完成");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"搜索服务系统初始化失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 初始化搜索上下文
        /// </summary>
        private void InitializeSearchContext()
        {
            searchContext = SearchService.CreateContext("asset");
            searchContext.SetFilter("t:Object");
            searchContext.maxResults = maxResults;
            
            Debug.Log("搜索上下文已初始化");
        }
        
        /// <summary>
        /// 加载可用搜索提供者
        /// </summary>
        private void LoadAvailableProviders()
        {
            availableProviders.Clear();
            providerRegistry.Clear();
            
            // 获取所有可用的搜索提供者
            var providers = SearchService.GetProviders("asset");
            foreach (var provider in providers)
            {
                availableProviders.Add(provider.name);
                providerRegistry[provider.name] = provider;
            }
            
            Debug.Log($"加载了 {availableProviders.Count} 个搜索提供者");
        }
        
        /// <summary>
        /// 注册自定义搜索提供者
        /// </summary>
        private void RegisterCustomProviders()
        {
            // 注册自定义搜索提供者
            RegisterCustomProvider("CustomProvider", "自定义提供者");
            RegisterCustomProvider("GameObjectProvider", "游戏对象提供者");
        }
        
        /// <summary>
        /// 注册自定义搜索提供者
        /// </summary>
        public void RegisterCustomProvider(string providerName, string displayName)
        {
            try
            {
                // 这里可以注册自定义的搜索提供者
                // 由于SearchService的API限制，这里只是示例
                availableProviders.Add(providerName);
                
                Debug.Log($"自定义搜索提供者已注册: {providerName}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"注册自定义搜索提供者失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 执行搜索
        /// </summary>
        public void PerformSearch(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                Debug.LogWarning("搜索查询为空");
                return;
            }
            
            if (searchContext == null)
            {
                Debug.LogError("搜索上下文未初始化");
                return;
            }
            
            try
            {
                isSearching = true;
                lastSearchQuery = query;
                
                // 设置搜索查询
                searchContext.searchText = query;
                
                // 执行搜索
                var results = SearchService.Request(searchContext);
                
                // 处理搜索结果
                ProcessSearchResults(results);
                
                Debug.Log($"搜索完成: {query}, 找到 {resultCount} 个结果");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"搜索失败: {e.Message}");
            }
            finally
            {
                isSearching = false;
            }
        }
        
        /// <summary>
        /// 处理搜索结果
        /// </summary>
        private void ProcessSearchResults(SearchResult[] results)
        {
            searchResults.Clear();
            
            foreach (var result in results)
            {
                var searchResult = new SearchResult(
                    result.id,
                    result.name,
                    result.path,
                    result.type,
                    result.score
                );
                
                // 尝试加载资产
                if (!string.IsNullOrEmpty(result.path))
                {
                    searchResult.asset = AssetDatabase.LoadAssetAtPath<Object>(result.path);
                }
                
                searchResults.Add(searchResult);
            }
            
            resultCount = searchResults.Count;
        }
        
        /// <summary>
        /// 按类型搜索
        /// </summary>
        public void SearchByType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                Debug.LogWarning("类型为空");
                return;
            }
            
            string query = $"t:{type}";
            PerformSearch(query);
        }
        
        /// <summary>
        /// 按名称搜索
        /// </summary>
        public void SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogWarning("名称为空");
                return;
            }
            
            string query = $"n:{name}";
            PerformSearch(query);
        }
        
        /// <summary>
        /// 按路径搜索
        /// </summary>
        public void SearchByPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("路径为空");
                return;
            }
            
            string query = $"p:{path}";
            PerformSearch(query);
        }
        
        /// <summary>
        /// 高级搜索
        /// </summary>
        public void AdvancedSearch(string query, string[] filters)
        {
            if (string.IsNullOrEmpty(query))
            {
                Debug.LogWarning("查询为空");
                return;
            }
            
            try
            {
                // 设置搜索上下文
                searchContext.searchText = query;
                
                // 应用过滤器
                if (filters != null && filters.Length > 0)
                {
                    foreach (string filter in filters)
                    {
                        searchContext.SetFilter(filter);
                    }
                }
                
                // 执行搜索
                var results = SearchService.Request(searchContext);
                ProcessSearchResults(results);
                
                Debug.Log($"高级搜索完成: {query}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"高级搜索失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 获取搜索结果信息
        /// </summary>
        public string GetSearchResultInfo(SearchResult result)
        {
            if (result == null)
                return "结果为空";
            
            return $"ID: {result.id}, 名称: {result.name}, 路径: {result.path}, 类型: {result.type}, 分数: {result.score:F2}";
        }
        
        /// <summary>
        /// 选择搜索结果
        /// </summary>
        public void SelectSearchResult(SearchResult result)
        {
            if (result == null)
            {
                Debug.LogWarning("结果为空");
                return;
            }
            
            selectedResult = result;
            
            // 在Project窗口中选中资产
            if (result.asset != null)
            {
                Selection.activeObject = result.asset;
                EditorGUIUtility.PingObject(result.asset);
            }
            
            Debug.Log($"已选择搜索结果: {result.name}");
        }
        
        /// <summary>
        /// 打开搜索结果
        /// </summary>
        public void OpenSearchResult(SearchResult result)
        {
            if (result == null)
            {
                Debug.LogWarning("结果为空");
                return;
            }
            
            if (result.asset != null)
            {
                AssetDatabase.OpenAsset(result.asset);
                Debug.Log($"已打开搜索结果: {result.name}");
            }
            else
            {
                Debug.LogWarning($"无法打开结果: {result.name}");
            }
        }
        
        /// <summary>
        /// 导出搜索结果
        /// </summary>
        public void ExportSearchResults()
        {
            if (searchResults.Count == 0)
            {
                Debug.LogWarning("没有搜索结果可导出");
                return;
            }
            
            string exportPath = EditorUtility.SaveFilePanel("导出搜索结果", "", "search_results", "json");
            if (string.IsNullOrEmpty(exportPath))
                return;
            
            try
            {
                var exportData = new List<object>();
                foreach (var result in searchResults)
                {
                    exportData.Add(new
                    {
                        id = result.id,
                        name = result.name,
                        path = result.path,
                        type = result.type,
                        score = result.score
                    });
                }
                
                string json = JsonUtility.ToJson(new { results = exportData }, true);
                System.IO.File.WriteAllText(exportPath, json);
                
                Debug.Log($"搜索结果已导出到: {exportPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导出搜索结果失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 清除搜索结果
        /// </summary>
        public void ClearSearchResults()
        {
            searchResults.Clear();
            resultCount = 0;
            selectedResult = null;
            
            Debug.Log("搜索结果已清除");
        }
        
        /// <summary>
        /// 获取搜索统计信息
        /// </summary>
        public string GetSearchStats()
        {
            return $"查询: {lastSearchQuery}, 结果数量: {resultCount}, 搜索中: {(isSearching ? "是" : "否")}";
        }
        
        /// <summary>
        /// 验证搜索查询
        /// </summary>
        public bool ValidateSearchQuery(string query)
        {
            return !string.IsNullOrEmpty(query) && query.Length >= 2;
        }
        
        /// <summary>
        /// 获取所有搜索提供者名称
        /// </summary>
        public string[] GetAllProviderNames()
        {
            return availableProviders.ToArray();
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 500, 700));
            GUILayout.Label("UnityEditor.SearchService 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {(enableSearchService ? "启用" : "禁用")}");
            GUILayout.Label($"提供者数量: {availableProviders.Count}");
            GUILayout.Label($"结果数量: {resultCount}");
            GUILayout.Label($"搜索中: {(isSearching ? "是" : "否")}");
            
            GUILayout.Space(10);
            GUILayout.Label("搜索配置", EditorStyles.boldLabel);
            
            searchQuery = GUILayout.TextField("搜索查询", searchQuery);
            searchProvider = EditorGUILayout.Popup("搜索提供者", 0, GetAllProviderNames()).ToString();
            maxResults = EditorGUILayout.IntField("最大结果数", maxResults);
            
            if (GUILayout.Button("执行搜索"))
            {
                if (ValidateSearchQuery(searchQuery))
                {
                    PerformSearch(searchQuery);
                }
                else
                {
                    Debug.LogWarning("搜索查询无效");
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("快速搜索", EditorStyles.boldLabel);
            
            if (GUILayout.Button("搜索GameObject"))
            {
                SearchByType("GameObject");
            }
            
            if (GUILayout.Button("搜索Material"))
            {
                SearchByType("Material");
            }
            
            if (GUILayout.Button("搜索Script"))
            {
                SearchByType("Script");
            }
            
            if (GUILayout.Button("搜索Texture"))
            {
                SearchByType("Texture2D");
            }
            
            GUILayout.Space(10);
            GUILayout.Label("搜索结果", EditorStyles.boldLabel);
            
            if (searchResults.Count > 0)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
                
                for (int i = 0; i < searchResults.Count; i++)
                {
                    var result = searchResults[i];
                    
                    GUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button(result.name, GUILayout.Width(200)))
                    {
                        SelectSearchResult(result);
                    }
                    
                    GUILayout.Label(result.type, GUILayout.Width(80));
                    GUILayout.Label($"{result.score:F2}", GUILayout.Width(50));
                    
                    if (GUILayout.Button("打开", GUILayout.Width(50)))
                    {
                        OpenSearchResult(result);
                    }
                    
                    GUILayout.EndHorizontal();
                }
                
                GUILayout.EndScrollView();
                
                if (GUILayout.Button("导出结果"))
                {
                    ExportSearchResults();
                }
                
                if (GUILayout.Button("清除结果"))
                {
                    ClearSearchResults();
                }
            }
            else
            {
                GUILayout.Label("没有搜索结果");
            }
            
            GUILayout.Space(10);
            GUILayout.Label("搜索统计", EditorStyles.boldLabel);
            
            GUILayout.Label(GetSearchStats());
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            enableSearchService = EditorGUILayout.Toggle("启用搜索服务", enableSearchService);
            
            GUILayout.EndArea();
        }
        
        private Vector2 scrollPosition = Vector2.zero;
    }
} 