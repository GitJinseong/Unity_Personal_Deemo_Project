using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour
{
    // JSON ���� ���
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
        // JSON ���� ã��
        jsonFilePath = defaultDirectoryPath + jsonFilePath;

        // JSON ���� �б�
        string jsonText = File.ReadAllText(jsonFilePath);
        JSONData jsonData = JsonUtility.FromJson<JSONData>(jsonText);

        // ������ ó��
        int speed = jsonData.speed;
        List<NoteData> notes = jsonData.notes;

        // �� ��Ʈ ������ ó��
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

            // ��Ʈ ���� ���
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

        // �߰� ������ ó�� ����

        // ����Ƽ ������Ʈ ����, �ִϸ��̼� ���� �۾� ����
    }
}
