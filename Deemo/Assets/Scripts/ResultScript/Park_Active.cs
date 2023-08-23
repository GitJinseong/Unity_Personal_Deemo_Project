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

    void Start()
    {
        targetObject.SetActive(true);
    }

    private void Update()
    {
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
        if (isPoint == true && activeObject.activeSelf == false)
        {
            activeObject.SetActive(true);
        }
    }
}
