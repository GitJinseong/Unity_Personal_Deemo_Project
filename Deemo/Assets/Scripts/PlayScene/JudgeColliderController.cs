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

    public bool isProcessed = false; // 중복 처리를 방지하기 위한 플래그

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // 현재 오브젝트의 Y 포지션을 확인
        float currentPosY = rectTransform.anchoredPosition.y;

        // Y 포지션이 340 이상이면 오브젝트를 비활성화
        if (currentPosY >= 340f)
        {
            Debug.Log("충돌처리무시함");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isProcessed) return; // 이미 처리되었으면 더 이상 진행하지 않음

        Debug.Log("충돌충돌");
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

            isProcessed = true; // 처리 완료 플래그 설정
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
        // 레이와 충돌한 객체에 대한 처리를 수행
        // hitObject가 노트일 경우에는 해당 노트에 대한 처리를 수행
        Debug.Log("Hit object: " + hitObject.name);
        // 이 부분에 레이와 충돌한 객체에 대한 처리 코드를 작성
    }
}
