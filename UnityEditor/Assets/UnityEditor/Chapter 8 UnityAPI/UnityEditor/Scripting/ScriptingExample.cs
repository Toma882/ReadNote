using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting;
using UnityEditor.Scripting.ScriptCompilation;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// UnityEditor.Scripting 命名空间案例演示
/// 展示脚本系统的使用，包括编译、脚本管理、代码生成等
/// </summary>
public class ScriptingExample : MonoBehaviour
{
    [Header("脚本系统配置")]
    [SerializeField] private bool enableScripting = true; // 是否启用脚本系统
    [SerializeField] private bool enableCompilation = true; // 是否启用编译
    [SerializeField] private bool enableCodeGeneration = true; // 是否启用代码生成
    [SerializeField] private bool enableScriptValidation = true; // 是否启用脚本验证
    [SerializeField] private bool enableScriptAnalysis = true; // 是否启用脚本分析
    [SerializeField] private bool enableScriptProfiling = true; // 是否启用脚本分析
    [SerializeField] private bool enableScriptDebugging = true; // 是否启用脚本调试
    [SerializeField] private bool enableScriptOptimization = true; // 是否启用脚本优化
    [SerializeField] private bool enableScriptHotReload = true; // 是否启用脚本热重载
    [SerializeField] private bool enableScriptBackup = true; // 是否启用脚本备份
    
    [Header("编译配置")]
    [SerializeField] private ScriptCompilationOptions compilationOptions = ScriptCompilationOptions.None; // 编译选项
    [SerializeField] private ScriptingBackend scriptingBackend = ScriptingBackend.Mono2x; // 脚本后端
    [SerializeField] private ApiCompatibilityLevel apiCompatibilityLevel = ApiCompatibilityLevel.NET_4_6; // API兼容级别
    [SerializeField] private bool enableUnsafeCode = false; // 是否启用不安全代码
    [SerializeField] private bool enableDeterministicCompilation = false; // 是否启用确定性编译
    [SerializeField] private bool enableScriptDebugging = true; // 是否启用脚本调试
    [SerializeField] private bool enableScriptOptimization = false; // 是否启用脚本优化
    [SerializeField] private bool enableIncrementalCompilation = true; // 是否启用增量编译
    [SerializeField] private bool enableAssemblyValidation = true; // 是否启用程序集验证
    [SerializeField] private bool enableScriptUpdater = true; // 是否启用脚本更新
    
    [Header("脚本状态")]
    [SerializeField] private ScriptingStatus scriptingStatus = ScriptingStatus.Idle; // 脚本状态
    [SerializeField] private bool isCompiling = false; // 是否正在编译
    [SerializeField] private bool isGeneratingCode = false; // 是否正在生成代码
    [SerializeField] private bool isAnalyzingScripts = false; // 是否正在分析脚本
    [SerializeField] private float compilationProgress = 0f; // 编译进度
    [SerializeField] private string compilationMessage = ""; // 编译消息
    [SerializeField] private int totalScripts = 0; // 总脚本数
    [SerializeField] private int compiledScripts = 0; // 已编译脚本数
    [SerializeField] private int failedScripts = 0; // 失败脚本数
    [SerializeField] private int warningsCount = 0; // 警告数
    [SerializeField] private int errorsCount = 0; // 错误数
    
    [Header("脚本信息")]
    [SerializeField] private ScriptInfo[] scriptInfos = new ScriptInfo[0]; // 脚本信息
    [SerializeField] private ScriptInfo[] compiledScriptInfos = new ScriptInfo[0]; // 已编译脚本信息
    [SerializeField] private ScriptInfo[] failedScriptInfos = new ScriptInfo[0]; // 失败脚本信息
    [SerializeField] private string[] scriptPaths = new string[0]; // 脚本路径
    [SerializeField] private string[] scriptNames = new string[0]; // 脚本名称
    [SerializeField] private ScriptType[] scriptTypes = new ScriptType[0]; // 脚本类型
    [SerializeField] private bool[] scriptEnabled = new bool[0]; // 脚本是否启用
    
    [Header("编译统计")]
    [SerializeField] private CompilationStatistics compilationStats = new CompilationStatistics();
    [SerializeField] private Dictionary<string, float> scriptCompileTimes = new Dictionary<string, float>(); // 脚本编译时间
    [SerializeField] private Dictionary<string, int> scriptErrorCounts = new Dictionary<string, int>(); // 脚本错误数
    [SerializeField] private Dictionary<string, int> scriptWarningCounts = new Dictionary<string, int>(); // 脚本警告数
    [SerializeField] private List<CompilationError> compilationErrors = new List<CompilationError>(); // 编译错误
    [SerializeField] private List<CompilationWarning> compilationWarnings = new List<CompilationWarning>(); // 编译警告
    
