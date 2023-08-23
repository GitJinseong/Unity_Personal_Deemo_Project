using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Park_PlaySceneOpacity : MonoBehaviour
{
    public float duration;
    public float delay;
    public float endAlpha;

    private Color startColor;
    private Color endColor;

    public bool isText = false;
    public bool isSprite = false;

    private void Awake()
    {
        if (isText == true && isSprite == false)
        {
            startColor = GetComponent<TMP_Text>().color;

            GetComponent<TMP_Text>().color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

            endColor = new Color(startColor.r, startColor.g, startColor.b, endAlpha);

            StartCoroutine(StartOpacityText());
        }
        else if (isSprite == true && isText == false)
        {
            startColor = GetComponent<SpriteRenderer>().color;

            GetComponent<SpriteRenderer>().color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

            endColor = new Color(startColor.r, startColor.g, startColor.b, endAlpha);

            StartCoroutine(StartOpacitySpriteRenderer());
        }
        else
        {
            startColor = GetComponent<Image>().color;

            GetComponent<Image>().color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

            endColor = new Color(startColor.r, startColor.g, startColor.b, endAlpha);

            StartCoroutine(StartOpacitySprite());
        }
    }

    private IEnumerator StartOpacitySprite()
    {
        yield return new WaitForSeconds(delay);

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, endColor, time);

            yield return null;
        }
    }

    private IEnumerator StartOpacityText()
    {
        yield return new WaitForSeconds(delay);

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            GetComponent<TMP_Text>().color = Color.Lerp(GetComponent<TMP_Text>().color, endColor, time);

            yield return null;
        }
    }

    private IEnumerator StartOpacitySpriteRenderer()
    {
        yield return new WaitForSeconds(delay);

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, endColor, time);

            yield return null;
        }
    }
}
