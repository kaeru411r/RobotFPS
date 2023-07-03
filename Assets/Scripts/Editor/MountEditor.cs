
using UnityEditor;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;


[CustomPropertyDrawer(typeof(Mount))]
public class MountDrawer : PropertyDrawer
{
    static Dictionary<Mount, int> _nums = new Dictionary<Mount, int>();
    bool b = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            position.height = EditorGUIUtility.singleLineHeight;

            var mount = property.GetUnderlyingValue() as Mount;

            var mountNameRect = new Rect(position)
            {
                y = position.y
            };
            var mountNameProperty = property.FindPropertyRelative("_name");
            mountNameProperty.stringValue = EditorGUI.TextField(mountNameRect, "名前", mountNameProperty.stringValue);
            var supportedUnitProperty = property.FindPropertyRelative("_supportedUnits");

            var unitRect = new Rect(mountNameRect)
            {
                y = mountNameRect.y + EditorGUIUtility.singleLineHeight + 2f
            };

            var units = new Unit[supportedUnitProperty.arraySize];
            var names = new string[supportedUnitProperty.arraySize];
            for (int i = 0; i < supportedUnitProperty.arraySize; i++)
            {
                units[i] = supportedUnitProperty.GetArrayElementAtIndex(i).objectReferenceValue as Unit;
                names[i] = units[i]?.name;
            }
            if (!_nums.ContainsKey(mount))
            {
                _nums.Add(mount, 0);
            }

            var unit = mount.Unit;
            _nums[mount] = 0;

            for (int i = 0; i < units.Length; i++)
            {
                if (unit && units[i] && units[i].name == unit.name)
                {
                    _nums[mount] = i;
                }
            }

            _nums[mount] = EditorGUI.Popup(unitRect, "装備するユニット", _nums[mount], names);
            unit = mount.Unit;

            //if (unit?.name != units[_nums[mount]]?.name)
            //{
            //    //mount.Unit = units[_nums[mount]];
            //    property.FindPropertyRelative("_unit").SetUnderlyingValue(units[_nums[mount]]);
            //    Debug.Log(2);
            //}

            var supportedUnitRect = new Rect(unitRect)
            {
                y = unitRect.y + EditorGUIUtility.singleLineHeight + 2f
            };

            EditorGUI.PropertyField(supportedUnitRect, supportedUnitProperty, new GUIContent("装備可能ユニット"));
        }
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = EditorGUIUtility.singleLineHeight;
        var supportedUnitProperty = property.FindPropertyRelative("_supportedUnits");
        if (!supportedUnitProperty.isExpanded)
        {
            height *= 4;
        }
        else if (supportedUnitProperty.arraySize == 0)
        {
            height *= 6;
        }
        else
        {
            var a = height * 1.1f;
            height = height * 5 + a * supportedUnitProperty.arraySize;
        }

        return height;
    }
}
