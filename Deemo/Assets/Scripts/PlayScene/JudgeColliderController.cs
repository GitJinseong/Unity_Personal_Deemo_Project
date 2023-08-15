using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeColliderController : MonoBehaviour
{
    public GameObject obj_Parent;
    public JudgeButtonController script_Parent;
    public CollisionDetection script_collision;

    public float normalStart = 177.45f;
    public float normalEnd = 175.59f;
    public float charmingStart = 176.73f;
    public float charmingEnd = 176.25f;
    private float converPositionY = 180f;

    private HashSet<Collider2D> processedColliders = new HashSet<Collider2D>();

    private RectTransform rectTransform;

    public bool isProcessed = false; // �ߺ� ó���� �����ϱ� ���� �÷���

    private void Awake()
    {
        script_Parent = obj_Parent.GetComponent<JudgeButtonController>();
        rectTransform = GetComponent<RectTransform>();
    }

    //private void Update()
    //{
    //    // ���� ������Ʈ�� Y �������� Ȯ��
    //    float currentPosY = rectTransform.anchoredPosition.y;

    //    // Y �������� 340 �̻��̸� ������Ʈ�� ��Ȱ��ȭ
    //    if (currentPosY >= 340f)
    //    {
    //        Debug.Log("�浹ó��������");
    //        gameObject.SetActive(false);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isProcessed) return;

        if (collision.CompareTag("Note"))
        {
            script_collision = collision.GetComponent<CollisionDetection>();

            if (script_collision.isHide == false)
            {
                if (!processedColliders.Contains(collision))
                {
                    processedColliders.Add(collision);

                    List<Collider2D> colliders = new List<Collider2D>(processedColliders);

                    colliders.Sort((a, b) =>
                    {
                        Vector3 notePositionA = a.transform.localPosition;
                        Vector3 notePositionB = b.transform.localPosition;

                        float distanceA = Mathf.Abs(notePositionA.y - rectTransform.anchoredPosition.y);
                        float distanceB = Mathf.Abs(notePositionB.y - rectTransform.anchoredPosition.y);

                        return distanceA.CompareTo(distanceB); // �� �κ��� �ٽ� ������� �����մϴ�.
                    });

                    if (colliders.Count > 0)
                    {
                        Debug.Log(colliders.Count);

                        Vector3 notePosition = colliders[0].transform.localPosition;
                        float distance = Mathf.Abs(notePosition.y - rectTransform.anchoredPosition.y);
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

                        SetActiveFalseObjects(colliders[0].gameObject);
                    }
                    isProcessed = true;
                }
            }
            else
            {
                Debug.Log("�ߺ��浹");
            }
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
        script_Parent.isActive = false;
        gameObject.SetActive(false);
    }

    private bool IsDistanceInRange(float distance, float start, float end)
    {
        return distance <= start && distance >= end;
    }

}
