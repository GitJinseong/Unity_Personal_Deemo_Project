using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_DelayReadyObject : MonoBehaviour
{
    public GameObject obj_Ready;
    public float delay = 9f;

    void Start()
    {
        StartCoroutine(DelayForActiveTrue());
    }

    public IEnumerator DelayForActiveTrue()
    {
        yield return new WaitForSeconds(delay);
        obj_Ready.SetActive(true);
    }
}
