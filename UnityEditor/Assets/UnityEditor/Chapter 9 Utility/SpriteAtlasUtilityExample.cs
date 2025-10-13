using UnityEngine;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine.U2D;
using System.Collections.Generic;

namespace UnityEditor.Examples
{
    /// <summary>
    /// SpriteAtlasUtility 工具类示例
    /// 提供精灵图集相关的实用工具功能
    /// </summary>
    public static class SpriteAtlasUtilityExample
    {
        #region 打包图集示例

        /// <summary>
        /// 打包图集
        /// </summary>
        public static void PackAtlasesExample()
        {
            // 查找所有SpriteAtlas资源
            string[] guids = AssetDatabase.FindAssets("t:SpriteAtlas");
            
            if (guids.Length == 0)
            {
                Debug.LogWarning("场景中没有找到SpriteAtlas资源");
                return;
            }

            List<SpriteAtlas> atlases = new List<SpriteAtlas>();
            
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SpriteAtlas atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(path);
                if (atlas != null)
                {
                    atlases.Add(atlas);
                }
            }

            if (atlases.Count > 0)
            {
                // 打包图集
                SpriteAtlasUtility.PackAtlases(atlases.ToArray(), EditorUserBuildSettings.activeBuildTarget);
                Debug.Log($"已打包 {atlases.Count} 个图集");
            }
        }

