using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Choi_TouchToStartController : MonoBehaviour
{
    public float delay = 0.1f; // 딜레이
    public int blinkInterval = 10;  // 투명도 조정 간격
    public float stopTime = 2f; // 투명도가 1이 될 때 정지할 시간

    private Image image;
    private float currentOpacity = 1f; // 현재 투명도 (1은 완전 불투명, 0은 완전 투명)
    private bool increasingOpacity = true; // 투명도 증가 여부
    private bool isWaiting = false; // 대기 상태 여부
    private bool isBlinkingInReverse = false; // 투명도 반전 여부

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(BlinkCoroutine());
    }

    // 투명도 조절 함수
    public void SetTransparency()
    {
        if (isWaiting) return;

        if (increasingOpacity)
        {
            currentOpacity += blinkInterval * 0.01f; // blinkInterval 값을 0.01로 나누어서 증감 값을 조절
            currentOpacity = Mathf.Clamp01(currentOpacity); // 0과 1 사이로 값을 제한
            if (currentOpacity >= 1f && !isWaiting)
            {
                increasingOpacity = false;
                if (isBlinkingInReverse)
                {
                    StartCoroutine(WaitAndStartBlinking());
                    isBlinkingInReverse = false;
                }
            }
        }
        else
        {
            currentOpacity -= blinkInterval * 0.01f; // blinkInterval 값을 0.01로 나누어서 증감 값을 조절
            currentOpacity = Mathf.Clamp01(currentOpacity); // 0과 1 사이로 값을 제한
            if (currentOpacity <= 0f)
            {
                increasingOpacity = true;
                isBlinkingInReverse = true;
            }
        }
        Color currentColor = image.color;
        currentColor.a = currentOpacity; // alpha 값만 변경
        image.color = currentColor;
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SetTransparency();
        }
    }

    private IEnumerator WaitAndStartBlinking()
    {
        isWaiting = true; // 대기 상태로 변경
        yield return new WaitForSeconds(stopTime); // 2초 대기
        isWaiting = false; // 대기 상태 해제
        increasingOpacity = true; // 투명도 변화 재개
    }
}