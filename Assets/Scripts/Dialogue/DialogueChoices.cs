using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueChoices : MonoBehaviour
{
    [SerializeField] private DialogueText _dialogueText;
    [SerializeField] private Button[] _choiceButtons;

    private List<TextMeshProUGUI> _choicesText = new List<TextMeshProUGUI>();

    private void Start()
    {
        foreach (var choiceButton in _choiceButtons)
        {
            _choicesText.Add(choiceButton.GetComponentInChildren<TextMeshProUGUI>());
            choiceButton.gameObject.SetActive(false);
        }
    }

    public void OnChoiceButtonClicked(int choiceIndex)
    {
        StartCoroutine(_dialogueText.ChooseDialogueChoice(choiceIndex));
    }

    public void HideChoices()
    {
        foreach (var choice in _choiceButtons)
        {
            choice.gameObject.SetActive(false);
        }
    }

    public void DisplayChoice(int index)
    {
        var currentChoices = _dialogueText.CurrentChoices;

        if (currentChoices.Count > _choiceButtons.Length)
        {
            Debug.LogError("More choices were found than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        _choiceButtons[index].gameObject.SetActive(true);
        _choicesText[index].text = currentChoices[index].text;

        StartCoroutine(SelectChoice(index));
    }

    public void DisplayAllChoices()
    {
        List<Choice> currentChoices = _dialogueText.CurrentChoices;

        if (currentChoices.Count > _choiceButtons.Length)
        {
            Debug.LogError("More choices were found than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        var index = 0;
        foreach (Choice choice in currentChoices)
        {
            _choiceButtons[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < _choiceButtons.Length; i++)
        {
            _choiceButtons[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectChoice(0));
    }

    private IEnumerator SelectChoice(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(_choiceButtons[index].gameObject);
    }
}
