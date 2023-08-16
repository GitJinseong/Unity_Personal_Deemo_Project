using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class JudgeButtonController : MonoBehaviour
{
    public GameObject obj_Notes;
    public TMP_Text text_judege;
    public string stringPos;
    private float gameTime;
    private float judgeTime;

    private void Start()
    {
        gameTime = TimeTracker.instance.startTime;
    }

    public void CallJudgeButton()
    {
        Note[] notes = obj_Notes.GetComponentsInChildren<Note>();

        Note noteToDeactivate = null;
        float lowestTime = float.MaxValue;

        foreach (Note note in notes)
        {
            CollisionDetection collisionDetection = note.GetComponent<CollisionDetection>();
            judgeTime = (Time.time - gameTime) - note.time;
            if (judgeTime >= 0.4f && note.stringPos == stringPos && note.time < lowestTime && !collisionDetection.isHide)
            {
                lowestTime = note.time;
                noteToDeactivate = note;
            }
        }

        if (noteToDeactivate != null)
        {
            CollisionDetection collisionDetection = noteToDeactivate.GetComponent<CollisionDetection>();
            if (collisionDetection != null)
            {
                Debug.Log("타임: " + ((Time.time - gameTime) - lowestTime));
                judgeTime = (Time.time - gameTime) - lowestTime;
                GameManager.instance.ChangeJudgeText(judgeTime.ToString());

                collisionDetection.Hide(); // CollisionDetection 스크립트의 Hide 함수 호출

                if (judgeTime >= 0.8f)
                {
                    Debug.Log("Charming");
                    GameManager.instance.ChangeTimingText(judgeTime.ToString());
                    GameManager.instance.AddCombo();
                    GameManager.instance.ChangeJudgeText("CHARMING!");
                }
                else if (judgeTime >= 0.7f)
                {
                    Debug.Log("Normal");
                    GameManager.instance.ChangeTimingText(judgeTime.ToString());
                    GameManager.instance.AddCombo();
                    GameManager.instance.ChangeJudgeText("NORMAL");
                }
                else
                {
                    GameManager.instance.ChangeTimingText(judgeTime.ToString());
                    GameManager.instance.ResetCombo();
                    GameManager.instance.ChangeJudgeText("MISS!");
                }
            }
        }
    }
}
