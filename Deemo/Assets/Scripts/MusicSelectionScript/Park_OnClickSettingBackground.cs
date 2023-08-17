using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_OnClickSettingBackground : MonoBehaviour
{
    public Park_OnClickSettingUi onClickSettingUi;

    // 코루틴이 진행중인지 확인
    public bool isCoroutine = false;

    // 버튼이 눌리고 있는지 확인
    private bool isPressed = false;

    // OnMouseUp 인지 확인
    public bool isCheck = false;

    private void OnMouseDown()
    {
        if (onClickSettingUi.isCoroutine == false)
        {
            isPressed = true;
        }
    }
    private void OnMouseUp()
    {
        if (isPressed == true)
        {
            isPressed = false;

            isCheck = true;
        }
    }
}
