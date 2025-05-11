#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using Sirenix.Utilities;
    using System.Linq;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    // 
    // 这是一个包含所有在Unity项目中可用的角色列表的可脚本化对象。
    // 当一个角色从RPG编辑器添加时，列表会通过UpdateCharacterOverview自动更新。
    //
    // 如果你在检查器中查看Character Overview，你还会注意到，
    // 列表不能直接修改。相反，我们已经自定义了它，使其包含一个刷新按钮，
    // 该按钮会扫描项目并自动填充列表。
    //
    // CharacterOverview继承自GlobalConfig，它只是一个由Odin Inspector用于配置文件等的
    // 可脚本化对象单例，但它也可以很容易地只是一个简单的可脚本化对象。
    // 

    [GlobalConfig("Plugins/Sirenix/Demos/RPG Editor/Characters")]
    public class CharacterOverview : GlobalConfig<CharacterOverview> 
    {
        [ReadOnly]
        [ListDrawerSettings(ShowFoldout = true)]
        public Character[] AllCharacters;

#if UNITY_EDITOR
        [Button(ButtonSizes.Medium), PropertyOrder(-1)]
        public void UpdateCharacterOverview()
        {
            // 查找并分配所有Character类型的可脚本化对象
            this.AllCharacters = AssetDatabase.FindAssets("t:Character")
                .Select(guid => AssetDatabase.LoadAssetAtPath<Character>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();
        }
#endif
    }
}
#endif
