using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Choi_SetTopObjs : MonoBehaviour
{
    public TMP_Text txt_SongName;
    public TMP_Text txt_Difficulty;
    public Image img_Difficulty;

    public Sprite sprite_HardIcon;
    public Sprite sprite_NormalIcon;
    public Sprite sprite_EasyIcon;

    private void Start()
    {
        txt_SongName.text = Park_GameManager.instance.title;
        string difficulty = "";
        switch (Park_GameManager.instance.difficulty)
        {
            case "Hard":
                difficulty = "HARD LV10";
                img_Difficulty.sprite = sprite_HardIcon;
                break;

            case "Normal":
                difficulty = "NORMAL LV8";
                img_Difficulty.sprite = sprite_NormalIcon;
                break;

            case "Easy":
                difficulty = "Easy LV4";
                img_Difficulty.sprite = sprite_EasyIcon;
                break;
        }

        txt_Difficulty.text = difficulty;
    }
}
