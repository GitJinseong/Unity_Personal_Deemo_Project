using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_CallLoadScene : MonoBehaviour
{
    public Choi_LoadScene loadScene;
    public string sceneName;
    public float delay = 4.0f;
    // Start is called before the first frame update

    void Start()
    {
        loadScene.Run(delay, sceneName);
    }
}
