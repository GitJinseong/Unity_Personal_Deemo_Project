using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Park_MainSceneOpacity : MonoBehaviour
{
    public float duration;

    void Start()
    {
        StartCoroutine(StartOpacity());
    }

    public IEnumerator StartOpacity()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0.0f, 1.0f, time);

            yield return null;
        }
    }

    public IEnumerator EndOpacity()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1.0f, 0.0f, time);

            yield return null;
        }
    }
}
