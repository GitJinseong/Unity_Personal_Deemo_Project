using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour
{
    public static TimeTracker instance;
    public TMP_Text timeText;
    public float startTime;

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        // ���� ���� �ð��� ����
        startTime = Time.time;
    }

    public void ResetTime()
    {
        // ��ư�� ���� ������ ���� �ð��� �����Ͽ� �ʱ�ȭ
        startTime = Time.time;
    }

    private void Update()
    {
        // ��� �ð� ���
        float elapsedTime = Time.time - startTime;

        // ��� �ð��� �ؽ�Ʈ�� ǥ��
        timeText.text = "Elapsed Time: " + elapsedTime.ToString("F2");
    }
}