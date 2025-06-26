using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// UnityEngine.DedicatedServer 命名空间案例演示
/// 展示专用服务器配置、网络管理、服务器状态监控等核心功能
/// </summary>
public class DedicatedServerExample : MonoBehaviour
{
    [Header("服务器配置")]
    [SerializeField] private string serverIP = "127.0.0.1"; //服务器IP
    [SerializeField] private int serverPort = 7777; //服务器端口
    [SerializeField] private int maxConnections = 32; //最大连接数
    [SerializeField] private bool useWebSockets = false; //使用WebSocket
    [SerializeField] private bool useSecureConnection = false; //使用安全连接
    
    [Header("服务器状态")]
    [SerializeField] private bool isServerRunning = false; //服务器是否运行
    [SerializeField] private int currentConnections = 0; //当前连接数
    [SerializeField] private float serverUptime = 0f; //服务器运行时间
    [SerializeField] private float serverLoad = 0f; //服务器负载
    [SerializeField] private string serverStatus = "未启动"; //服务器状态
    
    [Header("网络配置")]
    [SerializeField] private int networkUpdateRate = 30; //网络更新频率
    [SerializeField] private int networkBufferSize = 1024; //网络缓冲区大小
    [SerializeField] private bool enableNetworkLogging = false; //启用网络日志
    [SerializeField] private float networkTimeout = 30f; //网络超时时间
    
    [Header("性能监控")]
    [SerializeField] private bool enablePerformanceMonitoring = true; //启用性能监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private float cpuUsage = 0f; //CPU使用率
    [SerializeField] private float memoryUsage = 0f; //内存使用率
    [SerializeField] private float networkLatency = 0f; //网络延迟
    
    private NetworkManager networkManager;
    private float lastMonitoringTime = 0f;
    private float serverStartTime = 0f;
    private bool isInitialized = false;

    private void Start()
    {
        InitializeDedicatedServer();
    }

    /// <summary>
    /// 初始化专用服务器
    /// </summary>
    private void InitializeDedicatedServer()
    {
        // 获取NetworkManager组件
        networkManager = FindObjectOfType<NetworkManager>();
        if (networkManager == null)
        {
            networkManager = gameObject.AddComponent<NetworkManager>();
        }
        
        // 配置网络管理器
        ConfigureNetworkManager();
        
        // 初始化网络设置
        InitializeNetworkSettings();
        
        isInitialized = true;
        Debug.Log("专用服务器初始化完成");
    }

    /// <summary>
    /// 配置网络管理器
    /// </summary>
    private void ConfigureNetworkManager()
    {
        if (networkManager != null)
        {
            networkManager.networkAddress = serverIP;
            networkManager.networkPort = serverPort;
            networkManager.maxConnections = maxConnections;
            
            Debug.Log($"网络管理器配置: {serverIP}:{serverPort}, 最大连接: {maxConnections}");
        }
    }

    /// <summary>
    /// 初始化网络设置
    /// </summary>
    private void InitializeNetworkSettings()
    {
        // 设置网络更新频率
        Network.sendRate = networkUpdateRate;
        Network.updateRate = networkUpdateRate;
        
        // 设置网络缓冲区
        Network.bufferSize = networkBufferSize;
        
        // 设置网络超时
        Network.timeout = networkTimeout;
        
        Debug.Log($"网络设置: 更新频率={networkUpdateRate}Hz, 缓冲区={networkBufferSize}字节");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新服务器运行时间
        if (isServerRunning)
        {
            serverUptime = Time.time - serverStartTime;
        }
        
        // 性能监控
        if (enablePerformanceMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            MonitorServerPerformance();
            lastMonitoringTime = Time.time;
        }
        
        // 更新服务器状态
        UpdateServerStatus();
    }

    /// <summary>
    /// 启动专用服务器
    /// </summary>
    public void StartDedicatedServer()
    {
        if (isServerRunning)
        {
            Debug.LogWarning("服务器已在运行中");
            return;
        }
        
        try
        {
            // 启动服务器
            bool success = NetworkServer.Listen(serverPort);
            
            if (success)
            {
                isServerRunning = true;
                serverStartTime = Time.time;
                serverStatus = "运行中";
                
                // 注册网络消息处理
                RegisterNetworkMessageHandlers();
                
                Debug.Log($"专用服务器启动成功: {serverIP}:{serverPort}");
            }
            else
            {
                Debug.LogError("服务器启动失败");
                serverStatus = "启动失败";
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"启动服务器时发生错误: {e.Message}");
            serverStatus = "启动错误";
        }
    }

