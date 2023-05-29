using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public enum Fate
{
    BurnInHell = -9,
    NoPurification = -6,
    SlightSinner = -3,
    DeservePurification = 0,
    GoodFellow = 3,
    Righteous = 6,
}

public class FateJournal : MonoBehaviour
{
    [SerializeField] private ResultPanel _resultPanel;

    [Header("Journal")]
    [SerializeField] private GameObject _journalPanel;
    [SerializeField] private Transform _sinTransform;
    [SerializeField] private Transform _virtueTransform;
    [SerializeField] private GameObject _sinPrefab;
    [SerializeField] private GameObject _virtuePrefab;

    private Vector3 _deedOffset = new Vector3(0, -100, 0);
    private List<Deed> _sinsFound = new List<Deed>();
    private List<Deed> _virtuesFound = new List<Deed>();
    private SoulFacts _soulFacts;

    public void OpenJournal(SoulFacts soulFacts, List<Deed> sinsFound, List<Deed> virtuesFound)
    {
        _soulFacts = soulFacts;
        _sinsFound = sinsFound;
        _virtuesFound = virtuesFound;

        _journalPanel.SetActive(true);
        ShowFoundedDeeds();
    }

    public void ChooseFate(int fateValue)
    {
        bool isPLayerRight = CheckResult((Fate)fateValue, out Fate rightFate);
        _resultPanel.OpenResultPanel(rightFate, isPLayerRight);

        _journalPanel.SetActive(false);
    }

    private bool CheckResult(Fate playerDecision, out Fate rightFate)
    {
        int properFatePoints = _soulFacts.GetWeightResult();
        rightFate = CalculateFate(properFatePoints);
        
        return Math.Abs((int)rightFate - (int)playerDecision) <= 3;
    }

    private void ShowFoundedDeeds()
    {
        for (int i = 0; i < _sinsFound.Count; i++)
        {
            GameObject sin = Instantiate(_sinPrefab, _sinTransform.position + _deedOffset * i, Quaternion.identity, _sinTransform);
            TextMeshProUGUI[] text = sin.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].text = _virtuesFound[i].GetDescription();
        }
        for (int i = 0; i < _virtuesFound.Count; i++)
        {
            GameObject virtue = Instantiate(_virtuePrefab, _virtueTransform.position + _deedOffset * i, Quaternion.identity, _virtueTransform);
            TextMeshProUGUI[] text = virtue.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].text = _virtuesFound[i].GetDescription();
        }
    }

    private Fate CalculateFate(int fatePoints)
    {
        int clampedFate = (int)Fate.Righteous;
        while (fatePoints < clampedFate)
        {
            clampedFate -= 3;
        }

        return (Fate)clampedFate;
    }
}