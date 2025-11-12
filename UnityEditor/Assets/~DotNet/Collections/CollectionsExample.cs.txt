// CollectionsExample.cs
// .NET集合API使用详解示例
// 包含List、Dictionary、ConcurrentDictionary、HashSet、Queue、Stack、LinkedList、Linq等常用集合类型
// 每个方法、关键步骤、枚举值均有详细中文注释
// 适合.NET初学者学习和查阅
// 
// 主要功能模块：
// 1. List<T> - 动态数组，支持随机访问和动态扩容
// 2. Dictionary<TKey,TValue> - 键值对集合，快速查找
// 3. ConcurrentDictionary<TKey,TValue> - 线程安全的键值对集合
// 4. HashSet<T> - 无序不重复集合，快速查找和去重
// 5. Queue<T> - 队列，先进先出(FIFO)
// 6. Stack<T> - 栈，后进先出(LIFO)
// 7. LinkedList<T> - 双向链表，高效插入删除
// 8. LINQ - 语言集成查询，强大的数据操作
// 9. 高级集合操作 - 性能优化和特殊用法

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using UnityEngine;

namespace DotNet.Collections
{
    /// <summary>
    /// .NET集合API使用详解示例
    /// 演示List、Dictionary、ConcurrentDictionary、HashSet、Queue、Stack、LinkedList、Linq等常用集合类型
    /// 
    /// 重要说明：
    /// - List<T>是最常用的集合类型，适合大多数场景
    /// - Dictionary<TKey,TValue>提供O(1)的查找性能
    /// - ConcurrentDictionary<TKey,TValue>提供线程安全的字典操作
    /// - HashSet<T>适合去重和快速成员检查
    /// - Queue和Stack适合特定算法场景
    /// - LinkedList<T>适合频繁插入删除操作
    /// - LINQ提供强大的数据查询和转换功能
    /// - 跨平台注意：集合行为在不同平台基本一致
    /// </summary>
    public class CollectionsExample : MonoBehaviour
    {
        [Header("集合示例配置")]
        [Tooltip("是否自动运行所有示例代码")]
        [SerializeField] private bool runExamples = true;
        [Tooltip("是否显示性能测试结果")]
        [SerializeField] private bool showPerformanceTests = true;
        [Tooltip("测试数据规模")]
        [SerializeField] private int testDataSize = 1000;

        private void Start()
        {
            if (runExamples)
            {
                RunAllExamples();
            }
        }

        /// <summary>
        /// 运行所有集合相关示例
        /// 按顺序执行：List -> Dictionary -> ConcurrentDictionary -> HashSet -> Queue -> Stack -> LinkedList -> Linq -> 高级集合操作
        /// 
        /// 执行流程：
        /// 1. 基础动态数组操作
        /// 2. 键值对集合操作
        /// 3. 线程安全字典操作
        /// 4. 无序不重复集合操作
        /// 5. 队列操作
        /// 6. 栈操作
        /// 7. 链表操作
        /// 8. LINQ查询操作
        /// 9. 高级集合功能
        /// </summary>
        private void RunAllExamples()
        {
            Debug.Log("=== .NET集合API示例开始 ===");
            Debug.Log($"性能测试显示: {showPerformanceTests}");
            Debug.Log($"测试数据规模: {testDataSize}");
            
            ListExample();      // List<T> 动态数组
            DictionaryExample(); // Dictionary<TKey, TValue> 键值对集合
            ConcurrentDictionaryExample(); // ConcurrentDictionary<TKey, TValue> 线程安全字典
            HashSetExample();   // HashSet<T> 无序不重复集合
            QueueExample();     // Queue<T> 队列（先进先出）
            StackExample();     // Stack<T> 栈（后进先出）
            LinkedListExample(); // LinkedList<T> 双向链表
            LinqExample();      // LINQ查询操作
            AdvancedCollectionsExample(); // 高级集合操作
            
            Debug.Log("=== .NET集合API示例结束 ===");
        }

