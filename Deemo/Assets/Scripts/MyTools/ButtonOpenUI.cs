using UnityEngine;
using UnityEngine.UI;

public class ButtonOpenUI : MonoBehaviour
{
    public Button button; // ��ư ��ü�� �ν����Ϳ��� �Ҵ����־�� �մϴ�.
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

    // ��ư Ŭ���� �ʱ�ȭ�ϴ� �Լ�
    public void ResetButtonClick()
    {
        isButtonClicked = false;
    }
}
