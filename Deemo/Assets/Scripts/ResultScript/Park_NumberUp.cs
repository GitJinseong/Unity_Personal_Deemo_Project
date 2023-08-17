using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Park_NumberUp : MonoBehaviour
{
    private TMP_Text numText;

    public float duration;
    private float zeorScore = 0.0f;


    public int score;

    public bool isFloat;

    private void Awake()
    {
        numText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        if (isFloat == false)
        {
            StartCoroutine(IntUp(score, zeorScore));
        }
        else
        {
            StartCoroutine(FloatUp(score, zeorScore));
        }
    }

    private IEnumerator IntUp(float maxNum, float current)
    {
        float time = maxNum / duration;

        while (current < maxNum)
        {
            current += time * Time.deltaTime;

            numText.text = string.Format("{0}", (int)current);

            yield return null;
        }

        current = maxNum;

        numText.text = string.Format("{0}", (int)current);
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

        numText.text = string.Format("{0:F2} %", current);
    }
}
