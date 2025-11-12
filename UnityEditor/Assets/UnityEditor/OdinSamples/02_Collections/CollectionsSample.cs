using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OdinSamples.Collections
{
    /*
        学习要点：
        ----------------------
        ListDrawerSettings 列表绘制设置 用于绘制列表
            参数
            bool ShowIndexLabels: 是否显示索引
            string ListElementLabelName: 使用元素的指定字段作为标签
            bool DraggableItems: 可拖拽排序
            bool ShowPaging: 启用分页
            int NumberOfItemsPerPage: 每页显示数量
        TableList 表格列表 用于绘制表格列表
            参数
            bool ShowIndexLabels: 是否显示索引
            bool AlwaysExpanded: 是否始终展开
            bool ShowPaging: 启用分页
            int NumberOfItemsPerPage: 每页显示数量
        InlineEditor 内联编辑器 用于内联编辑器
            参数
            InlineEditorModes mode: 内联编辑器模式
        AssetsOnly / SceneObjectsOnly 对象类型限制 用于限制对象类型
            参数
            bool AssetsOnly: 只能选择项目资源
            bool SceneObjectsOnly: 只能选择场景对象
        PreviewField 预览字段 用于显示预览字段
            参数
            int height: 预览高度
            ObjectFieldAlignment alignment: 预览对齐方式
            bool showLabel: 是否显示标签
        Searchable 可搜索列表 用于搜索列表
            参数
            SearchFilterOptions filterOptions: 搜索过滤选项
        DictionaryDrawerSettings 字典显示 用于显示字典
            参数
            string KeyLabel: 键标签
            string ValueLabel: 值标签
            DictionaryDisplayOptions displayMode: 显示模式
        ----------------------
    */
    /// <summary>
    /// Odin Inspector 集合和列表演示
    /// 展示列表、数组等集合类型的高级显示方式
    /// </summary>
    public class CollectionsSample : MonoBehaviour
    {
        #region 基础列表

        [Title("基础列表")]
        [InfoBox("普通的 Unity 列表显示")]
        public List<string> normalList = new List<string> { "项目1", "项目2", "项目3" };

        #endregion

        #region ListDrawerSettings 列表绘制设置

        [Title("列表绘制设置")]

        [ListDrawerSettings(
            ShowIndexLabels = true,
            ListElementLabelName = "name",
            DraggableItems = true,
            ShowPaging = false,
            NumberOfItemsPerPage = 3)]  // 设置每页显示数量
        [InfoBox("ShowIndexLabels: 显示索引\nListElementLabelName: 使用元素的指定字段作为标签\nDraggableItems: 可拖拽排序")]
        public List<ItemData> itemList = new List<ItemData>
        {
            new ItemData { name = "生命药水", value = 50 },
            new ItemData { name = "魔法药水", value = 30 },
            new ItemData { name = "力量卷轴", value = 100 },
            new ItemData { name = "力量卷轴4", value = 100 },
            new ItemData { name = "力量卷轴5", value = 100 },
            new ItemData { name = "力量卷轴6", value = 100 },

        };

        [ListDrawerSettings(
            Expanded = true,
            ShowPaging = true,
            NumberOfItemsPerPage = 3,
            DraggableItems = false)]
        [InfoBox("ShowPaging: 启用分页\nNumberOfItemsPerPage: 每页显示数量")]
        public List<string> pagedList = new List<string>();

        [ListDrawerSettings(
            HideAddButton = false,
            HideRemoveButton = false,
            AlwaysAddDefaultValue = true,
            ShowItemCount = true)]
        [InfoBox("HideAddButton/HideRemoveButton: 隐藏添加/删除按钮\nShowItemCount: 显示元素数量")]
        public List<int> numberList = new List<int> { 1, 2, 3, 4, 5 };

        #endregion

        #region TableList 表格列表

        [Title("表格列表")]

        [TableList(ShowIndexLabels = true, AlwaysExpanded = true)]
        [InfoBox("TableList 以表格形式显示列表，更加紧凑直观")]
        public List<CharacterData> characters = new List<CharacterData>
        {
            new CharacterData { name = "战士", level = 10, health = 100, attack = 50 },
            new CharacterData { name = "法师", level = 8, health = 60, attack = 80 },
            new CharacterData { name = "刺客", level = 12, health = 70, attack = 90 }
        };

        [TableList(ShowPaging = true, NumberOfItemsPerPage = 5)]
        [InfoBox("表格列表也支持分页显示")]
        public List<EnemyData> enemies = new List<EnemyData>();

        #endregion

        #region InlineEditor 内联编辑器

        [Title("内联编辑器")]

        [InlineEditor(InlineEditorModes.FullEditor)]
        [InfoBox("InlineEditor 可以在 Inspector 中直接编辑 ScriptableObject 或预制体")]
        public GameObject prefabExample;

        [InlineEditor(InlineEditorModes.SmallPreview)]
        [InfoBox("SmallPreview 模式显示小预览图")]
        public Material materialExample;

        [InlineEditor(InlineEditorModes.LargePreview)]
        [InfoBox("LargePreview 模式显示大预览图")]
        public Texture2D textureExample;

        #endregion

        #region AssetsOnly / SceneObjectsOnly

        [Title("对象类型限制")]

        [AssetsOnly]
        [InfoBox("AssetsOnly 只能选择项目资源，不能选择场景对象")]
        public GameObject assetOnlyObject;

        [SceneObjectsOnly]
        [InfoBox("SceneObjectsOnly 只能选择场景中的对象，不能选择项目资源")]
        public GameObject sceneOnlyObject;

        [AssetsOnly]
        [PreviewField(100, ObjectFieldAlignment.Center)]
        [InfoBox("结合 PreviewField 显示资源预览")]
        public Sprite spriteAsset;

        #endregion

        #region PreviewField 预览字段

        [Title("预览字段")]

        [PreviewField(50)]
        [InfoBox("PreviewField 显示图片预览，参数是预览高度")]
        public Sprite smallPreview;

        [PreviewField(100, ObjectFieldAlignment.Left)]
        public Sprite mediumPreviewLeft;

        [PreviewField(150, ObjectFieldAlignment.Center)]
        public Sprite largePreviewCenter;

        [PreviewField(100, ObjectFieldAlignment.Right)]
        public Texture2D texturePreview;

        [PreviewField(80)]
        [HorizontalGroup("Preview")]
        public Sprite sprite1;

        [PreviewField(80)]
        [HorizontalGroup("Preview")]
        public Sprite sprite2;

        [PreviewField(80)]
        [HorizontalGroup("Preview")]
        public Sprite sprite3;

        #endregion

        #region 可搜索列表

        [Title("可搜索列表")]

        [Searchable]
        [InfoBox("Searchable 让列表支持搜索功能")]
        public List<string> searchableList = new List<string>
        {
            "苹果", "香蕉", "橙子", "葡萄", "西瓜",
            "草莓", "樱桃", "蓝莓", "芒果", "菠萝"
        };

        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        [TableList]
        public List<SearchableItem> searchableItems = new List<SearchableItem>
        {
            new SearchableItem { name = "剑", category = "武器", rarity = "稀有" },
            new SearchableItem { name = "盾", category = "防具", rarity = "普通" },
            new SearchableItem { name = "药水", category = "消耗品", rarity = "普通" }
        };

        #endregion

        #region 字典显示

        [Title("字典显示")]

        [DictionaryDrawerSettings(KeyLabel = "物品名称", ValueLabel = "数量")]
        [InfoBox("字典可以自定义键和值的标签名称")]
        public Dictionary<string, int> inventory = new Dictionary<string, int>
        {
            { "生命药水", 5 },
            { "魔法药水", 3 },
            { "金币", 1000 }
        };

        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
        [InfoBox("DisplayMode 控制字典的显示方式：Foldout（折叠）或 OneLine（单行）")]
        public Dictionary<ItemType, ItemData> itemDatabase = new Dictionary<ItemType, ItemData>();

        #endregion

        #region 测试按钮

        [Title("测试按钮")]

        [ButtonGroup("Buttons")]
        [Button("添加随机物品")]
        private void AddRandomItem()
        {
            string[] items = { "生命药水", "魔法药水", "力量卷轴", "防御护符", "速度之靴" };
            string randomItem = items[Random.Range(0, items.Length)];
            int randomValue = Random.Range(10, 100);

            itemList.Add(new ItemData { name = randomItem, value = randomValue });
            Debug.Log($"添加了物品：{randomItem}，价值：{randomValue}");
        }

        [ButtonGroup("Buttons")]
        [Button("添加随机角色")]
        private void AddRandomCharacter()
        {
            string[] names = { "战士", "法师", "刺客", "游侠", "牧师", "德鲁伊" };
            characters.Add(new CharacterData
            {
                name = names[Random.Range(0, names.Length)],
                level = Random.Range(1, 20),
                health = Random.Range(50, 150),
                attack = Random.Range(30, 100)
            });
        }

        [ButtonGroup("Buttons")]
        [Button("清空所有列表", ButtonSizes.Large)]
        [GUIColor(0.8f, 0.3f, 0.3f)]
        private void ClearAllLists()
        {
            itemList.Clear();
            characters.Clear();
            enemies.Clear();
            pagedList.Clear();
            numberList.Clear();
            searchableList.Clear();
            inventory.Clear();
            Debug.Log("所有列表已清空");
        }

        #endregion

        #region 内部类定义

        [System.Serializable]
        public class ItemData
        {
            [LabelText("名称")]
            public string name;

            [LabelText("价值")]
            public int value;
        }

        [System.Serializable]
        public class CharacterData
        {
            [TableColumnWidth(100)]
            [LabelText("名称")]
            public string name;

            [TableColumnWidth(60)]
            [LabelText("等级")]
            public int level;

            [TableColumnWidth(80)]
            [LabelText("生命值")]
            [ProgressBar(0, 200, ColorGetter = "GetHealthColor")]
            public float health;

            [TableColumnWidth(80)]
            [LabelText("攻击力")]
            public int attack;

            private Color GetHealthColor(float value)
            {
                return Color.Lerp(Color.red, Color.green, value / 200f);
            }
        }

        [System.Serializable]
        public class EnemyData
        {
            public string name;
            public int level;
            public float health;
        }

        [System.Serializable]
        public class SearchableItem : ISearchFilterable
        {
            [TableColumnWidth(100)]
            public string name;

            [TableColumnWidth(80)]
            public string category;

            [TableColumnWidth(80)]
            public string rarity;

            public bool IsMatch(string searchString)
            {
                return name.Contains(searchString) ||
                       category.Contains(searchString) ||
                       rarity.Contains(searchString);
            }
        }

        public enum ItemType
        {
            Weapon,
            Armor,
            Consumable,
            Material
        }

        #endregion
    }
}

