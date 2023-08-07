using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInSettingObjects : MonoBehaviour
{
    public float fadeDuration = 1.0f; // ���̵� �ο� �ɸ��� �ð� (��)
    public float targetAlpha = 1.0f; // ���̵� ���� �Ϸ�� ���� ���İ� (1�� �������ϰ�)
    public float scaleMultiplier = 1.1f; // ũ�� ��ȯ ���� ����
    private float fadeDelay = 0.2f; // ���̵� ���� �����ϱ��� ���

    private float currentTime = 0f; // ���� ��� �ð�

    private Image[] childImages; // �ڽ� �̹��� ������Ʈ �迭
    private TMP_Text[] childTexts; // �ڽ� TMP �ؽ�Ʈ ������Ʈ �迭

    private Color[] originalImageColors; // ���� �̹��� ���� �迭
    private Color[] originalTextColors; // ���� TMP �ؽ�Ʈ ���� �迭

    private Vector3[] originalScales; // ���� ũ�� �迭

    // ���̵� �� ȿ���� �����ϴ� �Լ�
    public void StartFadeIn()
    {
        currentTime = fadeDuration; // ���̵� �� ���۽� ���� �ð��� �ִ밪���� �����մϴ�.
        gameObject.SetActive(true); // ��ũ��Ʈ�� Ȱ��ȭ�� ������Ʈ�� Ȱ��ȭ�մϴ�.

        // �̹��� ������Ʈ�� ã�Ƽ� ���̵� �� ȿ���� �����մϴ�.
        childImages = GetComponentsInChildren<Image>();
        originalImageColors = new Color[childImages.Length];
        originalScales = new Vector3[childImages.Length];
        for (int i = 0; i < childImages.Length; i++)
        {
            originalImageColors[i] = childImages[i].color;
            originalScales[i] = childImages[i].rectTransform.localScale;
            StartFadeInImage(childImages[i]);
        }

        // �̹����� �ڽ� ������Ʈ���� TMP �ؽ�Ʈ ������Ʈ�� ã�Ƽ� ���̵� �� ȿ���� �����մϴ�.
        childTexts = GetComponentsInChildren<TMP_Text>();
        originalTextColors = new Color[childTexts.Length];
        for (int i = 0; i < childTexts.Length; i++)
        {
            originalTextColors[i] = childTexts[i].color;
            StartFadeInText(childTexts[i]);
        }
    }

    // �̹��� ���̵� �� ȿ���� �����մϴ�.
    private void StartFadeInImage(Image image)
    {
        StartCoroutine(FadeInCoroutine(image, originalImageColors, originalScales));
    }

    // TMP �ؽ�Ʈ ���̵� �� ȿ���� �����մϴ�.
    private void StartFadeInText(TMP_Text text)
    {
        StartCoroutine(FadeInCoroutine(text, originalTextColors, null));
    }

    // ���̵� �� �ڷ�ƾ �Լ�
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

        while (currentTime > 0f) // currentTime�� ���ҽ�Ű�� ���̵� �� ȿ���� �����մϴ�.
        {
            currentTime -= Time.deltaTime;
            float normalizedTime = currentTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(targetAlpha, startAlpha, normalizedTime); // startAlpha�� targetAlpha�� �ٲ��ݴϴ�.
            float currentScaleMultiplier = Mathf.Lerp(1f, scaleMultiplier, normalizedTime); // ũ�� ��ȯ�� ���� ���� ���

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

        // ���̵� ���� ���� ��, ������ ũ�⸦ ��ǥ ������ �����մϴ�.
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