using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonResize : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Image buttonImage;             // ��ư�� Image ������Ʈ�� �����ϱ� ���� ����
    private RectTransform buttonRectTransform; // ��ư �̹����� RectTransform
    private Vector2 originalPivot;         // ��ư �̹����� ���� �ǹ��� �����ϱ� ���� ����
    private Vector3 originalScale;         // ��ư�� ���� �������� �����ϱ� ���� ����
    private bool isPressed = false;        // ��ư�� ���ȴ��� ���θ� Ȯ���ϴ� �÷���
    private bool isResizing = false;       // �̹��� ������ ���� ������ Ȯ���ϴ� �÷���
    private bool isPointerOverButton = false; // ���콺 Ŀ���� ��ư ���� �ִ��� Ȯ���ϴ� �÷���

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
            ResizeButtonImage(0.96f); // �̹��� ����� 96%�� ���
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPressed)
        {
            isPressed = false;

            if (!isPointerOverButton)
            {
                ResetButtonImageScale(); // ���콺 Ŀ���� ��ư ������ �̵����� �� �̹��� ����� ���� ũ��� �ǵ���
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverButton = false;
        if (isPressed)
        {
            isPressed = false;
            ResetButtonImageScale(); // ���콺 Ŀ���� ��ư ������ �̵����� �� �̹��� ����� ���� ũ��� �ǵ���
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
            // ��ư�� ���� ���¿��� �߰����� ������ ���Ѵٸ� ���⿡ �ʿ��� ������ �߰��ϼ���.
        }
    }

    private void ResizeButtonImage(float scaleFactor)
    {
        isResizing = true;
        buttonRectTransform.localScale = originalScale * scaleFactor;

        // �̹��� ����� ����� ��, �ǹ��� �����Ͽ� �߾� ���� ����
        buttonRectTransform.pivot = new Vector2(0.5f, 0.5f);

        isResizing = false;
    }

    private void ResetButtonImageScale()
    {
        buttonRectTransform.localScale = originalScale;
        buttonRectTransform.pivot = originalPivot;
    }
}
