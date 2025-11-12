#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace OdinSamples.CustomEditors.Editor
{
    /// <summary>
    /// 自定义 Odin Editor 示例
    /// 展示如何为特定类创建自定义的 Inspector 编辑器
    /// </summary>
    [CustomEditor(typeof(CustomOdinEditorSample))]
    public class CustomOdinEditorSampleEditor : OdinEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        
        public override void OnInspectorGUI()
        {
            // 在默认 Inspector 之前绘制自定义内容
            DrawCustomHeader();
            
            // 绘制默认的 Odin Inspector
            base.OnInspectorGUI();
            
            // 在默认 Inspector 之后绘制自定义内容
            DrawCustomFooter();
        }
        
        /// <summary>
        /// 绘制自定义头部
        /// </summary>
        private void DrawCustomHeader()
        {
            var sample = target as CustomOdinEditorSample;
            if (sample == null) return;
            
            // 绘制自定义标题栏
            GUILayout.BeginVertical("box");
            {
                GUILayout.Label("自定义 Odin Editor", EditorStyles.boldLabel);
                GUILayout.Label("这是通过继承 OdinEditor 创建的自定义编辑器", EditorStyles.miniLabel);
                
                // 显示角色摘要信息
                GUILayout.Space(5);
                GUILayout.Label($"角色：{sample.characterName} Lv.{sample.level}", EditorStyles.largeLabel);
                
                // 绘制生命值和魔法值的进度条
                DrawStatusBar("生命值", sample.health, 100f, Color.red);
                DrawStatusBar("魔法值", sample.mana, 100f, Color.blue);
            }
            GUILayout.EndVertical();
            GUILayout.Space(10);
        }
        
        /// <summary>
        /// 绘制自定义底部
        /// </summary>
        private void DrawCustomFooter()
        {
            var sample = target as CustomOdinEditorSample;
            if (sample == null) return;
            
            GUILayout.Space(10);
            
            // 绘制操作按钮区域
            GUILayout.BeginVertical("box");
            {
                GUILayout.Label("快捷操作", EditorStyles.boldLabel);
                
                GUILayout.BeginHorizontal();
                {
                    // 升级按钮
                    GUI.backgroundColor = new Color(0.3f, 0.8f, 0.3f);
                    if (GUILayout.Button("升级", GUILayout.Height(30)))
                    {
                        Undo.RecordObject(sample, "Level Up");
                        sample.level++;
                        EditorUtility.SetDirty(sample);
                        Debug.Log($"{sample.characterName} 升级到 Lv.{sample.level}");
                    }
                    
                    // 恢复生命按钮
                    GUI.backgroundColor = new Color(0.8f, 0.3f, 0.3f);
                    if (GUILayout.Button("恢复生命", GUILayout.Height(30)))
                    {
                        Undo.RecordObject(sample, "Restore Health");
                        sample.health = 100f;
                        EditorUtility.SetDirty(sample);
                        Debug.Log($"{sample.characterName} 生命值已恢复");
                    }
                    
                    // 恢复魔法按钮
                    GUI.backgroundColor = new Color(0.3f, 0.5f, 0.8f);
                    if (GUILayout.Button("恢复魔法", GUILayout.Height(30)))
                    {
                        Undo.RecordObject(sample, "Restore Mana");
                        sample.mana = 50f;
                        EditorUtility.SetDirty(sample);
                        Debug.Log($"{sample.characterName} 魔法值已恢复");
                    }
                    
                    GUI.backgroundColor = Color.white;
                }
                GUILayout.EndHorizontal();
                
                GUILayout.Space(5);
                
                // 重置按钮
                GUI.backgroundColor = new Color(0.8f, 0.5f, 0.3f);
                if (GUILayout.Button("重置所有数据", GUILayout.Height(30)))
                {
                    if (EditorUtility.DisplayDialog("确认重置", "确定要重置所有数据吗？", "确定", "取消"))
                    {
                        Undo.RecordObject(sample, "Reset Data");
                        sample.characterName = "勇者";
                        sample.level = 1;
                        sample.health = 100f;
                        sample.mana = 50f;
                        EditorUtility.SetDirty(sample);
                        Debug.Log("数据已重置");
                    }
                }
                GUI.backgroundColor = Color.white;
            }
            GUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制状态栏
        /// </summary>
        private void DrawStatusBar(string label, float value, float maxValue, Color color)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(label, GUILayout.Width(60));
                
                var rect = GUILayoutUtility.GetRect(100, 18);
                EditorGUI.ProgressBar(rect, value / maxValue, $"{value:F0}/{maxValue:F0}");
                
                // 在进度条上绘制颜色
                var colorRect = new Rect(rect.x + 1, rect.y + 1, (rect.width - 2) * (value / maxValue), rect.height - 2);
                EditorGUI.DrawRect(colorRect, color * 0.7f);
            }
            GUILayout.EndHorizontal();
        }
    }
}
#endif

