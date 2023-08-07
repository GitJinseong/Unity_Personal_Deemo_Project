using UnityEngine;

public class SkipScrollObject : MonoBehaviour
{
    public GameObject obj_Bg;
    public Vector2 targetPosition = Vector2.zero; // 목표 지점
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = obj_Bg.GetComponent<RectTransform>();
    }

    // 스크롤을 즉시 목표 지점으로 이동시키는 함수
    public void SkipScroll()
    {
        obj_Bg.SetActive(false);
        obj_Bg.SetActive(true);
        rectTransform.anchoredPosition = targetPosition;
    }
}
