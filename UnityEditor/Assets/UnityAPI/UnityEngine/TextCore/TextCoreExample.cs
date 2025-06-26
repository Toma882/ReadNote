using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.TextCore.Text;

/// <summary>
/// UnityEngine.TextCore 命名空间案例演示
/// 展示文本渲染、字体管理、文本布局等核心功能
/// </summary>
public class TextCoreExample : MonoBehaviour
{
    [Header("文本核心配置")]
    [SerializeField] private bool enableTextCore = true; //启用文本核心
    [SerializeField] private bool enableFontAtlas = true; //启用字体图集
    [SerializeField] private bool enableTextMesh = true; //启用文本网格
    [SerializeField] private bool enableTextRendering = true; //启用文本渲染
    [SerializeField] private bool enableTextLayout = true; //启用文本布局
    
    [Header("字体配置")]
    [SerializeField] private FontAsset fontAsset; //字体资源
    [SerializeField] private int fontSize = 24; //字体大小
    [SerializeField] private FontWeight fontWeight = FontWeight.Regular; //字体粗细
    [SerializeField] private FontStyle fontStyle = FontStyle.Normal; //字体样式
    [SerializeField] private bool enableFontFallback = true; //启用字体回退
    [SerializeField] private FontAsset[] fallbackFonts; //回退字体列表
    
    [Header("文本渲染配置")]
    [SerializeField] private Color textColor = Color.white; //文本颜色
    [SerializeField] private Color outlineColor = Color.black; //轮廓颜色
    [SerializeField] private float outlineWidth = 0f; //轮廓宽度
    [SerializeField] private Color shadowColor = Color.black; //阴影颜色
    [SerializeField] private Vector2 shadowOffset = new Vector2(1f, -1f); //阴影偏移
    [SerializeField] private bool enableGradient = false; //启用渐变
    [SerializeField] private Color gradientTop = Color.white; //渐变顶部颜色
    [SerializeField] private Color gradientBottom = Color.gray; //渐变底部颜色
    
    [Header("文本布局配置")]
    [SerializeField] private TextAlignmentOptions alignment = TextAlignmentOptions.Left; //文本对齐
    [SerializeField] private float lineSpacing = 0f; //行间距
    [SerializeField] private float characterSpacing = 0f; //字符间距
    [SerializeField] private float wordSpacing = 0f; //词间距
    [SerializeField] private float paragraphSpacing = 0f; //段落间距
    [SerializeField] private bool enableWordWrap = true; //启用自动换行
    [SerializeField] private float maxWidth = 500f; //最大宽度
    [SerializeField] private float maxHeight = 300f; //最大高度
    
    [Header("文本内容")]
    [SerializeField] private string sampleText = "Hello World! 这是一个Unity TextCore示例。\n支持多行文本、富文本标签和特殊字符。"; //示例文本
    [SerializeField] private bool enableRichText = true; //启用富文本
    [SerializeField] private bool enableMarkdown = false; //启用Markdown
    [SerializeField] private string customText = ""; //自定义文本
    [SerializeField] private bool enableTextAnimation = false; //启用文本动画
    [SerializeField] private float animationSpeed = 1f; //动画速度
    
    [Header("性能监控")]
    [SerializeField] private bool enableTextMonitoring = true; //启用文本监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logTextData = false; //记录文本数据
    [SerializeField] private int totalCharacters = 0; //总字符数
    [SerializeField] private int totalWords = 0; //总词数
    [SerializeField] private int totalLines = 0; //总行数
    [SerializeField] private float textRenderTime = 0f; //文本渲染时间
    [SerializeField] private int fontAtlasSize = 0; //字体图集大小
    
    [Header("文本状态")]
    [SerializeField] private string textCoreState = "未初始化"; //文本核心状态
    [SerializeField] private string currentTextStatus = "空闲"; //当前文本状态
    [SerializeField] private bool isTextDirty = false; //文本是否脏
    [SerializeField] private bool isLayoutDirty = false; //布局是否脏
    [SerializeField] private bool isFontDirty = false; //字体是否脏
    [SerializeField] private Vector2 textBounds = Vector2.zero; //文本边界
    [SerializeField] private Vector2 textSize = Vector2.zero; //文本大小
    
