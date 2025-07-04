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
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;

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
            GCExample();
            DiagnosticsExample();
            SecurityExample();
            GlobalizationExample();
            ComponentModelExample();
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
                Debug.Log($"Tau: {Math.Tau}");
                
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
                try
                {
                    PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    float cpuUsage = cpuCounter.NextValue();
                    Debug.Log($"CPU使用率: {cpuUsage:F2}%");
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"性能计数器不可用: {ex.Message}");
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
                var validationContext = new ValidationContext(user);
                
                bool isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);
                Debug.Log($"用户数据验证结果: {isValid}");
                
                if (!isValid)
                {
                    foreach (var result in validationResults)
                    {
                        Debug.LogWarning($"验证错误: {result.ErrorMessage}");
                    }
                }
                
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
        
        // 属性变化事件处理
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.Log($"属性 {e.PropertyName} 发生变化");
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
            [StringLength(50, MinimumLength = 2, ErrorMessage = "姓名长度必须在2-50个字符之间")]
            public string Name { get; set; }
            
            [Range(0, 150, ErrorMessage = "年龄必须在0-150之间")]
            public int Age { get; set; }
            
            [EmailAddress(ErrorMessage = "邮箱格式不正确")]
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