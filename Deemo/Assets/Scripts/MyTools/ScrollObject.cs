using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public Vector2 initialPosition = Vector2.zero;      // 시작 지점
    public Vector2 targetPosition = Vector2.zero;       // 목표 지점
    public float duration = 1.0f;                       // 걸리는 시간

    private float timeElapsed = 0.0f;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 시간 경과에 따라 거리 값 보간
        timeElapsed += Time.deltaTime;

        // Lerp 값을 0 ~ 1 사이로 변경
        float time = Mathf.Clamp01(timeElapsed / duration);

        // 보간된 값을 사용하여 스케일 값을 변경
        Vector2 newPos = Vector2.Lerp(initialPosition, targetPosition, time);
        rectTransform.anchoredPosition = newPos;
    }
}
