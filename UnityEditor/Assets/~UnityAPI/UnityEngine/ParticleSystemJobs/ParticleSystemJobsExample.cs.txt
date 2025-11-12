using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;

/// <summary>
/// UnityEngine.ParticleSystemJobs 命名空间案例演示
/// 展示粒子系统作业、批量粒子处理、性能优化等核心功能
/// </summary>
public class ParticleSystemJobsExample : MonoBehaviour
{
    [Header("粒子系统配置")]
    [SerializeField] private bool enableParticleJobs = true; //启用粒子作业
    [SerializeField] private bool enableBurstCompilation = true; //启用Burst编译
    [SerializeField] private bool enableJobScheduling = true; //启用作业调度
    [SerializeField] private bool enablePerformanceOptimization = true; //启用性能优化
    [SerializeField] private bool enableParticleProfiling = true; //启用粒子分析
    
    [Header("粒子参数")]
    [SerializeField] private int particleCount = 10000; //粒子数量
    [SerializeField] private int jobBatchSize = 64; //作业批处理大小
    [SerializeField] private float particleLifetime = 5f; //粒子生命周期
    [SerializeField] private float particleSpeed = 10f; //粒子速度
    [SerializeField] private Vector3 particleGravity = new Vector3(0, -9.81f, 0); //粒子重力
    [SerializeField] private bool enableParticleCollision = true; //启用粒子碰撞
    
    [Header("粒子数据")]
    [SerializeField] private NativeArray<float3> particlePositions; //粒子位置
    [SerializeField] private NativeArray<float3> particleVelocities; //粒子速度
    [SerializeField] private NativeArray<float> particleLifetimes; //粒子生命周期
    [SerializeField] private NativeArray<float> particleSizes; //粒子大小
    [SerializeField] private NativeArray<float4> particleColors; //粒子颜色
    [SerializeField] private NativeArray<bool> particleAlive; //粒子存活状态
    
    [Header("性能监控")]
    [SerializeField] private bool enableParticleMonitoring = true; //启用粒子监控
    [SerializeField] private float monitoringInterval = 1f; //监控间隔
    [SerializeField] private bool logParticleData = false; //记录粒子数据
    [SerializeField] private int activeParticles = 0; //活跃粒子数
    [SerializeField] private float particleUpdateTime = 0f; //粒子更新时间
    [SerializeField] private int completedJobs = 0; //完成作业数
    
    [Header("粒子状态")]
    [SerializeField] private string particleSystemState = "未初始化"; //粒子系统状态
    [SerializeField] private float systemUptime = 0f; //系统运行时间
    [SerializeField] private float averageParticleLifetime = 0f; //平均粒子生命周期
    [SerializeField] private float particleEmissionRate = 0f; //粒子发射率
    [SerializeField] private float particleDeathRate = 0f; //粒子死亡率
    
    [Header("性能数据")]
    [SerializeField] private float[] performanceHistory = new float[100]; //性能历史
    [SerializeField] private int performanceIndex = 0; //性能索引
    [SerializeField] private float averagePerformance = 0f; //平均性能
    [SerializeField] private float minPerformance = 0f; //最低性能
    [SerializeField] private float maxPerformance = 0f; //最高性能
    
    private JobHandle[] particleJobHandles;
    private System.Collections.Generic.List<JobHandle> activeParticleJobs = new System.Collections.Generic.List<JobHandle>();
    private float lastMonitoringTime = 0f;
    private float systemStartTime = 0f;
    private bool isInitialized = false;
    private ParticleSystem particleSystem;

    private void Start()
    {
        InitializeParticleSystemJobs();
    }

    /// <summary>
    /// 初始化粒子系统作业
    /// </summary>
    private void InitializeParticleSystemJobs()
    {
        // 获取粒子系统组件
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem == null)
        {
            particleSystem = gameObject.AddComponent<ParticleSystem>();
        }
        
        // 初始化粒子数据
        InitializeParticleData();
        
        // 初始化粒子作业
        InitializeParticleJobs();
        
        // 初始化性能监控
        InitializePerformanceMonitoring();
        
        // 配置粒子系统
        ConfigureParticleSystem();
        
