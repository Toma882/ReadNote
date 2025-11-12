// UnityNamespaceExample.cs
// Unity 命名空间示例
// 展示 Burst、Collections、Jobs、Mathematics 等高性能API的使用

using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Profiling;

namespace UnityAPI.Unity
{
    /// <summary>
    /// Unity 命名空间示例
    /// 
    /// 涵盖内容：
    /// - Burst 编译器优化
    /// - Collections 高性能集合
    /// - Jobs 作业系统
    /// - Mathematics 数学库
    /// - Profiling 性能分析
    /// </summary>
    public class UnityNamespaceExample : MonoBehaviour
    {
        [Header("作业系统配置")]
        [SerializeField] private int arraySize = 10000;
        [SerializeField] private int batchSize = 32;
        [SerializeField] private bool enableBurstCompilation = true;
        
        [Header("性能分析")]
        [SerializeField] private bool enableProfiling = true;
        [SerializeField] private int profilingSamples = 1000;
        
        [Header("数学库测试")]
        [SerializeField] private int mathTestIterations = 1000000;
        
        // NativeArray 数据
        private NativeArray<float3> positions;
        private NativeArray<float3> velocities;
        private NativeArray<float> timers;
        private NativeArray<int> results;
        
        // 作业句柄
        private JobHandle currentJobHandle;
        
        // 性能分析标记
        private ProfilerMarker updateMarker;
        private ProfilerMarker mathMarker;
        private ProfilerMarker jobMarker;
        
        // 统计信息
        private PerformanceStats stats;
        
        void Start()
        {
            InitializeUnitySystems();
            RunPerformanceTests();
        }
        
        void Update()
        {
            if (enableProfiling)
            {
                using (updateMarker.Auto())
                {
                    UpdateUnitySystems();
                }
            }
            else
            {
                UpdateUnitySystems();
            }
        }
        
        void OnDestroy()
        {
            CleanupUnitySystems();
        }
        
        /// <summary>
        /// 初始化Unity系统
        /// </summary>
        private void InitializeUnitySystems()
        {
            Debug.Log("初始化Unity高性能系统");
            
            // 初始化性能分析标记
            updateMarker = new ProfilerMarker("UnityUpdate");
            mathMarker = new ProfilerMarker("Mathematics");
            jobMarker = new ProfilerMarker("JobSystem");
            
            // 初始化NativeArray
            positions = new NativeArray<float3>(arraySize, Allocator.Persistent);
            velocities = new NativeArray<float3>(arraySize, Allocator.Persistent);
            timers = new NativeArray<float>(arraySize, Allocator.Persistent);
            results = new NativeArray<int>(arraySize, Allocator.Persistent);
            
            // 初始化随机数据
            InitializeRandomData();
            
            // 初始化统计信息
            stats = new PerformanceStats();
            
            Debug.Log($"初始化完成 - 数组大小: {arraySize}");
        }
        
        /// <summary>
        /// 初始化随机数据
        /// </summary>
        private void InitializeRandomData()
        {
            var random = new Unity.Mathematics.Random(12345);
            
            for (int i = 0; i < arraySize; i++)
            {
                positions[i] = random.NextFloat3(-10f, 10f);
                velocities[i] = random.NextFloat3(-1f, 1f);
                timers[i] = random.NextFloat(0f, 10f);
                results[i] = 0;
            }
        }
        
        /// <summary>
        /// 更新Unity系统
        /// </summary>
        private void UpdateUnitySystems()
        {
            // 执行作业系统
            ExecuteJobSystem();
            
            // 执行数学库测试
            if (Time.frameCount % 60 == 0) // 每秒执行一次
            {
                ExecuteMathematicsTests();
            }
            
            // 更新统计信息
            UpdateStatistics();
        }
        
        /// <summary>
        /// 执行作业系统
        /// </summary>
        private void ExecuteJobSystem()
        {
            if (enableProfiling)
            {
                using (jobMarker.Auto())
                {
                    ExecuteJobSystemInternal();
                }
            }
            else
            {
                ExecuteJobSystemInternal();
            }
        }
        
        private void ExecuteJobSystemInternal()
        {
            // 等待上一个作业完成
            currentJobHandle.Complete();
            
            // 创建新的作业
            var updateJob = new PositionUpdateJob
            {
                positions = positions,
                velocities = velocities,
                timers = timers,
                deltaTime = Time.deltaTime
            };
            
            var calculationJob = new CalculationJob
            {
                positions = positions,
                results = results
            };
            
            // 调度作业
            currentJobHandle = updateJob.Schedule(arraySize, batchSize);
            currentJobHandle = calculationJob.Schedule(arraySize, batchSize, currentJobHandle);
        }
        
