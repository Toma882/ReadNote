#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using UnityEngine;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using System.Collections.Generic;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using UnityEditor;

#endif

    // 演示如何实现具有泛型约束的绘制器
    [TypeInfoBox(
        "这个示例演示了如何定义一个泛型的自定义绘制器。" +
        "\n这允许单个绘制器实现处理各种不同类型的值。")]
    public class GenericDrawerExample : SerializedMonoBehaviour
    {
        [OdinSerialize]
        public MyGenericClass<int, int> A = new MyGenericClass<int, int>(); // 使用结构体绘制器绘制

        [OdinSerialize]
        public MyGenericClass<Vector3, Quaternion> B = new MyGenericClass<Vector3, Quaternion>(); // 使用结构体绘制器绘制

        [OdinSerialize]
        public MyGenericClass<int, GameObject> C = new MyGenericClass<int, GameObject>(); // 使用泛型参数提取绘制器绘制

        [OdinSerialize]
        public MyGenericClass<string, List<string>> D = new MyGenericClass<string, List<string>>(); // 使用强列表绘制器绘制

        [OdinSerialize]
        public MyGenericClass<string, string> E = new MyGenericClass<string, string>(); // 使用默认绘制器绘制，因为下面的泛型绘制器都不适用

        public List<MyClass> F = new List<MyClass>(); // 使用自定义列表绘制器绘制
    }

    // 带有任意两个泛型类型的泛型类
    public class MyGenericClass<T1, T2>
    {
        public T1 First;
        public T2 Second;
    }

#if UNITY_EDITOR

    public class MyGenericClassDrawer_Struct<T1, T2> : OdinValueDrawer<MyGenericClass<T1, T2>>
        where T1 : struct
        where T2 : struct
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            SirenixEditorGUI.DrawSolidRect(EditorGUILayout.GetControlRect(), Color.red);
            this.CallNextDrawer(label);
        }
    }

    public class MyGenericClassDrawer_StrongList<TElement, TList> : OdinValueDrawer<MyGenericClass<TElement, TList>>
        where TList : IList<TElement>
        where TElement : class
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            SirenixEditorGUI.DrawSolidRect(EditorGUILayout.GetControlRect(), Color.blue);
            this.CallNextDrawer(label);
        }
    }

    // 注意这里可以将泛型参数作为绘制类型；Odin将查看参数的约束来确定适用范围
    public class MyGenericClassDrawer_GenericParameterExtraction<TValue, TUnityObject> : OdinValueDrawer<TValue>
        where TValue : MyGenericClass<int, TUnityObject>
        where TUnityObject : UnityEngine.Object
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            SirenixEditorGUI.DrawSolidRect(EditorGUILayout.GetControlRect(), Color.green);
            this.CallNextDrawer(label);
        }
    }

    [DrawerPriority(0, 0, 2)]
    public class MyClassListDrawer<TList, TElement> : OdinValueDrawer<TList>
        where TList : IList<TElement>
        where TElement : MyClass
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            SirenixEditorGUI.DrawSolidRect(EditorGUILayout.GetControlRect(), new Color(1, 0.5f, 0));
            this.CallNextDrawer(label);
        }
    }

#endif
}
#endif