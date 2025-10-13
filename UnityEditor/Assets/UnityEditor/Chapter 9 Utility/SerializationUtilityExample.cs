using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace UnityEditor.Examples
{
    /// <summary>
    /// SerializationUtility 工具类示例
    /// 提供序列化相关的实用工具功能
    /// </summary>
    public static class SerializationUtilityExample
    {
        #region 序列化值示例

        /// <summary>
        /// 序列化值
        /// </summary>
        public static void SerializeValueExample()
        {
            TestData data = new TestData
            {
                name = "测试数据",
                value = 42,
                isActive = true,
                position = new Vector3(1, 2, 3)
            };

            // 序列化值
            byte[] serializedData = SerializationUtility.SerializeValue(data, DataFormat.Binary);
            
            Debug.Log($"序列化完成:");
            Debug.Log($"- 数据大小: {serializedData.Length} 字节");
            Debug.Log($"- 原始对象: {data.name}, {data.value}, {data.isActive}");
        }

        /// <summary>
        /// 序列化值（JSON格式）
        /// </summary>
        public static void SerializeValueJsonExample()
        {
            TestData data = new TestData
            {
                name = "JSON测试",
                value = 100,
                isActive = false,
                position = Vector3.zero
            };

            // 序列化为JSON
            byte[] serializedData = SerializationUtility.SerializeValue(data, DataFormat.JSON);
            string jsonString = System.Text.Encoding.UTF8.GetString(serializedData);
            
            Debug.Log($"JSON序列化完成:");
            Debug.Log($"- JSON字符串: {jsonString}");
            Debug.Log($"- 数据大小: {serializedData.Length} 字节");
        }

        #endregion

        #region 反序列化值示例

        /// <summary>
        /// 反序列化值
        /// </summary>
        public static void DeserializeValueExample()
        {
            // 先序列化
            TestData originalData = new TestData
            {
                name = "原始数据",
                value = 999,
                isActive = true,
                position = new Vector3(5, 10, 15)
            };

            byte[] serializedData = SerializationUtility.SerializeValue(originalData, DataFormat.Binary);
            
            // 反序列化
            TestData deserializedData = SerializationUtility.DeserializeValue<TestData>(serializedData, DataFormat.Binary);
            
            Debug.Log($"反序列化完成:");
            Debug.Log($"- 原始: {originalData.name}, {originalData.value}, {originalData.isActive}");
            Debug.Log($"- 反序列化: {deserializedData.name}, {deserializedData.value}, {deserializedData.isActive}");
        }

        /// <summary>
        /// 反序列化值（JSON格式）
        /// </summary>
        public static void DeserializeValueJsonExample()
        {
            // 创建JSON数据
            TestData originalData = new TestData
            {
                name = "JSON反序列化",
                value = 777,
                isActive = false,
                position = new Vector3(1, 1, 1)
            };

            byte[] serializedData = SerializationUtility.SerializeValue(originalData, DataFormat.JSON);
            
            // 反序列化
            TestData deserializedData = SerializationUtility.DeserializeValue<TestData>(serializedData, DataFormat.JSON);
            
            Debug.Log($"JSON反序列化完成:");
            Debug.Log($"- 名称匹配: {originalData.name == deserializedData.name}");
            Debug.Log($"- 值匹配: {originalData.value == deserializedData.value}");
            Debug.Log($"- 状态匹配: {originalData.isActive == deserializedData.isActive}");
        }

        #endregion

        #region 复杂对象序列化示例

        /// <summary>
        /// 复杂对象序列化
        /// </summary>
        public static void ComplexObjectSerializationExample()
        {
            ComplexData complexData = new ComplexData
            {
                id = 1,
                title = "复杂数据",
                items = new List<string> { "项目1", "项目2", "项目3" },
                metadata = new Dictionary<string, int>
                {
                    { "count", 100 },
                    { "level", 5 }
                },
                nestedData = new TestData
                {
                    name = "嵌套数据",
                    value = 42,
                    isActive = true,
                    position = Vector3.one
                }
            };

            // 序列化
            byte[] serializedData = SerializationUtility.SerializeValue(complexData, DataFormat.Binary);
            
            Debug.Log($"复杂对象序列化完成:");
            Debug.Log($"- 数据大小: {serializedData.Length} 字节");
            Debug.Log($"- 项目数量: {complexData.items.Count}");
            Debug.Log($"- 元数据数量: {complexData.metadata.Count}");
        }

        /// <summary>
        /// 复杂对象反序列化
        /// </summary>
        public static void ComplexObjectDeserializationExample()
        {
            ComplexData originalData = new ComplexData
            {
                id = 2,
                title = "反序列化测试",
                items = new List<string> { "A", "B", "C" },
                metadata = new Dictionary<string, int>
                {
                    { "x", 10 },
                    { "y", 20 }
                },
                nestedData = new TestData
                {
                    name = "嵌套",
                    value = 123,
                    isActive = false,
                    position = Vector3.zero
                }
            };

            // 序列化
            byte[] serializedData = SerializationUtility.SerializeValue(originalData, DataFormat.Binary);
            
            // 反序列化
            ComplexData deserializedData = SerializationUtility.DeserializeValue<ComplexData>(serializedData, DataFormat.Binary);
            
            Debug.Log($"复杂对象反序列化完成:");
            Debug.Log($"- ID: {deserializedData.id}");
            Debug.Log($"- 标题: {deserializedData.title}");
            Debug.Log($"- 项目数量: {deserializedData.items.Count}");
            Debug.Log($"- 嵌套数据: {deserializedData.nestedData.name}");
        }

        #endregion

        #region 数据格式比较示例

        /// <summary>
        /// 比较二进制和JSON格式
        /// </summary>
        public static void CompareDataFormatsExample()
        {
            TestData data = new TestData
            {
                name = "格式比较",
                value = 12345,
                isActive = true,
                position = new Vector3(1, 2, 3)
            };

            // 二进制序列化
            byte[] binaryData = SerializationUtility.SerializeValue(data, DataFormat.Binary);
            
            // JSON序列化
            byte[] jsonData = SerializationUtility.SerializeValue(data, DataFormat.JSON);
            
            Debug.Log($"数据格式比较:");
            Debug.Log($"- 二进制大小: {binaryData.Length} 字节");
            Debug.Log($"- JSON大小: {jsonData.Length} 字节");
            Debug.Log($"- 大小差异: {jsonData.Length - binaryData.Length} 字节");
            Debug.Log($"- JSON内容: {System.Text.Encoding.UTF8.GetString(jsonData)}");
        }

        /// <summary>
        /// 序列化性能比较
        /// </summary>
        public static void SerializationPerformanceComparisonExample()
        {
            TestData data = new TestData
            {
                name = "性能测试",
                value = 99999,
                isActive = true,
                position = new Vector3(10, 20, 30)
            };

            int iterations = 1000;

            // 二进制序列化性能测试
            var binaryStopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                byte[] serializedData = SerializationUtility.SerializeValue(data, DataFormat.Binary);
            }
            binaryStopwatch.Stop();

            // JSON序列化性能测试
            var jsonStopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                byte[] serializedData = SerializationUtility.SerializeValue(data, DataFormat.JSON);
            }
            jsonStopwatch.Stop();

            Debug.Log($"序列化性能比较 ({iterations}次):");
            Debug.Log($"- 二进制: {binaryStopwatch.ElapsedMilliseconds}ms");
            Debug.Log($"- JSON: {jsonStopwatch.ElapsedMilliseconds}ms");
            Debug.Log($"- 性能差异: {jsonStopwatch.ElapsedMilliseconds - binaryStopwatch.ElapsedMilliseconds}ms");
        }

        #endregion

        #region 数组和集合序列化示例

        /// <summary>
        /// 数组序列化
        /// </summary>
        public static void ArraySerializationExample()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            // 序列化数组
            byte[] serializedData = SerializationUtility.SerializeValue(numbers, DataFormat.Binary);
            
            // 反序列化
            int[] deserializedNumbers = SerializationUtility.DeserializeValue<int[]>(serializedData, DataFormat.Binary);
            
            Debug.Log($"数组序列化:");
            Debug.Log($"- 原始数组长度: {numbers.Length}");
            Debug.Log($"- 反序列化数组长度: {deserializedNumbers.Length}");
            Debug.Log($"- 数据大小: {serializedData.Length} 字节");
        }

        /// <summary>
        /// 列表序列化
        /// </summary>
        public static void ListSerializationExample()
        {
            List<string> items = new List<string> { "苹果", "香蕉", "橙子", "葡萄" };
            
            // 序列化列表
            byte[] serializedData = SerializationUtility.SerializeValue(items, DataFormat.JSON);
            
            // 反序列化
            List<string> deserializedItems = SerializationUtility.DeserializeValue<List<string>>(serializedData, DataFormat.JSON);
            
            Debug.Log($"列表序列化:");
            Debug.Log($"- 原始列表: {string.Join(", ", items)}");
            Debug.Log($"- 反序列化列表: {string.Join(", ", deserializedItems)}");
        }

        /// <summary>
        /// 字典序列化
        /// </summary>
        public static void DictionarySerializationExample()
        {
            Dictionary<string, int> scores = new Dictionary<string, int>
            {
                { "玩家1", 100 },
                { "玩家2", 200 },
                { "玩家3", 300 }
            };
            
            // 序列化字典
            byte[] serializedData = SerializationUtility.SerializeValue(scores, DataFormat.Binary);
            
            // 反序列化
            Dictionary<string, int> deserializedScores = SerializationUtility.DeserializeValue<Dictionary<string, int>>(serializedData, DataFormat.Binary);
            
            Debug.Log($"字典序列化:");
            Debug.Log($"- 原始字典数量: {scores.Count}");
            Debug.Log($"- 反序列化字典数量: {deserializedScores.Count}");
            
            foreach (var kvp in deserializedScores)
            {
                Debug.Log($"  - {kvp.Key}: {kvp.Value}");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 保存和加载数据
        /// </summary>
        public static void SaveAndLoadDataExample()
        {
            // 创建数据
            TestData data = new TestData
            {
                name = "保存测试",
                value = 888,
                isActive = true,
                position = new Vector3(7, 8, 9)
            };

            // 序列化并保存
            byte[] serializedData = SerializationUtility.SerializeValue(data, DataFormat.Binary);
            string filePath = "Assets/TestData.dat";
            System.IO.File.WriteAllBytes(filePath, serializedData);
            
            Debug.Log($"数据已保存到: {filePath}");

            // 加载并反序列化
            if (System.IO.File.Exists(filePath))
            {
                byte[] loadedData = System.IO.File.ReadAllBytes(filePath);
                TestData deserializedData = SerializationUtility.DeserializeValue<TestData>(loadedData, DataFormat.Binary);
                
                Debug.Log($"数据已加载:");
                Debug.Log($"- 名称: {deserializedData.name}");
                Debug.Log($"- 值: {deserializedData.value}");
                Debug.Log($"- 状态: {deserializedData.isActive}");
                
                // 清理
                System.IO.File.Delete(filePath);
            }
        }

        /// <summary>
        /// 批量序列化
        /// </summary>
        public static void BatchSerializationExample()
        {
            List<TestData> dataList = new List<TestData>();
            
            for (int i = 0; i < 10; i++)
            {
                dataList.Add(new TestData
                {
                    name = $"数据{i}",
                    value = i * 10,
                    isActive = i % 2 == 0,
                    position = new Vector3(i, i, i)
                });
            }

            // 批量序列化
            byte[] serializedData = SerializationUtility.SerializeValue(dataList, DataFormat.Binary);
            
            // 反序列化
            List<TestData> deserializedList = SerializationUtility.DeserializeValue<List<TestData>>(serializedData, DataFormat.Binary);
            
            Debug.Log($"批量序列化完成:");
            Debug.Log($"- 原始数量: {dataList.Count}");
            Debug.Log($"- 反序列化数量: {deserializedList.Count}");
            Debug.Log($"- 数据大小: {serializedData.Length} 字节");
        }

        #endregion

        #region 数据结构定义

        [Serializable]
        public class TestData
        {
            public string name;
            public int value;
            public bool isActive;
            public Vector3 position;
        }

        [Serializable]
        public class ComplexData
        {
            public int id;
            public string title;
            public List<string> items;
            public Dictionary<string, int> metadata;
            public TestData nestedData;
        }

        #endregion
    }
}
