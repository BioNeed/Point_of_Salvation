using UnityEngine;
using UnityEngine.UI;

public class PortraitPanel : MonoBehaviour
{
    [SerializeField] private CurrentSoulContext _currentSoulContext;
    [SerializeField] private Image _soulColorImage;
    [SerializeField] private Text _occupationText;
    [SerializeField] private Text _distinctiveText;

    public void DisplaySoulPortret()
    {
        var soulPortraitInfo = _currentSoulContext.CurrentSoul.PortraitInfo;
        var color = soulPortraitInfo.MouseOverColor;
        _soulColorImage.color = new Color(color.r, color.g, color.b);
        _occupationText.text = soulPortraitInfo.Occupation;
        _distinctiveText.text = soulPortraitInfo.DistinctiveFeature;
    }
}
