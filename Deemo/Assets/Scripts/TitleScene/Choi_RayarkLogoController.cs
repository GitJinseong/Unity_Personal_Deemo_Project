using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Choi_RayarkLogoController : MonoBehaviour
{
    public float fadeInDuration = 3f; // 페이드 인이 완료되는 시간
    public float fadeOutDelay = 4f; // 페이드 아웃이 시작되는 딜레이 시간
    public float fadeOutDuration = 1f; // 페이드 아웃이 완료되는 시간
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float startTime = Time.time;
        float startAlpha = 0f; // 초기 알파값 (완전 투명)
        float endAlpha = 1f; // 목표 알파값 (완전 불투명)

        while (Time.time < startTime + fadeInDuration)
        {
            float elapsedTime = Time.time - startTime;
            float percentage = elapsedTime / fadeInDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, percentage);

            SetAlpha(currentAlpha);
            yield return null;
        }

        SetAlpha(endAlpha); // 최종 알파값을 설정하여 완전 불투명으로 만듦

        StartCoroutine(FadeOutCoroutine()); // 페이드 인이 완료된 후 페이드 아웃 시작
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(fadeOutDelay);

        float startTime = Time.time;
        float startAlpha = 1f; // 초기 알파값 (완전 불투명)
        float endAlpha = 0f; // 목표 알파값 (완전 투명)

        while (Time.time < startTime + fadeOutDuration)
        {
            float elapsedTime = Time.time - startTime;
            float percentage = elapsedTime / fadeOutDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, percentage);

            SetAlpha(currentAlpha);
            yield return null;
        }

        SetAlpha(endAlpha); // 최종 알파값을 설정하여 완전 투명으로 만듦
    }

    // 이미지의 알파값을 설정하는 함수
    private void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
