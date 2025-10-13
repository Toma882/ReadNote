# C# 正则表达式 (Regex) API 参考

## 概述

本文档提供了C#中正则表达式相关的完整API参考，包括System.Text.RegularExpressions命名空间中的所有主要类和方法。

## 主要类和接口

### 核心类

#### Regex 类
- [x] **System.Text.RegularExpressions.Regex** [正则表达式核心类] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regex)
    -[x] **Regex.IsMatch** [检查字符串是否匹配模式] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regex.ismatch)
    -[x] **Regex.Match** [查找第一个匹配项] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regex.match)
    -[x] **Regex.Matches** [查找所有匹配项] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regex.matches)
    -[x] **Regex.Replace** [替换匹配的文本] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regex.replace)
    -[x] **Regex.Split** [按模式分割字符串] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regex.split)

#### Match 类
- [x] **System.Text.RegularExpressions.Match** [单个匹配结果] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.match)
    -[x] **Match.Value** [匹配的文本值] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.match.value)
    -[x] **Match.Index** [匹配开始位置] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.match.index)
    -[x] **Match.Length** [匹配文本长度] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.match.length)
    -[x] **Match.Groups** [捕获组集合] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.match.groups)

#### MatchCollection 类
- [x] **System.Text.RegularExpressions.MatchCollection** [匹配结果集合] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.matchcollection)
    -[x] **MatchCollection.Count** [匹配项数量] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.matchcollection.count)
    -[x] **MatchCollection.Item** [按索引访问匹配项] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.matchcollection.item)

#### Group 类
- [x] **System.Text.RegularExpressions.Group** [捕获组] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.group)
    -[x] **Group.Value** [组的值] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.group.value)
    -[x] **Group.Success** [是否成功匹配] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.group.success)

#### GroupCollection 类
- [x] **System.Text.RegularExpressions.GroupCollection** [捕获组集合] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.groupcollection)
    -[x] **GroupCollection.Count** [组数量] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.groupcollection.count)

#### Capture 类
- [x] **System.Text.RegularExpressions.Capture** [捕获结果基类] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.capture)
    -[x] **Capture.Value** [捕获的值] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.capture.value)
    -[x] **Capture.Index** [捕获开始位置] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.capture.index)
    -[x] **Capture.Length** [捕获长度] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.capture.length)

### 枚举类型

#### RegexOptions 枚举
- [x] **System.Text.RegularExpressions.RegexOptions** [正则表达式选项] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexoptions)
    -[x] **RegexOptions.IgnoreCase** [忽略大小写] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexoptions)
    -[x] **RegexOptions.Multiline** [多行模式] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexoptions)
    -[x] **RegexOptions.Singleline** [单行模式] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexoptions)
    -[x] **RegexOptions.Compiled** [编译模式] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexoptions)
    -[x] **RegexOptions.ExplicitCapture** [显式捕获] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexoptions)
    -[x] **RegexOptions.IgnorePatternWhitespace** [忽略模式空白] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexoptions)

## 常用正则表达式模式

### 基础模式

#### 字符类
- [x] **数字匹配**
    - `\d` - 匹配任意数字 (0-9)
    - `\D` - 匹配任意非数字字符
    - `[0-9]` - 匹配0到9的数字
    - `[^0-9]` - 匹配非数字字符

- [x] **字母匹配**
    - `\w` - 匹配字母、数字、下划线
    - `\W` - 匹配非字母、数字、下划线
    - `[a-zA-Z]` - 匹配任意字母
    - `[a-z]` - 匹配小写字母
    - `[A-Z]` - 匹配大写字母

- [x] **空白字符**
    - `\s` - 匹配空白字符 (空格、制表符、换行符)
    - `\S` - 匹配非空白字符
    - `\t` - 匹配制表符
    - `\n` - 匹配换行符
    - `\r` - 匹配回车符

#### 量词
- [x] **基本量词**
    - `*` - 匹配0个或多个
    - `+` - 匹配1个或多个
    - `?` - 匹配0个或1个
    - `{n}` - 匹配恰好n个
    - `{n,}` - 匹配至少n个
    - `{n,m}` - 匹配n到m个

#### 位置锚点
- [x] **位置匹配**
    - `^` - 匹配字符串开始
    - `$` - 匹配字符串结束
    - `\b` - 匹配单词边界
    - `\B` - 匹配非单词边界

### 实用模式

#### 邮箱验证
- [x] **邮箱正则表达式**
    - 简单模式: `^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$`
    - 严格模式: `^[a-zA-Z0-9]([a-zA-Z0-9._-]*[a-zA-Z0-9])?@[a-zA-Z0-9]([a-zA-Z0-9.-]*[a-zA-Z0-9])?\.[a-zA-Z]{2,}$`

