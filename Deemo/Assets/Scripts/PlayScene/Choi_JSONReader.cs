using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;

public class Choi_JSONReader : MonoBehaviour
{
    public string jsonFileName = "leviathan.easy";
    public const float DEFAULT_POS_Y = 13f;

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
        string jsonText = "";
        string jsonFilePath = $"MusicJSONs/{jsonFileName}";

        TextAsset jsonAsset = Resources.Load<TextAsset>(jsonFilePath);

        if (jsonAsset != null)
        {
            jsonText = jsonAsset.text;
            // 이후 처리
        }
        else
        {
            Debug.LogError($"JSON file not found at path: {jsonFilePath}");
        }

        JSONData jsonData = JsonUtility.FromJson<JSONData>(jsonText);

        float speed = jsonData.speed;
        List<NoteData> notes = jsonData.notes;

        Debug.Log($"Music Speed: {speed}");

        int skippedNoteCount = 0;

        foreach (NoteData note in notes.OrderBy(n => n._time))
        {
            int noteId = note.noteId;
            int noteType = note.type;
            List<SoundData> sounds = note.sounds;
            float pos = note.pos;
            float size = note.size;
            float _time = note._time;
            int pitch = default;

            Debug.Log($"Time: {_time}");
            // 중앙을 기준으로 노트 생성
            Vector3 notePos = new Vector3(pos, DEFAULT_POS_Y);

            if (sounds.Count > 0)
            {
                pitch = sounds[0].p;
                float pitchPercentage = (float)pitch / 127f;
                float xPos = Mathf.Lerp(-6f, 6f, pitchPercentage);
                notePos = new Vector3(xPos, DEFAULT_POS_Y);
            }
            else
            {
                if (pos <= -6f)
                {
                    notePos = new Vector3(-6f, DEFAULT_POS_Y);
                }
                else if (pos >= 6f)
                {
                    notePos = new Vector3(6f, DEFAULT_POS_Y);
                }
            }


            var sameTimeNotes = notes.Where(n => Mathf.Abs(n._time - _time) < 0.01f).ToList();

            if (sameTimeNotes.Count >= 2)
            {
                bool shouldSkipNote = ShouldSkipNoteCreation(note, sameTimeNotes);

                if (!shouldSkipNote)
                {
                    CreateNotesAtStarts(sameTimeNotes, noteId);
                }
                else
                {
                    Debug.Log($"Note ID: {noteId} - Skipping creation");
                }
            }
            else
            {
                Vector3 adjustedPos = AdjustNotePosition(notePos, pitch, size);
                StartCoroutine(Choi_NoteManager.instance.SpawnNote(noteId, _time, adjustedPos, size));
                Debug.Log($"Note ID: {noteId} - Creating");
            }



            // 각 노트의 사운드 데이터 처리
            foreach (SoundData sound in sounds)
            {
                float delay = sound.d;
                pitch = sound.p;
                int volume = sound.v;

                // 사운드 정보 출력
                Debug.Log($"Sound Delay: {delay} Sound Pitch: {pitch} Sound Volume: {volume}");
            }
        }

        Debug.Log($"Skipped Note Count: {skippedNoteCount}");

        // 추가 데이터 처리 가능

        // 유니티 오브젝트 생성, 애니메이션 등의 작업 가능
    }
    bool ShouldSkipNoteCreation(NoteData currentNote, List<NoteData> sameTimeNotes)
    {
        NoteData farthestNote = GetFarthestNoteFromList(sameTimeNotes);
        return farthestNote != currentNote;
    }

    void CreateNotesAtStarts(List<NoteData> sameTimeNotes, int id)
    {
        NoteData leftNote = sameTimeNotes[0];
        NoteData rightNote = sameTimeNotes[1];

        // 큰 사이즈를 선택
        float maxSize = Mathf.Max(leftNote.size, rightNote.size);

        Vector3 leftStartPos = new Vector3(-6f + maxSize, DEFAULT_POS_Y);
        Vector3 rightStartPos = new Vector3(6f - maxSize, DEFAULT_POS_Y);

        StartCoroutine(Choi_NoteManager.instance.SpawnNote(id, leftNote._time, leftStartPos, maxSize));
        StartCoroutine(Choi_NoteManager.instance.SpawnNote(id, rightNote._time, rightStartPos, maxSize));

        Debug.Log($"Note ID: {leftNote.noteId} - Creating at Left Start with Size {maxSize}");
        Debug.Log($"Note ID: {rightNote.noteId} - Creating at Right Start with Size {maxSize}");
    }


    Vector3 AdjustNotePosition(Vector3 originalPosition, int pitch, float noteSize)
    {
        float adjustedX = originalPosition.x;

        float pitchPercentage = (float)pitch / 127f;

        if (pitchPercentage >= 0.5f)
        {
            float xPos = Mathf.Lerp(1.2f, 7.6f, (pitchPercentage - 0.5f) * 2);
            adjustedX = xPos;
        }
        else
        {
            float xPos = Mathf.Lerp(-7.6f, -1.2f, pitchPercentage * 2);
            adjustedX = xPos;

            if (pitch == 0)
            {
                adjustedX = -7.6f + noteSize;
            }
            else if (pitch == 1)
            {
                adjustedX = -1.2f + noteSize;
            }
        }

        float leftBoundary = -7.6f + noteSize;
        if (adjustedX < leftBoundary)
        {
            adjustedX = leftBoundary;
        }

        float rightBoundary = 7.6f - noteSize;
        if (adjustedX > rightBoundary)
        {
            adjustedX = rightBoundary;
        }

        Vector3 adjustedPosition = new Vector3(adjustedX, originalPosition.y);
        Choi_NoteManager.instance.DeactivateOverlappingNotes(adjustedPosition, 0.1f); // 수정된 부분

        return adjustedPosition;
    }

    NoteData GetFarthestNoteFromList(List<NoteData> noteList)
    {
        NoteData farthestNote = null;
        float maxDistance = 0f;

        foreach (NoteData note in noteList)
        {
            foreach (NoteData otherNote in noteList)
            {
                if (note != otherNote)
                {
                    float distance = Mathf.Abs(note.pos - otherNote.pos);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        farthestNote = note;
                    }
                }
            }
        }

        return farthestNote;
    }
}
