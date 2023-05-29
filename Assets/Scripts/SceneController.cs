using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PortraitPanel _portraitPanel;
    [SerializeField] private List<Soul> _soulsList;

    private Soul _currentSoul;
    private int _soulIndex;

    private void Start()
    {
        _currentSoul = _soulsList[0];
        _soulIndex = 0;
        _portraitPanel.DisplaySoulPortret(_currentSoul);
    }

    public Soul GetCurrentSoul()
    {
        return _currentSoul;
    }

    public void NextSoul()
    {
        if (_soulIndex + 1 == _soulsList.Count)
        {
            _currentSoul = null;
            return;
        }

        _soulIndex++;
        _currentSoul = _soulsList[_soulIndex];
        _portraitPanel.DisplaySoulPortret(_currentSoul);
    }
}
