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
        }
    }

}
