using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Choi_ButtonSettings : MonoBehaviour
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

    //********************************************************

    private float duration = 0.25f;

    //********************************************************

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

    public void ChangeColor(Image image_, TMP_Text text_)
    {
        // 활성화 된 상태일 경우
        if (image_.color.Equals(enableColor))
        {
            //image.color = disableColor;
            //text.color = disableColor;

            //********************************************************
            StartCoroutine(FadeDown(image_, text_));
            //********************************************************
        }
        // 비활성화 된 상태일 경우
        else
        {
            //********************************************************

            StartCoroutine(FadeUp(image_, text_));

            //********************************************************
        }
    }

    //********************************************************
    private IEnumerator FadeDown(Image image_, TMP_Text text_)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            image_.color = Color.Lerp(enableColor, disableColor, time);
            text_.color = Color.Lerp(enableColor, disableColor, time);

            yield return null;
        }
    }

    private IEnumerator FadeUp(Image image_, TMP_Text text_)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / duration);

            image_.color = Color.Lerp(disableColor, enableColor, time);
            text_.color = Color.Lerp(disableColor, enableColor, time);

            yield return null;
        }
    }
    //********************************************************
}
