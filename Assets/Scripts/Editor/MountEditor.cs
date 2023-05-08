using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Mount))]
public class MountEditor : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 16f * 3;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

    }
}
