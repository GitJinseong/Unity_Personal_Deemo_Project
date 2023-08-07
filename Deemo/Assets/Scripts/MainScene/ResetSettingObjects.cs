using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetSettingObjects : MonoBehaviour
{
    public float fadeDuration = 1.0f; // 페이드 아웃에 걸리는 시간 (초)
    public float targetAlpha = 0.5f; // 페이드 아웃이 완료될 때의 알파값

    private float currentTime = 0f; // 현재 경과 시간

    private Image[] childImages; // 자식 이미지 컴포넌트 배열
    private TMP_Text[] childTexts; // 자식 TMP 텍스트 컴포넌트 배열

    private Color[] originalImageColors; // 원본 이미지 색상 배열
    private Color[] originalTextColors; // 원본 TMP 텍스트 색상 배열

    // 페이드 아웃 효과를 시작하는 함수
    public void StartFadeOut()
    {
        currentTime = 0f; // 페이드 아웃 시작시 현재 시간을 초기화합니다.
        gameObject.SetActive(true); // 스크립트가 활성화된 오브젝트를 활성화합니다.

        // 이미지 컴포넌트를 찾아서 페이드 아웃 효과를 적용합니다.
        childImages = GetComponentsInChildren<Image>();
        originalImageColors = new Color[childImages.Length];
        for (int i = 0; i < childImages.Length; i++)
        {
            originalImageColors[i] = childImages[i].color;
            StartFadeOutImage(childImages[i]);
        }

        // 이미지의 자식 오브젝트에서 TMP 텍스트 컴포넌트를 찾아서 페이드 아웃 효과를 적용합니다.
        childTexts = GetComponentsInChildren<TMP_Text>();
        originalTextColors = new Color[childTexts.Length];
        for (int i = 0; i < childTexts.Length; i++)
        {
            originalTextColors[i] = childTexts[i].color;
            StartFadeOutText(childTexts[i]);
        }
    }

    private void Update()
    {
        // 페이드 아웃 효과를 적용합니다.
        if (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
        }
    }

    // 이미지 페이드 아웃 효과를 시작합니다.
    private void StartFadeOutImage(Image image)
    {
        StartCoroutine(FadeOutCoroutine(image, originalImageColors));
    }

    // TMP 텍스트 페이드 아웃 효과를 시작합니다.
    private void StartFadeOutText(TMP_Text text)
    {
        StartCoroutine(FadeOutCoroutine(text, originalTextColors));
    }

    // 페이드 아웃 코루틴 함수
    private IEnumerator FadeOutCoroutine(Graphic graphic, Color[] originalColors)
    {
        float startAlpha = graphic.color.a;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float normalizedTime = currentTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);

            Color color = originalColors[0];
            color.a = currentAlpha;
            graphic.color = color;

            yield return null;
        }

        // 페이드 아웃이 끝난 후, 투명도를 목표 알파값으로 설정합니다.
        Color finalColor = originalColors[0];
        finalColor.a = targetAlpha;
        graphic.color = finalColor;
    }
}
