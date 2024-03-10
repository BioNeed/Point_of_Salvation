using TMPro;
using UnityEngine;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] private PlayerState _playerState;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private TextMeshProUGUI _messageText;

    private void Start()
    {
        _messagePanel.SetActive(false);
    }

    private void Update()
    {
        if (_playerState.IsInMessagePanel 
                && Input.GetKeyDown(KeyCode.Space))
        {
            _messagePanel.SetActive(false);
            _playerState.FreePlayer();
        }
    }

    public void OpenMessagePanel(string message)
    {
        _messagePanel.SetActive(true);
        _messageText.text = message;
        _playerState.EnterMessagePanel();
    }
}
