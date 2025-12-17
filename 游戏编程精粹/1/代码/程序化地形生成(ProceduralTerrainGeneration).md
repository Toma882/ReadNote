# 程序化地形生成（Procedural Terrain Generation）- C#实现

## 核心概念

- **定义**：使用算法自动生成地形高度图，实现程序化地形创建
- **核心思想**：通过数学算法模拟自然地形形成过程，生成真实感的地形
- **应用场景**：开放世界游戏、程序化生成游戏、地形编辑器
- **Unity应用**：配合Unity Terrain系统使用，生成高度图数据

## 基础数据结构

```csharp
using UnityEngine;

/// <summary>
/// 地形生成器基类：提供统一接口
/// </summary>
public abstract class TerrainGenerator
{
    protected int width;      // 地形宽度（顶点数）
    protected int height;     // 地形高度（顶点数）
    protected float[,] heightMap;  // 高度图数据
    
    public TerrainGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.heightMap = new float[width, height];
    }
    
    /// <summary>
    /// 生成高度图（子类实现具体算法）
    /// </summary>
    public abstract float[,] Generate();
    
    /// <summary>
    /// 将高度图应用到Unity Terrain
    /// </summary>
    public void ApplyToTerrain(Terrain terrain, float maxHeight = 100f)
    {
        TerrainData terrainData = terrain.terrainData;
        terrainData.heightmapResolution = width;
        terrainData.size = new Vector3(width, maxHeight, height);
        
        // 归一化高度图到0-1范围
        float[,] normalizedHeights = NormalizeHeightMap(heightMap);
        terrainData.SetHeights(0, 0, normalizedHeights);
    }
    
    /// <summary>
    /// 归一化高度图到0-1范围
    /// </summary>
    protected float[,] NormalizeHeightMap(float[,] heights)
    {
        float min = float.MaxValue;
        float max = float.MinValue;
        
        // 找到最小值和最大值
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (heights[x, z] < min) min = heights[x, z];
                if (heights[x, z] > max) max = heights[x, z];
            }
        }
        
        // 归一化
        float[,] normalized = new float[width, height];
        float range = max - min;
        if (range > 0)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    normalized[x, z] = (heights[x, z] - min) / range;
                }
            }
        }
        
        return normalized;
    }
}
```

---

## 4.17 断层构造算法（Fault Formation）

### 核心原理

- **算法思想**：通过随机生成断层线，抬升/降低断层一侧的地形高度，模拟地质断层形成过程
- **特点**：生成的地形具有明显的断层特征，适合生成山脉、峡谷等地形
- **优点**：算法简单，计算快速
- **缺点**：生成的地形可能过于规则，缺乏细节

### 实现代码

```csharp
/// <summary>
/// 断层构造地形生成器
/// </summary>
public class FaultFormationGenerator : TerrainGenerator
{
    private int faultCount;        // 断层数量
    private float faultHeight;     // 断层高度变化
    private System.Random random;
    
    public FaultFormationGenerator(int width, int height, int faultCount = 100, float faultHeight = 5f) 
        : base(width, height)
    {
        this.faultCount = faultCount;
        this.faultHeight = faultHeight;
        this.random = new System.Random();
    }
    
    public override float[,] Generate()
    {
        // 初始化高度图为0
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                heightMap[x, z] = 0f;
            }
        }
        
        // 生成多个断层
        for (int i = 0; i < faultCount; i++)
        {
            GenerateFault();
        }
        
        return heightMap;
    }
    
    /// <summary>
    /// 生成单个断层
    /// </summary>
    private void GenerateFault()
    {
        // 随机生成断层线（使用两点定义直线）
        Vector2 point1 = new Vector2(
            random.Next(0, width),
            random.Next(0, height)
        );
        Vector2 point2 = new Vector2(
            random.Next(0, width),
            random.Next(0, height)
        );
        
        // 计算断层线的方向向量
        Vector2 direction = (point2 - point1).normalized;
        Vector2 normal = new Vector2(-direction.y, direction.x);  // 法向量（垂直方向）
        
        // 随机决定抬升方向（正或负）
        float heightChange = (random.Next(0, 2) == 0 ? 1f : -1f) * faultHeight;
        
        // 对每个点判断在断层的哪一侧，并调整高度
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector2 point = new Vector2(x, z);
                Vector2 toPoint = point - point1;
                
                // 使用点积判断点在断层线的哪一侧
                float dot = Vector2.Dot(toPoint, normal);
                
                // 如果点在法向量指向的一侧，抬升高度
                if (dot > 0)
                {
                    heightMap[x, z] += heightChange;
                }
            }
        }
    }
}
```

