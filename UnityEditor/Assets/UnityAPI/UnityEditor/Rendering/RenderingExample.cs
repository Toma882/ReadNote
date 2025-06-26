using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.Rendering 命名空间案例演示
/// 展示渲染系统的使用，包括渲染管线、着色器、材质等
/// </summary>
public class RenderingExample : MonoBehaviour
{
    [Header("渲染系统配置")]
    [SerializeField] private bool enableRenderingSystem = true;
    [SerializeField] private bool enableShaderManagement = true;
    [SerializeField] private bool enableMaterialManagement = true;
    [SerializeField] private bool enableRenderPipelineManagement = true;
    [SerializeField] private bool enableLightingManagement = true;
    [SerializeField] private bool enablePostProcessingManagement = true;
    
    [Header("渲染管线配置")]
    [SerializeField] private RenderPipelineAsset currentRenderPipeline;
    [SerializeField] private RenderPipelineAsset targetRenderPipeline;
    [SerializeField] private bool enablePipelineValidation = true;
    [SerializeField] private bool enablePipelineOptimization = true;
    [SerializeField] private bool enablePipelineProfiling = true;
    [SerializeField] private bool enablePipelineDebugging = true;
    
    [Header("着色器配置")]
    [SerializeField] private Shader currentShader;
    [SerializeField] private Shader targetShader;
    [SerializeField] private bool enableShaderCompilation = true;
    [SerializeField] private bool enableShaderValidation = true;
    [SerializeField] private bool enableShaderOptimization = true;
    [SerializeField] private bool enableShaderDebugging = true;
    [SerializeField] private bool enableShaderProfiling = true;
    
    [Header("材质配置")]
    [SerializeField] private Material currentMaterial;
    [SerializeField] private Material targetMaterial;
    [SerializeField] private bool enableMaterialValidation = true;
    [SerializeField] private bool enableMaterialOptimization = true;
    [SerializeField] private bool enableMaterialProfiling = true;
    [SerializeField] private bool enableMaterialDebugging = true;
    
    [Header("光照配置")]
    [SerializeField] private Light currentLight;
    [SerializeField] private Light targetLight;
    [SerializeField] private bool enableLightingValidation = true;
    [SerializeField] private bool enableLightingOptimization = true;
    [SerializeField] private bool enableLightingProfiling = true;
    [SerializeField] private bool enableLightingDebugging = true;
    
    [Header("后处理配置")]
    [SerializeField] private bool enablePostProcessing = true;
    [SerializeField] private bool enablePostProcessingValidation = true;
    [SerializeField] private bool enablePostProcessingOptimization = true;
    [SerializeField] private bool enablePostProcessingProfiling = true;
    [SerializeField] private bool enablePostProcessingDebugging = true;
    
    [Header("渲染状态")]
    [SerializeField] private RenderingStatus renderingStatus = RenderingStatus.Idle;
    [SerializeField] private bool isRendering = false;
    [SerializeField] private bool isCompiling = false;
    [SerializeField] private bool isOptimizing = false;
    [SerializeField] private bool isProfiling = false;
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private float renderingProgress = 0f;
    [SerializeField] private string renderingMessage = "";
    
    [Header("渲染统计")]
    [SerializeField] private int totalShaders = 0;
    [SerializeField] private int totalMaterials = 0;
    [SerializeField] private int totalLights = 0;
    [SerializeField] private int totalRenderers = 0;
    [SerializeField] private int totalCameras = 0;
    [SerializeField] private int totalPostProcessors = 0;
    [SerializeField] private float averageFrameTime = 0f;
    [SerializeField] private float maxFrameTime = 0f;
    [SerializeField] private float minFrameTime = 0f;
    [SerializeField] private int totalFrames = 0;
    [SerializeField] private float totalFrameTime = 0f;
    
    [Header("渲染性能")]
    [SerializeField] private bool enablePerformanceMonitoring = true;
    [SerializeField] private float[] frameTimeHistory = new float[100];
    [SerializeField] private int frameTimeIndex = 0;
    [SerializeField] private float targetFrameRate = 60f;
    [SerializeField] private float currentFrameRate = 0f;
    [SerializeField] private float frameRateVariance = 0f;
    [SerializeField] private int droppedFrames = 0;
    [SerializeField] private int totalDroppedFrames = 0;
    
