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
    // 定数の名前
    private readonly string[] menuNames;
    // 定数の値
    private readonly string[] menuValues;

    /// <summary>
    /// 引数で指定された型の定数のみをドロップダウンで入力できるようにする属性
    /// </summary>
    /// <param name="constantsClass">定数を取得するクラス</param>
    /// <param name="bindingFlags">取得する定数の設定用フラグ</param>
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
        // 現在選択されている定数のインデックス
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

            // ドロップダウンメニューを表示
            index = EditorGUI.Popup(position, label.text, index, targetAttribute.menuNames);
            property.stringValue = targetAttribute.menuValues[index];
        }
    }
#endif
}