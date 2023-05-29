using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoulFacts
{
    [SerializeField] private List<string> _factsOffContext;
    [SerializeField] private List<Deed> _sins;
    [SerializeField] private List<Deed> _virtues;

    public string GetFact(int index)
    {
        return _factsOffContext[index];
    }

    public Deed GetSin(int index)
    {
        return _sins[index];
    }

    public Deed GetVirtue(int index)
    {
        return _virtues[index];
    }

    public int GetWeightResult()
    {
        int result = 0;
        foreach (Deed virtue in _virtues)
        {
            result += virtue.GetWeight();
        }
        foreach (Deed sin in _sins)
        {
            result -= sin.GetWeight();
        }
        return result;
    }
}
