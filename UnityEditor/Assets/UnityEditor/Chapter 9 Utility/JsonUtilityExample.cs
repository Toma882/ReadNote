using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEditor.Chapter9Utility.JsonUtility
{
    /// <summary>
    /// JsonUtility 和 EditorJsonUtility 详细示例
    /// 展示JSON序列化和反序列化的各种用法
    /// </summary>
    public class JsonUtilityExample : EditorWindow
    {
        private Vector2 scrollPosition;
        private TestData testData = new TestData();
        private string jsonOutput = "";
        private string jsonInput = "";

        [MenuItem("Tools/Utility Examples/JsonUtility Detailed Example")]
        public static void ShowWindow()
        {
            JsonUtilityExample window = GetWindow<JsonUtilityExample>("JsonUtility 示例");
            window.Show();
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            GUILayout.Label("JsonUtility 和 EditorJsonUtility 示例", EditorStyles.boldLabel);
            
            EditorGUILayout.Space();
            
            // 测试数据编辑
            EditorGUILayout.LabelField("测试数据:", EditorStyles.boldLabel);
            testData.name = EditorGUILayout.TextField("名称", testData.name);
            testData.value = EditorGUILayout.IntField("数值", testData.value);
            testData.position = EditorGUILayout.Vector3Field("位置", testData.position);
            testData.isActive = EditorGUILayout.Toggle("是否激活", testData.isActive);
            
            EditorGUILayout.Space();
            
            // JsonUtility 操作
            EditorGUILayout.LabelField("JsonUtility 操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("序列化为JSON"))
            {
                SerializeToJson();
            }
            
            if (GUILayout.Button("从JSON反序列化"))
            {
                DeserializeFromJson();
            }
            
            if (GUILayout.Button("从JSON覆盖对象"))
            {
                OverwriteFromJson();
            }
            
            EditorGUILayout.Space();
            
            // EditorJsonUtility 操作
            EditorGUILayout.LabelField("EditorJsonUtility 操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("序列化选中对象"))
            {
                SerializeSelectedObject();
            }
            
            if (GUILayout.Button("从JSON覆盖选中对象"))
            {
                OverwriteSelectedObject();
            }
            
            EditorGUILayout.Space();
            
            // JSON输入输出
            EditorGUILayout.LabelField("JSON 输入输出:", EditorStyles.boldLabel);
            
            EditorGUILayout.LabelField("JSON 输出:");
            jsonOutput = EditorGUILayout.TextArea(jsonOutput, GUILayout.Height(100));
            
            EditorGUILayout.LabelField("JSON 输入:");
            jsonInput = EditorGUILayout.TextArea(jsonInput, GUILayout.Height(100));
            
            EditorGUILayout.Space();
            
            // 高级操作
            EditorGUILayout.LabelField("高级操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("创建复杂对象"))
            {
                CreateComplexObject();
            }
            
            if (GUILayout.Button("序列化复杂对象"))
            {
                SerializeComplexObject();
            }
            
            if (GUILayout.Button("批量序列化"))
            {
                BatchSerialize();
            }
            
            if (GUILayout.Button("JSON验证"))
            {
                ValidateJson();
            }
            
            EditorGUILayout.Space();
            
            // 文件操作
            EditorGUILayout.LabelField("文件操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("保存JSON到文件"))
            {
                SaveJsonToFile();
            }
            
            if (GUILayout.Button("从文件加载JSON"))
            {
                LoadJsonFromFile();
            }
            
            EditorGUILayout.EndScrollView();
        }

        #region JsonUtility 操作

        /// <summary>
        /// 序列化为JSON
        /// </summary>
        private void SerializeToJson()
        {
            try
            {
                jsonOutput = UnityEngine.JsonUtility.ToJson(testData, true);
                Debug.Log("序列化成功");
            }
            catch (Exception ex)
            {
                Debug.LogError($"序列化失败: {ex.Message}");
                EditorUtility.DisplayDialog("错误", $"序列化失败: {ex.Message}", "确定");
            }
        }

        /// <summary>
        /// 从JSON反序列化
        /// </summary>
        private void DeserializeFromJson()
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                EditorUtility.DisplayDialog("错误", "请输入JSON字符串", "确定");
                return;
            }
            
            try
            {
                TestData deserializedData = UnityEngine.JsonUtility.FromJson<TestData>(jsonInput);
                testData = deserializedData;
                Debug.Log("反序列化成功");
            }
            catch (Exception ex)
            {
                Debug.LogError($"反序列化失败: {ex.Message}");
                EditorUtility.DisplayDialog("错误", $"反序列化失败: {ex.Message}", "确定");
            }
        }

        /// <summary>
        /// 从JSON覆盖对象
        /// </summary>
        private void OverwriteFromJson()
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                EditorUtility.DisplayDialog("错误", "请输入JSON字符串", "确定");
                return;
            }
            
            try
            {
                UnityEngine.JsonUtility.FromJsonOverwrite(jsonInput, testData);
                Debug.Log("覆盖成功");
            }
            catch (Exception ex)
            {
                Debug.LogError($"覆盖失败: {ex.Message}");
                EditorUtility.DisplayDialog("错误", $"覆盖失败: {ex.Message}", "确定");
            }
        }

        #endregion

        #region EditorJsonUtility 操作

        /// <summary>
        /// 序列化选中对象
        /// </summary>
        private void SerializeSelectedObject()
        {
            UnityEngine.Object selectedObject = Selection.activeObject;
            
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个对象", "确定");
                return;
            }
            
            try
            {
                jsonOutput = EditorJsonUtility.ToJson(selectedObject, true);
                Debug.Log($"序列化对象 {selectedObject.name} 成功");
            }
            catch (Exception ex)
            {
                Debug.LogError($"序列化失败: {ex.Message}");
                EditorUtility.DisplayDialog("错误", $"序列化失败: {ex.Message}", "确定");
            }
        }

        /// <summary>
        /// 从JSON覆盖选中对象
        /// </summary>
        private void OverwriteSelectedObject()
        {
            UnityEngine.Object selectedObject = Selection.activeObject;
            
            if (selectedObject == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个对象", "确定");
                return;
            }
            
            if (string.IsNullOrEmpty(jsonInput))
            {
                EditorUtility.DisplayDialog("错误", "请输入JSON字符串", "确定");
                return;
            }
            
            try
            {
                EditorJsonUtility.FromJsonOverwrite(jsonInput, selectedObject);
                EditorUtility.SetDirty(selectedObject);
                Debug.Log($"覆盖对象 {selectedObject.name} 成功");
            }
            catch (Exception ex)
            {
                Debug.LogError($"覆盖失败: {ex.Message}");
                EditorUtility.DisplayDialog("错误", $"覆盖失败: {ex.Message}", "确定");
            }
        }

        #endregion

        #region 高级操作

        /// <summary>
        /// 创建复杂对象
        /// </summary>
        private void CreateComplexObject()
        {
            testData = new TestData
            {
                name = "复杂测试对象",
                value = 999,
                position = new Vector3(10, 20, 30),
                isActive = true,
                items = new List<string> { "项目1", "项目2", "项目3", "项目4", "项目5" },
                nestedData = new NestedData
                {
                    id = 123,
                    description = "嵌套数据描述",
                    values = new float[] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f }
                }
            };
            
            Debug.Log("复杂对象创建完成");
        }

        /// <summary>
        /// 序列化复杂对象
        /// </summary>
        private void SerializeComplexObject()
        {
            try
            {
                jsonOutput = UnityEngine.JsonUtility.ToJson(testData, true);
                Debug.Log("复杂对象序列化成功");
            }
            catch (Exception ex)
            {
                Debug.LogError($"复杂对象序列化失败: {ex.Message}");
                EditorUtility.DisplayDialog("错误", $"序列化失败: {ex.Message}", "确定");
            }
        }

        /// <summary>
        /// 批量序列化
        /// </summary>
        private void BatchSerialize()
        {
            List<TestData> dataList = new List<TestData>();
            
            for (int i = 0; i < 5; i++)
            {
                dataList.Add(new TestData
                {
                    name = $"对象{i}",
                    value = i * 10,
                    position = new Vector3(i, i * 2, i * 3),
                    isActive = i % 2 == 0
                });
            }
            
            try
            {
                jsonOutput = UnityEngine.JsonUtility.ToJson(dataList, true);
                Debug.Log($"批量序列化 {dataList.Count} 个对象成功");
            }
            catch (Exception ex)
            {
                Debug.LogError($"批量序列化失败: {ex.Message}");
                EditorUtility.DisplayDialog("错误", $"批量序列化失败: {ex.Message}", "确定");
            }
        }

        /// <summary>
        /// JSON验证
        /// </summary>
        private void ValidateJson()
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                EditorUtility.DisplayDialog("错误", "请输入JSON字符串", "确定");
                return;
            }
            
            try
            {
                // 尝试解析JSON
                TestData tempData = UnityEngine.JsonUtility.FromJson<TestData>(jsonInput);
                
                if (tempData != null)
                {
                    Debug.Log("JSON格式验证通过");
                    EditorUtility.DisplayDialog("验证结果", "JSON格式正确", "确定");
                }
                else
                {
                    Debug.LogWarning("JSON解析结果为空");
                    EditorUtility.DisplayDialog("验证结果", "JSON解析结果为空", "确定");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON验证失败: {ex.Message}");
                EditorUtility.DisplayDialog("验证结果", $"JSON格式错误: {ex.Message}", "确定");
            }
        }

        #endregion

        #region 文件操作

        /// <summary>
        /// 保存JSON到文件
        /// </summary>
        private void SaveJsonToFile()
        {
            if (string.IsNullOrEmpty(jsonOutput))
            {
                EditorUtility.DisplayDialog("错误", "没有JSON数据可保存", "确定");
                return;
            }
            
            string path = EditorUtility.SaveFilePanel("保存JSON文件", Application.dataPath, "data", "json");
            
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    System.IO.File.WriteAllText(path, jsonOutput);
                    Debug.Log($"JSON文件保存成功: {path}");
                    EditorUtility.DisplayDialog("成功", "JSON文件保存成功", "确定");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"保存失败: {ex.Message}");
                    EditorUtility.DisplayDialog("错误", $"保存失败: {ex.Message}", "确定");
                }
            }
        }

        /// <summary>
        /// 从文件加载JSON
        /// </summary>
        private void LoadJsonFromFile()
        {
            string path = EditorUtility.OpenFilePanel("加载JSON文件", Application.dataPath, "json");
            
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    jsonInput = System.IO.File.ReadAllText(path);
                    Debug.Log($"JSON文件加载成功: {path}");
                    EditorUtility.DisplayDialog("成功", "JSON文件加载成功", "确定");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"加载失败: {ex.Message}");
                    EditorUtility.DisplayDialog("错误", $"加载失败: {ex.Message}", "确定");
                }
            }
        }

        #endregion

        #region 数据类定义

        [System.Serializable]
        public class TestData
        {
            public string name = "测试对象";
            public int value = 42;
            public Vector3 position = Vector3.zero;
            public bool isActive = true;
            public List<string> items = new List<string>();
            public NestedData nestedData = new NestedData();
        }

        [System.Serializable]
        public class NestedData
        {
            public int id;
            public string description;
            public float[] values;
        }

        #endregion
    }
}