        isInitialized = true;
        systemStartTime = Time.time;
        particleSystemState = "已初始化";
        Debug.Log("粒子系统作业初始化完成");
    }

    /// <summary>
    /// 初始化粒子数据
    /// </summary>
    private void InitializeParticleData()
    {
        // 创建粒子数据数组
        particlePositions = new NativeArray<float3>(particleCount, Allocator.Persistent);
        particleVelocities = new NativeArray<float3>(particleCount, Allocator.Persistent);
        particleLifetimes = new NativeArray<float>(particleCount, Allocator.Persistent);
        particleSizes = new NativeArray<float>(particleCount, Allocator.Persistent);
        particleColors = new NativeArray<float4>(particleCount, Allocator.Persistent);
        particleAlive = new NativeArray<bool>(particleCount, Allocator.Persistent);
        
        // 初始化粒子
        for (int i = 0; i < particleCount; i++)
        {
            particlePositions[i] = new float3(
                Random.Range(-10f, 10f),
                Random.Range(0f, 20f),
                Random.Range(-10f, 10f)
            );
            
            particleVelocities[i] = new float3(
                Random.Range(-5f, 5f),
                Random.Range(-2f, 10f),
                Random.Range(-5f, 5f)
            );
            
            particleLifetimes[i] = Random.Range(0f, particleLifetime);
            particleSizes[i] = Random.Range(0.1f, 2f);
            particleColors[i] = new float4(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0.5f, 1f)
            );
            particleAlive[i] = true;
        }
        
        activeParticles = particleCount;
        Debug.Log($"粒子数据初始化完成: 粒子数量={particleCount}");
    }

    /// <summary>
    /// 初始化粒子作业
    /// </summary>
    private void InitializeParticleJobs()
    {
        if (enableParticleJobs)
        {
            particleJobHandles = new JobHandle[10]; // 最多10个并发粒子作业
            Debug.Log("粒子作业系统初始化完成");
        }
    }

    /// <summary>
    /// 初始化性能监控
    /// </summary>
    private void InitializePerformanceMonitoring()
    {
        if (enableParticleMonitoring)
        {
            performanceHistory = new float[100];
            performanceIndex = 0;
            averagePerformance = 0f;
            minPerformance = 0f;
            maxPerformance = 0f;
            
            Debug.Log("性能监控初始化完成");
        }
    }

    /// <summary>
    /// 配置粒子系统
    /// </summary>
    private void ConfigureParticleSystem()
    {
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.maxParticles = particleCount;
            main.startLifetime = particleLifetime;
            main.startSpeed = particleSpeed;
            main.gravityModifier = 1f;
            
            var emission = particleSystem.emission;
            emission.rateOverTime = particleCount / particleLifetime;
            
            Debug.Log("粒子系统配置完成");
        }
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        // 更新系统运行时间
        systemUptime = Time.time - systemStartTime;
        
        // 更新粒子系统
        UpdateParticleSystem();
        
        // 执行粒子作业
        ExecuteParticleJobs();
        
        // 粒子监控
        if (enableParticleMonitoring && Time.time - lastMonitoringTime > monitoringInterval)
        {
            MonitorParticlePerformance();
            lastMonitoringTime = Time.time;
        }
    }

    /// <summary>
    /// 更新粒子系统
    /// </summary>
    private void UpdateParticleSystem()
    {
        float startTime = Time.realtimeSinceStartup;
        
        // 更新粒子状态
        UpdateParticleStates();
        
        // 更新粒子统计
        UpdateParticleStatistics();
        
        particleUpdateTime = Time.realtimeSinceStartup - startTime;
    }

    /// <summary>
    /// 更新粒子状态
    /// </summary>
    private void UpdateParticleStates()
    {
        activeParticles = 0;
        
        for (int i = 0; i < particleCount; i++)
        {
            if (particleAlive[i])
            {
                activeParticles++;
                
                // 更新生命周期
                particleLifetimes[i] -= Time.deltaTime;
                
                // 检查粒子是否死亡
                if (particleLifetimes[i] <= 0f)
                {
                    particleAlive[i] = false;
                    continue;
                }
                
                // 更新颜色透明度
                float alpha = particleLifetimes[i] / particleLifetime;
                particleColors[i] = new float4(particleColors[i].xyz, alpha);
            }
        }
    }

    /// <summary>
    /// 更新粒子统计
    /// </summary>
    private void UpdateParticleStatistics()
    {
        // 计算平均生命周期
        float totalLifetime = 0f;
        int aliveCount = 0;
        
        for (int i = 0; i < particleCount; i++)
        {
            if (particleAlive[i])
            {
                totalLifetime += particleLifetimes[i];
                aliveCount++;
            }
        }
        
        if (aliveCount > 0)
        {
            averageParticleLifetime = totalLifetime / aliveCount;
        }
        
        // 计算发射率和死亡率
        particleEmissionRate = particleCount / particleLifetime;
        particleDeathRate = (particleCount - activeParticles) / particleLifetime;
    }

    /// <summary>
    /// 执行粒子作业
    /// </summary>
    private void ExecuteParticleJobs()
    {
        if (!enableParticleJobs) return;
        
        // 清理完成的作业
        CleanupCompletedParticleJobs();
        
        // 执行新作业
        if (activeParticleJobs.Count < particleJobHandles.Length)
        {
            ExecuteParticleJob();
        }
    }

    /// <summary>
    /// 执行粒子作业
    /// </summary>
    private void ExecuteParticleJob()
    {
        if (enableBurstCompilation)
        {
            var job = new ParticleBurstJob
            {
                positions = particlePositions,
                velocities = particleVelocities,
                lifetimes = particleLifetimes,
                sizes = particleSizes,
                colors = particleColors,
                alive = particleAlive,
                deltaTime = Time.deltaTime,
                gravity = new float3(particleGravity.x, particleGravity.y, particleGravity.z),
                particleLifetime = particleLifetime
            };
            
            var jobHandle = job.Schedule(particleCount, jobBatchSize);
            activeParticleJobs.Add(jobHandle);
            
            if (logParticleData)
            {
                Debug.Log($"Burst粒子作业已调度: 粒子数={particleCount}, 批处理={jobBatchSize}");
            }
        }
        else
        {
            var job = new ParticleJob
            {
                positions = particlePositions,
                velocities = particleVelocities,
                lifetimes = particleLifetimes,
                sizes = particleSizes,
                colors = particleColors,
                alive = particleAlive,
                deltaTime = Time.deltaTime,
                gravity = new float3(particleGravity.x, particleGravity.y, particleGravity.z),
                particleLifetime = particleLifetime
            };
            
            var jobHandle = job.Schedule(particleCount, jobBatchSize);
            activeParticleJobs.Add(jobHandle);
            
            if (logParticleData)
            {
                Debug.Log($"普通粒子作业已调度: 粒子数={particleCount}, 批处理={jobBatchSize}");
            }
        }
    }

    /// <summary>
    /// 清理完成的粒子作业
    /// </summary>
    private void CleanupCompletedParticleJobs()
    {
        for (int i = activeParticleJobs.Count - 1; i >= 0; i--)
        {
            if (activeParticleJobs[i].IsCompleted)
            {
                activeParticleJobs[i].Complete();
                activeParticleJobs.RemoveAt(i);
                completedJobs++;
            }
        }
    }

    /// <summary>
    /// 监控粒子性能
    /// </summary>
    private void MonitorParticlePerformance()
    {
        float currentPerformance = 1f / particleUpdateTime;
        
        // 更新性能历史
        performanceHistory[performanceIndex] = currentPerformance;
        performanceIndex = (performanceIndex + 1) % 100;
        
        // 计算性能统计
        CalculatePerformanceStats();
        
        if (logParticleData)
        {
            Debug.Log($"粒子性能监控: 性能={currentPerformance:F1}, 活跃粒子={activeParticles}, 更新时间={particleUpdateTime * 1000:F2}ms");
        }
    }

    /// <summary>
    /// 计算性能统计
    /// </summary>
    private void CalculatePerformanceStats()
    {
        float sum = 0f;
        minPerformance = float.MaxValue;
        maxPerformance = 0f;
        
        for (int i = 0; i < 100; i++)
        {
            float value = performanceHistory[i];
            sum += value;
            
            if (value < minPerformance) minPerformance = value;
            if (value > maxPerformance) maxPerformance = value;
        }
        
        averagePerformance = sum / 100;
    }

    /// <summary>
    /// 执行粒子碰撞检测
    /// </summary>
    public void ExecuteParticleCollision()
    {
        if (!enableParticleCollision) return;
        
        var job = new ParticleCollisionJob
        {
            positions = particlePositions,
            velocities = particleVelocities,
            alive = particleAlive,
            collisionRadius = 1f,
            bounceFactor = 0.8f
        };
        
        var jobHandle = job.Schedule(particleCount, jobBatchSize);
        activeParticleJobs.Add(jobHandle);
        
        Debug.Log("粒子碰撞检测作业已调度");
    }

    /// <summary>
    /// 执行粒子力场模拟
    /// </summary>
    public void ExecuteParticleForceField()
    {
        var job = new ParticleForceFieldJob
        {
            positions = particlePositions,
            velocities = particleVelocities,
            alive = particleAlive,
            forceFieldCenter = new float3(0, 0, 0),
            forceFieldStrength = 10f,
            forceFieldRadius = 5f
        };
        
        var jobHandle = job.Schedule(particleCount, jobBatchSize);
        activeParticleJobs.Add(jobHandle);
        
        Debug.Log("粒子力场模拟作业已调度");
    }

    /// <summary>
    /// 重置粒子系统
    /// </summary>
    public void ResetParticleSystem()
    {
        Debug.Log("重置粒子系统...");
        
        // 清理粒子作业
        CleanupCompletedParticleJobs();
        JobHandle.CompleteAll(activeParticleJobs.ToArray());
        activeParticleJobs.Clear();
        
        // 重新初始化粒子
        for (int i = 0; i < particleCount; i++)
        {
            particlePositions[i] = new float3(
                Random.Range(-10f, 10f),
                Random.Range(0f, 20f),
                Random.Range(-10f, 10f)
            );
            
            particleVelocities[i] = new float3(
                Random.Range(-5f, 5f),
                Random.Range(-2f, 10f),
                Random.Range(-5f, 5f)
            );
            
            particleLifetimes[i] = particleLifetime;
            particleSizes[i] = Random.Range(0.1f, 2f);
            particleColors[i] = new float4(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                1f
            );
            particleAlive[i] = true;
        }
        
        activeParticles = particleCount;
        completedJobs = 0;
        
        Debug.Log("粒子系统重置完成");
    }

    /// <summary>
    /// 生成粒子报告
    /// </summary>
    public void GenerateParticleReport()
    {
        Debug.Log("=== 粒子系统作业报告 ===");
        Debug.Log($"粒子系统状态: {particleSystemState}");
        Debug.Log($"系统运行时间: {systemUptime:F1}秒");
        Debug.Log($"活跃粒子数: {activeParticles}/{particleCount}");
        Debug.Log($"平均粒子生命周期: {averageParticleLifetime:F2}秒");
        Debug.Log($"粒子发射率: {particleEmissionRate:F1}/秒");
        Debug.Log($"粒子死亡率: {particleDeathRate:F1}/秒");
        Debug.Log($"粒子更新时间: {particleUpdateTime * 1000:F2}ms");
        Debug.Log($"完成作业数: {completedJobs}");
        Debug.Log($"平均性能: {averagePerformance:F1}");
        Debug.Log($"最低性能: {minPerformance:F1}");
        Debug.Log($"最高性能: {maxPerformance:F1}");
    }

    /// <summary>
    /// 导出粒子数据
    /// </summary>
    public void ExportParticleData()
    {
        var data = new ParticleSystemData
        {
            timestamp = System.DateTime.Now.ToString(),
            particleSystemState = particleSystemState,
            systemUptime = systemUptime,
            activeParticles = activeParticles,
            particleCount = particleCount,
            averageParticleLifetime = averageParticleLifetime,
            particleEmissionRate = particleEmissionRate,
            particleDeathRate = particleDeathRate,
            particleUpdateTime = particleUpdateTime,
            completedJobs = completedJobs,
            averagePerformance = averagePerformance,
            minPerformance = minPerformance,
            maxPerformance = maxPerformance,
            performanceHistory = performanceHistory
        };
        
        string json = JsonUtility.ToJson(data, true);
        string filename = $"particlesystem_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        
        System.IO.File.WriteAllText(filename, json);
        Debug.Log($"粒子数据已导出: {filename}");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 800));
        GUILayout.Label("ParticleSystemJobs 粒子系统作业演示", UnityEditor.EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        GUILayout.Label("粒子系统配置:");
        enableParticleJobs = GUILayout.Toggle(enableParticleJobs, "启用粒子作业");
        enableBurstCompilation = GUILayout.Toggle(enableBurstCompilation, "启用Burst编译");
        enableJobScheduling = GUILayout.Toggle(enableJobScheduling, "启用作业调度");
        enablePerformanceOptimization = GUILayout.Toggle(enablePerformanceOptimization, "启用性能优化");
        enableParticleProfiling = GUILayout.Toggle(enableParticleProfiling, "启用粒子分析");
        
        GUILayout.Space(10);
        GUILayout.Label("粒子参数:");
        particleCount = int.TryParse(GUILayout.TextField("粒子数量", particleCount.ToString()), out var count) ? count : particleCount;
        jobBatchSize = int.TryParse(GUILayout.TextField("作业批处理大小", jobBatchSize.ToString()), out var batchSize) ? batchSize : jobBatchSize;
        particleLifetime = float.TryParse(GUILayout.TextField("粒子生命周期", particleLifetime.ToString()), out var lifetime) ? lifetime : particleLifetime;
        particleSpeed = float.TryParse(GUILayout.TextField("粒子速度", particleSpeed.ToString()), out var speed) ? speed : particleSpeed;
        enableParticleCollision = GUILayout.Toggle(enableParticleCollision, "启用粒子碰撞");
        
        GUILayout.Space(10);
        GUILayout.Label("粒子状态:");
        GUILayout.Label($"状态: {particleSystemState}");
        GUILayout.Label($"运行时间: {systemUptime:F1}秒");
        GUILayout.Label($"活跃粒子: {activeParticles}/{particleCount}");
        GUILayout.Label($"平均生命周期: {averageParticleLifetime:F2}秒");
        GUILayout.Label($"发射率: {particleEmissionRate:F1}/秒");
        GUILayout.Label($"死亡率: {particleDeathRate:F1}/秒");
        GUILayout.Label($"更新时间: {particleUpdateTime * 1000:F2}ms");
        GUILayout.Label($"完成作业数: {completedJobs}");
        
        GUILayout.Space(10);
        GUILayout.Label("性能数据:");
        GUILayout.Label($"平均性能: {averagePerformance:F1}");
        GUILayout.Label($"最低性能: {minPerformance:F1}");
        GUILayout.Label($"最高性能: {maxPerformance:F1}");
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("执行粒子碰撞检测"))
        {
            ExecuteParticleCollision();
        }
        
        if (GUILayout.Button("执行粒子力场模拟"))
        {
            ExecuteParticleForceField();
        }
        
        if (GUILayout.Button("重置粒子系统"))
        {
            ResetParticleSystem();
        }
        
        if (GUILayout.Button("生成粒子报告"))
        {
            GenerateParticleReport();
        }
        
        if (GUILayout.Button("导出粒子数据"))
        {
            ExportParticleData();
        }
        
        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        // 清理粒子数据
        if (particlePositions.IsCreated)
            particlePositions.Dispose();
        if (particleVelocities.IsCreated)
            particleVelocities.Dispose();
        if (particleLifetimes.IsCreated)
            particleLifetimes.Dispose();
        if (particleSizes.IsCreated)
            particleSizes.Dispose();
        if (particleColors.IsCreated)
            particleColors.Dispose();
        if (particleAlive.IsCreated)
            particleAlive.Dispose();
        
        // 清理粒子作业
        CleanupCompletedParticleJobs();
        JobHandle.CompleteAll(activeParticleJobs.ToArray());
    }
}

