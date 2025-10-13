using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.IO;

namespace DotNet.Security
{
    /// <summary>
    /// .NET Security (安全) API 综合示例
    /// 展示System.Security.Cryptography命名空间中所有主要功能
    /// </summary>
    public class SecurityExample
    {
        #region 对称加密示例

        /// <summary>
        /// AES加密解密示例
        /// </summary>
        public static void AesEncryptionExample()
        {
            Console.WriteLine("=== AES加密解密示例 ===");
            
            string originalText = "这是需要加密的敏感数据！";
            Console.WriteLine($"原始文本: {originalText}");
            
            // 创建AES实例
            using (Aes aes = Aes.Create())
            {
                // 生成密钥和IV
                aes.GenerateKey();
                aes.GenerateIV();
                
                Console.WriteLine($"密钥长度: {aes.Key.Length * 8} 位");
                Console.WriteLine($"IV长度: {aes.IV.Length * 8} 位");
                
                // 加密
                byte[] encrypted = EncryptStringToBytes(originalText, aes.Key, aes.IV);
                Console.WriteLine($"加密后: {Convert.ToBase64String(encrypted)}");
                
                // 解密
                string decrypted = DecryptStringFromBytes(encrypted, aes.Key, aes.IV);
                Console.WriteLine($"解密后: {decrypted}");
                
                Console.WriteLine($"加密解密成功: {originalText == decrypted}");
            }
        }

        /// <summary>
        /// AES字符串加密
        /// </summary>
        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            byte[] encrypted;
            
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            
            return encrypted;
        }

