using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public Vector3 finalPosition = new Vector3(0f, 22f, 30f);
    private Vector3 leftStartPosition = new Vector3(-4f, -4f, -8.2f);
    private Vector3 rightStartPosition = new Vector3(4f, -4f, -8.2f);
    public float realPositionScale = 0.4f; // 실제 포지션 배율

    private float judge_Charming = 3.8f;
    private float judge_Normal = 4.5f;

    private int layerMask; // 레이어 마스크

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Note"); // 목표 레이어 이름 입력
    }

    private void Update()
    {
        // 왼쪽 손가락 누름 처리
        if (Input.touchCount > 0)
        {
            Touch leftTouch = Input.GetTouch(0);

            if (leftTouch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = leftTouch.position;
                touchPosition.z = -Camera.main.transform.position.z;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                float startX = worldTouchPosition.x * realPositionScale;

                leftStartPosition = new Vector3(startX, leftStartPosition.y, leftStartPosition.z);
                finalPosition = new Vector3(startX, finalPosition.y, finalPosition.z);

                Debug.DrawLine(leftStartPosition, finalPosition, Color.red);

                FireRaycast(leftStartPosition, finalPosition);
            }
        }

        // 오른쪽 손가락 누름 처리
        if (Input.touchCount > 1)
        {
            Touch rightTouch = Input.GetTouch(1);

            if (rightTouch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = rightTouch.position;
                touchPosition.z = -Camera.main.transform.position.z;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                float startX = worldTouchPosition.x * realPositionScale;

                rightStartPosition = new Vector3(startX, rightStartPosition.y, rightStartPosition.z);
                finalPosition = new Vector3(startX, finalPosition.y, finalPosition.z);

                Debug.DrawLine(rightStartPosition, finalPosition, Color.green);

                FireRaycast(rightStartPosition, finalPosition);
            }
        }
    }

    private void FireRaycast(Vector3 startPosition, Vector3 finalPosition)
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
