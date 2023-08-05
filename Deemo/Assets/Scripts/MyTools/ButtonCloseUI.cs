using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCloseUI : MonoBehaviour
{
    public GameObject clouseUI = default;

    public void CloseUI()
    {
        clouseUI.SetActive(false);
    }
}
