using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Park_ScrollHandle : MonoBehaviour
{
    public Park_OnClickSettingLeft OnClickSettingLeft;
    public Park_OnClickSettingCenter OnClickSettingCenter;
    public Park_OnClickSettingRight OnClickSettingRight;

    public Coroutine coroutine = null;

    private RectTransform rectTransform;

    public float xPos;

    private float MaxPos = 400.0f;
    private float MinPos = -400.0f;

    private float duration = 1.0f;

    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Park_GameManager.instance.coroutineCheck == true)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            Park_GameManager.instance.HandleCoroutine(false);
        }

        if (isPressed == true)
        {
            // 1. 마우스 포인터의 스크린 좌표를 월드 좌표로 변환
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            mousePosition.y = rectTransform.position.y;

            // 2. 마우스 포인터와 타일의 거리를 체크하여 타일 안에 있는지 확인
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f);

            rectTransform.position = new Vector2(Mathf.Clamp(hit.point.x, -5.5f, 5.5f), hit.point.y);
        }

        xPos = (int)(rectTransform.anchoredPosition.x + 400 + 25) / 50;
    }

    private void OnMouseDown()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        isPressed = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;

        //노드의 개수
        int nodeCount = 16;

        //노드 간의 거리
        float rectNodeDistance = (MaxPos - MinPos) / nodeCount;

        //노드의 인덱스
        //Debug.Log((int)((rectTransform.anchoredPosition.x + 400 + 25) / 50));

        //실제 anchoredPosition.x
        //Debug.Log(((int)((rectTransform.anchoredPosition.x + 400 + (rectNodeDistance / 2)) / rectNodeDistance) - 400 / rectNodeDistance) * rectNodeDistance);
        //                *            음수값 제거                 *  *  노드간의 거리 반값   *  
        //코루틴 시작
        coroutine = StartCoroutine(StartScroll(((int)((rectTransform.anchoredPosition.x + 425) / 50) - 8) * 50));
    }

    private IEnumerator StartScroll(float XPos)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            // 시간 경과에 따라 스케일 값 보간
            timeElapsed += Time.deltaTime;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / duration);

            // 보간된 값을 사용하여 스케일 값을 변경
            rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(rectTransform.anchoredPosition.x, XPos, time), rectTransform.anchoredPosition.y);

            yield return null;
        }
    }
}
