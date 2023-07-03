
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(RobotBase))]
public class RobotEditor : Editor
{
    ReorderableList _reorderableList;
    SerializedProperty _mountList;
    bool _isMountOpen = false;

    private void OnEnable()
    {
        _mountList = serializedObject.FindProperty("_mounts");

        _reorderableList = new ReorderableList(serializedObject, _mountList);
        _reorderableList.drawElementCallback = (rect, index, active, focused) =>
        {
            var actionData = _mountList.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, actionData);
        };
        _reorderableList.headerHeight = 0f;
        _reorderableList.elementHeightCallback = index => EditorGUI.GetPropertyHeight(_mountList.GetArrayElementAtIndex(index));
    }
    public override void OnInspectorGUI()
    {
        var robo = target as RobotBase;
        _isMountOpen = EditorGUILayout.Foldout(_isMountOpen, "aa");
        if (_isMountOpen)
        {
            _reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

