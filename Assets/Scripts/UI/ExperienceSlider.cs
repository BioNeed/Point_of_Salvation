using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceSlider : MonoBehaviour
{
    [SerializeField] private Image _experienceSlider;
    [SerializeField] private float _fillingSpeed;

    private void Start()
    {
        _experienceSlider.fillAmount = 0;
    }

    public void FillExperience(float value)
    {
        StartCoroutine(FillExperienceSlightly(value));
    }

    private IEnumerator FillExperienceSlightly(float value)
    {
        while (_experienceSlider.fillAmount < value)
        {
            _experienceSlider.fillAmount += _fillingSpeed * Time.deltaTime;
            yield return null;
        }

        if (_experienceSlider.fillAmount >= 1)
        {
            ResetExperienceSlider();
        }
    }

    private void ResetExperienceSlider()
    {
        _experienceSlider.fillAmount = 0;
    }
}
