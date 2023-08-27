using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_HomeScreenDetection : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject settingUI;

    // 홈 화면 이동 감지
    private void OnApplicationPause(bool pauseStatus)
    {
        // 홈 화면으로 이동했을 경우
        if (pauseStatus == true && pauseButton.activeSelf == true)
        {
            pauseButton.SetActive(false);
            settingUI.SetActive(true);
        }
    }
}