        /// <summary>
        /// AES字符串解密
        /// </summary>
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            string plaintext = null;
            
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            
            return plaintext;
        }

        /// <summary>
        /// DES加密解密示例
        /// </summary>
        public static void DesEncryptionExample()
        {
            Console.WriteLine("\n=== DES加密解密示例 ===");
            
            string originalText = "DES加密测试";
            Console.WriteLine($"原始文本: {originalText}");
            
            using (DES des = DES.Create())
            {
                des.GenerateKey();
                des.GenerateIV();
                
                // 加密
                byte[] encrypted = EncryptWithDes(originalText, des.Key, des.IV);
                Console.WriteLine($"DES加密后: {Convert.ToBase64String(encrypted)}");
                
                // 解密
                string decrypted = DecryptWithDes(encrypted, des.Key, des.IV);
                Console.WriteLine($"DES解密后: {decrypted}");
            }
        }

        private static byte[] EncryptWithDes(string plainText, byte[] key, byte[] iv)
        {
            using (DES des = DES.Create())
            {
                des.Key = key;
                des.IV = iv;
                
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(data, 0, data.Length);
                    }
                    return ms.ToArray();
                }
            }
        }

        private static string DecryptWithDes(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (DES des = DES.Create())
            {
                des.Key = key;
                des.IV = iv;
                
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion

        #region 非对称加密示例

        /// <summary>
        /// RSA加密解密示例
        /// </summary>
        public static void RsaEncryptionExample()
        {
            Console.WriteLine("\n=== RSA加密解密示例 ===");
            
            string originalText = "RSA非对称加密测试数据";
            Console.WriteLine($"原始文本: {originalText}");
            
            
            var dict = new Dictionary<string, string>();
            using(dict.TryGetValue("publicKey", out string publicKey))
            {
                Console.WriteLine($"公钥: {publicKey}");
            }
            using(dict.TryGetValue("privateKey", out string privateKey))
            {
                Console.WriteLine($"私钥: {privateKey}");
            }
            using (RSA rsa = RSA.Create())
            {
                // 生成密钥对
                Console.WriteLine($"RSA密钥长度: {rsa.KeySize} 位");
                
                // 获取公钥和私钥
                string publicKey = rsa.ToXmlString(false);
                string privateKey = rsa.ToXmlString(true);
                
                Console.WriteLine($"公钥长度: {publicKey.Length} 字符");
                Console.WriteLine($"私钥长度: {privateKey.Length} 字符");
                
                // 使用公钥加密
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(originalText);
                byte[] encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);
                Console.WriteLine($"RSA加密后: {Convert.ToBase64String(encryptedData)}");
                
                // 使用私钥解密
                byte[] decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
                string decryptedText = Encoding.UTF8.GetString(decryptedData);
                Console.WriteLine($"RSA解密后: {decryptedText}");
                
                Console.WriteLine($"RSA加密解密成功: {originalText == decryptedText}");
            }
        }

        /// <summary>
        /// RSA数字签名示例
        /// </summary>
        public static void RsaSignatureExample()
        {
            Console.WriteLine("\n=== RSA数字签名示例 ===");
            
            string message = "这是需要签名的消息";
            Console.WriteLine($"原始消息: {message}");
            
            using (RSA rsa = RSA.Create())
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                
                // 使用私钥签名
                byte[] signature = rsa.SignData(messageBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                Console.WriteLine($"数字签名: {Convert.ToBase64String(signature)}");
                
                // 使用公钥验证签名
                bool isValid = rsa.VerifyData(messageBytes, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                Console.WriteLine($"签名验证结果: {isValid}");
                
                // 测试篡改消息
                byte[] tamperedMessage = Encoding.UTF8.GetBytes("这是被篡改的消息");
                bool isTamperedValid = rsa.VerifyData(tamperedMessage, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                Console.WriteLine($"篡改消息验证结果: {isTamperedValid}");
            }
        }

        #endregion

        #region 哈希算法示例

        /// <summary>
        /// SHA哈希算法示例
        /// </summary>
        public static void ShaHashExample()
        {
            Console.WriteLine("\n=== SHA哈希算法示例 ===");
            
            string input = "这是需要计算哈希的数据";
            Console.WriteLine($"输入数据: {input}");
            
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            
            // SHA1
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hash1 = sha1.ComputeHash(inputBytes);
                Console.WriteLine($"SHA1: {Convert.ToHexString(hash1)}");
            }
            
            // SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash256 = sha256.ComputeHash(inputBytes);
                Console.WriteLine($"SHA256: {Convert.ToHexString(hash256)}");
            }
            
            // SHA384
            using (SHA384 sha384 = SHA384.Create())
            {
                byte[] hash384 = sha384.ComputeHash(inputBytes);
                Console.WriteLine($"SHA384: {Convert.ToHexString(hash384)}");
            }
            
            // SHA512
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hash512 = sha512.ComputeHash(inputBytes);
                Console.WriteLine($"SHA512: {Convert.ToHexString(hash512)}");
            }
        }

        /// <summary>
        /// MD5哈希示例
        /// </summary>
        public static void Md5HashExample()
        {
            Console.WriteLine("\n=== MD5哈希示例 ===");
            
            string input = "MD5哈希测试数据";
            Console.WriteLine($"输入数据: {input}");
            
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = md5.ComputeHash(inputBytes);
                Console.WriteLine($"MD5: {Convert.ToHexString(hash)}");
            }
        }

        /// <summary>
        /// HMAC示例
        /// </summary>
        public static void HmacExample()
        {
            Console.WriteLine("\n=== HMAC示例 ===");
            
            string message = "HMAC消息认证码测试";
            string key = "secret-key-12345";
            
            Console.WriteLine($"消息: {message}");
            Console.WriteLine($"密钥: {key}");
            
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            
            // HMAC-SHA1
            using (HMACSHA1 hmac1 = new HMACSHA1(keyBytes))
            {
                byte[] hash1 = hmac1.ComputeHash(messageBytes);
                Console.WriteLine($"HMAC-SHA1: {Convert.ToHexString(hash1)}");
            }
            
            // HMAC-SHA256
            using (HMACSHA256 hmac256 = new HMACSHA256(keyBytes))
            {
                byte[] hash256 = hmac256.ComputeHash(messageBytes);
                Console.WriteLine($"HMAC-SHA256: {Convert.ToHexString(hash256)}");
            }
        }

        #endregion

        #region 密码安全示例

        /// <summary>
        /// 密码哈希示例
        /// </summary>
        public static void PasswordHashExample()
        {
            Console.WriteLine("\n=== 密码哈希示例 ===");
            
            string password = "MySecurePassword123!";
            Console.WriteLine($"原始密码: {password}");
            
            // 生成盐值
            byte[] salt = GenerateSalt(32);
            Console.WriteLine($"盐值: {Convert.ToBase64String(salt)}");
            
            // 使用PBKDF2进行密码哈希
            string hashedPassword = HashPasswordWithPBKDF2(password, salt, 10000);
            Console.WriteLine($"哈希密码: {hashedPassword}");
            
            // 验证密码
            bool isValid = VerifyPasswordWithPBKDF2(password, hashedPassword, salt, 10000);
            Console.WriteLine($"密码验证: {isValid}");
            
            // 测试错误密码
            bool isInvalid = VerifyPasswordWithPBKDF2("WrongPassword", hashedPassword, salt, 10000);
            Console.WriteLine($"错误密码验证: {isInvalid}");
        }

        /// <summary>
        /// 生成随机盐值
        /// </summary>
        private static byte[] GenerateSalt(int length)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[length];
                rng.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// 使用PBKDF2哈希密码
        /// </summary>
        private static string HashPasswordWithPBKDF2(string password, byte[] salt, int iterations)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32字节 = 256位
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// 验证PBKDF2密码
        /// </summary>
        private static bool VerifyPasswordWithPBKDF2(string password, string hashedPassword, byte[] salt, int iterations)
        {
            string computedHash = HashPasswordWithPBKDF2(password, salt, iterations);
            return computedHash == hashedPassword;
        }

        #endregion

        #region 证书示例

        /// <summary>
        /// X.509证书示例
        /// </summary>
        public static void X509CertificateExample()
        {
            Console.WriteLine("\n=== X.509证书示例 ===");
            
            try
            {
                // 获取当前用户的证书存储
                using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
                {
                    store.Open(OpenFlags.ReadOnly);
                    
                    Console.WriteLine($"证书存储中的证书数量: {store.Certificates.Count}");
                    
                    // 显示前5个证书
                    int count = 0;
                    foreach (X509Certificate2 cert in store.Certificates)
                    {
                        if (count >= 5) break;
                        
                        Console.WriteLine($"\n证书 {count + 1}:");
                        Console.WriteLine($"  主题: {cert.Subject}");
                        Console.WriteLine($"  颁发者: {cert.Issuer}");
                        Console.WriteLine($"  有效期: {cert.NotBefore:yyyy-MM-dd} 到 {cert.NotAfter:yyyy-MM-dd}");
                        Console.WriteLine($"  是否有效: {cert.NotBefore <= DateTime.Now && DateTime.Now <= cert.NotAfter}");
                        
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"证书操作异常: {ex.Message}");
            }
        }

        #endregion

        #region 身份验证示例

        /// <summary>
        /// Windows身份验证示例
        /// </summary>
        public static void WindowsIdentityExample()
        {
            Console.WriteLine("\n=== Windows身份验证示例 ===");
            
            try
            {
                // 获取当前Windows身份
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                
                Console.WriteLine($"用户名: {identity.Name}");
                Console.WriteLine($"身份类型: {identity.AuthenticationType}");
                Console.WriteLine($"是否已验证: {identity.IsAuthenticated}");
                Console.WriteLine($"是否匿名: {identity.IsAnonymous}");
                Console.WriteLine($"是否Guest: {identity.IsGuest}");
                Console.WriteLine($"是否系统: {identity.IsSystem}");
                
                // 获取用户组
                Console.WriteLine("\n用户组:");
                foreach (IdentityReference group in identity.Groups)
                {
                    Console.WriteLine($"  - {group.Translate(typeof(NTAccount))}");
                }
                
                // 获取权限
                Console.WriteLine("\n权限:");
                foreach (string claim in identity.Claims)
                {
                    Console.WriteLine($"  - {claim}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"身份验证异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 泛型身份示例
        /// </summary>
        public static void GenericIdentityExample()
        {
            Console.WriteLine("\n=== 泛型身份示例 ===");
            
            // 创建泛型身份
            GenericIdentity identity = new GenericIdentity("testuser", "CustomAuth");
            GenericPrincipal principal = new GenericPrincipal(identity, new string[] { "Admin", "User" });
            
            Console.WriteLine($"身份名称: {identity.Name}");
            Console.WriteLine($"身份类型: {identity.AuthenticationType}");
            Console.WriteLine($"是否已验证: {identity.IsAuthenticated}");
            
            Console.WriteLine($"是否在Admin角色: {principal.IsInRole("Admin")}");
            Console.WriteLine($"是否在User角色: {principal.IsInRole("User")}");
            Console.WriteLine($"是否在Guest角色: {principal.IsInRole("Guest")}");
        }

        #endregion

        #region 安全随机数示例

        /// <summary>
        /// 加密安全随机数示例
        /// </summary>
        public static void SecureRandomExample()
        {
            Console.WriteLine("\n=== 加密安全随机数示例 ===");
            
            // 生成随机字节
            byte[] randomBytes = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
                Console.WriteLine($"随机字节: {Convert.ToBase64String(randomBytes)}");
            }
            
            // 生成随机整数
            byte[] randomIntBytes = new byte[4];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomIntBytes);
                int randomInt = BitConverter.ToInt32(randomIntBytes, 0);
                Console.WriteLine($"随机整数: {randomInt}");
            }
            
            // 生成随机字符串
            string randomString = GenerateSecureRandomString(16);
            Console.WriteLine($"随机字符串: {randomString}");
            
            // 生成随机密码
            string randomPassword = GenerateSecurePassword(12);
            Console.WriteLine($"随机密码: {randomPassword}");
        }

        /// <summary>
        /// 生成安全随机字符串
        /// </summary>
        private static string GenerateSecureRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            byte[] randomBytes = new byte[length];
            
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[randomBytes[i] % chars.Length];
            }
            
            return new string(result);
        }

        /// <summary>
        /// 生成安全随机密码
        /// </summary>
        private static string GenerateSecurePassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            byte[] randomBytes = new byte[length];
            
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[randomBytes[i] % chars.Length];
            }
            
            return new string(result);
        }

        #endregion

        #region 主方法

        /// <summary>
        /// 运行所有安全示例
        /// </summary>
        public static void RunAllExamples()
        {
            Console.WriteLine("C# Security (安全) API 综合示例");
            Console.WriteLine("================================");
            
            try
            {
                AesEncryptionExample();
                DesEncryptionExample();
                RsaEncryptionExample();
                RsaSignatureExample();
                ShaHashExample();
                Md5HashExample();
                HmacExample();
                PasswordHashExample();
                X509CertificateExample();
                WindowsIdentityExample();
                GenericIdentityExample();
                SecureRandomExample();
                
                Console.WriteLine("\n所有安全示例运行完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"运行示例时发生错误: {ex.Message}");
            }
        }

        #endregion
    }
}
