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




        listSize = mounts.arraySize;       //配列weapons　の長さを一時的に変数に保存しておく


        listSize = EditorGUILayout.IntField("Size", listSize);//一時的に保存した配列の長さをカスタムインスペクタに描画（ここで書き換えも可能）


        EditorGUILayout.Space();       //スペースを描画

        //一時的に保存した配列の長さと、本来の配列の長さが同じかチェックする
        if (listSize != mounts.arraySize)
        {
            //長さが違う場合は


            mounts.arraySize = listSize;     //長さの変更を適用

            //ここでserializedObjectへの変更を適用し、再び更新する
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        else
        {
            //一時的に保存した配列の長さと、本来の配列の長さが同じ場合は　配列の要素を描画する                 
            for (int i = 0; i < mounts.arraySize; ++i)
            {

                robot.Mounts[i].Unit = EditorGUILayout.ObjectField(robot.Mounts[i].Unit, typeof(UnitBase), true) as UnitBase;

                //player.Mounts[i].power = EditorGUILayout.IntField("Power:", player.weapons[i].power);

                //player.Mounts[i].accessory.name = EditorGUILayout.TextField("accessory name:", player.weapons[i].accessory.name);


                EditorGUILayout.Space();  //スペースを描画
            }
        }



        serializedObject.ApplyModifiedProperties(); //serializedObjectへの変更を適用
    }


}