    [Header("代码生成")]
    [SerializeField] private bool enableAutoCodeGeneration = true; // 是否启用自动代码生成
    [SerializeField] private bool enableTemplateCodeGeneration = true; // 是否启用模板代码生成
    [SerializeField] private bool enableScriptableObjectGeneration = true; // 是否启用脚本对象生成
    [SerializeField] private bool enableEditorScriptGeneration = true; // 是否启用编辑器脚本生成
    [SerializeField] private bool enableRuntimeScriptGeneration = true; // 是否启用运行时脚本生成
    [SerializeField] private string codeGenerationPath = "GeneratedScripts/"; // 代码生成路径
    [SerializeField] private string templatePath = "ScriptTemplates/"; // 模板路径
    [SerializeField] private string[] codeTemplates = new string[0]; // 代码模板
    [SerializeField] private string[] generatedScripts = new string[0]; // 已生成脚本
    
    [Header("脚本分析")]
    [SerializeField] private bool enableCodeAnalysis = true; // 是否启用代码分析
    [SerializeField] private bool enablePerformanceAnalysis = true; // 是否启用性能分析
    [SerializeField] private bool enableMemoryAnalysis = true; // 是否启用内存分析
    [SerializeField] private bool enableSecurityAnalysis = true; // 是否启用安全分析
    [SerializeField] private bool enableStyleAnalysis = true; // 是否启用样式分析
    [SerializeField] private List<CodeAnalysisResult> analysisResults = new List<CodeAnalysisResult>(); // 代码分析结果
    [SerializeField] private List<PerformanceIssue> performanceIssues = new List<PerformanceIssue>(); // 性能问题
    [SerializeField] private List<MemoryIssue> memoryIssues = new List<MemoryIssue>(); // 内存问题
    [SerializeField] private List<SecurityIssue> securityIssues = new List<SecurityIssue>(); // 安全问题
    [SerializeField] private List<StyleIssue> styleIssues = new List<StyleIssue>(); // 样式问题
    
    [Header("脚本优化")]
    [SerializeField] private bool enableCodeOptimization = true;
    [SerializeField] private bool enableDeadCodeElimination = true;
    [SerializeField] private bool enableConstantFolding = true;
    [SerializeField] private bool enableInlining = true;
    [SerializeField] private bool enableLoopOptimization = true;
    [SerializeField] private bool enableMemoryOptimization = true;
    [SerializeField] private List<OptimizationResult> optimizationResults = new List<OptimizationResult>();
    [SerializeField] private float optimizationScore = 0f;
    [SerializeField] private int optimizationSuggestions = 0;
    
    [Header("脚本调试")]
    [SerializeField] private bool enableDebugging = true;
    [SerializeField] private bool enableBreakpoints = true;
    [SerializeField] private bool enableStepThrough = true;
    [SerializeField] private bool enableVariableInspection = true;
    [SerializeField] private bool enableCallStack = true;
    [SerializeField] private List<DebugBreakpoint> breakpoints = new List<DebugBreakpoint>();
    [SerializeField] private List<DebugVariable> debugVariables = new List<DebugVariable>();
    [SerializeField] private List<DebugCallStack> callStacks = new List<DebugCallStack>();
    
    private bool isInitialized = false;
    private float compilationStartTime = 0f;
    private StringBuilder reportBuilder = new StringBuilder();
    private List<string> pendingScripts = new List<string>();
    private List<string> compiledScriptsList = new List<string>();
    private List<string> failedScriptsList = new List<string>();

    private void Start()
    {
        InitializeScripting();
    }

    private void InitializeScripting()
    {
        if (!enableScripting) return;
        
        InitializeScriptState();
        InitializeCompilationSettings();
        InitializeCodeGeneration();
        InitializeScriptAnalysis();
        RegisterScriptingCallbacks();
        
        isInitialized = true;
        scriptingStatus = ScriptingStatus.Idle;
        Debug.Log("脚本系统初始化完成");
    }

    private void InitializeScriptState()
    {
        scriptingStatus = ScriptingStatus.Idle;
        isCompiling = false;
        isGeneratingCode = false;
        isAnalyzingScripts = false;
        compilationProgress = 0f;
        compilationMessage = "就绪";
        totalScripts = 0;
        compiledScripts = 0;
        failedScripts = 0;
        warningsCount = 0;
        errorsCount = 0;
        
        Debug.Log("脚本状态已初始化");
    }

    private void InitializeCompilationSettings()
    {
        compilationOptions = ScriptCompilationOptions.None;
        scriptingBackend = ScriptingBackend.Mono2x;
        apiCompatibilityLevel = ApiCompatibilityLevel.NET_4_6;
        enableUnsafeCode = false;
        enableDeterministicCompilation = false;
        enableScriptDebugging = true;
        enableScriptOptimization = false;
        enableIncrementalCompilation = true;
        enableAssemblyValidation = true;
        enableScriptUpdater = true;
        
        Debug.Log("编译设置已初始化");
    }

    private void InitializeCodeGeneration()
    {
        if (enableCodeGeneration)
        {
            codeGenerationPath = "GeneratedScripts/";
            templatePath = "ScriptTemplates/";
            codeTemplates = new string[0];
            generatedScripts = new string[0];
            
            // 创建目录
            if (!Directory.Exists(codeGenerationPath))
            {
                Directory.CreateDirectory(codeGenerationPath);
            }
            
            if (!Directory.Exists(templatePath))
            {
                Directory.CreateDirectory(templatePath);
            }
            
            Debug.Log("代码生成已初始化");
        }
    }