    [Header("性能数据")]
    [SerializeField] private float[] renderTimeHistory = new float[100]; //渲染时间历史
    [SerializeField] private int renderTimeIndex = 0; //渲染时间索引
    [SerializeField] private float[] characterCountHistory = new float[100]; //字符数量历史
    [SerializeField] private int characterCountIndex = 0; //字符数量索引
    
    private TextMeshPro textMeshPro;
    private TextInfo textInfo;
    private FontAsset currentFontAsset;
    private System.Collections.Generic.List<TextElement> textElements = new System.Collections.Generic.List<TextElement>();
    private System.Collections.Generic.List<TextStyle> textStyles = new System.Collections.Generic.List<TextStyle>();
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;
    private float animationTime = 0f;

    private void Start()
    {
        InitializeTextCore();
    }

    /// <summary>
    /// 初始化文本核心
    /// </summary>
    private void InitializeTextCore()
    {
        // 创建TextMeshPro组件
        textMeshPro = gameObject.AddComponent<TextMeshPro>();
        
        // 初始化字体资源
        InitializeFontAsset();
        
        // 初始化文本信息
        InitializeTextInfo();
        
        // 初始化文本样式
        InitializeTextStyles();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置文本核心
        ConfigureTextCore();
        
        // 设置初始文本
        SetSampleText();
        
        isInitialized = true;
        textCoreState = "已初始化";
        Debug.Log("文本核心初始化完成");
    }

    /// <summary>
    /// 初始化字体资源
    /// </summary>
    private void InitializeFontAsset()
    {
        if (fontAsset == null)
        {
            // 创建默认字体资源
            fontAsset = CreateDefaultFontAsset();
        }
        
        currentFontAsset = fontAsset;
        textMeshPro.font = fontAsset;
        textMeshPro.fontSize = fontSize;
        textMeshPro.fontWeight = fontWeight;
        textMeshPro.fontStyle = fontStyle;
        
        if (enableFontFallback && fallbackFonts != null)
        {
            textMeshPro.fallbackFontAssetTable = new System.Collections.Generic.List<FontAsset>(fallbackFonts);
        }
        
        Debug.Log($"字体资源初始化完成: {fontAsset.name}");
    }

    /// <summary>
    /// 创建默认字体资源
    /// </summary>
    private FontAsset CreateDefaultFontAsset()
    {
        // 这里应该创建或加载一个默认字体资源
        // 在实际项目中，通常会从Resources文件夹或AssetBundle中加载
        var defaultFont = Resources.Load<FontAsset>("Fonts/DefaultFont");
        if (defaultFont == null)
        {
            Debug.LogWarning("未找到默认字体资源，将使用系统默认字体");
            // 创建一个基本的字体资源
            defaultFont = ScriptableObject.CreateInstance<FontAsset>();
            defaultFont.name = "DefaultFont";
        }
        return defaultFont;
    }

    /// <summary>
    /// 初始化文本信息
    /// </summary>
    private void InitializeTextInfo()
    {
        textInfo = new TextInfo();
        textInfo.textElementType = TextElementType.Character;
        
        Debug.Log("文本信息初始化完成");
    }

