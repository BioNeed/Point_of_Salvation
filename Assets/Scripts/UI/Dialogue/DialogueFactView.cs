using TMPro;
using UnityEngine;

public class DialogueFactView : MonoBehaviour
{
    [SerializeField] private DeedsFinding _deedsFinding;
    [SerializeField] private GameObject _factInDialoguePanel;

    private TextMeshProUGUI _factInDialogueTMPGUI;

    private void Start()
    {
        _factInDialogueTMPGUI = _factInDialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        _factInDialoguePanel.SetActive(false);
    }

    public void HideFactPanel()
    {
        _factInDialoguePanel.SetActive(false);
    }

    public void DisplayFactInDialogue()
    {
        if (!_factInDialoguePanel.activeSelf)
        {
            _factInDialoguePanel.SetActive(true);
        }

        var fact = _deedsFinding.FoundFact;
        _factInDialogueTMPGUI.text = fact.Description;
    }
}