    [Header("渲染质量")]
    [SerializeField] private bool enableQualityManagement = true;
    [SerializeField] private QualityLevel currentQualityLevel = QualityLevel.Medium;
    [SerializeField] private QualityLevel targetQualityLevel = QualityLevel.High;
    [SerializeField] private bool enableQualityValidation = true;
    [SerializeField] private bool enableQualityOptimization = true;
    [SerializeField] private bool enableQualityProfiling = true;
    
    [Header("渲染调试")]
    [SerializeField] private bool enableRenderingDebugging = true;
    [SerializeField] private bool enableWireframeMode = false;
    [SerializeField] private bool enableBoundingBoxMode = false;
    [SerializeField] private bool enableNormalsMode = false;
    [SerializeField] private bool enableUVMode = false;
    [SerializeField] private bool enableLightingMode = false;
    [SerializeField] private bool enableShadowMode = false;
    [SerializeField] private bool enableDepthMode = false;
    [SerializeField] private bool enableStencilMode = false;
    
    [Header("渲染历史")]
    [SerializeField] private RenderingHistoryEntry[] renderingHistory = new RenderingHistoryEntry[20];
    [SerializeField] private int renderingHistoryIndex = 0;
    [SerializeField] private bool enableRenderingHistory = true;
    
    [Header("渲染报告")]
    [SerializeField] private RenderingReport currentRenderingReport;
    [SerializeField] private bool enableRenderingReports = true;
    [SerializeField] private bool enableAutoReports = true;
    [SerializeField] private float reportInterval = 60f; // 1分钟
    [SerializeField] private float lastReportTime = 0f;
    
    private bool isInitialized = false;
    private float renderingStartTime = 0f;
    private float lastFrameTime = 0f;
    private float frameStartTime = 0f;
    private int frameCount = 0;
    private float frameTimeSum = 0f;
    private float frameTimeMin = float.MaxValue;
    private float frameTimeMax = 0f;

    private void Start()
    {
        InitializeRenderingSystem();
    }

    private void InitializeRenderingSystem()
    {
        if (!enableRenderingSystem) return;
        
        InitializeRenderingState();
        InitializePerformanceMonitoring();
        InitializeRenderingStatistics();
        InitializeRenderingDebugging();
        RegisterRenderingCallbacks();
        
        isInitialized = true;
        renderingStatus = RenderingStatus.Idle;
        Debug.Log("渲染系统初始化完成");
    }

    private void InitializeRenderingState()
    {
        renderingStatus = RenderingStatus.Idle;
        isRendering = false;
        isCompiling = false;
        isOptimizing = false;
        isProfiling = false;
        isDebugging = false;
        renderingProgress = 0f;
        renderingMessage = "就绪";
        
        Debug.Log("渲染状态已初始化");
    }

    private void InitializePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            frameTimeHistory = new float[100];
            frameTimeIndex = 0;
            averageFrameTime = 0f;
            maxFrameTime = 0f;
            minFrameTime = float.MaxValue;
            totalFrames = 0;
            totalFrameTime = 0f;
            currentFrameRate = 0f;
            frameRateVariance = 0f;
            droppedFrames = 0;
            totalDroppedFrames = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    private void InitializeRenderingStatistics()
    {
        totalShaders = 0;
        totalMaterials = 0;
        totalLights = 0;
        totalRenderers = 0;
        totalCameras = 0;
        totalPostProcessors = 0;
        
        Debug.Log("渲染统计初始化完成");
    }

    private void InitializeRenderingDebugging()
    {
        if (enableRenderingDebugging)
        {
            enableWireframeMode = false;
            enableBoundingBoxMode = false;
            enableNormalsMode = false;
            enableUVMode = false;
            enableLightingMode = false;
            enableShadowMode = false;
            enableDepthMode = false;
            enableStencilMode = false;
            
            Debug.Log("渲染调试初始化完成");
        }
    }

