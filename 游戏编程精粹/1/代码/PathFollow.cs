using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 路径跟随系统 - 简化易懂版本
/// 
/// ========== 核心概念 ==========
/// 
/// 1. 路径距离（Path Distance）：
///    - 从路径起点开始，沿着路径到某个位置的距离
///    - 例如：路径 A→B→C，如果B在路径上3米，C在路径上7米
/// 
/// 2. 路径段（Path Segment）：
///    - 路径由多个段组成，每段连接两个节点
///    - 例如：A→B→C 有两个段：段1(A→B)和段2(B→C)
/// 
/// 3. 路径跟随的工作原理：
///    - Agent当前位置 → 找到在路径上的距离（比如5米）
///    - 设置目标点 → 路径上前方一定距离（比如7米）
///    - Agent朝着目标点移动 → 从而沿着路径前进
/// 
/// ========== 使用示例 ==========
/// 
/// 1. 创建路径：
///    PathManager path = gameObject.AddComponent<PathManager>();
///    path.PathNodes = [node1, node2, node3]; // 设置路径节点
/// 
/// 2. 添加路径跟随：
///    PathFollowBehavior follower = agent.AddComponent<PathFollowBehavior>();
///    follower.Path = path;
///    follower.TargetDistanceOffset = 2.0f; // 目标点在前方2米
/// 
/// ========== 关键方法说明 ==========
/// 
/// GetPathDistance(position)：
///   - 输入：3D世界坐标
///   - 输出：这个位置在路径上的距离
///   - 原理：找到最近的路径段，将位置投影到段上，计算距离
/// 
/// GetPositionAtDistance(distance)：
///   - 输入：路径距离（比如5米）
///   - 输出：路径上这个距离对应的3D坐标
///   - 原理：找到对应的段，计算段内的位置
/// </summary>

// ==================== 路径段 ====================
/// <summary>
/// 路径段：表示路径上的一段，从起点到终点
/// </summary>
public class PathSegment
{
    public Vector3 StartPoint { get; private set; }
    public Vector3 EndPoint { get; private set; }
    public Vector3 Direction { get; private set; }
    public float Length { get; private set; }
    
    public PathSegment(Vector3 start, Vector3 end)
    {
        StartPoint = start;
        EndPoint = end;
        Direction = (end - start).normalized;
        Length = Vector3.Distance(start, end);
    }
}

// ==================== 路径管理 ====================
/// <summary>
/// 路径类：管理由多个节点组成的路径
/// 优化：预计算累计距离，使用二分查找快速定位段
/// </summary>
public class PathManager : MonoBehaviour
{
    [Header("路径节点")]
    public List<GameObject> PathNodes = new List<GameObject>();
    
    // 内部数据
    private List<PathSegment> pathSegments = new List<PathSegment>();
    private List<float> cumulativeDistances = new List<float>(); // 预计算：每个段结束时的累计距离
    private float totalPathLength = 0f;
    
    // 缓存最近访问的段索引（利用局部性优化）
    private int cachedSegmentIndex = 0;
    
    void Start()
    {
        BuildPath();
    }
    
    /// <summary>
    /// 构建路径：从节点生成路径段，并预计算累计距离
    /// </summary>
    public void BuildPath()
    {
        pathSegments.Clear();
        cumulativeDistances.Clear();
        totalPathLength = 0f;
        cachedSegmentIndex = 0;
        
        if (PathNodes == null || PathNodes.Count < 2)
        {
            Debug.LogWarning("路径节点数量不足，至少需要2个节点");
            return;
        }
        
        // 生成路径段
        for (int i = 0; i < PathNodes.Count - 1; i++)
        {
            Vector3 start = PathNodes[i].transform.position;
            Vector3 end = PathNodes[i + 1].transform.position;
            PathSegment segment = new PathSegment(start, end);
            pathSegments.Add(segment);
            
            // 预计算累计距离
            totalPathLength += segment.Length;
            cumulativeDistances.Add(totalPathLength);
        }
    }
    
