using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;

namespace UnityEditor.Chapter8.VFX
{
    /// <summary>
    /// UnityEngine.VFX 视觉效果系统案例
    /// 演示VisualEffect、VFXGraph、粒子系统、视觉效果控制等功能
    /// </summary>
    public class VFXExample : MonoBehaviour
    {
        [Header("Visual Effect 设置")]
        [SerializeField] private VisualEffect visualEffect;
        [SerializeField] private VisualEffectAsset vfxAsset;
        [SerializeField] private bool autoPlay = true;
        
        [Header("粒子系统设置")]
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private bool enableParticles = true;
        [SerializeField] private int maxParticles = 1000;
        
        [Header("视觉效果参数")]
        [SerializeField] private float emissionRate = 10f;
        [SerializeField] private float particleLifetime = 2f;
        [SerializeField] private float particleSpeed = 5f;
        [SerializeField] private Vector3 particleScale = Vector3.one;
        [SerializeField] private Color particleColor = Color.white;
        
        [Header("动画设置")]
        [SerializeField] private bool enableAnimation = true;
        [SerializeField] private float animationSpeed = 1f;
        [SerializeField] private bool loopAnimation = true;
        
        [Header("空间设置")]
        [SerializeField] private Vector3 effectPosition = Vector3.zero;
        [SerializeField] private Vector3 effectRotation = Vector3.zero;
        [SerializeField] private Vector3 effectScale = Vector3.one;
        
        [Header("UI控制")]
        [SerializeField] private bool showControls = true;
        
        private bool isPlaying = false;
        private bool isPaused = false;
        private float currentTime = 0f;
        private Vector3 originalPosition;
        private Vector3 originalRotation;
        private Vector3 originalScale;
        
        private void Start()
        {
            InitializeVFXComponents();
        }
        
        /// <summary>
        /// 初始化VFX组件
        /// </summary>
        private void InitializeVFXComponents()
        {
            // 获取或添加VisualEffect
            if (visualEffect == null)
            {
                visualEffect = GetComponent<VisualEffect>();
                if (visualEffect == null)
                {
                    visualEffect = gameObject.AddComponent<VisualEffect>();
                }
            }
            
            // 获取或添加ParticleSystem
            if (particleSystem == null)
            {
                particleSystem = GetComponent<ParticleSystem>();
                if (particleSystem == null && enableParticles)
                {
                    particleSystem = gameObject.AddComponent<ParticleSystem>();
                }
            }
            
            // 保存原始变换
            originalPosition = transform.position;
            originalRotation = transform.rotation.eulerAngles;
            originalScale = transform.localScale;
            
            // 设置初始属性
            SetupVisualEffect();
            SetupParticleSystem();
            SetupTransform();
        }
        
        /// <summary>
        /// 设置VisualEffect属性
        /// </summary>
        private void SetupVisualEffect()
        {
            if (visualEffect != null && vfxAsset != null)
            {
                visualEffect.visualEffectAsset = vfxAsset;
                visualEffect.playRate = animationSpeed;
                
                // 设置参数
                SetVFXParameter("EmissionRate", emissionRate);
                SetVFXParameter("ParticleLifetime", particleLifetime);
                SetVFXParameter("ParticleSpeed", particleSpeed);
                SetVFXParameter("ParticleScale", particleScale);
                SetVFXParameter("ParticleColor", particleColor);
                
                if (autoPlay)
                {
                    PlayVFX();
                }
            }
        }
        
        /// <summary>
        /// 设置ParticleSystem属性
        /// </summary>
        private void SetupParticleSystem()
        {
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.maxParticles = maxParticles;
                main.startLifetime = particleLifetime;
                main.startSpeed = particleSpeed;
                main.startSize = particleScale.x;
                main.startColor = particleColor;
                main.loop = loopAnimation;
                main.playOnAwake = autoPlay;
                
                var emission = particleSystem.emission;
                emission.rateOverTime = emissionRate;
                
                if (autoPlay)
                {
                    PlayParticles();
                }
            }
        }
        
        /// <summary>
        /// 设置变换属性
        /// </summary>
        private void SetupTransform()
        {
            transform.position = effectPosition;
            transform.rotation = Quaternion.Euler(effectRotation);
            transform.localScale = effectScale;
        }
        