    private void InitializeScriptAnalysis()
    {
        if (enableScriptAnalysis)
        {
            analysisResults.Clear();
            performanceIssues.Clear();
            memoryIssues.Clear();
            securityIssues.Clear();
            styleIssues.Clear();
            
            Debug.Log("脚本分析已初始化");
        }
    }

    private void RegisterScriptingCallbacks()
    {
        // 注册编译回调
        if (enableCompilation)
        {
            CompilationPipeline.compilationStarted += OnCompilationStarted;
            CompilationPipeline.compilationFinished += OnCompilationFinished;
        }
        
        Debug.Log("脚本回调已注册");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        UpdateScriptingStatus();
        UpdateCompilationProgress();
        UpdateScriptInfo();
        
        if (enableAutoCodeGeneration)
        {
            CheckCodeGeneration();
        }
        
        if (enableScriptAnalysis)
        {
            CheckScriptAnalysis();
        }
    }

    private void UpdateScriptingStatus()
    {
        if (isCompiling)
        {
            scriptingStatus = ScriptingStatus.Compiling;
        }
        else if (isGeneratingCode)
        {
            scriptingStatus = ScriptingStatus.GeneratingCode;
        }
        else if (isAnalyzingScripts)
        {
            scriptingStatus = ScriptingStatus.Analyzing;
        }
        else
        {
            scriptingStatus = ScriptingStatus.Idle;
        }
    }

    private void UpdateCompilationProgress()
    {
        if (isCompiling)
        {
            // 模拟编译进度
            compilationProgress = Mathf.Clamp01((Time.time - compilationStartTime) / 5f);
        }
        else
        {
            compilationProgress = 0f;
        }
    }

    private void UpdateScriptInfo()
    {
        // 更新脚本信息
        UpdateScriptInfos();
        
        // 更新编译统计
        UpdateCompilationStatistics();
    }

    private void UpdateScriptInfos()
    {
        var scripts = FindAllScripts();
        scriptInfos = new ScriptInfo[scripts.Length];
        scriptPaths = new string[scripts.Length];
        scriptNames = new string[scripts.Length];
        scriptTypes = new ScriptType[scripts.Length];
        scriptEnabled = new bool[scripts.Length];
        
        for (int i = 0; i < scripts.Length; i++)
        {
            var scriptPath = scripts[i];
            var scriptName = Path.GetFileNameWithoutExtension(scriptPath);
            var scriptType = GetScriptType(scriptPath);
            
            scriptInfos[i] = new ScriptInfo
            {
                path = scriptPath,
                name = scriptName,
                type = scriptType,
                enabled = true,
                lastModified = File.GetLastWriteTime(scriptPath),
                size = new FileInfo(scriptPath).Length
            };
            
            scriptPaths[i] = scriptPath;
            scriptNames[i] = scriptName;
            scriptTypes[i] = scriptType;
            scriptEnabled[i] = true;
        }
        
        totalScripts = scripts.Length;
        
        // 更新已编译脚本信息
        compiledScriptInfos = new ScriptInfo[compiledScriptsList.Count];
        for (int i = 0; i < compiledScriptsList.Count; i++)
        {
            var scriptPath = compiledScriptsList[i];
            var scriptName = Path.GetFileNameWithoutExtension(scriptPath);
            
            compiledScriptInfos[i] = new ScriptInfo
            {
                path = scriptPath,
                name = scriptName,
                type = GetScriptType(scriptPath),
                enabled = true,
                lastModified = File.GetLastWriteTime(scriptPath),
                size = new FileInfo(scriptPath).Length
            };
        }
        
        compiledScripts = compiledScriptsList.Count;
        
        // 更新失败脚本信息
        failedScriptInfos = new ScriptInfo[failedScriptsList.Count];
        for (int i = 0; i < failedScriptsList.Count; i++)
        {
            var scriptPath = failedScriptsList[i];
            var scriptName = Path.GetFileNameWithoutExtension(scriptPath);
            
            failedScriptInfos[i] = new ScriptInfo
            {
                path = scriptPath,
                name = scriptName,
                type = GetScriptType(scriptPath),
                enabled = false,
                lastModified = File.GetLastWriteTime(scriptPath),
                size = new FileInfo(scriptPath).Length
            };
        }
        
        failedScripts = failedScriptsList.Count;
    }

    private string[] FindAllScripts()
    {
        var scripts = new List<string>();
        var searchPatterns = new[] { "*.cs", "*.js", "*.boo" };
        
        foreach (var pattern in searchPatterns)
        {
            var files = Directory.GetFiles(Application.dataPath, pattern, SearchOption.AllDirectories);
            scripts.AddRange(files);
        }
        
        return scripts.ToArray();
    }

    private ScriptType GetScriptType(string scriptPath)
    {
        var extension = Path.GetExtension(scriptPath).ToLower();
        
        switch (extension)
        {
            case ".cs":
                return ScriptType.CSharp;
            case ".js":
                return ScriptType.JavaScript;
            case ".boo":
                return ScriptType.Boo;
            default:
                return ScriptType.Unknown;
        }
    }