    /// <summary>
    /// 初始化文本样式
    /// </summary>
    private void InitializeTextStyles()
    {
        // 创建基本文本样式
        var normalStyle = new TextStyle
        {
            name = "Normal",
            fontSize = fontSize,
            fontStyle = fontStyle,
            fontWeight = fontWeight,
            color = textColor
        };
        textStyles.Add(normalStyle);
        
        // 创建标题样式
        var titleStyle = new TextStyle
        {
            name = "Title",
            fontSize = fontSize * 1.5f,
            fontStyle = FontStyle.Bold,
            fontWeight = FontWeight.Bold,
            color = Color.yellow
        };
        textStyles.Add(titleStyle);
        
        // 创建强调样式
        var emphasisStyle = new TextStyle
        {
            name = "Emphasis",
            fontSize = fontSize,
            fontStyle = FontStyle.Italic,
            fontWeight = FontWeight.Medium,
            color = Color.cyan
        };
        textStyles.Add(emphasisStyle);
        
        Debug.Log($"文本样式初始化完成: {textStyles.Count} 个样式");
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        if (enableTextMonitoring)
        {
            renderTimeHistory = new float[100];
            characterCountHistory = new float[100];
            renderTimeIndex = 0;
            characterCountIndex = 0;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    /// <summary>
    /// 配置文本核心
    /// </summary>
    private void ConfigureTextCore()
    {
        // 配置文本渲染
        textMeshPro.color = textColor;
        textMeshPro.outlineColor = outlineColor;
        textMeshPro.outlineWidth = outlineWidth;
        textMeshPro.shadowColor = shadowColor;
        textMeshPro.shadowOffset = shadowOffset;
        
        // 配置文本布局
        textMeshPro.alignment = alignment;
        textMeshPro.lineSpacing = lineSpacing;
        textMeshPro.characterSpacing = characterSpacing;
        textMeshPro.wordSpacing = wordSpacing;
        textMeshPro.paragraphSpacing = paragraphSpacing;
        textMeshPro.enableWordWrapping = enableWordWrap;
        textMeshPro.rectTransform.sizeDelta = new Vector2(maxWidth, maxHeight);
        
        // 配置富文本
        textMeshPro.richText = enableRichText;
        
        Debug.Log("文本核心配置完成");
    }

    /// <summary>
    /// 设置示例文本
    /// </summary>
    private void SetSampleText()
    {
        string text = sampleText;
        
        if (enableRichText)
        {
            text = ApplyRichTextFormatting(text);
        }
        
        if (enableMarkdown)
        {
            text = ApplyMarkdownFormatting(text);
        }
        
        SetText(text);
    }

    /// <summary>
    /// 应用富文本格式
    /// </summary>
    private string ApplyRichTextFormatting(string text)
    {
        // 添加富文本标签
        text = text.Replace("Hello", "<color=yellow><b>Hello</b></color>");
        text = text.Replace("Unity", "<color=cyan><i>Unity</i></color>");
        text = text.Replace("TextCore", "<color=green><b>TextCore</b></color>");
        
        return text;
    }

    /// <summary>
    /// 应用Markdown格式
    /// </summary>
    private string ApplyMarkdownFormatting(string text)
    {
        // 简单的Markdown转换
        text = text.Replace("**", "<b>").Replace("**", "</b>");
        text = text.Replace("*", "<i>").Replace("*", "</i>");
        text = text.Replace("`", "<color=orange>").Replace("`", "</color>");
        
        return text;
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 文本动画
        if (enableTextAnimation)
        {
            UpdateTextAnimation();
        }
        
        // 文本监控
        if (enableTextMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            MonitorTextPerformance();
            lastMonitoringTime = Time.time;
        }
        
        // 检查文本是否需要更新
        if (isTextDirty)
        {
            UpdateText();
        }
    }

    /// <summary>
    /// 更新文本动画
    /// </summary>
    private void UpdateTextAnimation()
    {
        animationTime += Time.deltaTime * animationSpeed;
        
        // 简单的颜色动画
        Color animatedColor = Color.Lerp(textColor, gradientTop, Mathf.Sin(animationTime) * 0.5f + 0.5f);
        textMeshPro.color = animatedColor;
        
        // 简单的缩放动画
        float scale = 1f + Mathf.Sin(animationTime * 2f) * 0.1f;
        textMeshPro.transform.localScale = Vector3.one * scale;
    }

    /// <summary>
    /// 监控文本性能
    /// </summary>
    private void MonitorTextPerformance()
    {
        // 更新文本统计信息
        UpdateTextStatistics();
        
        if (logTextData)
        {
            Debug.Log($"文本性能监控: 字符数={totalCharacters}, 词数={totalWords}, 行数={totalLines}, 渲染时间={textRenderTime * 1000:F2}ms");
        }
    }

    /// <summary>
    /// 更新文本统计信息
    /// </summary>
    private void UpdateTextStatistics()
    {
        if (textMeshPro.textInfo != null)
        {
            totalCharacters = textMeshPro.textInfo.characterCount;
            totalWords = textMeshPro.textInfo.wordCount;
            totalLines = textMeshPro.textInfo.lineCount;
        }
        
        // 更新性能数据
        renderTimeHistory[renderTimeIndex] = textRenderTime;
        renderTimeIndex = (renderTimeIndex + 1) % 100;
        
        characterCountHistory[characterCountIndex] = totalCharacters;
        characterCountIndex = (characterCountIndex + 1) % 100;
    }

    /// <summary>
    /// 设置文本
    /// </summary>
    /// <param name="text">文本内容</param>
    public void SetText(string text)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = text;
            isTextDirty = true;
            currentTextStatus = "文本已设置";
            
            Debug.Log($"文本已设置: {text.Length} 个字符");
        }
    }

