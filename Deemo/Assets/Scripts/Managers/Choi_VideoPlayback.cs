using UnityEngine;
using UnityEngine.Video;

public class Choi_VideoPlayback : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoFilePath; // 비디오 파일의 경로

    private void Start()
    {
        // 비디오 파일 로드 및 할당
        videoPlayer.url = videoFilePath;

        // 비디오 재생 시작
        videoPlayer.Play();
    }
}
