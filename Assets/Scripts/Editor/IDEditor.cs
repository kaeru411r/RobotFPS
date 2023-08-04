using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.Cryptography;
using System;

[CustomPropertyDrawer(typeof(ID))]
public class IDEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            position.height = EditorGUIUtility.singleLineHeight;

            var idRect = new Rect(position)
            {
                y = position.y
            };
            var mountNameProperty = property.FindPropertyRelative("_id");
            var idPropatyValue = BitConverter.ToUInt32(BitConverter.GetBytes(mountNameProperty.intValue));
            EditorGUI.TextField(idRect, "ID", idPropatyValue.ToString("D10"));
            if (idPropatyValue == 0746421240) { }
        }
    }
}
