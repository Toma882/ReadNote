using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// UnityEngine.Networking 命名空间案例演示
/// 展示网络系统的核心功能
/// </summary>
public class NetworkingExample : MonoBehaviour
{
    [Header("网络设置")]
    [SerializeField] private string serverUrl = "https://httpbin.org/get";
    [SerializeField] private string postUrl = "https://httpbin.org/post";
    [SerializeField] private string downloadUrl = "https://httpbin.org/bytes/1024";
    [SerializeField] private int port = 8080;
    
    [Header("网络状态")]
    [SerializeField] private bool isConnected = false;
    [SerializeField] private string connectionStatus = "未连接";
    [SerializeField] private float downloadProgress = 0f;
    [SerializeField] private bool isDownloading = false;
    
    [Header("网络数据")]
    [SerializeField] private string responseData = "";
    [SerializeField] private string errorMessage = "";
    [SerializeField] private int responseCode = 0;
    
    // 网络请求
    private UnityWebRequest currentRequest;
    private List<UnityWebRequest> activeRequests = new List<UnityWebRequest>();
    
    private void Start()
    {
        InitializeNetworking();
    }
    
    /// <summary>
    /// 初始化网络系统
    /// </summary>
    private void InitializeNetworking()
    {
        // 检查网络连接
        CheckNetworkConnectivity();
        
        Debug.Log("网络系统初始化完成");
    }
    
