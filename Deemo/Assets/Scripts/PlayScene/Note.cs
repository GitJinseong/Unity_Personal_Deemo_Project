using UnityEngine;

public class Note : MonoBehaviour
{
    public float moveSpeed = 2.0f; // ��Ʈ �̵� �ӵ�
    private Vector3 direction = Vector3.down; // ��Ʈ �̵� ����
    public bool isCollisionHandled = false; // �浹 ó�� ����

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

    // �浹 ó�� �� �̵�
    public void HandleCollisionAndMove(Vector3 newPosition)
    {
        if (!isCollisionHandled)
        {
            isCollisionHandled = true;
            gameObject.SetActive(false);
            MoveToPosition(newPosition);
        }
    }

    // �̵�
    public void MoveToPosition(Vector3 position)
    {
        transform.position = position;
    }
}
