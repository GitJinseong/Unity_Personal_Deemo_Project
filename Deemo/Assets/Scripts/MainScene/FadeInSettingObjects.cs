using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInSettingObjects : MonoBehaviour
{
    public float fadeDuration = 1.0f; // 페이드 인에 걸리는 시간 (초)
    public float targetAlpha = 1.0f; // 페이드 인이 완료될 때의 알파값 (1로 불투명하게)
    public float scaleMultiplier = 1.1f; // 크기 변환 시의 배율
    private float fadeDelay = 0.2f; // 페이드 인이 시작하기전 대기

    private float currentTime = 0f; // 현재 경과 시간

    private Image[] childImages; // 자식 이미지 컴포넌트 배열
    private TMP_Text[] childTexts; // 자식 TMP 텍스트 컴포넌트 배열

    private Color[] originalImageColors; // 원본 이미지 색상 배열
    private Color[] originalTextColors; // 원본 TMP 텍스트 색상 배열

    private Vector3[] originalScales; // 원본 크기 배열

    // 페이드 인 효과를 시작하는 함수
    public void StartFadeIn()
    {
        currentTime = fadeDuration; // 페이드 인 시작시 현재 시간을 최대값으로 설정합니다.
        gameObject.SetActive(true); // 스크립트가 활성화된 오브젝트를 활성화합니다.

        // 이미지 컴포넌트를 찾아서 페이드 인 효과를 적용합니다.
        childImages = GetComponentsInChildren<Image>();
        originalImageColors = new Color[childImages.Length];
        originalScales = new Vector3[childImages.Length];
        for (int i = 0; i < childImages.Length; i++)
        {
            originalImageColors[i] = childImages[i].color;
            originalScales[i] = childImages[i].rectTransform.localScale;
            StartFadeInImage(childImages[i]);
        }

        // 이미지의 자식 오브젝트에서 TMP 텍스트 컴포넌트를 찾아서 페이드 인 효과를 적용합니다.
        childTexts = GetComponentsInChildren<TMP_Text>();
        originalTextColors = new Color[childTexts.Length];
        for (int i = 0; i < childTexts.Length; i++)
        {
            originalTextColors[i] = childTexts[i].color;
            StartFadeInText(childTexts[i]);
        }
    }

    // 이미지 페이드 인 효과를 시작합니다.
    private void StartFadeInImage(Image image)
    {
        StartCoroutine(FadeInCoroutine(image, originalImageColors, originalScales));
    }

    // TMP 텍스트 페이드 인 효과를 시작합니다.
    private void StartFadeInText(TMP_Text text)
    {
        StartCoroutine(FadeInCoroutine(text, originalTextColors, null));
    }

    // 페이드 인 코루틴 함수
     private IEnumerator FadeInCoroutine(Graphic graphic, Color[] originalColors, Vector3[] originalScales)
    {
        yield return new WaitForSeconds(fadeDelay);

        float startAlpha = graphic.color.a;
        Vector3 startScale = (originalScales != null) ? originalScales[0] : Vector3.one;
        int startFontSize = 0;
        int targetFontSize = 0;

        // Get the initial and target font size for TMP_Text component
        if (graphic is TMP_Text text)
        {
            startFontSize = (int)(startScale.x * 100f);
            targetFontSize = (int)(originalScales[0].x * scaleMultiplier * 100f);
        }

        while (currentTime > 0f) // currentTime을 감소시키며 페이드 인 효과를 적용합니다.
        {
            currentTime -= Time.deltaTime;
            float normalizedTime = currentTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(targetAlpha, startAlpha, normalizedTime); // startAlpha와 targetAlpha를 바꿔줍니다.
            float currentScaleMultiplier = Mathf.Lerp(1f, scaleMultiplier, normalizedTime); // 크기 변환을 위한 배율 계산

            Color color = originalColors[0];
            color.a = currentAlpha;
            graphic.color = color;

            if (originalScales != null)
            {
                Vector3 scale = startScale * currentScaleMultiplier;
                graphic.rectTransform.localScale = scale;
            }

            // Interpolate font size for TMP_Text component
            if (graphic is TMP_Text textComponent)
            {
                int currentFontSize = (int)Mathf.Lerp(startFontSize, targetFontSize, normalizedTime);
                textComponent.fontSize = currentFontSize;
            }

            yield return null;
        }

        // 페이드 인이 끝난 후, 투명도와 크기를 목표 값으로 설정합니다.
        Color finalColor = originalColors[0];
        finalColor.a = targetAlpha;
        graphic.color = finalColor;

        if (originalScales != null)
        {
            Vector3 finalScale = startScale * scaleMultiplier;
            graphic.rectTransform.localScale = finalScale;
        }

        // Set the final font size for TMP_Text component
        if (graphic is TMP_Text finalTextComponent)
        {
            finalTextComponent.fontSize = targetFontSize;
        }
    }
}