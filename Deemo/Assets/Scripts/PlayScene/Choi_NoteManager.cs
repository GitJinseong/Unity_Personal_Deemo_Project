using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class Choi_NoteManager : MonoBehaviour
{
    public static Choi_NoteManager instance;
    public GameObject notePrefab;
    public Transform notes_1;
    public Transform notes_2;
    public Transform notes_3;
    public Transform notes_4;
    public Transform notes_5;
    private int initialPoolSize = 20;
    private Vector3 originalScale = default;
    private List<GameObject> notes_1_Pool = new List<GameObject>();
    private List<GameObject> notes_2_Pool = new List<GameObject>();
    private List<GameObject> notes_3_Pool = new List<GameObject>();
    private List<GameObject> notes_4_Pool = new List<GameObject>();
    private List<GameObject> notes_5_Pool = new List<GameObject>();

    public List<GameObject> leftNotes = new List<GameObject>();
    public List<GameObject> rightNotes = new List<GameObject>();


    public LayerMask noteLayerMask;
    public string stringPos;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject note = Instantiate(notePrefab);
            note.transform.SetParent(notes_1);
            note.SetActive(false);
            notes_1_Pool.Add(note);

            note = Instantiate(notePrefab);
            note.transform.SetParent(notes_2);
            note.SetActive(false);
            notes_2_Pool.Add(note);

            note = Instantiate(notePrefab);
            note.transform.SetParent(notes_3);
            note.SetActive(false);
            notes_3_Pool.Add(note);

            note = Instantiate(notePrefab);
            note.transform.SetParent(notes_4);
            note.SetActive(false);
            notes_4_Pool.Add(note);

            note = Instantiate(notePrefab);
            note.transform.SetParent(notes_5);
            note.SetActive(false);
            notes_5_Pool.Add(note);
        }
        originalScale = notes_1_Pool[0].transform.localScale;
    }


    public IEnumerator SpawnNote(int id, float time, Vector3 spawnPosition, float size, int noteLine)
    {
        Debug.Log($"ID는 {id}");
        yield return new WaitForSeconds(time);
        GameObject note = default;
        switch (noteLine)
        {
            case 1:
                note = GetPooledNote_1();
                break;
            case 2:
                note = GetPooledNote_2();
                break;
            case 3:
                note = GetPooledNote_3();
                break;
            case 4:
                note = GetPooledNote_4();
                break;
            case 5:
                note = GetPooledNote_5();
                break;
        }
        if (note != null)
        {
            note.transform.position = spawnPosition;
            if (size < 1.0f) { size = 1.0f; }
            if (size > 1.0f)
            {
                size = (((size - 1.0f) * 0.4f) + 1.0f);
            }

            Choi_Note noteComponent = note.GetComponent<Choi_Note>();
            Choi_NoteMovement noteMovement = note.GetComponent<Choi_NoteMovement>();
            SpriteRenderer spriteRenderer = note.GetComponent<SpriteRenderer>();
            noteComponent.noteId = id;
            noteComponent.time = time;
            noteComponent.stringJudge = "";
            Color startColor = spriteRenderer.color;
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, 255f);
            spriteRenderer.color = newColor;

            noteMovement.ResetAndMove();

            Physics.SyncTransforms();

            note.SetActive(true);
            note.transform.localScale = originalScale * size;

            // 왼쪽과 오른쪽 노트의 시작 위치 계산
            float leftBoundary = -3.5f + size;
            float rightBoundary = 3.5f - size;

            string stringPos = CalculateStringPosition(spawnPosition.x, leftBoundary, rightBoundary);

            if (spawnPosition.x > 0.0f)
            {
                rightNotes.Add(note);
                Debug.Log($"Note ID: {id} - Creating at Right Start with Size {size}");
            }
            else
            {
                leftNotes.Add(note);
                Debug.Log($"Note ID: {id} - Creating at Left Start with Size {size}");
            }
        }
    }

    //private string GetStringPos(Vector3 spawnPosition)
    //{
    //    //Debug.Log("aaa: " + spawnPosition.x);
    //    if (spawnPosition.x <= 0f)
    //    {
    //        stringPos = "left";
    //    }
    //    else if (spawnPosition.x >= 0f)
    //    {
    //        stringPos = "right";
    //    }
    //    return stringPos;
    //}

    private GameObject GetPooledNote_1()
    {
        foreach (GameObject note in notes_1_Pool)
        {
            if (!note.activeInHierarchy)
            {
                return note;
            }
        }
        return null;

    }

    private GameObject GetPooledNote_2()
    {
        foreach (GameObject note in notes_2_Pool)
        {
            if (!note.activeInHierarchy)
            {
                return note;
            }
        }
        return null;
    }

    private GameObject GetPooledNote_3()
    {
        foreach (GameObject note in notes_3_Pool)
        {
            if (!note.activeInHierarchy)
            {
                return note;
            }
        }
        return null;
    }

    private GameObject GetPooledNote_4()
    {
        foreach (GameObject note in notes_4_Pool)
        {
            if (!note.activeInHierarchy)
            {
                return note;
            }
        }
        return null;
    }

    private GameObject GetPooledNote_5()
    {
        foreach (GameObject note in notes_5_Pool)
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

    //public IEnumerator SpawnNoteWithDelay(float time, Vector3 spawnPosition, float size)
    //{
    //    yield return new WaitForSeconds(time + 0.15f);
    //    GameObject note = GetPooledNote();
    //    if (note != null)
    //    {
    //        note.transform.position = spawnPosition;
    //        note.transform.localScale = originalScale * size;
    //        note.SetActive(true);
    //        Physics.SyncTransforms();
    //    }
    //}

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
