using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenUI : MonoBehaviour
{
    public GameObject openUI = default;

    public void OpenUI()
    {
        openUI.SetActive(true);
    }
}
