using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene_BackgroundController : MonoBehaviour
{
    public Vector2 initialPosition = Vector2.zero;      // 시작 지점
    public Vector2 targetPosition = Vector2.zero;       // 목표 지점
    public float duration = 1.0f;                       // 걸리는 시간

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(MoveBackgroundCoroutine());
    }

    // Coroutine for moving the background
    private IEnumerator MoveBackgroundCoroutine()
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 거리 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변경 (easeOutQuad 함수 사용)
            float time = Mathf.Clamp01(timeElapsed / duration);
            time = EaseOutQuad(time);

            // 보간된 값을 사용하여 스케일 값을 변경
            Vector2 newPos = Vector2.Lerp(initialPosition, targetPosition, time);
            rectTransform.anchoredPosition = newPos;

            yield return null;
        }

        // Ensure the final position is reached
        rectTransform.anchoredPosition = targetPosition;
    }

    // 빠르게 시작했다가 서서히 느려지는 이징 함수 (easeOutQuad)
    private float EaseOutQuad(float t)
    {
        return 1f - Mathf.Pow(1f - t, 2f);
    }
}
