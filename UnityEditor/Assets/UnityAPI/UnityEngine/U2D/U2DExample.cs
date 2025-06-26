using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;

namespace UnityEditor.Chapter8.U2D
{
    /// <summary>
    /// UnityEngine.U2D 2D系统案例
    /// 演示SpriteAtlas、SpriteRenderer、2D物理、2D动画等功能
    /// </summary>
    public class U2DExample : MonoBehaviour
    {
        [Header("Sprite Atlas 设置")]
        [SerializeField] private SpriteAtlas spriteAtlas;
        [SerializeField] private List<Sprite> sprites = new List<Sprite>();
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [Header("2D 物理设置")]
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Collider2D collider2D;
        [SerializeField] private bool enablePhysics = true;
        [SerializeField] private float physicsForce = 10f;
        
        [Header("2D 动画设置")]
        [SerializeField] private Animator animator;
        [SerializeField] private RuntimeAnimatorController animatorController;
        [SerializeField] private bool enableAnimation = true;
        
        [Header("2D 渲染设置")]
        [SerializeField] private SpriteSortPoint spriteSortPoint = SpriteSortPoint.Center;
        [SerializeField] private bool flipX = false;
        [SerializeField] private bool flipY = false;
        [SerializeField] private Color spriteColor = Color.white;
        
        [Header("UI控制")]
        [SerializeField] private bool showControls = true;
        
        private int currentSpriteIndex = 0;
        private Vector2 originalPosition;
        private bool isAnimating = false;
        
        private void Start()
        {
            InitializeU2DComponents();
        }
        
        /// <summary>
        /// 初始化2D组件
        /// </summary>
        private void InitializeU2DComponents()
        {
            // 获取或添加SpriteRenderer
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                }
            }
            
            // 获取或添加Rigidbody2D
            if (rigidbody2D == null)
            {
                rigidbody2D = GetComponent<Rigidbody2D>();
                if (rigidbody2D == null && enablePhysics)
                {
                    rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
                }
            }
            
            // 获取或添加Collider2D
            if (collider2D == null)
            {
                collider2D = GetComponent<Collider2D>();
                if (collider2D == null && enablePhysics)
                {
                    collider2D = gameObject.AddComponent<BoxCollider2D>();
                }
            }
            
            // 获取或添加Animator
            if (animator == null)
            {
                animator = GetComponent<Animator>();
                if (animator == null && enableAnimation)
                {
                    animator = gameObject.AddComponent<Animator>();
                }
            }
            
