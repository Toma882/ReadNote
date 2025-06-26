using UnityEngine;
#if UNITY_2021_1_OR_NEWER
using UnityEngine.Experimental.Rendering;
#endif

/// <summary>
/// UnityEngine.Experimental 命名空间案例演示
/// 展示实验性API的典型用法
/// </summary>
public class ExperimentalExample : MonoBehaviour
{
    [Header("实验性API设置")]
    [SerializeField] private bool useExperimentalRendering = true; //是否使用实验性渲染
    [SerializeField] private bool useExperimentalInput = false; //是否使用实验性输入
    [SerializeField] private bool useExperimentalXR = false; //是否使用实验性XR

    private void Start()
    {
#if UNITY_2021_1_OR_NEWER
        if (useExperimentalRendering)
        {
            Debug.Log($"当前GraphicsFormat: {SystemInfo.GetGraphicsFormat(UnityEngine.Experimental.Rendering.DefaultFormat.LDR)}");
        }
#endif
        if (useExperimentalInput)
        {
            Debug.Log("实验性Input API演示（需引入相关包）");
        }
        if (useExperimentalXR)
        {
            Debug.Log("实验性XR API演示（需引入相关包）");
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 180));
        GUILayout.Label("Experimental 实验性API演示", UnityEditor.EditorStyles.boldLabel);
        useExperimentalRendering = GUILayout.Toggle(useExperimentalRendering, "实验性Rendering");
        useExperimentalInput = GUILayout.Toggle(useExperimentalInput, "实验性Input");
        useExperimentalXR = GUILayout.Toggle(useExperimentalXR, "实验性XR");
        if (GUILayout.Button("执行实验性API演示"))
        {
            Start();
        }
        GUILayout.EndArea();
    }
} 