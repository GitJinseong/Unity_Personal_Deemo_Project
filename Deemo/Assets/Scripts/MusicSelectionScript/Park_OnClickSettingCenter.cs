using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Park_OnClickSettingCenter : MonoBehaviour
{
    // �ڵ��� ��ǥ�� �ʱ�ȭ �����ֱ� ���� ����
    public GameObject scrollHandleObject;

    // �ڵ��� ��ġ�� �������� ���� ����
    public Park_ScrollHandle scrollHandle;

    // ��Ȱ��ȭ ��Ű�� ���� �Լ�
    public GameObject left;
    public GameObject right;

    // �ݶ��̴� ���� ����
    private Collider2D collider;

    // ���� ����
    public TMP_Text speedText;

    // ��ư�� ������ �ִ��� Ȯ��
    private bool isPressed = false;

    // ���� �Ǿ����� Ȯ��
    public bool isCheck = false;

    private void Awake()
    {
        GetComponent<CanvasGroup>().alpha = 0.0f;
        GetComponent<Park_OpacityObject>().enabled = true;
        isCheck = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
        GetComponent<Park_OpacityObject>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheck == true)
        {
            speedText.text = string.Format("{0:F1}", scrollHandle.xPos / 2.0f + 1.0f);

            Park_GameManager.instance.Speed((scrollHandle.xPos * 0.5f) + 1.0f);
        }
    }

    private void OnMouseDown()
    {
        if (GetComponent<Park_OpacityObject>().enabled == false)
        {
            isPressed = true;
        }
    }

    private void OnMouseUp()
    {
        if (isPressed == true)
        {
            // ���콺 �������� ���� ��ǥ�� ��ȯ
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition.z = 0; // 2D ���������� z ��ǥ�� 0���� ����

            // ���콺 �������� �ݶ��̴� �ȿ� �ִ��� üũ
            if (collider.OverlapPoint(mousePosition) == true)
            {
                Park_GameManager.instance.HandleCoroutine(true);

                scrollHandleObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(((Park_GameManager.instance.speed - 1.0f) * 100.0f) - 400.0f, scrollHandleObject.GetComponent<RectTransform>().anchoredPosition.y);

                left.GetComponent<Park_OpacityObject>().enabled = false;
                left.GetComponent<Park_OnClickSettingLeft>().isCheck = false;
                left.GetComponent<CanvasGroup>().alpha = 0.0f;

                right.GetComponent<Park_OpacityObject>().enabled = false;
                right.GetComponent<Park_OnClickSettingRight>().isCheck = false;
                right.GetComponent<CanvasGroup>().alpha = 0.0f;

                GetComponent<CanvasGroup>().alpha = 0.0f;
                GetComponent<Park_OpacityObject>().enabled = true;
                isCheck = true;
            }

            isPressed = false;
        }
    }
}