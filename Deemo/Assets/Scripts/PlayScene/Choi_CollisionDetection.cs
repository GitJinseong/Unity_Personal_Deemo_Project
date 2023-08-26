using System.Collections;
using UnityEngine;

public class Choi_CollisionDetection : MonoBehaviour
{
    public GameObject circleEffect;
    public GameObject obj_LightEffect;
    private Animator animator;
    private Choi_Note script_Note;
    private Choi_NoteMovement script_NoteMovement;
    public Choi_SpriteAlphaFade spriteAlphaFade;
    private SpriteRenderer spriteRenderer;
    private float hideTime = 0.1f;
    public bool isHide = false;
    public bool isJudgeHide = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        script_Note = GetComponent<Choi_Note>();
        script_NoteMovement = GetComponent<Choi_NoteMovement>();
        spriteAlphaFade = GetComponent<Choi_SpriteAlphaFade>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("JudgeLine") && isHide == false)
        {
            float hideStartTime = Time.realtimeSinceStartup; // hide ���� �ð� ���
            float noteCreationTime = script_Note.time; // ��Ʈ ���� �ð� ��������
            float spendTime = hideStartTime - noteCreationTime; // �� �ð��� ���� ���
            Debug.Log("Note Creation to Hide - Time: " + spendTime);
            //Choi_GameManager.instance.ChangeTimingText(spendTime.ToString());

            HideForMissWithJudgeLine();
        }
    }

    public void LightMove()
    {
        obj_LightEffect.transform.position = gameObject.transform.position;
    }

    public void Hide()
    {
        if (isHide == false)
        {
            isHide = true;
            animator.SetBool("Destroy", true);
            circleEffect.SetActive(true);
            script_NoteMovement.enabled = false;
            LightMove();
            obj_LightEffect.SetActive(true);
            gameObject.SetActive(true);
            StartCoroutine(DelayForHide(0.1f));
        }
    }

    public void HideForMissWithJudgeLine()
    {
        if (isHide == false && isJudgeHide == false)
        {
            isJudgeHide = true;
            gameObject.SetActive(true);
            script_NoteMovement.enabled = false;
            StartCoroutine(DelayForHideWithJudgeLine(0.3f));
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
        circleEffect.SetActive(false);
        gameObject.SetActive(false);
    }


    private void SetSpriteAlpha(float alpha)
    {
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = alpha;
        spriteRenderer.color = spriteColor;
    }

    private IEnumerator ChangeSpriteAlphaOverTime(float targetAlpha, float duration)
    {
        Debug.Log("알파 코루틴실행");
        float startAlpha = spriteRenderer.color.a / 255f; // 알파 값을 정규화
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            SetSpriteAlpha(newAlpha);
            Debug.Log($"현재알파값: {newAlpha}");
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetSpriteAlpha(targetAlpha);
    }


    private IEnumerator DelayForHideWithJudgeLine(float t)
    {
        yield return new WaitForSeconds(hideTime);
        script_NoteMovement.enabled = false;

        float startAlpha = spriteRenderer.color.a / 255f; // 알파 값을 정규화
        float targetAlpha = 0f; // 목표 알파 값
        float elapsedTime = 0f;
        float duration = 0.3f; // 알파값을 변경하는데 걸리는 시간

        while (elapsedTime < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            SetSpriteAlpha(newAlpha);
            //Debug.Log($"현재알파값: {newAlpha}");
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetSpriteAlpha(targetAlpha);

        yield return new WaitForSeconds(hideTime);

        if (isHide == false)
        {
            Choi_GameManager.instance.AddMiss();
        }

        isHide = false;
        isJudgeHide = false;
        script_NoteMovement.enabled = true;
        circleEffect.SetActive(false);
        gameObject.SetActive(false);
    }


    private IEnumerator StopObjectMovement(float t)
    {
        yield return new WaitForSeconds(t);
        script_NoteMovement.enabled = false;
    }
}