        /// <summary>
        /// 执行数学库测试
        /// </summary>
        private void ExecuteMathematicsTests()
        {
            if (enableProfiling)
            {
                using (mathMarker.Auto())
                {
                    ExecuteMathematicsTestsInternal();
                }
            }
            else
            {
                ExecuteMathematicsTestsInternal();
            }
        }
        
        private void ExecuteMathematicsTestsInternal()
        {
            // 测试Unity.Mathematics性能
            var random = new Unity.Mathematics.Random((uint)Time.frameCount);
            float3 sum = float3.zero;
            
            for (int i = 0; i < mathTestIterations; i++)
            {
                float3 a = random.NextFloat3(-1f, 1f);
                float3 b = random.NextFloat3(-1f, 1f);
                
                // 使用Unity.Mathematics进行向量运算
                sum += math.cross(a, b);
                sum += math.normalize(a);
                sum += math.lerp(a, b, 0.5f);
            }
            
            stats.mathOperationsPerformed += mathTestIterations * 3;
            stats.lastMathResult = sum;
        }
        
        /// <summary>
        /// 更新统计信息
        /// </summary>
        private void UpdateStatistics()
        {
            stats.frameCount++;
            stats.averageFrameTime = Time.deltaTime;
            stats.totalMemoryUsage = GC.GetTotalMemory(false);
            
            // 计算作业系统性能
            if (currentJobHandle.IsCompleted)
            {
                stats.jobsCompleted++;
                currentJobHandle.Complete();
            }
        }
        
        /// <summary>
        /// 运行性能测试
        /// </summary>
        private void RunPerformanceTests()
        {
            Debug.Log("开始性能测试");
            
            // 测试NativeArray性能
            TestNativeArrayPerformance();
            
            // 测试作业系统性能
            TestJobSystemPerformance();
            
            // 测试数学库性能
            TestMathematicsPerformance();
            
            Debug.Log("性能测试完成");
        }
        
        /// <summary>
        /// 测试NativeArray性能
        /// </summary>
        private void TestNativeArrayPerformance()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            // 执行大量数组操作
            for (int i = 0; i < arraySize; i++)
            {
                positions[i] += velocities[i] * Time.deltaTime;
            }
            
            stopwatch.Stop();
            Debug.Log($"NativeArray操作耗时: {stopwatch.ElapsedMilliseconds}ms");
        }
        
        /// <summary>
        /// 测试作业系统性能
        /// </summary>
        private void TestJobSystemPerformance()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            var testJob = new PositionUpdateJob
            {
                positions = positions,
                velocities = velocities,
                timers = timers,
                deltaTime = 0.016f
            };
            
            var handle = testJob.Schedule(arraySize, batchSize);
            handle.Complete();
            
