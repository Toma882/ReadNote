using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// UnityEngine.Sprites 命名空间案例演示
/// 展示SpriteAtlas、SpriteRenderer、Sprite等核心功能
/// </summary>
public class SpritesExample : MonoBehaviour
{
    [Header("精灵设置")]
    [SerializeField] private Sprite[] sprites; //精灵数组
    [SerializeField] private SpriteAtlas spriteAtlas; //精灵图集
    [SerializeField] private bool useAtlas = false; //是否使用图集
    [SerializeField] private int currentSpriteIndex = 0; //当前精灵索引
    [SerializeField] private float animationSpeed = 1f; //动画速度

    private SpriteRenderer spriteRenderer;
    private float animationTimer = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if (sprites != null && sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

    private void Update()
    {
        if (sprites != null && sprites.Length > 1)
        {
            // 精灵动画
            animationTimer += Time.deltaTime * animationSpeed;
            if (animationTimer >= 1f)
            {
                animationTimer = 0f;
                currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
                spriteRenderer.sprite = sprites[currentSpriteIndex];
            }
        }
    }

    /// <summary>
    /// 从图集加载精灵
    /// </summary>
    /// <param name="spriteName">精灵名称</param>
    public void LoadSpriteFromAtlas(string spriteName)
    {
        if (spriteAtlas != null)
        {
            Sprite sprite = null;
            spriteAtlas.GetSprite(spriteName, out sprite);
            if (sprite != null)
            {
                spriteRenderer.sprite = sprite;
                Debug.Log($"从图集加载精灵: {spriteName}");
            }
            else
            {
                Debug.LogWarning($"图集中未找到精灵: {spriteName}");
            }
        }
    }

    /// <summary>
    /// 获取图集中的所有精灵
    /// </summary>
    public void GetAllSpritesFromAtlas()
    {
        if (spriteAtlas != null)
        {
            Sprite[] atlasSprites = new Sprite[spriteAtlas.spriteCount];
            spriteAtlas.GetSprites(atlasSprites);
            
            Debug.Log($"图集包含 {atlasSprites.Length} 个精灵:");
            for (int i = 0; i < atlasSprites.Length; i++)
            {
                if (atlasSprites[i] != null)
                {
                    Debug.Log($"  [{i}]: {atlasSprites[i].name}");
                }
            }
        }
    }

    /// <summary>
    /// 设置精灵
    /// </summary>
    /// <param name="index">精灵索引</param>
    public void SetSprite(int index)
    {
        if (sprites != null && index >= 0 && index < sprites.Length)
        {
            spriteRenderer.sprite = sprites[index];
            currentSpriteIndex = index;
            Debug.Log($"设置精灵: {sprites[index].name}");
        }
    }

    /// <summary>
    /// 获取精灵信息
    /// </summary>
    public void GetSpriteInfo()
    {
        if (spriteRenderer.sprite != null)
        {
            Sprite sprite = spriteRenderer.sprite;
            Debug.Log("=== 精灵信息 ===");
            Debug.Log($"精灵名称: {sprite.name}");
            Debug.Log($"精灵大小: {sprite.rect.size}");
            Debug.Log($"精灵像素密度: {sprite.pixelsPerUnit}");
            Debug.Log($"精灵边界: {sprite.bounds}");
            Debug.Log($"精灵纹理: {sprite.texture.name}");
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 300));
        GUILayout.Label("Sprites 精灵系统演示", UnityEditor.EditorStyles.boldLabel);
        
        useAtlas = GUILayout.Toggle(useAtlas, "使用精灵图集");
        animationSpeed = float.TryParse(GUILayout.TextField("动画速度", animationSpeed.ToString()), out var speed) ? speed : animationSpeed;
        
        GUILayout.Label($"当前精灵索引: {currentSpriteIndex}");
        if (sprites != null)
        {
            GUILayout.Label($"精灵数量: {sprites.Length}");
        }
        
        if (GUILayout.Button("获取精灵信息"))
        {
            GetSpriteInfo();
        }
        
        if (useAtlas && spriteAtlas != null)
        {
            if (GUILayout.Button("获取图集所有精灵"))
            {
                GetAllSpritesFromAtlas();
            }
            
            if (GUILayout.Button("从图集加载测试精灵"))
            {
                LoadSpriteFromAtlas("test_sprite");
            }
        }
        
        if (sprites != null && sprites.Length > 0)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("上一个精灵"))
            {
                int newIndex = (currentSpriteIndex - 1 + sprites.Length) % sprites.Length;
                SetSprite(newIndex);
            }
            if (GUILayout.Button("下一个精灵"))
            {
                int newIndex = (currentSpriteIndex + 1) % sprites.Length;
                SetSprite(newIndex);
            }
            GUILayout.EndHorizontal();
        }
        
        GUILayout.EndArea();
    }
} 