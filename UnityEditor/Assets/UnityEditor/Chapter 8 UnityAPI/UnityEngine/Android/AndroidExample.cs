using UnityEngine;

/// <summary>
/// UnityEngine.Android 命名空间案例演示
/// 展示Android平台特定功能、权限管理、设备信息等核心功能
/// </summary>
public class AndroidExample : MonoBehaviour
{
    [Header("Android设备信息")]
    [SerializeField] private string deviceModel = ""; //设备型号
    [SerializeField] private string deviceName = ""; //设备名称
    [SerializeField] private string androidVersion = ""; //Android版本
    [SerializeField] private int sdkVersion = 0; //SDK版本
    [SerializeField] private string manufacturer = ""; //制造商

    [Header("权限管理")]
    [SerializeField] private bool cameraPermission = false; //相机权限
    [SerializeField] private bool microphonePermission = false; //麦克风权限
    [SerializeField] private bool locationPermission = false; //位置权限
    [SerializeField] private bool storagePermission = false; //存储权限
    [SerializeField] private bool internetPermission = false; //网络权限

    [Header("Android功能")]
    [SerializeField] private bool enableVibration = true; //启用震动
    [SerializeField] private bool enableKeepAwake = true; //保持唤醒
    [SerializeField] private bool enableImmersiveMode = false; //沉浸模式
    [SerializeField] private bool enableFullscreen = false; //全屏模式

    [Header("Android设置")]
    [SerializeField] private int targetFrameRate = 60; //目标帧率
    [SerializeField] private bool enableMultithreadedRendering = true; //多线程渲染
    [SerializeField] private bool enableGraphicsJobs = true; //图形作业

    private void Start()
    {
        InitializeAndroidSystem();
    }

    /// <summary>
    /// 初始化Android系统
    /// </summary>
    private void InitializeAndroidSystem()
    {
        // 获取设备信息
        GetDeviceInfo();
        
        // 检查权限
        CheckPermissions();
        
        // 设置Android特定功能
        SetupAndroidFeatures();
        
        Debug.Log("Android系统初始化完成");
    }

    /// <summary>
    /// 获取设备信息
    /// </summary>
    private void GetDeviceInfo()
    {
        deviceModel = SystemInfo.deviceModel;
        deviceName = SystemInfo.deviceName;
        androidVersion = SystemInfo.operatingSystem;
        sdkVersion = GetAndroidSDKVersion();
        manufacturer = GetDeviceManufacturer();
        
        Debug.Log($"设备信息: {deviceModel} ({manufacturer})");
        Debug.Log($"Android版本: {androidVersion}");
        Debug.Log($"SDK版本: {sdkVersion}");
    }

    /// <summary>
    /// 获取Android SDK版本
    /// </summary>
    /// <returns>SDK版本号</returns>
    private int GetAndroidSDKVersion()
    {
        // 这里应该调用Android API获取SDK版本
        // 由于Unity的限制，这里返回模拟值
        return 30; // Android 11
    }

    /// <summary>
    /// 获取设备制造商
    /// </summary>
    /// <returns>制造商名称</returns>
    private string GetDeviceManufacturer()
    {
        // 这里应该调用Android API获取制造商信息
        return "Unknown";
    }

    /// <summary>
    /// 检查权限
    /// </summary>
    private void CheckPermissions()
    {
        // 检查各种权限状态
        cameraPermission = CheckPermission("android.permission.CAMERA");
        microphonePermission = CheckPermission("android.permission.RECORD_AUDIO");
        locationPermission = CheckPermission("android.permission.ACCESS_FINE_LOCATION");
        storagePermission = CheckPermission("android.permission.WRITE_EXTERNAL_STORAGE");
        internetPermission = CheckPermission("android.permission.INTERNET");
    }

    /// <summary>
    /// 检查权限
    /// </summary>
    /// <param name="permission">权限名称</param>
    /// <returns>是否有权限</returns>
    private bool CheckPermission(string permission)
    {
        // 这里应该调用Android API检查权限
        // 由于Unity的限制，这里返回模拟值
        return true;
    }

    /// <summary>
    /// 设置Android特定功能
    /// </summary>
    private void SetupAndroidFeatures()
    {
        // 设置目标帧率
        Application.targetFrameRate = targetFrameRate;
        
        // 设置多线程渲染
        QualitySettings.multiThreadedRendering = enableMultithreadedRendering;
        
        // 设置图形作业
        QualitySettings.graphicsJobs = enableGraphicsJobs;
        
        // 设置保持唤醒
        if (enableKeepAwake)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
    }

    /// <summary>
    /// 请求权限
    /// </summary>
    /// <param name="permission">权限名称</param>
    public void RequestPermission(string permission)
    {
        // 这里应该调用Android API请求权限
        Debug.Log($"请求权限: {permission}");
        
        // 模拟权限授予
        switch (permission)
        {
            case "android.permission.CAMERA":
                cameraPermission = true;
                break;
            case "android.permission.RECORD_AUDIO":
                microphonePermission = true;
                break;
            case "android.permission.ACCESS_FINE_LOCATION":
                locationPermission = true;
                break;
            case "android.permission.WRITE_EXTERNAL_STORAGE":
                storagePermission = true;
                break;
            case "android.permission.INTERNET":
                internetPermission = true;
                break;
        }
    }

