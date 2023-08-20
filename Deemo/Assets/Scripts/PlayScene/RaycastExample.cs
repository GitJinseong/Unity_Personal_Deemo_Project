using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public Vector3 finalPosition = new Vector3(0f, 22f, 30f);
    private Vector3 startPosition = new Vector3(0f, -2f, -6.2f);
    public float realPositionScale = 0.4f; // 실제 포지션 배율

    private float delayTime = 0f;
    private float judge_Charming = 1.5f;
    private float judge_Normal = 2.5f;

    private int layerMask; // 레이어 마스크

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Note"); // 목표 레이어 이름 입력
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치만 처리

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = touch.position;
                touchPosition.z = -Camera.main.transform.position.z;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                float startX = worldTouchPosition.x * realPositionScale;

                startPosition = new Vector3(startX, startPosition.y, startPosition.z);
                finalPosition = new Vector3(startX, finalPosition.y, finalPosition.z);

                Debug.DrawLine(startPosition, finalPosition, Color.red);

                FireRaycast();
            }
        }
    }

    private void FireRaycast()
    {
        Vector3 direction = (finalPosition - startPosition).normalized;
        Ray ray = new Ray(startPosition, direction);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Note"))
            {
                float distance = hit.distance;
                Choi_GameManager.instance.ChangeTimingText(distance.ToString());

                if (distance < 8.0f)
                {
                    Choi_CollisionDetection script_CollisionDetection = hit.collider.GetComponent<Choi_CollisionDetection>();
                    if (script_CollisionDetection != null && !script_CollisionDetection.isHide)
                    {
                        if (distance <= judge_Charming)
                        {
                            Choi_GameManager.instance.AddCombo();
                            Choi_GameManager.instance.ChangeJudgeText("CHARMING!");
                        }
                        else if (distance <= judge_Normal)
                        {
                            Choi_GameManager.instance.AddCombo();
                            Choi_GameManager.instance.ChangeJudgeText("NORMAL!");
                        }
                        else
                        {
                            Choi_GameManager.instance.ResetCombo();
                            Choi_GameManager.instance.ChangeJudgeText("MISS!");
                        }
                        script_CollisionDetection.Hide();
                        Debug.Log($"레이캐스트가 객체에 맞았습니다: {hit.collider.gameObject.name}");
                    }
                }
            }
        }
    }
}
