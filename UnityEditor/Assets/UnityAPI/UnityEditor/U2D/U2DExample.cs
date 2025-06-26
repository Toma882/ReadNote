using UnityEngine;
using UnityEditor;
using UnityEditor.U2D;

/// <summary>
/// UnityEditor.U2D 命名空间案例演示
/// 展示2D编辑器系统的核心功能，包括精灵编辑器、图集管理、2D动画等
/// </summary>
public class U2DExample : MonoBehaviour
{
    [Header("2D编辑器配置")]
    [SerializeField] private bool enableU2D = true; //启用2D编辑器
    [SerializeField] private bool enableSpriteEditor = true; //启用精灵编辑器
    [SerializeField] private bool enableAtlasEditor = true; //启用图集编辑器
    [SerializeField] private bool enableAnimationEditor = true; //启用动画编辑器
    [SerializeField] private bool enablePhysicsEditor = true; //启用物理编辑器
    [SerializeField] private bool enableShapeEditor = true; //启用形状编辑器
    
    [Header("精灵编辑器配置")]
    [SerializeField] private Texture2D[] spriteTextures; //精灵纹理数组
    [SerializeField] private Vector2[] spritePivots; //精灵轴心点数组
    [SerializeField] private Vector2[] spriteSizes; //精灵大小数组
    [SerializeField] private SpriteAlignment[] spriteAlignments; //精灵对齐方式数组
    [SerializeField] private Vector2[] spriteOffsets; //精灵偏移数组
    [SerializeField] private bool[] spriteGeneratePhysics; //精灵生成物理数组
    [SerializeField] private int activeSpriteIndex = 0; //活动精灵索引
    [SerializeField] private bool enableSpriteSlicing = true; //启用精灵切片
    [SerializeField] private Vector2 sliceSize = new Vector2(64, 64); //切片大小
    [SerializeField] private Vector2 sliceOffset = Vector2.zero; //切片偏移
    [SerializeField] private Vector2 slicePadding = Vector2.zero; //切片间距
    
    [Header("图集编辑器配置")]
    [SerializeField] private Texture2D[] atlasTextures; //图集纹理数组
    [SerializeField] private int atlasMaxSize = 2048; //图集最大大小
    [SerializeField] private int atlasPadding = 2; //图集间距
    [SerializeField] private bool atlasAllowRotation = false; //图集允许旋转
    [SerializeField] private bool atlasSquarePacking = true; //图集方形打包
    [SerializeField] private bool atlasTightPacking = true; //图集紧密打包
    [SerializeField] private string atlasName = "CustomAtlas"; //图集名称
    [SerializeField] private int activeAtlasIndex = 0; //活动图集索引
    [SerializeField] private bool enableAtlasGeneration = true; //启用图集生成
    
    [Header("动画编辑器配置")]
    [SerializeField] private Sprite[] animationSprites; //动画精灵数组
    [SerializeField] private float[] animationFrameRates; //动画帧率数组
    [SerializeField] private bool[] animationLooping; //动画循环数组
    [SerializeField] private string[] animationNames; //动画名称数组
    [SerializeField] private int activeAnimationIndex = 0; //活动动画索引
    [SerializeField] private bool enableAnimationPreview = true; //启用动画预览
    [SerializeField] private float animationSpeed = 1f; //动画速度
    [SerializeField] private bool animationPlaying = false; //动画播放状态
    
    [Header("物理编辑器配置")]
    [SerializeField] private Sprite[] physicsSprites; //物理精灵数组
    [SerializeField] private PhysicsShape2D[] physicsShapes; //物理形状数组
    [SerializeField] private bool[] physicsEnabled; //物理启用数组
    [SerializeField] private float[] physicsDensity; //物理密度数组
    [SerializeField] private float[] physicsFriction; //物理摩擦数组
    [SerializeField] private float[] physicsBounciness; //物理弹性数组
    [SerializeField] private int activePhysicsIndex = 0; //活动物理索引
    [SerializeField] private bool enablePhysicsPreview = true; //启用物理预览
    
    [Header("形状编辑器配置")]
    [SerializeField] private Vector2[] shapePoints; //形状点数组
    [SerializeField] private bool[] shapeClosed; //形状闭合数组
    [SerializeField] private float[] shapeRadius; //形状半径数组
    [SerializeField] private int activeShapeIndex = 0; //活动形状索引
    [SerializeField] private bool enableShapePreview = true; //启用形状预览
    [SerializeField] private Color shapeColor = Color.green; //形状颜色
    [SerializeField] private float shapeThickness = 2f; //形状厚度
    
