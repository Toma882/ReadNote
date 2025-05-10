using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GizmosExample))]
public class GizmosExampleEditor : Editor
{
    // 该特性用于在Unity编辑器中绘制Gizmo，用于视觉化游戏对象的信息。
    // GizmoType.Pickable: 使Gizmo可以被选中。
    // GizmoType.Active: 当游戏对象被选中时，Gizmo会被高亮显示。
    // GizmoType.InSelectionHierarchy: 当游戏对象或其子对象被选中时，Gizmo会被高亮显示。
    // GizmoType.NotInSelectionHierarchy: 当游戏对象或其子对象未被选中时，Gizmo会被高亮显示。
    // typeof(GizmosExample): 指定了这个特性应用于哪个类型的游戏对象。
    [DrawGizmo(GizmoType.Pickable | GizmoType.Active | GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy, typeof(GizmosExample))]

    public static void DrawGizmos(GizmosExample example, GizmoType gizmoType)
    {
        // 可以通过color调整颜色
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, Vector3.up);
        Gizmos.DrawSphere(Vector3.left, .3f);
        Gizmos.DrawWireSphere(Vector3.left * 2f, .3f);
        Gizmos.DrawCube(Vector3.left * 3f, Vector3.one * .3f);
        Gizmos.DrawWireCube(Vector3.left * 4f, Vector3.one * .3f);
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Draw Icon"))
        {
            Gizmos.DrawIcon(Vector3.zero, "Axe.png", true);
        }
        if (GUILayout.Button("Change Color"))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Vector3.left, .3f);
        }

    }
}   