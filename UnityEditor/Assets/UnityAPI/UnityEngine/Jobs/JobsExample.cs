using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

/// <summary>
/// UnityEngine.Jobs 命名空间案例演示
///主要用于实现高性能的​​多线程并行计算​​，尤其适用于需要处理大规模数据或计算密集型任务的场景。
///以下是其核心用途及技术细节：
/// 展示IJob、IJobParallelFor、JobHandle等核心功能
/// </summary>
public class JobsExample : MonoBehaviour
{
    [Header("Jobs 设置")]
    [SerializeField] private int arraySize = 10000; //数组大小
    [SerializeField] private bool useJobs = true; //是否使用Jobs
    [SerializeField] private bool useParallelJobs = false; //是否使用并行Jobs
    [SerializeField] private float jobTime = 0f; //Job执行时间

    private NativeArray<float> inputArray;
    private NativeArray<float> outputArray;
    private JobHandle jobHandle;

    private void Start()
    {
        // 初始化数组
        inputArray = new NativeArray<float>(arraySize, Allocator.Persistent);
        outputArray = new NativeArray<float>(arraySize, Allocator.Persistent);
        
        for (int i = 0; i < arraySize; i++)
        {
            inputArray[i] = i;
        }
    }

    private void Update()
    {
        if (useJobs)
        {
            if (useParallelJobs)
            {
                ExecuteParallelJob();
            }
            else
            {
                ExecuteSimpleJob();
            }
        }
        else
        {
            ExecuteMainThread();
        }
    }

    /// <summary>
    /// 执行简单Job
    /// </summary>
    private void ExecuteSimpleJob()
    {
        var job = new SimpleJob
        {
            input = inputArray,
            output = outputArray,
            multiplier = Time.time
        };

        var startTime = Time.realtimeSinceStartup;
        jobHandle = job.Schedule();
        jobHandle.Complete();
        jobTime = Time.realtimeSinceStartup - startTime;
    }

    /// <summary>
    /// 执行并行Job
    /// </summary>
    private void ExecuteParallelJob()
    {
        var job = new ParallelJob
        {
            input = inputArray,
            output = outputArray,
            multiplier = Time.time
        };

        var startTime = Time.realtimeSinceStartup;
        jobHandle = job.Schedule(arraySize, 64);
        jobHandle.Complete();
        jobTime = Time.realtimeSinceStartup - startTime;
    }

    /// <summary>
    /// 主线程执行
    /// </summary>
    private void ExecuteMainThread()
    {
        var startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < arraySize; i++)
        {
            outputArray[i] = inputArray[i] * Time.time;
        }
        jobTime = Time.realtimeSinceStartup - startTime;
    }

    private void OnDestroy()
    {
        if (inputArray.IsCreated)
            inputArray.Dispose();
        if (outputArray.IsCreated)
            outputArray.Dispose();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 250));
        GUILayout.Label("Jobs 作业系统演示", UnityEditor.EditorStyles.boldLabel);
        arraySize = int.TryParse(GUILayout.TextField(arraySize.ToString()), out var size) ? size : arraySize;
        useJobs = GUILayout.Toggle(useJobs, "使用Jobs");
        useParallelJobs = GUILayout.Toggle(useParallelJobs, "使用并行Jobs");
        GUILayout.Label($"执行时间: {jobTime:F4}s");
        GUILayout.Label($"数组大小: {arraySize}");
        if (GUILayout.Button("重新初始化数组"))
        {
            if (inputArray.IsCreated)
                inputArray.Dispose();
            if (outputArray.IsCreated)
                outputArray.Dispose();
            
            inputArray = new NativeArray<float>(arraySize, Allocator.Persistent);
            outputArray = new NativeArray<float>(arraySize, Allocator.Persistent);
            
            for (int i = 0; i < arraySize; i++)
            {
                inputArray[i] = i;
            }
        }
        GUILayout.EndArea();
    }
}

/// <summary>
/// 简单Job结构
/// </summary>
public struct SimpleJob : IJob
{
    [ReadOnly] public NativeArray<float> input;
    public NativeArray<float> output;
    public float multiplier;

    public void Execute()
    {
        for (int i = 0; i < input.Length; i++)
        {
            output[i] = input[i] * multiplier;
        }
    }
}

/// <summary>
/// 并行Job结构
/// </summary>
public struct ParallelJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<float> input;
    public NativeArray<float> output;
    public float multiplier;

    public void Execute(int index)
    {
        output[index] = input[index] * multiplier;
    }
} 