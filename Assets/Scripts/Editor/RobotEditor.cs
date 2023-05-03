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
//        EditorGUILayout.Space();       //�X�y�[�X��`��

//        if (a)
//        {
//            //EditorGUI.BeginChangeCheck();
//            listSize = EditorGUILayout.IntField("Size", listSize);//�ꎞ�I�ɕۑ������z��̒������J�X�^���C���X�y�N�^�ɕ`��i�����ŏ����������\�j
//            //Debug.Log(EditorGUI.EndChangeCheck());

//            EditorGUILayout.Space();       //�X�y�[�X��`��
//                                           //�ꎞ�I�ɕۑ������z��̒����ƁA�{���̔z��̒������������`�F�b�N����
//            if (listSize != mounts.arraySize)
//            {
//                //�������Ⴄ�ꍇ��

//                Mount[] buf = robot.Mounts;
//                Debug.Log($"{buf.Length}");

//                mounts.arraySize = listSize;     //�����̕ύX��K�p
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
//                //�ꎞ�I�ɕۑ������z��̒����ƁA�{���̔z��̒����������ꍇ�́@�z��̗v�f��`�悷��                 
//                for (int i = 0; i < mounts.arraySize; ++i)
//                {
//                    robot.Mounts[i].Unit = EditorGUILayout.ObjectField(robot.Mounts[i].Unit, typeof(UnitBase), true) as UnitBase;
//                    //EditorGUILayout.Space();  //�X�y�[�X��`��
//                }
//            }
//        }

//        EditorGUI.EndFoldoutHeaderGroup();

//        serializedObject.Update();


//        serializedObject.ApplyModifiedProperties();



//        serializedObject.ApplyModifiedProperties(); //serializedObject�ւ̕ύX��K�p
//    }


//    /// <summary>
//    /// ����
//    /// </summary>
//    public float Height { get { return _property.isExpanded ? (ArraySize + 1) * LineHeight : LineHeight; } }

//    private float ContentHeight { get; } = EditorGUIUtility.singleLineHeight;
//    private float LineHeight { get; } = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + 4;
//    private int ArraySize { get { return _property.arraySize; } }

//    /// <summary>
//    /// �w�b�_���`�悳�ꂽ�Ƃ��̃R�[���o�b�N
//    /// </summary>
//    public event System.Action<Rect> drawHeaderCallback;
//    /// <summary>
//    /// �v�f���`�悳�ꂽ�Ƃ��̃R�[���o�b�N
//    /// </summary>
//    public event System.Action<Rect, int> drawElementCallback;

//    private SerializedProperty _property;

//    /// <summary>
//    /// �R���X�g���N�^
//    /// </summary>
//    /// <param name="property">�z��̃v���p�e�B</param>
//    public RobotEditor(SerializedProperty property)
//    {
//        _property = property;

//        // �f�t�H���g�̃w�b�_�`�揈����o�^
//        drawHeaderCallback = rect =>
//        {
//            rect.xMin += 10;
//            EditorGUI.PropertyField(rect, property);
//        };
//        // �f�t�H���g�̗v�f�`�揈����o�^
//        drawElementCallback = (rect, index) =>
//        {
//            EditorGUI.PropertyField(rect, _property.GetArrayElementAtIndex(index));

//        };
//    }

//    /// <summary>
//    /// EditorGUILayout���ŕ`�悷��
//    /// </summary>
//    public void DoLayoutList()
//    {
//        var lastRect = GUILayoutUtility.GetRect(new GUIContent(""), GUIStyle.none);
//        var rect = GUILayoutUtility.GetRect(lastRect.width, Height);
//        DoList(rect);
//    }

//    /// <summary>
//    /// EditorGUI���ŕ`�悷��
//    /// </summary>
//    public void DoList(Rect position)
//    {
//        // �F�̒�`
//        var backgroundDefaultColor = GUI.backgroundColor;
//        var backgroundLightColor = backgroundDefaultColor;
//        var backgroundDarkColor = backgroundLightColor * 0.8f;
//        backgroundDarkColor.a = 1;

//        var isExpanded = _property.isExpanded;

//        // �O�g��`��
//        var outlineRect = position;
//        position.height = Height;
//        GUI.Box(outlineRect, "");

//        var fieldRect = position;
//        fieldRect.height = LineHeight;

//        // �w�b�_��`��
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

//                // �w�i��`��
//                var backgroundRect = fieldRect;
//                backgroundRect.xMin += 1;
//                backgroundRect.xMax -= 1;
//                if (ArraySize == i + 1)
//                {
//                    backgroundRect.yMax -= 1;
//                }
//                EditorGUI.DrawRect(backgroundRect, i % 2 == 0 ? backgroundDarkColor : backgroundLightColor);
//                // �v���p�e�B��`��
//                var propertyRect = GetContentRect(fieldRect);
//                drawElementCallback(propertyRect, i);

//                // �}�C�i�X�{�^����`��
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