using UnityEngine;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;
using System.Linq;

namespace UnityEditor.Localization.Examples
{
    /// <summary>
    /// UnityEditor.Localization 命名空间使用示例
    /// 演示本地化系统的配置、管理和使用功能
    /// </summary>
    public class LocalizationExample : MonoBehaviour
    {
        [Header("本地化配置")]
        [SerializeField] private bool enableLocalization = true;
        [SerializeField] private string defaultLocale = "en";
        [SerializeField] private string[] supportedLocales = { "en", "zh-CN", "ja", "ko" };
        
        [Header("本地化状态")]
        [SerializeField] private string currentLocale = "en";
        [SerializeField] private bool isInitialized = false;
        [SerializeField] private int localeCount = 0;
        [SerializeField] private string lastLocalizedText = "";
        
        [Header("测试文本")]
        [SerializeField] private string testKey = "hello_world";
        [SerializeField] private string testText = "Hello World";
        [SerializeField] private LocalizedString localizedString;
        
        private LocalizationSettings localizationSettings;
        private List<Locale> availableLocales = new List<Locale>();
        private Dictionary<string, string> customTranslations = new Dictionary<string, string>();
        
        /// <summary>
        /// 初始化本地化系统
        /// </summary>
        private void Start()
        {
            InitializeLocalizationSystem();
        }
        
