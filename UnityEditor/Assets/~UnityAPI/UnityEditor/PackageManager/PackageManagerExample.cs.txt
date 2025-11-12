using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.PackageManager 命名空间案例演示
/// 展示包管理系统的使用，包括包的安装、卸载、更新和查询
/// </summary>
public class PackageManagerExample : MonoBehaviour
{
    [Header("包管理系统配置")]
    [SerializeField] private bool enablePackageManager = true; //启用包管理系统
    [SerializeField] private bool enablePackageLogging = true; //启用包管理日志
    [SerializeField] private bool enablePackageValidation = true; //启用包管理验证
    [SerializeField] private bool enablePackageCache = true; //启用包管理缓存
    [SerializeField] private bool enablePackageAutoUpdate = false; //启用包自动更新
    
    [Header("包操作")]
    [SerializeField] private PackageOperation currentOperation = PackageOperation.None; //当前包操作
    [SerializeField] private string targetPackageName = ""; //目标包名称
    [SerializeField] private string targetPackageVersion = ""; //目标包版本
    [SerializeField] private PackageSource packageSource = PackageSource.Registry; //包源
    [SerializeField] private bool includePrerelease = false; //包含预发布版本
    [SerializeField] private bool includeHidden = false; //包含隐藏包
    
    [Header("包状态")]
    [SerializeField] private string packageManagerState = "未初始化"; //包管理器状态
    [SerializeField] private bool isOperationInProgress = false; //是否有操作正在进行
    [SerializeField] private float operationProgress = 0f; //操作进度
    [SerializeField] private string operationMessage = ""; //操作消息
    [SerializeField] private PackageOperationResult lastOperationResult = PackageOperationResult.None; //最后操作结果
    [SerializeField] private string lastOperationError = ""; //最后操作错误
    
    [Header("包列表")]
    [SerializeField] private PackageInfo[] installedPackages = new PackageInfo[0]; //已安装的包
    [SerializeField] private PackageInfo[] availablePackages = new PackageInfo[0]; //可用的包
    [SerializeField] private PackageInfo[] outdatedPackages = new PackageInfo[0]; //过时的包
    [SerializeField] private int totalInstalledPackages = 0; //总已安装包数
    [SerializeField] private int totalAvailablePackages = 0; //总可用包数
    [SerializeField] private int totalOutdatedPackages = 0; //总过时包数
    
    [Header("包历史")]
    [SerializeField] private PackageHistoryEntry[] packageHistory = new PackageHistoryEntry[10]; //包操作历史
    [SerializeField] private int packageHistoryIndex = 0; //包历史索引
    [SerializeField] private bool enablePackageHistory = true; //启用包历史
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceMonitoring = true; //启用性能监控
    [SerializeField] private float[] operationTimeHistory = new float[100]; //操作时间历史
    [SerializeField] private int operationTimeIndex = 0; //操作时间索引
    [SerializeField] private float averageOperationTime = 0f; //平均操作时间
    [SerializeField] private float maxOperationTime = 0f; //最大操作时间
    [SerializeField] private float totalOperationTime = 0f; //总操作时间
    [SerializeField] private int totalOperationCount = 0; //总操作次数
    
    [Header("包统计")]
    [SerializeField] private long totalPackageSize = 0; //总包大小
    [SerializeField] private int totalPackageDependencies = 0; //总包依赖数
    [SerializeField] private string[] packageCategories = new string[0]; //包分类
    [SerializeField] private Dictionary<string, int> packageCategoryCount = new Dictionary<string, int>(); //包分类统计
    
    private bool isInitialized = false;
    private float operationStartTime = 0f;
    private ListRequest listRequest;
    private SearchRequest searchRequest;
    private AddRequest addRequest;
    private RemoveRequest removeRequest;
    private UpdateRequest updateRequest;

    private void Start()
    {
        InitializePackageManager();
    }

    /// <summary>
    /// 初始化包管理系统
    /// </summary>
    private void InitializePackageManager()
    {
        if (!enablePackageManager) return;
        
        // 初始化包管理状态
        InitializePackageState();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 初始化包统计
        InitializePackageStatistics();
        
        // 刷新包列表
        RefreshPackageList();
        
        isInitialized = true;
        packageManagerState = "已初始化";
        Debug.Log("包管理系统初始化完成");
    }