    private void UpdateCompilationStatistics()
    {
        compilationStats.totalScripts = totalScripts;
        compilationStats.compiledScripts = compiledScripts;
        compilationStats.failedScripts = failedScripts;
        compilationStats.warningsCount = warningsCount;
        compilationStats.errorsCount = errorsCount;
        
        if (totalScripts > 0)
        {
            compilationStats.successRate = (float)compiledScripts / totalScripts * 100f;
        }
        else
        {
            compilationStats.successRate = 0f;
        }
    }

    private void CheckCodeGeneration()
    {
        if (enableAutoCodeGeneration && !isGeneratingCode)
        {
            // 检查是否需要生成代码
            var scriptsToGenerate = FindScriptsNeedingGeneration();
            if (scriptsToGenerate.Length > 0)
            {
                GenerateCodeForScripts(scriptsToGenerate);
            }
        }
    }

    private string[] FindScriptsNeedingGeneration()
    {
        var scripts = new List<string>();
        
        // 查找需要生成代码的脚本
        foreach (var scriptPath in scriptPaths)
        {
            if (NeedsCodeGeneration(scriptPath))
            {
                scripts.Add(scriptPath);
            }
        }
        
        return scripts.ToArray();
    }

    private bool NeedsCodeGeneration(string scriptPath)
    {
        // 检查脚本是否需要代码生成
        var content = File.ReadAllText(scriptPath);
        
        // 检查是否包含代码生成标记
        return content.Contains("[GenerateCode]") || 
               content.Contains("// GENERATE_CODE") ||
               content.Contains("/* GENERATE_CODE */");
    }

    private void CheckScriptAnalysis()
    {
        if (enableScriptAnalysis && !isAnalyzingScripts)
        {
            // 检查是否需要分析脚本
            var scriptsToAnalyze = FindScriptsNeedingAnalysis();
            if (scriptsToAnalyze.Length > 0)
            {
                AnalyzeScripts(scriptsToAnalyze);
            }
        }
    }

    private string[] FindScriptsNeedingAnalysis()
    {
        var scripts = new List<string>();
        
        // 查找需要分析的脚本
        foreach (var scriptPath in scriptPaths)
        {
            if (NeedsAnalysis(scriptPath))
            {
                scripts.Add(scriptPath);
            }
        }
        
        return scripts.ToArray();
    }

    private bool NeedsAnalysis(string scriptPath)
    {
        // 检查脚本是否需要分析
        var lastModified = File.GetLastWriteTime(scriptPath);
        var lastAnalyzed = GetLastAnalyzedTime(scriptPath);
        
        return lastModified > lastAnalyzed;
    }

    private System.DateTime GetLastAnalyzedTime(string scriptPath)
    {
        // 获取脚本最后分析时间（这里简化处理）
        return System.DateTime.MinValue;
    }

    private void OnCompilationStarted(object obj)
    {
        isCompiling = true;
        compilationStartTime = Time.time;
        compilationProgress = 0f;
        compilationMessage = "编译已开始";
        
        Debug.Log("脚本编译已开始");
    }

    private void OnCompilationFinished(object obj)
    {
        isCompiling = false;
        compilationProgress = 1f;
        compilationMessage = "编译已完成";
        
        // 更新编译结果
        UpdateCompilationResults();
        
        Debug.Log("脚本编译已完成");
    }

    private void UpdateCompilationResults()
    {
        // 更新编译结果
        var results = CompilationPipeline.GetCompilationDefines(compilationOptions);
        
        // 检查编译错误和警告
        CheckCompilationErrors();
        CheckCompilationWarnings();
        
        // 更新脚本列表
        UpdateScriptLists();
    }

    private void CheckCompilationErrors()
    {
        compilationErrors.Clear();
        errorsCount = 0;
        
        // 检查编译错误（这里简化处理）
        var errorLogs = GetCompilationErrorLogs();
        foreach (var error in errorLogs)
        {
            compilationErrors.Add(new CompilationError
            {
                message = error,
                severity = ErrorSeverity.Error,
                scriptPath = "",
                lineNumber = 0
            });
            errorsCount++;
        }
    }

    private void CheckCompilationWarnings()
    {
        compilationWarnings.Clear();
        warningsCount = 0;
        
        // 检查编译警告（这里简化处理）
        var warningLogs = GetCompilationWarningLogs();
        foreach (var warning in warningLogs)
        {
            compilationWarnings.Add(new CompilationWarning
            {
                message = warning,
                severity = WarningSeverity.Warning,
                scriptPath = "",
                lineNumber = 0
            });
            warningsCount++;
        }
    }

    private string[] GetCompilationErrorLogs()
    {
        // 获取编译错误日志（这里简化处理）
        return new string[0];
    }

    private string[] GetCompilationWarningLogs()
    {
        // 获取编译警告日志（这里简化处理）
        return new string[0];
    }

    private void UpdateScriptLists()
    {
        compiledScriptsList.Clear();
        failedScriptsList.Clear();
        
        foreach (var scriptPath in scriptPaths)
        {
            if (IsScriptCompiled(scriptPath))
            {
                compiledScriptsList.Add(scriptPath);
            }
            else
            {
                failedScriptsList.Add(scriptPath);
            }
        }
    }

    private bool IsScriptCompiled(string scriptPath)
    {
        // 检查脚本是否编译成功（这里简化处理）
        return !scriptPath.Contains("Error") && !scriptPath.Contains("Failed");
    }

