using UnityEngine;

public class ExperienceProgress : MonoBehaviour
{
    [SerializeField] private ExperienceSlider _experienceSlider;
    [SerializeField] private OpeningSkillsPanel _openingSkillsPanel;

    private int _experienceAmount = 0;
    private int _experienceToLevelUp = 100;

    public void GainExperienceForSoul()
    {
        _experienceAmount += 100;
        if (_experienceAmount >= _experienceToLevelUp)
        {
            _experienceSlider.FillExperience(_experienceAmount/_experienceToLevelUp);
            _openingSkillsPanel.ActivateIcon();
        }
    }
}
