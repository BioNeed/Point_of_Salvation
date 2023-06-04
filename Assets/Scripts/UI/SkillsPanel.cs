using UnityEngine;

public class SkillsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _skillsPanel;

    private void Start()
    {
        _skillsPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        _skillsPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        _skillsPanel.SetActive(false);
    }
}
