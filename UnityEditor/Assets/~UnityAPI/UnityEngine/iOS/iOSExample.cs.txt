using UnityEngine;

/// <summary>
/// UnityEngine.iOS 命名空间案例演示
/// 展示iOS平台特定功能、设备特性检测、iOS API调用等核心功能
/// </summary>
public class iOSExample : MonoBehaviour
{
    [Header("iOS设备信息")]
    [SerializeField] private string deviceModel = ""; //设备型号
    [SerializeField] private string deviceName = ""; //设备名称
    [SerializeField] private string systemVersion = ""; //系统版本
    [SerializeField] private string deviceIdentifier = ""; //设备标识符
    [SerializeField] private bool isIPad = false; //是否为iPad
    [SerializeField] private bool isIPhone = false; //是否为iPhone
    [SerializeField] private bool isIPod = false; //是否为iPod
    
    [Header("iOS功能检测")]
    [SerializeField] private bool supportsGyroscope = false; //支持陀螺仪
    [SerializeField] private bool supportsAccelerometer = false; //支持加速度计
    [SerializeField] private bool supportsLocationService = false; //支持位置服务
    [SerializeField] private bool supportsCamera = false; //支持相机
    [SerializeField] private bool supportsMicrophone = false; //支持麦克风
    [SerializeField] private bool supportsVibration = false; //支持震动
    [SerializeField] private bool supportsMultitouch = false; //支持多点触控
    [SerializeField] private bool supportsMetal = false; //支持Metal
    
    [Header("iOS设置")]
    [SerializeField] private bool enableIdleTimer = true; //启用空闲计时器
    [SerializeField] private bool enableMultitouch = true; //启用多点触控
    [SerializeField] private bool enableGyroscope = true; //启用陀螺仪
    [SerializeField] private bool enableAccelerometer = true; //启用加速度计
    [SerializeField] private bool enableLocationService = false; //启用位置服务
    [SerializeField] private bool enableCamera = false; //启用相机
    [SerializeField] private bool enableMicrophone = false; //启用麦克风
    
    [Header("iOS状态")]
    [SerializeField] private float batteryLevel = 0f; //电池电量
    [SerializeField] private bool isCharging = false; //是否充电
    [SerializeField] private string networkReachability = ""; //网络可达性
    [SerializeField] private bool isInternetReachable = false; //网络是否可达
    [SerializeField] private string deviceOrientation = ""; //设备方向
    [SerializeField] private string screenOrientation = ""; //屏幕方向
    
    [Header("iOS功能")]
    [SerializeField] private bool enableHapticFeedback = true; //启用触觉反馈
    [SerializeField] private bool enableScreenCapture = false; //启用屏幕捕获
    [SerializeField] private bool enableShareSheet = false; //启用分享功能
    [SerializeField] private bool enableLocalNotifications = false; //启用本地通知
    [SerializeField] private bool enablePushNotifications = false; //启用推送通知
    
    [Header("iOS调试")]
    [SerializeField] private bool enableDebugLogging = true; //启用调试日志
    [SerializeField] private bool enablePerformanceMonitoring = true; //启用性能监控
    [SerializeField] private bool enableCrashReporting = false; //启用崩溃报告
    [SerializeField] private bool enableAnalytics = false; //启用分析
    
    private bool isInitialized = false;
    private float lastUpdateTime = 0f;
    private Vector3 lastGyroscopeData = Vector3.zero;
    private Vector3 lastAccelerometerData = Vector3.zero;

    private void Start()
    {
        InitializeiOSSystem();
    }

    /// <summary>
    /// 初始化iOS系统
    /// </summary>
    private void InitializeiOSSystem()
    {
        // 检查是否为iOS平台
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            Debug.LogWarning("此脚本仅在iOS平台上运行");
            return;
        }
        
        // 获取iOS设备信息
        GetiOSDeviceInfo();
        
        // 检测iOS功能
        DetectiOSFeatures();
        
        // 初始化iOS设置
        InitializeiOSSettings();
        
        // 启动iOS功能
        StartiOSFeatures();
        
