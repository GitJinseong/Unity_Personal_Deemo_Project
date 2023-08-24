using UnityEngine;

public class Choi_PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    private void Start()
    {
        //Time.timeScale = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // P 키를 누르면 일시정지/해제
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0f; // 게임 일시정지
        isPaused = true;
    }

    private void ResumeGame()
    {
        Debug.Log("Game Resumed");
        Time.timeScale = 1f; // 게임 해제
        isPaused = false;
    }
}
