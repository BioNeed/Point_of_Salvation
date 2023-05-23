using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<SoulController> _soulsList;

    private SoulController _currentSoul;
    private int _soulIndex;

    private void Start()
    {
        _currentSoul = _soulsList[0];
        _soulIndex = 0;
    }

    public SoulController GetCurrentSoul()
    {
        return _currentSoul;
    }

    public void NextSoul()
    {
        _soulIndex++;
        _currentSoul = _soulsList[_soulIndex];
    }
}
