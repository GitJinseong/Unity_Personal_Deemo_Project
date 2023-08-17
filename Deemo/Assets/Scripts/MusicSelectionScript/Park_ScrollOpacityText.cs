using System.Collections;
using TMPro;
using UnityEngine;

public class Park_ScrollOpacityText : MonoBehaviour
{
    public GameObject mainTitleObj, composerObj;

    public RectTransform scrollContent;

    public TMP_Text mainTitle, composer;

    private Coroutine alphaUp, alphaDown;

    private float index;

    private int titleCount;

    private float duration = 0.5f;

    private int currentIndex = -1;

    private void Start()
    {
        titleCount = Park_GameManager.instance.musicInformation["Title"].Count;
    }

    // Update is called once per frame
    void Update()
    {
        index = (scrollContent.anchoredPosition.y + titleCount * 360.0f) - 720.0f;

        for (int i = 0; i < titleCount; i++)
        {
            if (i * 720.0f - 360.0f < index && index < i * 720.0f + 360.0f)
            {
                if (i != currentIndex)
                {
                    if (alphaUp != null) { StopCoroutine(alphaUp); }
                    if (alphaDown != null) { StopCoroutine(alphaDown); }

                    StartCoroutine(ChangeText(i));
                }
            }
        }
    }

    private IEnumerator ChangeText(int newIndex)
    {
        currentIndex = newIndex;
        mainTitleObj.GetComponent<CanvasGroup>().alpha = 0.0f;
        composerObj.GetComponent<CanvasGroup>().alpha = 0.0f;

        mainTitle.text = Park_GameManager.instance.musicInformation["Title"][titleCount - currentIndex - 1];
        composer.text = Park_GameManager.instance.musicInformation["Composer"][titleCount - currentIndex - 1];

        Park_GameManager.instance.Title(mainTitle.text);

        yield return null;

        alphaDown = StartCoroutine(AlphaDown());
        alphaUp = StartCoroutine(AlphaUp());
    }

    private IEnumerator AlphaUp()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 알파값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용해서 알파값을 변경
            mainTitleObj.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0.0f, 1.0f, time);
            composerObj.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0.0f, 1.0f, time);

            yield return null;
        }

    }

    private IEnumerator AlphaDown()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 알파값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용해서 알파값을 변경
            mainTitleObj.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1.0f, 0.0f, time);
            composerObj.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1.0f, 0.0f, time);

            yield return null;
        }

    }
}