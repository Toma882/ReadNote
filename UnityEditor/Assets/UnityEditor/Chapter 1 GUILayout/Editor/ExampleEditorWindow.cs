using UnityEngine;
using UnityEditor;

public class ExampleEditorWindow : EditorWindow
{
    [MenuItem("Example/Open Example Editor Window")]
    public static void Open()
    {
        GetWindow<ExampleEditorWindow>().Show();
    }

    private Vector2 scrollPosition;
    private const float splitterWidth = 2f;
    private float splitterPos;
    private Rect splitterRect;
    private bool isDragging;

    private void OnEnable()
    {
        splitterPos = position.width * .3f;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical(GUILayout.Width(splitterPos));
            GUILayout.Box("左侧区域", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            GUILayout.BeginVertical(GUILayout.Width(splitterPos));
            {
                // 显示标题文本
                GUILayout.Label("左侧面板", EditorStyles.boldLabel);
                
                // 使用ScrollView包裹内容，允许内容超出视图时滚动
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                {
                    // 创建一个文本输入框，用于输入字符串
                    EditorGUILayout.TextField("名称", "输入文本", GUILayout.ExpandWidth(true));
                    
                    // 创建一个数值输入框，用于输入整数
                    EditorGUILayout.IntField("数量", 0, GUILayout.ExpandWidth(true));
                    
                    // 创建一个浮点数输入框，用于输入小数
                    EditorGUILayout.FloatField("比例", 1.0f, GUILayout.ExpandWidth(true));
                    
                    // 创建一个开关按钮，用于切换布尔值
                    EditorGUILayout.Toggle("启用", false, GUILayout.ExpandWidth(true));
                    
                    // 创建一个下拉选择框
                    EditorGUILayout.Popup("选项", 0, new string[] { "选项1", "选项2", "选项3" });
                    
                    // 创建一个颜色选择器
                    EditorGUILayout.ColorField("颜色", Color.white);
                    
                    // 创建一个对象选择框
                    EditorGUILayout.ObjectField("对象", null, typeof(Object), true);
                    
                    // 创建一个可折叠的组
                    EditorGUILayout.Foldout(true, "折叠组");
                    
                    // 创建一个分隔线
                    EditorGUILayout.Space(10);
                    
                    // 创建一个按钮
                    if (GUILayout.Button("确认", GUILayout.Height(30)))
                    {
                       // Debug.Log("按钮被点击");
                    }
                }
    GUILayout.EndScrollView();
}
GUILayout.EndVertical();GUILayout.BeginVertical(GUILayout.Width(splitterPos));
{
    // 显示标题文本
    GUILayout.Label("左侧面板", EditorStyles.boldLabel);
    
    // 使用ScrollView包裹内容，允许内容超出视图时滚动
    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
    {
        // 创建一个文本输入框，用于输入字符串
        EditorGUILayout.TextField("名称", "输入文本", GUILayout.ExpandWidth(true));
        
        // 创建一个数值输入框，用于输入整数
        EditorGUILayout.IntField("数量", 0, GUILayout.ExpandWidth(true));
        
        // 创建一个浮点数输入框，用于输入小数
        EditorGUILayout.FloatField("比例", 1.0f, GUILayout.ExpandWidth(true));
        
        // 创建一个开关按钮，用于切换布尔值
        EditorGUILayout.Toggle("启用", false, GUILayout.ExpandWidth(true));
        
        // 创建一个下拉选择框
        EditorGUILayout.Popup("选项", 0, new string[] { "选项1", "选项2", "选项3" });
        
        // 创建一个颜色选择器
        EditorGUILayout.ColorField("颜色", Color.white);
        
        // 创建一个对象选择框
        EditorGUILayout.ObjectField("对象", null, typeof(Object), true);
        
        // 创建一个可折叠的组
        EditorGUILayout.Foldout(true, "折叠组");
        
        // 创建一个分隔线
        EditorGUILayout.Space(10);
        
        // 创建一个按钮
        if (GUILayout.Button("确认", GUILayout.Height(30)))
        {
            Debug.Log("按钮被点击");
        }
    }
    GUILayout.EndScrollView();
}
GUILayout.EndVertical();GUILayout.BeginVertical(GUILayout.Width(splitterPos));
{
    // 显示标题文本
    GUILayout.Label("左侧面板", EditorStyles.boldLabel);
    
    // 使用ScrollView包裹内容，允许内容超出视图时滚动
    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
    {
        // 创建一个文本输入框，用于输入字符串
        EditorGUILayout.TextField("名称", "输入文本", GUILayout.ExpandWidth(true));
        
        // 创建一个数值输入框，用于输入整数
        EditorGUILayout.IntField("数量", 0, GUILayout.ExpandWidth(true));
        
        // 创建一个浮点数输入框，用于输入小数
        EditorGUILayout.FloatField("比例", 1.0f, GUILayout.ExpandWidth(true));
        
        // 创建一个开关按钮，用于切换布尔值
        EditorGUILayout.Toggle("启用", false, GUILayout.ExpandWidth(true));
        
        // 创建一个下拉选择框
        EditorGUILayout.Popup("选项", 0, new string[] { "选项1", "选项2", "选项3" });
        
        // 创建一个颜色选择器
        EditorGUILayout.ColorField("颜色", Color.white);
        
        // 创建一个对象选择框
        EditorGUILayout.ObjectField("对象", null, typeof(Object), true);
        
        // 创建一个可折叠的组
        EditorGUILayout.Foldout(true, "折叠组");
        
        // 创建一个分隔线
        EditorGUILayout.Space(10);
        
        // 创建一个按钮
        if (GUILayout.Button("确认", GUILayout.Height(30)))
        {
            Debug.Log("按钮被点击");
        }
    }
    GUILayout.EndScrollView();
}
GUILayout.EndVertical();
            
            GUILayout.EndVertical();    
            
            //分割线 垂直扩展 
            GUILayout.Box("垂直布局", GUILayout.Width(splitterWidth),
                
                GUILayout.ExpandHeight(true));
            //该方法用于获取GUILayout最后用于控件的矩形区域
            splitterRect = GUILayoutUtility.GetLastRect();

            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            GUILayout.Box("右侧区域", GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true));
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();

        if (Event.current != null)
        {
            //该方法用于在指定区域内显示指定的鼠标光标类型
            EditorGUIUtility.AddCursorRect(splitterRect,
                MouseCursor.ResizeHorizontal);
            switch (Event.current.type)
            {
                //开始拖拽分割线
                case EventType.MouseDown:
                    isDragging = splitterRect.Contains(
                        Event.current.mousePosition);
                    break;
                case EventType.MouseDrag:
                    if (isDragging)
                    {
                        splitterPos += Event.current.delta.x;
                        //限制其最大最小值
                        splitterPos = Mathf.Clamp(splitterPos,
                            position.width * .2f, position.width * .8f);
                        Repaint();
                    }
                    break;
                //结束拖拽分割线
                case EventType.MouseUp:
                    if (isDragging)
                        isDragging = false;
                    break;
            }
        }
    }

    private void ScrollViewExample()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < 50; i++)
        {
            GUILayout.Button("Button" + i);
        }
        GUILayout.EndScrollView();
    }
}