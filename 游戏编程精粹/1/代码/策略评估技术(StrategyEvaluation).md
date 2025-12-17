# 策略评估技术 - C#实现

## 核心概念

- **定义**：资源分配树、依存图等策略评估技术，用于RTS/策略游戏的AI决策
- **核心思想**：通过评估资源分配和依存关系，做出最优策略决策
- **应用场景**：RTS游戏的经济规划、资源分配、建筑顺序决策

## 资源分配树

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 资源分配树节点
/// </summary>
public class ResourceAllocationNode
{
    public string resourceType;     // 资源类型（木材、矿石、食物等）
    public float currentAmount;      // 当前数量
    public float desiredAmount;      // 期望数量
    public float priority;            // 优先级
    
    public List<ResourceAllocationNode> children;  // 子节点
    
    public ResourceAllocationNode(string type)
    {
        resourceType = type;
        currentAmount = 0f;
        desiredAmount = 0f;
        priority = 1f;
        children = new List<ResourceAllocationNode>();
    }
    
    /// <summary>
    /// 计算资源缺口
    /// </summary>
    public float GetDeficit()
    {
        return Mathf.Max(0f, desiredAmount - currentAmount);
    }
}
```

## 依存图

```csharp
/// <summary>
/// 依存图节点：表示游戏中的建筑、单位等
/// </summary>
public class DependencyNode
{
    public string name;              // 节点名称
    public List<string> dependencies;  // 依赖项（前置条件）
    public float value;              // 节点价值
    public bool isBuilt;              // 是否已建造
    
    public DependencyNode(string nodeName)
    {
        name = nodeName;
        dependencies = new List<string>();
        value = 0f;
        isBuilt = false;
    }
    
    /// <summary>
    /// 检查是否可以建造（所有依赖是否满足）
    /// </summary>
    public bool CanBuild(Dictionary<string, bool> builtNodes)
    {
        foreach (string dep in dependencies)
        {
            if (!builtNodes.ContainsKey(dep) || !builtNodes[dep])
                return false;
        }
        return true;
    }
}

/// <summary>
/// 依存图：管理所有节点的依赖关系
/// </summary>
public class DependencyGraph
{
    private Dictionary<string, DependencyNode> nodes;
    
    public DependencyGraph()
    {
        nodes = new Dictionary<string, DependencyNode>();
    }
    
    /// <summary>
    /// 添加节点
    /// </summary>
    public void AddNode(DependencyNode node)
    {
        nodes[node.name] = node;
    }
    
    /// <summary>
    /// 查找脆弱的依赖（如果被破坏会影响多个节点）
    /// </summary>
    public List<string> FindVulnerableDependencies()
    {
        Dictionary<string, int> dependencyCount = new Dictionary<string, int>();
        
        // 统计每个节点被依赖的次数
        foreach (var node in nodes.Values)
        {
            foreach (string dep in node.dependencies)
            {
                if (!dependencyCount.ContainsKey(dep))
                    dependencyCount[dep] = 0;
                dependencyCount[dep]++;
            }
        }
        
        // 找出被依赖次数最多的节点（最脆弱）
        List<string> vulnerable = new List<string>();
        int maxCount = 0;
        
        foreach (var kvp in dependencyCount)
        {
            if (kvp.Value > maxCount)
            {
                maxCount = kvp.Value;
                vulnerable.Clear();
                vulnerable.Add(kvp.Key);
            }
            else if (kvp.Value == maxCount)
            {
                vulnerable.Add(kvp.Key);
            }
        }
        
        return vulnerable;
    }
}
```

## 策略决策

```csharp
/// <summary>
/// 策略评估器：基于资源分配和依存关系做出决策
/// </summary>
public class StrategyEvaluator
{
    private ResourceAllocationTree resourceTree;
    private DependencyGraph dependencyGraph;
    
    /// <summary>
    /// 评估当前资源分配情况
    /// </summary>
    public float EvaluateResourceAllocation()
    {
        float score = 0f;
        
        // 遍历资源树，评估资源分配
        EvaluateNode(resourceTree.root, ref score);
        
        return score;
    }
    
    private void EvaluateNode(ResourceAllocationNode node, ref float score)
    {
        // 计算资源缺口（越小越好）
        float deficit = node.GetDeficit();
        float nodeScore = 1f / (1f + deficit) * node.priority;
        score += nodeScore;
        
        // 递归评估子节点
        foreach (var child in node.children)
        {
            EvaluateNode(child, ref score);
        }
    }
    
    /// <summary>
    /// 选择最佳建造顺序
    /// </summary>
    public List<string> GetOptimalBuildOrder()
    {
        List<string> buildOrder = new List<string>();
        Dictionary<string, bool> builtNodes = new Dictionary<string, bool>();
        
        // 贪心算法：每次选择价值最高且可建造的节点
        while (buildOrder.Count < nodes.Count)
        {
            DependencyNode bestNode = null;
            float bestValue = float.MinValue;
            
            foreach (var node in dependencyGraph.GetNodes())
            {
                if (node.isBuilt || !node.CanBuild(builtNodes))
                    continue;
                
                // 计算节点价值（考虑依赖关系）
                float value = CalculateNodeValue(node, builtNodes);
                
                if (value > bestValue)
                {
                    bestValue = value;
                    bestNode = node;
                }
            }
            
            if (bestNode != null)
            {
                buildOrder.Add(bestNode.name);
                builtNodes[bestNode.name] = true;
                bestNode.isBuilt = true;
            }
            else
            {
                break;  // 无法继续建造
            }
        }
        
        return buildOrder;
    }
    
    private float CalculateNodeValue(DependencyNode node, Dictionary<string, bool> builtNodes)
    {
        float value = node.value;
        
        // 如果依赖的节点还未建造，降低价值
        foreach (string dep in node.dependencies)
        {
            if (!builtNodes.ContainsKey(dep) || !builtNodes[dep])
                value *= 0.5f;  // 降低50%价值
        }
        
        return value;
    }
}
```

## Unity应用示例

```csharp
/// <summary>
/// Unity中使用策略评估
/// </summary>
public class StrategyEvaluatorUnity : MonoBehaviour
{
    private StrategyEvaluator evaluator;
    
    void Start()
    {
        evaluator = new StrategyEvaluator();
        // 初始化资源树和依存图
    }
    
    void Update()
    {
        // 每帧评估资源分配
        float allocationScore = evaluator.EvaluateResourceAllocation();
        
        // 如果资源分配不理想，调整策略
        if (allocationScore < 0.5f)
        {
            List<string> buildOrder = evaluator.GetOptimalBuildOrder();
            ExecuteBuildOrder(buildOrder);
        }
    }
}
```

