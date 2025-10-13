using UnityEngine;
using UnityEditor;
using System.IO;

namespace UnityEditor.Examples
{
    /// <summary>
    /// EditorUtility 工具类示例
    /// 提供编辑器对话框、文件操作、进度条等功能
    /// </summary>
    public static class EditorUtilityExample
    {
        #region 对话框示例

        /// <summary>
        /// 显示确认对话框
        /// </summary>
        public static void ShowConfirmationDialog()
        {
            bool result = EditorUtility.DisplayDialog(
                "确认操作",
                "您确定要执行此操作吗？",
                "确定",
                "取消"
            );

            Debug.Log($"用户选择: {(result ? "确定" : "取消")}");
        }

        /// <summary>
        /// 显示复杂对话框
        /// </summary>
        public static void ShowComplexDialog()
        {
            bool result = EditorUtility.DisplayDialogComplex(
                "保存文件",
                "文件已修改，是否保存？",
                "保存",
                "不保存",
                "取消"
            );

            switch (result)
            {
                case 0: // 保存
                    Debug.Log("用户选择保存");
                    break;
                case 1: // 不保存
                    Debug.Log("用户选择不保存");
                    break;
                case 2: // 取消
                    Debug.Log("用户取消操作");
                    break;
            }
        }

        #endregion

        #region 文件操作示例