    [Header("2D编辑器状态")]
    [SerializeField] private string u2dState = "未初始化"; //2D编辑器状态
    [SerializeField] private string currentEditorMode = "空闲"; //当前编辑器模式
    [SerializeField] private bool isSpriteDirty = false; //精灵是否脏
    [SerializeField] private bool isAtlasDirty = false; //图集是否脏
    [SerializeField] private bool isAnimationDirty = false; //动画是否脏
    [SerializeField] private bool isPhysicsDirty = false; //物理是否脏
    [SerializeField] private Vector2 editorSize = Vector2.zero; //编辑器大小
    [SerializeField] private Vector2 editorPosition = Vector2.zero; //编辑器位置
    
    [Header("性能监控")]
    [SerializeField] private bool enableU2DMonitoring = true; //启用2D编辑器监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logU2DData = false; //记录2D编辑器数据
    [SerializeField] private float editorUpdateTime = 0f; //编辑器更新时间
    [SerializeField] private int totalSprites = 0; //总精灵数
    [SerializeField] private int totalAtlases = 0; //总图集数
    [SerializeField] private int totalAnimations = 0; //总动画数
    [SerializeField] private float memoryUsage = 0f; //内存使用量
    
    [Header("2D编辑器数据")]
    [SerializeField] private SpriteData[] spriteData; //精灵数据
    [SerializeField] private AtlasData[] atlasData; //图集数据
    [SerializeField] private AnimationData[] animationData; //动画数据
    [SerializeField] private PhysicsData[] physicsData; //物理数据
    [SerializeField] private ShapeData[] shapeData; //形状数据
    [SerializeField] private string[] editorLogs; //编辑器日志
    
    private SpriteEditor spriteEditor;
    private AtlasEditor atlasEditor;
    private AnimationEditor animationEditor;
    private PhysicsEditor physicsEditor;
    private ShapeEditor shapeEditor;
    private System.Collections.Generic.List<SpriteData> spriteDataList;
    private System.Collections.Generic.List<AtlasData> atlasDataList;
    private System.Collections.Generic.List<AnimationData> animationDataList;
    private System.Collections.Generic.List<PhysicsData> physicsDataList;
    private System.Collections.Generic.List<ShapeData> shapeDataList;
    private System.Collections.Generic.List<string> editorLogList;
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;
    private float animationTime = 0f;
    private int currentFrame = 0;

    private void Start()
    {
        InitializeU2D();
    }

    /// <summary>
    /// 初始化2D编辑器
    /// </summary>
    private void InitializeU2D()
    {
        // 初始化数据列表
        InitializeDataLists();
        
        // 初始化精灵编辑器
        InitializeSpriteEditor();
        
        // 初始化图集编辑器
        InitializeAtlasEditor();
        
        // 初始化动画编辑器
        InitializeAnimationEditor();
        
        // 初始化物理编辑器
        InitializePhysicsEditor();
        
        // 初始化形状编辑器
        InitializeShapeEditor();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置2D编辑器
        ConfigureU2D();
        
        isInitialized = true;
        u2dState = "已初始化";
        Debug.Log("2D编辑器系统初始化完成");
    }

    /// <summary>
    /// 初始化数据列表
    /// </summary>
    private void InitializeDataLists()
    {
        spriteDataList = new System.Collections.Generic.List<SpriteData>();
        atlasDataList = new System.Collections.Generic.List<AtlasData>();
        animationDataList = new System.Collections.Generic.List<AnimationData>();
        physicsDataList = new System.Collections.Generic.List<PhysicsData>();
        shapeDataList = new System.Collections.Generic.List<ShapeData>();
        editorLogList = new System.Collections.Generic.List<string>();
        
        Debug.Log("数据列表初始化完成");
    }

