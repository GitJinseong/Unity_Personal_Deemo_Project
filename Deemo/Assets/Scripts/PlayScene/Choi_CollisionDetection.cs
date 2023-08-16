using System.Collections;
using UnityEngine;

public class Choi_CollisionDetection : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private Animator animator;
    private Choi_Note script_Note;
    private float hideTime = 1f;
    public bool isHide = false;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        script_Note = GetComponent<Choi_Note>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("JudgeLine") && isHide == false)
        {
            float timeSinceStart = Time.time; // 현재 게임 시작으로부터 경과된 시간
            Debug.Log("JudgeLine - Time: " + (timeSinceStart - script_Note.time));
            Hide();
        }
    }

    public void Hide()
    {
        if (isHide == false)
        {
            isHide = true;
            animator.SetBool("Destroy", true);
            StopObjectMovement();
            gameObject.SetActive(true);
            StartCoroutine(DelayForHide());
        }
    }

    private IEnumerator DelayForHide()
    {
        yield return new WaitForSeconds(hideTime);
        rigid2D.gravityScale = 0f;
        StopObjectMovement();
        yield return new WaitForSeconds(hideTime);
        rigid2D.gravityScale = 1f;
        gameObject.SetActive(false);
        isHide = false;
    }

    private void StopObjectMovement()
    {
        rigid2D.velocity = Vector2.zero;
        rigid2D.angularVelocity = 0f;
        rigid2D.Sleep();
    }
}
