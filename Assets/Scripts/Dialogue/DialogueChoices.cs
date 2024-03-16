using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Dialogue
{
    public class DialogueChoices : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private DialogueText _dialogueText;
        [SerializeField] private Button[] _choiceButtons;

        private TextMeshProUGUI[] _choicesText;
        
        public void OnMakeChoice(int choiceIndex)
        {
            _currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
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

        private IEnumerator SelectChoice(int index)
        {
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(_choiceButtons[index].gameObject);
        }
    }
}
