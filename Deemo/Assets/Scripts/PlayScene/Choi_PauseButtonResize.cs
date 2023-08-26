using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Choi_PauseButtonResize : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    private RectTransform buttonRectTransform;
    private Vector2 originalScale;
    private bool isPressed = false;
    private bool isPointerOverButton = false;
    private bool isPointerDown = false;
    private bool isFirstClick = true; // 처음 버튼을 눌렀는지 여부를 판단하는 변수
    private float resizeScale = 0.9f; // 변경된 스케일 비율
    private float resizeDuration = 0.5f; // 크기 조절에 걸리는 시간

    private void Start()
    {
        buttonRectTransform = GetComponent<RectTransform>();
        originalScale = buttonRectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFirstClick)
        {
            isFirstClick = false;
            originalScale = buttonRectTransform.localScale; // 처음 버튼을 눌렀을 때 오리지널 스케일 값을 저장
        }

        isPointerDown = true;
        isPressed = true;
        ResizeButtonImage(resizeScale); // 이미지 사이즈를 축소
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPointerDown && isPressed)
        {
            isPressed = false;

            if (!isPointerOverButton)
            {
                ResetButtonImageScale(); // 마우스 커서가 버튼 밖으로 이동했을 때 이미지 사이즈를 원래 크기로 되돌림
            }
            else
            {
                ResizeButtonImage(1f); // 마우스 커서가 버튼 위에 있을 때 이미지 사이즈를 원래 크기로 되돌림
            }
        }

        isPointerDown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverButton = false;
        if (isPressed)
        {
            isPressed = false;
            ResetButtonImageScale(); // 마우스 커서가 버튼 밖으로 이동했을 때 이미지 사이즈를 원래 크기로 되돌림
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverButton = true;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            // 버튼이 포인트되어 눌려진 상태에서 추가적인 동작을 원한다면 여기에 필요한 로직을 추가하세요.
        }
    }

    private void ResizeButtonImage(float scaleFactor)
    {
        buttonRectTransform.localScale = originalScale * scaleFactor;

        // 이미지 사이즈가 변경된 후, 피벗을 변경하여 중앙 정렬 유지
        buttonRectTransform.pivot = new Vector2(0.5f, 0.5f);

        // 보간을 위해 코루틴 시작
        StopAllCoroutines(); // 기존의 코루틴 중단
        StartCoroutine(InterpolateButtonScale(originalScale * scaleFactor, resizeDuration));
    }

    public void ResetButtonImageScale()
    {
        StopAllCoroutines(); // 기존의 코루틴 중단
        StartCoroutine(InterpolateButtonScale(originalScale, resizeDuration));
    }

    private IEnumerator InterpolateButtonScale(Vector2 targetScale, float duration)
    {
        float startTime = Time.realtimeSinceStartup;
        Vector2 startScale = buttonRectTransform.localScale;

        while (Time.realtimeSinceStartup - startTime < duration)
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedT = Mathf.SmoothStep(0, 1, t); // 보간된 값이 0과 1 사이로 유지되도록 보간 함수 사용
            buttonRectTransform.localScale = Vector2.Lerp(startScale, targetScale, easedT);
            yield return null;
        }

        buttonRectTransform.localScale = targetScale;
    }
}
