using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Park_GameManager : MonoBehaviour
{
    public static Park_GameManager instance;
    
    public GameObject barPrefab;
    public GameObject spritePrefab;
    public GameObject textPrefab;

    public GameObject content;
    
    public Transform scrollBar;
    public Transform textImgSub;
    public Transform textTitleSub;

    public AudioSource audioSource;

    public string title;
    public string difficulty;
    public float score;

    public float bgm;
    public float speed;
    public float key;



    private Sprite[] spriteResources;

    public string path = "Scenes/MusicSelectionScene/MusicList/";
    public Dictionary<string, List<string>> musicInformation = new Dictionary<string, List<string>>();

    public bool coroutineCheck = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }

        musicInformation = Park_CSVReader.instance.ReadCSVFile("MusicList");

        for (int i = 0; i < musicInformation["Title"].Count; i++)
        {
            Instantiate(barPrefab, scrollBar);
        }

        content.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(content.GetComponent<RectTransform>().anchoredPosition.x,
            musicInformation["Title"].Count * 720.0f / 2.0f); 

        spriteResources = Resources.LoadAll<Sprite>(path + "Sprite");

        for (int j = 0; j < musicInformation["Title"].Count; j++)
        {
            string targetSpriteName = musicInformation["Sprite"][j];

            for (int i = 0; i < spriteResources.Length; i++)
            {
                if (targetSpriteName == spriteResources[i].name + ".png")
                {
                    GameObject newImg = Instantiate(spritePrefab, textImgSub);
                    newImg.GetComponent<Image>().sprite = spriteResources[i];
                    break;
                }
            }
        }

        for (int i = 0; i < musicInformation["Title"].Count; i++)
        {
            GameObject newText = Instantiate(textPrefab, textTitleSub);
            newText.transform.GetChild(0).gameObject.transform.GetComponent<TMP_Text>().text = musicInformation["Title"][i];
        }

        bgm = 5.0f;
        speed = 5.0f;
        key = 5.0f;
    }

    public void Title(string title_)
    {
        title = title_;
    }

    public void Difficulty(int difficulty_)
    {
        switch (difficulty_)
        {
            case 0:

                difficulty = "Easy";

                break;
            case 1:

                difficulty = "Normal";

                break;
            case 2:

                difficulty = "Hard";

                break;
        }
    }

    public void Score(float score_)
    {
        score = score_;
    }

    public void Bgm(float bgm_)
    {
        bgm = bgm_;
    }

    public void Speed(float speed_)
    {
        speed = speed_;
    }

    public void Key(float key_)
    {
        key = key_;
    }

    public void HandleCoroutine(bool coroutineCheck_)
    {
        coroutineCheck = coroutineCheck_;
    }
}
