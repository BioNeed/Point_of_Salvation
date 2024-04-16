using UnityEngine;

public class SoulPortraitInfo : MonoBehaviour
{
    [SerializeField] private Color _mouseOverColor;
    [SerializeField] private string _occupation;
    [SerializeField] private string _distinctiveFeature;

    public Color MouseOverColor => _mouseOverColor;

    public string Occupation => _occupation;

    public string DistinctiveFeature => _distinctiveFeature;
}
