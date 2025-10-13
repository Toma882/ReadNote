using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.SceneManagement;

namespace UnityEditor.Chapter9Utility
{
    /// <summary>
    /// Unity Utility Classes 综合示例
    /// 展示Unity中所有主要Utility工具类的使用方法
    /// </summary>
    public class UtilityExample : EditorWindow
    {
        #region 预制体工具示例

        /// <summary>
        /// PrefabUtility 预制体工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/PrefabUtility Example")]
        public static void PrefabUtilityExample()
        {
            Debug.Log("=== PrefabUtility 示例 ===");
            
            // 获取选中的游戏对象
            GameObject selectedObject = Selection.activeGameObject;
            if (selectedObject == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            // 检查是否为预制体实例
            bool isPrefabInstance = PrefabUtility.IsPartOfPrefabInstance(selectedObject);
            Debug.Log($"是否为预制体实例: {isPrefabInstance}");
            
            if (isPrefabInstance)
            {
                // 获取预制体资源路径
                string prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(selectedObject);
                Debug.Log($"预制体路径: {prefabPath}");
                
                // 获取预制体实例状态
                PrefabInstanceStatus status = PrefabUtility.GetPrefabInstanceStatus(selectedObject);
                Debug.Log($"预制体状态: {status}");
                
                // 应用预制体修改
                if (status == PrefabInstanceStatus.Modified)
                {
                    PrefabUtility.ApplyPrefabInstance(selectedObject, InteractionMode.UserAction);
                    Debug.Log("已应用预制体修改");
                }
                
                // 还原预制体修改
                if (status == PrefabInstanceStatus.Modified)
                {
                    PrefabUtility.RevertPrefabInstance(selectedObject, InteractionMode.UserAction);
                    Debug.Log("已还原预制体修改");
                }
            }
            else
            {
                // 保存为预制体
                string path = EditorUtility.SaveFilePanelInProject(
                    "保存预制体", 
                    selectedObject.name, 
                    "prefab", 
                    "选择保存位置");
                
                if (!string.IsNullOrEmpty(path))
                {
                    GameObject prefab = PrefabUtility.SaveAsPrefabAsset(selectedObject, path);
                    Debug.Log($"已保存预制体: {prefab.name}");
                }
            }
        }

        #endregion

        #region 编辑器工具示例

        /// <summary>
        /// EditorUtility 编辑器工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/EditorUtility Example")]
        public static void EditorUtilityExample()
        {
            Debug.Log("=== EditorUtility 示例 ===");
            
            // 显示对话框
            bool result = EditorUtility.DisplayDialog(
                "确认操作", 
                "是否继续执行此操作？", 
                "确定", 
                "取消");
            
            Debug.Log($"对话框结果: {result}");
            
            // 显示复杂对话框
            int option = EditorUtility.DisplayDialogComplex(
                "选择操作", 
                "请选择要执行的操作：", 
                "操作1", 
                "操作2", 
                "取消");
            
            Debug.Log($"复杂对话框结果: {option}");
            
            // 文件对话框
            string filePath = EditorUtility.OpenFilePanel(
                "选择文件", 
                Application.dataPath, 
                "cs");
            
            if (!string.IsNullOrEmpty(filePath))
            {
                Debug.Log($"选择的文件: {filePath}");
            }
            
            // 保存文件对话框
            string savePath = EditorUtility.SaveFilePanel(
                "保存文件", 
                Application.dataPath, 
                "MyFile", 
                "txt");
            
            if (!string.IsNullOrEmpty(savePath))
            {
                Debug.Log($"保存路径: {savePath}");
            }
            
            // 进度条示例
            for (int i = 0; i <= 100; i++)
            {
                EditorUtility.DisplayProgressBar("处理中", $"进度: {i}%", i / 100f);
                System.Threading.Thread.Sleep(10);
            }
            EditorUtility.ClearProgressBar();
            
            Debug.Log("进度条示例完成");
        }

        /// <summary>
        /// EditorGUIUtility 编辑器GUI工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/EditorGUIUtility Example")]
        public static void EditorGUIUtilityExample()
        {
            Debug.Log("=== EditorGUIUtility 示例 ===");
            
            // 获取对象内容
            GameObject selectedObject = Selection.activeGameObject;
            if (selectedObject != null)
            {
                GUIContent content = EditorGUIUtility.ObjectContent(selectedObject, typeof(GameObject));
                Debug.Log($"对象内容: {content.text}, 图标: {content.image}");
            }
            
            // 设置图标大小
            EditorGUIUtility.SetIconSize(new Vector2(32, 32));
            Debug.Log("图标大小已设置为 32x32");
            
            // 加载必需资源
            Texture2D icon = EditorGUIUtility.LoadRequired("Icons/UnityLogo.png") as Texture2D;
            if (icon != null)
            {
                Debug.Log($"加载的图标: {icon.name}");
            }
            
            // 系统复制缓冲区
            EditorGUIUtility.systemCopyBuffer = "这是复制到系统剪贴板的内容";
            Debug.Log($"系统剪贴板内容: {EditorGUIUtility.systemCopyBuffer}");
            
            // 重置图标大小
            EditorGUIUtility.SetIconSize(Vector2.zero);
        }

        #endregion

        #region GUI工具示例

        /// <summary>
        /// GUIUtility GUI工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/GUIUtility Example")]
        public static void GUIUtilityExample()
        {
            Debug.Log("=== GUIUtility 示例 ===");
            
            // 坐标转换
            Vector2 screenPoint = Input.mousePosition;
            Vector2 guiPoint = GUIUtility.ScreenToGUIPoint(screenPoint);
            Vector2 backToScreen = GUIUtility.GUIToScreenPoint(guiPoint);
            
            Debug.Log($"屏幕坐标: {screenPoint}");
            Debug.Log($"GUI坐标: {guiPoint}");
            Debug.Log($"转回屏幕坐标: {backToScreen}");
            
            // 获取控件ID
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            Debug.Log($"控件ID: {controlID}");
            
            // 键盘控制
            Debug.Log($"当前键盘控制ID: {GUIUtility.keyboardControl}");
            
            // 热键控制
            Event currentEvent = Event.current;
            if (currentEvent != null)
            {
                Debug.Log($"当前事件类型: {currentEvent.type}");
            }
        }

        /// <summary>
        /// GUILayoutUtility GUI布局工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/GUILayoutUtility Example")]
        public static void GUILayoutUtilityExample()
        {
            Debug.Log("=== GUILayoutUtility 示例 ===");
            
            // 获取布局矩形
            Rect layoutRect = GUILayoutUtility.GetRect(200, 50);
            Debug.Log($"布局矩形: {layoutRect}");
            
            // 获取最后矩形
            Rect lastRect = GUILayoutUtility.GetLastRect();
            Debug.Log($"最后矩形: {lastRect}");
            
            // 获取Aspect矩形
            Rect aspectRect = GUILayoutUtility.GetAspectRect(16f / 9f);
            Debug.Log($"宽高比矩形: {aspectRect}");
        }

        #endregion

        #region JSON工具示例

        /// <summary>
        /// JsonUtility JSON工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/JsonUtility Example")]
        public static void JsonUtilityExample()
        {
            Debug.Log("=== JsonUtility 示例 ===");
            
            // 创建测试数据
            var testData = new TestData
            {
                name = "测试对象",
                value = 42,
                position = new Vector3(1, 2, 3),
                items = new List<string> { "项目1", "项目2", "项目3" }
            };
            
            // 转换为JSON
            string json = JsonUtility.ToJson(testData, true);
            Debug.Log($"JSON字符串:\n{json}");
            
            // 从JSON转换
            TestData deserializedData = JsonUtility.FromJson<TestData>(json);
            Debug.Log($"反序列化数据: {deserializedData.name}, {deserializedData.value}");
            
            // 从JSON覆盖现有对象
            var newData = new TestData();
            JsonUtility.FromJsonOverwrite(json, newData);
            Debug.Log($"覆盖后数据: {newData.name}, {newData.value}");
        }

        /// <summary>
        /// EditorJsonUtility 编辑器JSON工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/EditorJsonUtility Example")]
        public static void EditorJsonUtilityExample()
        {
            Debug.Log("=== EditorJsonUtility 示例 ===");
            
            // 获取选中的对象
            UnityEngine.Object selectedObject = Selection.activeObject;
            if (selectedObject == null)
            {
                Debug.LogWarning("请先选择一个对象");
                return;
            }
            
            // 转换为JSON
            string json = EditorJsonUtility.ToJson(selectedObject, true);
            Debug.Log($"对象JSON:\n{json}");
            
            // 从JSON覆盖
            EditorJsonUtility.FromJsonOverwrite(json, selectedObject);
            Debug.Log("已从JSON覆盖对象数据");
            
            // 标记对象为脏
            EditorUtility.SetDirty(selectedObject);
        }

        #endregion

        #region 几何工具示例

        /// <summary>
        /// GeometryUtility 几何工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/GeometryUtility Example")]
        public static void GeometryUtilityExample()
        {
            Debug.Log("=== GeometryUtility 示例 ===");
            
            // 计算视锥平面
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
                Debug.Log($"视锥平面数量: {frustumPlanes.Length}");
                
                for (int i = 0; i < frustumPlanes.Length; i++)
                {
                    Debug.Log($"平面 {i}: {frustumPlanes[i].normal}, 距离: {frustumPlanes[i].distance}");
                }
                
                // 测试包围盒
                Renderer[] renderers = FindObjectsOfType<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    bool isVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
                    Debug.Log($"对象 {renderer.name} 是否在视锥内: {isVisible}");
                }
            }
        }

