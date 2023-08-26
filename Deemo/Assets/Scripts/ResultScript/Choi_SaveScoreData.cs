using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Choi_SaveScoreData : MonoBehaviour
{
    public GameObject obj_RecordEffect;
    void Start()
    {
        int index = Park_GameManager.index;
        string key = Park_GameManager.instance.difficulty + "BestScore";
        float score = Choi_GameManager.instance.total_Accuracy;
        Choi_PlayerDataManager.SaveScore(index, key, score);
        obj_RecordEffect.SetActive(true);
    }
}
