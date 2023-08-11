using UnityEngine;

public class ScaleUpToMatchYPosition : MonoBehaviour
{
    public GameObject targetObject; // 원하는 오브젝트
    public float scaleIncreaseRate = 0.1f; // 스케일 증가 속도
    public float targetScaleMultiplier = 2.0f; // 목표 스케일 배율
    public float moveSpeed = 1.0f; // 이동 속도

    private Vector3 initialScale; // 초기 스케일
    private float targetYPosition; // 목표 y 좌표
    private Vector3 targetScale; // 목표 스케일
    private float elapsedTime = 0f; // 경과 시간
    private bool shouldGrow = false; // 커질지 여부
    private bool scaleReverted = false; // 스케일 복원 여부

    private void Start()
    {
        initialScale = transform.localScale;
        transform.localScale = initialScale * 2.5f; // 처음 시작할 때 스케일 2.5로 설정
        targetYPosition = targetObject.transform.position.y;
        targetScale = initialScale * targetScaleMultiplier;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!scaleReverted && elapsedTime >= 0.1f)
        {
            transform.localScale = initialScale; // 0.1초 후에 원래대로 스케일 복원
            scaleReverted = true;
        }

        if (transform.position.y < targetYPosition)
        {
            float newYPosition = Mathf.Lerp(transform.position.y, targetYPosition, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }
        else
        {
            shouldGrow = true;
        }

        if (shouldGrow && transform.localScale.magnitude < targetScale.magnitude)
        {
            float scaleFactor = 1 + (scaleIncreaseRate * Time.deltaTime);
            transform.localScale *= scaleFactor;
        }
    }
}
