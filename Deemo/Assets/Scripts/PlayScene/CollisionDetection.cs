using System.Collections;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private Animator animator; // Animator 컴포넌트 레퍼런스

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("JudgeLine"))
        {

            gameObject.SetActive(false);
            //StartCoroutine(DelayForRemove());
        }
    }

    public IEnumerator DelayForRemove()
    {
        animator.SetBool("Destroy", true);

        // 애니메이션 길이만큼 기다림
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        yield return new WaitForSeconds(1f);

        // 애니메이션 트리거 리셋
        animator.SetBool("Destroy", false);

        gameObject.SetActive(false);
    }
}
