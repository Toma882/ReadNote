using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;

namespace OdinSamples.Validation
{
    /*
        学习要点：
        ----------------------
        Required 特性可以标记字段为必填，如果为空会显示错误
            参数
            string message: 错误消息
        ----------------------
        ValidateInput 特性可以自定义验证逻辑
            参数
            string methodName: 验证方法名称
            string message: 错误消息
            InfoMessageType type: 错误消息类型
        ----------------------
        MinValue 特性可以限制最小值
            参数
            int minValue: 最小值
        MaxValue 特性可以限制最大值
            参数
            int maxValue: 最大值
        Range 特性可以限制范围
            参数
            int minValue: 最小值
            int maxValue: 最大值
        ----------------------
        MinMaxSlider 特性可以限制范围
            参数
            int minValue: 最小值
            int maxValue: 最大值
            bool showFields: 是否显示范围输入框
        ----------------------
        FilePath 特性可以限制文件路径
            参数
            string extensions: 文件扩展名
        FolderPath 特性可以限制文件夹路径
            参数
            bool RequireExistingPath: 是否要求路径必须存在
        ----------------------
        AssetsOnly 特性可以限制只能选择项目资源
            参数
            bool SceneObjectsOnly: 只能选择场景对象
        ----------------------
        SceneObjectsOnly 特性可以限制只能选择场景对象
            参数
            bool SceneObjectsOnly: 只能选择场景对象
        ----------------------
        ChildGameObjectsOnly 特性可以限制只能选择子对象
            参数
            bool SceneObjectsOnly: 只能选择场景对象
        ----------------------
        AssetsOnly 特性可以限制只能选择项目资源
            参数
            bool SceneObjectsOnly: 只能选择场景对象
        ----------------------
        DisableInPlayMode 特性可以限制在播放模式下禁用
            参数
            bool DisableInPlayMode: 在播放模式下禁用
        ----------------------
    */


    /// <summary>
    /// Odin Inspector 验证和错误检查演示
    /// 展示各种数据验证特性
    /// </summary>
    public class ValidationSample : MonoBehaviour
    {
        #region Required 必填验证
        
        [Title("必填验证")]
        
        [Required]
        [InfoBox("Required 标记字段为必填，如果为空会显示错误")]
        public GameObject requiredObject;
        
        [Required("必须填写角色名称！")]
        public string characterName;
        
        [Required]
        public Transform requiredTransform;
        
        #endregion

        #region ValidateInput 自定义验证
        
        [Title("自定义验证")]
        
        [ValidateInput("ValidateUsername", "用户名长度必须在3-20个字符之间")]
        [InfoBox("ValidateInput 允许自定义验证逻辑")]
        public string username = "";
        
        [ValidateInput("ValidateEmail", "请输入有效的邮箱地址")]
        public string email = "";
        
        [ValidateInput("ValidatePassword", "密码必须至少8个字符")]
        public string password = "";
        
        [ValidateInput("ValidateAge", "年龄必须在1-150之间")]
        public int age = 0;
        
        // 验证方法
        private bool ValidateUsername(string value)
        {
            return value.Length >= 3 && value.Length <= 20;
        }
        
        private bool ValidateEmail(string value)
        {
            return value.Contains("@") && value.Contains(".");
        }
        
        private bool ValidatePassword(string value)
        {
            return value.Length >= 8;
        }
        
        private bool ValidateAge(int value)
        {
            return value >= 1 && value <= 150;
        }
        
        #endregion

        #region MinValue / MaxValue 数值范围
        
        [Title("数值范围验证")]
        
        [MinValue(0)]
        [InfoBox("MinValue 限制最小值")]
        public int minValueExample = 10;
        
        [MaxValue(100)]
        [InfoBox("MaxValue 限制最大值")]
        public float maxValueExample = 50f;
        
        [MinValue(0), MaxValue(100)]
        [InfoBox("同时使用 MinValue 和 MaxValue")]
        public int health = 100;
        
