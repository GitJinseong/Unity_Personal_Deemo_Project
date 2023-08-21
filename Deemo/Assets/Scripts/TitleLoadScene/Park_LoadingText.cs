using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Park_LoadingText : MonoBehaviour
{
    private TMP_Text loadingText;

    private float timeElapsed = 0.0f;
    private int count = 0;

    void Start()
    {
        loadingText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (0.3f <= timeElapsed)
        {
            switch (count)
            {
                case 0:

                    loadingText.text = "Loading";

                    TimeReset();

                    break;
                case 1:

                    loadingText.text = "Loading.";

                    TimeReset();

                    break;

                case 2:

                    loadingText.text = "Loading..";

                    TimeReset();

                    break;

                case 3:

                    loadingText.text = "Loading...";

                    TimeReset();

                    break;
            }

            count += 1;

            if (count == 4)
            {
                count = 0;
            }
        }
    }

    private void TimeReset()
    {
        timeElapsed = 0.0f;
    }
}
