using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteCollisionManager : MonoBehaviour
{
    public float collisionCheckDistance = 0.1f; // �浹 üũ �Ÿ�
    private List<GameObject> notePool = new List<GameObject>();  // ��Ʈ ������Ʈ ����Ʈ

    private void Update()
    {
        // �� ��Ʈ�� ���� �浹�� üũ�ϰ� ��ħ�� ����
        for (int i = 0; i < notePool.Count; i++)
        {
            for (int j = i + 1; j < notePool.Count; j++)
            {
                GameObject note1 = notePool[i];
                GameObject note2 = notePool[j];

                if (Vector3.Distance(note1.transform.position, note2.transform.position) < collisionCheckDistance)
                {
                    // ��Ʈ���� �浹�ϴ� ��� ��ġ�� �ʵ��� ó�� (��: �̵� ���� ����)
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

    // NoteManager���� ��Ʈ ����Ʈ�� �����ϴ� �޼���
    public void SetNotePool(List<GameObject> pool)
    {
        notePool = pool;
    }
}
