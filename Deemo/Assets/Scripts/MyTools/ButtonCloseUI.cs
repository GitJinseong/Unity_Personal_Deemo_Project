using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCloseUI : MonoBehaviour
{
    private Button btn_Setting = default;
    public GameObject closeUI = default;
    public GameObject obj_Setting = default;
    public float waitForCloseTime = 1.0f;

    private void Start()
    {
        btn_Setting = obj_Setting.GetComponent<Button>();
    }

    public void CloseUI()
    {
        if (btn_Setting.enabled == false)
        {
            StartCoroutine(Delay());
        }
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitForCloseTime);
        btn_Setting.enabled = true;
        closeUI.SetActive(false);
    }
}
