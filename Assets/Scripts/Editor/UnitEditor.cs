using System.Diagnostics;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;

[CustomEditor(typeof(Unit))]
public class UnitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var prefabNearestRoot = PrefabUtility.GetNearestPrefabInstanceRoot(target);
        var prefabRoot = (target as Unit).transform.root.gameObject;
        var currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        var isPrefabMode = currentPrefabStage != null;
        var isEditaleRoot = prefabNearestRoot != null && prefabRoot != prefabNearestRoot;
        if (!isPrefabMode)
        {
            EditorGUILayout.LabelField("", "�v���n�u���[�h�ȊO�Őݒ肷�邱�Ƃ͏o���܂���");
        }
        else if (isEditaleRoot)
        {
            EditorGUILayout.LabelField("", "�v���n�u���[�h�ȊO�Őݒ肷�邱�Ƃ͏o���܂���");
        }
        EditorGUI.BeginDisabledGroup(!isPrefabMode || isEditaleRoot);
        base.OnInspectorGUI();
        EditorGUI.EndDisabledGroup();
    }
}