using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Physics 命名空间案例演示
/// 展示物理系统的核心功能
/// </summary>
public class PhysicsExample : MonoBehaviour
{
    [Header("物理设置")]
    [SerializeField] private bool enablePhysics = true;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private int maxPhysicsObjects = 100;
    [SerializeField] private LayerMask physicsLayerMask = -1;
    
    [Header("物理对象")]
    [SerializeField] private GameObject physicsPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<Rigidbody> physicsObjects = new List<Rigidbody>();
    
    [Header("物理状态")]
    [SerializeField] private bool isSimulating = true;
    [SerializeField] private float simulationTime = 0f;
    [SerializeField] private int activeObjects = 0;
    [SerializeField] private float totalKineticEnergy = 0f;
    
    [Header("物理参数")]
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private float dragForce = 0.5f;
    [SerializeField] private float angularDrag = 0.05f;
    [SerializeField] private bool useGravity = true;
    
    [Header("碰撞检测")]
    [SerializeField] private bool enableCollisionDetection = true;
    [SerializeField] private float collisionRadius = 5f;
    [SerializeField] private Vector3 collisionCenter = Vector3.zero;
    
    // 物理事件
    private System.Action<Collision> onCollisionEnter;
    private System.Action<Collision> onCollisionExit;
    private System.Action<Collider> onTriggerEnter;
    private System.Action<Collider> onTriggerExit;
    
    private void Start()
    {
        InitializePhysicsSystem();
    }
    
    /// <summary>
    /// 初始化物理系统
    /// </summary>
    private void InitializePhysicsSystem()
    {
        // 设置物理参数
        Physics.gravity = new Vector3(0, gravity, 0);
        Physics.autoSimulation = isSimulating;
        
        // 设置碰撞检测
        if (enableCollisionDetection)
        {
            SetupCollisionDetection();
        }
        
        // 创建物理对象
        if (physicsPrefab != null)
        {
            CreatePhysicsObjects();
        }
        
        Debug.Log("物理系统初始化完成");
    }
    
    /// <summary>
    /// 设置碰撞检测
    /// </summary>
    private void SetupCollisionDetection()
    {
        // 添加碰撞器组件
        var collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<SphereCollider>();
        }
        
        // 设置触发器
        if (collider is SphereCollider sphereCollider)
        {
            sphereCollider.isTrigger = true;
            sphereCollider.radius = collisionRadius;
        }
        
