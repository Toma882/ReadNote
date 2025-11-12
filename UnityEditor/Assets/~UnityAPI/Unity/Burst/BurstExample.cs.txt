using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using System;

/// <summary>
/// Unity.Burst 命名空间案例演示
/// 展示Burst编译器的高性能计算功能
/// </summary>
public class BurstExample : MonoBehaviour
{
    [Header("Burst设置")]
    [SerializeField] private bool enableBurst = true;
    [SerializeField] private int arraySize = 1000000;
    [SerializeField] private int iterationCount = 100;
    
    [Header("性能测试")]
    [SerializeField] private float burstTime = 0f;
    [SerializeField] private float managedTime = 0f;
    [SerializeField] private float speedup = 0f;
    
    [Header("计算结果")]
    [SerializeField] private float[] results;
    [SerializeField] private float sum = 0f;
    [SerializeField] private float average = 0f;
    [SerializeField] private float maxValue = 0f;
    [SerializeField] private float minValue = 0f;
    
    // 测试数据
    private NativeArray<float> testData;
    private bool isInitialized = false;
    
    private void Start()
    {
        InitializeBurstSystem();
    }
    
    /// <summary>
    /// 初始化Burst系统
    /// </summary>
    private void InitializeBurstSystem()
    {
        // 检查Burst是否可用
        if (!BurstCompiler.IsEnabled)
        {
            Debug.LogWarning("Burst编译器未启用");
            return;
        }
        
        // 创建测试数据
        CreateTestData();
        
        isInitialized = true;
        Debug.Log("Burst系统初始化完成");
    }
    
    /// <summary>
    /// 创建测试数据
    /// </summary>
    private void CreateTestData()
    {
        // 分配原生数组
        testData = new NativeArray<float>(arraySize, Allocator.Persistent);
        
        // 填充随机数据
        for (int i = 0; i < arraySize; i++)
        {
            testData[i] = UnityEngine.Random.Range(0f, 1000f);
        }
        
        Debug.Log($"测试数据已创建，大小: {arraySize}");
    }
    
    /// <summary>
    /// 运行Burst性能测试
    /// </summary>
    [ContextMenu("运行Burst性能测试")]
    public void RunBurstPerformanceTest()
    {
        if (!isInitialized)
        {
            Debug.LogError("Burst系统未初始化");
            return;
        }
        
        Debug.Log("开始Burst性能测试...");
        
        // 测试Burst版本
        var burstJob = new BurstMathJob
        {
            input = testData,
            output = new NativeArray<float>(arraySize, Allocator.TempJob)
        };
        
        var burstHandle = burstJob.Schedule();
        
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        burstHandle.Complete();
        stopwatch.Stop();
        
        burstTime = (float)stopwatch.Elapsed.TotalMilliseconds;
        
        // 保存结果
        results = new float[arraySize];
        burstJob.output.CopyTo(results);
        
        // 计算统计信息
        CalculateStatistics();
        
        // 清理
        burstJob.output.Dispose();
        
        Debug.Log($"Burst版本完成，耗时: {burstTime:F2}ms");
    }
    
    /// <summary>
    /// 运行托管代码性能测试
    /// </summary>
    [ContextMenu("运行托管代码性能测试")]
    public void RunManagedPerformanceTest()
    {
        if (!isInitialized)
        {
            Debug.LogError("Burst系统未初始化");
            return;
        }
        
        Debug.Log("开始托管代码性能测试...");
        
        var managedData = new float[arraySize];
        testData.CopyTo(managedData);
        
        var output = new float[arraySize];
        
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        // 执行托管代码计算
        for (int iter = 0; iter < iterationCount; iter++)
        {
            for (int i = 0; i < arraySize; i++)
            {
                output[i] = ManagedMath(managedData[i]);
            }
        }
        
        stopwatch.Stop();
        managedTime = (float)stopwatch.Elapsed.TotalMilliseconds;
        
        // 计算加速比
        if (managedTime > 0)
        {
            speedup = managedTime / burstTime;
        }
        
        Debug.Log($"托管代码版本完成，耗时: {managedTime:F2}ms");
        Debug.Log($"Burst加速比: {speedup:F2}x");
    }
    
