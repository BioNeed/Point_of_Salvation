using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
[RequireComponent(typeof(SoulPortraitInfo))]
public class SoulColorChanging : MonoBehaviour
{
    private SoulPortraitInfo _portraitInfo;
    private Material _material;
    private Color _startColor;

    private void Start()
    {
        var renderer = GetComponent<SkinnedMeshRenderer>();
        _material = renderer.material;
        _startColor = _material.color;
        _portraitInfo = GetComponent<SoulPortraitInfo>();
    }

    private void OnMouseEnter()
    {
        _material.color = _portraitInfo.MouseOverColor;
    }

    private void OnMouseExit()
    {
        _material.color = _startColor;
    }
}
