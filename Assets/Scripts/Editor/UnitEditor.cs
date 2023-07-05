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
            EditorGUILayout.LabelField("", "プレハブモード以外で設定することは出来ません");
        }
        EditorGUI.BeginDisabledGroup(isPrefabMode);
        base.OnInspectorGUI();
        EditorGUI.EndDisabledGroup();
    }
}