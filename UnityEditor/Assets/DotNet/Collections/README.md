# .NET Collections API 参考文档

本文档基于 `CollectionsExample.cs` 文件，详细介绍了 .NET Collections 相关的所有常用 API。

## 目录
- [List<T> 动态数组](#listt-动态数组)
- [Dictionary<TKey, TValue> 键值对集合](#dictionarytkey-tvalue-键值对集合)
- [ConcurrentDictionary<TKey, TValue> 线程安全字典](#concurrentdictionarytkey-tvalue-线程安全字典)
- [HashSet<T> 无序不重复集合](#hashsett-无序不重复集合)
- [Queue<T> 队列（先进先出）](#queuet-队列先进先出)
- [Stack<T> 栈（后进先出）](#stackt-栈后进先出)
- [LinkedList<T> 双向链表](#linkedlistt-双向链表)
- [LINQ查询操作](#linq查询操作)
- [高级集合操作](#高级集合操作)

---

## List<T> 动态数组

### 创建和初始化

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new List<T>()` | 创建空List |
| `new List<T>(capacity)` | 创建指定初始容量的List |
| `new List<T> { item1, item2, ... }` | 创建并初始化List |

### 添加元素操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `List<T>.Add(item)` | 在末尾添加单个元素 |
| `List<T>.Insert(index, item)` | 在指定位置插入元素 |
| `List<T>.AddRange(collection)` | 在末尾添加多个元素 |
| `List<T>.InsertRange(index, collection)` | 在指定位置插入多个元素 |

### 访问元素操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `List<T>[index]` | 索引访问元素 |
| `List<T>[^index]` | 从末尾索引访问（C# 8.0） |
| `List<T>.Count` | 获取元素数量 |
| `List<T>.Capacity` | 获取当前容量 |

### 查找元素操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `List<T>.IndexOf(item)` | 查找元素的第一个索引 |
| `List<T>.LastIndexOf(item)` | 查找元素的最后一个索引 |
| `List<T>.Contains(item)` | 检查是否包含元素 |
| `List<T>.Exists(predicate)` | 使用条件查找元素 |
| `List<T>.Find(predicate)` | 查找第一个满足条件的元素 |
| `List<T>.FindAll(predicate)` | 查找所有满足条件的元素 |

### 排序和反转操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `List<T>.Sort()` | 排序（默认升序） |
| `List<T>.Sort(comparison)` | 自定义排序 |
| `List<T>.Reverse()` | 反转列表 |

### 移除元素操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `List<T>.Remove(item)` | 移除第一个匹配的元素 |
| `List<T>.RemoveAt(index)` | 移除指定位置的元素 |
| `List<T>.RemoveRange(index, count)` | 移除指定范围的元素 |
| `List<T>.RemoveAll(predicate)` | 移除所有满足条件的元素 |

### 转换操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `List<T>.ToArray()` | 转换为数组 |
| `List<T>.ToList()` | 创建副本 |

### 清空操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `List<T>.Clear()` | 清空所有元素 |
| `List<T>.TrimExcess()` | 释放多余容量 |

---

## Dictionary<TKey, TValue> 键值对集合

### 创建和初始化

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Dictionary<TKey, TValue>()` | 创建空字典 |
| `new Dictionary<TKey, TValue>(capacity)` | 创建指定初始容量的字典 |
| `new Dictionary<TKey, TValue> { { key, value }, ... }` | 创建并初始化字典 |

### 添加键值对操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Dictionary[key] = value` | 使用索引器添加 |
| `Dictionary.Add(key, value)` | 使用Add方法添加 |
| `Dictionary.TryAdd(key, value)` | 安全添加（.NET Core 2.0+） |

### 访问值操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Dictionary[key]` | 索引器访问值 |
| `Dictionary.TryGetValue(key, out value)` | 安全获取值 |
| `Dictionary.GetValueOrDefault(key, defaultValue)` | 获取值或默认值 |

### 检查操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Dictionary.ContainsKey(key)` | 检查键是否存在 |
| `Dictionary.ContainsValue(value)` | 检查值是否存在 |

### 遍历操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Dictionary.Keys` | 获取所有键的集合 |
| `Dictionary.Values` | 获取所有值的集合 |
| `Dictionary` | 遍历键值对 |

### 更新操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Dictionary[key] = newValue` | 更新现有值 |

### 移除操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Dictionary.Remove(key)` | 移除键值对 |
| `Dictionary.Clear()` | 清空字典 |

### 高级操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Dictionary.Values.Max()` | 获取值的最大值 |
| `Dictionary.Values.Min()` | 获取值的最小值 |
| `Dictionary.OrderByDescending(x => x.Value)` | 按值排序 |

---

## ConcurrentDictionary<TKey, TValue> 线程安全字典

### 创建和初始化

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new ConcurrentDictionary<TKey, TValue>()` | 创建空并发字典 |
| `new ConcurrentDictionary<TKey, TValue>(capacity)` | 创建指定初始容量的并发字典 |
| `new ConcurrentDictionary<TKey, TValue> { [key] = value, ... }` | 创建并初始化并发字典 |

### 线程安全的添加和更新操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentDictionary.TryAdd(key, value)` | 尝试添加键值对（线程安全） |
| `ConcurrentDictionary.TryUpdate(key, newValue, comparisonValue)` | 尝试更新值（线程安全） |
| `ConcurrentDictionary.AddOrUpdate(key, addValue, updateFactory)` | 添加或更新（线程安全） |
| `ConcurrentDictionary.GetOrAdd(key, value)` | 获取或添加（线程安全） |

### 线程安全的访问操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentDictionary.TryGetValue(key, out value)` | 尝试获取值（线程安全） |
| `ConcurrentDictionary[key]` | 索引器访问（线程安全） |
| `ConcurrentDictionary.ContainsKey(key)` | 检查键是否存在（线程安全） |

### 线程安全的删除操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentDictionary.TryRemove(key, out value)` | 尝试删除键值对（线程安全） |

### 批量操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentDictionary.ToArray()` | 转换为数组（线程安全） |
| `ConcurrentDictionary.Keys` | 获取所有键的集合（线程安全） |
| `ConcurrentDictionary.Values` | 获取所有值的集合（线程安全） |
| `ConcurrentDictionary.Count` | 获取键值对数量（线程安全） |
| `ConcurrentDictionary.IsEmpty` | 检查是否为空（线程安全） |

### 高级操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ConcurrentDictionary.GetEnumerator()` | 获取枚举器（线程安全） |
| `ConcurrentDictionary.Clear()` | 清空字典（线程安全） |

### 性能特点

- **线程安全**: 支持多线程并发读写
- **无锁设计**: 使用无锁算法，性能优异
- **原子操作**: 避免竞态条件
- **O(1)平均性能**: 查找、添加、删除操作

### 使用场景

- 多线程环境下的数据共享
- 缓存系统
- 计数器集合
- 状态管理
- 资源加载器

### 注意事项

- 比普通Dictionary稍慢
- 某些操作不是原子性的
- 迭代时可能看到不一致的状态
- 适合高并发场景

---

## HashSet<T> 无序不重复集合

### 创建和初始化

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new HashSet<T>()` | 创建空HashSet |
| `new HashSet<T>(comparer)` | 创建指定比较器的HashSet |
| `new HashSet<T> { item1, item2, ... }` | 创建并初始化HashSet |

### 添加元素操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HashSet<T>.Add(item)` | 添加元素 |
| `HashSet<T>.UnionWith(collection)` | 添加多个元素（并集） |

### 检查操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HashSet<T>.Contains(item)` | 检查是否包含元素 |

### 集合操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HashSet<T>.UnionWith(set)` | 并集操作 |
| `HashSet<T>.IntersectWith(set)` | 交集操作 |
| `HashSet<T>.ExceptWith(set)` | 差集操作 |
| `HashSet<T>.SymmetricExceptWith(set)` | 对称差集操作 |

### 集合关系检查

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HashSet<T>.IsSubsetOf(set)` | 检查是否为子集 |
| `HashSet<T>.IsSupersetOf(set)` | 检查是否为超集 |
| `HashSet<T>.IsProperSubsetOf(set)` | 检查是否为真子集 |
| `HashSet<T>.Overlaps(set)` | 检查是否有重叠 |
| `HashSet<T>.SetEquals(set)` | 检查是否相等 |

### 移除操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HashSet<T>.Remove(item)` | 移除元素 |
| `HashSet<T>.RemoveWhere(predicate)` | 条件移除 |
| `HashSet<T>.Clear()` | 清空集合 |

### 转换操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HashSet<T>.ToArray()` | 转换为数组 |
| `HashSet<T>.ToList()` | 转换为List |

---

## Queue<T> 队列（先进先出）

### 创建队列

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Queue<T>()` | 创建空队列 |
| `new Queue<T>(capacity)` | 创建指定初始容量的队列 |

### 入队操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Queue<T>.Enqueue(item)` | 入队 |

### 出队操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Queue<T>.Peek()` | 查看队首元素（不移除） |
| `Queue<T>.Dequeue()` | 出队 |

### 检查操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Queue<T>.Contains(item)` | 检查是否包含元素 |
| `Queue<T>.Count` | 获取队列长度 |

### 遍历操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Queue<T>` | 遍历队列元素 |

### 清空操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Queue<T>.Clear()` | 清空队列 |

---

## Stack<T> 栈（后进先出）

### 创建栈

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Stack<T>()` | 创建空栈 |
| `new Stack<T>(capacity)` | 创建指定初始容量的栈 |

### 压栈操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Stack<T>.Push(item)` | 压栈 |

### 出栈操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Stack<T>.Peek()` | 查看栈顶元素（不移除） |
| `Stack<T>.Pop()` | 出栈 |

### 检查操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Stack<T>.Contains(item)` | 检查是否包含元素 |
| `Stack<T>.Count` | 获取栈长度 |

### 遍历操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Stack<T>` | 遍历栈元素 |

### 清空操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Stack<T>.Clear()` | 清空栈 |

---

## LinkedList<T> 双向链表

### 创建链表

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new LinkedList<T>()` | 创建空链表 |

### 添加节点操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `LinkedList<T>.AddFirst(item)` | 在开头添加节点 |
| `LinkedList<T>.AddLast(item)` | 在末尾添加节点 |
| `LinkedList<T>.AddAfter(node, item)` | 在指定节点后添加 |
| `LinkedList<T>.AddBefore(node, item)` | 在指定节点前添加 |

### 访问节点操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `LinkedList<T>.First` | 获取第一个节点 |
| `LinkedList<T>.Last` | 获取最后一个节点 |
| `LinkedList<T>.Count` | 获取节点数量 |

### 查找操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `LinkedList<T>.Find(item)` | 查找第一个匹配的节点 |
| `LinkedList<T>.FindLast(item)` | 查找最后一个匹配的节点 |
| `LinkedList<T>.Contains(item)` | 检查是否包含元素 |

### 移除操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `LinkedList<T>.Remove(item)` | 移除第一个匹配的元素 |
| `LinkedList<T>.RemoveFirst()` | 移除第一个节点 |
| `LinkedList<T>.RemoveLast()` | 移除最后一个节点 |
| `LinkedList<T>.Clear()` | 清空链表 |

---

## LINQ查询操作

### 基本查询操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.Where(predicate)` | 过滤元素 |
| `IEnumerable<T>.Select(selector)` | 投影转换 |
| `IEnumerable<T>.OrderBy(keySelector)` | 升序排序 |
| `IEnumerable<T>.OrderByDescending(keySelector)` | 降序排序 |
| `IEnumerable<T>.ThenBy(keySelector)` | 次要排序 |
| `IEnumerable<T>.ThenByDescending(keySelector)` | 次要降序排序 |

### 聚合操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.Count()` | 计数 |
| `IEnumerable<T>.Sum(selector)` | 求和 |
| `IEnumerable<T>.Average(selector)` | 平均值 |
| `IEnumerable<T>.Max(selector)` | 最大值 |
| `IEnumerable<T>.Min(selector)` | 最小值 |

### 分组操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.GroupBy(keySelector)` | 分组 |
| `IGrouping<TKey, TElement>.Key` | 获取分组键 |
| `IGrouping<TKey, TElement>.Count()` | 获取分组数量 |

### 连接操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.Join(inner, outerKeySelector, innerKeySelector, resultSelector)` | 内连接 |
| `IEnumerable<T>.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector)` | 分组连接 |

### 集合操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.Distinct()` | 去重 |
| `IEnumerable<T>.Union(second)` | 并集 |
| `IEnumerable<T>.Intersect(second)` | 交集 |
| `IEnumerable<T>.Except(second)` | 差集 |

### 分页操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.Skip(count)` | 跳过指定数量 |
| `IEnumerable<T>.Take(count)` | 获取指定数量 |

### 条件操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.Any(predicate)` | 是否存在满足条件的元素 |
| `IEnumerable<T>.All(predicate)` | 是否所有元素都满足条件 |
| `IEnumerable<T>.First()` | 获取第一个元素 |
| `IEnumerable<T>.FirstOrDefault(predicate)` | 获取第一个满足条件的元素或默认值 |
| `IEnumerable<T>.Single(predicate)` | 获取唯一元素 |
| `IEnumerable<T>.SingleOrDefault(predicate)` | 获取唯一元素或默认值 |

---

## 高级集合操作

### 只读集合

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IList<T>.AsReadOnly()` | 创建只读集合 |

### 数组操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Array.Sort(array)` | 数组排序 |
| `Array.Reverse(array)` | 数组反转 |
| `Array.Find(array, predicate)` | 查找元素 |
| `Array.FindAll(array, predicate)` | 查找所有满足条件的元素 |

### 集合初始化器

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new List<T> { item1, item2, ... }` | List初始化器 |
| `new Dictionary<TKey, TValue> { [key] = value, ... }` | Dictionary初始化器 |
| `new HashSet<T> { item1, item2, ... }` | HashSet初始化器 |

### 集合转换

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.ToList()` | 转换为List |
| `IEnumerable<T>.ToArray()` | 转换为数组 |
| `IEnumerable<T>.ToDictionary(keySelector, elementSelector)` | 转换为Dictionary |
| `IEnumerable<T>.ToHashSet()` | 转换为HashSet |

### 集合比较

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IEnumerable<T>.SequenceEqual(second)` | 序列相等比较 |

### 集合性能优化

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new List<T>(capacity)` | 预分配容量 |
| `HashSet<T>.Contains(item)` | 快速查找 |

---

## 使用注意事项

1. **选择合适的集合类型**：根据使用场景选择最合适的集合类型
2. **性能考虑**：对于频繁查找操作，优先使用HashSet或Dictionary
3. **线程安全**：多线程环境下使用线程安全集合
4. **内存管理**：及时清理不再使用的集合
5. **LINQ性能**：避免在循环中重复执行LINQ查询
6. **集合初始化**：使用集合初始化器简化代码
7. **容量优化**：预知元素数量时指定初始容量
8. **只读集合**：需要保护数据时使用只读集合

---

## 示例代码

完整的示例代码请参考 `CollectionsExample.cs` 文件，其中包含了所有API的详细使用示例和中文注释。 