using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DotNet.Regex
{
    /// <summary>
    /// 正则表达式模式库
    /// 包含常用的正则表达式模式和识别码
    /// </summary>
    public static class RegexPatterns
    {
        #region 基础模式

        /// <summary>
        /// 数字相关模式
        /// </summary>
        public static class Numbers
        {
            /// <summary>
            /// 匹配任意数字
            /// </summary>
            public const string AnyDigit = @"\d";
            
            /// <summary>
            /// 匹配一个或多个数字
            /// </summary>
            public const string OneOrMoreDigits = @"\d+";
            
            /// <summary>
            /// 匹配恰好n位数字
            /// </summary>
            public static string ExactDigits(int n) => $@"\d{{{n}}}";
            
            /// <summary>
            /// 匹配n到m位数字
            /// </summary>
            public static string DigitsRange(int min, int max) => $@"\d{{{min},{max}}}";
            
            /// <summary>
            /// 匹配正整数
            /// </summary>
            public const string PositiveInteger = @"^[1-9]\d*$";
            
            /// <summary>
            /// 匹配负整数
            /// </summary>
            public const string NegativeInteger = @"^-[1-9]\d*$";
            
            /// <summary>
            /// 匹配整数（包括0）
            /// </summary>
            public const string Integer = @"^-?[0-9]+$";
            
            /// <summary>
            /// 匹配小数
            /// </summary>
            public const string Decimal = @"^-?[0-9]+\.?[0-9]*$";
            
            /// <summary>
            /// 匹配科学计数法
            /// </summary>
            public const string ScientificNotation = @"^-?[0-9]+\.?[0-9]*[eE][+-]?[0-9]+$";
        }

        /// <summary>
        /// 字母相关模式
        /// </summary>
        public static class Letters
        {
            /// <summary>
            /// 匹配任意字母
            /// </summary>
            public const string AnyLetter = @"[a-zA-Z]";
            
            /// <summary>
            /// 匹配小写字母
            /// </summary>
            public const string LowercaseLetter = @"[a-z]";
            
            /// <summary>
            /// 匹配大写字母
            /// </summary>
            public const string UppercaseLetter = @"[A-Z]";
            
            /// <summary>
            /// 匹配字母数字组合
            /// </summary>
            public const string Alphanumeric = @"[a-zA-Z0-9]";
            
            /// <summary>
            /// 匹配单词字符（字母、数字、下划线）
            /// </summary>
            public const string WordCharacter = @"\w";
            
            /// <summary>
            /// 匹配非单词字符
            /// </summary>
            public const string NonWordCharacter = @"\W";
        }

        /// <summary>
        /// 空白字符相关模式
        /// </summary>
        public static class Whitespace
        {
            /// <summary>
            /// 匹配任意空白字符
            /// </summary>
            public const string AnyWhitespace = @"\s";
            
            /// <summary>
            /// 匹配一个或多个空白字符
            /// </summary>
            public const string OneOrMoreWhitespace = @"\s+";
            
            /// <summary>
            /// 匹配非空白字符
            /// </summary>
            public const string NonWhitespace = @"\S";
            
            /// <summary>
            /// 匹配制表符
            /// </summary>
            public const string Tab = @"\t";
            
            /// <summary>
            /// 匹配换行符
            /// </summary>
            public const string Newline = @"\n";
            
            /// <summary>
            /// 匹配回车符
            /// </summary>
            public const string CarriageReturn = @"\r";
        }

        #endregion

        #region 验证模式

        /// <summary>
        /// 邮箱验证模式
        /// </summary>
        public static class Email
        {
            /// <summary>
            /// 简单邮箱验证
            /// </summary>
            public const string Simple = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            
            /// <summary>
            /// 严格邮箱验证
            /// </summary>
            public const string Strict = @"^[a-zA-Z0-9]([a-zA-Z0-9._-]*[a-zA-Z0-9])?@[a-zA-Z0-9]([a-zA-Z0-9.-]*[a-zA-Z0-9])?\.[a-zA-Z]{2,}$";
            
            /// <summary>
            /// RFC 5322 标准邮箱验证
            /// </summary>
            public const string RFC5322 = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        }

        /// <summary>
        /// 手机号码验证模式
        /// </summary>
        public static class Phone
        {
            /// <summary>
            /// 中国手机号（11位）
            /// </summary>
            public const string ChinaMobile = @"^1[3-9]\d{9}$";
            
            /// <summary>
            /// 中国手机号（带国际区号）
            /// </summary>
            public const string ChinaMobileWithCountryCode = @"^(\+86)?1[3-9]\d{9}$";
            
            /// <summary>
            /// 美国手机号
            /// </summary>
            public const string USMobile = @"^(\+1)?[2-9]\d{2}[2-9]\d{2}\d{4}$";
            
            /// <summary>
            /// 国际手机号（通用）
            /// </summary>
            public const string International = @"^\+[1-9]\d{1,14}$";
        }

        /// <summary>
        /// URL验证模式
        /// </summary>
        public static class URL
        {
            /// <summary>
            /// HTTP/HTTPS URL
            /// </summary>
            public const string HttpHttps = @"^https?://[^\s/$.?#].[^\s]*$";
            
            /// <summary>
            /// 完整URL（包含协议）
            /// </summary>
            public const string Full = @"^https?://(www\.)?[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(/.*)?$";
            
            /// <summary>
            /// 域名验证
            /// </summary>
            public const string Domain = @"^[a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(\.[a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        }

        /// <summary>
        /// IP地址验证模式
        /// </summary>
        public static class IPAddress
        {
            /// <summary>
            /// IPv4地址
            /// </summary>
            public const string IPv4 = @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";
            
            /// <summary>
            /// IPv6地址
            /// </summary>
            public const string IPv6 = @"^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$";
            
            /// <summary>
            /// IPv6地址（压缩格式）
            /// </summary>
            public const string IPv6Compressed = @"^(([0-9a-fA-F]{1,4}:)*[0-9a-fA-F]{1,4})?::(([0-9a-fA-F]{1,4}:)*[0-9a-fA-F]{1,4})?$";
        }

        /// <summary>
        /// 日期时间验证模式
        /// </summary>
        public static class DateTime
        {
            /// <summary>
            /// YYYY-MM-DD格式
            /// </summary>
            public const string YYYY_MM_DD = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";
            
            /// <summary>
            /// MM/DD/YYYY格式
            /// </summary>
            public const string MM_DD_YYYY = @"^(0[1-9]|1[0-2])/(0[1-9]|[12]\d|3[01])/\d{4}$";
            
            /// <summary>
            /// DD/MM/YYYY格式
            /// </summary>
            public const string DD_MM_YYYY = @"^(0[1-9]|[12]\d|3[01])/(0[1-9]|1[0-2])/\d{4}$";
            
            /// <summary>
            /// YYYY-MM-DD HH:mm:ss格式
            /// </summary>
            public const string YYYY_MM_DD_HH_mm_ss = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]) (0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$";
        }

        /// <summary>
        /// 密码强度验证模式
        /// </summary>
        public static class Password
        {
            /// <summary>
            /// 至少8位，包含大小写字母和数字
            /// </summary>
            public const string Medium = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{8,}$";
            
            /// <summary>
            /// 至少8位，包含大小写字母、数字和特殊字符
            /// </summary>
            public const string Strong = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            
            /// <summary>
            /// 至少12位，包含大小写字母、数字和特殊字符
            /// </summary>
            public const string VeryStrong = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$";
        }

        #endregion

        #region 身份证和证件号码

        /// <summary>
        /// 身份证号码验证模式
        /// </summary>
        public static class IDCard
        {
            /// <summary>
            /// 中国身份证18位
            /// </summary>
            public const string China18 = @"^\d{17}[\dXx]$";
            
            /// <summary>
            /// 中国身份证15位
            /// </summary>
            public const string China15 = @"^\d{15}$";
            
            /// <summary>
            /// 中国身份证（15位或18位）
            /// </summary>
            public const string China = @"^(\d{15}|\d{17}[\dXx])$";
        }

        /// <summary>
        /// 银行卡号验证模式
        /// </summary>
        public static class BankCard
        {
            /// <summary>
            /// 中国银行卡号（16-19位）
            /// </summary>
            public const string China = @"^\d{16,19}$";
            
            /// <summary>
            /// 信用卡号（16位）
            /// </summary>
            public const string CreditCard = @"^\d{16}$";
            
            /// <summary>
            /// 借记卡号（16-19位）
            /// </summary>
            public const string DebitCard = @"^\d{16,19}$";
        }

        #endregion

        #region 文件路径和名称

        /// <summary>
        /// 文件路径验证模式
        /// </summary>
        public static class FilePath
        {
            /// <summary>
            /// Windows文件路径
            /// </summary>
            public const string Windows = @"^[a-zA-Z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]*$";
            
            /// <summary>
            /// Unix/Linux文件路径
            /// </summary>
            public const string Unix = @"^/(?:[^/\0]|/(?!$))*$";
            
            /// <summary>
            /// 文件名（不包含路径）
            /// </summary>
            public const string FileName = @"^[^\\/:*?""<>|\r\n]+$";
            
            /// <summary>
            /// 文件扩展名
            /// </summary>
            public const string Extension = @"^\.[a-zA-Z0-9]+$";
        }

        #endregion

        #region 颜色和格式

        /// <summary>
        /// 颜色格式验证模式
        /// </summary>
        public static class Color
        {
            /// <summary>
            /// 十六进制颜色（#RRGGBB）
            /// </summary>
            public const string Hex = @"^#[0-9A-Fa-f]{6}$";
            
            /// <summary>
            /// 十六进制颜色（#RGB）
            /// </summary>
            public const string HexShort = @"^#[0-9A-Fa-f]{3}$";
            
            /// <summary>
            /// RGB颜色格式
            /// </summary>
            public const string RGB = @"^rgb\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*\)$";
            
            /// <summary>
            /// RGBA颜色格式
            /// </summary>
            public const string RGBA = @"^rgba\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(0|1|0\.\d+)\s*\)$";
        }

        #endregion

        #region 货币和金额

        /// <summary>
        /// 货币格式验证模式
        /// </summary>
        public static class Currency
        {
            /// <summary>
            /// 人民币格式
            /// </summary>
            public const string CNY = @"^¥\d+(\.\d{2})?$";
            
            /// <summary>
            /// 美元格式
            /// </summary>
            public const string USD = @"^\$\d+(\.\d{2})?$";
            
            /// <summary>
            /// 欧元格式
            /// </summary>
            public const string EUR = @"^€\d+(\.\d{2})?$";
            
            /// <summary>
            /// 通用货币格式（数字+小数点）
            /// </summary>
            public const string General = @"^\d+(\.\d{2})?$";
        }

        #endregion

        #region 网络相关

        /// <summary>
        /// MAC地址验证模式
        /// </summary>
        public static class MACAddress
        {
            /// <summary>
            /// MAC地址（冒号分隔）
            /// </summary>
            public const string ColonSeparated = @"^([0-9A-Fa-f]{2}:){5}[0-9A-Fa-f]{2}$";
            
            /// <summary>
            /// MAC地址（连字符分隔）
            /// </summary>
            public const string HyphenSeparated = @"^([0-9A-Fa-f]{2}-){5}[0-9A-Fa-f]{2}$";
            
            /// <summary>
            /// MAC地址（点分隔）
            /// </summary>
            public const string DotSeparated = @"^([0-9A-Fa-f]{4}\.){2}[0-9A-Fa-f]{4}$";
        }

        /// <summary>
        /// 端口号验证模式
        /// </summary>
        public static class Port
        {
            /// <summary>
            /// 有效端口号（1-65535）
            /// </summary>
            public const string Valid = @"^([1-9][0-9]{0,3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";
            
            /// <summary>
            /// 知名端口（1-1023）
            /// </summary>
            public const string WellKnown = @"^([1-9][0-9]{0,2}|10[0-1][0-9]|102[0-3])$";
        }

        #endregion

        #region 实用工具方法

        /// <summary>
        /// 验证字符串是否匹配指定模式
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式模式</param>
        /// <param name="options">正则表达式选项</param>
        /// <returns>是否匹配</returns>
        public static bool IsMatch(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
                return false;
            
            try
            {
                return Regex.IsMatch(input, pattern, options);
            }
            catch (RegexException)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取匹配的字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式模式</param>
        /// <param name="options">正则表达式选项</param>
        /// <returns>匹配的字符串，如果没有匹配则返回null</returns>
        public static string GetMatch(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
                return null;
            
            try
            {
                Match match = Regex.Match(input, pattern, options);
                return match.Success ? match.Value : null;
            }
            catch (RegexException)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有匹配的字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式模式</param>
        /// <param name="options">正则表达式选项</param>
        /// <returns>匹配的字符串列表</returns>
        public static List<string> GetAllMatches(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            List<string> matches = new List<string>();
            
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
                return matches;
            
            try
            {
                MatchCollection matchCollection = Regex.Matches(input, pattern, options);
                foreach (Match match in matchCollection)
                {
                    matches.Add(match.Value);
                }
            }
            catch (RegexException)
            {
                // 忽略异常，返回空列表
            }
            
            return matches;
        }

        /// <summary>
        /// 替换匹配的字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="options">正则表达式选项</param>
        /// <returns>替换后的字符串</returns>
        public static string Replace(string input, string pattern, string replacement, RegexOptions options = RegexOptions.None)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
                return input;
            
            try
            {
                return Regex.Replace(input, pattern, replacement, options);
            }
            catch (RegexException)
            {
                return input;
            }
        }

        /// <summary>
        /// 按模式分割字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式模式</param>
        /// <param name="options">正则表达式选项</param>
        /// <returns>分割后的字符串数组</returns>
        public static string[] Split(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
                return new string[] { input };
            
            try
            {
                return Regex.Split(input, pattern, options);
            }
            catch (RegexException)
            {
                return new string[] { input };
            }
        }

        #endregion

        #region 预定义模式集合

        /// <summary>
        /// 获取所有邮箱验证模式
        /// </summary>
        public static Dictionary<string, string> GetEmailPatterns()
        {
            return new Dictionary<string, string>
            {
                { "Simple", Email.Simple },
                { "Strict", Email.Strict },
                { "RFC5322", Email.RFC5322 }
            };
        }

        /// <summary>
        /// 获取所有手机号验证模式
        /// </summary>
        public static Dictionary<string, string> GetPhonePatterns()
        {
            return new Dictionary<string, string>
            {
                { "ChinaMobile", Phone.ChinaMobile },
                { "ChinaMobileWithCountryCode", Phone.ChinaMobileWithCountryCode },
                { "USMobile", Phone.USMobile },
                { "International", Phone.International }
            };
        }

        /// <summary>
        /// 获取所有日期格式验证模式
        /// </summary>
        public static Dictionary<string, string> GetDateTimePatterns()
        {
            return new Dictionary<string, string>
            {
                { "YYYY-MM-DD", DateTime.YYYY_MM_DD },
                { "MM/DD/YYYY", DateTime.MM_DD_YYYY },
                { "DD/MM/YYYY", DateTime.DD_MM_YYYY },
                { "YYYY-MM-DD HH:mm:ss", DateTime.YYYY_MM_DD_HH_mm_ss }
            };
        }

        /// <summary>
        /// 获取所有密码强度验证模式
        /// </summary>
        public static Dictionary<string, string> GetPasswordPatterns()
        {
            return new Dictionary<string, string>
            {
                { "Medium", Password.Medium },
                { "Strong", Password.Strong },
                { "VeryStrong", Password.VeryStrong }
            };
        }

        #endregion
    }
}
