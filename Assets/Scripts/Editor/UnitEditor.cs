using System.Diagnostics;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;

[CustomEditor(typeof(UnitBase))]
public class UnitEditor : Editor
{
    private void OnDisable()
    {
        
    }

    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        if (PrefabStageUtility.GetCurrentPrefabStage() != null)
        {
            base.OnInspectorGUI();
        }
    }
}