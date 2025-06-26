using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;
using UnityEngine.U2D;
using System.Collections.Generic;

namespace UnityEditor.Sprites.Examples
{
    /// <summary>
    /// UnityEditor.Sprites 命名空间使用示例
    /// 演示精灵编辑器系统的创建、编辑和管理功能
    /// </summary>
    public class SpritesExample : MonoBehaviour
    {
        [Header("精灵配置")]
        [SerializeField] private bool enableSpriteEditor = true;
        [SerializeField] private string spriteName = "CustomSprite";
        [SerializeField] private Vector2 spriteSize = new Vector2(64, 64);
        [SerializeField] private SpriteAlignment alignment = SpriteAlignment.Center;
        
        [Header("精灵状态")]
        [SerializeField] private int spriteCount = 0;
        [SerializeField] private string currentSprite = "";
        [SerializeField] private bool isSpriteValid = false;
        
        [Header("目标对象")]
        [SerializeField] private Texture2D sourceTexture;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteAtlas spriteAtlas;
        
        [Header("精灵数据")]
        [SerializeField] private List<Sprite> availableSprites = new List<Sprite>();
        [SerializeField] private Sprite currentSpriteAsset;
        
        private Dictionary<string, Sprite> spriteRegistry = new Dictionary<string, Sprite>();
        
        /// <summary>
        /// 初始化精灵系统
        /// </summary>
        private void Start()
        {
            InitializeSpriteSystem();
        }
        
        /// <summary>
        /// 初始化精灵系统
        /// </summary>
        private void InitializeSpriteSystem()
        {
            if (!enableSpriteEditor)
            {
                Debug.Log("精灵编辑器系统已禁用");
                return;
            }
            
            try
            {
                // 加载可用精灵
                LoadAvailableSprites();
                
                // 创建默认精灵
                CreateDefaultSprites();
                
                Debug.Log("精灵编辑器系统初始化完成");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"精灵编辑器系统初始化失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 加载可用精灵
        /// </summary>
        private void LoadAvailableSprites()
        {
            availableSprites.Clear();
            spriteRegistry.Clear();
            
            // 获取所有精灵资源
            string[] spriteGuids = AssetDatabase.FindAssets("t:Sprite");
            foreach (string guid in spriteGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                if (sprite != null)
                {
                    availableSprites.Add(sprite);
                    spriteRegistry[sprite.name] = sprite;
                }
            }
            
            spriteCount = availableSprites.Count;
            Debug.Log($"加载了 {spriteCount} 个精灵");
        }
        
        /// <summary>
        /// 创建默认精灵
        /// </summary>
        private void CreateDefaultSprites()
        {
            // 创建简单的彩色精灵
            CreateColorSprite("RedSprite", Color.red);
            CreateColorSprite("GreenSprite", Color.green);
            CreateColorSprite("BlueSprite", Color.blue);
        }
        
        /// <summary>
        /// 创建彩色精灵
        /// </summary>
        private void CreateColorSprite(string spriteName, Color color)
        {
            // 创建纹理
            Texture2D texture = new Texture2D((int)spriteSize.x, (int)spriteSize.y);
            Color[] pixels = new Color[texture.width * texture.height];
            
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }
            
            texture.SetPixels(pixels);
            texture.Apply();
            
            // 创建精灵
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
                Vector2.zero, 100, 0, SpriteMeshType.FullRect, Vector4.zero);
            sprite.name = spriteName;
            
            // 保存精灵
            SaveSprite(sprite, spriteName);
            
            // 添加到注册表
            availableSprites.Add(sprite);
            spriteRegistry[spriteName] = sprite;
            
            Debug.Log($"彩色精灵已创建: {spriteName}");
        }
        
        /// <summary>
        /// 保存精灵
        /// </summary>
        private void SaveSprite(Sprite sprite, string fileName)
        {
            string path = $"Assets/Sprites/{fileName}.png";
            
            // 确保目录存在
            string directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            
            // 保存纹理
            byte[] pngData = sprite.texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(path, pngData);
            
            // 刷新资源数据库
            AssetDatabase.Refresh();
            
            // 设置精灵导入设置
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteAlignment = (int)alignment;
                importer.spritePixelsPerUnit = 100;
                importer.SaveAndReimport();
            }
            
            Debug.Log($"精灵已保存: {path}");
        }
        
        /// <summary>
        /// 从纹理创建精灵
        /// </summary>
        public Sprite CreateSpriteFromTexture(Texture2D texture, string spriteName)
        {
            if (texture == null)
            {
                Debug.LogError("纹理为空");
                return null;
            }
            
            // 创建精灵
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
                Vector2.zero, 100, 0, SpriteMeshType.FullRect, Vector4.zero);
            sprite.name = spriteName;
            
