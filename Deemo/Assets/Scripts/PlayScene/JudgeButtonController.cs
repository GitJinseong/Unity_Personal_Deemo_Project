using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeButtonController : MonoBehaviour
{
    public GameObject obj_JudgeCollider; // ����ĳ��Ʈ�� ������Ʈ
    public JudgeColliderController script_JudgeCollider;
    public bool isActive = false;

    private const float POS_Y = -370f; // ���� ��ǥ�� ������ Y ��
    private float delay = 0.1f; // �ݶ��̴��� �ڵ����� ������ �ð�

    private void Awake()
    {
        script_JudgeCollider = obj_JudgeCollider.GetComponent<JudgeColliderController>();
    }

    public void SetActiveTrue()
    {
        if (isActive == false && GameManager.instance.activatedJudgeColliderCount < 2)
        {
            isActive = true;

            // ������Ʈ Ȱ��ȭ
            obj_JudgeCollider.SetActive(true);

            //// ������Ʈ �浹 ���ɿ��� �ʱ�ȭ
            script_JudgeCollider.isProcessed = false;

            StartCoroutine(DelayForActiveFalse());
        }
    }

    private IEnumerator DelayForActiveFalse()
    {
        yield return new WaitForSeconds(delay);

        obj_JudgeCollider.SetActive(false);
        isActive = false;
    }
}
