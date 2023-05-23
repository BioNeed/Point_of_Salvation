using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private Image _soulColorImage;
    [SerializeField] private Text _occupationText;
    [SerializeField] private Text _distinctiveText;

    private SoulController _soulController;
    private Color _soulColor;

    private void Start()
    {
        _soulController = _sceneController.GetCurrentSoul();
        _soulColor = _soulController.GetColor();
        _soulColorImage.color = new Color(_soulColor.r, _soulColor.g, _soulColor.b);
        _occupationText.text = "Занятие - " + _soulController.GetOccupation();
        _distinctiveText.text = "Особенность - " + _soulController.GetDistinctive();
    }

}
