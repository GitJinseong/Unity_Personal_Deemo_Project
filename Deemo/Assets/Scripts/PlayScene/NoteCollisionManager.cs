using System.Collections.Generic;
using UnityEngine;

public class NoteCollisionManager : MonoBehaviour
{
    public float collisionCheckDistance = 0.1f; // �浹 üũ �Ÿ�
    private List<GameObject> notePool = new List<GameObject>(); // ��Ʈ ������Ʈ ����Ʈ

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("����");
        if (other.CompareTag("Note") && other.gameObject != gameObject)
        {
            // ��Ȱ��ȭ���� �ʾ����� ��Ʈ�� ��Ȱ��ȭ�ϰ� ó��
            if (other.gameObject.activeSelf)
            {
                Debug.Log("�浹");

                // ��Ʈ1�� ��Ȱ��ȭ ó��
                other.gameObject.SetActive(false);

                // �߰��� ���
                float middleX = (transform.position.x + other.transform.position.x) / 2f;

                // ��Ʈ2�� ��ġ �̵�
                Note noteComponent2 = other.GetComponent<Note>();
                if (noteComponent2 != null)
                {
                    Vector3 newPosition = new Vector3(middleX, noteComponent2.transform.position.y, noteComponent2.transform.position.z);
                    noteComponent2.transform.position = newPosition;
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
