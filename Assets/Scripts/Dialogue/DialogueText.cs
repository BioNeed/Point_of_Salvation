﻿using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    [SerializeField] private CurrentSoulContext _currentSoulContext;
    [SerializeField] private DialogueFactView _dialogueFactView;
    [SerializeField] private DeedsFinding _deedsFinding;
    [SerializeField] private DialogueChoices _dialogueChoices;
    [SerializeField] private DialogueClickableWords _dialogueClickableWords;
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private PlayerState _playerState;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    private Story _dialogueStory;
    private Soul _dialogueSoul;

    public List<Choice> CurrentChoices => _dialogueStory.currentChoices;

    public string Text => _dialogueText.text;

    private void Update()
    {
        if (!_playerState.IsInDialogue
                || _dialogueStory?.currentChoices.Count != 0)
        {
            return;
        }

        if (Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }
    }

    public void StartDialogue()
    {
        _dialogueSoul = _currentSoulContext.CurrentSoul;
        _dialogueStory = new Story(_dialogueSoul.Dialogues.DialogueJsonText);
        ContinueStory();
    }

    public IEnumerator ChooseDialogueChoice(int choiceIndex)
    {
        yield return new WaitForEndOfFrame();
        _dialogueStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public int GetIntersectingCharIndex(Vector3 position)
    {
        return TMP_TextUtilities.FindIntersectingCharacter(
            text: _dialogueText,
            position: position,
            camera: null,
            visibleOnly: true);
    }

    private void ContinueStory()
    {
        if (_dialogueStory.canContinue)
        {
            _dialogueClickableWords.ClearWords();
            _dialogueChoices.HideChoices();
            _dialogueText.text = _dialogueStory.Continue();
            foreach (var tag in _dialogueStory.currentTags)
            {
                if (tag.StartsWith("KeyWord."))
                {
                    var keyWord = tag.Substring("KeyWord.".Length, tag.Length - "KeyWord.".Length);
                    _dialogueClickableWords.AddWord(keyWord);
                }
                else if (tag.StartsWith("Fact."))
                {
                    var factNum = int.Parse(tag.Substring("Fact.".Length, tag.Length - "Fact.".Length));
                    _deedsFinding.FoundFact = _dialogueSoul.Facts.GetFact(factNum);
                    _dialogueFactView.DisplayFactInDialogue();
                }
                else if (tag.StartsWith("Sin."))
                {
                    var sinNum = int.Parse(tag.Substring("Sin.".Length, tag.Length - "Sin.".Length));
                    _deedsFinding.AddFoundSin(_dialogueSoul.Facts.GetSin(sinNum));
                }
                else if (tag.StartsWith("Virtue."))
                {
                    var virtueNum = int.Parse(tag.Substring("Virtue.".Length, tag.Length - "Virtue.".Length));
                    _deedsFinding.AddFoundVirtue(_dialogueSoul.Facts.GetVirtue(virtueNum));
                }
            }

            _dialogueClickableWords.DisplayClickableWordsCount();
        }
        else
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForEndOfFrame();

        _dialogue.ExitDialogue();
    }
}