    public void CompileScripts()
    {
        if (isCompiling)
        {
            Debug.LogWarning("脚本正在编译中，请等待完成");
            return;
        }
        
        isCompiling = true;
        compilationStartTime = Time.time;
        compilationProgress = 0f;
        compilationMessage = "正在编译脚本...";
        
        Debug.Log("开始编译脚本");
    }

    public void GenerateCodeForScripts(string[] scriptPaths)
    {
        if (isGeneratingCode)
        {
            Debug.LogWarning("代码正在生成中，请等待完成");
            return;
        }
        
        isGeneratingCode = true;
        compilationMessage = "正在生成代码...";
        
        try
        {
            foreach (var scriptPath in scriptPaths)
            {
                GenerateCodeForScript(scriptPath);
            }
            
            Debug.Log($"代码生成完成，共生成 {scriptPaths.Length} 个脚本");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"代码生成失败: {e.Message}");
        }
        finally
        {
            isGeneratingCode = false;
        }
    }

    private void GenerateCodeForScript(string scriptPath)
    {
        var scriptName = Path.GetFileNameWithoutExtension(scriptPath);
        var generatedPath = Path.Combine(codeGenerationPath, $"{scriptName}_Generated.cs");
        
        var template = GetCodeTemplate(scriptName);
        var generatedCode = ProcessCodeTemplate(template, scriptName);
        
        File.WriteAllText(generatedPath, generatedCode);
        
        if (!generatedScripts.Contains(generatedPath))
        {
            var newGeneratedScripts = new string[generatedScripts.Length + 1];
            generatedScripts.CopyTo(newGeneratedScripts, 0);
            newGeneratedScripts[generatedScripts.Length] = generatedPath;
            generatedScripts = newGeneratedScripts;
        }
        
        Debug.Log($"代码已生成: {generatedPath}");
    }

    private string GetCodeTemplate(string scriptName)
    {
        return $@"using UnityEngine;

/// <summary>
/// 自动生成的脚本: {scriptName}
/// </summary>
public class {scriptName}_Generated : MonoBehaviour
{{
    [Header(""自动生成配置"")]
    [SerializeField] private bool enableAutoGeneration = true;
    [SerializeField] private string generatedBy = ""ScriptingExample"";
    [SerializeField] private System.DateTime generatedAt = System.DateTime.Now;
    
    private void Start()
    {{
        Debug.Log($""自动生成的脚本已启动: {{generatedBy}} at {{generatedAt}}"");
    }}
    
    private void Update()
    {{
        if (enableAutoGeneration)
        {{
            // 自动生成的更新逻辑
        }}
    }}
}}";
    }

    private string ProcessCodeTemplate(string template, string scriptName)
    {
        return template.Replace("{scriptName}", scriptName)
                      .Replace("{generatedBy}", "ScriptingExample")
                      .Replace("{generatedAt}", System.DateTime.Now.ToString());
    }

    public void AnalyzeScripts(string[] scriptPaths)
    {
        if (isAnalyzingScripts)
        {
            Debug.LogWarning("脚本正在分析中，请等待完成");
            return;
        }
        
        isAnalyzingScripts = true;
        compilationMessage = "正在分析脚本...";
        
        try
        {
            foreach (var scriptPath in scriptPaths)
            {
                AnalyzeScript(scriptPath);
            }
            
            Debug.Log($"脚本分析完成，共分析 {scriptPaths.Length} 个脚本");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"脚本分析失败: {e.Message}");
        }
        finally
        {
            isAnalyzingScripts = false;
        }
    }

    private void AnalyzeScript(string scriptPath)
    {
        var content = File.ReadAllText(scriptPath);
        var scriptName = Path.GetFileNameWithoutExtension(scriptPath);
        
        var result = new CodeAnalysisResult
        {
            scriptPath = scriptPath,
            scriptName = scriptName,
            analysisTime = System.DateTime.Now,
            lineCount = content.Split('\n').Length,
            characterCount = content.Length,
            complexity = CalculateComplexity(content),
            performanceScore = CalculatePerformanceScore(content),
            memoryScore = CalculateMemoryScore(content),
            securityScore = CalculateSecurityScore(content),
            styleScore = CalculateStyleScore(content)
        };
        
        analysisResults.Add(result);
        
        // 检查性能问题
        CheckPerformanceIssues(scriptPath, content);
        
        // 检查内存问题
        CheckMemoryIssues(scriptPath, content);
        
        // 检查安全问题
        CheckSecurityIssues(scriptPath, content);
        
        // 检查样式问题
        CheckStyleIssues(scriptPath, content);
        
        Debug.Log($"脚本分析完成: {scriptName}");
    }

    private int CalculateComplexity(string content)
    {
        // 计算代码复杂度（简化版本）
        int complexity = 0;
        complexity += content.Split("if").Length - 1;
        complexity += content.Split("for").Length - 1;
        complexity += content.Split("while").Length - 1;
        complexity += content.Split("foreach").Length - 1;
        complexity += content.Split("switch").Length - 1;
        return complexity;
    }

    private float CalculatePerformanceScore(string content)
    {
        // 计算性能评分（简化版本）
        float score = 100f;
        
        if (content.Contains("FindObjectOfType"))
            score -= 10f;
        if (content.Contains("GetComponent"))
            score -= 5f;
        if (content.Contains("Instantiate"))
            score -= 15f;
        if (content.Contains("Destroy"))
            score -= 10f;
        
        return Mathf.Max(0f, score);
    }

    private float CalculateMemoryScore(string content)
    {
        // 计算内存评分（简化版本）
        float score = 100f;
        
        if (content.Contains("new "))
            score -= 5f;
        if (content.Contains("GC.Collect"))
            score -= 20f;
        if (content.Contains("string +"))
            score -= 10f;
        
        return Mathf.Max(0f, score);
    }

    private float CalculateSecurityScore(string content)
    {
        // 计算安全评分（简化版本）
        float score = 100f;
        
        if (content.Contains("eval"))
            score -= 50f;
        if (content.Contains("System.IO.File"))
            score -= 10f;
        if (content.Contains("System.Net"))
            score -= 15f;
        
        return Mathf.Max(0f, score);
    }

    private float CalculateStyleScore(string content)
    {
        // 计算样式评分（简化版本）
        float score = 100f;
        
        if (content.Contains("    "))
            score -= 5f;
        if (content.Contains("// TODO"))
            score -= 10f;
        if (content.Contains("// FIXME"))
            score -= 15f;
        
        return Mathf.Max(0f, score);
    }

    private void CheckPerformanceIssues(string scriptPath, string content)
    {
        var issues = new List<PerformanceIssue>();
        
        if (content.Contains("FindObjectOfType"))
        {
            issues.Add(new PerformanceIssue
            {
                type = PerformanceIssueType.FindObjectOfType,
                severity = PerformanceIssueSeverity.Medium,
                message = "使用了FindObjectOfType，建议缓存引用",
                lineNumber = FindLineNumber(content, "FindObjectOfType")
            });
        }
        
        if (content.Contains("GetComponent"))
        {
            issues.Add(new PerformanceIssue
            {
                type = PerformanceIssueType.GetComponent,
                severity = PerformanceIssueSeverity.Low,
                message = "频繁使用GetComponent，建议缓存引用",
                lineNumber = FindLineNumber(content, "GetComponent")
            });
        }
        
        performanceIssues.AddRange(issues);
    }

    private void CheckMemoryIssues(string scriptPath, string content)
    {
        var issues = new List<MemoryIssue>();
        
        if (content.Contains("GC.Collect"))
        {
            issues.Add(new MemoryIssue
            {
                type = MemoryIssueType.GCCollect,
                severity = MemoryIssueSeverity.High,
                message = "手动调用GC.Collect，可能导致性能问题",
                lineNumber = FindLineNumber(content, "GC.Collect")
            });
        }
        
        memoryIssues.AddRange(issues);
    }

    private void CheckSecurityIssues(string scriptPath, string content)
    {
        var issues = new List<SecurityIssue>();
        
        if (content.Contains("eval"))
        {
            issues.Add(new SecurityIssue
            {
                type = SecurityIssueType.Eval,
                severity = SecurityIssueSeverity.Critical,
                message = "使用了eval，存在安全风险",
                lineNumber = FindLineNumber(content, "eval")
            });
        }
        
        securityIssues.AddRange(issues);
    }

    private void CheckStyleIssues(string scriptPath, string content)
    {
        var issues = new List<StyleIssue>();
        
        if (content.Contains("// TODO"))
        {
            issues.Add(new StyleIssue
            {
                type = StyleIssueType.TODO,
                severity = StyleIssueSeverity.Low,
                message = "存在TODO注释，需要完成",
                lineNumber = FindLineNumber(content, "// TODO")
            });
        }
        
        styleIssues.AddRange(issues);
    }

    private int FindLineNumber(string content, string searchTerm)
    {
        var lines = content.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains(searchTerm))
            {
                return i + 1;
            }
        }
        return 0;
    }

    public void GenerateScriptingReport()
    {
        reportBuilder.Clear();
        reportBuilder.AppendLine("=== 脚本系统报告 ===");
        reportBuilder.AppendLine($"生成时间: {System.DateTime.Now}");
        reportBuilder.AppendLine($"脚本系统状态: {scriptingStatus}");
        reportBuilder.AppendLine($"总脚本数: {totalScripts}");
        reportBuilder.AppendLine($"已编译脚本数: {compiledScripts}");
        reportBuilder.AppendLine($"失败脚本数: {failedScripts}");
        reportBuilder.AppendLine($"警告数: {warningsCount}");
        reportBuilder.AppendLine($"错误数: {errorsCount}");
        reportBuilder.AppendLine($"编译成功率: {compilationStats.successRate:F1}%");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 编译统计 ===");
        reportBuilder.AppendLine($"总编译时间: {compilationStats.totalCompileTime:F3}秒");
        reportBuilder.AppendLine($"平均编译时间: {compilationStats.averageCompileTime:F3}秒");
        reportBuilder.AppendLine($"最大编译时间: {compilationStats.maxCompileTime:F3}秒");
        reportBuilder.AppendLine($"编译次数: {compilationStats.compileCount}");
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 代码生成 ===");
        reportBuilder.AppendLine($"已生成脚本数: {generatedScripts.Length}");
        foreach (var script in generatedScripts)
        {
            reportBuilder.AppendLine($"- {Path.GetFileName(script)}");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 脚本分析 ===");
        reportBuilder.AppendLine($"已分析脚本数: {analysisResults.Count}");
        if (analysisResults.Count > 0)
        {
            var avgComplexity = 0f;
            var avgPerformance = 0f;
            var avgMemory = 0f;
            var avgSecurity = 0f;
            var avgStyle = 0f;
            
            foreach (var result in analysisResults)
            {
                avgComplexity += result.complexity;
                avgPerformance += result.performanceScore;
                avgMemory += result.memoryScore;
                avgSecurity += result.securityScore;
                avgStyle += result.styleScore;
            }
            
            avgComplexity /= analysisResults.Count;
            avgPerformance /= analysisResults.Count;
            avgMemory /= analysisResults.Count;
            avgSecurity /= analysisResults.Count;
            avgStyle /= analysisResults.Count;
            
            reportBuilder.AppendLine($"平均复杂度: {avgComplexity:F1}");
            reportBuilder.AppendLine($"平均性能评分: {avgPerformance:F1}");
            reportBuilder.AppendLine($"平均内存评分: {avgMemory:F1}");
            reportBuilder.AppendLine($"平均安全评分: {avgSecurity:F1}");
            reportBuilder.AppendLine($"平均样式评分: {avgStyle:F1}");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 性能问题 ===");
        foreach (var issue in performanceIssues)
        {
            reportBuilder.AppendLine($"[{issue.severity}] {issue.message} (行: {issue.lineNumber})");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 内存问题 ===");
        foreach (var issue in memoryIssues)
        {
            reportBuilder.AppendLine($"[{issue.severity}] {issue.message} (行: {issue.lineNumber})");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 安全问题 ===");
        foreach (var issue in securityIssues)
        {
            reportBuilder.AppendLine($"[{issue.severity}] {issue.message} (行: {issue.lineNumber})");
        }
        reportBuilder.AppendLine();
        
        reportBuilder.AppendLine("=== 样式问题 ===");
        foreach (var issue in styleIssues)
        {
            reportBuilder.AppendLine($"[{issue.severity}] {issue.message} (行: {issue.lineNumber})");
        }
        
        string report = reportBuilder.ToString();
        Debug.Log(report);
        
        if (enableDataExport)
        {
            ExportReport(report);
        }
    }

    private void ExportReport(string report)
    {
        try
        {
            string fileName = $"ScriptingReport_{System.DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(codeGenerationPath, fileName);
            
            Directory.CreateDirectory(codeGenerationPath);
            File.WriteAllText(filePath, report);
            
            Debug.Log($"脚本系统报告已导出: {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"导出脚本系统报告失败: {e.Message}");
        }
    }

    public void OpenScriptingWindow()
    {
        if (enableScripting)
        {
            EditorWindow.GetWindow<UnityEditor.ScriptingWindow>();
            Debug.Log("脚本窗口已打开");
        }
    }

    public void ResetScriptingData()
    {
        InitializeScriptState();
        InitializeCompilationSettings();
        InitializeCodeGeneration();
        InitializeScriptAnalysis();
        
        analysisResults.Clear();
        performanceIssues.Clear();
        memoryIssues.Clear();
        securityIssues.Clear();
        styleIssues.Clear();
        compilationErrors.Clear();
        compilationWarnings.Clear();
        
        Debug.Log("脚本系统数据已重置");
    }

    private void OnDestroy()
    {
        CompilationPipeline.compilationStarted -= OnCompilationStarted;
        CompilationPipeline.compilationFinished -= OnCompilationFinished;
        
        Debug.Log("脚本回调已清理");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("Scripting 脚本系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("脚本系统配置:");
        enableScripting = GUILayout.Toggle(enableScripting, "启用脚本系统");
        enableCompilation = GUILayout.Toggle(enableCompilation, "启用编译");
        enableCodeGeneration = GUILayout.Toggle(enableCodeGeneration, "启用代码生成");
        enableScriptAnalysis = GUILayout.Toggle(enableScriptAnalysis, "启用脚本分析");
        enableAutoCodeGeneration = GUILayout.Toggle(enableAutoCodeGeneration, "启用自动代码生成");
        
        GUILayout.Space(10);
        GUILayout.Label("脚本状态:");
        GUILayout.Label($"脚本状态: {scriptingStatus}");
        GUILayout.Label($"是否正在编译: {isCompiling}");
        GUILayout.Label($"是否正在生成代码: {isGeneratingCode}");
        GUILayout.Label($"是否正在分析: {isAnalyzingScripts}");
        GUILayout.Label($"编译进度: {compilationProgress * 100:F1}%");
        GUILayout.Label($"编译消息: {compilationMessage}");
        GUILayout.Label($"总脚本数: {totalScripts}");
        GUILayout.Label($"已编译脚本数: {compiledScripts}");
        GUILayout.Label($"失败脚本数: {failedScripts}");
        GUILayout.Label($"警告数: {warningsCount}");
        GUILayout.Label($"错误数: {errorsCount}");
        GUILayout.Label($"编译成功率: {compilationStats.successRate:F1}%");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("编译脚本"))
        {
            CompileScripts();
        }
        
        if (GUILayout.Button("生成代码"))
        {
            var scriptsToGenerate = FindScriptsNeedingGeneration();
            GenerateCodeForScripts(scriptsToGenerate);
        }
        
        if (GUILayout.Button("分析脚本"))
        {
            var scriptsToAnalyze = FindScriptsNeedingAnalysis();
            AnalyzeScripts(scriptsToAnalyze);
        }
        
        if (GUILayout.Button("生成脚本报告"))
        {
            GenerateScriptingReport();
        }
        
        if (GUILayout.Button("打开脚本窗口"))
        {
            OpenScriptingWindow();
        }
        
        if (GUILayout.Button("重置脚本数据"))
        {
            ResetScriptingData();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("性能问题:");
        foreach (var issue in performanceIssues)
        {
            string color = issue.severity == PerformanceIssueSeverity.High ? "red" : "yellow";
            GUILayout.Label($"<color={color}>[{issue.severity}] {issue.message}</color>");
        }
        
        GUILayout.Space(10);
        GUILayout.Label("内存问题:");
        foreach (var issue in memoryIssues)
        {
            string color = issue.severity == MemoryIssueSeverity.High ? "red" : "yellow";
            GUILayout.Label($"<color={color}>[{issue.severity}] {issue.message}</color>");
        }
        
        GUILayout.EndArea();
    }
}

public enum ScriptingStatus
{
    Idle,
    Compiling,
    GeneratingCode,
    Analyzing,
    Completed,
    Failed
}

public enum ScriptType
{
    Unknown,
    CSharp,
    JavaScript,
    Boo
}

public enum PerformanceIssueType
{
    FindObjectOfType,
    GetComponent,
    Instantiate,
    Destroy,
    Update,
    FixedUpdate
}

public enum PerformanceIssueSeverity
{
    Low,
    Medium,
    High,
    Critical
}

public enum MemoryIssueType
{
    GCCollect,
    MemoryLeak,
    LargeAllocation
}

public enum MemoryIssueSeverity
{
    Low,
    Medium,
    High,
    Critical
}

public enum SecurityIssueType
{
    Eval,
    FileAccess,
    NetworkAccess
}

public enum SecurityIssueSeverity
{
    Low,
    Medium,
    High,
    Critical
}

public enum StyleIssueType
{
    TODO,
    FIXME,
    Indentation,
    Naming
}

public enum StyleIssueSeverity
{
    Low,
    Medium,
    High,
    Critical
}

public enum ErrorSeverity
{
    Error,
    Fatal
}

public enum WarningSeverity
{
    Warning,
    Info
}

[System.Serializable]
public class ScriptInfo
{
    public string path;
    public string name;
    public ScriptType type;
    public bool enabled;
    public System.DateTime lastModified;
    public long size;
}

[System.Serializable]
public class CompilationStatistics
{
    public int totalScripts;
    public int compiledScripts;
    public int failedScripts;
    public int warningsCount;
    public int errorsCount;
    public float successRate;
    public float totalCompileTime;
    public float averageCompileTime;
    public float maxCompileTime;
    public int compileCount;
}

[System.Serializable]
public class CompilationError
{
    public string message;
    public ErrorSeverity severity;
    public string scriptPath;
    public int lineNumber;
}

[System.Serializable]
public class CompilationWarning
{
    public string message;
    public WarningSeverity severity;
    public string scriptPath;
    public int lineNumber;
}

[System.Serializable]
public class CodeAnalysisResult
{
    public string scriptPath;
    public string scriptName;
    public System.DateTime analysisTime;
    public int lineCount;
    public int characterCount;
    public int complexity;
    public float performanceScore;
    public float memoryScore;
    public float securityScore;
    public float styleScore;
}

[System.Serializable]
public class PerformanceIssue
{
    public PerformanceIssueType type;
    public PerformanceIssueSeverity severity;
    public string message;
    public int lineNumber;
}

[System.Serializable]
public class MemoryIssue
{
    public MemoryIssueType type;
    public MemoryIssueSeverity severity;
    public string message;
    public int lineNumber;
}

[System.Serializable]
public class SecurityIssue
{
    public SecurityIssueType type;
    public SecurityIssueSeverity severity;
    public string message;
    public int lineNumber;
}

[System.Serializable]
public class StyleIssue
{
    public StyleIssueType type;
    public StyleIssueSeverity severity;
    public string message;
    public int lineNumber;
}

[System.Serializable]
public class OptimizationResult
{
    public string scriptPath;
    public string optimizationType;
    public float improvement;
    public string description;
}

[System.Serializable]
public class DebugBreakpoint
{
    public string scriptPath;
    public int lineNumber;
    public bool enabled;
    public string condition;
}

[System.Serializable]
public class DebugVariable
{
    public string name;
    public string value;
    public string type;
    public bool isWatched;
}

[System.Serializable]
public class DebugCallStack
{
    public string functionName;
    public string scriptPath;
    public int lineNumber;
    public System.DateTime timestamp;
} 