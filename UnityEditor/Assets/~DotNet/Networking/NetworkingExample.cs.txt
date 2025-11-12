// NetworkingExample.cs
// .NET网络API使用详解示例
// 包含HttpClient、WebClient、Socket、WebSocket、网络工具类等
// 每个方法、关键步骤、枚举值均有详细中文注释
// 适合.NET初学者学习和查阅
// 
// 主要功能模块：
// 1. HttpClient - 现代HTTP客户端，支持异步操作
// 2. WebClient - 传统网络客户端，简单易用
// 3. Socket - 底层网络套接字编程
// 4. WebSocket - 全双工通信协议
// 5. 网络工具类 - URI、IP地址、DNS等
// 6. 网络信息 - 网络接口、连接状态等
// 7. 高级网络操作 - 代理、认证、SSL等

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotNet.Networking
{
    /// <summary>
    /// .NET网络API使用详解示例
    /// 演示HttpClient、WebClient、Socket、WebSocket、网络工具类等
    /// 
    /// 重要说明：
    /// - HttpClient推荐使用using语句或单例模式，避免频繁创建销毁
    /// - 异步操作使用async/await模式，避免阻塞主线程
    /// - 网络操作需要适当的错误处理和超时设置
    /// - 跨平台注意：某些网络功能在不同平台可能有差异
    /// </summary>
    public class NetworkingExample : MonoBehaviour
    {
        [Header("网络示例配置")]
        [Tooltip("是否自动运行所有示例代码")]
        [SerializeField] private bool runExamples = true;
        [Tooltip("测试URL地址 - 使用httpbin.org提供的测试服务")]
        [SerializeField] private string testUrl = "https://httpbin.org/get";
        [Tooltip("本地测试端口 - 用于Socket和WebSocket测试")]
        [SerializeField] private int testPort = 8080;
        [Tooltip("网络超时时间（秒）")]
        [SerializeField] private int timeoutSeconds = 10;

        private void Start()
        {
            if (runExamples)
            {
                RunAllExamples();
            }
        }

        /// <summary>
        /// 运行所有网络相关示例
        /// 按顺序执行：HttpClient -> 异步Http -> WebClient -> Socket -> WebSocket -> 网络工具类 -> 高级网络操作
        /// 
        /// 执行流程：
        /// 1. 基础HTTP客户端操作
        /// 2. 异步网络请求处理
        /// 3. 传统WebClient使用
        /// 4. 底层Socket编程
        /// 5. WebSocket实时通信
        /// 6. 网络工具类和信息查询
        /// 7. 高级网络功能
        /// </summary>
        private async void RunAllExamples()
        {
            Debug.Log("=== .NET网络API示例开始 ===");
            Debug.Log($"测试URL: {testUrl}");
            Debug.Log($"测试端口: {testPort}");
            Debug.Log($"超时时间: {timeoutSeconds}秒");
            
            HttpClientExample();        // HttpClient HTTP客户端
            await AsyncHttpExample();   // 异步HTTP操作
            WebClientExample();         // WebClient 网络客户端
            SocketExample();            // Socket 套接字
            await WebSocketExample();   // WebSocket 网络套接字
            NetworkUtilityExample();    // 网络工具类
            AdvancedNetworkingExample(); // 高级网络操作
            
            Debug.Log("=== .NET网络API示例结束 ===");
        }

        // ================= HttpClient HTTP客户端 =================
        /// <summary>
        /// HttpClient HTTP客户端示例
        /// HttpClient是.NET中推荐的HTTP客户端，支持现代HTTP功能
        /// 
        /// 主要特性：
        /// - 支持HTTP/1.1和HTTP/2
        /// - 内置连接池管理
        /// - 支持异步操作
        /// - 可配置超时和重试
        /// - 支持现代安全特性
        /// 
        /// 注意事项：
        /// - 建议使用单例模式或HttpClientFactory
        /// - 避免频繁创建和销毁HttpClient实例
        /// - 设置适当的超时时间
        /// - 处理网络异常和HTTP错误状态码
        /// </summary>
        private void HttpClientExample()
        {
            Debug.Log("--- HttpClient HTTP客户端示例 ---");
            
            try
            {
                // ========== 创建和配置HttpClient ==========
                
                // 创建HttpClient实例
                // 参数说明：无参数构造函数创建默认配置的HttpClient
                // 返回值：HttpClient实例，需要using语句管理生命周期
                using (HttpClient client = new HttpClient())
                {
                    // 设置超时时间
                    // 参数说明：TimeSpan.FromSeconds(seconds) - 超时时间
                    // 注意事项：超时时间过短可能导致请求失败，过长可能影响用户体验
                    client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
                    Debug.Log($"HttpClient超时时间: {client.Timeout}");
                    
                    // 设置默认请求头
                    // 参数说明：headerName - 请求头名称, headerValue - 请求头值
                    // 返回值：void，请求头会在所有请求中自动添加
                    client.DefaultRequestHeaders.Add("User-Agent", "Unity-Networking-Example");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Custom-Header", "CustomValue");
                    Debug.Log("已设置默认请求头");
                    
                    // ========== GET请求操作 ==========
                    
                    // 基本GET请求
                    // 参数说明：url - 请求的URL地址
                    // 返回值：Task<string> - 异步任务，包含响应内容
                    // 注意事项：使用.Result会阻塞线程，生产环境建议使用await
                    string response = client.GetStringAsync(testUrl).Result;
                    Debug.Log($"GET请求响应长度: {response.Length}");
                    Debug.Log($"GET请求响应前200字符: {response.Substring(0, Math.Min(200, response.Length))}...");
                    
                    // 带查询参数的GET请求
                    // 参数说明：Dictionary<string, string> - 键值对形式的查询参数
                    // 返回值：string - 构建的查询字符串
                    var queryParams = new Dictionary<string, string>
                    {
                        { "param1", "value1" },
                        { "param2", "value2" },
                        { "timestamp", DateTime.Now.Ticks.ToString() }
                    };
                    var queryString = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
                    var urlWithParams = $"{testUrl}?{queryString}";
                    string paramResponse = client.GetStringAsync(urlWithParams).Result;
                    Debug.Log($"带参数GET请求响应长度: {paramResponse.Length}");
                    
                    // ========== POST请求操作 ==========
                    
                    // JSON POST请求
                    // 参数说明：object - 要序列化的对象
                    // 返回值：string - JSON格式的字符串
                    var jsonData = new { 
                        name = "test", 
                        value = 123, 
                        timestamp = DateTime.Now,
                        isActive = true,
                        tags = new[] { "tag1", "tag2" }
                    };
                    var jsonContent = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(jsonData),
                        Encoding.UTF8,
                        "application/json"
                    );
                    
                    // 参数说明：url - 请求地址, content - 请求内容
                    // 返回值：HttpResponseMessage - 包含状态码、头信息、内容等
                    var postResponse = client.PostAsync("https://httpbin.org/post", jsonContent).Result;
                    string postResult = postResponse.Content.ReadAsStringAsync().Result;
                    Debug.Log($"POST请求响应状态码: {postResponse.StatusCode}");
                    Debug.Log($"POST请求响应长度: {postResult.Length}");
                    
                    // 表单POST请求
                    // 参数说明：List<KeyValuePair<string, string>> - 表单字段列表
                    // 返回值：FormUrlEncodedContent - 编码后的表单内容
                    var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("field1", "value1"),
                        new KeyValuePair<string, string>("field2", "value2"),
                        new KeyValuePair<string, string>("number", "42"),
                        new KeyValuePair<string, string>("date", DateTime.Now.ToString("yyyy-MM-dd"))
                    };
                    var formContent = new FormUrlEncodedContent(formData);
                    var formResponse = client.PostAsync("https://httpbin.org/post", formContent).Result;
                    string formResult = formResponse.Content.ReadAsStringAsync().Result;
                    Debug.Log($"表单POST请求响应状态码: {formResponse.StatusCode}");
                    
                    // ========== 文件上传操作 ==========
                    
                    // 创建测试文件内容
                    string testFileContent = "This is a test file for upload\nLine 2\nLine 3";
                    var fileContent = new StringContent(testFileContent, Encoding.UTF8, "text/plain");
                    
                    // 参数说明：content - 文件内容, name - 表单字段名, fileName - 文件名
                    // 返回值：MultipartFormDataContent - 多部分表单数据
                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(fileContent, "file", "test.txt");
                    multipartContent.Add(new StringContent("file description"), "description");
                    multipartContent.Add(new StringContent(DateTime.Now.ToString()), "uploadTime");
                    
                    var uploadResponse = client.PostAsync("https://httpbin.org/post", multipartContent).Result;
                    string uploadResult = uploadResponse.Content.ReadAsStringAsync().Result;
                    Debug.Log($"文件上传响应状态码: {uploadResponse.StatusCode}");
                    
                    // ========== 响应处理 ==========
                    
                    // 检查响应状态
                    // 参数说明：无
                    // 返回值：HttpStatusCode - HTTP状态码枚举
                    Debug.Log($"POST响应状态码: {postResponse.StatusCode}");
                    Debug.Log($"POST响应是否成功: {postResponse.IsSuccessStatusCode}");
                    Debug.Log($"POST响应原因短语: {postResponse.ReasonPhrase}");
                    
                    // 获取响应头
                    // 参数说明：无
                    // 返回值：HttpResponseHeaders - 响应头集合
                    Debug.Log("响应头信息:");
                    foreach (var header in postResponse.Headers)
                    {
                        Debug.Log($"  {header.Key}: {string.Join(", ", header.Value)}");
                    }
                    
                    // 获取内容头
                    // 参数说明：无
                    // 返回值：HttpContentHeaders - 内容头集合
                    Debug.Log("内容头信息:");
                    foreach (var header in postResponse.Content.Headers)
                    {
                        Debug.Log($"  {header.Key}: {string.Join(", ", header.Value)}");
                    }
                    
                    // ========== 错误处理 ==========
                    
                    try
                    {
                        // 测试404错误
                        var errorResponse = client.GetAsync("https://httpbin.org/status/404").Result;
                        Debug.Log($"错误响应状态码: {errorResponse.StatusCode}");
                        
                        if (!errorResponse.IsSuccessStatusCode)
                        {
                            Debug.LogWarning($"HTTP错误: {errorResponse.StatusCode} - {errorResponse.ReasonPhrase}");
                            
                            // 读取错误响应内容
                            string errorContent = errorResponse.Content.ReadAsStringAsync().Result;
                            Debug.LogWarning($"错误响应内容: {errorContent}");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Debug.LogError($"HTTP请求异常: {ex.Message}");
                        Debug.LogError($"异常类型: {ex.GetType().Name}");
                    }
                    catch (TaskCanceledException ex)
                    {
                        Debug.LogError($"请求超时异常: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"其他网络异常: {ex.Message}");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"HttpClient操作出错: {ex.Message}");
                Debug.LogError($"异常堆栈: {ex.StackTrace}");
            }
        }

        // ================= 异步HTTP操作 =================
        /// <summary>
        /// 异步HTTP操作示例
        /// 使用async/await进行异步HTTP请求
        /// 
        /// 主要特性：
        /// - 非阻塞式网络请求
        /// - 支持并发请求
        /// - 更好的性能和响应性
        /// - 适合UI应用程序
        /// 
        /// 注意事项：
        /// - 使用async/await避免阻塞主线程
        /// - 正确处理异步异常
        /// - 考虑取消令牌(CancellationToken)支持
        /// - 避免async void，除非是事件处理程序
        /// </summary>
        private async Task AsyncHttpExample()
        {
            Debug.Log("--- 异步HTTP操作示例 ---");
            
            try
            {
                // ========== 创建HttpClient ==========
                
                // 使用using语句确保资源正确释放
                using (HttpClient client = new HttpClient())
                {
                    // 配置客户端
                    client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
                    client.DefaultRequestHeaders.Add("User-Agent", "Unity-Async-Networking");
                    
                    // ========== 异步GET请求 ==========
                    
                    // 基本异步GET请求
                    // 参数说明：url - 请求地址
                    // 返回值：string - 响应内容
                    // 注意事项：await会等待请求完成，但不会阻塞线程
                    Debug.Log("开始异步GET请求...");
                    string response = await client.GetStringAsync(testUrl);
                    Debug.Log($"异步GET请求完成，响应长度: {response.Length}");
                    
                    // 并发异步请求
                    // 参数说明：多个Task<string> - 并发执行的请求任务
                    // 返回值：string[] - 所有响应的数组
                    Debug.Log("开始并发异步请求...");
                    var tasks = new List<Task<string>>
                    {
                        client.GetStringAsync("https://httpbin.org/delay/1"),
                        client.GetStringAsync("https://httpbin.org/delay/2"),
                        client.GetStringAsync("https://httpbin.org/delay/1")
                    };
                    
                    string[] results = await Task.WhenAll(tasks);
                    Debug.Log($"并发请求完成，共{results.Length}个响应");
                    for (int i = 0; i < results.Length; i++)
                    {
                        Debug.Log($"响应{i + 1}长度: {results[i].Length}");
                    }
                    
                    // ========== 异步POST请求 ==========
                    
                    // JSON异步POST请求
                    var jsonData = new { 
                        message = "异步POST请求", 
                        timestamp = DateTime.Now,
                        data = new { id = 1, name = "test" }
                    };
                    var jsonContent = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(jsonData),
                        Encoding.UTF8,
                        "application/json"
                    );
                    
                    Debug.Log("开始异步POST请求...");
                    var postResponse = await client.PostAsync("https://httpbin.org/post", jsonContent);
                    string postResult = await postResponse.Content.ReadAsStringAsync();
                    Debug.Log($"异步POST请求完成，状态码: {postResponse.StatusCode}");
                    
                    // ========== 异步文件上传 ==========
                    
                    // 创建测试文件内容
                    string fileContent = "异步文件上传测试内容\n" + 
                                       "第二行内容\n" + 
                                       "第三行内容";
                    var fileStringContent = new StringContent(fileContent, Encoding.UTF8, "text/plain");
                    
                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(fileStringContent, "file", "async_test.txt");
                    multipartContent.Add(new StringContent("异步上传的文件"), "description");
                    
                    Debug.Log("开始异步文件上传...");
                    var uploadResponse = await client.PostAsync("https://httpbin.org/post", multipartContent);
                    string uploadResult = await uploadResponse.Content.ReadAsStringAsync();
                    Debug.Log($"异步文件上传完成，状态码: {uploadResponse.StatusCode}");
                    
                    // ========== 异步错误处理 ==========
                    
                    try
                    {
                        // 测试异步错误请求
                        var errorResponse = await client.GetAsync("https://httpbin.org/status/500");
                        Debug.Log($"异步错误响应状态码: {errorResponse.StatusCode}");
                        
                        if (!errorResponse.IsSuccessStatusCode)
                        {
                            string errorContent = await errorResponse.Content.ReadAsStringAsync();
                            Debug.LogWarning($"异步错误响应内容: {errorContent}");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Debug.LogError($"异步HTTP请求异常: {ex.Message}");
                    }
                    catch (TaskCanceledException ex)
                    {
                        Debug.LogError($"异步请求超时: {ex.Message}");
                    }
                    
                    // ========== 异步流处理 ==========
                    
                    try
                    {
                        Debug.Log("开始异步流处理...");
                        var streamResponse = await client.GetAsync("https://httpbin.org/stream/3");
                        
                        using (var stream = await streamResponse.Content.ReadAsStreamAsync())
                        using (var reader = new StreamReader(stream))
                        {
                            string line;
                            int lineCount = 0;
                            while ((line = await reader.ReadLineAsync()) != null && lineCount < 10)
                            {
                                Debug.Log($"流数据行{++lineCount}: {line}");
                            }
                        }
                        Debug.Log("异步流处理完成");
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"异步流处理异常: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"异步HTTP操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        // ================= WebClient 网络客户端 =================
        /// <summary>
        /// WebClient 网络客户端示例
        /// WebClient提供简单的网络操作接口（已过时，推荐使用HttpClient）
        /// </summary>
        private void WebClientExample()
        {
            Debug.Log("--- WebClient 网络客户端示例 ---");
            
            try
            {
                using (WebClient client = new WebClient())
                {
                    // ========== 基本配置 ==========
                    
                    // 设置请求头
                    client.Headers.Add("User-Agent", "Unity-WebClient-Example");
                    client.Headers.Add("Accept", "application/json");
                    Debug.Log("已设置WebClient请求头");
                    
                    // 设置超时
                    client.Timeout = 10000; // 10秒
                    Debug.Log($"WebClient超时时间: {client.Timeout}ms");
                    
                    // ========== 下载操作 ==========
                    
                    // 下载字符串
                    string response = client.DownloadString(testUrl);
                    Debug.Log($"WebClient下载字符串长度: {response.Length}");
                    Debug.Log($"WebClient下载字符串前100字符: {response.Substring(0, Math.Min(100, response.Length))}...");
                    
                    // 下载字节数组
                    byte[] responseBytes = client.DownloadData(testUrl);
                    Debug.Log($"WebClient下载字节数组长度: {responseBytes.Length}");
                    
                    // 下载文件
                    string downloadPath = Path.Combine(Application.persistentDataPath, "downloaded_file.txt");
                    client.DownloadFile(testUrl, downloadPath);
                    Debug.Log($"文件已下载到: {downloadPath}");
                    
                    // 检查下载的文件
                    if (File.Exists(downloadPath))
                    {
                        var fileInfo = new FileInfo(downloadPath);
                        Debug.Log($"下载文件大小: {fileInfo.Length} 字节");
                    }
                    
                    // ========== 上传操作 ==========
                    
                    // 上传字符串
                    string uploadData = "{\"upload\":\"test data\",\"timestamp\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"}";
                    byte[] uploadResponseBytes = client.UploadString("https://httpbin.org/post", uploadData);
                    string uploadResponse = Encoding.UTF8.GetString(uploadResponseBytes);
                    Debug.Log($"上传响应长度: {uploadResponse.Length}");
                    Debug.Log($"上传响应前100字符: {uploadResponse.Substring(0, Math.Min(100, uploadResponse.Length))}...");
                    
                    // 上传字节数组
                    byte[] uploadBytes = Encoding.UTF8.GetBytes("{\"upload\":\"byte array data\"}");
                    byte[] uploadBytesResponse = client.UploadData("https://httpbin.org/post", uploadBytes);
                    string uploadBytesResult = Encoding.UTF8.GetString(uploadBytesResponse);
                    Debug.Log($"字节数组上传响应长度: {uploadBytesResult.Length}");
                    
                    // 上传文件
                    string testFilePath = Path.Combine(Application.persistentDataPath, "test_upload.txt");
                    File.WriteAllText(testFilePath, "This is a test file for upload");
                    
                    byte[] uploadFileResponse = client.UploadFile("https://httpbin.org/post", testFilePath);
                    string uploadFileResult = Encoding.UTF8.GetString(uploadFileResponse);
                    Debug.Log($"文件上传响应长度: {uploadFileResult.Length}");
                    
                    // ========== 异步操作 ==========
                    
                    // 异步下载字符串
                    var downloadStringTask = client.DownloadStringTaskAsync(testUrl);
                    string asyncResponse = downloadStringTask.Result;
                    Debug.Log($"异步下载字符串长度: {asyncResponse.Length}");
                    
                    // 异步上传字符串
                    var uploadStringTask = client.UploadStringTaskAsync("https://httpbin.org/post", uploadData);
                    string asyncUploadResponse = uploadStringTask.Result;
                    Debug.Log($"异步上传响应长度: {asyncUploadResponse.Length}");
                    
                    // ========== 事件处理 ==========
                    
                    // 下载进度事件
                    client.DownloadProgressChanged += (sender, e) =>
                    {
                        Debug.Log($"下载进度: {e.ProgressPercentage}%, 已下载: {e.BytesReceived} 字节");
                    };
                    
                    client.DownloadDataCompleted += (sender, e) =>
                    {
                        if (e.Error == null)
                        {
                            Debug.Log($"下载完成，数据长度: {e.Result.Length}");
                        }
                        else
                        {
                            Debug.LogError($"下载出错: {e.Error.Message}");
                        }
                    };
                    
                    // 上传进度事件
                    client.UploadProgressChanged += (sender, e) =>
                    {
                        Debug.Log($"上传进度: {e.ProgressPercentage}%, 已上传: {e.BytesSent} 字节");
                    };
                    
                    client.UploadDataCompleted += (sender, e) =>
                    {
                        if (e.Error == null)
                        {
                            Debug.Log($"上传完成，响应长度: {e.Result.Length}");
                        }
                        else
                        {
                            Debug.LogError($"上传出错: {e.Error.Message}");
                        }
                    };
                    
                    // ========== 清理操作 ==========
                    
                    // 清理下载的文件
                    if (File.Exists(downloadPath))
                    {
                        File.Delete(downloadPath);
                        Debug.Log("已清理下载的文件");
                    }
                    
                    if (File.Exists(testFilePath))
                    {
                        File.Delete(testFilePath);
                        Debug.Log("已清理测试上传文件");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"WebClient操作出错: {ex.Message}");
            }
        }

        // ================= Socket 套接字 =================
        /// <summary>
        /// Socket 套接字示例
        /// 演示TCP和UDP套接字的基本操作
        /// </summary>
        private void SocketExample()
        {
            Debug.Log("--- Socket 套接字示例 ---");
            
            try
            {
                // ========== TCP Socket示例 ==========
                TcpSocketExample();
                
                // ========== UDP Socket示例 ==========
                UdpSocketExample();
                
                // ========== Socket选项示例 ==========
                SocketOptionsExample();
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Socket操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// TCP Socket示例
        /// </summary>
        private void TcpSocketExample()
        {
            Debug.Log("--- TCP Socket示例 ---");
            
            try
            {
                // 创建TCP客户端
                using (TcpClient client = new TcpClient())
                {
                    // 连接到服务器
                    client.Connect("httpbin.org", 80);
                    Debug.Log("TCP客户端已连接到服务器");
                    
                    // 获取网络流
                    using (NetworkStream stream = client.GetStream())
                    {
                        // 发送HTTP GET请求
                        string httpRequest = "GET /get HTTP/1.1\r\nHost: httpbin.org\r\nConnection: close\r\n\r\n";
                        byte[] requestBytes = Encoding.UTF8.GetBytes(httpRequest);
                        stream.Write(requestBytes, 0, requestBytes.Length);
                        Debug.Log("已发送HTTP请求");
                        
                        // 读取响应
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        var responseBuilder = new StringBuilder();
                        
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                        }
                        
                        string response = responseBuilder.ToString();
                        Debug.Log($"TCP响应长度: {response.Length}");
                        Debug.Log($"TCP响应前200字符: {response.Substring(0, Math.Min(200, response.Length))}...");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"TCP Socket操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// UDP Socket示例
        /// </summary>
        private void UdpSocketExample()
        {
            Debug.Log("--- UDP Socket示例 ---");
            
            try
            {
                // 创建UDP客户端
                using (UdpClient client = new UdpClient())
                {
                    // 连接到DNS服务器
                    client.Connect("8.8.8.8", 53);
                    Debug.Log("UDP客户端已连接到DNS服务器");
                    
                    // 发送DNS查询（简化的）
                    string query = "test query";
                    byte[] queryBytes = Encoding.UTF8.GetBytes(query);
                    client.Send(queryBytes, queryBytes.Length);
                    Debug.Log("已发送UDP数据");
                    
                    // 接收响应
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] responseBytes = client.Receive(ref remoteEndPoint);
                    string response = Encoding.UTF8.GetString(responseBytes);
                    Debug.Log($"UDP响应长度: {responseBytes.Length}");
                    Debug.Log($"UDP响应来源: {remoteEndPoint}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"UDP Socket操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// Socket选项示例
        /// </summary>
        private void SocketOptionsExample()
        {
            Debug.Log("--- Socket选项示例 ---");
            
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    // 设置Socket选项
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
                    
                    // 设置发送和接收超时
                    socket.SendTimeout = 5000;
                    socket.ReceiveTimeout = 5000;
                    
                    Debug.Log($"Socket发送超时: {socket.SendTimeout}ms");
                    Debug.Log($"Socket接收超时: {socket.ReceiveTimeout}ms");
                    Debug.Log("Socket选项设置完成");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Socket选项设置出错: {ex.Message}");
            }
        }

        // ================= WebSocket 网络套接字 =================
        /// <summary>
        /// WebSocket 网络套接字示例
        /// 演示WebSocket的客户端操作
        /// </summary>
        private async Task WebSocketExample()
        {
            Debug.Log("--- WebSocket 网络套接字示例 ---");
            
            try
            {
                using (ClientWebSocket webSocket = new ClientWebSocket())
                {
                    // 设置WebSocket选项
                    webSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(30);
                    webSocket.Options.SetRequestHeader("User-Agent", "Unity-WebSocket-Example");
                    
                    Debug.Log("正在连接到WebSocket服务器...");
                    
                    // 连接到WebSocket服务器（使用echo服务器进行测试）
                    await webSocket.ConnectAsync(new Uri("wss://echo.websocket.org"), CancellationToken.None);
                    Debug.Log("WebSocket连接已建立");
                    
                    // 发送消息
                    string message = "Hello WebSocket!";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    Debug.Log($"已发送消息: {message}");
                    
                    // 接收响应
                    byte[] buffer = new byte[1024];
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string response = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Debug.Log($"收到响应: {response}");
                    }
                    
                    // 关闭WebSocket连接
                    if (webSocket.State == WebSocketState.Open)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                        Debug.Log("WebSocket连接已关闭");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"WebSocket操作出错: {ex.Message}");
            }
        }

        // ================= 网络工具类 =================
        /// <summary>
        /// 网络工具类示例
        /// 演示Uri、IPAddress、Dns等网络工具类的使用
        /// </summary>
        private void NetworkUtilityExample()
        {
            Debug.Log("--- 网络工具类示例 ---");
            
            try
            {
                // ========== Uri示例 ==========
                UriExample();
                
                // ========== IPAddress示例 ==========
                IPAddressExample();
                
                // ========== DNS示例 ==========
                DnsExample();
                
                // ========== 网络信息示例 ==========
                NetworkInformationExample();
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"网络工具类操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// Uri示例
        /// </summary>
        private void UriExample()
        {
            Debug.Log("--- Uri示例 ---");
            
            try
            {
                // 创建Uri
                Uri uri = new Uri("https://httpbin.org/get?param1=value1&param2=value2#fragment");
                
                Debug.Log($"Uri方案: {uri.Scheme}");
                Debug.Log($"Uri主机: {uri.Host}");
                Debug.Log($"Uri端口: {uri.Port}");
                Debug.Log($"Uri路径: {uri.AbsolutePath}");
                Debug.Log($"Uri查询: {uri.Query}");
                Debug.Log($"Uri片段: {uri.Fragment}");
                Debug.Log($"Uri绝对Uri: {uri.AbsoluteUri}");
                Debug.Log($"Uri本地路径: {uri.LocalPath}");
                
                // Uri构建
                var uriBuilder = new UriBuilder();
                uriBuilder.Scheme = "https";
                uriBuilder.Host = "httpbin.org";
                uriBuilder.Path = "/post";
                uriBuilder.Query = "param1=value1&param2=value2";
                
                Uri builtUri = uriBuilder.Uri;
                Debug.Log($"构建的Uri: {builtUri}");
                
                // Uri比较
                Uri uri1 = new Uri("https://httpbin.org/get");
                Uri uri2 = new Uri("https://httpbin.org/get");
                bool areEqual = Uri.Equals(uri1, uri2);
                Debug.Log($"Uri比较结果: {areEqual}");
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Uri操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// IPAddress示例
        /// </summary>
        private void IPAddressExample()
        {
            Debug.Log("--- IPAddress示例 ---");
            
            try
            {
                // 解析IP地址
                IPAddress ip1 = IPAddress.Parse("192.168.1.1");
                IPAddress ip2 = IPAddress.Parse("::1"); // IPv6
                
                Debug.Log($"IPv4地址: {ip1}");
                Debug.Log($"IPv6地址: {ip2}");
                Debug.Log($"IPv4地址族: {ip1.AddressFamily}");
                Debug.Log($"IPv6地址族: {ip2.AddressFamily}");
                
                // 特殊IP地址
                Debug.Log($"回环地址: {IPAddress.Loopback}");
                Debug.Log($"任意地址: {IPAddress.Any}");
                Debug.Log($"广播地址: {IPAddress.Broadcast}");
                
                // IP地址验证
                bool isValidIPv4 = IPAddress.TryParse("192.168.1.1", out IPAddress validIP);
                bool isInvalidIPv4 = IPAddress.TryParse("256.256.256.256", out IPAddress invalidIP);
                
                Debug.Log($"有效IPv4地址: {isValidIPv4}");
                Debug.Log($"无效IPv4地址: {isInvalidIPv4}");
                
                // 字节数组转换
                byte[] ipBytes = ip1.GetAddressBytes();
                Debug.Log($"IP地址字节数组: {string.Join(", ", ipBytes)}");
                
                IPAddress fromBytes = new IPAddress(ipBytes);
                Debug.Log($"从字节数组创建的IP地址: {fromBytes}");
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"IPAddress操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// DNS示例
        /// </summary>
        private void DnsExample()
        {
            Debug.Log("--- DNS示例 ---");
            
            try
            {
                // 主机名解析
                IPHostEntry hostEntry = Dns.GetHostEntry("httpbin.org");
                Debug.Log($"主机名: {hostEntry.HostName}");
                
                Debug.Log("IP地址列表:");
                foreach (IPAddress address in hostEntry.AddressList)
                {
                    Debug.Log($"  {address}");
                }
                
                Debug.Log("别名列表:");
                foreach (string alias in hostEntry.Aliases)
                {
                    Debug.Log($"  {alias}");
                }
                
                // 异步主机名解析
                var hostEntryTask = Dns.GetHostEntryAsync("google.com");
                var asyncHostEntry = hostEntryTask.Result;
                Debug.Log($"异步解析主机名: {asyncHostEntry.HostName}");
                
                // 获取本地主机信息
                string localHostName = Dns.GetHostName();
                Debug.Log($"本地主机名: {localHostName}");
                
                IPHostEntry localHostEntry = Dns.GetHostEntry(localHostName);
                Debug.Log($"本地主机IP地址:");
                foreach (IPAddress address in localHostEntry.AddressList)
                {
                    Debug.Log($"  {address}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"DNS操作出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 网络信息示例
        /// </summary>
        private void NetworkInformationExample()
        {
            Debug.Log("--- 网络信息示例 ---");
            
            try
            {
                // 获取网络接口信息
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                Debug.Log($"网络接口数量: {interfaces.Length}");
                
                foreach (NetworkInterface ni in interfaces)
                {
                    Debug.Log($"接口名称: {ni.Name}");
                    Debug.Log($"接口描述: {ni.Description}");
                    Debug.Log($"接口类型: {ni.NetworkInterfaceType}");
                    Debug.Log($"操作状态: {ni.OperationalStatus}");
                    Debug.Log($"速度: {ni.Speed} bps");
                    Debug.Log($"MAC地址: {ni.GetPhysicalAddress()}");
                    Debug.Log("---");
                }
                
                // 获取活动网络接口
                NetworkInterface activeInterface = NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(ni => ni.OperationalStatus == OperationalStatus.Up && 
                                        ni.NetworkInterfaceType != NetworkInterfaceType.Loopback);
                
                if (activeInterface != null)
                {
                    Debug.Log($"活动网络接口: {activeInterface.Name}");
                    
                    // 获取IP地址信息
                    IPInterfaceProperties ipProps = activeInterface.GetIPProperties();
                    
                    Debug.Log("IP地址:");
                    foreach (UnicastIPAddressInformation ip in ipProps.UnicastAddresses)
                    {
                        Debug.Log($"  {ip.Address}");
                    }
                    
                    Debug.Log("网关地址:");
                    foreach (GatewayIPAddressInformation gateway in ipProps.GatewayAddresses)
                    {
                        Debug.Log($"  {gateway.Address}");
                    }
                    
                    Debug.Log("DNS服务器:");
                    foreach (IPAddress dns in ipProps.DnsAddresses)
                    {
                        Debug.Log($"  {dns}");
                    }
                }
                
                // Ping测试
                try
                {
                    using (Ping ping = new Ping())
                    {
                        PingReply reply = ping.Send("8.8.8.8", 1000);
                        Debug.Log($"Ping 8.8.8.8 结果: {reply.Status}");
                        if (reply.Status == IPStatus.Success)
                        {
                            Debug.Log($"Ping响应时间: {reply.RoundtripTime}ms");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Ping测试出错: {ex.Message}");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"网络信息操作出错: {ex.Message}");
            }
        }

        // ================= 高级网络操作 =================
        /// <summary>
        /// 高级网络操作示例
        /// 包括网络监控、代理设置、SSL/TLS等高级功能
        /// </summary>
        private void AdvancedNetworkingExample()
        {
            Debug.Log("--- 高级网络操作示例 ---");
            
            try
            {
                // ========== 代理设置 ==========
                
                // 设置代理（示例）
                Debug.Log("代理设置示例（仅演示，实际需要有效代理）:");
                Debug.Log("HttpClient支持通过HttpClientHandler设置代理");
                Debug.Log("WebClient支持通过WebProxy设置代理");
                
                // ========== SSL/TLS设置 ==========
                
                // 设置SSL/TLS协议版本
                Debug.Log("SSL/TLS设置示例:");
                Debug.Log("可以通过ServicePointManager设置默认的SSL/TLS协议版本");
                
                // 设置证书验证回调
                Debug.Log("证书验证回调示例:");
                Debug.Log("可以通过ServicePointManager.ServerCertificateValidationCallback设置自定义证书验证");
                
                // ========== 网络监控 ==========
                
                // 网络状态监控
                Debug.Log("网络状态监控示例:");
                Debug.Log("可以通过NetworkChange.NetworkAvailabilityChanged事件监控网络可用性变化");
                
                // 网络接口状态监控
                Debug.Log("网络接口状态监控示例:");
                Debug.Log("可以通过NetworkChange.NetworkAddressChanged事件监控网络地址变化");
                
                // ========== 网络诊断 ==========
                
                // 网络连接测试
                Debug.Log("网络连接测试:");
                Debug.Log("可以使用Ping、TcpClient、HttpClient等工具进行网络连接测试");
                
                // 端口扫描（示例）
                Debug.Log("端口扫描示例（仅演示）:");
                Debug.Log("可以使用TcpClient尝试连接特定端口来检查端口是否开放");
                
                // ========== 网络配置 ==========
                
                // 设置默认连接限制
                Debug.Log("连接限制设置:");
                ServicePointManager.DefaultConnectionLimit = 10;
                Debug.Log($"默认连接限制: {ServicePointManager.DefaultConnectionLimit}");
                
                // 设置请求超时
                ServicePointManager.MaxServicePointIdleTime = 30000; // 30秒
                Debug.Log($"最大服务点空闲时间: {ServicePointManager.MaxServicePointIdleTime}ms");
                
                // ========== 网络统计 ==========
                
                // 获取网络统计信息
                Debug.Log("网络统计信息:");
                var tcpStats = IPGlobalProperties.GetIPGlobalProperties().GetTcpIPv4Statistics();
                Debug.Log($"TCP连接数: {tcpStats.CurrentConnections}");
                Debug.Log($"TCP监听端口数: {tcpStats.CurrentListening}");
                
                var udpStats = IPGlobalProperties.GetIPGlobalProperties().GetUdpIPv4Statistics();
                Debug.Log($"UDP监听端口数: {udpStats.CurrentListening}");
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"高级网络操作出错: {ex.Message}");
            }
        }
    }
} 