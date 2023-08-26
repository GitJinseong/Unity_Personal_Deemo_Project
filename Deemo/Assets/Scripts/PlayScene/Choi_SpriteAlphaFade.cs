using System.Collections;
using UnityEngine;

public class Choi_SpriteAlphaFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public void StartFadeOut(float duration, SpriteRenderer sprite)
    {
        spriteRenderer = sprite;
        StartCoroutine(FadeOut(duration));
    }

    private IEnumerator FadeOut(float duration)
    {
        float startAlpha = spriteRenderer.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);
            SetAlpha(alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetAlpha(0f);
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
