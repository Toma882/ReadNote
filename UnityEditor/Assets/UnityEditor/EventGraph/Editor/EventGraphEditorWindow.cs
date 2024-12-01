/* =======================================================
 *  Unity版本：2020.3.16f1c1
 *  作 者：张寿昆
 *  邮 箱：136512892@qq.com
 *  创建时间：2023-12-28 14:35:29
 *  当前版本：1.0.0
 *  主要功能：
 *  详细描述：
 *  修改记录：
 * =======================================================*/

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class EventGraphEditorWindow : EditorWindow
{
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int lineNumber)
    {
        string assetPath = AssetDatabase.GetAssetPath(instanceID);
        Type type = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
        if (type == typeof(EventGraph))
        {
            var window = GetWindow<EventGraphEditorWindow>();
            EventGraph graph = AssetDatabase.LoadAssetAtPath<EventGraph>(assetPath);
            window.titleContent = new GUIContent(graph.name);
            window.Open(graph);
            return true;
        }
        return false;
    }

    private EventGraph eventGraph;
    private List<NodeBase> nodes = new List<NodeBase>();

    public void Open(EventGraph eventGraph)
    {
        this.eventGraph = eventGraph;
    }

    private void Update()
    {
        Repaint();
    }

    private void OnGUI()
    {
        EventHandle();

        for (int i = 0; i < nodes.Count; i++)
        {
            NodeBase node = nodes[i];
            node.Update(
                out Vector2 prevWiringReleasePoint,
                out Vector2 nextWiringReleasePoint);

            if (prevWiringReleasePoint != Vector2.zero)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    NodeBase wireNode = nodes[j];
                    if (node == wireNode) continue;
                    if (wireNode.nextPort.Contains(prevWiringReleasePoint))
                    {
                        node.prevNode = wireNode;
                        if (wireNode.nextNode != null)
                            wireNode.nextNode.prevNode = null;
                        wireNode.nextNode = node;
                    }
                }
            }
            else if (nextWiringReleasePoint != Vector2.zero)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    NodeBase wireNode = nodes[j];
                    if (node == wireNode) continue;
                    if (wireNode.prevPort.Contains(nextWiringReleasePoint))
                    {
                        node.nextNode = wireNode;
                        if (wireNode.prevNode != null)
                            wireNode.prevNode.nextNode = null;
                        wireNode.prevNode = node;
                    }
                }
            }
        }
    }

    private void EventHandle()
    {
        switch (Event.current.type)
        {
            case EventType.MouseDown:
                //右键按下
                if (Event.current.button == 1)
                {
                    Vector2 mousePosition = Event.current.mousePosition;
                    GenericMenu gm = new GenericMenu();
                    gm.AddItem(new GUIContent("Delay Event"), false, 
                        () => nodes.Add(new DelayEventNode(mousePosition.x, mousePosition.y, 150f, 60f)));
                    gm.ShowAsContext();
                }
                break;
        }
    }
}