    /// <summary>
    /// 托管代码数学计算
    /// </summary>
    /// <param name="value">输入值</param>
    /// <returns>计算结果</returns>
    private float ManagedMath(float value)
    {
        // 模拟复杂的数学计算
        float result = value;
        
        // 正弦波计算
        result += math.sin(value * 0.1f) * 10f;
        
        // 平方根计算
        result += math.sqrt(math.abs(value)) * 0.5f;
        
        // 指数计算
        result += math.exp(value * 0.01f) * 0.1f;
        
        // 对数计算
        if (value > 0)
        {
            result += math.log(value + 1f) * 2f;
        }
        
        // 三角函数
        result += math.cos(value * 0.05f) * 5f;
        result += math.tan(value * 0.02f) * 1f;
        
        // 幂运算
        result += math.pow(value, 0.5f) * 3f;
        
        return result;
    }
    
    /// <summary>
    /// 计算统计信息
    /// </summary>
    private void CalculateStatistics()
    {
        if (results == null || results.Length == 0)
        {
            return;
        }
        
        sum = 0f;
        maxValue = float.MinValue;
        minValue = float.MaxValue;
        
        for (int i = 0; i < results.Length; i++)
        {
            float value = results[i];
            sum += value;
            
            if (value > maxValue)
            {
                maxValue = value;
            }
            
            if (value < minValue)
            {
                minValue = value;
            }
        }
        
        average = sum / results.Length;
        
        Debug.Log($"统计信息 - 总和: {sum:F2}, 平均值: {average:F2}, 最大值: {maxValue:F2}, 最小值: {minValue:F2}");
    }
    
    /// <summary>
    /// 运行向量运算测试
    /// </summary>
    [ContextMenu("运行向量运算测试")]
    public void RunVectorMathTest()
    {
        if (!isInitialized)
        {
            Debug.LogError("Burst系统未初始化");
            return;
        }
        
        Debug.Log("开始向量运算测试...");
        
        var vectorJob = new BurstVectorJob
        {
            input = new NativeArray<float3>(arraySize, Allocator.TempJob),
            output = new NativeArray<float3>(arraySize, Allocator.TempJob)
        };
        
        // 填充向量数据
        for (int i = 0; i < arraySize; i++)
        {
            vectorJob.input[i] = new float3(
                UnityEngine.Random.Range(-100f, 100f),
                UnityEngine.Random.Range(-100f, 100f),
                UnityEngine.Random.Range(-100f, 100f)
            );
        }
        
        var handle = vectorJob.Schedule();
        handle.Complete();
        
        // 计算向量统计
        float3 sum = float3.zero;
        float maxMagnitude = 0f;
        
        for (int i = 0; i < arraySize; i++)
        {
            sum += vectorJob.output[i];
            float magnitude = math.length(vectorJob.output[i]);
            if (magnitude > maxMagnitude)
            {
                maxMagnitude = magnitude;
            }
        }
        
        float3 average = sum / arraySize;
        
        Debug.Log($"向量运算完成 - 平均向量: {average}, 最大幅度: {maxMagnitude:F2}");
        
        // 清理
        vectorJob.input.Dispose();
        vectorJob.output.Dispose();
    }
    
