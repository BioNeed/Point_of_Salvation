using TMPro;
using UnityEngine;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] private DialogueState _dialogueState;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private TextMeshProUGUI _messageText;

    private bool _opened = false;

    private void Start()
    {
        _messagePanel.SetActive(false);
    }

    private void Update()
    {
        if (_opened == true && Input.GetKeyDown(KeyCode.Space))
        {
            _opened = false;
            _messagePanel.SetActive(false);
            _dialogueState.Invoke(nameof(_dialogueState.FinishDialogue), 0.2f);
        }
    }

    public void OpenMessagePanel(string message)
    {
        _messagePanel.SetActive(true);
        _messageText.text = message;
        _opened = true;
    }
}
