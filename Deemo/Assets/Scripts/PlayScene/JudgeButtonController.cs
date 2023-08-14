using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeButtonController : MonoBehaviour
{
    public GameObject obj_RayCastNote; // ����ĳ��Ʈ�� ������Ʈ
    public JudgeColliderController script_RayCastNote;
    public bool isActive = false;

    private const float POS_Y = -370f; // ���� ��ǥ�� ������ Y ��

    private void Awake()
    {
        script_RayCastNote = obj_RayCastNote.GetComponent<JudgeColliderController>();
    }

    public void CreateRayCastNote()
    {
        if (isActive == false && GameManager.instance.activatedJudgeColliderCount < 2)
        {
            Debug.Log("Create");

            // ������Ʈ Ȱ��ȭ
            obj_RayCastNote.SetActive(true);

            // ������Ʈ ��ġ �ʱ�ȭ
            Vector3 startPosition = new Vector3(160f, POS_Y);
            obj_RayCastNote.GetComponent<RectTransform>().anchoredPosition = startPosition;

            // ������Ʈ �浹 ���ɿ��� �ʱ�ȭ
            script_RayCastNote.isProcessed = false;
        }
    }
}
