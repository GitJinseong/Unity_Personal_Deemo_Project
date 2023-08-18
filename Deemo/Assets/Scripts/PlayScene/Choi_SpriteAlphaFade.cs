using UnityEngine;

using System.Collections;
using UnityEngine;

public class Choi_SpriteAlphaFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 0.3f;
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / duration);
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            spriteRenderer.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final alpha value is exactly 0
        Color finalColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        spriteRenderer.color = finalColor;
    }
}