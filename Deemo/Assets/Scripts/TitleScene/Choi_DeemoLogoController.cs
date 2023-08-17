using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Choi_DeemoLogoController : MonoBehaviour
{
    public float fadeInDuration = 5f; // 페이드 인이 완료되는 시간
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float startAlpha = 0f; // 초기 알파값 (완전 투명)
        float endAlpha = 1f; // 목표 알파값 (완전 불투명)
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / fadeInDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, percentage);
            SetAlpha(currentAlpha);
            yield return null;
        }

        SetAlpha(endAlpha); // 최종 알파값을 설정하여 완전 불투명으로 만듦
    }

    // 이미지의 알파값을 설정하는 함수
    private void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}