    private void RegisterRenderingCallbacks()
    {
        // 注册渲染回调
        Camera.onPreRender += OnPreRender;
        Camera.onPostRender += OnPostRender;
        
        Debug.Log("渲染回调已注册");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        UpdateRenderingStatus();
        UpdateRenderingProgress();
        UpdateRenderingStatistics();
        UpdatePerformanceMonitoring();
        
        if (enableAutoReports)
        {
            CheckAutoReport();
        }
        
        if (enableQualityManagement)
        {
            UpdateQualityManagement();
        }
    }

    private void UpdateRenderingStatus()
    {
        if (isRendering)
        {
            renderingStatus = RenderingStatus.Rendering;
        }
        else if (isCompiling)
        {
            renderingStatus = RenderingStatus.Compiling;
        }
        else if (isOptimizing)
        {
            renderingStatus = RenderingStatus.Optimizing;
        }
        else if (isProfiling)
        {
            renderingStatus = RenderingStatus.Profiling;
        }
        else if (isDebugging)
        {
            renderingStatus = RenderingStatus.Debugging;
        }
        else
        {
            renderingStatus = RenderingStatus.Idle;
        }
    }

    private void UpdateRenderingProgress()
    {
        // 更新渲染进度
        if (isRendering)
        {
            renderingProgress = (Time.time - renderingStartTime) / 1f; // 假设渲染需要1秒
            if (renderingProgress > 1f) renderingProgress = 1f;
        }
        else
        {
            renderingProgress = 0f;
        }
    }

    private void UpdateRenderingStatistics()
    {
        // 更新着色器统计
        var shaders = Resources.FindObjectsOfTypeAll<Shader>();
        totalShaders = shaders.Length;
        
        // 更新材质统计
        var materials = Resources.FindObjectsOfTypeAll<Material>();
        totalMaterials = materials.Length;
        
        // 更新光照统计
        var lights = FindObjectsOfType<Light>();
        totalLights = lights.Length;
        
        // 更新渲染器统计
        var renderers = FindObjectsOfType<Renderer>();
        totalRenderers = renderers.Length;
        
        // 更新相机统计
        var cameras = FindObjectsOfType<Camera>();
        totalCameras = cameras.Length;
        
        // 更新后处理器统计
        var postProcessors = FindObjectsOfType<MonoBehaviour>();
        totalPostProcessors = 0;
        foreach (var processor in postProcessors)
        {
            if (processor.GetType().Name.Contains("PostProcess"))
            {
                totalPostProcessors++;
            }
        }
    }

    private void UpdatePerformanceMonitoring()
    {
        if (enablePerformanceMonitoring)
        {
            float currentTime = Time.time;
            float frameTime = currentTime - frameStartTime;
            
            if (frameCount > 0)
            {
                frameTimeHistory[frameTimeIndex] = frameTime;
                frameTimeIndex = (frameTimeIndex + 1) % 100;
                
                frameTimeSum += frameTime;
                totalFrames++;
                
                if (frameTime > frameTimeMax)
                {
                    frameTimeMax = frameTime;
                }
                
                if (frameTime < frameTimeMin)
                {
                    frameTimeMin = frameTime;
                }
                
                averageFrameTime = frameTimeSum / totalFrames;
                currentFrameRate = 1f / averageFrameTime;
                
                // 检查丢帧
                float targetFrameTime = 1f / targetFrameRate;
                if (frameTime > targetFrameTime * 1.1f) // 超过目标帧时间的10%
                {
                    droppedFrames++;
                    totalDroppedFrames++;
                }
                
                // 计算帧率方差
                float varianceSum = 0f;
                for (int i = 0; i < 100; i++)
                {
                    varianceSum += Mathf.Pow(frameTimeHistory[i] - averageFrameTime, 2);
                }
                frameRateVariance = varianceSum / 100f;
            }
            
            frameStartTime = currentTime;
            frameCount++;
        }
    }

    private void UpdateQualityManagement()
    {
        if (currentQualityLevel != targetQualityLevel)
        {
            SetQualityLevel(targetQualityLevel);
        }
    }

