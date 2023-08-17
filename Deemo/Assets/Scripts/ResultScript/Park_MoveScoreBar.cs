using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Park_MoveScoreBar : MonoBehaviour
{
    private Image image;

    public float duration = 0.0f;

    public float moveStart, moveEnd;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 알파값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용해서 moveStart에서 moveEnd로 이동
            image.fillAmount = Mathf.Lerp(0.0f, 0.5f, time * time);

            yield return null;
        }

        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = 1.0f - Mathf.Pow(1.0f - Mathf.Clamp01(timeElapsed / duration), 2);

            image.fillAmount = Mathf.Lerp(0.5f, 1.0f, time);

            yield return null;
        }
    }
}
