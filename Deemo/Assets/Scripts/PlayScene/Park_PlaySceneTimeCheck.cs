using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_PlaySceneTimeCheck : MonoBehaviour
{
    private float timeElapsed = 0.0f;
    private int duration = 0;

    public IEnumerator Play()
    {
        for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
        {
            if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
            {
                duration = Convert.ToInt32(Park_GameManager.instance.musicInformation["Time"][i]);
            }
        }

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}