    /// <summary>
    /// 运行矩阵运算测试
    /// </summary>
    [ContextMenu("运行矩阵运算测试")]
    public void RunMatrixMathTest()
    {
        if (!isInitialized)
        {
            Debug.LogError("Burst系统未初始化");
            return;
        }
        
        Debug.Log("开始矩阵运算测试...");
        
        var matrixJob = new BurstMatrixJob
        {
            input = new NativeArray<float4x4>(arraySize / 100, Allocator.TempJob),
            output = new NativeArray<float4x4>(arraySize / 100, Allocator.TempJob)
        };
        
        // 填充矩阵数据
        for (int i = 0; i < matrixJob.input.Length; i++)
        {
            matrixJob.input[i] = float4x4.TRS(
                new float3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f)),
                quaternion.Euler(UnityEngine.Random.Range(-180f, 180f), UnityEngine.Random.Range(-180f, 180f), UnityEngine.Random.Range(-180f, 180f)),
                new float3(UnityEngine.Random.Range(0.1f, 2f), UnityEngine.Random.Range(0.1f, 2f), UnityEngine.Random.Range(0.1f, 2f))
            );
        }
        
        var handle = matrixJob.Schedule();
        handle.Complete();
        
        Debug.Log($"矩阵运算完成，处理了 {matrixJob.input.Length} 个矩阵");
        
        // 清理
        matrixJob.input.Dispose();
        matrixJob.output.Dispose();
    }
    
    /// <summary>
    /// 运行并行计算测试
    /// </summary>
    [ContextMenu("运行并行计算测试")]
    public void RunParallelComputationTest()
    {
        if (!isInitialized)
        {
            Debug.LogError("Burst系统未初始化");
            return;
        }
        
        Debug.Log("开始并行计算测试...");
        
        var parallelJob = new BurstParallelJob
        {
            input = testData,
            output = new NativeArray<float>(arraySize, Allocator.TempJob),
            iterationCount = iterationCount
        };
        
        var handle = parallelJob.Schedule(arraySize, 64);
        handle.Complete();
        
        // 计算并行结果统计
        float parallelSum = 0f;
        for (int i = 0; i < arraySize; i++)
        {
            parallelSum += parallelJob.output[i];
        }
        
        Debug.Log($"并行计算完成，总和: {parallelSum:F2}");
        
        // 清理
        parallelJob.output.Dispose();
    }
    
    /// <summary>
    /// 获取Burst信息
    /// </summary>
    public void GetBurstInfo()
    {
        Debug.Log("=== Burst信息 ===");
        Debug.Log($"Burst启用: {BurstCompiler.IsEnabled}");
        Debug.Log($"Burst编译器版本: {BurstCompiler.Version}");
        Debug.Log($"数组大小: {arraySize}");
        Debug.Log($"迭代次数: {iterationCount}");
        Debug.Log($"Burst耗时: {burstTime:F2}ms");
        Debug.Log($"托管代码耗时: {managedTime:F2}ms");
        Debug.Log($"加速比: {speedup:F2}x");
        Debug.Log($"系统初始化: {isInitialized}");
        
        if (results != null)
        {
            Debug.Log($"计算结果数量: {results.Length}");
            Debug.Log($"计算结果总和: {sum:F2}");
            Debug.Log($"计算结果平均值: {average:F2}");
            Debug.Log($"计算结果最大值: {maxValue:F2}");
            Debug.Log($"计算结果最小值: {minValue:F2}");
        }
    }
    
    /// <summary>
    /// 重置Burst设置
    /// </summary>
    public void ResetBurstSettings()
    {
        // 重置性能数据
        burstTime = 0f;
        managedTime = 0f;
        speedup = 0f;
        
        // 重置结果
        results = null;
        sum = 0f;
        average = 0f;
        maxValue = 0f;
        minValue = 0f;
        
        Debug.Log("Burst设置已重置");
    }
    
    private void OnDestroy()
    {
        // 清理原生数组
        if (testData.IsCreated)
        {
            testData.Dispose();
        }
    }
}

/// <summary>
/// Burst数学计算Job
/// </summary>
[BurstCompile]
public struct BurstMathJob : IJob
{
    [ReadOnly] public NativeArray<float> input;
    [WriteOnly] public NativeArray<float> output;
    
