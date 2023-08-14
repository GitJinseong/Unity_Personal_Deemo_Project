using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeColliderController : MonoBehaviour
{
    public float normalStart = 177.45f;
    public float normalEnd = 175.59f;
    public float charmingStart = 176.73f;
    public float charmingEnd = 176.25f;
    private float converPositionY = 180f;

    private HashSet<Collider2D> processedColliders = new HashSet<Collider2D>();

    private RectTransform rectTransform;

    public bool isProcessed = false; // �ߺ� ó���� �����ϱ� ���� �÷���

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // ���� ������Ʈ�� Y �������� Ȯ��
        float currentPosY = rectTransform.anchoredPosition.y;

        // Y �������� 340 �̻��̸� ������Ʈ�� ��Ȱ��ȭ
        if (currentPosY >= 340f)
        {
            Debug.Log("�浹ó��������");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isProcessed) return; // �̹� ó���Ǿ����� �� �̻� �������� ����

        Debug.Log("�浹�浹");
        if (collision.gameObject.CompareTag("TopLine"))
        {
            gameObject.SetActive(false);
            return;
        }

        if (!processedColliders.Contains(collision))
        {
            processedColliders.Add(collision);

            Vector3 notePosition = collision.transform.localPosition;
            float distance = Mathf.Abs(notePosition.y - converPositionY);
            Debug.Log(distance);
            if (IsDistanceInRange(distance, charmingStart, charmingEnd))
            {
                Debug.Log("Note: CHARMING!");
            }
            else if (IsDistanceInRange(distance, normalStart, normalEnd))
            {
                Debug.Log("Note: NORMAL!");
            }
            else
            {
                Debug.Log("Note: MISS!");
            }

            SetActiveFalseObjects(collision.gameObject);

            isProcessed = true; // ó�� �Ϸ� �÷��� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (processedColliders.Contains(collision))
        {
            processedColliders.Remove(collision);
        }
    }

    private void SetActiveFalseObjects(GameObject obj_Note)
    {
        CollisionDetection script_Note = obj_Note.GetComponent<CollisionDetection>();
        script_Note.Hide();
        gameObject.SetActive(false);
    }

    private bool IsDistanceInRange(float distance, float start, float end)
    {
        return distance <= start && distance >= end;
    }

    public void ProcessRaycastHit(GameObject hitObject)
    {
        // ���̿� �浹�� ��ü�� ���� ó���� ����
        // hitObject�� ��Ʈ�� ��쿡�� �ش� ��Ʈ�� ���� ó���� ����
        Debug.Log("Hit object: " + hitObject.name);
        // �� �κп� ���̿� �浹�� ��ü�� ���� ó�� �ڵ带 �ۼ�
    }
}
