using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEditor.Chapter9Utility.GeometryUtility
{
    /// <summary>
    /// GeometryUtility 和 RectTransformUtility 详细示例
    /// 展示几何计算和矩形变换的各种用法
    /// </summary>
    public class GeometryUtilityExample : EditorWindow
    {
        private Vector2 scrollPosition;
        private Camera targetCamera;
        private bool showFrustumPlanes = true;
        private bool showBounds = true;
        private Color frustumColor = Color.yellow;
        private Color boundsColor = Color.green;

        [MenuItem("Tools/Utility Examples/GeometryUtility Detailed Example")]
        public static void ShowWindow()
        {
            GeometryUtilityExample window = GetWindow<GeometryUtilityExample>("GeometryUtility 示例");
            window.Show();
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            GUILayout.Label("GeometryUtility 和 RectTransformUtility 示例", EditorStyles.boldLabel);
            
            EditorGUILayout.Space();
            
            // 相机设置
            EditorGUILayout.LabelField("相机设置:", EditorStyles.boldLabel);
            targetCamera = (Camera)EditorGUILayout.ObjectField("目标相机", targetCamera, typeof(Camera), true);
            
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }
            
            EditorGUILayout.Space();
            
            // 视锥平面操作
            EditorGUILayout.LabelField("视锥平面操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("计算视锥平面"))
            {
                CalculateFrustumPlanes();
            }
            
            if (GUILayout.Button("测试所有渲染器"))
            {
                TestAllRenderers();
            }
            
            if (GUILayout.Button("测试选中对象"))
            {
                TestSelectedObjects();
            }
            
            EditorGUILayout.Space();
            
            // 显示设置
            EditorGUILayout.LabelField("显示设置:", EditorStyles.boldLabel);
            showFrustumPlanes = EditorGUILayout.Toggle("显示视锥平面", showFrustumPlanes);
            showBounds = EditorGUILayout.Toggle("显示包围盒", showBounds);
            frustumColor = EditorGUILayout.ColorField("视锥颜色", frustumColor);
            boundsColor = EditorGUILayout.ColorField("包围盒颜色", boundsColor);
            
            EditorGUILayout.Space();
            
            // RectTransform 操作
            EditorGUILayout.LabelField("RectTransform 操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("坐标转换测试"))
            {
                CoordinateConversionTest();
            }
            
            if (GUILayout.Button("布局翻转测试"))
            {
                LayoutFlipTest();
            }
            
            if (GUILayout.Button("矩形计算测试"))
            {
                RectangleCalculationTest();
            }
            
            EditorGUILayout.Space();
            
            // 高级几何操作
            EditorGUILayout.LabelField("高级几何操作:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("包围盒相交测试"))
            {
                BoundsIntersectionTest();
            }
            
            if (GUILayout.Button("平面距离计算"))
            {
                PlaneDistanceCalculation();
            }
            
            if (GUILayout.Button("视锥剔除优化"))
            {
                FrustumCullingOptimization();
            }
            
            EditorGUILayout.EndScrollView();
        }

        #region 视锥平面操作

        /// <summary>
        /// 计算视锥平面
        /// </summary>
        private void CalculateFrustumPlanes()
        {
            if (targetCamera == null)
            {
                Debug.LogWarning("没有设置目标相机");
                return;
            }
            
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
            
            Debug.Log($"相机 {targetCamera.name} 的视锥平面:");
            for (int i = 0; i < frustumPlanes.Length; i++)
            {
                Plane plane = frustumPlanes[i];
                Debug.Log($"平面 {i}: 法向量={plane.normal}, 距离={plane.distance:F2}");
            }
            
            // 在场景视图中绘制
            SceneView.duringSceneGui += OnSceneGUI;
        }

        /// <summary>
        /// 测试所有渲染器
        /// </summary>
        private void TestAllRenderers()
        {
            if (targetCamera == null)
            {
                Debug.LogWarning("没有设置目标相机");
                return;
            }
            
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
            Renderer[] renderers = FindObjectsOfType<Renderer>();
            
            int visibleCount = 0;
            int totalCount = renderers.Length;
            
            Debug.Log($"测试 {totalCount} 个渲染器:");
            
            foreach (Renderer renderer in renderers)
            {
                bool isVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
                
                if (isVisible)
                {
                    visibleCount++;
                    Debug.Log($"✓ {renderer.name} - 在视锥内");
                }
                else
                {
                    Debug.Log($"✗ {renderer.name} - 在视锥外");
                }
            }
            
            Debug.Log($"可见渲染器: {visibleCount}/{totalCount} ({visibleCount * 100f / totalCount:F1}%)");
        }

        /// <summary>
        /// 测试选中对象
        /// </summary>
        private void TestSelectedObjects()
        {
            if (targetCamera == null)
            {
                Debug.LogWarning("没有设置目标相机");
                return;
            }
            
            GameObject[] selectedObjects = Selection.gameObjects;
            
            if (selectedObjects.Length == 0)
            {
                Debug.LogWarning("请先选择一些游戏对象");
                return;
            }
            
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
            
            Debug.Log($"测试 {selectedObjects.Length} 个选中对象:");
            
            foreach (GameObject obj in selectedObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    bool isVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
                    Debug.Log($"对象 {obj.name} - {(isVisible ? "可见" : "不可见")}");
                }
                else
                {
                    Debug.Log($"对象 {obj.name} - 没有Renderer组件");
                }
            }
        }

        #endregion

        #region RectTransform 操作

        /// <summary>
        /// 坐标转换测试
        /// </summary>
        private void CoordinateConversionTest()
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogWarning("场景中没有找到Canvas");
                return;
            }
            
            RectTransform[] rectTransforms = FindObjectsOfType<RectTransform>();
            
            Debug.Log("坐标转换测试:");
            
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
                    Debug.Log($"对象 {rectTransform.name}:");
                    Debug.Log($"  屏幕坐标: {Input.mousePosition}");
                    Debug.Log($"  本地坐标: {localPoint}");
                    
                    // 本地坐标转屏幕坐标
                    Vector2 screenPoint = RectTransformUtility.LocalPointToScreenPoint(
                        canvas.worldCamera,
                        localPoint);
                    Debug.Log($"  转回屏幕坐标: {screenPoint}");
                }
            }
        }

        /// <summary>
        /// 布局翻转测试
        /// </summary>
        private void LayoutFlipTest()
        {
            RectTransform[] rectTransforms = FindObjectsOfType<RectTransform>();
            
            Debug.Log("布局翻转测试:");
            
            foreach (RectTransform rectTransform in rectTransforms)
            {
                Vector2 originalSize = rectTransform.sizeDelta;
                
                // 翻转X轴
                RectTransformUtility.FlipLayoutOnAxis(rectTransform, 0, true, true);
                Debug.Log($"对象 {rectTransform.name} X轴翻转完成");
                
                // 翻转Y轴
                RectTransformUtility.FlipLayoutOnAxis(rectTransform, 1, true, true);
                Debug.Log($"对象 {rectTransform.name} Y轴翻转完成");
            }
        }

        /// <summary>
        /// 矩形计算测试
        /// </summary>
        private void RectangleCalculationTest()
        {
            RectTransform[] rectTransforms = FindObjectsOfType<RectTransform>();
            
            Debug.Log("矩形计算测试:");
            
            foreach (RectTransform rectTransform in rectTransforms)
            {
                Rect rect = rectTransform.rect;
                Vector2 size = rectTransform.sizeDelta;
                Vector2 anchoredPosition = rectTransform.anchoredPosition;
                
                Debug.Log($"对象 {rectTransform.name}:");
                Debug.Log($"  矩形: {rect}");
                Debug.Log($"  大小: {size}");
                Debug.Log($"  锚点位置: {anchoredPosition}");
                Debug.Log($"  世界位置: {rectTransform.position}");
            }
        }

        #endregion

        #region 高级几何操作

        /// <summary>
        /// 包围盒相交测试
        /// </summary>
        private void BoundsIntersectionTest()
        {
            Renderer[] renderers = FindObjectsOfType<Renderer>();
            
            if (renderers.Length < 2)
            {
                Debug.LogWarning("场景中需要至少2个Renderer进行相交测试");
                return;
            }
            
            Debug.Log("包围盒相交测试:");
            
            for (int i = 0; i < renderers.Length; i++)
            {
                for (int j = i + 1; j < renderers.Length; j++)
                {
                    Renderer renderer1 = renderers[i];
                    Renderer renderer2 = renderers[j];
                    
                    bool intersects = renderer1.bounds.Intersects(renderer2.bounds);
                    
                    Debug.Log($"对象 {renderer1.name} 和 {renderer2.name}: {(intersects ? "相交" : "不相交")}");
                }
            }
        }

        /// <summary>
        /// 平面距离计算
        /// </summary>
        private void PlaneDistanceCalculation()
        {
            if (targetCamera == null)
            {
                Debug.LogWarning("没有设置目标相机");
                return;
            }
            
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
            Transform[] transforms = FindObjectsOfType<Transform>();
            
            Debug.Log("平面距离计算:");
            
            foreach (Transform transform in transforms)
            {
                Vector3 position = transform.position;
                
                Debug.Log($"对象 {transform.name} 位置: {position}");
                
                for (int i = 0; i < frustumPlanes.Length; i++)
                {
                    float distance = frustumPlanes[i].GetDistanceToPoint(position);
                    Debug.Log($"  到平面 {i} 的距离: {distance:F2}");
                }
            }
        }

        /// <summary>
        /// 视锥剔除优化
        /// </summary>
        private void FrustumCullingOptimization()
        {
            if (targetCamera == null)
            {
                Debug.LogWarning("没有设置目标相机");
                return;
            }
            
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
            Renderer[] renderers = FindObjectsOfType<Renderer>();
            
            Debug.Log("视锥剔除优化:");
            
            List<Renderer> visibleRenderers = new List<Renderer>();
            List<Renderer> culledRenderers = new List<Renderer>();
            
            foreach (Renderer renderer in renderers)
            {
                bool isVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
                
                if (isVisible)
                {
                    visibleRenderers.Add(renderer);
                }
                else
                {
                    culledRenderers.Add(renderer);
                }
            }
            
            Debug.Log($"可见渲染器: {visibleRenderers.Count}");
            Debug.Log($"剔除渲染器: {culledRenderers.Count}");
            Debug.Log($"剔除率: {culledRenderers.Count * 100f / renderers.Length:F1}%");
            
            // 优化建议
            if (culledRenderers.Count > renderers.Length * 0.5f)
            {
                Debug.Log("建议: 超过50%的对象被剔除，考虑调整相机位置或优化场景");
            }
        }

        #endregion

        #region 场景绘制

        /// <summary>
        /// 场景GUI绘制
        /// </summary>
        private void OnSceneGUI(SceneView sceneView)
        {
            if (targetCamera == null) return;
            
            Handles.color = frustumColor;
            
            // 绘制视锥平面
            if (showFrustumPlanes)
            {
                Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
                
                for (int i = 0; i < frustumPlanes.Length; i++)
                {
                    Plane plane = frustumPlanes[i];
                    Vector3 center = plane.normal * plane.distance;
                    
                    // 绘制平面法向量
                    Handles.DrawLine(center, center + plane.normal * 2f);
                    
                    // 绘制平面边界
                    Vector3 right = Vector3.Cross(plane.normal, Vector3.up).normalized;
                    Vector3 up = Vector3.Cross(right, plane.normal).normalized;
                    
                    Vector3[] corners = new Vector3[4];
                    corners[0] = center + right * 2f + up * 2f;
                    corners[1] = center - right * 2f + up * 2f;
                    corners[2] = center - right * 2f - up * 2f;
                    corners[3] = center + right * 2f - up * 2f;
                    
                    Handles.DrawLine(corners[0], corners[1]);
                    Handles.DrawLine(corners[1], corners[2]);
                    Handles.DrawLine(corners[2], corners[3]);
                    Handles.DrawLine(corners[3], corners[0]);
                }
            }
            
            // 绘制包围盒
            if (showBounds)
            {
                Handles.color = boundsColor;
                Renderer[] renderers = FindObjectsOfType<Renderer>();
                
                foreach (Renderer renderer in renderers)
                {
                    Handles.DrawWireCube(renderer.bounds.center, renderer.bounds.size);
                }
            }
        }

        private void OnDestroy()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        #endregion

        #region 高级几何计算示例

        /// <summary>
        /// 计算包围盒
        /// </summary>
        public static void CalculateBoundsExample()
        {
            GameObject[] objects = FindObjectsOfType<GameObject>();
            if (objects.Length > 0)
            {
                Bounds bounds = GeometryUtility.CalculateBounds(objects[0].transform.position, objects[0].transform.rotation, Vector3.one);
                Debug.Log($"计算包围盒: {bounds}");
            }
        }

        /// <summary>
        /// 测试平面与AABB相交
        /// </summary>
        public static void TestPlanesAABBExample()
        {
            Plane[] planes = new Plane[]
            {
                new Plane(Vector3.up, Vector3.zero),
                new Plane(Vector3.down, Vector3.up * 10),
                new Plane(Vector3.left, Vector3.zero),
                new Plane(Vector3.right, Vector3.right * 10)
            };

            Bounds bounds = new Bounds(Vector3.zero, Vector3.one * 5);
            bool intersects = GeometryUtility.TestPlanesAABB(planes, bounds);
            Debug.Log($"平面与AABB相交: {intersects}");
        }

        /// <summary>
        /// 尝试平面相交
        /// </summary>
        public static void TryPlanesIntersectExample()
        {
            Plane plane1 = new Plane(Vector3.up, Vector3.zero);
            Plane plane2 = new Plane(Vector3.right, Vector3.zero);
            Plane plane3 = new Plane(Vector3.forward, Vector3.zero);

            Vector3 intersectionPoint;
            bool intersects = GeometryUtility.TryPlanesIntersect(plane1, plane2, plane3, out intersectionPoint);
            Debug.Log($"平面相交: {intersects}, 交点: {intersectionPoint}");
        }

        /// <summary>
        /// 检查点是否在三角形内
        /// </summary>
        public static void IsPointInTriangleExample()
        {
            Vector3 point = new Vector3(0.5f, 0.5f, 0);
            Vector3 a = new Vector3(0, 0, 0);
            Vector3 b = new Vector3(1, 0, 0);
            Vector3 c = new Vector3(0.5f, 1, 0);

            bool isInside = GeometryUtility.IsPointInTriangle(point, a, b, c);
            Debug.Log($"点是否在三角形内: {isInside}");
        }

        /// <summary>
        /// 计算OBB（有向包围盒）
        /// </summary>
        public static void CalculateOBBExample()
        {
            Vector3[] points = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(1, 1, 0)
            };

            Bounds obb = GeometryUtility.CalculateBounds(points);
            Debug.Log($"OBB包围盒: {obb}");
        }

        #endregion

        #region 几何变换示例

        /// <summary>
        /// 几何变换矩阵
        /// </summary>
        public static void GeometryTransformExample()
        {
            Vector3[] originalPoints = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0)
            };

            Matrix4x4 transformMatrix = Matrix4x4.TRS(
                new Vector3(5, 0, 0),
                Quaternion.Euler(0, 45, 0),
                Vector3.one * 2
            );

            Vector3[] transformedPoints = new Vector3[originalPoints.Length];
            for (int i = 0; i < originalPoints.Length; i++)
            {
                transformedPoints[i] = transformMatrix.MultiplyPoint(originalPoints[i]);
            }

            Debug.Log($"变换后的点: {string.Join(", ", transformedPoints)}");
        }

        /// <summary>
        /// 几何投影
        /// </summary>
        public static void GeometryProjectionExample()
        {
            Vector3 point = new Vector3(1, 2, 3);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Vector3 projectedPoint = plane.ClosestPointOnPlane(point);
            Debug.Log($"投影点: {projectedPoint}");
        }

        #endregion

        #region 几何碰撞检测示例

        /// <summary>
        /// 几何碰撞检测
        /// </summary>
        public static void GeometryCollisionExample()
        {
            Bounds bounds1 = new Bounds(Vector3.zero, Vector3.one);
            Bounds bounds2 = new Bounds(Vector3.one * 0.5f, Vector3.one);

            bool intersects = bounds1.Intersects(bounds2);
            Debug.Log($"包围盒相交: {intersects}");

            Bounds intersection = bounds1;
            intersection.Intersect(bounds2);
            Debug.Log($"相交区域: {intersection}");
        }

        /// <summary>
        /// 射线与几何体相交
        /// </summary>
        public static void RayGeometryIntersectionExample()
        {
            Ray ray = new Ray(Vector3.zero, Vector3.forward);
            Bounds bounds = new Bounds(Vector3.forward * 5, Vector3.one);

            float distance;
            bool intersects = bounds.IntersectRay(ray, out distance);
            Debug.Log($"射线与包围盒相交: {intersects}, 距离: {distance}");
        }

        #endregion

        #region 几何工具示例

        /// <summary>
        /// 几何工具函数
        /// </summary>
        public static void GeometryToolsExample()
        {
            // 计算距离
            Vector3 point1 = new Vector3(0, 0, 0);
            Vector3 point2 = new Vector3(3, 4, 0);
            float distance = Vector3.Distance(point1, point2);
            Debug.Log($"两点距离: {distance}");

            // 计算角度
            Vector3 vector1 = new Vector3(1, 0, 0);
            Vector3 vector2 = new Vector3(0, 1, 0);
            float angle = Vector3.Angle(vector1, vector2);
            Debug.Log($"两向量夹角: {angle}度");

            // 计算叉积
            Vector3 crossProduct = Vector3.Cross(vector1, vector2);
            Debug.Log($"叉积: {crossProduct}");
        }

        /// <summary>
        /// 几何优化
        /// </summary>
        public static void GeometryOptimizationExample()
        {
            Vector3[] points = new Vector3[1000];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = Random.insideUnitSphere * 10;
            }

            // 计算包围盒
            Bounds bounds = GeometryUtility.CalculateBounds(points);
            Debug.Log($"优化后的包围盒: {bounds}");

            // 计算中心点
            Vector3 center = Vector3.zero;
            foreach (Vector3 point in points)
            {
                center += point;
            }
            center /= points.Length;
            Debug.Log($"几何中心: {center}");
        }

        #endregion
    }
}
