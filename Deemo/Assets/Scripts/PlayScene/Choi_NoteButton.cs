using TMPro;
using UnityEngine;

public class Choi_NoteButton : MonoBehaviour
{
    private Choi_CollisionDetection script_CollisionDetection;
    private float judge_Charming = -1.2f;
    private float judge_Normal = -1.4f;

    private bool isPressed = false; // 노트가 눌렸는지 여부를 저장하는 변수

    private void Awake()
    {
        script_CollisionDetection = GetComponent<Choi_CollisionDetection>();
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, 10); // z값을 화면에 가까운 값으로 설정
                touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                Debug.Log(touchPosition);
                Collider collider = GetComponent<Collider>();

                // 터치 입력 위치와 노트의 클릭 영역 비교
                if (!isPressed && script_CollisionDetection.isHide == false && collider.bounds.Contains(touchPosition))
                {
                    HandleButtonPress();
                }
            }
        }
    }

    private void HandleButtonPress()
    {
        isPressed = true; // 노트가 눌렸음을 표시

        Debug.Log($"클릭한 노트의 y좌표 : {transform.position.y}");
        if (transform.position.y < judge_Charming)
        {
            Choi_GameManager.instance.AddCombo();
            Choi_GameManager.instance.ChangeJudgeText("CHARMING!");
        }
        else if (transform.position.y < judge_Normal)
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
    }
}
