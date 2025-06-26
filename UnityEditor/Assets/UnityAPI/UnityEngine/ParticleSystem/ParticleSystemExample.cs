using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.ParticleSystem 命名空间案例演示
/// 展示粒子系统的核心功能
/// </summary>
public class ParticleSystemExample : MonoBehaviour
{
    [Header("粒子系统组件")]
    [SerializeField] private ParticleSystem mainParticleSystem;    //主粒子系统
    [SerializeField] private ParticleSystem[] subParticleSystems; //子粒子系统
    [SerializeField] private ParticleSystemRenderer particleRenderer; //粒子系统渲染器
    
    [Header("粒子系统设置")]
    [SerializeField] private bool enableParticleSystem = true; //是否启用粒子系统
    [SerializeField] private bool autoStart = true; //是否自动开始
    [SerializeField] private bool loop = true; //是否循环
    [SerializeField] private float duration = 5f; //持续时间
    [SerializeField] private float startLifetime = 2f; //起始生命周期
    [SerializeField] private int maxParticles = 1000; //最大粒子数
    
    [Header("发射设置")]
    [SerializeField] private float emissionRate = 10f; //发射率
    [SerializeField] private float burstCount = 5f; //爆发数量
    [SerializeField] private float burstTime = 0.5f; //爆发时间
    [SerializeField] private ParticleSystemShapeType shapeType = ParticleSystemShapeType.Sphere; //形状类型
    [SerializeField] private float shapeRadius = 1f; //形状半径
    
    [Header("粒子属性")]
    [SerializeField] private float startSpeed = 5f; //起始速度
    [SerializeField] private float startSize = 0.1f; //起始大小
    [SerializeField] private Color startColor = Color.white; //起始颜色
    [SerializeField] private float gravityModifier = 1f; //重力修改器
    [SerializeField] private bool useWorldSpace = true; //是否使用世界空间
    
    [Header("粒子系统状态")]
    [SerializeField] private bool isPlaying = false; //是否播放
    [SerializeField] private bool isPaused = false; //是否暂停
    [SerializeField] private float currentTime = 0f; //当前时间
    [SerializeField] private int particleCount = 0; //粒子数量
    [SerializeField] private float emissionRateOverTime = 0f; //发射率
    
    [Header("效果设置")]
    [SerializeField] private bool enableTrails = false; //是否启用轨迹
    [SerializeField] private bool enableCollision = false; //是否启用碰撞
    [SerializeField] private bool enableSubEmitters = false; //是否启用子发射器
    [SerializeField] private bool enableForceOverLifetime = false; //是否启用力随时间变化
    [SerializeField] private bool enableColorOverLifetime = false;  //是否启用颜色随时间变化
    [SerializeField] private bool enableSizeOverLifetime = false; //是否启用大小随时间变化
    
    // 粒子系统事件
    private System.Action<ParticleSystem> onParticleSystemStart;  //粒子系统开始事件
    private System.Action<ParticleSystem> onParticleSystemStop; //粒子系统停止事件
    private System.Action<ParticleSystem> onParticleSystemPause; //粒子系统暂停事件
    
    private void Start()
    {
        InitializeParticleSystem();
    }
    
    /// <summary>
    /// 初始化粒子系统
    /// </summary>
    private void InitializeParticleSystem()
    {
        // 获取或创建主粒子系统
        if (mainParticleSystem == null)
        {
            mainParticleSystem = GetComponent<ParticleSystem>();
            if (mainParticleSystem == null)
            {
                mainParticleSystem = gameObject.AddComponent<ParticleSystem>();
            }
        }
        
        // 获取粒子系统渲染器
        if (particleRenderer == null)
        {
            particleRenderer = GetComponent<ParticleSystemRenderer>();
            if (particleRenderer == null)
            {
                particleRenderer = gameObject.GetComponent<ParticleSystemRenderer>();
            }
        }
        
        // 配置粒子系统
        ConfigureParticleSystem();
        
        // 设置粒子系统事件
        SetupParticleSystemEvents();
        
        // 创建子粒子系统
        CreateSubParticleSystems();
        
        Debug.Log("粒子系统初始化完成");
    }
    
