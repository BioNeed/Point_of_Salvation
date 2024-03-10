using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private PlayerState _playerState;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI _resultText;

    private bool _isPanelActive = false;

    private void Start()
    {
        _resultPanel.SetActive(false);
    }

    private void Update()
    {
        if (_isPanelActive == true && Input.GetButtonDown("Jump"))
        {
            _resultPanel.SetActive(false);
            _isPanelActive = false;
            _playerState.FreePlayer();
            _sceneController.NextSoul();
        }
    }

    public void OpenResultPanel(Fate rightFate, bool rightResult)
    {
        _playerState.EnterJournalResultPanel();
        _isPanelActive = true;
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

        var rightDesicion = ConvertFateToString(rightFate);
        resulText += "Правильным решением было " + rightDesicion;

        _resultText.text = resulText;
    }

    private string ConvertFateToString(Fate fate)
    {
        return fate switch
        {
            Fate.BurnInHell => "Гореть в аду",
            Fate.NoPurification => "Не заслужил очищение",
            Fate.SlightSinner => "Легкий грешник",
            Fate.DeservePurification => "Заслужил очищение",
            Fate.GoodFellow => "Славный малый",
            Fate.Righteous => "Праведник",
            _ => null
        };
    }
}
