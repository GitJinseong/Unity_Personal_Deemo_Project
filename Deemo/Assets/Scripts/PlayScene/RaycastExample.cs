using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public Vector3 finalPosition = new Vector3(0f, 22f, 30f);
    private Vector3 startPosition = new Vector3(0f, -2f, -6.2f);
    public float realPositionScale = 0.4f; // 실제 포지션 배율

    private bool isTouching = false; // 터치 여부를 저장하는 변수
    private Choi_CollisionDetection script_CollisionDetection;
    private Choi_Note script_Note;

    private float delayTime = 0f;
    private float judge_Charming = 1.5f;
    private float judge_Normal = 2.5f;
    private float collider_PosY;

    private bool leftTouching = false; // 왼쪽 터치 여부 저장
    private bool rightTouching = false; // 오른쪽 터치 여부 저장
    private bool leftRaycastFired = false; // 왼쪽 레이캐스트 발사 여부
    private bool rightRaycastFired = false; // 오른쪽 레이캐스트 발사 여부

    private bool isLeftTouching = false; // 왼쪽 터치 여부 저장
    private bool isRightTouching = false; // 오른쪽 터치 여부 저장

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // 터치 입력이 시작되었을 때
                if (touch.phase == TouchPhase.Began)
                {
                    // 터치 위치를 가져와 월드 좌표로 변환
                    Vector3 touchPosition = touch.position;
                    touchPosition.z = -Camera.main.transform.position.z; // 카메라의 z 위치에 대한 보정
                    Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                    // 터치한 위치를 이용하여 startX 업데이트
                    float startX = worldTouchPosition.x * realPositionScale;

                    // 시작 위치와 최종 위치 설정
                    startPosition = new Vector3(startX, startPosition.y, startPosition.z);
                    finalPosition = new Vector3(startX, finalPosition.y, finalPosition.z);

                    // 왼쪽과 오른쪽 터치 구분
                    if (touch.position.x < Screen.width / 2)
                    {
                        isLeftTouching = true;
                    }
                    else
                    {
                        isRightTouching = true;
                    }

                    // 디버깅용으로 레이 그리기
                    Debug.DrawLine(startPosition, finalPosition, Color.red);

                    // 특정 시간 이후에 레이를 발사하도록 Invoke 호출
                    FireRaycast();
                }

                // 터치 입력이 종료되었을 때
                if (touch.phase == TouchPhase.Ended)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        isLeftTouching = false;
                    }
                    else
                    {
                        isRightTouching = false;
                    }
                }
            }
        }
    }



    private void FireRaycast()
    {
        // 레이 시작 위치에서 최종 위치까지의 레이 생성
        Ray ray = new Ray(startPosition, (finalPosition - startPosition).normalized);

        // 레이캐스트 수행
        RaycastHit hit;

        // 레이어 마스크 설정
        int targetLayer = LayerMask.NameToLayer("Note"); // 목표 레이어 이름 입력
        int layerMask = 1 << targetLayer;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.layer == targetLayer)
            {
                Vector3 worldPosition = transform.TransformPoint(new Vector3(0, hit.collider.transform.position.y, 0));
                collider_PosY = worldPosition.y;
                float distance = hit.distance;
                Choi_GameManager.instance.ChangeTimingText(distance.ToString());
                // 레이가 콜라이더에 닿았는지 확인 && 노트와 레이의 거리가 8.0 이하일 경우만 동작
                if (hit.collider != null && distance < 8.0f)
                {
                    script_CollisionDetection = hit.collider.GetComponent<Choi_CollisionDetection>();
                    script_Note = hit.collider.GetComponent<Choi_Note>();
                    if (script_CollisionDetection.isHide == false)
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
