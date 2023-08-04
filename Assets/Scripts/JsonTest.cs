using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions.Must;
using UnityEngine.ResourceManagement.AsyncOperations;

public class JsonTest : MonoBehaviour
{
    [SerializeField]
    RobotBase robot;
    [SerializeField]
    AssetReferenceT<GameObject> _assetReference;
    [SerializeField]
    string _assetName;

    GameObject a;
    GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        _assetReference.InstantiateAsync().Completed += (h) => g = h.Result;
        //_assetName = _assetReference.SubObjectName;
        Addressables.LoadAssetAsync<GameObject>(_assetName).Completed += (h) => a = h.Result;
        var json = JsonUtility.ToJson(robot);
        Instantiate(JsonUtility.FromJson<RobotBase>(json));
        _assetReference.LoadAssetAsync<GameObject>().Completed += (data) =>
        {
            if (!(data.Result as GameObject).TryGetComponent<Unit>(out Unit _))
            {
                _assetReference = null;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (a && g)
        {
            Debug.Log($"{g.name}, {a.name}");
        }
    }

    private void OnValidate()
    {
    }

}