    public void Execute()
    {
        for (int i = 0; i < input.Length; i++)
        {
            output[i] = BurstMath(input[i]);
        }
    }
    
    /// <summary>
    /// Burst优化的数学计算
    /// </summary>
    /// <param name="value">输入值</param>
    /// <returns>计算结果</returns>
    private float BurstMath(float value)
    {
        // 使用Burst优化的数学函数
        float result = value;
        
        // 正弦波计算
        result += math.sin(value * 0.1f) * 10f;
        
        // 平方根计算
        result += math.sqrt(math.abs(value)) * 0.5f;
        
        // 指数计算
        result += math.exp(value * 0.01f) * 0.1f;
        
        // 对数计算
        if (value > 0)
        {
            result += math.log(value + 1f) * 2f;
        }
        
        // 三角函数
        result += math.cos(value * 0.05f) * 5f;
        result += math.tan(value * 0.02f) * 1f;
        
        // 幂运算
        result += math.pow(value, 0.5f) * 3f;
        
        return result;
    }
}

/// <summary>
/// Burst向量运算Job
/// </summary>
[BurstCompile]
public struct BurstVectorJob : IJob
{
    [ReadOnly] public NativeArray<float3> input;
    [WriteOnly] public NativeArray<float3> output;
    
    public void Execute()
    {
        for (int i = 0; i < input.Length; i++)
        {
            float3 vec = input[i];
            
            // 向量运算
            float3 result = vec;
            
            // 向量旋转
            result = math.mul(quaternion.Euler(0, 45, 0), result);
            
            // 向量缩放
            result *= 1.5f;
            
            // 向量归一化
            if (math.lengthsq(result) > 0)
            {
                result = math.normalize(result);
            }
            
            // 向量反射
            float3 normal = new float3(0, 1, 0);
            result = math.reflect(result, normal);
            
            output[i] = result;
        }
    }
}

/// <summary>
/// Burst矩阵运算Job
/// </summary>
[BurstCompile]
public struct BurstMatrixJob : IJob
{
    [ReadOnly] public NativeArray<float4x4> input;
    [WriteOnly] public NativeArray<float4x4> output;
    
    public void Execute()
    {
        for (int i = 0; i < input.Length; i++)
        {
            float4x4 matrix = input[i];
            
            // 矩阵运算
            float4x4 result = matrix;
            
            // 矩阵转置
            result = math.transpose(result);
            
            // 矩阵求逆
            if (math.determinant(result) != 0)
            {
                result = math.inverse(result);
            }
            
            // 矩阵乘法
            float4x4 rotation = float4x4.RotateY(math.radians(30f));
            result = math.mul(result, rotation);
            
            output[i] = result;
        }
    }
}

/// <summary>
/// Burst并行计算Job
/// </summary>
[BurstCompile]
public struct BurstParallelJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<float> input;
    [WriteOnly] public NativeArray<float> output;
    public int iterationCount;
    
    public void Execute(int index)
    {
        float value = input[index];
        
        // 多次迭代计算
        for (int iter = 0; iter < iterationCount; iter++)
        {
            value = BurstMath(value);
        }
        
        output[index] = value;
    }
    
    /// <summary>
    /// Burst优化的数学计算
    /// </summary>
    /// <param name="value">输入值</param>
    /// <returns>计算结果</returns>
    private float BurstMath(float value)
    {
        float result = value;
        
        // 复杂的数学运算
        result += math.sin(value * 0.1f) * 10f;
        result += math.sqrt(math.abs(value)) * 0.5f;
        result += math.exp(value * 0.01f) * 0.1f;
        
        if (value > 0)
        {
            result += math.log(value + 1f) * 2f;
        }
        
        result += math.cos(value * 0.05f) * 5f;
        result += math.tan(value * 0.02f) * 1f;
        result += math.pow(value, 0.5f) * 3f;
        
        return result;
    }
} 