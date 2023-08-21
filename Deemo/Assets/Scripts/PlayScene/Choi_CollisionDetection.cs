using System.Collections;
using UnityEngine;

public class Choi_CollisionDetection : MonoBehaviour
{
    private Rigidbody rigid;
    private Animator animator;
    private Choi_Note script_Note;
    private Choi_NoteMovement script_NoteMovement;
    private Choi_SpriteAlphaFade spriteAlphaFade;
    private float hideTime = 0.1f;
    public bool isHide = false;
    public bool isJudgeHide = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        script_Note = GetComponent<Choi_Note>();
        script_NoteMovement = GetComponent<Choi_NoteMovement>();
        spriteAlphaFade = GetComponent<Choi_SpriteAlphaFade>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Normal"))
        {
            script_Note.stringJudge = "Normal";
        }
        else if (collision.gameObject.CompareTag("Charming"))
        {
            script_Note.stringJudge = "Charming";
        }

        if (collision.gameObject.CompareTag("JudgeLine") && isHide == false)
        {
            float hideStartTime = Time.realtimeSinceStartup; // hide 시작 시간 기록
            float noteCreationTime = script_Note.time; // 노트 생성 시간 가져오기
            float spendTime = hideStartTime - noteCreationTime; // 두 시간의 차이 계산
            Debug.Log("Note Creation to Hide - Time: " + spendTime);
            //Choi_GameManager.instance.ChangeTimingText(spendTime.ToString());

            HideForMissWithJudgeLine();
        }
    }

    public void Hide()
    {
        if (isHide == false)
        {
            isHide = true;
            animator.SetBool("Destroy", true);
            script_NoteMovement.enabled = false;
            gameObject.SetActive(true);
            StartCoroutine(DelayForHide(0.1f));
        }
    }

    public void HideForMissWithJudgeLine()
    {
        if (isHide == false && isJudgeHide == false)
        {
            isJudgeHide = true;
            //animator.SetBool("Destroy", true);
            gameObject.SetActive(true);
            script_NoteMovement.enabled = false;
            StartCoroutine(DelayForHideWithJudgeLine(0.3f));
            spriteAlphaFade.StartFadeOut();
        }
    }

    private IEnumerator DelayForHide(float t)
    {
        yield return new WaitForSeconds(hideTime);
        StartCoroutine(StopObjectMovement(t));
        yield return new WaitForSeconds(hideTime);
        script_NoteMovement.enabled = true;
        isHide = false;
        isJudgeHide = false;
        gameObject.SetActive(false);
    }

    private IEnumerator DelayForHideWithJudgeLine(float t)
    {
        yield return new WaitForSeconds(hideTime);
        script_NoteMovement.enabled = false;
        yield return new WaitForSeconds(hideTime);
        if (isHide == false)
        {
            Choi_GameManager.instance.AddMiss();
        }
        isHide = false;
        isJudgeHide = false;
        script_NoteMovement.enabled = true;
        gameObject.SetActive(false);
    }
    private IEnumerator StopObjectMovement(float t)
    {
        yield return new WaitForSeconds(t);
        script_NoteMovement.enabled = false;
    }
}