    private void CheckAutoReport()
    {
        if (Time.time - lastReportTime > reportInterval)
        {
            GenerateRenderingReport();
            lastReportTime = Time.time;
        }
    }

    private void OnPreRender(Camera camera)
    {
        if (enableRenderingDebugging)
        {
            ApplyRenderingDebugModes();
        }
    }

    private void OnPostRender(Camera camera)
    {
        if (enableRenderingDebugging)
        {
            DisableRenderingDebugModes();
        }
    }

    private void ApplyRenderingDebugModes()
    {
        if (enableWireframeMode)
        {
            GL.wireframe = true;
        }
        
        if (enableBoundingBoxMode)
        {
            // 绘制边界框
            DrawBoundingBoxes();
        }
        
        if (enableNormalsMode)
        {
            // 绘制法线
            DrawNormals();
        }
        
        if (enableUVMode)
        {
            // 显示UV坐标
            ShowUVCoordinates();
        }
    }

    private void DisableRenderingDebugModes()
    {
        GL.wireframe = false;
    }

    private void DrawBoundingBoxes()
    {
        var renderers = FindObjectsOfType<Renderer>();
        foreach (var renderer in renderers)
        {
            Bounds bounds = renderer.bounds;
            Vector3 center = bounds.center;
            Vector3 size = bounds.size;
            
            // 绘制边界框
            Debug.DrawLine(center + new Vector3(-size.x/2, -size.y/2, -size.z/2), 
                          center + new Vector3(size.x/2, -size.y/2, -size.z/2), Color.red);
            Debug.DrawLine(center + new Vector3(-size.x/2, -size.y/2, -size.z/2), 
                          center + new Vector3(-size.x/2, size.y/2, -size.z/2), Color.green);
            Debug.DrawLine(center + new Vector3(-size.x/2, -size.y/2, -size.z/2), 
                          center + new Vector3(-size.x/2, -size.y/2, size.z/2), Color.blue);
        }
    }

    private void DrawNormals()
    {
        var meshFilters = FindObjectsOfType<MeshFilter>();
        foreach (var meshFilter in meshFilters)
        {
            Mesh mesh = meshFilter.sharedMesh;
            if (mesh != null)
            {
                Vector3[] vertices = mesh.vertices;
                Vector3[] normals = mesh.normals;
                
                for (int i = 0; i < vertices.Length; i++)
                {
                    Vector3 worldPos = meshFilter.transform.TransformPoint(vertices[i]);
                    Vector3 worldNormal = meshFilter.transform.TransformDirection(normals[i]);
                    
                    Debug.DrawRay(worldPos, worldNormal * 0.1f, Color.yellow);
                }
            }
        }
    }

    private void ShowUVCoordinates()
    {
        // 这里可以实现UV坐标的可视化
        // 由于需要特殊的着色器，这里只是占位符
        Debug.Log("UV坐标可视化功能需要特殊着色器支持");
    }

    public void CompileShaders()
    {
        if (isCompiling)
        {
            Debug.LogWarning("着色器正在编译中，请等待完成");
            return;
        }
        
        isCompiling = true;
        renderingStartTime = Time.time;
        renderingProgress = 0f;
        renderingMessage = "正在编译着色器...";
        
        try
        {
            // 强制重新编译所有着色器
            Shader.WarmupAllShaders();
            
            float compileTime = Time.time - renderingStartTime;
            isCompiling = false;
            renderingProgress = 1f;
            renderingMessage = "着色器编译完成";
            
            if (enableRenderingHistory)
            {
                AddRenderingHistoryEntry("CompileShaders", "所有着色器", compileTime);
            }
            
            Debug.Log($"着色器编译完成，耗时: {compileTime:F3}秒");
        }
        catch (System.Exception e)
        {
            isCompiling = false;
            renderingProgress = 0f;
            renderingMessage = $"着色器编译失败: {e.Message}";
            Debug.LogError($"着色器编译过程中发生错误: {e}");
        }
    }