        /// <summary>
        /// 设置VFX参数
        /// </summary>
        public void SetVFXParameter(string parameterName, object value)
        {
            if (visualEffect != null)
            {
                if (value is float floatValue)
                {
                    visualEffect.SetFloat(parameterName, floatValue);
                }
                else if (value is int intValue)
                {
                    visualEffect.SetInt(parameterName, intValue);
                }
                else if (value is Vector3 vector3Value)
                {
                    visualEffect.SetVector3(parameterName, vector3Value);
                }
                else if (value is Color colorValue)
                {
                    visualEffect.SetVector4(parameterName, colorValue);
                }
                else if (value is bool boolValue)
                {
                    visualEffect.SetBool(parameterName, boolValue);
                }
                
                Debug.Log($"设置VFX参数 {parameterName}: {value}");
            }
        }
        
        /// <summary>
        /// 播放VFX
        /// </summary>
        public void PlayVFX()
        {
            if (visualEffect != null)
            {
                visualEffect.Play();
                isPlaying = true;
                isPaused = false;
                Debug.Log("开始播放VFX");
            }
        }
        
        /// <summary>
        /// 暂停VFX
        /// </summary>
        public void PauseVFX()
        {
            if (visualEffect != null && isPlaying)
            {
                visualEffect.pause = true;
                isPaused = true;
                Debug.Log("VFX已暂停");
            }
        }
        
        /// <summary>
        /// 恢复VFX
        /// </summary>
        public void ResumeVFX()
        {
            if (visualEffect != null && isPaused)
            {
                visualEffect.pause = false;
                isPaused = false;
                Debug.Log("VFX已恢复");
            }
        }
        
        /// <summary>
        /// 停止VFX
        /// </summary>
        public void StopVFX()
        {
            if (visualEffect != null)
            {
                visualEffect.Stop();
                isPlaying = false;
                isPaused = false;
                Debug.Log("VFX已停止");
            }
        }
        
        /// <summary>
        /// 播放粒子系统
        /// </summary>
        public void PlayParticles()
        {
            if (particleSystem != null)
            {
                particleSystem.Play();
                Debug.Log("开始播放粒子系统");
            }
        }
        
        /// <summary>
        /// 暂停粒子系统
        /// </summary>
        public void PauseParticles()
        {
            if (particleSystem != null)
            {
                particleSystem.Pause();
                Debug.Log("粒子系统已暂停");
            }
        }
        
        /// <summary>
        /// 停止粒子系统
        /// </summary>
        public void StopParticles()
        {
            if (particleSystem != null)
            {
                particleSystem.Stop();
                Debug.Log("粒子系统已停止");
            }
        }
        
        /// <summary>
        /// 清除粒子
        /// </summary>
        public void ClearParticles()
        {
            if (particleSystem != null)
            {
                particleSystem.Clear();
                Debug.Log("粒子已清除");
            }
        }
        
        /// <summary>
        /// 设置发射率
        /// </summary>
        public void SetEmissionRate(float rate)
        {
            emissionRate = Mathf.Max(0, rate);
            
            if (visualEffect != null)
            {
                SetVFXParameter("EmissionRate", emissionRate);
            }
            
            if (particleSystem != null)
            {
                var emission = particleSystem.emission;
                emission.rateOverTime = emissionRate;
            }
            
            Debug.Log($"发射率设置为: {emissionRate}");
        }
        
        /// <summary>
        /// 设置粒子生命周期
        /// </summary>
        public void SetParticleLifetime(float lifetime)
        {
            particleLifetime = Mathf.Max(0.1f, lifetime);
            
            if (visualEffect != null)
            {
                SetVFXParameter("ParticleLifetime", particleLifetime);
            }
            
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startLifetime = particleLifetime;
            }
            
