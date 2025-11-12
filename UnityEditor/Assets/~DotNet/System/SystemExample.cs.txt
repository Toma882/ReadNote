// SystemExample.cs
// .NET System API使用详解示例
// 包含Math、DateTime、Convert、GC、Diagnostics、Security、Globalization、ComponentModel、Configuration等常用功能
// 每个方法、关键步骤、枚举值均有详细中文注释
// 适合.NET初学者学习和查阅

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Runtime;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.OdinInspector;

namespace DotNet.SystemNS
{
    /// <summary>
    /// .NET System API使用详解示例
    /// 演示Math、DateTime、Convert、GC、Diagnostics、Security、Globalization、ComponentModel、Configuration等常用操作
    /// </summary>
    public class SystemExample : MonoBehaviour
    {
        [Header("System示例")]
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
        /// 运行所有System相关示例
        /// </summary>
        private void RunAllExamples()
        {
            Debug.Log("=== .NET System API示例开始 ===");
            MathExample();
            DateTimeExample();
            ConvertExample();
            SpanExample();
            GCExample();
            DiagnosticsExample();
            SecurityExample();
            GlobalizationExample();
            ComponentModelExample();
            AppDomainExample();
            Debug.Log("=== .NET System API示例结束 ===");
        }

        // ================= Math 数学运算 =================
        /// <summary>
        /// Math类常用API演示
        /// 包含数学常量、基本运算、三角函数、对数函数等
        /// </summary>
        private void MathExample()
        {
            Debug.Log("--- Math 数学运算示例 ---");
            
            try
            {
                // ========== 数学常量 ==========
                Debug.Log($"PI: {Math.PI}");
                Debug.Log($"E: {Math.E}");
                Debug.Log($"Tau: {Math.Atanh(1)}");
                
                // ========== 基本运算 ==========
                double sqrt = Math.Sqrt(16);
                double pow = Math.Pow(2, 8);
                double abs = Math.Abs(-123.45);
                double max = Math.Max(10, 20);
                double min = Math.Min(10, 20);
                double floor = Math.Floor(3.7);
                double ceiling = Math.Ceiling(3.2);
                double round = Math.Round(3.14159, 2);
                
                Debug.Log($"Sqrt(16): {sqrt}");
                Debug.Log($"Pow(2,8): {pow}");
                Debug.Log($"Abs(-123.45): {abs}");
                Debug.Log($"Max(10,20): {max}");
                Debug.Log($"Min(10,20): {min}");
                Debug.Log($"Floor(3.7): {floor}");
                Debug.Log($"Ceiling(3.2): {ceiling}");
                Debug.Log($"Round(3.14159,2): {round}");
                
                // ========== 三角函数 ==========
                double sin = Math.Sin(Math.PI / 2);
                double cos = Math.Cos(0);
                double tan = Math.Tan(Math.PI / 4);
                double asin = Math.Asin(1);
                double acos = Math.Acos(0);
                double atan = Math.Atan(1);
                
                Debug.Log($"Sin(π/2): {sin}");
                Debug.Log($"Cos(0): {cos}");
                Debug.Log($"Tan(π/4): {tan}");
                Debug.Log($"Asin(1): {asin}");
                Debug.Log($"Acos(0): {acos}");
                Debug.Log($"Atan(1): {atan}");
                
                // ========== 对数函数 ==========
                double log = Math.Log(100);
                double log10 = Math.Log10(100);
                double exp = Math.Exp(1);
                
                Debug.Log($"Log(100): {log}");
                Debug.Log($"Log10(100): {log10}");
                Debug.Log($"Exp(1): {exp}");
                
                // ========== 其他数学函数 ==========
                double sign = Math.Sign(-5);
                double truncate = Math.Truncate(3.7);
                double remainder = Math.IEEERemainder(10, 3);
                
                Debug.Log($"Sign(-5): {sign}");
                Debug.Log($"Truncate(3.7): {truncate}");
                Debug.Log($"IEEERemainder(10,3): {remainder}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Math示例出错: {ex.Message}");
            }
        }