    /// <summary>
    /// 检查网络连接
    /// </summary>
    private void CheckNetworkConnectivity()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            connectionStatus = "无网络连接";
            isConnected = false;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            connectionStatus = "移动网络连接";
            isConnected = true;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            connectionStatus = "WiFi网络连接";
            isConnected = true;
        }
        
        Debug.Log($"网络状态: {connectionStatus}");
    }
    
    /// <summary>
    /// 发送GET请求
    /// </summary>
    public void SendGetRequest()
    {
        if (!isConnected)
        {
            Debug.LogWarning("无网络连接，无法发送请求");
            return;
        }
        
        StartCoroutine(GetRequestCoroutine(serverUrl));
    }
    
    /// <summary>
    /// GET请求协程
    /// </summary>
    /// <param name="url">请求URL</param>
    private IEnumerator GetRequestCoroutine(string url)
    {
        Debug.Log($"发送GET请求到: {url}");
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            currentRequest = request;
            activeRequests.Add(request);
            
            // 发送请求
            yield return request.SendWebRequest();
            
            // 处理响应
            ProcessResponse(request);
            
            activeRequests.Remove(request);
            currentRequest = null;
        }
    }
    
    /// <summary>
    /// 发送POST请求
    /// </summary>
    /// <param name="data">发送的数据</param>
    public void SendPostRequest(string data = "Hello World")
    {
        if (!isConnected)
        {
            Debug.LogWarning("无网络连接，无法发送请求");
            return;
        }
        
        StartCoroutine(PostRequestCoroutine(postUrl, data));
    }
    
    /// <summary>
    /// POST请求协程
    /// </summary>
    /// <param name="url">请求URL</param>
    /// <param name="data">发送的数据</param>
    private IEnumerator PostRequestCoroutine(string url, string data)
    {
        Debug.Log($"发送POST请求到: {url}");
        Debug.Log($"发送数据: {data}");
        
        byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
        
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            currentRequest = request;
            activeRequests.Add(request);
            
            // 发送请求
            yield return request.SendWebRequest();
            
            // 处理响应
            ProcessResponse(request);
            
            activeRequests.Remove(request);
            currentRequest = null;
        }
    }
    
    /// <summary>
    /// 下载文件
    /// </summary>
    public void DownloadFile()
    {
        if (!isConnected)
        {
            Debug.LogWarning("无网络连接，无法下载文件");
            return;
        }
        
        StartCoroutine(DownloadFileCoroutine(downloadUrl));
    }
    
    /// <summary>
    /// 文件下载协程
    /// </summary>
    /// <param name="url">下载URL</param>
    private IEnumerator DownloadFileCoroutine(string url)
    {
        Debug.Log($"开始下载文件: {url}");
        
        isDownloading = true;
        downloadProgress = 0f;
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            currentRequest = request;
            activeRequests.Add(request);
            
            // 发送请求
            var operation = request.SendWebRequest();
            
            // 监控下载进度
            while (!operation.isDone)
            {
                downloadProgress = request.downloadProgress;
                yield return null;
            }
            
            // 处理响应
            ProcessResponse(request);
            
            activeRequests.Remove(request);
            currentRequest = null;
        }
        
        isDownloading = false;
        downloadProgress = 0f;
    }
    
    /// <summary>
    /// 下载图片
    /// </summary>
    /// <param name="imageUrl">图片URL</param>
    public void DownloadImage(string imageUrl = "https://httpbin.org/image/png")
    {
        if (!isConnected)
        {
            Debug.LogWarning("无网络连接，无法下载图片");
            return;
        }
        
        StartCoroutine(DownloadImageCoroutine(imageUrl));
    }
    
    /// <summary>
    /// 图片下载协程
    /// </summary>
    /// <param name="url">图片URL</param>
    private IEnumerator DownloadImageCoroutine(string url)
    {
        Debug.Log($"开始下载图片: {url}");
        
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            currentRequest = request;
            activeRequests.Add(request);
            
            // 发送请求
            yield return request.SendWebRequest();
            
            // 处理响应
            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                Debug.Log($"图片下载成功，尺寸: {texture.width}x{texture.height}");
                
                // 可以在这里处理图片，比如保存到本地或应用到材质
            }
            else
            {
                Debug.LogError($"图片下载失败: {request.error}");
            }
            
            activeRequests.Remove(request);
            currentRequest = null;
        }
    }
    
    /// <summary>
    /// 发送带认证的请求
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    public void SendAuthenticatedRequest(string username, string password)
    {
        if (!isConnected)
        {
            Debug.LogWarning("无网络连接，无法发送请求");
            return;
        }
        
        StartCoroutine(AuthenticatedRequestCoroutine(serverUrl, username, password));
    }
    
    /// <summary>
    /// 认证请求协程
    /// </summary>
    /// <param name="url">请求URL</param>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    private IEnumerator AuthenticatedRequestCoroutine(string url, string username, string password)
    {
        Debug.Log($"发送认证请求到: {url}");
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // 设置认证头
            string auth = System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            request.SetRequestHeader("Authorization", $"Basic {auth}");
            
            currentRequest = request;
            activeRequests.Add(request);
            
            // 发送请求
            yield return request.SendWebRequest();
            
            // 处理响应
            ProcessResponse(request);
            
            activeRequests.Remove(request);
            currentRequest = null;
        }
    }
    
    /// <summary>
    /// 发送带自定义头的请求
    /// </summary>
    public void SendCustomHeaderRequest()
    {
        if (!isConnected)
        {
            Debug.LogWarning("无网络连接，无法发送请求");
            return;
        }
        
        StartCoroutine(CustomHeaderRequestCoroutine(serverUrl));
    }
    
    /// <summary>
    /// 自定义头请求协程
    /// </summary>
    /// <param name="url">请求URL</param>
    private IEnumerator CustomHeaderRequestCoroutine(string url)
    {
        Debug.Log($"发送自定义头请求到: {url}");
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // 设置自定义头
            request.SetRequestHeader("User-Agent", "Unity Networking Example");
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("X-Custom-Header", "CustomValue");
            
            currentRequest = request;
            activeRequests.Add(request);
            
            // 发送请求
            yield return request.SendWebRequest();
            
            // 处理响应
            ProcessResponse(request);
            
            activeRequests.Remove(request);
            currentRequest = null;
        }
    }
    
    /// <summary>
    /// 处理网络响应
    /// </summary>
    /// <param name="request">网络请求</param>
    private void ProcessResponse(UnityWebRequest request)
    {
        responseCode = (int)request.responseCode;
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            responseData = request.downloadHandler.text;
            errorMessage = "";
            Debug.Log($"请求成功 (状态码: {responseCode})");
            Debug.Log($"响应数据: {responseData}");
        }
        else
        {
            responseData = "";
            errorMessage = request.error;
            Debug.LogError($"请求失败: {request.error}");
        }
    }
    
    /// <summary>
    /// 取消所有请求
    /// </summary>
    public void CancelAllRequests()
    {
        foreach (var request in activeRequests)
        {
            if (request != null)
            {
                request.Abort();
            }
        }
        
        activeRequests.Clear();
        currentRequest = null;
        
        Debug.Log("所有网络请求已取消");
    }
    
    /// <summary>
    /// 获取网络信息
    /// </summary>
    public void GetNetworkInfo()
    {
        Debug.Log("=== 网络信息 ===");
        Debug.Log($"网络可达性: {Application.internetReachability}");
        Debug.Log($"连接状态: {connectionStatus}");
        Debug.Log($"是否连接: {isConnected}");
        Debug.Log($"活跃请求数: {activeRequests.Count}");
        Debug.Log($"当前请求: {(currentRequest != null ? "有" : "无")}");
        Debug.Log($"下载进度: {downloadProgress:P0}");
        Debug.Log($"是否下载中: {isDownloading}");
    }
    
    /// <summary>
    /// 测试网络延迟
    /// </summary>
    public void TestNetworkLatency()
    {
        if (!isConnected)
        {
            Debug.LogWarning("无网络连接，无法测试延迟");
            return;
        }
        
        StartCoroutine(LatencyTestCoroutine());
    }
    
    /// <summary>
    /// 延迟测试协程
    /// </summary>
    private IEnumerator LatencyTestCoroutine()
    {
        Debug.Log("开始网络延迟测试...");
        
        float startTime = Time.time;
        
        using (UnityWebRequest request = UnityWebRequest.Get(serverUrl))
        {
            yield return request.SendWebRequest();
            
            float endTime = Time.time;
            float latency = (endTime - startTime) * 1000f; // 转换为毫秒
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"网络延迟测试完成: {latency:F2}ms");
            }
            else
            {
                Debug.LogError($"延迟测试失败: {request.error}");
            }
        }
    }
    
    /// <summary>
    /// 检查网络连接状态
    /// </summary>
    public void CheckConnectionStatus()
    {
        CheckNetworkConnectivity();
        Debug.Log($"网络连接状态已更新: {connectionStatus}");
    }
    
    private void Update()
    {
        // 定期检查网络状态
        if (Time.frameCount % 300 == 0) // 每300帧检查一次
        {
            CheckNetworkConnectivity();
        }
    }
    
    private void OnDestroy()
    {
        // 清理所有网络请求
        CancelAllRequests();
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 600));
        GUILayout.Label("网络系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 网络状态
        GUILayout.Label($"连接状态: {connectionStatus}");
        GUILayout.Label($"是否连接: {isConnected}");
        GUILayout.Label($"活跃请求: {activeRequests.Count}");
        GUILayout.Label($"响应码: {responseCode}");
        
        if (isDownloading)
        {
            GUILayout.Label($"下载进度: {downloadProgress:P0}");
        }
        
        GUILayout.Space(10);
        
        // 请求按钮
        if (GUILayout.Button("发送GET请求"))
        {
            SendGetRequest();
        }
        
        if (GUILayout.Button("发送POST请求"))
        {
            SendPostRequest();
        }
        
        if (GUILayout.Button("下载文件"))
        {
            DownloadFile();
        }
        
        if (GUILayout.Button("下载图片"))
        {
            DownloadImage();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("发送认证请求"))
        {
            SendAuthenticatedRequest("user", "pass");
        }
        
        if (GUILayout.Button("发送自定义头请求"))
        {
            SendCustomHeaderRequest();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("测试网络延迟"))
        {
            TestNetworkLatency();
        }
        
        if (GUILayout.Button("检查连接状态"))
        {
            CheckConnectionStatus();
        }
        
        if (GUILayout.Button("获取网络信息"))
        {
            GetNetworkInfo();
        }
        
        if (GUILayout.Button("取消所有请求"))
        {
            CancelAllRequests();
        }
        
        GUILayout.Space(10);
        
        // 显示响应数据
        if (!string.IsNullOrEmpty(responseData))
        {
            GUILayout.Label("响应数据:");
            GUILayout.TextArea(responseData, GUILayout.Height(100));
        }
        
        // 显示错误信息
        if (!string.IsNullOrEmpty(errorMessage))
        {
            GUILayout.Label("错误信息:");
            GUILayout.TextArea(errorMessage, GUILayout.Height(50));
        }
        
        GUILayout.EndArea();
    }
} 