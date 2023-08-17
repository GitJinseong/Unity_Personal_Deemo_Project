using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_OpacityObjectOne : MonoBehaviour
{
    public GameObject targetObject;

    private CanvasGroup canvasGroup;

    public float duration = 0.0f;

    public float alphaStart, alphaEnd;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StartCoroutine(Opacity());
    }

    private IEnumerator Opacity()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 알파값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용해서 moveStart에서 moveEnd로 이동
            canvasGroup.alpha = Mathf.Lerp(alphaStart, alphaEnd, time);

            yield return null;
        }

        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }
}
