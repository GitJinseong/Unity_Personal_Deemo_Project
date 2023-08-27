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
        musicPlayer.Pause();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        musicPlayer.Play();
    }

    public void CoroutineDelayForResumeGame(float t)
    {
        StartCoroutine(DelayForResumeGame(t));
    }

    public IEnumerator DelayForPauseGame(float t)
    {
        yield return new WaitForSecondsRealtime(t);
        musicPlayer.Pause();
        Time.timeScale = 0f;
    }

    public IEnumerator DelayForResumeGame(float t)
    {
        yield return new WaitForSecondsRealtime(t);
        Time.timeScale = 1f;
        musicPlayer.Play();
    }
}
