using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeButtonController : MonoBehaviour
{
    public GameObject obj_JudgeCollider; // ����ĳ��Ʈ�� ������Ʈ
    public JudgeColliderController script_JudgeCollider;
    public bool isActive = false;

    private const float POS_Y = -370f; // ���� ��ǥ�� ������ Y ��

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

            // ������Ʈ Ȱ��ȭ
            obj_JudgeCollider.SetActive(true);

            //// ������Ʈ ��ġ �ʱ�ȭ
            //Vector3 startPosition = new Vector3(160f, POS_Y);
            //obj_JudgeCollider.GetComponent<RectTransform>().anchoredPosition = startPosition;

            //// ������Ʈ �浹 ���ɿ��� �ʱ�ȭ
            script_JudgeCollider.isProcessed = false;
        }
    }
}
