using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_ScaleObject : MonoBehaviour
{
    public Vector2 sizeMin = Vector2.zero;      // 최소 크기
    public Vector2 sizeMax = Vector2.zero;      // 최대 크기
    public float duration = 1.0f;               // 걸리는 시간

    private float timeElapsed = 0.0f;

    private bool isScaling = true;

    //private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        //rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 시간 경과에 따라 스케일 값 보간
        timeElapsed += Time.deltaTime;

        // Lerp 값을 0 ~ 1 사이로 변환
        float time = Mathf.Clamp01(timeElapsed / duration);

        // 보간된 값을 사용하여 스케일 값을 변경
        Vector2 newSize = Vector2.Lerp(sizeMin, sizeMax, time);
        transform.localScale = newSize;
        //rectTransform.sizeDelta = newSize;

        // 크기 변화 반복
        if (1.0f <= time)
        {
            isScaling = !isScaling;
            timeElapsed = 0.0f;
        }       // if: 크기가 최대로 커졌을 때 방향을 바꿔서 반복
    }
}
