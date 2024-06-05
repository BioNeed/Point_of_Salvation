using UnityEngine;

public class GateHint : MonoBehaviour
{
    [SerializeField] private string _messageToFindKey;
    [SerializeField] private GameObject _gateHintIndicator;
    [SerializeField] private MessagePanel _messagePanel;
    [SerializeField] private GateOpening[] _gatesToOpen;

    private bool _mustBeDisplayed = true;
    private bool _isTurnedOn = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GateEntering gateEntering) == true)
        {
            if (gateEntering.HasKey)
            {
                foreach (var gateToOpen in _gatesToOpen)
                {
                    gateToOpen.Open();
                }

                _mustBeDisplayed = false;
                _isTurnedOn = false;
                _gateHintIndicator.SetActive(false);
                return;
            }
            else if (_mustBeDisplayed == true && _isTurnedOn == true)
            {
                _isTurnedOn = false;
                _gateHintIndicator.SetActive(false);
                _messagePanel.OpenMessagePanel(_messageToFindKey);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GateEntering _) == true
            && _mustBeDisplayed == true
            && _isTurnedOn == false)
        {
            _isTurnedOn = true;
            _gateHintIndicator.SetActive(true);
        }
    }
}