/// <summary>
/// Burst粒子作业
/// </summary>
[BurstCompile]
public struct ParticleBurstJob : IJobParallelFor
{
    [NativeDisableParallelForRestriction] public NativeArray<float3> positions;
    [NativeDisableParallelForRestriction] public NativeArray<float3> velocities;
    [NativeDisableParallelForRestriction] public NativeArray<float> lifetimes;
    [NativeDisableParallelForRestriction] public NativeArray<float> sizes;
    [NativeDisableParallelForRestriction] public NativeArray<float4> colors;
    [NativeDisableParallelForRestriction] public NativeArray<bool> alive;
    public float deltaTime;
    public float3 gravity;
    public float particleLifetime;
    
    public void Execute(int index)
    {
        if (!alive[index]) return;
        
        // 更新位置
        positions[index] += velocities[index] * deltaTime;
        
        // 应用重力
        velocities[index] += gravity * deltaTime;
        
        // 更新生命周期
        lifetimes[index] -= deltaTime;
        
        // 检查粒子是否死亡
        if (lifetimes[index] <= 0f)
        {
            alive[index] = false;
            return;
        }
        
        // 更新颜色透明度
        float alpha = lifetimes[index] / particleLifetime;
        colors[index] = new float4(colors[index].xyz, alpha);
    }
}

