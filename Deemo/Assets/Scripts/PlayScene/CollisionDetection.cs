using System.Collections;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator; // Animator 컴포넌트 레퍼런스
    private float hideTime = 1f;
    private bool isHide = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TopLine"))
        {
            Debug.Log("TopLine");
        }

        if (collision.gameObject.CompareTag("JudgeLine"))
        {
            if (isHide == false)
            {
                isHide = true;
                animator.SetBool("Destroy", true);
                StopObjectMovement(); // 충돌시 바로 정지

                // 오브젝트 활성화 후 코루틴 실행
                gameObject.SetActive(true);
                StartCoroutine(DelayForHide());
            }
        }
    }

    private IEnumerator DelayForHide()
    {
        yield return new WaitForSeconds(hideTime);

        // 중력 비활성화
        rigidbody2D.gravityScale = 0f;

        // 정지된 상태에서 정지된 위치를 유지
        StopObjectMovement();

        // 일정 시간 후에 중력 다시 활성화
        yield return new WaitForSeconds(hideTime);
        rigidbody2D.gravityScale = 1f;

        // 비활성화
        gameObject.SetActive(false);
        isHide = false; // 재사용을 위해 isHide 초기화
    }

    private void StopObjectMovement()
    {
        // Rigidbody2D의 속도와 회전 속도를 0으로 설정하여 움직임 정지
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0f;
        rigidbody2D.Sleep(); // 물리 시뮬레이션 비활성화로 정지
    }
}
