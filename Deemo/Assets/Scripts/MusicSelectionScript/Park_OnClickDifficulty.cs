using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Park_OnClickDifficulty : MonoBehaviour
{
    public TMP_Text scoreText;
    private TMP_Text difficultyText;

    private int totalIndex;
    private int beforeIndex;
    private Dictionary<string, float[]> scores;
    private string[] csv_Titles;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        difficultyText = GetComponent<TMP_Text>();
        csv_Titles = Park_GameManager.instance.musicInformation["Title"].ToArray();
        scores = new Dictionary<string, float[]>();
        scores["EasyBestScore"] = Choi_PlayerDataManager.GetScoreForAll("EasyBestScore");
        scores["NormalBestScore"] = Choi_PlayerDataManager.GetScoreForAll("NormalBestScore");
        scores["HardBestScore"] = Choi_PlayerDataManager.GetScoreForAll("HardBestScore");

        totalIndex = Park_GameManager.instance.musicInformation["List"].Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Park_GameManager.instance.difficulty)
        {
            case "Easy":

                for (int i = 0; i < totalIndex; i++)
                {
                    if (csv_Titles[i] == Park_GameManager.instance.title)
                    {
                        difficultyText.text = "Easy LV" + Park_GameManager.instance.musicInformation["Easy"][i];
                        scoreText.text = string.Format("{0:F2} %", scores["EasyBestScore"][i]);
                        SaveIndexToParkGameManager(i);
                    }
                }

                break;

            case "Normal":

                for (int i = 0; i < totalIndex; i++)
                {
                    if (csv_Titles[i] == Park_GameManager.instance.title)
                    {
                        difficultyText.text = "Normal LV" + Park_GameManager.instance.musicInformation["Normal"][i];
                        scoreText.text = string.Format("{0:F2} %", scores["NormalBestScore"][i]);
                        SaveIndexToParkGameManager(i);
                    }
                }

                break;

            case "Hard":

                for (int i = 0; i < totalIndex; i++)
                {
                    if (csv_Titles[i] == Park_GameManager.instance.title)
                    {
                        difficultyText.text = "Hard LV" + Park_GameManager.instance.musicInformation["Hard"][i];
                        scoreText.text = string.Format("{0:F2} %", scores["HardBestScore"][i]);
                        SaveIndexToParkGameManager(i);
                    }
                }

                break;
        }
    }

    private void SaveIndexToParkGameManager(int index)
    {
        if (beforeIndex == index)
        { return; }
        Park_GameManager.index = index;
        beforeIndex = index;
        Debug.Log($"Save index: {Park_GameManager.index}");
    }
}
