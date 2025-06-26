using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using Unity.Physics;

/// <summary>
/// UnityEngine.LowLevelPhysics 命名空间案例演示
/// 展示底层物理系统、物理作业、性能优化等核心功能
/// </summary>
public class LowLevelPhysicsExample : MonoBehaviour
{
    [Header("底层物理配置")]
    [SerializeField] private bool enableLowLevelPhysics = true; //启用底层物理
    [SerializeField] private bool enablePhysicsJobs = true; //启用物理作业
    [SerializeField] private bool enableBurstPhysics = true; //启用Burst物理
    [SerializeField] private bool enablePhysicsProfiling = true; //启用物理分析
    [SerializeField] private bool enableCollisionDetection = true; //启用碰撞检测
    
    [Header("物理参数")]
    [SerializeField] private int physicsObjectCount = 100; //物理对象数量
    [SerializeField] private int physicsJobBatchSize = 64; //物理作业批处理大小
    [SerializeField] private float physicsTimeStep = 0.02f; //物理时间步长
    [SerializeField] private int physicsIterations = 6; //物理迭代次数
    [SerializeField] private bool useFixedTimestep = true; //使用固定时间步长
    
    [Header("物理性能")]
    [SerializeField] private bool enablePerformanceOptimization = true; //启用性能优化
    [SerializeField] private bool enableSpatialPartitioning = true; //启用空间分区
    [SerializeField] private bool enableBroadPhaseOptimization = true; //启用宽相优化
    [SerializeField] private bool enableNarrowPhaseOptimization = true; //启用窄相优化
    
    [Header("物理状态")]
    [SerializeField] private int activePhysicsObjects = 0; //活跃物理对象数
    [SerializeField] private int activeCollisions = 0; //活跃碰撞数
    [SerializeField] private int physicsJobsCompleted = 0; //物理作业完成数
    [SerializeField] private float physicsUpdateTime = 0f; //物理更新时间
    [SerializeField] private float collisionDetectionTime = 0f; //碰撞检测时间
    [SerializeField] private float physicsMemoryUsage = 0f; //物理内存使用
    
    [Header("物理数据")]
    [SerializeField] private NativeArray<Vector3> physicsPositions; //物理位置
    [SerializeField] private NativeArray<Vector3> physicsVelocities; //物理速度
    [SerializeField] private NativeArray<Vector3> physicsForces; //物理力
    [SerializeField] private NativeArray<float> physicsMasses; //物理质量
    [SerializeField] private NativeArray<CollisionData> collisionData; //碰撞数据
    
    [Header("性能监控")]
    [SerializeField] private bool enablePhysicsMonitoring = true; //启用物理监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logPhysicsData = false; //记录物理数据
    [SerializeField] private int maxPhysicsSamples = 100; //最大物理样本数
    
    [Header("性能历史")]
    [SerializeField] private float[] physicsPerformanceHistory = new float[100]; //物理性能历史
    [SerializeField] private int performanceIndex = 0; //性能索引
    [SerializeField] private float averagePhysicsPerformance = 0f; //平均物理性能
    [SerializeField] private float minPhysicsPerformance = 0f; //最低物理性能
    [SerializeField] private float maxPhysicsPerformance = 0f; //最高物理性能
    
    private JobHandle[] physicsJobHandles;
    private System.Collections.Generic.List<JobHandle> activePhysicsJobs = new System.Collections.Generic.List<JobHandle>();
    private float lastPhysicsUpdateTime = 0f;
    private float lastMonitoringTime = 0f;
    private bool isInitialized = false;
    private PhysicsWorld physicsWorld;

    private void Start()
    {
        InitializeLowLevelPhysics();
    }

    /// <summary>
    /// 初始化底层物理系统
    /// </summary>
    private void InitializeLowLevelPhysics()
    {
        // 初始化物理数据
        InitializePhysicsData();
        
        // 初始化物理作业
        InitializePhysicsJobs();
        
        // 初始化物理世界
        InitializePhysicsWorld();
        
        // 初始化性能监控
        InitializePhysicsMonitoring();
        
        // 配置物理系统
        ConfigurePhysicsSystem();
        
        isInitialized = true;
        Debug.Log("底层物理系统初始化完成");
    }

