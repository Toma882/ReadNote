using UnityEditor;
using UnityEngine;

public class AutoBoxCollider
{
    [MenuItem("CONTEXT/BoxCollider/Encapsulate")]
    public static void Execute()
    {
        Transform selected = Selection.activeTransform;
        var boxCollider = selected.GetComponent<BoxCollider>();
        //记录坐标、旋转值、缩放值
        Vector3 position = selected.position;
        Quaternion rotation = selected.rotation;
        Vector3 scale = selected.localScale;
        //坐标旋转归零
        selected.position = Vector3.zero;
        selected.rotation = Quaternion.identity;
        //创建边界盒并计算
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        var renders = selected.GetComponentsInChildren<MeshRenderer>(true);
        for (int j = 0; j < renders.Length; j++)
        {
            //Encapsulate方法将当前渲染器的边界包围到总边界中
            bounds.Encapsulate(renders[j].bounds); 
        }
        //设置边界盒中心
        boxCollider.center = bounds.center;
        //设置边界盒大小
        Vector3 size = bounds.size;
        size.x /= scale.x;
        size.y /= scale.y;
        size.z /= scale.z;
        boxCollider.size = size;
        //恢复坐标旋转
        selected.position = position;
        selected.rotation = rotation;
    }
}