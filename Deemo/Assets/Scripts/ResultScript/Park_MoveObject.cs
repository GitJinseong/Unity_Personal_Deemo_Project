using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Park_MoveObject : MonoBehaviour
{
    public GameObject targetObject;

    private RectTransform rectTransform;

    public float duration = 0.0f;

    public float moveStart, moveEnd;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    { 
        StartCoroutine(Move());
    }
    
    private IEnumerator Move()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 알파값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용해서 moveStart에서 moveEnd로 이동
            rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(moveStart, moveEnd, time),rectTransform.anchoredPosition.y);

            yield return null;
        }

        targetObject.SetActive(true);
    }
}
