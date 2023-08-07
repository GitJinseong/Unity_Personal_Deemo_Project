using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipScrollObject : MonoBehaviour
{
    public GameObject skipObj;
    public Vector2 targetPosition = Vector2.zero;       // 목표 지점

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = skipObj.GetComponent<RectTransform>();
    }

    public void RunSkipObject()
    {
        Vector2 newPos = Vector2.Lerp(targetPosition, targetPosition, 0f);

        rectTransform.anchoredPosition = newPos;

    }

}