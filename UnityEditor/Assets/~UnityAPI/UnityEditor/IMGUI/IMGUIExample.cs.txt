using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;

namespace UnityEditor.IMGUI.Examples
{
    /// <summary>
    /// UnityEditor.IMGUI 命名空间使用示例
    /// 演示即时模式GUI系统的各种控件和功能
    /// </summary>
    public class IMGUIExample : MonoBehaviour
    {
        [Header("GUI配置")]
        [SerializeField] private bool showAdvancedControls = false;
        [SerializeField] private float windowWidth = 400f;
        [SerializeField] private float windowHeight = 600f;
        [SerializeField] private Vector2 scrollPosition = Vector2.zero;
        
        [Header("控件状态")]
        [SerializeField] private string textFieldValue = "";
        [SerializeField] private bool toggleValue = false;
        [SerializeField] private float sliderValue = 0.5f;
        [SerializeField] private int selectedTab = 0;
        [SerializeField] private Vector2 dragValue = Vector2.zero;
        
        [Header("树形视图")]
        [SerializeField] private TreeViewState treeViewState;
        [SerializeField] private TreeView treeView;
        [SerializeField] private List<TreeViewItem> treeItems = new List<TreeViewItem>();
        
        [Header("表格视图")]
        [SerializeField] private MultiColumnHeaderState multiColumnHeaderState;
        [SerializeField] private MultiColumnHeader multiColumnHeader;
        [SerializeField] private List<TableRow> tableData = new List<TableRow>();
        
        private Rect windowRect = new Rect(100, 100, 400, 600);
        private bool isWindowOpen = true;
        private string[] tabNames = { "基础控件", "高级控件", "树形视图", "表格视图" };
        
        /// <summary>
        /// 表格行数据
        /// </summary>
        [System.Serializable]
        public class TableRow
        {
            public string name;
            public int value;
            public bool enabled;
            
            public TableRow(string name, int value, bool enabled)
            {
                this.name = name;
                this.value = value;
                this.enabled = enabled;
            }
        }
        
        /// <summary>
        /// 初始化IMGUI系统
        /// </summary>
        private void Start()
        {
            InitializeIMGUISystem();
        }
        
        /// <summary>
        /// 初始化IMGUI系统
        /// </summary>
        private void InitializeIMGUISystem()
        {
            // 初始化树形视图
            InitializeTreeView();
            
            // 初始化表格视图
            InitializeTableView();
            
            Debug.Log("IMGUI系统初始化完成");
        }
        
        /// <summary>
        /// 初始化树形视图
        /// </summary>
        private void InitializeTreeView()
        {
            treeViewState = new TreeViewState();
            
            // 创建示例树形数据
            treeItems.Clear();
            treeItems.Add(new TreeViewItem(1, 0, "根节点"));
            treeItems.Add(new TreeViewItem(2, 1, "子节点 1"));
            treeItems.Add(new TreeViewItem(3, 1, "子节点 2"));
            treeItems.Add(new TreeViewItem(4, 2, "孙节点 1"));
            treeItems.Add(new TreeViewItem(5, 2, "孙节点 2"));
            
            treeView = new TreeView(treeViewState);
            treeView.SetupParentsAndChildrenFromDepths(treeItems);
        }
        
        /// <summary>
        /// 初始化表格视图
        /// </summary>
        private void InitializeTableView()
        {
            // 创建表格数据
            tableData.Clear();
            tableData.Add(new TableRow("项目1", 100, true));
            tableData.Add(new TableRow("项目2", 200, false));
            tableData.Add(new TableRow("项目3", 300, true));
            
            // 创建多列表头
            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("名称"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 100,
                    minWidth = 50,
                    autoResize = true,
                    allowToggleVisibility = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("数值"),
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 80,
                    minWidth = 50,
                    autoResize = true,
                    allowToggleVisibility = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("启用"),
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 60,
                    minWidth = 50,
                    autoResize = true,
                    allowToggleVisibility = true
                }
            };
            
            multiColumnHeaderState = new MultiColumnHeaderState(columns);
            multiColumnHeader = new MultiColumnHeader(multiColumnHeaderState);
            multiColumnHeader.ResizeToFit();
        }
        
