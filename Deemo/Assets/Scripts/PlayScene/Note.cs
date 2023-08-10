using UnityEngine;

public class Note : MonoBehaviour
{
    public float moveSpeed = 2.0f; // 노트 이동 속도
    private Vector3 direction = Vector3.down; // 노트 이동 방향
    public bool isCollisionHandled = false; // 충돌 처리 여부

    private void Update()
    {
        // 노트를 아래 방향으로 이동
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // 노트 이동 방향 변경
    public void ChangeDirection()
    {
        direction = -direction;
    }

    // 충돌 처리 및 이동
    public void HandleCollisionAndMove(Vector3 newPosition)
    {
        if (!isCollisionHandled)
        {
            isCollisionHandled = true;
            gameObject.SetActive(false);
            MoveToPosition(newPosition);
        }
    }

    // 이동
    public void MoveToPosition(Vector3 position)
    {
        transform.position = position;
    }
}
