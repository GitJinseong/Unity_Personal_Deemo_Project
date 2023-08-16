using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choi_SettingUICloseEffect : MonoBehaviour
{
    public GameObject obj_closeUI = default;
    public GameObject obj_BtnSetting = default;
    public GameObject obj_Btns = default;
    public GameObject obj_SettingUIPanel = default;

    public float waitForCloseTime = 1.2f;

    private Image img_BtnSetting = default;
    private Coroutine delayCoroutine; // Delay 코루틴 참조를 저장하는 변수
    private Choi_FadeOutSettingObjects script_FadeOutSettingObjects = default;
    private Choi_FadeOutSettingPanel script_FadeOutSettingPanel = default;


    public void Awake()
    {
        img_BtnSetting = obj_BtnSetting.GetComponent<Image>();

        script_FadeOutSettingObjects = obj_Btns.GetComponent<Choi_FadeOutSettingObjects>();
        script_FadeOutSettingPanel = obj_SettingUIPanel.GetComponent<Choi_FadeOutSettingPanel>();
    }

    public void CloseUI()
    {
        if (delayCoroutine != null) // 이미 실행 중인 Delay 코루틴이 있다면 중지시킵니다.
        {
            return;
        }
        else
        {
            // 가져온 스크립트 컴포넌트 사용 예시
            script_FadeOutSettingObjects.StartFadeOut();
            script_FadeOutSettingPanel.StartFadeOut();
            delayCoroutine = StartCoroutine(Delay());
        }
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitForCloseTime);
        delayCoroutine = null; // 코루틴 실행이 끝났으므로 참조를 초기화합니다.
        img_BtnSetting.raycastTarget = true;
        obj_closeUI.SetActive(false);
    }
}
