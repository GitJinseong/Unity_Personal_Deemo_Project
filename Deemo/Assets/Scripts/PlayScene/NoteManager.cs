using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public GameObject notePrefab; // ��Ʈ ������
    public int initialPoolSize = 10; // �ʱ� ������Ʈ Ǯ ũ��
    public float spacing = 1.0f; // ������Ʈ ����

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

    public IEnumerator SpawnNote(float time, Vector3 spawnPosition, float size)
    {
        yield return new WaitForSeconds(time);
        GameObject note = GetPooledNote();
        if (note != null)
        {
            note.transform.position = spawnPosition;
            note.transform.localScale = originalScale * size;
            note.SetActive(true);
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

    //private void AdjustNotePosition(GameObject note)
    //{
    //    Collider[] colliders = Physics.OverlapSphere(note.transform.position, spacing, noteLayerMask);

    //    foreach (Collider collider in colliders)
    //    {
    //        Debug.Log("�浹");
    //        if (collider.gameObject != note)
    //        {
    //            Vector3 newPos = note.transform.position;
    //            newPos.x = collider.bounds.max.x + spacing;
    //            note.transform.position = newPos;
    //        }
    //    }
    //}

    private void AdjustNotePosition(GameObject note)
    {
        float increasedRadius = spacing * 2.0f; // �ݰ� ���� �� ��� �ø���

        Collider[] colliders = Physics.OverlapSphere(note.transform.position, increasedRadius, noteLayerMask);

        foreach (Collider collider in colliders)
        {
            Debug.Log("�浹");
            if (collider.gameObject != note)
            {
                Vector3 newPos = note.transform.position;
                newPos.x = collider.bounds.max.x + spacing;
                note.transform.position = newPos;
            }
        }
    }

}
