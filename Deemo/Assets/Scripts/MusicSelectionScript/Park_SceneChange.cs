using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_SceneChange : MonoBehaviour
{
    public Park_LoadScene loadScene;

    // 처음 클릭한 마우스 위치
    private Vector2 StartMousePosition;

    // 현재 마우스 위치
    private Vector2 MoveMousePosition;

    private bool isPressed = false;
    private bool isPoint = false;

    private void Update()
    {
        if (StartMousePosition != (Vector2)Input.mousePosition)
        {
            isPoint = false;
        }
    }

    private void OnMouseDown()
    {
        StartMousePosition = Input.mousePosition;

        isPoint = true;

        isPressed = true;
    }

    private void OnMouseUp()
    {
        if (isPoint == true)
        {
            loadScene.Run(0.0f, "PlayScene");
        }

        isPressed = false;
    }
}
