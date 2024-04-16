using System.Collections;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueFactView _dialogueFactView;
    [SerializeField] private CurrentSoulContext _currentSoulContext;
    [SerializeField] private DialogueText _dialogueText;
    [SerializeField] private PlayerStateMutable _playerState;
    [SerializeField] private MessagePanel _messagePanel;
    [SerializeField] private FateJournal _fateJournal;
    [SerializeField] private GameObject _dialoguePanel;

    private void Start()
    {
        _dialoguePanel.SetActive(false);
    }

    public void TryEnterDialogue(Soul soul)
    {
        if (_currentSoulContext.CurrentSoul == soul)
        {
            StartCoroutine(EnterDialogue());
        }
        else
        {
            _messagePanel.OpenMessagePanel("Неподходящая душа! Ищите душу по портрету слева вверху.");
        }
    }

    public void ExitDialogue()
    {
        _dialogueFactView.HideFactPanel();
        _dialoguePanel.SetActive(false);
        var currentSoul = _currentSoulContext.CurrentSoul;
        currentSoul.DisableTalking();
        _fateJournal.OpenJournal();
    }

    private IEnumerator EnterDialogue()
    {
        yield return new WaitForEndOfFrame();

        _playerState.EnterDialogue();
        _dialoguePanel.SetActive(true);
        _dialogueText.StartDialogue();
    }
}