/* =======================================================
 *  Unity版本：2020.3.16f1c1
 *  作 者：张寿昆
 *  邮 箱：136512892@qq.com
 *  创建时间：2023-12-28 13:23:16
 *  当前版本：1.0.0
 *  主要功能：
 *  详细描述：
 *  修改记录：
 * =======================================================*/

using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DelayEventNode : NodeBase
{
    public float delay = 1f;

    public DelayEventNode(float x, float y, float width, float height) 
        : base(x, y, width, height)
    {
        nodeTitle = "Delay";
    }

    protected override void OnGUI()
    {
        base.OnGUI();
        EditorGUI.LabelField(new Rect(x + 20f, y + 31f, 50f, 20f), "Duration");
        delay = EditorGUI.FloatField(new Rect(x + 80f, y + 31f, width - 100f, 20f), delay);
    }
}