using System.Collections;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueFactView _dialogueFactView;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private DialogueText _dialogueText;
    [SerializeField] private PlayerStateMutable _playerState;
    [SerializeField] private MessagePanel _messagePanel;
    [SerializeField] private FateJournal _fateJournal;
    [SerializeField] private GameObject _dialoguePanel;

    private Soul _dialogueSoul;

    private void Start()
    {
        _dialoguePanel.SetActive(false);
    }

    //private void Update()
    //{
    //    if (!_playerState.IsInDialogue 
    //            || _currentStory?.currentChoices.Count != 0)
    //    {
    //        return;
    //    }

    //    if (Input.GetButtonDown("Submit"))
    //    {
    //        ContinueStory();
    //    }
    //}

    public void TryEnterDialogue(Soul soul)
    {
        if (_sceneController.GetCurrentSoul() == soul)
        {
            StartCoroutine(EnterDialogue(soul));
        }
        else
        {
            _messagePanel.OpenMessagePanel("Неподходящая душа! Ищите душу по портрету слева вверху.");
        }
    }

    public void ExitDialogue()
    {
        _dialogueFactView.HideFactPanel();
        _dialogueSoul.DisableTalking();
        _dialoguePanel.SetActive(false);
        _fateJournal.OpenJournal(_dialogueSoul.SoulFacts);
    }

    private IEnumerator EnterDialogue(Soul soul)
    {
        yield return new WaitForEndOfFrame();

        _playerState.EnterDialogue();
        _dialogueSoul = soul;
        _dialoguePanel.SetActive(true);
        _dialogueText.StartDialogue(soul);
    }

    //public void MakeChoice(int choiceIndex)
    //{
    //    _currentStory.ChooseChoiceIndex(choiceIndex);
    //    ContinueStory();
    //}

    //private void ContinueStory()
    //{
    //    if (_currentStory.canContinue)
    //    {
    //        _clickableWords.Clear();
    //        foreach (GameObject choice in _choices)
    //        {
    //            choice.SetActive(false);
    //        }

    //        _dialogueTextText.text = _currentStory.Continue();
    //        foreach (var tag in _currentStory.currentTags)
    //        {
    //            if (tag.StartsWith("KeyWord."))
    //            {
    //                string keyWord = tag.Substring("KeyWord.".Length, tag.Length - "KeyWord.".Length);
    //                _clickableWords.Add(keyWord);
    //            }
    //            else if (tag.StartsWith("Fact."))
    //            {
    //                int factNum = int.Parse(tag.Substring("Fact.".Length, tag.Length - "Fact.".Length));
    //                _factText.text = _dialogueSoul.GetSoulFact(factNum);
    //            }
    //            else if (tag.StartsWith("Sin."))
    //            {
    //                int sinNum = int.Parse(tag.Substring("Sin.".Length, tag.Length - "Sin.".Length));
    //                _sinsFound.Add(_dialogueSoul.GetSoulSin(sinNum));
    //                //TODO всплывающее сообщение "Найден грех"
    //            }
    //            else if (tag.StartsWith("Virtue."))
    //            {
    //                int virtueNum = int.Parse(tag.Substring("Virtue.".Length, tag.Length - "Virtue.".Length));
    //                _virtuesFound.Add(_dialogueSoul.GetSoulVirtue(virtueNum));
    //                //TODO всплывающее сообщение "Найдена добродетель"
    //            }
    //        }
    //    }
    //    else
    //    {
    //        StartCoroutine(ExitDialogueMode());
    //    }
    //}

    //private void DisplayChoice(int index)
    //{
    //    var currentChoices = _currentStory.currentChoices;

    //    if (currentChoices.Count > _choices.Length)
    //    {
    //        Debug.LogError("More choices were found than the UI can support. Number of choices given: " + currentChoices.Count);
    //    }

    //    _choices[index].SetActive(true);
    //    _choicesText[index].text = currentChoices[index].text;

    //    StartCoroutine(SelectNewChoice(index));
    //}

    //private void DisplayAllChoices()
    //{
    //    List<Choice> currentChoices = _currentStory.currentChoices;

    //    if (currentChoices.Count > _choices.Length)
    //    {
    //        Debug.LogError("More choices were found than the UI can support. Number of choices given: " + currentChoices.Count);
    //    }

    //    int index = 0;
    //    foreach (Choice choice in currentChoices)
    //    {
    //        _choices[index].SetActive(true);
    //        _choicesText[index].text = choice.text;
    //        index++;
    //    }

    //    for (int i = index; i < _choices.Length; i++)
    //    {
    //        _choices[i].SetActive(false);
    //    }

    //    StartCoroutine(SelectFirstChoice());
    //}

    //private IEnumerator SelectNewChoice(int index)
    //{
    //    EventSystem.current.SetSelectedGameObject(null);
    //    yield return new WaitForEndOfFrame();
    //    EventSystem.current.SetSelectedGameObject(_choices[index]);
    //}

    //private IEnumerator SelectFirstChoice()
    //{
    //    EventSystem.current.SetSelectedGameObject(null);
    //    yield return new WaitForEndOfFrame();
    //    EventSystem.current.SetSelectedGameObject(_choices[0]);
    //}

    //private void OnGUI()
    //{
    //    if (Event.current.type == EventType.MouseDown && _clickableWords.Count != 0)
    //    {
    //        int charIndex = TMP_TextUtilities.FindIntersectingCharacter(_dialogueTextText, Input.mousePosition, null, true);
    //        for (int i = 0; i < _clickableWords.Count; i++)
    //        {
    //            if (string.IsNullOrEmpty(_clickableWords[i])) continue;
    //            if (!_dialogueTextText.text.Contains(_clickableWords[i])) continue;

    //            for (int pos = _dialogueTextText.text.IndexOf(_clickableWords[i]);
    //                pos <= _dialogueTextText.text.IndexOf(_clickableWords[i]) + _clickableWords[i].Length;
    //                ++pos)
    //            {
    //                if (charIndex == pos)
    //                {
    //                    DisplayChoice(i);
    //                    string temp = _dialogueTextText.text;
    //                    _dialogueTextText.text = "";
    //                    int keyWordStart = temp.IndexOf(_clickableWords[i]),
    //                        keyWordEnd = temp.IndexOf(_clickableWords[i]) + _clickableWords[i].Length;

    //                    _dialogueTextText.text += temp.Substring(0, keyWordStart);
    //                    //_dialogueText.text += "<color=#FFFF00>" + _clickableWords[i] + "</color>";
    //                    _dialogueTextText.text += _clickableWords[i];
    //                    _dialogueTextText.text += temp.Substring(keyWordEnd, temp.Length - keyWordEnd);
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}
}