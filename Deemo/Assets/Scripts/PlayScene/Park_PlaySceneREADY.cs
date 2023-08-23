using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Park_PlaySceneREADY : MonoBehaviour
{
    private Vector2 startPos;

    public float endPos;
    public float effectPos;

    public float delay;
    public float wordDelay;

    public float durationOpacity;
    public float durationChange;

    private float timeElapsed = 0.0f;

    public bool isScale = false;

    private void Start()
    {
        startPos = GetComponent<RectTransform>().anchoredPosition;

        StartCoroutine(PostionChange());
    }

    private IEnumerator PostionChange()
    {
        yield return new WaitForSeconds(delay);

        timeElapsed = 0.0f;

        if (isScale == false)
        {
            while (timeElapsed < durationOpacity)
            {
                timeElapsed += Time.deltaTime;

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
                timeElapsed += Time.deltaTime;

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
        yield return new WaitForSeconds(wordDelay);

        timeElapsed = 0.0f;

        if (isScale == false)
        {
            while (timeElapsed < durationChange)
            {
                Debug.Log(durationChange);

                timeElapsed += Time.deltaTime;

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
                timeElapsed += Time.deltaTime;

                float time = Mathf.Clamp01(timeElapsed / durationChange);

                GetComponent<Image>().color = new Color(
                    GetComponent<Image>().color.r,
                    GetComponent<Image>().color.g,
                    GetComponent<Image>().color.b,
                    Mathf.Lerp(1.0f, 0.0f, time * time));

                yield return null;
            }
        }
    }
}
