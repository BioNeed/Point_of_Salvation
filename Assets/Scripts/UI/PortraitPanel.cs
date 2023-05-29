using UnityEngine;
using UnityEngine.UI;

public class PortraitPanel : MonoBehaviour
{
    [SerializeField] private Image _soulColorImage;
    [SerializeField] private Text _occupationText;
    [SerializeField] private Text _distinctiveText;

    private Color _soulColor;

    public void DisplaySoulPortret(Soul soul)
    {
        _soulColor = soul.GetColor();
        _soulColorImage.color = new Color(_soulColor.r, _soulColor.g, _soulColor.b);
        _occupationText.text = "Занятие - " + soul.GetOccupation();
        _distinctiveText.text = "Особенность - " + soul.GetDistinctive();
    }
}
