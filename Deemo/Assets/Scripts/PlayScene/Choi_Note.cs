using UnityEngine;

public class Choi_Note : MonoBehaviour
{
    public Choi_CollisionDetection collisionDetection;
    public int noteId;
    public float time;
    public float runTime;
    public float duration = 0.0f;
    public float durtationDifference = 0.0f;
    private bool isEnabled = false;

    private void Update()
    {
        if (isEnabled)
        {
            runTime += Time.deltaTime;

            if (runTime >= duration - durtationDifference)
            {
                isEnabled = false;
                collisionDetection.JudgeLineRemoveNote();
            }
        }
    }

    private void OnEnable()
    {
        isEnabled = true;
        runTime = 0f;
    }

    private void OnDisable()
    {
        isEnabled = false;
        // 비활성화될 때 경과 시간을 계산하여 출력
        Debug.Log($"{runTime} seconds.");
    }
}
