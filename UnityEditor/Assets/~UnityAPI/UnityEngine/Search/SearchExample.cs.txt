using UnityEngine;
#if UNITY_2020_1_OR_NEWER
using UnityEngine.SearchService;
#endif

/// <summary>
/// UnityEngine.Search 命名空间案例演示
/// 展示SearchService、SearchProvider等核心功能
/// </summary>
public class SearchExample : MonoBehaviour
{
    [Header("搜索设置")]
    [SerializeField] private string searchQuery = "GameObject"; //搜索查询
    [SerializeField] private bool enableSearch = true; //是否启用搜索
    [SerializeField] private int maxResults = 10; //最大结果数

    private void Start()
    {
#if UNITY_2020_1_OR_NEWER
        if (enableSearch)
        {
            Debug.Log("Search Service 已启用");
            // 这里可以初始化搜索服务
        }
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && enableSearch)
        {
            PerformSearch();
        }
    }

    /// <summary>
    /// 执行搜索
    /// </summary>
    private void PerformSearch()
    {
#if UNITY_2020_1_OR_NEWER
        Debug.Log($"执行搜索: {searchQuery}");
        // 这里可以实现具体的搜索逻辑
        // 由于SearchService主要用于编辑器，这里只是演示框架
#else
        Debug.Log("Search Service 需要 Unity 2020.1 或更高版本");
#endif
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 200));
        GUILayout.Label("Search 搜索系统演示", UnityEditor.EditorStyles.boldLabel);
        searchQuery = GUILayout.TextField(searchQuery);
        maxResults = int.TryParse(GUILayout.TextField(maxResults.ToString()), out var max) ? max : maxResults;
        enableSearch = GUILayout.Toggle(enableSearch, "启用搜索");
        if (GUILayout.Button("执行搜索") || Input.GetKeyDown(KeyCode.S))
        {
            PerformSearch();
        }
        GUILayout.Label("按S键快速搜索");
        GUILayout.EndArea();
    }
} 