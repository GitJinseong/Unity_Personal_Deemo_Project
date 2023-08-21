using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Park_PlaySceneTitle : MonoBehaviour
{
    public TMP_Text title;

    private void Start()
    {
        title = GetComponent<TMP_Text>();

        title.text = Park_GameManager.instance.title;
    }
}
