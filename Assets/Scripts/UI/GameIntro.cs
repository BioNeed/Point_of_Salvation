using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class GameIntro : MonoBehaviour
{
    [SerializeField] private int _sceneNumberToLoadAfterIntro;

    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += SkipIntro;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SkipIntro(_videoPlayer);
        }
    }

    private void SkipIntro(VideoPlayer player)
    {
        player.Stop();
        SceneManager.LoadScene(_sceneNumberToLoadAfterIntro);
        _videoPlayer.loopPointReached -= SkipIntro;
    }
}
