using System.Collections.Generic;
using UnityEngine;

public class SoulFacts : MonoBehaviour
{
    [SerializeField] private List<Fact> _facts;
    [SerializeField] private List<Deed> _sins;
    [SerializeField] private List<Deed> _virtues;

    public Fact GetFact(int index)
    {
        return _facts[index];
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
            result += virtue.Weight;
        }

        foreach (var sin in _sins)
        {
            result -= sin.Weight;
        }

        return result;
    }
}
