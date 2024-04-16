using System;
using UnityEngine;

public class FateJournal : MonoBehaviour
{
    [SerializeField] private CurrentSoulContext _currentSoulContext;
    [SerializeField] private JournalPanel _journalPanel;
    [SerializeField] private DeedsFinding _deedsFinding;
    [SerializeField] private PlayerStateMutable _playerState;
    [SerializeField] private ResultPanel _resultPanel;

    private SoulFacts _soulFacts;

    public void OpenJournal()
    {
        _playerState.EnterJournal();
        _soulFacts = _currentSoulContext.CurrentSoul.Facts;
        _journalPanel.ShowJournalInfo();
    }

    public void CheckResultOnChosenFate(Fate chosenFate)
    {
        var isPLayerRight = CheckResult(chosenFate, out Fate rightFate);
        _resultPanel.OpenResultPanel(rightFate, isPLayerRight);
        CloseJournal();
    }

    private void CloseJournal()
    {
        _journalPanel.ClearJournalInfo();
        _deedsFinding.ClearFoundDeeds();
    }

    private bool CheckResult(
        Fate playerDecision, 
        out Fate rightFate)
    {
        var properFatePoints = _soulFacts.GetWeightResult();
        rightFate = CalculateFate(properFatePoints);
        
        return Math.Abs((int)rightFate - (int)playerDecision) <= 3;
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
