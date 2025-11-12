using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace OdinSamples.ValueDropdowns
{
    /*
        学习要点：
        ----------------------
        ValueDropdown 特性可以从方法获取下拉列表选项
            参数
            string methodName: 方法名称
            string dropdownTitle: 下拉框标题
            bool isUniqueList: 是否唯一
            bool expandAllMenuItems: 是否展开所有分组
            bool drawDropdownForListElements: 是否绘制下拉框
            bool disableListAddButtonBehaviour: 是否禁用列表添加按钮
            int dropdownHeight: 下拉框高度
            bool sortDropdownItems: 是否排序
            bool hideChildProperties: 是否隐藏子属性
            bool copyValues: 是否复制值
            bool onlyChangeValueOnConfirm: 是否只在确认时改变值
            bool doubleClickToConfirm: 是否双击确认
            bool flattenTreeView: 是否扁平化树视图
            int dropdownWidth: 下拉框宽度
            bool excludeExistingValuesInList: 是否排除现有值
            bool appendNextDrawer: 是否附加下一个抽屉
            bool disableGUIInAppendedDrawer: 是否禁用附加抽屉的GUI
            bool showLabel: 是否显示标签
            bool showValue: 是否显示值
            bool showTooltip: 是否显示提示
            bool showHelpBox: 是否显示帮助框
            bool showHelpBox: 是否显示帮助框
        ----------------------
    */

    /// <summary>
    /// Odin Inspector 下拉选择器演示
    /// 展示各种下拉列表和选择器特性
    /// </summary>
    public class ValueDropdownsSample : MonoBehaviour
    {
        #region ValueDropdown 基础用法
        
        [Title("基础下拉列表")]
        
        [ValueDropdown("GetWeaponNames")]
        [InfoBox("ValueDropdown 从方法获取下拉列表选项")]
        public string selectedWeapon = "长剑";
        
        [ValueDropdown("GetNumberList")]
        [InfoBox("可以返回任何类型的列表")]
        public int selectedNumber = 5;
        
        private static List<string> GetWeaponNames()
        {
            return new List<string> { "长剑", "短剑", "双手剑", "匕首", "长矛", "战斧" };
        }
        
        private static List<int> GetNumberList()
        {
            return new List<int> { 1, 5, 10, 25, 50, 100 };
        }
        
        #endregion

        #region ValueDropdown 高级用法
        
        [Title("高级下拉列表")]
        
        [ValueDropdown("GetItemDropdown")]
        [InfoBox("ValueDropdown 可以使用 ValueDropdownList 提供显示名称和值")]
        public string selectedItem = "sword_01";
        
        private static ValueDropdownList<string> GetItemDropdown()
        {
            return new ValueDropdownList<string>
            {
                { "铁剑", "sword_01" },
                { "钢剑", "sword_02" },
                { "精钢剑", "sword_03" },
                { "铁盾", "shield_01" },
                { "钢盾", "shield_02" }
            };
        }
        

        [ValueDropdown("GetLevelDropdown")]
        [InfoBox("支持分组和层级结构")]
        public int difficultyLevel = 1;
        private static ValueDropdownList<int> GetLevelDropdown()
        {
            var list = new ValueDropdownList<int>();
            list.Add("简单", 1);
            list.Add("普通", 2);
            list.Add("困难", 3);
            list.Add("专家/地狱", 4);
            list.Add("专家/末日", 5);
            return list;
        }
        
        #endregion

        #region ValueDropdown 动态列表
        
        [Title("动态下拉列表")]
        
        [InfoBox("下拉列表可以根据其他字段的值动态变化")]
        public bool includeRareItems = false;
        
        [ValueDropdown("GetDynamicItemList")]
        [InfoBox("勾选上方选项会包含稀有物品")]
        public string dynamicItem = "普通剑";
        
        private IEnumerable<string> GetDynamicItemList()
        {
            var items = new List<string> { "普通剑", "普通盾", "普通弓" };
            
            if (includeRareItems)
            {
                items.AddRange(new[] { "稀有剑", "稀有盾", "稀有弓", "传说武器" });
            }
            
            return items;
        }
        
        #endregion

        #region EnumToggleButtons 枚举切换按钮
        
        [Title("枚举切换按钮")]
        
        [EnumToggleButtons]
        [InfoBox("EnumToggleButtons 将枚举显示为切换按钮")]
        public Direction direction = Direction.North;
        
        [EnumToggleButtons]
        [InfoBox("适合选项较少的枚举")]
        public GameState gameState = GameState.Menu;
        
        #endregion

        #region EnumPaging 枚举分页
        
        [Title("枚举分页")]
        
        [EnumPaging]
        [InfoBox("EnumPaging 为枚举提供上一个/下一个按钮")]
        public Month currentMonth = Month.January;
        
        [EnumPaging]
        [InfoBox("适合需要顺序浏览的枚举")]
        public Season currentSeason = Season.Spring;
        
        #endregion

        #region ValueDropdown 自定义显示
        
        [Title("自定义显示")]
        
        [ValueDropdown("GetColorDropdown", DropdownTitle = "选择颜色")]
        [InfoBox("DropdownTitle 自定义下拉框标题")]
        public Color selectedColor = Color.white;
        
        [ValueDropdown("GetSkillDropdown", IsUniqueList = true, ExpandAllMenuItems = true)]
        [InfoBox("IsUniqueList: 确保选项唯一\nExpandAllMenuItems: 展开所有分组")]
        public string selectedSkill = "fireball";
        
        [ValueDropdown("GetEnemyDropdown", DrawDropdownForListElements = false)]
        [InfoBox("DrawDropdownForListElements: 控制列表元素是否使用下拉框")]
        public List<string> selectedEnemies = new List<string>();
        
        private static IEnumerable<ValueDropdownItem<Color>> GetColorDropdown()
        {
            yield return new ValueDropdownItem<Color>("红色", Color.red);
            yield return new ValueDropdownItem<Color>("绿色", Color.green);
            yield return new ValueDropdownItem<Color>("蓝色", Color.blue);
            yield return new ValueDropdownItem<Color>("黄色", Color.yellow);
            yield return new ValueDropdownItem<Color>("白色", Color.white);
            yield return new ValueDropdownItem<Color>("黑色", Color.black);
        }
        
        private static ValueDropdownList<string> GetSkillDropdown()
        {
            var list = new ValueDropdownList<string>();
            
            list.Add("火系/火球术", "fireball");
            list.Add("火系/流星术", "meteor");
            list.Add("火系/火墙", "firewall");
            
            list.Add("冰系/冰箭", "frostbolt");
            list.Add("冰系/暴风雪", "blizzard");
            list.Add("冰系/冰冻", "freeze");
            
            list.Add("雷系/闪电", "lightning");
            list.Add("雷系/雷暴", "thunderstorm");
            
            return list;
        }
        
        private static List<string> GetEnemyDropdown()
        {
            return new List<string> { "哥布林", "兽人", "巨魔", "恶魔", "亡灵", "龙" };
        }
        
        #endregion

        #region TypeFilter 类型过滤器
        
        [Title("类型过滤器")]
        
        [TypeFilter("GetFilteredTypeList")]
        [InfoBox("TypeFilter 提供类型选择下拉框")]
        public MonoBehaviour selectedComponentType;
        
        private IEnumerable<System.Type> GetFilteredTypeList()
        {
            var assembly = typeof(MonoBehaviour).Assembly;
            return assembly.GetTypes().Where(t => 
                t.IsSubclassOf(typeof(MonoBehaviour)) && 
                !t.IsAbstract &&
                t.Name.Contains("Transform")
            );
        }
        
        #endregion

        #region 实际应用示例
        
        [Title("实际应用示例 - 装备系统")]
        
        [BoxGroup("装备配置")]
        [ValueDropdown("GetEquipmentSlots")]
        [InfoBox("选择装备槽位")]
        public string equipmentSlot = "weapon";
        
        [BoxGroup("装备配置")]
        [ValueDropdown("GetEquipmentBySlot")]
        [InfoBox("根据槽位显示可用装备")]
        public string selectedEquipment = "";
        
        [BoxGroup("装备配置")]
        [ShowInInspector, ReadOnly]
        [InfoBox("装备信息预览")]
        public string EquipmentInfo
        {
            get
            {
                if (string.IsNullOrEmpty(selectedEquipment)) return "未选择装备";
                return GetEquipmentDescription(selectedEquipment);
            }
        }
        
        private ValueDropdownList<string> GetEquipmentSlots()
        {
            return new ValueDropdownList<string>
            {
                { "武器", "weapon" },
                { "护甲", "armor" },
                { "头盔", "helmet" },
                { "鞋子", "boots" },
                { "饰品", "accessory" }
            };
        }
        
        private IEnumerable<ValueDropdownItem<string>> GetEquipmentBySlot()
        {
            switch (equipmentSlot)
            {
                case "weapon":
                    yield return new ValueDropdownItem<string>("铁剑 (攻击+10)", "iron_sword");
                    yield return new ValueDropdownItem<string>("钢剑 (攻击+20)", "steel_sword");
                    yield return new ValueDropdownItem<string>("魔法杖 (魔法+15)", "magic_staff");
                    break;
                    
                case "armor":
                    yield return new ValueDropdownItem<string>("皮甲 (防御+5)", "leather_armor");
                    yield return new ValueDropdownItem<string>("铁甲 (防御+15)", "iron_armor");
                    yield return new ValueDropdownItem<string>("法袍 (魔法+10)", "magic_robe");
                    break;
                    
                case "helmet":
                    yield return new ValueDropdownItem<string>("铁盔 (防御+8)", "iron_helmet");
                    yield return new ValueDropdownItem<string>("魔法头冠 (魔法+12)", "magic_crown");
                    break;
                    
                case "boots":
                    yield return new ValueDropdownItem<string>("皮靴 (速度+5)", "leather_boots");
                    yield return new ValueDropdownItem<string>("飞行靴 (速度+15)", "flying_boots");
                    break;
                    
                case "accessory":
                    yield return new ValueDropdownItem<string>("力量戒指 (攻击+5)", "strength_ring");
                    yield return new ValueDropdownItem<string>("生命项链 (生命+50)", "life_amulet");
                    yield return new ValueDropdownItem<string>("魔法宝石 (魔法+20)", "magic_gem");
                    break;
            }
        }
        
        private string GetEquipmentDescription(string equipmentId)
        {
            var descriptions = new Dictionary<string, string>
            {
                { "iron_sword", "一把普通的铁剑\n攻击力: +10\n耐久度: 100" },
                { "steel_sword", "锋利的钢剑\n攻击力: +20\n暴击率: +5%\n耐久度: 150" },
                { "magic_staff", "魔法师的法杖\n魔法攻击: +15\n魔法回复: +5\n耐久度: 80" },
                { "leather_armor", "轻便的皮甲\n防御力: +5\n移动速度: +10%" },
                { "iron_armor", "坚固的铁甲\n防御力: +15\n耐久度: 200" },
                { "magic_robe", "魔法长袍\n魔法防御: +10\n魔法回复: +10" },
                { "iron_helmet", "铁制头盔\n防御力: +8" },
                { "magic_crown", "魔法王冠\n魔法攻击: +12\n魔法上限: +20" },
                { "leather_boots", "皮革靴子\n移动速度: +5%" },
                { "flying_boots", "飞行之靴\n移动速度: +15%\n跳跃高度: +50%" },
                { "strength_ring", "力量之戒\n攻击力: +5" },
                { "life_amulet", "生命护符\n最大生命: +50\n生命回复: +2/秒" },
                { "magic_gem", "魔法宝石\n魔法攻击: +20\n施法速度: +10%" }
            };
            
            return descriptions.ContainsKey(equipmentId) ? descriptions[equipmentId] : "未知装备";
        }
        
        #endregion

        #region 测试按钮
        
        [Title("测试按钮")]
        
        [Button("装备选中的物品", ButtonSizes.Large)]
        [GUIColor(0.3f, 0.8f, 0.3f)]
        private void EquipSelected()
        {
            if (string.IsNullOrEmpty(selectedEquipment))
            {
                Debug.Log("请先选择装备");
                return;
            }
            
            Debug.Log($"已装备：{selectedEquipment}\n{GetEquipmentDescription(selectedEquipment)}");
        }
        
        [Button("随机选择武器")]
        [GUIColor(0.3f, 0.5f, 0.8f)]
        private void RandomWeapon()
        {
            var weapons = GetWeaponNames();
            selectedWeapon = weapons[Random.Range(0, weapons.Count)];
            Debug.Log($"随机选择了：{selectedWeapon}");
        }
        
        [Button("打印所有技能")]
        private void PrintAllSkills()
        {
            Debug.Log("所有可用技能：");
            var skills = GetSkillDropdown();
            foreach (var skill in skills)
            {
                Debug.Log($"- {skill.Text}: {skill.Value}");
            }
        }
        
        #endregion

        #region 枚举定义
        
        public enum Direction
        {
            North,
            East,
            South,
            West
        }
        
        public enum GameState
        {
            Menu,
            Playing,
            Paused,
            GameOver
        }
        
        public enum Month
        {
            January, February, March, April, May, June,
            July, August, September, October, November, December
        }
        
        public enum Season
        {
            Spring,
            Summer,
            Autumn,
            Winter
        }
        
        #endregion
    }
}

