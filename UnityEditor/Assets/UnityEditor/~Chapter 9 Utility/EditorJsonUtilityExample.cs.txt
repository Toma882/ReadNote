using UnityEngine;
using UnityEditor;
using System;

namespace UnityEditor.Examples
{
    /// <summary>
    /// EditorJsonUtility 工具类示例
    /// 提供编辑器专用的JSON序列化和反序列化功能
    /// </summary>
    public static class EditorJsonUtilityExample
    {
        #region 基础序列化示例

        /// <summary>
        /// 对象转JSON字符串
        /// </summary>
        public static void ToJsonExample()
        {
            // 创建测试对象
            TestData data = new TestData
            {
                name = "测试对象",
                value = 42,
                isActive = true,
                position = new Vector3(1, 2, 3)
            };

            // 转换为JSON
            string json = EditorJsonUtility.ToJson(data, true);
            Debug.Log($"对象转JSON:\n{json}");
        }

        /// <summary>
        /// 对象转JSON（不格式化）
        /// </summary>
        public static void ToJsonCompactExample()
        {
            TestData data = new TestData
            {
                name = "紧凑格式",
                value = 100,
                isActive = false,
                position = Vector3.zero
            };

            string json = EditorJsonUtility.ToJson(data, false);
            Debug.Log($"紧凑JSON: {json}");
        }

        #endregion

        #region 反序列化示例

        /// <summary>
        /// JSON覆盖现有对象
        /// </summary>
        public static void FromJsonOverwriteExample()
        {
            // 创建原始对象
            TestData originalData = new TestData
            {
                name = "原始名称",
                value = 0,
                isActive = false,
                position = Vector3.zero
            };

            Debug.Log($"原始对象: {originalData.name}, {originalData.value}, {originalData.isActive}");

            // JSON数据
            string json = "{\"name\":\"新名称\",\"value\":999,\"isActive\":true,\"position\":{\"x\":5,\"y\":10,\"z\":15}}";

            // 覆盖现有对象
            EditorJsonUtility.FromJsonOverwrite(json, originalData);

            Debug.Log($"覆盖后对象: {originalData.name}, {originalData.value}, {originalData.isActive}");
        }

        /// <summary>
        /// 从JSON创建新对象
        /// </summary>
        public static void FromJsonCreateNewExample()
        {
            string json = "{\"name\":\"新对象\",\"value\":777,\"isActive\":true,\"position\":{\"x\":1,\"y\":2,\"z\":3}}";

            TestData newData = JsonUtility.FromJson<TestData>(json);
            Debug.Log($"新对象: {newData.name}, {newData.value}, {newData.isActive}");
        }

        #endregion

        #region 复杂对象示例

        /// <summary>
        /// 复杂对象序列化
        /// </summary>
        public static void ComplexObjectSerializationExample()
        {
            ComplexData complexData = new ComplexData
            {
                id = 1,
                title = "复杂数据",
                metadata = new Metadata
                {
                    version = "1.0.0",
                    author = "Unity开发者",
                    createdDate = DateTime.Now.ToString()
                },
                items = new string[] { "项目1", "项目2", "项目3" },
                settings = new Settings
                {
                    volume = 0.8f,
                    quality = "High",
                    enableEffects = true
                }
            };

            string json = EditorJsonUtility.ToJson(complexData, true);
            Debug.Log($"复杂对象JSON:\n{json}");
        }

        /// <summary>
        /// 复杂对象反序列化
        /// </summary>
        public static void ComplexObjectDeserializationExample()
        {
            string json = @"{
                ""id"": 2,
                ""title"": ""反序列化测试"",
                ""metadata"": {
                    ""version"": ""2.0.0"",
                    ""author"": ""测试用户"",
                    ""createdDate"": ""2024-01-01""
                },
                ""items"": [""测试项目1"", ""测试项目2""],
                ""settings"": {
                    ""volume"": 0.5,
                    ""quality"": ""Medium"",
                    ""enableEffects"": false
                }
            }";

            ComplexData data = JsonUtility.FromJson<ComplexData>(json);
            Debug.Log($"反序列化结果: {data.title}");
            Debug.Log($"元数据: {data.metadata.version} by {data.metadata.author}");
            Debug.Log($"设置: {data.settings.quality}, 音量: {data.settings.volume}");
        }

        #endregion

        #region 编辑器特定示例

        /// <summary>
        /// 编辑器设置序列化
        /// </summary>
        public static void EditorSettingsSerializationExample()
        {
            EditorSettings settings = new EditorSettings
            {
                projectName = "MyUnityProject",
                lastScene = "MainScene",
                buildTarget = "Windows",
                qualitySettings = new QualitySettings
                {
                    textureQuality = "Full Res",
                    shadowQuality = "High",
                    antiAliasing = "4x Multi Sampling"
                },
                preferences = new EditorPreferences
                {
                    autoSave = true,
                    showGrid = true,
                    snapToGrid = false
                }
            };

            string json = EditorJsonUtility.ToJson(settings, true);
            Debug.Log($"编辑器设置JSON:\n{json}");
        }

        /// <summary>
        /// 编辑器设置反序列化
        /// </summary>
        public static void EditorSettingsDeserializationExample()
        {
            string json = @"{
                ""projectName"": ""测试项目"",
                ""lastScene"": ""TestScene"",
                ""buildTarget"": ""Android"",
                ""qualitySettings"": {
                    ""textureQuality"": ""Half Res"",
                    ""shadowQuality"": ""Medium"",
                    ""antiAliasing"": ""2x Multi Sampling""
                },
                ""preferences"": {
                    ""autoSave"": false,
                    ""showGrid"": false,
                    ""snapToGrid"": true
                }
            }";

