using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System.IO;

/// <summary>
/// UnityEditor.AssetImporters 命名空间案例演示
/// 展示资产导入器的核心功能
/// </summary>
public class AssetImportersExample : MonoBehaviour
{
    [Header("导入器设置")]
    [SerializeField] private string customFileExtension = ".custom";
    [SerializeField] private bool enableCustomImporter = true;
    [SerializeField] private string importLogPath = "Assets/ImportLog.txt";
    
    [Header("导入统计")]
    [SerializeField] private int totalImportedFiles = 0;
    [SerializeField] private int successfulImports = 0;
    [SerializeField] private int failedImports = 0;
    
    private void Start()
    {
        InitializeAssetImporters();
    }
    
    /// <summary>
    /// 初始化资产导入器
    /// </summary>
    private void InitializeAssetImporters()
    {
        Debug.Log("资产导入器系统初始化完成");
        
        // 注册自定义导入器
        if (enableCustomImporter)
        {
            RegisterCustomImporter();
        }
    }
    
    /// <summary>
    /// 注册自定义导入器
    /// </summary>
    private void RegisterCustomImporter()
    {
        Debug.Log($"注册自定义导入器，文件扩展名: {customFileExtension}");
    }
    
    /// <summary>
    /// 创建测试文件
    /// </summary>
    public void CreateTestFiles()
    {
        string testDirectory = "Assets/TestFiles";
        
        // 确保目录存在
        if (!Directory.Exists(testDirectory))
        {
            Directory.CreateDirectory(testDirectory);
        }
        
        // 创建自定义格式文件
        for (int i = 0; i < 5; i++)
        {
            string fileName = $"test_file_{i}{customFileExtension}";
            string filePath = Path.Combine(testDirectory, fileName);
            
            string content = $"这是测试文件 {i} 的内容\n创建时间: {System.DateTime.Now}\n自定义数据: {Random.Range(1, 100)}";
            File.WriteAllText(filePath, content);
            
            Debug.Log($"创建测试文件: {filePath}");
        }
        
        // 刷新资产数据库
        AssetDatabase.Refresh();
        
        Debug.Log("测试文件创建完成");
    }
    
    /// <summary>
    /// 清理测试文件
    /// </summary>
    public void CleanupTestFiles()
    {
        string testDirectory = "Assets/TestFiles";
        
        if (Directory.Exists(testDirectory))
        {
            Directory.Delete(testDirectory, true);
            AssetDatabase.Refresh();
            Debug.Log("测试文件已清理");
        }
    }
    
    /// <summary>
    /// 获取导入统计信息
    /// </summary>
    public void GetImportStatistics()
    {
        Debug.Log("=== 导入统计信息 ===");
        Debug.Log($"总导入文件数: {totalImportedFiles}");
        Debug.Log($"成功导入数: {successfulImports}");
        Debug.Log($"失败导入数: {failedImports}");
        Debug.Log($"成功率: {(totalImportedFiles > 0 ? (float)successfulImports / totalImportedFiles * 100 : 0):F1}%");
    }
    