    /// <summary>
    /// 获取路径总长度
    /// </summary>
    public float GetTotalLength()
    {
        return totalPathLength;
    }
    
    /// <summary>
    /// 根据3D位置获取其在路径上的距离（从起点开始沿着路径的距离）
    /// 
    /// 核心概念：
    /// - 路径距离：从路径起点开始，沿着路径到某个位置的距离
    /// - 例如：路径 A→B→C，如果位置在B和C之间，距离 = AB长度 + 从B到该位置的距离
    /// 
    /// 实现思路：
    /// 1. 找到位置最近的路径段
    /// 2. 将位置投影到该段上（找到段上最近的点）
    /// 3. 计算：前面所有段的长度 + 当前段内的距离
    /// </summary>
    public float GetPathDistance(Vector3 position, float lastPathDistance = 0f)
    {
        if (pathSegments.Count == 0)
            return 0f;
        
        // 优化：利用局部性，Agent通常只在相邻段移动
        // 先检查缓存的段附近（前后各2个段），如果没找到再遍历全部
        
        float minDistance = float.MaxValue;
        int nearestSegmentIndex = 0;
        float nearestDistanceOnPath = 0f;
        
        // 优化1：先检查缓存段附近（利用局部性）
        int startIndex = Mathf.Max(0, cachedSegmentIndex - 2);
        int endIndex = Mathf.Min(pathSegments.Count - 1, cachedSegmentIndex + 2);
        
        float accumulatedDistance = 0f;
        // 先计算到startIndex的累计距离
        for (int i = 0; i < startIndex; i++)
        {
            accumulatedDistance += pathSegments[i].Length;
        }
        
        // 检查缓存附近的段
        for (int i = startIndex; i <= endIndex; i++)
        {
            PathSegment segment = pathSegments[i];
            
            Vector3 toPosition = position - segment.StartPoint;
            Vector3 projection = Vector3.Project(toPosition, segment.Direction);
            float distanceInSegment = Mathf.Clamp(projection.magnitude, 0f, segment.Length);
            Vector3 nearestPointOnSegment = segment.StartPoint + segment.Direction * distanceInSegment;
            float distanceToSegment = Vector3.Distance(position, nearestPointOnSegment);
            
            if (distanceToSegment < minDistance)
            {
                minDistance = distanceToSegment;
                nearestSegmentIndex = i;
                nearestDistanceOnPath = accumulatedDistance + distanceInSegment;
            }
            
            accumulatedDistance += segment.Length;
        }
        
        // 优化2：如果缓存附近没找到足够近的段，再遍历全部（防止Agent突然跳跃）
        // 但只在距离阈值较大时才需要（比如距离 > 1米）
        if (minDistance > 1.0f)
        {
            accumulatedDistance = 0f;
            for (int i = 0; i < pathSegments.Count; i++)
            {
                PathSegment segment = pathSegments[i];
                
                Vector3 toPosition = position - segment.StartPoint;
                Vector3 projection = Vector3.Project(toPosition, segment.Direction);
                float distanceInSegment = Mathf.Clamp(projection.magnitude, 0f, segment.Length);
                Vector3 nearestPointOnSegment = segment.StartPoint + segment.Direction * distanceInSegment;
                float distanceToSegment = Vector3.Distance(position, nearestPointOnSegment);
                
                if (distanceToSegment < minDistance)
                {
                    minDistance = distanceToSegment;
                    nearestSegmentIndex = i;
                    nearestDistanceOnPath = accumulatedDistance + distanceInSegment;
                }
                
                accumulatedDistance += segment.Length;
            }
        }
        
        // 更新缓存
        cachedSegmentIndex = nearestSegmentIndex;
        
        return nearestDistanceOnPath;
    }
    
