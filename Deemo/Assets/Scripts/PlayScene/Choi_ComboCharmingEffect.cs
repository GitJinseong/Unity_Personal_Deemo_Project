
using UnityEngine;

public class Choi_ComboCharmingEffect : MonoBehaviour
{
    SpriteRenderer sprite;
    public float originalScale = 0.8f;
    public float originalAlpha = 1.0f; // 1.0f는 완전 불투명, 0.0f는 완전 투명
    public float finalScale = 1.4f;
    public float finalAlpha = 0.0f;
    public float duration = 0.3f; // 애니메이션 지속 시간

    private float currentTime = 0f; // 현재 경과 시간

    private void OnEnable()
    {
        currentTime = 0f;
        transform.localScale = new Vector3(transform.localScale.x, originalScale, 1f);
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 시간 경과 업데이트
        currentTime += Time.deltaTime;

        // 보간 비율 계산
        float t = Mathf.Clamp01(currentTime / duration);

        // 보간된 스케일 값 계산
        float scaledValue = Mathf.Lerp(originalScale, finalScale, t);

        // 보간된 알파 값 계산
        float alphaValue = Mathf.Lerp(originalAlpha, finalAlpha, t);

        // 스케일 & 알파 값 적용
        transform.localScale = new Vector3(transform.localScale.x, scaledValue, 1f);
        Color newColor = sprite.color;
        newColor.a = alphaValue;
        sprite.color = newColor;

        // 애니메이션 종료 체크
        if (currentTime >= duration)
        {
            // 애니메이션 종료 시 처리 (예: 오브젝트 제거 등)
            gameObject.SetActive(false);
        }
    }
}
