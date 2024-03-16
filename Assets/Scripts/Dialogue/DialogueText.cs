using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dialogue
{
    public class DialogueText : MonoBehaviour
    {
        private Story _currentStory;

        public List<Choice> CurrentChoices => _currentStory.currentChoices;

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
                        //TODO всплывающее сообщение "Найден грех"
                    }
                    else if (tag.StartsWith("Virtue."))
                    {
                        int virtueNum = int.Parse(tag.Substring("Virtue.".Length, tag.Length - "Virtue.".Length));
                        _virtuesFound.Add(_dialogueSoul.GetSoulVirtue(virtueNum));
                        //TODO всплывающее сообщение "Найдена добродетель"
                    }
                }
            }
            else
            {
                StartCoroutine(ExitDialogueMode());
            }
        }
    }
}
