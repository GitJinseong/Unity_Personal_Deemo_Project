using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_PlaySceneScaleChange : MonoBehaviour
{
    public float scaleStart;
    public float scaleEnd;
    public float duration;
    private float originalScaleY;

    // Start is called before the first frame update
    void Start()
    {
        originalScaleY = GetComponent<Transform>().localScale.y;

        StartCoroutine(ScaleChange());
    }

    private IEnumerator ScaleChange()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 스케일 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용하여 스케일 값을 변경
            GetComponent<Transform>().localScale = new Vector2(Mathf.Lerp(scaleStart, scaleEnd, time), originalScaleY);

            yield return null;
        }
    }
}
