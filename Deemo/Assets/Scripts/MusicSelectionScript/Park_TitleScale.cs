using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Park_TitleScale : MonoBehaviour
{
    private Vector2 originalScale;
    private Vector2 scaleUpScale;

    private float timeElapsed = 0.0f;

    private bool isCoroutine = false;
    private bool isCheck = false;

    void Start()
    {
        originalScale = GetComponent<RectTransform>().localScale;
        scaleUpScale = originalScale * 1.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoroutine == false)
        {
            if (isCheck == false)
            {
                StartCoroutine(ScaleUp());
            }
            else
            {
                StartCoroutine(ScaleDown());
            }
        }
    }

    private IEnumerator ScaleUp()
    {
        isCoroutine = true;
        isCheck = true;

        timeElapsed = 0.0f;

        while (timeElapsed < 1.0f)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / 1.0f);

            GetComponent<RectTransform>().localScale = Vector2.Lerp(originalScale, scaleUpScale, time);

            yield return null;
        }

        isCoroutine = false;
    }

    private IEnumerator ScaleDown()
    {
        isCoroutine = true;
        isCheck = false;

        timeElapsed = 0.0f;

        while (timeElapsed < 1.0f)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / 1.0f);

            GetComponent<RectTransform>().localScale = Vector2.Lerp(scaleUpScale, originalScale, time);

            yield return null;
        }

        isCoroutine = false;
    }
}
