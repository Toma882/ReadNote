# .NET Security (安全) API 参考

## 概述

本文档提供了.NET中安全相关的完整API参考，包括加密、哈希、证书、身份验证等安全功能。

## 主要类和接口

### 加密相关

#### 对称加密
- [x] **System.Security.Cryptography.Aes** [AES加密] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.aes)
- [x] **System.Security.Cryptography.DES** [DES加密] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.des)
- [x] **System.Security.Cryptography.TripleDES** [3DES加密] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.tripledes)
- [x] **System.Security.Cryptography.RC2** [RC2加密] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.rc2)

#### 非对称加密
- [x] **System.Security.Cryptography.RSA** [RSA加密] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.rsa)
- [x] **System.Security.Cryptography.DSA** [DSA签名] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.dsa)
- [x] **System.Security.Cryptography.ECDsa** [椭圆曲线DSA] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.ecdsa)
- [x] **System.Security.Cryptography.ECDiffieHellman** [椭圆曲线Diffie-Hellman] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.ecdiffiehellman)

#### 哈希算法
- [x] **System.Security.Cryptography.SHA1** [SHA1哈希] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.sha1)
- [x] **System.Security.Cryptography.SHA256** [SHA256哈希] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.sha256)
- [x] **System.Security.Cryptography.SHA384** [SHA384哈希] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.sha384)
- [x] **System.Security.Cryptography.SHA512** [SHA512哈希] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.sha512)
- [x] **System.Security.Cryptography.MD5** [MD5哈希] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.md5)

#### 消息认证码
- [x] **System.Security.Cryptography.HMAC** [HMAC基类] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.hmac)
- [x] **System.Security.Cryptography.HMACSHA1** [HMAC-SHA1] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.hmacsha1)
- [x] **System.Security.Cryptography.HMACSHA256** [HMAC-SHA256] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.hmacsha256)

### 证书和身份验证

#### X.509证书
- [x] **System.Security.Cryptography.X509Certificates.X509Certificate** [X.509证书] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.x509certificates.x509certificate)
- [x] **System.Security.Cryptography.X509Certificates.X509Certificate2** [X.509证书2] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.x509certificates.x509certificate2)
- [x] **System.Security.Cryptography.X509Certificates.X509Store** [X.509证书存储] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.x509certificates.x509store)

#### 身份验证
- [x] **System.Security.Principal.WindowsIdentity** [Windows身份] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.principal.windowsidentity)
- [x] **System.Security.Principal.WindowsPrincipal** [Windows主体] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.principal.windowsprincipal)
- [x] **System.Security.Principal.GenericIdentity** [泛型身份] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.principal.genericidentity)
- [x] **System.Security.Principal.GenericPrincipal** [泛型主体] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.principal.genericprincipal)

### 权限和安全策略

#### 代码访问安全
- [x] **System.Security.Permissions.SecurityPermission** [安全权限] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.permissions.securitypermission)
- [x] **System.Security.Permissions.FileIOPermission** [文件IO权限] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.permissions.fileiopermission)
- [x] **System.Security.Permissions.RegistryPermission** [注册表权限] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.permissions.registrypermission)

#### 安全属性
- [x] **System.Security.SecurityCriticalAttribute** [安全关键特性] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.securitycriticalattribute)
- [x] **System.Security.SecuritySafeCriticalAttribute** [安全安全关键特性] (https://docs.microsoft.com/zh-cn/dotnet/api/system.security.securitysafecriticalattribute)

## 常用安全模式

### 密码加密
- [x] **密码哈希**
    - 使用BCrypt: `BCrypt.Net.BCrypt.HashPassword(password)`
    - 使用PBKDF2: `Rfc2898DeriveBytes.Pbkdf2()`
    - 使用Argon2: `Argon2.HashPassword()`

### 数据加密
- [x] **AES加密**
    - 生成密钥: `Aes.Create().GenerateKey()`
    - 加密数据: `Aes.Create().CreateEncryptor()`
    - 解密数据: `Aes.Create().CreateDecryptor()`

### 数字签名
- [x] **RSA签名**
    - 生成密钥对: `RSA.Create()`
    - 签名数据: `RSA.SignData()`
    - 验证签名: `RSA.VerifyData()`

### 安全随机数
- [x] **加密安全随机数**
    - 使用RNGCryptoServiceProvider: `RandomNumberGenerator.GetBytes()`
    - 生成安全随机字符串

## 最佳实践

### 密码安全
1. 使用强密码策略
2. 密码哈希存储
3. 盐值随机化
4. 定期更新密码

### 数据保护
1. 敏感数据加密存储
2. 传输过程加密
3. 密钥安全管理
4. 定期密钥轮换

### 身份验证
1. 多因素认证
2. 会话管理
3. 权限最小化原则
4. 审计日志

## 相关资源

- [.NET 安全文档](https://docs.microsoft.com/zh-cn/dotnet/standard/security/)
- [加密服务提供程序](https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography)
- [X.509证书](https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.x509certificates)
- [身份和权限](https://docs.microsoft.com/zh-cn/dotnet/api/system.security.principal)
