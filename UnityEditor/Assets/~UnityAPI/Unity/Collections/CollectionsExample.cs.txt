using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using System.Collections.Generic;

/// <summary>
/// Unity.Collections 命名空间案例演示
/// 展示Unity高性能集合系统的核心功能
/// </summary>
public class CollectionsExample : MonoBehaviour
{
    [Header("NativeArray 示例")]
    [SerializeField] private int arraySize = 1000;
    [SerializeField] private bool useJobSystem = true;
    
    [Header("性能测试")]
    [SerializeField] private float nativeArrayTime = 0f;
    [SerializeField] private float managedArrayTime = 0f;
    [SerializeField] private float speedupRatio = 0f;
    
    // NativeArray 示例
    private NativeArray<int> nativeArray;
    private NativeArray<float> nativeFloatArray;
    private NativeArray<Vector3> nativeVectorArray;
    
    // NativeList 示例
    private NativeList<int> nativeList;
    private NativeList<string> nativeStringList;
    
    // NativeHashMap 示例
    private NativeHashMap<int, string> nativeHashMap;
    private NativeHashMap<string, Vector3> nativeVectorMap;
    
    // NativeQueue 示例
    private NativeQueue<int> nativeQueue;
    private NativeQueue<GameObject> nativeGameObjectQueue;
    
    // NativeStack 示例
    private NativeStack<int> nativeStack;
    
    // 测试数据
    private int[] managedArray;
    private List<int> managedList;
    private Dictionary<int, string> managedDictionary;
    private Queue<int> managedQueue;
    private Stack<int> managedStack;
    
    private void Start()
    {
        InitializeCollections();
        RunPerformanceTest();
    }
    
    /// <summary>
    /// 初始化集合
    /// </summary>
    private void InitializeCollections()
    {
        // 初始化 NativeArray
        nativeArray = new NativeArray<int>(arraySize, Allocator.Persistent);
        nativeFloatArray = new NativeArray<float>(arraySize, Allocator.Persistent);
        nativeVectorArray = new NativeArray<Vector3>(arraySize, Allocator.Persistent);
        
        // 初始化 NativeList
        nativeList = new NativeList<int>(Allocator.Persistent);
        nativeStringList = new NativeList<string>(Allocator.Persistent);
        
        // 初始化 NativeHashMap
        nativeHashMap = new NativeHashMap<int, string>(arraySize, Allocator.Persistent);
        nativeVectorMap = new NativeHashMap<string, Vector3>(arraySize, Allocator.Persistent);
        
        // 初始化 NativeQueue
        nativeQueue = new NativeQueue<int>(Allocator.Persistent);
        nativeGameObjectQueue = new NativeQueue<GameObject>(Allocator.Persistent);
        
        // 初始化 NativeStack
        nativeStack = new NativeStack<int>(Allocator.Persistent);
        
        // 初始化托管集合
        managedArray = new int[arraySize];
        managedList = new List<int>();
        managedDictionary = new Dictionary<int, string>();
        managedQueue = new Queue<int>();
        managedStack = new Stack<int>();
        
        Debug.Log("集合系统初始化完成");
    }
    
    /// <summary>
    /// 运行性能测试
    /// </summary>
    private void RunPerformanceTest()
    {
        // 测试 NativeArray 性能
        float startTime = Time.realtimeSinceStartup;
        TestNativeArrayPerformance();
        nativeArrayTime = Time.realtimeSinceStartup - startTime;
        
        // 测试托管数组性能
        startTime = Time.realtimeSinceStartup;
        TestManagedArrayPerformance();
        managedArrayTime = Time.realtimeSinceStartup - startTime;
        
        // 计算加速比
        speedupRatio = managedArrayTime / nativeArrayTime;
        
        Debug.Log($"性能测试结果 - NativeArray: {nativeArrayTime:F4}s, ManagedArray: {managedArrayTime:F4}s, 加速比: {speedupRatio:F2}x");
    }
    
