using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_JudgeManager : MonoBehaviour
{
    public static Choi_JudgeManager instance;
    private float judge_Charming = -1.4f;
    private float judge_Normal = -0.5f;
    private float note_PosY;
    private Choi_CollisionDetection note_CollisionDetection;

    private void Awake()
    {
        instance = this;
    }

    public void CheckJudge(GameObject note)
    {
        note_PosY = note.transform.position.y;
        note_CollisionDetection = note.GetComponent<Choi_CollisionDetection>();
        if (note_CollisionDetection.isHide == false)
        {
            if (note_PosY < judge_Charming)
            {
                note_CollisionDetection.Hide();
                Choi_GameManager.instance.AddCombo();
                Choi_GameManager.instance.ChangeJudgeText("CHARMING!");
            }
            else if (note_PosY < judge_Normal)
            {
                note_CollisionDetection.Hide();
                Choi_GameManager.instance.AddCombo();
                Choi_GameManager.instance.ChangeJudgeText("NORMAL!");
            }
            else
            {
                note_CollisionDetection.Hide();
                Choi_GameManager.instance.ResetCombo();
                Choi_GameManager.instance.ChangeJudgeText("MISS!");
            }
        }
    }
}
