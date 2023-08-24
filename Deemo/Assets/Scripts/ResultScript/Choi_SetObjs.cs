using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Choi_SetObjs : MonoBehaviour
{
    public TMP_Text txt_Title;
    public TMP_Text txt_TitleShadow;
    public TMP_Text txt_Difficulty;
    public TMP_Text txt_TrueNote;

    private void Start()
    {
        txt_Title.text = Park_GameManager.instance.title;
        txt_TitleShadow.text = txt_Title.text;
        txt_TrueNote.text = Choi_GameManager.instance.GetTrueNotes().ToString();
        Debug.Log($"트루노트총합 : {txt_TrueNote.text}");

        string difficulty = "";
        switch (Park_GameManager.instance.difficulty)
        {
            case "Hard":
                difficulty = "HARD LV10";
                break;

            case "Normal":
                difficulty = "NORMAL LV8";
                break;

            case "Easy":
                difficulty = "Easy LV4";
                break;
        }

        txt_Difficulty.text = difficulty;
    }
}
