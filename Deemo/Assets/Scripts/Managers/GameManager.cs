using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public JudgeButtonController judgeAreaBtn_1;
    public JudgeButtonController judgeAreaBtn_2;
    public JudgeButtonController judgeAreaBtn_3;
    public JudgeButtonController judgeAreaBtn_4;

    public int activatedJudgeColliderCount = 0;

    private void Awake()
    {
        instance = this;
    }
}
