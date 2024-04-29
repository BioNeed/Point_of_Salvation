using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueClickableWords : MonoBehaviour
{
    [SerializeField] private DialogueChoices _dialogueChoices;
    [SerializeField] private PlayerState _playerState;
    [SerializeField] private DialogueText _dialogueText;
    [SerializeField] private ClickableWordsCounterView _clickableWordsCounterView;

    private List<ClickableWord> _clickableWords = new ();

    public void ClearWords()
    {
        _clickableWords.Clear();
    }

    public void AddWord(string newWord)
    {
        _clickableWords.Add(new ClickableWord(newWord));
    }

    public void DisplayClickableWordsCount()
    {
        _clickableWordsCounterView.DisplayWordsLeft(_clickableWords.Count);
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
                if (_clickableWords[i].IsFound)
                {
                    continue;
                }

                var clickableWord = _clickableWords[i].Word.ToUpper();
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
                        _clickableWords[i].MarkAsFound();
                        _dialogueChoices.DisplayChoice(i);

                        var wordsFoundCount = _clickableWords.Where(cw => cw.IsFound).Count();
                        _clickableWordsCounterView.DisplayWordsLeft(_clickableWords.Count, wordsFoundCount);
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
