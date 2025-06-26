// CollectionsExample.cs
// .NET集合API使用详解示例
// 包含List、Dictionary、HashSet、Queue、Stack、LinkedList、Linq等常用集合类型
// 每个方法、关键步骤、枚举值均有详细中文注释
// 适合.NET初学者学习和查阅

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DotNet.Collections
{
    /// <summary>
    /// .NET集合API使用详解示例
    /// 演示List、Dictionary、HashSet、Queue、Stack、LinkedList、Linq等常用集合类型
    /// </summary>
    public class CollectionsExample : MonoBehaviour
    {
        [Header("集合示例")]
        [Tooltip("是否自动运行所有示例代码")]
        [SerializeField] private bool runExamples = true;

        private void Start()
        {
            if (runExamples)
            {
                RunAllExamples();
            }
        }

        /// <summary>
        /// 运行所有集合相关示例
        /// 按顺序执行：List -> Dictionary -> HashSet -> Queue -> Stack -> LinkedList -> Linq -> 高级集合操作
        /// </summary>
        private void RunAllExamples()
        {
            Debug.Log("=== .NET集合API示例开始 ===");
            
            ListExample();      // List<T> 动态数组
            DictionaryExample(); // Dictionary<TKey, TValue> 键值对集合
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
        /// </summary>
        private void ListExample()
        {
            Debug.Log("--- List<T> 动态数组示例 ---");
            
            // ========== 创建和初始化 ==========
            
            // 创建空List
            var emptyList = new List<int>();
            
            // 创建并初始化List
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            var names = new List<string> { "Alice", "Bob", "Charlie" };
            
            // 指定初始容量（优化性能）
            var capacityList = new List<int>(100);
            Debug.Log($"指定容量的List: {capacityList.Capacity}");
            
            // ========== 添加元素操作 ==========
            
            // Add: 在末尾添加单个元素
            numbers.Add(6);
            Debug.Log($"添加元素6后: {string.Join(", ", numbers)}");
            
            // Insert: 在指定位置插入元素
            numbers.Insert(0, 0);
            Debug.Log($"在位置0插入0后: {string.Join(", ", numbers)}");
            
            // AddRange: 在末尾添加多个元素
            numbers.AddRange(new int[] { 7, 8, 9 });
            Debug.Log($"添加数组后: {string.Join(", ", numbers)}");
            
            // InsertRange: 在指定位置插入多个元素
            numbers.InsertRange(3, new int[] { 10, 11 });
            Debug.Log($"在位置3插入数组后: {string.Join(", ", numbers)}");
            
            // ========== 访问元素操作 ==========
            
            // 索引访问
            Debug.Log($"第一个元素: {numbers[0]}");
            Debug.Log($"最后一个元素: {numbers[^1]}"); // C# 8.0 索引语法
            Debug.Log($"倒数第二个元素: {numbers[^2]}");
            
            // 获取元素数量
            Debug.Log($"List长度: {numbers.Count}");
            Debug.Log($"List容量: {numbers.Capacity}");
            
            // ========== 查找元素操作 ==========
            
            // IndexOf: 查找元素的第一个索引
            int index = numbers.IndexOf(5);
            Debug.Log($"元素5的索引: {index}");
            
            // LastIndexOf: 查找元素的最后一个索引
            numbers.Add(5); // 添加重复元素
            int lastIndex = numbers.LastIndexOf(5);
            Debug.Log($"元素5的最后一个索引: {lastIndex}");
            
            // Contains: 检查是否包含元素
            bool contains = numbers.Contains(10);
            Debug.Log($"是否包含10: {contains}");
            
            // Exists: 使用条件查找元素
            bool exists = numbers.Exists(x => x > 10);
            Debug.Log($"是否存在大于10的元素: {exists}");
            
            // Find: 查找第一个满足条件的元素
            int found = numbers.Find(x => x % 2 == 0);
            Debug.Log($"第一个偶数: {found}");
            
            // FindAll: 查找所有满足条件的元素
            var evenNumbers = numbers.FindAll(x => x % 2 == 0);
            Debug.Log($"所有偶数: {string.Join(", ", evenNumbers)}");
            
            // ========== 排序和反转操作 ==========
            
            // Sort: 排序（默认升序）
            numbers.Sort();
            Debug.Log($"排序后: {string.Join(", ", numbers)}");
            
            // Sort: 自定义排序（降序）
            numbers.Sort((a, b) => b.CompareTo(a));
            Debug.Log($"降序排序后: {string.Join(", ", numbers)}");
            
            // Reverse: 反转
            numbers.Reverse();
            Debug.Log($"反转后: {string.Join(", ", numbers)}");
            
            // ========== 移除元素操作 ==========
            
            // Remove: 移除第一个匹配的元素
            bool removed = numbers.Remove(5);
            Debug.Log($"移除元素5成功: {removed}");
            
            // RemoveAt: 移除指定位置的元素
            numbers.RemoveAt(0);
            Debug.Log($"移除位置0的元素后: {string.Join(", ", numbers)}");
            
            // RemoveRange: 移除指定范围的元素
            numbers.RemoveRange(0, 2);
            Debug.Log($"移除范围[0,2)后: {string.Join(", ", numbers)}");
            
            // RemoveAll: 移除所有满足条件的元素
            int removedCount = numbers.RemoveAll(x => x < 5);
            Debug.Log($"移除了{removedCount}个小于5的元素");
            
            // ========== 转换操作 ==========
            
            // ToArray: 转换为数组
            int[] array = numbers.ToArray();
            Debug.Log($"转换为数组: {string.Join(", ", array)}");
            
            // ToList: 创建副本
            var copy = numbers.ToList();
            Debug.Log($"创建副本: {string.Join(", ", copy)}");
            
            // ========== 清空操作 ==========
            
            // Clear: 清空所有元素
            numbers.Clear();
            Debug.Log($"清空后长度: {numbers.Count}");
            
            // TrimExcess: 释放多余容量
            numbers.TrimExcess();
            Debug.Log($"释放容量后: {numbers.Capacity}");
        }

        // ================= Dictionary<TKey, TValue> 键值对集合 =================
        /// <summary>
        /// Dictionary<TKey, TValue> 键值对集合示例
        /// Dictionary提供快速的键值对查找，键必须唯一
        /// </summary>
        private void DictionaryExample()
        {
            Debug.Log("--- Dictionary<TKey, TValue> 键值对集合示例 ---");
            
            // ========== 创建和初始化 ==========
            
            // 创建空字典
            var emptyDict = new Dictionary<string, int>();
            
            // 创建并初始化字典
            var scores = new Dictionary<string, int>
            {
                { "Alice", 95 },
                { "Bob", 87 },
                { "Charlie", 92 }
            };
            
            // 指定初始容量
            var capacityDict = new Dictionary<string, int>(100);
            
            // ========== 添加键值对操作 ==========
            
            // 索引器添加
            scores["David"] = 88;
            Debug.Log($"使用索引器添加David: {scores["David"]}");
            
            // Add方法添加
            scores.Add("Eve", 91);
            Debug.Log($"使用Add方法添加Eve: {scores["Eve"]}");
            
            // TryAdd: 安全添加（.NET Core 2.0+）
            bool added = scores.TryAdd("Frank", 89);
            Debug.Log($"TryAdd Frank成功: {added}");
            
            // ========== 访问值操作 ==========
            
            // 索引器访问
            int aliceScore = scores["Alice"];
            Debug.Log($"Alice的分数: {aliceScore}");
            
            // TryGetValue: 安全获取值
            if (scores.TryGetValue("Bob", out int bobScore))
            {
                Debug.Log($"Bob的分数: {bobScore}");
            }
            
            // GetValueOrDefault: 获取值或默认值
            int defaultScore = scores.GetValueOrDefault("Unknown", -1);
            Debug.Log($"未知学生的分数: {defaultScore}");
            
            // ========== 检查操作 ==========
            
            // ContainsKey: 检查键是否存在
            bool hasKey = scores.ContainsKey("Charlie");
            Debug.Log($"是否包含Charlie: {hasKey}");
            
            // ContainsValue: 检查值是否存在
            bool hasValue = scores.ContainsValue(95);
            Debug.Log($"是否包含分数95: {hasValue}");
            
            // ========== 遍历操作 ==========
            
            // 遍历键值对
            Debug.Log("所有学生分数:");
            foreach (var kvp in scores)
            {
                Debug.Log($"{kvp.Key}: {kvp.Value}");
            }
            
            // 只遍历键
            Debug.Log("所有学生:");
            foreach (string name in scores.Keys)
            {
                Debug.Log($"学生: {name}");
            }
            
            // 只遍历值
            Debug.Log("所有分数:");
            foreach (int score in scores.Values)
            {
                Debug.Log($"分数: {score}");
            }
            
            // ========== 更新操作 ==========
            
            // 更新现有值
            scores["Alice"] = 98;
            Debug.Log($"更新Alice分数后: {scores["Alice"]}");
            
            // 条件更新
            if (scores.ContainsKey("Bob"))
            {
                scores["Bob"] += 5;
                Debug.Log($"Bob加分后: {scores["Bob"]}");
            }
            
            // ========== 移除操作 ==========
            
            // Remove: 移除键值对
            bool removed = scores.Remove("Charlie");
            Debug.Log($"移除Charlie成功: {removed}");
            
            // 获取数量
            Debug.Log($"字典中的键值对数量: {scores.Count}");
            
            // ========== 高级操作 ==========
            
            // 获取最大值和最小值
            var maxScore = scores.Values.Max();
            var minScore = scores.Values.Min();
            Debug.Log($"最高分: {maxScore}, 最低分: {minScore}");
            
            // 按值排序
            var sortedScores = scores.OrderByDescending(x => x.Value);
            Debug.Log("按分数排序:");
            foreach (var item in sortedScores)
            {
                Debug.Log($"{item.Key}: {item.Value}");
            }
            
            // 清空字典
            scores.Clear();
            Debug.Log($"清空后数量: {scores.Count}");
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