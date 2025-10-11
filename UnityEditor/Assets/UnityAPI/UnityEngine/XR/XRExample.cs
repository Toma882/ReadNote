// using UnityEngine;
// using UnityEngine.XR;
// using System.Collections.Generic;

// /// <summary>
// /// UnityEngine.XR 命名空间案例演示
// /// 展示XR设备检测、输入处理、空间定位等核心功能
// /// </summary>
// public class XRExample : MonoBehaviour
// {
//     [Header("XR设置")]
//     [SerializeField] private bool enableXR = true;
//     [SerializeField] private bool autoStartXR = true;
    
//     [Header("XR状态")]
//     [SerializeField] private bool isXRInitialized = false;
//     [SerializeField] private bool isXRRunning = false;
//     [SerializeField] private string currentXRDevice = "";
//     [SerializeField] private XRDisplaySubsystem xrDisplay;
//     [SerializeField] private XRInputSubsystem xrInput;
    
//     [Header("XR设备信息")]
//     [SerializeField] private bool isXRDevicePresent = false; //是否有XR设备
//     [SerializeField] private string deviceName = ""; //设备名称
//     [SerializeField] private string deviceModel = ""; //设备型号
//     [SerializeField] private bool isActive = false; //设备是否激活
    
//     [Header("控制器输入")]
//     [SerializeField] private Vector2 leftThumbstick = Vector2.zero; //左手摇杆
//     [SerializeField] private Vector2 rightThumbstick = Vector2.zero; //右手摇杆
//     [SerializeField] private bool leftTriggerPressed = false; //左手扳机
//     [SerializeField] private bool rightTriggerPressed = false; //右手扳机
//     [SerializeField] private bool leftGripPressed = false; //左手握把
//     [SerializeField] private bool rightGripPressed = false; //右手握把
    
//     [Header("头部追踪")]
//     [SerializeField] private Vector3 headPosition = Vector3.zero; //头部位置
//     [SerializeField] private Quaternion headRotation = Quaternion.identity; //头部旋转
//     [SerializeField] private bool headTrackingActive = false; //头部追踪是否激活
    
//     [Header("手部追踪")]
//     [SerializeField] private Vector3 leftHandPosition = Vector3.zero; //左手位置
//     [SerializeField] private Quaternion leftHandRotation = Quaternion.identity; //左手旋转
//     [SerializeField] private Vector3 rightHandPosition = Vector3.zero; //右手位置
//     [SerializeField] private Quaternion rightHandRotation = Quaternion.identity; //右手旋转
//     [SerializeField] private bool handTrackingActive = false; //手部追踪是否激活
    
//     [Header("空间定位")]
//     [SerializeField] private bool trackingSpaceActive = false; //追踪空间是否激活
//     [SerializeField] private Vector3 trackingOriginPosition = Vector3.zero; //追踪原点位置
//     [SerializeField] private Quaternion trackingOriginRotation = Quaternion.identity; //追踪原点旋转
    
//     [Header("XR渲染")]
//     [SerializeField] private float renderScale = 1.0f;
//     [SerializeField] private bool enableMSAA = true;
//     [SerializeField] private int msaaLevel = 4;
//     [SerializeField] private bool enableHDR = false;
    
//     // XR事件
//     private System.Action<bool> onXRInitialized;
//     private System.Action<bool> onXRStarted;
//     private System.Action<InputDevice> onDeviceConnected;
//     private System.Action<InputDevice> onDeviceDisconnected;
    
//     private InputDevice leftController;
//     private InputDevice rightController;
//     private InputDevice hmd;
    
//     private void Start()
//     {
//         InitializeXRSystem();
//     }
    
//     /// <summary>
//     /// 初始化XR系统
//     /// </summary>
//     private void InitializeXRSystem()
//     {
//         if (!enableXR)
//         {
//             Debug.Log("XR已禁用");
//             return;
//         }
        
//         // 检查XR设备
//         isXRDevicePresent = XRSettings.isDeviceActive;
//         deviceName = XRSettings.loadedDeviceName;
//         deviceModel = XRSettings.loadedDeviceName;
//         isActive = XRSettings.enabled;

//         if (isXRDevicePresent)
//         {
//             Debug.Log("XR设备已检测到");
//             SetupInputDevices();
//         }
//         else
//         {
//             Debug.Log("未检测到XR设备");
//         }
        
//         // 初始化XR管理器
//         var xrManagerSettings = XRGeneralSettings.Instance.Manager;
//         if (xrManagerSettings == null)
//         {
//             Debug.LogError("XR管理器设置不可用");
//             return;
//         }
        
