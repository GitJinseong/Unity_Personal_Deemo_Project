using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Park_SceneChangeEditer : MonoBehaviour
{
    private Image darkSprite;

    private float timeElapsed = 0.0f;
    private float duration;

    void Start()
    {
        darkSprite = GetComponent<Image>();    
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed < duration)
        { 
        
        }
    }
}
