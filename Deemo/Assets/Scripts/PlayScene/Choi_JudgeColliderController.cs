using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_JudgeColliderController : MonoBehaviour
{
    public GameObject obj_Parent;
    public Choi_JudgeButtonController script_Parent;
    public Choi_CollisionDetection script_collision;

    private float normalStart = 2f;
    private float charmingStart = 3f;

    private HashSet<Collider2D> processedColliders = new HashSet<Collider2D>();

    private RectTransform rectTransform;

    public bool isProcessed = false; // 중복 처리를 방지하기 위한 플래그
    private bool isAnimationRunning = false; // 추가된 변수

    private void Awake()
    {
        script_Parent = obj_Parent.GetComponent<Choi_JudgeButtonController>();
        rectTransform = GetComponent<RectTransform>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isProcessed) return;

        if (collision.CompareTag("Note"))
        {
            script_collision = collision.GetComponent<Choi_CollisionDetection>();

            if (script_collision.isHide == false)
            {
                if (!processedColliders.Contains(collision))
                {
                    processedColliders.Add(collision);
                    ProcessColliders();
                    isProcessed = true;
                }
            }
            else
            {
                Debug.Log("중복충돌");
            }
        }

    }

    private void ProcessColliders()
    {
        List<Collider2D> colliders = new List<Collider2D>(processedColliders);

        colliders.Sort((a, b) =>
        {
            Vector3 notePositionA = a.transform.localPosition;
            Vector3 notePositionB = b.transform.localPosition;

            float distanceA = Mathf.Abs(notePositionA.y);
            float distanceB = Mathf.Abs(notePositionB.y);

            return distanceA.CompareTo(distanceB); // distanceA가 더 작은 경우에 음수 값을 리턴하도록 변경합니다.
        });

        if (colliders.Count > 0)
        {
            foreach (Collider2D col in colliders)
            {
                Vector3 notePosition = col.transform.localPosition;
                float distance = Mathf.Abs(notePosition.y);
                Debug.Log(distance);

                if (distance >= charmingStart)
                {
                    Debug.Log("Note: CHARMING!");
                    Choi_GameManager.instance.AddCombo();
                    Choi_GameManager.instance.ChangeJudgeText("CHARMING!");
                }
                else if (distance >= normalStart)
                {
                    Debug.Log("Note: NORMAL!");
                    Choi_GameManager.instance.AddCombo();
                    Choi_GameManager.instance.ChangeJudgeText("NORMAL!");
                }
                else
                {
                    Debug.Log("Note: MISS!");
                    Choi_GameManager.instance.ResetCombo();
                    Choi_GameManager.instance.ChangeJudgeText("MISS!");
                }

                SetActiveFalseObjects(col.gameObject);
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
        Choi_CollisionDetection script_Note = obj_Note.GetComponent<Choi_CollisionDetection>();
        script_Note.Hide();
        //script_Parent.isActive = false;
        gameObject.SetActive(false);
    }
}