        // ================= DateTime 日期时间 =================
        /// <summary>
        /// DateTime和TimeSpan常用API演示
        /// 包含日期时间创建、格式化、计算、时区等操作
        /// </summary>
        private void DateTimeExample()
        {
            Debug.Log("--- DateTime 日期时间示例 ---");
            
            try
            {
                // ========== DateTime创建和获取 ==========
                DateTime now = DateTime.Now;
                DateTime utcNow = DateTime.UtcNow;
                DateTime today = DateTime.Today;
                DateTime minValue = DateTime.MinValue;
                DateTime maxValue = DateTime.MaxValue;
                
                Debug.Log($"当前时间: {now}");
                Debug.Log($"UTC时间: {utcNow}");
                Debug.Log($"今天: {today}");
                Debug.Log($"最小时间: {minValue}");
                Debug.Log($"最大时间: {maxValue}");
                
                // ========== 日期时间计算 ==========
                DateTime tomorrow = now.AddDays(1);
                DateTime yesterday = now.AddDays(-1);
                DateTime nextWeek = now.AddDays(7);
                DateTime nextMonth = now.AddMonths(1);
                DateTime nextYear = now.AddYears(1);
                
                Debug.Log($"明天: {tomorrow}");
                Debug.Log($"昨天: {yesterday}");
                Debug.Log($"下周: {nextWeek}");
                Debug.Log($"下月: {nextMonth}");
                Debug.Log($"明年: {nextYear}");
                
                // ========== 时间间隔计算 ==========
                TimeSpan diff = tomorrow - now;
                TimeSpan oneHour = TimeSpan.FromHours(1);
                TimeSpan oneDay = TimeSpan.FromDays(1);
                TimeSpan oneWeek = TimeSpan.FromDays(7);
                
                Debug.Log($"到明天的时间间隔: {diff}");
                Debug.Log($"一小时: {oneHour}");
                Debug.Log($"一天: {oneDay}");
                Debug.Log($"一周: {oneWeek}");
                
                // ========== 日期时间格式化 ==========
                string shortDate = now.ToString("yyyy-MM-dd");
                string longDate = now.ToString("yyyy年MM月dd日");
                string timeOnly = now.ToString("HH:mm:ss");
                string fullDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                string customFormat = now.ToString("MM/dd/yyyy HH:mm");
                
                Debug.Log($"短日期: {shortDate}");
                Debug.Log($"长日期: {longDate}");
                Debug.Log($"时间: {timeOnly}");
                Debug.Log($"完整日期时间: {fullDateTime}");
                Debug.Log($"自定义格式: {customFormat}");
                
                // ========== 日期时间解析 ==========
                string dateStr = "2023-12-25 14:30:00";
                DateTime parsedDate = DateTime.Parse(dateStr);
                DateTime.TryParse("2023-12-25", out DateTime tryParsedDate);
                
                Debug.Log($"解析的日期: {parsedDate}");
                Debug.Log($"TryParse结果: {tryParsedDate}");
                
                // ========== 日期时间属性 ==========
                Debug.Log($"年: {now.Year}");
                Debug.Log($"月: {now.Month}");
                Debug.Log($"日: {now.Day}");
                Debug.Log($"时: {now.Hour}");
                Debug.Log($"分: {now.Minute}");
                Debug.Log($"秒: {now.Second}");
                Debug.Log($"毫秒: {now.Millisecond}");
                Debug.Log($"星期: {now.DayOfWeek}");
                Debug.Log($"一年中的第几天: {now.DayOfYear}");
                
                // ========== 时间戳转换 ==========
                long ticks = now.Ticks;
                DateTime fromTicks = new DateTime(ticks);
                
                Debug.Log($"Ticks: {ticks}");
                Debug.Log($"从Ticks创建: {fromTicks}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"DateTime示例出错: {ex.Message}");
            }
        }

        // ================= Convert 类型转换 =================
        /// <summary>
        /// Convert和BitConverter常用API演示
        /// 包含各种类型之间的转换、字节转换等操作
        /// </summary>
        private void ConvertExample()
        {
            Debug.Log("--- Convert 类型转换示例 ---");
            
            try
            {
                // ========== Convert类转换 ==========
                // 字符串转数值类型
                int intValue = Convert.ToInt32("123");
                double doubleValue = Convert.ToDouble("3.14");
                decimal decimalValue = Convert.ToDecimal("123.456");
                bool boolValue = Convert.ToBoolean("true");
                char charValue = Convert.ToChar("A");
                
                Debug.Log($"字符串转int: {intValue}");
                Debug.Log($"字符串转double: {doubleValue}");
                Debug.Log($"字符串转decimal: {decimalValue}");
                Debug.Log($"字符串转bool: {boolValue}");
                Debug.Log($"字符串转char: {charValue}");
                
                // 数值类型转换
                int intFromDouble = Convert.ToInt32(3.7);
                double doubleFromInt = Convert.ToDouble(42);
                long longFromInt = Convert.ToInt64(123);
                
                Debug.Log($"double转int: {intFromDouble}");
                Debug.Log($"int转double: {doubleFromInt}");
                Debug.Log($"int转long: {longFromInt}");
                
                // 特殊转换
                string hexString = Convert.ToString(255, 16);
                int fromHex = Convert.ToInt32("FF", 16);
                string binaryString = Convert.ToString(42, 2);
                
                Debug.Log($"255转十六进制: {hexString}");
                Debug.Log($"FF转十进制: {fromHex}");
                Debug.Log($"42转二进制: {binaryString}");
                
                // ========== BitConverter字节转换 ==========
                // 基本类型转字节数组
                byte[] intBytes = BitConverter.GetBytes(123456);
                byte[] doubleBytes = BitConverter.GetBytes(3.14159);
                byte[] longBytes = BitConverter.GetBytes(123456789L);
                byte[] boolBytes = BitConverter.GetBytes(true);
                
                Debug.Log($"int转字节: {BitConverter.ToString(intBytes)}");
                Debug.Log($"double转字节: {BitConverter.ToString(doubleBytes)}");
                Debug.Log($"long转字节: {BitConverter.ToString(longBytes)}");
                Debug.Log($"bool转字节: {BitConverter.ToString(boolBytes)}");
                
                // 字节数组转基本类型
                int fromIntBytes = BitConverter.ToInt32(intBytes, 0);
                double fromDoubleBytes = BitConverter.ToDouble(doubleBytes, 0);
                long fromLongBytes = BitConverter.ToInt64(longBytes, 0);
                bool fromBoolBytes = BitConverter.ToBoolean(boolBytes, 0);
                
                Debug.Log($"字节转int: {fromIntBytes}");
                Debug.Log($"字节转double: {fromDoubleBytes}");
                Debug.Log($"字节转long: {fromLongBytes}");
                Debug.Log($"字节转bool: {fromBoolBytes}");
                
                // ========== 字节序处理 ==========
                bool isLittleEndian = BitConverter.IsLittleEndian;
                Debug.Log($"系统是否为小端序: {isLittleEndian}");
                
                // 如果需要大端序，可以反转字节数组
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(intBytes);
                    Debug.Log($"反转后字节: {BitConverter.ToString(intBytes)}");
                }
                
                // ========== 特殊转换 ==========
                // 字符串编码转换
                string text = "Hello, 世界!";
                byte[] utf8Bytes = Encoding.UTF8.GetBytes(text);
                byte[] asciiBytes = Encoding.ASCII.GetBytes("Hello");
                
                Debug.Log($"UTF8字节: {BitConverter.ToString(utf8Bytes)}");
                Debug.Log($"ASCII字节: {BitConverter.ToString(asciiBytes)}");
                
                // 字节数组转字符串
                string fromUtf8 = Encoding.UTF8.GetString(utf8Bytes);
                string fromAscii = Encoding.ASCII.GetString(asciiBytes);
                
                Debug.Log($"UTF8转字符串: {fromUtf8}");
                Debug.Log($"ASCII转字符串: {fromAscii}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Convert示例出错: {ex.Message}");
            }
        }