//         // 设置XR加载器
//         if (xrLoader != null)
//         {
//             xrManagerSettings.TryAddLoader(xrLoader);
//         }
        
//         // 启动XR
//         if (autoStartXR)
//         {
//             StartXR();
//         }
        
//         // 设置设备事件
//         SetupDeviceEvents();
        
//         Debug.Log("XR系统初始化完成");
//     }
    
//     /// <summary>
//     /// 设置输入设备
//     /// </summary>
//     private void SetupInputDevices()
//     {
//         // 获取HMD设备
//         var hmdDevices = new List<InputDevice>();
//         InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, hmdDevices);
//         if (hmdDevices.Count > 0)
//         {
//             hmd = hmdDevices[0];
//             Debug.Log($"HMD设备: {hmd.name}");
//         }

//         // 获取左手控制器
//         var leftHandDevices = new List<InputDevice>();
//         InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftHandDevices);
//         if (leftHandDevices.Count > 0)
//         {
//             leftController = leftHandDevices[0];
//             Debug.Log($"左手控制器: {leftController.name}");
//         }

//         // 获取右手控制器
//         var rightHandDevices = new List<InputDevice>();
//         InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightHandDevices);
//         if (rightHandDevices.Count > 0)
//         {
//             rightController = rightHandDevices[0];
//             Debug.Log($"右手控制器: {rightController.name}");
//         }
//     }
    
//     /// <summary>
//     /// 启动XR
//     /// </summary>
//     public void StartXR()
//     {
//         if (!enableXR)
//         {
//             Debug.Log("XR已禁用");
//             return;
//         }
        
//         var xrManagerSettings = XRGeneralSettings.Instance.Manager;
//         if (xrManagerSettings == null)
//         {
//             Debug.LogError("XR管理器设置不可用");
//             return;
//         }
        
//         // 启动XR
//         if (xrManagerSettings.activeLoader == null)
//         {
//             Debug.LogError("没有活动的XR加载器");
//             return;
//         }
        
//         xrManagerSettings.StartSubsystems();
//         isXRRunning = true;
        
//         // 获取XR子系统
//         GetXRSubsystems();
        
//         Debug.Log("XR已启动");
        
//         onXRStarted?.Invoke(true);
//     }
    
//     /// <summary>
//     /// 停止XR
//     /// </summary>
//     public void StopXR()
//     {
//         var xrManagerSettings = XRGeneralSettings.Instance.Manager;
//         if (xrManagerSettings == null)
//         {
//             return;
//         }
        
//         xrManagerSettings.StopSubsystems();
//         isXRRunning = false;
        
//         Debug.Log("XR已停止");
        
//         onXRStarted?.Invoke(false);
//     }
    
//     /// <summary>
//     /// 获取XR子系统
//     /// </summary>
//     private void GetXRSubsystems()
//     {
//         // 获取显示子系统
//         var displaySubsystems = new List<XRDisplaySubsystem>();
//         SubsystemManager.GetInstances(displaySubsystems);
//         if (displaySubsystems.Count > 0)
//         {
//             xrDisplay = displaySubsystems[0];
//         }
        
//         // 获取输入子系统
//         var inputSubsystems = new List<XRInputSubsystem>();
//         SubsystemManager.GetInstances(inputSubsystems);
//         if (inputSubsystems.Count > 0)
//         {
//             xrInput = inputSubsystems[0];
//         }
        
//         Debug.Log($"获取到 {displaySubsystems.Count} 个显示子系统, {inputSubsystems.Count} 个输入子系统");
//     }
    
//     /// <summary>
//     /// 设置设备事件
//     /// </summary>
//     private void SetupDeviceEvents()
//     {
//         // 设备连接事件
//         InputDevices.deviceConnected += OnDeviceConnected;
//         InputDevices.deviceDisconnected += OnDeviceDisconnected;
        
//         Debug.Log("设备事件设置完成");
//     }
    
//     /// <summary>
//     /// 设备连接事件
//     /// </summary>
//     /// <param name="device">连接的设备</param>
//     private void OnDeviceConnected(InputDevice device)
//     {
//         Debug.Log($"设备已连接: {device.name} ({device.characteristics})");
        
//         onDeviceConnected?.Invoke(device);
//     }
    
//     /// <summary>
//     /// 设备断开连接事件
//     /// </summary>
//     /// <param name="device">断开的设备</param>
//     private void OnDeviceDisconnected(InputDevice device)
//     {
//         Debug.Log($"设备已断开: {device.name}");
        
