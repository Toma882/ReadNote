using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// UnityEngine.Profiling 命名空间案例演示
/// 展示Profiler采样、性能统计等核心功能
/// </summary>
public class ProfilingExample : MonoBehaviour
{
    [Header("Profiler 设置")]
    [SerializeField] private string sampleName = "CustomSample";
    [SerializeField] private int loopCount = 10000;
    [SerializeField] private float lastSampleTime = 0f;

    private void Update()
    {
        Profiler.BeginSample(sampleName);
        float sum = 0f;
        for (int i = 0; i < loopCount; i++)
        {
            sum += Mathf.Sin(i);
        }
        Profiler.EndSample();
        lastSampleTime = Time.realtimeSinceStartup;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 180));
        GUILayout.Label("Profiling 性能分析演示", UnityEditor.EditorStyles.boldLabel);
        sampleName = GUILayout.TextField(sampleName);
        loopCount = int.TryParse(GUILayout.TextField(loopCount.ToString()), out var l) ? l : loopCount;
        GUILayout.Label($"上次采样时间: {lastSampleTime:F3}s");
        GUILayout.Label("请在Unity Profiler窗口查看自定义采样段");
        GUILayout.EndArea();
    }
} 