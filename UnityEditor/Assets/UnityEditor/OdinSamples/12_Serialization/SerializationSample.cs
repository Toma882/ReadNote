using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace OdinSamples.Serialization
{
    /*
        学习要点：
        ----------------------
        Odin Serialization 是 Odin Inspector 提供的强大序列化系统
        它解决了 Unity 原生序列化的诸多限制

        核心概念：
        ----------------------
        1. SerializedMonoBehaviour / SerializedScriptableObject
           - 继承这些基类，启用 Odin 序列化系统
           - 支持更多类型（Dictionary、接口、抽象类等）

        2. [OdinSerialize] 特性
           - 标记字段使用 Odin 序列化
           - 支持 Unity 原生不支持的类型

        3. 支持的序列化类型：
           - Dictionary<TKey, TValue>（Unity 原生不支持）
           - 接口类型（Unity 原生不支持）
           - 抽象类（Unity 原生不支持）
           - 泛型类型（Unity 原生限制多）
           - null 值（Unity 原生限制多）
           - 循环引用（Unity 原生不支持）

        4. 序列化策略：
           - SerializationPolicies.Unity：兼容 Unity 原生序列化规则
           - SerializationPolicies.Everything：序列化所有字段
           - SerializationPolicies.Strict：严格模式

        使用场景：
        ----------------------
        - 需要序列化 Dictionary 时
        - 需要序列化接口或抽象类时
        - 需要更灵活的序列化控制时
        - 需要序列化复杂数据结构时

        注意事项：
        ----------------------
        - 使用 SerializedMonoBehaviour/ScriptableObject 后，所有字段默认使用 Odin 序列化
        - [OdinSerialize] 可以标记特定字段使用 Odin 序列化
        - 与 Unity 原生序列化可以混用（使用 [SerializeField]）
        - 文件大小可能比 Unity 原生序列化稍大（但功能更强大）
        ----------------------
    */

    /// <summary>
    /// Odin Serialization 序列化系统演示
    /// 展示 Odin 序列化系统相比 Unity 原生序列化的优势
    /// </summary>
    public class SerializationSample : SerializedMonoBehaviour
    {
        [Title("1. Dictionary 序列化（Unity 原生不支持）")]
        [InfoBox("Unity 原生序列化不支持 Dictionary，Odin 完全支持！", InfoMessageType.Info)]
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "精灵GUID", ValueLabel = "突出点数据")]
        private Dictionary<string, Vector2> spritePointDict = new Dictionary<string, Vector2>();

        [Title("2. 接口类型序列化（Unity 原生不支持）")]
        [InfoBox("Unity 原生序列化不支持接口，Odin 支持多态序列化！", InfoMessageType.Info)]
        [OdinSerialize]
        [LabelText("接口类型数据")]
        private IDataContainer dataContainer;

        [Title("3. 抽象类序列化（Unity 原生不支持）")]
        [InfoBox("Unity 原生序列化不支持抽象类，Odin 支持！", InfoMessageType.Info)]
        [OdinSerialize]
        [LabelText("抽象类数据")]
        private BaseData baseData;

        [Title("4. 复杂泛型类型序列化")]
        [InfoBox("Unity 原生序列化对泛型支持有限，Odin 支持复杂泛型！", InfoMessageType.Info)]
        [OdinSerialize]
        [LabelText("泛型字典列表")]
        private List<Dictionary<string, int>> complexGenericList = new List<Dictionary<string, int>>();

        [Title("5. Null 值支持")]
        [InfoBox("Unity 原生序列化对 null 值支持有限，Odin 完全支持！", InfoMessageType.Info)]
        [OdinSerialize]
        [LabelText("可为空的字典")]
        private Dictionary<string, string> nullableDict;

        [Title("6. 与 Unity 原生序列化混用")]
        [InfoBox("可以在同一个类中混用 Odin 序列化和 Unity 原生序列化", InfoMessageType.Info)]
        [SerializeField] // Unity 原生序列化
        private int unitySerializedInt = 10;

        [OdinSerialize] // Odin 序列化
        private Dictionary<string, int> odinSerializedDict = new Dictionary<string, int>();

        [Title("7. 按钮测试")]
        [Button("添加示例数据到字典")]
        private void AddSampleData()
        {
            spritePointDict["sprite_001"] = new Vector2(0.5f, 1.2f);
            spritePointDict["sprite_002"] = new Vector2(-0.3f, 0.8f);
            spritePointDict["sprite_003"] = new Vector2(0.2f, 1.5f);
            Debug.Log("已添加示例数据到字典");
        }

        [Button("清空字典")]
        private void ClearDictionary()
        {
            spritePointDict.Clear();
            Debug.Log("已清空字典");
        }

        [Button("测试接口序列化")]
        private void TestInterfaceSerialization()
        {
            dataContainer = new ConcreteDataContainer { Data = "接口数据测试" };
            Debug.Log($"接口数据: {dataContainer.Data}");
        }

        [Button("测试抽象类序列化")]
        private void TestAbstractClassSerialization()
        {
            baseData = new ConcreteData { Value = 42 };
            Debug.Log($"抽象类数据: {baseData.Value}");
        }
    }

    /// <summary>
    /// 接口示例（Unity 原生不支持序列化接口）
    /// </summary>
    public interface IDataContainer
    {
        string Data { get; set; }
    }

    /// <summary>
    /// 接口实现类
    /// </summary>
    [System.Serializable]
    public class ConcreteDataContainer : IDataContainer
    {
        public string Data { get; set; }
    }

    /// <summary>
    /// 抽象类示例（Unity 原生不支持序列化抽象类）
    /// </summary>
    public abstract class BaseData
    {
        public abstract int Value { get; set; }
    }

    /// <summary>
    /// 抽象类实现
    /// </summary>
    [System.Serializable]
    public class ConcreteData : BaseData
    {
        [SerializeField]
        private int value;

        public override int Value
        {
            get => value;
            set => this.value = value;
        }
    }

    /// <summary>
    /// ScriptableObject 序列化示例
    /// </summary>
    [CreateAssetMenu(fileName = "SerializationData", menuName = "Odin Samples/Serialization Data")]
    public class SerializationDataSample : SerializedScriptableObject
    {
        [Title("ScriptableObject 中的 Dictionary")]
        [InfoBox("在 ScriptableObject 中使用 Odin 序列化，支持 Dictionary！", InfoMessageType.Info)]
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "键", ValueLabel = "值")]
        private Dictionary<string, float> dataDict = new Dictionary<string, float>();

        [Title("ScriptableObject 中的接口")]
        [OdinSerialize]
        private IDataContainer interfaceData;

        [Button("添加数据")]
        private void AddData()
        {
            dataDict["key1"] = 1.5f;
            dataDict["key2"] = 2.3f;
            dataDict["key3"] = 3.7f;
        }

        [Button("清空数据")]
        private void ClearData()
        {
            dataDict.Clear();
        }
    }
}

