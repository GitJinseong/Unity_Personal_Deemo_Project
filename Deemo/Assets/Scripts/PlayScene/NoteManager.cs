using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public GameObject notePrefab; // ��Ʈ ������
    public GameObject slideNotePrefab; // �����̵� ��Ʈ ������
    public int initialPoolSize = 30; // �ʱ� ������Ʈ Ǯ ũ��
    private Vector3 originalScale = default;
    private Vector3 originalScale_Slide = default;
    private List<GameObject> notePool = new List<GameObject>();
    private List<GameObject> slideNotePool = new List<GameObject>();


    public LayerMask noteLayerMask; // Note�� ���̾� ����ũ

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject note = Instantiate(notePrefab);
            GameObject slideNote = Instantiate(slideNotePrefab);
            note.SetActive(false);
            slideNote.SetActive(false);
            notePool.Add(note);
            slideNotePool.Add(note);
        }
        originalScale = notePool[0].transform.localScale;
        originalScale_Slide = notePool[0].transform.localScale;
    }

    public IEnumerator SpawnNote(float time, Vector3 spawnPosition, float size)
    {
        yield return new WaitForSeconds(time);
        GameObject note = GetPooledNote();
        if (note != null)
        {
            note.transform.position = spawnPosition;
            note.transform.localScale = originalScale * size;
            note.SetActive(true);
            Physics.SyncTransforms(); // �� �κ� �߰�
            AdjustNotePosition(note);
        }
    }

    public IEnumerator SpawnSlideNote(float time, Vector3 spawnPosition, float size)
    {
        yield return new WaitForSeconds(time);
        GameObject note = GetPooledSlideNote();
        if (note != null)
        {
            note.transform.position = spawnPosition;
            note.transform.localScale = originalScale * size;
            note.SetActive(true);
            Physics.SyncTransforms(); // �� �κ� �߰�
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

    private GameObject GetPooledSlideNote()
    {
        foreach (GameObject note in slideNotePool)
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
        // ���⼭ ��Ʈ�� ��ġ ���� ������ �����ϼ���.
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
                Debug.Log("�浹����");
                collider.gameObject.SetActive(false);
            }
        }
    }
}
