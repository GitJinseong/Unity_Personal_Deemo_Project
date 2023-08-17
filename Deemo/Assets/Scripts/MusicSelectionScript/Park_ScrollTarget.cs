using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_ScrollTarget : MonoBehaviour
{
    // Å¸°Ù ¿ÀºêÁ§Æ®
    public GameObject targetObject;

    // RectTransform ÄÄÆ÷³ÍÆ®
    private RectTransform gameObjectRectTransform;
    private RectTransform targetRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        gameObjectRectTransform = GetComponent<RectTransform>();
        targetRectTransform = targetObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObjectRectTransform.localScale = new Vector3(targetRectTransform.anchoredPosition.x + 400.0f, 1, 0);
    }
}
