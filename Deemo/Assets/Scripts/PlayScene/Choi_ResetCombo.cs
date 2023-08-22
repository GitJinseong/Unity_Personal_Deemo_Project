using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_ResetCombo : MonoBehaviour
{
    void Start()
    {
        Choi_GameManager.instance.RemoveHistory();
    }
}
