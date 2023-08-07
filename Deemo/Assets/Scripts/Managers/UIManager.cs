using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public bool isOpen_SettingUI = false;

    private void Awake()
    {
        instance = this;
    }

}