/// <summary>
/// 普通粒子作业
/// </summary>
public struct ParticleJob : IJobParallelFor
{
    [NativeDisableParallelForRestriction] public NativeArray<float3> positions;
    [NativeDisableParallelForRestriction] public NativeArray<float3> velocities;
    [NativeDisableParallelForRestriction] public NativeArray<float> lifetimes;
    [NativeDisableParallelForRestriction] public NativeArray<float> sizes;
    [NativeDisableParallelForRestriction] public NativeArray<float4> colors;
    [NativeDisableParallelForRestriction] public NativeArray<bool> alive;
    public float deltaTime;
    public float3 gravity;
    public float particleLifetime;
    
    public void Execute(int index)
    {
        if (!alive[index]) return;
        
        // 更新位置
        positions[index] += velocities[index] * deltaTime;
        
        // 应用重力
        velocities[index] += gravity * deltaTime;
        
        // 更新生命周期
        lifetimes[index] -= deltaTime;
        
        // 检查粒子是否死亡
        if (lifetimes[index] <= 0f)
        {
            alive[index] = false;
            return;
        }
        
        // 更新颜色透明度
        float alpha = lifetimes[index] / particleLifetime;
        colors[index] = new float4(colors[index].xyz, alpha);
    }
}

/// <summary>
/// 粒子碰撞作业
/// </summary>
[BurstCompile]
public struct ParticleCollisionJob : IJobParallelFor
{
    [NativeDisableParallelForRestriction] public NativeArray<float3> positions;
    [NativeDisableParallelForRestriction] public NativeArray<float3> velocities;
    [NativeDisableParallelForRestriction] public NativeArray<bool> alive;
    public float collisionRadius;
    public float bounceFactor;
    