//         onDeviceDisconnected?.Invoke(device);
//     }
    
//     /// <summary>
//     /// 获取XR设备信息
//     /// </summary>
//     public void GetXRDeviceInfo()
//     {
//         Debug.Log("=== XR设备信息 ===");
//         Debug.Log($"XR设备存在: {isXRDevicePresent}");
//         Debug.Log($"设备名称: {deviceName}");
//         Debug.Log($"设备型号: {deviceModel}");
//         Debug.Log($"XR已启用: {isActive}");
//         Debug.Log($"当前设备: {XRSettings.loadedDeviceName}");
//         Debug.Log($"支持设备: {string.Join(", ", XRSettings.supportedDevices)}");
//         Debug.Log($"渲染比例: {XRSettings.renderScale}");
//         Debug.Log($"视场角: {XRSettings.fieldOfView}");
//         Debug.Log($"眼睛纹理分辨率: {XRSettings.eyeTextureResolutionScale}");
//     }
    
//     /// <summary>
//     /// 获取输入设备信息
//     /// </summary>
//     public void GetInputDeviceInfo()
//     {
//         Debug.Log("=== 输入设备信息 ===");
        
//         var devices = new List<InputDevice>();
//         InputDevices.GetDevices(devices);
        
//         foreach (var device in devices)
//         {
//             Debug.Log($"设备: {device.name} - {device.characteristics}");
//         }
//     }
    
//     /// <summary>
//     /// 获取控制器输入信息
//     /// </summary>
//     public void GetControllerInputInfo()
//     {
//         Debug.Log("=== 控制器输入信息 ===");
//         Debug.Log($"左手摇杆: {leftThumbstick}");
//         Debug.Log($"右手摇杆: {rightThumbstick}");
//         Debug.Log($"左手扳机: {leftTriggerPressed}");
//         Debug.Log($"右手扳机: {rightTriggerPressed}");
//         Debug.Log($"左手握把: {leftGripPressed}");
//         Debug.Log($"右手握把: {rightGripPressed}");
//     }
    
//     /// <summary>
//     /// 获取追踪信息
//     /// </summary>
//     public void GetTrackingInfo()
//     {
//         Debug.Log("=== 追踪信息 ===");
//         Debug.Log($"头部位置: {headPosition}");
//         Debug.Log($"头部旋转: {headRotation.eulerAngles}");
//         Debug.Log($"头部追踪激活: {headTrackingActive}");
//         Debug.Log($"左手位置: {leftHandPosition}");
//         Debug.Log($"左手旋转: {leftHandRotation.eulerAngles}");
//         Debug.Log($"右手位置: {rightHandPosition}");
//         Debug.Log($"右手旋转: {rightHandRotation.eulerAngles}");
//         Debug.Log($"手部追踪激活: {handTrackingActive}");
//         Debug.Log($"追踪空间激活: {trackingSpaceActive}");
//     }
    
//     /// <summary>
//     /// 重置XR设置
//     /// </summary>
//     public void ResetXRSettings()
//     {
//         // 重置输入值
//         leftThumbstick = Vector2.zero;
//         rightThumbstick = Vector2.zero;
//         leftTriggerPressed = false;
//         rightTriggerPressed = false;
//         leftGripPressed = false;
//         rightGripPressed = false;

//         // 重置追踪数据
//         headPosition = Vector3.zero;
//         headRotation = Quaternion.identity;
//         leftHandPosition = Vector3.zero;
//         leftHandRotation = Quaternion.identity;
//         rightHandPosition = Vector3.zero;
//         rightHandRotation = Quaternion.identity;

//         Debug.Log("XR设置已重置");
//     }
    
//     /// <summary>
//     /// 检查特定输入
//     /// </summary>
//     /// <param name="inputName">输入名称</param>
//     /// <returns>是否按下</returns>
//     public bool CheckInput(string inputName)
//     {
//         switch (inputName.ToLower())
//         {
//             case "lefttrigger":
//                 return leftTriggerPressed;
//             case "righttrigger":
//                 return rightTriggerPressed;
//             case "leftgrip":
//                 return leftGripPressed;
//             case "rightgrip":
//                 return rightGripPressed;
//             default:
//                 return false;
//         }
//     }
    
//     /// <summary>
//     /// 获取摇杆输入
//     /// </summary>
//     /// <param name="hand">手部（left/right）</param>
//     /// <returns>摇杆值</returns>
//     public Vector2 GetThumbstickInput(string hand)
//     {
//         switch (hand.ToLower())
//         {
//             case "left":
//                 return leftThumbstick;
//             case "right":
//                 return rightThumbstick;
//             default:
//                 return Vector2.zero;
//         }
//     }
    
