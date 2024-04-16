using System;
using UnityEngine;

[Serializable]
public class Fact
{
    [SerializeField] private string _description;

    public string Description => _description;
}
