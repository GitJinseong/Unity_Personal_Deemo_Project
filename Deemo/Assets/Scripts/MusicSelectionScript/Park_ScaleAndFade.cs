using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_ScaleAndFade : MonoBehaviour
{
    // 점점 투명해지게 하기 위한 변수
    private CanvasGroup canvasGroup;

    // 이미지 크기 변경을 위한 변수
    public Vector2 zeroScale = Vector2.zero;
    public Vector2 maxScale = Vector2.zero;

    // 알파값, 크기 변경을 부드럽게 하기 위한 변수
    private float duration = 2.0f;

    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        // 알파값, 크기 초기화
        canvasGroup.alpha = 1.0f;
        transform.localScale = zeroScale;

        StartCoroutine(StartMaxScale());
    }
    
    private IEnumerator StartMaxScale()
    {
        float timeElapsed = 0.0f;
        
        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 알파값, 스케일 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용해서 스케일 값, 알파 값을 변경
            transform.localScale = Vector2.Lerp(zeroScale, maxScale, time);
            canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, time);

            yield return null;
        }
    }
}