        isInitialized = true;
        Debug.Log("iOS系统初始化完成");
    }

    /// <summary>
    /// 获取iOS设备信息
    /// </summary>
    private void GetiOSDeviceInfo()
    {
        deviceModel = SystemInfo.deviceModel;
        deviceName = SystemInfo.deviceName;
        systemVersion = SystemInfo.operatingSystem;
        deviceIdentifier = SystemInfo.deviceUniqueIdentifier;
        
        // 检测设备类型
        isIPad = deviceModel.ToLower().Contains("ipad");
        isIPhone = deviceModel.ToLower().Contains("iphone");
        isIPod = deviceModel.ToLower().Contains("ipod");
        
        Debug.Log($"iOS设备: {deviceModel} ({deviceName})");
        Debug.Log($"系统版本: {systemVersion}");
        Debug.Log($"设备类型: {(isIPad ? "iPad" : isIPhone ? "iPhone" : isIPod ? "iPod" : "未知")}");
    }

    /// <summary>
    /// 检测iOS功能
    /// </summary>
    private void DetectiOSFeatures()
    {
        supportsGyroscope = SystemInfo.supportsGyroscope;
        supportsAccelerometer = SystemInfo.supportsAccelerometer;
        supportsLocationService = Input.location.isEnabledByUser;
        supportsCamera = SystemInfo.supportsCamera;
        supportsMicrophone = SystemInfo.supportsAudio;
        supportsVibration = SystemInfo.supportsVibration;
        supportsMultitouch = Input.multiTouchEnabled;
        supportsMetal = SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Metal;
        
        Debug.Log($"陀螺仪支持: {supportsGyroscope}");
        Debug.Log($"加速度计支持: {supportsAccelerometer}");
        Debug.Log($"位置服务支持: {supportsLocationService}");
        Debug.Log($"相机支持: {supportsCamera}");
        Debug.Log($"麦克风支持: {supportsMicrophone}");
        Debug.Log($"震动支持: {supportsVibration}");
        Debug.Log($"多点触控支持: {supportsMultitouch}");
        Debug.Log($"Metal支持: {supportsMetal}");
    }

    /// <summary>
    /// 初始化iOS设置
    /// </summary>
    private void InitializeiOSSettings()
    {
        // 设置空闲计时器
        Screen.sleepTimeout = enableIdleTimer ? SleepTimeout.SystemSetting : SleepTimeout.NeverSleep;
        
        // 设置多点触控
        Input.multiTouchEnabled = enableMultitouch;
        
        // 设置屏幕方向
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
        
        Debug.Log("iOS设置初始化完成");
    }

    /// <summary>
    /// 启动iOS功能
    /// </summary>
    private void StartiOSFeatures()
    {
        // 启动位置服务
        if (enableLocationService && supportsLocationService)
        {
            StartLocationService();
        }
        
        // 启动相机
        if (enableCamera && supportsCamera)
        {
            StartCamera();
        }
        
        // 启动麦克风
        if (enableMicrophone && supportsMicrophone)
        {
            StartMicrophone();
        }
        
        Debug.Log("iOS功能启动完成");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新iOS状态
        UpdateiOSStatus();
        
        // 处理iOS输入
        HandleiOSInput();
        
        // 性能监控
        if (enablePerformanceMonitoring && Time.time - lastUpdateTime > 1f)
        {
            MonitorPerformance();
            lastUpdateTime = Time.time;
        }
    }

    /// <summary>
    /// 更新iOS状态
    /// </summary>
    private void UpdateiOSStatus()
    {
        // 获取电池信息
        batteryLevel = SystemInfo.batteryLevel;
        isCharging = SystemInfo.batteryStatus == BatteryStatus.Charging;
        
        // 获取网络状态
        networkReachability = Application.internetReachability.ToString();
        isInternetReachable = Application.internetReachability != NetworkReachability.NotReachable;
        
        // 获取设备方向
        deviceOrientation = Input.deviceOrientation.ToString();
        screenOrientation = Screen.orientation.ToString();
    }

    /// <summary>
    /// 处理iOS输入
    /// </summary>
    private void HandleiOSInput()
    {
        // 处理陀螺仪输入
        if (enableGyroscope && supportsGyroscope)
        {
            Vector3 gyroscopeData = Input.gyro.rotationRate;
            if (gyroscopeData != lastGyroscopeData)
            {
                lastGyroscopeData = gyroscopeData;
                if (enableDebugLogging)
                {
                    Debug.Log($"陀螺仪数据: {gyroscopeData}");
                }
            }
        }
        
        // 处理加速度计输入
        if (enableAccelerometer && supportsAccelerometer)
        {
            Vector3 accelerometerData = Input.acceleration;
            if (accelerometerData != lastAccelerometerData)
            {
                lastAccelerometerData = accelerometerData;
                if (enableDebugLogging)
                {
                    Debug.Log($"加速度计数据: {accelerometerData}");
                }
            }
        }
        
        // 处理触摸输入
        if (enableMultitouch && supportsMultitouch)
        {
            HandleTouchInput();
        }
    }

    /// <summary>
    /// 处理触摸输入
    /// </summary>
    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (enableDebugLogging)
                        {
                            Debug.Log($"触摸开始: {touch.position}, 手指ID: {touch.fingerId}");
                        }
                        break;
                    case TouchPhase.Moved:
                        if (enableDebugLogging)
                        {
                            Debug.Log($"触摸移动: {touch.position}, 手指ID: {touch.fingerId}");
                        }
                        break;
                    case TouchPhase.Ended:
                        if (enableDebugLogging)
                        {
                            Debug.Log($"触摸结束: {touch.position}, 手指ID: {touch.fingerId}");
                        }
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 监控性能
    /// </summary>
    private void MonitorPerformance()
    {
        float fps = 1f / Time.deltaTime;
        long memory = System.GC.GetTotalMemory(false);
        
        if (enableDebugLogging)
        {
            Debug.Log($"性能监控: FPS={fps:F1}, 内存={memory / (1024 * 1024):F1}MB");
        }
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
    /// 启动相机
    /// </summary>
    public void StartCamera()
    {
        if (supportsCamera)
        {
            // 这里可以实现相机功能
            Debug.Log("相机功能已启动");
        }
    }

    /// <summary>
    /// 启动麦克风
    /// </summary>
    public void StartMicrophone()
    {
        if (supportsMicrophone)
        {
            // 这里可以实现麦克风功能
            Debug.Log("麦克风功能已启动");
        }
    }

    /// <summary>
    /// 触发震动
    /// </summary>
    public void TriggerVibration()
    {
        if (supportsVibration)
        {
            Handheld.Vibrate();
            Debug.Log("震动已触发");
        }
    }

    /// <summary>
    /// 触发触觉反馈
    /// </summary>
    public void TriggerHapticFeedback()
    {
        if (enableHapticFeedback)
        {
            // iOS触觉反馈
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                // 这里可以调用iOS原生触觉反馈API
                Debug.Log("触觉反馈已触发");
            }
        }
    }

    /// <summary>
    /// 捕获屏幕截图
    /// </summary>
    public void CaptureScreenshot()
    {
        if (enableScreenCapture)
        {
            string filename = $"screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
            ScreenCapture.CaptureScreenshotAsTexture();
            Debug.Log($"屏幕截图已保存: {filename}");
        }
    }

    /// <summary>
    /// 显示分享界面
    /// </summary>
    public void ShowShareSheet()
    {
        if (enableShareSheet)
        {
            // 这里可以实现iOS分享功能
            Debug.Log("分享界面已显示");
        }
    }

    /// <summary>
    /// 发送本地通知
    /// </summary>
    public void SendLocalNotification()
    {
        if (enableLocalNotifications)
        {
            // 这里可以实现iOS本地通知
            Debug.Log("本地通知已发送");
        }
    }

    /// <summary>
    /// 获取iOS设备详细信息
    /// </summary>
    public void GetiOSDeviceDetails()
    {
        Debug.Log("=== iOS设备详细信息 ===");
        Debug.Log($"设备型号: {deviceModel}");
        Debug.Log($"设备名称: {deviceName}");
        Debug.Log($"系统版本: {systemVersion}");
        Debug.Log($"设备标识符: {deviceIdentifier}");
        Debug.Log($"设备类型: {(isIPad ? "iPad" : isIPhone ? "iPhone" : isIPod ? "iPod" : "未知")}");
        Debug.Log($"电池电量: {batteryLevel:P0}");
        Debug.Log($"充电状态: {(isCharging ? "充电中" : "未充电")}");
        Debug.Log($"网络状态: {networkReachability}");
        Debug.Log($"设备方向: {deviceOrientation}");
        Debug.Log($"屏幕方向: {screenOrientation}");
    }

    /// <summary>
    /// 获取iOS功能信息
    /// </summary>
    public void GetiOSFeaturesInfo()
    {
        Debug.Log("=== iOS功能信息 ===");
        Debug.Log($"陀螺仪支持: {supportsGyroscope}");
        Debug.Log($"加速度计支持: {supportsAccelerometer}");
        Debug.Log($"位置服务支持: {supportsLocationService}");
        Debug.Log($"相机支持: {supportsCamera}");
        Debug.Log($"麦克风支持: {supportsMicrophone}");
        Debug.Log($"震动支持: {supportsVibration}");
        Debug.Log($"多点触控支持: {supportsMultitouch}");
        Debug.Log($"Metal支持: {supportsMetal}");
    }

    /// <summary>
    /// 测试iOS功能
    /// </summary>
    public void TestiOSFeatures()
    {
        Debug.Log("开始测试iOS功能");
        
        // 测试震动
        TriggerVibration();
        
        // 测试触觉反馈
        TriggerHapticFeedback();
        
        // 测试位置服务
        if (supportsLocationService)
        {
            StartLocationService();
        }
        
        // 测试屏幕截图
        if (enableScreenCapture)
        {
            CaptureScreenshot();
        }
        
        Debug.Log("iOS功能测试完成");
    }

    /// <summary>
    /// 设置iOS配置
    /// </summary>
    public void SetiOSConfiguration()
    {
        // 设置屏幕方向
        Screen.orientation = ScreenOrientation.Portrait;
        
        // 设置空闲计时器
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        
        // 设置多点触控
        Input.multiTouchEnabled = true;
        
        Debug.Log("iOS配置已设置");
    }

    /// <summary>
    /// 重置iOS设置
    /// </summary>
    public void ResetiOSSettings()
    {
        enableIdleTimer = true;
        enableMultitouch = true;
        enableGyroscope = true;
        enableAccelerometer = true;
        enableLocationService = false;
        enableCamera = false;
        enableMicrophone = false;
        enableHapticFeedback = true;
        enableScreenCapture = false;
        enableShareSheet = false;
        enableLocalNotifications = false;
        enablePushNotifications = false;
        
        Debug.Log("iOS设置已重置");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("iOS 平台特定功能演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("设备信息:");
        GUILayout.Label($"设备: {deviceModel}");
        GUILayout.Label($"名称: {deviceName}");
        GUILayout.Label($"系统: {systemVersion}");
        GUILayout.Label($"类型: {(isIPad ? "iPad" : isIPhone ? "iPhone" : isIPod ? "iPod" : "未知")}");
        GUILayout.Label($"电池: {batteryLevel:P0}");
        GUILayout.Label($"充电: {(isCharging ? "是" : "否")}");
        GUILayout.Label($"网络: {networkReachability}");
        GUILayout.Label($"方向: {deviceOrientation}");
        
        GUILayout.Space(10);
        GUILayout.Label("功能支持:");
        GUILayout.Label($"陀螺仪: {(supportsGyroscope ? "支持" : "不支持")}");
        GUILayout.Label($"加速度计: {(supportsAccelerometer ? "支持" : "不支持")}");
        GUILayout.Label($"位置服务: {(supportsLocationService ? "支持" : "不支持")}");
        GUILayout.Label($"相机: {(supportsCamera ? "支持" : "不支持")}");
        GUILayout.Label($"麦克风: {(supportsMicrophone ? "支持" : "不支持")}");
        GUILayout.Label($"震动: {(supportsVibration ? "支持" : "不支持")}");
        GUILayout.Label($"多点触控: {(supportsMultitouch ? "支持" : "不支持")}");
        GUILayout.Label($"Metal: {(supportsMetal ? "支持" : "不支持")}");
        
        GUILayout.Space(10);
        GUILayout.Label("功能设置:");
        enableIdleTimer = GUILayout.Toggle(enableIdleTimer, "启用空闲计时器");
        enableMultitouch = GUILayout.Toggle(enableMultitouch, "启用多点触控");
        enableGyroscope = GUILayout.Toggle(enableGyroscope, "启用陀螺仪");
        enableAccelerometer = GUILayout.Toggle(enableAccelerometer, "启用加速度计");
        enableLocationService = GUILayout.Toggle(enableLocationService, "启用位置服务");
        enableCamera = GUILayout.Toggle(enableCamera, "启用相机");
        enableMicrophone = GUILayout.Toggle(enableMicrophone, "启用麦克风");
        enableHapticFeedback = GUILayout.Toggle(enableHapticFeedback, "启用触觉反馈");
        enableScreenCapture = GUILayout.Toggle(enableScreenCapture, "启用屏幕捕获");
        enableShareSheet = GUILayout.Toggle(enableShareSheet, "启用分享功能");
        enableLocalNotifications = GUILayout.Toggle(enableLocalNotifications, "启用本地通知");
        enablePushNotifications = GUILayout.Toggle(enablePushNotifications, "启用推送通知");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取设备详情"))
        {
            GetiOSDeviceDetails();
        }
        
        if (GUILayout.Button("获取功能信息"))
        {
            GetiOSFeaturesInfo();
        }
        
        if (GUILayout.Button("测试iOS功能"))
        {
            TestiOSFeatures();
        }
        
        if (GUILayout.Button("启动位置服务"))
        {
            StartLocationService();
        }
        
        if (GUILayout.Button("触发震动"))
        {
            TriggerVibration();
        }
        
        if (GUILayout.Button("触发触觉反馈"))
        {
            TriggerHapticFeedback();
        }
        
        if (GUILayout.Button("捕获屏幕截图"))
        {
            CaptureScreenshot();
        }
        
        if (GUILayout.Button("显示分享界面"))
        {
            ShowShareSheet();
        }
        
        if (GUILayout.Button("发送本地通知"))
        {
            SendLocalNotification();
        }
        
        if (GUILayout.Button("设置iOS配置"))
        {
            SetiOSConfiguration();
        }
        
        if (GUILayout.Button("重置iOS设置"))
        {
            ResetiOSSettings();
        }
        
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        // 停止位置服务
        if (Input.location.isEnabledByUser)
        {
            Input.location.Stop();
        }
    }
} 