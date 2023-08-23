using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Park_OnClickEffectBack : MonoBehaviour
{
    public Park_MainSceneOpacity park_MainSceneOpacity;

    // 실행 중인 코루틴 추적하기
    private Coroutine startOverScale;
    private Coroutine startOrignalScale;

    // 마우스 포인터가 오브젝트 위에 있는지 체크 하기 위한 변수(Camera)
    public Camera mainCamera;

    // 마우스 포인터가 오브젝트 위에 있는지 체크 하기 위한 변수(LayerMask)
    public LayerMask targetLayer;

    // 로드 씬용 변수
    public Choi_LoadScene loadScene;

    // 이미지 크기 변경을 위한 변수
    private Vector2 orignalScale;
    private Vector2 pressedScale;
    private Vector2 overScale;

    // 씬 변경을 위한 변수
    public string sceneName;
    public float delay = 0f;

    // 이미지 크기 배수 변경의 위한 변수
    public float pressedMult = 0.9f;
    public float overMult = 1.05f;

    // 크기 변경을 부드럽게 하기 위한 변수
    private float durationFirst = 0.2f;
    private float durationSecound = 0.1f;

    // 버튼이 눌리고 있는지 확인
    private bool isPressed = false;

    // 마우스 포인터가 오브젝트 안에 있는지 확인
    private bool isPoint = false;

    // Start is called before the first frame update
    void Start()
    {
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
            transform.localScale = pressedScale;
        }
    }

    private void OnMouseDown()
    {
        // 코루틴을 중지하고 새로운 코루틴을 시작
        if (startOverScale != null) { StopCoroutine(startOverScale); }
        if (startOrignalScale != null) { StopCoroutine(startOrignalScale); }

        isPressed = true;
    }

    private void OnMouseUp()
    {
        if (isPoint == true)
        {
            startOverScale = StartCoroutine(StartOverScale());

            loadScene.Run(delay, sceneName);

            StartCoroutine(park_MainSceneOpacity.EndOpacity());

            isPressed = false;
        }
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