        // ================= List<T> 动态数组 =================
        /// <summary>
        /// List<T> 动态数组示例
        /// List是最常用的集合类型，支持动态扩容，随机访问
        /// 
        /// 主要特性：
        /// - 动态扩容，自动管理内存
        /// - O(1)随机访问，O(n)插入删除
        /// - 支持索引访问和迭代
        /// - 丰富的内置方法
        /// 
        /// 注意事项：
        /// - 频繁插入删除时考虑LinkedList
        /// - 大量数据时考虑指定初始容量
        /// - 线程不安全，多线程需要同步
        /// - 值类型存储在连续内存中
        /// </summary>
        private void ListExample()
        {
            Debug.Log("--- List<T> 动态数组示例 ---");
            
            try
            {
                // ========== 创建和初始化 ==========
                
                // 创建空List
                // 参数说明：无
                // 返回值：List<T> - 空列表实例
                // 注意事项：初始容量为0，首次添加时扩容
                var emptyList = new List<int>();
                Debug.Log($"空List容量: {emptyList.Capacity}, 长度: {emptyList.Count}");
                
                // 创建并初始化List
                // 参数说明：collection - 初始元素集合
                // 返回值：List<T> - 包含初始元素的列表
                // 注意事项：集合初始化器语法，编译器自动优化
                var numbers = new List<int> { 1, 2, 3, 4, 5 };
                var names = new List<string> { "Alice", "Bob", "Charlie", "David", "Eve" };
                Debug.Log($"初始化List: {string.Join(", ", numbers)}");
                
                // 指定初始容量（优化性能）
                // 参数说明：capacity - 初始容量
                // 返回值：List<T> - 指定容量的空列表
                // 注意事项：避免频繁扩容，提升性能
                var capacityList = new List<int>(100);
                Debug.Log($"指定容量的List: {capacityList.Capacity}");
                
                // ========== 添加元素操作 ==========
                
                // Add: 在末尾添加单个元素
                // 参数说明：item - 要添加的元素
                // 返回值：void
                // 时间复杂度：平均O(1)，最坏O(n)（需要扩容）
                numbers.Add(6);
                Debug.Log($"Add(6)后: {string.Join(", ", numbers)}");
                
                // Insert: 在指定位置插入元素
                // 参数说明：index - 插入位置, item - 要插入的元素
                // 返回值：void
                // 时间复杂度：O(n)，需要移动后续元素
                numbers.Insert(0, 0);
                Debug.Log($"Insert(0, 0)后: {string.Join(", ", numbers)}");
                
                // AddRange: 在末尾添加多个元素
                // 参数说明：collection - 要添加的元素集合
                // 返回值：void
                // 注意事项：比多次Add更高效
                numbers.AddRange(new int[] { 7, 8, 9 });
                Debug.Log($"AddRange([7,8,9])后: {string.Join(", ", numbers)}");
                
                // InsertRange: 在指定位置插入多个元素
                // 参数说明：index - 插入位置, collection - 要插入的元素集合
                // 返回值：void
                // 时间复杂度：O(n + m)，n为列表长度，m为插入元素数量
                numbers.InsertRange(3, new int[] { 10, 11 });
                Debug.Log($"InsertRange(3, [10,11])后: {string.Join(", ", numbers)}");
                
                // ========== 访问元素操作 ==========
                
                // 索引访问
                // 参数说明：index - 元素索引
                // 返回值：T - 指定位置的元素
                // 时间复杂度：O(1)
                Debug.Log($"第一个元素: {numbers[0]}");
                Debug.Log($"最后一个元素: {numbers[^1]}"); // C# 8.0 索引语法
                Debug.Log($"倒数第二个元素: {numbers[^2]}");
                
                // 获取元素数量
                // 参数说明：无
                // 返回值：int - 元素数量
                Debug.Log($"List长度: {numbers.Count}");
                Debug.Log($"List容量: {numbers.Capacity}");
                
                // 检查容量使用率
                double usageRate = (double)numbers.Count / numbers.Capacity * 100;
                Debug.Log($"容量使用率: {usageRate:F1}%");
                
                // ========== 查找元素操作 ==========
                
                // IndexOf: 查找元素的第一个索引
                // 参数说明：item - 要查找的元素
                // 返回值：int - 元素索引，-1表示未找到
                // 时间复杂度：O(n)
                int index = numbers.IndexOf(5);
                Debug.Log($"IndexOf(5): {index}");
                
                // LastIndexOf: 查找元素的最后一个索引
                // 参数说明：item - 要查找的元素
                // 返回值：int - 元素索引，-1表示未找到
                // 时间复杂度：O(n)
                numbers.Add(5); // 添加重复元素
                int lastIndex = numbers.LastIndexOf(5);
                Debug.Log($"LastIndexOf(5): {lastIndex}");
                
                // Contains: 检查是否包含元素
                // 参数说明：item - 要检查的元素
                // 返回值：bool - 是否包含
                // 时间复杂度：O(n)
                bool contains = numbers.Contains(10);
                Debug.Log($"Contains(10): {contains}");
                
                // Exists: 使用条件查找元素
                // 参数说明：match - 匹配条件委托
                // 返回值：bool - 是否存在满足条件的元素
                // 时间复杂度：O(n)
                bool exists = numbers.Exists(x => x > 10);
                Debug.Log($"Exists(x > 10): {exists}");
                
                // Find: 查找第一个满足条件的元素
                // 参数说明：match - 匹配条件委托
                // 返回值：T - 找到的元素，默认值表示未找到
                // 时间复杂度：O(n)
                int found = numbers.Find(x => x % 2 == 0);
                Debug.Log($"Find(第一个偶数): {found}");
                
                // FindAll: 查找所有满足条件的元素
                // 参数说明：match - 匹配条件委托
                // 返回值：List<T> - 满足条件的元素列表
                // 时间复杂度：O(n)
                var evenNumbers = numbers.FindAll(x => x % 2 == 0);
                Debug.Log($"FindAll(所有偶数): {string.Join(", ", evenNumbers)}");
                
                // FindIndex: 查找第一个满足条件的元素索引
                // 参数说明：match - 匹配条件委托
                // 返回值：int - 元素索引，-1表示未找到
                int findIndex = numbers.FindIndex(x => x > 8);
                Debug.Log($"FindIndex(x > 8): {findIndex}");
                
                // ========== 排序和反转操作 ==========
                
                // Sort: 排序（默认升序）
                // 参数说明：无或comparison - 比较委托
                // 返回值：void
                // 时间复杂度：O(n log n)
                numbers.Sort();
                Debug.Log($"Sort()后: {string.Join(", ", numbers)}");
                
                // Sort: 自定义排序（降序）
                // 参数说明：comparison - 比较委托
                // 返回值：void
                numbers.Sort((a, b) => b.CompareTo(a));
                Debug.Log($"Sort(降序)后: {string.Join(", ", numbers)}");
                
                // Reverse: 反转
                // 参数说明：无
                // 返回值：void
                // 时间复杂度：O(n)
                numbers.Reverse();
                Debug.Log($"Reverse()后: {string.Join(", ", numbers)}");
                
                // ========== 移除元素操作 ==========
                
                // Remove: 移除第一个匹配的元素
                // 参数说明：item - 要移除的元素
                // 返回值：bool - 是否成功移除
                // 时间复杂度：O(n)
                bool removed = numbers.Remove(5);
                Debug.Log($"Remove(5)成功: {removed}");
                Debug.Log($"移除后: {string.Join(", ", numbers)}");
                
                // RemoveAt: 移除指定位置的元素
                // 参数说明：index - 要移除的位置
                // 返回值：void
                // 时间复杂度：O(n)，需要移动后续元素
                numbers.RemoveAt(0);
                Debug.Log($"RemoveAt(0)后: {string.Join(", ", numbers)}");
                
                // RemoveRange: 移除指定范围的元素
                // 参数说明：index - 起始位置, count - 移除数量
                // 返回值：void
                // 时间复杂度：O(n)
                numbers.RemoveRange(0, 2);
                Debug.Log($"RemoveRange(0, 2)后: {string.Join(", ", numbers)}");
                
                // RemoveAll: 移除所有满足条件的元素
                // 参数说明：match - 匹配条件委托
                // 返回值：int - 移除的元素数量
                // 时间复杂度：O(n)
                int removedCount = numbers.RemoveAll(x => x < 5);
                Debug.Log($"RemoveAll(x < 5)移除了{removedCount}个元素");
                Debug.Log($"移除后: {string.Join(", ", numbers)}");
                
                // ========== 转换操作 ==========
                
                // ToArray: 转换为数组
                // 参数说明：无
                // 返回值：T[] - 数组副本
                // 时间复杂度：O(n)
                int[] array = numbers.ToArray();
                Debug.Log($"ToArray(): {string.Join(", ", array)}");
                
                // ToList: 创建副本
                // 参数说明：无
                // 返回值：List<T> - 列表副本
                // 时间复杂度：O(n)
                var copy = numbers.ToList();
                Debug.Log($"ToList()副本: {string.Join(", ", copy)}");
                
                // ========== 清空操作 ==========
                
                // Clear: 清空所有元素
                // 参数说明：无
                // 返回值：void
                // 时间复杂度：O(n)
                numbers.Clear();
                Debug.Log($"Clear()后长度: {numbers.Count}");
                
                // TrimExcess: 释放多余容量
                // 参数说明：无
                // 返回值：void
                // 注意事项：只有当使用率低于90%时才释放
                numbers.TrimExcess();
                Debug.Log($"TrimExcess()后容量: {numbers.Capacity}");
                
                // ========== 性能测试 ==========
                
                if (showPerformanceTests)
                {
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    
                    // 测试添加性能
                    var perfList = new List<int>();
                    for (int i = 0; i < testDataSize; i++)
                    {
                        perfList.Add(i);
                    }
                    stopwatch.Stop();
                    Debug.Log($"添加{testDataSize}个元素耗时: {stopwatch.ElapsedMilliseconds}ms");
                    
                    // 测试查找性能
                    stopwatch.Restart();
                    for (int i = 0; i < 1000; i++)
                    {
                        perfList.Contains(i % testDataSize);
                    }
                    stopwatch.Stop();
                    Debug.Log($"1000次Contains操作耗时: {stopwatch.ElapsedMilliseconds}ms");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"List操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        // ================= Dictionary<TKey, TValue> 键值对集合 =================
        /// <summary>
        /// Dictionary<TKey, TValue> 键值对集合示例
        /// Dictionary提供快速的键值对查找，键必须唯一
        /// 
        /// 主要特性：
        /// - O(1)平均查找、插入、删除性能
        /// - 键必须唯一，值可以重复
        /// - 基于哈希表实现
        /// - 无序存储
        /// 
        /// 注意事项：
        /// - 键类型必须实现GetHashCode和Equals
        /// - 频繁修改时考虑ConcurrentDictionary
        /// - 自定义类型作为键需要正确实现哈希
        /// - 线程不安全，多线程需要同步
        /// </summary>
        private void DictionaryExample()
        {
            Debug.Log("--- Dictionary<TKey, TValue> 键值对集合示例 ---");
            
            try
            {
                // ========== 创建和初始化 ==========
                
                // 创建空字典
                // 参数说明：无
                // 返回值：Dictionary<TKey, TValue> - 空字典实例
                var emptyDict = new Dictionary<string, int>();
                Debug.Log($"空字典容量: {emptyDict.Count}");
                
                // 创建并初始化字典
                // 参数说明：collection - 初始键值对集合
                // 返回值：Dictionary<TKey, TValue> - 包含初始元素的字典
                var scores = new Dictionary<string, int>
                {
                    { "Alice", 95 },
                    { "Bob", 87 },
                    { "Charlie", 92 },
                    { "David", 78 },
                    { "Eve", 89 }
                };
                Debug.Log($"初始化字典: {scores.Count}个键值对");
                
                // 指定初始容量
                // 参数说明：capacity - 初始容量
                // 返回值：Dictionary<TKey, TValue> - 指定容量的空字典
                var capacityDict = new Dictionary<string, int>(100);
                Debug.Log($"指定容量的字典已创建");
                
                // ========== 添加和更新操作 ==========
                
                // Add: 添加键值对
                // 参数说明：key - 键, value - 值
                // 返回值：void
                // 时间复杂度：平均O(1)
                scores.Add("Frank", 85);
                Debug.Log($"Add(Frank, 85)后: {scores.Count}个键值对");
                
                // 索引器：添加或更新
                // 参数说明：key - 键
                // 返回值：TValue - 值
                // 注意事项：如果键不存在则添加，存在则更新
                scores["Alice"] = 98; // 更新
                scores["Grace"] = 91; // 添加
                Debug.Log($"使用索引器后: {scores.Count}个键值对");
                
                // TryAdd: 尝试添加（.NET Core 3.0+）
                // 参数说明：key - 键, value - 值
                // 返回值：bool - 是否成功添加
                bool added = scores.TryAdd("Henry", 88);
                Debug.Log($"TryAdd(Henry, 88): {added}");
                
                // ========== 访问和查找操作 ==========
                
                // 索引器访问
                // 参数说明：key - 键
                // 返回值：TValue - 值
                // 注意事项：键不存在时抛出异常
                int aliceScore = scores["Alice"];
                Debug.Log($"Alice的分数: {aliceScore}");
                
                // TryGetValue: 安全获取值
                // 参数说明：key - 键, value - 输出值
                // 返回值：bool - 是否找到键
                // 时间复杂度：平均O(1)
                if (scores.TryGetValue("Bob", out int bobScore))
                {
                    Debug.Log($"Bob的分数: {bobScore}");
                }
                else
                {
                    Debug.Log("未找到Bob的分数");
                }
                
                // ContainsKey: 检查是否包含键
                // 参数说明：key - 键
                // 返回值：bool - 是否包含
                // 时间复杂度：平均O(1)
                bool hasKey = scores.ContainsKey("Charlie");
                Debug.Log($"包含Charlie: {hasKey}");
                
                // ContainsValue: 检查是否包含值
                // 参数说明：value - 值
                // 返回值：bool - 是否包含
                // 时间复杂度：O(n)
                bool hasValue = scores.ContainsValue(95);
                Debug.Log($"包含分数95: {hasValue}");
                
                // ========== 遍历操作 ==========
                
                // 遍历所有键值对
                Debug.Log("所有键值对:");
                foreach (var kvp in scores)
                {
                    Debug.Log($"  {kvp.Key}: {kvp.Value}");
                }
                
                // 遍历所有键
                Debug.Log("所有键:");
                foreach (string key in scores.Keys)
                {
                    Debug.Log($"  {key}");
                }
                
                // 遍历所有值
                Debug.Log("所有值:");
                foreach (int value in scores.Values)
                {
                    Debug.Log($"  {value}");
                }
                
                // ========== 移除操作 ==========
                
                // Remove: 移除键值对
                // 参数说明：key - 要移除的键
                // 返回值：bool - 是否成功移除
                // 时间复杂度：平均O(1)
                bool removed = scores.Remove("David");
                Debug.Log($"Remove(David): {removed}");
                Debug.Log($"移除后: {scores.Count}个键值对");
                
                // TryRemove: 尝试移除（.NET Core 3.0+）
                // 参数说明：key - 要移除的键, value - 输出被移除的值
                // 返回值：bool - 是否成功移除
                if (scores.TryRemove("Eve", out int eveScore))
                {
                    Debug.Log($"TryRemove(Eve): 成功，值为{eveScore}");
                }
                
                // Clear: 清空字典
                // 参数说明：无
                // 返回值：void
                scores.Clear();
                Debug.Log($"Clear()后: {scores.Count}个键值对");
                
                // ========== 高级操作 ==========
                
                // 重新添加数据用于演示
                scores = new Dictionary<string, int>
                {
                    { "Alice", 95 }, { "Bob", 87 }, { "Charlie", 92 },
                    { "David", 78 }, { "Eve", 89 }, { "Frank", 85 }
                };
                
                // 获取或添加默认值
                // 参数说明：key - 键, defaultValue - 默认值
                // 返回值：TValue - 值
                int graceScore = scores.GetValueOrDefault("Grace", 0);
                Debug.Log($"Grace的分数(默认0): {graceScore}");
                
                // 条件更新
                if (scores.ContainsKey("Alice"))
                {
                    scores["Alice"] = Math.Max(scores["Alice"], 100);
                }
                Debug.Log($"Alice更新后分数: {scores["Alice"]}");
                
                // ========== 性能测试 ==========
                
                if (showPerformanceTests)
                {
                    var perfDict = new Dictionary<int, string>();
                    
                    // 测试添加性能
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    for (int i = 0; i < testDataSize; i++)
                    {
                        perfDict[i] = $"Value{i}";
                    }
                    stopwatch.Stop();
                    Debug.Log($"添加{testDataSize}个键值对耗时: {stopwatch.ElapsedMilliseconds}ms");
                    
                    // 测试查找性能
                    stopwatch.Restart();
                    for (int i = 0; i < 10000; i++)
                    {
                        perfDict.TryGetValue(i % testDataSize, out _);
                    }
                    stopwatch.Stop();
                    Debug.Log($"10000次查找操作耗时: {stopwatch.ElapsedMilliseconds}ms");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"Dictionary操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        // ================= ConcurrentDictionary<TKey, TValue> 线程安全字典 =================
        /// <summary>
        /// ConcurrentDictionary<TKey, TValue> 线程安全字典示例
        /// ConcurrentDictionary提供线程安全的字典操作，适合多线程环境
        /// 
        /// 主要特性：
        /// - 线程安全，支持并发读写
        /// - O(1)平均查找性能
        /// - 原子操作，避免竞态条件
        /// - 无锁设计，性能优异
        /// 
        /// 使用场景：
        /// - 多线程环境下的数据共享
        /// - 缓存系统
        /// - 计数器集合
        /// - 状态管理
        /// 
        /// 注意事项：
        /// - 比普通Dictionary稍慢
        /// - 某些操作不是原子性的
        /// - 迭代时可能看到不一致的状态
        /// </summary>
        private void ConcurrentDictionaryExample()
        {
            Debug.Log("--- ConcurrentDictionary<TKey, TValue> 线程安全字典示例 ---");
            
            try
            {
                // ========== 创建和初始化 ==========
                
                // 创建空并发字典
                // 参数说明：无
                // 返回值：ConcurrentDictionary<TKey, TValue> - 空并发字典实例
                var emptyConcurrentDict = new ConcurrentDictionary<string, int>();
                Debug.Log($"空并发字典容量: {emptyConcurrentDict.Count}");
                
                // 创建并初始化并发字典
                // 参数说明：collection - 初始键值对集合
                // 返回值：ConcurrentDictionary<TKey, TValue> - 包含初始元素的并发字典
                var concurrentScores = new ConcurrentDictionary<string, int>
                {
                    ["Alice"] = 95,
                    ["Bob"] = 87,
                    ["Charlie"] = 92,
                    ["David"] = 78,
                    ["Eve"] = 89
                };
                Debug.Log($"初始化并发字典: {concurrentScores.Count}个键值对");
                
                // 指定初始容量
                // 参数说明：capacity - 初始容量
                // 返回值：ConcurrentDictionary<TKey, TValue> - 指定容量的空并发字典
                var capacityConcurrentDict = new ConcurrentDictionary<string, int>(100);
                Debug.Log($"指定容量的并发字典已创建");
                
                // ========== 线程安全的添加和更新操作 ==========
                
                // TryAdd: 尝试添加键值对（线程安全）
                // 参数说明：key - 键, value - 值
                // 返回值：bool - 是否添加成功
                // 时间复杂度：平均O(1)
                bool addResult = concurrentScores.TryAdd("Frank", 85);
                Debug.Log($"TryAdd(Frank, 85)结果: {addResult}, 当前数量: {concurrentScores.Count}");
                
                // TryAdd 重复键会失败
                bool addDuplicateResult = concurrentScores.TryAdd("Alice", 100);
                Debug.Log($"TryAdd重复键(Alice, 100)结果: {addDuplicateResult}");
                
                // TryUpdate: 尝试更新值（线程安全）
                // 参数说明：key - 键, newValue - 新值, comparisonValue - 期望的旧值
                // 返回值：bool - 是否更新成功
                bool updateResult = concurrentScores.TryUpdate("Alice", 98, 95);
                Debug.Log($"TryUpdate(Alice, 98, 95)结果: {updateResult}");
                
                // TryUpdate 期望值不匹配会失败
                bool updateFailResult = concurrentScores.TryUpdate("Alice", 100, 90);
                Debug.Log($"TryUpdate期望值不匹配结果: {updateFailResult}");
                
                // AddOrUpdate: 添加或更新（线程安全）
                // 参数说明：key - 键, addValue - 添加时的值, updateValueFactory - 更新时的值工厂
                // 返回值：TValue - 最终的值
                int finalValue = concurrentScores.AddOrUpdate("Grace", 91, (key, oldValue) => oldValue + 5);
                Debug.Log($"AddOrUpdate(Grace)结果: {finalValue}");
                
                // AddOrUpdate 更新现有值
                int updatedValue = concurrentScores.AddOrUpdate("Alice", 0, (key, oldValue) => oldValue + 2);
                Debug.Log($"AddOrUpdate(Alice)更新结果: {updatedValue}");
                
                // GetOrAdd: 获取或添加（线程安全）
                // 参数说明：key - 键, value - 添加时的值
                // 返回值：TValue - 获取或添加的值
                int getOrAddValue = concurrentScores.GetOrAdd("Henry", 88);
                Debug.Log($"GetOrAdd(Henry, 88)结果: {getOrAddValue}");
                
                // GetOrAdd 获取现有值
                int existingValue = concurrentScores.GetOrAdd("Alice", 0);
                Debug.Log($"GetOrAdd(Alice)获取现有值: {existingValue}");
                
                // ========== 线程安全的访问操作 ==========
                
                // TryGetValue: 尝试获取值（线程安全）
                // 参数说明：key - 键, value - 输出参数，存储获取的值
                // 返回值：bool - 是否获取成功
                if (concurrentScores.TryGetValue("Bob", out int bobScore))
                {
                    Debug.Log($"TryGetValue(Bob)成功: {bobScore}");
                }
                
                // TryGetValue 不存在的键
                if (!concurrentScores.TryGetValue("NonExistent", out int nonExistentScore))
                {
                    Debug.Log($"TryGetValue(NonExistent)失败");
                }
                
                // 索引器访问（线程安全）
                // 参数说明：key - 键
                // 返回值：TValue - 值
                // 注意事项：如果键不存在会抛出异常
                int aliceScore = concurrentScores["Alice"];
                Debug.Log($"索引器访问Alice: {aliceScore}");
                
                // ContainsKey: 检查键是否存在（线程安全）
                // 参数说明：key - 键
                // 返回值：bool - 是否存在
                bool containsAlice = concurrentScores.ContainsKey("Alice");
                bool containsNonExistent = concurrentScores.ContainsKey("NonExistent");
                Debug.Log($"ContainsKey(Alice): {containsAlice}, ContainsKey(NonExistent): {containsNonExistent}");
                
                // ========== 线程安全的删除操作 ==========
                
                // TryRemove: 尝试删除键值对（线程安全）
                // 参数说明：key - 键, value - 输出参数，存储被删除的值
                // 返回值：bool - 是否删除成功
                if (concurrentScores.TryRemove("David", out int removedScore))
                {
                    Debug.Log($"TryRemove(David)成功，删除的值: {removedScore}, 剩余数量: {concurrentScores.Count}");
                }
                
                // TryRemove 不存在的键
                if (!concurrentScores.TryRemove("NonExistent", out int nonRemovedScore))
                {
                    Debug.Log($"TryRemove(NonExistent)失败");
                }
                
                // ========== 批量操作 ==========
                
                // ToArray: 转换为数组（线程安全）
                // 参数说明：无
                // 返回值：KeyValuePair<TKey, TValue>[] - 键值对数组
                var keyValuePairs = concurrentScores.ToArray();
                Debug.Log($"ToArray结果: {keyValuePairs.Length}个键值对");
                
                // Keys: 获取所有键的集合（线程安全）
                // 参数说明：无
                // 返回值：ICollection<TKey> - 键的集合
                var keys = concurrentScores.Keys;
                Debug.Log($"Keys集合: {string.Join(", ", keys)}");
                
                // Values: 获取所有值的集合（线程安全）
                // 参数说明：无
                // 返回值：ICollection<TValue> - 值的集合
                var values = concurrentScores.Values;
                Debug.Log($"Values集合: {string.Join(", ", values)}");
                
                // Count: 获取键值对数量（线程安全）
                // 参数说明：无
                // 返回值：int - 键值对数量
                int count = concurrentScores.Count;
                Debug.Log($"当前键值对数量: {count}");
                
                // IsEmpty: 检查是否为空（线程安全）
                // 参数说明：无
                // 返回值：bool - 是否为空
                bool isEmpty = concurrentScores.IsEmpty;
                Debug.Log($"字典是否为空: {isEmpty}");
                
                // ========== 高级操作 ==========
                
                // GetEnumerator: 获取枚举器（线程安全）
                // 参数说明：无
                // 返回值：IEnumerator<KeyValuePair<TKey, TValue>> - 枚举器
                Debug.Log("遍历并发字典:");
                foreach (var kvp in concurrentScores)
                {
                    Debug.Log($"  {kvp.Key}: {kvp.Value}");
                }
                
                // Clear: 清空字典（线程安全）
                // 参数说明：无
                // 返回值：void
                var tempDict = new ConcurrentDictionary<string, int>(concurrentScores);
                tempDict.Clear();
                Debug.Log($"Clear后数量: {tempDict.Count}");
                
                // ========== 性能测试 ==========
                
                if (showPerformanceTests)
                {
                    Debug.Log("--- ConcurrentDictionary性能测试 ---");
                    
                    var perfDict = new ConcurrentDictionary<int, string>();
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    
                    // 添加性能测试
                    for (int i = 0; i < testDataSize; i++)
                    {
                        perfDict.TryAdd(i, $"Value_{i}");
                    }
                    stopwatch.Stop();
                    Debug.Log($"添加{testDataSize}个元素耗时: {stopwatch.ElapsedMilliseconds}ms");
                    
                    // 查找性能测试
                    stopwatch.Restart();
                    for (int i = 0; i < testDataSize; i++)
                    {
                        perfDict.TryGetValue(i, out string value);
                    }
                    stopwatch.Stop();
                    Debug.Log($"查找{testDataSize}次耗时: {stopwatch.ElapsedMilliseconds}ms");
                    
                    // 更新性能测试
                    stopwatch.Restart();
                    for (int i = 0; i < testDataSize; i++)
                    {
                        perfDict.TryUpdate(i, $"Updated_{i}", $"Value_{i}");
                    }
                    stopwatch.Stop();
                    Debug.Log($"更新{testDataSize}次耗时: {stopwatch.ElapsedMilliseconds}ms");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"ConcurrentDictionary操作出错: {ex.Message}");
                Debug.LogError($"异常类型: {ex.GetType().Name}");
            }
        }

        // ================= HashSet<T> 无序不重复集合 =================
        /// <summary>
        /// HashSet<T> 无序不重复集合示例
        /// HashSet提供O(1)的查找性能，元素唯一且无序
        /// </summary>
        private void HashSetExample()
        {
            Debug.Log("--- HashSet<T> 无序不重复集合示例 ---");
            
            // ========== 创建和初始化 ==========
            
            // 创建空HashSet
            var emptySet = new HashSet<int>();
            
            // 创建并初始化HashSet
            var uniqueNumbers = new HashSet<int> { 1, 2, 3, 4, 5 };
            var names = new HashSet<string> { "Alice", "Bob", "Charlie" };
            
            // 指定比较器
            var caseInsensitiveNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Alice", "alice", "BOB", "Bob"
            };
            Debug.Log($"不区分大小写的HashSet: {string.Join(", ", caseInsensitiveNames)}");
            
            // ========== 添加元素操作 ==========
            
            // Add: 添加元素
            bool added = uniqueNumbers.Add(6);
            Debug.Log($"添加6成功: {added}");
            
            // 添加重复元素
            bool duplicateAdded = uniqueNumbers.Add(1);
            Debug.Log($"添加重复的1成功: {duplicateAdded}");
            
            // AddRange: 添加多个元素
            uniqueNumbers.UnionWith(new int[] { 7, 8, 9 });
            Debug.Log($"添加数组后: {string.Join(", ", uniqueNumbers)}");
            
            // ========== 检查操作 ==========
            
            // Contains: 检查是否包含元素
            bool contains = uniqueNumbers.Contains(5);
            Debug.Log($"是否包含5: {contains}");
            
            // ========== 集合操作 ==========
            
            var set1 = new HashSet<int> { 1, 2, 3, 4 };
            var set2 = new HashSet<int> { 3, 4, 5, 6 };
            
            // 并集 (Union)
            var union = new HashSet<int>(set1);
            union.UnionWith(set2);
            Debug.Log($"并集: {string.Join(", ", union)}");
            
            // 交集 (Intersection)
            var intersection = new HashSet<int>(set1);
            intersection.IntersectWith(set2);
            Debug.Log($"交集: {string.Join(", ", intersection)}");
            
            // 差集 (Difference)
            var difference = new HashSet<int>(set1);
            difference.ExceptWith(set2);
            Debug.Log($"差集: {string.Join(", ", difference)}");
            
            // 对称差集 (Symmetric Difference)
            var symmetricDifference = new HashSet<int>(set1);
            symmetricDifference.SymmetricExceptWith(set2);
            Debug.Log($"对称差集: {string.Join(", ", symmetricDifference)}");
            
            // ========== 集合关系检查 ==========
            
            // 子集检查
            bool isSubset = set1.IsSubsetOf(union);
            Debug.Log($"set1是union的子集: {isSubset}");
            
            // 超集检查
            bool isSuperset = union.IsSupersetOf(set1);
            Debug.Log($"union是set1的超集: {isSuperset}");
            
            // 真子集检查
            bool isProperSubset = set1.IsProperSubsetOf(union);
            Debug.Log($"set1是union的真子集: {isProperSubset}");
            
            // 重叠检查
            bool overlaps = set1.Overlaps(set2);
            Debug.Log($"set1和set2有重叠: {overlaps}");
            
            // 相等检查
            bool equals = set1.SetEquals(set2);
            Debug.Log($"set1和set2相等: {equals}");
            
            // ========== 移除操作 ==========
            
            // Remove: 移除元素
            bool removed = uniqueNumbers.Remove(5);
            Debug.Log($"移除5成功: {removed}");
            
            // RemoveWhere: 条件移除
            int removedCount = uniqueNumbers.RemoveWhere(x => x > 8);
            Debug.Log($"移除了{removedCount}个大于8的元素");
            
            // ========== 转换操作 ==========
            
            // ToArray: 转换为数组
            int[] array = uniqueNumbers.ToArray();
            Debug.Log($"转换为数组: {string.Join(", ", array)}");
            
            // ToList: 转换为List
            var list = uniqueNumbers.ToList();
            Debug.Log($"转换为List: {string.Join(", ", list)}");
            
            // 清空集合
            uniqueNumbers.Clear();
            Debug.Log($"清空后数量: {uniqueNumbers.Count}");
        }

        // ================= Queue<T> 队列（先进先出） =================
        /// <summary>
        /// Queue<T> 队列示例
        /// Queue实现先进先出(FIFO)的数据结构
        /// </summary>
        private void QueueExample()
        {
            Debug.Log("--- Queue<T> 队列示例 ---");
            
            // ========== 创建队列 ==========
            
            // 创建空队列
            var queue = new Queue<string>();
            
            // 指定初始容量
            var capacityQueue = new Queue<int>(100);
            
            // ========== 入队操作 ==========
            
            // Enqueue: 入队
            queue.Enqueue("第一个");
            queue.Enqueue("第二个");
            queue.Enqueue("第三个");
            Debug.Log($"入队后数量: {queue.Count}");
            
            // ========== 出队操作 ==========
            
            // Peek: 查看队首元素（不移除）
            string peeked = queue.Peek();
            Debug.Log($"队首元素: {peeked}");
            Debug.Log($"查看后数量: {queue.Count}");
            
            // Dequeue: 出队
            string dequeued = queue.Dequeue();
            Debug.Log($"出队元素: {dequeued}");
            Debug.Log($"出队后数量: {queue.Count}");
            
            // ========== 检查操作 ==========
            
            // Contains: 检查是否包含元素
            bool contains = queue.Contains("第二个");
            Debug.Log($"是否包含'第二个': {contains}");
            
            // ========== 遍历操作 ==========
            
            Debug.Log("队列中剩余元素:");
            foreach (string item in queue)
            {
                Debug.Log($"元素: {item}");
            }
            
            // ========== 清空操作 ==========
            
            // Clear: 清空队列
            queue.Clear();
            Debug.Log($"清空后数量: {queue.Count}");
            
            // ========== 高级操作 ==========
            
            // 批量入队
            var numberQueue = new Queue<int>();
            for (int i = 1; i <= 5; i++)
            {
                numberQueue.Enqueue(i);
            }
            
            // 批量出队
            Debug.Log("批量出队:");
            while (numberQueue.Count > 0)
            {
                int num = numberQueue.Dequeue();
                Debug.Log($"出队: {num}");
            }
        }

        // ================= Stack<T> 栈（后进先出） =================
        /// <summary>
        /// Stack<T> 栈示例
        /// Stack实现后进先出(LIFO)的数据结构
        /// </summary>
        private void StackExample()
        {
            Debug.Log("--- Stack<T> 栈示例 ---");
            
            // ========== 创建栈 ==========
            
            // 创建空栈
            var stack = new Stack<string>();
            
            // 指定初始容量
            var capacityStack = new Stack<int>(100);
            
            // ========== 压栈操作 ==========
            
            // Push: 压栈
            stack.Push("底部");
            stack.Push("中间");
            stack.Push("顶部");
            Debug.Log($"压栈后数量: {stack.Count}");
            
            // ========== 出栈操作 ==========
            
            // Peek: 查看栈顶元素（不移除）
            string peeked = stack.Peek();
            Debug.Log($"栈顶元素: {peeked}");
            Debug.Log($"查看后数量: {stack.Count}");
            
            // Pop: 出栈
            string popped = stack.Pop();
            Debug.Log($"出栈元素: {popped}");
            Debug.Log($"出栈后数量: {stack.Count}");
            
            // ========== 检查操作 ==========
            
            // Contains: 检查是否包含元素
            bool contains = stack.Contains("中间");
            Debug.Log($"是否包含'中间': {contains}");
            
            // ========== 遍历操作 ==========
            
            Debug.Log("栈中剩余元素:");
            foreach (string item in stack)
            {
                Debug.Log($"元素: {item}");
            }
            
            // ========== 清空操作 ==========
            
            // Clear: 清空栈
            stack.Clear();
            Debug.Log($"清空后数量: {stack.Count}");
            
            // ========== 高级操作 ==========
            
            // 批量压栈
            var numberStack = new Stack<int>();
            for (int i = 1; i <= 5; i++)
            {
                numberStack.Push(i);
            }
            
            // 批量出栈
            Debug.Log("批量出栈:");
            while (numberStack.Count > 0)
            {
                int num = numberStack.Pop();
                Debug.Log($"出栈: {num}");
            }
        }

        // ================= LinkedList<T> 双向链表 =================
        /// <summary>
        /// LinkedList<T> 双向链表示例
        /// LinkedList提供O(1)的插入和删除操作，但随机访问较慢
        /// </summary>
        private void LinkedListExample()
        {
            Debug.Log("--- LinkedList<T> 双向链表示例 ---");
            
            // ========== 创建链表 ==========
            
            // 创建空链表
            var linkedList = new LinkedList<string>();
            
            // ========== 添加节点操作 ==========
            
            // AddFirst: 在开头添加节点
            linkedList.AddFirst("第一个");
            linkedList.AddFirst("新的第一个");
            
            // AddLast: 在末尾添加节点
            linkedList.AddLast("最后一个");
            linkedList.AddLast("新的最后一个");
            
            // AddAfter: 在指定节点后添加
            var firstNode = linkedList.First;
            linkedList.AddAfter(firstNode, "在第一个之后");
            
            // AddBefore: 在指定节点前添加
            var lastNode = linkedList.Last;
            linkedList.AddBefore(lastNode, "在最后一个之前");
            
            Debug.Log($"添加节点后数量: {linkedList.Count}");
            
            // ========== 访问节点操作 ==========
            
            // First/Last: 获取首尾节点
            Debug.Log($"第一个节点: {linkedList.First?.Value}");
            Debug.Log($"最后一个节点: {linkedList.Last?.Value}");
            
            // 遍历节点
            Debug.Log("所有节点:");
            foreach (string item in linkedList)
            {
                Debug.Log($"节点: {item}");
            }
            
            // ========== 查找操作 ==========
            
            // Find: 查找第一个匹配的节点
            var foundNode = linkedList.Find("第一个");
            if (foundNode != null)
            {
                Debug.Log($"找到节点: {foundNode.Value}");
            }
            
            // FindLast: 查找最后一个匹配的节点
            var lastFoundNode = linkedList.FindLast("最后一个");
            if (lastFoundNode != null)
            {
                Debug.Log($"找到最后一个节点: {lastFoundNode.Value}");
            }
            
            // Contains: 检查是否包含元素
            bool contains = linkedList.Contains("第一个");
            Debug.Log($"是否包含'第一个': {contains}");
            
            // ========== 移除操作 ==========
            
            // Remove: 移除第一个匹配的元素
            bool removed = linkedList.Remove("第一个");
            Debug.Log($"移除'第一个'成功: {removed}");
            
            // RemoveFirst: 移除第一个节点
            linkedList.RemoveFirst();
            Debug.Log($"移除第一个节点后数量: {linkedList.Count}");
            
            // RemoveLast: 移除最后一个节点
            linkedList.RemoveLast();
            Debug.Log($"移除最后一个节点后数量: {linkedList.Count}");
            
            // ========== 清空操作 ==========
            
            // Clear: 清空链表
            linkedList.Clear();
            Debug.Log($"清空后数量: {linkedList.Count}");
        }

        // ================= LINQ查询操作 =================
        /// <summary>
        /// LINQ查询操作示例
        /// LINQ提供强大的查询和操作集合的功能
        /// </summary>
        private void LinqExample()
        {
            Debug.Log("--- LINQ查询操作示例 ---");
            
            // ========== 准备测试数据 ==========
            
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 25, City = "北京" },
                new Person { Name = "Bob", Age = 30, City = "上海" },
                new Person { Name = "Charlie", Age = 35, City = "北京" },
                new Person { Name = "David", Age = 28, City = "广州" },
                new Person { Name = "Eve", Age = 32, City = "上海" }
            };
            
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            // ========== 基本查询操作 ==========
            
            // Where: 过滤
            var adults = people.Where(p => p.Age >= 30);
            Debug.Log($"成年人数量: {adults.Count()}");
            
            // Select: 投影
            var names = people.Select(p => p.Name);
            Debug.Log($"所有姓名: {string.Join(", ", names)}");
            
            // OrderBy: 排序
            var sortedByAge = people.OrderBy(p => p.Age);
            Debug.Log("按年龄排序:");
            foreach (var person in sortedByAge)
            {
                Debug.Log($"{person.Name}: {person.Age}");
            }
            
            // ========== 聚合操作 ==========
            
            // Count: 计数
            int totalPeople = people.Count();
            Debug.Log($"总人数: {totalPeople}");
            
            // Sum: 求和
            int totalAge = people.Sum(p => p.Age);
            Debug.Log($"总年龄: {totalAge}");
            
            // Average: 平均值
            double avgAge = people.Average(p => p.Age);
            Debug.Log($"平均年龄: {avgAge:F2}");
            
            // Max/Min: 最大值/最小值
            int maxAge = people.Max(p => p.Age);
            int minAge = people.Min(p => p.Age);
            Debug.Log($"最大年龄: {maxAge}, 最小年龄: {minAge}");
            
            // ========== 分组操作 ==========
            
            // GroupBy: 分组
            var groupByCity = people.GroupBy(p => p.City);
            Debug.Log("按城市分组:");
            foreach (var group in groupByCity)
            {
                Debug.Log($"{group.Key}: {group.Count()}人");
                foreach (var person in group)
                {
                    Debug.Log($"  - {person.Name}");
                }
            }
            
            // ========== 连接操作 ==========
            
            // Join: 连接
            var cities = new List<string> { "北京", "上海", "深圳" };
            var joinResult = people.Join(cities, p => p.City, c => c, (p, c) => new { p.Name, City = c });
            Debug.Log("连接结果:");
            foreach (var item in joinResult)
            {
                Debug.Log($"{item.Name} - {item.City}");
            }
            
            // ========== 集合操作 ==========
            
            // Distinct: 去重
            var uniqueCities = people.Select(p => p.City).Distinct();
            Debug.Log($"唯一城市: {string.Join(", ", uniqueCities)}");
            
            // Union: 并集
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 3, 4, 5 };
            var union = set1.Union(set2);
            Debug.Log($"并集: {string.Join(", ", union)}");
            
            // Intersect: 交集
            var intersect = set1.Intersect(set2);
            Debug.Log($"交集: {string.Join(", ", intersect)}");
            
            // Except: 差集
            var except = set1.Except(set2);
            Debug.Log($"差集: {string.Join(", ", except)}");
            
            // ========== 分页操作 ==========
            
            // Skip/Take: 分页
            var page1 = numbers.Skip(0).Take(3);
            var page2 = numbers.Skip(3).Take(3);
            Debug.Log($"第一页: {string.Join(", ", page1)}");
            Debug.Log($"第二页: {string.Join(", ", page2)}");
            
            // ========== 条件操作 ==========
            
            // Any: 是否存在满足条件的元素
            bool hasOldPerson = people.Any(p => p.Age > 40);
            Debug.Log($"是否有40岁以上的人: {hasOldPerson}");
            
            // All: 是否所有元素都满足条件
            bool allAdults = people.All(p => p.Age >= 18);
            Debug.Log($"是否都是成年人: {allAdults}");
            
            // First/FirstOrDefault: 获取第一个元素
            var firstPerson = people.First();
            var firstOrDefault = people.FirstOrDefault(p => p.Age > 40);
            Debug.Log($"第一个: {firstPerson.Name}");
            Debug.Log($"第一个40岁以上的: {firstOrDefault?.Name ?? "无"}");
            
            // Single/SingleOrDefault: 获取唯一元素
            var singlePerson = people.Single(p => p.Name == "Alice");
            Debug.Log($"唯一的Alice: {singlePerson.Name}");
        }

        // ================= 高级集合操作 =================
        /// <summary>
        /// 高级集合操作示例
        /// 包括线程安全集合、只读集合、并发集合等
        /// </summary>
        private void AdvancedCollectionsExample()
        {
            Debug.Log("--- 高级集合操作示例 ---");
            
            // ========== 只读集合 ==========
            
            var originalList = new List<int> { 1, 2, 3, 4, 5 };
            var readOnlyList = originalList.AsReadOnly();
            Debug.Log($"只读集合数量: {readOnlyList.Count}");
            
            // 尝试修改只读集合会抛出异常
            try
            {
                // readOnlyList.Add(6); // 这会抛出异常
                Debug.Log("只读集合不能修改");
            }
            catch (Exception ex)
            {
                Debug.LogError($"修改只读集合出错: {ex.Message}");
            }
            
            // ========== 数组操作 ==========
            
            var array = new int[] { 1, 2, 3, 4, 5 };
            
            // Array.Sort: 数组排序
            Array.Sort(array);
            Debug.Log($"排序后数组: {string.Join(", ", array)}");
            
            // Array.Reverse: 数组反转
            Array.Reverse(array);
            Debug.Log($"反转后数组: {string.Join(", ", array)}");
            
            // Array.Find: 查找元素
            int found = Array.Find(array, x => x > 3);
            Debug.Log($"找到大于3的元素: {found}");
            
            // Array.FindAll: 查找所有满足条件的元素
            var foundAll = Array.FindAll(array, x => x % 2 == 0);
            Debug.Log($"所有偶数: {string.Join(", ", foundAll)}");
            
            // ========== 集合初始化器 ==========
            
            // 集合初始化器语法
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var dict = new Dictionary<string, int>
            {
                ["A"] = 1,
                ["B"] = 2,
                ["C"] = 3
            };
            var set = new HashSet<int> { 1, 2, 3, 4, 5 };
            
            Debug.Log($"初始化器创建的集合数量: List={list.Count}, Dict={dict.Count}, Set={set.Count}");
            
            // ========== 集合转换 ==========
            
            // 数组转List
            var arrayToList = array.ToList();
            Debug.Log($"数组转List: {arrayToList.Count}个元素");
            
            // List转数组
            var listToArray = list.ToArray();
            Debug.Log($"List转数组: {listToArray.Length}个元素");
            
            // 集合转Dictionary
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 25 },
                new Person { Name = "Bob", Age = 30 }
            };
            var peopleDict = people.ToDictionary(p => p.Name, p => p.Age);
            Debug.Log($"集合转Dictionary: {peopleDict.Count}个键值对");
            
            // ========== 集合比较 ==========
            
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 1, 2, 3 };
            var list3 = new List<int> { 3, 2, 1 };
            
            // 序列相等比较
            bool equal1 = list1.SequenceEqual(list2);
            bool equal2 = list1.SequenceEqual(list3);
            Debug.Log($"list1和list2相等: {equal1}");
            Debug.Log($"list1和list3相等: {equal2}");
            
            // ========== 集合性能优化 ==========
            
            // 预分配容量
            var optimizedList = new List<int>(1000);
            Debug.Log($"预分配容量的List容量: {optimizedList.Capacity}");
            
            // 使用HashSet提高查找性能
            var largeSet = new HashSet<int>();
            for (int i = 0; i < 10000; i++)
            {
                largeSet.Add(i);
            }
            bool fastLookup = largeSet.Contains(5000);
            Debug.Log($"HashSet快速查找: {fastLookup}");
        }
    }

    /// <summary>
    /// 用于演示的Person类
    /// </summary>
    [System.Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Age}岁, {City})";
        }
    }
} 