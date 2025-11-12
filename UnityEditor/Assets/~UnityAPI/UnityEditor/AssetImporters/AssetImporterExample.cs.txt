using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// UnityEditor.AssetImporters 命名空间案例演示
/// 展示资源导入器的核心功能
/// </summary>
public class AssetImporterExample : MonoBehaviour
{
    [Header("资源导入设置")]
    [SerializeField] private string importPath = "Assets/";
    [SerializeField] private string[] supportedExtensions = { ".png", ".jpg", ".jpeg", ".tga", ".psd", ".fbx", ".obj", ".wav", ".mp3", ".ogg" };
    [SerializeField] private bool autoImport = true;
    [SerializeField] private bool recursiveImport = true;
    
    [Header("纹理导入设置")]
    [SerializeField] private TextureImporterType textureType = TextureImporterType.Default;
    [SerializeField] private TextureImporterFormat textureFormat = TextureImporterFormat.Automatic;
    [SerializeField] private int maxTextureSize = 2048;
    [SerializeField] private bool generateMipMaps = true;
    [SerializeField] private FilterMode filterMode = FilterMode.Bilinear;
    [SerializeField] private TextureWrapMode wrapMode = TextureWrapMode.Clamp;
    
    [Header("模型导入设置")]
    [SerializeField] private ModelImporterAnimationType animationType = ModelImporterAnimationType.None;
    [SerializeField] private bool importBlendShapes = false;
    [SerializeField] private bool importVisibility = false;
    [SerializeField] private bool importCameras = false;
    [SerializeField] private bool importLights = false;
    [SerializeField] private float scaleFactor = 1.0f;
    
    [Header("音频导入设置")]
    [SerializeField] private AudioClipLoadType audioLoadType = AudioClipLoadType.Streaming;
    [SerializeField] private AudioCompressionFormat audioCompressionFormat = AudioCompressionFormat.Vorbis;
    [SerializeField] private float audioQuality = 0.5f;
    [SerializeField] private bool forceToMono = false;
    [SerializeField] private bool normalize = false;
    
    [Header("导入状态")]
    [SerializeField] private bool isImporting = false;
    [SerializeField] private int importedCount = 0;
    [SerializeField] private int failedCount = 0;
    [SerializeField] private List<string> importedAssets = new List<string>();
    [SerializeField] private List<string> failedAssets = new List<string>();
    
    // 导入事件
    private System.Action<string> onAssetImported;
    private System.Action<string> onAssetFailed;
    private System.Action onImportComplete;
    
    private void Start()
    {
        InitializeAssetImporter();
    }
    
    /// <summary>
    /// 初始化资源导入器
    /// </summary>
    private void InitializeAssetImporter()
    {
        // 设置默认导入路径
        if (string.IsNullOrEmpty(importPath))
        {
            importPath = "Assets/";
        }
        
        // 确保路径存在
        if (!Directory.Exists(importPath))
        {
            Directory.CreateDirectory(importPath);
        }
        
        // 设置资源导入回调
        AssetPostprocessor.OnPostprocessAllAssets += OnPostprocessAllAssets;
        
        Debug.Log("资源导入器初始化完成");
    }
    
