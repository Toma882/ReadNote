#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using System;
    using UnityEngine;

#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;
    using UnityEditor;

#endif

    // 演示如何创建自定义组绘制器的示例。
    [TypeInfoBox("我们可能在这个示例中做得有点过火")]
    public class CustomGroupExample : SerializedMonoBehaviour
    {
        [PartyGroup(3f, 20f)]
        public int MyInt;

        [PartyGroup]
        public float MyFloat { get; set; }

        [PartyGroup]
        public void StateTruth()
        {
            Debug.Log("Odin Inspector太棒了。");
        }

        [PartyGroup("Group Two", 10f, 8f)]
        public Vector3 AVector3;

        [PartyGroup("Group Two")]
        public int AnotherInt;

        [InfoBox("当然，所有控件仍然可用。前提是你能捉住它们。")]
        [PartyGroup("Group Three", 0.8f, 250f)]
        public Quaternion AllTheWayAroundAndBack;

        [PartyGroup("Group Four", 1f, 12f)]
        public Thingy ThingyField;

        public class Thingy
        {
            [PartyGroup(1f, 12f)]
            public Thingy ThingyField;
        }
    }

    // 自定义组属性。必须继承PropertyGroupAttribute类。
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PartyGroupAttribute : PropertyGroupAttribute
    {
        public float Speed { get; private set; }
        public float Range { get; private set; }

        public PartyGroupAttribute(float speed = 0f, float range = 0f, int order = 0) : base("_DefaultGroup", order)
        {
            this.Speed = speed;
            this.Range = range;
        }

        public PartyGroupAttribute(string groupId, float speed = 0f, float range = 0f, int order = 0) : base(groupId, order)
        {
            this.Speed = speed;
            this.Range = range;
        }

        // 这个函数用于将多个组属性组合在一起。
        // 通过这种方式，可以在单个属性中仅指定某些设置，
        // 并仍使这些设置影响整个组。
        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var party = (PartyGroupAttribute)other;
            if (this.Speed == 0f)
            {
                this.Speed = party.Speed;
            }

            if (this.Range == 0f)
            {
                this.Range = party.Range;
            }
        }
    }

#if UNITY_EDITOR

    // 将绘制器脚本文件放在Editor文件夹中。
    public class PartyGroupAttributeDrawer : OdinGroupDrawer<PartyGroupAttribute>
    {
        private Color start;
        private Color target;

        protected override void Initialize()
        {
            this.start = UnityEngine.Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
            this.target = UnityEngine.Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
        }

        // 记得为自定义绘制器类添加OdinDrawer，否则Odin将找不到它们。
        protected override void DrawPropertyLayout(GUIContent label)
        {
            GUILayout.Space(8f);

            // 更改当前GUI变换矩阵，使检查器变得热闹。
            if (Event.current.rawType != EventType.Layout)
            {
                Vector3 offset = this.Property.LastDrawnValueRect.position + new Vector2(this.Property.LastDrawnValueRect.width, this.Property.LastDrawnValueRect.height) * 0.5f;
                Matrix4x4 matrix =
                    Matrix4x4.TRS(offset, Quaternion.identity, Vector3.one) *
                    Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(Mathf.Sin((float)EditorApplication.timeSinceStartup * this.Attribute.Speed) * this.Attribute.Range, Vector3.forward), Vector3.one * (1f + MathUtilities.BounceEaseInFastOut(Mathf.Sin((float)UnityEditor.EditorApplication.timeSinceStartup * 2f)) * 0.1f)) *
                    Matrix4x4.TRS(-offset + new Vector3(Mathf.Sin((float)EditorApplication.timeSinceStartup * 2f), 0f, 0f) * 100f, Quaternion.identity, Vector3.one) *
                    GUI.matrix;
                GUIHelper.PushMatrix(matrix);
            }

            // 更改派对颜色。
            if (Event.current.rawType == EventType.Repaint)
            {
                float t = MathUtilities.Bounce(Mathf.Sin((float)EditorApplication.timeSinceStartup * 2f));
                if (t <= 0f)
                {
                    this.start = this.target;
                    this.target = UnityEngine.Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
                }

                GUIHelper.PushColor(Color.Lerp(this.start, this.target, t));
            }

            // 绘制组的所有子属性。
            SirenixEditorGUI.BeginBox();
            for (int i = 0; i < this.Property.Children.Count; i++)
            {
                var child = this.Property.Children[i];
                child.Draw(child.Label);
            }
            SirenixEditorGUI.EndBox();

            // 恢复对GUI颜色和矩阵的更改。
            if (Event.current.rawType == EventType.Repaint)
            {
                GUIHelper.PopColor();
            }
            if (Event.current.rawType != EventType.Layout)
            {
                GUIHelper.PopMatrix();
            }

            // 请求重绘以获得流畅的动画效果。
            GUIHelper.RequestRepaint();
            GUILayout.Space(8f);
        }
    }

#endif
}
#endif