        /// <summary>
        /// 保存文件面板示例
        /// </summary>
        public static void SaveFilePanelExample()
        {
            string path = EditorUtility.SaveFilePanel(
                "保存文件",
                "Assets",
                "MyFile",
                "txt"
            );

            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, "Hello World!");
                Debug.Log($"文件已保存到: {path}");
            }
        }

        /// <summary>
        /// 保存文件面板（项目内）
        /// </summary>
        public static void SaveFilePanelInProjectExample()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "保存预制体",
                "Assets",
                "MyPrefab",
                "prefab",
                "选择保存位置"
            );

            if (!string.IsNullOrEmpty(path))
            {
                Debug.Log($"将在项目内保存到: {path}");
            }
        }

        /// <summary>
        /// 打开文件面板示例
        /// </summary>
        public static void OpenFilePanelExample()
        {
            string path = EditorUtility.OpenFilePanel(
                "选择文件",
                "Assets",
                "txt"
            );

            if (!string.IsNullOrEmpty(path))
            {
                string content = File.ReadAllText(path);
                Debug.Log($"文件内容: {content}");
            }
        }

        /// <summary>
        /// 打开文件面板（项目内）
        /// </summary>
        public static void OpenFilePanelInProjectExample()
        {
            string path = EditorUtility.OpenFilePanelInProject(
                "选择项目文件",
                "Assets",
                "prefab",
                "选择预制体文件"
            );

            if (!string.IsNullOrEmpty(path))
            {
                Debug.Log($"选择的项目文件: {path}");
            }
        }

        /// <summary>
        /// 选择文件夹面板
        /// </summary>
        public static void SelectFolderPanelExample()
        {
            string path = EditorUtility.OpenFolderPanel(
                "选择文件夹",
                "Assets",
                "选择目标文件夹"
            );

            if (!string.IsNullOrEmpty(path))
            {
                Debug.Log($"选择的文件夹: {path}");
            }
        }

        #endregion

        #region 进度条示例

        /// <summary>
        /// 显示进度条示例
        /// </summary>
        public static void DisplayProgressBarExample()
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    EditorUtility.DisplayProgressBar(
                        "处理中...",
                        $"正在处理第 {i} 项",
                        i / 100f
                    );

                    // 模拟处理时间
                    System.Threading.Thread.Sleep(50);
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        /// <summary>
        /// 清除进度条
        /// </summary>
        public static void ClearProgressBarExample()
        {
            EditorUtility.DisplayProgressBar("测试", "测试进度条", 0.5f);
            
            // 延迟清除
            EditorApplication.delayCall += () =>
            {
                EditorUtility.ClearProgressBar();
                Debug.Log("进度条已清除");
            };
        }

        #endregion

        #region 对象操作示例

        /// <summary>
        /// 标记对象为脏
        /// </summary>
        public static void SetDirtyExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                EditorUtility.SetDirty(selected);
                Debug.Log($"对象 {selected.name} 已标记为脏");
            }
        }

        /// <summary>
        /// 检查对象是否脏
        /// </summary>
        public static void IsDirtyExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                bool isDirty = EditorUtility.IsDirty(selected);
                Debug.Log($"对象 {selected.name} 是否脏: {isDirty}");
            }
        }

        #endregion

        #region 资源操作示例

        /// <summary>
        /// 创建文件夹
        /// </summary>
        public static void CreateFolderExample()
        {
            string folderPath = "Assets/MyNewFolder";
            
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder("Assets", "MyNewFolder");
                Debug.Log($"文件夹已创建: {folderPath}");
            }
            else
            {
                Debug.Log($"文件夹已存在: {folderPath}");
            }
        }

        /// <summary>
        /// 复制资源
        /// </summary>
        public static void CopyAssetExample()
        {
            string sourcePath = "Assets/MyFile.txt";
            string destPath = "Assets/MyFile_Copy.txt";

            if (File.Exists(sourcePath))
            {
                AssetDatabase.CopyAsset(sourcePath, destPath);
                Debug.Log($"资源已复制: {sourcePath} -> {destPath}");
            }
        }

        #endregion

        #region 实用工具示例

        /// <summary>
        /// 获取资源路径
        /// </summary>
        public static void GetAssetPathExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                string path = AssetDatabase.GetAssetPath(selected);
                Debug.Log($"资源路径: {path}");
            }
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        public static void LoadAssetExample()
        {
            string assetPath = "Assets/MyPrefab.prefab";
            
            if (AssetDatabase.LoadAssetAtPath<GameObject>(assetPath) != null)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                Debug.Log($"资源已加载: {prefab.name}");
            }
        }

        /// <summary>
        /// 刷新资源数据库
        /// </summary>
        public static void RefreshAssetDatabaseExample()
        {
            AssetDatabase.Refresh();
            Debug.Log("资源数据库已刷新");
        }

        #endregion

        #region 编辑器窗口示例

        /// <summary>
        /// 聚焦项目窗口
        /// </summary>
        public static void FocusProjectWindowExample()
        {
            EditorUtility.FocusProjectWindow();
            Debug.Log("项目窗口已聚焦");
        }

        /// <summary>
        /// 聚焦检查器窗口
        /// </summary>
        public static void FocusInspectorWindowExample()
        {
            EditorUtility.FocusInspectorWindow();
            Debug.Log("检查器窗口已聚焦");
        }

        #endregion

        #region 资源导入和导出示例

        /// <summary>
        /// 重新导入资源
        /// </summary>
        public static void ReimportAssetExample()
        {
            string assetPath = "Assets/MyTexture.png";
            
            if (File.Exists(assetPath))
            {
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                Debug.Log($"资源已重新导入: {assetPath}");
            }
        }

        /// <summary>
        /// 导出包
        /// </summary>
        public static void ExportPackageExample()
        {
            string[] assetPaths = { "Assets/MyPrefab.prefab", "Assets/MyScript.cs" };
            string packagePath = "Assets/MyPackage.unitypackage";
            
            AssetDatabase.ExportPackage(assetPaths, packagePath, ExportPackageOptions.Recurse);
            Debug.Log($"包已导出到: {packagePath}");
        }

        /// <summary>
        /// 导入包
        /// </summary>
        public static void ImportPackageExample()
        {
            string packagePath = "Assets/MyPackage.unitypackage";
            
            if (File.Exists(packagePath))
            {
                AssetDatabase.ImportPackage(packagePath, true);
                Debug.Log($"包已导入: {packagePath}");
            }
        }

        #endregion

        #region 编译和脚本示例

        /// <summary>
        /// 请求脚本编译
        /// </summary>
        public static void RequestScriptCompilationExample()
        {
            EditorUtility.RequestScriptCompilation();
            Debug.Log("已请求脚本编译");
        }

        /// <summary>
        /// 检查脚本编译状态
        /// </summary>
        public static void CheckScriptCompilationExample()
        {
            bool isCompiling = EditorApplication.isCompiling;
            Debug.Log($"脚本是否正在编译: {isCompiling}");
        }

        #endregion

        #region 场景操作示例

        /// <summary>
        /// 保存场景
        /// </summary>
        public static void SaveSceneExample()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            
            if (currentScene.isDirty)
            {
                bool saved = EditorSceneManager.SaveScene(currentScene);
                Debug.Log($"场景保存结果: {saved}");
            }
            else
            {
                Debug.Log("场景无需保存");
            }
        }

        /// <summary>
        /// 标记场景为脏
        /// </summary>
        public static void MarkSceneDirtyExample()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(currentScene);
            Debug.Log($"场景 {currentScene.name} 已标记为脏");
        }

        #endregion

        #region 编辑器状态示例

        /// <summary>
        /// 检查编辑器是否正在播放
        /// </summary>
        public static void CheckEditorPlayingExample()
        {
            bool isPlaying = EditorApplication.isPlaying;
            Debug.Log($"编辑器是否正在播放: {isPlaying}");
        }

        /// <summary>
        /// 检查编辑器是否暂停
        /// </summary>
        public static void CheckEditorPausedExample()
        {
            bool isPaused = EditorApplication.isPaused;
            Debug.Log($"编辑器是否暂停: {isPaused}");
        }

        /// <summary>
        /// 暂停编辑器
        /// </summary>
        public static void PauseEditorExample()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPaused = true;
                Debug.Log("编辑器已暂停");
            }
            else
            {
                Debug.Log("编辑器未在播放状态，无法暂停");
            }
        }

        #endregion

        #region 资源刷新示例

        /// <summary>
        /// 刷新资源数据库
        /// </summary>
        public static void RefreshAssetDatabaseExample()
        {
            AssetDatabase.Refresh();
            Debug.Log("资源数据库已刷新");
        }

        /// <summary>
        /// 刷新资源数据库（异步）
        /// </summary>
        public static void RefreshAssetDatabaseAsyncExample()
        {
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            Debug.Log("资源数据库异步刷新已开始");
        }

        #endregion

        #region 资源路径示例

        /// <summary>
        /// 获取资源GUID
        /// </summary>
        public static void GetAssetGUIDExample()
        {
            string assetPath = "Assets/MyPrefab.prefab";
            
            if (File.Exists(assetPath))
            {
                string guid = AssetDatabase.AssetPathToGUID(assetPath);
                Debug.Log($"资源GUID: {guid}");
            }
        }

        /// <summary>
        /// GUID转资源路径
        /// </summary>
        public static void GUIDToAssetPathExample()
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                Debug.Log($"GUID对应的资源路径: {assetPath}");
            }
        }

        #endregion

        #region 资源依赖示例

        /// <summary>
        /// 获取资源依赖
        /// </summary>
        public static void GetAssetDependenciesExample()
        {
            string assetPath = "Assets/MyPrefab.prefab";
            
            if (File.Exists(assetPath))
            {
                string[] dependencies = AssetDatabase.GetDependencies(assetPath);
                Debug.Log($"资源 {assetPath} 的依赖:");
                
                foreach (string dependency in dependencies)
                {
                    Debug.Log($"  - {dependency}");
                }
            }
        }

        /// <summary>
        /// 检查资源是否依赖
        /// </summary>
        public static void CheckAssetDependencyExample()
        {
            string assetPath = "Assets/MyPrefab.prefab";
            string dependencyPath = "Assets/MyTexture.png";
            
            if (File.Exists(assetPath) && File.Exists(dependencyPath))
            {
                string[] dependencies = AssetDatabase.GetDependencies(assetPath);
                bool isDependent = System.Array.Exists(dependencies, path => path == dependencyPath);
                
                Debug.Log($"资源 {assetPath} 是否依赖 {dependencyPath}: {isDependent}");
            }
        }

        #endregion

        #region 资源搜索示例

        /// <summary>
        /// 搜索资源
        /// </summary>
        public static void FindAssetsExample()
        {
            // 搜索所有预制体
            string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
            Debug.Log($"找到 {prefabGuids.Length} 个预制体");
            
            // 搜索所有脚本
            string[] scriptGuids = AssetDatabase.FindAssets("t:Script");
            Debug.Log($"找到 {scriptGuids.Length} 个脚本");
            
            // 搜索特定名称的资源
            string[] namedGuids = AssetDatabase.FindAssets("MyPrefab");
            Debug.Log($"找到 {namedGuids.Length} 个名为MyPrefab的资源");
        }

        /// <summary>
        /// 搜索资源（带标签）
        /// </summary>
        public static void FindAssetsWithLabelsExample()
        {
            // 搜索带特定标签的资源
            string[] guids = AssetDatabase.FindAssets("l:Important");
            Debug.Log($"找到 {guids.Length} 个带Important标签的资源");
            
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Debug.Log($"  - {path}");
            }
        }

        #endregion

        #region 资源移动和重命名示例

        /// <summary>
        /// 移动资源
        /// </summary>
        public static void MoveAssetExample()
        {
            string sourcePath = "Assets/MyFile.txt";
            string destPath = "Assets/NewFolder/MyFile.txt";
            
            if (File.Exists(sourcePath))
            {
                string result = AssetDatabase.MoveAsset(sourcePath, destPath);
                
                if (string.IsNullOrEmpty(result))
                {
                    Debug.Log($"资源已移动: {sourcePath} -> {destPath}");
                }
                else
                {
                    Debug.LogError($"移动失败: {result}");
                }
            }
        }

        /// <summary>
        /// 重命名资源
        /// </summary>
        public static void RenameAssetExample()
        {
            string oldPath = "Assets/MyFile.txt";
            string newPath = "Assets/MyRenamedFile.txt";
            
            if (File.Exists(oldPath))
            {
                string result = AssetDatabase.RenameAsset(oldPath, "MyRenamedFile");
                
                if (string.IsNullOrEmpty(result))
                {
                    Debug.Log($"资源已重命名: {oldPath} -> {newPath}");
                }
                else
                {
                    Debug.LogError($"重命名失败: {result}");
                }
            }
        }

        #endregion

        #region 资源删除示例

        /// <summary>
        /// 删除资源
        /// </summary>
        public static void DeleteAssetExample()
        {
            string assetPath = "Assets/MyFile.txt";
            
            if (File.Exists(assetPath))
            {
                bool deleted = AssetDatabase.DeleteAsset(assetPath);
                Debug.Log($"资源删除结果: {deleted}");
            }
        }

        /// <summary>
        /// 移动资源到回收站
        /// </summary>
        public static void MoveAssetToTrashExample()
        {
            string assetPath = "Assets/MyFile.txt";
            
            if (File.Exists(assetPath))
            {
                bool moved = AssetDatabase.MoveAssetToTrash(assetPath);
                Debug.Log($"资源移动到回收站结果: {moved}");
            }
        }

        #endregion

        #region 编辑器设置和配置示例

        /// <summary>
        /// 获取编辑器设置
        /// </summary>
        public static void GetEditorSettingsExample()
        {
            Debug.Log($"编辑器设置信息:");
            Debug.Log($"- Unity版本: {Application.unityVersion}");
            Debug.Log($"- 平台: {Application.platform}");
            Debug.Log($"- 公司名称: {Application.companyName}");
            Debug.Log($"- 产品名称: {Application.productName}");
            Debug.Log($"- 数据路径: {Application.dataPath}");
            Debug.Log($"- 持久化数据路径: {Application.persistentDataPath}");
        }

        /// <summary>
        /// 设置编辑器标签
        /// </summary>
        public static void SetEditorLabelsExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                string[] labels = { "Important", "Test", "Editor" };
                AssetDatabase.SetLabels(selected, labels);
                Debug.Log($"已为 {selected.name} 设置标签: {string.Join(", ", labels)}");
            }
        }

        /// <summary>
        /// 获取编辑器标签
        /// </summary>
        public static void GetEditorLabelsExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                string[] labels = AssetDatabase.GetLabels(selected);
                Debug.Log($"对象 {selected.name} 的标签: {string.Join(", ", labels)}");
            }
        }

        #endregion

        #region 编辑器窗口管理示例

        /// <summary>
        /// 打开编辑器窗口
        /// </summary>
        public static void OpenEditorWindowExample()
        {
            // 打开项目窗口
            EditorUtility.FocusProjectWindow();
            
            // 打开检查器窗口
            EditorUtility.FocusInspectorWindow();
            
            Debug.Log("编辑器窗口已打开");
        }

        /// <summary>
        /// 关闭编辑器窗口
        /// </summary>
        public static void CloseEditorWindowExample()
        {
            // 关闭所有编辑器窗口
            EditorApplication.ExecuteMenuItem("Window/General/Close All");
            Debug.Log("所有编辑器窗口已关闭");
        }

        /// <summary>
        /// 最大化编辑器窗口
        /// </summary>
        public static void MaximizeEditorWindowExample()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Maximize");
            Debug.Log("编辑器窗口已最大化");
        }

        #endregion

        #region 编辑器状态控制示例

        /// <summary>
        /// 开始播放模式
        /// </summary>
        public static void StartPlayModeExample()
        {
            if (!EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = true;
                Debug.Log("编辑器已开始播放模式");
            }
            else
            {
                Debug.Log("编辑器已在播放模式");
            }
        }

        /// <summary>
        /// 停止播放模式
        /// </summary>
        public static void StopPlayModeExample()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                Debug.Log("编辑器已停止播放模式");
            }
            else
            {
                Debug.Log("编辑器未在播放模式");
            }
        }

        /// <summary>
        /// 切换播放模式
        /// </summary>
        public static void TogglePlayModeExample()
        {
            EditorApplication.isPlaying = !EditorApplication.isPlaying;
            Debug.Log($"播放模式已切换为: {EditorApplication.isPlaying}");
        }

        /// <summary>
        /// 单步执行
        /// </summary>
        public static void StepExample()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.Step();
                Debug.Log("已执行单步");
            }
            else
            {
                Debug.Log("编辑器未在播放模式，无法单步执行");
            }
        }

        #endregion

        #region 编辑器回调示例

        /// <summary>
        /// 注册编辑器回调
        /// </summary>
        public static void RegisterEditorCallbacksExample()
        {
            // 注册播放模式状态改变回调
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            
            // 注册编译完成回调
            EditorApplication.compilationFinished += OnCompilationFinished;
            
            // 注册延迟调用
            EditorApplication.delayCall += OnDelayCall;
            
            Debug.Log("编辑器回调已注册");
        }

        /// <summary>
        /// 播放模式状态改变回调
        /// </summary>
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            Debug.Log($"播放模式状态改变: {state}");
        }

        /// <summary>
        /// 编译完成回调
        /// </summary>
        private static void OnCompilationFinished(object obj)
        {
            Debug.Log("脚本编译完成");
        }

        /// <summary>
        /// 延迟调用回调
        /// </summary>
        private static void OnDelayCall()
        {
            Debug.Log("延迟调用执行");
        }

        #endregion

        #region 编辑器日志示例

        /// <summary>
        /// 清除控制台日志
        /// </summary>
        public static void ClearConsoleLogsExample()
        {
            // 清除控制台日志
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
            
            Debug.Log("控制台日志已清除");
        }

        /// <summary>
        /// 获取日志数量
        /// </summary>
        public static void GetLogCountExample()
        {
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("GetCount");
            int count = (int)method.Invoke(new object(), null);
            
            Debug.Log($"当前日志数量: {count}");
        }

        #endregion

        #region 编辑器选择示例

        /// <summary>
        /// 设置选择对象
        /// </summary>
        public static void SetSelectionExample()
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            if (allObjects.Length > 0)
            {
                Selection.objects = allObjects;
                Debug.Log($"已选择 {allObjects.Length} 个对象");
            }
        }

        /// <summary>
        /// 获取选择对象
        /// </summary>
        public static void GetSelectionExample()
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            Debug.Log($"当前选择了 {selectedObjects.Length} 个对象:");
            
            foreach (GameObject obj in selectedObjects)
            {
                Debug.Log($"- {obj.name}");
            }
        }

        /// <summary>
        /// 清除选择
        /// </summary>
        public static void ClearSelectionExample()
        {
            Selection.activeGameObject = null;
            Debug.Log("选择已清除");
        }

        #endregion

        #region 编辑器工具示例

        /// <summary>
        /// 执行菜单项
        /// </summary>
        public static void ExecuteMenuItemExample()
        {
            // 执行保存场景菜单项
            EditorApplication.ExecuteMenuItem("File/Save Scene");
            Debug.Log("已执行保存场景菜单项");
        }

        /// <summary>
        /// 执行菜单项（带参数）
        /// </summary>
        public static void ExecuteMenuItemWithParameterExample()
        {
            // 执行打开场景菜单项
            EditorApplication.ExecuteMenuItem("File/Open Scene");
            Debug.Log("已执行打开场景菜单项");
        }

        #endregion

        #region 编辑器时间示例

        /// <summary>
        /// 获取编辑器时间
        /// </summary>
        public static void GetEditorTimeExample()
        {
            double time = EditorApplication.timeSinceStartup;
            Debug.Log($"编辑器运行时间: {time:F2} 秒");
        }

        /// <summary>
        /// 延迟调用示例
        /// </summary>
        public static void DelayCallExample()
        {
            Debug.Log("开始延迟调用");
            
            EditorApplication.delayCall += () =>
            {
                Debug.Log("延迟调用执行完成");
            };
        }

        /// <summary>
        /// 延迟调用（带参数）
        /// </summary>
        public static void DelayCallWithParameterExample()
        {
            string message = "延迟消息";
            
            EditorApplication.delayCall += () =>
            {
                Debug.Log($"延迟调用消息: {message}");
            };
        }

        #endregion

        #region 编辑器事件示例

        /// <summary>
        /// 注册更新事件
        /// </summary>
        public static void RegisterUpdateEventExample()
        {
            EditorApplication.update += OnEditorUpdate;
            Debug.Log("编辑器更新事件已注册");
        }

        /// <summary>
        /// 编辑器更新回调
        /// </summary>
        private static void OnEditorUpdate()
        {
            // 这里可以添加每帧执行的代码
            // 注意：不要在这里执行耗时操作
        }

        /// <summary>
        /// 注销更新事件
        /// </summary>
        public static void UnregisterUpdateEventExample()
        {
            EditorApplication.update -= OnEditorUpdate;
            Debug.Log("编辑器更新事件已注销");
        }

        #endregion

        #region 编辑器项目示例

        /// <summary>
        /// 获取项目信息
        /// </summary>
        public static void GetProjectInfoExample()
        {
            Debug.Log($"项目信息:");
            Debug.Log($"- 项目名称: {Application.productName}");
            Debug.Log($"- 公司名称: {Application.companyName}");
            Debug.Log($"- 版本: {Application.version}");
            Debug.Log($"- Unity版本: {Application.unityVersion}");
            Debug.Log($"- 平台: {Application.platform}");
            Debug.Log($"- 数据路径: {Application.dataPath}");
            Debug.Log($"- 流媒体资源路径: {Application.streamingAssetsPath}");
            Debug.Log($"- 临时缓存路径: {Application.temporaryCachePath}");
        }

        /// <summary>
        /// 刷新项目
        /// </summary>
        public static void RefreshProjectExample()
        {
            AssetDatabase.Refresh();
            Debug.Log("项目已刷新");
        }

        /// <summary>
        /// 保存项目
        /// </summary>
        public static void SaveProjectExample()
        {
            AssetDatabase.SaveAssets();
            Debug.Log("项目已保存");
        }

        #endregion

        #region 编辑器调试示例

        /// <summary>
        /// 设置断点
        /// </summary>
        public static void SetBreakpointExample()
        {
            Debug.Log("设置断点示例");
            Debug.Break(); // 这会暂停编辑器
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        public static void DebugLogExample()
        {
            Debug.Log("普通日志");
            Debug.LogWarning("警告日志");
            Debug.LogError("错误日志");
            Debug.LogException(new System.Exception("异常日志"));
        }

        /// <summary>
        /// 条件调试
        /// </summary>
        public static void ConditionalDebugExample()
        {
            bool debugMode = true;
            
            if (debugMode)
            {
                Debug.Log("调试模式开启");
            }
            
            Debug.LogAssertion("断言日志");
        }

        #endregion

        #region 文件系统操作示例

        /// <summary>
        /// 用默认应用程序打开文件
        /// </summary>
        public static void OpenWithDefaultAppExample()
        {
            string filePath = "Assets/MyFile.txt";
            
            if (File.Exists(filePath))
            {
                EditorUtility.OpenWithDefaultApp(filePath);
                Debug.Log($"已用默认应用程序打开文件: {filePath}");
            }
            else
            {
                Debug.LogWarning($"文件不存在: {filePath}");
            }
        }

        /// <summary>
        /// 用默认应用程序打开文件夹
        /// </summary>
        public static void OpenFolderWithDefaultAppExample()
        {
            string folderPath = "Assets";
            
            if (Directory.Exists(folderPath))
            {
                EditorUtility.OpenWithDefaultApp(folderPath);
                Debug.Log($"已用默认应用程序打开文件夹: {folderPath}");
            }
            else
            {
                Debug.LogWarning($"文件夹不存在: {folderPath}");
            }
        }

        /// <summary>
        /// 用默认应用程序打开项目文件夹
        /// </summary>
        public static void OpenProjectFolderExample()
        {
            string projectPath = Application.dataPath;
            EditorUtility.OpenWithDefaultApp(projectPath);
            Debug.Log($"已用默认应用程序打开项目文件夹: {projectPath}");
        }

        /// <summary>
        /// 在文件管理器中显示文件
        /// </summary>
        public static void RevealInFinderExample()
        {
            string filePath = "Assets/MyFile.txt";
            
            if (File.Exists(filePath))
            {
                EditorUtility.RevealInFinder(filePath);
                Debug.Log($"已在文件管理器中显示文件: {filePath}");
            }
            else
            {
                Debug.LogWarning($"文件不存在: {filePath}");
            }
        }

        /// <summary>
        /// 在文件管理器中显示文件夹
        /// </summary>
        public static void RevealFolderInFinderExample()
        {
            string folderPath = "Assets";
            
            if (Directory.Exists(folderPath))
            {
                EditorUtility.RevealInFinder(folderPath);
                Debug.Log($"已在文件管理器中显示文件夹: {folderPath}");
            }
            else
            {
                Debug.LogWarning($"文件夹不存在: {folderPath}");
            }
        }

        /// <summary>
        /// 在文件管理器中显示项目根目录
        /// </summary>
        public static void RevealProjectInFinderExample()
        {
            string projectPath = Application.dataPath;
            EditorUtility.RevealInFinder(projectPath);
            Debug.Log($"已在文件管理器中显示项目根目录: {projectPath}");
        }

        /// <summary>
        /// 在文件管理器中显示选中的资源
        /// </summary>
        public static void RevealSelectedAssetInFinderExample()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                string assetPath = AssetDatabase.GetAssetPath(selected);
                if (!string.IsNullOrEmpty(assetPath))
                {
                    EditorUtility.RevealInFinder(assetPath);
                    Debug.Log($"已在文件管理器中显示选中的资源: {assetPath}");
                }
                else
                {
                    Debug.LogWarning("选中的对象不是资源文件");
                }
            }
            else
            {
                Debug.LogWarning("请先选择一个对象");
            }
        }

        /// <summary>
        /// 批量在文件管理器中显示资源
        /// </summary>
        public static void RevealMultipleAssetsInFinderExample()
        {
            string[] assetPaths = {
                "Assets/MyPrefab.prefab",
                "Assets/MyScript.cs",
                "Assets/MyTexture.png"
            };
            
            foreach (string assetPath in assetPaths)
            {
                if (File.Exists(assetPath))
                {
                    EditorUtility.RevealInFinder(assetPath);
                    Debug.Log($"已在文件管理器中显示: {assetPath}");
                }
            }
        }

        /// <summary>
        /// 用默认应用程序打开不同类型的文件
        /// </summary>
        public static void OpenDifferentFileTypesExample()
        {
            // 打开文本文件
            string textFile = "Assets/MyFile.txt";
            if (File.Exists(textFile))
            {
                EditorUtility.OpenWithDefaultApp(textFile);
                Debug.Log("已打开文本文件");
            }
            
            // 打开图片文件
            string imageFile = "Assets/MyImage.png";
            if (File.Exists(imageFile))
            {
                EditorUtility.OpenWithDefaultApp(imageFile);
                Debug.Log("已打开图片文件");
            }
            
            // 打开脚本文件
            string scriptFile = "Assets/MyScript.cs";
            if (File.Exists(scriptFile))
            {
                EditorUtility.OpenWithDefaultApp(scriptFile);
                Debug.Log("已打开脚本文件");
            }
        }

        /// <summary>
        /// 文件系统操作综合示例
        /// </summary>
        public static void FileSystemOperationsExample()
        {
            Debug.Log("=== 文件系统操作综合示例 ===");
            
            // 1. 显示项目文件夹
            EditorUtility.RevealInFinder(Application.dataPath);
            Debug.Log("1. 已在文件管理器中显示项目文件夹");
            
            // 2. 打开项目文件夹
            EditorUtility.OpenWithDefaultApp(Application.dataPath);
            Debug.Log("2. 已用默认应用程序打开项目文件夹");
            
            // 3. 显示临时文件夹
            EditorUtility.RevealInFinder(Application.temporaryCachePath);
            Debug.Log("3. 已在文件管理器中显示临时文件夹");
            
            // 4. 显示流媒体资源文件夹
            EditorUtility.RevealInFinder(Application.streamingAssetsPath);
            Debug.Log("4. 已在文件管理器中显示流媒体资源文件夹");
        }

        /// <summary>
        /// 错误处理示例
        /// </summary>
        public static void FileSystemErrorHandlingExample()
        {
            try
            {
                // 尝试打开不存在的文件
                string nonExistentFile = "Assets/NonExistentFile.txt";
                EditorUtility.OpenWithDefaultApp(nonExistentFile);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"打开文件失败: {e.Message}");
            }
            
            try
            {
                // 尝试显示不存在的文件夹
                string nonExistentFolder = "Assets/NonExistentFolder";
                EditorUtility.RevealInFinder(nonExistentFolder);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"显示文件夹失败: {e.Message}");
            }
        }

        #endregion

        #region 资源卸载和内存管理示例

        /// <summary>
        /// 立即卸载未使用的资源
        /// </summary>
        public static void UnloadUnusedAssetsImmediateExample()
        {
            Debug.Log("开始卸载未使用的资源...");
            
            // 卸载未使用的资源
            EditorUtility.UnloadUnusedAssetsImmediate();
            
            Debug.Log("未使用的资源已卸载");
        }

        /// <summary>
        /// 卸载未使用的资源（异步）
        /// </summary>
        public static void UnloadUnusedAssetsAsyncExample()
        {
            Debug.Log("开始异步卸载未使用的资源...");
            
            // 异步卸载未使用的资源
            Resources.UnloadUnusedAssets();
            
            Debug.Log("异步卸载未使用的资源已开始");
        }

        /// <summary>
        /// 强制垃圾回收
        /// </summary>
        public static void ForceGarbageCollectionExample()
        {
            Debug.Log("开始强制垃圾回收...");
            
            // 强制垃圾回收
            System.GC.Collect();
            
            Debug.Log("垃圾回收完成");
        }

        /// <summary>
        /// 内存使用情况检查
        /// </summary>
        public static void MemoryUsageCheckExample()
        {
            long beforeMemory = System.GC.GetTotalMemory(false);
            Debug.Log($"当前内存使用: {beforeMemory / 1024 / 1024}MB");
            
            // 卸载未使用的资源
            EditorUtility.UnloadUnusedAssetsImmediate();
            
            long afterMemory = System.GC.GetTotalMemory(false);
            Debug.Log($"卸载后内存使用: {afterMemory / 1024 / 1024}MB");
            Debug.Log($"释放内存: {(beforeMemory - afterMemory) / 1024 / 1024}MB");
        }

        #endregion

        #region 着色器和渲染示例

        /// <summary>
        /// 更新全局着色器属性
        /// </summary>
        public static void UpdateGlobalShaderPropertiesExample()
        {
            Debug.Log("更新全局着色器属性...");
            
            // 更新全局着色器属性
            EditorUtility.UpdateGlobalShaderProperties();
            
            Debug.Log("全局着色器属性已更新");
        }

        /// <summary>
        /// 设置全局着色器属性
        /// </summary>
        public static void SetGlobalShaderPropertiesExample()
        {
            // 设置全局着色器属性
            Shader.SetGlobalFloat("_GlobalTime", Time.time);
            Shader.SetGlobalVector("_GlobalPosition", Vector3.zero);
            Shader.SetGlobalColor("_GlobalColor", Color.white);
            
            // 更新着色器属性
            EditorUtility.UpdateGlobalShaderProperties();
            
            Debug.Log("全局着色器属性已设置并更新");
        }

        /// <summary>
        /// 重新编译着色器
        /// </summary>
        public static void RecompileShadersExample()
        {
            Debug.Log("重新编译着色器...");
            
            // 重新编译所有着色器
            ShaderUtil.RecompileAllShaders();
            
            Debug.Log("着色器重新编译完成");
        }

        #endregion

        #region 弹出菜单示例

        /// <summary>
        /// 显示弹出菜单
        /// </summary>
        public static void DisplayPopupMenuExample()
        {
            // 创建菜单项
            GenericMenu menu = new GenericMenu();
            
            menu.AddItem(new GUIContent("选项1"), false, () => {
                Debug.Log("选择了选项1");
            });
            
            menu.AddItem(new GUIContent("选项2"), false, () => {
                Debug.Log("选择了选项2");
            });
            
            menu.AddSeparator("");
            
            menu.AddItem(new GUIContent("禁用选项"), false, () => {
                Debug.Log("选择了禁用选项");
            });
            
            // 显示弹出菜单
            menu.ShowAsContext();
            
            Debug.Log("弹出菜单已显示");
        }

        /// <summary>
        /// 显示上下文菜单
        /// </summary>
        public static void ShowContextMenuExample()
        {
            // 创建上下文菜单
            GenericMenu contextMenu = new GenericMenu();
            
            contextMenu.AddItem(new GUIContent("复制"), false, () => {
                Debug.Log("复制操作");
            });
            
            contextMenu.AddItem(new GUIContent("粘贴"), false, () => {
                Debug.Log("粘贴操作");
            });
            
            contextMenu.AddSeparator("");
            
            contextMenu.AddItem(new GUIContent("删除"), false, () => {
                Debug.Log("删除操作");
            });
            
            // 在鼠标位置显示上下文菜单
            contextMenu.ShowAsContext();
            
            Debug.Log("上下文菜单已显示");
        }

        #endregion

        #region 编辑器偏好设置示例

        /// <summary>
        /// 设置编辑器偏好
        /// </summary>
        public static void SetEditorPrefsExample()
        {
            // 设置各种类型的偏好
            EditorPrefs.SetString("MyString", "Hello World");
            EditorPrefs.SetInt("MyInt", 42);
            EditorPrefs.SetFloat("MyFloat", 3.14f);
            EditorPrefs.SetBool("MyBool", true);
            
            Debug.Log("编辑器偏好已设置");
        }

        /// <summary>
        /// 获取编辑器偏好
        /// </summary>
        public static void GetEditorPrefsExample()
        {
            // 获取各种类型的偏好
            string myString = EditorPrefs.GetString("MyString", "默认值");
            int myInt = EditorPrefs.GetInt("MyInt", 0);
            float myFloat = EditorPrefs.GetFloat("MyFloat", 0f);
            bool myBool = EditorPrefs.GetBool("MyBool", false);
            
            Debug.Log($"编辑器偏好值:");
            Debug.Log($"- 字符串: {myString}");
            Debug.Log($"- 整数: {myInt}");
            Debug.Log($"- 浮点数: {myFloat}");
            Debug.Log($"- 布尔值: {myBool}");
        }

        /// <summary>
        /// 删除编辑器偏好
        /// </summary>
        public static void DeleteEditorPrefsExample()
        {
            // 删除特定偏好
            EditorPrefs.DeleteKey("MyString");
            EditorPrefs.DeleteKey("MyInt");
            
            // 删除所有偏好
            EditorPrefs.DeleteAll();
            
            Debug.Log("编辑器偏好已删除");
        }

        #endregion

        #region 编辑器窗口状态示例

        /// <summary>
        /// 检查编辑器窗口状态
        /// </summary>
        public static void CheckEditorWindowStateExample()
        {
            Debug.Log($"编辑器窗口状态:");
            Debug.Log($"- 是否最大化: {EditorApplication.isMaximized}");
            Debug.Log($"- 是否正在播放: {EditorApplication.isPlaying}");
            Debug.Log($"- 是否暂停: {EditorApplication.isPaused}");
            Debug.Log($"- 是否正在编译: {EditorApplication.isCompiling}");
        }

        /// <summary>
        /// 切换编辑器窗口最大化状态
        /// </summary>
        public static void ToggleEditorMaximizedExample()
        {
            EditorApplication.isMaximized = !EditorApplication.isMaximized;
            Debug.Log($"编辑器窗口最大化状态: {EditorApplication.isMaximized}");
        }

        /// <summary>
        /// 设置编辑器窗口标题
        /// </summary>
        public static void SetEditorWindowTitleExample()
        {
            // 获取当前活动窗口
            EditorWindow activeWindow = EditorWindow.focusedWindow;
            
            if (activeWindow != null)
            {
                activeWindow.titleContent = new GUIContent("自定义标题");
                Debug.Log("编辑器窗口标题已设置");
            }
        }

        #endregion

        #region 编辑器快捷键示例

        /// <summary>
        /// 注册快捷键
        /// </summary>
        public static void RegisterShortcutExample()
        {
            // 注册Ctrl+S快捷键
            EditorApplication.ExecuteMenuItem("File/Save Scene");
            Debug.Log("快捷键已注册");
        }

        /// <summary>
        /// 执行快捷键
        /// </summary>
        public static void ExecuteShortcutExample()
        {
            // 执行Ctrl+Z撤销
            EditorApplication.ExecuteMenuItem("Edit/Undo");
            Debug.Log("撤销操作已执行");
        }

        #endregion

        #region 编辑器主题示例

        /// <summary>
        /// 获取编辑器主题
        /// </summary>
        public static void GetEditorThemeExample()
        {
            // 获取编辑器主题
            bool isDarkTheme = EditorGUIUtility.isProSkin;
            
            Debug.Log($"编辑器主题: {(isDarkTheme ? "深色主题" : "浅色主题")}");
        }

        /// <summary>
        /// 设置编辑器主题
        /// </summary>
        public static void SetEditorThemeExample()
        {
            // 注意：主题设置通常通过Preferences窗口进行
            Debug.Log("编辑器主题设置需要通过Preferences窗口进行");
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 批量处理资源示例
        /// </summary>
        public static void BatchProcessAssetsExample()
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            
            try
            {
                for (int i = 0; i < guids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    
                    EditorUtility.DisplayProgressBar(
                        "批量处理预制体",
                        $"正在处理: {Path.GetFileName(path)}",
                        (float)i / guids.Length
                    );

                    // 处理预制体
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (prefab != null)
                    {
                        EditorUtility.SetDirty(prefab);
                    }
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
                AssetDatabase.SaveAssets();
                Debug.Log($"批量处理完成，共处理 {guids.Length} 个预制体");
            }
        }

        /// <summary>
        /// 资源导入设置示例
        /// </summary>
        public static void AssetImportSettingsExample()
        {
            string assetPath = "Assets/MyTexture.png";
            
            if (File.Exists(assetPath))
            {
                TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                if (importer != null)
                {
                    // 设置导入设置
                    importer.textureType = TextureImporterType.Sprite;
                    importer.spriteImportMode = SpriteImportMode.Single;
                    
                    EditorUtility.SetDirty(importer);
                    importer.SaveAndReimport();
                    
                    Debug.Log($"纹理导入设置已更新: {assetPath}");
                }
            }
        }

        #endregion
    }
}
