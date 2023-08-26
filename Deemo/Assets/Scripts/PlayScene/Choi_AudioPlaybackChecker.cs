using UnityEngine;
using System.Collections;

public class Choi_AudioPlaybackChecker : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스 컴포넌트
    public Choi_LoadScene loadScene;
    private string sceneName = "ResultScene";
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
            isWait = true;
            Debug.Log("Audio playback has ended.");
            loadScene.Run(0f, sceneName);
        }
    }

    private IEnumerator WaitForStartMusic()
    {
        yield return new WaitForSeconds(musicDelay);
        isWait = false;
    }
}