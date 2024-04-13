using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoulFacts
{
    [SerializeField] private List<string> _factsOffContext;
    [SerializeField] private List<Deed> _sins;
    [SerializeField] private List<Deed> _virtues;

    public Fact GetFact(int index)
    {
        return new Fact(_factsOffContext[index]);
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
        var result = 0;
        foreach (var virtue in _virtues)
        {
            result += virtue.GetWeight();
        }

        foreach (var sin in _sins)
        {
            result -= sin.GetWeight();
        }

        return result;
    }
}
