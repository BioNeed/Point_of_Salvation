using UnityEngine;

public class GateKey : MonoBehaviour
{
    [SerializeField] private string _messageOnFoundKey;
    [SerializeField] private MessagePanel _messagePanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GateEntering gateEntering) == true)
        {
            _messagePanel.OpenMessagePanel(_messageOnFoundKey);
            gateEntering.FoundKey();
            Destroy(gameObject);
        }
    }
}
