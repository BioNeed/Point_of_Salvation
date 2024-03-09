using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private Color _mouseOverColor;
    [SerializeField] private string _occupation;
    [SerializeField] private string _distinctiveFeature;
    [SerializeField] private GameObject _dialogIndicator;
    [SerializeField] private SoulFacts _soulFacts;
    [SerializeField] private bool _canTalk = true;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset _jsonDialogue;
    [SerializeField] private TextAsset _jsonPreDialogue;

    private Material _material;
    private Color _startColor;

    public SoulFacts SoulFacts => _soulFacts;
    public bool CanTalk => _canTalk;

    private void Start()
    {
        var renderer = GetComponent<SkinnedMeshRenderer>();
        _material = renderer.material;
        _startColor = _material.color;
        _dialogIndicator.SetActive(false);
    }

    private void OnMouseEnter()
    {
        _material.color = _mouseOverColor;
    }

    private void OnMouseExit()
    {
        _material.color = _startColor;
    }

    public Color GetColor()
    {
        return _mouseOverColor;
    }

    public string GetOccupation()
    {
        return _occupation;
    }

    public string GetDistinctive()
    {
        return _distinctiveFeature;
    }

    public TextAsset GetDialogue()
    {
        return _jsonDialogue;
    }

    public TextAsset GetPreDialogue()
    {
        return _jsonPreDialogue;
    }

    public void DialogIndicatorOn()
    {
        _dialogIndicator.SetActive(true);
    }

    public void DialogIndicatorOff()
    {
        _dialogIndicator.SetActive(false);
    }

    public string GetSoulFact(int index)
    {
        return _soulFacts.GetFact(index);
    }

    public Deed GetSoulSin(int index)
    {
        return _soulFacts.GetSin(index);
    }

    public Deed GetSoulVirtue(int index)
    {
        return _soulFacts.GetVirtue(index);
    }

    public void StopTalking()
    {
        _canTalk = false;
    }
}