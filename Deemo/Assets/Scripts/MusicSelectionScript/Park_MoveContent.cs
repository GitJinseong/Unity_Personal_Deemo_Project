using System;
using System.Collections;
using System.Reflection;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Park_MoveContent : MonoBehaviour
{
    public ScrollRect scrollRect;
    private RectTransform rectTransform;
    private Coroutine checkCoroutine;

    private int titleCount;

    private float pastPosition;
    private float nowPosition;

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

        pastPosition = (rectTransform.anchoredPosition.y + titleCount * 360.0f) - 720.0f;

        yield return new WaitForSeconds(0.1f);

        nowPosition = (rectTransform.anchoredPosition.y + titleCount * 360.0f) - 720.0f;

        Debug.Log(Mathf.Abs(pastPosition - nowPosition));

        if (Mathf.Abs(pastPosition - nowPosition) <= 10.0f)
        {
            isScrolling = false;

            //scrollRect.inertia = false;
            for (int i = 0; i < titleCount; i++)
            {
                if (i * 720.0f - 360.0f < nowPosition && nowPosition < i * 720.0f + 360.0f)
                {
                    float timeElapsed = 0.0f;

                    while (timeElapsed < 5.0f)
                    {
                        timeElapsed += Time.deltaTime;

                        float time = Mathf.Clamp01(timeElapsed / 5.0f);

                        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x,
                Mathf.Lerp(rectTransform.anchoredPosition.y, (i * 720.0f) - (titleCount * 360.0f - 720.0f), time));

                        yield return null;
                    }

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