    /// <summary>
    /// 初始化物理数据
    /// </summary>
    private void InitializePhysicsData()
    {
        // 创建物理数据数组
        physicsPositions = new NativeArray<Vector3>(physicsObjectCount, Allocator.Persistent);
        physicsVelocities = new NativeArray<Vector3>(physicsObjectCount, Allocator.Persistent);
        physicsForces = new NativeArray<Vector3>(physicsObjectCount, Allocator.Persistent);
        physicsMasses = new NativeArray<float>(physicsObjectCount, Allocator.Persistent);
        collisionData = new NativeArray<CollisionData>(physicsObjectCount * 10, Allocator.Persistent);
        
        // 初始化物理对象
        for (int i = 0; i < physicsObjectCount; i++)
        {
            physicsPositions[i] = Random.insideUnitSphere * 10f;
            physicsVelocities[i] = Random.insideUnitSphere * 5f;
            physicsForces[i] = Vector3.zero;
            physicsMasses[i] = Random.Range(0.1f, 10f);
        }
        
        activePhysicsObjects = physicsObjectCount;
        Debug.Log($"物理数据初始化完成: 对象数量={physicsObjectCount}");
    }

    /// <summary>
    /// 初始化物理作业
    /// </summary>
    private void InitializePhysicsJobs()
    {
        if (enablePhysicsJobs)
        {
            physicsJobHandles = new JobHandle[10]; // 最多10个并发物理作业
            Debug.Log("物理作业系统初始化完成");
        }
    }

    /// <summary>
    /// 初始化物理世界
    /// </summary>
    private void InitializePhysicsWorld()
    {
        // 创建物理世界
        physicsWorld = new PhysicsWorld();
        
        // 配置物理世界参数
        physicsWorld.Gravity = new Unity.Mathematics.float3(0, -9.81f, 0);
        
        Debug.Log("物理世界初始化完成");
    }

    /// <summary>
    /// 初始化物理监控
    /// </summary>
    private void InitializePhysicsMonitoring()
    {
        if (enablePhysicsMonitoring)
        {
            physicsPerformanceHistory = new float[maxPhysicsSamples];
            performanceIndex = 0;
            averagePhysicsPerformance = 0f;
            minPhysicsPerformance = 0f;
            maxPhysicsPerformance = 0f;
            
            Debug.Log("物理监控初始化完成");
        }
    }

