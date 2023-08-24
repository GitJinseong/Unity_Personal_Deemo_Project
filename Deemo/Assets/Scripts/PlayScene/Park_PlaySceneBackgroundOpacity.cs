using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_PlaySceneBackgroundOpacity : MonoBehaviour
{
    public Park_PlaySceneBtn resume;
    public Park_PlaySceneBtn retry;
    public Park_PlaySceneBtn songs;
    public Park_PlaySceneBtn home;

    private float timeElapsed = 0.0f;

    private bool isCheck = false;

    private void OnEnable()
    {
        StartCoroutine(StartScale());
    }

    private void Update()
    {
        if (isCheck == true)
        {
            StartCoroutine(StartScale());

            isCheck = false;
        }
    }

    private IEnumerator StartScale()
    {
        timeElapsed = 0.0f;

        while (timeElapsed < 2.0f)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / 2.0f);

            GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x,
                Mathf.Lerp(0.0f, 1.0f, time * time));

            yield return null;
        }

        StartCoroutine(resume.StartScale());
        StartCoroutine(retry.StartScale());
        StartCoroutine(songs.StartScale());
        StartCoroutine(home.StartScale());
        StartCoroutine(LoopScaleDown());
    }

    private IEnumerator LoopScaleDown()
    {
        timeElapsed = 0.0f;

        while (timeElapsed < 2.0f)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / 2.0f);

            GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x,
                Mathf.Lerp(1.0f, 0.5f, time));

            yield return null;
        }

        StartCoroutine(LoopScaleUp());
    }

    private IEnumerator LoopScaleUp()
    {
        timeElapsed = 0.0f;

        while (timeElapsed < 2.0f)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / 2.0f);

            GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x,
                Mathf.Lerp(0.5f, 1.0f, time));

            yield return null;
        }

        StartCoroutine(LoopScaleDown());
    }

    public IEnumerator EndScale()
    {
        float yPos = GetComponent<Transform>().localScale.y;

        timeElapsed = 0.0f;

        while (timeElapsed < 2.0f)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / 2.0f);

            GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x,
                Mathf.Lerp(yPos, 0.0f, time * time));

            yield return null;
        }
    }
}