    public void Execute(int index)
    {
        if (!alive[index]) return;
        
        // 简单的边界碰撞检测
        float3 pos = positions[index];
        
        // 地面碰撞
        if (pos.y < 0f)
        {
            pos.y = 0f;
            velocities[index].y = -velocities[index].y * bounceFactor;
        }
        
        // 边界碰撞
        if (math.abs(pos.x) > 10f)
        {
            pos.x = math.sign(pos.x) * 10f;
            velocities[index].x = -velocities[index].x * bounceFactor;
        }
        
        if (math.abs(pos.z) > 10f)
        {
            pos.z = math.sign(pos.z) * 10f;
            velocities[index].z = -velocities[index].z * bounceFactor;
        }
        
        positions[index] = pos;
    }
}

/// <summary>
/// 粒子力场作业
/// </summary>
[BurstCompile]
public struct ParticleForceFieldJob : IJobParallelFor
{
    [NativeDisableParallelForRestriction] public NativeArray<float3> positions;
    [NativeDisableParallelForRestriction] public NativeArray<float3> velocities;
    [NativeDisableParallelForRestriction] public NativeArray<bool> alive;
    public float3 forceFieldCenter;
    public float forceFieldStrength;
    public float forceFieldRadius;
    
    public void Execute(int index)
    {
        if (!alive[index]) return;
        
        float3 direction = forceFieldCenter - positions[index];
        float distance = math.length(direction);
        
        if (distance < forceFieldRadius && distance > 0f)
        {
            direction = math.normalize(direction);
            float force = forceFieldStrength * (1f - distance / forceFieldRadius);
            velocities[index] += direction * force * 0.016f; // 假设60FPS
        }
    }
}

/// <summary>
/// 粒子系统数据类
/// </summary>
[System.Serializable]
public class ParticleSystemData
{
    public string timestamp;
    public string particleSystemState;
    public float systemUptime;
    public int activeParticles;
    public int particleCount;
    public float averageParticleLifetime;
    public float particleEmissionRate;
    public float particleDeathRate;
    public float particleUpdateTime;
    public int completedJobs;
    public float averagePerformance;
    public float minPerformance;
    public float maxPerformance;
    public float[] performanceHistory;
} 