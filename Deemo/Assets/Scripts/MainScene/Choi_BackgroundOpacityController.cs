using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Choi_BackgroundOpacityController : MonoBehaviour
{
    public Image target = default; // 알파값을 변경할 대상 이미지
    private Color originalColor; // 원래 색상을 저장하기 위한 변수
    private Color targetColor; // 목표 색상을 저장하기 위한 변수
    private float duration = 0.5f; // 알파값이 변경되는데 걸리는 시간 (초 단위)
    private float startAlpha = 0f; // 시작 알파값
    private float targetAlpha = 120f; // 목표 알파값
    

    private void Start()
    {
        // 원래 색상을 저장합니다.
        originalColor = target.color;
        // 목표 색상을 설정합니다. (알파값이 120일 때)
        targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha / 255f);
    }

    // 이미지의 알파값을 변경하는 메서드
    public void ChangeOpacity()
    {
        // 코루틴을 시작합니다.
        StartCoroutine(ChangeOpacityCoroutine());
    }

    private IEnumerator ChangeOpacityCoroutine()
    {
        float time = 0f; // 경과 시간을 저장하기 위한 변수

        while (time < duration)
        {
            // 경과 시간을 증가시킵니다.
            time += Time.deltaTime;
            // 보간된 값이 0부터 1 사이가 되도록 클램핑합니다.
            float t = Mathf.Clamp01(time / duration);
            // 알파값을 시작 알파값과 목표 알파값 사이의 보간 비율로 설정합니다.
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            // 이미지의 색상을 보간값으로 변경하여 알파값을 조절합니다.
            target.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha / 255f);

            yield return null;
        }
    }
}
