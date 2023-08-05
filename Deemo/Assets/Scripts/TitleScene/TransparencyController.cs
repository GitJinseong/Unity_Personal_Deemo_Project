using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransparencyController
{
    public static IEnumerator BeginLateFadeOutImage(Image image, float lateDuration, float fadeDuration)
    {
        yield return new WaitForSeconds(lateDuration);

        float targetOpacity = 0f; // 목표 투명도 (완전 투명)
        float startOpacity = image.color.a; // 시작 투명도

        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            float elapsedTime = Time.time - startTime;
            float percentage = elapsedTime / fadeDuration;
            float currentOpacity = Mathf.Lerp(startOpacity, targetOpacity, percentage);

            Color newColor = image.color;
            newColor.a = currentOpacity;
            image.color = newColor;

            yield return null;
        }

        // 투명도가 완전 투명이 되도록 설정
        Color finalColor = image.color;
        finalColor.a = targetOpacity;
        image.color = finalColor;
    }

    public static IEnumerator FadeOutImage(Image image, float fadeDuration)
    {
        float targetOpacity = 0f; // 목표 투명도 (완전 투명)
        float startOpacity = image.color.a; // 시작 투명도

        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            float elapsedTime = Time.time - startTime;
            float percentage = elapsedTime / fadeDuration;
            float currentOpacity = Mathf.Lerp(startOpacity, targetOpacity, percentage);

            Color newColor = image.color;
            newColor.a = currentOpacity;
            image.color = newColor;

            yield return null;
        }

        // 투명도가 완전 투명이 되도록 설정
        Color finalColor = image.color;
        finalColor.a = targetOpacity;
        image.color = finalColor;
    }
}
