using UnityEngine;

/// <summary>
/// UnityEngine.Device 命名空间案例演示
/// 展示设备信息获取、设备特性检测、设备状态监控等核心功能
/// </summary>
public class DeviceExample : MonoBehaviour
{
    [Header("设备基本信息")]
    [SerializeField] private string deviceModel = ""; //设备型号
    [SerializeField] private string deviceName = ""; //设备名称
    [SerializeField] private string deviceType = ""; //设备类型
    [SerializeField] private string operatingSystem = ""; //操作系统
    [SerializeField] private string processorType = ""; //处理器类型
    [SerializeField] private int processorCount = 0; //处理器核心数

    [Header("设备性能")]
    [SerializeField] private int systemMemorySize = 0; //系统内存大小
    [SerializeField] private int graphicsMemorySize = 0; //图形内存大小
    [SerializeField] private string graphicsDeviceName = ""; //图形设备名称
    [SerializeField] private string graphicsDeviceVersion = ""; //图形设备版本
    [SerializeField] private bool supportsComputeShaders = false; //支持计算着色器
    [SerializeField] private bool supportsInstancing = false; //支持实例化

    [Header("设备特性")]
    [SerializeField] private bool supportsGyroscope = false; //支持陀螺仪
    [SerializeField] private bool supportsAccelerometer = false; //支持加速度计
    [SerializeField] private bool supportsLocationService = false; //支持位置服务
    [SerializeField] private bool supportsVibration = false; //支持震动
    [SerializeField] private bool supportsAudio = false; //支持音频
    [SerializeField] private bool supportsCamera = false; //支持相机

    [Header("设备状态")]
    [SerializeField] private float batteryLevel = 0f; //电池电量
    [SerializeField] private bool isCharging = false; //是否充电
    [SerializeField] private string networkReachability = ""; //网络可达性
    [SerializeField] private bool isInternetReachable = false; //网络是否可达
    [SerializeField] private string deviceUniqueIdentifier = ""; //设备唯一标识符

    [Header("设备监控")]
    [SerializeField] private bool enableDeviceMonitoring = true; //启用设备监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logDeviceChanges = false; //记录设备变化

    private float lastMonitoringTime = 0f;
    private string lastDeviceState = "";

    private void Start()
    {
        InitializeDeviceSystem();
    }

    /// <summary>
    /// 初始化设备系统
    /// </summary>
    private void InitializeDeviceSystem()
    {
        // 获取设备基本信息
        GetDeviceBasicInfo();
        
        // 获取设备性能信息
        GetDevicePerformanceInfo();
        
        // 检测设备特性
        DetectDeviceFeatures();
        
        // 获取设备状态
        GetDeviceStatus();
        
        Debug.Log("设备系统初始化完成");
    }

    /// <summary>
    /// 获取设备基本信息
    /// </summary>
    private void GetDeviceBasicInfo()
    {
        deviceModel = SystemInfo.deviceModel;
        deviceName = SystemInfo.deviceName;
        deviceType = SystemInfo.deviceType.ToString();
        operatingSystem = SystemInfo.operatingSystem;
        processorType = SystemInfo.processorType;
        processorCount = SystemInfo.processorCount;
        
        Debug.Log($"设备信息: {deviceModel} ({deviceName})");
        Debug.Log($"操作系统: {operatingSystem}");
        Debug.Log($"处理器: {processorType} ({processorCount}核)");
    }

    /// <summary>
    /// 获取设备性能信息
    /// </summary>
    private void GetDevicePerformanceInfo()
    {
        systemMemorySize = SystemInfo.systemMemorySize;
        graphicsMemorySize = SystemInfo.graphicsMemorySize;
        graphicsDeviceName = SystemInfo.graphicsDeviceName;
        graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;
        supportsComputeShaders = SystemInfo.supportsComputeShaders;
        supportsInstancing = SystemInfo.supportsInstancing;
        
        Debug.Log($"内存: {systemMemorySize}MB");
        Debug.Log($"图形设备: {graphicsDeviceName}");
        Debug.Log($"图形内存: {graphicsMemorySize}MB");
    }

    /// <summary>
    /// 检测设备特性
    /// </summary>
    private void DetectDeviceFeatures()
    {
        supportsGyroscope = SystemInfo.supportsGyroscope;
        supportsAccelerometer = SystemInfo.supportsAccelerometer;
        supportsLocationService = Input.location.isEnabledByUser;
        supportsVibration = SystemInfo.supportsVibration;
        supportsAudio = SystemInfo.supportsAudio;
        supportsCamera = SystemInfo.supportsCamera;
        
        Debug.Log($"陀螺仪: {supportsGyroscope}");
        Debug.Log($"加速度计: {supportsAccelerometer}");
        Debug.Log($"位置服务: {supportsLocationService}");
        Debug.Log($"震动: {supportsVibration}");
    }

