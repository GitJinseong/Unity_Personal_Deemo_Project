using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_HomeScreenDetection : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject settingUI;

    // Ȩ ȭ�� �̵� ����
    private void OnApplicationPause(bool pauseStatus)
    {
        // �̵����� ���
        if (pauseStatus == true && pauseButton.activeSelf == true)
        {
            pauseButton.SetActive(false);
            settingUI.SetActive(true);
            Debug.Log("App went to background (home screen)");
        }
    }
}
