using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_GetBeforePosition : MonoBehaviour
{
    RectTransform rectTransform;
    private float default_PosY = 14400f;
    private bool isMoved = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        int tempIndex = Park_GameManager.index;
        float tempPos_Y = default_PosY - (tempIndex * 720f);
        Debug.Log($"저장된 index: {tempIndex}");
        Debug.Log($"이동될 PosY: {tempPos_Y}");
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, tempPos_Y);
    }
}
