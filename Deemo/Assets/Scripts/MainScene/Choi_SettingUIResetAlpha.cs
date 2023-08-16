using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choi_SettingUIResetAlpha : MonoBehaviour
{
    public Image img_Panel;
    public Image img_Bg;
    private float panelOriginalAlpha = 120f / 255f;
    private float bgOriginalAlpha = 255f / 255f;

    public void ResetAlpha()
    {
        // 이미지들의 알파값을 원래 값으로 되돌립니다.
        Color panelColor = img_Panel.color;
        panelColor.a = panelOriginalAlpha;
        img_Panel.color = panelColor;

        Color bgColor = img_Bg.color;
        bgColor.a = bgOriginalAlpha;
        img_Bg.color = bgColor;
    }
}
