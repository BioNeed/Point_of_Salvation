using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private ExperienceProgress _experienceProgress;
    [SerializeField] private SoulOrdering _soulOrdering;
    [SerializeField] private PlayerStateMutable _playerState;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI _resultText;

    private void Start()
    {
        _resultPanel.SetActive(false);
    }

    private void Update()
    {
        if (_playerState.IsInJournalResultPanel 
                && Input.GetButtonDown("Jump"))
        {
            _resultPanel.SetActive(false);
            _playerState.FreePlayer();
            _soulOrdering.NextSoul();
            _experienceProgress.GainExperienceForSoul();
        }
    }

    public void OpenResultPanel(
        Fate rightFate, 
        bool rightResult)
    {
        _playerState.EnterJournalResultPanel();
        _resultPanel.SetActive(true);

        string resulText;
        if (rightResult == true)
        {
            resulText = "Верно! ";
        }
        else
        {
            resulText = "Неверно. ";
        }

        resulText += "Правильным решением было " + rightFate.ConvertToString();
        _resultText.text = resulText;
    }
}
