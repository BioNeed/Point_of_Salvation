using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioBackground : MonoBehaviour
{
    [SerializeField] private AudioClip[] _backgroundClips;

    private AudioSource _audioSource;
    private int _currentClipIndex;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentClipIndex = Random.Range(0, _backgroundClips.Length);
        SetNewClip(_currentClipIndex);
    }

    private void SetNewClip(int clipIndex)
    {
        _audioSource.clip = _backgroundClips[clipIndex];
        _audioSource.Play();
        StartCoroutine(SetNewClipAfterDelay(_backgroundClips[clipIndex].length));
    }

    private int GetRandomNumberExcludingValue(
        int excludingValue,
        int minInclusive,
        int maxInclusive)
    {
        int generatedValue;
        do
        {
            generatedValue = Random.Range(minInclusive, maxInclusive);
        } while (generatedValue == excludingValue);

        return generatedValue;
    }

    private IEnumerator SetNewClipAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        _currentClipIndex = GetRandomNumberExcludingValue(_currentClipIndex, 0, _backgroundClips.Length - 1);
        SetNewClip(_currentClipIndex);
    }
}
