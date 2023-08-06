using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSettings : MonoBehaviour
{
    public Image img_Ranking = default;
    public Image img_KeySound = default;
    public Image img_Achivement = default;
    public Image img_NoteEffect = default;
    public Image img_JudmentControl = default;
    public Image img_CloudStorage = default;
    public Image img_ClassicUI = default;
    public TMP_Text txt_Ranking = default;
    public TMP_Text txt_KeySound = default;
    public TMP_Text txt_Achivement = default;
    public TMP_Text txt_NoteEffect = default;
    public TMP_Text txt_JudmentControl = default;
    public TMP_Text txt_CloudStorage = default;
    public TMP_Text txt_ClassicUI = default;
    private Color enableColor = new Color(0f, 0f, 0f);
    private Color disableColor = new Color(153f / 255f, 153f / 255f, 153f / 255f);

    public void OnClickRankingUI()
    {
        ChangeColor(img_Ranking, txt_Ranking);
    }

    public void OnClickKeySoundUI()
    {
        ChangeColor(img_KeySound, txt_KeySound);
    }

    public void OnClickAchivementUI()
    {
        ChangeColor(img_Achivement, txt_Achivement);
    }

    public void OnClickNoteEffectUI()
    {
        ChangeColor(img_NoteEffect, txt_NoteEffect);
    }

    public void OnClickJudmentControlUI()
    {
        ChangeColor(img_JudmentControl, txt_JudmentControl);
    }

    public void OnClickCloudStorageUI()
    {
        ChangeColor(img_CloudStorage, txt_CloudStorage);
    }

    public void OnClickClassicUIUI()
    {
        ChangeColor(img_ClassicUI, txt_ClassicUI);
    }

    public void ChangeColor(Image image, TMP_Text text)
    {
        // 활성화 된 상태일 경우
        if (image.color.Equals(enableColor))
        {
            image.color = disableColor;
            text.color = disableColor;
        }
        // 비활성화 된 상태일 경우
        else
        {
            image.color = enableColor;
            text.color = enableColor;
        }
    }
}
