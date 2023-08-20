using UnityEngine;

public class Choi_Note : MonoBehaviour
{
    public int noteId;
    public float time;
    public float startTime;
    public string stringJudge;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        // 매 프레임마다 gameObject의 transform.position.y 값을 월드 좌표로 변환하여 디버그 출력
        //Vector3 worldPosition = transform.TransformPoint(new Vector3(0, 0, transform.position.z));
        //Debug.Log($"World Y Position: {worldPosition.z}");
    }

    public void Initialize()
    {
        startTime = Time.realtimeSinceStartup;
        // 그 외 초기화 로직...
    }
}