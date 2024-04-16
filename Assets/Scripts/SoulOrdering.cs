using System.Collections.Generic;
using UnityEngine;

public class SoulOrdering : MonoBehaviour
{
    [SerializeField] private PortraitPanel _portraitPanel;
    [SerializeField] private CurrentSoulContext _currentSoulContext;
    [SerializeField] private List<Soul> _soulsList;

    private int _soulIndex = 0;

    private void Start()
    {
        ChangeCurrentSoul(_soulIndex);
    }

    public void NextSoul()
    {
        if (_soulIndex + 1 == _soulsList.Count)
        {
            _currentSoulContext.SetCurrentSoul(null);
            return;
        }

        _soulIndex++;
        ChangeCurrentSoul(_soulIndex);
    }

    private void ChangeCurrentSoul(int newSoulIndex)
    {
        _currentSoulContext.SetCurrentSoul(_soulsList[newSoulIndex]);
        _portraitPanel.DisplaySoulPortret();
    }
}
