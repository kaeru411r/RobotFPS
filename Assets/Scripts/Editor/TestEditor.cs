using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(JsonTest))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var a = (JsonTest)target;
        var b = serializedObject.FindProperty("a");


    }
}