        /// <summary>
        /// 初始化本地化系统
        /// </summary>
        private void InitializeLocalizationSystem()
        {
            if (!enableLocalization)
            {
                Debug.Log("本地化系统已禁用");
                return;
            }
            
            try
            {
                // 获取本地化设置
                localizationSettings = LocalizationSettings.Instance;
                
                // 等待本地化系统初始化
                StartCoroutine(WaitForLocalizationInitialization());
                
                // 初始化自定义翻译
                InitializeCustomTranslations();
                
                Debug.Log("本地化系统初始化完成");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"本地化系统初始化失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 等待本地化系统初始化
        /// </summary>
        private System.Collections.IEnumerator WaitForLocalizationInitialization()
        {
            yield return LocalizationSettings.InitializationOperation;
            
            isInitialized = true;
            localeCount = LocalizationSettings.AvailableLocales.Locales.Count;
            
            // 设置默认语言
            SetLocale(defaultLocale);
            
            Debug.Log($"本地化系统已就绪，支持 {localeCount} 种语言");
        }
        
        /// <summary>
        /// 初始化自定义翻译
        /// </summary>
        private void InitializeCustomTranslations()
        {
            customTranslations.Clear();
            
            // 英文翻译
            customTranslations["en:hello_world"] = "Hello World";
            customTranslations["en:welcome"] = "Welcome to Unity";
            customTranslations["en:goodbye"] = "Goodbye";
            
            // 中文翻译
            customTranslations["zh-CN:hello_world"] = "你好世界";
            customTranslations["zh-CN:welcome"] = "欢迎使用Unity";
            customTranslations["zh-CN:goodbye"] = "再见";
            
            // 日文翻译
            customTranslations["ja:hello_world"] = "こんにちは世界";
            customTranslations["ja:welcome"] = "Unityへようこそ";
            customTranslations["ja:goodbye"] = "さようなら";
            
            // 韩文翻译
            customTranslations["ko:hello_world"] = "안녕하세요 세계";
            customTranslations["ko:welcome"] = "Unity에 오신 것을 환영합니다";
            customTranslations["ko:goodbye"] = "안녕히 가세요";
        }
        
        /// <summary>
        /// 设置当前语言
        /// </summary>
        public void SetLocale(string localeCode)
        {
            if (!isInitialized)
            {
                Debug.LogWarning("本地化系统尚未初始化");
                return;
            }
            
            var locale = LocalizationSettings.AvailableLocales.Locales.FirstOrDefault(l => l.Identifier.Code == localeCode);
            if (locale != null)
            {
                LocalizationSettings.SelectedLocale = locale;
                currentLocale = localeCode;
                Debug.Log($"语言已切换到: {localeCode}");
            }
            else
            {
                Debug.LogWarning($"不支持的语言: {localeCode}");
            }
        }
        
        /// <summary>
        /// 获取本地化文本
        /// </summary>
        public string GetLocalizedText(string key)
        {
            if (!isInitialized)
            {
                return key;
            }
            
            string translationKey = $"{currentLocale}:{key}";
            if (customTranslations.ContainsKey(translationKey))
            {
                lastLocalizedText = customTranslations[translationKey];
                return lastLocalizedText;
            }
            
            // 如果没有找到翻译，返回键名
            lastLocalizedText = key;
            return key;
        }
        
        /// <summary>
        /// 添加自定义翻译
        /// </summary>
        public void AddTranslation(string locale, string key, string translation)
        {
            string translationKey = $"{locale}:{key}";
            customTranslations[translationKey] = translation;
            Debug.Log($"添加翻译: {translationKey} = {translation}");
        }
        
        /// <summary>
        /// 移除翻译
        /// </summary>
        public void RemoveTranslation(string locale, string key)
        {
            string translationKey = $"{locale}:{key}";
            if (customTranslations.Remove(translationKey))
            {
                Debug.Log($"移除翻译: {translationKey}");
            }
        }
        
        /// <summary>
        /// 获取所有可用语言
        /// </summary>
        public string[] GetAvailableLocales()
        {
            if (!isInitialized)
            {
                return new string[0];
            }
            
            return LocalizationSettings.AvailableLocales.Locales
                .Select(l => l.Identifier.Code)
                .ToArray();
        }
        
        /// <summary>
        /// 获取当前语言信息
        /// </summary>
        public string GetCurrentLocaleInfo()
        {
            if (!isInitialized)
            {
                return "本地化系统未初始化";
            }
            
            var currentLocaleObj = LocalizationSettings.SelectedLocale;
            if (currentLocaleObj != null)
            {
                return $"当前语言: {currentLocaleObj.Identifier.Code} ({currentLocaleObj.Identifier.CultureInfo.NativeName})";
            }
            
            return "未设置语言";
        }
        
        /// <summary>
        /// 创建本地化字符串
        /// </summary>
        public LocalizedString CreateLocalizedString(string tableName, string key)
        {
            var localizedString = new LocalizedString(tableName, key);
            return localizedString;
        }
        
        /// <summary>
        /// 导出翻译数据
        /// </summary>
        public void ExportTranslations()
        {
            if (!isInitialized)
            {
                Debug.LogWarning("本地化系统尚未初始化");
                return;
            }
            
            string exportPath = EditorUtility.SaveFilePanel("导出翻译", "", "translations", "json");
            if (string.IsNullOrEmpty(exportPath))
                return;
            
            try
            {
                var exportData = new Dictionary<string, Dictionary<string, string>>();
                
                foreach (string locale in supportedLocales)
                {
                    exportData[locale] = new Dictionary<string, string>();
                    foreach (string key in customTranslations.Keys.Where(k => k.StartsWith(locale + ":")))
                    {
                        string cleanKey = key.Substring(locale.Length + 1);
                        exportData[locale][cleanKey] = customTranslations[key];
                    }
                }
                
                string json = JsonUtility.ToJson(new { translations = exportData }, true);
                System.IO.File.WriteAllText(exportPath, json);
                
                Debug.Log($"翻译数据已导出到: {exportPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导出翻译失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 导入翻译数据
        /// </summary>
        public void ImportTranslations()
        {
            string importPath = EditorUtility.OpenFilePanel("导入翻译", "", "json");
            if (string.IsNullOrEmpty(importPath))
                return;
            
            try
            {
                string json = System.IO.File.ReadAllText(importPath);
                var importData = JsonUtility.FromJson<Dictionary<string, Dictionary<string, string>>>(json);
                
                foreach (var locale in importData.Keys)
                {
                    foreach (var translation in importData[locale])
                    {
                        AddTranslation(locale, translation.Key, translation.Value);
                    }
                }
                
                Debug.Log($"翻译数据已从 {importPath} 导入");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导入翻译失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 验证翻译完整性
        /// </summary>
        public void ValidateTranslations()
        {
            var missingTranslations = new List<string>();
            
            foreach (string locale in supportedLocales)
            {
                foreach (string key in customTranslations.Keys.Where(k => k.StartsWith("en:")))
                {
                    string cleanKey = key.Substring(3);
                    string translationKey = $"{locale}:{cleanKey}";
                    
                    if (!customTranslations.ContainsKey(translationKey))
                    {
                        missingTranslations.Add($"{locale}:{cleanKey}");
                    }
                }
            }
            
            if (missingTranslations.Count > 0)
            {
                Debug.LogWarning($"发现缺失的翻译: {string.Join(", ", missingTranslations)}");
            }
            else
            {
                Debug.Log("所有翻译都完整");
            }
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 500, 700));
            GUILayout.Label("UnityEditor.Localization 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {(isInitialized ? "已初始化" : "初始化中...")}");
            GUILayout.Label($"当前语言: {currentLocale}");
            GUILayout.Label($"支持语言数量: {localeCount}");
            GUILayout.Label($"最后翻译: {lastLocalizedText}");
            
            GUILayout.Space(10);
            GUILayout.Label("语言控制", EditorStyles.boldLabel);
            
            string[] availableLocales = GetAvailableLocales();
            if (availableLocales.Length > 0)
            {
                int currentIndex = System.Array.IndexOf(availableLocales, currentLocale);
                int newIndex = EditorGUILayout.Popup("选择语言", currentIndex, availableLocales);
                if (newIndex != currentIndex && newIndex >= 0)
                {
                    SetLocale(availableLocales[newIndex]);
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("翻译测试", EditorStyles.boldLabel);
            
            testKey = GUILayout.TextField("翻译键", testKey);
            if (GUILayout.Button("获取翻译"))
            {
                string translation = GetLocalizedText(testKey);
                Debug.Log($"翻译结果: {translation}");
            }
            
            GUILayout.Space(10);
            GUILayout.Label("翻译管理", EditorStyles.boldLabel);
            
            string newLocale = GUILayout.TextField("语言代码", "en");
            string newKey = GUILayout.TextField("翻译键", "new_key");
            string newTranslation = GUILayout.TextField("翻译文本", "New Translation");
            
            if (GUILayout.Button("添加翻译"))
            {
                AddTranslation(newLocale, newKey, newTranslation);
            }
            
            if (GUILayout.Button("验证翻译完整性"))
            {
                ValidateTranslations();
            }
            
            GUILayout.Space(10);
            GUILayout.Label("导入导出", EditorStyles.boldLabel);
            
            if (GUILayout.Button("导出翻译"))
            {
                ExportTranslations();
            }
            
            if (GUILayout.Button("导入翻译"))
            {
                ImportTranslations();
            }
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            enableLocalization = EditorGUILayout.Toggle("启用本地化", enableLocalization);
            defaultLocale = EditorGUILayout.TextField("默认语言", defaultLocale);
            
            GUILayout.EndArea();
        }
    }
} 