    /// <summary>
    /// 初始化包管理状态
    /// </summary>
    private void InitializePackageState()
    {
        currentOperation = PackageOperation.None;
        isOperationInProgress = false;
        operationProgress = 0f;
        operationMessage = "就绪";
        lastOperationResult = PackageOperationResult.None;
        lastOperationError = "";
        
        Debug.Log("包管理状态已初始化");
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            operationTimeHistory = new float[100];
            operationTimeIndex = 0;
            averageOperationTime = 0f;
            maxOperationTime = 0f;
            totalOperationTime = 0f;
            totalOperationCount = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    /// <summary>
    /// 初始化包统计
    /// </summary>
    private void InitializePackageStatistics()
    {
        totalPackageSize = 0;
        totalPackageDependencies = 0;
        packageCategories = new string[0];
        packageCategoryCount.Clear();
        
        Debug.Log("包统计初始化完成");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新包管理状态
        UpdatePackageManagerStatus();
        
        // 处理包管理请求
        ProcessPackageRequests();
        
        // 性能监控
        if (enablePerformanceMonitoring)
        {
            UpdatePerformanceMonitoring();
        }
    }

    /// <summary>
    /// 更新包管理状态
    /// </summary>
    private void UpdatePackageManagerStatus()
    {
        if (isOperationInProgress)
        {
            packageManagerState = "操作中";
        }
        else
        {
            packageManagerState = "空闲";
        }
    }

    /// <summary>
    /// 处理包管理请求
    /// </summary>
    private void ProcessPackageRequests()
    {
        // 处理列表请求
        if (listRequest != null && listRequest.IsCompleted)
        {
            ProcessListRequest();
        }
        
        // 处理搜索请求
        if (searchRequest != null && searchRequest.IsCompleted)
        {
            ProcessSearchRequest();
        }
        
        // 处理添加请求
        if (addRequest != null && addRequest.IsCompleted)
        {
            ProcessAddRequest();
        }
        
        // 处理移除请求
        if (removeRequest != null && removeRequest.IsCompleted)
        {
            ProcessRemoveRequest();
        }
        
        // 处理更新请求
        if (updateRequest != null && updateRequest.IsCompleted)
        {
            ProcessUpdateRequest();
        }
    }

    /// <summary>
    /// 处理列表请求
    /// </summary>
    private void ProcessListRequest()
    {
        if (listRequest.Status == StatusCode.Success)
        {
            installedPackages = listRequest.Result.ToArray();
            totalInstalledPackages = installedPackages.Length;
            
            // 更新包统计
            UpdatePackageStatistics();
            
            operationMessage = $"已加载 {totalInstalledPackages} 个已安装包";
            lastOperationResult = PackageOperationResult.Success;
            
            if (enablePackageLogging)
            {
                Debug.Log($"包列表加载成功: {totalInstalledPackages} 个包");
            }
        }
        else
        {
            operationMessage = $"包列表加载失败: {listRequest.Error?.message}";
            lastOperationResult = PackageOperationResult.Failed;
            lastOperationError = listRequest.Error?.message ?? "未知错误";
            
            Debug.LogError($"包列表加载失败: {listRequest.Error?.message}");
        }
        
        listRequest = null;
        isOperationInProgress = false;
        operationProgress = 1f;
        
        // 更新性能数据
        UpdateOperationPerformance();
    }

    /// <summary>
    /// 处理搜索请求
    /// </summary>
    private void ProcessSearchRequest()
    {
        if (searchRequest.Status == StatusCode.Success)
        {
            availablePackages = searchRequest.Result.ToArray();
            totalAvailablePackages = availablePackages.Length;
            
            operationMessage = $"搜索完成: 找到 {totalAvailablePackages} 个可用包";
            lastOperationResult = PackageOperationResult.Success;
            
            if (enablePackageLogging)
            {
                Debug.Log($"包搜索成功: {totalAvailablePackages} 个包");
            }
        }
        else
        {
            operationMessage = $"包搜索失败: {searchRequest.Error?.message}";
            lastOperationResult = PackageOperationResult.Failed;
            lastOperationError = searchRequest.Error?.message ?? "未知错误";
            
            Debug.LogError($"包搜索失败: {searchRequest.Error?.message}");
        }
        
        searchRequest = null;
        isOperationInProgress = false;
        operationProgress = 1f;
        
        // 更新性能数据
        UpdateOperationPerformance();
    }

    /// <summary>
    /// 处理添加请求
    /// </summary>
    private void ProcessAddRequest()
    {
        if (addRequest.Status == StatusCode.Success)
        {
            operationMessage = $"包安装成功: {addRequest.Result.name}";
            lastOperationResult = PackageOperationResult.Success;
            
            // 记录到历史
            if (enablePackageHistory)
            {
                AddPackageHistoryEntry(PackageOperation.Install, addRequest.Result.name, addRequest.Result.version);
            }
            
            // 刷新包列表
            RefreshPackageList();
            
            if (enablePackageLogging)
            {
                Debug.Log($"包安装成功: {addRequest.Result.name} v{addRequest.Result.version}");
            }
        }
        else
        {
            operationMessage = $"包安装失败: {addRequest.Error?.message}";
            lastOperationResult = PackageOperationResult.Failed;
            lastOperationError = addRequest.Error?.message ?? "未知错误";
            
            Debug.LogError($"包安装失败: {addRequest.Error?.message}");
        }
        
        addRequest = null;
        isOperationInProgress = false;
        operationProgress = 1f;
        
        // 更新性能数据
        UpdateOperationPerformance();
    }

    /// <summary>
    /// 处理移除请求
    /// </summary>
    private void ProcessRemoveRequest()
    {
        if (removeRequest.Status == StatusCode.Success)
        {
            operationMessage = $"包卸载成功: {targetPackageName}";
            lastOperationResult = PackageOperationResult.Success;
            
            // 记录到历史
            if (enablePackageHistory)
            {
                AddPackageHistoryEntry(PackageOperation.Uninstall, targetPackageName, "");
            }
            
            // 刷新包列表
            RefreshPackageList();
            
            if (enablePackageLogging)
            {
                Debug.Log($"包卸载成功: {targetPackageName}");
            }
        }
        else
        {
            operationMessage = $"包卸载失败: {removeRequest.Error?.message}";
            lastOperationResult = PackageOperationResult.Failed;
            lastOperationError = removeRequest.Error?.message ?? "未知错误";
            
            Debug.LogError($"包卸载失败: {removeRequest.Error?.message}");
        }
        
        removeRequest = null;
        isOperationInProgress = false;
        operationProgress = 1f;
        
        // 更新性能数据
        UpdateOperationPerformance();
    }

    /// <summary>
    /// 处理更新请求
    /// </summary>
    private void ProcessUpdateRequest()
    {
        if (updateRequest.Status == StatusCode.Success)
        {
            operationMessage = $"包更新成功: {updateRequest.Result.name}";
            lastOperationResult = PackageOperationResult.Success;
            
            // 记录到历史
            if (enablePackageHistory)
            {
                AddPackageHistoryEntry(PackageOperation.Update, updateRequest.Result.name, updateRequest.Result.version);
            }
            
            // 刷新包列表
            RefreshPackageList();
            
            if (enablePackageLogging)
            {
                Debug.Log($"包更新成功: {updateRequest.Result.name} v{updateRequest.Result.version}");
            }
        }
        else
        {
            operationMessage = $"包更新失败: {updateRequest.Error?.message}";
            lastOperationResult = PackageOperationResult.Failed;
            lastOperationError = updateRequest.Error?.message ?? "未知错误";
            
            Debug.LogError($"包更新失败: {updateRequest.Error?.message}");
        }
        
        updateRequest = null;
        isOperationInProgress = false;
        operationProgress = 1f;
        
        // 更新性能数据
        UpdateOperationPerformance();
    }

    /// <summary>
    /// 更新包统计
    /// </summary>
    private void UpdatePackageStatistics()
    {
        totalPackageSize = 0;
        totalPackageDependencies = 0;
        packageCategoryCount.Clear();
        
        foreach (var package in installedPackages)
        {
            // 统计包大小（这里只是示例，实际需要从包信息中获取）
            totalPackageSize += 1024 * 1024; // 假设每个包1MB
            
            // 统计依赖数
            if (package.dependencies != null)
            {
                totalPackageDependencies += package.dependencies.Length;
            }
            
            // 统计分类
            string category = GetPackageCategory(package.name);
            if (!packageCategoryCount.ContainsKey(category))
            {
                packageCategoryCount[category] = 0;
            }
            packageCategoryCount[category]++;
        }
        
        packageCategories = new string[packageCategoryCount.Count];
        packageCategoryCount.Keys.CopyTo(packageCategories, 0);
    }

    /// <summary>
    /// 获取包分类
    /// </summary>
    /// <param name="packageName">包名称</param>
    /// <returns>包分类</returns>
    private string GetPackageCategory(string packageName)
    {
        if (packageName.StartsWith("com.unity."))
        {
            return "Unity官方包";
        }
        else if (packageName.StartsWith("com.unity.render-pipelines."))
        {
            return "渲染管线";
        }
        else if (packageName.StartsWith("com.unity.textmeshpro"))
        {
            return "文本系统";
        }
        else if (packageName.StartsWith("com.unity.inputsystem"))
        {
            return "输入系统";
        }
        else
        {
            return "第三方包";
        }
    }

    /// <summary>
    /// 更新操作性能数据
    /// </summary>
    private void UpdateOperationPerformance()
    {
        if (enablePerformanceMonitoring)
        {
            float operationTime = Time.realtimeSinceStartup - operationStartTime;
            
            operationTimeHistory[operationTimeIndex] = operationTime;
            operationTimeIndex = (operationTimeIndex + 1) % 100;
            
            totalOperationTime += operationTime;
            totalOperationCount++;
            
            if (operationTime > maxOperationTime)
            {
                maxOperationTime = operationTime;
            }
        }
    }

    /// <summary>
    /// 更新性能监控
    /// </summary>
    private void UpdatePerformanceMonitoring()
    {
        // 计算平均操作时间
        if (totalOperationCount > 0)
        {
            averageOperationTime = totalOperationTime / totalOperationCount;
        }
    }

    /// <summary>
    /// 刷新包列表
    /// </summary>
    public void RefreshPackageList()
    {
        if (isOperationInProgress)
        {
            Debug.LogWarning("有操作正在进行中，请等待完成");
            return;
        }
        
        isOperationInProgress = true;
        operationStartTime = Time.realtimeSinceStartup;
        currentOperation = PackageOperation.List;
        operationProgress = 0f;
        operationMessage = "正在加载包列表...";
        
        listRequest = Client.List();
        
        Debug.Log("开始刷新包列表");
    }

    /// <summary>
    /// 搜索包
    /// </summary>
    /// <param name="searchTerm">搜索词</param>
    public void SearchPackages(string searchTerm)
    {
        if (isOperationInProgress)
        {
            Debug.LogWarning("有操作正在进行中，请等待完成");
            return;
        }
        
        if (string.IsNullOrEmpty(searchTerm))
        {
            Debug.LogWarning("搜索词不能为空");
            return;
        }
        
        isOperationInProgress = true;
        operationStartTime = Time.realtimeSinceStartup;
        currentOperation = PackageOperation.Search;
        operationProgress = 0f;
        operationMessage = $"正在搜索包: {searchTerm}";
        
        searchRequest = Client.Search(searchTerm);
        
        Debug.Log($"开始搜索包: {searchTerm}");
    }

    /// <summary>
    /// 安装包
    /// </summary>
    /// <param name="packageName">包名称</param>
    /// <param name="version">版本（可选）</param>
    public void InstallPackage(string packageName, string version = "")
    {
        if (isOperationInProgress)
        {
            Debug.LogWarning("有操作正在进行中，请等待完成");
            return;
        }
        
        if (string.IsNullOrEmpty(packageName))
        {
            Debug.LogWarning("包名称不能为空");
            return;
        }
        
        isOperationInProgress = true;
        operationStartTime = Time.realtimeSinceStartup;
        currentOperation = PackageOperation.Install;
        operationProgress = 0f;
        operationMessage = $"正在安装包: {packageName}";
        
        targetPackageName = packageName;
        targetPackageVersion = version;
        
        if (string.IsNullOrEmpty(version))
        {
            addRequest = Client.Add(packageName);
        }
        else
        {
            addRequest = Client.Add($"{packageName}@{version}");
        }
        
        Debug.Log($"开始安装包: {packageName} {version}");
    }

    /// <summary>
    /// 卸载包
    /// </summary>
    /// <param name="packageName">包名称</param>
    public void UninstallPackage(string packageName)
    {
        if (isOperationInProgress)
        {
            Debug.LogWarning("有操作正在进行中，请等待完成");
            return;
        }
        
        if (string.IsNullOrEmpty(packageName))
        {
            Debug.LogWarning("包名称不能为空");
            return;
        }
        
        isOperationInProgress = true;
        operationStartTime = Time.realtimeSinceStartup;
        currentOperation = PackageOperation.Uninstall;
        operationProgress = 0f;
        operationMessage = $"正在卸载包: {packageName}";
        
        targetPackageName = packageName;
        
        removeRequest = Client.Remove(packageName);
        
        Debug.Log($"开始卸载包: {packageName}");
    }

    /// <summary>
    /// 更新包
    /// </summary>
    /// <param name="packageName">包名称</param>
    public void UpdatePackage(string packageName)
    {
        if (isOperationInProgress)
        {
            Debug.LogWarning("有操作正在进行中，请等待完成");
            return;
        }
        
        if (string.IsNullOrEmpty(packageName))
        {
            Debug.LogWarning("包名称不能为空");
            return;
        }
        
        isOperationInProgress = true;
        operationStartTime = Time.realtimeSinceStartup;
        currentOperation = PackageOperation.Update;
        operationProgress = 0f;
        operationMessage = $"正在更新包: {packageName}";
        
        targetPackageName = packageName;
        
        updateRequest = Client.Update(packageName);
        
        Debug.Log($"开始更新包: {packageName}");
    }

    /// <summary>
    /// 检查过时的包
    /// </summary>
    public void CheckOutdatedPackages()
    {
        // 这里应该实现检查过时包的逻辑
        // 由于Unity的包管理器API限制，这里只是示例
        outdatedPackages = new PackageInfo[0];
        totalOutdatedPackages = 0;
        
        Debug.Log("过时包检查完成");
    }

    /// <summary>
    /// 添加包历史条目
    /// </summary>
    /// <param name="operation">操作类型</param>
    /// <param name="packageName">包名称</param>
    /// <param name="version">版本</param>
    private void AddPackageHistoryEntry(PackageOperation operation, string packageName, string version)
    {
        var entry = new PackageHistoryEntry
        {
            timestamp = System.DateTime.Now.ToString(),
            operation = operation.ToString(),
            packageName = packageName,
            version = version,
            result = lastOperationResult.ToString()
        };
        
        packageHistory[packageHistoryIndex] = entry;
        packageHistoryIndex = (packageHistoryIndex + 1) % packageHistory.Length;
    }

    /// <summary>
    /// 生成包管理报告
    /// </summary>
    public void GeneratePackageReport()
    {
        Debug.Log("=== 包管理系统报告 ===");
        Debug.Log($"包管理系统状态: {packageManagerState}");
        Debug.Log($"当前操作: {currentOperation}");
        Debug.Log($"最后操作结果: {lastOperationResult}");
        Debug.Log($"总已安装包数: {totalInstalledPackages}");
        Debug.Log($"总可用包数: {totalAvailablePackages}");
        Debug.Log($"总过时包数: {totalOutdatedPackages}");
        Debug.Log($"总包大小: {FormatFileSize(totalPackageSize)}");
        Debug.Log($"总包依赖数: {totalPackageDependencies}");
        Debug.Log($"总操作次数: {totalOperationCount}");
        Debug.Log($"平均操作时间: {averageOperationTime:F2}秒");
        Debug.Log($"最大操作时间: {maxOperationTime:F2}秒");
        Debug.Log($"总操作时间: {totalOperationTime:F2}秒");
        
        Debug.Log("=== 包分类统计 ===");
        foreach (var category in packageCategories)
        {
            Debug.Log($"{category}: {packageCategoryCount[category]} 个包");
        }
    }

    /// <summary>
    /// 格式化文件大小
    /// </summary>
    /// <param name="bytes">字节数</param>
    /// <returns>格式化的文件大小字符串</returns>
    private string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes;
        int order = 0;
        
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        
        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// 清除包历史
    /// </summary>
    public void ClearPackageHistory()
    {
        packageHistory = new PackageHistoryEntry[10];
        packageHistoryIndex = 0;
        Debug.Log("包历史已清除");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("PackageManager 包管理系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("包管理系统配置:");
        enablePackageManager = GUILayout.Toggle(enablePackageManager, "启用包管理系统");
        enablePackageLogging = GUILayout.Toggle(enablePackageLogging, "启用包管理日志");
        enablePackageValidation = GUILayout.Toggle(enablePackageValidation, "启用包管理验证");
        enablePackageCache = GUILayout.Toggle(enablePackageCache, "启用包管理缓存");
        enablePackageAutoUpdate = GUILayout.Toggle(enablePackageAutoUpdate, "启用包自动更新");
        
        GUILayout.Space(10);
        GUILayout.Label("包操作:");
        targetPackageName = GUILayout.TextField("目标包名称", targetPackageName);
        targetPackageVersion = GUILayout.TextField("目标包版本", targetPackageVersion);
        packageSource = (PackageSource)System.Enum.Parse(typeof(PackageSource), GUILayout.TextField("包源", packageSource.ToString()));
        includePrerelease = GUILayout.Toggle(includePrerelease, "包含预发布版本");
        includeHidden = GUILayout.Toggle(includeHidden, "包含隐藏包");
        
        GUILayout.Space(10);
        GUILayout.Label("包状态:");
        GUILayout.Label($"包管理器状态: {packageManagerState}");
        GUILayout.Label($"当前操作: {currentOperation}");
        GUILayout.Label($"操作进度: {operationProgress * 100:F1}%");
        GUILayout.Label($"操作消息: {operationMessage}");
        GUILayout.Label($"最后结果: {lastOperationResult}");
        GUILayout.Label($"总已安装包数: {totalInstalledPackages}");
        GUILayout.Label($"总可用包数: {totalAvailablePackages}");
        GUILayout.Label($"总过时包数: {totalOutdatedPackages}");
        GUILayout.Label($"总操作次数: {totalOperationCount}");
        GUILayout.Label($"平均操作时间: {averageOperationTime:F2}秒");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("刷新包列表"))
        {
            RefreshPackageList();
        }
        
        if (GUILayout.Button("搜索包"))
        {
            SearchPackages(targetPackageName);
        }
        
        if (GUILayout.Button("安装包"))
        {
            InstallPackage(targetPackageName, targetPackageVersion);
        }
        
        if (GUILayout.Button("卸载包"))
        {
            UninstallPackage(targetPackageName);
        }
        
        if (GUILayout.Button("更新包"))
        {
            UpdatePackage(targetPackageName);
        }
        
        if (GUILayout.Button("检查过时包"))
        {
            CheckOutdatedPackages();
        }
        
        if (GUILayout.Button("生成包管理报告"))
        {
            GeneratePackageReport();
        }
        
        if (GUILayout.Button("清除包历史"))
        {
            ClearPackageHistory();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("包操作历史:");
        for (int i = 0; i < packageHistory.Length; i++)
        {
            if (packageHistory[i] != null && !string.IsNullOrEmpty(packageHistory[i].timestamp))
            {
                GUILayout.Label($"{packageHistory[i].timestamp} - {packageHistory[i].operation} - {packageHistory[i].packageName}");
            }
        }
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 包操作类型
/// </summary>
public enum PackageOperation
{
    None,
    List,
    Search,
    Install,
    Uninstall,
    Update
}

/// <summary>
/// 包操作结果
/// </summary>
public enum PackageOperationResult
{
    None,
    Success,
    Failed,
    Cancelled
}

/// <summary>
/// 包源
/// </summary>
public enum PackageSource
{
    Registry,
    Git,
    Local,
    Embedded
}

/// <summary>
/// 包历史条目
/// </summary>
[System.Serializable]
public class PackageHistoryEntry
{
    public string timestamp;
    public string operation;
    public string packageName;
    public string version;
    public string result;
} 