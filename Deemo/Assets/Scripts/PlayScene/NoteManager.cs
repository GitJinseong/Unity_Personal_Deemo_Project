using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking.Types;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public GameObject notePrefab; // ��Ʈ ������
    public int initialPoolSize = 30; // �ʱ� ������Ʈ Ǯ ũ��
    private Vector3 originalScale = default;
    private List<GameObject> notePool = new List<GameObject>();


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
            noteComponent.noteId = id; // ��Ʈ ������Ʈ�� ID ���� �Ҵ�
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