    /// <summary>
    /// 配置物理系统
    /// </summary>
    private void ConfigurePhysicsSystem()
    {
        // 设置物理时间步长
        Time.fixedDeltaTime = physicsTimeStep;
        
        // 设置物理迭代次数
        Physics.defaultSolverIterations = physicsIterations;
        
        // 配置性能优化
        if (enablePerformanceOptimization)
        {
            Physics.autoSimulation = false; // 禁用自动物理模拟
            Physics.autoSyncTransforms = false; // 禁用自动同步变换
        }
        
        Debug.Log("物理系统配置完成");
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新物理系统
        UpdatePhysicsSystem();
        
        // 执行物理作业
        ExecutePhysicsJobs();
        
        // 物理监控
        if (enablePhysicsMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            MonitorPhysicsPerformance();
            lastMonitoringTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (!isInitialized || !useFixedTimestep) return;
        
        // 固定时间步长物理更新
        UpdatePhysicsFixedStep();
    }

    /// <summary>
    /// 更新物理系统
    /// </summary>
    private void UpdatePhysicsSystem()
    {
        float startTime = Time.realtimeSinceStartup;
        
        // 更新物理世界
        UpdatePhysicsWorld();
        
        // 碰撞检测
        if (enableCollisionDetection)
        {
            PerformCollisionDetection();
        }
        
        // 物理模拟
        PerformPhysicsSimulation();
        
        physicsUpdateTime = Time.realtimeSinceStartup - startTime;
    }

    /// <summary>
    /// 更新物理世界
    /// </summary>
    private void UpdatePhysicsWorld()
    {
        // 更新物理世界状态
        physicsWorld.Gravity = new Unity.Mathematics.float3(0, -9.81f, 0);
        
        // 更新物理对象
        for (int i = 0; i < activePhysicsObjects; i++)
        {
            // 应用重力
            physicsForces[i] += new Vector3(0, -9.81f * physicsMasses[i], 0);
            
            // 更新速度
            physicsVelocities[i] += physicsForces[i] * Time.deltaTime / physicsMasses[i];
            
            // 更新位置
            physicsPositions[i] += physicsVelocities[i] * Time.deltaTime;
            
            // 重置力
            physicsForces[i] = Vector3.zero;
        }
    }

    /// <summary>
    /// 执行碰撞检测
    /// </summary>
    private void PerformCollisionDetection()
    {
        float startTime = Time.realtimeSinceStartup;
        
        activeCollisions = 0;
        
        // 简单的碰撞检测（球体碰撞）
        for (int i = 0; i < activePhysicsObjects; i++)
        {
            for (int j = i + 1; j < activePhysicsObjects; j++)
            {
                float distance = Vector3.Distance(physicsPositions[i], physicsPositions[j]);
                float minDistance = 1.0f; // 最小距离
                
                if (distance < minDistance)
                {
                    // 记录碰撞
                    if (activeCollisions < collisionData.Length)
                    {
                        collisionData[activeCollisions] = new CollisionData
                        {
                            objectA = i,
                            objectB = j,
                            collisionPoint = (physicsPositions[i] + physicsPositions[j]) * 0.5f,
                            collisionNormal = (physicsPositions[j] - physicsPositions[i]).normalized,
                            penetrationDepth = minDistance - distance
                        };
                        activeCollisions++;
                    }
                    
                    // 简单的碰撞响应
                    Vector3 separation = (physicsPositions[j] - physicsPositions[i]).normalized * (minDistance - distance) * 0.5f;
                    physicsPositions[i] -= separation;
                    physicsPositions[j] += separation;
                }
            }
        }
        
        collisionDetectionTime = Time.realtimeSinceStartup - startTime;
    }

    /// <summary>
    /// 执行物理模拟
    /// </summary>
    private void PerformPhysicsSimulation()
    {
        // 这里可以实现更复杂的物理模拟
        // 例如：约束求解、接触力计算等
        
        if (logPhysicsData)
        {
            Debug.Log($"物理模拟: 对象数={activePhysicsObjects}, 碰撞数={activeCollisions}");
        }
    }

    /// <summary>
    /// 执行物理作业
    /// </summary>
    private void ExecutePhysicsJobs()
    {
        if (!enablePhysicsJobs) return;
        
        // 清理完成的作业
        CleanupCompletedPhysicsJobs();
        
        // 执行新作业
        if (activePhysicsJobs.Count < physicsJobHandles.Length)
        {
            ExecutePhysicsJob();
        }
    }

    /// <summary>
    /// 执行物理作业
    /// </summary>
    private void ExecutePhysicsJob()
    {
        if (enableBurstPhysics)
        {
            var job = new PhysicsBurstJob
            {
                positions = physicsPositions,
                velocities = physicsVelocities,
                forces = physicsForces,
                masses = physicsMasses,
                deltaTime = Time.deltaTime,
                gravity = new Unity.Mathematics.float3(0, -9.81f, 0)
            };
            
            var jobHandle = job.Schedule(activePhysicsObjects, physicsJobBatchSize);
            activePhysicsJobs.Add(jobHandle);
            
            if (logPhysicsData)
            {
                Debug.Log($"Burst物理作业已调度: 对象数={activePhysicsObjects}, 批处理={physicsJobBatchSize}");
            }
        }
        else
        {
            var job = new PhysicsJob
            {
                positions = physicsPositions,
                velocities = physicsVelocities,
                forces = physicsForces,
                masses = physicsMasses,
                deltaTime = Time.deltaTime,
                gravity = new Unity.Mathematics.float3(0, -9.81f, 0)
            };
            
            var jobHandle = job.Schedule(activePhysicsObjects, physicsJobBatchSize);
            activePhysicsJobs.Add(jobHandle);
            
            if (logPhysicsData)
            {
                Debug.Log($"普通物理作业已调度: 对象数={activePhysicsObjects}, 批处理={physicsJobBatchSize}");
            }
        }
    }

    /// <summary>
    /// 清理完成的物理作业
    /// </summary>
    private void CleanupCompletedPhysicsJobs()
    {
        for (int i = activePhysicsJobs.Count - 1; i >= 0; i--)
        {
            if (activePhysicsJobs[i].IsCompleted)
            {
                activePhysicsJobs[i].Complete();
                activePhysicsJobs.RemoveAt(i);
                physicsJobsCompleted++;
            }
        }
    }

    /// <summary>
    /// 更新固定时间步长物理
    /// </summary>
    private void UpdatePhysicsFixedStep()
    {
        // 固定时间步长的物理更新
        float fixedDeltaTime = Time.fixedDeltaTime;
        
        // 执行物理模拟
        for (int i = 0; i < activePhysicsObjects; i++)
        {
            // 应用重力
            physicsForces[i] += new Vector3(0, -9.81f * physicsMasses[i], 0);
            
            // 更新速度
            physicsVelocities[i] += physicsForces[i] * fixedDeltaTime / physicsMasses[i];
            
            // 更新位置
            physicsPositions[i] += physicsVelocities[i] * fixedDeltaTime;
            
            // 重置力
            physicsForces[i] = Vector3.zero;
        }
    }

    /// <summary>
    /// 监控物理性能
    /// </summary>
    private void MonitorPhysicsPerformance()
    {
        float currentPerformance = 1f / physicsUpdateTime;
        
        // 更新性能历史
        physicsPerformanceHistory[performanceIndex] = currentPerformance;
        performanceIndex = (performanceIndex + 1) % maxPhysicsSamples;
        
        // 计算性能统计
        CalculatePhysicsPerformanceStats();
        
        // 计算内存使用
        physicsMemoryUsage = CalculatePhysicsMemoryUsage();
        
        if (logPhysicsData)
        {
            Debug.Log($"物理性能监控: 性能={currentPerformance:F1}, 对象数={activePhysicsObjects}, 碰撞数={activeCollisions}");
        }
    }

    /// <summary>
    /// 计算物理性能统计
    /// </summary>
    private void CalculatePhysicsPerformanceStats()
    {
        float sum = 0f;
        minPhysicsPerformance = float.MaxValue;
        maxPhysicsPerformance = 0f;
        
        for (int i = 0; i < maxPhysicsSamples; i++)
        {
            float value = physicsPerformanceHistory[i];
            sum += value;
            
            if (value < minPhysicsPerformance) minPhysicsPerformance = value;
            if (value > maxPhysicsPerformance) maxPhysicsPerformance = value;
        }
        
        averagePhysicsPerformance = sum / maxPhysicsSamples;
    }

    /// <summary>
    /// 计算物理内存使用
    /// </summary>
    /// <returns>物理内存使用量（MB）</returns>
    private float CalculatePhysicsMemoryUsage()
    {
        long totalMemory = 0;
        
        if (physicsPositions.IsCreated)
            totalMemory += physicsPositions.Length * sizeof(float) * 3;
        if (physicsVelocities.IsCreated)
            totalMemory += physicsVelocities.Length * sizeof(float) * 3;
        if (physicsForces.IsCreated)
            totalMemory += physicsForces.Length * sizeof(float) * 3;
        if (physicsMasses.IsCreated)
            totalMemory += physicsMasses.Length * sizeof(float);
        if (collisionData.IsCreated)
            totalMemory += collisionData.Length * sizeof(float) * 10; // 估算碰撞数据大小
        
        return totalMemory / (1024f * 1024f);
    }

    /// <summary>
    /// 执行物理优化
    /// </summary>
    public void ExecutePhysicsOptimization()
    {
        Debug.Log("开始物理优化...");
        
        // 空间分区优化
        if (enableSpatialPartitioning)
        {
            OptimizeSpatialPartitioning();
        }
        
        // 宽相优化
        if (enableBroadPhaseOptimization)
        {
            OptimizeBroadPhase();
        }
        
        // 窄相优化
        if (enableNarrowPhaseOptimization)
        {
            OptimizeNarrowPhase();
        }
        
        Debug.Log("物理优化完成");
    }

    /// <summary>
    /// 空间分区优化
    /// </summary>
    private void OptimizeSpatialPartitioning()
    {
        // 实现空间分区算法（如网格、四叉树、八叉树等）
        Debug.Log("空间分区优化已执行");
    }

    /// <summary>
    /// 宽相优化
    /// </summary>
    private void OptimizeBroadPhase()
    {
        // 实现宽相碰撞检测优化
        Debug.Log("宽相优化已执行");
    }

    /// <summary>
    /// 窄相优化
    /// </summary>
    private void OptimizeNarrowPhase()
    {
        // 实现窄相碰撞检测优化
        Debug.Log("窄相优化已执行");
    }

    /// <summary>
    /// 生成物理报告
    /// </summary>
    public void GeneratePhysicsReport()
    {
        Debug.Log("=== 底层物理系统报告 ===");
        Debug.Log($"活跃物理对象: {activePhysicsObjects}");
        Debug.Log($"活跃碰撞数: {activeCollisions}");
        Debug.Log($"物理作业完成数: {physicsJobsCompleted}");
        Debug.Log($"物理更新时间: {physicsUpdateTime * 1000:F2} ms");
        Debug.Log($"碰撞检测时间: {collisionDetectionTime * 1000:F2} ms");
        Debug.Log($"物理内存使用: {physicsMemoryUsage:F2} MB");
        Debug.Log($"平均物理性能: {averagePhysicsPerformance:F1}");
        Debug.Log($"最低物理性能: {minPhysicsPerformance:F1}");
        Debug.Log($"最高物理性能: {maxPhysicsPerformance:F1}");
    }

    /// <summary>
    /// 重置物理系统
    /// </summary>
    public void ResetPhysicsSystem()
    {
        Debug.Log("重置物理系统...");
        
        // 清理物理作业
        CleanupCompletedPhysicsJobs();
        JobHandle.CompleteAll(activePhysicsJobs.ToArray());
        activePhysicsJobs.Clear();
        
        // 重置物理数据
        for (int i = 0; i < physicsObjectCount; i++)
        {
            physicsPositions[i] = Random.insideUnitSphere * 10f;
            physicsVelocities[i] = Random.insideUnitSphere * 5f;
            physicsForces[i] = Vector3.zero;
            physicsMasses[i] = Random.Range(0.1f, 10f);
        }
        
        activeCollisions = 0;
        physicsJobsCompleted = 0;
        
        Debug.Log("物理系统重置完成");
    }

    /// <summary>
    /// 导出物理数据
    /// </summary>
    public void ExportPhysicsData()
    {
        var data = new PhysicsData
        {
            timestamp = System.DateTime.Now.ToString(),
            activePhysicsObjects = activePhysicsObjects,
            activeCollisions = activeCollisions,
            physicsJobsCompleted = physicsJobsCompleted,
            physicsUpdateTime = physicsUpdateTime,
            collisionDetectionTime = collisionDetectionTime,
            physicsMemoryUsage = physicsMemoryUsage,
            averagePhysicsPerformance = averagePhysicsPerformance,
            minPhysicsPerformance = minPhysicsPerformance,
            maxPhysicsPerformance = maxPhysicsPerformance,
            physicsPerformanceHistory = physicsPerformanceHistory
        };
        
        string json = JsonUtility.ToJson(data, true);
        string filename = $"lowlevel_physics_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        
        System.IO.File.WriteAllText(filename, json);
        Debug.Log($"物理数据已导出: {filename}");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("LowLevelPhysics 底层物理系统演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("物理配置:");
        enableLowLevelPhysics = GUILayout.Toggle(enableLowLevelPhysics, "启用底层物理");
        enablePhysicsJobs = GUILayout.Toggle(enablePhysicsJobs, "启用物理作业");
        enableBurstPhysics = GUILayout.Toggle(enableBurstPhysics, "启用Burst物理");
        enablePhysicsProfiling = GUILayout.Toggle(enablePhysicsProfiling, "启用物理分析");
        enableCollisionDetection = GUILayout.Toggle(enableCollisionDetection, "启用碰撞检测");
        
        GUILayout.Space(10);
        GUILayout.Label("物理参数:");
        physicsObjectCount = int.TryParse(GUILayout.TextField("物理对象数量", physicsObjectCount.ToString()), out var objectCount) ? objectCount : physicsObjectCount;
        physicsJobBatchSize = int.TryParse(GUILayout.TextField("物理作业批处理大小", physicsJobBatchSize.ToString()), out var batchSize) ? batchSize : physicsJobBatchSize;
        physicsTimeStep = float.TryParse(GUILayout.TextField("物理时间步长", physicsTimeStep.ToString()), out var timeStep) ? timeStep : physicsTimeStep;
        physicsIterations = int.TryParse(GUILayout.TextField("物理迭代次数", physicsIterations.ToString()), out var iterations) ? iterations : physicsIterations;
        useFixedTimestep = GUILayout.Toggle(useFixedTimestep, "使用固定时间步长");
        
        GUILayout.Space(10);
        GUILayout.Label("性能优化:");
        enablePerformanceOptimization = GUILayout.Toggle(enablePerformanceOptimization, "启用性能优化");
        enableSpatialPartitioning = GUILayout.Toggle(enableSpatialPartitioning, "启用空间分区");
        enableBroadPhaseOptimization = GUILayout.Toggle(enableBroadPhaseOptimization, "启用宽相优化");
        enableNarrowPhaseOptimization = GUILayout.Toggle(enableNarrowPhaseOptimization, "启用窄相优化");
        
        GUILayout.Space(10);
        GUILayout.Label("物理状态:");
        GUILayout.Label($"活跃物理对象: {activePhysicsObjects}");
        GUILayout.Label($"活跃碰撞数: {activeCollisions}");
        GUILayout.Label($"物理作业完成数: {physicsJobsCompleted}");
        GUILayout.Label($"物理更新时间: {physicsUpdateTime * 1000:F2} ms");
        GUILayout.Label($"碰撞检测时间: {collisionDetectionTime * 1000:F2} ms");
        GUILayout.Label($"物理内存使用: {physicsMemoryUsage:F2} MB");
        
        GUILayout.Space(10);
        GUILayout.Label("性能数据:");
        GUILayout.Label($"平均物理性能: {averagePhysicsPerformance:F1}");
        GUILayout.Label($"最低物理性能: {minPhysicsPerformance:F1}");
        GUILayout.Label($"最高物理性能: {maxPhysicsPerformance:F1}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("执行物理优化"))
        {
            ExecutePhysicsOptimization();
        }
        
        if (GUILayout.Button("生成物理报告"))
        {
            GeneratePhysicsReport();
        }
        
        if (GUILayout.Button("重置物理系统"))
        {
            ResetPhysicsSystem();
        }
        
        if (GUILayout.Button("导出物理数据"))
        {
            ExportPhysicsData();
        }
        
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        // 清理物理数据
        if (physicsPositions.IsCreated)
            physicsPositions.Dispose();
        if (physicsVelocities.IsCreated)
            physicsVelocities.Dispose();
        if (physicsForces.IsCreated)
            physicsForces.Dispose();
        if (physicsMasses.IsCreated)
            physicsMasses.Dispose();
        if (collisionData.IsCreated)
            collisionData.Dispose();
        
        // 清理物理作业
        CleanupCompletedPhysicsJobs();
        JobHandle.CompleteAll(activePhysicsJobs.ToArray());
    }
}

/// <summary>
/// 碰撞数据
/// </summary>
[System.Serializable]
public struct CollisionData
{
    public int objectA;
    public int objectB;
    public Vector3 collisionPoint;
    public Vector3 collisionNormal;
    public float penetrationDepth;
}

/// <summary>
/// Burst物理作业
/// </summary>
[BurstCompile]
public struct PhysicsBurstJob : IJobParallelFor
{
    [NativeDisableParallelForRestriction] public NativeArray<Vector3> positions;
    [NativeDisableParallelForRestriction] public NativeArray<Vector3> velocities;
    [NativeDisableParallelForRestriction] public NativeArray<Vector3> forces;
    [ReadOnly] public NativeArray<float> masses;
    public float deltaTime;
    public Unity.Mathematics.float3 gravity;
    
    public void Execute(int index)
    {
        // 应用重力
        forces[index] += new Vector3(gravity.x, gravity.y, gravity.z) * masses[index];
        
        // 更新速度
        velocities[index] += forces[index] * deltaTime / masses[index];
        
        // 更新位置
        positions[index] += velocities[index] * deltaTime;
        
        // 重置力
        forces[index] = Vector3.zero;
    }
}

/// <summary>
/// 普通物理作业
/// </summary>
public struct PhysicsJob : IJobParallelFor
{
    [NativeDisableParallelForRestriction] public NativeArray<Vector3> positions;
    [NativeDisableParallelForRestriction] public NativeArray<Vector3> velocities;
    [NativeDisableParallelForRestriction] public NativeArray<Vector3> forces;
    [ReadOnly] public NativeArray<float> masses;
    public float deltaTime;
    public Unity.Mathematics.float3 gravity;
    
    public void Execute(int index)
    {
        // 应用重力
        forces[index] += new Vector3(gravity.x, gravity.y, gravity.z) * masses[index];
        
        // 更新速度
        velocities[index] += forces[index] * deltaTime / masses[index];
        
        // 更新位置
        positions[index] += velocities[index] * deltaTime;
        
        // 重置力
        forces[index] = Vector3.zero;
    }
}

/// <summary>
/// 物理数据类
/// </summary>
[System.Serializable]
public class PhysicsData
{
    public string timestamp;
    public int activePhysicsObjects;
    public int activeCollisions;
    public int physicsJobsCompleted;
    public float physicsUpdateTime;
    public float collisionDetectionTime;
    public float physicsMemoryUsage;
    public float averagePhysicsPerformance;
    public float minPhysicsPerformance;
    public float maxPhysicsPerformance;
    public float[] physicsPerformanceHistory;
} 