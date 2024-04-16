using System.Collections.Generic;
using UnityEngine;

public class DeedsFinding : MonoBehaviour
{
    private readonly List<Deed> _virtuesFound = new List<Deed>();
    private readonly List<Deed> _sinsFound = new List<Deed>();

    public IEnumerable<Deed> VirtuesFound => _virtuesFound;

    public IEnumerable<Deed> SinsFound => _sinsFound;

    public Fact FoundFact { get; set; }

    public void AddFoundVirtue(Deed foundVirtue)
    {
        _virtuesFound.Add(foundVirtue);
    }

    public void AddFoundSin(Deed foundSin)
    {
        _sinsFound.Add(foundSin);
    }

    public void ClearFoundDeeds()
    {
        _virtuesFound.Clear();
        _sinsFound.Clear();
        FoundFact = null;
    }
}
