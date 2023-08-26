using UnityEngine;

public class Choi_TimeLineController : MonoBehaviour
{
    public GameObject[] notes;
    public Park_MainSceneOpacity mainSceneOpacity;
    public RectTransform movingObject;
    public Choi_LoadScene loadScene;
    public float duration = 30f;
    private float startDelay = 6.0f;
    private float startTime;
    private float startX = 16f;
    private float endX = 1264f;
    private string sceneName = "ResultScene";
    private bool isStarted = false;

    private void Awake()
    {
        int index = Park_GameManager.index;
        duration = float.Parse(Park_GameManager.instance.musicInformation["Time"][index]) - 3.0f;
    }

    private void Start()
    {
        Invoke("StartMoving", 3f); // 3초 후에 StartMoving 함수 호출
    }

    private void Update()
    {
        if (!isStarted)
            return;

        float t = (Time.time - startTime) / duration;
        float newX = Mathf.Lerp(startX, endX, t);

        Vector3 newPosition = movingObject.anchoredPosition;
        newPosition.x = newX;
        movingObject.anchoredPosition = newPosition;

        if (t >= 1f)
        {
            SetAllNoteActiveForFalse(); // 모든 노트가 비활성화되었는지 확인
        }
    }

    private void StartMoving()
    {
        isStarted = true;
        startTime = Time.time;
    }

    private void SetAllNoteActiveForFalse()
    {
        foreach (GameObject note in notes)
        {
            note.SetActive(false); // 각 노트를 비활성화합니다.
        }

        // 모든 노트가 비활성화되었을 때의 처리
        enabled = false; // 이 스크립트를 비활성화하여 업데이트 중지
        StartCoroutine(mainSceneOpacity.EndOpacity());
        loadScene.Run(1f, sceneName);
    }

}
