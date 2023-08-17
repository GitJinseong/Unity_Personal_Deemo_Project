using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_ScaleObjectLoop : MonoBehaviour
{
    private RectTransform rectTransform;

    public Vector2 scaleStart = Vector2.zero;
    public Vector2 scaleEnd = Vector2.zero;

    public float duration = 0.0f;

    // 코루틴 실행이 완료 되었는지 확인
    private bool isCheck = false;

    // 시간을 저장할 공간
    private float timeElapsed;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        // 시간값 초기화
        timeElapsed = 0.0f;
    }

    private void Update()
    {
        if (isCheck == false)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            rectTransform.localScale = new Vector2(
                Mathf.Lerp(scaleEnd.x, scaleStart.x, time),
                Mathf.Lerp(scaleEnd.y, scaleStart.y, time)
                );

            if (time >= 1.0f)
            {
                isCheck = true;

                timeElapsed = 0.0f;
            }
        }
        else
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            rectTransform.localScale = new Vector2(
                Mathf.Lerp(scaleStart.x, scaleEnd.x, time),
                Mathf.Lerp(scaleStart.y, scaleEnd.y, time)
                );

            if (time >= 1.0f)
            {
                isCheck = false;

                timeElapsed = 0.0f;
            }
        }
    }
}