    /// <summary>
    /// 停止专用服务器
    /// </summary>
    public void StopDedicatedServer()
    {
        if (!isServerRunning)
        {
            Debug.LogWarning("服务器未运行");
            return;
        }
        
        try
        {
            // 断开所有连接
            NetworkServer.DisconnectAll();
            
            // 停止服务器
            NetworkServer.Shutdown();
            
            isServerRunning = false;
            serverStatus = "已停止";
            currentConnections = 0;
            
            Debug.Log("专用服务器已停止");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"停止服务器时发生错误: {e.Message}");
        }
    }

    /// <summary>
    /// 注册网络消息处理器
    /// </summary>
    private void RegisterNetworkMessageHandlers()
    {
        // 注册连接事件
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);
        
        // 注册自定义消息
        NetworkServer.RegisterHandler(1001, OnCustomMessage);
        
        Debug.Log("网络消息处理器注册完成");
    }

    /// <summary>
    /// 客户端连接事件
    /// </summary>
    private void OnClientConnected(NetworkMessage netMsg)
    {
        currentConnections++;
        Debug.Log($"客户端连接: {netMsg.conn.address}, 当前连接数: {currentConnections}");
        
        // 发送欢迎消息
        SendWelcomeMessage(netMsg.conn);
    }

    /// <summary>
    /// 客户端断开连接事件
    /// </summary>
    private void OnClientDisconnected(NetworkMessage netMsg)
    {
        currentConnections = Mathf.Max(0, currentConnections - 1);
        Debug.Log($"客户端断开: {netMsg.conn.address}, 当前连接数: {currentConnections}");
    }

    /// <summary>
    /// 自定义消息处理
    /// </summary>
    private void OnCustomMessage(NetworkMessage netMsg)
    {
        var reader = netMsg.reader;
        string message = reader.ReadString();
        
        Debug.Log($"收到自定义消息: {message}");
        
        // 处理消息并回复
        ProcessCustomMessage(netMsg.conn, message);
    }

    /// <summary>
    /// 发送欢迎消息
    /// </summary>
    private void SendWelcomeMessage(NetworkConnection conn)
    {
        var writer = new NetworkWriter();
        writer.StartMessage(1002); // 欢迎消息ID
        writer.Write("欢迎连接到专用服务器!");
        writer.Write(serverUptime);
        writer.Write(currentConnections);
        writer.FinishMessage();
        
        conn.SendWriter(writer, 0);
    }

    /// <summary>
    /// 处理自定义消息
    /// </summary>
    private void ProcessCustomMessage(NetworkConnection conn, string message)
    {
        var response = $"服务器收到消息: {message}";
        
        var writer = new NetworkWriter();
        writer.StartMessage(1003); // 响应消息ID
        writer.Write(response);
        writer.Write(Time.time);
        writer.FinishMessage();
        
        conn.SendWriter(writer, 0);
    }

    /// <summary>
    /// 监控服务器性能
    /// </summary>
    private void MonitorServerPerformance()
    {
        // 模拟性能数据
        cpuUsage = Random.Range(10f, 80f);
        memoryUsage = Random.Range(20f, 90f);
        networkLatency = Random.Range(5f, 100f);
        
        // 计算服务器负载
        serverLoad = (cpuUsage + memoryUsage) / 2f;
        
        if (enableNetworkLogging)
        {
            Debug.Log($"性能监控: CPU={cpuUsage:F1}%, 内存={memoryUsage:F1}%, 延迟={networkLatency:F1}ms");
        }
    }

    /// <summary>
    /// 更新服务器状态
    /// </summary>
    private void UpdateServerStatus()
    {
        if (isServerRunning)
        {
            if (NetworkServer.active)
            {
                serverStatus = "运行中";
            }
            else
            {
                serverStatus = "异常";
            }
        }
        else
        {
            serverStatus = "已停止";
        }
    }

    /// <summary>
    /// 获取服务器信息
    /// </summary>
    public void GetServerInfo()
    {
        Debug.Log("=== 服务器信息 ===");
        Debug.Log($"服务器状态: {serverStatus}");
        Debug.Log($"服务器地址: {serverIP}:{serverPort}");
        Debug.Log($"当前连接数: {currentConnections}/{maxConnections}");
        Debug.Log($"运行时间: {serverUptime:F1}秒");
        Debug.Log($"服务器负载: {serverLoad:F1}%");
        Debug.Log($"CPU使用率: {cpuUsage:F1}%");
        Debug.Log($"内存使用率: {memoryUsage:F1}%");
        Debug.Log($"网络延迟: {networkLatency:F1}ms");
    }

    /// <summary>
    /// 获取网络统计信息
    /// </summary>
    public void GetNetworkStats()
    {
        Debug.Log("=== 网络统计信息 ===");
        Debug.Log($"网络更新频率: {Network.sendRate}Hz");
        Debug.Log($"网络缓冲区大小: {Network.bufferSize}字节");
        Debug.Log($"网络超时时间: {Network.timeout}秒");
        Debug.Log($"使用WebSocket: {useWebSockets}");
        Debug.Log($"使用安全连接: {useSecureConnection}");
        Debug.Log($"网络日志: {(enableNetworkLogging ? "启用" : "禁用")}");
    }

    /// <summary>
    /// 广播消息到所有客户端
    /// </summary>
    /// <param name="message">消息内容</param>
    public void BroadcastMessage(string message)
    {
        if (!isServerRunning)
        {
            Debug.LogWarning("服务器未运行，无法广播消息");
            return;
        }
        
        var writer = new NetworkWriter();
        writer.StartMessage(1004); // 广播消息ID
        writer.Write(message);
        writer.Write(Time.time);
        writer.FinishMessage();
        
        NetworkServer.SendToAll(1004, writer);
        Debug.Log($"广播消息: {message}");
    }

    /// <summary>
    /// 踢出客户端
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    public void KickClient(int connectionId)
    {
        if (!isServerRunning)
        {
            Debug.LogWarning("服务器未运行");
            return;
        }
        
        var conn = NetworkServer.connections[connectionId];
        if (conn != null)
        {
            conn.Disconnect();
            Debug.Log($"踢出客户端: {connectionId}");
        }
        else
        {
            Debug.LogWarning($"未找到连接ID: {connectionId}");
        }
    }

    /// <summary>
    /// 设置服务器配置
    /// </summary>
    /// <param name="ip">服务器IP</param>
    /// <param name="port">服务器端口</param>
    /// <param name="maxConn">最大连接数</param>
    public void SetServerConfig(string ip, int port, int maxConn)
    {
        serverIP = ip;
        serverPort = port;
        maxConnections = maxConn;
        
        if (networkManager != null)
        {
            ConfigureNetworkManager();
        }
        
        Debug.Log($"服务器配置已更新: {ip}:{port}, 最大连接: {maxConn}");
    }

    /// <summary>
    /// 设置网络配置
    /// </summary>
    /// <param name="updateRate">更新频率</param>
    /// <param name="bufferSize">缓冲区大小</param>
    /// <param name="timeout">超时时间</param>
    public void SetNetworkConfig(int updateRate, int bufferSize, float timeout)
    {
        networkUpdateRate = updateRate;
        networkBufferSize = bufferSize;
        networkTimeout = timeout;
        
        InitializeNetworkSettings();
        Debug.Log($"网络配置已更新: 频率={updateRate}Hz, 缓冲区={bufferSize}字节, 超时={timeout}秒");
    }

    /// <summary>
    /// 重启服务器
    /// </summary>
    public void RestartServer()
    {
        Debug.Log("重启服务器...");
        StopDedicatedServer();
        
        // 等待一秒后重启
        Invoke(nameof(StartDedicatedServer), 1f);
    }

    /// <summary>
    /// 保存服务器配置
    /// </summary>
    public void SaveServerConfig()
    {
        var config = new ServerConfig
        {
            serverIP = serverIP,
            serverPort = serverPort,
            maxConnections = maxConnections,
            networkUpdateRate = networkUpdateRate,
            networkBufferSize = networkBufferSize,
            networkTimeout = networkTimeout,
            enableNetworkLogging = enableNetworkLogging
        };
        
        string json = JsonUtility.ToJson(config, true);
        System.IO.File.WriteAllText("server_config.json", json);
        Debug.Log("服务器配置已保存");
    }

    /// <summary>
    /// 加载服务器配置
    /// </summary>
    public void LoadServerConfig()
    {
        if (System.IO.File.Exists("server_config.json"))
        {
            string json = System.IO.File.ReadAllText("server_config.json");
            var config = JsonUtility.FromJson<ServerConfig>(json);
            
            SetServerConfig(config.serverIP, config.serverPort, config.maxConnections);
            SetNetworkConfig(config.networkUpdateRate, config.networkBufferSize, config.networkTimeout);
            enableNetworkLogging = config.enableNetworkLogging;
            
            Debug.Log("服务器配置已加载");
        }
        else
        {
            Debug.LogWarning("配置文件不存在");
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("DedicatedServer 专用服务器演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("服务器配置:");
        serverIP = GUILayout.TextField("服务器IP", serverIP);
        serverPort = int.TryParse(GUILayout.TextField("服务器端口", serverPort.ToString()), out var port) ? port : serverPort;
        maxConnections = int.TryParse(GUILayout.TextField("最大连接数", maxConnections.ToString()), out var maxConn) ? maxConn : maxConnections;
        useWebSockets = GUILayout.Toggle(useWebSockets, "使用WebSocket");
        useSecureConnection = GUILayout.Toggle(useSecureConnection, "使用安全连接");
        
        GUILayout.Space(10);
        GUILayout.Label("网络配置:");
        networkUpdateRate = int.TryParse(GUILayout.TextField("网络更新频率", networkUpdateRate.ToString()), out var updateRate) ? updateRate : networkUpdateRate;
        networkBufferSize = int.TryParse(GUILayout.TextField("网络缓冲区大小", networkBufferSize.ToString()), out var bufferSize) ? bufferSize : networkBufferSize;
        networkTimeout = float.TryParse(GUILayout.TextField("网络超时时间", networkTimeout.ToString()), out var timeout) ? timeout : networkTimeout;
        enableNetworkLogging = GUILayout.Toggle(enableNetworkLogging, "启用网络日志");
        
        GUILayout.Space(10);
        GUILayout.Label("服务器状态:");
        GUILayout.Label($"状态: {serverStatus}");
        GUILayout.Label($"连接数: {currentConnections}/{maxConnections}");
        GUILayout.Label($"运行时间: {serverUptime:F1}秒");
        GUILayout.Label($"服务器负载: {serverLoad:F1}%");
        GUILayout.Label($"CPU使用率: {cpuUsage:F1}%");
        GUILayout.Label($"内存使用率: {memoryUsage:F1}%");
        GUILayout.Label($"网络延迟: {networkLatency:F1}ms");
        
        GUILayout.Space(10);
        GUILayout.Label("性能监控:");
        enablePerformanceMonitoring = GUILayout.Toggle(enablePerformanceMonitoring, "启用性能监控");
        monitoringInterval = float.TryParse(GUILayout.TextField("监控间隔", monitoringInterval.ToString()), out var interval) ? interval : monitoringInterval;
        
        GUILayout.Space(10);
        
        if (!isServerRunning)
        {
            if (GUILayout.Button("启动服务器"))
            {
                StartDedicatedServer();
            }
        }
        else
        {
            if (GUILayout.Button("停止服务器"))
            {
                StopDedicatedServer();
            }
            
            if (GUILayout.Button("重启服务器"))
            {
                RestartServer();
            }
        }
        
        if (GUILayout.Button("获取服务器信息"))
        {
            GetServerInfo();
        }
        
        if (GUILayout.Button("获取网络统计"))
        {
            GetNetworkStats();
        }
        
        if (GUILayout.Button("广播消息"))
        {
            BroadcastMessage("服务器广播消息: " + Time.time);
        }
        
        if (GUILayout.Button("保存配置"))
        {
            SaveServerConfig();
        }
        
        if (GUILayout.Button("加载配置"))
        {
            LoadServerConfig();
        }
        
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        if (isServerRunning)
        {
            StopDedicatedServer();
        }
    }
}

/// <summary>
/// 服务器配置类
/// </summary>
[System.Serializable]
public class ServerConfig
{
    public string serverIP;
    public int serverPort;
    public int maxConnections;
    public int networkUpdateRate;
    public int networkBufferSize;
    public float networkTimeout;
    public bool enableNetworkLogging;
} 