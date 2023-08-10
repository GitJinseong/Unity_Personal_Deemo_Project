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
        }
    }

}
