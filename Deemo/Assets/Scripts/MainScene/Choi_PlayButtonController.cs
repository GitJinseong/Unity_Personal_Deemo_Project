using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_PlayButtonController : MonoBehaviour
{
    public Choi_LoadScene loadScene = default;

    public void LoadScene()
    {
        // 다음 씬으로 전환
        loadScene.asyncLoadScene("PlayScene");
    }
}
