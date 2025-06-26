using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// UnityEngine.Pool 命名空间案例演示
/// 展示ObjectPool、CollectionPool等核心功能
/// </summary>
public class PoolExample : MonoBehaviour
{
    [Header("对象池设置")]
    [SerializeField] private GameObject prefab; //预制体
    [SerializeField] private int poolSize = 10; //池大小
    [SerializeField] private int spawnCount = 5; //生成数量
    [SerializeField] private bool usePool = true; //是否使用对象池

    private ObjectPool<GameObject> objectPool;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        if (prefab != null)
        {
            // 创建对象池
            objectPool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(prefab),
                actionOnGet: (obj) => {
                    obj.SetActive(true);
                    obj.transform.position = Random.insideUnitSphere * 5f;
                },
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: poolSize,
                maxSize: poolSize * 2
            );
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (usePool)
            {
                SpawnWithPool();
            }
            else
            {
                SpawnWithoutPool();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseAll();
        }
    }

    /// <summary>
    /// 使用对象池生成对象
    /// </summary>
    private void SpawnWithPool()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var obj = objectPool.Get();
            spawnedObjects.Add(obj);
        }
        Debug.Log($"使用对象池生成了 {spawnCount} 个对象");
    }

    /// <summary>
    /// 不使用对象池生成对象
    /// </summary>
    private void SpawnWithoutPool()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var obj = Instantiate(prefab);
            obj.transform.position = Random.insideUnitSphere * 5f;
            spawnedObjects.Add(obj);
        }
        Debug.Log($"不使用对象池生成了 {spawnCount} 个对象");
    }

    /// <summary>
    /// 释放所有对象
    /// </summary>
    private void ReleaseAll()
    {
        if (usePool)
        {
            foreach (var obj in spawnedObjects)
            {
                if (obj != null)
                {
                    objectPool.Release(obj);
                }
            }
            Debug.Log("已释放所有对象到对象池");
        }
        else
        {
            foreach (var obj in spawnedObjects)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
            Debug.Log("已销毁所有对象");
        }
        spawnedObjects.Clear();
    }

    private void OnDestroy()
    {
        if (objectPool != null)
        {
            objectPool.Dispose();
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 350, 250));
        GUILayout.Label("Object Pool 对象池演示", UnityEditor.EditorStyles.boldLabel);
        prefab = (GameObject)UnityEditor.EditorGUILayout.ObjectField("预制体", prefab, typeof(GameObject), true);
        poolSize = int.TryParse(GUILayout.TextField(poolSize.ToString()), out var size) ? size : poolSize;
        spawnCount = int.TryParse(GUILayout.TextField(spawnCount.ToString()), out var count) ? count : spawnCount;
        usePool = GUILayout.Toggle(usePool, "使用对象池");
        GUILayout.Label($"当前对象数量: {spawnedObjects.Count}");
        GUILayout.Label("按空格键生成对象，按R键释放对象");
        if (GUILayout.Button("生成对象"))
        {
            if (usePool)
            {
                SpawnWithPool();
            }
            else
            {
                SpawnWithoutPool();
            }
        }
        if (GUILayout.Button("释放所有对象"))
        {
            ReleaseAll();
        }
        GUILayout.EndArea();
    }
} 