    public void OptimizeMaterials()
    {
        if (isOptimizing)
        {
            Debug.LogWarning("材质正在优化中，请等待完成");
            return;
        }
        
        isOptimizing = true;
        renderingStartTime = Time.time;
        renderingProgress = 0f;
        renderingMessage = "正在优化材质...";
        
        try
        {
            var materials = Resources.FindObjectsOfTypeAll<Material>();
            int optimizedCount = 0;
            
            foreach (var material in materials)
            {
                if (material != null)
                {
                    // 优化材质设置
                    OptimizeMaterial(material);
                    optimizedCount++;
                }
            }
            
            float optimizeTime = Time.time - renderingStartTime;
            isOptimizing = false;
            renderingProgress = 1f;
            renderingMessage = $"材质优化完成，共优化 {optimizedCount} 个材质";
            
            if (enableRenderingHistory)
            {
                AddRenderingHistoryEntry("OptimizeMaterials", $"{optimizedCount} 个材质", optimizeTime);
            }
            
            Debug.Log($"材质优化完成，共优化 {optimizedCount} 个材质，耗时: {optimizeTime:F3}秒");
        }
        catch (System.Exception e)
        {
            isOptimizing = false;
            renderingProgress = 0f;
            renderingMessage = $"材质优化失败: {e.Message}";
            Debug.LogError($"材质优化过程中发生错误: {e}");
        }
    }

    private void OptimizeMaterial(Material material)
    {
        // 优化材质设置
        if (material.HasProperty("_MainTex"))
        {
            Texture mainTex = material.GetTexture("_MainTex");
            if (mainTex != null)
            {
                // 设置纹理压缩
                string texturePath = AssetDatabase.GetAssetPath(mainTex);
                if (!string.IsNullOrEmpty(texturePath))
                {
                    TextureImporter importer = AssetImporter.GetAtPath(texturePath) as TextureImporter;
                    if (importer != null)
                    {
                        importer.textureCompression = TextureImporterCompression.Compressed;
                        importer.SaveAndReimport();
                    }
                }
            }
        }
        
        // 优化着色器变体
        if (material.shader != null)
        {
            // 这里可以添加更多优化逻辑
        }
    }

    public void ProfileRendering()
    {
        if (isProfiling)
        {
            Debug.LogWarning("渲染正在分析中，请等待完成");
            return;
        }
        
        isProfiling = true;
        renderingStartTime = Time.time;
        renderingProgress = 0f;
        renderingMessage = "正在分析渲染性能...";
        
        try
        {
            // 开始性能分析
            Profiler.BeginSample("Rendering Profile");
            
            // 模拟分析过程
            System.Threading.Thread.Sleep(1000);
            
            Profiler.EndSample();
            
            float profileTime = Time.time - renderingStartTime;
            isProfiling = false;
            renderingProgress = 1f;
            renderingMessage = "渲染性能分析完成";
            
            if (enableRenderingHistory)
            {
                AddRenderingHistoryEntry("ProfileRendering", "渲染性能", profileTime);
            }
            
            Debug.Log($"渲染性能分析完成，耗时: {profileTime:F3}秒");
        }
        catch (System.Exception e)
        {
            isProfiling = false;
            renderingProgress = 0f;
            renderingMessage = $"渲染性能分析失败: {e.Message}";
            Debug.LogError($"渲染性能分析过程中发生错误: {e}");
        }
    }

    public void SetQualityLevel(QualityLevel level)
    {
        if (currentQualityLevel != level)
        {
            QualitySettings.SetQualityLevel((int)level, true);
            currentQualityLevel = level;
            
            Debug.Log($"质量等级已设置为: {level}");
        }
    }

    public void ToggleWireframeMode()
    {
        enableWireframeMode = !enableWireframeMode;
        Debug.Log($"线框模式: {(enableWireframeMode ? "开启" : "关闭")}");
    }

    public void ToggleBoundingBoxMode()
    {
        enableBoundingBoxMode = !enableBoundingBoxMode;
        Debug.Log($"边界框模式: {(enableBoundingBoxMode ? "开启" : "关闭")}");
    }

    public void ToggleNormalsMode()
    {
        enableNormalsMode = !enableNormalsMode;
        Debug.Log($"法线模式: {(enableNormalsMode ? "开启" : "关闭")}");
    }

