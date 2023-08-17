using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Park_OnClickSettingUi : MonoBehaviour
{
    public Park_OnClickSettingBackground onClickSettingBackground;

    // 코루틴을 추적하기 위한 변수
    private Coroutine startAlpha;
    private Coroutine stopAlpha;

    // 점점 불투명해지게 하기 위한 변수
    private CanvasGroup canvasGroup;

    // 알파값을 변경하기 위한 변수
    private float duration = 0.5f;

    // 코루틴이 진행중인지 확인
    public bool isCoroutine = false;

    // 호출이 끝났는지 확인
    public bool isEnable = false;

    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onClickSettingBackground.isCheck == true)
        {
            stopAlpha = StartCoroutine(StopAlpha());

            onClickSettingBackground.isCheck = false;
        }
    }

    private void OnEnable()
    {
        // 알파값 초기화
        canvasGroup.alpha = 0.0f;

        // 호출 되었음
        isEnable = true;

        startAlpha = StartCoroutine(StartAlpha());
    }
    // Update is called once per frame

    private IEnumerator StartAlpha()
    {
        isCoroutine = true;

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 알파값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용해서 알파값을 변경
            canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, time);

            yield return null;
        }

        isCoroutine = false;
    }

    private IEnumerator StopAlpha()
    {
        isCoroutine = true;

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 알파값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용해서 알파값을 변경
            canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, time);

            yield return null;
        }

        isCoroutine = false;

        // 호출이 끝났음
        isEnable = false;

        gameObject.SetActive(false);
    }
}
