using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FateJournal : MonoBehaviour
{
    [SerializeField] private DialogueFactView _dialogueFactView;
    [SerializeField] private DeedsFinding _deedsFinding;
    [SerializeField] private PlayerStateMutable _playerState;
    [SerializeField] private ResultPanel _resultPanel;

    [Header("Journal")]
    [SerializeField] private GameObject _journalPanel;
    [SerializeField] private Transform _sinInitialTransform;
    [SerializeField] private Transform _virtueInitialTransform;
    [SerializeField] private GameObject _sinPrefab;
    [SerializeField] private GameObject _virtuePrefab;

    private readonly Vector3 _deedOffset = new Vector3(0, -100, 0);
    private SoulFacts _soulFacts;

    private void Start()
    {
        _journalPanel.SetActive(false);
    }

    public void OpenJournal(SoulFacts soulFacts)
    {
        _playerState.EnterJournal();
        _soulFacts = soulFacts;
        _journalPanel.SetActive(true);
        ShowFoundedDeeds();
        _dialogueFactView.TryDisplayJournalFact();
    }

    public void ChooseFate(int fateValue)
    {
        bool isPLayerRight = CheckResult((Fate)fateValue, out Fate rightFate);
        _resultPanel.OpenResultPanel(rightFate, isPLayerRight);

        _journalPanel.SetActive(false);
        _dialogueFactView.HideJournalFactText();
        ClearDeedsList();
        _deedsFinding.ClearFoundDeeds();
    }

    private void ClearDeedsList()
    {
        foreach (Transform sin in _sinInitialTransform)
        {
            Destroy(sin.gameObject);
        }

        foreach (Transform virtue in _virtueInitialTransform)
        {
            Destroy(virtue.gameObject);
        }
    }

    private bool CheckResult(
        Fate playerDecision, 
        out Fate rightFate)
    {
        var properFatePoints = _soulFacts.GetWeightResult();
        rightFate = CalculateFate(properFatePoints);
        
        return Math.Abs((int)rightFate - (int)playerDecision) <= 3;
    }

    private void ShowFoundedDeeds()
    {
        FillJournalWithDeeds(
            _deedsFinding.SinsFound,
            _sinPrefab,
            _sinInitialTransform);

        FillJournalWithDeeds(
            _deedsFinding.VirtuesFound,
            _virtuePrefab,
            _virtueInitialTransform);
    }

    private void FillJournalWithDeeds(
        IEnumerable<Deed> deedsFound,
        GameObject deedPrefab,
        Transform deedInitialTransform)
    {
        var deedPosition = 0;
        foreach (var deedFound in deedsFound)
        {
            var deedGameObject = Instantiate(
                deedPrefab,
                deedInitialTransform.position + _deedOffset * deedPosition,
                Quaternion.identity,
                deedInitialTransform);

            var tmpGUI = deedGameObject.GetComponentInChildren<TextMeshProUGUI>();
            tmpGUI.text = deedFound.GetDescription();
            deedPosition++;
        }
    }

    private Fate CalculateFate(int fatePoints)
    {
        var clampedFate = (int)Fate.Righteous;
        while (fatePoints < clampedFate)
        {
            clampedFate -= 3;
        }

        return (Fate)clampedFate;
    }
}
