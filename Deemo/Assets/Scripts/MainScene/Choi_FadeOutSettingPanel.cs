using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Choi_FadeOutSettingPanel : MonoBehaviour
{
    public Image img_Panel; // 페이드 아웃할 첫 번째 이미지
    public Image img_Bg; // 페이드 아웃할 두 번째 이미지

    private bool isFading = false; // 페이드 아웃 중인지 여부를 저장하는 변수
    public float fadeDuration = 1.0f;
    private float fadeDelay = 0.0f;

    // 이미지 페이드 아웃 효과를 시작하는 함수
    public void StartFadeOut()
    {
        if (isFading)
            return;

        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        isFading = true;
        img_Panel.raycastTarget = false; // 이미지를 클릭하지 못하도록 Raycast Target을 비활성화
        img_Bg.raycastTarget = false;

        float currentTime = 0f;
        float startAlpha1 = img_Panel.color.a;
        float startAlpha2 = img_Bg.color.a;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float normalizedTime = currentTime / 1f;
            float targetAlpha = Mathf.Lerp(startAlpha1, 0f, normalizedTime);

            Color color1 = img_Panel.color;
            color1.a = targetAlpha;
            img_Panel.color = color1;

            Color color2 = img_Bg.color;
            color2.a = targetAlpha;
            img_Bg.color = color2;

            yield return null;
        }

        // 페이드 아웃이 끝난 후, 투명도를 0으로 설정합니다.
        Color finalColor1 = img_Panel.color;
        finalColor1.a = 0f;
        img_Panel.color = finalColor1;

        Color finalColor2 = img_Bg.color;
        finalColor2.a = 0f;
        img_Bg.color = finalColor2;

        img_Panel.raycastTarget = true; // 이미지의 페이드 아웃이 끝나면 다시 클릭 가능하도록 Raycast Target 활성화
        img_Bg.raycastTarget = true;

        isFading = false; // 페이드 아웃이 끝났으므로 isFading을 false로 설정합니다.
    }
}