    public void GenerateRenderingReport()
    {
        currentRenderingReport = new RenderingReport
        {
            timestamp = System.DateTime.Now.ToString(),
            totalShaders = totalShaders,
            totalMaterials = totalMaterials,
            totalLights = totalLights,
            totalRenderers = totalRenderers,
            totalCameras = totalCameras,
            totalPostProcessors = totalPostProcessors,
            averageFrameTime = averageFrameTime,
            maxFrameTime = maxFrameTime,
            minFrameTime = minFrameTime,
            currentFrameRate = currentFrameRate,
            frameRateVariance = frameRateVariance,
            droppedFrames = droppedFrames,
            totalDroppedFrames = totalDroppedFrames,
            currentQualityLevel = currentQualityLevel.ToString(),
            renderingStatus = renderingStatus.ToString()
        };
        
        Debug.Log("=== 渲染系统报告 ===");
        Debug.Log($"渲染系统状态: {renderingStatus}");
        Debug.Log($"总着色器数: {totalShaders}");
        Debug.Log($"总材质数: {totalMaterials}");
        Debug.Log($"总光照数: {totalLights}");
        Debug.Log($"总渲染器数: {totalRenderers}");
        Debug.Log($"总相机数: {totalCameras}");
        Debug.Log($"总后处理器数: {totalPostProcessors}");
        Debug.Log($"平均帧时间: {averageFrameTime:F3}秒");
        Debug.Log($"最大帧时间: {maxFrameTime:F3}秒");
        Debug.Log($"最小帧时间: {minFrameTime:F3}秒");
        Debug.Log($"当前帧率: {currentFrameRate:F1} FPS");
        Debug.Log($"帧率方差: {frameRateVariance:F6}");
        Debug.Log($"丢帧数: {droppedFrames}");
        Debug.Log($"总丢帧数: {totalDroppedFrames}");
        Debug.Log($"当前质量等级: {currentQualityLevel}");
    }

    private void AddRenderingHistoryEntry(string operation, string target, float time)
    {
        var entry = new RenderingHistoryEntry
        {
            timestamp = System.DateTime.Now.ToString(),
            operation = operation,
            target = target,
            time = time,
            success = !string.IsNullOrEmpty(renderingMessage) && !renderingMessage.Contains("失败")
        };
        
        renderingHistory[renderingHistoryIndex] = entry;
        renderingHistoryIndex = (renderingHistoryIndex + 1) % renderingHistory.Length;
    }

    public void ClearRenderingHistory()
    {
        renderingHistory = new RenderingHistoryEntry[20];
        renderingHistoryIndex = 0;
        Debug.Log("渲染历史已清除");
    }

    public void ResetRenderingStatistics()
    {
        totalShaders = 0;
        totalMaterials = 0;
        totalLights = 0;
        totalRenderers = 0;
        totalCameras = 0;
        totalPostProcessors = 0;
        averageFrameTime = 0f;
        maxFrameTime = 0f;
        minFrameTime = float.MaxValue;
        totalFrames = 0;
        totalFrameTime = 0f;
        currentFrameRate = 0f;
        frameRateVariance = 0f;
        droppedFrames = 0;
        totalDroppedFrames = 0;
        
        Debug.Log("渲染统计已重置");
    }