//     private void Update()
//     {
//         if (isXRRunning)
//         {
//             UpdateXRInput();
//             UpdateTrackingData();
//         }
//     }
    
//     /// <summary>
//     /// 更新XR输入
//     /// </summary>
//     private void UpdateXRInput()
//     {
//         // 更新左手控制器输入
//         if (leftController.isValid)
//         {
//             leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftThumbstick);
//             leftController.TryGetFeatureValue(CommonUsages.triggerButton, out leftTriggerPressed);
//             leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGripPressed);
//         }

//         // 更新右手控制器输入
//         if (rightController.isValid)
//         {
//             rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightThumbstick);
//             rightController.TryGetFeatureValue(CommonUsages.triggerButton, out rightTriggerPressed);
//             rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripPressed);
//         }
//     }
    
//     /// <summary>
//     /// 更新追踪数据
//     /// </summary>
//     private void UpdateTrackingData()
//     {
//         // 更新头部追踪
//         if (hmd.isValid)
//         {
//             hmd.TryGetFeatureValue(CommonUsages.devicePosition, out headPosition);
//             hmd.TryGetFeatureValue(CommonUsages.deviceRotation, out headRotation);
//             headTrackingActive = hmd.isValid;
//         }

//         // 更新左手追踪
//         if (leftController.isValid)
//         {
//             leftController.TryGetFeatureValue(CommonUsages.devicePosition, out leftHandPosition);
//             leftController.TryGetFeatureValue(CommonUsages.deviceRotation, out leftHandRotation);
//         }

//         // 更新右手追踪
//         if (rightController.isValid)
//         {
//             rightController.TryGetFeatureValue(CommonUsages.devicePosition, out rightHandPosition);
//             rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out rightHandRotation);
//         }

//         handTrackingActive = leftController.isValid || rightController.isValid;

//         // 更新追踪空间
//         trackingSpaceActive = XRSettings.isDeviceActive;
//         if (trackingSpaceActive)
//         {
//             trackingOriginPosition = InputTracking.GetLocalPosition(XRNode.TrackingReference);
//             trackingOriginRotation = InputTracking.GetLocalRotation(XRNode.TrackingReference);
//         }
//     }
    
//     /// <summary>
//     /// 设置渲染比例
//     /// </summary>
//     /// <param name="scale">渲染比例</param>
//     public void SetRenderScale(float scale)
//     {
//         renderScale = Mathf.Clamp(scale, 0.1f, 2.0f);
//         XRSettings.eyeTextureResolutionScale = renderScale;
        
//         Debug.Log($"渲染比例已设置为: {renderScale}");
//     }
    
//     /// <summary>
//     /// 设置MSAA
//     /// </summary>
//     /// <param name="enabled">是否启用MSAA</param>
//     /// <param name="level">MSAA级别</param>
//     public void SetMSAA(bool enabled, int level = 4)
//     {
//         enableMSAA = enabled;
//         msaaLevel = level;
        
//         if (enabled)
//         {
//             QualitySettings.antiAliasing = level;
//         }
//         else
//         {
//             QualitySettings.antiAliasing = 0;
//         }
        
//         Debug.Log($"MSAA已设置为: {(enabled ? $"启用({level}x)" : "禁用")}");
//     }
    
//     /// <summary>
//     /// 设置HDR
//     /// </summary>
//     /// <param name="enabled">是否启用HDR</param>
//     public void SetHDR(bool enabled)
//     {
//         enableHDR = enabled;
        
//         // 设置HDR
//         if (Camera.main != null)
//         {
//             Camera.main.allowHDR = enabled;
//         }
        
//         Debug.Log($"HDR已设置为: {enabled}");
//     }
    
//     /// <summary>
//     /// 获取XR性能信息
//     /// </summary>
//     public void GetXRPerformanceInfo()
//     {
//         Debug.Log("=== XR性能信息 ===");
//         Debug.Log($"渲染比例: {XRSettings.eyeTextureResolutionScale}");
//         Debug.Log($"显示频率: {XRSettings.refreshRate}");
//         Debug.Log($"立体渲染模式: {XRSettings.stereoRenderingMode}");
//         Debug.Log($"VR设备: {XRSettings.isDeviceActive}");
//         Debug.Log($"VR设备名称: {XRSettings.loadedDeviceName}");
//         Debug.Log($"VR设备模型: {XRSettings.deviceModel}");
        
