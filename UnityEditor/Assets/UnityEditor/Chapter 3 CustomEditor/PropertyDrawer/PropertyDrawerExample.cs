using UnityEngine;

public class PropertyDrawerExample : MonoBehaviour
{
    [Range(4f, 10f)]
    [SerializeField] private float radius1 = 5f;

    [Min(4f)]
    [SerializeField] private float radius2 = 5f;

    [Multiline(5), SerializeField] 
    private string text1 = "Hello World.\r\nToday is a good day.";

    [TextArea, SerializeField]
    private string text2 = "Hello World.\r\nToday is a good day.";

    [ColorUsage(true, true), SerializeField]
    private Color color = Color.white;

    [GradientUsage(false, ColorSpace.Gamma)]
    [SerializeField] private Gradient gradient;

    [SerializeField] private float radius3 = 5f;
    [Space(10f)]
    [SerializeField] private string text3 = "Hello World.";
    [SerializeField] private int intValue = 2;

    [Header("***相关变量")]
    [SerializeField] private float radius = 5f;
    [SerializeField] private string text4 = "Hello World.";

    [Time, SerializeField]
    private float duration = 596f;

    [Time, SerializeField]
    private int delay = 2;
}