using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Choi_ButtonResize : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    private RectTransform buttonRectTransform;
    private Vector2 originalScale;
    private bool isPressed = false;
    private bool isReleased = false;
    private bool isResizing = false;
    private bool isPointerOverButton = false;
    private bool isPointerDown = false;
    public float resizeScale = 0.93f; // 클릭된 상태에서 크기를 줄이는 비율
    public float resizeDuration = 0.3f; // 크기 조절에 걸리는 시간

    private void Start()
    {
        buttonRectTransform = GetComponent<RectTransform>();
        originalScale = buttonRectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        isPressed = true;
        isReleased = false;
        ResizeButtonImage(resizeScale); // 이미지 사이즈를 축소
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPressed && !isReleased)
        {
            isPressed = false;
            isReleased = true;

            if (!isPointerOverButton)
            {
                ResetButtonImageScale(); // 마우스 커서가 버튼 밖으로 이동했을 때 이미지 사이즈를 원래 크기로 되돌림
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverButton = false;
        if (isPressed && !isReleased)
        {
            isPressed = false;
            isReleased = true;
            ResetButtonImageScale(); // 마우스 커서가 버튼 밖으로 이동했을 때 이미지 사이즈를 원래 크기로 되돌림
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverButton = true;
    }

    private void Update()
    {
        if (isPointerDown && !isReleased)
        {
            // 버튼이 포인트되어 눌려진 상태에서 추가적인 동작을 원한다면 여기에 필요한 로직을 추가하세요.
        }
    }

    private void ResizeButtonImage(float scaleFactor)
    {
        isResizing = true;
        buttonRectTransform.localScale = originalScale * scaleFactor;

        // 이미지 사이즈가 변경된 후, 피벗을 변경하여 중앙 정렬 유지
        buttonRectTransform.pivot = new Vector2(0.5f, 0.5f);

        isResizing = false;

        // 보간을 위해 코루틴 시작
        StartCoroutine(InterpolateButtonScale(originalScale * scaleFactor, resizeDuration));
    }

    public void ResetButtonImageScale()
    {
        StartCoroutine(InterpolateButtonScale(originalScale, resizeDuration));
    }

    private IEnumerator InterpolateButtonScale(Vector2 targetScale, float duration)
    {
        float timeElapsed = 0.0f;
        Vector2 startScale = buttonRectTransform.localScale;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);
            buttonRectTransform.localScale = Vector2.Lerp(startScale, targetScale, t);
            yield return null;
        }

        buttonRectTransform.localScale = targetScale;
    }
}
