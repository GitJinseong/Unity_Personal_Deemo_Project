using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Choi_TitleScene_BackgroundLightController : MonoBehaviour
{
    public float delay = 0.1f; // 딜레이
    public int blinkInterval = 5; // 투명도 조정 간격
    public float stopTime = 3f; // 투명도가 1이 될 때 정지할 시간
    public float startDelay = 4f; // 최초 시작 딜레이

    private Image image;
    private float currentOpacity = 1f; // 현재 투명도 (1은 완전 불투명, 0은 완전 투명)
    private bool increasingOpacity = true; // 투명도 증가 여부
    private bool isWaiting = false; // 대기 상태 여부

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(StartAfterDelay());
    }

    // 최초 시작을 4초 후로 늦추는 코루틴
    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(BlinkCoroutine());
    }

    // 투명도 조절 함수
    private void SetTransparency()
    {
        if (isWaiting) return;

        // 투명도 조정
        currentOpacity += (increasingOpacity ? blinkInterval : -blinkInterval) * 0.01f;
        currentOpacity = Mathf.Clamp01(currentOpacity);

        // 이미지의 색상 변경으로 투명도 조절
        image.color = new Color(image.color.r, image.color.g, image.color.b, currentOpacity);

        // 투명도가 0 또는 1이면 정지 코루틴 시작
        if (currentOpacity == 1f || currentOpacity == 0f)
        {
            StartCoroutine(WaitAndStartBlinking());
        }
    }

    // 투명도 변화를 주기적으로 실행하는 코루틴
    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SetTransparency();
        }
    }

    // 정지 시간만큼 대기하는 코루틴
    private IEnumerator WaitAndStartBlinking()
    {
        isWaiting = true; // 대기 상태로 변경
        yield return new WaitForSeconds(stopTime);
        isWaiting = false; // 대기 상태 해제
        increasingOpacity = !increasingOpacity; // 투명도 변화 방향 변경
    }
}