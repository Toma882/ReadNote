using Sirenix.OdinInspector;
using UnityEngine;

namespace OdinSamples.Conditional
{

    /*
        学习要点：
        ----------------------
        ShowIf 特性可以根据条件显示字段
            参数
            string condition: 条件 使用@符号编写C#表达式
        HideIf 特性可以根据条件隐藏字段
            参数
            string condition: 条件 使用@符号编写C#表达式
        ----------------------
        EnableIf 特性可以根据条件启用字段
            参数
            string condition: 条件 使用@符号编写C#表达式
        DisableIf 特性可以根据条件禁用字段
            参数
            string condition: 条件 使用@符号编写C#表达式
        ----------------------
        DisableInPlayMode 特性可以在运行时禁用字段
            参数
            bool disableInPlayMode: 是否禁用
        DisableInEditorMode 特性可以在编辑器模式禁用字段
            参数
            bool disableInEditorMode: 是否禁用
     */
       

    /// <summary>
    /// Odin Inspector 条件显示演示
    /// 展示如何根据条件动态显示/隐藏字段
    /// </summary>
    public class ConditionalSample : MonoBehaviour
    {
        #region ShowIf / HideIf 基础用法
        
        [Title("基础条件显示")]
        
        [InfoBox("ShowIf 和 HideIf 根据条件显示或隐藏字段")]
        public bool showAdvancedOptions = false;
        
        [ShowIf("showAdvancedOptions")]
        [InfoBox("只有当 showAdvancedOptions 为 true 时才显示")]
        public string advancedOption1 = "高级选项1";
        
        [ShowIf("showAdvancedOptions")]
        public int advancedOption2 = 100;
        
        [HideIf("showAdvancedOptions")]
        [InfoBox("只有当 showAdvancedOptions 为 false 时才显示")]
        public string basicOption = "基础选项";
        
        #endregion

        #region 多条件判断
        
        [Title("多条件判断")]
        
        public bool condition1 = false;
        public bool condition2 = false;
        
        [ShowIf("condition1")]
        [InfoBox("条件1为true时显示")]
        public string field1 = "字段1";
        
        [ShowIf("@condition1 && condition2")]
        [InfoBox("条件1和条件2都为true时显示（使用@符号编写C#表达式）")]
        public string field2 = "字段2";
        
        [ShowIf("@condition1 || condition2")]
        [InfoBox("条件1或条件2为true时显示")]
        public string field3 = "字段3";
        
        [ShowIf("@!condition1")]
        [InfoBox("条件1为false时显示（使用!取反）")]
        public string field4 = "字段4";
        
        #endregion

        #region 枚举条件
        
        [Title("枚举条件")]
        
        public WeaponType weaponType = WeaponType.Sword;
        
        [ShowIf("weaponType", WeaponType.Sword)]
        [InfoBox("只有选择剑时显示")]
        public float swordDamage = 50f;
        
        [ShowIf("weaponType", WeaponType.Bow)]
        [InfoBox("只有选择弓时显示")]
        public float arrowSpeed = 20f;
        
        [ShowIf("weaponType", WeaponType.Magic)]
        [InfoBox("只有选择魔法时显示")]
        public int manaCost = 30;
        
        [ShowIf("@weaponType == WeaponType.Sword || weaponType == WeaponType.Bow")]
        [InfoBox("剑或弓时显示")]
        public float physicalDamage = 100f;
        
        #endregion

        #region 数值条件
        
        [Title("数值条件")]
        
        [Range(0, 100)]
        public int healthPercentage = 100;
        
        [ShowIf("@healthPercentage < 50")]
        [InfoBox("生命值低于50%时显示警告", InfoMessageType.Warning)]
        public string lowHealthWarning = "生命值过低！";
        
        [ShowIf("@healthPercentage < 25")]
        [InfoBox("生命值低于25%时显示危险警告", InfoMessageType.Error)]
        public string criticalHealthWarning = "生命值危险！";
        
        [Range(1, 100)]
        public int playerLevel = 1;
        
        [ShowIf("@playerLevel >= 10")]
        [InfoBox("等级达到10级解锁")]
        public GameObject skill1;
        
        [ShowIf("@playerLevel >= 20")]
        [InfoBox("等级达到20级解锁")]
        public GameObject skill2;
        
        [ShowIf("@playerLevel >= 30")]
        [InfoBox("等级达到30级解锁")]
        public GameObject skill3;
        
        #endregion

        #region 方法条件
        
        [Title("方法条件")]
        
        public string username = "";
        public string password = "";
        
        [ShowIf("IsLoginValid")]
        [InfoBox("用户名和密码都不为空时显示")]
        [Button("登录", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void Login()
        {
            Debug.Log($"登录：用户名={username}");
        }
        
        [ShowIf("IsLoginInvalid")]
        [InfoBox("请输入用户名和密码", InfoMessageType.Warning)]
        public string loginHint = "登录提示";
        
        private bool IsLoginValid()
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }
        
        private bool IsLoginInvalid()
        {
            return string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password);
        }
        
        #endregion

        #region EnableIf / DisableIf
        
        [Title("条件启用/禁用")]
        
        public bool allowEditing = false;
        
        [EnableIf("allowEditing")]
        [InfoBox("启用编辑时可以修改")]
        public string editableField = "可编辑字段";
        
        [DisableIf("allowEditing")]
        [InfoBox("禁用编辑时不可修改")]
        public string nonEditableField = "不可编辑字段";
        
        [EnableIf("@playerLevel >= 10")]
        [InfoBox("达到10级才能使用")]
        public bool useSpecialAbility = false;
        
