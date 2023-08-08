using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayRaycastCloseButton : MonoBehaviour
{
    public GameObject obj_CloseBtn = default;
    public float delay = 1.2f;
    private Image image = default;

    private void Start()
    {
        image = obj_CloseBtn.GetComponent<Image>();
    }
    public void VisibleCloseBtn()
    {
        StartCoroutine(DelayForRun());
    }

    public IEnumerator DelayForRun()
    {
        yield return new WaitForSeconds(delay);
        image.raycastTarget = true;
    }
}
