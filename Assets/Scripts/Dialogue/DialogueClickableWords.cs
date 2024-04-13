﻿using System.Collections.Generic;
using UnityEngine;

public class DialogueClickableWords : MonoBehaviour
{
    [SerializeField] private DialogueChoices _dialogueChoices;
    [SerializeField] private PlayerState _playerState;
    [SerializeField] private DialogueText _dialogueText;    
    [SerializeField] private List<string> _clickableWords = new List<string>();

    public void ClearWords()
    {
        _clickableWords.Clear();
    }

    public void AddWord(string newWord)
    {
        _clickableWords.Add(newWord);
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown 
                && _clickableWords.Count != 0
                && _playerState.IsInDialogue)
        {
            var intersectedCharIndex = _dialogueText.GetIntersectingCharIndex(Input.mousePosition);
            var dialogueText = _dialogueText.Text.ToUpper();
            for (int i = 0; i < _clickableWords.Count; i++)
            {
                var clickableWord = _clickableWords[i].ToUpper();
                if (string.IsNullOrEmpty(clickableWord))
                {
                    continue;
                }

                var clickableWordStartIndexes = GetWordStartIndexes(dialogueText, clickableWord);
                foreach (var clickableWordStartIndex in clickableWordStartIndexes)
                {
                    var clickableWordEndIndex = clickableWordStartIndex + clickableWord.Length;
                    if (intersectedCharIndex > clickableWordStartIndex
                            && intersectedCharIndex < clickableWordEndIndex)
                    {
                        _dialogueChoices.DisplayChoice(i);
                        //string temp = _dialogueTextText.text;
                        //_dialogueTextText.text = "";
                        //int keyWordStart = temp.IndexOf(clickableWord);
                        //int keyWordEnd = temp.IndexOf(clickableWord) + clickableWord.Length;

                        //_dialogueTextText.text += temp.Substring(0, keyWordStart);
                        ////_dialogueText.text += "<color=#FFFF00>" + _clickableWords[i] + "</color>";
                        //_dialogueTextText.text += clickableWord;
                        //_dialogueTextText.text += temp.Substring(keyWordEnd, temp.Length - keyWordEnd);
                        break;
                    }
                }
            }
        }
    }

    private IEnumerable<int> GetWordStartIndexes(string text, string word)
    {
        var startIndexes = new List<int>();
        var searchFromIndex = 0;
        while(true)
        {
            var startIndex = text.IndexOf(word, searchFromIndex);
            if (startIndex == -1)
            {
                break;
            }

            startIndexes.Add(startIndex);
            searchFromIndex = startIndex + word.Length;
        }

        return startIndexes;
    }
}