        /// <summary>
        /// 绘制基础控件
        /// </summary>
        private void DrawBasicControls()
        {
            GUILayout.Label("基础控件演示", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            // 文本字段
            textFieldValue = EditorGUILayout.TextField("文本字段", textFieldValue);
            
            // 按钮
            if (GUILayout.Button("点击按钮"))
            {
                Debug.Log("按钮被点击");
            }
            
            // 切换开关
            toggleValue = EditorGUILayout.Toggle("切换开关", toggleValue);
            
            // 滑块
            sliderValue = EditorGUILayout.Slider("滑块", sliderValue, 0f, 1f);
            
            // 颜色选择器
            Color color = EditorGUILayout.ColorField("颜色选择器", Color.red);
            
            // 对象字段
            GameObject obj = (GameObject)EditorGUILayout.ObjectField("对象字段", null, typeof(GameObject), true);
            
            // 枚举下拉菜单
            System.Enum enumValue = EditorGUILayout.EnumPopup("枚举选择", System.Enum.GetValues(typeof(KeyCode))[0]);
            
            // 向量字段
            Vector3 vector3 = EditorGUILayout.Vector3Field("向量3", Vector3.zero);
            
            // 矩形字段
            Rect rect = EditorGUILayout.RectField("矩形", new Rect(0, 0, 100, 100));
        }
        
        /// <summary>
        /// 绘制高级控件
        /// </summary>
        private void DrawAdvancedControls()
        {
            GUILayout.Label("高级控件演示", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            // 标签页
            selectedTab = GUILayout.Toolbar(selectedTab, tabNames);
            
            // 折叠面板
            showAdvancedControls = EditorGUILayout.Foldout(showAdvancedControls, "高级选项");
            if (showAdvancedControls)
            {
                EditorGUI.indentLevel++;
                
                // 曲线编辑器
                AnimationCurve curve = EditorGUILayout.CurveField("曲线", AnimationCurve.Linear(0, 0, 1, 1));
                
                // 渐变编辑器
                Gradient gradient = EditorGUILayout.GradientField("渐变", new Gradient());
                
                // 范围滑块
                Vector2 range = EditorGUILayout.Vector2Field("范围", new Vector2(0, 1));
                
                // 拖拽字段
                dragValue = EditorGUILayout.Vector2Field("拖拽值", dragValue);
                
                EditorGUI.indentLevel--;
            }
            
            // 进度条
            float progress = Mathf.PingPong(Time.time * 0.5f, 1f);
            EditorGUI.ProgressBar(new Rect(10, 200, 200, 20), progress, $"进度: {progress:P0}");
            
            // 帮助框
            EditorGUILayout.HelpBox("这是一个帮助信息框，用于显示提示信息。", MessageType.Info);
            
            // 分隔线
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
        }
        
        /// <summary>
        /// 绘制树形视图
        /// </summary>
        private void DrawTreeView()
        {
            GUILayout.Label("树形视图演示", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            if (treeView != null)
            {
                Rect treeRect = GUILayoutUtility.GetRect(200, 300);
                treeView.OnGUI(treeRect);
                
                GUILayout.Space(10);
                
                if (GUILayout.Button("添加节点"))
                {
                    AddTreeItem();
                }
                
                if (GUILayout.Button("删除选中节点"))
                {
                    RemoveSelectedTreeItem();
                }
            }
        }
        
        /// <summary>
        /// 绘制表格视图
        /// </summary>
        private void DrawTableView()
        {
            GUILayout.Label("表格视图演示", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            if (multiColumnHeader != null)
            {
                Rect tableRect = GUILayoutUtility.GetRect(300, 200);
                multiColumnHeader.OnGUI(tableRect, 0);
                
                // 绘制表格数据
                float yOffset = multiColumnHeader.height;
                for (int i = 0; i < tableData.Count; i++)
                {
                    Rect rowRect = new Rect(tableRect.x, tableRect.y + yOffset + i * 20, tableRect.width, 20);
                    
                    // 名称列
                    Rect nameRect = new Rect(rowRect.x, rowRect.y, 100, rowRect.height);
                    tableData[i].name = EditorGUI.TextField(nameRect, tableData[i].name);
                    
                    // 数值列
                    Rect valueRect = new Rect(rowRect.x + 100, rowRect.y, 80, rowRect.height);
                    tableData[i].value = EditorGUI.IntField(valueRect, tableData[i].value);
                    
                    // 启用列
                    Rect enabledRect = new Rect(rowRect.x + 180, rowRect.y, 60, rowRect.height);
                    tableData[i].enabled = EditorGUI.Toggle(enabledRect, tableData[i].enabled);
                }
                
                GUILayout.Space(220);
                
                if (GUILayout.Button("添加行"))
                {
                    AddTableRow();
                }
                
                if (GUILayout.Button("删除最后一行"))
                {
                    RemoveLastTableRow();
                }
            }
        }
        
        /// <summary>
        /// 添加树形节点
        /// </summary>
        private void AddTreeItem()
        {
            int newId = treeItems.Count + 1;
            treeItems.Add(new TreeViewItem(newId, 0, $"新节点 {newId}"));
            treeView.SetupParentsAndChildrenFromDepths(treeItems);
        }
        
        /// <summary>
        /// 删除选中的树形节点
        /// </summary>
        private void RemoveSelectedTreeItem()
        {
            if (treeViewState.selectedIDs.Count > 0)
            {
                int selectedId = treeViewState.selectedIDs[0];
                treeItems.RemoveAll(item => item.id == selectedId);
                treeView.SetupParentsAndChildrenFromDepths(treeItems);
            }
        }
        
        /// <summary>
        /// 添加表格行
        /// </summary>
        private void AddTableRow()
        {
            tableData.Add(new TableRow($"项目{tableData.Count + 1}", Random.Range(1, 1000), Random.value > 0.5f));
        }
        
        /// <summary>
        /// 删除最后一行表格
        /// </summary>
        private void RemoveLastTableRow()
        {
            if (tableData.Count > 0)
            {
                tableData.RemoveAt(tableData.Count - 1);
            }
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            // 绘制可拖拽窗口
            windowRect = GUI.Window(0, windowRect, DrawWindow, "IMGUI 示例窗口");
        }
        
        /// <summary>
        /// 绘制窗口内容
        /// </summary>
        private void DrawWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            
            // 标签页
            selectedTab = GUILayout.Toolbar(selectedTab, tabNames);
            
            GUILayout.Space(10);
            
            // 根据选中的标签页显示不同内容
            switch (selectedTab)
            {
                case 0:
                    DrawBasicControls();
                    break;
                case 1:
                    DrawAdvancedControls();
                    break;
                case 2:
                    DrawTreeView();
                    break;
                case 3:
                    DrawTableView();
                    break;
            }
            
            GUILayout.EndScrollView();
            
            // 允许窗口拖拽
            GUI.DragWindow();
        }
        
        /// <summary>
        /// 清理资源
        /// </summary>
        private void OnDestroy()
        {
            if (treeView != null)
            {
                treeView = null;
            }
            
            if (multiColumnHeader != null)
            {
                multiColumnHeader = null;
            }
        }
    }
} 