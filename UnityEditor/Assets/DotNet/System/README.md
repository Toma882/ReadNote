# .NET System 命名空间库操作指南

本目录包含了.NET Framework中System命名空间下主要库的详细示例和API参考。

## 目录结构

### 1. System.Core (核心库)
- **System.Linq** - LINQ查询操作
- **System.Linq.Expressions** - 表达式树
- **System.Dynamic** - 动态类型
- **System.Runtime.CompilerServices** - 编译器服务

### 2. System.Text (文本处理)
- **System.Text.RegularExpressions** - 正则表达式
- **System.Text.Encoding** - 字符编码
- **System.Text.StringBuilder** - 字符串构建器
- **System.Text.Json** - JSON处理

### 3. System.Math (数学运算)
- **System.Math** - 数学函数
- **System.Numerics** - 高精度数学
- **System.Random** - 随机数生成

### 4. System.DateTime (日期时间)
- **System.DateTime** - 日期时间处理
- **System.TimeSpan** - 时间间隔
- **System.DateTimeOffset** - 带时区的日期时间

### 5. System.Convert (类型转换)
- **System.Convert** - 类型转换工具
- **System.BitConverter** - 字节转换
- **System.GC** - 垃圾回收

### 6. System.Diagnostics (诊断)
- **System.Diagnostics.Process** - 进程管理
- **System.Diagnostics.Stopwatch** - 性能计时
- **System.Diagnostics.Debug** - 调试工具

### 7. System.Security (安全)
- **System.Security.Cryptography** - 加密解密
- **System.Security.Principal** - 身份验证
- **System.Security.Permissions** - 权限管理

### 8. System.Globalization (全球化)
- **System.Globalization.CultureInfo** - 文化信息
- **System.Globalization.DateTimeFormatInfo** - 日期格式
- **System.Globalization.NumberFormatInfo** - 数字格式

### 9. System.ComponentModel (组件模型)
- **System.ComponentModel.DataAnnotations** - 数据验证
- **System.ComponentModel.TypeConverter** - 类型转换器
- **System.ComponentModel.INotifyPropertyChanged** - 属性变化通知

### 10. System.Configuration (配置)
- **System.Configuration.ConfigurationManager** - 配置管理
- **System.Configuration.AppSettingsReader** - 应用设置

### 11. System 综合示例
- **SystemExample.cs** - 覆盖System命名空间常用API，包括：
  - System.Math：数学运算
  - System.DateTime/TimeSpan：日期时间
  - System.Convert/BitConverter：类型转换
  - System.GC：垃圾回收
  - System.Diagnostics.Stopwatch：性能计时
  - System.Security.Cryptography：加密解密
  - System.Globalization：全球化/本地化
  - System.ComponentModel：组件模型

## SystemExample.cs 使用说明

- 继承自MonoBehaviour，可直接挂载到Unity场景对象
- Inspector面板可配置是否自动运行
- 每个API模块均有详细中文注释和Debug.Log输出
- 适合查阅、学习和二次开发

## 使用说明

每个模块都包含：
- 详细的API使用示例
- 中文注释说明
- 性能优化建议
- 最佳实践指导

## 学习建议

1. **基础优先**：先学习System.Core和System.Text
2. **按需学习**：根据项目需求选择相应模块
3. **实践为主**：运行示例代码，理解API用法
4. **性能考虑**：注意各API的性能特点和使用场景

## 注意事项

- 某些System库在Unity中可能有限制
- 注意跨平台兼容性
- 合理使用内存和性能敏感的操作
- 遵循.NET最佳实践 