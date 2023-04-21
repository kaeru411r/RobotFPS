using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(RobotBase))]
public class RobotEditor : Editor
{



    public override void OnInspectorGUI()
    {
        int listSize = 0;
        serializedObject.Update();

        base.OnInspectorGUI();

        RobotBase robot = target as RobotBase;

        var mounts = serializedObject.FindProperty("_mounts");




        listSize = mounts.arraySize;       //�z��weapons�@�̒������ꎞ�I�ɕϐ��ɕۑ����Ă���


        listSize = EditorGUILayout.IntField("Size", listSize);//�ꎞ�I�ɕۑ������z��̒������J�X�^���C���X�y�N�^�ɕ`��i�����ŏ����������\�j


        EditorGUILayout.Space();       //�X�y�[�X��`��

        //�ꎞ�I�ɕۑ������z��̒����ƁA�{���̔z��̒������������`�F�b�N����
        if (listSize != mounts.arraySize)
        {
            //�������Ⴄ�ꍇ��


            mounts.arraySize = listSize;     //�����̕ύX��K�p

            //������serializedObject�ւ̕ύX��K�p���A�ĂэX�V����
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        else
        {
            //�ꎞ�I�ɕۑ������z��̒����ƁA�{���̔z��̒����������ꍇ�́@�z��̗v�f��`�悷��                 
            for (int i = 0; i < mounts.arraySize; ++i)
            {

                robot.Mounts[i].Unit = EditorGUILayout.ObjectField(robot.Mounts[i].Unit, typeof(UnitBase), true) as UnitBase;

                //player.Mounts[i].power = EditorGUILayout.IntField("Power:", player.weapons[i].power);

                //player.Mounts[i].accessory.name = EditorGUILayout.TextField("accessory name:", player.weapons[i].accessory.name);


                EditorGUILayout.Space();  //�X�y�[�X��`��
            }
        }



        serializedObject.ApplyModifiedProperties(); //serializedObject�ւ̕ύX��K�p
    }


}