#### 手机号码验证
- [x] **中国手机号**
    - 11位数字: `^1[3-9]\d{9}$`
    - 带国际区号: `^(\+86)?1[3-9]\d{9}$`

#### 身份证号码验证
- [x] **中国身份证**
    - 18位: `^\d{17}[\dXx]$`
    - 15位: `^\d{15}$`

#### URL验证
- [x] **URL正则表达式**
    - HTTP/HTTPS: `^https?://[^\s/$.?#].[^\s]*$`
    - 完整URL: `^https?://(www\.)?[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(/.*)?$`

#### IP地址验证
- [x] **IP地址**
    - IPv4: `^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$`
    - IPv6: `^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$`

#### 日期时间验证
- [x] **日期格式**
    - YYYY-MM-DD: `^\d{4}-\d{2}-\d{2}$`
    - MM/DD/YYYY: `^\d{2}/\d{2}/\d{4}$`
    - DD/MM/YYYY: `^\d{2}/\d{2}/\d{4}$`

#### 密码强度验证
- [x] **密码规则**
    - 至少8位，包含大小写字母和数字: `^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{8,}$`
    - 至少8位，包含大小写字母、数字和特殊字符: `^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$`

## 高级特性

### 捕获组
- [x] **命名捕获组**
    - `(?<name>pattern)` - 命名捕获组
    - `(?'name'pattern)` - 命名捕获组（另一种语法）
    - `(?P<name>pattern)` - Python风格命名组

- [x] **非捕获组**
    - `(?:pattern)` - 非捕获组
    - `(?=pattern)` - 正向先行断言
    - `(?!pattern)` - 负向先行断言
    - `(?<=pattern)` - 正向后行断言
    - `(?<!pattern)` - 负向后行断言

### 替换模式
- [x] **替换语法**
    - `$1, $2, $3...` - 按编号引用捕获组
    - `${name}` - 按名称引用捕获组
    - `$&` - 整个匹配
    - `$` - 匹配前的文本
    - `$'` - 匹配后的文本

## 性能优化

### 编译选项
- [x] **RegexOptions.Compiled** - 预编译正则表达式
- [x] **Regex.CompileToAssembly** - 编译到程序集
- [x] **Regex.CacheSize** - 缓存大小设置

### 最佳实践
- [x] **避免回溯**
    - 使用原子组 `(?>pattern)`
    - 避免嵌套量词
    - 使用具体字符类而非通用字符

- [x] **性能优化**
    - 预编译常用模式
    - 使用非捕获组
    - 避免过度复杂的模式

## 错误处理

### 异常类型
- [x] **System.Text.RegularExpressions.RegexException** [正则表达式异常] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexexception)
    -[x] **RegexException.Message** [异常消息] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexexception.message)
    -[x] **RegexException.Pattern** [问题模式] (https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regexexception.pattern)

### 常见错误
- [x] **语法错误**
    - 未闭合的括号
    - 无效的转义序列
    - 不匹配的字符类

- [x] **运行时错误**
    - 超时异常
    - 内存不足
    - 栈溢出

## 使用示例

### 基础用法
```csharp
// 检查是否匹配
bool isMatch = Regex.IsMatch("Hello World", @"\bWorld\b");

// 查找匹配
Match match = Regex.Match("Phone: 123-456-7890", @"\d{3}-\d{3}-\d{4}");

// 查找所有匹配
MatchCollection matches = Regex.Matches("abc123def456", @"\d+");

// 替换文本
string result = Regex.Replace("Hello World", @"World", "Universe");

// 分割字符串
string[] parts = Regex.Split("a,b;c:d", @"[,;:]");
```

### 高级用法
```csharp
// 使用命名捕获组
Match match = Regex.Match("John Doe", @"(?<FirstName>\w+)\s+(?<LastName>\w+)");
string firstName = match.Groups["FirstName"].Value;
string lastName = match.Groups["LastName"].Value;

// 使用替换模式
string result = Regex.Replace("John Doe", @"(?<FirstName>\w+)\s+(?<LastName>\w+)", 
    "${LastName}, ${FirstName}");

// 使用编译选项
Regex regex = new Regex(@"\b\w+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
```

## 相关资源

- [.NET 正则表达式文档](https://docs.microsoft.com/zh-cn/dotnet/standard/base-types/regular-expressions)
- [正则表达式语言参考](https://docs.microsoft.com/zh-cn/dotnet/standard/base-types/regular-expression-language-quick-reference)
- [正则表达式最佳实践](https://docs.microsoft.com/zh-cn/dotnet/standard/base-types/best-practices)
- [正则表达式性能](https://docs.microsoft.com/zh-cn/dotnet/standard/base-types/backtracking-in-regular-expressions)