    /// <summary>
    /// 初始化精灵编辑器
    /// </summary>
    private void InitializeSpriteEditor()
    {
        if (!enableSpriteEditor) return;
        
        spriteEditor = new SpriteEditor();
        
        // 创建默认精灵数据
        if (spriteTextures != null && spriteTextures.Length > 0)
        {
            for (int i = 0; i < spriteTextures.Length; i++)
            {
                var spriteData = new SpriteData
                {
                    spriteId = $"Sprite_{i}",
                    spriteName = $"精灵_{i}",
                    spriteTexture = spriteTextures[i],
                    spritePivot = spritePivots != null && i < spritePivots.Length ? spritePivots[i] : new Vector2(0.5f, 0.5f),
                    spriteSize = spriteSizes != null && i < spriteSizes.Length ? spriteSizes[i] : Vector2.one * 64f,
                    spriteAlignment = spriteAlignments != null && i < spriteAlignments.Length ? spriteAlignments[i] : SpriteAlignment.Center,
                    spriteOffset = spriteOffsets != null && i < spriteOffsets.Length ? spriteOffsets[i] : Vector2.zero,
                    spriteGeneratePhysics = spriteGeneratePhysics != null && i < spriteGeneratePhysics.Length ? spriteGeneratePhysics[i] : false
                };
                spriteDataList.Add(spriteData);
            }
        }
        
        Debug.Log("精灵编辑器初始化完成");
    }

    /// <summary>
    /// 初始化图集编辑器
    /// </summary>
    private void InitializeAtlasEditor()
    {
        if (!enableAtlasEditor) return;
        
        atlasEditor = new AtlasEditor();
        
        // 创建默认图集数据
        if (atlasTextures != null && atlasTextures.Length > 0)
        {
            for (int i = 0; i < atlasTextures.Length; i++)
            {
                var atlasData = new AtlasData
                {
                    atlasId = $"Atlas_{i}",
                    atlasName = $"图集_{i}",
                    atlasTexture = atlasTextures[i],
                    atlasMaxSize = atlasMaxSize,
                    atlasPadding = atlasPadding,
                    atlasAllowRotation = atlasAllowRotation,
                    atlasSquarePacking = atlasSquarePacking,
                    atlasTightPacking = atlasTightPacking
                };
                atlasDataList.Add(atlasData);
            }
        }
        
        Debug.Log("图集编辑器初始化完成");
    }

    /// <summary>
    /// 初始化动画编辑器
    /// </summary>
    private void InitializeAnimationEditor()
    {
        if (!enableAnimationEditor) return;
        
        animationEditor = new AnimationEditor();
        
        // 创建默认动画数据
        if (animationSprites != null && animationSprites.Length > 0)
        {
            for (int i = 0; i < animationSprites.Length; i++)
            {
                var animationData = new AnimationData
                {
                    animationId = $"Animation_{i}",
                    animationName = animationNames != null && i < animationNames.Length ? animationNames[i] : $"动画_{i}",
                    animationSprites = new Sprite[] { animationSprites[i] },
                    animationFrameRate = animationFrameRates != null && i < animationFrameRates.Length ? animationFrameRates[i] : 12f,
                    animationLooping = animationLooping != null && i < animationLooping.Length ? animationLooping[i] : true,
                    animationSpeed = animationSpeed
                };
                animationDataList.Add(animationData);
            }
        }
        
        Debug.Log("动画编辑器初始化完成");
    }

    /// <summary>
    /// 初始化物理编辑器
    /// </summary>
    private void InitializePhysicsEditor()
    {
        if (!enablePhysicsEditor) return;
        
        physicsEditor = new PhysicsEditor();
        
        // 创建默认物理数据
        if (physicsSprites != null && physicsSprites.Length > 0)
        {
            for (int i = 0; i < physicsSprites.Length; i++)
            {
                var physicsData = new PhysicsData
                {
                    physicsId = $"Physics_{i}",
                    physicsName = $"物理_{i}",
                    physicsSprite = physicsSprites[i],
                    physicsShape = physicsShapes != null && i < physicsShapes.Length ? physicsShapes[i] : PhysicsShape2D.Box,
                    physicsEnabled = physicsEnabled != null && i < physicsEnabled.Length ? physicsEnabled[i] : true,
                    physicsDensity = physicsDensity != null && i < physicsDensity.Length ? physicsDensity[i] : 1f,
                    physicsFriction = physicsFriction != null && i < physicsFriction.Length ? physicsFriction[i] : 0.2f,
                    physicsBounciness = physicsBounciness != null && i < physicsBounciness.Length ? physicsBounciness[i] : 0f
                };
                physicsDataList.Add(physicsData);
            }
        }
        
        Debug.Log("物理编辑器初始化完成");
    }

