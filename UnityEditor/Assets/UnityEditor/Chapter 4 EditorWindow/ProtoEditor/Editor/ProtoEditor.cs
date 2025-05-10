using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

/// <summary>
/// Protobuf通信协议类编辑器
/// </summary>
public class ProtoEditor : EditorWindow
{
    [MenuItem("Example/Multiplayer/Proto Editor")]
    public static void Open()
    {
        GetWindow<ProtoEditor>("Proto Editor").Show();
    }

    //.proto文件名称
    private string fileName;
    //存储所有类
    private List<Message> messages = new List<Message>();
    //滚动值
    private Vector2 scroll;
    //字段存储折叠状态
    private readonly Dictionary<Message, bool> foldoutDic = new Dictionary<Message, bool>();
    //json文件存放路径
    private const string workspacePath = "/Metaverse/Scripts/Proto";

    private void OnGUI()
    {
        OnEditGUI();
        OnBottomMenuGUI();
    }

    //编辑
    private void OnEditGUI()
    {
        //编辑.proto文件名称
        fileName = EditorGUILayout.TextField(".proto File Name", fileName);
        EditorGUILayout.Space();

        // 滚动视图
        scroll = GUILayout.BeginScrollView(scroll);
        for (int i = 0; i < messages.Count; i++)
        {
            var message = messages[i];

            GUILayout.BeginHorizontal();
            foldoutDic[message] = EditorGUILayout.Foldout(foldoutDic[message], message.name, true);
            
            // 插入新类
            if (GUILayout.Button("+", GUILayout.Width(20f)))
            {
                Message insertMessage = new Message();
                messages.Insert(i + 1, insertMessage);
                foldoutDic.Add(insertMessage, true);
                Repaint();
                return;
            }
            
            // 删除该类
            if (GUILayout.Button("-", GUILayout.Width(20f)))
            {
                messages.Remove(message);
                foldoutDic.Remove(message);
                Repaint();
                return;
            }
            GUILayout.EndHorizontal();

            // 如果折叠栏为打开状态 绘制具体字段内容
            if (foldoutDic[message])
            {
                // 编辑类名
                message.name = EditorGUILayout.TextField("Name", message.name);
                
                // 字段数量为0 提供按钮创建
                if (message.fieldsList.Count == 0)
                {
                    if (GUILayout.Button("New Field"))
                    {
                        message.fieldsList.Add(new Fields(1));
                    }
                }
                else
                {
                    for (int j = 0; j < message.fieldsList.Count; j++)
                    {
                        var item = message.fieldsList[j];
                        GUILayout.BeginHorizontal();
                        
                        // 修饰符类型
                        item.modifier = (ModifierType)EditorGUILayout.EnumPopup(item.modifier);
                        
                        // 字段类型
                        item.type = (FieldsType)EditorGUILayout.EnumPopup(item.type);
                        if (item.type == FieldsType.Custom)
                        {
                            item.typeName = GUILayout.TextField(item.typeName);
                        }
                        
                        // 编辑字段名
                        item.name = EditorGUILayout.TextField(item.name);
                        GUILayout.Label("=", GUILayout.Width(15f));
                        
                        // 分配标识号
                        item.flag = EditorGUILayout.IntField(item.flag, GUILayout.Width(50f));
                        
                        // 插入新字段
                        if (GUILayout.Button("+", GUILayout.Width(20f)))
                        {
                            message.fieldsList.Insert(j + 1, new Fields(message.fieldsList.Count + 1));
                            Repaint();
                            return;
                        }
                        
                        // 删除该字段
                        if (GUILayout.Button("-", GUILayout.Width(20f)))
                        {
                            message.fieldsList.Remove(item);
                            Repaint();
                            return;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
        }
        GUILayout.EndScrollView();
    }

    // 添加底部菜单GUI方法
    private void OnBottomMenuGUI()
    {
        GUILayout.BeginHorizontal();
        
        // 添加新类按钮
        if (GUILayout.Button("Add Message"))
        {
            Message message = new Message();
            messages.Add(message);
            foldoutDic.Add(message, true);
            Repaint();
        }
        
        
        // 保存按钮
        GUI.enabled = !string.IsNullOrEmpty(fileName);
        if (GUILayout.Button("Save"))
        {
            // 保存逻辑可以根据需要添加
            UnityEngine.Debug.Log("Save proto file: " + fileName);
        }
        GUI.enabled = true;
        
        GUILayout.EndHorizontal();
    }
}

// 添加Message的消息类
public class Message
{
    public string name = "NewMessage";
    public List<Fields> fieldsList = new List<Fields>();
}

// 添加Message的字段类
public class Fields
{
    public ModifierType modifier = ModifierType.None;
    public FieldsType type = FieldsType.Default;
    public string name = "field";
    public int flag;
    public string typeName = "";

    public Fields(int flag)
    {
        this.flag = flag;
    }
}

// 添加Message的修饰符类型枚举
public enum ModifierType
{
    None,
    Required,
    Optional,
    Repeated
}

// 添加Message的字段类型枚举
public enum FieldsType
{
    Default,
    Int32,
    Int64,
    UInt32,
    UInt64,
    Float,
    Double,
    Bool,
    String,
    Bytes,
    Custom
}