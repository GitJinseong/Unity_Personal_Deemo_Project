using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Park_OnClickEffectDifficulty : MonoBehaviour
{
    // 프리팹이 호출되는 위치
    public GameObject prefabSave;

    // 클릭과 동시에 호출할 프리팹
    public GameObject prefab;

    // 클릭과 동시에 움직일 오브젝트
    public GameObject namebar;
    public GameObject scorebar;

    // 이미지를 바꿀 대상의 이미지
    public Image targetDifficulty;
    public Image targetBar;

    // Easy, Normal, Hard 이미지 저장
    public Sprite newDifficultyEasy;
    public Sprite newDifficultyNormal;
    public Sprite newDifficultyHard;
    public Sprite newBarEasy;
    public Sprite newBarNormal;
    public Sprite newBarHard;

    // 초기 이미지는 쉬운 난이도 이미지로 설정
    private Sprite currentDifficulty;
    private Sprite currentBar;

    // 실행 중인 코루틴 추적하기
    private Coroutine startOverScale;
    private Coroutine startOrignalScale;
    private Coroutine startBarScaleFirst;
    private Coroutine startBarScaleSecound;

    // 마우스 포인터가 오브젝트 위에 있는지 체크 하기 위한 변수(Camera)
    public Camera mainCamera;

    // 호출한 프리팹을 저장하기 위한 공간
    private List<GameObject> prefabList = new List<GameObject>();

    // 마우스 포인터가 오브젝트 위에 있는지 체크 하기 위한 변수(LayerMask)
    public LayerMask targetLayer;

    // 이미지 크기 변경을 위한 변수
    private Vector2 orignalScale;
    private Vector2 pressedScale;
    private Vector2 overScale;

    // 다른 오브젝트 초기 크기값 저장
    private Vector2 namebarScale;
    private Vector2 scorebarScale;

    // 크기 변경을 부드럽게 하기 위한 변수
    private float durationFirst = 0.2f;
    private float durationSecound = 0.1f;
    private float durationBar = 0.15f;

    // 버튼이 눌리고 있는지 확인
    private bool isPressed = false;

    // 마우스 포인터가 오브젝트 안에 있는지 확인
    private bool isPoint = false;

    private void Start()
    {
        // 시작 시 이미지를 쉬운 난이도 이미지로 설정합니다.
        currentDifficulty = newDifficultyEasy;
        targetDifficulty.sprite = currentDifficulty;
        currentBar = newBarEasy;
        targetBar.sprite = currentBar;

        // 이미지 크기 지정
        orignalScale = targetDifficulty.transform.localScale;
        pressedScale = orignalScale * 0.9f;
        overScale = orignalScale * 1.05f;

        // 바 위치 지정
        namebarScale = namebar.transform.localScale;
        scorebarScale = scorebar.transform.localScale;
    }

    private void Update()
    {
        if (isPressed == true)
        {
            targetDifficulty.transform.localScale = pressedScale;
        }

        if (currentDifficulty == newDifficultyEasy) { Park_GameManager.instance.Difficulty(0); }
        else if (currentDifficulty == newDifficultyNormal) { Park_GameManager.instance.Difficulty(1); }
        else if (currentDifficulty == newDifficultyHard) { Park_GameManager.instance.Difficulty(2); }
    }

    private void OnMouseDown()
    {
        // 코루틴을 중지하고 새로운 코루틴을 시작
        if (startOverScale != null) { StopCoroutine(startOverScale); }
        if (startOrignalScale != null) { StopCoroutine(startOrignalScale); }

        if (startBarScaleFirst != null) { StopCoroutine(startBarScaleFirst); }
        if (startBarScaleSecound != null) { StopCoroutine(startBarScaleSecound); }

        isPressed = true;
    }

    private void OnMouseUp()
    {
        if (isPoint == true)
        {
            for (int i = 0; i <= prefabList.Count; i++)
            {
                if (prefabList.Count == i)
                {
                    prefabList.Add(Instantiate(prefab, transform.position, Quaternion.identity, prefabSave.transform));

                    StartCoroutine(AddPrefab(i));

                    break;
                }
                else
                {
                    if (prefabList[i].activeSelf)
                    {
                        continue;
                    }
                    else
                    {
                        StartCoroutine(RecyclePrefab(i));

                        break;
                    }

                }
            }

            // 버튼을 땟을 때 난이도에 따라 이미지를 변경합니다.
            if (currentDifficulty == newDifficultyEasy) { currentDifficulty = newDifficultyNormal; }
            else if (currentDifficulty == newDifficultyNormal) { currentDifficulty = newDifficultyHard; }
            else if (currentDifficulty == newDifficultyHard) { currentDifficulty = newDifficultyEasy; }

            if (currentBar == newBarEasy) { currentBar = newBarNormal; }
            else if (currentBar == newBarNormal) { currentBar = newBarHard; }
            else if (currentBar == newBarHard) { currentBar = newBarEasy; }

            // 변경된 이미지를 targetImage에 적용합니다.
            targetDifficulty.sprite = currentDifficulty;
            targetBar.sprite = currentBar;

            startOverScale = StartCoroutine(StartOverScale());
            startBarScaleFirst = StartCoroutine(StartBarScaleFirst());

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
            targetDifficulty.transform.localScale = Vector2.Lerp(pressedScale, overScale, time);

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
            targetDifficulty.transform.localScale = Vector2.Lerp(overScale, orignalScale, time);

            yield return null;
        }
    }

    private IEnumerator StartBarScaleFirst()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < durationBar)
        {
            // 시간 경과에 따라 스케일 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / durationBar);

            // 보간된 값을 사용하여 스케일 값을 변경
            float newXScale = Mathf.Lerp(namebarScale.x, namebarScale.x * 0.8f, time);

            namebar.transform.localScale = new Vector2(newXScale, namebarScale.y);
            scorebar.transform.localScale = new Vector2(newXScale, namebarScale.y);

            yield return null;
        }

        startBarScaleSecound = StartCoroutine(StartBarScaleSecound());
    }

    private IEnumerator StartBarScaleSecound()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < durationBar)
        {
            // 시간 경과에 따라 스케일 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / durationBar);

            // 보간된 값을 사용하여 스케일 값을 변경
            float newXScale = Mathf.Lerp(namebarScale.x * 0.8f, namebarScale.x, time);

            namebar.transform.localScale = new Vector2(newXScale, namebarScale.y);
            scorebar.transform.localScale = new Vector2(newXScale, namebarScale.y);

            yield return null;
        }
    }

    private IEnumerator AddPrefab(int i)
    {
        yield return new WaitForSeconds(2.0f);

        prefabList[i].SetActive(false);

    }

    private IEnumerator RecyclePrefab(int i)
    {
        prefabList[i].SetActive(true);

        yield return new WaitForSeconds(2.0f);

        prefabList[i].SetActive(false);
    }
}