        [Range(0, 100)]
        [InfoBox("Range 提供滑动条，同时限制范围")]
        public float percentage = 50f;
        
        [MinValue("@minHealth"), MaxValue("@maxHealth")]
        [InfoBox("可以引用其他字段作为最小/最大值")]
        public int currentHealth = 50;
        
        [HideInInspector]
        public int minHealth = 0;
        [HideInInspector]
        public int maxHealth = 100;
        
        #endregion

        #region MinMaxSlider 范围滑动条
        
        [Title("范围滑动条")]
        
        [MinMaxSlider(0, 100)]
        [InfoBox("MinMaxSlider 提供双向滑动条，用于选择范围")]
        public Vector2 damageRange = new Vector2(10, 50);
        
        [MinMaxSlider(0, 24, true)]
        [InfoBox("设置 true 参数显示数值输入框")]
        public Vector2 activeHours = new Vector2(9, 18);
        
        [MinMaxSlider("@minValue", "@maxValue")]
        [InfoBox("范围值也可以引用其他字段")]
        public Vector2 customRange = new Vector2(20, 80);
        
        [HideInInspector]
        public float minValue = 0;
        [HideInInspector]
        public float maxValue = 100;
        
        #endregion

        #region FilePath / FolderPath 路径验证
        
        [Title("路径验证")]
        
        [FilePath]
        [InfoBox("FilePath 提供文件选择对话框")]
        public string configFilePath;
        
        [FilePath(Extensions = "txt")]
        [InfoBox("可以限制文件扩展名")]
        public string textFilePath;
        
        [FilePath(Extensions = "png,jpg,jpeg")]
        [InfoBox("可以指定多个扩展名")]
        public string imagePath;
        
        [FolderPath]
        [InfoBox("FolderPath 用于选择文件夹")]
        public string dataFolderPath;
        
        [FolderPath(RequireExistingPath = true)]
        [InfoBox("RequireExistingPath 要求路径必须存在")]
        public string existingFolderPath;
        
        #endregion

        #region AssetsOnly / SceneObjectsOnly 对象验证
        
        [Title("对象类型验证")]
        
        [AssetsOnly]
        [Required("必须指定预制体")]
        [InfoBox("AssetsOnly 只能选择项目资源")]
        public GameObject prefabAsset;
        
        [SceneObjectsOnly]
        [Required("必须指定场景对象")]
        [InfoBox("SceneObjectsOnly 只能选择场景中的对象")]
        public GameObject sceneObject;
        
        [AssetsOnly]
        [ValidateInput("ValidateSprite", "精灵图片尺寸必须是256x256")]
        public Sprite sprite;
        
        private bool ValidateSprite(Sprite sprite)
        {
            if (sprite == null) return true;
            return sprite.texture.width == 256 && sprite.texture.height == 256;
        }
        
        #endregion

        #region ChildGameObjectsOnly / AssetsOnly 引用验证
        
        [Title("引用验证")]
        
        [ChildGameObjectsOnly]
        [InfoBox("ChildGameObjectsOnly 只能选择子对象")]
        public Transform childTransform;
        
        [AssetsOnly]
        [Required]
        [InfoBox("结合多个验证特性")]
        public Material requiredMaterial;
        
        #endregion

        #region 自定义验证消息
        
        [Title("自定义验证消息")]
        
        [ValidateInput("ValidateScore", "分数必须在0-100之间，当前值：$value", 
            InfoMessageType.Warning)]
        [InfoBox("验证消息支持 $value 占位符显示当前值")]
        public int score = 0;
        
        [ValidateInput("ValidateLevel", DefaultMessage = "等级验证失败")]
        public int level = 1;
        
        [ValidateInput("ValidateName", ContinuousValidationCheck = true)]
        [InfoBox("ContinuousValidationCheck 实时验证")]
        public string playerName = "";
        
        private bool ValidateScore(int value)
        {
            return value >= 0 && value <= 100;
        }
        
        private bool ValidateLevel(int value)
        {
            return value >= 1 && value <= 99;
        }
        
