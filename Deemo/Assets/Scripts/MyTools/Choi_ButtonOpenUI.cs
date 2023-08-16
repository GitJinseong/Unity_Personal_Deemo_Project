using UnityEngine;
using UnityEngine.UI;

public class Choi_ButtonOpenUI : MonoBehaviour
{
    public GameObject openUI = default;

    public void OpenUI()
    {
        if (openUI.active == true)
        {
            return;
        }
        openUI.SetActive(true);
    }
}
