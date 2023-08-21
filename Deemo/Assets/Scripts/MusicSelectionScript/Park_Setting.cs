using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_Setting : MonoBehaviour
{
    public GameObject optionUi;

    // 실행 중인 코루틴 추적하기
    private Coroutine startOverScale;
    private Coroutine startOrignalScale;

    // 마우스 포인터가 오브젝트 위에 있는지 체크 하기 위한 변수(Camera)
    public Camera mainCamera;

    // 마우스 포인터가 오브젝트 위에 있는지 체크 하기 위한 변수(LayerMask)
    public LayerMask targetLayer;

    // 이미지 크기 변경을 위한 변수
    private Vector2 orignalScale;
    private Vector2 pressedScale;
    private Vector2 overScale;

    // 처음 클릭한 마우스 위치
    private Vector2 startMousePosition;

    // 현재 마우스 위치
    private Vector2 moveMousePosition;

    // 이미지 크기 배수 변경의 위한 변수
    public float pressedMult;
    public float overMult;

    // 크기 변경을 부드럽게 하기 위한 변수
    private float durationFirst = 0.2f;
    private float durationSecound = 0.1f;

    private bool isPressed = false;
    private bool isPoint = false;
    private bool isCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        optionUi.SetActive(false);

        // 이미지 크기 지정
        orignalScale = transform.localScale;
        pressedScale = orignalScale * pressedMult;
        overScale = orignalScale * overMult;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == true)
        {
            moveMousePosition = Input.mousePosition;

            transform.localScale = pressedScale;
        }

        if (startMousePosition != (Vector2)Input.mousePosition)
        {
            isPoint = false;
        }
    }

    private void OnMouseDown()
    {
        // 코루틴을 중지하고 새로운 코루틴을 시작
        if (startOverScale != null) { StopCoroutine(startOverScale); }
        if (startOrignalScale != null) { StopCoroutine(startOrignalScale); }

        isPoint = true;

        isPressed = true;
    }

    private void OnMouseUp()
    {
        if (isPoint == true)
        {
            if (isCheck == false)
            {
                startOverScale = StartCoroutine(StartOverScale());

                optionUi.SetActive(true);
                optionUi.SetActive(false);
                optionUi.SetActive(true);

                isCheck = true;
            }
            else
            {
                optionUi.SetActive(true);
            }
        }

        isPressed = false;
    }

    private void OnMouseDrag()
    {
        // 마우스 포인터 위치를 카메라 스크린 좌표로 변환
        Vector3 mousePosition = Input.mousePosition;

        // 카메라 스크린 좌표를 레이로 변환
        Vector2 rayOrigin = mainCamera.ScreenToWorldPoint(mousePosition);

        // 레이캐스트로 2D 오브젝트와의 충돌 검사
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, 0f, targetLayer);

        if (hit.collider != null)
        {
            isPoint = true;
        }
        else
        {
            StartCoroutine(StartOverScale());

            isPressed = false;
            isPoint = false;
        }
    }

    private IEnumerator StartOverScale()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < durationFirst)
        {
            // 시간 경과에 따라 스케일 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / durationFirst);

            // 보간된 값을 사용하여 스케일 값을 변경
            transform.localScale = Vector2.Lerp(pressedScale, overScale, time);

            yield return null;
        }

        startOrignalScale = StartCoroutine(StartOrignalScale());
    }

    private IEnumerator StartOrignalScale()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < durationSecound)
        {
            // 시간 경과에 따라 스케일 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / durationSecound);

            // 보간된 값을 사용하여 스케일 값을 변경
            transform.localScale = Vector2.Lerp(overScale, orignalScale, time);

            yield return null;
        }
    }
}