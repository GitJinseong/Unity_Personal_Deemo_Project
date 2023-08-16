using UnityEngine;

public class Choi_NoteMoveDown : MonoBehaviour
{
    public float moveSpeed = 2.0f; // 이동 속도

    private void Update()
    {
        // 물체를 아래로 이동시킴
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
