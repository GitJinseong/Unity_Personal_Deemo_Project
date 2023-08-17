using System;
using System.Collections;
using UnityEngine;

public class Park_MoveContent : MonoBehaviour
{
    private Coroutine checkCoroutine;
    private Coroutine moveCoroutine;

    private RectTransform rectTransform;

    private float calculatePos;
    private float pastPos;
    private int titleCount;
    private float duration = 1.0f;
    private int currentIndex = -1;

    public bool isScroll;
    public bool isCoroutine = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        titleCount = Park_GameManager.instance.musicInformation["Title"].Count;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (checkCoroutine != null) { StopCoroutine(checkCoroutine); }
            if (moveCoroutine != null) { StopCoroutine(moveCoroutine); }

            isScroll = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isScroll = true;
        }

        if (isScroll == true)
        {
            checkCoroutine = StartCoroutine(Check());

            //isScroll = false;
        }

    }

    private IEnumerator Check()
    {
        pastPos = (rectTransform.anchoredPosition.y + titleCount * 360.0f) - 720.0f;

        yield return new WaitForSeconds(1.0f);

        calculatePos = (rectTransform.anchoredPosition.y + titleCount * 360.0f) - 720.0f;

        //Debug.LogFormat("{0},{1}", pastPos, calculatePos);
        Debug.Log(Mathf.Abs(pastPos - calculatePos));
        if (10.0f >= Mathf.Abs(pastPos - calculatePos))
        {
            for (int i = 0; i < titleCount; i++)
            {
                if (i * 720.0f - 360.0f < pastPos && pastPos < i * 720.0f + 360.0f)
                {
                    if (i != currentIndex)
                    {

                        moveCoroutine = StartCoroutine(Move(i));
                    }
                }
            }
        }
    }

    private IEnumerator Move(int i)
    {
        isCoroutine = true;

        currentIndex = i;

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x,
                Mathf.Lerp((i * 720.0f) - (titleCount * 360.0f - 720.0f), rectTransform.anchoredPosition.y, time));

            yield return null;
        }
        isCoroutine = false;
    }
}