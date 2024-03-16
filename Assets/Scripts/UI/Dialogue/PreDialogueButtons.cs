using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Dialogue
{
    public class PreDialogueButtons : MonoBehaviour
    {
        [SerializeField] private PlayerState _playerState;
        [SerializeField] private MessagePanel _messagePanel;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private PreDialogue _preDialogue;
        [SerializeField] private Button _startDialogueButton;
        [SerializeField] private Button _exitPreDialogueButton;

        private Soul _soul;

        public void InitializePreDialogueButtons(Soul soul)
        {
            _soul = soul;
            ToggleButtons(true);
        }

        private void OnEnable()
        {
            _startDialogueButton.onClick.AddListener(OnStartDialogueButtonClicked);
            _exitPreDialogueButton.onClick.AddListener(OnExitPreDialogueButtonClicked);
            ToggleButtons(false);
        }

        private void OnDisable()
        {
            _startDialogueButton.onClick.RemoveListener(OnStartDialogueButtonClicked);
            _exitPreDialogueButton.onClick.RemoveListener(OnExitPreDialogueButtonClicked);
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
            ToggleButtons(false);
            _playerState.FreePlayer();
        }

        private IEnumerator StartDialogue()
        {
            yield return new WaitForSeconds(0.2f);

            _preDialogue.TogglePreDialogue(false);
            global::Dialogue.GetInstance().EnterDialogueMode(_soul);
        }

        private void ToggleButtons(bool turnOn)
        {
            _startDialogueButton.gameObject.SetActive(turnOn);
            _exitPreDialogueButton.gameObject.SetActive(turnOn);
        }
    }
}
