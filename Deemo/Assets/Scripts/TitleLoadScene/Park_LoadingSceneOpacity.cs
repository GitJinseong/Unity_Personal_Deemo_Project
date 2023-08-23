using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_LoadingSceneOpacity : MonoBehaviour
{
    public Park_MainSceneOpacity park_MainSceneOpacity;
    public Choi_CallLoadScene choi_CallLoadScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coroutine(choi_CallLoadScene.delay));
    }

    private IEnumerator Coroutine(float time)
    {
        yield return new WaitForSeconds(time - park_MainSceneOpacity.duration);

        StartCoroutine(park_MainSceneOpacity.EndOpacity());
    }
}
