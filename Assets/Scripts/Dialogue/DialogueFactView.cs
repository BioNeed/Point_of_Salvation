using TMPro;
using UnityEngine;

public class DialogueFactView : MonoBehaviour
{
    [SerializeField] private GameObject _factInDialoguePanel;
    [SerializeField] private TextMeshProUGUI _factInJournalTMPGUI;

    private TextMeshProUGUI _factInDialogueTMPGUI;
    private Fact _foundFact;

    private void Start()
    {
        _factInDialogueTMPGUI = _factInDialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        _factInDialoguePanel.SetActive(false);
        _factInJournalTMPGUI.text = string.Empty;
    }

    public void SetFoundFact(Fact foundFact)
    {
        _foundFact = foundFact;
        DisplayFactInDialogue(foundFact.Description);
    }

    public void TryDisplayJournalFact()
    {
        if (_foundFact is null)
        {
            return;
        }

        _factInJournalTMPGUI.text = _foundFact.Description;
    }

    public void HideFactPanel()
    {
        _factInDialoguePanel.SetActive(false);
    }

    public void HideJournalFactText()
    {
        _factInJournalTMPGUI.text = string.Empty;
        _foundFact = null;
    }

    private void DisplayFactInDialogue(string text)
    {
        if (!_factInDialoguePanel.activeSelf)
        {
            _factInDialoguePanel.SetActive(true);
        }

        _factInDialogueTMPGUI.text = text;
    }
}
