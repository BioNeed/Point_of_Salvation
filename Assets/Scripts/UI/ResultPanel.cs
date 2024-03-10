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
            resulText = "�����! ";
        }
        else
        {
            resulText = "�������. ";
        }

        var rightDesicion = ConvertFateToString(rightFate);
        resulText += "���������� �������� ���� " + rightDesicion;

        _resultText.text = resulText;
    }

    private string ConvertFateToString(Fate fate)
    {
        return fate switch
        {
            Fate.BurnInHell => "������ � ���",
            Fate.NoPurification => "�� �������� ��������",
            Fate.SlightSinner => "������ �������",
            Fate.DeservePurification => "�������� ��������",
            Fate.GoodFellow => "������� �����",
            Fate.Righteous => "���������",
            _ => null
        };
    }
}
