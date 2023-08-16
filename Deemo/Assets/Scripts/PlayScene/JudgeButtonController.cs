using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JudgeButtonController : MonoBehaviour, IPointerDownHandler
{
    public GameObject obj_JudgeCollider; // 판정용 콜라이더
    public JudgeColliderController script_JudgeCollider;
    public RectTransform rectTransform_JudgeCollider;
    public bool isActive = false;

    private const float POS_Y = -180f; // 절대 좌표로 설정할 Y 값
    private float delay = 0.1f; // 콜라이더를 자동으로 종료할 시간
    private Vector3 touchPosition;

    private void Awake()
    {
        script_JudgeCollider = obj_JudgeCollider.GetComponent<JudgeColliderController>();
        rectTransform_JudgeCollider = obj_JudgeCollider.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchPosition = eventData.position;
        SetActiveTrue();
    }

    public void SetActiveTrue()
    {
        if (isActive == false && GameManager.instance.activatedJudgeColliderCount < 2)
        {
            isActive = true;

            // 오브젝트 활성화
            obj_JudgeCollider.SetActive(true);

            // 오브젝트 충돌 가능여부 초기화
            script_JudgeCollider.isProcessed = false;

            // 터치된 화면 좌표를 월드 좌표로 변환
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, POS_Y, 0f));

            // 콜라이더의 X 좌표를 카메라 시야 범위 내로 제한
            float clampedX = Mathf.Clamp(worldPosition.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x);

            // 콜라이더의 위치 설정 (x좌표는 월드 좌표, y 좌표는 POS_Y, z 좌표는 0f로 설정)
            obj_JudgeCollider.transform.position = new Vector3(clampedX, 0f, 0f);
            // 아래는 y와, z좌표를 상대 좌표로 설정
            rectTransform_JudgeCollider.anchoredPosition = new Vector3(rectTransform_JudgeCollider.anchoredPosition.x, POS_Y, 0f);
            

            StartCoroutine(DelayForActiveFalse());
        }
    }

    private IEnumerator DelayForActiveFalse()
    {
        yield return new WaitForSeconds(delay);

        obj_JudgeCollider.SetActive(false);
        isActive = false;
    }
}
