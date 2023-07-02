
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(RobotBase))]
public class RobotEditor : Editor
{
    private ReorderableList _reorderableList;
    private SerializedProperty _mountList;
    private void OnEnable()
    {
        _mountList = serializedObject.FindProperty("_mounts");

        _reorderableList = new ReorderableList(serializedObject, _mountList);
        _reorderableList.drawElementCallback = (rect, index, active, focused) =>
        {
            var actionData = _mountList.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, actionData);
        };
        _reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Mount List");
        _reorderableList.elementHeightCallback = index => EditorGUI.GetPropertyHeight(_mountList.GetArrayElementAtIndex(index));
    }
    public override void OnInspectorGUI()
    {

        _reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

    }
}