    /// <summary>
    /// 更新文本
    /// </summary>
    private void UpdateText()
    {
        float startTime = Time.realtimeSinceStartup;
        
        // 强制更新文本
        textMeshPro.ForceMeshUpdate();
        
        // 更新文本信息
        if (textMeshPro.textInfo != null)
        {
            textInfo = textMeshPro.textInfo;
            UpdateTextStatistics();
        }
        
        // 更新文本边界
        textBounds = textMeshPro.bounds.size;
        textSize = textMeshPro.rectTransform.sizeDelta;
        
        textRenderTime = Time.realtimeSinceStartup - startTime;
        
        isTextDirty = false;
        currentTextStatus = "文本已更新";
    }

    /// <summary>
    /// 应用文本样式
    /// </summary>
    /// <param name="styleName">样式名称</param>
    public void ApplyTextStyle(string styleName)
    {
        var style = textStyles.Find(s => s.name == styleName);
        if (style != null)
        {
            textMeshPro.fontSize = style.fontSize;
            textMeshPro.fontStyle = style.fontStyle;
            textMeshPro.fontWeight = style.fontWeight;
            textMeshPro.color = style.color;
            
            isTextDirty = true;
            Debug.Log($"文本样式已应用: {styleName}");
        }
    }

    /// <summary>
    /// 设置字体
    /// </summary>
    /// <param name="newFontAsset">新字体资源</param>
    public void SetFont(FontAsset newFontAsset)
    {
        if (newFontAsset != null)
        {
            currentFontAsset = newFontAsset;
            textMeshPro.font = newFontAsset;
            isFontDirty = true;
            
            Debug.Log($"字体已更改: {newFontAsset.name}");
        }
    }

    /// <summary>
    /// 设置文本颜色
    /// </summary>
    /// <param name="color">颜色</param>
    public void SetTextColor(Color color)
    {
        textColor = color;
        textMeshPro.color = color;
        isTextDirty = true;
        
        Debug.Log($"文本颜色已设置: {color}");
    }

    /// <summary>
    /// 设置文本对齐
    /// </summary>
    /// <param name="newAlignment">新对齐方式</param>
    public void SetTextAlignment(TextAlignmentOptions newAlignment)
    {
        alignment = newAlignment;
        textMeshPro.alignment = newAlignment;
        isLayoutDirty = true;
        
        Debug.Log($"文本对齐已设置: {newAlignment}");
    }

    /// <summary>
    /// 设置文本大小
    /// </summary>
    /// <param name="size">大小</param>
    public void SetTextSize(Vector2 size)
    {
        maxWidth = size.x;
        maxHeight = size.y;
        textMeshPro.rectTransform.sizeDelta = size;
        isLayoutDirty = true;
        
        Debug.Log($"文本大小已设置: {size}");
    }

    /// <summary>
    /// 添加文本效果
    /// </summary>
    /// <param name="effectType">效果类型</param>
    public void AddTextEffect(TextEffectType effectType)
    {
        switch (effectType)
        {
            case TextEffectType.Outline:
                textMeshPro.outlineWidth = 0.1f;
                textMeshPro.outlineColor = Color.black;
                break;
            case TextEffectType.Shadow:
                textMeshPro.shadowOffset = new Vector2(1f, -1f);
                textMeshPro.shadowColor = Color.black;
                break;
            case TextEffectType.Gradient:
                // 实现渐变效果
                break;
        }
        
        isTextDirty = true;
        Debug.Log($"文本效果已添加: {effectType}");
    }

