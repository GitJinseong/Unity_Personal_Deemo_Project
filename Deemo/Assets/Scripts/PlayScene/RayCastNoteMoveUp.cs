using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastNoteMoveUp : MonoBehaviour
{
    public float moveSpeed = 2.0f; // 이동 속도

    private void Update()
    {
        // 물체를 아래로 이동시킴
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
