using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Choi_SetTopObjs : MonoBehaviour
{
    public TMP_Text txt_SongName;
    public TMP_Text txt_Difficulty;
    public TMP_Text txt_backSongName;
    public Image img_Difficulty;

    public Sprite sprite_HardIcon;
    public Sprite sprite_NormalIcon;
    public Sprite sprite_EasyIcon;

    private void Start()
    {
        txt_SongName.text = Park_GameManager.instance.title;
        txt_backSongName.text = Park_GameManager.instance.title;
        string difficulty = "";
        switch (Park_GameManager.instance.difficulty)
        {
            case "Hard":
                for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
                {
                    if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
                    {
                        difficulty = "Hard LV" + Park_GameManager.instance.musicInformation["Hard"][i];
                    }
                }

                img_Difficulty.sprite = sprite_HardIcon;
                break;

            case "Normal":
                for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
                {
                    if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
                    {
                        difficulty = "Normal LV" + Park_GameManager.instance.musicInformation["Normal"][i];
                    }
                }

                img_Difficulty.sprite = sprite_NormalIcon;
                break;

            case "Easy":
                for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
                {
                    if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
                    {
                        difficulty = "Easy LV" + Park_GameManager.instance.musicInformation["Easy"][i];
                    }
                }

                img_Difficulty.sprite = sprite_EasyIcon;
                break;
        }

        txt_Difficulty.text = difficulty;
    }
}