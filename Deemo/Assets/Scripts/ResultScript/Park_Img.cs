using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Park_Img : MonoBehaviour
{
    private Sprite[] spriteResources;

    public void Start()
    {
        spriteResources = Resources.LoadAll<Sprite>(Park_GameManager.instance.path + "Sprite");

        for (int i = 0; i < Park_GameManager.instance.musicInformation["Title"].Count; i++)
        {
            string targetSpriteName = Park_GameManager.instance.musicInformation["Sprite"][i];

            if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
            {
                if (targetSpriteName == spriteResources[i].name + ".png")
                {
                    GetComponent<Image>().sprite = spriteResources[i];
                    break;
                }
            }
        }
    }
}
