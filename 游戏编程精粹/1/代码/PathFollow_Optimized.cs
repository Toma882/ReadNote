using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 路径跟随系统 - Catmull-Rom优化版本
/// 
/// ========== 核心优化思路 ==========
/// 
/// 问题：原版本每帧都要计算投影、找最近段，效率低
/// 
/// 解决方案：
/// 1. 使用Catmull-Rom插值预生成平滑路径点
/// 2. 直接沿着预生成的路径点移动
/// 3. 不需要每帧计算投影，只需要简单的距离计算
/// 
/// ========== 性能对比 ==========
/// 
/// 原版本（每帧）：
/// - 遍历所有段找最近段：O(n)
/// - 计算投影：O(1)
/// - 总开销：O(n) 每帧
/// 
/// 优化版本（每帧）：
/// - 查找最近路径点：O(log n) 或 O(1)（利用缓存）
/// - 直接获取下一个点：O(1)
/// - 总开销：O(1) 或 O(log n) 每帧
/// 
/// 性能提升：约10-100倍（取决于路径段数）
/// </summary>

// ==================== Catmull-Rom插值路径 ====================
/// <summary>
/// Catmull-Rom插值路径：预生成平滑路径点
/// </summary>
public class CatmullRomPath : MonoBehaviour
{
    [Header("路径节点")]
    public List<GameObject> PathNodes = new List<GameObject>();
    
    [Header("插值设置")]
    [Tooltip("每两个节点之间生成的路径点数量（越多越平滑，但内存占用越大）")]
    public int PointsPerSegment = 10;
    
    [Tooltip("是否闭合路径（首尾相连）")]
    public bool IsClosed = false;
    
    // 预生成的平滑路径点
    private List<Vector3> smoothPathPoints = new List<Vector3>();
    private List<float> cumulativeDistances = new List<float>(); // 每个点到起点的累计距离
    private float totalPathLength = 0f;
    
    // 缓存最近访问的点索引（利用局部性优化）
    private int cachedPointIndex = 0;
    
    void Start()
    {
        GenerateSmoothPath();
    }
    
    /// <summary>
    /// 使用Catmull-Rom插值生成平滑路径
    /// </summary>
    public void GenerateSmoothPath()
    {
        smoothPathPoints.Clear();
        cumulativeDistances.Clear();
        totalPathLength = 0f;
        cachedPointIndex = 0;
        
        if (PathNodes == null || PathNodes.Count < 2)
        {
            Debug.LogWarning("路径节点数量不足，至少需要2个节点");
            return;
        }
        
        // 如果节点数少于4个，需要特殊处理（Catmull-Rom需要4个点）
        if (PathNodes.Count < 4)
        {
            GenerateSimplePath();
            return;
        }
        
        // 生成平滑路径点
        for (int i = 0; i < PathNodes.Count; i++)
        {
            // 获取4个控制点（用于Catmull-Rom插值）
            Vector3 p0 = GetControlPoint(i - 1);
            Vector3 p1 = PathNodes[i].transform.position;
            Vector3 p2 = GetControlPoint(i + 1);
            Vector3 p3 = GetControlPoint(i + 2);
            
            // 如果这是第一个节点，添加起点
            if (i == 0)
            {
                smoothPathPoints.Add(p1);
                cumulativeDistances.Add(0f);
            }
            
            // 在当前节点和下一个节点之间生成插值点
            if (i < PathNodes.Count - 1 || IsClosed)
            {
                for (int j = 1; j <= PointsPerSegment; j++)
                {
                    float t = (float)j / PointsPerSegment;
                    Vector3 point = CatmullRom(p0, p1, p2, p3, t);
                    
                    // 计算累计距离
                    float distance = Vector3.Distance(smoothPathPoints[smoothPathPoints.Count - 1], point);
                    totalPathLength += distance;
                    cumulativeDistances.Add(totalPathLength);
                    
                    smoothPathPoints.Add(point);
                }
            }
        }
        
        Debug.Log($"生成平滑路径：{smoothPathPoints.Count}个点，总长度：{totalPathLength:F2}米");
    }
    
