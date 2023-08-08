using UnityEngine;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab; // 노트 프리팹
    public int initialPoolSize = 10; // 초기 오브젝트 풀 크기

    private List<GameObject> notePool = new List<GameObject>();

    private void Start()
    {
        // 초기 오브젝트 풀 생성
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject note = Instantiate(notePrefab);
            note.SetActive(false);
            notePool.Add(note);
        }
    }

    // 노트 생성 및 활성화
    public void SpawnNote(Vector3 spawnPosition)
    {
        GameObject note = GetPooledNote();
        if (note != null)
        {
            note.transform.position = spawnPosition;
            note.SetActive(true);
        }
    }

    // 오브젝트 풀에서 비활성화된 노트 오브젝트 반환
    private GameObject GetPooledNote()
    {
        foreach (GameObject note in notePool)
        {
            if (!note.activeInHierarchy)
            {
                return note;
            }
        }
        // 오브젝트 풀 크기 확장 또는 노트 생성 로직 추가 가능
        return null;
    }

    // 노트 재활용
    public void RecycleNote(GameObject note)
    {
        note.SetActive(false);
    }
}
