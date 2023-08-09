using UnityEngine;

public class Note : MonoBehaviour
{
    public float moveSpeed = 2.0f; // ��Ʈ �̵� �ӵ�
    private Vector3 direction = Vector3.down; // ��Ʈ �̵� ����

    private void Update()
    {
        // ��Ʈ�� �Ʒ� �������� �̵�
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // ��Ʈ �̵� ���� ����
    public void ChangeDirection()
    {
        direction = -direction;
    }
}
