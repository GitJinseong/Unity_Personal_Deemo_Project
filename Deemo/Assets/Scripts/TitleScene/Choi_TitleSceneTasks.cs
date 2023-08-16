using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneTasks : MonoBehaviour
{
    public static TitleSceneTasks instance = default;

    public GameObject obj_RayarkLogo = default;
    public GameObject obj_Background = default;
    public GameObject obj_Background_Light = default;
    public GameObject obj_DeemoLogo = default;
    public GameObject obj_TouchToStart = default;
    public GameObject obj_StartBtn = default;

    void Awake()
    {
        instance = this;
    }

    public void StartRayarkLogo()
    {
        obj_RayarkLogo.SetActive(true);
    }

    public void StartBackgrounds()
    {
        obj_Background.SetActive(true);
        obj_Background_Light.SetActive(true);
    }

    public void StartDeemoLogo()
    {
        obj_DeemoLogo.SetActive(true);
    }

    public void StartTouchToStart()
    {
        obj_TouchToStart.SetActive(true);
        obj_StartBtn.SetActive(true);
    }
}