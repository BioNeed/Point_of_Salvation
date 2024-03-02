using System;
using UnityEngine;

[Serializable]
public class Deed
{
    [SerializeField] private string _description;
    [SerializeField] private int _weight;

    public int GetWeight()
    {
        return _weight;
    }

    public string GetDescription()
    {
        return _description;
    }
}