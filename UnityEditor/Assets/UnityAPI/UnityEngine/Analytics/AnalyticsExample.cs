using UnityEngine;
#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif
using System.Collections.Generic;

/// <summary>
/// UnityEngine.Analytics 命名空间案例演示
/// 展示自定义事件、标准事件、会话管理等核心功能
/// </summary>
public class AnalyticsExample : MonoBehaviour
{
    [Header("Analytics 设置")]
    [SerializeField] private string customEventName = "custom_event"; //自定义事件名称
    [SerializeField] private int score = 100; //分数
    [SerializeField] private string userId = "user_001"; //用户ID
    [SerializeField] private bool sendStandardEvent = false; //是否发送标准事件
    [SerializeField] private bool sendCustomEvent = false; //是否发送自定义事件

    private void Start()
    {
#if UNITY_ANALYTICS
        Analytics.enabled = true;
        Analytics.deviceStatsEnabled = true;//设备统计
        Analytics.limitUserTracking = false;//限制用户跟踪
        Debug.Log("Analytics 已启用");
#endif
    }

    private void Update()
    {
        if (sendStandardEvent)
        {
            sendStandardEvent = false;
            SendStandardEvent();
        }
        if (sendCustomEvent)
        {
            sendCustomEvent = false;
            SendCustomEvent();
        }
    }

    /// <summary>
    /// 发送标准事件
    /// </summary>
    public void SendStandardEvent()
    {
#if UNITY_ANALYTICS
        AnalyticsEvent.LevelStart(1);//关卡开始
        AnalyticsEvent.LevelComplete(1);//关卡完成
        AnalyticsEvent.ItemAcquired(AcquisitionType.Soft, "coin", 10, "shop", "level1");//物品获取
        Debug.Log("已发送标准事件");
#else
        Debug.LogWarning("未启用Unity Analytics模块");
#endif
    }

    /// <summary>
    /// 发送自定义事件
    /// </summary>
    public void SendCustomEvent()
    {
#if UNITY_ANALYTICS
        var eventData = new Dictionary<string, object>//事件数据
        {
            { "score", score },//分数
            { "userId", userId },//用户ID
            { "time", System.DateTime.Now.ToString() }//时间
        };
        Analytics.CustomEvent(customEventName, eventData);
        Debug.Log($"已发送自定义事件: {customEventName}");
#else
        Debug.LogWarning("未启用Unity Analytics模块");
#endif
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 250));
        GUILayout.Label("Analytics 演示", UnityEditor.EditorStyles.boldLabel);
        customEventName = GUILayout.TextField(customEventName);
        userId = GUILayout.TextField(userId);
        score = int.TryParse(GUILayout.TextField(score.ToString()), out var s) ? s : score;
        if (GUILayout.Button("发送标准事件"))
        {
            SendStandardEvent();
        }
        if (GUILayout.Button("发送自定义事件"))
        {
            SendCustomEvent();
        }
        GUILayout.EndArea();
    }
} 