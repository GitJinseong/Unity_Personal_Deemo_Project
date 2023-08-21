using UnityEngine;
using System.Collections;

public class Choi_AudioPlaybackChecker : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스 컴포넌트
    private float musicDelay = 30.0f;

    private bool isWait = true;

    private void Start()
    {
        StartCoroutine(WaitForStartMusic());
    }

    private void Update()
    {
        if (!audioSource.isPlaying && isWait == false)
        {
            Debug.Log("Audio playback has ended.");
            // 여기에 재생 종료 후 실행할 로직 추가 가능
        }
    }

    private IEnumerator WaitForStartMusic()
    {
        yield return new WaitForSeconds(musicDelay);
        isWait = false;
    }
}