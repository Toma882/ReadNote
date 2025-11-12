using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Rendering 命名空间案例演示
/// 展示渲染系统的核心功能
/// </summary>
public class RenderingExample : MonoBehaviour
{
    [Header("渲染设置")]
    [SerializeField] private Camera targetCamera;
    [SerializeField] private RenderPipelineAsset renderPipelineAsset;
    [SerializeField] private bool useCustomRenderPipeline = false;
    
    [Header("渲染统计")]
    [SerializeField] private int drawCalls = 0;
    [SerializeField] private int triangles = 0;
    [SerializeField] private int vertices = 0;
    [SerializeField] private float frameTime = 0f;
    
    [Header("渲染目标")]
    [SerializeField] private RenderTexture customRenderTexture;
    [SerializeField] private Material renderMaterial;
    [SerializeField] private bool enableCustomRendering = false;
    
    [Header("渲染事件")]
    [SerializeField] private bool enableRenderEvents = true;
    [SerializeField] private bool enableCommandBuffer = false;
    
    // 渲染组件
    private Renderer[] sceneRenderers;
    private CommandBuffer commandBuffer;
    private RenderTargetIdentifier renderTarget;
    
    // 渲染统计
    private float lastFrameTime;
    private int frameCount = 0;
    
    private void Start()
    {
        InitializeRenderingSystem();
    }
    
    /// <summary>
    /// 初始化渲染系统
    /// </summary>
    private void InitializeRenderingSystem()
    {
        // 获取目标相机
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
            if (targetCamera == null)
            {
                targetCamera = FindObjectOfType<Camera>();
            }
        }
        
        // 获取场景中的所有渲染器
        sceneRenderers = FindObjectsOfType<Renderer>();
        
        // 初始化命令缓冲区
        if (enableCommandBuffer)
        {
            InitializeCommandBuffer();
        }
        
        // 初始化自定义渲染目标
        if (enableCustomRendering)
        {
            InitializeCustomRenderTarget();
        }
        
