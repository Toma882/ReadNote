/* =======================================================
 *  Unity版本：2020.3.16f1c1
 *  作 者：张寿昆
 *  邮 箱：136512892@qq.com
 *  创建时间：2023-12-27 17:38:11
 *  当前版本：1.0.0
 *  主要功能：
 *  详细描述：
 *  修改记录：
 * =======================================================*/

using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class NodeBase
{
    public float x;
    public float y;
    public float width;
    public float height;
    protected string nodeTitle;
    private bool isDragging;

    public NodeBase prevNode;
    public NodeBase nextNode;
    private readonly float portSize = 15f;
    public Rect prevPort;
    public Rect nextPort;
    private bool isPrevWiring;
    private bool isNextWiring;

    public NodeBase(float x, float y, float width, float height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public void Update(out Vector2 prevWiringReleasePoint, out Vector2 nextWiringReleasePoint)
    {
        //Wire
        prevWiringReleasePoint = Vector2.zero;
        nextWiringReleasePoint = Vector2.zero;
        if (nextNode != null)
        {
            Vector3 start = new Vector3(nextPort.x + portSize * .5f, nextPort.y + portSize * .5f);
            Vector3 end = new Vector3(nextNode.prevPort.x + portSize * .5f, nextNode.prevPort.y + portSize * .5f);
            Handles.DrawBezier(
                start,
                end,
                new Vector2(start.x + 100f, start.y),
                new Vector2(end.x - 100f, end.y),
                Color.white, null, 3f);
        }

        //Background
        Rect backgroundRect = new Rect(x, y, width, height);
        GUI.Box(backgroundRect, "", "Badge");

        //Port
        prevPort = new Rect(x + 2f, y + height * .5f + 1f, portSize, portSize);
        nextPort = new Rect(x + width - portSize - 2f, y + height * .5f + 1f, portSize, portSize);
        GUI.Label(prevPort, EditorGUIUtility.IconContent("node0 hex@2x"));
        GUI.Label(nextPort, EditorGUIUtility.IconContent("node0 hex@2x"));

        //Title
        Color cacheColor = GUI.color;
        GUI.color = Color.cyan;
        Rect titleRect = new Rect(x + 1f, y + 1f, width - 2f, 20f);
        GUI.Box(titleRect, nodeTitle);
        GUI.color = cacheColor;

        OnGUI();

        //Event
        switch (Event.current.type)
        {
            case EventType.MouseDown:
                if (titleRect.Contains(Event.current.mousePosition))
                    isDragging = true;
                else if (prevPort.Contains(Event.current.mousePosition))
                    isPrevWiring = true;
                else if (nextPort.Contains(Event.current.mousePosition))
                    isNextWiring = true;
                break;
            case EventType.MouseDrag:
                if (isDragging)
                {
                    Vector2 delta = Event.current.delta;
                    x += delta.x;
                    y += delta.y;
                }
                break;
            case EventType.MouseUp:
                isDragging = false;
                if (isPrevWiring)
                {
                    isPrevWiring = false;
                    prevWiringReleasePoint = Event.current.mousePosition;
                }
                else if (isNextWiring)
                {
                    isNextWiring = false;
                    nextWiringReleasePoint = Event.current.mousePosition;
                }
                break;
            case EventType.Repaint:
                if (isPrevWiring)
                {
                    Vector3 start = new Vector3(prevPort.x + portSize * .5f, prevPort.y + portSize * .5f);
                    Vector3 end = Event.current.mousePosition;
                    Handles.DrawBezier(
                        start,
                        end,
                        new Vector2(start.x + 100f, start.y),
                        new Vector2(end.x - 100f, end.y),
                        Color.white, null, 3f);
                }
                else if (isNextWiring)
                {
                    Vector3 start = new Vector3(nextPort.x + portSize * .5f, nextPort.y + portSize * .5f);
                    Vector3 end = Event.current.mousePosition;
                    Handles.DrawBezier(
                        start,
                        end,
                        new Vector2(start.x + 100f, start.y),
                        new Vector2(end.x - 100f, end.y),
                        Color.white, null, 3f);
                }
                break;
        }
    }

    protected virtual void OnGUI() { }
}