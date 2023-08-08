using UnityEngine;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab; // ��Ʈ ������
    public int initialPoolSize = 10; // �ʱ� ������Ʈ Ǯ ũ��

    private List<GameObject> notePool = new List<GameObject>();

    private void Start()
    {
        // �ʱ� ������Ʈ Ǯ ����
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject note = Instantiate(notePrefab);
            note.SetActive(false);
            notePool.Add(note);
        }
    }

    // ��Ʈ ���� �� Ȱ��ȭ
    public void SpawnNote(Vector3 spawnPosition)
    {
        GameObject note = GetPooledNote();
        if (note != null)
        {
            note.transform.position = spawnPosition;
            note.SetActive(true);
        }
    }

    // ������Ʈ Ǯ���� ��Ȱ��ȭ�� ��Ʈ ������Ʈ ��ȯ
    private GameObject GetPooledNote()
    {
        foreach (GameObject note in notePool)
        {
            if (!note.activeInHierarchy)
            {
                return note;
            }
        }
        // ������Ʈ Ǯ ũ�� Ȯ�� �Ǵ� ��Ʈ ���� ���� �߰� ����
        return null;
    }

    // ��Ʈ ��Ȱ��
    public void RecycleNote(GameObject note)
    {
        note.SetActive(false);
    }
}
