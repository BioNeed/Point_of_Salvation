using Assets.Scripts.UI.Dialogue;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreDialogue : MonoBehaviour
{
    [SerializeField] private PreDialogueButtons _preDialogueButtons;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject _preDialoguePanel;
    [SerializeField] private TextMeshProUGUI _preDialogueText;

    private static PreDialogue _instance;

    private Soul _soul;
    private bool _isActive = false;
    private Story _preDialogue;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Found more than one PreDialogue in the scene");
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
        TogglePreDialogue(true);
        _preDialogue = new Story(soul.GetPreDialogue().text);
        ContinuePreDialogue();
    }

    public void TogglePreDialogue(bool turnOn)
    {
        _isActive = turnOn;
        _preDialoguePanel.SetActive(turnOn);
    }

    private void ContinuePreDialogue()
    {
        if (_preDialogue.canContinue)
        {
            _preDialogueText.text = _preDialogue.Continue();
        }
        else
        {
            _preDialogueButtons.InitializePreDialogueFinishingButtons(_soul);
        }
    }
}