        private bool ValidateName(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Length >= 3;
        }
        
        #endregion

        #region DisableInPlayMode / DisableInEditorMode
        
        [Title("播放模式验证")]
        
        [DisableInPlayMode]
        [InfoBox("DisableInPlayMode 在播放模式下禁用")]
        public string editorOnlyField = "只能在编辑器模式修改";
        
        [DisableInEditorMode]
        [InfoBox("DisableInEditorMode 在编辑器模式下禁用")]
        public string playModeOnlyField = "只能在播放模式修改";
        
        #endregion

        #region 复杂验证示例
        
        [Title("复杂验证示例")]
        
        [BoxGroup("用户注册")]
        [Required("请输入用户名")]
        [ValidateInput("ValidateRegistrationUsername", "用户名必须是3-20个字符的字母或数字")]
        public string registrationUsername;
        
        [BoxGroup("用户注册")]
        [Required("请输入邮箱")]
        [ValidateInput("ValidateRegistrationEmail", "请输入有效的邮箱地址")]
        public string registrationEmail;
        
        [BoxGroup("用户注册")]
        [Required("请输入密码")]
        [ValidateInput("ValidateRegistrationPassword", "密码必须至少8个字符，包含字母和数字")]
        public string registrationPassword;
        
        [BoxGroup("用户注册")]
        [MinValue(13), MaxValue(120)]
        [InfoBox("年龄必须在13-120之间")]
        public int registrationAge = 18;
        
        [BoxGroup("用户注册")]
        [Button("验证注册信息", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void ValidateRegistration()
        {
            bool isValid = true;
            string message = "注册信息验证结果：\n";
            
            if (string.IsNullOrEmpty(registrationUsername))
            {
                message += "❌ 用户名不能为空\n";
                isValid = false;
            }
            else if (!ValidateRegistrationUsername(registrationUsername))
            {
                message += "❌ 用户名格式不正确\n";
                isValid = false;
            }
            else
            {
                message += "✓ 用户名格式正确\n";
            }
            
            if (string.IsNullOrEmpty(registrationEmail))
            {
                message += "❌ 邮箱不能为空\n";
                isValid = false;
            }
            else if (!ValidateRegistrationEmail(registrationEmail))
            {
                message += "❌ 邮箱格式不正确\n";
                isValid = false;
            }
            else
            {
                message += "✓ 邮箱格式正确\n";
            }
            
            if (string.IsNullOrEmpty(registrationPassword))
            {
                message += "❌ 密码不能为空\n";
                isValid = false;
            }
            else if (!ValidateRegistrationPassword(registrationPassword))
            {
                message += "❌ 密码强度不足\n";
                isValid = false;
            }
            else
            {
                message += "✓ 密码强度合格\n";
            }
            
            if (registrationAge < 13 || registrationAge > 120)
            {
                message += "❌ 年龄不在有效范围\n";
                isValid = false;
            }
            else
            {
                message += "✓ 年龄有效\n";
            }
            
            message += isValid ? "\n✅ 所有信息验证通过！" : "\n❌ 请修正错误信息";
            Debug.Log(message);
        }
        
        private bool ValidateRegistrationUsername(string value)
        {
            if (value.Length < 3 || value.Length > 20) return false;
            foreach (char c in value)
            {
                if (!char.IsLetterOrDigit(c)) return false;
            }
            return true;
        }
        
        private bool ValidateRegistrationEmail(string value)
        {
            return value.Contains("@") && value.Contains(".") && value.IndexOf("@") < value.LastIndexOf(".");
        }
        
        private bool ValidateRegistrationPassword(string value)
        {
            if (value.Length < 8) return false;
            bool hasLetter = false;
            bool hasDigit = false;
            foreach (char c in value)
            {
                if (char.IsLetter(c)) hasLetter = true;
                if (char.IsDigit(c)) hasDigit = true;
            }
            return hasLetter && hasDigit;
        }
        
        #endregion
    }
}

