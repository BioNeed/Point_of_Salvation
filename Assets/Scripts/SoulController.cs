using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoulController : MonoBehaviour
{
    [SerializeField] private Color _mouseOverColor;
    [SerializeField] private string _occupation;
    [SerializeField] private string _distinctiveFeature;
    [SerializeField] private GameObject _dialogIndicator;
    [SerializeField] private SoulFacts _soulFacts;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset _inkJSON;

    private Material _material;
    private Color _startColor;
    private int _deedsResult;
    private void Start()
    {
        Renderer renderer = GetComponent<MeshRenderer>();
        _material = renderer.material;
        _startColor = _material.color;
        _dialogIndicator.SetActive(false);
        _deedsResult = _soulFacts.GetWeightResult();
    }

    private void OnMouseEnter()
    {
        _material.color = _mouseOverColor;
    }

    private void OnMouseExit()
    {
        _material.color = _startColor;
    }

    public Color GetColor()
    {
        return _mouseOverColor;
    }

    public string GetOccupation()
    {
        return _occupation;
    }

    public string GetDistinctive()
    {
        return _distinctiveFeature;
    }

    public TextAsset GetInkFile()
    {
        return _inkJSON;
    }

    public void DialogIndicatorOn()
    {
        _dialogIndicator.SetActive(true);
    }

    public void DialogIndicatorOff()
    {
        _dialogIndicator.SetActive(false);
    }

    public string GetSoulFact(int index)
    {
        return _soulFacts.GetFact(index);
    }

    public Deed GetSoulSin(int index)
    {
        return _soulFacts.GetSin(index);
    }

    public Deed GetSoulVirtue(int index)
    {
        return _soulFacts.GetVirtue(index);
    }
}

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
        foreach(Deed virtue in _virtues)
        {
            result += virtue.GetWeight();
        }
        foreach(Deed sin in _sins)
        {
            result -= sin.GetWeight();
        }
        return result;
    }
}

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
