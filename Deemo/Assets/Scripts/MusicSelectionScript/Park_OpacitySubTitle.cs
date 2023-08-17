using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_OpacitySubTitle : MonoBehaviour
{
    // 컴포넌트 canvasGroup을 저장하기 위한 변수
    private CanvasGroup canvasGroup;

    // 오브젝트 RectTransform 를 불러오기 위한 변수
    private RectTransform rectTransform;

    // 가까워질수록 투명해지는 좌표(y)
    private float alphaDistance = 280.0f;

    // 계산된 좌표(y)
    private float distance = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = alphaDistance - Mathf.Abs(rectTransform.anchoredPosition.y);

        canvasGroup.alpha = Mathf.Clamp01(distance / alphaDistance);
    }
}
