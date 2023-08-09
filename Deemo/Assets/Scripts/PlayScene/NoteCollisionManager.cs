using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteCollisionManager : MonoBehaviour
{
    public float collisionCheckDistance = 0.1f; // 충돌 체크 거리
    private List<GameObject> notePool = new List<GameObject>();  // 노트 오브젝트 리스트

    private void Update()
    {
        // 각 노트들 간에 충돌을 체크하고 겹침을 방지
        for (int i = 0; i < notePool.Count; i++)
        {
            for (int j = i + 1; j < notePool.Count; j++)
            {
                GameObject note1 = notePool[i];
                GameObject note2 = notePool[j];

                if (Vector3.Distance(note1.transform.position, note2.transform.position) < collisionCheckDistance)
                {
                    // 노트들이 충돌하는 경우 겹치지 않도록 처리 (예: 이동 방향 변경)
                    Note noteComponent1 = note1.GetComponent<Note>();
                    Note noteComponent2 = note2.GetComponent<Note>();

                    if (noteComponent1 != null && noteComponent2 != null)
                    {
                        noteComponent1.ChangeDirection();
                        noteComponent2.ChangeDirection();
                    }
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