---

## 4.18 中点置换算法（Midpoint Displacement / Diamond-Square）

### 核心原理

- **算法思想**：递归地将地形分割成更小的区域，在每个区域的中点插入新的高度值，通过插值和随机扰动生成分形地形
- **Diamond-Square算法**：最经典的分形地形生成算法
  - **Diamond步骤**：在正方形中心（菱形）插入高度值
  - **Square步骤**：在正方形边缘中点插入高度值
- **特点**：生成的地形具有分形特征，细节丰富，自然真实
- **优点**：生成的地形自然、细节丰富
- **缺点**：计算复杂度较高，需要递归处理

### 实现代码

```csharp
/// <summary>
/// 中点置换（Diamond-Square）地形生成器
/// </summary>
public class MidpointDisplacementGenerator : TerrainGenerator
{
    private float roughness;      // 粗糙度（控制随机扰动幅度）
    private float initialHeight;  // 初始高度
    private System.Random random;
    
    public MidpointDisplacementGenerator(int width, int height, float roughness = 0.5f, float initialHeight = 0f) 
        : base(width, height)
    {
        this.roughness = roughness;
        this.initialHeight = initialHeight;
        this.random = new System.Random();
        
        // 宽度和高度必须是2的幂次+1（如33, 65, 129）
        if (!IsPowerOfTwoPlusOne(width) || !IsPowerOfTwoPlusOne(height))
        {
            Debug.LogWarning("Diamond-Square算法要求尺寸为2^n+1，将自动调整");
        }
    }
    
    public override float[,] Generate()
    {
        // 初始化四个角的高度
        heightMap[0, 0] = initialHeight + RandomOffset();
        heightMap[0, height - 1] = initialHeight + RandomOffset();
        heightMap[width - 1, 0] = initialHeight + RandomOffset();
        heightMap[width - 1, height - 1] = initialHeight + RandomOffset();
        
        // 递归生成
        int size = Mathf.Min(width, height) - 1;
        float currentRoughness = roughness;
        
        while (size > 1)
        {
            DiamondSquare(size, currentRoughness);
            size /= 2;
            currentRoughness *= 0.5f;  // 每次迭代减少粗糙度
        }
        
        return heightMap;
    }
    
    /// <summary>
    /// Diamond-Square算法的核心步骤
    /// </summary>
    private void DiamondSquare(int stepSize, float roughness)
    {
        int halfStep = stepSize / 2;
        
        // Diamond步骤：在正方形中心插入高度值
        for (int x = halfStep; x < width; x += stepSize)
        {
            for (int z = halfStep; z < height; z += stepSize)
            {
                DiamondStep(x, z, halfStep, roughness);
            }
        }
        
        // Square步骤：在正方形边缘中点插入高度值
        for (int x = 0; x < width; x += halfStep)
        {
            for (int z = (x + halfStep) % stepSize; z < height; z += stepSize)
            {
                SquareStep(x, z, halfStep, roughness);
            }
        }
    }
    
    /// <summary>
    /// Diamond步骤：计算正方形中心点的高度
    /// </summary>
    private void DiamondStep(int x, int z, int halfStep, float roughness)
    {
        float average = (
            GetHeight(x - halfStep, z - halfStep) +
            GetHeight(x + halfStep, z - halfStep) +
            GetHeight(x - halfStep, z + halfStep) +
            GetHeight(x + halfStep, z + halfStep)
        ) / 4f;
        
        heightMap[x, z] = average + RandomOffset() * roughness;
    }
    
    /// <summary>
    /// Square步骤：计算正方形边缘中点的高度
    /// </summary>
    private void SquareStep(int x, int z, int halfStep, float roughness)
    {
        float sum = 0f;
        int count = 0;
        
        // 计算四个相邻点的平均值
        if (x - halfStep >= 0)
        {
            sum += GetHeight(x - halfStep, z);
            count++;
        }
        if (x + halfStep < width)
        {
            sum += GetHeight(x + halfStep, z);
            count++;
        }
        if (z - halfStep >= 0)
        {
            sum += GetHeight(x, z - halfStep);
            count++;
        }
        if (z + halfStep < height)
        {
            sum += GetHeight(x, z + halfStep);
            count++;
        }
        
        if (count > 0)
        {
            heightMap[x, z] = (sum / count) + RandomOffset() * roughness;
        }
    }
    
    /// <summary>
    /// 安全获取高度值（处理边界）
    /// </summary>
    private float GetHeight(int x, int z)
    {
        if (x < 0 || x >= width || z < 0 || z >= height)
            return 0f;
        return heightMap[x, z];
    }
    
    /// <summary>
    /// 生成随机偏移值
    /// </summary>
    private float RandomOffset()
    {
        return (float)(random.NextDouble() * 2.0 - 1.0);  // -1 到 1
    }
    
    /// <summary>
    /// 检查是否为2的幂次+1
    /// </summary>
    private bool IsPowerOfTwoPlusOne(int n)
    {
        return (n - 1) > 0 && ((n - 1) & (n - 2)) == 0;
    }
}
```

