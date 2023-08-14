using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeButtonController : MonoBehaviour
{
    public GameObject JudgeColliders;
    public bool isActive = false;
    private const float INACTIVE_DELAY = 0.1f; 

    public void SetActiveColliders()
    {
        if (isActive == false && GameManager.instance.activatedJudgeColliderCount < 2)
        {
            isActive = true;
            GameManager.instance.activatedJudgeColliderCount += 1;
            JudgeColliders.SetActive(true);
            StartCoroutine(DelayForInActiveColliders());
        }
    }

    private IEnumerator DelayForInActiveColliders()
    {
        yield return new WaitForSeconds(INACTIVE_DELAY);
        GameManager.instance.activatedJudgeColliderCount -= 1;
        JudgeColliders.SetActive(false);
        isActive = false;   
    }
}
