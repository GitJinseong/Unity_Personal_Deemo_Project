using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_ResumeButtonController : MonoBehaviour
{
    public GameObject obj_SettingUI;
    public GameObject obj_ReadyUI;
    public void DoResume()
    {
        obj_SettingUI.SetActive(false);
        obj_ReadyUI.SetActive(true);
        Choi_TimeManager.instance.CoroutineDelayForResumeGame(4f);
    }
}