        // ================= Span 内存切片 =================
        /// <summary>
        /// Span内存切片常用API演示
        /// 包含数组切片、字符串切片、栈分配、零拷贝操作等
        /// 注意：Span<T> 是 ref struct，只能在栈上使用，不能作为泛型类型参数
        /// </summary>
        private void SpanExample()
        {
            Debug.Log("--- Span 内存切片示例 ---");
            
            try
            {
                // ========== 数组切片 ==========
                int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                Debug.Log($"原始数组: [{string.Join(", ", array)}]");
                
                // 创建 Span（零拷贝，直接引用原数组）
                Span<int> span = array.AsSpan();
                Debug.Log($"完整 Span: [{string.Join(", ", span.ToArray())}]");
                
                // 切片操作（不复制数据，只是改变视图）
                Span<int> slice1 = span.Slice(2, 5);  // 从索引2开始，长度5
                Debug.Log($"切片 [2:7]: [{string.Join(", ", slice1.ToArray())}]");
                
                // 修改 Span 会影响原数组
                slice1[0] = 100;
                Debug.Log($"修改切片后原数组: [{string.Join(", ", array)}]");
                
                // ========== 字符串切片 ==========
                string text = "Hello, World!";
                ReadOnlySpan<char> textSpan = text.AsSpan();
                Debug.Log($"原始字符串: {text}");
                Debug.Log($"Span 长度: {textSpan.Length}");
                
                // 字符串切片（ReadOnlySpan，因为字符串不可变）
                ReadOnlySpan<char> hello = textSpan.Slice(0, 5);
                ReadOnlySpan<char> world = textSpan.Slice(7, 5);
                
                Debug.Log($"'Hello' 切片: {hello.ToString()}");
                Debug.Log($"'World' 切片: {world.ToString()}");
                
                // ========== 栈分配 Span（Stackalloc） ==========
                // 在栈上分配内存，避免堆分配
                Span<int> stackAllocSpan = stackalloc int[10];
                for (int i = 0; i < stackAllocSpan.Length; i++)
                {
                    stackAllocSpan[i] = i * 2;
                }
                Debug.Log($"栈分配 Span: [{string.Join(", ", stackAllocSpan.ToArray())}]");
                
                // ========== Span 和 ReadOnlySpan 区别 ==========
                int[] readOnlyArray = { 10, 20, 30, 40, 50 };
                ReadOnlySpan<int> readOnlySpan = readOnlyArray;
                
                Debug.Log($"ReadOnlySpan 值: [{string.Join(", ", readOnlySpan.ToArray())}]");
                // readOnlySpan[0] = 100;  // 编译错误：ReadOnlySpan 是只读的
                
                // ========== 性能优势：零拷贝操作 ==========
                byte[] largeArray = new byte[1000];
                for (int i = 0; i < largeArray.Length; i++)
                {
                    largeArray[i] = (byte)(i % 256);
                }
                
                // 使用 Span 切片（不复制）
                Span<byte> largeSpan = largeArray.AsSpan();
                Span<byte> middleSlice = largeSpan.Slice(100, 200);
                Debug.Log($"大数组中间切片长度: {middleSlice.Length} (零拷贝)");
                
                // ========== Span 边界检查 ==========
                try
                {
                    Span<int> testSpan = array.AsSpan();
                    Debug.Log("Span 具有边界检查，访问越界会抛出异常");
                    Debug.Log($"Span 有效长度: {testSpan.Length}");
                    // testSpan[100] = 999;  // 会抛出 IndexOutOfRangeException
                }
                catch (IndexOutOfRangeException ex)
                {
                    Debug.LogError($"Span 边界检查异常: {ex.Message}");
                }
                
                // ========== Span 作为方法参数 ==========
                int sum = CalculateSum(span);
                Debug.Log($"Span 求和结果: {sum}");
                
                bool contains = ContainsValue(readOnlySpan, 30);
                Debug.Log($"ReadOnlySpan 是否包含30: {contains}");
                
                // ========== Span 转换 ==========
                // Span 转数组（会复制）
                int[] spanToArray = span.ToArray();
                Debug.Log($"Span 转数组: [{string.Join(", ", spanToArray)}]");
                
                // ========== 字符串解析示例 ==========
                string numbers = "1,2,3,4,5,6,7,8,9,10";
                ReadOnlySpan<char> numbersSpan = numbers.AsSpan();
                
                List<int> parsedNumbers = new List<int>();
                int start = 0;
                for (int i = 0; i < numbersSpan.Length; i++)
                {
                    if (numbersSpan[i] == ',' || i == numbersSpan.Length - 1)
                    {
                        int end = i == numbersSpan.Length - 1 ? numbersSpan.Length : i;
                        ReadOnlySpan<char> numberSpan = numbersSpan.Slice(start, end - start);
                        if (int.TryParse(numberSpan, out int number))
                        {
                            parsedNumbers.Add(number);
                        }
                        start = i + 1;
                    }
                }
                Debug.Log($"解析字符串: [{string.Join(", ", parsedNumbers)}]");
                
                // ========== Span 比较操作 ==========
                int[] array1 = { 1, 2, 3, 4, 5 };
                int[] array2 = { 1, 2, 3, 4, 5 };
                Span<int> span1 = array1.AsSpan();
                Span<int> span2 = array2.AsSpan();
                
                bool sequenceEqual = span1.SequenceEqual(span2);
                Debug.Log($"两个 Span 序列是否相等: {sequenceEqual}");
                
                // ========== Span 查找操作 ==========
                int[] searchArray = { 10, 20, 30, 40, 50, 30, 60 };
                Span<int> searchSpan = searchArray.AsSpan();
                
                int index = searchSpan.IndexOf(30);
                Debug.Log($"查找值30的索引: {index}");
                
                int lastIndex = searchSpan.LastIndexOf(30);
                Debug.Log($"查找值30的最后索引: {lastIndex}");
                
                bool containsValue = searchSpan.Contains(40);
                Debug.Log($"Span 是否包含40: {containsValue}");
                
                // ========== Span 填充和复制 ==========
                Span<int> fillSpan = array.AsSpan();
                fillSpan.Fill(999);
                Debug.Log($"填充后的数组: [{string.Join(", ", array)}]");
                
                // 恢复原值
                int[] original = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                original.CopyTo(array);
                
                // Span 复制
                int[] source = { 100, 200, 300 };
                int[] destination = new int[5];
                source.AsSpan().CopyTo(destination.AsSpan());
                Debug.Log($"复制后的目标数组: [{string.Join(", ", destination)}]");
                
                // ========== 注意事项 ==========
                Debug.LogWarning("⚠️ Span 使用注意事项：");
                Debug.LogWarning("1. Span<T> 是 ref struct，只能在栈上使用");
                Debug.LogWarning("2. 不能作为类的字段、数组元素、泛型类型参数");
                Debug.LogWarning("3. 不能在异步方法中使用");
                Debug.LogWarning("4. 不能装箱");
                Debug.LogWarning("5. Unity 2022 中 Transform/GameObject 等类新增了 Span 重载，但 ToLua# 无法绑定");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Span示例出错: {ex.Message}");
            }
        }
        
