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
        var isPlaying = UnityEngine.Application.isPlaying;
        if (isPlaying) { }
        else if (!isPrefabMode)
        {
            EditorGUILayout.LabelField("", "プレハブモード以外で設定することは出来ません");
        }
        else if (isEditaleRoot)
        {
            EditorGUILayout.LabelField("", "プレハブのルートが異なるため設定することが出来ません");
        }
        EditorGUI.BeginDisabledGroup(!isPlaying && (!isPrefabMode || isEditaleRoot));
        base.OnInspectorGUI();
        EditorGUI.EndDisabledGroup();
    }
}