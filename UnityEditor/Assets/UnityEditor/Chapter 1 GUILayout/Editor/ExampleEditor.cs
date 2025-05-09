using UnityEngine;
using UnityEditor;


// GUILayout和EditorGUILayout的区别
// GUILayout是用于在GUI布局中创建控件的函数，适用于在GUI布局中需要一个控件的情况。
// EditorGUILayout是用于在EditorGUI布局中创建控件的函数，适用于在EditorGUI布局中需要一个控件的情况。

// 为什么当前可以混用？
// 因为EditorGUILayout是GUILayout的子类，所以可以混用。



[CustomEditor(typeof(Example))]
public class ExampleEditor : Editor
{
    private bool boolValue1;
    private bool boolValue2;
    private string stringValue = "Hello World.";
    private float floatValue = 10f;
    private int intValue = 5;
    private long longValue = 1;
    private string passwordValue = "123456";
    private Vector2 vector2Value = Vector2.zero;
    private Vector3 vector3Value = Vector3.zero;
    private Vector4 vector4Value = Vector4.zero;

    public enum ExampleEnum
    {
        Enum1,
        Enum2,
        Enum3
    }

    private ExampleEnum enumValue = ExampleEnum.Enum1;
    private bool foldout1;
    private bool foldout2;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Label();
        Button();
        Toggle();
        InputField();
        Dropdown();
        Slider();
        Foldout();
        Layout();
        LayoutOption();
        Space();
    }

    private void Label()
    {
        GUILayout.Label("Hello world.", EditorStyles.label); // 显示普通标签
        GUILayout.Label("Hello world.", EditorStyles.miniLabel); // 显示迷你标签
        GUILayout.Label("Hello world.", EditorStyles.largeLabel); // 显示大标签
        GUILayout.Label("Hello world.", EditorStyles.boldLabel); // 显示粗体标签
        GUILayout.Label("Hello world.", EditorStyles.miniBoldLabel); // 显示迷你粗体标签
        GUILayout.Label("Hello world.", EditorStyles.centeredGreyMiniLabel); // 显示居中灰色迷你标签
        GUILayout.Label("Hello world.", EditorStyles.wordWrappedMiniLabel, GUILayout.Width(50f)); // 显示宽度为50的换行迷你标签
        GUILayout.Label("Hello world.", EditorStyles.wordWrappedLabel); // 显示换行标签
        GUILayout.Label("Hello world.", EditorStyles.linkLabel); // 显示链接标签
        GUILayout.Label("Hello world.", EditorStyles.whiteLabel); // 显示白色标签
        GUILayout.Label("Hello world.", EditorStyles.whiteMiniLabel); // 显示白色迷你标签
        GUILayout.Label("Hello world.", EditorStyles.whiteLargeLabel); // 显示白色大标签
        GUILayout.Label("Hello world.", EditorStyles.whiteBoldLabel); // 显示白色粗体标签
        // 自定义字体样式：右对齐、字号为20
        GUILayout.Label("Hello world.", new GUIStyle() 
        {
            alignment = TextAnchor.LowerRight, // 设置文本对齐方式为右对齐
            fontSize = 20 // 设置字体大小为20
        });
    }

    private void Button()
    {
        GUILayout.Button("Button1");
        GUILayout.Button("Button2", EditorStyles.miniButton);
        GUILayout.Button("Button3", EditorStyles.radioButton);
        GUILayout.Button("Button4", EditorStyles.toolbarButton);
        //水平方向布局
        GUILayout.BeginHorizontal();
        GUILayout.Button("Button5", EditorStyles.miniButtonLeft);
        GUILayout.Button("Button6", EditorStyles.miniButtonMid);
        GUILayout.Button("Button7", EditorStyles.miniButtonMid);
        GUILayout.Button("Button8", EditorStyles.miniButtonRight);
        GUILayout.EndHorizontal();
    }

    private void Toggle()
    {
        // GUILayout.Toggle用于在GUILayout布局中创建一个切换按钮，适用于在GUILayout布局中需要一个切换按钮的情况。
        boolValue1 = GUILayout.Toggle(boolValue1, "Toggle1");
        // EditorGUILayout.Toggle用于在EditorGUILayout布局中创建一个切换按钮，适用于在EditorGUILayout布局中需要一个切换按钮的情况。
        boolValue2 = EditorGUILayout.Toggle("Toggle2", boolValue2);
    }

    private void InputField()
    {
        stringValue = EditorGUILayout.TextField("StringValue", stringValue);
        floatValue = EditorGUILayout.FloatField("FloatValue", floatValue);
        intValue = EditorGUILayout.IntField("IntValue", intValue);
        longValue = EditorGUILayout.LongField("LongValue", longValue);
        passwordValue = EditorGUILayout.PasswordField("PasswordValue", passwordValue);
        vector2Value = EditorGUILayout.Vector2Field("Vector2Value", vector2Value);
        vector3Value = EditorGUILayout.Vector3Field("Vector3Value", vector3Value);
        vector4Value = EditorGUILayout.Vector4Field("Vector4Value", vector4Value);
    }

    //下拉菜单
    private void Dropdown()
    {
        enumValue = (ExampleEnum) EditorGUILayout.EnumPopup("EnumValue", enumValue); // 创建一个下拉菜单，用于选择枚举值
        Selection.activeGameObject.tag = EditorGUILayout.TagField("TagValue", Selection.activeGameObject.tag); // 创建一个标签字段，用于选择标签
        Selection.activeGameObject.layer = EditorGUILayout.LayerField("LayerValue", Selection.activeGameObject.layer); // 创建一个层字段，用于选择层
    }

    private void Slider()
    {
        intValue = EditorGUILayout.IntSlider("IntValue", intValue, 0, 5); // 创建一个整数滑动条，取值范围为0～5
        floatValue = EditorGUILayout.Slider("FloatValue", floatValue, 0f, 10f); // 创建一个浮点数滑动条，取值范围为0～10
    }

    private void Foldout()
    {
        foldout1 = EditorGUILayout.Foldout(foldout1, "折叠栏1", true);
        if (foldout1)
        {
            GUILayout.Label("Hello world.", EditorStyles.miniLabel); // 显示迷你标签
            GUILayout.Label("Hello world.", EditorStyles.boldLabel); // 显示粗体标签
            GUILayout.Label("Hello world.", EditorStyles.largeLabel); // 显示大标签
            EditorGUILayout.LabelField("Hello world.", EditorStyles.miniLabel); // 显示迷你标签
        }

        foldout2 = EditorGUILayout.Foldout(foldout2, "折叠栏2", true);
        if (foldout2)
        {
            GUILayout.Button("Button1"); // 创建一个按钮
            GUILayout.Button("Button2"); // 创建一个按钮
            GUILayout.Button("Button3"); // 创建一个按钮
        }

        //分割横线
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.EndHorizontal();
    }

    private void Layout()
    {
        //水平布局中嵌套两个垂直布局
        GUILayout.BeginHorizontal();
            //第一个垂直布局
            GUILayout.BeginVertical();
                GUILayout.Button("Button1"); // 创建一个按钮
                GUILayout.Button("Button2"); // 创建一个按钮
            GUILayout.EndVertical();
            //第二个垂直布局
            GUILayout.BeginVertical();
                GUILayout.Button("Button3"); // 创建一个按钮
                GUILayout.Button("Button4"); // 创建一个按钮
                GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    private void LayoutOption()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Button("Button1",GUILayout.Width(50f)); // 创建一个按钮，宽度为50
        GUILayout.Button("Button2",GUILayout.Width(150f), GUILayout.Height(30f)); // 创建一个按钮，宽度为150，高度为30     
        GUILayout.Button("Button3",GUILayout.Width(200f), GUILayout.Height(40f)); // 创建一个按钮，宽度为200，高度为40
        GUILayout.EndHorizontal();
    }

    private void Space()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Button("Button1", GUILayout.Width(80f)); // 创建一个按钮，宽度为80                
        //固定间隔50个像素
        GUILayout.Space(50f); // 创建一个间隔，间隔为50个像素
        GUILayout.Button("Button2", GUILayout.Width(80f)); // 创建一个按钮，宽度为80
        GUILayout.EndHorizontal(); // 结束水平布局

        GUILayout.BeginHorizontal();
        GUILayout.Button("Button1", GUILayout.Width(80f));
        //灵活调整间隙（Button1在最左侧 Button2在最右侧）
        GUILayout.FlexibleSpace();
        GUILayout.Button("Button2", GUILayout.Width(80f));
        GUILayout.EndHorizontal();
    }
}