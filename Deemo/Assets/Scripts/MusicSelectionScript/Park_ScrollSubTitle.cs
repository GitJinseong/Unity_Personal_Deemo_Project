using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_ScrollSubTitle : MonoBehaviour
{
    public RectTransform scrollContent;

    private RectTransform rectTransform;

    private RectTransform[] rectTransformChild;

    private float centerPos = -200.0f;

    private float radius = 300.0f;

    public float speed;

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
        speed = (scrollContent.anchoredPosition.y - rectTransformChild.Length * 360.0f) / 720.0f * 15.0f;

        for (int i = 0; i < rectTransformChild.Length; i++)
        {
            rectTransformChild[i].anchoredPosition =
                new Vector2
                (centerPos + (radius * Mathf.Cos((i * 15.0f + speed) * Mathf.Deg2Rad)),
                -(radius * 1.1f * Mathf.Sin((i * 15.0f + speed) * Mathf.Deg2Rad)));
        }
    }
}
