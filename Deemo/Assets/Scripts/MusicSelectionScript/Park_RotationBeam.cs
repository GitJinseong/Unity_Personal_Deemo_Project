using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Park_RotationBeam : MonoBehaviour
{
    public RectTransform scrollContent;

    // 오브젝트 RectTransform 를 불러오기 위한 변수
    private RectTransform rectTransform;

    private float radius = 300.0f;

    private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = (scrollContent.anchoredPosition.y - scrollContent.childCount * 360.0f) / 720.0f * 15.0f;

        rectTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, radius * (15.0f - speed)* Mathf.Deg2Rad));
    }
}
