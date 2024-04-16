using UnityEngine;

public class SoulDialogueIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _dialogIndicator;

    private void Start()
    {
        _dialogIndicator.SetActive(false);
    }

    public void TurnOn()
    {
        _dialogIndicator.SetActive(true);
    }

    public void TurnOff()
    {
        _dialogIndicator.SetActive(false);
    }
}
