using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public Vector3 finalPosition = new Vector3(0f, 22f, 30f);
    private Vector3 startPosition = new Vector3(0f, -2f, -6.2f);
    public float realPositionScale = 0.4f; // ���� ������ ����

    private bool isTouching = false; // ��ġ ���θ� �����ϴ� ����
    private Choi_CollisionDetection script_CollisionDetection;
    private Choi_Note script_Note;

    private float delayTime = 0f;
    private float judge_Charming = 1.5f;
    private float judge_Normal = 2.5f;
    private float collider_PosY;

    private bool leftTouching = false; // ���� ��ġ ���� ����
    private bool rightTouching = false; // ������ ��ġ ���� ����
    private bool leftRaycastFired = false; // ���� ����ĳ��Ʈ �߻� ����
    private bool rightRaycastFired = false; // ������ ����ĳ��Ʈ �߻� ����

    private bool isLeftTouching = false; // ���� ��ġ ���� ����
    private bool isRightTouching = false; // ������ ��ġ ���� ����

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // ��ġ �Է��� ���۵Ǿ��� ��
                if (touch.phase == TouchPhase.Began)
                {
                    // ��ġ ��ġ�� ������ ���� ��ǥ�� ��ȯ
                    Vector3 touchPosition = touch.position;
                    touchPosition.z = -Camera.main.transform.position.z; // ī�޶��� z ��ġ�� ���� ����
                    Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                    // ��ġ�� ��ġ�� �̿��Ͽ� startX ������Ʈ
                    float startX = worldTouchPosition.x * realPositionScale;

                    // ���� ��ġ�� ���� ��ġ ����
                    startPosition = new Vector3(startX, startPosition.y, startPosition.z);
                    finalPosition = new Vector3(startX, finalPosition.y, finalPosition.z);

                    // ���ʰ� ������ ��ġ ����
                    if (touch.position.x < Screen.width / 2)
                    {
                        isLeftTouching = true;
                    }
                    else
                    {
                        isRightTouching = true;
                    }

                    // ���������� ���� �׸���
                    Debug.DrawLine(startPosition, finalPosition, Color.red);

                    // Ư�� �ð� ���Ŀ� ���̸� �߻��ϵ��� Invoke ȣ��
                    FireRaycast();
                }

                // ��ġ �Է��� ����Ǿ��� ��
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
        // ���� ���� ��ġ���� ���� ��ġ������ ���� ����
        Ray ray = new Ray(startPosition, (finalPosition - startPosition).normalized);

        // ����ĳ��Ʈ ����
        RaycastHit hit;

        // ���̾� ����ũ ����
        int targetLayer = LayerMask.NameToLayer("Note"); // ��ǥ ���̾� �̸� �Է�
        int layerMask = 1 << targetLayer;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.layer == targetLayer)
            {
                Vector3 worldPosition = transform.TransformPoint(new Vector3(0, hit.collider.transform.position.y, 0));
                collider_PosY = worldPosition.y;
                float distance = hit.distance;
                Choi_GameManager.instance.ChangeTimingText(distance.ToString());
                // ���̰� �ݶ��̴��� ��Ҵ��� Ȯ�� && ��Ʈ�� ������ �Ÿ��� 8.0 ������ ��츸 ����
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
                        Debug.Log($"����ĳ��Ʈ�� ��ü�� �¾ҽ��ϴ�: {hit.collider.gameObject.name}");
                    }
                }
            }
        }
    }
}
