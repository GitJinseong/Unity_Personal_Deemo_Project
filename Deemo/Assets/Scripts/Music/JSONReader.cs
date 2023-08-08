using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour
{
    // JSON 파일 경로
    private string defaultDirectoryPath = "Assets/Resources/MusicJSONs/";
    public string jsonFilePath = "ad2discovered.easy.json.txt";

    [System.Serializable]
    public class SoundData
    {
        public int w;
        public float d;
        public int p;
        public int v;
    }

    [System.Serializable]
    public class NoteData
    {
        public int noteId;
        public int type;
        public List<SoundData> sounds;
        public float pos;
        public float size;
        public float _time;
        public float shift;
        public float time;
    }

    [System.Serializable]
    public class JSONData
    {
        public int speed;
        public List<NoteData> notes;
    }

    void Start()
    {
        // JSON 파일 찾기
        jsonFilePath = defaultDirectoryPath + jsonFilePath;

        // JSON 파일 읽기
        string jsonText = File.ReadAllText(jsonFilePath);
        JSONData jsonData = JsonUtility.FromJson<JSONData>(jsonText);

        // 데이터 처리
        int speed = jsonData.speed;
        List<NoteData> notes = jsonData.notes;

        // 각 노트 데이터 처리
        foreach (NoteData note in notes)
        {
            int noteId = note.noteId;
            int noteType = note.type;
            List<SoundData> sounds = note.sounds;
            float pos = note.pos;
            float size = note.size;
            float _time = note._time;
            float shift = note.shift;
            float time = note.time;

            // 노트 정보 출력
            Debug.Log($"Note ID: {noteId}");
            Debug.Log($"Note Type: {noteType}");
            Debug.Log($"Sounds Count: {sounds.Count}");
            Debug.Log($"Position: {pos}");
            Debug.Log($"Size: {size}");
            Debug.Log($"Time: {_time}");
            Debug.Log($"Shift: {shift}");
            Debug.Log($"Time: {time}");
            Debug.Log("=====================");
        }

        // 추가 데이터 처리 가능

        // 유니티 오브젝트 생성, 애니메이션 등의 작업 가능
    }
}