        // ================= GC 垃圾回收 =================
        /// <summary>
        /// GC垃圾回收常用API演示
        /// 包含内存监控、垃圾回收控制、弱引用等操作
        /// </summary>
        private void GCExample()
        {
            Debug.Log("--- GC 垃圾回收示例 ---");
            
            try
            {
                // ========== 内存监控 ==========
                long memBefore = GC.GetTotalMemory(false);
                Debug.Log($"初始内存: {memBefore} 字节 ({memBefore / 1024.0 / 1024.0:F2} MB)");
                
                // 分配大量内存
                var arrays = new List<byte[]>();
                for (int i = 0; i < 10; i++)
                {
                    arrays.Add(new byte[1024 * 1024]); // 每个1MB
                }
                
                long memAfter = GC.GetTotalMemory(false);
                Debug.Log($"分配后内存: {memAfter} 字节 ({memAfter / 1024.0 / 1024.0:F2} MB)");
                Debug.Log($"内存增长: {memAfter - memBefore} 字节");
                
                // 强制垃圾回收
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                
                long memCollected = GC.GetTotalMemory(true);
                Debug.Log($"GC后内存: {memCollected} 字节 ({memCollected / 1024.0 / 1024.0:F2} MB)");
                
                // ========== 代际信息 ==========
                int maxGeneration = GC.MaxGeneration;
                Debug.Log($"最大代际: {maxGeneration}");
                
                for (int i = 0; i <= maxGeneration; i++)
                {
                    long genMemory = GC.GetTotalMemory(false);
                    Debug.Log($"代际 {i} 内存: {genMemory} 字节");
                }
                
                // ========== 垃圾回收统计 ==========
                int collectionCount0 = GC.CollectionCount(0);
                int collectionCount1 = GC.CollectionCount(1);
                int collectionCount2 = GC.CollectionCount(2);
                
                Debug.Log($"代际0回收次数: {collectionCount0}");
                Debug.Log($"代际1回收次数: {collectionCount1}");
                Debug.Log($"代际2回收次数: {collectionCount2}");
                
                // ========== 弱引用示例 ==========
                var strongRef = new byte[1024 * 1024]; // 强引用
                var weakRef = new WeakReference(strongRef); // 弱引用
                
                Debug.Log($"弱引用目标是否存活: {weakRef.IsAlive}");
                Debug.Log($"弱引用目标: {weakRef.Target}");
                
                // 释放强引用
                strongRef = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                
                Debug.Log($"释放强引用后，弱引用目标是否存活: {weakRef.IsAlive}");
                Debug.Log($"弱引用目标: {weakRef.Target}");
                
                // ========== 内存压力 ==========
                GC.AddMemoryPressure(1024 * 1024); // 添加1MB内存压力
                Debug.Log("已添加内存压力");
                
                // ========== 垃圾回收模式 ==========
                GCLatencyMode currentMode = GCSettings.LatencyMode;
                Debug.Log($"当前GC延迟模式: {currentMode}");
                
                // 设置垃圾回收模式（仅在服务器模式下有效）
                if (Application.isEditor)
                {
                    GCSettings.LatencyMode = GCLatencyMode.LowLatency;
                    Debug.Log("已设置为低延迟模式");
                }
                
                // ========== 大对象堆信息 ==========
                bool isServerGC = GCSettings.IsServerGC;
                Debug.Log($"是否使用服务器GC: {isServerGC}");
                
                // 清理测试数据
                arrays.Clear();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Debug.LogError($"GC示例出错: {ex.Message}");
            }
        }

        // ================= Diagnostics 诊断 =================
        /// <summary>
        /// Diagnostics诊断常用API演示
        /// 包含性能计时、进程管理、调试工具等操作
        /// </summary>
        private void DiagnosticsExample()
        {
            Debug.Log("--- Diagnostics 诊断示例 ---");
            
            try
            {
                // ========== Stopwatch性能计时 ==========
                Stopwatch sw = new Stopwatch();
                
                // 基本计时
                sw.Start();
                for (int i = 0; i < 100000; i++)
                {
                    var x = Math.Sqrt(i);
                }
                sw.Stop();
                
                Debug.Log($"循环耗时: {sw.ElapsedMilliseconds}ms");
                Debug.Log($"循环耗时: {sw.ElapsedTicks} ticks");
                Debug.Log($"循环耗时: {sw.Elapsed.TotalSeconds:F4} 秒");
                
                // 重置和重新计时
                sw.Reset();
                sw.Start();
                Thread.Sleep(100);
                sw.Stop();
                Debug.Log($"睡眠100ms实际耗时: {sw.ElapsedMilliseconds}ms");
                
                // ========== 进程信息 ==========
                Process currentProcess = Process.GetCurrentProcess();
                Debug.Log($"当前进程ID: {currentProcess.Id}");
                Debug.Log($"当前进程名称: {currentProcess.ProcessName}");
                Debug.Log($"当前进程启动时间: {currentProcess.StartTime}");
                Debug.Log($"当前进程工作集内存: {currentProcess.WorkingSet64 / 1024 / 1024} MB");
                Debug.Log($"当前进程虚拟内存: {currentProcess.VirtualMemorySize64 / 1024 / 1024} MB");
                Debug.Log($"当前进程CPU时间: {currentProcess.TotalProcessorTime}");
                
                // ========== 系统进程列表 ==========
                Process[] processes = Process.GetProcesses();
                Debug.Log($"系统进程总数: {processes.Length}");
                
                // 查找特定进程
                Process[] unityProcesses = Process.GetProcessesByName("Unity");
                Debug.Log($"Unity相关进程数: {unityProcesses.Length}");
                
                // ========== 性能计数器 ==========
                // 注意：在Unity中某些性能计数器可能不可用
                // 确保引入必要的命名空间
                //using System.Diagnostics;

                // 使用 PerformanceCounter 之前检查是否可用
                // if (PerformanceCounterCategory.Exists("Processor"))
                // {
                //     try
                //     {
                //         using (PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
                //         {
                //             float cpuUsage = cpuCounter.NextValue();
                //             Debug.Log($"CPU使用率: {cpuUsage:F2}%");
                //         }
                //     }
                //     catch (Exception ex)
                //     {
                //         Debug.LogWarning($"性能计数器不可用: {ex.Message}");
                //     }
                // }
                // else
                // {
                //     Debug.LogWarning("性能计数器类别 'Processor' 不存在。");
                // }
                {
                  //  Debug.LogWarning($"性能计数器不可用: {ex.Message}");
                }
                
                // ========== 调试断言 ==========
                int value = 42;
                Debug.Assert(value > 0, "值必须大于0");
                Debug.Assert(value < 100, "值必须小于100");
                
                // ========== 条件编译 ==========
                #if DEBUG
                Debug.Log("这是调试版本");
                #else
                Debug.Log("这是发布版本");
                #endif
                
                // ========== 堆栈跟踪 ==========
                StackTrace stackTrace = new StackTrace();
                Debug.Log($"当前调用堆栈深度: {stackTrace.FrameCount}");
                
                // 获取调用方法信息
                StackFrame frame = stackTrace.GetFrame(0);
                if (frame != null)
                {
                    Debug.Log($"调用方法: {frame.GetMethod()?.Name}");
                    Debug.Log($"调用文件: {frame.GetFileName()}");
                    Debug.Log($"调用行号: {frame.GetFileLineNumber()}");
                }
                
                // ========== 内存诊断 ==========
                long managedMemory = GC.GetTotalMemory(false);
                Debug.Log($"托管内存: {managedMemory / 1024 / 1024} MB");
                
                // ========== 异常诊断 ==========
                try
                {
                    throw new InvalidOperationException("测试异常");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"捕获异常: {ex.Message}");
                    Debug.LogError($"异常堆栈: {ex.StackTrace}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Diagnostics示例出错: {ex.Message}");
            }
        }

