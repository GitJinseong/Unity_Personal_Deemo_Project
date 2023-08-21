using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Park_LoadingSprite : MonoBehaviour
{
    private Image loadingSprite;

    public List<Sprite> listSprite;

    private float timeElapsed = 0.0f;
    private int count = 0;

    void Start()
    {
        loadingSprite = GetComponent<Image>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (0.1f <= timeElapsed)
        {
            loadingSprite = GetComponent<Image>();

            loadingSprite.sprite = listSprite[count];

            count += 1;

            if (count == 10)
            {
                count = 0;
            }

            TimeReset();
        }
    }

    private void TimeReset()
    {
        timeElapsed = 0.0f;
    }
}
