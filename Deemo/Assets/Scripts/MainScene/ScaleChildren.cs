using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleChildren : MonoBehaviour
{
    public float scaleMultiplier = 1.1f; // 크기 변환 시의 배율

    private Image[] childImages; // 자식 이미지 컴포넌트 배열
    private TMP_Text[] childTexts; // 자식 TMP 텍스트 컴포넌트 배열

    // Start is called before the first frame update
    void Start()
    {
        // 이미지와 TMP 텍스트의 크기를 변환하는 함수를 호출합니다.
       // ScaleChildrenComponents();
    }

    // 이미지 크기 변환 함수
    private void ScaleImage(Image image)
    {
        Vector3 originalScale = image.rectTransform.localScale;
        Vector3 newScale = originalScale * scaleMultiplier;
        image.rectTransform.localScale = newScale;
    }

    // TMP 텍스트 크기 변환 함수
    private void ScaleTMPText(TMP_Text text)
    {
        Vector3 originalScale = text.rectTransform.localScale;
        Vector3 newScale = originalScale * scaleMultiplier;
        text.rectTransform.localScale = newScale;
    }

    // 이미지와 TMP 텍스트의 크기를 변환하는 함수
    public void ScaleChildrenComponents()
    {
        // 이미지 컴포넌트를 찾아서 크기 변환을 적용합니다.
        childImages = GetComponentsInChildren<Image>();
        foreach (Image image in childImages)
        {
            ScaleImage(image);
        }

        // 이미지의 자식 오브젝트에서 TMP 텍스트 컴포넌트를 찾아서 크기 변환을 적용합니다.
        childTexts = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in childTexts)
        {
            ScaleTMPText(text);
        }
    }
}
