using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Park_PlaySceneREADY : MonoBehaviour
{
    public GameObject pause_Button;
    private Vector2 startPos;
    private Vector2 originalPos; // 원래 포지션 값을 저장할 변수

    private RectTransform rectTransform;
    public float endPos;
    public float effectPos;

    public float delay = 3.0f;
    public float wordDelay;

    public float durationOpacity;
    public float durationChange;
    public bool remotePauseButton = false;
    private float timeElapsed = 0.0f;

    public bool isScale = false;
    private int runTime = 0;

    private void Awake()
    {
        originalPos = GetComponent<RectTransform>().anchoredPosition; // 원래 포지션 값을 저장
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        delay = 1.0f;
        startPos = originalPos; // 원래 포지션 값으로 복원
        startPos.y = originalPos.y; // position.y도 원래 값으로 초기화
        rectTransform.anchoredPosition = new Vector2(originalPos.x, originalPos.y);
        Debug.Log(startPos.y);
        StartCoroutine(PostionChange());
        Debug.Log("OnEnable");
    }

    private IEnumerator WaitForSecondsRealtimeCustom(float time)
    {
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + time)
        {
            yield return null;
        }
    }

    private IEnumerator PostionChange()
    {
        yield return StartCoroutine(WaitForSecondsRealtimeCustom(delay));

        timeElapsed = 0.0f;

        if (isScale == false)
        {
            while (timeElapsed < durationOpacity)
            {
                timeElapsed += Time.unscaledDeltaTime; // unscaledDeltaTime 사용

                float timeOpacity = 1.0f - Mathf.Pow(1.0f - Mathf.Clamp01(timeElapsed / durationOpacity), 2);
                float timePosition = Mathf.Clamp01(timeElapsed / durationOpacity);

                GetComponent<Image>().color = new Color(
                    GetComponent<Image>().color.r,
                    GetComponent<Image>().color.g,
                    GetComponent<Image>().color.b,
                    Mathf.Lerp(0.0f, 1.0f, timeOpacity));

                GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(startPos.x, endPos, timePosition),
                    GetComponent<RectTransform>().anchoredPosition.y);

                yield return null;
            }
        }
        else
        {
            while (timeElapsed < durationOpacity)
            {
                timeElapsed += Time.unscaledDeltaTime; // unscaledDeltaTime 사용

                float timeOpacity = 1.0f - Mathf.Pow(1.0f - Mathf.Clamp01(timeElapsed / durationOpacity), 2);
                float timeScale = Mathf.Clamp01(timeElapsed / durationOpacity);

                GetComponent<Image>().color = new Color(
                    GetComponent<Image>().color.r,
                    GetComponent<Image>().color.g,
                    GetComponent<Image>().color.b,
                    Mathf.Lerp(0.0f, 1.0f, timeOpacity));

                GetComponent<RectTransform>().localScale = new Vector2(Mathf.Lerp(0.0f, 1.0f, timeScale),
                    GetComponent<RectTransform>().localScale.y);

                yield return null;
            }
        }

        StartCoroutine(wordEffect());
    }

    private IEnumerator wordEffect()
    {
        yield return StartCoroutine(WaitForSecondsRealtimeCustom(wordDelay));

        timeElapsed = 0.0f;

        if (isScale == false)
        {
            while (timeElapsed < durationChange)
            {
                timeElapsed += Time.unscaledDeltaTime; // unscaledDeltaTime 사용

                float time = Mathf.Clamp01(timeElapsed / durationChange);

                GetComponent<Image>().color = new Color(
                    GetComponent<Image>().color.r,
                    GetComponent<Image>().color.g,
                    GetComponent<Image>().color.b,
                    Mathf.Lerp(1.0f, 0.0f, time * time));

                GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x,
                    Mathf.Lerp(startPos.y, effectPos, time));

                yield return null;
            }
        }
        else
        {
            while (timeElapsed < durationChange)
            {
                timeElapsed += Time.unscaledDeltaTime; // unscaledDeltaTime 사용

                float time = Mathf.Clamp01(timeElapsed / durationChange);

                GetComponent<Image>().color = new Color(
                    GetComponent<Image>().color.r,
                    GetComponent<Image>().color.g,
                    GetComponent<Image>().color.b,
                    Mathf.Lerp(1.0f, 0.0f, time * time));

                yield return null;
            }
        }

        StartCoroutine(DeactivateParentObject());
    }

    private IEnumerator DeactivateParentObject()
    {
        yield return StartCoroutine(WaitForSecondsRealtimeCustom(durationChange));

        // 정지 버튼 활성화
        pause_Button.SetActive(true);
        // 부모 오브젝트를 비활성화
        transform.parent.gameObject.SetActive(false);
    }
}