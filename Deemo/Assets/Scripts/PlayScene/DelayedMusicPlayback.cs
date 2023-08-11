using UnityEngine;

public class DelayedMusicPlayback : MonoBehaviour
{
    private AudioSource musicSource; // 음악을 재생할 AudioSource 컴포넌트
    public float delayInSeconds = 1.0f; // 지연 시간 (초) // 기본 1초

    private bool isMusicStarted = false;
    private float elapsedTime = 0.0f;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!isMusicStarted)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= delayInSeconds)
            {
                StartMusic();
            }
        }
    }

    private void StartMusic()
    {
        musicSource.Play();
        isMusicStarted = true;
    }
}