    /// <summary>
    /// 获取设备状态
    /// </summary>
    private void GetDeviceStatus()
    {
        deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        networkReachability = Application.internetReachability.ToString();
        isInternetReachable = Application.internetReachability != NetworkReachability.NotReachable;
        
        // 获取电池信息（仅在移动平台可用）
        if (Application.isMobilePlatform)
        {
            batteryLevel = SystemInfo.batteryLevel;
            isCharging = SystemInfo.batteryStatus == BatteryStatus.Charging;
        }
        
        Debug.Log($"设备ID: {deviceUniqueIdentifier}");
        Debug.Log($"网络状态: {networkReachability}");
        Debug.Log($"电池电量: {batteryLevel:P0}");
    }

    private void Update()
    {
        if (enableDeviceMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            MonitorDeviceStatus();
            lastMonitoringTime = Time.time;
        }
    }

    /// <summary>
    /// 监控设备状态
    /// </summary>
    private void MonitorDeviceStatus()
    {
        string currentState = GetCurrentDeviceState();
        
        if (logDeviceChanges && currentState != lastDeviceState)
        {
            Debug.Log($"设备状态变化: {currentState}");
            lastDeviceState = currentState;
        }
        
        // 更新状态信息
        GetDeviceStatus();
    }

    /// <summary>
    /// 获取当前设备状态
    /// </summary>
    /// <returns>设备状态字符串</returns>
    private string GetCurrentDeviceState()
    {
        return $"Battery:{batteryLevel:P0}, Network:{networkReachability}, Charging:{isCharging}";
    }

    /// <summary>
    /// 获取设备详细信息
    /// </summary>
    public void GetDeviceDetailedInfo()
    {
        Debug.Log("=== 设备详细信息 ===");
        Debug.Log($"设备型号: {deviceModel}");
        Debug.Log($"设备名称: {deviceName}");
        Debug.Log($"设备类型: {deviceType}");
        Debug.Log($"操作系统: {operatingSystem}");
        Debug.Log($"处理器: {processorType}");
        Debug.Log($"处理器核心数: {processorCount}");
        Debug.Log($"系统内存: {systemMemorySize}MB");
        Debug.Log($"图形设备: {graphicsDeviceName}");
        Debug.Log($"图形设备版本: {graphicsDeviceVersion}");
        Debug.Log($"图形内存: {graphicsMemorySize}MB");
        Debug.Log($"支持计算着色器: {supportsComputeShaders}");
        Debug.Log($"支持实例化: {supportsInstancing}");
        Debug.Log($"设备唯一ID: {deviceUniqueIdentifier}");
    }

    /// <summary>
    /// 获取设备特性信息
    /// </summary>
    public void GetDeviceFeaturesInfo()
    {
        Debug.Log("=== 设备特性信息 ===");
        Debug.Log($"陀螺仪支持: {supportsGyroscope}");
        Debug.Log($"加速度计支持: {supportsAccelerometer}");
        Debug.Log($"位置服务支持: {supportsLocationService}");
        Debug.Log($"震动支持: {supportsVibration}");
        Debug.Log($"音频支持: {supportsAudio}");
        Debug.Log($"相机支持: {supportsCamera}");
        Debug.Log($"网络可达性: {networkReachability}");
        Debug.Log($"网络可达: {isInternetReachable}");
    }

    /// <summary>
    /// 测试设备功能
    /// </summary>
    public void TestDeviceFeatures()
    {
        Debug.Log("开始测试设备功能");
        
        // 测试震动
        if (supportsVibration)
        {
            Handheld.Vibrate();
            Debug.Log("震动测试完成");
        }
        
        // 测试位置服务
        if (supportsLocationService)
        {
            StartLocationService();
        }
        
        // 测试网络连接
        TestNetworkConnection();
        
        Debug.Log("设备功能测试完成");
    }

    /// <summary>
    /// 启动位置服务
    /// </summary>
    public void StartLocationService()
    {
        if (supportsLocationService)
        {
            StartCoroutine(StartLocationServiceCoroutine());
        }
    }

    /// <summary>
    /// 启动位置服务协程
    /// </summary>
    private System.Collections.IEnumerator StartLocationServiceCoroutine()
    {
        Input.location.Start();
        
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        
        if (maxWait < 1)
        {
            Debug.Log("位置服务初始化超时");
            yield break;
        }
        
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("位置服务启动失败");
            yield break;
        }
        
