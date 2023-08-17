using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Park_OnClickSettingLeft : MonoBehaviour
{
    // �ڵ��� ��ǥ�� �ʱ�ȭ �����ֱ� ���� ����
    public GameObject scrollHandleObject;

    // �ڵ��� ��ġ�� �������� ���� ����
    public Park_ScrollHandle scrollHandle;

    // ��Ȱ��ȭ ��Ű�� ���� ����
    public GameObject center;
    public GameObject right;

    // �ݶ��̴� ���� ����
    private Collider2D collider;

    // ���� ����
    public TMP_Text bgmText;

    // ��ư�� ������ �ִ��� Ȯ��
    private bool isPressed = false;

    // ���� �Ǿ����� Ȯ��
    public bool isCheck = false;

    private void Awake()
    {
        GetComponent<CanvasGroup>().alpha = 0.0f;
        GetComponent<Park_OpacityObject>().enabled = false;
        isCheck = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        GetComponent<CanvasGroup>().alpha = 0.0f;
        GetComponent<Park_OpacityObject>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheck == true)
        {
            bgmText.text = string.Format("{0:F1}", (scrollHandle.xPos * 0.5f) + 1.0f);

            Park_GameManager.instance.Bgm((scrollHandle.xPos * 0.5f) + 1.0f);
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

                scrollHandleObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(((Park_GameManager.instance.bgm - 1) * 100.0f) - 400.0f, scrollHandleObject.GetComponent<RectTransform>().anchoredPosition.y);
                
                center.GetComponent<Park_OpacityObject>().enabled = false;
                center.GetComponent<Park_OnClickSettingCenter>().isCheck = false;
                center.GetComponent<CanvasGroup>().alpha = 0.0f;

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