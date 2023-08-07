using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCloseUI : MonoBehaviour
{
    private Image img_Btn = default;
    public GameObject clouseUI = default;
    public float waitForCloseTime = 1.0f;

    private void Start()
    {
        img_Btn = GetComponent<Image>();
    }

    public void CloseUI()
    {
        img_Btn.raycastTarget = false;
        StartCoroutine(Delay());
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitForCloseTime);
        img_Btn.raycastTarget = true;
        clouseUI.SetActive(false);
    }
}
