using System.Collections;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public Vector2 initialPosition = Vector2.zero;      // 시작 지점
    public Vector2 targetPosition = Vector2.zero;       // 목표 지점
    public float duration = 1.0f;                       // 걸리는 시간
    private Coroutine coroutine;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        coroutine = StartCoroutine(ScrollObjectCoroutine());
    }

    // 스크롤 코루틴
    private IEnumerator ScrollObjectCoroutine()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 거리 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변경
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용하여 스케일 값을 변경
            Vector2 newPos = Vector2.Lerp(initialPosition, targetPosition, time);
            rectTransform.anchoredPosition = newPos;

            // 다음 프레임까지 대기
            yield return null;
        }

        // 목표 지점에 정확하게 위치하도록 설정
        rectTransform.anchoredPosition = targetPosition;
    }
}
