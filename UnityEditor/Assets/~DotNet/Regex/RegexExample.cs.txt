using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace DotNet.Regex
{
    /// <summary>
    /// C# 正则表达式综合示例
    /// 展示System.Text.RegularExpressions命名空间中所有主要功能
    /// </summary>
    public class RegexExample
    {
        #region 基础正则表达式模式

        /// <summary>
        /// 基础字符类匹配示例
        /// </summary>
        public static void BasicCharacterClassExamples()
        {
            Console.WriteLine("=== 基础字符类匹配示例 ===");
            
            string text = "Hello123World456!@#";
            
            // 数字匹配
            Console.WriteLine("数字匹配:");
            MatchCollection digits = Regex.Matches(text, @"\d+");
            foreach (Match match in digits)
            {
                Console.WriteLine($"找到数字: {match.Value} (位置: {match.Index})");
            }
            
            // 字母匹配
            Console.WriteLine("\n字母匹配:");
            MatchCollection letters = Regex.Matches(text, @"[a-zA-Z]+");
            foreach (Match match in letters)
            {
                Console.WriteLine($"找到字母: {match.Value} (位置: {match.Index})");
            }
            
            // 非字母数字匹配
            Console.WriteLine("\n特殊字符匹配:");
            MatchCollection special = Regex.Matches(text, @"[^a-zA-Z0-9]+");
            foreach (Match match in special)
            {
                Console.WriteLine($"找到特殊字符: '{match.Value}' (位置: {match.Index})");
            }
        }

        /// <summary>
        /// 量词使用示例
        /// </summary>
        public static void QuantifierExamples()
        {
            Console.WriteLine("\n=== 量词使用示例 ===");
            
            string text = "a aa aaa aaaa aaaaa";
            
            // 匹配1个或多个a
            Console.WriteLine("匹配1个或多个a:");
            MatchCollection oneOrMore = Regex.Matches(text, @"a+");
            foreach (Match match in oneOrMore)
            {
                Console.WriteLine($"'{match.Value}' (长度: {match.Length})");
            }
            
            // 匹配恰好3个a
            Console.WriteLine("\n匹配恰好3个a:");
            MatchCollection exactlyThree = Regex.Matches(text, @"a{3}");
            foreach (Match match in exactlyThree)
            {
                Console.WriteLine($"'{match.Value}'");
            }
            
            // 匹配2到4个a
            Console.WriteLine("\n匹配2到4个a:");
            MatchCollection twoToFour = Regex.Matches(text, @"a{2,4}");
            foreach (Match match in twoToFour)
            {
                Console.WriteLine($"'{match.Value}' (长度: {match.Length})");
            }
        }

        /// <summary>
        /// 位置锚点示例
        /// </summary>
        public static void AnchorExamples()
        {
            Console.WriteLine("\n=== 位置锚点示例 ===");
            
            string[] texts = { "Hello World", "World Hello", "Hello", "World" };
            
            // 匹配以Hello开头的字符串
            Console.WriteLine("匹配以Hello开头的字符串:");
            foreach (string text in texts)
            {
                if (Regex.IsMatch(text, @"^Hello"))
                {
                    Console.WriteLine($"匹配: '{text}'");
                }
            }
            
            // 匹配以World结尾的字符串
            Console.WriteLine("\n匹配以World结尾的字符串:");
            foreach (string text in texts)
            {
                if (Regex.IsMatch(text, @"World$"))
                {
                    Console.WriteLine($"匹配: '{text}'");
                }
            }
            
            // 匹配完整单词
            Console.WriteLine("\n匹配完整单词'Hello':");
            foreach (string text in texts)
            {
                if (Regex.IsMatch(text, @"\bHello\b"))
                {
                    Console.WriteLine($"匹配: '{text}'");
                }
            }
        }

        #endregion

        #region 实用正则表达式模式

        /// <summary>
        /// 邮箱验证示例
        /// </summary>
        public static void EmailValidationExamples()
        {
            Console.WriteLine("\n=== 邮箱验证示例 ===");
            
            string[] emails = {
                "user@example.com",
                "test.email@domain.co.uk",
                "invalid.email",
                "@domain.com",
                "user@",
                "user.name+tag@example.org"
            };
            
            // 简单邮箱验证
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            
            foreach (string email in emails)
            {
                bool isValid = Regex.IsMatch(email, emailPattern);
                Console.WriteLine($"邮箱: {email} - {(isValid ? "有效" : "无效")}");
            }
        }

        /// <summary>
        /// 手机号码验证示例
        /// </summary>
        public static void PhoneValidationExamples()
        {
            Console.WriteLine("\n=== 手机号码验证示例 ===");
            
            string[] phones = {
                "13812345678",
                "15987654321",
                "12345678901", // 无效
                "1381234567",  // 无效
                "+8613812345678"
            };
            
            // 中国手机号验证
            string phonePattern = @"^(\+86)?1[3-9]\d{9}$";
            
            foreach (string phone in phones)
            {
                bool isValid = Regex.IsMatch(phone, phonePattern);
                Console.WriteLine($"手机号: {phone} - {(isValid ? "有效" : "无效")}");
            }
        }

        /// <summary>
        /// URL验证示例
        /// </summary>
        public static void UrlValidationExamples()
        {
            Console.WriteLine("\n=== URL验证示例 ===");
            
            string[] urls = {
                "https://www.example.com",
                "http://example.com/path",
                "ftp://files.example.com",
                "invalid-url",
                "https://",
                "www.example.com"
            };
            
            // URL验证模式
            string urlPattern = @"^https?://[^\s/$.?#].[^\s]*$";
            
            foreach (string url in urls)
            {
                bool isValid = Regex.IsMatch(url, urlPattern);
                Console.WriteLine($"URL: {url} - {(isValid ? "有效" : "无效")}");
            }
        }

        /// <summary>
        /// IP地址验证示例
        /// </summary>
        public static void IpAddressValidationExamples()
        {
            Console.WriteLine("\n=== IP地址验证示例 ===");
            
            string[] ips = {
                "192.168.1.1",
                "10.0.0.1",
                "255.255.255.255",
                "256.1.1.1",    // 无效
                "192.168.1",    // 无效
                "192.168.1.1.1" // 无效
            };
            
            // IPv4验证模式
            string ipPattern = @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";
            
            foreach (string ip in ips)
            {
                bool isValid = Regex.IsMatch(ip, ipPattern);
                Console.WriteLine($"IP地址: {ip} - {(isValid ? "有效" : "无效")}");
            }
        }

        /// <summary>
        /// 日期格式验证示例
        /// </summary>
        public static void DateValidationExamples()
        {
            Console.WriteLine("\n=== 日期格式验证示例 ===");
            
            string[] dates = {
                "2023-12-25",
                "12/25/2023",
                "25/12/2023",
                "2023-13-01",   // 无效月份
                "2023-02-30",   // 无效日期
                "23-12-25"      // 无效格式
            };
            
            // YYYY-MM-DD格式验证
            string datePattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";
            
            foreach (string date in dates)
            {
                bool isValid = Regex.IsMatch(date, datePattern);
                Console.WriteLine($"日期: {date} - {(isValid ? "有效" : "无效")}");
            }
        }

        /// <summary>
        /// 密码强度验证示例
        /// </summary>
        public static void PasswordStrengthExamples()
        {
            Console.WriteLine("\n=== 密码强度验证示例 ===");
            
            string[] passwords = {
                "Password123",
                "weak",
                "StrongPass1!",
                "NoNumbers",
                "12345678",
                "Abc123!@#"
            };
            
            // 密码强度验证：至少8位，包含大小写字母、数字和特殊字符
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            
            foreach (string password in passwords)
            {
                bool isValid = Regex.IsMatch(password, passwordPattern);
                Console.WriteLine($"密码: {password} - {(isValid ? "强度足够" : "强度不足")}");
            }
        }

        #endregion

        #region 高级功能

        /// <summary>
        /// 捕获组示例
        /// </summary>
        public static void CaptureGroupExamples()
        {
            Console.WriteLine("\n=== 捕获组示例 ===");
            
            string text = "John Doe, Jane Smith, Bob Johnson";
            
            // 命名捕获组
            string pattern = @"(?<FirstName>\w+)\s+(?<LastName>\w+)";
            MatchCollection matches = Regex.Matches(text, pattern);
            
            foreach (Match match in matches)
            {
                Console.WriteLine($"全名: {match.Value}");
                Console.WriteLine($"  名: {match.Groups["FirstName"].Value}");
                Console.WriteLine($"  姓: {match.Groups["LastName"].Value}");
                Console.WriteLine($"  位置: {match.Index}-{match.Index + match.Length}");
            }
        }

        /// <summary>
        /// 替换示例
        /// </summary>
        public static void ReplacementExamples()
        {
            Console.WriteLine("\n=== 替换示例 ===");
            
            string text = "Hello World! Welcome to the World of Programming.";
            
            // 基本替换
            string result1 = Regex.Replace(text, @"World", "Universe");
            Console.WriteLine($"基本替换: {result1}");
            
            // 使用捕获组替换
            string result2 = Regex.Replace(text, @"(\w+)\s+(\w+)", "$2, $1");
            Console.WriteLine($"交换单词: {result2}");
            
            // 使用命名组替换
            string result3 = Regex.Replace(text, @"(?<first>\w+)\s+(?<second>\w+)", "${second}, ${first}");
            Console.WriteLine($"命名组替换: {result3}");
            
            // 条件替换
            string result4 = Regex.Replace(text, @"\b\w{5}\b", match => 
                match.Value.ToUpper());
            Console.WriteLine($"5字母单词转大写: {result4}");
        }

        /// <summary>
        /// 分割示例
        /// </summary>
        public static void SplitExamples()
        {
            Console.WriteLine("\n=== 分割示例 ===");
            
            string text = "apple,banana;cherry:grape|orange";
            
            // 按多种分隔符分割
            string[] fruits = Regex.Split(text, @"[,;:|]");
            Console.WriteLine("分割结果:");
            foreach (string fruit in fruits)
            {
                Console.WriteLine($"  {fruit}");
            }
            
            // 保留分隔符的分割
            string[] partsWithDelimiters = Regex.Split(text, @"([,;:|])");
            Console.WriteLine("\n包含分隔符的分割:");
            foreach (string part in partsWithDelimiters)
            {
                Console.WriteLine($"  '{part}'");
            }
        }

        /// <summary>
        /// 非捕获组和断言示例
        /// </summary>
        public static void NonCapturingGroupExamples()
        {
            Console.WriteLine("\n=== 非捕获组和断言示例 ===");
            
            string text = "The quick brown fox jumps over the lazy dog.";
            
            // 非捕获组
            MatchCollection nonCapturing = Regex.Matches(text, @"(?:quick|slow)\s+\w+");
            Console.WriteLine("非捕获组匹配:");
            foreach (Match match in nonCapturing)
            {
                Console.WriteLine($"  {match.Value}");
            }
            
            // 正向先行断言
            MatchCollection positiveLookahead = Regex.Matches(text, @"\w+(?=\s+fox)");
            Console.WriteLine("\n正向先行断言 (fox前的单词):");
            foreach (Match match in positiveLookahead)
            {
                Console.WriteLine($"  {match.Value}");
            }
            
            // 负向先行断言
            MatchCollection negativeLookahead = Regex.Matches(text, @"\w+(?!\s+fox)");
            Console.WriteLine("\n负向先行断言 (非fox前的单词):");
            foreach (Match match in negativeLookahead)
            {
                Console.WriteLine($"  {match.Value}");
            }
        }

        #endregion

        #region 性能优化

        /// <summary>
        /// 编译选项示例
        /// </summary>
        public static void CompilationOptionsExamples()
        {
            Console.WriteLine("\n=== 编译选项示例 ===");
            
            string text = "Hello World! This is a test.";
            
            // 使用编译选项
            Regex compiledRegex = new Regex(@"\b\w{5}\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            MatchCollection matches = compiledRegex.Matches(text);
            Console.WriteLine("编译模式匹配结果:");
            foreach (Match match in matches)
            {
                Console.WriteLine($"  {match.Value}");
            }
            
            // 性能测试
            Console.WriteLine("\n性能测试:");
            TestRegexPerformance();
        }

        /// <summary>
        /// 正则表达式性能测试
        /// </summary>
        private static void TestRegexPerformance()
        {
            string text = "The quick brown fox jumps over the lazy dog. ".Repeat(1000);
            
            // 普通模式
            var startTime = DateTime.Now;
            for (int i = 0; i < 1000; i++)
            {
                Regex.Matches(text, @"\b\w{5}\b");
            }
            var normalTime = DateTime.Now - startTime;
            
            // 编译模式
            Regex compiledRegex = new Regex(@"\b\w{5}\b", RegexOptions.Compiled);
            startTime = DateTime.Now;
            for (int i = 0; i < 1000; i++)
            {
                compiledRegex.Matches(text);
            }
            var compiledTime = DateTime.Now - startTime;
            
            Console.WriteLine($"普通模式耗时: {normalTime.TotalMilliseconds}ms");
            Console.WriteLine($"编译模式耗时: {compiledTime.TotalMilliseconds}ms");
            Console.WriteLine($"性能提升: {normalTime.TotalMilliseconds / compiledTime.TotalMilliseconds:F2}x");
        }

        #endregion

        #region 错误处理

        /// <summary>
        /// 异常处理示例
        /// </summary>
        public static void ExceptionHandlingExamples()
        {
            Console.WriteLine("\n=== 异常处理示例 ===");
            
            // 无效正则表达式
            try
            {
                Regex invalidRegex = new Regex(@"(\w+"); // 缺少闭合括号
                Console.WriteLine("这行不会执行");
            }
            catch (RegexException ex)
            {
                Console.WriteLine($"正则表达式异常: {ex.Message}");
                Console.WriteLine($"问题模式: {ex.Pattern}");
            }
            
            // 超时处理
            try
            {
                // 创建一个可能导致回溯的模式
                string maliciousPattern = @"(a+)+b";
                string maliciousText = new string('a', 1000);
                
                Regex timeoutRegex = new Regex(maliciousPattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));
                Match match = timeoutRegex.Match(maliciousText);
                Console.WriteLine($"匹配结果: {match.Success}");
            }
            catch (RegexException ex)
            {
                Console.WriteLine($"超时异常: {ex.Message}");
            }
        }

        #endregion

        #region 实用工具方法

        /// <summary>
        /// 验证输入格式
        /// </summary>
        public static bool ValidateInput(string input, string pattern, string fieldName)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"{fieldName}不能为空");
                return false;
            }
            
            if (!Regex.IsMatch(input, pattern))
            {
                Console.WriteLine($"{fieldName}格式不正确");
                return false;
            }
            
            Console.WriteLine($"{fieldName}验证通过");
            return true;
        }

        /// <summary>
        /// 提取文本中的特定信息
        /// </summary>
        public static List<string> ExtractInformation(string text, string pattern)
        {
            List<string> results = new List<string>();
            MatchCollection matches = Regex.Matches(text, pattern);
            
            foreach (Match match in matches)
            {
                results.Add(match.Value);
            }
            
            return results;
        }

        /// <summary>
        /// 清理和格式化文本
        /// </summary>
        public static string CleanAndFormatText(string text)
        {
            // 移除多余空格
            text = Regex.Replace(text, @"\s+", " ");
            
            // 移除特殊字符
            text = Regex.Replace(text, @"[^\w\s]", "");
            
            // 首字母大写
            text = Regex.Replace(text, @"\b\w", match => match.Value.ToUpper());
            
            return text.Trim();
        }

        #endregion

        #region 主方法

        /// <summary>
        /// 运行所有示例
        /// </summary>
        public static void RunAllExamples()
        {
            Console.WriteLine("C# 正则表达式综合示例");
            Console.WriteLine("========================");
            
            try
            {
                BasicCharacterClassExamples();
                QuantifierExamples();
                AnchorExamples();
                EmailValidationExamples();
                PhoneValidationExamples();
                UrlValidationExamples();
                IpAddressValidationExamples();
                DateValidationExamples();
                PasswordStrengthExamples();
                CaptureGroupExamples();
                ReplacementExamples();
                SplitExamples();
                NonCapturingGroupExamples();
                CompilationOptionsExamples();
                ExceptionHandlingExamples();
                
                Console.WriteLine("\n=== 实用工具方法示例 ===");
                
                // 验证示例
                ValidateInput("user@example.com", @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", "邮箱");
                ValidateInput("13812345678", @"^1[3-9]\d{9}$", "手机号");
                
                // 提取信息示例
                string text = "联系我: 138-1234-5678 或 159-8765-4321";
                List<string> phones = ExtractInformation(text, @"1[3-9]\d-\d{4}-\d{4}");
                Console.WriteLine($"提取的手机号: {string.Join(", ", phones)}");
                
                // 清理文本示例
                string messyText = "  hello   world!!!  this   is   a   test...  ";
                string cleanText = CleanAndFormatText(messyText);
                Console.WriteLine($"清理后的文本: '{cleanText}'");
                
                Console.WriteLine("\n所有示例运行完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"运行示例时发生错误: {ex.Message}");
            }
        }

        #endregion
    }

    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 重复字符串指定次数
        /// </summary>
        public static string Repeat(this string str, int count)
        {
            return string.Concat(Enumerable.Repeat(str, count));
        }
    }
}
