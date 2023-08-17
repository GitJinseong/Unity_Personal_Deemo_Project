using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Park_ScrollContentImg : MonoBehaviour
{
    private RectTransform rectTransform;

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

        Vertical();
    }

    private void Vertical()
    {
        for (int i = 0; i < rectTransformChild.Length; i++)
        {
            rectTransformChild[i].anchoredPosition = new Vector2(128.0f, i * -512.0f);
        }
    }
}