    /// <summary>
    /// 资源后处理回调
    /// </summary>
    /// <param name="importedAssets">导入的资源</param>
    /// <param name="deletedAssets">删除的资源</param>
    /// <param name="movedAssets">移动的资源</param>
    /// <param name="movedFromAssetPaths">移动前的路径</param>
    private void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            Debug.Log($"资源已导入: {assetPath}");
            onAssetImported?.Invoke(assetPath);
        }
        
        foreach (string assetPath in deletedAssets)
        {
            Debug.Log($"资源已删除: {assetPath}");
        }
        
        foreach (string assetPath in movedAssets)
        {
            Debug.Log($"资源已移动: {assetPath}");
        }
    }
    
    /// <summary>
    /// 导入指定路径的资源
    /// </summary>
    /// <param name="path">资源路径</param>
    public void ImportAssets(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("导入路径不能为空");
            return;
        }
        
        if (!Directory.Exists(path))
        {
            Debug.LogError($"路径不存在: {path}");
            return;
        }
        
        isImporting = true;
        importedCount = 0;
        failedCount = 0;
        importedAssets.Clear();
        failedAssets.Clear();
        
        Debug.Log($"开始导入资源: {path}");
        
        // 获取所有支持的文件
        List<string> filesToImport = GetFilesToImport(path);
        
        foreach (string filePath in filesToImport)
        {
            try
            {
                ImportSingleAsset(filePath);
                importedCount++;
                importedAssets.Add(filePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导入失败 {filePath}: {e.Message}");
                failedCount++;
                failedAssets.Add(filePath);
            }
        }
        
        isImporting = false;
        
        Debug.Log($"导入完成: 成功 {importedCount} 个, 失败 {failedCount} 个");
        onImportComplete?.Invoke();
    }
    
    /// <summary>
    /// 获取需要导入的文件列表
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>文件列表</returns>
    private List<string> GetFilesToImport(string path)
    {
        List<string> files = new List<string>();
        
        SearchOption searchOption = recursiveImport ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        
        foreach (string extension in supportedExtensions)
        {
            string[] foundFiles = Directory.GetFiles(path, "*" + extension, searchOption);
            files.AddRange(foundFiles);
        }
        
        return files;
    }
    
    /// <summary>
    /// 导入单个资源
    /// </summary>
    /// <param name="filePath">文件路径</param>
    private void ImportSingleAsset(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLower();
        
        switch (extension)
        {
            case ".png":
            case ".jpg":
            case ".jpeg":
            case ".tga":
            case ".psd":
                ImportTexture(filePath);
                break;
                
            case ".fbx":
            case ".obj":
                ImportModel(filePath);
                break;
                
            case ".wav":
            case ".mp3":
            case ".ogg":
                ImportAudio(filePath);
                break;
                
            default:
                Debug.LogWarning($"不支持的文件类型: {extension}");
                break;
        }
    }
    
    /// <summary>
    /// 导入纹理
    /// </summary>
    /// <param name="filePath">文件路径</param>
    private void ImportTexture(string filePath)
    {
        string assetPath = ConvertToAssetPath(filePath);
        
        // 获取或创建纹理导入器
        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (importer == null)
        {
            Debug.LogError($"无法获取纹理导入器: {assetPath}");
            return;
        }
        
        // 设置纹理导入参数
        importer.textureType = textureType;
        importer.maxTextureSize = maxTextureSize;
        importer.generateMipMaps = generateMipMaps;
        importer.filterMode = filterMode;
        importer.wrapMode = wrapMode;
        
        // 设置平台特定设置
        TextureImporterPlatformSettings platformSettings = new TextureImporterPlatformSettings();
        platformSettings.name = "Default";
        platformSettings.overridden = true;
        platformSettings.format = textureFormat;
        platformSettings.maxTextureSize = maxTextureSize;
        platformSettings.compressionQuality = 50;
        
        importer.SetPlatformTextureSettings(platformSettings);
        
        // 应用设置
        importer.SaveAndReimport();
        
        Debug.Log($"纹理导入完成: {assetPath}");
    }
    
    /// <summary>
    /// 导入模型
    /// </summary>
    /// <param name="filePath">文件路径</param>
    private void ImportModel(string filePath)
    {
        string assetPath = ConvertToAssetPath(filePath);
        
        // 获取或创建模型导入器
        ModelImporter importer = AssetImporter.GetAtPath(assetPath) as ModelImporter;
        if (importer == null)
        {
            Debug.LogError($"无法获取模型导入器: {assetPath}");
            return;
        }
        
        // 设置模型导入参数
        importer.animationType = animationType;
        importer.importBlendShapes = importBlendShapes;
        importer.importVisibility = importVisibility;
        importer.importCameras = importCameras;
        importer.importLights = importLights;
        importer.globalScale = scaleFactor;
        
        // 设置动画导入参数
        if (animationType != ModelImporterAnimationType.None)
        {
            importer.importAnimation = true;
            importer.animationCompression = ModelImporterAnimationCompression.KeyframeReduction;
        }
        
        // 应用设置
        importer.SaveAndReimport();
        
        Debug.Log($"模型导入完成: {assetPath}");
    }
    
    /// <summary>
    /// 导入音频
    /// </summary>
    /// <param name="filePath">文件路径</param>
    private void ImportAudio(string filePath)
    {
        string assetPath = ConvertToAssetPath(filePath);
        
        // 获取或创建音频导入器
        AudioImporter importer = AssetImporter.GetAtPath(assetPath) as AudioImporter;
        if (importer == null)
        {
            Debug.LogError($"无法获取音频导入器: {assetPath}");
            return;
        }
        
        // 设置音频导入参数
        AudioImporterSampleSettings sampleSettings = new AudioImporterSampleSettings();
        sampleSettings.loadType = audioLoadType;
        sampleSettings.compressionFormat = audioCompressionFormat;
        sampleSettings.quality = audioQuality;
        sampleSettings.forceToMono = forceToMono;
        sampleSettings.normalize = normalize;
        
        importer.defaultSampleSettings = sampleSettings;
        
        // 应用设置
        importer.SaveAndReimport();
        
        Debug.Log($"音频导入完成: {assetPath}");
    }
    
    /// <summary>
    /// 转换文件路径为资源路径
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>资源路径</returns>
    private string ConvertToAssetPath(string filePath)
    {
        // 将绝对路径转换为相对于Assets的路径
        string fullPath = Path.GetFullPath(filePath);
        string assetsPath = Path.GetFullPath("Assets");
        
        if (fullPath.StartsWith(assetsPath))
        {
            return fullPath.Substring(assetsPath.Length).Replace('\\', '/').TrimStart('/');
        }
        
        return filePath;
    }
    
    /// <summary>
    /// 批量导入资源
    /// </summary>
    public void BatchImportAssets()
    {
        ImportAssets(importPath);
    }
    
    /// <summary>
    /// 重新导入指定资源
    /// </summary>
    /// <param name="assetPath">资源路径</param>
    public void ReimportAsset(string assetPath)
    {
        if (string.IsNullOrEmpty(assetPath))
        {
            Debug.LogError("资源路径不能为空");
            return;
        }
        
        AssetImporter importer = AssetImporter.GetAtPath(assetPath);
        if (importer != null)
        {
            importer.SaveAndReimport();
            Debug.Log($"资源重新导入完成: {assetPath}");
        }
        else
        {
            Debug.LogError($"无法找到资源导入器: {assetPath}");
        }
    }
    
    /// <summary>
    /// 重新导入所有资源
    /// </summary>
    public void ReimportAllAssets()
    {
        Debug.Log("开始重新导入所有资源");
        AssetDatabase.Refresh();
        Debug.Log("所有资源重新导入完成");
    }
    
    /// <summary>
    /// 设置纹理导入参数
    /// </summary>
    /// <param name="type">纹理类型</param>
    /// <param name="format">纹理格式</param>
    /// <param name="maxSize">最大尺寸</param>
    public void SetTextureImportSettings(TextureImporterType type, TextureImporterFormat format, int maxSize)
    {
        textureType = type;
        textureFormat = format;
        maxTextureSize = maxSize;
        
        Debug.Log($"纹理导入设置已更新: 类型={type}, 格式={format}, 最大尺寸={maxSize}");
    }
    
    /// <summary>
    /// 设置模型导入参数
    /// </summary>
    /// <param name="animType">动画类型</param>
    /// <param name="scale">缩放因子</param>
    public void SetModelImportSettings(ModelImporterAnimationType animType, float scale)
    {
        animationType = animType;
        scaleFactor = scale;
        
        Debug.Log($"模型导入设置已更新: 动画类型={animType}, 缩放因子={scale}");
    }
    
    /// <summary>
    /// 设置音频导入参数
    /// </summary>
    /// <param name="loadType">加载类型</param>
    /// <param name="compressionFormat">压缩格式</param>
    /// <param name="quality">质量</param>
    public void SetAudioImportSettings(AudioClipLoadType loadType, AudioCompressionFormat compressionFormat, float quality)
    {
        audioLoadType = loadType;
        audioCompressionFormat = compressionFormat;
        audioQuality = Mathf.Clamp01(quality);
        
        Debug.Log($"音频导入设置已更新: 加载类型={loadType}, 压缩格式={compressionFormat}, 质量={audioQuality}");
    }
    
    /// <summary>
    /// 获取导入信息
    /// </summary>
    public void GetImportInfo()
    {
        Debug.Log("=== 资源导入器信息 ===");
        Debug.Log($"导入路径: {importPath}");
        Debug.Log($"自动导入: {autoImport}");
        Debug.Log($"递归导入: {recursiveImport}");
        Debug.Log($"支持的文件类型: {string.Join(", ", supportedExtensions)}");
        Debug.Log($"导入状态: {(isImporting ? "导入中" : "空闲")}");
        Debug.Log($"已导入数量: {importedCount}");
        Debug.Log($"失败数量: {failedCount}");
        
        Debug.Log("纹理导入设置:");
        Debug.Log($"  纹理类型: {textureType}");
        Debug.Log($"  纹理格式: {textureFormat}");
        Debug.Log($"  最大尺寸: {maxTextureSize}");
        Debug.Log($"  生成MipMap: {generateMipMaps}");
        Debug.Log($"  过滤模式: {filterMode}");
        Debug.Log($"  环绕模式: {wrapMode}");
        
        Debug.Log("模型导入设置:");
        Debug.Log($"  动画类型: {animationType}");
        Debug.Log($"  导入混合形状: {importBlendShapes}");
        Debug.Log($"  导入可见性: {importVisibility}");
        Debug.Log($"  导入相机: {importCameras}");
        Debug.Log($"  导入灯光: {importLights}");
        Debug.Log($"  缩放因子: {scaleFactor}");
        
        Debug.Log("音频导入设置:");
        Debug.Log($"  加载类型: {audioLoadType}");
        Debug.Log($"  压缩格式: {audioCompressionFormat}");
        Debug.Log($"  质量: {audioQuality}");
        Debug.Log($"  强制单声道: {forceToMono}");
        Debug.Log($"  标准化: {normalize}");
        
        if (importedAssets.Count > 0)
        {
            Debug.Log("已导入的资源:");
            foreach (string asset in importedAssets)
            {
                Debug.Log($"  {asset}");
            }
        }
        
        if (failedAssets.Count > 0)
        {
            Debug.Log("导入失败的资源:");
            foreach (string asset in failedAssets)
            {
                Debug.Log($"  {asset}");
            }
        }
    }
    
    /// <summary>
    /// 重置导入设置
    /// </summary>
    public void ResetImportSettings()
    {
        // 重置纹理设置
        textureType = TextureImporterType.Default;
        textureFormat = TextureImporterFormat.Automatic;
        maxTextureSize = 2048;
        generateMipMaps = true;
        filterMode = FilterMode.Bilinear;
        wrapMode = TextureWrapMode.Clamp;
        
        // 重置模型设置
        animationType = ModelImporterAnimationType.None;
        importBlendShapes = false;
        importVisibility = false;
        importCameras = false;
        importLights = false;
        scaleFactor = 1.0f;
        
        // 重置音频设置
        audioLoadType = AudioClipLoadType.Streaming;
        audioCompressionFormat = AudioCompressionFormat.Vorbis;
        audioQuality = 0.5f;
        forceToMono = false;
        normalize = false;
        
        Debug.Log("导入设置已重置");
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("资源导入器演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 导入状态
        GUILayout.Label($"导入状态: {(isImporting ? "导入中" : "空闲")}");
        GUILayout.Label($"已导入: {importedCount}");
        GUILayout.Label($"失败: {failedCount}");
        
        GUILayout.Space(10);
        
        // 导入路径
        GUILayout.Label("导入路径:");
        importPath = GUILayout.TextField(importPath);
        
        GUILayout.Space(10);
        
        // 控制按钮
        if (GUILayout.Button("批量导入"))
        {
            BatchImportAssets();
        }
        
        if (GUILayout.Button("重新导入所有"))
        {
            ReimportAllAssets();
        }
        
        GUILayout.Space(10);
        
        // 纹理设置
        GUILayout.Label("纹理导入设置:", EditorStyles.boldLabel);
        
        textureType = (TextureImporterType)EditorGUILayout.EnumPopup("纹理类型", textureType);
        textureFormat = (TextureImporterFormat)EditorGUILayout.EnumPopup("纹理格式", textureFormat);
        maxTextureSize = EditorGUILayout.IntField("最大尺寸", maxTextureSize);
        generateMipMaps = EditorGUILayout.Toggle("生成MipMap", generateMipMaps);
        filterMode = (FilterMode)EditorGUILayout.EnumPopup("过滤模式", filterMode);
        wrapMode = (TextureWrapMode)EditorGUILayout.EnumPopup("环绕模式", wrapMode);
        
        GUILayout.Space(10);
        
        // 模型设置
        GUILayout.Label("模型导入设置:", EditorStyles.boldLabel);
        
        animationType = (ModelImporterAnimationType)EditorGUILayout.EnumPopup("动画类型", animationType);
        importBlendShapes = EditorGUILayout.Toggle("导入混合形状", importBlendShapes);
        importVisibility = EditorGUILayout.Toggle("导入可见性", importVisibility);
        importCameras = EditorGUILayout.Toggle("导入相机", importCameras);
        importLights = EditorGUILayout.Toggle("导入灯光", importLights);
        scaleFactor = EditorGUILayout.FloatField("缩放因子", scaleFactor);
        
        GUILayout.Space(10);
        
        // 音频设置
        GUILayout.Label("音频导入设置:", EditorStyles.boldLabel);
        
        audioLoadType = (AudioClipLoadType)EditorGUILayout.EnumPopup("加载类型", audioLoadType);
        audioCompressionFormat = (AudioCompressionFormat)EditorGUILayout.EnumPopup("压缩格式", audioCompressionFormat);
        audioQuality = EditorGUILayout.Slider("质量", audioQuality, 0f, 1f);
        forceToMono = EditorGUILayout.Toggle("强制单声道", forceToMono);
        normalize = EditorGUILayout.Toggle("标准化", normalize);
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取导入信息"))
        {
            GetImportInfo();
        }
        
        if (GUILayout.Button("重置设置"))
        {
            ResetImportSettings();
        }
        
        GUILayout.EndArea();
    }
    
    private void OnDestroy()
    {
        // 移除回调
        AssetPostprocessor.OnPostprocessAllAssets -= OnPostprocessAllAssets;
    }
} 