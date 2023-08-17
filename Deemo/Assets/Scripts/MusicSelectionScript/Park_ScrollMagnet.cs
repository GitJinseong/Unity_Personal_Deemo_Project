using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_ScrollMagnet : MonoBehaviour
{
    public RectTransform rectTransform;

    private RectTransform[] rectTransformChild;

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
        
    }
}