    /// <summary>
    /// 配置粒子系统
    /// </summary>
    private void ConfigureParticleSystem()
    {
        if (mainParticleSystem == null) return;
        
        var main = mainParticleSystem.main;
        var emission = mainParticleSystem.emission;
        var shape = mainParticleSystem.shape;
        var velocityOverLifetime = mainParticleSystem.velocityOverLifetime;
        var colorOverLifetime = mainParticleSystem.colorOverLifetime;
        var sizeOverLifetime = mainParticleSystem.sizeOverLifetime;
        var trails = mainParticleSystem.trails;
        var collision = mainParticleSystem.collision;
        var subEmitters = mainParticleSystem.subEmitters;
        var forceOverLifetime = mainParticleSystem.forceOverLifetime;
        
        // 主模块设置
        main.duration = duration;
        main.loop = loop;
        main.startLifetime = startLifetime;
        main.startSpeed = startSpeed;
        main.startSize = startSize;
        main.startColor = startColor;
        main.gravityModifier = gravityModifier;
        main.maxParticles = maxParticles;
        main.simulationSpace = useWorldSpace ? ParticleSystemSimulationSpace.World : ParticleSystemSimulationSpace.Local;
        
        // 发射模块设置
        emission.enabled = true;
        emission.rateOverTime = emissionRate;
        
        // 添加爆发
        var burst = new ParticleSystem.Burst(burstTime, burstCount);
        emission.SetBursts(new ParticleSystem.Burst[] { burst });
        
        // 形状模块设置
        shape.enabled = true;
        shape.shapeType = shapeType;
        shape.radius = shapeRadius;
        
        // 速度随时间变化
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.Local;
        
        // 颜色随时间变化
        if (enableColorOverLifetime)
        {
            colorOverLifetime.enabled = true;
            var colorGradient = new Gradient();
            colorGradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.blue, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
            );
            colorOverLifetime.color = colorGradient;
        }
        