    private void OnDestroy()
    {
        Camera.onPreRender -= OnPreRender;
        Camera.onPostRender -= OnPostRender;
        
        Debug.Log("渲染回调已清理");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Rendering 渲染系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("渲染系统配置:");
        enableRenderingSystem = GUILayout.Toggle(enableRenderingSystem, "启用渲染系统");
        enableShaderManagement = GUILayout.Toggle(enableShaderManagement, "启用着色器管理");
        enableMaterialManagement = GUILayout.Toggle(enableMaterialManagement, "启用材质管理");
        enableRenderPipelineManagement = GUILayout.Toggle(enableRenderPipelineManagement, "启用渲染管线管理");
        enableLightingManagement = GUILayout.Toggle(enableLightingManagement, "启用光照管理");
        enablePostProcessingManagement = GUILayout.Toggle(enablePostProcessingManagement, "启用后处理管理");
        
        GUILayout.Space(10);
        GUILayout.Label("渲染状态:");
        GUILayout.Label($"渲染状态: {renderingStatus}");
        GUILayout.Label($"是否正在渲染: {isRendering}");
        GUILayout.Label($"是否正在编译: {isCompiling}");
        GUILayout.Label($"是否正在优化: {isOptimizing}");
        GUILayout.Label($"是否正在分析: {isProfiling}");
        GUILayout.Label($"渲染进度: {renderingProgress * 100:F1}%");
        GUILayout.Label($"渲染消息: {renderingMessage}");
        
        GUILayout.Space(10);
        GUILayout.Label("渲染统计:");
        GUILayout.Label($"总着色器数: {totalShaders}");
        GUILayout.Label($"总材质数: {totalMaterials}");
        GUILayout.Label($"总光照数: {totalLights}");
        GUILayout.Label($"总渲染器数: {totalRenderers}");
        GUILayout.Label($"总相机数: {totalCameras}");
        GUILayout.Label($"总后处理器数: {totalPostProcessors}");
        
        GUILayout.Space(10);
        GUILayout.Label("性能监控:");
        GUILayout.Label($"平均帧时间: {averageFrameTime:F3}秒");
        GUILayout.Label($"最大帧时间: {maxFrameTime:F3}秒");
        GUILayout.Label($"最小帧时间: {minFrameTime:F3}秒");
        GUILayout.Label($"当前帧率: {currentFrameRate:F1} FPS");
        GUILayout.Label($"帧率方差: {frameRateVariance:F6}");
        GUILayout.Label($"丢帧数: {droppedFrames}");
        GUILayout.Label($"总丢帧数: {totalDroppedFrames}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("编译着色器"))
        {
            CompileShaders();
        }
        
        if (GUILayout.Button("优化材质"))
        {
            OptimizeMaterials();
        }
        
        if (GUILayout.Button("分析渲染性能"))
        {
            ProfileRendering();
        }
        
        if (GUILayout.Button("生成渲染报告"))
        {
            GenerateRenderingReport();
        }
        
        if (GUILayout.Button("切换线框模式"))
        {
            ToggleWireframeMode();
        }
        
        if (GUILayout.Button("切换边界框模式"))
        {
            ToggleBoundingBoxMode();
        }
        
        if (GUILayout.Button("切换法线模式"))
        {
            ToggleNormalsMode();
        }
        
        if (GUILayout.Button("清除渲染历史"))
        {
            ClearRenderingHistory();
        }
        
        if (GUILayout.Button("重置渲染统计"))
        {
            ResetRenderingStatistics();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("渲染历史:");
        for (int i = 0; i < renderingHistory.Length; i++)
        {
            if (renderingHistory[i] != null && !string.IsNullOrEmpty(renderingHistory[i].timestamp))
            {
                var entry = renderingHistory[i];
                string status = entry.success ? "成功" : "失败";
                GUILayout.Label($"{entry.timestamp} - {entry.operation} - {entry.target} - {status} - {entry.time:F3}s");
            }
        }
        
        GUILayout.EndArea();
    }
}

public enum RenderingStatus
{
    Idle,
    Rendering,
    Compiling,
    Optimizing,
    Profiling,
    Debugging,
    Completed,
    Failed
}

public enum QualityLevel
{
    Low,
    Medium,
    High,
    VeryHigh,
    Ultra
}

[System.Serializable]
public class RenderingHistoryEntry
{
    public string timestamp;
    public string operation;
    public string target;
    public float time;
    public bool success;
}

[System.Serializable]
public class RenderingReport
{
    public string timestamp;
    public int totalShaders;
    public int totalMaterials;
    public int totalLights;
    public int totalRenderers;
    public int totalCameras;
    public int totalPostProcessors;
    public float averageFrameTime;
    public float maxFrameTime;
    public float minFrameTime;
    public float currentFrameRate;
    public float frameRateVariance;
    public int droppedFrames;
    public int totalDroppedFrames;
    public string currentQualityLevel;
    public string renderingStatus;
} 