using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Choi_JudgeButtonController : MonoBehaviour
{
    public GameObject obj_Notes;
    public TMP_Text text_judege;
    public string stringPos;
    private float gameTime;
    private float judgeTime;

    private void Start()
    {
        gameTime = Choi_TimeTracker.instance.startTime;
    }

    public void CallJudgeButton()
    {
        Choi_Note[] notes = obj_Notes.GetComponentsInChildren<Choi_Note>();

        Choi_Note noteToDeactivate = null;
        float lowestTime = float.MaxValue;

        foreach (Choi_Note note in notes)
        {
            Choi_CollisionDetection collisionDetection = note.GetComponent<Choi_CollisionDetection>();
            judgeTime = (Time.time - gameTime) - note.time;
            if (judgeTime >= 0.4f && note.stringPos == stringPos && note.time < lowestTime && !collisionDetection.isHide)
            {
                lowestTime = note.time;
                noteToDeactivate = note;
            }
        }

        if (noteToDeactivate != null)
        {
            Choi_CollisionDetection collisionDetection = noteToDeactivate.GetComponent<Choi_CollisionDetection>();
            if (collisionDetection != null)
            {
                Debug.Log("타임: " + ((Time.time - gameTime) - lowestTime));
                judgeTime = (Time.time - gameTime) - lowestTime;
                Choi_GameManager.instance.ChangeJudgeText(judgeTime.ToString());

                collisionDetection.Hide(); // CollisionDetection 스크립트의 Hide 함수 호출

                if (judgeTime >= 0.8f)
                {
                    Debug.Log("Charming");
                    Choi_GameManager.instance.ChangeTimingText(judgeTime.ToString());
                    Choi_GameManager.instance.AddCombo();
                    Choi_GameManager.instance.ChangeJudgeText("CHARMING!");
                }
                else if (judgeTime >= 0.7f)
                {
                    Debug.Log("Normal");
                    Choi_GameManager.instance.ChangeTimingText(judgeTime.ToString());
                    Choi_GameManager.instance.AddCombo();
                    Choi_GameManager.instance.ChangeJudgeText("NORMAL");
                }
                else
                {
                    Choi_GameManager.instance.ChangeTimingText(judgeTime.ToString());
                    Choi_GameManager.instance.ResetCombo();
                    Choi_GameManager.instance.ChangeJudgeText("MISS!");
                }
            }
        }
    }
}
