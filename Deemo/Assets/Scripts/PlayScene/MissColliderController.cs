using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissColliderController : MonoBehaviour
{
    private Transform grandparent = default;

    private void Awake()
    {
        grandparent = gameObject.transform.parent.parent;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Note"))
    //    {
    //        Debug.Log("Miss");
    //        HideForParent();
    //    }
    //}


    //private void HideForParent()
    //{
    //    // 현재 오브젝트의 부모의 부모 비활성화
    //    if (grandparent != null)
    //    {
    //        grandparent.gameObject.SetActive(false);
    //    }
    //}
}
