using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_PlayButtonController : MonoBehaviour
{
    public Choi_LoadScene loadScene = default;
    public float delay = 0.3f;
    public string sceneName;
    public void LoadScene()
    {
        // 다음 씬으로 전환
        loadScene.Run(delay, sceneName);
    }
}