    /// <summary>
    /// 测试 NativeArray 性能
    /// </summary>
    private void TestNativeArrayPerformance()
    {
        // 填充数据
        for (int i = 0; i < arraySize; i++)
        {
            nativeArray[i] = i;
            nativeFloatArray[i] = i * 0.1f;
            nativeVectorArray[i] = new Vector3(i, i * 0.5f, i * 0.25f);
        }
        
        // 执行计算
        int sum = 0;
        float floatSum = 0f;
        Vector3 vectorSum = Vector3.zero;
        
        for (int i = 0; i < arraySize; i++)
        {
            sum += nativeArray[i];
            floatSum += nativeFloatArray[i];
            vectorSum += nativeVectorArray[i];
        }
        
        // 防止编译器优化
        if (sum > 0 && floatSum > 0 && vectorSum.magnitude > 0)
        {
            // 空操作
        }
    }
    
    /// <summary>
    /// 测试托管数组性能
    /// </summary>
    private void TestManagedArrayPerformance()
    {
        // 填充数据
        for (int i = 0; i < arraySize; i++)
        {
            managedArray[i] = i;
        }
        
        // 执行计算
        int sum = 0;
        for (int i = 0; i < arraySize; i++)
        {
            sum += managedArray[i];
        }
        
        // 防止编译器优化
        if (sum > 0)
        {
            // 空操作
        }
    }
    
    /// <summary>
    /// 演示 NativeArray 操作
    /// </summary>
    public void DemonstrateNativeArray()
    {
        Debug.Log("=== NativeArray 演示 ===");
        
        // 基本操作
        for (int i = 0; i < 10; i++)
        {
            nativeArray[i] = i * i;
            Debug.Log($"nativeArray[{i}] = {nativeArray[i]}");
        }
        
        // 复制操作
        NativeArray<int> copyArray = new NativeArray<int>(nativeArray, Allocator.Temp);
        Debug.Log($"复制数组长度: {copyArray.Length}");
        copyArray.Dispose();
        
        // 切片操作
        NativeSlice<int> slice = nativeArray.Slice(5, 5);
        Debug.Log($"切片长度: {slice.Length}, 第一个元素: {slice[0]}");
    }
    
    /// <summary>
    /// 演示 NativeList 操作
    /// </summary>
    public void DemonstrateNativeList()
    {
        Debug.Log("=== NativeList 演示 ===");
        
        // 添加元素
        nativeList.Add(10);
        nativeList.Add(20);
        nativeList.Add(30);
        
        Debug.Log($"NativeList 长度: {nativeList.Length}");
        Debug.Log($"NativeList 容量: {nativeList.Capacity}");
        
        // 访问元素
        for (int i = 0; i < nativeList.Length; i++)
        {
            Debug.Log($"nativeList[{i}] = {nativeList[i]}");
        }
        
        // 移除元素
        nativeList.RemoveAt(1);
        Debug.Log($"移除后长度: {nativeList.Length}");
        
        // 清空列表
        nativeList.Clear();
        Debug.Log($"清空后长度: {nativeList.Length}");
    }
    