        /// <summary>
        /// RectTransformUtility 矩形变换工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/RectTransformUtility Example")]
        public static void RectTransformUtilityExample()
        {
            Debug.Log("=== RectTransformUtility 示例 ===");
            
            // 查找Canvas
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogWarning("场景中没有找到Canvas");
                return;
            }
            
            // 查找RectTransform
            RectTransform[] rectTransforms = FindObjectsOfType<RectTransform>();
            foreach (RectTransform rectTransform in rectTransforms)
            {
                if (rectTransform == canvas.transform) continue;
                
                // 屏幕坐标转本地坐标
                Vector2 localPoint;
                bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rectTransform, 
                    Input.mousePosition, 
                    canvas.worldCamera, 
                    out localPoint);
                
                if (success)
                {
                    Debug.Log($"对象 {rectTransform.name} 本地坐标: {localPoint}");
                }
                
                // 本地坐标转屏幕坐标
                Vector2 screenPoint = RectTransformUtility.LocalPointToScreenPoint(
                    canvas.worldCamera, 
                    localPoint);
                
                Debug.Log($"屏幕坐标: {screenPoint}");
                
                // 翻转布局
                RectTransformUtility.FlipLayoutOnAxis(rectTransform, 0, true, true);
                Debug.Log($"已翻转对象 {rectTransform.name} 的X轴布局");
            }
        }

        #endregion

        #region 动画工具示例

        /// <summary>
        /// AnimationUtility 动画工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/AnimationUtility Example")]
        public static void AnimationUtilityExample()
        {
            Debug.Log("=== AnimationUtility 示例 ===");
            
            // 获取选中的游戏对象
            GameObject selectedObject = Selection.activeGameObject;
            if (selectedObject == null)
            {
                Debug.LogWarning("请先选择一个游戏对象");
                return;
            }
            
            // 获取动画剪辑
            AnimationClip[] clips = AnimationUtility.GetAnimationClips(selectedObject);
            Debug.Log($"对象 {selectedObject.name} 的动画剪辑数量: {clips.Length}");
            
            foreach (AnimationClip clip in clips)
            {
                Debug.Log($"动画剪辑: {clip.name}");
                
                // 获取动画事件
                AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
                Debug.Log($"动画事件数量: {events.Length}");
                
                foreach (AnimationEvent animEvent in events)
                {
                    Debug.Log($"事件: {animEvent.functionName} 在时间 {animEvent.time}");
                }
                
                // 获取动画属性
                EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
                Debug.Log($"动画属性数量: {bindings.Length}");
                
                foreach (EditorCurveBinding binding in bindings)
                {
                    Debug.Log($"属性: {binding.propertyName} 路径: {binding.path}");
                }
            }
        }

        #endregion

        #region 地形工具示例

        /// <summary>
        /// TerrainUtility 地形工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/TerrainUtility Example")]
        public static void TerrainUtilityExample()
        {
            Debug.Log("=== TerrainUtility 示例 ===");
            
            // 查找所有地形
            Terrain[] terrains = FindObjectsOfType<Terrain>();
            Debug.Log($"场景中地形数量: {terrains.Length}");
            
            if (terrains.Length > 0)
            {
                // 自动连接地形
                TerrainUtility.AutoConnect(terrains);
                Debug.Log("已自动连接所有地形");
                
                // 有效地形检查
                TerrainUtility.ValidTerrainsCheck();
                Debug.Log("已完成有效地形检查");
            }
        }

        #endregion

        #region 着色器工具示例

        /// <summary>
        /// ShaderUtil 着色器工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/ShaderUtil Example")]
        public static void ShaderUtilExample()
        {
            Debug.Log("=== ShaderUtil 示例 ===");
            
            // 查找所有材质
            Material[] materials = FindObjectsOfType<Material>();
            foreach (Material material in materials)
            {
                Shader shader = material.shader;
                if (shader == null) continue;
                
                Debug.Log($"材质 {material.name} 使用着色器: {shader.name}");
                
                // 获取着色器属性数量
                int propertyCount = ShaderUtil.GetPropertyCount(shader);
                Debug.Log($"着色器属性数量: {propertyCount}");
                
                // 获取属性信息
                for (int i = 0; i < propertyCount; i++)
                {
                    string propertyName = ShaderUtil.GetPropertyName(shader, i);
                    ShaderUtil.ShaderPropertyType propertyType = ShaderUtil.GetPropertyType(shader, i);
                    
                    Debug.Log($"属性 {i}: {propertyName} 类型: {propertyType}");
                }
            }
        }

        #endregion

        #region 内存工具示例

        /// <summary>
        /// UnsafeUtility 不安全工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/UnsafeUtility Example")]
        public static void UnsafeUtilityExample()
        {
            Debug.Log("=== UnsafeUtility 示例 ===");
            
            // 分配内存
            int size = 1024;
            IntPtr ptr = UnsafeUtility.Malloc(size, 4, Allocator.Persistent);
            Debug.Log($"分配内存: {size} 字节, 指针: {ptr}");
            
            // 内存复制
            byte[] sourceData = new byte[] { 1, 2, 3, 4, 5 };
            UnsafeUtility.MemCpy(ptr, sourceData, sourceData.Length);
            Debug.Log("已复制数据到分配的内存");
            
            // 读取内存
            byte[] readData = new byte[sourceData.Length];
            UnsafeUtility.MemCpy(readData, ptr, sourceData.Length);
            Debug.Log($"读取的数据: [{string.Join(", ", readData)}]");
            
            // 释放内存
            UnsafeUtility.Free(ptr, Allocator.Persistent);
            Debug.Log("已释放内存");
        }

        #endregion

        #region 游戏对象工具示例

        /// <summary>
        /// GameObjectUtility 游戏对象工具示例
        /// </summary>
        [MenuItem("Tools/Utility Examples/GameObjectUtility Example")]
        public static void GameObjectUtilityExample()
        {
            Debug.Log("=== GameObjectUtility 示例 ===");
            
            // 获取导航网格区域名称
            string[] areaNames = GameObjectUtility.GetNavMeshAreaNames();
            Debug.Log($"导航网格区域数量: {areaNames.Length}");
            
            foreach (string areaName in areaNames)
            {
                int areaIndex = GameObjectUtility.GetNavMeshAreaFromName(areaName);
                Debug.Log($"区域: {areaName}, 索引: {areaIndex}");
            }
        }

        #endregion

        #region 测试数据类

        [System.Serializable]
        public class TestData
        {
            public string name;
            public int value;
            public Vector3 position;
            public List<string> items;
        }

        #endregion

        #region 窗口显示

        [MenuItem("Tools/Utility Examples/Show Utility Window")]
        public static void ShowWindow()
        {
            UtilityExample window = GetWindow<UtilityExample>("Utility Examples");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Unity Utility Classes 示例", EditorStyles.boldLabel);
            
            if (GUILayout.Button("PrefabUtility 示例"))
            {
                PrefabUtilityExample();
            }
            
            if (GUILayout.Button("EditorUtility 示例"))
            {
                EditorUtilityExample();
            }
            
            if (GUILayout.Button("JsonUtility 示例"))
            {
                JsonUtilityExample();
            }
            
            if (GUILayout.Button("GeometryUtility 示例"))
            {
                GeometryUtilityExample();
            }
            
            if (GUILayout.Button("AnimationUtility 示例"))
            {
                AnimationUtilityExample();
            }
            
            if (GUILayout.Button("ShaderUtil 示例"))
            {
                ShaderUtilExample();
            }
        }

        #endregion
    }
}
