using UnityEngine;

public class ScaleUpToMatchYPosition : MonoBehaviour
{
    public GameObject targetObject; // 원하는 오브젝트
    public float scaleIncreaseRate = 0.1f; // 스케일 증가 속도
    public float targetScaleMultiplier = 2.0f; // 목표 스케일 배율

    private Vector3 initialScale; // 초기 스케일
    private float targetYPosition; // 목표 y 좌표
    private Vector3 targetScale; // 목표 스케일

    private void Start()
    {
        initialScale = transform.localScale;
        targetYPosition = targetObject.transform.position.y;
        targetScale = initialScale * targetScaleMultiplier;
    }

    private void Update()
    {
        if (transform.position.y < targetYPosition)
        {
            float newYPosition = Mathf.Lerp(transform.position.y, targetYPosition, scaleIncreaseRate * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }

        // 스케일 증가 (y 좌표가 목표에 도달한 이후에만 적용)
        if (transform.position.y >= targetYPosition && transform.localScale.magnitude < targetScale.magnitude)
        {
            float scaleFactor = 1 + (scaleIncreaseRate * Time.deltaTime);
            transform.localScale *= scaleFactor;
        }
    }
}
