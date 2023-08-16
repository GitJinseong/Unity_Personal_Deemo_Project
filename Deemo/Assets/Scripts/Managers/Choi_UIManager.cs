using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_UIManager : MonoBehaviour
{
    public static Choi_UIManager instance;

    public bool isOpen_SettingUI = false;

    private void Awake()
    {
        instance = this;
    }

}
