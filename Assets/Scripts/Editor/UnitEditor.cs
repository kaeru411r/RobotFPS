using System.Diagnostics;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;

[CustomEditor(typeof(Unit))]
public class UnitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        bool isPrefabMode = PrefabStageUtility.GetCurrentPrefabStage() == null;
        if(isPrefabMode)
        {
            EditorGUILayout.LabelField("", "�v���n�u���[�h�ȊO�Őݒ肷�邱�Ƃ͏o���܂���");
        }
        EditorGUI.BeginDisabledGroup(isPrefabMode);
        base.OnInspectorGUI();
        EditorGUI.EndDisabledGroup();
    }
}