using Ink.Runtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreDialogue : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private MessagePanel _messagePanel;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject _preDialoguePanel;
    [SerializeField] private TextMeshProUGUI _preDialogueText;
    [SerializeField] private Button _startDialogueButton;
    [SerializeField] private Button _exitButton;

    private static PreDialogue _instance;

    private Soul _soul;
    private bool _isActive;
    private Story _preDialogue;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }

        _instance = this;
        _preDialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (_isActive == true && Input.GetButtonDown("Submit"))
        {
            ContinuePreDialogue();
        }
    }

    public void StartPreDialogue(Soul soul)
    {
        _soul = soul;
        _preDialoguePanel.SetActive(true);
        _startDialogueButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);
        _isActive = true;
        _preDialogue = new Story(soul.GetPreDialogue().text);
        ContinuePreDialogue();
    }

    public void TryStartDialogue()
    {
        if (_sceneController.GetCurrentSoul() == _soul)
        {
            StartCoroutine(StartDialogue());
        }
        else
        {
            _messagePanel.OpenMessagePanel("Неподходящая душа! Ищите душу по портрету слева вверху.");
            _isActive = false;
            _preDialoguePanel.SetActive(false);
        }
    }

    public void ExitPreDialogue()
    {
        _isActive = false;
        _preDialoguePanel.SetActive(false);
        _playerController.Invoke(nameof(_playerController.StopDialogue), 0.2f);
    }

    private void ContinuePreDialogue()
    {
        if (_preDialogue.canContinue)
        {
            _preDialogueText.text = _preDialogue.Continue();
        }
        else
        {
            _startDialogueButton.gameObject.SetActive(true);
            _exitButton.gameObject.SetActive(true);
        }
    }

    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(0.2f);

        _isActive = false;
        _preDialoguePanel.SetActive(false);

        TextAsset inkJSON = _soul.GetDialogue();
        DialogueManager.GetInstance().EnterDialogueMode(_soul, inkJSON);
    }
}