        // 大小随时间变化
        if (enableSizeOverLifetime)
        {
            sizeOverLifetime.enabled = true;
            var sizeCurve = new AnimationCurve();
            sizeCurve.AddKey(0f, 0.1f);
            sizeCurve.AddKey(0.5f, 1f);
            sizeCurve.AddKey(1f, 0.1f);
            sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, sizeCurve);
        }
        
        // 轨迹设置
        if (enableTrails)
        {
            trails.enabled = true;
            trails.mode = ParticleSystemTrailMode.PerParticle;
            trails.ratio = 1f;
            trails.lifetime = 0.5f;
        }
        
        // 碰撞设置
        if (enableCollision)
        {
            collision.enabled = true;
            collision.type = ParticleSystemCollisionType.World;
            collision.mode = ParticleSystemCollisionMode.Collision3D;
        }
        
        // 力随时间变化
        if (enableForceOverLifetime)
        {
            forceOverLifetime.enabled = true;
            forceOverLifetime.space = ParticleSystemSimulationSpace.World;
            forceOverLifetime.x = new ParticleSystem.MinMaxCurve(0f, 1f);
            forceOverLifetime.y = new ParticleSystem.MinMaxCurve(0f, 1f);
            forceOverLifetime.z = new ParticleSystem.MinMaxCurve(0f, 1f);
        }
        
        Debug.Log("粒子系统配置完成");
    }
    
    /// <summary>
    /// 设置粒子系统事件
    /// </summary>
    private void SetupParticleSystemEvents()
    {
        if (mainParticleSystem == null) return;
        
        // 监听粒子系统事件
        mainParticleSystem.Stop();
        mainParticleSystem.Clear();
        
        Debug.Log("粒子系统事件设置完成");
    }
    
    /// <summary>
    /// 创建子粒子系统
    /// </summary>
    private void CreateSubParticleSystems()
    {
        if (subParticleSystems == null || subParticleSystems.Length == 0)
        {
            // 创建默认的子粒子系统
            subParticleSystems = new ParticleSystem[2];
            
            // 创建爆炸效果
            GameObject explosionObj = new GameObject("ExplosionParticles");
            explosionObj.transform.SetParent(transform);
            explosionObj.transform.localPosition = Vector3.zero;
            
            var explosionPS = explosionObj.AddComponent<ParticleSystem>();
            var explosionMain = explosionPS.main;
            explosionMain.startLifetime = 1f;
            explosionMain.startSpeed = 3f;
            explosionMain.startSize = 0.2f;
            explosionMain.startColor = Color.yellow;
            explosionMain.maxParticles = 50;
            
            var explosionEmission = explosionPS.emission;
            explosionEmission.rateOverTime = 0;
            explosionEmission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, 20) });
            
            subParticleSystems[0] = explosionPS;
            
            // 创建烟雾效果
            GameObject smokeObj = new GameObject("SmokeParticles");
            smokeObj.transform.SetParent(transform);
            smokeObj.transform.localPosition = Vector3.zero;
            
            var smokePS = smokeObj.AddComponent<ParticleSystem>();
            var smokeMain = smokePS.main;
            smokeMain.startLifetime = 3f;
            smokeMain.startSpeed = 1f;
            smokeMain.startSize = 0.5f;
            smokeMain.startColor = Color.gray;
            smokeMain.maxParticles = 100;
            
            var smokeEmission = smokePS.emission;
            smokeEmission.rateOverTime = 5f;
            
            subParticleSystems[1] = smokePS;
        }
        
        Debug.Log($"创建了 {subParticleSystems.Length} 个子粒子系统");
    }
    
    /// <summary>
    /// 播放粒子系统
    /// </summary>
    public void PlayParticleSystem()
    {
        if (mainParticleSystem != null)
        {
            mainParticleSystem.Play();
            isPlaying = true;
            isPaused = false;
        }
        
        // 播放子粒子系统
        foreach (var subPS in subParticleSystems)
        {
            if (subPS != null)
            {
                subPS.Play();
            }
        }
        
        Debug.Log("粒子系统开始播放");
        onParticleSystemStart?.Invoke(mainParticleSystem);
    }
    
    /// <summary>
    /// 暂停粒子系统
    /// </summary>
    public void PauseParticleSystem()
    {
        if (mainParticleSystem != null)
        {
            mainParticleSystem.Pause();
            isPaused = true;
        }
        
        // 暂停子粒子系统
        foreach (var subPS in subParticleSystems)
        {
            if (subPS != null)
            {
                subPS.Pause();
            }
        }
        
        Debug.Log("粒子系统已暂停");
        onParticleSystemPause?.Invoke(mainParticleSystem);
    }
    
    /// <summary>
    /// 停止粒子系统
    /// </summary>
    public void StopParticleSystem()
    {
        if (mainParticleSystem != null)
        {
            mainParticleSystem.Stop();
            isPlaying = false;
            isPaused = false;
        }
        
        // 停止子粒子系统
        foreach (var subPS in subParticleSystems)
        {
            if (subPS != null)
            {
                subPS.Stop();
            }
        }
        
        Debug.Log("粒子系统已停止");
        onParticleSystemStop?.Invoke(mainParticleSystem);
    }
    
    /// <summary>
    /// 清除粒子系统
    /// </summary>
    public void ClearParticleSystem()
    {
        if (mainParticleSystem != null)
        {
            mainParticleSystem.Clear();
        }
        
        // 清除子粒子系统
        foreach (var subPS in subParticleSystems)
        {
            if (subPS != null)
            {
                subPS.Clear();
            }
        }
        
        Debug.Log("粒子系统已清除");
    }
    
    /// <summary>
    /// 设置发射率
    /// </summary>
    /// <param name="rate">发射率</param>
    public void SetEmissionRate(float rate)
    {
        emissionRate = Mathf.Max(0f, rate);
        
        if (mainParticleSystem != null)
        {
            var emission = mainParticleSystem.emission;
            emission.rateOverTime = emissionRate;
        }
        
        Debug.Log($"发射率已设置为: {emissionRate}");
    }
    
    /// <summary>
    /// 设置粒子速度
    /// </summary>
    /// <param name="speed">速度</param>
    public void SetParticleSpeed(float speed)
    {
        startSpeed = Mathf.Max(0f, speed);
        
        if (mainParticleSystem != null)
        {
            var main = mainParticleSystem.main;
            main.startSpeed = startSpeed;
        }
        
        Debug.Log($"粒子速度已设置为: {startSpeed}");
    }
    
    /// <summary>
    /// 设置粒子大小
    /// </summary>
    /// <param name="size">大小</param>
    public void SetParticleSize(float size)
    {
        startSize = Mathf.Max(0f, size);
        
        if (mainParticleSystem != null)
        {
            var main = mainParticleSystem.main;
            main.startSize = startSize;
        }
        
        Debug.Log($"粒子大小已设置为: {startSize}");
    }
    
    /// <summary>
    /// 设置粒子颜色
    /// </summary>
    /// <param name="color">颜色</param>
    public void SetParticleColor(Color color)
    {
        startColor = color;
        
        if (mainParticleSystem != null)
        {
            var main = mainParticleSystem.main;
            main.startColor = startColor;
        }
        
        Debug.Log($"粒子颜色已设置为: {startColor}");
    }
    
    /// <summary>
    /// 设置重力修改器
    /// </summary>
    /// <param name="gravity">重力修改器</param>
    public void SetGravityModifier(float gravity)
    {
        gravityModifier = gravity;
        
        if (mainParticleSystem != null)
        {
            var main = mainParticleSystem.main;
            main.gravityModifier = gravityModifier;
        }
        
        Debug.Log($"重力修改器已设置为: {gravityModifier}");
    }
    
    /// <summary>
    /// 设置形状类型
    /// </summary>
    /// <param name="shapeType">形状类型</param>
    public void SetShapeType(ParticleSystemShapeType shapeType)
    {
        this.shapeType = shapeType;
        
        if (mainParticleSystem != null)
        {
            var shape = mainParticleSystem.shape;
            shape.shapeType = shapeType;
        }
        
        Debug.Log($"形状类型已设置为: {shapeType}");
    }
    
    /// <summary>
    /// 设置形状半径
    /// </summary>
    /// <param name="radius">半径</param>
    public void SetShapeRadius(float radius)
    {
        shapeRadius = Mathf.Max(0f, radius);
        
        if (mainParticleSystem != null)
        {
            var shape = mainParticleSystem.shape;
            shape.radius = shapeRadius;
        }
        
        Debug.Log($"形状半径已设置为: {shapeRadius}");
    }
    
    /// <summary>
    /// 触发爆炸效果
    /// </summary>
    public void TriggerExplosion()
    {
        if (subParticleSystems != null && subParticleSystems.Length > 0 && subParticleSystems[0] != null)
        {
            subParticleSystems[0].Play();
            Debug.Log("触发爆炸效果");
        }
    }
    
    /// <summary>
    /// 触发烟雾效果
    /// </summary>
    public void TriggerSmoke()
    {
        if (subParticleSystems != null && subParticleSystems.Length > 1 && subParticleSystems[1] != null)
        {
            subParticleSystems[1].Play();
            Debug.Log("触发烟雾效果");
        }
    }
    
    /// <summary>
    /// 获取粒子系统信息
    /// </summary>
    public void GetParticleSystemInfo()
    {
        Debug.Log("=== 粒子系统信息 ===");
        Debug.Log($"粒子系统启用: {enableParticleSystem}");
        Debug.Log($"播放状态: {(isPlaying ? (isPaused ? "暂停" : "播放中") : "停止")}");
        Debug.Log($"当前时间: {currentTime:F2}s");
        Debug.Log($"粒子数量: {particleCount}");
        Debug.Log($"发射率: {emissionRateOverTime}");
        Debug.Log($"持续时间: {duration}s");
        Debug.Log($"最大粒子数: {maxParticles}");
        Debug.Log($"循环播放: {loop}");
        Debug.Log($"自动开始: {autoStart}");
        
        if (mainParticleSystem != null)
        {
            Debug.Log($"主粒子系统: 已配置");
            Debug.Log($"活跃粒子数: {mainParticleSystem.particleCount}");
            Debug.Log($"总粒子数: {mainParticleSystem.totalParticleCount}");
            Debug.Log($"时间: {mainParticleSystem.time}");
            Debug.Log($"是否播放: {mainParticleSystem.isPlaying}");
            Debug.Log($"是否暂停: {mainParticleSystem.isPaused}");
        }
        
        if (subParticleSystems != null)
        {
            Debug.Log($"子粒子系统数量: {subParticleSystems.Length}");
            for (int i = 0; i < subParticleSystems.Length; i++)
            {
                if (subParticleSystems[i] != null)
                {
                    Debug.Log($"  子系统 {i}: {subParticleSystems[i].name} (活跃粒子: {subParticleSystems[i].particleCount})");
                }
            }
        }
    }
    
    /// <summary>
    /// 重置粒子系统设置
    /// </summary>
    public void ResetParticleSystemSettings()
    {
        // 重置设置
        enableParticleSystem = true;
        autoStart = true;
        loop = true;
        duration = 5f;
        startLifetime = 2f;
        maxParticles = 1000;
        emissionRate = 10f;
        startSpeed = 5f;
        startSize = 0.1f;
        startColor = Color.white;
        gravityModifier = 1f;
        shapeType = ParticleSystemShapeType.Sphere;
        shapeRadius = 1f;
        
        // 重新配置
        ConfigureParticleSystem();
        
        Debug.Log("粒子系统设置已重置");
    }
    
    private void Update()
    {
        if (mainParticleSystem != null)
        {
            // 更新状态
            currentTime = mainParticleSystem.time;
            particleCount = mainParticleSystem.particleCount;
            
            var emission = mainParticleSystem.emission;
            emissionRateOverTime = emission.rateOverTime.constant;
        }
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 700));
        GUILayout.Label("粒子系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 粒子系统状态
        GUILayout.Label($"播放状态: {(isPlaying ? (isPaused ? "暂停" : "播放中") : "停止")}");
        GUILayout.Label($"当前时间: {currentTime:F2}s");
        GUILayout.Label($"粒子数量: {particleCount}");
        GUILayout.Label($"发射率: {emissionRateOverTime:F1}");
        
        GUILayout.Space(10);
        
        // 控制按钮
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("播放"))
        {
            PlayParticleSystem();
        }
        if (GUILayout.Button("暂停"))
        {
            PauseParticleSystem();
        }
        if (GUILayout.Button("停止"))
        {
            StopParticleSystem();
        }
        GUILayout.EndHorizontal();
        
        if (GUILayout.Button("清除"))
        {
            ClearParticleSystem();
        }
        
        GUILayout.Space(10);
        
        // 效果按钮
        if (GUILayout.Button("触发爆炸"))
        {
            TriggerExplosion();
        }
        
        if (GUILayout.Button("触发烟雾"))
        {
            TriggerSmoke();
        }
        
        GUILayout.Space(10);
        
        // 参数控制
        GUILayout.Label("发射率:");
        emissionRate = GUILayout.HorizontalSlider(emissionRate, 0f, 50f);
        if (GUILayout.Button("设置发射率"))
        {
            SetEmissionRate(emissionRate);
        }
        
        GUILayout.Label("粒子速度:");
        startSpeed = GUILayout.HorizontalSlider(startSpeed, 0f, 20f);
        if (GUILayout.Button("设置速度"))
        {
            SetParticleSpeed(startSpeed);
        }
        
        GUILayout.Label("粒子大小:");
        startSize = GUILayout.HorizontalSlider(startSize, 0.01f, 2f);
        if (GUILayout.Button("设置大小"))
        {
            SetParticleSize(startSize);
        }
        
        GUILayout.Label("重力修改器:");
        gravityModifier = GUILayout.HorizontalSlider(gravityModifier, -2f, 2f);
        if (GUILayout.Button("设置重力"))
        {
            SetGravityModifier(gravityModifier);
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取粒子系统信息"))
        {
            GetParticleSystemInfo();
        }
        
        if (GUILayout.Button("重置设置"))
        {
            ResetParticleSystemSettings();
        }
        
        GUILayout.EndArea();
    }
} 