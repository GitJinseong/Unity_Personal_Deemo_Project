using System;
using System.Collections;
using System.Reflection;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Park_MoveContent : MonoBehaviour
{
    public ScrollRect scrollRect;
    private RectTransform rectTransform;
    private Coroutine checkCoroutine;

    private int titleCount;

    private float pastPosition;
    private float nowPosition;
    private float targetY;

    public bool isStop = false;
    private bool isCoroutine = false;
    public bool isScrolling = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        titleCount = Park_GameManager.instance.musicInformation["Title"].Count;
    }

    void Update()
    {
        if (isStop == true)
        {
            if (checkCoroutine != null)
            {
                StopCoroutine(checkCoroutine);
                isCoroutine = false;
                isStop = false;
            }
        }

        if (isScrolling == true)
        {
            if (isCoroutine == false)
            {
                checkCoroutine = StartCoroutine(Check());
            }
        }
    }


    private IEnumerator Check()
    {
        isCoroutine = true;

        targetY = titleCount * 360.0f;

        pastPosition = (rectTransform.anchoredPosition.y + titleCount * 360.0f) - 720.0f;

        yield return new WaitForSeconds(0.1f);

        nowPosition = (rectTransform.anchoredPosition.y + titleCount * 360.0f) - 720.0f;

        Debug.Log(Mathf.Abs(pastPosition - nowPosition));

        if (Mathf.Abs(pastPosition - nowPosition) <= 100.0f)
        {
            isScrolling = false;

            for (int i = 0; i < titleCount; i++)
            {
                if (i * 720.0f - 360.0f < nowPosition && nowPosition < i * 720.0f + 360.0f)
                {
                    float timeElapsed = 0.0f;

                    while (timeElapsed < 5.0f)
                    {
                        scrollRect.inertia = false;

                        timeElapsed += Time.deltaTime;

                        float time = Mathf.Clamp01(timeElapsed / 5.0f);

                        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x,
                            Mathf.Lerp(rectTransform.anchoredPosition.y, (i * 720.0f) - (titleCount * 360.0f - 720.0f), time));


                        yield return null;
                    }

                    scrollRect.inertia = true;

                    break;

                }
            }
            //scrollRect.inertia = true;

            isCoroutine = false;
        }
        else
        {
            isCoroutine = false;
        }



        //// 이제 덜덜 떨리지 않고 완료되도록 보강
        //if (!isScrolling)
        //{
        //    Debug.Log("호출");

        //    float elapsedTime = 0.0f;

        //    while (elapsedTime < 5.0f)
        //    {
        //        elapsedTime += Time.deltaTime;

        //        float time = Mathf.Clamp01(elapsedTime / 5.0f);

        //        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x,
        //            Mathf.Lerp(rectTransform.anchoredPosition.y, targetY, time));

        //        yield return null;
        //    }

        //    // 위치 보정
        //    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetY);
        //}

    }

    //private void Update()
    //{
    //    if (isScrolling)
    //    {
    //        // 부드러운 이동 구현
    //        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, smoothSpeed * Time.deltaTime);

    //        // 이동 완료 조건 확인
    //        if (Vector2.Distance(content.anchoredPosition, targetPosition) < snapThreshold)
    //        {
    //            isScrolling = false;  // 스크롤 완료
    //        }
    //    }
    //}

    //// 특정 위치로 스크롤 이동
    //public void ScrollToPosition(float normalizedPosition)
    //{
    //    // 정규화된 위치에 따라 이동할 Y 좌표 계산
    //    float targetY = Mathf.Lerp(0, content.sizeDelta.y - scrollRect.viewport.rect.height, normalizedPosition);

    //    // 목표 위치 설정
    //    targetPosition = new Vector2(content.anchoredPosition.x, targetY);

    //    // 스크롤 중 상태로 변경
    //    isScrolling = true;
    //}
}