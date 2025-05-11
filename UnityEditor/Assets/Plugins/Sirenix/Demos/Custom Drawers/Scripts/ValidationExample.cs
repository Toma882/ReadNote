#if UNITY_EDITOR
[assembly: Sirenix.OdinInspector.Editor.Validation.RegisterValidator(typeof(Sirenix.OdinInspector.Demos.NotOneAttributeValidator))]

namespace Sirenix.OdinInspector.Demos
{
    using System;
    using UnityEngine;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor.Validation;

#endif

    // 此示例演示如何实现验证器以验证属性，
    // 以及如何添加将被Odin项目验证器捕获的警告或错误。
    [TypeInfoBox(
       "此示例演示了如何实现自定义验证器，用于验证属性的值，" +
       "以及如何让Odin项目验证器（如果已安装）捕获该验证警告或错误。")]
    public class ValidationExample : MonoBehaviour
    {
        [NotOne]
        public int NotOne;
    }

    // NotOneAttributeDrawer使用的属性
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotOneAttribute : Attribute
    {
    }

#if UNITY_EDITOR

    // 将验证器脚本文件放在Editor文件夹中，并记得在此文件的顶部包含注册属性。
    public class NotOneAttributeValidator : AttributeValidator<NotOneAttribute, int>
    {
        protected override void Validate(ValidationResult result)
        {
            if (this.ValueEntry.SmartValue == 1)
            {
                result.Message = "1不是有效值。";
                result.ResultType = ValidationResultType.Error;
            }
        }
    }

#endif
}
#endif