        Debug.Log($"位置服务启动成功: {Input.location.lastData.latitude}, {Input.location.lastData.longitude}");
    }

    /// <summary>
    /// 测试网络连接
    /// </summary>
    public void TestNetworkConnection()
    {
        Debug.Log($"网络可达性: {Application.internetReachability}");
        
        switch (Application.internetReachability)
        {
            case NetworkReachability.NotReachable:
                Debug.Log("网络不可达");
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                Debug.Log("通过移动网络可达");
                break;
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                Debug.Log("通过局域网可达");
                break;
        }
    }

    /// <summary>
    /// 获取设备性能报告
    /// </summary>
    public void GetDevicePerformanceReport()
    {
        Debug.Log("=== 设备性能报告 ===");
        Debug.Log($"帧率: {1f / Time.deltaTime:F1} FPS");
        Debug.Log($"内存使用: {SystemInfo.systemMemorySize - SystemInfo.systemMemorySize}MB");
        Debug.Log($"图形内存使用: {SystemInfo.graphicsMemorySize}MB");
        Debug.Log($"纹理质量: {QualitySettings.masterTextureLimit}");
        Debug.Log($"抗锯齿: {QualitySettings.antiAliasing}");
        Debug.Log($"阴影质量: {QualitySettings.shadowResolution}");
    }

    /// <summary>
    /// 设置设备监控
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetDeviceMonitoring(bool enabled)
    {
        enableDeviceMonitoring = enabled;
        Debug.Log($"设备监控: {(enabled ? "启用" : "禁用")}");
    }

    /// <summary>
    /// 设置监控间隔
    /// </summary>
    /// <param name="interval">监控间隔（秒）</param>
    public void SetMonitoringInterval(float interval)
    {
        monitoringInterval = Mathf.Max(0.1f, interval);
        Debug.Log($"监控间隔设置为: {monitoringInterval}秒");
    }

    /// <summary>
    /// 重置设备设置
    /// </summary>
    public void ResetDeviceSettings()
    {
        enableDeviceMonitoring = true;
        monitoringInterval = 1f;
        logDeviceChanges = false;
        
        Debug.Log("设备设置已重置");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 700));
        GUILayout.Label("Device 设备信息演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("设备信息:");
        GUILayout.Label($"设备: {deviceModel}");
        GUILayout.Label($"名称: {deviceName}");
        GUILayout.Label($"类型: {deviceType}");
        GUILayout.Label($"系统: {operatingSystem}");
        GUILayout.Label($"处理器: {processorType}");
        GUILayout.Label($"核心数: {processorCount}");
        
        GUILayout.Space(10);
        GUILayout.Label("性能信息:");
        GUILayout.Label($"系统内存: {systemMemorySize}MB");
        GUILayout.Label($"图形内存: {graphicsMemorySize}MB");
        GUILayout.Label($"图形设备: {graphicsDeviceName}");
        GUILayout.Label($"计算着色器: {(supportsComputeShaders ? "支持" : "不支持")}");
        
        GUILayout.Space(10);
        GUILayout.Label("设备特性:");
        GUILayout.Label($"陀螺仪: {(supportsGyroscope ? "支持" : "不支持")}");
        GUILayout.Label($"加速度计: {(supportsAccelerometer ? "支持" : "不支持")}");
        GUILayout.Label($"位置服务: {(supportsLocationService ? "支持" : "不支持")}");
        GUILayout.Label($"震动: {(supportsVibration ? "支持" : "不支持")}");
        
        GUILayout.Space(10);
        GUILayout.Label("设备状态:");
        GUILayout.Label($"电池电量: {batteryLevel:P0}");
        GUILayout.Label($"充电状态: {(isCharging ? "充电中" : "未充电")}");
        GUILayout.Label($"网络状态: {networkReachability}");
        GUILayout.Label($"设备ID: {deviceUniqueIdentifier}");
        
        GUILayout.Space(10);
        GUILayout.Label("监控设置:");
        
        enableDeviceMonitoring = GUILayout.Toggle(enableDeviceMonitoring, "启用设备监控");
        logDeviceChanges = GUILayout.Toggle(logDeviceChanges, "记录设备变化");
        monitoringInterval = float.TryParse(GUILayout.TextField("监控间隔", monitoringInterval.ToString()), out var interval) ? interval : monitoringInterval;
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取详细信息"))
        {
            GetDeviceDetailedInfo();
        }
        
        if (GUILayout.Button("获取特性信息"))
        {
            GetDeviceFeaturesInfo();
        }
        
        if (GUILayout.Button("获取性能报告"))
        {
            GetDevicePerformanceReport();
        }
        
        if (GUILayout.Button("测试设备功能"))
        {
            TestDeviceFeatures();
        }
        
        if (GUILayout.Button("启动位置服务"))
        {
            StartLocationService();
        }
        
        if (GUILayout.Button("测试网络连接"))
        {
            TestNetworkConnection();
        }
        
        if (GUILayout.Button("重置设备设置"))
        {
            ResetDeviceSettings();
        }
        
        GUILayout.EndArea();
    }
} 