        // 添加刚体组件
        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        
        Debug.Log("碰撞检测设置完成");
    }
    
    /// <summary>
    /// 创建物理对象
    /// </summary>
    private void CreatePhysicsObjects()
    {
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
        
        for (int i = 0; i < maxPhysicsObjects; i++)
        {
            Vector3 spawnPosition = spawnPoint.position + Random.insideUnitSphere * 2f;
            GameObject obj = Instantiate(physicsPrefab, spawnPosition, Random.rotation);
            
            var rigidbody = obj.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                ConfigureRigidbody(rigidbody);
                physicsObjects.Add(rigidbody);
            }
        }
        
        activeObjects = physicsObjects.Count;
        Debug.Log($"创建了 {activeObjects} 个物理对象");
    }
    
    /// <summary>
    /// 配置刚体
    /// </summary>
    /// <param name="rigidbody">刚体组件</param>
    private void ConfigureRigidbody(Rigidbody rigidbody)
    {
        rigidbody.mass = Random.Range(0.1f, 5f);
        rigidbody.drag = dragForce;
        rigidbody.angularDrag = angularDrag;
        rigidbody.useGravity = useGravity;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        // 添加随机力
        rigidbody.AddForce(Random.insideUnitSphere * bounceForce, ForceMode.Impulse);
        rigidbody.AddTorque(Random.insideUnitSphere * bounceForce, ForceMode.Impulse);
    }
    
    /// <summary>
    /// 添加物理对象
    /// </summary>
    /// <param name="position">生成位置</param>
    public void AddPhysicsObject(Vector3 position)
    {
        if (physicsPrefab == null || physicsObjects.Count >= maxPhysicsObjects)
        {
            return;
        }
        
        GameObject obj = Instantiate(physicsPrefab, position, Random.rotation);
        var rigidbody = obj.GetComponent<Rigidbody>();
        
        if (rigidbody != null)
        {
            ConfigureRigidbody(rigidbody);
            physicsObjects.Add(rigidbody);
            activeObjects = physicsObjects.Count;
        }
        
        Debug.Log($"添加物理对象，当前数量: {activeObjects}");
    }
    
    /// <summary>
    /// 移除物理对象
    /// </summary>
    /// <param name="index">对象索引</param>
    public void RemovePhysicsObject(int index)
    {
        if (index >= 0 && index < physicsObjects.Count)
        {
            var rigidbody = physicsObjects[index];
            if (rigidbody != null)
            {
                Destroy(rigidbody.gameObject);
            }
            
            physicsObjects.RemoveAt(index);
            activeObjects = physicsObjects.Count;
            
            Debug.Log($"移除物理对象，当前数量: {activeObjects}");
        }
    }
    
    /// <summary>
    /// 清除所有物理对象
    /// </summary>
    public void ClearPhysicsObjects()
    {
        foreach (var rigidbody in physicsObjects)
        {
            if (rigidbody != null)
            {
                Destroy(rigidbody.gameObject);
            }
        }
        
        physicsObjects.Clear();
        activeObjects = 0;
        
        Debug.Log("所有物理对象已清除");
    }
    
    /// <summary>
    /// 设置重力
    /// </summary>
    /// <param name="newGravity">新的重力值</param>
    public void SetGravity(float newGravity)
    {
        gravity = newGravity;
        Physics.gravity = new Vector3(0, gravity, 0);
        
        Debug.Log($"重力已设置为: {gravity}");
    }
    
    /// <summary>
    /// 设置物理模拟
    /// </summary>
    /// <param name="simulate">是否模拟</param>
    public void SetPhysicsSimulation(bool simulate)
    {
        isSimulating = simulate;
        Physics.autoSimulation = simulate;
        
        Debug.Log($"物理模拟已设置为: {simulate}");
    }
    
    /// <summary>
    /// 手动模拟物理
    /// </summary>
    /// <param name="deltaTime">时间步长</param>
    public void SimulatePhysics(float deltaTime)
    {
        if (!isSimulating)
        {
            Physics.Simulate(deltaTime);
            simulationTime += deltaTime;
        }
    }
    
    /// <summary>
    /// 应用力到所有对象
    /// </summary>
    /// <param name="force">力的大小</param>
    /// <param name="forceMode">力的模式</param>
    public void ApplyForceToAll(float force, ForceMode forceMode = ForceMode.Force)
    {
        Vector3 forceVector = Random.insideUnitSphere * force;
        
        foreach (var rigidbody in physicsObjects)
        {
            if (rigidbody != null)
            {
                rigidbody.AddForce(forceVector, forceMode);
            }
        }
        
        Debug.Log($"向所有对象施加力: {force}");
    }
    
    /// <summary>
    /// 应用爆炸力
    /// </summary>
    /// <param name="explosionPosition">爆炸位置</param>
    /// <param name="explosionForce">爆炸力</param>
    /// <param name="explosionRadius">爆炸半径</param>
    public void ApplyExplosionForce(Vector3 explosionPosition, float explosionForce, float explosionRadius)
    {
        foreach (var rigidbody in physicsObjects)
        {
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
        
        Debug.Log($"应用爆炸力: {explosionForce}, 半径: {explosionRadius}");
    }
    
    /// <summary>
    /// 射线检测
    /// </summary>
    /// <param name="origin">射线起点</param>
    /// <param name="direction">射线方向</param>
    /// <param name="maxDistance">最大距离</param>
    /// <returns>检测结果</returns>
    public RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance = 100f)
    {
        return Physics.RaycastAll(origin, direction, maxDistance, physicsLayerMask);
    }
    
    /// <summary>
    /// 球体检测
    /// </summary>
    /// <param name="center">球心</param>
    /// <param name="radius">半径</param>
    /// <returns>检测结果</returns>
    public Collider[] SphereCastAll(Vector3 center, float radius)
    {
        return Physics.OverlapSphere(center, radius, physicsLayerMask);
    }
    
    /// <summary>
    /// 胶囊体检测
    /// </summary>
    /// <param name="point1">起点</param>
    /// <param name="point2">终点</param>
    /// <param name="radius">半径</param>
    /// <returns>检测结果</returns>
    public Collider[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius)
    {
        return Physics.OverlapCapsule(point1, point2, radius, physicsLayerMask);
    }
    
    /// <summary>
    /// 计算总动能
    /// </summary>
    private void CalculateTotalKineticEnergy()
    {
        totalKineticEnergy = 0f;
        
        foreach (var rigidbody in physicsObjects)
        {
            if (rigidbody != null)
            {
                // 平移动能
                float linearKE = 0.5f * rigidbody.mass * rigidbody.velocity.sqrMagnitude;
                
                // 转动动能
                float angularKE = 0.5f * rigidbody.mass * rigidbody.angularVelocity.sqrMagnitude;
                
                totalKineticEnergy += linearKE + angularKE;
            }
        }
    }
    
    /// <summary>
    /// 获取物理信息
    /// </summary>
    public void GetPhysicsInfo()
    {
        Debug.Log("=== 物理系统信息 ===");
        Debug.Log($"物理启用: {enablePhysics}");
        Debug.Log($"重力: {Physics.gravity}");
        Debug.Log($"物理模拟: {isSimulating}");
        Debug.Log($"模拟时间: {simulationTime:F2}s");
        Debug.Log($"活跃对象: {activeObjects}");
        Debug.Log($"总动能: {totalKineticEnergy:F2}");
        Debug.Log($"最大对象数: {maxPhysicsObjects}");
        Debug.Log($"碰撞检测: {enableCollisionDetection}");
        Debug.Log($"碰撞半径: {collisionRadius}");
        
        // 物理设置信息
        Debug.Log($"默认求解器迭代次数: {Physics.defaultSolverIterations}");
        Debug.Log($"默认求解器速度迭代次数: {Physics.defaultSolverVelocityIterations}");
        Debug.Log($"默认接触偏移: {Physics.defaultContactOffset}");
        Debug.Log($"默认睡眠阈值: {Physics.sleepThreshold}");
        Debug.Log($"默认最大角速度: {Physics.defaultMaxAngularSpeed}");
    }
    
    /// <summary>
    /// 重置物理系统
    /// </summary>
    public void ResetPhysicsSystem()
    {
        // 重置物理参数
        gravity = -9.81f;
        Physics.gravity = new Vector3(0, gravity, 0);
        
        // 重置对象参数
        bounceForce = 10f;
        dragForce = 0.5f;
        angularDrag = 0.05f;
        useGravity = true;
        
        // 重新配置所有对象
        foreach (var rigidbody in physicsObjects)
        {
            if (rigidbody != null)
            {
                ConfigureRigidbody(rigidbody);
            }
        }
        
        Debug.Log("物理系统已重置");
    }
    
    private void Update()
    {
        // 更新物理状态
        if (isSimulating)
        {
            simulationTime += Time.deltaTime;
        }
        
        // 计算总动能
        CalculateTotalKineticEnergy();
        
        // 更新活跃对象数量
        activeObjects = 0;
        foreach (var rigidbody in physicsObjects)
        {
            if (rigidbody != null && rigidbody.gameObject.activeInHierarchy)
            {
                activeObjects++;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"碰撞进入: {collision.gameObject.name}");
        onCollisionEnter?.Invoke(collision);
    }
    
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"碰撞离开: {collision.gameObject.name}");
        onCollisionExit?.Invoke(collision);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"触发器进入: {other.gameObject.name}");
        onTriggerEnter?.Invoke(other);
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"触发器离开: {other.gameObject.name}");
        onTriggerExit?.Invoke(other);
    }
    
    private void OnGUI()
    {
        // 简单的GUI界面用于测试
        GUILayout.BeginArea(new Rect(10, 10, 400, 600));
        GUILayout.Label("物理系统演示", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        // 物理状态
        GUILayout.Label($"物理模拟: {isSimulating}");
        GUILayout.Label($"活跃对象: {activeObjects}");
        GUILayout.Label($"总动能: {totalKineticEnergy:F2}");
        GUILayout.Label($"重力: {gravity:F2}");
        
        GUILayout.Space(10);
        
        // 控制按钮
        if (GUILayout.Button("添加物理对象"))
        {
            Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
            AddPhysicsObject(position);
        }
        
        if (GUILayout.Button("清除所有对象"))
        {
            ClearPhysicsObjects();
        }
        
        if (GUILayout.Button("应用爆炸力"))
        {
            ApplyExplosionForce(transform.position, 1000f, 10f);
        }
        
        if (GUILayout.Button("应用随机力"))
        {
            ApplyForceToAll(500f, ForceMode.Impulse);
        }
        
        GUILayout.Space(10);
        
        // 设置选项
        isSimulating = GUILayout.Toggle(isSimulating, "物理模拟");
        if (GUILayout.Button("设置物理模拟"))
        {
            SetPhysicsSimulation(isSimulating);
        }
        
        GUILayout.Label($"重力: {gravity:F2}");
        gravity = GUILayout.HorizontalSlider(gravity, -20f, 0f);
        if (GUILayout.Button("设置重力"))
        {
            SetGravity(gravity);
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("获取物理信息"))
        {
            GetPhysicsInfo();
        }
        
        if (GUILayout.Button("重置物理系统"))
        {
            ResetPhysicsSystem();
        }
        
        GUILayout.EndArea();
    }
    
    private void OnDrawGizmos()
    {
        // 绘制碰撞检测范围
        if (enableCollisionDetection)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + collisionCenter, collisionRadius);
        }
        
        // 绘制生成点
        if (spawnPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
        }
    }
} 