    /// <summary>
    /// 生成文本报告
    /// </summary>
    public void GenerateTextReport()
    {
        Debug.Log("=== 文本核心报告 ===");
        Debug.Log($"文本核心状态: {textCoreState}");
        Debug.Log($"当前文本状态: {currentTextStatus}");
        Debug.Log($"总字符数: {totalCharacters}");
        Debug.Log($"总词数: {totalWords}");
        Debug.Log($"总行数: {totalLines}");
        Debug.Log($"文本渲染时间: {textRenderTime * 1000:F2}ms");
        Debug.Log($"字体图集大小: {fontAtlasSize}");
        Debug.Log($"文本边界: {textBounds}");
        Debug.Log($"文本大小: {textSize}");
        Debug.Log($"当前字体: {currentFontAsset?.name}");
        Debug.Log($"字体大小: {fontSize}");
        Debug.Log($"文本对齐: {alignment}");
    }

    /// <summary>
    /// 导出文本数据
    /// </summary>
    public void ExportTextData()
    {
        var data = new TextCoreData
        {
            timestamp = System.DateTime.Now.ToString(),
            textCoreState = textCoreState,
            currentTextStatus = currentTextStatus,
            totalCharacters = totalCharacters,
            totalWords = totalWords,
            totalLines = totalLines,
            textRenderTime = textRenderTime,
            fontAtlasSize = fontAtlasSize,
            textBounds = textBounds,
            textSize = textSize,
            fontSize = fontSize,
            alignment = alignment.ToString(),
            renderTimeHistory = renderTimeHistory,
            characterCountHistory = characterCountHistory
        };
        
        string json = JsonUtility.ToJson(data, true);
        string filename = $"textcore_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        
        System.IO.File.WriteAllText(filename, json);
        Debug.Log($"文本数据已导出: {filename}");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("TextCore 文本核心演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("文本核心配置:");
        enableTextCore = GUILayout.Toggle(enableTextCore, "启用文本核心");
        enableFontAtlas = GUILayout.Toggle(enableFontAtlas, "启用字体图集");
        enableTextMesh = GUILayout.Toggle(enableTextMesh, "启用文本网格");
        enableTextRendering = GUILayout.Toggle(enableTextRendering, "启用文本渲染");
        enableTextLayout = GUILayout.Toggle(enableTextLayout, "启用文本布局");
        
        GUILayout.Space(10);
        GUILayout.Label("字体配置:");
        fontSize = int.TryParse(GUILayout.TextField("字体大小", fontSize.ToString()), out var size) ? size : fontSize;
        fontWeight = (FontWeight)System.Enum.Parse(typeof(FontWeight), GUILayout.TextField("字体粗细", fontWeight.ToString()));
        fontStyle = (FontStyle)System.Enum.Parse(typeof(FontStyle), GUILayout.TextField("字体样式", fontStyle.ToString()));
        enableFontFallback = GUILayout.Toggle(enableFontFallback, "启用字体回退");
        
        GUILayout.Space(10);
        GUILayout.Label("文本渲染配置:");
        textColor = UnityEditor.EditorGUILayout.ColorField("文本颜色", textColor);
        outlineColor = UnityEditor.EditorGUILayout.ColorField("轮廓颜色", outlineColor);
        outlineWidth = float.TryParse(GUILayout.TextField("轮廓宽度", outlineWidth.ToString()), out var outline) ? outline : outlineWidth;
        shadowColor = UnityEditor.EditorGUILayout.ColorField("阴影颜色", shadowColor);
        shadowOffset = UnityEditor.EditorGUILayout.Vector2Field("阴影偏移", shadowOffset);
        enableGradient = GUILayout.Toggle(enableGradient, "启用渐变");
        
        GUILayout.Space(10);
        GUILayout.Label("文本布局配置:");
        alignment = (TextAlignmentOptions)System.Enum.Parse(typeof(TextAlignmentOptions), GUILayout.TextField("文本对齐", alignment.ToString()));
        lineSpacing = float.TryParse(GUILayout.TextField("行间距", lineSpacing.ToString()), out var lineSpace) ? lineSpace : lineSpacing;
        characterSpacing = float.TryParse(GUILayout.TextField("字符间距", characterSpacing.ToString()), out var charSpace) ? charSpace : characterSpacing;
        wordSpacing = float.TryParse(GUILayout.TextField("词间距", wordSpacing.ToString()), out var wordSpace) ? wordSpace : wordSpacing;
        enableWordWrap = GUILayout.Toggle(enableWordWrap, "启用自动换行");
        maxWidth = float.TryParse(GUILayout.TextField("最大宽度", maxWidth.ToString()), out var width) ? width : maxWidth;
        maxHeight = float.TryParse(GUILayout.TextField("最大高度", maxHeight.ToString()), out var height) ? height : maxHeight;
        
        GUILayout.Space(10);
        GUILayout.Label("文本内容:");
        sampleText = GUILayout.TextArea(sampleText, GUILayout.Height(60));
        enableRichText = GUILayout.Toggle(enableRichText, "启用富文本");
        enableMarkdown = GUILayout.Toggle(enableMarkdown, "启用Markdown");
        enableTextAnimation = GUILayout.Toggle(enableTextAnimation, "启用文本动画");
        animationSpeed = float.TryParse(GUILayout.TextField("动画速度", animationSpeed.ToString()), out var animSpeed) ? animSpeed : animationSpeed;
        
        GUILayout.Space(10);
        GUILayout.Label("文本状态:");
        GUILayout.Label($"核心状态: {textCoreState}");
        GUILayout.Label($"文本状态: {currentTextStatus}");
        GUILayout.Label($"总字符数: {totalCharacters}");
        GUILayout.Label($"总词数: {totalWords}");
        GUILayout.Label($"总行数: {totalLines}");
        GUILayout.Label($"渲染时间: {textRenderTime * 1000:F2}ms");
        GUILayout.Label($"文本边界: {textBounds}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("设置示例文本"))
        {
            SetSampleText();
        }
        
        if (GUILayout.Button("应用普通样式"))
        {
            ApplyTextStyle("Normal");
        }
        
        if (GUILayout.Button("应用标题样式"))
        {
            ApplyTextStyle("Title");
        }
        
        if (GUILayout.Button("应用强调样式"))
        {
            ApplyTextStyle("Emphasis");
        }
        
        if (GUILayout.Button("添加轮廓效果"))
        {
            AddTextEffect(TextEffectType.Outline);
        }
        
        if (GUILayout.Button("添加阴影效果"))
        {
            AddTextEffect(TextEffectType.Shadow);
        }
        
        if (GUILayout.Button("生成文本报告"))
        {
            GenerateTextReport();
        }
        
        if (GUILayout.Button("导出文本数据"))
        {
            ExportTextData();
        }
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 文本样式
/// </summary>
[System.Serializable]
public class TextStyle
{
    public string name;
    public float fontSize;
    public FontStyle fontStyle;
    public FontWeight fontWeight;
    public Color color;
}

/// <summary>
/// 文本元素
/// </summary>
[System.Serializable]
public class TextElement
{
    public string text;
    public TextStyle style;
    public Vector2 position;
    public Vector2 size;
}

/// <summary>
/// 文本效果类型
/// </summary>
public enum TextEffectType
{
    None,
    Outline,
    Shadow,
    Gradient
}

/// <summary>
/// 文本核心数据类
/// </summary>
[System.Serializable]
public class TextCoreData
{
    public string timestamp;
    public string textCoreState;
    public string currentTextStatus;
    public int totalCharacters;
    public int totalWords;
    public int totalLines;
    public float textRenderTime;
    public int fontAtlasSize;
    public Vector2 textBounds;
    public Vector2 textSize;
    public int fontSize;
    public string alignment;
    public float[] renderTimeHistory;
    public float[] characterCountHistory;
} 