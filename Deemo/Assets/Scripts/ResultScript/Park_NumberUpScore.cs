using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Park_NumberUpScore : MonoBehaviour
{
    private TMP_Text numText;

    public float duration;
    private float zeorScore = 0.0f;


    private float score = float.Parse(Choi_GameManager.instance.formattedAccuracy);

    private void Awake()
    {
        numText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        StartCoroutine(FloatUp(score, zeorScore));
    }

    private IEnumerator FloatUp(float maxNum, float current)
    {
        float time = maxNum / duration;

        while (current < maxNum)
        {
            current += time * Time.deltaTime;

            numText.text = string.Format("{0:F2} %", current);

            yield return null;
        }

        current = maxNum;

        if (score != 0f)
        {
            numText.text = string.Format("{0:F2} %", current);
        }

        for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
        {
            if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
            {
                Park_GameManager.instance.musicInformation[Park_GameManager.instance.difficulty + "BestScore"][i] = "25";
            }
        }
    }
}
