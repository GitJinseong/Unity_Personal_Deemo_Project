using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_Active : MonoBehaviour
{
    public GameObject activeObject;

    public GameObject targetObject;

    // 처음 클릭한 마우스 위치
    private Vector2 StartMousePosition;

    // 버튼 클릭을 확인
    private bool isPoint = false;

    private float timeElapsed = 0.0f;

    void Start()
    {
        targetObject.SetActive(true);

        timeElapsed = 0.0f;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (StartMousePosition != (Vector2)Input.mousePosition)
        {
            isPoint = false;
        }
    }

    private void OnMouseDown()
    {
        StartMousePosition = Input.mousePosition;

        isPoint = true;
    }

    private void OnMouseUp()
    {
        if (isPoint == true && activeObject.activeSelf == false && 5.0f <= timeElapsed)
        {
            activeObject.SetActive(true);
        }
    }
}
