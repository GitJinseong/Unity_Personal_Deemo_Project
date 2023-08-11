using System.Collections;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator; // Animator ������Ʈ ���۷���
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
                StopObjectMovement(); // �浹�� �ٷ� ����

                // ������Ʈ Ȱ��ȭ �� �ڷ�ƾ ����
                gameObject.SetActive(true);
                StartCoroutine(DelayForHide());
            }
        }
    }

    private IEnumerator DelayForHide()
    {
        yield return new WaitForSeconds(hideTime);

        // �߷� ��Ȱ��ȭ
        rigidbody2D.gravityScale = 0f;

        // ������ ���¿��� ������ ��ġ�� ����
        StopObjectMovement();

        // ���� �ð� �Ŀ� �߷� �ٽ� Ȱ��ȭ
        yield return new WaitForSeconds(hideTime);
        rigidbody2D.gravityScale = 1f;

        // ��Ȱ��ȭ
        gameObject.SetActive(false);
        isHide = false; // ������ ���� isHide �ʱ�ȭ
    }

    private void StopObjectMovement()
    {
        // Rigidbody2D�� �ӵ��� ȸ�� �ӵ��� 0���� �����Ͽ� ������ ����
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0f;
        rigidbody2D.Sleep(); // ���� �ùķ��̼� ��Ȱ��ȭ�� ����
    }
}
