using UnityEngine;

public class GateHint : MonoBehaviour
{
    [SerializeField] private string _messageToFindKey;
    [SerializeField] private GameObject _gateHintIndicator;
    [SerializeField] private MessagePanel _messagePanel;
    [SerializeField] private GateOpening[] _gatesToOpen;

    private bool _mustBeDisplayed = true;
    private bool _isTurnedOn = true;

    public void TryShowHint(bool hasKey)
    {
        if (hasKey)
        {
            foreach (var gateToOpen in _gatesToOpen)
            {
                gateToOpen.Open();
            }

            return;
        }

        if (_mustBeDisplayed == true && _isTurnedOn == true)
        {
            _isTurnedOn = false;
            _gateHintIndicator.SetActive(false);
            _messagePanel.OpenMessagePanel(_messageToFindKey);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _) == true
            && _mustBeDisplayed == true
            && _isTurnedOn == false)
        {
            _isTurnedOn = true;
            _gateHintIndicator.SetActive(true);
        }
    }
}
