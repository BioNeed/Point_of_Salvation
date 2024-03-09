using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Dialogue
{
    public class PreDialogueFinishingButtons : MonoBehaviour
    {
        [SerializeField] private DialogueState _dialogueState;
        [SerializeField] private MessagePanel _messagePanel;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private PreDialogue _preDialogue;
        [SerializeField] private Button _startDialogueButton;
        [SerializeField] private Button _exitPreDialogueButton;

        private Soul _soul;

        private void Start()
        {
            _startDialogueButton.onClick.AddListener(OnStartDialogueButtonClicked);
            _exitPreDialogueButton.onClick.AddListener(OnExitPreDialogueButtonClicked);
            ToggleButtons(false);
        }

        public void InitializePreDialogueFinishingButtons(Soul soul)
        {
            _soul = soul;
            ToggleButtons(true);
        }

        private void OnStartDialogueButtonClicked()
        {
            if (_sceneController.GetCurrentSoul() == _soul)
            {
                StartCoroutine(StartDialogue());
            }
            else
            {
                _messagePanel.OpenMessagePanel("Неподходящая душа! Ищите душу по портрету слева вверху.");
                _preDialogue.TogglePreDialogue(false);
            }

            ToggleButtons(false);
        }

        private void OnExitPreDialogueButtonClicked()
        {
            _preDialogue.TogglePreDialogue(false);
            _dialogueState.Invoke(nameof(_dialogueState.FinishDialogue), 0.2f);
            ToggleButtons(false);
        }

        private IEnumerator StartDialogue()
        {
            yield return new WaitForSeconds(0.2f);

            _preDialogue.TogglePreDialogue(false);
            var inkJSON = _soul.GetDialogue();
            DialogueManager.GetInstance().EnterDialogueMode(_soul, inkJSON);
        }

        private void ToggleButtons(bool turnOn)
        {
            _startDialogueButton.gameObject.SetActive(turnOn);
            _exitPreDialogueButton.gameObject.SetActive(turnOn);
        }
    }
}
