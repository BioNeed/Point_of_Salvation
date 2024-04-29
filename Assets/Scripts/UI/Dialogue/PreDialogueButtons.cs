using UnityEngine;
using UnityEngine.UI;

public class PreDialogueButtons : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private PlayerStateMutable _playerState;
    [SerializeField] private PreDialogue _preDialogue;
    [SerializeField] private Button _startDialogueButton;
    [SerializeField] private Button _exitPreDialogueButton;

    private Soul _soul;

    public bool AreButtonsActive => _startDialogueButton.isActiveAndEnabled 
        && _exitPreDialogueButton.isActiveAndEnabled;

    private void Start()
    {
        ToggleButtons(false);
    }

    public void InitializePreDialogueButtons(Soul soul)
    {
        _soul = soul;
        ToggleButtons(true);
    }

    private void OnEnable()
    {
        _startDialogueButton.onClick.AddListener(OnStartDialogueButtonClicked);
        _exitPreDialogueButton.onClick.AddListener(OnExitPreDialogueButtonClicked);
    }

    private void OnDisable()
    {
        _startDialogueButton.onClick.RemoveListener(OnStartDialogueButtonClicked);
        _exitPreDialogueButton.onClick.RemoveListener(OnExitPreDialogueButtonClicked);
    }

    private void OnStartDialogueButtonClicked()
    {
        _dialogue.TryEnterDialogue(_soul);
        _preDialogue.TogglePreDialogue(false);
        ToggleButtons(false);
    }

    private void OnExitPreDialogueButtonClicked()
    {
        _preDialogue.TogglePreDialogue(false);
        ToggleButtons(false);
        _playerState.FreePlayer();
    }

    private void ToggleButtons(bool turnOn)
    {
        _startDialogueButton.gameObject.SetActive(turnOn);
        _exitPreDialogueButton.gameObject.SetActive(turnOn);
    }
}
