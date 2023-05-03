//using UnityEngine;
//using UnityEditor;


//[CustomEditor(typeof(RobotBase))]
//public class RobotEditor : Editor
//{


//    bool a = false;

//    public override void OnInspectorGUI()
//    {
//        int listSize = 0;
//        serializedObject.Update();

//        base.OnInspectorGUI();

//        RobotBase robot = target as RobotBase;

//        var mounts = serializedObject.FindProperty("_mounts");

//        listSize = mounts.arraySize;
//        a = EditorGUI.BeginFoldoutHeaderGroup(new Rect(20, 20, 70, 200), a, "a");
//        EditorGUILayout.Space();       //スペースを描画

//        if (a)
//        {
//            //EditorGUI.BeginChangeCheck();
//            listSize = EditorGUILayout.IntField("Size", listSize);//一時的に保存した配列の長さをカスタムインスペクタに描画（ここで書き換えも可能）
//            //Debug.Log(EditorGUI.EndChangeCheck());

//            EditorGUILayout.Space();       //スペースを描画
//                                           //一時的に保存した配列の長さと、本来の配列の長さが同じかチェックする
//            if (listSize != mounts.arraySize)
//            {
//                //長さが違う場合は

//                Mount[] buf = robot.Mounts;
//                Debug.Log($"{buf.Length}");

//                mounts.arraySize = listSize;     //長さの変更を適用
//                serializedObject.ApplyModifiedProperties();
//                Debug.Log($"a{buf.Length}, {listSize}");


//                EditorGUI.PropertyField(new Rect(0, 0, 100, 200), mounts);

//                for (int i = 0; i < Mathf.Min(buf.Length, listSize); i++)
//                {
//                    robot.Mounts[i] = buf[i];
//                }

//                if (buf.Length < listSize)
//                {
//                    robot.Mounts[listSize - 1] = new Mount(robot);
//                }


//                //serializedObject.Update();
//            }
//            else
//            {
//                //一時的に保存した配列の長さと、本来の配列の長さが同じ場合は　配列の要素を描画する                 
//                for (int i = 0; i < mounts.arraySize; ++i)
//                {
//                    robot.Mounts[i].Unit = EditorGUILayout.ObjectField(robot.Mounts[i].Unit, typeof(UnitBase), true) as UnitBase;
//                    //EditorGUILayout.Space();  //スペースを描画
//                }
//            }
//        }

//        EditorGUI.EndFoldoutHeaderGroup();

//        serializedObject.Update();


//        serializedObject.ApplyModifiedProperties();



//        serializedObject.ApplyModifiedProperties(); //serializedObjectへの変更を適用
//    }


//    /// <summary>
//    /// 高さ
//    /// </summary>
//    public float Height { get { return _property.isExpanded ? (ArraySize + 1) * LineHeight : LineHeight; } }

//    private float ContentHeight { get; } = EditorGUIUtility.singleLineHeight;
//    private float LineHeight { get; } = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + 4;
//    private int ArraySize { get { return _property.arraySize; } }

//    /// <summary>
//    /// ヘッダが描画されたときのコールバック
//    /// </summary>
//    public event System.Action<Rect> drawHeaderCallback;
//    /// <summary>
//    /// 要素が描画されたときのコールバック
//    /// </summary>
//    public event System.Action<Rect, int> drawElementCallback;

//    private SerializedProperty _property;

//    /// <summary>
//    /// コンストラクタ
//    /// </summary>
//    /// <param name="property">配列のプロパティ</param>
//    public RobotEditor(SerializedProperty property)
//    {
//        _property = property;

//        // デフォルトのヘッダ描画処理を登録
//        drawHeaderCallback = rect =>
//        {
//            rect.xMin += 10;
//            EditorGUI.PropertyField(rect, property);
//        };
//        // デフォルトの要素描画処理を登録
//        drawElementCallback = (rect, index) =>
//        {
//            EditorGUI.PropertyField(rect, _property.GetArrayElementAtIndex(index));

//        };
//    }

//    /// <summary>
//    /// EditorGUILayout環境で描画する
//    /// </summary>
//    public void DoLayoutList()
//    {
//        var lastRect = GUILayoutUtility.GetRect(new GUIContent(""), GUIStyle.none);
//        var rect = GUILayoutUtility.GetRect(lastRect.width, Height);
//        DoList(rect);
//    }

//    /// <summary>
//    /// EditorGUI環境で描画する
//    /// </summary>
//    public void DoList(Rect position)
//    {
//        // 色の定義
//        var backgroundDefaultColor = GUI.backgroundColor;
//        var backgroundLightColor = backgroundDefaultColor;
//        var backgroundDarkColor = backgroundLightColor * 0.8f;
//        backgroundDarkColor.a = 1;

//        var isExpanded = _property.isExpanded;

//        // 外枠を描画
//        var outlineRect = position;
//        position.height = Height;
//        GUI.Box(outlineRect, "");

//        var fieldRect = position;
//        fieldRect.height = LineHeight;

//        // ヘッダを描画
//        var headerRect = fieldRect;
//        var plusButtonRect = fieldRect;
//        plusButtonRect.xMin = fieldRect.xMax - fieldRect.height;
//        headerRect.xMax -= plusButtonRect.width - 1;
//        GUI.Box(headerRect, "");
//        drawHeaderCallback(GetContentRect(headerRect));
//        if (GUI.Button(plusButtonRect, "+", GUI.skin.box))
//        {
//            _property.arraySize++;
//        }

//        if (isExpanded)
//        {
//            for (int i = 0; i < ArraySize; i++)
//            {
//                fieldRect.y += LineHeight;

//                // 背景を描画
//                var backgroundRect = fieldRect;
//                backgroundRect.xMin += 1;
//                backgroundRect.xMax -= 1;
//                if (ArraySize == i + 1)
//                {
//                    backgroundRect.yMax -= 1;
//                }
//                EditorGUI.DrawRect(backgroundRect, i % 2 == 0 ? backgroundDarkColor : backgroundLightColor);
//                // プロパティを描画
//                var propertyRect = GetContentRect(fieldRect);
//                drawElementCallback(propertyRect, i);

//                // マイナスボタンを描画
//                var minusButtonRect = fieldRect;
//                minusButtonRect.height -= 4;
//                minusButtonRect.y += 2;
//                minusButtonRect.xMin = minusButtonRect.xMax - minusButtonRect.height - 2;
//                minusButtonRect.xMax -= 2;
//                if (GUI.Button(minusButtonRect, "X"))
//                {
//                    _property.DeleteArrayElementAtIndex(i);
//                    break;
//                }
//            }
//        }
//    }

//    private Rect GetContentRect(Rect fieldRect)
//    {
//        fieldRect.height = ContentHeight;
//        fieldRect.y += (LineHeight - ContentHeight) / 2;
//        fieldRect.xMin += 4;
//        fieldRect.xMax -= 24;
//        return fieldRect;
//    }

//}