            // 保存精灵
            SaveSprite(sprite, spriteName);
            
            // 添加到注册表
            availableSprites.Add(sprite);
            spriteRegistry[spriteName] = sprite;
            spriteCount = availableSprites.Count;
            
            Debug.Log($"从纹理创建精灵: {spriteName}");
            return sprite;
        }
        
        /// <summary>
        /// 创建精灵图集
        /// </summary>
        public SpriteAtlas CreateSpriteAtlas(string atlasName, Sprite[] sprites)
        {
            if (sprites == null || sprites.Length == 0)
            {
                Debug.LogError("精灵数组为空");
                return null;
            }
            
            // 创建精灵图集
            SpriteAtlas atlas = new SpriteAtlas();
            
            // 设置图集参数
            SpriteAtlasPackingSettings packingSettings = new SpriteAtlasPackingSettings()
            {
                enableRotation = false,
                enableTightPacking = true,
                padding = 2
            };
            atlas.SetPackingSettings(packingSettings);
            
            // 设置纹理参数
            SpriteAtlasTextureSettings textureSettings = new SpriteAtlasTextureSettings()
            {
                readable = false,
                generateMipMaps = false,
                sRGB = true,
                filterMode = FilterMode.Bilinear
            };
            atlas.SetTextureSettings(textureSettings);
            
            // 添加精灵
            atlas.Add(sprites);
            
            // 保存图集
            string path = $"Assets/Sprites/{atlasName}.spriteatlas";
            string directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            
            AssetDatabase.CreateAsset(atlas, path);
            AssetDatabase.SaveAssets();
            
            Debug.Log($"精灵图集已创建: {atlasName}");
            return atlas;
        }
        
        /// <summary>
        /// 设置精灵属性
        /// </summary>
        public void SetSpriteProperties(Sprite sprite, Vector2 pivot, SpriteAlignment alignment)
        {
            if (sprite == null)
            {
                Debug.LogError("精灵为空");
                return;
            }
            
            string path = AssetDatabase.GetAssetPath(sprite);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            
            if (importer != null)
            {
                importer.spriteAlignment = (int)alignment;
                importer.spritePivot = pivot;
                importer.SaveAndReimport();
                
                Debug.Log($"精灵属性已设置: {sprite.name}");
            }
        }
        
        /// <summary>
        /// 获取精灵信息
        /// </summary>
        public string GetSpriteInfo(Sprite sprite)
        {
            if (sprite == null)
                return "精灵为空";
            
            return $"名称: {sprite.name}, 大小: {sprite.rect.size}, 像素密度: {sprite.pixelsPerUnit}, 边界: {sprite.bounds}";
        }
        
        /// <summary>
        /// 验证精灵有效性
        /// </summary>
        public bool ValidateSprite(Sprite sprite)
        {
            return sprite != null && sprite.texture != null;
        }
        
        /// <summary>
        /// 导出精灵
        /// </summary>
        public void ExportSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.LogWarning("精灵为空");
                return;
            }
            
            string exportPath = EditorUtility.SaveFilePanel("导出精灵", "", sprite.name, "png");
            if (string.IsNullOrEmpty(exportPath))
                return;
            
            try
            {
                // 创建纹理副本
                Texture2D texture = sprite.texture;
                Rect rect = sprite.rect;
                
                // 提取精灵区域
                Color[] pixels = texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
                Texture2D spriteTexture = new Texture2D((int)rect.width, (int)rect.height);
                spriteTexture.SetPixels(pixels);
                spriteTexture.Apply();
                
                // 保存为PNG
                byte[] pngData = spriteTexture.EncodeToPNG();
                System.IO.File.WriteAllBytes(exportPath, pngData);
                
                Debug.Log($"精灵已导出到: {exportPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导出精灵失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 导入精灵
        /// </summary>
        public void ImportSprite()
        {
            string importPath = EditorUtility.OpenFilePanel("导入精灵", "", "png,jpg,jpeg");
            if (string.IsNullOrEmpty(importPath))
                return;
            
            try
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(importPath);
                string targetPath = $"Assets/Sprites/{fileName}.png";
                
                // 确保目录存在
                string directory = System.IO.Path.GetDirectoryName(targetPath);
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
                
                // 复制文件
                System.IO.File.Copy(importPath, targetPath, true);
                AssetDatabase.Refresh();
                
                // 设置导入设置
                TextureImporter importer = AssetImporter.GetAtPath(targetPath) as TextureImporter;
                if (importer != null)
                {
                    importer.textureType = TextureImporterType.Sprite;
                    importer.spriteAlignment = (int)alignment;
                    importer.spritePixelsPerUnit = 100;
                    importer.SaveAndReimport();
                }
                
                // 重新加载精灵
                LoadAvailableSprites();
                
                Debug.Log($"精灵已导入: {targetPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"导入精灵失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 获取所有精灵名称
        /// </summary>
        public string[] GetAllSpriteNames()
        {
            return availableSprites.Select(s => s.name).ToArray();
        }
        
        /// <summary>
        /// 应用精灵到渲染器
        /// </summary>
        public void ApplySpriteToRenderer(Sprite sprite, SpriteRenderer renderer)
        {
            if (sprite == null || renderer == null)
            {
                Debug.LogWarning("精灵或渲染器为空");
                return;
            }
            
            renderer.sprite = sprite;
            currentSprite = sprite.name;
            currentSpriteAsset = sprite;
            
            Debug.Log($"精灵已应用到渲染器: {sprite.name}");
        }
        
        /// <summary>
        /// 在编辑器中显示GUI
        /// </summary>
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 500, 700));
            GUILayout.Label("UnityEditor.Sprites 示例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            GUILayout.Label($"状态: {(enableSpriteEditor ? "启用" : "禁用")}");
            GUILayout.Label($"精灵数量: {spriteCount}");
            GUILayout.Label($"当前精灵: {currentSprite}");
            GUILayout.Label($"精灵有效: {(isSpriteValid ? "是" : "否")}");
            
            GUILayout.Space(10);
            GUILayout.Label("精灵创建", EditorStyles.boldLabel);
            
            spriteName = GUILayout.TextField("精灵名称", spriteName);
            spriteSize = EditorGUILayout.Vector2Field("精灵大小", spriteSize);
            alignment = (SpriteAlignment)EditorGUILayout.EnumPopup("对齐方式", alignment);
            
            if (GUILayout.Button("创建红色精灵"))
            {
                CreateColorSprite(spriteName + "_Red", Color.red);
            }
            
            if (GUILayout.Button("创建绿色精灵"))
            {
                CreateColorSprite(spriteName + "_Green", Color.green);
            }
            
            if (GUILayout.Button("创建蓝色精灵"))
            {
                CreateColorSprite(spriteName + "_Blue", Color.blue);
            }
            
            GUILayout.Space(10);
            GUILayout.Label("精灵管理", EditorStyles.boldLabel);
            
            sourceTexture = (Texture2D)EditorGUILayout.ObjectField("源纹理", sourceTexture, typeof(Texture2D), false);
            
            if (GUILayout.Button("从纹理创建精灵"))
            {
                if (sourceTexture != null)
                {
                    CreateSpriteFromTexture(sourceTexture, spriteName);
                }
            }
            
            string[] spriteNames = GetAllSpriteNames();
            if (spriteNames.Length > 0)
            {
                int selectedIndex = EditorGUILayout.Popup("选择精灵", 0, spriteNames);
                if (selectedIndex >= 0 && selectedIndex < availableSprites.Count)
                {
                    currentSpriteAsset = availableSprites[selectedIndex];
                    
                    GUILayout.Label(GetSpriteInfo(currentSpriteAsset));
                    
                    if (GUILayout.Button("导出精灵"))
                    {
                        ExportSprite(currentSpriteAsset);
                    }
                    
                    if (GUILayout.Button("验证精灵"))
                    {
                        isSpriteValid = ValidateSprite(currentSpriteAsset);
                        Debug.Log($"精灵验证结果: {(isSpriteValid ? "有效" : "无效")}");
                    }
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("精灵应用", EditorStyles.boldLabel);
            
            spriteRenderer = (SpriteRenderer)EditorGUILayout.ObjectField("精灵渲染器", spriteRenderer, typeof(SpriteRenderer), true);
            
            if (GUILayout.Button("应用当前精灵到渲染器"))
            {
                if (currentSpriteAsset != null && spriteRenderer != null)
                {
                    ApplySpriteToRenderer(currentSpriteAsset, spriteRenderer);
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("导入导出", EditorStyles.boldLabel);
            
            if (GUILayout.Button("导入精灵"))
            {
                ImportSprite();
            }
            
            if (GUILayout.Button("刷新精灵列表"))
            {
                LoadAvailableSprites();
            }
            
            GUILayout.Space(10);
            GUILayout.Label("配置", EditorStyles.boldLabel);
            
            enableSpriteEditor = EditorGUILayout.Toggle("启用精灵编辑器", enableSpriteEditor);
            
            GUILayout.EndArea();
        }
    }
} 