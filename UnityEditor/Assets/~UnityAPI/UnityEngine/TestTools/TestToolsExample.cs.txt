using UnityEngine;
#if UNITY_INCLUDE_TESTS
using UnityEditor.TestTools;
using NUnit.Framework;
using UnityEngine.TestTools;
#endif

/// <summary>
/// UnityEngine.TestTools 命名空间案例演示
/// 展示TestRunner、TestAttribute、Assert等核心功能
/// </summary>
public class TestToolsExample : MonoBehaviour
{
    [Header("测试工具设置")]
    [SerializeField] private bool enableTestTools = true; //是否启用测试工具
    [SerializeField] private string testResult = ""; //测试结果
    [SerializeField] private int testCount = 0; //测试数量
    [SerializeField] private int passedTests = 0; //通过测试数量

    private void Start()
    {
#if UNITY_INCLUDE_TESTS
        if (enableTestTools)
        {
            Debug.Log("Test Tools 已启用");
            RunBasicTests();
        }
#endif
    }

#if UNITY_INCLUDE_TESTS
    /// <summary>
    /// 运行基础测试
    /// </summary>
    private void RunBasicTests()
    {
        testCount = 0;
        passedTests = 0;

        // 测试1: 基本断言
        testCount++;
        try
        {
            Assert.IsTrue(true, "基本断言测试");
            passedTests++;
        }
        catch (AssertionException ex)
        {
            Debug.LogError($"测试1失败: {ex.Message}");
        }

        // 测试2: 数值比较
        testCount++;
        try
        {
            Assert.AreEqual(5, 5, "数值比较测试");
            passedTests++;
        }
        catch (AssertionException ex)
        {
            Debug.LogError($"测试2失败: {ex.Message}");
        }

        // 测试3: 字符串比较
        testCount++;
        try
        {
            Assert.AreEqual("Hello", "Hello", "字符串比较测试");
            passedTests++;
        }
        catch (AssertionException ex)
        {
            Debug.LogError($"测试3失败: {ex.Message}");
        }

        // 测试4: 对象比较
        testCount++;
        try
        {
            GameObject obj1 = new GameObject("TestObject1");
            GameObject obj2 = new GameObject("TestObject2");
            Assert.AreNotEqual(obj1, obj2, "对象比较测试");
            DestroyImmediate(obj1);
            DestroyImmediate(obj2);
            passedTests++;
        }
        catch (AssertionException ex)
        {
            Debug.LogError($"测试4失败: {ex.Message}");
        }

        // 测试5: 异常测试
        testCount++;
        try
        {
            Assert.Throws<System.ArgumentException>(() => {
                throw new System.ArgumentException("测试异常");
            }, "异常测试");
            passedTests++;
        }
        catch (AssertionException ex)
        {
            Debug.LogError($"测试5失败: {ex.Message}");
        }

        testResult = $"测试完成: {passedTests}/{testCount} 通过";
        Debug.Log(testResult);
    }

    /// <summary>
    /// 测试方法示例
    /// </summary>
    [Test]
    public void ExampleTest()
    {
        // 这是一个可以被TestRunner自动发现的测试方法
        Assert.IsTrue(true, "示例测试");
    }

    /// <summary>
    /// 异步测试方法示例
    /// </summary>
    [UnityTest]
    public System.Collections.IEnumerator ExampleAsyncTest()
    {
        // 这是一个异步测试方法
        yield return null; // 等待一帧
        Assert.IsTrue(true, "异步示例测试");
    }

    /// <summary>
    /// 性能测试示例
    /// </summary>
    [Test]
    [Performance]
    public void ExamplePerformanceTest()
    {
        // 这是一个性能测试方法
        Measure.Method(() => {
            // 执行需要测试性能的代码
            for (int i = 0; i < 1000; i++)
            {
                Mathf.Sin(i);
            }
        })
        .WarmupCount(3)
        .MeasurementCount(10)
        .Run();
    }
#endif

    /// <summary>
    /// 获取测试信息
    /// </summary>
    public void GetTestInfo()
    {
        Debug.Log("=== 测试工具信息 ===");
        Debug.Log($"测试工具启用: {enableTestTools}");
        Debug.Log($"测试结果: {testResult}");
        Debug.Log($"测试数量: {testCount}");
        Debug.Log($"通过测试: {passedTests}");
        
#if UNITY_INCLUDE_TESTS
        Debug.Log("TestRunner 可用");
#else
        Debug.Log("TestRunner 不可用 (需要包含测试模块)");
#endif
    }

    /// <summary>
    /// 重新运行测试
    /// </summary>
    public void RerunTests()
    {
#if UNITY_INCLUDE_TESTS
        RunBasicTests();
#else
        Debug.LogWarning("测试模块未包含，无法运行测试");
#endif
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 250));
        GUILayout.Label("Test Tools 测试工具演示", UnityEditor.EditorStyles.boldLabel);
        
        enableTestTools = GUILayout.Toggle(enableTestTools, "启用测试工具");
        GUILayout.Label($"测试结果: {testResult}");
        GUILayout.Label($"测试数量: {testCount}");
        GUILayout.Label($"通过测试: {passedTests}");
        
        if (GUILayout.Button("获取测试信息"))
        {
            GetTestInfo();
        }
        
        if (GUILayout.Button("重新运行测试"))
        {
            RerunTests();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("测试说明:");
        GUILayout.Label("• 需要包含Unity Test Framework");
        GUILayout.Label("• 使用[Test]标记测试方法");
        GUILayout.Label("• 使用[UnityTest]标记异步测试");
        GUILayout.Label("• 使用[Performance]标记性能测试");
        
        GUILayout.EndArea();
    }
} 