        // ================= Security 加密解密 =================
        /// <summary>
        /// Security加密解密常用API演示
        /// 包含哈希算法、对称加密、非对称加密等操作
        /// </summary>
        private void SecurityExample()
        {
            Debug.Log("--- Security 加密解密示例 ---");
            
            try
            {
                string originalText = "Hello, 世界! 这是一个测试文本。";
                byte[] originalBytes = Encoding.UTF8.GetBytes(originalText);
                
                // ========== 哈希算法 ==========
                // SHA256哈希
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hash256 = sha256.ComputeHash(originalBytes);
                    string hash256Str = BitConverter.ToString(hash256).Replace("-", "");
                    Debug.Log($"SHA256哈希: {hash256Str}");
                }
                
                // SHA1哈希（不推荐用于安全场景）
                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] hash1 = sha1.ComputeHash(originalBytes);
                    string hash1Str = BitConverter.ToString(hash1).Replace("-", "");
                    Debug.Log($"SHA1哈希: {hash1Str}");
                }
                
                // MD5哈希（不推荐用于安全场景）
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hashMD5 = md5.ComputeHash(originalBytes);
                    string hashMD5Str = BitConverter.ToString(hashMD5).Replace("-", "");
                    Debug.Log($"MD5哈希: {hashMD5Str}");
                }
                
                // ========== 对称加密 ==========
                // AES加密
                using (Aes aes = Aes.Create())
                {
                    aes.GenerateKey();
                    aes.GenerateIV();
                    
                    // 加密
                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    {
                        byte[] encrypted = encryptor.TransformFinalBlock(originalBytes, 0, originalBytes.Length);
                        string encryptedStr = Convert.ToBase64String(encrypted);
                        Debug.Log($"AES加密结果: {encryptedStr}");
                        
                        // 解密
                        using (ICryptoTransform decryptor = aes.CreateDecryptor())
                        {
                            byte[] decrypted = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
                            string decryptedText = Encoding.UTF8.GetString(decrypted);
                            Debug.Log($"AES解密结果: {decryptedText}");
                        }
                    }
                }
                
                // ========== 随机数生成 ==========
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    byte[] randomBytes = new byte[32];
                    rng.GetBytes(randomBytes);
                    string randomStr = BitConverter.ToString(randomBytes).Replace("-", "");
                    Debug.Log($"加密随机数: {randomStr}");
                }
                
                // ========== 密码哈希 ==========
                string password = "MyPassword123";
                byte[] salt = new byte[16];
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(salt);
                }
                
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
                {
                    byte[] hash = pbkdf2.GetBytes(32);
                    string passwordHash = Convert.ToBase64String(hash);
                    string saltStr = Convert.ToBase64String(salt);
                    Debug.Log($"密码哈希: {passwordHash}");
                    Debug.Log($"盐值: {saltStr}");
                }
                
                // ========== 数字签名 ==========
                // 注意：在Unity中某些加密算法可能不可用
                try
                {
                    using (RSA rsa = RSA.Create())
                    {
                        byte[] signature = rsa.SignData(originalBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                        string signatureStr = Convert.ToBase64String(signature);
                        Debug.Log($"RSA签名: {signatureStr}");
                        
                        bool isValid = rsa.VerifyData(originalBytes, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                        Debug.Log($"签名验证: {isValid}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"RSA签名不可用: {ex.Message}");
                }
                
                // ========== 安全字符串 ==========
                // 注意：System.Security.SecureString在某些平台可能不可用
                try
                {
                    var secureString = new System.Security.SecureString();
                    foreach (char c in "SecretPassword")
                    {
                        secureString.AppendChar(c);
                    }
                    Debug.Log($"安全字符串长度: {secureString.Length}");
                    secureString.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"SecureString不可用: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Security示例出错: {ex.Message}");
            }
        }

        // ================= Globalization 全球化 =================
        /// <summary>
        /// Globalization全球化常用API演示
        /// 包含文化信息、日期格式、数字格式、货币格式等操作
        /// </summary>
        private void GlobalizationExample()
        {
            Debug.Log("--- Globalization 全球化示例 ---");
            
            try
            {
                // ========== 文化信息 ==========
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
                CultureInfo invariantCulture = CultureInfo.InvariantCulture;
                
                Debug.Log($"当前文化: {currentCulture.Name} ({currentCulture.DisplayName})");
                Debug.Log($"当前UI文化: {currentUICulture.Name} ({currentUICulture.DisplayName})");
                Debug.Log($"不变文化: {invariantCulture.Name}");
                
                // ========== 不同文化的日期格式 ==========
                DateTime now = DateTime.Now;
                
                CultureInfo[] cultures = {
                    new CultureInfo("zh-CN"), // 中文简体
                    new CultureInfo("en-US"), // 英文美国
                    new CultureInfo("fr-FR"), // 法文法国
                    new CultureInfo("de-DE"), // 德文德国
                    new CultureInfo("ja-JP"), // 日文日本
                    new CultureInfo("ko-KR")  // 韩文韩国
                };
                
                foreach (var culture in cultures)
                {
                    string dateStr = now.ToString(culture);
                    string shortDate = now.ToString("d", culture);
                    string longDate = now.ToString("D", culture);
                    
                    Debug.Log($"{culture.DisplayName}: {dateStr}");
                    Debug.Log($"{culture.DisplayName} 短日期: {shortDate}");
                    Debug.Log($"{culture.DisplayName} 长日期: {longDate}");
                }
                
                // ========== 数字格式 ==========
                double number = 12345.67;
                
                foreach (var culture in cultures)
                {
                    string numberStr = number.ToString("N", culture);
                    string currencyStr = number.ToString("C", culture);
                    string percentStr = (number / 100000).ToString("P", culture);
                    
                    Debug.Log($"{culture.DisplayName} 数字: {numberStr}");
                    Debug.Log($"{culture.DisplayName} 货币: {currencyStr}");
                    Debug.Log($"{culture.DisplayName} 百分比: {percentStr}");
                }
                
                // ========== 日期时间格式信息 ==========
                CultureInfo zhCN = new CultureInfo("zh-CN");
                DateTimeFormatInfo dateFormat = zhCN.DateTimeFormat;
                
                Debug.Log($"中文月份名称: {string.Join(", ", dateFormat.MonthNames)}");
                Debug.Log($"中文星期名称: {string.Join(", ", dateFormat.DayNames)}");
                Debug.Log($"中文短星期名称: {string.Join(", ", dateFormat.AbbreviatedDayNames)}");
                Debug.Log($"中文AM/PM: {dateFormat.AMDesignator}, {dateFormat.PMDesignator}");
                
                // ========== 数字格式信息 ==========
                NumberFormatInfo numberFormat = zhCN.NumberFormat;
                
                Debug.Log($"数字分隔符: {numberFormat.NumberDecimalSeparator}");
                Debug.Log($"千位分隔符: {numberFormat.NumberGroupSeparator}");
                Debug.Log($"货币符号: {numberFormat.CurrencySymbol}");
                Debug.Log($"货币分隔符: {numberFormat.CurrencyDecimalSeparator}");
                
                // ========== 自定义格式 ==========
                CultureInfo customCulture = new CultureInfo("en-US");
                customCulture.NumberFormat.NumberDecimalDigits = 3;
                customCulture.NumberFormat.NumberGroupSeparator = "_";
                
                string customNumber = number.ToString("N", customCulture);
                Debug.Log($"自定义格式数字: {customNumber}");
                
                // ========== 文化比较 ==========
                string text1 = "café";
                string text2 = "CAFE";
                
                int comparison1 = string.Compare(text1, text2, StringComparison.Ordinal);
                int comparison2 = string.Compare(text1, text2, StringComparison.OrdinalIgnoreCase);
                int comparison3 = string.Compare(text1, text2, StringComparison.CurrentCulture);
                
                Debug.Log($"Ordinal比较: {comparison1}");
                Debug.Log($"OrdinalIgnoreCase比较: {comparison2}");
                Debug.Log($"CurrentCulture比较: {comparison3}");
                
                // ========== 时区信息 ==========
                TimeZoneInfo localZone = TimeZoneInfo.Local;
                Debug.Log($"本地时区: {localZone.DisplayName}");
                Debug.Log($"时区ID: {localZone.Id}");
                Debug.Log($"标准时间名称: {localZone.StandardName}");
                Debug.Log($"夏令时名称: {localZone.DaylightName}");
                Debug.Log($"当前偏移: {localZone.GetUtcOffset(DateTime.Now)}");
                
                // ========== 可用文化列表 ==========
                CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                Debug.Log($"系统支持的文化总数: {allCultures.Length}");
                
                // 显示前10个文化
                for (int i = 0; i < Math.Min(10, allCultures.Length); i++)
                {
                    Debug.Log($"文化{i + 1}: {allCultures[i].Name} - {allCultures[i].DisplayName}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Globalization示例出错: {ex.Message}");
            }
        }

        // ================= ComponentModel 组件模型 =================
        /// <summary>
        /// ComponentModel组件模型常用API演示
        /// 包含属性描述、类型转换、数据验证、属性变化通知等操作
        /// </summary>
        private void ComponentModelExample()
        {
            Debug.Log("--- ComponentModel 组件模型示例 ---");
            
            try
            {
                // ========== 属性描述器 ==========
                var person = new Person { Name = "张三", Age = 28, Email = "zhangsan@example.com" };
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(person);
                
                Debug.Log($"Person类的属性数量: {props.Count}");
                foreach (PropertyDescriptor prop in props)
                {
                    Debug.Log($"属性: {prop.Name}, 类型: {prop.PropertyType.Name}, 值: {prop.GetValue(person)}");
                }
                
                // ========== 类型转换器 ==========
                TypeConverter stringConverter = TypeDescriptor.GetConverter(typeof(string));
                TypeConverter intConverter = TypeDescriptor.GetConverter(typeof(int));
                TypeConverter boolConverter = TypeDescriptor.GetConverter(typeof(bool));
                
                Debug.Log($"字符串转换器: {stringConverter.GetType().Name}");
                Debug.Log($"整数转换器: {intConverter.GetType().Name}");
                Debug.Log($"布尔转换器: {boolConverter.GetType().Name}");
                
                // 测试转换
                if (intConverter.CanConvertFrom(typeof(string)))
                {
                    int convertedValue = (int)intConverter.ConvertFrom("123");
                    Debug.Log($"字符串'123'转换为int: {convertedValue}");
                }
                
                // ========== 数据验证 ==========
                var user = new User { Name = "李四", Age = 25, Email = "invalid-email" };
                var validationResults = new List<ValidationResult>();
                
                // // 确保引入必要的命名空间
                // var validationContext = new ValidationContext(user);
                
                // // 使用早期返回提高可读性
                // if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
                // {
                //     Debug.Log($"用户数据验证结果: false");
                //     foreach (var result in validationResults)
                //     {
                //         Debug.LogWarning($"验证错误: {result.ErrorMessage}");
                //     }
                //     return; // 早期返回
                // }
                
                // Debug.Log($"用户数据验证结果: true");
                // if (!isValid)
                // {
                //     foreach (var result in validationResults)
                //     {
                //         Debug.LogWarning($"验证错误: {result.ErrorMessage}");
                //     }
                // }
                
                // ========== 属性变化通知 ==========
                var observablePerson = new ObservablePerson { Name = "王五", Age = 30 };
                observablePerson.PropertyChanged += OnPropertyChanged;
                
                observablePerson.Name = "王六";
                observablePerson.Age = 31;
                
                // ========== 自定义类型转换器 ==========
                var colorConverter = new ColorConverter();
                if (colorConverter.CanConvertFrom(typeof(string)))
                {
                    var color = (Color)colorConverter.ConvertFrom("Red");
                    Debug.Log($"字符串'Red'转换为Color: {color}");
                }
                
                // ========== 事件描述器 ==========
                EventDescriptorCollection events = TypeDescriptor.GetEvents(typeof(ObservablePerson));
                Debug.Log($"ObservablePerson类的事件数量: {events.Count}");
                foreach (EventDescriptor evt in events)
                {
                    Debug.Log($"事件: {evt.Name}, 类型: {evt.EventType.Name}");
                }
                
                // ========== 属性网格支持 ==========
                var complexPerson = new ComplexPerson
                {
                    Name = "赵六",
                    Age = 35,
                    Address = new Address { City = "北京", Street = "长安街" }
                };
                
                PropertyDescriptorCollection complexProps = TypeDescriptor.GetProperties(complexPerson);
                Debug.Log($"ComplexPerson类的属性数量: {complexProps.Count}");
                foreach (PropertyDescriptor prop in complexProps)
                {
                    Debug.Log($"属性: {prop.Name}, 类型: {prop.PropertyType.Name}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"ComponentModel示例出错: {ex.Message}");
            }
        }
        
        // ================= AppDomain 应用程序域 =================
        /// <summary>
        /// AppDomain应用程序域常用API演示
        /// 包含程序集查询、类型查找、反射操作等
        /// AppDomain 是 .NET 中应用程序的隔离边界，用于加载和执行程序集
        /// </summary>
        private void AppDomainExample()
        {
            Debug.Log("--- AppDomain 应用程序域示例 ---");
            
            try
            {
                // ========== 获取当前应用程序域 ==========
                AppDomain currentDomain = AppDomain.CurrentDomain;
                Debug.Log($"当前应用程序域名称: {currentDomain.FriendlyName}");
                Debug.Log($"应用程序域ID: {currentDomain.Id}");
                Debug.Log($"基础目录: {currentDomain.BaseDirectory}");
                Debug.Log($"相对搜索路径: {currentDomain.RelativeSearchPath}");
                
                // ========== 获取所有已加载的程序集 ==========
                // GetAssemblies() 返回当前应用程序域中所有已加载的程序集
                // 这是反射操作中最常用的方法之一，用于查找类型、方法等
                Assembly[] assemblies = currentDomain.GetAssemblies();
                Debug.Log($"已加载的程序集总数: {assemblies.Length}");
                
                // 显示前10个程序集信息
                Debug.Log("--- 前10个程序集信息 ---");
                for (int i = 0; i < Math.Min(10, assemblies.Length); i++)
                {
                    Assembly assembly = assemblies[i];
                    Debug.Log($"程序集 {i + 1}: {assembly.GetName().Name}");
                    Debug.Log($"  完整名称: {assembly.FullName}");
                    Debug.Log($"  位置: {assembly.Location}");
                    Debug.Log($"  是否动态: {assembly.IsDynamic}");
                }
                
                // ========== 按名称查找程序集 ==========
                // 查找特定程序集
                Assembly[] unityAssemblies = assemblies.Where(a => 
                    a.GetName().Name.Contains("UnityEngine") || 
                    a.GetName().Name.Contains("Unity")).ToArray();
                Debug.Log($"Unity相关程序集数量: {unityAssemblies.Length}");
                
                foreach (var unityAssembly in unityAssemblies.Take(5))
                {
                    Debug.Log($"  - {unityAssembly.GetName().Name}");
                }
                
                // ========== 从程序集中查找类型 ==========
                Debug.Log("--- 类型查找示例 ---");
                
                // 方法1: 遍历所有程序集查找类型
                string targetTypeName = "System.String";
                Type foundType = null;
                
                foreach (Assembly assembly in assemblies)
                {
                    try
                    {
                        foundType = assembly.GetType(targetTypeName);
                        if (foundType != null)
                        {
                            Debug.Log($"找到类型 {targetTypeName} 在程序集: {assembly.GetName().Name}");
                            break;
                        }
                    }
                    catch
                    {
                        // 忽略无法加载的程序集
                    }
                }
                
                // 方法2: 使用 Type.GetType (更常用)
                Type stringType = Type.GetType(targetTypeName);
                if (stringType != null)
                {
                    Debug.Log($"Type.GetType 找到类型: {stringType.Name}");
                }
                
                // ========== 查找命名空间下的所有类型 ==========
                Debug.Log("--- 查找命名空间下的类型 ---");
                string targetNamespace = "UnityEngine";
                int typeCount = 0;
                
                foreach (Assembly assembly in assemblies)
                {
                    try
                    {
                        Type[] types = assembly.GetTypes();
                        var namespaceTypes = types.Where(t => 
                            t.Namespace == targetNamespace && 
                            t.IsPublic && 
                            !t.IsAbstract).Take(10);
                        
                        foreach (var type in namespaceTypes)
                        {
                            Debug.Log($"  {targetNamespace}.{type.Name}");
                            typeCount++;
                        }
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        // 某些程序集可能无法完全加载，只加载可用的类型
                        if (ex.Types != null)
                        {
                            foreach (var type in ex.Types.Where(t => t != null && t.Namespace == targetNamespace).Take(10))
                            {
                                Debug.Log($"  {targetNamespace}.{type.Name}");
                                typeCount++;
                            }
                        }
                    }
                    catch
                    {
                        // 忽略其他错误
                    }
                }
                Debug.Log($"找到 {targetNamespace} 命名空间下的类型数量: {typeCount}");
                
                // ========== 查找实现特定接口或继承特定基类的类型 ==========
                Debug.Log("--- 查找继承特定基类的类型 ---");
                Type componentBaseType = typeof(Component);
                int componentTypeCount = 0;
                
                foreach (Assembly assembly in assemblies)
                {
                    try
                    {
                        Type[] types = assembly.GetTypes();
                        var componentTypes = types.Where(t => 
                            componentBaseType.IsAssignableFrom(t) && 
                            !t.IsAbstract && 
                            t.IsClass).Take(5);
                        
                        foreach (var type in componentTypes)
                        {
                            Debug.Log($"  继承Component的类型: {type.Name}");
                            componentTypeCount++;
                        }
                    }
                    catch
                    {
                        // 忽略错误
                    }
                }
                Debug.Log($"找到继承Component的类型总数: {componentTypeCount}");
                
                // ========== 程序集信息查询 ==========
                Debug.Log("--- 程序集详细信息 ---");
                Assembly mscorlib = assemblies.FirstOrDefault(a => a.GetName().Name == "mscorlib");
                if (mscorlib != null)
                {
                    AssemblyName assemblyName = mscorlib.GetName();
                    Debug.Log($"程序集名称: {assemblyName.Name}");
                    Debug.Log($"版本: {assemblyName.Version}");
                    Debug.Log($"公钥标记: {BitConverter.ToString(assemblyName.GetPublicKeyToken())}");
                    Debug.Log($"文化信息: {assemblyName.CultureInfo?.Name ?? "中性"}");
                }
                
                // ========== 查找特定方法 ==========
                Debug.Log("--- 查找特定方法 ---");
                Type mathType = typeof(Math);
                MethodInfo[] mathMethods = mathType.GetMethods(BindingFlags.Public | BindingFlags.Static);
                Debug.Log($"Math类的公共静态方法数量: {mathMethods.Length}");
                
                // 查找包含特定名称的方法
                var sqrtMethods = mathMethods.Where(m => m.Name.Contains("Sqrt")).Take(3);
                foreach (var method in sqrtMethods)
                {
                    Debug.Log($"  Math.{method.Name}({string.Join(", ", method.GetParameters().Select(p => p.ParameterType.Name))})");
                }
                
                // ========== 程序集加载事件 ==========
                Debug.Log("--- 程序集加载事件 ---");
                // 注意：在 Unity 中，程序集加载事件可能不会按预期触发
                // 因为 Unity 使用自己的程序集加载机制
                currentDomain.AssemblyLoad += OnAssemblyLoad;
                Debug.Log("已注册程序集加载事件处理器");
                
                // ========== 动态加载程序集（示例） ==========
                Debug.Log("--- 动态加载程序集 ---");
                // 注意：在 Unity 中，动态加载程序集需要使用 Assembly.LoadFrom 或 Assembly.LoadFile
                // 但通常 Unity 会自动加载所有程序集，所以很少需要手动加载
                
                // 查找当前程序集
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Debug.Log($"当前执行程序集: {currentAssembly.GetName().Name}");
                Debug.Log($"当前程序集位置: {currentAssembly.Location}");
                
                // ========== 程序集依赖关系 ==========
                Debug.Log("--- 程序集引用关系 ---");
                AssemblyName[] referencedAssemblies = currentAssembly.GetReferencedAssemblies();
                Debug.Log($"当前程序集引用的程序集数量: {referencedAssemblies.Length}");
                
                foreach (var refAssembly in referencedAssemblies.Take(5))
                {
                    Debug.Log($"  引用: {refAssembly.Name} v{refAssembly.Version}");
                }
                
                // ========== 查找自定义特性 ==========
                Debug.Log("--- 查找程序集特性 ---");
                object[] attributes = currentAssembly.GetCustomAttributes(false);
                Debug.Log($"当前程序集的自定义特性数量: {attributes.Length}");
                
                foreach (var attr in attributes.Take(5))
                {
                    Debug.Log($"  特性: {attr.GetType().Name}");
                }
                
                // ========== 性能考虑 ==========
                Debug.LogWarning("⚠️ AppDomain.GetAssemblies() 使用注意事项：");
                Debug.LogWarning("1. GetAssemblies() 返回所有已加载的程序集，数量可能很大");
                Debug.LogWarning("2. 遍历所有程序集和类型可能很耗时，建议缓存结果");
                Debug.LogWarning("3. 某些程序集可能无法完全加载，需要处理 ReflectionTypeLoadException");
                Debug.LogWarning("4. 在 Unity 中，程序集加载时机可能与 .NET 标准不同");
                Debug.LogWarning("5. 使用 Type.GetType() 通常比遍历程序集更高效");
                
                // ========== 实用示例：查找所有 MonoBehaviour 子类 ==========
                Debug.Log("--- 实用示例：查找 MonoBehaviour 子类 ---");
                Type monoBehaviourType = typeof(MonoBehaviour);
                List<string> monoBehaviourTypes = new List<string>();
                
                foreach (Assembly assembly in assemblies)
                {
                    try
                    {
                        Type[] types = assembly.GetTypes();
                        var mbTypes = types.Where(t => 
                            monoBehaviourType.IsAssignableFrom(t) && 
                            t.IsClass && 
                            !t.IsAbstract &&
                            t != monoBehaviourType);
                        
                        foreach (var type in mbTypes.Take(5))
                        {
                            monoBehaviourTypes.Add($"{type.Namespace}.{type.Name}");
                        }
                    }
                    catch
                    {
                        // 忽略错误
                    }
                }
                
                Debug.Log($"找到 MonoBehaviour 子类数量: {monoBehaviourTypes.Count}");
                foreach (var typeName in monoBehaviourTypes.Take(10))
                {
                    Debug.Log($"  - {typeName}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"AppDomain示例出错: {ex.Message}");
                Debug.LogError($"堆栈跟踪: {ex.StackTrace}");
            }
        }
        
        /// <summary>
        /// 程序集加载事件处理器
        /// </summary>
        private void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Debug.Log($"程序集已加载: {args.LoadedAssembly.GetName().Name}");
        }
        
        // 属性变化事件处理
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.Log($"属性 {e.PropertyName} 发生变化");
        }
        
        // ================= Span 辅助方法 =================
        
        /// <summary>
        /// Span 作为方法参数的示例：计算 Span 中所有元素的和
        /// </summary>
        private int CalculateSum(Span<int> span)
        {
            int sum = 0;
            foreach (int value in span)
            {
                sum += value;
            }
            return sum;
        }
        
        /// <summary>
        /// ReadOnlySpan 作为方法参数的示例：检查是否包含指定值
        /// </summary>
        private bool ContainsValue(ReadOnlySpan<int> span, int value)
        {
            foreach (int item in span)
            {
                if (item == value)
                    return true;
            }
            return false;
        }
        
        // 自定义颜色转换器
        private class ColorConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string);
            }
            
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string colorName)
                {
                    switch (colorName.ToLower())
                    {
                        case "red": return Color.red;
                        case "green": return Color.green;
                        case "blue": return Color.blue;
                        default: return Color.white;
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
        
        // 数据验证示例类
        private class User
        {
            [Required(ErrorMessage = "姓名不能为空")]
            //[StringLength(50, MinimumLength = 2, ErrorMessage = "姓名长度必须在2-50个字符之间")]
            public string Name { get; set; }
            
            //[Range(0, 150, ErrorMessage = "年龄必须在0-150之间")]
            public int Age { get; set; }
            
            //[EmailAddress(ErrorMessage = "邮箱格式不正确")]
            public string Email { get; set; }
        }
        
        // 属性变化通知示例类
        private class ObservablePerson : INotifyPropertyChanged
        {
            private string name;
            private int age;
            
            public string Name
            {
                get => name;
                set
                {
                    if (name != value)
                    {
                        name = value;
                        OnPropertyChanged(nameof(Name));
                    }
                }
            }
            
            public int Age
            {
                get => age;
                set
                {
                    if (age != value)
                    {
                        age = value;
                        OnPropertyChanged(nameof(Age));
                    }
                }
            }
            
            public event PropertyChangedEventHandler PropertyChanged;
            
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        // 复杂对象示例类
        private class ComplexPerson
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Address Address { get; set; }
        }
        
        private class Address
        {
            public string City { get; set; }
            public string Street { get; set; }
        }

        // 内部测试类
        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
        }
    }
} 