using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigurable
{
    public string Seve();

    public void Load(string json);
}