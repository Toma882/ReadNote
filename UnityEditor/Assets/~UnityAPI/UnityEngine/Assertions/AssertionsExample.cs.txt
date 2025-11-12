using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// UnityEngine.Assertions 命名空间案例演示
/// 展示断言的核心功能
/// </summary>
public class AssertionsExample : MonoBehaviour
{
    [Header("断言测试参数")]
    [SerializeField] private int a = 5;//测试参数a
    [SerializeField] private int b = 5;//测试参数b
    [SerializeField] private int c = 10;//测试参数c
    [SerializeField] private string str = "hello";//测试参数str
    [SerializeField] private string str2 = "hello";
    [SerializeField] private GameObject go;

    private void Start()
    {
        // 基本断言
        Assert.IsTrue(a == b, "a 应该等于 b");
        Assert.AreEqual(a, b, "a 和 b 应该相等");
        Assert.AreNotEqual(a, c, "a 和 c 不应该相等");
        Assert.IsNotNull(go, "GameObject 不应为 null");
        Assert.AreEqual(str, str2, "字符串应该相等");
        Debug.Log("断言测试已执行，若无报错则全部通过");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 200));
        GUILayout.Label("Assertions 断言演示", UnityEditor.EditorStyles.boldLabel);
        a = int.TryParse(GUILayout.TextField(a.ToString()), out var aVal) ? aVal : a;   //测试参数a
        b = int.TryParse(GUILayout.TextField(b.ToString()), out var bVal) ? bVal : b;   //测试参数b
        c = int.TryParse(GUILayout.TextField(c.ToString()), out var cVal) ? cVal : c;   //测试参数c
        str = GUILayout.TextField(str);   //测试参数str
        str2 = GUILayout.TextField(str2);   //测试参数str2
        go = (GameObject)UnityEditor.EditorGUILayout.ObjectField("GameObject", go, typeof(GameObject), true);   //测试参数go
        if (GUILayout.Button("执行断言测试"))
        {
            try
            {
                Assert.IsTrue(a == b, "a 应该等于 b");
                Assert.AreEqual(a, b, "a 和 b 应该相等");
                Assert.AreNotEqual(a, c, "a 和 c 不应该相等");
                Assert.IsNotNull(go, "GameObject 不应为 null");
                Assert.AreEqual(str, str2, "字符串应该相等");
                Debug.Log("断言全部通过");
            }
            catch (AssertionException ex)
            {
                Debug.LogError($"断言失败: {ex.Message}");
            }
        }
        GUILayout.EndArea();
    }
} 