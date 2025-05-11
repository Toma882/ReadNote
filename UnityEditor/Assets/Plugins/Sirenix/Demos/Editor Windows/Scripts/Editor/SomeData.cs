#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using UnityEditor;
    using System;

    [HideLabel]
    [Serializable]
    public class SomeData
    {
        [MultiLineProperty(3), Title("基础Odin菜单编辑器窗口", "继承自OdinMenuEditorWindow，并构建您的菜单树")]
        public string Test1 = "这个值在重新加载之间是持久的，但一旦重启Unity或关闭窗口就会重置。";

        [MultiLineProperty(3), ShowInInspector, NonSerialized]
        public string Test2 = "这个值在重新加载之间不是持久的，一旦你点击播放或重新编译就会重置。";

        [MultiLineProperty(3), ShowInInspector]
        private string Test3
        {
            get
            {
                return EditorPrefs.GetString("OdinDemo.PersistentString",
                    "这个值永远是持久的，即使跨Unity项目也是如此。但它不会与您的项目一起保存。" +
                    "这就是ScriptableObejcts和OdinEditorWindows派上用场的地方。");
            }
            set
            {
                EditorPrefs.SetString("OdinDemo.PersistentString", value);
            }
        }
    }
}
#endif
