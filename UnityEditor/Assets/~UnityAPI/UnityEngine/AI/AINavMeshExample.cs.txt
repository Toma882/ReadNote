using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// UnityEngine.AI 命名空间案例演示
/// 展示NavMeshAgent、NavMeshObstacle等AI导航核心功能
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AINavMeshExample : MonoBehaviour
{
    [Header("导航设置")]
    [SerializeField] private Transform target; //目标位置
    [SerializeField] private bool autoUpdateDestination = true; //是否自动更新目标位置
    [SerializeField] private float updateInterval = 0.5f; //更新间隔
    [SerializeField] private bool enableObstacle = false; //是否启用障碍
    [SerializeField] private float obstacleRadius = 0.5f; //障碍半径
    [SerializeField] private float obstacleHeight = 2f; //障碍高度

    private NavMeshAgent agent; //导航代理
    private NavMeshObstacle obstacle; //障碍
    private float timer; //计时器

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (enableObstacle)
        {
            obstacle = gameObject.AddComponent<NavMeshObstacle>();
            obstacle.radius = obstacleRadius;
            obstacle.height = obstacleHeight;
            obstacle.carving = true;
        }
    }

    private void Start()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void Update()
    {
        if (autoUpdateDestination && target != null)
        {
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                agent.SetDestination(target.position);
                timer = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.green;
            var path = agent.path;
            Vector3 prev = agent.transform.position;
            foreach (var corner in path.corners)
            {
                Gizmos.DrawLine(prev, corner);
                prev = corner;
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 300));
        GUILayout.Label("AI导航演示 (NavMeshAgent)", UnityEditor.EditorStyles.boldLabel);
        GUILayout.Label($"当前位置: {transform.position}");
        if (target != null)
        {
            GUILayout.Label($"目标位置: {target.position}");
        }
        GUILayout.Label($"是否启用障碍: {enableObstacle}");
        GUILayout.Label($"自动更新目标: {autoUpdateDestination}");
        if (GUILayout.Button("立即寻路到目标") && target != null)
        {
            agent.SetDestination(target.position);
        }
        if (GUILayout.Button("停止寻路"))
        {
            agent.isStopped = true;
        }
        if (GUILayout.Button("恢复寻路"))
        {
            agent.isStopped = false;
        }
        GUILayout.EndArea();
    }
} 