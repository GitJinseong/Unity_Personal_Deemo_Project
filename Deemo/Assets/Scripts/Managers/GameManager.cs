using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text judgeText;
    public JudgeButtonController judgeAreaBtn_1;
    public JudgeButtonController judgeAreaBtn_2;
    public JudgeButtonController judgeAreaBtn_3;
    public JudgeButtonController judgeAreaBtn_4;

    public int activatedJudgeColliderCount = 0;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeJudgeText(string txt)
    {
        judgeText.text = txt;
    }
}
