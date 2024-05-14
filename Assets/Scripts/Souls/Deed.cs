﻿using System;
using UnityEngine;

[Serializable]
public class Deed
{
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private int _weight;

    public string Description => _description;

    public int Weight => _weight;
}