            // 设置初始属性
            originalPosition = transform.position;
            SetupSpriteRenderer();
            SetupPhysics();
            SetupAnimation();
        }
        
        /// <summary>
        /// 设置SpriteRenderer属性
        /// </summary>
        private void SetupSpriteRenderer()
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.spriteSortPoint = spriteSortPoint;
                spriteRenderer.flipX = flipX;
                spriteRenderer.flipY = flipY;
                spriteRenderer.color = spriteColor;
                
                // 设置第一个Sprite
                if (sprites.Count > 0)
                {
                    spriteRenderer.sprite = sprites[0];
                }
            }
        }
        
        /// <summary>
        /// 设置2D物理属性
        /// </summary>
        private void SetupPhysics()
        {
            if (rigidbody2D != null)
            {
                rigidbody2D.gravityScale = 1f;
                rigidbody2D.drag = 0.5f;
                rigidbody2D.angularDrag = 0.5f;
            }
        }
        
        /// <summary>
        /// 设置动画属性
        /// </summary>
        private void SetupAnimation()
        {
            if (animator != null && animatorController != null)
            {
                animator.runtimeAnimatorController = animatorController;
            }
        }
        
        /// <summary>
        /// 从SpriteAtlas加载Sprite
        /// </summary>
        public void LoadSpritesFromAtlas()
        {
            if (spriteAtlas != null)
            {
                spriteAtlas.GetSprites(sprites);
                Debug.Log($"从SpriteAtlas加载了 {sprites.Count} 个Sprite");
                
                if (sprites.Count > 0 && spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprites[0];
                    currentSpriteIndex = 0;
                }
            }
        }
        
        /// <summary>
        /// 切换到下一个Sprite
        /// </summary>
        public void NextSprite()
        {
            if (sprites.Count > 0)
            {
                currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprites[currentSpriteIndex];
                }
                Debug.Log($"切换到Sprite: {currentSpriteIndex}");
            }
        }
        
        /// <summary>
        /// 切换到上一个Sprite
        /// </summary>
        public void PreviousSprite()
        {
            if (sprites.Count > 0)
            {
                currentSpriteIndex = (currentSpriteIndex - 1 + sprites.Count) % sprites.Count;
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprites[currentSpriteIndex];
                }
                Debug.Log($"切换到Sprite: {currentSpriteIndex}");
            }
        }
        
        /// <summary>
        /// 应用物理力
        /// </summary>
        public void ApplyPhysicsForce(Vector2 force)
        {
            if (rigidbody2D != null)
            {
                rigidbody2D.AddForce(force, ForceMode2D.Impulse);
                Debug.Log($"应用物理力: {force}");
            }
        }
        
        /// <summary>
        /// 重置位置
        /// </summary>
        public void ResetPosition()
        {
            transform.position = originalPosition;
            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.angularVelocity = 0f;
            }
            Debug.Log("位置已重置");
        }
        
        /// <summary>
        /// 切换物理开关
        /// </summary>
        public void TogglePhysics()
        {
            enablePhysics = !enablePhysics;
            
            if (rigidbody2D != null)
            {
                rigidbody2D.simulated = enablePhysics;
            }
            
            if (collider2D != null)
            {
                collider2D.enabled = enablePhysics;
            }
            
            Debug.Log($"物理系统: {(enablePhysics ? "开启" : "关闭")}");
        }
        
        /// <summary>
        /// 切换动画开关
        /// </summary>
        public void ToggleAnimation()
        {
            enableAnimation = !enableAnimation;
            
            if (animator != null)
            {
                animator.enabled = enableAnimation;
            }
            
            Debug.Log($"动画系统: {(enableAnimation ? "开启" : "关闭")}");
        }
        
        /// <summary>
        /// 播放动画
        /// </summary>
        public void PlayAnimation(string triggerName)
        {
            if (animator != null && enableAnimation)
            {
                animator.SetTrigger(triggerName);
                isAnimating = true;
                Debug.Log($"播放动画: {triggerName}");
            }
        }
        
        /// <summary>
        /// 停止动画
        /// </summary>
        public void StopAnimation()
        {
            if (animator != null)
            {
                animator.enabled = false;
                isAnimating = false;
                Debug.Log("动画已停止");
            }
        }
        
        /// <summary>
        /// 设置Sprite颜色
        /// </summary>
        public void SetSpriteColor(Color color)
        {
            spriteColor = color;
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
        }
        
        /// <summary>
        /// 翻转Sprite
        /// </summary>
        public void FlipSprite(bool flipX, bool flipY)
        {
            this.flipX = flipX;
            this.flipY = flipY;
            
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = flipX;
                spriteRenderer.flipY = flipY;
            }
        }
        
        /// <summary>
        /// 设置Sprite排序点
        /// </summary>
        public void SetSpriteSortPoint(SpriteSortPoint sortPoint)
        {
            spriteSortPoint = sortPoint;
            if (spriteRenderer != null)
            {
                spriteRenderer.spriteSortPoint = sortPoint;
            }
        }
        
        /// <summary>
        /// 创建2D碰撞器
        /// </summary>
        public void CreateCollider2D(Collider2DType type)
        {
            // 移除现有碰撞器
            if (collider2D != null)
            {
                DestroyImmediate(collider2D);
            }
            
            // 创建新碰撞器
            switch (type)
            {
                case Collider2DType.Box:
                    collider2D = gameObject.AddComponent<BoxCollider2D>();
                    break;
                case Collider2DType.Circle:
                    collider2D = gameObject.AddComponent<CircleCollider2D>();
                    break;
                case Collider2DType.Capsule:
                    collider2D = gameObject.AddComponent<CapsuleCollider2D>();
                    break;
                case Collider2DType.Polygon:
                    collider2D = gameObject.AddComponent<PolygonCollider2D>();
                    break;
            }
            
            Debug.Log($"创建了 {type} 碰撞器");
        }
        
        /// <summary>
        /// 获取Sprite信息
        /// </summary>
        public void GetSpriteInfo()
        {
            if (spriteRenderer != null && spriteRenderer.sprite != null)
            {
                Sprite sprite = spriteRenderer.sprite;
                Debug.Log("=== Sprite信息 ===");
                Debug.Log($"名称: {sprite.name}");
                Debug.Log($"尺寸: {sprite.rect.width} x {sprite.rect.height}");
                Debug.Log($"像素密度: {sprite.pixelsPerUnit}");
                Debug.Log($"边界: {sprite.bounds}");
                Debug.Log($"枢轴点: {sprite.pivot}");
            }
        }
        
        private void OnGUI()
        {
            if (!showControls) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 400, 600));
            GUILayout.Label("UnityEngine.U2D 2D系统案例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            // Sprite控制
            GUILayout.Label("Sprite控制", EditorStyles.boldLabel);
            if (GUILayout.Button("从Atlas加载Sprites"))
            {
                LoadSpritesFromAtlas();
            }
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("上一个Sprite")) PreviousSprite();
            if (GUILayout.Button("下一个Sprite")) NextSprite();
            GUILayout.EndHorizontal();
            
            GUILayout.Label($"当前Sprite: {currentSpriteIndex + 1}/{sprites.Count}");
            
            GUILayout.Space(10);
            
            // 物理控制
            GUILayout.Label("物理控制", EditorStyles.boldLabel);
            if (GUILayout.Button($"物理系统: {(enablePhysics ? "开启" : "关闭")}"))
            {
                TogglePhysics();
            }
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("向上力")) ApplyPhysicsForce(Vector2.up * physicsForce);
            if (GUILayout.Button("向下力")) ApplyPhysicsForce(Vector2.down * physicsForce);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("向左力")) ApplyPhysicsForce(Vector2.left * physicsForce);
            if (GUILayout.Button("向右力")) ApplyPhysicsForce(Vector2.right * physicsForce);
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("重置位置"))
            {
                ResetPosition();
            }
            
            GUILayout.Space(10);
            
            // 动画控制
            GUILayout.Label("动画控制", EditorStyles.boldLabel);
            if (GUILayout.Button($"动画系统: {(enableAnimation ? "开启" : "关闭")}"))
            {
                ToggleAnimation();
            }
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("播放动画")) PlayAnimation("Play");
            if (GUILayout.Button("停止动画")) StopAnimation();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // 渲染设置
            GUILayout.Label("渲染设置", EditorStyles.boldLabel);
            
            bool newFlipX = GUILayout.Toggle(flipX, "水平翻转");
            if (newFlipX != flipX)
            {
                FlipSprite(newFlipX, flipY);
            }
            
            bool newFlipY = GUILayout.Toggle(flipY, "垂直翻转");
            if (newFlipY != flipY)
            {
                FlipSprite(flipX, newFlipY);
            }
            
            // 颜色选择
            Color newColor = spriteColor;
            GUILayout.Label("Sprite颜色:");
            newColor.r = GUILayout.HorizontalSlider(newColor.r, 0f, 1f);
            newColor.g = GUILayout.HorizontalSlider(newColor.g, 0f, 1f);
            newColor.b = GUILayout.HorizontalSlider(newColor.b, 0f, 1f);
            newColor.a = GUILayout.HorizontalSlider(newColor.a, 0f, 1f);
            
            if (newColor != spriteColor)
            {
                SetSpriteColor(newColor);
            }
            
            GUILayout.Space(10);
            
            // 碰撞器设置
            GUILayout.Label("碰撞器设置", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Box")) CreateCollider2D(Collider2DType.Box);
            if (GUILayout.Button("Circle")) CreateCollider2D(Collider2DType.Circle);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Capsule")) CreateCollider2D(Collider2DType.Capsule);
            if (GUILayout.Button("Polygon")) CreateCollider2D(Collider2DType.Polygon);
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // 信息显示
            GUILayout.Label("信息", EditorStyles.boldLabel);
            if (GUILayout.Button("获取Sprite信息"))
            {
                GetSpriteInfo();
            }
            
            GUILayout.Label($"位置: {transform.position}");
            GUILayout.Label($"旋转: {transform.rotation.eulerAngles}");
            GUILayout.Label($"缩放: {transform.localScale}");
            
            if (rigidbody2D != null)
            {
                GUILayout.Label($"速度: {rigidbody2D.velocity}");
                GUILayout.Label($"角速度: {rigidbody2D.angularVelocity}");
            }
            
            GUILayout.EndArea();
        }
        
        /// <summary>
        /// 碰撞器类型枚举
        /// </summary>
        public enum Collider2DType
        {
            Box,
            Circle,
            Capsule,
            Polygon
        }
    }
} 