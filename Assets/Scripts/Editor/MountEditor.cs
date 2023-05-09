using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(Mount))]
//public class MountEditor : PropertyDrawer
//{
//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return 17f * 3;
//    }
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {

//        var unitProp = property.FindPropertyRelative("_unit");
//        var supportedUnitsProp = property.FindPropertyRelative("_supportedUnits");

//        EditorGUIUtility.wideMode = true;
//        EditorGUIUtility.labelWidth = 70;
//        position.height /= 3;
//        position.y += position.height;
//        EditorGUI.BeginFoldoutHeaderGroup(position, false, "aa"/*, GUIStyle.none*/);
//        position.y += position.height;
//        unitProp.objectReferenceValue = EditorGUI.ObjectField(position, unitProp.objectReferenceValue, typeof(UnitBase), false);
//        EditorGUI.EndFoldoutHeaderGroup();
//        //supportedUnitsProp.va = EditorGUI.Vector3Field(position, "Normal", supportedUnitsProp.vector3Value);
//    }
//}
