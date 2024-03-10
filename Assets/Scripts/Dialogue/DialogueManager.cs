using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private PlayerState _playerState;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _factPanel;
    [SerializeField] private TextMeshProUGUI _factText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] _choices;

    [Header("Clickable words")]
    [SerializeField] private List<string> _clickableWords = new List<string>();

    [Header("Journal")]
    [SerializeField] private FateJournal _fateJournal;

    private Soul _dialogueSoul;
    private TextMeshProUGUI[] _choicesText;
    private Story _currentStory;
    private static DialogueManager _instance;
    private readonly List<Deed> _sinsFound = new List<Deed>();
    private readonly  List<Deed> _virtuesFound = new List<Deed>();

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }

        _instance = this;
    }

    private void Start()
    {
        _dialoguePanel.SetActive(false);
        _factPanel.SetActive(false);
        _choicesText = new TextMeshProUGUI[_choices.Length];
        int index = 0;
        foreach (GameObject choice in _choices)
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choice.SetActive(false);
            index++;
        }
    }

    private void Update()
    {
        if (!_playerState.IsInDialogue 
                || _currentStory?.currentChoices.Count != 0)
        {
            return;
        }

        if (Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    public void EnterDialogueMode(Soul soul, TextAsset inkJSON)
    {
        _playerState.EnterDialogue();
        _dialogueSoul = soul;
        _currentStory = new Story(inkJSON.text);
        _dialoguePanel.SetActive(true);
        _factPanel.SetActive(true);
        _factText.text = "";
        _sinsFound.Clear();
        _virtuesFound.Clear();
        ContinueStory();
    }

    public void MakeChoice(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        _dialoguePanel.SetActive(false);
        _dialogueText.text = "";
        _factPanel.SetActive(false);
        
        _dialogueSoul.DisableTalking();

        _fateJournal.OpenJournal(_dialogueSoul.SoulFacts, _sinsFound, _virtuesFound);
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            _clickableWords.Clear();
            foreach (GameObject choice in _choices)
            {
                choice.SetActive(false);
            }

            _dialogueText.text = _currentStory.Continue();
            foreach (var tag in _currentStory.currentTags)
            {
                if (tag.StartsWith("KeyWord."))
                {
                    string keyWord = tag.Substring("KeyWord.".Length, tag.Length - "KeyWord.".Length);
                    _clickableWords.Add(keyWord);
                }
                else if (tag.StartsWith("Fact."))
                {
                    int factNum = int.Parse(tag.Substring("Fact.".Length, tag.Length - "Fact.".Length));
                    _factText.text = _dialogueSoul.GetSoulFact(factNum);
                }
                else if (tag.StartsWith("Sin."))
                {
                    int sinNum = int.Parse(tag.Substring("Sin.".Length, tag.Length - "Sin.".Length));
                    _sinsFound.Add(_dialogueSoul.GetSoulSin(sinNum));
                    //TODO всплывающее сообщение "Ќайден грех"
                }
                else if (tag.StartsWith("Virtue."))
                {
                    int virtueNum = int.Parse(tag.Substring("Virtue.".Length, tag.Length - "Virtue.".Length));
                    _virtuesFound.Add(_dialogueSoul.GetSoulVirtue(virtueNum));
                    //TODO всплывающее сообщение "Ќайдена добродетель"
                }
            }
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoice(int index)
    {
        List<Choice> currentChoices = _currentStory.currentChoices;

        if (currentChoices.Count > _choices.Length)
        {
            Debug.LogError("More choices were than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        _choices[index].SetActive(true);
        _choicesText[index].text = currentChoices[index].text;

        StartCoroutine(SelectNewChoice(index));
    }

    private void DisplayAllChoices()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;

        if (currentChoices.Count > _choices.Length)
        {
            Debug.LogError("More choices were than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            _choices[index].SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < _choices.Length; i++)
        {
            _choices[i].SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectNewChoice(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(_choices[index]);
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(_choices[0]);
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown && _clickableWords.Count != 0)
        {
            int charIndex = TMP_TextUtilities.FindIntersectingCharacter(_dialogueText, Input.mousePosition, null, true);
            for (int i = 0; i < _clickableWords.Count; i++)
            {
                if (string.IsNullOrEmpty(_clickableWords[i])) continue;
                if (!_dialogueText.text.Contains(_clickableWords[i])) continue;

                for (int pos = _dialogueText.text.IndexOf(_clickableWords[i]);
                    pos <= _dialogueText.text.IndexOf(_clickableWords[i]) + _clickableWords[i].Length;
                    ++pos)
                {
                    if (charIndex == pos)
                    {
                        DisplayChoice(i);
                        string temp = _dialogueText.text;
                        _dialogueText.text = "";
                        int keyWordStart = temp.IndexOf(_clickableWords[i]),
                            keyWordEnd = temp.IndexOf(_clickableWords[i]) + _clickableWords[i].Length;

                        _dialogueText.text += temp.Substring(0, keyWordStart);
                        //_dialogueText.text += "<color=#FFFF00>" + _clickableWords[i] + "</color>";
                        _dialogueText.text += _clickableWords[i];
                        _dialogueText.text += temp.Substring(keyWordEnd, temp.Length - keyWordEnd);
                        break;
                    }
                }
            }
        }
    }
}