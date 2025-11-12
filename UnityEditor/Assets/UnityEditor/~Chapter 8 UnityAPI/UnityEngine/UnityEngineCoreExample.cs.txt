// UnityEngineCoreExample.cs
// UnityEngine 核心系统示例
// 展示 GameObject、Transform、Component 等核心API的使用

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityAPI.UnityEngine.Core
{
    /// <summary>
    /// UnityEngine 核心系统示例
    /// 
    /// 涵盖内容：
    /// - GameObject 创建、销毁、查找
    /// - Transform 变换操作
    /// - Component 组件管理
    /// - 场景管理
    /// - 时间系统
    /// - 输入系统
    /// </summary>
    public class UnityEngineCoreExample : MonoBehaviour
    {
        [Header("游戏对象管理")]
        [SerializeField] private GameObject prefab;
        [SerializeField] private int objectCount = 10;
        [SerializeField] private float spawnRadius = 5f;
        
        [Header("变换操作")]
        [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 45, 0);
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private bool enableAutoRotation = true;
        
        [Header("组件管理")]
        [SerializeField] private bool enableRigidbody = true;
        [SerializeField] private bool enableCollider = true;
        [SerializeField] private bool enableRenderer = true;
        
        private List<GameObject> spawnedObjects;
        private Camera mainCamera;
        private Rigidbody playerRigidbody;
        
        void Start()
        {
            InitializeCoreSystem();
            SpawnObjects();
            SetupPlayer();
        }
        
        void Update()
        {
            HandleInput();
            UpdateObjects();
            UpdatePlayer();
        }
        
        /// <summary>
        /// 初始化核心系统
        /// </summary>
        private void InitializeCoreSystem()
        {
            Debug.Log("初始化UnityEngine核心系统");
            
            // 获取主相机
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }
            
            // 初始化对象列表
            spawnedObjects = new List<GameObject>();
            
            // 设置时间缩放
            Time.timeScale = 1f;
            
            Debug.Log($"时间缩放: {Time.timeScale}");
            Debug.Log($"帧率: {1f / Time.deltaTime:F1} FPS");
        }
        
        /// <summary>
        /// 生成游戏对象
        /// </summary>
        private void SpawnObjects()
        {
            Debug.Log($"生成 {objectCount} 个游戏对象");
            
            for (int i = 0; i < objectCount; i++)
            {
                GameObject obj;
                
                if (prefab != null)
                {
                    // 使用预制体创建
                    obj = Instantiate(prefab);
                }
                else
                {
                    // 创建基础游戏对象
                    obj = new GameObject($"SpawnedObject_{i}");
                }
                
                // 设置位置
                Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
                randomPosition.y = Mathf.Abs(randomPosition.y); // 确保在地面上
                obj.transform.position = randomPosition;
                
                // 添加组件
                AddComponents(obj);
                
                // 设置随机颜色
                SetRandomColor(obj);
                
                spawnedObjects.Add(obj);
            }
            
            Debug.Log($"成功生成 {spawnedObjects.Count} 个游戏对象");
        }
        
        /// <summary>
        /// 添加组件
        /// </summary>
        private void AddComponents(GameObject obj)
        {
            // 添加刚体组件
            if (enableRigidbody && obj.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = obj.AddComponent<Rigidbody>();
                rb.mass = Random.Range(0.5f, 2f);
                rb.drag = Random.Range(0.1f, 1f);
            }
            
            // 添加碰撞器组件
            if (enableCollider && obj.GetComponent<Collider>() == null)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.AddComponent<MeshCollider>();
                }
                else
                {
                    obj.AddComponent<BoxCollider>();
                }
            }
            
            // 添加渲染器组件
            if (enableRenderer && obj.GetComponent<Renderer>() == null)
            {
                MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
                MeshFilter filter = obj.AddComponent<MeshFilter>();
                
                // 创建基础几何体
                filter.mesh = CreatePrimitiveMesh();
                
                // 设置材质
                Material material = new Material(Shader.Find("Standard"));
                material.color = Random.ColorHSV();
                renderer.material = material;
            }
        }
        
        /// <summary>
        /// 创建基础几何体网格
        /// </summary>
        private Mesh CreatePrimitiveMesh()
        {
            // 这里可以创建各种基础几何体
            // 为了简化，我们使用Unity的内置几何体
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Mesh mesh = temp.GetComponent<MeshFilter>().mesh;
            DestroyImmediate(temp);
            return mesh;
        }
        
        /// <summary>
        /// 设置随机颜色
        /// </summary>
        private void SetRandomColor(GameObject obj)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Random.ColorHSV();
            }
        }
        
        /// <summary>
        /// 设置玩家
        /// </summary>
        private void SetupPlayer()
        {
            // 创建玩家对象
            GameObject player = new GameObject("Player");
            player.transform.position = Vector3.zero;
            
            // 添加刚体
            playerRigidbody = player.AddComponent<Rigidbody>();
            playerRigidbody.mass = 1f;
            playerRigidbody.drag = 5f;
            playerRigidbody.freezeRotation = true;
            
            // 添加碰撞器
            CapsuleCollider collider = player.AddComponent<CapsuleCollider>();
            collider.height = 2f;
            collider.radius = 0.5f;
            
            // 添加渲染器
            MeshRenderer renderer = player.AddComponent<MeshRenderer>();
            MeshFilter filter = player.AddComponent<MeshFilter>();
            filter.mesh = CreatePrimitiveMesh();
            
            // 设置材质
            Material material = new Material(Shader.Find("Standard"));
            material.color = Color.blue;
            renderer.material = material;
            
            Debug.Log("玩家设置完成");
        }
        
        /// <summary>
        /// 处理输入
        /// </summary>
        private void HandleInput()
        {
            // 键盘输入
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnObjects();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                ClearAllObjects();
            }
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                ToggleTimeScale();
            }
            
            // 鼠标输入
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit))
                {
                    OnObjectClicked(hit.collider.gameObject);
                }
            }
        }
        
        /// <summary>
        /// 更新对象
        /// </summary>
        private void UpdateObjects()
        {
            if (!enableAutoRotation) return;
            
            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null)
                {
                    // 旋转
                    obj.transform.Rotate(rotationSpeed * Time.deltaTime);
                    
                    // 上下浮动
                    float yOffset = Mathf.Sin(Time.time + obj.GetInstanceID()) * 0.5f;
                    Vector3 pos = obj.transform.position;
                    pos.y += yOffset * Time.deltaTime;
                    obj.transform.position = pos;
                }
            }
        }
        
        /// <summary>
        /// 更新玩家
        /// </summary>
        private void UpdatePlayer()
        {
            if (playerRigidbody == null) return;
            
            // 移动输入
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed;
            playerRigidbody.velocity = new Vector3(movement.x, playerRigidbody.velocity.y, movement.z);
        }
        
        /// <summary>
        /// 对象被点击
        /// </summary>
        private void OnObjectClicked(GameObject obj)
        {
            Debug.Log($"点击了对象: {obj.name}");
            
            // 改变颜色
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Random.ColorHSV();
            }
            
            // 添加力
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 force = Random.insideUnitSphere * 10f;
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
        
        /// <summary>
        /// 清除所有对象
        /// </summary>
        private void ClearAllObjects()
        {
            Debug.Log("清除所有生成的对象");
            
            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
            
            spawnedObjects.Clear();
        }
        
        /// <summary>
        /// 切换时间缩放
        /// </summary>
        private void ToggleTimeScale()
        {
            Time.timeScale = Time.timeScale == 1f ? 0.5f : 1f;
            Debug.Log($"时间缩放: {Time.timeScale}");
        }
        
        /// <summary>
        /// 获取场景信息
        /// </summary>
        public void GetSceneInfo()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            Debug.Log($"当前场景: {currentScene.name}");
            Debug.Log($"场景路径: {currentScene.path}");
            Debug.Log($"场景是否已加载: {currentScene.isLoaded}");
            
            // 获取场景中的所有游戏对象
            GameObject[] allObjects = currentScene.GetRootGameObjects();
            Debug.Log($"场景根对象数量: {allObjects.Length}");
        }
        
        /// <summary>
        /// 查找游戏对象
        /// </summary>
        public void FindGameObjects()
        {
            // 按名称查找
            GameObject foundByName = GameObject.Find("Player");
            if (foundByName != null)
            {
                Debug.Log($"按名称找到: {foundByName.name}");
            }
            
            // 按标签查找
            GameObject[] foundByTag = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log($"按标签找到 {foundByTag.Length} 个对象");
            
            // 按类型查找
            Camera[] cameras = FindObjectsOfType<Camera>();
            Debug.Log($"找到 {cameras.Length} 个相机");
            
            // 按组件查找
            Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
            Debug.Log($"找到 {rigidbodies.Length} 个刚体");
        }
        
        /// <summary>
        /// 组件操作示例
        /// </summary>
        public void ComponentOperations()
        {
            GameObject obj = new GameObject("ComponentTest");
            
            // 添加组件
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            BoxCollider collider = obj.AddComponent<BoxCollider>();
            
            // 检查组件
            bool hasRigidbody = obj.GetComponent<Rigidbody>() != null;
            bool hasCollider = obj.GetComponent<Collider>() != null;
            
            Debug.Log($"有刚体: {hasRigidbody}, 有碰撞器: {hasCollider}");
            
            // 获取所有组件
            Component[] allComponents = obj.GetComponents<Component>();
            Debug.Log($"对象有 {allComponents.Length} 个组件");
            
            // 移除组件
            DestroyImmediate(rb);
            DestroyImmediate(collider);
            
            Debug.Log("组件操作完成");
        }
        
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 200));
            
            GUILayout.Label("UnityEngine 核心系统示例");
            GUILayout.Label($"生成对象数量: {spawnedObjects.Count}");
            GUILayout.Label($"时间缩放: {Time.timeScale:F1}");
            GUILayout.Label($"帧率: {1f / Time.deltaTime:F1} FPS");
            
            if (GUILayout.Button("生成对象"))
            {
                SpawnObjects();
            }
            
            if (GUILayout.Button("清除对象"))
            {
                ClearAllObjects();
            }
            
            if (GUILayout.Button("切换时间缩放"))
            {
                ToggleTimeScale();
            }
            
            if (GUILayout.Button("获取场景信息"))
            {
                GetSceneInfo();
            }
            
            if (GUILayout.Button("查找游戏对象"))
            {
                FindGameObjects();
            }
            
            if (GUILayout.Button("组件操作"))
            {
                ComponentOperations();
            }
            
            GUILayout.EndArea();
        }
    }
}
