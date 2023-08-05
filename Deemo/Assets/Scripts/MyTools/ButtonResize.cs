using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonResize : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Image buttonImage;             // 버튼의 Image 컴포넌트를 참조하기 위한 변수
    private RectTransform buttonRectTransform; // 버튼 이미지의 RectTransform
    private Vector2 originalPivot;         // 버튼 이미지의 원래 피벗을 저장하기 위한 변수
    private Vector3 originalScale;         // 버튼의 원래 스케일을 저장하기 위한 변수
    private bool isPressed = false;        // 버튼이 눌렸는지 여부를 확인하는 플래그
    private bool isResizing = false;       // 이미지 사이즈 조절 중인지 확인하는 플래그
    private bool isPointerOverButton = false; // 마우스 커서가 버튼 위에 있는지 확인하는 플래그

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonRectTransform = buttonImage.rectTransform;
        originalPivot = buttonRectTransform.pivot;
        originalScale = buttonRectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isResizing)
        {
            isPressed = true;
            ResizeButtonImage(0.96f); // 이미지 사이즈를 96%로 축소
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPressed)
        {
            isPressed = false;

            if (!isPointerOverButton)
            {
                ResetButtonImageScale(); // 마우스 커서가 버튼 밖으로 이동했을 때 이미지 사이즈를 원래 크기로 되돌림
            }
        }
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
        if (isPressed)
        {
            // 버튼이 눌린 상태에서 추가적인 동작을 원한다면 여기에 필요한 로직을 추가하세요.
        }
    }

    private void ResizeButtonImage(float scaleFactor)
    {
        isResizing = true;
        buttonRectTransform.localScale = originalScale * scaleFactor;

        // 이미지 사이즈가 변경된 후, 피벗을 변경하여 중앙 정렬 유지
        buttonRectTransform.pivot = new Vector2(0.5f, 0.5f);

        isResizing = false;
    }

    private void ResetButtonImageScale()
    {
        buttonRectTransform.localScale = originalScale;
        buttonRectTransform.pivot = originalPivot;
    }
}
