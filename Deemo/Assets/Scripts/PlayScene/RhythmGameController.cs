using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameController : MonoBehaviour
{
    public Transform hitMarker; // 입력을 표시하는 마커

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 예시로 스페이스바를 입력 키로 사용
        {
            EvaluateNote(); // 노트 판정 메서드 호출
        }
    }

    private void EvaluateNote()
    {
        // 현재 화면상의 모든 노트들을 가져온다.
        Note[] notes = FindObjectsOfType<Note>();

        foreach (Note note in notes)
        {
            Debug.Log(note.gameObject.transform.position.y);
            // 거리 계산
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
        // 노트와 마커 간의 거리를 계산하는 로직
        return Vector2.Distance(hitMarker.position, note.transform.position);
    }
}
