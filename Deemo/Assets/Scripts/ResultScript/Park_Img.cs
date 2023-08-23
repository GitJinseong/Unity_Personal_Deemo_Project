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
            if (Park_GameManager.instance.musicInformation["Title"][i] == Park_GameManager.instance.title)
            {
                for (int j = 0; j < Park_GameManager.instance.musicInformation["Title"].Count; j++)
                {
                    if (Park_GameManager.instance.musicInformation["Sprite"][i] == spriteResources[j].name + ".png")
                    {
                        gameObject.GetComponent<Image>().sprite = spriteResources[j];
                        break;
                    }
                }

            }
        }
    }
}
