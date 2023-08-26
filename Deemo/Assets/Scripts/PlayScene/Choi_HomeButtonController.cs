using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_HomeButtonController : MonoBehaviour
{
    public GameObject obj_SettingUI;
    public Choi_LoadScene loadScene;
    public string sceneName;
    public Park_MainSceneOpacity mainSceneOpacity;

    private List<GameObject> list = new List<GameObject>();

    private Color colorWhite = new Color(1, 1, 1, 1);
    private Color colorBlack = new Color(1, 1, 1, 0);
    public void DoRetry()
    {

        Time.timeScale = 1.0f;
        loadScene.RunForPause(1f, sceneName);
        StartCoroutine(mainSceneOpacity.EndOpacityForPause());

        StartCoroutine(btnOpacity());

        //obj_SettingUI.SetActive(false);
    }

    private IEnumerator btnOpacity()
    {


        float timeElapsed = 0.0f;

        while (timeElapsed < 0.5f)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / 0.5f);

            //µ· ÅÍÄ¡ ¹Ì :<
            obj_SettingUI.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.Lerp(colorWhite, colorBlack, time);
            obj_SettingUI.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.Lerp(colorWhite, colorBlack, time);
            obj_SettingUI.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.Lerp(colorWhite, colorBlack, time);
            obj_SettingUI.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.Lerp(colorWhite, colorBlack, time);

            yield return null;


        }
    }
}
