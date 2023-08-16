using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text judgeText;
    public TMP_Text timingText;
    private int combo;

    public int activatedJudgeColliderCount = 0;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeTimingText(string txt)
    {
        timingText.text = txt;

    }
    public void ChangeJudgeText(string txt)
    {
        judgeText.text = "(" + combo.ToString() + ") " + txt;
    }

    public void AddCombo()
    {
        combo++;
    }

    public void ResetCombo()
    {
        combo = 0;
    }
}
