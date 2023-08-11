using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonController : MonoBehaviour
{
    public LoadScene loadScene = default;

    public void LoadScene()
    {
        // 다음 씬으로 전환
        loadScene.asyncLoadScene("PlayScene");
    }
}
