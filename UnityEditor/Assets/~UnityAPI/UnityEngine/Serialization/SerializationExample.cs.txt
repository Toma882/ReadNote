using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Serialization 命名空间案例演示
/// 展示ISerializationCallbackReceiver、自定义序列化等核心功能
/// </summary>
[Serializable]
public class SerializationExample : MonoBehaviour, ISerializationCallbackReceiver
{
    [Header("序列化设置")]
    [SerializeField] private string dataString = "Hello World"; //数据字符串
    [SerializeField] private int dataInt = 42; //数据整数
    [SerializeField] private float dataFloat = 3.14f; //数据浮点数
    [SerializeField] private Vector3 dataVector = Vector3.one; //数据向量
    [SerializeField] private List<string> dataList = new List<string>(); //数据列表

    [Header("序列化状态")]
    [SerializeField] private bool isSerialized = false; //是否已序列化
    [SerializeField] private string serializationTime = ""; //序列化时间

    private void Start()
    {
        // 初始化数据列表
        if (dataList.Count == 0)
        {
            dataList.Add("Item 1");
            dataList.Add("Item 2");
            dataList.Add("Item 3");
        }
    }

    /// <summary>
    /// 序列化前回调
    /// </summary>
    public void OnBeforeSerialize()
    {
        isSerialized = true;
        serializationTime = DateTime.Now.ToString("HH:mm:ss");
        Debug.Log("序列化前回调执行");
    }

    /// <summary>
    /// 反序列化后回调
    /// </summary>
    public void OnAfterDeserialize()
    {
        Debug.Log("反序列化后回调执行");
        // 可以在这里进行数据验证和修复
        ValidateData();
    }

    /// <summary>
    /// 验证数据
    /// </summary>
    private void ValidateData()
    {
        if (string.IsNullOrEmpty(dataString))
        {
            dataString = "Default String";
        }
        if (dataInt < 0)
        {
            dataInt = 0;
        }
        if (dataFloat < 0)
        {
            dataFloat = 0f;
        }
        if (dataList == null)
        {
            dataList = new List<string>();
        }
    }

    /// <summary>
    /// 添加数据到列表
    /// </summary>
    public void AddDataToList(string item)
    {
        if (dataList == null)
        {
            dataList = new List<string>();
        }
        dataList.Add(item);
        Debug.Log($"添加数据: {item}");
    }

    /// <summary>
    /// 清空数据列表
    /// </summary>
    public void ClearDataList()
    {
        if (dataList != null)
        {
            dataList.Clear();
            Debug.Log("数据列表已清空");
        }
    }

    /// <summary>
    /// 获取序列化信息
    /// </summary>
    public void GetSerializationInfo()
    {
        Debug.Log("=== 序列化信息 ===");
        Debug.Log($"数据字符串: {dataString}");
        Debug.Log($"数据整数: {dataInt}");
        Debug.Log($"数据浮点数: {dataFloat}");
        Debug.Log($"数据向量: {dataVector}");
        Debug.Log($"数据列表数量: {dataList?.Count ?? 0}");
        Debug.Log($"是否已序列化: {isSerialized}");
        Debug.Log($"序列化时间: {serializationTime}");
        
        if (dataList != null && dataList.Count > 0)
        {
            Debug.Log("数据列表内容:");
            for (int i = 0; i < dataList.Count; i++)
            {
                Debug.Log($"  [{i}]: {dataList[i]}");
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 400));
        GUILayout.Label("Serialization 序列化演示", UnityEditor.EditorStyles.boldLabel);
        
        dataString = GUILayout.TextField("数据字符串", dataString);
        dataInt = int.TryParse(GUILayout.TextField("数据整数", dataInt.ToString()), out var i) ? i : dataInt;
        dataFloat = float.TryParse(GUILayout.TextField("数据浮点数", dataFloat.ToString()), out var f) ? f : dataFloat;
        
        GUILayout.Label($"数据向量: {dataVector}");
        GUILayout.Label($"数据列表数量: {dataList?.Count ?? 0}");
        GUILayout.Label($"是否已序列化: {isSerialized}");
        GUILayout.Label($"序列化时间: {serializationTime}");
        
        if (GUILayout.Button("添加数据到列表"))
        {
            AddDataToList($"Item {dataList?.Count + 1 ?? 1}");
        }
        
        if (GUILayout.Button("清空数据列表"))
        {
            ClearDataList();
        }
        
        if (GUILayout.Button("获取序列化信息"))
        {
            GetSerializationInfo();
        }
        
        GUILayout.EndArea();
    }
} 