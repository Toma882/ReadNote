using UnityEngine;
using UnityEditor;

namespace UnityEditor.Examples
{
    /// <summary>
    /// LicensingUtility 工具类示例
    /// 提供许可相关的实用工具功能
    /// </summary>
    public static class LicensingUtilityExample
    {
        #region 许可检查示例

        /// <summary>
        /// 检查是否有有效许可
        /// </summary>
        public static void HasAValidLicenseExample()
        {
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            Debug.Log($"许可检查结果:");
            Debug.Log($"- 是否有有效许可: {hasValidLicense}");
            
            if (hasValidLicense)
            {
                Debug.Log("✓ Unity许可证有效");
            }
            else
            {
                Debug.LogWarning("✗ Unity许可证无效或过期");
            }
        }

        /// <summary>
        /// 检查许可状态
        /// </summary>
        public static void CheckLicenseStatusExample()
        {
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            Debug.Log($"许可状态检查:");
            Debug.Log($"- 许可有效: {hasValidLicense}");
            
            if (hasValidLicense)
            {
                Debug.Log("- 可以使用Unity编辑器");
                Debug.Log("- 可以构建项目");
                Debug.Log("- 可以发布游戏");
            }
            else
            {
                Debug.LogWarning("- 无法使用Unity编辑器");
                Debug.LogWarning("- 无法构建项目");
                Debug.LogWarning("- 无法发布游戏");
            }
        }

        #endregion

        #region 许可类型检查示例

        /// <summary>
        /// 检查许可类型
        /// </summary>
        public static void CheckLicenseTypeExample()
        {
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            Debug.Log($"许可类型检查:");
            Debug.Log($"- 许可有效: {hasValidLicense}");
            
            if (hasValidLicense)
            {
                // 注意：Unity的许可类型检查通常需要更复杂的API
                // 这里只是示例，实际使用时可能需要其他方法
                Debug.Log("- 许可类型: 有效许可证");
                Debug.Log("- 可以使用所有Unity功能");
            }
            else
            {
                Debug.Log("- 许可类型: 无效或过期");
                Debug.Log("- 功能受限");
            }
        }

        /// <summary>
        /// 检查许可限制
        /// </summary>
        public static void CheckLicenseLimitationsExample()
        {
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            Debug.Log($"许可限制检查:");
            
            if (hasValidLicense)
            {
                Debug.Log("- 无功能限制");
                Debug.Log("- 可以创建商业项目");
                Debug.Log("- 可以发布到所有平台");
                Debug.Log("- 可以使用所有Unity服务");
            }
            else
            {
                Debug.LogWarning("- 功能受限");
                Debug.LogWarning("- 无法创建商业项目");
                Debug.LogWarning("- 无法发布游戏");
                Debug.LogWarning("- 无法使用Unity服务");
            }
        }

        #endregion

        #region 许可验证示例

        /// <summary>
        /// 验证许可
        /// </summary>
        public static void ValidateLicenseExample()
        {
            Debug.Log("开始许可验证...");
            
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            if (hasValidLicense)
            {
                Debug.Log("✓ 许可验证通过");
                Debug.Log("- Unity许可证有效");
                Debug.Log("- 可以正常使用Unity编辑器");
            }
            else
            {
                Debug.LogError("✗ 许可验证失败");
                Debug.LogError("- Unity许可证无效或过期");
                Debug.LogError("- 请检查许可证状态");
            }
        }

        /// <summary>
        /// 许可诊断
        /// </summary>
        public static void LicenseDiagnosticsExample()
        {
            Debug.Log("=== 许可诊断 ===");
            
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            Debug.Log($"✓ 许可检查: {(hasValidLicense ? "通过" : "失败")}");
            
            if (hasValidLicense)
            {
                Debug.Log("✓ Unity许可证状态: 有效");
                Debug.Log("✓ 编辑器功能: 可用");
                Debug.Log("✓ 构建功能: 可用");
                Debug.Log("✓ 发布功能: 可用");
            }
            else
            {
                Debug.LogWarning("✗ Unity许可证状态: 无效");
                Debug.LogWarning("✗ 编辑器功能: 受限");
                Debug.LogWarning("✗ 构建功能: 不可用");
                Debug.LogWarning("✗ 发布功能: 不可用");
            }
        }

        #endregion

        #region 许可信息示例

        /// <summary>
        /// 获取许可信息
        /// </summary>
        public static void GetLicenseInfoExample()
        {
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            Debug.Log($"许可信息:");
            Debug.Log($"- 许可状态: {(hasValidLicense ? "有效" : "无效")}");
            Debug.Log($"- Unity版本: {Application.unityVersion}");
            Debug.Log($"- 平台: {Application.platform}");
            
            if (hasValidLicense)
            {
                Debug.Log("- 许可类型: 有效许可证");
                Debug.Log("- 功能状态: 完全可用");
            }
            else
            {
                Debug.Log("- 许可类型: 无效或过期");
                Debug.Log("- 功能状态: 受限");
            }
        }