        Debug.Log($"渲染系统初始化完成 - 渲染器数量: {sceneRenderers.Length}");
    }
    
    /// <summary>
    /// 初始化命令缓冲区
    /// </summary>
    private void InitializeCommandBuffer()
    {
        commandBuffer = new CommandBuffer();
        commandBuffer.name = "Custom Rendering Commands";
        
        // 添加清除命令
        commandBuffer.ClearRenderTarget(true, true, Color.black);
        
        // 添加绘制命令
        commandBuffer.DrawProcedural(Matrix4x4.identity, renderMaterial, 0, MeshTopology.Triangles, 6);
        
        Debug.Log("命令缓冲区初始化完成");
    }
    
    /// <summary>
    /// 初始化自定义渲染目标
    /// </summary>
    private void InitializeCustomRenderTarget()
    {
        if (customRenderTexture == null)
        {
            customRenderTexture = new RenderTexture(1024, 1024, 24);
            customRenderTexture.name = "Custom Render Target";
            customRenderTexture.Create();
        }
        
        renderTarget = new RenderTargetIdentifier(customRenderTexture);
        
        Debug.Log("自定义渲染目标初始化完成");
    }
    
    /// <summary>
    /// 获取渲染统计信息
    /// </summary>
    public void GetRenderingStatistics()
    {
        // 获取当前帧的渲染统计
        drawCalls = UnityStats.drawCalls;
        triangles = UnityStats.triangles;
        vertices = UnityStats.vertices;
        
        // 计算帧时间
        frameTime = Time.time - lastFrameTime;
        lastFrameTime = Time.time;
        frameCount++;
        
        Debug.Log($"=== 渲染统计 (帧 {frameCount}) ===");
        Debug.Log($"绘制调用: {drawCalls}");
        Debug.Log($"三角形数: {triangles}");
        Debug.Log($"顶点数: {vertices}");
        Debug.Log($"帧时间: {frameTime * 1000:F2}ms");
        Debug.Log($"FPS: {1.0f / frameTime:F1}");
    }
    
    /// <summary>
    /// 设置渲染管线
    /// </summary>
    /// <param name="pipelineAsset">渲染管线资产</param>
    public void SetRenderPipeline(RenderPipelineAsset pipelineAsset)
    {
        if (pipelineAsset != null)
        {
            GraphicsSettings.renderPipelineAsset = pipelineAsset;
            useCustomRenderPipeline = true;
            Debug.Log($"渲染管线已设置为: {pipelineAsset.name}");
        }
        else
        {
            GraphicsSettings.renderPipelineAsset = null;
            useCustomRenderPipeline = false;
            Debug.Log("已切换到内置渲染管线");
        }
    }
    
    /// <summary>
    /// 切换渲染管线
    /// </summary>
    public void ToggleRenderPipeline()
    {
        if (useCustomRenderPipeline)
        {
            SetRenderPipeline(null);
        }
        else if (renderPipelineAsset != null)
        {
            SetRenderPipeline(renderPipelineAsset);
        }
    }
    
    /// <summary>
    /// 启用/禁用渲染器
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetRenderersEnabled(bool enabled)
    {
        foreach (var renderer in sceneRenderers)
        {
            if (renderer != null)
            {
                renderer.enabled = enabled;
            }
        }
        
        Debug.Log($"所有渲染器已{(enabled ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 设置渲染器材质
    /// </summary>
    /// <param name="material">材质</param>
    public void SetRenderersMaterial(Material material)
    {
        if (material == null) return;
        
        foreach (var renderer in sceneRenderers)
        {
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
        
        Debug.Log($"所有渲染器材质已设置为: {material.name}");
    }
    
    /// <summary>
    /// 设置渲染器排序顺序
    /// </summary>
    /// <param name="sortingOrder">排序顺序</param>
    public void SetRenderersSortingOrder(int sortingOrder)
    {
        foreach (var renderer in sceneRenderers)
        {
            if (renderer != null)
            {
                renderer.sortingOrder = sortingOrder;
            }
        }
        
        Debug.Log($"所有渲染器排序顺序已设置为: {sortingOrder}");
    }
    
    /// <summary>
    /// 设置渲染器层级
    /// </summary>
    /// <param name="layer">层级</param>
    public void SetRenderersLayer(int layer)
    {
        foreach (var renderer in sceneRenderers)
        {
            if (renderer != null)
            {
                renderer.gameObject.layer = layer;
            }
        }
        
        Debug.Log($"所有渲染器层级已设置为: {layer}");
    }
    
    /// <summary>
    /// 启用/禁用阴影
    /// </summary>
    /// <param name="enabled">是否启用</param>
    public void SetShadowsEnabled(bool enabled)
    {
        foreach (var renderer in sceneRenderers)
        {
            if (renderer != null)
            {
                renderer.shadowCastingMode = enabled ? ShadowCastingMode.On : ShadowCastingMode.Off;
                renderer.receiveShadows = enabled;
            }
        }
        
        Debug.Log($"阴影已{(enabled ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 设置LOD组
    /// </summary>
    /// <param name="quality">LOD质量 (0-1)</param>
    public void SetLODQuality(float quality)
    {
        quality = Mathf.Clamp01(quality);
        
        foreach (var renderer in sceneRenderers)
        {
            if (renderer != null)
            {
                var lodGroup = renderer.GetComponent<LODGroup>();
                if (lodGroup != null)
                {
                    LOD[] lods = lodGroup.GetLODs();
                    for (int i = 0; i < lods.Length; i++)
                    {
                        lods[i].screenRelativeTransitionHeight = quality * (1.0f - i * 0.3f);
                    }
                    lodGroup.SetLODs(lods);
                }
            }
        }
        
        Debug.Log($"LOD质量已设置为: {quality}");
    }
    
    /// <summary>
    /// 执行自定义渲染
    /// </summary>
    public void ExecuteCustomRendering()
    {
        if (!enableCustomRendering || customRenderTexture == null) return;
        
        // 设置渲染目标
        RenderTexture.active = customRenderTexture;
        
        // 清除渲染目标
        GL.Clear(true, true, Color.clear);
        
        // 执行渲染命令
        if (commandBuffer != null)
        {
            Graphics.ExecuteCommandBuffer(commandBuffer);
        }
        
        // 恢复默认渲染目标
        RenderTexture.active = null;
        
        Debug.Log("自定义渲染执行完成");
    }
    
    /// <summary>
    /// 添加渲染事件
    /// </summary>
    public void AddRenderEvent()
    {
        if (!enableRenderEvents || targetCamera == null) return;
        
        // 添加渲染事件
        targetCamera.AddCommandBuffer(CameraEvent.AfterEverything, commandBuffer);
        
        Debug.Log("渲染事件已添加");
    }
    
    /// <summary>
    /// 移除渲染事件
    /// </summary>
    public void RemoveRenderEvent()
    {
        if (targetCamera == null || commandBuffer == null) return;
        
        // 移除渲染事件
        targetCamera.RemoveCommandBuffer(CameraEvent.AfterEverything, commandBuffer);
        
        Debug.Log("渲染事件已移除");
    }
    
    /// <summary>
    /// 设置相机渲染路径
    /// </summary>
    /// <param name="renderingPath">渲染路径</param>
    public void SetCameraRenderingPath(RenderingPath renderingPath)
    {
        if (targetCamera == null) return;
        
        targetCamera.renderingPath = renderingPath;
        Debug.Log($"相机渲染路径已设置为: {renderingPath}");
    }
    
    /// <summary>
    /// 设置相机清除标志
    /// </summary>
    /// <param name="clearFlags">清除标志</param>
    public void SetCameraClearFlags(CameraClearFlags clearFlags)
    {
        if (targetCamera == null) return;
        
        targetCamera.clearFlags = clearFlags;
        Debug.Log($"相机清除标志已设置为: {clearFlags}");
    }
    
    /// <summary>
    /// 设置相机背景色
    /// </summary>
    /// <param name="backgroundColor">背景色</param>
    public void SetCameraBackgroundColor(Color backgroundColor)
    {
        if (targetCamera == null) return;
        
        targetCamera.backgroundColor = backgroundColor;
        Debug.Log($"相机背景色已设置为: {backgroundColor}");
    }
    
    /// <summary>
    /// 获取渲染器信息
    /// </summary>
    public void GetRendererInfo()
    {
        Debug.Log("=== 渲染器信息 ===");
        
        foreach (var renderer in sceneRenderers)
        {
            if (renderer != null)
            {
                Debug.Log($"渲染器: {renderer.name}");
                Debug.Log($"  材质: {renderer.material?.name ?? "无"}");
                Debug.Log($"  排序顺序: {renderer.sortingOrder}");
                Debug.Log($"  层级: {renderer.gameObject.layer}");
                Debug.Log($"  阴影投射: {renderer.shadowCastingMode}");
                Debug.Log($"  接收阴影: {renderer.receiveShadows}");
                Debug.Log($"  启用状态: {renderer.enabled}");
            }
        }
    }
    
    /// <summary>
    /// 优化渲染性能
    /// </summary>
    public void OptimizeRendering()
    {
        Debug.Log("开始渲染性能优化...");
        
        // 设置静态批处理
        foreach (var renderer in sceneRenderers)
        {
            if (renderer != null && renderer.gameObject.isStatic)
            {
                StaticBatchingUtility.Combine(renderer.gameObject);
            }
        }
        
        // 设置动态批处理
        QualitySettings.maxQueuedFrames = 2;
        QualitySettings.vSyncCount = 0;
        
        Debug.Log("渲染性能优化完成");
    }
    
    private void Update()
    {
        // 更新渲染统计
        if (Time.frameCount % 60 == 0) // 每60帧更新一次
        {
            GetRenderingStatistics();
        }
    }
    
    private void OnDestroy()
    {
        // 清理资源
        if (commandBuffer != null)
        {
            commandBuffer.Release();
        }
        
        if (customRenderTexture != null)
        {
            customRenderTexture.Release();
        }
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 600));
        GUILayout.Label("渲染系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 渲染统计
        GUILayout.Label($"绘制调用: {drawCalls}");
        GUILayout.Label($"三角形数: {triangles}");
        GUILayout.Label($"顶点数: {vertices}");
        GUILayout.Label($"帧时间: {frameTime * 1000:F2}ms");
        
        GUILayout.Space(10);
        
        // 渲染设置
        if (GUILayout.Button("切换渲染管线"))
        {
            ToggleRenderPipeline();
        }
        
        if (GUILayout.Button("启用所有渲染器"))
        {
            SetRenderersEnabled(true);
        }
        
        if (GUILayout.Button("禁用所有渲染器"))
        {
            SetRenderersEnabled(false);
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("启用阴影"))
        {
            SetShadowsEnabled(true);
        }
        
        if (GUILayout.Button("禁用阴影"))
        {
            SetShadowsEnabled(false);
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取渲染器信息"))
        {
            GetRendererInfo();
        }
        
        if (GUILayout.Button("优化渲染性能"))
        {
            OptimizeRendering();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("执行自定义渲染"))
        {
            ExecuteCustomRendering();
        }
        
        if (GUILayout.Button("添加渲染事件"))
        {
            AddRenderEvent();
        }
        
        if (GUILayout.Button("移除渲染事件"))
        {
            RemoveRenderEvent();
        }
        
        GUILayout.Space(10);
        
        // 设置选项
        enableCustomRendering = GUILayout.Toggle(enableCustomRendering, "启用自定义渲染");
        enableRenderEvents = GUILayout.Toggle(enableRenderEvents, "启用渲染事件");
        enableCommandBuffer = GUILayout.Toggle(enableCommandBuffer, "启用命令缓冲区");
        
        GUILayout.EndArea();
    }
} 