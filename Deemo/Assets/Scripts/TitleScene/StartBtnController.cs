using UnityEngine;
using UnityEngine.UI;

public class StartBtnController : MonoBehaviour
{
    public LoadScene loadScene = new LoadScene(); // LoadScene 클래스의 인스턴스 생성
    public Image img_Background_Clone = default;
    public Image img_Background_Light_Clone = default;
    public Image img_Background_Light_Skip = default;
    public Image img_DeemoLogo = default;
    public Image img_DeemoLogo_Skip = default;
    public GameObject obj_Background = default;
    public GameObject obj_Background_Light = default;
    public GameObject obj_Background_Light_Skip = default;
    public GameObject obj_Background_Clone = default;
    public GameObject obj_Background_Light_Clone = default;
    public GameObject obj_TouchToStart = default;
    public float fadeDuration = 2f;

    public void StartFadeOutObjects()
    {
        obj_TouchToStart.SetActive(false);
        obj_Background_Clone.SetActive(true);
        obj_Background_Light_Clone.SetActive(true);
        obj_Background.SetActive(false);
        obj_Background_Light.SetActive(false);
        obj_Background_Light_Skip.SetActive(false);
        StartCoroutine(TransparencyController.FadeOutImage(img_Background_Clone, fadeDuration));
        StartCoroutine(TransparencyController.FadeOutImage(img_Background_Light_Clone, fadeDuration));
        StartCoroutine(TransparencyController.FadeOutImage(img_Background_Light_Skip, fadeDuration));
        StartCoroutine(TransparencyController.BeginLateFadeOutImage(img_DeemoLogo, 3.0f, fadeDuration));
        StartCoroutine(TransparencyController.BeginLateFadeOutImage(img_DeemoLogo_Skip, 3.0f, fadeDuration));

        // 다음 씬으로 전환
        loadScene.Run(5.0f, "MainScene");
    }
}