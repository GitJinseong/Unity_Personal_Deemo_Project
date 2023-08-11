using UnityEngine;

public class NoteCollision : MonoBehaviour
{
    public delegate void NoteCollisionHandler(GameObject noteObject);
    public static event NoteCollisionHandler OnNoteCollision;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note") && other.gameObject != gameObject)
        {
            Note otherNote = other.GetComponent<Note>();
            Note myNote = GetComponent<Note>();

            if (otherNote != null && myNote != null)
            {
                // 비활성화되지 않았으면 노트를 비활성화 처리
                if (other.gameObject.activeSelf)
                {
                    // 내 noteId와 충돌한 노트의 noteId 비교
                    if (otherNote.noteId > myNote.noteId)
                    {
                        // 노트를 비활성화 처리
                        other.gameObject.SetActive(false);

                        Debug.Log($"비활성화 ID: {myNote.noteId}");

                        // 중복되는 다른 노트에 대한 이벤트 호출
                        OnNoteCollision?.Invoke(other.gameObject);
                    }
                }
            }
        }
    }
}
