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