    /// <summary>
    /// 重置统计信息
    /// </summary>
    public void ResetStatistics()
    {
        totalImportedFiles = 0;
        successfulImports = 0;
        failedImports = 0;
        Debug.Log("统计信息已重置");
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 300));
        GUILayout.Label("资产导入器演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 设置
        GUILayout.Label($"自定义文件扩展名: {customFileExtension}");
        enableCustomImporter = GUILayout.Toggle(enableCustomImporter, "启用自定义导入器");
        
        GUILayout.Space(10);
        
        // 操作按钮
        if (GUILayout.Button("创建测试文件"))
        {
            CreateTestFiles();
        }
        
        if (GUILayout.Button("清理测试文件"))
        {
            CleanupTestFiles();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取导入统计"))
        {
            GetImportStatistics();
        }
        
        if (GUILayout.Button("重置统计"))
        {
            ResetStatistics();
        }
        
        GUILayout.Space(10);
        
        // 统计信息显示
        GUILayout.Label($"总导入文件数: {totalImportedFiles}");
        GUILayout.Label($"成功导入数: {successfulImports}");
        GUILayout.Label($"失败导入数: {failedImports}");
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 自定义资产导入器示例
/// </summary>
[ScriptedImporter(1, "custom")]
public class CustomAssetImporter : ScriptedImporter
{
    [Header("导入设置")]
    public bool enableLogging = true;
    public string customData = "";
    
    public override void OnImportAsset(AssetImportContext ctx)
    {
        Debug.Log($"开始导入自定义资产: {ctx.assetPath}");
        
        // 读取文件内容
        string fileContent = File.ReadAllText(ctx.assetPath);
        
        // 创建自定义资产
        var customAsset = ScriptableObject.CreateInstance<CustomAsset>();
        customAsset.fileContent = fileContent;
        customAsset.importTime = System.DateTime.Now;
        customAsset.customData = customData;
        
        // 设置资产名称
        string assetName = Path.GetFileNameWithoutExtension(ctx.assetPath);
        customAsset.name = assetName;
        
        // 添加资产到导入上下文
        ctx.AddObjectToAsset("main", customAsset);
        ctx.SetMainObject(customAsset);
        
        Debug.Log($"自定义资产导入完成: {assetName}");
    }
}

/// <summary>
/// 自定义资产数据
/// </summary>
public class CustomAsset : ScriptableObject
{
    [Header("文件内容")]
    [TextArea(5, 10)]
    public string fileContent;
    
    [Header("导入信息")]
    public System.DateTime importTime;
    public string customData;
    
    /// <summary>
    /// 获取文件内容
    /// </summary>
    /// <returns>文件内容</returns>
    public string GetContent()
    {
        return fileContent;
    }
    
    /// <summary>
    /// 设置文件内容
    /// </summary>
    /// <param name="content">新内容</param>
    public void SetContent(string content)
    {
        fileContent = content;
        EditorUtility.SetDirty(this);
    }
    
    /// <summary>
    /// 获取导入时间
    /// </summary>
    /// <returns>导入时间</returns>
    public System.DateTime GetImportTime()
    {
        return importTime;
    }
    
    /// <summary>
    /// 获取自定义数据
    /// </summary>
    /// <returns>自定义数据</returns>
    public string GetCustomData()
    {
        return customData;
    }
    
    /// <summary>
    /// 设置自定义数据
    /// </summary>
    /// <param name="data">新数据</param>
    public void SetCustomData(string data)
    {
        customData = data;
        EditorUtility.SetDirty(this);
    }
}

/// <summary>
/// 自定义资产编辑器
/// </summary>
[CustomEditor(typeof(CustomAsset))]
public class CustomAssetEditor : Editor
{
    private CustomAsset customAsset;
    private bool showContent = true;
    private bool showInfo = true;
    
    private void OnEnable()
    {
        customAsset = (CustomAsset)target;
    }
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("自定义资产编辑器", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();
        
        // 内容显示
        showContent = EditorGUILayout.Foldout(showContent, "文件内容");
        if (showContent)
        {
            EditorGUI.BeginChangeCheck();
            string newContent = EditorGUILayout.TextArea(customAsset.fileContent, GUILayout.Height(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(customAsset, "修改文件内容");
                customAsset.SetContent(newContent);
            }
        }
        
        EditorGUILayout.Space();
        
        // 信息显示
        showInfo = EditorGUILayout.Foldout(showInfo, "导入信息");
        if (showInfo)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("导入时间", customAsset.importTime.ToString());
            EditorGUILayout.TextField("自定义数据", customAsset.customData);
            EditorGUI.EndDisabledGroup();
        }
        
        EditorGUILayout.Space();
        
        // 操作按钮
        if (GUILayout.Button("重新导入"))
        {
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(customAsset));
        }
        
        if (GUILayout.Button("导出内容"))
        {
            ExportContent();
        }
        
        if (GUILayout.Button("清空内容"))
        {
            if (EditorUtility.DisplayDialog("确认", "确定要清空文件内容吗？", "确定", "取消"))
            {
                Undo.RecordObject(customAsset, "清空文件内容");
                customAsset.SetContent("");
            }
        }
    }
    
    /// <summary>
    /// 导出内容
    /// </summary>
    private void ExportContent()
    {
        string path = EditorUtility.SaveFilePanel("导出内容", "", customAsset.name, "txt");
        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, customAsset.fileContent);
            Debug.Log($"内容已导出到: {path}");
        }
    }
}

/// <summary>
/// 批量导入处理器
/// </summary>
public class BatchImportProcessor : AssetPostprocessor
{
    private static int processedFiles = 0;
    private static int totalFiles = 0;
    
    /// <summary>
    /// 导入前处理
    /// </summary>
    private static void OnPreprocessAsset()
    {
        totalFiles++;
        Debug.Log($"开始处理资产: {assetImporter.assetPath}");
    }
    
    /// <summary>
    /// 导入后处理
    /// </summary>
    private static void OnPostprocessAsset()
    {
        processedFiles++;
        Debug.Log($"资产处理完成: {assetImporter.assetPath} ({processedFiles}/{totalFiles})");
    }
    
    /// <summary>
    /// 处理自定义文件
    /// </summary>
    private static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        Debug.Log($"批量处理完成 - 导入: {importedAssets.Length}, 删除: {deletedAssets.Length}, 移动: {movedAssets.Length}");
        
        // 重置计数器
        processedFiles = 0;
        totalFiles = 0;
    }
} 