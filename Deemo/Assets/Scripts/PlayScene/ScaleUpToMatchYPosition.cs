using UnityEngine;

public class ScaleUpToMatchYPosition : MonoBehaviour
{
    public GameObject targetObject; // ���ϴ� ������Ʈ
    public float scaleIncreaseRate = 0.1f; // ������ ���� �ӵ�
    public float targetScaleMultiplier = 2.0f; // ��ǥ ������ ����

    private Vector3 initialScale; // �ʱ� ������
    private float targetYPosition; // ��ǥ y ��ǥ
    private Vector3 targetScale; // ��ǥ ������

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

        // ������ ���� (y ��ǥ�� ��ǥ�� ������ ���Ŀ��� ����)
        if (transform.position.y >= targetYPosition && transform.localScale.magnitude < targetScale.magnitude)
        {
            float scaleFactor = 1 + (scaleIncreaseRate * Time.deltaTime);
            transform.localScale *= scaleFactor;
        }
    }
}
