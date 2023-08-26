using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_TimeManager : MonoBehaviour
{
    public AudioSource musicPlayer;
    public static Choi_TimeManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        MusicPlay();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        MusicPlay();
    }

    public void CoroutineDelayForResumeGame(float t)
    {
        StartCoroutine(DelayForResumeGame(t));
    }

    public IEnumerator DelayForPauseGame(float t)
    {
        yield return new WaitForSecondsRealtime(t);
        MusicPlay();
        Time.timeScale = 0f;
    }

    public IEnumerator DelayForResumeGame(float t)
    {
        yield return new WaitForSecondsRealtime(t);
        Time.timeScale = 1f;
        MusicPlay();
    }

    public void MusicPlay()
    {
        if (musicPlayer.isPlaying)
        {
            musicPlayer.Pause();
        }
        else
        {
            musicPlayer.Play();
        }
    }
}