            Debug.Log($"粒子生命周期设置为: {particleLifetime}秒");
        }
        
        /// <summary>
        /// 设置粒子速度
        /// </summary>
        public void SetParticleSpeed(float speed)
        {
            particleSpeed = speed;
            
            if (visualEffect != null)
            {
                SetVFXParameter("ParticleSpeed", particleSpeed);
            }
            
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startSpeed = particleSpeed;
            }
            
            Debug.Log($"粒子速度设置为: {particleSpeed}");
        }
        
        /// <summary>
        /// 设置粒子颜色
        /// </summary>
        public void SetParticleColor(Color color)
        {
            particleColor = color;
            
            if (visualEffect != null)
            {
                SetVFXParameter("ParticleColor", particleColor);
            }
            
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startColor = particleColor;
            }
            
            Debug.Log($"粒子颜色设置为: {particleColor}");
        }
        
        /// <summary>
        /// 设置粒子缩放
        /// </summary>
        public void SetParticleScale(Vector3 scale)
        {
            particleScale = scale;
            
            if (visualEffect != null)
            {
                SetVFXParameter("ParticleScale", particleScale);
            }
            
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startSize = particleScale.x;
            }
            
            Debug.Log($"粒子缩放设置为: {particleScale}");
        }
        
        /// <summary>
        /// 设置动画速度
        /// </summary>
        public void SetAnimationSpeed(float speed)
        {
            animationSpeed = Mathf.Max(0.1f, speed);
            
            if (visualEffect != null)
            {
                visualEffect.playRate = animationSpeed;
            }
            
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.simulationSpeed = animationSpeed;
            }
            
            Debug.Log($"动画速度设置为: {animationSpeed}x");
        }
        
        /// <summary>
        /// 重置变换
        /// </summary>
        public void ResetTransform()
        {
            transform.position = originalPosition;
            transform.rotation = Quaternion.Euler(originalRotation);
            transform.localScale = originalScale;
            
            effectPosition = originalPosition;
            effectRotation = originalRotation;
            effectScale = originalScale;
            
            Debug.Log("变换已重置");
        }
        
        /// <summary>
        /// 创建爆炸效果
        /// </summary>
        public void CreateExplosionEffect()
        {
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startLifetime = 1f;
                main.startSpeed = 10f;
                main.startSize = 0.5f;
                main.startColor = Color.red;
                
                var emission = particleSystem.emission;
                emission.rateOverTime = 0;
                emission.SetBursts(new ParticleSystem.Burst[]
                {
                    new ParticleSystem.Burst(0.0f, 100)
                });
                
                var shape = particleSystem.shape;
                shape.shapeType = ParticleSystemShapeType.Sphere;
                shape.radius = 0.1f;
                
                PlayParticles();
                Debug.Log("创建爆炸效果");
            }
        }
        
        /// <summary>
        /// 创建火焰效果
        /// </summary>
        public void CreateFireEffect()
        {
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startLifetime = 2f;
                main.startSpeed = 2f;
                main.startSize = 0.3f;
                main.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.red);
                
                var emission = particleSystem.emission;
                emission.rateOverTime = 50f;
                
                var shape = particleSystem.shape;
                shape.shapeType = ParticleSystemShapeType.Circle;
                shape.radius = 0.5f;
                
                PlayParticles();
                Debug.Log("创建火焰效果");
            }
        }
        
        /// <summary>
        /// 创建烟雾效果
        /// </summary>
        public void CreateSmokeEffect()
        {
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startLifetime = 3f;
                main.startSpeed = 1f;
                main.startSize = 1f;
                main.startColor = new ParticleSystem.MinMaxGradient(Color.gray, Color.black);
                
                var emission = particleSystem.emission;
                emission.rateOverTime = 20f;
                
                var shape = particleSystem.shape;
                shape.shapeType = ParticleSystemShapeType.Sphere;
                shape.radius = 0.2f;
                
                PlayParticles();
                Debug.Log("创建烟雾效果");
            }
        }
        
        /// <summary>
        /// 获取VFX信息
        /// </summary>
        public void GetVFXInfo()
        {
            Debug.Log("=== VFX信息 ===");
            
            if (visualEffect != null)
            {
                Debug.Log($"VFX资源: {(visualEffect.visualEffectAsset != null ? visualEffect.visualEffectAsset.name : "无")}");
                Debug.Log($"播放状态: {(visualEffect.isActive ? "活跃" : "非活跃")}");
                Debug.Log($"播放速率: {visualEffect.playRate}");
                Debug.Log($"暂停状态: {visualEffect.pause}");
            }
            
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                Debug.Log($"粒子系统: {particleSystem.name}");
                Debug.Log($"最大粒子数: {main.maxParticles}");
                Debug.Log($"当前粒子数: {particleSystem.particleCount}");
                Debug.Log($"是否播放中: {particleSystem.isPlaying}");
                Debug.Log($"是否暂停: {particleSystem.isPaused}");
            }
        }
        
        private void Update()
        {
            if (isPlaying)
            {
                currentTime += Time.deltaTime;
            }
        }
        
        private void OnGUI()
        {
            if (!showControls) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 400, 700));
            GUILayout.Label("UnityEngine.VFX 视觉效果系统案例", EditorStyles.boldLabel);
            
            GUILayout.Space(10);
            
            // VFX控制
            GUILayout.Label("VFX控制", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("播放VFX")) PlayVFX();
            if (GUILayout.Button("暂停VFX")) PauseVFX();
            if (GUILayout.Button("停止VFX")) StopVFX();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // 粒子系统控制
            GUILayout.Label("粒子系统控制", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("播放粒子")) PlayParticles();
            if (GUILayout.Button("暂停粒子")) PauseParticles();
            if (GUILayout.Button("停止粒子")) StopParticles();
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("清除粒子"))
            {
                ClearParticles();
            }
            
            GUILayout.Space(10);
            
            // 参数控制
            GUILayout.Label("参数控制", EditorStyles.boldLabel);
            
            float newEmissionRate = GUILayout.HorizontalSlider(emissionRate, 0f, 100f);
            if (Mathf.Abs(newEmissionRate - emissionRate) > 0.1f)
            {
                SetEmissionRate(newEmissionRate);
            }
            GUILayout.Label($"发射率: {emissionRate:F1}");
            
            float newLifetime = GUILayout.HorizontalSlider(particleLifetime, 0.1f, 10f);
            if (Mathf.Abs(newLifetime - particleLifetime) > 0.1f)
            {
                SetParticleLifetime(newLifetime);
            }
            GUILayout.Label($"生命周期: {particleLifetime:F1}秒");
            
            float newSpeed = GUILayout.HorizontalSlider(particleSpeed, 0f, 20f);
            if (Mathf.Abs(newSpeed - particleSpeed) > 0.1f)
            {
                SetParticleSpeed(newSpeed);
            }
            GUILayout.Label($"粒子速度: {particleSpeed:F1}");
            
            float newAnimationSpeed = GUILayout.HorizontalSlider(animationSpeed, 0.1f, 5f);
            if (Mathf.Abs(newAnimationSpeed - animationSpeed) > 0.1f)
            {
                SetAnimationSpeed(newAnimationSpeed);
            }
            GUILayout.Label($"动画速度: {animationSpeed:F1}x");
            
            GUILayout.Space(10);
            
            // 颜色控制
            GUILayout.Label("颜色控制", EditorStyles.boldLabel);
            Color newColor = particleColor;
            newColor.r = GUILayout.HorizontalSlider(newColor.r, 0f, 1f);
            newColor.g = GUILayout.HorizontalSlider(newColor.g, 0f, 1f);
            newColor.b = GUILayout.HorizontalSlider(newColor.b, 0f, 1f);
            newColor.a = GUILayout.HorizontalSlider(newColor.a, 0f, 1f);
            
            if (newColor != particleColor)
            {
                SetParticleColor(newColor);
            }
            
            GUILayout.Space(10);
            
            // 预设效果
            GUILayout.Label("预设效果", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("爆炸效果")) CreateExplosionEffect();
            if (GUILayout.Button("火焰效果")) CreateFireEffect();
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("烟雾效果"))
            {
                CreateSmokeEffect();
            }
            
            GUILayout.Space(10);
            
            // 变换控制
            GUILayout.Label("变换控制", EditorStyles.boldLabel);
            if (GUILayout.Button("重置变换"))
            {
                ResetTransform();
            }
            
            GUILayout.Space(10);
            
            // 信息显示
            GUILayout.Label("信息", EditorStyles.boldLabel);
            if (GUILayout.Button("获取VFX信息"))
            {
                GetVFXInfo();
            }
            
            GUILayout.Label($"播放时间: {currentTime:F1}秒");
            GUILayout.Label($"播放状态: {(isPlaying ? (isPaused ? "暂停" : "播放中") : "停止")}");
            
            if (particleSystem != null)
            {
                GUILayout.Label($"当前粒子数: {particleSystem.particleCount}");
            }
            
            GUILayout.EndArea();
        }
    }
} 