    /// <summary>
    /// 根据路径距离获取对应的3D位置
    /// 
    /// 功能：给定一个路径距离（比如5.5米），返回路径上这个距离对应的3D世界坐标
    /// 
    /// 示例：
    /// 路径：A(0,0,0) → B(3,0,0) → C(3,4,0)
    /// 段长度：[3, 4]，累计距离：[3, 7]
    /// 如果pathDistance = 5.5，那么：
    /// - 它在第2段（B→C）上
    /// - 段内距离 = 5.5 - 3 = 2.5
    /// - 位置 = B + 方向 * 2.5 = (3, 2.5, 0)
    /// </summary>
    public Vector3 GetPositionAtDistance(float pathDistance)
    {
        if (pathSegments.Count == 0)
            return Vector3.zero;
        
        // 限制距离范围（不能超出路径）
        pathDistance = Mathf.Clamp(pathDistance, 0f, totalPathLength);
        
        // 步骤1：找到这个距离在哪个路径段上
        int segmentIndex = FindSegmentIndex(pathDistance);
        if (segmentIndex < 0 || segmentIndex >= pathSegments.Count)
            return pathSegments[pathSegments.Count - 1].EndPoint;
        
        PathSegment segment = pathSegments[segmentIndex];
        
        // 步骤2：计算在这个段内的距离
        // 例如：段索引=1，累计距离=[3, 7]，pathDistance=5.5
        // distanceToSegmentStart = 3（到段起点的累计距离）
        // distanceInSegment = 5.5 - 3 = 2.5（在段内的距离）
        float distanceToSegmentStart = segmentIndex > 0 ? cumulativeDistances[segmentIndex - 1] : 0f;
        float distanceInSegment = pathDistance - distanceToSegmentStart;
        
        // 步骤3：计算3D位置
        // 位置 = 段起点 + 段方向 * 段内距离
        Vector3 position = segment.StartPoint + segment.Direction * distanceInSegment;
        
        return position;
    }
    
    /// <summary>
    /// 查找包含指定路径距离的段索引
    /// 
    /// 功能：给定一个路径距离（比如5.5米），找到这个距离在哪个路径段上
    /// 
    /// 示例：
    /// 路径：A→B→C，段长度：[3, 4, 5]
    /// 累计距离：[3, 7, 12]
    /// 如果pathDistance = 5.5，那么它在第2段（B→C）上，因为 3 < 5.5 < 7
    /// </summary>
    private int FindSegmentIndex(float pathDistance)
    {
        if (pathSegments.Count == 0)
            return -1;
        
        // 限制距离范围
        pathDistance = Mathf.Clamp(pathDistance, 0f, totalPathLength);
        
        // 简单实现：线性查找（容易理解）
        // 对于路径跟随，通常段数不多（10-20个），线性查找足够快
        float accumulatedDistance = 0f;
        for (int i = 0; i < pathSegments.Count; i++)
        {
            float segmentLength = pathSegments[i].Length;
            float segmentEndDistance = accumulatedDistance + segmentLength;
            
            // 如果距离在这个段的范围内
            if (pathDistance >= accumulatedDistance && pathDistance <= segmentEndDistance)
            {
                cachedSegmentIndex = i; // 更新缓存
                return i;
            }
            
            accumulatedDistance = segmentEndDistance;
        }
        
        // 边界情况：返回最后一个段
        return pathSegments.Count - 1;
    }
    
    /// <summary>
    /// 可视化路径（编辑器中使用）
    /// </summary>
    void OnDrawGizmos()
    {
        if (PathNodes == null || PathNodes.Count < 2)
            return;
        
        Color originalColor = Gizmos.color;
        Gizmos.color = Color.magenta;
        
        for (int i = 0; i < PathNodes.Count - 1; i++)
        {
            if (PathNodes[i] != null && PathNodes[i + 1] != null)
            {
                Vector3 start = PathNodes[i].transform.position;
                Vector3 end = PathNodes[i + 1].transform.position;
                Gizmos.DrawLine(start, end);
            }
        }
        
        Gizmos.color = originalColor;
    }
}

