using UnityEngine;
using UnityEngine.UI;

public class ButtonOpenUI : MonoBehaviour
{
    public Button button; // 버튼 객체를 인스펙터에서 할당해주어야 합니다.
    public GameObject openUI = default;
    private bool isButtonClicked = false;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void OpenUI()
    {
        openUI.SetActive(true);
    }

    private void OnButtonClick()
    {
        if (!isButtonClicked)
        {
            isButtonClicked = true;
            OpenUI();
        }
    }

    // 버튼 클릭을 초기화하는 함수
    public void ResetButtonClick()
    {
        isButtonClicked = false;
    }
}
