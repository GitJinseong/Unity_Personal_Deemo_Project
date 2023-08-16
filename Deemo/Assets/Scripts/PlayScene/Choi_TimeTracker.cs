using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Choi_TimeTracker : MonoBehaviour
{
    public static Choi_TimeTracker instance;
    public TMP_Text timeText;
    public float startTime;

    private void Awake()
    {
        // 게임 시작 시간을 저장
        startTime = Time.time;

        instance = this;
    }

    public void ResetTime()
    {
        // 버튼을 누를 때마다 현재 시간을 저장하여 초기화
        startTime = Time.time;
    }

    private void Update()
    {
        // 경과 시간 계산
        float elapsedTime = Time.time - startTime;

        // 경과 시간을 텍스트로 표시
        timeText.text = "Elapsed Time: " + elapsedTime.ToString("F2");
    }
}