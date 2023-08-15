using System.Collections;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private Animator animator; // Animator ������Ʈ ���۷���
    private float hideTime = 1f;
    public bool isHide = false;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("JudgeLine"))
        {
            Debug.Log("JudgeLine");
            Hide();
        }
    }

    public void Hide()
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

    private IEnumerator DelayForHide()
    {
        yield return new WaitForSeconds(hideTime);

        // �߷� ��Ȱ��ȭ
        rigid2D.gravityScale = 0f;

        // ������ ���¿��� ������ ��ġ�� ����
        StopObjectMovement();

        // ���� �ð� �Ŀ� �߷� �ٽ� Ȱ��ȭ
        yield return new WaitForSeconds(hideTime);
        rigid2D.gravityScale = 1f;

        // ��Ȱ��ȭ
        gameObject.SetActive(false);
        isHide = false; // ������ ���� isHide �ʱ�ȭ
    }

    private void StopObjectMovement()
    {
        // Rigidbody2D�� �ӵ��� ȸ�� �ӵ��� 0���� �����Ͽ� ������ ����
        rigid2D.velocity = Vector2.zero;
        rigid2D.angularVelocity = 0f;
        rigid2D.Sleep(); // ���� �ùķ��̼� ��Ȱ��ȭ�� ����
    }
}
