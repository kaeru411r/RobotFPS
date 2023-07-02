using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class DropdownConstantsAttribute : PropertyAttribute
{
    // �萔�̖��O
    private readonly string[] menuNames;
    // �萔�̒l
    private readonly string[] menuValues;

    /// <summary>
    /// �����Ŏw�肳�ꂽ�^�̒萔�݂̂��h���b�v�_�E���œ��͂ł���悤�ɂ��鑮��
    /// </summary>
    /// <param name="constantsClass">�萔���擾����N���X</param>
    /// <param name="bindingFlags">�擾����萔�̐ݒ�p�t���O</param>
    public DropdownConstantsAttribute(Type constantsClass,
        BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
    {
        var fields = constantsClass.GetFields(bindingFlags);
        var menuDictionary = new Dictionary<string, string>(fields.Length);
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(string))
            {
                menuDictionary.Add(field.Name, field.GetValue(constantsClass) as string);
            }
        }

        menuNames = menuDictionary.Keys.ToArray();
        menuValues = menuDictionary.Values.ToArray();
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DropdownConstantsAttribute))]
    public class DropdownConstantsAttributeDrawer : PropertyDrawer
    {
        // ���ݑI������Ă���萔�̃C���f�b�N�X
        private int index = -1;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.type != "string") return;

            var targetAttribute = (DropdownConstantsAttribute)attribute;

            if (index < 0)
            {
                index = string.IsNullOrEmpty(property.stringValue)
                    ? 0
                    : Array.IndexOf(targetAttribute.menuValues, property.stringValue);
            }

            // �h���b�v�_�E�����j���[��\��
            index = EditorGUI.Popup(position, label.text, index, targetAttribute.menuNames);
            property.stringValue = targetAttribute.menuValues[index];
        }
    }
#endif
}