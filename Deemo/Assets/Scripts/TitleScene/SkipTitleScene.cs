using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkipTitleScene: MonoBehaviour
{
    public GameObject obj_Bg;
    public GameObject obj_Bg_Light;
    public GameObject obj_Bg_Light_Skip;
    public GameObject obj_RayarkLogo;
    public GameObject obj_DeemoLogo_Skip;
    public GameObject obj_TouchToStart;
    public GameObject obj_StartBtn;
    public GameObject obj_DelayManager;
    private RectTransform rect_Obj_Bg;
    private RectTransform rect_Obj_Bg_Light;

    public Vector2 targetPosition = Vector2.zero; // 목표 지점

    // Start is called before the first frame update
    void Start()
    {
        rect_Obj_Bg = obj_Bg.GetComponent<RectTransform>();
        rect_Obj_Bg_Light = obj_Bg_Light.GetComponent<RectTransform>();
    }

    // 스크롤을 즉시 목표 지점으로 이동시키는 함수
    public void SkipScroll()
    {
        StartCoroutine(SkipDelay());
    }

    public IEnumerator SkipDelay()
    {
        obj_DelayManager.SetActive(false);
        obj_Bg.SetActive(true);
        obj_Bg_Light.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        obj_Bg.SetActive(false);
        obj_Bg_Light.SetActive(false);
        obj_Bg.SetActive(true);
        obj_Bg_Light_Skip.SetActive(true);

        rect_Obj_Bg.anchoredPosition = targetPosition;
        rect_Obj_Bg_Light.anchoredPosition = targetPosition;

        obj_RayarkLogo.SetActive(false);
        obj_DeemoLogo_Skip.SetActive(true);
        obj_TouchToStart.SetActive(true);
        obj_StartBtn.SetActive(true);

        gameObject.SetActive(false);
    }
}
