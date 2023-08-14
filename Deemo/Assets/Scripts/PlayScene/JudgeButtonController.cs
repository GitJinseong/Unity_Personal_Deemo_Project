using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeButtonController : MonoBehaviour
{
    public GameObject obj_RayCastNote; // 레이캐스트용 오브젝트
    public JudgeColliderController script_RayCastNote;
    public bool isActive = false;

    private const float POS_Y = -370f; // 절대 좌표로 설정할 Y 값

    private void Awake()
    {
        script_RayCastNote = obj_RayCastNote.GetComponent<JudgeColliderController>();
    }

    public void CreateRayCastNote()
    {
        if (isActive == false && GameManager.instance.activatedJudgeColliderCount < 2)
        {
            Debug.Log("Create");

            // 오브젝트 활성화
            obj_RayCastNote.SetActive(true);

            // 오브젝트 위치 초기화
            Vector3 startPosition = new Vector3(160f, POS_Y);
            obj_RayCastNote.GetComponent<RectTransform>().anchoredPosition = startPosition;

            // 오브젝트 충돌 가능여부 초기화
            script_RayCastNote.isProcessed = false;
        }
    }
}
