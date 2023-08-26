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
    private float duration = 0.5f;
    private float loopDuration = 2.0f;

    private bool isCheck = false;

    private void OnEnable()
    {
        Choi_TimeManager.instance.PauseGame();
        StartCoroutine(StartScale());
    }

    private void Update()
    {
        if (isCheck)
        {
            StartCoroutine(StartScale());
            isCheck = false;
        }
    }

    private IEnumerator StartScale()
    {
        float startTime = Time.realtimeSinceStartup;

        while (timeElapsed < duration)
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            timeElapsed = elapsedTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            Vector2 newScale = new Vector2(transform.localScale.x, Mathf.Lerp(0.0f, 1.0f, time * time));
            GetComponent<Transform>().localScale = newScale;

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
        float startTime = Time.realtimeSinceStartup;

        while (timeElapsed < loopDuration)
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            timeElapsed = elapsedTime;

            float time = Mathf.Clamp01(timeElapsed / loopDuration);

            Vector2 newScale = new Vector2(transform.localScale.x, Mathf.Lerp(1.0f, 0.5f, time));
            GetComponent<Transform>().localScale = newScale;

            yield return null;
        }

        // 스택 오버플로우 발생으로 예외처리
        //StartCoroutine(LoopScaleUp());
    }

    private IEnumerator LoopScaleUp()
    {
        float startTime = Time.realtimeSinceStartup;

        while (timeElapsed < loopDuration)
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            timeElapsed = elapsedTime;

            float time = Mathf.Clamp01(timeElapsed / loopDuration);

            Vector2 newScale = new Vector2(transform.localScale.x, Mathf.Lerp(0.5f, 1.0f, time));
            GetComponent<Transform>().localScale = newScale;

            yield return null;
        }

        // 스택 오버플로우 발생으로 예외처리
        //StartCoroutine(LoopScaleDown());
    }

    public IEnumerator EndScale()
    {
        float yPos = GetComponent<Transform>().localScale.y;
        float startTime = Time.realtimeSinceStartup;

        while (timeElapsed < loopDuration)
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            timeElapsed = elapsedTime;

            float time = Mathf.Clamp01(timeElapsed / loopDuration);

            Vector2 newScale = new Vector2(transform.localScale.x, Mathf.Lerp(yPos, 0.0f, time * time));
            GetComponent<Transform>().localScale = newScale;

            yield return null;
        }
    }
}
