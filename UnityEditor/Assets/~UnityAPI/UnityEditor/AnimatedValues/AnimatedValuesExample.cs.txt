using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

/// <summary>
/// UnityEditor.AnimatedValues 命名空间案例演示
/// 展示动画值系统的核心功能，包括数值动画、颜色动画、向量动画等
/// </summary>
public class AnimatedValuesExample : MonoBehaviour
{
    [Header("动画值配置")]
    [SerializeField] private bool enableAnimatedValues = true; //启用动画值
    [SerializeField] private bool enableFloatAnimation = true; //启用浮点数动画
    [SerializeField] private bool enableColorAnimation = true; //启用颜色动画
    [SerializeField] private bool enableVectorAnimation = true; //启用向量动画
    [SerializeField] private bool enableRectAnimation = true; //启用矩形动画
    [SerializeField] private bool enableBoolAnimation = true; //启用布尔值动画
    
    [Header("浮点数动画配置")]
    [SerializeField] private float floatStartValue = 0f; //浮点数起始值
    [SerializeField] private float floatEndValue = 100f; //浮点数结束值
    [SerializeField] private float floatAnimationDuration = 2f; //浮点数动画持续时间
    [SerializeField] private AnimationCurve floatAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); //浮点数动画曲线
    [SerializeField] private bool floatAnimationLooping = true; //浮点数动画循环
    [SerializeField] private bool floatAnimationPingPong = false; //浮点数动画乒乓
    [SerializeField] private int activeFloatIndex = 0; //活动浮点数索引
    [SerializeField] private bool enableFloatEasing = true; //启用浮点数缓动
    
    [Header("颜色动画配置")]
    [SerializeField] private Color colorStartValue = Color.red; //颜色起始值
    [SerializeField] private Color colorEndValue = Color.blue; //颜色结束值
    [SerializeField] private float colorAnimationDuration = 3f; //颜色动画持续时间
    [SerializeField] private AnimationCurve colorAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); //颜色动画曲线
    [SerializeField] private bool colorAnimationLooping = true; //颜色动画循环
    [SerializeField] private bool colorAnimationPingPong = false; //颜色动画乒乓
    [SerializeField] private int activeColorIndex = 0; //活动颜色索引
    [SerializeField] private bool enableColorInterpolation = true; //启用颜色插值
    
    [Header("向量动画配置")]
    [SerializeField] private Vector3 vectorStartValue = Vector3.zero; //向量起始值
    [SerializeField] private Vector3 vectorEndValue = Vector3.one * 10f; //向量结束值
    [SerializeField] private float vectorAnimationDuration = 2.5f; //向量动画持续时间
    [SerializeField] private AnimationCurve vectorAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); //向量动画曲线
    [SerializeField] private bool vectorAnimationLooping = true; //向量动画循环
    [SerializeField] private bool vectorAnimationPingPong = false; //向量动画乒乓
    [SerializeField] private int activeVectorIndex = 0; //活动向量索引
    [SerializeField] private bool enableVectorEasing = true; //启用向量缓动
    
    [Header("矩形动画配置")]
    [SerializeField] private Rect rectStartValue = new Rect(0, 0, 100, 100); //矩形起始值
    [SerializeField] private Rect rectEndValue = new Rect(200, 200, 300, 300); //矩形结束值
    [SerializeField] private float rectAnimationDuration = 2f; //矩形动画持续时间
    [SerializeField] private AnimationCurve rectAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); //矩形动画曲线
    [SerializeField] private bool rectAnimationLooping = true; //矩形动画循环
    [SerializeField] private bool rectAnimationPingPong = false; //矩形动画乒乓
    [SerializeField] private int activeRectIndex = 0; //活动矩形索引
    [SerializeField] private bool enableRectEasing = true; //启用矩形缓动
    
    [Header("布尔值动画配置")]
    [SerializeField] private bool boolStartValue = false; //布尔值起始值
    [SerializeField] private bool boolEndValue = true; //布尔值结束值
    [SerializeField] private float boolAnimationDuration = 1f; //布尔值动画持续时间
    [SerializeField] private AnimationCurve boolAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); //布尔值动画曲线
    [SerializeField] private bool boolAnimationLooping = true; //布尔值动画循环
    [SerializeField] private bool boolAnimationPingPong = false; //布尔值动画乒乓
    [SerializeField] private int activeBoolIndex = 0; //活动布尔值索引
    [SerializeField] private bool enableBoolEasing = true; //启用布尔值缓动
    
    [Header("动画值状态")]
    [SerializeField] private string animatedValuesState = "未初始化"; //动画值状态
    [SerializeField] private string currentAnimationMode = "空闲"; //当前动画模式
    [SerializeField] private bool isAnimationDirty = false; //动画是否脏
    [SerializeField] private bool isValueDirty = false; //值是否脏
    [SerializeField] private float animationTime = 0f; //动画时间
    [SerializeField] private float animationProgress = 0f; //动画进度
    [SerializeField] private bool animationPlaying = false; //动画播放状态
    [SerializeField] private bool animationPaused = false; //动画暂停状态
    
    [Header("性能监控")]
    [SerializeField] private bool enableAnimationMonitoring = true; //启用动画监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logAnimationData = false; //记录动画数据
    [SerializeField] private float animationUpdateTime = 0f; //动画更新时间
    [SerializeField] private int totalAnimations = 0; //总动画数
    [SerializeField] private int activeAnimations = 0; //活动动画数
    [SerializeField] private float memoryUsage = 0f; //内存使用量
    [SerializeField] private float frameRate = 60f; //帧率
    
    [Header("动画值数据")]
    [SerializeField] private AnimatedFloatData[] floatData; //浮点数数据
    [SerializeField] private AnimatedColorData[] colorData; //颜色数据
    [SerializeField] private AnimatedVectorData[] vectorData; //向量数据
    [SerializeField] private AnimatedRectData[] rectData; //矩形数据
    [SerializeField] private AnimatedBoolData[] boolData; //布尔值数据
    [SerializeField] private AnimationEventData[] eventData; //事件数据
    [SerializeField] private string[] animationLogs; //动画日志
    
    private System.Collections.Generic.List<AnimatedFloat> animatedFloats;
    private System.Collections.Generic.List<AnimatedColor> animatedColors;
    private System.Collections.Generic.List<AnimatedVector3> animatedVectors;
    private System.Collections.Generic.List<AnimatedRect> animatedRects;
    private System.Collections.Generic.List<AnimatedBool> animatedBools;
    private System.Collections.Generic.List<AnimatedFloatData> floatDataList;
    private System.Collections.Generic.List<AnimatedColorData> colorDataList;
    private System.Collections.Generic.List<AnimatedVectorData> vectorDataList;
    private System.Collections.Generic.List<AnimatedRectData> rectDataList;
    private System.Collections.Generic.List<AnimatedBoolData> boolDataList;
    private System.Collections.Generic.List<AnimationEventData> eventDataList;
    private System.Collections.Generic.List<string> animationLogList;
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;
    private float deltaTime = 0f;

    private void Start()
    {
        InitializeAnimatedValues();
    }

    /// <summary>
    /// 初始化动画值
    /// </summary>
    private void InitializeAnimatedValues()
    {
        // 初始化数据列表
        InitializeDataLists();
        
        // 初始化浮点数动画
        InitializeFloatAnimations();
        
        // 初始化颜色动画
        InitializeColorAnimations();
        
        // 初始化向量动画
        InitializeVectorAnimations();
        
        // 初始化矩形动画
        InitializeRectAnimations();
        
        // 初始化布尔值动画
        InitializeBoolAnimations();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置动画值
        ConfigureAnimatedValues();
        
        isInitialized = true;
        animatedValuesState = "已初始化";
        Debug.Log("动画值系统初始化完成");
    }

    /// <summary>
    /// 初始化数据列表
    /// </summary>
    private void InitializeDataLists()
    {
        animatedFloats = new System.Collections.Generic.List<AnimatedFloat>();
        animatedColors = new System.Collections.Generic.List<AnimatedColor>();
        animatedVectors = new System.Collections.Generic.List<AnimatedVector3>();
        animatedRects = new System.Collections.Generic.List<AnimatedRect>();
        animatedBools = new System.Collections.Generic.List<AnimatedBool>();
        floatDataList = new System.Collections.Generic.List<AnimatedFloatData>();
        colorDataList = new System.Collections.Generic.List<AnimatedColorData>();
        vectorDataList = new System.Collections.Generic.List<AnimatedVectorData>();
        rectDataList = new System.Collections.Generic.List<AnimatedRectData>();
        boolDataList = new System.Collections.Generic.List<AnimatedBoolData>();
        eventDataList = new System.Collections.Generic.List<AnimationEventData>();
        animationLogList = new System.Collections.Generic.List<string>();
        
        Debug.Log("数据列表初始化完成");
    }

    /// <summary>
    /// 初始化浮点数动画
    /// </summary>
    private void InitializeFloatAnimations()
    {
        if (!enableFloatAnimation) return;
        
        // 创建多个浮点数动画
        for (int i = 0; i < 5; i++)
        {
            var animatedFloat = new AnimatedFloat(floatStartValue + i * 10f);
            animatedFloat.speed = 1f / floatAnimationDuration;
            animatedFloat.valueChanged.AddListener(OnFloatValueChanged);
            animatedFloats.Add(animatedFloat);
            
            var floatData = new AnimatedFloatData
            {
                floatId = $"Float_{i}",
                floatName = $"浮点数动画_{i}",
                startValue = floatStartValue + i * 10f,
                endValue = floatEndValue + i * 10f,
                currentValue = animatedFloat.value,
                animationDuration = floatAnimationDuration,
                animationCurve = floatAnimationCurve,
                animationLooping = floatAnimationLooping,
                animationPingPong = floatAnimationPingPong,
                enableEasing = enableFloatEasing
            };
            floatDataList.Add(floatData);
        }
        
        Debug.Log("浮点数动画初始化完成");
    }

    /// <summary>
    /// 初始化颜色动画
    /// </summary>
    private void InitializeColorAnimations()
    {
        if (!enableColorAnimation) return;
        
        // 创建多个颜色动画
        Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta };
        for (int i = 0; i < 5; i++)
        {
            var animatedColor = new AnimatedColor(colors[i]);
            animatedColor.speed = 1f / colorAnimationDuration;
            animatedColor.valueChanged.AddListener(OnColorValueChanged);
            animatedColors.Add(animatedColor);
            
            var colorData = new AnimatedColorData
            {
                colorId = $"Color_{i}",
                colorName = $"颜色动画_{i}",
                startValue = colors[i],
                endValue = colors[(i + 1) % colors.Length],
                currentValue = animatedColor.value,
                animationDuration = colorAnimationDuration,
                animationCurve = colorAnimationCurve,
                animationLooping = colorAnimationLooping,
                animationPingPong = colorAnimationPingPong,
                enableInterpolation = enableColorInterpolation
            };
            colorDataList.Add(colorData);
        }
        
        Debug.Log("颜色动画初始化完成");
    }

    /// <summary>
    /// 初始化向量动画
    /// </summary>
    private void InitializeVectorAnimations()
    {
        if (!enableVectorAnimation) return;
        
        // 创建多个向量动画
        for (int i = 0; i < 5; i++)
        {
            var animatedVector = new AnimatedVector3(vectorStartValue + Vector3.one * i);
            animatedVector.speed = 1f / vectorAnimationDuration;
            animatedVector.valueChanged.AddListener(OnVectorValueChanged);
            animatedVectors.Add(animatedVector);
            
            var vectorData = new AnimatedVectorData
            {
                vectorId = $"Vector_{i}",
                vectorName = $"向量动画_{i}",
                startValue = vectorStartValue + Vector3.one * i,
                endValue = vectorEndValue + Vector3.one * i,
                currentValue = animatedVector.value,
                animationDuration = vectorAnimationDuration,
                animationCurve = vectorAnimationCurve,
                animationLooping = vectorAnimationLooping,
                animationPingPong = vectorAnimationPingPong,
                enableEasing = enableVectorEasing
            };
            vectorDataList.Add(vectorData);
        }
        
        Debug.Log("向量动画初始化完成");
    }

    /// <summary>
    /// 初始化矩形动画
    /// </summary>
    private void InitializeRectAnimations()
    {
        if (!enableRectAnimation) return;
        
        // 创建多个矩形动画
        for (int i = 0; i < 5; i++)
        {
            var animatedRect = new AnimatedRect(rectStartValue);
            animatedRect.speed = 1f / rectAnimationDuration;
            animatedRect.valueChanged.AddListener(OnRectValueChanged);
            animatedRects.Add(animatedRect);
            
            var rectData = new AnimatedRectData
            {
                rectId = $"Rect_{i}",
                rectName = $"矩形动画_{i}",
                startValue = rectStartValue,
                endValue = rectEndValue,
                currentValue = animatedRect.value,
                animationDuration = rectAnimationDuration,
                animationCurve = rectAnimationCurve,
                animationLooping = rectAnimationLooping,
                animationPingPong = rectAnimationPingPong,
                enableEasing = enableRectEasing
            };
            rectDataList.Add(rectData);
        }
        
        Debug.Log("矩形动画初始化完成");
    }

    /// <summary>
    /// 初始化布尔值动画
    /// </summary>
    private void InitializeBoolAnimations()
    {
        if (!enableBoolAnimation) return;
        
        // 创建多个布尔值动画
        for (int i = 0; i < 5; i++)
        {
            var animatedBool = new AnimatedBool(boolStartValue);
            animatedBool.speed = 1f / boolAnimationDuration;
            animatedBool.valueChanged.AddListener(OnBoolValueChanged);
            animatedBools.Add(animatedBool);
            
            var boolData = new AnimatedBoolData
            {
                boolId = $"Bool_{i}",
                boolName = $"布尔值动画_{i}",
                startValue = boolStartValue,
                endValue = boolEndValue,
                currentValue = animatedBool.value,
                animationDuration = boolAnimationDuration,
                animationCurve = boolAnimationCurve,
                animationLooping = boolAnimationLooping,
                animationPingPong = boolAnimationPingPong,
                enableEasing = enableBoolEasing
            };
            boolDataList.Add(boolData);
        }
        
        Debug.Log("布尔值动画初始化完成");
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        lastMonitoringTime = Time.time;
        Debug.Log("性能监控初始化完成");
    }

    /// <summary>
    /// 配置动画值
    /// </summary>
    private void ConfigureAnimatedValues()
    {
        // 配置浮点数动画
        ConfigureFloatAnimations();
        
        // 配置颜色动画
        ConfigureColorAnimations();
        
        // 配置向量动画
        ConfigureVectorAnimations();
        
        // 配置矩形动画
        ConfigureRectAnimations();
        
        // 配置布尔值动画
        ConfigureBoolAnimations();
        
        Debug.Log("动画值配置完成");
    }

    /// <summary>
    /// 配置浮点数动画
    /// </summary>
    private void ConfigureFloatAnimations()
    {
        for (int i = 0; i < animatedFloats.Count; i++)
        {
            var animatedFloat = animatedFloats[i];
            animatedFloat.speed = 1f / floatAnimationDuration;
            animatedFloat.value = floatStartValue + i * 10f;
        }
    }

    /// <summary>
    /// 配置颜色动画
    /// </summary>
    private void ConfigureColorAnimations()
    {
        for (int i = 0; i < animatedColors.Count; i++)
        {
            var animatedColor = animatedColors[i];
            animatedColor.speed = 1f / colorAnimationDuration;
        }
    }

    /// <summary>
    /// 配置向量动画
    /// </summary>
    private void ConfigureVectorAnimations()
    {
        for (int i = 0; i < animatedVectors.Count; i++)
        {
            var animatedVector = animatedVectors[i];
            animatedVector.speed = 1f / vectorAnimationDuration;
        }
    }

    /// <summary>
    /// 配置矩形动画
    /// </summary>
    private void ConfigureRectAnimations()
    {
        for (int i = 0; i < animatedRects.Count; i++)
        {
            var animatedRect = animatedRects[i];
            animatedRect.speed = 1f / rectAnimationDuration;
        }
    }

    /// <summary>
    /// 配置布尔值动画
    /// </summary>
    private void ConfigureBoolAnimations()
    {
        for (int i = 0; i < animatedBools.Count; i++)
        {
            var animatedBool = animatedBools[i];
            animatedBool.speed = 1f / boolAnimationDuration;
        }
    }

    private void Update()
    {
        if (!isInitialized || !enableAnimatedValues) return;
        
        deltaTime = Time.deltaTime;
        
        // 更新性能监控
        if (enableAnimationMonitoring && Time.time - lastMonitoringTime >= monitoringInterval)
        {
            MonitorAnimationPerformance();
            lastMonitoringTime = Time.time;
        }
        
        // 更新动画值
        UpdateAnimatedValues();
        
        // 更新动画状态
        UpdateAnimationStatus();
        
        // 处理动画输入
        HandleAnimationInput();
    }

    /// <summary>
    /// 监控动画性能
    /// </summary>
    private void MonitorAnimationPerformance()
    {
        totalAnimations = animatedFloats.Count + animatedColors.Count + animatedVectors.Count + animatedRects.Count + animatedBools.Count;
        activeAnimations = 0;
        
        // 计算活动动画数
        foreach (var animatedFloat in animatedFloats)
        {
            if (animatedFloat.isAnimating) activeAnimations++;
        }
        foreach (var animatedColor in animatedColors)
        {
            if (animatedColor.isAnimating) activeAnimations++;
        }
        foreach (var animatedVector in animatedVectors)
        {
            if (animatedVector.isAnimating) activeAnimations++;
        }
        foreach (var animatedRect in animatedRects)
        {
            if (animatedRect.isAnimating) activeAnimations++;
        }
        foreach (var animatedBool in animatedBools)
        {
            if (animatedBool.isAnimating) activeAnimations++;
        }
        
        memoryUsage = totalAnimations * 0.1f; // 估算内存使用量 (MB)
        frameRate = 1f / deltaTime;
        
        if (logAnimationData)
        {
            Debug.Log($"动画性能数据 - 总动画数: {totalAnimations}, 活动动画数: {activeAnimations}, 内存使用: {memoryUsage:F2}MB, 帧率: {frameRate:F1}FPS");
        }
    }

    /// <summary>
    /// 更新动画值
    /// </summary>
    private void UpdateAnimatedValues()
    {
        if (!animationPlaying || animationPaused) return;
        
        animationTime += deltaTime;
        animationProgress = (animationTime % 1f);
        
        // 更新浮点数动画
        UpdateFloatAnimations();
        
        // 更新颜色动画
        UpdateColorAnimations();
        
        // 更新向量动画
        UpdateVectorAnimations();
        
        // 更新矩形动画
        UpdateRectAnimations();
        
        // 更新布尔值动画
        UpdateBoolAnimations();
    }

    /// <summary>
    /// 更新浮点数动画
    /// </summary>
    private void UpdateFloatAnimations()
    {
        for (int i = 0; i < animatedFloats.Count; i++)
        {
            var animatedFloat = animatedFloats[i];
            if (animatedFloat.isAnimating)
            {
                float progress = floatAnimationCurve.Evaluate(animationProgress);
                float targetValue = Mathf.Lerp(floatStartValue + i * 10f, floatEndValue + i * 10f, progress);
                animatedFloat.target = targetValue;
                
                // 更新数据
                if (i < floatDataList.Count)
                {
                    floatDataList[i].currentValue = animatedFloat.value;
                }
            }
        }
    }

    /// <summary>
    /// 更新颜色动画
    /// </summary>
    private void UpdateColorAnimations()
    {
        for (int i = 0; i < animatedColors.Count; i++)
        {
            var animatedColor = animatedColors[i];
            if (animatedColor.isAnimating)
            {
                float progress = colorAnimationCurve.Evaluate(animationProgress);
                Color targetColor = Color.Lerp(colorStartValue, colorEndValue, progress);
                animatedColor.target = targetColor;
                
                // 更新数据
                if (i < colorDataList.Count)
                {
                    colorDataList[i].currentValue = animatedColor.value;
                }
            }
        }
    }

    /// <summary>
    /// 更新向量动画
    /// </summary>
    private void UpdateVectorAnimations()
    {
        for (int i = 0; i < animatedVectors.Count; i++)
        {
            var animatedVector = animatedVectors[i];
            if (animatedVector.isAnimating)
            {
                float progress = vectorAnimationCurve.Evaluate(animationProgress);
                Vector3 targetVector = Vector3.Lerp(vectorStartValue + Vector3.one * i, vectorEndValue + Vector3.one * i, progress);
                animatedVector.target = targetVector;
                
                // 更新数据
                if (i < vectorDataList.Count)
                {
                    vectorDataList[i].currentValue = animatedVector.value;
                }
            }
        }
    }

    /// <summary>
    /// 更新矩形动画
    /// </summary>
    private void UpdateRectAnimations()
    {
        for (int i = 0; i < animatedRects.Count; i++)
        {
            var animatedRect = animatedRects[i];
            if (animatedRect.isAnimating)
            {
                float progress = rectAnimationCurve.Evaluate(animationProgress);
                Rect targetRect = new Rect(
                    Mathf.Lerp(rectStartValue.x, rectEndValue.x, progress),
                    Mathf.Lerp(rectStartValue.y, rectEndValue.y, progress),
                    Mathf.Lerp(rectStartValue.width, rectEndValue.width, progress),
                    Mathf.Lerp(rectStartValue.height, rectEndValue.height, progress)
                );
                animatedRect.target = targetRect;
                
                // 更新数据
                if (i < rectDataList.Count)
                {
                    rectDataList[i].currentValue = animatedRect.value;
                }
            }
        }
    }

    /// <summary>
    /// 更新布尔值动画
    /// </summary>
    private void UpdateBoolAnimations()
    {
        for (int i = 0; i < animatedBools.Count; i++)
        {
            var animatedBool = animatedBools[i];
            if (animatedBool.isAnimating)
            {
                float progress = boolAnimationCurve.Evaluate(animationProgress);
                bool targetBool = progress > 0.5f ? boolEndValue : boolStartValue;
                animatedBool.target = targetBool;
                
                // 更新数据
                if (i < boolDataList.Count)
                {
                    boolDataList[i].currentValue = animatedBool.value;
                }
            }
        }
    }

    /// <summary>
    /// 更新动画状态
    /// </summary>
    private void UpdateAnimationStatus()
    {
        if (animationPlaying)
        {
            currentAnimationMode = animationPaused ? "暂停" : "播放";
        }
        else
        {
            currentAnimationMode = "停止";
        }
    }

    /// <summary>
    /// 处理动画输入
    /// </summary>
    private void HandleAnimationInput()
    {
        // 处理键盘快捷键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleAnimationPlayback();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleAnimationPause();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAnimations();
        }
        
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleFloatAnimation();
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleColorAnimation();
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ToggleVectorAnimation();
        }
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            ToggleRectAnimation();
        }
        
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ToggleBoolAnimation();
        }
    }

    /// <summary>
    /// 切换动画播放
    /// </summary>
    public void ToggleAnimationPlayback()
    {
        animationPlaying = !animationPlaying;
        LogAnimationEvent("切换动画播放", animationPlaying ? "播放" : "停止");
    }

    /// <summary>
    /// 切换动画暂停
    /// </summary>
    public void ToggleAnimationPause()
    {
        animationPaused = !animationPaused;
        LogAnimationEvent("切换动画暂停", animationPaused ? "暂停" : "继续");
    }

    /// <summary>
    /// 重置动画
    /// </summary>
    public void ResetAnimations()
    {
        animationTime = 0f;
        animationProgress = 0f;
        
        // 重置所有动画值
        foreach (var animatedFloat in animatedFloats)
        {
            animatedFloat.value = floatStartValue;
        }
        foreach (var animatedColor in animatedColors)
        {
            animatedColor.value = colorStartValue;
        }
        foreach (var animatedVector in animatedVectors)
        {
            animatedVector.value = vectorStartValue;
        }
        foreach (var animatedRect in animatedRects)
        {
            animatedRect.value = rectStartValue;
        }
        foreach (var animatedBool in animatedBools)
        {
            animatedBool.value = boolStartValue;
        }
        
        LogAnimationEvent("重置动画", "所有动画已重置");
    }

    /// <summary>
    /// 切换浮点数动画
    /// </summary>
    public void ToggleFloatAnimation()
    {
        enableFloatAnimation = !enableFloatAnimation;
        LogAnimationEvent("切换浮点数动画", enableFloatAnimation ? "启用" : "禁用");
    }

    /// <summary>
    /// 切换颜色动画
    /// </summary>
    public void ToggleColorAnimation()
    {
        enableColorAnimation = !enableColorAnimation;
        LogAnimationEvent("切换颜色动画", enableColorAnimation ? "启用" : "禁用");
    }

    /// <summary>
    /// 切换向量动画
    /// </summary>
    public void ToggleVectorAnimation()
    {
        enableVectorAnimation = !enableVectorAnimation;
        LogAnimationEvent("切换向量动画", enableVectorAnimation ? "启用" : "禁用");
    }

    /// <summary>
    /// 切换矩形动画
    /// </summary>
    public void ToggleRectAnimation()
    {
        enableRectAnimation = !enableRectAnimation;
        LogAnimationEvent("切换矩形动画", enableRectAnimation ? "启用" : "禁用");
    }

    /// <summary>
    /// 切换布尔值动画
    /// </summary>
    public void ToggleBoolAnimation()
    {
        enableBoolAnimation = !enableBoolAnimation;
        LogAnimationEvent("切换布尔值动画", enableBoolAnimation ? "启用" : "禁用");
    }

    /// <summary>
    /// 设置活动浮点数索引
    /// </summary>
    public void SetActiveFloatIndex(int index)
    {
        if (animatedFloats != null && index >= 0 && index < animatedFloats.Count)
        {
            activeFloatIndex = index;
            LogAnimationEvent("设置活动浮点数索引", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动颜色索引
    /// </summary>
    public void SetActiveColorIndex(int index)
    {
        if (animatedColors != null && index >= 0 && index < animatedColors.Count)
        {
            activeColorIndex = index;
            LogAnimationEvent("设置活动颜色索引", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动向量索引
    /// </summary>
    public void SetActiveVectorIndex(int index)
    {
        if (animatedVectors != null && index >= 0 && index < animatedVectors.Count)
        {
            activeVectorIndex = index;
            LogAnimationEvent("设置活动向量索引", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动矩形索引
    /// </summary>
    public void SetActiveRectIndex(int index)
    {
        if (animatedRects != null && index >= 0 && index < animatedRects.Count)
        {
            activeRectIndex = index;
            LogAnimationEvent("设置活动矩形索引", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动布尔值索引
    /// </summary>
    public void SetActiveBoolIndex(int index)
    {
        if (animatedBools != null && index >= 0 && index < animatedBools.Count)
        {
            activeBoolIndex = index;
            LogAnimationEvent("设置活动布尔值索引", $"索引_{index}");
        }
    }

    /// <summary>
    /// 生成动画报告
    /// </summary>
    public void GenerateAnimationReport()
    {
        AnimationReportData reportData = new AnimationReportData
        {
            timestamp = System.DateTime.Now.ToString(),
            animatedValuesState = animatedValuesState,
            currentAnimationMode = currentAnimationMode,
            animationTime = animationTime,
            animationProgress = animationProgress,
            animationPlaying = animationPlaying,
            animationPaused = animationPaused,
            totalAnimations = totalAnimations,
            activeAnimations = activeAnimations,
            memoryUsage = memoryUsage,
            frameRate = frameRate,
            enableFloatAnimation = enableFloatAnimation,
            enableColorAnimation = enableColorAnimation,
            enableVectorAnimation = enableVectorAnimation,
            enableRectAnimation = enableRectAnimation,
            enableBoolAnimation = enableBoolAnimation
        };
        
        string reportJson = JsonUtility.ToJson(reportData, true);
        Debug.Log($"动画报告生成完成:\n{reportJson}");
    }

    /// <summary>
    /// 导出动画数据
    /// </summary>
    public void ExportAnimationData()
    {
        // 导出浮点数数据
        floatData = floatDataList.ToArray();
        
        // 导出颜色数据
        colorData = colorDataList.ToArray();
        
        // 导出向量数据
        vectorData = vectorDataList.ToArray();
        
        // 导出矩形数据
        rectData = rectDataList.ToArray();
        
        // 导出布尔值数据
        boolData = boolDataList.ToArray();
        
        // 导出事件数据
        eventData = eventDataList.ToArray();
        
        // 导出日志数据
        animationLogs = animationLogList.ToArray();
        
        Debug.Log("动画数据导出完成");
    }

    // 事件处理函数
    private void OnFloatValueChanged(float newValue)
    {
        LogAnimationEvent("浮点数值改变", $"新值: {newValue:F2}");
    }

    private void OnColorValueChanged(Color newValue)
    {
        LogAnimationEvent("颜色值改变", $"新值: {newValue}");
    }

    private void OnVectorValueChanged(Vector3 newValue)
    {
        LogAnimationEvent("向量值改变", $"新值: {newValue}");
    }

    private void OnRectValueChanged(Rect newValue)
    {
        LogAnimationEvent("矩形值改变", $"新值: {newValue}");
    }

    private void OnBoolValueChanged(bool newValue)
    {
        LogAnimationEvent("布尔值改变", $"新值: {newValue}");
    }

    /// <summary>
    /// 记录动画事件
    /// </summary>
    private void LogAnimationEvent(string eventType, string eventData)
    {
        var eventInfo = new AnimationEventData
        {
            eventId = System.DateTime.Now.Ticks,
            timestamp = System.DateTime.Now.ToString(),
            eventType = eventType,
            eventData = eventData
        };
        
        eventDataList.Add(eventInfo);
        animationLogList.Add($"[{eventInfo.timestamp}] {eventInfo.eventType}: {eventInfo.eventData}");
        
        // 限制日志数量
        if (animationLogList.Count > 100)
        {
            animationLogList.RemoveAt(0);
        }
    }

    private void OnGUI()
    {
        if (!isInitialized) return;
        
        GUILayout.BeginArea(new Rect(10, 10, 300, Screen.height - 20));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label("动画值系统", EditorStyles.boldLabel);
        
        // 动画值配置
        GUILayout.Space(10);
        GUILayout.Label("动画配置", EditorStyles.boldLabel);
        enableAnimatedValues = GUILayout.Toggle(enableAnimatedValues, "启用动画值");
        enableFloatAnimation = GUILayout.Toggle(enableFloatAnimation, "启用浮点数动画");
        enableColorAnimation = GUILayout.Toggle(enableColorAnimation, "启用颜色动画");
        enableVectorAnimation = GUILayout.Toggle(enableVectorAnimation, "启用向量动画");
        enableRectAnimation = GUILayout.Toggle(enableRectAnimation, "启用矩形动画");
        enableBoolAnimation = GUILayout.Toggle(enableBoolAnimation, "启用布尔值动画");
        
        // 动画状态
        GUILayout.Space(10);
        GUILayout.Label("动画状态", EditorStyles.boldLabel);
        GUILayout.Label($"状态: {animatedValuesState}");
        GUILayout.Label($"模式: {currentAnimationMode}");
        GUILayout.Label($"时间: {animationTime:F2}s");
        GUILayout.Label($"进度: {animationProgress:F2}");
        GUILayout.Label($"播放: {animationPlaying}");
        GUILayout.Label($"暂停: {animationPaused}");
        GUILayout.Label($"总动画数: {totalAnimations}");
        GUILayout.Label($"活动动画数: {activeAnimations}");
        GUILayout.Label($"内存使用: {memoryUsage:F2}MB");
        GUILayout.Label($"帧率: {frameRate:F1}FPS");
        
        // 动画操作
        GUILayout.Space(10);
        GUILayout.Label("动画操作", EditorStyles.boldLabel);
        if (GUILayout.Button("切换播放")) ToggleAnimationPlayback();
        if (GUILayout.Button("切换暂停")) ToggleAnimationPause();
        if (GUILayout.Button("重置动画")) ResetAnimations();
        if (GUILayout.Button("切换浮点数")) ToggleFloatAnimation();
        if (GUILayout.Button("切换颜色")) ToggleColorAnimation();
        if (GUILayout.Button("切换向量")) ToggleVectorAnimation();
        if (GUILayout.Button("切换矩形")) ToggleRectAnimation();
        if (GUILayout.Button("切换布尔值")) ToggleBoolAnimation();
        
        // 动画数据
        GUILayout.Space(10);
        GUILayout.Label("动画数据", EditorStyles.boldLabel);
        if (GUILayout.Button("生成报告")) GenerateAnimationReport();
        if (GUILayout.Button("导出数据")) ExportAnimationData();
        if (GUILayout.Button("清空日志")) animationLogList.Clear();
        
        // 快捷键提示
        GUILayout.Space(10);
        GUILayout.Label("快捷键", EditorStyles.boldLabel);
        GUILayout.Label("空格: 切换播放");
        GUILayout.Label("P: 切换暂停");
        GUILayout.Label("R: 重置动画");
        GUILayout.Label("F1: 切换浮点数");
        GUILayout.Label("F2: 切换颜色");
        GUILayout.Label("F3: 切换向量");
        GUILayout.Label("F4: 切换矩形");
        GUILayout.Label("F5: 切换布尔值");
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}

/// <summary>
/// 浮点数数据
/// </summary>
[System.Serializable]
public class AnimatedFloatData
{
    public string floatId;
    public string floatName;
    public float startValue;
    public float endValue;
    public float currentValue;
    public float animationDuration;
    public AnimationCurve animationCurve;
    public bool animationLooping;
    public bool animationPingPong;
    public bool enableEasing;
}

/// <summary>
/// 颜色数据
/// </summary>
[System.Serializable]
public class AnimatedColorData
{
    public string colorId;
    public string colorName;
    public Color startValue;
    public Color endValue;
    public Color currentValue;
    public float animationDuration;
    public AnimationCurve animationCurve;
    public bool animationLooping;
    public bool animationPingPong;
    public bool enableInterpolation;
}

/// <summary>
/// 向量数据
/// </summary>
[System.Serializable]
public class AnimatedVectorData
{
    public string vectorId;
    public string vectorName;
    public Vector3 startValue;
    public Vector3 endValue;
    public Vector3 currentValue;
    public float animationDuration;
    public AnimationCurve animationCurve;
    public bool animationLooping;
    public bool animationPingPong;
    public bool enableEasing;
}

/// <summary>
/// 矩形数据
/// </summary>
[System.Serializable]
public class AnimatedRectData
{
    public string rectId;
    public string rectName;
    public Rect startValue;
    public Rect endValue;
    public Rect currentValue;
    public float animationDuration;
    public AnimationCurve animationCurve;
    public bool animationLooping;
    public bool animationPingPong;
    public bool enableEasing;
}

/// <summary>
/// 布尔值数据
/// </summary>
[System.Serializable]
public class AnimatedBoolData
{
    public string boolId;
    public string boolName;
    public bool startValue;
    public bool endValue;
    public bool currentValue;
    public float animationDuration;
    public AnimationCurve animationCurve;
    public bool animationLooping;
    public bool animationPingPong;
    public bool enableEasing;
}

/// <summary>
/// 动画事件数据
/// </summary>
[System.Serializable]
public class AnimationEventData
{
    public long eventId;
    public string timestamp;
    public string eventType;
    public string eventData;
}

/// <summary>
/// 动画报告数据
/// </summary>
[System.Serializable]
public class AnimationReportData
{
    public string timestamp;
    public string animatedValuesState;
    public string currentAnimationMode;
    public float animationTime;
    public float animationProgress;
    public bool animationPlaying;
    public bool animationPaused;
    public int totalAnimations;
    public int activeAnimations;
    public float memoryUsage;
    public float frameRate;
    public bool enableFloatAnimation;
    public bool enableColorAnimation;
    public bool enableVectorAnimation;
    public bool enableRectAnimation;
    public bool enableBoolAnimation;
} 