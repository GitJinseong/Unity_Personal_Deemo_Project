using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking.Types;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public GameObject notePrefab; // 노트 프리팹
    public int initialPoolSize = 30; // 초기 오브젝트 풀 크기
    private Vector3 originalScale = default;
    private List<GameObject> notePool = new List<GameObject>();


    public LayerMask noteLayerMask; // Note의 레이어 마스크

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject note = Instantiate(notePrefab);
            note.SetActive(false);
            notePool.Add(note);
        }
        originalScale = notePool[0].transform.localScale;
    }

    public IEnumerator SpawnNote(int id, float time, Vector3 spawnPosition, float size)
    {
        yield return new WaitForSeconds(time);
        GameObject note = GetPooledNote();
        if (note != null)
        {
            note.transform.position = spawnPosition;
            note.transform.localScale = originalScale * size;
            note.SetActive(true);
            Note noteComponent = note.GetComponent<Note>();
            noteComponent.noteId = id; // 노트 오브젝트에 ID 값을 할당
            Physics.SyncTransforms(); // 이 부분 추가
            AdjustNotePosition(note);
        }
    }

    private GameObject GetPooledNote()
    {
        foreach (GameObject note in notePool)
        {
            if (!note.activeInHierarchy)
            {
                return note;
            }
        }
        return null;
    }

    private void AdjustNotePosition(GameObject note)
    {
        // 여기서 노트의 위치 조정 로직을 구현하세요.
    }

    public IEnumerator SpawnNoteWithDelay(float time, Vector3 spawnPosition, float size)
    {
        yield return new WaitForSeconds(time + 0.15f);
        GameObject note = GetPooledNote();
        if (note != null)
        {
            note.transform.position = spawnPosition;
            note.transform.localScale = originalScale * size;
            note.SetActive(true);
            Physics.SyncTransforms();
            AdjustNotePosition(note);
        }
    }

    public void DeactivateOverlappingNotes(Vector3 position, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, noteLayerMask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != null && collider.gameObject != gameObject)
            {
                Debug.Log("충돌제거");
                collider.gameObject.SetActive(false);
            }
        }
    }
}