        /// <summary>
        /// 打包指定图集
        /// </summary>
        public static void PackSpecificAtlasExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            SpriteAtlasUtility.PackAtlases(new SpriteAtlas[] { atlas }, EditorUserBuildSettings.activeBuildTarget);
            Debug.Log($"图集 {atlas.name} 已打包");
        }

        /// <summary>
        /// 打包图集（指定平台）
        /// </summary>
        public static void PackAtlasesForPlatformExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            BuildTarget targetPlatform = BuildTarget.Android;
            SpriteAtlasUtility.PackAtlases(new SpriteAtlas[] { atlas }, targetPlatform);
            
            Debug.Log($"图集 {atlas.name} 已为平台 {targetPlatform} 打包");
        }

        #endregion

        #region 图集信息示例

        /// <summary>
        /// 获取图集信息
        /// </summary>
        public static void GetAtlasInfoExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            Debug.Log($"图集信息: {atlas.name}");
            Debug.Log($"- 图集资源: {atlas}");
            
            // 获取图集中的精灵
            Sprite[] sprites = new Sprite[atlas.spriteCount];
            atlas.GetSprites(sprites);
            
            Debug.Log($"- 精灵数量: {atlas.spriteCount}");
            Debug.Log($"- 精灵列表: {string.Join(", ", System.Array.ConvertAll(sprites, s => s.name))}");
        }

        /// <summary>
        /// 检查图集状态
        /// </summary>
        public static void CheckAtlasStatusExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            Debug.Log($"图集状态检查: {atlas.name}");
            
            // 检查图集是否已打包
            bool isPacked = atlas.isVariant;
            Debug.Log($"- 是否为变体: {isPacked}");
            
            // 检查图集设置
            SpriteAtlasTextureSettings textureSettings = atlas.GetTextureSettings();
            Debug.Log($"- 纹理格式: {textureSettings.format}");
            Debug.Log($"- 压缩质量: {textureSettings.compressionQuality}");
            
            SpriteAtlasPackingSettings packingSettings = atlas.GetPackingSettings();
            Debug.Log($"- 打包模式: {packingSettings.packingMode}");
            Debug.Log($"- 块大小: {packingSettings.blockOffset}");
        }

        #endregion

        #region 图集设置示例

        /// <summary>
        /// 设置图集纹理设置
        /// </summary>
        public static void SetAtlasTextureSettingsExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            SpriteAtlasTextureSettings textureSettings = atlas.GetTextureSettings();
            
            // 修改设置
            textureSettings.format = TextureImporterFormat.RGBA32;
            textureSettings.compressionQuality = 50;
            textureSettings.generateMipMaps = true;
            textureSettings.sRGB = true;
            
            atlas.SetTextureSettings(textureSettings);
            
            Debug.Log($"图集纹理设置已更新: {atlas.name}");
            Debug.Log($"- 格式: {textureSettings.format}");
            Debug.Log($"- 压缩质量: {textureSettings.compressionQuality}");
            Debug.Log($"- 生成MipMaps: {textureSettings.generateMipMaps}");
        }

        /// <summary>
        /// 设置图集打包设置
        /// </summary>
        public static void SetAtlasPackingSettingsExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            SpriteAtlasPackingSettings packingSettings = atlas.GetPackingSettings();
            
            // 修改设置
            packingSettings.packingMode = SpriteAtlasPackingMode.Tight;
            packingSettings.blockOffset = 1;
            packingSettings.enableRotation = false;
            packingSettings.enableTightPacking = true;
            
            atlas.SetPackingSettings(packingSettings);
            
            Debug.Log($"图集打包设置已更新: {atlas.name}");
            Debug.Log($"- 打包模式: {packingSettings.packingMode}");
            Debug.Log($"- 块偏移: {packingSettings.blockOffset}");
            Debug.Log($"- 启用旋转: {packingSettings.enableRotation}");
        }

        #endregion

        #region 精灵操作示例

        /// <summary>
        /// 添加精灵到图集
        /// </summary>
        public static void AddSpritesToAtlasExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            // 查找场景中的精灵
            Sprite[] sprites = Object.FindObjectsOfType<Sprite>();
            
            if (sprites.Length == 0)
            {
                Debug.LogWarning("场景中没有找到精灵");
                return;
            }

            // 添加精灵到图集
            List<Object> objectsToAdd = new List<Object>();
            foreach (Sprite sprite in sprites)
            {
                objectsToAdd.Add(sprite);
            }
            
            atlas.Add(objectsToAdd.ToArray());
            
            Debug.Log($"已添加 {objectsToAdd.Count} 个精灵到图集 {atlas.name}");
        }

        /// <summary>
        /// 从图集移除精灵
        /// </summary>
        public static void RemoveSpritesFromAtlasExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            // 获取图集中的精灵
            Sprite[] sprites = new Sprite[atlas.spriteCount];
            atlas.GetSprites(sprites);
            
            if (sprites.Length == 0)
            {
                Debug.LogWarning("图集中没有精灵");
                return;
            }

            // 移除第一个精灵
            atlas.Remove(new Object[] { sprites[0] });
            
            Debug.Log($"已从图集 {atlas.name} 移除精灵: {sprites[0].name}");
        }

        #endregion

        #region 图集变体示例

        /// <summary>
        /// 创建图集变体
        /// </summary>
        public static void CreateAtlasVariantExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            // 创建变体
            SpriteAtlas variant = SpriteAtlasUtility.CreateAtlasVariant(atlas, "HD", 2.0f);
            
            if (variant != null)
            {
                Debug.Log($"图集变体已创建: {variant.name}");
                Debug.Log($"- 原始图集: {atlas.name}");
                Debug.Log($"- 变体比例: 2.0x");
            }
        }

        /// <summary>
        /// 获取图集变体
        /// </summary>
        public static void GetAtlasVariantsExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            SpriteAtlas[] variants = SpriteAtlasUtility.GetAtlasVariants(atlas);
            
            Debug.Log($"图集 {atlas.name} 的变体:");
            Debug.Log($"- 变体数量: {variants.Length}");
            
            foreach (SpriteAtlas variant in variants)
            {
                Debug.Log($"  - {variant.name}");
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建图集测试
        /// </summary>
        public static void CreateAtlasTestExample()
        {
            // 创建新的SpriteAtlas
            SpriteAtlas atlas = ScriptableObject.CreateInstance<SpriteAtlas>();
            
            // 设置纹理设置
            SpriteAtlasTextureSettings textureSettings = new SpriteAtlasTextureSettings
            {
                format = TextureImporterFormat.RGBA32,
                compressionQuality = 50,
                generateMipMaps = false,
                sRGB = true
            };
            atlas.SetTextureSettings(textureSettings);
            
            // 设置打包设置
            SpriteAtlasPackingSettings packingSettings = new SpriteAtlasPackingSettings
            {
                packingMode = SpriteAtlasPackingMode.Tight,
                blockOffset = 1,
                enableRotation = false,
                enableTightPacking = true
            };
            atlas.SetPackingSettings(packingSettings);
            
            // 保存图集
            string path = "Assets/TestSpriteAtlas.spriteatlas";
            AssetDatabase.CreateAsset(atlas, path);
            AssetDatabase.SaveAssets();
            
            Debug.Log($"测试图集已创建: {path}");
        }

        /// <summary>
        /// 批量图集处理
        /// </summary>
        public static void BatchAtlasProcessingExample()
        {
            string[] guids = AssetDatabase.FindAssets("t:SpriteAtlas");
            
            Debug.Log($"批量处理 {guids.Length} 个图集:");
            
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SpriteAtlas atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(path);
                
                if (atlas != null)
                {
                    Debug.Log($"处理图集: {atlas.name}");
                    Debug.Log($"  - 路径: {path}");
                    Debug.Log($"  - 精灵数量: {atlas.spriteCount}");
                    
                    // 这里可以添加具体的处理逻辑
                }
            }
        }

        /// <summary>
        /// 图集诊断
        /// </summary>
        public static void AtlasDiagnosticsExample()
        {
            SpriteAtlas atlas = GetSelectedSpriteAtlas();
            if (atlas == null) return;

            Debug.Log($"=== 图集诊断: {atlas.name} ===");
            
            // 基本信息
            Debug.Log($"✓ 精灵数量: {atlas.spriteCount}");
            Debug.Log($"✓ 是否为变体: {atlas.isVariant}");
            
            // 纹理设置
            SpriteAtlasTextureSettings textureSettings = atlas.GetTextureSettings();
            Debug.Log($"✓ 纹理格式: {textureSettings.format}");
            Debug.Log($"✓ 压缩质量: {textureSettings.compressionQuality}");
            Debug.Log($"✓ 生成MipMaps: {textureSettings.generateMipMaps}");
            
            // 打包设置
            SpriteAtlasPackingSettings packingSettings = atlas.GetPackingSettings();
            Debug.Log($"✓ 打包模式: {packingSettings.packingMode}");
            Debug.Log($"✓ 块偏移: {packingSettings.blockOffset}");
            Debug.Log($"✓ 启用旋转: {packingSettings.enableRotation}");
            
            // 变体信息
            SpriteAtlas[] variants = SpriteAtlasUtility.GetAtlasVariants(atlas);
            Debug.Log($"✓ 变体数量: {variants.Length}");
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取选中的SpriteAtlas
        /// </summary>
        private static SpriteAtlas GetSelectedSpriteAtlas()
        {
            Object selected = Selection.activeObject;
            if (selected is SpriteAtlas atlas)
            {
                return atlas;
            }

            // 查找场景中的第一个SpriteAtlas
            string[] guids = AssetDatabase.FindAssets("t:SpriteAtlas");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<SpriteAtlas>(path);
            }

            Debug.LogWarning("未找到SpriteAtlas");
            return null;
        }

        #endregion
    }
}
