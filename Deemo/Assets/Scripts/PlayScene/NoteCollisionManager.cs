using System.Collections.Generic;
using UnityEngine;

public class NoteCollisionManager : MonoBehaviour
{
    public float collisionCheckDistance = 0.1f; // 충돌 체크 거리
    private List<GameObject> notePool = new List<GameObject>(); // 노트 오브젝트 리스트

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("감지");
        if (other.CompareTag("Note") && other.gameObject != gameObject)
        {
            // 비활성화되지 않았으면 노트를 비활성화하고 처리
            if (other.gameObject.activeSelf)
            {
                Debug.Log("충돌");

                // 노트1을 비활성화 처리
                other.gameObject.SetActive(false);

                // 중간값 계산
                float middleX = (transform.position.x + other.transform.position.x) / 2f;

                // 노트2의 위치 이동
                Note noteComponent2 = other.GetComponent<Note>();
                if (noteComponent2 != null)
                {
                    Vector3 newPosition = new Vector3(middleX, noteComponent2.transform.position.y, noteComponent2.transform.position.z);
                    noteComponent2.transform.position = newPosition;
                }
            }
        }
    }

    // NoteManager에서 노트 리스트를 설정하는 메서드
    public void SetNotePool(List<GameObject> pool)
    {
        notePool = pool;
    }
}