            EditorSettings settings = JsonUtility.FromJson<EditorSettings>(json);
            Debug.Log($"项目: {settings.projectName}");
            Debug.Log($"最后场景: {settings.lastScene}");
            Debug.Log($"构建目标: {settings.buildTarget}");
            Debug.Log($"质量设置: {settings.qualitySettings.textureQuality}");
        }

        #endregion

        #region 错误处理示例

        /// <summary>
        /// JSON错误处理
        /// </summary>
        public static void JsonErrorHandlingExample()
        {
            // 无效JSON
            string invalidJson = "{invalid json}";
            
            try
            {
                TestData data = JsonUtility.FromJson<TestData>(invalidJson);
                Debug.Log("反序列化成功");
            }
            catch (Exception e)
            {
                Debug.LogError($"JSON反序列化错误: {e.Message}");
            }

            // 空JSON
            string emptyJson = "";
            TestData emptyData = JsonUtility.FromJson<TestData>(emptyJson);
            Debug.Log($"空JSON结果: {emptyData.name ?? "null"}");
        }

        /// <summary>
        /// 类型不匹配处理
        /// </summary>
        public static void TypeMismatchHandlingExample()
        {
            // JSON中的字段类型与对象不匹配
            string mismatchedJson = "{\"name\":123,\"value\":\"not a number\",\"isActive\":\"not a boolean\"}";
            
            TestData data = JsonUtility.FromJson<TestData>(mismatchedJson);
            Debug.Log($"类型不匹配处理结果: {data.name}, {data.value}, {data.isActive}");
        }

        #endregion

        #region 性能测试示例

        /// <summary>
        /// 序列化性能测试
        /// </summary>
        public static void SerializationPerformanceExample()
        {
            TestData data = new TestData
            {
                name = "性能测试",
                value = 12345,
                isActive = true,
                position = new Vector3(1, 2, 3)
            };

            // 测试序列化性能
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int i = 0; i < 1000; i++)
            {
                string json = EditorJsonUtility.ToJson(data, false);
            }
            
            stopwatch.Stop();
            Debug.Log($"1000次序列化耗时: {stopwatch.ElapsedMilliseconds}ms");
        }

        /// <summary>
        /// 反序列化性能测试
        /// </summary>
        public static void DeserializationPerformanceExample()
        {
            string json = EditorJsonUtility.ToJson(new TestData
            {
                name = "性能测试",
                value = 12345,
                isActive = true,
                position = new Vector3(1, 2, 3)
            }, false);

            // 测试反序列化性能
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int i = 0; i < 1000; i++)
            {
                TestData data = JsonUtility.FromJson<TestData>(json);
            }
            
            stopwatch.Stop();
            Debug.Log($"1000次反序列化耗时: {stopwatch.ElapsedMilliseconds}ms");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 配置文件管理示例
        /// </summary>
        public static void ConfigFileManagementExample()
        {
            // 创建配置
            ProjectConfig config = new ProjectConfig
            {
                projectName = "Unity项目",
                version = "1.0.0",
                buildSettings = new BuildSettings
                {
                    targetPlatform = "Windows",
                    developmentBuild = true,
                    scriptDebugging = true
                },
                sceneSettings = new SceneSettings
                {
                    defaultScene = "MainMenu",
                    loadingScene = "Loading",
                    gameScene = "Gameplay"
                }
            };

            // 保存配置
            string configJson = EditorJsonUtility.ToJson(config, true);
            string configPath = "Assets/ProjectConfig.json";
            System.IO.File.WriteAllText(configPath, configJson);
            Debug.Log($"配置已保存到: {configPath}");

            // 加载配置
            if (System.IO.File.Exists(configPath))
            {
                string loadedJson = System.IO.File.ReadAllText(configPath);
                ProjectConfig loadedConfig = JsonUtility.FromJson<ProjectConfig>(loadedJson);
                Debug.Log($"配置已加载: {loadedConfig.projectName} v{loadedConfig.version}");
            }
        }

        #endregion

        #region 数据结构定义

        [System.Serializable]
        public class TestData
        {
            public string name;
            public int value;
            public bool isActive;
            public Vector3 position;
        }

        [System.Serializable]
        public class ComplexData
        {
            public int id;
            public string title;
            public Metadata metadata;
            public string[] items;
            public Settings settings;
        }

        [System.Serializable]
        public class Metadata
        {
            public string version;
            public string author;
            public string createdDate;
        }

        [System.Serializable]
        public class Settings
        {
            public float volume;
            public string quality;
            public bool enableEffects;
        }

        [System.Serializable]
        public class EditorSettings
        {
            public string projectName;
            public string lastScene;
            public string buildTarget;
            public QualitySettings qualitySettings;
            public EditorPreferences preferences;
        }

        [System.Serializable]
        public class QualitySettings
        {
            public string textureQuality;
            public string shadowQuality;
            public string antiAliasing;
        }

        [System.Serializable]
        public class EditorPreferences
        {
            public bool autoSave;
            public bool showGrid;
            public bool snapToGrid;
        }

        [System.Serializable]
        public class ProjectConfig
        {
            public string projectName;
            public string version;
            public BuildSettings buildSettings;
            public SceneSettings sceneSettings;
        }

        [System.Serializable]
        public class BuildSettings
        {
            public string targetPlatform;
            public bool developmentBuild;
            public bool scriptDebugging;
        }

        [System.Serializable]
        public class SceneSettings
        {
            public string defaultScene;
            public string loadingScene;
            public string gameScene;
        }

        #endregion
    }
}