    /// <summary>
    /// 启用震动
    /// </summary>
    /// <param name="duration">震动时长（毫秒）</param>
    public void Vibrate(int duration = 100)
    {
        if (enableVibration)
        {
            // 这里应该调用Android震动API
            Debug.Log($"震动 {duration}ms");
        }
    }

    /// <summary>
    /// 设置沉浸模式
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetImmersiveMode(bool enabled)
    {
        enableImmersiveMode = enabled;
        
        if (enabled)
        {
            // 启用沉浸模式
            Debug.Log("沉浸模式已启用");
        }
        else
        {
            // 禁用沉浸模式
            Debug.Log("沉浸模式已禁用");
        }
    }

    /// <summary>
    /// 设置全屏模式
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetFullscreenMode(bool enabled)
    {
        enableFullscreen = enabled;
        Screen.fullScreen = enabled;
        
        Debug.Log($"全屏模式: {(enabled ? "启用" : "禁用")}");
    }

    /// <summary>
    /// 获取Android系统信息
    /// </summary>
    public void GetAndroidSystemInfo()
    {
        Debug.Log("=== Android系统信息 ===");
        Debug.Log($"设备型号: {deviceModel}");
        Debug.Log($"设备名称: {deviceName}");
        Debug.Log($"制造商: {manufacturer}");
        Debug.Log($"Android版本: {androidVersion}");
        Debug.Log($"SDK版本: {sdkVersion}");
        Debug.Log($"处理器: {SystemInfo.processorType}");
        Debug.Log($"内存: {SystemInfo.systemMemorySize}MB");
        Debug.Log($"图形设备: {SystemInfo.graphicsDeviceName}");
        Debug.Log($"图形内存: {SystemInfo.graphicsMemorySize}MB");
    }

    /// <summary>
    /// 获取权限信息
    /// </summary>
    public void GetPermissionInfo()
    {
        Debug.Log("=== 权限信息 ===");
        Debug.Log($"相机权限: {cameraPermission}");
        Debug.Log($"麦克风权限: {microphonePermission}");
        Debug.Log($"位置权限: {locationPermission}");
        Debug.Log($"存储权限: {storagePermission}");
        Debug.Log($"网络权限: {internetPermission}");
    }

    /// <summary>
    /// 测试Android功能
    /// </summary>
    public void TestAndroidFeatures()
    {
        Debug.Log("开始测试Android功能");
        
        // 测试震动
        Vibrate(200);
        
        // 测试权限请求
        RequestPermission("android.permission.CAMERA");
        
        // 测试沉浸模式
        SetImmersiveMode(true);
        
        Debug.Log("Android功能测试完成");
    }

    /// <summary>
    /// 重置Android设置
    /// </summary>
    public void ResetAndroidSettings()
    {
        enableVibration = true;
        enableKeepAwake = true;
        enableImmersiveMode = false;
        enableFullscreen = false;
        targetFrameRate = 60;
        enableMultithreadedRendering = true;
        enableGraphicsJobs = true;
        
        SetupAndroidFeatures();
        Debug.Log("Android设置已重置");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 700));
        GUILayout.Label("Android 平台功能演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("设备信息:");
        GUILayout.Label($"设备: {deviceModel}");
        GUILayout.Label($"制造商: {manufacturer}");
        GUILayout.Label($"Android版本: {androidVersion}");
        GUILayout.Label($"SDK版本: {sdkVersion}");
        
        GUILayout.Space(10);
        GUILayout.Label("权限状态:");
        GUILayout.Label($"相机: {(cameraPermission ? "已授权" : "未授权")}");
        GUILayout.Label($"麦克风: {(microphonePermission ? "已授权" : "未授权")}");
        GUILayout.Label($"位置: {(locationPermission ? "已授权" : "未授权")}");
        GUILayout.Label($"存储: {(storagePermission ? "已授权" : "未授权")}");
        GUILayout.Label($"网络: {(internetPermission ? "已授权" : "未授权")}");
        
        GUILayout.Space(10);
        GUILayout.Label("功能设置:");
        
        enableVibration = GUILayout.Toggle(enableVibration, "启用震动");
        enableKeepAwake = GUILayout.Toggle(enableKeepAwake, "保持唤醒");
        enableImmersiveMode = GUILayout.Toggle(enableImmersiveMode, "沉浸模式");
        enableFullscreen = GUILayout.Toggle(enableFullscreen, "全屏模式");
        
        GUILayout.Space(5);
        targetFrameRate = int.TryParse(GUILayout.TextField("目标帧率", targetFrameRate.ToString()), out var fps) ? fps : targetFrameRate;
        enableMultithreadedRendering = GUILayout.Toggle(enableMultithreadedRendering, "多线程渲染");
        enableGraphicsJobs = GUILayout.Toggle(enableGraphicsJobs, "图形作业");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("请求相机权限"))
        {
            RequestPermission("android.permission.CAMERA");
        }
        
        if (GUILayout.Button("请求麦克风权限"))
        {
            RequestPermission("android.permission.RECORD_AUDIO");
        }
        
        if (GUILayout.Button("震动测试"))
        {
            Vibrate(500);
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取系统信息"))
        {
            GetAndroidSystemInfo();
        }
        
        if (GUILayout.Button("获取权限信息"))
        {
            GetPermissionInfo();
        }
        
        if (GUILayout.Button("测试Android功能"))
        {
            TestAndroidFeatures();
        }
        
        if (GUILayout.Button("重置Android设置"))
        {
            ResetAndroidSettings();
        }
        
        GUILayout.EndArea();
    }
} 