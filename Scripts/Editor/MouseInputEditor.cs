using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(MouseInput))]

public class MouseInputEditor : Editor
{

	// Use this for initialization
	void Start () {
	
	}
	
    void OnInspectorGUI ()
    {
        //EditorGUILayout.BeginVertical();
        EditorGUILayout.Foldout(true, "ass");
        //EditorGUILayout.EndVertical();
    }
}
