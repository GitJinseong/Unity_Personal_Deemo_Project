using System.Collections;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private Animator animator; // Animator ������Ʈ ���۷���

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

        // �ִϸ��̼� ���̸�ŭ ��ٸ�
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        yield return new WaitForSeconds(1f);

        // �ִϸ��̼� Ʈ���� ����
        animator.SetBool("Destroy", false);

        gameObject.SetActive(false);
    }
}
