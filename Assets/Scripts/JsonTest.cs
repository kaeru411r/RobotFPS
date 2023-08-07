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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnValidate()
    {
        a = null;
        //_assetName = _assetReference.SubObjectName;
        Addressables.LoadAssetAsync<GameObject>(_assetName).Completed += (h) =>
        {
            a = h.Result;
            if (a)
            {
                Debug.Log($"{a.name}");
            }
        };
        _assetReference.LoadAssetAsync<GameObject>().Completed += (data) =>
        {
            if (!(data.Result as GameObject).TryGetComponent<Unit>(out Unit _))
            {
                Debug.Log(1);
                _assetReference = null;
            }
        };
    }

}