---

## 4.19 粒子沉积算法（Particle Deposition）

### 核心原理

- **算法思想**：模拟自然沉积过程，通过粒子随机落下并堆积来生成地形
- **特点**：生成的地形更自然，具有真实的沉积特征，适合生成沙丘、火山等地形
- **优点**：生成的地形自然真实，适合特定地形类型
- **缺点**：计算复杂度较高，需要多次迭代

### 实现代码

```csharp
/// <summary>
/// 粒子沉积地形生成器
/// </summary>
public class ParticleDepositionGenerator : TerrainGenerator
{
    private int particleCount;     // 粒子数量
    private float particleSize;    // 粒子大小（影响范围）
    private float stabilityThreshold;  // 稳定性阈值
    private System.Random random;
    
    public ParticleDepositionGenerator(int width, int height, int particleCount = 10000, float particleSize = 2f, float stabilityThreshold = 0.5f) 
        : base(width, height)
    {
        this.particleCount = particleCount;
        this.particleSize = particleSize;
        this.stabilityThreshold = stabilityThreshold;
        this.random = new System.Random();
    }
    
    public override float[,] Generate()
    {
        // 初始化高度图为0
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                heightMap[x, z] = 0f;
            }
        }
        
        // 沉积粒子
        for (int i = 0; i < particleCount; i++)
        {
            DepositParticle();
        }
        
        return heightMap;
    }
    
    /// <summary>
    /// 沉积单个粒子
    /// </summary>
    private void DepositParticle()
    {
        // 随机选择起始位置（从顶部）
        int x = random.Next(0, width);
        int z = random.Next(0, height);
        
        // 粒子下落，直到找到稳定位置
        while (true)
        {
            // 检查当前位置是否稳定
            if (IsStable(x, z))
            {
                // 沉积粒子（增加高度）
                DepositAt(x, z);
                break;
            }
            
            // 如果不稳定，粒子向下移动（选择最低的邻居）
            Vector2Int nextPos = FindLowestNeighbor(x, z);
            if (nextPos.x == x && nextPos.y == z)
            {
                // 无法继续移动，直接沉积
                DepositAt(x, z);
                break;
            }
            
            x = nextPos.x;
            z = nextPos.y;
        }
    }
    
    /// <summary>
    /// 检查位置是否稳定（高度差小于阈值）
    /// </summary>
    private bool IsStable(int x, int z)
    {
        float currentHeight = GetHeight(x, z);
        
        // 检查所有邻居
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                if (dx == 0 && dz == 0) continue;
                
                int nx = x + dx;
                int nz = z + dz;
                
                if (IsValidPosition(nx, nz))
                {
                    float neighborHeight = GetHeight(nx, nz);
                    float heightDiff = currentHeight - neighborHeight;
                    
                    // 如果高度差大于阈值，不稳定
                    if (heightDiff > stabilityThreshold)
                    {
                        return false;
                    }
                }
            }
        }
        
        return true;
    }
    
    /// <summary>
    /// 找到最低的邻居位置
    /// </summary>
    private Vector2Int FindLowestNeighbor(int x, int z)
    {
        float minHeight = GetHeight(x, z);
        Vector2Int lowestPos = new Vector2Int(x, z);
        
        // 检查所有邻居
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                if (dx == 0 && dz == 0) continue;
                
                int nx = x + dx;
                int nz = z + dz;
                
                if (IsValidPosition(nx, nz))
                {
                    float neighborHeight = GetHeight(nx, nz);
                    if (neighborHeight < minHeight)
                    {
                        minHeight = neighborHeight;
                        lowestPos = new Vector2Int(nx, nz);
                    }
                }
            }
        }
        
        return lowestPos;
    }
    
    /// <summary>
    /// 在指定位置沉积粒子
    /// </summary>
    private void DepositAt(int x, int z)
    {
        // 在粒子大小范围内增加高度
        int radius = Mathf.CeilToInt(particleSize);
        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dz = -radius; dz <= radius; dz++)
            {
                int nx = x + dx;
                int nz = z + dz;
                
                if (IsValidPosition(nx, nz))
                {
                    float distance = Mathf.Sqrt(dx * dx + dz * dz);
                    if (distance <= particleSize)
                    {
                        // 使用距离衰减函数，中心影响最大
                        float influence = 1f - (distance / particleSize);
                        heightMap[nx, nz] += influence * 0.1f;  // 沉积高度
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 安全获取高度值
    /// </summary>
    private float GetHeight(int x, int z)
    {
        if (x < 0 || x >= width || z < 0 || z >= height)
            return float.MaxValue;  // 边界外视为无限高
        return heightMap[x, z];
    }
    
    /// <summary>
    /// 检查位置是否有效
    /// </summary>
    private bool IsValidPosition(int x, int z)
    {
        return x >= 0 && x < width && z >= 0 && z < height;
    }
}
```