// ==================== 路径跟随行为 ====================
/// <summary>
/// 路径跟随行为：让Agent沿着路径移动
/// 继承自Seek，通过动态更新目标位置实现路径跟随
/// </summary>
public class PathFollowBehavior : MonoBehaviour
{
    [Header("路径设置")]
    public PathManager Path;
    
    [Header("跟随参数")]
    [Tooltip("目标点距离当前位置的路径距离偏移（正数表示前方，负数表示后方）")]
    public float TargetDistanceOffset = 1.0f;
    
    [Header("调试")]
    public bool ShowDebugInfo = true;
    
    // 当前状态
    private float currentPathDistance = 0f;
    private Vector3 targetPosition;
    private SeekBehavior seekBehavior;
    
    void Awake()
    {
        seekBehavior = GetComponent<SeekBehavior>();
        if (seekBehavior == null)
        {
            Debug.LogError("PathFollowBehavior需要SeekBehavior组件");
        }
        
        // 创建目标对象
        GameObject targetObj = new GameObject("PathFollowTarget");
        targetObj.transform.SetParent(transform);
        targetPosition = transform.position;
    }
    
    void Update()
    {
        if (Path == null || seekBehavior == null)
            return;
        
        // ========== 路径跟随的核心逻辑 ==========
        // 
        // 思路：让Agent始终朝着路径前方的一个点移动
        // 
        // 步骤：
        // 1. 找到Agent当前位置在路径上的位置（路径距离）
        // 2. 在路径前方设置一个目标点（当前位置 + 偏移距离）
        // 3. 让Agent朝着这个目标点移动
        // 
        // 示例：
        // 路径：A→B→C，总长度10米
        // Agent在路径上5米的位置
        // 目标点设置在路径上7米的位置（5 + 2偏移）
        // Agent就会朝着7米位置移动，从而沿着路径前进
        
        // 步骤1：获取当前位置在路径上的距离
        // 例如：如果Agent在路径上5米的位置，currentPathDistance = 5
        currentPathDistance = Path.GetPathDistance(transform.position, currentPathDistance);
        
        // 步骤2：计算目标位置（当前位置前方一定距离）
        // 例如：currentPathDistance = 5, TargetDistanceOffset = 2
        // 那么targetPathDistance = 7，目标点在路径上7米的位置
        float targetPathDistance = currentPathDistance + TargetDistanceOffset;
        targetPosition = Path.GetPositionAtDistance(targetPathDistance);
        
        // 步骤3：让Agent朝着目标点移动（使用Seek行为）
        seekBehavior.SetTarget(targetPosition);
        
        // 调试：绘制从Agent到目标点的线
        if (ShowDebugInfo)
        {
            Debug.DrawLine(transform.position, targetPosition, Color.green);
        }
    }
    
    /// <summary>
    /// 重置路径跟随状态
    /// </summary>
    public void ResetPathFollow()
    {
        currentPathDistance = 0f;
    }
}

// ==================== Seek行为基类（简化版） ====================
/// <summary>
/// 寻找行为：移动到目标位置
/// </summary>
public class SeekBehavior : MonoBehaviour
{
    [Header("移动参数")]
    public float MaxAcceleration = 10f;
    public float MaxSpeed = 5f;
    
    private Vector3 targetPosition;
    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }
    
    /// <summary>
    /// 设置目标位置
    /// </summary>
    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }
    
    void FixedUpdate()
    {
        if (rb == null)
            return;
        
        // 计算到目标的方向
        Vector3 direction = targetPosition - transform.position;
        float distance = direction.magnitude;
        
        if (distance < 0.1f)
            return;
        
        // 计算期望速度
        direction.Normalize();
        Vector3 desiredVelocity = direction * MaxSpeed;
        
        // 计算转向力
        Vector3 steering = desiredVelocity - rb.velocity;
        steering = Vector3.ClampMagnitude(steering, MaxAcceleration);
        
        // 应用力
        rb.AddForce(steering, ForceMode.Acceleration);
    }
}