    /// <summary>
    /// 演示 NativeHashMap 操作
    /// </summary>
    public void DemonstrateNativeHashMap()
    {
        Debug.Log("=== NativeHashMap 演示 ===");
        
        // 添加键值对
        nativeHashMap.Add(1, "One");
        nativeHashMap.Add(2, "Two");
        nativeHashMap.Add(3, "Three");
        
        Debug.Log($"HashMap 长度: {nativeHashMap.Length()}");
        
        // 访问值
        if (nativeHashMap.TryGetValue(2, out string value))
        {
            Debug.Log($"Key 2 的值: {value}");
        }
        
        // 检查键是否存在
        Debug.Log($"Key 4 存在: {nativeHashMap.ContainsKey(4)}");
        
        // 移除键值对
        nativeHashMap.Remove(2);
        Debug.Log($"移除后长度: {nativeHashMap.Length()}");
        
        // 遍历所有键值对
        var keyValueEnumerator = nativeHashMap.GetEnumerator();
        while (keyValueEnumerator.MoveNext())
        {
            var kvp = keyValueEnumerator.Current;
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
    
    /// <summary>
    /// 演示 NativeQueue 操作
    /// </summary>
    public void DemonstrateNativeQueue()
    {
        Debug.Log("=== NativeQueue 演示 ===");
        
        // 入队
        nativeQueue.Enqueue(100);
        nativeQueue.Enqueue(200);
        nativeQueue.Enqueue(300);
        
        Debug.Log($"Queue 长度: {nativeQueue.Count}");
        
        // 出队
        while (nativeQueue.Count > 0)
        {
            int item = nativeQueue.Dequeue();
            Debug.Log($"出队: {item}");
        }
        
        Debug.Log($"出队后长度: {nativeQueue.Count}");
    }
    
    /// <summary>
    /// 演示 NativeStack 操作
    /// </summary>
    public void DemonstrateNativeStack()
    {
        Debug.Log("=== NativeStack 演示 ===");
        
        // 压栈
        nativeStack.Push(1000);
        nativeStack.Push(2000);
        nativeStack.Push(3000);
        
        Debug.Log($"Stack 长度: {nativeStack.Count}");
        
        // 出栈
        while (nativeStack.Count > 0)
        {
            int item = nativeStack.Pop();
            Debug.Log($"出栈: {item}");
        }
        
        Debug.Log($"出栈后长度: {nativeStack.Count}");
    }
    
    /// <summary>
    /// 演示 Job System 集成
    /// </summary>
    public void DemonstrateJobSystem()
    {
        if (!useJobSystem) return;
        
        Debug.Log("=== Job System 集成演示 ===");
        
        // 创建作业
        var job = new ArrayProcessJob
        {
            inputArray = nativeArray,
            outputArray = new NativeArray<int>(arraySize, Allocator.TempJob)
        };
        
        // 调度作业
        JobHandle jobHandle = job.Schedule(arraySize, 64);
        
        // 等待完成
        jobHandle.Complete();
        
        // 检查结果
        Debug.Log($"作业完成，输出数组第一个元素: {job.outputArray[0]}");
        
        // 清理
        job.outputArray.Dispose();
    }
    
    /// <summary>
    /// 演示并行作业
    /// </summary>
    public void DemonstrateParallelJob()
    {
        if (!useJobSystem) return;
        
        Debug.Log("=== 并行作业演示 ===");
        
        // 创建并行作业
        var parallelJob = new ParallelArrayJob
        {
            array = nativeArray
        };
        
        // 调度并行作业
        JobHandle jobHandle = parallelJob.Schedule(arraySize, 64);
        jobHandle.Complete();
        
        Debug.Log("并行作业完成");
    }
    
    /// <summary>
    /// 演示内存管理
    /// </summary>
    public void DemonstrateMemoryManagement()
    {
        Debug.Log("=== 内存管理演示 ===");
        
        // 使用 Temp 分配器（自动管理）
        using (var tempArray = new NativeArray<int>(100, Allocator.Temp))
        {
            Debug.Log($"临时数组长度: {tempArray.Length}");
            // 离开作用域时自动释放
        }
        
        // 使用 TempJob 分配器（需要手动释放）
        var jobArray = new NativeArray<int>(100, Allocator.TempJob);
        Debug.Log($"作业数组长度: {jobArray.Length}");
        jobArray.Dispose(); // 手动释放
        
        Debug.Log("内存管理演示完成");
    }
    
    /// <summary>
    /// 比较托管集合和 Native 集合
    /// </summary>
    public void CompareCollections()
    {
        Debug.Log("=== 集合比较 ===");
        
        // 填充托管集合
        for (int i = 0; i < 100; i++)
        {
            managedList.Add(i);
            managedDictionary[i] = $"Value_{i}";
            managedQueue.Enqueue(i);
            managedStack.Push(i);
        }
        
        // 填充 Native 集合
        for (int i = 0; i < 100; i++)
        {
            nativeList.Add(i);
            nativeHashMap.Add(i, $"Value_{i}");
            nativeQueue.Enqueue(i);
            nativeStack.Push(i);
        }
        
        Debug.Log($"托管 List 长度: {managedList.Count}");
        Debug.Log($"Native List 长度: {nativeList.Length}");
        Debug.Log($"托管 Dictionary 长度: {managedDictionary.Count}");
        Debug.Log($"Native HashMap 长度: {nativeHashMap.Length()}");
        Debug.Log($"托管 Queue 长度: {managedQueue.Count}");
        Debug.Log($"Native Queue 长度: {nativeQueue.Count}");
        Debug.Log($"托管 Stack 长度: {managedStack.Count}");
        Debug.Log($"Native Stack 长度: {nativeStack.Count}");
    }
    
    /// <summary>
    /// 清理所有集合
    /// </summary>
    public void ClearAllCollections()
    {
        // 清理 Native 集合
        if (nativeArray.IsCreated) nativeArray.Dispose();
        if (nativeFloatArray.IsCreated) nativeFloatArray.Dispose();
        if (nativeVectorArray.IsCreated) nativeVectorArray.Dispose();
        if (nativeList.IsCreated) nativeList.Dispose();
        if (nativeStringList.IsCreated) nativeStringList.Dispose();
        if (nativeHashMap.IsCreated) nativeHashMap.Dispose();
        if (nativeVectorMap.IsCreated) nativeVectorMap.Dispose();
        if (nativeQueue.IsCreated) nativeQueue.Dispose();
        if (nativeGameObjectQueue.IsCreated) nativeGameObjectQueue.Dispose();
        if (nativeStack.IsCreated) nativeStack.Dispose();
        
        // 清理托管集合
        managedList.Clear();
        managedDictionary.Clear();
        managedQueue.Clear();
        managedStack.Clear();
        
        Debug.Log("所有集合已清理");
    }
    
    private void OnDestroy()
    {
        // 确保在销毁时释放所有 Native 集合
        ClearAllCollections();
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 600));
        GUILayout.Label("Unity Collections 演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 性能信息
        GUILayout.Label($"NativeArray 时间: {nativeArrayTime:F4}s");
        GUILayout.Label($"ManagedArray 时间: {managedArrayTime:F4}s");
        GUILayout.Label($"加速比: {speedupRatio:F2}x");
        
        GUILayout.Space(10);
        
        // 操作按钮
        if (GUILayout.Button("演示 NativeArray"))
        {
            DemonstrateNativeArray();
        }
        
        if (GUILayout.Button("演示 NativeList"))
        {
            DemonstrateNativeList();
        }
        
        if (GUILayout.Button("演示 NativeHashMap"))
        {
            DemonstrateNativeHashMap();
        }
        
        if (GUILayout.Button("演示 NativeQueue"))
        {
            DemonstrateNativeQueue();
        }
        
        if (GUILayout.Button("演示 NativeStack"))
        {
            DemonstrateNativeStack();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("演示 Job System"))
        {
            DemonstrateJobSystem();
        }
        
        if (GUILayout.Button("演示并行作业"))
        {
            DemonstrateParallelJob();
        }
        
        if (GUILayout.Button("演示内存管理"))
        {
            DemonstrateMemoryManagement();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("比较集合"))
        {
            CompareCollections();
        }
        
        if (GUILayout.Button("清理所有集合"))
        {
            ClearAllCollections();
        }
        
        GUILayout.Space(10);
        
        useJobSystem = GUILayout.Toggle(useJobSystem, "启用 Job System");
        
        GUILayout.EndArea();
    }
}

/// <summary>
/// 数组处理作业
/// </summary>
public struct ArrayProcessJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<int> inputArray;
    [WriteOnly] public NativeArray<int> outputArray;
    
    public void Execute(int index)
    {
        outputArray[index] = inputArray[index] * 2;
    }
}

/// <summary>
/// 并行数组作业
/// </summary>
public struct ParallelArrayJob : IJobParallelFor
{
    public NativeArray<int> array;
    
    public void Execute(int index)
    {
        array[index] = array[index] + 1;
    }
} 