---

## 4.16 实时真实地形生成（综合应用）

### 核心概念

- **定义**：结合多种算法和技术，实现完整的程序化地形生成系统
- **主要内容**：
  - 基础地形生成（使用上述算法）
  - 风景设计（植被、水体、道路等）
  - 建筑物生成
  - 命名算法

### 综合应用示例

```csharp
/// <summary>
/// 综合地形生成系统
/// </summary>
public class ProceduralTerrainSystem : MonoBehaviour
{
    public Terrain terrain;
    public int terrainWidth = 513;
    public int terrainHeight = 513;
    public float maxHeight = 100f;
    
    [Header("地形生成算法")]
    public TerrainGenerationMethod method = TerrainGenerationMethod.MidpointDisplacement;
    
    [Header("断层构造参数")]
    public int faultCount = 100;
    public float faultHeight = 5f;
    
    [Header("中点置换参数")]
    public float roughness = 0.5f;
    
    [Header("粒子沉积参数")]
    public int particleCount = 10000;
    public float particleSize = 2f;
    
    public enum TerrainGenerationMethod
    {
        FaultFormation,
        MidpointDisplacement,
        ParticleDeposition
    }
    
    /// <summary>
    /// 生成地形
    /// </summary>
    public void GenerateTerrain()
    {
        TerrainGenerator generator = null;
        
        // 根据选择的方法创建生成器
        switch (method)
        {
            case TerrainGenerationMethod.FaultFormation:
                generator = new FaultFormationGenerator(terrainWidth, terrainHeight, faultCount, faultHeight);
                break;
            case TerrainGenerationMethod.MidpointDisplacement:
                generator = new MidpointDisplacementGenerator(terrainWidth, terrainHeight, roughness);
                break;
            case TerrainGenerationMethod.ParticleDeposition:
                generator = new ParticleDepositionGenerator(terrainWidth, terrainHeight, particleCount, particleSize);
                break;
        }
        
        if (generator != null)
        {
            // 生成高度图
            float[,] heightMap = generator.Generate();
            
            // 应用到Unity Terrain
            generator.ApplyToTerrain(terrain, maxHeight);
            
            Debug.Log("地形生成完成！");
        }
    }
    
    // Unity Editor中调用
    [ContextMenu("生成地形")]
    private void GenerateTerrainContextMenu()
    {
        GenerateTerrain();
    }
}
```

---

## 算法对比

| 算法 | 优点 | 缺点 | 适用场景 |
|------|------|------|---------|
| **断层构造** | 简单快速，生成明显的地形特征 | 地形可能过于规则 | 山脉、峡谷 |
| **中点置换** | 自然真实，细节丰富，经典算法 | 计算复杂度较高 | 通用地形生成 |
| **粒子沉积** | 自然真实，具有沉积特征 | 计算复杂度高 | 沙丘、火山 |

---

## Unity应用建议

1. **使用Unity Terrain系统**：
   - 使用`TerrainData.SetHeights()`应用高度图
   - 配合`TerrainData.alphamapTextures`添加纹理
   - 使用`TreeInstance`和`DetailPrototype`添加植被

2. **性能优化**：
   - 使用协程分帧生成，避免卡顿
   - 使用Job System并行计算
   - 缓存高度图数据，避免重复计算

3. **扩展功能**：
   - 添加噪声函数（Perlin Noise）增加细节
   - 使用多层算法组合生成复杂地形
   - 实现动态加载/卸载系统

---

## 参考文献

- 《游戏编程精粹1》- 4.16 实时真实地形生成
- 《游戏编程精粹1》- 4.17 分形地形生成 - 断层构造
- 《游戏编程精粹1》- 4.18 分形地形生成 - 中点置换
- 《游戏编程精粹1》- 4.19 分形地形生成 - 粒子沉积

