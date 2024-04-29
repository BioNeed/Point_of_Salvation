using Ink.Runtime;
using TMPro;
using UnityEngine;

public class PreDialogue : MonoBehaviour
{
    [SerializeField] private PlayerStateMutable _playerState;
    [SerializeField] private PreDialogueButtons _preDialogueButtons;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject _preDialoguePanel;
    [SerializeField] private TextMeshProUGUI _preDialogueText;

    private Soul _soul;
    private Story _preDialogueStory;

    private void Update()
    {
        if (_playerState.IsInPreDialogue == true 
                && !_preDialogueButtons.AreButtonsActive
                && Input.GetButtonDown("Submit"))
        {
            ContinuePreDialogue();
        }
    }

    public void StartPreDialogue(Soul soul)
    {
        _soul = soul;
        _playerState.EnterPreDialogue();
        _preDialogueStory = new Story(soul.Dialogues.PreDialogueJsonText);
        TogglePreDialogue(true);
        ContinuePreDialogue();
    }

    public void TogglePreDialogue(bool turnOn)
    {
        _preDialoguePanel.SetActive(turnOn);
    }

    private void ContinuePreDialogue()
    {
        if (_preDialogueStory.canContinue)
        {
            _preDialogueText.text = _preDialogueStory.Continue();
        }
        else
        {
            _preDialogueButtons.InitializePreDialogueButtons(_soul);
        }
    }
}
