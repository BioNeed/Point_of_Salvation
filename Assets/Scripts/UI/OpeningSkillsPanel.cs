using UnityEngine;

public class OpeningSkillsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _levelUpIconText;
    [SerializeField] private SkillsPanel _skillsPanel;

    private bool _levelUp;

    private void Start()
    {
        _levelUpIconText.SetActive(false);
    }

    private void Update()
    {
        if (_levelUp == true && Input.GetKeyDown(KeyCode.K))
        {
            _skillsPanel.OpenPanel();
            _levelUp = false;
            _levelUpIconText.SetActive(false);
        }
    }

    public void ActivateIcon()
    {
        _levelUpIconText.SetActive(true);
        _levelUp = true;
    }
}
