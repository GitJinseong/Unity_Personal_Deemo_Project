using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JudgeButtonController : MonoBehaviour, IPointerDownHandler
{
    public GameObject obj_JudgeCollider; // ������ �ݶ��̴�
    public JudgeColliderController script_JudgeCollider;
    public RectTransform rectTransform_JudgeCollider;
    public bool isActive = false;

    private const float POS_Y = -180f; // ���� ��ǥ�� ������ Y ��
    private float delay = 0.1f; // �ݶ��̴��� �ڵ����� ������ �ð�
    private Vector3 touchPosition;

    private void Awake()
    {
        script_JudgeCollider = obj_JudgeCollider.GetComponent<JudgeColliderController>();
        rectTransform_JudgeCollider = obj_JudgeCollider.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchPosition = eventData.position;
        SetActiveTrue();
    }

    public void SetActiveTrue()
    {
        if (isActive == false && GameManager.instance.activatedJudgeColliderCount < 2)
        {
            isActive = true;

            // ������Ʈ Ȱ��ȭ
            obj_JudgeCollider.SetActive(true);

            // ������Ʈ �浹 ���ɿ��� �ʱ�ȭ
            script_JudgeCollider.isProcessed = false;

            // ��ġ�� ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, POS_Y, 0f));

            // �ݶ��̴��� X ��ǥ�� ī�޶� �þ� ���� ���� ����
            float clampedX = Mathf.Clamp(worldPosition.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x);

            // �ݶ��̴��� ��ġ ���� (x��ǥ�� ���� ��ǥ, y ��ǥ�� POS_Y, z ��ǥ�� 0f�� ����)
            obj_JudgeCollider.transform.position = new Vector3(clampedX, 0f, 0f);
            // �Ʒ��� y��, z��ǥ�� ��� ��ǥ�� ����
            rectTransform_JudgeCollider.anchoredPosition = new Vector3(rectTransform_JudgeCollider.anchoredPosition.x, POS_Y, 0f);
            

            StartCoroutine(DelayForActiveFalse());
        }
    }

    private IEnumerator DelayForActiveFalse()
    {
        yield return new WaitForSeconds(delay);

        obj_JudgeCollider.SetActive(false);
        isActive = false;
    }
}
