using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Park_ScrollTitle : MonoBehaviour
{
    // 속도값을 가져오기 위한 다른 스크립트
    private Park_OnClickEffectBackground onClickEffectBackground;

    // 컴포넌트 canvasGroup을 저장하기 위한 변수
    private CanvasGroup canvasGroup;

    // 오브젝트 RectTransform 를 불러오기 위한 변수
    private RectTransform rectTransform;

    // 원의 중심 좌표
    public Vector2 centerPos = Vector2.zero;

    // 원의 반지름
    private float radius = 5.0f;

    // 가까워질수록 투명해지는 좌표(x)
    private float minDistance = 2.0f;

    // 가까워질수록 불투명해지는 좌표(x)
    private float maxDistance;

    // 드래그 속도
    public float velocity;

    // alphaXpos 와 현재위치 x 값간의 거리를 저장하기 위한 변수
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        onClickEffectBackground = GameObject.Find("Background").GetComponent<Park_OnClickEffectBackground>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        maxDistance = centerPos.x + (radius * 1.5f * Mathf.Cos(velocity * Mathf.Deg2Rad));  
    }

    // Update is called once per frame
    void Update()
    {
        velocity = onClickEffectBackground.currentVelocity * 0.05f;

        transform.position = centerPos + new Vector2(radius * 1.5f * Mathf.Cos(velocity * Mathf.Deg2Rad), radius * Mathf.Sin(velocity * Mathf.Deg2Rad));

        distance = Vector2.Distance(new Vector2(maxDistance, 0), new Vector2(centerPos.x + (radius * 1.5f * Mathf.Cos(velocity * Mathf.Deg2Rad)), 0));

        canvasGroup.alpha = 1.0f - Mathf.Clamp01(distance*2 / Mathf.Abs(maxDistance));

    }
}
