using TMPro;
using UnityEngine;

public class ClickableWordsCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clickableWordsCounter;
    [SerializeField] private Color _standardColor;
    [SerializeField] private Color _colorOnFoundAllWords;

    public void DisplayWordsLeft(int wordsCount, int wordsFound = 0)
    {
        if (wordsCount == 0)
        {
            DisplayMessageToContinue();
            return;
        }

        if (wordsFound < wordsCount)
        {
            _clickableWordsCounter.color = _standardColor;
        }
        else if (wordsFound == wordsCount)
        {
            _clickableWordsCounter.color = _colorOnFoundAllWords;
        }

        _clickableWordsCounter.text = $"Ключевые слова {wordsFound}/{wordsCount}";
    }

    private void DisplayMessageToContinue()
    {
        _clickableWordsCounter.color = _standardColor;
        _clickableWordsCounter.text = "Нажмите Space, чтобы продолжить...";
    }
}
