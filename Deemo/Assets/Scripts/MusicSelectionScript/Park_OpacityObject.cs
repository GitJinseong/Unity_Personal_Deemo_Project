using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_OpacityObject : MonoBehaviour
{
    // 세팅창이 모두 꺼졌을때 코루틴을 멈추게 하기 위한 변수
    public Park_OnClickSettingUi OnClickSettingUi;

    // 컴포넌트 canvasGroup을 저장하기 위한 변수
    private CanvasGroup canvasGroup;

    // 알파값을 부드럽게 하기 위한 변수
    private float duration = 1.0f;

    // 시간을 저장할 공간
    private float timeElapsed;

    // Alpha값 최대, 최소 값
    public float maxAlpha;
    public float minAlpha;

    // 호출이 끝나는지 확인
    private bool isEnable = false;

    // 코루틴 실행이 완료 되었는지 확인
    private bool isCheck = false;

    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        isEnable = OnClickSettingUi.isEnable;
    }

    private void OnEnable()
    {
        // 시간값 초기화
        timeElapsed = 0.0f;

        // 알파값 초기화
        canvasGroup.alpha = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (isEnable == true)
        {
            if (isCheck == false)
            {
                // 시간 경과에 따라 알파값 보간
                timeElapsed += Time.deltaTime;

                // Lerp 값을 0 ~ 1 사이로 변환
                float time = Mathf.Clamp01(timeElapsed / duration);

                // 보간된 값을 사용해서 알파값을 변경
                canvasGroup.alpha = Mathf.Lerp(maxAlpha, minAlpha, time);

                if (time >= 1.0f)
                {
                    isCheck = true;

                    timeElapsed = 0.0f;
                }
            }
            else
            {
                timeElapsed += Time.deltaTime;

                float time = Mathf.Clamp01(timeElapsed / duration);

                canvasGroup.alpha = Mathf.Lerp(minAlpha, maxAlpha, time);

                if (time >= 1.0f)
                {
                    isCheck = false;
                    timeElapsed = 0.0f;
                }
            }
        }
    }
}
