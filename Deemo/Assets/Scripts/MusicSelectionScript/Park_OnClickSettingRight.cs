using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Park_OnClickSettingRight : MonoBehaviour
{
    // 핸들의 좌표를 초기화 시켜주기 위한 변수
    public GameObject scrollHandleObject;

    // 핸들의 위치를 가져오기 위한 변수
    public Park_ScrollHandle scrollHandle;

    // 비활성화 시키기 위한 함수
    public GameObject left;
    public GameObject center;

    // 콜라이더 저장 공간
    private Collider2D collider;

    // 숫자 변경
    public TMP_Text keyText;

    // 버튼이 눌리고 있는지 확인
    private bool isPressed = false;

    // 실행 되었는지 확인
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
        isCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheck == true)
        {
            keyText.text = string.Format("{0:F1}", scrollHandle.xPos / 2.0f + 1.0f);

            Park_GameManager.instance.Key((scrollHandle.xPos * 0.5f) + 1.0f);
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
            // 마우스 포지션을 월드 좌표로 변환
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition.z = 0; // 2D 공간에서는 z 좌표를 0으로 설정

            // 마우스 포지션이 콜라이더 안에 있는지 체크
            if (collider.OverlapPoint(mousePosition) == true)
            {
                Park_GameManager.instance.HandleCoroutine(true); 
                


                left.GetComponent<Park_OpacityObject>().enabled = false;
                left.GetComponent<Park_OnClickSettingLeft>().isCheck = false;
                left.GetComponent<CanvasGroup>().alpha = 0.0f;

                center.GetComponent<Park_OpacityObject>().enabled = false;
                center.GetComponent<Park_OnClickSettingCenter>().isCheck = false;
                center.GetComponent<CanvasGroup>().alpha = 0.0f;

                
                GetComponent<CanvasGroup>().alpha = 0.0f;
                GetComponent<Park_OpacityObject>().enabled = true;
                isCheck = true;
                scrollHandleObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(((Park_GameManager.instance.key - 1.0f) * 100.0f) - 400.0f, scrollHandleObject.GetComponent<RectTransform>().anchoredPosition.y);
            }

            isPressed = false;
        }
    }
}
