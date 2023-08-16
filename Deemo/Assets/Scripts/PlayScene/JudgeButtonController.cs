using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JudgeButtonController : MonoBehaviour, IPointerDownHandler
{
    public GameObject obj_JudgeCollider; // ������ �ݶ��̴�
    public JudgeColliderController script_JudgeCollider;
    public RectTransform rectTransform_JudgeCollider;
    public BoxCollider2D collider_JudgeCollider;
    public bool isActive = false;

    private const float POS_Y = -240f; // ���� ��ǥ�� ������ Y ��
    private const float INITIAL_HEIGHT = 20f; // �ݶ��̴��� ���� ����
    private const float FINAL_HEIGHT = 600f; // �ݶ��̴��� ���� ����
    private const float ANIMATION_DURATION = 0.05f; // �ִϸ��̼� �ð�
    private float delay = 0.05f; // �ݶ��̴��� �ڵ����� ������ �ð�
    private Vector3 touchPosition;

    private void Awake()
    {
        script_JudgeCollider = obj_JudgeCollider.GetComponent<JudgeColliderController>();
        rectTransform_JudgeCollider = obj_JudgeCollider.GetComponent<RectTransform>();
        collider_JudgeCollider = obj_JudgeCollider.GetComponent<BoxCollider2D>(); // BoxCollider2D �ʱ�ȭ �߰�
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

            // �ݶ��̴��� ��ġ ���� (x��ǥ�� ���� ��ǥ, y ��ǥ�� POS_Y�� ����, z ��ǥ�� 0f�� ����)
            obj_JudgeCollider.transform.position = new Vector3(clampedX, POS_Y, 0f);
            // �Ʒ��� y��, z��ǥ�� ��� ��ǥ�� ����
            rectTransform_JudgeCollider.anchoredPosition = new Vector3(rectTransform_JudgeCollider.anchoredPosition.x, POS_Y, 0f);

            // ���� �ִϸ��̼� ����
            StartCoroutine(AnimateColliderHeight());

            StartCoroutine(DelayForActiveFalse());
        }
    }

    private IEnumerator AnimateColliderHeight()
    {
        float elapsedTime = 0f;
        while (elapsedTime < ANIMATION_DURATION)
        {
            float normalizedTime = elapsedTime / ANIMATION_DURATION;
            float currentHeight = Mathf.Lerp(INITIAL_HEIGHT, FINAL_HEIGHT, normalizedTime);

            rectTransform_JudgeCollider.sizeDelta = new Vector2(rectTransform_JudgeCollider.sizeDelta.x, currentHeight);
            collider_JudgeCollider.size = new Vector2(collider_JudgeCollider.size.x, currentHeight); // BoxCollider2D ���� ����

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform_JudgeCollider.sizeDelta = new Vector2(rectTransform_JudgeCollider.sizeDelta.x, FINAL_HEIGHT);
        collider_JudgeCollider.size = new Vector2(collider_JudgeCollider.size.x, FINAL_HEIGHT); // BoxCollider2D ���� ���� ����
    }

    private IEnumerator DelayForActiveFalse()
    {
        yield return new WaitForSeconds(delay);

        obj_JudgeCollider.SetActive(false);
        isActive = false;
    }
}