//         if (xrDisplay != null)
//         {
//             Debug.Log($"显示子系统运行中: {xrDisplay.running}");
//             Debug.Log($"显示子系统初始化: {xrDisplay.initialized}");
//         }
        
//         if (xrInput != null)
//         {
//             Debug.Log($"输入子系统运行中: {xrInput.running}");
//             Debug.Log($"输入子系统初始化: {xrInput.initialized}");
//         }
//     }
    
//     /// <summary>
//     /// 检查XR功能支持
//     /// </summary>
//     public void CheckXRFeatureSupport()
//     {
//         Debug.Log("=== XR功能支持检查 ===");
        
//         // 检查基本XR支持
//         Debug.Log($"XR支持: {XRSettings.supportedDevices.Length > 0}");
//         Debug.Log($"支持的设备: {string.Join(", ", XRSettings.supportedDevices)}");
        
//         // 检查特定功能
//         var devices = new List<InputDevice>();
//         InputDevices.GetDevices(devices);
        
//         foreach (var device in devices)
//         {
//             Debug.Log($"设备: {device.name}");
            
//             // 检查位置追踪
//             if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position))
//             {
//                 Debug.Log($"  位置追踪: 支持");
//             }
            
//             // 检查旋转追踪
//             if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation))
//             {
//                 Debug.Log($"  旋转追踪: 支持");
//             }
            
//             // 检查触发器
//             if (device.TryGetFeatureValue(CommonUsages.trigger, out float trigger))
//             {
//                 Debug.Log($"  触发器: 支持");
//             }
            
//             // 检查握把
//             if (device.TryGetFeatureValue(CommonUsages.grip, out float grip))
//             {
//                 Debug.Log($"  握把: 支持");
//             }
            
//             // 检查摇杆
//             if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstick))
//             {
//                 Debug.Log($"  摇杆: 支持");
//             }
//         }
//     }
    
//     private void OnGUI()
//     {
//         GUILayout.BeginArea(new Rect(10, 10, 400, 600));
//         GUILayout.Label("XR 扩展现实系统演示", UnityEditor.EditorStyles.boldLabel);
        
//         GUILayout.Space(10);
//         GUILayout.Label("设备状态:");
//         GUILayout.Label($"XR设备: {(isXRDevicePresent ? "已连接" : "未连接")}");
//         GUILayout.Label($"设备名称: {deviceName}");
//         GUILayout.Label($"设备激活: {isActive}");
        
//         GUILayout.Space(10);
//         GUILayout.Label("控制器输入:");
//         GUILayout.Label($"左手摇杆: {leftThumbstick}");
//         GUILayout.Label($"右手摇杆: {rightThumbstick}");
//         GUILayout.Label($"左手扳机: {(leftTriggerPressed ? "按下" : "释放")}");
//         GUILayout.Label($"右手扳机: {(rightTriggerPressed ? "按下" : "释放")}");
//         GUILayout.Label($"左手握把: {(leftGripPressed ? "按下" : "释放")}");
//         GUILayout.Label($"右手握把: {(rightGripPressed ? "按下" : "释放")}");
        
//         GUILayout.Space(10);
//         GUILayout.Label("追踪状态:");
//         GUILayout.Label($"头部追踪: {(headTrackingActive ? "激活" : "未激活")}");
//         GUILayout.Label($"手部追踪: {(handTrackingActive ? "激活" : "未激活")}");
//         GUILayout.Label($"追踪空间: {(trackingSpaceActive ? "激活" : "未激活")}");
        
//         GUILayout.Space(10);
        
//         if (GUILayout.Button("获取XR设备信息"))
//         {
//             GetXRDeviceInfo();
//         }
        
//         if (GUILayout.Button("获取输入设备信息"))
//         {
//             GetInputDeviceInfo();
//         }
        
//         if (GUILayout.Button("获取控制器输入信息"))
//         {
//             GetControllerInputInfo();
//         }
        
//         if (GUILayout.Button("获取追踪信息"))
//         {
//             GetTrackingInfo();
//         }
        
//         if (GUILayout.Button("重置XR设置"))
//         {
//             ResetXRSettings();
//         }
        
//         GUILayout.EndArea();
//     }
    
//     private void OnDestroy()
//     {
//         // 清理事件
//         InputDevices.deviceConnected -= OnDeviceConnected;
//         InputDevices.deviceDisconnected -= OnDeviceDisconnected;
        
//         // 停止XR
//         if (isXRRunning)
//         {
//             StopXR();
//         }
//     }
// } 
// } 