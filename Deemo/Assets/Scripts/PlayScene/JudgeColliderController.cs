using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeColliderController : MonoBehaviour
{
    public GameObject obj_Parent;
    public JudgeButtonController script_Parent;
    public CollisionDetection script_collision;

    public float normalStart = 177.45f;
    public float charmingStart = 176.73f;
    private float converPositionY = 180f;

    private HashSet<Collider2D> processedColliders = new HashSet<Collider2D>();

    private RectTransform rectTransform;

    public bool isProcessed = false; // 중복 처리를 방지하기 위한 플래그

    private void Awake()
    {
        script_Parent = obj_Parent.GetComponent<JudgeButtonController>();
        rectTransform = GetComponent<RectTransform>();
    }

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

                        return distanceA.CompareTo(distanceB); // 이 부분을 다시 원래대로 변경합니다.
                    });

                    if (colliders.Count > 0)
                    {
                        Debug.Log(colliders.Count);

                        Vector3 notePosition = colliders[0].transform.localPosition;
                        float distance = Mathf.Abs(notePosition.y - rectTransform.anchoredPosition.y);
                        Debug.Log(distance);

                        if (distance <= charmingStart)
                        {
                            Debug.Log("Note: CHARMING!");
                            GameManager.instance.ChangeJudgeText("CHARMING!");
                        }
                        else if (distance <= normalStart)
                        {
                            Debug.Log("Note: NORMAL!");
                            GameManager.instance.ChangeJudgeText("NORMAL!");
                        }
                        else
                        {
                            Debug.Log("Note: MISS!");
                            GameManager.instance.ChangeJudgeText("MISS!");
                        }

                        SetActiveFalseObjects(colliders[0].gameObject);
                    }
                    isProcessed = true;
                }
            }
            else
            {
                Debug.Log("중복충돌");
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
}
