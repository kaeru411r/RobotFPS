using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

[Serializable]
public class ID
{

    [SerializeField, ReadOnly, Tooltip("ID")]
    uint _id;

    static List<uint> registeredIDs = new List<uint>();

    public uint Id { get => _id;}

    static uint CreateID()
    {
        uint id;
        do
        {
            var idBit = BitConverter.GetBytes(RandomNumberGenerator.GetInt32(int.MinValue, int.MaxValue));
            id = BitConverter.ToUInt32(idBit);
        } while (registeredIDs.Contains(id));

        registeredIDs.Add(id);

        return id;
    }

    public ID()
    {
        _id = CreateID();
    }


    ~ID()
    {
        registeredIDs.Remove(_id);
    }
}