    /// <summary>
    /// 初始化形状编辑器
    /// </summary>
    private void InitializeShapeEditor()
    {
        if (!enableShapeEditor) return;
        
        shapeEditor = new ShapeEditor();
        
        // 创建默认形状数据
        for (int i = 0; i < 5; i++)
        {
            var shapeData = new ShapeData
            {
                shapeId = $"Shape_{i}",
                shapeName = $"形状_{i}",
                shapePoints = new Vector2[] { Vector2.zero, Vector2.right, Vector2.up, Vector2.one },
                shapeClosed = shapeClosed != null && i < shapeClosed.Length ? shapeClosed[i] : true,
                shapeRadius = shapeRadius != null && i < shapeRadius.Length ? shapeRadius[i] : 1f,
                shapeColor = shapeColor,
                shapeThickness = shapeThickness
            };
            shapeDataList.Add(shapeData);
        }
        
        Debug.Log("形状编辑器初始化完成");
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
    /// 配置2D编辑器
    /// </summary>
    private void ConfigureU2D()
    {
        // 配置精灵编辑器
        ConfigureSpriteEditor();
        
        // 配置图集编辑器
        ConfigureAtlasEditor();
        
        // 配置动画编辑器
        ConfigureAnimationEditor();
        
        // 配置物理编辑器
        ConfigurePhysicsEditor();
        
        // 配置形状编辑器
        ConfigureShapeEditor();
        
        Debug.Log("2D编辑器配置完成");
    }

    /// <summary>
    /// 配置精灵编辑器
    /// </summary>
    private void ConfigureSpriteEditor()
    {
        if (spriteEditor != null)
        {
            spriteEditor.enableSlicing = enableSpriteSlicing;
            spriteEditor.sliceSize = sliceSize;
            spriteEditor.sliceOffset = sliceOffset;
            spriteEditor.slicePadding = slicePadding;
        }
    }

    /// <summary>
    /// 配置图集编辑器
    /// </summary>
    private void ConfigureAtlasEditor()
    {
        if (atlasEditor != null)
        {
            atlasEditor.maxSize = atlasMaxSize;
            atlasEditor.padding = atlasPadding;
            atlasEditor.allowRotation = atlasAllowRotation;
            atlasEditor.squarePacking = atlasSquarePacking;
            atlasEditor.tightPacking = atlasTightPacking;
        }
    }

    /// <summary>
    /// 配置动画编辑器
    /// </summary>
    private void ConfigureAnimationEditor()
    {
        if (animationEditor != null)
        {
            animationEditor.enablePreview = enableAnimationPreview;
            animationEditor.animationSpeed = animationSpeed;
        }
    }

    /// <summary>
    /// 配置物理编辑器
    /// </summary>
    private void ConfigurePhysicsEditor()
    {
        if (physicsEditor != null)
        {
            physicsEditor.enablePreview = enablePhysicsPreview;
        }
    }

    /// <summary>
    /// 配置形状编辑器
    /// </summary>
    private void ConfigureShapeEditor()
    {
        if (shapeEditor != null)
        {
            shapeEditor.enablePreview = enableShapePreview;
            shapeEditor.shapeColor = shapeColor;
            shapeEditor.shapeThickness = shapeThickness;
        }
    }

    private void Update()
    {
        if (!isInitialized || !enableU2D) return;
        
        // 更新性能监控
        if (enableU2DMonitoring && Time.time - lastMonitoringTime >= monitoringInterval)
        {
            MonitorU2DPerformance();
            lastMonitoringTime = Time.time;
        }
        
        // 更新编辑器状态
        UpdateEditorStatus();
        
        // 更新动画编辑器
        UpdateAnimationEditor();
        
        // 处理编辑器输入
        HandleEditorInput();
    }

    /// <summary>
    /// 监控2D编辑器性能
    /// </summary>
    private void MonitorU2DPerformance()
    {
        totalSprites = spriteDataList != null ? spriteDataList.Count : 0;
        totalAtlases = atlasDataList != null ? atlasDataList.Count : 0;
        totalAnimations = animationDataList != null ? animationDataList.Count : 0;
        memoryUsage = (totalSprites + totalAtlases + totalAnimations) * 0.1f; // 估算内存使用量 (MB)
        
        if (logU2DData)
        {
            Debug.Log($"2D编辑器性能数据 - 精灵数: {totalSprites}, 图集数: {totalAtlases}, 动画数: {totalAnimations}, 内存使用: {memoryUsage:F2}MB");
        }
    }

    /// <summary>
    /// 更新编辑器状态
    /// </summary>
    private void UpdateEditorStatus()
    {
        if (spriteEditor != null)
        {
            editorSize = spriteEditor.size;
            editorPosition = spriteEditor.position;
        }
    }

    /// <summary>
    /// 更新动画编辑器
    /// </summary>
    private void UpdateAnimationEditor()
    {
        if (!enableAnimationPreview || !animationPlaying) return;
        
        animationTime += Time.deltaTime * animationSpeed;
        
        if (animationDataList != null && activeAnimationIndex < animationDataList.Count)
        {
            var animationData = animationDataList[activeAnimationIndex];
            float frameTime = 1f / animationData.animationFrameRate;
            currentFrame = Mathf.FloorToInt(animationTime / frameTime) % animationData.animationSprites.Length;
        }
    }

    /// <summary>
    /// 处理编辑器输入
    /// </summary>
    private void HandleEditorInput()
    {
        // 处理键盘快捷键
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleSpriteEditor();
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleAtlasEditor();
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ToggleAnimationEditor();
        }
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            TogglePhysicsEditor();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleAnimationPlayback();
        }
    }

    /// <summary>
    /// 切换精灵编辑器
    /// </summary>
    public void ToggleSpriteEditor()
    {
        enableSpriteEditor = !enableSpriteEditor;
        LogEditorEvent("切换精灵编辑器", enableSpriteEditor ? "启用" : "禁用");
    }

    /// <summary>
    /// 切换图集编辑器
    /// </summary>
    public void ToggleAtlasEditor()
    {
        enableAtlasEditor = !enableAtlasEditor;
        LogEditorEvent("切换图集编辑器", enableAtlasEditor ? "启用" : "禁用");
    }

    /// <summary>
    /// 切换动画编辑器
    /// </summary>
    public void ToggleAnimationEditor()
    {
        enableAnimationEditor = !enableAnimationEditor;
        LogEditorEvent("切换动画编辑器", enableAnimationEditor ? "启用" : "禁用");
    }

    /// <summary>
    /// 切换物理编辑器
    /// </summary>
    public void TogglePhysicsEditor()
    {
        enablePhysicsEditor = !enablePhysicsEditor;
        LogEditorEvent("切换物理编辑器", enablePhysicsEditor ? "启用" : "禁用");
    }

    /// <summary>
    /// 切换动画播放
    /// </summary>
    public void ToggleAnimationPlayback()
    {
        animationPlaying = !animationPlaying;
        LogEditorEvent("切换动画播放", animationPlaying ? "播放" : "暂停");
    }

    /// <summary>
    /// 设置活动精灵
    /// </summary>
    public void SetActiveSprite(int index)
    {
        if (spriteDataList != null && index >= 0 && index < spriteDataList.Count)
        {
            activeSpriteIndex = index;
            LogEditorEvent("设置活动精灵", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动图集
    /// </summary>
    public void SetActiveAtlas(int index)
    {
        if (atlasDataList != null && index >= 0 && index < atlasDataList.Count)
        {
            activeAtlasIndex = index;
            LogEditorEvent("设置活动图集", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动动画
    /// </summary>
    public void SetActiveAnimation(int index)
    {
        if (animationDataList != null && index >= 0 && index < animationDataList.Count)
        {
            activeAnimationIndex = index;
            LogEditorEvent("设置活动动画", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动物理
    /// </summary>
    public void SetActivePhysics(int index)
    {
        if (physicsDataList != null && index >= 0 && index < physicsDataList.Count)
        {
            activePhysicsIndex = index;
            LogEditorEvent("设置活动物理", $"索引_{index}");
        }
    }

    /// <summary>
    /// 设置活动形状
    /// </summary>
    public void SetActiveShape(int index)
    {
        if (shapeDataList != null && index >= 0 && index < shapeDataList.Count)
        {
            activeShapeIndex = index;
            LogEditorEvent("设置活动形状", $"索引_{index}");
        }
    }

    /// <summary>
    /// 生成2D编辑器报告
    /// </summary>
    public void GenerateU2DReport()
    {
        U2DReportData reportData = new U2DReportData
        {
            timestamp = System.DateTime.Now.ToString(),
            u2dState = u2dState,
            currentEditorMode = currentEditorMode,
            enableSpriteEditor = enableSpriteEditor,
            enableAtlasEditor = enableAtlasEditor,
            enableAnimationEditor = enableAnimationEditor,
            enablePhysicsEditor = enablePhysicsEditor,
            enableShapeEditor = enableShapeEditor,
            totalSprites = totalSprites,
            totalAtlases = totalAtlases,
            totalAnimations = totalAnimations,
            activeSpriteIndex = activeSpriteIndex,
            activeAtlasIndex = activeAtlasIndex,
            activeAnimationIndex = activeAnimationIndex,
            activePhysicsIndex = activePhysicsIndex,
            activeShapeIndex = activeShapeIndex,
            memoryUsage = memoryUsage,
            animationPlaying = animationPlaying,
            animationSpeed = animationSpeed,
            currentFrame = currentFrame
        };
        
        string reportJson = JsonUtility.ToJson(reportData, true);
        Debug.Log($"2D编辑器报告生成完成:\n{reportJson}");
    }

    /// <summary>
    /// 导出2D编辑器数据
    /// </summary>
    public void ExportU2DData()
    {
        // 导出精灵数据
        spriteData = spriteDataList.ToArray();
        
        // 导出图集数据
        atlasData = atlasDataList.ToArray();
        
        // 导出动画数据
        animationData = animationDataList.ToArray();
        
        // 导出物理数据
        physicsData = physicsDataList.ToArray();
        
        // 导出形状数据
        shapeData = shapeDataList.ToArray();
        
        // 导出日志数据
        editorLogs = editorLogList.ToArray();
        
        Debug.Log("2D编辑器数据导出完成");
    }

    /// <summary>
    /// 记录编辑器事件
    /// </summary>
    private void LogEditorEvent(string eventType, string eventData)
    {
        string logMessage = $"[{System.DateTime.Now:HH:mm:ss}] {eventType}: {eventData}";
        editorLogList.Add(logMessage);
        
        // 限制日志数量
        if (editorLogList.Count > 100)
        {
            editorLogList.RemoveAt(0);
        }
        
        Debug.Log(logMessage);
    }

    private void OnGUI()
    {
        if (!isInitialized) return;
        
        GUILayout.BeginArea(new Rect(10, 10, 300, Screen.height - 20));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label("2D编辑器系统", EditorStyles.boldLabel);
        
        // 2D编辑器配置
        GUILayout.Space(10);
        GUILayout.Label("编辑器配置", EditorStyles.boldLabel);
        enableU2D = GUILayout.Toggle(enableU2D, "启用2D编辑器");
        enableSpriteEditor = GUILayout.Toggle(enableSpriteEditor, "启用精灵编辑器");
        enableAtlasEditor = GUILayout.Toggle(enableAtlasEditor, "启用图集编辑器");
        enableAnimationEditor = GUILayout.Toggle(enableAnimationEditor, "启用动画编辑器");
        enablePhysicsEditor = GUILayout.Toggle(enablePhysicsEditor, "启用物理编辑器");
        enableShapeEditor = GUILayout.Toggle(enableShapeEditor, "启用形状编辑器");
        
        // 编辑器状态
        GUILayout.Space(10);
        GUILayout.Label("编辑器状态", EditorStyles.boldLabel);
        GUILayout.Label($"状态: {u2dState}");
        GUILayout.Label($"模式: {currentEditorMode}");
        GUILayout.Label($"精灵数: {totalSprites}");
        GUILayout.Label($"图集数: {totalAtlases}");
        GUILayout.Label($"动画数: {totalAnimations}");
        GUILayout.Label($"内存使用: {memoryUsage:F2}MB");
        GUILayout.Label($"动画播放: {animationPlaying}");
        GUILayout.Label($"当前帧: {currentFrame}");
        
        // 编辑器操作
        GUILayout.Space(10);
        GUILayout.Label("编辑器操作", EditorStyles.boldLabel);
        if (GUILayout.Button("切换精灵编辑器")) ToggleSpriteEditor();
        if (GUILayout.Button("切换图集编辑器")) ToggleAtlasEditor();
        if (GUILayout.Button("切换动画编辑器")) ToggleAnimationEditor();
        if (GUILayout.Button("切换物理编辑器")) TogglePhysicsEditor();
        if (GUILayout.Button("切换动画播放")) ToggleAnimationPlayback();
        
        // 编辑器数据
        GUILayout.Space(10);
        GUILayout.Label("编辑器数据", EditorStyles.boldLabel);
        if (GUILayout.Button("生成报告")) GenerateU2DReport();
        if (GUILayout.Button("导出数据")) ExportU2DData();
        if (GUILayout.Button("清空日志")) editorLogList.Clear();
        
        // 快捷键提示
        GUILayout.Space(10);
        GUILayout.Label("快捷键", EditorStyles.boldLabel);
        GUILayout.Label("F1: 切换精灵编辑器");
        GUILayout.Label("F2: 切换图集编辑器");
        GUILayout.Label("F3: 切换动画编辑器");
        GUILayout.Label("F4: 切换物理编辑器");
        GUILayout.Label("空格: 切换动画播放");
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}

/// <summary>
/// 精灵编辑器类
/// </summary>
public class SpriteEditor
{
    public bool enableSlicing;
    public Vector2 sliceSize;
    public Vector2 sliceOffset;
    public Vector2 slicePadding;
    public Vector2 size;
    public Vector2 position;
}

/// <summary>
/// 图集编辑器类
/// </summary>
public class AtlasEditor
{
    public int maxSize;
    public int padding;
    public bool allowRotation;
    public bool squarePacking;
    public bool tightPacking;
}

/// <summary>
/// 动画编辑器类
/// </summary>
public class AnimationEditor
{
    public bool enablePreview;
    public float animationSpeed;
}

/// <summary>
/// 物理编辑器类
/// </summary>
public class PhysicsEditor
{
    public bool enablePreview;
}

/// <summary>
/// 形状编辑器类
/// </summary>
public class ShapeEditor
{
    public bool enablePreview;
    public Color shapeColor;
    public float shapeThickness;
}

/// <summary>
/// 物理形状2D枚举
/// </summary>
public enum PhysicsShape2D
{
    Box,
    Circle,
    Polygon,
    Edge
}

/// <summary>
/// 精灵数据
/// </summary>
[System.Serializable]
public class SpriteData
{
    public string spriteId;
    public string spriteName;
    public Texture2D spriteTexture;
    public Vector2 spritePivot;
    public Vector2 spriteSize;
    public SpriteAlignment spriteAlignment;
    public Vector2 spriteOffset;
    public bool spriteGeneratePhysics;
}

/// <summary>
/// 图集数据
/// </summary>
[System.Serializable]
public class AtlasData
{
    public string atlasId;
    public string atlasName;
    public Texture2D atlasTexture;
    public int atlasMaxSize;
    public int atlasPadding;
    public bool atlasAllowRotation;
    public bool atlasSquarePacking;
    public bool atlasTightPacking;
}

/// <summary>
/// 动画数据
/// </summary>
[System.Serializable]
public class AnimationData
{
    public string animationId;
    public string animationName;
    public Sprite[] animationSprites;
    public float animationFrameRate;
    public bool animationLooping;
    public float animationSpeed;
}

/// <summary>
/// 物理数据
/// </summary>
[System.Serializable]
public class PhysicsData
{
    public string physicsId;
    public string physicsName;
    public Sprite physicsSprite;
    public PhysicsShape2D physicsShape;
    public bool physicsEnabled;
    public float physicsDensity;
    public float physicsFriction;
    public float physicsBounciness;
}

/// <summary>
/// 形状数据
/// </summary>
[System.Serializable]
public class ShapeData
{
    public string shapeId;
    public string shapeName;
    public Vector2[] shapePoints;
    public bool shapeClosed;
    public float shapeRadius;
    public Color shapeColor;
    public float shapeThickness;
}

/// <summary>
/// 2D编辑器报告数据
/// </summary>
[System.Serializable]
public class U2DReportData
{
    public string timestamp;
    public string u2dState;
    public string currentEditorMode;
    public bool enableSpriteEditor;
    public bool enableAtlasEditor;
    public bool enableAnimationEditor;
    public bool enablePhysicsEditor;
    public bool enableShapeEditor;
    public int totalSprites;
    public int totalAtlases;
    public int totalAnimations;
    public int activeSpriteIndex;
    public int activeAtlasIndex;
    public int activeAnimationIndex;
    public int activePhysicsIndex;
    public int activeShapeIndex;
    public float memoryUsage;
    public bool animationPlaying;
    public float animationSpeed;
    public int currentFrame;
} 