            stopwatch.Stop();
            Debug.Log($"作业系统操作耗时: {stopwatch.ElapsedMilliseconds}ms");
        }
        
        /// <summary>
        /// 测试数学库性能
        /// </summary>
        private void TestMathematicsPerformance()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            var random = new Unity.Mathematics.Random(12345);
            float3 result = float3.zero;
            
            for (int i = 0; i < 100000; i++)
            {
                float3 a = random.NextFloat3(-1f, 1f);
                float3 b = random.NextFloat3(-1f, 1f);
                
                result += math.cross(a, b);
                result += math.normalize(a);
                result += math.lerp(a, b, 0.5f);
            }
            
            stopwatch.Stop();
            Debug.Log($"数学库操作耗时: {stopwatch.ElapsedMilliseconds}ms");
        }
        
        /// <summary>
        /// 清理Unity系统
        /// </summary>
        private void CleanupUnitySystems()
        {
            Debug.Log("清理Unity高性能系统");
            
            // 等待作业完成
            if (currentJobHandle.IsValid)
            {
                currentJobHandle.Complete();
            }
            
            // 释放NativeArray
            if (positions.IsCreated) positions.Dispose();
            if (velocities.IsCreated) velocities.Dispose();
            if (timers.IsCreated) timers.Dispose();
            if (results.IsCreated) results.Dispose();
            
            Debug.Log("系统清理完成");
        }
        
        /// <summary>
        /// 获取性能统计
        /// </summary>
        public PerformanceStats GetPerformanceStats()
        {
            return stats;
        }
        
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 400, 300));
            
            GUILayout.Label("Unity 高性能系统示例", EditorStyles.boldLabel);
            GUILayout.Label($"数组大小: {arraySize}");
            GUILayout.Label($"批次大小: {batchSize}");
            GUILayout.Label($"帧数: {stats.frameCount}");
            GUILayout.Label($"平均帧时间: {stats.averageFrameTime:F4}s");
            GUILayout.Label($"内存使用: {stats.totalMemoryUsage / (1024 * 1024)}MB");
            GUILayout.Label($"作业完成数: {stats.jobsCompleted}");
            GUILayout.Label($"数学操作数: {stats.mathOperationsPerformed}");
            
            if (GUILayout.Button("运行性能测试"))
            {
                RunPerformanceTests();
            }
            
            if (GUILayout.Button("重新初始化数据"))
            {
                InitializeRandomData();
            }
            
            if (GUILayout.Button("切换Burst编译"))
            {
                enableBurstCompilation = !enableBurstCompilation;
                Debug.Log($"Burst编译: {enableBurstCompilation}");
            }
            
            if (GUILayout.Button("切换性能分析"))
            {
                enableProfiling = !enableProfiling;
                Debug.Log($"性能分析: {enableProfiling}");
            }
            
            GUILayout.EndArea();
        }
    }
    
    /// <summary>
    /// 位置更新作业（Burst编译）
    /// </summary>
    [BurstCompile]
    public struct PositionUpdateJob : IJobParallelFor
    {
        public NativeArray<float3> positions;
        public NativeArray<float3> velocities;
        public NativeArray<float> timers;
        public float deltaTime;
        
        public void Execute(int index)
        {
            // 更新位置
            positions[index] += velocities[index] * deltaTime;
            
            // 更新计时器
            timers[index] += deltaTime;
            
            // 边界检查
            if (math.length(positions[index]) > 20f)
            {
                velocities[index] = -velocities[index];
            }
        }
    }
    
    /// <summary>
    /// 计算作业（Burst编译）
    /// </summary>
    [BurstCompile]
    public struct CalculationJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<float3> positions;
        public NativeArray<int> results;
        
        public void Execute(int index)
        {
            // 计算距离原点的距离
            float distance = math.length(positions[index]);
            results[index] = (int)(distance * 100);
        }
    }
    
    /// <summary>
    /// 性能统计信息
    /// </summary>
    [System.Serializable]
    public class PerformanceStats
    {
        public int frameCount;
        public float averageFrameTime;
        public long totalMemoryUsage;
        public int jobsCompleted;
        public long mathOperationsPerformed;
        public float3 lastMathResult;
        
        public PerformanceStats()
        {
            frameCount = 0;
            averageFrameTime = 0f;
            totalMemoryUsage = 0;
            jobsCompleted = 0;
            mathOperationsPerformed = 0;
            lastMathResult = float3.zero;
        }
    }
    
    /// <summary>
    /// Collections 示例
    /// </summary>
    public class CollectionsExample : MonoBehaviour
    {
        private NativeList<int> nativeList;
        private NativeHashMap<int, float3> nativeHashMap;
        private NativeQueue<float> nativeQueue;
        
        void Start()
        {
            InitializeCollections();
            DemonstrateCollections();
        }
        
        void OnDestroy()
        {
            CleanupCollections();
        }
        
        private void InitializeCollections()
        {
            // 初始化NativeList
            nativeList = new NativeList<int>(100, Allocator.Persistent);
            
            // 初始化NativeHashMap
            nativeHashMap = new NativeHashMap<int, float3>(50, Allocator.Persistent);
            
            // 初始化NativeQueue
            nativeQueue = new NativeQueue<float>(Allocator.Persistent);
        }
        
        private void DemonstrateCollections()
        {
            // NativeList 操作
            for (int i = 0; i < 10; i++)
            {
                nativeList.Add(i * i);
            }
            
            Debug.Log($"NativeList 大小: {nativeList.Length}");
            
            // NativeHashMap 操作
            for (int i = 0; i < 5; i++)
            {
                nativeHashMap[i] = new float3(i, i * 2, i * 3);
            }
            
            Debug.Log($"NativeHashMap 大小: {nativeHashMap.Count}");
            
            // NativeQueue 操作
            for (int i = 0; i < 5; i++)
            {
                nativeQueue.Enqueue(i * 0.1f);
            }
            
            Debug.Log($"NativeQueue 大小: {nativeQueue.Count}");
        }
        
        private void CleanupCollections()
        {
            if (nativeList.IsCreated) nativeList.Dispose();
            if (nativeHashMap.IsCreated) nativeHashMap.Dispose();
            if (nativeQueue.IsCreated) nativeQueue.Dispose();
        }
    }
}