        /// <summary>
        /// 检查许可兼容性
        /// </summary>
        public static void CheckLicenseCompatibilityExample()
        {
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            Debug.Log($"许可兼容性检查:");
            Debug.Log($"- Unity版本: {Application.unityVersion}");
            Debug.Log($"- 平台: {Application.platform}");
            Debug.Log($"- 许可状态: {(hasValidLicense ? "有效" : "无效")}");
            
            if (hasValidLicense)
            {
                Debug.Log("- 兼容性: 完全兼容");
                Debug.Log("- 所有功能可用");
            }
            else
            {
                Debug.LogWarning("- 兼容性: 受限");
                Debug.LogWarning("- 部分功能不可用");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 许可状态监控
        /// </summary>
        public static void LicenseStatusMonitoringExample()
        {
            Debug.Log("许可状态监控:");
            
            // 检查当前许可状态
            bool currentStatus = LicensingUtility.HasAValidLicense();
            Debug.Log($"- 当前许可状态: {(currentStatus ? "有效" : "无效")}");
            
            // 模拟许可状态变化检测
            if (currentStatus)
            {
                Debug.Log("- 许可状态: 正常");
                Debug.Log("- 可以继续开发");
                Debug.Log("- 可以构建项目");
            }
            else
            {
                Debug.LogWarning("- 许可状态: 异常");
                Debug.LogWarning("- 需要检查许可证");
                Debug.LogWarning("- 功能可能受限");
            }
        }

        /// <summary>
        /// 许可检查工具
        /// </summary>
        public static void LicenseCheckToolExample()
        {
            Debug.Log("=== 许可检查工具 ===");
            
            // 基本许可检查
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            Debug.Log($"1. 基本许可检查:");
            Debug.Log($"   - 结果: {(hasValidLicense ? "通过" : "失败")}");
            
            // 功能可用性检查
            Debug.Log($"2. 功能可用性检查:");
            if (hasValidLicense)
            {
                Debug.Log("   - 编辑器功能: ✓ 可用");
                Debug.Log("   - 构建功能: ✓ 可用");
                Debug.Log("   - 发布功能: ✓ 可用");
                Debug.Log("   - Unity服务: ✓ 可用");
            }
            else
            {
                Debug.LogWarning("   - 编辑器功能: ✗ 受限");
                Debug.LogWarning("   - 构建功能: ✗ 不可用");
                Debug.LogWarning("   - 发布功能: ✗ 不可用");
                Debug.LogWarning("   - Unity服务: ✗ 不可用");
            }
            
            // 建议操作
            Debug.Log($"3. 建议操作:");
            if (hasValidLicense)
            {
                Debug.Log("   - 继续正常开发");
                Debug.Log("   - 可以构建和发布项目");
            }
            else
            {
                Debug.LogWarning("   - 检查Unity许可证");
                Debug.LogWarning("   - 更新或重新激活许可证");
                Debug.LogWarning("   - 联系Unity支持");
            }
        }

        /// <summary>
        /// 许可验证测试
        /// </summary>
        public static void LicenseValidationTestExample()
        {
            Debug.Log("许可验证测试:");
            
            // 多次检查许可状态
            int checkCount = 5;
            int validCount = 0;
            
            for (int i = 0; i < checkCount; i++)
            {
                bool hasValidLicense = LicensingUtility.HasAValidLicense();
                if (hasValidLicense)
                {
                    validCount++;
                }
                Debug.Log($"检查 {i + 1}: {(hasValidLicense ? "有效" : "无效")}");
            }
            
            Debug.Log($"验证结果:");
            Debug.Log($"- 总检查次数: {checkCount}");
            Debug.Log($"- 有效次数: {validCount}");
            Debug.Log($"- 有效率: {(validCount * 100.0f / checkCount):F1}%");
            
            if (validCount == checkCount)
            {
                Debug.Log("✓ 许可验证测试通过");
            }
            else
            {
                Debug.LogWarning("✗ 许可验证测试失败");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查许可是否有效
        /// </summary>
        private static bool IsLicenseValid()
        {
            return LicensingUtility.HasAValidLicense();
        }

        /// <summary>
        /// 获取许可状态描述
        /// </summary>
        private static string GetLicenseStatusDescription()
        {
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            return hasValidLicense ? "有效" : "无效";
        }

        /// <summary>
        /// 获取许可建议
        /// </summary>
        private static string GetLicenseAdvice()
        {
            bool hasValidLicense = LicensingUtility.HasAValidLicense();
            
            if (hasValidLicense)
            {
                return "许可证有效，可以正常使用Unity编辑器";
            }
            else
            {
                return "许可证无效，请检查许可证状态或联系Unity支持";
            }
        }

        #endregion
    }
}
