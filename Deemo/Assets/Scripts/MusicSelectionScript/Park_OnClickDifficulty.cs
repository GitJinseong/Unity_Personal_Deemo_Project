using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Park_OnClickDifficulty : MonoBehaviour
{
    private TMP_Text difficultyText;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        difficultyText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Park_GameManager.instance.difficulty)
        {
            case "Easy":

                for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
                {
                    if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
                    {
                        difficultyText.text = "Easy LV" + Park_GameManager.instance.musicInformation["Easy"][i];
                        scoreText.text = string.Format("{0:F2} %", float.Parse(Park_GameManager.instance.musicInformation["EasyBestScore"][i]));
                    }
                }

                break;

            case "Normal":

                for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
                {
                    if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
                    {
                        difficultyText.text = "Normal LV" + Park_GameManager.instance.musicInformation["Normal"][i];
                        scoreText.text = string.Format("{0:F2} %", float.Parse(Park_GameManager.instance.musicInformation["NormalBestScore"][i]));
                    }
                }

                break;

            case "Hard":

                for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
                {
                    if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
                    {
                        difficultyText.text = "Hard LV" + Park_GameManager.instance.musicInformation["Hard"][i];
                        scoreText.text = string.Format("{0:F2} %", float.Parse(Park_GameManager.instance.musicInformation["HardBestScore"][i]));
                    }
                }

                break;
        }
    }
}
