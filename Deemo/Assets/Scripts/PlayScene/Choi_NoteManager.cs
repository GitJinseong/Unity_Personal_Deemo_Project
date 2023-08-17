using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class Choi_NoteManager : MonoBehaviour
{
    public static Choi_NoteManager instance;
    public GameObject notePrefab;
    public Transform noteParent;
    public int initialPoolSize = 100;
    private Vector3 originalScale = default;
    private List<GameObject> notePool = new List<GameObject>();

    public LayerMask noteLayerMask;
    public string stringPos;

    // 새로 추가한 변수
    private float originalGravityScale = 0f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject note = Instantiate(notePrefab);
            note.transform.SetParent(noteParent);
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

            Choi_Note noteComponent = note.GetComponent<Choi_Note>();
            Choi_NoteMovement noteMovement = note.GetComponent<Choi_NoteMovement>();
            noteComponent.noteId = id;
            noteComponent.time = time;

            noteComponent.stringPos = GetStringPos(spawnPosition);

            Rigidbody noteRigidbody = note.GetComponent<Rigidbody>();

            // 이 부분에서 stringPos 값을 노트 컴포넌트에 설정
            noteComponent.stringPos = stringPos;

            noteMovement.ResetAndMove();

            Physics.SyncTransforms();
        }
    }

    private string GetStringPos(Vector3 spawnPosition)
    {
        Debug.Log("aaa: " + spawnPosition.x);
        if (spawnPosition.x <= 0f)
        {
            stringPos = "left";
        }
        else if (spawnPosition.x >= 0f)
        {
            stringPos = "right";
        }
        return stringPos;
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

    public string CalculateStringPosition(float adjustedX, float leftBoundary, float rightBoundary)
    {
        if (adjustedX < leftBoundary)
        {
            return "left";
        }
        else if (adjustedX > rightBoundary)
        {
            return "right";
        }
        return "";
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