        #endregion

        #region DisableInPlayMode / DisableInEditorMode
        
        [Title("运行模式条件")]
        
        [DisableInPlayMode]
        [InfoBox("只能在编辑器模式修改")]
        public int editorOnlyValue = 100;
        
        [DisableInEditorMode]
        [InfoBox("只能在运行时修改")]
        public float runtimeValue = 0f;
        
        [DisableInPlayMode]
        [Button("编辑器模式按钮")]
        private void EditorModeButton()
        {
            Debug.Log("这个按钮只能在编辑器模式点击");
        }
        
        [DisableInEditorMode]
        [Button("运行时按钮")]
        private void RuntimeButton()
        {
            Debug.Log("这个按钮只能在运行时点击");
        }
        
        #endregion

        #region 复杂条件示例
        
        [Title("复杂条件示例")]
        
        [BoxGroup("角色配置")]
        public CharacterClass characterClass = CharacterClass.Warrior;
        
        [BoxGroup("角色配置")]
        [Range(1, 100)]
        public int characterLevel = 1;
        
        [BoxGroup("角色配置")]
        public bool hasWeapon = false;
        
        // 战士属性
        [BoxGroup("战士属性")]
        [ShowIf("@characterClass == CharacterClass.Warrior")]
        [InfoBox("战士专属属性")]
        [Range(0, 100)]
        public int strength = 50;
        
        [BoxGroup("战士属性")]
        [ShowIf("@characterClass == CharacterClass.Warrior && characterLevel >= 10")]
        [InfoBox("10级战士解锁")]
        public bool berserkMode = false;
        
        // 法师属性
        [BoxGroup("法师属性")]
        [ShowIf("@characterClass == CharacterClass.Mage")]
        [InfoBox("法师专属属性")]
        [Range(0, 100)]
        public int intelligence = 50;
        
        [BoxGroup("法师属性")]
        [ShowIf("@characterClass == CharacterClass.Mage && characterLevel >= 10")]
        [InfoBox("10级法师解锁")]
        public bool arcaneBlast = false;
        
        // 刺客属性
        [BoxGroup("刺客属性")]
        [ShowIf("@characterClass == CharacterClass.Rogue")]
        [InfoBox("刺客专属属性")]
        [Range(0, 100)]
        public int agility = 50;
        
        [BoxGroup("刺客属性")]
        [ShowIf("@characterClass == CharacterClass.Rogue && characterLevel >= 10")]
        [InfoBox("10级刺客解锁")]
        public bool shadowStep = false;
        
        // 武器系统
        [ShowIf("hasWeapon")]
        [BoxGroup("武器系统")]
        [InfoBox("装备武器后显示")]
        public GameObject equippedWeapon;
        
        [ShowIf("@hasWeapon && characterClass == CharacterClass.Warrior")]
        [BoxGroup("武器系统")]
        [InfoBox("战士武器加成")]
        public float weaponDamageBonus = 1.2f;
        
        [ShowIf("@hasWeapon && characterClass == CharacterClass.Mage")]
        [BoxGroup("武器系统")]
        [InfoBox("法师武器加成")]
        public float spellPowerBonus = 1.5f;
        
        [ShowIf("@hasWeapon && characterClass == CharacterClass.Rogue")]
        [BoxGroup("武器系统")]
        [InfoBox("刺客武器加成")]
        public float criticalBonus = 1.8f;
        
        #endregion

        #region 动态提示
        
        [Title("动态提示")]
        
        [Range(0, 100)]
        public float health = 100f;
        
        [Range(0, 100)]
        public float mana = 50f;
        
        [ShowIf("@health < 30")]
        [InfoBox("警告：生命值过低！建议使用治疗药水", InfoMessageType.Warning)]
        public bool showHealthWarning = true;
        
        [ShowIf("@mana < 20")]
        [InfoBox("警告：魔法值不足！建议使用魔法药水", InfoMessageType.Warning)]
        public bool showManaWarning = true;
        
        [ShowIf("@health < 30 && mana < 20")]
        [InfoBox("危险：生命值和魔法值都过低！", InfoMessageType.Error)]
        public bool showCriticalWarning = true;
        
        #endregion

        #region 测试按钮
        
        [Title("测试按钮")]
        
        [ButtonGroup("测试")]
        [Button("增加等级")]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void IncreaseLevel()
        {
            playerLevel = Mathf.Min(playerLevel + 5, 100);
            Debug.Log($"等级提升到：{playerLevel}");
        }
        
        [ButtonGroup("测试")]
        [Button("减少生命值")]
        [GUIColor(0.8f, 0.5f, 0.3f)]
        private void DecreaseHealth()
        {
            health = Mathf.Max(health - 20f, 0f);
            Debug.Log($"生命值：{health}");
        }
        
        [ButtonGroup("测试")]
        [Button("切换职业")]
        [GUIColor(0.3f, 0.5f, 0.8f)]
        private void SwitchClass()
        {
            characterClass = (CharacterClass)(((int)characterClass + 1) % 3);
            Debug.Log($"职业切换为：{characterClass}");
        }
        
        [ButtonGroup("测试")]
        [Button("重置")]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void Reset()
        {
            playerLevel = 1;
            health = 100f;
            mana = 50f;
            characterLevel = 1;
            Debug.Log("已重置所有值");
        }
        
        #endregion

        #region 枚举定义
        
        public enum WeaponType
        {
            Sword,
            Bow,
            Magic
        }
        
        public enum CharacterClass
        {
            Warrior,
            Mage,
            Rogue
        }
        
        #endregion
    }
}

