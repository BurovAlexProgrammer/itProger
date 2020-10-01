using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    public string gg;

    [MenuItem("Tools/MyChecksum")]
    public static void SetChecksum()
    {
        EditorWindow.GetWindow<MyWindow>();

    }

    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Project checksum");
        var str = EditorGUILayout.TextField("");
        EditorGUILayout.EndHorizontal();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, 0.25f);
    }
}
