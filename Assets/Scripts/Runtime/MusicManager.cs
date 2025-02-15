using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private AudioSource _mainMusic;
    [SerializeField]
    private AudioSource _inGameMusic;

    public void StartMainMusic()
    {
        if (_inGameMusic.isPlaying)
        {
            _inGameMusic.Stop();
        }

        _mainMusic.Play();
    }

    public void StartInGameMusic()
    {
        if (_mainMusic.isPlaying)
        {
            _mainMusic.Stop();
        }

        _inGameMusic.Play();
    }

    public void StopMusic()
    {
        if (_inGameMusic.isPlaying)
        {
            _inGameMusic.Stop();
        }

        if (_mainMusic.isPlaying)
        {
            _mainMusic.Stop();
        }
    }
}
