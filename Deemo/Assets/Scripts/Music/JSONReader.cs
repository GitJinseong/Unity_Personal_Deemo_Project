using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

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
        public float speed;
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
        float speed = jsonData.speed;
        List<NoteData> notes = jsonData.notes;

        Debug.Log($"Music Speed: {speed}");

        foreach (NoteData note in notes)
        {
            int noteId = note.noteId;
            int noteType = note.type;
            List<SoundData> sounds = note.sounds; // note�� sounds ����Ʈ�� ����

            float pos = note.pos;
            float size = note.size;
            float _time = note._time;
            float shift = note.shift;
            float time = note.time;

            // ��Ʈ ���� ���
            Debug.Log($"Note ID: {noteId} Note Type: {noteType} Sounds : {sounds.Count} Position: {pos}" +
                $"Position: {pos} Size: {size} Time: {_time} Shift: {shift} Time: {time}");

            // �� ��Ʈ�� ���� ������ ó��
            foreach (SoundData sound in sounds)
            {
                float delay = sound.d;
                int pitch = sound.p;
                int volume = sound.v;

                // ���� ���� ���
                Debug.Log($"Sound Delay: {delay} Sound Pitch: {pitch} Sound Volume: {volume}");
            }
        }

        // �߰� ������ ó�� ����

        // ����Ƽ ������Ʈ ����, �ִϸ��̼� ���� �۾� ����
    }
}
