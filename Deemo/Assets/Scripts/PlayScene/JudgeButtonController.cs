using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeButtonController : MonoBehaviour
{
    public GameObject obj_JudgeCollider; // 레이캐스트용 오브젝트
    public JudgeColliderController script_JudgeCollider;
    public bool isActive = false;

    private const float POS_Y = -370f; // 절대 좌표로 설정할 Y 값

    private void Awake()
    {
        script_JudgeCollider = obj_JudgeCollider.GetComponent<JudgeColliderController>();
    }

    public void SetActiveTrue()
    {
        if (isActive == false && GameManager.instance.activatedJudgeColliderCount < 2)
        {
            isActive = true;
            //Debug.Log("Create");

            // 오브젝트 활성화
            obj_JudgeCollider.SetActive(true);

            //// 오브젝트 위치 초기화
            //Vector3 startPosition = new Vector3(160f, POS_Y);
            //obj_JudgeCollider.GetComponent<RectTransform>().anchoredPosition = startPosition;

            //// 오브젝트 충돌 가능여부 초기화
            script_JudgeCollider.isProcessed = false;
        }
    }
}
