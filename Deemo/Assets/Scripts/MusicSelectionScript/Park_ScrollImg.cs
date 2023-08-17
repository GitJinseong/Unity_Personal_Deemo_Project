using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_ScrollImg : MonoBehaviour
{
    public RectTransform ScrollContent;

    private RectTransform rectTransform;

    private RectTransform[] rectTransformChild;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransformChild = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rectTransformChild[i] = rectTransform.GetChild(i) as RectTransform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed = (ScrollContent.anchoredPosition.y - rectTransformChild.Length * 360.0f) / 720.0f * 512.0f;

        for (int i = 0; i < rectTransformChild.Length; i++)
        {
            rectTransformChild[i].anchoredPosition = new Vector2(128, -i * 512 - speed);
        }
    }
}
