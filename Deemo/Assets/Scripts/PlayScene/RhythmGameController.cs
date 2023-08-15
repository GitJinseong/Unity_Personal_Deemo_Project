using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameController : MonoBehaviour
{
    public Transform hitMarker; // �Է��� ǥ���ϴ� ��Ŀ

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ���÷� �����̽��ٸ� �Է� Ű�� ���
        {
            EvaluateNote(); // ��Ʈ ���� �޼��� ȣ��
        }
    }

    private void EvaluateNote()
    {
        // ���� ȭ����� ��� ��Ʈ���� �����´�.
        Note[] notes = FindObjectsOfType<Note>();

        foreach (Note note in notes)
        {
            Debug.Log(note.gameObject.transform.position.y);
            // �Ÿ� ���
            float distance = CalculateNoteDistance(note);

            if (distance <= 0.5f)
            {
                Debug.Log("PERFECT!");
                Destroy(note.gameObject);
            }
            else if (distance <= 1.0f)
            {
                Debug.Log("NORMAL!");
                Destroy(note.gameObject);
            }
            else
            {
                Debug.Log("MISS!");
            }
        }
    }

    private float CalculateNoteDistance(Note note)
    {
        // ��Ʈ�� ��Ŀ ���� �Ÿ��� ����ϴ� ����
        return Vector2.Distance(hitMarker.position, note.transform.position);
    }
}