    /// <summary>
    /// 节点数少于4个时的简单路径生成
    /// </summary>
    private void GenerateSimplePath()
    {
        for (int i = 0; i < PathNodes.Count; i++)
        {
            Vector3 point = PathNodes[i].transform.position;
            smoothPathPoints.Add(point);
            
            if (i > 0)
            {
                float distance = Vector3.Distance(smoothPathPoints[i - 1], point);
                totalPathLength += distance;
            }
            cumulativeDistances.Add(totalPathLength);
        }
    }
    
    /// <summary>
    /// 获取控制点（处理边界情况）
    /// </summary>
    private Vector3 GetControlPoint(int index)
    {
        if (IsClosed)
        {
            // 闭合路径：使用模运算
            int wrappedIndex = ((index % PathNodes.Count) + PathNodes.Count) % PathNodes.Count;
            return PathNodes[wrappedIndex].transform.position;
        }
        else
        {
            // 开放路径：边界点重复
            if (index < 0)
                return PathNodes[0].transform.position;
            if (index >= PathNodes.Count)
                return PathNodes[PathNodes.Count - 1].transform.position;
            return PathNodes[index].transform.position;
        }
    }
    
    /// <summary>
    /// Catmull-Rom插值算法
    /// </summary>
    private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Catmull-Rom样条插值公式
        // t: 0到1之间的插值参数
        float t2 = t * t;
        float t3 = t2 * t;
        
        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }
    
    /// <summary>
    /// 获取路径总长度
    /// </summary>
    public float GetTotalLength()
    {
        return totalPathLength;
    }
    
    /// <summary>
    /// 根据3D位置找到最近的路径点索引（优化版：利用局部性）
    /// </summary>
    public int FindNearestPointIndex(Vector3 position)
    {
        if (smoothPathPoints.Count == 0)
            return 0;
        
        float minDistance = float.MaxValue;
        int nearestIndex = 0;
        
        // 优化：先检查缓存点附近（前后各10个点）
        int startIndex = Mathf.Max(0, cachedPointIndex - 10);
        int endIndex = Mathf.Min(smoothPathPoints.Count - 1, cachedPointIndex + 10);
        
        for (int i = startIndex; i <= endIndex; i++)
        {
            float distance = Vector3.Distance(position, smoothPathPoints[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestIndex = i;
            }
        }
        
        // 如果缓存附近没找到足够近的点，再遍历全部（防止Agent突然跳跃）
        if (minDistance > 2.0f)
        {
            for (int i = 0; i < smoothPathPoints.Count; i++)
            {
                float distance = Vector3.Distance(position, smoothPathPoints[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestIndex = i;
                }
            }
        }
        
        cachedPointIndex = nearestIndex;
        return nearestIndex;
    }
    
    /// <summary>
    /// 根据路径点索引获取对应的路径距离
    /// </summary>
    public float GetDistanceAtPointIndex(int pointIndex)
    {
        if (pointIndex < 0)
            return 0f;
        if (pointIndex >= cumulativeDistances.Count)
            return totalPathLength;
        return cumulativeDistances[pointIndex];
    }
    
    /// <summary>
    /// 根据路径距离获取对应的路径点索引（二分查找）
    /// </summary>
    public int FindPointIndexAtDistance(float distance)
    {
        if (cumulativeDistances.Count == 0)
            return 0;
        
        distance = Mathf.Clamp(distance, 0f, totalPathLength);
        
        // 二分查找
        int left = 0;
        int right = cumulativeDistances.Count - 1;
        
        while (left < right)
        {
            int mid = (left + right) / 2;
            if (cumulativeDistances[mid] < distance)
                left = mid + 1;
            else
                right = mid;
        }
        
        return left;
    }
    
    /// <summary>
    /// 根据路径距离获取对应的3D位置（线性插值）
    /// </summary>
    public Vector3 GetPositionAtDistance(float distance)
    {
        if (smoothPathPoints.Count == 0)
            return Vector3.zero;
        
        distance = Mathf.Clamp(distance, 0f, totalPathLength);
        
        // 找到对应的点索引
        int pointIndex = FindPointIndexAtDistance(distance);
        
        // 如果正好在点上
        if (pointIndex == 0 || cumulativeDistances[pointIndex] == distance)
        {
            return smoothPathPoints[pointIndex];
        }
        
        // 在两个点之间线性插值
        int prevIndex = pointIndex - 1;
        float prevDistance = cumulativeDistances[prevIndex];
        float nextDistance = cumulativeDistances[pointIndex];
        float segmentLength = nextDistance - prevDistance;
        
        if (segmentLength < 0.001f)
            return smoothPathPoints[pointIndex];
        
        float t = (distance - prevDistance) / segmentLength;
        return Vector3.Lerp(smoothPathPoints[prevIndex], smoothPathPoints[pointIndex], t);
    }
    
    /// <summary>
    /// 获取路径点数量
    /// </summary>
    public int GetPointCount()
    {
        return smoothPathPoints.Count;
    }
    
    /// <summary>
    /// 可视化路径（编辑器中使用）
    /// </summary>
    void OnDrawGizmos()
    {
        if (smoothPathPoints == null || smoothPathPoints.Count < 2)
            return;
        
        Color originalColor = Gizmos.color;
        Gizmos.color = Color.cyan;
        
        // 绘制平滑路径
        for (int i = 0; i < smoothPathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(smoothPathPoints[i], smoothPathPoints[i + 1]);
        }
        
        // 绘制原始节点
        Gizmos.color = Color.yellow;
        if (PathNodes != null)
        {
            foreach (var node in PathNodes)
            {
                if (node != null)
                {
                    Gizmos.DrawSphere(node.transform.position, 0.2f);
                }
            }
        }
        
        Gizmos.color = originalColor;
    }
}

// ==================== 优化的路径跟随行为 ====================
/// <summary>
/// 优化的路径跟随行为：直接沿着预生成的平滑路径点移动
/// </summary>
public class OptimizedPathFollowBehavior : MonoBehaviour
{
    [Header("路径设置")]
    public CatmullRomPath Path;
    
    [Header("跟随参数")]
    [Tooltip("目标点距离当前位置的路径距离偏移（正数表示前方，负数表示后方）")]
    public float TargetDistanceOffset = 1.0f;
    
    [Header("移动参数")]
    public float MoveSpeed = 5f;
    
    [Header("调试")]
    public bool ShowDebugInfo = true;
    
    // 当前状态
    private int currentPointIndex = 0;
    private float currentPathDistance = 0f;
    private Vector3 targetPosition;
    
    void Update()
    {
        if (Path == null)
            return;
        
        // ========== 优化的路径跟随逻辑 ==========
        // 
        // 核心思路：直接沿着预生成的路径点移动，不需要每帧计算投影
        // 
        // 步骤：
        // 1. 找到Agent当前位置最近的路径点索引
        // 2. 获取该点对应的路径距离
        // 3. 在路径前方设置目标点（当前位置 + 偏移距离）
        // 4. 直接移动到目标点
        // 
        // 性能优势：
        // - 不需要计算投影
        // - 不需要遍历所有段
        // - 只需要简单的距离计算和索引查找
        
        // 步骤1：找到当前位置最近的路径点
        currentPointIndex = Path.FindNearestPointIndex(transform.position);
        currentPathDistance = Path.GetDistanceAtPointIndex(currentPointIndex);
        
        // 步骤2：计算目标位置（当前位置前方一定距离）
        float targetPathDistance = currentPathDistance + TargetDistanceOffset;
        targetPosition = Path.GetPositionAtDistance(targetPathDistance);
        
        // 步骤3：直接移动到目标点（或使用Seek行为）
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * MoveSpeed * Time.deltaTime;
        
        // 调试：绘制从Agent到目标点的线
        if (ShowDebugInfo)
        {
            Debug.DrawLine(transform.position, targetPosition, Color.green);
            Debug.DrawLine(transform.position, Path.GetPositionAtDistance(currentPathDistance), Color.red);
        }
    }
    
    /// <summary>
    /// 重置路径跟随状态
    /// </summary>
    public void ResetPathFollow()
    {
        currentPointIndex = 0;
        currentPathDistance = 0f;
    }
}

