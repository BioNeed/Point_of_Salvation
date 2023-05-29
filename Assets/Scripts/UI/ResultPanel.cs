using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private PlayerController _playerController;
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
            _playerController.Invoke(nameof(_playerController.StopDialogue), 0.2f);
            _sceneController.NextSoul();
        }
    }

    public void OpenResultPanel(Fate rightFate, bool rightResult)
    {
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

        string rightDesicion = ConvertFateToString(rightFate);
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
