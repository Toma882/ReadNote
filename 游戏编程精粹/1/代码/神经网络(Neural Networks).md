# 神经网络（Neural Networks）- Hebbian学习算法演示

## 核心概念

**Hebbian学习规则**：一起激活的神经元会连接在一起（Neurons that fire together, wire together）

**基本公式**：

```text
Δw = η × x × y
```

其中：

- `Δw`：权重变化量
- `η`：学习率（通常0.01-0.1）
- `x`：输入值
- `y`：输出值

**直观理解**：

- 输入和输出同时激活（都为正）→ 权重增加（强化连接）
- 输入和输出不同时激活 → 权重减少（弱化连接）

## 神经单元（Neuron）实现

```csharp
using UnityEngine;
using System;

/// <summary>
/// 神经单元：神经网络的基本计算单元
/// </summary>
public class Neuron
{
    public float[] weights;  // 权重数组
    public float bias;        // 偏置值
    
    public Neuron(int inputCount)
    {
        weights = new float[inputCount];
        bias = 0f;
        
        // 随机初始化权重
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = UnityEngine.Random.Range(-0.5f, 0.5f);
        }
    }
    
    /// <summary>
    /// 计算输出：output = Σ(input × weight) + bias
    /// </summary>
    public float Compute(float[] inputs)
    {
        if (inputs.Length != weights.Length)
        {
            throw new ArgumentException("输入数量与权重数量不匹配");
        }
        
        float sum = 0f;
        for (int i = 0; i < inputs.Length; i++)
        {
            sum += inputs[i] * weights[i];
        }
        sum += bias;
        
        // 使用Sigmoid激活函数
        return 1f / (1f + Mathf.Exp(-sum));
    }
}
```

## Hebbian学习实现

```csharp
/// <summary>
/// Hebbian学习：无监督学习算法
/// </summary>
public class HebbianLearning
{
    /// <summary>
    /// 更新权重：Δw = η × x × y
    /// </summary>
    public static void UpdateWeight(
        ref float weight, 
        float input, 
        float output, 
        float learningRate = 0.1f)
    {
        // Hebbian规则：权重变化 = 学习率 × 输入 × 输出
        float deltaWeight = learningRate * input * output;
        weight += deltaWeight;
    }
    
    /// <summary>
    /// 训练神经元（Hebbian学习）
    /// </summary>
    public static void TrainNeuron(
        Neuron neuron, 
        float[] inputs, 
        float learningRate = 0.1f)
    {
        // 步骤1：计算输出
        float output = neuron.Compute(inputs);
        
        // 步骤2：更新每个权重（使用Hebbian规则）
        for (int i = 0; i < neuron.weights.Length; i++)
        {
            UpdateWeight(
                ref neuron.weights[i], 
                inputs[i], 
                output, 
                learningRate
            );
        }
        
        // 步骤3：更新偏置
        float biasDelta = learningRate * 1f * output;
        neuron.bias += biasDelta;
    }
}
```

## 游戏AI应用示例

### 示例：NPC行为关联学习

```csharp
/// <summary>
/// NPC行为关联学习：学习环境状态与行为的关联
/// </summary>
public class NPCBehaviorAssociation
{
    private Neuron[] behaviorNeurons;  // 每个行为对应一个神经元
    
    public NPCBehaviorAssociation()
    {
        // 初始化行为神经元（输入：环境状态，输出：行为激活度）
        behaviorNeurons = new Neuron[]
        {
            new Neuron(4),  // 攻击行为（输入：血量、距离、盟友数、弹药）
            new Neuron(4),  // 防御行为
            new Neuron(4),  // 逃跑行为
            new Neuron(4)   // 待机行为
        };
    }
    
    /// <summary>
    /// 学习：当NPC执行某个行为时，使用Hebbian学习强化相关输入
    /// </summary>
    public void Learn(float[] environmentState, BehaviorType executedBehavior)
    {
        int behaviorIndex = (int)executedBehavior;
        Neuron behaviorNeuron = behaviorNeurons[behaviorIndex];
        
        // 使用Hebbian学习：强化环境状态与行为的关联
        HebbianLearning.TrainNeuron(behaviorNeuron, environmentState, 0.1f);
    }
    
    /// <summary>
    /// 决策：根据学习到的关联选择行为
    /// </summary>
    public BehaviorType Decide(float[] environmentState)
    {
        float maxOutput = float.MinValue;
        int bestBehavior = 0;
        
        // 计算每个行为的激活度
        for (int i = 0; i < behaviorNeurons.Length; i++)
        {
            float output = behaviorNeurons[i].Compute(environmentState);
            if (output > maxOutput)
            {
                maxOutput = output;
                bestBehavior = i;
            }
        }
        
        return (BehaviorType)bestBehavior;
    }
}

public enum BehaviorType
{
    Attack,   // 攻击
    Defend,   // 防御
    Flee,     // 逃跑
    Idle      // 待机
}
```

### 使用示例

```csharp
// 创建NPC行为学习系统
NPCBehaviorAssociation npcAI = new NPCBehaviorAssociation();

// 环境状态：[血量归一化, 敌人距离归一化, 盟友数归一化, 弹药归一化]
float[] state1 = new float[] { 0.3f, 0.1f, 0.2f, 0.5f };  // 低血量、敌人近
npcAI.Learn(state1, BehaviorType.Flee);  // 学习：这种状态下应该逃跑

float[] state2 = new float[] { 0.8f, 0.2f, 0.6f, 0.9f };  // 高血量、有盟友、弹药充足
npcAI.Learn(state2, BehaviorType.Attack);  // 学习：这种状态下应该攻击

// 决策
float[] currentState = new float[] { 0.4f, 0.15f, 0.3f, 0.6f };
BehaviorType decision = npcAI.Decide(currentState);  // 根据学习到的关联选择行为
```

## 总结

**Hebbian学习算法**的核心思想是"一起激活的神经元会连接在一起"。它不需要目标输出，可以实时学习，非常适合游戏AI中的行为关联学习场景。
