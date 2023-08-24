using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_BtnSetting : MonoBehaviour
{
    public GameObject settingBtn;
    public GameObject btnGroup;

    // 마우스 포인터가 오브젝트 위에 있는지 체크 하기 위한 변수(Camera)
    public Camera mainCamera;

    // 마우스 포인터가 오브젝트 위에 있는지 체크 하기 위한 변수(LayerMask)
    public LayerMask targetLayer;

    // 이미지 크기 변경을 위한 변수
    private Vector2 orignalScale;
    private Vector2 pressedScale;
    private Vector2 overScale;

    // 이미지 크기 배수 변경의 위한 변수
    public float pressedMult;
    public float overMult;

    // 크기 변경을 부드럽게 하기 위한 변수
    private float durationFirst = 0.2f;
    private float durationSecound = 0.1f;

    void Start()
    {
        // 이미지 크기 지정
        orignalScale = settingBtn.GetComponent<RectTransform>().localScale;
        pressedScale = orignalScale * pressedMult;
        overScale = orignalScale * overMult;
    }

    private void OnMouseDown()
    {
        StartCoroutine(StartOverScale());

        btnGroup.SetActive(true);
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
            settingBtn.GetComponent<RectTransform>().localScale = Vector2.Lerp(pressedScale, overScale, time);

            yield return null;
        }

        StartCoroutine(StartOrignalScale());
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
            settingBtn.GetComponent<RectTransform>().localScale = Vector2.Lerp(overScale, orignalScale, time);

            yield return null;
        }
    }
}
